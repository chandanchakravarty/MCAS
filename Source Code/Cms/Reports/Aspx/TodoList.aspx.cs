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
	public class TodoList : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblInceptionEndDate;
		protected System.Web.UI.WebControls.TextBox txtInceptionEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkInceptionEndDate;
		protected System.Web.UI.WebControls.Label lblInceptionStartDate;
		protected System.Web.UI.WebControls.TextBox txtInceptionStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkInceptionStartDate;
		protected System.Web.UI.WebControls.Label lblUnderwriter;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.DropDownList lstAddress;
		protected System.Web.UI.WebControls.Label lblNameFormat;
		protected System.Web.UI.WebControls.DropDownList lstNameFormat;
		protected System.Web.UI.WebControls.ListBox lstUnderwriter;
		protected System.Web.UI.WebControls.RadioButton rdoConsider1;
		protected System.Web.UI.WebControls.RadioButton rdoConsider2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revInceptionStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpToDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revInceptionEndDate;
		protected System.Web.UI.WebControls.RadioButton rdAddress1;
		protected System.Web.UI.WebControls.RadioButton rdAddress2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ListBox lstUnderWriterName;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;

		private void Page_Load(object sender, System.EventArgs e)
		{
			hlkInceptionStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtInceptionStartDate,document.forms[0].txtInceptionStartDate)");			
			hlkInceptionEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtInceptionEndDate,document.forms[0].txtInceptionEndDate)");			
			revInceptionStartDate.ValidationExpression = aRegExpDate;
			revInceptionEndDate.ValidationExpression = aRegExpDate;			
			base.ScreenId="336";				
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");
			if(!IsPostBack)
			{
				try
				{				
					Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
					DataSet ds = objDataWrapper.ExecuteDataSet("Select (isnull(User_Fname,'') + ' ' + isnull(User_Lname,'')) as UnderwriterName ,User_id as ID  FROM MNT_USER_LIST WHERE USER_TYPE_ID IN (SELECT USER_TYPE_ID FROM MNT_USER_TYPES WHERE USER_TYPE_CODE='UWT') order by UnderwriterName");
					lstUnderwriter.DataSource = ds.Tables[0];					
					lstUnderwriter.DataTextField = "UnderwriterName";
					lstUnderwriter.DataValueField = "ID";
					lstUnderwriter.DataBind();
					this.lstUnderwriter.Items.Insert(0,"All");
					this.lstUnderwriter.SelectedIndex =0;
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
