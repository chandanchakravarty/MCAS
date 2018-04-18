/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using System.Xml;
using Cms.BusinessLayer.BlCommon; 



namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// 
	/// </summary>
	public class AddExpertServiceProviders : Cms.CmsWeb.cmsbase  
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************		
		private const int EXPERT_SERVICE_PROVIDER_ID=4;				
		//creating resource manager object (used for reading field and label mapping)
		//System.Resources.ResourceManager objResourceMgr;
		private string strRowId;		
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_COUNTRY;

		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_TYPE_DESC;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_TYPE_DESC;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_TYPE_DESC;		


		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_MASTER_VENDOR_CODE;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_MASTER_VENDOR_CODE;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.WebControls.DropDownList cmbEXPERT_SERVICE_COUNTRY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPERT_SERVICE_CONTACT_PHONE;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPERT_SERVICE_CONTACT_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPERT_SERVICE_ZIP;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capMAIL; 
        protected System.Web.UI.WebControls.Label capCOPY;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbEXPERT_SERVICE_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_TYPE;		
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_NAME;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_NAME;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_ADDRESS1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_ADDRESS1;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_ADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_ADDRESS2;		
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_CITY;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_CITY;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_STATE;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_STATE;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_ZIP;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_ZIP;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_VENDOR_CODE;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_VENDOR_CODE;		
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_CONTACT_NAME;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_CONTACT_NAME;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_CONTACT_PHONE;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_CONTACT_PHONE;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_CONTACT_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_CONTACT_EMAIL;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_FEDRERAL_ID;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_FEDRERAL_ID;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_1099_PROCESSING_OPTION;
		protected System.Web.UI.WebControls.DropDownList cmbEXPERT_SERVICE_1099_PROCESSING_OPTION;
		protected System.Web.UI.WebControls.DropDownList cmbEXPERT_SERVICE_STATE;		
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARENT_ADJUSTER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPERT_SERVICE_MASTER_VENDOR_CODE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONCAT_DATA;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPERT_SERVICE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hid_ExpertServicePro;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPartiesAdjuster;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_FEDRERAL_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipeCodeVerificationMsg;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_VENDOR_CODE;
		protected System.Web.UI.WebControls.Label capPARTY_DETAIL;
		protected System.Web.UI.WebControls.DropDownList cmbPARTY_DETAIL;
		protected System.Web.UI.WebControls.Label capAGE;
		protected System.Web.UI.WebControls.TextBox txtAGE;
		protected System.Web.UI.WebControls.RangeValidator rngAGE;
		protected System.Web.UI.WebControls.Label capEXTENT_OF_INJURY;
		protected System.Web.UI.WebControls.TextBox txtEXTENT_OF_INJURY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXTENT_OF_INJURY;
		protected System.Web.UI.WebControls.Label capOTHER_DETAILS;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DETAILS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOTHER_DETAILS;
		protected System.Web.UI.WebControls.Label capBANK_NAME;
		protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
		protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.Label capACCOUNT_NAME;
		protected System.Web.UI.WebControls.TextBox txtACCOUNT_NAME;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_CONTACT_FAX;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_CONTACT_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPERT_SERVICE_CONTACT_FAX;
		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_CONTACT_PHONE_EXT;
		protected System.Web.UI.WebControls.TextBox txtEXPERT_SERVICE_CONTACT_PHONE_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPERT_SERVICE_CONTACT_PHONE_EXT;
		protected System.Web.UI.WebControls.Label capPARENT_ADJUSTER;
		protected System.Web.UI.WebControls.TextBox txtPARENT_ADJUSTER;
		protected System.Web.UI.HtmlControls.HtmlImage imgPARENT_ADJUSTER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPERT_SERVICE_1099_PROCESSING_OPTION;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPERT_SERVICE_FEDRERAL_ID;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
        protected System.Web.UI.WebControls.Label capMessages;
		
		protected System.Web.UI.WebControls.Label capMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.Label capMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.Label capMAIL_1099_CITY;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_CITY;
		protected System.Web.UI.WebControls.Label capMAIL_1099_STATE;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.Label capMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbMAIL_1099_STATE;
		protected System.Web.UI.WebControls.Label capMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.Image imgMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.HyperLink hlkMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.CheckBox chkCopyData;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMAIL_1099_ZIP;

		protected System.Web.UI.WebControls.Label capMAIL_1099_NAME;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_NAME;
		protected System.Web.UI.WebControls.Label capW9_FORM;
		protected System.Web.UI.WebControls.DropDownList cmbW9_FORM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvW9_FORM;

		protected System.Web.UI.WebControls.Label capEXPERT_SERVICE_FEDRERAL_ID_HID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPERT_SERVICE_FEDRERAL_ID;
		//Added By Raghav For Special Handling.
		protected System.Web.UI.WebControls.Label capREQ_SPECIAL_HANDLING; 
		protected System.Web.UI.WebControls.CheckBox chkREQ_SPECIAL_HANDLING;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREQ_SPECIAL_HANDLING;
        protected System.Web.UI.WebControls.Label capCPF;
        protected System.Web.UI.WebControls.TextBox txtCPF;
        protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.Label capACTIVITY;
        protected System.Web.UI.WebControls.DropDownList cmbACTIVITY;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_IDENTIFICATION;
        protected System.Web.UI.WebControls.Label capREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCPF_CNPJ;
        protected System.Web.UI.WebControls.HyperLink hlkREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEdit;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCancel;
        protected System.Web.UI.WebControls.CustomValidator csvREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
        public string javasciptmsg, javasciptCPFmsg, CPF_invalid, CNPJ_invalid;
        private string XmlFullFilePath = "";

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			//Done for Itrack Issue 6553 on 13 Oct 09
			base.ScreenId="300_1";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			lblMessage.Visible = false;
          
			chkCopyData.Attributes.Add("onClick","javascript:CopyData('txtEXPERT_SERVICE_NAME','txtMAIL_1099_NAME');CopyData('txtEXPERT_SERVICE_ADDRESS1','txtMAIL_1099_ADD1');CopyData('txtEXPERT_SERVICE_ADDRESS2','txtMAIL_1099_ADD2');CopyData('txtEXPERT_SERVICE_CITY','txtMAIL_1099_CITY');CopyData('txtEXPERT_SERVICE_ZIP','txtMAIL_1099_ZIP');CopyData('cmbEXPERT_SERVICE_STATE','cmbMAIL_1099_STATE');");
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddExpertServiceProviders" ,System.Reflection.Assembly.GetExecutingAssembly());
            hidZipeCodeVerificationMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "11"); //Added by aditya for tfs bug # 2687
            // Added by Swarup on 30-mar-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddressDetailsBR(hlkZipLookup, txtEXPERT_SERVICE_ADDRESS1,txtEXPERT_SERVICE_ADDRESS2
				, txtEXPERT_SERVICE_CITY, cmbEXPERT_SERVICE_STATE,txtEXPERT_SERVICE_ZIP);


            javasciptmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1861");
            javasciptCPFmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1081");
            CPF_invalid = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1859");
            CNPJ_invalid = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1860");
            hidCancel.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2097");
            hidEdit.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2096");
            hid_ExpertServicePro.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2098");
            hidPartiesAdjuster.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2099");
            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";
            XmlFullFilePath = Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + "AddExpertServiceProviders.xml";

			if(!Page.IsPostBack)
			{			
				imgMAIL_1099_ZIP.Attributes.Add("style","cursor:hand");
				
				base.VerifyAddress(hlkMAIL_1099_ZIP,txtMAIL_1099_ADD1,txtMAIL_1099_ADD2, txtMAIL_1099_CITY, cmbMAIL_1099_STATE, txtMAIL_1099_ZIP);

				imgSelect.Attributes.Add("onclick","javascript:OpenExpertParentLookup('')");
				imgPARENT_ADJUSTER.Attributes.Add("onclick","javascript:OpenParentAdjusterLookup('')");
				//imgPARENT_ADJUSTER.Attributes.Add("onclick","javascript:OpenExpertParentLookup('')");
				cmbEXPERT_SERVICE_TYPE.Attributes.Add("onChange","javascript:HideShowTypeDesc();");
                hlkREG_ID_ISSUE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtREG_ID_ISSUE_DATE,txtREG_ID_ISSUE_DATE)"); //Javascript Implementation for Calender				
                hlkDATE_OF_BIRTH.Attributes.Add("OnClick", "fPopCalendar(txtDATE_OF_BIRTH,txtDATE_OF_BIRTH)");
				this.cmbEXPERT_SERVICE_COUNTRY.SelectedIndex = int.Parse(aCountry);
				if(Request.QueryString["EXPERT_SERVICE_ID"]!=null && Request.QueryString["EXPERT_SERVICE_ID"].ToString()!="")
				{
					hidEXPERT_SERVICE_ID.Value = Request.QueryString["EXPERT_SERVICE_ID"].ToString();
					GetOldDataXML();
				}
                if (Request.QueryString["EXPERT_SERVICE_ID"] == null) {
                    btnActivateDeactivate.Visible = false;
                }
				LoadDropDowns();
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				//SetCaptions();
				SetErrorMessages();

            

                // Added by Agniswar for Screen Customization on 14 sep 2011


                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, "AddExpertServiceProviders.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/AddExpertServiceProviders.xml");
               
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
			if(hidEXPERT_SERVICE_ID.Value!="" && hidEXPERT_SERVICE_ID.Value!="0")
				hidOldData.Value	=	ClsExpertServiceProviders.GetExpertServiceProviders(int.Parse(hidEXPERT_SERVICE_ID.Value),int.Parse(GetLanguageID()));
			else
				hidOldData.Value	=	"";
            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString()));

			//Added by Mohit Agarwal 23-Sep-08
			if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
			{
				XmlDocument objxml = new XmlDocument();

				objxml.LoadXml(hidOldData.Value);

				XmlNode node = objxml.SelectSingleNode("NewDataSet");
				foreach(XmlNode nodes in node.SelectNodes("Table"))
				{
					XmlNode noder1 = nodes.SelectSingleNode("EXPERT_SERVICE_FEDRERAL_ID");

					hidEXPERT_SERVICE_FEDRERAL_ID.Value = noder1.InnerText;
					//noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
					string strEXPERT_SERVICE_FEDRERAL_ID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
					if(strEXPERT_SERVICE_FEDRERAL_ID != "")
					{
						string strvaln = "";
						for(int len=0; len < strEXPERT_SERVICE_FEDRERAL_ID.Length-4; len++)
							strvaln += "x";

						strvaln += strEXPERT_SERVICE_FEDRERAL_ID.Substring(strvaln.Length, strEXPERT_SERVICE_FEDRERAL_ID.Length - strvaln.Length);
						capEXPERT_SERVICE_FEDRERAL_ID_HID.Text = strvaln;
					}
					else
						capEXPERT_SERVICE_FEDRERAL_ID_HID.Text = "";
				}
				objxml = null;
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			revEXPERT_SERVICE_ZIP.ValidationExpression				=		  aRegExpZip;
            //Modify by santosh kumar gautam on 03 march 2011
            revEXPERT_SERVICE_ZIP.ErrorMessage                      =         Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            revMAIL_1099_ZIP.ErrorMessage                           =         Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
        

			revEXPERT_SERVICE_CONTACT_EMAIL.ValidationExpression	=		  aRegExpEmail;
			revEXPERT_SERVICE_CONTACT_EMAIL.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
			rfvEXPERT_SERVICE_NAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2095");			
			rfvEXPERT_SERVICE_ZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
            revEXPERT_SERVICE_CONTACT_PHONE.ValidationExpression    =          aRegExpPhoneBrazil;
			revEXPERT_SERVICE_CONTACT_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");						
			rfvEXPERT_SERVICE_ADDRESS1.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			rfvEXPERT_SERVICE_STATE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");			
			rfvEXPERT_SERVICE_TYPE.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			rfvEXPERT_SERVICE_FEDRERAL_ID.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
			rfvEXPERT_SERVICE_1099_PROCESSING_OPTION.ErrorMessage	=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
			rfvEXPERT_SERVICE_VENDOR_CODE.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");			
			rfvEXPERT_SERVICE_TYPE_DESC.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("550");		
			revEXTENT_OF_INJURY.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("895");
			revEXTENT_OF_INJURY.ValidationExpression= aRegExpAlphaNum;
			revOTHER_DETAILS.ErrorMessage   =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");
			revOTHER_DETAILS.ValidationExpression   = aRegExpTextArea500;
			rngAGE.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("896");			
			revEXPERT_SERVICE_CONTACT_PHONE_EXT.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			revEXPERT_SERVICE_CONTACT_PHONE_EXT.ValidationExpression = aRegExpExtn;
			revEXPERT_SERVICE_CONTACT_FAX.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			revEXPERT_SERVICE_CONTACT_FAX.ValidationExpression		= aRegExpFax;
			revEXPERT_SERVICE_FEDRERAL_ID.ValidationExpression		= aRegExpFederalID;
			revEXPERT_SERVICE_FEDRERAL_ID.ErrorMessage				= ClsMessages.FetchGeneralMessage("434");
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1081");
            revDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revDATE_OF_BIRTH.ValidationExpression =aRegExpDate;
            revREGIONAL_ID_ISSUE_DATE.ValidationExpression = aRegExpDate;

			rfvMAIL_1099_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","56");
			rfvMAIL_1099_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","35");
			rfvMAIL_1099_COUNTRY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","33");
			rfvMAIL_1099_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("32_0","37");
			revMAIL_1099_ZIP.ValidationExpression	=	aRegExpZip;
			
            rfvMAIL_1099_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6"); 
            rfvMAIL_1099_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7"); 
            rfvW9_FORM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            csvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            csvREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");

            rfvEXPERT_SERVICE_FEDRERAL_ID.Enabled = false;
		}

		#endregion

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsExpertServiceProviders objExpertServiceProviders = new ClsExpertServiceProviders();				

				//Retreiving the form values into model class object
				ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objExpertServiceProvidersInfo.CREATED_BY = int.Parse(GetUserId());
					objExpertServiceProvidersInfo.CREATED_DATETIME = DateTime.Now;
					objExpertServiceProvidersInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
                    intRetVal = objExpertServiceProviders.Add(objExpertServiceProvidersInfo, XmlFullFilePath);

					if(intRetVal>0)
					{
						hidEXPERT_SERVICE_ID.Value = objExpertServiceProvidersInfo.EXPERT_SERVICE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
					}
					else if(intRetVal == -2) //Duplicate Vendor Code
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"838");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -1) //Duplicate Authority Limit
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsExpertServiceProvidersInfo objOldExpertServiceProvidersInfo = new ClsExpertServiceProvidersInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldExpertServiceProvidersInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objExpertServiceProvidersInfo.MODIFIED_BY = int.Parse(GetUserId());
					objExpertServiceProvidersInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    
			
					//Updating the record using business layer class object
					string customInfo ="";
					customInfo +=	";Expert/Service Type = " + cmbEXPERT_SERVICE_TYPE.Items[cmbEXPERT_SERVICE_TYPE.SelectedIndex].Text;
					customInfo +=	";Expert Service Provider Name = " + txtEXPERT_SERVICE_NAME.Text;
