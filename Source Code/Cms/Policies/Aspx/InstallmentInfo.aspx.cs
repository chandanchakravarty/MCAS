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
using Cms.Model.Application;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;  
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls; 
using System.Configuration;
/******************************************************************************************
	<Author					: Neeraj Singh>
	<Start Date				: OCT 17, 2006 >
	<End Date				: OCT 17, 2006 >
	<Description			: This class is used for showing installment information. >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: > 29th May 2007
	<Modified By			: > Swastika Gaur	
	<Purpose				: > Added Credit Card Section
	
	<Modified Date			: >
	<Modified By			: >
	<Purpose				: >
					
*******************************************************************************************/
namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for InstallmentInfo.
	/// </summary>
	public class InstallmentInfo : Cms.Policies.policiesbase
	{
		#region Page Declarations
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCCFlag;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEFTFlag;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.DataGrid dgPolicyInstallInfo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbINSTALL_PLAN_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREVERIFIED_AC;
		
		protected string strBillType ="" ,strPolicyStatus = "",strInstallmentType="" ,strModeDownPay="", strPlanPayMode="";
		protected int intPolicyTerm,intInstallPlanID,intBillTypeID;
		protected System.Web.UI.WebControls.TextBox txtFEDERAL_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFEDERAL_ID;
		protected System.Web.UI.WebControls.TextBox txtDFI_ACC_NO;
		protected System.Web.UI.WebControls.CustomValidator csvDFI_ACC_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDFI_ACC_NO;
		protected System.Web.UI.WebControls.TextBox txtTRANSIT_ROUTING_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSIT_ROUTING_NO;
		protected System.Web.UI.WebControls.CustomValidator csvTRANSIT_ROUTING_NO;
		protected System.Web.UI.WebControls.CustomValidator csvVERIFYFY_TRANSIT_ROUTING_NO;
		protected System.Web.UI.WebControls.CustomValidator csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTRANSIT_ROUTING_NO;
		protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPEO;
		protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPET;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Panel pnlEFTCust;
		protected System.Web.UI.WebControls.Panel pnlCCMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnAdjust;
		protected System.Web.UI.WebControls.Label capEFT_TENTATIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFT_TENTATIVE_DATE;
		protected System.Web.UI.WebControls.RangeValidator rngEFT_TENTATIVE_DATE;
		protected const string DOWNPAYMENT_MODE_CHECK= "11972";
		protected const string DOWNPAYMENT_MODE_EFT = "11973";
		protected const string DOWNPAYMENT_MODE_CREDITCARD = "11974";
		protected const string BILLTYPEID_MORTGAGEE   = "11276";
		protected System.Web.UI.WebControls.RegularExpressionValidator revDFI_ACC_NO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDAY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFT_TENTATIVE_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILL_TYPE;
		protected System.Web.UI.WebControls.Label capCARD_NO;
		protected System.Web.UI.WebControls.TextBox txtCARD_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCARD_NO;
		protected System.Web.UI.WebControls.CustomValidator csvCARD_NO;
		protected System.Web.UI.WebControls.Label capCARD_DATE_VALID_FROM;
		protected System.Web.UI.WebControls.TextBox txtCARD_DATE_VALID_FROM;
		protected System.Web.UI.WebControls.Label capCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.TextBox txtCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.Label capCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.Label capCARD_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCARD_TYPE;
		protected Cms.CmsWeb.Controls.CmsButton btnCCSave;
		protected System.Web.UI.WebControls.Panel pnlCCCust;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.DropDownList cmbCARD_DATE_VALID_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_DATE_VALID_FROM;
		protected System.Web.UI.WebControls.RangeValidator rngCARD_DATE_VALID_FROM;
		protected System.Web.UI.WebControls.CustomValidator csvCARD_DATE_VALID_FROM;
		protected System.Web.UI.WebControls.DropDownList cmbCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.RangeValidator rngCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.CompareValidator cmpCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.CustomValidator csvCARD_DATE_VALID_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_CVV_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARD_TYPE;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.Label capCUSTOMER_FIRST_NAME;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_FIRST_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_FIRST_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_FIRST_NAME;
		protected System.Web.UI.WebControls.Label capCUSTOMER_MIDDLE_NAME;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_MIDDLE_NAME;
		protected System.Web.UI.WebControls.Label capCUSTOMER_LAST_NAME;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_LAST_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_LAST_NAME;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ADDRESS1;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ADDRESS2;
		protected System.Web.UI.WebControls.Label capCUSTOMER_CITY;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_CITY;
		protected System.Web.UI.WebControls.Label capCUSTOMER_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_COUNTRY;
		protected System.Web.UI.WebControls.Label capCUSTOMER_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_STATE;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ZIP;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ZIP;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_ZIP;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnCUSTOMER_ZIP;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_INFO;
		protected System.Web.UI.WebControls.Label capINSTALL_PLAN_ID;
		protected System.Web.UI.WebControls.Label capFEDERAL_ID;
		protected System.Web.UI.WebControls.Label capDFI_ACCT_NUMBER;
		protected System.Web.UI.WebControls.Label capTRAN_ROUT_NUMBER;
		protected System.Web.UI.WebControls.Label capIS_VERIFIED;
		protected System.Web.UI.WebControls.Label lblIS_VERIFIED;
		protected System.Web.UI.WebControls.Label capVERIFIED_DATE;
		protected System.Web.UI.WebControls.Label lblVERIFIED_DATE;
		protected System.Web.UI.WebControls.Label capREASON;
		protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;
		protected System.Web.UI.WebControls.CustomValidator csvCUSTOMER_ZIP;
		protected System.Web.UI.WebControls.Label capREVERIFIED_AC;
		protected System.Web.UI.WebControls.CheckBox chkREVERIFIED_AC;

		protected System.Web.UI.WebControls.TextBox txtFEDERAL_ID_HID;
		protected System.Web.UI.WebControls.TextBox txtDFI_ACC_NO_HID;
		protected System.Web.UI.WebControls.TextBox txtTRANSIT_ROUTING_NO_HID;

		protected System.Web.UI.WebControls.Label  capENCRYP_FEDERAL_ID;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid_ENCRYP_FEDERAL_ID;

		protected System.Web.UI.WebControls.Label  capENCRYP_TRANSIT_ROUTING_NO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid_ENCRYP_TRANSIT_ROUTING_NO;

		protected System.Web.UI.WebControls.Label  capENCRYP_DFI_ACC_NO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid_ENCRYP_DFI_ACC_NO;

	    //Added For AB By Raghav For Itrack Issue 4829
		protected System.Web.UI.HtmlControls.HtmlTableRow trBillingPlan;
		//Added By Raghav For Itrack Issue #4998
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCARD_TYPE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_COUNTRY_LIST;//Added on 25 Nov 2008 by Sibin
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID; //Added on 25 Nov 2008 by Sibin

		protected System.Web.UI.WebControls.Image imgZipLookup;
		

		public const string POLICY_STATUS = "INACTIVE";
		public string ServiceURL; 
		System.Resources.ResourceManager objResourceMgr;
		#endregion
		
			const string POLICY_STATUS_UREINST = "UREINST";
			const string POLICY_STATUS_URENEW = "URENEW";
			const string POLICY_STATUS_UNDERNONRENEW = "UNDERNONRENEW";
			const string POLICY_STATUS_UISSUE = "UISSUE";
			const string POLICY_STATUS_SUSPENDED = "SUSPENDED";
            const string POLICY_STATUS_RSUSPENSE = "RSUSPENSE";
			const string POLICY_STATUS_UREWRITE = "UREWRITE";
			const string POLICY_STATUS_REWRTSUSP = "REWRTSUSP";
		

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(InstallmentInfo));
			//set screen id
			base.ScreenId="1001";

			
			GetQueryStringValues();

			if(IsPolicyActive())
				gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			

			base.InitializeSecuritySettings(); 

			btnAdjust.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnAdjust.PermissionString = gstrSecurityXML;

			btnSave.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 30 Oct 08
			btnSave.PermissionString = gstrSecurityXML;

			btnPullCustomerAddress.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString =	gstrSecurityXML;

			revTRANSIT_ROUTING_NO.ValidationExpression = aRegExpInteger;	
			revFEDERAL_ID.ValidationExpression = aRegExpFederalID;//aRegExpDoublePositiveNonZero;
			revFEDERAL_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("434");

			btnAdjust.Attributes.Add("onclick","javascript:return ChkValidators();");
			

			GetCustomerInfo();
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.InstallmentInfo" ,System.Reflection.Assembly.GetExecutingAssembly());

			btnPullCustomerAddress.Attributes.Add("onclick","javascript:return FillCustomerName();");
			
			ServiceURL =System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
			ServiceURL+="webservices/wscmsweb.asmx?WSDL";

			if(!Page.IsPostBack)
			{
				BindGrid();
				FillCombos();
				SetCaptions();
				CheckDownPaymentMode();
				GetEFTInfo();
				//GetCreditCardInfo();
				GetErrorMessages();

				imgZipLookup.Attributes.Add("style","cursor:hand");
				base.VerifyAddress(hlkZipLookup, txtCUSTOMER_ADDRESS1,txtCUSTOMER_ADDRESS2
					, txtCUSTOMER_CITY, cmbCUSTOMER_STATE, txtCUSTOMER_ZIP);

				FetchCountryState(hidSTATE_COUNTRY_LIST.Value);
			}		
	
			btnSave.Attributes.Add("Onclick","javascript:fillstateFromCountry();DisableZipForCanada();");
			cmbCOUNTRY_Changed();
			
		}
		#endregion
		#region
		private bool IsPolicyActive()
		{
			//GET ACTIVE POLICY
			DataSet dsPolicy = null;

			try
			{
				ClsGeneralInformation objGenInfo=new ClsGeneralInformation();
				dsPolicy =	objGenInfo.GetPolicyDetails(int.Parse(hidCUSTOMER_ID.Value.ToString()),0,0,int.Parse(hidPOLICY_ID.Value.ToString()),int.Parse(hidPOLICY_VERSION_ID.Value.ToString()));
				string isActiveStatus="";
				if (dsPolicy.Tables[0].Rows.Count >0)
				{
					if ( dsPolicy.Tables[0].Rows[0]["POLICY_STATUS"]!=null)
						isActiveStatus= dsPolicy.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
				}
				if(isActiveStatus.ToUpper() == POLICY_STATUS)
					return(false);		//Inactive POL use Base Security XML;
				else
					return(true);		//Active Policies User defined Security XML;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				throw(ex);
			}
			finally
			{
				if(dsPolicy!=null)
				{
					dsPolicy.Clear();
					dsPolicy.Dispose(); 
				}
			}
					
			
			
		}

		#endregion

		#region Helper Functions : Get Querystring / CheckDownPayMode / Error Messages / Bindgrid /FillPlan / GetformValue / GetCustomerInfo
		private void GetQueryStringValues()
		{
			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")			
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString(); 
			else
				hidCUSTOMER_ID.Value = "0"; 
			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="")			
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString(); 
			else
				hidPOLICY_ID.Value = "0"; 
			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="")			
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString(); 
			else
				hidPOLICY_VERSION_ID.Value = "0"; 
			if(Request.QueryString["POLICY_LOB"]!=null && Request.QueryString["POLICY_LOB"].ToString()!="")			
				hidPOLICY_LOB.Value = Request.QueryString["POLICY_LOB"].ToString(); 
			else
				hidPOLICY_LOB.Value = "0"; 
		}

		private void SetCaptions()
		{
			capINSTALL_PLAN_ID.Text		=  objResourceMgr.GetString("cmbINSTALL_PLAN_ID");
			capFEDERAL_ID.Text			=  objResourceMgr.GetString("txtFEDERAL_ID");
			capDFI_ACCT_NUMBER.Text		=  objResourceMgr.GetString("txtDFI_ACC_NO");
			capTRAN_ROUT_NUMBER.Text	=  objResourceMgr.GetString("txtTRANSIT_ROUTING_NO");	
			capEFT_TENTATIVE_DATE.Text	=  objResourceMgr.GetString("txtEFT_TENTATIVE_DATE");

			capCARD_CVV_NUMBER.Text		= objResourceMgr.GetString("txtCARD_CVV_NUMBER");
			capCARD_DATE_VALID_TO.Text	= objResourceMgr.GetString("txtCARD_DATE_VALID_TO");
			capCARD_NO.Text  			= objResourceMgr.GetString("txtCARD_NO");
			capCARD_TYPE.Text  			= objResourceMgr.GetString("cmbCARD_TYPE");
			//Cust Info
			capCUSTOMER_FIRST_NAME.Text		= objResourceMgr.GetString("txtCUSTOMER_FIRST_NAME");
			capCUSTOMER_MIDDLE_NAME.Text		= objResourceMgr.GetString("txtCUSTOMER_MIDDLE_NAME");
			capCUSTOMER_LAST_NAME.Text			= objResourceMgr.GetString("txtCUSTOMER_LAST_NAME");
			capCUSTOMER_ADDRESS1.Text		= objResourceMgr.GetString("txtCUSTOMER_ADDRESS1");
			capCUSTOMER_ADDRESS2.Text		= objResourceMgr.GetString("txtCUSTOMER_ADDRESS2");
			capCUSTOMER_CITY.Text			= objResourceMgr.GetString("txtCUSTOMER_CITY");
			capCUSTOMER_COUNTRY.Text			= objResourceMgr.GetString("cmbCUSTOMER_COUNTRY");
			capCUSTOMER_STATE.Text			= objResourceMgr.GetString("cmbCUSTOMER_STATE");
			capCUSTOMER_ZIP.Text				= objResourceMgr.GetString("txtCUSTOMER_ZIP");
			capACCOUNT_TYPE.Text				= objResourceMgr.GetString("cmbACCOUNT_TYPE");
			capREVERIFIED_AC.Text				= objResourceMgr.GetString("chkREVERIFIED_AC");	
		}



		private void CheckDownPaymentMode()
		{
			if(strPlanPayMode == DOWNPAYMENT_MODE_EFT || strModeDownPay == DOWNPAYMENT_MODE_EFT)
			{
				pnlEFTCust.Visible = true;
				btnSave.Visible    = true;
				//Modified On 20 June 2008
				pnlCCMessage.Visible = false;
				hidEFTFlag.Value  = "1";
			}
			else
				pnlEFTCust.Visible = false;

			if(strPlanPayMode == DOWNPAYMENT_MODE_CREDITCARD || strModeDownPay == DOWNPAYMENT_MODE_CREDITCARD)
			{
				//pnlCCCust.Visible = true;
				//btnSave.Visible    = true;
				GetCreditCardInfo();
			}
			else
			{
				pnlCCCust.Visible = false;
				pnlCCMessage.Visible = false;
			}

			if(strInstallmentType == "FULLPAY")
			{
				pnlEFTCust.Visible = false;
				pnlCCCust.Visible  = false;
				btnSave.Visible    = false;
			}
			//Commented on 24 June 2008
			//if(strPlanPayMode.Equals(DOWNPAYMENT_MODE_CHECK))
			//	btnSave.Visible    = false;
			//Added on 24 June 2008
			if(strPlanPayMode == DOWNPAYMENT_MODE_CHECK && strModeDownPay != DOWNPAYMENT_MODE_CREDITCARD)
				btnSave.Visible    = false;
		}

		private void GetErrorMessages()
		{
			rfvTRANSIT_ROUTING_NO.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("927");
			rfvDFI_ACC_NO.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("928");
			rngEFT_TENTATIVE_DATE.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("929");
			revDFI_ACC_NO.ValidationExpression		= aRegExpAlphaNumWithDash;
			revDFI_ACC_NO.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("903");
			rfvEFT_TENTATIVE_DATE.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("930");
			revCARD_CVV_NUMBER.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");	
			revCARD_CVV_NUMBER.ValidationExpression = aRegExpInteger;
			revCARD_NO.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");	
			revCARD_NO.ValidationExpression			=	aRegExpInteger;
			csvCARD_NO.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("955");	
			csvCARD_CVV_NUMBER.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("959");


			rngCARD_DATE_VALID_TO.MinimumValue		=	"0";
			rngCARD_DATE_VALID_TO.MaximumValue		=	"99";

			
			cmpCARD_DATE_VALID_TO.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"5");
			csvCARD_DATE_VALID_TO.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"7");
			rfvCARD_NO.ErrorMessage					=	ClsMessages.GetMessage(base.ScreenId,"9");
			rfvCARD_DATE_VALID_TO.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"11");
			rfvCARD_TYPE.ErrorMessage				=	ClsMessages.GetMessage(base.ScreenId,"12");
			rfvCARD_CVV_NUMBER.ErrorMessage			=	ClsMessages.GetMessage(base.ScreenId,"13");
			csvCARD_CVV_NUMBER.ErrorMessage			=	ClsMessages.GetMessage(base.ScreenId,"20");  
			//Customer Info
			rfvCUSTOMER_FIRST_NAME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"57");
			revCUSTOMER_ZIP.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
			revCUSTOMER_FIRST_NAME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"13");
			rfvCUSTOMER_LAST_NAME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"58");
			revCUSTOMER_FIRST_NAME.ValidationExpression		= aRegExpAlphaNum;
			revCUSTOMER_ZIP.ValidationExpression			= aRegExpZip;
		}
	
		private void BindGrid()
		{
			DataSet dsPolicyInstallInfo = null;
			ClsInstallmentInfo objInstallmentInfo = null;
			try
			{
				objInstallmentInfo = new ClsInstallmentInfo();
				dsPolicyInstallInfo = objInstallmentInfo.GetPolicyInstallmentInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
				
				
				if(dsPolicyInstallInfo!=null && dsPolicyInstallInfo.Tables.Count>0 && dsPolicyInstallInfo.Tables[0]!=null && dsPolicyInstallInfo.Tables[0].Rows.Count>0)
				{
					strBillType = dsPolicyInstallInfo.Tables[0].Rows[0]["BILL_TYPE"].ToString();
					strInstallmentType  = dsPolicyInstallInfo.Tables[0].Rows[0]["INSTALLMENT_PLAN"].ToString();
					strPolicyStatus = dsPolicyInstallInfo.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
					intPolicyTerm = Convert.ToInt32(dsPolicyInstallInfo.Tables[0].Rows[0]["POLICY_TERMS"]);
					Cache["PolicyTerm"] = intPolicyTerm.ToString();
					intInstallPlanID = Convert.ToInt32(dsPolicyInstallInfo.Tables[0].Rows[0]["INSTALL_PLAN_ID"]);
					Cache["InstallPlanID"] = intInstallPlanID.ToString();
					intBillTypeID = Convert.ToInt32(dsPolicyInstallInfo.Tables[0].Rows[0]["BILL_TYPE_ID"]);
					Cache["BillTypeID"] = intBillTypeID.ToString();
					strModeDownPay= dsPolicyInstallInfo.Tables[0].Rows[0]["MODE_OF_DOWNPAY"].ToString().Trim();
					strPlanPayMode= dsPolicyInstallInfo.Tables[0].Rows[0]["PLAN_PAYMENT_MODE"].ToString().Trim();
					hidBILL_TYPE.Value = strBillType;
					if(dsPolicyInstallInfo.Tables[1].Rows.Count > 0)
					{
						hidDAY.Value = dsPolicyInstallInfo.Tables[1].Rows[0]["DAY_PART"].ToString().Trim();
						//Added For Itrack Issue #6888.
						if(hidDAY.Value != null && hidDAY.Value != "0")
						{
							txtEFT_TENTATIVE_DATE.Text =  hidDAY.Value.ToString();
						}
						//End here 
					}
					if(strBillType!="DB")
					{
						//Response.Write("<script language='javascript'> alert('1'); </script>");
						lblMessage.Text = "Billing plan not applicable in case of AB policies.";
						lblMessage.Visible = true;
						//Added For AB By Raghav For Itrack Issue 4829
						btnAdjust.Visible = false;
						//trBillingPlan.Visible = false;
						capINSTALL_PLAN_ID.Visible = false;
						cmbINSTALL_PLAN_ID.Visible = false; 
						btnSave.Visible = false;
						return;
					}
				}
				
				if(strInstallmentType == "FULLPAY")
				{
					lblMessage.Text = "Policy is under FullPay Plan";
					lblMessage.Visible = true;
					dgPolicyInstallInfo.Visible= false;
				}
				else
				{

					if(dsPolicyInstallInfo!=null && dsPolicyInstallInfo.Tables.Count>0 && dsPolicyInstallInfo.Tables[1]!=null && dsPolicyInstallInfo.Tables[1].Rows.Count>0)
					{
						
						//dgPolicyInstallInfo.DataSource = GetPaymentBreakdownData(dsPolicyInstallInfo.Tables[1]);
						if(dsPolicyInstallInfo.Tables[1].Rows.Count >0 && dsPolicyInstallInfo.Tables[1].Rows[0]["PERCENTAG_OF_PREMIUM"] != null 
							&& dsPolicyInstallInfo.Tables[1].Rows[0]["PERCENTAG_OF_PREMIUM"].ToString().Trim() == "NA")
						{
							dgPolicyInstallInfo.Columns[6].Visible = false ; 
						}
						dgPolicyInstallInfo.DataSource = dsPolicyInstallInfo.Tables[1];
						dgPolicyInstallInfo.DataBind();
						
					}
					dgPolicyInstallInfo.Visible= true;
				}
				
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(dsPolicyInstallInfo!=null)
					dsPolicyInstallInfo = null;
				if(objInstallmentInfo!=null)
					objInstallmentInfo = null;
			}
		}
		private void FillCombos()
		{
			// Cache Objects have been used to preserve the CssClass in the dropdown.
			// This was done because the value of intPolicyTerm & intInstallPlanID was not
			// coming from BindDataGrid() into this function.
			DataSet ds = null;
			if(Cache["PolicyTerm"] != null && Cache["PolicyTerm"].ToString() != "")
			{
				int intPolTerm = int.Parse(Cache["PolicyTerm"].ToString());
				ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(intPolTerm,int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),"POL") ;
			}
			else
				ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(intPolicyTerm,int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),"POL") ;
		
			int i=0;
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				ListItem listItem = null;
				cmbINSTALL_PLAN_ID.Items.Add(new ListItem(row["BILLING_PLAN"].ToString(),row["INSTALL_PLAN_ID"].ToString()));
				if(Cache["InstallPlanID"] != null && Cache["InstallPlanID"].ToString() != "")
					listItem = cmbINSTALL_PLAN_ID.Items.FindByValue(Cache["InstallPlanID"].ToString());
				else
					listItem = cmbINSTALL_PLAN_ID.Items.FindByValue(intInstallPlanID.ToString());
				cmbINSTALL_PLAN_ID.SelectedIndex= cmbINSTALL_PLAN_ID.Items.IndexOf(listItem);
				if(row["IS_ACTIVE"] != DBNull.Value) 
				{
					if(row["IS_ACTIVE"].ToString() == "N") 
						cmbINSTALL_PLAN_ID.Items[i].Attributes.Add ("Class","DeactivatedInstallmentPlan");

				}
				i++;
		}
			#region COMMENTED CODE
			/*
			cmbINSTALL_PLAN_ID.DataSource =ClsInstallmentInfo.GetApplicableInstallmentPlans(intPolicyTerm,int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),"POL");
			cmbINSTALL_PLAN_ID.DataTextField = "BILLING_PLAN";
			cmbINSTALL_PLAN_ID.DataValueField = "INSTALL_PLAN_ID";
			cmbINSTALL_PLAN_ID.DataBind();
			
			ListItem listItem = cmbINSTALL_PLAN_ID.Items.FindByValue(intInstallPlanID.ToString());
			cmbINSTALL_PLAN_ID.SelectedIndex= cmbINSTALL_PLAN_ID.Items.IndexOf(listItem);*/
			#endregion
			
			#region BILL TYYPE MORTAGAGEE
			if(Cache["BillTypeID"].ToString() == BILLTYPEID_MORTGAGEE)
			{
				cmbINSTALL_PLAN_ID.Disabled= true;
				btnAdjust.Visible = false;
			}
			#endregion

			cmbCARD_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CCTYPE");
			cmbCARD_TYPE.DataTextField="LookupDesc";
			cmbCARD_TYPE.DataValueField="LookupID";
			cmbCARD_TYPE.DataBind();
			cmbCARD_TYPE.Items.Insert(0,"");

			//DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
			cmbCUSTOMER_COUNTRY.DataSource			= dt;
			cmbCUSTOMER_COUNTRY.DataTextField		= COUNTRY_NAME;
			cmbCUSTOMER_COUNTRY.DataValueField		= COUNTRY_ID;
			cmbCUSTOMER_COUNTRY.DataBind();

			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbCUSTOMER_STATE.DataSource			= dt;
			cmbCUSTOMER_STATE.DataTextField			= STATE_NAME;
			cmbCUSTOMER_STATE.DataValueField		= STATE_ID;
			cmbCUSTOMER_STATE.DataBind();
			cmbCUSTOMER_STATE.Items.Insert(0,"");
		}


		private void cmbCOUNTRY_Changed()
		{
			try
			{
				if(cmbCUSTOMER_COUNTRY.SelectedItem!=null && cmbCUSTOMER_COUNTRY.SelectedItem.Value!="")
				{
					PopulateStateDropDown(int.Parse(cmbCUSTOMER_COUNTRY.SelectedItem.Value));										
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
			}
		}

		private void FetchCountryState(string strXML)
		{
			string strSelectedState = ClsCommon.FetchValueFromXML("STATE",strXML);
			string strSelectedCountry = ClsCommon.FetchValueFromXML("COUNTRY",strXML);
			if(strSelectedCountry!="" && strSelectedCountry!="0")
			{
				PopulateStateDropDown(int.Parse(strSelectedCountry));				
			}
			else
				PopulateStateDropDown(1);				
		}

		private void PopulateStateDropDown(int COUNTRY_ID)
		{
			ClsStates objStates = new ClsStates();
			DataSet dsStates;
			if(COUNTRY_ID==0)
				return;
			else
			{
				dsStates = objStates.GetStatesCountry(COUNTRY_ID);
				hidSTATE_COUNTRY_LIST.Value=ClsCommon.GetXMLEncoded(dsStates.Tables[0]);
			}
			cmbCUSTOMER_STATE.Items.Clear();
			DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
			if(dtStates!=null && dtStates.Rows.Count>0)
			{
				cmbCUSTOMER_STATE.DataSource = dtStates;
				cmbCUSTOMER_STATE.DataTextField			= STATE_NAME;
				cmbCUSTOMER_STATE.DataValueField		= STATE_ID;
				cmbCUSTOMER_STATE.DataBind();				
			}
			if(COUNTRY_ID!=1)
			{
				revCUSTOMER_ZIP.Enabled = false;										
			}
			else
			{
				revCUSTOMER_ZIP.Enabled = true;							
			}
		}

		private void GetCreditCardInfo()
		{
			DataSet dsCCInfo = null;
			ClsCreditCard objCC = null;
			string payPalId = "";
			try
			{
				objCC = new ClsCreditCard();
				dsCCInfo = objCC.GetPolCCCustInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));

				if(dsCCInfo!=null && dsCCInfo.Tables.Count>0 && dsCCInfo.Tables[0]!=null && dsCCInfo.Tables[0].Rows.Count>0)
				{
					if(dsCCInfo.Tables[0].Rows[0]["PAY_PAL_REF_ID"] != System.DBNull.Value)
					{
						payPalId = dsCCInfo.Tables[0].Rows[0]["PAY_PAL_REF_ID"].ToString();
					}
					if(payPalId!="")
					{
						pnlCCMessage.Visible = true;
						pnlCCCust.Visible  = true; //We can Access Div in JS
						btnSave.Visible    = true; //We can Access cntrl in JS
						hidCCFlag.Value = "1";
					}
					else
					{
						pnlCCMessage.Visible = false;
						pnlCCCust.Visible = true;
						btnSave.Visible    = true;		
						hidCCFlag.Value  = "0";
					}
		
				}	
				

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(dsCCInfo!=null)
					dsCCInfo = null;
				if(objCC!=null)
					objCC = null;
			}

		}

		private void GetEFTInfo()
		{
			DataSet dsEFTInfo = null;
			ClsEFT objEFT = null;
			string strDFIAccNo = "";
			string strAcctType = "";
			//int fedID = 0;
			string tranRoutNo = "";
			int EftDate = 0;
			try
			{
				objEFT = new ClsEFT();
				dsEFTInfo = objEFT.GetPolEFTCustInfo(int.Parse(hidCUSTOMER_ID.Value),
					int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));

				if(dsEFTInfo!=null && dsEFTInfo.Tables.Count>0 && dsEFTInfo.Tables[0]!=null && dsEFTInfo.Tables[0].Rows.Count>0)
				{
					if(dsEFTInfo.Tables[0].Rows[0]["FEDERAL_ID"] != System.DBNull.Value)
					{
						//fedID = int.Parse(dsEFTInfo.Tables[0].Rows[0]["FEDERAL_ID"].ToString());
						//txtFEDERAL_ID.Text = BusinessLayer.BlCommon.ClsCommon.DecryptString(dsEFTInfo.Tables[0].Rows[0]["FEDERAL_ID"].ToString());
						SetFEDERAL_ID(dsEFTInfo.Tables[0].Rows[0]["FEDERAL_ID"].ToString().Trim());
					}
					if(dsEFTInfo.Tables[0].Rows[0]["DFI_ACC_NO"] != System.DBNull.Value )
					{
						strDFIAccNo  = dsEFTInfo.Tables[0].Rows[0]["DFI_ACC_NO"].ToString();
						//txtDFI_ACC_NO.Text = BusinessLayer.BlCommon.ClsCommon.DecryptString(strDFIAccNo);
						SetDFI_ACC_NO(strDFIAccNo);

					}
					if(dsEFTInfo.Tables[0].Rows[0]["TRANSIT_ROUTING_NO"] != System.DBNull.Value)
					{
						tranRoutNo = dsEFTInfo.Tables[0].Rows[0]["TRANSIT_ROUTING_NO"].ToString().Trim();
						//txtTRANSIT_ROUTING_NO.Text = BusinessLayer.BlCommon.ClsCommon.DecryptString(tranRoutNo.ToString());
						SetTRANSIT_ROUTING_NO(tranRoutNo);

					}
					if(dsEFTInfo.Tables[0].Rows[0]["ACCOUNT_TYPE"] != System.DBNull.Value)
					{
						strAcctType = dsEFTInfo.Tables[0].Rows[0]["ACCOUNT_TYPE"].ToString();
						if(strAcctType.ToString() == Convert.ToString((int)Cms.BusinessLayer.BlAccount.EFTCodes.CheckingAccount))
							rdbACC_CASH_ACC_TYPEO.Checked = true;
						else
							rdbACC_CASH_ACC_TYPET.Checked = true;
						
					}
					if(dsEFTInfo.Tables[0].Rows[0]["EFT_TENTATIVE_DATE"] != System.DBNull.Value)
					{
						EftDate = Convert.ToInt32(dsEFTInfo.Tables[0].Rows[0]["EFT_TENTATIVE_DATE"]);
						txtEFT_TENTATIVE_DATE.Text = EftDate.ToString();

					}
					//Reverify EFT
					if(dsEFTInfo.Tables[0].Rows[0]["IS_VERIFIED"] != System.DBNull.Value)
					{
						lblIS_VERIFIED.Text = dsEFTInfo.Tables[0].Rows[0]["IS_VERIFIED"].ToString();
					}
					if(dsEFTInfo.Tables[0].Rows[0]["VERIFIED_DATE"] != System.DBNull.Value)
					{
						lblVERIFIED_DATE.Text = dsEFTInfo.Tables[0].Rows[0]["VERIFIED_DATE"].ToString();
					}

					if(dsEFTInfo.Tables[0].Rows[0]["REVERIFIED_AC"] != System.DBNull.Value)
					{
						int intYes = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString());
						if(dsEFTInfo.Tables[0].Rows[0]["REVERIFIED_AC"].ToString() == intYes.ToString())
							chkREVERIFIED_AC.Checked = true;
						else
							chkREVERIFIED_AC.Checked = false;
					}


					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsEFTInfo.Tables[0]);
		
				}
				else
				{
					hidOldData.Value = "";
					txtEFT_TENTATIVE_DATE.Text = hidDAY.Value.ToString().Trim();
				}

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(dsEFTInfo!=null)
					dsEFTInfo = null;
				if(objEFT!=null)
					objEFT = null;
			}

		}

		private ClsEFTInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsEFTInfo objEFTInfo;
			objEFTInfo = new ClsEFTInfo();

			objEFTInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value.ToString());
			objEFTInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value.ToString());
			objEFTInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value.ToString());

			/*if(txtFEDERAL_ID.Text!="")
				objEFTInfo.FEDERAL_ID = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtFEDERAL_ID.Text.ToString());

			if(txtDFI_ACC_NO.Text!="")
				objEFTInfo.DFI_ACC_NO = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtDFI_ACC_NO.Text.ToString());

			if(txtTRANSIT_ROUTING_NO.Text!="")
				objEFTInfo.TRANSIT_ROUTING_NO = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtTRANSIT_ROUTING_NO.Text.ToString().Trim());*/

			if(txtFEDERAL_ID.Text.Trim()!="")
			{
				objEFTInfo.FEDERAL_ID = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtFEDERAL_ID.Text.ToString());
				txtFEDERAL_ID.Text = "";
			}
			else
				objEFTInfo.FEDERAL_ID		= hid_ENCRYP_FEDERAL_ID.Value;


			if(txtDFI_ACC_NO.Text!="")
			{
				objEFTInfo.DFI_ACC_NO = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtDFI_ACC_NO.Text.ToString());
				txtDFI_ACC_NO.Text = "";

			}
			else
				objEFTInfo.DFI_ACC_NO		= hid_ENCRYP_DFI_ACC_NO.Value;


			if(txtTRANSIT_ROUTING_NO.Text!="")
			{
				objEFTInfo.TRANSIT_ROUTING_NO = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtTRANSIT_ROUTING_NO.Text.ToString().Trim());
				txtTRANSIT_ROUTING_NO.Text ="";
			}
			else
				objEFTInfo.TRANSIT_ROUTING_NO	= hid_ENCRYP_TRANSIT_ROUTING_NO.Value;


			if(rdbACC_CASH_ACC_TYPEO.Checked)//Checking
				objEFTInfo.ACCOUNT_TYPE = Convert.ToString((int)Cms.BusinessLayer.BlAccount.EFTCodes.CheckingAccount);
			else if(rdbACC_CASH_ACC_TYPET.Checked)//Saving
				objEFTInfo.ACCOUNT_TYPE = Convert.ToString((int)Cms.BusinessLayer.BlAccount.EFTCodes.SavingAccount);

			if(txtEFT_TENTATIVE_DATE.Text !="")
				objEFTInfo.EFT_TENTATIVE_DATE = int.Parse(txtEFT_TENTATIVE_DATE.Text);

			// Credit Card Info
			if(txtCARD_NO.Text !="")
				objEFTInfo.CARD_NO = txtCARD_NO.Text;

			if(txtCARD_CVV_NUMBER.Text !="")
				objEFTInfo.CARD_CVV_NUMBER = txtCARD_CVV_NUMBER.Text.ToString();

