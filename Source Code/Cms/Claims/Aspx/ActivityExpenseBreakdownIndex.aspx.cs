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
	public class ActivityExpenseBreakdownIndex : Cms.Claims.ClaimBase
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
        ResourceManager objResourceMgr = null;

		#region Local form variables
		private string strTemp="";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="530";
		
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.ActivityExpenseBreakDownIndex", Assembly.GetExecutingAssembly());
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
			if(Request["CLAIM_ID"] !=null && Request["ACTIVITY_ID"] != null && Request["EXPENSE_ID"] != null)
				strTemp = Request["CLAIM_ID"].ToString() + "&ACTIVITY_ID=" + Request["ACTIVITY_ID"].ToString() + "&EXPENSE_ID=" + Request["EXPENSE_ID"].ToString() + "&";

			
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{	
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE+= " * ";
				
				sFROMCLAUSE  = " ( SELECT D.DETAIL_TYPE_DESCRIPTION AS TRANSACTION_CODE, substring(convert(varchar(30),convert(money,B.PAID_AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,B.PAID_AMOUNT),1),0))  AMOUNT, B.CLAIM_ID, B.EXPENSE_ID, B.EXPENSE_BREAKDOWN_ID FROM CLM_ACTIVITY_EXPENSE_BREAKDOWN B  LEFT JOIN CLM_TYPE_DETAIL D ON B.TRANSACTION_CODE = D.DETAIL_TYPE_ID WHERE B.CLAIM_ID =   " + Request["CLAIM_ID"].ToString() + " AND B.EXPENSE_ID = " + Request["EXPENSE_ID"].ToString() +  " AND B.ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " ) Test ";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Transaction code^Amount";
				objWebGrid.SearchColumnNames = "TRANSACTION_CODE^AMOUNT";
				objWebGrid.SearchColumnType = "T^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Transaction code^Amount";
				objWebGrid.DisplayColumnNumbers = "1^2";
				objWebGrid.DisplayColumnNames = "TRANSACTION_CODE^AMOUNT";
				objWebGrid.DisplayTextLength = "50^50";
				objWebGrid.DisplayColumnPercent = "50^50";
				objWebGrid.PrimaryColumns = "5";
				objWebGrid.PrimaryColumnsName = "EXPENSE_BREAKDOWN_ID";
				objWebGrid.ColumnTypes = "B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Expense Breakdown";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterValue = "Y";
				objWebGrid.QueryStringColumns = "EXPENSE_BREAKDOWN_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddActivityExpenseBreakdown.aspx?CLAIM_ID=" +  strTemp +"&"; 
				TabCtl.TabTitles ="Expense Breakdown";
				TabCtl.TabLength =150;
	
			}
			catch (Exception ex)
			{throw (ex);}
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
