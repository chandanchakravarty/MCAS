/******************************************************************************************
<Author				: -   P Kumar
<Start Date				: -	8/08/2006 12:02:40 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 

using System;
using System.Web.Mail;

namespace Cms.BusinessLayer.BlClient
{
	/// <summary>
	/// Summary description for FaxManager.
	/// </summary>
	public class FaxManager
	{
		private string fax_Number			= "";
		private string fax_Number_Suffix	= "";

		private MailFormat fax_Format;				// html or text format
		private string fax_Subject			= "";
		private string fax_Body				= "";
		// Complete Path Including fileName
		private string fax_Attachement		= "";

		
		private string fax_UserID			= "";
		private string fax_UserPWD			= "";
		private string fax_Hosturl			= "";
		private string fax_Hostport			= "";	// not being used right now
		private string fax_from				= "";

		private string fax_To_Name  		= "";

		public FaxManager()
		{
		}

		public string FAX_NUMBER
		{
			get {return fax_Number;}
			set {fax_Number = value;}
		}
		
		public string FAX_NUMBER_SUFFIX
		{
			get {return fax_Number_Suffix;}
			set {fax_Number_Suffix = value;}
		}

		public string FAX_USERID
		{
			get {return fax_UserID;}
			set {fax_UserID = value;}
		}
		public string FAX_USERPWD
		{
			get {return fax_UserPWD;}
			set {fax_UserPWD = value;}
		}
		
		public string FAX_SUBJECT
		{
			get {return  fax_Subject;}
			set {fax_Subject = value;}
		}
		
		public string FAX_BODY
		{
			get {return  fax_Body;}
			set {fax_Body = value;}
		}
		
		public string FAX_ATTACHMENT
		{
			get {return  fax_Attachement;}
			set {fax_Attachement = value;}
		}
		
		public MailFormat FAX_FORMAT
		{
			get {return  fax_Format;}
			set {fax_Format = value;}
		}

		public string FAX_HOSTURL
		{
			get {return fax_Hosturl;}
			set {fax_Hosturl = value;}
		}

		public string FAX_HOSTPORT
		{
			get {return fax_Hostport;}
			set {fax_Hostport = value;}
		}
		//Added on 3 April 2008
		public string FAX_FROM
		{
			get {return fax_from;}
			set {fax_from = value;}
		}

		public string FAX_TO_NAME
		{
			get {return fax_To_Name;}
			set {fax_To_Name = value;}
		}



		//		Send Fax to the client
		//		Check all required params are populated
		//		Return Codes
		//      Success = true
		
		public bool sendFax()
		{
			bool boolReturnStatus	= false;
			MailMessage objMailMessage = new MailMessage();
			if (fax_Attachement.ToString() != "")
			{
				try
				{
					MailAttachment objMailAttachment = new MailAttachment(fax_Attachement);
					objMailMessage.Attachments.Add(objMailAttachment);
				}
				catch (Exception ex)
				{ throw ex;}
			}

			
			try
			{
				// Need to have to complete email address of senders
				//Commented on 3 April 2008
//				objMailMessage.From			= string.Concat(fax_Number,"@", fax_Number_Suffix);
//				objMailMessage.To			= fax_Number;
				//Modified on 3 April 2008
				objMailMessage.From			=  fax_from;
				objMailMessage.To			=  string.Concat(fax_Number,"@", fax_Number_Suffix);
				objMailMessage.Subject		=  fax_Subject;
				//objMailMessage.BodyFormat	= fax_Format; //do not set for fax
				objMailMessage.Body			=  fax_Body; 

				// Mail attachements
				SmtpMail.SmtpServer			= fax_Hosturl;
				

//				objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate",1);
//				objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", fax_UserID);
//				objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", fax_UserPWD);
				
				SmtpMail.Send(objMailMessage);
				boolReturnStatus = true;
			}
			catch(System.Exception ex)
			{
				boolReturnStatus = false;
				throw (ex);
			}
			finally
			{
					
			}
			return boolReturnStatus;
		}
	}
}
