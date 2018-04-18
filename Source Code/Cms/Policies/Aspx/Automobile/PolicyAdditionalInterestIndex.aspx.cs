/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		09-11-2005
<End Date			: -	
<Description		: - 	Index Page for Additional Interest
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		Vijay Arora	
<Modified By		:		23-11-2005
<Purpose			:		Make changes for WaterCraft Addtional Interest
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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using System.Xml; 
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for AdditionalInterestIndex.
	/// </summary>
	public class  PolicyAdditionalInterestIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidScreenCheck;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected Cms.CmsWeb.WebControls.Menu bottomMenu;
		public string tblHeaderClass;

		
		int iVEHICLE_ID;
		int iPBOAT_ID;
		string strCalledFrom="";
		protected System.Web.UI.HtmlControls.HtmlTable tblGridTable;
		public string pageFrom="";

		private void Page_Load(object sender, System.EventArgs e)
		{
					
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
			}
		
			if (Request.QueryString["PageFrom"] != null && Request.QueryString["PageFrom"] !="")
			{
				pageFrom=Request.QueryString["PageFrom"].ToString();
				
			}
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{
				case "Home":
				case "HOME":
					if(pageFrom.ToUpper() == "HREC")
						{base.ScreenId="243_1";}
					else
						{base.ScreenId="239_2";}
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="259_2";
					break;
				case "mot":
				case "MOT":
					base.ScreenId="231_3";
					break;
				case "ppa":
				case "PPA":
					base.ScreenId="227_3";
					break;
				case "GEN":
				case "gen":
					base.ScreenId="283_3";
					break;
				case "wat":
				case "WAT":
				switch (pageFrom)
				{
					case "WWAT":
					case "wwat":
						base.ScreenId="246_4"; 
						break;
					case "HWAT":
					case "hwat":
						//Added ScreenID Watercraft for Add. Interest 
						//base.ScreenId="148_2";
						base.ScreenId="251_4"; 
						break;
					case "RWAT":
					case "rwat":
						base.ScreenId="166_2"; 
						break;
				
				}

					break;
				case "wen":
				case "WEN":				
				switch (pageFrom)
				{
					case "WWEN":
					case "wwen":
						base.ScreenId = "74_2";
						break;
					case "HWEN":
					case "hwen":
						base.ScreenId="150_2";
						break;
					case "RWEN":
					case "rwen":
						base.ScreenId="168_2";
						break;
		
				}
					break;
				case "wtr":
				case "WTR":
				
				switch (pageFrom)	
				{
					case "WWTR":
					case "wwtr":
						base.ScreenId="248_1";
						break;
					case "HWTR":
					case "hwtr":
						//Added Screen It for Trailer Add Interrest
						//base.ScreenId="151_2"; 
						base.ScreenId="253_1";
						break;
					case "RWTR":
					case "rwtr":
						base.ScreenId="169_2"; 
						break;
				}
					break;
				default:
					base.ScreenId	=	"44_3";
					break;
			}
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
				int iCUSTOMER_ID,iPOLICY_ID,iPOLICY_VERSION_ID,iDWELLING_ID,iBOAT_ID,iTRAILER_ID,iENGINE_ID,iREC_VEH_ID = 0;
				iCUSTOMER_ID		=	FetchCustomerId();
				iPOLICY_ID				=	FetchPolicyId();
				iPOLICY_VERSION_ID		=	FetchPolicyVersionId();
				iVEHICLE_ID			=	0;
				iDWELLING_ID		=	0;
				iBOAT_ID            =   0;
				iTRAILER_ID         =   0;
				iENGINE_ID          =   0;
