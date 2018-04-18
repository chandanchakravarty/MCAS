/*******************************************************************
	<Author					:	
	<Created Date			:
	<Purpose				:

	<Modified Date			: - 07/02/06
	<Modified By			: - Shafi
	<Purpose				: - Populate Calender  And Validation Checks
  
	<Modified Date			: - 03-03-2006
	<Modified By			: - Vijay Arora
	<Purpose				: - 
 ******************************************************************/
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
	/// Summary description for AG_CheckListing.
	/// </summary>
	public class AG_CheckListing :Cms.Account.AccountBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnCreateChecks;
		protected Cms.CmsWeb.Controls.CmsButton btnDisplay;
		protected System.Web.UI.WebControls.HyperLink hlkFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.TextBox txtPolicyNumber;
		protected System.Web.UI.WebControls.HyperLink hlkToDate;
		protected System.Web.UI.WebControls.Label capACCOUNT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected System.Web.UI.WebControls.Label lblMessage;
		public int NoOfRows=0;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.CustomValidator csvFromDate;
		protected System.Web.UI.WebControls.CustomValidator csvToDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDate;
		protected System.Web.UI.WebControls.CompareValidator cmpToDate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;		
		protected System.Web.UI.WebControls.DataGrid grdReconcileItems;
		protected System.Web.UI.WebControls.DropDownList cmbCheckType;
		protected System.Web.UI.WebControls.Label lblCheckType;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBnkACMappingXML;
		public string CancellationType;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvToDate;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvFromDate;
		public static string PriorDaysDiff; 
		public static int numberDiv;
		protected System.Web.UI.WebControls.CheckBox chkSelectAll;
		private int  Payment_days ; 
		private string CheckType = "";
		
		//public string columnHeader = "";
		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvACCOUNT_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			
			revFromDate.ValidationExpression =aRegExpDate;
			revToDate.ValidationExpression =  aRegExpDate;
			revFromDate.ErrorMessage       =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			cmpToDate.ErrorMessage         = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			csvToDate.ErrorMessage         =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			csvFromDate.ErrorMessage       =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			revToDate.ErrorMessage         =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
