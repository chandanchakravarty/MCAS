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


namespace Account.Aspx
{
	/// <summary>
	/// Summary description for WriteOffPremium.
	/// </summary>
	public class WriteOffPremium :  Cms.Account.AccountBase//System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTotalAmount;
		protected System.Web.UI.WebControls.Label lblTotalBalance;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnFind;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.WebControls.DataGrid grdWriteOff;
		protected Cms.CmsWeb.Controls.CmsButton btnWriteoff;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBalanceAmount;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAMT_WRITE_OFF;
		string url="";
		protected System.Web.UI.WebControls.Label lblSpace;
		public double cumulativeWriteOffAmount = 0.0;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// Put user code to initialize the page here
			
			base.ScreenId = "423";
			/*********** Setting permissions and class (Read/write/execute/delete)  *************/
			SetPermissions();
			setErrorMessages();
			url = ClsCommon.GetLookupURL();			
			imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','POLICY_ID','POLICY_NUMBER','hidPolicyID','txtPOLICY_ID','PolicyForFeeRev','Policy','','');");		
			btnWriteoff.Attributes.Add("OnClick","javascript:if (Select()== false) return false;if(!Page_IsValid) return false;");
			btnReset.Attributes.Add("onclick","javascript:return formReset();");

			if(!IsPostBack)
			{
				BindGrid(null);
			}
		}

		private void SetPermissions()
		{
			btnFind.CmsButtonClass		= CmsButtonType.Read;
			btnFind.PermissionString	= gstrSecurityXML;

			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;
		
			btnWriteoff.CmsButtonClass		= CmsButtonType.Write;
			btnWriteoff.PermissionString	= gstrSecurityXML;
		}
		private void setErrorMessages()
		{
			revPOLICY_ID.ValidationExpression = aRegExpAlphaNum;
			revPOLICY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102");

				

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
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			this.grdWriteOff.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdWriteOff_ItemDataBound);
			this.btnWriteoff.Click += new System.EventHandler(this.btnWriteoff_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		private void BindGrid(string policyNo)
		{
			lblMessage.Visible=false;
			try
			{

				DataSet objDataSet = ClsAccount.GetWriteOffAmountDetail(policyNo);
				grdWriteOff.DataSource =  objDataSet.Tables[0];
				grdWriteOff.DataBind();	
			
				int NoOfRows = objDataSet.Tables[0].Rows.Count;
				if(NoOfRows>0)
				{
					btnWriteoff.Enabled = true;
					btnReset.Enabled =true;
				}
				else
				{
					btnWriteoff.Enabled = false;
					//btnReset.Enabled =false;(Itrack 6672)
				}
			}
			catch (Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"8") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}

			
		}
		private void grdWriteOff_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
				{
					
					Label lblBalAmt = (Label)e.Item.FindControl("lblBALANCE");
					string BalAmt = "";
					if(lblBalAmt!=null)
					{
						BalAmt = lblBalAmt.Text.ToString();
					}
					CheckBox chkAgenCheck = (CheckBox)e.Item.FindControl("chkSelect");
					chkAgenCheck.Attributes.Add("onClick","javaScript:fillBalAmtOnChkChng('" + (e.Item.ItemIndex + 2) + "','" + BalAmt + "');EnableValidators(" + (e.Item.ItemIndex + 2).ToString() + ");");

					#region ITRACK 4361
					TextBox txtAmt = null;
					txtAmt = (TextBox)e.Item.FindControl("txtAmtWriteOff");
					txtAmt.Attributes.Add("enabled","false");
					txtAmt.Attributes.Add("onblur","javascript:FormatAmount(this);ValidateWriteOffAmt('" + txtAmt.ClientID + "');UpdateWriteOffAmount(" + (e.Item.ItemIndex + 2).ToString() + ");EnableValidators(" + (e.Item.ItemIndex + 2).ToString() + ");");

					RequiredFieldValidator rfvValidator = (RequiredFieldValidator) e.Item.FindControl("rfvAMT_WRITE_OFF");
					rfvValidator.ErrorMessage	=  "Write Off Amount can not be Blank."; //Chnaged
					#endregion
				
					RegularExpressionValidator revValidator = null;
					revValidator = (RegularExpressionValidator)e.Item.FindControl("revAMT_WRITE_OFF");
					//revValidator.ValidationExpression=aRegExpDoublePositiveNonZero;
					//Added FOR Itrack Issue #5590.
					revValidator.ValidationExpression = aRegExpCurrencyformat;
					revValidator.ErrorMessage = ClsMessages.FetchGeneralMessage("163");
				
				}				
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
		}
		#endregion

		private void btnFind_Click(object sender, System.EventArgs e)
		{
            BindGrid(txtPOLICY_ID.Text.ToString().Trim());		
		}

		private void btnWriteoff_Click(object sender, System.EventArgs e)
		{
            ClsAccount ObjAct = new ClsAccount();
			cumulativeWriteOffAmount = GetCumulativeAmount();
			int i = 0;
			int idenRowId = 0;
			int group_id = 0;
			double amount = 0.0;
			string policyNo = txtPOLICY_ID.Text.ToString().Trim();
			double totalBalance = 0.0;
			if(hidBalanceAmount.Value!="")
				totalBalance =  Double.Parse(hidBalanceAmount.Value.ToString().Trim());

			foreach(DataGridItem item in grdWriteOff.Items)
			{
				if ( item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox chkAgenCheck = (CheckBox)item.FindControl("chkSelect");

					if(chkAgenCheck.Checked)
					{
						if(grdWriteOff.DataKeys[item.ItemIndex]!=DBNull.Value && grdWriteOff.DataKeys[item.ItemIndex].ToString().Length>0)
                           idenRowId = Convert.ToInt32(grdWriteOff.DataKeys[item.ItemIndex].ToString());

						TextBox txtText;
						txtText = (TextBox)item.FindControl("txtAmtWriteOff");
						if(txtText.Text!="")
							amount = Double.Parse(txtText.Text.ToString().Trim());
						group_id = ObjAct.ProcessPremiumWriteOffAmount(idenRowId,cumulativeWriteOffAmount,amount,DateTime.Now,i,int.Parse(GetUserId()),group_id,totalBalance,policyNo);
						i++;
						
					}
				}

			}
			BindGrid(txtPOLICY_ID.Text.ToString().Trim());
			//Added For Itrack Issue #5571.
			lblMessage.Text = ClsMessages.FetchGeneralMessage("1024") + txtPOLICY_ID.Text.ToString().ToUpper();
			lblMessage.Visible=true;
		
		}
		public double GetCumulativeAmount()
		{
			foreach(DataGridItem item in grdWriteOff.Items)
			{
				if ( item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox chkAgenCheck = (CheckBox)item.FindControl("chkSelect");

					if(chkAgenCheck.Checked)
					{
						TextBox txtText;
						txtText = (TextBox)item.FindControl("txtAmtWriteOff");	
						if(txtText.Text!="")
							cumulativeWriteOffAmount = cumulativeWriteOffAmount + Double.Parse(txtText.Text.ToString().Trim());		
						
					}
				}

			}
			return cumulativeWriteOffAmount;

		}
	}
}
