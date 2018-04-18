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

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for CoverageRangeIndex.
	/// </summary>
	public class CoverageRangeIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCov_Id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden3;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="198_0_1";
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

			int CovId=int.Parse(Request.QueryString["COVID"].ToString());
				
			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{		
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "t1.LIMIT_DEDUC_ID LimitDedId,t1.LIMIT_DEDUC_TYPE LimitDedType,t2.COV_CODE CovCode,t2.COV_DES CovDesc,t2.COV_ID";
				objWebGrid.FromClause = "MNT_COVERAGE_RANGES t1 left outer join MNT_COVERAGE t2 on t1.COV_ID=t2.COV_ID";					
				objWebGrid.WhereClause = "t2.COV_ID=" + CovId ;
				objWebGrid.SearchColumnHeadings = "Deductible Type^Coverage Code^Coverage Description";
				objWebGrid.SearchColumnNames = "t1.LIMIT_DEDUC_TYPE^t2.COV_CODE^t2.COV_DES";
				objWebGrid.SearchColumnType = "T^T^T";
				objWebGrid.OrderByClause = "LimitDedType asc";
				objWebGrid.DisplayColumnNumbers = "1^2^3";
				objWebGrid.DisplayColumnNames = "LimitDedType^CovCode^CovDesc";
				objWebGrid.DisplayColumnHeadings = "Deductible Type^Coverage Code^Coverage Description";
				objWebGrid.DisplayTextLength = "30^30^50";
				objWebGrid.DisplayColumnPercent = "20^20^40";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "LIMIT_DEDUC_ID";
				objWebGrid.ColumnTypes = "B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Deductible Limit" ;
				objWebGrid.QueryStringColumns="COV_ID";
				objWebGrid.RequireQuery="Y";
				

				objWebGrid.SelectClass = colors;
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddCoverageRange.aspx?COVID=" + CovId + "&";

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
