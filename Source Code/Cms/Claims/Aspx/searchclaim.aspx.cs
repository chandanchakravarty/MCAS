/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		27-04-2006
<End Date			: -	
<Description		: - 	Index Page for Claims.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance; 
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;



namespace Cms.Claims.Aspx
{

	/// <summary>
	/// 
	/// </summary>
	public class SearchClaim : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Literal litTextGrid;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		public string strSecurity = "";
        ResourceManager objResourceMgr = null;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			#region Setting screen id
			base.ScreenId	=	"303";
			#endregion
			//Set Security XML
			strSecurity = gstrSecurityXML;


			

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion         
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.SearchClaim", Assembly.GetExecutingAssembly());	
			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				
				string rootPath=System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
				string sSELECTCLAUSE="", sFROMCLAUSE="";
                sSELECTCLAUSE = " ISNULL(CONVERT (NVARCHAR(10),APP_EFFECTIVE_DATE, CASE WHEN " + ClsCommon.BL_LANG_ID + " = 2 THEN 103 ELSE 101 END),'') APP_EFFECTIVE_DATE,ISNULL(CONVERT (NVARCHAR(10),APP_EXPIRATION_DATE, CASE WHEN " + ClsCommon.BL_LANG_ID + " = 2 THEN 103 ELSE 101 END),'') APP_EXPIRATION_DATE,* ";
				/*sFROMCLAUSE  = " ( SELECT CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID,CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,MLM.LOB_DESC, CASE CLAIMANT_INSURED WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS CLAIMANT_INSURED, " + 
								" substring(convert(varchar(30),convert(money,PAID_LOSS),1),0,charindex('.',convert(varchar(30),convert(money,PAID_LOSS),1),0)) PAID_LOSS, " + 
								" substring(convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0)) OUTSTANDING_RESERVE, " + 
								" substring(convert(varchar(30),convert(money,RECOVERIES),1),0,charindex('.',convert(varchar(30),convert(money,RECOVERIES),1),0)) RECOVERIES, " + 
								" substring(convert(varchar(30),convert(money,RESINSURANCE_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,RESINSURANCE_RESERVE),1),0)) RESINSURANCE_RESERVE, " + 
								" substring(convert(varchar(30),convert(money,PAID_EXPENSE),1),0,charindex('.',convert(varchar(30),convert(money,PAID_EXPENSE),1),0)) PAID_EXPENSE, " + 
								//" PAID_LOSS,OUTSTANDING_RESERVE,RECOVERIES,CLAIMANT_NAME,RESINSURANCE_RESERVE,PAID_EXPENSE, " + 
								" CLAIMANT_NAME,CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, CTD.DETAIL_TYPE_ID,ISNULL(CCI.HOMEOWNER,0) HOMEOWNER,ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE  FROM CLM_CLAIM_INFO CCI LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID  ) Test ";*/

