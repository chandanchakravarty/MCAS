/******************************************************************************************
<Author				: -   Pravesh Chandel
<Start Date				: -	27/10/2006 
<End Date				: -	
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
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Summary description for AddMinimumPremAmtIndex.
	/// </summary>
	public class AddMinimumPremAmtIndex : Cms.CmsWeb.cmsbase
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
			// Put user code to initialize the page here
			//base.ScreenId="178";
			  base.ScreenId = "364";
			TabCtl.TabURLs = "AddMinimumPremAmt.aspx?";  //ROW_ID="+Request.QueryString["ROW_ID"];
			//TabCtl.TabTitles = Request.QueryString["COMMISSION_TYPE"]!="C"?"Agency Commission":"Inspection Credit";
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Accounting.AddMinimumPremAmtIndex", Assembly.GetExecutingAssembly());

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors =System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
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
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			SetRegularCommissionGrid(objWebGrid,colors,colorScheme);
		}
		#region  set grid
		private void SetRegularCommissionGrid(BaseDataGrid objWebGrid,string colors,string colorScheme)
		{
			try
			{
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                string XmlFullFilePath = ClsCommon.WebAppUNCPath + "/cmsweb/support/PageXml/" + strSysID + "/AddMinimumPremAmtIndex.xml ";
                SetDBGrid(objWebGrid, XmlFullFilePath, "");
				//Setting web grid control properties
				objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
                objWebGrid.SelectClause = "t1.ROW_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,convert(varchar,t1.EFFECTIVE_FROM_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as EFFECTIVE_TO_DATE,dbo.fun_FormatCurrency(t1.PREMIUM_AMT,"+ClsCommon.BL_LANG_ID +")  as PREMIUM_AMT ,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
				objWebGrid.SelectClause += "case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end as STATE_NAME,";
                objWebGrid.SelectClause += "case t1.LOB_ID when 0 then 'All' else case T6.LANG_ID when 2 then T6.LOB_DESC else t3.LOB_DESC end end  as LOB_DESC,";
				objWebGrid.SelectClause += "case t1.SUB_LOB_ID when 0 then 'All' else t4.SUB_LOB_DESC end as SUB_LOB_DESC,";
				objWebGrid.SelectClause += "t5.USER_FNAME+' '+t5.USER_LNAME as userName,";
				objWebGrid.SelectClause += "t3.LOB_ID as LobId";
				
				objWebGrid.FromClause =  " dbo.MNT_LOB_MASTER t3 RIGHT OUTER JOIN ";
				objWebGrid.FromClause += " dbo.ACT_MINIMUM_PREM_CANCEL t1 ON t3.LOB_ID = t1.LOB_ID LEFT OUTER JOIN  ";
				objWebGrid.FromClause +="  dbo.MNT_USER_LIST t5 ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN";
				//objWebGrid.FromClause +="  t3.LOB_ID = t1.LOB_ID FULL OUTER JOIN ";
				objWebGrid.FromClause +="  dbo.MNT_SUB_LOB_MASTER t4 ON t1.SUB_LOB_ID = t4.SUB_LOB_ID AND t3.LOB_ID = t4.LOB_ID LEFT OUTER JOIN ";
				objWebGrid.FromClause +="  dbo.MNT_COUNTRY_STATE_LIST t2 ON t1.STATE_ID = t2.STATE_ID";
                objWebGrid.FromClause += " LEFT OUTER JOIN dbo.MNT_LOB_MASTER_MULTILINGUAL T6 ON  t6.LOB_ID = t1.LOB_ID AND T6.LANG_ID=" + GetLanguageID();
					//LEFT OUTER JOIN";
				//objWebGrid.FromClause +="  ACT_COMMISION_CLASS_MASTER T7 ON T1.CLASS_RISK=T7.COMMISSION_CLASS_ID";
				objWebGrid.WhereClause = " 1=1";
				//objWebGrid.WhereClause = " ROW_ID='"+Request.QueryString["ROW_ID"]+"'";
				// old 
				//				objWebGrid.SearchColumnHeadings = "State^LOB";				
				//				objWebGrid.SearchColumnNames = "t2.STATE_NAME^t3.LOB_ID";				
				//				objWebGrid.SearchColumnType = "T^T";
				// new 


                /****FROM
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"State^LOB^From Date^To Date";				
                objWebGrid.SearchColumnNames = "case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end^t3.LOB_ID^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE";				
                objWebGrid.SearchColumnType = "T^L^D^D";

                objWebGrid.DropDownColumns="^LOB^^";
                //objWebGrid.OrderByClause = "case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end asc";
                objWebGrid.OrderByClause = " STATE_NAME";
                objWebGrid.DisplayColumnNumbers = "11^12^13^7^8";  //^9^17";
                objWebGrid.DisplayColumnNames = "STATE_NAME^LOB_DESC^SUB_LOB_DESC^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE^PREMIUM_AMT"; //^LAST_UPDATED_DATETIME^userName";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"State^LOB^Sub-LOB^Effective From Date^Effective To Date^Minimum Premium Amount";   //^Last Amended^Amended By";
                objWebGrid.DisplayTextLength = "100^100^100^100^100^100"; //^100^100";
                objWebGrid.DisplayColumnPercent = "10^20^20^10^20^20";  //^10^20";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "t1.ROW_ID";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B"; //^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                //specifying number of the rows to be shown
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Minimum Premium Amount at cancellation";
                objWebGrid.SelectClass = colors;
                //objWebGrid.FilterLabel = "Show InActive";
                //objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
                //objWebGrid.FilterValue = "Y";
                objWebGrid.RequireQuery = "Y";
                objWebGrid.QueryStringColumns = "ROW_ID";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.CellHorizontalAlign ="5";
                    ******/





				//Adding to controls to gridholder
				
				//Setting web grid control properties
			/*	objWebGrid.WebServiceURL =  "http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
				objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
				objWebGrid.SelectClause += "case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end as STATE_NAME,";
				objWebGrid.SelectClause += "case t1.LOB_ID when 0 then 'All' else t3.LOB_DESC end as LOB_DESC,";
				objWebGrid.SelectClause += "case t1.SUB_LOB_ID when 0 then 'All' else t4.SUB_LOB_DESC end as SUB_LOB_DESC,";
				objWebGrid.SelectClause += "case t1.TERM when 'F' then 'First Term' else 'Other Term' end as TermDesc,t5.USER_FNAME+' '+t5.USER_LNAME as userName,";
				objWebGrid.SelectClause += "T7.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC,t3.LOB_ID as LobId";
				
				objWebGrid.FromClause =  " dbo.MNT_LOB_MASTER t3 RIGHT OUTER JOIN ";
				objWebGrid.FromClause += " dbo.ACT_REG_COMM_SETUP t1 INNER JOIN  ";
				objWebGrid.FromClause +="  dbo.MNT_USER_LIST t5 ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN ";
				objWebGrid.FromClause +="  dbo.MNT_LOOKUP_VALUES t6 ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID FULL OUTER JOIN ";
				objWebGrid.FromClause +="  dbo.MNT_SUB_LOB_MASTER t4 ON t1.SUB_LOB_ID = t4.SUB_LOB_ID AND t3.LOB_ID = t4.LOB_ID LEFT OUTER JOIN ";
				objWebGrid.FromClause +="  dbo.MNT_COUNTRY_STATE_LIST t2 ON t1.STATE_ID = t2.STATE_ID LEFT OUTER JOIN";
				objWebGrid.FromClause +="  ACT_COMMISION_CLASS_MASTER T7 ON T1.CLASS_RISK=T7.COMMISSION_CLASS_ID";
				
				objWebGrid.WhereClause = " COMMISSION_TYPE='"+Request.QueryString["COMMISSION_TYPE"]+"'";
				// old 
				//				objWebGrid.SearchColumnHeadings = "State^LOB";				
				//				objWebGrid.SearchColumnNames = "t2.STATE_NAME^t3.LOB_ID";				
				//				objWebGrid.SearchColumnType = "T^T";
				// new 
				objWebGrid.SearchColumnHeadings = "State^LOB^Term^From Date^To Date";				
				objWebGrid.SearchColumnNames = "case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end^t3.LOB_ID^case t1.TERM when 'F' then 'First Term' else 'Other Term' end^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE";				
				objWebGrid.SearchColumnType = "T^T^T^D^D";

				objWebGrid.DropDownColumns="^LOB^^^^";
				objWebGrid.OrderByClause = "case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end asc";
				objWebGrid.DisplayColumnNumbers = "13^14^15^18^16^7^8^9^11^17";
				objWebGrid.DisplayColumnNames = "STATE_NAME^LOB_DESC^SUB_LOB_DESC^LOOKUP_VALUE_DESC^TermDesc^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE^COMMISSION_PERCENT^LAST_UPDATED_DATETIME^userName";
				objWebGrid.DisplayColumnHeadings = "State^LOB^Sub-LOB^Risks^Term^From Date^To Date^Comm %^Last Amended^Amended By";
				objWebGrid.DisplayTextLength = "100^100^100^100^100^100^100^100^100^100";
				objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10^10^10^10^10";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.COMM_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				//specifying number of the rows to be shown
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				objWebGrid.HImagePath = System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Mimimum Premium Amount at cancellation";
				objWebGrid.SelectClass = colors;
				objWebGrid.FilterLabel = "Show InActive";
				objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "COMM_ID";
				objWebGrid.DefaultSearch = "Y";
				//Adding to controls to gridholder
				*/
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.SelectClass = colors;
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
            TabCtl.TabURLs = "AddMinimumPremAmt.aspx??&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
		}
		#endregion


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
