/******************************************************************************************
<Author					: -   Raghav Gupta
<Start Date				: -	  07/1/2008 3:00:00 PM
<End Date				: -	
<Description			: -   Report of Bank Reconciliation.
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
using Cms.CmsWeb.Controls;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;


namespace Account
{
	/// <summary>
	/// Summary description for BankReconciliationReport.
	/// </summary>
	public class BankReconciliationReport :  Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.DataGrid dgReconcheck;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFILE_ID;
		protected System.Web.UI.WebControls.Label lblerr;
		protected System.Web.UI.WebControls.DropDownList cmb_MatchRecordStatus;  
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				dgReconcheck.PageSize = int.Parse(GetPageSize());
				BindGrid(int.Parse(cmb_MatchRecordStatus.SelectedValue));
			}
		}

		public void BindGrid(int intMATCHED_RECORD_STATUS)
		{
		
			try
			{

				if(Request.QueryString["FILE_ID"]!=null && Request.QueryString["FILE_ID"].ToString()!="")
				{
					int intFileID = int.Parse(Request.QueryString["FILE_ID"].ToString());
					
					DataSet Ds =null;
					Ds = ClsBankRconciliation.BankreconciliationReport(intFileID.ToString(),intMATCHED_RECORD_STATUS);					
					if(Ds!=null)
					{
						if(Ds.Tables[0].Rows.Count > 0)
						{
							dgReconcheck.DataSource = Ds;
							dgReconcheck.DataBind();
                            lblerr.Text = "";
						}
						else
						{
							//TO MOVE TO LOCAL VSS
							lblerr.Text ="No Matching records found.";		
							dgReconcheck.DataSource = Ds;
							dgReconcheck.DataBind();
						}
					}				
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		protected void dgReconcheck_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			dgReconcheck.CurrentPageIndex = e.NewPageIndex; 
			BindGrid(int.Parse(cmb_MatchRecordStatus.SelectedValue)); 
		}

		private void cmb_MatchRecordStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmb_MatchRecordStatus.SelectedValue != null && cmb_MatchRecordStatus.SelectedValue.ToString()!="")
			{ 
				dgReconcheck.CurrentPageIndex  =0; //Set to Page 1
				if(cmb_MatchRecordStatus.SelectedValue == "-1")
					BindGrid(-1);  
				else
					BindGrid(int.Parse(cmb_MatchRecordStatus.SelectedValue)); 

				
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
			//this.  += new DataGridPageChangedEventArgs(this.dgReconcheck_Paging);     
			this.Load += new System.EventHandler(this.Page_Load);
			this.cmb_MatchRecordStatus.SelectedIndexChanged  += new System.EventHandler(this.cmb_MatchRecordStatus_SelectedIndexChanged);    
			//this.cmbFISCAL_ID.SelectedIndexChanged += new System.EventHandler(this.cmbFISCAL_ID_SelectedIndexChanged);  
		
		}
		#endregion
	}
}