//                //for pre render
//				iPCUSTOMER_ID		    =	    iCUSTOMER_ID;
//				iPPOLICY_ID				=		iPOLICY_ID	;
//				iPPOLICY_VERSION_ID		=		iPOLICY_VERSION_ID;
				
				
		

				
				string tmp				=	Request.QueryString["VEHICLE_ID"];
				if (tmp != null && tmp != "")
					iVEHICLE_ID			= 	Convert.ToInt32(tmp);

				tmp						=	Request.QueryString["DWELLINGID"];
				if (tmp != null && tmp != "")
					iDWELLING_ID 		= 	Convert.ToInt32(tmp);

				tmp						=	Request.QueryString["BOAT_ID1"];
				if (tmp != null && tmp != "")
					iBOAT_ID            = 	Convert.ToInt32(tmp);

				tmp						=	Request.QueryString["TRAILERID"];
				if (tmp != null && tmp != "")
					iTRAILER_ID            = 	Convert.ToInt32(tmp);

				tmp						=	Request.QueryString["ENGINE_ID"];
				if (tmp != null && tmp != "")
					iENGINE_ID            = 	Convert.ToInt32(tmp);
				// End
				iPBOAT_ID                =		iBOAT_ID ;

				tmp						 =	Request.QueryString["REC_VEH_ID"];
				if (tmp != null && tmp != "")
					iREC_VEH_ID           = 	Convert.ToInt32(tmp);


				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				if (iVEHICLE_ID != 0)
					sWHERECLAUSE						=	" a.customer_id = " + iCUSTOMER_ID + " and a.POLICY_ID = " + iPOLICY_ID + " and a.POLICY_VERSION_ID = " + iPOLICY_VERSION_ID + " and a.VEHICLE_ID = " + iVEHICLE_ID ;//+ " and a.HOLDER_ID = b.HOLDER_ID ";
				else if(iBOAT_ID!=0)
					sWHERECLAUSE						=	" a.customer_id = " + iCUSTOMER_ID + " and a.POLICY_ID = " + iPOLICY_ID + " and a.POLICY_VERSION_ID = " + iPOLICY_VERSION_ID + " and a.BOAT_ID = " + iBOAT_ID ;//+ " and a.HOLDER_ID = b.HOLDER_ID ";
				else if(iTRAILER_ID!=0)
					sWHERECLAUSE						=	" a.customer_id = " + iCUSTOMER_ID + " and a.POLICY_ID = " + iPOLICY_ID + " and a.POLICY_VERSION_ID = " + iPOLICY_VERSION_ID + " and a.TRAILER_ID = " + iTRAILER_ID; //+ " and a.HOLDER_ID = b.HOLDER_ID ";
				else if(iENGINE_ID!=0)
					sWHERECLAUSE						=	" a.customer_id = " + iCUSTOMER_ID + " and a.APP_ID = " + iPOLICY_ID + " and a.APP_VERSION_ID = " + iPOLICY_VERSION_ID + " and a.ENGINE_ID = " + iENGINE_ID ; //+ " and a.HOLDER_ID = b.HOLDER_ID ";
				else if(iDWELLING_ID!=0)
					sWHERECLAUSE						=	" a.CUSTOMER_ID = " + iCUSTOMER_ID + " and a.POLICY_ID = " + iPOLICY_ID + " and a.POLICY_VERSION_ID = " + iPOLICY_VERSION_ID + " and a.DWELLING_ID = " + iDWELLING_ID ;//+ " and a.HOLDER_ID = b.HOLDER_ID ";
				else if(iREC_VEH_ID!=0)
					sWHERECLAUSE						=	" a.CUSTOMER_ID = " + iCUSTOMER_ID + " and a.POLICY_ID = " + iPOLICY_ID + " and a.POLICY_VERSION_ID = " + iPOLICY_VERSION_ID + " and a.REC_VEH_ID = " + iREC_VEH_ID ;//+ " and a.HOLDER_ID = b.HOLDER_ID ";
				else
					sWHERECLAUSE						=	" a.CUSTOMER_ID = " + iCUSTOMER_ID + " and a.POLICY_ID = " + iPOLICY_ID + " and a.POLICY_VERSION_ID = " + iPOLICY_VERSION_ID;//+ " and a.DWELLING_ID = " + iDWELLING_ID ;//+ " and a.HOLDER_ID = b.HOLDER_ID ";
					//sWHERECLAUSE						=	" a.CUSTOMER_ID = " + iCUSTOMER_ID + " and a.APP_ID = " + iPOLICY_ID + " and a.APP_VERSION_ID = " + iPOLICY_VERSION_ID;//+ " and a.DWELLING_ID = " + iDWELLING_ID ;//+ " and a.HOLDER_ID = b.HOLDER_ID ";

				string holderName = "CASE " +  
					"WHEN a.HOLDER_ID IS NULL THEN a.HOLDER_NAME " + 
					"ELSE b.HOLDER_NAME " + 
					"END HOLDER_NAME";

				string holderSearch = "CASE " +  
					"WHEN a.HOLDER_ID IS NULL THEN a.HOLDER_NAME " + 
					"ELSE b.HOLDER_NAME " + 
					"END" ;

				if (iVEHICLE_ID != 0)
					objWebGrid.SelectClause				=	" a.POLICY_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.POLICY_VERSION_ID,a.VEHICLE_ID,a.ADD_INT_ID,c.LOOKUP_VALUE_DESC,a.IS_ACTIVE";
				else if(iBOAT_ID!=0)
					objWebGrid.SelectClause				=	" a.POLICY_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.POLICY_VERSION_ID,a.BOAT_ID,a.ADD_INT_ID,c.LOOKUP_VALUE_DESC,a.IS_ACTIVE";
				else if(iTRAILER_ID!=0)
					objWebGrid.SelectClause				=	" a.POLICY_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.POLICY_VERSION_ID,a.TRAILER_ID,a.ADD_INT_ID,c.LOOKUP_VALUE_DESC,a.IS_ACTIVE";
				else if(iENGINE_ID!=0)
					objWebGrid.SelectClause				=	" a.APP_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.APP_VERSION_ID,a.ENGINE_ID,a.ADD_INT_ID,a.IS_ACTIVE";  
				else if(iREC_VEH_ID!=0)
					objWebGrid.SelectClause				=	" a.POLICY_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.POLICY_VERSION_ID,a.REC_VEH_ID,a.ADD_INT_ID,c.LOOKUP_VALUE_DESC,a.IS_ACTIVE";
				else if(iDWELLING_ID!=0)
					objWebGrid.SelectClause				=	" a.POLICY_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.POLICY_VERSION_ID,a.DWELLING_ID,a.ADD_INT_ID,c.LOOKUP_VALUE_DESC,a.IS_ACTIVE";
				else
					objWebGrid.SelectClause				=	" a.POLICY_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.POLICY_VERSION_ID,a.ADD_INT_ID,c.LOOKUP_VALUE_DESC,a.IS_ACTIVE";
					//objWebGrid.SelectClause				=	" a.APP_ID,a.HOLDER_ID," + holderName + ",a.MEMO,a.NATURE_OF_INTEREST,a.RANK,LOAN_REF_NUMBER,a.CUSTOMER_ID,a.APP_VERSION_ID,a.ADD_INT_ID,c.LOOKUP_VALUE_DESC,a.IS_ACTIVE";
				
				if (iVEHICLE_ID != 0)
				{
					objWebGrid.FromClause				=	" POL_ADD_OTHER_INT a LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST b ON a.HOLDER_ID = b.HOLDER_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES c ON a.NATURE_OF_INTEREST=c.LOOKUP_UNIQUE_ID  ";
					objWebGrid.FilterColumnName			=	" a.IS_ACTIVE";
				}
				else if(iBOAT_ID!=0)
				{
					objWebGrid.FromClause				=	" POL_WATERCRAFT_COV_ADD_INT a LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST b ON a.HOLDER_ID = b.HOLDER_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES c ON a.NATURE_OF_INTEREST=c.LOOKUP_UNIQUE_ID ";
					objWebGrid.FilterColumnName			=	" a.IS_ACTIVE";
				}
				else if(iTRAILER_ID!=0)
				{
					objWebGrid.FromClause				=	" POL_WATERCRAFT_TRAILER_ADD_INT a LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST b ON a.HOLDER_ID = b.HOLDER_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES c ON a.NATURE_OF_INTEREST=c.LOOKUP_UNIQUE_ID ";
					objWebGrid.FilterColumnName			=	" a.IS_ACTIVE";
				}
				else if(iENGINE_ID!=0)
				{
					objWebGrid.FromClause				=	" APP_WATERCRAFT_ENG_ADD_INT a LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST b ON a.HOLDER_ID = b.HOLDER_ID ";
					objWebGrid.FilterColumnName			=	" a.IS_ACTIVE";
				}
				else if(iREC_VEH_ID!=0)
				{
					objWebGrid.FromClause				=	" POL_HOMEOWNER_REC_VEH_ADD_INT a LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST b ON a.HOLDER_ID = b.HOLDER_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES c ON a.NATURE_OF_INTEREST=c.LOOKUP_UNIQUE_ID  ";
					objWebGrid.FilterColumnName			=	" a.IS_ACTIVE";
				}
				else if(iDWELLING_ID!=0)
				{
					objWebGrid.FromClause				=	" POL_HOME_OWNER_ADD_INT a LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST b ON a.HOLDER_ID = b.HOLDER_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES c ON a.NATURE_OF_INTEREST=c.LOOKUP_UNIQUE_ID ";
					objWebGrid.FilterColumnName			=	" a.IS_ACTIVE";

				}
				else
				
					objWebGrid.FromClause				=	" POL_GENERAL_HOLDER_INTEREST a LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST b ON a.HOLDER_ID = b.HOLDER_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES c ON a.NATURE_OF_INTEREST=c.LOOKUP_UNIQUE_ID ";

				objWebGrid.FilterColumnName			=	" a.IS_ACTIVE";
				objWebGrid.WhereClause				=	sWHERECLAUSE;
				

				objWebGrid.SearchColumnHeadings		=	"Holder Name^Memo^Nature Of Interest^Loan/Reference No^Rank";
				objWebGrid.SearchColumnNames		=	holderSearch + "^MEMO^LOOKUP_VALUE_DESC^LOAN_REF_NUMBER^RANK";
				objWebGrid.SearchColumnType			=	"T^T^T^T^T";
				
				objWebGrid.OrderByClause			=	"HOLDER_NAME ASC";
				objWebGrid.DefaultSearch			=	"Y";

				objWebGrid.DisplayColumnNumbers		=	"3^4^5^7^8";
				objWebGrid.DisplayColumnNames		=	"HOLDER_NAME^MEMO^LOOKUP_VALUE_DESC^LOAN_REF_NUMBER^RANK";
				objWebGrid.DisplayColumnHeadings	=	"Holder Name^Memo^Nature Of Interest^Loan/Reference No^Rank";

				objWebGrid.DisplayTextLength		=	"20^20^20^20^20";
				objWebGrid.DisplayColumnPercent		=	"20^20^20^20^20";
				objWebGrid.PrimaryColumns			=	"1";
				objWebGrid.PrimaryColumnsName		=	"ADD_INT_ID";

				objWebGrid.RequireQuery				=	"Y";
				objWebGrid.ColumnTypes				=	"B^B^B^B^B";
				objWebGrid.AllowDBLClick			=	"true";
				objWebGrid.FetchColumns				=	"1^2^3^4^5^6^7^8^9^10^11^12^13";
	
				objWebGrid.SearchMessage			=	"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons				=	"1^Add New^0^addRecord";
				objWebGrid.PageSize					=	int.Parse(GetPageSize());
				objWebGrid.CacheSize				=	int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString				=	"Additional Interest Details";
				objWebGrid.SelectClass				= colors;	
				
				//objWebGrid.FilterLabel = "Show Complete";
				//objWebGrid.FilterColumnName = "";
				//objWebGrid.FilterValue = "";

				//specifying text to be shown for filter checkbox
				objWebGrid.FilterLabel="Include Inactive";
				//specifying column to be used for filtering
				//objWebGrid.FilterColumnName="APP_ADD_OTHER_INT.IS_ACTIVE";
				//value of filtering record
				objWebGrid.FilterValue="Y";
				
				objWebGrid.QueryStringColumns		=	"ADD_INT_ID";
				
				
				TabCtl.TabURLs = "PolicyAdditionalInterest.aspx?CUSTOMER_ID=" + iCUSTOMER_ID 
					+ "&POLICY_ID=" + iPOLICY_ID 
					+ "&POLICY_VERSION_ID=" + iPOLICY_VERSION_ID 
					+ "&VEHICLE_ID=" + iVEHICLE_ID 
					+ "&DWELLING_ID=" + iDWELLING_ID
					+ "&CalledFrom="  + strCalledFrom
					+ "&pageFrom=" + pageFrom; 
				
				if(Request.QueryString["RISK_ID"] != null &&  Request.QueryString["RISK_ID"].ToString() != "" )
				{
					TabCtl.TabURLs += "&RISK_ID=" + Request.QueryString["RISK_ID"].ToString();
				}

				if(iBOAT_ID!=0)
					TabCtl.TabURLs += "&BOAT_ID=" + iBOAT_ID;

				if(iTRAILER_ID!=0)
					TabCtl.TabURLs += "&TRAILER_ID=" + iTRAILER_ID;

				if(iENGINE_ID!=0)
					TabCtl.TabURLs += "&ENGINE_ID=" + iENGINE_ID;

				TabCtl.TabURLs += "&" ;

				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
			#endregion

			SetWorkFlowControl();
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

		/// <summary>
		/// Used to fetch the Customer ID.
		/// </summary>
		/// <returns></returns>
		private int FetchCustomerId()
		{
			if(Request.QueryString["CUSTOMER_ID"]!="" && Request.QueryString["CUSTOMER_ID"]!=null)
				return Convert.ToInt32(Request.QueryString["CUSTOMER_ID"].ToString());
			else
				return Convert.ToInt32(GetCustomerID());
		}

		/// <summary>
		/// Used to fetch the policy ID.
		/// </summary>
		/// <returns></returns>
		private int FetchPolicyId()
		{
			if(Request.QueryString["POLICY_ID"]!="" && Request.QueryString["POLICY_ID"]!=null)
				return Convert.ToInt32(Request.QueryString["POLICY_ID"].ToString());
			else
				return Convert.ToInt32(GetPolicyID());
		}

		/// <summary>
		/// Used to fetch the policy version ID.
		/// </summary>
		/// <returns></returns>
		private int FetchPolicyVersionId()
		{
			if(Request.QueryString["POLICY_VERSION_ID"]!="" && Request.QueryString["POLICY_VERSION_ID"]!=null)
				return Convert.ToInt32(Request.QueryString["POLICY_VERSION_ID"].ToString());
			else
				return Convert.ToInt32(GetPolicyVersionID());
		}

		/// <summary>
		/// Set the properties to show the Client Control.
		/// </summary>
		private void ShowClientTopControl()
		{
			int intCustomerId=FetchCustomerId();			
			cltClientTop.CustomerID = intCustomerId;
			cltClientTop.Visible = true;
			cltClientTop.ShowHeaderBand = "Policy";			
			cltClientTop.PolicyID = Convert.ToInt32(GetPolicyID());
			cltClientTop.PolicyVersionID = Convert.ToInt32(GetPolicyVersionID());
		}

		
		/// <summary>
		/// Sets the workflow control properties.
		/// </summary>
		private void SetWorkFlowControl()
		{
			myWorkFlow.IsTop	=	false;
			myWorkFlow.ScreenID	=	base.ScreenId + "_0";
			if (base.ScreenId == "239_2" || base.ScreenId == "259_2" || base.ScreenId == "231_3" || base.ScreenId == "227_3" || base.ScreenId=="246_4" || base.ScreenId=="251_4" || base.ScreenId=="166_2" || base.ScreenId=="74_2" || base.ScreenId=="150_2" || base.ScreenId=="168_2" || base.ScreenId=="248_1" || base.ScreenId=="253_1" || base.ScreenId=="283_3" || base.ScreenId=="243_1")
			{
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";

				SetOtherKeys();
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
				myWorkFlow.Display=false;
			
			
			
		}

		

		#region SetOtherKeys
		/// <summary>
		/// Sets the keys for work flow depending upon the calledfrom variable
		/// </summary>
		private void SetOtherKeys()
		{
			tblHeaderClass="tableWidthHeader";
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{
				case "HOME":
					if(Request.QueryString["DWELLING_ID"] != null || Request.QueryString["DWELLING_ID"] != "")
						myWorkFlow.AddKeyValue("DWELLING_ID",Request.QueryString["DWELLING_ID"]);

					if(Request.QueryString["REC_VEH_ID"] != null && Request.QueryString["REC_VEH_ID"] != "")
						myWorkFlow.AddKeyValue("DWELLING_ID",Request.QueryString["REC_VEH_ID"]);
					break;
				case "RENTAL":
					//tblGridTable.Attributes.Remove(""
					break;
				case "MOT":
					if(Request.QueryString["VEHICLE_ID"] != null || Request.QueryString["VEHICLE_ID"] != "")
						myWorkFlow.AddKeyValue("VEHICLE_ID",Request.QueryString["VEHICLE_ID"]);
					break;
				case "PPA":
					if(Request.QueryString["VEHICLE_ID"] != null || Request.QueryString["VEHICLE_ID"] != "")
						myWorkFlow.AddKeyValue("VEHICLE_ID",Request.QueryString["VEHICLE_ID"]);
					break;
				case "WAT":
				switch (pageFrom)
				{
					case "WWAT":
						break;
					case "HWAT":
						break;
					case "RWAT":
						break;
				}
					break;
				case "WEN":				
				switch (pageFrom)
				{
					case "WWEN":
						break;
					case "HWEN":
						break;
					case "RWEN":
						break;
				}
					break;
				case "WTR":
				switch (pageFrom)	
				{
					case "WWTR":
						break;
					case "HWTR":
						break;
					case "RWTR":
						break;
				}
					break;
				case "GEN":
					//tblHeaderClass="tableWidth";
					//For client top be visible in the case of General Liability
					//ShowClientTopControl();
					//For general liability workflow should be top most
					myWorkFlow.IsTop	=	false;
					//To enable scroll-bars in the case of General Liability
					//hidScreenCheck.Value="1";
					bottomMenu.Visible = false;
					break;
				default:
					break;
			}
			//tblGridTable.Attributes.Add("","style: Class='" + tblHeaderClass + "'");
		}
		#endregion
	}
}
