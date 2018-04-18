/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	10/20/2005 12:33:04 PM
<End Date				: -	
<Description				: - 	This file is used to add / update attachment files for Endorsements
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - This file is used to
*******************************************************************************************/ 
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.IO;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This file is used to
	/// </summary>
	public class AddEndorsementAttachment : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlInputFile txtATTACH_FILE;
		#region Page controls declaration

		protected System.Web.UI.WebControls.TextBox txtVALID_DATE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		//protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVALID_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvATTACH_FILE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidfileLink;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capVALID_DATE;
		protected System.Web.UI.WebControls.Label lblVALID_DATE;
		
		protected System.Web.UI.WebControls.Label capATTACH_FILE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDORSEMENT_ATTACH_ID;
		protected System.Web.UI.WebControls.Label lblATTACH_FILE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidATTACH_FILE;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnFileName;
		//*********************
		protected System.Web.UI.WebControls.Label capEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.Label capEDITION_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.Label capDISABLED_DATE;
		protected System.Web.UI.WebControls.TextBox txtDISABLED_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkDISABLED_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDISABLED_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvDISABLED_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEDITION_DATE;
		protected System.Web.UI.WebControls.TextBox txtEDITION_DATE ;
		protected string strUserName ="";
		protected string strPassWd ="";
		protected string strDomain ="";
		public int flagFileCheck = 0;//Added for Itrack Issue 5906 on 8 June 09
		

		
		//********************
		//Defining the business layer class object
		ClsEndorsementAttachement  objEndorsementAttachement ;
		protected System.Web.UI.WebControls.HyperLink hlkVALID_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVALID_DATE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.CustomValidator csvVALID_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEDITION_DATE;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label capFORM_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtFORM_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDELETE;
		protected int Endrosement_id=0;
		protected System.Web.UI.WebControls.RegularExpressionValidator revATTACH_FILE;//Added for ItrackIssue 5553 on 9 march 2009
		protected System.Web.UI.WebControls.RegularExpressionValidator revATTACH_FILE_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revATTACH_FILE_PDF;//Added for ItrackIssue 5706 on 16 April 2009
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
			rfvVALID_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			csvVALID_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");			
			rfvATTACH_FILE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			revVALID_DATE.ValidationExpression  =  aRegExpDate;
			revVALID_DATE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");

			revEFFECTIVE_TO_DATE.ValidationExpression	= aRegExpDate;
			revEFFECTIVE_TO_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");			
			revDISABLED_DATE.ValidationExpression		= aRegExpDate;
			revDISABLED_DATE.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

			revEDITION_DATE.ValidationExpression		= aRegExpShortDate;
			revEDITION_DATE.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"17");

			csvEFFECTIVE_TO_DATE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"8");
			csvDISABLED_DATE.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"9");
			//Added for ItrackIssue 5553 on 9 march 2009
			revATTACH_FILE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");
			revATTACH_FILE_EXT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1048");
			revATTACH_FILE_PDF.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1049");//Added for Itrack Issue 5706 on 16 April 2009
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Sumit:15-02-2006:Save button disabled
			//btnSave.Enabled=false;
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			btnDelete.Attributes.Add("onclick","javascript:return deleteClick();");
			hlkVALID_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_ENDORSEMENT_ATTACHMENT.txtVALID_DATE, document.MNT_ENDORSEMENT_ATTACHMENT.txtVALID_DATE)");
			hlkEFFECTIVE_TO_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtEFFECTIVE_TO_DATE'), document.getElementById('txtEFFECTIVE_TO_DATE'))");
			hlkDISABLED_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtDISABLED_DATE'), document.getElementById('txtDISABLED_DATE'))");
			btnSave.Attributes.Add("onclick","javascript:RemoveSpecialChar(document.getElementById('txtATTACH_FILE').value,document.getElementById('revATTACH_FILE'));RemoveExecutableFiles(document.getElementById('txtATTACH_FILE').value,document.getElementById('revATTACH_FILE_EXT'));AllowPDFFiles(document.getElementById('txtATTACH_FILE').value,document.getElementById('revATTACH_FILE_PDF'));");//Added for ItrackIssue 5706 on 16 April 2009
			//txtVALID_DATE.Attributes.Add("onblur","javascript:ChangeDefaultDate();");
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="394_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Execute;
			btnReset.PermissionString		=	gstrSecurityXML;

