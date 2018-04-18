/******************************************************************************************
<Author					: - > Vijay Arora
<Start Date				: -	> 05-06-2006
<End Date				: - >
<Description			: - > This file is being used to show/search Claims Expense Breakdown.
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
using System.Reflection;
using System.Resources;


namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display Parties
	/// </summary>
	public class ReopenClaimIndex : Cms.Claims.ClaimBase
	{
		
		#region "web form designer controls declaration"
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected System.Web.UI.WebControls.Label lblError;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_RECORD;
        ResourceManager objResourceMgr = null;
		#endregion
		protected System.Web.UI.WebControls.Label capMessage;

		#region Local form variables
		private string strTemp="";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="306_10";
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.ReopenClaimIndex", Assembly.GetExecutingAssembly());
			SetClaimTop();
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
			if(Request["CLAIM_ID"] !=null )
				strTemp = Request["CLAIM_ID"].ToString() + "&";

			
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{	
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE+= " * ";
				
				sFROMCLAUSE  = " ( SELECT CONVERT(VARCHAR(10),REOPEN_DATE,101) AS REOPENED_ON, ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') AS REOPENED_BY, REASON, REOPEN_ID, R.IS_ACTIVE FROM CLM_REOPEN_CLAIM R LEFT JOIN MNT_USER_LIST U ON U.USER_ID = R.REOPEN_BY WHERE CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " ) Test ";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Reopened On^Reopened By^Reason";
				objWebGrid.SearchColumnNames = "REOPENED_ON^REOPENED_BY^REASON";
				objWebGrid.SearchColumnType = "T^T^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Reopened On^Reopened By^Reason";
				objWebGrid.DisplayColumnNumbers = "1^2^3";
				objWebGrid.DisplayColumnNames = "REOPENED_ON^REOPENED_BY^REASON";
				objWebGrid.DisplayTextLength = "25^25^50";
				objWebGrid.DisplayColumnPercent = "25^25^50";
				objWebGrid.PrimaryColumns = "4";
				objWebGrid.PrimaryColumnsName = "REOPEN_ID";
				objWebGrid.ColumnTypes = "B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Re-Open Claim";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//objWebGrid.ExtraButtons = "1^Add New^0^addRecord";															
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterValue = "Y";
				objWebGrid.QueryStringColumns = "REOPEN_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddReopenClaim.aspx?CLAIM_ID=" +  strTemp +"&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1966");//"Re-Open Claim";
				TabCtl.TabLength =150;
	
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion

		}

		private void SetClaimTop()
		{
			string strCustomerId,strPolicyID,strPolicyVersionID,strClaimID,strLOB_ID;
			strCustomerId = GetCustomerID();
			strPolicyID = GetPolicyID();
			strPolicyVersionID = GetPolicyVersionID();
			strClaimID = GetClaimID();
			strLOB_ID = GetLOBID();

			if(strCustomerId!=null && strCustomerId!="")
			{
				cltClaimTop.CustomerID = int.Parse(strCustomerId);
			}			

			if(strPolicyID!=null && strPolicyID!="")
			{
				cltClaimTop.PolicyID = int.Parse(strPolicyID);
			}

			if(strPolicyVersionID!=null && strPolicyVersionID!="")
			{
				cltClaimTop.PolicyVersionID = int.Parse(strPolicyVersionID);
			}
			if(strClaimID!=null && strClaimID!="")
			{
				cltClaimTop.ClaimID = int.Parse(strClaimID);
			}
			if(strLOB_ID!=null && strLOB_ID!="")
			{
				cltClaimTop.LobID = int.Parse(strLOB_ID);
			}
        
			cltClaimTop.ShowHeaderBand ="Claim";

			cltClaimTop.Visible = true;        
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
