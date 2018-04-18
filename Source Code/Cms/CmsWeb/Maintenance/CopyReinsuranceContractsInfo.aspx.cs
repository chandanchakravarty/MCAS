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


using System.Resources; 
using System.Reflection;

using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BlAccount;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for CopyReinsuranceContractsInfo.
	/// </summary>
	public class CopyReinsuranceContractsInfo : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidContractID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		protected System.Web.UI.WebControls.DataGrid dgrReinsurenceContract;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId	=	" ";
			btnSubmit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSubmit.PermissionString = gstrSecurityXML;

			btnClose.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnClose.PermissionString = gstrSecurityXML;

			btnClose.Enabled = false;
			btnClose.Visible = false;
			if (!IsPostBack)
			{				
				if (Request.QueryString["CONTRACT_ID"] != null && Request.QueryString["CONTRACT_ID"].ToString() != "")
				{ 						
					hidContractID.Value=Request.QueryString["CONTRACT_ID"].ToString(); 
				}

				try
				{	
					DataTable dtContractInfo;
//					ClsReinsuranceInformation objReinsuranceInformation = new ClsReinsuranceInformation();

					dtContractInfo=ClsReinsuranceInformation.GetReinsurenceContractInfo(hidContractID.Value);
				
					if(dtContractInfo.Rows.Count>0)
					{
						btnSubmit.Enabled=true;
					}
					else
					{	
						btnSubmit.Enabled=false;
						lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"868");
						lblMessage.Visible=true;
						btnSubmit.Visible =false;
						return;
					}
					dgrReinsurenceContract.DataSource=dtContractInfo.DefaultView;
					dgrReinsurenceContract.DataBind();
			}
						
			catch(Exception ex)
			{
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{			
			CheckBox chkBox;
			DataTable dt=new DataTable();
			int Contract_id_new = 0;
			ClsReinsuranceInformation objReinsuranceInformation = new ClsReinsuranceInformation();
			int strCreatedBy = int.Parse(GetUserId());
			dt.Columns.Add("CONTRACT_ID",typeof(int));
			try
			{
				//bool blChecked=false;
				// Code for finding the checked vehicle in the datagrid.
				foreach(DataGridItem dgi in dgrReinsurenceContract.Items)
				{
					chkBox=(CheckBox)dgi.FindControl("chkSelect");
					if (chkBox != null && chkBox.Checked)
					{
						DataRow dr=dt.NewRow();
						dr["CONTRACT_ID"]=dgi.Cells[1].Text;
						dt.Rows.Add(dr);
					}			
				}
				if (dt.Rows.Count > 0)	
				{	
					Contract_id_new= objReinsuranceInformation.CopyContract(dt,strCreatedBy);					
				}
				string strScript="";
				lblMessage.Text="Contract Information Copied Successfully";
				lblMessage.Visible=true;
				strScript = @"<script language='javascript'>" + 
					"window.opener.RefreshWebgrid('');" +
					//"window.close();" + 
					"</script>" 
					;
				if (!ClientScript.IsStartupScriptRegistered("Refresh"))
				{
					ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);
				}
				btnSubmit.Visible = false;
				btnClose.Visible = true;
				btnClose.Enabled = true;

			}
			catch(Exception ex)
			{
				//gIntSaved=0;
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			} 		
		}
		
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			string strScript="";
			strScript = @"<script language='javascript'>" + 
				//"window.opener.RefreshWebgrid('');" +
				"window.close();" + 
				"</script>" 
				;
			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);
			}
		}
	}
}
