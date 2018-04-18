/******************************************************************************************
Modification History

    
<Modified Date			: - > 30/10/2006
<Modified By			: - > Mohit Agarwal
<Purpose				: - > Changes made for showing address/coverage and other policies of selected policy
******************************************************************************************/
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
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlApplication;

namespace Cms.Account.Aspx
{
    /// <summary>
    /// Summary description for AR_Inquiry_Info.
    /// </summary>
    public class AR_Inquiry_Info : Cms.Account.AccountBase
    {
        protected System.Web.UI.HtmlControls.HtmlTableCell tdArReport;
        protected System.Web.UI.WebControls.TextBox txtPolicyNo;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnPOLICY_NO;
        protected System.Web.UI.HtmlControls.HtmlImage imgPOLICY_NO;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnPrint;  //Added by Sibin on 27 Oct 08
        public string URL = "";
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.DropDownList cmbTransYr;
        public string strPolicyNo = "";
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPolicyNo;
        public string callingCustomerId = "";
        public string callingAgencyId = "";
        public string systemID = "";
        protected System.Web.UI.WebControls.Button btnClose;
        protected System.Web.UI.HtmlControls.HtmlForm AR_INQUIRY;
        public string calledfrom = "";
        private const string CALLED_FROM_CUST_MANAGER = "InCLT";
        protected System.Web.UI.WebControls.Label lblPolicyNumber;
        protected System.Web.UI.WebControls.Label lblTransaction;
        protected System.Web.UI.WebControls.Label lblInquiryinfo;

        //Added By lalit chauhan,nov 22,2010
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_NO;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMessage2;
        System.Resources.ResourceManager objResourceMgr;

        private void Page_Load(object sender, System.EventArgs e)
        {


            // Put user code to initialize the page here
            base.ScreenId = "120_8";
            SetCookieValue();

            btnSave.CmsButtonClass = CmsButtonType.Execute;
            btnSave.PermissionString = gstrSecurityXML;

            btnPrint.CmsButtonClass = CmsButtonType.Execute;  //Permission set for Print button by Sibin on 27 Oct 08
            btnPrint.PermissionString = gstrSecurityXML;


            calledfrom = Request.QueryString["CalledFrom"];
            btnClose.Attributes.Add("onclick", "javascript:window.close();");
            btnPrint.Attributes.Add("onclick", "javascript:window.print();"); // Added by Sibin on 27 Oct 08 to call print method 
            //btnsave Added For according to PRAVEEN KASANA mail.
            btnSave.Attributes.Add("onclick", "javascript:save();");
            hidMessage.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2022");
            hidMessage2.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2023");
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AR_Inquiry_Info", System.Reflection.Assembly.GetExecutingAssembly());


            URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();

            callingCustomerId = "";
            callingAgencyId = GetSystemId();
            if (Request.Params["CUSTOMER_ID"] != null && Request.Params["CUSTOMER_ID"] != "")
            {
                callingCustomerId = Request.Params["CUSTOMER_ID"].ToString();

            }

            if (!Page.IsPostBack)
            {

                int i = 1;

                while (i <= 10)
                {
                    cmbTransYr.Items.Add(i.ToString());
                    i++;
                }
                //Modified to 1 form 0: Itrack 6543
                cmbTransYr.SelectedIndex = 1;

                rfvPolicyNo.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("485");
                //Added By Lalit Nov 2010
                if (Request.Params["CUSTOMER_ID"] != null && Request.Params["CUSTOMER_ID"] != "")
                {
                    hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"].ToString();
                }
                if (Request.Params["POLICY_ID"] != null && Request.Params["POLICY_ID"] != "")
                {
                    hidPOLICY_ID.Value = Request.Params["POLICY_ID"].ToString();
                }
                if (Request.Params["POLICY_VERSION_ID"] != null && Request.Params["POLICY_VERSION_ID"] != "")
                {
                    hidPOLICY_VERSION_ID.Value = Request.Params["POLICY_VERSION_ID"].ToString();
                }
                if (Request.Params["CalledFor"] != null && Request.Params["CalledFor"] != "")
                {
                    if (Request.Params["CalledFor"].ToString().Trim() == "WORKFLOW")
                        ShowAccountReport();
                }


            }
            lblPolicyNumber.Text = objResourceMgr.GetString("CapPolicyNumber");
            lblTransaction.Text = objResourceMgr.GetString("CapTransactionsPastYears");
            lblInquiryinfo.Text = objResourceMgr.GetString("CapAccountInquiryReport");
            btnSave.Text = CmsWeb.ClsMessages.GetButtonsText(base.ScreenId, "btnSave");
            hidPolicy.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1770");
            btnClose.Text = objResourceMgr.GetString("btnClose");
        }