                //sFROMCLAUSE  = " ( SELECT CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER,isnull(MLV.LOOKUP_VALUE_DESC, '') + '('+ isnull(MLV2.LOOKUP_VALUE_DESC, '') + ')' AS CLAIM_STATUS,CASE WHEN ISNULL(PCPL.POLICY_NUMBER,'')='' THEN CDP.POLICY_NUMBER ELSE PCPL.POLICY_NUMBER END AS POLICY_NUMBER,ISNULL(CP.[NAME],'') AS NAME,ISNULL(CTDP.DETAIL_TYPE_DESCRIPTION,'') AS TYPE, " + 
                //            //" ( SELECT CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,ISNULL(CP.[NAME],'') AS NAME,ISNULL(CTDP.DETAIL_TYPE_DESCRIPTION,'') AS TYPE, " +  -- Commented by Asfa (15-Jan-2008) - iTrack issue #3356
                //            //" substring(convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0)) AS OUTSTANDING, " + 
                //            " convert(varchar(30),convert(money,isnull( OUTSTANDING_RESERVE,0)),1) AS OUTSTANDING, " + 
                //            //" substring(convert(varchar(30),convert(money,PAID_LOSS),1),0,charindex('.',convert(varchar(30),convert(money,PAID_LOSS),1),0)) AS PAID, " + 
                //            " convert(varchar(30),convert(money,isnull( PAID_LOSS,0)),1) AS PAID, " + 
                //            //" substring(convert(varchar(30),convert(money,PAID_EXPENSE),1),0,charindex('.',convert(varchar(30),convert(money,PAID_EXPENSE),1),0)) AS EXPENSE, " + 
                //            " convert(varchar(30),convert(money,isnull( PAID_EXPENSE,0)),1) AS EXPENSE, " + 
                //            //" substring(convert(varchar(30),convert(money,RECOVERIES),1),0,charindex('.',convert(varchar(30),convert(money,RECOVERIES),1),0)) RECOVERIES, " +	
                //            " convert(varchar(30),convert(money,isnull( RECOVERIES,0)),1) AS RECOVERIES, " +
                //            " CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CCI.CLAIM_ID, " + 
                //            " CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, CTD.DETAIL_TYPE_ID,ISNULL(CCI.HOMEOWNER,0) HOMEOWNER, " + 
                //            " ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE  " + 
                //            " ,convert(money,isnull(OUTSTANDING_RESERVE,0)) AS OUTSTANDING_1 " + 
                //            " ,convert(money,isnull( PAID_LOSS,0)) AS PAID_1 " + 
                //            " ,convert(money,isnull( PAID_EXPENSE,0)) AS EXPENSE_1 " + 
                //            " ,convert(money,isnull( RECOVERIES,0)) AS RECOVERIES_1 " +
                //            " , LOSS_DATE AS LOSS_DATE_1 " + 	
                //            " FROM CLM_CLAIM_INFO CCI " + 
                //            " LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID " + 
                //            " AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID " + 
                //            " LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID " + 
                //            //Asfa (21-Apr-2008) - iTrack issue #4065 -- 2. In claim search grid, status column should be based on Claim Status Under.
                //            //" LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID " + 
                //            " LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS_UNDER = MLV.LOOKUP_UNIQUE_ID " + 
                //            " LEFT JOIN MNT_LOOKUP_VALUES MLV2 ON CCI.CLAIM_STATUS = MLV2.LOOKUP_UNIQUE_ID " + 
                //            " LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID " + 
                //            " left join CLM_PARTIES CP on CP.CLAIM_ID = CCI.CLAIM_ID " +
                //            //" LEFT JOIN CLM_TYPE_DETAIL CTDP ON CP.PARTY_TYPE_ID = CTDP.DETAIL_TYPE_ID) Test "; -- Commented by Asfa (15-Jan-2008) - iTrack issue #3356
                //            " LEFT JOIN CLM_TYPE_DETAIL CTDP ON CP.PARTY_TYPE_ID = CTDP.DETAIL_TYPE_ID LEFT JOIN CLM_DUMMY_POLICY CDP ON CDP.DUMMY_POLICY_ID = CCI.DUMMY_POLICY_ID where CP.IS_ACTIVE=CTDP.IS_ACTIVE) Test ";//Added for Itrack Issue 5842 on 15 May 2009

                // Added by Santosh Kumar Gautam on 18 dec 2010
                int LangID = int.Parse(GetLanguageID());

                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;