//			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Execute;
//			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Execute;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddEndorsementAttachment" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["ENDORSEMENT_ID"]!=null && Request.QueryString["ENDORSEMENT_ID"].ToString().Length>0)
				{
					Endrosement_id = Convert.ToInt32(Request.QueryString["ENDORSEMENT_ID"].ToString());
				}
				if(Request.QueryString["ENDORSEMENT_ATTACH_ID"]!=null && Request.QueryString["ENDORSEMENT_ATTACH_ID"].ToString().Length>0)
				{
					hidENDORSEMENT_ATTACH_ID.Value =Request.QueryString["ENDORSEMENT_ATTACH_ID"].ToString();
					hidOldData.Value = ClsEndorsementAttachement.GetXmlForPageControls(Request.QueryString["ENDORSEMENT_ATTACH_ID"].ToString());
				}

				string lob_path = "";
				string state_code = "";
				DataSet objdataSet = null;
				if(Endrosement_id != 0)
				{
					objdataSet = ClsEndorsmentDetails.GetEndorsement(Endrosement_id.ToString());
				}

				if(Request.QueryString["LobDesc"] != null && Request.QueryString["LobDesc"].ToString() != "")
				{
					lob_path = Request.QueryString["LobDesc"].ToString();
					switch(lob_path.ToUpper())
					{
						case "HOMEOWNERS" : lob_path = "HOME"; break;
						case "UMBRELLA" : lob_path = "UMB"; break;
						case "AUTOMOBILE" : lob_path = "PPA"; break;
						case "MOTORCYCLE" : lob_path = "MOT"; break;
						case "WATERCRAFT" : lob_path = "WAT"; break;
						case "RENTAL" : lob_path = "RENT"; break;
						default: break;
					}
				}
				else if(objdataSet != null && objdataSet.Tables[0].Rows.Count > 0)
				{
					switch(objdataSet.Tables[0].Rows[0]["LOB_ID"].ToString())
					{
						case "1" : lob_path = "HOME"; break;
						case "5" : lob_path = "UMB"; break;
						case "2" : lob_path = "PPA"; break;
						case "3" : lob_path = "MOT"; break;
						case "4" : lob_path = "WAT"; break;
						case "6" : lob_path = "RENT"; break;
						default: break;
					}
				}
				if(Request.QueryString["STATE_CODE"] != null && Request.QueryString["STATE_CODE"].ToString() != "")
				{
					state_code = Request.QueryString["STATE_CODE"].ToString();
				}
				else if(objdataSet != null && objdataSet.Tables[0].Rows.Count > 0)
				{
					switch(objdataSet.Tables[0].Rows[0]["STATE_ID"].ToString())
					{
						case "14" : state_code = "IN"; break;
						case "22" : state_code = "MI"; break;
						case "49" : state_code = "WI"; break;
						default: break;
					}
				}
				if(lob_path == "" || lob_path == "General Liability")
				{
					lblMessage.Visible = true;
					lblMessage.Text="The Lob does not have a valid INPUTPDF path";
				}
				else
				{
					//hidRootPath.Value = Server.MapPath("/cms/cmsweb/INPUTPDFs/" + lob_path + "/MI/");
					//hidRootPath.Value = "/cms/cmsweb/INPUTPDFs/" + lob_path + "/"+ state_code + "/";
					hidRootPath.Value = "/cms/Upload/OUTPUTPDFs/EndorsementAttachment/" + lob_path + "/"+ state_code + "/";
					//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/Endorsement/" + hidENDORSEMENT_ATTACH_ID.Value.ToString() ;
				}
				hidRootPath.Value = "/cms/Upload/OUTPUTPDFs/EndorsementAttachment/" + lob_path + "/"+ state_code + "/";
				GetOldDataXML();
				SetCaptions();
				#region "Loading singleton"
				#endregion//Loading singleton
			}
		}//end pageload
		#endregion
		/// <summary>
		/// validate posted data from form
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool doValidationCheck()
		{
			try
			{
				return true;
			}
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return false;
			}
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsEndorsementAttachmentInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsEndorsementAttachmentInfo objEndorsementAttachmentInfo;
			objEndorsementAttachmentInfo = new ClsEndorsementAttachmentInfo();

			//objEndorsementAttachmentInfo.ATTACH_FILE=	txtATTACH_FILE.Text;
			objEndorsementAttachmentInfo.ENDORSEMENT_ID = Endrosement_id;
			objEndorsementAttachmentInfo.VALID_DATE=	Convert.ToDateTime(txtVALID_DATE.Text);
			string strFileName = txtATTACH_FILE.PostedFile.FileName.Substring(txtATTACH_FILE.PostedFile.FileName.LastIndexOf("\\") + 1);
			objEndorsementAttachmentInfo.ATTACH_FILE = strFileName;
			objEndorsementAttachmentInfo.ENDORSEMENT_ID = int.Parse(Request.Params["ENDORSEMENT_ID"].ToString());

			if(txtEFFECTIVE_TO_DATE.Text.Trim() != "")
				objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE=Convert.ToDateTime(txtEFFECTIVE_TO_DATE.Text);
