/******************************************************************************************
<Author					: - Amar Singh  
<Start Date				: -	4/21/2006 11:43:10 AM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// objAdjuster
	/// </summary>
	public class AddAdjusterDetails : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.CustomValidator csvSUB_ADJUSTER_NOTES;
		protected System.Web.UI.WebControls.DropDownList cmbADJUSTER_TYPE;
		protected System.Web.UI.WebControls.TextBox txtADJUSTER_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbADJUSTER_CODE;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_LEGAL_NAME;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_ADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbSUB_ADJUSTER_STATE;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_ZIP;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_PHONE;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_FAX;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_EMAIL;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_WEBSITE;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_NOTES;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID_LIST;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADJUSTER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADJUSTER_CODE;
		
		protected Cms.CmsWeb.Controls.CmsButton btnReset;		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADJUSTER_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADJUSTER_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADJUSTER_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_ADJUSTER_LEGAL_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_ADJUSTER_ADDRESS1;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_ADJUSTER_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_ADJUSTER_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_ADJUSTER_ZIP;

		protected System.Web.UI.WebControls.RegularExpressionValidator revADJUSTER_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_LEGAL_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_WEBSITE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_ADJUSTER_NOTES;

        //Code added  by Agniswar for Singapore Implementation
        protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_GST;
        protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_GST_REG_NO;
        protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_MOBILE_NO;
        protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_CLASSIFICATION;

		protected System.Web.UI.WebControls.Label capADJUSTER_TYPE;
		protected System.Web.UI.WebControls.Label capADJUSTER_NAME;
		protected System.Web.UI.WebControls.Label capADJUSTER_CODE;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_LEGAL_NAME;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_ADDRESS1;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_ADDRESS2;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_CITY;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_STATE;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_ZIP;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_PHONE;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_FAX;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_EMAIL;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_WEBSITE;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_NOTES;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbSUB_ADJUSTER_COUNTRY;
		protected System.Web.UI.WebControls.ListBox cmbLOB_ID;
		protected System.Web.UI.WebControls.Label capSUB_ADJUSTER_CONTACT_NAME;
		protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_CONTACT_NAME;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.Label capSA_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtSA_ADDRESS1;
		protected System.Web.UI.WebControls.Label capSA_ADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtSA_ADDRESS2;
		protected System.Web.UI.WebControls.Label capSA_CITY;
		protected System.Web.UI.WebControls.TextBox txtSA_CITY;
		protected System.Web.UI.WebControls.Label capSA_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbSA_COUNTRY;
		protected System.Web.UI.WebControls.Label capSA_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbSA_STATE;
		protected System.Web.UI.WebControls.Label capSA_ZIPCODE;
		protected System.Web.UI.WebControls.TextBox txtSA_ZIPCODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSA_ZIPCODE;
		protected System.Web.UI.WebControls.Label capSA_PHONE;
		protected System.Web.UI.WebControls.TextBox txtSA_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSA_PHONE;
		protected System.Web.UI.WebControls.Label capSA_FAX;
		protected System.Web.UI.WebControls.TextBox txtSA_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSA_FAX;	
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.Image imgSAZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkSAZipLookup;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.Label capsubadjuster;
        protected System.Web.UI.WebControls.Label capDISPLAY_ON_CLAIM;
        protected System.Web.UI.WebControls.DropDownList cmbDISPLAY_ON_CLAIM;
        //protected System.Web.UI.WebControls.Button cmdGO;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAuthorityMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipeCodeVerificationMsg;
         protected System.Web.UI.HtmlControls.HtmlInputHidden hidSA_STATE;
         protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_ADJUSTER_STATE;


        //Added by Agniswar for Singapore implementation
         protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_GST;
         protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_GST_REG_NO;
         protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_MOBILE_NO;
         protected System.Web.UI.WebControls.TextBox txtSUB_ADJUSTER_CLASSIFICATION;


   

		private char LOB_DELIMITER = '^';
      
        
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;//, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdGO;
		
		//Defining the business layer class object
		ClsAdjusterDetails  objAdjuster ;
		
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
			rfvADJUSTER_TYPE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvADJUSTER_NAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvADJUSTER_CODE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvSUB_ADJUSTER_LEGAL_NAME.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvSUB_ADJUSTER_ADDRESS1.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");			
			rfvSUB_ADJUSTER_CITY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvSUB_ADJUSTER_STATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
            rfvSUB_ADJUSTER_ZIP.ErrorMessage        =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");

           
			revADJUSTER_NAME.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");			
			//revSUB_ADJUSTER.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			revSUB_ADJUSTER_LEGAL_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			revSUB_ADJUSTER_CITY.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
            revSUB_ADJUSTER_ZIP.ErrorMessage        = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            revSUB_ADJUSTER_PHONE.ErrorMessage      = ClsMessages.FetchGeneralMessage("1083");
            revSUB_ADJUSTER_FAX.ErrorMessage        = ClsMessages.FetchGeneralMessage("1085");
			revSUB_ADJUSTER_EMAIL.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			revSUB_ADJUSTER_WEBSITE.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("737");
			csvSUB_ADJUSTER_NOTES.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			//this.csvOTHER_DESCRIPTION.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");

			revADJUSTER_NAME.ValidationExpression			= aRegExpAlphaNum;			
			//revSUB_ADJUSTER.ValidationExpression			= aRegExpAlphaNum;
			revSUB_ADJUSTER_LEGAL_NAME.ValidationExpression = aRegExpAlpha;
			revSUB_ADJUSTER_CITY.ValidationExpression		= aRegExpAlphaNum;
			revSUB_ADJUSTER_ZIP.ValidationExpression		= aRegExpZip;
            //revSUB_ADJUSTER_PHONE.ValidationExpression		= aRegExpPhone;
            revSUB_ADJUSTER_FAX.ValidationExpression        = aRegExpPhoneBrazil;
			revSUB_ADJUSTER_EMAIL.ValidationExpression		= aRegExpEmail;
            revSUB_ADJUSTER_PHONE.ValidationExpression      = aRegExpPhoneBrazil;
			revSUB_ADJUSTER_WEBSITE.ValidationExpression	= RegExpSiteUrlWeb;			
			revSUB_ADJUSTER_NOTES.ValidationExpression		= aRegExpTextArea1000;
			
			revSA_ZIPCODE.ValidationExpression				= aRegExpZip;
            revSA_PHONE.ValidationExpression                = aRegExpPhoneBrazil;
            revSA_FAX.ValidationExpression                  = aRegExpPhoneBrazil;

            revSA_ZIPCODE.ErrorMessage                      = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            revSA_PHONE.ErrorMessage                        = ClsMessages.FetchGeneralMessage("1083");
            revSA_FAX.ErrorMessage                          = ClsMessages.FetchGeneralMessage("1085");
            hidAuthorityMessage.Value                       = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
            hidZipeCodeVerificationMsg.Value                = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
            Ajax.Utility.RegisterTypeForAjax(typeof(AddAdjusterDetails)); 		
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
            
			//this.cmbSUB_ADJUSTER_COUNTRY.SelectedIndex = int.Parse(aCountry);
			// Added by Swarup on 30-mar-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtSUB_ADJUSTER_ADDRESS1,txtSUB_ADJUSTER_ADDRESS2
				, txtSUB_ADJUSTER_CITY, cmbSUB_ADJUSTER_STATE, txtSUB_ADJUSTER_ZIP);
			imgSAZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkSAZipLookup, txtSUB_ADJUSTER_ADDRESS1,txtSUB_ADJUSTER_ADDRESS2
				, txtSUB_ADJUSTER_CITY, cmbSUB_ADJUSTER_STATE, txtSUB_ADJUSTER_ZIP);
           

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="298_0";
			lblMessage.Visible = false;
            //SetErrorMessages();
            //capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            cmdGO.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2101");
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write; //Permission made Write instead of Execute by Sibin on 22 Oct 08
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write; //Permission made Write instead of Execute by Sibin on 22 Oct 08
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write ; //Permission made Write instead of Execute by Sibin on 22 Oct 08
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddAdjusterDetails" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
                btnActivateDeactivate.Visible = false;
				cmbADJUSTER_TYPE.Attributes.Add("onChange","javascript: return CheckAdjusterType();");
				//cmbADJUSTER_CODE.Attributes.Add("onChange","javascript: return GetUserData(this);");

                LoadDropDowns();

                SetErrorMessages();
			//	GetOldDataXML();
				SetCaptions();

                // Added by Agniswar for Screen Customization on 14 sep 2011

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, "AddAdjusterDetails.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/AddAdjusterDetails.xml");


                if (Request.QueryString["ADJUSTER_ID"] != null && Request.QueryString["ADJUSTER_ID"].ToString().Length > 0)
                {
                    hidADJUSTER_ID.Value = Request.QueryString["ADJUSTER_ID"].ToString();
                    GetOldDataXML();
                }

				#region "Loading singleton"
				#endregion//Loading singleton

                


               
			}
			//Added for Itrack Issue 6453 on 24 Sept 09
			//btnSave.Attributes.Add("onClick","javascript: return GetUserData(document.getElementById('cmbADJUSTER_CODE'));");
		}//end pageload
		#endregion

		private void LoadDropDowns()
		{
			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbSUB_ADJUSTER_COUNTRY.DataSource		= dt;
			cmbSUB_ADJUSTER_COUNTRY.DataTextField	= "Country_Name";
			cmbSUB_ADJUSTER_COUNTRY.DataValueField	= "Country_Id";
			cmbSUB_ADJUSTER_COUNTRY.DataBind();

            if (GetLanguageID() == "3")
                cmbSUB_ADJUSTER_COUNTRY.SelectedValue = "7";

            if (cmbSUB_ADJUSTER_COUNTRY.SelectedValue != "")
            {
                Cms.BusinessLayer.BlCommon.ClsStates objStates = new Cms.BusinessLayer.BlCommon.ClsStates();
                DataSet ds = objStates.GetStatesCountry(int.Parse(cmbSUB_ADJUSTER_COUNTRY.SelectedValue));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cmbSUB_ADJUSTER_STATE.DataSource = ds;
                    cmbSUB_ADJUSTER_STATE.DataTextField = STATE_NAME;
                    cmbSUB_ADJUSTER_STATE.DataValueField = STATE_ID;
                    cmbSUB_ADJUSTER_STATE.DataBind();

                    cmbSUB_ADJUSTER_STATE.SelectedIndex = 0;
                }
            }
            //DataTable dtState = Cms.CmsWeb.ClsFetcher.State;
            //cmbSUB_ADJUSTER_STATE.DataSource = dtState;
            //cmbSUB_ADJUSTER_STATE.DataTextField = "STATE_NAME";
            //cmbSUB_ADJUSTER_STATE.DataValueField = "STATE_ID";
            //cmbSUB_ADJUSTER_STATE.DataBind();
            //cmbSUB_ADJUSTER_STATE.Items.Insert(0, new ListItem("", ""));
            //cmbSUB_ADJUSTER_STATE.SelectedIndex = 0;

			cmbADJUSTER_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("ADJTYP");
			cmbADJUSTER_TYPE.DataTextField="LookupDesc";
			cmbADJUSTER_TYPE.DataValueField="LookupID";
			cmbADJUSTER_TYPE.DataBind();
			cmbADJUSTER_TYPE.Items.Insert(0,"");

            cmbDISPLAY_ON_CLAIM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("YESNO");
            cmbDISPLAY_ON_CLAIM.DataTextField = "LookupDesc";
            cmbDISPLAY_ON_CLAIM.DataValueField = "LookupID";
            cmbDISPLAY_ON_CLAIM.DataBind();
            

			cmbADJUSTER_CODE.DataSource=ClsAdjusterDetails.GetAdjusterCode(GetSystemId());
			cmbADJUSTER_CODE.DataTextField="User_Name";
			cmbADJUSTER_CODE.DataValueField="User_ID";
			cmbADJUSTER_CODE.DataBind();
			cmbADJUSTER_CODE.Items.Insert(0,"");

			dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbSA_COUNTRY.DataSource		= dt;
			cmbSA_COUNTRY.DataTextField	= "Country_Name";
			cmbSA_COUNTRY.DataValueField	= "Country_Id";
			cmbSA_COUNTRY.DataBind();

            if (cmbSA_COUNTRY.SelectedValue != "")
            {
                Cms.BusinessLayer.BlCommon.ClsStates objStates = new Cms.BusinessLayer.BlCommon.ClsStates();
                DataSet ds = objStates.GetStatesCountry(int.Parse(cmbSA_COUNTRY.SelectedValue));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cmbSA_STATE.DataSource = ds;
                    cmbSA_STATE.DataTextField = STATE_NAME;
                    cmbSA_STATE.DataValueField = STATE_ID;
                    cmbSA_STATE.DataBind();
                }
            }
            //dtState = Cms.CmsWeb.ClsFetcher.State ;			
            //cmbSA_STATE.DataSource		= dtState;
            //cmbSA_STATE.DataTextField		= "STATE_NAME";
            //cmbSA_STATE.DataValueField	= "STATE_ID";
            //cmbSA_STATE.DataBind();
			cmbSA_STATE.Items.Insert(0,new ListItem("",""));
			cmbSA_STATE.SelectedIndex=0;

			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbLOB_ID.DataSource			= dtLOBs;
			cmbLOB_ID.DataTextField		= "LOB_DESC";
			cmbLOB_ID.DataValueField		= "LOB_ID";
			cmbLOB_ID.DataBind();


			
		}


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFillState(string CountryID)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.FillState(CountryID);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));

                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }



		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsAdjusterDetailsInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsAdjusterDetailsInfo objAdjusterInfo;
			objAdjusterInfo = new ClsAdjusterDetailsInfo();

			objAdjusterInfo.ADJUSTER_TYPE=	int.Parse(cmbADJUSTER_TYPE.SelectedValue.ToString());
            objAdjusterInfo.DISPLAY_ON_CLAIM = int.Parse(cmbDISPLAY_ON_CLAIM.SelectedValue.ToString());
			
			objAdjusterInfo.ADJUSTER_NAME=	txtADJUSTER_NAME.Text;
			hidLOB_ID_LIST.Value = "";
			if(cmbADJUSTER_TYPE.SelectedItem!=null && cmbADJUSTER_TYPE.SelectedItem.Value!="")
			{
				//Take the value from the hidden variable hidADJUSTER_CODE as the combo value contains concatenated string
				if(cmbADJUSTER_TYPE.SelectedItem.Value!=((int)enumClaimAdjusterTypes.THIRD_PARTY_ADJUSTER).ToString() && cmbADJUSTER_CODE.SelectedItem!=null && cmbADJUSTER_CODE.SelectedItem.Value!="")
				{
                    string[] delimeter = { "^" };
                    string AdjusterDetails = cmbADJUSTER_CODE.SelectedValue;
                    string[] Arr = AdjusterDetails.Split(delimeter, StringSplitOptions.None);
                    objAdjusterInfo.ADJUSTER_CODE = Arr[1];
				
					//Added by Asfa 29-Aug-2007
                    objAdjusterInfo.USER_ID =int.Parse( Arr[0]);
                    objAdjusterInfo.ADJUSTER_NAME = Arr[2];
				}
				//Saving rest information for thirdparty adjuster only
				if(cmbADJUSTER_TYPE.SelectedItem.Value == ((int)enumClaimAdjusterTypes.THIRD_PARTY_ADJUSTER).ToString())
				{
					objAdjusterInfo.SUB_ADJUSTER_LEGAL_NAME=	txtSUB_ADJUSTER_LEGAL_NAME.Text;
					objAdjusterInfo.SUB_ADJUSTER_ADDRESS1=	txtSUB_ADJUSTER_ADDRESS1.Text;
					objAdjusterInfo.SUB_ADJUSTER_ADDRESS2=	txtSUB_ADJUSTER_ADDRESS2.Text;
					objAdjusterInfo.SUB_ADJUSTER_CITY=	txtSUB_ADJUSTER_CITY.Text;
                    if (hidSUB_ADJUSTER_STATE.Value != null && hidSUB_ADJUSTER_STATE.Value != "")
                        objAdjusterInfo.SUB_ADJUSTER_STATE = int.Parse(hidSUB_ADJUSTER_STATE.Value);
					objAdjusterInfo.SUB_ADJUSTER_ZIP=	txtSUB_ADJUSTER_ZIP.Text.Trim();
					objAdjusterInfo.SUB_ADJUSTER_PHONE=	txtSUB_ADJUSTER_PHONE.Text;
					objAdjusterInfo.SUB_ADJUSTER_FAX=	txtSUB_ADJUSTER_FAX.Text;
					objAdjusterInfo.SUB_ADJUSTER_EMAIL=	txtSUB_ADJUSTER_EMAIL.Text;
					objAdjusterInfo.SUB_ADJUSTER_WEBSITE=	txtSUB_ADJUSTER_WEBSITE.Text;
					objAdjusterInfo.SUB_ADJUSTER_NOTES=	txtSUB_ADJUSTER_NOTES.Text;

					if(cmbSUB_ADJUSTER_COUNTRY.SelectedItem!=null && cmbSUB_ADJUSTER_COUNTRY.SelectedItem.Value!="")
						objAdjusterInfo.SUB_ADJUSTER_COUNTRY=	cmbSUB_ADJUSTER_COUNTRY.SelectedValue;
					//Fetch list of LOBs selected by the user
					hidLOB_ID_LIST.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetDelimitedValuesFromListbox(cmbLOB_ID,LOB_DELIMITER);
					objAdjusterInfo.LOB_ID = hidLOB_ID_LIST.Value;
				}
			}

            //Code added by Agniswar for Singapore Implementation

            if (GetSystemId() == "S001" || GetSystemId() == "SUAT")
            {
                objAdjusterInfo.ADJUSTER_CODE = txtADJUSTER_NAME.Text;
                objAdjusterInfo.SUB_ADJUSTER_GST = txtSUB_ADJUSTER_GST.Text;
                objAdjusterInfo.SUB_ADJUSTER_GST_REG_NO = txtSUB_ADJUSTER_GST_REG_NO.Text;
                objAdjusterInfo.SUB_ADJUSTER_MOBILE = txtSUB_ADJUSTER_MOBILE_NO.Text;
                objAdjusterInfo.SUB_ADJUSTER_CLASSIFICATION = txtSUB_ADJUSTER_CLASSIFICATION.Text;

            }


            // Till here
			
			objAdjusterInfo.SUB_ADJUSTER=	txtSUB_ADJUSTER.Text;
			objAdjusterInfo.SUB_ADJUSTER_CONTACT_NAME = txtSUB_ADJUSTER_CONTACT_NAME.Text.Trim();
			objAdjusterInfo.SA_ADDRESS1 =	txtSA_ADDRESS1.Text; 
			objAdjusterInfo.SA_ADDRESS2 =	txtSA_ADDRESS2.Text;
			objAdjusterInfo.SA_CITY =	txtSA_CITY.Text;

			if(cmbSA_COUNTRY.SelectedItem!=null && cmbSA_COUNTRY.SelectedItem.Value!="")
				objAdjusterInfo.SA_COUNTRY =	cmbSA_COUNTRY.SelectedValue;

            if (hidSA_STATE.Value != null && hidSA_STATE.Value != "")
                objAdjusterInfo.SA_STATE = int.Parse(hidSA_STATE.Value);
            
					
			objAdjusterInfo.SA_ZIPCODE  =	txtSA_ZIPCODE.Text; 
			objAdjusterInfo.SA_PHONE =	txtSA_PHONE.Text;
			objAdjusterInfo.SA_FAX =	txtSA_FAX.Text;

			if(hidADJUSTER_ID.Value.ToUpper()!="NEW")
				objAdjusterInfo.ADJUSTER_ID = int.Parse(hidADJUSTER_ID.Value);
			
			strRowId = hidADJUSTER_ID.Value;
			return objAdjusterInfo;
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
				objAdjuster = new  ClsAdjusterDetails();

				//Retreiving the form values into model class object
				ClsAdjusterDetailsInfo objAdjusterInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objAdjusterInfo.CREATED_BY = int.Parse(GetUserId());
					objAdjusterInfo.CREATED_DATETIME = DateTime.Now;
					objAdjusterInfo.IS_ACTIVE = "Y";
					//Calling the add method of business layer class
					intRetVal = objAdjuster.Add(objAdjusterInfo);

					if(intRetVal>0)
					{
						hidADJUSTER_ID.Value	= objAdjusterInfo.ADJUSTER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		=	"1";
						//hidOldData.Value		= ClsAdjusterDetails.GetAdjusterDetails(int.Parse(hidADJUSTER_ID.Value));
						hidIS_ACTIVE.Value		= "Y";

                        btnActivateDeactivate.Visible = true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText("Y");

                        GetOldDataXML();
                      
					}
					else if(intRetVal == -1)
					{
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "23");
						hidFormSaved.Value		=		"2";
					}
					else
					{
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsAdjusterDetailsInfo objOldAdjusterInfo;
					objOldAdjusterInfo = new ClsAdjusterDetailsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldAdjusterInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objAdjusterInfo.ADJUSTER_ID = int.Parse(strRowId);
					objAdjusterInfo.MODIFIED_BY = int.Parse(GetUserId());
					objAdjusterInfo.LAST_UPDATED_DATETIME = DateTime.Now;	
				
					string customer_Info="";					
					customer_Info +=	";Adjuster Type = " + cmbADJUSTER_TYPE.Items[cmbADJUSTER_TYPE.SelectedIndex].Text;
					customer_Info +=	";Adjuster Name = " + txtADJUSTER_NAME.Text;
					//Updating the record using business layer class object
					intRetVal	= objAdjuster.Update(objOldAdjusterInfo,objAdjusterInfo,customer_Info);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						//hidOldData.Value = ClsAdjusterDetails.GetAdjusterDetails(int.Parse(strRowId));
                        GetOldDataXML();

                      
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
                        lblMessage.Text         = ClsMessages.GetMessage(base.ScreenId, "23");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
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
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objAdjuster!= null)
					objAdjuster.Dispose();
			}
		}
		
	
		#endregion
		private void SetCaptions()
		{
			capADJUSTER_TYPE.Text						=		objResourceMgr.GetString("cmbADJUSTER_TYPE");
			capADJUSTER_NAME.Text						=		objResourceMgr.GetString("txtADJUSTER_NAME");
			capADJUSTER_CODE.Text						=		objResourceMgr.GetString("cmbADJUSTER_CODE");
			capSUB_ADJUSTER.Text						=		objResourceMgr.GetString("txtSUB_ADJUSTER");
			capSUB_ADJUSTER_LEGAL_NAME.Text				=		objResourceMgr.GetString("txtSUB_ADJUSTER_LEGAL_NAME");
			capSUB_ADJUSTER_ADDRESS1.Text				=		objResourceMgr.GetString("txtSUB_ADJUSTER_ADDRESS1");
			capSUB_ADJUSTER_ADDRESS2.Text				=		objResourceMgr.GetString("txtSUB_ADJUSTER_ADDRESS2");
			capSUB_ADJUSTER_CITY.Text					=		objResourceMgr.GetString("txtSUB_ADJUSTER_CITY");
			capSUB_ADJUSTER_STATE.Text					=		objResourceMgr.GetString("cmbSUB_ADJUSTER_STATE");
			capSUB_ADJUSTER_ZIP.Text					=		objResourceMgr.GetString("txtSUB_ADJUSTER_ZIP");
			capSUB_ADJUSTER_PHONE.Text					=		objResourceMgr.GetString("txtSUB_ADJUSTER_PHONE");
			capSUB_ADJUSTER_FAX.Text					=		objResourceMgr.GetString("txtSUB_ADJUSTER_FAX");
			capSUB_ADJUSTER_EMAIL.Text					=		objResourceMgr.GetString("txtSUB_ADJUSTER_EMAIL");
			capSUB_ADJUSTER_WEBSITE.Text				=		objResourceMgr.GetString("txtSUB_ADJUSTER_WEBSITE");
			capSUB_ADJUSTER_NOTES.Text					=		objResourceMgr.GetString("txtSUB_ADJUSTER_NOTES");
			capSUB_ADJUSTER_COUNTRY.Text				=		objResourceMgr.GetString("cmbSUB_ADJUSTER_COUNTRY");
			capSUB_ADJUSTER_CONTACT_NAME.Text			=		objResourceMgr.GetString("txtSUB_ADJUSTER_CONTACT_NAME");
			capSA_ADDRESS1.Text							=		objResourceMgr.GetString("txtSA_ADDRESS1");
			capSA_ADDRESS2.Text							=		objResourceMgr.GetString("txtSA_ADDRESS2");
			capSA_CITY.Text								=		objResourceMgr.GetString("txtSA_CITY");
			capSA_COUNTRY.Text							=		objResourceMgr.GetString("cmbSA_COUNTRY");
			capSA_STATE.Text							=		objResourceMgr.GetString("cmbSA_STATE");
			capSA_ZIPCODE.Text							=		objResourceMgr.GetString("txtSA_ZIPCODE");
			capSA_PHONE.Text							=		objResourceMgr.GetString("txtSA_PHONE");
			capSA_FAX.Text								=		objResourceMgr.GetString("txtSA_FAX");
			capLOB_ID.Text								=		objResourceMgr.GetString("cmbLOB_ID");
            capDISPLAY_ON_CLAIM.Text                    =       objResourceMgr.GetString("cmbDISPLAY_ON_CLAIM");
            // Code added by Agniswar for Singapore Implementation
            capSUB_ADJUSTER_GST.Text = objResourceMgr.GetString("txtSUB_ADJUSTER_GST");
            capSUB_ADJUSTER_GST_REG_NO.Text = objResourceMgr.GetString("txtSUB_ADJUSTER_GST_REG_NO");
            capSUB_ADJUSTER_MOBILE_NO.Text = objResourceMgr.GetString("txtSUB_ADJUSTER_MOBILE_NO");
            capSUB_ADJUSTER_CLASSIFICATION.Text = objResourceMgr.GetString("txtSUB_ADJUSTER_CLASSIFICATION");
           
            capsubadjuster.Text = objResourceMgr.GetString("capsubadjuster");
           
		}
		private void GetOldDataXML()
		{
            if (hidADJUSTER_ID.Value != "" && hidADJUSTER_ID.Value != "0")
            {
                string AdjusterDetails = ClsAdjusterDetails.GetAdjusterDetails(int.Parse(hidADJUSTER_ID.Value));
                hidOldData.Value = AdjusterDetails;
                hidLOB_ID_LIST.Value = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("LOB_ID", AdjusterDetails);
                if (hidLOB_ID_LIST.Value != "" && hidLOB_ID_LIST.Value != "0")
                {
                    Cms.BusinessLayer.BlCommon.ClsCommon.SelectValuesAtListbox(cmbLOB_ID, hidLOB_ID_LIST.Value, LOB_DELIMITER);
                }
                btnActivateDeactivate.Visible = true;
                string IsActive = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE", AdjusterDetails);
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(IsActive.Trim());

                string AdjusterCountry      = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_COUNTRY", AdjusterDetails);
                string AdjusterState        = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_STATE", AdjusterDetails);
                string SubAdjusterCountry   = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_COUNTRY", AdjusterDetails);
                string SubAdjusterState     = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_STATE", AdjusterDetails);
                          
                string DISPLAY_ON_CLAIM     = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("DISPLAY_ON_CLAIM", AdjusterDetails);
                string USER_ID              = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_ID", AdjusterDetails);
                string ADJUSTER_TYPE        = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("ADJUSTER_TYPE", AdjusterDetails);
                string SUB_ADJUSTER_STATE   = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_STATE", AdjusterDetails);
                string SUB_ADJUSTER_COUNTRY = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_COUNTRY", AdjusterDetails);

                if(ADJUSTER_TYPE!="" && ADJUSTER_TYPE!="0")
                    cmbADJUSTER_TYPE.SelectedValue=ADJUSTER_TYPE;
                
                if (AdjusterCountry != "" && AdjusterCountry != "0")
                {
                    if (cmbSUB_ADJUSTER_COUNTRY.Items.FindByValue(AdjusterCountry) != null)
                        cmbSUB_ADJUSTER_COUNTRY.SelectedValue = AdjusterCountry;
                    else
                        cmbSUB_ADJUSTER_COUNTRY.SelectedValue = "5";//BRASIL

                      FillState(int.Parse(AdjusterCountry), ref cmbSUB_ADJUSTER_STATE);
                    
                }

                if (SubAdjusterCountry != "" && SubAdjusterCountry != "0")
                {
                    FillState(int.Parse(SubAdjusterCountry), ref cmbSA_STATE);
                    cmbSA_COUNTRY.SelectedValue = SubAdjusterCountry;
                }

                if (SubAdjusterState != "0" && SubAdjusterState != "")
                {
                    if (cmbSA_STATE.Items.FindByValue(SubAdjusterState) != null)
                        cmbSA_STATE.SelectedValue = SubAdjusterState;

                    hidSA_STATE.Value = cmbSA_STATE.SelectedValue;
                }

                if (AdjusterState != "0" && AdjusterState != "")
                {
                    if (cmbSUB_ADJUSTER_STATE.Items.FindByValue(AdjusterState) != null)
                        cmbSUB_ADJUSTER_STATE.SelectedValue = AdjusterState;
                    
                    hidSUB_ADJUSTER_STATE.Value = cmbSUB_ADJUSTER_STATE.SelectedValue;
                }

                

              
              
                 if (DISPLAY_ON_CLAIM != ""&&  DISPLAY_ON_CLAIM != "0")
                    cmbDISPLAY_ON_CLAIM.SelectedValue = DISPLAY_ON_CLAIM;

                 if (USER_ID != "" && USER_ID != "0")
                 {
                     string[] delimeter = { "^" };
                     string Adjuster = cmbADJUSTER_CODE.SelectedValue;
                     string[] Arr = {};                    
                     foreach (ListItem lst in cmbADJUSTER_CODE.Items)
                     {
                         if (lst.Value != "")
                         {
                             Arr = lst.Value.Split(delimeter, StringSplitOptions.None);
                             if (Arr[0] == USER_ID)
                             {
                                 cmbADJUSTER_CODE.SelectedValue = lst.Value;
                                 break;
                             }
                         }
                     }
                     //if(lst!=null)
                     //    cmbADJUSTER_CODE.SelectedValue = lst.Value;
                 }

                          

                txtADJUSTER_NAME.Text           = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("ADJUSTER_NAME", hidOldData.Value);
                txtSUB_ADJUSTER_LEGAL_NAME.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_LEGAL_NAME", hidOldData.Value);
                txtSUB_ADJUSTER_ADDRESS1.Text   = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_ADDRESS1", AdjusterDetails);
                txtSUB_ADJUSTER_ADDRESS2.Text   = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_ADDRESS2", AdjusterDetails);
                txtSUB_ADJUSTER_CITY.Text       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_CITY", AdjusterDetails);
                txtSUB_ADJUSTER_ZIP.Text        = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_ZIP", AdjusterDetails);
                txtSUB_ADJUSTER_PHONE.Text      = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_PHONE", AdjusterDetails);
                txtSUB_ADJUSTER_FAX.Text        = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_FAX", AdjusterDetails);
                txtSUB_ADJUSTER_EMAIL.Text      = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_EMAIL", AdjusterDetails);
                txtSUB_ADJUSTER_WEBSITE.Text    = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_WEBSITE", AdjusterDetails);
                txtSUB_ADJUSTER_NOTES.Text      = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_NOTES", AdjusterDetails);
                txtSUB_ADJUSTER.Text            = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER", AdjusterDetails);
                txtSUB_ADJUSTER_CONTACT_NAME.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_CONTACT_NAME", AdjusterDetails);
               
                txtSA_ADDRESS1.Text             = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_ADDRESS1", AdjusterDetails);
                txtSA_ADDRESS2.Text             = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_ADDRESS2", AdjusterDetails);
                txtSA_CITY.Text                 = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_CITY", AdjusterDetails);                                
                txtSA_ZIPCODE.Text              = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_ZIPCODE", AdjusterDetails);
                txtSA_PHONE.Text                = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_PHONE", AdjusterDetails);               
                txtSA_FAX.Text                  = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SA_FAX", AdjusterDetails);

                //Code add by Agniswar for Singapore Implementation on 16 Sep 2011

                txtSUB_ADJUSTER_GST.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_GST", AdjusterDetails);
                txtSUB_ADJUSTER_GST_REG_NO.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_GST_REG_NO", AdjusterDetails);
                txtSUB_ADJUSTER_MOBILE_NO.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_MOBILE", AdjusterDetails);
                txtSUB_ADJUSTER_CLASSIFICATION.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SUB_ADJUSTER_CLASSIFICATION", AdjusterDetails);

            }
            else
            {
                hidOldData.Value = "";
            }
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
			    Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objAdjuster =  new ClsAdjusterDetails();
				string strRetVal = "";
				string customInfo ="";
				customInfo +=	";Adjuster Type = " + cmbADJUSTER_TYPE.Items[cmbADJUSTER_TYPE.SelectedIndex].Text;
				customInfo +=	";Adjuster Name = " + txtADJUSTER_NAME.Text;
				//customInfo +=	";Adjuster Type = " + cmbADJUSTER_TYPE.Items[cmbADJUSTER_TYPE.SelectedIndex].Text;

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Claim Adjuster has been Deactivated Successfully.";
					objAdjuster.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objAdjuster.ActivateDeactivate(hidADJUSTER_ID.Value,"N",customInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Claim Adjuster has been Activated Successfully.";
					objAdjuster.TransactionInfoParams = objStuTransactionInfo;
					objAdjuster.ActivateDeactivate(hidADJUSTER_ID.Value,"Y",customInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"5";
				if (strRetVal == "-2")
				{
					//Current Adjuster is assigned to some claims, hence it cannot be deactivated
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"20");
				}
				else
				{
					ClientScript.RegisterStartupScript(this.GetType(),"RefreshGRid","<script>RefreshWebGrid(1,1,true)</script>");
					GetOldDataXML();
				}

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objAdjuster!= null)
					objAdjuster.Dispose();
			}
			
		}

        [System.Web.Services.WebMethod]
        public static String GetAdjusterZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }


        private void FillState(int CountryID, ref DropDownList cmbState)
        {
            Cms.BusinessLayer.BlCommon.ClsStates objStates = new Cms.BusinessLayer.BlCommon.ClsStates();
            DataSet ds = objStates.GetStatesCountry(CountryID);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cmbState.DataSource = ds;
                cmbState.DataTextField = STATE_NAME;
                cmbState.DataValueField = STATE_ID;
                cmbState.DataBind();
            }

        }

        protected void cmbADJUSTER_CODE_SelectedIndexChanged(object sender, EventArgs e)
        {

            string []delimeter={"^"};
            string AdjusterDetails=cmbADJUSTER_CODE.SelectedValue;

            cmbADJUSTER_TYPE.SelectedValue = cmbADJUSTER_TYPE.SelectedValue;

            if (AdjusterDetails != "" && AdjusterDetails!="0")
            {
                string[] Arr = AdjusterDetails.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);

                string RetXml=  Cms.BusinessLayer.BlCommon.ClsUser.GetXmlForPageControls(Arr[0]);
                txtSA_ADDRESS1.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_ADD1", RetXml);
                txtSA_ADDRESS2.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_ADD2", RetXml);
                txtSA_CITY.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_CITY", RetXml);
                txtSA_ZIPCODE.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_ZIP", RetXml);
                txtSA_PHONE.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_PHONE", RetXml);
                txtSA_FAX.Text = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_FAX", RetXml);
                string Counry = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_COUNTRY", RetXml);
                string State = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("USER_STATE", RetXml);

                if (Counry == "" || Counry == "0")
                    Counry = "5";
                              
                 if (cmbSA_COUNTRY.Items.FindByValue(Counry) != null)
                      cmbSA_COUNTRY.SelectedValue = Counry;
                  else
                      cmbSA_COUNTRY.SelectedValue = "5";//BRASIL

                 FillState(int.Parse(cmbSA_COUNTRY.SelectedValue), ref cmbSA_STATE);

              

                 if (State != "" && State != "0")
                 {
                     if (cmbSA_STATE.Items.FindByValue(State) != null)                     
                         cmbSA_STATE.SelectedValue = State;

                     hidSA_STATE.Value = cmbSA_STATE.SelectedValue;
                 }
                
            }

            
        }
	}
}
