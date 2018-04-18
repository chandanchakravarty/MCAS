/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: March 15, 2005 -	>
	<End Date				: March 17, 2005 - >
	<Description			: - >This file is used to Set System Parameters 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 8/7/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Adding Insurance Score period parameter
  
    <Modified Date			: - > 30/06/2010
	<Modified By			: - > Pradeep Kushwaha
	<Purpose				: - > Add new field NOTIFY_RECVE_INSURED
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
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance;  



namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This Class is used to provide interface to System Parameters details.
	/// Apart from this, it is also used to implement Update functionality for System Parameters.
	/// </summary>
	public class SystemParams : Cms.CmsWeb.cmsbase
	{
		#region Web Control Designer Generated Code
		protected System.Web.UI.WebControls.TextBox txtSYS_BAD_LOGON_ATTMPT;
		protected System.Web.UI.WebControls.TextBox txtSYS_RENEWAL_FOR_ALERT;
		protected System.Web.UI.WebControls.TextBox txtSYS_PENDING_QUOTE_FOR_ALERT;
		protected System.Web.UI.WebControls.TextBox txtSYS_QUOTED_QUOTE_FOR_ALERT;
		protected System.Web.UI.WebControls.TextBox txtSYS_NUM_DAYS_EXPIRE;
		protected System.Web.UI.WebControls.TextBox txtSYS_NUM_DAYS_PEN_TO_NTU;
		protected System.Web.UI.WebControls.TextBox txtSYS_NUM_DAYS_EXPIRE_QUOTE;

        protected System.Web.UI.WebControls.TextBox txtMinInstallPlan;
	    protected System.Web.UI.WebControls.TextBox txtAmtUnderPayment;
        protected System.Web.UI.WebControls.TextBox txtMinDays_Premium;
		protected System.Web.UI.WebControls.TextBox txtMinDays_Cancel;
		protected System.Web.UI.WebControls.TextBox txtPostPhone;
		protected System.Web.UI.WebControls.TextBox txtPostCancel;

		protected System.Web.UI.WebControls.DropDownList cboSYS_DEFAULT_POL_TERM;
		protected System.Web.UI.WebControls.DropDownList cmbSYS_DEFAULT_POL_TERM;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.DropDownList cmbSYS_STATEMENT_NAME_LOGO;
		protected System.Web.UI.WebControls.TextBox txtSYS_CLAIME_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtSYS_INSTALLMENT_FEES;
		protected System.Web.UI.WebControls.TextBox txtSYS_NON_SUFFICIENT_FUND_FEES;
		protected System.Web.UI.WebControls.TextBox txtSYS_REINSTATEMENT_FEES;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBadLogin;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRenewalAlert;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvQuotedQuoteAlert;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNumberDaysExpire;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNumberDaysExpireQuote;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPendingQuoteAlert;
        
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMinInstallPlan;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAmtUnderPayment;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMinDays_Premium;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMinDays_Cancel;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPostPhone;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPostCancel;
        protected System.Web.UI.WebControls.DropDownList cmbBASE_CURRENCY;
        protected System.Web.UI.WebControls.Label capBASE_CURRENCY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBASE_CURRENCY;



		protected System.Web.UI.HtmlControls.HtmlForm MNT_SYSTEM_PARAMS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBadLogin;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRenewalAlert;
		protected System.Web.UI.WebControls.RegularExpressionValidator revQuotedQuoteAlert;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNumberDaysExpire;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNumberDaysExpireQuote;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMinInstallPlan;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMinDays_Premium;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAmtUnderPayment;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMinDays_Cancel;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPostPhone;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPostCancel;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPendingQuoteAlert;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNumberDaysPendingNTU;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNumberDaysPendingNTU;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDAYS_FOR_BOLETO_EXPIRATION;

		protected System.Web.UI.WebControls.DropDownList cmbSYS_PRINT_FOLLOWING;
		protected System.Web.UI.WebControls.DropDownList cmbSYS_INDICATE_POL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvInstallmentFees;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNonSufficientFundFees;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvReinstatementFees;
		protected System.Web.UI.WebControls.RegularExpressionValidator revInstallmentFees;
		protected System.Web.UI.WebControls.RegularExpressionValidator revClaimsNumber;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNonSufficientFundFees;
		protected System.Web.UI.WebControls.RegularExpressionValidator revReinstatementFees;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEmployeeDiscount;
		protected System.Web.UI.WebControls.RangeValidator rngEmployeeDiscount;
		protected System.Web.UI.WebControls.TextBox txtSYS_EMPLOYEE_DISCOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEmployeeDiscount;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.RangeValidator rngSYS_BAD_LOGON_ATTMPT;//Added by Sibin on 5 Dec 09

		protected System.Web.UI.WebControls.Label capSYS_BAD_LOGON_ATTMPT;
		protected System.Web.UI.WebControls.Label capINSURANCE_SCORE_VALIDITY;
		protected System.Web.UI.WebControls.Label capSYS_RENEWAL_FOR_ALERT;
		protected System.Web.UI.WebControls.Label capSYS_PENDING_QUOTE_FOR_ALERT;
		protected System.Web.UI.WebControls.Label capSYS_QUOTED_QUOTE_FOR_ALERT;
		protected System.Web.UI.WebControls.Label capSYS_NUM_DAYS_EXPIRE;
		protected System.Web.UI.WebControls.Label capSYS_NUM_DAYS_PEN_TO_NTU;
		protected System.Web.UI.WebControls.Label capSYS_NUM_DAYS_EXPIRE_QUOTE;
		protected System.Web.UI.WebControls.Label capSYS_DEFAULT_POL_TERM;
		protected System.Web.UI.WebControls.Label capSYS_INDICATE_POL;
		protected System.Web.UI.WebControls.Label capSYS_STATEMENT_NAME_LOGO;
		protected System.Web.UI.WebControls.Label capSYS_PRINT_FOLLOWING;
		protected System.Web.UI.WebControls.Label capSYS_CLAIME_NUMBER;
		protected System.Web.UI.WebControls.Label capSYS_INSTALLMENT_FEES;
		protected System.Web.UI.WebControls.Label capSYS_NON_SUFFICIENT_FUND_FEES;
		protected System.Web.UI.WebControls.Label capSYS_REINSTATEMENT_FEES;
		protected System.Web.UI.WebControls.Label capSYS_EMPLOYEE_DISCOUNT;
		protected System.Web.UI.WebControls.Label capMinInstallPlan;
		protected System.Web.UI.WebControls.Label capAmtUnderPayment;
		protected System.Web.UI.WebControls.Label capMinDays_Premium;
		protected System.Web.UI.WebControls.Label capMinDays_Cancel;
		protected System.Web.UI.WebControls.Label capPostPhone;
		protected System.Web.UI.WebControls.Label capPostCancel;
        protected System.Web.UI.WebControls.Label capNOTIFY_RECVE_INSURED;
        protected System.Web.UI.WebControls.DropDownList cmbNOTIFY_RECVE_INSURED;
		
        //Added by Pradeep0 Kushwaha on 1-july-2010
        protected System.Web.UI.WebControls.Label capGeneralSetup;
        protected System.Web.UI.WebControls.Label capManHeader;
        protected System.Web.UI.WebControls.Label capSystemParameters;
        protected System.Web.UI.WebControls.Label capPolicyActions;
        protected System.Web.UI.WebControls.Label capStatementParameters;
        protected System.Web.UI.WebControls.Label capClaimsParameters;
        protected System.Web.UI.WebControls.Label capCertifiedMailParameters;
        protected System.Web.UI.WebControls.Label capPrintParam;
        
        protected System.Web.UI.WebControls.Label capDAYS_FOR_BOLETO_EXPIRATION;
        protected System.Web.UI.WebControls.TextBox txtDAYS_FOR_BOLETO_EXPIRATION;

        //Added till here 
        protected ResourceManager objResourceMgr; 

		#endregion

		#region Form Variables
		//These variables are used to retreive the values from controls 
        //private int intBadLoginAttempt;			// Holds the numeric value for Bad Login Attempt
        //private int intRenewalForAlert;			// Holds the numeric valud for Renewal Alert
        //private int intPendingQuoteForAlert;	// Holds the numeric value for Pending Quote Alert
        //private int intQuotedQuoteForAlert;		// Holds the numeric value for Quoted Qutoe Alert
        //private int intNumberDaysExpire;		// Holds the numeric value for days to expire
        //private int intNumberDaysPendingNTU;	// Holds the numeric value for Pending NTU
        //private int intClaimNumber;				// Holds the claim number
        //private int intNumberDaysExpireQuote;	// Holds the numeric value for Quote Expire
        //private int intDefaultPolicyTerm;		// Holds the numeric value for default policy terms
        //private double dblEmployeeDiscount;		// Holds the decimal value for employee discount
        //private string strGraphLogAllow;		// Holds the value for graph logo value will be YES/NO
        //private string strPrintFollowing ;		// Holds the value of format to print
        //private double dblInstallmentFees;		// Holds the installment fees
        //private double dblNonSufficientFundFees;// Holds the Non sufficient fund fees	
        //private double dblReinstatementFees;
        //private string strInsuranceScore;

        //private double intMinInstallPlan;
        //private double intAmtUnderPayment;
        //private int intMinDays_Premium;
        //private int intMinDays_Cancel;
        //private int intPostPhone;
        //private int intPostCancel;

		protected System.Web.UI.WebControls.TextBox txtINSURANCE_SCORE_VALIDITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURANCE_SCORE_VALIDITY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label capPOSTAGE_FEE;
		protected System.Web.UI.WebControls.TextBox txtPOSTAGE_FEE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOSTAGE_FEE;
		protected System.Web.UI.WebControls.Label capRESTR_DELIV_FEE;
		protected System.Web.UI.WebControls.TextBox txtRESTR_DELIV_FEE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRESTR_DELIV_FEE;
		protected System.Web.UI.WebControls.Label capCERTIFIED_FEE;
		protected System.Web.UI.WebControls.TextBox txtCERTIFIED_FEE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCERTIFIED_FEE;
		protected System.Web.UI.WebControls.Label capRET_RECEIPT_FEE;
		protected System.Web.UI.WebControls.TextBox txtRET_RECEIPT_FEE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRET_RECEIPT_FEE;	
		
		
		private ClsSystemParams objSystemParams	;	// Creating instanse of ClsSystemParams class
		
		//private string SCREENID;
		#endregion

		#region S E T  C A P T I O N S 
		private void SetCaptions()
		{
			capSYS_BAD_LOGON_ATTMPT.Text				=	objResourceMgr.GetString("txtSYS_BAD_LOGON_ATTMPT");
			capINSURANCE_SCORE_VALIDITY.Text			=	objResourceMgr.GetString("txtINSURANCE_SCORE_VALIDITY");
			capSYS_RENEWAL_FOR_ALERT.Text				=	objResourceMgr.GetString("txtSYS_RENEWAL_FOR_ALERT");
			capSYS_PENDING_QUOTE_FOR_ALERT.Text			=	objResourceMgr.GetString("txtSYS_PENDING_QUOTE_FOR_ALERT");
			capSYS_QUOTED_QUOTE_FOR_ALERT.Text			=	objResourceMgr.GetString("txtSYS_QUOTED_QUOTE_FOR_ALERT");
			capSYS_NUM_DAYS_EXPIRE.Text					=	objResourceMgr.GetString("txtSYS_NUM_DAYS_EXPIRE");
			capSYS_NUM_DAYS_PEN_TO_NTU.Text				=	objResourceMgr.GetString("txtSYS_NUM_DAYS_PEN_TO_NTU");
			capSYS_NUM_DAYS_EXPIRE_QUOTE.Text			=	objResourceMgr.GetString("txtSYS_NUM_DAYS_EXPIRE_QUOTE");
			capSYS_DEFAULT_POL_TERM.Text				=	objResourceMgr.GetString("cmbSYS_DEFAULT_POL_TERM");
			capSYS_INDICATE_POL.Text					=	objResourceMgr.GetString("cmbSYS_INDICATE_POL");
			capSYS_STATEMENT_NAME_LOGO.Text				=	objResourceMgr.GetString("cmbSYS_STATEMENT_NAME_LOGO");
			capSYS_PRINT_FOLLOWING.Text					=	objResourceMgr.GetString("cmbSYS_PRINT_FOLLOWING");
			capSYS_CLAIME_NUMBER.Text					=	objResourceMgr.GetString("txtSYS_CLAIME_NUMBER");
			capSYS_INSTALLMENT_FEES.Text				=	objResourceMgr.GetString("txtSYS_INSTALLMENT_FEES");
			capSYS_NON_SUFFICIENT_FUND_FEES.Text		=	objResourceMgr.GetString("txtSYS_NON_SUFFICIENT_FUND_FEES");
			capSYS_REINSTATEMENT_FEES.Text				=	objResourceMgr.GetString("txtSYS_REINSTATEMENT_FEES");
			capSYS_EMPLOYEE_DISCOUNT.Text				=	objResourceMgr.GetString("txtSYS_EMPLOYEE_DISCOUNT");
			capMinInstallPlan.Text						=	objResourceMgr.GetString("txtMinInstallPlan");
			capAmtUnderPayment.Text						=	objResourceMgr.GetString("txtAmtUnderPayment");
			capMinDays_Premium.Text						=	objResourceMgr.GetString("txtMinDays_Premium");
			capMinDays_Cancel.Text						=	objResourceMgr.GetString("txtMinDays_Cancel");
			capPostPhone.Text							=	objResourceMgr.GetString("txtPostPhone");
			capPostCancel.Text							=	objResourceMgr.GetString("txtPostCancel");
			capPOSTAGE_FEE.Text							=	objResourceMgr.GetString("txtPOSTAGE_FEE");
			capRESTR_DELIV_FEE.Text						=	objResourceMgr.GetString("txtRESTR_DELIV_FEE");
			capCERTIFIED_FEE.Text						=	objResourceMgr.GetString("txtCERTIFIED_FEE");
			capRET_RECEIPT_FEE.Text						=	objResourceMgr.GetString("txtRET_RECEIPT_FEE");
            capNOTIFY_RECVE_INSURED.Text                =   objResourceMgr.GetString("cmbNOTIFY_RECVE_INSURED");

            //Added by Pradeep Kushwaha
            capGeneralSetup.Text                        =   objResourceMgr.GetString("capGeneralSetup");
           // capManHeader.Text                           =   objResourceMgr.GetString("capManHeader");
            capSystemParameters.Text                    =   objResourceMgr.GetString("capSystemParameters");
            capPolicyActions.Text                       =   objResourceMgr.GetString("capPolicyActions");
            capStatementParameters.Text                 =   objResourceMgr.GetString("capStatementParameters");
            capClaimsParameters.Text                    =   objResourceMgr.GetString("capClaimsParameters");
            capCertifiedMailParameters.Text             =   objResourceMgr.GetString("capCertifiedMailParameters");
            capPrintParam.Text                          =   objResourceMgr.GetString("capPrintParam");
            //Added till here
            capBASE_CURRENCY.Text = objResourceMgr.GetString("cmbBASE_CURRENCY");
            capDAYS_FOR_BOLETO_EXPIRATION.Text = objResourceMgr.GetString("txtDAYS_FOR_BOLETO_EXPIRATION");
            
        
        }
		#endregion
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			// Calling AddData function 
			btnReset.Attributes.Add("onclick","javascript:return AddData();");
			txtSYS_EMPLOYEE_DISCOUNT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			txtSYS_INSTALLMENT_FEES.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			txtSYS_NON_SUFFICIENT_FUND_FEES.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			txtAmtUnderPayment.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			txtMinInstallPlan.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			txtSYS_REINSTATEMENT_FEES.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			//Assigning the screen id of form
			base.ScreenId = "188";

			btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnReset.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			
			lblMessage.Visible = false;
			objResourceMgr = new System.Resources.ResourceManager("cms.cmsweb.Maintenance.SystemParams" ,System.Reflection.Assembly.GetExecutingAssembly());
            capManHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			if(!Page.IsPostBack)
			{
				CURRENCY();
				cmbSYS_DEFAULT_POL_TERM.SelectedIndex=1;
                BindNotifying();
                SetFormValues();
				SetCaptions();
				SetErrorMessages();
				GetOldXML();
              
                
			}
			
		}
        //Added by pradeep kushwaha on 30-June-2010
        private void BindNotifying()
        {
            cmbNOTIFY_RECVE_INSURED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RECIND");
            cmbNOTIFY_RECVE_INSURED.DataTextField = "LookupDesc";
            cmbNOTIFY_RECVE_INSURED.DataValueField = "LookupID";
            cmbNOTIFY_RECVE_INSURED.DataBind();
            cmbNOTIFY_RECVE_INSURED.Items.Insert(0, "");

            cmbSYS_STATEMENT_NAME_LOGO.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbSYS_STATEMENT_NAME_LOGO.DataTextField = "LookupDesc";
            cmbSYS_STATEMENT_NAME_LOGO.DataValueField = "LookupID";
            cmbSYS_STATEMENT_NAME_LOGO.DataBind();


            cmbSYS_INDICATE_POL.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%Lasp");
            cmbSYS_INDICATE_POL.DataTextField = "LookupDesc";
            cmbSYS_INDICATE_POL.DataValueField = "LookupID";
            cmbSYS_INDICATE_POL.DataBind();

            cmbSYS_PRINT_FOLLOWING.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%dept1");
            cmbSYS_PRINT_FOLLOWING.DataTextField = "LookupDesc";
            cmbSYS_PRINT_FOLLOWING.DataValueField = "LookupID";
            cmbSYS_PRINT_FOLLOWING.DataBind();

            cmbSYS_DEFAULT_POL_TERM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%Month");
            cmbSYS_DEFAULT_POL_TERM.DataTextField = "LookupDesc";
            cmbSYS_DEFAULT_POL_TERM.DataValueField = "LookupCode";
            cmbSYS_DEFAULT_POL_TERM.DataBind();
        }
        private void CURRENCY()
       {
            DataTable dt = Cms.CmsWeb.ClsFetcher.Currency;
            cmbBASE_CURRENCY.DataSource = dt;
            cmbBASE_CURRENCY.DataTextField = "CURR_DESC";
            cmbBASE_CURRENCY.DataValueField = "CURRENCY_ID";
            cmbBASE_CURRENCY.DataBind();
            cmbBASE_CURRENCY.Items.Insert(0, "");

       }
        
        //Added till here 
		private void GetOldXML()
		{
			ClsSystemParams	objSystemParams		=	new ClsSystemParams();
			hidOldData.Value = objSystemParams.GetOldXML();			 
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

		/// <summary>
		/// This is used to save values to MNT_SYSTEM_PARAMS Tabel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//Saving form values
			SaveFormValues();
		}

		#region Methods to do form processing

		/// <summary>
		/// This function is used to reterive control values and assign them to variables.
		/// </summary>
		private Model.Maintenance.ClsSystemParamsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsSystemParamsInfo objSystemParamsInfo;
			objSystemParamsInfo = new ClsSystemParamsInfo();
			objSystemParamsInfo.SYS_BAD_LOGON_ATTMPT	=	int.Parse(txtSYS_BAD_LOGON_ATTMPT.Text.Trim());
			objSystemParamsInfo.SYS_RENEWAL_FOR_ALERT   =	int.Parse(txtSYS_RENEWAL_FOR_ALERT.Text.Trim());
			objSystemParamsInfo.SYS_PENDING_QUOTE_FOR_ALERT	=	int.Parse(txtSYS_PENDING_QUOTE_FOR_ALERT.Text.Trim());
			objSystemParamsInfo.SYS_QUOTED_QUOTE_FOR_ALERT	=	int.Parse(txtSYS_QUOTED_QUOTE_FOR_ALERT.Text.Trim());
			objSystemParamsInfo.SYS_NUM_DAYS_EXPIRE		=	int.Parse(txtSYS_NUM_DAYS_EXPIRE.Text.Trim());
			objSystemParamsInfo.SYS_NUM_DAYS_PEN_TO_NTU		=	int.Parse(txtSYS_NUM_DAYS_PEN_TO_NTU.Text.Trim());
			objSystemParamsInfo.SYS_NUM_DAYS_EXPIRE_QUOTE=	int.Parse(txtSYS_NUM_DAYS_EXPIRE_QUOTE.Text.Trim());
			objSystemParamsInfo.SYS_DEFAULT_POL_TERM	=	int.Parse(cmbSYS_DEFAULT_POL_TERM.SelectedValue.Trim());
			objSystemParamsInfo.SYS_INDICATE_POL_AS			=	cmbSYS_INDICATE_POL.SelectedValue.Trim();
			//objSystemParamsInfo.SYS_EMPLOYEE_DISCOUNT		=	Convert.ToDecimal(txtSYS_EMPLOYEE_DISCOUNT.Text.Trim());
			objSystemParamsInfo.SYS_GRAPH_LOGO_ALLOW		=	cmbSYS_STATEMENT_NAME_LOGO.SelectedValue.Trim();
			objSystemParamsInfo.SYS_PRINT_FOLLOWING			=	cmbSYS_PRINT_FOLLOWING.SelectedValue.Trim();
			//objSystemParamsInfo.SYS_INSTALLMENT_FEES		=	Convert.ToDecimal(txtSYS_INSTALLMENT_FEES.Text.Trim());
			//objSystemParamsInfo.SYS_NON_SUFFICIENT_FUND_FEES=	Convert.ToDecimal(txtSYS_NON_SUFFICIENT_FUND_FEES.Text.Trim());
			//objSystemParamsInfo.SYS_REINSTATEMENT_FEES		=	Convert.ToDecimal(txtSYS_REINSTATEMENT_FEES.Text.Trim());
            if (txtSYS_CLAIME_NUMBER.Text != "")
            objSystemParamsInfo.SYS_CLAIM_NO			    =	int.Parse(txtSYS_CLAIME_NUMBER.Text.Trim());
			objSystemParamsInfo.SYS_INSURANCE_SCORE_VALIDITY=	txtINSURANCE_SCORE_VALIDITY.Text.Trim();  
			//objSystemParamsInfo.SYS_Min_Install_Plan      =  Convert.ToDecimal(txtMinInstallPlan.Text.Trim());
			//objSystemParamsInfo.SYS_AmtUnder_Payment      =   Convert.ToDecimal(txtAmtUnderPayment.Text.Trim()); 
			//objSystemParamsInfo.SYS_MinDays_Premium		  =   int.Parse(txtMinDays_Premium.Text.Trim());
			//objSystemParamsInfo.SYS_MinDays_Cancel        =   int.Parse(txtMinDays_Cancel.Text.Trim());
			//objSystemParamsInfo.SYS_Post_Phone            =   int.Parse(txtPostPhone.Text.Trim());
			//objSystemParamsInfo.SYS_Post_Cancel           =   int.Parse(txtPostCancel.Text.Trim());
            if (txtPOSTAGE_FEE.Text != "")
            objSystemParamsInfo.POSTAGE_FEE				  =   int.Parse(txtPOSTAGE_FEE.Text.Trim());
            if (txtRESTR_DELIV_FEE.Text != "")
            objSystemParamsInfo.RESTR_DELIV_FEE			  =   int.Parse(txtRESTR_DELIV_FEE.Text.Trim());
            if (txtCERTIFIED_FEE.Text != "")
            objSystemParamsInfo.CERTIFIED_FEE             =   int.Parse(txtCERTIFIED_FEE.Text.Trim());
            if (txtRET_RECEIPT_FEE.Text != "")
            objSystemParamsInfo.RET_RECEIPT_FEE           =   int.Parse(txtRET_RECEIPT_FEE.Text.Trim());
            if (txtDAYS_FOR_BOLETO_EXPIRATION.Text!="")
            objSystemParamsInfo.DAYS_FOR_BOLETO_EXPIRATION = int.Parse(txtDAYS_FOR_BOLETO_EXPIRATION.Text.Trim());

            //Added BY Abhinav Agarwal 9-September-2010
            if (cmbBASE_CURRENCY.SelectedValue != null && cmbBASE_CURRENCY.SelectedValue.Trim() != "")
                objSystemParamsInfo.BASE_CURRENCY = int.Parse(cmbBASE_CURRENCY.SelectedValue);
            
            //Added by pradeep kushwaha on 30-June-2010
            if (cmbNOTIFY_RECVE_INSURED.SelectedValue != null && cmbNOTIFY_RECVE_INSURED.SelectedValue.Trim() != "")
                 objSystemParamsInfo.NOTIFY_RECVE_INSURED = int.Parse(cmbNOTIFY_RECVE_INSURED.SelectedValue);
            //Added till here 

			return objSystemParamsInfo;

		}

		/// <summary>
		/// This function is used to check validation on controls.
		/// </summary>
		/// <returns></returns>
		private bool doValidationCheck()
		{
			try
			{
				if(txtSYS_BAD_LOGON_ATTMPT.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtSYS_RENEWAL_FOR_ALERT.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtSYS_NUM_DAYS_EXPIRE.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtSYS_NUM_DAYS_EXPIRE_QUOTE.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtSYS_NUM_DAYS_PEN_TO_NTU.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtSYS_PENDING_QUOTE_FOR_ALERT.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtSYS_QUOTED_QUOTE_FOR_ALERT.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtSYS_CLAIME_NUMBER.Text.Trim().Equals(""))
				{
					return false;
				}
//				if(txtSYS_INSTALLMENT_FEES.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtSYS_NON_SUFFICIENT_FUND_FEES.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtSYS_REINSTATEMENT_FEES.Text.Trim().Equals(""))
//				 {
//					 return false;
//				 }
//				if(txtSYS_EMPLOYEE_DISCOUNT.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtMinInstallPlan.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtAmtUnderPayment.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtMinDays_Premium.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtMinDays_Cancel.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtPostPhone.Text.Trim().Equals(""))
//				{
//					return false;
//				}
//				if(txtPostCancel.Text.Trim().Equals(""))
//				{
//					return false;
//				}
				if(txtPOSTAGE_FEE.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtRESTR_DELIV_FEE.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtCERTIFIED_FEE.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtRET_RECEIPT_FEE.Text.Trim().Equals(""))
				{
					return false;
				}
                return true;
                }
			catch
			{
				return false;
			}
		}
		/// <summary>
		/// This function is used to update system parameters in MNT_SYSTEM_PARAMS Table
		/// </summary>
		private void SaveFormValues()
		{
			try
			{
				int intRetVal;
				//Retreiving the form values into model class object
				ClsSystemParamsInfo objSystemParamsInfo = GetFormValue();
				//Creating the Model object for holding the Old data
				ClsSystemParamsInfo objOldSystemParamsInfo;
				objOldSystemParamsInfo = new ClsSystemParamsInfo();
				base.PopulateModelObject(objOldSystemParamsInfo, hidOldData.Value);

				//int ModifiedBy = int.Parse()
				if(doValidationCheck())
				{
					objSystemParams		=	new ClsSystemParams();
					intRetVal = objSystemParams.UpdateSystemParams(objOldSystemParamsInfo,objSystemParamsInfo,GetUserId());
					if(intRetVal >0)
					{
						lblMessage.Text	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"31");//base.RECORD_UPDATED;
						GetOldXML();
					}
					else
					{
						lblMessage.Text	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");//base.RECORD_UPDATE_FAILED;
					}
					lblMessage.Visible	= true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + "\n Try again!";
				lblMessage.Visible		=	true;
				//Publishing the exception using the static method of Exception manager class
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);


//				lblMessage.Text			=	"Error occured while saving information, Following error occured. " 
//					+ "\n" + ex.Message + "\n Try again!";
//				lblMessage.Visible		=	true;
			}
			finally
			{
				if(objSystemParams != null)
			    objSystemParams.Dispose();
			}
		}

		/// <summary>
		/// This function is used to reterive values from database and assign them to controls when page loads.
		/// </summary>
		private void SetFormValues()
		{
			ClsSystemParams clsSystemParameter	=	new ClsSystemParams();
			DataSet dsDataSet	=	new DataSet();

			dsDataSet	=	clsSystemParameter.GetSystemParams();

			try
			{
				if(dsDataSet !=null)
				{
					if(dsDataSet.Tables[0].Rows.Count > 0)
					{
						txtSYS_BAD_LOGON_ATTMPT.Text			=	dsDataSet.Tables[0].Rows[0][0].ToString();
						txtSYS_RENEWAL_FOR_ALERT.Text			=	dsDataSet.Tables[0].Rows[0][1].ToString();
						txtSYS_PENDING_QUOTE_FOR_ALERT.Text		=	dsDataSet.Tables[0].Rows[0][2].ToString();
						txtSYS_QUOTED_QUOTE_FOR_ALERT.Text		=	dsDataSet.Tables[0].Rows[0][3].ToString();
						txtSYS_NUM_DAYS_EXPIRE.Text				=	dsDataSet.Tables[0].Rows[0][4].ToString();
						txtSYS_NUM_DAYS_PEN_TO_NTU.Text			=	dsDataSet.Tables[0].Rows[0][5].ToString();
						txtSYS_NUM_DAYS_EXPIRE_QUOTE.Text		=	dsDataSet.Tables[0].Rows[0][6].ToString();
						cmbSYS_DEFAULT_POL_TERM.SelectedValue	=	dsDataSet.Tables[0].Rows[0][7].ToString();
						cmbSYS_INDICATE_POL.SelectedValue		=	dsDataSet.Tables[0].Rows[0][8].ToString();
						cmbSYS_STATEMENT_NAME_LOGO.SelectedValue=	dsDataSet.Tables[0].Rows[0][9].ToString().Trim();
                       
						if(dsDataSet.Tables[0].Rows[0][10]      !=  System.DBNull.Value)
						txtSYS_INSTALLMENT_FEES.Text			 =	double.Parse(dsDataSet.Tables[0].Rows[0][10].ToString()).ToString();
						
						if(dsDataSet.Tables[0].Rows[0][11]      !=  System.DBNull.Value)
						txtSYS_NON_SUFFICIENT_FUND_FEES.Text	 =	double.Parse(dsDataSet.Tables[0].Rows[0][11].ToString()).ToString();
						
						if(dsDataSet.Tables[0].Rows[0][12]     !=  System.DBNull.Value)
						txtSYS_REINSTATEMENT_FEES.Text			=	double.Parse(dsDataSet.Tables[0].Rows[0][12].ToString()).ToString();
						
						if(dsDataSet.Tables[0].Rows[0][13]      !=   System.DBNull.Value)
						txtSYS_EMPLOYEE_DISCOUNT.Text			=	double.Parse(dsDataSet.Tables[0].Rows[0][13].ToString()).ToString();

						cmbSYS_PRINT_FOLLOWING.SelectedValue	=	dsDataSet.Tables[0].Rows[0][14].ToString();
						txtSYS_CLAIME_NUMBER.Text				=	dsDataSet.Tables[0].Rows[0][15].ToString();
						txtINSURANCE_SCORE_VALIDITY.Text		=	dsDataSet.Tables[0].Rows[0]["SYS_INSURANCE_SCORE_VALIDITY"].ToString();
						
						if(dsDataSet.Tables[0].Rows[0]["SYS_Min_Install_Plan"] != System.DBNull.Value )
                        txtMinInstallPlan.Text                  =   double.Parse(dsDataSet.Tables[0].Rows[0]["SYS_Min_Install_Plan"].ToString()).ToString();
						else
                        txtMinInstallPlan.Text                  = "";
                        
						if(dsDataSet.Tables[0].Rows[0]["SYS_AmtUnder_Payment"] != System.DBNull.Value )
    					txtAmtUnderPayment.Text                 =   double.Parse(dsDataSet.Tables[0].Rows[0]["SYS_AmtUnder_Payment"].ToString()).ToString();
						else
                        txtAmtUnderPayment.Text                 =  "";
						
						if(dsDataSet.Tables[0].Rows[0]["SYS_MinDays_Premium"]!=System.DBNull.Value)
						txtMinDays_Premium.Text                 =   dsDataSet.Tables[0].Rows[0]["SYS_MinDays_Premium"].ToString();
                        
						if(dsDataSet.Tables[0].Rows[0]["SYS_MinDays_Cancel"]!=System.DBNull.Value)   
						txtMinDays_Cancel.Text                  =   dsDataSet.Tables[0].Rows[0]["SYS_MinDays_Cancel"].ToString();
                        
						if(dsDataSet.Tables[0].Rows[0]["SYS_Post_Phone"]!=System.DBNull.Value)   
						txtPostPhone.Text                      =   dsDataSet.Tables[0].Rows[0]["SYS_Post_Phone"].ToString();
						
						if(dsDataSet.Tables[0].Rows[0]["SYS_Post_Cancel"]!=System.DBNull.Value) 
							txtPostCancel.Text                      =   dsDataSet.Tables[0].Rows[0]["SYS_Post_Cancel"].ToString();   

						if(dsDataSet.Tables[0].Rows[0]["POSTAGE_FEE"]!=System.DBNull.Value) 
							txtPOSTAGE_FEE.Text                      =   dsDataSet.Tables[0].Rows[0]["POSTAGE_FEE"].ToString();   

						if(dsDataSet.Tables[0].Rows[0]["RESTR_DELIV_FEE"]!=System.DBNull.Value) 
							txtRESTR_DELIV_FEE.Text                      =   dsDataSet.Tables[0].Rows[0]["RESTR_DELIV_FEE"].ToString();   

						if(dsDataSet.Tables[0].Rows[0]["CERTIFIED_FEE"]!=System.DBNull.Value) 
							txtCERTIFIED_FEE.Text                      =   dsDataSet.Tables[0].Rows[0]["CERTIFIED_FEE"].ToString();   

						if(dsDataSet.Tables[0].Rows[0]["RET_RECEIPT_FEE"]!=System.DBNull.Value) 
							txtRET_RECEIPT_FEE.Text =   dsDataSet.Tables[0].Rows[0]["RET_RECEIPT_FEE"].ToString();
                        //Added By Abhinav Agarwal
                        if (dsDataSet.Tables[0].Rows[0]["BASE_CURRENCY"] != System.DBNull.Value)
                        {
                            cmbBASE_CURRENCY.SelectedValue = dsDataSet.Tables[0].Rows[0]["BASE_CURRENCY"].ToString(); 
                            
                        }
                        if (dsDataSet.Tables[0].Rows[0]["DAYS_FOR_BOLETO_EXPIRATION"] != System.DBNull.Value)
                        {
                            txtDAYS_FOR_BOLETO_EXPIRATION.Text = dsDataSet.Tables[0].Rows[0]["DAYS_FOR_BOLETO_EXPIRATION"].ToString();
                        }
                        //Added by Pradeep Kushwaha on 30-June-2010
                        if (dsDataSet.Tables[0].Rows[0]["NOTIFY_RECVE_INSURED"] != System.DBNull.Value)
                        {
                            cmbNOTIFY_RECVE_INSURED.SelectedIndex = cmbNOTIFY_RECVE_INSURED.Items.IndexOf(cmbNOTIFY_RECVE_INSURED.Items.FindByValue(dsDataSet.Tables[0].Rows[0]["NOTIFY_RECVE_INSURED"].ToString()));
                        }

					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				dsDataSet.Dispose();
			}
		}
		#endregion

		#region Set Validators Error Messages
/// <summary>
/// This function is used to set error messages for Validators on the page.
/// </summary>
		private void SetErrorMessages()
		{
			rfvBadLogin.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"45");
			rfvRenewalAlert.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"46");
			rfvPendingQuoteAlert.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"47");
			rfvQuotedQuoteAlert.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"48");
			rfvNumberDaysExpire.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"49");
			rfvNumberDaysExpireQuote.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"72");
			rfvNumberDaysPendingNTU.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"73");
			rfvInstallmentFees.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"74");
			rfvNonSufficientFundFees.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"75");
			rfvReinstatementFees.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"76");
			rfvEmployeeDiscount.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"77");
			rfvINSURANCE_SCORE_VALIDITY.ErrorMessage=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"420");
            rfvMinInstallPlan.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvAmtUnderPayment.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvMinDays_Premium.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
		    rfvMinDays_Cancel.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
            rfvPostPhone.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvPostCancel.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvBASE_CURRENCY.ErrorMessage           =    Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");


			revBadLogin.ValidationExpression				=	aRegExpExtn;
			revRenewalAlert.ValidationExpression			=	aRegExpExtn;
			revNumberDaysExpire.ValidationExpression		=	aRegExpExtn;;
			revNumberDaysExpireQuote.ValidationExpression	=	aRegExpExtn;
			revQuotedQuoteAlert.ValidationExpression		=	aRegExpExtn;
			revPendingQuoteAlert.ValidationExpression		=	aRegExpExtn;
			revNumberDaysPendingNTU.ValidationExpression	=	aRegExpExtn;
            revDAYS_FOR_BOLETO_EXPIRATION.ValidationExpression = aRegExpInteger;
			revClaimsNumber.ValidationExpression			=	aRegExpInteger;
			revInstallmentFees.ValidationExpression			=	aRegExpCurrencyformat;
			revNonSufficientFundFees.ValidationExpression	=	aRegExpCurrencyformat;
			revReinstatementFees.ValidationExpression		=	aRegExpCurrencyformat;
			
			revMinInstallPlan.ValidationExpression          =   aRegExpCurrencyformat;
			revAmtUnderPayment.ValidationExpression         =   aRegExpCurrencyformat;
			revMinDays_Cancel.ValidationExpression          =   aRegExpInteger;
			revPostCancel.ValidationExpression              =   aRegExpInteger;
			revPostPhone.ValidationExpression               =   aRegExpInteger;
			revMinDays_Premium.ValidationExpression         =   aRegExpInteger;
			
			revPOSTAGE_FEE.ValidationExpression             =   aRegExpInteger;
			revRESTR_DELIV_FEE.ValidationExpression         =   aRegExpInteger;
			revCERTIFIED_FEE.ValidationExpression           =   aRegExpInteger;
			revRET_RECEIPT_FEE.ValidationExpression         =   aRegExpInteger;
			
			revMinInstallPlan.ErrorMessage                  =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revMinInstallPlan.ErrorMessage                  =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revAmtUnderPayment.ErrorMessage                 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revMinDays_Cancel.ErrorMessage                  =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revPostCancel.ErrorMessage                      =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revPostPhone.ErrorMessage                       =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revMinDays_Premium.ErrorMessage                 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			

			//revEmployeeDiscount.ValidationExpression		=	aRegExpCurrencyformat;

			revClaimsNumber.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revInstallmentFees.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revNonSufficientFundFees.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revReinstatementFees.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			//revEmployeeDiscount.ErrorMessage				=	Cms.CmsWeb.ClsSingleton.GetRegularValidator();

			

			revBadLogin.ErrorMessage						=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revRenewalAlert.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revNumberDaysExpire.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revNumberDaysExpireQuote.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revQuotedQuoteAlert.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revPendingQuoteAlert.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
			revNumberDaysPendingNTU.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");//Cms.CmsWeb.ClsSingleton.GetRegularValidator();
            revDAYS_FOR_BOLETO_EXPIRATION.ErrorMessage      =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");

            revPOSTAGE_FEE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");
            revRESTR_DELIV_FEE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");
            revCERTIFIED_FEE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");
            revRET_RECEIPT_FEE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");
            rngSYS_BAD_LOGON_ATTMPT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2066");
		}
		#endregion
	}
}
