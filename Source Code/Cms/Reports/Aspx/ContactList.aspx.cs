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
	public class ContactList : Cms.CmsWeb.cmsbase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnSumbit;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.ListBox lstUnAssignUser;
		protected System.Web.UI.WebControls.ListBox lstAssignUser;
		//protected Microsoft.Samples.ReportingServices.ReportViewer ReportViewer1;
		
		string Retvalues="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedUsersName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedUsers;
		protected Cms.CmsWeb.Controls.CmsButton Cmsbutton1;
		string RetNames="";

		private void Page_Load(object sender, System.EventArgs e)
		{
//			ReportViewer1.Visible = false;
			base.ScreenId="795";
			btnSumbit.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnSumbit.PermissionString	=	gstrSecurityXML;

			Cmsbutton1.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Execute;
			Cmsbutton1.PermissionString	=	gstrSecurityXML;

			Retvalues="";
			Retvalues = hidSelectedUsers.Value;	

			RetNames="";
			RetNames=hidSelectedUsersName.Value;	

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
					btnSumbit.Attributes.Add("onclick","CountAssignCodes();");
				}
				catch(Exception ex)
				{
					//throw ex;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				
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
			this.btnSumbit.Click += new System.EventHandler(this.btnSumbit_Click);
			this.Cmsbutton1.Click += new System.EventHandler(this.Cmsbutton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSumbit_Click(object sender, System.EventArgs e)
		{
			string str = "";
            string strReportServer = System.Configuration.ConfigurationManager.AppSettings.Get("ReportServer").ToString();
            string strReportPath = System.Configuration.ConfigurationManager.AppSettings.Get("ReportPath").ToString();

			if(CheckBox1.Checked == true)
			{
				str="0";
			}

			else
			{
				str=Retvalues;
				
				if (str == "")
					str="-1";
			}

			/*	foreach(ListItem li in lstAssignUser.Items)
				{
					//if (li.Selected == true)
					//{
						str+=li.Value + ",";					 
					//}
				}
				if (str!= "")
					str=str.Substring(0,str.Length-1); 
			}*/			
						
//			ReportViewer1.ServerUrl = strReportServer;
//			string param1 = "&UserID=" + str;
//			ReportViewer1.ReportPath= strReportPath + "/UserLicense" + param1;
//			Panel1.Visible =false;
//			ReportViewer1.Visible = true;
		}

		private void Cmsbutton1_Click(object sender, System.EventArgs e)
		{
			
			string strnew = "";
			
			if(CheckBox1.Checked == true)
			{
				strnew="0";
			}

			else
			{
				strnew=Retvalues;
				
				if (strnew == "")
					strnew="-1";
			}
			//path="/CustomReport.aspx?Userid="+ str + "&Customerid=" + str2;
			string path="CustomReport.aspx?Userid="+ strnew;
			Response.Redirect(path);
		}
	}
}
