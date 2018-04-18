/******************************************************************************************
<Author					: -  Ashwani
<Start Date				: -	 4/25/2005 9:17:59 PM
<End Date				: -	
<Description			: -  Show the index page of Customer Notes.
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
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
namespace Cms.Client.aspx
{
	/// <summary>
	/// Summary description for CustomerNotesIndex 
	/// </summary>
	public class CustomerNotesIndex : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedPolicy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidClaimsPopUp;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;	
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelString;

		
	
		protected string ClaimID, ActivityID,Delstring = "";
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strLOB_ID;
		public int customer_ID;
        ResourceManager objResourceMgr = null;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
            //objResourceMgr = new ResourceManager("Cms.client.aspx.CustomerNotesIndex", Assembly.GetExecutingAssembly());
			Delstring = hidDelString.Value;
			
			if (Request["CalledFrom"] != null && Request["CalledFrom"].ToString() == "CLAIMS")
				base.ScreenId="313";
			else
				base.ScreenId="108";
            objResourceMgr = new ResourceManager("Cms.client.aspx.CustomerNotesIndex", Assembly.GetExecutingAssembly());
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

         
            //if(GetCustomerID())
			 customer_ID=GetCustomerID()=="" ?0 : int.Parse(GetCustomerID());
			
            if(customer_ID!=0)
            {
         

				if (Request["CalledFrom"] != null && Request["CalledFrom"] != "")
				{
					if(Request.QueryString["ClaimsPopUp"]!=null && Request.QueryString["ClaimsPopUp"].ToString()!="")
						hidClaimsPopUp.Value =  Request.QueryString["ClaimsPopUp"].ToString().ToUpper();

					if(Request.QueryString["SelectedPolicy"]!=null && Request.QueryString["SelectedPolicy"].ToString()!="")
						hidSelectedPolicy.Value =  Request.QueryString["SelectedPolicy"].ToString().ToUpper();

					
					hidCalledFrom.Value = Request["CalledFrom"].ToString().Trim().ToUpper();
					if (hidCalledFrom.Value == "CLAIMS")
					{
						if(GetClaimID()!=null && GetClaimID()!="")
							hidCLAIM_ID.Value = GetClaimID();
						if(hidClaimsPopUp.Value!="1")
							SetClaimTop(); 
						else
						{
							cltClaimTop.Visible = false;
						}						
					}
				}
				else
				{
					SetClientTop();
				}

				//RegisterStartupScript("showTop","<script>showTop('" + hidCalledFrom.Value + "');</script>");
			
				#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				string strSelectClause="";
				string strFromClause="";				

				#region SELECTandFROMclause
                strSelectClause += " T1.NOTES_ID,T1.CUSTOMER_ID,T1.NOTES_SUBJECT,isnull(mlv.LOOKUP_VALUE_DESC,MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC)LOOKUP_VALUE_DESC,  ";
				strSelectClause += " T1.POLICY_ID, T1.POLICY_VER_TRACKING_ID,  ";
				strSelectClause += " CASE T1.CLAIMS_ID WHEN 0 THEN '' ELSE CCI.CLAIM_NUMBER END AS CLAIMS_ID,  ";
				strSelectClause += " CONVERT(VARCHAR,T1.NOTES_DESC) AS NOTES_DESC,T1.VISIBLE_TO_AGENCY,T1.IS_ACTIVE,  ";
				strSelectClause += " T1.CREATED_BY,T1.CREATED_DATETIME,  ";
				strSelectClause += " USER_FNAME + ' ' + USER_LNAME AS USER_NAME ,  ";
				//strSelectClause += " VW.USER_NAME ,";
                strSelectClause += " CONVERT(VARCHAR,T1.LAST_UPDATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end) AS LAST_UPDATED_DATETIME,  ";
				strSelectClause += " CONVERT(VARCHAR(10),T1.POLICY_ID) + '-' + CONVERT(VARCHAR(10),T1.POLICY_VER_TRACKING_ID) POLICY_ID1 ,  ";
				strSelectClause += " case QQ_APP_POL when 'QQ' then  CQL.QQ_NUMBER    + ' - Quote'     ";
                strSelectClause += " when 'APP' then AL.APP_NUMBER + '(Ver:'+ CONVERT(VARCHAR, AL.APP_VERSION_ID) +') -'+ case when "+ ClsCommon.BL_LANG_ID + "=2 then 'Proposta' else 'Application' end";
                strSelectClause += " when 'POL' then  PL.Policy_NUMBER   + '(Ver:'+ CONVERT(VARCHAR, PL.Policy_VERSION_ID) +') -'+ + case when " + ClsCommon.BL_LANG_ID + "=2 then 'Apolice' else 'Policy' end  END AS DisplayMsg,";
				//Old data b4 this implemenattion will not be shown. It will be displayed as <BLANK>. 
				//To show text in place of <BLANK> uncomment next line
				//strSelectClause += " --else 'Old Data' ";
                strSelectClause += " isnull(QQ_APP_POL,'') as QQ_APP_POL,isnull(mlv.LOOKUP_UNIQUE_ID,MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID)as LOOKUP_UNIQUE_ID";  // ,VW.DisplayMsg";
				
				strFromClause += " CLT_CUSTOMER_NOTES T1  WITH (NOLOCK)  ";
				strFromClause += " LEFT JOIN POL_CUSTOMER_POLICY_LIST  PL 	ON T1.CUSTOMER_ID = PL.CUSTOMER_ID AND T1.POLICY_ID = PL.POLICY_ID AND T1.POLICY_VER_TRACKING_ID = PL.POLICY_VERSION_ID  ";
				strFromClause += " LEFT JOIN APP_LIST AL 	ON T1.CUSTOMER_ID = AL.CUSTOMER_ID AND T1.POLICY_ID = AL.APP_ID AND T1.POLICY_VER_TRACKING_ID = AL.APP_VERSION_ID  ";
				strFromClause += " LEFT JOIN APP_LIST AL1	ON T1.CUSTOMER_ID = AL1.CUSTOMER_ID AND T1.POLICY_ID = AL1.APP_ID AND T1.POLICY_VER_TRACKING_ID = AL1.APP_VERSION_ID  ";
				//strFromClause += " LEFT JOIN CLT_QUICKQUOTE_LIST CQL   	ON T1.CUSTOMER_ID = CQL.CUSTOMER_ID  AND CQL.QQ_APP_NUMBER =AL1.APP_NUMBER  ";
				strFromClause += " LEFT JOIN CLT_QUICKQUOTE_LIST CQL   	ON T1.CUSTOMER_ID = CQL.CUSTOMER_ID  AND CQL.QQ_ID = T1.POLICY_ID  ";
				strFromClause += " left JOIN MNT_LOOKUP_VALUES 	ON T1.NOTES_TYPE = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID    ";
                strFromClause += " LEFT JOIN MNT_USER_LIST UL	ON  T1.CREATED_BY = UL.USER_ID    ";
				strFromClause += " LEFT JOIN CLM_CLAIM_INFO CCI 	ON CCI.CLAIM_ID = T1.CLAIMS_ID  ";
                strFromClause += "left JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLV ON T1.NOTES_TYPE = MLV.LOOKUP_UNIQUE_ID and MLV.LANG_ID = " + GetLanguageID() +@"" ;
				//strFromClause += " INNER JOIN VW_CLT_NOTES_VIEW VW 	ON VW.CUSTOMER_ID = T1.CUSTOMER_ID AND T1.CREATED_BY = VW.USER_ID";
				#endregion

				objWebGrid.SelectClause = strSelectClause;
				objWebGrid.FromClause = strFromClause;
				objWebGrid.WhereClause = "t1.CUSTOMER_ID=" + customer_ID ;

				//objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//2^Add New~Single Combined List^0~1^addRecord~showList//
				if (hidCLAIM_ID.Value != "")
				{
					objWebGrid.WhereClause += " AND t1.CLAIMS_ID=" + hidCLAIM_ID.Value ;
					/*If the notes page is called from claims notification pop up window, display only those
					notes having their type as pink slip*/
					if(hidClaimsPopUp.Value=="1")
					{
						objWebGrid.WhereClause+= " AND T1.NOTES_TYPE = " + clsCustomerNotes.PinkSlipNotesTypeID;
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtonsNonCarr");//1^Add New^0^addRecord
					}
				}


                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");// "Subject^Type^Modified Date^Modified By^Quote#/Application#/Policy#^Claim^Description";//Subject^Type^Modified Date^Modified By^Quote#/Application#/Policy#^Claim^Description
                objWebGrid.SearchColumnNames = "t1.NOTES_SUBJECT^LOOKUP_UNIQUE_ID^CONVERT(VARCHAR,T1.LAST_UPDATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end)^USER_FNAME ! ' ' ! USER_LNAME^case QQ_APP_POL when 'QQ' then  CQL.QQ_NUMBER    ! ' - Quote' when 'APP' then AL.APP_NUMBER ! '(Ver:' ! CONVERT(VARCHAR, AL.APP_VERSION_ID) ! ') - Application'  when 'POL' then  PL.Policy_NUMBER   ! '(Ver:' ! CONVERT(VARCHAR, PL.Policy_VERSION_ID)  ! ') - Policy'  END^CLAIMS_ID^Convert(varchar,t1.NOTES_DESC)";  //Convert(varchar,VW.USER_NAME)^
				objWebGrid.SearchColumnType = "T^L^D^T^T^T^T";
				objWebGrid.OrderByClause = "NOTES_SUBJECT asc";
				objWebGrid.DisplayColumnNumbers = "3^4^14^13^16^7^8";
				objWebGrid.DropDownColumns          =   "^note_type^^^^^";

				objWebGrid.DisplayColumnNames = "NOTES_SUBJECT^LOOKUP_VALUE_DESC^LAST_UPDATED_DATETIME^USER_NAME^DisplayMsg^CLAIMS_ID^NOTES_DESC";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");// Subject^Type^Modified Date^Modified By^Quote#/Application#/Policy#^Claim ^Description
				objWebGrid.DisplayTextLength = "30^25^25^50^50^40^60";
				objWebGrid.DisplayColumnPercent = "10^10^15^15^15^10^15";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.NOTES_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";				
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Customer Notes";
                //string st = "abhinav " + "/";
				objWebGrid.SelectClass = colors;
