/******************************************************************************************
<Author						: -   
<Start Date					: -	
<End Date					: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By				: - 	
Modification History
<Modified Date				:  
<Modified By			    :  
<Purpose				    :  
*******************************************************************************************/ 
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
using System.Configuration;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for DistributeCashReceipt.
	/// </summary>
	public class DistributeCashReceipt : Cms.Account.AccountBase
	{		
		protected System.Web.UI.WebControls.Label lblMessage;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		

		protected System.Web.UI.WebControls.DataGrid dgrCashReceipt;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected DataTable dtTemp;	
		private int intFromUserId;
		public int validDistribution = 0;
		protected System.Web.UI.WebControls.Label lblINVOICE_NUMBER;
		protected System.Web.UI.WebControls.Label lblVENDOR_NAME;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Button btnPrevious;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidtotalPages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCurrentPage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLineItemId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCash;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected Int32 _currentPageNumber = 1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidpageDefaultsize;
		public int count=0;
		public string EntityName="";
		DataTable oldDtUpdate;
		public ClsDistributeCashReceipt objClsDistributeCashReceipt=null;
		Hashtable hshTblAccNo = new Hashtable();
		protected Cms.CmsWeb.Controls.CmsButton btnSavClos;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddNew;
		protected System.Web.UI.WebControls.Label lblPaymentFrom;
		protected double TotalSum;
		protected const string CALLEDFROM = "REINS";

		public DistributeCashReceipt()
		{
			
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["opener"] != null && Request.QueryString["opener"].ToString().Equals("AutoCheckMisc"))
				objClsDistributeCashReceipt = new  ClsDistributeCashReceipt(new TempDistribute()); 	
			else
				objClsDistributeCashReceipt = new ClsDistributeCashReceipt();
			
			if (Request.QueryString["GROUP_TYPE"] == "VEN")// || Request.QueryString["GROUP_TYPE"] == "CHQ" )
				btnClose.Attributes.Add("onclick","javascript:window.opener.parent.RefreshWebgrid(1);refreshParent();");	
			else if( (Request.QueryString["SUB_CALLED_FROM"] == "MISC") || (Request.QueryString["SUB_CALLED_FROM"] == "REINS") )
				btnClose.Attributes.Add("onclick","javascript:window.opener.location=window.opener.location;window.close();");
				//btnClose.Attributes.Add("onclick","javascript:window.opener.location.reload();window.close();");	
			else
				btnClose.Attributes.Add("onclick","javascript:window.close();");	

			
			


			btnSave.Attributes.Add("onclick","javascript: return DoValidationCheck();");	
			btnSavClos.Attributes.Add("onclick","Javascript: return DoValidationCheck();");
			btnDelete.Attributes.Add("onclick","javascript: return  DeleteRows();");
			btnReset.Attributes.Add("onclick","Javascript: return FuncReset();");
			

			intFromUserId =	int.Parse(GetUserId());
			SetScreenId();
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			btnDelete.CmsButtonClass	=   CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;
			btnClose.CmsButtonClass		=   CmsButtonType.Execute;
			btnClose.PermissionString	=	gstrSecurityXML;
			btnSavClos.CmsButtonClass	=   CmsButtonType.Execute;
			btnSavClos.PermissionString	=	gstrSecurityXML;
			
	
			hidpageDefaultsize.Value=(ConfigurationSettings.AppSettings["CoverageRows"]).ToString(); 
			if(Request.QueryString["GROUP_TYPE"].ToString().Equals("CHQ"))
				EntityName = "Check";
			else if(Request.QueryString["GROUP_TYPE"].ToString().Equals("VEN"))
				EntityName = "Vendor";
			else if(Request.QueryString["GROUP_TYPE"].ToString().Equals("DEP"))
				EntityName = "Cash";
			else if(Request.QueryString["GROUP_TYPE"].ToString().Equals("BRN"))
				EntityName = "Bank";
			if (!IsPostBack)
			{
				GetQueryString();
				//CheckCash(); 
				int pSize=dgrCashReceipt.PageSize;
				BindGridPage(1,dgrCashReceipt.PageSize);
				
				
			}

			CalledFromRegister();
		}


		private void CalledFromRegister()
		{
			if (Request.QueryString["CalledFrom"] == "Register")	
			{
				btnSavClos.Enabled=false;
				btnSave.Enabled=false;
				btnDelete.Enabled=false;
			}

		}

		private void SetScreenId()
		{
			
			switch(Request.QueryString["GROUP_TYPE"].ToString())
			{
				case "CHQ":
					base.ScreenId="210_0_0";
					break;
				case "VEN":
					base.ScreenId="190_0_0";
					break;
				case "DEP":
					base.ScreenId="";
					break;
				default:
					base.ScreenId="911";
					break;
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
			this.dgrCashReceipt.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgrCashReceipt_ItemDataBound);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSavClos.Click += new System.EventHandler(this.btnSavClos_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void GetQueryString()
		{
			if (Request.QueryString["GROUP_ID"] != null && Request.QueryString["GROUP_ID"].ToString() != "")
			{ 						
				hidLineItemId.Value=Request.QueryString["GROUP_ID"].ToString(); 
			}				
			if (Request.QueryString["DISTRIBUTION_AMOUNT"]!=null && Request.QueryString["DISTRIBUTION_AMOUNT"].ToString() != "")
			{ 						
				hidCash.Value=Request.QueryString["DISTRIBUTION_AMOUNT"].ToString();
			}
			if (Request.QueryString["GROUP_TYPE"] != null )
			{
				hidCalledFrom.Value = Request.QueryString["GROUP_TYPE"].ToString();
			}	
			//Itrack #3823
			/*if (Request.QueryString["CalledFrom"] == "EditC")	
				hidCalledFrom.Value = Request.QueryString["CalledFrom"].ToString();*/

			if (Request.QueryString["PAYMENT_FROM"]!=null && Request.QueryString["PAYMENT_FROM"].ToString() != "")
			{ 						
				lblPaymentFrom.Text = Request.QueryString["PAYMENT_FROM"].ToString();
			}
			if (Request.QueryString["VENDOR_NAME"]!=null && Request.QueryString["VENDOR_NAME"].ToString() != "")
			{ 						
				lblVENDOR_NAME.Text = Request.QueryString["VENDOR_NAME"].ToString();
			}
			if (Request.QueryString["INVOICE_NUMBER"]!=null && Request.QueryString["INVOICE_NUMBER"].ToString() != "")
			{ 						
				lblINVOICE_NUMBER.Text = Request.QueryString["INVOICE_NUMBER"].ToString();
			}
		}

		public void dgrCashReceipt_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			DataTable dtTempDdl;
			dtTempDdl=BindDropDown();
			
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
	
				int index = e.Item.ItemIndex + 2;	
				TextBox txtAccount = (TextBox)e.Item.FindControl("txtAccount");	
				TextBox txtPercentage = (TextBox)e.Item.FindControl("txtPercentage");
				TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
				DropDownList cmbAccount=(DropDownList)e.Item.FindControl("cmbAccount");
				CheckBox chk=(CheckBox)e.Item.FindControl("chkSelect");
				txtAccount.Attributes.Add("onBlur","JavaScript:searchAccount('" + txtAccount.ClientID +"','" + cmbAccount.ClientID +"');");
				cmbAccount.Attributes.Add("onchange","JavaScript:fillAccount('" + txtAccount.ClientID +"','" + cmbAccount.ClientID +"');");
				cmbAccount.Attributes.Add("onfocus","JavaScript:fillAccount('" + txtAccount.ClientID +"','" + cmbAccount.ClientID +"');");
				//Page.RegisterStartupScript("fillAmt","<script>fillAccount('" + txtAccount.ClientID +"','" + cmbAccount.ClientID +"');</script>");
				if(dgrCashReceipt.DataKeys[e.Item.ItemIndex].ToString() != "")
				{
					chk.Checked = true;
					ClientScript.RegisterStartupScript(this.GetType(),"fillAmt","<script>fillAccNumTextBox('" + chk.ClientID + "','" + txtAccount.ClientID +"','" + cmbAccount.ClientID +"');</script>");
				//	btnReset.Attributes.Add("onclick","Javascript: return FuncReset();fillAccNumTextBox('" + chk.ClientID + "','" + txtAccount.ClientID +"','" + cmbAccount.ClientID +"');");
				}
				else
					chk.Checked = false;

				if(txtAmount.Text.Length>0)
					chk.Visible = true;
				else
					chk.Visible = false;
				
				if(hshTblAccNo.Count >0)
				{
					if(hshTblAccNo.ContainsKey(e.Item.ItemIndex))
					{
						string strAccNo = hshTblAccNo[e.Item.ItemIndex].ToString();
						if (strAccNo != "")
						{
							txtAccount.Text = strAccNo;
						}
					}
				}

						
				txtPercentage.Attributes.Add("onchange","CalcAmount('"+txtPercentage.ClientID+"','"+txtAmount.ClientID+"');");
				txtPercentage.Attributes.Add("ONBLUR","formatAmount('"+txtAmount.ClientID+"');");
				txtAmount.Attributes.Add("onchange","CalcPercentage('"+txtAmount.ClientID+"','"+txtPercentage.ClientID+"');");

				RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revPercentage");
				revValidator.ErrorMessage = ClsMessages.FetchGeneralMessage("493");
				revValidator.ValidationExpression = aRegExpCurrencyformat;
	
				RegularExpressionValidator revValidator1 = (RegularExpressionValidator) e.Item.FindControl("revAmount");
				revValidator1.ErrorMessage = ClsMessages.FetchGeneralMessage("116");
				revValidator1.ValidationExpression = aRegExpCurrencyformat;

				DataRowView drv=(DataRowView)e.Item.DataItem;
				DropDownList ddl=(DropDownList)e.Item.FindControl("cmbAccount");
				//Modfied Itrack #3769
				if(Request.QueryString["SUB_CALLED_FROM"]!=null && Request.QueryString["SUB_CALLED_FROM"]!="")
				{
					if(Request.QueryString["SUB_CALLED_FROM"] == CALLEDFROM )
					     Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAllGLAccountsInDropDownReins(ddl);
					else
						Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAllGLAccountsInDropDown(ddl);
				}
				else
                    Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAllGLAccountsInDropDown(ddl);


			
				ddl.SelectedValue = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"ACCOUNT_ID"));
				if (dtTemp.Rows.Count > count)
				{
					if (Session["flagMode"].ToString() == "Update" && dtTemp.Rows[count][3].ToString() != "")
					{
						ddl.SelectedValue = Convert.ToString(dtTemp.Rows[count][3]);
						count=count+1;
					}
				}
				
			}		
			
		}

		public DataTable BindDropDown()
		{
			return ClsDistributeCashReceipt.FetchGLAccounts();
		}		
		public void BindGridPage(int currentPage,int pageSize)
		{			
			int totalRecord;
			//Double totalPages = 1;
			Session["flagMode"]="";
			hidCurrentPage.Value=currentPage.ToString();
			
			if (int.Parse(hidCurrentPage.Value) > 1)
			{
				btnPrevious.Enabled = true;
			}
			else
			{
				btnPrevious.Enabled = false;
			}
			
			//dtTemp=ClsDistributeCashReceipt.FetchPagedGLAccounts(currentPage,pageSize,out totalRecord);			
			dtTemp = objClsDistributeCashReceipt.FetchDistributionDetail(int.Parse(hidLineItemId.Value),hidCalledFrom.Value,currentPage,pageSize,out totalRecord, out TotalSum);
			ViewState["dtOldData"] = dtTemp;
			
			
			if (dtTemp.Rows.Count > 0)
			{
				Session["flagMode"]="Update";
				btnDelete.Enabled =true;
			}
			else
			{
				Session["flagMode"]="Save";
				btnDelete.Enabled =false;
			}

	
			if (Session["flagMode"].ToString() == "Update")
			{
				//dgrCashReceipt.DataKeyField="IDEN_ROW_ID";
				if (dtTemp.Rows.Count < int.Parse(hidpageDefaultsize.Value))
				{
					int addMoreRows=int.Parse(hidpageDefaultsize.Value)-dtTemp.Rows.Count;

					if (addMoreRows > 1)
					{
						btnNext.Enabled = false;
					}
					else
					{
						btnNext.Enabled = true;
					}

					for (int i=0;i < addMoreRows ; i++)
					{
						DataRow dr=dtTemp.NewRow();
						dr["IDEN_ROW_ID"]=DBNull.Value;	
						dr["GROUP_ID"]=DBNull.Value;		
						dr["GROUP_TYPE"]=DBNull.Value;	
						dr["ACCOUNT_ID"]=DBNull.Value;	
						dr["DISTRIBUTION_PERCT"]=DBNull.Value;	
						dr["DISTRIBUTION_AMOUNT"]=DBNull.Value;
						dr["NOTE"]=DBNull.Value;
						dtTemp.Rows.Add(dr);

					}
					dgrCashReceipt.DataSource=dtTemp.DefaultView;
					dgrCashReceipt.DataBind();	
					//btnNext.Enabled=true;
				}
				else if (dtTemp.Rows.Count == int.Parse(hidpageDefaultsize.Value) || dtTemp.Rows.Count > int.Parse(hidpageDefaultsize.Value) )
				{	
					dgrCashReceipt.DataSource=dtTemp.DefaultView;
					dgrCashReceipt.DataBind();
					btnNext.Enabled=true;	
				}
			}
			else if (Session["flagMode"].ToString() == "Save")
			{
				try
				{
					for (int i=0;i<10;i++)
					{
						DataRow dr=dtTemp.NewRow();
						dr["IDEN_ROW_ID"]=DBNull.Value;	
						dr["GROUP_ID"]=DBNull.Value;
						dr["GROUP_TYPE"]=DBNull.Value;	
						dr["ACCOUNT_ID"]=DBNull.Value;	
						dr["DISTRIBUTION_PERCT"]=DBNull.Value;	
						dr["DISTRIBUTION_AMOUNT"]=DBNull.Value;
						dr["NOTE"]=DBNull.Value;
						dtTemp.Rows.Add(dr);
					}
					dgrCashReceipt.DataSource=dtTemp.DefaultView;
					dgrCashReceipt.DataBind();			
					btnNext.Enabled = false;
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}			

		}

		protected void Navigation_Click(Object sender,CommandEventArgs e )
		{			
			switch ( e.CommandName)
			{				
				case "Next":
					
					_currentPageNumber =int.Parse(hidCurrentPage.Value) + 1;
					lblMessage.Visible=false;
					//btnSave_Click(sender,e);		
					break;
				case "Previous":
					if (int.Parse(hidCurrentPage.Value) > 1)
					{
						_currentPageNumber =int.Parse(hidCurrentPage.Value) - 1;
						lblMessage.Visible=false;
					//	btnSave_Click(sender,e);
					}
					else
						_currentPageNumber =1;

					break;
			}
			
			BindGridPage(_currentPageNumber,dgrCashReceipt.PageSize);
			CalledFromRegister();
			
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			Save();
		}
		protected void Save()
		{
			

			
			//CheckBox chkBox;
			DropDownList ddl;
			TextBox txt;
			TextBox txtAcc;
			int ddlSelectedValue;
			int result=0;
			//bool flagCheck=false;
				
			DataTable dtAdd=new DataTable();
			dtAdd.Columns.Add("ACCOUNT_ID",typeof(int));
			dtAdd.Columns.Add("DISTRIBUTION_PERCT",typeof(double));
			dtAdd.Columns.Add("DISTRIBUTION_AMOUNT",typeof(double));
			dtAdd.Columns.Add("Note",typeof(string));

			DataTable dtUpdate=new DataTable();
			dtUpdate.Columns.Add("IDEN_ROW_ID",typeof(int));
			dtUpdate.Columns.Add("ACCOUNT_ID",typeof(int));
			dtUpdate.Columns.Add("DISTRIBUTION_PERCT",typeof(double));
			dtUpdate.Columns.Add("DISTRIBUTION_AMOUNT",typeof(double));
			dtUpdate.Columns.Add("Note",typeof(string));
			hshTblAccNo.Clear();
					
			try
			{				
					
				foreach(DataGridItem dgi in dgrCashReceipt.Items)
				{
					//int.Parse(dgrCashReceipt.DataKeys[dgi.ItemIndex].ToString());
							
					if(dgrCashReceipt.DataKeys[dgi.ItemIndex].ToString() == "")
					{
						//Add case.
						txt=(TextBox)dgi.Cells[3].FindControl("txtAmount");
						if(txt.Text.Length<=0)
							continue;
						//						chkBox=(CheckBox)dgi.FindControl("chkSelect");
						//						if (chkBox != null && chkBox.Checked)
						//						{
						//							flagCheck=true;
						DataRow dr=dtAdd.NewRow();
						ddl=(DropDownList)dgi.Cells[1].FindControl("cmbAccount");
						if(ddl.SelectedValue == "")
							continue;
						else
							ddlSelectedValue=int.Parse(ddl.SelectedValue);
						dr["ACCOUNT_ID"]=ddlSelectedValue;
						txt=(TextBox)dgi.Cells[2].FindControl("txtPercentage");
						dr["DISTRIBUTION_PERCT"]=txt.Text;
						txt=(TextBox)dgi.Cells[3].FindControl("txtAmount");
						dr["DISTRIBUTION_AMOUNT"]=txt.Text;
						txt=(TextBox)dgi.Cells[4].FindControl("txtNote");
						dr["Note"]=txt.Text;
						dtAdd.Rows.Add(dr);
						txtAcc=(TextBox)dgi.Cells[1].FindControl("txtAccount");
						hshTblAccNo.Add(dgi.ItemIndex ,txtAcc.Text);
					}			
						//}
					else
					{
						//						chkBox=(CheckBox)dgi.FindControl("chkSelect");
						//						if (chkBox != null && chkBox.Checked)
						//						{
						//							flagCheck=true;
						DataRow dr=dtUpdate.NewRow();
						dr["IDEN_ROW_ID"]=int.Parse(dgrCashReceipt.DataKeys[dgi.ItemIndex].ToString());
						ddl=(DropDownList)dgi.Cells[1].FindControl("cmbAccount");
						ddlSelectedValue=int.Parse(ddl.SelectedValue);
						dr["ACCOUNT_ID"]=ddlSelectedValue;
						txt=(TextBox)dgi.Cells[2].FindControl("txtPercentage");
						if (txt.Text == "")
						{
							dr["DISTRIBUTION_PERCT"]=DBNull.Value;
						}
						else
						{
							dr["DISTRIBUTION_PERCT"]=txt.Text;
						}
								
						txt=(TextBox)dgi.Cells[3].FindControl("txtAmount");
						dr["DISTRIBUTION_AMOUNT"]=txt.Text;
						txt=(TextBox)dgi.Cells[4].FindControl("txtNote");
						dr["Note"]=txt.Text;
						txtAcc=(TextBox)dgi.Cells[1].FindControl("txtAccount");
						hshTblAccNo.Add(dgi.ItemIndex ,txtAcc.Text);
						dtUpdate.Rows.Add(dr);
						//}				
							
					}
				}
					
				//Adding new records
				if (dtAdd.Rows.Count >0)
				{
					result=objClsDistributeCashReceipt.InsertDistributionDetail(Double.Parse(hidCash.Value),dtAdd,int.Parse(hidLineItemId.Value),hidCalledFrom.Value,intFromUserId);
				}

				//Updating previous records
				if (dtUpdate.Rows.Count >0)
				{
					oldDtUpdate = new DataTable();
					if(ViewState["dtOldData"]!=null)
					{
						oldDtUpdate = (DataTable)ViewState["dtOldData"];
					}					
						
					result=objClsDistributeCashReceipt.UpdateDistributionDetail(Double.Parse(hidCash.Value),hidCalledFrom.Value.ToString(),oldDtUpdate,dtUpdate,intFromUserId,int.Parse(hidLineItemId.Value));
				}

				//Refreshing grid
				BindGridPage(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));


				//Showing message
				if(result > 0 && dtUpdate.Rows.Count > 0) 
				{
					lblMessage.Visible=true;			
					lblMessage.Text="Updated successfully.";
					
				}
				else if (result > 0 && dtAdd.Rows.Count > 0) 
				{
					lblMessage.Visible=true;			
					lblMessage.Text="Saved successfully.";
					
				}
				else if (result == 0)
				{
					ClientScript.RegisterStartupScript(this.GetType(),"CloseWin","<script>javascript:alert('Please enter data to be saved.');</script>");	
					
				}
			}
					
			catch(Exception ex)
			{
				//gIntSaved=0;
				lblMessage.Visible=true;			
				lblMessage.Text=ex.Message;
					
			}	
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			CheckBox chkBox;
			DataTable dt=new DataTable();
			dt.Columns.Add("RowID",typeof(int));
			try
			{				
				foreach(DataGridItem dgi in dgrCashReceipt.Items)
				{
					chkBox=(CheckBox)dgi.FindControl("chkSelect");
					if (chkBox != null && chkBox.Checked)
					{
						if (dgrCashReceipt.DataKeys[dgi.ItemIndex].ToString() !="")
						{
							DataRow dr=dt.NewRow();
							dr["RowID"]=int.Parse(dgrCashReceipt.DataKeys[dgi.ItemIndex].ToString());	
							dt.Rows.Add(dr);
						}
					}			
				}
				if (dt.Rows.Count > 0)	
				{	
					//int result=objClsDistributeCashReceipt.DeleteDistributionDetail(dt);
					int result=objClsDistributeCashReceipt.DeleteDistributionDetail(Double.Parse(hidCash.Value),dt,int.Parse(hidLineItemId.Value),hidCalledFrom.Value,intFromUserId);
					BindGridPage(int.Parse(hidCurrentPage.Value),dgrCashReceipt.PageSize);											
					if(result>0)
					{
						lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("5");		
						lblMessage.Visible=true;	
					}
				}
			}
			catch(Exception ex)
			{
				//gIntSaved=0;
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
			}		
		}

		private void btnSavClos_Click(object sender, System.EventArgs e)
		{
			Save();
			if(Request.QueryString["GROUP_TYPE"] == "CHQ" && Request.QueryString["SUB_CALLED_FROM"] == "MISC")
				ClientScript.RegisterStartupScript(this.GetType(),"CloseWin","<script>javascript:window.opener.location=window.opener.location;window.close();</script>");
			else if (Request.QueryString["GROUP_TYPE"] == "VEN" )
                ClientScript.RegisterStartupScript(this.GetType(),"CloseWin", "<script>javascript:window.opener.parent.RefreshWebgrid(1);refreshParent();</script>");
			else if (Request.QueryString["GROUP_TYPE"] == "BRN" )
                ClientScript.RegisterStartupScript(this.GetType(),"CloseWin", "<script>javascript:window.opener.parent.RefreshWebgrid(1);refreshParent();</script>");
			else if(Request.QueryString["SUB_CALLED_FROM"] == "MISC")
                ClientScript.RegisterStartupScript(this.GetType(), "CloseWin", "<script>javascript:window.opener.location=window.opener.location;window.close();</script>");
			else if(Request.QueryString["SUB_CALLED_FROM"] == "REINS")
                ClientScript.RegisterStartupScript(this.GetType(),"CloseWin", "<script>javascript:window.opener.location=window.opener.location;window.close();</script>");
			else
                ClientScript.RegisterStartupScript(this.GetType(),"CloseWin", "<script>javascript:window.close();</script>");	
		}		
		}		
	}


