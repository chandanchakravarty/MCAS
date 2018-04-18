/******************************************************************************************
<Author				: -   praveen
<Start Date				: -	
<End Date				: -	
<Description				: - 	
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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class  Form1099Index: Cms.Account.AccountBase
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
		protected System.Web.UI.WebControls.DropDownList cmbYEAR_1099;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidYear1099;
		
        private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"427";
			if(!Page.IsPostBack)
			{
				FillCombos();
				hidYear1099.Value = cmbYEAR_1099.SelectedValue.ToString();
			}
			
			
			/* This Security XML has been explicitly specified.
			 * It is done coz, when we go to this page, the ApplicationID session variable has a value.
			 * This in turn checks for a converted Application and then accordingly sets security XML,
			 * resulting in different Permission XML. Refer Support>Appbase.cs (Line no 70)
			*/
			//gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			base.InitializeSecuritySettings();
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
				//objWebGrid.SelectClause = "FORM_1099_ID,isnull(RECIPIENT_IDENTIFICATION,'') as RECIPIENT_IDENTIFICATION,RECIPIENT_NAME ,CONVERT(VARCHAR,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID)),1) as TOTAL_INCOME, ISNULL(RECIPIENT_STREET_ADDRESS1,'') + CASE WHEN ISNULL(RECIPIENT_STREET_ADDRESS2,'') = '' THEN '' ELSE ',' END + ISNULL(RECIPIENT_STREET_ADDRESS2,'') AS RECIPIENT_ADDRESS ,CONVERT(VARCHAR,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID)),1) as TOTAL_INCOME_1";/*LOB_DESC*/
				//Added For Itrack 4947
				//objWebGrid.SelectClause = "FORM_1099_ID,isnull(RECIPIENT_IDENTIFICATION,'') as RECIPIENT_IDENTIFICATION,RECIPIENT_NAME ,CONVERT(VARCHAR,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID)),1) as TOTAL_INCOME,ISNULL(RECIPIENT_STREET_ADDRESS1,'') + CASE WHEN ISNULL(RECIPIENT_STREET_ADDRESS2,'') = '' THEN '' ELSE ',' END + ISNULL(RECIPIENT_STREET_ADDRESS2,'') AS RECIPIENT_ADDRESS ,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID),1) as TOTAL_INCOME_1";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.SelectClause = "FORM_1099_ID,RECIPIENT_NAME ,CONVERT(VARCHAR,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID)),1) as TOTAL_INCOME,ISNULL(RECIPIENT_STREET_ADDRESS1,'') + CASE WHEN ISNULL(RECIPIENT_STREET_ADDRESS2,'') = '' THEN '' ELSE ',' END + ISNULL(RECIPIENT_STREET_ADDRESS2,'') AS RECIPIENT_ADDRESS ,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID),1) as TOTAL_INCOME_1 , ACCOUNT_NO,$DECRYPT_RECIPIENT_IDENTIFICATION";
				objWebGrid.FromClause = "FORM_1099";
				objWebGrid.WhereClause = "YEAR_1099 = " + int.Parse(hidYear1099.Value.ToString().Trim());
				//objWebGrid.SearchColumnHeadings = "Recipient Identification^Recipient Name^Total Income";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				objWebGrid.SearchColumnHeadings = "Recipient Name^Total Income^Account No^Federal Id #";
				//objWebGrid.SearchColumnNames = "RECIPIENT_IDENTIFICATION^RECIPIENT_NAME^CONVERT(VARCHAR,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID)),1)";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.SearchColumnNames = "RECIPIENT_NAME^CONVERT(VARCHAR,CONVERT(MONEY,dbo.fun_GetTotalIncome_1099(FORM_1099_ID)),1)^ACCOUNT_NO^$DECRYPT_6_RECIPIENT_IDENTIFICATION";
				//objWebGrid.SearchColumnType = "T^T^T";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.SearchColumnType = "T^T^T^T^T";
				objWebGrid.OrderByClause = "RECIPIENT_NAME asc";
				//objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.DisplayColumnNumbers = "2^3^4^6^7";
				//objWebGrid.DisplayColumnNames = "RECIPIENT_IDENTIFICATION^RECIPIENT_NAME^RECIPIENT_ADDRESS^TOTAL_INCOME";
				objWebGrid.DisplayColumnNames = "RECIPIENT_NAME^RECIPIENT_ADDRESS^TOTAL_INCOME^ACCOUNT_NO^RECIPIENT_IDENTIFICATION";
				//objWebGrid.DisplayColumnHeadings = "Identification Number^Recipient Name^Recipient Address^Total Income";
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.DisplayColumnHeadings = "Recipient Name^Recipient Address^Total Income^Account No^Federal Id #";
				//objWebGrid.DisplayTextLength = "20^30^30^20";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.DisplayTextLength = "25^25^15^15^20";
				//objWebGrid.DisplayColumnPercent = "30^15^15^15";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.DisplayColumnPercent = "25^25^20^20^30";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "FORM_1099_ID";
				//objWebGrid.ColumnTypes = "B^B^B^N";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.ColumnTypes = "B^B^N^B^B";
				objWebGrid.AllowDBLClick = "true";
				//objWebGrid.FetchColumns = "1^2^3^4^5";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				//ACCOUNT_NO Added For itrack Issue 6854
				objWebGrid.FetchColumns = "1^2^3^4^6";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^ ";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());

                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Form 1099" ;
				objWebGrid.SelectClass = colors;

				//objWebGrid.SelectClass= "";
				//objWebGrid.FilterLabel = "Show Complete";
				//objWebGrid.FilterColumnName = "";
				//objWebGrid.FilterValue = "";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "FORM_1099_ID";
				//Amount Alignment
				//objWebGrid.CellHorizontalAlign = "3";
				//Modified by Sibin on 19 Dec 08 to remove Recipient Identification
				objWebGrid.CellHorizontalAlign = "2";
				objWebGrid.DefaultSearch="Y";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
				string strCalledFrom = "ACT";

				TabCtl.TabURLs = "AddForm1099.aspx?CALLEDFROM=" + strCalledFrom + "&";
			}
			catch(Exception ex)
            {
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

              			
		}

		private void FillCombos()
		{
			try
			{
				Cms.BusinessLayer.BlAccount.ClsAccount.GetForm1099YeardropDown(cmbYEAR_1099);
			}
			catch(Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
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