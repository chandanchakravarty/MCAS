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
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Account;
using System.Configuration;
using System.Xml;
using Cms.CmsWeb.Utils;
using Cms.ExceptionPublisher.ExceptionManagement; 
namespace Account.Aspx
{
	/// <summary>
	/// Summary description for PaymentReversal.
	/// </summary>
	public class PaymentReversal : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.WebControls.DataGrid grdPaymentReversal;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VER_ID;
		protected System.Web.UI.WebControls.CheckBox chkSWEEP;
		string url="";
		public static int numberDiv;
		public static int RecordCount;
		string ReversableRow="";
//		CheckBox chkSWEEP;
		public static int flag;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId = "436";
			RecordCount=1;
			SetPermissions();
			SetErrorMessages();
			url = ClsCommon.GetLookupURL();	
			imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','POLICY_ID','POLICY_NUMBER','hidPolicyID','txtPOLICY_ID','PolicyForFeeRev','Policy','','');");
			lblMessage.Visible = false;
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.grdPaymentReversal.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdPaymentReversal_ItemDataBound);
			this.grdPaymentReversal.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdPaymentReversal_ItemCommand);
		}
		#endregion

		private void SetPermissions()
		{
			btnSave.CmsButtonClass		= CmsButtonType.Read;
			btnSave.PermissionString	= gstrSecurityXML;
		}


		private void SetErrorMessages()
		{
			revPOLICY_ID.ValidationExpression = aRegExpAlphaNum;
			revPOLICY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102");
		}
	

		private void BindGrid(string policyNo)
		{
			//lblMessage.Visible=false;
			try
			{
				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsDeposit.GetPaymentDetail(policyNo);
				
				if(objDataSet.Tables.Count>1 && objDataSet.Tables[1] != null && objDataSet.Tables[1].Rows[0]["IDEN_ROW_ID"] != DBNull.Value)
				{
					ReversableRow= objDataSet.Tables[1].Rows[0]["IDEN_ROW_ID"].ToString();
				}

				else
				{

				}
				grdPaymentReversal.DataSource =  objDataSet.Tables[0];
				grdPaymentReversal.DataBind();	
				
			}
			catch (Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"8") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}

			
		}
		/// <summary>
		/// Sorting Coilumns
		/// </summary>
		/// <param name="policyNo"></param>
		/// <param name="feeType"></param>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		private void BindGridSort(string policyNo,string sortExpr)
		{
			lblMessage.Visible=false;
			try
			{
				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsDeposit.GetPaymentDetail(policyNo);
				//DataView for sorting:
				DataView dvSortView = new DataView(objDataSet.Tables[0]);
				
				if( (numberDiv%2) == 0)
					dvSortView.Sort = sortExpr + " " + "ASC";
				else
					dvSortView.Sort = sortExpr + " " + "DESC";
				numberDiv++;
				grdPaymentReversal.DataSource = dvSortView;
				grdPaymentReversal.DataBind();	
			
			}
			catch (Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"8") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}

			
		}
		protected void grdPaymentReversal_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			lblMessage.Visible=false;
			grdPaymentReversal.CurrentPageIndex = e.NewPageIndex; 
			BindGrid(txtPOLICY_ID.Text.ToString());			
		}

		public void Sort_Grid(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs  e)
		{
			BindGridSort(txtPOLICY_ID.Text.ToString(),e.SortExpression.ToString());			
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			grdPaymentReversal.CurrentPageIndex =0;
			BindGrid(txtPOLICY_ID.Text.ToString());	
		}


		private void grdPaymentReversal_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
				{
					HtmlInputHidden hidIDEN_ROW_ID = (HtmlInputHidden)e.Item.FindControl("hidIDEN_ROW_ID");
					HtmlInputHidden hidPOLICY_ID = (HtmlInputHidden)e.Item.FindControl("hidPOLICY_ID");
					string POLICY_ID = hidPOLICY_ID.Value.ToString();

					Button btnReverseAmt = (Button)e.Item.FindControl("btnReverseAmt");

					HtmlInputHidden hidRECEIPT_MODE = (HtmlInputHidden)e.Item.FindControl("hidRECEIPT_MODE");
					chkSWEEP = (CheckBox)e.Item.FindControl("chkSWEEP");


					if(hidIDEN_ROW_ID.Value.Trim() == ReversableRow)
					{
						btnReverseAmt.Visible = true;
						if(Convert.ToInt32( hidRECEIPT_MODE.Value.Trim()) == (int)PaymentModes.CreditCard
							|| Convert.ToInt32( hidRECEIPT_MODE.Value.Trim()) == (int)PaymentModes.EFT )
						{
							chkSWEEP.Enabled = true; 
							//chkSWEEP.Checked = true;
						}
						else
						{
							chkSWEEP.Enabled = false; 
							//chkSWEEP.Checked = false;
						}
					}
					else 
					{
						btnReverseAmt.Visible = false;
						chkSWEEP.Enabled = false; 
						//chkSWEEP.Checked = false;
					}

				}				
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
		}
		

		private void grdPaymentReversal_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Equals("ReverseAmount"))
				{
					ClsDeposit objPaymentReversal	=	new ClsDeposit();
					int retVal = 0;

					HtmlInputHidden hidCUSTOMER_ID = (HtmlInputHidden)e.Item.FindControl("hidCUSTOMER_ID");
					int customerID = int.Parse(hidCUSTOMER_ID.Value.ToString());
					
					HtmlInputHidden hidCD_LINE_ITEM_ID = (HtmlInputHidden)e.Item.FindControl("hidCD_LINE_ITEM_ID");
					int lineITEM_ID = int.Parse(hidCD_LINE_ITEM_ID.Value.ToString());
					
					HtmlInputHidden hidPOLICY_ID = (HtmlInputHidden)e.Item.FindControl("hidPOLICY_ID");
					int PolicyId = int.Parse(hidPOLICY_ID.Value.ToString());

					HtmlInputHidden hidPOLICY_VER_ID = (HtmlInputHidden)e.Item.FindControl("hidPOLICY_VER_ID");
					int PolicyVerId = int.Parse(hidPOLICY_VER_ID.Value.ToString());									
					
					HtmlInputHidden hidCREATED_BY = (HtmlInputHidden)e.Item.FindControl("hidCREATED_BY");					
					int CreatedBy = int.Parse(GetUserId());

					HtmlInputHidden hidDEPOSIT_NO = (HtmlInputHidden)e.Item.FindControl("hidDEPOSIT_NO");
					int DepositNo = int.Parse(hidDEPOSIT_NO.Value.ToString());

					HtmlInputHidden hidRECEIPT_AMOUNT = (HtmlInputHidden)e.Item.FindControl("hidRECEIPT_AMOUNT");
					double RecieptAmount = Convert.ToDouble(hidRECEIPT_AMOUNT.Value.ToString());

					HtmlInputHidden hidREVERSAL_DATE = (HtmlInputHidden)e.Item.FindControl("hidREVERSAL_DATE");
				
					int user_id = int.Parse(GetUserId());
					
					//ClsDepartment objDeposit = new ClsDeposit();
					chkSWEEP = (CheckBox)e.Item.FindControl("chkSWEEP");

					bool flag = chkSWEEP.Checked;

                    string PayPalUserName = ConfigurationManager.AppSettings.Get("PayPalUserName");
                    string PayPalVendorName = ConfigurationManager.AppSettings.Get("PayPalVendor");
                    string PayPalHostName = ConfigurationManager.AppSettings.Get("HostName");
                    string PayPalPartnerName = ConfigurationManager.AppSettings.Get("PaypalPartner");
                    string PayPalPassword = ConfigurationManager.AppSettings.Get("PayPalPassword");
								
					retVal = objPaymentReversal.ReversePayment(txtPOLICY_ID.Text.ToString().Trim(),customerID,
						lineITEM_ID,PolicyId,PolicyVerId,CreatedBy,DepositNo,RecieptAmount,user_id,flag,
						PayPalUserName,PayPalVendorName,PayPalHostName,PayPalPartnerName,PayPalPassword);

					if(retVal == 1)
					{
						lblMessage.Text= "Payment Reversed Successfully";
						lblMessage.Visible = true;
						Button btnReverseAmt = (Button)e.Item.FindControl("btnReverseAmt");
						btnReverseAmt.Visible = false;
						BindGrid(txtPOLICY_ID.Text.ToString());	
					}
					else if(retVal == -1 )
					{
						lblMessage.Text = "Unable to Reverse the Payment, error while reversing transaction with Credit Card Processor. Please review Credit Card Sweep history for details.";
						lblMessage.Visible=true;
					}
					else
					{
                        lblMessage.Text = "Unable to Reverse Payment this time, Please try again later.";
						lblMessage.Visible=true;
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text= ex.Message.ToString();
				lblMessage.Visible = true;
			}	
		}
		

	}
}
