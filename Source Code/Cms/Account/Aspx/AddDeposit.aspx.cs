/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	6/20/2005 2:29:04 PM
<End Date				: -	
<Description			: - Code behind file Deposit screen.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Class for showing the add deposit screen.
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
using Cms.BusinessLayer.BlCommon;
using System.IO;
using System.Xml;
using Model.Account;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AddDeposit.
	/// </summary>
	public class AddDeposit : Cms.Account.AccountBase
	{
		#region Page Variable Declarations
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capFISCAL_ID;
		protected System.Web.UI.WebControls.DropDownList cmbFISCAL_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFISCAL_ID;
		protected System.Web.UI.WebControls.Label capRECEIPT_MODE;
		protected System.Web.UI.WebControls.DropDownList cmbRECEIPT_MODE;
        protected System.Web.UI.WebControls.DropDownList cmbDEPOSIT_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECEIPT_MODE;
        protected System.Web.UI.WebControls.Label capACCOUNT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected System.Web.UI.WebControls.Label capDEPOSIT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtDEPOSIT_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPOSIT_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDEPOSIT_NUMBER;
		protected System.Web.UI.WebControls.Label capDEPOSIT_TRAN_DATE;
		protected System.Web.UI.WebControls.TextBox txtDEPOSIT_TRAN_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPOSIT_TRAN_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDEPOSIT_TRAN_DATE;
		protected System.Web.UI.WebControls.Label capCREATED_DATETIME;
		protected System.Web.UI.WebControls.Label capTOTAL_DEPOSIT_AMOUNT;
		protected System.Web.UI.WebControls.Label capDEPOSIT_NOTE;
		protected System.Web.UI.WebControls.TextBox txtDEPOSIT_NOTE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnShowExceptionItems; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTOTAL_DEPOSIT_AMOUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;	
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidfileLink;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_TRAN_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCREATED_DATETIME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidVal;
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected string strUserName ="";
		protected string strPassWd ="";
		protected string strDomain ="";
		protected string strFileName ="";
		protected string strUploadedFileName ="";
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label lblCREATED_DATETIME;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected Cms.CmsWeb.Controls.CmsButton btnNext;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCashAccountXML;
		protected System.Web.UI.WebControls.CustomValidator csvDEPOSIT_TRAN_DATE;
		 protected System.Web.UI.WebControls.HyperLink hlkDEPOSIT_TRAN_DATE;
		protected System.Web.UI.WebControls.Label lblTOTAL_DEPOSIT_AMOUNT;
		protected System.Web.UI.WebControls.Label capACCOUNT_BALANCE;
		protected System.Web.UI.WebControls.Label lblACCOUNT_BALANCE;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit;
		protected System.Web.UI.WebControls.Label capTapeTotalCust;
		protected System.Web.UI.WebControls.Label capTapeTotalAgency;
		protected System.Web.UI.WebControls.Label capTapeTotalClaim;
		protected System.Web.UI.WebControls.Label capTotalMisc;
		protected System.Web.UI.WebControls.Label lblTAPE_TOTAL_CUST;
		protected System.Web.UI.WebControls.Label lblTOTAL_AGENCY;
		protected System.Web.UI.WebControls.Label lblTAPE_TOTAL_CLM;
		protected System.Web.UI.WebControls.Label lblTAPE_TOTAL_MISC;
		protected System.Web.UI.WebControls.Label capRTL_FILE;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtRTL_FILE;
		protected System.Web.UI.WebControls.Label lblRTL_FILE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRTL_FILE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;
		protected Cms.CmsWeb.Controls.CmsButton btnImport;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAccountBalance;
		protected System.Web.UI.WebControls.Button btnImportProgress;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBalance;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_NO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_NUMBER;
        protected System.Web.UI.WebControls.Label capMandatoryNotes;
        protected System.Web.UI.WebControls.Label capDEPOSIT_TYPE;
        protected System.Web.UI.WebControls.CustomValidator csvDEPOSIT_TRAN_DATE2;
		string strACCOUNT_BALANCE = "";
		//Defining the business layer class object
		ClsDeposit  objDeposit ;

		
		
		//END:*********** Local variables *************

		#endregion

		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
        {
            Page.DataBind();
			//Ajax Function 
			Ajax.Utility.RegisterTypeForAjax(typeof(AddDeposit));
  
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			btnNext.Attributes.Add("OnClick","javascript:return OnNextClick();");
			btnDelete.Attributes.Add("OnClick","javascript:OnDeleteClick();");
			btnImport.Attributes.Add("Onclick","javascript:HideShowImportInProgress();");
			btnImport.Attributes.Add("Onclick","javascript:HideShowImportInProgress();");
			btnCommit.Attributes.Add("onClick","return confirmCommit();");
           
			SetScreenId();
			lblMessage.Visible = false;
			SetErrorMessages();
            hidVal.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1980");
            
			
			#region Button Permissions
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;

			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;

			btnCopy.CmsButtonClass		= CmsButtonType.Write;
			btnCopy.PermissionString	= gstrSecurityXML;

			btnNext.CmsButtonClass		= CmsButtonType.Read;
			btnNext.PermissionString	= gstrSecurityXML;

			btnDelete.CmsButtonClass	= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			btnCommit.CmsButtonClass	= CmsButtonType.Write;
			btnCommit.PermissionString	= gstrSecurityXML;

			btnImport.CmsButtonClass	= CmsButtonType.Execute;
			btnImport.PermissionString	= gstrSecurityXML;


            btnShowExceptionItems.CmsButtonClass = CmsButtonType.Execute;
            btnShowExceptionItems.PermissionString = gstrSecurityXML;
            
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			#endregion

			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddDeposit" ,System.Reflection.Assembly.GetExecutingAssembly());
			hidRootPath.Value  = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/DepositsRTLFile/";
            hlkDEPOSIT_TRAN_DATE.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtDEPOSIT_TRAN_DATE'), document.getElementById('txtDEPOSIT_TRAN_DATE'))");

			if(!Page.IsPostBack)
			{
				GetQueryString();
				GetOldDataXML();
                
				SetCaptions();
				FillCombos();
//				EnableDisableControls();
                SetFiscalYear();
				hidFormSaved.Value = "0";
                BindDepositType();
			}
           
			//hidRootPath.Value  = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/DepositsRTLFile/";
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsDepositInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsDepositInfo objDepositInfo;
			objDepositInfo = new ClsDepositInfo();

			if(cmbFISCAL_ID.SelectedValue != null && cmbFISCAL_ID.SelectedValue != "")
				objDepositInfo.FISCAL_ID = int.Parse(cmbFISCAL_ID.SelectedValue);

			if (cmbACCOUNT_ID.SelectedValue != null && cmbACCOUNT_ID.SelectedValue != "")
			{
				//retreiving the id of account from value of combox box
				objDepositInfo.ACCOUNT_ID = int.Parse(cmbACCOUNT_ID.SelectedValue);
			}

            if (txtDEPOSIT_NUMBER.Text != "")
            {
                //objDepositInfo.DEPOSIT_NUMBER = int.Parse(txtDEPOSIT_NUMBER.Text);
                objDepositInfo.DEPOSIT_NUMBER = int.Parse(hidDEPOSIT_NUMBER.Value);
            }

            if (txtDEPOSIT_TRAN_DATE.Text.ToString() != "")
            {
                objDepositInfo.DEPOSIT_TRAN_DATE = ConvertToDate(txtDEPOSIT_TRAN_DATE.Text);
                //objDepositInfo.DEPOSIT_TRAN_DATE = ConvertToDate( hidDEPOSIT_TRAN_DATE.Value);
            }
            //if (cmbDEPOSIT_TYPE.SelectedItem != null && cmbDEPOSIT_TYPE.SelectedValue != "")
            //    objDepositInfo.DEPOSIT_TYPE = cmbDEPOSIT_TYPE.SelectedItem.Value;

            if (hidDEPOSIT_TYPE.Value != null && hidDEPOSIT_TYPE.Value != "")
                objDepositInfo.DEPOSIT_TYPE = hidDEPOSIT_TYPE.Value;

           
			if(cmbRECEIPT_MODE.SelectedItem!=null && cmbRECEIPT_MODE.SelectedItem.Value!="")
				objDepositInfo.RECEIPT_MODE = int.Parse(cmbRECEIPT_MODE.SelectedItem.Value);
			objDepositInfo.DEPOSIT_NOTE = txtDEPOSIT_NOTE.Text;
			//objDepositInfo.DEPOSIT_TYPE = hidDEPOSIT_TYPE.Value;

//			if(hidAccountBalance.Value != "" && hidAccountBalance.Value !=null)
//			{
//				strACCOUNT_BALANCE = hidAccountBalance.Value; 
//			}
			strACCOUNT_BALANCE = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAccountBalanceByAccountID(int.Parse(cmbACCOUNT_ID.SelectedValue));
			if(strACCOUNT_BALANCE != "")
				objDepositInfo.ACCOUNT_BALANCE = double.Parse(strACCOUNT_BALANCE);

			//These  assignments are common to all pages.
			strFormSaved = hidFormSaved.Value;
			strRowId = hidDEPOSIT_ID.Value;
			oldXML = hidOldData.Value;

			//=========  File Upload ===========START
			
			strUserName  = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
            strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
            strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

			//---- Begin
			//Below code gets the File name from Label or Text i.e; 
			//in either update or saved case.
			if(oldXML != "")
			{
				System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
				xmlDoc.LoadXml(oldXML);
		
				System.Xml.XmlNode xmlNode = xmlDoc.SelectSingleNode("/NewDataSet/Table/RTL_FILE");
				if(xmlNode!=null)
					lblRTL_FILE.Text		=  xmlNode.InnerText;
			}	
			if (Request.Form["hidRTL_FILE"].ToString().Equals("Y"))				
				strFileName	=	txtRTL_FILE.PostedFile.FileName;
			else				
				strFileName	=	lblRTL_FILE.Text;				
			int intIndex		=	strFileName.LastIndexOf("\\");
			strFileName		=	strFileName.Substring(intIndex+1);	//Taking only file name not whole path
			//Case Added For Itrack Issue #6568.
            if (cmbRECEIPT_MODE.SelectedValue == "11975" || cmbRECEIPT_MODE.SelectedValue == "14919")
			{
				objDepositInfo.RTL_FILE = strFileName;	
			}
			else
			{
			  objDepositInfo.RTL_FILE = "";	
			}
			//=========  File Upload ===========END

			return objDepositInfo;
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
			this.cmbFISCAL_ID.SelectedIndexChanged += new System.EventHandler(this.cmbFISCAL_ID_SelectedIndexChanged);
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Web Event Handlers (SAVE / COPY / DELETE / COMMIT / COMBO CHANGE / IMPORT RTL FILE)
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
            {
				int intRetVal;	//For retreiving the return value of business class save function
				 objDeposit = new  ClsDeposit();
				
				//Retreiving the form values into model class object
				ClsDepositInfo objDepositInfo = GetFormValue();
			
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{

					objDepositInfo.CREATED_BY = int.Parse(GetUserId());
					objDepositInfo.CREATED_DATETIME = DateTime.Now;
					objDepositInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objDeposit.Add(objDepositInfo);

					if(intRetVal>0)
					{
						hidDEPOSIT_ID.Value = objDepositInfo.DEPOSIT_ID.ToString();
						lblMessage.Text	= ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value = "1";
						GetOldDataXML();
						UploadFile();
						if(ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value)!="" && ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value)!= null)
						{
							string filename = hidRootPath.Value + ClsCommon.FetchValueFromXML("DEPOSIT_ID",hidOldData.Value) + '_' + ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value);
							int startOfFile = filename.IndexOf("Upload");
							string filePath = filename.Substring(startOfFile + 6);
							string []fileURL = filePath.Split('.'); 
							string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
							hidfileLink.Value = EncryptedPath;
						}
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"6");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsDepositInfo objOldDepositInfo;
					objOldDepositInfo = new ClsDepositInfo();
				
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDepositInfo, hidOldData.Value);
					
					//Setting those values into the Model object which are not in the page
					objDepositInfo.DEPOSIT_ID = int.Parse(strRowId);
                    
					objDepositInfo.MODIFIED_BY = int.Parse(GetUserId());
					objDepositInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objDepositInfo.CREATED_DATETIME = objOldDepositInfo.CREATED_DATETIME;

					//Updating the record using business layer class object
					intRetVal	= objDeposit.Update(objOldDepositInfo,objDepositInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
						UploadFile();
						if(ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value)!="" && ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value)!= null)
						{
							string filename = hidRootPath.Value + ClsCommon.FetchValueFromXML("DEPOSIT_ID",hidOldData.Value) + '_' + ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value);
							int startOfFile = filename.IndexOf("Upload");
							string filePath = filename.Substring(startOfFile + 6);
							string []fileURL = filePath.Split('.'); 
							string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
							hidfileLink.Value = EncryptedPath;
						}
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"5");
						hidFormSaved.Value		=	"2";
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
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
				if(objDeposit!= null)
					objDeposit.Dispose();
			}
		}

		//Added by Raghav Itrack Issue #5013 //Calling Ajax Function.
		//TO MOVE TO LOCAL VSS
		#region AJAX CALL TO FILL DRIVER DROPDOWNS
		[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
		public string AjaxFunction (int AccountID,int FiscalID) //Call on Combo Change Ajax Call
		{
           
			string strResponse = "";				
			try
			{				
				string DepositNumber = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(FiscalID,AccountID);
                string Balance = String.Empty;
				//string Balance=	Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAccountBalanceByAccountID(AccountID);
				strResponse = DepositNumber + ";" +  Balance;
				
			}
			catch(Exception objEx)
			{
				lblMessage.Text = objEx.Message;

			} 
		     	
		    return strResponse;
		}	   
		//End here
		#endregion
		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			try
			{
				
				//Code to Test EFT 
				/*ClsEFT objEFT = new ClsEFT(3);
				objEFT.Start();*/
				
				/*Copying the whole record*/
				objDeposit = new ClsDeposit();
				int intRetVal = objDeposit.Copy(int.Parse(hidDEPOSIT_ID.Value));

				if (intRetVal>0)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("824");
					lblMessage.Visible = true;
					hidDEPOSIT_ID.Value = intRetVal.ToString();
					hidFormSaved.Value = "5";
					GetOldDataXML();
				}	
			}
			catch (Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if(hidDelete.Value == "Yes")
			{
				//Retreiving the form values into model class object
				ClsDepositInfo objDepositInfo = GetFormValue();

				try
				{
					/*Deleting the whole record*/
					objDeposit = new ClsDeposit();
					int CREATED_BY = int.Parse(GetUserId());
					int intRetVal = objDeposit.Delete(int.Parse(hidDEPOSIT_ID.Value),objDepositInfo,CREATED_BY);

					if (intRetVal > 0)
					{
						lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("127");
						hidDEPOSIT_ID.Value = "";
						hidFormSaved.Value = "5";
						hidOldData.Value = "";
						tblBody.Attributes.Add("style", "display:none");
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"6");
						hidFormSaved.Value		=	"2";
					}
					else
					{
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "128");
						hidFormSaved.Value = "2";
					}
					lblMessage.Visible = true;
				}
				catch (Exception objEx)
				{
					lblMessage.Text = objEx.Message.ToString();
					lblMessage.Visible = true;
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
				}
			}
		}

		private void cmbFISCAL_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmbFISCAL_ID.SelectedValue != null && cmbFISCAL_ID.SelectedValue.ToString()!="")
			{
				FillAccountDetails(int.Parse(cmbFISCAL_ID.SelectedValue));
			}
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strDepositId;
				strDepositId = hidDEPOSIT_ID.Value;

				Cms.BusinessLayer.BlAccount.ClsDeposit 
					objDeposit = new Cms.BusinessLayer.BlAccount.ClsDeposit();

                /* Commit will be executed from EOD process.
                 * Commenting Commit Logic. and Executing Sent To Spool Process.
                 * Modified on 21 March 2008 : Praveen
                 * Itrack #3907
                 * */
                #region Comment For on (03-12-2010) By Anurag
                //int RetVal = objDeposit.CommitToSpool(int.Parse(strDepositId), int.Parse(GetUserId()));
                //switch(RetVal)
                //{
                //    case 1:
                //        //Deposit Spooled sucessfully
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1021");
                //        break;
                //    case -1:
                //        //Deposit not exists
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "715");
                //        break;
                //    case -2:
                //        //Deposit already commited
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "716");
                //        break;
                //    case -4:
                //        //Line items not exists
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "718");
                //        break;
                //    case -5:
                //        //Day is less then lock date
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "719");
                //        break;
                //    case -6:
                //        //Deposit is not confirmed
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "720");
                //        break;
                //    case -7:
                //        //Deposit is not confirmed AGNC
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "738");
                //        break;
                //    case -8:
                //        //Deposit is not confirmed MISC
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "886");
                //        break;
                //    case -9:
                //        //Payment Mode is EFT and deposit contains records other than Agency
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
                //        break;
                //    case -10:
                //        //Deposits with 0 Balance cannot be commited : MISC
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "960");
                //        break;
                //    case -11:
                //        //Deposits with 0 Balance cannot be commited : CUST
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "961");
                //        break;
                //    case -12:
                //        //Deposits with 0 Balance cannot be commited : AGN
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "962");
                //        break;
                //    case -13:
                //        //Deposits with 0 Balance cannot be commited : CLAM
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "963");
                //        break;
                //    case -14:
                //        //Restrict user to commit deposit with –ve amount
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1015");
                //        break;
                //    default:
                //        //Some other error occured
                //        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "717");
                //        break;

                //}
                #endregion
                 int RetVal = objDeposit.Commit(int.Parse(strDepositId), int.Parse(GetUserId()));

				switch(RetVal)
				{
					case 1:
						//Deposit Commited sucessfully
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "714");
						break;
					case -1:
						//Deposit not exists
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "715");
						break;
					case -2:
						//Deposit already commited
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "716");
						break;
					case -4:
						//Line items not exists
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "718");
						break;
					case -5:
						//Day is less then lock date
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "719");
						break;
					case -6:
						//Deposit is not confirmed
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "720");
						break;
					case -7:
						//Deposit is not confirmed AGNC
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "738");
						break;
					case -8:
						//Deposit is not confirmed MISC
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "886");
						break;
					case -9:
						//Payment Mode is EFT and deposit contains records other than Agency
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
						break;
					case -10:
						//Deposits with 0 Balance cannot be commited : MISC
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "960");
						break;
					case -11:
						//Deposits with 0 Balance cannot be commited : CUST
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "961");
						break;
					case -12:
						//Deposits with 0 Balance cannot be commited : AGN
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "962");
						break;
					case -13:
						//Deposits with 0 Balance cannot be commited : CLAM
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "963");
						break;
					case -14:
						//Restrict user to commit deposit with –ve amount
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1015");
						break;
                    case -15:
                        //Deposit cannot be committed as there are one or more receipts with unapproved exception.
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
                        break;
                    case -16:
                        //Already paid boleto is again recorded and approved.
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
                        break;
                    case -17:
                        //Same boleto is recorded twice in the deposit.
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
                        break;
                    case -18:
                        //One or more receipts have already been added in another deposit. Please verify.
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
                        break;
                    case -19:
                        //Same boleto recorded multiple times in the deposit
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");
                        break;
					default:
						//Some other error occured
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "717");
						break;

				} 
				

				if (RetVal == 1)
				{
					hidOldData.Value = "";
					//Refreshing the grid, and removing the deposit details tab
					string JavascriptText = "<script>RefreshWebGrid('1','" + hidDEPOSIT_ID.Value + "',false);RemoveTab(2,parent.parent);</script>";
					ClientScript.RegisterStartupScript(this.GetType(),"RefreshGrid",JavascriptText);
					tblBody.Attributes.Add("style", "display:none");
					hidFormSaved.Value = "5";
				}
				lblMessage.Visible = true;

			}
			catch(Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}
        private void ImportFileDataForOurNumber()
        {
            ArrayList objArrLst = new ArrayList();
            try
            {
                // Get File Path /  File Name / File Length
                string strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
                string strDirName = Server.MapPath(strRoot);
                strDirName = strDirName + "\\DepositsRTLFile";

                XmlDocument objXMLDoc = new XmlDocument();
                objXMLDoc.LoadXml(hidOldData.Value);
                strUploadedFileName = ClsCommon.GetNodeValue(objXMLDoc, "//RTL_FILE");

                //=========  File Upload ===========START

                strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

                Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
                long FileLen = 0;
                StreamReader objSR = null;

                if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                {
                    FileInfo objFileInfo = new FileInfo(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);
                    FileLen = objFileInfo.Length;
                    // File StreamReader
                    objSR = new StreamReader(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);
                    //ending the impersonation 
                    objAttachment.endImpersonation();
                }
                else
                {
                    //Impersation failed
                    lblMessage.Text += "\n " + ClsMessages.FetchGeneralMessage("1420"); //Unable to Read the file. User impersonation failed.";
                }


                //Obj 
                ClsDepositDetails objDeposit = new ClsDepositDetails();

                string strFile;
                int i = 1;
                string[] arrFile = new string[FileLen];
                while ((strFile = objSR.ReadLine()) != null && i <= FileLen)
                {
                    ClsAddDepositDetailsinfo objAddDepositInfo = new ClsAddDepositDetailsinfo();
                    arrFile[i] = strFile;
                    try
                    {
                        if (strFile.Trim() != "")
                        {
                            objAddDepositInfo.DEPOSIT_ID.CurrentValue = int.Parse(hidDEPOSIT_ID.Value);
                           
                            //VALIDATE RTL OUR NUMBER
                            if (objDeposit.IsRTLOurNumberValid(arrFile[i].ToString().Trim()))
                            {
                                objAddDepositInfo.OUR_NUMBER.CurrentValue = arrFile[i].Substring(18, 12).ToString().Trim();
                                objAddDepositInfo.INCORRECT_OUR_NUMBER.CurrentValue = arrFile[i].Substring(18, 12).ToString().Trim();
                            }
                            else
                            {
                                objAddDepositInfo.OUR_NUMBER.CurrentValue = String.Empty; 
                                objAddDepositInfo.INCORRECT_OUR_NUMBER.CurrentValue = String.Empty;
                            }
                            
                            objAddDepositInfo.POLICY_NUMBER.CurrentValue = String.Empty;
                            //VALIDATE RTL AMOUNT
                            string amount = "";
                            if (objDeposit.IsRTLPaidPremiumnValid(arrFile[i].ToString().Trim()))
                            {
                                amount = arrFile[i].Substring(39, 13).ToString().Trim();
                                if(ClsCommon.BL_LANG_ID==2)
                                    amount = amount.Insert(amount.Length - 2, ",");
                                else
                                    amount = amount.Insert(amount.Length - 2, ".");

                                objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = Double.Parse(amount);
                                objAddDepositInfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Double.Parse(amount);
                            }
                            else
                            {
                                objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue(); ;
                                objAddDepositInfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Double.Parse(amount);
                            }


                            //VALIDATE LATE FEE
                            String latefee = String.Empty;
                            if (objDeposit.IsRTLFinancial_PenaltyValid(arrFile[i].ToString().Trim()))
                            {
                                latefee = arrFile[i].Substring(57, 13).ToString().Trim();
                                if (ClsCommon.BL_LANG_ID == 2)
                                    latefee = latefee.Insert(latefee.Length - 2, ",");
                                else
                                    latefee = latefee.Insert(latefee.Length - 2, ".");
                                objAddDepositInfo.LATE_FEE.CurrentValue = Convert.ToDouble(latefee);
                            }
                            else
                                objAddDepositInfo.LATE_FEE.CurrentValue = base.GetEbixDoubleDefaultValue();

                            //VALIDATE LATE FEE
                            DateTime validDate=new DateTime();
                            if (objDeposit.IsRTLValidDate(arrFile[i].ToString().Trim(),ref validDate))
                                objAddDepositInfo.PAYMENT_DATE.CurrentValue = Convert.ToDateTime(validDate.ToShortDateString());
                            //else
                            //    objAddDepositInfo.PAYMENT_DATE.CurrentValue = ConvertToDate(null);

                            objAddDepositInfo.PAY_MODE.CurrentValue = 14692;// for boleto payment
                            objAddDepositInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                            objAddDepositInfo.CREATED_FROM.CurrentValue = "RTL";
                            objAddDepositInfo.CREATED_DATETIME.CurrentValue =Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            objAddDepositInfo.DEPOSIT_TYPE.CurrentValue = cmbDEPOSIT_TYPE.SelectedItem.Value.ToString();
                            objAddDepositInfo.COMMISSION_AMOUNT.CurrentValue = 0.0;
                            objArrLst.Add(objAddDepositInfo);
                        }
                    }
                    catch (Exception objEx)
                    {
                        if (objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue == double.MinValue)
                            objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(null);
                        if (objAddDepositInfo.LATE_FEE.CurrentValue == double.MinValue)
                            objAddDepositInfo.LATE_FEE.CurrentValue = Convert.ToDouble(null);

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1418");  //"RTL file is not in Correct format. Please review the RTL file.";
                        objDeposit.LogRTLProcessForOurNumber(objAddDepositInfo.DEPOSIT_ID.CurrentValue,
                            objAddDepositInfo.OUR_NUMBER.CurrentValue.ToString(), ClsMessages.FetchGeneralMessage("1419") + objEx.Message.ToString(),
                            ClsMessages.FetchGeneralMessage("1878"), "", ClsMessages.FetchGeneralMessage("1418"),
                            objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue, objAddDepositInfo.LATE_FEE.CurrentValue,""
                            , 396, 401, 402, objAddDepositInfo);//Modified for itrack 1506 on 10-Aug-2011
                        lblMessage.Visible = true;
                        throw (objEx);
                    }
                }
                int RetVal;
                if (objArrLst != null && objArrLst.Count > 0)
                {
                    ClsDepositDetails objBl = new ClsDepositDetails();
                    RetVal = objBl.ImportRTLFileForOurNumber(objArrLst,int.Parse(GetUserId()));
                    if (RetVal > 0)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1417");
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        //If RTL import Successful then Display the Commit option
                        btnCommit.Visible = true;
                    }
                    else if (RetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId ,"10");// "Not all Our Number imported from RTL file. Please review customer deposit section.";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        //If RTL import Successful then Display the Commit option
                        btnCommit.Visible = true;
                    }
                    else if (RetVal == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "11"); //"No records imported please review RTL file";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                    }
                    else
                    {
                        btnImport.Enabled = true;
                    }

                }
                //Added For Itrack Issue #6330.
               // GetOldDataXML();
            }
            catch (Exception objEx)
            {

                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "12");// "File could not be read as the RTL file is not in correct format. Please review the RTL file.";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
                lblMessage.Visible = true;
            }
        }
        //Added by Pradeep -on 08-Dec-2011 iTrack#1722/TFS#1890-- New Bank receiving layout implementation of RTL file provided by Brazil Team
        /// <summary>
        /// this function is use to read RTL file provided by brazil bank 
        /// </summary>
        private void ReceiveRTLBankFileData()
        {
            ArrayList objArrLst = new ArrayList();
            try
            {
                // Get File Path /  File Name / File Length
                string strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
                string strDirName = Server.MapPath(strRoot);
                strDirName = strDirName + "\\DepositsRTLFile";

                XmlDocument objXMLDoc = new XmlDocument();
                objXMLDoc.LoadXml(hidOldData.Value);
                strUploadedFileName = ClsCommon.GetNodeValue(objXMLDoc, "//RTL_FILE");

                //=========  File Upload ===========START

                strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

                Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
                long FileLen = 0;
                StreamReader objSR = null;
                StreamReader objStreamReader = null;
                var lineCount = 0;
                if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                {
                    FileInfo objFileInfo = new FileInfo(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);
                    FileLen = objFileInfo.Length;
                    // File StreamReader
                    objSR = new StreamReader(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);
                    objStreamReader = new StreamReader(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);
                    lineCount = File.ReadAllLines(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName).Length;
                    //ending the impersonation 
                    objAttachment.endImpersonation();
                }
                else
                {
                    lblMessage.Text += "\n " + ClsMessages.FetchGeneralMessage("1420"); //Unable to Read the file. User impersonation failed.";
                    return;
                }
                
                ClsDepositDetails objDeposit = new ClsDepositDetails();

                #region Number of records match at the end of the line.
                string strFile;
                int _Count = 1;
                string[] arrFileDetails = new string[FileLen];
                while ((strFile = objSR.ReadLine()) != null && _Count <= FileLen)
                {
                    arrFileDetails[_Count] = strFile;
                    //Read File footer
                    if (objSR.Peek() == -1)//Read End of the line 
                    {
                        int TotalLineCount = 0;
                        if (int.TryParse(arrFileDetails[_Count].Substring(396, 4).ToString().Trim(), out TotalLineCount))
                        {
                            if (TotalLineCount != lineCount)
                            {
                                //"Number of records dos not match."
                                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "24");//Number of records dos not match.
                                lblMessage.Visible = true;
                                btnImport.Enabled = false;
                                return;
                            }//if (TotalLineCount != lineCount)
                        }//if (int.TryParse(arrFile[line].Substring(396, 400).ToString().Trim(), out TotalLineCount))
                    }// if (objSR.Peek() == -1)
                }
                
                #endregion

                strFile=string.Empty;
                string[] arrFile = new string[FileLen];
                int CountRecord = 1;
                int retrunVal = 0;
                while ((strFile = objStreamReader.ReadLine()) != null && _Count <= FileLen)
                {
                    ClsAddDepositDetailsinfo objAddDepositInfo = new ClsAddDepositDetailsinfo();
                    
                    arrFile[_Count] = strFile;
                    try
                    {
                        if (strFile.Trim() != "" && strFile.Length == 400)
                        {
                            // Read file header
                            if (CountRecord == 1)//read only line one 
                            {
                                if (arrFile[_Count].Substring(76, 18).ToString().Trim() != "001BANCO DO BRASIL")
                                {
                                    //Invalid file. Check position 77 on the header
                                    retrunVal = -10;
                                }//if (arrFile[_Count].Substring(76, 18).ToString().Trim() != "001BANCO DO BRASIL")
                                else if (arrFile[_Count].Substring(40, 4).ToString().Trim() != "5285")
                                { 
                                    //Invalid file. Check position 41 on the header.
                                    retrunVal = -11;
                                }//else if (arrFile[_Count].Substring(40, 4).ToString().Trim() != "5285")
                                else if (arrFile[_Count].Substring(26, 4).ToString().Trim() != "3429")
                                {
                                    //Invalid file. Check position 27 on the header.
                                    retrunVal = -12;
                                }//else if (arrFile[_Count].Substring(26, 4).ToString().Trim() != "3429")
                                else if (arrFile[_Count].Substring(31, 8).ToString().Trim() != "00150079")
                                {
                                    //Invalid file. Check position 32 on the header.
                                    retrunVal = -13;
                                }//else if (arrFile[_Count].Substring(31, 8).ToString().Trim() != "00150079")
                                
                            }//if (CountRecord == 1)

                            if (retrunVal < 0)
                                break;

                            //Read file details
                            if (CountRecord != 1 && objStreamReader.Peek() != -1 && retrunVal == 0)
                            {
                                objAddDepositInfo.DEPOSIT_ID.CurrentValue = int.Parse(hidDEPOSIT_ID.Value);

                                int _record_id = 0;
                                int.TryParse(arrFile[_Count].Substring(0, 1).ToString().Trim(), out  _record_id);

                                objAddDepositInfo.RECORD_ID.CurrentValue = _record_id;
                                //VALIDATE RTL OUR NUMBER
                                if (objDeposit.ValidOurNunber(arrFile[_Count].ToString().Trim()))
                                {
                                    objAddDepositInfo.OUR_NUMBER.CurrentValue = arrFile[_Count].Substring(62, 12).ToString().Trim();
                                    objAddDepositInfo.INCORRECT_OUR_NUMBER.CurrentValue = arrFile[_Count].Substring(62, 12).ToString().Trim();
                                }
                                else
                                {
                                    objAddDepositInfo.OUR_NUMBER.CurrentValue = String.Empty;
                                    objAddDepositInfo.INCORRECT_OUR_NUMBER.CurrentValue = String.Empty;
                                }//if (objDeposit.ValidOurNunber(arrFile[_Count].ToString().Trim()))

                                objAddDepositInfo.POLICY_NUMBER.CurrentValue = String.Empty;
                                
                                DateTime validDate = new DateTime();
                                if (objDeposit.IsRTLValidDate(arrFile[_Count].ToString().Trim(), ref validDate))
                                    objAddDepositInfo.PAYMENT_DATE.CurrentValue = Convert.ToDateTime(validDate.ToShortDateString());


                                //VALIDATE RTL AMOUNT
                                string amount = "";
                                if (objDeposit.IsRTLPaidPremiumnValid(arrFile[_Count].ToString().Trim()))
                                {
                                    amount = arrFile[_Count].Substring(152, 13).ToString().Trim();
                                    if (ClsCommon.BL_LANG_ID == 2)
                                        amount = amount.Insert(amount.Length - 2, ",");
                                    else
                                        amount = amount.Insert(amount.Length - 2, ".");

                                    objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = Double.Parse(amount);
                                    objAddDepositInfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Double.Parse(amount);
                                }//if (objDeposit.IsRTLPaidPremiumnValid(arrFile[_Count].ToString().Trim()))
                                else
                                {
                                    objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue(); ;
                                    objAddDepositInfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Double.Parse(amount);
                                }

                                 
                                //VALIDATE ANC CALCULATE LATE FEE
                                String latefee = String.Empty;
                                String Otherlatefee = String.Empty;
                                String Deductiblelatefee = String.Empty;
                                if (objDeposit.IsRTLFinancial_PenaltyValid(arrFile[_Count].ToString().Trim()))
                                {
                                    latefee = arrFile[_Count].Substring(266, 13).ToString().Trim();

                                    if (objDeposit.IsRTLLateFeeValid(arrFile[_Count].ToString().Trim()))
                                        Otherlatefee = arrFile[_Count].Substring(279, 13).ToString().Trim();

                                    if (objDeposit.IsRTLDeductiblelatefeeValid(arrFile[_Count].ToString().Trim()))
                                        Deductiblelatefee = arrFile[_Count].Substring(292, 13).ToString().Trim();

                                    if (ClsCommon.BL_LANG_ID == 2)
                                    {
                                        latefee = latefee.Insert(latefee.Length - 2, ",");
                                        Otherlatefee = Otherlatefee.Insert(Otherlatefee.Length - 2, ",");
                                        Deductiblelatefee = Deductiblelatefee.Insert(Deductiblelatefee.Length - 2, ",");
                                    }//if (ClsCommon.BL_LANG_ID == 2)
                                    else
                                    {
                                        latefee = latefee.Insert(latefee.Length - 2, ".");
                                        Otherlatefee = Otherlatefee.Insert(Otherlatefee.Length - 2, ".");
                                        Deductiblelatefee = Deductiblelatefee.Insert(Deductiblelatefee.Length - 2, ".");
                                    }
                                    //We must calculate: LATE FEES = (10)MORA MULTA + (11)OUTORS RECEBIMENTOS - (12)ABATIMENTO NÏ APROVEITADO SACADO     
                                    Double _latefee = Convert.ToDouble(latefee) + Convert.ToDouble(Otherlatefee) - Convert.ToDouble(Deductiblelatefee);

                                    objAddDepositInfo.LATE_FEE.CurrentValue = _latefee;
                                }//if (objDeposit.IsRTLFinancial_PenaltyValid(arrFile[_Count].ToString().Trim()))
                                else
                                    objAddDepositInfo.LATE_FEE.CurrentValue = base.GetEbixDoubleDefaultValue();
                                
                                objAddDepositInfo.PAY_MODE.CurrentValue = 14692;// for boleto payment
                                objAddDepositInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                                objAddDepositInfo.CREATED_FROM.CurrentValue = "RTL";
                                objAddDepositInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                objAddDepositInfo.DEPOSIT_TYPE.CurrentValue = cmbDEPOSIT_TYPE.SelectedItem.Value.ToString();
                                objAddDepositInfo.COMMISSION_AMOUNT.CurrentValue = 0.0;
                                objArrLst.Add(objAddDepositInfo);


                            }// if (CountRecord != 1 && objStreamReader.Peek() != -1 && retrunVal == 0)
                          
                            
                        }//if (strFile.Trim() != "" && strFile.Length==400)
                        else
                        {
                            retrunVal = -15;//Invalid Layout, only file with 400 positions will be accepted.
                            break;

                        }
                    }
                    catch (Exception objEx)
                    {
                        if (objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue == double.MinValue)
                            objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(null);
                        if (objAddDepositInfo.LATE_FEE.CurrentValue == double.MinValue)
                            objAddDepositInfo.LATE_FEE.CurrentValue = Convert.ToDouble(null);

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1418");  //"RTL file is not in Correct format. Please review the RTL file.";
                        objDeposit.LogRTLProcessForOurNumber(objAddDepositInfo.DEPOSIT_ID.CurrentValue,
                            objAddDepositInfo.OUR_NUMBER.CurrentValue.ToString(), ClsMessages.FetchGeneralMessage("1419") + objEx.Message.ToString(),
                            ClsMessages.FetchGeneralMessage("1878"), "", ClsMessages.FetchGeneralMessage("1418"),
                            objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue, objAddDepositInfo.LATE_FEE.CurrentValue, ""
                            , 396, 401, 402, objAddDepositInfo);//Modified for itrack 1506 on 10-Aug-2011
                        lblMessage.Visible = true;
                        throw (objEx);
                    }
                    CountRecord++;
                }//while ((strFile = objStreamReader.ReadLine()) != null && _Count <= FileLen)
                
                //Handle exception on header or footer details of RTL file 
                if (retrunVal < 0)
                {
                    if (retrunVal == -10)
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "20");//Invalid file. Check position 77 on the header
                    else if (retrunVal == -11)
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "21");//Invalid file. Check position 41 on the header.
                    else if (retrunVal == -12)
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "22");//Invalid file. Check position 27 on the header.
                    else if (retrunVal == -13)
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "23");//Invalid file. Check position 32 on the header.
                    else if (retrunVal == -14)
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "24");//Number of records dos not match.
                    else if (retrunVal == -15)
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "25");//Invalid Layout, only file with 400 positions will be accepted.
                     
                    lblMessage.Visible = true;
                    btnImport.Enabled = false;
                    return;
                }//if (retrunVal < 0)

                int RetVal;
                if (retrunVal==0 && objArrLst != null && objArrLst.Count > 0)
                {
                    ClsDepositDetails objBl = new ClsDepositDetails();
                    RetVal = objBl.ImportRTLFileForOurNumber(objArrLst, int.Parse(GetUserId()));
                    if (RetVal > 0)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1417");
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        btnCommit.Visible = true;//If RTL import Successful then Display the Commit option
                    }
                    else if (RetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "10");// "Not all Our Number imported from RTL file. Please review customer deposit section.";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        btnCommit.Visible = true;//If RTL import Successful then Display the Commit option
                    }
                    else if (RetVal == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "11"); //"No records imported please review RTL file";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                    }
                    else
                    {
                        btnImport.Enabled = true;
                    }

                }
               
            }
            catch (Exception objEx)
            {

                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "12");// "File could not be read as the RTL file is not in correct format. Please review the RTL file.";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
                lblMessage.Visible = true;
            }
        }
        private void ImportFileDataForPolicyNumber()
        {
            ArrayList objArrLst = new ArrayList();
            try
            {
                // Get File Path /  File Name / File Length
                string strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
                string strDirName = Server.MapPath(strRoot);
                strDirName = strDirName + "\\DepositsRTLFile";

                XmlDocument objXMLDoc = new XmlDocument();
                objXMLDoc.LoadXml(hidOldData.Value);
                strUploadedFileName = ClsCommon.GetNodeValue(objXMLDoc, "//RTL_FILE");

                //=========  File Upload ===========START

                strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

                Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
                long FileLen = 0;
                StreamReader objSR = null;

                if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                {
                    FileInfo objFileInfo = new FileInfo(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);
                    FileLen = objFileInfo.Length;
                    // File StreamReader
                    objSR = new StreamReader(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);

                    //ending the impersonation 
                    objAttachment.endImpersonation();
                }
                else
                {
                    //Impersation failed
                    lblMessage.Text += "\n Unable to Read the file. User impersonation failed.";
                }


                //Obj 
                ClsDepositDetails objDeposit = new ClsDepositDetails();

                string strFile;
                int i = 1;
                string[] arrFile = new string[FileLen];
                while ((strFile = objSR.ReadLine()) != null && i <= FileLen)
                {
                    ClsDepositDetailsInfo objInfo = new ClsDepositDetailsInfo();
                    arrFile[i] = strFile;
                    try
                    {
                        if (strFile.Trim() != "")
                        {
                            objInfo.DEPOSIT_ID = int.Parse(hidDEPOSIT_ID.Value);
                            objInfo.DEPOSIT_TYPE = DepositType.DEPOSIT_CUSTOMER;

                            //VALIDATE RTL POLICY NUMBER
                            if (objDeposit.IsRTLPolicyValid(arrFile[i].ToString().Trim()))
                                objInfo.POLICY_NO = arrFile[i].Substring(0, 8).ToString();
                            else
                                objInfo.POLICY_NO = arrFile[i].ToString();

                            //VALIDATE RTL AMOUNT
                            string amount = "";
                            if (objDeposit.IsRTLAmountValid(arrFile[i].ToString().Trim()))
                            {
                                amount = arrFile[i].Substring(8, 7).ToString().Trim();
                                amount = amount.Insert(amount.Length - 2, ".");
                                objInfo.RECEIPT_AMOUNT = Double.Parse(amount);
                            }
                            else
                                objInfo.RECEIPT_AMOUNT = 0.0;

                            //VALIDATE RTL_BATCH_NUMBER
                            if (objDeposit.IsRTLBatchNumber(arrFile[i].ToString().Trim()))
                                objInfo.RTL_BATCH_NUMBER = arrFile[i].Substring(17, 8).ToString();
                            else
                                objInfo.RTL_BATCH_NUMBER = "";

                            //VALIDATE RTL_GROUP_NUMBER
                            if (objDeposit.IsRTLGroupNumber(arrFile[i].ToString().Trim()))
                                objInfo.RTL_GROUP_NUMBER = arrFile[i].Substring(25, 8).ToString();
                            else
                                objInfo.RTL_GROUP_NUMBER = "";

                            objInfo.CREATED_BY = int.Parse(GetUserId());
                            objInfo.CREATED_FROM = "RTL";
                            objInfo.CREATED_DATETIME = DateTime.Now;
                            objArrLst.Add(objInfo);
                        }
                    }
                    catch (Exception objEx)
                    {
                        //lblMessage.Text = objEx.Message.ToString();
                        lblMessage.Text = "RTL file is not in Correct format. Please review the RTL file.";
                        objDeposit.LogRTLProcess(objInfo.DEPOSIT_ID, objInfo.POLICY_NO, "RTL file is not in Correct format : " + objEx.Message.ToString(), "FAILED", "", "RTL file is not in Correct format.Please review the RTL file.", objInfo.RECEIPT_AMOUNT.ToString());
                        lblMessage.Visible = true;
                        throw (objEx);
                    }
                }
                int RetVal;
                if (objArrLst != null && objArrLst.Count > 0)
                {
                    ClsDepositDetails objBl = new ClsDepositDetails();
                    RetVal = objBl.ImportRTLFile(objArrLst);
                    if (RetVal > 0)
                    {
                        lblMessage.Text = "Records Imported successfully.";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        //If RTL import Successful then Display the Commit option
                        btnCommit.Visible = true;
                    }
                    else if (RetVal == -2)
                    {
                        lblMessage.Text = "Not all Policies imported from RTL file. Please review customer deposit section.";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        //If RTL import Successful then Display the Commit option
                        btnCommit.Visible = true;
                    }
                    else if (RetVal == -3)
                    {
                        lblMessage.Text = "No records imported please review RTL file";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                    }
                    else if (RetVal == -10)
                    {
                        lblMessage.Text = "AB Type policies will not be Imported.";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                    }
                    else
                    {
                        btnImport.Enabled = true;
                    }

                }
                //Added For Itrack Issue #6330.
                GetOldDataXML();
            }
            catch (Exception objEx)
            {

                lblMessage.Text = "File could not be read as the RTL file is not in correct format. Please review the RTL file.";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
                lblMessage.Visible = true;
            }
        }
        /// <summary>
        /// Import the data of Co-Insurance
        /// </summary>
        // Added By Pradeep Kushwaha on 11-Jan-2010
        private void ImportFileDataOfCoInsurance()
        {
            ArrayList objArrLst = new ArrayList();
            try
            {
                // Get File Path /  File Name / File Length
                string strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
                string strDirName = Server.MapPath(strRoot);
                strDirName = strDirName + "\\DepositsRTLFile";

                XmlDocument objXMLDoc = new XmlDocument();
                objXMLDoc.LoadXml(hidOldData.Value);
                strUploadedFileName = ClsCommon.GetNodeValue(objXMLDoc, "//RTL_FILE");

                //=========  File Upload ===========START

                strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

                Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
                long FileLen = 0;
                StreamReader objSR = null;

                if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                {
                    FileInfo objFileInfo = new FileInfo(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);
                    FileLen = objFileInfo.Length;
                    // File StreamReader
                    objSR = new StreamReader(strDirName + "/" + hidDEPOSIT_ID.Value + "_" + strUploadedFileName);

                    //ending the impersonation 
                    objAttachment.endImpersonation();
                }
                else
                {
                    //Impersation failed
                    lblMessage.Text += "\n "+ ClsMessages.FetchGeneralMessage("1420");//Unable to Read the file. User impersonation failed.";
                }


                //Obj 
                ClsDepositDetails objDeposit = new ClsDepositDetails();

                string strFile;
                int i = 1;
                string[] arrFile = new string[FileLen];
                while ((strFile = objSR.ReadLine()) != null && i <= FileLen)
                {
                    ClsAddDepositDetailsinfo objAddDepositInfo = new ClsAddDepositDetailsinfo();
                    arrFile[i] = strFile;
                    try
                    {
                        if (strFile.Trim() != "")
                        {
                            objAddDepositInfo.DEPOSIT_ID.CurrentValue = int.Parse(hidDEPOSIT_ID.Value);
                            //Coinsurance Carrier Leader- 6785
                            if (objDeposit.ValidateValue(arrFile[i].ToString(), 0, 4))
                                objAddDepositInfo.COINS_CARRIER_LEADER.CurrentValue = arrFile[i].Substring(0, 4).ToString().Trim();
                            else
                                objAddDepositInfo.COINS_CARRIER_LEADER.CurrentValue = String.Empty;

                            //Policyholder Name -DENISE HELENA CARVALHO PASTORI     
                            if (objDeposit.ValidateValue(arrFile[i].ToString(),4, 40))
                                objAddDepositInfo.POLICY_HOLDER_NAME.CurrentValue = arrFile[i].Substring(4, 40).ToString().Trim();
                            else
                                objAddDepositInfo.POLICY_HOLDER_NAME.CurrentValue = String.Empty;

                            //SUSEP Class of Business-14
                            if (objDeposit.ValidateValue(arrFile[i].ToString(), 44, 2))
                                objAddDepositInfo.SUSEP_CLASS_OF_BUSINESS.CurrentValue =arrFile[i].Substring(44, 2).ToString().Trim();
                            else
                                objAddDepositInfo.SUSEP_CLASS_OF_BUSINESS.CurrentValue = String.Empty;

                            //Leader Policy Id-1932007
                            if (objDeposit.ValidateValue(arrFile[i].ToString(), 46, 7))
                                objAddDepositInfo.LEADER_POLICY_ID.CurrentValue = arrFile[i].Substring(46, 7).ToString().Trim();
                            else
                               objAddDepositInfo.LEADER_POLICY_ID.CurrentValue = String.Empty;

                            //Leader Doc Id(Endorement NO)-000000
                            if (objDeposit.ValidateValue(arrFile[i].ToString(), 53, 6))
                                objAddDepositInfo.LEADER_DOC_ID.CurrentValue = arrFile[i].Substring(53, 6).ToString().Trim();
                            else
                                objAddDepositInfo.LEADER_DOC_ID.CurrentValue = String.Empty;
                            //COINSURANCE TRANSACTION ID FIRST 3 DIGITS, Branch Coinsurance Id-000
                            if (objDeposit.ValidateValue(arrFile[i].ToString(), 59, 3))
                                objAddDepositInfo.BRANCH_COINS_ID.CurrentValue = arrFile[i].Substring(59, 3).ToString().Trim();
                            else
                                objAddDepositInfo.BRANCH_COINS_ID.CurrentValue = String.Empty;
                            //COINSURANCE TRANSACTION ID LAST 10 DIGITS,  Coinsurance Id-0000320691
                            if (objDeposit.ValidateValue(arrFile[i].ToString(), 62, 10))
                                objAddDepositInfo.COINSURANCE_ID.CurrentValue = arrFile[i].Substring(62, 10).ToString().Trim();
                            else
                                objAddDepositInfo.COINSURANCE_ID.CurrentValue = String.Empty;
                            //Installment Number-06
                            if (objDeposit.ValidateNumber(arrFile[i].ToString(), 72, 2))
                                objAddDepositInfo.INSTALLMENT_NO.CurrentValue = Convert.ToInt32(arrFile[i].Substring(72, 2).ToString().Trim());
                            else
                                objAddDepositInfo.INSTALLMENT_NO.CurrentValue = base.GetEbixIntDefaultValue();
                            //Number of Installments-01
                            if (objDeposit.ValidateNumber(arrFile[i].ToString(), 74, 2))
                                objAddDepositInfo.NO_OF_INSTALLMENTS.CurrentValue = Convert.ToInt32(arrFile[i].Substring(74, 2).ToString().Trim());
                            else
                                objAddDepositInfo.NO_OF_INSTALLMENTS.CurrentValue = base.GetEbixIntDefaultValue();
                           
                            //Payment Day-26
                            //Payment Month-07
                            //Payment Year-2010
                            DateTime validPaymentDate = new DateTime();
                            if (objDeposit.IsRTLPaymentDateValid(arrFile[i].ToString().Trim(), ref validPaymentDate))
                                objAddDepositInfo.PAYMENT_DATE.CurrentValue = Convert.ToDateTime(validPaymentDate.ToShortDateString());
                            //Premium Amount-0000000004453
                            string strPremiumAmount = String.Empty;
                            if (objDeposit.IsPremiumAmountValid(arrFile[i].ToString()))
                            {
                                strPremiumAmount = arrFile[i].Substring(84, 13).ToString().Trim();
                                if (ClsCommon.BL_LANG_ID == 2)
                                    strPremiumAmount = strPremiumAmount.Insert(strPremiumAmount.Length - 2, ",");
                                else
                                    strPremiumAmount = strPremiumAmount.Insert(strPremiumAmount.Length - 2, ".");
                                objAddDepositInfo.RISK_PREMIUM.CurrentValue = Double.Parse(strPremiumAmount);
                            }
                            else
                                objAddDepositInfo.RISK_PREMIUM.CurrentValue = base.GetEbixDoubleDefaultValue();

                            //Commission Amount-0000000001461
                            string strCommissionAmount = String.Empty;
                            if (objDeposit.IsCommissionAmountValid(arrFile[i].ToString()))
                            {
                                strCommissionAmount = arrFile[i].Substring(97, 13).ToString().Trim();
                                if (ClsCommon.BL_LANG_ID == 2)
                                    strCommissionAmount = strCommissionAmount.Insert(strCommissionAmount.Length - 2, ",");
                                else
                                    strCommissionAmount = strCommissionAmount.Insert(strCommissionAmount.Length - 2, ".");
                                objAddDepositInfo.COMMISSION_AMOUNT.CurrentValue = Double.Parse(strCommissionAmount);
                            }
                            else
                                objAddDepositInfo.COMMISSION_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue();
                            //Coinsurance Fee Amount-0000000000000
                            string strCoinsuranceFeeAmount = String.Empty;
                            if (objDeposit.IsCoinsuranceFeeAmountValid(arrFile[i].ToString()))
                            {
                                strCoinsuranceFeeAmount = arrFile[i].Substring(110, 13).ToString().Trim();
                                if (ClsCommon.BL_LANG_ID == 2)
                                    strCoinsuranceFeeAmount = strCoinsuranceFeeAmount.Insert(strCoinsuranceFeeAmount.Length - 2, ",");
                                else
                                    strCoinsuranceFeeAmount = strCoinsuranceFeeAmount.Insert(strCoinsuranceFeeAmount.Length - 2, ".");
                                objAddDepositInfo.FEE.CurrentValue = Double.Parse(strCoinsuranceFeeAmount);
                            }
                            else
                                objAddDepositInfo.FEE.CurrentValue = base.GetEbixDoubleDefaultValue();
                            //Interest-0000000000419
                            string strInterestAmount = String.Empty;
                            if (objDeposit.IsInterestValid(arrFile[i].ToString()))
                            {
                                strInterestAmount = arrFile[i].Substring(123, 13).ToString().Trim();
                                if (ClsCommon.BL_LANG_ID == 2)
                                    strInterestAmount = strInterestAmount.Insert(strInterestAmount.Length - 2, ",");
                                else
                                    strInterestAmount = strInterestAmount.Insert(strInterestAmount.Length - 2, ".");
                                objAddDepositInfo.INTEREST.CurrentValue = Double.Parse(strInterestAmount);
                            }
                            else
                                objAddDepositInfo.INTEREST.CurrentValue = base.GetEbixDoubleDefaultValue();
                            
                            //Net Premium-0000000003410
                            string strNetPremium = String.Empty;
                            if (objDeposit.IsNetPremiumValid(arrFile[i].ToString()))
                            {
                                strNetPremium = arrFile[i].Substring(136, 13).ToString().Trim();
                                if (ClsCommon.BL_LANG_ID == 2)
                                    strNetPremium = strNetPremium.Insert(strNetPremium.Length - 2, ",");
                                else
                                    strNetPremium = strNetPremium.Insert(strNetPremium.Length - 2, ".");
                                objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = Double.Parse(strNetPremium);
                                //Added by Pradeep to get the Net installment Amount in Total premium collection As discussed with Anurag
                                objAddDepositInfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Double.Parse(strNetPremium);
                            }
                            else
                            {
                                objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = base.GetEbixDoubleDefaultValue();
                                //Added by Pradeep to get the Net installment Amount in Total premium collection As discussed with Anurag
                                objAddDepositInfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = base.GetEbixDoubleDefaultValue();
                            }


                            objAddDepositInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                            objAddDepositInfo.CREATED_FROM.CurrentValue = "RTL";
                            objAddDepositInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            objAddDepositInfo.IS_ACTIVE.CurrentValue = "Y";
                            objAddDepositInfo.DEPOSIT_TYPE.CurrentValue =cmbDEPOSIT_TYPE.SelectedItem.Value.ToString();
                            objAddDepositInfo.DEPOSIT_TYPE.CurrentValue =  hidDEPOSIT_TYPE.Value.ToString() ;
                            
                          
                            objArrLst.Add(objAddDepositInfo);
                        }
                    }
                    catch (Exception objEx)
                    {
                        #region SET MIN VALUE
                        if (objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue == double.MinValue)
                            objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(null);
                        if (objAddDepositInfo.LATE_FEE.CurrentValue == double.MinValue)
                            objAddDepositInfo.LATE_FEE.CurrentValue = Convert.ToDouble(null);
                         
                        #endregion

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1418");  //"RTL file is not in Correct format. Please review the RTL file.";
                        objDeposit.LogRTLProcessoCoInsurance(ClsMessages.FetchGeneralMessage("1419") + objEx.Message.ToString(),
                            ClsMessages.FetchGeneralMessage("1878"), "", ClsMessages.FetchGeneralMessage("1418"), objAddDepositInfo, 396, 401, 402);

                        lblMessage.Visible = true;
                        throw (objEx);
                    }
                }
                int RetVal;
                if (objArrLst != null && objArrLst.Count > 0)
                {
                    ClsDepositDetails objDepositDetails = new ClsDepositDetails();
                    RetVal = objDepositDetails.ImportRTLFileOfCoInsurance(objArrLst, int.Parse(GetUserId()));
                    if (RetVal > 0)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1417"); //"Records Imported successfully.";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        //If RTL import Successful then Display the Commit option
                        btnCommit.Visible = true;
                    }
                    else if (RetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "13");// "Not all Co-Insurance imported from RTL file. Please review Co-Insurance deposit section.";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                        //If RTL import Successful then Display the Commit option
                        btnCommit.Visible = true;
                    }
                    else if (RetVal == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(ScreenId, "11");// "No records imported please review RTL file";
                        lblMessage.Visible = true;
                        btnImport.Enabled = false;
                    }
                    else
                    {
                        btnImport.Enabled = true;
                    }

                }
                
            }
            catch (Exception objEx)
            {

                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "12");// "File could not be read as the RTL file is not in correct format. Please review the RTL file.";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
                lblMessage.Visible = true;
            }
        }
		private void btnImport_Click(object sender, System.EventArgs e)
		{
            //this.ImportFileDataForPolicyNumber();
            if (hidDEPOSIT_TYPE.Value.ToString() == "14831")	//Normal
                //this.ImportFileDataForOurNumber();
                this.ReceiveRTLBankFileData();
            if (hidDEPOSIT_TYPE.Value.ToString() == "14832")//Co-Insurance
                this.ImportFileDataOfCoInsurance();
            

		}
	
		#endregion

		#region Helper Functions(ScreenID / ErrorMessages / Captions / GetOldXML / Fill...)
		private void SetScreenId()
		{
			base.ScreenId="187_0";
		}

		private void SetErrorMessages()
		{ 
			rfvFISCAL_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("919");
			rfvACCOUNT_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvDEPOSIT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
            //rfvDEPOSIT_TRAN_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revDEPOSIT_NUMBER.ValidationExpression = aRegExpInteger;
			revDEPOSIT_TRAN_DATE.ValidationExpression = aRegExpDate;
			revDEPOSIT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
            revDEPOSIT_TRAN_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");
            csvDEPOSIT_TRAN_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "405");
			rfvRECEIPT_MODE.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("877");
            hidTAB_TITLES.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            csvDEPOSIT_TRAN_DATE2.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
            
            
		}
		private void SetCaptions()
		{
            capMandatoryNotes.Text = ClsMessages.FetchGeneralMessage("1168");
			capFISCAL_ID.Text			= objResourceMgr.GetString("cmbFISCAL_ID");
			capACCOUNT_ID.Text			= objResourceMgr.GetString("cmbACCOUNT_ID");
			capDEPOSIT_NUMBER.Text		= objResourceMgr.GetString("txtDEPOSIT_NUMBER");
			capDEPOSIT_TRAN_DATE.Text	= objResourceMgr.GetString("txtDEPOSIT_TRAN_DATE");
			capTOTAL_DEPOSIT_AMOUNT.Text= objResourceMgr.GetString("lblTOTAL_DEPOSIT_AMOUNT");
			capDEPOSIT_NOTE.Text		= objResourceMgr.GetString("txtDEPOSIT_NOTE");
			capCREATED_DATETIME.Text	= objResourceMgr.GetString("lblCREATED_DATETIME");
			capRECEIPT_MODE.Text		= objResourceMgr.GetString("cmbRECEIPT_MODE");
            capDEPOSIT_NOTE.Text        = objResourceMgr.GetString("cmbDEPOSIT_TYPE");
            capDEPOSIT_TYPE.Text        = objResourceMgr.GetString("cmbDEPOSIT_TYPE");
            capACCOUNT_BALANCE.Text = objResourceMgr.GetString("txtACCOUNT_BALANCE");
            capTapeTotalCust.Text = objResourceMgr.GetString("txtTapeTotalCust");
            capTapeTotalAgency.Text = objResourceMgr.GetString("txtTapeTotalAgency");
            capTapeTotalClaim.Text = objResourceMgr.GetString("txtTapeTotalClaim");
            capTotalMisc.Text = objResourceMgr.GetString("txtTotalMisc");
            capRTL_FILE.Text = objResourceMgr.GetString("txtRTL_FILE");
            btnCommit.Text = objResourceMgr.GetString("btnCommit");
            btnCopy.Text = objResourceMgr.GetString("btnCopy");
            btnNext.Text = objResourceMgr.GetString("btnNext");
            btnSave.Text = objResourceMgr.GetString("btnSave");
            btnImport.Text = objResourceMgr.GetString("btnImport");
            btnShowExceptionItems.Text = objResourceMgr.GetString("btnShowExceptionItems");
            btnImportProgress.Text = objResourceMgr.GetString("btnImportProgress");
            hidMessage.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1766");
            
		}
		
		private void GetQueryString()
		{
			if(Request.QueryString["DEPOSIT_ID"]!=null && Request.QueryString["DEPOSIT_ID"].ToString()!="")
				hidDEPOSIT_ID.Value		= Request.QueryString["DEPOSIT_ID"].ToString();
			if(Request.QueryString["DEPOSIT_TYPE"]!=null && Request.QueryString["DEPOSIT_TYPE"].ToString()!="")
				hidDEPOSIT_TYPE.Value		= Request.QueryString["DEPOSIT_TYPE"].ToString();
		}

		/// <summary>
		/// Retreive the information about selected record in the form of XML
		/// and saves it into hidden control
		/// </summary>
		private void GetOldDataXML()
		{
			

			if (hidDEPOSIT_ID.Value != "")
			{
				hidOldData.Value = ClsDeposit.GetDepositInfo(int.Parse(hidDEPOSIT_ID.Value));
				//Added For Itrack Issue 5013
				hidBalance.Value = ClsCommon.FetchValueFromXML("ACCOUNT_BALANCE",hidOldData.Value);
				hidACCOUNT_ID.Value = ClsCommon.FetchValueFromXML("ACCOUNT_ID", hidOldData.Value);   			   
				hidDEPOSIT_NO.Value = ClsCommon.FetchValueFromXML("DEPOSIT_NUMBER",hidOldData.Value); 
				if (hidOldData.Value != "" )
				{

					//by default commit should be invisible
					btnCommit.Visible = false;
					//Fetching values from old xml
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
                    cmbDEPOSIT_TYPE.Enabled = false;
					string strFiscalId = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//FISCAL_ID");
					if (strFiscalId != "")
					{
						FillAccountDetails(int.Parse(strFiscalId));
					}
                    
					lblCREATED_DATETIME.Text =ConvertToDateCulture(Convert.ToDateTime(Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//CREATED_DATETIME")));

                    hidCREATED_DATETIME.Value = lblCREATED_DATETIME.Text;

					txtDEPOSIT_NUMBER.Text = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//DEPOSIT_NUMBER");
                    hidDEPOSIT_NUMBER.Value = txtDEPOSIT_NUMBER.Text;
                    hidDEPOSIT_TRAN_DATE.Value = ConvertToDateCulture(Convert.ToDateTime(Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//DEPOSIT_TRAN_DATE"))); 

					//Import Hide Show
					string rtlLineItems = ClsCommon.GetNodeValue(doc,"//RTL_LINE_ITEMS");
					if(rtlLineItems!="0")
						btnImport.Enabled = false;
					else
						btnImport.Enabled = true;

					//Commit should be visible for confirm deposit having no of line items greater then zero
                    //if (Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//IS_CONFIRM").ToUpper() == "Y")
                    //{
						string NO_OF_LINE_ITEMS = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//NO_OF_LINE_ITEMS");

						if( NO_OF_LINE_ITEMS.Trim() != ""  && int.Parse(NO_OF_LINE_ITEMS.Trim()) > 0)
						{
							btnCommit.Visible = true;
						}
                    //}
					if(ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value)!="" && ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value)!= null)
					{
						string filename = hidRootPath.Value + ClsCommon.FetchValueFromXML("DEPOSIT_ID",hidOldData.Value) + '_' + ClsCommon.FetchValueFromXML("RTL_FILE",hidOldData.Value);
						int startOfFile = filename.IndexOf("Upload");
						string filePath = filename.Substring(startOfFile + 6);
						string []fileURL = filePath.Split('.'); 
						string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
						hidfileLink.Value = EncryptedPath;
					}
				}	
			}
		}

		/// <summary>
		/// Fills the various combo boxes in the page
		/// </summary>
		private void FillCombos()
		{
			try
			{
				Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgersIndropDown(cmbFISCAL_ID);
				cmbFISCAL_ID.Items.Insert(0,new ListItem("",""));
				cmbRECEIPT_MODE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RPT_MD",null,"Y");
				cmbRECEIPT_MODE.DataTextField="LookupDesc";
				cmbRECEIPT_MODE.DataValueField="LookupID";
				cmbRECEIPT_MODE.DataBind();
				
                ListItem Li = new ListItem();
                Li = cmbRECEIPT_MODE.Items.FindByValue("11976"); // Remove option : "EFT-Sweep"
                cmbRECEIPT_MODE.Items.Remove(Li);
               
                ListItem Li1 = new ListItem();
                Li1 = cmbRECEIPT_MODE.Items.FindByValue("11977"); // Remove option : "Already Processed"
                cmbRECEIPT_MODE.Items.Remove(Li1);

                cmbRECEIPT_MODE.Items.Insert(0, "");
			}
			catch(Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}

		/// <summary>
		/// Fills information related to accounts, account combox and deposit no
		/// </summary>
		private void FillAccountDetails(int FiscalId)
		{
			DataSet ds = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetCashAccountInfo(FiscalId);
			
            //cmbACCOUNT_ID.DataSource = ds.Tables[0];
            //cmbACCOUNT_ID.DataTextField = "ACC_DESCRIPTION";
            //cmbACCOUNT_ID.DataValueField = "EXTRA_INFO";
            //cmbACCOUNT_ID.DataBind();

            cmbACCOUNT_ID.DataSource = ds.Tables[0];
            cmbACCOUNT_ID.DataTextField = "ACC_DESCRIPTION";
            cmbACCOUNT_ID.DataValueField = "BANK_ID";
            cmbACCOUNT_ID.DataBind();

			if(cmbACCOUNT_ID.Items.Count > 0)
			{
				string defaultDepAcc  = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDefaultDepositsAcc();
				if (cmbACCOUNT_ID.Items.FindByValue(defaultDepAcc) != null)
				{
					//Defaulting the bank account in the ACT_GENERAL_LEDGER
					cmbACCOUNT_ID.SelectedValue = defaultDepAcc;
				}
				else
				{
					//Defaulting the zeroth item
					cmbACCOUNT_ID.SelectedIndex = 0;
				}

				//Calling the selected index change event , which will set the deposit number
				//cmbACCOUNT_ID_SelectedIndexChanged(null, null);
				//Added 20 June
//				if (hidDEPOSIT_ID.Value == "" || hidDEPOSIT_ID.Value == "New")
//				{
					if (cmbACCOUNT_ID.SelectedItem != null && cmbFISCAL_ID.SelectedItem != null)
					txtDEPOSIT_NUMBER.Text = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(int.Parse(cmbFISCAL_ID.SelectedValue), int.Parse(cmbACCOUNT_ID.SelectedValue));
                    hidDEPOSIT_NUMBER.Value = txtDEPOSIT_NUMBER.Text;
					//lblACCOUNT_BALANCE.Text =	Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetAccountBalanceByAccountID(int.Parse(cmbACCOUNT_ID.SelectedValue));
		//		}
			}
		}


		/// <summary>
		/// Enable and disable ctrls, depending upon the existence of child controls
		/// COMMENTED : Irrelevant function.
		/// </summary>
		/*private void EnableDisableControls()
		{
			try
			{
				objDeposit = new ClsDeposit();
				
				if  (hidDEPOSIT_ID.Value != "")
				{
					if (objDeposit.ChildRecordExists(int.Parse(hidDEPOSIT_ID.Value)))
					{
						//child record exists hence
						//hence disabling accounting and dept drop down
						cmbFISCAL_ID.Enabled = false;
						//cmbACCOUNT_ID.Enabled = false;
						txtDEPOSIT_NUMBER.Enabled = false;
					}
				}
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}*/

		/// <summary>
		/// This function defaults the fiscal yr drop down to current yr
		/// </summary>
        /// 


        private void BindDepositType()
        {
            cmbDEPOSIT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DESTYP");
            cmbDEPOSIT_TYPE.DataTextField = "LookupDesc";
            cmbDEPOSIT_TYPE.DataValueField = "LookupID";
            cmbDEPOSIT_TYPE.DataBind();
            //ListItem Li = new ListItem();
            //Li = cmbDEPOSIT_TYPE.Items.FindByValue("14916");
            //cmbDEPOSIT_TYPE.Items.Remove(Li);
            //ListItem Lit = new ListItem();
            //Lit = cmbDEPOSIT_TYPE.Items.FindByValue("14917");
            //cmbDEPOSIT_TYPE.Items.Remove(Lit);
            //ListItem Lit1 = new ListItem();
            //Lit1 = cmbDEPOSIT_TYPE.Items.FindByValue("14918");
            //cmbDEPOSIT_TYPE.Items.Remove(Lit1);
                        
        } //private void BindConstruction()

        
		private void SetFiscalYear()
		{
			try
			{
				DateTime tranDate = DateTime.Now;
				string trandate1 = DateTime.Now.ToShortDateString();
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
                     
					//Added by Uday to Get the financial begin date & financial end date
                    int i = Convert.ToInt32(DateTime.Compare(DateTime.Parse(trandate1), DateTime.Parse(d1)));
                    int j = Convert.ToInt32(DateTime.Compare(DateTime.Parse(trandate1), DateTime.Parse(d2)));

                    if (tranDate > DateTime.Parse(d1) && tranDate < DateTime.Parse(d2))		
					{
						//Transaction date is in between financial dates
						//Hence selecting this fiscal year
						cmbFISCAL_ID.SelectedIndex = ctr;
						cmbFISCAL_ID_SelectedIndexChanged(null, null);
						break;
					}
					else if(i==0 || j==0)
					{	
						//Transaction date is in between financial dates
						//Hence selecting this fiscal year
						cmbFISCAL_ID.SelectedIndex = ctr;
						cmbFISCAL_ID_SelectedIndexChanged(null, null);
						break;
					}
					//
				}
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

		#endregion
	
		#region UPLOAD FILE :: Add / Create Directory Structure / Save Uploaded File

		protected void UploadFile()
		{
			//=================== File Upload ====================
			//Beginigng the impersonation 
			Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
			if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
			{
				if (!SaveUploadedFile(txtRTL_FILE))
				{
					//Some error occured while uploading 
                    lblMessage.Text += "\n " + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15") ; //"\n Unable to upload the file.";
				}
										
				//ending the impersonation 
				objAttachment.endImpersonation();
			}
			else
			{
				//Impersation failed
				lblMessage.Text += "\n Unable to upload the file. User impersonation failed.";
			}
			//=================== File Upload ====================
		}
		private bool SaveUploadedFile(HtmlInputFile objFile1)
		{
			try
			{
				//Stores the name of the directory where file will get stored
				string strDirName;
				strDirName = CreateDirStructure();

				
				//Retreiving the extension -- FILE 1
				string strFileName1;
				int Index1 = objFile1.PostedFile.FileName.LastIndexOf("\\");
				if (Index1 >=0 )
				{
					strFileName1 = objFile1.PostedFile.FileName.Substring(Index1+1);
				}
				else
				{
					strFileName1 = objFile1.PostedFile.FileName;
				}

				//copying the files
				objFile1.PostedFile.SaveAs(strDirName + "\\" + hidDEPOSIT_ID.Value + "_" + strFileName1);

				return true;
			}
			catch (Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				return false;
			}
		}

		
		private string CreateDirStructure()
		{
			
			string strRoot, strDirName = "";
			try
			{
                strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
				strDirName = Server.MapPath(strRoot);
				//Creating the Attachment folder if not exists
				strDirName = strDirName + "\\DepositsRTLFile";
				if (!System.IO.Directory.Exists(strDirName))
				{
					//Creating the directory
					System.IO.Directory.CreateDirectory(strDirName);
				}
			}
			catch (Exception objEx)
			{
				throw (objEx);
			}
			return strDirName;
		}
		#endregion

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public String AjaxGetDepositDetails(int DEPOSIT_ID, String DEPOSIT_TYPE)
        {
            try
            {
                String RetVal = String.Empty;
                ClsDepositDetails objDepositDetails = new ClsDepositDetails();
                DataSet objDataSet = new DataSet();

                objDataSet = objDepositDetails.GetDepositLineItemDetails(DEPOSIT_ID, DEPOSIT_TYPE);
                if (objDataSet != null && objDataSet.Tables[0].Rows.Count > 0)
                    RetVal = "true";
                else
                    RetVal = "false";

                return RetVal;
            }

            catch
            {
                return "false";
            }
        }
	}
}
