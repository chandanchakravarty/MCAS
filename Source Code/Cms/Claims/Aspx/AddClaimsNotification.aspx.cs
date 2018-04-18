/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 27,2006
	<End Date				: - >
	<Description			: - > Page is used for Claims Notification
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History
	Name    : Raman
	Date    : 4 Dec 2006
	Purpose : Insured name should prefill and default the address. Make address as non editable.


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
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddClaimsNotification : Cms.Claims.ClaimBase
	{
		#region Local form variables
		//START:*********** Local form variables *************		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		//Integer variable to indicate that the session value of policy_vesion_id has been changed at page level
		int SessionValueChanged = -1;
		private string strRowId;//, strAddNew;	
		private bool LOAD_DATA_FLAG = true;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblAGENCY_DISPLAY_NAME;
		protected System.Web.UI.WebControls.Label capAGENCY_DISPLAY_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectPinkSlipNotifyUsers;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnAddClaimant;
		protected System.Web.UI.WebControls.ListBox cmbRECIEVE_PINK_SLIP_USERS_LIST;
		protected System.Web.UI.WebControls.Label capRECIEVE_PINK_SLIP_USERS_LIST;
		protected System.Web.UI.WebControls.ListBox cmbPINK_SLIP_TYPE_LIST;
		protected System.Web.UI.WebControls.Label capPINK_SLIP_TYPE_LIST;
		//protected Cms.CmsWeb.Controls.CmsButton btnRescind;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnMatchPolicy;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLINKED_CLAIM_ID_LIST;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLINKED_CLAIM_LIST;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHOMEOWNER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReserveAdded;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAuthorized;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRECR_VEH;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIN_MARINE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSS_DATE_MATCHED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDUMMY_POLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDIARY_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIMANT_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCo_Insurance_Type;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOFFICIAL_CLAIM_NUMBER;
		//Added by Asfa 31-Aug-2007
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADJUSTER_CODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADJUSTER_ID; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCATASTROPHE_EVENT_CODE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLISTID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTOUSERID;

        //Added by Santosh Gautam 12 Nov 2010
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLD_LITIGATION_FILE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCURRENT_LITIGATION_FILE;


        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLD_IS_VICTIM_CLAIM;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCURRENT_IS_VICTIM_CLAIM;

		
		protected System.Web.UI.WebControls.Label capCLAIM_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_NUMBER;
//		protected System.Web.UI.WebControls.TextBox txtINSURED_RELATIONSHIP;		
		protected System.Web.UI.WebControls.Label capLOSS_DATE;
		protected System.Web.UI.WebControls.TextBox txtLOSS_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkLOSS_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSS_DATE;
		protected System.Web.UI.WebControls.Label capADJUSTER_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbADJUSTER_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADJUSTER_CODE;
		protected System.Web.UI.WebControls.Label capREPORTED_TO;
		protected System.Web.UI.WebControls.TextBox txtREPORTED_TO;
		protected System.Web.UI.WebControls.Label capREPORTED_BY;
		protected System.Web.UI.WebControls.TextBox txtREPORTED_BY;
		protected System.Web.UI.WebControls.Label capCATASTROPHE_EVENT_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbCATASTROPHE_EVENT_CODE;
		//protected System.Web.UI.WebControls.Label capCLAIMANT_INSURED;
		//		protected System.Web.UI.WebControls.DropDownList cmbCLAIMANT_INSURED;
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.TextBox txtZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revZIP;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		//Added by Asfa (22-Apr-2008) - iTrack issue #3697
		protected System.Web.UI.WebControls.Label capINSURED_NAME;
		protected System.Web.UI.WebControls.Label lblINSURED_NAME;
		protected System.Web.UI.WebControls.Label capINSUREDADDRESS1;
		protected System.Web.UI.WebControls.Label lblINSUREDADDRESS1;
		protected System.Web.UI.WebControls.Label capINSUREDADDRESS2;
		protected System.Web.UI.WebControls.Label lblINSUREDADDRESS2;
		protected System.Web.UI.WebControls.Label capINSUREDCITY;
		protected System.Web.UI.WebControls.Label lblINSUREDCITY;
		protected System.Web.UI.WebControls.Label capINSUREDSTATE;
		protected System.Web.UI.WebControls.Label lblINSUREDSTATE;
		protected System.Web.UI.WebControls.Label capINSUREDCOUNTRY;
		protected System.Web.UI.WebControls.Label lblINSUREDCOUNTRY;
		protected System.Web.UI.WebControls.Label capINSUREDZIP;
		protected System.Web.UI.WebControls.Label lblINSUREDZIP;
        protected System.Web.UI.WebControls.Label lblCLAIM_OFFICIAL_NUMBER;
        protected System.Web.UI.WebControls.Label capCLAIM_OFFICIAL_NUMBER;
        protected System.Web.UI.WebControls.Label capGenerated;
        protected System.Web.UI.WebControls.Label capCap;
        
        
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.Label capHOME_PHONE;
		protected System.Web.UI.WebControls.TextBox txtHOME_PHONE;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_PHONE;
		protected System.Web.UI.WebControls.Label capWORK_PHONE;
		protected System.Web.UI.WebControls.TextBox txtWORK_PHONE;
		protected System.Web.UI.WebControls.Label capEXTENSION;
		protected System.Web.UI.WebControls.TextBox txtEXTENSION;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revWORK_PHONE;
		protected System.Web.UI.WebControls.Label capMOBILE_PHONE;
		protected System.Web.UI.WebControls.TextBox txtMOBILE_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMOBILE_PHONE;
		protected System.Web.UI.WebControls.Label capWHERE_CONTACT;
		protected System.Web.UI.WebControls.TextBox txtWHERE_CONTACT;
		protected System.Web.UI.WebControls.Label capWHEN_CONTACT;
		protected System.Web.UI.WebControls.TextBox txtWHEN_CONTACT;
		protected System.Web.UI.WebControls.Label capDIARY_DATE;
		protected System.Web.UI.WebControls.TextBox txtDIARY_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkDIARY_DATE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revDIARY_DATE;
		protected System.Web.UI.WebControls.Label capCLAIM_STATUS;
		protected System.Web.UI.WebControls.Label capCLAIM_STATUS_UNDER;
		protected System.Web.UI.WebControls.DropDownList cmbCLAIM_STATUS;
		protected System.Web.UI.WebControls.DropDownList cmbCLAIM_STATUS_UNDER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAIM_STATUS;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAIM_STATUS_UNDER;
		protected System.Web.UI.WebControls.Label capOUTSTANDING_RESERVE;		
		protected System.Web.UI.WebControls.Label capRESINSURANCE_RESERVE;		
		protected System.Web.UI.WebControls.Label capPAID_LOSS;		
		protected System.Web.UI.WebControls.Label capPAID_EXPENSE;		
		protected System.Web.UI.WebControls.Label capRECOVERIES;		
		protected System.Web.UI.WebControls.Label capCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_DESCRIPTION;
//		protected System.Web.UI.WebControls.Label capINSURED_RELATIONSHIP;
		protected System.Web.UI.WebControls.Label capCLAIMANT_NAME;
		protected System.Web.UI.WebControls.TextBox txtCLAIMANT_NAME;
		//		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER;
		//		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER;
		//		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_CONTACT;
		//		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_CONTACT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_DATE;
		//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAIMANT_INSURED;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIARY_DATE;
		protected System.Web.UI.WebControls.Label lblOUTSTANDING_RESERVE;
		protected System.Web.UI.WebControls.Label lblRESINSURANCE_RESERVE;
		protected System.Web.UI.WebControls.Label lblPAID_LOSS;
		protected System.Web.UI.WebControls.Label lblPAID_EXPENSE;
		protected System.Web.UI.WebControls.Label lblRECOVERIES;
		protected System.Web.UI.WebControls.CustomValidator csvLOSS_DATE;
		//protected System.Web.UI.WebControls.CustomValidator csvDIARY_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvCLAIM_DESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		//		protected System.Web.UI.HtmlControls.HtmlGenericControl spnCLAIMANT_INSURED;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectAdjuster;
		protected Cms.CmsWeb.Controls.CmsButton btnManageTrackActivity;
		protected Cms.CmsWeb.Controls.CmsButton btnCompleteActivity;
		protected Cms.CmsWeb.Controls.CmsButton btnReserves;
		protected Cms.CmsWeb.Controls.CmsButton btnAuthorizeTransaction;
		protected Cms.CmsWeb.Controls.CmsButton btnReOpenClaims;
		protected System.Web.UI.WebControls.Label capLOSS_TIME;
		protected System.Web.UI.WebControls.TextBox txtLOSS_HOUR;
		protected System.Web.UI.WebControls.Label lblLOSS_HOUR;
		protected System.Web.UI.WebControls.TextBox txtLOSS_MINUTE;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList cmbMERIDIEM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_HOUR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_MINUTE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMERIDIEM;
		//Added By Asfa 31-Aug-2007
		protected System.Web.UI.WebControls.CustomValidator csvADJUSTER_CODE;
		
		protected System.Web.UI.WebControls.RangeValidator rnvLOSS_HOUR;
		protected System.Web.UI.WebControls.RangeValidator rnvtLOSS_MINUTE;
		protected System.Web.UI.WebControls.RangeValidator rngEXTENSION;//Added for Itrack Issue 5641
		protected System.Web.UI.WebControls.Label capLITIGATION_FILE;
		protected System.Web.UI.WebControls.DropDownList cmbLITIGATION_FILE;		
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.Label capCLAIMANT_PARTY;
		protected System.Web.UI.WebControls.DropDownList cmbCLAIMANT_PARTY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAIMANT_PARTY;
		//		protected System.Web.UI.HtmlControls.HtmlTableRow trCLAIMANT_INSURED;
		protected System.Web.UI.WebControls.Label capLINKED_TO_CLAIM;
		//protected System.Web.UI.WebControls.TextBox txtLINKED_TO_CLAIM;
		//protected Cms.CmsWeb.Controls.CmsButton btnLINKED_TO_CLAIM;
		//		protected System.Web.UI.WebControls.Label capADD_FAULT;
		//		protected System.Web.UI.WebControls.DropDownList cmbADD_FAULT;
		//		protected System.Web.UI.WebControls.Label capTOTAL_LOSS;
		//		protected System.Web.UI.WebControls.DropDownList cmbTOTAL_LOSS;
		protected System.Web.UI.WebControls.Label capNOTIFY_REINSURER;
		protected System.Web.UI.WebControls.DropDownList cmbNOTIFY_REINSURER;
		protected System.Web.UI.WebControls.Label capFIRST_NOTICE_OF_LOSS;
        protected System.Web.UI.WebControls.Label capLAST_DOC_RECEIVE_DATE;
        
		protected System.Web.UI.WebControls.TextBox txtFIRST_NOTICE_OF_LOSS;
        protected System.Web.UI.WebControls.TextBox txtLAST_DOC_RECEIVE_DATE;
        
		protected System.Web.UI.WebControls.HyperLink hlkFIRST_NOTICE_OF_LOSS;
        protected System.Web.UI.WebControls.HyperLink hlkLAST_DOC_RECEIVE_DATE;
        
		protected System.Web.UI.WebControls.RegularExpressionValidator revFIRST_NOTICE_OF_LOSS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revLAST_DOC_RECEIVE_DATE;
        
		string strCATASTROPHE_EVENT_CODE="";
		protected System.Web.UI.WebControls.ImageButton  imgbtnUpdateDiary;
		private char STRING_DELIMITER_COMMA = ',';
		//Done for Itrack Issue 6620 on 27 Nov 09
		protected System.Web.UI.WebControls.Label capAT_FAULT_INDICATOR;
		protected System.Web.UI.WebControls.DropDownList cmbAT_FAULT_INDICATOR;
        protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbIS_VICTIM_CLAIM;

        protected System.Web.UI.WebControls.Button btnOFFCIAL_CLAIM_NUMBER;
        protected System.Web.UI.WebControls.Button btnFNOL;
        protected System.Web.UI.WebControls.Label capHeader;


        protected System.Web.UI.WebControls.Label capIS_VICTIM_CLAIM;
        protected System.Web.UI.WebControls.Label capREINSURANCE_TYPE;
        protected System.Web.UI.WebControls.Label capREIN_LOSS_NOTICE_NUM;
        protected System.Web.UI.WebControls.Label capREIN_CLAIM_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtREIN_LOSS_NOTICE_NUM;
        protected System.Web.UI.WebControls.TextBox txtREIN_CLAIM_NUMBER;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSSDATE_FUTUREDATE_ERRORMESSAGE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSSDATE_COMMON_ERRORMESSAGE;


        protected System.Web.UI.WebControls.Label capPOSSIBLE_PAYMENT_DATE;
        protected System.Web.UI.WebControls.TextBox txtPOSSIBLE_PAYMENT_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkPOSSIBLE_PAYMENT_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPOSSIBLE_PAYMENT_DATE;
        
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCELLED_POLICY_MESSAGE_1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCELLED_POLICY_MESSAGE_2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNO_CORRESSPOND_DATE_MESSAGE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAS400_MESSAGE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_EXISTS_ALERT_MSG;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_EXISTS_CONFIRM_MSG;
        public string Co_Insurance_Type;

		protected enum enumCLAIM_STATUS
		{
			OPEN = 11739,
			CLOSED = 11740,
			LITIGATION = 11741,
			SUBROGATION = 11742,
			RECORD_ONLY = 11743,
			RESCINDED = 11835
		}
		
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{

            txtREPORTED_TO.Enabled = false;

			Ajax.Utility.RegisterTypeForAjax(typeof(AddClaimsNotification));    
			SetActivityStatus("");		
			base.ScreenId="306_0";
			
			lblMessage.Visible = false;

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnAddClaimant.CmsButtonClass	=	CmsButtonType.Write;
			btnAddClaimant.PermissionString		=	gstrSecurityXML;

			//btnRescind.CmsButtonClass	=	CmsButtonType.Write;
			//btnRescind.PermissionString		=	gstrSecurityXML;
			
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			btnMatchPolicy.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnMatchPolicy.PermissionString	=	gstrSecurityXML;

			btnReserves.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReserves.PermissionString	=	gstrSecurityXML;

			btnAuthorizeTransaction.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnAuthorizeTransaction.PermissionString	=	gstrSecurityXML;

			btnReOpenClaims.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReOpenClaims.PermissionString	=	gstrSecurityXML;

			btnManageTrackActivity.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnManageTrackActivity.PermissionString	=	gstrSecurityXML;

			btnCompleteActivity.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnCompleteActivity.PermissionString	=	gstrSecurityXML;
			
			//btnLINKED_TO_CLAIM.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			//btnLINKED_TO_CLAIM.PermissionString	=	gstrSecurityXML;
						
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddClaimsNotification" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			//Execute txtLossDate onChange event when user moves from loss date textfield
			//if (Request.Form["__EVENTTARGET"] == "LossDateBlur")
			//{   
				//txtLOSS_DATE_TextChanged();		
			//}

			//Execute txtLossDate onChange event when user moves from loss date textfield
			if (Request.Form["__EVENTTARGET"] == "DeactivateActivity")
			{   
				DeactivateActivity();						
			}

            
			if(!Page.IsPostBack)
			{				
				cmbCATASTROPHE_EVENT_CODE.Attributes.Add("onChange","javascript:cmbCATASTROPHE_Changed()");
				imgSelect.Attributes.Add("onclick","javascript:OpenClaimsLookup('')");
				imgSelectPinkSlipNotifyUsers.Attributes.Add("onClick","javascript:OpenNotesWindow()");
                SetCaptions();
				GetQueryStringValues();
				LoadDropDowns();
                btnFNOL.Visible = false;
                if (GetLOBID() == ((int)enumLOB.PAPEACC).ToString())
                {
                    cmbIS_VICTIM_CLAIM.SelectedValue = "10963";
                }

				GetOldData(LOAD_DATA_FLAG);		
				BindCatastropheDropDown();
				//txtLOSS_DATE.Attributes.Add("onBlur","javascript:CheckLossDate();");
                //capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
				btnReOpenClaims.Attributes.Add("onClick","javascript: return GoToReopenClaims();");
				btnReserves.Attributes.Add("onClick","javascript : return GoToReserve();");				
				btnManageTrackActivity.Attributes.Add("onClick","javascript : return GoToActivity();");				
				btnAuthorizeTransaction.Attributes.Add("onClick","javascript : return GoToActivityAuthorize();");				
				//cmbCLAIMANT_INSURED.Attributes.Add("onChange","javascript: return cmbCLAIMANT_INSURED_Change();");
				//btnMatchPolicy.Attributes.Add("onClick","javascript : return RedirectToSearchPolicy();");
				hlkLOSS_DATE.Attributes.Add("OnClick","temp=0;fPopCalendar(document.CLM_CLAIM_INFO.txtLOSS_DATE,document.CLM_CLAIM_INFO.txtLOSS_DATE)"); //Javascript Implementation for Date				
				//hlkDIARY_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_CLAIM_INFO.txtDIARY_DATE,document.CLM_CLAIM_INFO.txtDIARY_DATE)"); //Javascript Implementation for Date
				hlkFIRST_NOTICE_OF_LOSS.Attributes.Add("OnClick","fPopCalendar(document.CLM_CLAIM_INFO.txtFIRST_NOTICE_OF_LOSS,document.CLM_CLAIM_INFO.txtFIRST_NOTICE_OF_LOSS)"); //Javascript Implementation for Date
                hlkLAST_DOC_RECEIVE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtLAST_DOC_RECEIVE_DATE,txtLAST_DOC_RECEIVE_DATE)"); //Javascript Implementation for Date
                hlkPOSSIBLE_PAYMENT_DATE.Attributes.Add("OnClick", "fPopCalendar(txtPOSSIBLE_PAYMENT_DATE,txtPOSSIBLE_PAYMENT_DATE)"); //Javascript Implementation for Date

                imgbtnUpdateDiary.ToolTip = objResourceMgr.GetString("imgbtnUpdateDiary");
                
                
				string url=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();
				//imgSelectVehicleModel.Attributes.Add("onclick",@"javascript:OpenLookup('"+url+"','Model','Model','','txtMODEL','ModelMC','ModelMC','@Manufacturer='+varLOB);");
																				
				//imgSelectAdjuster.Attributes.Add("onclick",@"javascript:OpenLookupProxy('" + url + "');");
				//imgSelectAdjuster.Attributes.Add("onclick",@"javascript:OpenLookup('" + url + "','SUB_ADJUSTER','SUB_ADJUSTER_CONTACT_NAME','txtSUB_ADJUSTER','txtSUB_ADJUSTER_CONTACT','ClaimsSubAdjuster','Claims SubAdjuster','@ADJUSTER_CODE=<%=hidADJUSTER_CODE.Value%>');");
				//				if(Request.QueryString["EXPERT_SERVICE_ID"]!=null && Request.QueryString["EXPERT_SERVICE_ID"].ToString()!="")
				//				{
				//					hidCLAIM_ID.Value = Request.QueryString["EXPERT_SERVICE_ID"].ToString();
				//					GetOldDataXML();
				//				}				
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				//btnReserves.Attributes.Add("onclick","javascript:return AddReserve();");				
				btnAddClaimant.Attributes.Add("onclick","javascript:return AddClaimant();");
               // btnSave.Attributes.Add("onClick", "javascript:Page_ClientValidate(); ");
				// Added by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 3.
				imgbtnUpdateDiary.Attributes.Add("onClick","javascript:OpenDiary();return false;");
                btnFNOL.Attributes.Add("OnClick", "javascript:return viewFNOLLetter();");
				//Set the Reported To value to current logged in user
                if (hidCLAIM_ID.Value == "" || hidCLAIM_ID.Value == "0")
				  txtREPORTED_TO.Text = ClsClaimsNotification.GetCurrentUserName(GetUserId());
				SetErrorMessages();					
				ShowClaimNumberForFirstTimeLoad();	
				CheckForReserves();
				CheckForAuthorisation();
				//SetClaimantFields();
				//CheckUserAuthorityLevel();

				if (Request["CLAIM_ID"] != null && Request["CLAIM_ID"]!="" && Convert.ToInt32(Request["CLAIM_ID"]) == 0)
				{
					//This code block will prefill the address details if the Claimant / Insured Option is selected as Yes.
					//					if (cmbCLAIMANT_INSURED.SelectedItem.Text.Trim().ToUpper().Equals("YES"))
					//					{
					//						cmbCLAIMANT_INSURED_SelectedIndexChanged(null,null); 
					//					}

					LoadInsuredDetails();
					//Set the Dairy Date default value to Current Date + 1 month
					//Modified by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 2.
					//txtDIARY_DATE.Text = DateTime.Today.Date.AddMonths(1).ToShortDateString(); 

					//Set the First Loss of date to Current Date
					txtFIRST_NOTICE_OF_LOSS.Text = DateTime.Today.Date.ToShortDateString();
				}
				hidDIARY_DATE.Value= txtDIARY_DATE.Text;
			}
			if(hidOldData.Value!="" && hidOldData.Value!="0")
				SetClaimCookies(ClsCommon.FetchValueFromXML("CLAIM_NUMBER",hidOldData.Value),hidCUSTOMER_ID.Value,hidPOLICY_ID.Value,hidPOLICY_VERSION_ID.Value,hidCLAIM_ID.Value,hidLOB_ID.Value);

            if (hidCLAIM_ID.Value != "" && hidCLAIM_ID.Value != "0")
            {
               
                if (hidCo_Insurance_Type.Value == "14548")
                {
                    if (lblCLAIM_OFFICIAL_NUMBER.Text == hidOFFICIAL_CLAIM_NUMBER.Value)
                    {
                        if (hidLOB_ID.Value == "9" || hidLOB_ID.Value == "10" || hidLOB_ID.Value == "11" || hidLOB_ID.Value == "13" || hidLOB_ID.Value == "14" || hidLOB_ID.Value == "16" || hidLOB_ID.Value == "17" || hidLOB_ID.Value == "18" || hidLOB_ID.Value == "20" || hidLOB_ID.Value == "21" || hidLOB_ID.Value == "23" || hidLOB_ID.Value == "27" || hidLOB_ID.Value == "29" || hidLOB_ID.Value == "31" || hidLOB_ID.Value == "33" || hidLOB_ID.Value == "34" || hidLOB_ID.Value == "35")
                        {
                            btnFNOL.Visible = true;
                        }
                    }
                    else
                    {
                        btnFNOL.Visible = false;
                    }
                }
                else
                    btnFNOL.Visible = false;
            }
		}
		#endregion
		#region Check Authority Level of the current user and set buttons accordingly
		private void CheckUserAuthorityLevel()
		{
			if(hidDUMMY_POLICY_ID.Value!="" && hidDUMMY_POLICY_ID.Value!="0" && hidDUMMY_POLICY_ID.Value.ToUpper()!="T")
			{
				bool dtAuthority = ClsMatchClaims.GetUserAuthorityLevel(GetUserId(),hidDUMMY_POLICY_ID.Value);
				if(!dtAuthority)			
				{
					//btnMatchPolicy.Visible = false;
					btnReserves.Visible = true;
				}
				else
				{
					//btnMatchPolicy.Visible = true;
					//btnReserves.Visible = false;
					btnReserves.Visible = true;
				}
			}
			else
			{
				//btnMatchPolicy.Visible = false;
				//btnReserves.Visible = true;
			}

			//			if((hidDUMMY_POLICY_ID.Value!="" && hidDUMMY_POLICY_ID.Value!="0") || hidDUMMY_POLICY_ID.Value.ToUpper()=="T")
			//				trCLAIMANT_INSURED.Attributes.Add("style","display:none");	
			
				


		}
		#endregion
		#region Check whether reserves for current claim have been added or not
		private void CheckForReserves()
		{
			//value of ""/0 indicates that reserves have not been added, need to fetch data 
			if(hidReserveAdded.Value=="" || hidReserveAdded.Value=="0")
			{
				hidReserveAdded.Value = ClsClaimsNotification.CheckForReservesAdded(hidCLAIM_ID.Value);
			}
			//if(hidReserveAdded.Value=="2")
			//	btnReserves.Visible = false;

		}
		#endregion
		#region Check whether any activities are awaiting authorisation of current user
		private void CheckForAuthorisation()
		{
			//value of ""/0 indicates that data has not fetched once, lets fetch it
			if(hidAuthorized.Value=="" || hidAuthorized.Value=="0")
			{
				hidAuthorized.Value = ClsClaimsNotification.CheckForActivitiesInQueue(hidCLAIM_ID.Value,GetUserId());
			}

		}
		#endregion
		#region GetQueryStringValues
		private void GetQueryStringValues()
		{
			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
			{
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"];
			}
			else
			{
				hidCUSTOMER_ID.Value = GetCustomerID();//Done for Itrack Issue 6821 on 15 Feb 2010
			}
			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="")
			{
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"];
			}			
			else
			{
				hidCUSTOMER_ID.Value = GetPolicyID();//Done for Itrack Issue 6821 on 15 Feb 2010
			}
			//First Take the policy version id from session as this may change at the page level itself
			//If not found in the session, then take it from QueryString itself
			if(GetPolicyVersionID()!=null && GetPolicyVersionID()!="" && SessionValueChanged!=-1)
				hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
			else
			{
				if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="")
					hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"];
			}			
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="" && Request.QueryString["CLAIM_ID"].ToString()!="0")
			{
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"];
			}
			else
				hidCLAIM_ID.Value="0";

            //if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "" && Request.QueryString["LOB_ID"].ToString() != "0")
            //{
            //    hidLOB_ID.Value = Request.QueryString["LOB_ID"];

            //}
            //else
                hidLOB_ID.Value = GetLOBID();
			
				

			if(Request["ADD_NEW"]!=null && Request["ADD_NEW"].ToString()=="0")
			{
				SetClaimAddNew("0");
			}


