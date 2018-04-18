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
	/// Summary description for Logout.
	/// </summary>
	public class Logout : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{

			//Clear Session : 05 Feb 2009
			Session.Abandon();
			Session.Clear();
			Session.RemoveAll();

			
			// Put user code to initialize the page here : Commented on 24 Feb 2009
			//Response.Cookies["ckUserId"].Expires=DateTime.Now.AddYears(-30);
			//Response.Cookies["ckUserTypeId"].Expires=DateTime.Now.AddYears(-30);
			//Response.Cookies["ckUserNm"].Expires=DateTime.Now.AddYears(-30);
			#region Explanation regarding Multiple browsers
			//Commented Cookie "ckSysId" :Praveen Kasana
			/*If user has opened two browsers at the same time,and he logouts from one window resulting Cookie expiration
			 .After that if he login from second browser he redirected to the Agency home page 
			 becasue at that time Response.Cookies["ckSysId"] set to blank.
			 As per the MENU logic it take AgencyCode as input parameter.(AgencyCode is taken from Cookie which we set during login)
			 AgencyCode is set from Cookie.
			 GetAgencyID() in menucontents.js
			  */
			#endregion
			//Response.Cookies["ckSysId"].Expires=DateTime.Now.AddYears(-30);
			/*Response.Cookies["ckImgFld"].Expires=DateTime.Now.AddYears(-30);
			Response.Cookies["ckUserFLNm"].Expires=DateTime.Now.AddYears(-30);
			//Added by mohit.
			Response.Cookies["ckGridSize"].Expires=DateTime.Now.AddYears(-30);

			Request.Cookies["ckUserId"].Expires=DateTime.Now.AddYears(-30);
			Request.Cookies["ckUserTypeId"].Expires=DateTime.Now.AddYears(-30);
			Request.Cookies["ckUserNm"].Expires=DateTime.Now.AddYears(-30);
			Request.Cookies["ckSysId"].Expires=DateTime.Now.AddYears(-30);
			Request.Cookies["ckImgFld"].Expires=DateTime.Now.AddYears(-30);						
			Request.Cookies["ckImgFld"].Expires=DateTime.Now.AddYears(-30);
			Request.Cookies["ckUserFLNm"].Expires=DateTime.Now.AddYears(-30);
			//Added by mohit
			Request.Cookies["ckGridSize"].Expires=DateTime.Now.AddYears(-30);*/
				
			#region UPDATE LOGIN STATUS
//			ClsLogin clsLogin = new ClsLogin(); // creating object of clslogin class
//			if(Session["userId"]!=null && Session["userId"].ToString()!="")
//			{
//				//check if already log in ,,dont update to N //Miltiple windows on single machine
//				DataSet ldsStatus = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetUserLoggedStatus " + int.Parse(Session["userId"].ToString()));
//				if(ldsStatus!=null && ldsStatus.Tables[0].Rows.Count>0)
//				{
//					if(ldsStatus.Tables[0].Rows[0]["LOGGED_STATUS"].ToString()== "Y")
//					{   
//						clsLogin.UpdateLoggedStatus(int.Parse(Session["userId"].ToString()),"N");//dnt update if already log in                        
//					}
//					else
//						clsLogin.UpdateLoggedStatus(int.Parse(Session["userId"].ToString()),"Y");//update                         
//
//				}
//				
//			}
			#endregion
			
			
			
			Response.Redirect("/cms/cmsweb/aspx/login.aspx",true);
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
