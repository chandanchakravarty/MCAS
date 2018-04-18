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

/******************************************************************************************
	<Author					: - >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for ForgotPassword.
	/// </summary>
	public class ForgotPassword : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.TextBox txtUserId;
		protected System.Web.UI.WebControls.Button btnGetPassword;
		protected System.Web.UI.WebControls.TextBox txtEmailId;
	
		#region Form Variables

		string strEmailId;   // Holds the value for EmailId field
		string strUserId;
		protected System.Web.UI.WebControls.Label lblMessage;   // Holds the value for UserId field

		Cms.BusinessLayer.BlCommon.ClsForgotPassword objForgotPassword;    // Creating instanse of ClsForgotPassword Class
		
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			lblMessage.Visible = false;
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
			this.btnGetPassword.Click += new System.EventHandler(this.btnGetPassword_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Method to do form processing

		/// <summary>
		/// Check for password and display on Label
		/// </summary>
		private void GetPassword()
		{
			try
			{
				GetFormValues();
				if(DoValidationCheck())
				{
				objForgotPassword =new Cms.BusinessLayer.BlCommon.ClsForgotPassword();
					string strPassword = objForgotPassword.GetPassword(strEmailId,strUserId);
					
					if(strPassword!=null && strPassword!="")
					{
						objForgotPassword.SendEmail(strEmailId,strUserId,strPassword);
						lblMessage.Text = "Email Send Successfully";
						lblMessage.Visible = true;
					}
					else
					{
						lblMessage.Text = "No Password Found";
						lblMessage.Visible = true;
						
					}
				}

				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				// Publish Exception
			}
		}

		/// <summary>
		/// Fetch form's value and stores into variables.
		/// </summary>
		private void GetFormValues()
		{
			try
			{
				strEmailId	=	txtEmailId.Text.Trim().Replace("'","''");
				strUserId	=	txtUserId.Text.Trim().Replace("'","''");
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Validate posted data from form. Calls in SaveFormData function
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool DoValidationCheck()
		{
			try
			{
				if(txtEmailId.Text.Trim().Equals(""))
				{
					return false;
				}
				if(txtUserId.Text.Trim().Equals(""))
				{
					return false;
				}
				return true;
			}
			catch
			{
				return false;
			}


		}


		#endregion
		private void btnGetPassword_Click(object sender, System.EventArgs e)
		{
		GetPassword();
		}
	}
}
