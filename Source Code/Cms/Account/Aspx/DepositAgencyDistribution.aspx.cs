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
using Cms.Model.Account;
using Cms.CmsWeb;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for DepositAgencyDistribution.
	/// </summary>
	public class DepositAgencyDistribution : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.CheckBox chkApplyOldestItems;
		protected System.Web.UI.WebControls.Label capTotalOwed;
		protected System.Web.UI.WebControls.Label lblTotalOwed;
		protected System.Web.UI.WebControls.DataGrid dgOpenItems;
		protected System.Web.UI.WebControls.CheckBox chSelectAll;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblTotalReceptAmount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMONTH;
		protected System.Web.UI.WebControls.Label lblTotalDue;
		protected System.Web.UI.WebControls.Label lblTotalAmount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidYEAR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGROUP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCD_LINE_ITEM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReconStatus;
		protected int intCount=0;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "1412";
			
			btnReset.PermissionString = gstrSecurityXML;
			btnReset.CmsButtonClass = CmsButtonType.Write;

			btnSave.PermissionString = gstrSecurityXML;
			btnSave.CmsButtonClass = CmsButtonType.Write;

			btnClose.CmsButtonClass			=	CmsButtonType.Execute;
			btnClose.PermissionString		=	gstrSecurityXML;

			btnSave.Attributes.Add("onclick","javascript:CalculateTotalAmount();if(OnSaveClick()== false) return false; else return true;");
			btnReset.Attributes.Add("onClick","javascript:return Reset();");
			btnClose.Attributes.Add("onClick","javascript:window.close();");

			if ( ! Page.IsPostBack)
			{
				GetQueryString();
				BindGrid();
			}
		}


		/// <summary>
		/// Binds the grid with data set
		/// </summary>
		private void BindGrid()
		{
			ClsDepositDetails objDepositDetails = new ClsDepositDetails();
			System.Data.DataSet ds = objDepositDetails.GetAgencyOpenItems(int.Parse(hidAGENCY_ID.Value), int.Parse(hidMONTH.Value), int.Parse(hidYEAR.Value), int.Parse(hidCD_LINE_ITEM_ID.Value));
			dgOpenItems.DataSource = ds;
			dgOpenItems.DataBind();
		}


		/// <summary>
		/// Retreives the query string from url
		/// </summary>
		private void GetQueryString()
		{
			hidAGENCY_ID.Value = Request.Params["AGENCY_ID"];
			hidMONTH.Value = Request.Params["MONTH"];
			hidYEAR.Value = Request.Params["YEAR"];
			lblTotalReceptAmount.Text = Request.Params["RECEIPT_AMOUNT"];
			hidCD_LINE_ITEM_ID.Value = Request.Params["CD_LINE_ITEM_ID"];

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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgOpenItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			

			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				
				//assigning the policy number
				Label lbl = (Label)e.Item.FindControl("lblPOLICY_NUMBER");
				if (DataBinder.Eval(e.Item.DataItem, "POLICY_NUMBER") != null)
				{
					lbl.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "POLICY_NUMBER"));
				}

				//Assigning the due amount
				lbl = (Label)e.Item.FindControl("lblDUE_AMOUNT");
				if (DataBinder.Eval(e.Item.DataItem, "NET_DUE") != null)
				{
					lbl.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "NET_DUE"));
				}

				//Assigning the due amount
				TextBox txt = (TextBox)e.Item.FindControl("txtAPPLIED_AMOUNT");
				txt.Attributes.Add("onblur","javascript:FormatAmount(this);");
				txt.Attributes.Add("onchange", "javascript:onAmountChange(" + (e.Item.ItemIndex + 2).ToString() + ")");
				
				RegularExpressionValidator rev = (RegularExpressionValidator)e.Item.FindControl("revAPPLIED_AMOUNT");
				rev.ValidationExpression = aRegExpDouble;
				rev.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "163");

				CheckBox objCheck = (CheckBox)e.Item.FindControl("chkSelect");
				objCheck.Attributes.Add("onClick","Javascript:fillAmtOnChkChng('" + (e.Item.ItemIndex + 2) + "','" + lbl.Text + "');");
				if(txt!=null && txt.Text!="")
					objCheck.Checked = true;	
				else
					objCheck.Checked= false;
				intCount++;
			//RegisterStartupScript("RunningTotal","<script>fillAmtOnChkChng('" + (e.Item.ItemIndex + 2) + "','" + lbl.Text + "');</script>");
	
			} 
			hidRowCount.Value=intCount.ToString();
		}

		private void MakeGroup()
		{

			if (hidGROUP_ID.Value == "")
			{
				//Creating the Model object for holding the New data
				ClsReconGroupInfo objReconGroupInfo;
				objReconGroupInfo = new ClsReconGroupInfo();
				if (hidAGENCY_ID.Value != "")
					objReconGroupInfo.RECON_ENTITY_ID = int.Parse(hidAGENCY_ID.Value);

				objReconGroupInfo.RECON_ENTITY_TYPE = ReconEntityType.AGN.ToString();;
				objReconGroupInfo.CREATED_DATETIME = DateTime.Now;
				objReconGroupInfo.CREATED_BY = int.Parse(GetUserId());

				if (hidCD_LINE_ITEM_ID.Value != "")
					objReconGroupInfo.CD_LINE_ITEM_ID = int.Parse(hidCD_LINE_ITEM_ID.Value);

				ClsReconGroup objReconGroup = new  ClsReconGroup();

				//Calling the add method of business layer class
				int intRetVal = objReconGroup.Add(objReconGroupInfo);
				hidGROUP_ID.Value = objReconGroupInfo.GROUP_ID.ToString();
			}


		}


		/// <summary>
		/// Saves the data on form
		/// </summary>
		/// <returns>True if sucessfull else false</returns>
		private bool Save()
		{

			//Making group
			MakeGroup();


			ArrayList alRecr = new ArrayList();
		
			foreach(DataGridItem dgi in dgOpenItems.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox objCheck = (CheckBox)dgi.FindControl("chkSelect");
					if(objCheck.Checked)
					{
						
						ClsReconDetailInfo objInfo = new ClsReconDetailInfo();

						TextBox txtText;
						HtmlInputHidden hidHiddenCtrl;
						txtText = (TextBox)dgi.FindControl("txtAPPLIED_AMOUNT");
						
						if (txtText.Text.Trim() != "")
						{
							objInfo.RECON_AMOUNT = Double.Parse(txtText.Text);
						}
						else
						{
							continue;
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
						//objInfo.ITEM_TYPE = ((System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidUPDATED_FROM")).Value;

						objInfo.GROUP_ID = int.Parse(hidGROUP_ID.Value);
						objInfo.IDEN_ROW_NO = -1;

						alRecr.Add(objInfo);
					}
				}
			}
		
			ClsReconDetail objRecon = new ClsReconDetail();

			int CREATED_BY = int.Parse(GetUserId());
			int CheckTypeID = 0 ;
			int intCdLineItem =  Convert.ToInt32(Request.QueryString["CD_LINE_ITEM_ID"].Trim());
			if ( objRecon.Save(alRecr,CheckTypeID ,CREATED_BY,intCdLineItem ) == true)
			{
				//Update Reconciliation Status of Line Item
				intCdLineItem = 0;

				intCdLineItem =  Convert.ToInt32(Request.QueryString["CD_LINE_ITEM_ID"].Trim());
				ClsDepositDetails objDepositDetails=  new ClsDepositDetails();
				
				objDepositDetails.UpdateReconcileStatus(intCdLineItem,hidReconStatus.Value,CREATED_BY);
				//saved successfully
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "29");
			}
			else
			{
				//error successfully
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "18");
			}

			lblMessage.Visible = true;
			return true;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (Save() == true)
			{
				//BindGrid();
			}
		}
	}
}
