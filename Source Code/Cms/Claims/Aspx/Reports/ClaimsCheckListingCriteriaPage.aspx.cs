/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> May 10,2006
	<End Date				: - >
	<Description			: - > Page is used to attach claims to policies
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;




namespace Cms.Claims.Aspx.Reports
{
	/// <summary>
	/// 
	/// </summary>
	public class ClaimsCheckListingCriteriaPage : Cms.CmsWeb.cmsbase  
	{
		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblHeader;
		protected Cms.CmsWeb.Controls.CmsButton btnGenerateReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReportType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label capFROM_AMOUNT;
		protected System.Web.UI.WebControls.Label capTO_AMOUNT;
		protected System.Web.UI.WebControls.Label capSTART_DATE;		
		protected System.Web.UI.WebControls.TextBox txtSTART_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkSTART_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTART_DATE;
		protected System.Web.UI.WebControls.Label capEND_DATE;
		protected System.Web.UI.WebControls.TextBox txtEND_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEND_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEND_DATE;
		protected System.Web.UI.WebControls.TextBox txtFROM_AMOUNT;		
		protected System.Web.UI.WebControls.TextBox txtTO_AMOUNT;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTO_AMOUNT;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnEND_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvEND_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvSTART_DATE;		
		protected System.Web.UI.WebControls.CompareValidator cmpTO_AMOUNT;
		protected System.Web.UI.WebControls.Label lblOrderBy;
		protected System.Web.UI.WebControls.ListBox lstOrderBy;
		protected System.Web.UI.WebControls.ListBox lstSelOrderBy;
		protected System.Web.UI.WebControls.CustomValidator csvSelOrderBy;
		protected System.Web.UI.WebControls.Button btnSel;
		protected System.Web.UI.WebControls.Button btnDeSel;
        //protected System.Web.UI.WebControls.Button btnGenerateReport;
//		protected System.Web.UI.WebControls.Label capLISTING_TYPE;
//		protected System.Web.UI.WebControls.RadioButtonList rblLISTING_TYPE;		
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;		
		
		

		#endregion
	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{			
			this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="382";
			
			lblMessage.Visible = false;
//			btnGenerateReport.CmsButtonClass	=	CmsButtonType.Write;
//			btnGenerateReport.PermissionString		=	gstrSecurityXML;
			btnSel.Attributes.Add("onClick","funcSetOrderBy();return false;");
			btnDeSel.Attributes.Add("onClick","funcRemoveOrderBy();return false;");

			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.Reports.ClaimsCheckListingCriteriaPage" ,System.Reflection.Assembly.GetExecutingAssembly());

			if(!IsPostBack)
			{
				btnGenerateReport.Attributes.Add("onClick","javascript:OpenReportsPopUp();return false;");
				hlkSTART_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_CLAIM_REPORT.txtSTART_DATE,document.CLM_CLAIM_REPORT.txtSTART_DATE)"); //Javascript Implementation for Date				
				hlkEND_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_CLAIM_REPORT.txtEND_DATE,document.CLM_CLAIM_REPORT.txtEND_DATE)"); //Javascript Implementation for Date				
				txtFROM_AMOUNT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
				txtTO_AMOUNT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");				
				txtSTART_DATE.Attributes.Add("onBlur","javascript: CompareEndDateWithStartDate();");
				txtEND_DATE.Attributes.Add("onBlur","javascript: CompareEndDateWithStartDate();");
				GetQueryStringValues();
				SetCaptions();
				SetErrorMessages();				
				btnGenerateReport.CmsButtonClass	=	CmsButtonType.Write;
				btnGenerateReport.PermissionString		=	gstrSecurityXML;
			}
		}
		#endregion	

		private void GetQueryStringValues()
		{
			if(Request.QueryString["ID"]!=null && Request.QueryString["ID"].ToString()!="")
				hidReportType.Value = Request.QueryString["ID"].ToString().Trim();

		}

		private void btnGenerateReport_Click(object sender, System.EventArgs e)
		{
		
		}

		private void SetCaptions()
		{
			capSTART_DATE.Text				=		objResourceMgr.GetString("txtSTART_DATE");
			capEND_DATE.Text					=		objResourceMgr.GetString("txtEND_DATE");
			capFROM_AMOUNT.Text				=		objResourceMgr.GetString("txtFROM_AMOUNT");	
			capTO_AMOUNT.Text				=		objResourceMgr.GetString("txtTO_AMOUNT");
            lblOrderBy.Text = objResourceMgr.GetString("lblOrderBy");
            btnGenerateReport.Text = objResourceMgr.GetString("btnGenerateReport");
			switch(hidReportType.Value)
			{
				case "ISSUED":
                    lblHeader.Text = objResourceMgr.GetString("lblHeader"); //"Check List Reports - Issued Checks";
					base.ScreenId="384";
					break;
				case "CLEARED":
					base.ScreenId="382";
                    lblHeader.Text = objResourceMgr.GetString("lblHeader1"); //"Check List Reports - Cleared Checks";
					break;
				case "OUTSTANDING":
					base.ScreenId="383";
                    lblHeader.Text = objResourceMgr.GetString("lblHeader2"); //"Check List Reports - Outstanding Checks";
					break;
				case "VOID":
					base.ScreenId ="445_0";
                    lblHeader.Text = objResourceMgr.GetString("lblHeader3"); //"Check List Reports - Void Checks";
                    break;  
				default:				
					base.ScreenId="382";
					lblHeader.Text = "Check List Reports";
					break;
			}
			lblHeader.Visible = true;
//			capLISTING_TYPE.Text				=		objResourceMgr.GetString("rblLISTING_TYPE");	
		}

		private void SetErrorMessages()
		{
			revSTART_DATE.ValidationExpression			=		aRegExpDate;
			revSTART_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			revEND_DATE.ValidationExpression			=		aRegExpDate;
			revEND_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			revFROM_AMOUNT.ValidationExpression	= aRegExpDoubleZeroToPositive;
			revFROM_AMOUNT.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			revTO_AMOUNT.ValidationExpression	= aRegExpDoubleZeroToPositive;
			revTO_AMOUNT.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
			spnEND_DATE.InnerHtml					=	"<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("942");
			csvEND_DATE.ErrorMessage					=   "<br>" +  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"940");
			csvSTART_DATE.ErrorMessage					=   "<br>" +  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"939");
			cmpTO_AMOUNT.ErrorMessage					=   "<br>" +  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"941");
			
		}

		
		
	}
}
