using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlProcess;
using Cms.Claims;
using Cms.Model.Claims;
using System.Data;
using Cms.CmsWeb.Controls;
using System.Text;
using ABCpdf = WebSupergoo.ABCpdf7;
using WebSupergoo.ABCpdf7;

namespace Cms.Claims.Aspx
{
    public partial class ClaimReceipt : Cms.Claims.ClaimBase
    {

        string TempClaimID = "";
        string TempActivityID = "";
        string TempReportType = "";
        string CustomerID="";
        string PolicyID = "";
        string POlicyVersionID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
            return;

        if (Request["CLAIM_ID"] != null && Request["CLAIM_ID"].ToString() != "")
            TempClaimID = Request["CLAIM_ID"].ToString();

        if (Request["ACTIVITY_ID"] != null && Request["ACTIVITY_ID"].ToString() != "")
            TempActivityID = Request["ACTIVITY_ID"].ToString();

        if (Request["REPORT_TYPE"] != null && Request["REPORT_TYPE"].ToString() != "")
            TempReportType = Request["REPORT_TYPE"].ToString();

        if (Request["CUSTOMER_ID"] != null && Request["CUSTOMER_ID"].ToString() != "")
            CustomerID = Request["CUSTOMER_ID"].ToString();

        if (Request["POLICY_ID"] != null && Request["POLICY_ID"].ToString() != "")
            PolicyID = Request["POLICY_ID"].ToString();

        if (Request["POLICY_VERSION_ID"] != null && Request["POLICY_VERSION_ID"].ToString() != "")
            POlicyVersionID = Request["POLICY_VERSION_ID"].ToString();

        if (TempClaimID != "" && TempActivityID != "")
        {
            if (TempReportType == "CLAIM_RECEIPT")
            {
                int ClaimID = 0;
                int ActivityID = 0;
                if (int.TryParse(TempClaimID, out ClaimID) && int.TryParse(TempActivityID, out ActivityID))
                {
                    GetClaimReceiptReports(ClaimID, ActivityID,"CLM_RECEIPT");
                }
            }
            if (TempReportType == "REMIND_LETTER")
            {
                int ClaimID = 0;
                int ActivityID = 0;
                if (int.TryParse(TempClaimID, out ClaimID) && int.TryParse(TempActivityID, out ActivityID))
                {
                    GetClaimReceiptReports(ClaimID, ActivityID, "CLM_REMIND");
                }

            }

        }

    }

    public string FILEPATH { get; set; }
       public void GetClaimReceiptReports(int ClaimID, int ActivityID, string ReportType)
       {
           	System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ClaimReceipt" ,System.Reflection.Assembly.GetExecutingAssembly());

          
           //ClsProductPdfXml c = new ClsProductPdfXml();
           //DataSet ds = c.GetClaimReports(ClaimID, ActivityID, ReportType);

           //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           //{
           //    DataTable dt = ds.Tables[0];
           //    for (int i = 0; i < dt.Rows.Count; i++)
           //    {
           //        string str = ds.Tables[0].Rows[0]["DOC_TEXT"].ToString();
                 
               Response.Redirect("/cms/application/Aspx/DeclarationPage.aspx?CALLED_FROM=" + ReportType + "" + "&Claim_ID=" + ClaimID + "&Activity_ID=" + ActivityID+"&CUSTOMER_ID=" + CustomerID + "&POLICY_ID=" + PolicyID + "&POLICY_VERSION_ID=" + POlicyVersionID);
           //    }
           //}
           //else
           //{
           //    Response.Write(objResourceMgr.GetString("EmptyReport"));
           //}


           
       }

    
    }
}
