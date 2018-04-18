/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	09/06/2005 11:53 AM
<End Date				: -	12/06/2005 06:00 PM
<Description			: - Code behind class of Journal Entry Master page.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Used for addition or modification of all types of journal entries.

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
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Account;
using Cms.CmsWeb.Controls;


namespace Cms.Account.Aspx
{
	/// <summary>
	/// Code behind class of Journal Entry Master page.
	/// </summary>
	public class AddJournalEntryMaster : Cms.Account.AccountBase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capTRANS_DATE;
		protected System.Web.UI.WebControls.TextBox txtTRANS_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANS_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTRANS_DATE;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.Label capJOURNAL_ENTRY_NO;
		protected System.Web.UI.WebControls.Label capDIV_ID;
		protected System.Web.UI.WebControls.DropDownList cmbDIV_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_ID;
		protected System.Web.UI.WebControls.Label capGL_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGL_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidJOURNAL_ID;
		protected System.Web.UI.WebControls.CustomValidator csvTRANS_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTRANS_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnNext;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.DropDownList cmbFISCAL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGROUP_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbFREQUENCY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFREQUENCY;
		protected System.Web.UI.WebControls.TextBox txtSTART_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkSTART_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTART_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTART_DATE;
		protected System.Web.UI.WebControls.TextBox txtEND_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEND_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEND_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEND_DATE;
		protected System.Web.UI.WebControls.Label capDAY_OF_THE_WK;
		protected System.Web.UI.WebControls.DropDownList cmbDAY_OF_THE_WK;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDAY_OF_THE_WK;
		protected System.Web.UI.WebControls.Label capFREQUENCY;
		protected System.Web.UI.WebControls.Label capEND_DATE;
		protected System.Web.UI.WebControls.Label capLAST_PROCESSED_DATE;
		protected System.Web.UI.WebControls.Label capLAST_TRANSACTION_DATE;
		protected System.Web.UI.WebControls.Label capSTART_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvSTART_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvEND_DATE;
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRecurrMsg;
		protected System.Web.UI.WebControls.TextBox txtJOURNAL_ENTRY_NO;
		#endregion

		#region Local form variables
		System.Resources.ResourceManager objResourceMgr;							//creating resource manager object
		Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster objJournalEntryMaster;
		protected System.Web.UI.WebControls.CompareValidator cpvEND_DATE;
		protected System.Web.UI.WebControls.Label  JOURNAL_ENTRY_NO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGenLedgerXML;
		protected System.Web.UI.WebControls.TextBox  txtDESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEndFiscalDate;
		protected Cms.CmsWeb.Controls.CmsButton btnRecurr;
		protected string dtLastPostDate;
		protected System.Web.UI.WebControls.Label lblLAST_PROCESSED_DATE;
		protected System.Web.UI.WebControls.Label lblLAST_TRANSACTION_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyTemplate;

