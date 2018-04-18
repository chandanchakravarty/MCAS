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
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;

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
	<Modified Date			: - > 20/08/2007
	<Modified By			: - > Praveen Kasana
	<Purpose				: - > Adding checks when user attempts mltiple login ValidateMultipleLogin()
*******************************************************************************************/

namespace Cms.CmsWeb.Aspx
{
	/// <summary>
	/// Summary description for index.
	/// </summary>
	/// 
	
	public class index : cmsbase
	{
        public string strAlert = "";
        private ResourceManager objResourceMgr;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["userId"]!=null && Session["userId"].ToString()!="")
			{
				ValidateMultipleLogin(Session["userId"].ToString(),Session.SessionID.ToString());
			}

            //Added by Charles on 12-Mar-10 for Multilingual Implementation
            if (strAlert.Trim() == "")
            {
                SetCultureThread(GetLanguageCode());
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Aspx.index", Assembly.GetExecutingAssembly());
                strAlert = objResourceMgr.GetString("strAlert");
            }//Added till here
		}
		private void ValidateMultipleLogin(string userID,string sessionID)
		{
			//Check if user
			ClsLogin clsLogin = new ClsLogin();
			DataSet ldsStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetUserLoggedStatus " + int.Parse(Session["userId"].ToString()));
			if(ldsStatus!=null && ldsStatus.Tables[0].Rows.Count>0)
			{
				clsLogin.UpdateLoggedStatus(int.Parse(userID.ToString()),"N",sessionID);
				Session.Remove(ldsStatus.Tables[0].Rows[0]["SESSION_ID"].ToString());		
				
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
