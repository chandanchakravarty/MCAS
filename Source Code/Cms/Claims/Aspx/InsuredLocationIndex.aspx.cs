/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		28-04-2006
<End Date			: -	
<Description		: - 	Index Page for Insured Locations.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance; 
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;


namespace Cms.Claims.Aspx
{

	/// <summary>
	/// 
	/// </summary>
	public class InsuredLocationIndex : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Literal litTextGrid;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private string ClaimID = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id and Claim ID
			if(Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"] != "")
			{
				ClaimID	=	Request.QueryString["CLAIM_ID"];
			}

			base.ScreenId	=	"306_3";
			#endregion

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
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE= " * ";
				sFROMCLAUSE  = " ( SELECT INSURED_LOCATION_ID, CCI.CLAIM_NUMBER AS CLAIM_NUMBER, LOCATION_DESCRIPTION, CIL.ADDRESS1 + ' ' + CIL.ADDRESS2 AS ADDRESS, MCSL.STATE_NAME AS STATE, CIL.ZIP, MCL.COUNTRY_NAME AS COUNTRY, CIL.IS_ACTIVE FROM CLM_INSURED_LOCATION CIL LEFT JOIN MNT_COUNTRY_STATE_LIST MCSL ON CIL.STATE = MCSL.STATE_ID LEFT JOIN MNT_COUNTRY_LIST MCL ON CIL.COUNTRY = MCL.COUNTRY_ID LEFT JOIN CLM_CLAIM_INFO CCI ON CIL.CLAIM_ID = CCI.CLAIM_ID WHERE CIL.CLAIM_ID = " + ClaimID.Trim()  + " ) Test ";
				
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.SearchColumnHeadings = "Address^State^Zip^Description";	
				objWebGrid.SearchColumnNames = "ADDRESS^STATE^ZIP^LOCATION_DESCRIPTION";
				objWebGrid.SearchColumnType = "T^T^T^T";
				objWebGrid.DisplayColumnHeadings = "Location #^Address^State^Zip^Description";
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
				objWebGrid.DisplayColumnNames = "INSURED_LOCATION_ID^ADDRESS^STATE^ZIP^LOCATION_DESCRIPTION";
				objWebGrid.DisplayTextLength = "10^20^10^10^30";
				objWebGrid.DisplayColumnPercent = "10^20^10^10^30";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "INSURED_LOCATION_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B";
				objWebGrid.HeaderString ="Insured Location" ;
				objWebGrid.OrderByClause	="INSURED_LOCATION_ID asc";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;				
				objWebGrid.FilterLabel = "Include Inactive";//Uncommented for Itrack Issue 5833 on 21 July 2009				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "INSURED_LOCATION_ID";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterColumnName = "IS_ACTIVE";//Uncommented for Itrack Issue 5833 on 21 July 2009
				objWebGrid.FilterValue = "Y";//Uncommented for Itrack Issue 5833 on 21 July 2009
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddInsuredLocation.aspx?CLAIM_ID=" + Request["CLAIM_ID"].ToString() + "&"; 
				TabCtl.TabTitles ="Insured Location";
				TabCtl.TabLength =150;

			}
			catch (Exception ex)
			{throw (ex);}
			#endregion

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}