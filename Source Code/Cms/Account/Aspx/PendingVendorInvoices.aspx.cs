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

namespace Cms.Account.Aspx 
{
	
	public class PendingVendorInvoices : Cms.Account.AccountBase
	{
		#region Local Form Variables
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgPendingInvoices;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;

		public int VendorID;
		public int CheckID;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		public double TotalBalance =0;
		protected System.Web.UI.WebControls.Label lblTotalDue;
		protected System.Web.UI.WebControls.Label lblTotAmt;
		protected Cms.CmsWeb.Controls.CmsButton btnSaveClose;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		public bool flag;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowCount;
		protected int intCount=0;
		#endregion
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "";
			btnSave.CmsButtonClass			=	CmsButtonType.Execute;
			btnSave.PermissionString		=	gstrSecurityXML;
			btnClose.CmsButtonClass			=	CmsButtonType.Execute;
			btnClose.PermissionString		=	gstrSecurityXML;
			btnSaveClose.CmsButtonClass		=   CmsButtonType.Execute;
			btnSaveClose.PermissionString	=	gstrSecurityXML;

			//	btnSave.Attributes.Add("onClick","javascript:return CompareAll();");
			//btnClose.Attributes.Add("onClick","javascript:FunClose();");

			if(Request.QueryString["VENDOR_ID"] != "" && Request.QueryString["VENDOR_ID"] !=null)
				VendorID = Convert.ToInt32(Request.QueryString["VENDOR_ID"]);
			if(Request.QueryString["CHECK_ID"] != "" && Request.QueryString["CHECK_ID"] !=null)
				CheckID = Convert.ToInt32(Request.QueryString["CHECK_ID"]);

			if(!IsPostBack)
			{
				BindGrid();	
				
			}
				
		}

		#region Bind Grid
		private void BindGrid()
		{
			DataTable objTable = ClsVendorInvoices.GetPendingInvoices(VendorID,CheckID).Tables[0];
			dgPendingInvoices.DataSource =  objTable;
			dgPendingInvoices.DataBind();
			
		}
		#endregion