//			else
//				objEndorsementAttachmentInfo.EFFECTIVE_TO_DATE=Convert.ToDateTime(null);

			if(txtDISABLED_DATE.Text.Trim() != "")
				objEndorsementAttachmentInfo.DISABLED_DATE=Convert.ToDateTime(txtDISABLED_DATE.Text);
//			else
//			    objEndorsementAttachmentInfo.DISABLED_DATE=Convert.ToDateTime(null);

			if(txtEDITION_DATE.Text.Trim() != "")
				objEndorsementAttachmentInfo.EDITION_DATE=txtEDITION_DATE.Text;
//			else 
//				objEndorsementAttachmentInfo.EDITION_DATE=Convert.ToDateTime(null);

			objEndorsementAttachmentInfo.FORM_NUMBER=txtFORM_NUMBER.Text;
//			objEndorsementAttachmentInfo.FORM_NUMBER="";

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidENDORSEMENT_ATTACH_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object
			//=========  File Upload ===========START
			
			strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
            strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
            strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");


			return objEndorsementAttachmentInfo;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
				string strUerName, strPassWd, strDomain;
				strUerName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");
				int intRetVal;	//For retreiving the return value of business class save function
				objEndorsementAttachement = new  ClsEndorsementAttachement();

				//Retreiving the form values into model class object
				ClsEndorsementAttachmentInfo objEndorsementAttachmentInfo = GetFormValue();
				if(objEndorsementAttachmentInfo.ATTACH_FILE == "")
					objEndorsementAttachmentInfo.ATTACH_FILE = hidATTACH_FILE.Value;
				if(!objEndorsementAttachmentInfo.ATTACH_FILE.EndsWith(".pdf"))
				{
					lblMessage.Visible = true;
					lblMessage.Text="The Attachment file can only be .pdf for saving the record";
					//Response.Write("<script language='javascript'> alert('The Attachment file can only be .pdf for saving the record');</script>");
					return; 
				}

				if(strRowId.ToUpper().Equals("NEW") || strRowId=="0" ) //save case
				{
					objEndorsementAttachmentInfo.CREATED_BY = int.Parse(GetUserId());
					objEndorsementAttachmentInfo.CREATED_DATETIME = DateTime.Now;

					string InvalidAttach = "";
					//Calling the add method of business layer class
					intRetVal = objEndorsementAttachement.Add(objEndorsementAttachmentInfo, ref InvalidAttach);

					if(InvalidAttach != "")
					{
						lblMessage.Text += "Please enter End Date or Disabled Date for previous attachment '" + InvalidAttach + "'";
					}
					else if(intRetVal>0)
					{
						hidENDORSEMENT_ATTACH_ID.Value = objEndorsementAttachmentInfo.ENDORSEMENT_ATTACH_ID.ToString();
						//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/Endorsement/" + hidENDORSEMENT_ATTACH_ID.Value.ToString() ;
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value = ClsEndorsementAttachement.GetXmlForPageControls(hidENDORSEMENT_ATTACH_ID.Value);
						hidIS_ACTIVE.Value = "Y";
						//Beginigng the imporsonation 
						ClsAttachment objAttachment = new ClsAttachment();
						if (objAttachment.ImpersonateUser(strUerName, strPassWd, strDomain))
						{
							if ( SaveUploadedFile(txtATTACH_FILE,Convert.ToInt32(hidENDORSEMENT_ATTACH_ID.Value)) == false)
							{
								//Some error occured while uploading 
								lblMessage.Text += "\n Unable to upload the file.";
							}
							//ending the imporsonation 
							objAttachment.endImpersonation();
						}
						else
						{
							//Imporsation failed
							lblMessage.Text += "\n Unable to upload the file. User imporsonation failed.";
						}
						GetOldDataXML();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"11");
						hidFormSaved.Value			=		"2";
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
					ClsEndorsementAttachmentInfo objOldEndorsementAttachmentInfo;
					objOldEndorsementAttachmentInfo = new ClsEndorsementAttachmentInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldEndorsementAttachmentInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objEndorsementAttachmentInfo.ENDORSEMENT_ATTACH_ID = int.Parse(strRowId);
					objEndorsementAttachmentInfo.MODIFIED_BY = int.Parse(GetUserId());
					objEndorsementAttachmentInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objEndorsementAttachement.Update(objOldEndorsementAttachmentInfo,objEndorsementAttachmentInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value = ClsEndorsementAttachement.GetXmlForPageControls(strRowId);
						//Beginigng the imporsonation 
						ClsAttachment objAttachment = new ClsAttachment();
						if (objAttachment.ImpersonateUser(strUerName, strPassWd, strDomain))
						{
							if ((txtATTACH_FILE.Value.Trim() != "") && (SaveUploadedFile(txtATTACH_FILE,Convert.ToInt32(hidENDORSEMENT_ATTACH_ID.Value)) == false))
							{
								//Some error occured while uploading 
								lblMessage.Text += "\n Unable to upload the file.";
							}
							//ending the imporsonation 
							objAttachment.endImpersonation();
						}
						else
						{
							//Imporsation failed
							lblMessage.Text += "\n Unable to upload the file. User imporsonation failed.";
						}
						GetOldDataXML();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
						hidFormSaved.Value		=	"1";
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
				if(objEndorsementAttachement!= null)
					objEndorsementAttachement.Dispose();
			}
		}

