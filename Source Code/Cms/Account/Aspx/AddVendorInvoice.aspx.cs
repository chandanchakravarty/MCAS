/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	6/28/2005 10:27:46 AM
<End Date				: -	
<Description				: - 	Code behind for Vendor invoices.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Code behind for Vendor invoices.

<Modified Date			: - 25/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page 
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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Code behind for Vendor invoices.
	/// </summary>
	public class AddVendorInvoice : Cms.Account.AccountBase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbVENDOR_ID;
		protected System.Web.UI.WebControls.TextBox txtINVOICE_NUM;
		protected System.Web.UI.WebControls.TextBox txtREF_PO_NUM;
		protected System.Web.UI.WebControls.TextBox txtTRANSACTION_DATE;
		protected System.Web.UI.WebControls.TextBox txtDUE_DATE;
		protected System.Web.UI.WebControls.TextBox txtINVOICE_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtNOTE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;		

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvINVOICE_NUM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINVOICE_AMOUNT;

		protected System.Web.UI.WebControls.RegularExpressionValidator revINVOICE_NUM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREF_PO_NUM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTRANSACTION_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVENDOR_DETAILS;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revDUE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINVOICE_AMOUNT;
		protected System.Web.UI.WebControls.Label lblMessage;

		protected System.Web.UI.WebControls.HyperLink hlkTran_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkDue_DATE;
		//protected System.Web.UI.HtmlControls.HtmlImage imgTran_DATE;
	//	protected System.Web.UI.HtmlControls.HtmlImage imgDue_DATE;
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capVENDOR_ID;
		protected System.Web.UI.WebControls.Label capINVOICE_NUM;
		protected System.Web.UI.WebControls.Label capREF_PO_NUM;
		protected System.Web.UI.WebControls.Label capTRANSACTION_DATE;
		protected System.Web.UI.WebControls.Label capDUE_DATE;
		protected System.Web.UI.WebControls.Label capINVOICE_AMOUNT;
		protected System.Web.UI.WebControls.Label capNOTE;
		protected Cms.CmsWeb.Controls.CmsButton btnDistribute;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitPost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidINVOICE_ID;
		//protected Cms.CmsWeb.Controls.CmsButton btnApprove;
		protected System.Web.UI.WebControls.Image imgTran_DATE;
		protected System.Web.UI.WebControls.Image imgDue_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvDUE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDUE_DATE;
		
		protected System.Web.UI.WebControls.Table tblACT_VENDOR_INVOICES;
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		protected Cms.CmsWeb.Controls.CmsButton btnApprove;
		protected System.Web.UI.WebControls.Label capVENDORADDRESS;
		protected System.Web.UI.WebControls.Label lblVENDORADDRESS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVENDOR_ID;
		protected System.Web.UI.WebControls.Label capFISCAL_ID;
		protected System.Web.UI.WebControls.DropDownList cmbFISCAL_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFISCAL_ID;
		protected System.Web.UI.WebControls.CustomValidator csvTRANSACTION_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnCommitting;	//For #5534 on 12/3/09.

		//Defining the business layer class object
		ClsVendorInvoices  objVendorInvoices ;
		//END:*********** Local variables *************

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvVENDOR_ID.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			//rfvINVOICE_NUM.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvTRANSACTION_DATE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDUE_DATE.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvINVOICE_AMOUNT.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			revINVOICE_NUM.ValidationExpression			=	aRegExpAlphaNum;
			revREF_PO_NUM.ValidationExpression			=	aRegExpAlphaNum;
			revTRANSACTION_DATE.ValidationExpression	=	aRegExpDate;
			revDUE_DATE.ValidationExpression			=	aRegExpDate;
			revINVOICE_AMOUNT.ValidationExpression		=	aRegExpCurrencyformat;
			revINVOICE_NUM.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			revREF_PO_NUM.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revTRANSACTION_DATE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			revDUE_DATE.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			revINVOICE_AMOUNT.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			csvDUE_DATE.ErrorMessage					=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			rfvFISCAL_ID.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");

		}

		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			btnDistribute.Attributes.Add("onclick","javascript:return OpenDistributeWindow();");
			btnCommitPost.Attributes.Add("onclick","javascript:HideShowCommit();");
			txtINVOICE_AMOUNT.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtINVOICE_AMOUNT'));");
			cmbVENDOR_ID.Attributes.Add("onChange","javascript:return ShowVendorAddress('lblVENDORADDRESS','cmbVENDOR_ID');");  
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="190_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;			
//			btnApprove.CmsButtonClass		=	CmsButtonType.Write;
//			btnApprove.PermissionString		=	gstrSecurityXML;
			btnDistribute.CmsButtonClass	=	CmsButtonType.Execute;
			btnDistribute.PermissionString	=	gstrSecurityXML;
			btnCommitPost.CmsButtonClass	=	CmsButtonType.Write;
			btnCommitPost.PermissionString	=	gstrSecurityXML;
			//For #5534 on 12/03/09.
			btnCommitting.CmsButtonClass = CmsButtonType.Execute;
			btnCommitting.PermissionString = gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddVendorInvoice" ,System.Reflection.Assembly.GetExecutingAssembly());

			
			/*else
			{
				btnApprove.Enabled = false;
				btnCommitPost.Enabled = false;
			}*/
			hlkDue_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_VENDOR_INVOICES.txtDUE_DATE,document.ACT_VENDOR_INVOICES.txtDUE_DATE)");
			hlkTran_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_VENDOR_INVOICES.txtTRANSACTION_DATE,document.ACT_VENDOR_INVOICES.txtTRANSACTION_DATE)");
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["INVOICE_ID"]!=null && Request.QueryString["INVOICE_ID"].ToString().Length>0)
					CheckInvoiceStatus(Request.QueryString["INVOICE_ID"].ToString());
				//Cms.BusinessLayer.BlCommon.clsVendor.GetVendorNamesInDropDown(cmbVENDOR_ID);
				
				Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgersIndropDown(cmbFISCAL_ID);
				cmbFISCAL_ID.Items.Insert(0,new ListItem("",""));

				cmbVENDOR_ID.DataSource =  Cms.BusinessLayer.BlCommon.clsVendor.GetVendorNames();
				cmbVENDOR_ID.DataTextField = "COMPANY_NAME";
				cmbVENDOR_ID.DataValueField = "COMPANY_NAME_DATA";
				cmbVENDOR_ID.DataBind();
				//
				cmbVENDOR_ID.Items.Insert(0,"");
				SetCaptions();		
				SetFiscalYear();
				if(Request.QueryString["INVOICE_ID"]!=null && Request.QueryString["INVOICE_ID"].ToString().Length>0)
					hidOldData.Value = ClsVendorInvoices.GetXmlForPageControls(Request.QueryString["INVOICE_ID"]);
			}
		}//end pageload
		#endregion
		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsVendorInvoicesInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsVendorInvoicesInfo objVendorInvoicesInfo;
			objVendorInvoicesInfo = new ClsVendorInvoicesInfo();
			string[] Vendor_ID= cmbVENDOR_ID.SelectedValue.Split('~'); 
			//objVendorInvoicesInfo.VENDOR_ID			=	int.Parse(cmbVENDOR_ID.SelectedValue);
			objVendorInvoicesInfo.VENDOR_ID			=	int.Parse(Vendor_ID[0].ToString());
			
			//Added For Itrack Issue # 6677.
			hidVENDOR_ID.Value = objVendorInvoicesInfo.VENDOR_ID.ToString();
			
			objVendorInvoicesInfo.INVOICE_NUM		=	txtINVOICE_NUM.Text;
			objVendorInvoicesInfo.REF_PO_NUM		=	txtREF_PO_NUM.Text;

			objVendorInvoicesInfo.TRANSACTION_DATE	=	DateTime.Parse(txtTRANSACTION_DATE.Text);
			objVendorInvoicesInfo.DUE_DATE			=	DateTime.Parse(txtDUE_DATE.Text);

			
		    objVendorInvoicesInfo.INVOICE_AMOUNT	=	double.Parse(txtINVOICE_AMOUNT.Text);
		
			objVendorInvoicesInfo.NOTE				=	txtNOTE.Text;

			if(cmbFISCAL_ID.SelectedValue != null && cmbFISCAL_ID.SelectedValue != "")
				objVendorInvoicesInfo.FISCAL_ID = int.Parse(cmbFISCAL_ID.SelectedValue);

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidINVOICE_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objVendorInvoicesInfo;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
			this.btnCommitPost.Click += new System.EventHandler(this.btnCommitPost_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objVendorInvoices = new  ClsVendorInvoices(true);				

				//Retreiving the form values into model class object
				ClsVendorInvoicesInfo objVendorInvoicesInfo = GetFormValue();
				string strVendorInvoiceDetails = "Vendor Name: " + cmbVENDOR_ID.SelectedItem.Text + ";Invoice #:" + txtINVOICE_NUM.Text.Trim();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objVendorInvoicesInfo.CREATED_BY = int.Parse(GetUserId());
					objVendorInvoicesInfo.CREATED_DATETIME = DateTime.Now;
					objVendorInvoicesInfo.MODIFIED_BY = int.Parse(GetUserId());
					objVendorInvoicesInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objVendorInvoices.Add(objVendorInvoicesInfo,strVendorInvoiceDetails);

					if(intRetVal>0)
					{
						hidINVOICE_ID.Value = objVendorInvoicesInfo.INVOICE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value = ClsVendorInvoices.GetXmlForPageControls(objVendorInvoicesInfo.INVOICE_ID.ToString());
						CheckInvoiceStatus(hidINVOICE_ID.Value);
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsVendorInvoicesInfo objOldVendorInvoicesInfo;
					objOldVendorInvoicesInfo = new ClsVendorInvoicesInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldVendorInvoicesInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objVendorInvoicesInfo.INVOICE_ID = int.Parse(strRowId);
					objVendorInvoicesInfo.MODIFIED_BY = int.Parse(GetUserId());
					objVendorInvoicesInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objVendorInvoices.Update(objOldVendorInvoicesInfo,objVendorInvoicesInfo,strVendorInvoiceDetails);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value = ClsVendorInvoices.GetXmlForPageControls(objVendorInvoicesInfo.INVOICE_ID.ToString());
						//hidOldData.Value = ClsVendorInvoices.GetXmlForPageControls(Request.QueryString["INVOICE_ID"]);
						CheckInvoiceStatus(hidINVOICE_ID.Value);
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVendorInvoices!= null)
					objVendorInvoices.Dispose();
			}
		}

		
		#endregion

		#region Delete Event Handler
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objVendorInvoices = new  ClsVendorInvoices(true);

				//Retreiving the form values into model class object
				ClsVendorInvoicesInfo objVendorInvoicesInfo = new ClsVendorInvoicesInfo();
				
				base.PopulateModelObject(objVendorInvoicesInfo,hidOldData.Value);
				string strINVOICE_NUM = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("INVOICE_NUM",hidOldData.Value);
				string strVendorInvoiceDetails = "Vendor Name: " + hidVENDOR_DETAILS.Value + ";Invoice #:" + txtINVOICE_NUM.Text.Trim();
				objVendorInvoicesInfo.MODIFIED_BY = int.Parse(GetUserId());
				objVendorInvoicesInfo.LAST_UPDATED_DATETIME = DateTime.Now;

				if(objVendorInvoicesInfo.INVOICE_ID>0)
				{
					intRetVal = objVendorInvoices.Delete(objVendorInvoicesInfo,strVendorInvoiceDetails);
					if(intRetVal>0)
					{
						tblBody.Attributes.Add("style","display:none");
						hidFormSaved.Value = "5";
						hidOldData.Value = "";
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"127");
					}
					else
					{
						hidFormSaved.Value = "2";
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
					}
					lblMessage.Visible	=	true;
				}				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVendorInvoices!= null)
					objVendorInvoices.Dispose();
			}
		}
		#endregion
		private void SetCaptions()
		{
			capVENDOR_ID.Text						=		objResourceMgr.GetString("cmbVENDOR_ID");
			capINVOICE_NUM.Text						=		objResourceMgr.GetString("txtINVOICE_NUM");
			capREF_PO_NUM.Text						=		objResourceMgr.GetString("txtREF_PO_NUM");
			capTRANSACTION_DATE.Text				=		objResourceMgr.GetString("txtTRANSACTION_DATE");
			capDUE_DATE.Text						=		objResourceMgr.GetString("txtDUE_DATE");
			capINVOICE_AMOUNT.Text					=		objResourceMgr.GetString("txtINVOICE_AMOUNT");
			capNOTE.Text							=		objResourceMgr.GetString("txtNOTE");
		}

		/*private void btnApprove_Click(object sender, System.EventArgs e)
		{
			try
			{
				objVendorInvoices = new  ClsVendorInvoices(true);

				//Creating the Model object for holding the Old data
				//ClsVendorInvoicesInfo objOldVendorInvoicesInfo;
				//objOldVendorInvoicesInfo = new ClsVendorInvoicesInfo();

				//Setting  the Old Page details(XML File containing old details) into the Model Object
				//base.PopulateModelObject(objOldVendorInvoicesInfo,hidOldData.Value);
				
				ClsVendorInvoicesInfo objVendorInvoicesInfo;
				objVendorInvoicesInfo = new ClsVendorInvoicesInfo();
				base.PopulateModelObject(objVendorInvoicesInfo,hidOldData.Value);

				objVendorInvoicesInfo.INVOICE_ID = int.Parse(hidINVOICE_ID.Value);
				objVendorInvoicesInfo.MODIFIED_BY = int.Parse(GetUserId());
				objVendorInvoicesInfo.LAST_UPDATED_DATETIME = DateTime.Now;

				objVendorInvoicesInfo.IS_APPROVED		=	"Y";
				objVendorInvoicesInfo.APPROVED_BY		=	int.Parse(GetUserId());
				objVendorInvoicesInfo.APPROVED_DATE_TIME=   DateTime.Now;
				int	intRetVal	= objVendorInvoices.Approve(objVendorInvoicesInfo);
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
					hidFormSaved.Value		=	"1";
					CheckInvoiceStatus(hidINVOICE_ID.Value);
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"10");
					hidFormSaved.Value		=	"1";
				}
				lblMessage.Visible = true;
			
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVendorInvoices!= null)
					objVendorInvoices.Dispose();
			}
		}
*/
		private void btnCommitPost_Click(object sender, System.EventArgs e)
		{
			try
			{
				objVendorInvoices = new  ClsVendorInvoices(true);

				//Creating the Model object for holding the Old data
				//ClsVendorInvoicesInfo objOldVendorInvoicesInfo;
				//objOldVendorInvoicesInfo = new ClsVendorInvoicesInfo();

				//Setting  the Old Page details(XML File containing old details) into the Model Object
				//base.PopulateModelObject(objOldVendorInvoicesInfo,hidOldData.Value);
				
				ClsVendorInvoicesInfo objVendorInvoicesInfo;
				objVendorInvoicesInfo = new ClsVendorInvoicesInfo();
				base.PopulateModelObject(objVendorInvoicesInfo,hidOldData.Value);
				System.Xml.XmlDocument objXMLDoc = new System.Xml.XmlDocument();
				objXMLDoc.LoadXml(hidOldData.Value);
				double dblInvAmt = double.Parse(Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(objXMLDoc,"//INVOICE_AMOUNT"));
				int	intRetVal;
				if(dblInvAmt >= 0)
				{
					objVendorInvoicesInfo.INVOICE_ID = int.Parse(hidINVOICE_ID.Value);
					objVendorInvoicesInfo.MODIFIED_BY = int.Parse(GetUserId());
					objVendorInvoicesInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objVendorInvoicesInfo.IS_COMMITTED		=	"Y";
					objVendorInvoicesInfo.DATE_COMMITTED	=	DateTime.Now;
					objVendorInvoicesInfo.IS_APPROVED		=	"Y";
					objVendorInvoicesInfo.APPROVED_BY		=	int.Parse(GetUserId());
					objVendorInvoicesInfo.APPROVED_DATE_TIME=   DateTime.Now;
					intRetVal	= objVendorInvoices.Commit(objVendorInvoicesInfo);
				}
				else
				{
					intRetVal = -6;
				}
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"12");
					hidFormSaved.Value		=	"5";
					CheckInvoiceStatus(hidINVOICE_ID.Value);
					
					tblBody.Attributes.Add("style","display:none");


				}
				else if( intRetVal == -1 )			// Record not existed, hence exiting 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"14");
					hidFormSaved.Value		=	"1";
					CheckInvoiceStatus(hidINVOICE_ID.Value);
				}
				else if( intRetVal == -2 )			// Record already commited hence exiting
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"15");
					hidFormSaved.Value		=	"1";
					CheckInvoiceStatus(hidINVOICE_ID.Value);
				}
				else if( intRetVal == -3 )			//Invoice not distributed fully, therefore exiting
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"16");
					hidFormSaved.Value		=	"1";
					CheckInvoiceStatus(hidINVOICE_ID.Value);
				}
				else if( intRetVal == -5 )			//Transaction date is less then lock date, hence can not be commited
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"19");
					hidFormSaved.Value		=	"1";
					CheckInvoiceStatus(hidINVOICE_ID.Value);
				}
				else if( intRetVal == -6 )			//Transaction date is less then lock date, hence can not be commited
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
					hidFormSaved.Value		=	"1";
					CheckInvoiceStatus(hidINVOICE_ID.Value);
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"13");
					hidFormSaved.Value		=	"1";
				}
				lblMessage.Visible = true;
			
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVendorInvoices!= null)
					objVendorInvoices.Dispose();
			}
		}
		/// <summary>
		/// N: Not Distributed
		/// D:Distributed 
		/// A:approved it is implicit that invoice is distributed.
		/// C:committed , it is implicit that invoice is distributed and approved.
		/// </summary>
		/// <param name="INVOICE_ID"></param>
		private void CheckInvoiceStatus(string INVOICE_ID)
		{
			string status = ClsVendorInvoices.GetInvoiceStatus(INVOICE_ID);
			switch(status)
			{
				case "N":
//					btnDistribute.Enabled	=	true;
//					btnApprove.Enabled		=	false;
					btnCommitPost.Enabled	=	false;
					break;					
				case "D":
					//btnDistribute.Enabled	=	false;
//					btnApprove.Enabled		=	true;
					btnCommitPost.Enabled	=	true;
					break;
				case "A":
//					btnDistribute.Enabled	=	false;
//					btnApprove.Enabled		=	false;
					btnCommitPost.Enabled	=	true;
					break;
				case "C":
//					btnDistribute.Enabled	=	false;
//					btnApprove.Enabled		=	false;
					btnCommitPost.Enabled	=	false;
					break;
			}
			btnDistribute.Enabled	=	true;
		}

		private void btnDistribute_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
		
		}
		private void SetFiscalYear()
		{
			try
			{
				DateTime tranDate = DateTime.Now;
				string fdate;

				for(int ctr = 0; ctr < cmbFISCAL_ID.Items.Count; ctr++)
				{
					fdate = cmbFISCAL_ID.Items[ctr].Text;
				
					if (fdate.Trim() == "")
					{
						continue;
					}
			
					//Getting the financial dates, from financial year
					string d1 = fdate.Substring(fdate.IndexOf("(") + 1, 11);
					string d2 = fdate.Substring(fdate.IndexOf("-") + 1, 11);
				
					if (tranDate >= DateTime.Parse(d1) && tranDate <= DateTime.Parse(d2))		
					{
						//Transaction date is in between financial dates
						//Hence selecting this fiscal year
						cmbFISCAL_ID.SelectedIndex = ctr;
						break;
					}	
				}
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
	}
}
