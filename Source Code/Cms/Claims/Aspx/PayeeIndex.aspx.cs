/******************************************************************************************
<Author					: - > Vijay Arora
<Start Date				: -	> 01-06-2006
<End Date				: - >
<Description			: - > This file is being used to show/search Claims Payees.
<Review Date			: - >
<Reviewed By			: - >
Modification History
<Modified Date			: - >
<Modified By			: - >
<Purpose				: - >
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
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display Parties
	/// </summary>
	public class PayeeIndex : Cms.Claims.ClaimBase
	{
		
		#region "web form designer controls declaration"
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected System.Web.UI.WebControls.Label lblError;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_RECORD;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_PAYMENT_EXPENSE;
        ResourceManager objResourceMgr = null;
		#endregion
		protected System.Web.UI.WebControls.Label capMessage;
		string strEXPENSE_ID="";
        string strACTIVITY_REASON = "";
        string IS_PAYMENT_EXPENSE = "";
		#region Local form variables
		private string strTemp="";
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="309_1_0_1_0_1";
			
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.PayeeIndex", Assembly.GetExecutingAssembly());	
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
			if(Request["CLAIM_ID"] !=null && Request["ACTIVITY_ID"] != null )//&& Request["CALLED_FROM"]!=null)
			{
				if(Request["EXPENSE_ID"]!=null && Request["EXPENSE_ID"].ToString()!="")
					strEXPENSE_ID = Request["EXPENSE_ID"].ToString();
				else
					strEXPENSE_ID = "0";

                if (Request["IS_PAYMENT_EXPENSE"] != null && Request["IS_PAYMENT_EXPENSE"].ToString() != "")
                    IS_PAYMENT_EXPENSE = Request["IS_PAYMENT_EXPENSE"].ToString();
                else
                    IS_PAYMENT_EXPENSE = "0";


                if (Request["ACTIVITY_REASON"] != null && Request["ACTIVITY_REASON"].ToString() != "")
                    strACTIVITY_REASON = Request["ACTIVITY_REASON"].ToString();
                else
                    strACTIVITY_REASON = "0";

                strTemp = "&CLAIM_ID=" + Request["CLAIM_ID"].ToString() + "&ACTIVITY_ID=" + Request["ACTIVITY_ID"].ToString() + "&EXPENSE_ID=" + strEXPENSE_ID + "&ACTIVITY_REASON=" + strACTIVITY_REASON + "&";

                if (IS_PAYMENT_EXPENSE == "Y")
                    strTemp += "&IS_PAYMENT_EXPENSE=Y";
			}

			
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{	
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE+= " * ";
				
				//if(strCALLED_FROM==CALLED_FROM_PAYMENT)
				{	
					TabCtl.TabURLs = "AddPayee.aspx?" +  strTemp +"&"; 
					//sFROMCLAUSE  = " ( SELECT CASE WHEN ISNULL(SECONDARY_PARTY_ID,0) > 0 THEN ISNULL(P.[NAME],'') + ' , ' + ISNULL(P2.[NAME],'') ELSE P.[NAME] END PAYEE, L.LOOKUP_VALUE_DESC AS PAYMENT_METHOD, ISNULL(CP.ADDRESS1,'') + ' ' + ISNULL(CP.ADDRESS2,'') AS ADDRESS, CP.NARRATIVE, CP.PAYEE_ID, CP.IS_ACTIVE,substring(convert(varchar(30),convert(money,AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,AMOUNT),1),0)) AMOUNT   FROM CLM_PAYEE CP LEFT JOIN CLM_PARTIES P ON P.CLAIM_ID = CP.CLAIM_ID AND P.PARTY_ID = CP.PARTY_ID LEFT JOIN CLM_PARTIES P2 ON P2.CLAIM_ID = CP.CLAIM_ID AND P2.PARTY_ID = CP.SECONDARY_PARTY_ID LEFT JOIN MNT_LOOKUP_VALUES L ON L.LOOKUP_UNIQUE_ID = CP.PAYMENT_METHOD WHERE CP.CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " AND CP.ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " AND CP.EXPENSE_ID = " + strEXPENSE_ID + " ) Test ";
                    //sFROMCLAUSE  = " ( SELECT REPLACE(ISNULL(CP.SECONDARY_PARTY_ID,'') ,'^', ' And ') AS  PAYEE, L.LOOKUP_VALUE_DESC AS PAYMENT_METHOD, ISNULL(CP.ADDRESS1,'') + ' ' + ISNULL(CP.ADDRESS2,'') AS ADDRESS, CP.NARRATIVE, CP.PAYEE_ID, CP.IS_ACTIVE,substring(convert(varchar(30),convert(money,AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,AMOUNT),1),0)) AMOUNT   FROM CLM_PAYEE CP  LEFT JOIN MNT_LOOKUP_VALUES L ON L.LOOKUP_UNIQUE_ID = CP.PAYMENT_METHOD WHERE CP.CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " AND CP.ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " AND CP.EXPENSE_ID = " + strEXPENSE_ID + " ) Test ";
                    sFROMCLAUSE  = " ( SELECT REPLACE(ISNULL(CP.SECONDARY_PARTY_ID,'') ,'^', ' And ') AS  PAYEE, L.LOOKUP_VALUE_DESC AS PAYMENT_METHOD, ISNULL(CP.ADDRESS1,'') + ' ' + ISNULL(CP.ADDRESS2,'') AS ADDRESS, CP.NARRATIVE, CP.PAYEE_ID, CP.IS_ACTIVE,substring(convert(varchar(30),convert(money,AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,AMOUNT),1),0)) AMOUNT   FROM CLM_PAYEE CP  LEFT JOIN MNT_LOOKUP_VALUES L ON L.LOOKUP_UNIQUE_ID = CP.PAYMENT_METHOD WHERE CP.CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " AND CP.ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " ) Test ";
				}
                //else //Called from expense
                //{	TabCtl.TabURLs = "AddExpensePayee.aspx?" +  strTemp +"&"; 
                //    sFROMCLAUSE  = " ( SELECT P.[NAME] AS PAYEE, L.LOOKUP_VALUE_DESC AS PAYMENT_METHOD, ISNULL(CP.ADDRESS1,'') + ' ' + ISNULL(CP.ADDRESS2,'') AS ADDRESS, CP.NARRATIVE, CP.PAYEE_ID, CP.IS_ACTIVE,substring(convert(varchar(30),convert(money,AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,AMOUNT),1),0)) AMOUNT   FROM CLM_PAYEE CP LEFT JOIN CLM_PARTIES P ON P.CLAIM_ID = CP.CLAIM_ID AND P.PARTY_ID = CP.PARTY_ID LEFT JOIN MNT_LOOKUP_VALUES L ON L.LOOKUP_UNIQUE_ID = CP.PAYMENT_METHOD WHERE CP.CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " AND CP.ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " AND CP.EXPENSE_ID = " + strEXPENSE_ID + " ) Test ";
                //}
				//sFROMCLAUSE  = " ( SELECT P.[NAME] AS PAYEE, L.LOOKUP_VALUE_DESC AS PAYMENT_METHOD, ISNULL(CP.ADDRESS1,'') + ' ' + ISNULL(CP.ADDRESS2,'') AS ADDRESS, CP.NARRATIVE, CP.PAYEE_ID, CP.IS_ACTIVE,substring(convert(varchar(30),convert(money,AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,AMOUNT),1),0)) AMOUNT   FROM CLM_PAYEE CP LEFT JOIN CLM_PARTIES P ON P.CLAIM_ID = CP.CLAIM_ID AND P.PARTY_ID = CP.PARTY_ID LEFT JOIN MNT_LOOKUP_VALUES L ON L.LOOKUP_UNIQUE_ID = CP.PAYMENT_METHOD WHERE CP.CLAIM_ID = " + Request["CLAIM_ID"].ToString() + " AND CP.ACTIVITY_ID = " + Request["ACTIVITY_ID"].ToString() + " AND CP.EXPENSE_ID = " + strEXPENSE_ID + " ) Test ";

//				objWebGrid.SearchColumnHeadings = "Payee Name^Payment Method";//^Amount";
//				objWebGrid.SearchColumnNames = "PAYEE^PAYMENT_METHOD";//^AMOUNT";
//				objWebGrid.SearchColumnType = "T^T";//^T";
//				objWebGrid.DisplayColumnHeadings = "Payee Name^Payment Method";//^Amount";
//				objWebGrid.DisplayColumnNumbers = "1^2";//^3";
//				objWebGrid.DisplayColumnNames = "PAYEE^PAYMENT_METHOD";//^AMOUNT";
//				objWebGrid.DisplayTextLength = "30^20";//^20";
//				objWebGrid.DisplayColumnPercent = "30^20";//^20";										
//				objWebGrid.ColumnTypes = "B^B";//^B";										
//				objWebGrid.FetchColumns = "1^2";//^3";

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Payee Name";//^Amount";
				objWebGrid.SearchColumnNames = "PAYEE";//^AMOUNT";
				objWebGrid.SearchColumnType = "T";//^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Payee Name";//^Amount";
				objWebGrid.DisplayColumnNumbers = "1";//^3";
				objWebGrid.DisplayColumnNames = "PAYEE";//^AMOUNT";
				objWebGrid.DisplayTextLength = "100";//^20";
				objWebGrid.DisplayColumnPercent = "100";//^20";										
				objWebGrid.ColumnTypes = "B";//^B";										
				objWebGrid.FetchColumns = "1";//^3";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "PAYEE";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Payee Details";
				objWebGrid.AllowDBLClick = "true";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
			//	string strClaimStatus = GetClaimStatus();
              //  if (strClaimStatus != CLAIM_STATUS_CLOSED)
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				//objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.QueryStringColumns = "PAYEE_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabTitles =Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId,"TabCtl");//"Payee Details";
				TabCtl.TabLength =150;
	
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
