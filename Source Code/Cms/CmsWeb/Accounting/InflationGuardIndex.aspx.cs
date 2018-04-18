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
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Summary description for InflationGuardIndex.
	/// </summary>
	public class InflationGuardIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
	//	private string strCustomerID, strAppId, strAppVersionId;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="365";
			if (!CanShow())
			{
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("401");
				capMessage.Visible=true;
				return;
			}
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Accounting.InflationGuardIndex", Assembly.GetExecutingAssembly());

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

			#region loading web grid control
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "ICF.INFLATION_ID,ICF.LOB_ID,ICF.ZIP_CODE,ICF.STATE_ID,CSL.STATE_NAME,convert(varchar(20),EFFECTIVE_DATE,101) as EFFECTIVE_DATE ,"
										+ " convert(varchar(20),EXPIRY_DATE,101) as EXPIRY_DATE,ICF.FACTOR,ICF.IS_ACTIVE,ICF.LOB_ID,CASE ICF.LOB_ID WHEN 0 THEN 'Home & Rental' ELSE LOB.LOB_DESC END AS LOB_DESC";
				objWebGrid.FromClause = " INFLATION_COST_FACTORS ICF INNER JOIN MNT_COUNTRY_STATE_LIST CSL ON CSL.STATE_ID = ICF.STATE_ID "
										+ " LEFT OUTER JOIN MNT_LOB_MASTER LOB ON LOB.LOB_ID = ICF.LOB_ID";
				objWebGrid.WhereClause = " ";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Zip Code(First 3 chars)^State^Effective Date^Expiry Date^Inflation Factor";
				objWebGrid.SearchColumnNames = "ZIP_CODE^STATE_NAME^EFFECTIVE_DATE^EXPIRY_DATE^FACTOR";
				objWebGrid.SearchColumnType = "T^T^D^D^T";
				objWebGrid.OrderByClause = "";
				
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                objWebGrid.DisplayColumnNames = "ZIP_CODE^LOB_DESC^STATE_NAME^EFFECTIVE_DATE^EXPIRY_DATE^FACTOR";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Zip Code^Lob^State^Effective Date^Expiry Date^Inflation Factor";

				objWebGrid.DisplayTextLength = "10^15^15^15^15^15^15";
				objWebGrid.DisplayColumnPercent = "10^15^15^15^15^15^15";
				objWebGrid.PrimaryColumns = "3";
				objWebGrid.PrimaryColumnsName = "ICF.ZIP_CODE^ICF.LOB_ID^ICF.STATE_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6";

                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";

                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Inflation Guard Setup";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "INFLATION_ID^ZIP_CODE^STATE_ID^LOB_ID";
				/*objWebGrid.FilterLabel="Show Inactive";
				objWebGrid.FilterColumnName="ICF.IS_ACTIVE";
				objWebGrid.FilterValue="Y";	*/			

				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);

				TabCtl.TabURLs   =  "InflationGuardDetails.aspx?&";
              
				
			}
			catch
			{
			}
			#endregion
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");//"Inflation Guard Details";
						
		}

		private bool CanShow()
		{
			//Checking whether customer id exits in database or not
			//if (System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper() != GetSystemId().ToUpper() )
            if (CarrierSystemID.ToUpper() != GetSystemId().ToUpper())
			{
				return false;
			}

			return true;
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
