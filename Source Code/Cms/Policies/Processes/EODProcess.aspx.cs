/******************************************************************************************
<Author				: -  Vijay Arora
<Start Date			: -	 06-02-2006
<End Date			: -	 
<Description		: -  Class for End of the Day Process.
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified By		: - 
<Modified Date		: - 
<Purpose			: - 
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls; 
using System.Xml;
using Cms.Model.Policy; 
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;

namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for EODProcess.
	/// </summary>
	public class EODProcess : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnLaunch;
        protected System.Web.UI.WebControls.Label capheader;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="5000_27";

			btnLaunch.CmsButtonClass = CmsButtonType.Write;
			btnLaunch.PermissionString = gstrSecurityXML;
            capheader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1240");
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
			this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnLaunch_Click(object sender, System.EventArgs e)
		{
			ClsEndOfDayProcess objTest = new ClsEndOfDayProcess();
			objTest.CurrentUserID = int.Parse(GetUserId());
			objTest.LaunchEODProcess();
		}
	}
}
