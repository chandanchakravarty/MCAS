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
using System.Text ; 
using Cms.BusinessLayer.BlCommon;

namespace Reports.Aspx
{
	/// <summary>
	/// Summary description for ReinsuranceReport.
	/// </summary>in	
	public class ReinsuranceReport : Cms.CmsWeb.cmsbase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.WebControls.RangeValidator rngtxtYearFrom;
		protected System.Web.UI.WebControls.RangeValidator rngtxtYearTo;
		protected System.Web.UI.WebControls.ListBox lstContractNumbers;
		protected System.Web.UI.WebControls.TextBox txtYearFrom;
		protected System.Web.UI.WebControls.TextBox txtYearTo;
		protected System.Web.UI.WebControls.CustomValidator cvtxtYearTo;
		protected System.Web.UI.WebControls.ListBox  lloB;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			base.ScreenId="434";
			btnReport.CmsButtonClass					=   Cms.CmsWeb.Controls.CmsButtonType.Execute; 
			btnReport.PermissionString					=	gstrSecurityXML;
			btnReport.Attributes.Add("onClick","ShowReport();return false;");
			//
			SetErrorMessages();
			
			if(!IsPostBack)
			{
				try
				{
					DataSet ds = ClsReinsuranceContact.FillContractNumber();
					lstContractNumbers.DataSource = ds.Tables[0];					
					lstContractNumbers.DataTextField = "CONTRACT_NUMBER";
					lstContractNumbers.DataValueField = "CONTRACT_ID";
					lstContractNumbers.DataBind();
					//lstContractNumbers.Items.Insert(0,new ListItem("","0"));
					lstContractNumbers.Items.Insert(0,new ListItem("All","0"));					
					this.lstContractNumbers.SelectedIndex =0;					
					txtYearFrom.Text=DateTime.Now.Year.ToString();
					txtYearTo.Text=DateTime.Now.Year.ToString();

					DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
					lloB.DataSource			= dtLOBs;
					lloB.DataTextField		= "LOB_DESC";
					lloB.DataValueField		= "LOB_ID";
					lloB.DataBind();
					lloB.Items.Insert(0,new ListItem("All","ALL"));
					this.lloB.SelectedIndex = 0;  
					
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
			
			}
		}

		private void SetErrorMessages()
		{
			rngtxtYearFrom.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngtxtYearFrom.MinimumValue = aAppMinYear  ;
			rngtxtYearFrom.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"673");

			rngtxtYearTo.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngtxtYearTo.MinimumValue = aAppMinYear  ;
			rngtxtYearTo.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"673");
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
			//this.Load += new System.EventHandler(this.btnReport_Click);

		}
		#endregion

		
	}
}
