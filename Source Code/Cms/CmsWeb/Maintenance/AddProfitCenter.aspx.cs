/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	5/9/2005 3:12:38 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 26/08/2005
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using System.Resources;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;

namespace Cms.CmsWeb
{
	/// <summary>
	/// 
	/// </summary>
	public class AddProfitCenter :Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtPC_CODE;
		protected System.Web.UI.WebControls.TextBox txtPC_NAME;
		protected System.Web.UI.WebControls.TextBox txtPC_ADD1;
		protected System.Web.UI.WebControls.TextBox txtPC_ADD2;
		protected System.Web.UI.WebControls.TextBox txtPC_ZIP;
		protected System.Web.UI.WebControls.TextBox txtPC_PHONE;
		protected System.Web.UI.WebControls.TextBox txtPC_EXT;
		protected System.Web.UI.WebControls.TextBox txtPC_FAX;
		protected System.Web.UI.WebControls.TextBox txtPC_EMAIL;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPC_STATE;

		//protected System.Web.UI.WebControls.RegularExpressionValidator revPC_NAME;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revPC_CODE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revPC_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPC_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPC_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPC_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPC_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPC_EMAIL;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.CustomValidator csvPC_ZIP;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capPC_NAME;
		protected System.Web.UI.WebControls.Label capPC_CODE;
		protected System.Web.UI.WebControls.Label capPC_ADD1;
		protected System.Web.UI.WebControls.Label capPC_ADD2;
		protected System.Web.UI.WebControls.Label capPC_CITY;
		protected System.Web.UI.WebControls.Label capPC_COUNTRY;
		protected System.Web.UI.WebControls.Label capPC_STATE;
		protected System.Web.UI.WebControls.Label capPC_ZIP;
		protected System.Web.UI.WebControls.Label capPC_PHONE;
		protected System.Web.UI.WebControls.Label capPC_EXT;
		protected System.Web.UI.WebControls.Label capPC_FAX;
		protected System.Web.UI.WebControls.Label capPC_EMAIL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPC_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPC_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbPC_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbPC_STATE;
		protected System.Web.UI.WebControls.TextBox txtPC_CITY;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		//Defining the business layer class object
		private Cms.BusinessLayer.BlCommon.ClsProfitCentre ObjProfitCenter ;
		//END:*********** Local variables *************
		protected Cms.CmsWeb.Controls.CmsButton btnCopyDivisionAddress;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDIV_ADDRESS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidExt;
        private string NewscreenId;
		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
            this.revPC_ZIP.ValidationExpression = aRegExpZipBrazil;// aRegExpZip;done by abhinav
            this.revPC_PHONE.ValidationExpression =aRegExpAgencyPhone;//aRegExpPhone;
            this.revPC_FAX.ValidationExpression =aRegExpAgencyPhone;//aRegExpFax;//done by abhinav
			this.revPC_EXT.ValidationExpression=aRegExpExtn;
			this.revPC_EMAIL.ValidationExpression=aRegExpEmail;

			//this.revPC_NAME.ValidationExpression=aRegExpClientName;
			//this.revPC_CODE.ValidationExpression=aRegExpClientName;
			//this.revPC_CITY.ValidationExpression=aRegExpClientName;


