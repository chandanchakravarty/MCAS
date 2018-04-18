using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.DocumentMerge
{
	/// <Author>Deepak Gupta</Author>
	/// <Dated>Aug-29-2006</Date>
	/// <Purpose>This Webservice will be communication Media Between Document Merge Editor And WebServer</Purpose>
	public class DocumentMerge : System.Web.Services.WebService 
	{
		#region Constructor And Designer Generated Code
		public DocumentMerge()
		{
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion
		#endregion

		#region ReadTemplateFromServer
		[WebMethod]
		public byte[] ReadTemplateFromServer(string TemplateId)
		{
			string lstrUserName=ConfigurationManager.AppSettings.Get("IUserName");
            string lstrPassword = ConfigurationManager.AppSettings.Get("IPassWd");
            string lstrDomain = ConfigurationManager.AppSettings.Get("IDomain");
			ClsAttachment lImpertionation =  new ClsAttachment();

			if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
			{
				string FilePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\DocMergeTemplates\\Template_" + TemplateId + ".rtf";
				FileStream FSO = new FileStream(FilePath,FileMode.Open,FileAccess.Read);
				byte[] TemplateBytes = new byte[FSO.Length];
				FSO.Read(TemplateBytes,0,Convert.ToInt32(FSO.Length));
				FSO.Close();
				
				lImpertionation.endImpersonation();

				return(TemplateBytes);
			}
			else
				return(null);
		}
		#endregion

		#region GetTemplateName
		[WebMethod]
		public string GetTemplateName(string TemplateId)
		{
			return(new ClsDocumentMerge().getTemplateName(TemplateId).ToString());
		}
		#endregion

		#region GetTreeXmls
		[WebMethod]
		public string GetTreeXmls(string strType)
		{
			FileStream FSO;
			StreamReader SR;
			string TreeXml="";
			if (strType.ToUpper().Trim() == "CMS")
			{
				FSO = new FileStream(Server.MapPath("") + "\\W$18T.Xml",FileMode.Open,FileAccess.Read);
				SR = new StreamReader(FSO);
				TreeXml = SR.ReadToEnd().ToString();
			}
			else if (strType.ToUpper().Trim()=="USERDEFINDED")
			{
				//Need To Write Code for UserDefined
				//FSO = new FileStream(Server.MapPath("") + "\\W$18UDT.Xml",FileMode.Open,FileAccess.Read);
				//SR = new StreamReader(FSO);
				//TreeXml = SR.ReadToEnd().ToString();
			}	
			return(TreeXml);
		}
		#endregion

		#region GetWordMergeDictionary
		[WebMethod]
		public byte[] GetWordMergeDictionary(string DictionayName)
		{
			FileStream FSO = new FileStream(Server.MapPath("") + "\\" + DictionayName,FileMode.Open,FileAccess.Read);
			byte[] DictionaryBytes = new byte[FSO.Length];
			FSO.Read(DictionaryBytes,0,Convert.ToInt32(FSO.Length));
			return(DictionaryBytes);
		}
		#endregion 

		#region SaveTemplateOnServer
		//public bool SaveTemplateOnServer(byte[] EditedTemplateBytes,string TemplateId,string MarginXML)
		[WebMethod]
		public bool SaveTemplateOnServer(byte[] EditedTemplateBytes,string TemplateId)
		{
            string lstrUserName = ConfigurationManager.AppSettings.Get("IUserName");
            string lstrPassword = ConfigurationManager.AppSettings.Get("IPassWd");
            string lstrDomain = ConfigurationManager.AppSettings.Get("IDomain");
			
			ClsAttachment lImpertionation =  new ClsAttachment();
			
			if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
			{
                string TemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\DocMergeTemplates\\Template_" + TemplateId + ".rtf";
				
				FileStream STR=new FileStream(TemplateFileName,FileMode.Create,FileAccess.Write);
				System.IO.BinaryWriter BW = new BinaryWriter(STR);
				BW.Write(EditedTemplateBytes);
				BW.Close();
				BW = null;

				//ClsBuildQueryXml BQXml = new ClsBuildQueryXml(TemplateId,Server.MapPath(""),MarginXML,TemplateFileName);
				ClsBuildQueryXml BQXml = new ClsBuildQueryXml(TemplateId,Server.MapPath(""),TemplateFileName);
				BQXml.CreateQueryXML();

				lImpertionation.endImpersonation();

				return(true);
			}
			else 
				return(false);
		}
		#endregion

		#region GetDataXML
		[WebMethod]
		public string GetDataXML(string MergeID)
		{
			ClsGetDataXml GDX = new ClsGetDataXml(MergeID);
			return(GDX.CreateDataXML().ToString());
		}
		#endregion 

		#region SaveTemplateInTransactionLog
		[WebMethod]
		public string SaveTemplateInTransactionLog(byte[] MergedTemplateBytes,string MergeId,string TranLogMessage,string TransactionId,string OldTransId)
		{
			ClsDocumentMerge DocMerge = new ClsDocumentMerge();
			if (TransactionId=="0")
			{
				TransactionId = DocMerge.InsertUpdateTransactionLog(MergeId,OldTransId,TransactionId,TranLogMessage);
			}

			if (TransactionId!="0")
			{
                string lstrUserName = ConfigurationManager.AppSettings.Get("IUserName");
                string lstrPassword = ConfigurationManager.AppSettings.Get("IPassWd");
                string lstrDomain = ConfigurationManager.AppSettings.Get("IDomain");
				string TemplateFileName="";
			
				ClsAttachment lImpertionation =  new ClsAttachment();
				if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
				{
                    TemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\" + DocMerge.GetMergeedTemplateTransactionPath(TransactionId);
					if (!Directory.Exists(TemplateFileName)) Directory.CreateDirectory(TemplateFileName);
					TemplateFileName = TemplateFileName + TransactionId.ToString() + ".rtf";
				
					FileStream STR=new FileStream(TemplateFileName,FileMode.Create,FileAccess.Write);
					System.IO.BinaryWriter BW = new BinaryWriter(STR);
					BW.Write(MergedTemplateBytes);
					BW.Close();
					BW = null;

					lImpertionation.endImpersonation();
				}
				//Updating Template Path for Mail Attachment on SEND DOCUMENT screen
				string refPath = "\\DocumentMerge\\" +  DocMerge.GetMergeedTemplateTransactionPath(TransactionId);
				refPath = refPath + TransactionId.ToString() + ".rtf";
				ClsGetDataXml objpath = new ClsGetDataXml(MergeId);
				if(refPath!="")
				objpath.UpdateTemplatePath(refPath,MergeId);
								

			}

			return(TransactionId);
		}
		#endregion

		#region ReadDocumentFromTransaction
		[WebMethod]
		public byte[] ReadDocumentFromTransaction(string TransId)
		{
            string lstrUserName = ConfigurationManager.AppSettings.Get("IUserName");
            string lstrPassword = ConfigurationManager.AppSettings.Get("IPassWd");
            string lstrDomain = ConfigurationManager.AppSettings.Get("IDomain");
			ClsAttachment lImpertionation =  new ClsAttachment();

			if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
			{
                string FilePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()) + "\\DocumentMerge\\" + new ClsDocumentMerge().GetMergeedTemplateTransactionPath(TransId) + TransId.ToString() + ".rtf"; ;
				FileStream FSO = new FileStream(FilePath,FileMode.Open,FileAccess.Read);
				byte[] TemplateBytes = new byte[FSO.Length];
				FSO.Read(TemplateBytes,0,Convert.ToInt32(FSO.Length));
				FSO.Close();
				
				lImpertionation.endImpersonation();

				return(TemplateBytes);
			}
			else
				return(null);
		}
		#endregion
	}
}