        public void CreateTable(int intCustomerId, int intPolicyId)
        {
            double RunningBalance = 0;
            lblMessage.Visible = false;
            int trans = 0;
            DateTime PastTrans;
            languageId = GetLanguageID();
            StringBuilder strBuilder = new StringBuilder();

            DataSet ds = null;
            Cms.BusinessLayer.BlAccount.ClsDeposit objAccount = new Cms.BusinessLayer.BlAccount.ClsDeposit();
            //			if(txtPolicyNo.Text.Trim().Equals(strPolicyNo))
            //			{
            //				strPolicyNo="";
            //			}
            //			else
            //	{
            strPolicyNo = txtPolicyNo.Text;
            //	}
            PastTrans = DateTime.Now;
            if (aAppDtFormat == enumDateFormat.DDMMYYYY)
            {
                PastTrans = Convert.ToDateTime(Convert.ToString(PastTrans.Day) + "/" + Convert.ToString(PastTrans.Month) + "/" + Convert.ToString(PastTrans.Year - cmbTransYr.SelectedIndex - 1));

            }
            else
            {
                PastTrans = Convert.ToDateTime(Convert.ToString(PastTrans.Month) + "/" + Convert.ToString(PastTrans.Day) + "/" + Convert.ToString(PastTrans.Year - cmbTransYr.SelectedIndex - 1));
            }
            ds = objAccount.GetArInfo(intCustomerId, intPolicyId, strPolicyNo, PastTrans, callingAgencyId, languageId);

            if (ds.Tables.Count != 0)
            {
                //Get The General Information of Policy
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    lblMessage.Text = ClsCommon.FetchGeneralMessage("1443", "");// "Either Policy Number is not valid Or Policy does not belong to this agency.";
                    lblMessage.Visible = true;
                    tdArReport.Visible = false;
                    ds = null;
                    return;
                }
                else
                { tdArReport.Visible = true; }
            }
            else
            {

                lblMessage.Text = ClsCommon.FetchGeneralMessage("1442", "");//"Please Enter a Valid Policy Number.";
                lblMessage.Visible = true;
                tdArReport.Visible = false;
                ds = null;
                return;
            }
            DataTable dt = ds.Tables[0];

            //	strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");

            strBuilder.Append("<tr height='20'>");
            strBuilder.Append("<td width='9%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapPolicyNumber") + "</b></td>");

