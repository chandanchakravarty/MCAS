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
	/// Summary description for JournalEntryDetailIndex.
	/// </summary>
	public class JournalEntryDetailIndex : Cms.Account.AccountBase
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
		protected System.Web.UI.WebControls.Label lblGeneralLedger;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJOURNAL_ID;
		string strJOURNAL_ID,strCalledFrom;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//update the cookie to set the row value in another cookie
			Response.Cookies["newRowAddedFlag"].Value = "1";
			
			//fetching the query string
			GetQueryString();

			//fetching the main journal entry information
			ShowJournalMasterInfo();

			//Setting the screen id
			SetScreenId();

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

				objWebGrid.SelectClause = "CASE TYPE " 
					+ " WHEN 'AGN' THEN 'Agency' "
					+ " WHEN 'CUS' THEN 'Customer' "
					+ " WHEN 'OTH' THEN 'Other' "
					+ " WHEN 'VEN' THEN 'Vendor' "
					+ " WHEN 'MOR' THEN 'Mortgage' "
					+ " WHEN 'TAX' THEN 'Tax' "
					+ " END TYPE,"
					+ " CASE TYPE "
					+ " WHEN 'OTH' THEN Convert(varchar,REGARDING) " 
					+ " ELSE IsNull(CUSTOMER_FIRST_NAME,'') + ' ' +IsNull(CUSTOMER_MIDDLE_NAME,'') + ' ' + IsNull(CUSTOMER_LAST_NAME,'') "
					+ "	+ ISNULL(COMPANY_NAME,'') "
					+ "	+ IsNull(AGENCY_DISPLAY_NAME,'')  "
					+ "	+ IsNull(TAX_NAME,'') END As REGARDING,"
					+ "PCL.POLICY_NUMBER + ' ' + PCL.POLICY_DISP_VERSION POLICY_NO, " 
					//+ "ISNULL(Convert(varchar, ACC_DISP_NUMBER),'') + ' : ' + ISNULL(ACC_DESCRIPTION,'') ACCOUNT_ID,"
					+ " ( SELECT  CASE WHEN T1.ACC_PARENT_ID IS NULL   "
					+ " THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    "
					+ " ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  "
					+ " END AS ACC_DESCRIPTION  "
					+ " FROM ACT_GL_ACCOUNTS T1 with(nolock)  " 
					+ " LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 with(nolock) ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   "
					+ " WHERE T1.ACC_LEVEL_TYPE='AS'  AND T1.ACCOUNT_ID= ACCOUNT.ACCOUNT_ID    ) AS ACCOUNT_ID,"
					+ "DEPT_NAME,PC_NAME,convert(varchar(30),convert(money,isnull(LINE_ITEMS.AMOUNT,0)),1) AMOUNT,NOTE,JE_LINE_ITEM_ID,"
					+ "MUL.USER_FNAME + ' ' + MUL.USER_LNAME as CREATED_BY,LINE_ITEMS.CREATED_DATETIME as DATE_CREATED "
					+ " ,convert(money,isnull(LINE_ITEMS.AMOUNT,0)) AMOUNT_1 ";
				
				objWebGrid.FromClause = "ACT_JOURNAL_LINE_ITEMS LINE_ITEMS with(nolock)"
						+ " LEFT JOIN CLT_CUSTOMER_LIST CUST with(nolock) ON Convert(varchar,CUST.CUSTOMER_ID) = REGARDING AND TYPE = 'CUS' "
						+ " LEFT JOIN MNT_AGENCY_LIST AGENCY with(nolock) ON Convert(varchar,AGENCY_ID) = REGARDING AND TYPE = 'AGN' "
						+ " LEFT JOIN MNT_VENDOR_LIST VENDOR with(nolock) ON Convert(varchar,VENDOR_ID) = REGARDING AND TYPE = 'VEN' "
						+ " LEFT JOIN MNT_HOLDER_INTEREST_LIST HOLDER with(nolock) ON Convert(varchar,HOLDER_ID) = REGARDING AND TYPE = 'MOR' "
						+ " LEFT JOIN MNT_TAX_ENTITY_LIST TAX with(nolock) ON Convert(varchar,TAX_ID) = REGARDING AND TYPE = 'TAX' "
						+ " LEFT JOIN MNT_DEPT_LIST DEPT with(nolock) ON  DEPT.DEPT_ID = LINE_ITEMS.DEPT_ID "
						+ " LEFT JOIN ACT_GL_ACCOUNTS ACCOUNT with(nolock) ON  ACCOUNT.ACCOUNT_ID = LINE_ITEMS.ACCOUNT_ID "
						+ " LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL with(nolock) ON PCL.CUSTOMER_ID = LINE_ITEMS.REF_CUSTOMER AND PCL.POLICY_ID = LINE_ITEMS.POLICY_ID AND PCL.POLICY_VERSION_ID = LINE_ITEMS.POLICY_VERSION_ID"
						+ " LEFT JOIN MNT_PROFIT_CENTER_LIST PC with(nolock) ON  PC.PC_ID = LINE_ITEMS.PC_ID "
						+ " INNER JOIN MNT_USER_LIST MUL with(nolock) ON  MUL.[USER_ID] = LINE_ITEMS.CREATED_BY ";
				
				objWebGrid.WhereClause = "JOURNAL_ID=" + strJOURNAL_ID;
				
				
				objWebGrid.SearchColumnHeadings = "Type^Regarding^Policy #^Account #^Amount^Note^Created By^ Date Created";
				objWebGrid.SearchColumnNames = "CASE TYPE " 
					+ " WHEN 'AGN' THEN 'Agency' "
					+ " WHEN 'CUS' THEN 'Customer' "
					+ " WHEN 'OTH' THEN 'Other' "
					+ " WHEN 'VEN' THEN 'Vendor' "
					+ " WHEN 'MOR' THEN 'Mortgage' "
					+ " WHEN 'TAX' THEN 'Tax' "
					+ " END^"
					+ "CASE TYPE WHEN 'OTH' THEN Convert(varchar,REGARDING) ELSE IsNull(CUSTOMER_FIRST_NAME,'') ! ' ' !IsNull(CUSTOMER_MIDDLE_NAME,'') ! ' ' ! IsNull(CUSTOMER_LAST_NAME,'')"
					+ "!ISNULL(COMPANY_NAME,'') "
					+ "!IsNull(AGENCY_DISPLAY_NAME,'')  "
					+ "!IsNull(TAX_NAME,'') END^PCL.POLICY_NUMBER ! ' ' ! PCL.POLICY_DISP_VERSION^ISNULL(Convert(varchar, ACC_DISP_NUMBER),'') ! ' : ' ! ISNULL(ACC_DESCRIPTION,'')^CONVERT(VARCHAR,CONVERT(MONEY,AMOUNT),1)^NOTE^MUL.USER_FNAME ! ' ' ! MUL.USER_LNAME^LINE_ITEMS.CREATED_DATETIME";
				objWebGrid.SearchColumnType = "T^T^T^T^T^T^T^D";
				
				//objWebGrid.OrderByClause = "JE_LINE_ITEM_ID DESC";
				objWebGrid.OrderByClause = "JE_LINE_ITEM_ID";
				
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^7^8^14^15";
				objWebGrid.DisplayColumnNames = "TYPE^REGARDING^POLICY_NO^ACCOUNT_ID^AMOUNT^NOTE^CREATED_BY^DATE_CREATED";
				objWebGrid.DisplayColumnHeadings = "Type^Regarding^Policy #^Account #^Amount^Note^Created By^Date Created";

				objWebGrid.DisplayTextLength = "10^15^10^20^10^15^10^10";
				objWebGrid.DisplayColumnPercent = "10^15^10^20^10^15^10^10";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "JE_LINE_ITEM_ID";

				//Modified by Asfa(18-june-2008) - iTrack #3906(Note: 1)
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "B^B^B^B^N^B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons = "2^Add New~Back^0~2^addRecord~back";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Journal Entries Details";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "JE_LINE_ITEM_ID";
				objWebGrid.CellHorizontalAlign = "4";
						
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);

				TabCtl.TabURLs = "AddJournalEntryDetail.aspx?JOURNAL_ID=" + strJOURNAL_ID + "&CalledFrom=" + strCalledFrom + "&";
			}
			catch
			{
			}
			#endregion

			
		}

		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			strJOURNAL_ID = Request.Params["JOURNAL_ID"];
			hidJOURNAL_ID.Value=strJOURNAL_ID;
			if(Request.QueryString["CalledFrom"]!=null && Request.QueryString["CalledFrom"].ToString()!="")
				strCalledFrom =  Request.QueryString["CalledFrom"].ToString().ToUpper();

	}

		/// <summary>
		/// Show the details of master record on header band
		/// </summary>
		private void ShowJournalMasterInfo()
		{
			try
			{
				string strXml = Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster.GetJournalEntryInfo(int.Parse(strJOURNAL_ID));
				hidJournalInfoXML.Value = strXml;

			}
			catch (Exception objExp)
			{
				capMessage.Text = objExp.Message.ToString();
				capMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

		/// <summary>
		/// Sets the screen id of screen
		/// </summary>
		private void SetScreenId()
		{
			try
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml (hidJournalInfoXML.Value);
				string strType = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//JOURNAL_GROUP_TYPE");

				switch(strType.ToUpper())
				{
					case "ML":
						base.ScreenId = "173_1";
						break;
					case "RC":
						base.ScreenId = "175_1";
						break;
					case "TP":
						base.ScreenId = "174_1";
						break;
					case "13":
						base.ScreenId = "176_1";
						break;
				}


			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				capMessage.Text = objExp.Message.ToString();
				capMessage.Visible = true;
			}

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
