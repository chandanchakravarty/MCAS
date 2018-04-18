/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 27, 2007
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
using System.Resources;
using System.Reflection;

using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for ConstructionTranslationIndex.
	/// </summary>
	public class ConstructionTranslationIndex : Cms.CmsWeb.cmsbase
	{
		
		# region D E C L A R A T I O N 

		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		//protected System.Web.UI.WebControls.Table tblReport;
		//protected System.Web.UI.WebControls.Panel pnlReport;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		# endregion 
		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				base.ScreenId = "399_1";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.ConstructionTranslationIndex", Assembly.GetExecutingAssembly());
		
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
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "T.REIN_CONSTRUCTION_CODE_ID,ISNULL(CM.LOOKUP_VALUE_DESC,C.lookup_value_desc) As REIN_EXTERIOR_CONSTRUCTION,ISNULL(lm.LOOKUP_VALUE_DESC, L.lookup_value_desc) As REIN_DESCRIPTION,REIN_REPORT_CODE,REIN_NISS, ISNULL(T.IS_ACTIVE,'Y') AS IS_ACTIVE";
				objWebGrid.FromClause			= "MNT_REIN_CONSTRUCTION_TRANSLATION T inner join mnt_lookup_values L   on  T.REIN_DESCRIPTION = L.LOOKUP_VALUE_CODE and L.LOOKUP_ID  =1320 inner join mnt_lookup_values C on T.REIN_EXTERIOR_CONSTRUCTION= C.LOOKUP_VALUE_CODE and C.LOOKUP_ID  =1002 left outer join MNT_LOOKUP_VALUES_MULTILINGUAL LM on LM.LOOKUP_UNIQUE_ID in (select LOOKUP_UNIQUE_ID  from MNT_LOOKUP_VALUES where LOOKUP_ID=1320 and T.REIN_DESCRIPTION =LOOKUP_VALUE_CODE) and LM.LANG_ID="+GetLanguageID()+" left outer join MNT_LOOKUP_VALUES_MULTILINGUAL CM on CM.LOOKUP_UNIQUE_ID in (select LOOKUP_UNIQUE_ID from MNT_LOOKUP_VALUES where LOOKUP_ID=1002 and T.REIN_EXTERIOR_CONSTRUCTION= LOOKUP_VALUE_CODE) and CM.LANG_ID="+GetLanguageID();
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Exterior Construction^Reinsurance Description^Reinsurance Code^NISS Code";
				//objWebGrid.SearchColumnNames	= "REIN_EXTERIOR_CONSTRUCTION^REIN_DESCRIPTION^REIN_REPORT_CODE^REIN_NISS";
				objWebGrid.SearchColumnNames	= "C.lookup_value_desc^L.lookup_value_desc^REIN_REPORT_CODE^REIN_NISS";
				objWebGrid.SearchColumnType		= "T^T^T^T";
				objWebGrid.OrderByClause		= "REIN_CONSTRUCTION_CODE_ID asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				objWebGrid.DisplayColumnNames	= "REIN_EXTERIOR_CONSTRUCTION^REIN_DESCRIPTION^REIN_REPORT_CODE^REIN_NISS";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Exterior Construction^Reinsurance Description^Reinsurance Code^NISS Code";
				objWebGrid.DisplayTextLength	= "100^200^100^100";
				objWebGrid.DisplayColumnPercent = "20^40^20^20";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "REIN_CONSTRUCTION_CODE_ID";

				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6";
                objWebGrid.SearchMessage =objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Construction Translation";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="T.IS_ACTIVE";
				objWebGrid.QueryStringColumns = "REIN_CONSTRUCTION_CODE_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
			
				
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "ConstructionTranslation.aspx?";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 150;
				# endregion
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion 
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







