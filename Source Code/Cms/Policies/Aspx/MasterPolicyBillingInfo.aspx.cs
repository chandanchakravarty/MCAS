/******************************************************************************************
<Author					: -  Lalit Chauhan  
<Start Date				: -	 25 - Oct - 2010
<End Date				: -	
<Description			: -  Add New Billing Info
<Review Date			: - 
<Reviewed By			: - 	
 
Modification History
<Modified Date			: 
<Modified By			: 
<Purpose				: 
						
****************************************************************************************** */
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

namespace Cms.Policies.Aspx
{
    public partial class MasterPolicyBillingInfo : Cms.Policies.policiesbase
    {
        protected List<ClsPolicyBillingInfo> lstBillingInfo = new List<ClsPolicyBillingInfo>();
        Cms.BusinessLayer.Blapplication.ClsPolicyInstallments objclsBLInstallments = new Cms.BusinessLayer.Blapplication.ClsPolicyInstallments();
        ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.MasterPolicyBillingInfo", System.Reflection.Assembly.GetExecutingAssembly()); //Resource manager For Multilingual Support
        //numberFormatInfo
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "224_11";
            numberFormatInfo.NumberDecimalDigits = 2;
            btnView_All_Boleto.Attributes.Add("onclick", "javascript:return viewAllBoleto()");
            btnGENRATE_INSTALLMENTS.Attributes.Add("onclick", "javascript:return UserIsManager()");  // changed by praveer for itrack no 1761
            if (!IsPostBack)
            {
                hidCUSTOMER_ID.Value = GetCustomerID();
                hidPOLICY_ID.Value = GetPolicyID();
                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
                hidPOLICY_STATUS.Value = GetPolicyStatus();
                hidCALLED_FROM.Value = Request.QueryString["CALLEDFROM"].ToString();
                this.getPolicy_SelectedPlan();
                GetPolicy_CurrentStatus(); //get current policy process status   
                BindGrid();
            }
            SetCaptions();

            btnGENRATE_INSTALLMENTS.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnGENRATE_INSTALLMENTS.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            //btnBoletoReprint.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            //btnBoletoReprint.PermissionString = gstrSecurityXML;

