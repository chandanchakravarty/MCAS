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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Account;
using Cms.CmsWeb;
#endregion
namespace Cms.Account.Aspx
{
	public class AddCustAgencyPayments : Cms.Account.AccountBase
	{
		#region Control Declarations
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgCustAgencyPay;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnConfirm;
		protected Cms.CmsWeb.Controls.CmsButton btnSaveConfirm;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_NAME;
		protected string URL;
		protected string AgenCode,CarrierCode;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAllowEFT;
		protected System.Web.UI.HtmlControls.HtmlForm ACT_CUSTOMER_PAYMENTS_FROM_AGENCY;
		protected Cms.CmsWeb.Controls.CmsButton btnConfirmPrint;
		private string strpolList = "";
		protected Cms.CmsWeb.Controls.CmsButton btnConfirming;
		#endregion
	
		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region  Button Permissions/Screen ID
			base.ScreenId	=	"392";
			btnConfirm.CmsButtonClass		=	CmsButtonType.Execute;
			btnConfirm.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnSaveConfirm.CmsButtonClass	=	CmsButtonType.Execute;
			btnSaveConfirm.PermissionString	=	gstrSecurityXML;

			btnConfirmPrint.CmsButtonClass	=	CmsButtonType.Execute;
			btnConfirmPrint.PermissionString	=	gstrSecurityXML;

			btnConfirming.CmsButtonClass = CmsButtonType.Execute;
			btnConfirming.PermissionString = gstrSecurityXML;
			#endregion

			URL	   = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			AgenCode = GetSystemId();
			CarrierCode = CarrierSystemID;
			//Commented and changes has been done For Itrack issue  #6730.
			//btnSave.Attributes.Add("onclick","javascript:Page_ClientValidate();return DoValidationCheckOnSave();");
			//btnConfirm.Attributes.Add("onclick","javascript:Page_ClientValidate();HideShowCommit();javascript:return DoValidationCheckOnSave();");
			//btnConfirmPrint.Attributes.Add("onclick","javascript:return DoValidationCheckOnSave();");
			//btnDelete.Attributes.Add("onclick","javascript:return CheckedRow();return DeleteRows();");

			btnSave.Attributes.Add("onclick","javascript:HideShowCommitInProgress();return HideShowCommitIn(document.getElementById('btnSave')); ");						
			btnConfirm.Attributes.Add("onclick","javascript:HideShowCommit();return HideShowCommitIn(document.getElementById('btnConfirm'));");
			btnConfirmPrint.Attributes.Add("onclick","javascript:return HideShowCommitIn(document.getElementById('btnConfirmPrint'));");
			btnDelete.Attributes.Add("onclick","javascript:return HideShowCommitIn(document.getElementById('btnDelete'));return CheckedRow();return DeleteRows();");
			
			btnSaveConfirm.Visible=false;
			if(!Page.IsPostBack)
			{
				BindGrid(AgenCode);
					
			}
		}
		#endregion

		#region BindGrid/ItemDataBound
		private void dgCustAgencyPay_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				#region Fill Payment Mode
				DropDownList cmbMode	 = (DropDownList)e.Item.FindControl("cmbMODE");
				cmbMode.DataSource		 = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RPT_MD");
				cmbMode.DataTextField	 = "LookupDesc"; 
				cmbMode.DataValueField	 = "LookupID";
				cmbMode.DataBind();
				
				ListItem Li = new ListItem();
				Li = cmbMode.Items.FindByValue("11977"); // Remove option : "Already processed"
				cmbMode.Items.Remove(Li);
				cmbMode.Items.Insert(0,"");
				if(!AgenCode.Equals(CarrierCode.ToUpper())) // If Agency is not Wolverine then show payment modes on basis of agency.
				{
					string strAllowEFT = hidAllowEFT.Value;//e.Item.Cells[6].Text.ToString();
					if (strAllowEFT == "10964")
					{
						ListItem Lis = new ListItem("EFT","11976");
						cmbMode.Items.Remove(Lis);
					}
				}
				
				
				SetCheckboxProperties(e);
				if ( DataBinder.Eval(e.Item.DataItem,"MODE") != System.DBNull.Value) 
				{
					ClsCommon.SelectValueInDDL(cmbMode,DataBinder.Eval(e.Item.DataItem,"MODE"));
				}	
				#endregion

