/******************************************************************************************
<Author					: -   Sumit Chhabra
<Start Date				: -	  29 May , 2006
<End Date				: -	
<Description			: -  Scheduled Items / Coverages for Inland Marine (Policy) at Claims
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Claims;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// Summary description for Coverages.
	/// </summary>
	public class AddActivityExpenseUmb : Cms.Claims.ClaimBase		
	{
		
		System.Resources.ResourceManager objResourceMgr;	
		
		
		
		protected int TotalDeductible=0;
		
		private double TotalOutstanding=0;
		const string SubHeading = "SubHeading";
		const string VehicleText = "Vehicle # : ";
		public static string ExpenseDataGridID="";		
		const int POLICY_PAYMENT_TABLE=0;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capADDITIONAL_EXPENSE;
		protected System.Web.UI.WebControls.TextBox txtADDITIONAL_EXPENSE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADDITIONAL_EXPENSE;
		protected System.Web.UI.WebControls.DataGrid dgExpense;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvCLAIM_DESCRIPTION;
//		protected System.Web.UI.WebControls.Label capACTION_ON_PAYMENT;
//		protected System.Web.UI.WebControls.DropDownList cmbACTION_ON_PAYMENT;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;
//		public DataTable gDtOldData=null;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_PAYMENT;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnPaymentBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalPayments;
		protected System.Web.UI.HtmlControls.HtmlTableRow trExpense;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyRowCount;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPayeeID;	
		
		
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="309_11";	

			btnPaymentBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnPaymentBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			//btnGoBack.CmsButtonClass		=	CmsButtonType.Read;
			//btnGoBack.PermissionString		=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddActivityExpenseUmb"  ,System.Reflection.Assembly.GetExecutingAssembly());
			// Put user code to initialize the page here
			
			string strClaimStatus = GetClaimStatus();
			if(strClaimStatus == CLAIM_STATUS_CLOSED)
				btnSave.Visible=false;
			else
				btnSave.Visible=true;


			if ( !Page.IsPostBack)
			{
				ExpenseDataGridID = dgExpense.ID;				
				SetCaptions();				
				GetQueryStringValues();
				GetClaimDetails();
//				LoadDropDowns();
				btnBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");
				txtADDITIONAL_EXPENSE.Attributes.Add("onBlur","this.value = formatCurrencyWithCents(this.value);javascript:CalculateTotalPayment();");
				cmbPAYMENT_METHOD.Attributes.Add("onChange","javascript:return cmbPAYMENT_METHOD_Change();");
				//btnGoBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");				
				//Temp
				//btnSave.Attributes.Add("onClick","javascript: return CompareAllOutstandingAndPayment();");
				//btnPaymentBreakdown.Attributes.Add("onClick","javascript: return OpenWindow();");				
				LoadAcntgDropDowns();
				GetOldData();	
				//GetActivityDate();
				GetActivityDescription();				
			}		
			//SetClaimTop();
		}
		
		private void GetClaimDetails()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsTemp = objActivity.GetClaimDetails(int.Parse(hidCLAIM_ID.Value));
			if (dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
			{
				DataRow dr = dsTemp.Tables[0].Rows[0];
				hidCustomerID.Value = dr["CUSTOMER_ID"].ToString();
				hidPolicyID.Value = dr["POLICY_ID"].ToString();
				hidPolicyVersionID.Value = dr["POLICY_VERSION_ID"].ToString();
				hidLOB_ID.Value = dr["POLICY_LOB"].ToString();
				//hidSTATE_ID.Value = dr["STATE_ID"].ToString();
			}
		}

		private void GetActivityDescription()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsActivity = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			if(dsActivity.Tables[0].Rows[0]["DESCRIPTION"]!=null)
				txtCLAIM_DESCRIPTION.Text = dsActivity.Tables[0].Rows[0]["DESCRIPTION"].ToString();
		}

//		private void LoadDropDowns()
//		{
//			DataTable dtTransactionCodes = ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.CLAIM_TRANSACTION_CODE,(int)enumTransactionLookup.EXPENSE_PAYMENT);
//			if(dtTransactionCodes!=null && dtTransactionCodes.Rows.Count>0)
//			{
//				cmbACTION_ON_PAYMENT.DataSource =  dtTransactionCodes;
//				cmbACTION_ON_PAYMENT.DataTextField="DETAIL_TYPE_DESCRIPTION";
//				cmbACTION_ON_PAYMENT.DataValueField="DETAIL_TYPE_ID";
//				cmbACTION_ON_PAYMENT.DataBind();
//				cmbACTION_ON_PAYMENT.SelectedIndex = 1 ;//Set listbox selected to Reduce Reserves by payment amount 
//			}
//		}	

		

		private void GetOldData()
		{
			DataSet dsOldData = ClsActivityExpense.GetOldDataForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			//when the claim id is null/0/'', we have no data at claim payment table, 
			//we need to fetch data from reserve table as the user has come to this page for the first time
			if(dsOldData==null ||  dsOldData.Tables.Count<1 || dsOldData.Tables[0]==null || dsOldData.Tables[0].Rows.Count<1)
			{
				ClsReserveDetails  objReserveDetails = ClsReserveDetails.CreateReserveObject(hidLOB_ID.Value);
				dsOldData = objReserveDetails.GetOldData(hidCLAIM_ID.Value);
				if(dsOldData==null || dsOldData.Tables.Count<1)
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
					lblMessage.Visible = true;
					dgExpense.Visible = false;										
					trTotalPayments.Attributes.Add("style","display:none");
					trExpense.Attributes.Add("style","display:none");
					trBody.Attributes.Add("style","display:none");
					btnSave.Visible = false;
					return;
				}
				//Add columns for Payment Amount and Action on Payment as they are not present at reserve table..
				for(int i=0;i<dsOldData.Tables.Count;i++)
				{
					DataColumn dtCol = new DataColumn("PAYMENT_AMOUNT");
					dsOldData.Tables[i].Columns.Add(dtCol);
//					dtCol = new DataColumn("ACTION_ON_PAYMENT");
//					dsOldData.Tables[i].Columns.Add(dtCol);
					dtCol = new DataColumn("EXPENSE_ID");
					dsOldData.Tables[i].Columns.Add(dtCol);				
				}
				hidOldData.Value = "";
			}	
			else
			{
				hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);				
				LoadData(dsOldData.Tables[0]);
			}

			BindGrid(dsOldData);
		}

		
		private void LoadData(DataTable dtOldData)
		{
			if(dtOldData!=null && dtOldData.Rows.Count>0)
			{
				if(dtOldData.Rows[0]["EXPENSE_ACTION_ON_PAYMENT"]!=null && dtOldData.Rows[0]["EXPENSE_ACTION_ON_PAYMENT"].ToString()!="")
					hidACTION_ON_PAYMENT.Value = dtOldData.Rows[0]["EXPENSE_ACTION_ON_PAYMENT"].ToString();
				if(dtOldData.Rows[0]["ADDITIONAL_EXPENSE"]!=null && dtOldData.Rows[0]["ADDITIONAL_EXPENSE"].ToString()!="")
					txtADDITIONAL_EXPENSE.Text = dtOldData.Rows[0]["ADDITIONAL_EXPENSE"].ToString();
				if(dtOldData.Rows[0]["DrAccts"]!=null && dtOldData.Rows[0]["DrAccts"].ToString()!="")
					cmbDrAccts.SelectedValue = dtOldData.Rows[0]["DrAccts"].ToString();

				if(dtOldData.Rows[0]["CrAccts"]!=null && dtOldData.Rows[0]["CrAccts"].ToString()!="")
					cmbCrAccts.SelectedValue = dtOldData.Rows[0]["CrAccts"].ToString();
				if(dtOldData.Rows[0]["EXPENSE_PAYMENT_METHOD"]!=null && dtOldData.Rows[0]["EXPENSE_PAYMENT_METHOD"].ToString()!="")
					cmbPAYMENT_METHOD.SelectedValue = dtOldData.Rows[0]["EXPENSE_PAYMENT_METHOD"].ToString();

				if(dtOldData.Rows[0]["CHECK_NUMBER"]!=null && dtOldData.Rows[0]["CHECK_NUMBER"].ToString()!="")
					txtCHECK_NUMBER.Text = dtOldData.Rows[0]["CHECK_NUMBER"].ToString();
			}
		}

		
		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capPAYMENT_METHOD.Text	=		objResourceMgr.GetString("cmbPAYMENT_METHOD");
			//capACTIVITY_DATE.Text 		=		objResourceMgr.GetString("lblACTIVITY_DATE");			
			//capTOTAL_OUTSTANDING.Text	=		objResourceMgr.GetString("txtTOTAL_OUTSTANDING");
			//capTOTAL_PAYMENT.Text 		=		objResourceMgr.GetString("txtTOTAL_PAYMENT");
			capCLAIM_DESCRIPTION.Text 		=		objResourceMgr.GetString("txtCLAIM_DESCRIPTION");
			//capPAYEE.Text 				=		objResourceMgr.GetString("cmbPAYEE");
//			capACTION_ON_PAYMENT.Text 	=		objResourceMgr.GetString("cmbACTION_ON_PAYMENT");
			capADDITIONAL_EXPENSE.Text 	=		objResourceMgr.GetString("txtADDITIONAL_EXPENSE");
			//revADDITIONAL_EXPENSE.ValidationExpression	= aRegExpCurrencyformat;//Done by Sibin on 11 Feb 09 for Itrack Issue 5385
			revADDITIONAL_EXPENSE.ValidationExpression	= aRegExpDoublePositiveZero;
			revADDITIONAL_EXPENSE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
			
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			if(dsData==null || dsData.Tables.Count<1 || dsData.Tables[POLICY_PAYMENT_TABLE]==null || dsData.Tables[POLICY_PAYMENT_TABLE].Rows.Count<1)
			{
				lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
				lblMessage.Visible = true;
				dgExpense.Visible = false;
				return;
			}			

			if(dsData!=null && dsData.Tables.Count>POLICY_PAYMENT_TABLE && dsData.Tables[POLICY_PAYMENT_TABLE]!=null && dsData.Tables[POLICY_PAYMENT_TABLE].Rows.Count>0)
			{
				dgExpense.DataSource = dsData.Tables[POLICY_PAYMENT_TABLE];
				dgExpense.DataBind();	
				hidPolicyRowCount.Value = dsData.Tables[POLICY_PAYMENT_TABLE].Rows.Count.ToString();
			}
			else
			{
				dgExpense.Visible = false;
				trExpense.Attributes.Add("style","display:none");
			}

			
			if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
				txtTOTAL_OUTSTANDING.Text=Double.Parse(TotalOutstanding.ToString()).ToString("N");
			
		}

		private void dgExpense_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				Label lblOutstanding = (Label)e.Item.FindControl("lblOUTSTANDING");								
				Label lblRI_Reserve = (Label)e.Item.FindControl("lblRI_RESERVE");								
				
				
				TextBox txtPAYMENT_AMOUNT = (TextBox)(e.Item.FindControl("txtPAYMENT_AMOUNT"));
				if(txtPAYMENT_AMOUNT!=null)
				{
					txtPAYMENT_AMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();CompareOutstandingAndPayment('" + lblOutstanding.ClientID + "','" + txtPAYMENT_AMOUNT.ClientID + "');");
					if (DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT"))!="0") 
						txtPAYMENT_AMOUNT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT"));
					else
						txtPAYMENT_AMOUNT.Text = "";
				}				
				//((RangeValidator)(e.Item.FindControl("rngPAYMENT_AMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216"); - TO MOVE TO LOCAL VSS				
				//((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 11 Feb 09 for Itrack Issue 5385	
				//Done for Itrack Issue 6516 on 15 Oct 09
				//((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpDoublePositiveZero;
				((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpDoublePositiveNonZero;
				((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 11 Feb 09 for Itrack Issue 5385
				
				if (DataBinder.Eval(e.Item.DataItem,"RI_RESERVE") != System.DBNull.Value)
					lblRI_Reserve.Text = Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"RI_RESERVE"))).ToString("N");
				else
					lblRI_Reserve.Text = "";


				//if(lblOutstanding.Text.Trim()!="" && int.Parse(lblOutstanding.Text.replace(/\,/g,''))>0)
				if(lblOutstanding!=null && DataBinder.Eval(e.Item.DataItem,"OUTSTANDING") != System.DBNull.Value)
				{
					//string strOutstanding = lblOutstanding.Text.replace(/\,/g,'');
					//lblOutstanding.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"));
					lblOutstanding.Text = Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"))).ToString("N");
					TotalOutstanding += Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING")));					
				}
				else
				{
					txtPAYMENT_AMOUNT.Attributes.Add("readOnly","true");
					txtPAYMENT_AMOUNT.Attributes.Add("style","background-color:#C0C0C0;");
				}			
				
				//We have to allow the user to enter values exceeding outstanding amount
//				else
//				{
//					txtPAYMENT_AMOUNT.Attributes.Add("readOnly","true");
//				}								
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);			
			this.dgExpense.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgExpense_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")			
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();				
			else
				hidCLAIM_ID.Value = "0";

			if(Request["ACTIVITY_ID"]!=null && Request["ACTIVITY_ID"].ToString()!="")
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();
			else
				hidACTIVITY_ID.Value = "0";
			if(Request["ACTION_ON_PAYMENT"]!=null && Request["ACTION_ON_PAYMENT"].ToString()!="")
				hidACTION_ON_PAYMENT.Value = Request["ACTION_ON_PAYMENT"].ToString();
			else
				hidACTION_ON_PAYMENT.Value = "0";

			if(GetLOBID()!=null && GetLOBID()!="")
				hidLOB_ID.Value = GetLOBID();								

		}		

		private void PopulateArray(ArrayList aPaymentList,DataGrid dtGrid)
		{
			foreach(DataGridItem dgi in dtGrid.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					if(dtGrid.ID.ToUpper().Equals(ExpenseDataGridID.ToUpper()))
					{
						Label lblCOV_DESC = (Label)(dgi.FindControl("lblCOV_DESC"));
						if(lblCOV_DESC!=null && lblCOV_DESC.Text.IndexOf(VehicleText)!=-1)
							continue;
					}
					//Model Object
					ClsActivityExpenseInfo objActivityExpenseInfo = new	 ClsActivityExpenseInfo();
					
					TextBox txtPAYMENT_AMOUNT = (TextBox)(dgi.FindControl("txtPAYMENT_AMOUNT"));
					
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					Label lblEXPENSE_ID = (Label)(dgi.FindControl("lblEXPENSE_ID"));
					Label lblVEHICLE_ID = (Label)(dgi.FindControl("lblVEHICLE_ID"));
					
					if(hidACTION_ON_PAYMENT.Value!="")
						objActivityExpenseInfo.ACTION_ON_PAYMENT = int.Parse(hidACTION_ON_PAYMENT.Value);

					objActivityExpenseInfo.IS_ACTIVE="Y";
					objActivityExpenseInfo.CREATED_BY = int.Parse(GetUserId());
					objActivityExpenseInfo.CREATED_DATETIME = System.DateTime.Now;
					objActivityExpenseInfo.MODIFIED_BY = int.Parse(GetUserId());
					objActivityExpenseInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
					objActivityExpenseInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					objActivityExpenseInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);					

					if(lblRESERVE_ID!=null && lblRESERVE_ID.Text.Trim()!="")
						objActivityExpenseInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());
					if(txtPAYMENT_AMOUNT!=null && txtPAYMENT_AMOUNT.Text.Trim()!="")
						objActivityExpenseInfo.PAYMENT_AMOUNT = Convert.ToDouble(txtPAYMENT_AMOUNT.Text.Trim());
					if(lblEXPENSE_ID!=null && lblEXPENSE_ID.Text.Trim()!="")
						objActivityExpenseInfo.EXPENSE_ID = int.Parse(lblEXPENSE_ID.Text.Trim());										
					if(txtADDITIONAL_EXPENSE.Text.Trim()!="")
						objActivityExpenseInfo.ADDITIONAL_EXPENSE = Convert.ToDouble(txtADDITIONAL_EXPENSE.Text.Trim());
					if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
						objActivityExpenseInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);
					if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
						objActivityExpenseInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);
					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="")
						objActivityExpenseInfo.PAYMENT_METHOD = Convert.ToInt32(cmbPAYMENT_METHOD.SelectedValue);
					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="" && cmbPAYMENT_METHOD.SelectedItem.Value=="11979")
						objActivityExpenseInfo.CHECK_NUMBER = txtCHECK_NUMBER.Text.Trim() ;					
					aPaymentList.Add(objActivityExpenseInfo);
				}
			}
		}
		private void LoadAcntgDropDowns()
		{
			//DataSet DS = BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims(Convert.ToInt32(cmbACTION_ON_PAYMENT.SelectedValue));
			DataSet DS = BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims(Convert.ToInt32(hidACTION_ON_PAYMENT.Value));
			if(DS==null || DS.Tables[0]==null || DS.Tables[0].Rows.Count<1)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");
				lblMessage.Visible = true;
				return;
			}
			//If more then one acct exist then add a BLANK Option and default BLANK.
			//If single account exists then default that and disable dropdown

			cmbDrAccts.DataSource=DS.Tables[0];
			cmbDrAccts.DataTextField="DESC_TO_SHOW";
			cmbDrAccts.DataValueField="ACCOUNT_ID";
			cmbDrAccts.DataBind();

			ListItem lItem = new ListItem("","");
			if (DS.Tables[0].Rows.Count > 1)
			{
				//ListItem LI_DR = new ListItem("","");
				//cmbDrAccts.Items.Add(LI_DR);
				cmbDrAccts.Items.Insert(0,lItem);
				//cmbDrAccts.ClearSelection();				
				//cmbDrAccts.Items.FindByValue("").Selected = true;
			}
			else
			{
				cmbDrAccts.Enabled=false;
			}

			
			cmbCrAccts.DataSource = DS.Tables[1];
			cmbCrAccts.DataTextField="DESC_TO_SHOW";
			cmbCrAccts.DataValueField="ACCOUNT_ID";
			cmbCrAccts.DataBind();


			if (DS.Tables[0].Rows.Count > 1)
			{
				//ListItem LI_CR = new ListItem();
				//cmbCrAccts.Items.Add(LI_CR);
				cmbCrAccts.Items.Insert(0,lItem);
				//cmbCrAccts.ClearSelection();
				//cmbCrAccts.Items.FindByValue("").Selected = true;
			}
			else
			{
				cmbCrAccts.Enabled=false;
			}
			ClsPayment objPayment = new ClsPayment();
			objPayment.PopulatePaymentMethodDropDown(cmbPAYMENT_METHOD,"PYMTD",((int)Cms.CmsWeb.cmsbase.enumPaymentMethod.EFT).ToString());
		}		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				int PAYMENT_ACTION;
				//string strPAYEE;				
				ClsActivityExpense objExpense = new ClsActivityExpense();
				ArrayList ExpenseArrayList = new ArrayList();				
				PopulateArray(ExpenseArrayList,dgExpense);

				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					intRetVal = objExpense.Add(ExpenseArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");						
				}
				else
				{
					intRetVal = objExpense.Update(ExpenseArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");					
				}					
				if(intRetVal>0)
				{			
					//strPAYEE = GetPayees();
					//ClsClaimsNotification.UpdateClaimDescription(hidCLAIM_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					ClsActivity objActivity = new ClsActivity();
					objActivity.UpdateActivityDescription(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					if(hidACTION_ON_PAYMENT.Value!="")
						PAYMENT_ACTION = int.Parse(hidACTION_ON_PAYMENT.Value);
					else
						PAYMENT_ACTION = -1;
					ClsActivityExpense.UpdateReserveActivity(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,PAYMENT_ACTION.ToString());					
					//RegisterStartupScript("ReLoadTab","<script>GoBack('PaymentDetailsTab.aspx');</script>");			
					GetOldData();
					
				}				
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
					
				}
				lblMessage.Visible = true;	
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);								
			}
			finally
			{
				
			}
		}		
	}
}
