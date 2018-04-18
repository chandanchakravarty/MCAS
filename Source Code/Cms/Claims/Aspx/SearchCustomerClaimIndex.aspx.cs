/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		22-06-2006
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
	public class SearchCustomerClaimIndex : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWolverineUser;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
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
        ResourceManager objResourceMgr = null;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.SearchCustomerClaimIndex", Assembly.GetExecutingAssembly());
            SetCookieValue();
			#region Setting screen id
			//base.ScreenId	=	"303";
			base.ScreenId	=	"120_3"; //Changed by Sibin on 09-10-08
			#endregion


			//Clear claims session values
			//Commented because this will result in Blank session and if user migrate to Application Info 
			//or policy info from this tab that will result in crash as these pages pick value from session for
			//Customer/App/Policy IDs
			//base.ClearClaimsSessionValues();

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
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{

				GetQueryStringValues();

                string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE= " * ";
				//				sFROMCLAUSE  = " ( SELECT CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID,CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,MLM.LOB_DESC, CASE CLAIMANT_INSURED WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS CLAIMANT_INSURED,PAID_LOSS,OUTSTANDING_RESERVE,RECOVERIES,CLAIMANT_NAME,RESINSURANCE_RESERVE,PAID_EXPENSE, CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, CTD.DETAIL_TYPE_ID,ISNULL(CCI.HOMEOWNER,0) HOMEOWNER,ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE  FROM CLM_CLAIM_INFO CCI LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID WHERE CCI.CUSTOMER_ID = " + Request["CustomerID"].ToString() + " ) Test ";
				//
				//				objWebGrid.WebServiceURL = "http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//				objWebGrid.SelectClause = sSELECTCLAUSE;
				//				objWebGrid.FromClause = sFROMCLAUSE ; 
				//				objWebGrid.SearchColumnHeadings = "Date of Loss^Claim #^Claim Status^Policy #^Insured^LOB";	
				//				objWebGrid.SearchColumnNames = "CLAIMANT_NAME^CLAIM_NUMBER^LOSS_DATE^POLICY_NUMBER^CLAIMANT_INSURED^lob_id";
				//				objWebGrid.DropDownColumns          =   "^^^^^LOB";
				//				objWebGrid.SearchColumnType			=	"T^T^T^T^T^L";
				//				objWebGrid.DisplayColumnHeadings = "Date of Loss^Claim #^Claim Status^Policy #^Insured/Claimant^LOB";
				//				objWebGrid.DisplayColumnNumbers = "5^6^7^8^10^9";
				//				objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^CLAIMANT_INSURED^LOB_DESC";
				//				objWebGrid.DisplayTextLength = "10^10^10^10^10^10";
				//				objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10";
				//				objWebGrid.PrimaryColumns = "4";
				//				objWebGrid.PrimaryColumnsName = "CLAIM_ID";
				//				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				//				objWebGrid.HeaderString ="Claim Search" ;
				//				objWebGrid.OrderByClause = "CLAIM_ID asc";
				//				objWebGrid.ColumnsLink= rootPath + "claims/aspx/ClaimsNotificationIndex.aspx?"; 
				//				objWebGrid.AllowDBLClick = "true";
				//				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16";
				//				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//				//objWebGrid.ExtraButtons = "1^Add New Claim^0^addNewClaim";											
				//				objWebGrid.PageSize = int.Parse (GetPageSize());
				//				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				//				objWebGrid.ImagePath = System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				//				objWebGrid.HImagePath = System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
				//				objWebGrid.SelectClass = colors;				
				//				objWebGrid.FilterLabel = "Include Inactive";				 
				//				objWebGrid.RequireQuery = "Y";a
				//				objWebGrid.QueryStringColumns = "CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^CLAIM_ID^LOB_ID^HOMEOWNER^RECR_VEH^IN_MARINE";
				//				objWebGrid.DefaultSearch = "Y";
				//				objWebGrid.FilterColumnName = "IS_ACTIVE";
				//				objWebGrid.FilterValue = "Y";
				
				/* Commented by Asfa(30-June-2008) - iTrack #4406
				 
				sFROMCLAUSE  = " ( SELECT CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,CCI.CLAIMANT_NAME AS INSURED, " + 
					" substring(convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0)) AS OUTSTANDING, " + 
					" substring(convert(varchar(30),convert(money,PAID_LOSS),1),0,charindex('.',convert(varchar(30),convert(money,PAID_LOSS),1),0)) AS PAID, " + 
					" substring(convert(varchar(30),convert(money,PAID_EXPENSE),1),0,charindex('.',convert(varchar(30),convert(money,PAID_EXPENSE),1),0)) AS EXPENSE, " + 
					" substring(convert(varchar(30),convert(money,RECOVERIES),1),0,charindex('.',convert(varchar(30),convert(money,RECOVERIES),1),0)) RECOVERIES, " +
					" substring(convert(varchar(30),convert(money,((ISNULL(OUTSTANDING_RESERVE,0) + ISNULL(PAID_LOSS,0))- ISNULL(RECOVERIES,0))),1),0,charindex('.',convert(varchar(30),convert(money,((ISNULL(OUTSTANDING_RESERVE,0) + ISNULL(PAID_LOSS,0))- ISNULL(RECOVERIES,0))),1),0))  AS INCURRED, " +  
					" CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID, " + 
					" CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, CTD.DETAIL_TYPE_ID,ISNULL(CCI.HOMEOWNER,0) HOMEOWNER, " + 
					" ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE, MLM.LOB_DESC AS LOB_DESC " + 
					" ,ADJ.ADJUSTER_ID,ADJ.ADJUSTER_NAME as ADJUSTER " +
					" ,convert(money,substring(convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0))) AS OUTSTANDING_1 " +
					" ,convert(money,substring(convert(varchar(30),convert(money,PAID_LOSS),1),0,charindex('.',convert(varchar(30),convert(money,PAID_LOSS),1),0))) AS PAID_1 " +
					" ,convert(money,substring(convert(varchar(30),convert(money,PAID_EXPENSE),1),0,charindex('.',convert(varchar(30),convert(money,PAID_EXPENSE),1),0))) AS EXPENSE_1 " +
					" ,convert(money,substring(convert(varchar(30),convert(money,RECOVERIES),1),0,charindex('.',convert(varchar(30),convert(money,RECOVERIES),1),0))) RECOVERIES_1 " +
					" ,convert(money,substring(convert(varchar(30),convert(money,((ISNULL(OUTSTANDING_RESERVE,0) + ISNULL(PAID_LOSS,0))- ISNULL(RECOVERIES,0))),1),0,charindex('.',convert(varchar(30),convert(money,((ISNULL(OUTSTANDING_RESERVE,0) + ISNULL(PAID_LOSS,0))- ISNULL(RECOVERIES,0))),1),0)))  AS INCURRED_1 " +
					" FROM CLM_CLAIM_INFO CCI " + 
					//" LEFT JOIN CLM_ADJUSTER ADJ on ADJ.ADJUSTER_ID = CCI.ADJUSTER_CODE " +
					" LEFT JOIN CLM_ADJUSTER ADJ on ADJ.ADJUSTER_CODE = CCI.ADJUSTER_CODE " +
					" LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID " + 
					" AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID " + 
					" LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID " + 
					" LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID " + 
					" LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID  WHERE CCI.CUSTOMER_ID = " + hidCUSTOMER_ID.Value+  " ) Test ";
			  */
                sFROMCLAUSE = "(SELECT isnull(CONVERT(VARCHAR(10),LOSS_DATE,case when " + ClsCommon.BL_LANG_ID +" = 2 then 103 else 101 end),'') AS LOSS_DATE,CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER, ISNULL(mlvm.LOOKUP_VALUE_DESC , MLV.LOOKUP_VALUE_DESC) AS CLAIM_STATUS,PCPL.POLICY_NUMBER," +
					//Done for Itrack Issue 6618 on 24 Oct 09
					/*" substring(convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0)) AS OUTSTANDING,  " +
					" substring(convert(varchar(30),convert(money,PAID_LOSS),1),0,charindex('.',convert(varchar(30),convert(money,PAID_LOSS),1),0)) AS PAID, " +*/
					" convert(varchar(30),convert(money,isnull( OUTSTANDING_RESERVE,0)),1) AS OUTSTANDING,  " +
					" convert(varchar(30),convert(money,isnull( PAID_LOSS,0)),1) AS PAID, " +
					//Done for Itrack Issue 6620 on 27 Nov 09
					" CASE WHEN AT_FAULT_INDICATOR = 2 THEN 'Y' ELSE 'N' END AS AT_FAULT, " +
					//" CASE WHEN PINK_SLIP_TYPE_LIST LIKE '%1' + '3005%' THEN 'Y' ELSE 'N' END AS AT_FAULT," 
					//Done for Itrack Issue 6640 on 9 Dec 09
					" CASE WHEN WEATHER_RELATED_LOSS = 10963 THEN 'Y' ELSE 'N' END AS WEATHER_RELATED_LOSS, " +
					" dbo.fun_GetClaimLossDescription(COD.LOSS_TYPE) AS LOSS_TYPE, LOSS_DESCRIPTION, dbo.fun_GetClaimVehicleClass(CCI.CLAIM_ID) AS VEHICLE_CLASS, " +
					" dbo.fun_GetClaimDriverName(CCI.CLAIM_ID) AS DRIVER_NAME, CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CCI.CLAIM_ID, CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, " +
					" ISNULL(CCI.HOMEOWNER,0) HOMEOWNER, ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE, MLM.LOB_DESC AS LOB_DESC, " +
					" convert(money,substring(convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0))) AS OUTSTANDING_1,  " + 
					" convert(money,substring(convert(varchar(30),convert(money,PAID_LOSS),1),0,charindex('.',convert(varchar(30),convert(money,PAID_LOSS),1),0))) AS PAID_1," +
					//Added for Itrack Issue 6069 on 21 Oct 09
					"CA.ADJUSTER_NAME AS ADJUSTER_NAME" +
					" FROM CLM_CLAIM_INFO CCI LEFT OUTER JOIN CLM_ADJUSTER CA ON CCI.ADJUSTER_ID=CA.ADJUSTER_ID LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON COD.CLAIM_ID=CCI.CLAIM_ID " + 
					" LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID  AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  " +
					" LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID  LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID " +
                    " LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL mlvm on mlvm.LOOKUP_UNIQUE_ID =CCI.CLAIM_STATUS and mlvm.LANG_ID="+GetLanguageID ()+
					" WHERE CCI.CUSTOMER_ID = " + hidCUSTOMER_ID.Value+  " ) Test ";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				//Done for Itrack Issue 6069 on 21 Oct 09
				//objWebGrid.SearchColumnHeadings = "Date of Loss^Claim #^Status^Policy #^LOB";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Date of Loss^Claim #^Status^Policy #^Adjuster Name";	
				//Done for Itrack Issue 6069 on 21 Oct 09
				//objWebGrid.SearchColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^LOB_ID";
				objWebGrid.SearchColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^ADJUSTER_NAME";
				//Done for Itrack Issue 6069 on 21 Oct 09
				//objWebGrid.DropDownColumns          =   "^^^^LOB";
				objWebGrid.DropDownColumns          =   "^^^^^^";
				//Done for Itrack Issue 6069 on 21 Oct 09
				//objWebGrid.SearchColumnType			=	"D^T^T^T^T";
				objWebGrid.SearchColumnType			=	"T^T^T^T^T";
				objWebGrid.PrimaryColumns = "15";
				objWebGrid.PrimaryColumnsName = "CLAIM_ID";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");// Claim Search
                objWebGrid.OrderByClause = "LOSS_DATE";
				//Done for Itrack Issue 6069 on 21 Oct 09
				//objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				if(hidWolverineUser.Value=="1") //Wolverine User..show all the options
				{
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnHeadings = "Date of Loss^Claim #^Status^Policy #^Outstanding^Paid^At Fault^Loss Type^Loss Description^Driver Class^Driver Name^LOB";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnHeadings = "Date of Loss^Claim #^Status^Policy #^Outstanding^Paid^At Fault^Loss Description^Driver Class^Driver Name^Adjuster Name";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Date of Loss^Claim #^Status^Policy #^Outstanding^Paid^At Fault^Weather Related^Loss Type^Loss Description^Driver Class^Driver Name^Adjuster Name";
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9^10^11^21";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9^10^11^24";
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^9^10^24";
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^OUTSTANDING^PAID^AT_FAULT^LOSS_TYPE^LOSS_DESCRIPTION^VEHICLE_CLASS^DRIVER_NAME^LOB_DESC";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^OUTSTANDING^PAID^AT_FAULT^LOSS_TYPE^LOSS_DESCRIPTION^VEHICLE_CLASS^DRIVER_NAME^ADJUSTER_NAME";
					objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^OUTSTANDING^PAID^AT_FAULT^LOSS_TYPE^LOSS_DESCRIPTION^ADJUSTER_NAME";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayTextLength = "10^10^5^5^10^10^5^10^5^15^5^10";
					objWebGrid.DisplayTextLength = "10^10^5^5^10^10^5^10^5^10";
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnPercent = "10^10^5^5^10^10^5^10^5^15^5^10";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnPercent = "10^10^5^5^5^10^5^10^5^15^5^20";
					objWebGrid.DisplayColumnPercent = "10^10^5^5^5^10^5^10^5^20";
					//Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
					//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B^B^B^B";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.ColumnTypes = "B^B^B^B^N^N^B^B^B^B^B^B";
					objWebGrid.ColumnTypes = "B^B^B^B^N^N^B^B^B^B";					
					objWebGrid.ColumnsLink= rootPath + "claims/aspx/ClaimsNotificationIndex.aspx?";
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New Claim^0^addNewClaim";											
				}
				else
				{
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnHeadings = "Date of Loss^Claim #^Status^Policy #^Outstanding^Paid^At Fault^Loss Type^Loss Description^LOB";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnHeadings = "Date of Loss^Claim #^Status^Policy #^Outstanding^Paid^At Fault^Loss Type^Loss Description^Adjuster Name";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsNonCarr");//"Date of Loss^Claim #^Status^Policy #^Outstanding^Paid^At Fault^Weather Related^Loss Type^Loss Description^Adjuster Name";
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9^21";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9^24";
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9^10^24";
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^OUTSTANDING^PAID^AT_FAULT^LOSS_TYPE^LOSS_DESCRIPTION^LOB_DESC";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^OUTSTANDING^PAID^AT_FAULT^LOSS_TYPE^LOSS_DESCRIPTION^ADJUSTER_NAME";
					objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^OUTSTANDING^PAID^AT_FAULT^WEATHER_RELATED_LOSS^LOSS_TYPE^LOSS_DESCRIPTION^ADJUSTER_NAME";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayTextLength = "10^10^5^5^10^10^5^10^5^15^5^10";
					objWebGrid.DisplayTextLength = "10^10^5^5^10^10^5^5^10^5^15^5^10";
					//Done for Itrack Issue 6069 on 21 Oct 09
					//objWebGrid.DisplayColumnPercent = "10^10^5^10^10^10^10^15^10^10";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.DisplayColumnPercent = "10^10^5^5^5^10^5^10^5^15^5^20";
					objWebGrid.DisplayColumnPercent = "10^10^5^5^5^10^5^5^10^5^15^5^20";
					//Done for Itrack Issue 6640 on 9 Dec 09
					//objWebGrid.ColumnTypes = "B^B^B^B^N^N^B^B^B^B";
					objWebGrid.ColumnTypes = "B^B^B^B^N^N^B^B^B^B^B";
					objWebGrid.RequireNormalCursor = "Y";
				}
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;				
				objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//Include Inactive				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^CLAIM_ID^LOB_ID^HOMEOWNER^RECR_VEH^IN_MARINE";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";

				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion

		}

		private void GetQueryStringValues()
		{
			if(Request.QueryString["WOLVERINE_USER"]!=null && Request.QueryString["WOLVERINE_USER"].ToString()!="")
				hidWolverineUser.Value	= Request.QueryString["WOLVERINE_USER"].ToString();

			if(Request.QueryString["CustomerID"]!=null && Request.QueryString["CustomerID"].ToString()!="")
				hidCUSTOMER_ID.Value	= Request.QueryString["CustomerID"].ToString();

		}
		private void SetCookieValue()
		{
            Response.Cookies["LastVisitedTab"].Value = "2"; //Changed from 3 for Policy Page Implementation
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
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