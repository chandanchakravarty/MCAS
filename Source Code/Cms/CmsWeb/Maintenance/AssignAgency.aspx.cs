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
using Cms.BusinessLayer.BlApplication;
using Cms.DataLayer;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using System.Resources;
using System.Reflection; 
using Cms.CmsWeb.Controls;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AssignAgency.
	/// </summary>
	public class AssignAgency : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.ListBox lbAssignAgency;
		protected System.Web.UI.WebControls.ListBox lbUnassignAgency;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Assign_Agency;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAgencyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAssignAgencyID;
        protected System.Web.UI.WebControls.Label CapUnAssigned;
        protected System.Web.UI.WebControls.Label CapLabel;
        protected System.Web.UI.WebControls.Label CapAssigned;
        protected System.Web.UI.WebControls.Label CapAssign;
		private DataSet ds = new DataSet();
		public string strCalledFrom="";
		public string userSubCode="";
        System.Resources.ResourceManager objResourceMgr;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Request.QueryString["CALLAGENCY"] != null || Request.QueryString["CALLAGENCY"] != "")
			{
				strCalledFrom = Request.QueryString["CALLAGENCY"];
			}
			
			base.ScreenId = "10_1_3";
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AssignAgency", System.Reflection.Assembly.GetExecutingAssembly());

			if(Request.QueryString["USER_SUB_CODE"] != null || Request.QueryString["USER_SUB_CODE"] != "")
				userSubCode=Request.QueryString["USER_SUB_CODE"];

			btnSave.CmsButtonClass = CmsButtonType.Execute;
			btnSave.PermissionString = gstrSecurityXML;

			if(Request.QueryString["UserId"]!=null && Request.QueryString["UserId"].ToString().Length>0)
			{
				hidUserID.Value =Request.QueryString["UserId"].ToString();
			}

			if(!IsPostBack)
			{
				Populate_AssignedAgency();
				Populate_UnAssignedAgency();
				this.btnSave.Attributes.Add("onClick","javascript:CountUnassignAgency();CountAssignAgency();");
                SetCaptions();
			}
			lblMessage.Text = "";
			lblMessage.Visible=false;

		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int result;
			try
			{
				ClsUser objAssign = new ClsUser();
				string loggedUser = GetUserId();
				result = objAssign.AddUserAgency(loggedUser,int.Parse(hidUserID.Value),hidAgencyID.Value,hidAssignAgencyID.Value,userSubCode);
				Populate_AssignedAgency();
				Populate_UnAssignedAgency();

				if(result >= 1 && result != 6)
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"31");
				}
				
				this.lblMessage.Visible=true;
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			

		}
        private void SetCaptions()
        {
            CapUnAssigned.Text = objResourceMgr.GetString("CapUnAssigned");
            CapLabel.Text = objResourceMgr.GetString("CapLabel");
            CapAssigned.Text = objResourceMgr.GetString("CapAssigned");
            CapAssign.Text = objResourceMgr.GetString("CapAssign");
        }
	
		private void Populate_AssignedAgency()
		{ 
			DataSet ds = new DataSet();
			try
			{
				ds = ClsUser.Populate_AssignedAgency(int.Parse(hidUserID.Value));
				lbAssignAgency.DataSource = ds.Tables[0];
				lbAssignAgency.DataTextField = ds.Tables[0].Columns["AGENCY_DISPLAY_NAME"].ToString();
				lbAssignAgency.DataValueField = ds.Tables[0].Columns["AGENCY_ID"].ToString();
				lbAssignAgency.DataBind();
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}	
		}


		private void Populate_UnAssignedAgency()
		{

			DataSet ds = new DataSet();
			try
			{
				ds = ClsUser.Populate_UnAssignedAgency(int.Parse(hidUserID.Value));
				lbUnassignAgency.DataSource = ds.Tables[0];
				lbUnassignAgency.DataTextField = ds.Tables[0].Columns["AGENCY_DISPLAY_NAME"].ToString();
				lbUnassignAgency.DataValueField = ds.Tables[0].Columns["AGENCY_ID"].ToString();
				lbUnassignAgency.DataBind();
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
