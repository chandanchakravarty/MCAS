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
using Cms.Model.Account;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for JournalEntryIndex.
	/// </summary>
	public class JournalEntryMasterIndex : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;		
		//Holds the property setting for extrabuttonhanler of grid
		//to be set in GetQueryString(), function
		string strExtraButtonProp;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGridRowClickNumber;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGridRowClickMsg;

		private string strCalledFrom;
		private void Page_Load(object sender, System.EventArgs e)
		{
			//update the cookie to set the row value in another cookie
			if (Request.Cookies["newRowAddedFlag"] == null)
			{
				HttpCookie objCookie = new HttpCookie("newRowAddedFlag");
				objCookie.Expires = DateTime.MaxValue;
				Response.Cookies.Add(objCookie);
			}

			Response.Cookies["newRowAddedFlag"].Value = "0";
			
			//base.ScreenId = "910";

			capMessage.Text = "";
			GetQueryString();

			

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
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "Convert(numeric,JOURNAL_ENTRY_NO) JOURNAL_ENTRY_NO,Convert(varchar,ACT_JOURNAL_MASTER.CREATED_DATETIME,109) ajm,Convert(Varchar,TRANS_DATE,101) TRANS_DATE,"
					+ "CASE WHEN Len(DESCRIPTION)>28 THEN Substring(DESCRIPTION,0,28) + '...' ELSE DESCRIPTION END DESCRIPTION,convert(varchar(30),convert(money,SUM(IsNull(ACT_JOURNAL_LINE_ITEMS.AMOUNT,0))),1) PROFF, "
					+ "ACT_JOURNAL_MASTER.JOURNAL_ID,ACT_JOURNAL_MASTER.IS_ACTIVE,	Convert(Varchar,START_DATE,101) START_DATE,	Convert(Varchar,END_DATE,101) END_DATE,	FREQUENCY_DESCRIPTION,	Convert(Varchar,LAST_VALID_POSTING_DATE,101) LAST_VALID_POSTING_DATE,NO_OF_RUN, "
					+ "convert(money,SUM(IsNull(ACT_JOURNAL_LINE_ITEMS.AMOUNT,0))) PROFF_1 " ;

				objWebGrid.GroupByClause = "ACT_JOURNAL_MASTER.JOURNAL_ID,ACT_JOURNAL_MASTER.CREATED_DATETIME,JOURNAL_ENTRY_NO,TRANS_DATE," 
					+ "DESCRIPTION,ACT_JOURNAL_MASTER.JOURNAL_ID,ACT_JOURNAL_MASTER.IS_ACTIVE , START_DATE,END_DATE,FREQUENCY_DESCRIPTION,LAST_VALID_POSTING_DATE,NO_OF_RUN";

				objWebGrid.FromClause = "ACT_JOURNAL_MASTER with(nolock)" 
					+ " LEFT JOIN ACT_JOURNAL_LINE_ITEMS with(nolock) ON ACT_JOURNAL_MASTER.JOURNAL_ID = ACT_JOURNAL_LINE_ITEMS.JOURNAL_ID LEFT JOIN ACT_FREQUENCY_MASTER with(nolock) 	ON ACT_JOURNAL_MASTER.FREQUENCY=ACT_FREQUENCY_MASTER.FREQUENCY_CODE";

				
				
				objWebGrid.WhereClause = "IsNull(IS_COMMITED,'')<>'Y' AND IsNull(JOURNAL_GROUP_TYPE,'') = '" + GetGroupType() + "'";
				
				
				objWebGrid.SearchColumnHeadings = "Entry Number^Date Created^Transaction Date^Description";
				objWebGrid.SearchColumnNames = "JOURNAL_ENTRY_NO^ACT_JOURNAL_MASTER.CREATED_DATETIME^TRANS_DATE^DESCRIPTION";
				objWebGrid.SearchColumnType = "T^D^D^T";
				
				//objWebGrid.OrderByClause = "Convert(numeric,JOURNAL_ENTRY_NO) ASC";
				objWebGrid.OrderByClause = "JOURNAL_ENTRY_NO ASC";

				
				
				// In case of recurring only 
				if(strCalledFrom.ToUpper().ToString()=="RECURR")
				{
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^8^9^10^11^12";
					objWebGrid.DisplayColumnNames = "JOURNAL_ENTRY_NO^ajm^TRANS_DATE^DESCRIPTION^PROFF^START_DATE^END_DATE^FREQUENCY_DESCRIPTION^LAST_VALID_POSTING_DATE^NO_OF_RUN";
					objWebGrid.DisplayColumnHeadings = "Entry Number^Date Created^Transaction Date^Description^Proof^Start Date^End Date^Frequency^Last Processed^Number of Run";
					objWebGrid.DisplayTextLength = "10^10^10^10^10^10^10^10^10^10";
					objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10^10^10^10^10";
					// Added by Asfa(17-June-2008) - iTrack #3906(Note:1)
					//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B^B";
					objWebGrid.ColumnTypes = "B^B^B^B^N^B^B^B^B^B";
					objWebGrid.FetchColumns = "1^2^3^4^5^8^9^10^11^12";					
					
					objWebGrid.FilterLabel = "Show InActive";
					objWebGrid.FilterColumnName = "ACT_JOURNAL_MASTER.IS_ACTIVE";
					objWebGrid.FilterValue = "Y";
				}
				else
				{
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
					objWebGrid.DisplayColumnNames = "JOURNAL_ENTRY_NO^ajm^TRANS_DATE^DESCRIPTION^PROFF";
					objWebGrid.DisplayColumnHeadings = "Entry Number^Date Created^Transaction Date^Description^Proof";
					objWebGrid.DisplayTextLength = "20^20^20^20^20";
					objWebGrid.DisplayColumnPercent = "20^20^20^20^20";
					// Added by Asfa(17-June-2008) - iTrack #3906(Note:1)
					objWebGrid.ColumnTypes = "B^B^B^B^N";
					objWebGrid.FetchColumns = "1^2^3^4^5^6";
				}

				objWebGrid.PrimaryColumns = "6";
				objWebGrid.PrimaryColumnsName = "ACT_JOURNAL_MASTER.JOURNAL_ID";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = strExtraButtonProp;
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = GetGridHeader();
				objWebGrid.SelectClass = colors;
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "JOURNAL_ID";	
				objWebGrid.CellHorizontalAlign ="4";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
				TabCtl.TabURLs = "AddJournalEntryMaster.aspx?GROUP_TYPE=" + GetGroupType() + "&CalledFrom=" + strCalledFrom.ToUpper() + "&";
			}
			catch
			{
			}
			#endregion
	

		}
		
		/// <summary>
		/// Get query string from url 
		/// </summary>
		private void GetQueryString()
		{
			strCalledFrom = Request.Params["CalledFrom"];
			if ( strCalledFrom == null)
				strCalledFrom = "";

			//setting different property of grid
			//Setting the screen id also
			switch(strCalledFrom.ToUpper())
			{
				case "TMPLT":
					strExtraButtonProp = "2^Add New~Print Preview^0~2^addRecord~OnPreview";
					
					//Setting the screen id
					base.ScreenId = "174_0";
					break;
				case "RECURR":
					//strExtraButtonProp = "3^Add New~Commit~Print Preview^0~1~2^addRecord~OnCommit~OnPreview";
					strExtraButtonProp = "2^Add New~Print Preview^0~1^addRecord~OnPreview";
					//Setting the screen id
					base.ScreenId = "175_0";
					break;
				case "13PER":
					strExtraButtonProp = "2^Add New~Print Preview^0~1^addRecord~OnPreview";

					//Setting the screen id
					base.ScreenId = "176_0";
					break;
				default:
					strExtraButtonProp = "2^Add New~Print Preview^0~1^addRecord~OnPreview";

					//Setting the screen id
					base.ScreenId = "173_0";
					break;
			}
		}

		/// <summary>
		/// Makes the grid header string
		/// </summary>
		/// <returns>Header string for grid</returns>
		private string GetGridHeader()
		{
			string strRetVal;
			switch(strCalledFrom.ToUpper())
			{
				case "TMPLT":
					strRetVal = "Journal Entries Template";
					break;
				case "RECURR":
					strRetVal = "Journal Entries Recurring";
					break;
				case "13PER":
					strRetVal = "Journal Entries 13th Period";
					break;
				default:
					strRetVal = "Journal Entries Manual";
					break;
			}
			return strRetVal;

		}

		/// <summary>
		/// Makes the grid header string
		/// </summary>
		/// <returns>Header string for grid</returns>
		private string GetGroupType()
		{
			string strRetVal;
			switch(strCalledFrom.ToUpper())
			{
				case "TMPLT":
					strRetVal = "TP";
					break;
				case "RECURR":
					strRetVal = "RC";
					break;
				case "13PER":
					strRetVal = "13";
					break;
				default:
					strRetVal = "ML";
					break;
			}
			return strRetVal;
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

