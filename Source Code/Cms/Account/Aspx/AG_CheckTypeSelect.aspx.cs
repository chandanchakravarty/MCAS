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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Account;
using Cms.CmsWeb;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AG_CheckTypeSelect.
	/// </summary>
	public class AG_CheckTypeSelect : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblCommitStatus;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitOnAllPages;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "210";
			btnCommitOnAllPages.CmsButtonClass	= CmsButtonType.Read;
			btnCommitOnAllPages.PermissionString	= gstrSecurityXML;
		
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
			this.btnCommitOnAllPages.Click += new System.EventHandler(this.btnCommitOnAllPages_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCommitOnAllPages_Click(object sender, System.EventArgs e)
		{
			if(CommitAllChecks())
			{
				//Printing
			}

		}
		private bool CommitAllChecks()
		{
			bool flag=false;
			ClsChecks objClsCheck = new ClsChecks();
			string isCommited = "Y";
			int userID = int.Parse(GetUserId());
			int div_ID = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("DIV_ID"));
            int dept_ID = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("DEPT_ID"));
            int pc_ID = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("PC_ID"));
			
			try 
			{
				int intResult=0;
				intResult = objClsCheck.ExecCommitAllChecks(isCommited,userID,div_ID,dept_ID,pc_ID);
				if(intResult>=0)
				{
					lblCommitStatus.Text = "All Check Committed Successfully."; //to be changed
					flag=true;
				}
				else
				{
					lblCommitStatus.Text = "Unable to Commit.";
				}
				
				lblCommitStatus.Visible=true;
			}
			catch(Exception objExp)
			{
				lblCommitStatus.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("21") + "\n " + objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblCommitStatus.Visible=true;
			}
			finally
			{
				if(objClsCheck!=null)
					objClsCheck.Dispose();
			}
			return flag;
		
		}
		
	}
}
