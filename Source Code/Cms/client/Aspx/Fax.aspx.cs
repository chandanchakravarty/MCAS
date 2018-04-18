using System;
using System.Data;
using System.Web;
using System.Xml;
using Cms.CmsWeb;
using System.Web.UI;
using System.Web.Mail;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlClient;
using Cms.CmsWeb.webcontrols;
using Cms.Model.Client;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for Email.
	/// </summary>
	public class Fax : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Label lblMessage1;
		protected System.Web.UI.WebControls.Label lblATTACHMENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTAX_STATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.TextBox txtFROM_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFROM_NAME;
		protected System.Web.UI.WebControls.Label capFROM_NAME;
		protected System.Web.UI.WebControls.Label capFROM_FAX;
		protected System.Web.UI.WebControls.TextBox txtFROM_FAX;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFROM_FAX;
		protected System.Web.UI.WebControls.Label capSubject;
		protected System.Web.UI.WebControls.TextBox txtSUBJECT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUBJECT;
		protected System.Web.UI.WebControls.TextBox txtTO;
		protected System.Web.UI.WebControls.Label capMESSAGE;
		protected System.Web.UI.WebControls.TextBox txtMESSAGE;
		protected System.Web.UI.WebControls.Label capATTACHMENT;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSend;
		protected System.Web.UI.WebControls.Label capErrMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMESSAGE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_FAX;
		protected System.Web.UI.HtmlControls.HtmlInputFile txtATTACHMENT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label capFROM_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtFROM_EMAIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFROM_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_EMAIL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;

		int		intLoggedInUserID;
		int		custID;

		protected System.Web.UI.WebControls.Label capTO_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTO_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTO_NUMBER;
		protected System.Web.UI.WebControls.Label capTO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTO;
		protected System.Web.UI.WebControls.TextBox txtTO_NUMBER; 

		protected System.Web.UI.WebControls.Label capDIARY_ITEM_REQ;
		protected System.Web.UI.WebControls.DropDownList cmbDIARY_ITEM_REQ;
		protected System.Web.UI.WebControls.Label capFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.TextBox txtFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator  revFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.CustomValidator  csvFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.Label capDIARY_ITEM_TO;
		protected System.Web.UI.WebControls.DropDownList cmbDIARY_ITEM_TO;
		public string strFileName="";
		public string strGUIDFileName=""; //Will be saved in Database.

		protected int intToUserID=0; // holds the value of touserid	

		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			intLoggedInUserID		=	int.Parse(base.GetUserId());
			Session.Add("userId",intLoggedInUserID); 
			if(GetCalledFor()!="CLAIM")
				base.ScreenId			=	"109_0"; //can be Alpha Numeric
			else
				base.ScreenId = "315_0";
			
			btnReset.Attributes.Add("onclick","javascript:document.forms[0].reset();return false;");
			hlkFOLLOW_UP_DATE.Attributes.Add("OnClick","fPopCalendar(document.FAX.txtFOLLOW_UP_DATE,document.FAX.txtFOLLOW_UP_DATE)"); //Javascript Implementation for Calender		
			cmbDIARY_ITEM_REQ.Attributes.Add("Onclick","javascript:return ShowFOLLOW_UP_DATE();");   
			cmbDIARY_ITEM_REQ.Attributes.Add("Onblur","javascript:return ShowFOLLOW_UP_DATE();");   

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
			btnSend.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnSend.PermissionString				=	gstrSecurityXML;
			
			objResourceMgr							=	new System.Resources.ResourceManager("cms.client.Aspx.Fax",System.Reflection.Assembly.GetExecutingAssembly());
			custID									=	GetCustomerID()== "" ? 0 : int.Parse(GetCustomerID()); 

			hidCUSTOMER_ID.Value=Convert.ToString(custID);

            hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "//" + custID.ToString() + "//" + "System"; 

			

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
		
			#region If form is not posted back then setting the default values
			if( ! Page.IsPostBack)
			{
				//Setting xml for the page to be displayed in page controls
				if(Request.QueryString["FAX_ROW_ID"]!=null && Request.QueryString["FAX_ROW_ID"].ToString().Length>0)
				{
					int intFaxRowId	= int.Parse(Request.QueryString["FAX_ROW_ID"].ToString());
					int intCustomerId	= int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
					hidOldData.Value	= ClsFax.GetFaxXml(intFaxRowId,intCustomerId);
				}
				if(Request.QueryString["cmbDIARY_ITEM_TO"]!=null)
					intToUserID=int.Parse(Request.QueryString["cmbDIARY_ITEM_TO"].ToString());
				else
					intToUserID=int.Parse(GetUserId().ToString());
				loadDropDowns();
				SetCaptions();
				GetUserEmail();
				GetCustomerFax();
				if (Request.QueryString["CalledFor"]!=null && Request.QueryString["CalledFor"]=="POLICY")
				{
					GetPolicyAgencyEmail();
				}
			}
			//Initializing the validators and other controls
			SetPageLabels();

		}
		#endregion
		private void  loadDropDowns()
		{
			try
			{
				int UserId		=	intToUserID == 0 ? -1 : intToUserID;
				cmbDIARY_ITEM_TO.DataSource		=	ClsCommon.GetUserList();
				cmbDIARY_ITEM_TO.DataTextField	=	USERNAME;
				cmbDIARY_ITEM_TO.DataValueField	=	USERID;
				cmbDIARY_ITEM_TO.DataBind();

				ListItem li=new ListItem(); 
				li=cmbDIARY_ITEM_TO.Items.FindByValue(intToUserID.ToString());   
				if(li!=null)
					li.Selected=true;

                cmbDIARY_ITEM_REQ.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YES_NO");
                cmbDIARY_ITEM_REQ.DataTextField = "LookupDesc";
                cmbDIARY_ITEM_REQ.DataValueField = "LookupCode";
                cmbDIARY_ITEM_REQ.DataBind();
                cmbDIARY_ITEM_REQ.Items.Insert(0, "");
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
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
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReset_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{
			bool bolStatus			=		false;

			try
			{
				string strFrom			=		txtFROM_NAME.Text.Trim().Replace("'","''");
				string strFromName		=		txtFROM_FAX.Text.Trim().Replace("'","''");
				string strToName		=		txtTO.Text.Trim().Replace("'","''"); //Added to name
				string strToNumber		=		txtTO_NUMBER.Text.Trim().Replace("'","''");
				strToNumber				=		strToNumber.Replace("(","");
				strToNumber				=		strToNumber.Replace(")","");
				strToNumber				=		strToNumber.Replace("-","");
				string strSubject		=		txtSUBJECT.Text.Trim().Replace("'","''");
				string strMessage		=		txtMESSAGE.Text.Trim().Replace("'","''");

				string strAttachment	=		txtATTACHMENT.Value.Trim().Replace("'","''");
				string strFileFullPath	=		"";
				if(strAttachment!="")
				{
					strAttachment			=		AttachmentFileName(out strFileFullPath);
					//strAttachment			=		AttachmentFileName();
					strAttachment			=		strAttachment.Trim().Replace("'","''");
				}
				//string strAttachmentPath =		AttachmentDirectoryName(strAttachment);

				
				bolStatus = SendEmail (strToNumber, strFrom, strFromName, strSubject, strMessage, strFileFullPath,strToName);
				
                	
				if (bolStatus)
				{
					ClsFax		objFax		=		new ClsFax();
					ClsFaxInfo objFaxInfo	=		GetFormValue();
			
					int intRetVal			=		0;
					intRetVal				=		objFax.AddNew(objFaxInfo,intLoggedInUserID);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage1.Text		=		Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1013");
						lblMessage1.Visible		=		true;
						hidFormSaved.Value		=		"1";
						hidRowId.Value			=		Convert.ToString(objFaxInfo.FAX_ROW_ID);				
						hidOldData.Value		=		ClsFax.GetFaxXml(intRetVal,int.Parse(hidCUSTOMER_ID.Value));
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{	
						lblMessage1.Text	=		Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value	=		"2";
					}
					else						// Error occured while processing, update failed
					{
						lblMessage1.Text	=		Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value	=		"2";
					}
						
				}
				else
				{
					// failure to send fax
					lblMessage1.Text		=		Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1016");
					hidFormSaved.Value		=		"2";
					lblMessage1.Visible		=		true;
				}
			}
			catch(Exception ex)
			{
				//Publishing the exception using the static method of Exception manager class
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

				lblMessage1.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1016")+ " Try again!";
					//+ "\n" + ex.Message + "\n Try again!";
				lblMessage1.Visible		=	true;
				
				
				return;
			}
			finally{}
		}
					
		private void SetCaptions()
		{
			capFROM_NAME.Text		=	objResourceMgr.GetString("txtFROM_NAME");
			capFROM_FAX.Text		=	objResourceMgr.GetString("txtFROM_FAX");
			capTO.Text				=	objResourceMgr.GetString("txtTO");
			capTO_NUMBER.Text		=	objResourceMgr.GetString("txtTO_NUMBER");
			capSubject.Text			=	objResourceMgr.GetString("txtSUBJECT");
			capMESSAGE.Text			=	objResourceMgr.GetString("txtMESSAGE");
			capATTACHMENT.Text		=	objResourceMgr.GetString("txtATTACHMENT");
            capDIARY_ITEM_REQ.Text = objResourceMgr.GetString("cmbDIARY_ITEM_REQ");
            btnSend.Text            = objResourceMgr.GetString("btnSend");//ashish
            capFOLLOW_UP_DATE.Text = objResourceMgr.GetString("capFOLLOW_UP_DATE");
            capDIARY_ITEM_TO.Text = objResourceMgr.GetString("capDIARY_ITEM_TO");
		}

		private void SetPageLabels()
		{			
			//setting error message by calling singleton functions
			rfvFROM_NAME.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			rfvFROM_FAX.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			rfvSUBJECT.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");
			rfvTO.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
			rfvTO_NUMBER.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
			rfvMESSAGE.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
			revFROM_FAX.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
			revTO_NUMBER.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");

            revTO_NUMBER.ValidationExpression = aRegExpAgencyPhone;
			revFROM_FAX.ValidationExpression		= aRegExpEmail;
			revFOLLOW_UP_DATE.ValidationExpression			= aRegExpDate;
			revFOLLOW_UP_DATE.ErrorMessage					=  "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			csvFOLLOW_UP_DATE.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvFOLLOW_UP_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");

		}
	
		#region Send Email
		/// <summary>
		/// This function is used to send email if password found
		/// </summary>
		/// <param name="strEmail">Email Id of the User</param>
		/// <returns></returns>
		/// strToNumber, strFrom, strFromName, strSubject, strMessage, strAttachment

		public bool SendEmail(string strToNumber, string strFrom, string strFromName, string strSubject, string strMessage, string strAttachmentFile,string strToName)
		{
			FaxManager objFaxManager		=	new FaxManager();
            objFaxManager.FAX_HOSTURL = System.Configuration.ConfigurationManager.AppSettings.Get("FAX_HOSTURL").ToString();
            objFaxManager.FAX_HOSTPORT = System.Configuration.ConfigurationManager.AppSettings.Get("FAX_HOSTPORT").ToString();
            objFaxManager.FAX_USERID = System.Configuration.ConfigurationManager.AppSettings.Get("FAX_USERID").ToString();
            objFaxManager.FAX_USERPWD = System.Configuration.ConfigurationManager.AppSettings.Get("FAX_USERPWD").ToString();
            objFaxManager.FAX_NUMBER_SUFFIX = System.Configuration.ConfigurationManager.AppSettings.Get("FAX_NUMBER_SUFFIX").ToString();

			//objFaxManager.FAX_NUMBER_SUFFIX =	strFrom;	\\Commented Should be pic from web Config key FAX_NUMBER_SUFFIX	


			objFaxManager.FAX_NUMBER		=	strToNumber;
			objFaxManager.FAX_SUBJECT 		=	strSubject;
			
//			string strPath		= null;
//			strPath				= CreateDirStructure();
//			strPath				= strPath + "\\" + "_FAX_" + strAttachmentFile;
//			objFaxManager.FAX_ATTACHMENT	=   strPath;

			objFaxManager.FAX_ATTACHMENT	=   strAttachmentFile;
			objFaxManager.FAX_BODY			=	strMessage;
			objFaxManager.FAX_FORMAT		=	MailFormat.Html; //Not Used in sendFax()
			objFaxManager.FAX_FROM			=	strFromName;
			objFaxManager.FAX_TO_NAME		=	strToName; //Added


			bool boolReturnValue			=	objFaxManager.sendFax();

			return(boolReturnValue);	
		}
		#endregion

		private void GetUserEmail()
		{
			DataRow drCustDetails;
			ClsCustomer ojCustomer;
			ojCustomer			=	new ClsCustomer();
			int UserId			=	Convert.ToInt32(GetUserId());
			drCustDetails		=	ojCustomer.FillUserEmail(UserId).Rows[0];
			txtFROM_NAME.Text	=	drCustDetails[0].ToString() + " " + drCustDetails[1].ToString();
			txtFROM_FAX.Text	=	drCustDetails[2].ToString();
		}
		
		private void GetCustomerFax()
		{
			DataRow drCustDetails;
			ClsCustomer ojCustomer;
			ojCustomer			=	new ClsCustomer();
			int CustId			=	Convert.ToInt32(GetCustomerID());
			if (txtTO.Text.Trim() == "" )
			{
				drCustDetails	=	ojCustomer.FetchCustomerFaxNumber(CustId).Rows[0];
				txtTO.Text				=	drCustDetails[1].ToString() + "/" + drCustDetails[0].ToString();
				//Modified By (Praveen) 12 May 2008
				txtTO_NUMBER.Text	=	drCustDetails[0].ToString();
				txtSUBJECT.Text		=	drCustDetails[1].ToString();
			}
		}

		//If Menu Called from POLICY 14 May 2008
		private void GetPolicyAgencyEmail()
		{
			DataSet dsPolicyDetails;
			ClsCustomer	ojCustomer	=	new ClsCustomer();
			int policyId = 0;
			if(GetPolicyID()!="")
				policyId			=	Convert.ToInt32(GetPolicyID());

			int policyVersionId = 0;
			if(GetPolicyVersionID()!="")
				 policyVersionId	=	Convert.ToInt32(GetPolicyVersionID());

			int CustId = 0;
			if(GetCustomerID()!="")
				CustId				=	Convert.ToInt32(GetCustomerID());	
			
			try
			{
				dsPolicyDetails			=	ojCustomer.GetPolicyAgencyEmail(CustId,policyId,policyVersionId);
				if(dsPolicyDetails!=null && dsPolicyDetails.Tables[0].Rows.Count > 0)
				{
					txtTO.Text				=	dsPolicyDetails.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString() + "/" + dsPolicyDetails.Tables[0].Rows[0]["AGENCY_FAX"].ToString();
					txtTO_NUMBER.Text		=	dsPolicyDetails.Tables[0].Rows[0]["AGENCY_FAX"].ToString();
					//populate the email address of logged in user. Commented on 14 Oct 2008
					//txtFROM_FAX.Text		=	dsPolicyDetails.Tables[0].Rows[0]["AGENCY_EMAIL"].ToString();
					//Itrack 4965
					dsPolicyDetails = ojCustomer.GetPolicyInsuredName(CustId,policyId,policyVersionId);
					if(dsPolicyDetails!=null && dsPolicyDetails.Tables[0].Rows.Count > 0)
					{
						txtSUBJECT.Text		=	dsPolicyDetails.Tables[0].Rows[0]["INSURED_NAME"].ToString();
					}					

				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}


		public ClsFaxInfo GetFormValue()
		{
			ClsFaxInfo objFaxInfo		=	new ClsFaxInfo();	
			objFaxInfo.CUSTOMER_ID		=	custID;
			objFaxInfo.FAX_NUMBER		=	txtTO_NUMBER.Text;
			objFaxInfo.FAX_FROM_NAME	=	txtFROM_NAME.Text;
			objFaxInfo.FAX_FROM			=	txtFROM_FAX.Text;
			objFaxInfo.FAX_TO			=	txtTO.Text;
			objFaxInfo.FAX_RECIPIENTS 	=	txtFROM_NAME.Text;
			objFaxInfo.FAX_SUBJECT		=	txtSUBJECT.Text;
			objFaxInfo.FAX_REFERENCE	=	"";
			objFaxInfo.FAX_BODY			=	txtMESSAGE.Text;
			//objFaxInfo.FAX_ATTACH_PATH	=	strFileName.ToString();
			objFaxInfo.FAX_ATTACH_PATH	=	strGUIDFileName.ToString();
			objFaxInfo.FAX_SEND_DATE	=	DateTime.Now;
			objFaxInfo.FAX_RETURN_CODE	=	"1";		// hard coding to be removed
			objFaxInfo.CREATED_BY		=	int.Parse(GetUserId());
			objFaxInfo.DIARY_ITEM_REQ	=	cmbDIARY_ITEM_REQ.SelectedValue;
			if (cmbDIARY_ITEM_REQ.SelectedValue == "Y")
			{
				objFaxInfo.FOLLOW_UP_DATE=Convert.ToDateTime(txtFOLLOW_UP_DATE.Text);
				objFaxInfo.DIARY_ITEM_TO = int.Parse(cmbDIARY_ITEM_TO.SelectedValue);  
			}
			
				
			
			return objFaxInfo;
		}

		#region File Attachment
//		public string AttachmentFileName()
//		{
//			string strFileName,strFileFullPath;
//			strFileFullPath = "";
//			strFileName	= "";
//			strFileName = AttachmentFileName(out strFileFullPath);
//			return strFileName;
//		}
		public string AttachmentFileName(out string strFileFullPath)
		{
			//string strFileName	= null;
			string strPath		= null;
			strFileFullPath = "";
			strPath				= CreateDirStructure();

			if( txtATTACHMENT.PostedFile != null )
			{
				int Index = txtATTACHMENT.PostedFile.FileName.LastIndexOf("\\");
				if (Index >= 0 )
				{
					strFileName = txtATTACHMENT.PostedFile.FileName.Substring(Index+1);
				}
				else
				{
					strFileName = txtATTACHMENT.PostedFile.FileName;
				}
				string strGUID = System.Guid.NewGuid().ToString();
				strFileFullPath = strPath + "\\" + strGUID + "_FAX_" + strFileName;
				strGUIDFileName = strGUID + "_FAX_" + strFileName; //Added by kasana
				txtATTACHMENT.PostedFile.SaveAs(strFileFullPath);
			}
			else
			{
				// No file
			}
			
			return strFileName;
		}

		string AttachmentDirectoryName(string strFileName)
		{
			string strRoot,strFilePath, strDirName = "";
			try
			{
				string strAgencyCode = GetSystemId();  //Login agency name, will come from session
                strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
				strDirName = Server.MapPath(strRoot);
				strDirName = strDirName + "\\" + custID.ToString() + "\\System";
				strFilePath = strDirName + "\\" + System.Guid.NewGuid() + "_FAX_" + strFileName;
				return strFilePath;
			}
			catch
			{return "";}

		}
		/// <summary>
		/// Creates the directory structure, where the file will be saved
		/// </summary>
		private string CreateDirStructure()
		{
			
			string strRoot, strDirName = "";
			try
			{
				string strAgencyCode = GetSystemId();  //Login agency name, will come from session
                strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
				strDirName = Server.MapPath(strRoot);
				{
					//For fax , attachment will go into customer directory
					/*strDirName = strDirName + "\\Customer"; */
					string strDirSystem = strDirName + "\\" + strAgencyCode;
					strDirName = strDirName + "\\" + custID.ToString() + "\\System";

					if (!System.IO.Directory.Exists(strDirName))
					{
						//Creating the directory
						System.IO.Directory.CreateDirectory(strDirName);
					}
				}
			}
			catch (Exception objEx)
			{
				throw (objEx);
			}
			return strDirName;
		}
		#endregion
	}
}
