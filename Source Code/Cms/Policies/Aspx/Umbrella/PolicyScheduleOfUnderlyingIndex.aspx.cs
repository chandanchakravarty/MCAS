/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-22-2006
	<End Date				: ->
	<Description			: -> Index Page to display Underlying Policies
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History
******************************************************************************************/
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
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyScheduleOfUnderlyingIndex.
	/// </summary>
	public class PolicyScheduleOfUnderlyingIndex :Cms.Policies.policiesbase 
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId ="273_3";

			#region Fetching Color Scheme
			
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors =System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
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


			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;
			string strCustomerID			 = GetCustomerID();
			string strPolicyID = GetPolicyID();
			string strPolicyVerID=GetPolicyVersionID();

			SetWorkFlow();

            string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				//objWebGrid.SelectClause ="POLICY_LOB,P.POLICY_COMPANY AS POLICY_COMPANY,P.POLICY_NUMBER AS POLICY_NUMBER,CONVERT(VARCHAR(10),P.POLICY_START_DATE,101) AS POLICY_START_DATE ,CONVERT(VARCHAR(10),P.POLICY_EXPIRATION_DATE,101) AS POLICY_EXPIRATION_DATE,CONVERT(varchar(100),CONVERT(money,P.POLICY_PREMIUM),1) AS POLICY_PREMIUM,P.CUSTOMER_ID AS CUSTOMER_ID,C.COVERAGE_DESC AS COVERAGE_DESC,CONVERT(varchar(100),CONVERT(money,C.COVERAGE_AMOUNT),1) AS COVERAGE_AMOUNT,C.POLICY_TEXT AS POLICY_TEXT";
				//objWebGrid.SelectClause ="POLICY_LOB,P.POLICY_COMPANY AS POLICY_COMPANY,P.POLICY_NUMBER AS POLICY_NUMBER,CONVERT(VARCHAR(10),P.POLICY_START_DATE,101) AS POLICY_START_DATE ,CONVERT(VARCHAR(10),P.POLICY_EXPIRATION_DATE,101) AS POLICY_EXPIRATION_DATE,P.CUSTOMER_ID AS CUSTOMER_ID,C.COVERAGE_DESC AS COVERAGE_DESC,CONVERT(varchar(100),CONVERT(money,C.COVERAGE_AMOUNT),1) AS COVERAGE_AMOUNT,C.POLICY_TEXT AS POLICY_TEXT";
				objWebGrid.SelectClause ="POLICY_LOB,P.POLICY_COMPANY AS POLICY_COMPANY,P.POLICY_NUMBER AS POLICY_NUMBER,CONVERT(VARCHAR(10),P.POLICY_START_DATE,101) AS POLICY_START_DATE ,CONVERT(VARCHAR(10),P.POLICY_EXPIRATION_DATE,101) AS POLICY_EXPIRATION_DATE,P.CUSTOMER_ID AS CUSTOMER_ID,C.COVERAGE_DESC AS COVERAGE_DESC,C.COVERAGE_AMOUNT as COVERAGE_AMOUNT,C.POLICY_TEXT AS POLICY_TEXT";
				objWebGrid.SelectClause += " ,MLM.LOB_DESC AS LOB ";
				//objWebGrid .FromClause =" POL_UMBRELLA_UNDERLYING_POLICIES P INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES C ON P.POLICY_NUMBER= C.POLICY_NUMBER ";
				objWebGrid .FromClause =" POL_UMBRELLA_UNDERLYING_POLICIES P LEFT OUTER JOIN POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES C ON P.POLICY_NUMBER= C.POLICY_NUMBER AND P.CUSTOMER_ID = C.CUSTOMER_ID AND P.POLICY_ID = C.POLICY_ID AND P.POLICY_VERSION_ID = C.POLICY_VERSION_ID ";
				objWebGrid.FromClause += " LEFT OUTER JOIN MNT_LOB_MASTER MLM ON MLM.LOB_ID = P.POLICY_LOB " ;

				//objWebGrid.WhereClause				=	" P.CUSTOMER_ID = " + strCustomerID + " AND P.POLICY_ID = " +  strPolicyID  + " AND P.POLICY_VERSION_ID = " + strPolicyVerID ;
				objWebGrid.WhereClause=  " P.CUSTOMER_ID = " + strCustomerID + " AND P.POLICY_ID = " +  strPolicyID  + " AND P.POLICY_VERSION_ID = " + strPolicyVerID;
				
				objWebGrid.OrderByClause			=	" POLICY_LOB,POLICY_COMPANY,POLICY_NUMBER";
				
				objWebGrid.DisplayColumnNames		=	"LOB^POLICY_COMPANY^POLICY_NUMBER^POLICY_START_DATE^POLICY_EXPIRATION_DATE^COVERAGE_DESC^COVERAGE_AMOUNT";
				
				objWebGrid.DisplayColumnHeadings	=	"LOB^Company^Policy Number^Start Date^End Date^Coverage Description^Coverage Amount";
							
				objWebGrid.DisplayColumnPercent		=	"10^12^13^10^10^35^10";

				objWebGrid.SearchColumnHeadings		=	"LOB^Company^Policy Number^Effective date^Expiration date^Coverage Description^Coverage Amount";
				objWebGrid.SearchColumnNames		=	"MLM.LOB_DESC^POLICY_COMPANY^P.POLICY_NUMBER^POLICY_START_DATE^POLICY_EXPIRATION_DATE^COVERAGE_DESC^COVERAGE_AMOUNT";
				objWebGrid.SearchColumnType			=	"T^T^T^D^D^T^T";	
				objWebGrid.DisplayColumnNumbers		=	"1^2^3^4^5^7^8";
				objWebGrid.FetchColumns				=	"1^2^3^4^5^7^8";
				objWebGrid.ColumnTypes				=	"LBL^LBL^LBL^B^B^B^B";
				
				
				objWebGrid.ColumnsLink              =   " ^ ^ " ;
				objWebGrid.AllowDBLClick			=	"true";
				
				objWebGrid.PageSize					=	int.Parse(GetPageSize());
				
				objWebGrid.CacheSize				=	int.Parse(GetCacheSize());
				
				objWebGrid.HeaderString				=	"Schedule Of Underlying";
				objWebGrid.SelectClass              =   colors;

                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                
				objWebGrid.DefaultSearch			=	"Y";
				
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				
				objWebGrid.Grouping                 =	"Z";
				objWebGrid.GroupQueryColumns        =	"POLICY_LOB^POLICY_LOB^POLICY_NUMBER";
				
				objWebGrid.RequireQuery				=	"Y";
				objWebGrid.QueryStringColumns		=	"POLICY_NUMBER";
				objWebGrid.PrimaryColumns			=	"3";
				objWebGrid.PrimaryColumnsName		=	"P.POLICY_NUMBER";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		
		}

		#region SetWorkFlow Function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "273_3")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId + "_0";
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
			
		}
		#endregion

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