            this.revPC_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1181");
            this.revPC_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
			this.revPC_EXT.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			this.revPC_EMAIL.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
            this.revPC_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
			this.rfvPC_NAME.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(NewscreenId,"23");
            this.rfvPC_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(NewscreenId, "24");
			this.rfvPC_ADD1.ErrorMessage=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"909");
			this.rfvPC_COUNTRY.ErrorMessage=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"33");
			this.rfvPC_STATE.ErrorMessage=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"35");
			this.rfvPC_CITY.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"56");
			//this.revPC_NAME.ErrorMessage =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"63");
			//this.revPC_CODE.ErrorMessage =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"64");
			//this.revPC_CITY.ErrorMessage =	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"39");
			this.rfvPC_ZIP.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"37");  
			//this.revPC_ZIP.ErrorMessage = 

			

		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(AddProfitCenter));
			
			
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			//btnActivateDeactivate.Attributes.Add("onclick","javascript:document.MNT_PROFIT_CENTER_LIST.reset();");
			txtPC_NAME.Attributes.Add("onblur","javascript:generateCode();");
            txtPC_PHONE.Attributes.Add("onBlur", "javascript:FormatBrazilPhone();DisableExt('txtPC_PHONE','txtPC_EXT');");
			// Added by Swarup on 30-mar-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtPC_ADD1,txtPC_ADD2
				, txtPC_CITY, cmbPC_STATE, txtPC_ZIP);

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="27_0";
            NewscreenId = "478_1";
			lblMessage.Visible = false;
			
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//btnReset.CmsButtonClass	=	Cms.CmsWeb.WebControls.;
			btnReset.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;

			btnDelete.CmsButtonClass   = Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;

			btnCopyDivisionAddress.CmsButtonClass =     Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnCopyDivisionAddress.PermissionString =	gstrSecurityXML;
            btnCopyDivisionAddress.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1324"); //sneha

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.AddProfitCenter",System.Reflection.Assembly.GetExecutingAssembly());

			btnCopyDivisionAddress.Attributes.Add("onclick","javascript:return CopyDivisionAddress();") ;

			if(!Page.IsPostBack)
			{
                this.cmbPC_COUNTRY.SelectedIndex=int.Parse(aCountry);

                SetErrorMessages();
				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
				cmbPC_COUNTRY.DataSource		= dt;
				this.cmbPC_COUNTRY.DataTextField	= "Country_Name";
				cmbPC_COUNTRY.DataValueField	= "Country_Id";
				cmbPC_COUNTRY.DataBind();
                hidPC_COUNTRY.Value = cmbPC_COUNTRY.SelectedValue;
                GetQueryString();
                GetOldDataXML();
                SetCaptions();
                //dt = Cms.CmsWeb.ClsFetcher.State;
                //cmbPC_STATE.DataSource		= dt;
                //cmbPC_STATE.DataTextField	= "State_Name";
                //cmbPC_STATE.DataValueField	= "State_Id";
                //cmbPC_STATE.DataBind();
				#endregion//Loading singleton
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddProfitCenter.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddProfitCenter.xml");
                }
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
		private Cms.Model.Maintenance.ClsProfitCenterInfo  GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsProfitCenterInfo objProfitCenterInfo;
			objProfitCenterInfo = new ClsProfitCenterInfo();

			objProfitCenterInfo.PC_CODE=	txtPC_CODE.Text;
			objProfitCenterInfo.PC_NAME=	txtPC_NAME.Text;
			objProfitCenterInfo.PC_ADD1=	txtPC_ADD1.Text;
			objProfitCenterInfo.PC_ADD2=	txtPC_ADD2.Text;
			objProfitCenterInfo.PC_CITY=	txtPC_CITY.Text;
			//objProfitCenterInfo.PC_STATE=	txtPC_STATEText;
			objProfitCenterInfo.PC_ZIP=	txtPC_ZIP.Text;
            //if(cmbPC_STATE.SelectedIndex >= 0)
            //    objProfitCenterInfo.PC_STATE=cmbPC_STATE.SelectedValue;
            //else
            //    objProfitCenterInfo.PC_STATE="";
            objProfitCenterInfo.PC_STATE = hidSTATE.Value;
			objProfitCenterInfo.PC_COUNTRY=	cmbPC_COUNTRY.SelectedValue;
			objProfitCenterInfo.PC_PHONE=	txtPC_PHONE.Text;
			//objProfitCenterInfo.PC_EXT=	txtPC_EXT.Text;
            objProfitCenterInfo.PC_EXT = hidExt.Value;
			objProfitCenterInfo.PC_FAX=	txtPC_FAX.Text;
			objProfitCenterInfo.PC_EMAIL=	txtPC_EMAIL.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidPC_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objProfitCenterInfo;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
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
				ObjProfitCenter = new  Cms.BusinessLayer.BlCommon.ClsProfitCentre();

				//Retreiving the form values into model class object
				ClsProfitCenterInfo ObjProfitCenterInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					ObjProfitCenterInfo.CREATED_BY = int.Parse(GetUserId());
					ObjProfitCenterInfo.CREATED_DATETIME = DateTime.Now;
                    ObjProfitCenterInfo.CUSTOM_INFO = ClsMessages.GetMessage(NewscreenId, "17") + ":" + ObjProfitCenterInfo.PC_NAME + "<br>" + ClsMessages.GetMessage(NewscreenId, "18") + ":" + ObjProfitCenterInfo.PC_CODE;
					//Calling the add method of business layer class
					intRetVal = ObjProfitCenter.Add(ObjProfitCenterInfo);

					if(intRetVal>0)
					{
						hidPC_ID.Value = ObjProfitCenterInfo.PC_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
                        revPC_ZIP.Enabled = false;
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
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
					ClsProfitCenterInfo objOldProfitCenterInfo;
					objOldProfitCenterInfo = new ClsProfitCenterInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldProfitCenterInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					ObjProfitCenterInfo.PC_ID = int.Parse(strRowId);
					ObjProfitCenterInfo.MODIFIED_BY = int.Parse(GetUserId());
					ObjProfitCenterInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					ObjProfitCenterInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= ObjProfitCenter.Update(objOldProfitCenterInfo,ObjProfitCenterInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
                        revPC_ZIP.Enabled = false;
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
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
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(ObjProfitCenter!= null)
					ObjProfitCenter.Dispose();
			}
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				ObjProfitCenter =  new ClsProfitCentre();
				Model.Maintenance.ClsProfitCenterInfo ObjProfitCenterInfo;
				ObjProfitCenterInfo = GetFormValue();

				string strRetVal = "";
				string strCustomInfo ="";
                strCustomInfo = ClsMessages.GetMessage(NewscreenId, "17") + ":" + ObjProfitCenterInfo.PC_NAME + "<br>" + ClsMessages.GetMessage(NewscreenId, "18") + ":" + ObjProfitCenterInfo.PC_CODE;
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					//objStuTransactionInfo.transactionDescription = "Deactivated Successfully.";
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "8");
					ObjProfitCenter.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = ObjProfitCenter.ActivateDeactivate(hidPC_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					//objStuTransactionInfo.transactionDescription = "Activated Successfully.";
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "7");
					ObjProfitCenter.TransactionInfoParams = objStuTransactionInfo;
					ObjProfitCenter.ActivateDeactivate(hidPC_ID.Value,"Y",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				
				if (strRetVal == "-1")
				{
					/*Profit Center is assigned*/
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"514");
					lblDelete.Visible = false;
				}

				hidFormSaved.Value			=	"1";
				GetOldDataXML();
				hidReset.Value="0";
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
				if(ObjProfitCenter!= null)
					ObjProfitCenter.Dispose();
			}
		}
		#endregion
		private void SetCaptions()
		{
			capPC_CODE.Text						=		objResourceMgr.GetString("txtPC_CODE");
			capPC_NAME.Text						=		objResourceMgr.GetString("txtPC_NAME");
			capPC_ADD1.Text						=		objResourceMgr.GetString("txtPC_ADD1");
			capPC_ADD2.Text						=		objResourceMgr.GetString("txtPC_ADD2");
			capPC_CITY.Text						=		objResourceMgr.GetString("txtPC_CITY");
			capPC_STATE.Text					=		objResourceMgr.GetString("cmbPC_STATE");
			capPC_ZIP.Text						=		objResourceMgr.GetString("txtPC_ZIP");
			capPC_COUNTRY.Text					=		objResourceMgr.GetString("cmbPC_COUNTRY");
			capPC_PHONE.Text					=		objResourceMgr.GetString("txtPC_PHONE");
			capPC_EXT.Text						=		objResourceMgr.GetString("txtPC_EXT");
			capPC_FAX.Text						=		objResourceMgr.GetString("txtPC_FAX");
			capPC_EMAIL.Text					=		objResourceMgr.GetString("txtPC_EMAIL");
            btnCopyDivisionAddress.Text         =       objResourceMgr.GetString("btnCopyDivisionAddress"); //sneha
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("NEW");
            hidTitle.Value = objResourceMgr.GetString("hidTitle");
		}
		private void GetOldDataXML()
		{
			if ( hidPC_ID.Value != "" ) 
			{
				hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsProfitCentre.GetProfitCenterXml(int.Parse(hidPC_ID.Value));
			}
            FetchCountryState(hidOldData.Value);
            hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value);
            if (hidOldData.Value != "")
            {
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }

                else
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }

            }
           
           
		}

         private void FetchCountryState(string strXML)
        {
            string strSelectedState = ClsCommon.FetchValueFromXML("PC_STATE", strXML);
            string strSelectedCountry = ClsCommon.FetchValueFromXML("PC_COUNTRY", strXML);
            hidSTATE.Value = strSelectedState;
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {
                PopulateStateDropDown(int.Parse(strSelectedCountry));
            }
            else
                if (hidPC_COUNTRY.Value != "")
                {
                    PopulateStateDropDown(Int32.Parse(hidPC_COUNTRY.Value));
                }
                else
                {
                    PopulateStateDropDown(5);
                }
        }

         private void PopulateStateDropDown(int COUNTRY_ID)
         {
             ClsStates objStates = new ClsStates();
             DataSet dsStates;
             if (COUNTRY_ID == 0)
                 return;
             else
                 dsStates = objStates.GetStatesCountry(COUNTRY_ID);

             cmbPC_STATE.Items.Clear();
             DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
             if (dtStates != null && dtStates.Rows.Count > 0)
             {
                 cmbPC_STATE.DataSource = dtStates;
                 cmbPC_STATE.DataTextField = STATE_NAME;
                 cmbPC_STATE.DataValueField = STATE_ID;
                 cmbPC_STATE.DataBind();
                 cmbPC_STATE.Items.Insert(0, "");
             }
         }

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			
			int intPCID = int.Parse(hidPC_ID.Value);
			int modifiedBy = int.Parse(GetUserId());
			Model.Maintenance.ClsProfitCenterInfo ObjProfitCenterInfo;
			ObjProfitCenterInfo = GetFormValue();
			//string  modifiedBy = ObjProfitCenterInfo.MODIFIED_BY;
			string customInfo="";
            customInfo = ClsMessages.GetMessage(NewscreenId, "17") + ":" + ObjProfitCenterInfo.PC_NAME + "<br>" + ClsMessages.GetMessage(NewscreenId, "18") + ":" + ObjProfitCenterInfo.PC_CODE;
			ObjProfitCenter = new Cms.BusinessLayer.BlCommon.ClsProfitCentre();
			intRetVal = ObjProfitCenter.DeleteProfitCenter(intPCID,customInfo,modifiedBy);
			
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
				
			}
			else if(intRetVal == -1)
			{
				lblDelete.Visible =true;
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"489");
				hidFormSaved.Value		=	"2";
			}
			lblDelete.Visible = true;
		}

		private void GetQueryString()
		{
			hidPC_ID.Value = Request.Params["PC_ID"];
		}
	}
}
