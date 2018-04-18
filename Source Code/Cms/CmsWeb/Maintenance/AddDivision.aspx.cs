/******************************************************************************************
<Author					: -		Ashwani
<Start Date				: -		5/10/2005 10:27:01 AM
<End Date				: -	
<Description			: -		Shows the add divison page.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: May 27, 2005- >
<Modified By			: Anshuman - >
<Purpose				: updating screenid according to menuid - > 

<Modified Date			: July 13, 2005- >
<Modified By			: Gaurav - >
<Purpose				: Implement Delete functionality and auto generate code - > 

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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using System.Xml;

namespace Cms.CmsWeb
{
    /// <summary>
    /// Class for showing the Add Divsion. 
    /// </summary>
    public class clsAddDivision : Cms.CmsWeb.cmsbase
    {
        #region Page controls declaration
        protected System.Web.UI.WebControls.TextBox txtDIV_CODE;
        protected System.Web.UI.WebControls.TextBox txtDIV_NAME;
        protected System.Web.UI.WebControls.TextBox txtDIV_ADD1;
        protected System.Web.UI.WebControls.TextBox txtDIV_ADD2;
        protected System.Web.UI.WebControls.TextBox txtDIV_CITY;
        protected System.Web.UI.WebControls.DropDownList cmbDIV_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbACTIVITY;
        protected System.Web.UI.WebControls.TextBox txtDIV_ZIP;
        protected System.Web.UI.WebControls.DropDownList cmbDIV_COUNTRY;
        protected System.Web.UI.WebControls.TextBox txtDIV_PHONE;
        protected System.Web.UI.WebControls.TextBox txtDIV_EXT;
        protected System.Web.UI.WebControls.TextBox txtDIV_FAX;
        protected System.Web.UI.WebControls.TextBox txtDIV_EMAIL;
        protected System.Web.UI.WebControls.TextBox txtBRANCH_CODE;
        protected System.Web.UI.WebControls.TextBox txtBRANCH_CNPJ;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_IDENTIFICATION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_NAME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_ADD1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_CITY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_COUNTRY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_STATE;
       // protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_BRANCHCODE;   Changed by Amit on 29 sep 2011 for tfs bug #
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBRANCH_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREG_ID_ISSUE_DATE;
        // protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_BRANCHCNPJ;
        public string javasciptmsg, javasciptCNPJmsg, CPF_invalid, CNPJ_invalid;

        //  protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_NAME;
        // protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_CODE;
        // protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_EXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_FAX;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_EMAIL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDIV_BRANCHCODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBRANCH_CNPJ;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Image imgZipLookup;
        protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
        protected System.Web.UI.WebControls.CustomValidator csvDIV_ZIP;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.Label capACTIVITY;
        protected System.Web.UI.WebControls.Label capREG_ID_ISSUE;
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUE;
        protected System.Web.UI.WebControls.CompareValidator cpvREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvCREATION_DATE;
        private string NewscreenId;


        #endregion

        #region Local form variables
        //START:*********** Local form variables *************
        string oldXML;

       // string ISACTIVE;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
       // private int intLoggedInUserID;
        protected System.Web.UI.WebControls.Label capDIV_NAME;
        protected System.Web.UI.WebControls.Label capDIV_CODE;
        protected System.Web.UI.WebControls.Label capDIV_ADD1;
        protected System.Web.UI.WebControls.Label capDIV_ADD2;
        protected System.Web.UI.WebControls.Label capDIV_CITY;
        protected System.Web.UI.WebControls.Label capDIV_COUNTRY;
        protected System.Web.UI.WebControls.Label capDIV_STATE;
        protected System.Web.UI.WebControls.Label capDIV_ZIP;
        protected System.Web.UI.WebControls.Label capDIV_PHONE;
        protected System.Web.UI.WebControls.Label capDIV_EXT;
        protected System.Web.UI.WebControls.Label capDIV_FAX;
        protected System.Web.UI.WebControls.Label capDIV_EMAIL;
        protected System.Web.UI.WebControls.Label capDIV_BRANCHCODE;
        protected System.Web.UI.WebControls.Label capDIV_BRANCHCNPJ;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDIV_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_ZIP;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidExt;
        protected System.Web.UI.WebControls.Label capNAIC_CODE;
        protected System.Web.UI.WebControls.TextBox txtNAIC_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDIV_COUNTRY;
        protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.HyperLink hlkREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
        //Defining the business layer class object
        ClsDivision objDivision;

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
            try
            {
                //setting validation expression for different validation control
                // revDIV_NAME.ValidationExpression = aRegExpClientName;
                //  revDIV_CITY.ValidationExpression = aRegExpClientName;
                revDIV_ZIP.ValidationExpression = aRegExpZipBrazil;// aRegExpZip;
                revDIV_PHONE.ValidationExpression = aRegExpAgencyPhone; //aRegExpPhone;

                revDIV_EXT.ValidationExpression = aRegExpExtn;
                revDIV_FAX.ValidationExpression = aRegExpAgencyPhone;//aRegExpFax;
                revDIV_EMAIL.ValidationExpression = aRegExpEmail;
                //  revDIV_CODE.ValidationExpression = aRegExpClientName;
                revDIV_BRANCHCODE.ValidationExpression = aRegExpBranchCode;
                revBRANCH_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
                revDATE_OF_BIRTH.ValidationExpression = aRegExpDate;
                CNPJ_invalid = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1206");
                this.revDIV_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");
                this.revDIV_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2007");
                this.revDIV_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1974");
                this.revDIV_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
                this.revDIV_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
                this.rfvDIV_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(NewscreenId, "19");
                this.rfvDIV_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(NewscreenId, "20");
                this.rfvDIV_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "909");
                this.rfvDIV_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "33");
                this.rfvDIV_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");
                this.rfvDIV_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "56");
                // this.revDIV_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "63");
                //  this.revDIV_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "64");
                // this.revDIV_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "39");
                this.rfvDIV_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
                this.revREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
                this.revDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
                this.revBRANCH_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1203");
                //this.rfvDIV_BRANCHCODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1089");
                this.rfvBRANCH_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1089");
                //this.rfvDIV_BRANCHCNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1089");
                this.revDIV_BRANCHCODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1090");
                // this.revDIV_BRANCHCNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1090");
                csvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("198");
                javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1203");
                cpvREG_ID_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1988");
                cpvREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
                csvCREATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1409");
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
            Ajax.Utility.RegisterTypeForAjax(typeof(clsAddDivision));


            btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
            txtDIV_NAME.Attributes.Add("onblur", "javascript:generateCode();");
           // txtDIV_PHONE.Attributes.Add("onBlur", "javascript:DisableExt('txtDIV_PHONE','txtDIV_EXT');setExtFocus();");
            //txtDIV_PHONE.Attributes.Add("onMouseOut","javascript:DisableExt('txtDIV_PHONE','txtDIV_EXT');setExtFocus();");
            //txtDIV_PHONE.Attributes.Add("onLostFocus","javascript:DisableExt('txtDIV_PHONE','txtDIV_EXT');setExtFocus();");
            //btnActivateDeactivate.Attributes.Add("onclick","javascript:document.MNT_DIV_LIST.reset();");
            hlkDATE_OF_BIRTH.Attributes.Add("onclick", "fPopCalendar(document.MNT_DIV_LIST.txtDATE_OF_BIRTH, document.MNT_DIV_LIST.txtDATE_OF_BIRTH)");

            hlkREG_ID_ISSUE_DATE.Attributes.Add("onclick", "fPopCalendar(document.MNT_DIV_LIST.txtREG_ID_ISSUE_DATE, document.MNT_DIV_LIST.txtREG_ID_ISSUE_DATE)");
            
            base.ScreenId = "28_0";
            lblMessage.Visible = false;
          
            NewscreenId = "478_1";
            Page.DataBind();
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
            
            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.clsAddDivision", System.Reflection.Assembly.GetExecutingAssembly());
            cpvREG_ID_ISSUE.Style.Add("display", "none");
            cpvREG_ID_ISSUE.Enabled = false;
            if (!Page.IsPostBack)
            {

                // Added by Swarup on 30-mar-2007
                imgZipLookup.Attributes.Add("style", "cursor:hand");
                SetErrorMessages();
                base.VerifyAddress(hlkZipLookup, txtDIV_ADD1, txtDIV_ADD2
                    , txtDIV_CITY, cmbDIV_STATE, txtDIV_ZIP);


                #region "Loading singleton"
                DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
                cmbDIV_COUNTRY.DataSource = dt;
                cmbDIV_COUNTRY.DataTextField = "Country_Name";
                cmbDIV_COUNTRY.DataValueField = "Country_Id";
                cmbDIV_COUNTRY.DataBind();
                this.cmbDIV_COUNTRY.SelectedIndex = int.Parse(aCountry);
                hidDIV_COUNTRY.Value = cmbDIV_COUNTRY.SelectedValue;
                GetQueryString();
               
                GetOldDataXML();
                SetCaptions();
                Bindactivity();
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddDivision.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddDivision.xml");
                }
                //cmbDIV_COUNTRY.Items.Insert(0, new ListItem("", ""));
                //dt = Cms.CmsWeb.ClsFetcher.State;
                //cmbDIV_STATE.DataSource		= dt;
                //cmbDIV_STATE.DataTextField	= "State_Name";
                //cmbDIV_STATE.DataValueField	= "State_Id";
                //cmbDIV_STATE.DataBind();
                #endregion//Loading singleton
               
            }
        }//end pageload
        #endregion

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private Cms.Model.Maintenance.ClsDivisionInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsDivisionInfo objDivisionInfo;
            objDivisionInfo = new ClsDivisionInfo();

            objDivisionInfo.DIV_CODE = txtDIV_CODE.Text;
            objDivisionInfo.DIV_NAME = txtDIV_NAME.Text;
            objDivisionInfo.DIV_ADD1 = txtDIV_ADD1.Text;
            objDivisionInfo.DIV_ADD2 = txtDIV_ADD2.Text;
            objDivisionInfo.DIV_CITY = txtDIV_CITY.Text;
            //objDivisionInfo.DIV_STATE = cmbDIV_STATE.SelectedValue;
            objDivisionInfo.DIV_STATE = hidSTATE.Value;
            objDivisionInfo.DIV_ZIP = txtDIV_ZIP.Text;
            objDivisionInfo.DIV_COUNTRY = cmbDIV_COUNTRY.SelectedValue;
           // objDivisionInfo.ACTIVITY = int.Parse(cmbACTIVITY.SelectedValue).ToString();
            objDivisionInfo.DIV_PHONE = txtDIV_PHONE.Text;
            //objDivisionInfo.DIV_EXT=	txtDIV_EXT.Text;
            objDivisionInfo.DIV_EXT = hidExt.Value;
            objDivisionInfo.DIV_FAX = txtDIV_FAX.Text;
            objDivisionInfo.DIV_EMAIL = txtDIV_EMAIL.Text;
            objDivisionInfo.NAIC_CODE = txtNAIC_CODE.Text;
            objDivisionInfo.BRANCH_CODE = txtBRANCH_CODE.Text;
            objDivisionInfo.BRANCH_CNPJ = txtBRANCH_CNPJ.Text;
            if (cmbACTIVITY.SelectedItem != null && cmbACTIVITY.SelectedItem.Value != "")
                objDivisionInfo.ACTIVITY = int.Parse(cmbACTIVITY.SelectedItem.Value);
            if (txtDATE_OF_BIRTH.Text.Trim() != "")
                objDivisionInfo.DATE_OF_BIRTH = Convert.ToDateTime(txtDATE_OF_BIRTH.Text.Trim());
            if (txtREG_ID_ISSUE_DATE.Text != "")
            {
                objDivisionInfo.REG_ID_ISSUE_DATE = Convert.ToDateTime(txtREG_ID_ISSUE_DATE.Text);
            }

            //objDivisionInfo.DATE_OF_BIRTH = ConvertToDate(txtDATE_OF_BIRTH.Text);
            //objDivisionInfo.REG_ID_ISSUE_DATE = ConvertToDate(txtREG_ID_ISSUE_DATE.Text);//REGIONAL_ID_ISSUE_DATE
            objDivisionInfo.REG_ID_ISSUE = txtREG_ID_ISSUE.Text;
            objDivisionInfo.REGIONAL_IDENTIFICATION = txtREGIONAL_IDENTIFICATION.Text;
            //These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = hidDIV_ID.Value;
            oldXML = hidOldData.Value;
            //Returning the model object

            return objDivisionInfo;
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
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
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
            catch// (Exception ex)
            {
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
                objDivision = new ClsDivision();

                //Retreiving the form values into model class object
                ClsDivisionInfo objDivisionInfo = GetFormValue();
                
                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objDivisionInfo.CREATED_BY = int.Parse(GetUserId());
                    objDivisionInfo.CREATED_DATETIME = DateTime.Now;

                    //Calling the add method of business layer class
                    intRetVal = objDivision.Add(objDivisionInfo);

                    if (intRetVal > 0)
                    {
                        hidDIV_ID.Value = objDivisionInfo.DIV_ID.ToString();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        GetOldDataXML();
                        revDIV_ZIP.Enabled = false;
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
                    ClsDivisionInfo objOldDivisionInfo;
                    objOldDivisionInfo = new ClsDivisionInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldDivisionInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objDivisionInfo.DIV_ID = int.Parse(strRowId);
                    objDivisionInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objDivisionInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    objDivisionInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

                    //Updating the record using business layer class object
                    intRetVal = objDivision.Update(objOldDivisionInfo, objDivisionInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        GetOldDataXML();
                        revDIV_ZIP.Enabled = false;
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
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objDivision != null)
                    objDivision.Dispose();
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
                objDivision = new ClsDivision();

                Model.Maintenance.ClsDivisionInfo objDivisionInfo;
                objDivisionInfo = GetFormValue();
                string strRetVal = "";
                string strCustomInfo = "";
                strCustomInfo = "Division Name:" + objDivisionInfo.DIV_NAME + "<br>" + "Division Code:" + objDivisionInfo.DIV_CODE;
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    //objStuTransactionInfo.transactionDescription = "Deactivated Successfully.";
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "2");
                    objDivision.TransactionInfoParams = objStuTransactionInfo;

                    strRetVal = objDivision.ActivateDeactivate(hidDIV_ID.Value, "N", strCustomInfo);
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }
                else
                {
                    //objStuTransactionInfo.transactionDescription = "Activated Successfully.";
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "1");
                    objDivision.TransactionInfoParams = objStuTransactionInfo;
                    strRetVal = objDivision.ActivateDeactivate(hidDIV_ID.Value, "Y", strCustomInfo);
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }

                if (strRetVal == "-1")
                {
                    /*Division is assigned*/
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "512");
                    lblDelete.Visible = false;

                }


                hidFormSaved.Value = "0";
                GetOldDataXML();
                hidReset.Value = "0";
                //Added by Swastika on 10th Mar'06 for Gen Iss #2426
               ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidDIV_ID.Value + ");</script>");

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (objDivision != null)
                    objDivision.Dispose();
            }
        }
        #endregion

        # region  function SetCaptions()
        private void SetCaptions()
        {
            capDIV_CODE.Text = objResourceMgr.GetString("txtDIV_CODE");
            capDIV_NAME.Text = objResourceMgr.GetString("txtDIV_NAME");
            capDIV_ADD1.Text = objResourceMgr.GetString("txtDIV_ADD1");
            capDIV_ADD2.Text = objResourceMgr.GetString("txtDIV_ADD2");
            capDIV_CITY.Text = objResourceMgr.GetString("txtDIV_CITY");
            capDIV_STATE.Text = objResourceMgr.GetString("cmbDIV_STATE");
            capDIV_ZIP.Text = objResourceMgr.GetString("txtDIV_ZIP");
            capDIV_COUNTRY.Text = objResourceMgr.GetString("cmbDIV_COUNTRY");
            capDIV_PHONE.Text = objResourceMgr.GetString("txtDIV_PHONE");
            capDIV_EXT.Text = objResourceMgr.GetString("txtDIV_EXT");
            capDIV_FAX.Text = objResourceMgr.GetString("txtDIV_FAX");
            capDIV_EMAIL.Text = objResourceMgr.GetString("txtDIV_EMAIL");
            capNAIC_CODE.Text = objResourceMgr.GetString("txtNAIC_CODE");
            capDIV_BRANCHCODE.Text = objResourceMgr.GetString("txtBRANCH_CODE");
            capDIV_BRANCHCNPJ.Text = objResourceMgr.GetString("txtBRANCH_CNPJ");
            capDATE_OF_BIRTH.Text = objResourceMgr.GetString("txtDATE_OF_BIRTH");
            capREGIONAL_ID.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION");
            capREGIONAL_ID_ISSUE_DATE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE_DATE");
            capACTIVITY.Text = objResourceMgr.GetString("cmbACTIVITY");
            capREG_ID_ISSUE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("NEW");
        }
        #endregion

        # region Function GetOldDataXML()
        private void GetOldDataXML()
        {

            if (hidDIV_ID.Value != "")
            {
                hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsDivision.GetDivisionXml(int.Parse(hidDIV_ID.Value), int.Parse(GetLanguageID()));
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
            revREGIONAL_ID_ISSUE_DATE.ValidationExpression = aRegExpDate;


        }
        #endregion

        #region Function GetQueryString()
        private void GetQueryString()
        {
            hidDIV_ID.Value = Request.Params["DIV_ID"];
        }
        #endregion


        private void FetchCountryState(string strXML)
        {
            string strSelectedState = ClsCommon.FetchValueFromXML("DIV_STATE", strXML);
            hidSTATE.Value = strSelectedState;
            string strSelectedCountry = ClsCommon.FetchValueFromXML("DIV_COUNTRY", strXML);
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {
                PopulateStateDropDown(int.Parse(strSelectedCountry));
            }
            else
                if (hidDIV_COUNTRY.Value != "")
                {
                    PopulateStateDropDown(Int32.Parse(hidDIV_COUNTRY.Value));
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

            cmbDIV_STATE.Items.Clear();
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbDIV_STATE.DataSource = dtStates;
                cmbDIV_STATE.DataTextField = STATE_NAME;
                cmbDIV_STATE.DataValueField = STATE_ID;
                cmbDIV_STATE.DataBind();
                cmbDIV_STATE.Items.Insert(0, "");
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            int intRetVal;
            int intDivID = int.Parse(hidDIV_ID.Value);
            int modifiedBy = int.Parse(GetUserId());
            Model.Maintenance.ClsDivisionInfo objDivisionInfo;
            objDivisionInfo = GetFormValue();
            string customInfo = "";
            customInfo = "; Division Name:" + objDivisionInfo.DIV_NAME + "<br>" + "; Division Code:" + objDivisionInfo.DIV_CODE;
            objDivision = new Cms.BusinessLayer.BlCommon.ClsDivision();
            intRetVal = objDivision.Delete(intDivID, customInfo, modifiedBy);
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
                lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "506");
                hidFormSaved.Value = "2";
            }
            lblDelete.Visible = true;
        }



        private void btnReset_Click(object sender, System.EventArgs e)
        {

        }

        private void Bindactivity()
        {
            DataTable dt = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
            cmbACTIVITY.DataSource = dt;
            cmbACTIVITY.DataTextField = "ACTIVITY_DESC";
            cmbACTIVITY.DataValueField = "ACTIVITY_ID";
            cmbACTIVITY.DataBind();
            cmbACTIVITY.Items.Insert(0, "");

        }//private void Bindactivity() 

    }
}
