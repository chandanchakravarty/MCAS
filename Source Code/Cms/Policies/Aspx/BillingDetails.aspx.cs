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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Configuration;
using System.Collections.Generic;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Policy;
using System.Resources;
using Cms.BusinessLayer.Blapplication;
using System.Linq;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
namespace Cms.Policies.Aspx
{
    public partial class BillingDetails : Cms.Policies.policiesbase
    {

        String LobId = string.Empty;
        static DataSet dsPOL_PLAN;
        string TRAN_TYP;
        string strPlanPayMode = "", strModeDownPay = "";
        Cms.BusinessLayer.Blapplication.ClsBillingDetails objclsBLInstallments = new Cms.BusinessLayer.Blapplication.ClsBillingDetails();
        ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.BillingDetails", System.Reflection.Assembly.GetExecutingAssembly());
        protected List<ClsBillingDetailsInfo> lstBillingInfo = new List<ClsBillingDetailsInfo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(BillingDetails));
            base.ScreenId = "224_11";
            //Security XML
            btnGet_Premium.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnGet_Premium.PermissionString = gstrSecurityXML;
            btnGenrateInstallment.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnGenrateInstallment.PermissionString = gstrSecurityXML;
            btnAdjust.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAdjust.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            numberFormatInfo.NumberDecimalDigits = 2;
            #region Not Is Postback
            if (Request.QueryString["POLICY_LOB"] != null && Request.QueryString["POLICY_LOB"].ToString() != "")
                LobId = Request.QueryString["POLICY_LOB"].ToString();
            else
                LobId = GetLOBID();
            if (!IsPostBack)
            {

                if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                    hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
                else
                    hidCUSTOMER_ID.Value = GetCustomerID();

                if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                    hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
                else
                    hidPOLICY_ID.Value = GetPolicyID();
                //hidPOLICY_ID.Value = GetPolicyID();
                if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                    hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
                else
                    hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

                //hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

                hidPOLICY_STATUS.Value = GetPolicyStatus();
                hidCALLED_FROM.Value = Request.QueryString["CALLEDFROM"].ToString();



                // hidPRINTPARAMETER_FROM.Value = objClsProductPdfXml.BoletoORpremNoticeGenerate();
                getPolicy_SelectedPlan();
                GetPolicy_CurrentStatus(); //get current policy process status     
                FillComboBox();
                SeterrorMessages();
                BindGrid();

                //added by kuldeep
                ClsBillingDetailsInfo objBillingInfo = new ClsBillingDetailsInfo();
                DataSet ds = new DataSet();
                ds = objBillingInfo.GetPolicyPremComm(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));

