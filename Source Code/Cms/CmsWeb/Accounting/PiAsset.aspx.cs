/******************************************************************************************
	<Author				: -   Ajit Singh Chahal
	<Start Date				: -	5/26/2005 5:55:12 PM
	<End Date				: -	
	<Description				: - 	Posting interface - Asset.
	<Review Date				: - 
	<Reviewed By			: - 	
	Modification History
	<Modified Date			: - 
	<Modified By				: - 
	<Purpose				: - Posting interface - Asset.
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

namespace  Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Posting interface - Asset.
	/// </summary>
	public class PiAsset : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbAST_UNCOLL_PRM_CUSTOMER;
		protected System.Web.UI.WebControls.DropDownList cmbAST_UNCOLL_PRM_AGENCY;
		protected System.Web.UI.WebControls.DropDownList cmbAST_UNCOLL_PRM_SUSPENSE;
		protected System.Web.UI.WebControls.DropDownList cmbAST_PRM_SUSPENSE;
		protected System.Web.UI.WebControls.DropDownList cmbAST_MCCA_FEE_SUSPENSE;
		protected System.Web.UI.WebControls.DropDownList cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE;
		protected System.Web.UI.WebControls.DropDownList cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_UNCOLL_PRM_CUSTOMER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_UNCOLL_PRM_AGENCY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_UNCOLL_PRM_SUSPENSE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_PRM_SUSPENSE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_MCCA_FEE_SUSPENSE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_OTHER_STATE_ASSMT_FEE_SUSPENSE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_COMM_RECV_REINS_EXCESS_CONTRACT;

		protected System.Web.UI.WebControls.Label lblMessage;
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capAST_UNCOLL_PRM_CUSTOMER;
		protected System.Web.UI.WebControls.Label capAST_UNCOLL_PRM_AGENCY;
		protected System.Web.UI.WebControls.Label capAST_UNCOLL_PRM_SUSPENSE;
		protected System.Web.UI.WebControls.Label capAST_PRM_SUSPENSE;
		protected System.Web.UI.WebControls.Label capAST_MCCA_FEE_SUSPENSE;
		protected System.Web.UI.WebControls.Label capAST_OTHER_STATE_ASSMT_FEE_SUSPENSE;
		protected System.Web.UI.WebControls.Label capAST_COMM_RECV_REINS_EXCESS_CONTRACT;
		protected System.Web.UI.WebControls.Label capAST_COMM_RECV_REINS_UMBRELLA_CONTRACT;
        protected System.Web.UI.WebControls.Label capMAN; //sneha
        protected System.Web.UI.WebControls.Label capDIRBIL; //sneha
        protected System.Web.UI.WebControls.Label capAGENGYBIL; //sneha
		protected System.Web.UI.WebControls.DropDownList cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_COMM_RECV_REINS_UMBRELLA_CONTRACT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
		protected System.Web.UI.WebControls.TextBox txtAST_UNCOLL_PRM_CUSTOMER;
		protected System.Web.UI.WebControls.Label capAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.Label capAST_PRM_WRIT_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_PRM_WRIT_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.Label capAST_PRM_WRIT_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_PRM_WRIT_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.Label capAST_MCCA_FEE_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_MCCA_FEE_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.Label capAST_MCCA_FEE_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_MCCA_FEE_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.Label capAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.Label capAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.Label capAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.Label capAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.Label capAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL;
		protected System.Web.UI.WebControls.Label capAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.Label capAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.DropDownList cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL;
		protected System.Web.UI.HtmlControls.HtmlForm ACT_GENERAL_LEDGER;
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
			rfvAST_UNCOLL_PRM_CUSTOMER.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_UNCOLL_PRM_AGENCY.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_PRM_WRIT_SUSPENSE_DIRECT_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_PRM_WRIT_SUSPENSE_AGENCY_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_MCCA_FEE_SUSPENSE_DIRECT_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_MCCA_FEE_SUSPENSE_AGENCY_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_COMM_RECV_REINS_EXCESS_CONTRACT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_COMM_RECV_REINS_UMBRELLA_CONTRACT.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");

			rfvAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");

		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="126_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.PiAsset" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
                SetCaptions();
				string GL_ID= Request.QueryString["GL_ID"].ToString();
				/*Including Fiscal ID*/
				FISCAL_ID= Request.QueryString["FISCAL_ID"].ToString();

				string AssetType = "1";
				ArrayList alAccountDropdowns = new ArrayList();
				DropDownList[] ddlAccountDropdowns = null;
				alAccountDropdowns.Add(cmbAST_UNCOLL_PRM_AGENCY);
				alAccountDropdowns.Add(cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL);
				alAccountDropdowns.Add(cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL);
				alAccountDropdowns.Add(cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL);
				alAccountDropdowns.Add(cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL);
				alAccountDropdowns.Add(cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL);
				alAccountDropdowns.Add(cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL);
				alAccountDropdowns.Add(cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL);
				alAccountDropdowns.Add(cmbAST_UNCOLL_PRM_CUSTOMER);
				alAccountDropdowns.Add(cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT);
				alAccountDropdowns.Add(cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT);
				alAccountDropdowns.Add(cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL);
				alAccountDropdowns.Add(cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL);
				alAccountDropdowns.Add(cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL);
				alAccountDropdowns.Add(cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL);
				alAccountDropdowns.Add(cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL);
			
     			 ddlAccountDropdowns = (DropDownList[])alAccountDropdowns.ToArray(typeof(DropDownList));				
				ClsGlAccounts.GetAccountsInDropDown(ddlAccountDropdowns,GL_ID,AssetType);
				
				hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Asset(GL_ID,FISCAL_ID);
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

			
				objPostingInterfaceInfo.AST_UNCOLL_PRM_CUSTOMER									=		int.Parse(cmbAST_UNCOLL_PRM_CUSTOMER.SelectedValue);
				objPostingInterfaceInfo.AST_UNCOLL_PRM_AGENCY									=		int.Parse(cmbAST_UNCOLL_PRM_AGENCY.SelectedValue);
				objPostingInterfaceInfo.AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL						=		int.Parse(cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_PRM_WRIT_SUSPENSE_DIRECT_BILL						=		int.Parse(cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_PRM_WRIT_SUSPENSE_AGENCY_BILL						=		int.Parse(cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_MCCA_FEE_SUSPENSE_DIRECT_BILL						=		int.Parse(cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_MCCA_FEE_SUSPENSE_AGENCY_BILL						=		int.Parse(cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL			=		int.Parse(cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL			=		int.Parse(cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_COMM_RECV_REINS_EXCESS_CONTRACT						=		int.Parse(cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT.SelectedValue);
				objPostingInterfaceInfo.AST_COMM_RECV_REINS_UMBRELLA_CONTRACT					=		int.Parse(cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT.SelectedValue);

				objPostingInterfaceInfo.AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL					=		int.Parse(cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL				=		int.Parse(cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL				=		int.Parse(cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL				=		int.Parse(cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL.SelectedValue);
				objPostingInterfaceInfo.AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL				=		int.Parse(cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL.SelectedValue);
				if(Request.QueryString["FISCAL_ID"]!=null && Request.QueryString["FISCAL_ID"].ToString()!="")
					objPostingInterfaceInfo.FISCAL_ID												=		int.Parse(Request.QueryString["FISCAL_ID"].ToString());

			

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidGL_ID.Value;
			oldXML			=	hidOldData.Value;
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
				intRetVal	= objGeneralLedger.Update_Asset(objOldPostingInterfaceInfo,objPostingInterfaceInfo);
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
					hidFormSaved.Value		=	"1";
					hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Asset(objPostingInterfaceInfo.GL_ID.ToString(),objPostingInterfaceInfo.FISCAL_ID.ToString());
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
			
			capAST_UNCOLL_PRM_CUSTOMER.Text									=		objResourceMgr.GetString("cmbAST_UNCOLL_PRM_CUSTOMER");
			capAST_UNCOLL_PRM_AGENCY.Text									=		objResourceMgr.GetString("cmbAST_UNCOLL_PRM_AGENCY");
			capAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL.Text						=		objResourceMgr.GetString("cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL");
			capAST_PRM_WRIT_SUSPENSE_DIRECT_BILL.Text						=		objResourceMgr.GetString("cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL");
			capAST_PRM_WRIT_SUSPENSE_AGENCY_BILL.Text						=		objResourceMgr.GetString("cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL");
			capAST_MCCA_FEE_SUSPENSE_DIRECT_BILL.Text						=		objResourceMgr.GetString("cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL");
			capAST_MCCA_FEE_SUSPENSE_AGENCY_BILL.Text						=		objResourceMgr.GetString("cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL");
			capAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL.Text			=		objResourceMgr.GetString("cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL");
			capAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL.Text			=		objResourceMgr.GetString("cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL");
			capAST_COMM_RECV_REINS_EXCESS_CONTRACT.Text						=		objResourceMgr.GetString("cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT");
			capAST_COMM_RECV_REINS_UMBRELLA_CONTRACT.Text					=		objResourceMgr.GetString("cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT");

			
			capAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL.Text					=		objResourceMgr.GetString("cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL");
			capAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL.Text				=		objResourceMgr.GetString("cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL");
			capAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL.Text				=		objResourceMgr.GetString("cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL");
			capAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL.Text				=		objResourceMgr.GetString("cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL");
			capAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL.Text				=		objResourceMgr.GetString("cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL");
            capMAN.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); //sneha
            capDIRBIL.Text = objResourceMgr.GetString("capDIRBIL"); //sneha
            capAGENGYBIL.Text = objResourceMgr.GetString("capAGENGYBIL"); //sneha

		}
	}
}
