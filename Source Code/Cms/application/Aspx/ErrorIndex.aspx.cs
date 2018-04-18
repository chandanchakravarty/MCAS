/******************************************************************************************
<Author					: -  Swarup
<Start Date				: -	 8-Mar-2007
<End Date				: -	
<Description			: -  Show the index page of Error Details.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
namespace Cms.Application.Aspx
{
	/// <summary>
	/// Summary description for ErrorIndex 
	/// </summary>
	public class ErrorIndex : Cms.Application.appbase	
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedPolicy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidClaimsPopUp;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelString;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidErrMsg;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;		
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		
	
		protected string ClaimID, ActivityID = "";
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strLOB_ID;
		public int customer_ID;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="108";
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

		
			if(hidDelString.Value!="")
				Delete();

				#region loading web grid control
				BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
				try
				{
					//Setting web grid control properties
					objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
					string strSelectClause="";
					string strFromClause="";				

					#region SELECTandFROMclause
					strSelectClause = "EXCEPTIONID,EXCEPTIONDATE,MESSAGE,SOURCE,EXCEPTION_TYPE";
					#region HIDE PORTION
					//strSelectClause += " T1.POLICY_ID, T1.POLICY_VER_TRACKING_ID,  ";
					//strSelectClause += " CASE T1.CLAIMS_ID WHEN 0 THEN '' ELSE CCI.CLAIM_NUMBER END AS CLAIMS_ID,  ";
					//strSelectClause += " CONVERT(VARCHAR,T1.NOTES_DESC) AS NOTES_DESC,T1.VISIBLE_TO_AGENCY,T1.IS_ACTIVE,  ";
					//strSelectClause += " T1.CREATED_BY,T1.CREATED_DATETIME,  ";
					//strSelectClause += " USER_FNAME + ' ' + USER_LNAME AS USER_NAME ,  ";
					//strSelectClause += " VW.USER_NAME ,";
					//strSelectClause += " CONVERT(VARCHAR,T1.LAST_UPDATED_DATETIME,101) AS LAST_UPDATED_DATETIME,  ";
					//strSelectClause += " CONVERT(VARCHAR(10),T1.POLICY_ID) + '-' + CONVERT(VARCHAR(10),T1.POLICY_VER_TRACKING_ID) POLICY_ID1 ,  ";
					//strSelectClause += " case QQ_APP_POL when 'QQ' then  CQL.QQ_NUMBER    + ' - Quote'     ";
					//strSelectClause += " when 'APP' then AL.APP_NUMBER + '(Ver:'+ CONVERT(VARCHAR, AL.APP_VERSION_ID) +') - Application'  ";
					//strSelectClause += " when 'POL' then  PL.Policy_NUMBER   + '(Ver:'+ CONVERT(VARCHAR, PL.Policy_VERSION_ID) +') - Policy'  END AS DisplayMsg,";
					//Old data b4 this implemenattion will not be shown. It will be displayed as <BLANK>. 
					//To show text in place of <BLANK> uncomment next line
					//strSelectClause += " --else 'Old Data' ";
					//strSelectClause += " isnull(QQ_APP_POL,'') as QQ_APP_POL,LOOKUP_UNIQUE_ID";  // ,VW.DisplayMsg";
					#endregion
					strFromClause = "EXCEPTIONLOG EL WITH (NOLOCK)";
					#region HIDE PORTION
//					strFromClause += " LEFT JOIN POL_CUSTOMER_POLICY_LIST  PL 	ON T1.CUSTOMER_ID = PL.CUSTOMER_ID AND T1.POLICY_ID = PL.POLICY_ID AND T1.POLICY_VER_TRACKING_ID = PL.POLICY_VERSION_ID  ";
//					strFromClause += " LEFT JOIN APP_LIST AL 	ON T1.CUSTOMER_ID = AL.CUSTOMER_ID AND T1.POLICY_ID = AL.APP_ID AND T1.POLICY_VER_TRACKING_ID = AL.APP_VERSION_ID  ";
//					strFromClause += " LEFT JOIN APP_LIST AL1	ON T1.CUSTOMER_ID = AL1.CUSTOMER_ID AND T1.POLICY_ID = AL1.APP_ID AND T1.POLICY_VER_TRACKING_ID = AL1.APP_VERSION_ID  ";
//					strFromClause += " LEFT JOIN CLT_QUICKQUOTE_LIST CQL   	ON T1.CUSTOMER_ID = CQL.CUSTOMER_ID  AND CQL.QQ_APP_NUMBER =AL1.APP_NUMBER  ";
//					strFromClause += " left JOIN MNT_LOOKUP_VALUES 	ON T1.NOTES_TYPE = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID    ";
//					strFromClause += " LEFT JOIN MNT_USER_LIST UL	ON  T1.CREATED_BY = UL.USER_ID    ";
//					strFromClause += " LEFT JOIN CLM_CLAIM_INFO CCI 	ON CCI.CLAIM_ID = T1.CLAIMS_ID  ";
					//strFromClause += " INNER JOIN VW_CLT_NOTES_VIEW VW 	ON VW.CUSTOMER_ID = T1.CUSTOMER_ID AND T1.CREATED_BY = VW.USER_ID";
					#endregion
					#endregion

					objWebGrid.SelectClause = strSelectClause;
					objWebGrid.FromClause = strFromClause;
					objWebGrid.WhereClause = " ";
					objWebGrid.SearchColumnHeadings = "Exception ID lesser than^Exception Date^Description^Source^Exception Type";
					objWebGrid.SearchColumnNames = "EXCEPTIONID^EXCEPTIONDATE^MESSAGE^SOURCE^EXCEPTION_TYPE";
					objWebGrid.SearchColumnType = "LT^D^T^T^T";
					objWebGrid.OrderByClause = "EXCEPTIONID DESC";
					objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
					objWebGrid.DropDownColumns          =   "^^^^";

					objWebGrid.DisplayColumnNames = "EXCEPTIONID^EXCEPTIONDATE^MESSAGE^SOURCE^EXCEPTION_TYPE";
					objWebGrid.DisplayColumnHeadings = "Exception ID^Exception DateTime^Description^Source^Exception Type";
					objWebGrid.DisplayTextLength = "15^50^100^45^70";
					objWebGrid.DisplayColumnPercent = "5^18^36^16^25";
					objWebGrid.PrimaryColumns = "1";
					objWebGrid.PrimaryColumnsName = "EXCEPTIONID";
					objWebGrid.ColumnTypes = "B^B^B^B^B";
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3^4^5";
					objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";				
					objWebGrid.PageSize = int.Parse (GetPageSize());
					objWebGrid.CacheSize = int.Parse(GetCacheSize());					
//					objWebGrid.PageSize = 10;
//					objWebGrid.CacheSize = 40;
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
					objWebGrid.HeaderString ="Exception Index" ;

					objWebGrid.SelectClass = colors;
					//				objWebGrid.FilterLabel = "Show Complete";
					//				objWebGrid.FilterColumnName = "";
					//				objWebGrid.FilterValue = "";

					objWebGrid.RequireQuery = "Y";
					objWebGrid.QueryStringColumns = "EXCEPTIONID";
					objWebGrid.DefaultSearch="Y";

					objWebGrid.ExtraButtons = "1^Delete^0^DeleteRecords";
					objWebGrid.RequireCheckbox ="Y";

					//Adding to controls to gridholder
					GridHolder.Controls.Add(objWebGrid);
				}
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				}
				#endregion
            
			TabCtl.TabURLs = "ErrorDetail.aspx?&";

			TabCtl.TabTitles = "Exception Detail";
			TabCtl.TabLength = 150;


		}

		/// <summary>

		/// Delets an exception log entry from the database

		/// </summary>

		private void Delete()
		{

			Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
			string listID=hidDelString.Value; 
			//int listID = 0;//Convert.ToInt32(this.hidKeyValues.Value);
			int result=objDiary.DeleteDiaryEntry(listID,"exceptionlog");
			lblMessage.Visible=true;
			lblMessage.Text="<br>Records have been successfully deleted";
			if(result >0)
				this.hidErrMsg.Value= "1";			
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