                txtTOTAL_PREMIUM.Text = ds.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString();
                hidTOTAL_PREMIUM.Value = ds.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString();

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["POLICY_LEVEL_COMISSION"] != DBNull.Value)
                {
                    double comm_perc = Convert.ToDouble(ds.Tables[0].Rows[0]["POLICY_LEVEL_COMISSION"]);
                    hidComm_percentage.Value = comm_perc.ToString();
                    double comm_amt = Convert.ToDouble(txtTOTAL_PREMIUM.Text) * comm_perc / 100;
                    txtGross_Commission.Text = comm_amt.ToString();
                    hidGross_Commission.Value = comm_amt.ToString();
                }




                //CheckDownPaymentMode(); Comment by Kuldeep
                //GetEFTInfo(); Comment by Kuldeep
                //CheckPolicyCreatedMode();Comment by Kuldeep


            }  //Assign first time
            else
            {
                //BindGrid();
            }
            #endregion

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            if (grdBILLING_INFO.Rows.Count > 0)
            {
                ArrayList arlobjBillingInfo = new ArrayList();
                int retval = 0;
                try
                {
                    arlobjBillingInfo = GetUpdatedInstallmentDetails();
                    if (arlobjBillingInfo.Count > 0)
                    {
                        retval = objclsBLInstallments.SaveInstallments(arlobjBillingInfo);
                        if (retval > 0)
                        {
                            BindGrid();
                            // CheckDownPaymentMode();Comment By Kuldeep
                            // GetEFTInfo(); Comment By Kuldeep
                            base.OpenEndorsementDetails();
                            hidUPDATEDROWS.Value = "";
                            hidArrObj.Value = "";
                            foreach (GridViewRow rw in grdBILLING_INFO.Rows)
                            { HtmlInputHidden hidUPDATEFlag = (HtmlInputHidden)rw.FindControl("hidUPDATEFlag"); hidUPDATEFlag.Value = ""; }

                            if (lblMessage.Text != ClsMessages.GetMessage(ScreenId, "25"))
                                lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                            else
                            {
                                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "52");//+ ClsMessages.GetMessage(ScreenId, "25");
                            }
                        }
                        else if (retval == -1)
                        {
                            lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "16");
                        }
                        else if (retval == -4)
                        {
                            lblcustommsg.Text = ClsMessages.GetMessage(ScreenId, "24");
                            BindGrid();
                        }
                    }
                    else
                    {
                        //lblMessage.Text = lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        BindGrid();
                        base.OpenEndorsementDetails();
                        hidUPDATEDROWS.Value = "";
                        hidArrObj.Value = "";
                        foreach (GridViewRow rw in grdBILLING_INFO.Rows)
                        { HtmlInputHidden hidUPDATEFlag = (HtmlInputHidden)rw.FindControl("hidUPDATEFlag"); hidUPDATEFlag.Value = ""; }

                        //commented by Lalit if no change done on gridview record .the information not goes to save 
                        //but message shows information saved successfully.no transaction maintain.i-track # 693
                        //lblMessage.Text = ClsMessages.GetMessage(ScreenId, "51");
                        if (lblMessage.Text != ClsMessages.GetMessage(ScreenId, "25"))
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "2") + ": " + ex.Message;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    // ExceptionManager.Publish(ex);
                }
            }

        } //btn Submitt click

        private ArrayList GetUpdatedInstallmentDetails()
        {
            ArrayList arlobjBillingInfo = new ArrayList();

            lstBillingInfo = (List<ClsBillingDetailsInfo>)GetPageModelObjects();
            int flag = 0;
            int totalUpdatedRows = 0;
            if (hidUPDATEDROWS.Value != "")
            {
                totalUpdatedRows = Convert.ToInt32(hidUPDATEDROWS.Value);
            }
            foreach (GridViewRow rw in grdBILLING_INFO.Rows)
            {
                HtmlInputHidden hidUPDATEFlag = (HtmlInputHidden)rw.FindControl("hidUPDATEFlag");
                Label lblINSTALLMENT_NO = (Label)rw.FindControl("lblINSTALLMENT_NO");
                Label lblTRAN_TYPE = (Label)rw.FindControl("lblTRAN_TYPE");
                Label lblPOLICY_VERSION_ID = (Label)rw.FindControl("lblPOLICY_VERSION_ID");

                TextBox txtINSTALLMENT_EFFECTIVE_DATE = (TextBox)rw.FindControl("txtINSTALLMENT_EFFECTIVE_DATE");
                TextBox txtPREMIUM = (TextBox)rw.FindControl("txtPREMIUM");
                TextBox txtINTEREST_AMOUNT = (TextBox)rw.FindControl("txtINTEREST_AMOUNT");
                TextBox txtFEE = (TextBox)rw.FindControl("txtFEE");
                TextBox txtTAXES = (TextBox)rw.FindControl("txtTAXES");
                TextBox txtTOTAL = (TextBox)rw.FindControl("txtTOTAL");
                HtmlInputHidden hidTOTAL = (HtmlInputHidden)rw.FindControl("hidTOTAL");

                //TextBox txtTRAN_PREMIUM = (TextBox)rw.FindControl("txtTRAN_PREMIUM_AMOUNT");
                //TextBox txtTRAN_INTEREST_AMOUNT = (TextBox)rw.FindControl("txtTRAN_INTEREST_AMOUNT");
                //TextBox txtTRAN_FEE = (TextBox)rw.FindControl("txtTRAN_FEE");
                //TextBox txtTRAN_TAXES = (TextBox)rw.FindControl("txtTRAN_TAXES");
                //TextBox txtTRAN_TOTAL = (TextBox)rw.FindControl("txtTRAN_TOTAL");
                HtmlInputHidden hidTRAN_TOTAL = (HtmlInputHidden)rw.FindControl("hidTRAN_TOTAL");



                if (hidUPDATEFlag.Value.Equals("1"))
                {

                    //ClsPolicyBillingInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { reinsurer = ob, REINID = id }).Where(ob => ob.reinsurer.REINSURANCE_ID.CurrentValue == int.Parse(lb.Text.Trim()) && ob.reinsurer.COMPANY_ID.CurrentValue == CompanyId).Select(ob => ob.reinsurer).First();
                    //Find model object from list using lamda expression
                    ClsBillingDetailsInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.INSTALLMENT_NO.CurrentValue == int.Parse(lblINSTALLMENT_NO.Text.Trim()) && ob.BillingInfo.POLICY_VERSION_ID.CurrentValue == int.Parse(lblPOLICY_VERSION_ID.Text)).Select(ob => ob.BillingInfo).First();
                    objBillingInfo.ACTION = enumAction.Update;
                    //objBillingInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    //objBillingInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    //objBillingInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);

                    if (lblINSTALLMENT_NO.Text != "")
                        if (objBillingInfo.POLICY_VERSION_ID.CurrentValue == int.Parse(lblPOLICY_VERSION_ID.Text) && objBillingInfo.INSTALLMENT_NO.CurrentValue == int.Parse(lblINSTALLMENT_NO.Text.Trim()))
                        {
                            objBillingInfo.INSTALLMENT_NO.CurrentValue = int.Parse(lblINSTALLMENT_NO.Text);
                            objBillingInfo.INSTALLMENT_NO.IsChanged = true;

                            if (txtPREMIUM.Text != "")
                                objBillingInfo.INSTALLMENT_AMOUNT.CurrentValue = decimal.Parse(txtPREMIUM.Text, numberFormatInfo);
                            else
                                objBillingInfo.INSTALLMENT_AMOUNT.CurrentValue = 0;

                            if (txtINTEREST_AMOUNT.Text != "")
                                objBillingInfo.INTEREST_AMOUNT.CurrentValue = decimal.Parse(txtINTEREST_AMOUNT.Text, numberFormatInfo);
                            else
                                objBillingInfo.INTEREST_AMOUNT.CurrentValue = 0;

                            if (txtFEE.Text != "")
                                objBillingInfo.FEE.CurrentValue = decimal.Parse(txtFEE.Text, numberFormatInfo);
                            else
                                objBillingInfo.FEE.CurrentValue = 0;

                            if (txtTAXES.Text != "")
                                objBillingInfo.TAXES.CurrentValue = decimal.Parse(txtTAXES.Text, numberFormatInfo);
                            else
                                objBillingInfo.TAXES.CurrentValue = 0;
                            if (hidTOTAL.Value != "")
                                objBillingInfo.TOTAL.CurrentValue = decimal.Parse(hidTOTAL.Value, numberFormatInfo);
                            else
                                objBillingInfo.TOTAL.CurrentValue = 0;

                            if (txtPREMIUM.Text != "")
                                objBillingInfo.TRAN_PREMIUM_AMOUNT.CurrentValue = decimal.Parse(txtPREMIUM.Text, numberFormatInfo);
                            else
                                objBillingInfo.TRAN_PREMIUM_AMOUNT.CurrentValue = 0;

                            if (txtINTEREST_AMOUNT.Text != "")
                                objBillingInfo.TRAN_INTEREST_AMOUNT.CurrentValue = decimal.Parse(txtINTEREST_AMOUNT.Text, numberFormatInfo);
                            else
                                objBillingInfo.TRAN_INTEREST_AMOUNT.CurrentValue = 0;

                            if (txtFEE.Text != "")
                                objBillingInfo.TRAN_FEE.CurrentValue = decimal.Parse(txtFEE.Text, numberFormatInfo);
                            else
                                objBillingInfo.TRAN_FEE.CurrentValue = 0;

                            if (txtTAXES.Text != "")
                                objBillingInfo.TRAN_TAXES.CurrentValue = decimal.Parse(txtTAXES.Text, numberFormatInfo);
                            else
                                objBillingInfo.TRAN_TAXES.CurrentValue = 0;

                            if (hidTOTAL_PREMIUM.Value != "")
                                objBillingInfo.TOTAL_PREMIUM.CurrentValue = decimal.Parse(hidTOTAL_PREMIUM.Value, numberFormatInfo);
                            else
                                objBillingInfo.TOTAL_PREMIUM.CurrentValue = 0;


                            if (hidTRAN_TOTAL.Value != "")
                                objBillingInfo.TRAN_TOTAL.CurrentValue = decimal.Parse(hidTRAN_TOTAL.Value, numberFormatInfo);
                            else
                                objBillingInfo.TRAN_TOTAL.CurrentValue = 0;


                            if (txtINSTALLMENT_EFFECTIVE_DATE.Text != "")
                            {
                                objBillingInfo.INSTALLMENT_EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtINSTALLMENT_EFFECTIVE_DATE.Text);
                            }
                            objBillingInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                            objBillingInfo.LAST_UPDATED_DATETIME.CurrentValue = DateTime.Now;

                            flag = flag + 1;

                            //Update Policy Total Amounts In ACT_POLICY_INSTALL_PALN_DATA Table                   
                            //Update Total amount if gridview row has updated.
                            //create Model Object set Policy Total Mount values ie:Total Policy Premium, Total Policy Taxes etc.
                            //set value in last updated installment object 

                            //Start
                            if (flag == totalUpdatedRows)  //Check for object add in array list at last when all updated row will added in arraylist   
                            {
                                //objBillingInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                                // objBillingInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                                //objBillingInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);

                                if (txtTOTAL_PREMIUM.Text != "")
                                {
                                    objBillingInfo.TOTAL_PREMIUM.CurrentValue = decimal.Parse(txtTOTAL_PREMIUM.Text, numberFormatInfo);
                                }
                                else
                                    objBillingInfo.TOTAL_PREMIUM.CurrentValue = GetEbixDecimalDefaultValue();

                                if (hidTOTAL_INTEREST_AMOUNT.Value != "")
                                    objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue = decimal.Parse(hidTOTAL_INTEREST_AMOUNT.Value, numberFormatInfo);
                                else
                                    objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue = GetEbixDecimalDefaultValue();

                                if (hidTOTAL_FEES.Value != "")
                                    objBillingInfo.TOTAL_FEES.CurrentValue = decimal.Parse(hidTOTAL_FEES.Value, numberFormatInfo);
                                else
                                    objBillingInfo.TOTAL_FEES.CurrentValue = GetEbixDecimalDefaultValue();

                                if (hidTOTAL_TAXES.Value != "")
                                    objBillingInfo.TOTAL_TAXES.CurrentValue = decimal.Parse(hidTOTAL_TAXES.Value, numberFormatInfo);
                                else
                                    objBillingInfo.TOTAL_TAXES.CurrentValue = GetEbixDecimalDefaultValue();


                                if (hidTOTAL_AMOUNT.Value != "")
                                    objBillingInfo.TOTAL_AMOUNT.CurrentValue = decimal.Parse(hidTOTAL_AMOUNT.Value, numberFormatInfo);//double.Parse(txtTOTAL_AMOUNT.Text);
                                else
                                    objBillingInfo.TOTAL_AMOUNT.CurrentValue = GetEbixDecimalDefaultValue();

                                if (cmbBILLING_PLAN.SelectedValue != "")
                                    objBillingInfo.PLAN_ID.CurrentValue = int.Parse(cmbBILLING_PLAN.SelectedValue);

                                if (txtTOTAL_CHANGE_INFORCE_PRM.Text != "")
                                    objBillingInfo.TOTAL_CHANGE_INFORCE_PRM.CurrentValue = decimal.Parse(txtTOTAL_CHANGE_INFORCE_PRM.Text, numberFormatInfo);

                                objBillingInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                objBillingInfo.LAST_UPDATED_DATETIME.CurrentValue = DateTime.Now;

                                if (hidSTATE_FEES.Value != "")
                                {
                                    objBillingInfo.TOTAL_STATE_FEES.CurrentValue = double.Parse(hidSTATE_FEES.Value, numberFormatInfo);
                                }
                                else
                                    objBillingInfo.TOTAL_STATE_FEES.CurrentValue = 0;

                                if (objBillingInfo.TRAN_TYPE.CurrentValue == "END")
                                {
                                    if (txtTOTAL_TRAN_INTEREST_AMOUNT.Text != "")
                                        objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue = decimal.Parse(txtTOTAL_TRAN_PREMIUM.Text, numberFormatInfo);
                                    else
                                        objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue = 0;

                                    if (txtTOTAL_TRAN_INTEREST_AMOUNT.Text != "")
                                        objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue = decimal.Parse(txtTOTAL_TRAN_INTEREST_AMOUNT.Text, numberFormatInfo);
                                    else
                                        objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue = 0;

                                    if (txtTOTAL_TRAN_FEES.Text != "")
                                        objBillingInfo.TOTAL_TRAN_FEES.CurrentValue = decimal.Parse(txtTOTAL_TRAN_FEES.Text, numberFormatInfo);
                                    else
                                        objBillingInfo.TOTAL_TRAN_FEES.CurrentValue = 0;

                                    if (txtTOTAL_TRAN_TAXES.Text != "")
                                        objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue = decimal.Parse(txtTOTAL_TRAN_TAXES.Text, numberFormatInfo);
                                    else
                                        objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue = 0;

                                    if (hidTOTAL_TRAN_AMOUNT.Value != "")
                                        objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue = decimal.Parse(hidTOTAL_TRAN_AMOUNT.Value, numberFormatInfo);
                                    else
                                        objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue = 0;


                                    if (hidSTATE_TRAN_FEES.Value != "")
                                    {
                                        objBillingInfo.TOTAL_TRAN_STATE_FEES.CurrentValue = double.Parse(hidSTATE_TRAN_FEES.Value, numberFormatInfo);
                                    }
                                    else
                                        objBillingInfo.TOTAL_TRAN_STATE_FEES.CurrentValue = 0;


                                }
                                else
                                {
                                    objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue = objBillingInfo.TOTAL_PREMIUM.CurrentValue;
                                    objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue = objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue;
                                    objBillingInfo.TOTAL_TRAN_FEES.CurrentValue = objBillingInfo.TOTAL_FEES.CurrentValue;
                                    objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue = objBillingInfo.TOTAL_TAXES.CurrentValue;
                                    objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue = objBillingInfo.TOTAL_AMOUNT.CurrentValue;

                                    /////////

                                    objBillingInfo.TOTAL_TRAN_PREMIUM.IsChanged = false;
                                    objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.IsChanged = false;
                                    objBillingInfo.TOTAL_TRAN_FEES.IsChanged = false;
                                    objBillingInfo.TOTAL_TRAN_TAXES.IsChanged = false;
                                    objBillingInfo.TOTAL_TRAN_AMOUNT.IsChanged = false;
                                }


                                arlobjBillingInfo.Add(objBillingInfo);  //Add  model Object for total value field in array list
                            }
                            else
                            {
                                arlobjBillingInfo.Add(objBillingInfo);
                            }
                            //End
                        }
                }
            }
            return arlobjBillingInfo;
        } //update billing installments data
        private void SeterrorMessages()
        {
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            this.revTOTAL_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
            this.revTOTAL_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
            //this.revTOTAL_FEES.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpDoubleZeroToPositive;//aRegExpCurrencyformat;
            //this.revTOTAL_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
            this.revTOTAL_INTEREST_AMOUNT.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpDoubleZeroToPositive; //aRegExpCurrencyformat;
            this.revTOTAL_INTEREST_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
            this.revTOTAL_PREMIUM.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpCurrencyformat;
            this.revTOTAL_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
            this.revTOTAL_TAXES.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpDoubleZeroToPositive;//aRegExpCurrencyformat;
            this.revTOTAL_TAXES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
            //this.rfvTOTAL_FEES.ErrorMessage = ClsMessages.GetMessage(ScreenId, "6");// "Please Enter Total Fee.";
            this.rfvTOTAL_INTEREST_AMOUNT.ErrorMessage = ClsMessages.GetMessage(ScreenId, "5"); //"Please Enter Total interest amount.";
            this.rfvTOTAL_PREMIUM.ErrorMessage = ClsMessages.GetMessage(ScreenId, "4");//"Please Enter Total premium amount.";
            this.rfvTOTAL_TAXES.ErrorMessage = ClsMessages.GetMessage(ScreenId, "7");//"Please Enter Total taxes amount.";
            rfvBILLING_PLAN.ErrorMessage = ClsMessages.GetMessage(ScreenId, "14"); //lease select Billing Plan
            //this.csvTOTAL_FEES.ErrorMessage = ClsMessages.GetMessage(ScreenId, "32");//"Please Enter Total taxes amount.";


            //for Card information move from previous billing info
            /*Comment by Kuldeep
            //revFEDERAL_ID.ValidationExpression = aRegExpFederalID;//aRegExpDoublePositiveNonZero;
            //revFEDERAL_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("434");

            //rfvTRANSIT_ROUTING_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("927");
            //rfvDFI_ACC_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("928");
            //rngEFT_TENTATIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("929");
            //revDFI_ACC_NO.ValidationExpression = aRegExpAlphaNumWithDash;
            //revDFI_ACC_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("903");
            //rfvEFT_TENTATIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("930");
            //revCARD_CVV_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            //revCARD_CVV_NUMBER.ValidationExpression = aRegExpInteger;
            //revCARD_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            //revCARD_NO.ValidationExpression = aRegExpInteger;
            //csvCARD_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("955");
            //csvCARD_CVV_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("959");


            //revTOTAL_TRAN_PREMIUM.ValidationExpression = aRegExpCurrencyformat;
            //revTOTAL_TRAN_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            //revTOTAL_TRAN_INTEREST_AMOUNT.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpDoublePositiveWithZero;//aRegExpDoubleZeroToPositive;//RegExpDoublePositiveWithZero
            //revTOTAL_TRAN_INTEREST_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");


            //revTOTAL_TRAN_FEES.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpDoubleZeroToPositive;
            //revTOTAL_TRAN_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            //revTOTAL_TRAN_TAXES.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpDoubleZeroToPositive;
            //revTOTAL_TRAN_TAXES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");


            //rngCARD_DATE_VALID_TO.MinimumValue = "0";
            //rngCARD_DATE_VALID_TO.MaximumValue = "99";


            //cmpCARD_DATE_VALID_TO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "34");
            //csvCARD_DATE_VALID_TO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "35");
            //rfvCARD_NO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "37");
            //rfvCARD_DATE_VALID_TO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "39");
            //rfvCARD_TYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "40");
            //rfvCARD_CVV_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "41");
            //csvCARD_CVV_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "42");
            ////Customer Info
            //rfvCUSTOMER_FIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("57");
            //revCUSTOMER_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
            //revCUSTOMER_FIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            //rfvCUSTOMER_LAST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("58");
            //revCUSTOMER_FIRST_NAME.ValidationExpression = aRegExpAlphaNum;
            //revCUSTOMER_ZIP.ValidationExpression = aRegExpZipBrazil;
            //revCUSTOMER_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            //Added by Pradeep itrack 1512/TFS#240
             till here*/
            csvTOTAL_PREMIUM.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
            //Added till here 
        }  //Set Validation Error Messages 

        protected void btnGet_Premium_Click(object sender, EventArgs e)
        { }
        override protected void OnInit(EventArgs e)
        {

            InitializeComponent();
            base.OnInit(e);
        }


        private void InitializeComponent()
        {

            this.btnGenrateInstallment.Click += new EventHandler(btnGenrateInstallment_Click);
        }
        protected void btnGenrateInstallment_Click(object sender, EventArgs e)
        {

           
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            lblMessage.Text = "";
            int retval = 0;
            try
            {

                int BillingPlanId = 0;
                if (hidPOLICY_STATUS.Value.Trim().ToUpper() != "UENDRS" && hidPOLICY_STATUS.Value.Trim().ToUpper() != "MENDORSE" || hidPROCESS_ID.Value == "14" || hidPROCESS_ID.Value == "3")
                {
                    cmbPRM_DIST_TYPE.SelectedIndex = cmbPRM_DIST_TYPE.Items.IndexOf(cmbPRM_DIST_TYPE.Items.FindByValue("14855"));
                    if (cmbPRM_DIST_TYPE.SelectedValue == "14855" || cmbPRM_DIST_TYPE.SelectedValue == "")//if use wants to genrate endorsement installment according to selected plan
                    {
                        cmbPRM_DIST_TYPE.SelectedIndex = cmbPRM_DIST_TYPE.Items.IndexOf(cmbPRM_DIST_TYPE.Items.FindByValue("14855"));
                        BillingPlanId = int.Parse(cmbBILLING_PLAN.SelectedValue);
                    }
                    else if (hidSELECTED_PLAN_ID.Value != "")
                        BillingPlanId = int.Parse(hidSELECTED_PLAN_ID.Value);
                }
                else
                    if (hidSELECTED_PLAN_ID.Value != "")
                        BillingPlanId = int.Parse(hidSELECTED_PLAN_ID.Value);

                retval = GenrateInstall(BillingPlanId);

                if (retval >= 0)
                {
                    if (retval > 0)
                    {
                        //base.OpenEndorsementDetails();
                    }
                    BindGrid();

                    //CheckDownPaymentMode(); Comment By Kuldeep
                    //GetEFTInfo(); Comment by Kuldeep
                    // itrack 693
                    if (double.Parse(hidPOL_COV_PRE.Value.Trim(), numberFormatInfo) > 0 && decimal.Parse(txtTOTAL_TRAN_PREMIUM.Text, numberFormatInfo) == 0 && hidPOLICY_STATUS.Value.Trim().ToUpper() == "UENDRS")
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "54");

                    else if (double.Parse(hidPOL_COV_PRE.Value.Trim(), numberFormatInfo) > 0 && double.Parse(txtTOTAL_PREMIUM.Text.Trim(), numberFormatInfo) == 0)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "54");
                    }
                    else if (lblMessage.Text == ClsMessages.GetMessage(ScreenId, "25"))
                    {// changed by praveer for itrack no 693/TFS # 435
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "25");
                    }
                    else
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "53");// changed by praveer for itrack no 693/TFS # 435
                    //lblMessage.Text = "";
                }
                else if (retval == -2)
                {
                    // Apllication is under agency bill 
                    BindBlankGrid();
                    lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "23");
                }
                else if (retval == -4)
                {
                    // Apllication is under agency bill 
                    BindBlankGrid();
                    lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "24");
                }
                else if (retval == -5)
                {
                    lblMessage.Text = ClsMessages.GetMessage(ScreenId, "27");
                    BindGrid();
                }
                else if (retval == -6)
                {
                    lblMessage.Text = ClsMessages.GetMessage(ScreenId, "28");
                    BindGrid();
                }
                else if (retval == -10)//changed by praveer for itrack no 1761
                {
                    BindGrid();
                    lblMessage.Text = ClsMessages.GetMessage(ScreenId, "55");

                }
                else
                {
                    // ----------IF ANY ERROR ACCORD  
                    BindGrid();
                    lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "21");// +": " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                BindGrid();
                lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "19") + ": " + ex.Message;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                //  ExceptionManager.Publish(ex);
            }

        }

        private int GenrateInstall(int PLAN_ID)
        {
            int retval = 0;
            ClsBillingDetailsInfo objBillingInfo;
            ///if Policy Status is endorsment in progress then transaction premium can not be blank and zero
            if (hidPOLICY_STATUS.Value.Trim().ToUpper() == "UENDRS" || hidPOLICY_STATUS.Value.Trim().ToUpper() == "MENDORSE" || hidPROCESS_ID.Value == "14" || hidPROCESS_ID.Value == "3")
            {
                //if (txtTOTAL_TRAN_PREMIUM.Text != "" && double.Parse(txtTOTAL_TRAN_PREMIUM.Text) != 0)
                //{

                objBillingInfo = this.GetformValues(PLAN_ID);
                retval = objclsBLInstallments.GenrateInstallment(objBillingInfo);
                //}
                ////else { retval = 0;  }

            }
            else
            {
                //if Policy Status is appliction or new business in progress then total policy premium are required 
                if (this.hidTOTAL_PREMIUM.Value != "")//&& double.Parse(this.hidTOTAL_PREMIUM.Value, numberFormatInfo) != 0)
                {
                    objBillingInfo = this.GetformValues(PLAN_ID);
                    retval = objclsBLInstallments.GenrateInstallment(objBillingInfo);
                }
                else
                    return retval = 0;
            }

            return retval;
        }  //genrate installments 
        private ClsBillingDetailsInfo GetformValues(int PLAN_ID)
        {


            ClsBillingDetailsInfo objBillingInfo = new ClsBillingDetailsInfo();
            objBillingInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
            objBillingInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
            objBillingInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
            objBillingInfo.TOTAL_PREMIUM.CurrentValue = this.hidTOTAL_PREMIUM.Value == "" ? 0 : decimal.Parse(this.hidTOTAL_PREMIUM.Value, numberFormatInfo);
            objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue = this.hidTOTAL_INTEREST_AMOUNT.Value == "" ? 0 : decimal.Parse(this.hidTOTAL_INTEREST_AMOUNT.Value, numberFormatInfo);
            objBillingInfo.TOTAL_FEES.CurrentValue = hidTOTAL_FEES.Value == "" ? 0 : decimal.Parse(hidTOTAL_FEES.Value, numberFormatInfo);
            objBillingInfo.TOTAL_TAXES.CurrentValue = hidTOTAL_TAXES.Value == "" ? 0 : decimal.Parse(hidTOTAL_TAXES.Value, numberFormatInfo);
            objBillingInfo.TOTAL_STATE_FEES.CurrentValue = hidSTATE_FEES.Value == "" ? 0 : double.Parse(hidSTATE_FEES.Value, numberFormatInfo);
            //Added by Kuldeep for TFS 3408 on 8 - feb -2012

            objBillingInfo.TOTAL_BEFORE_GST.CurrentValue = hidTotal_Before_GST.Value == "" ? 0 : double.Parse(hidTotal_Before_GST.Value, numberFormatInfo);
            objBillingInfo.TOTAL_AFTER_GST.CurrentValue = hidTotal_After_GST.Value == "" ? 0 : double.Parse(hidTotal_After_GST.Value, numberFormatInfo);
            objBillingInfo.GROSS_COMMISSION.CurrentValue = hidGross_Commission.Value == "" ? 0 : double.Parse(hidGross_Commission.Value, numberFormatInfo);
            objBillingInfo.GST_ON_COMMISSION.CurrentValue = hidGST_on_Commission.Value == "" ? 0 : double.Parse(hidGST_on_Commission.Value, numberFormatInfo);
            objBillingInfo.TOTAL_COMM_AFTER_GST.CurrentValue = hidTot_Comm_After_GST.Value == "" ? 0 : double.Parse(hidTot_Comm_After_GST.Value, numberFormatInfo);


            objBillingInfo.TOTAL_AMOUNT.CurrentValue = hidTOTAL_AMOUNT.Value == "" ? 0 : decimal.Parse(hidTOTAL_AMOUNT.Value, numberFormatInfo);

            if (hidPOLICY_STATUS.Value.Trim().ToUpper() == "UENDRS" || hidPROCESS_ID.Value == "3")
            { //policy endorsment in progress then transaction amount will suply to genrate installment 
                txtTOTAL_CHANGE_INFORCE_PRM.ReadOnly = false;
                txtTOTAL_INFO_PRM.ReadOnly = false;
                objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue = txtTOTAL_TRAN_PREMIUM.Text == "" ? 0 : decimal.Parse(txtTOTAL_TRAN_PREMIUM.Text, numberFormatInfo);
                objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue = txtTOTAL_TRAN_INTEREST_AMOUNT.Text == "" ? 0 : decimal.Parse(txtTOTAL_TRAN_INTEREST_AMOUNT.Text, numberFormatInfo);
                objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue = txtTOTAL_TRAN_TAXES.Text == "" ? 0 : decimal.Parse(txtTOTAL_TRAN_TAXES.Text, numberFormatInfo);
                objBillingInfo.TOTAL_TRAN_FEES.CurrentValue = txtTOTAL_TRAN_FEES.Text == "" ? 0 : decimal.Parse(txtTOTAL_TRAN_FEES.Text, numberFormatInfo);
                objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue = hidTOTAL_TRAN_AMOUNT.Value == "" ? 0 : decimal.Parse(hidTOTAL_TRAN_AMOUNT.Value, numberFormatInfo);
                objBillingInfo.TOTAL_CHANGE_INFORCE_PRM.CurrentValue = hidTOTAL_CHANGE_INFORCE_PRM.Value == "" ? 0 : decimal.Parse(hidTOTAL_CHANGE_INFORCE_PRM.Value, numberFormatInfo); //double.Parse(txtTOTAL_CHANGE_INFORCE_PRM.Text,numberFormatInfo);
                objBillingInfo.TOTAL_INFO_PRM.CurrentValue = hidTOTAL_INFO_PRM.Value == "" ? 0 : decimal.Parse(hidTOTAL_INFO_PRM.Value, numberFormatInfo);
                if (txtTOTAL_TRAN_PREMIUM.Text != "" && decimal.Parse(txtTOTAL_TRAN_PREMIUM.Text, numberFormatInfo) != 0)
                {
                    if (hidTOTAL_CHANGE_INFORCE_PRM.Value == "" || decimal.Parse(hidTOTAL_CHANGE_INFORCE_PRM.Value, numberFormatInfo) == 0)
                    {
                        objBillingInfo.TOTAL_CHANGE_INFORCE_PRM.CurrentValue = decimal.Parse(txtTOTAL_TRAN_PREMIUM.Text, numberFormatInfo);//hidTOTAL_CHANGE_INFORCE_PRM.Value == "" ? 0 : double.Parse(hidTOTAL_CHANGE_INFORCE_PRM.Value, numberFormatInfo); //double.Parse(txtTOTAL_CHANGE_INFORCE_PRM.Text,numberFormatInfo);
                        objBillingInfo.TOTAL_INFO_PRM.CurrentValue = decimal.Parse(hidTOTAL_INFO_PRM.Value, numberFormatInfo) + decimal.Parse(txtTOTAL_TRAN_PREMIUM.Text, numberFormatInfo);//hidTOTAL_INFO_PRM.Value == "" ? 0 : double.Parse(hidTOTAL_INFO_PRM.Value, numberFormatInfo);
                    }
                }


                if (hidSTATE_FEES.Value != "" && hidSTATE_TRAN_FEES.Value != "")
                {
                    objBillingInfo.TOTAL_TRAN_STATE_FEES.CurrentValue = double.Parse(hidSTATE_FEES.Value, numberFormatInfo) - double.Parse(hidSTATE_TRAN_FEES.Value, numberFormatInfo);
                }
                else
                    objBillingInfo.TOTAL_TRAN_STATE_FEES.CurrentValue = 0;

                if (cmbPRM_DIST_TYPE.SelectedValue != "")
                    objBillingInfo.PRM_DIST_TYPE.CurrentValue = int.Parse(cmbPRM_DIST_TYPE.SelectedValue);
                else
                    objBillingInfo.PRM_DIST_TYPE.CurrentValue = 14672; //if endorsment installemnt distribution type is not selected it default distribute amount in all installments

            }
            else
            {
                //otherwise if policy under new business in progress then transaction amount are same.
                objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue = 0;
                objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue = 0;
                objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue = 0;
                objBillingInfo.TOTAL_TRAN_FEES.CurrentValue = 0;
                objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue = 0;
                objBillingInfo.TOTAL_CHANGE_INFORCE_PRM.CurrentValue = 0;
                objBillingInfo.TOTAL_INFO_PRM.CurrentValue = 0;
            }

            objBillingInfo.PLAN_ID.CurrentValue = PLAN_ID;
            objBillingInfo.CREATED_BY.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
            objBillingInfo.CREATED_DATETIME.CurrentValue = DateTime.Now;
            objBillingInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
            return objBillingInfo;
        }//getform values for supply the values from UI to database stored procedure

        protected void btnAdjust_Click(object sender, EventArgs e)
        {
        }
        private void BindGrid()
        {
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            //DataSet BillingInfo = new DataSet();
            //incosistency occure to fetch date msg in row databound method
            //so if function throw any exception the msg can b blank
            try
            {
                hidDATE_MSG.Value = ClsMessages.FetchGeneralMessage("22");
            }
            catch
            {
                hidDATE_MSG.Value = "";
            }

            try
            {
                DataSet ds = new DataSet();
                DataSet dsInstallments = new DataSet();
                ClsBillingDetailsInfo objBillingInfo = new ClsBillingDetailsInfo();
                lstBillingInfo = ClsBillingDetails.GetInstallmentDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                //ds = objBillingInfo.GetPolicyInstallments(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                ds = objclsBLInstallments.Getpolicy_NBSAmount(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), hidPROCESS_ID.Value.Trim());
                //added by kuldeep for tfs3408
                dsInstallments = objBillingInfo.GetInstallmentsDetailsNew(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                if (lstBillingInfo != null && lstBillingInfo.Count > 0)
                {

                    GetNBS_AmountDetails(ds);
                    //BindTotals(lstBillingInfo); Comented by kuldeep as transaction fields are not required for Singapore
                    //grdBILLING_INFO.DataSource = lstBillingInfo;
                    //grdBILLING_INFO.DataBind();

                    grdBILLING_DETAILS.DataSource = dsInstallments.Tables[0];
                    grdBILLING_DETAILS.DataBind();

                    hidROWCOUNT.Value = grdBILLING_INFO.Rows.Count.ToString();
                    // btnGenrateInstallment.Enabled = false;
                    //btnAdjust.Enabled = true;
                }
                else
                {
                    BindBlankGrid();
                    hidROWCOUNT.Value = "";
                    if (hidPOLICY_STATUS.Value.Trim().ToUpper() == "UENDRS" || hidPOLICY_STATUS.Value.Trim().ToUpper() == "MENDORSE" || hidPROCESS_ID.Value == "14" || hidPROCESS_ID.Value == "3")
                    {
                        GetNBS_AmountDetails(ds);
                    }
                    else
                    {
                        GetPolicyCoveragesPremium();
                        //btnGenrateInstallment.Enabled = true;
                    }
                }
                if (hidCALLED_FROM.Value == "QAPP") //Boleto is not for Quick App
                {
                    grdBILLING_INFO.Columns[11].Visible = false;
                }
                //if (hidPRINTPARAMETER_FROM.Value == "PREM_NOTICE") //Boleto is not for Quick App
                //{
                //    grdBILLING_INFO.Columns[9].Visible = false;
                //}

                SetPageModelObjects(lstBillingInfo); //set page model object into session
                if (hidPROCESS_ID.Value != "12" && hidPROCESS_ID.Value != "2")//No need to compaire premium if cancellation is committ
                    // ComparePremiumAmount(); Comment By Kuldeep

                    if (hidPROCESS_ID.Value == "" || hidPROCESS_ID.Value == "0" || hidPROCESS_ID.Value == "24")
                    {
                        //grdBILLING_INFO.Columns[9].Visible = false;
                        // btnBoletoReprint.Visible = false; Comment by Kuldeep
                        // btnView_All_Boleto.Visible = false; Comment by Kuldeep
                    }

                cmbBILLING_PLAN.SelectedValue = hidSELECTED_PLAN_ID.Value.Trim();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "20") + ": " + ex.Message;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                //  ExceptionManager.Publish(ex);
            }


        }
        /// <summary>
        /// Display Policy premium and other surcharges for billing if 
        /// </summary>
        /// <param name="lstBillingInfo"></param>
        private void BindTotals(List<ClsBillingDetailsInfo> lstBillingInfo)
        {
            int EndorsmentCount = 0;
            numberFormatInfo.NumberDecimalDigits = 2;
            ClsBillingDetailsInfo objmodelInfo = new ClsBillingDetailsInfo();
            int Instl = objmodelInfo.Check_InstallGenstatus(int.Parse(hidPOLICY_ID.Value), int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            hidInstallGen.Value = Instl.ToString();
            if (lstBillingInfo.Count > 0)
            {
                try
                {
                    EndorsmentCount = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.TRAN_TYPE.CurrentValue.ToUpper().Contains("END")).Select(ob => ob.BillingInfo).Count();
                }
                catch
                {

                }
                if (EndorsmentCount > 0)
                {
                    trTran.Style.Add("display", "inline");
                    trINFOR.Style.Add("display", "inline");
                    //trEND_PRM_DIST_OPTION.Style.Add("display", "inline");
                    txtTOTAL_PREMIUM.ReadOnly = true;
                    txtTOTAL_TAXES.ReadOnly = true;
                    txtTotal_After_GST.ReadOnly = true;
                    txtTOTAL_INTEREST_AMOUNT.ReadOnly = true;
                    //txtTOTAL_PREMIUM.Attributes.Add("CssClass", "inputcurrency midcolora");

                }
            }

            if (Instl != 0)
            {
                ClsBillingDetailsInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.POLICY_VERSION_ID.CurrentValue == int.Parse(hidPOLICY_VERSION_ID.Value)).Select(ob => ob.BillingInfo).First();
                if (objBillingInfo != null)
                {

                    txtTOTAL_PREMIUM.Text = Convert.ToDecimal(objBillingInfo.TOTAL_PREMIUM.CurrentValue).ToString("N", numberFormatInfo);
                    hidTOTAL_PREMIUM.Value = Convert.ToDecimal(objBillingInfo.TOTAL_PREMIUM.CurrentValue).ToString("N", numberFormatInfo);

                    this.txtTOTAL_INTEREST_AMOUNT.Text = Convert.ToDecimal(objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue).ToString("N", numberFormatInfo);
                    hidTOTAL_INTEREST_AMOUNT.Value = Convert.ToDecimal(objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue).ToString("N", numberFormatInfo);

                    //this.txtTOTAL_FEES.Text = Convert.ToDecimal(objBillingInfo.TOTAL_FEES.CurrentValue).ToString("N", numberFormatInfo);
                    //hidTOTAL_FEES.Value = Convert.ToDecimal(objBillingInfo.TOTAL_FEES.CurrentValue).ToString("N", numberFormatInfo);

                    this.txtTOTAL_TAXES.Text = Convert.ToDecimal(objBillingInfo.TOTAL_TAXES.CurrentValue).ToString("N", numberFormatInfo);
                    hidTOTAL_TAXES.Value = Convert.ToDecimal(objBillingInfo.TOTAL_TAXES.CurrentValue).ToString("N", numberFormatInfo);

                    this.txtTOTAL_AMOUNT.Text = Convert.ToDecimal(objBillingInfo.TOTAL_AMOUNT.CurrentValue).ToString("N", numberFormatInfo);
                    hidTOTAL_AMOUNT.Value = Convert.ToDecimal(objBillingInfo.TOTAL_AMOUNT.CurrentValue).ToString("N", numberFormatInfo);


                    //Added by Kuldeep for TFS 3408 on 9-feb-2012

                    this.txtTotal_Before_GST.Text = Convert.ToDecimal(objBillingInfo.TOTAL_BEFORE_GST.CurrentValue).ToString("N", numberFormatInfo);
                    hidTotal_Before_GST.Value = Convert.ToDecimal(objBillingInfo.TOTAL_BEFORE_GST.CurrentValue).ToString("N", numberFormatInfo);

                    this.txtTotal_After_GST.Text = Convert.ToDecimal(objBillingInfo.TOTAL_AFTER_GST.CurrentValue).ToString("N", numberFormatInfo);
                    hidTotal_After_GST.Value = Convert.ToDecimal(objBillingInfo.TOTAL_AFTER_GST.CurrentValue).ToString("N", numberFormatInfo);


                    this.txtGross_Commission.Text = Convert.ToDecimal(objBillingInfo.GROSS_COMMISSION.CurrentValue).ToString("N", numberFormatInfo);
                    hidGross_Commission.Value = Convert.ToDecimal(objBillingInfo.GROSS_COMMISSION.CurrentValue).ToString("N", numberFormatInfo);


                    this.txtGST_on_Commission.Text = Convert.ToDecimal(objBillingInfo.GST_ON_COMMISSION.CurrentValue).ToString("N", numberFormatInfo);
                    hidGST_on_Commission.Value = Convert.ToDecimal(objBillingInfo.GST_ON_COMMISSION.CurrentValue).ToString("N", numberFormatInfo);


                    this.txtTot_Comm_After_GST.Text = Convert.ToDecimal(objBillingInfo.TOTAL_COMM_AFTER_GST.CurrentValue).ToString("N", numberFormatInfo);
                    hidTot_Comm_After_GST.Value = Convert.ToDecimal(objBillingInfo.TOTAL_COMM_AFTER_GST.CurrentValue).ToString("N", numberFormatInfo);

                    //till Here
                    cmbBILLING_PLAN.SelectedValue = Convert.ToString(objBillingInfo.PLAN_ID.CurrentValue);

                    strPlanPayMode = objBillingInfo.MODE_OF_PAYMENT.CurrentValue;
                    strModeDownPay = objBillingInfo.MODE_OF_DOWN_PAYMENT.CurrentValue;


                    if (hidPOLICY_STATUS.Value.Trim().ToUpper() == "UENDRS"
                        || hidPOLICY_STATUS.Value.Trim().ToUpper() == "MENDORSE"
                        || hidPROCESS_ID.Value == "14" || hidPROCESS_ID.Value == "3"
                        || hidPROCESS_ID.Value == "12"
                        || hidPROCESS_ID.Value == "37"
                        )
                    {
                        int PLAN_ID = int.Parse(objBillingInfo.PLAN_ID.CurrentValue.ToString());
                        cmbBILLING_PLAN.SelectedIndex = cmbBILLING_PLAN.Items.IndexOf(cmbBILLING_PLAN.Items.FindByValue(PLAN_ID.ToString()));
                        cmbBILLING_PLAN.Enabled = false;
                        hidSELECTED_PLAN_ID.Value = cmbBILLING_PLAN.SelectedValue;
                        this.txtTOTAL_TRAN_PREMIUM.Text = objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue.ToString("N", numberFormatInfo);
                        this.txtTOTAL_TRAN_INTEREST_AMOUNT.Text = Convert.ToDecimal(objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue).ToString("N", numberFormatInfo);
                        this.txtTOTAL_TRAN_FEES.Text = Convert.ToDecimal(objBillingInfo.TOTAL_TRAN_FEES.CurrentValue).ToString("N", numberFormatInfo);
                        this.txtTOTAL_TRAN_TAXES.Text = Convert.ToDecimal(objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue).ToString("N", numberFormatInfo);
                        this.txtTOTAL_TRAN_AMOUNT.Text = Convert.ToDecimal(objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue).ToString("N", numberFormatInfo);
                        this.hidTOTAL_TRAN_AMOUNT.Value = Convert.ToDecimal(objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue).ToString("N", numberFormatInfo); ;
                        this.cmbPRM_DIST_TYPE.SelectedValue = objBillingInfo.PRM_DIST_TYPE.CurrentValue.ToString();
                        //if (txtTOTAL_CHANGE_INFORCE_PRM.ToString() != objBillingInfo.TOTAL_CHANGE_INFORCE_PRM.CurrentValue.ToString())
                        //this.txtTOTAL_CHANGE_INFORCE_PRM.Text = Convert.ToDecimal(objBillingInfo.TOTAL_CHANGE_INFORCE_PRM.CurrentValue).ToString("N", numberFormatInfo);
                        //this.txtTOTAL_INFO_PRM.Text = Convert.ToDecimal(objBillingInfo.TOTAL_INFO_PRM.CurrentValue).ToString("N", numberFormatInfo);
                        hidTOTAL_CHANGE_INFORCE_PRM.Value = txtTOTAL_CHANGE_INFORCE_PRM.Text;
                        hidTOTAL_INFO_PRM.Value = txtTOTAL_INFO_PRM.Text;
                        //this.hidSTATE_FEES.Value = objBillingInfo.TOTAL_STATE_FEES.CurrentValue.ToString();
                        //hidSTATE_TRAN_FEES
                    }
                    else
                    {

                        int PLAN_ID = int.Parse(objBillingInfo.PLAN_ID.CurrentValue.ToString());
                        cmbBILLING_PLAN.SelectedIndex = cmbBILLING_PLAN.Items.IndexOf(cmbBILLING_PLAN.Items.FindByValue(PLAN_ID.ToString()));
                        cmbBILLING_PLAN.Enabled = true;
                        // hidSELECTED_PLAN_ID.Value = cmbBILLING_PLAN.SelectedValue;

                    }
                }
            }
            else
            {

                string MaxPolicyVersion = lstBillingInfo.Max(o => o.POLICY_VERSION_ID.CurrentValue).ToString();
                ClsBillingDetailsInfo obDistType = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.POLICY_VERSION_ID.CurrentValue == int.Parse(MaxPolicyVersion)).Select(ob => ob.BillingInfo).First();//.Where(ob => ob.BillingInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(MaxPolicyVersion));

                cmbPRM_DIST_TYPE.SelectedIndex = cmbPRM_DIST_TYPE.Items.IndexOf(cmbPRM_DIST_TYPE.Items.FindByValue(obDistType.PRM_DIST_TYPE.ToString()));
            }

        }   //Dispalay Policy Premium Amount at UI on behalf Installment is genrated
        private void GetPolicyCoveragesPremium()
        {
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            //double Total = 0;
            string SelectedVal = "";
            if (dsPOL_PLAN != null && dsPOL_PLAN.Tables[0].Rows.Count > 0)
            {

                // txtTOTAL_PREMIUM.Text = dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_COVERAGE_PREMIUM"].ToString() == "" ? "0" : Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_COVERAGE_PREMIUM"]).ToString("N", numberFormatInfo);
                //hidTOTAL_PREMIUM.Value = txtTOTAL_PREMIUM.Text;
                SelectedVal = dsPOL_PLAN.Tables[0].Rows[0]["INSTALL_PLAN_ID"].ToString();


                if (dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_STATE_FEES"] != null && dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_STATE_FEES"].ToString() != "")
                {
                    txtTOTAL_FEES.Text = Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_STATE_FEES"].ToString()).ToString("N", numberFormatInfo);
                    hidTOTAL_FEES.Value = Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_STATE_FEES"].ToString()).ToString("N", numberFormatInfo);
                    hidSTATE_FEES.Value = Convert.ToDouble(dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_STATE_FEES"].ToString()).ToString("N", numberFormatInfo);
                    //Comment By Kuldeep //this.csvTOTAL_FEES.ErrorMessage = ClsMessages.GetMessage(ScreenId, "32") + " " + hidSTATE_FEES.Value.ToString();  //"Please Enter Total taxes amount.";

                }
                else
                {
                    txtTOTAL_FEES.Text = "0";
                    hidSTATE_FEES.Value = "0";
                }
                txtTOTAL_TAXES.Text = "0";
                txtTOTAL_INTEREST_AMOUNT.Text = "0";

                //Total = this.PolicyPrmSum(); //double.Parse(txtTOTAL_PREMIUM.Text, numberFormatInfo) + double.Parse(txtTOTAL_FEES.Text, numberFormatInfo) + double.Parse(txtTOTAL_TAXES.Text, numberFormatInfo) + double.Parse(txtTOTAL_INTEREST_AMOUNT.Text, numberFormatInfo);

                txtTOTAL_AMOUNT.Text = this.PolicyPrmSum().Trim();//Total.ToString();
                cmbBILLING_PLAN.SelectedIndex = cmbBILLING_PLAN.Items.IndexOf(cmbBILLING_PLAN.Items.FindByValue(SelectedVal.Trim()));
                // = cmbBILLING_PLAN.SelectedValue;
                if (SelectedVal == "0" || SelectedVal == "")
                {
                    lblMessage.Text = ClsMessages.GetMessage(ScreenId, "23");
                }
            }
        }//get policy Coverages premium 

        private string PolicyPrmSum()
        {
            //Changed by Kuldeep as per Singapore Requirement
            double sum;
            string strSum;
            txtTOTAL_PREMIUM.Text = txtTOTAL_PREMIUM.Text == "" ? "0" : txtTOTAL_PREMIUM.Text;
            txtTOTAL_INTEREST_AMOUNT.Text = txtTOTAL_INTEREST_AMOUNT.Text == "" ? "0" : txtTOTAL_INTEREST_AMOUNT.Text;

            txtTOTAL_TAXES.Text = txtTOTAL_TAXES.Text == "" ? "0" : txtTOTAL_TAXES.Text;
            //txtTOTAL_FEES.Text = txtTOTAL_FEES.Text == "" ? "0" : txtTOTAL_FEES.Text;
            txtTot_Comm_After_GST.Text = txtTot_Comm_After_GST.Text == "" ? "0" : txtTot_Comm_After_GST.Text;

            sum = double.Parse(txtTOTAL_PREMIUM.Text, numberFormatInfo) + double.Parse(txtTOTAL_TAXES.Text, numberFormatInfo) + double.Parse(txtTOTAL_INTEREST_AMOUNT.Text, numberFormatInfo) - double.Parse(txtTot_Comm_After_GST.Text, numberFormatInfo);
            if (sum > 9999999999999)
                strSum = txtTOTAL_AMOUNT.Text;
            else
                strSum = sum.ToString() == "" ? "0" : Convert.ToDecimal(sum).ToString("N", numberFormatInfo);
            return strSum;
        }  //Policy Amount Sum 
        private void GetNBS_AmountDetails(DataSet ds)
        {

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                numberFormatInfo.NumberDecimalDigits = 2;
                this.txtTOTAL_PREMIUM.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_PREMIUM"]).ToString("N", numberFormatInfo);
                this.hidTOTAL_PREMIUM.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_PREMIUM"]).ToString("N", numberFormatInfo);
                this.txtTOTAL_INTEREST_AMOUNT.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_INTEREST_AMOUNT"]).ToString("N", numberFormatInfo);
                this.hidTOTAL_INTEREST_AMOUNT.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_INTEREST_AMOUNT"]).ToString("N", numberFormatInfo);
                // this.txtTOTAL_FEES.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_FEES"]).ToString("N", numberFormatInfo);
                //this.hidTOTAL_FEES.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_FEES"]).ToString("N", numberFormatInfo);
                this.txtTOTAL_TAXES.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_TAXES"]).ToString("N", numberFormatInfo);
                this.hidTOTAL_TAXES.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_TAXES"]).ToString("N", numberFormatInfo);
                this.txtTOTAL_AMOUNT.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_AMOUNT"]).ToString("N", numberFormatInfo);
                hidNBS_PREMIUM.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_PREMIUM"]).ToString("N", numberFormatInfo);
                hidNBS_INFO_PRM.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_INFO_PRM"]).ToString("N", numberFormatInfo);

                hidNBS_FEES.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_FEES"]).ToString("N", numberFormatInfo);
                hidNBS_INTEREST.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_INTEREST_AMOUNT"]).ToString("N", numberFormatInfo);
                hidNBS_TAXES.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_TAXES"]).ToString("N", numberFormatInfo);
                hidPOL_EFF_DATE.Value = ds.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"].ToString();
                hidEXPIRY_DATE.Value = ds.Tables[0].Rows[0]["EXPIRY_DATE"].ToString();
                this.txtTOTAL_TRAN_PREMIUM.Text = Convert.ToDecimal("0").ToString("N", numberFormatInfo);
                this.txtTOTAL_TRAN_INTEREST_AMOUNT.Text = Convert.ToDecimal("0").ToString("N", numberFormatInfo);
                this.txtTOTAL_TRAN_FEES.Text = Convert.ToDecimal("0").ToString("N", numberFormatInfo);
                this.txtTOTAL_TRAN_TAXES.Text = Convert.ToDecimal("0").ToString("N", numberFormatInfo);
                this.txtTOTAL_TRAN_AMOUNT.Text = Convert.ToDecimal("0").ToString("N", numberFormatInfo);
                this.hidTOTAL_TRAN_AMOUNT.Value = Convert.ToDecimal("0").ToString("N", numberFormatInfo);
                // this.txtTOTAL_CHANGE_INFORCE_PRM.Text = Convert.ToDouble("0").ToString("N", numberFormatInfo);


                ////Added By Lalit Dec 29,2010
                //this.lblNBS_PREMIUM.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["NBS_PREMIUM"]).ToString("N", numberFormatInfo); //this.txtTOTAL_PREMIUM.Text;
                //this.lblNBS_INTR.Text =  Convert.ToDouble(ds.Tables[0].Rows[0]["NBS_INTEREST"]).ToString("N", numberFormatInfo);//this.txtTOTAL_INTEREST_AMOUNT.Text;
                //this.lblNBS_FEES.Text =  Convert.ToDouble(ds.Tables[0].Rows[0]["NBS_FEES"]).ToString("N", numberFormatInfo);//this.txtTOTAL_FEES.Text;
                //this.lblNBS_TAX.Text =  Convert.ToDouble(ds.Tables[0].Rows[0]["NBS_TAX"]).ToString("N", numberFormatInfo);//this.txtTOTAL_TAXES.Text;
                //this.lblNBS_TOTAL.Text =  Convert.ToDouble(ds.Tables[0].Rows[0]["NBS_TOTAL"]).ToString("N", numberFormatInfo);//this.txtTOTAL_AMOUNT.Text;

                // cmpINSTALLMENT_EFF_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(ds.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"]));
                // cmpINSTALLMENT_EXP_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(ds.Tables[0].Rows[0]["EXPIRY_DATE"]));

                if (hidPOL_COV_PRE.Value != "")
                {
                    //Decimal diffVal = 0;
                    Decimal Infoceprm = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_INFO_PRM"], numberFormatInfo);
                    // diffVal = Convert.ToDouble(hidPOL_COV_PRE.Value, numberFormatInfo) - Infoceprm;
                    //txtTOTAL_INFO_PRM.Text = (Infoceprm + diffVal).ToString("N", numberFormatInfo);
                    //hidTOTAL_INFO_PRM.Value = txtTOTAL_INFO_PRM.Text;


                    Decimal NbsPrm = Infoceprm;//Convert.ToDouble(hidNBS_PREMIUM.Value, numberFormatInfo);
                    Decimal changeInfo = Convert.ToDecimal(hidPOL_COV_PRE.Value, numberFormatInfo) - NbsPrm;

                    //txtTOTAL_INFO_PRM.Text = hidTOTAL_INFO_PRM.Value = (changeInfo + NbsPrm).ToString("N", numberFormatInfo);
                    //hidTOTAL_INFO_PRM.Value = txtTOTAL_INFO_PRM.Text;

                    //txtTOTAL_CHANGE_INFORCE_PRM.Text = changeInfo.ToString("N", numberFormatInfo);
                    //hidTOTAL_CHANGE_INFORCE_PRM.Value = txtTOTAL_CHANGE_INFORCE_PRM.Text;
                }
                else
                {
                    //txtTOTAL_INFO_PRM.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["TOTAL_INFO_PRM"]).ToString("N", numberFormatInfo);
                    //hidTOTAL_INFO_PRM.Value = txtTOTAL_INFO_PRM.Text;
                }
                hidSTATE_TRAN_FEES.Value = Convert.ToDouble(ds.Tables[0].Rows[0]["TOTAL_TRAN_STATE_FEES"]).ToString("N", numberFormatInfo);

                strPlanPayMode = ds.Tables[0].Rows[0]["MODE_OF_PAYMENT"].ToString();
                strModeDownPay = ds.Tables[0].Rows[0]["MODE_OF_DOWN_PAYMENT"].ToString();

            }
            else { this.txtTOTAL_PREMIUM.Text = Convert.ToDecimal("0").ToString("N", numberFormatInfo); }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                //Added By Lalit Dec 29,2010
                this.lblNBS_PREMIUM.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["NBS_PREMIUM"]).ToString("N", numberFormatInfo); //this.txtTOTAL_PREMIUM.Text;
                this.lblNBS_INTR.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["NBS_INTEREST"]).ToString("N", numberFormatInfo);//this.txtTOTAL_INTEREST_AMOUNT.Text;
                this.lblNBS_FEES.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["NBS_FEES"]).ToString("N", numberFormatInfo);//this.txtTOTAL_FEES.Text;
                this.lblNBS_TAX.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["NBS_TAX"]).ToString("N", numberFormatInfo);//this.txtTOTAL_TAXES.Text;
                this.lblNBS_TOTAL.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["NBS_TOTAL"]).ToString("N", numberFormatInfo);//this.txtTOTAL_AMOUNT.Text;

            }
        }//show NBS amount for compare and get the amount difference between NBS and endorsment amount
        private void GetPolicy_CurrentStatus()
        {





        }//show transaction amount field if policy process id in 14,3 for endorsment

        private void FillComboBox()
        {
            DataSet DsPlans = null;
            DataTable dtPlans = null;
            DsPlans = ClsInstallmentInfo.GetApplicableInstallmentPlans(int.Parse(hidPOL_TERMS.Value));
            if (DsPlans != null && DsPlans.Tables.Count > 0 && DsPlans.Tables[0].Rows.Count > 0)
            {
                try
                {
                    dtPlans = DsPlans.Tables[0].Select("PLAN_TYPE NOT IN ('MAUTO','MMANNUAL') OR PLAN_TYPE IS NULL").CopyToDataTable<DataRow>();
                }
                catch
                {
                    dtPlans.Clear();
                    dtPlans = DsPlans.Tables[0].Copy();
                }
            }
            else
            {
                dtPlans = DsPlans.Tables[0].Copy();
            }
            cmbBILLING_PLAN.Items.Clear();
            cmbBILLING_PLAN.DataSource = dtPlans;
            cmbBILLING_PLAN.DataTextField = "BILLING_PLAN";
            cmbBILLING_PLAN.DataValueField = "INSTALL_PLAN_ID";
            cmbBILLING_PLAN.DataBind();
            cmbBILLING_PLAN.Items.Insert(0, "");


            //Comment by Kuldeep
            //IList ListDistOp = ClsCommon.GetLookup("DISTOP");

            //cmbPRM_DIST_TYPE.Items.Clear();
            //cmbPRM_DIST_TYPE.DataSource = ListDistOp;
            //cmbPRM_DIST_TYPE.DataTextField = "LookupDesc";
            //cmbPRM_DIST_TYPE.DataValueField = "LookupID";
            //cmbPRM_DIST_TYPE.DataBind();
            //cmbPRM_DIST_TYPE.Items.Insert(0, "");

            //DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
            //cmbCUSTOMER_COUNTRY.DataSource = dt;
            //cmbCUSTOMER_COUNTRY.DataTextField = COUNTRY_NAME;
            //cmbCUSTOMER_COUNTRY.DataValueField = COUNTRY_ID;
            //cmbCUSTOMER_COUNTRY.DataBind();
            //cmbCUSTOMER_COUNTRY.Items.Insert(0, "");
            //cmbCUSTOMER_COUNTRY.SelectedValue = "5";

            //dt = GetDataTable(Cms.CmsWeb.ClsFetcher.State.Select("COUNTRY_ID=5"));
            //cmbCUSTOMER_STATE.DataSource = dt;
            //cmbCUSTOMER_STATE.DataTextField = STATE_NAME;
            //cmbCUSTOMER_STATE.DataValueField = STATE_ID;
            //cmbCUSTOMER_STATE.DataBind();
            //cmbCUSTOMER_STATE.Items.Insert(0, "");


            //cmbCARD_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CCTYPE");
            //cmbCARD_TYPE.DataTextField = "LookupDesc";
            //cmbCARD_TYPE.DataValueField = "LookupID";
            //cmbCARD_TYPE.DataBind();
            //cmbCARD_TYPE.Items.Insert(0, "");

        }
        private void getPolicy_SelectedPlan()
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            ClsBillingDetailsInfo objBillingInfo = new ClsBillingDetailsInfo();


            dsPOL_PLAN = objBillingInfo.GetCoveragesPremium(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(LobId));
            if (dsPOL_PLAN != null && dsPOL_PLAN.Tables.Count > 0 && dsPOL_PLAN.Tables[0].Rows.Count > 0)
            {
                hidPOL_TERMS.Value = dsPOL_PLAN.Tables[0].Rows[0]["APP_TERMS"].ToString();
                hidPOL_COV_PRE.Value = dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_COVERAGE_PREMIUM"].ToString() == "" ? "0" : Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_COVERAGE_PREMIUM"]).ToString("N", numberFormatInfo);
                hidSELECTED_PLAN_ID.Value = dsPOL_PLAN.Tables[0].Rows[0]["INSTALL_PLAN_ID"].ToString();
                hidPOLICY_CURRENCY.Value = dsPOL_PLAN.Tables[0].Rows[0]["POLICY_CURRENCY"].ToString();
                hidPOLICY_BILL_TYPE.Value = dsPOL_PLAN.Tables[0].Rows[0]["BILL_TYPE"].ToString();
                hidSTATE_FEES.Value = dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_STATE_FEES"].ToString() == "" ? "0" : Convert.ToDouble(dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_STATE_FEES"]).ToString("N", numberFormatInfo);
                hidPOL_EFFECTIVE_DATE.Value = dsPOL_PLAN.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"].ToString();
                hidPOL_EXPIRY_DATE.Value = dsPOL_PLAN.Tables[0].Rows[0]["POLICY_EXPIRATION_DATE"].ToString();
                hidCO_INSURANCE.Value = dsPOL_PLAN.Tables[0].Rows[0]["CO_INSURANCE"].ToString();
                //Added By Lalit April 26,2011,i-track # 1126
                //change inforce premium now come prom column change inforce premium in POL_PRODUCT_COVERAGE_TABLE
                if (dsPOL_PLAN.Tables[0].Rows[0]["CHANGE_INFO_PREMIUM"].ToString() != "")
                    //Comment by Kuldeep                    hidTOTAL_CHANGE_INFORCE_PRM.Value = txtTOTAL_CHANGE_INFORCE_PRM.Text = Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["CHANGE_INFO_PREMIUM"], numberFormatInfo).ToString("N", numberFormatInfo);
                    ////txtTOTAL_CHANGE_INFORCE_PRM.Text = dsPOL_PLAN.Tables[0].Rows[0]["CHANGE_INFO_PREMIUM"].ToString();

                    if (dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_INFO_PREMIUM"].ToString() != "")
                        //comment by Kuldeep                   hidTOTAL_INFO_PRM.Value = txtTOTAL_INFO_PRM.Text = Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["TOTAL_INFO_PREMIUM"], numberFormatInfo).ToString("N", numberFormatInfo);
                        // changed by praveer for itrack no 1761
                        if (dsPOL_PLAN.Tables[0].Rows[0]["IOF_PERCENTAGE"].ToString() != "")
                            hid_IOF_PERCENTAGE.Value = Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["IOF_PERCENTAGE"], numberFormatInfo).ToString("N", numberFormatInfo);



                //txtTOTAL_INFO_PRM.Text = (Infoceprm + diffVal).ToString("N", numberFormatInfo);
                //hidTOTAL_INFO_PRM.Value = txtTOTAL_INFO_PRM.Text;

            }

        }//get policy select plan for billing at page load

        private void BindBlankGrid()
        {
            // ClsMessages.SetCustomizedXml(GetLanguageCode());
            //DataSet BillingInfo = new DataSet();
            lstBillingInfo = new List<ClsBillingDetailsInfo>();

            DataTable dt = new DataTable();
            dt.Columns.Add("INSTALLMENT_NO", typeof(string));
            dt.Columns.Add("TRAN_TYPE", typeof(string));
            dt.Columns.Add("INSTALLMENT_EFFECTIVE_DATE", typeof(string));

            dt.Columns.Add("INSTALLMENT_AMOUNT", typeof(string));
            dt.Columns.Add("TRAN_PREMIUM_AMOUNT", typeof(string));

            dt.Columns.Add("INTEREST_AMOUNT", typeof(string));
            dt.Columns.Add("TRAN_INTEREST_AMOUNT", typeof(string));

            dt.Columns.Add("FEE", typeof(string));
            dt.Columns.Add("TRAN_FEE", typeof(string));

            dt.Columns.Add("TAXES", typeof(string));
            dt.Columns.Add("TRAN_TAXES", typeof(string));

            dt.Columns.Add("TOTAL", typeof(string));
            dt.Columns.Add("TRAN_TOTAL", typeof(string));
            dt.Columns.Add("ENDO_NO", typeof(string));
            dt.Columns.Add("BOLETO_NO", typeof(string));
            dt.Columns.Add("RELEASED_STATUS", typeof(string));
            dt.Columns.Add("RECEIVED_DATE", typeof(string));

            DataRow dr = dt.NewRow();
            dr["INSTALLMENT_NO"] = "0";
            dr["TRAN_TYPE"] = "";
            dr["INSTALLMENT_EFFECTIVE_DATE"] = "";
            dr["INSTALLMENT_AMOUNT"] = "";
            dr["TRAN_PREMIUM_AMOUNT"] = "";
            dr["INTEREST_AMOUNT"] = "";
            dr["TRAN_INTEREST_AMOUNT"] = "";
            dr["FEE"] = "";
            dr["TRAN_FEE"] = "";
            dr["TAXES"] = "";
            dr["TRAN_TAXES"] = "";
            dr["ENDO_NO"] = "";
            dr["TOTAL"] = "";
            dr["TRAN_TOTAL"] = "";
            dr["BOLETO_NO"] = "";
            dr["RELEASED_STATUS"] = "";
            dr["RECEIVED_DATE"] = "";

            dt.Rows.Add(dr);

            foreach (DataRow dro in dt.Rows)
            {
                ClsBillingDetailsInfo objmodelInfo = new ClsBillingDetailsInfo();
                ClsCommon.PopulateEbixPageModel(dro, objmodelInfo);
                lstBillingInfo.Add(objmodelInfo);
            }
            grdBILLING_INFO.DataSource = lstBillingInfo;
            grdBILLING_INFO.DataBind();
            if (grdBILLING_INFO.Rows.Count > 0)
            {
                for (int i = 0; i < grdBILLING_INFO.Rows[0].Cells.Count; i++)
                    grdBILLING_INFO.Rows[0].Cells[i].Controls.Clear();
            }
            //btnGenrateInstallment.Enabled = true;
        }
        protected void grdBILLING_INFO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label capHEADER_INSLAMENT = (Label)e.Row.FindControl("capINSLAMENT_NO");
                capHEADER_INSLAMENT.Text = objResourceMgr.GetString("lblINSTALLMENT_NO");

                Label capHEADER_TRAN_TYPE = (Label)e.Row.FindControl("capTRAN_TYPE");
                capHEADER_TRAN_TYPE.Text = objResourceMgr.GetString("txtTRAN_TYPE");

                Label capHEADER_INSTALLMENT_DATE = (Label)e.Row.FindControl("capINSTALLMENT_DATE");
                capHEADER_INSTALLMENT_DATE.Text = objResourceMgr.GetString("txtINSTALLMENT_EFFECTIVE_DATE");

                Label capHEADER_POLICY_PREMIUM = (Label)e.Row.FindControl("capPREMIUM");
                capHEADER_POLICY_PREMIUM.Text = objResourceMgr.GetString("txtPREMIUM");

                Label capHEADER_POLICY_INTEREST_AMOUNT = (Label)e.Row.FindControl("capINTEREST_AMOUNT");
                capHEADER_POLICY_INTEREST_AMOUNT.Text = objResourceMgr.GetString("txtINTEREST_AMOUNT");

                Label capHEADER_POLICY_FEE = (Label)e.Row.FindControl("capFEE");
                capHEADER_POLICY_FEE.Text = objResourceMgr.GetString("txtFEE");

                Label capHEADER_TAXES = (Label)e.Row.FindControl("capTAXES");
                capHEADER_TAXES.Text = objResourceMgr.GetString("txtTAXES");

                Label capHEADER_TOTAL = (Label)e.Row.FindControl("capTOTAL");
                capHEADER_TOTAL.Text = objResourceMgr.GetString("txtTOTAL");

                Label capBOLETO = (Label)e.Row.FindControl("capBOLETO");
                capBOLETO.Text = objResourceMgr.GetString("capBOLETO");

                Label capRELEASED_STATUS = (Label)e.Row.FindControl("capRELEASED_STATUS");
                capRELEASED_STATUS.Text = objResourceMgr.GetString("lblRELEASED_STATUS");

                Label capRECIVED_DATE = (Label)e.Row.FindControl("capRECEIVED_DATE");
                capRECIVED_DATE.Text = objResourceMgr.GetString("txtRECEIVED_DATE");

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int a = e.Row.RowIndex + 1;
                //Label Installment = (Label)e.Row.FindControl("lblINSTALLMENT_NO");
                // Installment.Text = a.ToString();

                RegularExpressionValidator revINSTALLMENT_DATE = (RegularExpressionValidator)e.Row.FindControl("revINSTALLMENT_DATE");
                RegularExpressionValidator revPREMIUM = (RegularExpressionValidator)e.Row.FindControl("revPREMIUM");
                RegularExpressionValidator revINTEREST_AMOUNT = (RegularExpressionValidator)e.Row.FindControl("revINTEREST_AMOUNT");
                RegularExpressionValidator revFEE = (RegularExpressionValidator)e.Row.FindControl("revFEE");
                RegularExpressionValidator revTAXES = (RegularExpressionValidator)e.Row.FindControl("revTAXES");

                RequiredFieldValidator rfvINSTALLMENT_DATE = (RequiredFieldValidator)e.Row.FindControl("rfvINSTALLMENT_DATE");
                RequiredFieldValidator rfvPREMIUM = (RequiredFieldValidator)e.Row.FindControl("rfvPREMIUM");
                RequiredFieldValidator rfvINTEREST_AMOUNT = (RequiredFieldValidator)e.Row.FindControl("rfvINTEREST_AMOUNT");
                RequiredFieldValidator rfvFEE = (RequiredFieldValidator)e.Row.FindControl("rfvFEE");
                RequiredFieldValidator rfvTAXES = (RequiredFieldValidator)e.Row.FindControl("rfvTAXES");

                CustomValidator csvPREMIUM = (CustomValidator)e.Row.FindControl("csvPREMIUM");
                CustomValidator csvINTEREST_AMOUNT = (CustomValidator)e.Row.FindControl("csvINTEREST_AMOUNT");
                CustomValidator csvFEE = (CustomValidator)e.Row.FindControl("csvFEE");
                CustomValidator csvTAXES = (CustomValidator)e.Row.FindControl("csvTAXES");


                TextBox txtPREMIUM = (TextBox)e.Row.FindControl("txtPREMIUM");
                // txtPREMIUM.Text = lstBillingInfo[e.Row.DataItemIndex].INSTALLMENT_AMOUNT.CurrentValue.ToString("N",numberFormatInfo);
                TextBox txtINTEREST_AMOUNT = (TextBox)e.Row.FindControl("txtINTEREST_AMOUNT");
                // txtINTEREST_AMOUNT.Text = lstBillingInfo[e.Row.DataItemIndex].INTEREST_AMOUNT.CurrentValue.ToString("N", numberFormatInfo);


                // txtINTEREST_AMOUNT.Text = AA.ToString("N", numberFormatInfo); //lstBillingInfo[e.Row.DataItemIndex].INTEREST_AMOUNT.CurrentValue.ToString("N", numberFormatInfo);
                TextBox txtFEE = (TextBox)e.Row.FindControl("txtFEE");
                //txtFEE.Text = lstBillingInfo[e.Row.DataItemIndex].FEE.CurrentValue.ToString("N", numberFormatInfo);
                TextBox txtTAXES = (TextBox)e.Row.FindControl("txtTAXES");
                // txtTAXES.Text = lstBillingInfo[e.Row.DataItemIndex].TAXES.CurrentValue.ToString("N", numberFormatInfo);
                TextBox txtTOTAL = (TextBox)e.Row.FindControl("txtTOTAL");
                //  txtTOTAL.Text = lstBillingInfo[e.Row.DataItemIndex].TOTAL.CurrentValue.ToString("N", numberFormatInfo);
                //Label lblTRAN_TYPE_NBS = (Label)e.Row.FindControl("lblTRAN_TYPE_NBS");

                //Transaction Amount txtbox
                Label lblBOLETO = (Label)e.Row.FindControl("lblBOLETO");
                lblBOLETO.Text = objResourceMgr.GetString("lblBOLETO");

                //Label lblTRAN_BOLETO = (Label)e.Row.FindControl("lblTRAN_BOLETO");
                //lblTRAN_BOLETO.Text = objResourceMgr.GetString("lblBOLETO");

                Label lblTRAN_TYPE = (Label)e.Row.FindControl("lblTRAN_TYPE");
                Label lblRELEASED_STATUS = (Label)e.Row.FindControl("lblRELEASED_STATUS");
                //TextBox txtTRAN_PREMIUM = (TextBox)e.Row.FindControl("txtTRAN_PREMIUM_AMOUNT");
                //TextBox txtTRAN_INTEREST_AMOUNT = (TextBox)e.Row.FindControl("txtTRAN_INTEREST_AMOUNT");
                //TextBox txtTRAN_FEE = (TextBox)e.Row.FindControl("txtTRAN_FEE");
                //TextBox txtTRAN_TAXES = (TextBox)e.Row.FindControl("txtTRAN_TAXES");
                //TextBox txtTRAN_TOTAL = (TextBox)e.Row.FindControl("txtTRAN_TOTAL");
                //  ClsMessages.SetCustomizedXml(GetLanguageCode());
                csvPREMIUM.ErrorMessage = ClsMessages.GetMessage(ScreenId, "9");
                csvINTEREST_AMOUNT.ErrorMessage = ClsMessages.GetMessage(ScreenId, "10");
                csvFEE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "11");
                csvTAXES.ErrorMessage = ClsMessages.GetMessage(ScreenId, "12");

                revINSTALLMENT_DATE.ValidationExpression = aRegExpDate;
                revINSTALLMENT_DATE.ErrorMessage = hidDATE_MSG.Value;//ClsCommon.FetchGeneralMessage("22","");//Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

                revPREMIUM.ValidationExpression = aRegExpCurrencyformat;//aRegExpCurrencyformat;  aRegExpCurrency           
                revPREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                revINTEREST_AMOUNT.ValidationExpression = aRegExpCurrencyformat;//aRegExpCurrencyformat;
                revINTEREST_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                revFEE.ValidationExpression = aRegExpCurrencyformat;//aRegExpCurrencyformat;
                revFEE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                revTAXES.ValidationExpression = aRegExpCurrencyformat; // aRegExpCurrencyformat;
                revTAXES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                rfvINSTALLMENT_DATE.Enabled = false;
                rfvPREMIUM.Enabled = false;
                rfvINTEREST_AMOUNT.Enabled = false;
                rfvFEE.Enabled = false;
                rfvTAXES.Enabled = false;

                TextBox txtINSTALLMENT_EFFECTIVE_DATE = (TextBox)e.Row.FindControl("txtINSTALLMENT_EFFECTIVE_DATE");
                DateTime dt = lstBillingInfo[e.Row.RowIndex].INSTALLMENT_EFFECTIVE_DATE.CurrentValue;
                txtINSTALLMENT_EFFECTIVE_DATE.Text = ConvertToDateCulture(dt);

                TextBox txtRECEIVED_DATE = (TextBox)e.Row.FindControl("txtRECEIVED_DATE"); //Added By Lalit 
                DateTime Releaseddt = lstBillingInfo[e.Row.RowIndex].RECEIVED_DATE.CurrentValue;
                if (Releaseddt == DateTime.MinValue)
                    txtRECEIVED_DATE.Text = "";
                else
                    txtRECEIVED_DATE.Text = ConvertToDateCulture(Releaseddt);



                //txtINSTALLMENT_EFFECTIVE_DATE = 

                if (lstBillingInfo[e.Row.RowIndex].ENDO_NO.CurrentValue.ToString() != null && lstBillingInfo[e.Row.RowIndex].ENDO_NO.CurrentValue.ToString() != "")
                    TRAN_TYP = lstBillingInfo[e.Row.RowIndex].TRAN_TYPE.CurrentValue + " (" + lstBillingInfo[e.Row.RowIndex].ENDO_NO.CurrentValue.ToString() + ")";
                else
                    TRAN_TYP = lstBillingInfo[e.Row.RowIndex].TRAN_TYPE.CurrentValue.ToString();
                lblTRAN_TYPE.Text = TRAN_TYP;
                if (lstBillingInfo[e.Row.RowIndex].RELEASED_STATUS.CurrentValue == "Y")
                {
                    e.Row.Enabled = false;
                    if (hidREL_INSTLL_NO.Value == "")
                    {
                        hidREL_INSTLL_NO.Value = "1";
                    }
                    else
                    {
                        hidREL_INSTLL_NO.Value = Convert.ToString(int.Parse(hidREL_INSTLL_NO.Value) + 1);
                    }
                }
                if (hidPOLICY_STATUS.Value.Trim().ToUpper() == "UENDRS" || hidPOLICY_STATUS.Value.Trim().ToUpper() == "MENDORSE" || hidPROCESS_ID.Value == "14" || hidPROCESS_ID.Value == "3")
                {
                    if (lstBillingInfo[e.Row.RowIndex].TRAN_TYPE.CurrentValue == "END")
                    {

                    }
                    else if (lstBillingInfo[e.Row.RowIndex].TRAN_TYPE.CurrentValue == "NBS")
                    {


                    }
                }
                //if (hidPOLICY_STATUS.Value.Trim().ToUpper() == "UENDRS" || hidPOLICY_STATUS.Value.Trim().ToUpper() == "MENDORSE")
                //{

                if (lstBillingInfo[e.Row.RowIndex].POLICY_VERSION_ID.CurrentValue != int.Parse(hidPOLICY_VERSION_ID.Value))
                {
                    txtPREMIUM.ReadOnly = true;
                    txtINTEREST_AMOUNT.ReadOnly = true;
                    txtFEE.ReadOnly = true;
                    txtTAXES.ReadOnly = true;
                    txtTOTAL.ReadOnly = true;
                    txtINSTALLMENT_EFFECTIVE_DATE.ReadOnly = true;


                }
                if (lstBillingInfo[e.Row.RowIndex].BOLETO_NO.CurrentValue == null || lstBillingInfo[e.Row.RowIndex].BOLETO_NO.CurrentValue.ToString() == "")
                {
                    lblBOLETO.Attributes.Add("style", "display:none");
                }
                //txtTRAN_PREMIUM.Attributes.Add("style", "display:none");
                //txtTRAN_INTEREST_AMOUNT.Attributes.Add("style", "display:none");
                //txtTRAN_FEE.Attributes.Add("style", "display:none");
                //txtTRAN_TAXES.Attributes.Add("style", "display:none");
                //txtTRAN_TOTAL.Attributes.Add("style", "display:none");
                //lblTRAN_BOLETO.Attributes.Add("style", "display:none");
                //lblTRAN_TYPE_NBS.Attributes.Add("style", "display:none");
                //txtTRAN_PREMIUM.Text = "";
                //txtTRAN_INTEREST_AMOUNT.Text = "";
                //txtTRAN_FEE.Text = "";
                //txtTRAN_TAXES.Text = "";
                //txtTRAN_TOTAL.Text = "";

                //added By lalit Nov,22
                // itrack no 1761 by prveer 
                //if (hidCO_INSURANCE.Value == CO_INSURANCE_FOLLOWER)
                //{
                //    txtFEE.ReadOnly = true;
                //    txtTAXES.ReadOnly = true;
                //}

                CompareValidator cmpINSTALLMENT_EFF_DATE = (CompareValidator)e.Row.FindControl("cmpINSTALLMENT_EFF_DATE"); //Added By Lalit 
                CompareValidator cmpINSTALLMENT_EXP_DATE = (CompareValidator)e.Row.FindControl("cmpINSTALLMENT_EXP_DATE"); //Added By Lalit 

                cmpINSTALLMENT_EFF_DATE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "47");
                cmpINSTALLMENT_EXP_DATE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "48");

                if (hidPOL_EFFECTIVE_DATE.Value != "" && hidPOL_EXPIRY_DATE.Value != "")
                    if (lblTRAN_TYPE.Text.Trim().ToUpper().Contains("END"))
                    {
                        //cmpINSTALLMENT_EFF_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(hidPOL_EFF_DATE.Value));
                        //cmpINSTALLMENT_EXP_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(hidEXPIRY_DATE.Value));
                        cmpINSTALLMENT_EFF_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(hidPOL_EFFECTIVE_DATE.Value));
                        cmpINSTALLMENT_EXP_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(hidPOL_EXPIRY_DATE.Value));
                        cmpINSTALLMENT_EFF_DATE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "49");
                        cmpINSTALLMENT_EXP_DATE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "50");

                    }
                    else
                    {

                        cmpINSTALLMENT_EFF_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(hidPOL_EFFECTIVE_DATE.Value));
                        cmpINSTALLMENT_EXP_DATE.ValueToCompare = ConvertToDateCulture(Convert.ToDateTime(hidPOL_EXPIRY_DATE.Value));


                    }

                //if CUP in progress then user can readjust installment amoumt of previus version
                Label lblPOLICY_VERSION_ID = (Label)e.Row.FindControl("lblPOLICY_VERSION_ID");
                if (hidPROCESS_ID.Value == "8" || hidPROCESS_ID.Value == "9")
                {
                    if (lblPOLICY_VERSION_ID != null)
                        if (lblPOLICY_VERSION_ID.Text.Trim() == lblPOLICY_VERSION_ID.Text.Trim())
                        {
                            txtPREMIUM.ReadOnly = false;
                            txtINTEREST_AMOUNT.ReadOnly = false;
                            txtFEE.ReadOnly = false;
                            txtTAXES.ReadOnly = false;
                            txtTOTAL.ReadOnly = false;
                            txtINSTALLMENT_EFFECTIVE_DATE.ReadOnly = false;
                        }
                }
                //
                if (lblRELEASED_STATUS.Text.Equals("U"))
                {
                    e.Row.Style.Add("display", "none");
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {


            }
        }//Add validation Messages for textbox in GridView

    }
}