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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.IO;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlProcess;

namespace Cms.Claims.Aspx
{
    public partial class CededCOILetter : Cms.Claims.ClaimBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.CededCOILetter", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Claim_ID"].ToString() != "" && Request.QueryString["Claim_ID"].ToString() != null)
                {
                    hidClaimId.Value = Request.QueryString["Claim_ID"].ToString();
                }
                if (Request.QueryString["Activity_ID"].ToString() != "" && Request.QueryString["Activity_ID"].ToString() != null)
                {
                    hidActivityId.Value = Request.QueryString["Activity_ID"].ToString();
                }
                else
                {
                    hidActivityId.Value = "1";
                }
                if (Request.QueryString["PROCESS_TYPE"].ToString() != "" && Request.QueryString["PROCESS_TYPE"].ToString() != null)
                {
                    hidProcessType.Value = Request.QueryString["PROCESS_TYPE"].ToString();
                }
                GetClaimCededCOILetter(int.Parse(hidClaimId.Value), int.Parse(hidActivityId.Value), hidProcessType.Value);
              }
        }

        private void GetClaimCededCOILetter(int ClaimID, int ActivityID, string ReportType)
        {
            ClsProductPdfXml COI = new ClsProductPdfXml();
            DataSet ds = COI.GetClaimReports(ClaimID, ActivityID, ReportType);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str = ds.Tables[0].Rows[0]["DOC_TEXT"].ToString();
                    LiteralControl includet = new LiteralControl(str);
                    panelshow.Controls.Add(includet);
                        if (str.ToString() != "")
                            {
                                PrintFNOL.Visible = true;
                            }
                        else
                            {
                                PrintFNOL.Visible = false;
                            }
                }


            }
        }

    }
}