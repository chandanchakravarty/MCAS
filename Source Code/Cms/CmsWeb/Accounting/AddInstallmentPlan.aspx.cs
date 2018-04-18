/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	6/7/2005 1:11:37 PM
<End Date				: -	
<Description			: - 	Page class for Inatallment Plan screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Page class for Inatallment Plan screen
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
using Cms.Model.Maintenance.Accounting;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlApplication;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
using System.Globalization;
namespace Cms.CmsWeb.Accounting
{
    /// <summary>
    /// Summary description for AddInstallmentPlan.
    /// </summary>
    public class AddInstallmentPlan : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capPLAN_CODE;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revPLAN_CODE;
        protected System.Web.UI.WebControls.Label capPLAN_DESCRIPTION;
        protected System.Web.UI.WebControls.TextBox txtPLAN_DESCRIPTION;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLAN_DESCRIPTION;
        protected System.Web.UI.WebControls.Label capPLAN_TYPE;
        protected System.Web.UI.WebControls.TextBox txtPLAN_TYPE;
        protected System.Web.UI.WebControls.Label capNO_OF_PAYMENTS;
        protected System.Web.UI.WebControls.DropDownList cmbNO_OF_PAYMENTS;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_OF_PAYMENTS;
        protected System.Web.UI.WebControls.Label capApplicabletoProduct;
        protected System.Web.UI.WebControls.DropDownList cmbApplicabletoProduct;
        //protected System.Web.UI.WebControls.Label capMONTHS_BETWEEN;
        //protected System.Web.UI.WebControls.TextBox txtMONTHS_BETWEEN;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvMONTHS_BETWEEN;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revMONTHS_BETWEEN;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN1;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN2;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN3;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN3;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN4;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN4;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN5;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN5;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN6;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN6;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN7;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN7;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN8;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN8;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN9;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN9;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN10;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN10;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN11;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN11;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN12;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDUE_DAYS_DOWNPYMT;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
        protected System.Web.UI.WebControls.Label lblPLAN_TYPE;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIDEN_PLAN_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAny;
        protected System.Web.UI.WebControls.Label capCanc;
        protected System.Web.UI.WebControls.Label capHead;
        protected System.Web.UI.WebControls.Label lblNEW;
        protected System.Web.UI.WebControls.Label Label3;
        protected System.Web.UI.WebControls.Label capheader;

