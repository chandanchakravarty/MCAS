/******************************************************************************************
<Author					: - Ravindra Gupta
<Start Date				: -	2-9-2007
<End Date				: -	
<Description			: - AJAX Handeler Implementation
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Class for showing the add deposit screen.
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
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;  
using System.Text;


namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for AJAXRequestHandler.
	/// </summary>
	public class AJAXRequestHandler : System.Web.UI.Page
	{
		private const string CUST_BALANCE = "BAL"; // Accounting >>> Custmer Payments from Agency
		private const string CUST_INFO = "CUST_INFO";
		private const string MOD_NAME = "MOD";  // Diary Setup
		private const string NSF_DB_POL = "NSF_DB_POL"; // Accounting >>> Add NSF
		private const string AI = "AI_INFO"; // Account Inquiry, Process Credit Card
		private const string ACCOUNT_BALANCE ="ACC_BAL";
		private const string LOG_OUT ="LOG_OUT";
		private const string COMM_CLASS_XML ="COMM_CLASS_XML"; // Maintenance > Commissions class
		private const string POL_CHK = "CUST_POL_CHECK"; // Customer Balance > check for invalid policy num
		private const string DEP_POL_CHK = "DEP_VALIDATE_POLNUM"; // Customer Deposits > validate DB policy 
		private const string STARTING_BAL ="START_BAL"; // Starting Bal
		//Added FOr Itrack Issue 4906
		private const string RESIN_FEES = "RESIN_FEES" ;
		private const string LATE_FEES  = "LATE_FEES";
		private const string INSTALLMENT_FEES = "INSTALLMENT_FEES";

		protected int CustID,PolID,PolVerID,stateID,vendorID;
		protected string PolNum,CalledFrom; 
		//ClsGeneralInformation objGenInfo= new ClsGeneralInformation();
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Ravindra(03-28-2009): Disable browser caching
			Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
			Response.CacheControl = "no-cache"; 
			Response.Expires = -1500;
			Response.ExpiresAbsolute= DateTime.Now.AddDays(-1);

			Response.Clear();
			Response.ContentType ="text/xml";
			string strResponse= "";
			
			if(Request.QueryString["ACTION"] == null)
			{
				strResponse= "";
			}
			#region Fetch Menu Data
			if(Request.QueryString["ACTION"]  == "POLMENU")
			{
				string lob="",agencyId="";

				if(Request.QueryString["customerID"].ToString() !="")
					CustID = int.Parse(Request.QueryString["customerID"].ToString());
				if(Request.QueryString["policyID"].ToString() !="")
					PolID = int.Parse(Request.QueryString["policyID"].ToString());
				if(Request.QueryString["policyVersionId"].ToString() !="")
					PolVerID = int.Parse(Request.QueryString["policyVersionId"].ToString());
				if(Request.QueryString["lob"].ToString() !="")
					lob = Request.QueryString["lob"].ToString();
                if (lob.ToString() == "UMB")
                    lob = "MOT";
				if(Request.QueryString["strAgencyId"].ToString() !="") 
					agencyId = Request.QueryString["strAgencyId"].ToString();

				strResponse = new CmsWeb.support.ClsMenuData().fetchPolicyMenu(PolID.ToString(),PolVerID.ToString(),CustID.ToString(),lob,agencyId);
			}
			if(Request.QueryString["ACTION"]  == "APPMENU")
			{
				string lob="",agencyId="",policyLevel="";

				if(Request.QueryString["lobString"].ToString() !="")
					lob = Request.QueryString["lobString"].ToString();
				if(Request.QueryString["PolicyLevel"].ToString() !="")
					policyLevel = Request.QueryString["PolicyLevel"].ToString();
				if(Request.QueryString["strAgencyId"].ToString() !="") 
					agencyId = Request.QueryString["strAgencyId"].ToString();
				strResponse = new CmsWeb.support.ClsMenuData().fetchLobMenu(lob,policyLevel,agencyId);
			}
			if(Request.QueryString["ACTION"]  == "DEFAULTMENU")
			{
				string agencyId="";
				if(Request.QueryString["strAgencyId"].ToString() !="") 
					agencyId = Request.QueryString["strAgencyId"].ToString();
				strResponse = new CmsWeb.support.ClsMenuData().fetchDefaultMenu(agencyId);
			}
			#endregion
			#region Fetch Customer Total Due, Min Due for 'Cutomer Payments from Agency'
			if(Request.QueryString["ACTION"]  == CUST_BALANCE)
			{
				
				if(Request.QueryString["CUSTOMER_ID"].ToString() !="")
					CustID = int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
				if(Request.QueryString["POLICY_ID"].ToString() !="")
					PolID = int.Parse(Request.QueryString["POLICY_ID"].ToString());
				if(Request.QueryString["POLICY_NUMBER"].ToString() !="")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();
				if(Request.QueryString["POLICY_VERSION_ID"].ToString() !="")
					PolVerID = int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString());
				// Called From: Policy num entered directly into textbox or entered through Lookup Window(L/T)
				if(Request.QueryString["CALLED_FROM"].ToString() !="") 
					CalledFrom = Request.QueryString["CALLED_FROM"].ToString();
				
				strResponse = ClsCustAgencyPayments.GetCustomerBal(CustID,PolID,PolVerID,PolNum,CalledFrom).ToString();
			}
			#endregion
			#region Fetch Customer Info for Credit Card Prcessing
			if(Request.QueryString["ACTION"]  == CUST_INFO)
			{

				if(Request.QueryString["POLICY_NUMBER"].ToString() !="")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();

				if(PolNum!="")
				{
					CustID = ClsCustAgencyPayments.GetCustomerId(PolNum);
					strResponse = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetCustomerNameXML(CustID);
				}

			}


			#endregion

			//Added by Shikha Dixit for itrack# 4096.
			
			#region Add Resintatement Page: Get Resintatement Fee.
			if (Request.QueryString["ACTION"] == RESIN_FEES)
			{
				if (Request.QueryString["POLICY_NUMBER"].ToString() != "")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();
				strResponse = ClsAccount.GetResintatementFee(PolNum).ToString();
			}
			#endregion
			//End of addition.
            //Added  For Itrack Issue #4906 Late_fee_screen.
			#region Add Late Fee Page: Get Late Fee.
			if (Request.QueryString["ACTION"] == LATE_FEES)
			{
				if (Request.QueryString["POLICY_NUMBER"].ToString() != "")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();
				strResponse = ClsAccount.GetLateFee(PolNum).ToString();
			}
			#endregion
			//Added For Charge Installment Fee screen			
			if (Request.QueryString["ACTION"] == INSTALLMENT_FEES)
			{
				if (Request.QueryString["POLICY_NUMBER"].ToString() != "")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();
				strResponse = ClsAccount.GetInstallmentFee(PolNum).ToString();
			}            

			#region logout
			if(Request.QueryString["ACTION"]  == LOG_OUT )
			{
				Cms.BusinessLayer.BlCommon.ClsLogin objLogin = new ClsLogin();
				if(Session["userId"]!=null && Session["userId"].ToString()!="")
					objLogin.UpdateLoggedStatus(int.Parse(Session["userId"].ToString()),"Y");
			}
			#endregion

			#region Add NSF Page: Check whether policy entered through textbox is DB policy or not
			if(Request.QueryString["ACTION"]  == NSF_DB_POL)
			{
				
				if(Request.QueryString["CUSTOMER_ID"].ToString() !="")
					CustID = int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
				if(Request.QueryString["POLICY_ID"].ToString() !="")
					PolID = int.Parse(Request.QueryString["POLICY_ID"].ToString());
				if(Request.QueryString["POLICY_NUMBER"].ToString() !="")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();
				if(Request.QueryString["POLICY_VERSION_ID"].ToString() !="")
					PolVerID = int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString());
				
				strResponse = ClsCustAgencyPayments.GetCustomerBal(PolVerID,CustID,PolID,PolNum,"T").ToString();
			}
			#endregion

			#region AI Info Page: Fetch Policy Info on basis of Policy Number
			if(Request.QueryString["ACTION"]  == AI)
			{
				if(Request.QueryString["POLICY_NUMBER"].ToString() !="")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();
				strResponse = ClsAccount.GetInfoFromPolicyNum(PolNum).ToString();
			}
			#endregion

			#region AI Info Page: Fetch Policy Info on basis of Policy Number
			if(Request.QueryString["ACTION"]  == DEP_POL_CHK)
			{
				if(Request.QueryString["POLICY_NUMBER"].ToString() !="")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();
				strResponse = ClsAccount.ValidatePolicyNum(PolNum).ToString();
			}
			#endregion
			

			#region Commissions : Fetch Commission class xml
			/*
			if(Request.QueryString["ACTION"]  == COMM_CLASS_XML)
			{
				if(Request.QueryString["STATE_ID"].ToString() !="")
				stateID = int.Parse(Request.QueryString["STATE_ID"].ToString());
				strResponse = objGenInfo.GetAllClassOnStateId(stateID);
			}
			*/
			#endregion   

			#region Account Balance
			if(Request.QueryString["ACTION"]  == ACCOUNT_BALANCE )
			{
				int AccountID=0,FiscalID=0;
				if(Request.QueryString["ACC_ID"].ToString() !="")
					AccountID = Convert.ToInt32(Request.QueryString["ACC_ID"].ToString().Trim());
				if(Request.QueryString["FISC_ID"].ToString() !="")
					FiscalID = Convert.ToInt32(Request.QueryString["FISC_ID"].ToString().Trim());	

				string DepositNumber = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(FiscalID,AccountID);
				string Balance=	Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAccountBalanceByAccountID(AccountID);
				strResponse = DepositNumber + ";" +  Balance;
			}
			#endregion
			#region Starting Balance
			if(Request.QueryString["ACTION"]  == STARTING_BAL )
			{
				int AccountID=0;
				if(Request.QueryString["ACC_ID"].ToString() !="")
					AccountID = Convert.ToInt32(Request.QueryString["ACC_ID"].ToString().Trim());

				//string DepositNumber = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(FiscalID,AccountID);
				//string Balance=	Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAccountBalanceByAccountID(AccountID);
				//strResponse = DepositNumber + ";" +  Balance;
				string endingBalance = ClsBankRconciliation.GetPreviousEndingBalance(AccountID.ToString());
				strResponse = endingBalance;
			}

			#endregion

			#region Account Balance
			if(Request.QueryString["ACTION"]  == POL_CHK )
			{
				if(Request.QueryString["CUSTOMER_ID"].ToString() !="")
					CustID = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"].ToString().Trim());
				if(Request.QueryString["POLICY_NUMBER"].ToString() !="")
					PolNum = Request.QueryString["POLICY_NUMBER"].ToString();	

				string strRetVal = ClsAccount.ValidatePolicyNumForCust(PolNum,CustID);
				strResponse = strRetVal;
			}
			#endregion

			Response.Write(strResponse);
			Response.End();
		}

	
		private void PopulateDiaryType(string ModuleID)
		{
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
