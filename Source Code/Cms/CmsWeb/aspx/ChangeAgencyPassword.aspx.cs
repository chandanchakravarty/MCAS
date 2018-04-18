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
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using System.IO;
using System.Xml;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for ChangeAgencyPassword.
	/// </summary>
	public class ChangeAgencyPassword :  Cms.CmsWeb.cmsbase 
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUserName;
		protected System.Web.UI.WebControls.Label capOld_Password;
		protected System.Web.UI.WebControls.TextBox txtOld_Password;
		protected System.Web.UI.WebControls.Label capNew_Password;
        protected System.Web.UI.WebControls.Label cap_pass;
		protected System.Web.UI.WebControls.TextBox txtNew_Password;
		protected System.Web.UI.WebControls.Label capConfirm_Password;
		protected System.Web.UI.WebControls.TextBox txtConfirm_Password;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNew_Password;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvConfirm_Password;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOld_Password;
		protected System.Web.UI.WebControls.CompareValidator cmpvNew_Password;
		protected System.Web.UI.WebControls.CustomValidator csvNew_Password;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnCancel;
		protected System.Web.UI.WebControls.Label capUserName;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.WebControls.RegularExpressionValidator revConfirm_Password;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNew_Password;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLEDFROM; 
		System.Resources.ResourceManager objResourceMgr;

		ClsUser  objUser ;

        #endregion
	

		#region SetErrorMessages

		
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "89";
		
			// Put user code to initialize the page here

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnCancel.CmsButtonClass=	CmsButtonType.Read;
			btnCancel.PermissionString		=	gstrSecurityXML;
			if (Request.QueryString["CalledFrom"]!=null)
				hidCALLEDFROM.Value=Request.QueryString["CalledFrom"].ToString();
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Aspx.ChangeAgencyPassword" ,System.Reflection.Assembly.GetExecutingAssembly());
			btnReset.Attributes.Add("onclick","javascript:Reset();");
			btnCancel.Attributes.Add("onclick","javascript:Cancel();");
			if(!Page.IsPostBack)
			{
				DefaultUserName();
				SetCaptions();
				SetErrorMessages();
			}
			
		}


		private void SetCaptions()
		{	
			capUserName.Text						=		objResourceMgr.GetString("txtUserName");
			capOld_Password.Text					=		objResourceMgr.GetString("txtOld_Password");
			capNew_Password.Text					=		objResourceMgr.GetString("txtNew_Password");
			capConfirm_Password.Text				=	    objResourceMgr.GetString("txtConfirm_Password");
            cap_pass.Text                           =       Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10") ;//objResourceMgr.GetString("cap_pass");
            btnCancel.Text = objResourceMgr.GetString("btnCancel");
		}

		private void SetErrorMessages()
		{

			rfvUserName.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"34");
			rfvOld_Password.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"68");			
			rfvNew_Password.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvConfirm_Password.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"69");
			cmpvNew_Password.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");				
			//csvNew_Password.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			revConfirm_Password.ValidationExpression	= aRegExpPasswordOneNumeric;
			revConfirm_Password.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1036");
			revNew_Password.ValidationExpression		=aRegExpPasswordOneNumeric;
			revNew_Password.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1036");
		}

		private void DefaultUserName()
		{
			string user_ID = GetUserId();
			DataSet ds =null;
			ds=ClsUser.GetUserLoginName(user_ID);
			txtUserName.Text  = ds.Tables[0].Rows[0]["USER LOGIN"].ToString();
		}


		private ClsUserInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsUserInfo objUserInfo;
			objUserInfo = new ClsUserInfo();

			objUserInfo.USER_ID = int.Parse(GetUserId());
			objUserInfo.USER_LOGIN_ID  =	txtUserName.Text;
			objUserInfo.USER_PWD	   =	txtOld_Password.Text;

			return objUserInfo;
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
				if(Page.IsValid == true)
				{
					int intRetVal;	//For retreiving the return value of business class save function
					objUser = new  ClsUser();
				
					//string User_Id = ;
				
					string New_Password = txtNew_Password.Text;

					ClsUserInfo objUserInfo = GetFormValue();

					//Calling the add method of business layer class
					intRetVal = objUser.SaveNewPassword(objUserInfo,New_Password);

					if(intRetVal>0)
					{
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1") + "<BR> " + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1769");  //"<BR>You will be redirected to Login page soon.";
						hidFormSaved.Value			=	"1";
						string strCode = strCode = @"<script>RedirectToLogoutPage();</script>";
						ClientScript.RegisterStartupScript(this.GetType(),"test",strCode);
					}
					else if(intRetVal == -1)  //Comments
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"2");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2) //Comments
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"3");
						hidFormSaved.Value			=		"2";
					}

					else  //Comments
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"4");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				}
			}  

			catch(Exception ex)
			{

				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"4");
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			}

			finally
			{
				if(objUser!= null)
					objUser.Dispose();
			}
		}

		
	}
}
