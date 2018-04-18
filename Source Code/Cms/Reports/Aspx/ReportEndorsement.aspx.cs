/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> March 11,2005
	<End Date				: - >
	<Description			: - > This file is used to add agency details,show agency details, update agency details 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History
GeneralLedger_ AccountSummary.aspx
	<Modified Date			: - > 31/03/2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Removed labels and clssingleton messages,removing setting text of activate and deactivate button
    

	<Modified Date			: - > 1/04/2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changes done according to review sheet issues

    <Modified Date			: - > 3/04/2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changes done according to review sheet issues
    
    <Modified Date			: - > 05/04/2005    
	<Modified By			: - > Anurag Verma
	<Purpose				: - > making changes for activate/deactivate functionality and removing SCREENID and SetSecurityXML function from page
    
    <Modified Date			: - > 12/04/2005    
	<Modified By			: - > Anurag Verma
	<Purpose				: - > making changes for activate/deactivate button and changing error message for extension
	

	<Modified Date			: - > 12/05/2005    
	<Modified By			: - > Gaurav
	<Purpose				: - > Making changes to CmsButton permissions , from Execute to Write
	<Modified Date			: - > 15/07/2005    
	<Modified By			: - > Gaurav
	<Purpose				: - > New Field Added
	

	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page
	<Modified Date			: - > 17/11/2006    
	<Modified By			: - > Pravesh
	<Purpose				: - > Add Termination date renewal and termination Notice
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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 