        //protected System.Web.UI.WebControls.CompareValidator cmpMONTHS_BETWEEN;
        #region Local form variables
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId;
        protected System.Web.UI.WebControls.Label lblPERCENT_BREAKDOWN1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN3;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN4;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN5;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN6;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN7;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN8;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN9;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN10;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN11;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN12;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN2;
        protected System.Web.UI.WebControls.RegularExpressionValidator revINTREST_RATES; 
        //protected System.Web.UI.WebControls.RangeValidator rngMONTHS_BETWEEN;
        protected System.Web.UI.WebControls.Label lblPERCENT_BREAKDOWN;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected System.Web.UI.WebControls.Label capINSTALLMENT_FEES;
        protected System.Web.UI.WebControls.TextBox txtINSTALLMENT_FEES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSTALLMENT_FEES;
        protected System.Web.UI.WebControls.Label capNON_SUFFICIENT_FUND_FEES;
        protected System.Web.UI.WebControls.TextBox txtNON_SUFFICIENT_FUND_FEES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_SUFFICIENT_FUND_FEES;
        protected System.Web.UI.WebControls.Label capREINSTATEMENT_FEES;
        protected System.Web.UI.WebControls.TextBox txtREINSTATEMENT_FEES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREINSTATEMENT_FEES;
        protected System.Web.UI.WebControls.Label capMIN_INSTALLMENT_AMOUNT;
        protected System.Web.UI.WebControls.TextBox txtMIN_INSTALLMENT_AMOUNT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMIN_INSTALLMENT_AMOUNT;
        protected System.Web.UI.WebControls.Label capMIN_INSTALL_PLAN;
        protected System.Web.UI.WebControls.TextBox txtMIN_INSTALL_PLAN;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMIN_INSTALL_PLAN;
        protected System.Web.UI.WebControls.Label capAMTUNDER_PAYMENT;
        protected System.Web.UI.WebControls.TextBox txtAMTUNDER_PAYMENT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMTUNDER_PAYMENT;
        protected System.Web.UI.WebControls.Label capMINDAYS_PREMIUM;
        protected System.Web.UI.WebControls.TextBox txtMINDAYS_PREMIUM;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINDAYS_PREMIUM;
        protected System.Web.UI.WebControls.Label capMINDAYS_CANCEL;
        protected System.Web.UI.WebControls.TextBox txtMINDAYS_CANCEL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINDAYS_CANCEL;
        protected System.Web.UI.WebControls.Label capPOST_PHONE;
        protected System.Web.UI.WebControls.TextBox txtPOST_PHONE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOST_PHONE;
        protected System.Web.UI.WebControls.Label capPOST_CANCEL;
        protected System.Web.UI.WebControls.TextBox txtPOST_CANCEL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOST_CANCEL;
        protected System.Web.UI.WebControls.Label capINTREST_RATES;
        protected System.Web.UI.WebControls.Label capBASE_DATE_DOWNPAYMENT;
        protected System.Web.UI.WebControls.Label capDUE_DAYS_DOWNPYMT;
        protected System.Web.UI.WebControls.Label capDAYS_SUBSEQUENT_INSTALLMENTS;
        protected System.Web.UI.WebControls.TextBox txtDAYS_SUBSEQUENT_INSTALLMENTS;
        protected System.Web.UI.WebControls.TextBox txtINTREST_RATES;
        protected System.Web.UI.WebControls.DropDownList cmbBASE_DATE_DOWNPAYMENT;
        protected System.Web.UI.WebControls.DropDownList cmbSUBSEQUENT_INSTALLMENTS_OPTION;
        protected System.Web.UI.WebControls.TextBox txtDUE_DAYS_DOWNPYMT;
        protected System.Web.UI.WebControls.Label capDOWN_PAYMENT_PLAN_RENEWAL;
        protected System.Web.UI.WebControls.DropDownList cmbDOWN_PAYMENT_PLAN_RENEWAL;
        protected System.Web.UI.WebControls.Label capDOWN_PAYMENT_PLAN;
        protected System.Web.UI.WebControls.DropDownList cmbDOWN_PAYMENT_PLAN;
        protected System.Web.UI.WebControls.DropDownList cmbBDATE_INSTALL_NXT_DOWNPYMT;
        protected System.Web.UI.WebControls.Label capBDATE_INSTALL_NXT_DOWNPYMT;
        protected System.Web.UI.WebControls.Label capDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT;
        protected System.Web.UI.WebControls.TextBox txtDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT;
        protected System.Web.UI.WebControls.Label capAPPLICABLE_LOB;
        protected System.Web.UI.WebControls.DropDownList cmbAPP_LOB;
        protected Cms.CmsWeb.Controls.CmsButton Cmsbutton1;
        protected Cms.CmsWeb.Controls.CmsButton Cmsbutton2;
        protected Cms.CmsWeb.Controls.CmsButton Cmsbutton3;
        protected System.Web.UI.WebControls.RegularExpressionValidator revINSTALLMENT_FEES;
        protected System.Web.UI.WebControls.RegularExpressionValidator revNON_SUFFICIENT_FUND_FEES;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREINSTATEMENT_FEES;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMIN_INSTALLMENT_AMOUNT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAMTUNDER_PAYMENT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMINDAYS_PREMIUM;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMINDAYS_CANCEL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPOST_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPOST_CANCEL;
        protected System.Web.UI.WebControls.Label capDEFAULT_PLAN;
        protected System.Web.UI.WebControls.CheckBox chkDEFAULT_PLAN;
        protected System.Web.UI.WebControls.Panel PlanBody;
        protected System.Web.UI.WebControls.TextBox PERCENT_BREAKDOWN19;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN1;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN2;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN12;
        protected System.Web.UI.WebControls.Label spnTotalAmount;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN13;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN13;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN13;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN14;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN14;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN14;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN15;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN15;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN15;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN16;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN16;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN16;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN17;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN17;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN17;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN18;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN18;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN18;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN19;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN19;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN19;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN20;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN20;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN20;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN21;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN21;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN21;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN22;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN22;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN22;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN23;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN23;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN23;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWN24;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWN24;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWN24;
        protected System.Web.UI.WebControls.Label spnTotalAmountRP;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP1;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP1;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP2;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP2;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP2;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP3;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP3;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP3;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP4;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP4;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP4;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP5;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP5;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP5;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP6;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP6;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP6;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP7;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP7;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP7;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP8;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP8;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP8;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP9;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP9;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP9;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP10;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP10;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP10;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP11;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP11;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP11;
        protected System.Web.UI.WebControls.TextBox txtPERCENT_BREAKDOWNRP12;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPERCENT_BREAKDOWNRP12;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERCENT_BREAKDOWNRP12;
        protected System.Web.UI.WebControls.CheckBox chkCopy;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label capLATE_FEES;
        protected System.Web.UI.WebControls.TextBox txtLATE_FEES;
        protected System.Web.UI.WebControls.RegularExpressionValidator revLATE_FEES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSERVICE_CHARGE;
        protected System.Web.UI.WebControls.Label capCONVENIENCE_FEE;
        protected System.Web.UI.WebControls.TextBox txtCONVENIENCE_FEE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONVENIENCE_FEE;
        protected System.Web.UI.WebControls.TextBox txtGRACE_PERIOD;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRACE_PERIOD;
        protected System.Web.UI.WebControls.RegularExpressionValidator revGRACE_PERIOD;
        protected System.Web.UI.WebControls.Label capGRACE_PERIOD;
        protected System.Web.UI.WebControls.CustomValidator csvNO_INS_DOWNPAYMENT;
        protected System.Web.UI.WebControls.CustomValidator csvAMTUNDER_PAYMENT;
        protected System.Web.UI.WebControls.RangeValidator rngAMTUNDER_PAYMENT;
        protected System.Web.UI.WebControls.TextBox txtAPPLABLE_POLTERM;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPPLABLE_POLTERM;
        protected System.Web.UI.WebControls.TextBox txtPLAN_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLAN_CODE;
        protected System.Web.UI.WebControls.Label capAPPLABLE_POLTERM;
        protected System.Web.UI.WebControls.DropDownList cmbAPPLABLE_POLTERM;
        protected System.Web.UI.WebControls.Label capPLAN_PAYMENT_MODE;
        protected System.Web.UI.WebControls.DropDownList cmbPLAN_PAYMENT_MODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLAN_PAYMENT_MODE;
        protected System.Web.UI.WebControls.Label capMODE_OF_DOWNPAY_RENEW;
        protected System.Web.UI.WebControls.ListBox cmbMODE_OF_DOWNPAY_RENEW;
        //protected System.Web.UI.WebControls.Label capNO_INS_DOWNPAY;
        //protected System.Web.UI.WebControls.TextBox txtNO_INS_DOWNPAY;
        protected System.Web.UI.WebControls.Label capMODE_OF_DOWNPAY;
        protected System.Web.UI.WebControls.ListBox cmbMODE_OF_DOWNPAY;
        //protected System.Web.UI.WebControls.Label capNO_INS_DOWNPAY_RENEW;
        protected System.Web.UI.WebControls.Label capPAST_DUE_RENEW;
        protected System.Web.UI.WebControls.TextBox txtPAST_DUE_RENEW;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spanTotal_For_Renewal_Process;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spanTotal_For_New_Business_Process;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAST_DUE_RENEW;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPAST_DUE_RENEW;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revNO_INS_DOWNPAY_RENEW;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revNO_INS_DOWNPAY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_INS_DOWNPAY;
        //protected System.Web.UI.WebControls.TextBox txtNO_INS_DOWNPAY_RENEW;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_INS_DOWNPAY_RENEW;
        protected System.Web.UI.WebControls.RangeValidator rngPAST_DUE_RENEW;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODE_OF_DOWNPAY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODE_OF_DOWNPAY_RENEW;
        protected System.Web.UI.WebControls.RangeValidator rvalidator1;
        protected System.Web.UI.WebControls.RangeValidator rvalidator2;
        //protected System.Web.UI.WebControls.RangeValidator rvalidator3;
        protected System.Web.UI.WebControls.CustomValidator csvINTREST_RATES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINTREST_RATES;
        //protected System.Web.UI.WebControls.Label capDAYS_DUE_PREM_NOTICE_PRINTD;
        //protected System.Web.UI.WebControls.TextBox txtDAYS_DUE_PREM_NOTICE_PRINTD;
        protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvDAYS_DUE_PREM_NOTICE_PRINTD;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revDAYS_DUE_PREM_NOTICE_PRINTD;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLATE_FEES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPPLICABLE_LOB;
        protected System.Web.UI.WebControls.CustomValidator csvSUBSEQUENT_INSTALLMENTS;
        protected System.Web.UI.WebControls.CustomValidator csvSUBSEQUENT_INSTALLMENTS_OPTION;
        protected System.Web.UI.WebControls.RequiredFieldValidator RfvDOWN_PAYMENT_PLAN_RENEWAL;
        protected System.Web.UI.WebControls.RequiredFieldValidator RfvDOWN_PAYMENT_PLAN;
        
