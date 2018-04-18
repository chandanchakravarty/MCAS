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
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Ajax;
using System.Data.SqlClient;
using System.Reflection;
using System.Resources;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for DepositAgency.
	/// </summary>
	public class DepositAgency : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;		
		protected System.Web.UI.WebControls.Label capAGENCY_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_NAME;
		protected System.Web.UI.WebControls.Label capMONTH;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_MONTH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMONTH;
		protected System.Web.UI.WebControls.Label capRECEIPT_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtRECEIPT_AMOUNT;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCD_LINE_ITEM_ID;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		protected System.Web.UI.WebControls.TextBox txtTest;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR;
		private ClsDepositDetails objDepositDetails;
		protected Cms.WebControls.AjaxLookup txtAGENCY_NAME;
		protected Cms.CmsWeb.Controls.CmsButton btnFind;
		protected System.Web.UI.WebControls.DataGrid dgAgencyDeposit;
		protected Cms.CmsWeb.Controls.CmsButton btn;
		protected System.Web.UI.WebControls.CheckBox chSelectAll;
		protected System.Web.UI.WebControls.RegularExpressionValidator revReceipt_Amt;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidYear;		
		protected Cms.CmsWeb.Controls.CmsButton btnReconcile;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECEIPT_AMOUNT;
		protected System.Web.UI.WebControls.Label lblDepositNum;
		protected System.Web.UI.WebControls.Label lblDEPOSIT_NUM;
        protected System.Web.UI.WebControls.Label capMandatory;
		protected string strDEPOSIT_TYPE ;
        System.Resources.ResourceManager objResourceMgr;
		
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "187_2";
			SetButtonsSecurityXML();
            capMandatory.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.DepositAgency", System.Reflection.Assembly.GetExecutingAssembly());
			strDEPOSIT_TYPE = Request.QueryString["DEPOSIT_TYPE"].ToString();
			
			if (! Page.IsPostBack)
			{
                SetCaption();
                SetValidators();		//Setting the validation controls properties
				GetQueryString();		//Fetching the query string from query string
				Binding(int.Parse(hidDEPOSIT_ID.Value));
				txtRECEIPT_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this);");
				btnSave.Attributes.Add("OnClick","javascript:return Select();");
				btnDelete.Attributes.Add("OnClick","javascript:return Select();");
				txtYEAR.Text = DateTime.Now.Year.ToString();
				cmbPOLICY_MONTH.SelectedValue = DateTime.Now.Month.ToString();
				btnReset.Attributes.Add("onClick","javascript:return Reset();");
				//GetOldXml();			//Making the XML of old records
			}
			//Ajax.Utility.RegisterTypeForAjax(typeof (DepositAgency));
			Ajax.Utility.RegisterTypeForAjax(typeof (AccountBase));
			
			
			
		}


		[AjaxMethod()]
		public string GetSearchItems1(string query)
		{

			DataSet ds = Cms.BusinessLayer.BlCommon.ClsCommon.ExecuteDataSet(
				"select agency_id, agency_display_name from mnt_agency_list where agency_display_name like '" 
				+ query + "%' order by agency_display_name");

			string xml = ds.GetXml();
			return xml;
		}


		/// <summary>
		/// Sets the validation properties of validation controls
		/// </summary>
		private void SetValidators()
		{
			rfvAGENCY_NAME.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "65");
			revRECEIPT_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("965");
			//revRECEIPT_AMOUNT.ValidationExpression = aRegExpDoublePositiveNonZeroStartWithZero;
			//revRECEIPT_AMOUNT.ValidationExpression = aRegExpDoublePositiveStartWithDecimal;			
			revRECEIPT_AMOUNT.ValidationExpression =  aRegExpCurrencyformat;
			rfvMONTH.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "477");
			rngYEAR.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngYEAR.MinimumValue = aAppMinYear  ;
			rngYEAR.Type = ValidationDataType.Integer;
			rngYEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"736");
			
		}
        private void SetCaption()
        {
            lblDepositNum.Text = objResourceMgr.GetString("lblDepositNum");
            capAGENCY_NAME.Text = objResourceMgr.GetString("capAGENCY_NAME");
            capMONTH.Text = objResourceMgr.GetString("capMONTH");
            capRECEIPT_AMOUNT.Text = objResourceMgr.GetString("capRECEIPT_AMOUNT");
            btnDelete.Text = objResourceMgr.GetString("btnDelete");
            btnFind.Text = objResourceMgr.GetString("btnFind");
            btnSave.Text = objResourceMgr.GetString("btnSave");
            btnReset.Text = objResourceMgr.GetString("btnReset");
            chSelectAll.Text = objResourceMgr.GetString("chSelectAll");
        }

		/// <summary>
		/// Sets the validatiors properties used in this page
		/// </summary>
		/// <param name="e">Data grid items</param>
		private void SetValidators(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revReceipt_Amt");
			revValidator.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "965");
			//revValidator.ValidationExpression = aRegExpDoublePositiveNonZeroStartWithZero;
			//revValidator.ValidationExpression = aRegExpDoublePositiveStartWithDecimal;
			revValidator.ValidationExpression = aRegExpCurrencyformat;
		}
		


		/// <summary>
		/// Sets the PermissionString property of buttons to gstrSecurityXML global const.
		/// </summary>
		private void SetButtonsSecurityXML()
		{
			btnSave.PermissionString = gstrSecurityXML;
			btnSave.CmsButtonClass = CmsButtonType.Write;

			btnReset.PermissionString = gstrSecurityXML;
			btnReset.CmsButtonClass = CmsButtonType.Execute;

			btnDelete.PermissionString = gstrSecurityXML;
			btnDelete.CmsButtonClass = CmsButtonType.Delete;

			btnReconcile.PermissionString = gstrSecurityXML;
			btnReconcile.CmsButtonClass = CmsButtonType.Read;

			btnFind.PermissionString = gstrSecurityXML;
			btnFind.CmsButtonClass = CmsButtonType.Write;

		}

		/// <summary>
		/// Gets the query string values from url into hidden fields
		/// </summary>
		private void GetQueryString()
		{
			hidDEPOSIT_ID.Value = Request.Params["DEPOSIT_ID"];
			lblDEPOSIT_NUM.Text		= Request.QueryString["DEPOSIT_NUM"].ToString();
		}

		/// <summary>
		/// Gets the old xml of the current depoit and saves to the hidden variable
		/// </summary>
		private void GetOldXml()
		{
			try
			{
				objDepositDetails = new ClsDepositDetails();
				hidOldData.Value = objDepositDetails.GetXml(int.Parse(hidDEPOSIT_ID.Value));
				//txtAGENCY_NAME.va
				objDepositDetails.Dispose();
			}
			catch(Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

		/// <summary>
		/// Populates the ClDepositDetailsInfo class object and returns it
		/// </summary>
		/// <returns>ClsDepositDetailsInfo model class object</returns>
		private ClsDepositDetailsInfo GetFormValue()
		{
			ClsDepositDetailsInfo objDepositDetailsInfo = new ClsDepositDetailsInfo();

			objDepositDetailsInfo.DEPOSIT_ID = int.Parse(hidDEPOSIT_ID.Value);

			if (txtAGENCY_NAME.DataValue != "")
				objDepositDetailsInfo.RECEIPT_FROM_ID = int.Parse(txtAGENCY_NAME.DataValue);

			objDepositDetailsInfo.RECEIPT_AMOUNT = double.Parse(txtRECEIPT_AMOUNT.Text);
			objDepositDetailsInfo.RECEIPT_FROM_NAME = txtAGENCY_NAME.Text;
			objDepositDetailsInfo.MONTH_YEAR = int.Parse(txtYEAR.Text);

			if (hidCD_LINE_ITEM_ID.Value != "")
				objDepositDetailsInfo.CD_LINE_ITEM_ID = int.Parse(hidCD_LINE_ITEM_ID.Value);

			if (cmbPOLICY_MONTH.SelectedValue != "")
				objDepositDetailsInfo.POLICY_MONTH = int.Parse(cmbPOLICY_MONTH.SelectedValue);

			return objDepositDetailsInfo;
		}
		//Added PK 21 Nov 2006
		private ClsDepositDetailsInfo GetFormValuesFromGrid(int depositID,string strTxtRecAmt,int receiptFromID,string agencyName,int agencyYear,int agencyMonth,int cdLineItemID)
		{
			ClsDepositDetailsInfo objDepositDetailsInfo = new ClsDepositDetailsInfo();

			objDepositDetailsInfo.DEPOSIT_ID = depositID;
			objDepositDetailsInfo.RECEIPT_FROM_ID = receiptFromID;

			objDepositDetailsInfo.RECEIPT_AMOUNT = double.Parse(strTxtRecAmt);
			objDepositDetailsInfo.RECEIPT_FROM_NAME = agencyName;
			objDepositDetailsInfo.MONTH_YEAR = agencyYear;

			objDepositDetailsInfo.CD_LINE_ITEM_ID = cdLineItemID;

			if (cmbPOLICY_MONTH.SelectedValue != "")
				objDepositDetailsInfo.POLICY_MONTH = agencyMonth;

			
			return objDepositDetailsInfo;

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
			this.btnReconcile.Click += new System.EventHandler(this.btnReconcile_Click);
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.dgAgencyDeposit.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgAgencyDeposit_ItemDataBound);
			//this.btnDelete1.Click += new System.EventHandler(this.btnDelete1_Click);
			//this.btnSave1.Click += new System.EventHandler(this.btnSave1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReconcile_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Registering the script
				//for opening the reconciled window
				WriteOpenReconcileWindowScript();
			}
			catch(Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Visible = true;	
			}
		}

		#region COmmented Code
