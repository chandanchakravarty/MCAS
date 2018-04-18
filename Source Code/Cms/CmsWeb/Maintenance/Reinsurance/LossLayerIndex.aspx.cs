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
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for LossLayerIndex.
	/// </summary>
	public class LossLayerIndex :  Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
	
		protected string strContractID = "";
        ResourceManager objResourceMgr = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				base.ScreenId = "262_9";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.LossLayerIndex", Assembly.GetExecutingAssembly());
		
				if(Request.QueryString["CONTRACT_ID"]!=null && Request.QueryString["CONTRACT_ID"].ToString().Length>0)
					strContractID = Request.QueryString["CONTRACT_ID"].ToString();

				#region G E T T I N G   B A S E   C O L O R   F O R   R O W   S E L E C T I O N

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

				# region C O D E   F O R   G R I D   C O N T R O L

				Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = "http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//objWebGrid.SelectClause			= "COVERAGE_CATEGORY_ID,EFFECTIVE_DATE,LOB_ID,CATEGORY,ISNULL(IS_ACTIVE,'Y') AS IS_ACTIVE";

                int LangId = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    LangId = 2;
                else
                    LangId = 1;

                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;

                string SelectClause = " LOSS_LAYER_ID,CONTRACT_ID,LAYER,case " + GetLanguageID() + " when 2 then case when  MYN.VALUE_DESC ='yes' then 'Sim' else 'Não' end else MYN.VALUE_DESC  end  AS COMPANY_RETENTION, " +
                                      " dbo.fun_FormatCurrency(LAYER_AMOUNT," + LangId + ") LAYER_AMOUNT, " +
                                      " dbo.fun_FormatCurrency(RETENTION_AMOUNT," + LangId + ") RETENTION_AMOUNT, " +
                                      " CASE WHEN " + BaseCurrency + "=2 THEN REPLACE(CAST( RETENTION_PERCENTAGE AS VARCHAR(50)),'.',',') ELSE CAST( RETENTION_PERCENTAGE AS VARCHAR(50)) END AS RETENTION_PERCENTAGE , " +
                                      " CASE WHEN " + BaseCurrency + "=2 THEN REPLACE(CAST( REIN_CEDED_PERCENTAGE AS VARCHAR(50)),'.',',') ELSE CAST( REIN_CEDED_PERCENTAGE AS VARCHAR(50)) END AS REIN_CEDED_PERCENTAGE , " +
                                      //" dbo.fun_FormatCurrency(RETENTION_PERCENTAGE," + LangId + ") RETENTION_PERCENTAGE, " +
                                      //" dbo.fun_FormatCurrency(REIN_CEDED_PERCENTAGE," + LangId + ") REIN_CEDED_PERCENTAGE, " +
                                      " dbo.fun_FormatCurrency(REIN_CEDED," + LangId + ") REIN_CEDED, " +                                     
                                      " MRL.IS_ACTIVE";

                objWebGrid.SelectClause         = SelectClause;// "LOSS_LAYER_ID,CONTRACT_ID,LAYER,MYN.VALUE_DESC AS COMPANY_RETENTION,LAYER_AMOUNT,RETENTION_AMOUNT,RETENTION_PERCENTAGE,REIN_CEDED_PERCENTAGE,REIN_CEDED,MRL.IS_ACTIVE";
				objWebGrid.FromClause			= "MNT_REIN_LOSSLAYER MRL INNER JOIN MNT_YESNO MYN ON  MRL.COMPANY_RETENTION = MYN.VALUE_ID";
				objWebGrid.WhereClause			= "CONTRACT_ID = '"+Request.QueryString["CONTRACT_ID"]+"'";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Layer^Company Retention^Layer Amount^Retention %^Company Retention Amount^Ceded %^Ceded Amount";
				objWebGrid.SearchColumnNames	= "LAYER^MYN.VALUE_DESC^LAYER_AMOUNT^RETENTION_PERCENTAGE^RETENTION_AMOUNT^REIN_CEDED_PERCENTAGE^REIN_CEDED";
				objWebGrid.SearchColumnType		= "T^T^T^T^T^T^T";
				objWebGrid.DropDownColumns          =   "^^^^^^^";
				objWebGrid.OrderByClause		= "LOSS_LAYER_ID asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7^8";
				objWebGrid.DisplayColumnNames	= "LAYER^COMPANY_RETENTION^LAYER_AMOUNT^RETENTION_PERCENTAGE^RETENTION_AMOUNT^REIN_CEDED_PERCENTAGE^REIN_CEDED";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Layer^Company Retention^Layer Amount^Retention %^Company Retention Amount^Ceded %^Ceded Amount";
				objWebGrid.DisplayTextLength	= "100^100^100^100^100^100";
				objWebGrid.DisplayColumnPercent = "15^10^15^15^15^15^15";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "LOSS_LAYER_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Loss Layer";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="IS_ACTIVE";
				objWebGrid.QueryStringColumns = "LOSS_LAYER_ID^CONTRACT_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.CellHorizontalAlign = "2^3^4^5^6";
				
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "LossLayer.aspx?ContractID="+strContractID+"&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

				# endregion
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
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
