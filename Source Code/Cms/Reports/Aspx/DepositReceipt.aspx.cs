/******************************************************************************************
Modification History
<Modified Date			: - 8-April-2011 
<Modified By			: - Pradeep Kushwaha 
<Purpose				: - To show Commited deposit details
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
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;
using Cms.DataLayer;
using Model.Account;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlAccount;
using Cms.CmsWeb.Controls;
using System.IO;
using System.Globalization;


namespace Cms.Reports.Aspx
{
    public partial class DepositReceipt : Cms.Account.AccountBase
    {

        ResourceManager objResourceMgr = null;
        protected Cms.CmsWeb.Controls.CmsButton btnViewAllReceipt;
        System.Globalization.CultureInfo oldculture;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "535_0";
            objResourceMgr = new ResourceManager("Cms.Reports.Aspx.DepositReceipt", Assembly.GetExecutingAssembly());
            lblDEPOSIT.Text = objResourceMgr.GetString("lblDEPOSIT");
            btnIOF_REPORT.Text = objResourceMgr.GetString("btnIOF_REPORT");

            btnViewAllReceipt.CmsButtonClass = CmsButtonType.Execute;
            btnViewAllReceipt.PermissionString = gstrSecurityXML;

            btnIOF_REPORT.CmsButtonClass = CmsButtonType.Execute;
            btnIOF_REPORT.PermissionString = gstrSecurityXML;

            btnViewAllReceipt.Text = ClsMessages.FetchGeneralMessage("1998");

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "")
                {
                    hidDEPOSIT_ID.Value = Request.QueryString["DEPOSIT_ID"].ToString();
                    rptDepositReceiptBind();
                }
            }           

        }

        private void rptDepositReceiptBind()
        {
            ClsAddDepositDetailsinfo objAddDepositDetailsinfo = new ClsAddDepositDetailsinfo();
            objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue = int.Parse(hidDEPOSIT_ID.Value);
            DataSet dsDepositReceipt = null;
            dsDepositReceipt = objAddDepositDetailsinfo.FetchDataOfDepositReceipt(ClsCommon.BL_LANG_ID);
            if (dsDepositReceipt.Tables[0].Rows.Count != 0)
            {
                grdCOMMITED_DEPOSIT.DataSource = dsDepositReceipt.Tables[0];
                grdCOMMITED_DEPOSIT.DataBind();
            }//if (dsDepositReceipt.Tables[0].Rows.Count != 0)
            else
                btnViewAllReceipt.Visible = false;
        }//private void rptDepositReceiptBind()


        protected void grdCOMMITED_DEPOSIT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label lblNAME = (Label)e.Row.FindControl("lblNAME");
                lblNAME.Text = objResourceMgr.GetString("lblNAME");

                Label lblPOLICY_NUMBER = (Label)e.Row.FindControl("lblPOLICY_NUMBER");
                lblPOLICY_NUMBER.Text = objResourceMgr.GetString("lblPOLICY_NUMBER");
                Label lblENDORSEMENT_NO = (Label)e.Row.FindControl("lblENDORSEMENT_NO");
                lblENDORSEMENT_NO.Text = objResourceMgr.GetString("lblENDORSEMENT_NO");

                Label lblINSTALLMENT_NO = (Label)e.Row.FindControl("lblINSTALLMENT_NO");
                lblINSTALLMENT_NO.Text = objResourceMgr.GetString("lblINSTALLMENT_NO");

                Label lblRISK_PREMIUM = (Label)e.Row.FindControl("lblRISK_PREMIUM");
                lblRISK_PREMIUM.Text = objResourceMgr.GetString("lblRISK_PREMIUM");
                Label lblFEE = (Label)e.Row.FindControl("lblFEE");
                lblFEE.Text = objResourceMgr.GetString("lblFEE");
                Label lblTAX = (Label)e.Row.FindControl("lblTAX");
                lblTAX.Text = objResourceMgr.GetString("lblTAX");
                Label lblINTEREST = (Label)e.Row.FindControl("lblINTEREST");
                lblINTEREST.Text = objResourceMgr.GetString("lblINTEREST");
                Label lblLATE_FEE = (Label)e.Row.FindControl("lblLATE_FEE");
                lblLATE_FEE.Text = objResourceMgr.GetString("lblLATE_FEE");
                Label lblRECEIPT_AMOUNT = (Label)e.Row.FindControl("lblRECEIPT_AMOUNT");
                lblRECEIPT_AMOUNT.Text = objResourceMgr.GetString("lblRECEIPT_AMOUNT");
                Label lblEXCEPTION_REASON = (Label)e.Row.FindControl("lblEXCEPTION_REASON");
                lblEXCEPTION_REASON.Text = objResourceMgr.GetString("lblEXCEPTION_REASON");
                Label lblOUR_NUMBER = (Label)e.Row.FindControl("lblOUR_NUMBER");
                lblOUR_NUMBER.Text = objResourceMgr.GetString("lblOUR_NUMBER");
                Label lblIS_APPROVE = (Label)e.Row.FindControl("lblIS_APPROVE");
                lblIS_APPROVE.Text = objResourceMgr.GetString("lblIS_APPROVE");

                Label lblRECEIPT = (Label)e.Row.FindControl("lblRECEIPT");
                lblRECEIPT.Text = objResourceMgr.GetString("lblRECEIPT");
                //iTrack 1323 Notes By Paula Dated 13-July-2011
                Label lblRECEIPT_DATE = (Label)e.Row.FindControl("lblRECEIPT_DATE");
                lblRECEIPT_DATE.Text = objResourceMgr.GetString("lblPAYMENT_DATE");
                //till here 
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HtmlInputHidden hidDEPOSIT_ID = (HtmlInputHidden)e.Row.FindControl("hidDEPOSIT_ID");
                HtmlInputHidden hidRECEIPT_NUM = (HtmlInputHidden)e.Row.FindControl("hidRECEIPT_NUM");
                HtmlInputHidden hidCD_LINE_ITEM_ID = (HtmlInputHidden)e.Row.FindControl("hidCD_LINE_ITEM_ID");
                HtmlInputHidden hidCUSTOMER_ID = (HtmlInputHidden)e.Row.FindControl("hidCUSTOMER_ID");
                HtmlInputHidden hidPOLICY_ID = (HtmlInputHidden)e.Row.FindControl("hidPOLICY_ID");
                HtmlInputHidden hidPOLICY_VERSION_ID = (HtmlInputHidden)e.Row.FindControl("hidPOLICY_VERSION_ID");


                HyperLink hlGenerateReceipt = (HyperLink)e.Row.FindControl("hlGenerateReceipt");
                hlGenerateReceipt.Text = objResourceMgr.GetString("lblRECEIPT");
                hlGenerateReceipt.Attributes.Add("onclick", "javascript:GenerateReceipt('" + hidRECEIPT_NUM.Value + "','" + hidDEPOSIT_ID.Value + "','" + hidCD_LINE_ITEM_ID.Value + "','" + hidCUSTOMER_ID.Value + "','" + hidPOLICY_ID.Value + "','" + hidPOLICY_VERSION_ID.Value + "')");

                HtmlInputHidden hidINSTALLMENT_NO = (HtmlInputHidden)e.Row.FindControl("hidINSTALLMENT_NO");
                HtmlInputHidden hidIS_APPROVE1 = (HtmlInputHidden)e.Row.FindControl("hidIS_APPROVE1");

                if (hidINSTALLMENT_NO.Value != "1" || hidIS_APPROVE1.Value.ToString().Trim() == "R")//Change for itrack - 913 on 14-06-2011
                    hlGenerateReceipt.Visible = false;
                else
                    hlGenerateReceipt.Visible = true;
                //iTrack 1323 Notes By Paula Dated 13-July-2011
                Label lblPAYMENT_DATE = (Label)e.Row.FindControl("lblPAYMENT_DATE");
                lblPAYMENT_DATE.Text = Convert.ToDateTime(lblPAYMENT_DATE.Text).ToShortDateString();
                //till here 
                //grvIOFReport.Columns[0].Visible = false;
                //grvIOFReport.Columns[1].Visible = false;
                //grvIOFReport.Columns[2].Visible = false;

               
            }
        }

        protected void btnIOF_REPORT_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            DataSet dsIofReport = null;

            ClsAddDepositDetailsinfo objAddDepositDetailsinfo = new ClsAddDepositDetailsinfo();
            if (hidDEPOSIT_ID.Value != "")
            {
                dsIofReport = objAddDepositDetailsinfo.FetchIOFReport(int.Parse(hidDEPOSIT_ID.Value));
                if (dsIofReport != null && dsIofReport.Tables.Count > 0 && dsIofReport.Tables[0].Rows.Count > 0)
                {
                    // grvIOFReport.DataSource = dsIofReport.Tables[0];
                    //grvIOFReport.DataBind();



                    HttpResponse response = HttpContext.Current.Response;

                    // first let's clean up the response.object   
                    response.Clear();
                    response.Charset = "";

                    response.ClearContent();
                    response.ClearHeaders();
                    response.AddHeader("cache-control", "max-age=1");
                    //*************************************


                    // set the response mime type for excel   
                    response.ContentType = "application/vnd.ms-excel";
                    response.AddHeader("Content-Disposition", "attachment;filename=IOF.xls"); //\"" + filename + "\"");

                    // create a string writer   
                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                        {
                            grvIOFReport.DataSource = dsIofReport.Tables[0];
                            grvIOFReport.DataBind();

                            grvIOFReport.RenderControl(htw);

                            DateTimeFormatInfo DateFormatinfoBR = new CultureInfo(enumCulture.BR, true).DateTimeFormat;
                            DateTimeFormatInfo DateFormatinfoUS = new CultureInfo(enumCulture.US, true).DateTimeFormat;
                            DateTime dt2 = new DateTime();
                            if (dt2.ToString().Trim() != "")
                            {

                                if (ClsCommon.BL_LANG_ID == 2)
                                    dt2 = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now, DateFormatinfoBR), DateFormatinfoUS);
                                else
                                    dt2 = Convert.ToDateTime(DateTime.Now, DateFormatinfoUS);
                            }
                            else
                                dt2 = ConvertToDate(null);

                            response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'>");
                            response.Write("<style>td{mso-number-format:\\@}</style>");
                            response.Write("<h4><table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>Ao</td></tr> " +

                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["BANK_NAME"].ToString() + " </td> " +
                                           "<td width=80 style='width:65pt'></td></tr> " +

                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["BANK_CITY"].ToString() + " - " + dsIofReport.Tables[0].Rows[0]["BANK_STATE"].ToString() + " </td> " +
                                           "<td width=80 style='width:65pt'></td></tr> " +

                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td colspan= 8 width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["FIXED_TEXT"].ToString() + "</td> " +
                                           //"<td width=80 style='width:65pt'></td> " +
                                           //"<td width=80 style='width:65pt'></td> " +
                                           //"<td width=80 style='width:65pt'></td> " +
                                           "<td width=80 style='width:65pt'></td> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>EM " + dt2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " </td> " +
                                           "<td class=xl67></td> " +
                                           "</tr></table></h4>");
                            
                            response.Write("");
                            response.Write(sw.ToString());
                            response.Write("<h4><table border=0 cellpadding=0 cellspacing=0 width=1768 style='border-collapse:collapse;table-layout:fixed;width:1328pt'> " +
                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>TOT:  R$ </td> " +
                                           "<td style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'></td> " +
                                           "<td style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["PREMIUM_TOTAL"].ToString() + " </td> " +
                                           "<td width=80 style='width:65pt'></td> " +
                                           "<td width=80 style='width:65pt'></td> " +
                                           "<td width=80 style='width:65pt'></td> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["VALOR_PREMIO_TOTAL"].ToString() + " </td> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["VALOR_ISOF_TOTAL"].ToString() + " </td></tr> " +

                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>QUANT. DOC.</td> " +
                                            "<td  width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["QUANT. DOC."].ToString() + "</td> " +

                                           "<td class=xl67></td></tr> " +

                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td colspan= 10 style='height:30.75pt;font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>AUTORIZAMOS QUE O VALOR DE R$ " + dsIofReport.Tables[0].Rows[0]["VALOR_ISOF_TOTAL"].ToString() + ", SEJA DEBITADO EM NOSSA CONTA CORRENTE " + dsIofReport.Tables[0].Rows[0]["NUMBER"].ToString() + " REFERENTE AO IMPOSTO SOBRE OPERACOES FINANCEIRAS, DOS DOCUMENTOS DE SEGUROS ACIMA. </td> " +
                                           "<td width=80 style='width:65pt'></td></tr> " +


                                           "<tr style='mso-height-source:userset;height:20pt'> " +
                                           "<td width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["BANK_CITY"].ToString() + "</td> " +
                                            "<td  width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dt2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "</td> " +
                                           
                                           "<td class=xl67></td></tr> " +

                                            "<tr style='mso-height-source:userset;height:20pt'> " +
                                            "<td colspan= 4 width=80 style='font-family:arial;color:black;font-size:15px;text-align:left;font-weight:700;'>" + dsIofReport.Tables[0].Rows[0]["FIXED_TEXT1"].ToString() + "</td> " +
                                           "</tr></table></h4>");
                            response.Write("</body></html>");
                            response.End();
                        }
                    }
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void grvIOFReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
        }

    }
}
