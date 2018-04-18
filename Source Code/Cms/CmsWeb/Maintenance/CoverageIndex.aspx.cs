/******************************************************************************************
	<Author					:   Mohit Gupta
	<Start Date				:   28/06/2005
	<End Date				: - >
	<Description			: - >
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
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for CoverageIndex.
	/// </summary>
	public class CoverageIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			  //base.ScreenId="198";
                base.ScreenId = "492";
              objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.CoverageIndex", Assembly.GetExecutingAssembly());
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
				BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
				try
				{		
					//Setting web grid control properties
					objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
					
					//objWebGrid.SelectClause = "t1.COV_REF_CODE CovRefCode ,t1.COV_CODE CovCode,t1.COV_DES CovDesc,t1.LOB_ID LobId,t1.COV_ID,t2.LOB_DESC LobDesc,t1.IS_ACTIVE,t3.STATE_NAME stateName,t3.STATE_CODE,t3.STATE_ID,t1.TYPE";					
					//objWebGrid.FromClause = "MNT_COVERAGE t1 left outer join MNT_LOB_MASTER t2 on t1.LOB_ID=t2.LOB_ID left outer join MNT_COUNTRY_STATE_LIST t3 on t1.STATE_ID=t3.STATE_ID";					
                    objWebGrid.SelectClause = "CovRefCode ,CovCode,CovDesc,LobId,COV_ID,LobDesc,IS_ACTIVE,stateName,STATE_CODE,STATE_ID,TYPE,TYPE_DESC,SUB_LOB_DESC";
                    objWebGrid.FromClause = "(select t1.COV_REF_CODE CovRefCode ,t1.COV_CODE CovCode,isnull (t9.COV_DES,t1.COV_DES) CovDesc,t1.LOB_ID LobId,t1.COV_ID,isnull(t8.LOB_DESC,t2.LOB_DESC) LobDesc,t1.IS_ACTIVE,case t1.STATE_ID when 0 then case when " + GetLanguageID() + "=2 then 'Todos' else 'All' end else t3.STATE_NAME end as stateName,t3.STATE_CODE,t3.STATE_ID,t1.TYPE,";
                    objWebGrid.FromClause = objWebGrid.FromClause + " case t1.TYPE when '3' then 'Reinsurance Coverage' when '2' then 'Endorsement Coverage' else 'Coverage' end as TYPE_DESC,isnull(MLVS.SUB_LOB_DESC,t4.SUB_LOB_DESC)as SUB_LOB_DESC";
                    objWebGrid.FromClause = objWebGrid.FromClause + " from  MNT_COVERAGE t1 left outer join MNT_COVERAGE_MULTILINGUAL t9 on t1.COV_ID=t9.COV_ID and t9.LANG_ID =" + GetLanguageID() + " left outer join MNT_LOB_MASTER t2 on t1.LOB_ID=t2.LOB_ID left outer join MNT_LOB_MASTER_MULTILINGUAL t8 on t2.LOB_ID=t8.LOB_ID and t8.LANG_ID=" + GetLanguageID() + " left outer join MNT_COUNTRY_STATE_LIST t3 on t1.STATE_ID=t3.STATE_ID  left outer join MNT_SUB_LOB_MASTER t4 on t1.SUB_LOB_ID=t4.SUB_LOB_ID and  t4.LOB_ID = t1.LOB_ID  left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MLVS on t1.SUB_LOB_ID=MLVS.SUB_LOB_ID and  MLVS.LOB_ID = t1.LOB_ID and MLVS.LANG_ID=" + GetLanguageID() + " where ISNULL(t1.IS_ACTIVE,'Y') = 'Y'AND ISNULL(t2.IS_ACTIVE,'Y') = 'Y'";
                    objWebGrid.FromClause = objWebGrid.FromClause + " union	select t1.COV_REF_CODE CovRefCode ,t1.COV_CODE CovCode,t1.COV_DES CovDesc,t1.LOB_ID LobId,t1.COV_ID,isnull(t8.LOB_DESC,t2.LOB_DESC) LobDesc,t1.IS_ACTIVE,case t1.STATE_ID when 0 then case when " + GetLanguageID() + "=2 then 'Todos' else 'All' end else t3.STATE_NAME end as  stateName,t3.STATE_CODE,t3.STATE_ID,t1.TYPE,";
                    objWebGrid.FromClause = objWebGrid.FromClause + " case t1.TYPE when '3' then 'Reinsurance Coverage' when '2' then 'Endorsement Coverage' else 'Coverage' end as TYPE_DESC,isnull(MLVS.SUB_LOB_DESC,t4.SUB_LOB_DESC)as SUB_LOB_DESC";
                    objWebGrid.FromClause = objWebGrid.FromClause + " from  MNT_REINSURANCE_COVERAGE t1 left outer join MNT_LOB_MASTER t2 on t1.LOB_ID=t2.LOB_ID left outer join MNT_LOB_MASTER_MULTILINGUAL t8 on t2.LOB_ID=t8.LOB_ID and t8.LANG_ID=" + GetLanguageID() + " left outer join MNT_COUNTRY_STATE_LIST t3 on t1.STATE_ID=t3.STATE_ID left outer join MNT_SUB_LOB_MASTER t4 on t1.SUB_LOB_ID=t4.SUB_LOB_ID and  t4.LOB_ID = t1.LOB_ID left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MLVS on t1.SUB_LOB_ID=MLVS.SUB_LOB_ID and  MLVS.LOB_ID = t1.LOB_ID and MLVS.LANG_ID=" + GetLanguageID() + " WHERE ISNULL(t1.IS_ACTIVE,'Y')='Y' AND ISNULL(t2.IS_ACTIVE,'Y') = 'Y') tempt";
					//objWebGrid.SearchColumnHeadings = "Coverage Code^Coverage Ref Code^State^Description^LOB";
					objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Coverage Code^State^Description^LOB^Coverage Type";

					//objWebGrid.SearchColumnNames = "t1.COV_CODE^t1.COV_REF_CODE^t3.STATE_NAME^t1.COV_DES^t1.LOB_ID";
					//objWebGrid.SearchColumnNames = "t1.COV_CODE^t3.STATE_NAME^t1.COV_DES^t1.LOB_ID";
                    objWebGrid.SearchColumnNames = "CovCode^stateName^CovDesc^LobId^TYPE_DESC^SUB_LOB_DESC";

					//objWebGrid.DropDownColumns="^^^LOB^";
					objWebGrid.DropDownColumns="^^^LOB^^";
					objWebGrid.SearchColumnType = "T^T^T^L^T^T";
					objWebGrid.OrderByClause = "CovCode asc";
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "CovCode^stateName^CovDesc^LobDesc^SUB_LOB_DESC";
					objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Coverage Code^State^Description^LOB";
					objWebGrid.DisplayTextLength = "20^20^35^25^25";
					objWebGrid.DisplayColumnPercent = "10^10^20^20^35";
					objWebGrid.PrimaryColumns = "1";
					objWebGrid.PrimaryColumnsName = "COV_ID";
					objWebGrid.ColumnTypes = "B^B^B^B^B";
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3^4";
					objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
					//objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
					objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
					objWebGrid.PageSize = int.Parse (GetPageSize());
					objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Coverages" ;
					objWebGrid.QueryStringColumns="COV_ID^LobDesc^STATE_CODE^STATE_ID^LobId^TYPE";
					//specifying text to be shown for filter checkbox
					//objWebGrid.FilterLabel="Include Inactive";
					//specifying column to be used for filtering
					//objWebGrid.FilterColumnName="t1.IS_ACTIVE";
					//value of filtering record
					//objWebGrid.FilterValue="Y";
					objWebGrid.RequireQuery="Y";
					objWebGrid.DefaultSearch="Y";
					
					objWebGrid.SelectClass = colors;
					GridHolder.Controls.Add(objWebGrid);
					TabCtl.TabURLs = "AddCoverageDetails.aspx?";
                    TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl"); //sneha
				}
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				}
				#endregion			
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