namespace Reports.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class ReportEndorsement  : Cms.CmsWeb.cmsbase  
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtAGENCY_CODE;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_DISPLAY_NAME;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_LIC_NUM;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_ADD1;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_ADD2;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_STATE;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_COUNTRY;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_PHONE;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_EXT;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_FAX;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_WEBSITE;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_PAYMENT_METHOD;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_BILL_TYPE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_CODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_DISPLAY_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_PAYMENT_METHOD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_BILL_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_LIC_NUM;

		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_DISPLAY_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_WEBSITE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_LIC_NUM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_EXT;

		protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFEDERAL_ID;

		protected System.Web.UI.WebControls.Label lblMessage;


		protected System.Web.UI.WebControls.Label capAGENCY_CODE;
		protected System.Web.UI.WebControls.Label capAGENCY_DISPLAY_NAME;
		protected System.Web.UI.WebControls.Label capAGENCY_LIC_NUM;
		protected System.Web.UI.WebControls.Label capAGENCY_ADD1;
		protected System.Web.UI.WebControls.Label capAGENCY_ADD2;
		protected System.Web.UI.WebControls.Label capAGENCY_CITY;
		protected System.Web.UI.WebControls.Label capAGENCY_STATE;
		protected System.Web.UI.WebControls.Label capAGENCY_ZIP;
		protected System.Web.UI.WebControls.Label capAGENCY_COUNTRY;
		protected System.Web.UI.WebControls.Label capAGENCY_PHONE;
		protected System.Web.UI.WebControls.Label capAGENCY_EXT;
		protected System.Web.UI.WebControls.Label capAGENCY_FAX;
		protected System.Web.UI.WebControls.Label capAGENCY_EMAIL;
		protected System.Web.UI.WebControls.Label capAGENCY_WEBSITE;
		protected System.Web.UI.WebControls.Label capAGENCY_PAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capAGENCY_BILL_TYPE;
		public string  strSystemID="";
		protected string strCarrierSystemID="";
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label capPRINCIPAL_CONTACT;
		protected System.Web.UI.WebControls.TextBox txtPRINCIPAL_CONTACT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPRINCIPAL_CONTACT;
		protected System.Web.UI.WebControls.Label capOTHER_CONTACT;
		protected System.Web.UI.WebControls.TextBox txtOTHER_CONTACT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOTHER_CONTACT;
		protected System.Web.UI.WebControls.Label capFEDERAL_ID;
		protected System.Web.UI.WebControls.TextBox txtFEDERAL_ID;
		protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtROUTING_NUMBER;
		protected System.Web.UI.WebControls.Label capORIGINAL_CONTRACT_DATE;
		protected System.Web.UI.WebControls.Label capCURRENT_CONTRACT_DATE;
		protected System.Web.UI.WebControls.TextBox txtORIGINAL_CONTRACT_DATE;
		protected System.Web.UI.WebControls.TextBox txtCURRENT_CONTRACT_DATE;

		protected System.Web.UI.WebControls.HyperLink hlkORIGINAL_CONTRACT_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkCURRENT_CONTRACT_DATE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderWriter;
		protected System.Web.UI.WebControls.RegularExpressionValidator revROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvORIGINAL_CONTRACT_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revORIGINAL_CONTRACT_DATE;

		protected System.Web.UI.WebControls.CustomValidator csvCURRENT_CONTRACT_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCURRENT_CONTRACT_DATE;


		protected System.Web.UI.WebControls.Label capM_AGENCY_ADD_1;
		protected System.Web.UI.WebControls.TextBox txtM_AGENCY_ADD_1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_AGENCY_ADD_1;
		protected System.Web.UI.WebControls.Label capM_AGENCY_ADD_2;
		protected System.Web.UI.WebControls.TextBox txtM_AGENCY_ADD_2;
		protected System.Web.UI.WebControls.Label capM_AGENCY_CITY;
		protected System.Web.UI.WebControls.TextBox txtM_AGENCY_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_CITY;
		protected System.Web.UI.WebControls.Label capM_AGENCY_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbM_AGENCY_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_AGENCY_COUNTRY;
		protected System.Web.UI.WebControls.Label capM_AGENCY_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbM_AGENCY_STATE;
		protected System.Web.UI.WebControls.Label capM_AGENCY_ZIP;
		protected System.Web.UI.WebControls.TextBox txtM_AGENCY_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_ZIP;
		//protected System.Web.UI.WebControls.Label capM_AGENCY_PHONE;
		//protected System.Web.UI.WebControls.TextBox txtM_AGENCY_PHONE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_PHONE;
		//protected System.Web.UI.WebControls.Label capM_AGENCY_EXT;
		//protected System.Web.UI.WebControls.TextBox txtM_AGENCY_EXT;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_EXT;
		//protected System.Web.UI.WebControls.Label capM_AGENCY_FAX;
		//protected System.Web.UI.WebControls.TextBox txtM_AGENCY_FAX;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_FAX;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyPhysicalAddress;
		protected System.Web.UI.WebControls.Label lblCopy_Address;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.WebControls.TextBox txtTERMINATION_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_DATE;
		//protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_REASON;

		protected System.Web.UI.WebControls.RegularExpressionValidator revTERMINATION_DATE;
		protected System.Web.UI.HtmlControls.HtmlTableRow rowTermination;
		protected System.Web.UI.WebControls.Label capAGENCY_SPEED_DIAL;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_SPEED_DIAL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_SPEED_DIAL;
		protected System.Web.UI.WebControls.Label capAGENCY_DBA;
		protected System.Web.UI.WebControls.Label capAgencyDecName;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_DBA;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_DBA;
		protected System.Web.UI.WebControls.Label capAGENCY_COMBINED_CODE;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_COMBINED_CODE;
		protected System.Web.UI.WebControls.Label capBANK_NAME;
		protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_NAME;
		protected System.Web.UI.WebControls.Label capBANK_BRANCH;
		protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_BRANCH;
		protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER1;
		protected System.Web.UI.WebControls.TextBox txtBANK_ACCOUNT_NUMBER1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_ACCOUNT_NUMBER1;
		protected System.Web.UI.WebControls.Label capROUTING_NUMBER1;
		protected System.Web.UI.WebControls.Label capROUTING_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtROUTING_NUMBER1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revROUTING_NUMBER1;
		protected System.Web.UI.WebControls.Label capNOTES;
		protected System.Web.UI.WebControls.TextBox txtNOTES;
		protected System.Web.UI.WebControls.Label capNUM_AGENCY_CODE;
		protected System.Web.UI.WebControls.TextBox txtNUM_AGENCY_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNUM_AGENCY_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbAgencyNameDecPage;
		protected System.Web.UI.WebControls.Label capTERMINATION_DATE_RENEW;
		protected System.Web.UI.WebControls.Label capTERMINATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtTERMINATION_DATE_RENEW;
		protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_DATE_RENEW;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTERMINATION_DATE_RENEW;
		protected System.Web.UI.WebControls.Label capTERMINATION_NOTICE;
		protected System.Web.UI.WebControls.DropDownList cmbTERMINATION_NOTICE;
		
		//Defining the business layer class object
		ClsAgency  objAgency ;
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
			rfvAGENCY_DISPLAY_NAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"65");
			rfvAGENCY_CODE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"38");
			rfvAGENCY_ADD1.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
			//rfvAGENCY_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			rfvAGENCY_PAYMENT_METHOD.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"78");
			rfvAGENCY_BILL_TYPE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"79");
			rfvAGENCY_LIC_NUM.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"80");

			revAGENCY_DISPLAY_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revAGENCY_DBA.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revAGENCY_EMAIL.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("124");
			revAGENCY_FAX.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			revAGENCY_WEBSITE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("871");
			revAGENCY_LIC_NUM.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revAGENCY_CITY.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revAGENCY_ZIP.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			revAGENCY_PHONE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revAGENCY_EXT.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revAGENCY_SPEED_DIAL.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

			revAGENCY_DISPLAY_NAME.ValidationExpression=aRegExpClientName; 
			revAGENCY_DBA.ValidationExpression=aRegExpClientName; 
			revAGENCY_EMAIL.ValidationExpression=aRegExpEmail;
			revAGENCY_FAX.ValidationExpression=aRegExpFax;
			revAGENCY_WEBSITE.ValidationExpression=aRegExpSiteUrlWithoutHttp;
			
			revAGENCY_LIC_NUM.ValidationExpression=aRegExpInteger;
			revAGENCY_CITY.ValidationExpression=aRegExpClientName;
			revAGENCY_ZIP.ValidationExpression=aRegExpZip;
			revAGENCY_PHONE.ValidationExpression=aRegExpPhone;
			revAGENCY_EXT.ValidationExpression=aRegExpPhone;
			revAGENCY_SPEED_DIAL.ValidationExpression=aRegExpInteger;
			revBANK_ACCOUNT_NUMBER.ValidationExpression	=	aRegExpClientName;
			revBANK_ACCOUNT_NUMBER1.ValidationExpression	=	aRegExpClientName;
			//revFEDERAL_ID.ValidationExpression	=	aRegExpClientName;
			revFEDERAL_ID.ValidationExpression	=	aRegExpFederalID;
			revPRINCIPAL_CONTACT.ValidationExpression	=	aRegExpClientName;
			revOTHER_CONTACT.ValidationExpression	=	aRegExpClientName;
			revROUTING_NUMBER.ValidationExpression	=	aRegExpClientName;
			revROUTING_NUMBER1.ValidationExpression	=	aRegExpClientName;
			revBANK_BRANCH.ValidationExpression	=	aRegExpClientName;
			revBANK_NAME.ValidationExpression	=	aRegExpClientName;
			revNUM_AGENCY_CODE.ValidationExpression= aRegExpInteger;
			
			revFEDERAL_ID.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("434");
			revBANK_ACCOUNT_NUMBER.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("433");
			revBANK_ACCOUNT_NUMBER1.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("433");
			revBANK_BRANCH.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("63");
			revBANK_NAME.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("63");
			revPRINCIPAL_CONTACT.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("435");
			revOTHER_CONTACT.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("436");
			revROUTING_NUMBER.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("437");
			revROUTING_NUMBER1.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("437");
			
			revORIGINAL_CONTRACT_DATE.ValidationExpression		= aRegExpDate;
			revORIGINAL_CONTRACT_DATE.ErrorMessage              = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			csvORIGINAL_CONTRACT_DATE.ErrorMessage				= "<br>" +  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("510");
			
			revCURRENT_CONTRACT_DATE.ValidationExpression		= aRegExpDate;
			revCURRENT_CONTRACT_DATE.ErrorMessage              = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			csvCURRENT_CONTRACT_DATE.ErrorMessage				= "<br>" +  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("510");

			rfvM_AGENCY_ADD_1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
			rfvM_AGENCY_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			//revM_AGENCY_FAX.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			revM_AGENCY_CITY.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
			revM_AGENCY_ZIP.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			revNUM_AGENCY_CODE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("467");
			/*
			revM_AGENCY_PHONE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revM_AGENCY_EXT.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			revM_AGENCY_FAX.ValidationExpression=aRegExpFax;
			
			revM_AGENCY_PHONE.ValidationExpression=aRegExpPhone;
			revM_AGENCY_EXT.ValidationExpression=aRegExpExtn; */
			revM_AGENCY_CITY.ValidationExpression=aRegExpClientName;
			revM_AGENCY_ZIP.ValidationExpression=aRegExpZip;
			// Added by Mohit on 2/11/2005
			revTERMINATION_DATE.ValidationExpression=aRegExpDate;
			revTERMINATION_DATE.ValidationExpression		= aRegExpDate;
			revTERMINATION_DATE.ErrorMessage              = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revTERMINATION_DATE_RENEW.ValidationExpression		= aRegExpDate;
			revTERMINATION_DATE_RENEW.ErrorMessage              = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			//csvTERMINATION_DATE.ErrorMessage				= "<br>" +  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("525");

			//			revTERMINATION_REASON.ValidationExpression=aRegExpDate;
			//			revTERMINATION_REASON.ValidationExpression		= aRegExpDate;
			//			revTERMINATION_REASON.ErrorMessage              = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			//			
			// -------------End
		}

		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			cmbAGENCY_COUNTRY.SelectedIndex=int.Parse(aCountry);
			txtAGENCY_DISPLAY_NAME.Attributes.Add("onblur","javascript:generateCode();");  		           
			txtAGENCY_PHONE.Attributes.Add("onBlur","javascript:DisableExt('txtAGENCY_PHONE','txtAGENCY_EXT');");
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			btnCopyPhysicalAddress.Attributes.Add("onclick","javascript:return CopyPhysicalAddress();") ;
			hlkORIGINAL_CONTRACT_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE,document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE)"); //Javascript Implementation for Calender				
			hlkCURRENT_CONTRACT_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtCURRENT_CONTRACT_DATE,document.MNT_AGENCY_LIST.txtCURRENT_CONTRACT_DATE)"); //Javascript Implementation for Calender				
			// Added by mohit on 2/11/2005
			    
			hlkTERMINATION_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtTERMINATION_DATE,document.MNT_AGENCY_LIST.txtTERMINATION_DATE)");
			hlkTERMINATION_DATE_RENEW.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtTERMINATION_DATE_RENEW,document.MNT_AGENCY_LIST.txtTERMINATION_DATE_RENEW)");
			//   end
			//	hlkTERMINATION_REASON.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtTERMINATION_REASON,document.MNT_AGENCY_LIST.txtTERMINATION_REASON)");
			//btnActivateDeactivate.Attributes.Add("onclick","javascript:document.MNT_AGENCY_LIST.reset();");
			//  this.btnSave.Attributes.Add("onClick","javascript:CountUnderWriter();");
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="10_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;


			btnDelete.CmsButtonClass = CmsButtonType.Delete;
			btnDelete.PermissionString = gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			
			btnCopyPhysicalAddress.CmsButtonClass =     CmsButtonType.Write;
			btnCopyPhysicalAddress.PermissionString =	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddAgency" ,System.Reflection.Assembly.GetExecutingAssembly());
			ReadOnlyFields();

			if(!Page.IsPostBack)
			{
				int intAgencyID = ClsAgency.GetAgencyIDFromCode(GetSystemId());
				FillUnderWriter(intAgencyID);
				if(Request.QueryString["AGENCY_ID"]!=null)
				{					
					hidAGENCY_CODE.Value = ClsAgency.GetAgencyCodeFromID(int.Parse(Request.QueryString["AGENCY_ID"]));
					// Added by mohit
					// Commented by mohit on 28/10/2005
					//hidAGENCY_ID.Value=Request.QueryString["AGENCY_ID"].ToString();
					GenerateXML(Request.QueryString["AGENCY_ID"].ToString());
				}
				// ReadOnlyFields();
				SetCaptions();
				//Populating DropdownList
				SetDropdownList();                
			}
		}//end pageload
		#endregion


		private void FillUnderWriter(int AgencyID)
		{	
			try
			{
				//Populating the producer
				//				Cms.BusinessLayer.BlCommon.ClsUser objUser = new Cms.BusinessLayer.BlCommon.ClsUser();
				//				DataTable dt = objUser.FillUser("UWT",AgencyID).Tables[0];
				//				lstUNDERWRITER_ASSIGNED_AGENCY.DataSource = dt;
				//				lstUNDERWRITER_ASSIGNED_AGENCY.DataTextField = "USER_NAME";
				//				lstUNDERWRITER_ASSIGNED_AGENCY.DataValueField = "USER_ID";
				//				lstUNDERWRITER_ASSIGNED_AGENCY.DataBind();
				//				lstUNDERWRITER_ASSIGNED_AGENCY.Items.Insert(0,new System.Web.UI.WebControls.ListItem("",""));
				
			}
			catch(Exception exc)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
			}
			
		}
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string agencyID)
		{
			string strAgencyID=agencyID;
            
			objAgency=new ClsAgency(); 
  
			
			if(strAgencyID!="" && strAgencyID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objAgency.FetchData(int.Parse(strAgencyID));
					//hidOldData.Value=ds.GetXml(); 
					hidOldData.Value=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
					//hidFormSaved.Value="1"; 
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
					if(objAgency!= null)
						objAgency.Dispose();
				}  
                
			}
                
		}

		/// <summary>
		/// This function will populate dropdown list
		/// </summary>
		private void SetDropdownList()
		{
			ListItem agencyLI	= new ListItem("Agency Name","0");
			ListItem agencyLI1	= new ListItem("DBA Name","1");
			
			cmbAgencyNameDecPage.Items.Insert(0,agencyLI);
			cmbAgencyNameDecPage.Items.Insert(1,agencyLI1);
			cmbAgencyNameDecPage.Items[0].Value="0";
			cmbAgencyNameDecPage.Items[1].Value="1";
			cmbAgencyNameDecPage.SelectedIndex =0;


			//list item for payment method dropdown
			ListItem li         = new ListItem("Net","0");
			ListItem li1        = new ListItem("Gross","1");
 
			cmbAGENCY_PAYMENT_METHOD.Items.Insert(0,li);
			cmbAGENCY_PAYMENT_METHOD.Items.Insert(1,li1);  			
			cmbAGENCY_PAYMENT_METHOD.Items[0].Value="0";  
			cmbAGENCY_PAYMENT_METHOD.Items[1].Value="1"; 
			cmbAGENCY_PAYMENT_METHOD.SelectedIndex =0;

			cmbAGENCY_BILL_TYPE.Items.Insert(0,"Direct Bill");
			cmbAGENCY_BILL_TYPE.Items.Insert(1,"Agency Bill");
			cmbAGENCY_BILL_TYPE.SelectedIndex=0; 
			cmbAGENCY_BILL_TYPE.Items[0].Value="0";
			cmbAGENCY_BILL_TYPE.Items[1].Value="1";
			
			//using singleton object for country and state dropdown
			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbAGENCY_COUNTRY.DataSource		= dt;
			cmbAGENCY_COUNTRY.DataTextField	= "Country_Name";
			cmbAGENCY_COUNTRY.DataValueField	= "Country_Id";
			cmbAGENCY_COUNTRY.DataBind();
			cmbAGENCY_COUNTRY.Items[0].Selected=true;  
			cmbM_AGENCY_COUNTRY.DataSource		= dt;
			cmbM_AGENCY_COUNTRY.DataTextField	= "Country_Name";
			cmbM_AGENCY_COUNTRY.DataValueField	= "Country_Id";
			cmbM_AGENCY_COUNTRY.DataBind();
			cmbM_AGENCY_COUNTRY.Items[0].Selected=true; 

			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbAGENCY_STATE.DataSource		= dt;
			cmbAGENCY_STATE.DataTextField	= "STATE_NAME";
			cmbAGENCY_STATE.DataValueField	= "STATE_ID";
			cmbAGENCY_STATE.DataBind();
			cmbAGENCY_STATE.Items.Insert(0,"");
		
			

			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbM_AGENCY_STATE.DataSource		= dt;
			cmbM_AGENCY_STATE.DataTextField	= "STATE_NAME";
			cmbM_AGENCY_STATE.DataValueField	= "STATE_ID";
			cmbM_AGENCY_STATE.DataBind();
			cmbM_AGENCY_STATE.Items.Insert(0,"");


			
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsAgencyInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsAgencyInfo objAgencyInfo;
			objAgencyInfo = new ClsAgencyInfo();

			objAgencyInfo.AGENCY_CODE= txtAGENCY_CODE.Text;
			objAgencyInfo.AGENCY_COMBINED_CODE=	txtAGENCY_COMBINED_CODE.Text;
			objAgencyInfo.AGENCY_DISPLAY_NAME=	txtAGENCY_DISPLAY_NAME.Text;
			objAgencyInfo.AGENCY_DBA=	txtAGENCY_DBA.Text;
			objAgencyInfo.AGENCY_LIC_NUM=	int.Parse(txtAGENCY_LIC_NUM.Text);
			objAgencyInfo.AGENCY_ADD1=	txtAGENCY_ADD1.Text;
			objAgencyInfo.AGENCY_ADD2=	txtAGENCY_ADD2.Text;
			objAgencyInfo.AGENCY_CITY=	txtAGENCY_CITY.Text;
			objAgencyInfo.AGENCY_STATE=	cmbAGENCY_STATE.SelectedValue;
			objAgencyInfo.AGENCY_ZIP=	txtAGENCY_ZIP.Text;
			objAgencyInfo.AGENCY_COUNTRY=	cmbAGENCY_COUNTRY.SelectedValue;
			objAgencyInfo.AGENCY_PHONE=	txtAGENCY_PHONE.Text;
			objAgencyInfo.AGENCY_EXT=	txtAGENCY_EXT.Text;
			objAgencyInfo.AGENCY_FAX=	txtAGENCY_FAX.Text;
			objAgencyInfo.AGENCY_SPEED_DIAL=	txtAGENCY_SPEED_DIAL.Text;
			objAgencyInfo.AGENCY_EMAIL=	txtAGENCY_EMAIL.Text;
			objAgencyInfo.AGENCY_WEBSITE=	txtAGENCY_WEBSITE.Text;
			objAgencyInfo.M_AGENCY_ADD_1 = txtM_AGENCY_ADD_1.Text;
			objAgencyInfo.M_AGENCY_ADD_2= txtAGENCY_ADD2.Text;
			objAgencyInfo.M_AGENCY_CITY= txtAGENCY_CITY.Text	;
			objAgencyInfo.M_AGENCY_COUNTRY= cmbM_AGENCY_COUNTRY.SelectedValue;
			objAgencyInfo.M_AGENCY_STATE= cmbAGENCY_STATE.SelectedValue;
			objAgencyInfo.M_AGENCY_ZIP= txtM_AGENCY_ZIP.Text;
			//objAgencyInfo.NUM_AGENCY_CODE= int.Parse(txtNUM_AGENCY_CODE.Text.Trim()==""?"0":txtNUM_AGENCY_CODE.Text.Trim());
			if (txtNUM_AGENCY_CODE.Text.Trim() !="")
			{objAgencyInfo.NUM_AGENCY_CODE= Convert.ToInt32(txtNUM_AGENCY_CODE.Text);}
			
			//objAgencyInfo.M_AGENCY_PHONE= txtM_AGENCY_PHONE.Text;
			//objAgencyInfo.M_AGENCY_EXT= txtM_AGENCY_EXT.Text;
			//objAgencyInfo.M_AGENCY_FAX= txtM_AGENCY_FAX.Text;

						
			// Added by mohit on 2/11/2005
			if(txtTERMINATION_DATE.Text.Trim()!="")
				objAgencyInfo.TERMINATION_DATE= Convert.ToDateTime(txtTERMINATION_DATE.Text);
			//objAgencyInfo.TERMINATION_REASON= txtTERMINATION_REASON.Text;
			// End

			//nbisht
			//if(txtTERMINATION_REASON.Text.Trim()!="")
			//	objAgencyInfo.TERMINATION_REASON= Convert.ToDateTime(txtTERMINATION_REASON.Text);
			
			//Start --  New Field Added -- Gaurav
			if(txtPRINCIPAL_CONTACT.Text.Trim()!="")
				objAgencyInfo.PRINCIPAL_CONTACT = txtPRINCIPAL_CONTACT.Text;
			if(txtOTHER_CONTACT.Text.Trim()!="")
				objAgencyInfo.OTHER_CONTACT = txtOTHER_CONTACT.Text;
			if(txtORIGINAL_CONTRACT_DATE.Text.Trim()!="")
				objAgencyInfo.ORIGINAL_CONTRACT_DATE = Convert.ToDateTime(txtORIGINAL_CONTRACT_DATE.Text);

			if(txtCURRENT_CONTRACT_DATE.Text.Trim()!="")
				objAgencyInfo.CURRENT_CONTRACT_DATE = Convert.ToDateTime(txtCURRENT_CONTRACT_DATE.Text);


			if(txtROUTING_NUMBER.Text.Trim()!="")
				objAgencyInfo.ROUTING_NUMBER = txtROUTING_NUMBER.Text;
			if(txtBANK_ACCOUNT_NUMBER.Text.Trim()!="")
				objAgencyInfo.BANK_ACCOUNT_NUMBER = txtBANK_ACCOUNT_NUMBER.Text;
			if(txtROUTING_NUMBER1.Text.Trim()!="")
				objAgencyInfo.ROUTING_NUMBER1 = txtROUTING_NUMBER1.Text;
			if(txtBANK_ACCOUNT_NUMBER1.Text.Trim()!="")
				objAgencyInfo.BANK_ACCOUNT_NUMBER1 = txtBANK_ACCOUNT_NUMBER1.Text;
			if(txtBANK_NAME.Text.Trim()!="")
				objAgencyInfo.BANK_NAME = txtBANK_NAME.Text;
			if(txtBANK_BRANCH.Text.Trim()!="")
				objAgencyInfo.BANK_BRANCH = txtBANK_BRANCH.Text;
			if(txtNOTES.Text.Trim()!="")
				objAgencyInfo.NOTES= txtNOTES.Text;
			if(txtFEDERAL_ID.Text.Trim()!="")
				objAgencyInfo.FEDERAL_ID = txtFEDERAL_ID.Text;

			//			if(lstUNDERWRITER_ASSIGNED_AGENCY.SelectedItem!=null)
			//			objAgencyInfo.UNDERWRITER_ASSIGNED_AGENCY = hidUnderWriter.Value;

			//End --  New Field Added -- Gaurav
			
			objAgencyInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			objAgencyInfo.AGENCY_PAYMENT_METHOD=	cmbAGENCY_PAYMENT_METHOD.SelectedValue;
			objAgencyInfo.AGENCY_BILL_TYPE=	cmbAGENCY_BILL_TYPE.SelectedValue;
			objAgencyInfo.AgencyName=	cmbAgencyNameDecPage.SelectedValue;
			//BY PRAVESH
			//objAgencyInfo.TERMINATION_REASON =	txtTERMINATION_REASON.Text;
			if(txtTERMINATION_DATE_RENEW.Text.Trim()!="")
				objAgencyInfo.TERMINATION_DATE_RENEW = Convert.ToDateTime(txtTERMINATION_DATE_RENEW.Text);
			objAgencyInfo.TERMINATION_NOTICE =cmbTERMINATION_NOTICE.SelectedValue;  
			//END HERE
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidAGENCY_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objAgencyInfo;
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
			this.txtM_AGENCY_ADD_2.TextChanged += new System.EventHandler(this.txtM_AGENCY_ADD_2_TextChanged);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
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
				int intRetVal;	//For retreiving the return value of business class save function
				objAgency = new  ClsAgency();

				//Retreiving the form values into model class object
				ClsAgencyInfo objAgencyInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objAgencyInfo.CREATED_BY = int.Parse(GetUserId());
					objAgencyInfo.CREATED_DATETIME = DateTime.Now;
					objAgencyInfo.IS_ACTIVE="Y"; 

					//Calling the add method of business layer class
					intRetVal = objAgency.Add(objAgencyInfo);

					if(intRetVal>0)
					{
						hidAGENCY_ID.Value = objAgencyInfo.AGENCY_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"16");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -3)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"854");
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
					ClsAgencyInfo objOldAgencyInfo;
					objOldAgencyInfo = new ClsAgencyInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldAgencyInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
						objAgencyInfo.AGENCY_ID = int.Parse(strRowId);
					objAgencyInfo.MODIFIED_BY = int.Parse(GetUserId());
					objAgencyInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

					//Updating the record using business layer class object
					intRetVal	= objAgency.Update(objOldAgencyInfo,objAgencyInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"16");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -3)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"854");
						hidFormSaved.Value			=		"2";
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
                
			}
			finally
			{
				if(objAgency!= null)
					objAgency.Dispose();
			}

			if(hidAGENCY_ID.Value.ToUpper()!="NEW")
				GenerateXML(hidAGENCY_ID.Value);
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			Cms.BusinessLayer.BlCommon.ClsAgency objAgency      = new Cms.BusinessLayer.BlCommon.ClsAgency();
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
                

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objAgency.TransactionInfoParams = objStuTransactionInfo;
					objAgency.ActivateDeactivate(hidAGENCY_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objAgency.TransactionInfoParams = objStuTransactionInfo;
					objAgency.ActivateDeactivate(hidAGENCY_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"0";
				GenerateXML(hidAGENCY_ID.Value);
				hidReset.Value="0";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
                
			}
			finally
			{
				lblMessage.Visible = true;
				if(objAgency!= null)
					objAgency.Dispose();
			}
			//if(hidAGENCY_ID.Value.ToUpper()!="NEW")
			//GenerateXML(Request.QueryString["AGENCY_ID"].ToString());
			//	GenerateXML(hidAGENCY_ID.Value);

		}
		#endregion
		private void SetCaptions()
		{
			capAGENCY_CODE.Text							=		objResourceMgr.GetString("txtAGENCY_CODE");
			capAGENCY_COMBINED_CODE.Text				=		objResourceMgr.GetString("txtAGENCY_COMBINED_CODE");
			capAGENCY_DISPLAY_NAME.Text					=		objResourceMgr.GetString("txtAGENCY_DISPLAY_NAME");
			capAGENCY_DBA.Text							=		objResourceMgr.GetString("txtAGENCY_DBA");
			capAgencyDecName.Text						=		objResourceMgr.GetString("txtAgencyDecPage");
			capAGENCY_LIC_NUM.Text						=		objResourceMgr.GetString("txtAGENCY_LIC_NUM");
			capAGENCY_ADD1.Text							=		objResourceMgr.GetString("txtAGENCY_ADD1");
			capAGENCY_ADD2.Text							=		objResourceMgr.GetString("txtAGENCY_ADD2");
			capAGENCY_CITY.Text							=		objResourceMgr.GetString("txtAGENCY_CITY");
			capAGENCY_STATE.Text						=		objResourceMgr.GetString("cmbAGENCY_STATE");
			capAGENCY_ZIP.Text							=		objResourceMgr.GetString("txtAGENCY_ZIP");
			capAGENCY_COUNTRY.Text						=		objResourceMgr.GetString("cmbAGENCY_COUNTRY");
			capAGENCY_PHONE.Text						=		objResourceMgr.GetString("txtAGENCY_PHONE");
			capAGENCY_EXT.Text							=		objResourceMgr.GetString("txtAGENCY_EXT");
			capAGENCY_FAX.Text							=		objResourceMgr.GetString("txtAGENCY_FAX");
			capAGENCY_SPEED_DIAL.Text					=		objResourceMgr.GetString("txtAGENCY_SPEED_DIAL");
			capAGENCY_EMAIL.Text						=		objResourceMgr.GetString("txtAGENCY_EMAIL");
			capAGENCY_WEBSITE.Text						=		objResourceMgr.GetString("txtAGENCY_WEBSITE");
			capAGENCY_PAYMENT_METHOD.Text				=		objResourceMgr.GetString("cmbAGENCY_PAYMENT_METHOD");
			capAGENCY_BILL_TYPE.Text					=		objResourceMgr.GetString("cmbAGENCY_BILL_TYPE");
			capPRINCIPAL_CONTACT.Text					=		objResourceMgr.GetString("txtPRINCIPAL_CONTACT");
			capOTHER_CONTACT.Text						=		objResourceMgr.GetString("txtOTHER_CONTACT");
			capFEDERAL_ID.Text							=		objResourceMgr.GetString("txtFEDERAL_ID");
			capORIGINAL_CONTRACT_DATE.Text				=		objResourceMgr.GetString("txtORIGINAL_CONTRACT_DATE");
			capCURRENT_CONTRACT_DATE.Text				=		objResourceMgr.GetString("txtCURRENT_CONTRACT_DATE");
			
			//capUNDERWRITER_ASSIGNED_AGENCY.Text		=		objResourceMgr.GetString("lstUNDERWRITER_ASSIGNED_AGENCY");
			capBANK_ACCOUNT_NUMBER.Text					=		objResourceMgr.GetString("txtBANK_ACCOUNT_NUMBER");
			capROUTING_NUMBER.Text						=	    objResourceMgr.GetString("txtROUTING_NUMBER");
			capBANK_ACCOUNT_NUMBER1.Text				=		objResourceMgr.GetString("txtBANK_ACCOUNT_NUMBER1");
			capROUTING_NUMBER1.Text						=	    objResourceMgr.GetString("txtROUTING_NUMBER1");
			capBANK_NAME.Text							=		objResourceMgr.GetString("txtBANK_NAME");
			capBANK_BRANCH.Text							=		objResourceMgr.GetString("txtBANK_BRANCH");
			capNOTES.Text								=		objResourceMgr.GetString("txtNOTES");
			capNUM_AGENCY_CODE.Text						=		objResourceMgr.GetString("txtNUM_AGENCY_CODE");

			//new fiels
			capM_AGENCY_ADD_1.Text						=	   objResourceMgr.GetString("txtM_AGENCY_ADD_1");
			capM_AGENCY_ADD_2.Text						=	   objResourceMgr.GetString("txtM_AGENCY_ADD_2");
			capM_AGENCY_CITY.Text						=	   objResourceMgr.GetString("txtM_AGENCY_CITY");
			capM_AGENCY_COUNTRY.Text					=      objResourceMgr.GetString("cmbM_AGENCY_COUNTRY");
			capM_AGENCY_STATE.Text						=      objResourceMgr.GetString("cmbM_AGENCY_STATE");
			capM_AGENCY_ZIP.Text						=	   objResourceMgr.GetString("txtM_AGENCY_ZIP");
			//capM_AGENCY_PHONE.Text					=      objResourceMgr.GetString("txtM_AGENCY_PHONE");
			//capM_AGENCY_EXT.Text						=	   objResourceMgr.GetString("txtM_AGENCY_EXT");
			//capM_AGENCY_FAX.Text						=	   objResourceMgr.GetString("txtM_AGENCY_FAX");
			lblCopy_Address.Text						=	   objResourceMgr.GetString("lblCopy_Address");


			// Added by Mohit on 2/11/2005
			capTERMINATION_DATE.Text=objResourceMgr.GetString("txtTERMINATION_DATE");
			capTERMINATION_DATE_RENEW.Text=objResourceMgr.GetString("txtTERMINATION_DATE_RENEW");
			capTERMINATION_NOTICE.Text=objResourceMgr.GetString("txtTERMINATION_NOTICE");
			//capTERMINATION_REASON.Text=objResourceMgr.GetString("txtTERMINATION_REASON");

			// End
		}


		private void ReadOnlyFields()
		{
			strSystemID			 = GetSystemId();
			string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
			strCarrierSystemID = CarrierSystemID;
			if ( strSystemID.Trim().ToUpper() == strCarrierSystemID.Trim().ToUpper())
			{
				txtAGENCY_CODE.ReadOnly = false;
				txtAGENCY_LIC_NUM.ReadOnly = false;
				txtORIGINAL_CONTRACT_DATE.ReadOnly = false;
				txtCURRENT_CONTRACT_DATE.ReadOnly = false;
				//imgORIGINAL_CONTRACT_DATE.Enabled = false;
				//hlkORIGINAL_CONTRACT_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE,document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE)"); //Javascript Implementation for Calender

				// Added by Mohit on 2/11/2005
				// txtTERMINATION_DATE.ReadOnly=false;
				// txtTERMINATION_REASON.ReadOnly=false;
				rowTermination.Visible=true;

				// End 
			}
			else
			{
				txtAGENCY_CODE.ReadOnly = true;
				txtAGENCY_LIC_NUM.ReadOnly = true;
				txtORIGINAL_CONTRACT_DATE.ReadOnly = true;
				txtCURRENT_CONTRACT_DATE.ReadOnly = true;
				//imgORIGINAL_CONTRACT_DATE.Enabled = true;
				//hlkORIGINAL_CONTRACT_DATE.Attributes.Add("OnClick","javascript:return false;"); //Javascript Implementation for Calender
				
				// Added by Mohit on 2/11/2005
				//txtTERMINATION_DATE.ReadOnly=true;
				//txtTERMINATION_REASON.ReadOnly=true;	
				rowTermination.Visible=false;	
				// End

			}
					
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			int intAgencyID = int.Parse(hidAGENCY_ID.Value);
			
			objAgency = new Cms.BusinessLayer.BlCommon.ClsAgency();
			intRetVal = objAgency.Delete(intAgencyID);
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
				
				string strScript = "<script>parent.RefreshWebgrid(1);</script>";
				ClientScript.RegisterClientScriptBlock(this.GetType(),"refresh",strScript);
				lblDelete.Visible = true;
			}
			else if(intRetVal == -2)
			{
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"449"); // chk it
				hidFormSaved.Value		=	"2";			
			}
			else if(intRetVal == -1)
			{
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblMessage.Visible = true;
		}

		private void txtM_AGENCY_ADD_2_TextChanged(object sender, System.EventArgs e)
		{
		
		}
   
	}
}
