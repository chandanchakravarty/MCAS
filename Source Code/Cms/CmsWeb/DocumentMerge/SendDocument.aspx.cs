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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlClient;
using System.Web.Mail;
using Cms.DataLayer;
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.DocumentMerge
{
	/// <Created By>Deepak Gupta</Created>
	/// <Dated>SEP-14-2006</Dated>
	/// <Purpose>It will be used to gather information regarding the template merge.</Purpose>
	/// Last changed by Shailja for #1161 on 03/06/2007
	public class SendDocument : cmsbase
	{
		#region Declarations
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTemplateType;
		protected System.Web.UI.WebControls.Label lblClientType;
		protected System.Web.UI.WebControls.Label lblApplicationType;
		protected System.Web.UI.WebControls.Label lblPolicyType;
		protected System.Web.UI.WebControls.CheckBox chkClientType;
		protected System.Web.UI.WebControls.CheckBox chkApplicationType;
		protected System.Web.UI.WebControls.CheckBox chkPolicyType;
		protected System.Web.UI.WebControls.Label lblLob;
		protected System.Web.UI.WebControls.Label lblClaimParties;
		protected System.Web.UI.WebControls.Label lblAgency;
		protected System.Web.UI.WebControls.DropDownList ddlLob;
		protected System.Web.UI.WebControls.DropDownList ddlAgency;
		protected System.Web.UI.WebControls.Label LblDocument;
		protected System.Web.UI.WebControls.Label lblATTACHMENT;
		protected System.Web.UI.WebControls.DropDownList ddlDocument;
		protected System.Web.UI.WebControls.Label lblPOLICY_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
		protected System.Web.UI.WebControls.Label lblApplication;
		protected System.Web.UI.WebControls.TextBox txtAPP_NUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnMerge;
		protected System.Web.UI.WebControls.CheckBox chkClaimType;
        protected System.Web.UI.HtmlControls.HtmlButton btnBack;
        protected System.Web.UI.HtmlControls.HtmlButton btnResetMail;
        protected System.Web.UI.HtmlControls.HtmlButton btnCustomerAssistant;
		//protected System.Web.UI.HtmlControls.HtmlButton btnReset;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.HtmlControls.HtmlImage imgCustomer;
		protected System.Web.UI.HtmlControls.HtmlImage imgApplication;
		protected System.Web.UI.HtmlControls.HtmlImage imgPolicy;
		protected System.Web.UI.HtmlControls.HtmlImage imgAddIntrst;
		protected System.Web.UI.HtmlControls.HtmlImage Img1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_POLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMergeId;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidApplication;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidClaim;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoApplicant;
        protected Cms.CmsWeb.WebControls.Menu bottomMenu;
		protected System.Web.UI.WebControls.Label lblClient;
		protected Cms.CmsWeb.Controls.CmsButton btnSaveToSpooler;
		public string strCssClass="";
		protected string strCalledFrom="";
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvReqDocument;
		private ClsDocumentMerge DocMerge;
		private DataSet dsPolicy;
		protected string strCalledFor="";
		protected System.Web.UI.WebControls.Label capDIARY_ITEM_REQ;
		protected System.Web.UI.WebControls.DropDownList cmbDIARY_ITEM_REQ;
		protected System.Web.UI.WebControls.Label capFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.TextBox txtFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFOLLOW_UP_DATE;
        protected System.Web.UI.WebControls.Label lblClaim1;
		protected System.Web.UI.WebControls.Label lblAccount;
		protected System.Web.UI.WebControls.CheckBox chkAccount;
		protected System.Web.UI.WebControls.HyperLink hlkToCheckDate;
		protected System.Web.UI.WebControls.HyperLink hlkFromCheckDate;
		protected System.Web.UI.WebControls.TextBox txtFROM_CHECK_DATE;
		protected System.Web.UI.WebControls.TextBox txtTO_CHECK_DATE;
		protected System.Web.UI.WebControls.Label lblFrom;
		protected System.Web.UI.WebControls.Label lblChkNo;
		protected System.Web.UI.WebControls.Label lblChkNoTo;
		protected System.Web.UI.WebControls.Label lbl;
		protected System.Web.UI.WebControls.Label lblChkNoFrom;
		protected System.Web.UI.WebControls.TextBox txtChkNoFrom;
		protected System.Web.UI.WebControls.TextBox txtChkNoTo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckId;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFromCheckNumber;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToCheckNumber;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTO_CHECK_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_CHECK_DATE;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_NAME;
		protected System.Web.UI.WebControls.Label lblCoApp;
		protected System.Web.UI.WebControls.TextBox txtCO_APPLICANT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvReqCutomer_Name;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvReqClientUndefined;
		protected System.Web.UI.WebControls.RequiredFieldValidator ReqCoAppUndefined;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqPartiesUndefined;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_NUMBER_ID;
		protected System.Web.UI.WebControls.TextBox txtADD_INT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHOLDER_ID;
		protected System.Web.UI.WebControls.Label lblAddIntrst;
		protected System.Web.UI.WebControls.Label lblchkDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHOLDER_ID_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCO_APP_NUMBER_ID;
		protected System.Web.UI.WebControls.Label lblMessage1;
		protected System.Web.UI.WebControls.Label capFROM_NAME;
		protected System.Web.UI.WebControls.TextBox txtFROM_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFROM_NAME;
		protected System.Web.UI.WebControls.Label capFROM_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtFROM_EMAIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFROM_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFROM_EMAIL;
		protected System.Web.UI.WebControls.Label capTO;
		protected System.Web.UI.WebControls.Label capCONTACTDETAILS;
		protected System.Web.UI.WebControls.ListBox cmbCONTACTDETAILS;
		protected System.Web.UI.WebControls.Label capADDITIONAL;
		protected System.Web.UI.WebControls.TextBox txtADDITIONAL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADDITIONAL;
		protected System.Web.UI.WebControls.Button btnSELECT;
		protected System.Web.UI.WebControls.Button btnDESELECT;
		protected System.Web.UI.WebControls.Label capRECIPIENTS1;
		protected System.Web.UI.WebControls.ListBox cmbRECIPIENTS;
		protected System.Web.UI.WebControls.CustomValidator csvRECIPIENTS;
		protected System.Web.UI.WebControls.Label capRECIPIENTS;
		protected System.Web.UI.WebControls.TextBox txtRECIPIENTS;
		protected System.Web.UI.WebControls.Label capSubject;
		protected System.Web.UI.WebControls.TextBox txtSUBJECT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUBJECT;
		protected System.Web.UI.WebControls.Label capMESSAGE;
		protected System.Web.UI.WebControls.TextBox txtMESSAGE;
		protected Cms.CmsWeb.Controls.CmsButton btnSend;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECIPIENTS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected System.Web.UI.WebControls.CheckBox chkMail;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMESSAGE;
		protected System.Web.UI.WebControls.CustomValidator FromDate;
		protected System.Web.UI.WebControls.CustomValidator ToDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		protected System.Web.UI.WebControls.Label capDIARY_ITEM_TO;
		protected System.Web.UI.WebControls.DropDownList cmbDIARY_ITEM_TO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPostBack;
		protected System.Web.UI.WebControls.DropDownList ddlClaimParties;

		protected System.Web.UI.WebControls.Label lblClaim;
		protected System.Web.UI.WebControls.TextBox	txtCLAIM_NUMBER;
		protected System.Web.UI.WebControls.Label lblParties;
		protected System.Web.UI.WebControls.TextBox	txtPARTY_NAME;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.Label capDocument;


		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARTY_ID;
        System.Resources.ResourceManager objResourceMgr;

		protected int intToUserID=0; // holds the value of touserid	
		int			intLoggedInUserID;
		protected bool UpdateGrid=false;
		public const string SUBJECT_DOC_MERGE ="Document Merge Communication";

        private string strServerName = System.Configuration.ConfigurationManager.AppSettings.Get("ServerName").ToString();
		
		
		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>

        
		private void SetErrorMessages()
		{
			revFromCheckNumber.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			revToCheckNumber.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			revFromCheckNumber.ValidationExpression = aRegExpInteger;
			revToCheckNumber.ValidationExpression = aRegExpInteger;

			revFROM_CHECK_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			revTO_CHECK_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");

            revFROM_CHECK_DATE.ValidationExpression = aRegExpDate;		
			revTO_CHECK_DATE.ValidationExpression = aRegExpDate;
            rfvReqDocument.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
            ReqCoAppUndefined.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
           ReqPartiesUndefined.ErrorMessage = Cms .CmsWeb .ClsMessages .GetMessage (base.ScreenId ,"9");
           revFOLLOW_UP_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
         
   
			//Mail Section
			rfvFROM_NAME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvFROM_EMAIL.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvSUBJECT.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			revFROM_EMAIL.ValidationExpression	=	aRegExpEmail;
			revFROM_EMAIL.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");

			revADDITIONAL.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");//"Enter Additional Email.";
			revADDITIONAL.ValidationExpression	=	aRegExpEmail;

			csvRECIPIENTS.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//End Mail section
            rfvReqCutomer_Name.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1349"); 
            rfvReqClientUndefined.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1350"); 
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Setting the cookie value
			SetCookieValue();
			lblMessage.Visible = false;
			chkMail.Enabled = false;
			intLoggedInUserID		=	int.Parse(base.GetUserId());
          capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

          if (Request.QueryString["CalledFor"].ToUpper().ToString() == "CLAIM" || Request.QueryString["CalledFor"].ToUpper().ToString()=="POLICY")
			//if(GetCalledFor()!="CLAIM")
				base.ScreenId = "358";
            else if (Request.QueryString["CalledFor"].ToUpper().ToString() == "MAINTENANCE")
            {
                base.ScreenId = "350_0";
            }

            else

                base.ScreenId = "316_0";


//			if(GetCalledFor()=="CLAIM")
//				base.ScreenId = "316_0";
//
//			else if(GetCalledFor()=="APPLICATION")
//				base.ScreenId ="111";
//			else
//				base.ScreenId ="358";
            
			strCssClass="tableWidth";
			DocMerge = new ClsDocumentMerge();
			bottomMenu.Visible = false;
            objResourceMgr = new System.Resources.ResourceManager("cms.CmsWeb.DocumentMerge.SendDocument", System.Reflection.Assembly.GetExecutingAssembly());
			#region Followup Date
			//hlkFOLLOW_UP_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtFOLLOW_UP_DATE'),document.getElementById('txtFOLLOW_UP_DATE'))"); //Javascript Implementation for Calender		
			hlkFOLLOW_UP_DATE.Attributes.Add("OnClick","fPopCalendar(document.SendDocument.txtFOLLOW_UP_DATE,document.SendDocument.txtFOLLOW_UP_DATE)"); //Javascript Implementation for Calender				
			cmbDIARY_ITEM_REQ.Attributes.Add("Onclick","javascript:return ShowFOLLOW_UP_DATE();");   
			cmbDIARY_ITEM_REQ.Attributes.Add("Onblur","javascript:return ShowFOLLOW_UP_DATE();");   

			#region check date
			hlkFromCheckDate.Attributes.Add("OnClick","fPopCalendar(document.SendDocument.txtFROM_CHECK_DATE,document.SendDocument.txtFROM_CHECK_DATE)"); //Javascript Implementation for Calender				
			hlkToCheckDate.Attributes.Add("OnClick","fPopCalendar(document.SendDocument.txtTO_CHECK_DATE,document.SendDocument.txtTO_CHECK_DATE)"); //Javascript Implementation for Calender				
			#endregion

			#region Validation
			revFOLLOW_UP_DATE.ValidationExpression			=   aRegExpDate;
			revFOLLOW_UP_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            csvFOLLOW_UP_DATE.ErrorMessage                  = "Follow Up Date can not be previous date.";
			rfvFOLLOW_UP_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("42");
        
			#endregion

			#endregion
			SetErrorMessages();
			#region MAIL SEND
			btnSELECT.Attributes.Add("onclick","javascript:selectRecipients();return false;");
			btnDESELECT.Attributes.Add("onclick","javascript:deselectRecipients();return false;");
			btnSend.Attributes.Add("onclick","javascript:return setRecipients();");   
			btnReset.Attributes.Add("onclick","javascript:ResetValues();");

			btnMerge.Attributes.Add("onclick","javascript:disableMailValidators();");   
			
			#endregion

			if (!IsPostBack)
			{
	            if(Request.QueryString["cmbDIARY_ITEM_TO"]!=null)
					intToUserID=int.Parse(Request.QueryString["cmbDIARY_ITEM_TO"].ToString());
				else
					intToUserID=int.Parse(GetUserId().ToString());

				#region Filling Dropdowns
				DataSet DsTemp = new DataSet();
				//Line Of Business
                //DsTemp = DocMerge.GetLineOfBusinesses();
                //ddlLob.DataSource = DsTemp;
                //ddlLob.DataTextField = "LOB_DESC";
                ////ddlLob.DataValueField = "LOB_ID";
                //ddlLob.DataBind();
                //ddlLob.Items.Insert(0,new ListItem("","0"));
                string item = objResourceMgr.GetString("item");

                DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
                ddlLob.DataSource = dtLOBs;
                ddlLob.DataTextField = "LOB_DESC";
                ddlLob.DataValueField = "LOB_ID";
                ddlLob.DataBind();
                this.ddlLob.Items.Insert(0, item);
                ddlLob.Items.Insert(0, new ListItem("", "0"));

           

				//Agency
				DsTemp = new ClsAgency().FillAgency();
				ddlAgency.DataSource = DsTemp;
				ddlAgency.DataTextField = "AGENCY_DISPLAY_NAME";
				ddlAgency.DataValueField = "AGENCY_ID";
				ddlAgency.DataBind();
				ddlAgency.Items.Insert(0,new ListItem("","0"));

				int UserId		=	intToUserID == 0 ? -1 : intToUserID;
				cmbDIARY_ITEM_TO.DataSource		=	ClsCommon.GetUserList();
				cmbDIARY_ITEM_TO.DataTextField	=	USERNAME;
				cmbDIARY_ITEM_TO.DataValueField	=	USERID;
				cmbDIARY_ITEM_TO.DataBind();

				ListItem li=new ListItem(); 
				li=cmbDIARY_ITEM_TO.Items.FindByValue(intToUserID.ToString());   
				if(li!=null)
					li.Selected=true; 

				#endregion
				#region Filling Data
				GetCustomerAppPolicyValues();
				LoadData();
				GetTemplateInfo();
                SetCaptions();
                dropdownyesno();
				#endregion
				//Future Code
				if (Request.QueryString["MERGE_ID"] !=null)
				{
					hidMergeId.Value = Request.QueryString["MERGE_ID"].ToString();
					if(hidMergeId.Value!="" && hidMergeId.Value!="0")
					{
						GetDocLetterDetails(hidMergeId.Value);
					}
				}
				else
					hidMergeId.Value = "-1";
				
				DsTemp = DocMerge.GetTemplates("","0","0");
				if(DsTemp.Tables[0].Rows.Count > 0)
				{
					ddlDocument.DataSource = DsTemp;
					ddlDocument.DataTextField = "DISPLAYNAME";
					ddlDocument.DataValueField = "TEMPLATE_ID";
					ddlDocument.DataBind();
					ddlDocument.Items.Insert(0,new ListItem("",""));
				}
			}

			#region Setting Button Premissions
			btnSaveToSpooler.CmsButtonClass			=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSaveToSpooler.PermissionString		=	gstrSecurityXML;
			btnMerge.CmsButtonClass			=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnMerge.PermissionString		=	gstrSecurityXML;

			btnSend.CmsButtonClass			=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSend.PermissionString		=	gstrSecurityXML;


			btnReset.CmsButtonClass			=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			#endregion
		}
		
		private void SetCookieValue ()
		{
			Response.Cookies["LastVisitedTab"].Value = "0";
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
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
			this.chkClientType.CheckedChanged += new System.EventHandler(this.chkClientType_CheckedChanged);
			this.chkApplicationType.CheckedChanged += new System.EventHandler(this.chkApplicationType_CheckedChanged);
			this.chkPolicyType.CheckedChanged += new System.EventHandler(this.chkPolicyType_CheckedChanged);
			this.chkClaimType.CheckedChanged += new System.EventHandler(this.chkClaimType_CheckedChanged);
			this.chkAccount.CheckedChanged += new System.EventHandler(this.chkAccount_CheckedChanged);
			this.ddlLob.SelectedIndexChanged += new System.EventHandler(this.ddlLob_SelectedIndexChanged);
			this.ddlAgency.SelectedIndexChanged += new System.EventHandler(this.ddlAgency_SelectedIndexChanged);
			this.btnSaveToSpooler.Click += new System.EventHandler(this.btnSaveToSpooler_Click);
			this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void chkClientType_CheckedChanged(object sender, System.EventArgs e)
		{
			string CurrentTemplateId = ddlDocument.SelectedValue.ToString();
			ddlDocument.Items.Clear();
			
			string LETTERTYPE = "";
			string LOB = "0";
			string AGENCY = "0";

			if (chkClientType.Checked)
				LETTERTYPE = "CL";
			if (chkApplicationType.Checked && LETTERTYPE.Trim()=="")
				LETTERTYPE = "APP";
			else if (chkApplicationType.Checked && LETTERTYPE.Trim()!="")
				LETTERTYPE = LETTERTYPE + "','APP";
			if (chkPolicyType.Checked && LETTERTYPE.Trim()=="")
				LETTERTYPE = "POL";
			else if (chkPolicyType.Checked && LETTERTYPE.Trim()!="")
				LETTERTYPE = LETTERTYPE + "','POL";

			if (chkClaimType.Checked && LETTERTYPE.Trim()=="")
				LETTERTYPE = "CLM";
			else if (chkClaimType.Checked && LETTERTYPE.Trim()!="")
				LETTERTYPE = LETTERTYPE + "','CLM";


			//Added for Account type:
			if (chkAccount.Checked)
			{
				ddlLob.SelectedIndex =-1;
				ddlAgency.SelectedIndex =-1;
				LETTERTYPE = "ACT";
			}

            if (ddlLob.SelectedIndex == 1)
                LOB = "";
	        else
			LOB = ddlLob.SelectedValue.ToString();
			if(LOB=="") 
				LOB ="0";
			AGENCY = ddlAgency.SelectedValue.ToString();
			if(AGENCY=="")
				AGENCY= "0";

			DataSet DsTemp = new DataSet();
			DsTemp = DocMerge.GetTemplates(LETTERTYPE,LOB,AGENCY);
			if(DsTemp.Tables[0].Rows.Count > 0)
			{
				ddlDocument.DataSource = DsTemp;
				ddlDocument.DataTextField = "DISPLAYNAME";
				ddlDocument.DataValueField = "TEMPLATE_ID";
				ddlDocument.DataBind();
				ddlDocument.Items.Insert(0,new ListItem("",""));
			}
			else
			{
				ddlDocument.Items.Insert(0,new ListItem("",""));
			}

			ListItem lstItem  = ddlDocument.Items.FindByValue(CurrentTemplateId);
			if(lstItem !=null )
			{
				ddlDocument.SelectedIndex =-1;
				lstItem.Selected=true;
			}
		}
		private void chkApplicationType_CheckedChanged(object sender, System.EventArgs e)
		{
			chkClientType_CheckedChanged(null,null);			
		}
		private void chkPolicyType_CheckedChanged(object sender, System.EventArgs e)
		{
            chkClientType_CheckedChanged(null,null);			
		}
		private void chkClaimType_CheckedChanged(object sender, System.EventArgs e)
		{
			chkClientType_CheckedChanged(null,null);			
		}
		private void ddlLob_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			chkClientType_CheckedChanged(null,null);			
		}
		private void ddlAgency_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			chkClientType_CheckedChanged(null,null);
			LoadData();			
		}
		private void btnSaveToSpooler_Click(object sender, System.EventArgs e)
		{			
			SaveSendDocument("SS");
            if (hidMergeId.Value != "" && hidMergeId.Value != "0")
            {
                GetDocLetterDetails(hidMergeId.Value);
            }
		}
		private void btnMerge_Click(object sender, System.EventArgs e)
		{
			if(chkAccount.Checked == true)
			{
				SaveAccountSendDoc("S");
				chkMail.Checked = false; //Multiple Merge do not have Email Option:
				chkMail.Enabled = false;
			}
			else
			{
				SaveSendDocument("S");
				GetApplicantEMail();
				chkMail.Visible =true; //Single Merge Email Option
				chkMail.Enabled = true;
				chkMail.Checked = false;
			}

			//Response.Redirect("TemplateLoad.aspx?MODE=MERGE&MergeId=" + hidMergeId.Value.ToString().Trim());
			
			string strScript="TemplateLoad.aspx?MODE=MERGE&MergeId=" + hidMergeId.Value.ToString().Trim();			
			if(hidMergeId.Value.ToString() != "-1")	
			{
				//Modified to Display Send Document Screen after Merging:
				ClientScript.RegisterStartupScript(this.GetType(),"script","<script>CallUrl('" + strScript + "')</script>");
				chkMail.Enabled = true;
				
			}
			else
			{
				lblMessage.Visible = true;
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13"); //"No Data found in Search creteria.\n Select at least one search criteria from CHECK DATE and CHECK NUMBER.";
                
			}
            GetDocLetterDetails(hidMergeId.Value);
		}
		private void SaveSendDocument(string MergeStatus)
		{
			try
			{
				string[] strMergeInfo = new string[17];
				strMergeInfo[0] = hidMergeId.Value.ToString().Trim();
				strMergeInfo[1] = ddlDocument.SelectedValue.ToString().Trim();
				strMergeInfo[2] = hidCUSTOMER_ID.Value.ToString().Trim();
				strMergeInfo[3] = hidAPP_ID.Value.ToString().Trim();
				strMergeInfo[4] = hidAPP_VERSION_ID.Value.ToString().Trim();
				strMergeInfo[5] = hidPOLICY_ID.Value.ToString().Trim();
				strMergeInfo[6] = hidPOLICY_VERSION_ID.Value.ToString().Trim();
				//IF claim is selected the System will have Claim Policy Version Id. It could be different form the Policy version ID.
				if(hidCLAIM_POLICY_VERSION_ID.Value!="" && hidCLAIM_POLICY_VERSION_ID.Value!="0")
					strMergeInfo[6] = hidCLAIM_POLICY_VERSION_ID.Value.ToString().Trim();
			
				strMergeInfo[7] = GetUserId().ToString().Trim().Trim();
				strMergeInfo[8] = MergeStatus.Trim();
				//Following code added by Shailja for Diary Item Creation(#1095)
				strMergeInfo[9] = cmbDIARY_ITEM_REQ.SelectedValue;
				if(cmbDIARY_ITEM_REQ.SelectedValue == "1")
				{
					strMergeInfo[10] = txtFOLLOW_UP_DATE.Text;
					strMergeInfo[14] = cmbDIARY_ITEM_TO.SelectedValue;  
				}

				strMergeInfo[11] = "0"; //0 in case of Non Account Merge
				if(hidCO_APP_NUMBER_ID.Value.ToString().Trim()!="")
					strMergeInfo[12] = hidCO_APP_NUMBER_ID.Value.ToString().Trim(); //Applicant ID
				else
					strMergeInfo[12] = "0";

				if(hidHOLDER_ID.Value.ToString().Trim()!="")
					strMergeInfo[13] = hidHOLDER_ID.Value.ToString().Trim(); //Holder_ID
				else
					strMergeInfo[13] = "0";


				//Claim Merge Info with Parties
				strMergeInfo[15]  = "0";
				strMergeInfo[16]  = "0";
				if(hidCLAIM_ID.Value!="")
					strMergeInfo[15]  = hidCLAIM_ID.Value;
			
				if(hidPARTY_ID.Value!="")
					strMergeInfo[16] = hidPARTY_ID.Value.ToString().Trim();

				hidMergeId.Value = DocMerge.InsertUpdateSendDocument(strMergeInfo);
            			
				lblMessage.Visible = true;
                if (hidMergeId.Value.ToString().Trim() != "-1")
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1351"); //"Send document has been saved.";//sneha
                    UpdateGrid = true;
                    //Disable Second Time Merge
                    //btnMerge.Enabled = false;
                    //btnSaveToSpooler.Enabled = false;
                    btnReset.Enabled = false;

                }
                else
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1352"); //"Some error occured while saving the document.";
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish (objExp);
			}

	}
        private void SetCaptions()
        {
            lblClient.Text = objResourceMgr.GetString("txtCUSTOMER_NAME");
            lblApplicationType.Text = objResourceMgr.GetString("chkApplicationType");
            lblPolicyType.Text = objResourceMgr.GetString("chkPolicyType");
            //lblTemplateType.Text = objResourceMgr.GetString("ddlTemplateType");
            lblLob.Text = objResourceMgr.GetString("ddlLob");
            lblClaimParties.Text = objResourceMgr.GetString("ddlClaimParties");
            lblAgency.Text = objResourceMgr.GetString("ddlAgency");
            //lblATTACHMENT.Text = objResourceMgr.GetString("lblATTACHMENT");
            LblDocument.Text = objResourceMgr.GetString("ddlDocument");
            lblPOLICY_NUMBER.Text = objResourceMgr.GetString("txtPOLICY_NUMBER");
            lblApplication.Text = objResourceMgr.GetString("txtApp_Num");
            lblAccount.Text = objResourceMgr.GetString("chkAccount");
            lblClaim1.Text = objResourceMgr.GetString("chkClaimType");
            lblClientType.Text = objResourceMgr.GetString("chkClientType");
            lblCoApp.Text = objResourceMgr.GetString("txtCO_APPLICANT");
            lblClaim.Text = objResourceMgr.GetString("txtCLAIM_NUMBER");
            lblParties.Text = objResourceMgr.GetString("txtPARTY_NAME");
            lblAddIntrst.Text = objResourceMgr.GetString("txtADD_INT");
            capDIARY_ITEM_REQ.Text = objResourceMgr.GetString("cmbDIARY_ITEM_REQ");
            capFOLLOW_UP_DATE.Text = objResourceMgr.GetString("capFOLLOW_UP_DATE");
            capDIARY_ITEM_TO.Text = objResourceMgr.GetString("cmbDIARY_ITEM_TO");
            lblchkDate.Text = objResourceMgr.GetString("lblchkDate");
            lblFrom.Text = objResourceMgr.GetString("txtFROM_CHECK_DATE");
            lblChkNoFrom.Text = objResourceMgr.GetString("txtChkNoFrom");
            lblChkNoTo.Text = objResourceMgr.GetString("txtChkNoTo");
            lblTemplateType.Text = objResourceMgr.GetString("lblTemplateType"); 
            chkMail.Text = objResourceMgr.GetString("chkMail");  
            capDocument.Text = objResourceMgr.GetString("capDocument"); 
            btnMerge.Text = objResourceMgr.GetString("btnMerge"); 
            hidCustomer.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1395");
            hidApplication.Value= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            hidClaim.Value= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            hidCoApplicant.Value= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
            hidPolicy.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");
            lblChkNo.Text = objResourceMgr.GetString("lblChkNo");
            capFROM_NAME.Text = objResourceMgr.GetString("capFROM_NAME");
            capFROM_EMAIL.Text = objResourceMgr.GetString("capFROM_EMAIL");
            capTO.Text = objResourceMgr.GetString("capTO");
            capRECIPIENTS.Text = objResourceMgr.GetString("capRECIPIENTS");
            capSubject.Text = objResourceMgr.GetString("capSubject");
            capMESSAGE.Text = objResourceMgr.GetString("capMESSAGE");
            capCONTACTDETAILS.Text = objResourceMgr.GetString("capCONTACTDETAILS");
            capADDITIONAL.Text = objResourceMgr.GetString("capADDITIONAL");
            capRECIPIENTS1.Text = objResourceMgr.GetString("capRECIPIENTS1");
            lbl.Text = objResourceMgr.GetString("lbl");
           
        }
        private void dropdownyesno()
        {

            cmbDIARY_ITEM_REQ.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbDIARY_ITEM_REQ.DataTextField = "LookupDesc";
            cmbDIARY_ITEM_REQ.DataValueField = "LookupCode";
            cmbDIARY_ITEM_REQ.DataBind();
        }
		#region ACCOUNT DOC DATA
		private void SaveAccountSendDoc(string MergeStatus)
		{
			try
			{
				string strSQL;
				DataSet dsAcct = new DataSet();
				//Params for Acct data
				string strDateFrm = txtFROM_CHECK_DATE.Text.ToString().Trim();
				string strDateTo = txtTO_CHECK_DATE.Text.ToString().Trim();
				string strChkFrm = txtChkNoFrom.Text.ToString().Trim();
				string strChkTo = txtChkNoTo.Text.ToString().Trim();
				string strMerge_Ids = "";
			

				strSQL="Proc_DOC_GetAccountMergeInfo '" + strDateFrm + "'," + "'" + strDateTo + "'," + "'" +strChkFrm + "'," + "'" + strChkTo + "'";
				dsAcct=DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,strSQL);

				if(dsAcct.Tables[0].Rows.Count>0)
				{
					foreach (DataRow Row in dsAcct.Tables[0].Rows)
					{
						string[] strMergeInfo = new string[15];
						//strMergeInfo[0] = hidMergeId.Value.ToString().Trim();
						strMergeInfo[0] = "-1";
						strMergeInfo[1] = ddlDocument.SelectedValue.ToString().Trim();
						strMergeInfo[2] = hidCUSTOMER_ID.Value.ToString().Trim();
						strMergeInfo[3] = hidAPP_ID.Value.ToString().Trim();
						strMergeInfo[4] = hidAPP_VERSION_ID.Value.ToString().Trim();
						strMergeInfo[5] = hidPOLICY_ID.Value.ToString().Trim();
						strMergeInfo[6] = hidPOLICY_VERSION_ID.Value.ToString().Trim();
						strMergeInfo[7] = GetUserId().ToString().Trim().Trim();
						strMergeInfo[8] = MergeStatus.Trim();
						//Following code added by Shailja for Diary Item Creation(#1095)
						strMergeInfo[9] = cmbDIARY_ITEM_REQ.SelectedValue;
						if(cmbDIARY_ITEM_REQ.SelectedValue == "1")
						{
							strMergeInfo[10] = txtFOLLOW_UP_DATE.Text;
							strMergeInfo[14] = cmbDIARY_ITEM_TO.SelectedValue;  
						}

						strMergeInfo[11] = Row["CHECK_ID"].ToString().Trim();

						strMergeInfo[12] = "0"; //Applicant ID 0 in case of Acct
						strMergeInfo[13] = "0"; //Holder_ID 0 in case of Acct


						hidMergeId.Value = DocMerge.InsertUpdateSendDocument(strMergeInfo);
						strMerge_Ids = strMerge_Ids + "," + hidMergeId.Value.ToString();
						

					}
				}
				else
				{
					hidMergeId.Value="-1";
				}

				//Sending Multiple MergeIDs CSV
				if(hidMergeId.Value!="-1" && strMerge_Ids.ToString() !="")
					hidMergeId.Value = strMerge_Ids.ToString().Substring(1);
			

				lblMessage.Visible = true;
                if (hidMergeId.Value.ToString().Trim() != "-1")
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1351");// "Send document has been saved."; //sneha
                    // Send Follow Up Entry during Merge  : 16 Sep 2008
                    if (cmbDIARY_ITEM_REQ.SelectedValue == "1")
                    {
                        string[] strDiaryInfo = new string[8];
                        strDiaryInfo[0] = txtFOLLOW_UP_DATE.Text;
                        strDiaryInfo[1] = cmbDIARY_ITEM_TO.SelectedValue;
                        strDiaryInfo[2] = hidCUSTOMER_ID.Value.ToString().Trim();
                        strDiaryInfo[3] = hidAPP_ID.Value.ToString().Trim();
                        strDiaryInfo[4] = hidAPP_VERSION_ID.Value.ToString().Trim();
                        strDiaryInfo[5] = hidPOLICY_ID.Value.ToString().Trim();
                        strDiaryInfo[6] = hidPOLICY_VERSION_ID.Value.ToString().Trim();
                        strDiaryInfo[7] = GetUserId().ToString().Trim().Trim();
                        string strSubject = SUBJECT_DOC_MERGE;
                        string strMessage = "";

                        DocMerge.Diary(strSubject, strMessage, strDiaryInfo, intLoggedInUserID);

                    }
                }
                else
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1352");//"Some error occured while saving the document.";//sneha
			}
			catch(Exception objExp)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish (objExp);                
			}

	

		}
		#endregion
		#region Filling Data CALLING FUNCTIONS
		private void GetCustomerAppPolicyValues()
		{
			strCalledFor				= Request["CalledFor"]+"";
			hidCalledFor.Value			= strCalledFor;

            hidCUSTOMER_ID.Value 		= GetCustomerID();
			if (hidCUSTOMER_ID.Value=="") hidCUSTOMER_ID.Value="0";
			
			hidAPP_ID.Value				= GetAppID(); 
			if (hidAPP_ID.Value=="") hidAPP_ID.Value="0";
				hidAPP_VERSION_ID.Value		= GetAppVersionID();

			if (hidAPP_VERSION_ID.Value=="") hidAPP_VERSION_ID.Value="0";
			
			if(strCalledFor.Trim()=="POLICY")
			{
				hidPOLICY_ID.Value			= GetPolicyID();
				if (hidPOLICY_ID.Value=="") hidPOLICY_ID.Value="0";
				hidPOLICY_VERSION_ID.Value	= GetPolicyVersionID();
				if (hidPOLICY_VERSION_ID.Value=="") hidPOLICY_VERSION_ID.Value="0";
			}
			if(strCalledFor.Trim() != "" || strCalledFor == "CUSTOMER" )
			{
				hidAPP_LOB.Value = GetLOBID();
				
			}
			if (hidAPP_LOB.Value=="") hidAPP_LOB.Value="0";

			if(hidCO_APP_NUMBER_ID.Value == "") hidCO_APP_NUMBER_ID.Value = "0";
			if(hidHOLDER_ID.Value == "") hidHOLDER_ID.Value = "0";
		}

		private void GetTemplateInfo()
		{
			//Default Values
			int intTemplateID = 0;
			int letterType = 0; //CUST/APP/POL/ACCT
			try
			{
				if(Request.QueryString["TEMPLATE_ID"]!=null)
				{
					intTemplateID=int.Parse(Request.QueryString["TEMPLATE_ID"].ToString());
					if(Request.QueryString["LETTERTYPE"]!=null)
					{
						/*	<option value=""></option>
						<option value="14126">Account</option>
						<option selected="selected" value="11939">Application</option>
						<option value="11938">Customer</option>
						<option value="11940">Policy</option>*/
						letterType = int.Parse(Request.QueryString["LETTERTYPE"].ToString());
						switch(letterType)
						{
							case 14126:
								chkAccount.Checked = true;
								chkAccount_CheckedChanged(null,null);
								break;
							case 11939:
								chkApplicationType.Checked = true;
								chkApplicationType_CheckedChanged(null,null);
								break;
							case 11938:
								chkClientType.Checked = true;
								chkClientType_CheckedChanged(null,null);
								break;
							case 11940:
								chkPolicyType.Checked = true;
								chkPolicyType_CheckedChanged(null,null);
								break;
							case 11200:
								chkClaimType.Checked = true;
								chkClaimType_CheckedChanged(null,null);	
								break;
							default:
                                break;
						}
					}
					ddlDocument.SelectedValue = intTemplateID.ToString();
					//Disable Buttons
					btnMerge.Visible = false;
					btnSaveToSpooler.Visible = false;
					btnReset.Visible = false;
					
					
				}
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message.ToString();
			}
		}
		private void LoadData()
		{		
			try
			{
				//For Claims 
				//if(GetCalledFor()=="CLAIM")
				if(strCalledFor=="CLAIM")
				{
					if(GetClaimID()!="")
					{
						chkClaimType.Checked = true;
						chkClientType_CheckedChanged(null,null);

						DataSet dsclaim = new DataSet();
						dsclaim = DocMerge.GetClaimInfoDocMerge(int.Parse(GetClaimID()==""?"0": GetClaimID()));
						if(dsclaim!=null)
						{
							if (dsclaim.Tables[0].Rows.Count>0)
							{
								//Set Hidden Varibles 
								hidCLAIM_ID.Value = dsclaim.Tables[0].Rows[0]["CLAIM_ID"].ToString();
								hidAPP_ID.Value = dsclaim.Tables[0].Rows[0]["APP_ID"].ToString();
								hidAPP_VERSION_ID.Value =dsclaim.Tables[0].Rows[0]["APP_VERSION_ID"].ToString();
								hidPOLICY_ID.Value = dsclaim.Tables[0].Rows[0]["POLICY_ID"].ToString();
								hidPOLICY_VERSION_ID.Value = dsclaim.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
								                            
								//Set Form Values
								txtCUSTOMER_NAME.Text = dsclaim.Tables[0].Rows[0]["CUSTOMERNAME"].ToString();						
								txtPOLICY_NUMBER.Text = dsclaim.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
								txtAPP_NUMBER.Text = dsclaim.Tables[0].Rows[0]["APP_NUMBER"].ToString();
								txtCLAIM_NUMBER.Text = dsclaim.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();


							}
						}
					}
				}
				else
				{
					if (strCalledFor=="" ||strCalledFor=="0")
						return;
					dsPolicy = new DataSet();
					dsPolicy = DocMerge.GetCustomerAppPolicyValues(int.Parse(hidCUSTOMER_ID.Value==""?"0": hidCUSTOMER_ID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value), int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value), int.Parse(hidAPP_ID.Value==""?"0":hidAPP_ID.Value), int.Parse(hidAPP_VERSION_ID.Value==""?"0":hidAPP_VERSION_ID.Value), strCalledFor);			
				
					if (dsPolicy!=null && dsPolicy.Tables[0].Rows.Count>0)
					{
						txtCUSTOMER_NAME.Text = dsPolicy.Tables[0].Rows[0]["CUSTOMERNAME"].ToString();						
						txtPOLICY_NUMBER.Text = dsPolicy.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
						txtAPP_NUMBER.Text = dsPolicy.Tables[0].Rows[0]["APP_NUMBER"].ToString();
						string strLOB = dsPolicy.Tables[0].Rows[0]["LOB"].ToString();
				
						if(strLOB!="0")
							ddlLob.SelectedIndex = ddlLob.Items.IndexOf(ddlLob.Items.FindByValue(strLOB));
						
						if (strCalledFor.Trim()=="CUSTOMER")
						{
							chkClientType.Checked=true;
						}
						if(strCalledFor.Trim()=="APPLICATION")
						{
							chkClientType.Checked=true;
							chkApplicationType.Checked=true;
						}
						if(strCalledFor.Trim()=="POLICY")
						{
							chkClientType.Checked=true;
							chkApplicationType.Checked=true;
							chkPolicyType.Checked=true;
						}
				
						chkClientType_CheckedChanged(null,null);
					}
				}
			}
			catch( Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish (objExp);
			}
			finally
			{
				
			}
		}

		/// <summary>
		/// //loadAgency();Removed Agency Issue 2228 : Agency Will not be Default at Cust,App and Pol. (To get Document List)
		/// </summary>
		private void loadAgency()
		{
			if (strCalledFor=="" ||strCalledFor=="0")
				return;

			string strAgency = "0";
			if(dsPolicy.Tables[0].Rows.Count > 0)
			{
				strAgency = dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString();
			}
			if(strAgency!="0")
				ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue(strAgency));
			if (strCalledFor.Trim()=="CUSTOMER")
			{
				strAgency = ddlAgency.SelectedValue.ToString();
				DefaultAgency(strAgency);

			}
			if(strCalledFor.Trim()=="APPLICATION")
			{
				strAgency = ddlAgency.SelectedValue.ToString();
				DefaultAgency(strAgency);
			}
			if(strCalledFor.Trim()=="POLICY")
			{
				strAgency = ddlAgency.SelectedValue.ToString();
				DefaultAgency(strAgency);
			}
			chkClientType_CheckedChanged(null,null);
	
		}

		
		#endregion
		private void DefaultAgency(string strAgency)
		{
			if(strAgency != "0")
				ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue(strAgency));
			else
				ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("0"));


		}

		private void chkAccount_CheckedChanged(object sender, System.EventArgs e)
		{
		   chkClientType_CheckedChanged(null,null);
				chkMail.Checked = false;
			
		}
		#region Get MailIDs
		private void GetApplicantEMail()
		{
			ClsCustomer ojCustomer;
			ojCustomer			=	new ClsCustomer();
			int CustId			=	Convert.ToInt32(hidCUSTOMER_ID.Value.ToString().Trim());
						
			cmbCONTACTDETAILS.DataSource  		=	ojCustomer.FetchApplicantEMailAddressDocMerge(CustId,int.Parse(hidPOLICY_ID.Value.ToString()),int.Parse(hidPOLICY_VERSION_ID.Value.ToString()));
			cmbCONTACTDETAILS.DataTextField ="CUSTOMER_NAME";
			cmbCONTACTDETAILS.DataValueField ="CUSTOMER_Email"; 
			cmbCONTACTDETAILS.DataBind(); 
			
		}
		#endregion
		private void btnSend_Click(object sender, System.EventArgs e)
		{
			//string strTo			=		"aa";//txtTO.Text.Trim().Replace("'","''");
			string strFrom			=		txtFROM_EMAIL.Text.Trim().Replace("'","''");
			string strSubject		=		txtSUBJECT.Text.Trim().Replace("'","''");
			string strMessage		=		txtMESSAGE.Text.Trim().Replace("'","''");			
			
			string recipient=(string)hidRECIPIENTS.Value;
			if (recipient !="" && recipient != "0")
			{
				string[] recipients= recipient.Split(',');  
				recipient="";
				for (int i=0;i <recipients.GetLength(0)-1 ;i++)
				{
					recipient=recipient + recipients[i].ToString()  + ","; 	
				}
				recipient = recipient.Substring(0,recipient.LastIndexOf(","));
			}			
			if (recipient =="0" ) 
				recipient="";	
			if (SendEmail(strFrom,strSubject,recipient,strMessage)==false)
			{
                lblMessage1.Text += Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");//"\n Email can not be sent.";
				lblMessage1.Visible=true;
				return;
			}
			else
			{
                lblMessage1.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1353"); //"Email has been Sent.";
				lblMessage1.Visible=true;
				//Diary Entry Will Send on Merge Not on Email Send : 16 Sep 2008
				/*if(cmbDIARY_ITEM_REQ.SelectedValue == "Y")
				{
					string[] strMergeInfo = new string[8];
					strMergeInfo[0] = txtFOLLOW_UP_DATE.Text;
					strMergeInfo[1] = cmbDIARY_ITEM_TO.SelectedValue; 
					strMergeInfo[2] = hidCUSTOMER_ID.Value.ToString().Trim();
					strMergeInfo[3] = hidAPP_ID.Value.ToString().Trim();
					strMergeInfo[4] = hidAPP_VERSION_ID.Value.ToString().Trim();
					strMergeInfo[5] = hidPOLICY_ID.Value.ToString().Trim();
					strMergeInfo[6] = hidPOLICY_VERSION_ID.Value.ToString().Trim();
					strMergeInfo[7] = GetUserId().ToString().Trim().Trim();

					DocMerge.Diary(strFrom,strSubject,strMessage,strMergeInfo,intLoggedInUserID);
					
				}*/
				//Diary Entry Will go only on Merge.
				//saveTransactionLog();
			}
			//Mail Option
			chkMail.Enabled = true;

			
		}
		
		private void saveTransactionLog()
		{
			Cms.DataLayer.DataWrapper objDataWrapper = new Cms.DataLayer.DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				Cms.Model.Client.ClsEmailInfo objMailInfo=new Cms.Model.Client.ClsEmailInfo();	
				objMailInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value.ToString().Trim());
				objMailInfo.CREATED_BY=int.Parse(GetUserId());
				objMailInfo.EMAIL_FROM_NAME=txtFROM_EMAIL.Text;
				objMailInfo.POLICY_NUMBER=txtPOLICY_NUMBER.Text;
				objMailInfo.APP_NUMBER=txtAPP_NUMBER.Text;

				string recipient=(string)hidRECIPIENTS.Value;
				if (recipient !="" && recipient != "0")
				{
					string[] recipients= recipient.Split(',');  
					recipient="";
					for (int i=0;i <recipients.GetLength(0)-1 ;i++)
					{
						recipient=recipient + recipients[i].ToString()  + ","; 	
					}
					recipient = recipient.Substring(0,recipient.LastIndexOf(","));
				}			
				if (recipient =="0" ) 
					recipient="";			
				objMailInfo.EMAIL_RECIPIENTS=recipient;
				objMailInfo.EMAIL_SUBJECT=txtSUBJECT.Text;
				objMailInfo.EMAIL_MESSAGE=txtMESSAGE.Text;
				objMailInfo.EMAIL_SEND_DATE=DateTime.Now;
				objMailInfo.DIARY_ITEM_REQ =cmbDIARY_ITEM_REQ.SelectedValue;
				if (cmbDIARY_ITEM_REQ.SelectedValue == "1")
				{
					objMailInfo.FOLLOW_UP_DATE= Convert.ToDateTime(txtFOLLOW_UP_DATE.Text);    
					objMailInfo.DIARY_ITEM_TO = int.Parse(cmbDIARY_ITEM_TO.SelectedValue);  

				}
				//int returnResult = 0;
				objMailInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"client\Aspx\Email.aspx.resx");
				Cms.DataLayer.SqlUpdateBuilder objBuilder = new Cms.DataLayer.SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objMailInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objMailInfo.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Email has been Sent From " + objMailInfo.EMAIL_FROM_NAME + " To " + objMailInfo.EMAIL_RECIPIENTS;					
				objTransactionInfo.APP_ID			=	int.Parse(hidAPP_ID.Value.ToString().Trim());
				objTransactionInfo.POLICY_ID		=	int.Parse(hidPOLICY_ID.Value.ToString().Trim());
				objTransactionInfo.POLICY_VER_TRACKING_ID	=	int.Parse(hidPOLICY_VERSION_ID.Value.ToString().Trim());
				objTransactionInfo.APP_VERSION_ID	=	int.Parse(hidAPP_VERSION_ID.Value.ToString().Trim());
				objTransactionInfo.CLIENT_ID		=	objMailInfo.CUSTOMER_ID;
				objTransactionInfo.CHANGE_XML		=	strTranXML;					
				objTransactionInfo.CUSTOM_INFO		=	";Recipients = " + objMailInfo.EMAIL_RECIPIENTS + ";Subject = " + objMailInfo.EMAIL_SUBJECT;
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			}
			catch( Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish (objExp);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		#region GetData
		private void GetDocLetterDetails(string mergeId)
		{
			DataSet dstemp = new DataSet();

			try
			{
				dstemp = DocMerge.GetDocLetterDetails(int.Parse(mergeId));			
				if (dstemp!=null && dstemp.Tables[0].Rows.Count>0)
				{
					txtCUSTOMER_NAME.Text = dstemp.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();						
					txtAPP_NUMBER.Text = dstemp.Tables[0].Rows[0]["APP_NUMBER"].ToString();
					txtPOLICY_NUMBER.Text = dstemp.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
					txtCO_APPLICANT.Text =  dstemp.Tables[0].Rows[0]["CO_APPLICANT_NAME"].ToString();
					string strLOB_ID = dstemp.Tables[0].Rows[0]["LOB_ID"].ToString();
					string strAgencyId =  dstemp.Tables[0].Rows[0]["AGENCY_ID"].ToString();
					string strDocPath = dstemp.Tables[0].Rows[0]["TEMPLATE_PATH"].ToString();
					
					if(strLOB_ID!="0")
						ddlLob.SelectedIndex = ddlLob.Items.IndexOf(ddlLob.Items.FindByValue(strLOB_ID));

					if(strLOB_ID!="0")
						ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue(strLOB_ID));

					if(strDocPath!="")
					{
						string filename = "Document";
						lblATTACHMENT.Visible = true;
						//Added for Security Implementations in Document Merge(Impersonaisation)
						string uploadURL = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString();
						string filesource = uploadURL + strDocPath;
						int startOfFile = filesource.IndexOf("Upload");
						string filePath = filesource.Substring(startOfFile + 6);
						string []fileURL = filePath.Split('.'); 
						string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
						string path = "<a href='" + EncryptedPath + "' target='blank'>" + filename + "</a>";
						lblATTACHMENT.Text = path;						

					}

					string diaryItem = dstemp.Tables[0].Rows[0]["DIARY_ITEM_REQ"].ToString();
					if(diaryItem!="")
						cmbDIARY_ITEM_REQ.SelectedValue = diaryItem;

                    //Modfied on 11 June 2009 -kasana
					//Fill both the Fields when Diary is Set to YES
					if(diaryItem!="" && diaryItem!="0")
					{
						cmbDIARY_ITEM_TO.SelectedValue =  dstemp.Tables[0].Rows[0]["DIARY_ITEM_TO"].ToString();//Done for Itrack Issue 4846 on 4 June 2009
						txtFOLLOW_UP_DATE.Text = dstemp.Tables[0].Rows[0]["FOLLOW_UP_DATE"].ToString();
					}
                   

					if(dstemp.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString()!="")
						txtCLAIM_NUMBER.Text = dstemp.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();

					if(dstemp.Tables[0].Rows[0]["PARTY_NAME"].ToString()!="")
						txtPARTY_NAME.Text = dstemp.Tables[0].Rows[0]["PARTY_NAME"].ToString();
                   
				}

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}
			finally
			{
				dstemp = null;
			}
		}
		#endregion	
		#region Send Email
		/// <summary>
		/// This function is used to send email if password found
		/// </summary>
		/// <param name="strEmail">Email Id of the User</param>
		/// <returns></returns>
		public bool SendEmail(string strFromEmail, string strSubject, string strRecipients, string strMessage)
		{
			try
			{
				
			
				MailMessage objMailMessage =  new MailMessage();
				
				objMailMessage.To				=	strRecipients;
				objMailMessage.From				=	strFromEmail;
				objMailMessage.BodyFormat		=	MailFormat.Html;
				objMailMessage.Subject			=	strSubject;
				objMailMessage.Priority			=	MailPriority.Normal;
				//get File Attachment 
				string strFilePath = "";
				ClsDocumentMerge objTemp = new ClsDocumentMerge();
				DataSet ds = null;
				if(hidMergeId.Value!="" && hidMergeId.Value!="-1")
				{
					ds = objTemp.GetTemplatePath(int.Parse(hidMergeId.Value.Trim()));				
				}
				if(ds.Tables[0].Rows.Count>0)
				{
					string TemplateFileName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString());
					strFilePath = TemplateFileName + ds.Tables[0].Rows[0]["TEMPLATE_PATH"].ToString();

				}
				if(strFilePath!="")
				{
					try
					{
						MailAttachment attach = new MailAttachment(strFilePath);
						objMailMessage.Attachments.Add(attach);
					}
					catch
					{
                        lblMessage1.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1354"); //"Invalid Attachment."; //sneha
						lblMessage1.Visible=true;

					}
				}
				

//				if (txtATTACHMENT.PostedFile != null && txtATTACHMENT.PostedFile.FileName != "")
//				{
//					HttpPostedFile attFile = txtATTACHMENT.PostedFile;
//					int attachFileLength = attFile.ContentLength; 
//					string strFilePath;
//					string strFileName;
//					if (attachFileLength > 0)
//					{ 
//						try
//						{
//						
//							//strFileName = txtATTACHMENT.PostedFile.FileName; 
//							strFilePath = strDirName + "\\" + strFileName;
//							MailAttachment attach = new MailAttachment(strFilePath);
//							objMailMessage.Attachments.Add(attach);
//							
//						}
//						catch
//						{
//							lblMessage1.Text = "Invalid Attachment.";
//							lblMessage1.Visible=true;
//							return false;
//						}
//					}
//				} 
				objMailMessage.Body				=	strMessage;
				SmtpMail.SmtpServer				=	strServerName;
				try
				{
					SmtpMail.Send(objMailMessage);
				}
				catch
				{
                    lblMessage1.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1355"); ; //"Invalid E-Mail address(s).";//sneha
					lblMessage1.Visible=true;
					return false;
				}
	
				
				return true;
			}
			catch(Exception exp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exp);
				return false;
			}
		}
		
		#endregion
	}
}
