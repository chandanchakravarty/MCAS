/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		06-01-2006
<End Date			: -	
<Description		: - 	Policy Process Log Index Page
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;
namespace Cms.Policies.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class PolicyProcessLogIndex : Cms.Policies.policiesbase
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        ResourceManager objResourceMgr = null;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id
			base.ScreenId	=	"224_9"; //Changed Screen id to 224_9 for 501 by Sibin on 21 Oct 08 to add it into Policy Details Permission List
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.PolicyProcessLogIndex", Assembly.GetExecutingAssembly());	

			#endregion

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
				string strCUSTOMER_ID = GetCustomerID();
				string strPolicyID = GetPolicyID();

				hidCustomerID.Value =strCUSTOMER_ID;
				hidPolicyID.Value = strPolicyID;
 
				string sSELECTCLAUSE="", sWHERECLAUSE="", sFROMCLAUSE="", sQUERYSTRING="";

                sSELECTCLAUSE = " PL.POLICY_NUMBER POLICY_NUMBER, " +
                    " case WHEN P.PROCESS_STATUS = 'ROLLBACK' THEN '' ELSE PL.POLICY_DISP_VERSION END AS POLICY_DISP_VERSION, " +
                    //"case when p.process_id = 10 then case " + GetLanguageID() + " when 2 then 'Reverter processo corretivo do usuário' else 'Rolled back Corrective User Process' end when p.process_id = 13 then case " + GetLanguageID() + " when 2 then 'Reverter processo de cancelamento' else 'Rolled back Cancellation Process' end when p.process_id = 15 then case " + GetLanguageID() + " when 2 then 'Reverter processo de endosso' else 'Rolled back Endorsement Process' end when p.process_id = 17 then case " + GetLanguageID() + " when 2 then 'Reverter processo de reativação' else 'Rolled back Reinstate Process' end when p.process_id = 19 then case " + GetLanguageID() + " when 2 then 'Reverter processo de renovação' else 'Rolled back Renewal Process' end  when p.process_id = 21 then case " + GetLanguageID() + " when 2 then 'Reverter processo de não renovação' else 'Rolled back Non-Renewal Process' end when p.process_id = 23 then  case " + GetLanguageID() + " when 2 then 'Revertida Negue Processo' else 'Rolled back Negate Process' end when p.process_id = 26 then case " + GetLanguageID() + " when 2 then 'Revertida Novos Negócios' else 'Rolled back New Business' end when p.process_id = 30 then case " + GetLanguageID() + " when 2 then 'Revertida Rescindir Processo' else 'Rolled back Rescind Process' end when p.process_id = 33 then case " + GetLanguageID() + " when 2 then 'Revertida Reescreva Processo' else 'Rolled back Rewrite Process' end " +
                    //"when p.process_id = 36 then ISNULL( Mn.PROCESS_DESC,m.PROCESS_DESC )  else ISNULL( Mn.PROCESS_DESC,m.PROCESS_DESC ) end as PROCESS_DESC," +
                    "case when p.process_id IN (10,13,15,17,19,21,23,26,30,33,36) then ISNULL( Mn.PROCESS_DESC,m.PROCESS_DESC )  else ISNULL( Mn.PROCESS_DESC,m.PROCESS_DESC ) end as PROCESS_DESC," +
                    "case " + GetLanguageID() + " when 2 then case when P.PROCESS_STATUS='ROLLBACK' then 'Revertida' else case  when  P.PROCESS_STATUS='PENDING' then 'Pendente' else case when p.PROCESS_STATUS='COMPLETE' then 'Completo' end end end else P.PROCESS_STATUS end  + CASE WHEN ISNULL(P.REVERT_BACK,'')='Y' THEN case " + GetLanguageID() + " when 2 then '(Cancelado)' else  '(Reverted)' end ELSE '' END AS PROCESS_STATUS , ISNULL(CONVERT(VARCHAR,P.EFFECTIVE_DATETIME, CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END),'') AS EFFECTIVE_DATETIME," +
                    "ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') " +
                    "AS CREATED_BY,ISNULL(Convert(varchar,P.CREATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'')AS CREATED_DATETIME, " +
                    "P.CUSTOMER_ID,P.POLICY_ID,P.POLICY_VERSION_ID,P.ROW_ID  "; //Spaces added by Charles for Itrack 6283 on 21-Aug-09
                sFROMCLAUSE = "POL_POLICY_PROCESS P left outer join POL_PROCESS_MASTER_MULTILINGUAL  Mn on Mn.PROCESS_ID = P.PROCESS_ID and Mn.LANG_ID = " + GetLanguageID() +
                                                "left outer join POL_PROCESS_MASTER M on M.PROCESS_ID = P.PROCESS_ID " +
                                                "left outer join POL_CUSTOMER_POLICY_LIST PL ON PL.CUSTOMER_ID = P.CUSTOMER_ID and PL.POLICY_ID = P.POLICY_ID  " +
                    //"AND PL.POLICY_VERSION_ID = P.new_POLICY_VERSION_ID "+
                                                "AND PL.POLICY_VERSION_ID = CASE P.PROCESS_STATUS WHEN 'ROLLBACK' THEN  P.POLICY_VERSION_ID ELSE P.NEW_POLICY_VERSION_ID END " +
                                                "left outer join MNT_USER_LIST  U ON U.USER_ID= CASE WHEN P.PROCESS_STATUS ='PENDING' THEN P.CREATED_BY ELSE P.COMPLETED_BY END ";
                sWHERECLAUSE = "P.CUSTOMER_ID = " + strCUSTOMER_ID + " AND P.POLICY_ID = " + strPolicyID + " AND M.IS_ACTIVE = 'Y'";
                sQUERYSTRING = "&POLICY_ID=" + hidPolicyID.Value;


				
