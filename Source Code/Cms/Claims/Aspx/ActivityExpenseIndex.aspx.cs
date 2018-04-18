/******************************************************************************************
<Author					: - > Vijay Arora
<Start Date				: -	> 31-05-2006
<End Date				: - >
<Description			: - > This file is being used to show/search Claims Expense Activity.
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
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication; 



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display Parties
	/// </summary>
	public class ActivityExpenseIndex : Cms.Claims.ClaimBase
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
		protected string ClaimID, ActivityID = "";
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strClaimID,strLOB_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnGoBack;


		#region Local form variables
		private string strTemp="";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="502";
			SetClaimTop();
			btnGoBack.CmsButtonClass		=	CmsButtonType.Read;
			btnGoBack.PermissionString		=	gstrSecurityXML;
			
			if (Request["CLAIM_ID"] != null && Request["ACTIVITY_ID"] != null)
			{
				ClaimID = Request["CLAIM_ID"].ToString();
				ActivityID =  Request["ACTIVITY_ID"].ToString();
			}
			btnGoBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");

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
			if(Request["CLAIM_ID"] !=null && Request["ACTIVITY_ID"] != null)
				strTemp = Request["CLAIM_ID"].ToString() + "&ACTIVITY_ID=" + Request["ACTIVITY_ID"].ToString();

			
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{	
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE+= " * ";
				
				sFROMCLAUSE  = " ( SELECT CTD.DETAIL_TYPE_DESCRIPTION AS ACTION_ON_PAYMENT,SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PAYMENT_AMOUNT),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PAYMENT_AMOUNT),1),0)) AS PAYMENT_AMOUNT, EXPENSE_ID, CLAIM_ID, ACTIVITY_ID FROM CLM_ACTIVITY_EXPENSE E LEFT OUTER JOIN CLM_TYPE_DETAIL CTD ON E.ACTION_ON_PAYMENT = CTD.DETAIL_TYPE_ID WHERE E.CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " AND  E.ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " ) Test ";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.SearchColumnHeadings = "Transaction Code^Amount";
				objWebGrid.SearchColumnNames = "ACTION_ON_PAYMENT^PAYMENT_AMOUNT";
				objWebGrid.SearchColumnType = "T^T";
				objWebGrid.DisplayColumnHeadings = "Transaction Code^Amount";
				objWebGrid.DisplayColumnNumbers = "1^2";
				objWebGrid.DisplayColumnNames = "ACTION_ON_PAYMENT^PAYMENT_AMOUNT";
				objWebGrid.DisplayTextLength = "50^50";
				objWebGrid.DisplayColumnPercent = "50^50";
				objWebGrid.PrimaryColumns = "6";
				objWebGrid.PrimaryColumnsName = "EXPENSE_ID";
				objWebGrid.ColumnTypes = "B^B";
				objWebGrid.HeaderString ="Expense Activity";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
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
				//objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.QueryStringColumns = "EXPENSE_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddActivityExpense.aspx?CLAIM_ID=" +  strTemp +"&"; 
				TabCtl.TabTitles ="Expense Activity";
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
