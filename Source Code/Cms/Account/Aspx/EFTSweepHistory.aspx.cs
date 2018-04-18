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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for EFTSweepHistory.
	/// </summary>
	public class EFTSweepHistory : Cms.Account.AccountBase
	{			
		protected System.Web.UI.WebControls.DropDownList cmbEntityType;
		protected System.Web.UI.WebControls.TextBox txtDateFromSpool;
		protected System.Web.UI.WebControls.TextBox txtDateToSpool;
		protected System.Web.UI.WebControls.TextBox txtDateFromSweep;
		protected System.Web.UI.WebControls.HyperLink hlkDateFromSweep;
		protected System.Web.UI.WebControls.CustomValidator csvDateFromSweep;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDateFromSweep;
		protected System.Web.UI.WebControls.TextBox txtDateToSweep;
		protected System.Web.UI.WebControls.HyperLink hlkDateToSweep;
		protected System.Web.UI.WebControls.CustomValidator csvDateToSweep;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDateSweep;
		protected System.Web.UI.WebControls.CustomValidator csvDateFromSpool;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDateFromSpool;
		protected System.Web.UI.WebControls.CustomValidator csvDateToSpool;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDateSpool;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTransactionAmount;
		protected System.Web.UI.WebControls.HyperLink hlkDateFromSpool;
		protected System.Web.UI.WebControls.HyperLink hlkDateToSpool;
		protected System.Web.UI.WebControls.CompareValidator cmpSpoolDate;
		protected System.Web.UI.WebControls.CompareValidator cmpSweepDate;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.WebControls.TextBox txtTransactionAmount;

	
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			//btnReport.Attributes.Add("onclick","javascript:AccountDetails();");
			hlkDateFromSpool.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateFromSpool,document.EFTSweepHistory.txtDateFromSpool)"); //Javascript Implementation for Calender				
			hlkDateToSpool.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateToSpool,document.EFTSweepHistory.txtDateToSpool)"); //Javascript Implementation for Calender				
			
			hlkDateFromSweep.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateFromSweep,document.EFTSweepHistory.txtDateFromSweep)"); //Javascript Implementation for Calender				
			hlkDateToSweep.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateToSweep,document.EFTSweepHistory.txtDateToSweep)"); //Javascript Implementation for Calender				
			
			base.ScreenId="406";

			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			btnReport.PermissionString	= gstrSecurityXML;

			if(!Page.IsPostBack)
			{
				FillCombo();
				SetValidators();
			}
			//FillCombo();
		}
		private void SetValidators()
		{
			revDateFromSpool.ValidationExpression = aRegExpDate;
			revToDateSpool.ValidationExpression	= aRegExpDate;
			revDateFromSweep.ValidationExpression = aRegExpDate;
			revToDateSweep.ValidationExpression	= aRegExpDate;		
	
			revTransactionAmount.ValidationExpression = aRegExpDoublePositiveNonZero;


			revDateFromSpool.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revToDateSpool.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revDateFromSweep.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revToDateSweep.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			
			csvDateFromSpool.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("448");
			csvDateToSpool.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("446");
			csvDateFromSweep.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("448");
			csvDateToSweep.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("446");
	
		}
		private void FillCombo()
		{
			cmbEntityType.Items.Insert(0,"");
			cmbEntityType.Items[0].Value = "";
			cmbEntityType.Items.Insert(1,"Customer");
			cmbEntityType.Items[1].Value = "CUST";
			cmbEntityType.Items.Insert(2,"Agency");
			cmbEntityType.Items[2].Value = "AGN";			
			cmbEntityType.Items.Insert(3,"Vendor");
			cmbEntityType.Items[3].Value = "VEN";

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
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			string strAmount = txtTransactionAmount.Text.ToString().Replace(",","");
			string strValue = "<script>"
				+ "window.open('EFTSweepHistoryDetails.aspx?DateFromSpool=" + txtDateFromSpool.Text + "&DateToSpool=" + txtDateToSpool.Text + "&DateFromSweep=" + txtDateFromSweep.Text + "&DateToSweep=" + txtDateToSweep.Text +  "&EntityType=" + cmbEntityType.SelectedItem.Value+"&TransactionAmount=" + strAmount.ToString().Trim()+"');</script>";
	
			if(!ClientScript.IsClientScriptBlockRegistered("EFTSweepHistoryDetails"))
				ClientScript.RegisterClientScriptBlock(this.GetType(),"EFTSweepHistoryDetails",strValue);
				
		}
	}
}
