/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		06-01-2006
<End Date			: -	
<Description		: - 	Policy Endorsement Log Index Page
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;
using System.Resources;
using System.Reflection;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class PolicyEndorsementLogIndex : Cms.Policies.policiesbase
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        ResourceManager objResourceMgr = null;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			#region Setting screen id
				base.ScreenId	=	"224_10";// Changed Screen id to 224_9 fro 500 by Sibin on 21 Oct 08 to add it into Policy Details Permission List
                objResourceMgr = new ResourceManager("Cms.Policies.Aspx.PolicyEndorsementLogIndex", Assembly.GetExecutingAssembly());
			#endregion

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
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				string strCUSTOMER_ID = GetCustomerID();
				string strPolicyID = GetPolicyID();

				hidCustomerID.Value =strCUSTOMER_ID;
				hidPolicyID.Value = strPolicyID;
 
				string sSELECTCLAUSE="", sWHERECLAUSE="", sFROMCLAUSE="";
				
				//sSELECTCLAUSE=" CONVERT(VARCHAR(50),E.ENDORSEMENT_DATE,109) ENDORSEMENT_DATE, E.ENDORSEMENT_DESC, E.REMARKS,CASE D.ENDORSEMENT_STATUS WHEN 'COM' THEN 'Complete' WHEN 'CAN' THEN 'Rolled Back' ELSE 'Pending' END ENDORSEMENT_STATUS,ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') AS CREATED_BY,E.TRANS_ID TRANS_ID";
                sSELECTCLAUSE = " ENDORSEMENT_NO,DETAILS,CASE WHEN ISNULL(ENDORSEMENT_TYPE,0) = '0' THEN '' ELSE ENDORSEMENT_TYPE END AS ENDORSEMENT_TYPE,ENDORSEMENT_DESC,REMARKS,CREATED_BY,CREATED_DATETIME,TRANS_ID,ENDORSEMENT_DETAIL_ID,CUSTOMER_ID,POLICY_ID";
				//sFROMCLAUSE  = " POL_POLICY_ENDORSEMENTS_DETAILS E, MNT_USER_LIST U, POL_POLICY_ENDORSEMENTS D";
				sFROMCLAUSE  = "VIW_ENDORSEMENT_LOG_DETAILS";
				sWHERECLAUSE = " CUSTOMER_ID = " + strCUSTOMER_ID + " AND POLICY_ID = " + strPolicyID ;
				
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.WhereClause = sWHERECLAUSE;	
				//Modified by Swastika on 1st Mar'06 for Pol Iss # 41
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Type^Description^Remarks^Processed By";
//				objWebGrid.SearchColumnNames = "ENDORSEMENT_DESC^ENDORSEMENT_TYPE^REMARKS^ISNULL(USER_TITLE,'') ! ' ' ! ISNULL(USER_FNAME,'') ! ' ' ! ISNULL(USER_LNAME,'')";
                objWebGrid.SearchColumnNames = "ISNULL(ENDORSEMENT_TYPE,0)^ENDORSEMENT_DESC^REMARKS^CREATED_BY";
				objWebGrid.SearchColumnType = "T^T^T^T";
				objWebGrid.OrderByClause	="ENDORSEMENT_NO desc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5^6";
				objWebGrid.DisplayColumnNames = "DETAILS^ENDORSEMENT_TYPE^ENDORSEMENT_DESC^REMARKS^CREATED_BY";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Details^Endorsement Type^Description^Remarks^Processed By";
				objWebGrid.DisplayTextLength = "65^20^80^35^50";
				objWebGrid.DisplayColumnPercent = "25^10^25^16^17";
				objWebGrid.PrimaryColumns = "1^9";
				objWebGrid.PrimaryColumnsName = "ENDORSEMENT_NO^ENDORSEMENT_DETAIL_ID";
				objWebGrid.ColumnTypes = "LBL^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Endorsement Log" ;
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show Complete";				 
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "TRANS_ID";
				objWebGrid.RequireQuery="Y";
				objWebGrid.FetchColumns = "2";
//				objWebGrid.ColumnsLink              =   "PolicyEndorsementLogIndex.aspx?^";
				objWebGrid.Grouping                 = "Z";
				objWebGrid.GroupQueryColumns        = "DETAILS^DETAILS";		
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
                TabCtl.TabURLs = "/cms/cmsweb/maintenance/TransactionLogDetail.aspx?calledfrom=ENDORSEMENT&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId,"TabCtl");
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