				HtmlImage imgSelect = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgPOLICY_NO");
				imgSelect.Attributes.Add("onclick","OpenPolicyLookup('" + (e.Item.ItemIndex + 2).ToString() + "')" );
				TextBox objTxt = (TextBox)e.Item.FindControl("txtPolicyNo");
				objTxt.Attributes.Add("onBlur","FillPolDetails('" + (e.Item.ItemIndex + 2).ToString() + "')");
				SetValidators(e);
			}
		}

		private void SetCheckboxProperties(DataGridItemEventArgs e)
		{
			try
			{
				CheckBox cb = (CheckBox ) e.Item.FindControl("chkSelect");
				if(dgCustAgencyPay.DataKeys[e.Item.ItemIndex]!= System.DBNull.Value)
					cb.Checked = true;
				cb.Attributes.Add("onclick","HideErrMsgsOnCheckChange('" + cb.ClientID + "')");
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
		}
		protected void BindGrid(string AgenCode)
		{
			ClsCustAgencyPayments objBL = new ClsCustAgencyPayments();
			
			int pageSize = base.DepositDetailsPageSize; // Grid Page Size
			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
			DataSet ds = new DataSet();
			ds = objBL.GetCustAgencyPayments(AgenCode);
			if(ds.Tables[0].Rows.Count >0 )
			{
				int totRecords = Convert.ToInt32(ds.Tables[0].Rows.Count);
				int TotalPages = 0;
				if ( totRecords % pageSize  == 0 ) 
				{TotalPages = totRecords / pageSize;}
				else
				{TotalPages = (totRecords / pageSize) + 1;}
			}
			int currentRowCount = ds.Tables[0].Rows.Count;
			if( currentRowCount < pageSize )
			{
				for ( int i = 0; i < pageSize - currentRowCount; i++ )
				{
					DataTable dt = ds.Tables[0];
					DataRow dr = dt.NewRow();
					dt.Rows.Add(dr);
				}
			}
			//hidAllowEFT.Value = ds.Tables[1].Rows[0]["ALLOW_EFT"].ToString(); 15 May 2008
			//hidAGENCY_ID.Value = ds.Tables[1].Rows[0]["AGENCY_ID"].ToString();
			dgCustAgencyPay.DataSource = ds.Tables[0];
			dgCustAgencyPay.DataBind();
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
			this.dgCustAgencyPay.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCustAgencyPay_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);			
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			this.btnConfirmPrint.Click += new System.EventHandler(this.btnConfirmPrint_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Event Handlers : Save, Delete, Confirm
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			 fxnSave(true);
		}

		// Save the records in Temporary table.On confirm, they will be moved to the main table.
		protected void fxnSave(bool TranLog)
		{
			CheckBox objChk = null;
			strpolList = "";
			foreach(DataGridItem dgItem in dgCustAgencyPay.Items)
			{
				string tempPolList="";
				objChk = (CheckBox)dgItem.FindControl("chkSelect");
				if(objChk.Checked)
				{
					try
					{
							ClsCustAgencyPaymentsInfo objInfo = new ClsCustAgencyPaymentsInfo();
							if (((TextBox)dgItem.FindControl("txtPolicyNo")).Text != "")
							{
								objInfo.POLICY_NUMBER = ((TextBox)dgItem.FindControl("txtPolicyNo")).Text;
								//strpolList += objInfo.POLICY_NUMBER + "|";
								tempPolList= objInfo.POLICY_NUMBER + "|";
							}
						//Added For Itrack Issue #6172.

						string Policy_Info = Cms.BusinessLayer.BlAccount.ClsCustAgencyPayments.GetPolicyInformationForAddCustAgencyPayemnts(objInfo.POLICY_NUMBER);     
						//ClsCustAgencyPayments.GetCustomerBal(CustID,PolID,PolVerID,PolNum,CalledFrom).ToString();
						string [] arrRows = Policy_Info.Split('-');
						int cust_id = int.Parse(arrRows[0]);
						int pol_id = int.Parse(arrRows[1]);
						int pol_version_id = int.Parse(arrRows[2]);
						//objBudgetPlanInfo.GL_ID = int.Parse(arrRows[0]);
						//objBudgetPlanInfo.FISCAL_ID = int.Parse(arrRows[1]);

						string Policy = Cms.BusinessLayer.BlAccount.ClsCustAgencyPayments.GetCustomerBal(cust_id,pol_id,pol_version_id,objInfo.POLICY_NUMBER,null);         
                        string [] CustInfo =  Policy.Split('~'); 
                        //End ####6172.

						//Code Commented and Added For Itrack Issue #6172.
						
						/*if(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidTOTAL_DUE")).Value != "") // Total Due
						{
							objInfo.TOTAL_DUE = double.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidTOTAL_DUE")).Value);
						}*/
							if(CustInfo[0]!= "")
							    objInfo.TOTAL_DUE = Double.Parse(CustInfo[0]); 
							else
							   objInfo.TOTAL_DUE = 0.0;
							
							/*if(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidMIN_DUE")).Value != "") // Min Due
							{
								objInfo.MIN_DUE = double.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidMIN_DUE")).Value);
							}*/

							if(CustInfo[1]!= "" )
								objInfo.MIN_DUE = Double.Parse(CustInfo[1]); 
							else
								objInfo.MIN_DUE =0.0;
                             //End #####6172.
						

							if (((TextBox)dgItem.FindControl("txtAMOUNT")).Text != "")
							{
								objInfo.AMOUNT = double.Parse(((TextBox)dgItem.FindControl("txtAMOUNT")).Text);
								//strpolList += objInfo.AMOUNT.ToString();
								tempPolList+= ((TextBox)dgItem.FindControl("txtAMOUNT")).Text.Trim();//objInfo.AMOUNT.ToString();
							}
							//strpolList += "~";
							if (((DropDownList)dgItem.FindControl("cmbMODE")).SelectedIndex != 0)
							{
								objInfo.MODE = int.Parse(((DropDownList)dgItem.FindControl("cmbMODE")).SelectedValue);
							}
							//Code Commented and Added For Itrack Issue #6172.

							if(CustInfo[7] != "")
								objInfo.POLICY_ID  = int.Parse(CustInfo[7]); 
						
							/*if(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidPOLICY_ID")).Value != "") 
							{
								objInfo.POLICY_ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidPOLICY_ID")).Value);
							}*/
							if(CustInfo[8] != "" )
							{
								objInfo.POLICY_VERSION_ID  = int.Parse(CustInfo[8]); 
							}

							/*if(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidPOLICY_VERSION_ID")).Value != "") 
							{
								objInfo.POLICY_VERSION_ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidPOLICY_VERSION_ID")).Value);
							}*/
							
							/*if(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidCUSTOMER_ID")).Value != "") 
							{
								objInfo.CUSTOMER_ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidCUSTOMER_ID")).Value);
							}*/
							if(CustInfo[6] != "" )
							{
								objInfo.CUSTOMER_ID   = int.Parse(CustInfo[6]); 
							}
						     //End #####6172.    

                            //Changed by Charles on 19-May-10 for Itrack 51
							if(GetSystemId() == CarrierSystemID)//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString().ToUpper())
							{
								/*if(dgItem.Cells[8].Text.Trim() !="" && dgCustAgencyPay.DataKeys[dgItem.ItemIndex] != System.DBNull.Value && dgItem.Cells[8].Text.Trim() !="&nbsp;")	
									objInfo.AGENCY_ID = int.Parse(dgItem.Cells[8].Text); // Agency ID
								else
									objInfo.AGENCY_ID = int.Parse(hidAGENCY_ID.Value);*/
						
								//Commnted on 17 Sep 2009  and added For Itrack Issue #6172.

								/*if(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidAGENCY_ID")).Value != "") 
								{
									objInfo.AGENCY_ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidAGENCY_ID")).Value);
								}*/
								if(CustInfo[2] != "" )
								{
									objInfo.AGENCY_ID    = int.Parse(CustInfo[2]); 
								}	

							}
							else //if Agency Login (Agency ID would be same for all)
							{
								//objInfo.AGENCY_ID = int.Parse(hidAGENCY_ID.Value);
								//objInfo.AGENCY_ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidAGENCY_ID")).Value);
								objInfo.AGENCY_ID    = int.Parse(CustInfo[2]);
							}
                             //########End 6172.
						
							objInfo.CREATED_BY_USER = int.Parse(GetUserId());

							// Check if EFT is allowed for the Policy Number Chosen
							//string strEFT = ((System.Web.UI.HtmlControls.HtmlInputHidden)dgItem.FindControl("hidALLOW_EFT")).Value; 
							string strEFT = CustInfo[4].ToString();

							if((strEFT.Equals("10964") || strEFT.Equals("") || strEFT.Equals("0")) && (objInfo.MODE != 11975)) // Mode is EFT (NO)
							{
								string PolNo = ((TextBox)dgItem.FindControl("txtPolicyNo")).Text;
								lblMessage.Text = lblMessage.Text + " Agency does not allow EFT ("+ PolNo +")";
								lblMessage.Visible	= true;
								break;
							}

							ClsCustAgencyPayments objBL = new ClsCustAgencyPayments();
							if (dgCustAgencyPay.DataKeys[dgItem.ItemIndex] != System.DBNull.Value) // Update
							{
								objInfo.IDEN_ROW_ID = Convert.ToInt32(dgCustAgencyPay.DataKeys[dgItem.ItemIndex].ToString());
								int RetVal = objBL.UpdateTempCustAgencyPayments(objInfo,TranLog);
								if(RetVal > 0)
								{
									lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "31");
									lblMessage.Visible	= true;
								}
								else
								{
									lblMessage.Text = ClsMessages.FetchGeneralMessage("1035");  
									lblMessage.Visible	= true;
								}
								//if flag is Passed FALSE then it is Confirm Case :
								if(TranLog == false)
								{
									ClsCustAgencyPayments objBLCustAgen = new ClsCustAgencyPayments();
									int RetValConfirm = objBLCustAgen.AddActualCustAgencyPayments(objInfo.IDEN_ROW_ID,objInfo.CREATED_BY_USER);
									if(RetValConfirm > 0)
									{
										strpolList += tempPolList + "~";
										lblMessage.Text = ClsMessages.FetchGeneralMessage("902");
										lblMessage.Visible	= true;
										//BindGrid(AgenCode);
									}
                                    
								}
							}
							else // save
							{
								
								int RetVal = objBL.AddTempCustAgencyPayments(objInfo,TranLog);
								if(RetVal > 0)
								{
									lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "29");
									lblMessage.Visible	= true;
								}
								else
								{
								   lblMessage.Text = ClsMessages.FetchGeneralMessage("1035");
									lblMessage.Visible	= true;
								}
								//if flag is Passed FALSE then it is Confirm Case :
								if(TranLog == false)
								{
									ClsCustAgencyPayments objBLCustAgen = new ClsCustAgencyPayments();
									int RetValConfirm = objBLCustAgen.AddActualCustAgencyPayments(objInfo.IDEN_ROW_ID,objInfo.CREATED_BY_USER);
									if(RetValConfirm > 0)
									{
										strpolList += tempPolList + "~";
										lblMessage.Text = ClsMessages.FetchGeneralMessage("902");
										lblMessage.Visible	= true;
									}
                                    
								}
 
							}						
					}
							
					  catch(Exception objEx)
					{
						lblMessage.Text		= objEx.Message;
						lblMessage.Visible	= true;
						return;
					}
				}
			}
			BindGrid(AgenCode);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			Cms.Model.Account.ClsCustAgencyPaymentsInfo objInfo;
			foreach(DataGridItem dgItem in dgCustAgencyPay.Items)
			{
				try
				{
					if ( dgItem.ItemType == ListItemType.Item || dgItem.ItemType == ListItemType.AlternatingItem )
					{
						if (dgCustAgencyPay.DataKeys[dgItem.ItemIndex] != System.DBNull.Value && ((CheckBox)dgItem.FindControl("chkSelect")).Checked == true)
						{
							objInfo = new ClsCustAgencyPaymentsInfo();
							objInfo.IDEN_ROW_ID = Convert.ToInt32(dgCustAgencyPay.DataKeys[dgItem.ItemIndex]);
							objInfo.CREATED_BY_USER = int.Parse(GetUserId());
							ClsCustAgencyPayments objBL = new ClsCustAgencyPayments();
							int RetVal = objBL.DeleteTempCustAgencyPayments(objInfo);
							if(RetVal > 0)
							{
								lblMessage.Text = ClsMessages.FetchGeneralMessage("127");
								lblMessage.Visible	= true;
							
							}
						}
					}
				}
				catch(Exception ex)					
				{
					lblMessage.Text	=	ex.Message;
					lblMessage.Visible=true;
				}
			}
			BindGrid(AgenCode);
	
		}

		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			fxnSave(false);
