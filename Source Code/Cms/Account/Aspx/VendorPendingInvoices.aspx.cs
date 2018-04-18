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
	
	public class VendorPendingInvoices : Cms.Account.AccountBase
	{
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.TextBox txtFROM_TRAN_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFROM_TRAN_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_TRAN_DATE;
		protected System.Web.UI.WebControls.TextBox txtTO_TRAN_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTO_TRAN_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint;
		protected System.Web.UI.WebControls.TextBox txtFROM_DUE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFROM_DUE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_DUE_DATE;
		protected System.Web.UI.WebControls.TextBox txtTO_DUE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTO_DUE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTO_DUE_DATE;
		protected System.Web.UI.WebControls.Label capFROM_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtFROM_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtTO_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTO_AMOUNT;
		protected System.Web.UI.WebControls.Label capVENDOR_ID;
		protected System.Web.UI.WebControls.DropDownList cmbVENDOR_ID;
		protected System.Web.UI.WebControls.DataGrid dgVenPendInv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tbDataGrid;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTO_TRAN_DATE;
		#endregion

		#region Local Variables
		string FromTranDate="";
		string ToTranDate = "";
		string FromDueDate = "";
		string ToDueDate = "";
		double ToAmount = 0.0;
		double FromAmount=0.0;
		protected System.Web.UI.WebControls.CompareValidator cmpTO_TRAN_DATE;
		protected System.Web.UI.WebControls.CompareValidator cmpTO_DUE_DATE;
		protected System.Web.UI.WebControls.Label lblDatagrid;
		int VendorId;
		public static int numberDiv;
		#endregion
	
		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			//btnPrint.Attributes.Add("onclick","JavaScript:callPrint('divPrint');");
			btnPrint.Attributes.Add("onclick","JavaScript:window.print();");
			if(!Page.IsPostBack)
			{
				BindGrid(null,null,null,null,0.0,0.0,0);
				tbDataGrid.Visible=false;

				cmbVENDOR_ID.DataSource =  Cms.BusinessLayer.BlCommon.clsVendor.GetVendorNames();
				cmbVENDOR_ID.DataTextField = "COMPANY_NAME";
				cmbVENDOR_ID.DataValueField = "VENDOR_ID";
				cmbVENDOR_ID.DataBind();
				cmbVENDOR_ID.Items.Insert(0,"");
				SetErrorMessages();
			}
			#region Calendar Controls / Added Attributes
			hlkFROM_TRAN_DATE.Attributes.Add("OnClick","fPopCalendar(document.VendorPendingInvoices.txtFROM_TRAN_DATE,document.VendorPendingInvoices.txtFROM_TRAN_DATE)"); 
			hlkTO_TRAN_DATE.Attributes.Add("OnClick","fPopCalendar(document.VendorPendingInvoices.txtTO_TRAN_DATE,document.VendorPendingInvoices.txtTO_TRAN_DATE)");			
			hlkFROM_DUE_DATE.Attributes.Add("OnClick","fPopCalendar(document.VendorPendingInvoices.txtFROM_DUE_DATE,document.VendorPendingInvoices.txtFROM_DUE_DATE)"); 
			hlkTO_DUE_DATE.Attributes.Add("OnClick","fPopCalendar(document.VendorPendingInvoices.txtTO_DUE_DATE,document.VendorPendingInvoices.txtTO_DUE_DATE)");			
			txtFROM_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this);");
			txtTO_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this);");
			#endregion

			#region Button Permissions / Screen ID
			base.ScreenId = "391";
			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			btnReport.PermissionString	= gstrSecurityXML;
			btnPrint.CmsButtonClass	= CmsButtonType.Execute;
			btnPrint.PermissionString	= gstrSecurityXML;
			#endregion
			GetFormValues();
			
		
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
			revFROM_TRAN_DATE.ValidationExpression	= aRegExpDate;
			revTO_TRAN_DATE.ValidationExpression	= aRegExpDate;
			revFROM_DUE_DATE.ValidationExpression	= aRegExpDate;
			revTO_DUE_DATE.ValidationExpression		= aRegExpDate;
			revFROM_AMOUNT.ValidationExpression		= aRegExpCurrencyformat;
			revTO_AMOUNT.ValidationExpression		= aRegExpCurrencyformat;

			revFROM_TRAN_DATE.ErrorMessage			= "Please enter date in proper(MM/DD/YYYY) format.";
			revTO_TRAN_DATE.ErrorMessage			= "Please enter date in proper(MM/DD/YYYY) format.";
			revFROM_DUE_DATE.ErrorMessage			= "Please enter date in proper(MM/DD/YYYY) format.";
			revTO_DUE_DATE.ErrorMessage				= "Please enter date in proper(MM/DD/YYYY) format.";
			revFROM_AMOUNT.ErrorMessage				= "Please enter amount in numeric format";
			revTO_AMOUNT.ErrorMessage				= "Please enter amount in numeric format";			
		}
		protected void GetFormValues()
		{
			FromTranDate = txtFROM_TRAN_DATE.Text.Trim();
			ToTranDate	= txtTO_TRAN_DATE.Text.Trim();
			FromDueDate	= txtFROM_DUE_DATE.Text.Trim();
			ToDueDate	= txtTO_DUE_DATE.Text.Trim();
			if(txtFROM_AMOUNT.Text != "")
				FromAmount	= double.Parse(txtFROM_AMOUNT.Text.ToString());
			if(txtTO_AMOUNT.Text != "")
				ToAmount		= double.Parse(txtTO_AMOUNT.Text.ToString());
			if(cmbVENDOR_ID.SelectedItem !=null && cmbVENDOR_ID.SelectedIndex != 0)
				VendorId		= int.Parse(cmbVENDOR_ID.SelectedValue);
			
		}

		#endregion
		
		#region Run Report / Bindgrid / Paging / Sorting
		private void BindGrid(string FromTranDate,string ToTranDate,string FromDueDate,string ToDueDate,double FromAmount,double ToAmount,int VendorId)
		{
			try
			{
				GetFormValues();
		
				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsVendorInvoices.GetVendorPendingInvoices(FromTranDate,ToTranDate,FromDueDate,ToDueDate,FromAmount,ToAmount,VendorId);
				if(objDataSet.Tables[0].Rows.Count > 0)
				{
					dgVenPendInv.DataSource =  objDataSet.Tables[0];
					dgVenPendInv.DataBind();
					tbDataGrid.Visible=true;
					lblDatagrid.Visible = false;
					
				}
				else
				{
					lblDatagrid.Text = "No Pending Invoice exists.";
					lblDatagrid.Visible = true;
					tbDataGrid.Visible=false;
				}
				
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}
		}

		private void BindGridSort(string FromTranDate,string ToTranDate,string FromDueDate,string ToDueDate,double FromAmount,double ToAmount,int VendorId,string sortExpr)
		{
			try
			{
				GetFormValues();
		
				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsVendorInvoices.GetVendorPendingInvoices(FromTranDate,ToTranDate,FromDueDate,ToDueDate,FromAmount,ToAmount,VendorId);
				if(objDataSet.Tables[0].Rows.Count > 0)
				{
					//dgVenPendInv.DataSource =  objDataSet.Tables[0];
					//dgVenPendInv.DataBind();
					//DataView for sorting:
					DataView dvSortView = new DataView(objDataSet.Tables[0]);
					if( (numberDiv%2) == 0)
						dvSortView.Sort = sortExpr + " " + "ASC";
					else
						dvSortView.Sort = sortExpr + " " + "DESC";
					numberDiv++;
					dgVenPendInv.DataSource = dvSortView;
					dgVenPendInv.DataBind();	
					//end
					tbDataGrid.Visible=true;
					lblDatagrid.Visible = false;
					
				}
				else
				{
					lblDatagrid.Text = "No Pending Invoice exists.";
					lblDatagrid.Visible = true;
					tbDataGrid.Visible=false;
				}
				
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}
		}

		protected string FormatMoney(object amount) 
		{		
			string tempMoney = String.Format("{0:0,0.00}",amount);
			if(Convert.ToDouble(tempMoney) < 0)
			{
				tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
				if(tempMoney.StartsWith("0"))
				{
					tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
				}
				tempMoney = "-" + tempMoney;
			}
			else
			{
				if(tempMoney.StartsWith("0"))
				{
					tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
				}
			}
			return tempMoney;
		}
		protected void dgVenPendInv_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			dgVenPendInv.CurrentPageIndex = e.NewPageIndex; 
			BindGrid(FromTranDate,ToTranDate,FromDueDate,ToDueDate,FromAmount,ToAmount,VendorId);
		}

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			dgVenPendInv.CurrentPageIndex =0;
			dgVenPendInv.Visible = true;
			BindGrid(FromTranDate,ToTranDate,FromDueDate,ToDueDate,FromAmount,ToAmount,VendorId);
		}
		//Sorting
		public void Sort_Grid(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs  e)
		{
			BindGridSort(FromTranDate,ToTranDate,FromDueDate,ToDueDate,FromAmount,ToAmount,VendorId,e.SortExpression.ToString());			
		}

		#endregion
	}
}
