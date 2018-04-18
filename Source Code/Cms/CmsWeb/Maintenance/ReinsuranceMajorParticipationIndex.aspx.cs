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
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for ReinsuranceMajorParticipationIndex.
	/// </summary>
	public class ReinsuranceMajorParticipationIndex : Cms.CmsWeb.cmsbase
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
        ResourceManager objResourceMgr = null;

		protected string strContractID = "0";

		# endregion 

		# region P A G E   L O A D 
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
			
				// Put user code to initialize the page here

				//base.ScreenId = "262_5";
					base.ScreenId = "262_3";
                    objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ReinsuranceMajorParticipationIndex", Assembly.GetExecutingAssembly());
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

				objWebGrid.SelectClause			= "MRP.PARTICIPATION_ID AS PARTICIPATION_ID,";
                objWebGrid.SelectClause += "MRCL.REIN_COMAPANY_NAME AS REIN_COMAPANY_NAME,MRP.LAYER AS LAYER,isnull(mlmm.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) AS NET_RETENTION,ISNULL(MRP.WHOLE_PERCENT,0) As WHOLE_PERCENT,isnull(mlmm1.LOOKUP_VALUE_DESC,MLV1.LOOKUP_VALUE_DESC) AS MINOR_PARTICIPANTS,";
				objWebGrid.SelectClause			+="MRP.IS_ACTIVE,MRP.CONTRACT_ID";
				
				objWebGrid.FromClause			= "MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MRP "; 
				objWebGrid.FromClause			+= "INNER JOIN MNT_LOOKUP_VALUES MLV ON MRP.NET_RETENTION = MLV.LOOKUP_UNIQUE_ID INNER JOIN MNT_LOOKUP_VALUES MLV1 ON MRP.MINOR_PARTICIPANTS = MLV1.LOOKUP_UNIQUE_ID  ";
                objWebGrid.FromClause += "INNER JOIN MNT_REIN_COMAPANY_LIST MRCL ON MRP.REINSURANCE_COMPANY = MRCL.REIN_COMAPANY_ID left outer  join MNT_LOOKUP_VALUES_MULTILINGUAL mlmm on MLV.LOOKUP_UNIQUE_ID=mlmm.LOOKUP_UNIQUE_ID and mlmm.LANG_ID="+GetLanguageID()+" left outer join MNT_LOOKUP_VALUES_MULTILINGUAL mlmm1 on MLV1.LOOKUP_UNIQUE_ID = mlmm1.LOOKUP_UNIQUE_ID  and mlmm1.LANG_ID ="+GetLanguageID();	
				
				objWebGrid.WhereClause			= "MRP.CONTRACT_ID = " + strContractID + ""; // AND PARTICIPATION_TYPE = 'MAJOR' ";//FOR MAJOR PARTICIPATION SCREEN
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Reinsurer/Broker^Layer^Net Retention^% of Whole Layer^Minor Participants";
				objWebGrid.SearchColumnNames	= "REIN_COMAPANY_NAME^LAYER^MLV.LOOKUP_VALUE_DESC^WHOLE_PERCENT^MLV1.LOOKUP_VALUE_DESC";
				objWebGrid.SearchColumnType		= "T^T^T^T^T";
				objWebGrid.OrderByClause		= "PARTICIPATION_ID asc";
				
				objWebGrid.DisplayColumnNumbers = "2^3^4^5^6";
				objWebGrid.DisplayColumnNames	= "REIN_COMAPANY_NAME^LAYER^NET_RETENTION^WHOLE_PERCENT^MINOR_PARTICIPANTS";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Reinsurer/Broker^Layer^Net Retention^% of Whole Layer^Minor Participants";
				objWebGrid.DisplayTextLength	= "100^100^130^100^120";
				objWebGrid.DisplayColumnPercent = "20^20^20^20^20";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "PARTICIPATION_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Major Participation";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="MRP.IS_ACTIVE";
				objWebGrid.QueryStringColumns = "PARTICIPATION_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				//TabCtl.TabURLs = "AddReinsuranceMajorPart.aspx?ContractID="+strContractID+"&";
				//TabCtl.TabURLs = "AddReinsuranceMajorPart.aspx?"+"&";

				# endregion
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
            //SANTOSH GAUTAM : BELOW LINE MODIFIED ON 01 Nov 2010
            //OLD VALUE => TabCtl.TabURLs = "AddReinsuranceMajorPart.aspx;           
            TabCtl.TabURLs = "AddReinsuranceMajorPart.aspx?ContractID=" + strContractID + "&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
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
