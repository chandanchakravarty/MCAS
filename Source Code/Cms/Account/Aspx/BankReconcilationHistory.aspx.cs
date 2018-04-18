/******************************************************************************************
<Author					: -   Raghav Gupta
<Start Date				: -	  07/1/2008 3:00:00 PM
<End Date				: -	
<Description			: -    Bank Reconciliation.
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
using System.Text;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlAccount;



namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AccountDetails.
	/// </summary>
	public class BankReconcilationHistory : Cms.Account.AccountBase
	{	
		protected System.Web.UI.WebControls.DropDownList cmbBank_Account;
		protected System.Web.UI.WebControls.DropDownList cmb_Year;  
		protected System.Web.UI.WebControls.DataGrid dgRecCheck;		
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;		
		protected System.Web.UI.WebControls.HyperLink hlkViewdetails;
		public string strCalledFrom="";
		private void Page_Load(object sender, System.EventArgs e)
		{			
			if(!IsPostBack)
			{
				cmbBank_Account.AutoPostBack = true ; 
				cmb_Year.AutoPostBack = true ;
				ClsGlAccounts.GetCashAccountsInDropDown(cmbBank_Account);//General
				
				int currYear = System.DateTime.Now.Year;
				int prevYear =currYear -1;
				int prYear  =prevYear-1;
				cmb_Year.Items.Add(new ListItem(currYear.ToString(),currYear.ToString()));
				cmb_Year.Items.Add(new ListItem(prevYear.ToString(),prevYear.ToString()));
				cmb_Year.Items.Add(new ListItem(prYear.ToString(),prYear.ToString()));
				base.ScreenId = "430";	   
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
			this.cmbBank_Account.SelectedIndexChanged += new System.EventHandler(this.cmbBank_Account_SelectedIndexChanged);
			this.cmb_Year.SelectedIndexChanged += new System.EventHandler(this.cmb_Year_SelectedIndexChanged );  
			this.dgRecCheck.ItemDataBound +=new DataGridItemEventHandler(this.dgRecCheck_ItemDataBound); 
			this.Load += new System.EventHandler(this.Page_Load);
			
            
		}
		#endregion		
	
		private void cmbBank_Account_SelectedIndexChanged(object sender, EventArgs e)
		{			
			BindGrid();
		}		
		private void cmb_Year_SelectedIndexChanged(object sender, EventArgs e)
		{						
			BindGrid();
		}			
								
		private void BindGrid()			
		{
			try
			{
				DataSet Ds =null;
				string accId =  cmbBank_Account.SelectedValue; 
				string year =     cmb_Year.SelectedValue; 			
				Ds = ClsBankRconciliation.BankReconciliationInput(accId,year);				
				dgRecCheck.DataSource = Ds;
				dgRecCheck.DataBind();					
			}
			catch(Exception ex)
			{ 
				throw (ex); 			  
			  }			
		}

		private void dgRecCheck_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{		  
				int Fileid = int.Parse(e.Item.Cells[3].Text.ToString().Trim());
				HyperLink hlnkView = ((HyperLink)e.Item.FindControl("hlnkView"));				
				string path = "'BankReconciliationReport.aspx?FILE_ID="+Fileid + "'";
				hlnkView.Attributes.Add("OnClick","javascript:ShowBankReconDetails(" + path + ");");
			}	
		}
    }
}
