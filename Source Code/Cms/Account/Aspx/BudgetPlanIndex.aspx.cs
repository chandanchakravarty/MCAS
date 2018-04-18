/******************************************************************************************
<Author					: -  Manoj Rathore
<Start Date				: -	6/22/2007 10:27:46 AM
<End Date				: -	
<Description			: - 	Code behind for Budget Plan.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Code behind for Budget Plan.
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
	public class BudgetPlanIndex : Cms.Account.AccountBase
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

			try
			{
				/*	'UNFORMATTED_' columns have been added intentionally to let
				 *  the grid search on unformatted amount.  *  */

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "IDEN_ROW_ID,BP.BUDGET_CATEGORY_ID,GL.LEDGER_NAME +' ('+ CONVERT(VARCHAR,GL.FISCAL_BEGIN_DATE,101) + " ;
				objWebGrid.SelectClause = objWebGrid.SelectClause + " ' - '+ CONVERT(VARCHAR,GL.FISCAL_END_DATE,101)+')' AS LEDGER_NAME ,BP.GL_ID,BP.FISCAL_ID,JAN_BUDGET,FEB_BUDGET,MARCH_BUDGET,APRIL_BUDGET,MAY_BUDGET,JUNE_BUDGET,JULY_BUDGET,AUG_BUDGET,SEPT_BUDGET,OCT_BUDGET,NOV_BUDGET,DEC_BUDGET, +";
				objWebGrid.SelectClause = objWebGrid.SelectClause + " case when t1.acc_parent_id is null then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'') + ' / '   + isnull(acc.CATEGORY_DEPARTEMENT_NAME,'')   else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')  + ' / '   + isnull(acc.CATEGORY_DEPARTEMENT_NAME,'')   end as CATEGORY_DEPARTEMENT_NAME , CONVERT(VARCHAR,CONVERT(MONEY, ISNULL(JAN_BUDGET,0) + ISNULL(FEB_BUDGET,0) + ISNULL(MARCH_BUDGET,0) + ISNULL(APRIL_BUDGET,0) + ISNULL(MAY_BUDGET,0) + ISNULL(JUNE_BUDGET,0) +ISNULL(JULY_BUDGET,0) + ISNULL(AUG_BUDGET,0) + ISNULL(SEPT_BUDGET,0) + ISNULL(OCT_BUDGET,0) + ISNULL(NOV_BUDGET,0) + ISNULL(DEC_BUDGET,0)),1) As TOTAL_AMOUNT";
				//objWebGrid.FromClause = "ACT_BUDGET_PLAN BP with(nolock) inner join ACT_BUDGET_CATEGORY acc with(nolock) on BP.BUDGET_CATEGORY_ID = acc.CATEGEORY_ID inner join ACT_GENERAL_LEDGER GL with(nolock) on GL.GL_ID=BP.GL_ID and GL.FISCAL_ID=BP.FISCAL_ID  inner join ACT_GL_ACCOUNTS t1 with(nolock) on T1.BUDGET_CATEGORY_ID=acc.CATEGEORY_ID LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 with(nolock) ON t2.account_id = t1.acc_parent_id";
				objWebGrid.FromClause = " ACT_BUDGET_PLAN BP with(nolock) inner join ACT_GENERAL_LEDGER GL with(nolock) on GL.GL_ID=BP.GL_ID and GL.FISCAL_ID=BP.FISCAL_ID  inner join ACT_GL_ACCOUNTS t1 with(nolock) on T1.account_ID=bp.account_id inner join ACT_BUDGET_CATEGORY acc with(nolock) on acc.CATEGEORY_ID = T1.BUDGET_CATEGORY_ID LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 with(nolock) ON t2.account_id = t1.acc_parent_id ";
				objWebGrid.SearchColumnHeadings = "General Ledger^General Ledger Account/Category";
				//objWebGrid.SearchColumnNames = "GL.LEDGER_NAME  ! ' (' ! CONVERT(VARCHAR,GL.FISCAL_BEGIN_DATE,101) ! ' - ' ! CONVERT(VARCHAR,GL.FISCAL_END_DATE,101) ! ')'^ACC.CATEGORY_DEPARTEMENT_NAME";
				objWebGrid.SearchColumnNames = "GL.LEDGER_NAME  ! ' (' ! CONVERT(VARCHAR,GL.FISCAL_BEGIN_DATE,101) ! ' - ' ! CONVERT(VARCHAR,GL.FISCAL_END_DATE,101) ! ')'^case when t1.acc_parent_id is null then t1.ACC_DESCRIPTION ! ' : ' !  isnull(t1.ACC_DISP_NUMBER,'') ! ' / '   ! isnull(acc.CATEGORY_DEPARTEMENT_NAME,'')   else  isnull(t2.acc_description,'') ! ' - ' ! t1.ACC_DESCRIPTION  ! ' : ' ! isnull(t1.ACC_DISP_NUMBER,'')  ! ' / '   ! isnull(acc.CATEGORY_DEPARTEMENT_NAME,'')  end";
				//objWebGrid.SearchColumnNames = "BP.GL_ID^BUDGET_CATEGORY_ID";
				objWebGrid.SearchColumnType = "T^T";			
				objWebGrid.OrderByClause = "IDEN_ROW_ID DESC";				
				objWebGrid.DisplayColumnNumbers = "3^4^18";
				objWebGrid.DisplayColumnNames = "LEDGER_NAME^CATEGORY_DEPARTEMENT_NAME^TOTAL_AMOUNT";
				objWebGrid.DisplayColumnHeadings = "General Ledger^General Ledger Account/Category^Total Distributed Amount";
				objWebGrid.DisplayTextLength = "30^50^20";
				objWebGrid.DisplayColumnPercent = "30^50^20";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "IDEN_ROW_ID";
				objWebGrid.ColumnTypes = "B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Plan Budget";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "IDEN_ROW_ID";
				//objWebGrid.CellHorizontalAlign = "5^6^7";
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
