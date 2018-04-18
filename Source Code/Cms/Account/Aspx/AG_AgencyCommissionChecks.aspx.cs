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
using Cms.CmsWeb;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.ExceptionPublisher.ExceptionManagement;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AG_AgencyCommissionChecks.
	/// </summary>
	public class AG_AgencyCommissionChecks :Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.TextBox txtAmount;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.RangeValidator rnvYear;
		protected System.Web.UI.WebControls.CustomValidator csvAMOUNT;
		protected System.Web.UI.HtmlControls.HtmlForm Agency_CommissionChecks;		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYear;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAmount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCHECK_ID;
		protected System.Web.UI.WebControls.DropDownList CmbCommType;
		protected Cms.CmsWeb.Controls.CmsButton btnFind;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.WebControls.CheckBox chSelectAll;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.DataGrid dgAgencyCheck;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnCreateChecks;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidYear;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYear;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPAYMENT_AMOUNT;
		public int AgenID;
		//private string CARRIERCODE = "";
		private string CommType ="";
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				
				if(Request.QueryString["TypeID"].ToString().Equals("6"))//Re-Insurance
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11199);
				else if(Request.QueryString["TypeID"].ToString().Equals("5"))//Claims
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11200);
				else
				{
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,0);//General
					SetBankAccountDD();
				}

			
				btnSave.Attributes.Add("OnClick","javascript:if (Select()== false) return false;");
				btnDelete.Attributes.Add("OnClick","javascript:return Select();");
				//btnCreateChecks.Attributes.Add("OnClick","javascript:return CheckDistribution();");
				btnCreateChecks.Attributes.Add("OnClick","javascript:if(CheckDistribution() == false) return false;if (Select()== false) return false;if(fnCheckForNegAmt()==false)return false;");
				//btnFind.Attributes.Add("Onclick","javascript:")				
				txtYear.Text = DateTime.Now.Year.ToString();
				cmbMonth.SelectedValue = DateTime.Now.Month.ToString();
				BindGrid(0,0,0,"REG");
				

			}
	
		
			base.ScreenId = "210_0"; // To be change
			lblMessage.Visible=false;
			txtAmount.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtAmount'));");
			btnBack.Attributes.Add("onclick","javascript:return DisableValidators();");	

			btnFind.PermissionString = gstrSecurityXML;
			btnFind.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;

			/*Modified on 30 Nov 2006*/
			btnSave.PermissionString = gstrSecurityXML;
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;

			btnDelete.PermissionString = gstrSecurityXML;
			btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;

			btnCreateChecks.PermissionString = gstrSecurityXML;
			btnCreateChecks.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;

			btnBack.CmsButtonClass				=	CmsButtonType.Execute;
			btnBack.PermissionString			=	gstrSecurityXML;

			SetErrorMessages();
			
		}
		private void SetErrorMessages()
		{
			revYear.ValidationExpression = aRegExpInteger;
			rfvACCOUNT_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("945");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			this.dgAgencyCheck.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgAgencyCheck_ItemDataBound);
			
			this.btnCreateChecks.Click += new System.EventHandler(this.btnCreateChecks_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			//this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			 

		}
		// btn Save
