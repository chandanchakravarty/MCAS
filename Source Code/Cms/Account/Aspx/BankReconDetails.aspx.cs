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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for BankReconDetails.
	/// </summary>
	public class BankReconDetails : AccountBase 
	{
		protected System.Web.UI.WebControls.DataGrid dgRecondetails;
	
		private int Account_ID,Recon_ID ;
		private string Called_From;
		private int iTotalChecks = 0,iTotalDeposits = 0 ,iTotalJE = 0,iTotalOthers = 0;
		private int iRowNo = 1 ;
		private bool isDeposit = false;
		private bool isCheck  = false;
		private bool isJE = false;
		private bool isOther = false;
		public string pathPlus	=	"/cms/cmsweb/Images/plus2.gif";
		public string pathMinus	=	"/cms/cmsweb/Images/minus2.gif";
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_DEPOSITS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_CHECKS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_OTHER;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint;
		protected System.Web.UI.WebControls.Label lblAcc_Number;
		protected System.Web.UI.WebControls.Label lblGLBalance;
		protected System.Web.UI.WebControls.Label  lblStatementDate;    
		protected System.Web.UI.WebControls.Label lblOutStandings;
		protected System.Web.UI.WebControls.Label lblBankCharges;
		protected System.Web.UI.WebControls.Label lblCalcBalance;
		protected System.Web.UI.WebControls.Label lblEndingBalance;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOTAL_JE; void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="205_0_1";
             btnPrint.CmsButtonClass =CmsButtonType.Execute; 
             btnPrint.PermissionString = gstrSecurityXML;
			btnPrint.Attributes.Add("onclick","javascript:window.print();"); 

			if(Request.QueryString["ACCOUNT_ID"] != null && Request.QueryString["ACCOUNT_ID"].Trim() != "")
			{
				Account_ID = Convert.ToInt32(Request.QueryString["ACCOUNT_ID"]);
			}
			if(Request.QueryString["RECON_ID"] != null && Request.QueryString["RECON_ID"].Trim() != "")
			{
				Recon_ID= Convert.ToInt32(Request.QueryString["RECON_ID"]);
			}
			if(Request.QueryString["CALLED_FROM"] != null && Request.QueryString["CALLED_FROM"].Trim() != "")
			{
				Called_From  = Request.QueryString["CALLED_FROM"].Trim();
			}
			BindGrid();
		}

		private void BindGrid()
		{
			DataSet objDataSet = ClsBankRconciliation.GetReconDetails(Account_ID,Recon_ID,Called_From);
			dgRecondetails.DataSource = objDataSet.Tables[0];
			dgRecondetails.DataBind();
			hidTOTAL_CHECKS.Value  = iTotalChecks.ToString() ;
			hidTOTAL_DEPOSITS.Value  = iTotalDeposits.ToString();
			hidTOTAL_JE.Value 		= iTotalJE.ToString();
			hidTOTAL_OTHER.Value = iTotalOthers.ToString();

			if(objDataSet.Tables.Count >=2 )
			{

				if(objDataSet.Tables[1].Rows[0]["ACC_DESCRIPTION"] != null && 
					objDataSet.Tables[1].Rows[0]["ACC_DESCRIPTION"].ToString().Trim() != "")
				{
					lblAcc_Number.Text = objDataSet.Tables[1].Rows[0]["ACC_DESCRIPTION"].ToString();
				}


				if(objDataSet.Tables[1].Rows[0]["GL_BALANCE"] != null && 
					objDataSet.Tables[1].Rows[0]["GL_BALANCE"].ToString().Trim() != "")
				{
					lblGLBalance.Text = objDataSet.Tables[1].Rows[0]["GL_BALANCE"].ToString();
				}
				if(objDataSet.Tables[1].Rows[0]["BANK_CHARGES"] != null && 
					objDataSet.Tables[1].Rows[0]["BANK_CHARGES"].ToString().Trim() != "")
				{
					lblBankCharges.Text = objDataSet.Tables[1].Rows[0]["BANK_CHARGES"].ToString();
				}
				if(objDataSet.Tables[1].Rows[0]["TOTAL_OUTSTANDNIG"] != null && 
					objDataSet.Tables[1].Rows[0]["TOTAL_OUTSTANDNIG"].ToString().Trim() != "")
				{
					lblOutStandings.Text = objDataSet.Tables[1].Rows[0]["TOTAL_OUTSTANDNIG"].ToString();
				}
				if(objDataSet.Tables[1].Rows[0]["CALC_BALANCE"] != null && 
					objDataSet.Tables[1].Rows[0]["CALC_BALANCE"].ToString().Trim() != "")
				{
					lblCalcBalance.Text = objDataSet.Tables[1].Rows[0]["CALC_BALANCE"].ToString();
				}
				if(objDataSet.Tables[1].Rows[0]["ENDING_BALANCE"] != null && 
					objDataSet.Tables[1].Rows[0]["ENDING_BALANCE"].ToString().Trim() != "")
				{
					lblEndingBalance.Text = objDataSet.Tables[1].Rows[0]["ENDING_BALANCE"].ToString();
				}
				//Statement_Date Added For Itrack Issue #5921.
				if(objDataSet.Tables[1].Rows[0]["STATEMENT_DATE"] !=null )
				{
				   lblStatementDate.Text = objDataSet.Tables[1].Rows[0]["STATEMENT_DATE"].ToString();   
				}
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
			this.dgRecondetails.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgRecondetails_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgRecondetails_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				if(drvItem["ROW_LEVEL"] != null && drvItem["ROW_LEVEL"].ToString() == "H")
				{
					iRowNo = 1;
					if(drvItem["UPDATED_FROM"] != null & drvItem["UPDATED_FROM"].ToString() =="C")
					{
						isCheck = true;
						isDeposit = false;
						isJE = false;
						isOther = false;
						e.Item.ID = "trCheckHead";
						e.Item.Attributes.Add("name","trCheckHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideChecks()");
					}
					else if(drvItem["UPDATED_FROM"] != null & drvItem["UPDATED_FROM"].ToString() =="D")
					{
						isCheck = false;
						isDeposit = true;
						isJE = false;
						isOther = false;
						e.Item.ID = "trDepositHead";
						e.Item.Attributes.Add("name","trDepositHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideDeposits()");
					}
					else if(drvItem["UPDATED_FROM"] != null & drvItem["UPDATED_FROM"].ToString() =="J")
					{
						isCheck = false;
						isDeposit = false;
						isJE = true;
						isOther = false;
						e.Item.ID = "trJEHead";
						e.Item.Attributes.Add("name","trJEHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideJEs()");
					}
					else
					{
						isOther = true;
						isCheck = false;
						isDeposit = false;
						isJE = false;
						e.Item.ID = "trOtherHead";
						e.Item.Attributes.Add("name","trOtherHead");
						e.Item.Attributes.Add("onClick","javascript:ShowHideOthers()");
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
					System.Web.UI.WebControls.Image  img = (System.Web.UI.WebControls.Image)(e.Item.FindControl("imgCollaspe"));
					img.Visible = true;
					
				}
				else
				{
					if(isCheck)
					{
						e.Item.ID = "trCheckDetail" + iRowNo.ToString();
						e.Item.Attributes.Add("name","trCheckDetail" + iRowNo.ToString());
						iTotalChecks++;
					}
					if(isDeposit)
					{
						e.Item.ID = "trDepositDetail" + iRowNo.ToString();
						e.Item.Attributes.Add("name","trDepositDetail" + iRowNo.ToString());
						iTotalDeposits++;
					}
					if(isJE)
					{
						e.Item.ID = "trJEDetail" + iRowNo.ToString();
						e.Item.Attributes.Add("name","trJEDetail" + iRowNo.ToString());
						iTotalJE++;

					}
					if(isOther)
					{
						e.Item.ID = "trOtherDetail" + iRowNo.ToString();
						e.Item.Attributes.Add("name","trOtherDetail" + iRowNo.ToString());
						iTotalOthers++;
					}
					iRowNo++;				
				}
			}
		}
	}
}
