#region namespaces
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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;
#endregion

namespace Cms.Account.Aspx
{

	public class DepositOthers : Cms.Account.AccountBase //System.Web.UI.Page
	{
		#region Variable Declarations
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgDepositDetails;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.Label lblTotalAmount;
		protected System.Web.UI.WebControls.Label lblTapeTotal;
		protected System.Web.UI.WebControls.TextBox txtTapeTotal;
		protected System.Web.UI.WebControls.RegularExpressionValidator rfvTapeTotal;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGLAccountXML;
		protected Cms.CmsWeb.Controls.CmsButton btnCompareTotal;
		protected Cms.CmsWeb.Controls.CmsButton btnPrevious;
		protected Cms.CmsWeb.Controls.CmsButton btnNext;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICYINFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_TYPE;
		protected string strDepositType;
		protected System.Web.UI.WebControls.Label lblDepositNum;
		protected System.Web.UI.WebControls.Label lblDEPOSIT_NUM;
		protected System.Web.UI.WebControls.Label lblPageCurr;
		protected System.Web.UI.WebControls.Label lblPageLast;
		protected System.Web.UI.WebControls.Label lblPagingLeft;
		protected System.Web.UI.WebControls.Label lblPagingRight;
        System.Resources.ResourceManager objResourceMgr;
		public int save;
		private bool blnPagingFlag = true;
		DataSet ds = new DataSet();
		protected int currentPageIndex;
		#endregion

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			save=0;
			if(Request.QueryString["DEPOSIT_TYPE"]!=null && Request.QueryString["DEPOSIT_TYPE"].ToString()!="")
				hidDEPOSIT_TYPE.Value = Request.QueryString["DEPOSIT_TYPE"].ToString().ToUpper();
			//Setting the screen id
			if(hidDEPOSIT_TYPE.Value!="")
				strDepositType = hidDEPOSIT_TYPE.Value;
			SetScreenId();
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.DepositOthers", System.Reflection.Assembly.GetExecutingAssembly());
            		
			btnSave.Attributes.Add("onclick","javascript:return DoValidationCheck();");            
			btnDelete.Attributes.Add("onclick","javascript:return DeleteRows();");
			btnBack.Attributes.Add("onclick","javascript:return BackClick();");
		
			rfvTapeTotal.ValidationExpression =  aRegExpDoublePositiveStartWithDecimal;;
			rfvTapeTotal.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "965");
			txtTapeTotal.Attributes.Add("onblur","javascript:FormatAmount(this);");

			btnDelete.Enabled = false; //AT first if there are No records Disable the Button

			/*if ((strDepositType == "CLAM") || (strDepositType == "RINS"))
				dgDepositDetails.Columns[5].Visible=false;*/

			if (hidDEPOSIT_TYPE.Value.Trim() == "CLAM")
				dgDepositDetails.Columns[5].Visible=false;

			#region setting security xml
			btnSave.CmsButtonClass = CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;
			
			btnReset.CmsButtonClass = CmsButtonType.Write;
			btnReset.PermissionString = gstrSecurityXML;

			btnDelete.CmsButtonClass = CmsButtonType.Delete;
			btnDelete.PermissionString = gstrSecurityXML;

			btnCompareTotal.CmsButtonClass = CmsButtonType.Execute;
			btnCompareTotal.PermissionString = gstrSecurityXML;
			btnCompareTotal.Attributes.Add("onClick","javascript:DoValidationCheck();return false;");

			btnNext.CmsButtonClass = CmsButtonType.Read;
			btnNext.PermissionString = gstrSecurityXML;

			btnPrevious.CmsButtonClass = CmsButtonType.Read;
			btnPrevious.PermissionString = gstrSecurityXML;

			
			btnBack.CmsButtonClass = CmsButtonType.Read;
			btnBack.PermissionString = gstrSecurityXML;			

			#endregion

			if (! Page.IsPostBack )
			{
				GetQueryString();
				btnReset.Attributes.Add("onClick","javascript:return Reset();");
                SetCaption();
				ViewState["CurrentPageIndex"] = 1;
				
				if ( Request.QueryString["DEPOSIT_ID"] != null )
				{
					hidDEPOSIT_ID.Value = Request.QueryString["DEPOSIT_ID"].ToString();
					
					try
					{
						BindGrid();
						setPagingText();
						//FetchOtherDetails();
					}
					catch(Exception ex)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
						return;
					}
				}
				// Added By swarup on 12/12/2006
				/*foreach(DataGridItem dgi in dgDepositDetails.Items)
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
				}*/
		
