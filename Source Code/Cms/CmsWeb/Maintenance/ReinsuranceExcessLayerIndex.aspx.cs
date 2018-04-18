/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is Grid file used for Excess Layer for a reinsurance contract. 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/


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
using Cms.CmsWeb.WebControls;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for ReinsuranceExcessLayerIndex.
	/// </summary>
	public class ReinsuranceExcessLayerIndex : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N 

		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		protected string strContractID = "";

		# endregion 		
		
		# region P A G E   L O A D 
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
			
				// Put user code to initialize the page here

				base.ScreenId = "262_1";

				if(Request.QueryString["CONTRACT_ID"]!=null && Request.QueryString["CONTRACT_ID"].ToString().Length>0)
					strContractID = Request.QueryString["CONTRACT_ID"].ToString();

				#region G E T T I N G   B A S E   C O L O R   F O R   R O W   S E L E C T I O N
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

				# region C O D E   F O R   G R I D   C O N T R O L

				Control c1 = LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");

				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause			= "MRE.EXCESS_ID AS EXCESS_ID,MRC.CONTRACT_NUMBER AS CONTRACT_NUMBER,MRE.CONTRACT_ID AS CONTRACT_ID,ISNULL(MRE.LAYER_AMOUNT,0) as LAYER_AMOUNT,";
				objWebGrid.SelectClause			+="ISNULL(MRE.UNDERLYING_AMOUNT,0) as UNDERLYING_AMOUNT,ISNULL(MRE.LAYER_PREMIUM,0) As LAYER_PREMIUM,ISNULL(MRE.CEDING_COMMISSION,0) AS CEDING_COMMISSION,ISNULL(MRE.AC_PREMIUM,0) as AC_PREMIUM,";
				objWebGrid.SelectClause			+="ISNULL(MRE.IS_ACTIVE,'Y') AS IS_ACTIVE,MRE.CREATED_BY AS CREATED_BY,MRE.CREATED_DATETIME AS CREATED_DATETIME,MRE.MODIFIED_BY AS MODIFIED_BY,MRE.LAST_UPDATED_DATETIME AS LAST_UPDATED_DATETIME";
				objWebGrid.FromClause			= "MNT_REINSURANCE_EXCESS MRE INNER JOIN MNT_REINSURANCE_CONTRACT MRC ON MRE.CONTRACT_ID =  MRC.CONTRACT_ID";
				objWebGrid.WhereClause			= "MRE.CONTRACT_ID = " + strContractID + " AND LAYER_TYPE = 'E' ";//FOR PRO-RATA/QUOTA SHARE SCREEN
				objWebGrid.SearchColumnHeadings = "Excess ID^Contract ID^Layer Amount";
				objWebGrid.SearchColumnNames	= "EXCESS_ID^CONTRACT_ID^LAYER_AMOUNT";
				objWebGrid.SearchColumnType		= "T^T^T";
				objWebGrid.OrderByClause		= "EXCESS_ID asc";
				objWebGrid.DisplayColumnNumbers = "2^4^5^6^7^8";
				objWebGrid.DisplayColumnNames	= "CONTRACT_NUMBER^LAYER_AMOUNT^UNDERLYING_AMOUNT^LAYER_PREMIUM^CEDING_COMMISSION^AC_PREMIUM";
				objWebGrid.DisplayColumnHeadings= "Contract Number^Layer Amount^Underlying Amount^Layer Premium^Ceding Commission^A/C Premium";
				objWebGrid.DisplayTextLength	= "125^125^125^125^125^125";
				objWebGrid.DisplayColumnPercent = "20^15^15^15^15^20";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "EXCESS_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Reinsurance Excess Layer";
				objWebGrid.SelectClass = colors;
				objWebGrid.FilterLabel="Include Inactive";
				objWebGrid.FilterColumnName="MRE.IS_ACTIVE";
				objWebGrid.QueryStringColumns = "EXCESS_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddReinsuranceExcessLayer.aspx?ContractID="+strContractID+"&";

				# endregion
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion 

		#region W E B  F O R M   D E S I G N E R   G E N E R A T E D   C O D E
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