//					if(txtEXPERT_SERVICE_MASTER_VENDOR_CODE.Text!="")
//					{
//						customInfo +=	";Master Vendor Code = " + txtEXPERT_SERVICE_MASTER_VENDOR_CODE.Text;					
//					}
					customInfo +=	";Vendor Code = " + txtEXPERT_SERVICE_VENDOR_CODE.Text;

                    intRetVal = objExpertServiceProviders.Update(objOldExpertServiceProvidersInfo, customInfo, objExpertServiceProvidersInfo, XmlFullFilePath);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					//Done for Itrack Issue 6020 on 29 June 09
					else if(intRetVal == -1) //Duplicate Authority Limit
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"838");
						hidFormSaved.Value		=	"2";
					}
					//Added till here
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
                btnActivateDeactivate.Visible = true;
                btnActivateDeactivate.Enabled = true;
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString()));
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
			finally
			{
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
        //private void SetCaptions()
        //{
        //    capEXPERT_SERVICE_TYPE.Text						=		objResourceMgr.GetString("cmbEXPERT_SERVICE_TYPE");
        //    capEXPERT_SERVICE_NAME.Text						=		objResourceMgr.GetString("txtEXPERT_SERVICE_NAME");
        //    capEXPERT_SERVICE_ADDRESS1.Text					=		objResourceMgr.GetString("txtEXPERT_SERVICE_ADDRESS1");
        //    capEXPERT_SERVICE_ADDRESS2.Text					=		objResourceMgr.GetString("txtEXPERT_SERVICE_ADDRESS2");
        //    capEXPERT_SERVICE_CITY.Text						=		objResourceMgr.GetString("txtEXPERT_SERVICE_CITY");
        //    capEXPERT_SERVICE_STATE.Text					=		objResourceMgr.GetString("cmbEXPERT_SERVICE_STATE");
        //    capEXPERT_SERVICE_ZIP.Text						=		objResourceMgr.GetString("txtEXPERT_SERVICE_ZIP");
        //    capEXPERT_SERVICE_VENDOR_CODE.Text				=		objResourceMgr.GetString("txtEXPERT_SERVICE_VENDOR_CODE");
        //    capEXPERT_SERVICE_CONTACT_NAME.Text				=		objResourceMgr.GetString("txtEXPERT_SERVICE_CONTACT_NAME");
        //    capEXPERT_SERVICE_CONTACT_PHONE.Text			=		objResourceMgr.GetString("txtEXPERT_SERVICE_CONTACT_PHONE");
        //    capEXPERT_SERVICE_CONTACT_EMAIL.Text			=		objResourceMgr.GetString("txtEXPERT_SERVICE_CONTACT_EMAIL");
        //    capEXPERT_SERVICE_FEDRERAL_ID.Text				=		objResourceMgr.GetString("txtEXPERT_SERVICE_FEDRERAL_ID");
        //    capEXPERT_SERVICE_1099_PROCESSING_OPTION.Text	=		objResourceMgr.GetString("cmbEXPERT_SERVICE_1099_PROCESSING_OPTION");
        //    capEXPERT_SERVICE_COUNTRY.Text					=		objResourceMgr.GetString("cmbEXPERT_SERVICE_COUNTRY");
        //    capEXPERT_SERVICE_MASTER_VENDOR_CODE.Text		=		objResourceMgr.GetString("txtEXPERT_SERVICE_MASTER_VENDOR_CODE");
        //    capEXPERT_SERVICE_TYPE_DESC.Text				=		objResourceMgr.GetString("txtEXPERT_SERVICE_TYPE_DESC");
        //    capPARTY_DETAIL.Text							=		objResourceMgr.GetString("cmbPARTY_DETAIL");
        //    capAGE.Text										=		objResourceMgr.GetString("txtAGE");
        //    capEXTENT_OF_INJURY.Text						=		objResourceMgr.GetString("txtEXTENT_OF_INJURY");
        //    capOTHER_DETAILS.Text							=		objResourceMgr.GetString("txtOTHER_DETAILS");
        //    capBANK_NAME.Text								=		objResourceMgr.GetString("txtBANK_NAME");
        //    capACCOUNT_NUMBER.Text							=		objResourceMgr.GetString("txtACCOUNT_NUMBER");
        //    capACCOUNT_NAME.Text							=		objResourceMgr.GetString("txtACCOUNT_NAME");
        //    capEXPERT_SERVICE_TYPE_DESC.Text				=		objResourceMgr.GetString("txtEXPERT_SERVICE_TYPE_DESC");
        //    capEXPERT_SERVICE_CONTACT_PHONE_EXT.Text		=		objResourceMgr.GetString("txtEXPERT_SERVICE_CONTACT_PHONE_EXT");
        //    capEXPERT_SERVICE_CONTACT_FAX.Text				=		objResourceMgr.GetString("txtEXPERT_SERVICE_CONTACT_FAX");
        //    capPARENT_ADJUSTER.Text							=		objResourceMgr.GetString("txtPARENT_ADJUSTER");
        //    capMAIL_1099_ADD1.Text				=		objResourceMgr.GetString("txtMAIL_1099_ADD1");
        //    capMAIL_1099_ADD2.Text				=		objResourceMgr.GetString("txtMAIL_1099_ADD2");
        //    capMAIL_1099_CITY.Text				=		objResourceMgr.GetString("txtMAIL_1099_CITY");
        //    capMAIL_1099_STATE.Text				=		objResourceMgr.GetString("txtMAIL_1099_STATE");
        //    capMAIL_1099_COUNTRY.Text			=		objResourceMgr.GetString("txtMAIL_1099_COUNTRY");
        //    capMAIL_1099_ZIP.Text				=		objResourceMgr.GetString("txtMAIL_1099_ZIP");
        //    //Added By Raghav For Special Handling
        //    capREQ_SPECIAL_HANDLING.Text        =        objResourceMgr.GetString("chkREQ_SPECIAL_HANDLING");   

        //    capMAIL_1099_NAME.Text				=		objResourceMgr.GetString("txtMAIL_1099_NAME");
        //    capW9_FORM.Text			=		objResourceMgr.GetString("txtW9_FORM");
        //    capMAIL.Text = objResourceMgr.GetString("capMAIL"); 
        //    capCOPY.Text = objResourceMgr.GetString("capCOPY");
        //    capCPF.Text = objResourceMgr.GetString("txtCPF");
        //    capDATE_OF_BIRTH.Text = objResourceMgr.GetString("txtDATE_OF_BIRTH");
        //    capACTIVITY.Text = objResourceMgr.GetString("cmbACTIVITY");
        //    capREGIONAL_ID.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION");
        //    capREGIONAL_ID_ISSUE_DATE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE_DATE");
        //    capREGIONAL_ID_ISSUE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE");
        //    hid_ExpertServicePro.Value = objResourceMgr.GetString("hid_ExpertServicePro");
        //    hidPartiesAdjuster.Value = objResourceMgr.GetString("hidPartiesAdjuster");
        //    hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg");
        //}
	

		#region GetFormValue
		private ClsExpertServiceProvidersInfo GetFormValue()
		{
			ClsExpertServiceProvidersInfo objExpertServiceProvidersInfo = new ClsExpertServiceProvidersInfo();

			if(cmbEXPERT_SERVICE_TYPE.SelectedItem!=null && cmbEXPERT_SERVICE_TYPE.SelectedItem.Value!="")
				objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE	=	int.Parse(cmbEXPERT_SERVICE_TYPE.SelectedItem.Value);
			if(objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE==EXPERT_SERVICE_PROVIDER_TYPE_OTHER && txtEXPERT_SERVICE_TYPE_DESC.Text.Trim()!="")
				objExpertServiceProvidersInfo.EXPERT_SERVICE_TYPE_DESC = txtEXPERT_SERVICE_TYPE_DESC.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_NAME		=	txtEXPERT_SERVICE_NAME.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS1	=	txtEXPERT_SERVICE_ADDRESS1.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_ADDRESS2	=	txtEXPERT_SERVICE_ADDRESS2.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_CITY		=	txtEXPERT_SERVICE_CITY.Text.Trim();
			if(cmbEXPERT_SERVICE_STATE.SelectedItem!=null && cmbEXPERT_SERVICE_STATE.SelectedItem.Value!="")
				objExpertServiceProvidersInfo.EXPERT_SERVICE_STATE		=	int.Parse(cmbEXPERT_SERVICE_STATE.SelectedItem.Value);
			objExpertServiceProvidersInfo.EXPERT_SERVICE_ZIP			=	txtEXPERT_SERVICE_ZIP.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_VENDOR_CODE	=	txtEXPERT_SERVICE_VENDOR_CODE.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_NAME	=	txtEXPERT_SERVICE_CONTACT_NAME.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE	=	txtEXPERT_SERVICE_CONTACT_PHONE.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_EMAIL	=	txtEXPERT_SERVICE_CONTACT_EMAIL.Text.Trim();
            if (cmbACTIVITY.SelectedItem != null && cmbACTIVITY.SelectedItem.Value != "")
                objExpertServiceProvidersInfo.ACTIVITY = int.Parse(cmbACTIVITY.SelectedItem.Value);
            //objExpertServiceProvidersInfo.DATE_OF_BIRTH =ConvertToDate(txtDATE_OF_BIRTH.Text);
            //objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE = ConvertToDate(txtREG_ID_ISSUE_DATE.Text);//REGIONAL_ID_ISSUE_DATE
            if (txtDATE_OF_BIRTH.Text != "")
            {
                objExpertServiceProvidersInfo.DATE_OF_BIRTH = ConvertToDate(txtDATE_OF_BIRTH.Text);
            }
            if (txtREG_ID_ISSUE_DATE.Text!= "")
            {
                objExpertServiceProvidersInfo.REG_ID_ISSUE_DATE = ConvertToDate(txtREG_ID_ISSUE_DATE.Text);//REGIONAL_ID_ISSUE_DATE}
            }
            objExpertServiceProvidersInfo.REG_ID_ISSUE = txtREG_ID_ISSUE.Text.ToString();
            objExpertServiceProvidersInfo.REGIONAL_IDENTIFICATION = txtREGIONAL_IDENTIFICATION.Text.ToString();
            objExpertServiceProvidersInfo.CPF = txtCPF.Text.ToString();

			if(txtEXPERT_SERVICE_FEDRERAL_ID.Text.Trim()!="")
			{
				objExpertServiceProvidersInfo.EXPERT_SERVICE_FEDRERAL_ID			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtEXPERT_SERVICE_FEDRERAL_ID.Text.Trim());
				txtEXPERT_SERVICE_FEDRERAL_ID.Text = "";
			}
			else
				objExpertServiceProvidersInfo.EXPERT_SERVICE_FEDRERAL_ID			= hidEXPERT_SERVICE_FEDRERAL_ID.Value;

			//objExpertServiceProvidersInfo.EXPERT_SERVICE_FEDRERAL_ID	=	txtEXPERT_SERVICE_FEDRERAL_ID.Text.Trim();
			if(cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.SelectedItem!=null && cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.SelectedItem.Value!="")
				objExpertServiceProvidersInfo.EXPERT_SERVICE_1099_PROCESSING_OPTION=int.Parse(cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.SelectedItem.Value);

			objExpertServiceProvidersInfo.EXPERT_SERVICE_MASTER_VENDOR_CODE = hidEXPERT_SERVICE_MASTER_VENDOR_CODE_ID.Value;
			objExpertServiceProvidersInfo.EXPERT_SERVICE_COUNTRY =	cmbEXPERT_SERVICE_COUNTRY.SelectedValue;
			
			objExpertServiceProvidersInfo.MAIL_1099_ADD1	=	txtMAIL_1099_ADD1.Text.Trim();
			objExpertServiceProvidersInfo.MAIL_1099_ADD2	=	txtMAIL_1099_ADD2.Text.Trim();
			objExpertServiceProvidersInfo.MAIL_1099_CITY		=	txtMAIL_1099_CITY.Text.Trim();
			if(cmbMAIL_1099_STATE.SelectedItem!=null && cmbMAIL_1099_STATE.SelectedItem.Value!="")
				objExpertServiceProvidersInfo.MAIL_1099_STATE		=	cmbMAIL_1099_STATE.SelectedItem.Value;
			objExpertServiceProvidersInfo.MAIL_1099_ZIP			=	txtMAIL_1099_ZIP.Text.Trim();
			objExpertServiceProvidersInfo.MAIL_1099_COUNTRY =	cmbMAIL_1099_COUNTRY.SelectedValue;
			
			objExpertServiceProvidersInfo.MAIL_1099_NAME	=	txtMAIL_1099_NAME.Text.Trim();
			objExpertServiceProvidersInfo.W9_FORM =	cmbW9_FORM.SelectedValue;

			if(hidEXPERT_SERVICE_ID.Value.ToUpper()=="NEW" || hidEXPERT_SERVICE_ID.Value=="0")
				strRowId="NEW";
			else
			{
				strRowId=hidEXPERT_SERVICE_ID.Value;
				objExpertServiceProvidersInfo.EXPERT_SERVICE_ID		=	int.Parse(hidEXPERT_SERVICE_ID.Value);
			}


			if(cmbPARTY_DETAIL.SelectedValue!=null && cmbPARTY_DETAIL.SelectedValue.Trim()!="")
				objExpertServiceProvidersInfo.PARTY_DETAIL =	int.Parse(cmbPARTY_DETAIL.SelectedValue);
			if(txtAGE.Text.Trim()!="")
				objExpertServiceProvidersInfo.AGE = int.Parse(txtAGE.Text.Trim());
			
			objExpertServiceProvidersInfo.OTHER_DETAILS = txtOTHER_DETAILS.Text.Trim();
			objExpertServiceProvidersInfo.EXTENT_OF_INJURY	 =	txtEXTENT_OF_INJURY.Text;
			objExpertServiceProvidersInfo.BANK_NAME = txtBANK_NAME.Text; 
			objExpertServiceProvidersInfo.ACCOUNT_NUMBER = txtACCOUNT_NUMBER.Text; 
			objExpertServiceProvidersInfo.ACCOUNT_NAME = txtACCOUNT_NAME.Text; 
			if(hidPARENT_ADJUSTER_ID.Value!="")
				objExpertServiceProvidersInfo.PARENT_ADJUSTER = int.Parse(hidPARENT_ADJUSTER_ID.Value.Trim());
			
			if(txtEXPERT_SERVICE_CONTACT_PHONE_EXT.Text!="")
				objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_PHONE_EXT = txtEXPERT_SERVICE_CONTACT_PHONE_EXT.Text.Trim();
			objExpertServiceProvidersInfo.EXPERT_SERVICE_CONTACT_FAX = txtEXPERT_SERVICE_CONTACT_FAX.Text.Trim();
			//Added By Raghav For Special Handling
			if(chkREQ_SPECIAL_HANDLING.Checked == true)
				objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()); //YES
			
			else
				objExpertServiceProvidersInfo.REQ_SPECIAL_HANDLING = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()); //NO
			
			return objExpertServiceProvidersInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			DataTable dtServiceProviders = ClsDefaultValues.GetDefaultValuesDetails(EXPERT_SERVICE_PROVIDER_ID);
			if(dtServiceProviders!=null)
			{
				cmbEXPERT_SERVICE_TYPE.DataSource	=	dtServiceProviders;
				cmbEXPERT_SERVICE_TYPE.DataTextField=	"DETAIL_TYPE_DESCRIPTION";
				cmbEXPERT_SERVICE_TYPE.DataValueField=	"DETAIL_TYPE_ID";
				cmbEXPERT_SERVICE_TYPE.DataBind();
			}
			/*Modified by Asfa (26-May-2008) - iTrack issue #4214
			//cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ESPPO");
			*/
			cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ESPPO",null,"S");
			cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.DataTextField="LookupDesc";
			cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.DataValueField="LookupID";
			cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.DataBind();
			cmbEXPERT_SERVICE_1099_PROCESSING_OPTION.Items.Insert(0,"");

			// Added by Mohit Agarwal 13-Oct 2008 ITrack 4795 Reopen
			Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbEXPERT_SERVICE_1099_PROCESSING_OPTION,"14123");

			/*Modified by Asfa (26-May-2008) - iTrack issue #4214
			//Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbEXPERT_SERVICE_1099_PROCESSING_OPTION,"14123");
			//Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbEXPERT_SERVICE_1099_PROCESSING_OPTION,"11733");
			*/
			dtServiceProviders						= Cms.CmsWeb.ClsFetcher.State;
			cmbEXPERT_SERVICE_STATE.DataSource		= dtServiceProviders;
			cmbEXPERT_SERVICE_STATE.DataTextField	= "State_Name";
			cmbEXPERT_SERVICE_STATE.DataValueField	= "State_Id";
			cmbEXPERT_SERVICE_STATE.DataBind();
			cmbEXPERT_SERVICE_STATE.Items.Insert(0,"");

			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbEXPERT_SERVICE_COUNTRY.DataSource		= dt;
			cmbEXPERT_SERVICE_COUNTRY.DataTextField	= "Country_Name";
			cmbEXPERT_SERVICE_COUNTRY.DataValueField	= "Country_Id";
			cmbEXPERT_SERVICE_COUNTRY.DataBind();

			cmbMAIL_1099_COUNTRY.DataSource		= dt;
			cmbMAIL_1099_COUNTRY.DataTextField	= "Country_Name";
			cmbMAIL_1099_COUNTRY.DataValueField	= "Country_Id";
			cmbMAIL_1099_COUNTRY.DataBind();

			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbMAIL_1099_STATE.DataSource =dt;
			cmbMAIL_1099_STATE.DataTextField ="State_Name";
			cmbMAIL_1099_STATE.DataValueField ="State_Id";
			cmbMAIL_1099_STATE.DataBind();


        
			cmbPARTY_DETAIL.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PTYDET");
			cmbPARTY_DETAIL.DataTextField="LookupDesc";
			cmbPARTY_DETAIL.DataValueField="LookupID";
			cmbPARTY_DETAIL.DataBind();
			cmbPARTY_DETAIL.Items.Insert(0, new ListItem("",""));

			//Added by Mohit Agarwal 25-Aug-2008
			cmbW9_FORM.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("W9FORM");
			cmbW9_FORM.DataTextField	= "LookupDesc";
			cmbW9_FORM.DataValueField	= "LookupID";
			cmbW9_FORM.DataBind();
			cmbW9_FORM.Items.Insert(0,"");

            DataTable dp = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
            cmbACTIVITY.DataSource = dp;
            cmbACTIVITY.DataTextField = "ACTIVITY_DESC";
            cmbACTIVITY.DataValueField = "ACTIVITY_ID";
            cmbACTIVITY.DataBind();
            cmbACTIVITY.Items.Insert(0, "");
            cmbACTIVITY.SelectedIndex = 0;
	
		}
		#endregion

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				ClsExpertServiceProviders objBL = new ClsExpertServiceProviders();				
				string strRetVal = "";
				string customInfo ="";
				customInfo +=	";Expert/Service Type = " + cmbEXPERT_SERVICE_TYPE.Items[cmbEXPERT_SERVICE_TYPE.SelectedIndex].Text;
				customInfo +=	";Expert Service Provider Name = " + txtEXPERT_SERVICE_NAME.Text;
				customInfo +=	";Vendor Code = " + txtEXPERT_SERVICE_VENDOR_CODE.Text;
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Expert Service Providers has been Deactivated Successfully.";
					objBL.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objBL.ActivateDeactivate(hidEXPERT_SERVICE_ID.Value,"N",customInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Expert Service Providers has been Activated Successfully.";
					objBL.TransactionInfoParams = objStuTransactionInfo;
					objBL.ActivateDeactivate(hidEXPERT_SERVICE_ID.Value,"Y",customInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				
				if (strRetVal == "-1")
				{
					/*Profit Center is assigned*/
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"514");
				}

				lblMessage.Visible = true;
				hidFormSaved.Value			=	"0";
				ClientScript.RegisterStartupScript(this.GetType(),"RefreshGRid","<script>RefreshWebGrid(1,1,true)</script>");
				GetOldDataXML();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		
		}

        [System.Web.Services.WebMethod]
        public static String GetCustomerAddressDetailsUsingZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }
	}
}
