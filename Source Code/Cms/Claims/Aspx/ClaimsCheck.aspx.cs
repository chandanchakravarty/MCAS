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

using Cms.Claims;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.CmsWeb.WebControls;

using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;

using Cms.ExceptionPublisher.ExceptionManagement;
using System.Resources;
using System.Reflection;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// Summary description for ClaimsCheck.
	/// </summary>
	public class ClaimsCheck  : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        System.Resources.ResourceManager objResourceMgr;
		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");			
			base.ScreenId	=	"386";
//			base.ClearClaimsSessionValues();
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.ClaimsCheck", Assembly.GetExecutingAssembly());
	
			
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

                string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
                string sSELECTCLAUSE = "", sFROMCLAUSE = "";
               
//				sSELECTCLAUSE= "  'CHECK_ID='+ISNULL(CAST(CHECK_ID AS VARCHAR(8000)),0) AS UNIQUEGRDID, CHECK_ID,CHECK_TYPE, CASE WHEN ISNULL(MANUAL_CHECK,'N') = 'Y' THEN 'Yes' ELSE 'No' END AS MANUAL_CHECK, ISNULL(CHECK_NUMBER,'-') AS CHECK_NUMBER, CONVERT(VARCHAR(20),CHECK_DATE,101) AS CHECK_DATE,  CASE WHEN ISNULL(IS_COMMITED,'N') = 'Y' THEN 'Yes' ELSE 'No' END AS IS_COMMITED, PAYMENT_MODE, LOOKUP_VALUE_DESC,ACCOUNTS.TRAN_DESC AS CLAIM_NUMBER,   ";
//				sSELECTCLAUSE += " CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL( CHECK_AMOUNT,0)),1) as CHECK_AMOUNT, " ;
//				sSELECTCLAUSE += " CASE WHEN ISNULL(CLAIM_TO_ORDER_DESC,'') = '' THEN ISNULL(CHECKS.PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD1,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD2,'') + ' ' + ISNULL(CHECKS.PAYEE_CITY,'') + ' ' + ISNULL(CHECKS.PAYEE_STATE,'') + ' ' + ISNULL(CHECKS.PAYEE_ZIP,'') WHEN CLAIM_TO_ORDER_DESC = '' THEN ISNULL(CHECKS.PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD1,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD2,'') + ' ' + ISNULL(CHECKS.PAYEE_CITY,'') + ' ' + ISNULL(CHECKS.PAYEE_STATE,'') + ' ' + ISNULL(CHECKS.PAYEE_ZIP,'') ELSE ISNULL(CLAIM_TO_ORDER_DESC,'') END AS PAYEE_FULL_ADD";
//				sFROMCLAUSE  = "  ACT_CHECK_INFORMATION CHECKS INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON LOOKUP_UNIQUE_ID=PAYMENT_MODE   ";
//				sFROMCLAUSE += " LEFT OUTER JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS " ;
//				sFROMCLAUSE += " ON ACCOUNTS.CUSTOMER_ID = CHECKS.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = CHECKS.POLICY_ID ";
//				sFROMCLAUSE += " AND ACCOUNTS.POLICY_VERSION_ID = CHECKS.POLICY_VER_TRACKING_ID AND ACCOUNTS.SOURCE_ROW_ID = CHECKS.CHECK_ID ";
//				sFROMCLAUSE += " AND  CHECKS.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT ";
				
				/* commented by Asfa (04-Dec-2008)
				
				sSELECTCLAUSE= " DISTINCT 'CHECK_ID='+ISNULL(CAST(CHECK_ID AS VARCHAR(8000)),0) AS UNIQUEGRDID, CHECK_ID,CHECK_TYPE,CASE WHEN ISNULL(MANUAL_CHECK,'N') = 'Y' THEN 'Yes' ELSE 'No' END AS MANUAL_CHECK, ISNULL(CHECK_NUMBER,'-') AS CHECK_NUMBER, CONVERT(VARCHAR(20),CHECK_DATE,101) AS CHECK_DATE, CASE WHEN ISNULL(IS_COMMITED,'N') = 'Y' THEN 'Yes' ELSE 'No' END AS IS_COMMITED, PAYMENT_MODE, LOOKUP_VALUE_DESC,ACCOUNTS.TRAN_DESC AS CLAIM_NUMBER,";  
				sSELECTCLAUSE+= "CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL( CHECK_AMOUNT,0)),1) as CHECK_AMOUNT,";  
				sSELECTCLAUSE+= "CASE WHEN ISNULL(CLAIM_TO_ORDER_DESC,'') = '' THEN ISNULL(CHECKS.PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD1,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD2,'') + ' ' + ISNULL(CHECKS.PAYEE_CITY,'') + ' ' + ISNULL(CHECKS.PAYEE_STATE,'') + ' ' + ISNULL(CHECKS.PAYEE_ZIP,'') WHEN CLAIM_TO_ORDER_DESC = '' THEN ISNULL(CHECKS.PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD1,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD2,'') + ' ' + ISNULL(CHECKS.PAYEE_CITY,'') + ' ' + ISNULL(CHECKS.PAYEE_STATE,'') + ' ' + ISNULL(CHECKS.PAYEE_ZIP,'') ELSE ISNULL(CLAIM_TO_ORDER_DESC,'') END AS PAYEE_FULL_ADD ";  

				sFROMCLAUSE  = " ACT_CHECK_INFORMATION CHECKS INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON LOOKUP_UNIQUE_ID=PAYMENT_MODE ";
				sFROMCLAUSE  += "INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS "; 
				sFROMCLAUSE  += "ON ACCOUNTS.CUSTOMER_ID = CHECKS.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = CHECKS.POLICY_ID AND ACCOUNTS.POLICY_VERSION_ID = CHECKS.POLICY_VER_TRACKING_ID"; 
				sFROMCLAUSE  += " AND CHECKS.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT"; 
				*/

				// IF CHECK NUM IS NULL THAT MEANS CHECK IS NOT PRINTED
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//PAYEE_STATE Replace by MCSL.STATE_NAME For Itrack Issue #6522.  
				sSELECTCLAUSE= " DISTINCT 'CHECK_ID='+ISNULL(CAST(CHECKS.CHECK_ID AS VARCHAR(8000)),0) AS UNIQUEGRDID, CHECKS.CHECK_ID,CHECK_TYPE,CASE WHEN ISNULL(MANUAL_CHECK,'N') = 'Y' THEN 'YES' ELSE 'NO' END AS MANUAL_CHECK, ISNULL(CHECK_NUMBER,'-') AS CHECK_NUMBER, CONVERT(VARCHAR(20),CHECK_DATE,101) AS CHECK_DATE, CASE WHEN ISNULL(IS_COMMITED,'N') = 'Y' THEN 'YES' ELSE 'NO' END AS IS_COMMITED, PAYMENT_MODE, LOOKUP_VALUE_DESC,CLAIM_NUMBER,CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL( CHECK_AMOUNT,0)),1) AS CHECK_AMOUNT,CASE WHEN ISNULL(CLAIM_TO_ORDER_DESC,'') = '' THEN ISNULL(CHECKS.PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD1,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD2,'') + ' ' + ISNULL(CHECKS.PAYEE_CITY,'') + ' ' + ISNULL(MCSL.STATE_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ZIP,'') WHEN CLAIM_TO_ORDER_DESC = '' THEN ISNULL(CHECKS.PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD1,'') + ' ' + ISNULL(CHECKS.PAYEE_ADD2,'') + ' ' + ISNULL(CHECKS.PAYEE_CITY,'') + ' ' + ISNULL(MCSL.STATE_NAME,'') + ' ' + ISNULL(CHECKS.PAYEE_ZIP,'') ELSE ISNULL(CLAIM_TO_ORDER_DESC,'') END AS PAYEE_FULL_ADD,convert(datetime,CHECK_DATE) ";
				objWebGrid.SelectClause  = sSELECTCLAUSE;
				//INNER JOIN MNT_COUNTRY_STATE_LIST For Itrack Issue 6522. 
				sFROMCLAUSE=" ACT_CHECK_INFORMATION CHECKS INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON LOOKUP_UNIQUE_ID=PAYMENT_MODE INNER JOIN MNT_COUNTRY_STATE_LIST MCSL  ON MCSL.STATE_ID = CHECKS.PAYEE_STATE INNER JOIN CLM_ACTIVITY AS CA ON CA.CHECK_ID=CHECKS.CHECK_ID INNER JOIN CLM_CLAIM_INFO AS CCI ON CCI.CLAIM_ID=CA.CLAIM_ID ";
				objWebGrid.FromClause    = sFROMCLAUSE ; 
				objWebGrid.WhereClause   = " CHECK_TYPE=9937  AND CHECK_NUMBER IS NULL AND PAYMENT_MODE =11787 AND ISNULL(CHECKS.GL_UPDATE,'') <> '2' ";

				/*objWebGrid.SearchColumnHeadings = "Check Number^Manual Check^Check Date^Payment Mode";
				objWebGrid.SearchColumnNames = "CHECK_NUMBER^MANUAL_CHECK^CHECK_DATE^LOOKUP_VALUE_DESC";		
				objWebGrid.SearchColumnType			=	"T^T^D^T";*/
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Check Date^Claim #^Manual Check^Check Amount^Payment Mode";				
				
				/* commented by Asfa (04-Dec-2008)
				objWebGrid.SearchColumnNames = "CHECK_DATE^ACCOUNTS.TRAN_DESC^CASE WHEN ISNULL(MANUAL_CHECK,'N') = 'Y' THEN 'Yes' ELSE 'No' END^CHECK_AMOUNT^LOOKUP_VALUE_DESC";
				*/
				objWebGrid.SearchColumnNames = "CHECK_DATE^CLAIM_NUMBER^CASE WHEN ISNULL(MANUAL_CHECK,'N') = 'Y' THEN 'Yes' ELSE 'No' END^CHECK_AMOUNT^LOOKUP_VALUE_DESC";
				
				objWebGrid.SearchColumnType	= "D^T^T^T^T";

                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Print Claims Checks";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); // "Check Date^Claim #^Manual Check^Check Amount^Payment Mode^Payee Details";
				objWebGrid.DisplayColumnNumbers = "5^6^7^10^11^12";
				objWebGrid.DisplayColumnNames = "CHECK_DATE^CLAIM_NUMBER^MANUAL_CHECK^CHECK_AMOUNT^LOOKUP_VALUE_DESC^PAYEE_FULL_ADD";
				//objWebGrid.DisplayTextLength = "12^22^22^22^22^50";
				//objWebGrid.DisplayColumnPercent = "12^22^22^22^22^30";
				objWebGrid.DisplayTextLength = "12^16^16^16^16^30";
				objWebGrid.DisplayColumnPercent = "12^16^16^16^16^30";
								
				objWebGrid.PrimaryColumns = "2";
				objWebGrid.PrimaryColumnsName = "CHECKS.CHECK_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				objWebGrid.OrderByClause = "CHECKS.CHECK_ID";
				//objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;				
				objWebGrid.RequireQuery = "Y";
				objWebGrid.RequireCheckbox="Y";
				objWebGrid.RequireNormalCursor = "Y";
				objWebGrid.CellHorizontalAlign= "4";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Print Check^0^PrintClaimCheck";											
				
				//objWebGrid.ColumnsLink= rootPath + "Account/Aspx/AddCheck.aspx?"; 				
				objWebGrid.QueryStringColumns = "CHECKS.CHECK_ID";
				objWebGrid.DefaultSearch = "Y";

				//Response.Write("<!-- Query:\n" + objWebGrid.SqlQuery + "-->");
				GridHolder.Controls.Add(objWebGrid);
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion

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
			this.Unload += new System.EventHandler(this.ClaimsCheck_Unload);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ClaimsCheck_Unload(object sender, EventArgs e)
		{
			//string str="<script>changeSortFirstTime(1,'CHECK_NUMBER',true,false);document.getElementById('SearchButton').click();</script>";
			//Response.Write(str);
		}
	}
}