//		private void btnSave_Click(object sender, System.EventArgs e)
//		{
//			objDepositDetails = new ClsDepositDetails();
//			try
//			{
//				ClsDepositDetailsInfo objDepositDetailsInfo;
//				objDepositDetailsInfo = GetFormValue();
//				int intReturnValue;	//For taking the return value of bl method
//
//				if (hidOldData.Value == "")
//				{
//					objDepositDetailsInfo.CREATED_BY = int.Parse(GetUserId());
//					objDepositDetailsInfo.CREATED_DATETIME = DateTime.Now;
//
//					//Saving the record
//					intReturnValue = objDepositDetails.AddAgencyDepositLineItems(objDepositDetailsInfo);
//
//					if (intReturnValue > 0)
//					{
//						hidCD_LINE_ITEM_ID.Value = objDepositDetailsInfo.CD_LINE_ITEM_ID.ToString();
//						hidFormSaved.Value = "1";
//						GetOldXml();
//					}
//					else
//					{
//						hidFormSaved.Value = "2";
//					}
//				}
//				else
//				{
//					objDepositDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
//					objDepositDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
//
//					//Updating the record
//					intReturnValue = objDepositDetails.UpdateAgencyDepositLineItems(objDepositDetailsInfo);
//					GetOldXml();
//				}
//
//				if (intReturnValue == 1)
//				{
//					//Unable to reconciled the agency amount hence registering the script
//					//for opening the reconciled window
//					WriteOpenReconcileWindowScript();
//				}
//
//				if (intReturnValue > 0)
//				{
//					//Saved successfully
//					hidFormSaved.Value = "1";
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
//				}
//				else 
//				{
//					//Some error occured
//					hidFormSaved.Value = "2";
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
//				}
//				lblMessage.Visible = true;
//			}
//			catch(Exception objExp)
//			{
//				lblMessage.Text = objExp.Message.ToString();
//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
//				lblMessage.Visible = true;
//			}
//		}
		#endregion

		/// <summary>
		/// Writes the script for opening the reconciled window
		/// </summary>
		private void WriteOpenReconcileWindowScript()
		{
            if (!ClientScript.IsStartupScriptRegistered("OpenReconciledWindow"))
			{
				ClientScript.RegisterStartupScript(this.GetType(),"OpenReconciledWindow",
					"<script language='javascript'>mywin = window.open('DepositAgencyDistribution.aspx?AGENCY_ID=' + " + txtAGENCY_NAME.DataValue
						+ "+ '&MONTH=' + (parseInt(document.getElementById('cmbPOLICY_MONTH').selectedIndex) + 1) "
						+ "+ '&YEAR=' + parseInt(document.getElementById('txtYEAR').value)"
						+ "+ '&CD_LINE_ITEM_ID=' + parseInt(document.getElementById('hidCD_LINE_ITEM_ID').value)"
						+ "+ '&RECEIPT_AMOUNT=' + document.getElementById('txtRECEIPT_AMOUNT').value"
						+ "+ '&' ,'DistributeAgencyReceipts','height=500,width=1000,status= no,resizable= no,scrollbars=no,toolbar=no,location=no,menubar=no');"
					+ "mywin.moveTo(0,0);</script>"
					);
			}
		}
		#region Commented Code
