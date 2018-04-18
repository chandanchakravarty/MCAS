	/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	4/28/2005 9:06:31 PM
<End Date				: -	
<Description				: - 	Code behind for pkg Lob details Index.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
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


namespace Cms.CmsWeb.Maintenance.Accounting
{

		/// <summary>
		/// Code behind for define sub ranges Index.
		/// </summary>
		public class DefineSubRangesIndex : Cms.CmsWeb.cmsbase
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
			{
				base.ScreenId="124_1";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Accounting.DefineSubRangesIndex", Assembly.GetExecutingAssembly());	
				//TabCtl.TabURLs = "AddPkgLobDetails.aspx?CUSTOMERID="+Request.QueryString["CUSTOMER_ID"]+"&APPVERSIONID="+Request.QueryString["APP_VERSION_ID"]+"&APPID="+Request.QueryString["APP_ID"]+"&";
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
					
					//Setting web grid control properties
					objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
                    objWebGrid.SelectClause = "t1.CATEGORY_ID,t1.PARENT_CATEGORY_ID,case " + GetLanguageID() + " when 2 then case when t1.CATEGORY_DESC='Asset' then 'Ativos' else case when t1.CATEGORY_DESC='Liability' then 'Responsabilidade' else case when t1.CATEGORY_DESC='Equity' then 'Capital' else case when t1.CATEGORY_DESC='Income' then 'Renda' else case when t1.CATEGORY_DESC='Expense' then 'Despesa' else case when t1.CATEGORY_DESC='llllll' then 'llllll' end end  end  end end end Else t1.CATEGORY_DESC end as CATEGORY_DESC,t1.RANGE_FROM,t1.RANGE_TO,case  " + GetLanguageID() + " when 2 then case when t2.ACC_TYPE_DESC='Asset' then 'Ativos' else case when t2.ACC_TYPE_DESC='Liability' then 'Responsabilidade' else case when t2.ACC_TYPE_DESC='Equity' then 'Capital' else case when t2.ACC_TYPE_DESC='Income' then 'Renda' else case when t2.ACC_TYPE_DESC='Expense' then 'Despesa' end  end  end end end Else t2.ACC_TYPE_DESC end as ACC_TYPE_DESC ";
					objWebGrid.FromClause = "ACT_GL_ACCOUNT_RANGES t1, ACT_TYPE_MASTER t2";
					objWebGrid.WhereClause = " t1.PARENT_CATEGORY_ID = t2.ACC_TYPE_ID and PARENT_CATEGORY_ID is not null";
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Main Type^Sub Type^From Account Number^To Account Number";
					objWebGrid.SearchColumnNames = "t2.ACC_TYPE_DESC^t1.CATEGORY_DESC^t1.RANGE_FROM^t1.RANGE_TO";
					objWebGrid.SearchColumnType = "T^T^T^T";
					objWebGrid.OrderByClause = "ACC_TYPE_DESC asc";
					objWebGrid.DisplayColumnNumbers = "6^3^4^5";
					objWebGrid.DisplayColumnNames = "ACC_TYPE_DESC^CATEGORY_DESC^RANGE_FROM^RANGE_TO";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Main Type^Sub Type^From Account Number^To Account Number";
					objWebGrid.DisplayTextLength = "100^100^100^100";
					objWebGrid.DisplayColumnPercent = "25^25^25^25";
					objWebGrid.PrimaryColumns = "1";
					objWebGrid.PrimaryColumnsName = "t1.CATEGORY_ID";
					objWebGrid.ColumnTypes = "B^B^B^B";
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3^4^5";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"2^Add New~Back^0~1^addRecord~ChangeToPrevTab";
					//specifying number of the rows to be shown
					objWebGrid.PageSize = int.Parse(GetPageSize());
					objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Define Sub Ranges";
					objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show InActive";
					objWebGrid.FilterColumnName = "";
					objWebGrid.FilterValue = "";
					objWebGrid.RequireQuery = "Y";
					objWebGrid.QueryStringColumns = "CATEGORY_ID";
					objWebGrid.DefaultSearch	= "Y";
					//Adding to controls to gridholder
					GridHolder.Controls.Add(objWebGrid);
				}
				catch
				{
				}
                TabCtl.TabURLs ="AddSubRanges.aspx??&";
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