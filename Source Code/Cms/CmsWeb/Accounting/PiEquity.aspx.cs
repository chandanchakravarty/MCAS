/******************************************************************************************
	<Author				: -   Ajit Singh Chahal
	<Start Date				: -	5/26/2005 5:55:12 PM
	<End Date				: -	
	<Description				: - 	Posting interface - Equity.
	<Review Date				: - 
	<Reviewed By			: - 	
	Modification History
	<Modified Date			: - 
	<Modified By				: - 
	<Purpose				: - Posting interface - Equity.
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
	/// Summary description for PiEquity.
	/// </summary>
	public class PiEquity : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbEQU_TRANSFER;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEQU_TRANSFER;

		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
		protected System.Web.UI.WebControls.Label capEQU_TRANSFER;
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capEQU_UNASSIGNED_SURPLUS;
        protected System.Web.UI.WebControls.Label capMANDATORY; //sneha
		protected System.Web.UI.WebControls.DropDownList cmbEQU_UNASSIGNED_SURPLUS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEQU_UNASSIGNED_SURPLUS;
		protected System.Web.UI.WebControls.Label capUNASSIGNED_SURPLUS;
		protected System.Web.UI.WebControls.DropDownList cmbUNASSIGNED_SURPLUS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUNASSIGNED_SURPLUS;
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
			rfvEQU_TRANSFER.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"203");
			rfvEQU_UNASSIGNED_SURPLUS.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("888");	
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="126_2";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.PiEquity" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				SetCaptions();
				string GL_ID= Request.QueryString["GL_ID"].ToString();
				FISCAL_ID= Request.QueryString["FISCAL_ID"].ToString();
				string EquityType = "3";
				ClsGlAccounts.GetAccountsInDropDown(cmbEQU_TRANSFER,GL_ID,EquityType);
				ClsGlAccounts.GetAccountsInDropDown(cmbEQU_UNASSIGNED_SURPLUS,GL_ID,EquityType);
				hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Equity(GL_ID,FISCAL_ID);
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

			objPostingInterfaceInfo.EQU_TRANSFER=	int.Parse(cmbEQU_TRANSFER.SelectedValue);
			objPostingInterfaceInfo.EQU_UNASSIGNED_SURPLUS=	int.Parse(cmbEQU_UNASSIGNED_SURPLUS.SelectedValue);
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
				intRetVal	= objGeneralLedger.Update_Equity(objOldPostingInterfaceInfo,objPostingInterfaceInfo);
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
					hidFormSaved.Value		=	"1";
					hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControls_Equity(objPostingInterfaceInfo.GL_ID.ToString(),objPostingInterfaceInfo.FISCAL_ID.ToString());
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
		#endregion
		private void SetCaptions()
		{
			capEQU_TRANSFER.Text						=		objResourceMgr.GetString("cmbEQU_TRANSFER");
			capEQU_UNASSIGNED_SURPLUS.Text					=		objResourceMgr.GetString("cmbEQU_UNASSIGNED_SURPLUS");
            capMANDATORY.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); //sneha
		}
		
	}
}
