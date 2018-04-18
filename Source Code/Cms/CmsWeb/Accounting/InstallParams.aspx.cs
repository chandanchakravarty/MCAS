/******************************************************************************************
<Author				: -   Vijay
<Start Date				: -	6/6/2005 6:52:19 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - d
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
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.Model.Maintenance.Accounting;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;


namespace Cms.Accounting
{
	/// <summary>
	/// d
	/// </summary>
	public class InstallParams : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeptID;
		protected System.Web.UI.WebControls.Label capINSTALL_DAYS_IN_ADVANCE;
		protected System.Web.UI.WebControls.TextBox txtINSTALL_DAYS_IN_ADVANCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSTALL_DAYS_IN_ADVANCE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINSTALL_DAYS_IN_ADVANCE;
		protected System.Web.UI.WebControls.Label capINSTALL_NOTIFY_ACCOUNTEXE;
		protected System.Web.UI.WebControls.CheckBox chkINSTALL_NOTIFY_ACCOUNTEXE;
		protected System.Web.UI.WebControls.Label capINSTALL_NOTIFY_OTHER_USERS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm ACT_INSTALL_PARAMS;
		protected System.Web.UI.WebControls.ListBox lbUnAssignUsers;
		protected System.Web.UI.WebControls.ListBox lbAssignUsers;
		protected System.Web.UI.WebControls.CheckBox chkINSTALL_NOTIFY_UNDERWRITER;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSTALL_NOTIFY_OTHER_USERS;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		//private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		//Defining the business layer class object
		ClsInstallParams  objInstallParams ;
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
			rfvINSTALL_DAYS_IN_ADVANCE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");

			revINSTALL_DAYS_IN_ADVANCE.ValidationExpression	= aRegExpInteger;
			revINSTALL_DAYS_IN_ADVANCE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
			
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return formReset();");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="182";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			
			lblMessage.Visible = false;
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Accounting.InstallParams" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				this.btnSave.Attributes.Add("onClick","javascript:CountAssignDepts();");					
				GetOldData(true);
				SetCaptions();
			}
		}//end pageload
		#endregion
		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsInstallParamsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsInstallParamsInfo objInstallParamsInfo;
			objInstallParamsInfo = new ClsInstallParamsInfo();

			if (txtINSTALL_DAYS_IN_ADVANCE.Text.Trim() != "")
				objInstallParamsInfo.INSTALL_DAYS_IN_ADVANCE = int.Parse(txtINSTALL_DAYS_IN_ADVANCE.Text);

			if (chkINSTALL_NOTIFY_ACCOUNTEXE.Checked == true)
				objInstallParamsInfo.INSTALL_NOTIFY_ACCOUNTEXE = "Y";
			else
				objInstallParamsInfo.INSTALL_NOTIFY_ACCOUNTEXE = "N";
			
			if(chkINSTALL_NOTIFY_UNDERWRITER.Checked == true)
				objInstallParamsInfo.INSTALL_NOTIFY_UNDERWRITER = "Y";
			else
				objInstallParamsInfo.INSTALL_NOTIFY_UNDERWRITER = "N";

			objInstallParamsInfo.INSTALL_NOTIFY_OTHER_USERS=	hidINSTALL_NOTIFY_OTHER_USERS.Value;

			//Returning the model object

			return objInstallParamsInfo;
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
				objInstallParams = new  ClsInstallParams();

				//Retreiving the form values into model class object
				ClsInstallParamsInfo objInstallParamsInfo = GetFormValue();

				if(hidOldData.Value == "") //save case
				{
					objInstallParamsInfo.CREATED_BY = int.Parse(GetUserId());
					objInstallParamsInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objInstallParams.Add(objInstallParamsInfo);

					if(intRetVal>0)
					{
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	=	"1";
						GetOldData(false);
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text		=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value	=		"2";
					}
					else
					{
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value	=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsInstallParamsInfo objOldInstallParamsInfo;
					objOldInstallParamsInfo = new ClsInstallParamsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldInstallParamsInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objInstallParamsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objInstallParamsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objInstallParamsInfo.IS_ACTIVE = "Y";

					//Updating the record using business layer class object
					intRetVal	= objInstallParams.Update(objOldInstallParamsInfo,objInstallParamsInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value	=	"1";
						GetOldData(false);
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value	=	"1";
					}
					else 
					{
						lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value	=	"1";
					}
					lblMessage.Visible = true;
				}
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
				if(objInstallParams!= null)
					objInstallParams.Dispose();
			}
		}
		
		#endregion

		private void SetCaptions()
		{
			capINSTALL_DAYS_IN_ADVANCE.Text		=	objResourceMgr.GetString("txtINSTALL_DAYS_IN_ADVANCE");
			capINSTALL_NOTIFY_ACCOUNTEXE.Text	=	objResourceMgr.GetString("chkINSTALL_NOTIFY_ACCOUNTEXE");
			capINSTALL_NOTIFY_OTHER_USERS.Text	=	objResourceMgr.GetString("txtINSTALL_NOTIFY_OTHER_USERS");
		}

		/// <summary>
		/// Retreivie the data from database using Bl object
		/// </summary>
		/// <param name="FillCtrl">If true fills the Ctrl with values else sets the hidOldaData control only</param>
		private void GetOldData( bool FillCtrl)
		{
			try
			{
				DataSet ds = ClsInstallParams.GetInstallParams();

				//Setting the old data xml hidden control
				if (ds.Tables[0].Rows.Count > 0)
				{
					hidOldData.Value = ds.GetXml();

					if (FillCtrl == true)
					{
						DataRow dr = ds.Tables[0].Rows[0];
						txtINSTALL_DAYS_IN_ADVANCE.Text = dr["INSTALL_DAYS_IN_ADVANCE"].ToString();;
						
						if (dr["INSTALL_NOTIFY_ACCOUNTEXE"].ToString().ToUpper() == "Y")
							chkINSTALL_NOTIFY_ACCOUNTEXE.Checked = true;
						else
							chkINSTALL_NOTIFY_ACCOUNTEXE.Checked = false;

						if (dr["INSTALL_NOTIFY_UNDERWRITER"].ToString().ToUpper() == "Y")
							chkINSTALL_NOTIFY_UNDERWRITER.Checked = true;
						else
							chkINSTALL_NOTIFY_UNDERWRITER.Checked = false;

						hidINSTALL_NOTIFY_OTHER_USERS.Value = dr["INSTALL_NOTIFY_OTHER_USERS"].ToString();
						
						
					}		
				}
				ds.Dispose();
				PopulateListbox();
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
		private void PopulateListbox()
		{
			try
			{	
				System.Data.DataSet ds = ClsInstallParams.GetInstallParamsAssignedUnassignedUsers(GetSystemId());

				lbUnAssignUsers.DataSource = ds.Tables["UnAssignedUsers"];
				lbUnAssignUsers.DataTextField = "USER_NAME";
				lbUnAssignUsers.DataValueField = "USER_ID";
				lbUnAssignUsers.DataBind();
				lbUnAssignUsers.Items.Insert(0, new ListItem("---Please Select---",""));

				lbAssignUsers.DataSource = ds.Tables["AssignedUsers"];
				lbAssignUsers.DataTextField = "USER_NAME";
				lbAssignUsers.DataValueField = "USER_ID";
				lbAssignUsers.DataBind();
				lbAssignUsers.Items.Insert(0, new ListItem("---Please Select---",""));
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
	}
}