//		/// <summary>
//		/// Activates and deactivates  .
//		/// </summary>
//		/// <param name="sender"></param>
//		/// <param name="e"></param>
//		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
//		{
//			try
//			{
//				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
//				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
//				objStuTransactionInfo.loggedInUserName = GetUserName();
//				objEndorsementAttachement =  new ClsEndorsementAttachement();
//
//				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
//				{
//					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
//					objEndorsementAttachement.TransactionInfoParams = objStuTransactionInfo;
//					objEndorsementAttachement.ActivateDeactivate(hidENDORSEMENT_ATTACH_ID.Value,"N");
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
//					hidIS_ACTIVE.Value="N";
//				}
//				else
//				{
//					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
//					objEndorsementAttachement.TransactionInfoParams = objStuTransactionInfo;
//					objEndorsementAttachement.ActivateDeactivate(hidENDORSEMENT_ATTACH_ID.Value,"Y");
//					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
//					hidIS_ACTIVE.Value="Y";
//				}
//				hidFormSaved.Value			=	"1";
//			}
//			catch(Exception ex)
//			{
//				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
//				lblMessage.Visible	=	true;
//				ExceptionManager.Publish(ex);
//			}
//			finally
//			{
//				lblMessage.Visible = true;
//				if(objEndorsementAttachement!= null)
//					objEndorsementAttachement.Dispose();
//			}
//		}
		#endregion
		private void SetCaptions()
		{
			capATTACH_FILE.Text						=		objResourceMgr.GetString("txtATTACH_FILE");
			capVALID_DATE.Text						=		objResourceMgr.GetString("txtVALID_DATE");
			//capEFFECTIVE_FROM_DATE.Text           =		objResourceMgr.GetString("txtEFFECTIVE_FROM_DATE");
			capEFFECTIVE_TO_DATE.Text				=		objResourceMgr.GetString("txtEFFECTIVE_TO_DATE");
			capDISABLED_DATE.Text					=		objResourceMgr.GetString("txtDISABLED_DATE");
//			capFORM_NUMBER.Text						=		objResourceMgr.GetString("txtFORM_NUMBER");
			capEDITION_DATE.Text					=		objResourceMgr.GetString("txtEDITION_DATE");
		}
		private void GetOldDataXML()
		{
			if ( Request.Params.Count != 0 ) 
			{
			}
			else 
			{
			}
			if(hidOldData.Value!="")
			{
				if(ClsCommon.FetchValueFromXML("ENDORSEMENT_ID",hidOldData.Value)!="" && ClsCommon.FetchValueFromXML("ENDORSEMENT_ID",hidOldData.Value)!=null)
				{
					string filename =  hidRootPath.Value + ClsCommon.FetchValueFromXML("ATTACH_FILE",hidOldData.Value);											   
					if(filename!="") //File Exists or not
					{
						int startOfFile = filename.IndexOf("Upload");
						string filePath = filename.Substring(startOfFile + 6);
						string []fileURL = filePath.Split('.'); 
						string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
						hidfileLink.Value = EncryptedPath;
						revATTACH_FILE_EXT.Enabled=false;
						revATTACH_FILE_PDF.Enabled=false;
						flagFileCheck =1;//Added for Itrack Issue 5906 on 8 June 09
					}
					else //Added for Itrack Issue 5906 on 8 June 09
					  flagFileCheck = 0;
				}
			}
		}

		/// <summary>
		/// Creates the directory structure, where the file will be saved
		/// </summary>
		private string CreateDirStructure()
		{

            string strDirName = "";//strRoot,
			try
			{
				string strAgencyCode = GetSystemId();  //Login agency name, will come from session
				
//				strRoot = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL");
//			
				strDirName = Server.MapPath(hidRootPath.Value);
				
				//Creating the Endoresment folder if not exists
				//strDirName = strDirName + "\\Endorsement";
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

		public string GetExistingPdf(string strpath, string strnameBegin)
		{
			try
			{
				string FilePath = strpath + "temp"; //Server.MapPath(strpath+"temp");

				FileInfo finfo = new FileInfo(FilePath);

				DirectoryInfo dinfo = finfo.Directory;

				FileSystemInfo[] fsinfo = dinfo.GetFiles();

				foreach (FileSystemInfo info in fsinfo)
				{
					if(info.Name == strnameBegin)
					{
						return info.Name;
					}
				}
				return "";
			}
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return "";
			}
		}

		/// <summary>
		/// This function is used to save the uploaded file in harddisk
		/// </summary>
		private bool SaveUploadedFile(HtmlInputFile objFile,int intAttachmentId)
		{
			try
			{
				//Stores the name of the directory where file will get stored
				string strDirName;
				strDirName = CreateDirStructure	();

				
				//Retreiving the extension
				string strFileName;
				int Index = objFile.PostedFile.FileName.LastIndexOf("\\");
				if (Index >=0 )
				{
					strFileName = objFile.PostedFile.FileName.Substring(Index+1);
				}
				else
				{
					strFileName = objFile.PostedFile.FileName;
				}
				//Added by Mohit Agarwal 9-Aug-07 ITrack 2302
				string existingpdf = "";
				Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
				if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
				{

					existingpdf = GetExistingPdf(strDirName, strFileName);

					if(existingpdf != "")
					{
						existingpdf += "." + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
						System.IO.File.Copy(strDirName + strFileName, strDirName + existingpdf, false);
						lblMessage.Text += "\n File already exists so previous file renamed to: " + existingpdf;
					}
					//copying the file
					objFile.PostedFile.SaveAs(strDirName + strFileName);
					//ending the impersonation 
					objAttachment.endImpersonation();
				}
				else
				{
					//Impersation failed
					lblMessage.Text += "\n Unable to Upload the file. User impersonation failed.";
					return false;

				}
				return true;
			}
			catch (Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				return false;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				objEndorsementAttachement = new  ClsEndorsementAttachement();
				int attach_id = int.Parse(hidENDORSEMENT_ATTACH_ID.Value);
				int userId = int.Parse(GetUserId());
		
				objEndorsementAttachement.Delete(attach_id,userId);

				lblMessage.Text = "Endorsement deleted successfully";

			}
			catch (Exception ex)
			{
				lblMessage.Text = "Endorsement could not be deleted";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			lblMessage.Visible = true;
		}
	}
}
