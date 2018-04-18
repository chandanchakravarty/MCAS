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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Account;
using Cms.CmsWeb;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for ReconDetail.
	/// </summary>
	public class ReconDetail :Cms.Account.AccountBase
	{
		
		protected System.Web.UI.WebControls.DataGrid dgOpenItems;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENTITY_ID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.CheckBox chkApplyOldestItems;
		protected System.Web.UI.WebControls.CheckBox Checkbox1;
		protected System.Web.UI.WebControls.Label capTotalOwed;
		protected System.Web.UI.WebControls.Label lblTotalOwed;
		protected System.Web.UI.WebControls.Label capTotalReceiptBalance;
		protected System.Web.UI.WebControls.Label lblTotalReceiptBalance;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECEIPT_AMOUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCD_LINE_ITEM_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARENT_GROUP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENTITY_TYPE;
		//ClsReconDetail objReconDetail;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		string strEntityType= "";
		public int gridCount = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "191_0_1";
			btnReset.Attributes.Add ("onclick","javascript:return Reset();");
			
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;

			btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString = gstrSecurityXML;

			btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString = gstrSecurityXML;

			 GetQueryString();

			if (! Page.IsPostBack)
			{

				//btnSave.Attributes.Add("OnClick","javascript:if (Select()== false) return false; if(CheckTotalRconcileAmount() == false) return false;");
				btnSave.Attributes.Add("OnClick","javascript:PressEnter(); if(CheckTotalRconcileAmount() == false) return false;");
				btnDelete.Attributes.Add("OnClick","javascript:return Select();");

				SetDefaultValues();
				BindGrid();
			}
		}
		
		private void BindGrid()
		{

			ClsReconDetail objReconDetail = new ClsReconDetail();
			DataSet ds = new DataSet();
			//Entity Type 1(customer)  2(Agency)\
			try
			{
				strEntityType = hidENTITY_TYPE.Value.ToString().Trim();
				ds = objReconDetail.GetOpenItems(int.Parse(hidENTITY_ID.Value), int.Parse(hidPARENT_GROUP_ID.Value),strEntityType);

				//check records
				gridCount = ds.Tables[1].Rows.Count;
				if(gridCount==0)
					gridCount = 20;
				else if(gridCount==1 || gridCount==2 || gridCount==3)
					gridCount = 80;
				else if(gridCount==4 || gridCount==5 ||gridCount==6 || gridCount==7)
					gridCount = 200;
				else
					gridCount=500;
				
				dgOpenItems.DataSource = ds.Tables[1];
				
			
				if(strEntityType  == ReconEntityType.VEN.ToString())
				{
					dgOpenItems.Columns[3].Visible = false;
				}
				if(strEntityType  == ReconEntityType.CUST.ToString())
				{	
					// Add DUE_DATE column in case of CUSTOMER
					BoundColumn colDUE_DATE = new BoundColumn();
					colDUE_DATE.DataField = "DUE_DATE";
					colDUE_DATE.HeaderText= "Due Date";
					colDUE_DATE.ItemStyle.Width= Unit.Percentage(10);
					colDUE_DATE.Visible = true;
					dgOpenItems.Columns.Add(colDUE_DATE);
				}
				dgOpenItems.DataBind();
			}
			catch(Exception Ex)
			{
				lblMessage.Text = Ex.Message.ToString();
				lblMessage.Visible=true;
				return;
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
			this.dgOpenItems.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgOpenItems_ItemDataBound);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void dgOpenItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				SetValidators(e);
				SetAmountProperties(e);
				SetCheckboxProperties(e);
			}
		}
		
		private void SetCheckboxProperties(DataGridItemEventArgs e)
		{
			try
			{
				CheckBox cb = (CheckBox ) e.Item.FindControl("chkAPPLY_ALL");
				cb.Attributes.Add ("onclick","OnApplyAll(this,'" + (e.Item.ItemIndex + 2).ToString() + "');");
				if(dgOpenItems.DataKeys[e.Item.ItemIndex]!= System.DBNull.Value)
					cb.Checked = true;


			}
			catch(Exception objExp)
			{
				ExceptionManager.Publish(objExp);
			}
		}

		private void SetValidators(DataGridItemEventArgs e)
		{
			try
			{
				RegularExpressionValidator revRECON_AMOUNT = (RegularExpressionValidator) e.Item.FindControl("revRECON_AMOUNT");
				revRECON_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
				revRECON_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			}
			catch(Exception objExp)
			{
				ExceptionManager.Publish(objExp);
			}
		}
		
		private void SetAmountProperties(DataGridItemEventArgs e)
		{
			try
			{
				TextBox txtRECON_AMOUNT = (TextBox) e.Item.FindControl("txtRECON_AMOUNT");
				Label lblBALANCE = (Label)e.Item.FindControl("lblBALANCE");
				Label lblDUE = (Label)e.Item.FindControl("lblDUE");
				txtRECON_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this);OnAmountChange(this,'" + (e.Item.ItemIndex + 2).ToString() + "');");
			}
			catch(Exception objExp)
			{
				ExceptionManager.Publish(objExp);
			}
		}

		
		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			//string strDepositId		= Request.Params["DEPOSIT_ID"];
			string strLineItemId	= Request.Params["CD_LINE_ITEM_ID"];
			string strXML = ClsDepositDetails.GetXmlForEditPageControls(strLineItemId);

			hidENTITY_ID.Value		= Request.Params["ENTITY_ID"];
			hidENTITY_TYPE.Value	= Request.Params["ENTITY_TYPE"];
						
			if(Request.Params["GROUP_ID"] != null && Request.Params["GROUP_ID"] != "" )
				hidPARENT_GROUP_ID.Value = Request.Params["GROUP_ID"];			
