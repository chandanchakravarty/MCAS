/******************************************************************************************
	<Author					: - >Priya Arora
	<Start Date				: -	> Jan 05, 2006
	<End Date				: - >Jan 05, 2006
	<Description			: - >This file is being used for locading grid control to show reinsurer records
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
using System.Resources;
using System.Reflection;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for ReinsurerIndex.
	/// </summary>
	public class ReinsurerIndex : Cms.CmsWeb.cmsbase  
	{
 
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        ResourceManager objResourceMgr = null;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		public string  strSystemID="";
		private void Page_Load(object sender, System.EventArgs e)
		{
				  base.ScreenId="263";
                  objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ReinsurerIndex", Assembly.GetExecutingAssembly());
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
			try
			{
				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
//				objWebGrid.SelectClause = "REIN_COMAPANY_ID,REIN_COMAPANY_CODE,REIN_COMAPANY_NAME,IsNull(REIN_COMAPANY_ADD1,'')+' '+IsNull(REIN_COMAPANY_ADD2,'')+' '+IsNull(M_REIN_COMPANY_ADD_1,'')+IsNull(M_RREIN_COMPANY_ADD_2,'') As Address,ISNULL(REIN_COMAPANY_PHONE,'')+' '+ISNULL(REIN_COMAPANY_EXT,'')+' '+ISNULL(M_REIN_COMPANY_PHONE,'')+' '+ISNULL(M_REIN_COMPANY_EXT,'') Phone,REIN_COMAPANY_EXT,ISNULL(REIN_COMAPANY_CITY,'') +' '+ISNULL(M_REIN_COMPANY_CITY,'') As City,B.STATE_NAME REIN_COMAPANY_STATE,REIN_COMAPANY_FAX,convert(varchar,TERMINATION_DATE,101) TERMINATION_DATE,C.LOOKUP_VALUE_DESC AS REIN_COMPANY_TYPE,REIN_COMAPANY_CITY,REIN_COMAPANY_PHONE,REIN_COMAPANY_ADD1,ISNULL(A.IS_ACTIVE,'Y') AS IS_ACTIVE";
                objWebGrid.SelectClause = "REIN_COMAPANY_ID,REIN_COMAPANY_CODE,REIN_COMAPANY_NAME,IsNull(REIN_COMAPANY_ADD1,'')+' '+IsNull(REIN_COMAPANY_ADD2,'')+' '+IsNull(M_REIN_COMPANY_ADD_1,'')+IsNull(M_RREIN_COMPANY_ADD_2,'') As Address,ISNULL(REIN_COMAPANY_PHONE,'')+' '+ISNULL(REIN_COMAPANY_EXT,'')+' '+ISNULL(M_REIN_COMPANY_PHONE,'')+' '+ISNULL(M_REIN_COMPANY_EXT,'') Phone,REIN_COMAPANY_EXT,ISNULL(REIN_COMAPANY_CITY,'') +' '+ISNULL(M_REIN_COMPANY_CITY,'') As City,REIN_COMAPANY_STATE,REIN_COMAPANY_FAX,ISNULL(Convert(varchar,TERMINATION_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'') AS TERMINATION_DATE,ISNULL(CM.LOOKUP_VALUE_DESC,C.LOOKUP_VALUE_DESC) AS REIN_COMPANY_TYPE,REIN_COMAPANY_CITY,REIN_COMAPANY_PHONE,REIN_COMAPANY_ADD1,ISNULL(A.IS_ACTIVE,'Y') AS IS_ACTIVE,SUSEP_NUM";
                                                      
//				objWebGrid.FromClause = "MNT_REIN_COMAPANY_LIST A left outer join MNT_COUNTRY_STATE_LIST B ON A.REIN_COMAPANY_STATE=B.STATE_ID left outer join MNT_LOOKUP_VALUES C ON A.REIN_COMPANY_TYPE=C.lookup_value_code and C.lookup_id=1315";
                objWebGrid.FromClause = "MNT_REIN_COMAPANY_LIST A left outer join MNT_LOOKUP_VALUES C ON A.REIN_COMPANY_TYPE=C.LOOKUP_UNIQUE_ID and C.lookup_id=1315 left outer join MNT_LOOKUP_VALUES_MULTILINGUAL  CM ON A.REIN_COMPANY_TYPE=CM.LOOKUP_UNIQUE_ID and C.lookup_id=1315 and CM.LANG_ID="+GetLanguageID();
				//objWebGrid.WhereClause = "";

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Carrier Name^Carrier Code^Carrier Type^Address^City^State^Phone^Fax^Termination Date^SUSEP Number";
                objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames");//"REIN_COMAPANY_NAME^REIN_COMAPANY_CODE^C.LOOKUP_VALUE_DESC^IsNull(REIN_COMAPANY_ADD1,'') ! ' ' ! +IsNull(REIN_COMAPANY_ADD2,'')! ' ' ! IsNull(M_REIN_COMPANY_ADD_1,'')! ' ' ! IsNull(M_RREIN_COMPANY_ADD_2,'') ^ISNULL(REIN_COMAPANY_CITY,'') ! ' ' ! ISNULL(M_REIN_COMPANY_CITY,'')^REIN_COMAPANY_STATE^ISNULL(REIN_COMAPANY_PHONE,'') ! ' ' ! ISNULL(REIN_COMAPANY_EXT,'') ! ' ' ! ISNULL(M_REIN_COMPANY_PHONE,'') ! ' ' ! ISNULL(M_REIN_COMPANY_EXT,'')^REIN_COMAPANY_FAX^TERMINATION_DATE^SUSEP_NUM";
				//objWebGrid.SearchColumnType = "T^T^T^T^T^T^T^T^D^T";
                objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType");

				objWebGrid.OrderByClause = "REIN_COMAPANY_NAME asc";

                objWebGrid.DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers"); //"3^2^11^4^7^8^5^6^10";

				//objWebGrid.DisplayColumnNumbers = "3^2^11^4^7^8^5^6^9";
                //objWebGrid.DisplayColumnNames = "REIN_COMAPANY_NAME^REIN_COMAPANY_CODE^REIN_COMPANY_TYPE^REIN_COMAPANY_ADD1^REIN_COMAPANY_CITY^REIN_COMAPANY_STATE^REIN_COMAPANY_PHONE^REIN_COMAPANY_EXT^REIN_COMAPANY_FAX^TERMINATION_DATE^SUSEP_NUM";
                objWebGrid.DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames");
                //objWebGrid.DisplayColumnNames = "REIN_COMAPANY_NAME^REIN_COMAPANY_CODE^REIN_COMPANY_TYPE^Address^REIN_COMAPANY_CITY^REIN_COMAPANY_STATE^REIN_COMAPANY_PHONE^REIN_COMAPANY_EXT^REIN_COMAPANY_FAX";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Carrier Name^Carrier Code^Carrier Type^Address^City^State^Phone 1^Phone 2^Fax^Termination Date^SUSEP Number";
                objWebGrid.DisplayTextLength = objResourceMgr.GetString("DisplayTextLength");// "150^100^100^100^100^100^100^100^100^100^50";

                objWebGrid.DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent"); //"10^10^10^9^9^9^9^9^9^9^7";

				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "REIN_COMAPANY_ID";

                objWebGrid.ColumnTypes = objResourceMgr.GetString("ColumnTypes"); //"B^B^B^B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = objResourceMgr.GetString("FetchColumns");//"1^2^3^4^5^6^7^8^9^10^11^12^13";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Carrier Information" ;
				objWebGrid.SelectClass = colors ;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="A.IS_ACTIVE";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "REIN_COMAPANY_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);


				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}

            TabCtl.TabURLs = "AddReinsurer.aspx?REIN_COMAPANY_ID&";
            TabCtl.TabLength = 150;
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

		}
	
		
		#endregion



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
