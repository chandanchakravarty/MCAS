using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Collections.Generic;




[Serializable]
class ReportServerCredentials : IReportServerCredentials
{
    private string my_userName;
    private string my_password;
    private string my_domain;



    public ReportServerCredentials(string userName, string password, string domain)
    {
        my_userName = userName;
        my_password = password;
        my_domain = domain;
    }

    public System.Security.Principal.WindowsIdentity ImpersonationUser
    {
        get { return null; }
    }

    public ICredentials NetworkCredentials
    {
        get { return new NetworkCredential(my_userName, my_password, my_domain); }
    }

    public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
    {
        authCookie = null;
        userName = my_userName;
        password = my_password;
        authority = my_domain;
        return false;
    }
}
namespace Cms.Reports.Aspx
{
    public partial class ReportViewerNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string ServerURL = System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"].Trim();
            string UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"].Trim();
            string Password = System.Configuration.ConfigurationManager.AppSettings["Password"].Trim();
            string Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"].Trim();
            string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].Trim();
            /*Microsoft.Reporting.WebForms.ReportViewer rptVW;
            rptVW = new Microsoft.Reporting.WebForms.ReportViewer();*/
            rptVW.ProcessingMode = ProcessingMode.Remote;
            rptVW.Height = Unit.Percentage(100);
            rptVW.Width = Unit.Percentage(100);
            rptVW.ShowCredentialPrompts = false;
            rptVW.ShowParameterPrompts = false;
            rptVW.CssClass = "fullheight";
            rptVW.AsyncRendering = true;
            rptVW.ServerReport.ReportServerUrl = new Uri(ServerURL);

            ReportServerCredentials objCredentials = new ReportServerCredentials(UserName, Password, Domain);
            rptVW.ServerReport.ReportServerCredentials = objCredentials;

            #region Calling reports
            ///Set Parameters for Reports

            List<ReportParameter> paramList = new List<ReportParameter>();

            string PageName = Request.QueryString["PageName"];

            if (PageName == "ProfitLoss")
            {
                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_StatementofIncome";
                paramList.Add(new ReportParameter("GLID", Request.QueryString["GLID"]));
                paramList.Add(new ReportParameter("YEARFROM", Request.QueryString["YEARFROM"]));
                paramList.Add(new ReportParameter("YEARTO", Request.QueryString["YEARTO"]));
                paramList.Add(new ReportParameter("MMONTH", Request.QueryString["MMONTH"]));
            }
            else if (PageName == "BalanceSheet")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_StatementofAssets";

                paramList.Add(new ReportParameter("GLID", Request.QueryString["GLID"]));
                paramList.Add(new ReportParameter("YEARFROM", Request.QueryString["YEARFROM"]));
                paramList.Add(new ReportParameter("YEARTO", Request.QueryString["YEARTO"]));
                paramList.Add(new ReportParameter("MMONTH", Request.QueryString["MMONTH"]));


            }


            else if (PageName == "MonthlyCheck")
            {

                if ((Request.QueryString["AccountId"] != "NULL") || (Request.QueryString["AccountId"] != ""))
                {

                    paramList.Add(new ReportParameter("ACCOUNT_ID", Request.QueryString["AccountId"]));

                }

                if ((Request.QueryString["CheckType"] != "NULL") || (Request.QueryString["CheckType"] != ""))
                {

                    paramList.Add(new ReportParameter("CHECKTYPE", Request.QueryString["CheckType"]));
                }

                if ((Request.QueryString["FROMDATE"] != "NULL") || (Request.QueryString["FROMDATE"] != ""))
                {

                    paramList.Add(new ReportParameter("FROMDATE", Request.QueryString["FROMDATE"]));
                }

                if ((Request.QueryString["TODATE"] != "NULL") || (Request.QueryString["TODATE"] != ""))
                {

                    paramList.Add(new ReportParameter("TODATE", Request.QueryString["TODATE"]));
                }

                if ((Request.QueryString["FROMAMT"] != "NULL") || (Request.QueryString["FROMAMT"] != ""))
                {

                    paramList.Add(new ReportParameter("FROMAMT", Request.QueryString["FROMAMT"]));
                }

                if ((Request.QueryString["TOAMT"] != "NULL") || (Request.QueryString["TOAMT"] != ""))
                {

                    paramList.Add(new ReportParameter("TOAMT", Request.QueryString["TOAMT"]));
                }

                if ((Request.QueryString["CheckNo"] != "NULL") || (Request.QueryString["CheckNo"] != ""))
                {

                    paramList.Add(new ReportParameter("CHECK_NO", Request.QueryString["CheckNo"]));

                }

                if ((Request.QueryString["Payee"] != "NULL") || (Request.QueryString["Payee"] != ""))
                {

                    paramList.Add(new ReportParameter("PAYEE_ID", Request.QueryString["Payee"]));
                }

                //if ((Request.QueryString["CustomerID"] != "NULL") || (Request.QueryString["CustomerID"] != ""))
                //{
                //    paramList.Add(new ReportParameter("CUSTOMER_ID", Request.QueryString["CustomerID"]));
                //}

                //if ((Request.QueryString["PolicyID"] != "NULL") || (Request.QueryString["PolicyID"] != ""))
                //{

                //    paramList.Add(new ReportParameter("POLICY_ID", Request.QueryString["PolicyID"]));
                //}

                if ((Request.QueryString["ClaimNo"] != "NULL") || (Request.QueryString["ClaimNo"] != ""))
                {

                    paramList.Add(new ReportParameter("CLAIM_NO", Request.QueryString["ClaimNo"]));
                }

                if ((Request.QueryString["FirstSortid"] != "NULL") || (Request.QueryString["FirstSortid"] != ""))
                {

                    paramList.Add(new ReportParameter("FIRST_SORT", Request.QueryString["FirstSortid"]));
                }

                if ((Request.QueryString["VoidChecks"] != "NULL") || (Request.QueryString["VoidChecks"] != ""))
                {

                    paramList.Add(new ReportParameter("DISP_VOID_CHECKS", Request.QueryString["VoidChecks"]));

                }
                if ((Request.QueryString["PolicyNumber"] != "NULL") || (Request.QueryString["PolicyNumber"] != ""))
                {

                    paramList.Add(new ReportParameter("PolicyNumber", Request.QueryString["PolicyNumber"]));

                }


                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_MonthlyCheckReport";


            }

            else if (PageName == "UserList")
            {

                paramList.Add(new ReportParameter("UserID", Request.QueryString["Userid"]));
                rptVW.ServerReport.ReportPath = ReportPath + "/UserLicense";

            }

            else if (PageName == "PolicyRenewal")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/RenewalList";
                paramList.Add(new ReportParameter("NameAddress", Request.QueryString["Addressid"]));
                paramList.Add(new ReportParameter("ExpirationDateStart", Request.QueryString["ExpirationStartDateid"]));
                paramList.Add(new ReportParameter("ExpirationDateEnd", Request.QueryString["ExpirationEndDateid"]));
                paramList.Add(new ReportParameter("CLIENT_ID", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("GLID", Request.QueryString["Agencyid"]));
                paramList.Add(new ReportParameter("UnderWriter", Request.QueryString["Underwriterid"]));
                paramList.Add(new ReportParameter("LOB", Request.QueryString["LOBid"]));
                paramList.Add(new ReportParameter("BillType", Request.QueryString["BillTypeid"]));


            }
            else if (PageName == "ClientList")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/ClientList";
                paramList.Add(new ReportParameter("NameFormat", Request.QueryString["NameFormatid"]));
                paramList.Add(new ReportParameter("NameAddress", Request.QueryString["Addressid"]));
                paramList.Add(new ReportParameter("intCLIENTACTIVE", Request.QueryString["ClientTypeid"]));
                paramList.Add(new ReportParameter("intCLIENT_ID", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("intAgencyId", Request.QueryString["Agencyid"]));
                paramList.Add(new ReportParameter("ClientStates", Request.QueryString["Stateid"]));
                paramList.Add(new ReportParameter("ClientZip", Request.QueryString["Zipid"]));


            }
            else if (PageName == "TodoList")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/TodoList";
                paramList.Add(new ReportParameter("NameFormat", Request.QueryString["NameFormatid"]));
                paramList.Add(new ReportParameter("AddressonReport", Request.QueryString["Addressid"]));
                paramList.Add(new ReportParameter("UnderWriter", Request.QueryString["Underwriterid"]));
                paramList.Add(new ReportParameter("FromDate", Request.QueryString["StartDateid"]));
                paramList.Add(new ReportParameter("EndDate", Request.QueryString["EndDateid"]));

            }

            else if (PageName == "AgentList")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/AgentList";
                paramList.Add(new ReportParameter("Hierarchy", Request.QueryString["HierarchySelectedid"]));
                paramList.Add(new ReportParameter("AgentID", Request.QueryString["Agencyid"]));




            }
            else if (PageName == "AgentCommission")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/AgentCommissions";
                paramList.Add(new ReportParameter("AgencyID", Request.QueryString["Agentid"]));


            }

            else if (PageName == "PolicyExpiration")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/PolicyExpiration";
                paramList.Add(new ReportParameter("NameAddress", Request.QueryString["Addressid"]));
                paramList.Add(new ReportParameter("ExpirationDateStart", Request.QueryString["ExpirationStartDateid"]));
                paramList.Add(new ReportParameter("ExpirationDateEnd", Request.QueryString["ExpirationEndDateid"]));
                paramList.Add(new ReportParameter("intCLIENT_ID", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("intBrokerId", Request.QueryString["Agencyid"]));
                paramList.Add(new ReportParameter("UnderWriter", Request.QueryString["Underwriterid"]));
                paramList.Add(new ReportParameter("LOB", Request.QueryString["LOBid"]));
                paramList.Add(new ReportParameter("BillType", Request.QueryString["BillTypeid"]));
                paramList.Add(new ReportParameter("PolicyStatus", Request.QueryString["PolicyStatusid"]));
                paramList.Add(new ReportParameter("ORDERBY", Request.QueryString["ORDERBY"]));


            }

            else if (PageName == "ClientPolicyList")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/PolicyList";
                paramList.Add(new ReportParameter("NameAddress", Request.QueryString["Addressid"]));
                paramList.Add(new ReportParameter("InceptionDateStart", Request.QueryString["InceptionStartDateid"]));
                paramList.Add(new ReportParameter("InceptionDateEnd", Request.QueryString["InceptionEndDateid"]));
                paramList.Add(new ReportParameter("EffectiveDateStart", Request.QueryString["EffectiveStartDateid"]));
                paramList.Add(new ReportParameter("EffectiveDateEnd", Request.QueryString["EffectiveEndDateid"]));
                paramList.Add(new ReportParameter("ExpirationDateStart", Request.QueryString["ExpirationStartDateid"]));
                paramList.Add(new ReportParameter("ExpirationDateEnd", Request.QueryString["ExpirationEndDateid"]));
                paramList.Add(new ReportParameter("intCLIENT_ID", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("intBrokerId", Request.QueryString["Agencyid"]));
                paramList.Add(new ReportParameter("UnderWriter", Request.QueryString["Underwriterid"]));
                paramList.Add(new ReportParameter("LOB", Request.QueryString["LOBid"]));
                paramList.Add(new ReportParameter("BillType", Request.QueryString["BillTypeid"]));



            }
            else if (PageName == "LapsedPolicy")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/LapsedPolicies";
                paramList.Add(new ReportParameter("NameAddress", Request.QueryString["Addressid"]));
                paramList.Add(new ReportParameter("InceptionDateStart", Request.QueryString["InceptionStartDateid"]));
                paramList.Add(new ReportParameter("InceptionDateEnd", Request.QueryString["InceptionEndDateid"]));
                paramList.Add(new ReportParameter("EffectiveDateStart", Request.QueryString["EffectiveStartDateid"]));
                paramList.Add(new ReportParameter("EffectiveDateEnd", Request.QueryString["EffectiveEndDateid"]));
                paramList.Add(new ReportParameter("ExpirationDateStart", Request.QueryString["ExpirationStartDateid"]));
                paramList.Add(new ReportParameter("ExpirationDateEnd", Request.QueryString["ExpirationEndDateid"]));
                paramList.Add(new ReportParameter("intCLIENT_ID", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("intBrokerId", Request.QueryString["Agencyid"]));
                paramList.Add(new ReportParameter("UnderWriter", Request.QueryString["Underwriterid"]));
                paramList.Add(new ReportParameter("LOB", Request.QueryString["LOBid"]));
                paramList.Add(new ReportParameter("BillType", Request.QueryString["BillTypeid"]));



            }
            else if (PageName == "1099Report")
            {
                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_Summary1099";
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
            }

            else if (PageName == "Endorsement")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Endorsement";
                paramList.Add(new ReportParameter("intCustomerId", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("intPolicyId", Request.QueryString["Policyid"]));
                paramList.Add(new ReportParameter("intAgencyId", Request.QueryString["Agencyid"]));


            }

            else if (PageName == "HolderList")
            {

                paramList.Add(new ReportParameter("intCustomerId", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("intPolicyId", Request.QueryString["Policyid"]));
                rptVW.ServerReport.ReportPath = ReportPath + "/HolderListing";


            }

            else if (PageName == "ClientInstallment")
            {

                paramList.Add(new ReportParameter("intCustomerId", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("intPolicyId", Request.QueryString["Policyid"]));
                paramList.Add(new ReportParameter("intAgencyId", Request.QueryString["Agencyid"]));
                rptVW.ServerReport.ReportPath = ReportPath + "/InstallmentSchedule";

            }

            else if (PageName == "TrialBalance")
            {
                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_TrialBalance";
                paramList.Add(new ReportParameter("GLID", Request.QueryString["GLID"]));
                paramList.Add(new ReportParameter("YEARFROM", Request.QueryString["YEARFROM"]));
                paramList.Add(new ReportParameter("YEARTO", Request.QueryString["YEARTO"]));
                paramList.Add(new ReportParameter("MMONTH", Request.QueryString["MMONTH"]));
            }

            else if (PageName == "AgentCommissionStatmentRegular")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_AgentAcctCommissionSummary";
                paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
                paramList.Add(new ReportParameter("COMM_TYPE", Request.QueryString["COMM_TYPE"]));




            }

            else if (PageName == "AgentCommissionStatmentRegularGroup")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_AgentAcctCommissionProducer";
                paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
                paramList.Add(new ReportParameter("COMM_TYPE", Request.QueryString["COMM_TYPE"]));
                paramList.Add(new ReportParameter("USER_TYPE_CODE", Request.QueryString["USER_TYPE_CODE"]));


            }

            else if (PageName == "AgentStatementRemittance")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_AgentAcctCommission";
                paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
                paramList.Add(new ReportParameter("COMM_TYPE", Request.QueryString["COMM_TYPE"]));
                paramList.Add(new ReportParameter("CALLED_FROM", Request.QueryString["CALLED_FROM"]));
            }

            else if (PageName == "SummaryAgenyStatementRegular")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_SummaryAgencyStatements";
                paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
                paramList.Add(new ReportParameter("COMM_TYPE", Request.QueryString["COMM_TYPE"]));
            }

            else if (PageName == "SummaryAgenyStatementAdditional")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_SummaryAgencyStatementsAdd";
                paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
                paramList.Add(new ReportParameter("COMM_TYPE", Request.QueryString["COMM_TYPE"]));


            }

            if (PageName == "AgentAccountCommissionComplete")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/AcctAgentCAC";
                paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
                paramList.Add(new ReportParameter("COMM_TYPE", Request.QueryString["COMM_TYPE"]));


            }
            else if (PageName == "LossRatio")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Clm_LossRatio";
                paramList.Add(new ReportParameter("DATE_FROM", Request.QueryString["ExpirationStartDateid"]));
                paramList.Add(new ReportParameter("DATE_TO", Request.QueryString["ExpirationEndDateid"]));
                paramList.Add(new ReportParameter("M_CUSTOMERID", Request.QueryString["Customerid"]));
                paramList.Add(new ReportParameter("M_AGENCYID", Request.QueryString["Agencyid"]));
                paramList.Add(new ReportParameter("M_LOB", Request.QueryString["LOBid"]));
                paramList.Add(new ReportParameter("M_UNDERWRITER", Request.QueryString["Underwriterid"]));
                paramList.Add(new ReportParameter("FIRST_SORT", Request.QueryString["FirstSortid"]));
                paramList.Add(new ReportParameter("SECOND_SORT", Request.QueryString["SecondSortid"]));
                paramList.Add(new ReportParameter("THIRD_SORT", Request.QueryString["ThirdSortid"]));


            }


            else if (PageName == "ClaimsMonthlyCheck")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_ClaimsCheckReport";
                paramList.Add(new ReportParameter("ACCOUNT_ID", Request.QueryString["AccountId"]));
                paramList.Add(new ReportParameter("CHECKTYPE", Request.QueryString["CheckType"]));
                paramList.Add(new ReportParameter("FROMDATE", Request.QueryString["FROMDATE"]));
                paramList.Add(new ReportParameter("TODATE", Request.QueryString["TODATE"]));
                paramList.Add(new ReportParameter("FROMAMT", Request.QueryString["FROMAMT"]));
                paramList.Add(new ReportParameter("TOAMT", Request.QueryString["TOAMT"]));
                paramList.Add(new ReportParameter("CHECK_NO", Request.QueryString["CheckNo"]));
                paramList.Add(new ReportParameter("PAYEE_ID", Request.QueryString["Payee"]));
                paramList.Add(new ReportParameter("CUSTOMER_ID", Request.QueryString["CustomerID"]));
                paramList.Add(new ReportParameter("POLICY_ID", Request.QueryString["PolicyID"]));
                paramList.Add(new ReportParameter("CLAIM_NO", Request.QueryString["ClaimNo"]));
                paramList.Add(new ReportParameter("FIRST_SORT", Request.QueryString["FirstSortid"]));
                paramList.Add(new ReportParameter("CALLED_FROM", Request.QueryString["CALLED_FROM"]));



            }

            else if (PageName == "ClaimCountbyAdjuster")
            {

                rptVW.ServerReport.ReportPath = ReportPath + "/RptClaimAdjuster";
                paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));
                //paramList.Add(new ReportParameter("SELECTED_PARTY_TYPES", Request.QueryString["PartyType"]));
                paramList.Add(new ReportParameter("SELECTED_ADJUSTER", Request.QueryString["Adjuster"]));
                paramList.Add(new ReportParameter("SELECTED_CLAIM_STATUS", Request.QueryString["ClaimStatus"]));
                paramList.Add(new ReportParameter("SELECTED_LOB", Request.QueryString["LOB"]));
                paramList.Add(new ReportParameter("FIRST_SORT", Request.QueryString["FirstSort"]));
                paramList.Add(new ReportParameter("SECOND_SORT", Request.QueryString["SecondSort"]));

            }

            else if (PageName == "EarnedPremium")
            {

                if (Request.QueryString["CalledFrom"] == "G")
                {

                    rptVW.ServerReport.ReportPath = ReportPath + "/Acct_EarnedPremium_ASODL";
                    paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                    paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));


                }
                else
                {

                    rptVW.ServerReport.ReportPath = ReportPath + "/Acct_EarnedPremium";
                    paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));
                    paramList.Add(new ReportParameter("YEAR", Request.QueryString["YEAR"]));


                }
            }

            else if (PageName == "AdvanceDeposit")
            {

                if (Request.QueryString["Agencyid"] != "")
                {

                    paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["Agencyid"]));
                }

                if (Request.QueryString["Customerid"] != "")
                {

                    paramList.Add(new ReportParameter("CUSTOMER_ID", Request.QueryString["Customerid"]));
                }

                if (Request.QueryString["Month"] != "")
                {

                    paramList.Add(new ReportParameter("MONTH", Request.QueryString["Month"]));
                }

                if (Request.QueryString["Year"] != "")
                {

                    paramList.Add(new ReportParameter("YEAR", Request.QueryString["year"]));
                }


                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_AdvanceDeposit";

            }

            else if (PageName == "ClaimCheckListing")
            {
                if (Request.QueryString["RptType"] != "NULL")
                {
                    paramList.Add(new ReportParameter("REPORTTYPE", Request.QueryString["RptType"]));

                }

                if (Request.QueryString["StartDate"] != "")
                {
                    //string param2 = "&StartDate=" + Request.QueryString["StartDate"];
                    paramList.Add(new ReportParameter("StartDate", Request.QueryString["StartDate"]));

                }

                if (Request.QueryString["EndDate"] != "")
                {
                    // string param3 = "&EndDate=" + Request.QueryString["EndDate"];
                    paramList.Add(new ReportParameter("EndDate", Request.QueryString["EndDate"]));
                }

                if (Request.QueryString["Check_From_Amount"] != "")
                {
                    //string param4 = "&Check_From_Amount=" + Request.QueryString["Check_From_Amount"];
                    paramList.Add(new ReportParameter("Check_From_Amount", Request.QueryString["Check_From_Amount"]));
                }

                if (Request.QueryString["Check_To_Amount"] != "")
                {
                    // string param5 = "&Check_To_Amount=" + Request.QueryString["Check_To_Amount"];
                    paramList.Add(new ReportParameter("Check_To_Amount", Request.QueryString["Check_To_Amount"]));

                }

                if (Request.QueryString["ORDERBY"] != "")
                {
                    // string param6 = "&ORDERBY=" + Request.QueryString["ORDERBY"];
                    paramList.Add(new ReportParameter("ORDERBY", Request.QueryString["ORDERBY"]));

                }

                rptVW.ServerReport.ReportPath = ReportPath + "/Clm_Checks";

            }



            else if (PageName == "VendorInvoice")
            {


                if (Request.QueryString["FROMDATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FromDate", Request.QueryString["FROMDATE"]));

                }

                if (Request.QueryString["TODATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ToDate", Request.QueryString["TODATE"]));

                }

                if (Request.QueryString["VENDORID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("VendorId", Request.QueryString["VENDORID"]));

                }


                rptVW.ServerReport.ReportPath = ReportPath + "/VendorInvoiceDist";



            }

            else if (PageName == "EODProcesslog")
            {

                if (Request.QueryString["FROMDATE"] != "NULL")
                {
                    string param1 = "&FROM_DATE=" + Request.QueryString["FROMDATE"];
                    paramList.Add(new ReportParameter("FROM_DATE", Request.QueryString["FROMDATE"]));
                }

                if (Request.QueryString["TODATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TO_DATE", Request.QueryString["TODATE"]));
                }

                if (Request.QueryString["StatusID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("StatusId", Request.QueryString["StatusID"]));
                }

                if (Request.QueryString["Option"] != "NULL")
                {

                    paramList.Add(new ReportParameter("Option", Request.QueryString["Option"]));

                }

                if (Request.QueryString["Activity"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ACTIVITY", Request.QueryString["Activity"]));

                }
                rptVW.ServerReport.ReportPath = ReportPath + "/EODProcessLog";
                rptVW.ServerReport.SetParameters(paramList);
                return;
            }

            else if (PageName == "BudgetCategoryBC")
            {

                if (Request.QueryString["BCID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("BCID", Request.QueryString["BCID"]));

                }

                if (Request.QueryString["FISCALID"] != "NULL")
                {


                    paramList.Add(new ReportParameter("FISCALID", Request.QueryString["FISCALID"]));

                }

                if (Request.QueryString["YEARFROM"] != "NULL")
                {


                    paramList.Add(new ReportParameter("YEARFROM", Request.QueryString["YEARFROM"]));

                }

                if (Request.QueryString["YEARTO"] != "NULL")
                {

                    paramList.Add(new ReportParameter("YEARTO", Request.QueryString["YEARTO"]));


                }

                if (Request.QueryString["MONTH"] != "NULL")
                {

                    paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));


                }

                rptVW.ServerReport.ReportPath = ReportPath + "/BudgetCategory_ByBudgetCategory";



            }

            else if (PageName == "BudgetCategoryDept")
            {

                if (Request.QueryString["BCID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("BCID", Request.QueryString["BCID"]));

                }

                if (Request.QueryString["FISCALID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FISCALID", Request.QueryString["FISCALID"]));

                }

                if (Request.QueryString["YEARFROM"] != "NULL")
                {

                    paramList.Add(new ReportParameter("YEARFROM", Request.QueryString["YEARFROM"]));

                }

                if (Request.QueryString["YEARTO"] != "NULL")
                {

                    paramList.Add(new ReportParameter("YEARTO", Request.QueryString["YEARTO"]));

                }

                if (Request.QueryString["MONTH"] != "NULL")
                {

                    paramList.Add(new ReportParameter("MONTH", Request.QueryString["MONTH"]));

                }



                rptVW.ServerReport.ReportPath = ReportPath + "/BudgetCategory_ByDept";

            }
            else if (PageName == "ClaimStatus")
            {

                if (Request.QueryString["Agencyid"] != "NULL")
                {

                    paramList.Add(new ReportParameter("AGENCYID", Request.QueryString["Agencyid"]));
                }

                if (Request.QueryString["Insuredid"] != "NULL")
                {

                    paramList.Add(new ReportParameter("INSUREDID", Request.QueryString["Insuredid"]));

                }

                if (Request.QueryString["Claimid"] != "NULL")
                {

                    paramList.Add(new ReportParameter("CLAIMID", Request.QueryString["Claimid"]));

                }

                if (Request.QueryString["CustomerID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("CUSTOMERID", Request.QueryString["CustomerID"]));

                }

                if (Request.QueryString["PolicyID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("POLICYID", Request.QueryString["PolicyID"]));

                }

                if (Request.QueryString["VersionID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("VERSIONID", Request.QueryString["VersionID"]));

                }

                if (Request.QueryString["StartDateid"] != "NULL")
                {

                    paramList.Add(new ReportParameter("STARTDATEID", Request.QueryString["StartDateid"]));

                }

                if (Request.QueryString["EndDateid"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ENDDATEID", Request.QueryString["EndDateid"]));

                }

                if (Request.QueryString["OrderBy"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ORDERBY", Request.QueryString["OrderBy"]));

                }

                rptVW.ServerReport.ReportPath = ReportPath + "/ClaimStatus";

            }



            else if (PageName == "SuspenseDeposits")
            {


                if (Request.QueryString["FROMDATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FromDate", Request.QueryString["FROMDATE"]));

                }

                if (Request.QueryString["TODATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ToDate", Request.QueryString["TODATE"]));

                }

                rptVW.ServerReport.ReportPath = ReportPath + "/Acct_SuspenseDeposits";

            }

            else if (PageName == "ReinsuranceReport")
            {
                if (Request.QueryString["StartMonth"] != "NULL")
                {

                    paramList.Add(new ReportParameter("STARTMONTH", Request.QueryString["StartMonth"]));

                }
                if (Request.QueryString["EndMonth"] != "NULL" && Request.QueryString["EndMonth"] != "")
                {

                    paramList.Add(new ReportParameter("ENDMONTH", Request.QueryString["EndMonth"]));

                }
                if (Request.QueryString["ContractNumbers"] != "NULL")
                {

                    paramList.Add(new ReportParameter("CONTRACTNUMBERS", Request.QueryString["ContractNumbers"]));

                }
                if (Request.QueryString["YearFrom"] != "NULL")
                {

                    paramList.Add(new ReportParameter("YEARFROM", Request.QueryString["YearFrom"]));

                }
                if (Request.QueryString["YearTo"] != "NULL")
                {

                    paramList.Add(new ReportParameter("YEARTO", Request.QueryString["YearTo"]));

                }
                if (Request.QueryString["PolicyNumber"] != "NULL" && Request.QueryString["PolicyNumber"] != "")
                {

                    paramList.Add(new ReportParameter("POLICYNUMBER", Request.QueryString["PolicyNumber"]));

                }
                if (Request.QueryString["LOB"] != "NULL")
                {

                    paramList.Add(new ReportParameter("LOB", Request.QueryString["LOB"]));

                }

                rptVW.ServerReport.ReportPath = ReportPath + "/Reinsurance_Report";

            }
            //Added For Itrack Issue #6282.
            else if (PageName == "PriorLossSummary")
            {
                if (Request.QueryString["STARTDATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("STARTDATE", Request.QueryString["STARTDATE"]));
                }
                if (Request.QueryString["ENDDATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ENDDATE", Request.QueryString["ENDDATE"]));
                }
                if (Request.QueryString["AGENCYID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("AGENCYID", Request.QueryString["AGENCYID"]));
                }
                if (Request.QueryString["LOBID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("LOBID", Request.QueryString["LOBID"]));
                }
                if (Request.QueryString["CLAIMENTID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("CLAIMENTID", Request.QueryString["CLAIMENTID"]));
                }
                if (Request.QueryString["SORTBY"] != "NULL")
                {

                    paramList.Add(new ReportParameter("SORTBY", Request.QueryString["SORTBY"]));
                }


                rptVW.ServerReport.ReportPath = ReportPath + "/PaidLoss";
            }

            //Changes By raghav Gupta For Itrack Issue #6135.

            else if (PageName == "AgencyTransaction")
            {
                if (Request.QueryString["AGENCY_ID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                }
                if (Request.QueryString["EFFECTIVE_MONTH"] != "NULL")
                {

                    paramList.Add(new ReportParameter("EFFECTIVE_MONTH", Request.QueryString["EFFECTIVE_MONTH"]));
                }
                if (Request.QueryString["EFFECTIVE_YEAR"] != "NULL")
                {

                    paramList.Add(new ReportParameter("EFFECTIVE_YEAR", Request.QueryString["EFFECTIVE_YEAR"]));
                }
                if (Request.QueryString["FROM_EFF_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROM_EFF_DATE", Request.QueryString["FROM_EFF_DATE"]));
                }

                if (Request.QueryString["TO_EFF_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TO_EFF_DATE", Request.QueryString["TO_EFF_DATE"]));
                }
                if (Request.QueryString["TRAN_MONTH"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TRAN_MONTH", Request.QueryString["TRAN_MONTH"]));
                }
                if (Request.QueryString["TRAN_YEAR"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TRAN_YEAR", Request.QueryString["TRAN_YEAR"]));
                }
                if (Request.QueryString["FROM_TRAN_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROM_TRAN_DATE", Request.QueryString["FROM_TRAN_DATE"]));
                }
                if (Request.QueryString["TO_TRAN_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TO_TRAN_DATE", Request.QueryString["TO_TRAN_DATE"]));
                }
                if (Request.QueryString["POLICY_NUMBER"] != "NULL")
                {

                    paramList.Add(new ReportParameter("POLICY_NUMBER", Request.QueryString["POLICY_NUMBER"]));
                }
                if (Request.QueryString["CUSTOMER_ID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("CUSTOMER_ID", Request.QueryString["CUSTOMER_ID"]));
                }
                if (Request.QueryString["ORDER_BY"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ORDER_BY", Request.QueryString["ORDER_BY"]));
                }

                rptVW.ServerReport.ReportPath = ReportPath + "/AgencyTransaction";
            }

            //Added For Itrack Issue #6135.

            else if (PageName == "AgencyTransactionWithComm")
            {
                if (Request.QueryString["AGENCY_ID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                }
                if (Request.QueryString["EFFECTIVE_MONTH"] != "NULL")
                {

                    paramList.Add(new ReportParameter("EFFECTIVE_MONTH", Request.QueryString["EFFECTIVE_MONTH"]));
                }
                if (Request.QueryString["EFFECTIVE_YEAR"] != "NULL")
                {

                    paramList.Add(new ReportParameter("EFFECTIVE_YEAR", Request.QueryString["EFFECTIVE_YEAR"]));
                }
                if (Request.QueryString["FROM_EFF_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROM_EFF_DATE", Request.QueryString["FROM_EFF_DATE"]));
                }

                if (Request.QueryString["TO_EFF_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TO_EFF_DATE", Request.QueryString["TO_EFF_DATE"]));
                }
                if (Request.QueryString["TRAN_MONTH"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TRAN_MONTH", Request.QueryString["TRAN_MONTH"]));
                }
                if (Request.QueryString["TRAN_YEAR"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TRAN_YEAR", Request.QueryString["TRAN_YEAR"]));
                }
                if (Request.QueryString["FROM_TRAN_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROM_TRAN_DATE", Request.QueryString["FROM_TRAN_DATE"]));
                }
                if (Request.QueryString["TO_TRAN_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TO_TRAN_DATE", Request.QueryString["TO_TRAN_DATE"]));
                }
                if (Request.QueryString["POLICY_NUMBER"] != "NULL")
                {

                    paramList.Add(new ReportParameter("POLICY_NUMBER", Request.QueryString["POLICY_NUMBER"]));
                }
                if (Request.QueryString["CUSTOMER_ID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("CUSTOMER_ID", Request.QueryString["CUSTOMER_ID"]));
                }
                if (Request.QueryString["ORDER_BY"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ORDER_BY", Request.QueryString["ORDER_BY"]));
                }

                rptVW.ServerReport.ReportPath = ReportPath + "/AgencyTransactionWithCommission";

            }

            //Added For  Itrack Issue # 6496.
            else if (PageName == "NonPayCancellation")
            {
                if (Request.QueryString["FROM_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROM_DATE", Request.QueryString["FROM_DATE"]));
                }
                if (Request.QueryString["TO_DATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TO_DATE", Request.QueryString["TO_DATE"]));
                }
                if (Request.QueryString["ORDER_BY"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ORDER_BY", Request.QueryString["ORDER_BY"]));
                }
                if (Request.QueryString["AGENCY_ID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("AGENCY_ID", Request.QueryString["AGENCY_ID"]));
                }


                rptVW.ServerReport.ReportPath = ReportPath + "/NonPayCancellation";
            }





            else if (PageName == "AccountSummary")
            {

                if (Request.QueryString["FROMSOURCE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROMSOURCE", Request.QueryString["FROMSOURCE"]));
                }

                if (Request.QueryString["TOSOURCE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TOSOURCE", Request.QueryString["TOSOURCE"]));

                }


                if (Request.QueryString["FROMDATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROMDATE", Request.QueryString["FROMDATE"]));
                }

                if (Request.QueryString["TODATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TODATE", Request.QueryString["TODATE"]));
                }

                if (Request.QueryString["ACCOUNT_ID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ACCOUNT_ID", Request.QueryString["ACCOUNT_ID"]));
                }

                if (Request.QueryString["UPDATED_FROM"] != "NULL")
                {

                    paramList.Add(new ReportParameter("UPDATED_FROM", Request.QueryString["UPDATED_FROM"]));

                }

                if (Request.QueryString["STATE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("STATE", Request.QueryString["STATE"]));
                }

                if (Request.QueryString["LOB"] != "NULL")
                {

                    paramList.Add(new ReportParameter("LOB", Request.QueryString["LOB"]));

                }

                if (Request.QueryString["MONTH"] != "NULL" && Request.QueryString["YEAR"] != "NULL")
                {
                    paramList.Add(new ReportParameter("YEARMONTH", Request.QueryString["MONTH"] + "," + Request.QueryString["YEAR"]));
                }

                if (Request.QueryString["VENDORID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("VENDORID", Request.QueryString["VENDORID"]));

                }

                if (Request.QueryString["CustomerID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("CUSTOMERID", Request.QueryString["CustomerID"]));

                }

                if (Request.QueryString["FROMAMT"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROMAMT", Request.QueryString["FROMAMT"]));

                }

                if (Request.QueryString["TOAMT"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TOAMT", Request.QueryString["TOAMT"]));

                }

                if (Request.QueryString["TRANSTYPE"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TRANSTYPE", Request.QueryString["TRANSTYPE"]));

                }

                if (Request.QueryString["PolicyID"] != "NULL")
                {

                    paramList.Add(new ReportParameter("POLICYID", Request.QueryString["PolicyID"]));

                }

                if (Request.QueryString["FROMACT"] != "NULL")
                {

                    paramList.Add(new ReportParameter("FROMACT", Request.QueryString["FROMACT"]));

                }

                if (Request.QueryString["TOACT"] != "NULL")
                {

                    paramList.Add(new ReportParameter("TOACT", Request.QueryString["TOACT"]));

                }

                if (Request.QueryString["ORDERBY"] != "NULL")
                {

                    paramList.Add(new ReportParameter("ORDERBY", Request.QueryString["ORDERBY"]));

                }

                if (Request.QueryString["POLICY_NUMBER"] != "NULL")
                {

                    paramList.Add(new ReportParameter("POLICY_NUMBER", Request.QueryString["POLICY_NUMBER"]));

                }
                if (Request.QueryString["CalledFrom"] == "GLAT")
                {

                    rptVW.ServerReport.ReportPath = ReportPath + "/Acct_GeneralLedgerTrans";
                }
                else if (Request.QueryString["CalledFrom"] == "GLAN")
                {

                    rptVW.ServerReport.ReportPath = ReportPath + "/GLSummary";


                }
                else
                {

                    rptVW.ServerReport.ReportPath = ReportPath + "/Acct_GeneralLedgerSummary";

                }

            }
            //else
            //    rptVW.ServerReport.ReportPath = ReportPath + "/" + PageName;
            if (paramList.Count>0)
            rptVW.ServerReport.SetParameters(paramList);
            //End Setting Parameters
            #endregion
        }
    }
}