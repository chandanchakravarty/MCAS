/******************************************************************************************
<Author					: - Pradeep Kushwaha 
<Start Date				: -	22-Oct-2010
<End Date				: -	
<Description			: - Code behind file Deposit screen.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Class for showing the add deposit screen.
*******************************************************************************************/
#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Model.Account;
using System.Threading;
using System.Globalization;
using System.Data;
#endregion


namespace Cms.Account.Aspx
{
    public partial class AddDepositDetails : Cms.Account.AccountBase
    {
       
        System.Resources.ResourceManager objResourceMgr;
        public string URL;
        private string strRowId = "";
        ClsDepositDetails ObjDepositDetails = new ClsDepositDetails();
        public NumberFormatInfo nfi;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "187_1";
            numberFormatInfo.NumberDecimalDigits = 2;
            #region setting security permission
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnGenerateReceipt.CmsButtonClass = CmsButtonType.Write;
            btnGenerateReceipt.PermissionString = gstrSecurityXML;

            btnAccept.CmsButtonClass = CmsButtonType.Execute;
            btnAccept.PermissionString = gstrSecurityXML;

            hidPolicy.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2006");
            #endregion
            
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddDepositDetails", System.Reflection.Assembly.GetExecutingAssembly());
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            hlkPAYMENT_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtPAYMENT_DATE'), document.getElementById('txtPAYMENT_DATE'))");
            URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
            Ajax.Utility.RegisterTypeForAjax(typeof(AddDepositDetails));
            if (!IsPostBack)
            {
               
                this.SetErrorMessages();
                this.BindDownPaymentModeDD();//Bind the Down Payment Mode option
                this.GetSysParam();
                this.BindApproveRefundDD();
                
                if (Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "" && 
                    Request.QueryString["CD_LINE_ITEM_ID"] != null && Request.QueryString["CD_LINE_ITEM_ID"].ToString() != ""
                    )
                {
                   hidDEPOSIT_ID.Value = Request.QueryString["DEPOSIT_ID"].ToString();
                   hidCD_LINE_ITEM_ID.Value = Request.QueryString["CD_LINE_ITEM_ID"].ToString();
                   hidDEPOSIT_NUMBER.Value = Request.QueryString["DEPOSIT_NUMBER"].ToString();
                   hidDEPOSIT_TYPE.Value = Request.QueryString["DEPOSIT_TYPE"].ToString();
                   hidDEPOSIT_TYPE1.Value = Request.QueryString["DEPOSIT_TYPE"].ToString();
                   hidRECEIPT_MODE.Value = Request.QueryString["RECEIPT_MODE"].ToString();//Added for itrack- 1495
                   this.BindDownPaymentModeDD();
                   if (Request.QueryString["BOLETO_ID"] != null && Request.QueryString["BOLETO_ID"].ToString() != "")
                        hidBOLETO_NO.Value = Request.QueryString["BOLETO_ID"].ToString();
                   this.GetOldDataObject(int.Parse(hidCD_LINE_ITEM_ID.Value));
                   btnDelete.Visible = true;
                   
                }
                else
                {
                    if (Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "")
                    {
                        hidDEPOSIT_ID.Value = Request.QueryString["DEPOSIT_ID"].ToString();
                        hidDEPOSIT_NUMBER.Value = Request.QueryString["DEPOSIT_NUMBER"].ToString();
                        hidDEPOSIT_TYPE.Value = Request.QueryString["DEPOSIT_TYPE"].ToString();
                        hidDEPOSIT_TYPE1.Value = Request.QueryString["DEPOSIT_TYPE"].ToString();
                        hidRECEIPT_MODE.Value = Request.QueryString["RECEIPT_MODE"].ToString();//Added for itrack- 1495
                        this.BindDownPaymentModeDD();
                       
                    }
                    //Modifid To display the Receipt Date for co-insurance deposit as discussed by Anurag
                    //Comment to Display The Payment date for all Deposit Type - as for the itrack - 1515
                    //if (hidDEPOSIT_TYPE1.Value.ToString() == "14831" || hidDEPOSIT_TYPE1.Value.ToString() == "14832")//Deposit Type Normal 
                        txtPAYMENT_DATE.Text = DateTime.Now.ToShortDateString();
                    hidCD_LINE_ITEM_ID.Value = "NEW";
                    btnDelete.Enabled = false;
                    btnGenerateReceipt.Visible = false;
                    btnGenerateReceipt.Text = ClsMessages.GetMessage(base.ScreenId, "34");
                    //cmbPAY_MODE.SelectedIndex = cmbPAY_MODE.Items.IndexOf(cmbPAY_MODE.Items.FindByValue("14692"));//Display Default Boleto
                    
                }
                this.SetCaptions();
                strRowId = "NEW";
            }
            
        }
        /// <summary>
        /// Contain Appove and refund value in dropdown
        /// </summary>
        private void BindApproveRefundDD()
        {
            cmbAPPROVE_REFUND.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACCEPT");
            cmbAPPROVE_REFUND.DataTextField = "LookupDesc";
            cmbAPPROVE_REFUND.DataValueField = "LookupCode";
            cmbAPPROVE_REFUND.DataBind();
            cmbAPPROVE_REFUND.Items.Insert(0, "");
            
        }
        /// <summary>
        /// Bind Leader Installment No for co-Insurance 
        /// </summary>
        private void BindLeaderInstallmentNo(ClsAddDepositDetailsinfo objaddDepositDetailsinfo)
        {
          DataSet ds= ObjDepositDetails.GetInstallmentDetails(objaddDepositDetailsinfo.CUSTOMER_ID.CurrentValue,
                objaddDepositDetailsinfo.POLICY_ID.CurrentValue,
                objaddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue, 0,"COINS_DEPOSIT");
          cmbINSTALLMENT.Items.Clear();
          cmbINSTALLMENT.SelectedIndex = -1;

          cmbINSTALLMENT.DataSource = ds;
          cmbINSTALLMENT.DataTextField = "INSTALLMENT";
          cmbINSTALLMENT.DataValueField = "INSTALLMENT_NO";
          cmbINSTALLMENT.DataBind();
          cmbINSTALLMENT.Items.Insert(0, "");
          cmbINSTALLMENT.SelectedIndex = cmbINSTALLMENT.Items.IndexOf(cmbINSTALLMENT.Items.FindByValue(objaddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue.ToString()));
        }
        /// <summary>
        /// Bind Leader Endoresment No for Co-Insurance  
        /// </summary>
        private void BindLeaderEndoresmentNo(ClsAddDepositDetailsinfo objaddDepositDetailsinfo)
        {
            DataSet ds = ObjDepositDetails.GetInstallmentDetailsForCoInsurance(objaddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue, "COINS_DEPOSIT");
            cmbLEADER_ENDORSEMENT_NUMBER.Items.Clear();
            cmbLEADER_ENDORSEMENT_NUMBER.SelectedIndex = -1;

            cmbLEADER_ENDORSEMENT_NUMBER.DataSource = ds.Tables[0];
            cmbLEADER_ENDORSEMENT_NUMBER.DataTextField = "CO_ENDORSEMENT_NO";
            cmbLEADER_ENDORSEMENT_NUMBER.DataValueField = "POLICY_VERSION_ID";
            cmbLEADER_ENDORSEMENT_NUMBER.DataBind();
            cmbLEADER_ENDORSEMENT_NUMBER.Items.Insert(0, "");
            cmbLEADER_ENDORSEMENT_NUMBER.SelectedIndex = cmbLEADER_ENDORSEMENT_NUMBER.Items.IndexOf(cmbLEADER_ENDORSEMENT_NUMBER.Items.FindByValue(objaddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue.ToString()));

        }
        /// <summary>
        /// Bind Receipt from   
        /// </summary>
        private void BindBindReceiptfrom(ClsAddDepositDetailsinfo objaddDepositDetailsinfo)
        {
            DataSet ds = ObjDepositDetails.GetInstallmentDetails(objaddDepositDetailsinfo.CUSTOMER_ID.CurrentValue,
                objaddDepositDetailsinfo.POLICY_ID.CurrentValue,
                objaddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue,0 ,"REFUND_DEPOSIT");
            cmbRECEIPT_FROM_ID.Items.Clear();
            cmbRECEIPT_FROM_ID.SelectedIndex = -1;
            int TableNo = 0;
            switch (hidDEPOSIT_TYPE1.Value.ToString()) 
            {
                case "14916": //Broker Refund
                    TableNo = 0;
                    break;
                case "14917": //Reinsurance Refund
                    TableNo = 1;
                    break;
                case "14918": //Ceded CO Refund
                    TableNo = 2;
                    break;
                default:
                    TableNo = 0;
                    break;
            }
            cmbRECEIPT_FROM_ID.DataSource = ds.Tables[TableNo];
            cmbRECEIPT_FROM_ID.DataTextField = "RECEIPT_FROM_NAME";
            cmbRECEIPT_FROM_ID.DataValueField = "RECEIPT_FROM_ID";
            cmbRECEIPT_FROM_ID.DataBind();
            cmbRECEIPT_FROM_ID.Items.Insert(0, "");
            cmbRECEIPT_FROM_ID.SelectedIndex = cmbRECEIPT_FROM_ID.Items.IndexOf(cmbRECEIPT_FROM_ID.Items.FindByValue(objaddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue.ToString()));

        }
        /// <summary>
        /// Bind Receipt from   
        /// </summary>
        private void BindEndorsement(ClsAddDepositDetailsinfo objaddDepositDetailsinfo)
        {
            DataSet ds = ObjDepositDetails.GetInstallmentDetailsForCoInsurance(objaddDepositDetailsinfo.POLICY_NUMBER.CurrentValue,
                               "REFUND_DEPOSIT");
            cmbPOLICY_DESCRIPTION.DataSource = ds.Tables[0];
            cmbPOLICY_DESCRIPTION.DataTextField = "POLICY_DESCRIPTION";
            cmbPOLICY_DESCRIPTION.DataValueField = "POLICY_VERSION_ID";
            cmbPOLICY_DESCRIPTION.DataBind();
            cmbPOLICY_DESCRIPTION.Items.Insert(0, "");
            cmbPOLICY_DESCRIPTION.SelectedIndex = cmbPOLICY_DESCRIPTION.Items.IndexOf(cmbPOLICY_DESCRIPTION.Items.FindByValue(objaddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue.ToString()));

        }
        /// <summary>
        /// Bind Policy Installment No for Refund
        /// </summary>
        private void BindPolicyInstallmentNo(ClsAddDepositDetailsinfo objaddDepositDetailsinfo)
        {
        

            DataSet ds = ObjDepositDetails.GetInstallmentDetails(objaddDepositDetailsinfo.CUSTOMER_ID.CurrentValue,
                  objaddDepositDetailsinfo.POLICY_ID.CurrentValue,
                  objaddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue,0, "COINS_DEPOSIT");
            cmbPOLICY_INSTALLMENT.DataSource = ds;
            cmbPOLICY_INSTALLMENT.DataTextField = "INSTALLMENT";
            cmbPOLICY_INSTALLMENT.DataValueField = "INSTALLMENT_NO";
            cmbPOLICY_INSTALLMENT.DataBind();
            cmbPOLICY_INSTALLMENT.Items.Insert(0, "");
            cmbPOLICY_INSTALLMENT.SelectedIndex = cmbPOLICY_INSTALLMENT.Items.IndexOf(cmbPOLICY_INSTALLMENT.Items.FindByValue(objaddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue.ToString()));
        }
        /// <summary>
        /// Get the Boleto tolerance Limit
        /// </summary>
        private void GetSysParam()
        {
            String retValue = String.Empty;
            ClsDepositDetails objDepositDetails = new ClsDepositDetails();
            
            DataSet ds = objDepositDetails.GetSysParm();
           
            //retValue = objDepositDetails.GetSysParm();

            System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
            if (GetSYSBaseCurrency() == enumCurrencyId.BR)
            {nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
             nfi.NumberDecimalDigits = 2;}
            else{nfi = new CultureInfo(enumCulture.US, true).NumberFormat;
                nfi.NumberDecimalDigits = 2;}

            if (ds.Tables[0].Rows.Count > 0)
            {
                retValue = ds.Tables[0].Rows[0]["BOLETO_TOLERANCE_LIMIT"].ToString();
                hidCOACC_TOLERANCE_LIMIT_PERCENTAGE.Value = Convert.ToDouble(ds.Tables[0].Rows[0]["COACC_TOLERANCE_LIMIT_PERCENTAGE"].ToString()).ToString("N", nfi);
                hidCOACC_TOLERANCE_LIMIT_AMOUNT.Value = Convert.ToDouble(ds.Tables[0].Rows[0]["COACC_TOLERANCE_LIMIT_AMOUNT"].ToString()).ToString("N",nfi);
            }

            hidBOLETO_TOLERANCE_LIMIT.Value = Convert.ToDouble(retValue).ToString("N", nfi);
            Thread.CurrentThread.CurrentCulture = oldculture;
            
        }
       
        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="Vehicle_ID"></param>
        private void GetOldDataObject(Int32 CD_LINE_ITEM_ID)
        {
            ClsAddDepositDetailsinfo  objAddDepositDetailsinfo = new ClsAddDepositDetailsinfo();

            objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue = CD_LINE_ITEM_ID;
            objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue = int.Parse(hidDEPOSIT_ID.Value);
            objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue = int.Parse(hidDEPOSIT_NUMBER.Value);
          
            if (ObjDepositDetails.FetchDepositLineItemsData(ref objAddDepositDetailsinfo))
            {
                PopulatePageFromEbixModelObject(this.Page, objAddDepositDetailsinfo);
                //capPolicyStatus.Text = objAddDepositDetailsinfo.POLICY_STATUS.CurrentValue.ToString();
                System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;

                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                {
                    nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
                    nfi.NumberDecimalDigits = 2;

                }
                else
                {   nfi = new CultureInfo(enumCulture.US, true).NumberFormat;
                    nfi.NumberDecimalDigits = 2;
                }
                if (hidDEPOSIT_TYPE1.Value.ToString() == "14832")//Co-Insurance 
                {
                    if (objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue.ToString() != "")
                        txtINSTALLMENT_RISK_PREMIUM.Text = objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue.ToString("N", nfi);

                    if (objAddDepositDetailsinfo.FEE.CurrentValue.ToString() != "")
                        txtCO_COMMISSION_AMOUNT.Text = objAddDepositDetailsinfo.FEE.CurrentValue.ToString("N", nfi);

                    if (objAddDepositDetailsinfo.INTEREST.CurrentValue.ToString() != "")
                        txtINTEREST_AMOUNT.Text = objAddDepositDetailsinfo.INTEREST.CurrentValue.ToString("N", nfi);

                    if (objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue.ToString() != "")
                        txtBROKER_COMMISSION_AMOUNT.Text = objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue.ToString("N", nfi);

                    if (objAddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue.ToString() != "")
                        txtLEADER_POLICY_NUMBER.Text = objAddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue.ToString();

                    hidRECEIPT_FROM_ID.Value = objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue.ToString();

                    this.BindLeaderEndoresmentNo(objAddDepositDetailsinfo);
                    this.BindLeaderInstallmentNo(objAddDepositDetailsinfo);


                    if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString() != "")
                    {
                        hidNET_INSTALLMENT_AMOUNT.Value = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);
                        txtNET_INSTALLMENT_AMOUNT.Text = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);
                        txtCOI_TOTAL_PREMIUM_COLLECTION.Text = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);
                    }
                    //Added By Pradeep for as discussed by Anurag
                    if (objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue != -1.0)
                        txtCOI_TOTAL_PREMIUM_COLLECTION.Text = objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue.ToString("N", nfi);
                    else
                        txtCOI_TOTAL_PREMIUM_COLLECTION.Text = "";
                    //Added till here 

                    //Added by Pradeep On 16-Jun-2011 for itrack 1172
                    DataSet dsInstall = null;
                    dsInstall = ObjDepositDetails.GetInstallmentDetails(objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue,
                        objAddDepositDetailsinfo.POLICY_ID.CurrentValue,
                        objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue,
                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue,
                                   "COINS_DEPOSIT");
                    if (dsInstall != null && dsInstall.Tables[0].Rows.Count > 0)
                    {
                        double NET_INSTALLMENT_AMOUNT = 0;
                        //Modified to add - Co Commission Amt to check exception - itrack 1148 as discussed with Anurag      
                        double CO_COMM_AMT = 0;
                        CO_COMM_AMT = Convert.ToDouble(dsInstall.Tables[0].Rows[0]["CO_COMM_AMT"]);

                        NET_INSTALLMENT_AMOUNT = (Convert.ToDouble(dsInstall.Tables[0].Rows[0]["INSTALLMENT_AMOUNT"]) - CO_COMM_AMT
                             + Convert.ToDouble(dsInstall.Tables[0].Rows[0]["INTEREST_AMOUNT"]));
                        hidNET_INSTALLMENT_AMOUNT_ON_UPDATE.Value = NET_INSTALLMENT_AMOUNT.ToString("N", nfi);
                        //Till here 
                    }
                    else
                        hidNET_INSTALLMENT_AMOUNT_ON_UPDATE.Value = "0";
                    if (objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue.ToString().Trim() == "Y")
                    {
                        //Added for itrack 1148 to show exception as discussed by Anurag 
                        if (objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.ToString().Trim() == "" || objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.ToString().Trim() == "N")
                        {
                            if (objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue == 434// --Payment against installment already paid.
                                || objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue == 435//--Payment against installment already  in progress.
                                || objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue == 409)//Change in Original Net Installment Amount.
                            {
                                //14728 - Approve
                                //14729 - Refund
                                if (objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue == 434
                                    || objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue == 435)
                                {
                                    ListItem Li = new ListItem();
                                    Li = cmbAPPROVE_REFUND.Items.FindByValue("A");
                                    cmbAPPROVE_REFUND.Items.Remove(Li);
                                }

                            }
                            btnAccept.Visible = true;
                            trAPPROVE_REFUND.Visible = true;
                        }
                        else
                        {
                            btnAccept.Visible = false;
                            trAPPROVE_REFUND.Visible = false;
                        }
                        //Added till here 
                        hidEXCEPTION.Value = "YES";
                        capIS_EXCEPTION_COINS.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1765");//"Exception";
                    }
                    else
                    {
                        hidEXCEPTION.Value = "NO";
                        capIS_EXCEPTION_COINS.Text = "";
                        //Added for itrack 1148 to show exception as discussed by Anurag 
                        btnAccept.Visible = false;
                        trAPPROVE_REFUND.Visible = false;
                        
                    }
                    txtLEADER_POLICY_NUMBER.Enabled = false;
                    cmbLEADER_ENDORSEMENT_NUMBER.Enabled = false;
                    cmbINSTALLMENT.Enabled = false;
                    //till here 
                }
                //Refund
                // ATUL UPDATE
                else if (hidDEPOSIT_TYPE1.Value.ToString() == "14916"
                    || hidDEPOSIT_TYPE1.Value.ToString() == "14917"
                    || hidDEPOSIT_TYPE1.Value.ToString() == "14918")
                {
                    if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString() != "")
                        txtREFUND_AMOUNT.Text = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);


                    this.BindBindReceiptfrom(objAddDepositDetailsinfo);
                    this.BindEndorsement(objAddDepositDetailsinfo);
                    this.BindPolicyInstallmentNo(objAddDepositDetailsinfo);
                    hidRECEIPT_FROM_ID.Value = objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue.ToString();

                    //Added by Pradeep- for itrack-1049 	
                    if (objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue.ToString().Trim() == "Y")
                    {
                        if (objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.ToString().Trim() == "" || objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.ToString().Trim() == "N")
                        {
                            if (objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue == 443 //--Refund related to this installment was already received  
                                || objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue == 444)//Refund related to this installment was already in progress  
                            {
                                ListItem Li = new ListItem();
                                Li = cmbAPPROVE_REFUND.Items.FindByValue("A");
                                cmbAPPROVE_REFUND.Items.Remove(Li);
                            }
                            btnAccept.Visible = true;
                            trAPPROVE_REFUND.Visible = true;
                        }
                        else
                        {
                            btnAccept.Visible = false;
                            trAPPROVE_REFUND.Visible = false;
                        }
                        hidEXCEPTION.Value = "YES";
                        capExceptionRefund.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1765");//"Exception";
                    }
                    else
                    {
                        hidEXCEPTION.Value = "NO";
                        capExceptionRefund.Text = "";
                        btnAccept.Visible = false;
                        trAPPROVE_REFUND.Visible = false;
                    }
                    //Added till here 
                }
                else
                {
                    if (objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue.ToString() != "")
                        txtRISK_PREMIUM.Text = objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue.ToString("N", nfi);
                    if (objAddDepositDetailsinfo.FEE.CurrentValue.ToString() != "")
                        txtFEE.Text = objAddDepositDetailsinfo.FEE.CurrentValue.ToString("N", nfi);
                    if (objAddDepositDetailsinfo.TAX.CurrentValue.ToString() != "")
                        txtTAX.Text = objAddDepositDetailsinfo.TAX.CurrentValue.ToString("N", nfi);
                    if (objAddDepositDetailsinfo.INTEREST.CurrentValue.ToString() != "")
                        txtINTEREST.Text = objAddDepositDetailsinfo.INTEREST.CurrentValue.ToString("N", nfi);
                    if (objAddDepositDetailsinfo.LATE_FEE.CurrentValue.ToString() != "")
                        txtLATE_FEE.Text = objAddDepositDetailsinfo.LATE_FEE.CurrentValue.ToString("N", nfi);

                    if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString() != "")
                    {
                        txtRECEIPT_AMOUNT.Text = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);
                        hidRECEIPT_AMOUNT.Value = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);
                        txtTOTAL_PREMIUM_COLLECTION.Text = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);
                    }
                    //Added By Pradeep Kushwaha on 24-March-2011 for itrack 913
                    if (objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue != -1.0)
                        txtTOTAL_PREMIUM_COLLECTION.Text = objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue.ToString("N", nfi);
                    else
                        txtTOTAL_PREMIUM_COLLECTION.Text = "";
                    //Added till here 
                    hidTOTAL.Value = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue.ToString("N", nfi);



                    if (objAddDepositDetailsinfo.PAY_MODE.CurrentValue.ToString().Trim() == "14692" || objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue.ToString().Trim() == "14831")
                    {
                        ClsDepositDetails objDepositDetails = new ClsDepositDetails();
                        String[] SplitOurNumberDetails = objDepositDetails.GetInstallmentDetailsUsingOurNumber(objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue.Trim()).Split('^');
                        if (SplitOurNumberDetails[0] != "" && SplitOurNumberDetails.Length > 0)
                            hidTOTAL_VALUE_ON_UPDATE.Value = Convert.ToDouble(SplitOurNumberDetails[7]).ToString("N", nfi);
                        else
                            hidTOTAL_VALUE_ON_UPDATE.Value = "0";

                    }
                    Thread.CurrentThread.CurrentCulture = oldculture;
                    txtOUR_NUMBER.Enabled = false;
                    txtPOLICY_NUMBER.Enabled = false;
                    cmbPOLICY_DESCRIPTION.Enabled = false;
                    cmbPOLICY_INSTALLMENT.Enabled = false;
                    cmbRECEIPT_FROM_ID.Enabled = false;
                    txtLEADER_POLICY_NUMBER.Enabled = false;
                    cmbLEADER_ENDORSEMENT_NUMBER.Enabled = false;
                    cmbINSTALLMENT.Enabled = false;
                    if (objAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue.ToString().Trim() != "")
                        btnGenerateReceipt.Text = ClsMessages.GetMessage(base.ScreenId, "33");
                    else
                        btnGenerateReceipt.Text = ClsMessages.GetMessage(base.ScreenId, "34");

                    Boolean IsApproveOrRefuncd = false;
                    //Pay mode Boleto
                    if ((objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue.ToString().Trim() == "14916" ||
                        objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue.ToString().Trim() == "14917" ||
                        objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue.ToString().Trim() == "14918")
                     && objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue.ToString().Trim() == "Y")
                    {
                        hidEXCEPTION.Value = "YES";
                        capExceptionRefund.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1765");//"Exception";
                        hidEXCEPTION_REASON.Value = objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString();

                    }

                    else if ((objAddDepositDetailsinfo.PAY_MODE.CurrentValue.ToString().Trim() == "14692"
                       || objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue.ToString().Trim() == "14831")
                       && objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue.ToString().Trim() == "Y")
                    {

                        hidEXCEPTION.Value = "YES";
                        capIS_EXCEPTION.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1765");//"Exception";

                        hidIS_APPROVE.Value = objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.Trim().ToString();

                        hidEXCEPTION_REASON.Value = objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString();

                        cmbAPPROVE_REFUND.SelectedIndex = cmbAPPROVE_REFUND.Items.IndexOf(cmbAPPROVE_REFUND.Items.FindByValue(objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.ToString().Trim()));

                        if (objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.ToString().Trim() == "" || objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.ToString().Trim() == "N")
                        {
                            if (objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue.ToString().Trim() == "Y" &&
                                (objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString().Trim() == "294" ||
                                objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString().Trim() == "408" ||
                                objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString().Trim() == "293" ||
                                objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString().Trim() == "395"))  //395-Our Number Not Found in Database
                            {
                                //14728 - Approve
                                //14729 - Refund
                                if (objAddDepositDetailsinfo.IS_COMMITED.CurrentValue.ToString().Trim() == "Y" ||
                                    objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString().Trim() == "293" ||
                                    objAddDepositDetailsinfo.IS_ACTIVE.CurrentValue.ToString().Trim() == "N" ||
                                    objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue.ToString().Trim() == "395")
                                {
                                    ListItem Li = new ListItem();
                                    Li = cmbAPPROVE_REFUND.Items.FindByValue("A");
                                    cmbAPPROVE_REFUND.Items.Remove(Li);
                                }
                                /* COMMNETED BY ATUL
                                //ListItem Li = new ListItem();
                                //Li = cmbAPPROVE_REFUND.Items.FindByValue("A");
                                //cmbAPPROVE_REFUND.Items.Remove(Li);*/
                            }
                            btnAccept.Visible = true;
                            trAPPROVE_REFUND.Visible = true;
                        }
                        else
                        {
                            btnAccept.Visible = false;
                            trAPPROVE_REFUND.Visible = false;
                            IsApproveOrRefuncd = true;
                        }
                    }
                    else
                    {
                        hidEXCEPTION.Value = "NO";
                        capIS_EXCEPTION.Text = "";
                        btnAccept.Visible = false;
                        trAPPROVE_REFUND.Visible = false;
                    }

                    if (objAddDepositDetailsinfo.PAY_MODE.CurrentValue.ToString().Trim() == "14692" || objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue.ToString().Trim() == "14831")
                    {
                        if (objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue == 1)
                        {
                            if (hidEXCEPTION.Value == "YES" && IsApproveOrRefuncd == true)
                                btnGenerateReceipt.Visible = true;
                            else if (hidEXCEPTION.Value != "YES" && IsApproveOrRefuncd == false)
                                btnGenerateReceipt.Visible = true;
                            else
                                btnGenerateReceipt.Visible = false;

                        }
                        else
                            btnGenerateReceipt.Visible = false;

                    }
                    else
                        btnGenerateReceipt.Visible = true;
                }//Modifed for itrack 1148 as discussed by Anurag
                base.SetPageModelObject(objAddDepositDetailsinfo);
            }//  if (ObjDepositDetails.FetchDepositLineItemsData(ref objAddDepositDetailsinfo))
            btnGenerateReceipt.Visible = false;//Added for itrack-913 to hide the generate receipt button

        }//private void GetOldDataObject(Int32 Vehicle_ID)

        /// <summary>
        /// User to bind the downpayment mode option in dropdown
        /// </summary>
        private void BindDownPaymentModeDD()
        {
            //cmbPAY_MODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CDCD");
            //cmbPAY_MODE.DataTextField = "LookupDesc";
            //cmbPAY_MODE.DataValueField = "LookupID";
            //cmbPAY_MODE.DataBind();
            //ListItem Li = new ListItem();
            //Li = cmbPAY_MODE.Items.FindByValue("2884"); // Remove option : "Already processed"
            //cmbPAY_MODE.Items.Remove(Li);
            
            //if (hidDEPOSIT_TYPE.Value.ToString() == "14832"
            //    || hidDEPOSIT_TYPE.Value.ToString() == "14916"
            //    || hidDEPOSIT_TYPE.Value.ToString() == "14917"
            //    || hidDEPOSIT_TYPE.Value.ToString() == "14918")//Co-Insurance,Broker Refund,Reinsurance Refund and Ceded CO Refund
            //{
            //    ListItem Lit = new ListItem();
            //    Lit = cmbPAY_MODE.Items.FindByValue("14692");//Boleto
            //    cmbPAY_MODE.Items.Remove(Lit);
            //    cmbPAY_MODE.Items.Insert(0, "");
            //}
            //else
            //{
            //    ListItem Lit = new ListItem();
            //    Lit = cmbPAY_MODE.Items.FindByValue("2883");//Boleto
            //    cmbPAY_MODE.Items.Remove(Lit);
            //    ListItem Lit1 = new ListItem();
            //    Lit1 = cmbPAY_MODE.Items.FindByValue("14693");//Boleto
            //    cmbPAY_MODE.Items.Remove(Lit1);
            //}
           
           
        }
        /// <summary>
        /// Use to set the error messages on the controls for validation  
        /// </summary>
        private void SetErrorMessages()
        {
          
            //rfvPAY_MODE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "9");
            rfvOUR_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "10");
            rfvPOLICY_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "11");
            //rfvBANK_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "12");
            //rfvBANK_AGENCY_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "13");
            //rfvBANK_ACCOUNT_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "14");
            rfvRISK_PREMIUM.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "15");
            rfvRECEIPT_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "20");
            rfvAPPROVE_REFUND.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "35");
            rfvTOTAL_PREMIUM_COLLECTION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "37");
            rfvLEADER_POLICY_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "38");
            rfvLEADER_ENDORSEMENT_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "39");
            rfvINSTALLMENT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "40");
            rfvINSTALLMENT_RISK_PREMIUM.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "41");

            revINSTALLMENT_RISK_PREMIUM.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency           
            revINSTALLMENT_RISK_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            rfvPOLICY_DESCRIPTION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "43");//"Please Select Policy Version.";
            rfvPOLICY_INSTALLMENT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "44");//"Please Select Policy Installment.";
            //rfvRECEIPT_FROM_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "45");//"Please Select the Name.";
            rfvREFUND_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "46");//;"Please enter Refund Amount.";
 
            revREFUND_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency           
            revREFUND_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revINTEREST_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency           
            revINTEREST_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revCO_COMMISSION_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency           
            revCO_COMMISSION_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revBROKER_COMMISSION_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency           
            revBROKER_COMMISSION_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revNET_INSTALLMENT_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency           
            revNET_INSTALLMENT_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            //revPOLICY_NUMBER.ValidationExpression = aRegExpInteger;
            //revPOLICY_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "21");

            //revOUR_NUMBER.ValidationExpression = aRegExpInteger;
            //revOUR_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "22");


            //revBANK_NUMBER.ValidationExpression = aRegExpInteger;
            //revBANK_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "23");

            //revBANK_AGENCY_NUMBER.ValidationExpression = aRegExpInteger;
            //revBANK_AGENCY_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "24");

            //revBANK_ACCOUNT_NUMBER.ValidationExpression = aRegExpInteger;
            //revBANK_ACCOUNT_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "25");

            revRISK_PREMIUM.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency           
            revRISK_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revFEE.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrency;
            revFEE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revTAX.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat; // aRegExpCurrency;
            revTAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");


            revINTEREST.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat;//aRegExpCurrency;
            revINTEREST.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");


            revLATE_FEE.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat;//aRegExpCurrency;
            revLATE_FEE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revRECEIPT_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat;//aRegExpCurrency;
            revRECEIPT_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revTOTAL_PREMIUM_COLLECTION.ValidationExpression = aRegExpBaseCurrencyformat;
            revTOTAL_PREMIUM_COLLECTION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            
            hidPolicyNumberMsg.Value = ClsMessages.GetMessage(base.ScreenId, "26");
            hidOurNumberMsg.Value = ClsMessages.GetMessage(base.ScreenId, "27");
            hidLeaderPolicyNumberMsg.Value = ClsMessages.GetMessage(base.ScreenId, "42");

            rfvPAYMENT_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "47");
            revPAYMENT_DATE.ValidationExpression = aRegExpDate;
            revPAYMENT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            csvPAYMENT_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "48");
            
            //Added for Co-Insurance  Coi total premium collection 
            rfvCOI_TOTAL_PREMIUM_COLLECTION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "37");
            revCOI_TOTAL_PREMIUM_COLLECTION.ValidationExpression = aRegExpBaseCurrencyformat;
            revCOI_TOTAL_PREMIUM_COLLECTION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
            //Added till here 

            //Added three more fields on Customer Receipt as per the itrack -  1495-
            rfvBANK_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "12");
            rfvBANK_AGENCY_NUMBER.ErrorMessage =ClsMessages.GetMessage(base.ScreenId, "13");
            rfvBANK_ACCOUNT_NUMBER.ErrorMessage =ClsMessages.GetMessage(base.ScreenId, "14");
            revBANK_NUMBER.ValidationExpression = aRegExpInteger;
            revBANK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102");
            revBANK_AGENCY_NUMBER.ValidationExpression = aRegExpBankBranchNumber; 
            revBANK_AGENCY_NUMBER.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "50");
            revBANK_ACCOUNT_NUMBER.ValidationExpression = aRegExpInteger;
            revBANK_ACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102");
            //Added till here 

        }
        #region setcaption in resource file
        private void SetCaptions()
        {
            capMandatoryNotes.Text = ClsMessages.FetchGeneralMessage("1168");
           // capDownPayMentMode.Text = objResourceMgr.GetString("cmbPAY_MODE");
            capBOLETO_NO.Text = objResourceMgr.GetString("txtBOLETO_NO");
            capPOLICY_NUMBER.Text = objResourceMgr.GetString("txtPOLICY_NUMBER");
            capRISK_PREMIUM.Text = objResourceMgr.GetString("txtRISK_PREMIUM");
            capFEE.Text = objResourceMgr.GetString("txtFEE");
            capTAX.Text = objResourceMgr.GetString("txtTAX");
            capINTEREST.Text = objResourceMgr.GetString("txtINTEREST");
            capLATE_FEE.Text = objResourceMgr.GetString("txtLATE_FEE");
            capRECEIPT_AMOUNT.Text = objResourceMgr.GetString("txtRECEIPT_AMOUNT");
            //capBANK_NUMBER.Text = objResourceMgr.GetString("txtBANK_NUMBER");
            //capBANK_AGENCY_NUMBER.Text = objResourceMgr.GetString("txtBANK_AGENCY_NUMBER");
            //capBANK_ACCOUNT_NUMBER.Text = objResourceMgr.GetString("txtBANK_ACCOUNT_NUMBER");
            capAPPROVE_REFUND.Text = objResourceMgr.GetString("cmbAPPROVE_REFUND");
            hidExceptionMsg.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1765");
            capTOTAL_PREMIUM_COLLECTION.Text= objResourceMgr.GetString("txtTOTAL_PREMIUM_COLLECTION");
            btnAccept.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1805");
            capLEADER_POLICY_NUMBER.Text = objResourceMgr.GetString("txtLEADER_POLICY_NUMBER");
            capPOLICY_DESCRIPTION.Text = objResourceMgr.GetString("capPOLICY_DESCRIPTION");
            capREFUND_AMOUNT.Text = objResourceMgr.GetString("capREFUND_AMOUNT");
            capPOLICY_INSTALLMENT.Text = objResourceMgr.GetString("capPOLICY_INSTALLMENT");
            capCO_COMMISSION_AMOUNT.Text = objResourceMgr.GetString("capCO_COMMISSION_AMOUNT");
            capINSTALLMENT.Text = objResourceMgr.GetString("capINSTALLMENT");
            capINSTALLMENT_RISK_PREMIUM.Text = objResourceMgr.GetString("capINSTALLMENT_RISK_PREMIUM");
            capINTEREST_AMOUNT.Text = objResourceMgr.GetString("capINTEREST_AMOUNT");
            capNET_INSTALLMENT_AMOUNT.Text = objResourceMgr.GetString("capNET_INSTALLMENT_AMOUNT");
            capBROKER_COMMISSION_AMOUNT.Text = objResourceMgr.GetString("capBROKER_COMMISSION_AMOUNT");
            capLEADER_ENDORSEMENT_NUMBER.Text = objResourceMgr.GetString("capLEADER_ENDORSEMENT_NUMBER");
            capPAYMENT_DATE.Text = objResourceMgr.GetString("txtPAYMENT_DATE");

            capBANK_NUMBER.Text = objResourceMgr.GetString("txtBANK_NUMBER");
            capBANK_AGENCY_NUMBER.Text = objResourceMgr.GetString("txtBANK_AGENCY_NUMBER");
            capBANK_ACCOUNT_NUMBER.Text = objResourceMgr.GetString("txtBANK_ACCOUNT_NUMBER");
            
            capCOI_TOTAL_PREMIUM_COLLECTION.Text = objResourceMgr.GetString("txtTOTAL_PREMIUM_COLLECTION");//caption added Total Premium collection

            switch (hidDEPOSIT_TYPE1.Value) {
                case "14916": //Broker Refund
                    capRECEIPT_FROM_ID.Text = objResourceMgr.GetString("cmbBrokerName");// "Broker Name.";
                    rfvRECEIPT_FROM_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2069");//added by abhinav
                    break;
                case "14917": //Reinsurance Refund
                    capRECEIPT_FROM_ID.Text = objResourceMgr.GetString("cmbReinsuranceName"); //"Reinsurance Name.";
                    rfvRECEIPT_FROM_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2068");//added by abhinav
                    break;
                case "14918": //Ceded CO Refund
                    capRECEIPT_FROM_ID.Text = objResourceMgr.GetString("cmbFollowerName"); //"Follower Name.";
                    rfvRECEIPT_FROM_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2070");//added by abhinav
                    break;
                default:
                    break;
            }

        }
        #endregion
        [System.Web.Services.WebMethod]
        public static string GetInstallmentDetails(String OUR_NUMBER)
        {
            String retValue = String.Empty;
            ClsDepositDetails objDepositDetails = new ClsDepositDetails();

            retValue = objDepositDetails.GetInstallmentDetailsUsingOurNumber(OUR_NUMBER);
            
            return retValue;
        }
        /// <summary>
        /// Get the POlicy Number based on Custome id , policy id , policy version id
        /// </summary>
        /// <param name="CUSTOMER_ID"></param>
        /// <param name="POLICY_ID"></param>
        /// <param name="POLICY_VERSION_ID"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string GetPolicyStatus(String CUSTOMER_ID, String POLICY_ID, String POLICY_VERSION_ID)
        {
            String retValue = String.Empty;
            ClsDepositDetails objDepositDetails = new ClsDepositDetails();

            retValue= objDepositDetails.GetPolicyStatus(int.Parse(CUSTOMER_ID),int.Parse(POLICY_ID),int.Parse(POLICY_VERSION_ID));
            return retValue;
        }
        /// <summary>
        /// get the Leader policy details
        /// </summary>
        /// <param name="POLICY_NUMBER"></param>
        /// <returns></returns>
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxLeaderPolicyDetailsInfo(String LEADER_POLICY_NUMBER)
        {
            try
            {
            DataSet ds = null;
            ClsDepositDetails objDepositDetails = new ClsDepositDetails();
            ds= objDepositDetails.GetInstallmentDetailsForCoInsurance(LEADER_POLICY_NUMBER,
                           "COINS_DEPOSIT");

                return ds;
            }
            
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// get the policy details
        /// </summary>
        /// <param name="POLICY_NUMBER"></param>
        /// <returns></returns>
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxPolicyDetailsInfo(String POLICY_NUMBER)
        {
            try
            {
                DataSet ds = null;
                ClsDepositDetails objDepositDetails = new ClsDepositDetails();
                ds = objDepositDetails.GetInstallmentDetailsForCoInsurance(POLICY_NUMBER,
                               "REFUND_DEPOSIT");

                return ds;
            }

            catch
            {
                return null;
            }
        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxGetInstallmentDetailsInfo(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int INSTALLMENT_NO)
        {
            try
            {
            DataSet ds = null;
            ClsDepositDetails objDepositDetails = new ClsDepositDetails();
            ds = objDepositDetails.GetInstallmentDetails(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, INSTALLMENT_NO,
                           "COINS_DEPOSIT");

                return ds;
            }
            
            catch
            {
                return null;
            }
        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxGetInsurerInfo(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            try
            {
                DataSet ds = null;
                ClsDepositDetails objDepositDetails = new ClsDepositDetails();
                ds = objDepositDetails.GetInstallmentDetails(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, 0,
                               "REFUND_DEPOSIT");

                return ds;
            }

            catch
            {
                return null;
            }
        }
        /// <summary>
        /// get the policy Status
        /// </summary>
        /// <param name="POLICY_NUMBER"></param>
        /// <returns></returns>
         [System.Web.Services.WebMethod]
        public static string GetPolicyDetails(String POLICY_NUMBER)
        {
            String retValue = String.Empty;
            ClsDepositDetails objDepositDetails = new ClsDepositDetails();

            retValue = objDepositDetails.GetPolicyDetailsData(POLICY_NUMBER);
            return retValue;
        }
        
         /// <summary>
        /// Use to set the form controls values in page model
        /// </summary>
        /// <param name="ObjCivilTransportVehicleInfo"></param>
        private void GetFormValue(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)
        {
            System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;

            if (GetSYSBaseCurrency() == enumCurrencyId.BR)
            {
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
                nfi.NumberDecimalDigits = 2;

            }
            else
            {
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;
                nfi.NumberDecimalDigits = 2;
            }
            switch(hidDEPOSIT_TYPE1.Value.ToString())
            {
                case "14831"://For normal deposit
                    #region Normal Deposit
                    if (txtRISK_PREMIUM.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = Convert.ToDouble(txtRISK_PREMIUM.Text, nfi);
                    else
                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (txtFEE.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.FEE.CurrentValue = Convert.ToDouble(txtFEE.Text, nfi);
                    else
                        objAddDepositDetailsinfo.FEE.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (txtTAX.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.TAX.CurrentValue = Convert.ToDouble(txtTAX.Text, nfi);
                    else
                        objAddDepositDetailsinfo.TAX.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (txtINTEREST.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.INTEREST.CurrentValue = Convert.ToDouble(txtINTEREST.Text, nfi);
                    else
                        objAddDepositDetailsinfo.INTEREST.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (txtLATE_FEE.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.LATE_FEE.CurrentValue = Convert.ToDouble(txtLATE_FEE.Text, nfi);
                    else
                        objAddDepositDetailsinfo.LATE_FEE.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (hidRECEIPT_AMOUNT.Value.ToString().Trim() != "")
                        objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(hidRECEIPT_AMOUNT.Value, nfi);
                    else
                        objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue();

                    //Added By Pradeep Kushwaha on 24-March-2011 for itrack 913
                    if (txtTOTAL_PREMIUM_COLLECTION.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Convert.ToDouble(txtTOTAL_PREMIUM_COLLECTION.Text, nfi);
                    else
                        objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = base.GetEbixDoubleDefaultValue();
    
                    objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue = 0.0;
                    if (hidINSTALLMENT_NO.Value != "")
                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = int.Parse(hidINSTALLMENT_NO.Value);
                    else
                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = base.GetEbixIntDefaultValue();
                    objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue = base.GetEbixIntDefaultValue();

              
                    if (txtPAYMENT_DATE.Text.ToString() != "")
                        objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue = ConvertToDate(txtPAYMENT_DATE.Text);
                    else
                        objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    //Added three more fields on Customer Receipt as per the itrack -  1495 (Date 17-08-2011)
                    if (txtBANK_NUMBER.Text.ToString() != "")
                        objAddDepositDetailsinfo.BANK_NUMBER.CurrentValue = txtBANK_NUMBER.Text;
                    else
                        objAddDepositDetailsinfo.BANK_NUMBER.CurrentValue = String.Empty;

                    if (txtBANK_AGENCY_NUMBER.Text.ToString() != "")
                        objAddDepositDetailsinfo.BANK_AGENCY_NUMBER.CurrentValue = txtBANK_AGENCY_NUMBER.Text;
                    else
                        objAddDepositDetailsinfo.BANK_AGENCY_NUMBER.CurrentValue = String.Empty;

                    if (txtBANK_ACCOUNT_NUMBER.Text.ToString() != "")
                        objAddDepositDetailsinfo.BANK_ACCOUNT_NUMBER.CurrentValue = txtBANK_ACCOUNT_NUMBER.Text;
                    else
                        objAddDepositDetailsinfo.BANK_ACCOUNT_NUMBER.CurrentValue = String.Empty;
                    
                    //Added till here 
                    #endregion
                    break;
                case "14832":
                    #region Co-Insurance
                    if (txtINSTALLMENT_RISK_PREMIUM.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = Convert.ToDouble(txtINSTALLMENT_RISK_PREMIUM.Text, nfi);
                    else
                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (txtCO_COMMISSION_AMOUNT.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.FEE.CurrentValue = Convert.ToDouble(txtCO_COMMISSION_AMOUNT.Text, nfi);
                    else
                        objAddDepositDetailsinfo.FEE.CurrentValue = base.GetEbixDoubleDefaultValue();

                    objAddDepositDetailsinfo.TAX.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (txtINTEREST_AMOUNT.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.INTEREST.CurrentValue = Convert.ToDouble(txtINTEREST_AMOUNT.Text, nfi);
                    else
                        objAddDepositDetailsinfo.INTEREST.CurrentValue = base.GetEbixDoubleDefaultValue();
                    
                    if (txtBROKER_COMMISSION_AMOUNT.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue = Convert.ToDouble(txtBROKER_COMMISSION_AMOUNT.Text, nfi);
                    else
                        objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue();

                    
                    objAddDepositDetailsinfo.LATE_FEE.CurrentValue = base.GetEbixDoubleDefaultValue();

                    if (hidNET_INSTALLMENT_AMOUNT.Value.ToString().Trim() != "")
                        objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(hidNET_INSTALLMENT_AMOUNT.Value, nfi);
                    else
                        objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue();

                    objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = base.GetEbixDoubleDefaultValue();


                    if (txtLEADER_POLICY_NUMBER.Text.Trim().ToString() != "")
                        objAddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue = txtLEADER_POLICY_NUMBER.Text.Trim();
                    else
                        objAddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue = String.Empty;

                    if (hidLEADER_ENDORSEMENT_NUMBER.Value != "")
                        objAddDepositDetailsinfo.LEADER_DOC_ID.CurrentValue =  hidLEADER_ENDORSEMENT_NUMBER.Value;
                    else
                        objAddDepositDetailsinfo.LEADER_DOC_ID.CurrentValue = String.Empty;
                    
                    
                    if (hidINSTALLMENT_NO.Value != "")
                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = int.Parse(hidINSTALLMENT_NO.Value);
                    else
                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = base.GetEbixIntDefaultValue();
                    objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue = base.GetEbixIntDefaultValue();

                    if (hidRECEIPT_FROM_ID.Value != "")
                        objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue = int.Parse(hidRECEIPT_FROM_ID.Value);
                    else
                        objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue = base.GetEbixIntDefaultValue();
                    //Display payment receipt date for Co-Insurance As discueed by Anurag itrack 1148
                    if (txtPAYMENT_DATE.Text.ToString() != "")
                        objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue = ConvertToDate(txtPAYMENT_DATE.Text);
                    else
                        objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    //Added till Here 

                    //Added By Pradeep Co-Insurance As discueed by Anurag itrack 1148
                    if (txtCOI_TOTAL_PREMIUM_COLLECTION.Text.ToString().Trim() != "")
                        objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Convert.ToDouble(txtCOI_TOTAL_PREMIUM_COLLECTION.Text, nfi);
                    else
                        objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = base.GetEbixDoubleDefaultValue();
                    //Added till here 
    

                    #endregion
                    break;
                case "14916": //Broker Refund
                case "14917": //Reinsurance Refund
                case "14918": //Ceded CO Refund
                    #region  Broker Refund,Reinsurance Refund,Ceded CO Refund
                    objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = base.GetEbixDoubleDefaultValue();
                    objAddDepositDetailsinfo.FEE.CurrentValue = base.GetEbixDoubleDefaultValue();
                    objAddDepositDetailsinfo.TAX.CurrentValue = base.GetEbixDoubleDefaultValue();
                    objAddDepositDetailsinfo.LATE_FEE.CurrentValue = base.GetEbixDoubleDefaultValue();
                    if (txtREFUND_AMOUNT.Text.ToString().Trim() != "")
                    {
                        objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(txtREFUND_AMOUNT.Text, nfi);
                        objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Convert.ToDouble(txtREFUND_AMOUNT.Text, nfi);
                    }
                    else
                    {
                        objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue();
                        objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Convert.ToDouble(txtREFUND_AMOUNT.Text, nfi);
                    }
                    
                    objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue = 0.0;
                    if (hidINSTALLMENT_NO.Value != "")
                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = int.Parse(hidINSTALLMENT_NO.Value);
                    else
                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = base.GetEbixIntDefaultValue();
                    if (hidRECEIPT_FROM_ID.Value != "")
                        objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue = int.Parse(hidRECEIPT_FROM_ID.Value);
                    else
                        objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue = base.GetEbixIntDefaultValue();

                    //Display payment receipt date for Broker Refund,Reinsurance Refund and Ceded CO Refund
                    if (txtPAYMENT_DATE.Text.ToString() != "")
                        objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue = ConvertToDate(txtPAYMENT_DATE.Text);
                    else
                        objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    //Added till Here 
                    #endregion
                    break;
                default:
                    #region
                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = base.GetEbixDoubleDefaultValue();
                        objAddDepositDetailsinfo.FEE.CurrentValue = base.GetEbixDoubleDefaultValue();
                        objAddDepositDetailsinfo.TAX.CurrentValue = base.GetEbixDoubleDefaultValue();
                        objAddDepositDetailsinfo.LATE_FEE.CurrentValue = base.GetEbixDoubleDefaultValue();
                        if (txtREFUND_AMOUNT.Text.ToString().Trim() != "")
                            objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(txtREFUND_AMOUNT.Text, nfi);
                        else
                            objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue();
                   
                        objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = base.GetEbixDoubleDefaultValue();
                        objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue = 0.0;
                        if (hidINSTALLMENT_NO.Value != "")
                            objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = int.Parse(hidINSTALLMENT_NO.Value);
                        else
                            objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = base.GetEbixIntDefaultValue();

                    break;
                    #endregion
            }
            

            //Added till here 
            Thread.CurrentThread.CurrentCulture = oldculture;

            if (hidBOLETO_NO.Value.ToString().Trim() != "" && hidBOLETO_NO.Value.ToString().Trim() != "0")
                objAddDepositDetailsinfo.BOLETO_NO.CurrentValue = int.Parse(hidBOLETO_NO.Value);
            else
                objAddDepositDetailsinfo.BOLETO_NO.CurrentValue = base.GetEbixIntDefaultValue();
            if (txtOUR_NUMBER.Text.ToString().Trim() != "")
                objAddDepositDetailsinfo.INCORRECT_OUR_NUMBER.CurrentValue = txtOUR_NUMBER.Text.ToString().Trim();
            else
                objAddDepositDetailsinfo.INCORRECT_OUR_NUMBER.CurrentValue = string.Empty;
            if (txtPOLICY_NUMBER.Text.ToString().Trim() != "")
                objAddDepositDetailsinfo.POLICY_NUMBER.CurrentValue = txtPOLICY_NUMBER.Text.ToString();
            else
                objAddDepositDetailsinfo.POLICY_NUMBER.CurrentValue = String.Empty;


            if (hidDEPOSIT_TYPE1.Value.ToString().Trim() != "")
            {
                if (hidDEPOSIT_TYPE1.Value.ToString() == "14831")//Normal
                    objAddDepositDetailsinfo.PAY_MODE.CurrentValue = 14692;
                else
                    objAddDepositDetailsinfo.PAY_MODE.CurrentValue = base.GetEbixIntDefaultValue();
                
                objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue = hidDEPOSIT_TYPE1.Value.ToString().Trim();
            }
            else
            {
                objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue = String.Empty;
            }
            
            //if ((cmbPAY_MODE.SelectedItem != null) && (cmbPAY_MODE.SelectedItem.Text.ToString().Trim() != ""))
            //    objAddDepositDetailsinfo.PAY_MODE.CurrentValue = int.Parse(cmbPAY_MODE.SelectedItem.Value);
            //else
            //    objAddDepositDetailsinfo.PAY_MODE.CurrentValue = base.GetEbixIntDefaultValue();

            //Exception handling case
            //if (cmbPAY_MODE.SelectedIndex != -1 && cmbPAY_MODE.SelectedValue == "14692" )
            objAddDepositDetailsinfo.IS_ACTIVE.CurrentValue = "Y";
            #region For Deposit Type is Normal
            if (hidDEPOSIT_TYPE1.Value.ToString().Trim() == "14831")//Deposit Type 14831(Normal)
            {
                //When user enters Boleto Number, check if this exists in Boleto table, 
                //if it exists as an INACTIVE record, fetch amounts from Installment Details History table and 
                // rest functionality should work  in same way. This deposit item would go in exception.
                if (hidIS_ACTIVE.Value == "N")
                {
                    objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";
                    objAddDepositDetailsinfo.IS_APPROVE.CurrentValue = String.Empty;
                    objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = this.hidEXCEPTION_REASON.Value == "" ? base.GetEbixIntDefaultValue() : int.Parse(this.hidEXCEPTION_REASON.Value);
                    objAddDepositDetailsinfo.IS_ACTIVE.CurrentValue = "N";
                }//if (hidIS_ACTIVE.Value == "N")
                else
                {
                    if (hidEXCEPTION.Value.ToString() != "")
                    {
                       
                        if (hidEXCEPTION.Value.ToString() == "YES")
                        {
                            objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";

                            //Itrack 913 Added by Pradeep on 25-March-2011
                            #region Comment
                            //Total Premium Collection is not equal to Receipt Amount. then we put this into exception case 
                            //if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue != objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue)
                            //    objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = 391;//Total Premium Collection is not equal to Receipt Amount.
                            //else
                            //    objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "N";
                            //Added till here
                            #endregion
                            if (this.hidEXCEPTION_REASON.Value != "294")
                            {
                                if (objAddDepositDetailsinfo.IS_APPROVE.CurrentValue!=null && objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.Trim().ToString() == "A")//For Approve
                                    this.hidEXCEPTION_REASON.Value = "385";//Changes made in already approved Boleto.
                            
                                objAddDepositDetailsinfo.IS_APPROVE.CurrentValue = String.Empty;
                                objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = this.hidEXCEPTION_REASON.Value == "" ? base.GetEbixIntDefaultValue() : int.Parse(this.hidEXCEPTION_REASON.Value);
                            }

                        }//if (hidEXCEPTION.Value.ToString() == "YES")
                        else
                        {
                            //objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = base.GetEbixIntDefaultValue();

                            //Itrack 913 Added by Pradeep on 25-March-2011
                            #region
                            //Total Premium Collection is not equal to Receipt Amount. then we put this into exception case 
                            //if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue != objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue)
                            //{
                            //    objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";
                            //    objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = 391;//Total Premium Collection is not equal to Receipt Amount.
                            //}
                            //else
                            #endregion

                            objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "N";                            
                        }
                        //Added till here 
                    }// if (hidEXCEPTION.Value.ToString() != "")
                    else
                    {
                        objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = String.Empty;
                        objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = this.hidEXCEPTION_REASON.Value == "" ? base.GetEbixIntDefaultValue() : int.Parse(this.hidEXCEPTION_REASON.Value);
                    }
                }
            }// if (cmbPAY_MODE.SelectedValue != "" && cmbPAY_MODE.SelectedValue == "14692")
            else
            {
                objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = String.Empty;
                objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = base.GetEbixIntDefaultValue();
            }
            #endregion
            if (hidDEPOSIT_TYPE1.Value.ToString().Trim() == "14916" ||
                hidDEPOSIT_TYPE1.Value.ToString().Trim() == "14917" ||
                hidDEPOSIT_TYPE1.Value.ToString().Trim() == "14918")////Broker Refund Ceded CO Refund
            {
                objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = hidEXCEPTION.Value.ToString() == "YES" ? "Y" : "N";
                objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = this.hidEXCEPTION_REASON.Value == "" ? base.GetEbixIntDefaultValue() : int.Parse(this.hidEXCEPTION_REASON.Value);
            }
            if (hidDEPOSIT_TYPE1.Value.ToString().Trim() == "14832")////Co-Insurance
            {
                objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = hidEXCEPTION.Value.ToString() == "YES" ? "Y" : "N";
                objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = this.hidEXCEPTION_REASON.Value == "" ? base.GetEbixIntDefaultValue() : int.Parse(this.hidEXCEPTION_REASON.Value);
            }
            
            //292-Beyond Tolerance
            //293-Already cancelled Boleto
            //294-Already Paid Boleto (REPLACED WITH(Boleto already has a deposit item in progress))
            //385-Changes made in already approved Boleto.
            //386-Changes made in already refund Boleto.
            //391-Total Premium Collection is not equal to Receipt Amount.
            //406 -Change in Broker Refund Amount.
            //407 -Change in Follower Refund Amount.

            
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval=0;
            strRowId=hidCD_LINE_ITEM_ID.Value.ToString();
            try
            {
                //For The Save Case    
                ClsAddDepositDetailsinfo objAddDepositDetailsinfo;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objAddDepositDetailsinfo = new ClsAddDepositDetailsinfo();
                
                    this.GetFormValue(objAddDepositDetailsinfo);

                    objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    objAddDepositDetailsinfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                    objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue = int.Parse(hidDEPOSIT_NUMBER.Value);

                    objAddDepositDetailsinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objAddDepositDetailsinfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                  
                    objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue = int.Parse(hidDEPOSIT_ID.Value);
                    
                    //Code comment for itrack-1515
                    //Modifed to save payment date as discussed by Anurag itrack-1148
                    //if (hidDEPOSIT_TYPE1.Value.ToString() != "14831" && hidDEPOSIT_TYPE1.Value.ToString() != "14832")// Not for normal deposit and Co-Insurance 
                    //    objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    //Commented till here 

                    intRetval = ObjDepositDetails.AddDepositLineItemsInfo(objAddDepositDetailsinfo);

                    if (intRetval >0)
                    {
                        hidCD_LINE_ITEM_ID.Value = objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue.ToString();
                        hidCUSTOMER_ID.Value = objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue.ToString();
                        hidPOLICY_ID.Value = objAddDepositDetailsinfo.POLICY_ID.CurrentValue.ToString();
                        hidPOLICY_VERSION_ID.Value = objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue.ToString();
                        this.GetOldDataObject(objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue);
                       
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        btnDelete.Visible = true;
                        btnDelete.Enabled = true;
                    }
                    else if (intRetval == -10)
                    {
                        hidCD_LINE_ITEM_ID.Value = objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue.ToString();
                        hidCUSTOMER_ID.Value = objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue.ToString();
                        hidPOLICY_ID.Value = objAddDepositDetailsinfo.POLICY_ID.CurrentValue.ToString();
                        hidPOLICY_VERSION_ID.Value = objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue.ToString();

                       DataSet ds=objAddDepositDetailsinfo.FetchDepositNumberOfRefundBoleto();
                       String strDepositNumber = String.Empty;
                       
                       if (ds != null && ds.Tables[0].Rows.Count > 0)
                            strDepositNumber = ds.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();

                       this.GetOldDataObject(objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue);

                       lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "49").Replace("~",strDepositNumber);
                       hidFormSaved.Value = "1";
                       btnDelete.Visible = true;
                       btnDelete.Enabled = true;
                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } // if (strRowId.ToUpper().Equals("NEW"))
                else //For The Update cse
                {
                    objAddDepositDetailsinfo = (ClsAddDepositDetailsinfo)base.GetPageModelObject();
                    this.GetFormValue(objAddDepositDetailsinfo);
                    objAddDepositDetailsinfo.IS_ACTIVE.CurrentValue = "Y";
                    objAddDepositDetailsinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue = int.Parse(hidDEPOSIT_NUMBER.Value);
                    objAddDepositDetailsinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                   
                    intRetval = ObjDepositDetails.UpdateDepositLineItemsInfo(objAddDepositDetailsinfo);
                    if (intRetval >0)
                    {
                       
                        hidCD_LINE_ITEM_ID.Value = objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue.ToString();
                        hidCUSTOMER_ID.Value = objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue.ToString();
                        hidPOLICY_ID.Value = objAddDepositDetailsinfo.POLICY_ID.CurrentValue.ToString();
                        hidPOLICY_VERSION_ID.Value = objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue.ToString();
                        
                        this.GetOldDataObject(objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue);
                        
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        btnDelete.Visible = true;
                        btnDelete.Enabled = true;
                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage("G", "20") + " - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {}
        }
            
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsAddDepositDetailsinfo objAddDepositDetailsinfo;
            try
            {
                objAddDepositDetailsinfo = (ClsAddDepositDetailsinfo)base.GetPageModelObject();
                
                objAddDepositDetailsinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue = int.Parse(hidDEPOSIT_NUMBER.Value);
                int intRetval = ObjDepositDetails.DeleteDepositLineItemInfo(objAddDepositDetailsinfo);
                
                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    hidCD_LINE_ITEM_ID.Value = "";
                    hidDEPOSIT_ID.Value="";
                    trBody.Attributes.Add("style", "display:none");
                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "128");
                    hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;
                  
                 
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage("G", "128") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }


        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ClsAddDepositDetailsinfo objAddDepositDetailsinfo;
            try
            {
                if (cmbAPPROVE_REFUND.SelectedItem != null)
                {
                    objAddDepositDetailsinfo = (ClsAddDepositDetailsinfo)base.GetPageModelObject();
                    //this.GetFormValue(objAddDepositDetailsinfo);
                    objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue = int.Parse(hidDEPOSIT_NUMBER.Value);
                    objAddDepositDetailsinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objAddDepositDetailsinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    
                    if ((cmbAPPROVE_REFUND.SelectedItem != null) && (cmbAPPROVE_REFUND.SelectedItem.Value.ToString().Trim() != ""))
                        objAddDepositDetailsinfo.IS_APPROVE.CurrentValue = cmbAPPROVE_REFUND.SelectedItem.Value.ToString().Trim();
                    else
                        objAddDepositDetailsinfo.IS_APPROVE.CurrentValue = String.Empty;

                    int intRetval = ObjDepositDetails.ApproveDepositLineItemInfo(objAddDepositDetailsinfo);
                    
                    
                    if (intRetval > 0)
                    {
                        hidCD_LINE_ITEM_ID.Value = objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue.ToString();
                        hidCUSTOMER_ID.Value = objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue.ToString();
                        hidPOLICY_ID.Value = objAddDepositDetailsinfo.POLICY_ID.CurrentValue.ToString();
                        hidPOLICY_VERSION_ID.Value = objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue.ToString();

                       this.GetOldDataObject(objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue);
                       if( objAddDepositDetailsinfo.IS_APPROVE.CurrentValue.Trim()=="A" )//Approve
                            lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "28");
                       else
                           lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "36");//Refund
                        hidFormSaved.Value = "1";
                        btnDelete.Visible = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29" )+" - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        private void GetGeneratedReceiptDetails(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)
        {
            try
            {
              
                 String str=@"?DEPOSIT_ID=" + objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue.ToString()
                    + "&CD_LINE_ITEM_ID=" + objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue.ToString()
                    + "&RECEIPT_NUM=" + objAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue.ToString();
             
                ClientScript.RegisterStartupScript(this.GetType(), "OPEN_RECEIPT_WINDOW",
                    "<script language='javascript'> window.open( '/cms/Account/Aspx/GeneratePaymentReceipt.aspx" + str + "',null,'resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50 ' ); </script>");

              
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "32") + " - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
        protected void btnGenerateReceipt_Click(object sender, EventArgs e)
        {
            ClsAddDepositDetailsinfo objAddDepositDetailsinfo;
            try
            {
               
                objAddDepositDetailsinfo = (ClsAddDepositDetailsinfo)base.GetPageModelObject();

                if (objAddDepositDetailsinfo.RECEIPT_NUM.CurrentValue.ToString().Trim() == "")
                {
                    objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue = int.Parse(hidDEPOSIT_NUMBER.Value);
                    objAddDepositDetailsinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objAddDepositDetailsinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    objAddDepositDetailsinfo.RECEIPT_ISSUED_DATE.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    int intRetval = ObjDepositDetails.GenerateReceiptOfDepositLineItemInfo(objAddDepositDetailsinfo);

                    if (intRetval > 0)
                    {
                        hidCD_LINE_ITEM_ID.Value = objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue.ToString();
                        hidCUSTOMER_ID.Value = objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue.ToString();
                        hidPOLICY_ID.Value = objAddDepositDetailsinfo.POLICY_ID.CurrentValue.ToString();
                        hidPOLICY_VERSION_ID.Value = objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue.ToString();

                        this.GetOldDataObject(objAddDepositDetailsinfo.CD_LINE_ITEM_ID.CurrentValue);
                        objAddDepositDetailsinfo = (ClsAddDepositDetailsinfo)base.GetPageModelObject();
                        this.GetGeneratedReceiptDetails(objAddDepositDetailsinfo);

                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "30");
                        hidFormSaved.Value = "1";
                        btnGenerateReceipt.Visible = true;
                        btnDelete.Visible = true;
                        btnDelete.Enabled = true;
                    }
                    else if (intRetval == -10)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "31");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "32");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }
                else
                {
                    this.GetGeneratedReceiptDetails(objAddDepositDetailsinfo);
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "32") + " - " + ex.Message;
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
 
    }
}
