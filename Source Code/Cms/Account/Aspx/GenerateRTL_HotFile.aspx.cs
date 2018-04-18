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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;

namespace Account
{
	/// <summary>
	/// Summary description for GenerateRTL_HotFile.
	/// </summary>
	public class GenerateRTL_HotFile : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnGenerateRTLHotFile;
		protected System.Web.UI.WebControls.Label lblProcess;
		protected string strUserName ="";
		protected string strPassWd ="";
		protected string strDomain ="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			SetScreenId();
			btnGenerateRTLHotFile.CmsButtonClass		=	CmsButtonType.Execute;
			btnGenerateRTLHotFile.PermissionString		=	gstrSecurityXML;
			btnGenerateRTLHotFile.Attributes.Add("Onclick","javascript:HideShowInProgress();");

		}
		private void SetScreenId()
		{
            base.ScreenId="432";
		}

		public void GenerateRTLHotFile()
		{
			
			ClsAccount objAcct = new ClsAccount();
			strUserName  = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
			strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
			strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

			Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
			try
			{
				
				if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
				{
					
					//string LocalUploadDirectory = RTLParameters.SelectSingleNode("LocalUploadDirectory").InnerText.Trim();
					//string FilePath = LocalUploadDirectory + RTLParameters.SelectSingleNode("RTLFileName").InnerText.Trim();

                    string LocalUploadDirectory = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("RTLUploadDirectory"));
					if (System.IO.Directory.Exists(LocalUploadDirectory))
					{
						string FilePath = LocalUploadDirectory + "RTLHotFile";
			
						string strDate = DateTime.Now.Year.ToString().Substring(2) 
							+ DateTime.Now.Month.ToString().PadLeft(2,'0') 
							+ DateTime.Now.Day.ToString().PadLeft(2,'0');

						string strTime = DateTime.Now.Hour.ToString().PadLeft(2,'0')
							+ DateTime.Now.Minute.ToString().PadLeft(2,'0');

						FilePath = FilePath + " - " + strDate + strTime + ".txt";

					
						DataSet dsFiscalPolicies= null;
				
						try
						{
							string PolNum = "";
				
							dsFiscalPolicies = new DataSet();
							dsFiscalPolicies = objAcct.GetFiscalYearActiveCancPolicies(int.Parse(GetUserId()));				
					
							System.IO.StreamWriter RTLFile = new System.IO.StreamWriter(FilePath, true);
						
							foreach(DataRow drFiscal in dsFiscalPolicies.Tables[0].Rows)
							{
								PolNum = drFiscal["POLICY_NUMBER"].ToString();
								//Code will come from SP itself
								//					if(drFiscal["POLICY_STATUS"].ToString().ToUpper() == "CANCEL")
								//						PolNum += "C";
								RTLFile.WriteLine(PolNum);
							}
							RTLFile.Close();
							lblMessage.Text = "RTL Hot File Generated Successfully.";
							lblMessage.Visible = true;

					
						}
						catch(Exception ex)
						{
							lblMessage.Text = ex.Message;
							lblMessage.Visible = true;
						
						}
					
		
					}
					else
					{
						lblMessage.Text = "Directory does not exists at " + LocalUploadDirectory.ToString();
						lblMessage.Visible = true;

					}

				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
				
			}

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
			this.btnGenerateRTLHotFile.Click += new System.EventHandler(this.btnGenerateRTLHotFile_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnGenerateRTLHotFile_Click(object sender, System.EventArgs e)
		{
			GenerateRTLHotFile();
		}
	}
}
