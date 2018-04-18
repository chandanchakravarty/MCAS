#region Namespaces
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
#endregion

namespace Cms.Account.Aspx
{	
	public class PendingAgencyStatement : Cms.Account.AccountBase
	{
		#region Page Controls Declaration

		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected System.Web.UI.WebControls.DataGrid dgVenPendInv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tbDataGrid;
		protected System.Web.UI.WebControls.Label lblDatagrid;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYear;
		protected System.Web.UI.WebControls.RangeValidator rnvYear;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYear;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		#endregion

		#region Local Variables
		int Month;
		int Year;
		string AgencyName="",CommType="";
		protected System.Web.UI.WebControls.DropDownList CmbCommType;
		int AgencyId;		
		#endregion
	
		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Button Permissions / Screen ID
			base.ScreenId = "412";
			btnReport.PermissionString	= gstrSecurityXML;
			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			#endregion

			if(!Page.IsPostBack)
			{				
				//txtYear.Text = DateTime.Now.Year.ToString();
				//cmbMonth.SelectedValue = DateTime.Now.Month.ToString();
				txtYear.Text = "";
				BindGrid(0,0,"","");
				tbDataGrid.Visible=false;
				SetErrorMessages();				
			}			
			///GetFormValues();					
		}

		#endregion
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
	
		private void InitializeComponent()
		{    
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region  Error messages / GetFormValues
		private void SetErrorMessages()
		{
			revYear.ValidationExpression = aRegExpInteger;
		}
		protected void GetFormValues()
		{	if(cmbMonth.SelectedItem !=null && cmbMonth.SelectedIndex != 0)		
			Month=int.Parse(cmbMonth.SelectedValue);
			if(txtYear.Text.ToString().Trim()!="")
			Year=int.Parse(txtYear.Text.ToString().Trim());
			if(txtAGENCY_NAME.Text.ToString().Trim()!="")
			AgencyName=txtAGENCY_NAME.Text.ToString().Trim();
			//Null check added For Itrack Issue #5426.
			if(hidAGENCY_ID.Value !=null && hidAGENCY_ID.Value!= "")            
			AgencyId=int.Parse(hidAGENCY_ID.Value);
			//if(CmbCommType.SelectedItem !=null && CmbCommType.SelectedIndex != 0)		
			CommType = CmbCommType.SelectedValue.ToString().Trim();
		}
		#endregion
		
		#region Run Report / Bindgrid / Paging
		
		private void BindGrid(int month,int year,string agencyName,string CommType)
		{
			try
			{
				//GetFormValues();
		
				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsVendorInvoices.FetchPendingAgencyStatements(month,year,agencyName,CommType);
				if(objDataSet.Tables[0].Rows.Count > 0)
				{
					dgVenPendInv.DataSource =  objDataSet.Tables[0];
					dgVenPendInv.DataBind();
					tbDataGrid.Visible=true;
					lblDatagrid.Visible = false;					
				}
				else
				{
					lblDatagrid.Text = "No Pending Agency Statements exists.";
					lblDatagrid.Visible = true;
					tbDataGrid.Visible=false;
				}
				
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}
		}

		/*private void dgVenPendInv_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				//int agencyID = int.Parse(e.Item.Cells[1].Text.ToString());
				//TextBox txtReceipt_Amt = (TextBox)e.Item.FindControl("txtReceipt_Amt");
				//txtReceipt_Amt.Attributes.Add("onblur","javascript:FormatAmount(this);");
			}
		}*/

		protected void dgVenPendInv_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			dgVenPendInv.CurrentPageIndex = e.NewPageIndex; 
			GetFormValues();	
			BindGrid(Month,Year,AgencyName,CommType);			
		}

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			dgVenPendInv.CurrentPageIndex =0;
			dgVenPendInv.Visible = true;
			GetFormValues();
			BindGrid(Month,Year,AgencyName,CommType);
		}
		#endregion
	}
}