//		private void btnDelete_Click(object sender, System.EventArgs e)
//		{
//			ClsDepositDetails objDepositDetails = new ClsDepositDetails();
//
//			try
//			{
//				int CREATED_BY = int.Parse(GetUserId());
//				if (objDepositDetails.Delete(int.Parse(hidCD_LINE_ITEM_ID.Value),CREATED_BY) > 0)
//				{
//					//Deleted successfully
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "127");
//					hidFormSaved.Value = "0";
//					hidOldData.Value = "";
//				}
//				else
//				{
//					//Unable to deleted
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "128");
//					hidFormSaved.Value = "-2";
//				}
//
//			}
//			catch (Exception objExp)
//			{
//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
//				lblMessage.Text = objExp.Message.ToString();
//				lblMessage.Visible = true;
//			}
//			finally
//			{
//				objDepositDetails.Dispose();
//			}
//		}
		#endregion

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			/*get hidden Values*/
			if(cmbPOLICY_MONTH.SelectedValue.ToString()!="")
			hidMonth.Value = cmbPOLICY_MONTH.SelectedValue.ToString().Trim();
			if(txtYEAR.Text.ToString()!="")
				hidYear.Value = txtYEAR.Text.ToString().Trim();
			string strAgencyName = txtAGENCY_NAME.Text.Trim() == "" ? "" : txtAGENCY_NAME.Text.Trim();

			BindingAgencyDeposit(int.Parse(cmbPOLICY_MONTH.SelectedValue.ToString()),int.Parse(txtYEAR.Text.ToString().Trim()),strAgencyName,int.Parse(hidDEPOSIT_ID.Value.ToString()));
			if(dgAgencyDeposit.Items.Count > 0)
				btnFind.CausesValidation = false;
			else
				btnFind.CausesValidation = true;

		}
		private void BindingAgencyDeposit(int month,int year,string agencyName,int depositID)
		{
			ClsDepositDetails objDepositDetails = new ClsDepositDetails();
			DataSet dsAgenDeposit = objDepositDetails.FetchAgencyDepositsLineItems(month,year,agencyName,depositID);
			dgAgencyDeposit.DataSource = dsAgenDeposit;
			dgAgencyDeposit.DataBind();

		}
		private void Binding(int depositID)
		{
			ClsDepositDetails objDepositDetails = new ClsDepositDetails();
			DataSet dsAgenDeposit = objDepositDetails.FetchAgencyDeposits(depositID);
			dgAgencyDeposit.DataSource = dsAgenDeposit;
			dgAgencyDeposit.DataBind();

		}

		private void dgAgencyDeposit_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				int agencyID = int.Parse(e.Item.Cells[1].Text.ToString());
				TextBox txtReceipt_Amt = (TextBox)e.Item.FindControl("txtReceipt_Amt");
			
				txtReceipt_Amt.Attributes.Add("onblur","javascript:FormatAmount(this);");
				HyperLink hlnkReconcile = ((HyperLink)e.Item.FindControl("hlnkReconcile"));

				int agencyMonth  = int.Parse(e.Item.Cells[4].Text.ToString().Trim());			//MONTH_YEAR
				int agencyYear = int.Parse(e.Item.Cells[5].Text.ToString().Trim());				//POLICY_MONTH

				string cdLineItemID = dgAgencyDeposit.DataKeys[e.Item.ItemIndex].ToString();	//CD_LINE_ITEM_ID
				if(cdLineItemID == "")
				{
					hlnkReconcile.Enabled = false;
				}
				else
				{
					hlnkReconcile.Enabled = true;
					Label lblStatus = ((Label)(e.Item.FindControl("lblStatus")));
					if(lblStatus!=null)
					{
						dgAgencyDeposit.Columns[11].Visible = true;
						lblStatus.Visible = true;
						if(lblStatus.Text.Trim() == "Y")
						{
							lblStatus.Text="Fully Distributed";
						}
						else
						{
							lblStatus.Text="Not Distributed";
						}
					}
				}



				hlnkReconcile.NavigateUrl = "javascript:OpenReconcileWindow('" 
					+ dgAgencyDeposit.DataKeys[e.Item.ItemIndex] 
					+ "','" 
					+ agencyID
					+ "','" 
					+ agencyMonth
					+ "','" 
					+ agencyYear
					+ "','" 
					+ dgAgencyDeposit.DataKeys[e.Item.ItemIndex] //hidCD_LINE_ITEM_ID.Value
					+ "','" 
					+ txtReceipt_Amt.Text 
					+ "')";
				//double BalAmt = double.Parse(e.Item.Cells[8].Text);
				Label lblBalAmt = (Label)e.Item.FindControl("lbl_BalAmount");
				string BalAmt = "0";
				if(lblBalAmt!=null)
				{
					BalAmt = lblBalAmt.Text;					
				}
				CheckBox chkAgenDepot = (CheckBox)e.Item.FindControl("chkAgenDepot");
				chkAgenDepot.Attributes.Add("onClick","JavaScript:fillBalAmtOnChkChng('" + (e.Item.ItemIndex + 2) + "','" + BalAmt + "');");
				if(chkAgenDepot!=null)
				{
					if(DataBinder.Eval(e.Item.DataItem,"SELECTED_ITEMS").ToString()=="True")					
						chkAgenDepot.Checked = true;					
					else
						chkAgenDepot.Checked= false;
				}

				
				//Set Validations:
				SetValidators(e);

			}
		}

		//added by uday to format amount
		protected string FormatMoney(object amount) 
		{		
			string tempMoney = String.Format("{0:0,0.00}", amount);
			if(tempMoney.StartsWith("0"))
			{
				tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
			}
			return tempMoney;
		//	return String.Format("{0:C}", amount);
		}
		//

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
            //Get all the selected records
			bool blnSave ;
			int cdLineItemID = 0;
			string strTxtRecAmt = "";
			foreach (DataGridItem item in dgAgencyDeposit.Items)
			{
				blnSave = ((CheckBox)(item.Cells[0].FindControl("chkAgenDepot"))).Checked;
				
				if(blnSave)
				{
					if(dgAgencyDeposit.DataKeys[item.ItemIndex].ToString() == "")
						cdLineItemID = 0;
					else
						cdLineItemID = int.Parse(dgAgencyDeposit.DataKeys[item.ItemIndex].ToString()); //CD_LINE_ITEM_ID

					int depositID = int.Parse(hidDEPOSIT_ID.Value.ToString());						//DEPOSIT ID
                    TextBox txtReceipt_Amt = ((TextBox)(item.FindControl("txtReceipt_Amt")));		//REONCILE AMOUNT
					if(txtReceipt_Amt.Text.Trim()!="")
						strTxtRecAmt = txtReceipt_Amt.Text.ToString();
					else
						continue;
                        
					int receiptFromID = int.Parse(item.Cells[1].Text.ToString());					//RECEIPT_FROM_ID
					string agencyName = item.Cells[2].Text.ToString();								//RECEIPT_FROM_NAME
					int agencyMonth  = int.Parse(item.Cells[4].Text.ToString().Trim());				//MONTH_YEAR
					int agencyYear = int.Parse(item.Cells[5].Text.ToString().Trim());				//POLICY_MONTH
					
					#region SAVE CURRENT_DEPOSIT_LINE_ITEMS
					objDepositDetails = new ClsDepositDetails();
					try
					{
						ClsDepositDetailsInfo objDepositDetailsInfo;
						//POPULATE THE MODEL OBJECT FOR SELECTED ITEMS
						objDepositDetailsInfo = GetFormValuesFromGrid(depositID,strTxtRecAmt,receiptFromID,agencyName,agencyYear,agencyMonth,cdLineItemID);
						int intReturnValue;	//For taking the return value of bl method

							objDepositDetailsInfo.CREATED_BY = int.Parse(GetUserId());
							objDepositDetailsInfo.CREATED_DATETIME = DateTime.Now;
							objDepositDetailsInfo.DEPOSIT_TYPE = strDEPOSIT_TYPE;

							//Saving the record
							intReturnValue = objDepositDetails.AddAgencyDepositLineItems(objDepositDetailsInfo);

						Label lblStatus = ((Label)(item.FindControl("lblStatus")));	
						if (intReturnValue == 2)
						{
							//Unable to reconciled the agency amount hence registering the script
							//for opening the reconciled window
							//Commented the WriteOpenReconcileWindowScript();
							if(lblStatus!=null)
							{
								//dgAgencyDeposit.Columns[11].Visible = true;
								//lblStatus.Visible = true;
								lblStatus.Text = "Not Distributed";
							}

						}
						else if (intReturnValue == 1)
						{
							if(lblStatus!=null)
							{
								lblStatus.Text = "Fully Distributed";
							}

						}
						
						

						if (intReturnValue > 0)
						{
							//Saved successfully
							hidFormSaved.Value = "1";

							//Added by Swarup on 12/12/2006
							if (cdLineItemID==0)
							{
								lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
							}
							else 
							{
								// for updation
								lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "31");
							}
						}
						else 
						{
							//Some error occured
							hidFormSaved.Value = "2";
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
						}
						lblMessage.Visible = true;
					}
					catch(Exception objExp)
					{
						lblMessage.Text = objExp.Message.ToString();
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
						lblMessage.Visible = true;
					}
					#endregion
				
				}

			}
			//BINDING GRID for the fetched and Saved details : 
			if(hidMonth.Value!="" || hidYear.Value!="")
                BindingAgencyDeposit(int.Parse(hidMonth.Value.ToString()),int.Parse(hidYear.Value.ToString().Trim()),"",int.Parse(hidDEPOSIT_ID.Value));

					
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			bool blnSave ;
			int cdLineItemID = 0;
			foreach (DataGridItem item in dgAgencyDeposit.Items)
			{
				blnSave = ((CheckBox)(item.Cells[0].FindControl("chkAgenDepot"))).Checked;
				
				if(blnSave)
				{
					if(dgAgencyDeposit.DataKeys[item.ItemIndex].ToString() == "")
						cdLineItemID = 0;
					else
						cdLineItemID = int.Parse(dgAgencyDeposit.DataKeys[item.ItemIndex].ToString()); //CD_LINE_ITEM_ID
					#region Delete Line Items
					ClsDepositDetails objDepositDetails = new ClsDepositDetails();

					try
					{
						int CREATED_BY = int.Parse(GetUserId());
						if (objDepositDetails.Delete(cdLineItemID,CREATED_BY) > 0)
						{
							//Deleted successfully
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "127");
							hidFormSaved.Value = "0";
							hidOldData.Value = "";
						}
						else
						{
							//Unable to deleted
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "128");
							hidFormSaved.Value = "-2";
						}

					}
					catch (Exception objExp)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
						lblMessage.Text = objExp.Message.ToString();
						lblMessage.Visible = true;
					}
					finally
					{
						objDepositDetails.Dispose();
					}
					#endregion
				
				}

			}
			//BINDING GRID for the fetched and Saved details : 
			if(hidMonth.Value!="" || hidYear.Value!="")
				BindingAgencyDeposit(int.Parse(hidMonth.Value.ToString()),int.Parse(hidYear.Value.ToString().Trim()),"",int.Parse(hidDEPOSIT_ID.Value));		
			else
				Binding(int.Parse(hidDEPOSIT_ID.Value));

			#region Commented Code
			/*bool blnSave ;
			int cdLineItemID = 0;
			foreach (DataGridItem item in dgAgencyDeposit.Items)
			{
				blnSave = ((CheckBox)(item.Cells[0].FindControl("chkAgenDepot"))).Checked;
				
				if(blnSave)
				{
					if(dgAgencyDeposit.DataKeys[item.ItemIndex].ToString() == "")
						cdLineItemID = 0;
					else
						cdLineItemID = int.Parse(dgAgencyDeposit.DataKeys[item.ItemIndex].ToString()); //CD_LINE_ITEM_ID
					#region Delete Line Items
					ClsDepositDetails objDepositDetails = new ClsDepositDetails();

					try
					{
						int CREATED_BY = int.Parse(GetUserId());
						if (objDepositDetails.Delete(cdLineItemID,CREATED_BY) > 0)
						{
							//Deleted successfully
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "127");
							hidFormSaved.Value = "0";
							hidOldData.Value = "";
						}
						else
						{
							//Unable to deleted
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "128");
							hidFormSaved.Value = "-2";
						}

					}
					catch (Exception objExp)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
						lblMessage.Text = objExp.Message.ToString();
						lblMessage.Visible = true;
					}
					finally
					{
						objDepositDetails.Dispose();
					}
					#endregion
				
				}

			}
			//BINDING GRID for the fetched and Saved details : 
			if(hidMonth.Value!="" || hidYear.Value!="")
				BindingAgencyDeposit(int.Parse(hidMonth.Value.ToString()),int.Parse(hidYear.Value.ToString().Trim()),null,int.Parse(hidDEPOSIT_ID.Value));		
			else
				Binding(int.Parse(hidDEPOSIT_ID.Value));*/
			#endregion

		}
	
	}
}

