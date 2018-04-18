/******************************************************************************************
<Author				: -   Ajit Singh chahal
<Start Date				: -	7/8/2005 4:16:15 PM
<End Date				: -	
<Description				: - 	Code behind for add bank reconciliation.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Code behind for add bank reconciliation.
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
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using System.Xml;
using System.IO;
using System.Text;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Code behind for add bank reconciliation.
	/// </summary>
	public class AddBankRecon : Cms.Account.AccountBase
	{
			#region Page controls declaration
			protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		
			protected System.Web.UI.WebControls.TextBox txtSTATEMENT_DATE;
			protected System.Web.UI.WebControls.TextBox txtSTARTING_BALANCE;
			protected System.Web.UI.WebControls.TextBox txtENDING_BALANCE;
			protected System.Web.UI.WebControls.TextBox txtBANK_CHARGES_CREDITS;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidAC_RECONCILIATION_ID;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidREF_FILE_ID;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidStarting_bal;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidUPLOAD_FILE;
		

			protected Cms.CmsWeb.Controls.CmsButton btnReset;
			protected Cms.CmsWeb.Controls.CmsButton btnSave;
			protected Cms.CmsWeb.Controls.CmsButton btnDelete;
			
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATEMENT_DATE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTARTING_BALANCE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDING_BALANCE;

			protected System.Web.UI.WebControls.RegularExpressionValidator revSTARTING_BALANCE;
			protected System.Web.UI.WebControls.RegularExpressionValidator revENDING_BALANCE;
			protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_CHARGES_CREDITS;
			protected System.Web.UI.WebControls.RegularExpressionValidator revSTATEMENT_DATE;
			protected System.Web.UI.WebControls.Label lblMessage;
			
			protected System.Web.UI.WebControls.Label capACCOUNT_ID;
			
			protected System.Web.UI.WebControls.Label capSTATEMENT_DATE;
			protected System.Web.UI.WebControls.Label capSTARTING_BALANCE;
			protected System.Web.UI.WebControls.Label capENDING_BALANCE;
			protected System.Web.UI.WebControls.Label capBANK_CHARGES_CREDITS;
			protected System.Web.UI.WebControls.Label capGL_NAME;
			protected System.Web.UI.WebControls.TextBox txtGL_NAME;
			protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		    protected System.Web.UI.HtmlControls.HtmlInputHidden hidfileLink;
			#endregion

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
	 
			protected Cms.CmsWeb.Controls.CmsButton btnCommit;
			protected Cms.CmsWeb.Controls.CmsButton btnItemsToReconcile;
			protected Cms.CmsWeb.Controls.CmsButton btnReProcessItemsToReconcile;
        	protected Cms.CmsWeb.Controls.CmsButton btnDistribute;
			protected System.Web.UI.WebControls.HyperLink hlkSTATEMENT_DATE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidEvent;
			protected Cms.CmsWeb.Controls.CmsButton btnRptItemsToRecon;
			protected Cms.CmsWeb.Controls.CmsButton btnRptOustandings;
			protected Cms.CmsWeb.Controls.CmsButton btnImport;
			protected System.Web.UI.WebControls.Label capUPLOAD_FILE;
			protected System.Web.UI.HtmlControls.HtmlInputFile txtUPLOAD_FILE;
			protected System.Web.UI.WebControls.Label lblUPLOAD_FILE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;
		
			//Defining the business layer class object
			ClsBankRconciliation  objBankRconciliation ;
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
				rfvACCOUNT_ID.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
								
				rfvSTATEMENT_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
				rfvSTARTING_BALANCE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
				rfvENDING_BALANCE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

				revSTARTING_BALANCE.ValidationExpression		=  aRegExpCurrencyformat;
				revENDING_BALANCE.ValidationExpression			=  aRegExpCurrencyformat;
				revBANK_CHARGES_CREDITS.ValidationExpression	=  aRegExpCurrencyformat;
				revSTATEMENT_DATE.ValidationExpression			=	aRegExpDate;

				revSTARTING_BALANCE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
				revENDING_BALANCE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
				revBANK_CHARGES_CREDITS.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
				revSTATEMENT_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			}
			#endregion

			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				btnReset.Attributes.Add("onclick","javascript:return formReset();");
				btnDistribute.Attributes.Add("onclick","javascript:return OpenDistributeReconciliation();");
				btnItemsToReconcile.Attributes.Add("onclick","javascript:return OpenItemsTobeReconcilied();");
				btnRptItemsToRecon.Attributes.Add("onClick","javascript:return OpenDetailReport('DETAIL');");
				btnRptOustandings.Attributes.Add("onClick","javascript:return OpenDetailReport('OUTSTANDING');");
				btnDelete.Attributes.Add("onclick","DisableValidators();");
				//btnRptItemsToRecon.Attributes.Add("onclick"  

				btnImport.Attributes.Add("onclick","DisableButtonOnImport();");
				btnCommit.Attributes.Add("onclick","return confirmCommit();");  
				
				

				txtSTARTING_BALANCE.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtSTARTING_BALANCE'));");
				txtENDING_BALANCE.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtENDING_BALANCE'));");
				txtBANK_CHARGES_CREDITS.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtBANK_CHARGES_CREDITS'));");
				// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
				base.ScreenId="205_0";
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass			=	CmsButtonType.Execute;
				btnReset.PermissionString		=	gstrSecurityXML;

				btnSave.CmsButtonClass			=	CmsButtonType.Execute;
				btnSave.PermissionString		=	gstrSecurityXML;

				btnCommit.CmsButtonClass		=	CmsButtonType.Execute;
				btnCommit.PermissionString		=	gstrSecurityXML;

				btnItemsToReconcile.CmsButtonClass			=	CmsButtonType.Execute;
				btnItemsToReconcile.PermissionString		=	gstrSecurityXML;

				btnReProcessItemsToReconcile.CmsButtonClass			=	CmsButtonType.Execute;
				btnReProcessItemsToReconcile.PermissionString		=	gstrSecurityXML;

				btnDelete.CmsButtonClass			=	CmsButtonType.Delete;
				btnDelete.PermissionString			=	gstrSecurityXML;

				btnDistribute.CmsButtonClass		=	CmsButtonType.Execute;
				btnDistribute.PermissionString		=	gstrSecurityXML;

				btnRptItemsToRecon.CmsButtonClass	=	CmsButtonType.Read ;
				btnRptItemsToRecon.PermissionString =	gstrSecurityXML;

				btnRptOustandings.CmsButtonClass	=	CmsButtonType.Read;
				btnRptOustandings.PermissionString	=	gstrSecurityXML;

				btnImport.CmsButtonClass	= CmsButtonType.Execute;
				btnImport.PermissionString	= gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddBankRecon" ,System.Reflection.Assembly.GetExecutingAssembly());
				
				hlkSTATEMENT_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_BANK_RECONCILIATION.txtSTATEMENT_DATE,document.ACT_BANK_RECONCILIATION.txtSTATEMENT_DATE)");
				hidRootPath.Value  = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/BankReconFile/";
				if(!Page.IsPostBack)
				{
					Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID);
					SetCaptions();
					if(Request.QueryString["AC_RECONCILIATION_ID"]!=null && Request.QueryString["AC_RECONCILIATION_ID"].ToString().Length>0)
					{
						hidOldData.Value = ClsBankRconciliation.GetXmlForPageControls(Request.QueryString["AC_RECONCILIATION_ID"].ToString());
						EnableDisableImportButton(hidOldData.Value);
						XmlDocument xdoc = new XmlDocument();
						xdoc.LoadXml(hidOldData.Value);
						ClsCommon.GetNodeValue(xdoc,"//ACCOUNT_ID");
						//cmbACCOUNT_ID.SelectedValue=ClsCommon.GetNodeValue(xdoc,"//ACCOUNT_ID");
						ListItem Li = new ListItem();
						Li.Value = ClsCommon.GetNodeValue(xdoc,"//ACCOUNT_ID");
						if(Li.Value != "" && Li != null)
						{
							if(cmbACCOUNT_ID.Items.FindByValue(Li.Value) != null)
								cmbACCOUNT_ID.SelectedValue = Li.Value;
							else
								cmbACCOUNT_ID.SelectedIndex = -1;
						}

						GetReconciliationStatus(Request.QueryString["AC_RECONCILIATION_ID"].ToString());

						if(ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value)!="" && ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value)!= null)
						{
							string []file = ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value).Split('_');
							string []fileExt = file[1].Split('.');
							string filename = hidRootPath.Value + file[0] + "_"+ ClsCommon.FetchValueFromXML("AC_RECONCILIATION_ID",hidOldData.Value)+ "." +fileExt[1];
							int startOfFile = filename.IndexOf("Upload");
							string filePath = filename.Substring(startOfFile + 6);
							string []fileURL = filePath.Split('.'); 
							string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
							hidfileLink.Value = EncryptedPath;
						}
//						
					}
					else
					{
						txtGL_NAME.Text	= Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGLName();
					}
				}
				//hidRootPath.Value  = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/BankReconFile/";
			}//end pageload
			#endregion
			
			#region GetFormValue
			/// <summary>
			/// Fetch form's value and stores into model class object and return that object.
			/// </summary>
			private ClsBankRconciliationInfo GetFormValue()
			{
				//Creating the Model object for holding the New data
				ClsBankRconciliationInfo objBankRconciliationInfo;
				objBankRconciliationInfo = new ClsBankRconciliationInfo();

				objBankRconciliationInfo.ACCOUNT_ID				=	int.Parse(cmbACCOUNT_ID.SelectedValue);
				objBankRconciliationInfo.STATEMENT_DATE			=	DateTime.Parse(txtSTATEMENT_DATE.Text);
				//objBankRconciliationInfo.STARTING_BALANCE		=	double.Parse(txtSTARTING_BALANCE.Text);
				if(hidStarting_bal.Value!="")
					objBankRconciliationInfo.STARTING_BALANCE		=	double.Parse(hidStarting_bal.Value);
				else
					objBankRconciliationInfo.STARTING_BALANCE		=	double.Parse(txtSTARTING_BALANCE.Text);
				
				objBankRconciliationInfo.ENDING_BALANCE			=	double.Parse(txtENDING_BALANCE.Text);
				if(txtBANK_CHARGES_CREDITS.Text.Length>0)
					objBankRconciliationInfo.BANK_CHARGES_CREDITS	=	double.Parse(txtBANK_CHARGES_CREDITS.Text);

				//These  assignments are common to all pages.
				strFormSaved	=	hidFormSaved.Value;
				strRowId		=	hidAC_RECONCILIATION_ID.Value;
				oldXML			=	hidOldData.Value;
				//Returning the model object

				//=========  File Upload ===========START
			
				strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

				//---- Begin
				//Below code gets the File name from Label or Text i.e; 
				//in either update or saved case.
				if(oldXML != "")
				{
					System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
					xmlDoc.LoadXml(oldXML);
							
					System.Xml.XmlNode xmlNode = xmlDoc.SelectSingleNode("/NewDataSet/Table/UPLOAD_FILE");
					if(xmlNode!=null)
						lblUPLOAD_FILE.Text		=  xmlNode.InnerText;
					
				}	
					//if (Request.Form["hidUPLOAD_FILE"].ToString().Equals("Y"))	
					if (hidUPLOAD_FILE.Value.Equals("Y"))
						strFileName	=	txtUPLOAD_FILE.PostedFile.FileName;
					else				
						strFileName	=	lblUPLOAD_FILE.Text;				
					
					//----  End
					int intIndex		=	strFileName.LastIndexOf("\\");
					strFileName		=	strFileName.Substring(intIndex+1);	//Taking only file name not whole path
					//=========  File Upload ===========END
					return objBankRconciliationInfo;
				}
			#endregion
			
			#region UPLOAD FILE :: Add / Create Directory Structure / Save Uploaded File

		protected void UploadFile()
		{
			//=================== File Upload ====================
			//Beginigng the impersonation 
			Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
			if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
			{
				if (!SaveUploadedFile(txtUPLOAD_FILE))
				{
					//Some error occured while uploading 
					lblMessage.Text += "\n Unable to upload the file.";
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
				
				string[] fileArray;
				fileArray = strFileName1.Split('.');
				strFileName1 = fileArray[0].ToString() + "_" + hidAC_RECONCILIATION_ID.Value.ToString() + "." + fileArray[1].ToString();
				//copying the files
				objFile1.PostedFile.SaveAs(strDirName + "\\" + strFileName1);

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
				strDirName = strDirName + "\\BankReconFile";
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

		private void DeleteFile(string fileName)
		{	
			string strRoot, strDirName = "";
			try
			{
				//Beginigng the impersonation 
				Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
				//Ravindra(12-17-2008); Fetch Credentials from web.config before Impersonating 
				strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

				if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
				{
					strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
					strDirName = Server.MapPath(strRoot);
					strDirName = strDirName + "\\BankReconFile";

					FileInfo TheFile = new FileInfo(strDirName + "\\" + fileName); 
					if (TheFile.Exists) 
					{ 
						File.Delete(strDirName + "\\" + fileName); 
					}
				}
				else
				{
					//Impersation failed
					lblMessage.Text += "\n Unable to Delte the file. User impersonation failed.";
				}

			}
			catch (Exception objEx)
			{
				throw (objEx);
			}
			
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
				this.cmbACCOUNT_ID.SelectedIndexChanged += new System.EventHandler(this.cmbACCOUNT_ID_SelectedIndexChanged);
				this.btnReProcessItemsToReconcile.Click += new System.EventHandler(this.btnReProcessItemsToReconcile_Click);
				this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
				this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
				this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
				this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
				this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
                this.Load += new System.EventHandler(this.Page_Load);

			}
			#endregion

			#region "Web Event Handlers"
			private void btnDistribute_Click(object sender, System.EventArgs e)
			{
			
			}
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
					objBankRconciliation = new  ClsBankRconciliation(true);

					//Retreiving the form values into model class object
					ClsBankRconciliationInfo objBankRconciliationInfo = GetFormValue();
					
					// Uploaded File should be a .txt file only
					if(!strFileName.EndsWith(".txt") && strFileName !="")
					{
						lblMessage.Text = "The Uploaded file can only be .txt for saving the record.";
						lblMessage.Visible=true;
						return;
					}
					
					objBankRconciliationInfo.CREATED_BY = int.Parse(GetUserId());
					objBankRconciliationInfo.CREATED_DATETIME = DateTime.Now;

					if(strRowId.ToUpper().Equals("NEW")) //save case
					{
						
						objBankRconciliationInfo.MODIFIED_BY = int.Parse(GetUserId());
						objBankRconciliationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
						//Calling the add method of business layer class
						intRetVal = objBankRconciliation.Add(objBankRconciliationInfo,strFileName);

						if(intRetVal>0)
						{
							hidAC_RECONCILIATION_ID.Value = objBankRconciliationInfo.AC_RECONCILIATION_ID.ToString();
							hidREF_FILE_ID.Value	= objBankRconciliationInfo.REF_FILE_ID.ToString();
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value			=	"1";
							hidOldData.Value = ClsBankRconciliation.GetXmlForPageControls(objBankRconciliationInfo.AC_RECONCILIATION_ID.ToString());
							EnableDisableImportButton(hidOldData.Value);
							GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
							if(txtUPLOAD_FILE.Value!="")
							UploadFile();

							if(ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value)!="" && ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value)!= null)
							{
								string []file = ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value).Split('_');
								string []fileExt = file[1].Split('.');
								string filename = hidRootPath.Value + file[0] + "_"+ ClsCommon.FetchValueFromXML("AC_RECONCILIATION_ID",hidOldData.Value)+ "." +fileExt[1];
								int startOfFile = filename.IndexOf("Upload");
								string filePath = filename.Substring(startOfFile + 6);
								string []fileURL = filePath.Split('.'); 
								string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
								hidfileLink.Value = EncryptedPath;
							}
						
						}
						else if(intRetVal == -2)//reconciliation pending for selected account
						{
							//Button Enabled By Raghav For Itrack issue #5270
							btnDelete.Enabled = false;
							btnCommit.Enabled = false;
							btnItemsToReconcile.Enabled = false;
							btnDistribute.Enabled = false;
							btnReProcessItemsToReconcile.Enabled = false;
							btnRptItemsToRecon.Enabled = false;  
                            btnRptOustandings.Enabled = false;
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"12");
							hidFormSaved.Value			=		"2";
							hidOldData.Value = ClsBankRconciliation.GetXmlForPageControls(objBankRconciliationInfo.AC_RECONCILIATION_ID.ToString());
							EnableDisableImportButton(hidOldData.Value);
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
						ClsBankRconciliationInfo objOldBankRconciliationInfo;
						objOldBankRconciliationInfo = new ClsBankRconciliationInfo();
			
						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldBankRconciliationInfo,hidOldData.Value);

						//Setting those values into the Model object which are not in the page
						objBankRconciliationInfo.AC_RECONCILIATION_ID = int.Parse(strRowId);
						objBankRconciliationInfo.MODIFIED_BY = int.Parse(GetUserId());
						objBankRconciliationInfo.LAST_UPDATED_DATETIME = DateTime.Now;

						//Rename File Name 
						string fileName = ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value);
						if(txtUPLOAD_FILE.PostedFile.FileName !="")
						{
							if(fileName == "")
							{
								//Set File Naming Conventions
								string[] fileArray;
								fileArray = strFileName.Split('.');
								strFileName = fileArray[0].ToString() + "_" + objBankRconciliationInfo.AC_RECONCILIATION_ID.ToString() + "." + fileArray[1].ToString();
							}
						}

						//Updating the record using business layer class object
						intRetVal	= objBankRconciliation.Update(objOldBankRconciliationInfo,objBankRconciliationInfo,strFileName);
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							hidOldData.Value = ClsBankRconciliation.GetXmlForPageControls(objBankRconciliationInfo.AC_RECONCILIATION_ID.ToString());
							EnableDisableImportButton(hidOldData.Value);
							GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
							if(txtUPLOAD_FILE.Value!="")
							UploadFile();

							if(ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value)!="" && ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value)!= null)
							{
								string []file = ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value).Split('_');
								string []fileExt = file[1].Split('.');
								string filename = hidRootPath.Value + file[0] + "_"+ ClsCommon.FetchValueFromXML("AC_RECONCILIATION_ID",hidOldData.Value)+ "." +fileExt[1];
								int startOfFile = filename.IndexOf("Upload");
								string filePath = filename.Substring(startOfFile + 6);
								string []fileURL = filePath.Split('.'); 
								string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
								hidfileLink.Value = EncryptedPath;
							}
						
						}
						else if(intRetVal == -2)//reconciliation pending for selected account
						{
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"13");
							hidFormSaved.Value			=		"2";
							hidOldData.Value = ClsBankRconciliation.GetXmlForPageControls(objBankRconciliationInfo.AC_RECONCILIATION_ID.ToString());
							EnableDisableImportButton(hidOldData.Value);
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
					if(objBankRconciliation!= null)
						objBankRconciliation.Dispose();
				}
			}

		private void EnableDisableImportButton(string xml)
		{
			if(xml!="")
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(xml);
				string RECON_CHECK_FILE = ClsCommon.GetNodeValue(doc,"//REF_FILE_ID_COUNT");
				if(RECON_CHECK_FILE!="0")
					btnImport.Enabled = false;
				else
					btnImport.Enabled = true;
			}
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
			{
				
				try
				{
					//				---------------
					objBankRconciliation = new  ClsBankRconciliation(true);

					//Creating the Model object for holding the Old data
					//ClsBankRconciliationInfo objOldBankReconciliationInfo;
					//objOldBankReconciliationInfo = new ClsBankRconciliationInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					//base.PopulateModelObject(objOldBankReconciliationInfo,hidOldData.Value);

					//ClsBankRconciliationInfo objBankReconciliationInfo;
                    ClsBankRconciliationInfo objBankReconciliationInfo = new ClsBankRconciliationInfo();
					base.PopulateModelObject(objBankReconciliationInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objBankReconciliationInfo.AC_RECONCILIATION_ID = int.Parse(hidAC_RECONCILIATION_ID.Value);
					objBankReconciliationInfo.MODIFIED_BY = int.Parse(GetUserId());
					objBankReconciliationInfo.COMMITTED_BY = int.Parse(GetUserId());
					objBankReconciliationInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					objBankReconciliationInfo.IS_COMMITED		=	"Y";
					objBankReconciliationInfo.DATE_COMMITED		=	DateTime.Now;
					
					int	intRetVal	= objBankRconciliation.Commit(objBankReconciliationInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
						hidFormSaved.Value		=	"1";
						GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
						hidOldData.Value = "";
						tblBody.Attributes.Add("style","display:none");
						
					}
					else if( intRetVal == -1 )			// Record not existed, hence exiting 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"8");
						hidFormSaved.Value		=	"1";
						GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
					}
					else if( intRetVal == -2 )			// Record already commited hence exiting
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"9");
						hidFormSaved.Value		=	"1";
						GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
					}
					else if( intRetVal == -3 )			//Invoice not distributed fully, therefore exiting
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"10");
						hidFormSaved.Value		=	"1";
						GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
					}
					else if( intRetVal == -4 )			//Invoice not distributed fully, therefore exiting
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"14");
						hidFormSaved.Value		=	"1";
						GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"7");
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
					if(objBankRconciliation!= null)
						objBankRconciliation.Dispose();
				}
				
			}	
			
		private void cmbACCOUNT_ID_SelectedIndexChanged(object sender, System.EventArgs e)
			{
//				string endingBalance = ClsBankRconciliation.GetPreviousEndingBalance(cmbACCOUNT_ID.SelectedValue);
//				if(endingBalance.Length>0)
//				{
//					txtSTARTING_BALANCE.Enabled=false;
//				}
//				else
//				{
//					txtSTARTING_BALANCE.Enabled=true;
//				}	
//				txtSTARTING_BALANCE.Text = endingBalance;
//				hidFormSaved.Value = "3";
//
//
//				SetFocus("cmbACCOUNT_ID");

				
			}//cmbACCOUNT_ID_SelectedIndexChanged
		private void btnDelete_Click(object sender, System.EventArgs e)
			{
				ClsBankRconciliationInfo objInfo = new ClsBankRconciliationInfo();
				base.PopulateModelObject(objInfo,hidOldData.Value);
				try
				{
					objInfo.AC_RECONCILIATION_ID = int.Parse(hidAC_RECONCILIATION_ID.Value);
					//Added FOR Itrack Issue #6677.
                    objInfo.CREATED_BY = int.Parse(GetUserId());  

					int intRetVal = new ClsBankRconciliation().Delete(objInfo);
					if( intRetVal > 0 )			// delete successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127");
						hidFormSaved.Value		=	"5";
							
						tblBody.Attributes.Add("style","display:none");
						//Delete File From Recon Folder Physicaly
						string fileName = ClsCommon.FetchValueFromXML("UPLOAD_FILE",hidOldData.Value);
						if(fileName!="")
                            DeleteFile(fileName); 

						hidOldData.Value		=	"";			
					}
					else 

					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"128");
						hidFormSaved.Value		=	"2";
					}

					lblMessage.Visible = true;
				}
				catch (Exception Ex)
				{
					lblMessage.Text = Ex.Message.ToString();
					lblMessage.Visible = true;
				}
			}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
				StreamReader objSR = null;
				try
				{
					
					// Get File Path /  File Name / File Length
					string strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
					string strDirName = Server.MapPath(strRoot);
					strDirName = strDirName + "\\BankReconFile";

					XmlDocument objXMLDoc = new XmlDocument();
					objXMLDoc.LoadXml(hidOldData.Value);
					strUploadedFileName = ClsCommon.GetNodeValue(objXMLDoc,"//UPLOAD_FILE");

					//Add Impersonation
					Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
					long FileLen = 0;
					//StreamReader objSR = null;
					

					//Ravindra(12-17-2008); Fetch Credentials from web.config before Impersonating 
					strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                    strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                    strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

					if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
					{
						FileInfo objFileInfo = new FileInfo(strDirName + "/" + strUploadedFileName);
						FileLen	=	objFileInfo.Length;

						// File StreamReader
						objSR	= new StreamReader(strDirName + "/" + strUploadedFileName);
						//ending the impersonation 
						objAttachment.endImpersonation();

						//Ravindra (12/17/2008): Parsing should be done only is system is able to read file 
						//from disk
						// Get ACCOUNT_ID
						int intAccID;
						intAccID = int.Parse(ClsCommon.GetNodeValue(objXMLDoc,"//ACCOUNT_ID"));

						string strFile;
						string[] arrFile = new string[FileLen];
						ClsBankRconciliationInfo objInfo = new ClsBankRconciliationInfo();
						ClsBankRconciliation objBL = new ClsBankRconciliation();

						int Failed = 0 ; 
						int Success = 0;
						int i = 1;
						int FileID = objBL.GetReconUploadedFileID(int.Parse(hidAC_RECONCILIATION_ID.Value));
						// Loop till all of the file records have been inserted into file details table
						while ((strFile = objSR.ReadLine()) != null && i<= FileLen) 
						{
							arrFile[i] = strFile;
							/* Current Record Format:  '#' represent Fillers, '-' represent No Space 
								* 
								##0000192503-0000209790-022107#0000001000-MATTHEW A FLITT-45059634##################
								F(2)   (10)      (10)    (8) F(1)  (10)        (15)       (8)       F(18)

								*/
							try
							{
								objInfo.ACCOUNT_NUMBER	=	arrFile[i].Substring(2,10).ToString();
								objInfo.ACCOUNT_ID		=	intAccID;
								objInfo.SERIAL_NUMBER	=	arrFile[i].Substring(12,10).ToString();
								/* * Passing additional param for check num / serial num
								 * with no leading 0's so as to match with the existing checks	 * */
								string strCheckNum		=	Convert.ToInt32(arrFile[i].Substring(12,10).ToString()).ToString();
								objInfo.CHECK_NUMBER	=	strCheckNum;

								string checkDate		=	arrFile[i].Substring(22,6);
								checkDate = checkDate.Insert(2,"/");
								checkDate = checkDate.Insert(5,"/");
								objInfo.CHECK_DATE				=	Convert.ToDateTime(checkDate);
								string strAmount				=   arrFile[i].Substring(29,10);
								int strAmountLen				=	strAmount.Length;
								strAmount						=	strAmount.Insert(strAmountLen - 2,".");
								objInfo.AMOUNT					=	Double.Parse(strAmount.ToString());
								objInfo.ADDITIONAL_DATA			=	arrFile[i].Substring(39,15).ToString();
								objInfo.SEQUENCE_NUMBER			=	int.Parse(arrFile[i].Substring(54,8));
								objInfo.RECON_GROUP_ID			=	int.Parse(hidAC_RECONCILIATION_ID.Value);
								objInfo.REF_FILE_ID				=	FileID;
								objInfo.MATCHED_RECORD_STATUS	=	0;
								objInfo.ERROR_DESC				=	"";
								objInfo.CREATED_BY				=	int.Parse(GetUserId());
								objInfo.CREATED_DATETIME		=	System.DateTime.Now;
								// Import Records
								ClsBankRconciliation.AddBankReconUploadFileDetails(objInfo);
								i++;
								Success++;
							}
							catch
							{
								Failed++;
//								lblMessage.Text = "The file imported is not in correct format.Please try again.";
//								lblMessage.Visible = true;
//								return;
							}
						}

						if(Failed == 0 && Success > 0) 
						{
							lblMessage.Text = "Records imported successfully.";
						}
						else if(Failed > 0 && Success == 0) 
						{
							lblMessage.Text = "The file imported is not in correct format.Please try again.";
						}
						else 
						{
							lblMessage.Text = "The file imported is not in correct format. " + Failed + " record(s) failed " + " and " + Success + " imported sucessfully. ";
						}


						lblMessage.Visible = true;
						btnImport.Enabled=false;
					}
					else
					{
						//Impersation failed
						lblMessage.Text += "\n Unable to Read the file. User impersonation failed.";
					}

					//Ravindra (12/17/2008): Parsing should be done only is system is able to read file 
					//from disk