//			if(Request["ADD_NEW"]!=null && Request["ADD_NEW"].ToString()!="")
//				strAddNew = Request["ADD_NEW"].ToString();
//			else
//				strAddNew = "0";

			if(Request["HOMEOWNER"]!=null && Request["HOMEOWNER"].ToString()!="")
				hidHOMEOWNER.Value = Request["HOMEOWNER"].ToString();
			else
				hidHOMEOWNER.Value = "0";
			if(Request["RECR_VEH"]!=null && Request["RECR_VEH"].ToString()!="")
				hidRECR_VEH.Value = Request["RECR_VEH"].ToString();
			else
				hidRECR_VEH.Value = "0";
			if(Request["IN_MARINE"]!=null && Request["IN_MARINE"].ToString()!="")
				hidIN_MARINE.Value = Request["IN_MARINE"].ToString();
			else
				hidIN_MARINE.Value= "0";

			if(Request["LOSS_DATE"]!=null && Request["LOSS_DATE"].ToString()!="")			
				txtLOSS_DATE.Text = Request["LOSS_DATE"].ToString();

			if(Request["DUMMY_POLICY"]!=null && Request["DUMMY_POLICY"].ToString()!="")			
				hidDUMMY_POLICY_ID.Value = Request["DUMMY_POLICY"].ToString();
				


		}
		#endregion
		#region Display New Claim Number when first generated
		public void ShowClaimNumberForFirstTimeLoad()
		{
			if(GetClaimAddNew()=="1") //We have a case of new claim number generated, lets display it to the user
			{
                lblMessage.Text = objResourceMgr.GetString("Str") + " " + objResourceMgr.GetString("Str1") + " " + txtCLAIM_NUMBER.Text; ;
				lblMessage.Visible		=	true;
				SetClaimAddNew("0");
			}
		}
		#endregion
		#region GetOldDataXML
		private void GetOldData(bool flag)
		{
			DataSet dsOldData = new DataSet();
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
			{
				//dsOldData	=	ClsClaimsNotification.GetClaimsNotification(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidCLAIM_ID.Value));
				dsOldData	=	ClsClaimsNotification.GetClaimsNotification(int.Parse(hidCLAIM_ID.Value));
                Co_Insurance_Type = dsOldData.Tables[0].Rows[0]["CO_INSURANCE_TYPE"].ToString();
                hidCo_Insurance_Type.Value = Co_Insurance_Type;
				if(dsOldData!=null && dsOldData.Tables.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);					
					if(flag)
					{
						LoadData(dsOldData);	
						if(dsOldData.Tables[1]!=null && dsOldData.Tables[1].Rows.Count>0)
							CreateLinkedClaimString(dsOldData.Tables[1]);	
					}
					LoadPinkSlipDropDowns(dsOldData);
					if(dsOldData.Tables[2].Rows.Count>0)
					{
						hidLISTID.Value=dsOldData.Tables[2].Rows[0]["LISTID"].ToString();
						hidTOUSERID.Value=dsOldData.Tables[2].Rows[0]["TOUSERID"].ToString();
					}
                        
                    // Added by Santosh Kumar Gautam 
                    // IF any litigation is filled under this claim then disable cmbLITIGATION_FILE dropdown
                    if (dsOldData.Tables[3].Rows.Count > 0)
                    {
                        if (dsOldData.Tables[3].Rows[0]["HAS_LITIGATION"].ToString() == "1")
                            cmbLITIGATION_FILE.Enabled = false;
                        else
                            cmbLITIGATION_FILE.Enabled = true;
                    }

                    if (dsOldData.Tables.Count > 3 && dsOldData.Tables[4].Rows.Count > 0)
                    {
                        hidACTIVITY_ID.Value = dsOldData.Tables[4].Rows[0][0].ToString();
                        cmbIS_VICTIM_CLAIM.Enabled = false;
                    }

				}
				else
				{
					hidOldData.Value	=	"";		
					LoadPinkSlipDropDowns(null);
				}
				//Added by Asfa (22-Apr-2008) - iTrack issue #3697
				DataSet dsInsured = ClsAddPartyDetails.GetValuesForPartyTypes(hidCLAIM_ID.Value ,"10");	//10 - Insured
				if(dsInsured != null && dsInsured.Tables[0].Rows.Count>0)
				{
					ClsStates objState= new ClsStates();
					DataRow dr= dsInsured.Tables[0].Rows[0];
					lblINSURED_NAME.Text	= dr["NAME"].ToString();
					lblINSUREDADDRESS1.Text	= dr["ADDRESS1"].ToString();
					lblINSUREDADDRESS2.Text	= dr["ADDRESS2"].ToString();
					lblINSUREDCITY.Text		= dr["CITY"].ToString();
					lblINSUREDSTATE.Text	= ClsStates.GetStateList(dr["STATE"].ToString());
					lblINSUREDCOUNTRY.Text	= ClsCommon.GetCountryList(dr["COUNTRY"].ToString());
					lblINSUREDZIP.Text		= dr["ZIP"].ToString();
				}
			}
			else
			{
                txtCLAIM_NUMBER.Text = objResourceMgr.GetString("capGenerated");//"To be generated";				
				LoadPinkSlipDropDowns(null);
			}
		}
		#endregion
		private void CreateLinkedClaimString(DataTable dtLinkedClaims)
		{
			if(dtLinkedClaims!=null && dtLinkedClaims.Rows.Count>0)
			{
				hidLINKED_CLAIM_ID_LIST.Value = "";
				hidLINKED_CLAIM_LIST.Value = "";
				for(int iCounter=0;iCounter<dtLinkedClaims.Rows.Count;iCounter++)
				{
					if(dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_ID_LIST"]!=null && dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_ID_LIST"].ToString()!="" && dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_ID_LIST"].ToString()!="0")
						hidLINKED_CLAIM_ID_LIST.Value = hidLINKED_CLAIM_ID_LIST.Value + "^" + dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_ID_LIST"].ToString();

					if(dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_LIST"]!=null && dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_LIST"].ToString()!="" && dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_LIST"].ToString()!="0")
						hidLINKED_CLAIM_LIST.Value = hidLINKED_CLAIM_LIST.Value + "~" + dtLinkedClaims.Rows[iCounter]["LINKED_CLAIM_LIST"].ToString();
				}
				if(hidLINKED_CLAIM_ID_LIST.Value!="" && hidLINKED_CLAIM_ID_LIST.Value.Length>1)
					hidLINKED_CLAIM_ID_LIST.Value = hidLINKED_CLAIM_ID_LIST.Value.Substring(1,hidLINKED_CLAIM_ID_LIST.Value.Length-1);
				if(hidLINKED_CLAIM_LIST.Value!="" && hidLINKED_CLAIM_LIST.Value.Length>1)
					hidLINKED_CLAIM_LIST.Value = hidLINKED_CLAIM_LIST.Value.Substring(1,hidLINKED_CLAIM_LIST.Value.Length-1);
			}
		}
		
		#region LoadData
		private void LoadData(DataSet dsLoadData)
		{
			if(dsLoadData!=null && dsLoadData.Tables.Count>0)
			{

                //string CLAIM_CURRENCY_ID = dsLoadData.Tables[0].Rows[0]["CLAIM_CURRENCY_ID"].ToString();
                //if (!string.IsNullOrEmpty(CLAIM_CURRENCY_ID))
                //    SetClaimCurrency(CLAIM_CURRENCY_ID);

                

				txtCLAIM_NUMBER.Text = dsLoadData.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
               string CLAIM_OFFICIAL_NUMBER = dsLoadData.Tables[0].Rows[0]["OFFCIAL_CLAIM_NUMBER"].ToString();
               hidOFFICIAL_CLAIM_NUMBER.Value = CLAIM_OFFICIAL_NUMBER;
                if (!string.IsNullOrEmpty(CLAIM_OFFICIAL_NUMBER))
                {
                    lblCLAIM_OFFICIAL_NUMBER.Text = CLAIM_OFFICIAL_NUMBER;
                    btnOFFCIAL_CLAIM_NUMBER.Visible=false;
                }
                 else
                    btnOFFCIAL_CLAIM_NUMBER.Visible = true;
				//txtLOSS_DATE.Text	 = dsLoadData.Tables[0].Rows[0]["LOSS_DATE"].ToString().Trim();
				if (dsLoadData.Tables[0].Rows[0]["LOSS_TIME"] != DBNull.Value)
				{
					int hour = 0;
					hour = Convert.ToInt32(Convert.ToDateTime(dsLoadData.Tables[0].Rows[0]["LOSS_TIME"]).Hour.ToString().Trim());
					
					if (hour > 12)
					{
						hour = hour - 12;
					}
					
					txtLOSS_HOUR.Text =   hour.ToString().Trim();

					txtLOSS_MINUTE.Text = Convert.ToDateTime(dsLoadData.Tables[0].Rows[0]["LOSS_TIME"]).Minute.ToString().Trim();
					if(dsLoadData.Tables[0].Rows[0]["LOSS_TIME_AM_PM"]!=null && dsLoadData.Tables[0].Rows[0]["LOSS_TIME_AM_PM"].ToString()!="")
						cmbMERIDIEM.SelectedValue = dsLoadData.Tables[0].Rows[0]["LOSS_TIME_AM_PM"].ToString();
				}

				if (dsLoadData.Tables[0].Rows[0]["LOSS_DATE"] != DBNull.Value)
				{
					txtLOSS_DATE.Text = ConvertDBDateToCulture( dsLoadData.Tables[0].Rows[0]["LOSS_DATE"].ToString().Trim());
				}
				//Commented by Asfa - 30-Aug-2007
				/*
				  if(dsLoadData.Tables[0].Rows[0]["ADJUSTER_CODE"]!=null && dsLoadData.Tables[0].Rows[0]["ADJUSTER_CODE"].ToString()!="")
					cmbADJUSTER_ID.SelectedValue = dsLoadData.Tables[0].Rows[0]["ADJUSTER_CODE"].ToString();
				*/
				//Added by Asfa - 30-Aug-2007
				if(dsLoadData.Tables[0].Rows[0]["ADJUSTER_ID"]!=null && dsLoadData.Tables[0].Rows[0]["ADJUSTER_ID"].ToString()!="")
					cmbADJUSTER_ID.SelectedValue = dsLoadData.Tables[0].Rows[0]["ADJUSTER_ID"].ToString()+"^"+dsLoadData.Tables[0].Rows[0]["ADJUSTER_CODE"].ToString();
				//if(dsLoadData.Tables[0].Rows[0]["CATASTROPHE_EVENT_CODE"] != null && dsLoadData.Tables[0].Rows[0]["CATASTROPHE_EVENT_CODE"].ToString() != "0")
				//	cmbCATASTROPHE_EVENT_CODE.SelectedValue= dsLoadData.Tables[0].Rows[0]["CATASTROPHE_EVENT_CODE"].ToString();
				
				txtREPORTED_BY.Text = dsLoadData.Tables[0].Rows[0]["REPORTED_BY"].ToString();
				strCATASTROPHE_EVENT_CODE = dsLoadData.Tables[0].Rows[0]["CATASTROPHE_EVENT_CODE"].ToString();

                if (strCATASTROPHE_EVENT_CODE != "" && strCATASTROPHE_EVENT_CODE != "0")
                {
                    cmbCATASTROPHE_EVENT_CODE.Enabled = false;
                }

				//				if(dsLoadData.Tables[0].Rows[0]["CLAIMANT_INSURED"]!=null && dsLoadData.Tables[0].Rows[0]["CLAIMANT_INSURED"].ToString().ToUpper()=="TRUE") 
				//					cmbCLAIMANT_INSURED.SelectedIndex = 0;					
				//				else
				//					cmbCLAIMANT_INSURED.SelectedIndex = 1;
//				txtINSURED_RELATIONSHIP.Text = dsLoadData.Tables[0].Rows[0]["INSURED_RELATIONSHIP"].ToString();
				txtCLAIMANT_NAME.Text = dsLoadData.Tables[0].Rows[0]["CLAIMANT_NAME"].ToString();
				cmbCOUNTRY.SelectedValue = dsLoadData.Tables[0].Rows[0]["COUNTRY"].ToString();
				txtZIP.Text = dsLoadData.Tables[0].Rows[0]["ZIP"].ToString();
				txtADDRESS1.Text = dsLoadData.Tables[0].Rows[0]["ADDRESS1"].ToString();
				txtADDRESS2.Text = dsLoadData.Tables[0].Rows[0]["ADDRESS2"].ToString();
				txtCITY.Text = dsLoadData.Tables[0].Rows[0]["CITY"].ToString();
				txtHOME_PHONE.Text = dsLoadData.Tables[0].Rows[0]["HOME_PHONE"].ToString();
				txtWORK_PHONE.Text = dsLoadData.Tables[0].Rows[0]["WORK_PHONE"].ToString();
				txtMOBILE_PHONE.Text = dsLoadData.Tables[0].Rows[0]["MOBILE_PHONE"].ToString();
				txtWHERE_CONTACT.Text = dsLoadData.Tables[0].Rows[0]["WHERE_CONTACT"].ToString();
				txtWHEN_CONTACT.Text = dsLoadData.Tables[0].Rows[0]["WHEN_CONTACT"].ToString();
                txtDIARY_DATE.Text = ConvertDBDateToCulture(dsLoadData.Tables[0].Rows[0]["DIARY_DATE"].ToString().Trim());
                if (!string.IsNullOrEmpty( dsLoadData.Tables[0].Rows[0]["LAST_DOC_RECEIVE_DATE"].ToString()))
                    txtLAST_DOC_RECEIVE_DATE.Text = ConvertDBDateToCulture(dsLoadData.Tables[0].Rows[0]["LAST_DOC_RECEIVE_DATE"].ToString().Trim());

				//cmbCLAIM_STATUS.SelectedValue=dsLoadData.Tables[0].Rows[0]["CLAIM_STATUS"].ToString();
				cmbCLAIM_STATUS.SelectedIndex=cmbCLAIM_STATUS.Items.IndexOf(cmbCLAIM_STATUS.Items.FindByValue(dsLoadData.Tables[0].Rows[0]["CLAIM_STATUS"].ToString()));
				cmbCLAIM_STATUS_UNDER.SelectedIndex=cmbCLAIM_STATUS_UNDER.Items.IndexOf(cmbCLAIM_STATUS_UNDER.Items.FindByValue(dsLoadData.Tables[0].Rows[0]["CLAIM_STATUS_UNDER"].ToString()));
			    
				
				if(dsLoadData.Tables[0].Rows[0]["OUTSTANDING_RESERVE"]!=null && dsLoadData.Tables[0].Rows[0]["OUTSTANDING_RESERVE"].ToString()!="")
				{
					lblOUTSTANDING_RESERVE.Text= Double.Parse(dsLoadData.Tables[0].Rows[0]["OUTSTANDING_RESERVE"].ToString()).ToString("N");					
				}

				if(dsLoadData.Tables[0].Rows[0]["RESINSURANCE_RESERVE"]!=null && dsLoadData.Tables[0].Rows[0]["RESINSURANCE_RESERVE"].ToString()!="")
					lblRESINSURANCE_RESERVE.Text= Double.Parse(dsLoadData.Tables[0].Rows[0]["RESINSURANCE_RESERVE"].ToString()).ToString("N");
					//lblRESINSURANCE_RESERVE.Text=String.Format("{0:,#,###.##}",dsLoadData.Tables[0].Rows[0]["RESINSURANCE_RESERVE"]);
				
				if(dsLoadData.Tables[0].Rows[0]["PAID_LOSS"]!=null && dsLoadData.Tables[0].Rows[0]["PAID_LOSS"].ToString()!="")
					lblPAID_LOSS.Text= Double.Parse(dsLoadData.Tables[0].Rows[0]["PAID_LOSS"].ToString()).ToString("N");
					//lblPAID_LOSS.Text=String.Format("{0:,#,###.##}",dsLoadData.Tables[0].Rows[0]["PAID_LOSS"]);

				if(dsLoadData.Tables[0].Rows[0]["PAID_EXPENSE"]!=null && dsLoadData.Tables[0].Rows[0]["PAID_EXPENSE"].ToString()!="")
					lblPAID_EXPENSE.Text= Double.Parse(dsLoadData.Tables[0].Rows[0]["PAID_EXPENSE"].ToString()).ToString("N");
					//lblPAID_EXPENSE.Text=String.Format("{0:,#,###.##}",dsLoadData.Tables[0].Rows[0]["PAID_EXPENSE"]);

                // MODIFIED BY SANTOSH KR GAUTAM ON 23 AUG 2011 FOR ITRACK 1480
                if (dsLoadData.Tables[0].Rows[0]["RECOVERY_OUTSTANDING"] != null && dsLoadData.Tables[0].Rows[0]["RECOVERY_OUTSTANDING"].ToString() != "")
                    lblRECOVERIES.Text = Double.Parse(dsLoadData.Tables[0].Rows[0]["RECOVERY_OUTSTANDING"].ToString()).ToString("N");
               
					//lblRECOVERIES.Text=String.Format("{0:#,###.##}",dsLoadData.Tables[0].Rows[0]["RECOVERIES"]);
				
				
				txtCLAIM_DESCRIPTION.Text = dsLoadData.Tables[0].Rows[0]["CLAIM_DESCRIPTION"].ToString();
				//				txtSUB_ADJUSTER.Text = dsLoadData.Tables[0].Rows[0]["SUB_ADJUSTER"].ToString();
				//				txtSUB_ADJUSTER_CONTACT.Text = dsLoadData.Tables[0].Rows[0]["SUB_ADJUSTER_CONTACT"].ToString();
				txtEXTENSION.Text = dsLoadData.Tables[0].Rows[0]["EXTENSION"].ToString();
				//hidLINKED_CLAIM_ID_LIST.Value = dsLoadData.Tables[0].Rows[0]["LINKED_CLAIM_ID_LIST"].ToString();
				//hidLINKED_CLAIM_LIST.Value = dsLoadData.Tables[0].Rows[0]["LINKED_CLAIM_LIST"].ToString();
				if(dsLoadData.Tables[0].Rows[0]["DUMMY_POLICY_ID"]!=null && dsLoadData.Tables[0].Rows[0]["DUMMY_POLICY_ID"].ToString()!="")
					hidDUMMY_POLICY_ID.Value = dsLoadData.Tables[0].Rows[0]["DUMMY_POLICY_ID"].ToString();
                if (dsLoadData.Tables[0].Rows[0]["LITIGATION_FILE"] != null && dsLoadData.Tables[0].Rows[0]["LITIGATION_FILE"].ToString() != "")
                {
                    string strLitigation = dsLoadData.Tables[0].Rows[0]["LITIGATION_FILE"].ToString();
                    hidOLD_LITIGATION_FILE.Value = strLitigation;
                    hidCURRENT_LITIGATION_FILE.Value = strLitigation;
                    cmbLITIGATION_FILE.SelectedValue = strLitigation;
                    //cmbLITIGATION_FILE.Enabled = false;
                }
                hidOLD_IS_VICTIM_CLAIM.Value = dsLoadData.Tables[0].Rows[0]["IS_VICTIM_CLAIM"].ToString();
                hidCURRENT_IS_VICTIM_CLAIM.Value = dsLoadData.Tables[0].Rows[0]["IS_VICTIM_CLAIM"].ToString();

				if(dsLoadData.Tables[0].Rows[0]["STATE"]!=null && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["STATE"].ToString()!="0")
					cmbSTATE.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["STATE"].ToString();
				else
					cmbSTATE.SelectedIndex = 0;

				if(dsLoadData.Tables[0].Rows[0]["CLAIMANT_PARTY"]!=null && dsLoadData.Tables[0].Rows[0]["CLAIMANT_PARTY"].ToString()!="" && dsLoadData.Tables[0].Rows[0]["CLAIMANT_PARTY"].ToString()!="0")
					cmbCLAIMANT_PARTY.SelectedValue	=		dsLoadData.Tables[0].Rows[0]["CLAIMANT_PARTY"].ToString();
				else
					cmbCLAIMANT_PARTY.SelectedIndex = 0;

				//				if (dsLoadData.Tables[0].Rows[0]["LINKED_TO_CLAIM"]!= System.DBNull.Value)
				//				{
				//					txtLINKED_TO_CLAIM.Text = dsLoadData.Tables[0].Rows[0]["LINKED_TO_CLAIM"].ToString().Trim();					 
				//				}
				//				else
				//					txtLINKED_TO_CLAIM.Text = "";

				//				cmbADD_FAULT.SelectedValue = dsLoadData.Tables[0].Rows[0]["ADD_FAULT"].ToString();
				//				cmbTOTAL_LOSS.SelectedValue = dsLoadData.Tables[0].Rows[0]["TOTAL_LOSS"].ToString();
				cmbNOTIFY_REINSURER.SelectedValue = dsLoadData.Tables[0].Rows[0]["NOTIFY_REINSURER"].ToString();
				if (dsLoadData.Tables[0].Rows[0]["REPORTED_TO"] != DBNull.Value)
					txtREPORTED_TO.Text =  dsLoadData.Tables[0].Rows[0]["REPORTED_TO"].ToString();
				if (dsLoadData.Tables[0].Rows[0]["FIRST_NOTICE_OF_LOSS"] != DBNull.Value)
					txtFIRST_NOTICE_OF_LOSS.Text = ConvertDBDateToCulture(dsLoadData.Tables[0].Rows[0]["FIRST_NOTICE_OF_LOSS"].ToString().Trim());	
				//Done for Itrack Issue 6620 on 27 Nov 09
				if (dsLoadData.Tables[0].Rows[0]["AT_FAULT_INDICATOR"] != DBNull.Value)
					cmbAT_FAULT_INDICATOR.SelectedValue = dsLoadData.Tables[0].Rows[0]["AT_FAULT_INDICATOR"].ToString();

                if (dsLoadData.Tables[0].Rows[0]["REINSURANCE_TYPE"] != DBNull.Value)
                    cmbREINSURANCE_TYPE.SelectedValue = dsLoadData.Tables[0].Rows[0]["REINSURANCE_TYPE"].ToString();

                txtREIN_CLAIM_NUMBER.Text = dsLoadData.Tables[0].Rows[0]["REIN_CLAIM_NUMBER"].ToString();
                txtREIN_LOSS_NOTICE_NUM.Text = dsLoadData.Tables[0].Rows[0]["REIN_LOSS_NOTICE_NUM"].ToString();

                if (dsLoadData.Tables[0].Rows[0]["IS_VICTIM_CLAIM"] != DBNull.Value)
                    cmbIS_VICTIM_CLAIM.SelectedValue = dsLoadData.Tables[0].Rows[0]["IS_VICTIM_CLAIM"].ToString();

                if (!string.IsNullOrEmpty(dsLoadData.Tables[0].Rows[0]["POSSIBLE_PAYMENT_DATE"].ToString()))
                    txtPOSSIBLE_PAYMENT_DATE.Text = ConvertDBDateToCulture(dsLoadData.Tables[0].Rows[0]["POSSIBLE_PAYMENT_DATE"].ToString().Trim());

                txtFIRST_NOTICE_OF_LOSS.ReadOnly = true;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
          //  this.btnReserves.Click += new System.EventHandler(this.btnReserves_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.imgbtnUpdateDiary.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnUpdateDiary_Click); 

		}
		#endregion
		#region Delete Button Feature
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			//			int intRetVal;	
			//			ClsExpertServiceProviders objExpertServiceProviders	=	new ClsExpertServiceProviders();
			//			
			//			intRetVal = objExpertServiceProviders.Delete(int.Parse(hidCLAIM_ID.Value));
			//			if(intRetVal>0)
			//			{
			//				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");				
			//				hidFormSaved.Value = "5";
			//				hidOldData.Value = "";
			//				trBody.Attributes.Add("style","display:none");				
			//			}
			//			else if(intRetVal == -1)
			//			{
			//				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
			//				hidFormSaved.Value		=	"2";
			//			}
			//			lblDelete.Visible = true;
			
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
            ClsMessages.SetCustomizedXml(GetLanguageCode());
			
			rfvADJUSTER_CODE.ErrorMessage				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvCLAIM_STATUS.ErrorMessage				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
//			rfvCLAIM_STATUS_UNDER.ErrorMessage			=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"23");
			//revDIARY_DATE.ValidationExpression			=		aRegExpDate;
			//revDIARY_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            //revHOME_PHONE.ValidationExpression = aRegExpPhoneBrazil;
            //revHOME_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
			revLOSS_DATE.ValidationExpression			=		aRegExpDate;
			revLOSS_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            //revMOBILE_PHONE.ValidationExpression = aRegExpPhoneBrazil;
			//revMOBILE_PHONE.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1086");
            //revWORK_PHONE.ValidationExpression = aRegExpPhoneBrazil;
			//revWORK_PHONE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            revZIP.ValidationExpression = aRegExpZipBrazil; //aRegExpZip;			
			revZIP.ErrorMessage							=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");			
			rfvLOSS_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			//			rfvCLAIMANT_INSURED.ErrorMessage			=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//rfvDIARY_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");			
			//csvDIARY_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			csvLOSS_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			csvCLAIM_DESCRIPTION.ErrorMessage			=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");			
			rfvLOSS_HOUR.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			rfvLOSS_MINUTE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			rfvMERIDIEM.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			rfvCLAIMANT_PARTY.ErrorMessage				=	    Cms.CmsWeb.ClsMessages.FetchGeneralMessage("802");			 
			//csvDIARY_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			revFIRST_NOTICE_OF_LOSS.ValidationExpression=		aRegExpDate;
			revFIRST_NOTICE_OF_LOSS.ErrorMessage		=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			//Added by Asfa 31-Aug-2007
			csvADJUSTER_CODE.ErrorMessage				=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			rngEXTENSION.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");//Added for Itrack Issue 5641
            revMOBILE_PHONE.ValidationExpression = aRegExpAgencyPhone;
            revHOME_PHONE.ValidationExpression = aRegExpAgencyPhone;
            revWORK_PHONE.ValidationExpression = aRegExpAgencyPhone;
            revMOBILE_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1974");
            revHOME_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1974");
            revWORK_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1974");


            hidLOSSDATE_FUTUREDATE_ERRORMESSAGE.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "28");
            hidLOSSDATE_COMMON_ERRORMESSAGE.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");

            revLAST_DOC_RECEIVE_DATE.ValidationExpression = aRegExpDate;
            revLAST_DOC_RECEIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

            revPOSSIBLE_PAYMENT_DATE.ValidationExpression = aRegExpDate;
            revPOSSIBLE_PAYMENT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            rnvLOSS_HOUR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("771");
            rnvtLOSS_MINUTE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("772");
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            hidCANCELLED_POLICY_MESSAGE_1.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
            hidCANCELLED_POLICY_MESSAGE_2.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "30");
            hidNO_CORRESSPOND_DATE_MESSAGE.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");
            hidAS400_MESSAGE.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "32");
            hidCLAIM_EXISTS_ALERT_MSG.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "33");
            hidCLAIM_EXISTS_CONFIRM_MSG.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "34");

           
		}

		#endregion
		/*private string FetchValueFromXML(string claimNo,string XMLString)
		{
			try
			{
				string strClaimNo="";
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(XMLString);
				XmlNodeList claimNoNode=doc.GetElementsByTagName(claimNo);
			
				if(claimNoNode.Count>0)
				{
					strClaimNo=claimNoNode.Item(0).InnerText ;
					

				}
				return strClaimNo;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
			}
		}*/

		private void SetClaimCookies(string claimNo,string custID,string polId,string polVersionID,string claimID,string aLOBID)
		{
			//Added Mohit Agarwal 19-Feb 2007 to store last 3 cookies
			# region last 3 Items
			string AgencyId = GetSystemId();
			if(AgencyId.ToUpper() != CarrierSystemID )
			{
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
				DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(GetUserId()),AgencyId);

					//if(System.Web.HttpContext.Current.Request.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"] != null)
					if(ds!=null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0]!=null && ds.Tables[0].Rows[0]["LAST_VISITED_CLAIM"].ToString()!="")
					{
						string prevCook = ds.Tables[0].Rows[0]["LAST_VISITED_CLAIM"].ToString();
						string [] cookArr = prevCook.Split('@');
						if(cookArr.Length > 0 && cookArr.Length < 4)
						{
							//System.Web.HttpContext.Current.Response.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@" + System.Web.HttpContext.Current.Request.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
							string Claim_Details=claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@" + System.Web.HttpContext.Current.Request.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedClaim= new ClsGeneralInformation(); 
							objLastVisitedClaim.UpdateLastVisitedPageEntry("Claim",Claim_Details,int.Parse(GetUserId()),AgencyId);
						}
						else if(cookArr.Length >= 4)
						{
							int maxindex = cookArr.Length-1;
							if(maxindex >= 3)
								maxindex = 2;

							//System.Web.HttpContext.Current.Response.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
							string Claim_Details=claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedClaim= new ClsGeneralInformation(); 
							objLastVisitedClaim.UpdateLastVisitedPageEntry("Claim",Claim_Details,int.Parse(GetUserId()),AgencyId);
							for(int cookindex = 0; cookindex < maxindex; cookindex++)
							{
								//System.Web.HttpContext.Current.Response.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value += cookArr[cookindex] + "@";
								Claim_Details += cookArr[cookindex] + "@";
							}
							objLastVisitedClaim.UpdateLastVisitedPageEntry("Claim",Claim_Details,int.Parse(GetUserId()),AgencyId);
						}
						else
						{
							//System.Web.HttpContext.Current.Response.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value=claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
							string Claim_Details=claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
							Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedClaim= new ClsGeneralInformation(); 
							objLastVisitedClaim.UpdateLastVisitedPageEntry("Claim",Claim_Details,int.Parse(GetUserId()),AgencyId);
						}
						//System.Web.HttpContext.Current.Response.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
					}
					else
					{
						//System.Web.HttpContext.Current.Response.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value=claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
						//System.Web.HttpContext.Current.Response.Cookies["claimNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
						string Claim_Details=claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
						Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedClaim= new ClsGeneralInformation(); 
						objLastVisitedClaim.UpdateLastVisitedPageEntry("Claim",Claim_Details,int.Parse(GetUserId()),AgencyId);
					}
				}
					#endregion
				else
				{
					string Claim_Details=claimNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  claimID + "~" + aLOBID + "~" + DateTime.Today.Date;
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedApp= new ClsGeneralInformation(); 
					objLastVisitedApp.UpdateLastVisitedPageEntry("Claim",Claim_Details,int.Parse(GetUserId()),AgencyId);
				}
			
			
		}

		private void btnReOpenClaims_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function				
				ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();				

				intRetVal	= objClaimsNotification.UpdateStatus(int.Parse(hidCLAIM_ID.Value),(int)enumCLAIM_STATUS.OPEN);
				if( intRetVal > 0 )			// update successfully performed
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"17");
					GetOldData(LOAD_DATA_FLAG);
				}					
				else
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");					
				}					
				
				lblMessage.Visible = true;
			}
			catch(Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
			finally
			{
				
			}

		}

		private void btnReserves_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				if(hidReserveAdded.Value=="" || hidReserveAdded.Value=="0")
				{
					//For retreiving the return value of business class save function				
					ClsActivity  objActivity = new ClsActivity();
					ClsActivityInfo objActivityInfo = new ClsActivityInfo();
					objActivityInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					objActivityInfo.CREATED_BY = int.Parse(GetUserId());				
					objActivityInfo.ACTIVITY_REASON = (int)enumActivityReason.NEW_RESERVE;
					objActivityInfo.ACTIVITY_STATUS = (int)enumClaimActivityStatus.COMPLETE;

					intRetVal	= objActivity.Add(objActivityInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						hidACTIVITY_ID.Value = objActivityInfo.ACTIVITY_ID.ToString();
						hidReserveAdded.Value = "1";
						ClientScript.RegisterStartupScript(this.GetType(),"GoToReserve","<script>GoToReserve();</script>");
					}					
					else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");					
					}					
					
				}
				else
				{
					ClientScript.RegisterStartupScript(this.GetType(),"GoToReserve","<script>GoToReserve();</script>");
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
			finally
			{
				
			}

		}
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				string JavascriptText="";
				//Added by Asfa (09-Apr-2008) - iTrack issue #4036
                if (lblOUTSTANDING_RESERVE.Text != "" && lblOUTSTANDING_RESERVE.Text != "0.00" && lblOUTSTANDING_RESERVE.Text != "0,00" && cmbCLAIM_STATUS.SelectedValue == "11740")
				{
                    string Msg = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "27");
                    JavascriptText = "alert('" + Msg + "');";
					ClientScript.RegisterStartupScript(this.GetType(),"Test","<script>" + JavascriptText +"</script>");
					cmbCLAIM_STATUS.SelectedIndex=1;//To control the visiblity of btnReOpenClaims only when claim is closed
					return;
				}

				//For retreiving the return value of business class save function				
				ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();				

				#region COMMENTED CODE
				//Check the hidden variable whether the date of loss matched with any policy 
				//if((hidOldData.Value=="" || hidOldData.Value=="0") && hidLOSS_DATE_MATCHED.Value=="2") //Date of loss against current policy has not been matched, lets do it
				//	txtLOSS_DATE_TextChanged();
				//txtLOSS_DATE_TextChanged(null,null);

				//Commented 
				//if(hidLOSS_DATE_MATCHED.Value=="-1") //Not matched, the method called itself will display an alert message..
					//we just need to return from the function
				//{
				//	return;
				//}
				#endregion 
				
				//Retreiving the form values into model class object				
				ClsClaimsNotficationInfo objClaimsNotificationInfo = GetFormValue();
				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					
					objClaimsNotificationInfo.CREATED_BY = int.Parse(GetUserId());
					objClaimsNotificationInfo.CREATED_DATETIME = DateTime.Now;
					objClaimsNotificationInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objClaimsNotification.Add(objClaimsNotificationInfo);
					

					if(intRetVal>0)
					{
						hidCLAIM_ID.Value = objClaimsNotificationInfo.CLAIM_ID.ToString();
                       

                      
						//Save the Dummy Policy Record
						//**************Start*************
						
						if (Request["DUMMY_POLICY"] != null && Request["DUMMY_POLICY"].ToString().ToUpper().Trim() == "T")
						{
//							ClsDummyPolicyInfo objDummyPolicyInfo = new ClsDummyPolicyInfo();
//							objDummyPolicyInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
//							objDummyPolicyInfo.INSURED_NAME		=	Request["INSURED_NAME"].ToString();
//							objDummyPolicyInfo.EFFECTIVE_DATE	=	Convert.ToDateTime(Request["EFFECTIVE_DATE"].ToString());		
//							objDummyPolicyInfo.EXPIRATION_DATE	=	Convert.ToDateTime(Request["EXPIRATION_DATE"].ToString());		
//							objDummyPolicyInfo.NOTES			=	Request["NOTES"].ToString();
//							objDummyPolicyInfo.LOB_ID			=	int.Parse(Request["LOB"].ToString());
//							objDummyPolicyInfo.CREATED_BY = int.Parse(GetUserId());
//							objDummyPolicyInfo.CREATED_DATETIME = DateTime.Now;
//
//							string [] address = Request["ADDRESS"].ToString().Split('~');
//							if(address.Length >= 7)
//							{
//								objDummyPolicyInfo.POLICY_NUMBER = address[0];
//								objDummyPolicyInfo.DUMMY_ADD1 = address[1];
//								objDummyPolicyInfo.DUMMY_ADD2 = address[2];
//								objDummyPolicyInfo.DUMMY_CITY = address[3];
//								objDummyPolicyInfo.DUMMY_ZIP = address[4];
//								objDummyPolicyInfo.DUMMY_STATE = address[5];
//								objDummyPolicyInfo.DUMMY_COUNTRY = address[6];
//							}
//
//							ClsDummyPolicy objDummyPolicy = new ClsDummyPolicy();
//							int retVal = objDummyPolicy.Add(objDummyPolicyInfo);

							if (Request["DUMMY_POLICY_ID"] != null && Request["DUMMY_POLICY_ID"].ToString().Trim() != "")
							{
								ClsDummyPolicy objDummyPolicy = new ClsDummyPolicy();
								int retVal = objDummyPolicy.Update(int.Parse(hidCLAIM_ID.Value), int.Parse(Request["DUMMY_POLICY_ID"].ToString()));
							}
						}
						//**************End***************


						/*Commented on 08 Sep 2009 (Praveen Kasana)
						 * For only to display Claim Number has been genarated.
						 * Message will Display form ShowClaimNumberForFirstTimeLoad() Method.*/

						//lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						SetClaimAddNew("1");
						GetOldData(!LOAD_DATA_FLAG);
						SetStartUpScript("Reload_Save");

						
					}	
					else if(intRetVal==-2)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("848");
						hidFormSaved.Value			=	"2";
					}		
					else
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data					
					ClsClaimsNotficationInfo objOldClaimsNotificationInfo = new  ClsClaimsNotficationInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldClaimsNotificationInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objClaimsNotificationInfo.MODIFIED_BY = int.Parse(GetUserId());
					objClaimsNotificationInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objClaimsNotification.Update(objOldClaimsNotificationInfo,objClaimsNotificationInfo);
					if( intRetVal > 0 )			// update successfully performed
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
						hidFormSaved.Value		=	"1";
                        hidCURRENT_LITIGATION_FILE.Value = objClaimsNotificationInfo.LITIGATION_FILE.ToString();
                        hidCURRENT_IS_VICTIM_CLAIM.Value = objClaimsNotificationInfo.IS_VICTIM_CLAIM.ToString();

						GetOldData(!LOAD_DATA_FLAG);
					}
					else if(intRetVal==-2)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("848");
						hidFormSaved.Value			=	"2";
					}		
					else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
				lblMessage.Text	=	ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
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

		private void DeactivateActivity()
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function				
				ClsActivity objActivity = new ClsActivity();
				if(hidACTIVITY_ID.Value!="" && hidACTIVITY_ID.Value!="0")
				{
					intRetVal = objActivity.ActivateDeactivateActivity(null,hidCLAIM_ID.Value, hidACTIVITY_ID.Value,((int)enumClaimActivityStatus.DEACTIVATE).ToString(),"N");//Done for Itrack Issue 6932 on 1 Feb 2010
					//intRetVal = objActivity.ActivateDeactivateActivity(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,"N");
					if(intRetVal>0)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						//Set hidACTIVITYID to BLANK so that the ActivityTab does not open it
						hidACTIVITY_ID.Value="-1";
						ClientScript.RegisterStartupScript(this.GetType(),"Test","<script>GoToActivity();</script>");
					}					
					else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				}
			}
			catch(Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			    
			}
			finally
			{
				
			}

		}

		private void btnManageTrackActivity_Click(object sender, System.EventArgs e)
		{
			try
			{
				hidACTIVITY_ID.Value = ClsClaimsNotification.CheckForIncompleteActivity(hidCLAIM_ID.Value);
				string strScript="";
				if(hidACTIVITY_ID.Value!="" && hidACTIVITY_ID.Value!="0") 
				{
					//An Incomplete Activity Exists..Display the message box
					strScript = "<script>CheckIncompleteActivity();</script>";
				}
				else
				{
					//No Incomplete Activity exits, let the user go to Activity Index page
					//hidACTIVITY_ID.Value = "-1";
					strScript = "<script>GoToActivity();</script>";
				}
				ClientScript.RegisterStartupScript(this.GetType(),"Test",strScript);
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			    
			}
			finally
			{
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
		private void SetStartUpScript(string FunctionName)
		{
			//string strURL = "ClaimsTab.aspx?CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOB_ID="+ hidLOB_ID.Value + "&CLAIM_ID=" + hidCLAIM_ID.Value + "&";
			//RegisterStartupScript("ReLoadClaimsTab","<script>this.parent.location.href=" + strURL + ";</script>");			
			ClientScript.RegisterStartupScript(this.GetType(),"ReLoadClaimsTab","<script>" + FunctionName + "();</script>");			
		}

		private void SetCaptions()
		{
			capCLAIM_NUMBER.Text				=		objResourceMgr.GetString("txtCLAIM_NUMBER");
			capLOSS_DATE.Text					=		objResourceMgr.GetString("txtLOSS_DATE");
			capADJUSTER_CODE.Text				=		objResourceMgr.GetString("cmbADJUSTER_ID");
			capREPORTED_TO.Text					=		objResourceMgr.GetString("txtREPORTED_TO");
			capREPORTED_BY.Text					=		objResourceMgr.GetString("txtREPORTED_BY");
			capCATASTROPHE_EVENT_CODE.Text		=		objResourceMgr.GetString("cmbCATASTROPHE_EVENT_CODE");
			//			capCLAIMANT_INSURED.Text			=		objResourceMgr.GetString("cmbCLAIMANT_INSURED");
//			capINSURED_RELATIONSHIP.Text		=		objResourceMgr.GetString("txtINSURED_RELATIONSHIP");
			capCLAIMANT_NAME.Text				=		objResourceMgr.GetString("txtCLAIMANT_NAME");
			capINSURED_NAME.Text				=		objResourceMgr.GetString("txtCLAIMANT_NAME");
			capCOUNTRY.Text						=		objResourceMgr.GetString("cmbCOUNTRY");
			capINSUREDCOUNTRY.Text				=		objResourceMgr.GetString("cmbCOUNTRY");
			capZIP.Text							=		objResourceMgr.GetString("txtZIP");
			capINSUREDZIP.Text					=		objResourceMgr.GetString("txtZIP");
			capADDRESS1.Text					=		objResourceMgr.GetString("txtADDRESS1");
			capINSUREDADDRESS1.Text				=		objResourceMgr.GetString("txtADDRESS1");
			capADDRESS2.Text					=		objResourceMgr.GetString("txtADDRESS2");
			capINSUREDADDRESS2.Text				=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text						=		objResourceMgr.GetString("txtCITY");
			capINSUREDCITY.Text					=		objResourceMgr.GetString("txtCITY");
			capHOME_PHONE.Text					=		objResourceMgr.GetString("txtHOME_PHONE");
			capWORK_PHONE.Text					=		objResourceMgr.GetString("txtWORK_PHONE");
			capMOBILE_PHONE.Text				=		objResourceMgr.GetString("txtMOBILE_PHONE");			
			capWHERE_CONTACT.Text				=		objResourceMgr.GetString("txtWHERE_CONTACT");
			capWHEN_CONTACT.Text				=		objResourceMgr.GetString("txtWHEN_CONTACT");
			capDIARY_DATE.Text					=		objResourceMgr.GetString("txtDIARY_DATE");
			capCLAIM_STATUS.Text				=		objResourceMgr.GetString("cmbCLAIM_STATUS");
			capCLAIM_STATUS_UNDER.Text			=		objResourceMgr.GetString("cmbCLAIM_STATUS_UNDER");
			capOUTSTANDING_RESERVE.Text			=		objResourceMgr.GetString("lblOUTSTANDING_RESERVE");
			capRESINSURANCE_RESERVE.Text		=		objResourceMgr.GetString("lblRESINSURANCE_RESERVE");
			capPAID_LOSS.Text					=		objResourceMgr.GetString("lblPAID_LOSS");
			capPAID_EXPENSE.Text				=		objResourceMgr.GetString("lblPAID_EXPENSE");
			capRECOVERIES.Text					=		objResourceMgr.GetString("lblRECOVERIES");
			capCLAIM_DESCRIPTION.Text			=		objResourceMgr.GetString("txtCLAIM_DESCRIPTION");
			//			capSUB_ADJUSTER.Text				=		objResourceMgr.GetString("txtSUB_ADJUSTER");
			//			capSUB_ADJUSTER_CONTACT.Text		=		objResourceMgr.GetString("txtSUB_ADJUSTER_CONTACT");			
			capEXTENSION.Text					=		objResourceMgr.GetString("txtEXTENSION");	
			capLOSS_TIME.Text					=		objResourceMgr.GetString("txtLOSS_TIME");
			capLITIGATION_FILE.Text				=		objResourceMgr.GetString("cmbLITIGATION_FILE");
			capSTATE.Text						=		objResourceMgr.GetString("cmbSTATE");
			capINSUREDSTATE.Text				=		objResourceMgr.GetString("cmbSTATE");
			capCLAIMANT_PARTY.Text				=	    objResourceMgr.GetString("cmbCLAIMANT_PARTY");
			capLINKED_TO_CLAIM.Text				=		objResourceMgr.GetString("txtLINKED_CLAIM_ID_LIST");//Done for Itrack Issue 6932 on 10 Feb 2010 -- 'LINKED_TO_CLAIM' Changed To show Linked Claim in Transaction Log
			//			capADD_FAULT.Text					=		objResourceMgr.GetString("cmbADD_FAULT");
			//			capTOTAL_LOSS.Text					=		objResourceMgr.GetString("cmbTOTAL_LOSS");
			capNOTIFY_REINSURER.Text			=		objResourceMgr.GetString("cmbNOTIFY_REINSURER");
			capFIRST_NOTICE_OF_LOSS.Text		=		objResourceMgr.GetString("txtFIRST_NOTICE_OF_LOSS");
			capRECIEVE_PINK_SLIP_USERS_LIST.Text=		objResourceMgr.GetString("cmbRECIEVE_PINK_SLIP_USERS_LIST");
			capAGENCY_DISPLAY_NAME.Text			=		objResourceMgr.GetString("lblAGENCY_DISPLAY_NAME");
			capPINK_SLIP_TYPE_LIST.Text	=		objResourceMgr.GetString("cmbPINK_SLIP_TYPE_LIST");
			capAT_FAULT_INDICATOR.Text			=		objResourceMgr.GetString("cmbAT_FAULT_INDICATOR");//Done for Itrack Issue 6620 on 27 Nov 09
            capCLAIM_OFFICIAL_NUMBER.Text = objResourceMgr.GetString("lblCLAIM_OFFICIAL_NUMBER");
            btnManageTrackActivity.Text = objResourceMgr.GetString("btnManageTrackActivity");
            btnReserves.Text = objResourceMgr.GetString("btnReserves");
            btnAddClaimant.Text = objResourceMgr.GetString("btnAddClaimant");
            capCap.Text = objResourceMgr.GetString("capCap");

            capLAST_DOC_RECEIVE_DATE.Text = objResourceMgr.GetString("txtLAST_DOC_RECEIVE_DATE");
            
            btnOFFCIAL_CLAIM_NUMBER.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "25"); ;
            lblCLAIM_OFFICIAL_NUMBER.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "26"); ;

            capREINSURANCE_TYPE.Text = objResourceMgr.GetString("cmbREINSURANCE_TYPE");
            capREIN_CLAIM_NUMBER.Text = objResourceMgr.GetString("txtREIN_CLAIM_NUMBER");
            capREIN_LOSS_NOTICE_NUM.Text = objResourceMgr.GetString("txtREIN_LOSS_NOTICE_NUM");
            capIS_VICTIM_CLAIM.Text = objResourceMgr.GetString("cmbIS_VICTIM_CLAIM");

            capPOSSIBLE_PAYMENT_DATE.Text = objResourceMgr.GetString("txtPOSSIBLE_PAYMENT_DATE");
            btnReOpenClaims.Text = objResourceMgr.GetString("btnReOpenClaims");
            btnFNOL.Text = objResourceMgr.GetString("btnCOI");
		}
	
		#region GetFormValue
		private ClsClaimsNotficationInfo GetFormValue()
		{
			ClsClaimsNotficationInfo objClaimsNotficationInfo= new  ClsClaimsNotficationInfo();			
			objClaimsNotficationInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objClaimsNotficationInfo.POLICY_ID	 = int.Parse(hidPOLICY_ID.Value);
			objClaimsNotficationInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
            
			//objClaimsNotficationInfo.LOSS_DATE	=	Convert.ToDateTime(txtLOSS_DATE.Text.Trim());
			if (txtLOSS_DATE.Text != "")
			{
				DateTime LossDate = ConvertToDate(txtLOSS_DATE.Text);

				int Hr = int.Parse(txtLOSS_HOUR.Text);
				
				if(cmbMERIDIEM.SelectedIndex == 1)
				{
					Hr+=12;
				}
				if(Hr==24)
				{
					Hr=00;
				}

				objClaimsNotficationInfo.LOSS_DATE = new DateTime(LossDate.Year, LossDate.Month, LossDate.Day
					, Hr, int.Parse(txtLOSS_MINUTE.Text)
					, 0);
				if(cmbMERIDIEM.SelectedItem!=null && cmbMERIDIEM.SelectedItem.Value!="")
					objClaimsNotficationInfo.LOSS_TIME_AM_PM = int.Parse(cmbMERIDIEM.SelectedItem.Value);

			}
			//Added by Asfa - 30/Aug/2007
			if(cmbADJUSTER_ID.SelectedItem!=null && cmbADJUSTER_ID.SelectedItem.Value!="")
			{
				objClaimsNotficationInfo.ADJUSTER_CODE= hidADJUSTER_CODE.Value;
				objClaimsNotficationInfo.ADJUSTER_ID= int.Parse(hidADJUSTER_ID.Value);
			}

			objClaimsNotficationInfo.REPORTED_BY = txtREPORTED_BY.Text;
			if(cmbCATASTROPHE_EVENT_CODE.SelectedItem!=null && cmbCATASTROPHE_EVENT_CODE.SelectedItem.Value!="")
				objClaimsNotficationInfo.CATASTROPHE_EVENT_CODE = int.Parse(cmbCATASTROPHE_EVENT_CODE.SelectedItem.Value);
			//Commented For itrack Issue #7370.
			//if(hidCATASTROPHE_EVENT_CODE.Value != "")
			//	objClaimsNotficationInfo.CATASTROPHE_EVENT_CODE=int.Parse(hidCATASTROPHE_EVENT_CODE.Value);
			//			if(cmbCLAIMANT_INSURED.SelectedItem!=null && cmbCLAIMANT_INSURED.SelectedItem.Text!="")
			//			{
			//				if(cmbCLAIMANT_INSURED.SelectedItem.Text.ToUpper()=="YES")
			//					objClaimsNotficationInfo.CLAIMANT_INSURED = true;
			//				else
			//					objClaimsNotficationInfo.CLAIMANT_INSURED = false;
			//			}
//			objClaimsNotficationInfo.INSURED_RELATIONSHIP = txtINSURED_RELATIONSHIP.Text.Trim();

			objClaimsNotficationInfo.CLAIMANT_NAME = txtCLAIMANT_NAME.Text.Trim();

            if (hidCLAIMANT_TYPE.Value != "0" || hidCLAIMANT_TYPE.Value != "")
                objClaimsNotficationInfo.CLAIMANT_TYPE = int.Parse(hidCLAIMANT_TYPE.Value);

			if(cmbCOUNTRY.SelectedItem!=null && cmbCOUNTRY.SelectedItem.Value!="")
				objClaimsNotficationInfo.COUNTRY	= int.Parse(cmbCOUNTRY.SelectedItem.Value);
			objClaimsNotficationInfo.ZIP			=	txtZIP.Text.Trim();
			objClaimsNotficationInfo.ADDRESS1		=	txtADDRESS1.Text.Trim();
			objClaimsNotficationInfo.ADDRESS2		=	txtADDRESS2.Text.Trim();
			objClaimsNotficationInfo.CITY			=	txtCITY.Text.Trim();
			objClaimsNotficationInfo.HOME_PHONE		=	txtHOME_PHONE.Text.Trim();
			objClaimsNotficationInfo.WORK_PHONE		= txtWORK_PHONE.Text.Trim();
			objClaimsNotficationInfo.MOBILE_PHONE	= txtMOBILE_PHONE.Text.Trim();
			objClaimsNotficationInfo.WHERE_CONTACT	= txtWHERE_CONTACT.Text.Trim();
			objClaimsNotficationInfo.WHEN_CONTACT	= txtWHEN_CONTACT.Text.Trim();
			
			// Added by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 2.
			txtDIARY_DATE.Text= hidDIARY_DATE.Value;
			if(txtDIARY_DATE.Text != "")
				objClaimsNotficationInfo.DIARY_DATE		= ConvertToDate(txtDIARY_DATE.Text.Trim());
			else
                objClaimsNotficationInfo.DIARY_DATE = ConvertToDate(DateTime.Now.ToString());

			if(cmbCLAIM_STATUS.SelectedItem!=null && cmbCLAIM_STATUS.SelectedItem.Value!="")
				objClaimsNotficationInfo.CLAIM_STATUS	= int.Parse(cmbCLAIM_STATUS.SelectedItem.Value);
			if(cmbCLAIM_STATUS_UNDER.SelectedItem!=null && cmbCLAIM_STATUS_UNDER.SelectedItem.Value!="")
				objClaimsNotficationInfo.CLAIM_STATUS_UNDER	= int.Parse(cmbCLAIM_STATUS_UNDER.SelectedItem.Value);
			objClaimsNotficationInfo.CLAIM_DESCRIPTION	= txtCLAIM_DESCRIPTION.Text.Trim();
			//			objClaimsNotficationInfo.SUB_ADJUSTER = txtSUB_ADJUSTER.Text.Trim();
			//			objClaimsNotficationInfo.SUB_ADJUSTER_CONTACT = txtSUB_ADJUSTER_CONTACT.Text.Trim();
			objClaimsNotficationInfo.EXTENSION			= txtEXTENSION.Text.Trim();
			if(txtCLAIM_NUMBER.Text != "To be generated")
				objClaimsNotficationInfo.CLAIM_NUMBER	= txtCLAIM_NUMBER.Text.Trim();

			objClaimsNotficationInfo.HOMEOWNER	= hidHOMEOWNER.Value;
			objClaimsNotficationInfo.RECR_VEH	= hidRECR_VEH.Value;
			objClaimsNotficationInfo.IN_MARINE	= hidIN_MARINE.Value;

			if(cmbLITIGATION_FILE.SelectedItem!=null && cmbLITIGATION_FILE.SelectedItem.Value!="")
				objClaimsNotficationInfo.LITIGATION_FILE = int.Parse(cmbLITIGATION_FILE.SelectedItem.Value);
	
			if(hidCLAIM_ID.Value=="" || hidCLAIM_ID.Value=="0")
				strRowId="NEW";
			else
			{
				strRowId = hidCLAIM_ID.Value;
				objClaimsNotficationInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			}

			if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedValue!="")
				objClaimsNotficationInfo.STATE	=	int.Parse(cmbSTATE.SelectedValue);

			if(cmbCLAIMANT_PARTY.SelectedItem!=null && cmbCLAIMANT_PARTY.SelectedValue!="")
				objClaimsNotficationInfo.CLAIMANT_PARTY =	int.Parse(cmbCLAIMANT_PARTY.SelectedValue);

			//objClaimsNotficationInfo.LINKED_TO_CLAIM = txtLINKED_TO_CLAIM.Text.Trim();
			objClaimsNotficationInfo.LINKED_CLAIM_ID_LIST = hidLINKED_CLAIM_ID_LIST.Value;
			//			objClaimsNotficationInfo.ADD_FAULT = cmbADD_FAULT.SelectedValue;
			//			objClaimsNotficationInfo.TOTAL_LOSS = cmbTOTAL_LOSS.SelectedValue;
            if(cmbNOTIFY_REINSURER.SelectedValue!="")
			objClaimsNotficationInfo.NOTIFY_REINSURER = int.Parse(cmbNOTIFY_REINSURER.SelectedValue);
			if(hidLOB_ID.Value!="" && hidLOB_ID.Value!="0")
				objClaimsNotficationInfo.LOB_ID = hidLOB_ID.Value;
			
			objClaimsNotficationInfo.REPORTED_TO = txtREPORTED_TO.Text; 
			if(txtFIRST_NOTICE_OF_LOSS.Text.Trim()!="" && IsDate(txtFIRST_NOTICE_OF_LOSS.Text.Trim()))
				objClaimsNotficationInfo.FIRST_NOTICE_OF_LOSS  = ConvertToDate(txtFIRST_NOTICE_OF_LOSS.Text.Trim());

            if (txtLAST_DOC_RECEIVE_DATE.Text.Trim() != "" && IsDate(txtLAST_DOC_RECEIVE_DATE.Text.Trim()))
                objClaimsNotficationInfo.LAST_DOC_RECEIVE_DATE = ConvertToDate(txtLAST_DOC_RECEIVE_DATE.Text.Trim());

			if(cmbRECIEVE_PINK_SLIP_USERS_LIST.SelectedItem!=null && cmbRECIEVE_PINK_SLIP_USERS_LIST.SelectedItem.Value!="")			
				objClaimsNotficationInfo.RECIEVE_PINK_SLIP_USERS_LIST = Cms.BusinessLayer.BlCommon.ClsCommon.GetDelimitedValuesFromListbox(cmbRECIEVE_PINK_SLIP_USERS_LIST,STRING_DELIMITER_COMMA);

			if(cmbPINK_SLIP_TYPE_LIST.SelectedItem!=null && cmbPINK_SLIP_TYPE_LIST.SelectedItem.Value!="")			
				objClaimsNotficationInfo.PINK_SLIP_TYPE_LIST = Cms.BusinessLayer.BlCommon.ClsCommon.GetDelimitedValuesFromListbox(cmbPINK_SLIP_TYPE_LIST,STRING_DELIMITER_COMMA);
			objClaimsNotficationInfo.NEW_RECIEVE_PINK_SLIP_USERS_LIST = NewPinkSlipUsers(objClaimsNotficationInfo.RECIEVE_PINK_SLIP_USERS_LIST);
				
			//Done for Itrack Issue 6620 on 27 Nov 09
			if(cmbAT_FAULT_INDICATOR.SelectedItem!=null && cmbAT_FAULT_INDICATOR.SelectedItem.Value!="")
				objClaimsNotficationInfo.AT_FAULT_INDICATOR = int.Parse(cmbAT_FAULT_INDICATOR.SelectedItem.Value);

            if (cmbREINSURANCE_TYPE.SelectedValue != "")
                objClaimsNotficationInfo.REINSURANCE_TYPE = int.Parse(cmbREINSURANCE_TYPE.SelectedValue);
            else
                objClaimsNotficationInfo.REINSURANCE_TYPE = 0;

                objClaimsNotficationInfo.REIN_CLAIM_NUMBER = txtREIN_CLAIM_NUMBER.Text.Trim();
                objClaimsNotficationInfo.REIN_LOSS_NOTICE_NUM = txtREIN_LOSS_NOTICE_NUM.Text.Trim();

            if (cmbIS_VICTIM_CLAIM.SelectedValue != "")
                objClaimsNotficationInfo.IS_VICTIM_CLAIM = int.Parse(cmbIS_VICTIM_CLAIM.SelectedValue);
            else
                objClaimsNotficationInfo.IS_VICTIM_CLAIM = 0;


            if (txtPOSSIBLE_PAYMENT_DATE.Text.Trim() != "" && IsDate(txtPOSSIBLE_PAYMENT_DATE.Text.Trim()))
                objClaimsNotficationInfo.POSSIBLE_PAYMENT_DATE = ConvertToDate(txtPOSSIBLE_PAYMENT_DATE.Text.Trim());


			return objClaimsNotficationInfo;
		}
		#endregion
		//Gets the actual new users selected to recieve pink slip notification
		private string NewPinkSlipUsers(string SelectedUsers)
		{
			
			if(hidOldData.Value=="" || hidOldData.Value=="0")
				return SelectedUsers;
			else
			{
				if(SelectedUsers==null || SelectedUsers=="")
					return "";
			
				string strOldUsers = ClsCommon.FetchValueFromXML("RECIEVE_PINK_SLIP_USERS_LIST",hidOldData.Value);
				if(strOldUsers==null || strOldUsers=="")
					return SelectedUsers;

				if(strOldUsers==SelectedUsers)
					return "";

				string [] strOldUsersArray = strOldUsers.Split(STRING_DELIMITER_COMMA);
				string [] strSelectedUsersArray = SelectedUsers.Split(STRING_DELIMITER_COMMA);
				int iOldUsersLength = strOldUsersArray.Length;
				int iSelectedUsersLength = strSelectedUsersArray.Length;				
				System.Text.StringBuilder sbNewUsersArray = new System.Text.StringBuilder();
				bool NewValue=true;
				for(int iCounter=0;iCounter<iSelectedUsersLength;iCounter++)
				{
					NewValue = true;
					for(int jCounter=0;jCounter<iOldUsersLength;jCounter++)
					{
						if(strSelectedUsersArray[iCounter]==strOldUsersArray[jCounter])
						{
							NewValue = false;
							break;
						}
					}
					if(NewValue)
						sbNewUsersArray.Append(STRING_DELIMITER_COMMA + strSelectedUsersArray[iCounter].ToString());						
					
				}	
				if(sbNewUsersArray.Length>1)
					return sbNewUsersArray.ToString().Substring(1,sbNewUsersArray.Length-1).ToString();
				else
					return "";
			}

		}

		#region LoadPinkSlipDropDowns
		private void LoadPinkSlipDropDowns(DataSet dsLoadData)
		{
			int i=0;
			ClsClaimsNotification objClaimsNotify = new ClsClaimsNotification();
			DataSet dsPinkSlip = objClaimsNotify.GetPinkSlips(hidCLAIM_ID.Value);
			//Load the dropdown with values
            if (dsPinkSlip != null && dsPinkSlip.Tables.Count > 0)
			{
				if(dsPinkSlip.Tables.Count>i && dsPinkSlip.Tables[i]!=null && dsPinkSlip.Tables[i].Rows.Count>0)
				{
					cmbRECIEVE_PINK_SLIP_USERS_LIST.DataSource =  dsPinkSlip.Tables[i++];
					cmbRECIEVE_PINK_SLIP_USERS_LIST.DataTextField="USER_NAME";
					cmbRECIEVE_PINK_SLIP_USERS_LIST.DataValueField="USER_ID";
					cmbRECIEVE_PINK_SLIP_USERS_LIST.DataBind();	
				}
				if(dsPinkSlip.Tables.Count>i && dsPinkSlip.Tables[i]!=null && dsPinkSlip.Tables[i].Rows.Count>0)
				{
					cmbPINK_SLIP_TYPE_LIST.DataSource =  dsPinkSlip.Tables[i++];
					cmbPINK_SLIP_TYPE_LIST.DataTextField="LOOKUP_VALUE_DESC";
					cmbPINK_SLIP_TYPE_LIST.DataValueField="LOOKUP_UNIQUE_ID";
					cmbPINK_SLIP_TYPE_LIST.DataBind();	
				}					
			}
			//Select the existing values using old data
			if(dsLoadData!=null && dsLoadData.Tables.Count>0)
			{
                string strRECIEVE_PINK_SLIP_USERS_LIST ="";
                if (dsLoadData.Tables.Count > 0 && dsLoadData.Tables[0].Rows.Count > 0 && dsLoadData.Tables[0].Rows[0]["RECIEVE_PINK_SLIP_USERS_LIST"] != null)
                {
                    strRECIEVE_PINK_SLIP_USERS_LIST = dsLoadData.Tables[0].Rows[0]["RECIEVE_PINK_SLIP_USERS_LIST"].ToString();
                    if (strRECIEVE_PINK_SLIP_USERS_LIST != "" && strRECIEVE_PINK_SLIP_USERS_LIST != "0")
                    {
                        Cms.BusinessLayer.BlCommon.ClsCommon.SelectValuesAtListbox(cmbRECIEVE_PINK_SLIP_USERS_LIST, strRECIEVE_PINK_SLIP_USERS_LIST, STRING_DELIMITER_COMMA);
                    }
                }

                if (dsLoadData.Tables.Count > 0 && dsLoadData.Tables[0].Rows.Count > 0 && dsLoadData.Tables[0].Rows[0]["PINK_SLIP_TYPE_LIST"] != null)
                {
                    //Using the existing string variable for selecting pink slip types 				
                    strRECIEVE_PINK_SLIP_USERS_LIST = dsLoadData.Tables[0].Rows[0]["PINK_SLIP_TYPE_LIST"].ToString();
                    if (strRECIEVE_PINK_SLIP_USERS_LIST != "" && strRECIEVE_PINK_SLIP_USERS_LIST != "0")
                    {
                        Cms.BusinessLayer.BlCommon.ClsCommon.SelectValuesAtListbox(cmbPINK_SLIP_TYPE_LIST, strRECIEVE_PINK_SLIP_USERS_LIST, STRING_DELIMITER_COMMA);
                    }
                }
			}
		}		
		#endregion
		#region LoadDropDowns
		private void LoadDropDowns()
		{
			DataSet dsLookup = new DataSet();
			try
			{
				int i=0;
				dsLookup	=	ClsClaimsNotification.GetClaimsLookupData(hidCUSTOMER_ID.Value,txtLOSS_DATE.Text.Trim(),hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value,hidCLAIM_ID.Value,int.Parse(GetLOBID()));
				if(dsLookup!=null)
				{
					if(dsLookup.Tables.Count>i)
					{
						cmbADJUSTER_ID.DataSource				=	dsLookup.Tables[i++];
						cmbADJUSTER_ID.DataTextField			=	"ADJUSTER_NAME";
						cmbADJUSTER_ID.DataValueField			=	"ADJUSTER_ID_CODE";
						cmbADJUSTER_ID.DataBind();
						cmbADJUSTER_ID.Items.Insert(0,"");
						
						//						if(dsLookup.Tables[0].Rows.Count>0 && dsLookup.Tables[0].Rows[0]["SUB_ADJUSTER"]!=null)
						//						{
						//							txtSUB_ADJUSTER.Text		 = dsLookup.Tables[0].Rows[0]["SUB_ADJUSTER"].ToString();							
						//						}
						//						if(dsLookup.Tables[0].Rows.Count>0 && dsLookup.Tables[0].Rows[0]["SUB_ADJUSTER_PHONE"]!=null)
						//						{							
						//							txtSUB_ADJUSTER_CONTACT.Text = dsLookup.Tables[0].Rows[0]["SUB_ADJUSTER_PHONE"].ToString();
						//						}
					}
					/*if(dsLookup.Tables.Count>1)
					{
						cmbCATASTROPHE_EVENT_CODE.DataSource	=	dsLookup.Tables[1];
						cmbCATASTROPHE_EVENT_CODE.DataValueField=	"CATASTROPHE_EVENT_ID";
						cmbCATASTROPHE_EVENT_CODE.DataTextField=	"DESCRIPTION";
						cmbCATASTROPHE_EVENT_CODE.DataBind();
						//cmbCATASTROPHE_EVENT_CODE.Items.Add(new ListItem("Other","0"));
					}*/
					if(dsLookup.Tables.Count>i)
					{
						cmbCOUNTRY.DataSource					=	dsLookup.Tables[i++];
						cmbCOUNTRY.DataValueField				=	"COUNTRY_ID";
						cmbCOUNTRY.DataTextField				=	"COUNTRY_NAME";
						cmbCOUNTRY.DataBind();					
					}
					if(dsLookup.Tables.Count>i)
					{
                        i++;
                        //cmbCLAIM_STATUS.DataSource				=	dsLookup.Tables[i++];
                        //cmbCLAIM_STATUS.DataValueField			=	"LOOKUP_UNIQUE_ID";
                        //cmbCLAIM_STATUS.DataTextField			=	"LOOKUP_VALUE_DESC";
                        //cmbCLAIM_STATUS.DataBind();
                        //cmbCLAIM_STATUS.Items.Insert(0,"");
                        //cmbCLAIM_STATUS.SelectedIndex = 1;

                        cmbCLAIM_STATUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLMST");
                        cmbCLAIM_STATUS.DataValueField = "LookupID";
                        cmbCLAIM_STATUS.DataTextField = "LookupDesc";
                        cmbCLAIM_STATUS.DataBind();
                        cmbCLAIM_STATUS.Items.Insert(0, "");
                        if (cmbCLAIM_STATUS.Items.FindByValue("11739") != null)
                            cmbCLAIM_STATUS.SelectedValue = "11739";
                        else
                            cmbCLAIM_STATUS.SelectedIndex = 0;
					}
					if(dsLookup.Tables.Count>i)
					{
                        cmbCLAIM_STATUS_UNDER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLMSTU");
                        cmbCLAIM_STATUS_UNDER.DataValueField = "LookupID";
                        cmbCLAIM_STATUS_UNDER.DataTextField = "LookupDesc";
						cmbCLAIM_STATUS_UNDER.DataBind();
						cmbCLAIM_STATUS_UNDER.Items.Insert(0,"");
						cmbCLAIM_STATUS_UNDER.SelectedIndex = 0;
					}

                    // Added by Santosh Kumar Gautam on 17 dec 2010
                    cmbLITIGATION_FILE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
                    cmbLITIGATION_FILE.DataTextField = "LookupDesc";
                    cmbLITIGATION_FILE.DataValueField = "LookupID";
                    cmbLITIGATION_FILE.DataBind();

                    IList YesNoList = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
                    cmbAT_FAULT_INDICATOR.DataSource = YesNoList;
                    cmbAT_FAULT_INDICATOR.DataTextField = "LookupDesc";
                    cmbAT_FAULT_INDICATOR.DataValueField = "LookupID";
                    cmbAT_FAULT_INDICATOR.DataBind();
                    
                    cmbIS_VICTIM_CLAIM.DataSource = YesNoList;
                    cmbIS_VICTIM_CLAIM.DataTextField = "LookupDesc";
                    cmbIS_VICTIM_CLAIM.DataValueField = "LookupID";
                    cmbIS_VICTIM_CLAIM.DataBind();
                    
                    
                    ListItem list = new ListItem("", "");
                    cmbAT_FAULT_INDICATOR.Items.Insert(0,list);                    
                    cmbAT_FAULT_INDICATOR.SelectedIndex = 1;//Done for Itrack Issue 7369 on 23 Sept 2010


                    cmbCLAIMANT_PARTY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLM_PT");
                    cmbCLAIMANT_PARTY.DataValueField = "LookupID";
                    cmbCLAIMANT_PARTY.DataTextField = "LookupDesc";
                    cmbCLAIMANT_PARTY.DataBind();
                    cmbCLAIMANT_PARTY.Items.Insert(0, "");
                    cmbCLAIMANT_PARTY.SelectedIndex = 1;

					/*cmbCLAIMANT_PARTY.Items.Insert(0,"");
					cmbCLAIMANT_PARTY.Items.Insert(1,"First Party");
					cmbCLAIMANT_PARTY.Items[1].Value = "1";
					cmbCLAIMANT_PARTY.Items.Insert(2,"Third Party");
					cmbCLAIMANT_PARTY.Items[2].Value = "2";*
					
					/*if(dsLookup.Tables.Count>4 && dsLookup.Tables[4]!=null && dsLookup.Tables[4].Rows.Count>0)
					{
						cmbRECIEVE_PINK_SLIP_USERS_LIST.DataSource =  dsLookup.Tables[4];
						cmbRECIEVE_PINK_SLIP_USERS_LIST.DataTextField="USER_NAME";
						cmbRECIEVE_PINK_SLIP_USERS_LIST.DataValueField="USER_ID";
						cmbRECIEVE_PINK_SLIP_USERS_LIST.DataBind();	
					}*/
					if(dsLookup.Tables.Count>i && dsLookup.Tables[i]!=null && dsLookup.Tables[i].Rows.Count>0 && dsLookup.Tables[i].Rows[0]["AGENCY_DISPLAY_NAME"]!=null && dsLookup.Tables[i].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()!="")
					{
						lblAGENCY_DISPLAY_NAME.Text = dsLookup.Tables[i++].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
					}
					else
					{
						i++;
					}
					if(dsLookup.Tables.Count>i)
					{
						
					}
					//					if(dsLookup.Tables.Count>4)
					//					{
					//						cmbINSURED_RELATIONSHIP.DataSource			=	dsLookup.Tables[4];
					//						cmbINSURED_RELATIONSHIP.DataValueField		=	"DETAIL_TYPE_ID";
					//						cmbINSURED_RELATIONSHIP.DataTextField		=	"DETAIL_TYPE_DESCRIPTION";
					//						cmbINSURED_RELATIONSHIP.DataBind();
					//						cmbINSURED_RELATIONSHIP.Items.Insert(0,"");
					//					}
					/*if(dsLookup.Tables.Count>4 && dsLookup.Tables[4].Rows.Count>0)
					{
						cmbCLAIMANT_INSURED.DataSource			=	dsLookup.Tables[4];
						cmbCLAIMANT_INSURED.DataValueField		=	"CUSTOMER_DATA";
						cmbCLAIMANT_INSURED.DataTextField		=	"YES_OPTION";
						cmbCLAIMANT_INSURED.DataBind();
						cmbCLAIMANT_INSURED.Items.Insert(0,new ListItem("No","0"));
						cmbCLAIMANT_INSURED.SelectedIndex = -1;
					}
					else
					{*/
					//						cmbCLAIMANT_INSURED.Items.Insert(0,new ListItem("Yes","0"));
					//						cmbCLAIMANT_INSURED.Items.Insert(1,new ListItem("No","1"));
					//						cmbCLAIMANT_INSURED.SelectedIndex = -1;
					//}
				}
				cmbMERIDIEM.Items.Insert(0,"AM");
				cmbMERIDIEM.Items[0].Value = "0";
				cmbMERIDIEM.Items.Insert(1,"PM");
				cmbMERIDIEM.Items[1].Value = "1";

                // Added by Santosh Kumar Gautam on 08 Feb 2011
                cmbREINSURANCE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RNTYPE");
                cmbREINSURANCE_TYPE.DataValueField = "LookupID";
                cmbREINSURANCE_TYPE.DataTextField = "LookupDesc";
                cmbREINSURANCE_TYPE.DataBind();
                cmbREINSURANCE_TYPE.Items.Insert(0, "");
                cmbREINSURANCE_TYPE.SelectedIndex = 0;
				//				cmbCLAIMANT_INSURED.Items.Insert(0,new ListItem("No","0"));
				//				cmbCLAIMANT_INSURED.Items.Insert(1,new ListItem("Yes","1"));
				//				cmbCLAIMANT_INSURED.SelectedIndex = 0;

               
				DataTable dtState	= Cms.CmsWeb.ClsFetcher.State;
				cmbSTATE.DataSource		= dtState;
				cmbSTATE.DataTextField	= "STATE_NAME";
				cmbSTATE.DataValueField	= "STATE_ID";
				cmbSTATE.DataBind();
				cmbSTATE.Items.Insert(0,"");

				//Done for Itrack Issue 6620 on 27 Nov 09
				

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible=true;
			}
			finally
			{
				dsLookup = null;
			}
			
		}
		#endregion
		private void BindCatastropheDropDown()
		{
           
            DateTime LossDate = new DateTime();

            if (txtLOSS_DATE.Text.Trim() != "")
            {

                LossDate = ConvertToDate(txtLOSS_DATE.Text.Trim());
            }

            DataTable dtCode = ClsClaimsNotification.GetCatastropheEventCodes(int.Parse(GetLOBID()));
			if(dtCode!=null && dtCode.Rows.Count>0)
			{
				cmbCATASTROPHE_EVENT_CODE.DataSource	=	dtCode;
				cmbCATASTROPHE_EVENT_CODE.DataValueField=	"CATASTROPHE_EVENT_ID";
				cmbCATASTROPHE_EVENT_CODE.DataTextField=	"DESCRIPTION";
				cmbCATASTROPHE_EVENT_CODE.DataBind();
				cmbCATASTROPHE_EVENT_CODE.Items.Insert(0,new ListItem("","0"));
				if(strCATASTROPHE_EVENT_CODE!="")
					cmbCATASTROPHE_EVENT_CODE.SelectedValue = strCATASTROPHE_EVENT_CODE;
			}
		}

		/// <summary>
		/// This Function is not in Use.
		/// </summary>
		private void txtLOSS_DATE_TextChanged()
		{
			try
			{
				//Check loss date entered against the policy effective and expiration date	
				DateTime lossDateTime ;
				if(txtLOSS_DATE.Text.Trim()=="") return;
				if(IsDate(txtLOSS_DATE.Text.Trim())==false)
					return;
				else
				{
					DateTime LossDate = ConvertToDate(txtLOSS_DATE.Text);

					int Hr = int.Parse(txtLOSS_HOUR.Text);
				
					if(cmbMERIDIEM.SelectedIndex == 1)
					{
						Hr+=12;
					}
					if(Hr==24)
					{
						Hr=00;
					}

					

					lossDateTime = new DateTime(LossDate.Year, LossDate.Month, LossDate.Day
						, Hr, int.Parse(txtLOSS_MINUTE.Text)
						, 0);
				}
				DataSet dsLossDate = ClsClaimsNotification.CheckLossDateAgainstPolicy(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),lossDateTime);
				if(dsLossDate!=null && dsLossDate.Tables.Count>1)
				{
					string strCode = @"<script>AlertMessage(0);</script>";
					// Added by Asfa(02-May-2008) - iTrack issue #4146
					if(Convert.ToInt32(dsLossDate.Tables[0].Rows[0]["ADDCLAIM"]) == -1)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        hidMessage.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1975");//"You are trying to add Claim against a Cancelled Policy!";
						hidLOSS_DATE_MATCHED.Value = "0";
						ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode);
						
					}
					else if(Convert.ToInt32(dsLossDate.Tables[0].Rows[0]["ADDCLAIM"]) == 0)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        hidMessage.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1976");//"Can't add claim, Policy is cancelled !";
						hidLOSS_DATE_MATCHED.Value = "-1";
						ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode);
						
					}
					else if(Convert.ToInt32(dsLossDate.Tables[0].Rows[0]["ADDCLAIM"]) == -2) //Loss Date does not exists
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        hidMessage.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1977");//"Loss Date does not exists, Please try other date!";//Done for Itrack Issue 5978 on 15 June 2009
						hidLOSS_DATE_MATCHED.Value = "-1";
						ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode);
						
					}
				
					if(dsLossDate.Tables.Count>1)
					{
						if(dsLossDate.Tables.Count>=3)
						{
							if(dsLossDate.Tables[2].Rows[0]["RESULT"]!=null && dsLossDate.Tables[2].Rows[0]["RESULT"].ToString()!="")
							{
								int intResult = int.Parse(dsLossDate.Tables[2].Rows[0]["RESULT"].ToString());
								//Set hidLOSS_DATE_MATCHED to indicate that atleast once the date of loss has been matched
								hidLOSS_DATE_MATCHED.Value = "0";
								//A value of 0 for intResult indicates that current policy is ok.
								if(intResult>0) //New Policy Version is Found corresponding to current data
								{
									//When customer_id and policy_id have been provided, ie not 0 or blank, then only change policy_version_id 
									if(hidCUSTOMER_ID.Value!="0" && hidCUSTOMER_ID.Value!="" && hidPOLICY_ID.Value!="0" && hidPOLICY_ID.Value!="")
									{
										//Set the variable to indicate that Session has been set
										SessionValueChanged=1;
										SetPolicyVersionID(intResult.ToString());
										hidPOLICY_VERSION_ID.Value = intResult.ToString();
										hidMessage.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
										//Refreshing the claim top to indicate that a new version of policy is being set
										ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode);
										SetStartUpScript("Reload_LossDate");
										hidLOSS_DATE_MATCHED.Value = "3";
										
									}								
								}
								else if(intResult<0) //No corresponding policy found, give an error
								{
									hidMessage.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21");
									//lblMessage.Visible=true;	
									hidLOSS_DATE_MATCHED.Value = "-1";
                                    ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode);
									//SetStartUpScript("Reload_Save");
								}
	
								if(intResult==0)
								{
									hidMessage.Value = "";
									ClientScript.RegisterStartupScript(this.GetType(),"Test", "");	
								
								}
							}
						}
					}
					//if everything else fails, we have a good policy, lets leave it as it is
				}
			}				
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible=true;
			}
			finally{}
		}	
		
		//		private void SetClaimantFields()
		//		{
		//			if(cmbCLAIMANT_INSURED.SelectedItem!=null && cmbCLAIMANT_INSURED.SelectedItem.Text.ToUpper().Equals("YES"))
		//			{
		//				//Disable the fields						
		//				txtCLAIMANT_NAME.Enabled = txtADDRESS1.Enabled = txtADDRESS2.Enabled = cmbSTATE.Enabled = cmbCOUNTRY.Enabled = txtZIP.Enabled = txtCITY.Enabled = false;
		//			}
		//			else
		//			{
		//				//Enable the fields
		//				txtCLAIMANT_NAME.Enabled = txtADDRESS1.Enabled = txtADDRESS2.Enabled = cmbSTATE.Enabled = cmbCOUNTRY.Enabled = txtZIP.Enabled = txtCITY.Enabled = true;
		//			}
		//
		//		}
		private void LoadInsuredDetails()
		{
			DataSet dsClaimant = new DataSet();
			try
			{	
				if(hidCUSTOMER_ID.Value!="" && hidCUSTOMER_ID.Value!="0")
				{
					//Done for Itrack Issue 6761 on 27 Nov 09
					//dsClaimant = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(int.Parse(hidCUSTOMER_ID.Value));
					dsClaimant = Cms.BusinessLayer.BlClient.ClsApplicantInsued.GetPolicy_PrimaryApplicant_NameInsured(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
					if(dsClaimant!=null && dsClaimant.Tables.Count>0 && dsClaimant.Tables[0].Rows.Count>0)
					{
						//txtCLAIMANT_NAME.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() + " " + dsClaimant.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString()  + " " +  dsClaimant.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString(); 
						string strName="";
						//Done for Itrack Issue 6761 on 27 Nov 09
						strName = dsClaimant.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString().Trim();
//						strName = dsClaimant.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim()+ " ";
//						if(dsClaimant.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString()!="")
//							strName += dsClaimant.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString().Trim() + " ";
//						strName += dsClaimant.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString().Trim();
						
						txtCLAIMANT_NAME.Text =  strName;

                        hidCLAIMANT_TYPE.Value = dsClaimant.Tables[0].Rows[0]["APPLICANT_TYPE"].ToString();
						txtADDRESS1.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
						txtADDRESS2.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
						txtCITY.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString();
						if(dsClaimant.Tables[0].Rows[0]["CUSTOMER_STATE"]!=null && dsClaimant.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString()!="" && dsClaimant.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString()!="0")
							cmbSTATE.SelectedValue = dsClaimant.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString();
						txtZIP.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString();
						
						//Done for Itrack Issue 6761 on 27 Nov 09
//						if(dsClaimant.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString()==((int)enumCUSTOMER_TYPE.COMMERCIAL).ToString())
//						{
//							txtMOBILE_PHONE.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_MOBILE"].ToString();
//							txtEXTENSION.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_EXT"].ToString();
//							txtWORK_PHONE.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString();
//						}
//						else
//						{
//							txtMOBILE_PHONE.Text = dsClaimant.Tables[0].Rows[0]["PER_CUST_MOBILE"].ToString();
//						}
						txtMOBILE_PHONE.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_MOBILE"].ToString();
						txtHOME_PHONE.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_HOME_PHONE"].ToString();
						txtEXTENSION.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_EXT"].ToString();
						txtWORK_PHONE.Text = dsClaimant.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString();
						if(dsClaimant.Tables[0].Rows[0]["CUSTOMER_COUNTRY"]!=null && dsClaimant.Tables[0].Rows[0]["CUSTOMER_COUNTRY"].ToString()!="" && dsClaimant.Tables[0].Rows[0]["CUSTOMER_COUNTRY"].ToString()!="0")
							cmbCOUNTRY.SelectedValue = dsClaimant.Tables[0].Rows[0]["CUSTOMER_COUNTRY"].ToString();
						
					}
				}
				else
				{
					//Added by Mohit Agarwal 4-Dec 2007
					if (Request["ADDRESS"] != null && Request["ADDRESS"]!="")
					{
						string [] address = Request["ADDRESS"].ToString().Split('~');
						if(address.Length >= 7)
						{
							if (Request["INSURED_NAME"] != null && Request["INSURED_NAME"]!="")
								txtCLAIMANT_NAME.Text = Request["INSURED_NAME"].ToString();
							txtADDRESS1.Text = address[1];
							txtADDRESS2.Text = address[2];
							txtCITY.Text = address[3];
							txtZIP.Text = address[4];
							cmbSTATE.SelectedValue = address[5];
							cmbCOUNTRY.SelectedValue = address[6];
						}
					}
				}
				//SetClaimantFields();
			}		
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dsClaimant!=null)
					dsClaimant.Dispose();
			}
		}

		#region Update Diary Entry
		private void imgbtnUpdateDiary_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Request["DIARY_ID"] != null && Request["DIARY_ID"].ToString() != "")
			{
				ClsDiary objDiary = new ClsDiary();
				int returnResult = objDiary.CompleteDiaryEntry(int.Parse(Request["DIARY_ID"].ToString()));
				if(returnResult>0)
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("86");
					lblMessage.Visible = true;
				}
			}
		}
		#endregion
		#region Validate Loss date --> AJAX CALL 
		/// <summary>
		/// New Function CheckLossDateAgainstPolicy() used in Place of Function txtLOSS_DATE_TextChanged()
		/// To validate the Loss Date according to Policy Effective Date and Policy Versions
		/// </summary>
		/// <param name="CUSTOMER_ID"></param>
		/// <param name="POLICY_ID"></param>
		/// <param name="POLICY_VERSION_ID"></param>
		/// <param name="LOSS_DATE"></param>
		/// <param name="LOSS_HOUR"></param>
		/// <param name="LOSS_MINUTE"></param>
		/// <param name="MERIDIEM"></param>
		/// <returns></returns>
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
		public int CheckLossDateAgainstPolicy(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, string LOSS_DATE,string LOSS_HOUR,string LOSS_MINUTE,string MERIDIEM)
		{
			int RetVal = 0;
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			
			try
			{
				//Calculate Date Time
				DateTime lossDateTime ;
                SetCultureThread(GetLanguageCode());
				if(LOSS_DATE.Trim()=="") return -10;
				if(IsDate(LOSS_DATE.Trim())==false)
					return -10;
				else
				{
                    DateTime LossDate = ConvertToDate(LOSS_DATE);

					int Hr = int.Parse(LOSS_HOUR);
				
					if(int.Parse(MERIDIEM) == 1)
					{
						Hr+=12;
					}
					if(Hr==24)
					{
						Hr=00;
					}

					lossDateTime = new DateTime(LossDate.Year, LossDate.Month, LossDate.Day
						, Hr, int.Parse(LOSS_MINUTE)
						, 0);
				}

				DataSet dsClaim = obj.CheckLossDateAgainstPolicy(CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,lossDateTime);
				if(dsClaim!=null && dsClaim.Tables.Count > 0)
				{
					if(Convert.ToInt32(dsClaim.Tables[0].Rows[0]["ADDCLAIM"]) == -1)
						RetVal = -1;							
					else if(Convert.ToInt32(dsClaim.Tables[0].Rows[0]["ADDCLAIM"]) == 0)
						RetVal = 0;	
					else if(Convert.ToInt32(dsClaim.Tables[0].Rows[0]["ADDCLAIM"]) == -2) //Loss Date does not exists
						RetVal = -2;	
					else if(Convert.ToInt32(dsClaim.Tables[0].Rows[0]["ADDCLAIM"]) == -5) //Policy from AS400
						RetVal = -5;
                    else if (Convert.ToInt32(dsClaim.Tables[0].Rows[0]["ADDCLAIM"]) == -8) //Policy from AS400
                    {                       
                        RetVal = -8;
                    }
                    else if (Convert.ToInt32(dsClaim.Tables[0].Rows[0]["ADDCLAIM"]) == 1) //Itrack 7204
                        RetVal = -4; //Allow toAdd Claim
					if(dsClaim.Tables.Count>1)
					{
						if(dsClaim.Tables.Count>=2)
						{

                            if (dsClaim.Tables[1].Columns["IS_DUPLICATE_CLAIM_EXIST"] != null && dsClaim.Tables[1].Rows[0]["IS_DUPLICATE_CLAIM_EXIST"].ToString() != "0" && dsClaim.Tables[1].Rows[0]["IS_DUPLICATE_CLAIM_EXIST"].ToString() != "")
                            {
                                RetVal = -15; // SOME CLAIM WITH SAME LOSS DATE ALREADY EXISTS FOR PROVIDED POLICY	
                                return RetVal;
                            }

                            if (dsClaim.Tables[2].Columns["RESULT"] != null && dsClaim.Tables[2].Rows[0]["RESULT"].ToString() != "")
							{
								int intResult = int.Parse(dsClaim.Tables[2].Rows[0]["RESULT"].ToString());
									
								//A value of 0 for intResult indicates that current policy is ok.
								if(intResult>0) //New Policy Version is Found corresponding to current data
								{
									RetVal = intResult;									
								}
								else if(intResult<0) //No corresponding policy found, give an error
								{
									RetVal = -3;
								}
	
								if(intResult==0)
								{
									RetVal = -4; //Allow to add claim															
								}
							}
						}
					}
				}
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			return RetVal;
			
		}
		#endregion END AJAX CALL 

        protected void btnOFFCIAL_CLAIM_NUMBER_Click(object sender, EventArgs e)
        {
            //For retreiving the return value of business class save function				
            ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();

            DateTime FNOLDate = DateTime.MinValue;
            int AdjusterID= int.Parse(hidADJUSTER_ID.Value);
            int LangID=int.Parse( GetLanguageID());
            DateTime LossDate = ConvertToDate(txtLOSS_DATE.Text);
            if(txtFIRST_NOTICE_OF_LOSS.Text!="")
            FNOLDate= ConvertToDate(txtFIRST_NOTICE_OF_LOSS.Text);

            string ClaimNumber = objClaimsNotification.GenerateOfficialClaimNumber(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value, LossDate,FNOLDate, hidCLAIM_ID.Value, AdjusterID, LangID);
            if (!string.IsNullOrEmpty(ClaimNumber))
            {
                lblCLAIM_OFFICIAL_NUMBER.Text = ClaimNumber;
                btnOFFCIAL_CLAIM_NUMBER.Visible = false;
            }
            
            if (hidCo_Insurance_Type.Value == "14548")
            {
                if (lblCLAIM_OFFICIAL_NUMBER.Text == ClaimNumber)
                {
                    if (hidLOB_ID.Value == "9" || hidLOB_ID.Value == "10" || hidLOB_ID.Value == "11" || hidLOB_ID.Value == "13" || hidLOB_ID.Value == "14" || hidLOB_ID.Value == "16" || hidLOB_ID.Value == "17" || hidLOB_ID.Value == "18" || hidLOB_ID.Value == "20" || hidLOB_ID.Value == "21" || hidLOB_ID.Value == "23" || hidLOB_ID.Value == "27" || hidLOB_ID.Value == "29" || hidLOB_ID.Value == "31" || hidLOB_ID.Value == "33" || hidLOB_ID.Value == "34" || hidLOB_ID.Value == "35")
                    {
                        btnFNOL.Visible = true;
                    }
                }
                else
                {
                    btnFNOL.Visible = false;
                }
            }
            else
                btnFNOL.Visible = false;
        }

        protected void cmbCOUNTRY0_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbREINSURANCE_TYPE0_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
