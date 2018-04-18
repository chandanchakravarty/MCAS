/******************************************************************************************
<Author					: - > Pradeep Kushwaha 	
<Start Date				: -	> 12-Nov-2010
<End Date				: - > 
<Description			: - > To Show the report of Exception on depost items
<Review Date			: - >
<Reviewed By			: - >
   
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
using System.Text;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
    public partial class ExceptionItemsDetails : AccountBase
    {
        protected System.Web.UI.HtmlControls.HtmlTableCell tdRTLImportHistoryDetails;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capTime;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPrint;
        public string capTitle;
        System.Resources.ResourceManager objResourceMgr;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Put user code to initialize the page here
            base.ScreenId = "187_0_3";
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.ExceptionItemsDetails", System.Reflection.Assembly.GetExecutingAssembly());
            this.SetCaptions();
            CreateTable();
            
        }
        #region setcaption in resource file
        private void SetCaptions()
        {
            lblDepositLineException.Text = objResourceMgr.GetString("lblDepositLineException");
            lblTodayDate.Text = objResourceMgr.GetString("lblTodayDate");
            lblDepositLineItmes.Text = objResourceMgr.GetString("lblDepositLineItmes");
            capTime.Text = objResourceMgr.GetString("capTime");
            hidPrint.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505");
            hid_close.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1506");
            capTitle = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1808");
        }
        #endregion
        private void CreateTable()
        {
            
            int DEPOSIT_ID = 0;
            string DEPOSIT_TYPE = string.Empty;//Added to Co-insurance Exception Details
            if (Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "" && Request.QueryString["DEPOSIT_ID"].ToString() != "New")
                DEPOSIT_ID =Convert.ToInt32(Request.QueryString["DEPOSIT_ID"].ToString());
            //Added to Co-insurance Exception Details
            if (Request.QueryString["DEPOSIT_TYPE"] != null && Request.QueryString["DEPOSIT_TYPE"].ToString() != "")
                DEPOSIT_TYPE = Request.QueryString["DEPOSIT_TYPE"].ToString();
            //Added till here 
           
            StringBuilder strBuilder = new StringBuilder();

            ClsDepositDetails objDepositDetails = new ClsDepositDetails();
            
            DataSet objDataSet = new DataSet();

            objDataSet = objDepositDetails.GetDepositLineItemExceptionDetails(DEPOSIT_ID);

            if (objDataSet == null)
            {
                lblMessage.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1807");// "No records found for this search criteria.";
                return;
            }
            else
            {
                if (objDataSet.Tables[0].Rows.Count == 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1807");//"No records found for this search criteria.";
                    return;
                }
            }
            //Modified to dispay the Co-Insurance details 
            if (DEPOSIT_TYPE == "14832")//Co-Insurance
            {
                strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='5%'><b>" + objResourceMgr.GetString("txtDeposit") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + objResourceMgr.GetString("txtDate") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + objResourceMgr.GetString("txtLEADER_POLICY_NUMBER") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + objResourceMgr.GetString("txtCO_ENDORSEMENT_NO") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='13%'><b>" + objResourceMgr.GetString("txtReceipt_Amount") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Right'width='10%'><b>" + objResourceMgr.GetString("txtINSTALLMENT_NO") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='30%'><b>" + objResourceMgr.GetString("txtException_Reason") + "</b></td>");
                strBuilder.Append("</tr>");

                try
                {
                    if (objDataSet != null)
                    {
                        foreach (DataRow dr in objDataSet.Tables[0].Rows)
                        {
                            strBuilder.Append("<tr>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["DEPOSIT_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + Convert.ToDateTime(dr["DEPOSIT_TRAN_DATE"]).ToShortDateString() + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["LEADER_POLICY_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["CO_ENDORSEMENT_NO"].ToString().ToUpper() + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Right'>" + dr["RECEIPT_AMOUNT"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["INSTALLMENT_NO"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["EXCEPTION_REASON"] + "</td>");
                            strBuilder.Append("</tr>");
                        }
                    }
                    else
                    {
                        strBuilder.Append("<tr>");
                        strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>No Record Found.</td>");
                        strBuilder.Append("</tr>");
                        tdRTLImportHistoryDetails.InnerHtml = strBuilder.ToString();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                string strPrint = hidPrint.Value;
                string strClose = hid_close.Value;
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td id='buttons' colSpan='11'><span id='spn_Button'>");
                strBuilder.Append("<table cellSpacing='0' cellPadding='2' width='100%'>");
                strBuilder.Append("<tr class='tableFooter'>");
                strBuilder.Append("<td height='24' class='midcolora'><INPUT class='clsButton' onclick='JavaScript:showPrint();' type='button' value='" + strPrint + "'>");
                strBuilder.Append("<!-- <a href='JavaScript:showPrint()'><img src='../images/Print_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
                strBuilder.Append("<td height='24' class='midcolorr'><INPUT class='clsButton' onclick='JavaScript:window.close();' type='button' value='" + strClose + "'>");
                strBuilder.Append("<!-- <a href='JavaScript:window.close()'><img src='../images/close_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
                strBuilder.Append("</tr>");
                strBuilder.Append("</table>");
                strBuilder.Append("</span>");
                strBuilder.Append("</td>");
                strBuilder.Append("</tr>");
                strBuilder.Append("</TD></TR></table>");
            }
            else
            {
                strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='5%'><b>" + objResourceMgr.GetString("txtDeposit") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + objResourceMgr.GetString("txtDate") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + objResourceMgr.GetString("txtOur_Number") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='13%'><b>" + objResourceMgr.GetString("txtBoleto_Amount") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='13%'><b>" + objResourceMgr.GetString("txtReceipt_Amount") + "</b></td>");
                //strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='5%'><b>" + objResourceMgr.GetString("txtException") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Right'width='14%'><b>" + objResourceMgr.GetString("txtBoleto_Status") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='30%'><b>" + objResourceMgr.GetString("txtException_Reason") + "</b></td>");
                strBuilder.Append("</tr>");

                try
                {
                    if (objDataSet != null)
                    {
                        foreach (DataRow dr in objDataSet.Tables[0].Rows)
                        {

                            strBuilder.Append("<tr>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["DEPOSIT_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + Convert.ToDateTime(dr["DEPOSIT_TRAN_DATE"]).ToShortDateString() + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["OUR_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Right'>" + dr["BOLETO_AMOUNT"].ToString().ToUpper() + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Right'>" + dr["RECEIPT_AMOUNT"] + "</td>");
                            //Commneted for itrack 966/913 by Pradeep Kushwaha
                            //if (dr["IS_EXCEPTION"].ToString().Trim().ToUpper() == "Y")
                            //    strBuilder.Append("<td class='DataGridRow' align='Right'>" + objResourceMgr.GetString("Yes") + "</td>");
                            //else
                            //    strBuilder.Append("<td class='DataGridRow' align='Right'>" +objResourceMgr.GetString("No")+ "</td>");
                            //Added till here
                            if (dr["BOLETO_STATUS"].ToString().Trim() == "N")
                                strBuilder.Append("<td class='DataGridRow' align='Right'>" + objResourceMgr.GetString("txtDeActiveBoleto") + "</td>");
                            else if (dr["BOLETO_STATUS"].ToString().Trim() == "--")
                                strBuilder.Append("<td class='DataGridRow' align='Right'>" + "--" + "</td>");
                            else
                                strBuilder.Append("<td class='DataGridRow' align='Right'>" + objResourceMgr.GetString("txtActiveBoleto") + "</td>");



                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["EXCEPTION_REASON"] + "</td>");
                            strBuilder.Append("</tr>");
                        }

                    }
                    else
                    {
                        strBuilder.Append("<tr>");
                        strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>No Record Found.</td>");
                        strBuilder.Append("</tr>");
                        tdRTLImportHistoryDetails.InnerHtml = strBuilder.ToString();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                string strPrint = hidPrint.Value;
                string strClose = hid_close.Value;
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td id='buttons' colSpan='11'><span id='spn_Button'>");
                strBuilder.Append("<table cellSpacing='0' cellPadding='2' width='100%'>");
                strBuilder.Append("<tr class='tableFooter'>");
                strBuilder.Append("<td height='24' class='midcolora'><INPUT class='clsButton' onclick='JavaScript:showPrint();' type='button' value='" + strPrint + "'>");
                strBuilder.Append("<!-- <a href='JavaScript:showPrint()'><img src='../images/Print_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
                strBuilder.Append("<td height='24' class='midcolorr'><INPUT class='clsButton' onclick='JavaScript:window.close();' type='button' value='" + strClose + "'>");
                strBuilder.Append("<!-- <a href='JavaScript:window.close()'><img src='../images/close_blue.gif' border='0' WIDTH='78' HEIGHT='24'></a> --></td>");
                strBuilder.Append("</tr>");
                strBuilder.Append("</table>");
                strBuilder.Append("</span>");
                strBuilder.Append("</td>");
                strBuilder.Append("</tr>");
                strBuilder.Append("</TD></TR></table>");
            }
            //Modified till here 
            tdRTLImportHistoryDetails.InnerHtml = strBuilder.ToString();
        }
    }
}
