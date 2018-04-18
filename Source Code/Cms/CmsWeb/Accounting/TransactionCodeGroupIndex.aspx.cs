/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	4/28/2005 9:06:31 PM
<End Date				: -	
<Description			: - 	Code behind for Transaction Codes Group Index.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Code behind for Transaction codes group index.
	/// </summary>
	public class TransactionCodeGroupIndex :  Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		private void Page_Load(object sender, System.EventArgs e)
		{
			 base.ScreenId="186";
			//	TabCtl.TabURLs = "AddGlAccountInformation.aspx?GL_ID="+Session["GL_ID"]+"&";
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
				
				//ACT_TRAN_CODE_GROUP.TRAN_GROUP_ID,1
				//ACT_TRAN_CODE_GROUP.POLICY_TYPE, 2
				//ACT_TRAN_CODE_GROUP.PROCESS_TRAN_TYPE,3
				//MNT_COUNTRY_STATE_LIST.STATE_NAME AS STATE_ID, ";4
				//MNT_SUB_LOB_MASTER.SUB_LOB_DESC AS SUB_LOB_ID,5
				//MNT_LOOKUP_TABLES.LOOKUP_DESC AS CLASS_RISK, ";6
				//MNT_LOB_MASTER.LOB_DESC AS LOB_ID ";7

				//Setting web grid control properties
				objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
				
				objWebGrid.SelectClause ="  ACT_TRAN_CODE_GROUP.TRAN_GROUP_ID, ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME, 'All') AS STATE_NAME, ACT_TRAN_CODE_GROUP.STATE_ID as STATE_ID1, ";
				objWebGrid.SelectClause +=" ISNULL(MNT_LOB_MASTER.LOB_DESC, 'All') AS LOB_DESC,  ";
				objWebGrid.SelectClause +=" ISNULL(MNT_SUB_LOB_MASTER.SUB_LOB_DESC, 'All') AS SUB_LOB_DESC,  ";
				objWebGrid.SelectClause +=" ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC, 'All') AS CLASS_RISK_DESC,  ";
				objWebGrid.SelectClause +=" isnull(nullif(case ACT_TRAN_CODE_GROUP.NEW_BUSINESS when 'Y' then 'New Business/' else '' end + ";
				objWebGrid.SelectClause +=" case ACT_TRAN_CODE_GROUP.CHANGE_IN_NEW_BUSINESS when 'Y' then 'Change in New Business /' else ''  end + ";
				objWebGrid.SelectClause +=" case ACT_TRAN_CODE_GROUP.RENEWAL when 'Y' then 'Renewal/'  else '' end + ";
				objWebGrid.SelectClause +=" case ACT_TRAN_CODE_GROUP.CHANGE_IN_RENEWAL when 'Y' then 'Change in Renewal/'  else '' end + ";
				objWebGrid.SelectClause +=" case ACT_TRAN_CODE_GROUP.REINSTATE_SAME_TERM when 'Y' then 'Reinstate  - Same Term/' else ''  end + ";
				objWebGrid.SelectClause +=" case ACT_TRAN_CODE_GROUP.REINSTATE_NEW_TERM when 'Y' then 'Reinstate – New Term/'  else '' end + ";
				objWebGrid.SelectClause +=" case ACT_TRAN_CODE_GROUP.CANCELLATION when 'Y' then 'Cancellation'  else '' end,''),'-') as PROCESS_TRAN_TYPE ";
				objWebGrid.SelectClause +=" ,case ACT_TRAN_CODE_GROUP.POLICY_TYPE when 'P' then 'Package' else 'Mono' end as POLICY_TYPE,MNT_LOB_MASTER.LOB_ID ";
				
				objWebGrid.FromClause = "  MNT_LOB_MASTER INNER JOIN ";
				objWebGrid.FromClause += " MNT_SUB_LOB_MASTER ON MNT_LOB_MASTER.LOB_ID = MNT_SUB_LOB_MASTER.LOB_ID RIGHT OUTER JOIN ";
				objWebGrid.FromClause += " ACT_TRAN_CODE_GROUP LEFT OUTER JOIN ";
				objWebGrid.FromClause += " MNT_LOOKUP_VALUES ON ACT_TRAN_CODE_GROUP.CLASS_RISK = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID LEFT OUTER JOIN ";
				objWebGrid.FromClause += " MNT_COUNTRY_STATE_LIST ON ACT_TRAN_CODE_GROUP.STATE_ID = MNT_COUNTRY_STATE_LIST.STATE_ID ON  ";
				objWebGrid.FromClause += " MNT_LOB_MASTER.LOB_ID = ACT_TRAN_CODE_GROUP.LOB_ID AND  ";
				objWebGrid.FromClause += " MNT_SUB_LOB_MASTER.SUB_LOB_ID = ACT_TRAN_CODE_GROUP.SUB_LOB_ID ";


				objWebGrid.WhereClause = "";
				objWebGrid.SearchColumnHeadings = "State^Lob^Sub Lob";

				//objWebGrid.SearchColumnNames = "ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME, 'All')^ISNULL(MNT_LOB_MASTER.LOB_DESC, 'All')^ISNULL(MNT_SUB_LOB_MASTER.SUB_LOB_DESC, 'All')";
				objWebGrid.SearchColumnNames = "ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME, 'All')^MNT_LOB_MASTER.LOB_ID^ISNULL(MNT_SUB_LOB_MASTER.SUB_LOB_DESC, 'All')";
				objWebGrid.DropDownColumns="^LOB^";
				objWebGrid.SearchColumnType = "T^T^T";
				objWebGrid.OrderByClause = "STATE_NAME asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7";
				objWebGrid.DisplayColumnNames = "STATE_NAME^LOB_DESC^SUB_LOB_DESC^CLASS_RISK_DESC^PROCESS_TRAN_TYPE^POLICY_TYPE";
				objWebGrid.DisplayColumnHeadings = "State^LOB^Sub LOB^Class/Risk^Transaction Type^Policy Type";
				objWebGrid.DisplayTextLength = "100^100^100^100^100^100";
				objWebGrid.DisplayColumnPercent = "15^15^15^13^33^9";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "ACT_TRAN_CODE_GROUP.TRAN_GROUP_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				//specifying number of the rows to be shown
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Transaction Code Groups";
				objWebGrid.SelectClass = colors;
				objWebGrid.FilterLabel = "Show InActive";
				objWebGrid.FilterColumnName = "";
				objWebGrid.FilterValue = "";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "TRAN_GROUP_ID";
				objWebGrid.DefaultSearch = "Y";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
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