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
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Code behind for RegCommSetup_AgencyIndex.
	/// </summary>
	public class RegCommSetup_AgencyIndex :  Cms.CmsWeb.cmsbase
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
            SetCultureThread(GetLanguageCode());
			base.ScreenId="178";
			//TabCtl.TabURLs	 = Request.QueryString["COMMISSION_TYPE"]!="P"?"AddRegCommSetup_Agency.aspx?COMMISSION_TYPE="+Request.QueryString["COMMISSION_TYPE"]+"&":"CommPropInspCredit.aspx?COMMISSION_TYPE="+Request.QueryString["COMMISSION_TYPE"]+"&";
			//TabCtl.TabTitles = Request.QueryString["COMMISSION_TYPE"]!="P"?"Agency Commission":"Inspection Credit";

			string CommType = Request.QueryString["COMMISSION_TYPE"].ToString();
			
			switch(CommType)
			{
				case "P":
						TabCtl.TabURLs	 = "CommPropInspCredit.aspx?COMMISSION_TYPE="+CommType+"&";
						TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1509");//"Inspection Credit";
						break;
				case "C":
						TabCtl.TabURLs	 = "CommCompAppBonus.aspx?COMMISSION_TYPE="+CommType+"&";
                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1508");//"Complete App Bonus";
						break;
				default :
						TabCtl.TabURLs	 = "AddRegCommSetup_Agency.aspx?COMMISSION_TYPE="+CommType+"&";
                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1507"); //"Agency Commission";
						break;

			}
			#region GETTING BASE COLOR FOR ROW SELECTION
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Accounting.RegCommSetup_AgencyIndex", Assembly.GetExecutingAssembly());  
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
			switch(Request.QueryString["COMMISSION_TYPE"].ToString())
			{
				case "R":
					 base.ScreenId="178";
					SetRegularCommissionGrid(objWebGrid,colors,colorScheme);
					break;
				case "A":
					base.ScreenId="179";
					SetAdditionalCommissionGrid(objWebGrid,colors,colorScheme);
					break;
				case "P":
					base.ScreenId="180";
					SetPropertyInspectionCreditGrid(objWebGrid,colors,colorScheme);
					break;
				case "C":
					base.ScreenId="385";
					//base.ScreenId="180_1"; not in use
					SetCompleteAppBonusGrid(objWebGrid,colors,colorScheme);
					break;
			}
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

		private void SetRegularCommissionGrid(BaseDataGrid objWebGrid,string colors,string colorScheme)
		{
			try
			{
				//t1.COMM_ID 1,
				//t1.STATE_ID 2
				//,t1.LOB_ID 3
				//,t1.SUB_LOB_ID 4,
				//t1.CLASS_RISK 5,
				//t1.TERM 6,
				//t1.EFFECTIVE_FROM_DATE 7 ,
				//t1.EFFECTIVE_TO_DATE 8,
				//t1.COMMISSION_PERCENT 9,
				//t1.MODIFIED_BY 10,
				//t1.LAST_UPDATED_DATETIME 11,
				//t1.IS_ACTIVE 12 , 
				//STATE_NAME 13,
				//LOB_DESC 14,
				//SUB_LOB_DESC 15,
				//TermDesc 16,
				//userName 17
				//LOOKUP_VALUE_DESC 18

				//Setting web grid control properties
				objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
                //objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
                objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,case when " + GetLanguageID() + "=1 then 101 else 103 end) as EFFECTIVE_FROM_DATE,Convert(varchar,t1.EFFECTIVE_TO_DATE,case when " + GetLanguageID() + "=1 then 101 else 103 end) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,case when " + GetLanguageID() + "=1 then 101 else 103 end) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";                
                objWebGrid.SelectClause += "case " + GetLanguageID() + " when 2 then case t1.STATE_ID when 0 then 'Todos' else t2.STATE_NAME end else case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end end as STATE_NAME,";
                objWebGrid.SelectClause += "case " + GetLanguageID() + " when 2 then case t1.LOB_ID when 0 then 'Todos' else isnull(mlmm.LOB_DESC,t3.LOB_DESC) end else case t1.LOB_ID when 0 then 'All' else isnull(mlmm.LOB_DESC,t3.LOB_DESC) end end as LOB_DESC,";
                objWebGrid.SelectClause += "case " + GetLanguageID() + " when 2 then case t1.SUB_LOB_ID when 0 then 'Todos' else t4.SUB_LOB_DESC end else case t1.SUB_LOB_ID when 0 then 'All' else t4.SUB_LOB_DESC end end as SUB_LOB_DESC,";
                objWebGrid.SelectClause += "case t1.TERM when 'F' then case "+GetLanguageID()+" when 2 then 'Primeiro Termo (NBS)' else 'First Term(NBS)' end else case "+GetLanguageID()+" when 2 then 'Termo Outros' else 'Other Term' end  end as TermDesc,ISNULL(t5.USER_FNAME+' '+t5.USER_LNAME,' ') as userName,";
				objWebGrid.SelectClause += "T7.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC,t3.LOB_ID as LobId";
				
				objWebGrid.FromClause =  " dbo.MNT_LOB_MASTER t3 RIGHT OUTER JOIN ";
                objWebGrid.FromClause += " dbo.ACT_REG_COMM_SETUP t1 LEFT OUTER JOIN  ";
                objWebGrid.FromClause +="  dbo.MNT_USER_LIST t5 ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN ";
                objWebGrid.FromClause +="  dbo.MNT_LOOKUP_VALUES t6 ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID FULL OUTER JOIN ";
                objWebGrid.FromClause +="  dbo.MNT_SUB_LOB_MASTER t4 ON t1.SUB_LOB_ID = t4.SUB_LOB_ID AND t3.LOB_ID = t4.LOB_ID LEFT OUTER JOIN ";
                objWebGrid.FromClause +="  dbo.MNT_COUNTRY_STATE_LIST t2 ON t1.STATE_ID = t2.STATE_ID LEFT OUTER JOIN";
                objWebGrid.FromClause += "  ACT_COMMISION_CLASS_MASTER T7 ON T1.CLASS_RISK=T7.COMMISSION_CLASS_ID  left outer join MNT_LOB_MASTER_MULTILINGUAL mlmm on mlmm.LOB_ID=t3.LOB_ID and mlmm.LANG_ID ="+GetLanguageID();

                objWebGrid.WhereClause = " COMMISSION_TYPE='" + Request.QueryString["COMMISSION_TYPE"]+"'" ;
                objWebGrid.WhereClause +=" and t1.STATE_ID=92" ;//Added by kuldeep only for Singapore(for demo only)
                // TFS # 836
                if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                    objWebGrid.SearchColumnHeadings = "Country^Product^Term^From Date^To Date";
                else
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeading");//"State^Product^Term^From Date^To Date";			//;//	
				objWebGrid.SearchColumnNames = "case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end^t3.LOB_ID^case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' end^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE";				
				objWebGrid.SearchColumnType = "T^L^T^D^D";

				objWebGrid.DropDownColumns="^LOB^^^";
				objWebGrid.OrderByClause = "t1.STATE_ID";
				objWebGrid.DisplayColumnNumbers = "13^14^15^18^16^7^8^9^11^17";
				objWebGrid.DisplayColumnNames = "STATE_NAME^LOB_DESC^SUB_LOB_DESC^LOOKUP_VALUE_DESC^TermDesc^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE^COMMISSION_PERCENT^LAST_UPDATED_DATETIME^userName";
                if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                    objWebGrid.DisplayColumnHeadings = "Country^Product^LOB^Risks^Term^From Date^To Date^Comm %^Last Amended^Amended By";
                else
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DispHeading"); //"State^Product^LOB^Risks^Term^From Date^To Date^Comm %^Last Amended^Amended By";
				objWebGrid.DisplayTextLength = "100^100^100^100^100^100^100^100^100^100";
				objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10^10^10^10^10";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.COMM_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";//
				//specifying number of the rows to be shown
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");// "Regular Commission Setup - Agency";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Show InActive";
				objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "COMM_ID";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.CellHorizontalAlign = "7";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		private void SetAdditionalCommissionGrid(BaseDataGrid objWebGrid,string colors,string colorScheme)
		{
			try
			{
				//t1.COMM_ID 1,
				//t1.STATE_ID 2
				//,t1.LOB_ID 3
				//,t1.SUB_LOB_ID 4,
				//t1.CLASS_RISK 5,
				//t1.TERM 6,
				//t1.EFFECTIVE_FROM_DATE 7 ,
				//t1.EFFECTIVE_TO_DATE 8,
				//t1.COMMISSION_PERCENT 9,
				//t1.MODIFIED_BY 10,
				//t1.LAST_UPDATED_DATETIME 11,
				//t1.IS_ACTIVE 12 , 
				//STATE_NAME 13,
				//LOB_DESC 14,
				//SUB_LOB_DESC 15,
				//TermDesc 16,
				//userName 17
				//LOOKUP_VALUE_DESC 18
				//t1.COMM_ID 19

				//Setting web grid control properties
				objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
				//objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
                objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,case when " + GetLanguageID() + "=1 then 101 else 103 end) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,case when " + GetLanguageID() + "=1 then 101 else 103 end) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,case when " + GetLanguageID() + "=1 then 101 else 103 end) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
                
                objWebGrid.SelectClause += "case " + GetLanguageID() + " when 2 then case t1.STATE_ID when 0 then 'Todos' else t2.STATE_NAME end else case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end end as STATE_NAME,";
                objWebGrid.SelectClause += "case " + GetLanguageID() + "when 2 then case t1.LOB_ID when 0 then 'Todos' else ISNULL(T9.LOB_DESC,t3.LOB_DESC) end else case t1.LOB_ID when 0 then 'All' else t3.LOB_DESC end end as LOB_DESC,";
                objWebGrid.SelectClause += "case " + GetLanguageID() + "when 2 then case t1.SUB_LOB_ID when 0 then 'Todos' else t4.SUB_LOB_DESC end else case t1.SUB_LOB_ID when 0 then 'All' else t4.SUB_LOB_DESC end end as SUB_LOB_DESC,";
                objWebGrid.SelectClause += "case t1.TERM when 'F' then case "+GetLanguageID()+" when 2 then 'Primeiro Termo (NBS)' else 'First Term(NBS)' end else case "+GetLanguageID()+" when 2 then 'Termo Outros' else 'Other Term' end  end as TermDesc,ISNULL(t5.USER_FNAME+' '+t5.USER_LNAME,' ') as userName,";
				objWebGrid.SelectClause += "T8.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC,t7.AGENCY_DISPLAY_NAME,t3.LOB_ID as LobId,t7.NUM_AGENCY_CODE ";
			
				objWebGrid.FromClause =  "  dbo.MNT_SUB_LOB_MASTER t4 FULL OUTER JOIN";
				objWebGrid.FromClause += "         dbo.MNT_LOB_MASTER t3 RIGHT OUTER JOIN";
				objWebGrid.FromClause += "   dbo.MNT_AGENCY_LIST t7 INNER JOIN";
				objWebGrid.FromClause += "    dbo.ACT_REG_COMM_SETUP t1 LEFT OUTER JOIN";
				objWebGrid.FromClause += "   dbo.MNT_USER_LIST t5 ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN";
				objWebGrid.FromClause += "   dbo.MNT_LOOKUP_VALUES t6 ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t7.AGENCY_ID = t1.AGENCY_ID ON t3.LOB_ID = t1.LOB_ID ON ";
				objWebGrid.FromClause += "   t4.SUB_LOB_ID = t1.SUB_LOB_ID AND t4.LOB_ID = t3.LOB_ID LEFT OUTER JOIN";
				objWebGrid.FromClause += "    dbo.MNT_COUNTRY_STATE_LIST t2 ON t1.STATE_ID = t2.STATE_ID  LEFT OUTER JOIN";
				objWebGrid.FromClause +="  ACT_COMMISION_CLASS_MASTER T8 ON T1.CLASS_RISK=T8.COMMISSION_CLASS_ID";
                objWebGrid.FromClause += " LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL T9 ON T9.LOB_ID=t1.LOB_ID AND T9.LANG_ID="+GetLanguageID();

				objWebGrid.FilterValue = "Y";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel4");// "Include Inactive";
				objWebGrid.FilterColumnName = "t1.IS_ACTIVE";

				objWebGrid.WhereClause = " COMMISSION_TYPE='"+Request.QueryString["COMMISSION_TYPE"]+"'";
				// old