            if (hidCO_INSURANCE.Value == CO_INSURANCE_FOLLOWER && grdBILLING_INFO.Columns[12] != null)
            {
                grdBILLING_INFO.Columns[12].Visible = false;
                btnView_All_Boleto.Visible = false;
                btnBoletoReprint.Style.Add("display", "none");
            }

        }
        private void getPolicy_SelectedPlan()
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            ClsPolicyBillingInfo objBillingInfo = new ClsPolicyBillingInfo();
            DataSet dsPOL_PLAN = objBillingInfo.GetCoveragesPremium(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(GetLOBID()));
            if (dsPOL_PLAN != null && dsPOL_PLAN.Tables[0].Rows.Count > 0)
            {

                hidCO_INSURANCE.Value = dsPOL_PLAN.Tables[0].Rows[0]["CO_INSURANCE"].ToString();
                // change by praveer for itrack no 1511
                hidSELECTED_PLAN_ID.Value = dsPOL_PLAN.Tables[0].Rows[0]["INSTALL_PLAN_ID"].ToString();
                hidPLAN_TYPE.Value = dsPOL_PLAN.Tables[0].Rows[0]["PLAN_TYPE"].ToString();// changed by praveer for itrack no 1567/TFS # 629
                // changed by praveer for itrack no 1761
                if (dsPOL_PLAN.Tables[0].Rows[0]["IOF_PERCENTAGE"].ToString() != "")
                    hid_IOF_PERCENTAGE.Value = Convert.ToDecimal(dsPOL_PLAN.Tables[0].Rows[0]["IOF_PERCENTAGE"], numberFormatInfo).ToString("N", numberFormatInfo);

            }

        }//get policy select plan for billing at page load
        private void SetCaptions()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnGENRATE_INSTALLMENTS.Text = ClsMessages.GetButtonsText(ScreenId, "btnGenrateInstallment");
            lblHeader.Text = objResourceMgr.GetString("lblHeader");
            btnView_All_Boleto.Text = ClsMessages.GetButtonsText(ScreenId, "btnView_All_Boleto");
            btnBoletoReprint.Text = ClsMessages.GetButtonsText(ScreenId, "btnBoletoReprint");
        }
        private void GetPolicy_CurrentStatus()
        {
            DataSet ds_process = null;
            ClsPolicyBillingInfo objBillingInfo = new ClsPolicyBillingInfo();
            ds_process = objBillingInfo.GetPolicy_ProcessStatus(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            if (ds_process != null && ds_process.Tables.Count > 0 && ds_process.Tables[0].Rows.Count > 0)
            {

                hidPROCESS_ID.Value = ds_process.Tables[0].Rows[0]["PROCESS_ID"].ToString();
            }
            else
            {

                hidPROCESS_ID.Value = "0";
            }
        }//show transaction amount field if policy process id in 14,3 for endorsment
        private void BindGrid()
        {
            DataSet ds = new DataSet();
            lstBillingInfo = ClsPolicyInstallments.GetInstallmentDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            if (lstBillingInfo.Count > 0)
            {
                hidFlag.Value = "Y";// changed by praveer for itrack no 1761
                grdBILLING_INFO.DataSource = lstBillingInfo;
                grdBILLING_INFO.DataBind();
                //SetPageModelObjects(lstBillingInfo);

            }
            else
            {
                this.BindBlankGrid();
            }
            //
            if (hidPROCESS_ID.Value == "" || hidPROCESS_ID.Value == "0" || hidPROCESS_ID.Value == "24")
            {

                btnBoletoReprint.Visible = false;
                btnView_All_Boleto.Visible = false;
            }

            SetPageModelObjects(lstBillingInfo);
           
               
            
            
            hidGRID_ROW_COUNT.Value = grdBILLING_INFO.Rows.Count.ToString();
            if (hidPROCESS_ID.Value != "37" && hidPROCESS_ID.Value != "12" && hidPROCESS_ID.Value != "2")
            ComparePremium(lstBillingInfo);
        }

        private void BindBlankGrid()
        {
            //DataSet BillingInfo = new DataSet();
            lstBillingInfo = new List<ClsPolicyBillingInfo>();

            DataTable dt = new DataTable();
            dt.Columns.Add("ROW_ID", typeof(string));
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
            dt.Columns.Add("CO_APPLICANT_NAME", typeof(string));
            dt.Columns.Add("BOLETO_NO", typeof(string));

            DataRow dr = dt.NewRow();

            dr["ROW_ID"] = "0";
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
            dr["CO_APPLICANT_NAME"] = "";
            dr["BOLETO_NO"] = "";
            dt.Rows.Add(dr);

            foreach (DataRow dro in dt.Rows)
            {
                ClsPolicyBillingInfo objmodelInfo = new ClsPolicyBillingInfo();
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
        } //Bind Gridview Control With Premium Installment

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (grdBILLING_INFO.Rows.Count > 0)
            {
                ArrayList arlobjBillingInfo = new ArrayList();
                int retval = 0;
                try
                {
                    arlobjBillingInfo = GetUpdatedInstallmentDetails();
                    if (arlobjBillingInfo.Count > 0)
                    {
                        retval = objclsBLInstallments.SaveMasterPolicyInstallments(arlobjBillingInfo);
                        if (retval > 0)
                        {
                            ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                            //base.OpenEndorsementDetails();
                        }
                        else
                        {
                            ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = ClsMessages.GetMessage(ScreenId, "24");
                        }
                    }
                    else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = "";//lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    }

                }
                catch (Exception ex)
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "2") + ": " + ex.Message;
                }
                finally { BindGrid(); }
            }

        } //btn Submitt click

        private ArrayList GetUpdatedInstallmentDetails()
        {
            ArrayList arlobjBillingInfo = new ArrayList();

            lstBillingInfo = (List<ClsPolicyBillingInfo>)GetPageModelObjects();
            //int flag = 0;
            int totalRowsCount = grdBILLING_INFO.Rows.Count;
            int Counter = 0;
            Decimal TotalPolicyPremium = 0;
            Decimal TotalPolicyTaxes = 0;
            Decimal TotalPolicyFess = 0;
            Decimal TotalPolicyInterestAmount = 0;
            Decimal InstallmentTotal = 0;
            foreach (GridViewRow rw in grdBILLING_INFO.Rows)
            {
                HtmlInputHidden hidUPDATEFlag = (HtmlInputHidden)rw.FindControl("hidUPDATEFlag");
                Label lblINSTALLMENT_NO = (Label)rw.FindControl("lblINSTALLMENT_NO");
                Label lblROW_ID = (Label)rw.FindControl("lblROW_ID");
                Label lblTRAN_TYPE = (Label)rw.FindControl("lblTRAN_TYPE");
                Label lblPOLICY_VERSION_ID = (Label)rw.FindControl("lblPOLICY_VERSION_ID");

                TextBox txtINSTALLMENT_DATE = (TextBox)rw.FindControl("txtINSTALLMENT_DATE");
                TextBox txtPREMIUM = (TextBox)rw.FindControl("txtPREMIUM");
                TextBox txtINTEREST_AMOUNT = (TextBox)rw.FindControl("txtINTEREST_AMOUNT");
                TextBox txtFEE = (TextBox)rw.FindControl("txtFEE");
                TextBox txtTAXES = (TextBox)rw.FindControl("txtTAXES");
                TextBox txtTOTAL = (TextBox)rw.FindControl("txtTOTAL");
                HtmlInputHidden hidTOTAL = (HtmlInputHidden)rw.FindControl("hidTOTAL");

                TextBox txtTRAN_PREMIUM = (TextBox)rw.FindControl("txtTRAN_PREMIUM_AMOUNT");
                TextBox txtTRAN_INTEREST_AMOUNT = (TextBox)rw.FindControl("txtTRAN_INTEREST_AMOUNT");
                TextBox txtTRAN_FEE = (TextBox)rw.FindControl("txtTRAN_FEE");
                TextBox txtTRAN_TAXES = (TextBox)rw.FindControl("txtTRAN_TAXES");
                TextBox txtTRAN_TOTAL = (TextBox)rw.FindControl("txtTRAN_TOTAL");
                HtmlInputHidden hidTRAN_TOTAL = (HtmlInputHidden)rw.FindControl("hidTRAN_TOTAL");


                Counter = Counter + 1;
                //ClsPolicyBillingInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { reinsurer = ob, REINID = id }).Where(ob => ob.reinsurer.REINSURANCE_ID.CurrentValue == int.Parse(lb.Text.Trim()) && ob.reinsurer.COMPANY_ID.CurrentValue == CompanyId).Select(ob => ob.reinsurer).First();
                //Find model object from list using lamda expression
                //ClsPolicyBillingInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.INSTALLMENT_NO.CurrentValue == int.Parse(lblINSTALLMENT_NO.Text.Trim()) && ob.BillingInfo.POLICY_VERSION_ID.CurrentValue == int.Parse(hidPOLICY_VERSION_ID.Value) && ob.BillingInfo.ROW_ID.CurrentValue == int.Parse(lblROW_ID.Text.Trim())).Select(ob => ob.BillingInfo).First();
                if (lblROW_ID.Text != "")
                {
                    ClsPolicyBillingInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.ROW_ID.CurrentValue == int.Parse(lblROW_ID.Text.Trim())).Select(ob => ob.BillingInfo).First();
                    if (objBillingInfo != null)
                    {
                        objBillingInfo.ACTION = enumAction.Update;
                        //objBillingInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                        //objBillingInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                        //objBillingInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                        InstallmentTotal = 0;
                        TotalPolicyAMount(lstBillingInfo);
                        if (lblINSTALLMENT_NO.Text != "")
                            if (int.Parse(lblPOLICY_VERSION_ID.Text) == int.Parse(hidPOLICY_VERSION_ID.Value))
                            {
                                totalRowsCount = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.POLICY_VERSION_ID.CurrentValue == int.Parse(hidPOLICY_VERSION_ID.Value.Trim())).Count();

                                objBillingInfo.INSTALLMENT_NO.CurrentValue = int.Parse(lblINSTALLMENT_NO.Text);
                                objBillingInfo.INSTALLMENT_NO.IsChanged = true;

                                if (txtPREMIUM.Text != "")
                                {
                                    objBillingInfo.INSTALLMENT_AMOUNT.CurrentValue = decimal.Parse(txtPREMIUM.Text, numberFormatInfo);
                                    TotalPolicyPremium = TotalPolicyPremium + decimal.Parse(txtPREMIUM.Text, numberFormatInfo);
                                    InstallmentTotal = objBillingInfo.INSTALLMENT_AMOUNT.CurrentValue;
                                }
                                else
                                    objBillingInfo.INSTALLMENT_AMOUNT.CurrentValue = 0;

                                if (txtINTEREST_AMOUNT.Text != "")
                                {
                                    objBillingInfo.INTEREST_AMOUNT.CurrentValue = decimal.Parse(txtINTEREST_AMOUNT.Text, numberFormatInfo);
                                    InstallmentTotal = InstallmentTotal + objBillingInfo.INTEREST_AMOUNT.CurrentValue;
                                    TotalPolicyInterestAmount = TotalPolicyInterestAmount + objBillingInfo.INTEREST_AMOUNT.CurrentValue;
                                }
                                else
                                    objBillingInfo.INTEREST_AMOUNT.CurrentValue = 0;

                                if (txtFEE.Text != "")
                                {
                                    objBillingInfo.FEE.CurrentValue = decimal.Parse(txtFEE.Text, numberFormatInfo);
                                    InstallmentTotal = InstallmentTotal + objBillingInfo.FEE.CurrentValue;
                                    TotalPolicyFess = TotalPolicyFess + objBillingInfo.FEE.CurrentValue;
                                }
                                else
                                    objBillingInfo.FEE.CurrentValue = 0;

                                if (txtTAXES.Text != "")
                                {
                                    objBillingInfo.TAXES.CurrentValue = decimal.Parse(txtTAXES.Text, numberFormatInfo);
                                    InstallmentTotal = InstallmentTotal + objBillingInfo.TAXES.CurrentValue;
                                    TotalPolicyTaxes = TotalPolicyTaxes + objBillingInfo.TAXES.CurrentValue;
                                }
                                else
                                    objBillingInfo.TAXES.CurrentValue = 0;

                                objBillingInfo.TOTAL.CurrentValue = InstallmentTotal;// double.Parse(InstallmentTotal, numberFormatInfo);

                                if (objBillingInfo.TRAN_TYPE.CurrentValue.Contains("END"))
                                {
                                    objBillingInfo.TRAN_PREMIUM_AMOUNT.CurrentValue = objBillingInfo.INSTALLMENT_AMOUNT.CurrentValue;
                                    objBillingInfo.TRAN_INTEREST_AMOUNT.CurrentValue = objBillingInfo.INTEREST_AMOUNT.CurrentValue;
                                    objBillingInfo.TRAN_FEE.CurrentValue = objBillingInfo.FEE.CurrentValue;
                                    objBillingInfo.TRAN_TAXES.CurrentValue = objBillingInfo.TAXES.CurrentValue;
                                    objBillingInfo.TRAN_TOTAL.CurrentValue = objBillingInfo.TOTAL.CurrentValue;
                                }
                                else
                                {
                                    objBillingInfo.TRAN_PREMIUM_AMOUNT.CurrentValue = 0;
                                    objBillingInfo.TRAN_INTEREST_AMOUNT.CurrentValue = 0;
                                    objBillingInfo.TRAN_FEE.CurrentValue = 0;
                                    objBillingInfo.TRAN_TAXES.CurrentValue = 0;
                                    objBillingInfo.TRAN_TOTAL.CurrentValue = 0;
                                }

                                if (txtINSTALLMENT_DATE.Text != "")
                                {
                                    objBillingInfo.INSTALLMENT_EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtINSTALLMENT_DATE.Text);
                                }
                                objBillingInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                objBillingInfo.LAST_UPDATED_DATETIME.CurrentValue = DateTime.Now;

                                //Update Policy Total Amounts In ACT_POLICY_INSTALL_PALN_DATA Table                   
                                //Update Total amount if gridview row has updated.
                                //create Model Object set Policy Total Mount values ie:Total Policy Premium, Total Policy Taxes etc.
                                //set value in last updated installment object 

                                if (Counter == totalRowsCount)
                                {
                                    //objBillingInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                                    //objBillingInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                                    //objBillingInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                                    objBillingInfo.TOTAL_PREMIUM.CurrentValue = this.TotalPolicyAMount(lstBillingInfo);//TotalPolicyPremium;
                                    objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue = TotalPolicyInterestAmount;
                                    objBillingInfo.TOTAL_FEES.CurrentValue = TotalPolicyFess;
                                    objBillingInfo.TOTAL_TAXES.CurrentValue = TotalPolicyTaxes;
                                    objBillingInfo.TOTAL_AMOUNT.CurrentValue = objBillingInfo.TOTAL_PREMIUM.CurrentValue + TotalPolicyInterestAmount + TotalPolicyFess + TotalPolicyTaxes; //double.Parse("0");//double.Parse(txtTOTAL_AMOUNT.Text);
                                    objBillingInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                    objBillingInfo.LAST_UPDATED_DATETIME.CurrentValue = DateTime.Now;
                                    //objBillingInfo.TOTAL_INFO_PRM.CurrentValue = 
                                    if (objBillingInfo.TRAN_TYPE.CurrentValue.Contains("END"))
                                    {
                                        objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue = this.TotalTranPolicyAMount(lstBillingInfo, hidPOLICY_VERSION_ID.Value);
                                        objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue = TotalPolicyInterestAmount;
                                        objBillingInfo.TOTAL_TRAN_FEES.CurrentValue = TotalPolicyFess;
                                        objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue = TotalPolicyTaxes;
                                        objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue = objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue + TotalPolicyInterestAmount + TotalPolicyFess + TotalPolicyTaxes; //double.Parse("0");//double.Parse(txtTOTAL_AMOUNT.Text);

                                    }
                                    else 
                                    {// changed by praveer for TFS# 2501
                                        objBillingInfo.TOTAL_TRAN_PREMIUM.CurrentValue = objBillingInfo.TOTAL_PREMIUM.CurrentValue; 
                                        objBillingInfo.TOTAL_TRAN_INTEREST_AMOUNT.CurrentValue = objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue; 
                                        objBillingInfo.TOTAL_TRAN_FEES.CurrentValue = objBillingInfo.TOTAL_FEES.CurrentValue;
                                        objBillingInfo.TOTAL_TRAN_TAXES.CurrentValue = objBillingInfo.TOTAL_TAXES.CurrentValue;
                                        objBillingInfo.TOTAL_TRAN_AMOUNT.CurrentValue = objBillingInfo.TOTAL_AMOUNT.CurrentValue;

                                    }
                                }
                                arlobjBillingInfo.Add(objBillingInfo);  //Add  model Object for total value field in array list
                            }
                    }
                }
            }
            return arlobjBillingInfo;
        }

        protected void btnGENRATE_INSTALLMENTS_Click(object sender, EventArgs e)
        {
            this.GenrateInstallment();
        }
        private Decimal TotalPolicyAMount(List<ClsPolicyBillingInfo> lstBillingInfo)
        {
            Decimal Total_premium = 0;
            if (lstBillingInfo.Count > 0)
            {
                Total_premium = lstBillingInfo.Sum(x => x.INSTALLMENT_AMOUNT.CurrentValue);
            }
            return Math.Round(Total_premium, 2);
        }
        private Decimal TotalTranPolicyAMount(List<ClsPolicyBillingInfo> lstBillingInfo, string PolicyVersionId)
        {
            Decimal Total_premium = 0;
            if (lstBillingInfo.Count > 0)
            {
                Total_premium = lstBillingInfo.Where(p => p.POLICY_VERSION_ID.CurrentValue == int.Parse(hidPOLICY_VERSION_ID.Value)).Sum(x => x.INSTALLMENT_AMOUNT.CurrentValue); //.Sum(x => x.TOTAL_TRAN_PREMIUM.CurrentValue);
            }
            return Math.Round(Total_premium, 2);
        }

        private void GenrateInstallment()
        {
            
            int retVal = 0;
            ClsPolicyBillingInfo objPolicyBillingInfo = new ClsPolicyBillingInfo();
            objPolicyBillingInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
            objPolicyBillingInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
            objPolicyBillingInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
            //objPolicyBillingInfo.PLAN_ID.CurrentValue = int.Parse();
            // added by praveer for itrack no 1208
            objPolicyBillingInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
            objPolicyBillingInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
            retVal = objclsBLInstallments.GenrateMasterPolicyInstallment(objPolicyBillingInfo);
            BindGrid();
            if (retVal > 0) 
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "53");//+ "<br/>" + lblMessage.Text;

            }
            else if (retVal == -10)//changed by praveer for itrack no 1761
            {
               lblMessage.Text = ClsMessages.GetMessage(ScreenId, "55");

            }
            

        }

        protected void grdBILLING_INFO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            if (e.Row.RowType == DataControlRowType.Header)
            {

                Label capINSLAMENT_NO = (Label)e.Row.FindControl("capINSLAMENT_NO");
                capINSLAMENT_NO.Text = objResourceMgr.GetString("lblPOLICY_VERSION_ID");

                Label capTRAN_TYPE = (Label)e.Row.FindControl("capTRAN_TYPE");
                capTRAN_TYPE.Text = objResourceMgr.GetString("lblTRAN_TYPE");

                Label capCO_APPLICANT_NAME = (Label)e.Row.FindControl("capCO_APPLICANT_NAME");
                capCO_APPLICANT_NAME.Text = objResourceMgr.GetString("lblCO_APPLICANT_NAME");

                Label capINSTALLMENT_DATE = (Label)e.Row.FindControl("capINSTALLMENT_DATE");
                capINSTALLMENT_DATE.Text = objResourceMgr.GetString("txtINSTALLMENT_DATE");

                Label capPREMIUM = (Label)e.Row.FindControl("capPREMIUM");
                capPREMIUM.Text = objResourceMgr.GetString("txtPREMIUM");

                Label capINTEREST_AMOUNT = (Label)e.Row.FindControl("capINTEREST_AMOUNT");
                capINTEREST_AMOUNT.Text = objResourceMgr.GetString("txtINTEREST_AMOUNT");

                Label capFEE = (Label)e.Row.FindControl("capFEE");
                capFEE.Text = objResourceMgr.GetString("txtFEE");

                Label capTAXES = (Label)e.Row.FindControl("capTAXES");
                capTAXES.Text = objResourceMgr.GetString("txtTAXES");

                Label capTOTAL = (Label)e.Row.FindControl("capTOTAL");
                capTOTAL.Text = objResourceMgr.GetString("txtTOTAL");

                //Label capBOLETO = (Label)e.Row.FindControl("capBOLETO");
                //capBOLETO.Text = objResourceMgr.GetString("capBOLETO");

                Label capRELEASED_STATUS = (Label)e.Row.FindControl("capRELEASED_STATUS");
                capRELEASED_STATUS.Text = objResourceMgr.GetString("lblRELEASED_STATUS");

                Label capRECEIVED_DATE = (Label)e.Row.FindControl("capRECEIVED_DATE");
                capRECEIVED_DATE.Text = objResourceMgr.GetString("txtRECEIVED_DATE");


                Label capBOLETO = (Label)e.Row.FindControl("capBOLETO");
                capBOLETO.Text = objResourceMgr.GetString("lblBOLETO");


            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblINSTALLMENT_NO = (Label)e.Row.FindControl("lblINSTALLMENT_NO");
                Label lblROW_ID = (Label)e.Row.FindControl("lblROW_ID");
                Label lblTRAN_TYPE = (Label)e.Row.FindControl("lblTRAN_TYPE");
                Label lblENDO_NO = (Label)e.Row.FindControl("lblENDO_NO");
                Label lblBOLETO = (Label)e.Row.FindControl("lblBOLETO");
                lblBOLETO.Text = objResourceMgr.GetString("lblBOLETO");

                Label lblPOLICY_VERSION_ID = (Label)e.Row.FindControl("lblPOLICY_VERSION_ID");
                TextBox txtINSTALLMENT_DATE = (TextBox)e.Row.FindControl("txtINSTALLMENT_DATE");
                TextBox txtPREMIUM = (TextBox)e.Row.FindControl("txtPREMIUM");
                TextBox txtINTEREST_AMOUNT = (TextBox)e.Row.FindControl("txtINTEREST_AMOUNT");
                TextBox txtFEE = (TextBox)e.Row.FindControl("txtFEE");
                TextBox txtTAXES = (TextBox)e.Row.FindControl("txtTAXES");
                TextBox txtTOTAL = (TextBox)e.Row.FindControl("txtTOTAL");

                RegularExpressionValidator revTAXES = (RegularExpressionValidator)e.Row.FindControl("revTAXES");
                RegularExpressionValidator revFEE = (RegularExpressionValidator)e.Row.FindControl("revFEE");
                RegularExpressionValidator revINTEREST_AMOUNT = (RegularExpressionValidator)e.Row.FindControl("revINTEREST_AMOUNT");
                RegularExpressionValidator revPREMIUM = (RegularExpressionValidator)e.Row.FindControl("revPREMIUM");
                RequiredFieldValidator rfvINSTALLMENT_DATE = (RequiredFieldValidator)e.Row.FindControl("rfvINSTALLMENT_DATE");
                rfvINSTALLMENT_DATE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "46");
                RegularExpressionValidator revINSTALLMENT_DATE = (RegularExpressionValidator)e.Row.FindControl("revINSTALLMENT_DATE");
                Label lblBOLETO_NO = (Label)e.Row.FindControl("lblBOLETO_NO");
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                revTAXES.ValidationExpression = aRegExpDoublePositiveWithZero; //aRegExpCurrencyformat;
                revTAXES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

                revFEE.ValidationExpression = aRegExpDoublePositiveWithZero; //aRegExpCurrencyformat;
                revFEE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

                revINTEREST_AMOUNT.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpCurrencyformat;
                revINTEREST_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

                revPREMIUM.ValidationExpression = aRegExpCurrencyformat;
                revPREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");


                revINSTALLMENT_DATE.ValidationExpression = aRegExpDate;
                revINSTALLMENT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");


                DateTime dt = Convert.ToDateTime(txtINSTALLMENT_DATE.Text);
                txtINSTALLMENT_DATE.Text = ConvertToDateCulture(dt); //ConvertDBDateToCulture(txtINSTALLMENT_DATE.Text);


                txtPREMIUM.Attributes.Add("onBlur", "InstallmentTotal(this);this.value=formatAmount(this.value)");
                txtINTEREST_AMOUNT.Attributes.Add("onBlur", "InstallmentTotal(this);this.value=formatAmount(this.value)");
                txtFEE.Attributes.Add("onBlur", "InstallmentTotal(this);this.value=formatAmount(this.value)");
                txtTAXES.Attributes.Add("onBlur", "InstallmentTotal(this);this.value=formatAmount(this.value)");
                txtINSTALLMENT_DATE.Attributes.Add("onBlur", "FormatDate()");

                if (lblTRAN_TYPE != null && lblTRAN_TYPE.Text.Trim() == "END" && lblENDO_NO.Text.Trim() != "")
                {
                    lblTRAN_TYPE.Text = lblTRAN_TYPE.Text.Trim() + "(" + lblENDO_NO.Text.Trim() + ")";
                }

                if (lblPOLICY_VERSION_ID.Text.Trim() != hidPOLICY_VERSION_ID.Value.Trim())
                {
                    txtPREMIUM.ReadOnly = true;
                    txtINTEREST_AMOUNT.ReadOnly = true;
                    txtFEE.ReadOnly = true;
                    txtTAXES.ReadOnly = true;

                }
                if (lblBOLETO_NO.Text == "")
                    lblBOLETO.Attributes.Add("style", "display:none");
                // changed by praveer for itrack no 1761
                //if (hidCO_INSURANCE.Value == CO_INSURANCE_FOLLOWER)
                //{
                //    txtFEE.ReadOnly = true;
                //    txtTAXES.ReadOnly = true;
                //}

                TextBox txtRECEIVED_DATE = (TextBox)e.Row.FindControl("txtRECEIVED_DATE");
                if (txtRECEIVED_DATE.Text != "")
                {
                    DateTime recivedDate = Convert.ToDateTime(txtRECEIVED_DATE.Text);
                    if (recivedDate == DateTime.MinValue)
                        txtRECEIVED_DATE.Text = "";
                    else
                        txtRECEIVED_DATE.Text = ConvertToDateCulture(recivedDate);
                }

                Label lblRELEASED_STATUS = (Label)e.Row.FindControl("lblRELEASED_STATUS");
                if (lblRELEASED_STATUS.Text.Equals("U")) 
                {
                    e.Row.Style.Add("display", "none");
                    revTAXES.Enabled=false;
                    revPREMIUM.Enabled=false;
                    revFEE.Enabled = false;
                    revINSTALLMENT_DATE.Enabled = false;
                    revINTEREST_AMOUNT.Enabled = false;
                }

            }

        }
        private bool ComparePremium(List<ClsPolicyBillingInfo> lstPolicyBillingInfo) 
        {
            decimal Premium = 0;
            bool valid = true;
            ClsPolicyBillingInfo objBillingInfo = new ClsPolicyBillingInfo();
            int Instl = objBillingInfo.Check_InstallGenstatus(int.Parse(hidPOLICY_ID.Value), int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            objBillingInfo.Dispose();
            if (Instl != 0)
            {
                if (lstPolicyBillingInfo.Count > 0)
                {
                    ClsPolicyBillingInfo objPolicyBillingInfo = (ClsPolicyBillingInfo)lstPolicyBillingInfo[0];
                    Premium = objclsBLInstallments.GetMasterPolicyPremium(objPolicyBillingInfo);
                    decimal Total;
                    Total = lstPolicyBillingInfo.Where(x => x.POLICY_VERSION_ID.CurrentValue == int.Parse(hidPOLICY_VERSION_ID.Value)).Sum(x => x.INSTALLMENT_AMOUNT.CurrentValue);
                    // change by praveer incase of Auto billing we donot need to compare policy premium to coverage premium (itrack no 1511)
                    if (hidPLAN_TYPE.Value == "MAUTO" || hidPLAN_TYPE.Value == "MMANNUAL" || hidPOLICY_STATUS.Value == "NORMAL")// changed by praveer for itrack no 1567/TFS # 629
                    {
                        Total = Premium;
                    }
                    
                    if (Total != Premium)
                    {
                        valid = false;
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        // changes by praveer for itrack no 1511
                        if (lblMessage.Text == ClsMessages.FetchGeneralMessage("31"))
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("31")+ "<br/>"+ ClsMessages.GetMessage(ScreenId, "25");
                        else
                            lblMessage.Text = ClsMessages.GetMessage(ScreenId, "25");
                    }
                }
            }
            return valid;
        }
    }
}
