	/******************************************************************************************
	<Author					: -   Ajit Singh Chahal
	<Start Date				: -   5/16/2005 11:31:31 AM
	<End Date				: -	
	<Description			: -   Business Logic for GL Account Ranges.
	<Review Date			: - 
	<Reviewed By			: - 	
	Modification History
	<Modified Date			: - 
	<Modified By			: - 
	<Purpose				: - 
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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for PrintChartOfAccountRanges.
	/// </summary>

	public class CheckRegisterItemsPrintPopup  :  Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.HyperLink hlkFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.HyperLink hlkToDate;
		protected System.Web.UI.WebControls.DropDownList cmbCHECK_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHECK_TYPE;
		protected Cms.CmsWeb.Controls.CmsButton btnDisplay1;
		protected Cms.CmsWeb.Controls.CmsButton btnExportToExcel1;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint1;
		protected Cms.CmsWeb.Controls.CmsButton btnDisplay;
		protected Cms.CmsWeb.Controls.CmsButton btnExportToExcel;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFromDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFromDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvToDate;
		protected System.Web.UI.WebControls.CompareValidator cpvEND_DATE;
		protected System.Web.UI.WebControls.Literal litReport;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			

			base.ScreenId  = "207_1";
			btnDisplay.CmsButtonClass		=	CmsButtonType.Execute;
			btnDisplay.PermissionString	=	gstrSecurityXML;
			btnDisplay1.CmsButtonClass		=	CmsButtonType.Execute;
			btnDisplay1.PermissionString	=	gstrSecurityXML;


			btnExportToExcel.CmsButtonClass		=	CmsButtonType.Execute;
			btnExportToExcel.PermissionString	=	gstrSecurityXML;
			btnExportToExcel1.CmsButtonClass		=	CmsButtonType.Execute;
			btnExportToExcel1.PermissionString	=	gstrSecurityXML;

			btnPrint.CmsButtonClass		=	CmsButtonType.Execute;
			btnPrint.PermissionString	=	gstrSecurityXML;
			btnPrint1.CmsButtonClass		=	CmsButtonType.Execute;
			btnPrint1.PermissionString	=	gstrSecurityXML;
			
			SetErrorMessages();
			
				
			if(IsPostBack)
			{
				
			}
			else
			{
				Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupDDL(ref cmbCHECK_TYPE,"CPAYTP");
				cmbCHECK_TYPE.Items.Insert(0,new ListItem("All","0"));
				cmbCHECK_TYPE.Items.Insert(0,new ListItem("",""));
			}
			
			hlkFromDate.Attributes.Add("OnClick","fPopCalendar(document.frmPrint.txtFromDate,document.frmPrint.txtFromDate)");
			hlkToDate.Attributes.Add("OnClick","fPopCalendar(document.frmPrint.txtToDate,document.frmPrint.txtToDate)");
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
			this.btnDisplay1.Click += new System.EventHandler(this.btnDisplay_Click);
			this.btnExportToExcel1.Click += new System.EventHandler(this.btnExportToExcel_Click);
			this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
			this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void  SetErrorMessages()
		{
			revFromDate.ValidationExpression	= aRegExpDate;
			revToDate.ValidationExpression	=	aRegExpDate;
			revFromDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revToDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			cpvEND_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("447");
		}
		private void btnDisplay_Click(object sender, System.EventArgs e)
		{
			
			litReport.Text =  GetHtmlToRender();
						
		}

		private void btnExportToExcel_Click(object sender, System.EventArgs e)
		{
			Response.Clear(); 
			Response.Buffer=true; 
			Response.Charset = ""; 
			Response.ContentType = "application/vnd.ms-excel"; 
			System.IO.StringWriter stwWriter = new System.IO.StringWriter(); 
			Response.Write(GetHtmlToRender().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>","")); 
			Response.End(); 
		}
		
		private string GetHtmlToRender()
		{
			DateTime fromDate=DateTime.Parse(txtFromDate.Text);
			DateTime toDate=DateTime.Parse(txtToDate.Text);
			int checkType=int.Parse(cmbCHECK_TYPE.SelectedValue);

			string xml =   ClsChecks.GetChecksForPrint(fromDate,toDate,checkType).GetXml();
			string time = DateTime.Now.TimeOfDay.ToString();
			string dat = DateTime.Now.ToString();
			dat = dat.Substring(0,dat.IndexOf(' '));
			time = time.Substring(0,time.IndexOf('.'));
			xml = xml.Replace("<NewDataSet>","<NewDataSet><Date>" + dat + "</Date>" + "<Time>" +  time + "</Time>");
			XmlDocument xmlDocInput = new XmlDocument();
			xmlDocInput.LoadXml(xml); 
			//XslTransform tr = new XslTransform();
            XslCompiledTransform tr = new XslCompiledTransform();
			tr.Load(Server.MapPath("/cms/Account/Support/ChecksRegisterPrint.xsl"));
			XPathNavigator nav = ((IXPathNavigable) xmlDocInput).CreateNavigator();
			StringWriter sw1 = new StringWriter();
			tr.Transform(nav,null,sw1);
			string Html = sw1.ToString();
			sw1.Close();
			return Html;
		}
		
	}
}
