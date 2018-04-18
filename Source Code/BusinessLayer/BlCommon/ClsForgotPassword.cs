/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: April 07, 2005-	>
	<End Date				: April 07, 2005- >
	<Description			: - >This file is used to implement methods / functions for Forgot Password functionality 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
	
	
*******************************************************************************************/

using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Web.Mail;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsForgotPassword.
	/// </summary>
	public class ClsForgotPassword : ClsCommon
	{
		private const string strProcName = "Proc_GetPassword";
		//Server name will now take system defined value instead of being hard-coded.
		private string strServerName = System.Configuration.ConfigurationManager.AppSettings.Get("ServerName").ToString();
		public ClsForgotPassword()
		{
			
		}


		#region Get Password
		/// <summary>
		/// This method is used to get the password based on email id and user id match in database
		/// </summary>
		/// <param name="EmailId">Email Id of the user</param>
		/// <param name="UserId">Login Id of the user</param>
		/// <returns></returns>
		public string GetPassword(string EmailId, string UserId)
		{
			SqlParameter[] sqlParmas =  new SqlParameter[2];

			try
			{
				sqlParmas[0] = new SqlParameter("@User_Email",SqlDbType.NVarChar,50);
				sqlParmas[0].Value = EmailId;
				sqlParmas[1] = new SqlParameter("@User_Login_Id",SqlDbType.NVarChar,10);
				sqlParmas[1].Value=UserId;
				object obj = DataWrapper.ExecuteScalar(ConnStr,strProcName,sqlParmas);
				if(obj==null)
					return "";
				else
					return	obj.ToString();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{

			}
		}

		#endregion

		#region Send Email
		/// <summary>
		/// This function is used to send email if password found
		/// </summary>
		/// <param name="strEmail">Email Id of the User</param>
		/// <returns></returns>
		public void SendEmail(string strEmail, string strUserId, string strPassword)
		{
            MailMessage objMailMessage =  new MailMessage();

			objMailMessage.To = strEmail;
			objMailMessage.From	= "gtyagi@ebix.com";
			objMailMessage.BodyFormat = MailFormat.Html;
			objMailMessage.Subject = "Login Details";
			objMailMessage.Priority = MailPriority.Normal;

			objMailMessage.Body = "Dear User,<BR><BR> Following are the login deatils:<BR><BR><strong>User Id:</strong>"+strUserId+"<BR><strong>Password:</strong>"+strPassword+"<BR><BR> Thanks for contacting us<BR>";
			SmtpMail.SmtpServer = strServerName;
			SmtpMail.Send(objMailMessage);
			
		}
		#endregion
	}
}
