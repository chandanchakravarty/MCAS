/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	6/28/2005 10:27:46 AM
<End Date				: -	
<Description			: - 	Code behind for Vendor invoices grid.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Code behind for Vendor invoices.
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
	/// VendorInvoicesIndex grid.
	/// </summary>
	public class VendorInvoicesIndex : Cms.Account.AccountBase
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJournalInfoXML;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="190";
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
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				/*	'UNFORMATTED_' columns have been added intentionally to let
				 *  the grid search on unformatted amount.  *  */

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "  *  ";
				objWebGrid.FromClause = " ( SELECT convert(varchar,vinv.CREATED_DATETIME,101) as CREATED_DATETIME1,vinv.CREATED_DATETIME ORDERED_CREATED_DATETIME,vinv.INVOICE_ID,vinv.VENDOR_ID,vinv.INVOICE_NUM,vinv.REF_PO_NUM,convert(varchar,vinv.TRANSACTION_DATE,101) as TRANSACTION_DATE,vinv.TRANSACTION_DATE TRANSACTIONDATE,convert(varchar,vinv.DUE_DATE,101) as DUE_DATE,vinv.DUE_DATE DUEDATE, convert(varchar(30),convert(money,isnull( vinv.INVOICE_AMOUNT,0)),1) INVOICE_AMOUNT,  ven.COMPANY_NAME AS VendorName,  "
										+ " dbo.MNT_USER_LIST.USER_ID,  isnull(dbo.MNT_USER_LIST.USER_FNAME + ' ' + dbo.MNT_USER_LIST.USER_LNAME,'') AS APPROVED_BY,  isnull((SELECT convert(varchar(30),convert(money,isnull(SUM(DISTRIBUTION_AMOUNT),0)),1) FROM ACT_DISTRIBUTION_DETAILS with(nolock) WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = vinv.INVOICE_ID),0) AS AppliedAmount, convert(varchar(30), convert(money,  vinv.INVOICE_AMOUNT - isnull((SELECT SUM(DISTRIBUTION_AMOUNT)      FROM ACT_DISTRIBUTION_DETAILS  with(nolock)    WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = vinv.INVOICE_ID),0)),1) AS RemainingAmount, convert(varchar(30),INVOICE_AMOUNT,1)UNFORMATTED_INVOICE_AMOUNT, "
										+ " CASE (vinv.INVOICE_AMOUNT-ISNULL((SELECT SUM(DISTRIBUTION_AMOUNT)      FROM ACT_DISTRIBUTION_DETAILS with(nolock)     WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = vinv.INVOICE_ID),0)) WHEN 0.00 THEN 'Fully Distributed' else 'Not Distributed' end as DistributionStatus, "
										+ " (SELECT convert(varchar(30),SUM(DISTRIBUTION_AMOUNT),1) FROM ACT_DISTRIBUTION_DETAILS with(nolock) WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = vinv.INVOICE_ID) AS UNFORMATTED_AppliedAmount,convert(varchar(30),vinv.INVOICE_AMOUNT - isnull((SELECT SUM(DISTRIBUTION_AMOUNT) FROM ACT_DISTRIBUTION_DETAILS with(nolock)     WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = vinv.INVOICE_ID),0),1) AS UNFORMATTED_RemainingAmount " 
										+ " ,convert(money,isnull( vinv.INVOICE_AMOUNT,0)) INVOICE_AMOUNT_1 "
										+ " ,isnull((SELECT convert(money,isnull(SUM(DISTRIBUTION_AMOUNT),0)) FROM ACT_DISTRIBUTION_DETAILS with(nolock) WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = vinv.INVOICE_ID),0) AS AppliedAmount_1 "
										+ " ,convert(money,  vinv.INVOICE_AMOUNT - isnull((SELECT SUM(DISTRIBUTION_AMOUNT) FROM ACT_DISTRIBUTION_DETAILS  with(nolock)    WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = vinv.INVOICE_ID),0)) AS RemainingAmount_1 "
										+ " from dbo.ACT_VENDOR_INVOICES vinv with(nolock)   INNER JOIN dbo.MNT_VENDOR_LIST ven with(nolock) ON vinv.VENDOR_ID = ven.VENDOR_ID   LEFT OUTER JOIN dbo.MNT_USER_LIST with(nolock) ON vinv.APPROVED_BY = dbo.MNT_USER_LIST.USER_ID  where isnull(IS_COMMITTED,'N')='N') Test ";
				objWebGrid.SearchColumnHeadings = "Date Created^Vendor Name^Invoice #^Transaction Date^Due Date^Invoice Amount^Amount Applied^Remaining Amount^Distribution Status";
				objWebGrid.SearchColumnNames = "ORDERED_CREATED_DATETIME^VendorName^INVOICE_NUM^TRANSACTIONDATE^DUEDATE^CONVERT(VARCHAR,CONVERT(MONEY,UNFORMATTED_INVOICE_AMOUNT),1)^CONVERT(VARCHAR,CONVERT(MONEY,UNFORMATTED_AppliedAmount),1)^CONVERT(VARCHAR,CONVERT(MONEY,UNFORMATTED_RemainingAmount),1)^DistributionStatus";
				objWebGrid.SearchColumnType = "D^T^T^D^D^T^T^T^T";
				objWebGrid.OrderByClause = "ORDERED_CREATED_DATETIME DESC";				
				objWebGrid.DisplayColumnNumbers = "1^9^4^11^7^8^12^13^14";
				objWebGrid.DisplayColumnNames = "CREATED_DATETIME1^VendorName^INVOICE_NUM^TRANSACTION_DATE^DUE_DATE^INVOICE_AMOUNT^AppliedAmount^RemainingAmount^DistributionStatus";
				objWebGrid.DisplayColumnHeadings = "Date Created^Vendor Name^Invoice #^Transaction Date^Due Date^Invoice Amount^Amount Applied^Remaining Amount^Distribution Status";
				objWebGrid.DisplayTextLength = "10^10^10^10^10^10^10^10^10^10";
				objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10^10^10^10^10";
				objWebGrid.PrimaryColumns = "2";
				objWebGrid.PrimaryColumnsName = "INVOICE_ID";
				//Modified by Asfa(18_june-2008) - iTrack #3906(Note: 1)
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "B^B^B^B^B^N^N^N^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Vendor Invoices";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "INVOICE_ID";
				objWebGrid.CellHorizontalAlign = "5^6^7";
				
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