				//Formating the grid and showing only appropriate columns
				FormatGrid();
			}
		}

		#endregion

		#region ItemDataBound / BindGrid()
		private void dgDepositDetails_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				int index = e.Item.ItemIndex + 2;	
				string depType=""; 
				if(Request.QueryString["DEPOSIT_TYPE"] != null && Request.QueryString["DEPOSIT_TYPE"].ToString()!= "")
					depType = Request.QueryString["DEPOSIT_TYPE"].ToString();
				// Hide Distribution Status column in case of Claim/Reins receipts
				if(depType.Equals("CLAM"))
					dgDepositDetails.Columns[6].Visible=false;
				else
					dgDepositDetails.Columns[6].Visible=true;


				TextBox txtPAYMENT_FROM = (TextBox)e.Item.FindControl("txtPAYMENT_FROM");
				TextBox txtRECEIPT_AMOUNT = (TextBox)e.Item.FindControl("txtRECEIPT_AMOUNT");
				txtRECEIPT_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this);");

				//Check if any records Exists Enable the Delete Button 
				//Check it for Mandatory Field.
				if(txtRECEIPT_AMOUNT.Text!="")
					btnDelete.Enabled = true;
				
				

				System.Web.UI.WebControls.HyperLink hlkDistribute = ((System.Web.UI.WebControls.HyperLink)e.Item.FindControl("hlkDistribute"));

				if (dgDepositDetails.DataKeys[e.Item.ItemIndex] == System.DBNull.Value )
				{
					((CheckBox)e.Item.FindControl("chkDelete")).Visible = false;
					hlkDistribute.Enabled = false;
				}
				else
				{
					((CheckBox)e.Item.FindControl("chkDelete")).Visible = true;

					hlkDistribute.NavigateUrl = "javascript:OpenDistributeWindow('" 
						+ dgDepositDetails.DataKeys[e.Item.ItemIndex] 
						+ "','" 
						+ txtRECEIPT_AMOUNT.Text 
						+ "','" 
						+ txtPAYMENT_FROM.Text.Trim()
						+ "')";
					hlkDistribute.Enabled = true;
				}
				SetValidators(e);
              
			}
		}

		/// <summary>
		/// Binds the data set with the data grid
		/// </summary>
		private void BindGrid()
		{
			
			ClsDepositDetails objDeposit = new ClsDepositDetails();
			
			int pageSize = base.DepositDetailsPageSize;
			currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);

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
			// Display records according to PageID. i.e; Different PageID in Different Grid  Page
			if(currentPageIndex <= ds.Tables[3].Rows.Count)
			{
				//Filter records from the fetched DS according to PageID
				string strExp = ds.Tables[3].Rows[currentPageIndex-1]["PAGE_ID"].ToString();
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

            //added to format amount
		/*	protected string FormatMoney(object amount) 
			//{		
				string tempMoney = String.Format("{0:0,0.00}", amount);
				if(tempMoney.StartsWith("0"))
				{
					tempMoney = tempMoney.Substring(1, tempMoney.Length-1);
				}
				return tempMoney;
				//	return String.Format("{0:C}", amount);
		//	}
		*/	

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
					txtTapeTotal.Text= String.Format("{0:0,0.00}",Convert.ToDouble(Tape_Total));
					
			}
		}

		#endregion

		#region Set ScreenID(),setPagingText(), GetQueryString(),FormatGrid(),SetValidators()
		private void SetScreenId()
		{
			
			if (strDepositType == "CLAM")
			{
				base.ScreenId = "187_3";		//Claims Deposit Line Item Destails
			}
			else if (strDepositType == "RINS")
			{
				base.ScreenId = "187_3";		//Reinsurance Deposit Line Item Destails
                
			}
			else if  (strDepositType == "MISC")
			{
				base.ScreenId = "187_4";//Misc Deposit Line Item Destails
               
			}
			else
				base.ScreenId = "187_5";
            
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
				lblPageCurr.Text = currentPageIndex.ToString();
				lblPagingLeft.Text = "Page ";
				lblPagingRight.Text = " of ";
			}
			
			
		}
		private void GetQueryString()
		{
			hidDEPOSIT_ID.Value		= Request.Params["DEPOSIT_ID"];
			lblDEPOSIT_NUM.Text		= Request.QueryString["DEPOSIT_NUM"].ToString();
		}

		private void FormatGrid()
		{
			//string strCalledFrom = Request.Params["CalledFrom"].ToString();

			//if (strCalledFrom.ToUpper() == "MISC")
			if (hidDEPOSIT_TYPE.Value.ToUpper() == "MISC")
			{
				//If called for miscellenaous receipts then claim number column is not required
				dgDepositDetails.Columns[2].Visible = false;
			}
		}

		private void SetValidators(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revRECEIPT_AMOUNT");
			revValidator.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "965");
			//revValidator.ValidationExpression =  aRegExpDoublePositiveStartWithDecimal;
			revValidator.ValidationExpression =  aRegExpCurrencyformat;
			revValidator.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());
		
		}

        private void SetCaption()
        {
            btnNext.Text = objResourceMgr.GetString("btnNext");
            btnPrevious.Text = objResourceMgr.GetString("btnPrevious");
            lblTapeTotal.Text = objResourceMgr.GetString("lblTapeTotal");
            lblMsg.Text = objResourceMgr.GetString("lblMsg");
            lblDepositNum.Text = objResourceMgr.GetString("lblDepositNum");


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

		#region Page Event Handlers(save/Delete/Next/Prev..)
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			bool blnflag = false;
			ArrayList alRecr = new ArrayList();
			Cms.Model.Account.ClsDepositDetailsInfo objInfo;

			foreach(DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					if ( dgDepositDetails.DataKeys[dgi.ItemIndex] != System.DBNull.Value && ((CheckBox)dgi.FindControl("chkDelete")).Checked == true)
					{
						objInfo = new ClsDepositDetailsInfo();
						objInfo.CD_LINE_ITEM_ID = Convert.ToInt32(dgDepositDetails.DataKeys[dgi.ItemIndex]);
						blnflag = true;
						alRecr.Add(objInfo);
					}
				}
			}
			
			if (blnflag == true)
			{

				ClsDepositDetails objDeposit = new ClsDepositDetails();
				int CREATED_BY = int.Parse(GetUserId());
				if (objDeposit.Delete(alRecr,CREATED_BY) > 0)
				{
					//deleted successfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "127");
					BindGrid();
					setPagingText();
				}
				else
				{
					//deleted successfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "128");
				}
			}
			else
			{
				//No record to delete successfully
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "1");
			}
			lblMessage.Visible = true;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			Save(true);			
		}

		private int Save(bool confirm)
		{
			ArrayList alRecr = new ArrayList();
			
			foreach(DataGridItem dgi in dgDepositDetails.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					TextBox txtText;
					txtText = (TextBox)dgi.FindControl("txtRECEIPT_AMOUNT");

					if (txtText.Text == "")
						continue;

					ClsDepositDetailsInfo objInfo = new ClsDepositDetailsInfo();

					if (((TextBox)dgi.FindControl("txtCLAIM_NUMBER")).Text != "")
					{
						objInfo.CLAIM_NUMBER= ((TextBox)dgi.FindControl("txtCLAIM_NUMBER")).Text;
					}
					objInfo.RECEIPT_FROM_NAME = ((TextBox)dgi.FindControl("txtPAYMENT_FROM")).Text;

					objInfo.DEPOSIT_ID = int.Parse(hidDEPOSIT_ID.Value);
					
					txtText = (TextBox)dgi.FindControl("txtRECEIPT_AMOUNT");					
					if (txtText.Text.Trim() != "")
						objInfo.RECEIPT_AMOUNT = Double.Parse(txtText.Text);

					objInfo.CREATED_BY = int.Parse(GetUserId());
					objInfo.CREATED_DATETIME = DateTime.Now;

					/*	if ( dgDepositDetails.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							objInfo.CD_LINE_ITEM_ID = Convert.ToInt32(dgDepositDetails.DataKeys[dgi.ItemIndex]);

							objInfo.MODIFIED_BY = int.Parse(GetUserId());
							objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
							save=1;
						}
						else
						{
							objInfo.CD_LINE_ITEM_ID = -1;
						
						}
						objInfo.DEPOSIT_TYPE = hidDEPOSIT_TYPE.Value;

						alRecr.Add(objInfo);*/
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
			
			double  dblTapeTotal = 0;
			if(txtTapeTotal.Text.ToString().Trim() != "" )
				dblTapeTotal=Double.Parse(txtTapeTotal.Text);

			if(alRecr.Count == 0)
				return 0;
			if ( objDeposit.AddOtherDepositLineItems(alRecr, dblTapeTotal,confirm) > 0)
			{
				//saved successfully
				if ( save==1 )
				{
					//updated successfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "31");
				}
				else
				{
					//saved successfully
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "29");
				}
				
				BindGrid();
				setPagingText();
			}
			else
			{
				//error occured, showing the error message
				lblMessage.Text = ClsMessages.GetMessage(base.ScreenId , "20");
			}
			
			lblMessage.Visible = true;
			return 0;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

       

	}
}
