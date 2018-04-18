/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 27, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is Grid file used for Excess Layer for a reinsurance contract. 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

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
using System.Resources;
using System.Reflection;
using System.IO;
using Cms.ExceptionPublisher;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for SplitIndex.
	/// </summary>
	public class SplitIndex :Cms.CmsWeb.cmsbase
	{
		
		# region D E C L A R A T I O N 

		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		//protected System.Web.UI.WebControls.Table tblReport;
		//protected System.Web.UI.WebControls.Panel pnlReport;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
        ResourceManager objResourceMgr = null;
		# endregion 
		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				base.ScreenId = "398_0";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.SplitIndex", Assembly.GetExecutingAssembly());
		
				#region G E T T I N G   B A S E   C O L O R   F O R   R O W   S E L E C T I O N

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

				# region C O D E   F O R   G R I D   C O N T R O L

				Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
																				
				//objWebGrid.SelectClause			= "A.REIN_SPLIT_DEDUCTION_ID REIN_SPLIT_DEDUCTION_ID,B.LOB_DESC as Policy,B.LOB_DESC As PremiumSplit,convert(varchar,A.REIN_EFFECTIVE_DATE,101) as REIN_EFFECTIVE_DATE,B.LOB_DESC As REIN_LINE_OF_BUSINESS,D.STATE_CODE as REIN_STATE,A.REIN_COVERAGE As REIN_COVERAGE,A.REIN_IST_SPLIT As REIN_IST_SPLIT,F.COV_DES As REIN_IST_SPLIT_COVERAGE,A.REIN_2ND_SPLIT As REIN_2ND_SPLIT,E.COV_DES As REIN_2ND_SPLIT_COVERAGE, ISNULL(A.IS_ACTIVE,'Y') AS IS_ACTIVE";
				objWebGrid.SelectClause			= "REIN_SPLIT_DEDUCTION_ID,Policy,PremiumSplit,REIN_EFFECTIVE_DATE,REIN_LINE_OF_BUSINESS,REIN_STATE,REIN_COVERAGE,REIN_IST_SPLIT,REIN_IST_SPLIT_COVERAGE,REIN_2ND_SPLIT,REIN_2ND_SPLIT_COVERAGE,IS_ACTIVE,LOB_ID";
				//objWebGrid.FromClause			= "MNT_REIN_SPLIT A INNER JOIN MNT_LOB_MASTER B ON A.REIN_LINE_OF_BUSINESS=B.LOB_ID inner join MNT_COUNTRY_STATE_LIST D On D.State_ID=A.REIN_STATE left outer join MNT_COVERAGE F on F.COV_CODE=A.REIN_IST_SPLIT_COVERAGE AND A.REIN_STATE =F.STATE_ID and F.LOB_ID=A.REIN_LINE_OF_BUSINESS left outer join MNT_COVERAGE E on E.COV_CODE=A.REIN_2ND_SPLIT_COVERAGE AND A.REIN_STATE =E.STATE_ID and E.LOB_ID=A.REIN_LINE_OF_BUSINESS";
				objWebGrid.FromClause			+= "(select A.REIN_SPLIT_DEDUCTION_ID REIN_SPLIT_DEDUCTION_ID,B.LOB_DESC as Policy,B.LOB_DESC As PremiumSplit,convert(varchar,A.REIN_EFFECTIVE_DATE,103) as REIN_EFFECTIVE_DATE,B.LOB_DESC As REIN_LINE_OF_BUSINESS,D.STATE_NAME as REIN_STATE,A.REIN_COVERAGE As REIN_COVERAGE,A.REIN_IST_SPLIT As REIN_IST_SPLIT,";
//				objWebGrid.FromClause			+= " case when (select COV_DES from MNT_COVERAGE   where COV_CODE=A.REIN_IST_SPLIT_COVERAGE AND A.REIN_STATE =STATE_ID  and LOB_ID=A.REIN_LINE_OF_BUSINESS) ";
//				objWebGrid.FromClause			+= " is not null then (select COV_DES from MNT_COVERAGE   where COV_CODE=A.REIN_IST_SPLIT_COVERAGE AND A.REIN_STATE =STATE_ID  and LOB_ID=A.REIN_LINE_OF_BUSINESS) ";
//				objWebGrid.FromClause			+= " else (select  COV_DES from MNT_REINSURANCE_COVERAGE where COV_CODE=A.REIN_IST_SPLIT_COVERAGE AND A.REIN_STATE =STATE_ID  and LOB_ID=A.REIN_LINE_OF_BUSINESS) end AS ";
				objWebGrid.FromClause			+= " dbo.func_IstSplitCoverage(REIN_SPLIT_DEDUCTION_ID) as REIN_IST_SPLIT_COVERAGE,A.REIN_2ND_SPLIT As REIN_2ND_SPLIT,";
//				objWebGrid.FromClause			+= " case when (select COV_DES from MNT_COVERAGE   where COV_CODE=A.REIN_2ND_SPLIT_COVERAGE AND A.REIN_STATE =STATE_ID  and LOB_ID=A.REIN_LINE_OF_BUSINESS)";
//				objWebGrid.FromClause			+= " is not null then  (select COV_DES from MNT_COVERAGE   where COV_CODE=A.REIN_2ND_SPLIT_COVERAGE AND A.REIN_STATE =STATE_ID  and LOB_ID=A.REIN_LINE_OF_BUSINESS)";
//				objWebGrid.FromClause			+= " else (select COV_DES from MNT_REINSURANCE_COVERAGE   where COV_CODE=A.REIN_2ND_SPLIT_COVERAGE AND A.REIN_STATE =STATE_ID  and LOB_ID=A.REIN_LINE_OF_BUSINESS) end AS 
				objWebGrid.FromClause			+= " dbo.func_2ndSplitCoverage(REIN_SPLIT_DEDUCTION_ID) as REIN_2ND_SPLIT_COVERAGE, ISNULL(A.IS_ACTIVE,'Y') AS IS_ACTIVE,B.LOB_ID LOB_ID";
				objWebGrid.FromClause			+= " from MNT_REIN_SPLIT A 	inner JOIN MNT_LOB_MASTER B ON A.REIN_LINE_OF_BUSINESS=B.LOB_ID inner join MNT_COUNTRY_STATE_LIST D On D.State_ID=A.REIN_STATE ) tempt";


//				objWebGrid.FromClause			= "(select A.REIN_SPLIT_DEDUCTION_ID REIN_SPLIT_DEDUCTION_ID,B.LOB_DESC as Policy,B.LOB_DESC As PremiumSplit,convert(varchar,A.REIN_EFFECTIVE_DATE,101) as REIN_EFFECTIVE_DATE,B.LOB_DESC As REIN_LINE_OF_BUSINESS,D.STATE_NAME as REIN_STATE,A.REIN_COVERAGE As REIN_COVERAGE,A.REIN_IST_SPLIT As REIN_IST_SPLIT,F.COV_DES As REIN_IST_SPLIT_COVERAGE,A.REIN_2ND_SPLIT As REIN_2ND_SPLIT,E.COV_DES As REIN_2ND_SPLIT_COVERAGE, ISNULL(A.IS_ACTIVE,'Y') AS IS_ACTIVE,B.LOB_ID LOB_ID";
//				objWebGrid.FromClause			+= " from MNT_REIN_SPLIT A 	inner JOIN MNT_LOB_MASTER B ON A.REIN_LINE_OF_BUSINESS=B.LOB_ID inner join MNT_COUNTRY_STATE_LIST D On D.State_ID=A.REIN_STATE inner  join MNT_COVERAGE F on F.COV_CODE=A.REIN_IST_SPLIT_COVERAGE AND A.REIN_STATE =F.STATE_ID and F.LOB_ID=A.REIN_LINE_OF_BUSINESS	inner join MNT_COVERAGE E on E.COV_CODE=A.REIN_2ND_SPLIT_COVERAGE AND A.REIN_STATE =E.STATE_ID and E.LOB_ID=A.REIN_LINE_OF_BUSINESS ";
//				objWebGrid.FromClause			+= "union select A.REIN_SPLIT_DEDUCTION_ID REIN_SPLIT_DEDUCTION_ID,B.LOB_DESC as Policy,B.LOB_DESC As PremiumSplit,convert(varchar,A.REIN_EFFECTIVE_DATE,101) as REIN_EFFECTIVE_DATE,B.LOB_DESC As REIN_LINE_OF_BUSINESS,D.STATE_NAME as REIN_STATE,A.REIN_COVERAGE As REIN_COVERAGE,A.REIN_IST_SPLIT As REIN_IST_SPLIT,F.COV_DES As REIN_IST_SPLIT_COVERAGE,A.REIN_2ND_SPLIT As REIN_2ND_SPLIT,E.COV_DES As REIN_2ND_SPLIT_COVERAGE, ISNULL(A.IS_ACTIVE,'Y') AS IS_ACTIVE,B.LOB_ID LOB_ID from MNT_REIN_SPLIT A ";
//				objWebGrid.FromClause			+="inner JOIN MNT_LOB_MASTER B ON A.REIN_LINE_OF_BUSINESS=B.LOB_ID 	inner join MNT_COUNTRY_STATE_LIST D On D.State_ID=A.REIN_STATE 	inner join MNT_REINSURANCE_COVERAGE F on F.COV_CODE=A.REIN_IST_SPLIT_COVERAGE AND A.REIN_STATE =F.STATE_ID and F.LOB_ID=A.REIN_LINE_OF_BUSINESS inner join MNT_REINSURANCE_COVERAGE E on E.COV_CODE=A.REIN_2ND_SPLIT_COVERAGE AND A.REIN_STATE =E.STATE_ID and E.LOB_ID=A.REIN_LINE_OF_BUSINESS) tempt";
				//objWebGrid.SearchColumnHeadings = "LOB^Deduct Policy Assessment/Fees from Coverage^Effective Date^State^1st Split^2nd Split";
				//objWebGrid.SearchColumnHeadings = "State^Effective Date^LOB^1st Split^2nd Split^1st Split%^2nd Split%";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Product^Effective Date^State^1st Split Coverage^1st Split%^2nd Split Coverage^2nd Split%";
				//objWebGrid.SearchColumnNames	= "A.REIN_LINE_OF_BUSINESS^REIN_EFFECTIVE_DATE^D.STATE_CODE^F.COV_DES^E.COV_DES";
				//objWebGrid.SearchColumnNames	= "REIN_STATE^REIN_EFFECTIVE_DATE^LOB_ID^REIN_IST_SPLIT_COVERAGE^REIN_2ND_SPLIT_COVERAGE^REIN_IST_SPLIT^REIN_2ND_SPLIT";
                objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames");//"LOB_ID^REIN_EFFECTIVE_DATE^REIN_STATE^REIN_IST_SPLIT_COVERAGE^REIN_IST_SPLIT^REIN_2ND_SPLIT_COVERAGE^REIN_2ND_SPLIT";
				//objWebGrid.SearchColumnType		= "T^D^L^T^T^T^T";
                objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType"); //"L^D^T^T^T^T^T";
                objWebGrid.DropDownColumns = objResourceMgr.GetString("DropDownColumns");//"LOB^^^^^^";
				objWebGrid.OrderByClause		= "REIN_SPLIT_DEDUCTION_ID asc";
				//objWebGrid.DisplayColumnNumbers = "5^4^6^2^3^9^8^11^10";
				//objWebGrid.DisplayColumnNumbers = "5^4^6^9^8^11^10";
				objWebGrid.DisplayColumnNumbers = "6^4^5^9^11^8^10";
				//objWebGrid.DisplayColumnNames	= "REIN_LINE_OF_BUSINESS^REIN_EFFECTIVE_DATE^REIN_STATE^Policy^PremiumSplit^REIN_IST_SPLIT_COVERAGE^REIN_IST_SPLIT^REIN_2ND_SPLIT_COVERAGE^REIN_2ND_SPLIT";
				objWebGrid.DisplayColumnNames	= "REIN_LINE_OF_BUSINESS^REIN_EFFECTIVE_DATE^REIN_STATE^REIN_IST_SPLIT_COVERAGE^REIN_IST_SPLIT^REIN_2ND_SPLIT_COVERAGE^REIN_2ND_SPLIT";
				//objWebGrid.DisplayColumnHeadings= "Line of Business^Effective Date^State^Deduct Policy Assessment/Fees from Coverage^Premium Split Required^1st Split^1st Split %^2nd Split^2nd Split %";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Product^Effective Date^State^1st Split Coverage^1st Split %^2nd Split Coverage^2nd Split %";
				objWebGrid.DisplayTextLength	= "100^100^100^100^100^100^100";
				objWebGrid.DisplayColumnPercent = "13^13^10^22^10^22^10";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "REIN_SPLIT_DEDUCTION_ID";

				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage ");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Splits & Deductions";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="IS_ACTIVE";
				objWebGrid.QueryStringColumns = "REIN_SPLIT_DEDUCTION_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				
				
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "Split.aspx?";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 150;

				# endregion
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion 
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