//				objWebGrid.SearchColumnHeadings = "Agency Name^State^LOB";
//				objWebGrid.SearchColumnNames = "t7.AGENCY_DISPLAY_NAME^t2.STATE_NAME^t3.LOB_ID";
//				objWebGrid.SearchColumnType = "T^T^T";

				// new 
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");//"Agency Name^Num Code^State^Product^Term^From Date^To Date";
				objWebGrid.SearchColumnNames = "t7.AGENCY_DISPLAY_NAME^t7.NUM_AGENCY_CODE^t2.STATE_NAME^t3.LOB_ID^case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' end^t1.EFFECTIVE_FROM_DATE^t1.EFFECTIVE_TO_DATE";
				objWebGrid.SearchColumnType = "T^T^T^L^T^D^D";

				objWebGrid.DropDownColumns="^^^LOB^^^";				

				objWebGrid.OrderByClause = "AGENCY_DISPLAY_NAME asc";
				objWebGrid.DisplayColumnNumbers = "19^21^13^14^15^18^16^7^8^9^11^17";
				objWebGrid.DisplayColumnNames = "AGENCY_DISPLAY_NAME^NUM_AGENCY_CODE^STATE_NAME^LOB_DESC^SUB_LOB_DESC^LOOKUP_VALUE_DESC^TermDesc^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE^COMMISSION_PERCENT^LAST_UPDATED_DATETIME^userName";
                objWebGrid.DisplayColumnHeadings = "Agency^Num Code^Country^Product^LOB^Risks^Term^From Date^To Date^Comm %^Last Amended^Amended By";//objResourceMgr.GetString("DispHeading1"); Changed By Kuldeep
				objWebGrid.DisplayTextLength = "100^100^100^100^100^100^100^100^100^100^100^100";
				objWebGrid.DisplayColumnPercent = "9^9^9^9^9^9^9^9^9^9^9^9";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.COMM_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage1");  //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons1");//"1^Add New^0^addRecord";//
				//specifying number of the rows to be shown
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString1"); //"Additional Commission Setup - Agency";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel1"); //"Show Inactive";
				objWebGrid.FilterColumnName="t1.IS_ACTIVE";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "COMM_ID";
				objWebGrid.CellHorizontalAlign = "8";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		
        private void SetPropertyInspectionCreditGrid(BaseDataGrid objWebGrid,string colors,string colorScheme)
		{
			try
			{
				//t1.COMM_ID 1,
				//t1.STATE_ID 2
				//,t1.LOB_ID 3
				//,t1.SUB_LOB_ID 4,
				//t1.CLASS_RISK 5,
				//t1.TERM 6,
				//t1.EFFECTIVE_FROM_DATE 7 ,
				//t1.EFFECTIVE_TO_DATE 8,
				//t1.COMMISSION_PERCENT 9,
				//t1.MODIFIED_BY 10,
				//t1.LAST_UPDATED_DATETIME 11,
				//t1.IS_ACTIVE 12 , 
				//STATE_NAME 13,
				//LOB_DESC 14,
				//SUB_LOB_DESC 15,
				//TermDesc 16,
				//userName 17
				//LOOKUP_VALUE_DESC 18
                //Added by Agniswar
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                string XmlFullFilePath = ClsCommon.WebAppUNCPath + "/cmsweb/support/PageXml/" + strSysID + "/PropertyInspectionCreditIndex.xml ";
                SetDBGrid(objWebGrid, XmlFullFilePath, "");
				//Setting web grid control properties
				objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
                objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,case " + GetLanguageID() + " when 1 then 101 else 103 end) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,case " + GetLanguageID() + " when 1 then 101 else 103 end) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,case " + GetLanguageID() + " when 1 then 101 else 103 end) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
                objWebGrid.SelectClause += "case " + GetLanguageID() + " when 2 then case t1.STATE_ID when 0 then 'Todos' else t2.STATE_NAME end else  case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end end as STATE_NAME,";
                objWebGrid.SelectClause += "case " + GetLanguageID() + " when 2 then case t1.LOB_ID when 0 then 'Todos' else MLMM.LOB_DESC end else case t1.LOB_ID when 0 then 'All' else t3.LOB_DESC end end as LOB_DESC,";
				//objWebGrid.SelectClause += "case t1.SUB_LOB_ID when 0 then 'All' else t4.SUB_LOB_DESC end as SUB_LOB_DESC,";
                objWebGrid.SelectClause += "case 2 when 2 then  case t1.TERM when 'F' then 'Primeiro Período (NBS)' else 'Termo Outros' end else case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' end end as TermDesc,ISNULL(t5.USER_FNAME+' '+t5.USER_LNAME,' ') as userName,";
				objWebGrid.SelectClause += "T8.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC";
				
				objWebGrid.FromClause =  " MNT_LOB_MASTER t3 INNER JOIN";
				//objWebGrid.FromClause += " MNT_SUB_LOB_MASTER t4 ON t3.LOB_ID = t4.LOB_ID RIGHT OUTER JOIN";
				objWebGrid.FromClause +=" ACT_REG_COMM_SETUP t1 LEFT OUTER JOIN";
				objWebGrid.FromClause +=" MNT_USER_LIST t5 ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN";
				objWebGrid.FromClause +=" MNT_LOOKUP_VALUES t6 ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID LEFT OUTER JOIN ";
				//objWebGrid.FromClause +=" t4.SUB_LOB_ID = t1.SUB_LOB_ID ";
				objWebGrid.FromClause +=" MNT_COUNTRY_STATE_LIST t2 ON t1.STATE_ID = t2.STATE_ID  LEFT OUTER JOIN";
                objWebGrid.FromClause += "  ACT_COMMISION_CLASS_MASTER T8 ON T1.CLASS_RISK=T8.COMMISSION_CLASS_ID left outer join MNT_LOB_MASTER_MULTILINGUAL MLMM on MLMM.LOB_ID=t1.LOB_ID and MLMM.LANG_ID="+GetLanguageID();

                //objWebGrid.FilterValue = "Y";
                //objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel4"); //"Include Inactive";
                //objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
                objWebGrid.WhereClause = "t3.IS_ACTIVE='Y'";
                //objWebGrid.WhereClause = " COMMISSION_TYPE='"+Request.QueryString["COMMISSION_TYPE"]+"'";

                //objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings2");// "State^LOB^From Date^To Date";
                //objWebGrid.SearchColumnNames = "t2.STATE_NAME^t3.LOB_ID^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE";
                //objWebGrid.SearchColumnType = "T^L^D^D";

                //objWebGrid.DropDownColumns="^LOB^^";

                //objWebGrid.OrderByClause = "STATE_NAME asc";
                //objWebGrid.DisplayColumnNumbers = "13^14^7^8^9^11^17";
                //objWebGrid.DisplayColumnNames = "STATE_NAME^LOB_DESC^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE^COMMISSION_PERCENT^LAST_UPDATED_DATETIME^userName";
                //objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DispHeading2");//"State^LOB^From Date^To Date^Credit %^Last Amended^Amended By";
                //objWebGrid.DisplayTextLength = "100^100^100^100^100^100^100";
                //objWebGrid.DisplayColumnPercent = "14^14^14^14^14^14^16";
                //objWebGrid.PrimaryColumns = "1";
                //objWebGrid.PrimaryColumnsName = "t1.COMM_ID";
                //objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
                //objWebGrid.AllowDBLClick = "true";
                //objWebGrid.FetchColumns = "1^2^3^4^5";
                //objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage2"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons2"); //"1^Add New^0^addRecord";//
                ////specifying number of the rows to be shown
                //objWebGrid.PageSize = int.Parse(GetPageSize());
                //objWebGrid.CacheSize = int.Parse(GetCacheSize());
                //objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                //objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                //objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString2"); //"Property Inspection Credit";
                //objWebGrid.SelectClass = colors;
                //objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel2"); //"Show InActive";
                //objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
                //objWebGrid.FilterValue = "Y";
                //objWebGrid.RequireQuery = "Y";
                //objWebGrid.DefaultSearch = "Y";
                //objWebGrid.QueryStringColumns = "COMM_ID";
                //objWebGrid.CellHorizontalAlign = "4";
				//Adding to controls to gridholder

                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.SelectClass = colors;

                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}

		private void SetCompleteAppBonusGrid(BaseDataGrid objWebGrid,string colors,string colorScheme)
		{
			try
			{
                ////Setting web grid control properties
                //objWebGrid.WebServiceURL =  httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";;
                //objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
                //objWebGrid.SelectClause += "t2.STATE_NAME as STATE_NAME,";
                //objWebGrid.SelectClause += "isnull(mlmm.LOB_DESC, t3.LOB_DESC) as LOB_DESC,";
                //objWebGrid.SelectClause += "case "+GetLanguageID()+" when 2 then case t1.TERM when 'F' then 'Primeiro Período (NBS)' else 'Termo Outros' end else case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' end  end as TermDesc,ISNULL(t5.USER_FNAME+' '+t5.USER_LNAME,' ') as userName,";
                //objWebGrid.SelectClause += "T8.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC";
				
                //objWebGrid.FromClause =  " MNT_LOB_MASTER t3 INNER JOIN";
                //objWebGrid.FromClause +=" ACT_REG_COMM_SETUP t1 LEFT OUTER JOIN";
                //objWebGrid.FromClause +=" MNT_USER_LIST t5 ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN";
                //objWebGrid.FromClause +=" MNT_LOOKUP_VALUES t6 ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID LEFT OUTER JOIN ";
                //objWebGrid.FromClause +=" MNT_COUNTRY_STATE_LIST t2 ON t1.STATE_ID = t2.STATE_ID  LEFT OUTER JOIN";
                //objWebGrid.FromClause += "  ACT_COMMISION_CLASS_MASTER T8 ON T1.CLASS_RISK=T8.COMMISSION_CLASS_ID left outer join MNT_LOB_MASTER_MULTILINGUAL MLMM on MLMM.LOB_ID=t3.LOB_ID and MLMM.LANG_ID="+GetLanguageID();
			
                //objWebGrid.WhereClause = " COMMISSION_TYPE='"+Request.QueryString["COMMISSION_TYPE"]+"'";
							
                //objWebGrid.OrderByClause = "STATE_NAME asc";

                //objWebGrid.SearchColumnHeadings = "State^LOB^From Date^To Date";//objResourceMgr.GetString("SearchColumnHeadings3");//
                //objWebGrid.SearchColumnNames = "t2.STATE_NAME^t3.LOB_ID^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE";
                //objWebGrid.SearchColumnType = "T^L^D^D";

                //objWebGrid.DropDownColumns="^LOB^^";
				
                //objWebGrid.DisplayColumnNumbers = "2^12^5^6^7^9^14";
                //objWebGrid.DisplayColumnNames = "STATE_NAME^LOB_DESC^EFFECTIVE_FROM_DATE^EFFECTIVE_TO_DATE^COMMISSION_PERCENT^LAST_UPDATED_DATETIME^userName";
                //objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DispHeading3");//"State^LOB^From Date^To Date^Amount^Last Amended^Amended By";
                //objWebGrid.DisplayTextLength = "15^15^20^20^10^10^10";
                //objWebGrid.DisplayColumnPercent = "15^15^20^20^10^10^10";
				
                //objWebGrid.PrimaryColumns = "1";
                //objWebGrid.PrimaryColumnsName = "t1.COMM_ID";
				
                //objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
                //objWebGrid.AllowDBLClick = "true";
                //objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                //objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage3");// "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //objWebGrid.ExtraButtons =objResourceMgr.GetString("ExtraButtons3"); //"1^Add New^0^addRecord";//
                //objWebGrid.PageSize = int.Parse(GetPageSize());
                //objWebGrid.CacheSize = int.Parse(GetCacheSize());
                //objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                //objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                //objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString3");//"Complete App Bonus";
                //objWebGrid.SelectClass = colors;
                //objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel3");// "Show InActive";
                //objWebGrid.FilterColumnName = "t1.IS_ACTIVE";
                //objWebGrid.FilterValue = "Y";
                //objWebGrid.RequireQuery = "Y";
                //objWebGrid.DefaultSearch = "Y";
                //objWebGrid.QueryStringColumns = "COMM_ID";
                //objWebGrid.CellHorizontalAlign = "4";

                ////Adding to controls to gridholder
                //GridHolder.Controls.Add(objWebGrid);

                /****
                 * 
                 * */

            
                //Added by Agniswar
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                string XmlFullFilePath = ClsCommon.WebAppUNCPath + "/cmsweb/support/PageXml/" + strSysID + "/CompleteAppBonusIndex.xml";
               
                ////Setting web grid control properties
      				
                SetDBGrid(objWebGrid, XmlFullFilePath, "");
               // objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;
                objWebGrid.SelectClause = "t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.TERM,convert(varchar,t1.EFFECTIVE_FROM_DATE,case " + GetLanguageID() + " when 1 then 101 else 103 end) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,case " + GetLanguageID() + " when 1 then 101 else 103 end) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,convert(varchar,t1.LAST_UPDATED_DATETIME,case " + GetLanguageID() + " when 1 then 101 else 103 end) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,";
                objWebGrid.SelectClause += "t2.STATE_NAME as STATE_NAME,";
                objWebGrid.SelectClause += "isnull(mlmm.LOB_DESC, t3.LOB_DESC) as LOB_DESC,";
                objWebGrid.SelectClause += "case " + GetLanguageID() + " when 2 then case t1.TERM when 'F' then 'Primeiro Período (NBS)' else 'Termo Outros' end else case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' end  end as TermDesc,ISNULL(t5.USER_FNAME+' '+t5.USER_LNAME,' ') as userName,";
                objWebGrid.SelectClause += "T8.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC";

                objWebGrid.FromClause = " MNT_LOB_MASTER t3 INNER JOIN";
                objWebGrid.FromClause += " ACT_REG_COMM_SETUP t1 LEFT OUTER JOIN";
                objWebGrid.FromClause += " MNT_USER_LIST t5 ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN";
                objWebGrid.FromClause += " MNT_LOOKUP_VALUES t6 ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID LEFT OUTER JOIN ";
                objWebGrid.FromClause += " MNT_COUNTRY_STATE_LIST t2 ON t1.STATE_ID = t2.STATE_ID  LEFT OUTER JOIN";
                objWebGrid.FromClause += "  ACT_COMMISION_CLASS_MASTER T8 ON T1.CLASS_RISK=T8.COMMISSION_CLASS_ID left outer join MNT_LOB_MASTER_MULTILINGUAL MLMM on MLMM.LOB_ID=t3.LOB_ID and MLMM.LANG_ID=" + GetLanguageID();

                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.SelectClass = colors;

                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);
            }
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
	}
}
