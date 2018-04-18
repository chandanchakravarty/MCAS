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
	/// Summary description for CheckRegisterItemsPaidPopup.
	/// </summary>
	public class CheckRegisterItemsPaidPopup : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgOpenItems;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECEIPT_AMOUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCD_LINE_ITEM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENTITY_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.TextBox txtTotal;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARENT_GROUP_ID;
		protected System.Web.UI.WebControls.Label lblHeader;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		public string Mode="";//grid text box attributes

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "204_1";
			//btnReset.Attributes.Add ("onclick","javascript:return ResetForm('ItemToBePaid');");
			btnReset.Attributes.Add("onclick","javascript:return ResetMyForm('" + Page.Controls[0].ID + "' );");
			
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;

			btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString = gstrSecurityXML;

			if (! Page.IsPostBack)
			{
				if(Request.QueryString["Mode"]!=null && Request.QueryString["Mode"].ToString().ToUpper().Equals("REGISTER"))
				{
					lblHeader.Text = "Items Paid";
					btnSave.Visible = false;
					Mode = "Register";
				}

				//retreiving the query string values
				GetQueryString();
				//SetDefaultValues();
				BindGrid();
			}
		}
		private void BindGrid()
		{

			ClsReconDetail objReconDetail = new ClsReconDetail();
			
			int pageSize = 0;

			if ( System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"] == null )
			{
				pageSize = 10;
			}
			else
			{
				pageSize = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"]);
			}
		
			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]) + 1;

			DataSet ds = new DataSet();
			
			string checkTypeID = Request.QueryString["CHECK_TYPE_ID1"].ToString();
			ds = objReconDetail.GetCheckOpenItems(int.Parse(hidENTITY_ID.Value), int.Parse(hidCD_LINE_ITEM_ID.Value), currentPageIndex, pageSize,checkTypeID);


			dgOpenItems.DataSource = ds.Tables[1];
			dgOpenItems.DataBind();
			
			//	DataTable dtCoverages = ds.Tables[0];
		}
		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			//string strDepositId		= Request.Params["DEPOSIT_ID"];
			string strLineItemId	= Request.Params["CD_LINE_ITEM_ID"];
			string strXML = ClsDepositDetails.GetXmlForEditPageControls(strLineItemId);

			hidENTITY_ID.Value		= Request.Params["PAYEE_ENTITY_ID"];
			
//			if(Request.Params["GROUP_ID"] != null)
//			{
//				hidPARENT_GROUP_ID.Value = Request.Params["GRP_ID"];
//			}

			if (Request.Params["CHECK_ID"] != null)
			{
				hidCD_LINE_ITEM_ID.Value= Request.Params["CHECK_ID"];
			}
//			else
//			{
//				hidCD_LINE_ITEM_ID.Value= "0";
//			}
			//hidRECEIPT_AMOUNT.Value = Request.Params["RECEIPT_AMOUNT"];
		}

		private void dgOpenItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				SetValidators(e);
			}
		}
		private void SetValidators(DataGridItemEventArgs e)
		{
			try
			{
				RegularExpressionValidator revRECON_AMOUNT = (RegularExpressionValidator) e.Item.FindControl("revRECON_AMOUNT");
				revRECON_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
				revRECON_AMOUNT.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			}
			catch(Exception objExp)
			{
				ExceptionManager.Publish(objExp);
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
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// Saves the data on form
		/// </summary>
		/// <returns>True if sucessfull else false</returns>
		private bool Save()
		{
			ArrayList alRecr = new ArrayList();
			ClsReconDetail objRecon=null;
			bool flag=false;
			try
			{
				foreach(DataGridItem dgi in dgOpenItems.Items)
				{
					if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
					{
						ClsReconDetailInfo objInfo = new ClsReconDetailInfo();

						TextBox txtText;
						HtmlInputHidden hidHiddenCtrl;
						txtText = (TextBox)dgi.FindControl("txtRECON_AMOUNT");
					
						if (txtText.Text.Trim() != "")
						{
							objInfo.RECON_AMOUNT = Double.Parse(txtText.Text);
						}
					
						hidHiddenCtrl = (HtmlInputHidden)dgi.FindControl("hidSOURCE_ROW_ID");

						if(hidHiddenCtrl.Value != "")
						{
							objInfo.ITEM_REFERENCE_ID = int.Parse(hidHiddenCtrl.Value);
						}
						else
						{
							objInfo.ITEM_REFERENCE_ID = 0;
						}
						objInfo.ITEM_TYPE = ((System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidUPDATED_FROM")).Value;

						if ( dgOpenItems.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							objInfo.GROUP_ID	= int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidGROUP_ID")).Value);
							objInfo.IDEN_ROW_NO = Convert.ToInt32(dgOpenItems.DataKeys[dgi.ItemIndex]);
						}
						else
						{
							objInfo.IDEN_ROW_NO = -1;
							if (hidPARENT_GROUP_ID.Value != "")
							{
								//Pasing the group id , passed from previous tab
								objInfo.GROUP_ID = int.Parse(hidPARENT_GROUP_ID.Value);
							}
						}

						alRecr.Add(objInfo);
					}
				}
		
				objRecon = new ClsReconDetail();
				int CheckTypeId = 0;
				try
				{
					CheckTypeId = int.Parse(Request.QueryString["CHECK_TYPE_ID1"].ToString());
				}
				catch//(Exception ex)
				{
					throw new Exception("Check Type Is required");
				}
				int CREATED_BY = int.Parse(GetUserId());
				if ( objRecon.Save(alRecr,CheckTypeId,CREATED_BY) == true)
				{
					//saved successfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "29");
					flag =true;
				}
				else
				{
					//error successfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "20");
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
				if(objRecon!= null)
					objRecon.Dispose();
			}
			return flag;
		}
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (Save() == true)
			{
				BindGrid();
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			BindGrid();
		}
	}
}
