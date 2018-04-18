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
	/// Summary description for Credit Card Sweep.
	/// </summary>
	public class CreditCardSweep  : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckedRowIDs;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");			
			base.ScreenId	=	"428";
			
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

			if (Request.Form["__EVENTTARGET"] == "Hold" && hidCheckedRowIDs.Value!="")
			{   
				hidCheckedRowIDs.Value = hidCheckedRowIDs.Value.Replace("~",",");
				hidCheckedRowIDs.Value = hidCheckedRowIDs.Value.Replace(" ","");
				Cms.BusinessLayer.BlAccount.ClsAccount.CreditCardSweepOperation(hidCheckedRowIDs.Value,"HOLD");
				hidCheckedRowIDs.Value="";
			}
			else if (Request.Form["__EVENTTARGET"] == "Unhold" && hidCheckedRowIDs.Value!="")
			{   
				hidCheckedRowIDs.Value = hidCheckedRowIDs.Value.Replace("~",",");
				hidCheckedRowIDs.Value = hidCheckedRowIDs.Value.Replace(" ","");
				Cms.BusinessLayer.BlAccount.ClsAccount.CreditCardSweepOperation(hidCheckedRowIDs.Value,"UNHOLD");
				hidCheckedRowIDs.Value="";
			}

			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{

                string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				sSELECTCLAUSE= " IDEN_ROW_ID, ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME ,'') AS CUSTOMER_NAME , POLICY_NUMBER, CONVERT(VARCHAR,CONVERT(MONEY,ISNULL(TRANSACTION_AMOUNT,0),1),1) AS TRANSACTION_AMOUNT, CONVERT(VARCHAR(20), ECCS.CREATED_DATETIME,101) AS CREATED_DATETIME , CONVERT(VARCHAR(20),PROCESSED_DATETIME,101) AS PROCESSED_DATETIME, CASE ISNULL(PROCESSED,'N') WHEN 'F' THEN 'Failed' WHEN 'H' THEN 'On Hold' END PROCESSED , ERROR_DESCRIPTION, CONVERT(MONEY,ISNULL(TRANSACTION_AMOUNT,0),1) AS TRANSACTION_AMOUNT_1 ";
				objWebGrid.SelectClause  = sSELECTCLAUSE;
				sFROMCLAUSE=" EOD_CREDIT_CARD_SPOOL ECCS LEFT OUTER JOIN CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID=ECCS.ENTITY_ID ";
				objWebGrid.FromClause    = sFROMCLAUSE ; 
				objWebGrid.WhereClause   = " ISNULL(PROCESSED,'F') IN ('F','H') ";

				objWebGrid.SearchColumnHeadings = "Spool Date^Sweep Date";				
				
				objWebGrid.SearchColumnNames = "ECCS.CREATED_DATETIME^PROCESSED_DATETIME";
				
				objWebGrid.SearchColumnType	= "D^D";

				objWebGrid.HeaderString = "Hold Unhold Credit Card Sweep";
				objWebGrid.DisplayColumnHeadings = "Customer Name^Policy Number^Transaction Amount^Spooled On^Last Processed^Status^Error Description";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7^8";
				objWebGrid.DisplayColumnNames = "CUSTOMER_NAME^POLICY_NUMBER^TRANSACTION_AMOUNT^CREATED_DATETIME^PROCESSED_DATETIME^PROCESSED^ERROR_DESCRIPTION";
				objWebGrid.DisplayTextLength = "25^5^10^10^5^10^35";
				objWebGrid.DisplayColumnPercent = "25^5^10^10^5^10^35";
								
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "IDEN_ROW_ID";
				//Modified by Asfa (18-June-2008) - iTrack #3906(Note: 1)
				//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.ColumnTypes = "B^B^N^B^B^B^B";
				objWebGrid.OrderByClause = "IDEN_ROW_ID";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;				
				objWebGrid.RequireQuery = "Y";
				objWebGrid.RequireCheckbox="Y";
				objWebGrid.RequireNormalCursor = "Y";
				objWebGrid.ExtraButtons = "2^Hold~Unhold^0~2^Hold~Unhold";											
				
				objWebGrid.QueryStringColumns = "IDEN_ROW_ID";
				objWebGrid.CellHorizontalAlign= "3";
				objWebGrid.DefaultSearch = "Y";

				
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
