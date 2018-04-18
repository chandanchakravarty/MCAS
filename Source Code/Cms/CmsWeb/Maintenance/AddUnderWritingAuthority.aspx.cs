/******************************************************************************************
<Author					: -		Amit kr Mishra
<Start Date				: -		01/11/2011
<End Date				: -	    01/11/2011
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - This is use to maintain the underwriter authority claim limit for a underwriter type of user.
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
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms.Blcommon;
using Cms.BusinessLayer.BlCommon;


namespace CmsWeb.Maintenance
{
    public partial class AddUnderWritingAuthority : Cms.CmsWeb.cmsbase
    {
        #region Page controls declaration
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;

        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidExtraCover;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.Label lblMessage;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidASSIGN_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_CODE;
        protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
        protected System.Web.UI.HtmlControls.HtmlTableRow trTcode;

        protected System.Web.UI.WebControls.Label capMessages;

        #endregion

        #region Local form variables
        //creating resource manager object (used for reading field and label mapping)
        //System.Resources.ResourceManager objResourceMgr;
        //Defining the business layer class object
       

        private string strRowId = "";
        private string strCalledFrom = "";
        private string XmlSchemaFileName = ""; //Added by Amit k mishra for Singapore Implementation
        private string XmlFullFilePath = ""; //Added by Amit k mishra for Singapore Implementation
         ClsUnderWriterClaimsLimit objUnderWriterClaimsLimit;
        protected System.Web.UI.WebControls.Label capCOUNTRY_CODE;
        protected System.Web.UI.WebControls.Label capLOB_ID;
        protected System.Web.UI.WebControls.Label capPML_LIMIT;
       
        protected System.Web.UI.WebControls.TextBox txtPML_LIMIT;
        protected System.Web.UI.WebControls.TextBox txtPREMIUM_APPROVAL_LIMIT;
        protected System.Web.UI.WebControls.TextBox txtCLAIM_RESERVE_LIMIT;
        protected System.Web.UI.WebControls.TextBox txtCLAIM_SETTLMENT_LIMIT;

        
     
        protected System.Web.UI.WebControls.DropDownList cmbCOUNTRTY_CODE;
        protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
        protected System.Web.UI.WebControls.DropDownList cmbCLAIM_REOPEN;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRTY_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAIM_RESERVE_LIMIT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAIM_REOPEN;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAIM_SETTLMENT_LIMIT;

    
        #endregion
        #region Set Validators ErrorMessages
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>
        private void SetErrorMessages()
        {
            rfvCOUNTRTY_CODE.ErrorMessage = "Please Select Country";
            rfvCLAIM_RESERVE_LIMIT.ErrorMessage = "Please Enter Claim Reserve Limit";
            rfvLOB_ID.ErrorMessage = "Please Select Product";
            rfvCLAIM_SETTLMENT_LIMIT.ErrorMessage = "Please enter Claim Settlement Limit";

            rfvEffectiveDate.ErrorMessage = "Please select Effective Date";
            rfvEndDate.ErrorMessage = "Please select End Date";
            cpvEndDate.ErrorMessage = "End Date should be greater than the Effective Date";
        }
        #endregion

        #region AJAX Methods
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchLobByCountryId(int iCOUNTRY_ID)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                DataTable dt1 = new DataTable();
                try
                {
                    ClsUnderWriterClaimsLimit objUnderWriterClaimsLimit = new ClsUnderWriterClaimsLimit();
                    dt1 = objUnderWriterClaimsLimit.GetLobForCountry(iCOUNTRY_ID);
                    
                }
                catch
                { }
                ds.Tables.Add(dt1.Copy());
                ds.Tables[0].TableName = "LOB_MASTER";

                return ds;
            }
            catch
            {
                return null;
            }
            finally 
            {
            
            }
        }

        #endregion

        #region PageLoad event


        private void Page_Load(object sender, System.EventArgs e)
        {
                
            Ajax.Utility.RegisterTypeForAjax(typeof(AddUnderWritingAuthority));

            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            //#region Setting Screen ID
            
            base.ScreenId = "25_3";

            //strCalledFrom = Request.Params["CalledFrom"].ToString();
            ////Added Till here

            //#endregion

            lblMessage.Visible = false;
            //SetErrorMessages();

            ////START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write  ;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;  //Permission made Write instead of Execute by Sibin on 23 Oct 08
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
            btnActivateDeactivate.Visible = true;

            //btnViewHistory.CmsButtonClass = CmsButtonType.Write;
            //btnViewHistory.PermissionString = gstrSecurityXML;
            //btnViewHistory.Visible = true;

            string strSysID = GetSystemId();
            XmlSchemaFileName = "AddUnderWritingAuthority.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName;                                

            if (!IsPostBack)
            {
                hlkEffectiveDate.Attributes.Add("onClick", "fPopCalendar(document.MNT_UNDERWRITER_AUTHORITY.txtEffectiveDate,document.MNT_UNDERWRITER_AUTHORITY.txtEffectiveDate)");
                hlkEndDate.Attributes.Add("OnClick", "fPopCalendar(document.MNT_UNDERWRITER_AUTHORITY.txtEndDate,document.MNT_UNDERWRITER_AUTHORITY.txtEndDate)");


                //txtEffectiveDate.Text = ConvertToDate(txtEffectiveDate.Text);
                SetErrorMessages();
                if (Request.QueryString["ASSIGN_ID"] != null && Request.QueryString["ASSIGN_ID"].ToString().Length > 0)
                    hidASSIGN_ID.Value = Request.QueryString["ASSIGN_ID"].ToString();

                if(Request.QueryString["UserId"]!= null && Request.QueryString["UserId"].ToString().Length > 0)
                    hidUSER_ID.Value = Request.QueryString["UserId"].ToString();
                //btnSave.Attributes.Add("onClick","return SelectItem();");

                if (Request.QueryString["ASSIGN_ID"] != null && Request.QueryString["ASSIGN_ID"].ToString().Length > 0)
                {
                    hidASSIGN_ID.Value = Request.QueryString["ASSIGN_ID"].ToString();
                    hidOldData.Value = ClsUnderWriterClaimsLimit.GetXmlForPageControls(hidASSIGN_ID.Value);
                }                

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath, "cmsweb/support/PageXml/" + strSysID + "/" + XmlSchemaFileName))
                {
                    //setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() +"/"+ XmlSchemaFileName);
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/S001/" + XmlSchemaFileName);
                }
                bindDropDown();

                if (hidOldData.Value != "")
                {
                    txtEffectiveDate.ReadOnly = true;
                    rfvEffectiveDate.Enabled = false;
                    txtEndDate.ReadOnly = true;
                    rfvEndDate.Enabled = false;
                }
            }

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            //objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddDefaultValueClaims" ,System.Reflection.Assembly.GetExecutingAssembly());
                             

            }

        //end pageload
        #endregion 

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsUnderwritingAuthorityLimitsInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsUnderwritingAuthorityLimitsInfo objUnderwritingAuthorityLimitsInfo = new ClsUnderwritingAuthorityLimitsInfo();
            objUnderwritingAuthorityLimitsInfo.COUNTRY_ID = int.Parse(cmbCOUNTRTY_CODE.SelectedValue);
            if(cmbCLAIM_REOPEN.SelectedIndex!=-1)
            objUnderwritingAuthorityLimitsInfo.CLAIM_REOPEN= cmbCLAIM_REOPEN.SelectedValue;
            objUnderwritingAuthorityLimitsInfo.LOB_ID = int.Parse(cmbLOB_ID.SelectedValue);
           
            if(txtCLAIM_RESERVE_LIMIT.Text!="")
            objUnderwritingAuthorityLimitsInfo.CLAIM_RESERVE_LIMIT = decimal.Parse(txtCLAIM_RESERVE_LIMIT.Text);

            if (txtCLAIM_SETTLMENT_LIMIT.Text != "") 
            objUnderwritingAuthorityLimitsInfo.CLAIM_SETTLMENT_LIMIT = decimal.Parse(txtCLAIM_SETTLMENT_LIMIT.Text);

            if (txtPML_LIMIT.Text != "")
            objUnderwritingAuthorityLimitsInfo.PML_LIMIT = decimal.Parse(txtPML_LIMIT.Text);
            
            if(txtPREMIUM_APPROVAL_LIMIT.Text!="")
            objUnderwritingAuthorityLimitsInfo.PREMIUM_APPROVAL_LIMIT = decimal.Parse(txtPREMIUM_APPROVAL_LIMIT.Text);

            objUnderwritingAuthorityLimitsInfo.USER_ID = int.Parse(hidUSER_ID.Value);


            if (hidASSIGN_ID.Value.ToUpper() != "NEW")
                objUnderwritingAuthorityLimitsInfo.ASSIGN_ID = int.Parse(hidASSIGN_ID.Value);

            strRowId = hidASSIGN_ID.Value;

            //Effective and End Dates added by Ruchika Chauhan on 3-Feb-2012 for TFS # 3322
            if (txtEffectiveDate.Text.Trim() != "")
                objUnderwritingAuthorityLimitsInfo.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text.Trim());

            if (txtEndDate.Text.Trim() != "")
                objUnderwritingAuthorityLimitsInfo.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
          

            return objUnderwritingAuthorityLimitsInfo;
        }
        #endregion

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            int intRetVal;	//For retreiving the return value of business class save function
            objUnderWriterClaimsLimit = new ClsUnderWriterClaimsLimit();
            ClsUnderwritingAuthorityLimitsInfo obj = GetFormValue();
            try
            {

                if (strRowId.ToUpper().Equals("NEW"))
                {
                    obj.IS_ACTIVE = "Y";
                    obj.CREATED_BY = int.Parse(GetUserId());
                    obj.CREATED_DATETIME = DateTime.Now;
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                    //Calling the add method of business layer class
                    intRetVal = objUnderWriterClaimsLimit.Add(obj, XmlFullFilePath);
                    if (intRetVal > 0)
                    {
                        hidASSIGN_ID.Value = obj.ASSIGN_ID.ToString();

                        this.GetOldDataXML();

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;

                } 
                else //UPDATE CASE
                {

                        //Creating the Model object for holding the Old data
                    ClsUnderwritingAuthorityLimitsInfo objOldInfo = new ClsUnderwritingAuthorityLimitsInfo();
                    this.GetOldDataXML();
                        //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldInfo, hidOldData.Value);

                        //Setting those values into the Model object which are not in the page
                        obj.ASSIGN_ID= int.Parse(strRowId);
                        obj.MODIFIED_BY = int.Parse(GetUserId());
                        obj.LAST_UPDATED_DATETIME = DateTime.Now;
                        //Updating the record using business layer class object
                        intRetVal = objUnderWriterClaimsLimit.Update(objOldInfo, obj, XmlFullFilePath);
                        if (intRetVal > 0)			// update successfully performed
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                            hidFormSaved.Value = "1";
                            hidOldData.Value = ClsUnderWriterClaimsLimit.GetXmlForPageControls(hidASSIGN_ID.Value);
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
            }
            finally
            {
                if (objUnderWriterClaimsLimit != null)
                    objUnderWriterClaimsLimit.Dispose();
            }
        
        
        }
      
        #region GetOldDataXML
        private void GetOldDataXML()
        {
            if (hidASSIGN_ID.Value != "")
            {
                hidOldData.Value =ClsUnderWriterClaimsLimit.GetXmlForPageControls(hidASSIGN_ID.Value);
            }
            else
                hidOldData.Value = "";

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

           this.Load += new System.EventHandler(this.Page_Load);
           this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }
        #endregion

        private void bindDropDown()
        {
            cmbCLAIM_REOPEN.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbCLAIM_REOPEN.DataTextField = "LookupDesc";
            cmbCLAIM_REOPEN.DataValueField = "LookupID";
            cmbCLAIM_REOPEN.DataBind();

            DataTable dtCountry = Cms.CmsWeb.ClsFetcher.AllCountry;
            cmbCOUNTRTY_CODE.DataSource = dtCountry;
            cmbCOUNTRTY_CODE.DataTextField = COUNTRY_NAME;
            cmbCOUNTRTY_CODE.DataValueField = COUNTRY_ID;
            cmbCOUNTRTY_CODE.DataBind();
            cmbCOUNTRTY_CODE.Items.FindByText("Singapore").Selected = true;

            DataSet ds = new DataSet();
            ds = AjaxFetchLobByCountryId(int.Parse(cmbCOUNTRTY_CODE.SelectedValue));
            cmbLOB_ID.DataSource = ds;
            cmbLOB_ID.DataTextField = "LOB_DESC";
            cmbLOB_ID.DataValueField = "LOB_ID";
            cmbLOB_ID.DataBind();


        
        }
    }
}
