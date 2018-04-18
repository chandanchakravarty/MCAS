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
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Text ;
using System.Globalization; 

namespace Reports.Aspx
{
	public class CustomReport : Cms.CmsWeb.cmsbase
	{
//		protected Microsoft.Samples.ReportingServices.ReportViewer ReportViewer1;
//	
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMONTH;
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPageName;
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGeneralLedgerId;
//		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFinancialYear;
        DateTimeFormatInfo DateFormatinfoBR = new CultureInfo(enumCulture.BR, true).DateTimeFormat;
        DateTimeFormatInfo DateFormatinfoUS = new CultureInfo(enumCulture.US, true).DateTimeFormat;

		private void Page_Load(object sender, System.EventArgs e)
		{
           // SetCultureThread(GetLanguageCode());

			StringBuilder sbQueryString = new StringBuilder();
            DateTime Dt;
			for(int Counter = 0 ; Counter < Request.QueryString.Count ; Counter++)
			{
				sbQueryString.Append(Request.QueryString.GetKey(Counter));
				sbQueryString.Append("=");
                if (Request.QueryString.GetKey(Counter)!=null && Request.QueryString.GetKey(Counter).ToUpper().Contains("DATE") && GetLanguageCode().ToUpper() == "PT-BR" && Request.QueryString[Counter].Trim().ToUpper() != "NULL")
                {
                    Dt = Convert.ToDateTime(Convert.ToDateTime(Request.QueryString[Counter].Trim(), DateFormatinfoBR), DateFormatinfoUS);
                    sbQueryString.Append(Dt.ToShortDateString());

                }
                else
                    sbQueryString.Append(Request.QueryString[Counter].Trim());
				sbQueryString.Append("&");
			}
			  
			string ReportViewerUrl = System.Configuration.ConfigurationSettings.AppSettings["ReportViewerUrl"];

			string QueryString =Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(sbQueryString.ToString());

			string EncryptedURL = ReportViewerUrl + "?R=" + QueryString;
            //Server.Transfer(EncryptedURL);

			Response.Redirect(EncryptedURL);

			//Commented by Ravindra(02-19-2009): 

//			string strReportServer=System.Configuration.ConfigurationSettings.AppSettings.Get("ReportServer").ToString();
//			string strReportPath=System.Configuration.ConfigurationSettings.AppSettings.Get("ReportPath").ToString();
//			string Pagename="";
//			Pagename=Request.QueryString["PageName"];
//			
//			//Pagename=Request.Form["hidPageName"];
//
//			if (Pagename == "UserList")
//			{
//				string struserid="";
//				struserid=Request.QueryString["Userid"];
//				string param1 = "&UserID=" + struserid;
//				ReportViewer1.ReportPath= strReportPath + "/UserLicense" + param1;
//			}
//				
//
//			if(Pagename == "PolicyRenewal")
//			{
//				string param1 = "&NameAddress=" + Request.QueryString["Addressid"];
//				string param2 = "&ExpirationDateStart=" + Request.QueryString["ExpirationStartDateid"];
//				string param3 = "&ExpirationDateEnd=" + Request.QueryString["ExpirationEndDateid"];
//				string param4 = "&intCLIENT_ID=" + Request.QueryString["Customerid"];
//				string param5 = "&intBrokerId=" + Request.QueryString["Agencyid"];
//				string param6 = "&UnderWriter=" + Request.QueryString["Underwriterid"];
//				string param7 = "&LOB=" + Request.QueryString["LOBid"];
//				string param8 = "&BillType=" + Request.QueryString["BillTypeid"];
//				ReportViewer1.ReportPath= strReportPath + "/RenewalList" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8;
//			}
//
//			if(Pagename == "ClientList")
//			{
//				string param1 = "&NameFormat=" + Request.QueryString["NameFormatid"];
//				string param2 = "&NameAddress=" + Request.QueryString["Addressid"];
//				string param3 = "&intCLIENTACTIVE=" + Request.QueryString["ClientTypeid"];
//				string param4 = "&intCLIENT_ID=" + Request.QueryString["Customerid"];
//				string param5 = "&intAgencyId=" + Request.QueryString["Agencyid"];
//				string param6 = "&ClientStates=" + Request.QueryString["Stateid"];
//				string param7 = "&ClientZip=" + Request.QueryString["Zipid"];
//				ReportViewer1.ReportPath= strReportPath + "/ClientList" + param1 + param2 + param3 + param4 + param5 + param6 + param7;
//			}
//
//			if(Pagename == "TodoList")
//			{
//				string param1 = "&NameFormat=" + Request.QueryString["NameFormatid"];
//				string param2 = "&AddressonReport=" + Request.QueryString["Addressid"];
//				string param3 = "&UnderWriter=" + Request.QueryString["Underwriterid"];
//				string param4 = "&FromDate=" + Request.QueryString["StartDateid"];
//				string param5 = "&EndDate=" + Request.QueryString["EndDateid"];
//				ReportViewer1.ReportPath= strReportPath + "/TodoList" + param1 + param2 + param3 + param4 + param5;
//			}
//
//			if(Pagename == "AgentList")
//			{				
//				string param1 = "&Hierarchy=" + Request.QueryString["HierarchySelectedid"];
//				string param2 = "&AgentID=" + Request.QueryString["Agencyid"];
//				
//				//string param1= "&Hierarchy=" + Request.Form["hidAGENCY_ID"]; 
//				//string param2= "&AgentID=" + Request.Form["hidHierarchySelected"];
//				ReportViewer1.ReportPath= strReportPath + "/AgentList" + param1 + param2;				
//			}
//
//			if(Pagename == "AgentCommission")
//			{
//				string stragentid="";
//				stragentid=Request.QueryString["Agentid"];
//				string param1 = "&AgencyID=" + stragentid;
//				ReportViewer1.ReportPath= strReportPath + "/AgentCommissions" + param1;
//			}
//
//			if(Pagename == "PolicyExpiration")
//			{
//				string param1 = "&NameAddress=" + Request.QueryString["Addressid"];
//				string param2 = "&ExpirationDateStart=" + Request.QueryString["ExpirationStartDateid"];
//				string param3 = "&ExpirationDateEnd=" + Request.QueryString["ExpirationEndDateid"];
//				string param4 = "&intCLIENT_ID=" + Request.QueryString["Customerid"];
//				string param5 = "&intBrokerId=" + Request.QueryString["Agencyid"];
//				string param6 = "&UnderWriter=" + Request.QueryString["Underwriterid"];
//				string param7 = "&LOB=" + Request.QueryString["LOBid"];
//				string param8 = "&BillType=" + Request.QueryString["BillTypeid"];
//				string param9 = "&PolicyStatus=" + Request.QueryString["PolicyStatusid"];
//				ReportViewer1.ReportPath= strReportPath + "/PolicyExpiration" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8 + param9;
//			}
//
//			if(Pagename == "ClientPolicyList")
//			{
//				string param1 = "&NameAddress=" + Request.QueryString["Addressid"];
//				string param2 = "&InceptionDateStart=" + Request.QueryString["InceptionStartDateid"];
//				string param3 = "&InceptionDateEnd=" + Request.QueryString["InceptionEndDateid"];
//				string param4 = "&EffectiveDateStart=" + Request.QueryString["EffectiveStartDateid"];
//				string param5 = "&EffectiveDateEnd=" + Request.QueryString["EffectiveEndDateid"];
//				string param6 = "&ExpirationDateStart=" + Request.QueryString["ExpirationStartDateid"];
//				string param7 = "&ExpirationDateEnd=" + Request.QueryString["ExpirationEndDateid"];
//				string param8 = "&intCLIENT_ID=" + Request.QueryString["Customerid"];
//				string param9 = "&intBrokerId=" + Request.QueryString["Agencyid"];
//				string param10 = "&UnderWriter=" + Request.QueryString["Underwriterid"];
//				string param11 = "&LOB=" + Request.QueryString["LOBid"];
//				string param12 = "&BillType=" + Request.QueryString["BillTypeid"];
//				
//				ReportViewer1.ReportPath= strReportPath + "/PolicyList" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8 + param9 + param10 + param11 + param12;
//			}
//
//			if(Pagename == "LapsedPolicy")
//			{
//				string param1 = "&NameAddress=" + Request.QueryString["Addressid"];
//				string param2 = "&InceptionDateStart=" + Request.QueryString["InceptionStartDateid"];
//				string param3 = "&InceptionDateEnd=" + Request.QueryString["InceptionEndDateid"];
//				string param4 = "&EffectiveDateStart=" + Request.QueryString["EffectiveStartDateid"];
//				string param5 = "&EffectiveDateEnd=" + Request.QueryString["EffectiveEndDateid"];
//				string param6 = "&ExpirationDateStart=" + Request.QueryString["ExpirationStartDateid"];
//				string param7 = "&ExpirationDateEnd=" + Request.QueryString["ExpirationEndDateid"];
//				string param8 = "&intCLIENT_ID=" + Request.QueryString["Customerid"];
//				string param9 = "&intBrokerId=" + Request.QueryString["Agencyid"];
//				string param10 = "&UnderWriter=" + Request.QueryString["Underwriterid"];
//				string param11 = "&LOB=" + Request.QueryString["LOBid"];
//				string param12 = "&BillType=" + Request.QueryString["BillTypeid"];
//				ReportViewer1.ReportPath= strReportPath + "/LapsedPolicies" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8 + param9 + param10 + param11 + param12;
//			}
//
//			//Added by Raghav 
//
//			if (Pagename == "1099Report")
//			{
//				string year="";
//				year=Request.QueryString["YEAR"];
//				string param1 = "&YEAR=" + year;
//				ReportViewer1.ReportPath= strReportPath + "/Acct_Summary1099" + param1;
//			}
//
//			//ADDED BY PRAVEEN KUMAR(20-01-2009):ITRACK :5309
//			if(Pagename == "ReinsuranceReport")
//			{
//				string StartMonth="";
//				string EndMonth="";
//				string ContractNumbers="";
//				string YearFrom="";
//				string YearTo="";
//				string PolicyNumber="";
//				string LOB="";
//				string Parameters="";
//				
//				StartMonth=Request.QueryString["StartMonth"];
//				string param1 = "&STARTMONTH=" + StartMonth.Trim();
//				if(StartMonth.Trim()!="") Parameters+=param1;
//				EndMonth=Request.QueryString["EndMonth"];
//				string param2 = "&ENDMONTH=" + EndMonth.Trim();
//				if(EndMonth.Trim()!="") Parameters+=param2;
//				ContractNumbers=Request.QueryString["ContractNumbers"];
//				string param3 = "&CONTRACTNUMBERS=" + ContractNumbers.Trim();
//				if(ContractNumbers.Trim()!="") Parameters+=param3;
//				YearFrom=Request.QueryString["YearFrom"];
//				string param4 = "&YEARFROM=" + YearFrom.Trim();
//				if(YearFrom.Trim()!="") Parameters+=param4;
//				YearTo=Request.QueryString["YearTo"];
//				string param5 = "&YEARTO=" + YearTo.Trim();
//				if(YearTo.Trim()!="") Parameters+=param5;
//				PolicyNumber=Request.QueryString["PolicyNumber"];
//				string param6 = "&POLICYNUMBER=" + PolicyNumber.Trim();
//				if(PolicyNumber.Trim()!="") Parameters+=param6;
//				LOB=Request.QueryString["LOB"];
//				string param7="&LOB=" + LOB.Trim();
//				if(LOB.Trim()!="") Parameters+=param7;
//				ReportViewer1.ReportPath= strReportPath + "/Reinsurance_Report" + Parameters; 
//			}
//
//			//End PRAVEEN KUMAR
//
//
//			if(Pagename == "Endorsement")
//			{
//				string param1 = "&intCustomerId=" + Request.QueryString["Customerid"];
//				string param2 = "&intPolicyId=" + Request.QueryString["Policyid"];
//				string param3 = "&intAgencyId=" + Request.QueryString["Agencyid"];
//				ReportViewer1.ReportPath= strReportPath + "/Endorsement" + param1 + param2 + param3;
//			}
//
//			if(Pagename == "HolderList")
//			{
//				string param1 = "&intCustomerId=" + Request.QueryString["Customerid"];
//				string param2 = "&intPolicyId=" + Request.QueryString["Policyid"];
//				ReportViewer1.ReportPath= strReportPath + "/HolderListing" + param1 + param2;
//			}
//			
//			if(Pagename == "ClientInstallment")
//			{
//				string param1 = "&intCustomerId=" + Request.QueryString["Customerid"];
//				string param2 = "&intPolicyId=" + Request.QueryString["Policyid"];
//				string param3 = "&intAgencyId=" + Request.QueryString["Agencyid"];
//				ReportViewer1.ReportPath= strReportPath + "/InstallmentSchedule" + param1 + param2 + param3;
//			}
//
//			if(Pagename == "TrialBalance")
//			{
//				string param1 = "&GLID=" + Request.QueryString["GLID"];
//				string param2 = "&YEARFROM=" + Request.QueryString["YEARFROM"];
//				string param3 = "&YEARTO=" + Request.QueryString["YEARTO"];
//				string param4 = "&MMONTH=" + Request.QueryString["MMONTH"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_TrialBalance" + param1 + param2 + param3 + param4;	
//				
//				//string param5 = "&MONTHNAME=";
//				//string param6 = "&QUERY=";
//				//string param7 = "&SUBQUERY=";
//				//ReportViewer1.ReportPath= strReportPath + "/Acct_TrialBalance" + param1 + param2 + param3 + param4 + param5 + param6 + param7;
//				//ReportViewer1.Parameters = Microsoft.Samples.ReportingServices.ReportViewer.multiState.False;
//			}
//
//			if(Pagename == "ProfitLoss")
//			{
//				string param1 = "&GLID=" + Request.QueryString["GLID"];
//				string param2 = "&YEARFROM=" + Request.QueryString["YEARFROM"];
//				string param3 = "&YEARTO=" + Request.QueryString["YEARTO"];
//				string param4 = "&MMONTH=" + Request.QueryString["MMONTH"];
//
//				/*string param1 = "&GLID=" + Request.Form["hidGeneralLedgerId"];
//				string param2 = "&YEARFROM=" + Request.Form["hidFinancialYear"].Split('-')[0];
//				string param3 = "&YEARTO=" + Request.Form["hidFinancialYear"].Split('-')[1];
//				string param4 = "&MMONTH=" + Request.Form["hidMONTH"];*/
//				
//				ReportViewer1.ReportPath= strReportPath + "/Acct_StatementofIncome" + param1 + param2 + param3 + param4;
//			}
//
//			if(Pagename == "BalanceSheet")
//			{
//				string param1 = "&GLID=" + Request.QueryString["GLID"];
//				string param2 = "&YEARFROM=" + Request.QueryString["YEARFROM"];
//				string param3 = "&YEARTO=" + Request.QueryString["YEARTO"];
//				string param4 = "&MMONTH=" + Request.QueryString["MMONTH"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_StatementofAssets" + param1 + param2 + param3 + param4;
//			}
//
//			if(Pagename == "AgentCommissionStatmentRegular")
//			{
//				string param1 = "&AGENCY_ID=" + Request.QueryString["AGENCY_ID"];
//				string param2 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param3 = "&YEAR=" + Request.QueryString["YEAR"];
//				string param4 = "&COMM_TYPE=" + Request.QueryString["COMM_TYPE"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_AgentAcctCommissionSummary" + param1 + param2 + param3 + param4;
//			}
//
//			if(Pagename == "AgentCommissionStatmentRegularGroup")
//			{
//				string param1 = "&AGENCY_ID=" + Request.QueryString["AGENCY_ID"];
//				string param2 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param3 = "&YEAR=" + Request.QueryString["YEAR"];
//				string param4 = "&COMM_TYPE=" + Request.QueryString["COMM_TYPE"];
//				//User_Type_Code Added By Raghav For Itrack Issue 4676
//				string param5 = "&USER_TYPE_CODE=" + Request.QueryString["USER_TYPE_CODE"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_AgentAcctCommissionProducer" + param1 + param2 + param3 + param4 + param5 ;
//			}
//
//			if(Pagename == "AgentStatementRemittance")
//			{
//				string param1 = "&AGENCY_ID=" + Request.QueryString["AGENCY_ID"];
//				string param2 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param3 = "&YEAR=" + Request.QueryString["YEAR"];
//				string param4 = "&COMM_TYPE=" + Request.QueryString["COMM_TYPE"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_AgentAcctCommission" + param1 + param2 + param3 + param4;
//			}
//
//			if(Pagename == "SummaryAgenyStatementRegular")
//			{
//				string param1 = "&AGENCY_ID=" + Request.QueryString["AGENCY_ID"];
//				string param2 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param3 = "&YEAR=" + Request.QueryString["YEAR"];
//				string param4 = "&COMM_TYPE=" + Request.QueryString["COMM_TYPE"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_SummaryAgencyStatements" + param1 + param2 + param3 + param4;
//			}
//
//			if(Pagename == "SummaryAgenyStatementAdditional")
//			{
//				string param1 = "&AGENCY_ID=" + Request.QueryString["AGENCY_ID"];
//				string param2 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param3 = "&YEAR=" + Request.QueryString["YEAR"];
//				string param4 = "&COMM_TYPE=" + Request.QueryString["COMM_TYPE"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_SummaryAgencyStatementsAdd" + param1 + param2 + param3 + param4;
//			}
//			
//			if(Pagename == "AgentAccountCommissionComplete")
//			{
//				string param1 = "&AGENCY_ID=" + Request.QueryString["AGENCY_ID"];
//				string param2 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param3 = "&YEAR=" + Request.QueryString["YEAR"];
//				string param4 = "&COMM_TYPE=" + Request.QueryString["COMM_TYPE"];
//				ReportViewer1.ReportPath= strReportPath + "/AcctAgentCAC" + param1 + param2 + param3 + param4;
//			}
//						
//
//			/*if(Pagename == "Test")
//			{
//				//string param1 = "&CID=" + Request.QueryString["CID"];
//				//string param1 = "&customer_id=918";
//
//				string ParameterString ="";
//				string param1 = "&customer_id=NULL";
//				//string param1 = "&customer_id=" + Request.QueryString["CID"];
//
//				/*string param1 = "&customer_id=" + Request.QueryString["CID"];
//				if (Request.QueryString["CID"] == "NULL")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Report1";
//				}
//
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Report1" + param1;
//				}
//				
//			}*/
//
//			if(Pagename == "AccountSummary")
//			{
//
//				string ParameterString ="";				
//
//				//string param1 = "&FROMSOURCE=NULL";
//				//string param2 = "&TOSOURCE=NULL";
//				//var url="CustomReport.aspx?PageName=AccountSummary&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&CustomerID=" + CustomerID + "&PolicyID=" + PolicyID + "&VersionID=" + VersionID + "&FROMSOURCE=" + SourceFrom + "&TOSOURCE=" + SourceTo  + "&UPDATED_FROM=" + UpdatedFrom + "&CalledFrom=GLAT";
//				
//				if(Request.QueryString["FROMSOURCE"] != "NULL")
//				{
//					string param1 = "&FROMSOURCE=" + Request.QueryString["FROMSOURCE"];
//					if (ParameterString == "")
//					{
//						ParameterString = param1;
//					}
//					else
//					{
//						ParameterString = ParameterString + param1;	
//					}
//				}	
//	
//				if(Request.QueryString["TOSOURCE"] != "NULL")
//				{
//					string param2 = "&TOSOURCE=" + Request.QueryString["TOSOURCE"];
//					if (ParameterString == "")
//					{
//						ParameterString = param2;
//					}
//					else
//					{
//						ParameterString = ParameterString + param2;	
//					}
//				}	
//		
//				
//				if(Request.QueryString["FROMDATE"] != "NULL")
//				{
//					string param3 = "&FROMDATE=" + Request.QueryString["FROMDATE"];
//					if (ParameterString == "")
//					{
//						ParameterString = param3;
//					}
//					else
//					{
//						ParameterString = ParameterString + param3;	
//					}
//				}		
//		
//				if(Request.QueryString["TODATE"] != "NULL")
//				{
//					string param4 = "&TODATE=" + Request.QueryString["TODATE"];
//					if (ParameterString == "")
//					{
//						ParameterString = param4;
//					}
//					else
//					{
//						ParameterString = ParameterString + param4;	
//					}
//				}
//		
//				if(Request.QueryString["ACCOUNT_ID"] != "NULL")
//				{
//					string param5 = "&ACCOUNT_ID=" + Request.QueryString["ACCOUNT_ID"];
//					if (ParameterString == "")
//					{
//						ParameterString = param5;
//					}
//					else
//					{
//						ParameterString = ParameterString + param5;	
//					}
//				}				
//			
//				if(Request.QueryString["UPDATED_FROM"] != "NULL")
//				{
//					string param6 = "&UPDATED_FROM=" + Request.QueryString["UPDATED_FROM"];
//					if (ParameterString == "")
//					{
//						ParameterString = param6;
//					}
//					else
//					{
//						ParameterString = ParameterString + param6;	
//					}
//				}	
//
//				if(Request.QueryString["STATE"] != "NULL")
//				{
//					string param7 = "&STATE=" + Request.QueryString["STATE"];
//					if (ParameterString == "")
//					{
//						ParameterString = param7;
//					}
//					else
//					{
//						ParameterString = ParameterString + param7;	
//					}
//				}	
//
//				if(Request.QueryString["LOB"] != "NULL")
//				{
//					string param8 = "&LOB=" + Request.QueryString["LOB"];
//					if (ParameterString == "")
//					{
//						ParameterString = param8;
//					}
//					else
//					{
//						ParameterString = ParameterString + param8;	
//					}
//				}	
//
//				if(Request.QueryString["MONTH"] != "NULL" && Request.QueryString["YEAR"] != "NULL")
//				{
//					string param9 = "&YEARMONTH=" + Request.QueryString["MONTH"] + "," + Request.QueryString["YEAR"];
//					if (ParameterString == "")
//					{
//						ParameterString = param9;
//					}
//					else
//					{
//						ParameterString = ParameterString + param9;	
//					}
//				}
//	
//				if(Request.QueryString["VENDORID"] != "NULL")
//				{
//					string param10 = "&VENDORID=" + Request.QueryString["VENDORID"];
//					if (ParameterString == "")
//					{
//						ParameterString = param10;
//					}
//					else
//					{
//						ParameterString = ParameterString + param10;	
//					}
//				}	
//
//				if(Request.QueryString["CustomerID"] != "NULL")
//				{
//					string param11 = "&CUSTOMERID=" + Request.QueryString["CustomerID"];
//					if (ParameterString == "")
//					{
//						ParameterString = param11;
//					}
//					else
//					{
//						ParameterString = ParameterString + param11;	
//					}
//				}
//	
//				if(Request.QueryString["FROMAMT"] != "NULL")
//				{
//					string param12 = "&FROMAMT=" + Request.QueryString["FROMAMT"];
//					if (ParameterString == "")
//					{
//						ParameterString = param12;
//					}
//					else
//					{
//						ParameterString = ParameterString + param12;	
//					}
//				}	
//
//				if(Request.QueryString["TOAMT"] != "NULL")
//				{
//					string param13 = "&TOAMT=" + Request.QueryString["TOAMT"];
//					if (ParameterString == "")
//					{
//						ParameterString = param13;
//					}
//					else
//					{
//						ParameterString = ParameterString + param13;	
//					}
//				}	
//
//				if(Request.QueryString["TRANSTYPE"] != "NULL")
//				{
//					string param14 = "&TRANSTYPE=" + Request.QueryString["TRANSTYPE"];
//					if (ParameterString == "")
//					{
//						ParameterString = param14;
//					}
//					else
//					{
//						ParameterString = ParameterString + param14;	
//					}
//				}	
//
//				if(Request.QueryString["PolicyID"] != "NULL")
//				{
//					string param15 = "&POLICYID=" + Request.QueryString["PolicyID"];
//					if (ParameterString == "")
//					{
//						ParameterString = param15;
//					}
//					else
//					{
//						ParameterString = ParameterString + param15;	
//					}
//				}
//	
//
//				if(Request.QueryString["FROMACT"] != "NULL")
//				{
//					string param16 = "&FROMACT=" + Request.QueryString["FROMACT"];
//					if (ParameterString == "")
//					{
//						ParameterString = param16;
//					}
//					else
//					{
//						ParameterString = ParameterString + param16;	
//					}
//				}	
//
//				if(Request.QueryString["TOACT"] != "NULL")
//				{
//					string param17 = "&TOACT=" + Request.QueryString["TOACT"];
//					if (ParameterString == "")
//					{
//						ParameterString = param17;
//					}
//					else
//					{
//						ParameterString = ParameterString + param17;	
//					}
//				}	
//
//				if(Request.QueryString["ORDERBY"] != "NULL")
//				{
//					string param18 = "&ORDERBY=" + Request.QueryString["ORDERBY"];
//					if (ParameterString == "")
//					{
//						ParameterString = param18;
//					}
//					else
//					{
//						ParameterString = ParameterString + param18;	
//					}
//				}	
//
//				if(Request.QueryString["POLICY_NUMBER"] != "NULL")
//				{
//					string param19 = "&POLICY_NUMBER=" + Request.QueryString["POLICY_NUMBER"];
//					if (ParameterString == "")
//					{
//						ParameterString = param19;
//					}
//					else
//					{
//						ParameterString = ParameterString + param19;	
//					}
//				}	
//								
//			
//				if(ParameterString != "")
//				{
//					if(Request.QueryString["CalledFrom"] == "GLAT")
//					{
//						ReportViewer1.ReportPath= strReportPath + "/Acct_GeneralLedgerTrans" + ParameterString;
//					}
//					else if(Request.QueryString["CalledFrom"] == "GLAN")
//					{
//						ReportViewer1.ReportPath= strReportPath + "/GLSummary" + ParameterString;
//						//ReportViewer1.ReportPath= strReportPath + "/GeneralLedgerComb" + ParameterString;
//					}
//					else
//					{
//						ReportViewer1.ReportPath= strReportPath + "/Acct_GeneralLedgerSummary" + ParameterString;
//					}
//
//				}
//
//				else
//				{
//					if(Request.QueryString["CalledFrom"] == "GLAT")
//					{
//						ReportViewer1.ReportPath= strReportPath + "/Acct_GeneralLedgerTrans";
//					}
//					else if (Request.QueryString["CalledFrom"] == "GLAN")
//					{
//						ReportViewer1.ReportPath= strReportPath + "/GLSummary";
//						//ReportViewer1.ReportPath= strReportPath + "/GeneralLedgerComb";
//					}
//					else
//					{
//						ReportViewer1.ReportPath= strReportPath + "/Acct_GeneralLedgerSummary";
//					}
//				}
//
//				/*if(ParameterString != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_GeneralLedgerTrans" + ParameterString;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_GeneralLedgerTrans";
//				}*/
//			}
//
//			
//			if(Pagename == "LossRatio")
//			{
//				string param1 = "&DATE_FROM=" + Request.QueryString["ExpirationStartDateid"];
//				string param2 = "&DATE_TO=" + Request.QueryString["ExpirationEndDateid"];
//				string param3 = "&M_CUSTOMERID=" + Request.QueryString["Customerid"];
//				string param4 = "&M_AGENCYID=" + Request.QueryString["Agencyid"];
//				string param5 = "&M_LOB=" + Request.QueryString["LOBid"];
//				string param6 = "&M_UNDERWRITER=" + Request.QueryString["Underwriterid"];				
//				string param7 = "&FIRST_SORT=" + Request.QueryString["FirstSortid"];
//				string param8 = "&SECOND_SORT=" + Request.QueryString["SecondSortid"];
//				string param9 = "&THIRD_SORT=" + Request.QueryString["ThirdSortid"];
//
//				ReportViewer1.ReportPath= strReportPath + "/Clm_LossRatio" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8 + param9;
//			}
//
//			if(Pagename == "MonthlyCheck")
//			{
//				string MonthlyCheckParameters ="";
//
//				if ((Request.QueryString["AccountId"] != "NULL") || (Request.QueryString["AccountId"] != ""))
//				{
//					string param1 = "&ACCOUNT_ID=" + Request.QueryString["AccountId"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param1;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param1;	
//					}
//				}
//				
//				if ((Request.QueryString["CheckType"] != "NULL") || (Request.QueryString["CheckType"] != ""))
//				{
//					string param2 = "&CHECKTYPE=" + Request.QueryString["CheckType"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param2;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param2;	
//					}
//				}
//
//				if ((Request.QueryString["FROMDATE"] != "NULL") || (Request.QueryString["FROMDATE"] != ""))
//				{
//					string param3 = "&FROMDATE=" + Request.QueryString["FROMDATE"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param3;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param3;	
//					}
//				}
//
//				if ((Request.QueryString["TODATE"] != "NULL") || (Request.QueryString["TODATE"] != ""))
//				{
//					string param4 = "&TODATE=" + Request.QueryString["TODATE"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param4;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param4;	
//					}
//				}
//
//				if ((Request.QueryString["FROMAMT"] != "NULL") || (Request.QueryString["FROMAMT"] != ""))
//				{
//					string param5 = "&FROMAMT=" + Request.QueryString["FROMAMT"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param5;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param5;	
//					}
//				}
//
//				if ((Request.QueryString["TOAMT"] != "NULL") || (Request.QueryString["TOAMT"] != ""))
//				{
//					string param6 = "&TOAMT=" + Request.QueryString["TOAMT"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param6;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param6;	
//					}
//				}
//
//				if ((Request.QueryString["CheckNo"] != "NULL") || (Request.QueryString["CheckNo"] != ""))
//				{
//					string param7 = "&CHECK_NO=" + Request.QueryString["CheckNo"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param7;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param7;	
//					}
//				}
//
//				if ((Request.QueryString["Payee"] != "NULL") || (Request.QueryString["Payee"] != ""))
//				{
//					string param8 = "&PAYEE_ID=" + Request.QueryString["Payee"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param8;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param8;	
//					}
//				}
//
//				if ((Request.QueryString["PolicyID"] != "NULL") || (Request.QueryString["PolicyID"] != ""))
//				{
//					string param9 = "&POLICY_ID=" + Request.QueryString["PolicyID"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param9;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param9;	
//					}
//				}
//
//				if ((Request.QueryString["ClaimNo"] != "NULL") || (Request.QueryString["ClaimNo"] != ""))
//				{
//					string param10 = "&CLAIM_NO=" + Request.QueryString["ClaimNo"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param10;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param10;	
//					}
//				}
//
//				if ((Request.QueryString["FirstSortid"] != "NULL") || (Request.QueryString["FirstSortid"] != ""))
//				{
//					string param11 = "&FIRST_SORT=" + Request.QueryString["FirstSortid"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param11;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param11;	
//					}
//				}
//
//				if ((Request.QueryString["VoidChecks"] != "NULL") || (Request.QueryString["VoidChecks"] != ""))
//				{
//					string param12 = "&DISP_VOID_CHECKS=" + Request.QueryString["VoidChecks"];
//														
//					if (MonthlyCheckParameters == "")
//					{
//						MonthlyCheckParameters = param12;
//					}
//					else
//					{
//						MonthlyCheckParameters = MonthlyCheckParameters + param12;	
//					}
//				}
//
//				if(MonthlyCheckParameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_MonthlyCheckReport" + MonthlyCheckParameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_MonthlyCheckReport";
//				}
//
//				/*string param1 = "&ACCOUNT_ID=" + Request.QueryString["AccountId"];
//				string param2 = "&CHECKTYPE=" + Request.QueryString["CheckType"];
//				string param3 = "&FROMDATE=" + Request.QueryString["FROMDATE"];
//				string param4 = "&TODATE=" + Request.QueryString["TODATE"];
//				string param5 = "&FROMAMT=" + Request.QueryString["FROMAMT"];
//				string param6 = "&TOAMT=" + Request.QueryString["TOAMT"];
//				string param7 = "&CHECK_NO=" + Request.QueryString["CheckNo"];
//				string param8 = "&PAYEE_ID=" + Request.QueryString["Payee"];
//				string param9 = "&POLICY_ID=" + Request.QueryString["PolicyID"];
//				string param10 = "&CLAIM_NO=" + Request.QueryString["ClaimNo"];
//				string param11 = "&FIRST_SORT=" + Request.QueryString["FirstSortid"];
//				string param12 = "&DISP_VOID_CHECKS=" + Request.QueryString["VoidChecks"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_MonthlyCheckReport" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8 + param9 + param10 + param11 + param12;*/
//
//			}
//
//			if(Pagename == "ClaimsMonthlyCheck")
//			{
//				string param1 = "&ACCOUNT_ID=" + Request.QueryString["AccountId"];
//				string param2 = "&CHECKTYPE=" + Request.QueryString["CheckType"];
//				string param3 = "&FROMDATE=" + Request.QueryString["FROMDATE"];
//				string param4 = "&TODATE=" + Request.QueryString["TODATE"];
//				string param5 = "&FROMAMT=" + Request.QueryString["FROMAMT"];
//				string param6 = "&TOAMT=" + Request.QueryString["TOAMT"];
//				string param7 = "&CHECK_NO=" + Request.QueryString["CheckNo"];
//				string param8 = "&PAYEE_ID=" + Request.QueryString["Payee"];
//				string param9 = "&POLICY_ID=" + Request.QueryString["PolicyID"];
//				string param10 = "&CLAIM_NO=" + Request.QueryString["ClaimNo"];
//				string param11 = "&FIRST_SORT=" + Request.QueryString["FirstSortid"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_ClaimsCheckReport" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8 + param9 + param10 + param11;
//			}	
//
//			if(Pagename == "ClaimCountbyAdjuster")
//			{
//				//string param1 = "&DATE_FROM=" + Request.QueryString["StartDate"];
//				//string param2 = "&DATE_THROUGH=" + Request.QueryString["EndDate"];
//				string param1 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param2 = "&YEAR=" + Request.QueryString["YEAR"];
//				string param3 = "&SELECTED_PARTY_TYPES=" + Request.QueryString["PartyType"];
//				string param4 = "&SELECTED_ADJUSTER=" + Request.QueryString["Adjuster"];
//				string param5 = "&SELECTED_CLAIM_STATUS=" + Request.QueryString["ClaimStatus"];
//				string param6 = "&SELECTED_LOB=" + Request.QueryString["LOB"];
//				string param7 = "&FIRST_SORT=" + Request.QueryString["FirstSort"];
//				string param8 = "&SECOND_SORT=" + Request.QueryString["SecondSort"];
//				ReportViewer1.ReportPath= strReportPath + "/RptClaimAdjuster" + param1 + param2 + param3 + param4 + param5 + param6 + param7 + param8;
//			}	
//
//			if(Pagename == "EarnedPremium")
//			{
//				string param1 = "&MONTH=" + Request.QueryString["MONTH"];
//				string param2 = "&YEAR=" + Request.QueryString["YEAR"];
//
//				if(Request.QueryString["CalledFrom"] == "G")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_EarnedPremium_ASODL" + param1 + param2;
//				}				
//				else 
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_EarnedPremium" + param1 + param2;
//				}
//			}	
//
//			if(Pagename == "AdvanceDeposit")
//			{
//				string AdvanceDepositParameters ="";
//
//				if (Request.QueryString["Agencyid"] != "")
//				{
//					string param1 = "&AGENCY_ID=" + Request.QueryString["Agencyid"];
//														
//					if (AdvanceDepositParameters == "")
//					{
//						AdvanceDepositParameters = param1;
//					}
//					else
//					{
//						AdvanceDepositParameters = AdvanceDepositParameters + param1;	
//					}
//				}
//				
//				if (Request.QueryString["Customerid"] != "")
//				{
//					string param2 = "&CUSTOMER_ID=" + Request.QueryString["Customerid"];
//														
//					if (AdvanceDepositParameters == "")
//					{
//						AdvanceDepositParameters = param2;
//					}
//					else
//					{
//						AdvanceDepositParameters = AdvanceDepositParameters + param2;	
//					}
//				}
//				//Added By Raghav
//				if (Request.QueryString["Month"] != "")
//				{
//					string param3 = "&MONTH=" + Request.QueryString["Month"];
//														
//					if (AdvanceDepositParameters == "")
//					{
//						AdvanceDepositParameters = param3;
//					}
//					else
//					{
//						AdvanceDepositParameters = AdvanceDepositParameters + param3;	
//					}
//				}
//
//				if (Request.QueryString["Year"] != "")
//				{
//					string param4 = "&YEAR=" + Request.QueryString["year"];
//														
//					if (AdvanceDepositParameters == "")
//					{
//						AdvanceDepositParameters = param4;
//					}
//					else
//					{
//						AdvanceDepositParameters = AdvanceDepositParameters + param4;	
//					}
//				}
//				
//
//				if(AdvanceDepositParameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_AdvanceDeposit" + AdvanceDepositParameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_AdvanceDeposit";
//				}
//				/*string param1 = "&CUSTOMER_ID =" + Request.QueryString["Customerid"];
//				string param2 = "&AGENCY_ID=" + Request.QueryString["Agencyid"];
//				ReportViewer1.ReportPath= strReportPath + "/Acct_AdvanceDeposit" + param1 + param2;*/
//			}	
//
//			if(Pagename == "ClaimCheckListing")
//			{
//
//				string ClaimCheckListingParameters ="";
//				
//				if(Request.QueryString["RptType"] != "NULL")
//				{
//					string param1 = "&REPORTTYPE=" + Request.QueryString["RptType"];
//					if (ClaimCheckListingParameters == "")
//					{
//						ClaimCheckListingParameters = param1;
//					}
//					else
//					{
//						ClaimCheckListingParameters = ClaimCheckListingParameters + param1;	
//					}
//				}	
//	
//				if(Request.QueryString["StartDate"] != "")
//				{
//					string param2 = "&StartDate=" + Request.QueryString["StartDate"];
//					if (ClaimCheckListingParameters == "")
//					{
//						ClaimCheckListingParameters = param2;
//					}
//					else
//					{
//						ClaimCheckListingParameters = ClaimCheckListingParameters + param2;	
//					}
//				}
//
//				if(Request.QueryString["EndDate"] != "")
//				{
//					string param3 = "&EndDate=" + Request.QueryString["EndDate"];
//					if (ClaimCheckListingParameters == "")
//					{
//						ClaimCheckListingParameters = param3;
//					}
//					else
//					{
//						ClaimCheckListingParameters = ClaimCheckListingParameters + param3;	
//					}
//				}
//				
//				if(Request.QueryString["Check_From_Amount"] != "")
//				{
//					string param4 = "&Check_From_Amount=" + Request.QueryString["Check_From_Amount"];
//					if (ClaimCheckListingParameters == "")
//					{
//						ClaimCheckListingParameters = param4;
//					}
//					else
//					{
//						ClaimCheckListingParameters = ClaimCheckListingParameters + param4;	
//					}
//				}
//
//				if(Request.QueryString["Check_To_Amount"] != "")
//				{
//					string param5 = "&Check_To_Amount=" + Request.QueryString["Check_To_Amount"];
//					if (ClaimCheckListingParameters == "")
//					{
//						ClaimCheckListingParameters = param5;
//					}
//					else
//					{
//						ClaimCheckListingParameters = ClaimCheckListingParameters + param5;	
//					}
//				}
//				
//				if(Request.QueryString["ORDERBY"] != "")
//				{
//					string param6 = "&ORDERBY=" + Request.QueryString["ORDERBY"];
//					if (ClaimCheckListingParameters == "")
//					{
//						ClaimCheckListingParameters = param6;
//					}
//					else
//					{
//						ClaimCheckListingParameters = ClaimCheckListingParameters + param6;	
//					}
//
//				}
//				
//				if(ClaimCheckListingParameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Clm_Checks" + ClaimCheckListingParameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Clm_Checks";
//				}
//
//			}	
//
//			if(Pagename == "VendorInvoice")
//
//			{
//				string Parameters ="";
//				
//				if(Request.QueryString["FROMDATE"] != "NULL")
//				{
//					string param1 = "&FromDate=" + Request.QueryString["FROMDATE"];
//					if (Parameters == "")
//					{
//						Parameters = param1;
//					}
//					else
//					{
//						Parameters = Parameters + param1;	
//					}
//				}		
//				
//				if(Request.QueryString["TODATE"] != "NULL")
//				{
//					string param2 = "&ToDate=" + Request.QueryString["TODATE"];
//					if (Parameters == "")
//					{
//						Parameters = param2;
//					}
//					else
//					{
//						Parameters = Parameters + param2;	
//					}
//				}
//
//				if(Request.QueryString["VENDORID"] != "NULL")
//				{
//					string param3 = "&VendorId=" + Request.QueryString["VENDORID"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param3;
//					}
//					else
//					{
//						Parameters = Parameters + param3;	
//					}
//				}
//	
//				if(Parameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/VendorInvoiceDist" + Parameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/VendorInvoiceDist";
//				}
//
//			}
//
//			if(Pagename == "EODProcesslog")
//
//			{
//				string Parameters ="";
//				
//				if(Request.QueryString["FROMDATE"] != "NULL")
//				{
//					string param1 = "&FROM_DATE=" + Request.QueryString["FROMDATE"];
//					if (Parameters == "")
//					{
//						Parameters = param1;
//					}
//					else
//					{
//						Parameters = Parameters + param1;	
//					}
//				}		
//				
//				if(Request.QueryString["TODATE"] != "NULL")
//				{
//					string param2 = "&TO_DATE=" + Request.QueryString["TODATE"];
//					if (Parameters == "")
//					{
//						Parameters = param2;
//					}
//					else
//					{
//						Parameters = Parameters + param2;	
//					}
//				}
//
//				if(Request.QueryString["StatusID"] != "NULL")
//				{
//					string param3 = "&StatusId=" + Request.QueryString["StatusID"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param3;
//					}
//					else
//					{
//						Parameters = Parameters + param3;	
//					}
//				}
//				
//				if(Request.QueryString["Option"] != "NULL")
//				{
//					string param4 = "&Option=" + Request.QueryString["Option"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param4;
//					}
//					else
//					{
//						Parameters = Parameters + param4;	
//					}
//				}
//
//				if(Request.QueryString["Activity"] != "NULL")
//				{
//					string param5 = "&ACTIVITY=" + Request.QueryString["Activity"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param5;
//					}
//					else
//					{
//						Parameters = Parameters + param5;	
//					}
//				}
//
//	
//				if(Parameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/EODProcessLog" + Parameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/EODProcessLog";
//				}
//
//			}
//
//			if(Pagename == "BudgetCategoryBC")
//
//			{
//				string Parameters ="";
//				
//				if(Request.QueryString["BCID"] != "NULL")
//				{
//					string param1 = "&BCID=" + Request.QueryString["BCID"];
//					if (Parameters == "")
//					{
//						Parameters = param1;
//					}
//					else
//					{
//						Parameters = Parameters + param1;	
//					}
//				}
//		
//				if(Request.QueryString["FISCALID"] != "NULL")
//				{
//					string param2 = "&FISCALID=" + Request.QueryString["FISCALID"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param2;
//					}
//					else
//					{
//						Parameters = Parameters + param2;	
//					}
//				}
//				
//				if(Request.QueryString["YEARFROM"] != "NULL")
//				{
//					string param3 = "&YEARFROM=" + Request.QueryString["YEARFROM"];
//					if (Parameters == "")
//					{
//						Parameters = param3;
//					}
//					else
//					{
//						Parameters = Parameters + param3;	
//					}
//				}
//
//				if(Request.QueryString["YEARTO"] != "NULL")
//				{
//					string param4 = "&YEARTO=" + Request.QueryString["YEARTO"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param4;
//					}
//					else	
//					{
//						Parameters = Parameters + param4;	
//					}
//				}
//
//				if(Request.QueryString["MONTH"] != "NULL")
//				{
//					string param5 = "&MONTH=" + Request.QueryString["MONTH"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param5;
//					}
//					else	
//					{
//						Parameters = Parameters + param5;	
//					}
//				}
//	
//				if(Parameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/BudgetCategory_ByBudgetCategory" + Parameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/BudgetCategory_ByBudgetCategory";
//				}
//
//			}
//	
//			if(Pagename == "BudgetCategoryDept")
//
//			{
//				string Parameters ="";
//				
//				if(Request.QueryString["BCID"] != "NULL")
//				{
//					string param1 = "&BCID=" + Request.QueryString["BCID"];
//					if (Parameters == "")
//					{
//						Parameters = param1;
//					}
//					else
//					{
//						Parameters = Parameters + param1;	
//					}
//				}
//		
//				if(Request.QueryString["FISCALID"] != "NULL")
//				{
//					string param2 = "&FISCALID=" + Request.QueryString["FISCALID"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param2;
//					}
//					else
//					{
//						Parameters = Parameters + param2;	
//					}
//				}
//				
//				if(Request.QueryString["YEARFROM"] != "NULL")
//				{
//					string param3 = "&YEARFROM=" + Request.QueryString["YEARFROM"];
//					if (Parameters == "")
//					{
//						Parameters = param3;
//					}
//					else
//					{
//						Parameters = Parameters + param3;	
//					}
//				}
//
//				if(Request.QueryString["YEARTO"] != "NULL")
//				{
//					string param4 = "&YEARTO=" + Request.QueryString["YEARTO"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param4;
//					}
//					else	
//					{
//						Parameters = Parameters + param4;	
//					}
//				}
//
//				if(Request.QueryString["MONTH"] != "NULL")
//				{
//					string param5 = "&MONTH=" + Request.QueryString["MONTH"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param5;
//					}
//					else	
//					{
//						Parameters = Parameters + param5;	
//					}
//				}
//	
//				if(Parameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/BudgetCategory_ByDept" + Parameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/BudgetCategory_ByDept";
//				}
//
//			}
//
//			if(Pagename == "ClaimStatus")
//
//			{
//				string Parameters ="";
//				
//				if(Request.QueryString["Agencyid"] != "NULL")
//				{
//					string param1 = "&AGENCYID=" + Request.QueryString["Agencyid"];
//					if (Parameters == "")
//					{
//						Parameters = param1;
//					}
//					else
//					{
//						Parameters = Parameters + param1;	
//					}
//				}
//		
//				if(Request.QueryString["Insuredid"] != "NULL")
//				{
//					string param2 = "&INSUREDID=" + Request.QueryString["Insuredid"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param2;
//					}
//					else
//					{
//						Parameters = Parameters + param2;	
//					}
//				}
//				
//				if(Request.QueryString["Claimid"] != "NULL")
//				{
//					string param3 = "&CLAIMID=" + Request.QueryString["Claimid"];
//					if (Parameters == "")
//					{
//						Parameters = param3;
//					}
//					else
//					{
//						Parameters = Parameters + param3;	
//					}
//				}
//
//				if(Request.QueryString["CustomerID"] != "NULL")
//				{
//					string param4 = "&CUSTOMERID=" + Request.QueryString["CustomerID"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param4;
//					}
//					else	
//					{
//						Parameters = Parameters + param4;	
//					}
//				}
//
//				if(Request.QueryString["PolicyID"] != "NULL")
//				{
//					string param5 = "&POLICYID=" + Request.QueryString["PolicyID"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param5;
//					}
//					else	
//					{
//						Parameters = Parameters + param5;	
//					}
//				}
//
//				if(Request.QueryString["VersionID"] != "NULL")
//				{
//					string param6 = "&VERSIONID=" + Request.QueryString["VersionID"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param6;
//					}
//					else	
//					{
//						Parameters = Parameters + param6;	
//					}
//				}
//
//				if(Request.QueryString["StartDateid"] != "NULL")
//				{
//					string param7 = "&STARTDATEID=" + Request.QueryString["StartDateid"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param7;
//					}
//					else	
//					{
//						Parameters = Parameters + param7;	
//					}
//				}
//
//				if(Request.QueryString["EndDateid"] != "NULL")
//				{
//					string param8 = "&ENDDATEID=" + Request.QueryString["EndDateid"];
//														
//					if (Parameters == "")
//					{
//						Parameters = param8;
//					}
//					else	
//					{
//						Parameters = Parameters + param8;	
//					}
//				}
//	
//				if(Parameters != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/ClaimStatus" + Parameters;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/ClaimStatus";
//				}
//
//			}
//
//	
//			if(Pagename == "SuspenseDeposits")
//			{
//				
//				string ParametersSD ="";
//				
//				if(Request.QueryString["FROMDATE"] != "NULL")
//				{
//					string param1 = "&FromDate=" + Request.QueryString["FROMDATE"];
//					if (ParametersSD == "")
//					{
//						ParametersSD = param1;
//					}
//					else
//					{
//						ParametersSD = ParametersSD + param1;	
//					}
//				}		
//				
//				if(Request.QueryString["TODATE"] != "NULL")
//				{
//					string param2 = "&ToDate=" + Request.QueryString["TODATE"];
//					if (ParametersSD == "")
//					{
//						ParametersSD = param2;
//					}
//					else
//					{
//						ParametersSD = ParametersSD + param2;	
//					}
//				}
//				
//				if(ParametersSD != "")
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_SuspenseDeposits" + ParametersSD;
//				}
//				else
//				{
//					ReportViewer1.ReportPath= strReportPath + "/Acct_SuspenseDeposits";
//				}
//
//			}
//
//				ReportViewer1.ServerUrl = strReportServer;	
//				
//				//ReportViewer1.Toolbar = Microsoft.Samples.ReportingServices.ReportViewer.multiState.False;
//				ReportViewer1.Parameters  = Microsoft.Samples.ReportingServices.ReportViewer.multiState.False;

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