//				sSELECTCLAUSE=" PL.POLICY_NUMBER,PL.POLICY_DISP_VERSION,M.PROCESS_DESC, P.PROCESS_STATUS, ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') AS CREATED_BY,CONVERT(VARCHAR(50),P.CREATED_DATETIME,109) AS CREATED_DATETIME,P.CUSTOMER_ID,P.POLICY_ID,P.POLICY_VERSION_ID,P.ROW_ID";
//				sFROMCLAUSE  = " POL_POLICY_PROCESS P, POL_PROCESS_MASTER M, MNT_USER_LIST U,POL_CUSTOMER_POLICY_LIST PL  ";
//				sWHERECLAUSE = " P.PROCESS_ID = M.PROCESS_ID AND P.CREATED_BY = U.USER_ID AND P.CUSTOMER_ID = " + strCUSTOMER_ID + " AND P.POLICY_ID = " + strPolicyID + " AND M.IS_ACTIVE = 'Y' AND P.CUSTOMER_ID = PL.CUSTOMER_ID AND P.POLICY_ID = PL.POLICY_ID AND CASE P.NEW_POLICY_VERSION_ID WHEN 0 THEN P.POLICY_VERSION_ID ELSE P.NEW_POLICY_VERSION_ID END = PL.POLICY_VERSION_ID";
//				sSELECTCLAUSE =  " case when (p.process_id = 31 or  p.process_id = 32) then PL.POLICY_NUMBER else " +
//				"CUSTOMER_ID = P.CUSTOMER_ID and POLICY_ID = P.POLICY_ID ) end as POLICY_NUMBER, " +
//				sSELECTCLAUSE = "case isnull(PL.POLICY_DISP_VERSION,'-1') when '-1' then convert(nvarchar,cast(P.new_POLICY_VERSION_ID as numeric(2,1))) "+
//					"(SELECT TOP 1 POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST WHERE " +
                //sSELECTCLAUSE =  " * ";
                //sFROMCLAUSE =   " ( Select PL.POLICY_NUMBER POLICY_NUMBER, " +
                //                " case WHEN P.PROCESS_STATUS = 'ROLLBACK' THEN '' ELSE PL.POLICY_DISP_VERSION END AS POLICY_DISP_VERSION, "+
                //                "case when p.process_id = 10 then case " + GetLanguageID() + " when 2 then 'Reverter processo corretivo do usuário' else 'Rolled back Corrective User Process' end when p.process_id = 13 then case " + GetLanguageID() + " when 2 then 'Reverter processo de cancelamento' else 'Rolled back Cancellation Process' end when p.process_id = 15 then case " + GetLanguageID() + " when 2 then 'Reverter processo de endosso' else 'Rolled back Endorsement Process' end when p.process_id = 17 then case " + GetLanguageID() + " when 2 then 'Reverter processo de reativação' else 'Rolled back Reinstate Process' end when p.process_id = 19 then case " + GetLanguageID() + " when 2 then 'Reverter processo de renovação' else 'Rolled back Renewal Process' end  when p.process_id = 21 then case " + GetLanguageID() + " when 2 then 'Reverter processo de não renovação' else 'Rolled back Non-Renewal Process' end when p.process_id = 23 then  case " + GetLanguageID() + " when 2 then 'Revertida Negue Processo' else 'Rolled back Negate Process' end when p.process_id = 26 then case " + GetLanguageID() + " when 2 then 'Revertida Novos Negócios' else 'Rolled back New Business' end when p.process_id = 30 then case " + GetLanguageID() + " when 2 then 'Revertida Rescindir Processo' else 'Rolled back Rescind Process' end when p.process_id = 33 then case " + GetLanguageID() + " when 2 then 'Revertida Reescreva Processo' else 'Rolled back Rewrite Process' end " +
                //                "when p.process_id = 36 then ISNULL( Mn.PROCESS_DESC,m.PROCESS_DESC )  else ISNULL( Mn.PROCESS_DESC,m.PROCESS_DESC ) end as PROCESS_DESC," +
                //                "case " + GetLanguageID() + " when 2 then case when P.PROCESS_STATUS='ROLLBACK' then 'Revertida' else case  when  P.PROCESS_STATUS='PENDING' then 'Pendente' else case when p.PROCESS_STATUS='COMPLETE' then 'Completo' end end end else P.PROCESS_STATUS end  + CASE WHEN ISNULL(P.REVERT_BACK,'')='Y' THEN case " + GetLanguageID() + " when 2 then '(Cancelado)' else  '(Reverted)' end ELSE '' END AS PROCESS_STATUS , ISNULL(CONVERT(VARCHAR,P.EFFECTIVE_DATETIME, CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END),'') AS EFFECTIVE_DATETIME," +
                //                "ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') " +
                //                "AS CREATED_BY,ISNULL(Convert(varchar,P.CREATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'')AS CREATED_DATETIME, "+
                //                "P.CUSTOMER_ID,P.POLICY_ID,P.POLICY_VERSION_ID,P.ROW_ID,M.IS_ACTIVE,P.NEW_POLICY_VERSION_ID,isnull(P.REVERT_BACK,'') REVERT_BACK "; //Spaces added by Charles for Itrack 6283 on 21-Aug-09
                //sFROMCLAUSE +=   " FROM POL_POLICY_PROCESS P left outer join POL_PROCESS_MASTER_MULTILINGUAL  Mn on Mn.PROCESS_ID = P.PROCESS_ID and Mn.LANG_ID = " + GetLanguageID() +
                //                "left outer join POL_PROCESS_MASTER M on M.PROCESS_ID = P.PROCESS_ID " + " AND M.IS_ACTIVE = 'Y' " +
                //                "left outer join POL_CUSTOMER_POLICY_LIST PL ON PL.CUSTOMER_ID = P.CUSTOMER_ID and PL.POLICY_ID = P.POLICY_ID  " +
                //                //"AND PL.POLICY_VERSION_ID = P.new_POLICY_VERSION_ID "+
                //                "AND PL.POLICY_VERSION_ID = CASE P.PROCESS_STATUS WHEN 'ROLLBACK' THEN  P.POLICY_VERSION_ID ELSE P.NEW_POLICY_VERSION_ID END " +
                //                "left outer join MNT_USER_LIST  U ON U.USER_ID= CASE WHEN P.PROCESS_STATUS ='PENDING' THEN P.CREATED_BY ELSE P.COMPLETED_BY END ";
                //sFROMCLAUSE += " )P ";
                //sWHERECLAUSE = "P.CUSTOMER_ID = " + strCUSTOMER_ID + " AND P.POLICY_ID = " + strPolicyID; //+ " AND P.IS_ACTIVE = 'Y'";
				//sQUERYSTRING = "&POLICY_ID=" + hidPolicyID.Value ;  
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE;
				objWebGrid.WhereClause = sWHERECLAUSE;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Policy No.^Effective Date^Version^Description^Status^Processed By^Processed DateTime";
				//objWebGrid.SearchColumnNames = "PL.POLICY_NUMBER^PL.POLICY_DISP_VERSION^M.PROCESS_DESC^P.PROCESS_STATUS^ISNULL(U.USER_TITLE,'') ! ' ' ! ISNULL(U.USER_FNAME,'') ! ' ' ! ISNULL(U.USER_LNAME,'')^CONVERT(VARCHAR(50),P.CREATED_DATETIME,109)";
				//objWebGrid.SearchColumnNames = "case when (p.process_id = 31 or  p.process_id = 32) then PL.POLICY_NUMBER else (SELECT TOP 1 POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = P.CUSTOMER_ID and POLICY_ID = P.POLICY_ID ) end^P.EFFECTIVE_DATETIME^" +
				//								"case isnull(PL.POLICY_DISP_VERSION,'-1') when '-1' then convert(nvarchar,cast(P.new_POLICY_VERSION_ID as numeric(5,1))) else PL.POLICY_DISP_VERSION end^M.PROCESS_DESC^P.PROCESS_STATUS ! CASE WHEN ISNULL(P.REVERT_BACK,'')='Y' THEN '(Reverted)' ELSE '' END^ISNULL(U.USER_TITLE,'') ! ' ' ! ISNULL(U.USER_FNAME,'') ! ' ' ! ISNULL(U.USER_LNAME,'')^CONVERT(VARCHAR(50),P.CREATED_DATETIME,100)";
                objWebGrid.SearchColumnNames = "POLICY_NUMBER^P.EFFECTIVE_DATETIME^case isnull(P.POLICY_DISP_VERSION,'-1') when '-1' then convert(nvarchar,cast(P.NEW_POLICY_VERSION_ID as numeric(5,1))) else P.POLICY_DISP_VERSION end^ISNULL(Mn.PROCESS_DESC,m.PROCESS_DESC)^P.PROCESS_STATUS ! CASE WHEN ISNULL(P.REVERT_BACK,'')='Y' THEN '(Reverted)' ELSE '' END^CREATED_BY^P.CREATED_DATETIME";
				objWebGrid.SearchColumnType = "T^D^T^T^T^T^D";
				objWebGrid.OrderByClause	="P.CREATED_DATETIME desc";
				objWebGrid.DisplayColumnNumbers = "1^5^2^3^4^6^7";
				objWebGrid.DisplayColumnNames = "POLICY_NUMBER^EFFECTIVE_DATETIME^POLICY_DISP_VERSION^PROCESS_DESC^PROCESS_STATUS^CREATED_BY^CREATED_DATETIME";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Policy No.^Effective Date^Version^Description^Status^Processed By^Processed DateTime";
				objWebGrid.DisplayTextLength = "20^20^20^40^50^35^60";
				objWebGrid.DisplayColumnPercent = "10^10^7^23^15^15^20";
				objWebGrid.PrimaryColumns = "6";
				objWebGrid.PrimaryColumnsName = "P.CREATED_DATETIME";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Process Log" ;
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show Complete";				 
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.RequireNormalCursor = "Y";
				objWebGrid.QueryStringColumns = "CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^ROW_ID";

						
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "PolicyProcessLogDetail.aspx?" + "&" + sQUERYSTRING + "&"; 
				TabCtl.TabTitles ="Process Log Detail";
				TabCtl.TabLength =150;
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