/******************************************************************************************
<Author					: - >Sumit Chhabra
<Start Date				: -	>April 27,2006
<End Date				: - >
<Description			: - >This file is being used for loading grid control to show claims notification records
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
	/// This class is used for showing grid that search and display agency records
	/// </summary>
	public class ClaimsNotificationIndex : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;		
		string sWHERECLAUSE="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_RECORD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAttachParameters;	
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;		
		protected string ClaimID, ActivityID = "";
		protected string strCustomerId,strPolicyID,strPolicyVersionID,strClaimID,strLOB_ID;
        ResourceManager objResourceMgr = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			base.ScreenId="306_1_0";
			
			// Put user code to initialize the page here

			
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
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.ClaimsNotificationIndex", Assembly.GetExecutingAssembly());
			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{	
				GetQueryStringValues();
				SetClaimTop();
				//Check for whether any record exists for current customer and policy 
				//If no record exists, let the user be redirected to add new claim
				
				bool ClaimExists = Cms.BusinessLayer.BLClaims.ClsClaims.ClaimExistsForCustomer(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value);
				if(!ClaimExists)
				{
					ClientScript.RegisterStartupScript(this.GetType(),"AddNewClaim","<script>addNewClaim();</script>");
					//dummy values given to tab  control
					TabCtl.TabURLs="";
					TabCtl.TabTitles="";
					return;
				}

                string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE= " * ";

                string LangID = GetLanguageID();
                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;

				//sFROMCLAUSE  = " ( SELECT CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID,CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,MLM.LOB_DESC, CASE CLAIMANT_INSURED WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS CLAIMANT_INSURED,PAID_LOSS,OUTSTANDING_RESERVE,RECOVERIES,CLAIMANT_NAME, CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,MLM.LOB_ID FROM CLM_CLAIM_INFO CCI LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID  ) Test ";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				
				if(sWHERECLAUSE!="") //Specifying additional columns when customer_id, policy_id and policy_version_id are available to us
				{
					//objWebGrid.SelectClause+=" ,LOB_ID";
					//sFROMCLAUSE  = " ( SELECT CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID,CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,MLM.LOB_DESC,MLM.LOB_ID, CASE CLAIMANT_INSURED WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS CLAIMANT_INSURED,PAID_LOSS,OUTSTANDING_RESERVE,RECOVERIES,CLAIMANT_NAME, CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,MLM.LOB_ID FROM CLM_CLAIM_INFO CCI LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID  WHERE " +  sWHERECLAUSE  + " ) Test ";
					//sFROMCLAUSE  = " ( SELECT CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CCI.CLAIM_ID,CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,case when cdp.dummy_policy_id is null then MLM.LOB_dESC else mlm2.lob_DESC end LOB_DESC,case when cdp.dummy_policy_id is null then MLM.LOB_ID else mlm2.LOB_ID end LOB_ID, CASE CLAIMANT_INSURED WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS CLAIMANT_INSURED,PAID_LOSS,OUTSTANDING_RESERVE,RESINSURANCE_RESERVE,PAID_EXPENSE,RECOVERIES,CLAIMANT_NAME, CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,CCI.HOMEOWNER,CCI.RECR_VEH,CCI.IN_MARINE FROM CLM_CLAIM_INFO CCI LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID left outer join clm_dummy_policy cdp on cci.dummy_policy_id = cdp.dummy_policy_id left outer join mnt_lob_master mlm2 on cdp.lob_id=mlm2.lob_id WHERE " +  sWHERECLAUSE  + "  ) Test ";					



                    sFROMCLAUSE = " ( SELECT ISNULL(Convert(varchar,LOSS_DATE,case when " + LangID + "=2 then 103 else 101 end),'') AS LOSS_DATE,  " +
                        " CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER,ISNULL(M.LOOKUP_VALUE_DESC, MLV.LOOKUP_VALUE_DESC) AS CLAIM_STATUS,PCPL.POLICY_NUMBER,CCI.CLAIMANT_NAME AS INSURED, " +
                        " dbo.fun_FormatCurrency (ISNULL(OUTSTANDING_RESERVE,0)," + BaseCurrency + ") AS  OUTSTANDING, " +
                        " dbo.fun_FormatCurrency (ISNULL(PAID_LOSS,0)," + BaseCurrency + ") AS  PAID,   " +
                        " dbo.fun_FormatCurrency (ISNULL(PAID_EXPENSE,0)," + BaseCurrency + ") AS  EXPENSE, " +
                        " dbo.fun_FormatCurrency (ISNULL(RECOVERIES,0)," + BaseCurrency + ") AS  RECOVERIES, " +
						" CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID, " + 
						" CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, CTD.DETAIL_TYPE_ID,ISNULL(CCI.HOMEOWNER,0) HOMEOWNER, " + 
						" ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE,  " +
                        " LOSS_DATE AS LOSS_DATE_1 FROM CLM_CLAIM_INFO CCI " + 
						" LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID " + 
						" AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID " + 
						" LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID " + 
						" LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID " + 
                        // Added by santosh kumar gautam on 18 dec 2010
                        " LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL M ON CCI.CLAIM_STATUS = M.LOOKUP_UNIQUE_ID AND M.LANG_ID="+LangID+ 

						" LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID WHERE " +  sWHERECLAUSE  + " ) Test ";


					TabCtl.TabURLs = "AddClaimsNotification.aspx?&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&" + "POLICY_VERSION_ID="+hidPOLICY_VERSION_ID.Value + "&";
					objWebGrid.QueryStringColumns = "CLAIM_ID^LOB_ID^HOMEOWNER^RECR_VEH^IN_MARINE^POLICY_VERSION_ID"; //Added Policy Version ID : Itrack 7012
					hidAttachParameters.Value = "1";
                    TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
					//objWebGrid.FromClause+=" INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND PCPL.POLICY_ID=CCI.POLICY_ID AND PCPL.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID INNER JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID";
				}
				else
				{
					//sFROMCLAUSE  = " ( SELECT CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID,CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,MLM.LOB_DESC,MLM.LOB_ID, CASE CLAIMANT_INSURED WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS CLAIMANT_INSURED,PAID_LOSS,OUTSTANDING_RESERVE,RECOVERIES,CLAIMANT_NAME, CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,MLM.LOB_ID FROM CLM_CLAIM_INFO CCI LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID  ) Test ";
					//sFROMCLAUSE  = " ( SELECT CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CCI.CLAIM_ID,CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE,CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,case when cdp.dummy_policy_id is null then MLM.LOB_dESC else mlm2.lob_DESC end LOB_DESC,case when cdp.dummy_policy_id is null then MLM.LOB_ID else mlm2.LOB_ID end LOB_ID, CASE CLAIMANT_INSURED WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END AS CLAIMANT_INSURED,PAID_LOSS,OUTSTANDING_RESERVE,RESINSURANCE_RESERVE,PAID_EXPENSE,RECOVERIES,CLAIMANT_NAME, CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,CCI.HOMEOWNER,CCI.RECR_VEH,CCI.IN_MARINE FROM CLM_CLAIM_INFO CCI LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID left outer join clm_dummy_policy cdp on cci.dummy_policy_id = cdp.dummy_policy_id left outer join mnt_lob_master mlm2 on cdp.lob_id=mlm2.lob_id ) Test ";


					sFROMCLAUSE  = " ( SELECT CONVERT(VARCHAR(10),LOSS_DATE,101) AS LOSS_DATE, "+
                       " CAST(CLAIM_NUMBER AS VARCHAR(10)) AS CLAIM_NUMBER,MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,PCPL.POLICY_NUMBER,CCI.CLAIMANT_NAME AS INSURED, " + 
						" substring(convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0,charindex('.',convert(varchar(30),convert(money,OUTSTANDING_RESERVE),1),0)) AS OUTSTANDING, " + 
						" substring(convert(varchar(30),convert(money,PAID_LOSS),1),0,charindex('.',convert(varchar(30),convert(money,PAID_LOSS),1),0)) AS PAID, " + 
						" substring(convert(varchar(30),convert(money,PAID_EXPENSE),1),0,charindex('.',convert(varchar(30),convert(money,PAID_EXPENSE),1),0)) AS EXPENSE, " + 
						" substring(convert(varchar(30),convert(money,RECOVERIES),1),0,charindex('.',convert(varchar(30),convert(money,RECOVERIES),1),0)) RECOVERIES, " +
						" CCI.CUSTOMER_ID,CCI.POLICY_ID,CCI.POLICY_VERSION_ID,CLAIM_ID, " + 
						" CTD.DETAIL_TYPE_DESCRIPTION AS CATASTROPHE_EVENT_CODE,CCI.IS_ACTIVE,ISNULL(MLM.LOB_ID,0) LOB_ID, CTD.DETAIL_TYPE_ID,ISNULL(CCI.HOMEOWNER,0) HOMEOWNER, " + 
						" ISNULL(CCI.RECR_VEH,0) RECR_VEH,ISNULL(CCI.IN_MARINE,0) IN_MARINE,  " +
                        " LOSS_DATE AS LOSS_DATE_1 FROM CLM_CLAIM_INFO CCI " + 
						" LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND CCI.POLICY_ID = PCPL.POLICY_ID " + 
						" AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID " + 
						" LEFT JOIN MNT_LOB_MASTER MLM ON PCPL.POLICY_LOB = MLM.LOB_ID " + 
						" LEFT JOIN MNT_LOOKUP_VALUES MLV ON CCI.CLAIM_STATUS = MLV.LOOKUP_UNIQUE_ID " + 
						" LEFT JOIN CLM_TYPE_DETAIL CTD ON CCI.CATASTROPHE_EVENT_CODE = CTD.DETAIL_TYPE_ID  ) Test ";


					TabCtl.TabURLs = "AddClaimsNotification.aspx?&";
					//TabCtl.TabURLs = "AddClaimsNotification.aspx?&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&" + "POLICY_VERSION_ID="+hidPOLICY_VERSION_ID.Value + "&";
                    TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
					objWebGrid.QueryStringColumns = "CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^CLAIM_ID^LOB_ID^HOMEOWNER^RECR_VEH^IN_MARINE";
					
				}
				objWebGrid.FromClause = sFROMCLAUSE ;

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Date of Loss^Claim #^Status^Policy #^Insured";	
                objWebGrid.SearchColumnNames = "LOSS_DATE_1^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^INSURED";
                //objWebGrid.DropDownColumns          =   "^^^^^LOB^";
				objWebGrid.SearchColumnType			=	"D^T^T^T^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Date of Loss^Claim #^Status^Policy #^Insured^Outstanding^Paid^Expense^Recovery";
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9";
				objWebGrid.DisplayColumnNames = "LOSS_DATE^CLAIM_NUMBER^CLAIM_STATUS^POLICY_NUMBER^INSURED^OUTSTANDING^PAID^EXPENSE^RECOVERIES";
				objWebGrid.DisplayTextLength = "10^10^10^10^10^10^10^10^10";
				objWebGrid.DisplayColumnPercent = "10^10^10^10^10^10^10^10^10";
				objWebGrid.PrimaryColumns = "13";
				objWebGrid.PrimaryColumnsName = "CLAIM_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Claims Notification Index" ;
				//objWebGrid.OrderByClause = "LOSS_DATE  DESC";
				objWebGrid.ColumnsLink= rootPath + "claims/aspx/ClaimsNotificationIndex.aspx?"; 
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addNewClaim";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";				
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
                objWebGrid.CellHorizontalAlign = "5^6^7^8";// INDEX BASED (THIS IS USED TO RIGHT ALIGN THE COLUMN DATA)
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				//TabCtl.TabTitles = "Claims Notification";
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion


		}

		public void GetQueryStringValues()
		{
			if(Request["CUSTOMER_ID"]!=null && Request["CUSTOMER_ID"].ToString()!="")
			{
				hidCUSTOMER_ID.Value = Request["CUSTOMER_ID"].ToString();
				SetCustomerID(hidCUSTOMER_ID.Value);
			}
			else
				hidCUSTOMER_ID.Value = GetCustomerID();

			if(Request["POLICY_ID"]!=null && Request["POLICY_ID"].ToString()!="")
			{
				hidPOLICY_ID.Value = Request["POLICY_ID"].ToString();
				SetPolicyID(hidPOLICY_ID.Value);
			}
			else
				hidPOLICY_ID.Value = GetPolicyID();

			if(Request["POLICY_VERSION_ID"]!=null && Request["POLICY_VERSION_ID"].ToString()!="")
			{
				hidPOLICY_VERSION_ID.Value = Request["POLICY_VERSION_ID"].ToString();
				SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
			}
			else
				hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

			if(Request["NEW_RECORD"]!=null && Request["NEW_RECORD"].ToString()!="")
				hidNEW_RECORD.Value = Request["NEW_RECORD"].ToString();
			else
				hidNEW_RECORD.Value = "0";

			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")
			{
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();
				SetClaimID(hidCLAIM_ID.Value);
				SetCalledFor("CLAIM");
			}
			else
				hidCLAIM_ID.Value = "0";
			if(Request["LOB_ID"]!=null && Request["LOB_ID"].ToString()!="")
			{
				hidLOB_ID.Value = Request["LOB_ID"].ToString();
				
			}
			else
				hidLOB_ID.Value = "0";
			

			if(hidCUSTOMER_ID.Value == "" || hidPOLICY_ID.Value == "" || hidPOLICY_VERSION_ID.Value == "")	
				sWHERECLAUSE = "";
			else
				sWHERECLAUSE=" CCI.CUSTOMER_ID= " + hidCUSTOMER_ID.Value + " AND CCI.POLICY_ID = " + hidPOLICY_ID.Value;// + " AND CCI.POLICY_VERSION_ID = " + hidPOLICY_VERSION_ID.Value;//Removed Policy Version ID : Itrack 7012
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
	
		private void SetClaimTop()
		{
			
			strCustomerId = hidCUSTOMER_ID.Value;
			strPolicyID = hidPOLICY_ID.Value;
			strPolicyVersionID = hidPOLICY_VERSION_ID.Value;
			//strClaimID = GetClaimID();
			strLOB_ID = hidLOB_ID.Value;

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
		}
	
	}

}