//			
			if(hidPARENT_GROUP_ID.Value == null || hidPARENT_GROUP_ID.Value == "")
			{
				if(Request.QueryString["GRP_ID"] != null && Request.QueryString["GRP_ID"].ToString() != "" )
				{
					hidPARENT_GROUP_ID.Value = Request.QueryString["GRP_ID"].ToString();			
			//		Request.Params["GROUP_ID"] = hidPARENT_GROUP_ID.Value;
				}
			}
		}

		/// <summary>
		/// Saves the data on form
		/// </summary>
		/// <returns>True if sucessfull else false</returns>
		private bool Save()
		{
			ArrayList alRecr = new ArrayList();
			ArrayList alPolicy = new ArrayList();
			bool blnSave;
		
			foreach(DataGridItem dgi in dgOpenItems.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					//Saving selected reocords only: 
					blnSave = ((CheckBox)(dgi.Cells[0].FindControl("chkAPPLY_ALL"))).Checked;
					if(blnSave)
					{
					
						ClsReconDetailInfo objInfo = new ClsReconDetailInfo();

						TextBox txtText;
						HtmlInputHidden hidITEM_REF_ID;
						txtText = (TextBox)dgi.FindControl("txtRECON_AMOUNT");
					
						if (txtText.Text.Trim() != "")
						{
							objInfo.RECON_AMOUNT = Double.Parse(txtText.Text);
						}
					
						hidITEM_REF_ID = (HtmlInputHidden)dgi.FindControl("hidITEM_REF_ID");

						if(hidITEM_REF_ID.Value != "")
						{
							objInfo.ITEM_REFERENCE_ID = int.Parse(hidITEM_REF_ID.Value);
						}
						else
						{
							objInfo.ITEM_REFERENCE_ID = 0;
						}
						
						
						objInfo.ITEM_TYPE = ((System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidUPDATED_FROM")).Value;
						//objInfo.ITEM_TYPE = strEntityType ;

						if (hidPARENT_GROUP_ID.Value != "")
						{
							//Pasing the group id , passed from previous tab
							objInfo.GROUP_ID = int.Parse(hidPARENT_GROUP_ID.Value);
						}

						if ( dgOpenItems.DataKeys[dgi.ItemIndex] != System.DBNull.Value)
						{
							objInfo.IDEN_ROW_NO = Convert.ToInt32(dgOpenItems.DataKeys[dgi.ItemIndex]);
						}
						else
						{
							objInfo.IDEN_ROW_NO = -1;
						}

						Label policyNum = (Label)dgi.FindControl("lblPOLICY_NUMBER");
						alPolicy.Add(policyNum.Text);
						

						alRecr.Add(objInfo);
						
						
					}
				}
			}
		
			try
			{
				ClsReconDetail objRecon = new ClsReconDetail();
				DataSet ds = new DataSet();
				strEntityType = hidENTITY_TYPE.Value.ToString().Trim();
				string EntityId = hidENTITY_ID.Value;
				int UserID = Convert.ToInt32(GetUserId()); 
		
				if ( objRecon.SaveReconDetails(alRecr,alPolicy,strEntityType,EntityId,UserID) == true)
				{
					//saved successfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "29");
				}
			}
			catch(Exception ex )
			{
				lblMessage.Visible=true;
				lblMessage.Text= ex.Message;
			}
			

			lblMessage.Visible = true;
			return true;
		}
		private void SetDefaultValues()
		{
			//lblTotalReceiptBalance.Text = hidRECEIPT_AMOUNT.Value;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (Save() == true)
			{
				BindGrid();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			ArrayList alRecr = new ArrayList();
			ArrayList alPolicy = new ArrayList();
			bool blnDelete;
		
			foreach(DataGridItem dgi in dgOpenItems.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					//Deleting selected saved reocords 
					blnDelete = ((CheckBox)(dgi.Cells[0].FindControl("chkAPPLY_ALL"))).Checked;
					if(blnDelete &&  (dgOpenItems.DataKeys[dgi.ItemIndex] != System.DBNull.Value ))
					{
					
						ClsReconDetailInfo objInfo = new ClsReconDetailInfo();

						
							objInfo.ITEM_TYPE = ((System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidUPDATED_FROM")).Value;
							
							objInfo.IDEN_ROW_NO = Convert.ToInt32(dgOpenItems.DataKeys[dgi.ItemIndex]);
							//Praveen Kumar
							Label policyNum = (Label)dgi.FindControl("lblPOLICY_NUMBER");
							alPolicy.Add(policyNum.Text);
							//End
							alRecr.Add(objInfo);
					}
				}
			}
		
			try
			{
				ClsReconDetail objRecon = new ClsReconDetail();
				int UserID = Convert.ToInt32(GetUserId()); 
				
				if ( objRecon.DeleteReconDetails(alRecr,alPolicy,UserID,hidENTITY_ID.Value, hidENTITY_TYPE.Value.ToString().Trim()) == true)
				{
					lblMessage.Text ="Information deleted successfully";
					lblMessage.Visible = true;
					BindGrid();
				}
			}
			catch(Exception ex )
			{
				lblMessage.Visible=true;
				lblMessage.Text= ex.Message;
			}
					
		}
	}
}
