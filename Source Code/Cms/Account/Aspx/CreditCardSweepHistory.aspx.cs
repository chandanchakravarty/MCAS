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
	/// Summary description for CreditCardSweepHistory.
	/// </summary>
	public class CreditCardSweepHistory : Cms.Account.AccountBase
	{		
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
		protected System.Web.UI.WebControls.RegularExpressionValidator revTransactionAmount;
		protected System.Web.UI.WebControls.CustomValidator csvDateToSpool;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDateSpool;
		protected System.Web.UI.WebControls.HyperLink hlkDateFromSpool;
		protected System.Web.UI.WebControls.HyperLink hlkDateToSpool;
		protected System.Web.UI.HtmlControls.HtmlForm EFTSweepHistory;
		protected System.Web.UI.WebControls.CompareValidator cmpSpoolDate;
		protected System.Web.UI.WebControls.CompareValidator cmpSweepDate;
        protected System.Web.UI.WebControls.DropDownList paypalStatusList;  
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.WebControls.TextBox txtTransactionAmount;
		//Added By Raghav For itrack Issue4646
		protected System.Web.UI.WebControls.ListBox txtUsers;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			//btnReport.Attributes.Add("onclick","javascript:AccountDetails();");
			hlkDateFromSpool.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateFromSpool,document.EFTSweepHistory.txtDateFromSpool)"); //Javascript Implementation for Calender				
			hlkDateToSpool.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateToSpool,document.EFTSweepHistory.txtDateToSpool)"); //Javascript Implementation for Calender				
			
			hlkDateFromSweep.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateFromSweep,document.EFTSweepHistory.txtDateFromSweep)"); //Javascript Implementation for Calender				
			hlkDateToSweep.Attributes.Add("OnClick","fPopCalendar(document.EFTSweepHistory.txtDateToSweep,document.EFTSweepHistory.txtDateToSweep)"); //Javascript Implementation for Calender				
			
			base.ScreenId = "407";

			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			btnReport.PermissionString	= gstrSecurityXML;

			if(!Page.IsPostBack)
			{
				FillCombo();				
				SetValidators();
			}
			
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
//			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
//			cmbLob.DataSource			= dtLOBs;
//			cmbLob.DataTextField		= "LOB_DESC";
//			cmbLob.DataValueField		= "LOB_ID";
//			cmbLob.DataBind();
//			cmbLob.Items.Insert(0,new ListItem("",""));
//			cmbLob.SelectedIndex=0;
//
//			#region "Loading singleton"
//			DataTable dt;
//			
//			dt = Cms.CmsWeb.ClsFetcher.State;
//			cmbState.DataSource		= dt;
//			cmbState.DataTextField	= "State_Name";
//			cmbState.DataValueField	= "State_Id";
//			cmbState.DataBind();
//			cmbState.Items.Insert(0,"");
//			cmbState.SelectedIndex=0;
//			#endregion//Loading singleton
			//Added By Raghav Itarck Issue#4646
			Cms.BusinessLayer.BlAccount.ClsAgencyStatement objAgencyStatement = new Cms.BusinessLayer.BlAccount.ClsAgencyStatement();
			DataSet objDataSet = new DataSet();

			try
			{
				objDataSet = objAgencyStatement.GetMntUsers(GetSystemId());			
				txtUsers.DataSource = objDataSet.Tables[0];					
				txtUsers.DataTextField = "FULL_NAME";
				txtUsers.DataValueField = "USERID";
				txtUsers.DataBind();
				this.txtUsers.Items.Insert(0,"All");   
				this.txtUsers.SelectedIndex =0;		
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
                objAgencyStatement = null;
				objDataSet = null;

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
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			string strAmount = txtTransactionAmount.Text.ToString().Replace(",","");
			string userId  = "";     

			for(int Listindex =1;Listindex<txtUsers.Items.Count;Listindex++)
			{
				if(txtUsers.Items[Listindex].Selected == true)
				{
					if(userId != "")
						userId += ",";
					userId += txtUsers.Items[Listindex].Value;
				}
			}

		string strValue = "<script>"
				+ "window.open('CreditCardSweepHistoryDetails.aspx?DateFromSpool=" + txtDateFromSpool.Text + "&DateToSpool=" + txtDateToSpool.Text + "&DateFromSweep=" + txtDateFromSweep.Text + "&DateToSweep=" + txtDateToSweep.Text + "&ProcessStatus=" +paypalStatusList.SelectedValue.ToString()+"&TransactionAmount=" + strAmount.ToString().Trim()+"&Users="+userId.ToString().Trim()+"');</script>";
	
			if(!ClientScript.IsClientScriptBlockRegistered("CreditCardSweepHistoryDetails"))
				ClientScript.RegisterClientScriptBlock(this.GetType(),"CreditCardSweepHistoryDetails",strValue);
		}
	}
}
