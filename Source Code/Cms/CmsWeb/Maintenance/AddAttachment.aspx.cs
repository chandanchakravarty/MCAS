/******************************************************************************************
	<Author					: - >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > May 03, 2005
	<Modified By			: - > Pradeep
	<Purpose				: - > Added code in PopulateXML javacript function to display link to file
    
	<Modified Date			: - > June 03, 2005
	<Modified By			: - > Pradeep
	<Purpose				: - > Passed attachment id in refreshwebgrid function after insert and update
	
	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page	
	
     <Modified Date			: -17/01/2006
	<Modified By			: - shafe
	<Purpose				: - Attachment for Policy
*******************************************************************************************/

using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;




namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddAttachment.
	/// </summary>
	public class AddAttachment : Cms.CmsWeb.cmsbase
	{
		System.Resources.ResourceManager objResourceMgr;
		#region Form Variable
		private Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment;
		string	strFileName	;	//stores the name of file to be uploaded
		string	strFileDesc	;	//User defined description of file
		string	strType	;		//Type of File
		//string	strRowId ;		//Holds the id of the record
		int		intEntityId ;	//Holds the id of the enitity , to which file will attach
		string strEntityType;	// Holds the type of entity, to which the file will attach
		int intAttachType ; 

		#endregion

		#region web controls variables
		protected System.Web.UI.HtmlControls.HtmlInputFile txtATTACH_FILE_NAME;
		protected System.Web.UI.WebControls.TextBox txtATTACH_FILE_DESC;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAttachFileName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAttachSourceID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApplicationNumber;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApplicationVersion;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnFileName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvATTACH_FILE_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPostedFile;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntity_Type;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRootPath;
		protected System.Web.UI.WebControls.Label lblATTACH_FILE_TYPE;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label lblATTACH_FILE_NAME;
		protected System.Web.UI.WebControls.Label lblAppNumber;
		protected System.Web.UI.WebControls.Label lblAppVersion;
		protected Cms.CmsWeb.Controls.CmsButton btnback1;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.WebControls.Label capATTACH_FILE_TYPE;
		protected System.Web.UI.WebControls.Label capATTACH_FILE_NAME;
		protected System.Web.UI.WebControls.Label capATTACH_FILE_DESC;
		protected System.Web.UI.WebControls.TextBox txtATTACH_FILE_TYPE;
		protected System.Web.UI.WebControls.Label capATTACH_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbATTACH_TYPE;
		 public string strCalledFrom="";
		protected Cms.CmsWeb.Controls.CmsButton btnback2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidfileLink;
		protected System.Web.UI.WebControls.RegularExpressionValidator revATTACH_FILE_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revATTACH_FILE_EXT;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revATTACH_FILE_PDF;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.Label Caplook;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
            
			// Put user code to initialize the page here
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();
                hidCalledFrom.Value = Request.QueryString["CALLEDFROM"].ToString().Trim();
           
			}
			SetErrorMessages();
            btnSave.Attributes.Add("onClick", "javascript:SetFileType();RemoveSpecialChar(document.getElementById('txtATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_NAME'));RemoveExecutableFiles(document.getElementById('txtATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_EXT'));");
			switch(strCalledFrom)
			{
				//Added by Sibin on 20 Oct 08 to add it into Application Details Permission List
				case "application" :
				case "Application" :
					base.ScreenId	=	"201_8_1";
					break;
				case "reinsurance" :
				case "REINSURANCE" :
					base.ScreenId	=	"262_2";
					break;

				case "bank" :
				case "BANK" :
					base.ScreenId	=	"125_1_2_0";
					break;
				case "agency" :
				case "AGENCY" :
					base.ScreenId	=	"10_2_0";
					break;
				case "department" :
				case "DEPARTMENT" :
					base.ScreenId	=	"29_1_0";
					break;
				case "division" :
				case "DIVISION" :
					base.ScreenId	=	"28_1_0";
					break;
				
				case "fin" :
				case "FIN" :
					base.ScreenId	=	"35_2_0";
					break;
				case "mortgage" :
				case "MORTGAGE" :
					base.ScreenId	=	"37_1_0";
					break;
				case "profit" :
				case "PROFIT" :
                case "ProfitCenter" :					
					base.ScreenId	=	"27_1_0";
					break;


					//Added by Sibin on 21 Oct 08 to add it into Policy Details Permission List
				case "policy" :
				case "POLICY" :
					base.ScreenId	=	"224_8_1";
					break;

				case "tax" :
				case "TAX" :
					base.ScreenId	=	"36_1_0";
					break;
				case "vendor" :
				case "VENDOR" :
					base.ScreenId	=	"32_1";
					break;
				case "reinsurer" :
				case "REINSURER" :
					base.ScreenId	=	"263_1_0";
					break;
               
				case "InCLT" :
               
					base.ScreenId	=	"120_4_0";
					break;

                case "CUSTOMER":
                    base.ScreenId = "134_4_0";
                    break;
				case "CLAIM" :
                
					base.ScreenId = "306_9_0";
					break;
				default :
					base.ScreenId	=	"36_1";
					break;
			}
			#endregion
            

			//Updating the security xml, as per the called from
			UpdateSecurityXML(strCalledFrom);

            //Set the Culture Language Added By Lalit March 17,2010

            SetCultureThread(GetLanguageCode());
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddAttachment" ,System.Reflection.Assembly.GetExecutingAssembly());
			SetCaptions();

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";
            if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "AddAttachment.xml"))
                setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/AddAttachment.xml");  


			btnBack.Attributes.Add("onclick","javascript:return DoBack();");
			btnback1.Attributes.Add("onclick","javascript:return DoBack();");
			this.btnback2.Attributes.Add("onclick","javascript:return DoBack();");

			SetText();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			// code for providing the back to serach button only on customer Detail form tabs. 
			if (Request.QueryString["BackOption"] != null && Request.QueryString["BackOption"].ToString()=="Y")
			{
				btnBack.Visible = true;
			}
			
			btnBack.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnBack.PermissionString	=	gstrSecurityXML;	

			btnback1.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnback1.PermissionString	=	gstrSecurityXML;	
			
			btnback2.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Read;
			btnback2.PermissionString	=	gstrSecurityXML;	
			
			btnReset.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			if (! IsPostBack)
			{
				hidCustomInfo.Value = "";
				hidEntityId.Value		=	Request.Params["EntityId"];
				hidEntity_Type.Value	=	Request.Params["EntityType"];

				//added by vj to get the application number and application version
				hidApplicationNumber.Value  =	Convert.ToString(Request.QueryString["APP_NUMBER"]);	
				hidApplicationVersion.Value = Convert.ToString(Request.QueryString["APP_VERSION"]);	

				lblAppNumber.Text = hidApplicationNumber.Value;
				lblAppVersion.Text = hidApplicationVersion.Value;
				// fill attachment type from lookup
				cmbATTACH_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ATHTYP");					
				cmbATTACH_TYPE.DataTextField		=	"LookupDesc";
				cmbATTACH_TYPE.DataValueField	=	"LookupID";
				cmbATTACH_TYPE.DataBind();

				//if (hidEntity_Type.Value.ToUpper().Equals("CUSTOMER"))

                if (hidEntity_Type.Value.ToUpper().Trim() == "CUSTOMER")// && hidApplicationVersion.Value.ToString().Trim() == "")
				{
					//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/Customer/" + hidEntityId.Value + "/";
					hidEntityId.Value		=	Request.Params["EntityId"];
					//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/Customer/" + hidEntityId.Value + "/Attachment/";
					hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/" + GetSystemId() + "/" + hidEntityId.Value + "/Attachments/";
				}
				else if (hidEntity_Type.Value.ToUpper().Equals("APPLICATION"))// || Request.Params["EntityId"]!=null)
				{
					//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/Customer/" +  "/Application/";
					hidEntityId.Value		=	GetCustomerID();
					//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/Customer/" +  hidEntityId.Value + "/System/";					
					hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/" + GetSystemId() + "/" +  hidEntityId.Value + "/Attachments/";					
				}
				else if (hidApplicationVersion.Value.ToString().Trim() != "")
				{
					//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/Customer/" +  "/Application/";
					hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/Customer/" +  hidEntityId.Value + "/System/";
				}// for itrack no 1225
				else if (hidEntity_Type.Value.ToUpper().Trim() =="POLICY" || hidEntity_Type.Value.ToUpper().Trim()=="CLAIM")
               
				{					
					hidEntityId.Value		=	GetCustomerID();
					//hidRootPath.Value = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/Customer/" +  hidEntityId.Value + "/System/";
					hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/" + GetSystemId() + "/" +  hidEntityId.Value + "/Attachments/";					
				}
				else
                {
                 
					hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/attachment/" + GetSystemId() + "/";
				}

				//Initializing the validators and other controls
				SetPageLabels();
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				if ( Request.QueryString["Attach_ID"] != null )
				{
					//Update
					LoadData(Convert.ToInt32(Request.QueryString["Attach_ID"]));
					this.rfvATTACH_FILE_NAME.Enabled = false;
				}
				else
				{
					//Add new
					//hidRowId.Value = "New";
				}

			}
			else
			{
				if (hidRowId.Value == "New")
					hidPostedFile.Value		=	txtATTACH_FILE_NAME.PostedFile.FileName;
			}


			//added by vj to set the hidRowId value if the entity type = Application 
			if ( Request.QueryString["Attach_ID"] != null )
			{
				//if (Request.QueryString["calledfrom"].ToString().ToUpper().Trim() == "APPLICATION")
           
                if(strCalledFrom.ToUpper() == "APPLICATION")
				{
					objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
					hidAttachSourceID.Value = Convert.ToString(objAttachment.GetAppAttachSourceID(Convert.ToInt32(Request.QueryString["Attach_ID"])));
				}
				else if (hidApplicationVersion.Value.Trim() != "")
				{
					objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
					hidAttachSourceID.Value = Convert.ToString(objAttachment.GetAppAttachSourceID(Convert.ToInt32(Request.QueryString["Attach_ID"])));				
				}
			}
		}

		private void UpdateSecurityXML(string CalledFrom)
		{
			//If called from application, then changing the security xml to read only mode,
			//if application is converted into policy
			if (strCalledFrom.ToUpper() == "APPLICATION")
			{
				//Called from application, hence checking whether policy for the application exist or not

				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				//System.Data.DataSet ds = objGenInfo.GetPolicyDetails(int.Parse(GetCustomerID()), int.Parse(GetAppID()), int.Parse(GetAppVersionID()));
				System.Data.DataSet ds = objGenInfo.GetPolicyDetailsForAttachment(int.Parse(GetCustomerID()), int.Parse(GetAppID()));
				if (ds.Tables[0].Rows.Count > 0)
				{
					//Policy exists for this particulat application, hence changing the security xml to view mode only
					gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
					base.InitializeSecuritySettings();
				}
				ds.Dispose();
			}
			else if (strCalledFrom.ToUpper() == "POLICY")
			{
				//Here we will check status of policy in session
				try
				{
					//Changing the security xml 
					gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
					base.InitializeSecuritySettings(); 
					// Commmented by swastika acc to iTrack #1434
					// Irrespective of the policy status, the attachment can be added.
					/*
					string strPolicyId = GetPolicyID();
					string strPolicyVerId = GetPolicyVersionID();
					string strCustomerID = GetCustomerID();
					
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo;
					objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

					System.Data.DataSet ds = objGenInfo.GetPolicyDataSet(int.Parse(strCustomerID)
						, int.Parse(strPolicyId), int.Parse(strPolicyVerId));

					if (ds.Tables[0].Rows.Count > 0)
					{

						//If policy status is not one of following, changing the security xml to read only mode

						string policyStatus = ds.Tables[0].Rows[0]["POLICY_STATUS_CODE"].ToString().ToUpper().Trim();
						if ( ! ( policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ENDORSEMENT 
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_RENEW
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_CORRECTIVE_USER 
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ISSUE
							|| policyStatus == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_SUSPENDED) )
						{

							//Changing the security xml to view mode only
							gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
							base.InitializeSecuritySettings(); 
						}
					}
					ds.Dispose();
					*/
				}
				catch
				{
				}
			}
		}

		private Model.Maintenance.ClsAttachmentInfo GetFormValues()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.ClsAttachmentInfo objAttachmentInfo = new Cms.Model.Maintenance.ClsAttachmentInfo();
			if (hidRowId.Value == "New")
			{
				objAttachmentInfo.ATTACH_FILE_NAME = txtATTACH_FILE_NAME.PostedFile.FileName;				
			
				int intIndex	=	strFileName.LastIndexOf("\\");
				strFileName		=	strFileName.Substring(intIndex+1);	//Taking only file name not whole path
				objAttachmentInfo.ATTACH_FILE_NAME = strFileName;				
				strType			=	txtATTACH_FILE_TYPE.Text;
				objAttachmentInfo.ATTACH_FILE_TYPE = strType;				
				intEntityId		=	int.Parse(hidEntityId.Value);
				objAttachmentInfo.ATTACH_ENT_ID = intEntityId;				
				strEntityType	=	hidEntity_Type.Value;
				objAttachmentInfo.ATTACH_ENTITY_TYPE = strEntityType;
				//objAttachmentInfo.ATTACH_USER_ID=int.Parse(GetUserId());				
				objAttachmentInfo.ATTACH_DATE_TIME = DateTime.Now;
				
			}
			else
			{
				objAttachmentInfo.ATTACH_FILE_NAME = hidAttachFileName.Value;
				objAttachmentInfo.ATTACH_FILE_TYPE = txtATTACH_FILE_TYPE.Text;
			}
			if(Request.QueryString["APP_ID"]!=null && Request.QueryString["APP_ID"]!="")
				objAttachmentInfo.ATTACH_APP_ID = Convert.ToInt32(Request.QueryString["APP_ID"]);
			if(Request.QueryString["APP_VERSION_ID"]!=null && Request.QueryString["APP_VERSION_ID"]!="")
				objAttachmentInfo.ATTACH_APP_VER_ID = Convert.ToInt32(Request.QueryString["APP_VERSION_ID"]);
			objAttachmentInfo.ATTACH_USER_ID=int.Parse(GetUserId());				
			objAttachmentInfo.CREATED_BY = int.Parse(GetUserId());
			objAttachmentInfo.MODIFIED_BY = int.Parse(GetUserId());
			strFileDesc		=	txtATTACH_FILE_DESC.Text;
			objAttachmentInfo.ATTACH_FILE_DESC = strFileDesc;	
			//if(cmbATTACH_TYPE.SelectedIndex >0)
			if(cmbATTACH_TYPE.SelectedItem!=null && cmbATTACH_TYPE.SelectedItem.Value!="")
			{ 
				objAttachmentInfo.ATTACH_TYPE = intAttachType = int.Parse(cmbATTACH_TYPE.SelectedValue.ToString());
				//objAttachmentInfo.ATTACH_TYPE = int.Parse(cmbATTACH_TYPE.SelectedValue.ToString());
			}
			else
			{
				objAttachmentInfo.ATTACH_TYPE = intAttachType =0;
				//objAttachmentInfo.ATTACH_TYPE=0;
			}
			return objAttachmentInfo;
		}

		private void SetCaptions()
		{			
			capATTACH_FILE_NAME.Text						=		objResourceMgr.GetString("txtATTACH_FILE_NAME");
			capATTACH_FILE_DESC.Text						=		objResourceMgr.GetString("txtATTACH_FILE_DESC");
			capATTACH_FILE_TYPE.Text						=       objResourceMgr.GetString("txtATTACH_FILE_TYPE");
            capATTACH_TYPE.Text                             =       objResourceMgr.GetString("cmbATTACH_TYPE");
            btnback2.Text                                   =       objResourceMgr.GetString("btnback2");
            Caplook.Text = objResourceMgr.GetString("Caplook");
		}

		private void SetErrorMessages()
		{
			revATTACH_FILE_NAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1047");
			revATTACH_FILE_EXT.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1048");
           // revATTACH_FILE_PDF.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1049");
		}
		public void SetText()

		{
			if(strCalledFrom=="reinsurer")			
			{
				btnback1.Visible=	true;
				btnBack.Visible	=false;
			}
			else 
			{
				btnBack.Visible	=true;
				btnback1.Visible=	false;
			}

			if(strCalledFrom=="REINSURANCE")			
			{
				btnback2.Visible=	true;
				btnBack.Visible	=false;
				btnback1.Visible=false;
			}
			else 
			{
				btnBack.Visible	=true;
				btnback2.Visible=	false;
				btnback1.Visible=false;
			}
		}
      

		public void LoadData(int attachID)
		{
			//int attachID = Convert.ToInt32(Request.QueryString["Attach_ID"]);
					
			Cms.BusinessLayer.BlCommon.ClsAttachment objAttach = new Cms.BusinessLayer.BlCommon.ClsAttachment();
			DataSet dsAttachment = objAttach.GetAttachmentByID(attachID);
					
			this.hidOldData.Value = dsAttachment.GetXml();

			string filename = hidRootPath.Value + ClsCommon.FetchValueFromXML("ATTACH_ID",hidOldData.Value) + '_' + ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME",hidOldData.Value);
			int startOfFile = filename.IndexOf("Upload");
			string filePath = filename.Substring(startOfFile + 6);
			string []fileURL = filePath.Split('.'); 
			string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
			hidfileLink.Value = EncryptedPath; 
			revATTACH_FILE_NAME.Visible=false;
			revATTACH_FILE_EXT.Visible=false;
            //revATTACH_FILE_PDF.Enabled = false;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFormValue();
		}

		#region Private form methods
 
		/// <summary>
		/// Validate posted data from form. Calls in SaveFormData function
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool DoValidationCheck()
		{
			try
			{
				if (hidRowId.Value == "New")
				{
					if( txtATTACH_FILE_NAME.PostedFile.FileName.Trim().Equals(""))
					{
						return false;
					}

                    //if( txtATTACH_FILE_TYPE.Text.Trim().Equals(""))
                    //{
                    //    return false;
                    //}
					if (txtATTACH_FILE_NAME.PostedFile.InputStream.Length == 0)
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","123");
						hidFormSaved.Value = "1";
						return false;
					}
				}
				
				return true;
			}
			catch 
			{
				return false;
			}
		}

		/// <summary>
		/// This function will set the Error Message,validation expresion all validators controls
		/// </summary>
		private void SetPageLabels()
		{
			//setting validation expression for different validation control
			rfvATTACH_FILE_NAME.ErrorMessage =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"50");

			//setting error message by calling singleton functions
			
		}



		/// <summary>
		/// Saves the posted data from form, calls in click event of save button
		/// </summary>
		/// <returns></returns>
		private void SaveFormValue()
		{
			//User details to be imporsonate, comes from web.config
			string strUerName, strPassWd, strDomain;

			int intAttachID = 0;	//Holds the id of records
			objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
			int CustomerID=0,ApplicationID=0,AppVerID=0,intUserID;
			int PolicyID, PolicyVerID;
			ClsAttachmentInfo objAttachmentInfo = new ClsAttachmentInfo();
			//Getting the user,passs wd and domain from web.config
			strUerName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
            strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
            strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

			try
			{
			
				//Retreiving the form values
				GetFormValue();
			
				//Checking whether the values supplied are valid or not
				if ( DoValidationCheck() == false ) return;

						
					int intReturnVal;		//For taking the return value of business object func
					
					if(Request.QueryString["EntityType"] != null)
						strEntityType = Request.QueryString["EntityType"].ToString();

					if(Request.QueryString["EntityName"] != null)
						strEntityType += "~" + Request.QueryString["EntityName"].ToString();

					if(hidRowId.Value == "New")	//Checking the form contains the new record or existing record
					{
						//Adding a new record to database using the Division object
						
						//modified by vj to add the application attachement.


                        if (strCalledFrom.ToString().Trim().ToUpper() == "APPLICATION")
						{
							
							//CustomerID = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"]);							
							CustomerID = Convert.ToInt32(GetCustomerID());
							ApplicationID = Convert.ToInt32(Request.QueryString["APP_ID"]);
							AppVerID = Convert.ToInt32(Request.QueryString["APP_VERSION_ID"]);
                            if (GetPolicyID() != "")
                                PolicyID = Convert.ToInt32(GetPolicyID());
                            else
                                PolicyID = 0;
                            if (GetPolicyVersionID() != "")
                                PolicyVerID = Convert.ToInt32(GetPolicyVersionID());
                            else
                                PolicyVerID = 0;
							intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
								,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
								,txtATTACH_FILE_NAME.PostedFile.InputStream,strEntityType,
                                CustomerID, ApplicationID, AppVerID, PolicyID, PolicyVerID, intAttachType);
						}

                        else if (strCalledFrom.ToString().Trim().ToUpper() == "POLICY" || strCalledFrom.ToString().Trim().ToUpper() == "CLAIM")
						{							
							/*CustomerID = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"]);
							PolicyID = Convert.ToInt32(Request.QueryString["POLICY_ID"]);
							PolicyVerID = Convert.ToInt32(Request.QueryString["POLICY_VERSION_ID"]);

							intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
								,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
								,txtATTACH_FILE_NAME.PostedFile.InputStream,strEntityType,
								CustomerID,0,0,PolicyID,PolicyVerID);*/
							
							//Added By shafi 17-10-2006
							//Get The POlicy id and version Id From session
							//if not prsent then set to 0
							int pol_id;
							int pol_Ver_id;
                            if (strCalledFrom.ToString().Trim().ToUpper() == "CLAIM")
							{
								strEntityType = "Claim";
								if(Request.QueryString["EntityId"]!=null && Request.QueryString["EntityId"].ToString()!="")
									intEntityId = int.Parse(Request.QueryString["EntityId"].ToString());
							}
							else
								strEntityType = "Policy";

							if(GetPolicyID()!="")
								pol_id =Convert.ToInt32(GetPolicyID());
							else
								pol_id =0;
							if(GetPolicyVersionID()!="")
								pol_Ver_id=Convert.ToInt32(GetPolicyVersionID());
							else
								pol_Ver_id =0;

                            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                                CustomerID = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"]);
                            else
                                CustomerID = int.Parse(GetCustomerID());
							if(Request.QueryString["APP_ID"]!=null && Request.QueryString["APP_ID"].ToString()!="")
								ApplicationID = Convert.ToInt32(Request.QueryString["APP_ID"]);
							if(Request.QueryString["APP_VERSION_ID"]!=null && Request.QueryString["APP_VERSION_ID"].ToString()!="")
								AppVerID = Convert.ToInt32(Request.QueryString["APP_VERSION_ID"]);

							intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
								,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
								,txtATTACH_FILE_NAME.PostedFile.InputStream,strEntityType,
								CustomerID,ApplicationID,AppVerID,pol_id,pol_Ver_id,intAttachType);
						}

						else if (strCalledFrom.ToString().Trim().ToUpper() == "INCLT")
						{
							if(GetCustomerID()!="")
								CustomerID = Convert.ToInt32(GetCustomerID());
							else
								CustomerID = 0;

							intUserID=int.Parse(GetUserId());
							objAttachmentInfo = GetFormValues();
							objAttachmentInfo.ATTACH_CUSTOMER_ID = CustomerID;
							objAttachmentInfo.CREATED_BY = int.Parse(GetUserId());

							if(hidCustomInfo.Value=="" && CustomerID != 0) 
								hidCustomInfo.Value=objAttachment.GetCustomerDetails(CustomerID.ToString());
							/////////
							int intAttachType =0;
							intAttachType = objAttachmentInfo.ATTACH_TYPE;
							string strAttachType ="";
							switch(intAttachType)
							{
								case 11791:
									strAttachType ="Home Photograph";
									break;
								case 11792:
									strAttachType ="Protective Device Certificate";
									break;
								case 11793:
									strAttachType ="Scheduled Articles Photograph";
									break;
								case 11794:
									strAttachType ="Appraisal";
									break;
								case 11795:
									strAttachType ="Bill of Sale";
									break;
								case 11796:
									strAttachType ="Other";
									break;
								case 11933:
									strAttachType ="Supporting Document";
                                    break;
                                case 14643:
                                    strAttachType = "Risk Survey Requirement";
									break;
							}
				

							////////

							hidCustomInfo.Value+="; File Name:- " + objAttachmentInfo.ATTACH_FILE_NAME 
								+"<br>"+"; File Description:- "+ objAttachmentInfo.ATTACH_FILE_DESC
								+"<br>"+"; File Type:- " + objAttachmentInfo.ATTACH_FILE_TYPE 
								+"<br>" +"; Attachment Type:- " +	strAttachType; 
							//intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
							//	,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
							//	,txtATTACH_FILE_NAME.PostedFile.InputStream,strEntityType);

							//intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
							//	,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
							//	,txtATTACH_FILE_NAME.PostedFile.InputStream,strEntityType,CustomerID,intUserID,hidCustomInfo.Value);
							intReturnVal = objAttachment.AddAttachment(objAttachmentInfo,ref intAttachID,txtATTACH_FILE_NAME.PostedFile.InputStream,hidCustomInfo.Value,intAttachType);
							//public int AddAttachment(ClsAttachmentInfo objAttachmentInfo, ref int intAttachmentId,System.IO.Stream objFileStream,string strCustomInfo)
							//objAttachmentInfo.ATTACH_FILE_NAME
                        }
                        else if (strCalledFrom.ToString().Trim().ToUpper() == "CUSTOMER")
                        {
                            if (GetCustomerID() != "")
                                CustomerID = Convert.ToInt32(GetCustomerID());
                            else
                                CustomerID = 0;

                            intUserID = int.Parse(GetUserId());
                            objAttachmentInfo = GetFormValues();
                            objAttachmentInfo.ATTACH_CUSTOMER_ID = CustomerID;
                            objAttachmentInfo.CREATED_BY = int.Parse(GetUserId());

                            if (hidCustomInfo.Value == "" && CustomerID != 0)
                                hidCustomInfo.Value = objAttachment.GetCustomerDetails(CustomerID.ToString());
                            /////////
                            int intAttachType = 0;
                            intAttachType = objAttachmentInfo.ATTACH_TYPE;
                            string strAttachType = "";
                            switch (intAttachType)
                            {
                                case 11791:
                                    strAttachType = "Home Photograph";
                                    break;
                                case 11792:
                                    strAttachType = "Protective Device Certificate";
                                    break;
                                case 11793:
                                    strAttachType = "Scheduled Articles Photograph";
                                    break;
                                case 11794:
                                    strAttachType = "Appraisal";
                                    break;
                                case 11795:
                                    strAttachType = "Bill of Sale";
                                    break;
                                case 11796:
                                    strAttachType = "Other";
                                    break;
                                case 11933:
                                    strAttachType = "Supporting Document";
                                    break;
                                case 14643:
                                    strAttachType = "Risk Survey Requirement";
                                    break;
                            }


                            ////////

                            hidCustomInfo.Value += "; File Name:- " + objAttachmentInfo.ATTACH_FILE_NAME
                                + "<br>" + "; File Description:- " + objAttachmentInfo.ATTACH_FILE_DESC
                                + "<br>" + "; File Type:- " + objAttachmentInfo.ATTACH_FILE_TYPE
                                + "<br>" + "; Attachment Type:- " + strAttachType;
                            //intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
                            //	,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
                            //	,txtATTACH_FILE_NAME.PostedFile.InputStream,strEntityType);

                            //intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
                            //	,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
                            //	,txtATTACH_FILE_NAME.PostedFile.InputStream,strEntityType,CustomerID,intUserID,hidCustomInfo.Value);
                            intReturnVal = objAttachment.AddAttachment(objAttachmentInfo, ref intAttachID, txtATTACH_FILE_NAME.PostedFile.InputStream, hidCustomInfo.Value, intAttachType);
                            //public int AddAttachment(ClsAttachmentInfo objAttachmentInfo, ref int intAttachmentId,System.IO.Stream objFileStream,string strCustomInfo)
                            //objAttachmentInfo.ATTACH_FILE_NAME
                        }
						else //Called for Division or others
						{
                            if (GetCustomerID() != "")
                            {
                                CustomerID = Convert.ToInt32(GetCustomerID());
                                //ApplicationID = Convert.ToInt32(GetAppID());
                                //AppVerID = Convert.ToInt32(GetAppVersionID());
                            }
                           // objAttachmentInfo.ATTACH_CUSTOMER_ID = CustomerID;
                            if (GetAppID() != "")
                            {
                                ApplicationID = Convert.ToInt32(GetAppID());
                            }
                            if (GetAppVersionID() != "")
                            {
                                AppVerID = Convert.ToInt32(GetAppVersionID());
                            }
                          
                            hidEntityId.Value = Request.Params["EntityId"];
                            hidEntity_Type.Value = Request.Params["EntityType"];
                            strEntityType = hidEntity_Type.Value;
                            intEntityId = int.Parse(hidEntityId.Value);
                          
                            
							intReturnVal = objAttachment.AddAttachment(ref intAttachID,intEntityId, strFileName
								,DateTime.Now,int.Parse(base.GetUserId()),strFileDesc,strType
                                , txtATTACH_FILE_NAME.PostedFile.InputStream, CustomerID, ApplicationID, AppVerID, strEntityType, intAttachType);
						}
							 
						

						if (intReturnVal > 0)
						{
							lblMessage.Text					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"29");
							hidFormSaved.Value				= "1";
							hidRowId.Value					= intAttachID.ToString();
							//added by vj to set the hidRowId value if the entity type = Application 
							hidAttachSourceID.Value			= intAttachID.ToString();
							objAttachmentInfo.ATTACH_ID = intAttachID;
							rfvATTACH_FILE_NAME.Enabled		= false;

							
							//Beginigng the imporsonation 
							if (objAttachment.ImpersonateUser(strUerName, strPassWd, strDomain))
							{
								if ( SaveUploadedFile(txtATTACH_FILE_NAME,intEntityId.ToString(),intAttachID) == false)
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
						}
						else
						{
							lblMessage.Text	= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
							hidFormSaved.Value				= "2";
						}
						lblMessage.Visible	= true;
					}
					else
					{
						//Updating the attachment
						intAttachID = int.Parse(hidRowId.Value);
						if(GetCustomerID()!="")
							CustomerID = Convert.ToInt32(GetCustomerID());
						else
							CustomerID = 0;
						intUserID=int.Parse(GetUserId());
						if (strCalledFrom.ToString().Trim().ToUpper() == "APPLICATION")
						{	
							objAttachmentInfo = GetFormValues();
							objAttachmentInfo.ATTACH_CUSTOMER_ID = CustomerID;
							objAttachmentInfo.CREATED_BY = int.Parse(GetUserId());							
							objAttachmentInfo.ATTACH_ID = intAttachID;
                            objAttachmentInfo.ATTACH_POLICY_ID = Convert.ToInt32(GetPolicyID());
                            objAttachmentInfo.ATTACH_POLICY_VER_TRACKING_ID = Convert.ToInt32(GetPolicyVersionID());
							ClsAttachmentInfo objOldAttachmentInfo = new ClsAttachmentInfo();
							//string strEntityType="";
							base.PopulateModelObject(objOldAttachmentInfo,hidOldData.Value);
							//
							if(Request.QueryString["APP_ID"]!=null && Request.QueryString["APP_ID"]!="")
								ApplicationID = Convert.ToInt32(Request.QueryString["APP_ID"]);
							if(Request.QueryString["APP_VERSION_ID"]!=null && Request.QueryString["APP_VERSION_ID"]!="")
								AppVerID = Convert.ToInt32(Request.QueryString["APP_VERSION_ID"]);
							//intReturnVal = objAttachment.Update(intAttachID,strFileDesc,hidOldData.Value,CustomerID,intUserID,0);
							//intReturnVal = objAttachment.Update(intAttachID,strFileDesc,hidOldData.Value,CustomerID,ApplicationID,AppVerID,intUserID,0);
							intReturnVal = objAttachment.Update(objAttachmentInfo,objOldAttachmentInfo,strEntityType);
							
						}
                        else if (strCalledFrom.ToString().Trim().ToUpper() == "INCLT")
						{
							objAttachmentInfo = GetFormValues();							
							objAttachmentInfo.CREATED_BY = int.Parse(GetUserId());
							objAttachmentInfo.ATTACH_CUSTOMER_ID = CustomerID;
							if(CustomerID!=0)
								{									
									hidCustomInfo.Value=objAttachment.GetCustomerDetails(CustomerID.ToString()); 
//									+";File Name = " + objAttachmentInfo.ATTACH_FILE_NAME + "<br>"
//									+"; Attachment Type =" + objAttachmentInfo.ATTACH_TYPE +"<br>"+
//									"File Type =" + objAttachmentInfo.ATTACH_FILE_TYPE;
								}
								else
								{
									hidCustomInfo.Value="";
								}
							LoadData(intAttachID);
							objAttachmentInfo.ATTACH_ID=intAttachID;
							ClsAttachmentInfo objOldAttachmentInfo = new ClsAttachmentInfo();
							base.PopulateModelObject(objOldAttachmentInfo,hidOldData.Value);
							//intReturnVal = objAttachment.Update(intAttachID,strFileDesc,hidOldData.Value,CustomerID,intUserID,1,hidCustomInfo.Value);
							//public int Update(ClsAttachmentInfo objAttachmentInfo,ClsAttachmentInfo objOldAttachmentInfo,int flag, string strCustomInfo)				
							intReturnVal = objAttachment.Update(objAttachmentInfo,objOldAttachmentInfo,1,hidCustomInfo.Value,intAttachType);							
						}
                       
						else 
						{
							//Updating the attachment

                           
							intAttachID = int.Parse(hidRowId.Value);
							intReturnVal = objAttachment.Update(intAttachID,strFileDesc, intAttachType.ToString(),hidOldData.Value);
						}
						//intReturnVal = objAttachment.Update(intAttachID,strFileDesc,hidOldData.Value,CustomerID,intUserID,1);
						if (intReturnVal > 0 && strCalledFrom.ToUpper() != "INCLT")
						{
							//Model.Maintenance.ClsAttachmentInfo objAttachmentInfo;
							objAttachmentInfo = GetFormValues();
							//ClsAttachmentInfo objAttachmentInfo = new ClsAttachmentInfo();
							ClsAttachmentInfo objOldAttachmentInfo = new ClsAttachmentInfo();
							base.PopulateModelObject(objOldAttachmentInfo,hidOldData.Value);

							objAttachmentInfo.ATTACH_FILE_NAME = txtATTACH_FILE_NAME.PostedFile.FileName;				
			
							strType			=	txtATTACH_FILE_TYPE.Text;
							objAttachmentInfo.ATTACH_FILE_TYPE = strType;
                            strEntityType = hidEntity_Type.Value;
							string entityName = "", entityCode = "";
                         
                                string[] entity = strEntityType.Split('~');
                                if (entity.Length > 2)
                                {
                                    entityName = entity[1];
                                    entityCode = entity[2];
                                }
                                strEntityType = entity[0];
                         
							string TransactionDescription="";
							//							TransactionDescription = "Attachment has been modified.";
							string CustomDesc="";
							if(strEntityType == "Division")
							{
								CustomDesc = "; Division Name:" + entityName +"<br>" +
									"; Division Code:" + entityCode +"<br>";
                                TransactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1611");// "Attachment of Division has been modified.";
							}
							else if(strEntityType == "Department")
							{
								CustomDesc = "; Department Name:" + entityName +"<br>" +
									"; Department Code:" + entityCode +"<br>";
                                TransactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1612");// "Attachment of Department has been modified.";
							}
							else if(strEntityType == "ProfitCenter")
							{
								CustomDesc = "; ProfitCenter Name:" + entityName +"<br>" +
									"; ProfitCenter Code:" + entityCode +"<br>";
                                TransactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1613");// "Attachment of Profit Center has been modified.";
							}

							else if(strEntityType == "Mortgage")
							{
								CustomDesc = "; Additional Interest Name:" + entityName +"<br>" +
									"; Additional Interest Code:" + entityCode +"<br>";
                                TransactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1614");// "Attachment of Additional Interest has been modified.";
							}

							else if(strEntityType == "Vendor")
							{
								CustomDesc = "; Company Name:" + entityName +"<br>" +
									"; Vendor Code:" + entityCode +"<br>";
                                TransactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1615");// "Attachment of Vendor has been modified.";
							}

							else if(strEntityType == "BankInformation")
							{
//								CustomDesc = "; Company Name:" + entityName +"<br>" +
//									"; Vendor Code:" + entityCode +"<br>";
                                TransactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1616");// "Attachment at Bank Information has been modified.";
							}

							else
							{
                                TransactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1610");// "Attachment has been modified.";
							}
							string strAttachType ="";
							string strOldAttachType ="";

							switch(objAttachmentInfo.ATTACH_TYPE)
							{
								case 11791:
									strAttachType ="Home Photograph";
									break;
								case 11792:
									strAttachType ="Protective Device Certificate";
									break;
								case 11793:
									strAttachType ="Scheduled Articles Photograph";
									break;
								case 11794:
									strAttachType ="Appraisal";
									break;
								case 11795:
									strAttachType ="Bill of Sale";
									break;
								case 11796:
									strAttachType ="Other";
									break;
								case 11933:
									strAttachType ="Supporting Document";
									break;
                                case 14643:
                                    strAttachType = "Risk Survey Requirement";
                                    break;
							}

							switch(objOldAttachmentInfo.ATTACH_TYPE)
							{
								case 11791:
									strOldAttachType ="Home Photograph";
									break;
								case 11792:
									strOldAttachType ="Protective Device Certificate";
									break;
								case 11793:
									strOldAttachType ="Scheduled Articles Photograph";
									break;
								case 11794:
									strOldAttachType ="Appraisal";
									break;
								case 11795:
									strOldAttachType ="Bill of Sale";
									break;
								case 11796:
									strOldAttachType ="Other";
									break;
								case 11933:
									strOldAttachType ="Supporting Document";
									break;
                                case 14643:
                                    strAttachType = "Risk Survey Requirement";
                                    break;
							}

                         

                            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                                CustomerID = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"]);
                            if (Request.QueryString["APP_ID"] != null && Request.QueryString["APP_ID"].ToString() != "")
                                ApplicationID = Convert.ToInt32(Request.QueryString["APP_ID"]);

                            

							if (objOldAttachmentInfo.ATTACH_FILE_DESC != objAttachmentInfo.ATTACH_FILE_DESC && objOldAttachmentInfo.ATTACH_TYPE != objAttachmentInfo.ATTACH_TYPE)
							{
								CustomDesc +=  "; File Description: From :- "  + objOldAttachmentInfo.ATTACH_FILE_DESC +  " To :-" + objAttachmentInfo.ATTACH_FILE_DESC +"<br>"+
									"; Attachment Type: From :-"  + strOldAttachType + " To :-" +  strAttachType;
								Cms.BusinessLayer.BlApplication.ClsGeneralInformation objblapp = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
								objblapp.WriteTransactionLog(CustomerID,ApplicationID,AppVerID,TransactionDescription, int.Parse(GetUserId()), CustomDesc, "Application"); 
								
							}
							else if (objOldAttachmentInfo.ATTACH_FILE_DESC == objAttachmentInfo.ATTACH_FILE_DESC && objOldAttachmentInfo.ATTACH_TYPE != objAttachmentInfo.ATTACH_TYPE)
							{
								CustomDesc +=  "; Attachment Type: From :- "  + strOldAttachType + " To :- " + strAttachType;
								Cms.BusinessLayer.BlApplication.ClsGeneralInformation objblapp = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
								objblapp.WriteTransactionLog(CustomerID,ApplicationID,AppVerID,TransactionDescription, int.Parse(GetUserId()), CustomDesc, "Application"); 
							}
							else if (objOldAttachmentInfo.ATTACH_FILE_DESC != objAttachmentInfo.ATTACH_FILE_DESC && objOldAttachmentInfo.ATTACH_TYPE == objAttachmentInfo.ATTACH_TYPE)
							{
								CustomDesc +=  "; File Description: From :- "  + objOldAttachmentInfo.ATTACH_FILE_DESC +  " To :- " + objAttachmentInfo.ATTACH_FILE_DESC;
								Cms.BusinessLayer.BlApplication.ClsGeneralInformation objblapp = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
								objblapp.WriteTransactionLog(CustomerID,ApplicationID,AppVerID,TransactionDescription, int.Parse(GetUserId()), CustomDesc, "Application"); 
							}
							else if (objOldAttachmentInfo.ATTACH_FILE_DESC == objAttachmentInfo.ATTACH_FILE_DESC && objOldAttachmentInfo.ATTACH_TYPE == objAttachmentInfo.ATTACH_TYPE)
							{
								CustomDesc += "";
							}
							
							lblMessage.Text					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value				= "1";
							hidRowId.Value					=  intAttachID.ToString();
                            rfvATTACH_FILE_NAME.Enabled = false;
						}
						else if (intReturnVal > 0 && strCalledFrom.ToUpper() == "INCLT")
						{
							lblMessage.Text					= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value				= "1";
							hidRowId.Value					=  intAttachID.ToString();
						}
						else
						{
							lblMessage.Text	= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
							hidFormSaved.Value				= "2";
						}
						lblMessage.Visible	= true;
                        hidFormSaved.Value = "1";
					}

			}
			catch(Exception ex)
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"21")
					+ "\n" + ex.Message + "\n Try again!";
				lblMessage.Visible		=	true;
				//Publishing the exception using the static method of Exception manager class
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				
				return;
			}
			finally
			{
				objAttachment.endImpersonation();

				if(objAttachment != null) 
				{
					objAttachment.Dispose();
				}
				
			}
			
			string strCode = "";
			
			//hidFormSaved.Value = "0";

			LoadData(intAttachID);


			//Add script to refresh the correct grid
			if ( Request.QueryString["grid"] == null )
			{
				strCode = @"
				<script>
					if ( this.parent.document.gridObject != null )
					{
						this.parent.document.gridObject.refreshcompletegrid();
						AddData();
					}
				</script>" ;

			}
			else
			{
				strCode = @"
				<script>
				RefreshWebGrid(1," + hidRowId.Value + ")" + 
					"</script>";

			}

			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",
					strCode);
			}
		}

		/// <summary>
		/// Fetch form's value and stores it into variables.
		/// </summary>
		private void GetFormValue()
		{
			//Code for retreiving the forms valus will go here
			if (hidRowId.Value == "New")
			{
				strFileName		=	txtATTACH_FILE_NAME.PostedFile.FileName;
			
				int intIndex	=	strFileName.LastIndexOf("\\");
				strFileName		=	strFileName.Substring(intIndex+1);	//Taking only file name not whole path
				strType			=	txtATTACH_FILE_TYPE.Text;
				if(hidEntityId.Value!="")
					intEntityId		=	int.Parse(hidEntityId.Value);
				strEntityType	=	hidEntity_Type.Value;
			}

			strFileDesc		=	txtATTACH_FILE_DESC.Text;
			intAttachType = int.Parse(cmbATTACH_TYPE.SelectedValue);
						
		}


		/// <summary>
		/// This function is used to save the uploaded file in harddisk
		/// </summary>
		private bool SaveUploadedFile(HtmlInputFile objFile,string intEntityId,int intAttachmentId)
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

				//copying the file
				objFile.PostedFile.SaveAs(strDirName + "\\" + intAttachmentId + "_" + strFileName);

				return true;
			}
			catch (Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				return false;
			}
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
				//CREATE DIRECTORY STRUCTURE FOR CASES OTHER THAN CUSTOMER,APPLICATION AND POLICY
                //if(hidEntity_Type.Value.ToUpper().Trim()!="CUSTOMER" && hidEntity_Type.Value.ToUpper()!="APPLICATION" && hidEntity_Type.Value.ToUpper()!="POLICY" && hidEntity_Type.Value.ToUpper()!="CLAIM")
                //{
                //    //Creating the Attachment folder if not exists
                //    strDirName = strDirName + "\\Attachment";
                //    if (!System.IO.Directory.Exists(strDirName))
                //    {
                //        //Creating the directory
                //        System.IO.Directory.CreateDirectory(strDirName);
                //    }
                //}
				
                ////Creating the login agence code, under attachment if not exists
                //strDirName = strDirName + "\\" + strAgencyCode;
                //if (!System.IO.Directory.Exists(strDirName))
                //{
                //    //Creating the directory
                //    System.IO.Directory.CreateDirectory(strDirName);
                //}

				if (hidEntity_Type.Value.ToUpper().Trim()=="CUSTOMER")
				{

					//For customer , attachment will go into customer directory
					/*strDirName = strDirName + "\\Customer";
					if (!System.IO.Directory.Exists(strDirName))
					{
						//Creating the directory
						System.IO.Directory.CreateDirectory(strDirName);
					}*/

					//For customer , attachment will go into customer directory
					//strDirName = strDirName + "\\" + hidEntityId.Value;
                    strDirName = strDirName + "\\" + strAgencyCode;
                    if (!System.IO.Directory.Exists(strDirName))
                    {
                        //Creating the directory
                        System.IO.Directory.CreateDirectory(strDirName);
                    }
					string strDirSystem=strDirName + "\\" + hidEntityId.Value + "\\System";
					strDirName = strDirName + "\\" + hidEntityId.Value + "\\Attachments";
					
					if (!System.IO.Directory.Exists(strDirName))
					{
						//Creating the directory
						System.IO.Directory.CreateDirectory(strDirName);
					}
					//Create directory structure for System
					if (!System.IO.Directory.Exists(strDirSystem))
					{
						//Creating the directory
						System.IO.Directory.CreateDirectory(strDirSystem);
					}
				}
                    // for itrack no 1225              
                else if (hidEntity_Type.Value.ToUpper().Trim()=="APPLICATION" || hidEntity_Type.Value.ToUpper().Trim()=="POLICY" || hidEntity_Type.Value.ToUpper().Trim()=="CLAIM")
				{

					//For customer , attachment will go into customer directory
					/*strDirName = strDirName + "\\Customer";
					if (!System.IO.Directory.Exists(strDirName))
					{
						//Creating the directory
						System.IO.Directory.CreateDirectory(strDirName);
					}*/

					//For Application , attachment will go into Application directory
					//strDirName = strDirName + "\\" + hidEntity_Type.Value;
                    strDirName = strDirName + "\\" + strAgencyCode;
                    if (!System.IO.Directory.Exists(strDirName))
                    {
                        //Creating the directory
                        System.IO.Directory.CreateDirectory(strDirName);
                    }
					strDirName = strDirName + "\\" + hidEntityId.Value + "\\Attachments";
					if (!System.IO.Directory.Exists(strDirName))
					{
						//Creating the directory
						System.IO.Directory.CreateDirectory(strDirName);
					}
				}


            //    if (hidEntity_Type.Value.ToUpper().Trim() != "CUSTOMER" && hidEntity_Type.Value.ToUpper() != "APPLICATION" && hidEntity_Type.Value.ToUpper() != "POLICY" && hidEntity_Type.Value.ToUpper() != "CLAIM")
               
                else
                {
                    //Creating the Attachment folder if not exists

                 
                    strDirName = strDirName + "\\Attachment";
                    if (!System.IO.Directory.Exists(strDirName))
                    {
                        //Creating the directory
                        System.IO.Directory.CreateDirectory(strDirName);
                    }

                    strDirName = strDirName + "\\" + strAgencyCode;
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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			string strAttachType = "";
			int intAttachID = int.Parse(hidRowId.Value);
			objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();

            strEntityType = hidEntity_Type.Value;
			int CustomerID =0;
			int intUserID=int.Parse(GetUserId());
			if(Request.QueryString["EntityType"] != null)
				strEntityType = Request.QueryString["EntityType"].ToString();

			if(Request.QueryString["EntityName"] != null)
				strEntityType += "~" + Request.QueryString["EntityName"].ToString();

			if(strCalledFrom=="Application")
			{
				ClsAttachmentInfo objAttachmentInfo = new ClsAttachmentInfo();
				objAttachmentInfo = GetFormValues();
				objAttachmentInfo.ATTACH_ID = intAttachID;
				CustomerID=Convert.ToInt32(GetCustomerID());
				objAttachmentInfo.ATTACH_CUSTOMER_ID = CustomerID;
				int modifiedBy =int.Parse(GetUserId());
				//intRetVal = objAttachment.Delete(intAttachID,CustomerID,intUserID,strCalledFrom,"");
				intRetVal = objAttachment.Delete(objAttachmentInfo,modifiedBy,strEntityType);
			}
			else if(strCalledFrom=="Customer")
			{
				CustomerID=Convert.ToInt32(GetCustomerID());
				if(hidCustomInfo.Value=="")
					hidCustomInfo.Value=objAttachment.GetCustomerDetails(CustomerID.ToString()) 
						+ ";File Name = " + hidAttachFileName.Value; 
				
				intRetVal = objAttachment.Delete(intAttachID,CustomerID,intUserID,strCalledFrom,hidCustomInfo.Value);
			}

			else if(strCalledFrom=="InCLT")
			{
				
				ClsAttachmentInfo objAttachmentInfo = new ClsAttachmentInfo();
				objAttachmentInfo = GetFormValues();
				switch(objAttachmentInfo.ATTACH_TYPE)
				{
					case 11791:
						strAttachType ="Home Photograph";
						break;
					case 11792:
						strAttachType ="Protective Device Certificate";
						break;
					case 11793:
						strAttachType ="Scheduled Articles Photograph";
						break;
					case 11794:
						strAttachType ="Appraisal";
						break;
					case 11795:
						strAttachType ="Bill of Sale";
						break;
					case 11796:
						strAttachType ="Other";
						break;
					case 11933:
						strAttachType ="Supporting Document";
						break;
                    case 14643:
                        strAttachType = "Risk Survey Requirement";
                        break;
				}

				if(GetCustomerID()!="")
					CustomerID = Convert.ToInt32(GetCustomerID());
				else
					CustomerID = 0;

				objAttachmentInfo = GetFormValues();
				objAttachmentInfo.ATTACH_ID = intAttachID;
				int modifiedBy =int.Parse(GetUserId());
				if(hidCustomInfo.Value=="")
					hidCustomInfo.Value= ";File Name: " + hidAttachFileName.Value +"<br>"+
						"; File Type: "     + objAttachmentInfo.ATTACH_FILE_TYPE +"<br>"+ 
						"; File Description: " + objAttachmentInfo.ATTACH_FILE_DESC +"<br>"+
						"; Attachment Type: "  + strAttachType; 
				intRetVal = objAttachment.Delete(intAttachID,CustomerID,intUserID,strCalledFrom,hidCustomInfo.Value);
				//intRetVal = objAttachment.Delete(objAttachmentInfo,modifiedBy,strEntityType);
			
			}
			else
			{	
				ClsAttachmentInfo objAttachmentInfo = new ClsAttachmentInfo();
               
                
				//CustomerID = 0;
				//				if(Request.QueryString["EntityID"] != null)
				//					CustomerID = Convert.ToInt32(Request.QueryString["EntityID"].ToString());
				//				
				objAttachmentInfo = GetFormValues();
				switch(objAttachmentInfo.ATTACH_TYPE)
				{
					case 11791:
						strAttachType ="Home Photograph";
						break;
					case 11792:
						strAttachType ="Protective Device Certificate";
						break;
					case 11793:
						strAttachType ="Scheduled Articles Photograph";
						break;
					case 11794:
						strAttachType ="Appraisal";
						break;
					case 11795:
						strAttachType ="Bill of Sale";
						break;
					case 11796:
						strAttachType ="Other";
						break;
					case 11933:
						strAttachType ="Supporting Document";
						break;
                    case 14643:
                        strAttachType = "Risk Survey Requirement";
                        break;
				}

                
				objAttachmentInfo = GetFormValues();
                if (GetPolicyID() != "")
                    objAttachmentInfo.ATTACH_POLICY_ID = Convert.ToInt32(GetPolicyID());
                else
                    objAttachmentInfo.ATTACH_POLICY_ID = 0;
                if (GetPolicyVersionID() != "")
                    objAttachmentInfo.ATTACH_POLICY_VER_TRACKING_ID = Convert.ToInt32(GetPolicyVersionID());
                else
                    objAttachmentInfo.ATTACH_POLICY_VER_TRACKING_ID = 0;
				objAttachmentInfo.ATTACH_ID = intAttachID;
				int modifiedBy =int.Parse(GetUserId());

                if (GetCustomerID() != "")
                {
                    objAttachmentInfo.ATTACH_CUSTOMER_ID = Convert.ToInt32(GetCustomerID());
                }
				if(hidCustomInfo.Value=="")
					hidCustomInfo.Value= ";File Name: " + hidAttachFileName.Value +"<br>"+
						"; File Type: "     + objAttachmentInfo.ATTACH_FILE_TYPE +"<br>"+ 
						"; File Description: " + objAttachmentInfo.ATTACH_FILE_DESC +"<br>"+
						"; Attachment Type: "  + strAttachType; 
				//				intRetVal = objAttachment.Delete(intAttachID,CustomerID,intUserID,strCalledFrom,hidCustomInfo.Value);
				intRetVal = objAttachment.Delete(objAttachmentInfo,modifiedBy,strEntityType);
                hidFormSaved.Value = "1";
			}
			
			if(intRetVal>0)
			{
				lblDelete.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
				hidFormSaved.Value	 =	"5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblDelete.Visible = true;
            hidFormSaved.Value = "1";
			
		}

	}
	
}
