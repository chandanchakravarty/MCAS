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
using System.Text;

#endregion

namespace Cms.Account.Aspx
{
	public class DepositDetails : Cms.Account.AccountBase
	{
		#region Variable Declarations
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgDepositDetails;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGLAccountXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.Label lblTotalAmount;
		protected System.Web.UI.WebControls.Label lblTapeTotal;
		protected System.Web.UI.WebControls.TextBox txtTapeTotal;
		protected System.Web.UI.WebControls.RegularExpressionValidator rfvTapeTotal;
		protected Cms.CmsWeb.Controls.CmsButton btnConfirm;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnCompareTotal;
		protected Cms.CmsWeb.Controls.CmsButton btnPrevious;
		protected Cms.CmsWeb.Controls.CmsButton btnNext;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		public string URL;
		protected System.Web.UI.WebControls.Label lblDEPOSIT_NUM;
		protected System.Web.UI.WebControls.Label lblDepositNum;
		public int save;
		public bool blnConditionalDelete = false;
		protected System.Web.UI.WebControls.Label lblPageCurr;
		protected System.Web.UI.WebControls.Label lblPageLast;
		protected System.Web.UI.WebControls.Label lblPagingLeft;
		protected System.Web.UI.WebControls.Label lblPagingRight;
		public int CdLineItemID;
		private bool blnPagingFlag = true;
		DataSet ds = new DataSet();
		protected int currentPageIndex;
		#endregion
		
		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{

			btnSave.Attributes.Add("onclick","javascript:return DoValidationCheck();");
			btnDelete.Attributes.Add("onclick","javascript:return DeleteRows();");
			btnBack.Attributes.Add("onclick","javascript:return BackClick();");
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
			//Setting the screen id
			base.ScreenId = "187_1";
			rfvTapeTotal.ValidationExpression =  aRegExpDoublePositiveStartWithDecimal;
			rfvTapeTotal.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "965");
			txtTapeTotal.Attributes.Add("onblur","javascript:FormatAmount(this);");
			btnDelete.Enabled = false; //AT first if there are No records Disable the Button
			// Added By swarup on 12/12/2006
			foreach(DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					if ( dgDepositDetails.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
					{
						save=1;
					}
					else
					{
						save=0;
					}
				}
			}
			#region setting security permission
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass = CmsButtonType.Write;
			btnReset.PermissionString = gstrSecurityXML;

			btnDelete.CmsButtonClass = CmsButtonType.Delete;
			btnDelete.PermissionString = gstrSecurityXML;

			btnConfirm.CmsButtonClass = CmsButtonType.Write;
			btnConfirm.PermissionString = gstrSecurityXML;

			btnSave.CmsButtonClass = CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;

			btnCompareTotal.CmsButtonClass = CmsButtonType.Execute;
			btnCompareTotal.PermissionString = gstrSecurityXML;

			btnNext.CmsButtonClass = CmsButtonType.Read;
			btnNext.PermissionString = gstrSecurityXML;

			btnPrevious.CmsButtonClass = CmsButtonType.Read;
			btnPrevious.PermissionString = gstrSecurityXML;
			
			btnBack.CmsButtonClass = CmsButtonType.Read;
			btnBack.PermissionString = gstrSecurityXML;		
	
//			btnCompareTotal.Attributes.Add("onClick","javascript:CompareTotal();return false;");
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			#endregion
			

