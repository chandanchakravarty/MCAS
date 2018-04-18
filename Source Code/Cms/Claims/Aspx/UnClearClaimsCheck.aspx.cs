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
using System.Reflection;
using System.Resources;

namespace Claims.Aspx
{
	/// <summary>
	/// Summary description for UnClearClaimsCheck.
	/// </summary>
	public class UnClearClaimsCheck : Cms.Claims.ClaimBase
	{

		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        System.Resources.ResourceManager objResourceMgr;
		public string strSecurity = "";


		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");			
			base.ScreenId	=	"389";
//			base.ClearClaimsSessionValues();

            objResourceMgr = new ResourceManager("Claims.Aspx.UnClearClaimsCheck", Assembly.GetExecutingAssembly());
			//Set Security XML
			strSecurity = gstrSecurityXML;
	
			if (Request.Form["__EVENTTARGET"] == "UnClearChecks" && hidCheckedRowIDs.Value!="")
			{   
				hidCheckedRowIDs.Value = hidCheckedRowIDs.Value.Replace("~",",");
				hidCheckedRowIDs.Value = hidCheckedRowIDs.Value.Replace(" ","");
				ClsClaims.ClaimsCheckOperation(hidCheckedRowIDs.Value,"UN-CLEAR");
				hidCheckedRowIDs.Value="";
			}
			
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

                string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE= "  'CHECK_ID='+ISNULL(CAST(ACT_CHECK_INFORMATION.CHECK_ID AS VARCHAR(8000)),0) AS UNIQUEGRDID, ACT_CHECK_INFORMATION.CHECK_ID,CHECK_TYPE, CASE WHEN ISNULL(MANUAL_CHECK,'N') = 'Y' THEN 'Yes' ELSE 'No' END AS MANUAL_CHECK, ISNULL(CHECK_NUMBER,'-') AS CHECK_NUMBER, CONVERT(VARCHAR(20),CHECK_DATE,101) AS CHECK_DATE, ISNULL(INFO.CLAIM_NUMBER,'') AS CLAIM_NUMBER,  CASE WHEN ISNULL(IS_COMMITED,'N') = 'Y' THEN 'Yes' ELSE 'No' END AS IS_COMMITED, PAYMENT_MODE, LOOKUP_VALUE_DESC  , ";
				sSELECTCLAUSE += " CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL( CHECK_AMOUNT,0)),1) CHECK_AMOUNT, " ;
				//PAYEE_STATE Replace by MCSL.STATE_NAME For Itrack Issue #6522. 
				sSELECTCLAUSE += " CASE WHEN ISNULL(CLAIM_TO_ORDER_DESC,'') = '' THEN ISNULL(PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(PAYEE_ADD1,'') + ' ' + ISNULL(PAYEE_ADD2,'') + ' ' +ISNULL(PAYEE_CITY,'') + ' ' + ISNULL(MCSL.STATE_NAME,'') + ' ' + ISNULL(PAYEE_ZIP,'') WHEN CLAIM_TO_ORDER_DESC = '' THEN ISNULL(PAYEE_ENTITY_NAME,'') + ' ' + ISNULL(PAYEE_ADD1,'') + ' ' + ISNULL(PAYEE_ADD2,'') + ' ' +ISNULL(PAYEE_CITY,'') + ' ' + ISNULL(MCSL.STATE_NAME,'') + ' ' + ISNULL(PAYEE_ZIP,'') ELSE ISNULL(CLAIM_TO_ORDER_DESC,'') END AS PAYEE_FULL_ADD";
				sSELECTCLAUSE += " ,CONVERT(MONEY,ISNULL(CHECK_AMOUNT,0)) CHECK_AMOUNT_1";
				//INNER JOIN MNT_COUNTRY_STATE_LIST For Itrack Issue 6522. 
				sFROMCLAUSE  = "  ACT_CHECK_INFORMATION INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE  INNER JOIN MNT_COUNTRY_STATE_LIST MCSL  ON MCSL.STATE_ID = ACT_CHECK_INFORMATION.PAYEE_STATE INNER JOIN CLM_ACTIVITY CLM ON CLM.CHECK_ID = ACT_CHECK_INFORMATION.CHECK_ID INNER JOIN CLM_CLAIM_INFO INFO ON INFO.CLAIM_ID =  CLM.CLAIM_ID ";

				// IF CHECK NUM IS NULL THAT MEANS CHECK IS NOT PRINTED
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause  = sSELECTCLAUSE;
				objWebGrid.FromClause    = sFROMCLAUSE ; 
				objWebGrid.WhereClause   = " CHECK_TYPE=9937    AND ISNULL(GL_UPDATE,1) =1 AND ISNULL(IS_BNK_RECONCILED,'N') = 'Y' AND CHECK_NUMBER IS NOT NULL ";


                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Un-Clear Claims Checks" ;

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Check Date^Claim Number^Check Number^Manual Check^Check Amount^Payment Mode";
				objWebGrid.SearchColumnNames = "CHECK_DATE^CLAIM_NUMBER^CHECK_NUMBER^CASE WHEN ISNULL(MANUAL_CHECK,'N') = 'Y' THEN 'Yes' ELSE 'No' END^CHECK_AMOUNT^LOOKUP_VALUE_DESC";		
				objWebGrid.SearchColumnType		= "D^T^T^T^T^T";

                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"Check Date^Claim #^Check Number^Manual Check^Check Amount^Payment Mode^Payee Details";
				//objWebGrid.DisplayColumnNumbers = "5^4^6^7^10^11";
				objWebGrid.DisplayColumnNumbers = "5^6^4^8^11^12";
				objWebGrid.DisplayColumnNames = "CHECK_DATE^CLAIM_NUMBER^CHECK_NUMBER^MANUAL_CHECK^CHECK_AMOUNT^LOOKUP_VALUE_DESC^PAYEE_FULL_ADD";
				//objWebGrid.DisplayTextLength = "16^16^16^16^16^30";
				//objWebGrid.DisplayColumnPercent = "16^16^16^16^16^30";
				objWebGrid.DisplayTextLength = "16^16^10^10^16^16^50";
				objWebGrid.DisplayColumnPercent = "16^10^10^16^16^16^50";

								
				objWebGrid.PrimaryColumns = "2";
				objWebGrid.PrimaryColumnsName = "ACT_CHECK_INFORMATION.CHECK_ID";
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B";

				//Modified by Asfa(18-june-2008) - iTrack #3906(Note: 1)
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "B^B^B^B^N^B^B";
				objWebGrid.OrderByClause = "ACT_CHECK_INFORMATION.CHECK_ID";
				//objWebGrid.AllowDBLClick = "true";
				//objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11";
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
                if (ClsCommon.CheckSecurity(strSecurity))
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Un-Clear Check^0^UnclearClaimCheck";											
				
				//objWebGrid.ColumnsLink= rootPath + "Account/Aspx/AddCheck.aspx?"; 				
				objWebGrid.QueryStringColumns = "ACT_CHECK_INFORMATION.CHECK_ID";
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