                sFROMCLAUSE = " ( SELECT ISNULL(Convert(varchar,LOSS_DATE,case when " + LangID + "=2 then 103 else 101 end),'') AS LOSS_DATE, " +
                               " CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER, " +
                               " isnull(isnull(N1.LOOKUP_VALUE_DESC, MLV.LOOKUP_VALUE_DESC),'') + '('+ isnull(isnull(N2.LOOKUP_VALUE_DESC,MLV2.LOOKUP_VALUE_DESC),'') + ')' AS CLAIM_STATUS, " +
                               " CASE WHEN ISNULL(PCPL.POLICY_NUMBER,'')=''  THEN   CDP.POLICY_NUMBER ELSE PCPL.POLICY_NUMBER END AS POLICY_NUMBER, " +
                               " ISNULL(CP.[NAME],'') AS NAME,ISNULL(ISNULL(N3.TYPE_DESC, CTDP.DETAIL_TYPE_DESCRIPTION),'') AS TYPE,  " +
                                //" convert(varchar(30),convert(money,isnull( OUTSTANDING_RESERVE,0)),1) AS OUTSTANDING,  " +
                               " dbo.fun_FormatCurrency (ISNULL(OUTSTANDING_RESERVE,0)," + BaseCurrency + ") AS  OUTSTANDING, " +
                              //" convert(varchar(30),convert(money,isnull( PAID_LOSS,0)),1) AS PAID,  " +
                               " dbo.fun_FormatCurrency (ISNULL(PAID_LOSS,0)," + BaseCurrency + ") AS  PAID, " +
                                //" convert(varchar(30),convert(money,isnull( PAID_EXPENSE,0)),1) AS EXPENSE,  " +
                               " dbo.fun_FormatCurrency (ISNULL(PAID_EXPENSE,0)," + BaseCurrency + ") AS  EXPENSE, " +
                                //" convert(varchar(30),convert(money,isnull( RECOVERIES,0)),1) AS RECOVERIES,  " +
                                " dbo.fun_FormatCurrency (ISNULL(RECOVERIES,0)," + BaseCurrency + ") AS  RECOVERIES, " +
                               " CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CCI.CLAIM_ID,  " +
                               " CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE," +
                               " CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, " +
                               " CTD.DETAIL_TYPE_ID,ISNULL(CCI.HOMEOWNER,0) HOMEOWNER, " +
                               " ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE   , " +
                    // " convert(money,isnull(OUTSTANDING_RESERVE,0)) AS OUTSTANDING_1  " +
                               " dbo.fun_FormatCurrency (ISNULL(OUTSTANDING_RESERVE,0)," + BaseCurrency + ") AS  OUTSTANDING_1, " +
                    //" ,convert(money,isnull( PAID_LOSS,0)) AS PAID_1  ,convert(money,isnull( PAID_EXPENSE,0)) AS EXPENSE_1  , " +
                               " dbo.fun_FormatCurrency (ISNULL(PAID_LOSS,0)," + BaseCurrency + ") AS  EXPENSE_1, " +
                    //" convert(money,isnull( RECOVERIES,0)) AS RECOVERIES_1  , " +\
                               " dbo.fun_FormatCurrency (ISNULL(RECOVERIES,0)," + BaseCurrency + ") AS  RECOVERIES_1, " +
                               " LOSS_DATE AS LOSS_DATE_1, " +
                               " ISNULL(CAL.FIRST_NAME ,'') + ' ' + ISNULL(CAL.MIDDLE_NAME,'')+' '+ISNULL(CAL.LAST_NAME,'') AS CO_APPLICANT_NAME, " +
                               " POL_VER_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE, " +  
		                       " POL_VER_EXPIRATION_DATE AS APP_EXPIRATION_DATE, " +
                               " CCI.LEADER_CLAIM_NUMBER, " + 
                               " R.ACTIVITY_STATUS, " + 
                               " ISNULL(N4.LOOKUP_VALUE_DESC,MLV3.LOOKUP_VALUE_DESC) AS ACTIVITY_STATUS_DESC " + 
                               " FROM CLM_CLAIM_INFO CCI  " +
                               //" LEFT OUTER JOIN CLM_ACTIVITY CA ON CCI.CLAIM_ID = CA.CLAIM_ID " +
                               " LEFT JOIN (SELECT AC.ACTIVITY_STATUS as ACTIVITY_STATUS,AC.CLAIM_ID  FROM CLM_ACTIVITY AC WITH(NOLOCK) INNER JOIN (SELECT MAX(ACTIVITY_ID) AS ACTIVITY_ID ,CLAIM_ID  FROM CLM_ACTIVITY WITH(NOLOCK) GROUP BY CLAIM_ID) AS T ON AC.CLAIM_ID = T.CLAIM_ID  AND AC.ACTIVITY_ID = T.ACTIVITY_ID) AS R ON CCI.CLAIM_ID = R.CLAIM_ID " +
                               " LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID  AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  " +
                               " LEFT JOIN MNT_LOB_MASTER MLM WITH(NOLOCK) ON PCPL.POLICY_LOB = MLM.LOB_ID  " +
                               " LEFT JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON CCI.CLAIM_STATUS_UNDER = MLV.LOOKUP_UNIQUE_ID  " +
                               " LEFT JOIN MNT_LOOKUP_VALUES MLV2 WITH(NOLOCK) ON CCI.CLAIM_STATUS = MLV2.LOOKUP_UNIQUE_ID  " +
                               " LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV3 WITH(NOLOCK) ON R.ACTIVITY_STATUS = MLV3.LOOKUP_UNIQUE_ID " +
                               " LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL N4 WITH(NOLOCK) ON  R.ACTIVITY_STATUS = N4.LOOKUP_UNIQUE_ID AND N4.LANG_ID = " + LangID +
                               " LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL N1 WITH(NOLOCK) ON CCI.CLAIM_STATUS_UNDER = N1.LOOKUP_UNIQUE_ID  AND N1.LANG_ID= " + LangID +
                               " LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL N2 WITH(NOLOCK) ON CCI.CLAIM_STATUS = N2.LOOKUP_UNIQUE_ID  AND N2.LANG_ID=" + LangID +
                               " LEFT JOIN CLM_TYPE_DETAIL CTD WITH(NOLOCK) ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID   " +
                               " left join CLM_PARTIES CP WITH(NOLOCK) on CP.CLAIM_ID = CCI.CLAIM_ID   " +
                               " LEFT JOIN CLM_TYPE_DETAIL CTDP WITH(NOLOCK) ON CP.PARTY_TYPE_ID = CTDP.DETAIL_TYPE_ID  " +
                               " LEFT JOIN CLM_TYPE_DETAIL_MULTILINGUAL N3 WITH(NOLOCK) ON CP.PARTY_TYPE_ID = N3.DETAIL_TYPE_ID AND N3.LANG_ID=" + LangID +
                               " LEFT JOIN CLM_DUMMY_POLICY CDP WITH(NOLOCK) ON CDP.DUMMY_POLICY_ID = CCI.DUMMY_POLICY_ID " +
                               " LEFT JOIN CLM_INSURED_PRODUCT CIP WITH(NOLOCK) ON CIP.CLAIM_ID = CCI.CLAIM_ID " +       //comment added by aditya,changed for itrack # 1295
                               " LEFT OUTER JOIN POL_APPLICANT_LIST PAL WITH(NOLOCK) ON PAL.CUSTOMER_ID=PCPL.CUSTOMER_ID  AND PAL.POLICY_ID = PCPL.POLICY_ID AND PAL.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND CIP.RISK_CO_APP_ID = PAL.APPLICANT_ID " +
                               " LEFT OUTER JOIN CLT_APPLICANT_LIST CAL WITH(NOLOCK) ON CAL.CUSTOMER_ID= PAL.CUSTOMER_ID AND CAL.APPLICANT_ID= PAL.APPLICANT_ID " +
                               " where CP.IS_ACTIVE=CTDP.IS_ACTIVE AND CTDP.DETAIL_TYPE_ID=10 )TEST ";
               
               
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				//objWebGrid.SearchColumnHeadings = "Insured^Date of Loss^Claim #^Status^Policy #";	
				//objWebGrid.SearchColumnHeadings = "Insured^Date of Loss^Claim #^Status^Policy #^Party Name";	
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Name^Date of Loss^Claim #^Status^Policy #";	
				//objWebGrid.SearchColumnNames = "INSURED^LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^NAME";
                objWebGrid.SearchColumnNames = "NAME^LOSS_DATE_1^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^CO_APPLICANT_NAME^APP_EFFECTIVE_DATE^APP_EXPIRATION_DATE^LEADER_CLAIM_NUMBER^ACTIVITY_STATUS";
				//objWebGrid.DropDownColumns          =   "^^^^^LOB^ctet";
				objWebGrid.SearchColumnType			=	"T^D^T^T^T^T^D^D^T^L";
                objWebGrid.DropDownColumns = "^^^^^^^^^ACTIVITYSTATUS";
				//objWebGrid.DisplayColumnHeadings = "Date of Loss^Claim #^Status^Policy #^Insured^Outstanding^Paid^Expense^Recovery";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Date of Loss^Claim #^Status^Policy #^Name^Type^Outstanding^Paid^Expense^Recovery";
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9^10^11^12^13";
                objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^NAME^TYPE^OUTSTANDING^PAID^EXPENSE^RECOVERIES^CO_APPLICANT_NAME^APP_EFFECTIVE_DATE^APP_EXPIRATION_DATE^LEADER_CLAIM_NUMBER";
				objWebGrid.DisplayTextLength = "10^10^10^10^10^10^10^10^10^10^10^10^10^10";
				objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10^10^10^10^10^10^10^10^10";
				objWebGrid.PrimaryColumns = "13";
				objWebGrid.PrimaryColumnsName = "CLAIM_ID";

				//Modified by Asfa(18-june-2008) - iTrack #3906(Note: 1)
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^N^N^N^N^B^B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Claim Search" ;
                objWebGrid.OrderByClause = "LOSS_DATE  DESC";
				//objWebGrid.OrderByClause = " NAME ";
				objWebGrid.ColumnsLink= rootPath + "claims/aspx/ClaimsNotificationIndex.aspx?"; 
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";

                if (ClsCommon.CheckReadSecurity(strSecurity))
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New Claim^0^addNewClaim";	
										
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
                objWebGrid.QueryStringColumns = "CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^CLAIM_ID^LOB_ID^HOMEOWNER^RECR_VEH^IN_MARINE";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
                objWebGrid.CellHorizontalAlign = "6^7^8^9";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch (Exception ex)
			{throw (ex);}
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