//			rfvFromDate.ErrorMessage	   =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("875");
//			rfvToDate.ErrorMessage		   =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("876");
		}
		#endregion


		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId  = "210_2";
			btnCreateChecks.CmsButtonClass		=	CmsButtonType.Execute;
			btnCreateChecks.PermissionString	=	gstrSecurityXML;
			btnDisplay.CmsButtonClass			=	CmsButtonType.Execute;
			btnDisplay.PermissionString			=	gstrSecurityXML;	
			btnCreateChecks.Attributes.Add("onclick","javascript:RemoveValidation();");
			
			btnBack.CmsButtonClass		= CmsButtonType.Write;
			btnBack.PermissionString	= gstrSecurityXML;

			
			SetErrorMessages();
			btnBack.Attributes.Add("OnClick","javascript:return back();");
			
			//hlkFromDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtFromDate,document.Form1.txtFromDate)");
			//hlkToDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtToDate,document.Form1.txtToDate)");

			hlkFromDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtFromDate,document.Form1.txtFromDate)");
			hlkToDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtToDate,document.Form1.txtToDate)");
				
			hlkFromDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtFromDate,document.forms[0].txtFromDate)");
			hlkToDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtToDate,document.forms[0].txtToDate)");
			
			cmbCheckType.Attributes.Add("OnChange","javascript:SetBankAccountDD();");
			btnDisplay.Attributes.Add("OnClick","javascript:return CheckDateDiff();");
			btnDisplay.Attributes.Add("OnClick","DateValidators();");
			btnCreateChecks.Attributes.Add("OnClick","return CheckboxValidate();");

            
			if(!IsPostBack)
			{
				if(Request.QueryString["TypeID"] != null)
				{
					if(Request.QueryString["TypeID"].ToString().Equals("6"))//Re-Insurance
						ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11199);
					else if(Request.QueryString["TypeID"].ToString().Equals("5"))//Claims
						ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11200);
					else
						ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,0);//General
				}
				else
				{
					ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID,11201);//General
				}
				txtToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
				string ToDate="";
				PriorDaysDiff = System.Configuration.ConfigurationManager.AppSettings.Get("PriorDaysDiff").ToString();
				if(txtToDate.Text.Trim() !="") 
					//ToDate =(Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(Convert.ToDouble(Convert.ToInt32(PriorDaysDiff)*-1)).ToString("MM/dd/yyyy")).ToString();
					ToDate = txtToDate.Text.Trim();
				BindGrid(null,ToDate,Get_ITEM_STATUS(),txtPolicyNumber.Text.Trim());				
				SetBankAccountDD();
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
			this.cmbCheckType.SelectedIndexChanged += new System.EventHandler(this.ddlCheckType_SelectedIndexChanged);
			this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
			this.grdReconcileItems.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdReconcileItems_ItemDataBound);
			this.btnCreateChecks.Click += new System.EventHandler(this.btnCreateChecks_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SetBankAccountDD()
		{
			int intFiscalID = 0;
			intFiscalID = ClsGeneralLedger.GetFiscalID();

			string strXML = ClsGeneralLedger.GetXmlForPageControls_Bnk_AC_Mapping("1",intFiscalID.ToString().Trim());
			hidBnkACMappingXML.Value =strXML;
			strXML = strXML.Replace("\r\n","");
			strXML = strXML.Replace("<NewDataSet>","");
			strXML = strXML.Replace("</NewDataSet>","");	
			
			XmlDocument  objXmlDocument  = new XmlDocument();			
			objXmlDocument.LoadXml(strXML);
			XmlNode Node = objXmlDocument.SelectSingleNode("Table/BNK_OVER_PAYMENT");
			if(Node != null)
			{
				XmlNodeList objXmlNode = objXmlDocument.GetElementsByTagName("BNK_OVER_PAYMENT");
				string strBNK_OVER_PAYMENT =  objXmlNode.Item(0).InnerText;
				cmbACCOUNT_ID.SelectedValue=(strBNK_OVER_PAYMENT);	
			}
		}
		protected string headers()
		{
			string header="";
			string Type;

			//Check For Query String If Not Exists Select The Value From Drop Down

			if(Request.QueryString["TypeID"] != null)
			{
				Type = Request.QueryString["TypeID"];
			}
			else
			{
				Type =cmbCheckType.SelectedValue;
			}
			switch(Type)
			{
				case "1":
					header = "Over Payment Amount";
					break;
					
				case "2":
					header = "Suspense Amount";
					break;
					
				case "3":
					header = "Cancellation and Change Premium  Payment  Amount";
					break;
					
				case "4":
					header = "Agency Commission";
					break;

				case "5":
					header = "Claims";
					break;
					
					
				case "6":
					header = "Reinsurance Premium";
					break;
					
				case "7":
					header = "Vendor";
					break;

				case "8":
					header = "Miscellaneous";
					break;
			}
			return header;


		}
		private void BindGrid(string fromDate,string toDate,string ITEM_STATUS,string polNum)
		{
			CheckType = ITEM_STATUS ; 

			DataSet objDataSet = ClsChecks.GetOpenItemsForCheck(fromDate,toDate,ITEM_STATUS,polNum);			               
			Payment_days   = Convert.ToInt32(objDataSet.Tables[1].Rows[0]["SYS_PAYMENT_DAYS"]);
			grdReconcileItems.DataSource =  objDataSet.Tables[0];
			try
			{
				grdReconcileItems.DataBind();
			}
			catch
			{
					grdReconcileItems.CurrentPageIndex=0;
					grdReconcileItems.DataBind();
					grdReconcileItems.Visible=true;
			}
			NoOfRows = objDataSet.Tables[0].Rows.Count;
			
			if(NoOfRows>0)
			{
			//	CancellationType = objDataSet.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString();
				btnCreateChecks.Enabled =  true;
			}
			else
				btnCreateChecks.Enabled =  false;
             //Added By Raghav For Itrack 4907
			if(ITEM_STATUS != "RP") 
			{
				grdReconcileItems.Columns[7].Visible=false;
			}
			else
			{
				grdReconcileItems.Columns[7].Visible=true;
			}
		}
		//Sorting 
		private void BindGridSort(string fromDate,string toDate,string ITEM_STATUS,string polNum,string sortExpr)
		{
			DataSet objDataSet = ClsChecks.GetOpenItemsForCheck(fromDate,toDate,ITEM_STATUS,polNum);
			//grdReconcileItems.DataSource =  objDataSet.Tables[0];

			CheckType = ITEM_STATUS ; 
			try
			{
				DataView dvSortView = new DataView(objDataSet.Tables[0]);
				if( (numberDiv%2) == 0)
					dvSortView.Sort = sortExpr + " " + "ASC";
				else
					dvSortView.Sort = sortExpr + " " + "DESC";
				numberDiv++;
				grdReconcileItems.DataSource = dvSortView;
				grdReconcileItems.DataBind();
			}
			catch
			{
				grdReconcileItems.CurrentPageIndex=0;
				grdReconcileItems.DataBind();
				grdReconcileItems.Visible=true;
			}
			NoOfRows = objDataSet.Tables[0].Rows.Count;
			if(NoOfRows>0)
			{
				//	CancellationType = objDataSet.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString();
				btnCreateChecks.Enabled =  true;
			}
			else
				btnCreateChecks.Enabled =  false;
		}


		private void btnDisplay_Click(object sender, System.EventArgs e)
		{
			string ToDate="";
			//string  PriorDaysDiff = System.Configuration.ConfigurationSettings.AppSettings.Get("PriorDaysDiff").ToString();
			if(txtToDate.Text.Trim() !="")
				//ToDate =(Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(Convert.ToDouble(Convert.ToInt32(PriorDaysDiff)*-1)).ToString("MM/dd/yyyy")).ToString();
				ToDate = txtToDate.Text.Trim();
			BindGrid(txtFromDate.Text,ToDate,Get_ITEM_STATUS(),txtPolicyNumber.Text.Trim());
		}

		

		private string Get_ITEM_STATUS()
		{
			string status="";
			string Type="";
			//Check For Query String If Not Exists Select The Value From Drop Down
			if(Request.QueryString["TypeID"] != null)
			{
				Type = Request.QueryString["TypeID"];
			}
			else
			{
				Type =cmbCheckType.SelectedValue;
			}
			switch(Type)
			{
				case "1"://Premium Refund Checks for Over Payment
					status = "OP";
					break;
				case "2"://Premium Refund Checks for Suspense Amount
					status = "RSP";
					break;
				case "3"://Premium Refund Checks for Return Premium Payment
					status = "RP";
					break;
				case "4"://Agency Commission checks
					status = "AC";
					break;
				case "5"://Claim Checks
					status = "CC";
					break;
				case "6"://Reinsurance Premium Checks
					status = "RE";
					break;
				case "7"://Vendor Checks
					status = "VC";
					break;
				case "8"://Miscellaneous (Other) Checks
					status = "MC";
					break;
			}
			return status;
		}


		private string GetCheckType()
		{
			string checkType="0";
			string Type="";
			//Check For Query String If Not Exists Select The Value From Drop Down
			if(Request.QueryString["TypeID"] != null)
			{
				Type = Request.QueryString["TypeID"];
			}
			else
			{
				Type =cmbCheckType.SelectedValue;
			}
			switch(Type)
			{
				case "1"://Premium Refund Checks for Over Payment
					checkType = ClsChecks.CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_OVER_PAYMENT;
					break;
				case "2"://Premium Refund Checks for Suspense Amount
					checkType = ClsChecks.CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_SUSPENSE_AMOUNT;
					break;
				case "3"://Premium Refund Checks for Return Premium Payment
					checkType = ClsChecks.CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_RETURN_PREMIUM_PAYMENT;
					break;
				case "4"://Agency Commission checks
					checkType = ClsChecks.CHECK_TYPES.AGENCY_COMMISSION_CHECKS;
					break;
				case "5"://Claim Checks
					checkType = ClsChecks.CHECK_TYPES.CLAIMS_CHECKS;
					break;
				case "6"://Reinsurance Premium Checks
					checkType = ClsChecks.CHECK_TYPES.REINSURANCE_PREMIUM_CHECKS;
					break;
				case "7"://Vendor Checks
					checkType = ClsChecks.CHECK_TYPES.VENDOR_CHECKS;
					break;
				case "8"://Miscellaneous (Other) Checks
					checkType = ClsChecks.CHECK_TYPES.MISCELLANEOUS_OTHER_CHECKS;
					break;
					
			}
			return checkType.ToString();
		}


		private void btnCreateChecks_Click(object sender, System.EventArgs e)
		{
			int intRetVal=1;
			ArrayList objchecks= new ArrayList();
			ArrayList OPEN_ITEM_IDs= new ArrayList();

			foreach(DataGridItem dgi in grdReconcileItems.Items)
			{
				
			
				CheckBox objCheckBox=null;
				Label objLabel=null;
				objCheckBox = (CheckBox)dgi.FindControl("chkSelect");
				if(objCheckBox.Checked)
				{
					if (grdReconcileItems.DataKeys[dgi.ItemIndex].ToString() !="")
					{
						ClsChecksInfo objcheck	=	new ClsChecksInfo();
						objcheck.OPEN_ITEM_ROW_ID = Convert.ToInt32(grdReconcileItems.DataKeys[dgi.ItemIndex]);

						objcheck.POLICY_ID		=	int.Parse(((HtmlInputHidden) dgi.FindControl("hidPOLICY_ID")).Value);
						objcheck.POLICY_VER_TRACKING_ID	=	int.Parse(((HtmlInputHidden) dgi.FindControl("hidPOLICY_VERSION_ID")).Value);
						objcheck.CUSTOMER_ID	=	int.Parse(((HtmlInputHidden) dgi.FindControl("hidCUSTOMER_ID")).Value);


						objcheck.ACCOUNT_ID		=	int.Parse(cmbACCOUNT_ID.SelectedValue);

						objLabel				=	(Label)dgi.FindControl("lblOP_AMOUNT");
						objcheck.CHECK_AMOUNT	=	double.Parse(objLabel.Text);

						objLabel				=	(Label)dgi.FindControl("lblCheckNumber");
						//objcheck.CHECK_NUMBER	=	double.Parse(objLabel.Text);
                        
                      
						OPEN_ITEM_IDs.Add(grdReconcileItems.DataKeys[dgi.ItemIndex]);

						objcheck.CHECK_TYPE		=	GetCheckType();
						objcheck.CHECK_ID		=   int.Parse(grdReconcileItems.DataKeys[dgi.ItemIndex].ToString());
						objcheck.CHECK_DATE		=	DateTime.Now;
						objcheck.CREATED_BY		= int.Parse(GetUserId());
						objcheck.CREATED_DATETIME = DateTime.Now;
						objcheck.MODIFIED_BY	= int.Parse(GetUserId());
						objcheck.LAST_UPDATED_DATETIME = DateTime.Now;
						objcheck.MANUAL_CHECK = "N";

						objLabel				=	(Label)dgi.FindControl("lblCANCELLATION_TYPE");
						CancellationType		=	objLabel.Text.ToString();
					
						if(CancellationType.Equals("Rescind") && CancellationType != null)
						{	
							DataSet objDS = ClsChecks.GetPayeeInfo(objcheck.CUSTOMER_ID,objcheck.POLICY_ID,objcheck.POLICY_VER_TRACKING_ID);
							
							objcheck.PAYEE_ENTITY_NAME = objDS.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
							objcheck.PAYEE_ADD1  = objDS.Tables[0].Rows[0]["AGENCY_ADD1"].ToString();
							objcheck.PAYEE_ADD2  = objDS.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();
							objcheck.PAYEE_CITY  = objDS.Tables[0].Rows[0]["AGENCY_CITY"].ToString();
							objcheck.PAYEE_STATE = objDS.Tables[0].Rows[0]["AGENCY_STATE"].ToString();
							objcheck.PAYEE_ZIP   = objDS.Tables[0].Rows[0]["AGENCY_ZIP"].ToString();
							
						}
							
						objchecks.Add(objcheck);	
					}											
				}
			}
		
			//intRetVal=new ClsChecks().Add(objchecks,OPEN_ITEM_IDs);//id of ACT_ACCOUNTS_POSTING_DETAILS
			if( intRetVal > 0 )			
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("884");
				btnDisplay_Click(sender,e);
			}
			else if(intRetVal == -2)
			{
				lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"4");
				//hidFormSaved.Value			=		"2";
			}
			else 
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
			}
			lblMessage.Visible = true;
						
		}


		private void grdReconcileItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{	
			if ( e.Item.ItemType ==  ListItemType.Header)
			{
				e.Item.Cells[6].Text=headers();			
			}
			//Added By Raghav For Itrack Issue #4969 
			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				Label lblPAYMENT_DATE = (Label)e.Item.FindControl("lblPAYMENT_DATE");
				if(lblPAYMENT_DATE.Text.Trim() != "" && CheckType == "RP") 
				{
					TimeSpan  ts = DateTime.Now - DateTime.Parse(lblPAYMENT_DATE.Text.Trim()); 
					if(ts.Days >= Payment_days)
					{
						e.Item.ForeColor = System.Drawing.Color.Red; 
					}
				}
			}
		
				/*		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
	
				CheckBox objchk = (CheckBox)e.Item.FindControl("chkSelect");
				objchk.Checked  = true;
			}*/
		}


		protected void grdReconcileItems_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			grdReconcileItems.CurrentPageIndex = e.NewPageIndex; 
			string ToDate="";
			//string  PriorDaysDiff = System.Configuration.ConfigurationSettings.AppSettings.Get("PriorDaysDiff").ToString();
			if(txtToDate.Text.Trim() !="")
				//ToDate =(Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(Convert.ToDouble(Convert.ToInt32(PriorDaysDiff)*-1)).ToString("MM/dd/yyyy")).ToString();
				ToDate = txtToDate.Text.Trim();
			//else
			//	ToDate =(Convert.ToDateTime(DateTime.Now.ToString().Trim()).AddDays(Convert.ToDouble(Convert.ToInt32(PriorDaysDiff)*-1)).ToString("MM/dd/yyyy")).ToString();

			//BindGrid(null,ToDate,Get_ITEM_STATUS(),txtPolicyNumber.Text.Trim());

			//Added on 21 March 2008 to preserve searching while Navigationg records
			string fromdate="";
				fromdate = txtFromDate.Text.Trim();
			BindGrid(fromdate,ToDate,Get_ITEM_STATUS(),txtPolicyNumber.Text.Trim());
		}

		private void ddlCheckType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			headers();
			string ToDate="";
			//string  PriorDaysDiff = System.Configuration.ConfigurationSettings.AppSettings.Get("PriorDaysDiff").ToString();
			if(txtToDate.Text.Trim() !="")
				//ToDate =(Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(Convert.ToDouble(Convert.ToInt32(PriorDaysDiff)*-1)).ToString("MM/dd/yyyy")).ToString();
				ToDate =txtToDate.Text.Trim();
			//else
			//	ToDate =(Convert.ToDateTime(DateTime.Now.ToString().Trim()).AddDays(Convert.ToDouble(Convert.ToInt32(PriorDaysDiff)*-1)).ToString("MM/dd/yyyy")).ToString();
			
			grdReconcileItems.CurrentPageIndex = 0;
			BindGrid(txtFromDate.Text,ToDate,Get_ITEM_STATUS(),txtPolicyNumber.Text.Trim());
		}

		//Sorting
		public void Sort_Grid(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs  e)
		{
			string ToDate="";
			if(txtToDate.Text.Trim() !="")
				ToDate =txtToDate.Text.Trim();
			BindGridSort(txtFromDate.Text,ToDate,Get_ITEM_STATUS(),txtPolicyNumber.Text.Trim(),e.SortExpression.ToString());
		}
		
	}	
}