            strBuilder.Append("<td width='9%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapPolicyTerm") + "</b></td>");
            strBuilder.Append("<td width='10%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapInsuredName") + "</b></td>");

            strBuilder.Append("<td width='20%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapAgency/AgencyCode") + "</b></td>");

            strBuilder.Append("<td width='10%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapAgencyPhone") + "</b></td>");
            strBuilder.Append("<td width='10%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapPolicyCurrency") + "</b></td>");

            strBuilder.Append("<td width='14%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapProduct") + "</b></td>");
            strBuilder.Append("<td width='12%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapPaymentPlan") + "</b></td>");
            strBuilder.Append("<td width='10%' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapStatus") + "</b></td>");

            strBuilder.Append("</tr>");

            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["POLICY_NO"] + "</td>");
            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["APP_TERM"] + "</td>");

            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["CUSTOMER_NAME"] + "</td>");
            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["AGEN_NAME"].ToString().ToUpper() + "<BR>"
                + dt.Rows[0]["AGENCY_ID"].ToString() + "</td>");
            //ToUpper()  Added FOr Itrack Issue #6191 

            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["AGENCY_PHONE"].ToString() + "</td>");
            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["POLICY_CURRENCY"] + "</td>");

            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["LOB_DESC"] + "</td>");
            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["PMT_PLAN"] + "</td>");
            strBuilder.Append("<td class='midcolora' align='Left'>" + dt.Rows[0]["STATUS"] + "</td>");
       
            string strPolicyType = dt.Rows[0]["POLICY_TYPE"].ToString();
            string strBillType = dt.Rows[0]["BILL_TYPE"].ToString();


            //Get The General Information of Accounting
            DataTable objData = new DataTable();
            //			if(ds == null)
            //			{
            //				lblMessage.Visible =true;
            //				//lblMessage.Text = "No transaction has been posted for the selected Policy.";
            //				lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
            //				return;
            //			}
            //			else
            //			{
            //				if (ds.Tables.Count == 0)
            //				{
            //					lblMessage.Visible =true;
            //					lblMessage.Text =  "No Account Transaction has been Posted for AB Type Policies.";
            //					return;
            //				}
            //
            //			}

            if (strBillType.Equals("AB"))
            {
                if (ds.Tables[7].Rows.Count == 0)
                {
                    lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
                    //"No Account Transaction has been Posted for AB Type Policies. There are no associated policies for the selected customer.";
                    lblMessage.Visible = true;
                    //	return;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["RENEWED"].ToString() != "YES")
                    {
                        lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
                        lblMessage.Visible = true;
                    }
                    //	return;
                }


            }
            else if (ds.Tables[7].Rows.Count == 0)
            {
                //lblMessage.Text    =  "There are no associated policies for the selected policy.";
                lblMessage.Text = "";//"There is no policy for the selected customer.";
                lblMessage.Visible = true;
                //	return;
            }


            //			if (ds.Tables.Count == 0)
            //			{
            //				lblMessage.Visible =true;
            //				lblMessage.Text =  lblMessage.Text + "/n No associated policies for the selected policy.";
            //				return;
            //			}

            objData = ds.Tables[1];

            //
            //			if (objData == null)
            //			{
            //				lblMessage.Visible =true;
            //				//lblMessage.Text = "No transaction has been posted for the selected policy.";
            //				lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
            //				trans = 1;
            ////				tdArReport.InnerHtml = strBuilder.ToString();
            ////				return;
            //			}
            //			else
            //			{
            //				if (objData.Rows.Count == 0)
            //				{
            //					lblMessage.Visible =true;
            //					lblMessage.Text = "No Account Transaction has been Posted for AB Type Policies.";
            //					trans = 1;
            ////					tdArReport.InnerHtml = strBuilder.ToString();
            ////					return;
            //				}
            //			}

            if (trans == 0)
            {
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Left' width='9%'><b>" + objResourceMgr.GetString("CapTransactionDate") + " </b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='9%'><b>" + objResourceMgr.GetString("CapEffectiveDate") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='9%'><b>" + objResourceMgr.GetString("CapDueDate") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='20%'><b>" + objResourceMgr.GetString("CapDescription") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='10%'><b>" + objResourceMgr.GetString("CapTotalAmount") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='12%'><b>" + objResourceMgr.GetString("CapPremiumAmount") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='8%'><b>" + objResourceMgr.GetString("CapTotalFee") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='20%' colspan='4'><b>" + objResourceMgr.GetString("CapAdjustedPolicyAmount") + "</b></td>");
                strBuilder.Append("</tr>");
                try
                {
                    if (objData != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in objData.Rows)
                        {
                            i++;
                            strBuilder.Append("<tr height='20'>");
                            strBuilder.Append("<td class='midcolora' align='Left'>" + ConvertDBDateToCulture(dr["SOURCE_TRAN_DATE"].ToString()) + "</td>");
                            strBuilder.Append("<td class='midcolora' align='Left'>" + ConvertDBDateToCulture(dr["SOURCE_EFF_DATE"].ToString()) + "</td>");//Convert.ToDateTime(dr["SOURCE_EFF_DATE"]).ToShortDateString()
                            strBuilder.Append("<td class='midcolora' align='Left' >" + ConvertDBDateToCulture(dr["POSTING_DATE"].ToString()) + "</td>"); //Convert.ToDateTime(dr["POSTING_DATE"]).ToShortDateString() 
                            strBuilder.Append("<td class='midcolora' align='Left' >" + dr["Description"]); // + "</td>");
                            if (dr["RTL_BATCH_NUMBER"] != DBNull.Value && dr["RTL_GROUP_NUMBER"] != DBNull.Value)
                            {
                                string BN = dr["RTL_BATCH_NUMBER"].ToString();
                                string GN = dr["RTL_GROUP_NUMBER"].ToString();
                                //strBuilder.Append("&nbsp;&nbsp;<img id='imgNotice_" + i.ToString() +"' title='Open Check' onClick=\"window.open('OpenCheckImage.aspx?BN="+ BN + "&GN="+ GN );
                                systemID = GetSystemId();
                                //CarrierSystemID Condition Added or Itrack issue #5943. 
                                if (systemID.ToUpper() == CarrierSystemID.ToUpper())
                                {
                                    strBuilder.Append("&nbsp;&nbsp;<img id='imgNotice_" + i.ToString() + "'title='Open Check' onClick=\"window.open('OpenCheckImage.aspx?BN=" + BN + "&GN=" + GN + "','','menubar=no ,scrollbars=yes ,height=500, width=500')\"");
                                    strBuilder.Append("\" src='../../cmsweb/Images1/Rule_ver.gif' alt='' align='AbsMiddle' style='border-width:0px;border-style:None;height:15px;CURSOR:hand' />");
                                }

                            }

                            if (dr["NOTICE_URL"] != DBNull.Value && Convert.ToString(dr["NOTICE_URL"]) != "NA")
                            {
                                strBuilder.Append("&nbsp;&nbsp;<img id='imgNotice_" + i.ToString() + "' title='Open Notice' onClick=\"window.open('");
                                //strBuilder.Append(Convert.ToString(dr["NOTICE_URL"]));
                                string strAccountInquiryInfo = dr["NOTICE_URL"].ToString();
                                int loadFile = strAccountInquiryInfo.IndexOf("/OUTPUTPDFs");
                                string Path = strAccountInquiryInfo.Substring(loadFile);
                                string[] Path_Info = Path.Split('.');
                                string EncryptedPath = ClsCommon.CreateContentViewerURL(Path, Path_Info[1].ToUpper());
                                strBuilder.Append(EncryptedPath);
                                strBuilder.Append("')\" src='../../cmsweb/Images1/Rule_ver.gif' alt='' align='AbsMiddle' style='border-width:0px;border-style:None;height:15px;CURSOR:hand' />");

                            }
                            strBuilder.Append("</td>");
                            if (dr["TOTAL_AMOUNT"] != DBNull.Value)
                            {
                                strBuilder.Append("<td  class='midcolorr' align='Right' >" + Convert.ToDouble(dr["TOTAL_AMOUNT"]).ToString("N") + " </td>");
                            }
                            else
                            {
                                strBuilder.Append("<td class='midcolorr' align='Right' >" + dr["TOTAL_AMOUNT"] + "</td>");
                            }

                            //Added on 12 OCt 2009 - Cumulative Total Due of Policy  
                            RunningBalance = Convert.ToDouble(dr["TOTAL_DUE_ON_POLICY"].ToString());

                            //if(dr["TYPE"].ToString()  != "P")
                            //{						

                            if (dr["TOTAL_PREMIUM"] != DBNull.Value)
                            {
                                strBuilder.Append("<td class='midcolorr' align='Right' >" + Convert.ToDouble(dr["TOTAL_PREMIUM"]).ToString("N") + "</td>");
                                //Commented on 12 Oct 2009
                                //RunningBalance+=Convert.ToDouble(dr["TOTAL_PREMIUM"].ToString());
                            }
                            else
                            {
                                strBuilder.Append("<td class='midcolorr' align='Right' >" + dr["TOTAL_PREMIUM"] + "</td>");
                            }
                            /*}
                            else
                            {
							
                                if(dr["TOTAL_AMOUNT"] != DBNull.Value)
                                {
                                    strBuilder.Append("<td class='midcolora' align='Right' >" + Convert.ToDouble(dr["TOTAL_AMOUNT"]).ToString("N") + "</td>");
                                    RunningBalance+=Convert.ToDouble(dr["TOTAL_AMOUNT"].ToString());
                                }
                                else
                                {
                                    strBuilder.Append("<td class='midcolora' align='Right' >" + dr["TOTAL_AMOUNT"] + "</td>");
                                }

                            }*/

                            if (dr["TYPE"].ToString() != "P")
                            {
                                if (dr["TOTAL_FEE"] != DBNull.Value)
                                {
                                    if (Convert.ToDouble(dr["TOTAL_FEE"]) != 0)
                                    {
                                        strBuilder.Append("<td class='midcolorr' align='Right' ><A href='#' onclick=\"window.open('ShowARFee.aspx?CALLED_FROM=F&INSF_FEE=" + dr["INSF_FEE"].ToString() + "&LATE_FEE=" + dr["LATE_FEE"].ToString() + "&REINS_FEE=" + dr["REINS_FEE"].ToString() + "&S_DATE=" + dr["SOURCE_TRAN_DATE"].ToString() + "&E_DATE=" + dr["SOURCE_EFF_DATE"].ToString() + "&NSF_FEE=" + dr["NSF_FEE"].ToString() + "','','height=400, width=400,left=100px;top=20px;status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no,left=20px;top=20px')\">" + Convert.ToDouble(dr["TOTAL_FEE"]).ToString("N") + "</a></td>");
                                        //strBuilder.Append("<td class='midcolora' align='Left' > </td>");
                                        //strBuilder.Append("<td class='midcolora' WIDTH='25%' align='left' ></td>");
                                    }
                                    else
                                    {
                                        strBuilder.Append("<td class='midcolorr' align='Left' > </td>");
                                        //	strBuilder.Append("<td class='midcolora' WIDTH='25%' align='left' ></td>");
                                    }
                                }
                                else
                                {
                                    strBuilder.Append("<td class='midcolorr' align='Left' > </td>");
                                    //strBuilder.Append("<td class='midcolora' WIDTH='25%' align='left' ></td>");
                                }
                                if (dr["ADJUSTED"] != DBNull.Value && dr["ADJUSTED"].ToString().Trim() != "0")
                                {
                                    string Type = dr["TYPE"].ToString();
                                    //strBuilder.Append("<td class='midcolora' align='Left' > </td>");
                                    //strBuilder.Append("<td class='midcolora'  WIDTH='25%' colspan='4' align='Right'>"  + dr["ADJUSTED"] + "</td>");
                                    strBuilder.Append("<td class='midcolorr'  WIDTH='25%' colspan='4' align='Right'><A href='#' onclick=\"window.open('ShowARFee.aspx?CALLED_FROM=" + Type + "&OP_ID=" + dr["OPEN_ITEM_ID"].ToString() + "&POL_ID=" + dr["POL_ID"].ToString() + "','','height=400, width=400,left=100px;top=20px;status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no,left=20px;top=20px')\">"
                                    + dr["ADJUSTED"] + "</a></td>");
                                    //	strBuilder.Append("<td class='midcolora' align='Left' > </td>");
                                }
                                else
                                {
                                    strBuilder.Append("<td class='midcolorr' align='Left' colspan='4' > </td>");
                                }

                            }
                            else
                            {
                                strBuilder.Append("<td class='midcolorr' align='Left' > </td>");
                                strBuilder.Append("<td class='midcolorr' WIDTH='25%' colspan='4' align='Left'></td>");

                            }

                            strBuilder.Append("</tr>");


                        }
                        strBuilder.Append("<tr>");
                        strBuilder.Append("<td colspan='4' class='midcolora' align='Right'><span class='labelfont'>" + objResourceMgr.GetString("CapTotalDue") + "</span></td>");
                        strBuilder.Append("<td class='midcolora'></td>");
                        strBuilder.Append("<td class='midcolorr' align='Right' ><span class='labelfont'>" + RunningBalance.ToString("N") + "</span></td>");
                        strBuilder.Append("<td class='midcolorr' COLSPAN='5' WIDTH='25%' align='Right' ></td>");



                        strBuilder.Append("</tr>");

                        //					tdArReport.InnerHtml =strBuilder.ToString();
                    }
                    else
                    {
                        //strBuilder.Append("<table>");
                        strBuilder.Append("<tr>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='6'>No Record Found.</td>");
                        strBuilder.Append("</tr>");
                        //					tdArReport.InnerHtml = strBuilder.ToString();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            // Added Mohit Agarwal 27-Oct-2006
            DataTable add1 = new DataTable();

            add1 = ds.Tables[2];
            strBuilder.Append("<tr height='20'>");
            strBuilder.Append("<td align='Right' ><b>&nbsp;</b></td>");
            strBuilder.Append("</tr>");

            if (add1 != null && add1.Rows.Count != 0)
            {
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='2' width='20%'><b>" + objResourceMgr.GetString("CapInsuredMailingAddress") + " </b></td>");
                //	strBuilder.Append("</tr>");

                //	strBuilder.Append("<tr height='20'>");
                //	strBuilder.Append("<td class='midcolora' align='Right' colspan='2'>&nbsp;</td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='8' width='80%'>" + dt.Rows[0]["CUSTOMER_NAME"] + "</td>");
                strBuilder.Append("</tr>");
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Right' colspan='2'><b>&nbsp;</b></td>");
                if (add1.Rows[0]["CUSTOMER_ADDRESS2"].ToString() != "" && add1.Rows[0]["CUSTOMER_ADDRESS2"] != System.DBNull.Value)
                    strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + add1.Rows[0]["CUSTOMER_ADDRESS1"] + ", " + add1.Rows[0]["CUSTOMER_ADDRESS2"] + "</td>");
                else
                    strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + add1.Rows[0]["CUSTOMER_ADDRESS1"] + "</td>");
                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Right' colspan='2'><b>&nbsp;</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='1'>" + add1.Rows[0]["CUSTOMER_CITY"] + "</td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='7'>" + add1.Rows[0]["CUSTOMER_STATE"] + " " + add1.Rows[0]["CUSTOMER_ZIP"] + "</td>");
                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
                strBuilder.Append("</tr>");
            }

            DataTable add2 = new DataTable();

            add2 = ds.Tables[3];

            int cover = 0;

            if (add2 != null && add2.Rows.Count != 0 && (add2.Rows[0]["LOB_ID"].ToString() == "1" || add2.Rows[0]["LOB_ID"].ToString() == "6"))
            {
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='2'><b>" + add2.Rows[0]["LOC_TYPE"] + "</b></td>");
                //strBuilder.Append("</tr>");


                //strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>&nbsp;</td>");
                //	strBuilder.Append("</tr>");
                //	strBuilder.Append("<tr height='20'>");
                if (add2.Rows[0]["LOC_ADD1"].ToString().Trim() == "")
                    strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + add2.Rows[0]["LOC_ADD2"] + "</td>");
                else
                {
                    if (add2.Rows[0]["LOC_ADD2"].ToString().Trim() != "")
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + add2.Rows[0]["LOC_ADD1"] + ", " + add2.Rows[0]["LOC_ADD2"] + "</td>");
                    else
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + add2.Rows[0]["LOC_ADD1"] + "</td>");

                }

                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Right' colspan='2'><b>&nbsp;</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='1'>" + add2.Rows[0]["LOC_CITY"] + "</td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='7'>" + add2.Rows[0]["LOC_STATE"] + " " + add2.Rows[0]["LOC_ZIP"] + "</td>");
                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
                strBuilder.Append("</tr>");
                cover = 1;
            }

            DataTable cov = ds.Tables[4];
            int intCov = 0;
            string strCov = "";


            if (cov != null && cov.Rows.Count != 0 && cover == 1)
            {
                cover = 0;
                // Coverage 'C' will only be shown for Policy Type : HO-4 & HO-6
                if (strPolicyType == HomeProductType.HO6_UNIT || strPolicyType == HomeProductType.HO4_TENANT)
                {
                    if (cov.Rows[0]["COV_DESC"].ToString().Trim() != "")
                    {
                        cover = 1;
                        //Convert to Currency : 
                        intCov = int.Parse(cov.Rows[0]["LIMITC"].ToString());
                        strCov = String.Format("{0:c}", intCov);
                        strBuilder.Append("<tr height='20'>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='2'><b>" + cov.Rows[0]["COV_DESC"] + "</b></td>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + strCov + "</td>");
                        strBuilder.Append("</tr>");
                    }
                }
                else // Coverage 'A' will only be shown for Policy Types other than : HO-4 & HO-6
                {
                    if (cov.Rows[0]["COV_DESA"].ToString().Trim() != "")
                    {
                        cover = 1;
                        //Convert to Currency : 
                        intCov = int.Parse(cov.Rows[0]["LIMITA"].ToString());
                        strCov = String.Format("{0:c}", intCov);
                        strBuilder.Append("<tr height='20'>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='2'><b>" + cov.Rows[0]["COV_DESA"] + "</b></td>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + strCov + "</td>");
                        strBuilder.Append("</tr>");
                    }
                }

                if (cov.Rows[0]["DEDUC_DESC"].ToString().Trim() != "")
                {
                    cover = 1;
                    //Convert to Currency : 
                    intCov = int.Parse(cov.Rows[0]["DEDUCTIBLE"].ToString());
                    strCov = String.Format("{0:c}", intCov);
                    strBuilder.Append("<tr height='20'>");
                    strBuilder.Append("<td class='midcolora' align='Left' colspan='2'><b>" + cov.Rows[0]["DEDUC_DESC"] + "</b></td>");
                    strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + strCov + "</td>");
                    strBuilder.Append("</tr>");
                }

                if (cover == 1)
                {
                    strBuilder.Append("<tr height='20'>");
                    strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
                    strBuilder.Append("</tr>");
                }
            }

            DataTable bill_desc = ds.Tables[5];

            if (bill_desc != null && bill_desc.Rows.Count != 0)
            {
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td  colspan='2' class='midcolora' align='Left'><b>" + objResourceMgr.GetString("CapBilledTo") + " </b></td>");
                //	strBuilder.Append("</tr>");

                //	strBuilder.Append("<tr height='20'>");
                //	strBuilder.Append("<td class='midcolora' align='Left' colspan='2'>&nbsp;</td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>" + bill_desc.Rows[0]["LOOKUP_VALUE_DESC"] + "</td>");
                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
                strBuilder.Append("</tr>");
            }

            DataTable add3 = new DataTable();

            add3 = ds.Tables[6];

            if (add3 != null && add3.Rows.Count != 0 && add3.Rows[0]["HOLDER_TYPE"].ToString() != "")
            {
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='2'><b>" + add3.Rows[0]["HOLDER_TYPE"] + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='2'><b>Holder Name</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='8'><b>Holder Address</b></td>");
                //strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>"+add3.Rows[0]["HOLDER_NAME"]+"</td>");
                strBuilder.Append("</tr>");
                try
                {
                    foreach (DataRow dr in add3.Rows)
                    {
                        strBuilder.Append("<tr height='20'>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='2'><b>&nbsp;</b></td>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='2'>" + dr["HOLDER_NAME"] + "</td>");
                        if (dr["HOLDER_ADD1"] != System.DBNull.Value && dr["HOLDER_ADD1"].ToString() != "" && dr["HOLDER_ADD2"] != System.DBNull.Value && dr["HOLDER_ADD2"].ToString() != "")
                            strBuilder.Append("<td class='midcolora' align='Left' colspan='6'>" + dr["HOLDER_ADD1"] + ", " + dr["HOLDER_ADD2"] + "</td>");
                        else if (dr["HOLDER_ADD1"] != System.DBNull.Value && dr["HOLDER_ADD1"].ToString() != "")
                            strBuilder.Append("<td class='midcolora' align='Left' colspan='6'>" + dr["HOLDER_ADD1"] + "</td>");
                        else if (dr["HOLDER_ADD2"] != System.DBNull.Value && dr["HOLDER_ADD2"].ToString() != "")
                            strBuilder.Append("<td class='midcolora' align='Left' colspan='6'>" + dr["HOLDER_ADD2"] + "</td>");
                        strBuilder.Append("</tr>");

                        strBuilder.Append("<tr height='20'>");
                        strBuilder.Append("<td class='midcolora' align='Right' colspan='4'><b>&nbsp;</b></td>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='1'>" + dr["HOLDER_CITY"] + "</td>");
                        //LOAN_REF_NUMBER Added By Raghav For Itrack Issue #4973
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='5'>" + dr["HOLDER_STATE"] + " " + dr["HOLDER_ZIP"] + "</td>");
                        strBuilder.Append("<tr height='20'>");
                        strBuilder.Append("<td class='midcolora' align='Right' colspan='4'><b>&nbsp;</b></td>");
                        strBuilder.Append("<td class='midcolora' align='LEFT' colspan='6'>" + dr["LOAN_REF_NUMBER"] + "</td>");
                        strBuilder.Append("</tr>");
                        strBuilder.Append("</tr>");
                    }


                    //				strBuilder.Append("<tr height='20'>");
                    //				strBuilder.Append("<td class='midcolora' align='Right' colspan='2'><b>&nbsp;</b></td>");
                    //				strBuilder.Append("<td class='midcolora' align='Left' colspan='1'>"+add3.Rows[0]["HOLDER_CITY"]+"</td>");
                    //				strBuilder.Append("<td class='midcolora' align='Left' colspan='7'>"+add3.Rows[0]["HOLDER_STATE"]+" "+add3.Rows[0]["HOLDER_ZIP"]+"</td>");
                    //				strBuilder.Append("</tr>");

                    strBuilder.Append("<tr height='20'>");
                    strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
                    strBuilder.Append("</tr>");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //Commented By Ravindra (March 05 2007) 
            //Why to have to datasets for Additional interest can fetch in one recordset only
            /*DataTable add4 = new DataTable();

            add4 = ds.Tables[7];

            if(add4 != null && add4.Rows.Count != 0 && add4.Rows[0]["HOLDER_TYPE"].ToString() != "")
            {
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Right' colspan='10'><b>"+add4.Rows[0]["HOLDER_TYPE"]+"</b></td>");
                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='2'>&nbsp;</td>");
                if(add4.Rows[0]["HOLDER_ADD1"].ToString().Trim() == "")
                    strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>"+add4.Rows[0]["HOLDER_ADD2"]+"</td>");
                else
                    strBuilder.Append("<td class='midcolora' align='Left' colspan='8'>"+add4.Rows[0]["HOLDER_ADD1"]+", "+add4.Rows[0]["HOLDER_ADD2"]+"</td>");

                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Right' colspan='2'><b>&nbsp;</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='1'>"+add4.Rows[0]["HOLDER_CITY"]+"</td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='7'>"+add4.Rows[0]["HOLDER_STATE"]+" "+add4.Rows[0]["HOLDER_ZIP"]+"</td>");
                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td align='Right' width='15%'><b>&nbsp;</b></td>");
                strBuilder.Append("</tr>");
            }*/


            /*DataTable assocPol = new DataTable();
            assocPol=ds.Tables[8]; */
           // if (!(Request.Params["CALLEDFOR"] != null && Request.Params["CALLEDFOR"].ToString() == "WORKFLOW"))
           // {
                DataTable assocPol = assocPol = ds.Tables[7];


                // Added Mohit Agarwal 26-Oct-2006
                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Left' width='15%'><b>" + objResourceMgr.GetString("CapOtherPoliciesof") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='9'><b>" + dt.Rows[0]["CUSTOMER_NAME"] + "</b></td>");

                strBuilder.Append("</tr>");

                strBuilder.Append("<tr height='20'>");
                strBuilder.Append("<td class='midcolora' align='Left' width='9%'><b>" + objResourceMgr.GetString("CapPolicyNumber") + "</b></td>");

                strBuilder.Append("<td class='midcolora' align='Left' width='9%'><b>" + objResourceMgr.GetString("CapPolicyTerm") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='10%'><b>" + objResourceMgr.GetString("CapInsuredName") + "</b></td>");

                strBuilder.Append("<td class='midcolora' align='Left' width='20%'><b>" + objResourceMgr.GetString("CapAgency") + "</b></td>");
                //strBuilder.Append("<td class='midcolora' align='Left' width='10%'><b>Agency Code</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='10%'><b>" + objResourceMgr.GetString("CapPolicyStatus") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' width='14%'><b>" + objResourceMgr.GetString("CapProduct") + "</b></td>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='4'><b>" + objResourceMgr.GetString("CapPaymentPlan") + "</b></td>");
                //strBuilder.Append("<td class='midcolora' align='left' COLSPAN='4' WIDTH='25%'><b>Status</b></td>");

                strBuilder.Append("</tr>");



                //
                //			if (assocPol == null)
                //			{
                //				lblMessage.Visible =true;
                //				lblMessage.Text = "No associated policies for the selected policy.";
                //				tdArReport.InnerHtml = strBuilder.ToString();
                //				return;
                //			}
                //			else
                //			{
                //				if (assocPol.Rows.Count == 0)
                //				{
                //					lblMessage.Visible =true;
                //					lblMessage.Text =  "No associated policies for the selected policy.";
                //					tdArReport.InnerHtml = strBuilder.ToString();
                //					return;
                //				}
                //			}			

                try
                {
                    if (assocPol != null)
                    {
                        foreach (DataRow dr in assocPol.Rows)
                        {
                            if (dt.Rows[0]["POLICY_NO"].ToString() != dr["POLICY_NO"].ToString())
                            {
                                strBuilder.Append("<td class='midcolora' align='Left'><a style='CURSOR: hand;color: #0000FF' onclick='ShowAssoc(\"" + dr["POLICY_NO"] + "\")'>" + dr["POLICY_NO"] + " </a></td>");
                                //strBuilder.Append("<td class='midcolora' align='Left'><img style='CURSOR: hand' src='../../cmsweb/Images1/blank.gif' alt="+ dr["POLICY_NO"] + " onclick='ShowAssoc(\"" + dr["POLICY_NO"] + "\")'></img></td>");
                                strBuilder.Append("<td class='midcolora' align='Left'>" + dr["APP_TERM"] + "</td>");

                                strBuilder.Append("<td class='midcolora' align='Left'>" + dr["CUSTOMER_NAME"] + "</td>");
                                strBuilder.Append("<td class='midcolora' align='Left'>" + dr["AGEN_NAME"] + "</td>");
                                //strBuilder.Append("<td class='midcolora' align='Left'>"+dr["AGENCY_ID"]+"</td>");
                                strBuilder.Append("<td class='midcolora' align='Left'>" + dr["POLICY_STATUS"] + "</td>");
                                strBuilder.Append("<td class='midcolora' align='Left'>" + dr["LOB_DESC"] + "</td>");
                                strBuilder.Append("<td class='midcolora' align='Left' colspan='4'>" + dr["PMT_PLAN"] + "</td>");
                                //strBuilder.Append("<td class='midcolora' COLSPAN='4' WIDTH='25%' align='left'>"+dt.Rows[0]["STATUS"]+"</td>");
                                strBuilder.Append("</tr>");
                            }
                        }

                        strBuilder.Append("</tr>");

                        tdArReport.InnerHtml = strBuilder.ToString();
                        calledfrom = Request.QueryString["CalledFrom"];

                    }
                    else
                    {
                        //strBuilder.Append("<table>");
                        strBuilder.Append("<tr>");
                        strBuilder.Append("<td class='midcolora' align='Left' colspan='6'>No Record Found.</td>");
                        strBuilder.Append("</tr>");
                        tdArReport.InnerHtml = strBuilder.ToString();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ds.Dispose();
                }
            //}
           // else
               // tdArReport.InnerHtml = strBuilder.ToString();
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        private void SetCookieValue()
        {
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                if (Request.QueryString["CalledFrom"].Trim().ToUpper() == CALLED_FROM_CUST_MANAGER.ToUpper())
                {
                    Response.Cookies["LastVisitedTab"].Value = "6";//Changed from 7 for Policy Page Implementation
                    Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0, 0));
                }
            }
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Request.Params["CalledFor"] != null && Request.Params["CalledFor"] != "")
            {
                if (Request.Params["CalledFor"].ToString().Trim() == "WORKFLOW")
                    ShowAccountReport();
            }
            else
                ShowReport(0, 0);
        }
        private void ShowReport(int CUSTOMER_ID, int POLICY_ID)
        {
            int intPolicyId = 0;
            int intCustomerID = 0;
            string[] arrLookUp;
            if (hidPOLICYINFO.Value != "" && hidPOLICYINFO.Value != "0")
            {
                arrLookUp = (hidPOLICYINFO.Value).Split('^');
                intPolicyId = int.Parse(arrLookUp[0].ToString());
                intCustomerID = int.Parse(arrLookUp[1].ToString());
                strPolicyNo = arrLookUp[2].ToString();
            }
            if (callingCustomerId != "" && callingCustomerId == intCustomerID.ToString())
            {
                CreateTable(intCustomerID, intPolicyId);
            }
            //When Called from Top Menu:Case No Customer 
            else if (callingCustomerId == "")
            {
                CreateTable(intCustomerID, intPolicyId);
            }
            else if (CUSTOMER_ID != 0 && POLICY_ID != 0)
            {
                CreateTable(CUSTOMER_ID, POLICY_ID);
            }
            else
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("<tr>");
                strBuilder.Append("<td class='midcolora' align='Left' colspan='6'>Policy not of current customer.</td>");
                strBuilder.Append("</tr>");
                tdArReport.InnerHtml = strBuilder.ToString();

                lblMessage.Visible = true;
                lblMessage.Text = "Report of current customer only possible";
            }
        }
        //Added By Lalit.Nov 22,2010
        private void GetpolicyDetails()
        {
            ClsGeneralInformation objGenralInfo = new ClsGeneralInformation();
            DataSet ds = new DataSet();
            ds = objGenralInfo.GetPolicyDetails(int.Parse(hidCUSTOMER_ID.Value), 0, 0, int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["POLICY_NUMBER"] != null && ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString() != "")
                    hidPOLICY_NO.Value = ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
                else
                    hidPOLICY_NO.Value = ds.Tables[0].Rows[0]["APP_NUMBER"].ToString();
            }
        }
        private void ShowAccountReport()
        {
            this.GetpolicyDetails();
            txtPolicyNo.Text = hidPOLICY_NO.Value;
            if (hidCUSTOMER_ID.Value != "" && hidPOLICY_ID.Value != "")
                ShowReport(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
        }
    }
}