			if ( !Page.IsPostBack )
			{
				GetQueryString();

				hidGLAccountXML.Value = ClsDeposit.GetGLAccountXML(int.Parse(hidDEPOSIT_ID.Value));
				
				btnReset.Attributes.Add("onClick","javascript:return Reset();");

				ViewState["CurrentPageIndex"] = 1;
				if(Request.QueryString["DEPOSIT_TYPE"]!=null && Request.QueryString["DEPOSIT_TYPE"].ToString()!="")
					hidDEPOSIT_TYPE.Value = Request.QueryString["DEPOSIT_TYPE"].ToString();
				if ( Request.QueryString["DEPOSIT_ID"] != null )
				{
					hidDEPOSIT_ID.Value = Request.QueryString["DEPOSIT_ID"].ToString();
					
					try
					{
						BindGrid();
						setPagingText();
					}
					catch(Exception ex)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
						return;
					}
				}
			}
		}

		#endregion

		#region ItemDataBound / BindGrid()
		private void dgDepositDetails_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				int index = e.Item.ItemIndex + 2;	

				TextBox txtRECEIPT_AMOUNT = (TextBox)e.Item.FindControl("txtRECEIPT_AMOUNT");
				txtRECEIPT_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this);");

				//Check if any records Exists Enable the Delete Button 
				//Check it for Mandatory Field.
				if(txtRECEIPT_AMOUNT.Text!="")
					btnDelete.Enabled = true;
				
				TextBox txtPolicyNo = (TextBox)e.Item.FindControl("txtPolicyNo");				
				txtPolicyNo.Attributes.Add("value", Convert.ToString(DataBinder.Eval(e.Item.DataItem, "POLICY_NUMBER")));
				SetPolicyAttributes(txtPolicyNo, e.Item.ItemIndex + 2);
				
				TextBox txtCheckPolicyNo = (TextBox)e.Item.FindControl("txtCheckPolicyNo");
				//txtCheckPolicyNo.TextMode = TextBoxMode.Password;
				txtCheckPolicyNo.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "POLICY_NUMBER"));
				SetPolicyAttributes(txtCheckPolicyNo, e.Item.ItemIndex + 2);

				HtmlImage imgSelect = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgPOLICY_NO");
				if (imgSelect != null)
				{
					//SetPolicyTextBox
					imgSelect.Attributes.Add("onclick","OpenPolicyLookup('" + (e.Item.ItemIndex + 2).ToString() + "')" );
				}

				((Label)e.Item.FindControl("lblStatus")).Text = ((DataTable)dgDepositDetails.DataSource).Rows[e.Item.ItemIndex]["Status"].ToString();

				if (dgDepositDetails.DataKeys[e.Item.ItemIndex] == System.DBNull.Value )
				{
					((CheckBox)e.Item.FindControl("chkDelete")).Visible = false;
				}
				else
				{
					((CheckBox)e.Item.FindControl("chkDelete")).Visible = true;
				}
				
				SetValidators(e);
			}
		}

		private void BindGrid()
		{
			
			ClsDepositDetails objDeposit = new ClsDepositDetails();
			
			int pageSize = base.DepositDetailsPageSize;
			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);

			if (currentPageIndex == 1)
			{
				btnPrevious.Enabled = false;
			}
			else
			{
				btnPrevious.Enabled = true;
			}
		
			ds = objDeposit.GetDepositLineItems(int.Parse(hidDEPOSIT_ID.Value),hidDEPOSIT_TYPE.Value.Trim(),
				currentPageIndex,pageSize);

			// Make a clone of the Datatable consisting of records to be shown.
			DataTable dt = ds.Tables[1].Clone();
			string strExp;
			// Display records according to PageID. i.e; Different PageID in Different Grid  Page
			if(currentPageIndex <= ds.Tables[3].Rows.Count)
			{
				//Filter records from the fetched DS according to PageID
				strExp = ds.Tables[3].Rows[currentPageIndex-1]["PAGE_ID"].ToString();
				ViewState["DepositPageId"] = strExp;
				DataRow[] drPgID = ds.Tables[1].Select("PAGE_ID='"+ strExp +"'");
				foreach (DataRow drTmp in drPgID)
				{
					dt.ImportRow(drTmp);
				}
			}
			else
			{
				ViewState["DepositPageId"] = "";
			}
			// Add New rows
			int currentRowCount = dt.Rows.Count;
			if  ( currentRowCount < pageSize )
			{
				for ( int i = 0; i < pageSize - currentRowCount; i++ )
				{
					DataRow dr = dt.NewRow();
					dt.Rows.Add(dr);
				}
				btnNext.Enabled = true;
			}
			else
			{
				btnNext.Enabled = true;
			}
			// If Grid page is the last Page(Add Mode) then disable 'Next' button
			if(currentRowCount == 0)
			{
				btnNext.Enabled = false;
			}
			else
			{
				btnNext.Enabled = true;
			}
			
			dgDepositDetails.DataSource = dt;//ds.Tables[1];
			dgDepositDetails.DataBind();

		
			// fetch tape Total for a particular batch of records whose pageId is same and
			// are shown on the same page.
			DataTable dtTapeTotal = ds.Tables[3];
			string Tape_Total="0"; 
			if(dtTapeTotal != null)
			{
				if(currentPageIndex <= ds.Tables[3].Rows.Count)
				{
					Tape_Total = ds.Tables[3].Rows[currentPageIndex-1]["TAPE_TOTAL"].ToString();
				}
				if( Tape_Total=="0")
					txtTapeTotal.Text = "";
				else
					txtTapeTotal.Text=String.Format("{0:0,0.00}",Convert.ToDouble(Tape_Total));
				    //Added For Itrack Issue #6330.
				    lblTotalAmount.Text = String.Format("{0:0,0.00}",Convert.ToDouble(Tape_Total)); 
			}
		}

		#endregion

		#region QueryString(), SetPolicyAttributes(), SetValidators()

		private void GetQueryString()
		{
			hidDEPOSIT_ID.Value		= Request.Params["DEPOSIT_ID"];
			lblDEPOSIT_NUM.Text		= Request.QueryString["DEPOSIT_NUM"].ToString();
		}
		private void SetPolicyAttributes(TextBox txtPolicyNo, int intRowNo)
		{
			txtPolicyNo.Attributes.Add("onchange","javascript:checkPolicyNo(" + intRowNo.ToString() + ");checkPolicyStatus(" + intRowNo.ToString() + ");");
			txtPolicyNo.Attributes.Add("RowNo",intRowNo.ToString());
		}
		private void SetValidators(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revRECEIPT_AMOUNT");
			revValidator.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "965");
			//revValidator.ValidationExpression = aRegExpDoublePositiveNonZero;
			//revValidator.ValidationExpression = aRegExpDoublePositiveStartWithDecimal;
			revValidator.ValidationExpression = aRegExpCurrencyformat;
			revValidator.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());
		
			CustomValidator csvValidate = (CustomValidator) e.Item.FindControl("csvPolicyNo");
			csvValidate.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
			csvValidate.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

			csvValidate = (CustomValidator) e.Item.FindControl("csvCheckPolicyNo");
			csvValidate.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
			csvValidate.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());

		}

		private void ShowLineItemStatus(ArrayList alStatus)
		{
			try
			{
				int ctr = 0;
				foreach(DataGridItem dgi in dgDepositDetails.Items)
				{
					if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
					{

						TextBox txtText;
						txtText = (TextBox)dgi.FindControl("txtRECEIPT_AMOUNT");

						if (txtText.Text == "")
							continue;


						switch(int.Parse(alStatus[ctr].ToString()))
						{
							case -2:
								//Invalid policy
								((Label)dgi.FindControl("lblStatus")).Text = "Invalid policy number. Please enter a valid policy number.";
								break;
							case -1:
								((Label)dgi.FindControl("lblStatus")).Text = "Invalid policy number. Please enter a valid policy number.";
								break;
							default:
								((Label)dgi.FindControl("lblStatus")).Text = "";
								break;
						}
						ctr++;
					}
				}
			}
			catch(Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

		
		/// <summary>
		/// Returns the value of specified node from hidGLAccountXML
		/// </summary>
		/// <param name="nodeName">Name of node whose value to returnes</param>
		/// <returns>Value of node</returns>
		private string GetValueFromGLAccountXML(string nodeName)
		{
			if (hidGLAccountXML.Value != "")
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(hidGLAccountXML.Value);

				//Retreiving the GL ID and Fiscal Id
				System.Xml.XmlNode objNode = doc.SelectSingleNode("/NewDataSet/Table/" + nodeName );
				
				if(objNode != null)
				{	
					return objNode.InnerXml;
				}
				else
				{
					return "";
				}
			}
			else
			{
				return "";
			}
		}
		
		private void setPagingText()
		{
			foreach (DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					TextBox txtAmt = (TextBox)dgi.FindControl("txtRECEIPT_AMOUNT");
					if(txtAmt.Text !="")
						blnPagingFlag = false;
				}
			}
			
			if(blnPagingFlag)//Grid has data
			{
				lblPageLast.Text = "Page";
				lblPageCurr.Text = "New ";
				lblPagingLeft.Text = "";
				lblPagingRight.Text = "";
			}
			else
			{
				lblPageLast.Text = Convert.ToString(ds.Tables[3].Rows.Count);
				lblPageCurr.Text = ViewState["CurrentPageIndex"].ToString();
				lblPagingLeft.Text = "Page ";
				lblPagingRight.Text = " of ";
			}
			
			
		}
		#endregion

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

		#region Page Event Handlers
		
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			bool blnflag = false;
			ArrayList alRecr = new ArrayList();
			Cms.Model.Account.ClsDepositDetailsInfo objInfo;
			if(!blnConditionalDelete) // Normal CheckBox Deletion
			{
				foreach(DataGridItem dgi in dgDepositDetails.Items)
				{
					if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
					{
						if ( dgDepositDetails.DataKeys[dgi.ItemIndex] != System.DBNull.Value && ((CheckBox)dgi.FindControl("chkDelete")).Checked == true)
						{
							objInfo = new ClsDepositDetailsInfo();
							objInfo.CD_LINE_ITEM_ID = Convert.ToInt32(dgDepositDetails.DataKeys[dgi.ItemIndex]);
							objInfo.CUSTOMER_ID = int.Parse(GetUserId());
							blnflag = true;
							alRecr.Add(objInfo);
						}
					}
				}
				
				if (blnflag == true)
				{
					
					ClsDepositDetails objDeposit = new ClsDepositDetails();
					int CREATED_BY = int.Parse(GetUserId());
					if (objDeposit.Delete(alRecr/*,CREATED_BY*/) > 0)
					{
						//deleted sucessfully
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "127");
						BindGrid();
						setPagingText();
					}
					else
					{
						//deleted sucessfully
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "128");
					}
				}
				else
				{
					//No record to delete sucessfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "1");
				}
				lblMessage.Visible = true;
			}
			else // Conditional deletion where all 3 controls are blank.
			{
				ClsDepositDetails objDeposit = new ClsDepositDetails();
				objDeposit.Delete(CdLineItemID);
			}
		}


		private int Save(bool Confirm)
		{
			ArrayList alRecr = new ArrayList();
			
			foreach(DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					TextBox txtText;
					txtText = (TextBox)dgi.FindControl("txtRECEIPT_AMOUNT");

					if (txtText.Text == "")
					{
						blnConditionalDelete = true;
						if(dgDepositDetails.DataKeys[dgi.ItemIndex] != System.DBNull.Value)
						{
							CdLineItemID = Convert.ToInt32(dgDepositDetails.DataKeys[dgi.ItemIndex]);
							btnDelete_Click(null,null);
						}
						continue;
					}

					ClsDepositDetailsInfo objInfo = new ClsDepositDetailsInfo();

					//Saving the policy no and version no
					if (((HtmlInputHidden)dgi.FindControl("hidPOLICY_ID")).Value != "")
					{
						objInfo.POLICY_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidPOLICY_ID")).Value);
						objInfo.POLICY_VERSION_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidPOLICY_VERSION_ID")).Value);
					}
					
					if (((TextBox)dgi.FindControl("txtPolicyNo")).Text != "")
					{
						objInfo.POLICY_NO = ((TextBox)dgi.FindControl("txtPolicyNo")).Text;
						//Added PK to get password value 
						((TextBox)dgi.FindControl("txtPolicyNo")).Attributes.Add("value",objInfo.POLICY_NO.ToString());
					}

					objInfo.DEPOSIT_ID = int.Parse(hidDEPOSIT_ID.Value);
					
					txtText = (TextBox)dgi.FindControl("txtRECEIPT_AMOUNT");
					if (txtText.Text.Trim() != "")
						objInfo.RECEIPT_AMOUNT = Double.Parse(txtText.Text);

					objInfo.CREATED_BY = int.Parse(GetUserId());
					
					//objInfo.CREATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					objInfo.CREATED_DATETIME = DateTime.Now;
					if(ViewState["DepositPageId"].ToString() != "")// Update
					{
						if ( dgDepositDetails.DataKeys[dgi.ItemIndex] != System.DBNull.Value)
						{
							objInfo.CD_LINE_ITEM_ID = Convert.ToInt32(dgDepositDetails.DataKeys[dgi.ItemIndex]);
							objInfo.MODIFIED_BY = int.Parse(GetUserId());
							objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
							save=1;

						}
						else
						{
							objInfo.CD_LINE_ITEM_ID = -1;
							objInfo.PAGE_ID = ViewState["DepositPageId"].ToString();
						}
					}
					else // Add Case : Generate a new PageID
					{
						
						objInfo.CD_LINE_ITEM_ID = -1;
						//Random objRandNum = new Random();
						//int intRandNum = objRandNum.Next();
						//objInfo.PAGE_ID = Session.SessionID+intRandNum.ToString(); // Assign unique page ID in ADD mode
						int intRandNum = ClsDepositDetails.GetMaxLineItemID();
						objInfo.PAGE_ID = intRandNum.ToString();
						ViewState["DepositPageId"] = objInfo.PAGE_ID;
					}
					objInfo.DEPOSIT_TYPE = hidDEPOSIT_TYPE.Value;
					alRecr.Add(objInfo);
				}
			}
			
			ClsDepositDetails objDeposit = new ClsDepositDetails();
			ArrayList alStatus;
			
			double dbTapeTotal = 0 ;
			if(txtTapeTotal.Text != null &&  txtTapeTotal.Text.ToString().Trim() !="")
				dbTapeTotal=Double.Parse(txtTapeTotal.Text);
			
			if(alRecr.Count == 0)
				return 0;
			if ( objDeposit.AddCustomerDepositLineItems(alRecr, out alStatus, dbTapeTotal, Confirm) > 0)
			{
				
				if ( save==1 )
				{
					//updated sucessfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "31");
				}
				else
				{
					//saved sucessfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "29");
				}
				BindGrid();
				setPagingText();
			}
			else
			{
				//error occured, showing the error message
				lblMessage.Text = "Invalid Policy Number(s) have been entered. Please enter valid Policy Number(s) only.";
				ShowLineItemStatus(alStatus);
			}
				
			lblMessage.Visible = true;
			return 0;
		}

		/// <summary>
		/// Shows the status of each line item, return bl class method
		/// </summary>
		/// <param name="alStatus"></param>
	
		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			//Making blobject for confirming the deposit
			ClsDeposit objDeposit;
			objDeposit = new ClsDeposit();
				
			try
			{
				//Calling the method for confirming the deposit
				objDeposit.Confirm(int.Parse(hidDEPOSIT_ID.Value));
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
				lblMessage.Visible = true;
			}
			catch(Exception objExp)
			{
				lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "9");
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Visible = true;
			}
			finally
			{
				objDeposit.Dispose();
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
				
			Save(true);
			
		}

		private void btnPrevious_Click(object sender, System.EventArgs e)
		{
			ViewState["CurrentPageIndex"] = Convert.ToInt32(ViewState["CurrentPageIndex"]) - 1;
			if (Convert.ToInt32(ViewState["CurrentPageIndex"]) < 1)
				ViewState["CurrentPageIndex"] = "1";
			BindGrid();
			setPagingText();
		}

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			ViewState["CurrentPageIndex"] = Convert.ToInt32(ViewState["CurrentPageIndex"]) + 1;
			BindGrid();
			setPagingText();
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
			this.dgDepositDetails.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDepositDetails_ItemDataBound);
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


	}
}
