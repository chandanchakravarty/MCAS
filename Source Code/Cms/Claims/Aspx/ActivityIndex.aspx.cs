/******************************************************************************************
<Author					: - >Vijay Arora
<Start Date				: -	> 24-05-2006
<End Date				: - >
<Description			: - > This file is being used to show/search Claims Activity.
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
using Cms.BusinessLayer.BLClaims;



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display Parties
	/// </summary>
	public class ActivityIndex : Cms.Claims.ClaimBase
	{
		
		#region "web form designer controls declaration"
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblSummaryRow;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected System.Web.UI.WebControls.Label blblError;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_RECORD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAUTHORIZE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReserveAdded;
		protected System.Web.UI.WebControls.CheckBox chkShowAll;

		#endregion
		protected System.Web.UI.WebControls.Label capMessage;
//		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;	
		double RESERVE_LIMIT,PAYMENT_LIMIT;


		#region Local form variables
		private string ClaimID="";
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strClaimID,strLOB_ID;
		#endregion
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		string rootPath=System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
        System.Resources.ResourceManager objResourceMgr;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			base.ScreenId="309_1";
			//SetClaimTop();
			GetAdjusterLimits();

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ActivityIndex", System.Reflection.Assembly.GetExecutingAssembly());
            
			
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
			if (Request["CLAIM_ID"] != null) 
				ClaimID = Request["CLAIM_ID"].ToString();
			else
				ClaimID = GetClaimID();

			if(Request.QueryString["ACTIVITY_ID"]!=null && Request.QueryString["ACTIVITY_ID"].ToString()!="")			
				hidACTIVITY_ID.Value = Request.QueryString["ACTIVITY_ID"].ToString(); //Take the activity_id value to edit the record							
			else
				hidACTIVITY_ID.Value = ""; //When no activity_id is being specified, let the index page open and leave as it is

			if(Request.QueryString["AUTHORIZE"]!=null && Request.QueryString["AUTHORIZE"].ToString()!="")			
				hidAUTHORIZE.Value = Request.QueryString["AUTHORIZE"].ToString(); 
			else
				hidAUTHORIZE.Value = "0"; 

			if(!Page.IsPostBack)
			{
				CheckForReserves();
			}
			
			string ClaimStatus = "";
            double AmountToRecover = 0;
     

			DataSet ds = ClsActivity.GetCalimStatus(ClaimID);
			if (ds.Tables[0].Rows.Count >0)
            {
				ClaimStatus = ds.Tables[0].Rows[0]["CLAIM_STATUS"].ToString();
                AmountToRecover = double.Parse(ds.Tables[0].Rows[0]["AMOUNT_TO_RECOVER"].ToString());                
           
            }

            string IsClaimVictim = "";
            if (ds.Tables[1].Rows.Count > 0)
            {
                IsClaimVictim = ds.Tables[1].Rows[0]["IS_VICTIM_CLAIM"].ToString();
            }

            chkShowAll.Text = objResourceMgr.GetString("chkShowAll");

			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{

                string sSELECTCLAUSE = "", sFROMCLAUSE = "", sWHERECLAUSE = "";
                #region commented code
                /*
                sSELECTCLAUSE += " ACTIVITY_ID,ACCOUNTING_SUPPRESSED, * ";//Added ACCOUNTING_SUPPRESSED for Itrack Issue 7169 -- To set font color when activity is suppressed --> used in ClsWebGridHelper to change color
				sFROMCLAUSE += " ( SELECT MLV.LOOKUP_VALUE_DESC AS REASON, CTD.DETAIL_TYPE_DESCRIPTION AS ACTIVITY_DESCRIPTION, "; 
				//sFROMCLAUSE += " substring(convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(CA.CLAIM_RESERVE_AMOUNT,0)),1),0)) AS SEARCH_OUTSTANDING,";
				
				////Commented by Asfa (20-Sept-2007) in order to correct Activities page search option 
				//sFROMCLAUSE += " substring(convert(varchar(30),convert(money,ISNULL(CA.RESERVE_AMOUNT,0)-isnull(CA.PAYMENT_AMOUNT,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(CA.RESERVE_AMOUNT,0)-isnull(CA.PAYMENT_AMOUNT,0)),1),0)) AS SEARCH_OUTSTANDING,";
				sFROMCLAUSE += " replace(case convert(varchar(30),convert(money,isnull( CA.RESERVE_AMOUNT,0)-isnull( CA.PAYMENT_AMOUNT,0)),1) when '0.00' then '' else  convert(varchar(30),convert(money,isnull( CA.RESERVE_AMOUNT,0)-isnull( CA.PAYMENT_AMOUNT,0)),1) END,',','') AS SEARCH_OUTSTANDING,";
				
				//sFROMCLAUSE += " convert(varchar(30),convert(money,isnull( CA.CLAIM_RESERVE_AMOUNT,0)),1) AS DISPLAY_OUTSTANDING,";
				//sFROMCLAUSE += " convert(varchar(30),convert(money,isnull( CA.RESERVE_AMOUNT,0)-isnull( CA.PAYMENT_AMOUNT,0)),1) AS DISPLAY_OUTSTANDING,";
				sFROMCLAUSE += " case convert(varchar(30),convert(money,isnull( CA.RESERVE_AMOUNT,0)-isnull( CA.PAYMENT_AMOUNT,0)),1) when '0.00' then '' else  convert(varchar(30),convert(money,isnull( CA.RESERVE_AMOUNT,0)-isnull( CA.PAYMENT_AMOUNT,0)),1) END AS DISPLAY_OUTSTANDING, ";
				
				////Commented by Asfa (20-Sept-2007) in order to correct Activities page search option 
				//sFROMCLAUSE += " substring(convert(varchar(30),convert(money,ISNULL(CA.PAYMENT_AMOUNT,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(CA.PAYMENT_AMOUNT,0)),1),0)) AS SEARCH_PAID, ";				
				sFROMCLAUSE += " replace(CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,CA.PAYMENT_AMOUNT),1) when '0.00' then '' else  convert(varchar(30),convert(money,CA.PAYMENT_AMOUNT),1) END END,',','') AS SEARCH_PAID,"; 
				
				//sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '0.00' ELSE  convert(varchar(30),convert(money,isnull( CA.PAYMENT_AMOUNT,0)),1) END AS PAID, ";
				sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,CA.PAYMENT_AMOUNT),1) when '0.00' then '' else  convert(varchar(30),convert(money,CA.PAYMENT_AMOUNT),1) END END AS DISPLAY_PAID,"; 

				////Commented by Asfa (20-Sept-2007) in order to correct Activities page search option 
				//sFROMCLAUSE += " CASE CAST (CA.ACTIVITY_REASON AS VARCHAR) WHEN '11836' THEN '0' ELSE substring(convert(varchar(30),convert(money,ISNULL(CA.RECOVERY,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(CA.RECOVERY,0)),1),0)) END AS SEARCH_RECOVERY, ";
				sFROMCLAUSE += " replace(CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,isnull( CA.RECOVERY,0)),1) when '0.00' then '' else convert(varchar(30),convert(money,isnull( CA.RECOVERY,0)),1) END END,',','') AS SEARCH_RECOVERY, ";
				
				//sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '0.00' ELSE convert(varchar(30),convert(money,isnull( CA.RECOVERY,0)),1) END AS RECOVERY, ";
				
				sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,isnull( CA.RECOVERY,0)),1) when '0.00' then '' else convert(varchar(30),convert(money,isnull( CA.RECOVERY,0)),1) END END AS DISPLAY_RECOVERY, ";
								
				//sFROMCLAUSE += " convert(varchar(30),convert(money,isnull( CASE CAST (CA.ACTIVITY_REASON AS VARCHAR) WHEN '11836' THEN '0' ELSE substring(convert(varchar(30),convert(money,ISNULL(CA.RECOVERY,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(CA.RECOVERY,0)),1),0)) END,0)),1) AS DISPLAY_RECOVERY, ";
				//sFROMCLAUSE += " convert(varchar(30),convert(money,isnull( CASE CAST (CA.ACTIVITY_REASON AS VARCHAR) WHEN '11836' THEN substring(convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(CA.CLAIM_RESERVE_AMOUNT,0)),1),0)) ELSE substring(convert(varchar(30),convert(money,((ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0))- ISNULL(CA.RECOVERY,0))),1),0,charindex('.',convert(varchar(30),convert(money,((ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0))- ISNULL(CA.RECOVERY,0))),1),0)) END,0)),1) AS DISPLAY_INCURRED, ";
				//sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '0.00' ELSE convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)- ISNULL(CA.RECOVERY,0)),1) END AS INCURRED,";


				//Commented by Asfa (14-Sept-2007) in order to correct Activities as per email sent by Gagan.
				//sFROMCLAUSE += " CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)- ISNULL(CA.RECOVERY,0)),1) when '0.00' then '' else " ;
				//sFROMCLAUSE += " convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)- ISNULL(CA.RECOVERY,0)),1) END END AS INCURRED, ";
				//sFROMCLAUSE += " CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)+ ISNULL(CA.RECOVERY,0)),1) when '0.00' then '' else " ;
				//sFROMCLAUSE += " convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)+ ISNULL(CA.RECOVERY,0)),1) END END AS INCURRED, ";

				////Added by Asfa (20-Sept-2007) in order to correct Activities page search option 
				sFROMCLAUSE += " replace(CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)),1) when '0.00' then '' else " ;
				sFROMCLAUSE += " convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)),1) END END,',','') AS SEARCH_INCURRED, ";

				sFROMCLAUSE += " CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)),1) when '0.00' then '' else " ;
				sFROMCLAUSE += " convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)),1) END END AS DISPLAY_INCURRED, ";
				
				//sFROMCLAUSE += " CASE CAST (CA.ACTIVITY_REASON AS VARCHAR) WHEN '11836' THEN substring(convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(CA.CLAIM_RESERVE_AMOUNT,0)),1),0)) ELSE substring(convert(varchar(30),convert(money,((ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0))- ISNULL(CA.RECOVERY,0))),1),0,charindex('.',convert(varchar(30),convert(money,((ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0))- ISNULL(CA.RECOVERY,0))),1),0)) END AS SEARCH_INCURRED, ";
				//sFROMCLAUSE += " CASE CAST (CA.ACTIVITY_REASON AS VARCHAR) WHEN '11836' THEN '0' ELSE substring(convert(varchar(30),convert(money,ISNULL(CA.EXPENSES,0)),1),0,charindex('.',convert(varchar(30),convert(money,ISNULL(CA.EXPENSES,0)),1),0)) END AS SEARCH_EXPENSE, ";
				//sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '0.00' ELSE convert(varchar(30),convert(money,isnull( -CA.EXPENSES,0)),1) END AS EXPENSE, ";
				
				//Commented by Asfa (14-Sept-2007) in order to correct Activities as per email sent by Gagan.
				//sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE (case convert(varchar(30),convert(money,-CA.EXPENSES),1) when '0.00' then '' else convert(varchar(30),convert(money,-CA.EXPENSES),1) end ) END AS EXPENSE, ";
				
				////Added by Asfa (20-Sept-2007) in order to correct Activities page search option 
				sFROMCLAUSE += " replace(CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE (case convert(varchar(30),convert(money,CA.EXPENSES),1) when '0.00' then '' else convert(varchar(30),convert(money,CA.EXPENSES),1) end ) END,',','') AS SEARCH_EXPENSE, ";
				sFROMCLAUSE += "CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE (case convert(varchar(30),convert(money,CA.EXPENSES),1) when '0.00' then '' else convert(varchar(30),convert(money,CA.EXPENSES),1) end ) END AS DISPLAY_EXPENSE, ";

				//sFROMCLAUSE += " convert(varchar(30),convert(money,isnull( CASE CAST (CA.ACTIVITY_REASON AS VARCHAR) WHEN '11836' THEN '0' ELSE substring(convert(varchar(30),convert(money,ISNULL(-CA.EXPENSES,0)),1),0,charindex('.',convert(varchar(30),convert(money,ISNULL(-CA.EXPENSES,0)),1),0)) END,0)),1) AS DISPLAY_EXPENSE, " ;
				//sFROMCLAUSE += " convert(varchar(30),convert(money,isnull( -CA.EXPENSES,0)),1) AS DISPLAY_EXPENSE, " ;
				
				////Commented by Asfa (20-Sept-2007) in order to correct Activities page search option 
				//sFROMCLAUSE += " CA.RI_RESERVE AS SEARCH_REINSURANCE_OUTSTANDING ,";
				sFROMCLAUSE += " REPLACE(CASE ISNULL(RI_RESERVE,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.RI_RESERVE,0))),1) END,',','') AS SEARCH_REINSURANCE_OUTSTANDING, ";
				
				//sFROMCLAUSE += " convert(varchar(30),convert(money,isnull( CA.RI_RESERVE,0)),1) AS DISPLAY_REINSURANCE_OUTSTANDING,";
				//sFROMCLAUSE += " convert(varchar(30),convert(money, CA.RI_RESERVE),1) AS DISPLAY_REINSURANCE_OUTSTANDING, ";
				sFROMCLAUSE += " CASE ISNULL(RI_RESERVE,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.RI_RESERVE,0))),1) END AS DISPLAY_REINSURANCE_OUTSTANDING, ";
				sFROMCLAUSE += " REPLACE(CASE ISNULL(LOSS_REINSURANCE_RECOVERED,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.LOSS_REINSURANCE_RECOVERED,0))),1) END,',','') AS SEARCH_LOSS_REINSURANCE_RECOVERED, ";
				sFROMCLAUSE += " CASE ISNULL(LOSS_REINSURANCE_RECOVERED,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.LOSS_REINSURANCE_RECOVERED,0))),1) END AS DISPLAY_LOSS_REINSURANCE_RECOVERED, ";
				sFROMCLAUSE += " REPLACE(CASE ISNULL(EXPENSE_REINSURANCE_RECOVERED,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.EXPENSE_REINSURANCE_RECOVERED,0))),1) END,',','') AS SEARCH_EXPENSE_REINSURANCE_RECOVERED, "; 
				sFROMCLAUSE += " CASE ISNULL(EXPENSE_REINSURANCE_RECOVERED,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.EXPENSE_REINSURANCE_RECOVERED,0))),1) END AS DISPLAY_EXPENSE_REINSURANCE_RECOVERED, "; 


				sFROMCLAUSE += " LV.LOOKUP_VALUE_DESC AS ACTIVITY_STATUS1, "; 
				//sFROMCLAUSE += " case LV.LOOKUP_UNIQUE_ID when 11801 then '<img src=" + (rootPath) +   "/cmsweb/images"+ colorScheme + "/TabCheckRed.gif style=''Border-Width:0'' ImageAlign=absMiddle>'  else '<img src=" + (rootPath) +   "/cmsweb/images" + colorScheme + "/cross.gif style=''Border-Width:0'' ImageAlign=absMiddle>' end as ACTIVITY_STATUS,";
			
				sFROMCLAUSE += " case LV.LOOKUP_UNIQUE_ID when 11800  then '<img src=" + (rootPath) +   "/cmsweb/images"+ colorScheme + "/incomplete.gif style=''Border-Width:0'' ImageAlign=absMiddle>'";
				sFROMCLAUSE += " when 11801	then  '<img src=" + (rootPath) +   "/cmsweb/images"+ colorScheme + "/complete.gif style=''Border-Width:0'' ImageAlign=absMiddle>' ";
				sFROMCLAUSE += " when 11804 then  '<img src=" + (rootPath) +   "/cmsweb/images"+ colorScheme + "/authenticated.gif style=''Border-Width:0'' ImageAlign=absMiddle>'";
				//Commented for Itrack Issue 6547 on 12 Oct 09
				//sFROMCLAUSE += " when 11803  then '<img src=" + (rootPath) +   "/cmsweb/images"+ colorScheme + "/incomplete.gif style=''Border-Width:0'' ImageAlign=absMiddle>'";
				sFROMCLAUSE += " else '<img src=" + (rootPath) +   "/cmsweb/images"+ colorScheme + "/waiting.gif style=''Border-Width:0'' ImageAlign=absMiddle>' end as ACTIVITY_STATUS,";
				//sFROMCLAUSE += " CONVERT(VARCHAR(10),CA.ACTIVITY_DATE,101) AS ACTIVITY_DATE, "; 
				//sFROMCLAUSE += "  ACTIVITY_DATE, "; 
				sFROMCLAUSE += "  dbo.FormatDateTime(ACTIVITY_DATE, 'MM/DD/YY') + ' ' + dbo.FormatDateTime(ACTIVITY_DATE, 'HH:MM 12')  as ACTIVITY_DATE,";
				sFROMCLAUSE += " ISNULL(UL.USER_TITLE,'') + ' ' + ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS ADDED_BY, "; 
				sFROMCLAUSE += " CA.IS_ACTIVE, CA.ACTIVITY_ID, CA.CLAIM_ID, CA.ACTIVITY_REASON, CA.CREATED_DATETIME,CA.ACTION_ON_PAYMENT, CTD.ALLOW_MANUAL, CTD.IS_SYSTEM_GENERATED, CTD.GL_POSTING_REQUIRED "; 
				//Added by Asfa(19-June-2008) - iTrack #3906(Note: 1)
				sFROMCLAUSE += " ,convert(money,case convert(varchar(30),convert(money,isnull( CA.RESERVE_AMOUNT,0)-isnull( CA.PAYMENT_AMOUNT,0)),1) when '0.00' then '' else  convert(varchar(30),convert(money,isnull( CA.RESERVE_AMOUNT,0)-isnull( CA.PAYMENT_AMOUNT,0)),1) END) AS DISPLAY_OUTSTANDING_1, ";
				sFROMCLAUSE += " convert(money,CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,CA.PAYMENT_AMOUNT),1) when '0.00' then '' else  convert(varchar(30),convert(money,CA.PAYMENT_AMOUNT),1) END END) AS DISPLAY_PAID_1 ,"; 
				sFROMCLAUSE += " convert(money,CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,isnull( CA.RECOVERY,0)),1) when '0.00' then '' else convert(varchar(30),convert(money,isnull( CA.RECOVERY,0)),1) END END) AS DISPLAY_RECOVERY_1 , ";
				sFROMCLAUSE += " convert(money,CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE case convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)),1) when '0.00' then '' else " ;
				sFROMCLAUSE += " convert(varchar(30),convert(money,ISNULL(CA.CLAIM_RESERVE_AMOUNT,0) + ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)),1) END END) AS DISPLAY_INCURRED_1 , ";
				sFROMCLAUSE += " convert(money,CASE CA.ACTIVITY_REASON WHEN '11836' THEN '' ELSE (case convert(varchar(30),convert(money,CA.EXPENSES),1) when '0.00' then '' else convert(varchar(30),convert(money,CA.EXPENSES),1) end ) END) AS DISPLAY_EXPENSE_1 , ";
				sFROMCLAUSE += " convert(money,CASE ISNULL(RI_RESERVE,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.RI_RESERVE,0))),1) END) AS DISPLAY_REINSURANCE_OUTSTANDING_1 , ";
				sFROMCLAUSE += " convert(money,CASE ISNULL(LOSS_REINSURANCE_RECOVERED,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.LOSS_REINSURANCE_RECOVERED,0))),1) END) AS DISPLAY_LOSS_REINSURANCE_RECOVERED_1 , ";
				sFROMCLAUSE += " convert(money,CASE ISNULL(EXPENSE_REINSURANCE_RECOVERED,0) WHEN 0 THEN '' ELSE   CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(CA.EXPENSE_REINSURANCE_RECOVERED,0))),1) END) AS DISPLAY_EXPENSE_REINSURANCE_RECOVERED_1, "; 
				sFROMCLAUSE += " CA.ACCOUNTING_SUPPRESSED AS ACCOUNTING_SUPPRESSED";//Added ACCOUNTING_SUPPRESSED for Itrack Issue 7169
				
				sFROMCLAUSE += " FROM CLM_ACTIVITY CA ";  
				sFROMCLAUSE += " LEFT JOIN CLM_CLAIM_INFO CCI ON CCI.CLAIM_ID = CA.CLAIM_ID "; 
				sFROMCLAUSE += " LEFT JOIN MNT_LOOKUP_VALUES MLV ON CA.ACTIVITY_REASON = MLV.LOOKUP_UNIQUE_ID "; 
				sFROMCLAUSE += " LEFT JOIN MNT_LOOKUP_VALUES LV ON CA.ACTIVITY_STATUS = LV.LOOKUP_UNIQUE_ID "; 
				sFROMCLAUSE += " LEFT JOIN MNT_USER_LIST UL ON CA.CREATED_BY = UL.[USER_ID] "; 
				sFROMCLAUSE += " LEFT OUTER JOIN CLM_TYPE_DETAIL CTD ON CTD.DETAIL_TYPE_ID = CA.ACTION_ON_PAYMENT ";
				*/
				//Calculation query for LOSS_REINSURANCE_RECOVERED
				//Displaying 0 for LOSS_REINSURANCE_RECOVERED for now
				/*sFROMCLAUSE += " (SELECT ISNULL(SUM(AMOUNT),0) FROM CLM_ACTIVITY_RECOVERY WHERE TRANSACTION_CODE IN  "; 
				 * 				sFROMCLAUSE += " (SELECT DETAIL_TYPE_ID FROM CLM_TYPE_DETAIL WHERE TYPE_ID = 8 AND TRANSACTION_CODE = 11776 AND DETAIL_TYPE_ID IN (86,87,88)) "; 
				sFROMCLAUSE += " AND CLAIM_ID =  " + ClaimID ; 
				sFROMCLAUSE += " ) "; */

				//Calculation query for EXPENSE_REINSURANCE_RECOVERED
				//Displaying 0 for EXPENSE_REINSURANCE_RECOVERED for now
                /*sFROMCLAUSE += " (SELECT ISNULL(SUM(AMOUNT),0) FROM CLM_ACTIVITY_RECOVERY WHERE TRANSACTION_CODE IN  "; 
                sFROMCLAUSE += " (SELECT DETAIL_TYPE_ID FROM CLM_TYPE_DETAIL WHERE TYPE_ID = 8 AND TRANSACTION_CODE = 11776 AND DETAIL_TYPE_ID BETWEEN 89 AND 93) "; 
                sFROMCLAUSE += " AND CLAIM_ID =  " + ClaimID ; 
                sFROMCLAUSE += " ) "; */
                /*
              
                if (hidAUTHORIZE.Value=="1")
				{
					sFROMCLAUSE += " WHERE CCI.CLAIM_ID = "  + ClaimID;
					sFROMCLAUSE += " AND ACTIVITY_STATUS=" + ((int)enumClaimActivityStatus.AWAITING_AUTHORIZATION).ToString();
					sFROMCLAUSE += " AND ACTIVITY_REASON IN (" + ((int)enumActivityReason.CLAIM_PAYMENT).ToString() + "," + ((int)enumActivityReason.EXPENSE_PAYMENT).ToString() + "," + ((int)enumActivityReason.RESERVE_UPDATE).ToString() + " ) ";
					sFROMCLAUSE += " AND (RESERVE_AMOUNT<=" + RESERVE_LIMIT + " OR PAYMENT_AMOUNT<=" + PAYMENT_LIMIT + " OR EXPENSES<=" + PAYMENT_LIMIT + " )"; 
					if(chkShowAll.Checked == false)
						sFROMCLAUSE += " AND (ACTIVITY_DATE >='" + DateTime.Today.Date.AddMonths(-3).ToShortDateString() + "' )";
					sFROMCLAUSE += " AND (RESERVE_AMOUNT IS NOT NULL AND RESERVE_AMOUNT<>0 OR PAYMENT_AMOUNT IS NOT NULL AND PAYMENT_AMOUNT<>0 OR EXPENSES IS NOT NULL OR EXPENSES<>0)"  + " ) TEST";  
					lblSummaryRow.Visible = false;
				}
				else
				{
					sFROMCLAUSE += " WHERE CCI.CLAIM_ID = "  + ClaimID ; 
					if(chkShowAll.Checked == false)
						sFROMCLAUSE += " AND (ACTIVITY_DATE >='" + DateTime.Today.Date.AddMonths(-3).ToShortDateString() + "') ";
					sFROMCLAUSE += " ) TEST";
					//Setting the summary row 
					SetSummaryRow();
				}
				
			
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.SearchColumnHeadings = "Transaction Activity^Reserves/ Outstanding^Paid^Recoveries^Incurred^Expense^Reinsurance Reserves/Outstanding^Loss Reinsurance Recovered^Expense Reinsurance Recovered^Date^Added By^Status";
				objWebGrid.SearchColumnNames = "ACTIVITY_DESCRIPTION^SEARCH_OUTSTANDING^SEARCH_PAID^SEARCH_RECOVERY^SEARCH_INCURRED^SEARCH_EXPENSE^SEARCH_REINSURANCE_OUTSTANDING^SEARCH_LOSS_REINSURANCE_RECOVERED^SEARCH_EXPENSE_REINSURANCE_RECOVERED^ACTIVITY_DATE^ADDED_BY^ACTIVITY_STATUS";
				objWebGrid.SearchColumnType = "T^T^T^T^T^T^T^T^T^T^T^T";
				objWebGrid.DisplayColumnHeadings = "Transaction Activity^Reserves/ Outstanding^Paid^Recoveries^Incurred^Expense^Reinsurance Reserves/ Outstanding^Loss Reinsurance Recovered^Expense Reinsurance Recovered^Date^Added By^";
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9^10^11^12";
				//objWebGrid.DisplayColumnNames = "ACTIVITY_DATE^ACTIVITY_DESCRIPTION^DISPLAY_OUTSTANDING^DISPLAY_PAID^DISPLAY_RECOVERY^DISPLAY_INCURRED^DISPLAY_EXPENSE^DISPLAY_REINSURANCE_OUTSTANDING^LOSS_REINSURANCE_RECOVERED^EXPENSE_REINSURANCE_RECOVERED^ADDED_BY^ACTIVITY_STATUS";
				objWebGrid.DisplayColumnNames = "ACTIVITY_DESCRIPTION^DISPLAY_OUTSTANDING^DISPLAY_PAID^DISPLAY_RECOVERY^DISPLAY_INCURRED^DISPLAY_EXPENSE^DISPLAY_REINSURANCE_OUTSTANDING^DISPLAY_LOSS_REINSURANCE_RECOVERED^DISPLAY_EXPENSE_REINSURANCE_RECOVERED^ACTIVITY_DATE^ADDED_BY^ACTIVITY_STATUS";
				//objWebGrid.DisplayTextLength = "20^8^8^7^7^7^7^7^7^12^7^2";
				//objWebGrid.DisplayColumnPercent = "20^8^8^7^7^7^7^7^7^12^7^2";
				objWebGrid.DisplayTextLength = "17^8^8^7^7^7^7^7^7^10^12^2";
				objWebGrid.DisplayColumnPercent = "17^8^8^7^7^7^7^7^7^10^12^2";
				objWebGrid.PrimaryColumns = "14";
				objWebGrid.PrimaryColumnsName = "ACTIVITY_ID";
				//Modified by Asfa(19-June-2008) - iTrack #3906(Note: 1)
				objWebGrid.ColumnTypes = "B^N^N^N^N^N^N^N^N^B^B^B";
				objWebGrid.HeaderString ="Activity Details";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                */
                #endregion

                int LangID = int.Parse(GetLanguageID());
                string PolicyCurrency = GetPolicyCurrency();
                if (IsClaimVictim != "10963")
                {
                    sSELECTCLAUSE = "CA.ACTIVITY_ID, " +
                                    " ISNULL(CTDM.TYPE_DESC,CTD.DETAIL_TYPE_DESCRIPTION )AS DETAIL_TYPE_DESCRIPTION ," + // Added by santosh kumar gautam on 18 dec 2010
                        //" CTD.DETAIL_TYPE_DESCRIPTION ," +
                                    " CA.ACCOUNTING_SUPPRESSED ," +
                                    " dbo.fun_FormatCurrency (ISNULL(CA.CLAIM_RESERVE_AMOUNT,0)," + PolicyCurrency + ") AS  CLAIM_RESERVE_AMOUNT, " +
                                    " dbo.fun_FormatCurrency ( (ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0) +ISNULL(CA.TOTAL_EXPENSE,0)) ," + PolicyCurrency + ") AS  CLAIM_PAYMENT_AMOUNT, " +
                                    " dbo.fun_FormatCurrency ( (ISNULL(CA.PAYMENT_AMOUNT,0)+ ISNULL(CA.EXPENSES,0))," + PolicyCurrency + ") AS  PAYMENT_AMOUNT, " +
                                    " dbo.fun_FormatCurrency (ISNULL(CA.CLAIM_RI_RESERVE,0)," + PolicyCurrency + ") AS  CLAIM_RI_RESERVE, " +
                                    " dbo.fun_FormatCurrency (ISNULL(RI_NET_PAID_RESERVE,0)," + PolicyCurrency + ") AS  RI_PAID_RESERVE, " +
                                    " dbo.fun_FormatCurrency (ISNULL(CO_TOTAL_RESERVE_AMT,0)," + PolicyCurrency + ") AS  CO_TOTAL_RESERVE_AMT, " +
                                    " dbo.fun_FormatCurrency (ISNULL(COI_NET_PAID_RESERVE,0)," + PolicyCurrency + ") AS  CO_PAID_RESREVE, " +

                                    //" dbo.FormatDateTime(CA.ACTIVITY_DATE, 'MM/DD/YY') + ' ' + dbo.FormatDateTime(CA.ACTIVITY_DATE, 'HH:MM 12')  as ACTIVITY_DATE, " +
                                    " ISNULL(Convert(varchar,CA.ACTIVITY_DATE,case when " + LangID + "=2 then 103 else 101 end),'') AS ACTIVITY_DATE, " +
                                    " ISNULL(UL.USER_TITLE,'') + ' ' + ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS ADDED_BY, " +
                                    " CASE LV.LOOKUP_UNIQUE_ID " +
                                    " WHEN 11800  THEN '<img src=/cms//cmsweb/images1/incomplete.gif style=''Border-Width:0'' ImageAlign=absMiddle>' " +
                        //|Modified by Naveen    itrack 940
                        // did changes in grid query applied case, when  IS_VOIDED_REVERSED_ACTIVITY='10963' then 'VOIDED' will be displayed on the status field.
                                    " WHEN 11801 THEN   CASE WHEN CA.IS_VOIDED_REVERSED_ACTIVITY='10963' THEN  case when " + LangID + "=1 then 'VOIDED' ELSE 'ANULADA' end  	ELSE  '<img src=/cms//cmsweb/images1/complete.gif style=''Border-Width:0'' ImageAlign=absMiddle>' END " +
                                    " WHEN 11804 THEN  '<img src=/cms//cmsweb/images1/authenticated.gif style=''Border-Width:0'' ImageAlign=absMiddle>' " +
                                    " ELSE '<img src=/cms//cmsweb/images1/waiting.gif style=''Border-Width:0'' ImageAlign=absMiddle>' END AS ACTIVITY_STATUS, " +
                                    " LV.LOOKUP_VALUE_DESC AS ACTIVITY_STATUS1, " +
                                    " CA.IS_ACTIVE, " +
                                    " CA.ACTIVITY_ID, " +
                                    " CA.CLAIM_ID, " +
                                    " CA.ACTIVITY_REASON, " +
                                    " CA.CREATED_DATETIME," +
                                    " CA.ACTION_ON_PAYMENT, " +
                                    " CTD.ALLOW_MANUAL, " +
                                    " CTD.IS_SYSTEM_GENERATED," +
                                    " CTD.GL_POSTING_REQUIRED,  " +
                                    " CA.IS_ACTIVE, " +
                                    " CA.IS_VOIDED_REVERSED_ACTIVITY, " +
                                    " CTD.ACTIVITY_AT_VOID, " +
                                    " CTD.FOLLOWUP_ACTIVITY_AT_VOID ";

                    sFROMCLAUSE = " CLM_ACTIVITY AS CA " +
                                  " LEFT OUTER JOIN CLM_TYPE_DETAIL CTD ON CTD.DETAIL_TYPE_ID = CA.ACTION_ON_PAYMENT " +
                                  " LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL CTDM ON CTDM.DETAIL_TYPE_ID = CA.ACTION_ON_PAYMENT AND CTDM.LANG_ID=" + LangID +
                                  " LEFT JOIN MNT_USER_LIST UL ON CA.CREATED_BY = UL.[USER_ID] " +
                                  " LEFT JOIN MNT_LOOKUP_VALUES LV ON CA.ACTIVITY_STATUS = LV.LOOKUP_UNIQUE_ID  ";


                    if (hidAUTHORIZE.Value == "1")
                    {
                        sWHERECLAUSE += " CA.CLAIM_ID = " + ClaimID;
                        sWHERECLAUSE += " AND ACTIVITY_STATUS=" + ((int)enumClaimActivityStatus.AWAITING_AUTHORIZATION).ToString();
                        sWHERECLAUSE += " AND ACTIVITY_REASON IN (" + ((int)enumActivityReason.CLAIM_PAYMENT).ToString() + "," + ((int)enumActivityReason.EXPENSE_PAYMENT).ToString() + "," + ((int)enumActivityReason.RESERVE_UPDATE).ToString() + " ) ";
                        sWHERECLAUSE += " AND (RESERVE_AMOUNT<=" + RESERVE_LIMIT + " OR PAYMENT_AMOUNT<=" + PAYMENT_LIMIT + " OR EXPENSES<=" + PAYMENT_LIMIT + " )";
                        if (chkShowAll.Checked == false)
                            sWHERECLAUSE += " AND (ACTIVITY_DATE >='" + DateTime.Today.Date.AddMonths(-3).ToShortDateString() + "' )";
                        sWHERECLAUSE += " AND (RESERVE_AMOUNT IS NOT NULL AND RESERVE_AMOUNT<>0 OR PAYMENT_AMOUNT IS NOT NULL AND PAYMENT_AMOUNT<>0 OR EXPENSES IS NOT NULL OR EXPENSES<>0)";
                        lblSummaryRow.Visible = false;
                    }
                    else
                    {
                        sWHERECLAUSE += " CA.CLAIM_ID = " + ClaimID;
                        if (chkShowAll.Checked == false)
                            sWHERECLAUSE += " AND (ACTIVITY_DATE >=" + DateTime.Today.Date.AddMonths(-3).ToShortDateString() + ") ";
                        // sFROMCLAUSE += " ) TEST";
                        //Setting the summary row 
                        SetSummaryRow();

                    }
               

                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objWebGrid.SelectClause = sSELECTCLAUSE;
                objWebGrid.FromClause = sFROMCLAUSE;
                objWebGrid.WhereClause = sWHERECLAUSE;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                objWebGrid.SearchColumnNames = "ISNULL(CTDM.TYPE_DESC,CTD.DETAIL_TYPE_DESCRIPTION )";
                objWebGrid.SearchColumnType = "T^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Activity Desc^Out Reserve^Total Paid^Paid^RI Out Res^RI Paid Res^COI Out Res^COI Paid Res^Date^Added By^Status";
                objWebGrid.DisplayColumnNumbers = "2^4^5^6^7^8^9^10^11^12^13";
                objWebGrid.DisplayColumnNames = "DETAIL_TYPE_DESCRIPTION^CLAIM_RESERVE_AMOUNT^CLAIM_PAYMENT_AMOUNT^PAYMENT_AMOUNT^CLAIM_RI_RESERVE^RI_PAID_RESERVE^CO_TOTAL_RESERVE_AMT^CO_PAID_RESREVE^ACTIVITY_DATE^ADDED_BY^ACTIVITY_STATUS";
                objWebGrid.DisplayTextLength = "13^8^8^8^8^10^10^10^10^10^5";
                objWebGrid.DisplayColumnPercent = "13^8^8^8^8^10^10^10^10^10^5";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "ACTIVITY_ID";
                objWebGrid.ColumnTypes = "B^B^B^N^N^N^N^N^B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24^25^26^27^28";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");

                }
                else
                {
                    sSELECTCLAUSE = "CA.ACTIVITY_ID, " +
                                 " ISNULL(CTDM.TYPE_DESC,CTD.DETAIL_TYPE_DESCRIPTION )AS DETAIL_TYPE_DESCRIPTION ," + // Added by santosh kumar gautam on 18 dec 2010
                        //" CTD.DETAIL_TYPE_DESCRIPTION ," +
                                 " CA.ACCOUNTING_SUPPRESSED ," +
                                 " dbo.fun_FormatCurrency (ISNULL(TEMP.CLAIM_RESERVE_AMOUNT,0)," + PolicyCurrency + ") AS  CLAIM_RESERVE_AMOUNT, " +
                                 " dbo.fun_FormatCurrency ( (ISNULL(TEMP.CLAIM_PAYMENT_AMOUNT,0)) ," + PolicyCurrency + ") AS  CLAIM_PAYMENT_AMOUNT, " +
                                 " dbo.fun_FormatCurrency ( (ISNULL(TEMP.PAYMENT_AMOUNT,0))," + PolicyCurrency + ") AS  PAYMENT_AMOUNT, " +
                                 " dbo.fun_FormatCurrency (ISNULL(TEMP.CLAIM_RI_RESERVE,0)," + PolicyCurrency + ") AS  CLAIM_RI_RESERVE, " +
                                 " dbo.fun_FormatCurrency (ISNULL(ABS((SELECT SUM(RI_RESERVE_TRAN) FROM CLM_ACTIVITY CA1 " +
								 " LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR1 ON CA1.CLAIM_ID = CAR1.CLAIM_ID AND CA1.ACTIVITY_ID = CAR1.ACTIVITY_ID AND CA1.ACTION_ON_PAYMENT IN (180,181) " +
								 " WHERE CAR1.CLAIM_ID = CA.CLAIM_ID  AND CAR1.ACTIVITY_ID <= CA.ACTIVITY_ID )),0)," + PolicyCurrency + ") AS  RI_PAID_RESERVE, " +
                                 " dbo.fun_FormatCurrency (ISNULL(TEMP.CO_TOTAL_RESERVE_AMT,0)," + PolicyCurrency + ") AS  CO_TOTAL_RESERVE_AMT, " +
                                 " dbo.fun_FormatCurrency (ISNULL(ABS((SELECT SUM(CO_RESERVE_TRAN) FROM CLM_ACTIVITY CA1 " +
                                 " LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR1 ON CA1.CLAIM_ID = CAR1.CLAIM_ID AND CA1.ACTIVITY_ID = CAR1.ACTIVITY_ID AND CA1.ACTION_ON_PAYMENT IN (180,181) " +
								 " WHERE CAR1.CLAIM_ID = CA.CLAIM_ID  AND CAR1.ACTIVITY_ID <= CA.ACTIVITY_ID )),0)," + PolicyCurrency + ") AS  CO_PAID_RESREVE, " +

                                 //" dbo.FormatDateTime(CA.ACTIVITY_DATE, 'MM/DD/YY') + ' ' + dbo.FormatDateTime(CA.ACTIVITY_DATE, 'HH:MM 12')  as ACTIVITY_DATE, " +
                                 " ISNULL(Convert(varchar,CA.ACTIVITY_DATE,case when " + LangID + "=2 then 103 else 101 end),'') AS ACTIVITY_DATE, " +
                                 " ISNULL(UL.USER_TITLE,'') + ' ' + ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS ADDED_BY, " +
                                 " CASE LV.LOOKUP_UNIQUE_ID " +
                                 " WHEN 11800  THEN '<img src=/cms//cmsweb/images1/incomplete.gif style=''Border-Width:0'' ImageAlign=absMiddle>' " +
                        //|Modified by Naveen    itrack 940
                        // did changes in grid query applied case, when  IS_VOIDED_REVERSED_ACTIVITY='10963' then 'VOIDED' will be displayed on the status field.
                                 " WHEN 11801 THEN   CASE WHEN CA.IS_VOIDED_REVERSED_ACTIVITY='10963' THEN  case when " + LangID + "=1 then 'VOIDED' ELSE 'ANULADA' end  	ELSE  '<img src=/cms//cmsweb/images1/complete.gif style=''Border-Width:0'' ImageAlign=absMiddle>' END " +
                                 " WHEN 11804 THEN  '<img src=/cms//cmsweb/images1/authenticated.gif style=''Border-Width:0'' ImageAlign=absMiddle>' " +
                                 " ELSE '<img src=/cms//cmsweb/images1/waiting.gif style=''Border-Width:0'' ImageAlign=absMiddle>' END AS ACTIVITY_STATUS, " +
                                 " LV.LOOKUP_VALUE_DESC AS ACTIVITY_STATUS1, " +
                                 " CA.IS_ACTIVE, " +
                                 " CA.ACTIVITY_ID, " +
                                 " CA.CLAIM_ID, " +
                                 " CA.ACTIVITY_REASON, " +
                                 " CA.CREATED_DATETIME," +
                                 " CA.ACTION_ON_PAYMENT, " +
                                 " CTD.ALLOW_MANUAL, " +
                                 " CTD.IS_SYSTEM_GENERATED," +
                                 " CTD.GL_POSTING_REQUIRED,  " +
                                 " CA.IS_ACTIVE, " +
                                 " CA.IS_VOIDED_REVERSED_ACTIVITY, " +
                                 " CTD.ACTIVITY_AT_VOID, " +
                                 " CTD.FOLLOWUP_ACTIVITY_AT_VOID, " +
                                 " TEMP.VICTIM_ID,  " +
                                 " TEMP.VICTIM_NAME AS VICTIM_NAME " ;

                    sFROMCLAUSE = " CLM_CLAIM_INFO CCI " +
                                  " LEFT OUTER JOIN CLM_ACTIVITY CA ON CA.CLAIM_ID = CCI.CLAIM_ID " +
                                  " LEFT OUTER JOIN CLM_TYPE_DETAIL CTD ON CTD.DETAIL_TYPE_ID = CA.ACTION_ON_PAYMENT " +
                                  " LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL CTDM ON CTDM.DETAIL_TYPE_ID = CA.ACTION_ON_PAYMENT AND CTDM.LANG_ID=" + LangID +
                                  " LEFT JOIN MNT_USER_LIST UL ON CA.CREATED_BY = UL.[USER_ID] " +
                                  " LEFT JOIN MNT_LOOKUP_VALUES LV ON CA.ACTIVITY_STATUS = LV.LOOKUP_UNIQUE_ID  " +
                                  " LEFT JOIN (SELECT " +
                                  "  SUM(CAR.OUTSTANDING) AS CLAIM_RESERVE_AMOUNT," +
                                  " SUM(CAR.TOTAL_PAYMENT_AMOUNT) AS CLAIM_PAYMENT_AMOUNT, " +
                                  " SUM(CAR.PAYMENT_AMOUNT) AS PAYMENT_AMOUNT, " +
                                  " SUM(CAR.RI_RESERVE) AS CLAIM_RI_RESERVE, " +
                                  " SUM(CAR.CO_RESERVE) AS CO_TOTAL_RESERVE_AMT, " +
                                  " SUM(CAR.CO_RESERVE_TRAN) AS CO_PAID_RESREVE," +
                                  " CAR.VICTIM_ID AS VICTIM_ID,CAR.CLAIM_ID AS CLAIM_ID,CAR.ACTIVITY_ID AS ACTIVITY_ID1,CVI.NAME AS VICTIM_NAME " +
                                  " FROM CLM_ACTIVITY CA1 " +
                                  " LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR ON CA1.CLAIM_ID = CAR.CLAIM_ID AND CA1.ACTIVITY_ID = CAR.ACTIVITY_ID " +
                                  " LEFT OUTER JOIN CLM_VICTIM_INFO CVI ON CAR.CLAIM_ID = CVI.CLAIM_ID AND CAR.VICTIM_ID = CVI.VICTIM_ID " +
                                  " GROUP BY CAR.VICTIM_ID,CAR.CLAIM_ID,CAR.ACTIVITY_ID,CA1.ACTION_ON_PAYMENT,CVI.NAME " +
                                  " HAVING (CASE WHEN CA1.ACTION_ON_PAYMENT IN(165,166,167,168) THEN SUM(CAR.OUTSTANDING_TRAN)  " +
			                      "              WHEN CA1.ACTION_ON_PAYMENT IN (180,181) THEN SUM(CAR.PAYMENT_AMOUNT) " +
			                      "              WHEN CA1.ACTION_ON_PAYMENT IN (190,192) THEN SUM(CAR.RECOVERY_AMOUNT) END) <> 0 " +
			                      "              ) TEMP ON TEMP.CLAIM_ID = CCI.CLAIM_ID AND TEMP.ACTIVITY_ID1 = CA.ACTIVITY_ID "; 


                    if (hidAUTHORIZE.Value == "1")
                    {
                        sWHERECLAUSE += " CA.CLAIM_ID = " + ClaimID;
                        sWHERECLAUSE += " AND ACTIVITY_STATUS=" + ((int)enumClaimActivityStatus.AWAITING_AUTHORIZATION).ToString();
                        sWHERECLAUSE += " AND ACTIVITY_REASON IN (" + ((int)enumActivityReason.CLAIM_PAYMENT).ToString() + "," + ((int)enumActivityReason.EXPENSE_PAYMENT).ToString() + "," + ((int)enumActivityReason.RESERVE_UPDATE).ToString() + " ) ";
                        sWHERECLAUSE += " AND (RESERVE_AMOUNT<=" + RESERVE_LIMIT + " OR PAYMENT_AMOUNT<=" + PAYMENT_LIMIT + " OR EXPENSES<=" + PAYMENT_LIMIT + " )";
                        if (chkShowAll.Checked == false)
                            sWHERECLAUSE += " AND (ACTIVITY_DATE >='" + DateTime.Today.Date.AddMonths(-3).ToShortDateString() + "' )";
                        sWHERECLAUSE += " AND (RESERVE_AMOUNT IS NOT NULL AND RESERVE_AMOUNT<>0 OR PAYMENT_AMOUNT IS NOT NULL AND PAYMENT_AMOUNT<>0 OR EXPENSES IS NOT NULL OR EXPENSES<>0)";
                        lblSummaryRow.Visible = false;
                    }
                    else
                    {
                        sWHERECLAUSE += " CA.CLAIM_ID = " + ClaimID;
                        if (chkShowAll.Checked == false)
                            sWHERECLAUSE += " AND (ACTIVITY_DATE >=" + DateTime.Today.Date.AddMonths(-3).ToShortDateString() + ") ";
                        // sFROMCLAUSE += " ) TEST";
                        //Setting the summary row 
                        SetSummaryRow();

                    }


                    objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = sFROMCLAUSE;
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings2");
                    objWebGrid.SearchColumnNames = "ISNULL(CTDM.TYPE_DESC,CTD.DETAIL_TYPE_DESCRIPTION )^TEMP.VICTIM_NAME";
                    objWebGrid.SearchColumnType = "T^T";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings2");//"Activity Desc^Out Reserve^Total Paid^Paid^RI Out Res^RI Paid Res^COI Out Res^COI Paid Res^Date^Added By^Status";
                    objWebGrid.DisplayColumnNumbers = "2^4^5^6^7^8^9^10^11^12^13^29";
                    objWebGrid.DisplayColumnNames = "DETAIL_TYPE_DESCRIPTION^CLAIM_RESERVE_AMOUNT^CLAIM_PAYMENT_AMOUNT^PAYMENT_AMOUNT^CLAIM_RI_RESERVE^RI_PAID_RESERVE^CO_TOTAL_RESERVE_AMT^CO_PAID_RESREVE^ACTIVITY_DATE^ADDED_BY^ACTIVITY_STATUS^VICTIM_NAME";
                    objWebGrid.DisplayTextLength = "13^8^8^8^8^10^10^10^10^10^5^13";
                    objWebGrid.DisplayColumnPercent = "13^8^8^8^8^10^10^10^10^10^5^13";
                    objWebGrid.PrimaryColumns = "1";
                    objWebGrid.PrimaryColumnsName = "ACTIVITY_ID";
                    objWebGrid.ColumnTypes = "B^B^B^N^N^N^N^N^B^B^B^B";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24^25^26^27^28^29";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                }

                //----------------------------------------------------------------------------------------
                // WHEN CLAIM IS CLOSED AND AMOUNT TO RECOVER IS PENDING            
                //----------------------------------------------------------------------------------------
                if (ClaimStatus == CLAIM_STATUS_CLOSED)
                {
                    if (AmountToRecover!=0)
                    {
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                    }
                }
                else
                {
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                }		
				
				//objWebGrid.PageSize=1000;
				objWebGrid.PageSize=int.Parse(GetPageSize());
				//objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				objWebGrid.TotRecords = "100";
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName = "CA.IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
                objWebGrid.QueryStringColumns = "ACTIVITY_ID^ACTIVITY_REASON^ACTIVITY_STATUS^ACTION_ON_PAYMENT^ACTIVITY_STATUS1^ALLOW_MANUAL^IS_SYSTEM_GENERATED^GL_POSTING_REQUIRED^ACTIVITY_AT_VOID^FOLLOWUP_ACTIVITY_AT_VOID^IS_VOIDED_REVERSED_ACTIVITY";
                //MODIFIED BY SANTOSH KR GAUTAM ON 12 JULY 2011 (REF ITRACK :975)
                objWebGrid.OrderByClause = "CA.ACTIVITY_ID DESC ";
                objWebGrid.CellHorizontalAlign = "1^2^3^4^5^6^7";// INDEX BASED (THIS IS USED TO RIGHT ALIGN THE COLUMN DATA)
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddActivity.aspx?CLAIM_ID=" + ClaimID +"&AUTHORIZE=" + hidAUTHORIZE.Value + "&";
                TabCtl.TabTitles = objResourceMgr.GetString("TabTitles");	
				TabCtl.TabLength =150;
	
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion

			

		}
		private void GetAdjusterLimits()
		{
			DataTable dtTemp = new DataTable();
			ClsActivity objActivity = new ClsActivity();;
			try
			{
				dtTemp = objActivity.GetAdjusterLimits(GetClaimID(),GetUserId());
				if(dtTemp!=null && dtTemp.Rows.Count>0)
				{
					if(dtTemp.Rows[0]["RESERVE_LIMIT"]!=null && dtTemp.Rows[0]["RESERVE_LIMIT"].ToString()!="")
						RESERVE_LIMIT = Convert.ToDouble(dtTemp.Rows[0]["RESERVE_LIMIT"].ToString());
					if(dtTemp.Rows[0]["PAYMENT_LIMIT"]!=null && dtTemp.Rows[0]["PAYMENT_LIMIT"].ToString()!="")
						PAYMENT_LIMIT = Convert.ToDouble(dtTemp.Rows[0]["PAYMENT_LIMIT"].ToString());
				}
				
				
			}
			catch(Exception ex)
			{
				capMessage.Text = ex.Message;
				capMessage.Visible = true;
			}
//			finally
//			{
//				if(objActivity!=null)
//					objActivity.Dispose();
//				if(dtTemp!=null)
//					dtTemp.Dispose();				
//			}
		}

     

		private void SetSummaryRow()
		{
			ClsActivity objActivity = new ClsActivity();
            lblSummaryRow.Text = objActivity.GetActivitySummary(ClaimID, int.Parse(GetPolicyCurrency())).Replace("@SUMMARY@", Cms.CmsWeb.ClsMessages.GetMessage("309_1_0", "11")); ;

          
		}
		/*private void SetClaimTop()
		{
			
			strCustomerId = GetCustomerID();
			strPolicyID = GetPolicyID();
			strPolicyVersionID = GetPolicyVersionID();
			strClaimID = GetClaimID();
			strLOB_ID = GetLOBID();

			if(strCustomerId!=null && strCustomerId!="")
			{
				cltClaimTop.CustomerID = int.Parse(strCustomerId);
			}			

			if(strPolicyID!=null && strPolicyID!="")
			{
				cltClaimTop.PolicyID = int.Parse(strPolicyID);
			}

			if(strPolicyVersionID!=null && strPolicyVersionID!="")
			{
				cltClaimTop.PolicyVersionID = int.Parse(strPolicyVersionID);
			}
			if(strClaimID!=null && strClaimID!="")
			{
				cltClaimTop.ClaimID = int.Parse(strClaimID);
			}
			if(strLOB_ID!=null && strLOB_ID!="")
			{
				cltClaimTop.LobID = int.Parse(strLOB_ID);
			}
        
			cltClaimTop.ShowHeaderBand ="Claim";

			cltClaimTop.Visible = true;        
		}*/
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

		#region Check whether reserves for current claim have been added or not
		private void CheckForReserves()
		{
			//value of ""/0 indicates that reserves have not been added, need to fetch data 
			if(hidReserveAdded.Value=="" || hidReserveAdded.Value=="0")
			{
				hidReserveAdded.Value = ClsClaimsNotification.CheckForReservesAdded(ClaimID);
			}
			//if(hidReserveAdded.Value=="2")
			//	btnReserves.Visible = false;

		}
		#endregion
	}
}
