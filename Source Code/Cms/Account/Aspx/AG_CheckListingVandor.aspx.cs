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
	public class AG_CheckListingVandor : Cms.Account.AccountBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnCreateChecks;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label capACCOUNT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected System.Web.UI.WebControls.Label lblMessage;
		public int NoOfRows=0,SelectedRows=0;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.DataGrid grdReconcileItems;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNoOfRowsDisplyed;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		private DataTable dsGridData=null;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidcmbpaymentmodecmbpaymentmode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAllowEFT;
				
		//protected System.Web.UI.HtmlControls.HtmlSelect cmbReinsuranceCompany; 
		bool flag;
		private const string GL_ID="1";
		private const string LIABILITYTYPE = "2";
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
			
			base.ScreenId  = "210_4";
			btnCreateChecks.CmsButtonClass		=	CmsButtonType.Execute;
			btnCreateChecks.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass			=	CmsButtonType.Delete;
			btnDelete.PermissionString			=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnBack.CmsButtonClass				=	CmsButtonType.Execute;
			btnBack.PermissionString			=	gstrSecurityXML;

			btnBack.Attributes.Add("onclick","javascript:return DisableValidators();");	
			btnCreateChecks.Attributes.Add("onclick","javascript:if(fnCheckForNegAmt()==false)return false;");
			if(!IsPostBack)
			{

				if(Request.QueryString["TypeID"].ToString().Equals("6"))//Re-Insurance
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11199);
				else if(Request.QueryString["TypeID"].ToString().Equals("5"))//Claims
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11200);
				else
				{
					try
					{
						//ClsGlAccounts.GetAccountsInDropDown(cmbACCOUNT_ID,GL_ID,LIABILITYTYPE);
						ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,0);//General
						SetBankAccountDD();//To be Set ..from Posting Interface
					}
					catch
					{
						cmbACCOUNT_ID.SelectedIndex = -1;
					}
				}
				BindGrid();
				
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion 

		private void BindGrid()
		{
			DataTable objTable = ClsChecks.GetTempCheckData(int.Parse(GetUserId()),int.Parse(ClsChecks.CHECK_TYPES.VENDOR_CHECKS)).Tables[0];
			dsGridData = objTable;
			SelectedRows = objTable.Rows.Count;
			hidNoOfRowsDisplyed.Value = SelectedRows.ToString();
			if(objTable.Rows.Count<15)
				for(int i=objTable.Rows.Count;i<15;i++)
					objTable.Rows.Add(objTable.NewRow());
			 grdReconcileItems.DataSource =  objTable;
			grdReconcileItems.DataBind();

			NoOfRows = 15;
				
			try
			{
				if(objTable.Rows[0]["ACCOUNT_ID"]!=null && objTable.Rows[0]["ACCOUNT_ID"].ToString()!="" && objTable.Rows[0]["ACCOUNT_ID"].ToString()!="0")
					cmbACCOUNT_ID.SelectedValue = objTable.Rows[0]["ACCOUNT_ID"].ToString();
			}
			catch
			{
				cmbACCOUNT_ID.SelectedIndex = -1;
			}
				
		}
		#region Set Account ID
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
			XmlNode Node = objXmlDocument.SelectSingleNode("Table/BNK_VEN_CHK_DEFAULT_AC");
			if(Node != null)
			{
				XmlNodeList objXmlNode = objXmlDocument.GetElementsByTagName("BNK_VEN_CHK_DEFAULT_AC");
				string strBNK_VEN_CHK_DEFAULT_AC  =  objXmlNode.Item(0).InnerText;
				cmbACCOUNT_ID.SelectedValue=(strBNK_VEN_CHK_DEFAULT_AC);	
			}
		}
		#endregion
		private void btnCreateChecks_Click(object sender, System.EventArgs e)
		{
			
			int intRetVal=1;
			ArrayList alCheckInfoObjects= new ArrayList();
			ArrayList OPEN_ITEM_IDs= new ArrayList();
			ArrayList alCheckIds = new ArrayList();
			try
			{
				int intSuccessCode= SaveChecks();
//				if( intSuccessCode> 0 )			
//				{
//					BindGrid();
//				}
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
							TextBox txtNote = (TextBox)dgi.FindControl("txtNote");
							if(txtNote.Text.Trim() == "")
							{
								objInfo.CHECK_NOTE = "Auto Generated Vendor Checks";
							}
							else
							{
								objInfo.CHECK_NOTE = txtNote.Text;
							}
							objInfo.CHECK_TYPE = ClsChecks.CHECK_TYPES.VENDOR_CHECKS;
							objInfo.CHECK_DATE = DateTime.Now;
							objInfo.CREATED_BY = int.Parse(GetUserId());
							objInfo.CREATED_DATETIME = DateTime.Now;
							objInfo.MODIFIED_BY = int.Parse(GetUserId());
							objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
							HtmlInputHidden hidTEMP_CHECK_ID = (HtmlInputHidden)dgi.FindControl("hidTEMP_CHECK_ID");
							if(hidTEMP_CHECK_ID.Value.Trim() != "")
							{
								objInfo.TMP_CHECK_ID = Convert.ToInt32(hidTEMP_CHECK_ID.Value.Trim());
							}
									
							TextBox txtText;
							//DropDownList cmbCombo;
							//cmbCombo = (DropDownList)dgi.FindControl("cmbReinsuranceCompany");
							System.Web.UI.HtmlControls.HtmlSelect cmbCombo;
							cmbCombo = (System.Web.UI.HtmlControls.HtmlSelect)dgi.FindControl("cmbReinsuranceCompany");
							if (cmbCombo.SelectedIndex>0)
							{
								string[] VendorValue = cmbCombo.Value.Split("~".ToCharArray());
								objInfo.PAYEE_ENTITY_ID = Convert.ToInt32(VendorValue[0]);
								objInfo.PAYEE_ENTITY_NAME = cmbCombo.Items[cmbCombo.SelectedIndex].Text;
								objInfo.PAYEE_ENTITY_TYPE = "RP";
							}
						
							txtText = (TextBox)dgi.FindControl("txtPremiumAmount");
							if (txtText.Text.Trim() != "")
							{
								objInfo.CHECK_AMOUNT = double.Parse(txtText.Text);
							}
							DropDownList ddl = ((DropDownList)(dgi.FindControl("cmbPaymentMode")));	
							objInfo.PAYMENT_MODE = int.Parse(ddl.SelectedValue.ToString().Trim());
			
							
							int CheckID = int.Parse(grdReconcileItems.DataKeys[dgi.ItemIndex].ToString());		
							int Value = new ClsVendorInvoices().ChkForPayAmtAndCheckAmt(CheckID,objInfo.PAYEE_ENTITY_ID);
							if(Value > 0)
							{
								alCheckInfoObjects.Add(objInfo);
								alCheckIds.Add(grdReconcileItems.DataKeys[dgi.ItemIndex]);
								flag = true;
							}
							else
							{
								lblMessage.Text			=	"Check amount for some check(s) is not equal to the Pending Vendor Invoices Total";
								flag = false;
								break;
							}
						}											
					}
					
				}//for each
				if(flag)	
				{
					intRetVal=new ClsChecks().AddTempVendorChecksToActual(alCheckInfoObjects,alCheckIds);//id of ACT_ACCOUNTS_POSTING_DETAILS
					if( intRetVal > 0 )			
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
						BindGrid();
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
					}
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
		DataTable renisuranceDropDownItems=null;

		private void grdReconcileItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
					
				HtmlInputHidden hidTEMP_CHECK_ID = (HtmlInputHidden)e.Item.FindControl("hidTEMP_CHECK_ID");  
				HtmlImage imgVendorInvoices = (HtmlImage)e.Item.FindControl("imgVendorInvoices");
				//DropDownList cmbReinsuranceCompany = (DropDownList)e.Item.FindControl("cmbReinsuranceCompany");
                 System.Web.UI.HtmlControls.HtmlSelect cmbReinsuranceCompany = (System.Web.UI.HtmlControls.HtmlSelect)e.Item.FindControl("cmbReinsuranceCompany"); 
				TextBox txtPremiumAmount = (TextBox)e.Item.FindControl("txtPremiumAmount");
				txtPremiumAmount.Attributes.Add("onblur","javascript:FormatAmount(this);");
				if(grdReconcileItems.DataKeys[e.Item.ItemIndex]!=DBNull.Value)
				{
					if(grdReconcileItems.DataKeys[e.Item.ItemIndex].ToString().Length>0)
					{
						CheckBox objchk = (CheckBox)e.Item.FindControl("chkSelect");
						objchk.Checked  = true;
					}
				}

				
				if(hidTEMP_CHECK_ID.Value == "")
				{
					imgVendorInvoices.Visible=false;
					txtPremiumAmount.Visible=false;
					//cmbReinsuranceCompany.Enabled = true;
					cmbReinsuranceCompany.Disabled  = false;
				}
				else
				{
					imgVendorInvoices.Visible=true;
					txtPremiumAmount.Visible=true;
					//cmbReinsuranceCompany.Enabled = false;
					 cmbReinsuranceCompany.Disabled  = true;
				}

				RequiredFieldValidator rfvValidator = null;
				rfvValidator=(RequiredFieldValidator) e.Item.FindControl("rfvReinsuranceCompany");
				rfvValidator.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "8");

				//Change Web Control to HTML dropdown
				System.Web.UI.HtmlControls.HtmlSelect combo = (System.Web.UI.HtmlControls.HtmlSelect)e.Item.FindControl("cmbReinsuranceCompany");
				//DropDownList combo = (DropDownList)e.Item.FindControl("cmbReinsuranceCompany");
				if(renisuranceDropDownItems==null)	
					renisuranceDropDownItems = clsVendor.GetVendorNames();
				/*combo.DataSource = renisuranceDropDownItems;
				combo.DataTextField = "COMPANY_NAME";
				combo.DataValueField = "COMPANY_NAME_DATA";
				combo.DataBind();
				combo.Items.Insert(0,"");*/
				//Common Fxn'
				BindDropDown(combo,renisuranceDropDownItems);
				


				Label lblVendor = (Label)e.Item.FindControl("lblVENDOR_ADDRESS");
				//DropDownList cmbVendor = (DropDownList)e.Item.FindControl("cmbReinsuranceCompany");
				System.Web.UI.HtmlControls.HtmlSelect cmbVendor = (System.Web.UI.HtmlControls.HtmlSelect)e.Item.FindControl("cmbReinsuranceCompany");
				cmbVendor.Attributes.Add("onchange","JavaScript:ShowVendorAddress('" + lblVendor.ClientID + "','" + cmbVendor.ClientID + "');EnableSave('" + (e.Item.ItemIndex + 2) + "');");
				int Rein_Comapany_ID;
				if ( DataBinder.Eval(e.Item.DataItem,"PAYEE_ENTITY_ID") != System.DBNull.Value )
				{
					Rein_Comapany_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"PAYEE_ENTITY_ID"));
					Cms.BusinessLayer.BlCommon.ClsCommon.SetComboValueForConcatenatedString(combo,Rein_Comapany_ID.ToString(),'~',0);						
				}
				//string VendorData=cmbVendor.SelectedItem.Value; 
				 string VendorData=cmbVendor.Value; 
				if (VendorData!="")
				{
					string[]  VendorArray = VendorData.Split('~');
					if(VendorArray.GetLength(0)!=0)
					{
						// Assign corresponding Vendor Address into the Label field
						lblVendor.Text = VendorArray[2] + ' ' + VendorArray[3] + ' ' + VendorArray[4] + ' ' + VendorArray[5];
					}	
				}
				string	ChkIDForQueryStr = grdReconcileItems.DataKeys[e.Item.ItemIndex].ToString();
				HtmlImage imgVendorInv = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgVendorInvoices");
				if(imgVendorInv !=null)
					imgVendorInv.Attributes.Add("onclick","OpenVendorInvoices('" + combo.ClientID + "','" + ChkIDForQueryStr + "')");

				DropDownList ddl = ((DropDownList)(e.Item.FindControl("cmbpaymentmode")));
				ddl.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTD");
				ddl.DataTextField="LookupDesc"; 
				ddl.DataValueField="LookupID";
				ddl.DataBind();
				ddl.Items.Remove(new ListItem("Manual Check","11979"));
                ddl.Attributes.Add("onChange","ValidateEFTValue('" + ddl.ClientID + "')");
				int PayID;
				if ( DataBinder.Eval(e.Item.DataItem,"PAYMENT_MODE") != System.DBNull.Value )
				{
					PayID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"PAYMENT_MODE"));
					ddl.SelectedValue = PayID.ToString();
				}
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal = SaveChecks();
				if( intRetVal > 0 )			
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
					BindGrid();
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"10");
				}
				lblMessage.Visible = true;

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
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
			ArrayList alCheckIds = new ArrayList();
				
		
			foreach(DataGridItem dgi in grdReconcileItems.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					/*Remove Check Box enable saving
					 * Save on the basis of Reins Contact 
					 * */
					//DropDownList cmbCombo;
					//cmbCombo = (DropDownList)dgi.FindControl("cmbReinsuranceCompany");
					System.Web.UI.HtmlControls.HtmlSelect cmbCombo = (System.Web.UI.HtmlControls.HtmlSelect)dgi.FindControl("cmbReinsuranceCompany");
					//CheckBox chkSelect = (CheckBox)dgi.FindControl("chkSelect");
					//if(chkSelect.Checked)
					//{
					//if(cmbCombo.SelectedValue!="")
				    if(cmbCombo.Value!="")
 	                {

						ClsChecksInfo objInfo = new ClsChecksInfo();
																
						objInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue.ToString());
						
						objInfo.CHECK_TYPE = ClsChecks.CHECK_TYPES.VENDOR_CHECKS;
						objInfo.CHECK_DATE = DateTime.Now;
						objInfo.CREATED_BY = int.Parse(GetUserId());
						objInfo.CREATED_DATETIME = DateTime.Now;
						objInfo.MODIFIED_BY = int.Parse(GetUserId());
						objInfo.LAST_UPDATED_DATETIME = DateTime.Now;

						TextBox txtText;
						if (cmbCombo.SelectedIndex>0)
						{
							//string[] VendorValue = cmbCombo.SelectedValue.Split("~".ToCharArray());
							string[] VendorValue = cmbCombo.Value.Split("~".ToCharArray());
							objInfo.PAYEE_ENTITY_ID = Convert.ToInt32(VendorValue[0]);
							//objInfo.PAYEE_ENTITY_NAME = cmbCombo.SelectedItem.Text;
							objInfo.PAYEE_ENTITY_NAME = cmbCombo.Items[cmbCombo.SelectedIndex].Text;
							objInfo.PAYEE_ENTITY_TYPE = "RP";
						}
						txtText = (TextBox)dgi.FindControl("txtNote");
						if (txtText.Text.Trim() != "")
						{
							objInfo.CHECK_NOTE  = txtText.Text;
						}
					
						txtText = (TextBox)dgi.FindControl("txtPremiumAmount");
						if (txtText.Text.Trim() != "")
						{
							objInfo.CHECK_AMOUNT =  Double.Parse(txtText.Text);
						}

						DropDownList ddl = ((DropDownList)(dgi.FindControl("cmbpaymentmode")));	
						objInfo.PAYMENT_MODE = int.Parse(ddl.SelectedValue.ToString().Trim());

						if(grdReconcileItems.DataKeys[dgi.ItemIndex]!=DBNull.Value && grdReconcileItems.DataKeys[dgi.ItemIndex].ToString().Length>0)
						{//update
							objInfo.CHECK_ID = Convert.ToInt32(grdReconcileItems.DataKeys[dgi.ItemIndex].ToString());
							alCheckInfoObjectsUpdate.Add(objInfo);

						}
						else//add new
							alCheckInfoObjects.Add(objInfo);
										
					}
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
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127");
					BindGrid();
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1022");
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
		//Display RED 
		public void BindDropDown(HtmlSelect ddlToBind,DataTable dtSource)
		{
            
			DataView dv = new DataView(dtSource);
			
			int i =0;
			foreach(DataRowView row in dv)
			{
						
				ddlToBind.Items.Add(new ListItem(row["COMPANY_NAME"].ToString(),row["COMPANY_NAME_DATA"].ToString()));


				string[] strCompanyData ;
				strCompanyData = row["COMPANY_NAME_DATA"].ToString().Split('~');
				if(strCompanyData[7] =="10963") 
				{			
					ddlToBind.Items[i].Attributes.Add ("Class","GrandFatheredRange");
					
				}   
			  
															  
				i++;
			}
			ddlToBind.Items.Insert(0,"");
		}

		
		
	}
		
}

	
