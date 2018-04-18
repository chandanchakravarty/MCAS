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

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Summary description for InstallmentPlanIndex.
	/// </summary>
	public class InstallmentPlanIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		//private string strCustomerID, strAppId, strAppVersionId;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        ResourceManager objResourceMgr = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="181";   // Screen Id set to 181 instead of 183 to Include it into Maintenance-Accounting-Billing Permission List- Done by Sibin on 23 Oct 08
			if (!CanShow())
			{
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("401");
				capMessage.Visible=true;
				return;
			}
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Accounting.InstallmentPlanIndex", Assembly.GetExecutingAssembly());
			
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

				string strAppPolTerm  = " CASE LKP.LOOKUP_VALUE_DESC WHEN '1 Year' then '12 Months' WHEN 'Term Not Defined In Months' THEN 'Any' ELSE LKP.LOOKUP_VALUE_DESC END ";
				string strDefaultPlan =" CASE WHEN IPD.DEFAULT_PLAN = '1' THEN 'Yes' ELSE 'No' END";

                objWebGrid.SelectClause = " DISTINCT CASE IPD.APP_LOB WHEN 0 THEN 'ALL' ELSE LOB_DESC END AS LOB_DESC,IDEN_PLAN_ID,APP_LOB,PLAN_CODE,PLAN_DESCRIPTION,CASE PLAN_TYPE WHEN 'M' THEN 'MONTHLY' ELSE PLAN_TYPE END "
										+ " PLAN_TYPE,NO_OF_PAYMENTS,MONTHS_BETWEEN,IPD.IS_ACTIVE,ISNULL(MNT.LOOKUP_VALUE_DESC,'') AS PLAN_PAYMENT_MODE,'IDEN_PLAN_ID='+ISNULL(CAST(IDEN_PLAN_ID AS VARCHAR(8000)),0) AS UNIQUEGRDID, "
										+ strAppPolTerm + "as APPLABLE_POLTERM," + strDefaultPlan + " as DEFAULT_PLAN";

                objWebGrid.FromClause = " ACT_INSTALL_PLAN_DETAIL IPD LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT ON MNT.LOOKUP_UNIQUE_ID = IPD.PLAN_PAYMENT_MODE "
                                        + "LEFT OUTER JOIN  MNT_LOB_MASTER ON APP_LOB=LOB_ID "
										+ " LEFT JOIN MNT_LOOKUP_VALUES LKP ON CONVERT(varchar,IPD.APPLABLE_POLTERM)  = LKP.LOOKUP_VALUE_CODE AND LKP.LOOKUP_ID IN(315,394,1189,1190,1191,1192,1193,1194)";
				//objWebGrid.WhereClause  = " (IPD.IS_ACTIVE='Y') AND ((REPLACE(PLAN_CODE,' ','') LIKE ('%%')))";
				objWebGrid.WhereClause  = " ((REPLACE(PLAN_CODE,' ','') LIKE ('%%')))";


                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Plan Code^Description^Plan Type^No of Payments^Months Between^Plan Payment Mode^Applicable to Policy Term^Default Plan";
				objWebGrid.SearchColumnNames = "PLAN_CODE^PLAN_DESCRIPTION^APP_LOB^NO_OF_PAYMENTS^MNT.LOOKUP_VALUE_DESC^" + strAppPolTerm + "^" + strDefaultPlan;
				objWebGrid.SearchColumnType = "T^T^L^T^T^T^T";
                objWebGrid.DropDownColumns = "^^MNTLOB^^^^";
				
				//objWebGrid.OrderByClause = "PLAN_CODE ASC";
				objWebGrid.OrderByClause = "PLAN_DESCRIPTION";
			
				
				objWebGrid.DisplayColumnNumbers = "2^3^1^5^8^10^11";
				objWebGrid.DisplayColumnNames = "PLAN_CODE^PLAN_DESCRIPTION^LOB_DESC^NO_OF_PAYMENTS^PLAN_PAYMENT_MODE^APPLABLE_POLTERM^DEFAULT_PLAN";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Plan Code^Description^Plan Type^# of Payments^Months Between^Plan Payment Mode^Applicable to Policy Term^Default Plan";

				objWebGrid.DisplayTextLength = "15^20^15^10^10^15^15";
				objWebGrid.DisplayColumnPercent = "15^20^10^10^10^15^15";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "IDEN_PLAN_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "2^3^1^5^8^10^11";

                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";

                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");// "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");// "Billing Plan Details";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "IDEN_PLAN_ID";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="IPD.IS_ACTIVE";
				objWebGrid.FilterValue="Y";

				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch
			{
			}
			#endregion
            TabCtl.TabURLs = "AddInstallmentPlan.aspx??&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
		}

		private bool CanShow()
		{
			//Checking whether customer id exits in database or not
			//if (System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper() != GetSystemId().ToUpper() )
            if (CarrierSystemID != GetSystemId().ToUpper())
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