//				objWebGrid.FilterLabel = "Show Complete";
//				objWebGrid.FilterColumnName = "";
//				objWebGrid.FilterValue = "";

				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "NOTES_ID";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.RequireCheckbox ="Y";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			#endregion
            
				

            }
            else
            {
                capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("115");       
                capMessage.Visible=true; 
            }


			/*if (hidCalledFrom.Value == "CLAIMS")
				TabCtl.TabURLs = "AddCustomerNotes.aspx?CalledFrom=CLAIMS&";
			else
				TabCtl.TabURLs = "AddCustomerNotes.aspx?&";*/

			TabCtl.TabURLs = "AddCustomerNotes.aspx?&CalledFrom=" + hidCalledFrom.Value + "&ClaimsPopUp=" + hidClaimsPopUp.Value + "&SelectedPolicy=" + hidSelectedPolicy.Value + "&";

            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl"); // "Customer Notes";
			TabCtl.TabLength = 150;


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
	
		
		private void SetClientTop()
		{
			cltClientTop.CustomerID = customer_ID;
			cltClientTop.Visible = true;
			cltClientTop.ShowHeaderBand = "Client";
		}

		private void SetClaimTop()
		{
			strCustomerId = GetCustomerID();
			strPolicyID = GetPolicyID();
			strPolicyVersionID = GetPolicyVersionID();
			hidCLAIM_ID.Value = GetClaimID();
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
			if(hidCLAIM_ID.Value!=null && hidCLAIM_ID.Value!="")
			{
				cltClaimTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
			}
			if(strLOB_ID!=null && strLOB_ID!="")
			{
				cltClaimTop.LobID = int.Parse(strLOB_ID);
			}
        
			cltClaimTop.ShowHeaderBand ="Claim";

			cltClaimTop.Visible = true;        
		}
	}
}