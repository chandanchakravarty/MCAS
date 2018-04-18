
	/******************************************************************************************
	<Author				: -   Ajit Singh Chahal
	<Start Date				: -	5/27/2005 4:49:52 PM
	<End Date				: -	
	<Description				: - 	Code Behind for Posting Interface - Liability.
	<Review Date				: - 
	<Reviewed By			: - 	
	Modification History
	<Modified Date			: - 
	<Modified By				: - 
	<Purpose				: - Code Behind for Posting Interface - Liability.
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
		/// Code Behind for Posting Interface - Liability.
		/// </summary>
		public class PiLiabilities : Cms.CmsWeb.cmsbase
		{
			#region Page controls declaration
			protected System.Web.UI.WebControls.DropDownList cmbLIB_COMM_PAYB_AGENCY_BILL;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_COMM_PAYB_DIRECT_BILL;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_REINS_PAYB_EXCESS_CONTRACT;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_REINS_PAYB_CAT_CONTRACT;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_REINS_PAYB_MCCA;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_REINS_PAYB_UMBRELLA;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_REINS_PAYB_FACULTATIVE;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_OUT_DRAFTS;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_ADVCE_PRM_DEPOSIT;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_ADVCE_PRM_DEPOSIT_2M;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_UNEARN_PRM;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_UNEARN_PRM_MCCA;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE;
			//protected System.Web.UI.WebControls.DropDownList cmbLIB_TAX_PAYB;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_VENDOR_PAYB;
			protected System.Web.UI.WebControls.DropDownList cmbLIB_COLL_ON_NONISSUED_POLICY;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

			protected Cms.CmsWeb.Controls.CmsButton btnReset;
			protected Cms.CmsWeb.Controls.CmsButton btnSave;

			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_COMM_PAYB_AGENCY_BILL;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_COMM_PAYB_DIRECT_BILL;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_REINS_PAYB_EXCESS_CONTRACT;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_REINS_PAYB_CAT_CONTRACT;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_REINS_PAYB_MCCA;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_REINS_PAYB_UMBRELLA;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_REINS_PAYB_FACULTATIVE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_OUT_DRAFTS;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_ADVCE_PRM_DEPOSIT;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_ADVCE_PRM_DEPOSIT_2M;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_UNEARN_PRM;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_UNEARN_PRM_MCCA;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE;
			//protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_TAX_PAYB;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_VENDOR_PAYB;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIB_COLL_ON_NONISSUED_POLICY;

			protected System.Web.UI.WebControls.Label lblMessage;

			#endregion
			#region Local form variables
			//START:*********** Local form variables *************
			string oldXML;
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;
			private string strRowId, strFormSaved;
			protected System.Web.UI.WebControls.Label capLIB_COMM_PAYB_AGENCY_BILL;
			protected System.Web.UI.WebControls.Label capLIB_COMM_PAYB_DIRECT_BILL;
			protected System.Web.UI.WebControls.Label capLIB_REINS_PAYB_EXCESS_CONTRACT;
			protected System.Web.UI.WebControls.Label capLIB_REINS_PAYB_CAT_CONTRACT;
			protected System.Web.UI.WebControls.Label capLIB_REINS_PAYB_MCCA;
			protected System.Web.UI.WebControls.Label capLIB_REINS_PAYB_UMBRELLA;
			protected System.Web.UI.WebControls.Label capLIB_REINS_PAYB_FACULTATIVE;
			protected System.Web.UI.WebControls.Label capLIB_OUT_DRAFTS;
			protected System.Web.UI.WebControls.Label capLIB_ADVCE_PRM_DEPOSIT;
			protected System.Web.UI.WebControls.Label capLIB_ADVCE_PRM_DEPOSIT_2M;
			protected System.Web.UI.WebControls.Label capLIB_UNEARN_PRM;
			protected System.Web.UI.WebControls.Label capLIB_UNEARN_PRM_MCCA;
			protected System.Web.UI.WebControls.Label capLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS;
			protected System.Web.UI.WebControls.Label capLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS;
			protected System.Web.UI.WebControls.Label capLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE;
			//protected System.Web.UI.WebControls.Label capLIB_TAX_PAYB;
			protected System.Web.UI.WebControls.Label capLIB_VENDOR_PAYB;
			protected System.Web.UI.WebControls.Label capLIB_COLL_ON_NONISSUED_POLICY;
            protected System.Web.UI.WebControls.Label capMANDATORY; //sneha
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
			protected System.Web.UI.WebControls.TextBox LIB_UNEARN_PRM;
			public string FISCAL_ID="";
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
				rfvLIB_COMM_PAYB_AGENCY_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_COMM_PAYB_DIRECT_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_REINS_PAYB_EXCESS_CONTRACT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_REINS_PAYB_CAT_CONTRACT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_REINS_PAYB_MCCA.ErrorMessage			        =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_REINS_PAYB_UMBRELLA.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_REINS_PAYB_FACULTATIVE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_OUT_DRAFTS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_ADVCE_PRM_DEPOSIT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_ADVCE_PRM_DEPOSIT_2M.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_UNEARN_PRM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_UNEARN_PRM_MCCA.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			//	rfvLIB_TAX_PAYB.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_VENDOR_PAYB.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
				rfvLIB_COLL_ON_NONISSUED_POLICY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			}
			#endregion
			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				// phone and extension control names: cmbPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('cmbPHONE','cmbEXT');");
				base.ScreenId="126_1";
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass	=	CmsButtonType.Write;
				btnReset.PermissionString		=	gstrSecurityXML;

				btnSave.CmsButtonClass	=	CmsButtonType.Write;
				btnSave.PermissionString		=	gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.PiLiabilities" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{
					SetCaptions();
					string GL_ID= Request.QueryString["GL_ID"].ToString();
					FISCAL_ID= Request.QueryString["FISCAL_ID"].ToString();
					string LiabilityType = "2";
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_ADVCE_PRM_DEPOSIT,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_ADVCE_PRM_DEPOSIT_2M,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_COLL_ON_NONISSUED_POLICY,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_COMM_PAYB_AGENCY_BILL,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_COMM_PAYB_DIRECT_BILL,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_OUT_DRAFTS,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_REINS_PAYB_CAT_CONTRACT,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_REINS_PAYB_EXCESS_CONTRACT,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_REINS_PAYB_FACULTATIVE,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_REINS_PAYB_MCCA,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_REINS_PAYB_UMBRELLA,GL_ID,LiabilityType);
					//ClsGlAccounts.GetAccountsInDropDown(cmbLIB_TAX_PAYB,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_UNEARN_PRM,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_UNEARN_PRM_MCCA,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_VENDOR_PAYB,GL_ID,LiabilityType);
					ClsGlAccounts.GetAccountsInDropDown(cmbLIB_VENDOR_PAYB,GL_ID,LiabilityType);
					hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Liability(GL_ID,FISCAL_ID);
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
                if (cmbLIB_COMM_PAYB_AGENCY_BILL.SelectedValue != "")
				objPostingInterfaceInfo.LIB_COMM_PAYB_AGENCY_BILL=	int.Parse(cmbLIB_COMM_PAYB_AGENCY_BILL.SelectedValue);
                if(cmbLIB_COMM_PAYB_DIRECT_BILL.SelectedValue!="")
				objPostingInterfaceInfo.LIB_COMM_PAYB_DIRECT_BILL=	int.Parse(cmbLIB_COMM_PAYB_DIRECT_BILL.SelectedValue);

                if (cmbLIB_REINS_PAYB_EXCESS_CONTRACT.SelectedValue != "")
				objPostingInterfaceInfo.LIB_REINS_PAYB_EXCESS_CONTRACT=	int.Parse(cmbLIB_REINS_PAYB_EXCESS_CONTRACT.SelectedValue);
                if (cmbLIB_REINS_PAYB_CAT_CONTRACT.SelectedValue != "")
				objPostingInterfaceInfo.LIB_REINS_PAYB_CAT_CONTRACT=	int.Parse(cmbLIB_REINS_PAYB_CAT_CONTRACT.SelectedValue);
                if (cmbLIB_REINS_PAYB_MCCA.SelectedValue != "")
				objPostingInterfaceInfo.LIB_REINS_PAYB_MCCA=	int.Parse(cmbLIB_REINS_PAYB_MCCA.SelectedValue);
                if (cmbLIB_REINS_PAYB_UMBRELLA.SelectedValue != "")
				objPostingInterfaceInfo.LIB_REINS_PAYB_UMBRELLA=	int.Parse(cmbLIB_REINS_PAYB_UMBRELLA.SelectedValue);
                if (cmbLIB_REINS_PAYB_FACULTATIVE.SelectedValue != "")
				objPostingInterfaceInfo.LIB_REINS_PAYB_FACULTATIVE=	int.Parse(cmbLIB_REINS_PAYB_FACULTATIVE.SelectedValue);
                if (cmbLIB_OUT_DRAFTS.SelectedValue != "")
				objPostingInterfaceInfo.LIB_OUT_DRAFTS=	int.Parse(cmbLIB_OUT_DRAFTS.SelectedValue);
                if (cmbLIB_ADVCE_PRM_DEPOSIT.SelectedValue != "")
				objPostingInterfaceInfo.LIB_ADVCE_PRM_DEPOSIT=	int.Parse(cmbLIB_ADVCE_PRM_DEPOSIT.SelectedValue);
                if (cmbLIB_ADVCE_PRM_DEPOSIT_2M.SelectedValue != "")
				objPostingInterfaceInfo.LIB_ADVCE_PRM_DEPOSIT_2M=	int.Parse(cmbLIB_ADVCE_PRM_DEPOSIT_2M.SelectedValue);
                if (cmbLIB_UNEARN_PRM.SelectedValue != "")
				objPostingInterfaceInfo.LIB_UNEARN_PRM=	int.Parse(cmbLIB_UNEARN_PRM.SelectedValue);
                if (cmbLIB_UNEARN_PRM_MCCA.SelectedValue != "")
				objPostingInterfaceInfo.LIB_UNEARN_PRM_MCCA=	int.Parse(cmbLIB_UNEARN_PRM_MCCA.SelectedValue);
                if (cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS.SelectedValue != "")
				objPostingInterfaceInfo.LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS=	int.Parse(cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS.SelectedValue);
                if (cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS.SelectedValue != "")
				objPostingInterfaceInfo.LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS=	int.Parse(cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS.SelectedValue);
                if (cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE.SelectedValue != "")
				objPostingInterfaceInfo.LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE=	int.Parse(cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE.SelectedValue);
                if (cmbLIB_VENDOR_PAYB.SelectedValue != "")
				//objPostingInterfaceInfo.LIB_TAX_PAYB=	int.Parse(cmbLIB_TAX_PAYB.SelectedValue);
				objPostingInterfaceInfo.LIB_VENDOR_PAYB=	int.Parse(cmbLIB_VENDOR_PAYB.SelectedValue);
                if (cmbLIB_COLL_ON_NONISSUED_POLICY.SelectedValue != "")
				objPostingInterfaceInfo.LIB_COLL_ON_NONISSUED_POLICY=	int.Parse(cmbLIB_COLL_ON_NONISSUED_POLICY.SelectedValue);

				if(Request.QueryString["FISCAL_ID"]!=null && Request.QueryString["FISCAL_ID"].ToString()!="")
					objPostingInterfaceInfo.FISCAL_ID			  =	int.Parse(Request.QueryString["FISCAL_ID"].ToString());


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
					intRetVal	= objGeneralLedger.Update_Liability(objOldPostingInterfaceInfo,objPostingInterfaceInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Liability(objPostingInterfaceInfo.GL_ID.ToString(),objPostingInterfaceInfo.FISCAL_ID.ToString());
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
				capLIB_COMM_PAYB_AGENCY_BILL.Text						=		objResourceMgr.GetString("cmbLIB_COMM_PAYB_AGENCY_BILL");
				capLIB_COMM_PAYB_DIRECT_BILL.Text						=		objResourceMgr.GetString("cmbLIB_COMM_PAYB_DIRECT_BILL");
				capLIB_REINS_PAYB_EXCESS_CONTRACT.Text						=		objResourceMgr.GetString("cmbLIB_REINS_PAYB_EXCESS_CONTRACT");
				capLIB_REINS_PAYB_CAT_CONTRACT.Text						=		objResourceMgr.GetString("cmbLIB_REINS_PAYB_CAT_CONTRACT");
				capLIB_REINS_PAYB_MCCA.Text						=		objResourceMgr.GetString("cmbLIB_REINS_PAYB_MCCA");
				capLIB_REINS_PAYB_UMBRELLA.Text						=		objResourceMgr.GetString("cmbLIB_REINS_PAYB_UMBRELLA");
				capLIB_REINS_PAYB_FACULTATIVE.Text						=		objResourceMgr.GetString("cmbLIB_REINS_PAYB_FACULTATIVE");
				capLIB_OUT_DRAFTS.Text						=		objResourceMgr.GetString("cmbLIB_OUT_DRAFTS");
				capLIB_ADVCE_PRM_DEPOSIT.Text						=		objResourceMgr.GetString("cmbLIB_ADVCE_PRM_DEPOSIT");
				capLIB_ADVCE_PRM_DEPOSIT_2M.Text						=		objResourceMgr.GetString("cmbLIB_ADVCE_PRM_DEPOSIT_2M");
				capLIB_UNEARN_PRM.Text						=		objResourceMgr.GetString("cmbLIB_UNEARN_PRM");
				capLIB_UNEARN_PRM_MCCA.Text						=		objResourceMgr.GetString("cmbLIB_UNEARN_PRM_MCCA");
				capLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS.Text						=		objResourceMgr.GetString("cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS");
				capLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS.Text						=		objResourceMgr.GetString("cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS");
				capLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE.Text						=		objResourceMgr.GetString("cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE");
				//capLIB_TAX_PAYB.Text						=		objResourceMgr.GetString("cmbLIB_TAX_PAYB");
				capLIB_VENDOR_PAYB.Text						=		objResourceMgr.GetString("cmbLIB_VENDOR_PAYB");
				capLIB_COLL_ON_NONISSUED_POLICY.Text						=		objResourceMgr.GetString("cmbLIB_COLL_ON_NONISSUED_POLICY");
                capMANDATORY.Text                           =       Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); //sneha
			}
			private void GetOldDataXML()
			{
				if ( Request.Params.Count != 0 ) 
				{
				}
				else 
				{
				}
			}
		}
	}
