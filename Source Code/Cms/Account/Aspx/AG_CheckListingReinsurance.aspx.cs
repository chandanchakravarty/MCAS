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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AG_CheckListingReinsurance.
	/// </summary>
	public class AG_CheckListingReinsurance : Cms.Account.AccountBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnCreateChecks;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label capACCOUNT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected System.Web.UI.WebControls.Label lblMessage;
		public int NoOfRows=0,SelectedRows=0;
		protected Cms.CmsWeb.Controls.CmsButton btnCreateChecksConfirm;
		protected Cms.CmsWeb.Controls.CmsButton btnSaveTemp;
		protected System.Web.UI.WebControls.DataGrid grdReconcileItems;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNoOfRowsDisplyed;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		private DataTable dsGridData=null;
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvACCOUNT_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
		}
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId  = "210_3";
			btnCreateChecks.CmsButtonClass		=	CmsButtonType.Execute;
			btnCreateChecks.PermissionString	=	gstrSecurityXML;

			btnCreateChecksConfirm.CmsButtonClass	=	CmsButtonType.Execute;
			btnCreateChecksConfirm.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass			=	CmsButtonType.Delete;
			btnDelete.PermissionString			=	gstrSecurityXML;

			btnSaveTemp.CmsButtonClass			=	CmsButtonType.Write;
			btnSaveTemp.PermissionString		=	gstrSecurityXML;

			btnBack.CmsButtonClass				=	CmsButtonType.Execute;
			btnBack.PermissionString			=	gstrSecurityXML;
			
			btnBack.Attributes.Add("onclick","javascript:return DisableValidators();");
			btnCreateChecks.Attributes.Add("onclick","javascript:if(fnCheckForNegAmt()==false)return false;");
			btnCreateChecksConfirm.Attributes.Add("onclick","javascript:if(fnCheckForNegAmt()==false)return false;");
			//hlkFromDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtFromDate,document.Form1.txtFromDate)");
			//	hlkToDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtToDate,document.Form1.txtToDate)");
			if(!IsPostBack)
			{
				if(Request.QueryString["TypeID"].ToString().Equals("6"))//Re-Insurance
					ClsGlAccounts.GetAccountsInDropDown(cmbACCOUNT_ID); //Show all type of Accounts in case of Re-Insurance( As Discussed with Rajan sir)
				else if(Request.QueryString["TypeID"].ToString().Equals("5"))//Claims
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11200);
				else
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,0);//General
				BindGrid();
				SetBankAccountDD();
				
			}		
			SetErrorMessages();
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
			this.btnCreateChecks.Click += new System.EventHandler(this.btnCreateChecks_Click);
			this.btnCreateChecksConfirm.Click += new System.EventHandler(this.btnCreateChecksConfirm_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			this.btnSaveTemp.Click += new System.EventHandler(this.btnSaveTemp_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void SetBankAccountDD()
		{
			int intFiscalID = 0;
			intFiscalID = ClsGeneralLedger.GetFiscalID();

			string strXML = ClsGeneralLedger.GetXmlForPageControls_Bnk_AC_Mapping("1",intFiscalID.ToString().Trim());
			strXML = strXML.Replace("\r\n","");
			strXML = strXML.Replace("<NewDataSet>","");
			strXML = strXML.Replace("</NewDataSet>","");	
			
			XmlDocument  objXmlDocument  = new XmlDocument();			
			objXmlDocument.LoadXml(strXML);
			XmlNode Node = objXmlDocument.SelectSingleNode("Table/BNK_REINSURANCE_DEFAULT_AC");
			if(Node != null)
			{
				try
				{
					XmlNodeList objXmlNode = objXmlDocument.GetElementsByTagName("BNK_REINSURANCE_DEFAULT_AC");
					string strBNK_REINSURANCE_DEFAULT_AC =  objXmlNode.Item(0).InnerText;
					cmbACCOUNT_ID.SelectedValue=(strBNK_REINSURANCE_DEFAULT_AC);	
				}
				catch
				{
					cmbACCOUNT_ID.SelectedIndex = -1;
				}
			}
		}

		private void BindGrid()
		{
			DataTable objTable = ClsChecks.GetTempCheckData(int.Parse(GetUserId()),int.Parse(ClsChecks.CHECK_TYPES.REINSURANCE_PREMIUM_CHECKS)).Tables[0];
			dsGridData = objTable;
			SelectedRows = objTable.Rows.Count;
			hidNoOfRowsDisplyed.Value = SelectedRows.ToString();
			if(objTable.Rows.Count<15)
				for(int i=objTable.Rows.Count;i<15;i++)
					objTable.Rows.Add(objTable.NewRow());
			grdReconcileItems.DataSource =  objTable;
			grdReconcileItems.DataBind();
			NoOfRows = 15;
			/*try
			{		
				cmbACCOUNT_ID.SelectedValue = objTable.Rows[0]["ACCOUNT_ID"].ToString();
			}
			catch(Exception ee)
			{
				throw(ee);
			}*/
		}
		private void btnCreateChecks_Click(object sender, System.EventArgs e)
		{
			
			int intRetVal=1;
			ArrayList alCheckInfoObjects= new ArrayList();
			ArrayList OPEN_ITEM_IDs= new ArrayList();
			ArrayList alCheckIds = new ArrayList();
			try
			{
				 SaveChecks();
				foreach(DataGridItem dgi in grdReconcileItems.Items)
				{
				
					CheckBox objCheckBox=null;
					objCheckBox = (CheckBox)dgi.FindControl("chkSelect");
					if(objCheckBox.Checked)
					{
						if (grdReconcileItems.DataKeys[dgi.ItemIndex].ToString() !="")
						{
							ClsChecksInfo objInfo = new ClsChecksInfo();
																
							objInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue.ToString());
							objInfo.CHECK_NOTE = "Auto Generated Reinsurance Premium Checks";
							objInfo.CHECK_TYPE = ClsChecks.CHECK_TYPES.REINSURANCE_PREMIUM_CHECKS;
							objInfo.CHECK_DATE = DateTime.Now;
							objInfo.CREATED_BY = int.Parse(GetUserId());
							objInfo.CREATED_DATETIME = DateTime.Now;
							objInfo.MODIFIED_BY = int.Parse(GetUserId());
							objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
									
							TextBox txtText;
							DropDownList cmbCombo;
							cmbCombo = (DropDownList)dgi.FindControl("cmbReinsuranceCompany");
							if (cmbCombo.SelectedIndex>0)
							{
								objInfo.PAYEE_ENTITY_ID = Convert.ToInt32(cmbCombo.SelectedValue);
								objInfo.PAYEE_ENTITY_NAME = cmbCombo.SelectedItem.Text;
								objInfo.PAYEE_ENTITY_TYPE = "RP";
							}
						
							txtText = (TextBox)dgi.FindControl("txtPremiumAmount");
							if (txtText.Text.Trim() != "")
							{
								objInfo.CHECK_AMOUNT = double.Parse(txtText.Text);
							}
							
								
								
							alCheckInfoObjects.Add(objInfo);
							alCheckIds.Add(grdReconcileItems.DataKeys[dgi.ItemIndex]);
						}											
					}
					
				}//for each
				Boolean blnChkDistribution = new ClsChecks().ConfirmDistributionForTempChecks(alCheckIds);
				if(blnChkDistribution)
				{
					intRetVal=new ClsChecks().AddTempChecksToActual(alCheckInfoObjects,alCheckIds);//id of ACT_ACCOUNTS_POSTING_DETAILS
					if( intRetVal > 0 )			
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"10");
						BindGrid();
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"6");
					}
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
				}
			}
			catch(Exception ex)					
			{
				lblMessage.Text	=	 ex.Message + " Try again!";
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
			}		
						
		}
		DataTable renisuranceDropDownItems=null;
		private void grdReconcileItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
					
			{
				TextBox objTxt = (TextBox)e.Item.FindControl("txtPremiumAmount");
				objTxt.Attributes.Add("onblur","javascript:FormatAmount(this);EnableSave('" + (e.Item.ItemIndex + 2) + "');");

				//Added to Check on Combo and text values and Set Save Button
				DropDownList combo = (DropDownList)e.Item.FindControl("cmbReinsuranceCompany");
				combo.Attributes.Add("onchange","javascript:EnableSave('" + (e.Item.ItemIndex + 2) + "');");


				if(grdReconcileItems.DataKeys[e.Item.ItemIndex]!=DBNull.Value)
				{
					if(grdReconcileItems.DataKeys[e.Item.ItemIndex].ToString().Length>0)
					{
						CheckBox objchk = (CheckBox)e.Item.FindControl("chkSelect");
						objchk.Checked  = true;
						HyperLink objlnk = (HyperLink)e.Item.FindControl("hlkOpenDistibute");
						objlnk.Visible  = true;
						

						/*if(dsGridData.Rows[e.Item.ItemIndex]["DistributedAmount"]!=DBNull.Value)
						{
							if(Convert.ToDouble(dsGridData.Rows[e.Item.ItemIndex]["DistributedAmount"])==Convert.ToDouble(objTxt.Text))
								objlnk.Text = "Edit";
						}*/

						//objlnk.Attributes.Add();
						string queryString = "GROUP_ID="+grdReconcileItems.DataKeys[e.Item.ItemIndex]+"&GROUP_TYPE=CHQ&DISTRIBUTION_AMOUNT="+objTxt.Text+"&SUB_CALLED_FROM=REINS";
						objlnk.NavigateUrl = "javascript:OpenDistributePopup('"+queryString+"')";
					}
				}
				RegularExpressionValidator revValidator	= null;													
				revValidator = (RegularExpressionValidator) e.Item.FindControl("revPremiumAmount");
				revValidator.ErrorMessage = ClsMessages.FetchGeneralMessage("116");
				revValidator.ValidationExpression = aRegExpCurrencyformat;

				RequiredFieldValidator rfvValidator = null;

																		
				rfvValidator=(RequiredFieldValidator) e.Item.FindControl("rfvPremiumAmount");
				rfvValidator.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
								
				rfvValidator=(RequiredFieldValidator) e.Item.FindControl("rfvReinsuranceCompany");
				rfvValidator.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");

					
				//DropDownList combo = (DropDownList)e.Item.FindControl("cmbReinsuranceCompany");
				if(renisuranceDropDownItems==null)	
					renisuranceDropDownItems = ClsChecks.GetReinsuranceCompanies();
				combo.DataSource = renisuranceDropDownItems;
				combo.DataTextField = "Rein_Comapany_NAME";
				combo.DataValueField = "Rein_Comapany_ID";
				combo.DataBind();
				combo.Items.Insert(0,"");
				if ( DataBinder.Eval(e.Item.DataItem,"PAYEE_ENTITY_ID") != System.DBNull.Value )
				{
						
					int Rein_Comapany_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"PAYEE_ENTITY_ID"));
					ListItem item = combo.Items.FindByValue(Convert.ToString(Rein_Comapany_ID));
						
					if ( item != null )
					{
						combo.SelectedIndex = combo.Items.IndexOf(item);
					}

				}

			}
				//	catch(Exception ee){}
			}
		}

		private void btnCreateChecksConfirm_Click(object sender, System.EventArgs e)
		{
			
			bool IsConfirmed=false;
			ArrayList alCheckIds = new ArrayList();
			try
			{
				SaveChecks();
				BindGrid();
				foreach(DataGridItem dgi in grdReconcileItems.Items)
				{
				
					CheckBox objCheckBox=null;
					objCheckBox = (CheckBox)dgi.FindControl("chkSelect");
					if(objCheckBox.Checked)
					{
						if (grdReconcileItems.DataKeys[dgi.ItemIndex].ToString() !="")
						{
							alCheckIds.Add(grdReconcileItems.DataKeys[dgi.ItemIndex]);
						}											
					}
					
				}//for each
				IsConfirmed=new ClsChecks().ConfirmDistributionForTempChecks(alCheckIds);
				if(IsConfirmed)			
				{
					btnCreateChecks_Click(sender,e);
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
				}
			}
			catch(Exception ex)					
			{
				lblMessage.Text	=	ex.Message + " Try again!";
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
			}						
		}

		private void btnSaveTemp_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal = SaveChecks();
				if( intRetVal > 0 )			
				{
					//lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
					BindGrid();
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
				}
				lblMessage.Visible = true;

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				//hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				
			}
		}
		private int SaveChecks()
		{
			ClsChecks objCheck = null;
			ArrayList alCheckInfoObjects = new ArrayList();
			ArrayList alCheckInfoObjectsUpdate = new ArrayList();
			ArrayList alHyperLinks = new ArrayList();
			ArrayList alCheckIds = new ArrayList();
				
			foreach(DataGridItem dgi in grdReconcileItems.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					//Save on the basis of Contact and Amount
					TextBox premiumAmtTxt = (TextBox)dgi.FindControl("txtPremiumAmount");
					DropDownList contactCombo = (DropDownList)dgi.FindControl("cmbReinsuranceCompany");
					if(premiumAmtTxt.Text!="" && contactCombo.SelectedValue!="")
					{
						//CheckBox chkSelect = (CheckBox)dgi.FindControl("chkSelect");
						//if(chkSelect.Checked)
						//{
						ClsChecksInfo objInfo = new ClsChecksInfo();
																
						objInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue.ToString());
						objInfo.CHECK_NOTE = "Auto Generated Reinsurance Premium Checks";
						objInfo.CHECK_TYPE = ClsChecks.CHECK_TYPES.REINSURANCE_PREMIUM_CHECKS;
						objInfo.CHECK_DATE = DateTime.Now;
						objInfo.CREATED_BY = int.Parse(GetUserId());
						objInfo.CREATED_DATETIME = DateTime.Now;
						objInfo.MODIFIED_BY = int.Parse(GetUserId());
						objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
										
						alHyperLinks.Add(dgi.FindControl("hlkOpenDistibute"));

						TextBox txtText;
						DropDownList cmbCombo;
					
						cmbCombo = (DropDownList)dgi.FindControl("cmbReinsuranceCompany");
						if (cmbCombo.SelectedIndex>0)
						{
							objInfo.PAYEE_ENTITY_ID = Convert.ToInt32(cmbCombo.SelectedValue);
							objInfo.PAYEE_ENTITY_NAME = cmbCombo.SelectedItem.Text;
							objInfo.PAYEE_ENTITY_TYPE = "RP";
						}
						
						txtText = (TextBox)dgi.FindControl("txtPremiumAmount");
						if (txtText.Text.Trim() != "")
						{
							objInfo.CHECK_AMOUNT = double.Parse(txtText.Text);
						}
															
									
						if(grdReconcileItems.DataKeys[dgi.ItemIndex]!=DBNull.Value && grdReconcileItems.DataKeys[dgi.ItemIndex].ToString().Length>0)
						{//update
							objInfo.CHECK_ID = Convert.ToInt32(grdReconcileItems.DataKeys[dgi.ItemIndex].ToString());
							alCheckInfoObjectsUpdate.Add(objInfo);
							lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

						}
						else//add new
						{
							alCheckInfoObjects.Add(objInfo);
							lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");

						}
					}
										
						
					//}
				}

			}//for each
			objCheck = new ClsChecks();
			int intRetVal = objCheck.SaveTempData(alCheckInfoObjects,alCheckInfoObjectsUpdate);
			return intRetVal;
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			ArrayList alCheckIds = new ArrayList();
			try
			{
				foreach(DataGridItem dgi in grdReconcileItems.Items)
				{
							
				CheckBox objCheckBox=null;
				objCheckBox = (CheckBox)dgi.FindControl("chkSelect");
				if(objCheckBox.Checked)
				{
					if (grdReconcileItems.DataKeys[dgi.ItemIndex].ToString() !="")
					{
					alCheckIds.Add(grdReconcileItems.DataKeys[dgi.ItemIndex]);
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
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
					BindGrid();
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
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
			}					
		}

		private void btnBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("AG_CheckTypeSelect.aspx");
		}
		
		}
		
	}	
	
