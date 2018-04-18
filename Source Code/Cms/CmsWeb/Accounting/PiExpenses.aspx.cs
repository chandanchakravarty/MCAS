/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	5/27/2005 7:39:12 PM
<End Date				: -	
<Description				: - 	Code behind for Posting Interface - Expenses.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Code behind for Posting Interface - Expenses.
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
	/// Code behind for Posting Interface - Expenses.
	/// </summary>
	public class PiExpenses : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbEXP_COMM_INCURRED;
		protected System.Web.UI.WebControls.DropDownList cmbEXP_REINS_COMM_EXCESS_CON;
		protected System.Web.UI.WebControls.DropDownList cmbEXP_REINS_COMM_UMBRELLA_CON;
		protected System.Web.UI.WebControls.DropDownList cmbEXP_ASSIGNED_CLAIMS;
		protected System.Web.UI.WebControls.DropDownList cmbEXP_REINS_PAID_LOSSES;
		protected System.Web.UI.WebControls.DropDownList cmbEXP_REINS_PAID_LOSSES_CAT;
		protected System.Web.UI.WebControls.DropDownList cmbEXP_SMALL_BALANCE_WRITE_OFF;
	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_COMM_INCURRED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_REINS_COMM_EXCESS_CON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_REINS_COMM_UMBRELLA_CON;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_ASSIGNED_CLAIMS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_REINS_PAID_LOSSES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_REINS_PAID_LOSSES_CAT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXP_SMALL_BALANCE_WRITE_OFF;

		protected System.Web.UI.WebControls.Label lblMessage;

		protected System.Web.UI.WebControls.Label capEXP_COMM_INCURRED;
		protected System.Web.UI.WebControls.Label capEXP_REINS_COMM_EXCESS_CON;
		protected System.Web.UI.WebControls.Label capEXP_REINS_COMM_UMBRELLA_CON;
		protected System.Web.UI.WebControls.Label capEXP_ASSIGNED_CLAIMS;
		protected System.Web.UI.WebControls.Label capEXP_REINS_PAID_LOSSES;
		protected System.Web.UI.WebControls.Label capEXP_REINS_PAID_LOSSES_CAT;
		protected System.Web.UI.WebControls.Label capEXP_SMALL_BALANCE_WRITE_OFF;
		public string FISCAL_ID="";
		

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.Label EXP_SMALL_BALANCE_WRITE_OFF;
        protected System.Web.UI.WebControls.Label capMANDATORY; //sneha
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
			rfvEXP_COMM_INCURRED.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvEXP_REINS_COMM_EXCESS_CON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvEXP_REINS_COMM_UMBRELLA_CON.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvEXP_ASSIGNED_CLAIMS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvEXP_REINS_PAID_LOSSES.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvEXP_REINS_PAID_LOSSES_CAT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvEXP_SMALL_BALANCE_WRITE_OFF.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="126_4";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.PiExpenses" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{

				SetCaptions();
				string GL_ID= Request.QueryString["GL_ID"].ToString();
				FISCAL_ID= Request.QueryString["FISCAL_ID"].ToString();
				string ExpenseType = "5";
				ClsGlAccounts.GetAccountsInDropDown(cmbEXP_ASSIGNED_CLAIMS,GL_ID,ExpenseType);
				ClsGlAccounts.GetAccountsInDropDown(cmbEXP_COMM_INCURRED,GL_ID,ExpenseType);
				ClsGlAccounts.GetAccountsInDropDown(cmbEXP_REINS_COMM_EXCESS_CON,GL_ID,ExpenseType);
				ClsGlAccounts.GetAccountsInDropDown(cmbEXP_REINS_COMM_UMBRELLA_CON,GL_ID,ExpenseType);
				ClsGlAccounts.GetAccountsInDropDown(cmbEXP_REINS_PAID_LOSSES,GL_ID,ExpenseType);
				ClsGlAccounts.GetAccountsInDropDown(cmbEXP_REINS_PAID_LOSSES_CAT,GL_ID,ExpenseType);
				ClsGlAccounts.GetAccountsInDropDown(cmbEXP_SMALL_BALANCE_WRITE_OFF,GL_ID,ExpenseType);
				hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Expense(GL_ID,FISCAL_ID);
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
            if (cmbEXP_COMM_INCURRED.SelectedValue != "")
            objPostingInterfaceInfo.EXP_COMM_INCURRED						=	int.Parse(cmbEXP_COMM_INCURRED.SelectedValue);
            if (cmbEXP_REINS_COMM_EXCESS_CON.SelectedValue != "")
			objPostingInterfaceInfo.EXP_REINS_COMM_EXCESS_CON				=	int.Parse(cmbEXP_REINS_COMM_EXCESS_CON.SelectedValue);
            if (cmbEXP_REINS_COMM_UMBRELLA_CON.SelectedValue != "")
			objPostingInterfaceInfo.EXP_REINS_COMM_UMBRELLA_CON             =	int.Parse(cmbEXP_REINS_COMM_UMBRELLA_CON.SelectedValue);
            if (cmbEXP_ASSIGNED_CLAIMS.SelectedValue != "")
			objPostingInterfaceInfo.EXP_ASSIGNED_CLAIMS			            =	int.Parse(cmbEXP_ASSIGNED_CLAIMS.SelectedValue);
            if (cmbEXP_REINS_PAID_LOSSES.SelectedValue != "")
			objPostingInterfaceInfo.EXP_REINS_PAID_LOSSES			        =	int.Parse(cmbEXP_REINS_PAID_LOSSES.SelectedValue);
            if (cmbEXP_REINS_PAID_LOSSES_CAT.SelectedValue != "")
			objPostingInterfaceInfo.EXP_REINS_PAID_LOSSES_CAT			    =	int.Parse(cmbEXP_REINS_PAID_LOSSES_CAT.SelectedValue);
            if (cmbEXP_SMALL_BALANCE_WRITE_OFF.SelectedValue != "")
			objPostingInterfaceInfo.EXP_SMALL_BALANCE_WRITE_OFF			    =	int.Parse(cmbEXP_SMALL_BALANCE_WRITE_OFF.SelectedValue);
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
				intRetVal	= objGeneralLedger.Update_Expense(objOldPostingInterfaceInfo,objPostingInterfaceInfo);
				
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
					hidFormSaved.Value		=	"1";
                    hidOldData.Value = ClsGeneralLedger.GetXmlForPageControls_Expense(objPostingInterfaceInfo.GL_ID.ToString(), objPostingInterfaceInfo.FISCAL_ID.ToString());
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
			capEXP_COMM_INCURRED.Text								=		objResourceMgr.GetString("cmbEXP_COMM_INCURRED");
			capEXP_REINS_COMM_EXCESS_CON.Text						=		objResourceMgr.GetString("cmbEXP_REINS_COMM_EXCESS_CON");
			capEXP_REINS_COMM_UMBRELLA_CON.Text						=		objResourceMgr.GetString("cmbEXP_REINS_COMM_UMBRELLA_CON");
			capEXP_ASSIGNED_CLAIMS.Text								=		objResourceMgr.GetString("cmbEXP_ASSIGNED_CLAIMS");
			capEXP_REINS_PAID_LOSSES.Text							=		objResourceMgr.GetString("cmbEXP_REINS_PAID_LOSSES");
			capEXP_REINS_PAID_LOSSES_CAT.Text						=		objResourceMgr.GetString("cmbEXP_REINS_PAID_LOSSES_CAT");
			capEXP_SMALL_BALANCE_WRITE_OFF.Text						=		objResourceMgr.GetString("cmbEXP_SMALL_BALANCE_WRITE_OFF");
            capMANDATORY.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); //sneha
		}
		
	}
}
