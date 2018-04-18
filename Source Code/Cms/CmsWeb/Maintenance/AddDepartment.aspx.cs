/******************************************************************************************
<Author					: -		Ashwani
<Start Date				: -		5/9/2005 1:56:35 PM
<End Date				: -	
<Description			: - 	Generated the add department page.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: May 27, 2005- >
<Modified By			: Anshuman - >
<Purpose				: updating screenid according to menuid - >


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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
    /// <summary>
    /// Class for showing the Add Deprtment page.
    /// </summary>
    public class AddDepartment : Cms.CmsWeb.cmsbase
    {
        #region Page controls declaration
        protected System.Web.UI.WebControls.TextBox txtDEPT_CODE;
        protected System.Web.UI.WebControls.TextBox txtDEPT_NAME;
        protected System.Web.UI.WebControls.TextBox txtDEPT_ADD1;
        protected System.Web.UI.WebControls.TextBox txtDEPT_ADD2;
        protected System.Web.UI.WebControls.TextBox txtDEPT_CITY;
        protected System.Web.UI.WebControls.DropDownList cmbDEPT_STATE;
        protected System.Web.UI.WebControls.TextBox txtDEPT_ZIP;
        protected System.Web.UI.WebControls.DropDownList cmbDEPT_COUNTRY;
        protected System.Web.UI.WebControls.TextBox txtDEPT_PHONE;
        protected System.Web.UI.WebControls.TextBox txtDEPT_EXT;
        protected System.Web.UI.WebControls.TextBox txtDEPT_FAX;
        protected System.Web.UI.WebControls.TextBox txtDEPT_EMAIL;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_NAME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_ADD1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_CITY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_COUNTRY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_STATE;

       // protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_NAME;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_CODE;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_EXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_FAX;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDEPT_EMAIL;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Image imgZipLookup;
        protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
        protected System.Web.UI.WebControls.CustomValidator csvDIV_ZIP;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
        #endregion

        #region Local form variables
        //START:*********** Local form variables *************
        string oldXML;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;

        protected System.Web.UI.WebControls.Label capDEPT_NAME;
        protected System.Web.UI.WebControls.Label capDEPT_CODE;
        protected System.Web.UI.WebControls.Label capDEPT_ADD1;
        protected System.Web.UI.WebControls.Label capDEPT_ADD2;
        protected System.Web.UI.WebControls.Label capDEPT_CITY;
        protected System.Web.UI.WebControls.Label capDEPT_COUNTRY;
        protected System.Web.UI.WebControls.Label capDEPT_STATE;
        protected System.Web.UI.WebControls.Label capDEPT_ZIP;
        protected System.Web.UI.WebControls.Label capDEPT_PHONE;
        protected System.Web.UI.WebControls.Label capDEPT_EXT;
        protected System.Web.UI.WebControls.Label capDEPT_FAX;
        protected System.Web.UI.WebControls.Label capDEPT_EMAIL;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPT_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPT_ZIP;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidExt;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPT_COUNTRY;
        protected System.Web.UI.WebControls.CustomValidator csvDEPT_ZIP;
        //Defining the business layer class object
        ClsDepartment objDepartment;
        //END:*********** Local variables *************
        protected Cms.CmsWeb.Controls.CmsButton btnCopyDivisionAddress;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDIV_ADDRESS;
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
            try
            {
                //setting validation expression for different validation control
                this.revDEPT_ZIP.ValidationExpression = aRegExpZipBrazil; //aRegExpZip;
                this.revDEPT_PHONE.ValidationExpression = aRegExpAgencyPhone;//aRegExpPhone;			
                this.revDEPT_EXT.ValidationExpression = aRegExpExtn;
                this.revDEPT_EMAIL.ValidationExpression = aRegExpEmail;
                this.revDEPT_FAX.ValidationExpression = aRegExpAgencyPhone;//aRegExpFax;//done by abhinav
                //this.revDEPT_NAME.ValidationExpression = aRegExpClientName;
              //  this.revDEPT_CODE.ValidationExpression = aRegExpClientName;
                //this.revDEPT_CITY.ValidationExpression = aRegExpClientName;
                //setting error message 


                this.revDEPT_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1181");
                this.revDEPT_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
                this.revDEPT_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
                this.revDEPT_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "23");
                this.rfvDEPT_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(NewscreenId, "21");
                this.rfvDEPT_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(NewscreenId, "22");
                this.rfvDEPT_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "909");
                this.rfvDEPT_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "33");
                this.rfvDEPT_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");
                this.rfvDEPT_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "56");
              //  this.revDEPT_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "63");
               // this.revDEPT_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "64");
               // this.revDEPT_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "39");
                this.revDEPT_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "25");
                this.rfvDEPT_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "37");


            }
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }
        }
        #endregion

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                Ajax.Utility.RegisterTypeForAjax(typeof(AddDepartment));

                base.ScreenId = "29_0";
                NewscreenId = "478_1";

                btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
                txtDEPT_NAME.Attributes.Add("onblur", "javascript:generateCode();");
                txtDEPT_PHONE.Attributes.Add("onBlur", "javascript:FormatBrazilPhone();DisableExt('txtDEPT_PHONE','txtDEPT_EXT');");
                // Added by Swarup on 30-mar-2007
                imgZipLookup.Attributes.Add("style", "cursor:hand");
                base.VerifyAddress(hlkZipLookup, txtDEPT_ADD1, txtDEPT_ADD2
                    , txtDEPT_CITY, cmbDEPT_STATE, txtDEPT_ZIP);
                //btnActivateDeactivate.Attributes.Add("onclick","javascript:document.MNT_DEPT_LIST.reset();");

                //btnActivateDeactivate.Attributes.Add("onclick","javascript:DisableValidators();");
                lblMessage.Visible = false;
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");

                //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
                btnReset.CmsButtonClass = CmsButtonType.Write;
                btnReset.PermissionString = gstrSecurityXML;

                btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
                btnActivateDeactivate.PermissionString = gstrSecurityXML;

                btnDelete.CmsButtonClass = CmsButtonType.Delete;
                btnDelete.PermissionString = gstrSecurityXML;

                btnSave.CmsButtonClass = CmsButtonType.Write;
                btnSave.PermissionString = gstrSecurityXML;

                btnCopyDivisionAddress.CmsButtonClass = CmsButtonType.Write;
                btnCopyDivisionAddress.PermissionString = gstrSecurityXML;
                //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
                objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddDepartment", System.Reflection.Assembly.GetExecutingAssembly());
                btnCopyDivisionAddress.Attributes.Add("onclick", "javascript:return CopyDivisionAddress();");

                if (!Page.IsPostBack)
                {
                    //this.cmbDEPT_COUNTRY.SelectedIndex = int.Parse(aCountry);
                    SetErrorMessages();
                    #region "Loading singleton"
                    DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
                    cmbDEPT_COUNTRY.DataSource = dt;
                    cmbDEPT_COUNTRY.DataTextField = "Country_Name";
                    cmbDEPT_COUNTRY.DataValueField = "Country_Id";
                    cmbDEPT_COUNTRY.DataBind();
                    hidDEPT_COUNTRY.Value = cmbDEPT_COUNTRY.SelectedValue;
                    GetQueryString();
                    GetOldDataXML();
                    SetCaptions();
                    //dt = Cms.CmsWeb.ClsFetcher.State;
                    //cmbDEPT_STATE.DataSource		= dt;
                    //cmbDEPT_STATE.DataTextField	= "State_Name";
                    //cmbDEPT_STATE.DataValueField	= "State_Id";
                    //cmbDEPT_STATE.DataBind();
                    #endregion//Loading singleton
                    if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddDepartment.xml"))
                    {
                        setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddDepartment.xml");
                    }
                }
            }
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }

        }//end pageload
        #endregion

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private Model.Maintenance.ClsDepartmentInfo GetFormValue()
        {
            //Creating the Model object for holding the New data

            ClsDepartmentInfo objDepartmentInfo;
            objDepartmentInfo = new ClsDepartmentInfo();

            objDepartmentInfo.DEPT_CODE = txtDEPT_CODE.Text;
            objDepartmentInfo.DEPT_NAME = txtDEPT_NAME.Text;
            objDepartmentInfo.DEPT_ADD1 = txtDEPT_ADD1.Text;
            objDepartmentInfo.DEPT_ADD2 = txtDEPT_ADD2.Text;
            objDepartmentInfo.DEPT_CITY = txtDEPT_CITY.Text;
            //objDepartmentInfo.DEPT_STATE = cmbDEPT_STATE.SelectedValue;
            objDepartmentInfo.DEPT_STATE = hidSTATE.Value; 
            objDepartmentInfo.DEPT_ZIP = txtDEPT_ZIP.Text;
            objDepartmentInfo.DEPT_COUNTRY = cmbDEPT_COUNTRY.SelectedValue;
            objDepartmentInfo.DEPT_PHONE = txtDEPT_PHONE.Text;
            //objDepartmentInfo.DEPT_EXT=	txtDEPT_EXT.Text;
            objDepartmentInfo.DEPT_EXT = hidExt.Value;
            objDepartmentInfo.DEPT_FAX = txtDEPT_FAX.Text;
            objDepartmentInfo.DEPT_EMAIL = txtDEPT_EMAIL.Text;

            //These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = hidDEPT_ID.Value;
            //hidSTATE.Value = cmbDEPT_STATE.SelectedValue;
            oldXML = hidOldData.Value;
            //Returning the model object

            return objDepartmentInfo;
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
                objDepartment = new ClsDepartment();

                //Retreiving the form values into model class object
                ClsDepartmentInfo objDepartmentInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objDepartmentInfo.CREATED_BY = int.Parse(GetUserId());
                    objDepartmentInfo.CREATED_DATETIME = DateTime.Now;

                    //Calling the add method of business layer class
                    intRetVal = objDepartment.Add(objDepartmentInfo);

                    if (intRetVal > 0)
                    {
                        hidDEPT_ID.Value = objDepartmentInfo.DEPT_ID.ToString();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        GetOldDataXML();
                        revDEPT_ZIP.Enabled = false;
                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {

                    //Creating the Model object for holding the Old data
                    ClsDepartmentInfo objOldDepartmentInfo;
                    objOldDepartmentInfo = new ClsDepartmentInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldDepartmentInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objDepartmentInfo.DEPT_ID = int.Parse(strRowId);
                    objDepartmentInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objDepartmentInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    objDepartmentInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

                    //Updating the record using business layer class object
                    intRetVal = objDepartment.Update(objOldDepartmentInfo, objDepartmentInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        GetOldDataXML();
                        revDEPT_ZIP.Enabled = false;
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                        hidFormSaved.Value = "1";
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                        hidFormSaved.Value = "1";
                    }
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                hidFormSaved.Value = "2";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

                //ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objDepartment != null)
                    objDepartment.Dispose();
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
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                objDepartment = new ClsDepartment();

                Model.Maintenance.ClsDepartmentInfo objDepartmentInfo;
                objDepartmentInfo = GetFormValue();
                string strRetVal = "";
                string strCustomInfo = "";
                strCustomInfo = "Department Name:" + objDepartmentInfo.DEPT_NAME + "<br>" + "Department Code:" + objDepartmentInfo.DEPT_CODE;
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    //objStuTransactionInfo.transactionDescription = "Deactivated Successfully.";
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "4");
                    objDepartment.TransactionInfoParams = objStuTransactionInfo;
                    strRetVal = objDepartment.ActivateDeactivate(hidDEPT_ID.Value, "N", strCustomInfo);
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                }
                else
                {
                    //objStuTransactionInfo.transactionDescription = "Activated Successfully.";
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "3");
                    objDepartment.TransactionInfoParams = objStuTransactionInfo;
                    objDepartment.ActivateDeactivate(hidDEPT_ID.Value, "Y", strCustomInfo);
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                }

                if (strRetVal == "-1")
                {
                    /*Profit Center is assigned*/
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "513");
                    lblDelete.Visible = false;
                }

                hidFormSaved.Value = "1";
                GetOldDataXML();
                hidReset.Value = "0";

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (objDepartment != null)
                    objDepartment.Dispose();
            }
        }
        #endregion

        private void SetCaptions()
        {
            try
            {
                capDEPT_CODE.Text = objResourceMgr.GetString("txtDEPT_CODE");
                capDEPT_NAME.Text = objResourceMgr.GetString("txtDEPT_NAME");
                capDEPT_ADD1.Text = objResourceMgr.GetString("txtDEPT_ADD1");
                capDEPT_ADD2.Text = objResourceMgr.GetString("txtDEPT_ADD2");
                capDEPT_CITY.Text = objResourceMgr.GetString("txtDEPT_CITY");
                capDEPT_STATE.Text = objResourceMgr.GetString("cmbDEPT_STATE");
                capDEPT_ZIP.Text = objResourceMgr.GetString("txtDEPT_ZIP");
                capDEPT_COUNTRY.Text = objResourceMgr.GetString("cmbDEPT_COUNTRY");
                capDEPT_PHONE.Text = objResourceMgr.GetString("txtDEPT_PHONE");
                capDEPT_EXT.Text = objResourceMgr.GetString("txtDEPT_EXT");
                capDEPT_FAX.Text = objResourceMgr.GetString("txtDEPT_FAX");
                capDEPT_EMAIL.Text = objResourceMgr.GetString("txtDEPT_EMAIL");
                btnCopyDivisionAddress.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1324");
                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("NEW");
                hidTitle.Value = objResourceMgr.GetString("hidTitle");
            }
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }
        }


        private void GetQueryString()
        {
            hidDEPT_ID.Value = Request.Params["DEPT_ID"];
        }


        private void GetOldDataXML()
        {
            if (hidDEPT_ID.Value != "")
            {
                hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsDepartment.GetDepartmentXml(
                    int.Parse(hidDEPT_ID.Value));
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
            string strSelectedState = ClsCommon.FetchValueFromXML("DEPT_STATE", strXML);
            hidSTATE.Value = strSelectedState; 
            string strSelectedCountry = ClsCommon.FetchValueFromXML("DEPT_COUNTRY", strXML);
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {
                PopulateStateDropDown(int.Parse(strSelectedCountry));
            }
            else
                if (hidDEPT_COUNTRY.Value != "")
                {
                    PopulateStateDropDown(Int32.Parse(hidDEPT_COUNTRY.Value));
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

            cmbDEPT_STATE.Items.Clear();
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbDEPT_STATE.DataSource = dtStates;
                cmbDEPT_STATE.DataTextField = STATE_NAME;
                cmbDEPT_STATE.DataValueField = STATE_ID;
                cmbDEPT_STATE.DataBind();
                cmbDEPT_STATE.Items.Insert(0, "");
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string AjaxFetchZipForState(int stateID, string ZipID)
        {
            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
            string result = "";
            result = obj.FetchZipForState(stateID, ZipID);
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

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            int intRetVal;
            int intDeptID = int.Parse(hidDEPT_ID.Value);
            int modifiedBy = int.Parse(GetUserId());
            Model.Maintenance.ClsDepartmentInfo objDepartmentInfo;
            objDepartmentInfo = GetFormValue();
            string customInfo = "";
            customInfo = "; Department Name:" + objDepartmentInfo.DEPT_NAME + "<br>" + "; Department Code:" + objDepartmentInfo.DEPT_CODE;
            objDepartment = new Cms.BusinessLayer.BlCommon.ClsDepartment();
            intRetVal = objDepartment.DeleteDepartment(intDeptID, customInfo, modifiedBy);

            if (intRetVal > 0)
            {
                lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                hidFormSaved.Value = "5";
                hidOldData.Value = "";
                trBody.Attributes.Add("style", "display:none");

            }
            else if (intRetVal == -1)
            {
                lblDelete.Visible = true;
                lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "505");
                hidFormSaved.Value = "2";
            }
            lblDelete.Visible = true;
        }
    }
}
