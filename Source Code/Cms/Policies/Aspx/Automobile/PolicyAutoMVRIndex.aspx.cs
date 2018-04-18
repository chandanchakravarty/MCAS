/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date				: -	9/28/2005 12:51:20 PM
<End Date				: -	
<Description				: - 	Grid to display MVR information
<Review Date				: - 
<Reviewed By			: - 	
Modification History
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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyAutoMVRIndex.
	/// </summary>
	public class PolicyAutoMVRIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Panel pnlReport;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDriverID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string strCalledFrom = "";
			


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

			/* Check the SystemID of the logged in user.
				 * If the user is not a Wolverine user then display records of that agency ONLY
				 * else the normal flow follows */
			string sWHERECLAUSE			=	"";
			string  strSystemID			=	GetSystemId();
            //Changed by Charles on 19-May-10 for Itrack 51
            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
		
			try
			{

				hidCustomerID.Value		=   Request.QueryString["Customer_Id"];
				hidAppID.Value			=	Request.QueryString["POLICY_ID"];
				hidAppVersionID.Value	=	Request.QueryString["POLICY_VERSION_ID"];
				hidDriverID.Value		=	Request.QueryString["Driver_Id"];				
				hidCalledFrom.Value		=	Request.QueryString["CalledFrom"];
				strCalledFrom			=	Request.QueryString["CalledFrom"];

				string strMvrDeath="CASE WHEN ami.MVR_DEATH = 'y' THEN 'Yes' ELSE 'No' END MVR_DEATH";
				//string strMvrDate="cast(mvr_date as varchar(12)) MVR_DATE";
				string strMvrDate=" convert(char,mvr_date,103) MVR_DATE";
				//Nov,07,2005:Sumit Chhabra:Decimal places at the end of mvr_amount have been taken off
				//string strMvrAmount=" cast(mvr_amount as decimal(20,2)) MVR_AMOUNT";
				
				sWHERECLAUSE	 =" ami.CUSTOMER_ID=" + int.Parse(hidCustomerID.Value) + " AND ami.Policy_ID=" + int.Parse(hidAppID.Value)+ " AND ami.Policy_VERSION_ID=" + int.Parse(hidAppVersionID.Value) + " AND ami.DRIVER_ID=" + int.Parse(hidDriverID.Value);
				

				//Setting web grid control properties				
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//objWebGrid.SelectClause				=	"MV.VIOLATION_DES," + strMvrDate + ",AMI.MVR_AMOUNT," + strMvrDeath + ",AMI.APP_MVR_ID";

				if((hidCalledFrom.Value=="WAT") || (hidCalledFrom.Value=="Home")) 
				{
					//objWebGrid.SelectClause				=	"MV.VIOLATION_DES AS VIOLATION_DESC," + strMvrDate + ", substring(convert(varchar(30),convert(money,MVR_AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,MVR_AMOUNT),1),0)) MVR_AMOUNT  ," + strMvrDeath + ",AMI.APP_WATER_MVR_ID,AMI.IS_ACTIVE,MT.VIOLATION_DES AS VIOLATION_TYPE";
					objWebGrid.SelectClause				=	" CASE WHEN AMI.VIOLATION_TYPE>=15000 THEN DETAILS WHEN AMI.VIOLATION_TYPE=13220 THEN MVR_DESCRIPTION ELSE MV.VIOLATION_DES END AS VIOLATION_DESC, CASE AMI.VIOLATION_TYPE WHEN 13220 THEN convert(char,MVR.mvr_date,101) ELSE convert(char,AMI.occurence_date,101) END MVR_DATE, CASE AMI.VIOLATION_TYPE WHEN 13220 THEN convert(char,MVR.mvr_date,101) ELSE convert(char,AMI.mvr_date,101) END CONV_DATE, CASE AMI.VIOLATION_TYPE WHEN 13220 THEN '0' ELSE substring(convert(varchar(30),convert(money,MVR_AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,MVR_AMOUNT),1),0)) END MVR_AMOUNT,CASE AMI.VIOLATION_TYPE WHEN 13220 THEN 'No' ELSE CASE WHEN ami.MVR_DEATH = 'y' THEN 'Yes' ELSE 'No' END END MVR_DEATH,CASE AMI.VIOLATION_TYPE WHEN 13220 THEN AMI.APP_WATER_MVR_ID ELSE AMI.APP_WATER_MVR_ID END APP_WATER_MVR_ID,AMI.IS_ACTIVE,MT.VIOLATION_DES AS VIOLATION_TYPE,AMI.POINTS_ASSIGNED as MVR_POINTS";
					objWebGrid.SelectClause				+=	" ,CONVERT(VARCHAR,isnull(AMI.POINTS_ASSIGNED,0) + isnull(AMI.ADJUST_VIOLATION_POINTS,0)) + CASE isnull(AMI.ADJUST_VIOLATION_POINTS,0) WHEN 0 THEN '' ELSE '*' END TOTAL_MVR_POINTS";


					//objWebGrid.SelectClause				=	"MV.VIOLATION_DES," + strMvrDate + ", substring(convert(varchar(30),convert(money,MVR_AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,MVR_AMOUNT),1),0)) MVR_AMOUNT  ," + strMvrDeath + ",AMI.APP_WATER_MVR_ID,AMI.IS_ACTIVE";
					//objWebGrid.FromClause			=	" POL_WATERCRAFT_MVR_INFORMATION ami join MNT_VIOLATIONS mv on ami.VIOLATION_ID=mv.VIOLATION_ID  JOIN MNT_VIOLATIONS MT ON AMI.VIOLATION_TYPE=MT.VIOLATION_ID";
					objWebGrid.FromClause			=	" POL_WATERCRAFT_MVR_INFORMATION ami left join MNT_VIOLATIONS mv on ami.VIOLATION_ID=mv.VIOLATION_ID left join MNT_VIOLATIONS MT ON AMI.VIOLATION_TYPE=MT.VIOLATION_ID LEFT JOIN MVR_EXCEPTION MVR ON AMI.VIOLATION_ID=MVR.ID ";
					//objWebGrid.FromClause			=	" POL_WATERCRAFT_MVR_INFORMATION ami join MNT_VIOLATIONS mv on ami.VIOLATION_ID=mv.VIOLATION_ID ";
					objWebGrid.PrimaryColumnsName		=	"APP_WATER_MVR_ID";
					objWebGrid.QueryStringColumns		=	"APP_WATER_MVR_ID";
				}
				else if(hidCalledFrom.Value.ToUpper()=="UMB")				
				{
					objWebGrid.SelectClause				=	"MV.VIOLATION_DES AS VIOLATION_DESC," + strMvrDate + ",  substring(convert(varchar(30),convert(money,MVR_AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,MVR_AMOUNT),1),0)) MVR_AMOUNT ," + strMvrDeath + ",AMI.POL_UMB_MVR_ID,AMI.IS_ACTIVE,MT.VIOLATION_DES AS VIOLATION_TYPE";
					//objWebGrid.SelectClause				=	"MV.VIOLATION_DES," + strMvrDate + ",  substring(convert(varchar(30),convert(money,MVR_AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,MVR_AMOUNT),1),0)) MVR_AMOUNT ," + strMvrDeath + ",AMI.POL_UMB_MVR_ID,AMI.IS_ACTIVE";
					objWebGrid.FromClause			=	" POL_UMBRELLA_MVR_INFORMATION ami join MNT_VIOLATIONS mv on ami.VIOLATION_ID=mv.VIOLATION_ID JOIN MNT_VIOLATIONS MT ON AMI.VIOLATION_TYPE=MT.VIOLATION_ID";
					//objWebGrid.FromClause			=	" POL_UMBRELLA_MVR_INFORMATION ami join MNT_VIOLATIONS mv on ami.VIOLATION_ID=mv.VIOLATION_ID ";
					objWebGrid.PrimaryColumnsName		=	"POL_UMB_MVR_ID";
					objWebGrid.QueryStringColumns		=	"POL_UMB_MVR_ID";				
				}
				else
				{
                    objWebGrid.SelectClause = "CASE WHEN AMI.VIOLATION_TYPE>=15000 THEN DETAILS WHEN AMI.VIOLATION_TYPE=13220 THEN  AMI.DETAILS ELSE AMI.DETAILS END AS VIOLATION_DESC, CASE AMI.VIOLATION_TYPE WHEN 13220 THEN CONVERT(CHAR,MVR.MVR_DATE,103) ELSE CONVERT(CHAR,AMI.OCCURENCE_DATE,103) END MVR_DATE, CASE AMI.VIOLATION_TYPE WHEN 13220 THEN convert(char,MVR.mvr_date,103) ELSE convert(char,AMI.mvr_date,103) END CONV_DATE, CASE AMI.VIOLATION_TYPE WHEN 13220 THEN '0' ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,MVR_AMOUNT),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,MVR_AMOUNT),1),0)) END MVR_AMOUNT,CASE AMI.VIOLATION_TYPE WHEN 13220 THEN 'NO' ELSE CASE WHEN AMI.MVR_DEATH = 'Y' THEN 'YES' ELSE 'NO' END END MVR_DEATH,CASE AMI.VIOLATION_TYPE WHEN 13220 THEN AMI.POL_MVR_ID ELSE AMI.POL_MVR_ID END POL_MVR_ID,AMI.IS_ACTIVE,MT.VIOLATION_DES AS VIOLATION_TYPE,AMI.POINTS_ASSIGNED AS MVR_POINTS";
					objWebGrid.SelectClause				+=	" ,CONVERT(VARCHAR,isnull(AMI.POINTS_ASSIGNED,0) + isnull(AMI.ADJUST_VIOLATION_POINTS,0)) + CASE isnull(AMI.ADJUST_VIOLATION_POINTS,0) WHEN 0 THEN '' ELSE '*' END TOTAL_MVR_POINTS";

					//objWebGrid.SelectClause				=	"MV.VIOLATION_DES," + strMvrDate + ",  substring(convert(varchar(30),convert(money,MVR_AMOUNT),1),0,charindex('.',convert(varchar(30),convert(money,MVR_AMOUNT),1),0)) MVR_AMOUNT ," + strMvrDeath + ",AMI.POL_MVR_ID,AMI.IS_ACTIVE";
					objWebGrid.FromClause			=	" POL_MVR_INFORMATION AMI LEFT JOIN MNT_VIOLATIONS MV ON AMI.VIOLATION_ID=MV.VIOLATION_ID LEFT JOIN MNT_VIOLATIONS MT ON AMI.VIOLATION_TYPE=MT.VIOLATION_ID LEFT JOIN MVR_EXCEPTION MVR ON AMI.VIOLATION_ID=MVR.ID";
					//objWebGrid.FromClause			=	" POL_MVR_INFORMATION ami join MNT_VIOLATIONS mv on ami.VIOLATION_ID=mv.VIOLATION_ID ";
					objWebGrid.PrimaryColumnsName		=	"POL_MVR_ID";
					objWebGrid.QueryStringColumns		=	"POL_MVR_ID";
				}

									

				objWebGrid.WhereClause				=	sWHERECLAUSE;

				//objWebGrid.SearchColumnHeadings		=	"Violation Description^Date^Amount^Death";
				objWebGrid.SearchColumnHeadings		=	"Violation Type^Violation Description^Occurrence Date^Conviction Date^MVR Points";
				//objWebGrid.SearchColumnNames		=	"mv.VIOLATION_DES^ami.MVR_DATE^ami.MVR_AMOUNT^CASE MVR_DEATH WHEN 'Y' THEN 'Yes' ELSE 'No' END";
				//objWebGrid.SearchColumnNames		=	"MT.VIOLATION_DES^mv.VIOLATION_DES^ami.OCCURENCE_DATE^ami.MVR_DATE^AMI.POINTS_ASSIGNED";
				objWebGrid.SearchColumnNames		=	"MT.VIOLATION_DES^mv.VIOLATION_DES^ami.OCCURENCE_DATE^ami.MVR_DATE^CONVERT(VARCHAR,isnull(AMI.POINTS_ASSIGNED,0) ! isnull(AMI.ADJUST_VIOLATION_POINTS,0)) ! CASE isnull(AMI.ADJUST_VIOLATION_POINTS,0) WHEN 0 THEN '' ELSE '*' END";
				//objWebGrid.SearchColumnType			=	"T^D^T^T";
				objWebGrid.SearchColumnType			=	"T^T^D^D^T";

				
				
				//objWebGrid.OrderByClause			=	"MT.VIOLATION_DES ASC";
				objWebGrid.OrderByClause			=	"VIOLATION_TYPE ASC";
				objWebGrid.DefaultSearch			=	"Y";

				//objWebGrid.DisplayColumnNumbers		=	"1^2^3^4";
				objWebGrid.DisplayColumnNumbers		=	"1^2^3^4^6";
				//objWebGrid.DisplayColumnNames		=	"VIOLATION_DES^MVR_DATE^MVR_AMOUNT^MVR_DEATH";
				objWebGrid.DisplayColumnNames		=	"VIOLATION_TYPE^VIOLATION_DESC^MVR_DATE^CONV_DATE^TOTAL_MVR_POINTS";
				//objWebGrid.DisplayColumnHeadings	=	"Violation Description^Date^Amount^Death";
				objWebGrid.DisplayColumnHeadings	=	"Violation Type^Violation Description^Occurrence Date^Conviction Date^MVR Points";

				//objWebGrid.DisplayTextLength		=	"100^100^50^80";
				objWebGrid.DisplayTextLength		=	"60^90^40^40^30";
				//objWebGrid.DisplayColumnPercent		=	"60^15^15^10";
				objWebGrid.DisplayColumnPercent		=	"23^23^10^10^5";
				objWebGrid.PrimaryColumns			=	"1";

				//objWebGrid.ColumnTypes				=	"B^B^B^B";
				objWebGrid.ColumnTypes				=	"B^B^B^B^B";
				objWebGrid.AllowDBLClick			=	"true";
				objWebGrid.FetchColumns				=	"1^2^3^4^5^6";
	
				objWebGrid.SearchMessage			=	"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons				=	"1^Add New^0^addRecord";
				objWebGrid.PageSize					=	int.Parse(GetPageSize());
				objWebGrid.CacheSize				=	int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString				=	"MVR Information";
				objWebGrid.SelectClass				= colors;	
				
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterColumnName = "ami.Is_Active";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
							
				
				TabCtl.TabURLs = "PolicyAutoMVR.aspx?CUSTOMER_ID=" + hidCustomerID.Value
					+ "&Policy_ID=" + hidAppID.Value
					+ "&Policy_VERSION_ID=" + hidAppVersionID.Value					
					+ "&DRIVER_ID=" + hidDriverID.Value
					+ "&CALLEDFROM=" + hidCalledFrom.Value + "&";

				TabCtl.TabURLs += "&" ;

				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
			#endregion

			#region setting screen id
			strCalledFrom=hidCalledFrom.Value;
			switch(strCalledFrom.ToUpper())
			{
				case "PPA" :
					base.ScreenId	=	"228_1";
					break;
				case "MOT" :
					base.ScreenId	=	"237_1";
					break;
				case "UMB" :
					base.ScreenId	=	"278_1";
					break;
				case "WAT" :
					base.ScreenId	=	"247_1";
					break;
				case "Home":
				case "HOME":
					//Applied ScreenID for MVR List
					//base.ScreenId	=  "149_1";
					base.ScreenId  =  "252_1";
					break;
				case "RENT":
					base.ScreenId	=  "167_1";
					break;
				default :
					base.ScreenId	=	"45_1";
					break;
			}
			#endregion

			SetWorkflow();
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

		private void SetWorkflow()
		{
			if(base.ScreenId == "228_1" || base.ScreenId == "237_1" || base.ScreenId == "247_1" || base.ScreenId == "252_1" || base.ScreenId == "278_1")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId + "_0";
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				if ( hidDriverID.Value != null && hidDriverID.Value != "" )
				{
					myWorkFlow.AddKeyValue("DRIVER_ID",hidDriverID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		

	}
}
