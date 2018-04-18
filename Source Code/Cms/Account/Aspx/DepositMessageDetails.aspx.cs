/******************************************************************************************
<Author					: - > Pradeep Kushwaha 	
<Start Date				: -	> 18-Aug-20101
<End Date				: - > 
<Description			: - > The requirement to show a popup window while commit deposit if there is any unpaid installment is left
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
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;

namespace Cms.Account.Aspx
{
    public partial class DepositMessageDetails : AccountBase
    {
        System.Resources.ResourceManager objResourceMgr;
        public string capTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            //itrack - 1049 - Validation message display
            // Put user code to initialize the page here
            base.ScreenId = "187_0_4";
            #region Button Permissions
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnSubmitAnyway.CmsButtonClass = CmsButtonType.Write;
            btnSubmitAnyway.PermissionString = gstrSecurityXML;

            btnClose.CmsButtonClass = CmsButtonType.Write;
            btnClose.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            #endregion
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.DepositMessageDetails", System.Reflection.Assembly.GetExecutingAssembly());
            this.SetCaptions();
            CreateTable();
             
        }
        #region setcaption in resource file
        private void SetCaptions()
        {
            lblDepositLineException.Text = ClsMessages.GetMessage(base.ScreenId, "1"); 
            hid_close.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1506");
            capTitle = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1808");
        }
        #endregion
        private void CreateTable()
        {

            int DEPOSIT_ID = 0;
            string DEPOSIT_TYPE = string.Empty;//Added to Co-insurance Exception Details
            if (Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "" && Request.QueryString["DEPOSIT_ID"].ToString() != "New")
                DEPOSIT_ID = Convert.ToInt32(Request.QueryString["DEPOSIT_ID"].ToString());
            if (Request.QueryString["DEPOSIT_TYPE"] != null && Request.QueryString["DEPOSIT_TYPE"].ToString() != "")
                DEPOSIT_TYPE = Request.QueryString["DEPOSIT_TYPE"].ToString();
            

            StringBuilder strBuilder = new StringBuilder();
            ClsDepositDetails objDepositDetails = new ClsDepositDetails();
            DataSet objDataSet = new DataSet();

            objDataSet = objDepositDetails.GetDepositLineItemDetails(DEPOSIT_ID, DEPOSIT_TYPE);

            if (objDataSet == null)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1807");
                return;
            }
            else
            {
                if (objDataSet.Tables[0].Rows.Count == 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1807");
                    return;
                }
            }
          
            //Co-Insurance details 
            if (DEPOSIT_TYPE == "14832")//Co-Insurance
            {
                strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='5%'><b>" + objResourceMgr.GetString("txtDeposit") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + objResourceMgr.GetString("txtLEADER_POLICY_NUMBER") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + objResourceMgr.GetString("txtCO_ENDORSEMENT_NO") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + objResourceMgr.GetString("txtINSTALLMENT_NO") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='13%'><b>" + objResourceMgr.GetString("txtReceipt_Amount") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='30%'><b>" + objResourceMgr.GetString("txtException_Reason") + "</b></td>");
                strBuilder.Append("</tr>");

                try
                {
                    if (objDataSet != null)
                    {
                        foreach (DataRow dr in objDataSet.Tables[0].Rows)
                        {
                            strBuilder.Append("<tr>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["DEPOSIT_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["LEADER_POLICY_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["CO_ENDORSEMENT_NO"].ToString().ToUpper() + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["INSTALLMENT_NO"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Right'>" + dr["RECEIPT_AMOUNT"] + "</td>");
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
                strBuilder.Append("</TD></TR></table>");
            }
            else if (DEPOSIT_TYPE == "14831")//Normal
            {
                strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='5%'><b>" + objResourceMgr.GetString("txtDeposit") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>" + objResourceMgr.GetString("txtINSTALLMENT_NO") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>" + objResourceMgr.GetString("txtOur_Number") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='13%'><b>" + objResourceMgr.GetString("txtReceipt_Amount") + "</b></td>");
                strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='30%'><b>" + objResourceMgr.GetString("txtException_Reason") + "</b></td>");
                strBuilder.Append("</tr>");

                try
                {
                    if (objDataSet != null)
                    {
                        foreach (DataRow dr in objDataSet.Tables[0].Rows)
                        {

                            strBuilder.Append("<tr>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["DEPOSIT_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Center'>" + dr["INSTALLMENT_NO"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Left'>" + dr["OUR_NUMBER"] + "</td>");
                            strBuilder.Append("<td class='DataGridRow' align='Right'>" + dr["RECEIPT_AMOUNT"] + "</td>");
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
                strBuilder.Append("</TD></TR></table>");
            }
            //Modified till here 
            tdRTLImportHistoryDetails.InnerHtml = strBuilder.ToString();
        }
    }
}