//			string strFromDate = cmbCARD_DATE_VALID_FROM.SelectedValue + txtCARD_DATE_VALID_FROM.Text;
//			objEFTInfo.CARD_DATE_VALID_FROM = strFromDate;	

			string strToDate = cmbCARD_DATE_VALID_TO.SelectedValue + txtCARD_DATE_VALID_TO.Text;
			objEFTInfo.CARD_DATE_VALID_TO = strToDate;

			if (cmbCARD_TYPE.SelectedValue != null && cmbCARD_TYPE.SelectedValue != "")
			{
				objEFTInfo.CARD_TYPE = int.Parse(cmbCARD_TYPE.SelectedValue);
			}
			objEFTInfo.CREATED_BY = int.Parse(GetUserId());

			//Customer Info : 
			if(txtCUSTOMER_FIRST_NAME.Text != "")
				objEFTInfo.CUSTOMER_FIRST_NAME = txtCUSTOMER_FIRST_NAME.Text.ToString().Trim();
			if(txtCUSTOMER_MIDDLE_NAME.Text != "")
				objEFTInfo.CUSTOMER_MIDDLE_NAME = txtCUSTOMER_MIDDLE_NAME.Text.ToString().Trim();
			if(txtCUSTOMER_LAST_NAME.Text != "")
				objEFTInfo.CUSTOMER_LAST_NAME = txtCUSTOMER_LAST_NAME.Text.ToString().Trim();

			if(txtCUSTOMER_ADDRESS1.Text !="")
				objEFTInfo.CUSTOMER_ADDRESS1 = txtCUSTOMER_ADDRESS1.Text.ToString().Trim();

			if(txtCUSTOMER_ADDRESS2.Text !="")
				objEFTInfo.CUSTOMER_ADDRESS2 = txtCUSTOMER_ADDRESS2.Text.ToString().Trim();

			objEFTInfo.CUSTOMER_COUNTRY = cmbCUSTOMER_COUNTRY.SelectedValue.ToString();
			if(txtCUSTOMER_CITY.Text!="")
				objEFTInfo.CUSTOMER_CITY = txtCUSTOMER_CITY.Text.ToString().Trim();

			objEFTInfo.CUSTOMER_STATE = cmbCUSTOMER_STATE.SelectedValue.ToString();

			if(txtCUSTOMER_ZIP.Text!="")
				objEFTInfo.CUSTOMER_ZIP = txtCUSTOMER_ZIP.Text.ToString().Trim();

			//Reverify Model Objects
			if(chkREVERIFIED_AC.Checked == true)
				objEFTInfo.REVERIFIED_AC = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()); //YES
			else
				objEFTInfo.REVERIFIED_AC = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()); //NO

			return objEFTInfo;
		}
		private void GetCustomerInfo()
		{
			if (hidCUSTOMER_ID.Value != "")
			{
				hidCUSTOMER_INFO.Value=Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetCustomerNameXML(int.Parse(hidCUSTOMER_ID.Value));
			}
			
		}
		#endregion
		
		#region Event Handlers : Adjust / Save
		private void btnAdjust_Click(object sender, System.EventArgs e)
		{
			//Reset the variables CC and EFT
			hidCCFlag.Value = "";
			hidEFTFlag.Value = "";
			try
			{ 
				ClsEFTInfo objEFTInfo = GetFormValue();
				ClsInstallmentInfo objInstall = new ClsInstallmentInfo();
				int intNewPlanID = 0,intRetVal = 0,oldPlanID = 0;
				intNewPlanID = Convert.ToInt32(cmbINSTALL_PLAN_ID.Value.ToString().Trim() );
				string strOldInstPlanID  = "";
				
				if(Cache["InstallPlanID"].ToString()!="")
				{
					oldPlanID = Convert.ToInt32(Cache["InstallPlanID"].ToString().Trim());
					if(intNewPlanID != oldPlanID)
					{
						ListItem listItem = null;
						listItem = cmbINSTALL_PLAN_ID.Items.FindByValue(Cache["InstallPlanID"].ToString());
						strOldInstPlanID  = listItem.Text.ToString();
					}
				}
				
				if(intNewPlanID != 0 && intNewPlanID != -1)
				{

					intRetVal =  objInstall.ReadjustInstallmentPlan(objEFTInfo,int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),intNewPlanID,GetUserId(),cmbINSTALL_PLAN_ID.Items[cmbINSTALL_PLAN_ID.SelectedIndex].Text,strOldInstPlanID); 

					if(intRetVal == -1)
					{
						lblMessage.Visible= true;
						lblMessage.Text = "Selected Installment plan is not available any more.";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Visible= true;
						lblMessage.Text = "You are not eligible to opt this plan because of Minimum Premium limit.";
					}
					else if(intRetVal == 1 || intRetVal == 2 )
					{
						//Showing the endorsement popup window
						base.OpenEndorsementDetails();

						lblMessage.Visible= true;
						lblMessage.Text = "Installment plan changed successfully.";
						BindGrid();
						CheckDownPaymentMode();
						cmbINSTALL_PLAN_ID.Items.Clear();
						FillCombos();
					}
					else
					{
						lblMessage.Visible= true;
						lblMessage.Text = "Plan cannot be readjusted. " ;
					}
				}

			}
			catch(Exception ex)
			{
				lblMessage.Visible= true;
				lblMessage.Text = "Plan can't be readjusted " + ex.Message ;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}
		
		}
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=0;

				ClsEFT objEFT = new  ClsEFT();
				ClsCreditCard objCCBL = new  ClsCreditCard();

                objCCBL.PayPalAPI.UserName = ConfigurationManager.AppSettings.Get("PayPalUserName");
                objCCBL.PayPalAPI.VendorName = ConfigurationManager.AppSettings.Get("PayPalVendor");
                objCCBL.PayPalAPI.HostName = ConfigurationManager.AppSettings.Get("HostName");
                objCCBL.PayPalAPI.PartnerName = ConfigurationManager.AppSettings.Get("PaypalPartner");
                objCCBL.PayPalAPI.Password = ConfigurationManager.AppSettings.Get("PayPalPassword");

				ClsEFTInfo objEFTInfo = GetFormValue();

				objEFTInfo.CREATED_BY = int.Parse(GetUserId());
				objEFTInfo.CREATED_DATETIME = DateTime.Now;
				objEFTInfo.MODIFIED_BY = int.Parse(GetUserId());
				objEFTInfo.LAST_UPDATED_DATETIME = DateTime.Now;

				
				ClsEFTInfo objOldEFTInfo = new ClsEFTInfo();

				//Retreiving the form values into model class object
				//ClsEFT objEFT = new  ClsEFT();
				//ClsCreditCard objCCBL = new  ClsCreditCard();
				//ClsEFTInfo objEFTInfo = GetFormValue();
				//ClsEFTInfo objOldEFTInfo = new ClsEFTInfo();

				if(hidOldData.Value!="" && hidOldData.Value!="0")
					base.PopulateModelObject(objOldEFTInfo,hidOldData.Value);


				if(pnlCCCust.Visible == true)
				{
					if(txtCUSTOMER_FIRST_NAME.Text.ToString().Trim()!="") //Mandatory Field
					{
						PayPalResponse objResponse = objCCBL.PolSave(objEFTInfo,int.Parse(GetUserId()));
						if(Convert.ToInt32(objResponse.Result.Trim()) == (int)PayPalResult.Approved) 
						{
							lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"17");
							lblMessage.Visible	=	true;
							GetCreditCardInfo();
						}
						else
						{
							lblMessage.Text		=	"Unable to update Credit Card info: " + objResponse.ResponseMessage; 
							lblMessage.Visible	=	true;
						}
					}

				}

				//Calling the add method of business layer class
				if(hidOldData.Value!="" && hidOldData.Value!="0")
				{
					if(pnlEFTCust.Visible == true)
					intRetVal	= objEFT.SavePolicy(objEFTInfo,objOldEFTInfo);
					//if(pnlCCCust.Visible == true)
					//	intCCRetVal = objCCBL.PolSave(objEFTInfo,objOldEFTInfo);
				}

				else
				{
					if(pnlEFTCust.Visible == true)
					intRetVal = objEFT.SavePolicy(objEFTInfo,null);
					//if(pnlCCCust.Visible == true)
					//	intCCRetVal = objCCBL.PolSave(objEFTInfo,null);
				}
				cmbINSTALL_PLAN_ID.Items.Clear();
				FillCombos();
				//CC Info Messages
				/*(if(pnlCCCust.Visible == true)
				{
					if(intCCRetVal == 1)
					{
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"17");
						lblMessage.Visible	=	true;
						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
						GetCreditCardInfo();
							BindGrid();
					}
					else if(intCCRetVal == 2)
					{
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"18");
						lblMessage.Visible	=	true;
						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
						GetCreditCardInfo();
							BindGrid();
					}
					else //Error
					{
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"19");
						lblMessage.Visible	=	true;

					}
				}*/
				//EFT Info Messages
				if(pnlEFTCust.Visible == true)
				{
					if(intRetVal == 1)
					{
						lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"17");
						lblMessage.Visible	=	true;
						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
						GetEFTInfo();
						BindGrid();
					}
					else if(intRetVal == 2)
					{
						lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"18");
						lblMessage.Visible	=	true;
						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
						GetEFTInfo();
						BindGrid();
					}
					else //Error
					{
						lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"19");
						lblMessage.Visible	=	true;

					}
				}
			BindGrid();
	
			}
				
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				
			}
		}

		private void btnCCSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal = 0;	
				ClsCreditCard objCCBL = new  ClsCreditCard();
				ClsEFTInfo objEFTInfo = GetFormValue();
				ClsEFTInfo objOldEFTInfo = new ClsEFTInfo();
				if(hidOldData.Value!="" && hidOldData.Value!="0")
					base.PopulateModelObject(objOldEFTInfo,hidOldData.Value);

				objEFTInfo.CREATED_BY = int.Parse(GetUserId());
				objEFTInfo.CREATED_DATETIME = DateTime.Now;
				objEFTInfo.MODIFIED_BY = int.Parse(GetUserId());
				objEFTInfo.LAST_UPDATED_DATETIME = DateTime.Now;

				//Calling the add method of business layer class
				/*if(hidOldData.Value!="" && hidOldData.Value!="0")
					intRetVal = objCCBL.PolSave(objEFTInfo,objOldEFTInfo);
					
				else
					intRetVal = objCCBL.PolSave(objEFTInfo,null);*/
					
				if(intRetVal == 1)
				{
					//Showing the endorsement popup window
					base.OpenEndorsementDetails();
					lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"14");
					lblMessage.Visible	=	true;
					GetCreditCardInfo();
					FetchCountryState(hidSTATE_COUNTRY_LIST.Value);
				}
				else if(intRetVal == 2)
				{
					//Showing the endorsement popup window
					base.OpenEndorsementDetails();
					lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"15");
					lblMessage.Visible	=	true;
					GetCreditCardInfo();
				}
				else //Error
				{
					lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"16");
					lblMessage.Visible	=	true;

				}
	
			}
				
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				
			}
		}
		
		#endregion

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
			this.dgPolicyInstallInfo.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPolicyInstallInfo_ItemDataBound);
			this.btnAdjust.Click += new System.EventHandler(this.btnAdjust_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#region SET ENCRYPTION DECRYPTION
		/// <summary>
		/// ENCRYPT FEDERAL_ID / DFI / ROUTING NO
		/// </summary>
		/// <param name="fed_id"></param>
		/// <returns></returns>
		private void SetFEDERAL_ID(string fed_id)
		{
			string strFed_id = "";
			hid_ENCRYP_FEDERAL_ID.Value = fed_id;
			if(fed_id!="0" && fed_id!="")
			{
				strFed_id = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(fed_id);
			}
			else
				capENCRYP_FEDERAL_ID.Text = "";

			if(strFed_id != "")
			{
				string strvaln = "";
				for(int len=0; len < strFed_id.Length-4; len++)
					strvaln += "x";

				strvaln += strFed_id.Substring(strvaln.Length, strFed_id.Length - strvaln.Length);
				capENCRYP_FEDERAL_ID.Text = strvaln;
			}
			else
				capENCRYP_FEDERAL_ID.Text = "";
		}

		private void SetDFI_ACC_NO(string dfi_No)
		{
			string strdfi_No = "";
			hid_ENCRYP_DFI_ACC_NO.Value = dfi_No;
			if(dfi_No!="0" && dfi_No!="")
			{
				strdfi_No = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(dfi_No);
			}
			else
				capENCRYP_DFI_ACC_NO.Text = "";

			if(strdfi_No != "")
			{
				string strvaln = "";
				for(int len=0; len < strdfi_No.Length-4; len++)
					strvaln += "x";

				strvaln += strdfi_No.Substring(strvaln.Length, strdfi_No.Length - strvaln.Length);
				capENCRYP_DFI_ACC_NO.Text = strvaln;
			}
			else
				capENCRYP_DFI_ACC_NO.Text = "";
		}

		private void SetTRANSIT_ROUTING_NO(string rout_No)
		{

			string strRout_No ="";
			hid_ENCRYP_TRANSIT_ROUTING_NO.Value = rout_No;
			if(rout_No!="0" && rout_No!="")
				strRout_No = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(rout_No);
			else
				capENCRYP_TRANSIT_ROUTING_NO.Text = "";

			if(strRout_No != "")
			{
				string strvaln = "";
				for(int len=0; len < strRout_No.Length-4; len++)
					strvaln += "x";

				strvaln += strRout_No.Substring(strvaln.Length, strRout_No.Length - strvaln.Length);
				capENCRYP_TRANSIT_ROUTING_NO.Text = strvaln;
			}
			else
				capENCRYP_TRANSIT_ROUTING_NO.Text = "";
		}

		

		
		#endregion
		private void dgPolicyInstallInfo_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				if(strPolicyStatus.ToUpper() != POLICY_STATUS_REWRTSUSP && 
					strPolicyStatus.ToUpper() != POLICY_STATUS_RSUSPENSE && 
					strPolicyStatus.ToUpper() != POLICY_STATUS_SUSPENDED &&
					strPolicyStatus.ToUpper() != POLICY_STATUS_UISSUE &&
					strPolicyStatus.ToUpper() != POLICY_STATUS_UNDERNONRENEW &&
					strPolicyStatus.ToUpper() != POLICY_STATUS_UREINST && 
					strPolicyStatus.ToUpper() != POLICY_STATUS_URENEW &&
					strPolicyStatus.ToUpper() != POLICY_STATUS_UREWRITE)
				{
				
					e.Item.Attributes.Add("onclick", "ShowTooltip('" + 
						hidCUSTOMER_ID.Value + "','" + 
						DataBinder.Eval(e.Item.DataItem, "POLICY_ID").ToString() + "','" + 
						DataBinder.Eval(e.Item.DataItem, "INSTALLMENT_NO").ToString() + "','" + 
						DataBinder.Eval(e.Item.DataItem, "CURRENT_TERM").ToString() + 
						"');javascript:ChangeRowColor('" + e.Item.ClientID + "');");

					e.Item.Attributes.Add("onmouseover", "this.style.cursor='hand'"); 

					//e.Item.Attributes.Add("onmouseout",	"this.style.backgroundColor=this.originalstyle;");
					//string orginialbg = e.Item.BackColor.ToString();
					//e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" + "'");                				
					//e.Item.Attributes.Add("onclick", "this.style.backgroundColor='#FFC0C0'");
					
				}

			}
		}
		#endregion
		[Ajax.AjaxMethod()]
		public string AjaxFillState(string CountryID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FillState(CountryID);
			return result;
			
		}
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}
		[Ajax.AjaxMethod()]
		public string AjaxDetailBreakupXML(int cust_ID,int pol_ID,int install_No,int current_Term) //To be paramaterized
		{
			ClsInstallmentInfo objInstallmentInfo = new ClsInstallmentInfo();
			string result = "";
			DataSet objDataSet = null;
			objDataSet = objInstallmentInfo.GetInstallmentDetailBreakup(cust_ID,pol_ID,install_No,current_Term);

			//CREATE TABLE

			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

			strBuilder.Append("<table cellSpacing='1' cellPadding='1' border='0' width='100%' align='center'>");
			strBuilder.Append("<tr height='10'>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='5%'><b>Version</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='5%'><b>Risk ID</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='15%'><b>Trans Desc</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Total Due</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Left' width='10%'><b>Total Paid</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Net Premium</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Gross Premium</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='10%'><b>Processed Amount</b></td>");
			strBuilder.Append("<td class='headereffectWebGrid' align='Right' width='15%'><b>Comm Perc (%)</b></td>");		
			strBuilder.Append("</tr>");

			if(objDataSet!=null)
			{
				foreach(DataRow dr in objDataSet.Tables[0].Rows)
				{
						
					strBuilder.Append("<tr>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["POLICY_VERSION_ID"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["RISK_ID"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["TRANS_DESC"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["TOTAL_DUE"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["TOTAL_PAID"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["NET_PREMIUM"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["GROSS_AMOUNT"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["PROCESSED_AMOUNT_FOR_AGENCY"]+"</td>");
					strBuilder.Append("<td class='DataGridRow' align='Left'>"+dr["AGENCY_COMM_PERC"]+"</td>");
					strBuilder.Append("</tr>");		
			
					
				}
			}
			else
			{
				strBuilder.Append("<tr>");
				strBuilder.Append("<td class='DataGridRow' align='Left' colspan='11'>No Record Found.</td>");
				strBuilder.Append("</tr>");
				
			}

			strBuilder.Append("<tr>");
			strBuilder.Append("<td><input type=button value=Close onclick = 'JavaScript:HideTooltip();' class=clsButton></td>");
			strBuilder.Append("</tr>");
			strBuilder.Append("</table>");

			result = strBuilder.ToString();		

			return result;
			
		}

	}
}