        //Added by pradeep kushwaha on 30-june-2010
        protected System.Web.UI.WebControls.DropDownList cmbRECVE_NOTIFICATION_DOC;
        protected System.Web.UI.WebControls.Label capRECVE_NOTIFICATION_DOC;
        //Added till here 
        //Defining the business layer class object
        ClsInstallmentPlan objInstallmentPlan;
        public string recalbusinessprocess;
        public string recalrenewalprocess;
        public string totbusinessprocess;
        public string totrenewalprocess;
        public NumberFormatInfo NfiCulture;
        #endregion

        #region Set Validators ErrorMessages
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>
        private void SetErrorMessages()
        {
            rfvPLAN_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvPLAN_DESCRIPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvNO_OF_PAYMENTS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            //rfvMONTHS_BETWEEN.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");


            //revPLAN_CODE.ValidationExpression = aRegExpAlphaNumStrict;
            //revMONTHS_BETWEEN.ValidationExpression	=   aRegExpInteger;

            revPERCENT_BREAKDOWN1.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN2.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN3.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN4.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN5.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN6.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN7.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN8.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN9.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN10.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN11.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWN12.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;

            revPERCENT_BREAKDOWNRP1.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP2.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP3.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP4.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP5.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP6.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//RegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP7.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP8.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP9.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP10.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP11.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
            revPERCENT_BREAKDOWNRP12.ValidationExpression = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;


            //revPLAN_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "26");
            //revMONTHS_BETWEEN.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "26");

            revPERCENT_BREAKDOWN1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN3.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN4.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN5.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN6.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN7.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN8.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN9.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN10.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN11.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWN12.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");

            revPERCENT_BREAKDOWNRP1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP3.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP4.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP5.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP6.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP7.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP8.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP9.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP10.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP11.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            revPERCENT_BREAKDOWNRP12.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");

            rfvPERCENT_BREAKDOWN1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN3.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN4.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN5.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN6.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN7.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN8.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN9.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN10.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN11.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWN12.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            rfvPERCENT_BREAKDOWNRP1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP3.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP4.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP5.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP6.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP7.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP8.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP9.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP10.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP11.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvPERCENT_BREAKDOWNRP12.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvMODE_OF_DOWNPAY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "37");
            rfvMODE_OF_DOWNPAY_RENEW.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "37");
            RfvDOWN_PAYMENT_PLAN.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "38");

            //rngMONTHS_BETWEEN.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");

            rfvINSTALLMENT_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "33");
            //rfvLATE_FEES.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");


            rfvNON_SUFFICIENT_FUND_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "75");
            rfvMIN_INSTALLMENT_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");
            rfvAMTUNDER_PAYMENT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "23");
            rfvMINDAYS_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
            rfvMINDAYS_CANCEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "25");
            rfvPOST_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "26");
            rfvPOST_CANCEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "27");
            rfvGRACE_PERIOD.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("874");
            rfvREINSTATEMENT_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "28");

            //rfvDAYS_DUE_PREM_NOTICE_PRINTD.ErrorMessage			=	ClsMessages.FetchGeneralMessage("882");
            //revDAYS_DUE_PREM_NOTICE_PRINTD.ValidationExpression = aRegExpInteger;
            //revDAYS_DUE_PREM_NOTICE_PRINTD.ErrorMessage			=  ClsMessages.FetchGeneralMessage("163");



            //rfvNO_INS_DOWNPAY.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            //rfvNO_INS_DOWNPAY_RENEW.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            rfvPAST_DUE_RENEW.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");



            revINSTALLMENT_FEES.ValidationExpression = aRegExpBaseCurrencyformat;// aRegExpCurrencyformat;
            revLATE_FEES.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat;

            revNON_SUFFICIENT_FUND_FEES.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat;
            revREINSTATEMENT_FEES.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat;
            revMIN_INSTALLMENT_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;//aRegExpCurrencyformat;
            revAMTUNDER_PAYMENT.ValidationExpression = aRegExpInteger;//aRegExpCurrencyformat;
            revMINDAYS_PREMIUM.ValidationExpression = aRegExpInteger;
            revMINDAYS_CANCEL.ValidationExpression = aRegExpInteger;
            revPOST_PHONE.ValidationExpression = aRegExpInteger;
            revPOST_CANCEL.ValidationExpression = aRegExpInteger;

            revINSTALLMENT_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
            revLATE_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            //revNO_INS_DOWNPAY.ValidationExpression		 = aRegExpInteger;
            //revNO_INS_DOWNPAY_RENEW.ValidationExpression = aRegExpInteger;
            revPAST_DUE_RENEW.ValidationExpression = aRegExpInteger;
            //revNO_INS_DOWNPAY.ErrorMessage				 = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            //revNO_INS_DOWNPAY_RENEW.ErrorMessage		 = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");
            /*revSERVICE_CHARGE.ValidationExpression  =	aRegExpCurrencyformat;
            revCONVENIENCE_FEES.ValidationExpression =	aRegExpCurrencyformat;
            revCONVENIENCE_FEES.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revSERVICE_CHARGE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            */
            revGRACE_PERIOD.ValidationExpression = aRegExpInteger;

            revGRACE_PERIOD.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("874");

            revINTREST_RATES.ValidationExpression = aRegExpBaseDouble;//aRegExpBaseDoublePositiveWithZero;
            revINTREST_RATES.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "34");

            revNON_SUFFICIENT_FUND_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
            revREINSTATEMENT_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("76");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
            revMIN_INSTALLMENT_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revAMTUNDER_PAYMENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revMINDAYS_CANCEL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revPOST_CANCEL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revPOST_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revMINDAYS_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            rfvLATE_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");
            // apply custom message for this validator
            csvSUBSEQUENT_INSTALLMENTS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
            csvSUBSEQUENT_INSTALLMENTS.Attributes.Add("ErrMsgdays", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13"));
            csvSUBSEQUENT_INSTALLMENTS.Attributes.Add("ErrMsgmonths", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14"));
            csvSUBSEQUENT_INSTALLMENTS.Attributes.Add("ErrMsgweeks", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15"));
            csvSUBSEQUENT_INSTALLMENTS_OPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
            csvSUBSEQUENT_INSTALLMENTS_OPTION.Attributes.Add("ErrMsgdays", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13"));
            csvSUBSEQUENT_INSTALLMENTS_OPTION.Attributes.Add("ErrMsgmonths", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14"));
            csvSUBSEQUENT_INSTALLMENTS_OPTION.Attributes.Add("ErrMsgweeks", Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15"));
            rfvINTREST_RATES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
            //rvalidator3.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            csvINTREST_RATES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            rvalidator1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "30");
            rvalidator2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "30"); 
            rfvDUE_DAYS_DOWNPYMT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
            rfvDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");
            recalrenewalprocess = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            recalbusinessprocess = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");
            totbusinessprocess = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
            totrenewalprocess = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
            rfvAPPLICABLE_LOB.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "32");
            RequiredFieldValidator1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "35");
            RfvDOWN_PAYMENT_PLAN_RENEWAL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "36");
        }
        #endregion

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                NfiBaseCurrency.NumberDecimalDigits = 2;

                btnReset.Attributes.Add("onclick", "javascript:return formReset();");
                //btnSave.Attributes.Add("onclick","javascript:if( CheckTotalAmount() == false ) return false;  if( CheckTotalAmountRP() == false ) return false; ");
                //Commented and Added For Itrack Issue #6746
                btnSave.Attributes.Add("onclick", "javascript:return CompareFunction();");

                txtINSTALLMENT_FEES.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
                txtNON_SUFFICIENT_FUND_FEES.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
                //txtAMTUNDER_PAYMENT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
                txtMIN_INSTALLMENT_AMOUNT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
                txtREINSTATEMENT_FEES.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
                txtLATE_FEES.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
                //txtSERVICE_CHARGE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");  
                //txtCONVENIENCE_FEES.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");  
                //txtGRACE_PERIOD.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");//Commented by Pradeep
                txtINTREST_RATES.Attributes.Add("onBlur", "this.value=formatRateBase(this.value);");
                // phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
                base.ScreenId = "181_0";
                lblMessage.Visible = false;
                SetErrorMessages();
                capheader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

                //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
                btnReset.CmsButtonClass = CmsButtonType.Write;
                btnReset.PermissionString = gstrSecurityXML;
                btnSave.CmsButtonClass = CmsButtonType.Write;
                btnSave.PermissionString = gstrSecurityXML;
                btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
                btnActivateDeactivate.PermissionString = gstrSecurityXML;


                //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
                objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Accounting.AddInstallmentPlan", System.Reflection.Assembly.GetExecutingAssembly());

                if (!Page.IsPostBack)
                {
                    GetQueryString();
                    GetOldDataXML();
                    SetCaptions();

                    if (hidIDEN_PLAN_ID.Value.ToUpper().Trim() == "NEW" || hidIDEN_PLAN_ID.Value.ToUpper().Trim() == "0" || hidOldData.Value == "")
                    {
                        imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('IDEN_PLAN_ID','" + 0 + "','ACT_INSTALL_PLAN_DETAIL','ACT_INSTALL_PLAN_DETAIL_MULTILINGUAL','PLAN_DESCRIPTION');return false;");
                    }
                    else
                    {
                        if (imgSelect.Attributes["onclick"] != null)
                            imgSelect.Attributes.Remove("onclick");

                        imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('IDEN_PLAN_ID','" + hidIDEN_PLAN_ID.Value + "','ACT_INSTALL_PLAN_DETAIL','ACT_INSTALL_PLAN_DETAIL_MULTILINGUAL','PLAN_DESCRIPTION');return false;");
                    }//Added till here

                    cmbAPPLABLE_POLTERM.DataSource = ClsGeneralInformation.GetLOBTerms(0);
                    cmbAPPLABLE_POLTERM.DataTextField = "LOOKUP_VALUE_DESC";
                    cmbAPPLABLE_POLTERM.DataValueField = "LOOKUP_VALUE_CODE";
                    cmbAPPLABLE_POLTERM.DataBind();
                    cmbAPPLABLE_POLTERM.Items.Insert(0, "");
                    cmbAPPLABLE_POLTERM.Items.Insert(1, hidAny.Value);//"Any"
                    cmbAPPLABLE_POLTERM.Items.FindByText(hidAny.Value).Value = "0";//"Any"
                    ListItem Li = new ListItem();
                    if (GetLanguageID() == "2")
                    {
                        Li = cmbAPPLABLE_POLTERM.Items.FindByText("1 Ano");
                    }
                    else
                    {
                        Li = cmbAPPLABLE_POLTERM.Items.FindByText("1 Year");
                    }
                    cmbAPPLABLE_POLTERM.Items.Remove(Li);


                    cmbPLAN_PAYMENT_MODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PPYMOD");
                    cmbPLAN_PAYMENT_MODE.DataTextField = "LookupDesc";
                    cmbPLAN_PAYMENT_MODE.DataValueField = "LookupID";
                    cmbPLAN_PAYMENT_MODE.DataBind();

                    // cmbPLAN_PAYMENT_MODE.Items.Remove(cmbPLAN_PAYMENT_MODE.Items.FindByValue("11972"));

                    cmbMODE_OF_DOWNPAY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PPYMOD");
                    cmbMODE_OF_DOWNPAY.DataTextField = "LookupDesc";
                    cmbMODE_OF_DOWNPAY.DataValueField = "LookupID";
                    cmbMODE_OF_DOWNPAY.DataBind();
                    // cmbMODE_OF_DOWNPAY.Items.Remove(cmbMODE_OF_DOWNPAY.Items.FindByValue("11972"));// new ListItem("Check", ));//Commented by Pradeep Kushwaha on 23-Nov-2010
                    cmbMODE_OF_DOWNPAY.Items.Remove(cmbMODE_OF_DOWNPAY.Items.FindByValue("11973"));

                    cmbMODE_OF_DOWNPAY_RENEW.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PPYMOD");
                    cmbMODE_OF_DOWNPAY_RENEW.DataTextField = "LookupDesc";
                    cmbMODE_OF_DOWNPAY_RENEW.DataValueField = "LookupID";
                    cmbMODE_OF_DOWNPAY_RENEW.DataBind();
                    //cmbMODE_OF_DOWNPAY_RENEW.Items.Remove(cmbMODE_OF_DOWNPAY_RENEW.Items.FindByValue("11972"));

                    cmbSUBSEQUENT_INSTALLMENTS_OPTION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("INSOPT");
                    cmbSUBSEQUENT_INSTALLMENTS_OPTION.DataTextField = "LookupDesc";
                    cmbSUBSEQUENT_INSTALLMENTS_OPTION.DataValueField = "LookupID";

                    cmbSUBSEQUENT_INSTALLMENTS_OPTION.DataBind();

                    cmbSUBSEQUENT_INSTALLMENTS_OPTION.SelectedIndex = cmbSUBSEQUENT_INSTALLMENTS_OPTION.Items.IndexOf(cmbSUBSEQUENT_INSTALLMENTS_OPTION.Items.FindByValue("14664"));

                    DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;//new Addition  for  lob  on 5 may 2010
                    cmbAPP_LOB.DataSource = dtLOBs;
                    cmbAPP_LOB.DataTextField = "LOB_DESC";
                    cmbAPP_LOB.DataValueField = "LOB_ID";
                    cmbAPP_LOB.DataBind();
                    cmbAPP_LOB.Items.Insert(0, "");
                    cmbAPP_LOB.Items.Insert(1, "All");
                    cmbAPP_LOB.Items.FindByText("All").Value = "0";
                    //Commented by Sibin for Itrack Issue 5166 on 8 Jan 09
                    //cmbAPP_LOB.Items.Insert(0,new ListItem("","0"));
                    //cmbAPP_LOB.Items.Insert(0, new ListItem("", ""));
                    //cmbAPP_LOB.Items.Insert(1, "All");
                    //cmbAPP_LOB.Items.FindByText("Any").Value = "16";

                    cmbBDATE_INSTALL_NXT_DOWNPYMT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("INSDAT");
                    cmbBDATE_INSTALL_NXT_DOWNPYMT.DataTextField = "LookupDesc";
                    cmbBDATE_INSTALL_NXT_DOWNPYMT.DataValueField = "LookupID";
                    cmbBDATE_INSTALL_NXT_DOWNPYMT.DataBind();
                    cmbBDATE_INSTALL_NXT_DOWNPYMT.SelectedIndex = cmbBDATE_INSTALL_NXT_DOWNPYMT.Items.IndexOf(cmbBDATE_INSTALL_NXT_DOWNPYMT.Items.FindByValue("14449"));

                    cmbBASE_DATE_DOWNPAYMENT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("INSDAT");
                    cmbBASE_DATE_DOWNPAYMENT.DataTextField = "LookupDesc";
                    cmbBASE_DATE_DOWNPAYMENT.DataValueField = "LookupID";
                    cmbBASE_DATE_DOWNPAYMENT.DataBind();

                    //Added by pradeep kushwaha on 30-June-2010
                    cmbRECVE_NOTIFICATION_DOC.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RENTDC");
                    cmbRECVE_NOTIFICATION_DOC.DataTextField = "LookupDesc";
                    cmbRECVE_NOTIFICATION_DOC.DataValueField = "LookupID";
                    cmbRECVE_NOTIFICATION_DOC.DataBind();
                    cmbRECVE_NOTIFICATION_DOC.Items.Insert(0, "");
                    //Added till here 




                }

                if (hidOldData.Value != "")
                {
                    ClsInstallmentPlanInfo objOldInstallmentPlanInfo;
                    objOldInstallmentPlanInfo = new ClsInstallmentPlanInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldInstallmentPlanInfo, hidOldData.Value);

                    if (objOldInstallmentPlanInfo.PLAN_CODE == "FULLPAY")
                    {
                        cmbMODE_OF_DOWNPAY.Items[0].Selected = true;
                        cmbMODE_OF_DOWNPAY_RENEW.Items[0].Selected = true;
                        rfvNO_OF_PAYMENTS.Enabled = false;

                    }
                }
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

        }//end pageload
        #endregion

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsInstallmentPlanInfo GetFormValue()
        {
            
            //Creating the Model object for holding the New data
            ClsInstallmentPlanInfo objInstallmentPlanInfo;
            objInstallmentPlanInfo = new ClsInstallmentPlanInfo();

            objInstallmentPlanInfo.PLAN_CODE = txtPLAN_CODE.Text;
            objInstallmentPlanInfo.PLAN_DESCRIPTION = txtPLAN_DESCRIPTION.Text;
            //objInstallmentPlanInfo.PLAN_TYPE = lblPLAN_TYPE.Text.Substring(0,1);

            if (cmbNO_OF_PAYMENTS.SelectedValue != null && cmbNO_OF_PAYMENTS.SelectedValue.Trim() != "")
              objInstallmentPlanInfo.NO_OF_PAYMENTS = int.Parse(cmbNO_OF_PAYMENTS.SelectedValue);

            //if (txtMONTHS_BETWEEN.Text.Trim() != "")
            //    objInstallmentPlanInfo.MONTHS_BETWEEN = int.Parse(txtMONTHS_BETWEEN.Text);
          

            if (txtPERCENT_BREAKDOWN1.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN1 = Double.Parse(txtPERCENT_BREAKDOWN1.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN2.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN2 = Double.Parse(txtPERCENT_BREAKDOWN2.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN3.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN3 = Double.Parse(txtPERCENT_BREAKDOWN3.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN4.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN4 = Double.Parse(txtPERCENT_BREAKDOWN4.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN5.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN5 = Double.Parse(txtPERCENT_BREAKDOWN5.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN6.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN6 = Double.Parse(txtPERCENT_BREAKDOWN6.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN7.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN7 = Double.Parse(txtPERCENT_BREAKDOWN7.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN8.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN8 = Double.Parse(txtPERCENT_BREAKDOWN8.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN9.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN9 = Double.Parse(txtPERCENT_BREAKDOWN9.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN10.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN10 = Double.Parse(txtPERCENT_BREAKDOWN10.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN11.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN11 = Double.Parse(txtPERCENT_BREAKDOWN11.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWN12.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWN12 = Double.Parse(txtPERCENT_BREAKDOWN12.Text, NfiBaseCurrency);

            // For Renewal Process
            if (txtPERCENT_BREAKDOWNRP1.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP1 = Double.Parse(txtPERCENT_BREAKDOWNRP1.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP2.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP2 = Double.Parse(txtPERCENT_BREAKDOWNRP2.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP3.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP3 = Double.Parse(txtPERCENT_BREAKDOWNRP3.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP4.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP4 = Double.Parse(txtPERCENT_BREAKDOWNRP4.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP5.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP5 = Double.Parse(txtPERCENT_BREAKDOWNRP5.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP6.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP6 = Double.Parse(txtPERCENT_BREAKDOWNRP6.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP7.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP7 = Double.Parse(txtPERCENT_BREAKDOWNRP7.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP8.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP8 = Double.Parse(txtPERCENT_BREAKDOWNRP8.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP9.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP9 = Double.Parse(txtPERCENT_BREAKDOWNRP9.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP10.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP10 = Double.Parse(txtPERCENT_BREAKDOWNRP10.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP11.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP11 = Double.Parse(txtPERCENT_BREAKDOWNRP11.Text, NfiBaseCurrency);

            if (txtPERCENT_BREAKDOWNRP12.Text.Trim() != "")
                objInstallmentPlanInfo.PERCENT_BREAKDOWNRP12 = Double.Parse(txtPERCENT_BREAKDOWNRP12.Text, NfiBaseCurrency);




            if (txtINSTALLMENT_FEES.Text.Trim() != "")
                objInstallmentPlanInfo.INSTALLMENT_FEES = Double.Parse(txtINSTALLMENT_FEES.Text, NfiBaseCurrency);

            if (txtLATE_FEES.Text.Trim() != "")
                objInstallmentPlanInfo.LATE_FEES = Double.Parse(txtLATE_FEES.Text,NfiBaseCurrency);


            if (txtMIN_INSTALLMENT_AMOUNT.Text.Trim() != "")
                objInstallmentPlanInfo.MIN_INSTALLMENT_AMOUNT = Double.Parse(txtMIN_INSTALLMENT_AMOUNT.Text, NfiBaseCurrency);

            if (txtNON_SUFFICIENT_FUND_FEES.Text.Trim() != "")
                objInstallmentPlanInfo.NON_SUFFICIENT_FUND_FEES = Double.Parse(txtNON_SUFFICIENT_FUND_FEES.Text, NfiBaseCurrency);

            if (txtREINSTATEMENT_FEES.Text.Trim() != "")
                objInstallmentPlanInfo.REINSTATEMENT_FEES = Double.Parse(txtREINSTATEMENT_FEES.Text, NfiBaseCurrency);
            /*
            if (txtSERVICE_CHARGE.Text.Trim() != "")
                objInstallmentPlanInfo.SERVICE_CHARGE = Double.Parse(txtSERVICE_CHARGE.Text);
            if (txtCONVENIENCE_FEES.Text.Trim() != "")
                objInstallmentPlanInfo.CONVENIENCE_FEES = Double.Parse(txtCONVENIENCE_FEES.Text);
            */
            if (txtGRACE_PERIOD.Text.Trim() != "")
                objInstallmentPlanInfo.GRACE_PERIOD = int.Parse(txtGRACE_PERIOD.Text);

            //if (txtDAYS_DUE_PREM_NOTICE_PRINTD.Text.Trim() != "")
            //    objInstallmentPlanInfo.DAYS_DUE_PREM_NOTICE_PRINTD = int.Parse(txtDAYS_DUE_PREM_NOTICE_PRINTD.Text);


            if (txtMIN_INSTALLMENT_AMOUNT.Text.Trim() != "")
                objInstallmentPlanInfo.MIN_INSTALLMENT_AMOUNT = Double.Parse(txtMIN_INSTALLMENT_AMOUNT.Text,NfiBaseCurrency);

            if (txtAMTUNDER_PAYMENT.Text.Trim() != "")
                objInstallmentPlanInfo.AMTUNDER_PAYMENT = Double.Parse(txtAMTUNDER_PAYMENT.Text);

            if (txtMINDAYS_PREMIUM.Text.Trim() != "")
                objInstallmentPlanInfo.MINDAYS_PREMIUM = int.Parse(txtMINDAYS_PREMIUM.Text);

            if (txtMINDAYS_CANCEL.Text.Trim() != "")
                objInstallmentPlanInfo.MINDAYS_CANCEL = int.Parse(txtMINDAYS_CANCEL.Text);

            if (txtPOST_PHONE.Text.Trim() != "")
                objInstallmentPlanInfo.POST_PHONE = int.Parse(txtPOST_PHONE.Text);


            if (txtPOST_CANCEL.Text.Trim() != "")
                objInstallmentPlanInfo.POST_CANCEL = int.Parse(txtPOST_CANCEL.Text);

            objInstallmentPlanInfo.DEFAULT_PLAN = chkDEFAULT_PLAN.Checked;

            ///added by pravesh on 30 nov 2006 for applicable term and payment mode
            objInstallmentPlanInfo.APPLABLE_POLTERM = int.Parse(cmbAPPLABLE_POLTERM.SelectedValue);
            objInstallmentPlanInfo.PLAN_PAYMENT_MODE = int.Parse(cmbPLAN_PAYMENT_MODE.SelectedValue);
           

            //if (txtNO_INS_DOWNPAY.Text.Trim() != "")
            //    objInstallmentPlanInfo.NO_INS_DOWNPAY = int.Parse(txtNO_INS_DOWNPAY.Text);

            string selectMode = "";
            for (int i = 0; i < cmbMODE_OF_DOWNPAY.Items.Count; i++)
            {
                if (cmbMODE_OF_DOWNPAY.Items[i].Selected == true)
                {
                    selectMode = selectMode + cmbMODE_OF_DOWNPAY.Items[i].Value + ",";
                }
            }
            string[] selectModes = selectMode.Split(',');
            objInstallmentPlanInfo.MODE_OF_DOWNPAY = int.Parse(cmbMODE_OF_DOWNPAY.SelectedValue);
            if (selectModes[0].Trim() != "")
                objInstallmentPlanInfo.MODE_OF_DOWNPAY = int.Parse(selectModes[0].Trim());
            if (selectModes.GetLength(0) >= 2)
            {
                if (selectModes[1].Trim() != "")
                    objInstallmentPlanInfo.MODE_OF_DOWNPAY1 = int.Parse(selectModes[1].Trim());
            }
            if (selectModes.GetLength(0) >= 3)
            {
                if (selectModes[2].Trim() != "")
                    objInstallmentPlanInfo.MODE_OF_DOWNPAY2 = int.Parse(selectModes[2].Trim());
            }

            //if (txtNO_INS_DOWNPAY_RENEW.Text.Trim() != "")
            //    objInstallmentPlanInfo.NO_INS_DOWNPAY_RENEW = int.Parse(txtNO_INS_DOWNPAY_RENEW.Text);

            selectMode = "";
            for (int i = 0; i < cmbMODE_OF_DOWNPAY_RENEW.Items.Count; i++)
            {
                if (cmbMODE_OF_DOWNPAY_RENEW.Items[i].Selected == true)
                {
                    selectMode = selectMode + cmbMODE_OF_DOWNPAY_RENEW.Items[i].Value + ",";
                }
            }
            //objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW	= int.Parse(cmbMODE_OF_DOWNPAY_RENEW.SelectedValue);  
            selectModes = selectMode.Split(',');
            if (selectModes[0].Trim() != "")
                objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW = int.Parse(selectModes[0].Trim());
            if (selectModes.GetLength(0) >= 2)
            {
                if (selectModes[1].Trim() != "")
                    objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW1 = int.Parse(selectModes[1].Trim());
            }
            if (selectModes.GetLength(0) >= 3)
            {
                if (selectModes[2].Trim() != "")
                    objInstallmentPlanInfo.MODE_OF_DOWNPAY_RENEW2 = int.Parse(selectModes[2].Trim());
            }
            objInstallmentPlanInfo.PAST_DUE_RENEW = int.Parse(txtPAST_DUE_RENEW.Text.Trim());

            objInstallmentPlanInfo.INTREST_RATES = Double.Parse(txtINTREST_RATES.Text, NfiBaseCurrency);
            objInstallmentPlanInfo.DAYS_SUBSEQUENT_INSTALLMENTS = int.Parse(txtDAYS_SUBSEQUENT_INSTALLMENTS.Text);
            objInstallmentPlanInfo.SUBSEQUENT_INSTALLMENTS_OPTION = cmbSUBSEQUENT_INSTALLMENTS_OPTION.SelectedItem.Value;
            objInstallmentPlanInfo.BASE_DATE_DOWNPAYMENT = int.Parse(cmbBASE_DATE_DOWNPAYMENT.SelectedItem.Value);
            objInstallmentPlanInfo.DUE_DAYS_DOWNPYMT = int.Parse(txtDUE_DAYS_DOWNPYMT.Text);
            objInstallmentPlanInfo.BDATE_INSTALL_NXT_DOWNPYMT = int.Parse(cmbBDATE_INSTALL_NXT_DOWNPYMT.SelectedItem.Value);
            objInstallmentPlanInfo.DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT = int.Parse(txtDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT.Text);
            objInstallmentPlanInfo.DOWN_PAYMENT_PLAN = int.Parse(cmbDOWN_PAYMENT_PLAN.SelectedItem.Value);
            objInstallmentPlanInfo.DOWN_PAYMENT_PLAN_RENEWAL = int.Parse(cmbDOWN_PAYMENT_PLAN_RENEWAL.SelectedItem.Value);
            
            //Modified by pradeep Kushwaha
            if(cmbAPP_LOB.SelectedValue!=null && cmbAPP_LOB.SelectedValue.Trim()!="")
                objInstallmentPlanInfo.APPLICABLE_LOB = int.Parse(cmbAPP_LOB.SelectedItem.Value);
            
            
            //Added by pradeep kushwaha on 30-June-2010
            if (cmbRECVE_NOTIFICATION_DOC.SelectedValue != null && cmbRECVE_NOTIFICATION_DOC.SelectedValue.Trim() != "")
                objInstallmentPlanInfo.RECVE_NOTIFICATION_DOC = int.Parse(cmbRECVE_NOTIFICATION_DOC.SelectedValue);
            //Added till here 

            //These  assignments are common to all pages.
            strRowId = hidIDEN_PLAN_ID.Value;

           
            //Returning the model object
            return objInstallmentPlanInfo;
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region "Web Event Handlers"
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                objInstallmentPlan = new ClsInstallmentPlan();
                //Retreiving the form values into model class object
                ClsInstallmentPlanInfo objInstallmentPlanInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objInstallmentPlanInfo.CREATED_BY = int.Parse(GetUserId());
                    objInstallmentPlanInfo.CREATED_DATETIME = DateTime.Now;
                    //Calling the add method of business layer class
                    intRetVal = objInstallmentPlan.Add(objInstallmentPlanInfo);

                    if (intRetVal > 0)
                    {
                        hidIDEN_PLAN_ID.Value = objInstallmentPlanInfo.IDEN_PLAN_ID.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidIS_ACTIVE.Value = "Y";
                        hidFormSaved.Value = "1";
                        GetOldDataXML();
                    }
                    else if (intRetVal == -1)
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
                } // end save case
                else //UPDATE CASE
                {

                    //Creating the Model object for holding the Old data
                    ClsInstallmentPlanInfo objOldInstallmentPlanInfo;
                    objOldInstallmentPlanInfo = new ClsInstallmentPlanInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldInstallmentPlanInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objInstallmentPlanInfo.IDEN_PLAN_ID = int.Parse(strRowId);
                    objInstallmentPlanInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objInstallmentPlanInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    objInstallmentPlanInfo.IS_ACTIVE = "Y";

                    //Updating the record using business layer class object
                    intRetVal = objInstallmentPlan.Update(objOldInstallmentPlanInfo, objInstallmentPlanInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        GetOldDataXML();
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "1";
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "1";
                    }
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objInstallmentPlan != null)
                    objInstallmentPlan.Dispose();
            }
        }
        #endregion

        private void SetCaptions()
        {
            try
            {
                capPLAN_CODE.Text = objResourceMgr.GetString("txtPLAN_CODE");
                capPLAN_DESCRIPTION.Text = objResourceMgr.GetString("txtPLAN_DESCRIPTION");
                //capPLAN_TYPE.Text = objResourceMgr.GetString("txtPLAN_TYPE");
                capNO_OF_PAYMENTS.Text = objResourceMgr.GetString("cmbNO_OF_PAYMENTS");
                // capMONTHS_BETWEEN.Text = objResourceMgr.GetString("txtMONTHS_BETWEEN");

                capINSTALLMENT_FEES.Text = objResourceMgr.GetString("txtINSTALLMENT_FEES");
                capNON_SUFFICIENT_FUND_FEES.Text = objResourceMgr.GetString("txtNON_SUFFICIENT_FUND_FEES");
                capREINSTATEMENT_FEES.Text = objResourceMgr.GetString("txtREINSTATEMENT_FEES");
                capMIN_INSTALLMENT_AMOUNT.Text = objResourceMgr.GetString("txtMIN_INSTALLMENT_AMOUNT");
                capAMTUNDER_PAYMENT.Text = objResourceMgr.GetString("txtAMTUNDER_PAYMENT");
                capMINDAYS_PREMIUM.Text = objResourceMgr.GetString("txtMINDAYS_PREMIUM");
                capMINDAYS_CANCEL.Text = objResourceMgr.GetString("txtMINDAYS_CANCEL");
                capPOST_CANCEL.Text = objResourceMgr.GetString("txtPOST_CANCEL");
                capPOST_PHONE.Text = objResourceMgr.GetString("txtPOST_PHONE");
                capLATE_FEES.Text = objResourceMgr.GetString("txtLATE_FEES");
                //capSERVICE_CHARGE.Text		=	objResourceMgr.GetString("txtSERVICE_CHARGE"); 
                //capCONVENIENCE_FEES.Text     =	objResourceMgr.GetString("txtCONVENIENCE_FEES");
                capGRACE_PERIOD.Text = objResourceMgr.GetString("txtGRACE_PERIOD");
                // capDAYS_DUE_PREM_NOTICE_PRINTD.Text = objResourceMgr.GetString("txtDAYS_DUE_PREM_NOTICE_PRINTD");
                capPLAN_PAYMENT_MODE.Text = objResourceMgr.GetString("txtPLAN_PAYMENT_MODE");
                capAPPLABLE_POLTERM.Text = objResourceMgr.GetString("txtAPPLABLE_POLTERM");
                //capNO_INS_DOWNPAY.Text		=   objResourceMgr.GetString("txtNO_INS_DOWNPAY");
                capMODE_OF_DOWNPAY.Text = objResourceMgr.GetString("txtMODE_OF_DOWNPAY");
                capNO_OF_PAYMENTS.Text = objResourceMgr.GetString("cmbNO_OF_PAYMENTS");
                capMODE_OF_DOWNPAY_RENEW.Text = objResourceMgr.GetString("txtMODE_OF_DOWNPAY_RENEW");
                capBASE_DATE_DOWNPAYMENT.Text = objResourceMgr.GetString("cmbBASE_DATE_DOWNPAYMENT");
                capDAYS_SUBSEQUENT_INSTALLMENTS.Text = objResourceMgr.GetString("txtDAYS_SUBSEQUENT_INSTALLMENTS");
                capINTREST_RATES.Text = objResourceMgr.GetString("txtINTREST_RATES");
                capDOWN_PAYMENT_PLAN_RENEWAL.Text = objResourceMgr.GetString("cmbDOWN_PAYMENT_PLAN_RENEWAL");
                capDOWN_PAYMENT_PLAN.Text = objResourceMgr.GetString("cmbDOWN_PAYMENT_PLAN");
                capDUE_DAYS_DOWNPYMT.Text = objResourceMgr.GetString("txtDUE_DAYS_DOWNPYMT");
                capBDATE_INSTALL_NXT_DOWNPYMT.Text = objResourceMgr.GetString("cmbBDATE_INSTALL_NXT_DOWNPYMT");
                capDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT.Text = objResourceMgr.GetString("txtDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT");
                capAPPLICABLE_LOB.Text = objResourceMgr.GetString("cmbAPP_LOB");
                capPAST_DUE_RENEW.Text = objResourceMgr.GetString("txtPAST_DUE_RENEW");
                spanTotal_For_Renewal_Process.InnerText = objResourceMgr.GetString("spanTotal_For_Renewal_Process");
                Label1.Text = objResourceMgr.GetString("Label1");
                Label2.Text = objResourceMgr.GetString("Label2");
                lblPERCENT_BREAKDOWN.Text = objResourceMgr.GetString("lblPERCENT_BREAKDOWN");
                spanTotal_For_New_Business_Process.InnerText = objResourceMgr.GetString("spanTotal_For_New_Business_Process");
                capRECVE_NOTIFICATION_DOC.Text = objResourceMgr.GetString("cmbRECVE_NOTIFICATION_DOC");
                capCanc.Text = objResourceMgr.GetString("capCanc");
                capHead.Text = objResourceMgr.GetString("capHead");
                lblNEW.Text = objResourceMgr.GetString("lblNEW");
                Label3.Text = objResourceMgr.GetString("Label3");
                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("NEW");
                hidAny.Value = objResourceMgr.GetString("any");
            }

            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
        private void GetOldDataXML()
        {
           
            if (hidIDEN_PLAN_ID.Value.Trim() != "")
            {
                hidOldData.Value = ClsInstallmentPlan.GetInstallmentPlanInfo(int.Parse(hidIDEN_PLAN_ID.Value));
                if (hidIDEN_PLAN_ID.Value == ClsInstallmentPlan.FullPayPlanID().ToString())
                {
                    //For system generated full pay plan, delete and activate button will not be visible
                }
               
            }
            hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value);
            if (hidOldData.Value != "")
            {
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }

                else
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }

            }
        }

        private void GetQueryString()
        {
            hidIDEN_PLAN_ID.Value = Request.Params["IDEN_PLAN_ID"];
        }

        #region btnActivateDeactivate_Click

        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {

            ClsInstallmentPlan objInstallment = new ClsInstallmentPlan();
            ClsInstallmentPlanInfo objInstallmentInfo = new ClsInstallmentPlanInfo();
          
            string is_Active;
           
            try
            {

               
                objInstallmentInfo.CREATED_BY = int.Parse(GetUserId());
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    is_Active = "N";

                    objInstallmentInfo.IS_ACTIVE = "N";
                    ClsInstallmentPlan.ActivateDeactivate(int.Parse(hidIDEN_PLAN_ID.Value), is_Active, objInstallmentInfo);
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objInstallmentInfo.IS_ACTIVE.ToString().Trim());
                    lblMessage.Visible = true;
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");

                }
                else
                {

                    is_Active = "Y";
                    
                    objInstallmentInfo.IS_ACTIVE = "Y";
                    ClsInstallmentPlan.ActivateDeactivate(int.Parse(hidIDEN_PLAN_ID.Value), is_Active, objInstallmentInfo);
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objInstallmentInfo.IS_ACTIVE.ToString().Trim());
                    lblMessage.Visible = true;
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");


                }
                hidOldData.Value = ClsInstallmentPlan.GetInstallmentPlanInfo(int.Parse(hidIDEN_PLAN_ID.Value));
                hidFormSaved.Value = "1";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                ExceptionManager.Publish(ex);
            }

        }
        #endregion

        protected void txtNUMBER_OF_DUE_DAYS_FOR_DOWNPAYMENT_TextChanged(object sender, EventArgs e)
        {

        }

       

        //protected void cmbNO_OF_PAYMENTS_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}