		//Defining the business layer class object
		private string strRowId;
		int intTmpVar;
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			//Setting the error message 
			rfvTRANS_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvDIV_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvGL_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			csvTRANS_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"405");
			csvSTART_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("G","1001");
			csvEND_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("G","1002");
			
			rfvDAY_OF_THE_WK.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvEND_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			rfvFREQUENCY.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvSTART_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			
			revTRANS_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("175_0_0","23");
			revEND_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("175_0_0","23");
			revSTART_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("175_0_0","23");
			cpvEND_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"733");

			this.csvDESCRIPTION.ErrorMessage =		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("441");
			
			//Setting the validation expressions
			revTRANS_DATE.ValidationExpression	=	aRegExpDate;
			revEND_DATE.ValidationExpression	=	aRegExpDate;
			revSTART_DATE.ValidationExpression	=	aRegExpDate;
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{

			#region Setting the controls attribues
			//Setting the attributes of controls
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			btnNext.Attributes.Add("OnClick","javascript:return OnNextClick();");
			btnDelete.Attributes.Add("OnClick","javascript:return OnDeleteClick();");
			hlkTRANS_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_JOURNAL_MASTER.txtTRANS_DATE,document.ACT_JOURNAL_MASTER.txtTRANS_DATE)");
			hlkSTART_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_JOURNAL_MASTER.txtSTART_DATE,document.ACT_JOURNAL_MASTER.txtSTART_DATE)");
			hlkEND_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_JOURNAL_MASTER.txtEND_DATE,document.ACT_JOURNAL_MASTER.txtEND_DATE)");
			cmbFREQUENCY.Attributes.Add("OnChange","javascript:return cmbFREQUENCY_Change();");
			btnCopyTemplate.Attributes.Add("onClick","javascript:CopyRecords();return false;");

			
			#endregion

			//Setting the screen id
			SetScreenId();
			
			lblMessage.Visible = false;
			
			SetErrorMessages();		//Seeting the property of validation controls

			#region Setting the properties of CmsButton 
			//START:** Setting permissions and class (Read/write/execute/delete) of Cmsbutton**********
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;

			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;

			btnCopy.CmsButtonClass		= CmsButtonType.Write;
			btnCopy.PermissionString	= gstrSecurityXML;

			btnNext.CmsButtonClass		= CmsButtonType.Write;
			btnNext.PermissionString	= gstrSecurityXML;

			btnDelete.CmsButtonClass	= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			btnCommit.CmsButtonClass	= CmsButtonType.Delete;
			btnCommit.PermissionString	= gstrSecurityXML;
			
			btnActivateDeactivate.CmsButtonClass	= CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;

			btnRecurr.CmsButtonClass	= CmsButtonType.Execute;
			btnRecurr.PermissionString  = gstrSecurityXML;

			btnCopyTemplate.CmsButtonClass		= CmsButtonType.Read;
			btnCopyTemplate.PermissionString	= gstrSecurityXML;


			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			#endregion

			//Making resource manager object for reading the resource file
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddJournalEntryMaster" ,System.Reflection.Assembly.GetExecutingAssembly());

			#region function to be called in Postback 
			if(!Page.IsPostBack)
			{
				
				//Retreiving the query string from url into hidden controls
				GetQueryString();
				if(hidGROUP_TYPE.Value.ToUpper().ToString()=="RC")
				{
					btnActivateDeactivate.Visible=true;
					btnCopy.Visible = false;
					btnCommit.Visible = false;
					btnRecurr.Visible = true;
					trRecurrMsg.Visible = true;
				}
				else
				{
					btnActivateDeactivate.Visible=false;
					btnCopy.Visible = true;
					btnCommit.Visible = true;
					btnRecurr.Visible = false;
					trRecurrMsg.Visible = false;
				}
				
				//Getting the maximum journal entry number, which is to be shown by default
				if(hidJOURNAL_ID.Value.ToString() =="" || hidJOURNAL_ID.Value == null || hidJOURNAL_ID.Value == "0")		
				{
					txtJOURNAL_ENTRY_NO.Text = ClsJournalEntryMaster.GetMaxEntryNo().ToString();					
				}
								
				//Filling the combos
				FillCombos();

				SetDefaultValues();

				//Getting the old data in the form of xml, to be used for maintening the transaction log
				GetOldDataXML();
				

				//Enabling the diabling different controls based on some criteria
				EnableDisableControls();

				//Setting the caption so labels
				SetCaptions();
			}
			#endregion

			DisplayCommit();
		
		}//end pageload
		#endregion

		
		private void SetDefaultValues()
		{
			try
			{
				DataSet ds = Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgersEOYRecords();
				hidGenLedgerXML.Value = ds.GetXml();
				if(hidGROUP_TYPE.Value.ToString().ToUpper() == "13")
				{
					hlkTRANS_DATE.Attributes.Add("style","display:none");	
					if(ds!= null && ds.Tables[0].Rows.Count>0 )
					{
						if (ds.Tables[0].Rows[0]["FISCAL_END_DATE"] != DBNull.Value)
						{
							//txtTRANS_DATE.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FISCAL_END_DATE"]).ToString("MM/dd/yyyy");
							txtTRANS_DATE.Attributes.Add("ReadOnly","True");
							cmbFISCAL_ID.Attributes.Add("onchange","javascript:SetTransDate();");
												
						}				
					}
				}
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsJournalEntryMasterInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsJournalEntryMasterInfo objJournalEntryMaster;
			objJournalEntryMaster = new ClsJournalEntryMasterInfo();

			//Populating the fields values of model class object from controls

			if (txtTRANS_DATE.Text.Trim() != "")
				objJournalEntryMaster.TRANS_DATE = ConvertToDate(txtTRANS_DATE.Text);

			objJournalEntryMaster.DESCRIPTION = txtDESCRIPTION.Text;

			if(cmbDIV_ID.SelectedValue != null && cmbDIV_ID.SelectedValue .Trim() != "")
			{
				string strEntity = cmbDIV_ID.SelectedValue;
				objJournalEntryMaster.DIV_ID = int.Parse(strEntity.Substring(0,strEntity.IndexOf("_")));
				objJournalEntryMaster.DEPT_ID = int.Parse(strEntity.Substring(strEntity.IndexOf("_") + 1, strEntity.Length-(strEntity.LastIndexOf("_") + 1)));
				objJournalEntryMaster.PC_ID = int.Parse(strEntity.Substring(strEntity.LastIndexOf("_")+1));
			}

			if (cmbFISCAL_ID.SelectedValue != null && cmbFISCAL_ID.SelectedValue.Trim() != "")
				objJournalEntryMaster.FISCAL_ID = int.Parse(cmbFISCAL_ID.SelectedValue);

			//objJournalEntryMaster.JOURNAL_ENTRY_NO = lblJOURNAL_ENTRY_NO.Text;
				objJournalEntryMaster.JOURNAL_ENTRY_NO = txtJOURNAL_ENTRY_NO.Text;
			objJournalEntryMaster.JOURNAL_GROUP_TYPE = hidGROUP_TYPE.Value;

			if (hidGROUP_TYPE.Value.ToUpper() == "RC")	//If group type is recurring 
			{
				//Popualting the fields ralated to recurring journal entries
				objJournalEntryMaster.FREQUENCY = cmbFREQUENCY.SelectedValue;

				if (txtSTART_DATE.Text != "")
					objJournalEntryMaster.START_DATE = ConvertToDate(txtSTART_DATE.Text);

				if (txtEND_DATE.Text != "")
					objJournalEntryMaster.END_DATE = ConvertToDate(txtEND_DATE.Text);
                //added by uday to remove the day of week if frequency is not weekly
             //   if (cmbFREQUENCY.SelectedValue.ToString() == "1")
				//
				objJournalEntryMaster.DAY_OF_THE_WK = cmbDAY_OF_THE_WK.SelectedValue;
				
			}
			
			if(txtJOURNAL_ENTRY_NO.Text != "")
				objJournalEntryMaster.TEMP_JE_NUM = int.Parse(txtJOURNAL_ENTRY_NO.Text);
			


			//These  assignments are common to all pages.
			strRowId		=	hidJOURNAL_ID.Value;
			
			//Returning the model object
			return objJournalEntryMaster;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			this.btnCopyTemplate.Click += new System.EventHandler(this.btnCopyTemplate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnRecurr.Click += new System.EventHandler(this.btnRecurr_Click);
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
				objJournalEntryMaster = new ClsJournalEntryMaster();

				//Retreiving the form values into model class object
				ClsJournalEntryMasterInfo objJournalEntryMasterInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{

					//Filling the CREATED_BY to current login user and CREATED_DATE to current date
					objJournalEntryMasterInfo.CREATED_BY = int.Parse(GetUserId());
					objJournalEntryMasterInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objJournalEntryMaster.Add(objJournalEntryMasterInfo);

					if(intRetVal>0)				//Saved successfully
					{
						hidJOURNAL_ID.Value	=	objJournalEntryMasterInfo.JOURNAL_ID.ToString();
						txtJOURNAL_ENTRY_NO.Text = objJournalEntryMasterInfo.JOURNAL_ENTRY_NO.ToString();
						intTmpVar				=  objJournalEntryMasterInfo.TEMP_JE_NUM;
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	=	"1";
						hidIS_ACTIVE.Value	=	"Y";
						btnActivateDeactivate.Text="Deactivate";
						GetOldDataXML();
						btnRecurr.Enabled = true;
					}
					else if(intRetVal == -1)	//Duplicate journal entry number error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value	=	"2";
					}
					else						//Some other error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value	=	"2";
					}
					if(intTmpVar == -1)
					{	
						lblMessage.Text = "JE # already existed. Hence, has been updated to the latest JE #. " + lblMessage.Text;	
					}
					lblMessage.Visible = true;
				}	// end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsJournalEntryMasterInfo objOldJournalEntryMasterInfo;
					objOldJournalEntryMasterInfo = new ClsJournalEntryMasterInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldJournalEntryMasterInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objJournalEntryMasterInfo.JOURNAL_ID = int.Parse(strRowId);
					objJournalEntryMasterInfo.MODIFIED_BY = int.Parse(GetUserId());
					objJournalEntryMasterInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objJournalEntryMasterInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objJournalEntryMaster.Update(objOldJournalEntryMasterInfo,objJournalEntryMasterInfo);
					if( intRetVal > 0 )				//update successfully performed
					{
						
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -2)		//Duplicate journal entry number error
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"5");
						hidFormSaved.Value		=	"2";
					}
					else if(intRetVal == -1)		//Duplicate code exist, update failed
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
				//Some exception occured in code, hence showing the exception error message
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				hidFormSaved.Value	=	"2";
				
				//Publishing the exception
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objJournalEntryMaster!= null)
					objJournalEntryMaster.Dispose();
			}
		}

		#endregion
		
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capTRANS_DATE.Text		= objResourceMgr.GetString("txtTRANS_DATE");
			//capJOURNAL_ENTRY_NO.Text= objResourceMgr.GetString("lblJOURNAL_ENTRY_NO");
			capJOURNAL_ENTRY_NO.Text= objResourceMgr.GetString("txtJOURNAL_ENTRY_NO");
			capDESCRIPTION.Text		= objResourceMgr.GetString("txtDESCRIPTION");
			capDIV_ID.Text			= objResourceMgr.GetString("cmbDIV_ID");
			capGL_ID.Text			= objResourceMgr.GetString("cmbFISCAL_ID");
			capFREQUENCY.Text		= objResourceMgr.GetString("cmbFREQUENCY");
			capSTART_DATE.Text		= objResourceMgr.GetString("txtSTART_DATE");
			capEND_DATE.Text		= objResourceMgr.GetString("txtEND_DATE");
			capDAY_OF_THE_WK.Text	= objResourceMgr.GetString("cmbDAY_OF_THE_WK");
			capLAST_PROCESSED_DATE.Text	= objResourceMgr.GetString("lblLAST_PROCESSED_DATE");
			capLAST_TRANSACTION_DATE.Text= objResourceMgr.GetString("lblLAST_TRANSACTION_DATE");

		}

		/// <summary>
		/// Retreive the information about selected record in the form of XML
		/// and saves it into hidden control
		/// </summary>
		private void GetOldDataXML()
		{
			if ( hidJOURNAL_ID.Value != "" ) 
			{
				//Retreiving the information of selected journal entry in the form of XML 				
				hidOldData.Value = ClsJournalEntryMaster.GetJournalEntryInfo(int.Parse(hidJOURNAL_ID.Value));

				if (hidOldData.Value != "")
				{
					//div, dep and pc id
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
					
					string divId = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//DIV_ID");
					string deptId = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//DEPT_ID");
					string pcId = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//PC_ID");

					txtJOURNAL_ENTRY_NO.Text = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//JOURNAL_ENTRY_NO");
					string strFISCAL_ID = "";
					strFISCAL_ID = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//FISCAL_ID");
					dtLastPostDate = Cms.BusinessLayer.BlCommon.ClsCommon.GetNodeValue(doc, "//LAST_VALID_POSTING_DATE");

					
					if (cmbFISCAL_ID.Items.FindByValue(strFISCAL_ID) != null)
						cmbFISCAL_ID.SelectedValue = strFISCAL_ID;
					

					if (cmbDIV_ID.Items.FindByValue(divId + "_" + deptId + "_" + pcId) != null)
						cmbDIV_ID.SelectedValue = divId + "_" + deptId + "_" + pcId;
				}

			}
			
		}
		
		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidJOURNAL_ID.Value = Request.Params["JOURNAL_ID"];
			hidGROUP_TYPE.Value = Request.Params["GROUP_TYPE"];
			if(Request.QueryString["CalledFrom"]!=null && Request.QueryString["CalledFrom"].ToString()!="")
				hidCalledFrom.Value = Request.QueryString["CalledFrom"].ToString().ToUpper();
			
		}

		/// <summary>
		/// Fills the various combo boxes in the page
		/// </summary>
		private void FillCombos()
		{
			try
			{
				objJournalEntryMaster =  new ClsJournalEntryMaster();
				Cms.BusinessLayer.BlCommon.ClsDivision.GetDefaultHierarchyDropDown(cmbDIV_ID);
				cmbDIV_ID.Items.Insert(0,new ListItem("",""));	
				if(hidGROUP_TYPE.Value.ToString().ToUpper() == "13")
					hidEndFiscalDate.Value = Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgersEOYIndropDown(cmbFISCAL_ID);
				else
					Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgersIndropDown(cmbFISCAL_ID);				
				cmbFISCAL_ID.Items.Insert(0,new ListItem("",""));	
				objJournalEntryMaster.FillFrequency(cmbFREQUENCY);
			}
			catch(Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		}

		/// <summary>
		/// Enable and disable ctrls, depending upon the existence of child controls
		/// </summary>
		private void EnableDisableControls()
		{
			try
			{
				objJournalEntryMaster = new ClsJournalEntryMaster();
				
				if  (hidJOURNAL_ID.Value != "")
				{
					if (objJournalEntryMaster.ChildRecordExists(int.Parse(hidJOURNAL_ID.Value)))
					{
						//child record exists hence
						//hence disabling accounting and dept drop down
						cmbDIV_ID.Enabled = false;
						cmbFISCAL_ID.Enabled = false;
					}
					btnRecurr.Enabled = true;
				}
				else
				{
                    btnRecurr.Enabled = false;
				}
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

		/// <summary>
		/// Setting the screen id
		/// </summary>
		private void SetScreenId()
		{
			try
			{
				string strType = Request.Params["GROUP_TYPE"].ToString();

				switch(strType.ToUpper())
				{
					case "ML":	//Manual journal entry
						base.ScreenId = "173_0_0";
						break;
					case "RC":	//Recurring journal entry
						base.ScreenId = "175_0_0";
						break;
					case "TP":	//Template journal entry
						base.ScreenId = "174_0_0";
						break;
					case "13":	//13th Period adjustment
						base.ScreenId = "176_0_0";
						break;
				}

			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
			}
		}

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			try
			{

				ClsJournalEntryMasterInfo objJournalEntryMasterInfo = GetFormValue();
				objJournalEntryMasterInfo.CREATED_BY = int.Parse(GetUserId().ToString());
				/*Copying the whole record*/
				objJournalEntryMaster = new ClsJournalEntryMaster();
				int intRetVal = objJournalEntryMaster.Copy(int.Parse(hidJOURNAL_ID.Value),objJournalEntryMasterInfo,txtJOURNAL_ENTRY_NO.Text);

				if (intRetVal>0)
				{
					//Copied successfully
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
					lblMessage.Visible = true;
					hidJOURNAL_ID.Value = intRetVal.ToString();
					hidFormSaved.Value = "5";

					//Generatin the Xml of old record
					GetOldDataXML();

				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
					lblMessage.Visible = true;
					hidFormSaved.Value = "5";
					//Generatin the Xml of old record
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

		private void btnCopyTemplate_Click(object sender, System.EventArgs e)
		{
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				/*Deleting the whole record*/
				objJournalEntryMaster = new ClsJournalEntryMaster();
				int intRetVal = objJournalEntryMaster.Delete(int.Parse(hidJOURNAL_ID.Value),int.Parse(GetUserId()),txtJOURNAL_ENTRY_NO.Text);

				if (intRetVal > 0)
				{
					//Deleted successfully
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "127");
					hidJOURNAL_ID.Value = "";
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					tblBody.Attributes.Add("style","display:none");
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



		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strJournalId;
				//Retreiving the journal id
				strJournalId = hidJOURNAL_ID.Value;

				Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster 
					objJournalEntryMaster = new Cms.BusinessLayer.BlAccount.ClsJournalEntryMaster();
				ClsJournalEntryMasterInfo objInfo = new ClsJournalEntryMasterInfo();
				base.PopulateModelObject(objInfo,ClsJournalEntryMaster.GetJournalEntryInfo(int.Parse(strJournalId)));
				objInfo.JOURNAL_ID = int.Parse(strJournalId);
				objInfo.MODIFIED_BY = int.Parse(GetUserId());
				

				int RetVal = objJournalEntryMaster.Commit(objInfo);

				switch(RetVal)
				{
					case 1:
						//JE Commited successfully
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");
						break;
					case -1:
						//JE not exists
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");
						break;
					case -2:
						//JE already commited
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
						break;
					case -3:
						//Proof is not equal to zero
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
						break;
					case -4:
						//Line items not exists
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
						break;
					case -5:
						//Day is less then lock date
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
						break;
					case -6:
						//Line item exists with amount = 0
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");
						break;
					case -7:
						//Line item exists without all details
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
						break;
					default:
						//Some other error occured
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");
						break;

				}

				lblMessage.Visible = true;

				//If committed successfully, then hiding the page and refreshing the index page
				if (RetVal == 1)
				{
					tblBody.Attributes.Add("style","display:none");

					hidOldData.Value = "";
					hidFormSaved.Value = "5";
					//Refreshing the grid, and removing the deposit details tab
					string JavascriptText = "<script>RefreshWebGrid('1','" + hidJOURNAL_ID.Value + "',false);RemoveTab(2,parent.parent);</script>";
                    ClientScript.RegisterStartupScript(this.GetType(),"RefreshGrid", JavascriptText);
				}

			}
			catch(Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}


		private void DisplayCommit()
		{
			try
			{
				ClsJournalEntryMaster objJournal = new ClsJournalEntryMaster();
				bool DispCommit = false;

				if (Request["JOURNAL_ID"] != null)
				{
					DispCommit = objJournal.HavingLineItems(int.Parse(Request["JOURNAL_ID"].ToString()));
				}
				if(hidGROUP_TYPE.Value.ToUpper().ToString() =="TP" || hidGROUP_TYPE.Value.ToUpper().ToString() == "RC")
					DispCommit=false;				
				btnCommit.Visible = DispCommit;
			}
			catch (Exception ex)
			{
				throw(ex);
			}
		}
	/// <summary>
	/// activate/deactivate the record.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{ 			
			//only in case of journal Entry recurring.. 
			ClsJournalEntryMaster objClsJournalEntryMaster =new ClsJournalEntryMaster();			
			
			try
			{
				//Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo();
				//journalentryno = Convert.ToInt32(ClsJournalEntryMaster.GetMaxEntryNo().ToString());
			//	journalentryno = 

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					
					objClsJournalEntryMaster.ActivateDeactivateJournal(hidJOURNAL_ID.Value,"N",txtJOURNAL_ENTRY_NO.Text);
					lblMessage.Text = ClsMessages.FetchGeneralMessage("41");
					btnActivateDeactivate.Text="Activate";
					hidIS_ACTIVE.Value="N";
				}
				else
				{					
					objClsJournalEntryMaster.ActivateDeactivateJournal(hidJOURNAL_ID.Value,"Y",txtJOURNAL_ENTRY_NO.Text);
					lblMessage.Text = ClsMessages.FetchGeneralMessage("40");
					btnActivateDeactivate.Text="Deactivate";
					hidIS_ACTIVE.Value="Y";
				}
				hidOldData.Value = ClsJournalEntryMaster.GetJournalEntryInfo(int.Parse(hidJOURNAL_ID.Value));
				hidFormSaved.Value			=	"1";
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objClsJournalEntryMaster!= null)
					objClsJournalEntryMaster.Dispose();
			}

		}

		private void btnRecurr_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsJournalEntryMaster objBL = new ClsJournalEntryMaster();
				ClsJournalEntryMasterInfo objInfo = GetFormValue();
				int JournalID = int.Parse(hidJOURNAL_ID.Value);
				int DayOfWeek = int.Parse(objInfo.DAY_OF_THE_WK);
				int Frequency = int.Parse(objInfo.FREQUENCY);
			//	int DayOfWeek = int.Parse(intobjJournalEntryMaster.DAY_OF_THE_WK);
			//	int Frequency = int.Parse(intobj
				DateTime StartDate = objInfo.START_DATE;
				DateTime EndDate = objInfo.END_DATE;
				DateTime dt = new DateTime();
				dt = Convert.ToDateTime(dtLastPostDate);
				DateTime LastValidPostingDate = dt;
				int UserID = int.Parse(GetUserId());
				int RetVal = objBL.PostRecurringJEs(JournalID,DayOfWeek,Frequency,StartDate,EndDate,LastValidPostingDate,UserID,txtJOURNAL_ENTRY_NO.Text);
				if(RetVal > 0 )
				{
					hidOldData.Value = "";
					hidFormSaved.Value = "5";
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"22");
					lblMessage.Visible = true;
					tblBody.Attributes.Add("style","display:none");
					trRecurrMsg.Visible = false;
				
				}
				else if(RetVal == -2)
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"20");
					lblMessage.Visible=true;
					hidFormSaved.Value = "2";
				}
				else
				{
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"21");
					lblMessage.Visible=true;
					hidFormSaved.Value = "2";
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
	}
}
