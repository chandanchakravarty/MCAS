/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	4/28/2005 9:06:31 PM
<End Date				: -	
<Description			: - 	Code behind for Transaction Codes Index.
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
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Code behind for Transaction codes index.
	/// </summary>
	public class TransactionCodesIndex :  Cms.CmsWeb.cmsbase
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
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{	  base.ScreenId="184";//Scrren_id changed from 185 to 184 for Itrack Issue 4798 on 13 Jan 09
			//	TabCtl.TabURLs = "AddGlAccountInformation.aspx?GL_ID="+Session["GL_ID"]+"&";
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Accounting.TransactionCodesIndex", Assembly.GetExecutingAssembly());
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
				

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
                objWebGrid.SelectClause = "t1.TRAN_ID,case "+ GetLanguageID()+"when 2 then case t1.CATEGOTY_CODE when 'ACT' then 'Contabilidade' else 'Não-Contabilidade' end else case t1.CATEGOTY_CODE when 'ACT' then 'Accounting' else 'Non-Accounting' end  end as CATEGOTY_CODE,";
				objWebGrid.SelectClause +="case t1.TRAN_TYPE";
                objWebGrid.SelectClause += " when 'pre' then case " + GetLanguageID() + " when 2 then 'Transações premium' else 'Premium Transactions' end";
                objWebGrid.SelectClause += " when 'fee' then case " + GetLanguageID() + " when 2 then 'Transações taxas' else 'Fees Transactions' end";
                objWebGrid.SelectClause += " when 'rec' then case " + GetLanguageID() + " when 2 then 'recibos' else 'Reciepts' end";
                objWebGrid.SelectClause += " when 'pay' then case " + GetLanguageID() + " when 2 then 'pagamentos' else 'Payments' end";
                objWebGrid.SelectClause += " when 'dis' then case " + GetLanguageID() + " when 2 then 'descontos' else 'Discounts' end";
                objWebGrid.SelectClause += " when 'pnc' then case " + GetLanguageID() + " when 2 then 'Aviso Códigos Premium' else 'Premium Notice Codes' end ";
                objWebGrid.SelectClause += " when 'pas' then case " + GetLanguageID() + " when 2 then 'Devido passado Códigos' else 'Past Due Codes' end ";
                objWebGrid.SelectClause += " when 'pri' then case " + GetLanguageID() + " when 2 then 'Códigos de impressão' else 'Print Codes' end";
                objWebGrid.SelectClause += " when 'can' then case " + GetLanguageID() + " when 2 then 'Códigos de cancelamento' else 'Cancellation Codes' end";
				objWebGrid.SelectClause +=" end";
				objWebGrid.SelectClause +=" as TRAN_TYPE,";
				objWebGrid.SelectClause += "t1.TRAN_CODE,";
				objWebGrid.SelectClause += "t1.DISPLAY_DESCRIPTION,t1.IS_ACTIVE";								
				objWebGrid.FromClause = "ACT_TRANSACTION_CODES t1";

				objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.FilterLabel = "Include Inactive";

				objWebGrid.WhereClause = "";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Category^Transaction Type^Transaction Code^Transaction Description";
				objWebGrid.SearchColumnNames = "case t1.CATEGOTY_CODE when 'ACT' then 'Accounting' else 'Non-Accounting' end^t1.TRAN_TYPE^t1.TRAN_CODE^t1.DISPLAY_DESCRIPTION";
				objWebGrid.SearchColumnType = "T^T^T^T";
				objWebGrid.OrderByClause = "CATEGOTY_CODE asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				objWebGrid.DisplayColumnNames = "CATEGOTY_CODE^TRAN_TYPE^TRAN_CODE^DISPLAY_DESCRIPTION";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Category^Transaction Type^Transaction Code^Transaction Description";
				objWebGrid.DisplayTextLength = "100^100^100^100";
				objWebGrid.DisplayColumnPercent = "25^25^25^25";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.TRAN_ID";
				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				//specifying number of the rows to be shown
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Transaction Codes Setup";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show InActive";
				objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "TRAN_ID";
				objWebGrid.DefaultSearch = "Y";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
            TabCtl.TabURLs = "AddTransactionCodes.aspx??&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
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