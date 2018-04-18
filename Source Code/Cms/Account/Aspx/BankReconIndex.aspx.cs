	/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -   6/28/2005 10:27:46 AM
<End Date				: -	
<Description			: -   Code behind for bank reconciliation grid.
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
using Cms.CmsWeb.WebControls;

namespace Cms.Account.Aspx
{
	/// <summary>
	///  Code behind for bank reconciliation grid.
	/// </summary>
	public class BankReconIndex : Cms.Account.AccountBase	
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label lblEntryNo;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblProof;


		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="205";
			if(!IsPostBack)
			{
			
			}


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
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;
			//BR.AC_RECONCILIATION_ID, 1
			//BR.ACCOUNT_ID, 2
			//BR.START_STATEMENT_DATE 3
			//BR.STATEMENT_DATE// 4
			//BR.STARTING_BALANCE 5
			//BR.ENDING_BALANCE 6
			//,BR.BANK_CHARGES_CREDITS 7
			//BR.IS_ACTIVE 8
			//BR.LAST_RECONCILED 9
			//ACC_DESCRIPTION 10
			//proof 11
			//AccountBalance 12

			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = " BR.AC_RECONCILIATION_ID,BR.ACCOUNT_ID,convert(varchar,BR.START_STATEMENT_DATE,101) as START_STATEMENT_DATE,convert(varchar,BR.STATEMENT_DATE,101) as STATEMENT_DATE,CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(BR.STARTING_BALANCE,0)),1) AS STARTING_BALANCE ,CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(BR.ENDING_BALANCE,0)),1) AS ENDING_BALANCE,CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(BR.BANK_CHARGES_CREDITS,0)),1) AS BANK_CHARGES_CREDITS,BR.IS_ACTIVE,";
				objWebGrid.SelectClause +=" convert(varchar,BR.LAST_RECONCILED,101) as LAST_RECONCILED,";
				objWebGrid.SelectClause +=" convert(varchar,acc.ACC_DISP_NUMBER)+': '+acc.ACC_DESCRIPTION as ACC_DESCRIPTION ,";
				objWebGrid.SelectClause +=" CONVERT(VARCHAR(30),CONVERT(MONEY,(STARTING_BALANCE+(select isnull(sum(TRANSACTION_AMOUNT),0) from ACT_ACCOUNTS_POSTING_DETAILS with(nolock) where IDENTITY_ROW_ID in (select IDENTITY_ROW_ID from ACT_BANK_RECONCILIATION_ITEMS_DETAILS with(nolock) where AC_RECONCILIATION_ID=BR.AC_RECONCILIATION_ID))-ENDING_BALANCE - ISNULL(BANK_CHARGES_CREDITS,0))),1) as Proof,";
				objWebGrid.SelectClause +="(select CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ACC_CURRENT_BALANCE,0)),1)  from ACT_GL_ACCOUNTS with(nolock) where BR.ACCOUNT_ID = ACCOUNT_ID) as AccountBalance";
				objWebGrid.FromClause = "  ACT_BANK_RECONCILIATION BR with(nolock) inner join ACT_GL_ACCOUNTS acc with(nolock) on BR.ACCOUNT_ID = acc.ACCOUNT_ID";
				objWebGrid.WhereClause = " isnull(IS_COMMITED,'N')<>'Y' and isnull(ACC_CASH_ACCOUNT,'N')='Y'";
				objWebGrid.SearchColumnHeadings = "Account Number^Statement Date";
				objWebGrid.SearchColumnNames = "acc.ACC_NUMBER^STATEMENT_DATE";
				objWebGrid.SearchColumnType = "T^D";			
				objWebGrid.OrderByClause = "ACC_DESCRIPTION ASC";				
				objWebGrid.DisplayColumnNumbers = "10^4^5^6^7^9^11^12";
				objWebGrid.DisplayColumnNames = "ACC_DESCRIPTION^STATEMENT_DATE^STARTING_BALANCE^ENDING_BALANCE^BANK_CHARGES_CREDITS^LAST_RECONCILED^Proof^AccountBalance";
				objWebGrid.DisplayColumnHeadings = "Account^Statement Date^Starting Balance^Ending Balance^Bank Charges & Credits^Last Reconciled^Proof^Account Balance";
				objWebGrid.DisplayTextLength = "16^12^12^12^12^12^12^12";
				objWebGrid.DisplayColumnPercent = "22^12^12^12^10^12^10^10";
				objWebGrid.PrimaryColumns = "8";
				objWebGrid.PrimaryColumnsName = "AC_RECONCILIATION_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^8";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Bank Reconciliation";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "AC_RECONCILIATION_ID";
				objWebGrid.CellHorizontalAlign="2^3^4^6^7";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);

				
			}
			catch
			{
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
