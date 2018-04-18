		/******************************************************************************************
<Author				: -   Ajit Singh chahal
<Start Date				: -	7/8/2005 4:16:15 PM
<End Date				: -	
<Description				: - 	Code behind for add bank reconciliation - Reconcile items.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Code behind for add bank reconciliation.
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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for ItemsToReconcile.
	/// </summary>
	public class ItemsToReconcile :Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.DataGrid grdReconcileItems;
		protected System.Web.UI.WebControls.Label lblAcc_Number;
		protected System.Web.UI.WebControls.Label lblStartBal;
		protected System.Web.UI.WebControls.Label lblEndBal;
		protected System.Web.UI.WebControls.Label lblDiff;

		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnRemove;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblBankCharges;
		protected System.Web.UI.WebControls.Label lblTotalTransactions;
		protected System.Web.UI.WebControls.Label lblCalcEndBal;
		protected System.Web.UI.WebControls.Label lblTotalDep;
		protected System.Web.UI.WebControls.Label lblTotalChecks;
		protected System.Web.UI.WebControls.Label lblTotalJE;
		protected System.Web.UI.WebControls.Label lblTotalInvoices;
		protected System.Web.UI.WebControls.Label lblTotalOther;
        protected Cms.CmsWeb.Controls.CmsButton btnAddItem;
        
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_DEPOSITS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_CHECKS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_JE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_INVOICE;
		private int iTotalChecks = 0,iTotalDeposits = 0 ,iTotalJE = 0,iTotalInvoices=0;
		private int iRowNo = 2 ;
		private bool isDeposit = false;
		private bool isCheck  = false;
		private bool isJE = false;
		private bool isInvoice = false;
		public string pathPlus	=	"/cms/cmsweb/Images/plus2.gif";
		public string pathMinus	=	"/cms/cmsweb/Images/minus2.gif";

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="205_0_0";
			btnSave.CmsButtonClass				=	CmsButtonType.Execute;
			btnSave.PermissionString			=	gstrSecurityXML;

			btnRemove.CmsButtonClass			=	CmsButtonType.Execute;
			btnRemove.PermissionString			=	gstrSecurityXML;

			btnAddItem.CmsButtonClass			=	CmsButtonType.Execute;
			btnAddItem.PermissionString			=	gstrSecurityXML;		
			
			if(!IsPostBack)
			{
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"]=="Add")
				{
					BindGrid(true);
					btnSave.Visible = true;
					btnRemove.Visible = false;
					btnAddItem.Visible = false;
				}
				else
				{
					BindGrid(false);
					btnSave.Visible = false;
					btnRemove.Visible = true;
					btnAddItem.Visible = true;
				}
			}
			
			btnAddItem.Attributes.Add("onclick","javascript: return OpenAddItemsTobeReconcilied();");
           
			
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
			this.grdReconcileItems.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdReconcileItems_ItemDataBound);
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			int intRetVal=1;
			ArrayList IDENTITY_ROW_IDs = new ArrayList();

			foreach(DataGridItem dgi in grdReconcileItems.Items)
			{
				CheckBox objCheckBox = null;
				objCheckBox = (CheckBox)dgi.FindControl("chkSelect");
				
				if(objCheckBox.Checked)
				{
					if (grdReconcileItems.DataKeys[dgi.ItemIndex].ToString() !="")
					{
						IDENTITY_ROW_IDs.Add(grdReconcileItems.DataKeys[dgi.ItemIndex].ToString());	
					}											
				}
				
            }
	
			intRetVal=ClsBankRconciliation.DeleteReconciliationItem(IDENTITY_ROW_IDs);//id of ACT_ACCOUNTS_POSTING_DETAILS
			if( intRetVal > 0 )			// delete successfully performed
			{

				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127");
				
				BindGrid(false);
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"]=="Add")
				{
					Response.Write("<script>window.opener.opener.RefreshPageWebGrid();window.opener.location=window.opener.location;</script>");
				}
				else
				{
					//Response.Write("<script>window.opener.RefreshPageWebGrid();window.opener.location=window.opener.location;</script>");
					ClientScript.RegisterStartupScript(this.GetType(),"CloseWin","<script>javascript:window.opener.RefreshPageWebGrid();refreshParent();</script>");
				}
			}
			else 
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"128");
				
			}

			lblMessage.Visible = true;
			
		}//btnRemove_Click

		private void BindGrid(bool isAddMode)
		{
			string  ACCOUNT_ID = Request.QueryString["ACCOUNT_ID"].ToString();
			string  AC_RECONCILIATION_ID = Request.QueryString["AC_RECONCILIATION_ID"].ToString();
			bool CopyRecords=false;
			if(!IsPostBack)
				CopyRecords = true;
			DataSet objDataSet = ClsBankRconciliation.GetReconcileItemsDetails(AC_RECONCILIATION_ID,ACCOUNT_ID,CopyRecords,isAddMode);
			grdReconcileItems.DataSource = objDataSet.Tables[0];
			grdReconcileItems.DataBind();

			hidTOTAL_CHECKS.Value  = iTotalChecks.ToString() ;
			hidTOTAL_DEPOSITS.Value  = iTotalDeposits.ToString();
			hidTOTAL_JE.Value 		= iTotalJE.ToString();
			hidTOTAL_INVOICE.Value		= iTotalInvoices.ToString();

			lblAcc_Number.Text = objDataSet.Tables[1].Rows[0]["ACC_DESCRIPTION"].ToString();
			lblStartBal.Text = objDataSet.Tables[1].Rows[0]["STARTING_BALANCE"].ToString();
			lblEndBal.Text = objDataSet.Tables[1].Rows[0]["ENDING_BALANCE"].ToString();
			
			if(objDataSet.Tables[1].Rows[0]["TOTAL_ITEMS"] != null)
			{
				lblTotalTransactions.Text  = objDataSet.Tables[1].Rows[0]["TOTAL_ITEMS"].ToString();
			}
			if(objDataSet.Tables[1].Rows[0]["CALCULATED_ENDING"] != null)
			{
				lblCalcEndBal.Text 		 = objDataSet.Tables[1].Rows[0]["CALCULATED_ENDING"].ToString();
			}
			if(objDataSet.Tables[1].Rows[0]["TOTAL_DEPOSITS"] != null)
			{
				lblTotalDep.Text   = objDataSet.Tables[1].Rows[0]["TOTAL_DEPOSITS"].ToString();
			}
			if(objDataSet.Tables[1].Rows[0]["TOTAL_CHECKS"] != null)
			{
				lblTotalChecks.Text  = objDataSet.Tables[1].Rows[0]["TOTAL_CHECKS"].ToString();
			}
			if(objDataSet.Tables[1].Rows[0]["TOTAL_JE"] != null)
			{
				lblTotalJE.Text  = objDataSet.Tables[1].Rows[0]["TOTAL_JE"].ToString();
			}
			if(objDataSet.Tables[1].Rows[0]["TOTAL_INVOICES"] != null)
			{
				lblTotalInvoices.Text  = objDataSet.Tables[1].Rows[0]["TOTAL_INVOICES"].ToString();
			}
			if(objDataSet.Tables[1].Rows[0]["TOTAL_OTHER"] != null)
			{
				lblTotalOther.Text  = objDataSet.Tables[1].Rows[0]["TOTAL_OTHER"].ToString();
			}


			if(objDataSet.Tables[1].Rows[0]["BANK_CHARGES"] != null)
			{
				lblBankCharges.Text  = objDataSet.Tables[1].Rows[0]["BANK_CHARGES"].ToString();
			}
			lblDiff.Text = objDataSet.Tables[1].Rows[0]["difference"].ToString();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intRetVal=1;
			ArrayList IDENTITY_ROW_IDs = new ArrayList();

			foreach(DataGridItem dgi in grdReconcileItems.Items)
			{
				//if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
			{
				CheckBox objCheckBox;
				objCheckBox = (CheckBox)dgi.FindControl("chkSelect");
				if(objCheckBox.Checked)
				{
					if (grdReconcileItems.DataKeys[dgi.ItemIndex].ToString() !="")
					{
						IDENTITY_ROW_IDs.Add(grdReconcileItems.DataKeys[dgi.ItemIndex].ToString());	
					}											
				}
			}
			}
	
			intRetVal=ClsBankRconciliation.SaveReconciliationItem(IDENTITY_ROW_IDs,Request.QueryString["AC_RECONCILIATION_ID"].ToString());//id of ACT_ACCOUNTS_POSTING_DETAILS
			if( intRetVal > 0 )			// save successfully performed
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
			
				BindGrid(true);
				//Response.Write("<script>window.opener.location=window.opener.location;</script>");
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"]=="Add")
				{
					Response.Write("<script>window.opener.opener.RefreshPageWebGrid();window.opener.location=window.opener.location;</script>");
				}
				else
				{
					Response.Write("<script>window.opener.RefreshPageWebGrid();window.opener.location=window.opener.location;</script>");
				}
			}
			else 

			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
				
			}

			lblMessage.Visible = true;
		}

		private void grdReconcileItems_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
		if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
		{
				
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				if(drvItem["ROW_LEVEL"] != null && drvItem["ROW_LEVEL"].ToString() == "H")
				{
					iRowNo = 2;
					if(drvItem["UPDATED_FROM"] != null & drvItem["UPDATED_FROM"].ToString() =="C")
					{
						isCheck = true;
						isDeposit = false;
						isJE = false;
						isInvoice = false;
						e.Item.ID = "_trCheckHead";
						e.Item.Attributes.Add("name","_trCheckHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideChecks()");
					}
					else if(drvItem["UPDATED_FROM"] != null & drvItem["UPDATED_FROM"].ToString() =="D")
					{
						isCheck = false;
						isDeposit = true;
						isJE = false;
						isInvoice = false;
						e.Item.ID = "_trDepositHead";
						e.Item.Attributes.Add("name","_trDepositHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideDeposits()");
					}
					else if(drvItem["UPDATED_FROM"] != null & drvItem["UPDATED_FROM"].ToString() =="J")
					{
						isCheck = false;
						isDeposit = false;
						isJE = true;
						isInvoice = false;
						e.Item.ID = "_trJEHead";
						e.Item.Attributes.Add("name","_trJEHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideJEs()");
					}
					else if(drvItem["UPDATED_FROM"] != null & drvItem["UPDATED_FROM"].ToString() =="I")
					{
						isCheck = false;
						isDeposit = false;
						isJE = false;
						isInvoice = true;
						e.Item.ID = "_trINVOICEHead";
						e.Item.Attributes.Add("name","_trINVOICEHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideInvoices()");
					}
				
					e.Item.BackColor= System.Drawing.Color.Red;
					
					//Will Have a CSS Class for this 
					Label lblDesc = (Label)(e.Item.FindControl("lblTransactionType"));
					lblDesc.ForeColor =   System.Drawing.Color.Red;
					lblDesc.Font.Bold = true;
					
					Label lblSrc = (Label)(e.Item.FindControl("lblSOURCE_NUM"));
					lblSrc.Text = "";
						
					//Will Have a CSS Class for this
					Label lblTotal = (Label)(e.Item.FindControl("lblTRANSACTION_AMOUNT"));
					lblTotal.Text = "Total Amount: " + lblTotal.Text.ToString();
					lblTotal.Font.Bold = true;
					lblTotal.ForeColor =   System.Drawing.Color.Red;

					//Handel Expand Collapse
					CheckBox chkSelect = (CheckBox)e.Item.FindControl("chkSelect");
					chkSelect.Visible = false;
					System.Web.UI.WebControls.Image  img = (System.Web.UI.WebControls.Image)(e.Item.FindControl("imgCollaspe"));
					img.Visible = true;
					
					
					
				}
				else
				{
					if(isCheck)
					{
						e.Item.Attributes.Add("id","grdReconcileItems__trCheckDetail" + iRowNo.ToString());
						iTotalChecks++;					
					}
					if(isDeposit)
					{
						
						e.Item.Attributes.Add("id","grdReconcileItems__trDepositDetail" + iRowNo.ToString());
						iTotalDeposits++;
						
					}
					if(isJE)
					{
						e.Item.Attributes.Add("id","grdReconcileItems__trJEDetail" + iRowNo.ToString());
						iTotalJE++;

					}
					if(isInvoice)
					{
						
						e.Item.Attributes.Add("id","grdReconcileItems__trInvoiceDetail" + iRowNo.ToString());
						iTotalInvoices++;
					
					}
					
					HtmlInputHidden hidIS_ALREADY_CLEARED =(HtmlInputHidden) e.Item.FindControl("hidIS_ALREADY_CLEARED");
					CheckBox chkSelect = (CheckBox)e.Item.FindControl("chkSelect");
				
					int id = e.Item.ItemIndex + 2;

					chkSelect.Attributes.Add("Onclick","javascript:CheckBoxClicked(this);");

					if(hidIS_ALREADY_CLEARED.Value.Trim() == "1")
						chkSelect.Enabled=false;

					/*if(drvItem["PAYMENT_MODE"] !=null && drvItem["PAYMENT_MODE"].ToString().ToUpper()  =="CREDIT CARD")
					{
						chkSelect.Enabled = false;  	
					}*/

					chkSelect.Enabled = true;

					iRowNo++;				
				}
				
				

			}
		}
	}
}
