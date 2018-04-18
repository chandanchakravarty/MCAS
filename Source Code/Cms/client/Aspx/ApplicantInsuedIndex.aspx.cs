/******************************************************************************************
	<Author					:   Mohit Gupta
	<Start Date				:   28/06/2005
	<End Date				: - >
	<Description			: - >
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlClient;
using System.Resources;
using System.Reflection;

namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for ApplicantInsuedIndex.
	/// </summary>
	public class ApplicantInsuedIndex : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBackToApplication;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReturnURL;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden3;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		int customerId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsActive;
		string customerType;
        ResourceManager objResourceMgr = null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
            
			base.ScreenId = "134_1";  //Changed Screen id to 134_1 from 33_0 to include it into Customer Details Permission List -Added by Sibin on 21 Oct 08
            objResourceMgr = new ResourceManager("Cms.client.Aspx.ApplicantInsuedIndex", Assembly.GetExecutingAssembly());
            // Put user code to initialize the page here
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";
			string sWHERECLAUSE	=	"";
			string isActive;
			

			if (Request.QueryString["Customer_ID"] != null || Request.QueryString["Customer_ID"] !="")
			{
				customerId=int.Parse(Request.QueryString["Customer_ID"]);
			}
			if (Request.QueryString["CUSTOMER_TYPE"] != null || Request.QueryString["CUSTOMER_TYPE"] !="")
			{
				customerType=Request.QueryString["CUSTOMER_TYPE"].ToString();
			}
			if (Request.QueryString["BACK_TO_APPLICATION"] != null && Request.QueryString["BACK_TO_APPLICATION"] !="")
			{
				hidBackToApplication.Value=Request.QueryString["BACK_TO_APPLICATION"].ToString();
			}			

			
			sWHERECLAUSE = " t1.CUSTOMER_ID = "+ customerId;

			ClsApplicantInsued.CheckCustomerIsActive(customerId,out isActive);
			if (isActive == "N")
			{
				//capMessage.Visible=true;
				//capMessage.Text="Co-Applicant for inacitve customer can not be created";
				hidIsActive.Value="N";
			}

			
			

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

			//Temporary Session to indicate that a new customer has been added.
			//Session set to null here so that message at customer page does not repeat
			if(Session["Insert"]!=null && Session["Insert"].ToString()!="")
				Session["Insert"]=null;
				
			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{		
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "APPLICANT_ID,t2.LOOKUP_VALUE_DESC TITLE,t4.ACTIVITY_DESC as Position,IsNull(FIRST_NAME,'')+ ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'') name,"
                    + "Rtrim(Ltrim(IsNull(ADDRESS1,'') + '' + case when NUMBER !='' then  IsNull(', '+NUMBER,'') else '' end + ' ' + IsNull(ADDRESS2,'') )) Address,"
                    + " CITY city,t3.STATE_CODE STATE,ZIP_CODE zip,CASE t1.IS_ACTIVE WHEN 'Y' THEN '" + objResourceMgr.GetString("Active") + "' ELSE '" + objResourceMgr.GetString("Inactive") + "' END As status,CASE IS_PRIMARY_APPLICANT WHEN '1' THEN 'Yes' ELSE 'No' END AS PrimaryApp,t1.IS_ACTIVE";

                objWebGrid.FromClause = "CLT_APPLICANT_LIST t1 left outer join MNT_LOOKUP_VALUES t2 on t1.title=t2.LOOKUP_UNIQUE_ID left outer join MNT_COUNTRY_STATE_LIST t3 on  t1.STATE=t3.STATE_ID and t1.COUNTRY=t3.COUNTRY_ID  AND ISNULL(t3.IS_ACTIVE,'Y')='Y'left outer join MNT_ACTIVITY_MASTER t4 on t1.POSITION=t4.ACTIVITY_ID  ";	
				objWebGrid.WhereClause			=	sWHERECLAUSE;

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Title^Name^Address^City^State^Zip^PrimaryApp";
                objWebGrid.SearchColumnNames = "FIRST_NAME ! IsNull(MIDDLE_NAME,'') ! IsNull(LAST_NAME,'')^ADDRESS1 ! IsNull(ADDRESS2,'')^CITY^t3.STATE_NAME^ZIP_CODE";//^IS_PRIMARY_APPLICANT";isnull(t2.LOOKUP_VALUE_DESC,'')^
                objWebGrid.SearchColumnType = "T^T^T^T^T";//^L";T^
                objWebGrid.DropDownColumns = "^^^^^";//YESNO";^
				
				
				//objWebGrid.OrderByClause = "APPLICANT_ID asc";				
                objWebGrid.OrderByClause = "name asc";
                objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                objWebGrid.DisplayColumnNames = "name^address^city^STATE^zip^status";//^PrimaryApp";TITLE^
				objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//Title^Position^Name^Address^City^State^Zip^Primary Applicant
                objWebGrid.DisplayTextLength = "15^15^15^15^15^10"; //15 ^
                objWebGrid.DisplayColumnPercent = "15^15^15^15^15^10";//15^
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "APPLICANT_ID";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B";//B^
				objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5^6";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button
				//objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
				//BacktoApplication - 1 - Back to Application Applicant page
				//BacktoApplication - 2 - Back to Policy Applicant/Named Insured page
				//BacktoApplication - 0/none - Normal call, don't display backtoApplication button
				if(hidBackToApplication.Value=="1")
				{
					objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons1");//"3^Add New~Back To Customer Assistant~Back to Application Co-Applicant^0~1~2^addRecord1~BackToCustomer~BackToApplicationPolicy";
					GetReturnURL();
				}
				else if(hidBackToApplication.Value=="2")
				{
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons2");//"3^Add New~Back To Customer Assistant~Back to Policy Named Insured^0~1~2^addRecord1~BackToCustomer~BackToApplicationPolicy";
					GetPolicyReturnURL();
				}
				else
                    objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"2^Add New~Back To Customer Assistant^0~1^addRecord1~BackToCustomer";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//Co-Applicant Details
				objWebGrid.QueryStringColumns="APPLICANT_ID";
				//specifying text to be shown for filter checkbox
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");// Include Inactive
				//specifying column to be used for filtering
				objWebGrid.FilterColumnName="t1.IS_ACTIVE";
				//value of filtering record
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery="Y";
				objWebGrid.DefaultSearch="Y";

				objWebGrid.SelectClass = colors;
				
				TabCtl.TabURLs = "AddApplicantInsued.aspx?CUSTOMER_ID=" + customerId + "&CUSTOMER_TYPE=" + customerType + "&BackToApplication=" + hidBackToApplication.Value + "&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 150;

				GridHolder.Controls.Add(objWebGrid);

			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
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
		private void GetReturnURL()
		{			
			string AppID = GetAppID();
			string AppVersionID = GetAppVersionID();
			string strLOB = GetLOBString();
			string strLOBID="1";
			switch(strLOB.ToUpper())
			{
				case "HOME":
					strLOBID="1";
					break;
				case "PPA":
					strLOBID="2";
					break;
				case "MOT":
					strLOBID="3";
					break;
				case "WAT":
					strLOBID="4";
					break;
				case "UMB":
					strLOBID="5";
					break;
				case "RENT":
					strLOBID="6";
					break;
				case "GEN":
					strLOBID="7";
					break;
			}
			if(strLOBID=="5")
				hidReturnURL.Value="/cms/Application/aspx/ApplicationTab.aspx?CUSTOMER_ID=" + customerId.ToString() + "&APP_ID=" + AppID + "&APP_VERSION_ID=" + AppVersionID + "&LOB_ID=" + strLOBID + "&TabNumber=2&transferdata=";
			else
				hidReturnURL.Value="/cms/Application/aspx/ApplicationTab.aspx?CUSTOMER_ID=" + customerId.ToString() + "&APP_ID=" + AppID + "&APP_VERSION_ID=" + AppVersionID + "&LOB_ID=" + strLOBID + "&TabNumber=1&transferdata=";
		}

		private void GetPolicyReturnURL()
		{
            //Commented for itrack -859 (By Pradeep Kushwaha on 15-04-2011)
            //string AppID =  GetAppID();
            //string AppVersionID =GetAppVersionID();
            //till here 
            string AppID = GetPolicyID(); 
            string AppVersionID = GetPolicyVersionID(); 

			string strLOB = GetLOBID();			
			hidReturnURL.Value="/cms/Policies/aspx/PolicyTab.aspx?CUSTOMER_ID=" + customerId.ToString() + "&APP_ID=" + AppID + "&APP_VERSION_ID=" + AppVersionID + "&POLICY_LOB=" + strLOB + "&TabNumber=1&transferdata=";
		}
	}
}

