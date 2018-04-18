/******************************************************************************************
	<Author					: - > Swarup	
	<Start Date				: -	> 27-feb-2008
	<End Date				: - > 
	<Description			: - > To Show the report of RTL Import
	<Review Date			: - >
	<Reviewed By			: - >
	
	
   
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
using System.Reflection;
using System.Resources;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer;


namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for RTLImportHistory.
	/// </summary>
	public class RTLImportHistory : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.TextBox txtDepositNumber;
		protected System.Web.UI.WebControls.TextBox txtDateFrom;
		protected System.Web.UI.WebControls.HyperLink hlkDateFrom;
		protected System.Web.UI.WebControls.CustomValidator csvDateFrom;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDateFrom;
		protected System.Web.UI.WebControls.TextBox txtDateTo;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capDept_Number;
        protected System.Web.UI.WebControls.Label capFrom_Date;
        protected System.Web.UI.WebControls.Label capDEPOSIT_TYPE; //Added by aditya on 29-08-2011 for itrack # 1327
        protected System.Web.UI.WebControls.Label capTo_Date;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label capStatus;
		protected System.Web.UI.WebControls.HyperLink hlkDateTo;
		protected System.Web.UI.WebControls.CustomValidator csvDateTo;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDate;
		protected System.Web.UI.WebControls.CompareValidator cmpDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDepositNumber;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.WebControls.DropDownList StatusList;
        protected System.Web.UI.WebControls.DropDownList cmbDEPOSIT_TYPE;
        protected System.Web.UI.WebControls.Label capDEPOSIT_NOTE;
        System.Resources.ResourceManager objResourceMgr;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			hlkDateFrom.Attributes.Add("OnClick","fPopCalendar(document.RTLImportHistory.txtDateFrom,document.RTLImportHistory.txtDateFrom)"); //Javascript Implementation for Calender				
			hlkDateTo.Attributes.Add("OnClick","fPopCalendar(document.RTLImportHistory.txtDateTo,document.RTLImportHistory.txtDateTo)"); //Javascript Implementation for Calender				

			base.ScreenId = "420";
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.RTLImportHistory", System.Reflection.Assembly.GetExecutingAssembly());
			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			btnReport.PermissionString	= gstrSecurityXML;

			if(!Page.IsPostBack)
			{
				SetValidators();
                setCaptions();
                BindNotification();
                BindDepositType();
               // BindDownPaymentModeDD();
			}

		}

		private void SetValidators()
		{
			revDateFrom.ValidationExpression = aRegExpDate;
			revToDate.ValidationExpression	= aRegExpDate;

			revDepositNumber.ValidationExpression = aRegExpInteger;

			revDepositNumber.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1019");
			revDateFrom.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revToDate.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			csvDateFrom.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("448");
			csvDateTo.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("446");



		}

		private void btnReport_Click(object sender, System.EventArgs e)
		{


            String str = "RTLImportHistoryDetails.aspx?DateFrom=" + txtDateFrom.Text + "&DateTo=" + txtDateTo.Text + "&DepositNumber=" + txtDepositNumber.Text + "&ProcessStatus=" + StatusList.SelectedValue.ToString() + "&DepositType=" + cmbDEPOSIT_TYPE.SelectedValue.ToString();
			string strValue = "<script>"
                + "window.open('" + str + "','ImportHistoryDetails','resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50'); return false;</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "RTLImportHistoryDetails", strValue);


            //if(!Page.IsClientScriptBlockRegistered("RTLImportHistoryDetails"))
            //    Page.RegisterClientScriptBlock("RTLImportHistoryDetails",strValue);
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
        private void BindNotification()
        {

            StatusList.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%RTL");
            StatusList.DataTextField = "LookupDesc";
            StatusList.DataValueField = "LookupCode";
            StatusList.DataBind();
        }

        private void BindDepositType()
        {
            cmbDEPOSIT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DESTYP");
            cmbDEPOSIT_TYPE.DataTextField = "LookupDesc";
            cmbDEPOSIT_TYPE.DataValueField = "LookupID";
            cmbDEPOSIT_TYPE.DataBind();
            ListItem Li = new ListItem();
            Li = cmbDEPOSIT_TYPE.Items.FindByValue("14916");
            cmbDEPOSIT_TYPE.Items.Remove(Li);
            ListItem Lit = new ListItem();
            Lit = cmbDEPOSIT_TYPE.Items.FindByValue("14917");
            cmbDEPOSIT_TYPE.Items.Remove(Lit);
            ListItem Lit1 = new ListItem();
            Lit1 = cmbDEPOSIT_TYPE.Items.FindByValue("14918");
            cmbDEPOSIT_TYPE.Items.Remove(Lit1);
            ListItem Lit2 = new ListItem();
            Lit2 = cmbDEPOSIT_TYPE.Items.FindByValue("14831");
            cmbDEPOSIT_TYPE.Items.Remove(Lit2);
            cmbDEPOSIT_TYPE.Items.Insert(0, Lit2);
            ListItem Lit3 = new ListItem();
            Lit3 = cmbDEPOSIT_TYPE.Items.FindByValue("14832");
            cmbDEPOSIT_TYPE.Items.Remove(Lit3);
            cmbDEPOSIT_TYPE.Items.Insert(1, Lit3);
        }

        private void setCaptions()
        {
            capHeader.Text = objResourceMgr.GetString("capHeader");
            capDept_Number.Text = objResourceMgr.GetString("capDept_Number");
            capFrom_Date.Text = objResourceMgr.GetString("capFrom_Date");
            capTo_Date.Text = objResourceMgr.GetString("capTo_Date");
            capMessage.Text = objResourceMgr.GetString("capMessage");
            btnReport.Text = objResourceMgr.GetString("btnReport");
            capStatus.Text = objResourceMgr.GetString("capStatus");
            capDEPOSIT_TYPE.Text = objResourceMgr.GetString("capDEPOSIT_TYPE"); //Changed by aditya on 29-08-2011 for itrack # 1327

        }
        private void InitializeComponent()
		{    
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
