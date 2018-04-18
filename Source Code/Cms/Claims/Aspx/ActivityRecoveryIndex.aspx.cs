/******************************************************************************************
<Author					: - >Vijay Arora
<Start Date				: -	> 25-05-2006
<End Date				: - >
<Description			: - > This file is being used to show/search Claims Recovery Activity.
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



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display Parties
	/// </summary>
	public class ActivityRecoveryIndex : Cms.Claims.ClaimBase
	{
		
		#region "web form designer controls declaration"
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected System.Web.UI.WebControls.Label lblError;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_RECORD;
		#endregion
		protected System.Web.UI.WebControls.Label capMessage;
protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;	
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strClaimID,strLOB_ID;

		#region Local form variables
		private string gStrClaimId="";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="355";
			SetClaimTop();
			
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
			if(Session["claimID"]!=null)
				gStrClaimId = Session["claimID"].ToString();
			
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{	
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE+= " * ";
				sFROMCLAUSE  = " ( SELECT R.CLAIM_ID,R.RECOVERY_ID,R.ACTIVITY_ID,TD.DETAIL_TYPE_DESCRIPTION AS RECOVERY_TYPE,CONVERT(varchar(10),R.RECEIVED_DATE,101) AS RECEIVED_DATE,R.RECEIVED_FROM,substring(convert(varchar(30),convert(money,R.AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,R.AMOUNT),1),0)) AS AMOUNT,R.IS_ACTIVE, D.DETAIL_TYPE_DESCRIPTION AS TRANSACTION_CODE FROM CLM_ACTIVITY_RECOVERY R LEFT JOIN CLM_TYPE_DETAIL TD ON TD.DETAIL_TYPE_ID = R.RECOVERY_TYPE  LEFT JOIN CLM_TYPE_DETAIL D ON D.DETAIL_TYPE_ID = R.TRANSACTION_CODE WHERE CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " AND ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " ) Test ";
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.SearchColumnHeadings = "Recovery Type^Amount^Received Date^Received From";
				objWebGrid.SearchColumnNames = "RECOVERY_TYPE^AMOUNT^RECEIVED_DATE^RECEIVED_FROM";
				objWebGrid.SearchColumnType = "T^T^T^T";
				objWebGrid.DisplayColumnHeadings = "Recovery Type^Amount^Received Date^Received From";
				objWebGrid.DisplayColumnNumbers = "4^5^6^7";
				objWebGrid.DisplayColumnNames = "RECOVERY_TYPE^AMOUNT^RECEIVED_DATE^RECEIVED_FROM";
				objWebGrid.DisplayTextLength = "25^25^25^25";
				objWebGrid.DisplayColumnPercent = "25^25^25^25";
				objWebGrid.PrimaryColumns = "2";
				objWebGrid.PrimaryColumnsName = "RECOVERY_ID";
				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.HeaderString ="Recovery Details";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;				
				objWebGrid.FilterLabel = "Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "RECOVERY_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddActivityRecovery.aspx?CLAIM_ID=" + Request["CLAIM_ID"]+"&ACTIVITY_ID=" + Request["ACTIVITY_ID"] + "&"; 
				TabCtl.TabTitles ="Recovery Details";
				TabCtl.TabLength =150;
	
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion

		}

		private void SetClaimTop()
		{
			
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