		#region DataGrid ItemDataBound
		private void dgPendingInvoices_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)	
			{
				HtmlInputHidden hidSELECTED = (HtmlInputHidden)e.Item.FindControl("hidSELECTED");
			/*	CheckBox chkSELECT = (CheckBox)e.Item.FindControl("chkSelect");
				if(hidSELECTED.Value != "0")
				{
					chkSELECT.Checked = true;
				}
				else
				{
					chkSELECT.Checked=false;
				}
				*/
				RegularExpressionValidator revAmt = null;
				revAmt = (RegularExpressionValidator)e.Item.FindControl("revPayAmt");
				revAmt.ValidationExpression = aRegExpCurrencyformat;
				revAmt.ErrorMessage = ClsMessages.FetchGeneralMessage("116");

				Label objlblBalAmt = null;
				objlblBalAmt = (Label)e.Item.FindControl("lblBalAmt");
                
				TextBox txtPayAmt = ((TextBox)(e.Item.FindControl("txtPayAmt")));
				txtPayAmt.Attributes.Add("onblur","javascript:FormatAmount(this);CalculateTotalBal();");
				txtPayAmt.Attributes.Add("onblur","javascript:ComparePayAndBalAmt(" + (e.Item.ItemIndex + 2).ToString() + ");");
				//changes made by uday on 20th Nov                
			//	txtPayAmt.Attributes.Add("onchange", "javascript:onAmountChange(" + (e.Item.ItemIndex + 2).ToString() + ")");  //added by uday 			
				CheckBox objCheck = (CheckBox)e.Item.FindControl("chkSelect");
				objCheck.Attributes.Add("onClick","Javascript:fillAmtOnChkChng('" + (e.Item.ItemIndex + 2) + "','" + objlblBalAmt.Text + "');CalculateTotalBal();");
				if(txtPayAmt!=null && txtPayAmt.Text!="")
					objCheck.Checked = true;	
				else
					objCheck.Checked= false;
				intCount++;
				//
			/*	if(txtPayAmt!=null)
				{
					
					
					chkSELECT.Attributes.Add("onclick", "javascript:onChange(" + (e.Item.ItemIndex + 2).ToString() + ");CalculateTotalBal();");
					
				}
				*/
			}
			hidRowCount.Value=intCount.ToString();
		}
		#endregion

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
			this.dgPendingInvoices.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPendingInvoices_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Save Invoices
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			save();
			ClientScript.RegisterStartupScript(this.GetType(),"CloseWin","<script>javascript:OnSaveFunction();</script>");
		}

		protected void save()
		{
			foreach(DataGridItem dgi in dgPendingInvoices.Items)
			{
				
				CheckBox chkSelect = null;
				TextBox objTxtBox = null;
				Label objLabel = null;
				chkSelect = (CheckBox)dgi.FindControl("chkSelect");
				if(chkSelect.Checked)
				{
					try
					{
						// Create Model object
						ClsPendingVendorInvoicesInfo objPVIInfo = new ClsPendingVendorInvoicesInfo();
						objPVIInfo.CHECK_ID				= CheckID;
						objPVIInfo.VENDOR_ID			= VendorID;
						objLabel						= (Label)dgi.FindControl("lblOPEN_ITEM_ID");
						objPVIInfo.OPEN_ITEM_ROW_ID		= Convert.ToInt32(objLabel.Text);
						objTxtBox						= (TextBox)dgi.FindControl("txtPayAmt");
						if(objTxtBox.Text!="")
							objPVIInfo.AMOUNT_TO_APPLY	= double.Parse(objTxtBox.Text);
						objPVIInfo.CREATED_BY			= int.Parse(GetUserId());
						objPVIInfo.CREATED_DATETIME		= DateTime.Now;

						HtmlInputHidden hidREF_INVOICE_ID =(HtmlInputHidden)dgi.FindControl("hidREF_INVOICE_ID");
						objPVIInfo.REF_INVOICE_ID = Convert.ToInt32(hidREF_INVOICE_ID.Value.Trim());

						Label lblInvoiceNum = (Label)dgi.FindControl("lblInvoiceNum");
						objPVIInfo.REF_INVOICE_NO = lblInvoiceNum.Text;

						Label lblRefNum = (Label)dgi.FindControl("lblRefNum");
						objPVIInfo.REF_INVOICE_REF_NO = lblRefNum.Text ;
													
						TotalBalance+= objPVIInfo.AMOUNT_TO_APPLY;
						
						if(dgPendingInvoices.DataKeys[dgi.ItemIndex]!=DBNull.Value && dgPendingInvoices.DataKeys[dgi.ItemIndex].ToString().Length>0)
						{
							// update items in vendor distribution table
							ClsVendorInvoices objVI = new ClsVendorInvoices();
							objPVIInfo.CHECK_ID				= CheckID;
							objPVIInfo.IDEN_ROW_ID = Convert.ToInt32(dgPendingInvoices.DataKeys[dgi.ItemIndex].ToString());
							int RetVal = objVI.UpdatePendingInvoices(objPVIInfo);
							if(RetVal > 0)
							{
								lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
								lblMessage.Visible=true;
							}
							else
							{
								lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
								lblMessage.Visible=true;
							}
						}
						else
						{
							// save items in vendor distribution table
							ClsVendorInvoices objVI = new ClsVendorInvoices();
							int RetVal = objVI.SavePendingInvoices(objPVIInfo);
							if(RetVal > 0)
							{
								lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
								lblMessage.Visible=true;
							}
							else
							{
								lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
								lblMessage.Visible=true;
							}
						}
					}
					catch(Exception Ex)
					{
						lblMessage.Text		= Ex.Message;
						lblMessage.Visible	= true;
						return;
					}
				}
				else
				{
					try
					{
						//Delete Invoices
						ClsPendingVendorInvoicesInfo objPVIInfoCheck = new ClsPendingVendorInvoicesInfo();
						objPVIInfoCheck.CHECK_ID				= CheckID;
						HtmlInputHidden hidSELECTED = (HtmlInputHidden)dgi.FindControl("hidSELECTED");
						if(hidSELECTED.Value!="0")
						{
							objPVIInfoCheck.IDEN_ROW_ID = int.Parse(hidSELECTED.Value.ToString());
							ClsVendorInvoices objVICheck = new ClsVendorInvoices();
							objVICheck.DeletePendingInvoices(objPVIInfoCheck);

						}
					}
					catch(Exception Ex)
					{
						lblMessage.Text		= Ex.Message;
						lblMessage.Visible	= true;
						return;

					}

					

				}
			}
			BindGrid();
		}

		private void btnSaveClose_Click(object sender, System.EventArgs e)
		{
			save();
			ClientScript.RegisterStartupScript(this.GetType(),"CloseWin","<script>javascript:FunClose();</script>");
		}
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			//save();
		   ClientScript.RegisterStartupScript(this.GetType(),"CloseWin","<script>javascript:FunClose();</script>");
		}
		#endregion


	}
}
