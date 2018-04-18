/******************************************************************************************
<Author				: -		Amar Singh
<Start Date			: -		20-04-2006
<End Date			: -	
<Description		: - 	Index Page for Claim Adjusters link.
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

namespace Cms.CmsWeb.Maintenance.Claims
{

	/// <summary>
	/// 
	/// </summary>
	public class ClaimAdjustersIndex : Cms.CmsWeb.cmsbase
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
		private string TypeID = "";
        ResourceManager objResourceMgr = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id and Type ID
			if(Request.QueryString["TYPE_ID"] != null && Request.QueryString["TYPE_ID"] != "")
			{
				TypeID	=	Request.QueryString["TYPE_ID"];
			}

			base.ScreenId	=	"298";
			#endregion
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.ClaimAdjustersIndex", Assembly.GetExecutingAssembly());
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
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{				
				string sSELECTCLAUSE="",  sFROMCLAUSE="";
                sSELECTCLAUSE += "A.ADJUSTER_CODE AS ADJUSTERCODE,ISNULL(ADJ_TYPE_M.LOOKUP_VALUE_DESC, ADJ_TYPE.LOOKUP_VALUE_DESC) ADJUST_TYPE_DESC,ADJUSTER_TYPE, CASE ADJUSTER_TYPE WHEN 11738 THEN SUB_ADJUSTER_LEGAL_NAME ELSE ADJUSTER_NAME END AS ADJUSTER_NAME, ISNULL(SUB_ADJUSTER_ADDRESS1,'') + ' ' + ISNULL(SUB_ADJUSTER_ADDRESS2,'') AS ADDRESS ,ADJUSTER_ID,ADJUSTER_TYPE,SUB_ADJUSTER,SUB_ADJUSTER_LEGAL_NAME,SUB_ADJUSTER_ADDRESS1,SUB_ADJUSTER_ADDRESS2,SUB_ADJUSTER_CITY,SUB_ADJUSTER_STATE,SUB_ADJUSTER_ZIP,SUB_ADJUSTER_PHONE,SUB_ADJUSTER_FAX,SUB_ADJUSTER_EMAIL,SUB_ADJUSTER_WEBSITE,SUB_ADJUSTER_NOTES,A.IS_ACTIVE,A.CREATED_BY,A.CREATED_DATETIME,A.MODIFIED_BY,A.LAST_UPDATED_DATETIME,SUB_ADJUSTER_COUNTRY,SUB_ADJUSTER_CONTACT_NAME,SA_ADDRESS1,SA_ADDRESS2,SA_CITY,SA_COUNTRY,SA_STATE,SA_ZIPCODE,SA_PHONE,SA_FAX,LOB_ID,B.* ";
				//Ambiguous column ADJUSTER_NAME removed, added alias ADJUSTERCODE,Charles,Itrack 6636, 26-Oct-09
                sFROMCLAUSE = " CLM_ADJUSTER A LEFT JOIN MNT_USER_LIST MUL ON MUL.USER_ID = A.USER_ID LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST B ON A.SUB_ADJUSTER_STATE = B.STATE_ID LEFT JOIN MNT_LOOKUP_VALUES ADJ_TYPE ON ADJ_TYPE.LOOKUP_UNIQUE_ID = A.ADJUSTER_TYPE LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL ADJ_TYPE_M ON ADJ_TYPE_M.LOOKUP_UNIQUE_ID = ADJ_TYPE.LOOKUP_UNIQUE_ID AND ADJ_TYPE_M.LANG_ID=" + ClsCommon.BL_LANG_ID;
							
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ;

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Adjuster Code^Adjuster Name^Adjuster Type";
				objWebGrid.SearchColumnNames = "isnull(A.ADJUSTER_CODE,'')^CASE ADJUSTER_TYPE WHEN 11738 THEN SUB_ADJUSTER_LEGAL_NAME ELSE ADJUSTER_NAME END^ADJ_TYPE.LOOKUP_VALUE_DESC";
				objWebGrid.SearchColumnType = "T^T^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Adjuster Code^Adjuster Name^Adjuster Type";
				objWebGrid.DisplayColumnNumbers = "6^8^1";
				objWebGrid.DisplayColumnNames = "ADJUSTERCODE^ADJUSTER_NAME^ADJUST_TYPE_DESC";//Changed from ADJUSTER_CODE,Charles,Itrack 6636, 26-Oct-09
				objWebGrid.DisplayTextLength = "10^90^90";
				objWebGrid.DisplayColumnPercent = "15^30^40";
				objWebGrid.PrimaryColumns = "4";
				objWebGrid.PrimaryColumnsName = "ADJUSTER_ID";
				objWebGrid.ColumnTypes = "B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Claim Adjusters" ;
				objWebGrid.OrderByClause	="SUB_ADJUSTER_LEGAL_NAME asc";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                //objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
                //objWebGrid.RequireQuery = "Y";
                //objWebGrid.DefaultSearch = "Y";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName = "a.IS_ACTIVE";
                objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.SystemColumnName = "a.IS_ACTIVE";
				objWebGrid.QueryStringColumns = "ADJUSTER_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				//TabCtl.TabURLs = "AddAdjusterDetails.aspx?ADJUSTER_ID=" + TypeID + "&,AddClaimsAdjusterAuthority.aspx?ADJUSTER_ID=" + TypeID + "&"; 
				TabCtl.TabURLs = "AddAdjusterDetails.aspx?" + "&";//,ClaimsAdjusterAuthorityIndex.aspx?" + "&"; 
				//TabCtl.TabTitles ="Claim Adjusters";//,Authority";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
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