//		private void btnSave_Click(object sender, System.EventArgs e)
//		{
//			int intAgencyID =0,intMonth=0,intYear=0,result=0, checkID=0 ;
//			double decAmount =0;
//			ClsChecks objClsChecks = new ClsChecks();
//			SaveChecksTemp();
//
//			try
//			{
//
//				if(this.hidAGENCY_ID.Value !="")
//					intAgencyID = int.Parse(hidAGENCY_ID.Value.ToString());
//				if(this.cmbMonth.SelectedIndex>0)
//					intMonth=int.Parse(this.cmbMonth.SelectedValue.ToString());
//				if(this.txtYear.Text != "")
//					intYear=int.Parse(this.txtYear.Text.ToString());
//				if(this.txtAmount.Text != "")
//					decAmount=double.Parse(this.txtAmount.Text.ToString());
//				// Save
//				if(hidCHECK_ID.Value.ToString() == "")
//				{
//					result = objClsChecks.Save(intAgencyID,intMonth,intYear,decAmount,out checkID);			
//					//For opening the reconciled window
//					hidCHECK_ID.Value = checkID.ToString();
//					if(result==2)
//					{					
//						WriteOpenReconcileWindowScript();
//					}
//					if(result>0)
//						lblMessage.Text= ClsMessages.GetMessage(base.ScreenId,"1");
//					else
//						lblMessage.Text= ClsMessages.FetchGeneralMessage("20");
//				}
//				else
//				{
//					//Update
//					result = objClsChecks.Update(intAgencyID,intMonth,intYear,decAmount, int.Parse(hidCHECK_ID.Value));			
//					//For opening the reconciled window
//					//hidCHECK_ID.Value = checkID.ToString();
//					if(result==2)
//					{					
//						WriteOpenReconcileWindowScript();
//					}
//					if(result>0)
//						lblMessage.Text= ClsMessages.GetMessage(base.ScreenId,"2");
//					else
//						lblMessage.Text= ClsMessages.FetchGeneralMessage("20");
//				}			
//				lblMessage.Visible=true;
//				
//			}
//			catch(Exception ex)
//			{
//				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
//				lblMessage.Visible	=	true;
//				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
//			   ExceptionManager.Publish(ex);
//			}
//			finally
//			{
//				
//			}
//		}	
	
		/// <summary>
		/// Writes the script for opening the reconciled window
		/// </summary>
		private void WriteOpenReconcileWindowScript()
		{
			if (! ClientScript.IsStartupScriptRegistered("OpenReconciledWindow"))
			{
				ClientScript.RegisterStartupScript(this.GetType(),"OpenReconciledWindow",
					"<script language='javascript'>mywin = window.open('DepositAgencyDistribution.aspx?AGENCY_ID=' + " + hidAGENCY_ID.Value
					+ "+ '&MONTH=' + (parseInt(document.getElementById('cmbMonth').selectedIndex) + 1) "
					+ "+ '&YEAR=' + parseInt(document.getElementById('txtYear').value)"
					+ "+ '&CD_LINE_ITEM_ID=' + parseInt(document.getElementById('hidCHECK_ID').value)"
					+ "+ '&RECEIPT_AMOUNT=' + document.getElementById('txtAmount').value"
					+ "+ '&' ,'DistributeAgencyReceipts','height=500,width=1000,status= no,resizable= no,scrollbars=no,toolbar=no,location=no,menubar=no');"
					+ "mywin.moveTo(0,0);</script>"
					);
			}
		}

		#endregion

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			
			if(cmbMonth.SelectedValue.ToString()!="")
				hidMonth.Value = cmbMonth.SelectedValue.ToString().Trim();
			if(txtYear.Text.ToString()!="")
				hidYear.Value = txtYear.Text.ToString().Trim();		
			if(hidAGENCY_ID.Value !="")
				AgenID = int.Parse(hidAGENCY_ID.Value.ToString());
			BindGrid(int.Parse(cmbMonth.SelectedValue),int.Parse(txtYear.Text.ToString().Trim()),AgenID,CmbCommType.SelectedValue.ToString().Trim());
		}
		private void BindGrid(int month,int year,int AgenID,string commType)
		{
			
			CommType= CmbCommType.SelectedValue.ToString().Trim();
			ClsChecks objClsChecks = new ClsChecks();
			DataSet dsAgenChecks = objClsChecks.FetchTempAgencyChecks(month,year,AgenID,commType);
			dgAgencyCheck.DataSource = dsAgenChecks;
			dgAgencyCheck.DataBind();
			//Itrack 4009

			foreach(DataRow dr in dsAgenChecks.Tables[0].Rows)
			{
				if(dr["CHECK_ID"].ToString()!="")
				{
					btnDelete.Enabled = true;
					btnCreateChecks.Enabled = true;
				}
				else
				{
					btnDelete.Enabled = false;
					btnCreateChecks.Enabled = false;
				}
			}
			
		}

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("AG_CheckTypeSelect.aspx");
		}

		private void dgAgencyCheck_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{ 
			//Adding Style to Alternating Item
			//raghav
			//e.Item.Attributes.Add("Class","midcolora"); 
		
			if(e.Item.ItemType  == ListItemType.Header )
			{
				if(CommType == "CAC")
				{
					e.Item.Cells[2].Text = "CSR Name";
				}
			}

			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				Label lblREQ_SPECIAL_HANDLING = (Label)e.Item.FindControl("lblREQ_SPECIAL_HANDLING");			 
				if(lblREQ_SPECIAL_HANDLING.Text == "10963" )
					e.Item.Attributes.CssStyle.Add("COLOR","Red");
					//e.Item.Attributes.Add("Class","GrandFatheredCoverage");
				else
					e.Item.Attributes.Add("Class","midcolora");
				
				int agencyID = int.Parse(e.Item.Cells[1].Text.ToString());
				TextBox txtPayment_Amt = (TextBox)e.Item.FindControl("txtPayment_Amt");
				txtPayment_Amt.Attributes.Add("onblur","javascript:FormatAmount(this);");
				
				if(CommType == "CAC")
				{
					txtPayment_Amt.ReadOnly = true;
				}
				RegularExpressionValidator revValidator = null;
				revValidator = (RegularExpressionValidator)e.Item.FindControl("revPAYMENT_AMOUNT");
				revValidator.ValidationExpression=aRegExpCurrencyformat;
				revValidator.ErrorMessage = ClsMessages.FetchGeneralMessage("163");
				
				int agencyMonth  = int.Parse(e.Item.Cells[4].Text.ToString().Trim());		
				int agencyYear = int.Parse(e.Item.Cells[5].Text.ToString().Trim());	

				
				Label lblBalAmt = (Label)e.Item.FindControl("lbl_BalAmount");
				string BalAmt = "0";
				if(lblBalAmt!=null)
				{
					BalAmt = lblBalAmt.Text.ToString().Trim();
				}


				CheckBox chkAgenCheck = (CheckBox)e.Item.FindControl("chkAgenCheck");
				if(chkAgenCheck!=null)
				{
					if(DataBinder.Eval(e.Item.DataItem,"SELECTED_ITEMS").ToString()=="True")					
						chkAgenCheck.Checked = true;					
					else
						chkAgenCheck.Checked= false;
				}
				chkAgenCheck.Attributes.Add("onClick","JavaScript:fillBalAmtOnChkChng('" + (e.Item.ItemIndex + 2) + "','" + BalAmt + "');");


				HyperLink hlnkDistribute = ((HyperLink)e.Item.FindControl("hlnkDistribute"));

				string checkID = dgAgencyCheck.DataKeys[e.Item.ItemIndex].ToString();	//CHECK_ID
				Label lblStatus = ((Label)(e.Item.FindControl("lblStatus")));
				if(checkID == "")
				{
					hlnkDistribute.Enabled = false;
						lblStatus.Text="";
				}
				else
				{
					
					
					if(lblStatus!=null)
					{
						dgAgencyCheck.Columns[13].Visible = true;
						lblStatus.Visible = true;
						if(lblStatus.Text.Trim() == "Y")
						{
							lblStatus.Text="Distributed";
							//hlnkDistribute.Enabled = false;
						}
						else
						{
							lblStatus.Text="Not Distributed";
							hlnkDistribute.Enabled = true;
						}
					}
				}

				hlnkDistribute.NavigateUrl = "javascript:OpenReconcileWindow('" 
					+ checkID
					+ "')";
           
				DropDownList ddl = ((DropDownList)(e.Item.FindControl("cmbPAYMENT_MODE")));
				ddl.Items.Insert(0, new ListItem("Check", "11787")); 
				ddl.Items.Insert(0, new ListItem("EFT", "11976"));
				string strAllowEFT = e.Item.Cells[14].Text.ToString();
				// If Allow EFT value is 'NO' in Maintenance>>AgencyInfo then Remove 'CHECK' option
				if (strAllowEFT.Equals("NO"))
				{
					ListItem Li = new ListItem("EFT","11976");
					ddl.Items.Remove(Li);
				}

				if ( DataBinder.Eval(e.Item.DataItem,"PAYMENT_MODE") != System.DBNull.Value) 
				{
					Cms.BusinessLayer.BlCommon.ClsCommon.SelectValueInDDL(ddl,DataBinder.Eval(e.Item.DataItem,"PAYMENT_MODE"));
				}	
            }
		}
		#region Set Acct Id for Agency Checks :Added on 15 Oct 2007
		private void SetBankAccountDD()
		{
			int intFiscalID = 0;
			intFiscalID = ClsGeneralLedger.GetFiscalID();

			string strXML = ClsGeneralLedger.GetXmlForPageControls_Bnk_AC_Mapping("1",intFiscalID.ToString().Trim());
			strXML = strXML.Replace("\r\n","");
			strXML = strXML.Replace("<NewDataSet>","");
			strXML = strXML.Replace("</NewDataSet>","");	
			
			System.Xml.XmlDocument  objXmlDocument  = new System.Xml.XmlDocument();			
			objXmlDocument.LoadXml(strXML);
			System.Xml.XmlNode Node = objXmlDocument.SelectSingleNode("Table/BNK_AGEN_CHK_DEFAULT_AC");
			if(Node != null)
			{
				try
				{
					System.Xml.XmlNodeList objXmlNode = objXmlDocument.GetElementsByTagName("BNK_AGEN_CHK_DEFAULT_AC");
					string strBNK_AGEN_CHK_DEFAULT_AC =  objXmlNode.Item(0).InnerText;
					cmbACCOUNT_ID.SelectedValue=(strBNK_AGEN_CHK_DEFAULT_AC);	
				}
				catch
				{
					cmbACCOUNT_ID.SelectedIndex = -1;
				}
			}
		}
		#endregion
		private int SaveChecksTemp()
		{
			ClsChecks objCheck = null;
			ArrayList alCheckInfoObjects = new ArrayList();
			ArrayList alCheckInfoObjectsUpdate = new ArrayList();
			ArrayList alCheckIds = new ArrayList();
			int agencyID = 0, PayeeID = 0 ;
			int intMonth = 0;
			int intYear = 0;
				
			foreach(DataGridItem item in dgAgencyCheck.Items)
			{
				if ( item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem )
				{
					//CheckBox chkAgenCheck = (CheckBox)item.FindControl("chkAgenCheck");
					//if(chkAgenCheck.Checked)
                    TextBox txtText;
					txtText = (TextBox)item.FindControl("txtPayment_Amt");
					if(txtText.Text.ToString().Trim()!="")
					{
						ClsChecksInfo objInfo = new ClsChecksInfo();
																
						objInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue.ToString());
						
						objInfo.CHECK_TYPE = ClsChecks.CHECK_TYPES.AGENCY_COMMISSION_CHECKS;
						objInfo.CHECK_DATE = DateTime.Now;
						objInfo.CREATED_BY = int.Parse(GetUserId());
						objInfo.CREATED_DATETIME = DateTime.Now;
						objInfo.MODIFIED_BY = int.Parse(GetUserId());
						objInfo.LAST_UPDATED_DATETIME = DateTime.Now;

						agencyID = int.Parse(item.Cells[1].Text.ToString());
                        
						HtmlInputHidden hidPayeeID = (HtmlInputHidden) item.FindControl("hidPAYEE_ID");

						PayeeID  = Convert.ToInt32(hidPayeeID.Value.Trim());
						objInfo.PAYEE_ENTITY_ID = PayeeID;

                        intMonth = int.Parse(item.Cells[4].Text.ToString());
						intYear = int.Parse(item.Cells[5].Text.ToString());

						objInfo.AGENCY_ID = agencyID;
						objInfo.MONTH = intMonth;
						objInfo.YEAR = intYear;
						objInfo.COMM_TYPE = CmbCommType.SelectedValue.ToString().Trim();
					
						if(objInfo.COMM_TYPE == "CAC")
							objInfo.PAYEE_ENTITY_TYPE = "CSR";

						if (txtText.Text.Trim() != "")
						{
							objInfo.CHECK_AMOUNT =  Double.Parse(txtText.Text);
						}

						DropDownList ddl = (DropDownList)item.FindControl("cmbPAYMENT_MODE");

						objInfo.PAYMENT_MODE = int.Parse(ddl.SelectedValue.ToString().Trim());

						
						if(dgAgencyCheck.DataKeys[item.ItemIndex]!=DBNull.Value && dgAgencyCheck.DataKeys[item.ItemIndex].ToString().Length>0)
						{	//UPDATE
							objInfo.CHECK_ID = Convert.ToInt32(dgAgencyCheck.DataKeys[item.ItemIndex].ToString());
							alCheckInfoObjectsUpdate.Add(objInfo);

						}
						else//ADD NEW
							alCheckInfoObjects.Add(objInfo);
										
						
					}
				}

			}//FOR EACH
			objCheck = new ClsChecks();
			int intRetVal = objCheck.SaveTempDataAgen(alCheckInfoObjects,alCheckInfoObjectsUpdate);
			return intRetVal;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal = SaveChecksTemp();
				if( intRetVal > 0 )			
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
					if(hidAGENCY_ID.Value == "")
						hidAGENCY_ID.Value = "0";
					if(hidMonth.Value!="" && hidYear.Value!="")
						BindGrid(int.Parse(hidMonth.Value.ToString()),int.Parse(hidYear.Value.ToString().Trim()),int.Parse(hidAGENCY_ID.Value.ToString().Trim()),CmbCommType.SelectedValue.ToString().Trim());
					else
						BindGrid(0,0,0,CmbCommType.SelectedValue.ToString().Trim());

				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
				}
				lblMessage.Visible = true;

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				ExceptionManager.Publish(ex);
			}
			finally
			{
				
			}
		}

		private void btnCreateChecks_Click(object sender, System.EventArgs e)
		{		
			int intRetVal=1;
			ArrayList alCheckInfoObjects= new ArrayList();
			ArrayList OPEN_ITEM_IDs= new ArrayList();
			ArrayList alCheckIds = new ArrayList();
			try
			{
				SaveChecksTemp();
				if(hidAGENCY_ID.Value == "")
					hidAGENCY_ID.Value = "0";
				if(hidMonth.Value!="" && hidYear.Value!="")
					BindGrid(int.Parse(hidMonth.Value.ToString()),int.Parse(hidYear.Value.ToString().Trim()),int.Parse(hidAGENCY_ID.Value.ToString().Trim()),CmbCommType.SelectedValue.ToString().Trim());
				else
					BindGrid(0,0,0,CmbCommType.SelectedValue.ToString().Trim());

				foreach(DataGridItem item in dgAgencyCheck.Items)
				{
					
					CheckBox objCheckBox=null;
					objCheckBox = (CheckBox)item.FindControl("chkAgenCheck");
					if(objCheckBox.Checked)
					{
						if (dgAgencyCheck.DataKeys[item.ItemIndex].ToString() !="")
						{
							Label lblStatus = (Label) item.FindControl("lblStatus");
							if(lblStatus.Text.ToUpper() != "Distributed".ToUpper())
								continue;

							ClsChecksInfo objInfo = new ClsChecksInfo();
							objInfo.TMP_CHECK_ID = int.Parse(dgAgencyCheck.DataKeys[item.ItemIndex].ToString());																
							objInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue.ToString());
							objInfo.CHECK_TYPE = ClsChecks.CHECK_TYPES.AGENCY_COMMISSION_CHECKS;
							objInfo.CHECK_DATE = DateTime.Now;
							objInfo.CREATED_BY = int.Parse(GetUserId());
							objInfo.CREATED_DATETIME = DateTime.Now;
							objInfo.MODIFIED_BY = int.Parse(GetUserId());
							objInfo.LAST_UPDATED_DATETIME = DateTime.Now;

							objInfo.MONTH = int.Parse(item.Cells[4].Text.ToString()); 
							objInfo.YEAR = int.Parse(item.Cells[5].Text.ToString());
							objInfo.COMM_TYPE = CmbCommType.SelectedValue.Trim(); 

							TextBox txtText;
							txtText = (TextBox)item.FindControl("txtPayment_Amt");
									
							if (txtText.Text.Trim() != "")
							{
								objInfo.CHECK_AMOUNT = double.Parse(txtText.Text);
							}

							int agencyID = int.Parse(item.Cells[1].Text.ToString());
							objInfo.PAYEE_ENTITY_ID = agencyID;

							objInfo.PAYMENT_MODE = int.Parse(item.Cells[10].Text.ToString());
								
							alCheckInfoObjects.Add(objInfo);
							alCheckIds.Add(dgAgencyCheck.DataKeys[item.ItemIndex]);
						}											
					}
					
				}//FOR EACH
				intRetVal=new ClsChecks().AddTempCommChecksToActual(alCheckInfoObjects,alCheckIds);//id of ACT_ACCOUNTS_POSTING_DETAILS
				if( intRetVal > 0 )			
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage("G","884");
					if(hidAGENCY_ID.Value == "")
						hidAGENCY_ID.Value = "0";
					if(hidMonth.Value!="" && hidYear.Value!="")
						BindGrid(int.Parse(hidMonth.Value.ToString()),int.Parse(hidYear.Value.ToString().Trim()),int.Parse(hidAGENCY_ID.Value.ToString().Trim()),CmbCommType.SelectedValue.ToString().Trim());
					else
						BindGrid(0,0,0,CmbCommType.SelectedValue.ToString().Trim());
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
				}
			}
			catch(Exception ex)					
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"3") + " - " + ex.Message + " Try again!";
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
			}		
						
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			/*bool blnSave ;
			string checkID = "0";
			foreach (DataGridItem item in dgAgencyCheck.Items)
			{
				blnSave = ((CheckBox)(item.Cells[0].FindControl("chkAgenCheck"))).Checked;
				
				if(blnSave)
				{
					if(dgAgencyCheck.DataKeys[item.ItemIndex].ToString() == "")
						checkID = "0";
					else
						checkID = dgAgencyCheck.DataKeys[item.ItemIndex].ToString(); 
					#region Delete Line Items
					ClsChecks objChecks = new ClsChecks();

					try
					{
						if (objChecks.Delete(checkID,int.Parse(GetUserId())) > 0)
						{
							//Deleted successfully
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "127");
							
						}
						else
						{
							//Unable to deleted
							lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "128");
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
						objChecks.Dispose();
					}
					#endregion
				
				}

			}*/
			ArrayList alCheckIds = new ArrayList();
			try
			{
				foreach(DataGridItem dgi in dgAgencyCheck.Items)
				{
							
					CheckBox objCheckBox=null;
					objCheckBox = (CheckBox)dgi.FindControl("chkAgenCheck");
					if(objCheckBox.Checked)
					{
						if (dgAgencyCheck.DataKeys[dgi.ItemIndex].ToString() !="")
						{
							alCheckIds.Add(dgAgencyCheck.DataKeys[dgi.ItemIndex]);
						}											
					}
					
				}
				int intRetVal=0;
				if(alCheckIds.Count>0)
				{
					ClsChecks objCheck = new ClsChecks();
					intRetVal=objCheck.DeleteSelectedChecksFromTempCheckTable(alCheckIds,Convert.ToInt32(GetUserId()));
				}
				if( intRetVal > 0 )			
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"127");
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1022");
				}
			}
			catch(Exception ex)					
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"333") + " - " + ex.Message + " Try again!";
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(hidAGENCY_ID.Value == "")
					hidAGENCY_ID.Value = "0";
				if(hidMonth.Value!="" && hidYear.Value!="")
					BindGrid(int.Parse(hidMonth.Value.ToString()),int.Parse(hidYear.Value.ToString().Trim()),int.Parse(hidAGENCY_ID.Value.ToString().Trim()),CmbCommType.SelectedValue.ToString().Trim());
				else
					BindGrid(0,0,0,CmbCommType.SelectedValue.ToString().Trim());
			}	

		


		}
	}
}
