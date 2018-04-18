/******************************************************************************************
<Author					: - >Amar Singh
<Start Date				: -	>May 03,2006
<End Date				: - >
<Description			: - >This file is being used to show/search Insured Vehicle Information
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
	/// This class is used for showing grid that search and display Insured Vehicle Records
	/// </summary>
	public class InsuredVehicleIndex : Cms.Claims.ClaimBase
	{
		//protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected System.Web.UI.WebControls.Label lblError;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_RECORD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		string strClaim_ID="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			base.ScreenId="306_2";
			
			// Put user code to initialize the page here

			
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
				GetQueryStringValues();
				string sSELECTCLAUSE="", sWHERECLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE+= " * ";
				
				sFROMCLAUSE  = " CLM_INSURED_VEHICLE  ";
				sWHERECLAUSE = " CLAIM_ID = " + strClaim_ID;
			
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.WhereClause = sWHERECLAUSE;	
				objWebGrid.SearchColumnHeadings = "VIN^Vehicle Number^Year^Make^Model^Body Type";
				objWebGrid.SearchColumnNames = "VIN^INSURED_VEHICLE_ID^VEHICLE_YEAR^MAKE^MODEL^BODY_TYPE";
				objWebGrid.SearchColumnType = "T^T^T^T^T^T";
				objWebGrid.DisplayColumnHeadings = "VIN^Vehicle Number^Year^Make^Model^Body Type";
				objWebGrid.DisplayColumnNumbers = "7^1^4^5^6^8";
				objWebGrid.DisplayColumnNames = "VIN^INSURED_VEHICLE_ID^VEHICLE_YEAR^MAKE^MODEL^BODY_TYPE";
				objWebGrid.DisplayTextLength = "50^10^4^20^20^50";
				objWebGrid.DisplayColumnPercent = "15^15^10^20^20^20";
				objWebGrid.PrimaryColumns = "2";
				objWebGrid.PrimaryColumnsName = "INSURED_VEHICLE_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				if(hidLOB_ID.Value==((int)(enumLOB.CYCL)).ToString())
				{
					objWebGrid.HeaderString ="Insured Motorcycle Information" ;
					TabCtl.TabTitles ="Insured Motorcycle";
				}
				else
				{
					objWebGrid.HeaderString ="Insured Vehicle Information" ;
					TabCtl.TabTitles ="Insured Vehicle";
				}
				objWebGrid.OrderByClause	="INSURED_VEHICLE_ID Asc";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;				
				objWebGrid.FilterLabel = "Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.SystemColumnName = "IS_ACTIVE";
				objWebGrid.QueryStringColumns = "INSURED_VEHICLE_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddInsuredVehicle.aspx?CLAIM_ID=" + strClaim_ID + "&LOB_ID=" + hidLOB_ID.Value +  "&"; 				
				TabCtl.TabLength =150;
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion


		}
		private void GetQueryStringValues()
		{
			if(Request["Claim_ID"]!=null && Request["Claim_ID"].ToString()!="")
				strClaim_ID = Request["Claim_ID"].ToString();
			else
				strClaim_ID = "";

			if(Request["LOB_ID"]!=null && Request["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request["LOB_ID"].ToString();
			else
				hidLOB_ID.Value = "";

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
