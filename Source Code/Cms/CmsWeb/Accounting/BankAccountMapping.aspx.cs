/******************************************************************************************
	<Author					: - Ashwani   
	<Start Date				: -	10 Oct 5:55:12 PM
	<End Date				: -	
	<Description			: - Posting interface - Bank A/C mapping.
	<Review Date			: - 
	<Reviewed By			: - 	
	Modification History
	<Modified Date			: - 
	<Modified By			: - 
	<Purpose				: - Posting interface - Bank A/C mapping.
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
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Summary description for BankAccountMapping.
	/// </summary>
	public class BankAccountMapping : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.DropDownList cmbBnk_Over_Payment;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBnk_Over_Payment;
		protected System.Web.UI.WebControls.DropDownList cmbBnk_Suspense_Amount;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBnk_Suspense_Amount;
		//protected System.Web.UI.WebControls.Label capChk_Suspense_Amount;
		//protected System.Web.UI.WebControls.Label capChk_Rtrn_Prm_Payemnt;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv_Bnk_Return_Prm_Payment;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
		//protected System.Web.UI.WebControls.Label capChk_OverPayment;
		private string strFormSaved,strRowId,oldXML;
		//Defining the business layer class object
		ClsGeneralLedger  objGeneralLedger ;
		protected System.Web.UI.WebControls.DropDownList cmbBnk_Return_Prm_Payment;
		protected System.Web.UI.WebControls.Label capBnk_Over_Payment;
		protected System.Web.UI.WebControls.Label capBnk_Suspense_Amount;
		protected System.Web.UI.WebControls.Label capBnk_Return_Prm_Payment;
		protected System.Web.UI.WebControls.Label capBNK_CLAIMS_DEFAULT_AC;
		protected System.Web.UI.WebControls.DropDownList cmbBNK_CLAIMS_DEFAULT_AC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBNK_CLAIMS_DEFAULT_AC;
		protected System.Web.UI.WebControls.Label capBNK_REINSURANCE_DEFAULT_AC;
		protected System.Web.UI.WebControls.DropDownList cmbBNK_REINSURANCE_DEFAULT_AC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBNK_REINSURANCE_DEFAULT_AC;
		protected System.Web.UI.WebControls.Label capBNK_DEPOSITS_DEFAULT_AC;
		protected System.Web.UI.WebControls.DropDownList cmbBNK_DEPOSITS_DEFAULT_AC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBNK_DEPOSITS_DEFAULT_AC;
		protected System.Web.UI.WebControls.Label capBNK_MISC_CHK_DEFAULT_AC;
		protected System.Web.UI.WebControls.DropDownList cmbBNK_MISC_CHK_DEFAULT_AC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBNK_MISC_CHK_DEFAULT_AC;
		protected System.Web.UI.WebControls.Label capCLM_CHECK_DEFAULT_AC;
		protected System.Web.UI.WebControls.DropDownList cmbCLM_CHECK_DEFAULT_AC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLM_CHECK_DEFAULT_AC;
		protected System.Web.UI.WebControls.Label capBNK_CUST_DEP_EFT_CARD;
		protected System.Web.UI.WebControls.DropDownList cmbBNK_CUST_DEP_EFT_CARD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBNK_CUST_DEP_EFT_CARD;
		//Added on 15oct 2007
		protected System.Web.UI.WebControls.Label capBNK_AGEN_CHK_DEFAULT_AC;
		protected System.Web.UI.WebControls.DropDownList cmbBNK_AGEN_CHK_DEFAULT_AC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBNK_AGEN_CHK_DEFAULT_AC;
		public string FISCAL_ID="";
		

		System.Resources.ResourceManager objResourceMgr;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// Put user code to initialize the page here
			base.ScreenId="126_5";
			SetErrorMessages();
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.BankAccountMapping" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!IsPostBack)
            {
				/*At Default Account Mappings tab for populating drop downs of 
				 * accounts system is picking Bank Accounts which related to
				 *  "General". Let it pick all bank accounts. 
				 * Previous @Relates_TO_Type 11201
				 * */
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBnk_Over_Payment,0);//General
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBnk_Suspense_Amount,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBnk_Return_Prm_Payment,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBNK_CLAIMS_DEFAULT_AC,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBNK_REINSURANCE_DEFAULT_AC,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBNK_DEPOSITS_DEFAULT_AC,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBNK_MISC_CHK_DEFAULT_AC,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBNK_CUST_DEP_EFT_CARD,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbCLM_CHECK_DEFAULT_AC,0);
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBNK_AGEN_CHK_DEFAULT_AC,0);

				string GL_ID= Request.QueryString["GL_ID"].ToString();
				FISCAL_ID= Request.QueryString["FISCAL_ID"].ToString();

				hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Bnk_AC_Mapping(GL_ID,FISCAL_ID);
				SetCaptions();
			}
		}
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvBnk_Over_Payment.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvBnk_Suspense_Amount.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfv_Bnk_Return_Prm_Payment.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvBNK_CLAIMS_DEFAULT_AC.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvBNK_REINSURANCE_DEFAULT_AC.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvBNK_DEPOSITS_DEFAULT_AC.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvBNK_MISC_CHK_DEFAULT_AC.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvCLM_CHECK_DEFAULT_AC.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			rfvBNK_CUST_DEP_EFT_CARD.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvBNK_AGEN_CHK_DEFAULT_AC.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");	
			
			
		}

		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPostingInterfaceInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPostingInterfaceInfo objPostingInterfaceInfo;
			objPostingInterfaceInfo = new ClsPostingInterfaceInfo();
			
			if(cmbBnk_Over_Payment.SelectedValue != null)
				objPostingInterfaceInfo.BNK_OVER_PAYMENT			=	int.Parse(cmbBnk_Over_Payment.SelectedValue);
			if(cmbBnk_Return_Prm_Payment.SelectedValue != null)
				objPostingInterfaceInfo.BNK_RETURN_PRM_PAYMENT		=	int.Parse(cmbBnk_Return_Prm_Payment.SelectedValue);
			if(cmbBnk_Suspense_Amount.SelectedValue != null)
				objPostingInterfaceInfo.BNK_SUSPENSE_AMOUNT			=	int.Parse(cmbBnk_Suspense_Amount.SelectedValue);
			if(cmbBNK_CLAIMS_DEFAULT_AC.SelectedValue != null)
				objPostingInterfaceInfo.BNK_CLAIMS_DEFAULT_AC		=	int.Parse(cmbBNK_CLAIMS_DEFAULT_AC.SelectedValue);
			if(cmbBNK_REINSURANCE_DEFAULT_AC.SelectedValue != null)
				objPostingInterfaceInfo.BNK_REINSURANCE_DEFAULT_AC	=	int.Parse(cmbBNK_REINSURANCE_DEFAULT_AC.SelectedValue);
			if(cmbBNK_DEPOSITS_DEFAULT_AC.SelectedValue != null)
				objPostingInterfaceInfo.BNK_DEPOSITS_DEFAULT_AC		=	int.Parse(cmbBNK_DEPOSITS_DEFAULT_AC.SelectedValue);
			if(cmbBNK_MISC_CHK_DEFAULT_AC.SelectedValue != null)
				objPostingInterfaceInfo.BNK_MISC_CHK_DEFAULT_AC		=	int.Parse(cmbBNK_MISC_CHK_DEFAULT_AC.SelectedValue);
			if(cmbCLM_CHECK_DEFAULT_AC.SelectedValue != null)
				objPostingInterfaceInfo.CLM_CHECK_DEFAULT_AC		=	int.Parse(cmbCLM_CHECK_DEFAULT_AC.SelectedValue);
			if(cmbBNK_CUST_DEP_EFT_CARD.SelectedValue != null)
				objPostingInterfaceInfo.BNK_CUST_DEP_EFT_CARD		=	int.Parse(cmbBNK_CUST_DEP_EFT_CARD.SelectedValue);
			if(cmbBNK_AGEN_CHK_DEFAULT_AC.SelectedValue != null)
				objPostingInterfaceInfo.BNK_AGEN_CHK_DEFAULT_AC		=	int.Parse(cmbBNK_AGEN_CHK_DEFAULT_AC.SelectedValue);
			if(Request.QueryString["FISCAL_ID"]!=null && Request.QueryString["FISCAL_ID"].ToString()!="")
				objPostingInterfaceInfo.FISCAL_ID			  =	int.Parse(Request.QueryString["FISCAL_ID"].ToString());


			



			

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidGL_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objPostingInterfaceInfo;
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal=-10;	//For retreiving the return value of business class save function
				objGeneralLedger = new  ClsGeneralLedger(true);

				//Retreiving the form values into model class object
				ClsPostingInterfaceInfo objPostingInterfaceInfo = GetFormValue();

				

				//Creating the Model object for holding the Old data
				ClsPostingInterfaceInfo objOldPostingInterfaceInfo;
				objOldPostingInterfaceInfo = new ClsPostingInterfaceInfo();

				//Setting  the Old Page details(XML File containing old details) into the Model Object
				base.PopulateModelObject(objOldPostingInterfaceInfo,hidOldData.Value);

				//Setting those values into the Model object which are not in the page
				//objPostingInterfaceInfo.GL_ID = int.Parse(strRowId);
				objPostingInterfaceInfo.GL_ID = int.Parse(Request.QueryString["GL_ID"].ToString());

				objPostingInterfaceInfo.MODIFIED_BY = int.Parse(GetUserId());
				objPostingInterfaceInfo.LAST_UPDATED_DATETIME = DateTime.Now;

				//Updating the record using business layer class object
				intRetVal	= objGeneralLedger.Update_Bank_AC_Mapping(objOldPostingInterfaceInfo,objPostingInterfaceInfo);
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
					hidFormSaved.Value		=	"1";
					hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Bnk_AC_Mapping(objPostingInterfaceInfo.GL_ID.ToString(),objPostingInterfaceInfo.FISCAL_ID.ToString());
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value		=	"2";
				}
				lblMessage.Visible = true;
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//	ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objGeneralLedger!= null)
					objGeneralLedger.Dispose();
			}
			
		}

		private void SetCaptions()
		{
			try
			{
				capBnk_Over_Payment.Text			=		objResourceMgr.GetString("cmbBnk_Over_Payment");
				capBnk_Return_Prm_Payment.Text		=		objResourceMgr.GetString("cmbBnk_Return_Prm_Payment");
				capBnk_Suspense_Amount.Text			=		objResourceMgr.GetString("cmbBnk_Suspense_Amount");
				capBNK_CLAIMS_DEFAULT_AC.Text		=		objResourceMgr.GetString("cmbBNK_CLAIMS_DEFAULT_AC");
				capBNK_REINSURANCE_DEFAULT_AC.Text	=		objResourceMgr.GetString("cmbBNK_REINSURANCE_DEFAULT_AC");
				capBNK_DEPOSITS_DEFAULT_AC.Text		=		objResourceMgr.GetString("cmbBNK_DEPOSITS_DEFAULT_AC");
				capBNK_MISC_CHK_DEFAULT_AC.Text		=		objResourceMgr.GetString("cmbBNK_MISC_CHK_DEFAULT_AC");
				capCLM_CHECK_DEFAULT_AC.Text		=		objResourceMgr.GetString("cmbCLM_CHECK_DEFAULT_AC");
				capBNK_CUST_DEP_EFT_CARD.Text		=		objResourceMgr.GetString("cmbBNK_CUST_DEP_EFT_CARD");
				capBNK_AGEN_CHK_DEFAULT_AC.Text		=		objResourceMgr.GetString("cmbBNK_AGEN_CHK_DEFAULT_AC");
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		
	}
}
