/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	5/27/2005 7:09:27 PM
<End Date				: -	
<Description				: - 	Code Behind for Posting Interface - Income.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -		5th Apr,06
<Modified By			: -		Swastika Gaur
<Purpose				: -		Added Accounts dropdown (cmbINC_INC_INSTALLMENT_FEES,cmbINC_RE_INSTATEMENT_FEES)
<								,cmbINC_NON_SUFFICIENT_FUND_FEES,cmbINC_LATE_FEES
<Modified Date			: -		30-August-2010
<Modified By			: -		Pradeep Kushwaha
<Purpose				: -		Add Three More Accounts 
                                1. Interest Amount 
                                2. Policy Taxes 
                                3. Policy Fees
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
	/// Code Behind for Posting Interface - Income.
	/// </summary>
	public class PiIncome : Cms.CmsWeb.cmsbase
	{
	
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbINC_PRM_WRTN;
		protected System.Web.UI.WebControls.DropDownList cmbINC_PRM_WRTN_MCCA;
		protected System.Web.UI.WebControls.DropDownList cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE;
		protected System.Web.UI.WebControls.DropDownList cmbINC_REINS_CEDED_EXCESS_CON;
		protected System.Web.UI.WebControls.DropDownList cmbINC_REINS_CEDED_CAT_CON;
		protected System.Web.UI.WebControls.DropDownList cmbINC_REINS_CEDED_UMBRELLA_CON;
		protected System.Web.UI.WebControls.DropDownList cmbINC_REINS_CEDED_FACUL_CON;
		protected System.Web.UI.WebControls.DropDownList cmbINC_REINS_CEDED_MCCA_CON;
		protected System.Web.UI.WebControls.DropDownList cmbINC_CHG_UNEARN_PRM;
		protected System.Web.UI.WebControls.DropDownList cmbINC_CHG_UNEARN_PRM_MCCA;
		protected System.Web.UI.WebControls.DropDownList cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE;
		protected System.Web.UI.WebControls.DropDownList cmbINC_CHG_CEDED_UNEARN_MCCA;
		protected System.Web.UI.WebControls.DropDownList cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_PRM_WRTN;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_PRM_WRTN_MCCA;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_PRM_WRTN_OTH_STATE_ASSESS_FEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_REINS_CEDED_EXCESS_CON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_REINS_CEDED_CAT_CON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_REINS_CEDED_UMBRELLA_CON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_REINS_CEDED_FACUL_CON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_REINS_CEDED_MCCA_CON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_CHG_UNEARN_PRM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_CHG_UNEARN_PRM_MCCA;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_CHG_UNEARN_PRM_OTH_STATE_FEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_CHG_CEDED_UNEARN_MCCA;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_CHG_CEDED_UNEARN_UMBRELLA_REINS;



		protected System.Web.UI.WebControls.Label capINC_PRM_WRTN;
		protected System.Web.UI.WebControls.Label capINC_PRM_WRTN_MCCA;
		protected System.Web.UI.WebControls.Label capINC_PRM_WRTN_OTH_STATE_ASSESS_FEE;
		protected System.Web.UI.WebControls.Label capINC_REINS_CEDED_EXCESS_CON;
		protected System.Web.UI.WebControls.Label capINC_REINS_CEDED_CAT_CON;
		protected System.Web.UI.WebControls.Label capINC_REINS_CEDED_UMBRELLA_CON;
		protected System.Web.UI.WebControls.Label capINC_REINS_CEDED_FACUL_CON;
		protected System.Web.UI.WebControls.Label capINC_REINS_CEDED_MCCA_CON;
		protected System.Web.UI.WebControls.Label capINC_CHG_UNEARN_PRM;
		protected System.Web.UI.WebControls.Label capINC_CHG_UNEARN_PRM_MCCA;
		protected System.Web.UI.WebControls.Label capINC_CHG_UNEARN_PRM_OTH_STATE_FEE;
		protected System.Web.UI.WebControls.Label capINC_CHG_CEDED_UNEARN_MCCA;
		protected System.Web.UI.WebControls.Label capINC_CHG_CEDED_UNEARN_UMBRELLA_REINS;
        protected System.Web.UI.WebControls.Label capMANDATORY; //sneha	
        protected System.Web.UI.WebControls.Label capFEEDETL; //sneha	

		public string FISCAL_ID="";

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;

		protected System.Web.UI.WebControls.Label lblMessage;

        //Added by Pradeep Kushwaha on 30-August-2010
        protected System.Web.UI.WebControls.DropDownList cmbINC_INTEREST_AMOUNT;
        protected System.Web.UI.WebControls.Label capINC_INTEREST_AMOUNT;
        protected System.Web.UI.WebControls.DropDownList cmbINC_POLICY_TAXES;
        protected System.Web.UI.WebControls.Label capINC_POLICY_TAXES;
        protected System.Web.UI.WebControls.DropDownList cmbINC_POLICY_FEES;
        protected System.Web.UI.WebControls.Label capINC_POLICY_FEES;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_INTEREST_AMOUNT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_POLICY_TAXES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_POLICY_FEES;
        //Added till here

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capINC_INSTALLMENT_FEES;
		protected System.Web.UI.WebControls.DropDownList cmbINC_INSTALLMENT_FEES;
		protected System.Web.UI.WebControls.Label capINC_RE_INSTATEMENT_FEES;
		protected System.Web.UI.WebControls.DropDownList cmbINC_RE_INSTATEMENT_FEES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_RE_INSTATEMENT_FEES;
		protected System.Web.UI.WebControls.Label capINC_NON_SUFFICIENT_FUND_FEES;
		protected System.Web.UI.WebControls.DropDownList cmbINC_NON_SUFFICIENT_FUND_FEES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_NON_SUFFICIENT_FUND_FEES;
		protected System.Web.UI.WebControls.DropDownList cmbINC_LATE_FEES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_LATE_FEES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_INSTALLMENT_FEES;
		protected System.Web.UI.WebControls.Label capINC_SERVICE_CHARGE;
		protected System.Web.UI.WebControls.DropDownList cmbINC_SERVICE_CHARGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_SERVICE_CHARGE;
		protected System.Web.UI.WebControls.Label capINC_LATE_FEES;
		protected System.Web.UI.WebControls.Label capINC_CONVENIENCE_FEE;
		protected System.Web.UI.WebControls.DropDownList cmbINC_CONVENIENCE_FEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINC_CONVENIENCE_FEE;
		protected System.Web.UI.HtmlControls.HtmlForm ACT_GENERAL_LEDGER;
		
		//Defining the business layer class object
		ClsGeneralLedger  objGeneralLedger ;
		//END:*********** Local variables *************

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvINC_PRM_WRTN.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_PRM_WRTN_MCCA.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_PRM_WRTN_OTH_STATE_ASSESS_FEE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_REINS_CEDED_EXCESS_CON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_REINS_CEDED_CAT_CON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_REINS_CEDED_UMBRELLA_CON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_REINS_CEDED_FACUL_CON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_REINS_CEDED_MCCA_CON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_CHG_UNEARN_PRM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_CHG_UNEARN_PRM_MCCA.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_CHG_UNEARN_PRM_OTH_STATE_FEE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_CHG_CEDED_UNEARN_MCCA.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_CHG_CEDED_UNEARN_UMBRELLA_REINS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			
			rfvINC_INSTALLMENT_FEES.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_RE_INSTATEMENT_FEES.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_NON_SUFFICIENT_FUND_FEES.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_LATE_FEES.ErrorMessage							=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_SERVICE_CHARGE.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvINC_CONVENIENCE_FEE.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");

            //Added by Pradeep Kushwaha on 30-August-2010
            rfvINC_INTEREST_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "203");
            rfvINC_POLICY_TAXES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "203");
            rfvINC_POLICY_FEES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "203");
            //Added till here 
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="126_3";
			lblMessage.Visible = false;
			SetErrorMessages();
			
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.PiIncome" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{

				SetCaptions();
				string GL_ID= Request.QueryString["GL_ID"].ToString();
				FISCAL_ID= Request.QueryString["FISCAL_ID"].ToString();
				string IncomeType = "4";
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_CHG_CEDED_UNEARN_MCCA,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_CHG_UNEARN_PRM,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_CHG_UNEARN_PRM_MCCA,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_PRM_WRTN,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_PRM_WRTN_MCCA,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_REINS_CEDED_CAT_CON,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_REINS_CEDED_EXCESS_CON,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_REINS_CEDED_FACUL_CON,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_REINS_CEDED_MCCA_CON,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_REINS_CEDED_UMBRELLA_CON,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_INSTALLMENT_FEES,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_RE_INSTATEMENT_FEES,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_NON_SUFFICIENT_FUND_FEES,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_LATE_FEES,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_SERVICE_CHARGE,GL_ID,IncomeType);
				ClsGlAccounts.GetAccountsInDropDown(cmbINC_CONVENIENCE_FEE,GL_ID,IncomeType);
                
                //Added by Pradeep Kushwaha on 30-August-2010
                ClsGlAccounts.GetAccountsInDropDown(cmbINC_INTEREST_AMOUNT, GL_ID, IncomeType);
                ClsGlAccounts.GetAccountsInDropDown(cmbINC_POLICY_TAXES, GL_ID, IncomeType);
                ClsGlAccounts.GetAccountsInDropDown(cmbINC_POLICY_FEES, GL_ID, IncomeType);
                //Added till here
				
                hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Income(GL_ID,FISCAL_ID);
			}
		}//end pageload
		#endregion
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPostingInterfaceInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPostingInterfaceInfo objPostingInterfaceInfo;
			objPostingInterfaceInfo = new ClsPostingInterfaceInfo();
            if (cmbINC_PRM_WRTN.SelectedValue != "")
			objPostingInterfaceInfo.INC_PRM_WRTN=	int.Parse(cmbINC_PRM_WRTN.SelectedValue);
            if (cmbINC_PRM_WRTN_MCCA.SelectedValue != "")
			objPostingInterfaceInfo.INC_PRM_WRTN_MCCA=	int.Parse(cmbINC_PRM_WRTN_MCCA.SelectedValue);
            if (cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE.SelectedValue != "")
			objPostingInterfaceInfo.INC_PRM_WRTN_OTH_STATE_ASSESS_FEE=	int.Parse(cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE.SelectedValue);
            if (cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE.SelectedValue != "")
			objPostingInterfaceInfo.INC_REINS_CEDED_EXCESS_CON=	int.Parse(cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE.SelectedValue);
            if (cmbINC_REINS_CEDED_CAT_CON.SelectedValue != "")
			objPostingInterfaceInfo.INC_REINS_CEDED_CAT_CON=	int.Parse(cmbINC_REINS_CEDED_CAT_CON.SelectedValue);
            if (cmbINC_REINS_CEDED_UMBRELLA_CON.SelectedValue != "")
			objPostingInterfaceInfo.INC_REINS_CEDED_UMBRELLA_CON=	int.Parse(cmbINC_REINS_CEDED_UMBRELLA_CON.SelectedValue);
            if (cmbINC_REINS_CEDED_FACUL_CON.SelectedValue != "")
			objPostingInterfaceInfo.INC_REINS_CEDED_FACUL_CON=	int.Parse(cmbINC_REINS_CEDED_FACUL_CON.SelectedValue);
            if (cmbINC_REINS_CEDED_MCCA_CON.SelectedValue != "")
			objPostingInterfaceInfo.INC_REINS_CEDED_MCCA_CON=	int.Parse(cmbINC_REINS_CEDED_MCCA_CON.SelectedValue);
            if (cmbINC_CHG_UNEARN_PRM.SelectedValue != "")
			objPostingInterfaceInfo.INC_CHG_UNEARN_PRM=	int.Parse(cmbINC_CHG_UNEARN_PRM.SelectedValue);
            if (cmbINC_CHG_UNEARN_PRM_MCCA.SelectedValue != "")
			objPostingInterfaceInfo.INC_CHG_UNEARN_PRM_MCCA=	int.Parse(cmbINC_CHG_UNEARN_PRM_MCCA.SelectedValue);
            if (cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE.SelectedValue != "")
			objPostingInterfaceInfo.INC_CHG_UNEARN_PRM_OTH_STATE_FEE=	int.Parse(cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE.SelectedValue);
            if (cmbINC_CHG_CEDED_UNEARN_MCCA.SelectedValue != "")
			objPostingInterfaceInfo.INC_CHG_CEDED_UNEARN_MCCA=	int.Parse(cmbINC_CHG_CEDED_UNEARN_MCCA.SelectedValue);
            if (cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS.SelectedValue != "")
			objPostingInterfaceInfo.INC_CHG_CEDED_UNEARN_UMBRELLA_REINS=	int.Parse(cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS.SelectedValue);
            if (cmbINC_INSTALLMENT_FEES.SelectedValue != "")
			objPostingInterfaceInfo.INC_INSTALLMENT_FEES=	int.Parse(cmbINC_INSTALLMENT_FEES.SelectedValue);
            if (cmbINC_RE_INSTATEMENT_FEES.SelectedValue != "")
			objPostingInterfaceInfo.INC_RE_INSTATEMENT_FEES=	int.Parse(cmbINC_RE_INSTATEMENT_FEES.SelectedValue);
            if (cmbINC_NON_SUFFICIENT_FUND_FEES.SelectedValue != "")
			objPostingInterfaceInfo.INC_NON_SUFFICIENT_FUND_FEES=	int.Parse(cmbINC_NON_SUFFICIENT_FUND_FEES.SelectedValue);
            if (cmbINC_LATE_FEES.SelectedValue != "")
			objPostingInterfaceInfo.INC_LATE_FEES=	int.Parse(cmbINC_LATE_FEES.SelectedValue);
            if (cmbINC_SERVICE_CHARGE.SelectedValue != "")
			objPostingInterfaceInfo.INC_SERVICE_CHARGE = int.Parse(cmbINC_SERVICE_CHARGE.SelectedValue);
            if (cmbINC_CONVENIENCE_FEE.SelectedValue != "")
			objPostingInterfaceInfo.INC_CONVENIENCE_FEE = int.Parse(cmbINC_CONVENIENCE_FEE.SelectedValue);
			if(Request.QueryString["FISCAL_ID"]!=null && Request.QueryString["FISCAL_ID"].ToString()!="")
				objPostingInterfaceInfo.FISCAL_ID			  =	int.Parse(Request.QueryString["FISCAL_ID"].ToString());

            //Added by Pradeep Kushwaha on 30-August-2010
            if (cmbINC_INTEREST_AMOUNT.SelectedValue != "")
            objPostingInterfaceInfo.INC_INTEREST_AMOUNT = int.Parse(cmbINC_INTEREST_AMOUNT.SelectedValue);
            if (cmbINC_POLICY_TAXES.SelectedValue != "")
            objPostingInterfaceInfo.INC_POLICY_TAXES = int.Parse(cmbINC_POLICY_TAXES.SelectedValue);
            if (cmbINC_POLICY_FEES.SelectedValue != "")
            objPostingInterfaceInfo.INC_POLICY_FEES = int.Parse(cmbINC_POLICY_FEES.SelectedValue);
            //Added till here


			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidGL_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objPostingInterfaceInfo;
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
				objPostingInterfaceInfo.GL_ID = int.Parse(strRowId);
				objPostingInterfaceInfo.MODIFIED_BY = int.Parse(GetUserId());
				objPostingInterfaceInfo.LAST_UPDATED_DATETIME = DateTime.Now;

				//Updating the record using business layer class object
				intRetVal	= objGeneralLedger.Update_Income(objOldPostingInterfaceInfo,objPostingInterfaceInfo);
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
					hidFormSaved.Value		=	"1";
					hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Income(objPostingInterfaceInfo.GL_ID.ToString(),objPostingInterfaceInfo.FISCAL_ID.ToString());
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
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objGeneralLedger!= null)
					objGeneralLedger.Dispose();
			}
		}

		#endregion
		private void SetCaptions()
		{
			capINC_PRM_WRTN.Text								=		objResourceMgr.GetString("cmbINC_PRM_WRTN");
			capINC_PRM_WRTN_MCCA.Text							=		objResourceMgr.GetString("cmbINC_PRM_WRTN_MCCA");
			capINC_PRM_WRTN_OTH_STATE_ASSESS_FEE.Text			=		objResourceMgr.GetString("cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE");
			capINC_REINS_CEDED_EXCESS_CON.Text					=		objResourceMgr.GetString("cmbINC_REINS_CEDED_EXCESS_CON");
			capINC_REINS_CEDED_CAT_CON.Text						=		objResourceMgr.GetString("cmbINC_REINS_CEDED_CAT_CON");
			capINC_REINS_CEDED_UMBRELLA_CON.Text				=		objResourceMgr.GetString("cmbINC_REINS_CEDED_UMBRELLA_CON");
			capINC_REINS_CEDED_FACUL_CON.Text					=		objResourceMgr.GetString("cmbINC_REINS_CEDED_FACUL_CON");
			capINC_REINS_CEDED_MCCA_CON.Text					=		objResourceMgr.GetString("cmbINC_REINS_CEDED_MCCA_CON");
			capINC_CHG_UNEARN_PRM.Text							=		objResourceMgr.GetString("cmbINC_CHG_UNEARN_PRM");
			capINC_CHG_UNEARN_PRM_MCCA.Text						=		objResourceMgr.GetString("cmbINC_CHG_UNEARN_PRM_MCCA");
			capINC_CHG_UNEARN_PRM_OTH_STATE_FEE.Text			=		objResourceMgr.GetString("cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE");
			capINC_CHG_CEDED_UNEARN_MCCA.Text					=		objResourceMgr.GetString("cmbINC_CHG_CEDED_UNEARN_MCCA");
			capINC_CHG_CEDED_UNEARN_UMBRELLA_REINS.Text			=		objResourceMgr.GetString("cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS");
			capINC_INSTALLMENT_FEES.Text						=		objResourceMgr.GetString("cmbINC_INSTALLMENT_FEES");
			capINC_RE_INSTATEMENT_FEES.Text						=		objResourceMgr.GetString("cmbINC_RE_INSTATEMENT_FEES");
			capINC_NON_SUFFICIENT_FUND_FEES.Text				=		objResourceMgr.GetString("cmbINC_NON_SUFFICIENT_FUND_FEES");
			capINC_LATE_FEES.Text								=		objResourceMgr.GetString("cmbINC_LATE_FEES");
			capINC_SERVICE_CHARGE.Text							=		objResourceMgr.GetString("cmbINC_SERVICE_CHARGE");
			capINC_CONVENIENCE_FEE.Text							=		objResourceMgr.GetString("cmbINC_CONVENIENCE_FEE");
            //Added by Pradeep Kushwaha on 30-August-2010
            capINC_INTEREST_AMOUNT.Text                         =       objResourceMgr.GetString("cmbINC_INTEREST_AMOUNT");
            capINC_POLICY_TAXES.Text                            =       objResourceMgr.GetString("cmbINC_POLICY_TAXES");
            capINC_POLICY_FEES.Text                             =       objResourceMgr.GetString("cmbINC_POLICY_FEES");
            //Added till here
            capMANDATORY.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); //sneha
            capFEEDETL.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1330"); //sneha



		}
		
	}
}