//					// Get ACCOUNT_ID
//					int intAccID;
//					intAccID = int.Parse(ClsCommon.GetNodeValue(objXMLDoc,"//ACCOUNT_ID"));
//
//					string strFile;
//					string[] arrFile = new string[FileLen];
//					ClsBankRconciliationInfo objInfo = new ClsBankRconciliationInfo();
//					ClsBankRconciliation objBL = new ClsBankRconciliation();
//
//					int i = 1;
//					int FileID = objBL.GetReconUploadedFileID(int.Parse(hidAC_RECONCILIATION_ID.Value));
//					// Loop till all of the file records have been inserted into file details table
//					while ((strFile = objSR.ReadLine()) != null && i<= FileLen) 
//					{
//						arrFile[i] = strFile;
//						/* Current Record Format:  '#' represent Fillers, '-' represent No Space 
//							* 
//							##0000192503-0000209790-022107#0000001000-MATTHEW A FLITT-45059634##################
//							F(2)   (10)      (10)    (8) F(1)  (10)        (15)       (8)       F(18)
//
//							*/
//						try
//						{
//							objInfo.ACCOUNT_NUMBER	=	arrFile[i].Substring(2,10).ToString();
//							objInfo.ACCOUNT_ID		=	intAccID;
//							objInfo.SERIAL_NUMBER	=	arrFile[i].Substring(12,10).ToString();
//							/* * Passing additional param for check num / serial num
//							 * with no leading 0's so as to match with the existing checks	 * */
//							string strCheckNum		=	Convert.ToInt32(arrFile[i].Substring(12,10).ToString()).ToString();
//							objInfo.CHECK_NUMBER	=	strCheckNum;
//
//							string checkDate		=	arrFile[i].Substring(22,6);
//							checkDate = checkDate.Insert(2,"/");
//							checkDate = checkDate.Insert(5,"/");
//							objInfo.CHECK_DATE				=	Convert.ToDateTime(checkDate);
//							string strAmount				=   arrFile[i].Substring(29,10);
//							int strAmountLen				=	strAmount.Length;
//							strAmount						=	strAmount.Insert(strAmountLen - 2,".");
//							objInfo.AMOUNT					=	Double.Parse(strAmount.ToString());
//							objInfo.ADDITIONAL_DATA			=	arrFile[i].Substring(39,15).ToString();
//							objInfo.SEQUENCE_NUMBER			=	int.Parse(arrFile[i].Substring(54,8));
//							objInfo.RECON_GROUP_ID			=	int.Parse(hidAC_RECONCILIATION_ID.Value);
//							objInfo.REF_FILE_ID				=	FileID;
//							objInfo.MATCHED_RECORD_STATUS	=	0;
//							objInfo.ERROR_DESC				=	"";
//							objInfo.CREATED_BY				=	int.Parse(GetUserId());
//							objInfo.CREATED_DATETIME		=	System.DateTime.Now;
//							// Import Records
//							ClsBankRconciliation.AddBankReconUploadFileDetails(objInfo);
//							i++;
//						}
//						catch
//						{
//							lblMessage.Text = "The file imported is not in correct format.Please try again.";
//							lblMessage.Visible = true;
//							return;
//						}
//					}
//					lblMessage.Text = "Records imported successfully.";
//					lblMessage.Visible = true;
//					btnImport.Enabled=false;
				}
				catch(Exception objEx)
				{
					Exception customEx = new Exception("Error Importing Bank Reconciliation file. ",objEx);
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(customEx);

					lblMessage.Text = "File could not be Read as the " + objEx.Message.ToString();
					lblMessage.Visible=true;
				}
				finally
				{
					//Ravindra(12/17/2008): Close only if stream is created.
					if(objSR != null)
					{
						objSR.Close();					
					}
				}
		}
		private void btnReProcessItemsToReconcile_Click(object sender, System.EventArgs e)
		{
			try
			{				
				objBankRconciliation = new  ClsBankRconciliation(true);
				//ClsBankRconciliationInfo objBankReconciliationInfo;
				
				int accReconId		=	int.Parse(hidAC_RECONCILIATION_ID.Value);
				int createdBy		=	int.Parse(GetUserId());
				int accountId		=	int.Parse(cmbACCOUNT_ID.SelectedValue);
													
				int	intRetVal	= objBankRconciliation.ReProcessBankReconDetails(accReconId,accountId,createdBy);
				if( intRetVal > 0 )			// Reprocessed successfully 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"15");
					GetReconciliationStatus(hidAC_RECONCILIATION_ID.Value);
									
				}
				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"16");
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
				if(objBankRconciliation!= null)
					objBankRconciliation.Dispose();
			}

		}
			#endregion

			#region Set Captions / Get Reconciliation Status
			private void SetCaptions()
			{
				capACCOUNT_ID.Text						=		objResourceMgr.GetString("cmbACCOUNT_ID");
				capGL_NAME.Text							=		objResourceMgr.GetString("txtGL_Name");
				capSTATEMENT_DATE.Text					=		objResourceMgr.GetString("txtSTATEMENT_DATE");
				capSTARTING_BALANCE.Text				=		objResourceMgr.GetString("txtSTARTING_BALANCE");
				capENDING_BALANCE.Text					=		objResourceMgr.GetString("txtENDING_BALANCE");
				capBANK_CHARGES_CREDITS.Text			=		objResourceMgr.GetString("txtBANK_CHARGES_CREDITS").Replace("@amp;","&");
			}

			/// <summary>
			/// N: Not Distributed
			/// D:Distributed 
			/// A:approved it is implicit that invoice is distributed.
			/// C:committed , it is implicit that invoice is distributed and approved.
			/// </summary>
			/// <param name="CHECK_ID"></param>
			/// N-Not distributed, 
			/// D-Distributed, 
			/// C-Committed
		private void GetReconciliationStatus(string AC_RECONCILIATION_ID)
		{
			string status = ClsBankRconciliation.GetReconciliationStatus(AC_RECONCILIATION_ID);
			switch(status)
			{
				case "N":
					btnDistribute.Enabled	=	true;
					btnDelete.Enabled		=   true;
					btnCommit.Enabled		=	false;
					break;								
				case "D":
					btnDistribute.Enabled	=	false;
					btnDelete.Enabled		=   true;
					btnCommit.Enabled		=	true;
					break;
				case "C":
					btnDistribute.Enabled	=	false;
					btnDelete.Enabled		=   false;
					btnCommit.Enabled		=	false;
					break;
			}
			//changes by uday
		//	btnCommit.Enabled=true;
			//end
		}

		#endregion
	}
}