//			int UserID = int.Parse(GetUserId());
//			ClsCustAgencyPayments objBL = new ClsCustAgencyPayments();
//			int RetVal = objBL.AddActualCustAgencyPayments(UserID);
//			if(RetVal > 0)
//			{
//				lblMessage.Text = ClsMessages.FetchGeneralMessage("902");
//				lblMessage.Visible	= true;
//				BindGrid(AgenCode);
//			}
		}
		#endregion

		#region Set Validators
		private void SetValidators(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revAMOUNT");
			revValidator.ValidationExpression = aRegExpDoublePositiveNonZero;
			revValidator.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

			CustomValidator csvValidate = (CustomValidator) e.Item.FindControl("csvPolicyNo");
			csvValidate.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

			CustomValidator csvValAmt = (CustomValidator)e.Item.FindControl("csvAMOUNT");
			csvValAmt.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

			CustomValidator csvPayMode = (CustomValidator)e.Item.FindControl("csvMODE");
			csvPayMode.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());
		}
		#endregion

		private void btnConfirmPrint_Click(object sender, System.EventArgs e)
		{
			fxnSave(false);
			/*int UserID = int.Parse(GetUserId());
			ClsCustAgencyPayments objBL = new ClsCustAgencyPayments();
			int RetVal = objBL.AddActualCustAgencyPayments(UserID);
			if(RetVal > 0)
			{
				string responsestring = "<script language='javascript'> window.open(" + "'../../application/aspx/DecPage.aspx?CalledFrom=CUST_RECEIPT&LOB_ID=CHK&CHECK_ID=" + strpolList + "'); </script>";
				Response.Write(responsestring);
				lblMessage.Text = ClsMessages.FetchGeneralMessage("902");
				lblMessage.Visible	= true;
				BindGrid(AgenCode);
			}*/	
			if(strpolList!="")
			{
				string responsestring = "<script language='javascript'> window.open(" + "'../../application/aspx/DecPage.aspx?CalledFrom=CUST_RECEIPT&LOB_ID=CHK&CHECK_ID=" + strpolList.ToUpper() + "'); </script>";
				Response.Write(responsestring);

			}
		}
	}
}
