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
using Cms.DataLayer;
using Cms.CmsWeb;

namespace Reports.Aspx
{
	public class ReportUserLicense : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.ListBox lstUnAssignUser;
		protected System.Web.UI.WebControls.ListBox lstAssignUser;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedUsersName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedUsers;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="337";		
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowPopup();return false;");
			if(!IsPostBack)
			{
				try
				{
					Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
					DataSet ds = objDataWrapper.ExecuteDataSet("select (isnull(USER_FNAME,'') + ' ' + isnull(USER_LNAME,'')) as UserName ,USER_ID as ID ,* from MNT_USER_LIST order by UserName");
					lstUnAssignUser.DataSource = ds.Tables[0];					
					lstUnAssignUser.DataTextField = "UserName";
					lstUnAssignUser.DataValueField = "ID";
					lstUnAssignUser.DataBind();

					lstUnAssignUser.Attributes.Add("ondblclick","AssignUsers();");
					lstAssignUser.Attributes.Add("ondblclick","UnAssignUsers();");				
					//btnSumbit.Attributes.Add("onclick","CountAssignCodes();");
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				
			}	
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{			
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
	}
}
