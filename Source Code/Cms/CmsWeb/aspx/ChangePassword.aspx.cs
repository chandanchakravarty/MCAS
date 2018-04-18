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
	/// Summary description for ChangePassword.
	/// </summary>
	public class ChangePassword : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblPwd;
		protected System.Web.UI.WebControls.Label lblNWPwd;
		protected System.Web.UI.WebControls.Label CNFPwd;
		
		protected System.Web.UI.WebControls.TextBox txtNewPassword;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNewPassword;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOldPassword;
		protected System.Web.UI.WebControls.CustomValidator csvNewPassword;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvConfirmPassword;
		protected System.Web.UI.WebControls.CompareValidator cmpvNewPassword;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPassword;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNewPassword;
		
		protected System.Web.UI.WebControls.Label lblUserName;
		protected System.Web.UI.WebControls.TextBox txtUserName;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLEDFROM;
		protected System.Web.UI.WebControls.TextBox txtConfirmPassword;
		protected System.Web.UI.WebControls.ImageButton btnSave;
		System.Resources.ResourceManager objResourceMgr;

		//protected Cms.CmsWeb.Controls.CmsButton btnSave;
		//protected Cms.CmsWeb.Controls.CmsButton btnReset;
		
		protected System.Web.UI.WebControls.Label lblCNFPwd;

		ClsUser  objUser ;

		
	

		#region SetErrorMessages

		
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "89";
//		
//			// Put user code to initialize the page here
//
//			btnSave.CmsButtonClass	=	CmsButtonType.Write;
//			btnSave.PermissionString		=	gstrSecurityXML;
//
//			btnReset.CmsButtonClass	=	CmsButtonType.Write;
//			btnReset.PermissionString		=	gstrSecurityXML;
			
			
			if (Request.QueryString["CalledFrom"]!=null)
				hidCALLEDFROM.Value=Request.QueryString["CalledFrom"].ToString();
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Aspx.ChangePassword" ,System.Reflection.Assembly.GetExecutingAssembly());
	//		btnReset.Attributes.Add("onclick","javascript:Reset();");
			
			if(!Page.IsPostBack)
			{
				DefaultUserName();
				Setcaptions();
				SetErrorMessages();
			}
		}


		private void Setcaptions()
		{	
			lblUserName.Text			=		objResourceMgr.GetString("txtUserName");
			lblPwd.Text					=		objResourceMgr.GetString("txtOldPassword");
			lblNWPwd.Text				=		objResourceMgr.GetString("txtNewPassword");
			lblCNFPwd.Text				=	    objResourceMgr.GetString("txtConfirmPassword");		
			
		}

		private void SetErrorMessages()
		{
			//rfvOld_Password.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"68");			
			rfvNewPassword.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvOldPassword.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvConfirmPassword.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"69");
			cmpvNewPassword.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");				
			//csvNewPassword.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//csvOldPassword.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
//			revConfirmPassword.ValidationExpression	= aRegExpPasswordOneNumeric;
//			revConfirmPassword.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			revNewPassword.ValidationExpression		=aRegExpPasswordOneNumeric;
			revNewPassword.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1036");
			
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
			objUserInfo.USER_PWD	   =	txtPassword.Text;

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
			this.btnSave.Click += new System.Web.UI.ImageClickEventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{

			try
			{
				if(Page.IsValid == true)
				{		 
					int intRetVal;	//For retreiving the return value of business class save function
					objUser = new  ClsUser();
				
					//string User_Id = ;
				
					string New_Password = txtNewPassword.Text;

					ClsUserInfo objUserInfo = GetFormValue();

					//Calling the add method of business layer class
					intRetVal = objUser.SaveNewPassword(objUserInfo,New_Password);

					if(intRetVal>0)
					{
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1") + "<BR>"+Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1769"); //"<BR>You will be redirected to Login page soon.";
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
