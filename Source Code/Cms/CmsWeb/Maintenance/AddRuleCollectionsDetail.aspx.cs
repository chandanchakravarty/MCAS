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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.IO;
using Cms.CmsWeb.webservices;
using Cms.Blcommon;
using Cms.Model.Maintenance;
using Cms.EbixDataTypes;
using System.Globalization;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddRuleCollectionsDetail : Cms.CmsWeb.cmsbase
    {
        protected global::Cms.CmsWeb.Controls.CmsButton btnReset;
        protected global::Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected global::Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected HtmlInputHidden hidATTACH_FILE_NAME;
       // protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB_ID;
        protected HtmlInputHidden hidLOBXML;
        private string strRowId = "";
        public string systemid;
        string strUserName = "";
        string strPassWd = "";
        string strDomain = "";
        protected string strFileName = "";
        ClsRuleCollectioninfo objRuleCollectioninfo = new ClsRuleCollectioninfo();
        System.Resources.ResourceManager objresource;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "556_0";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
           
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;
            Ajax.Utility.RegisterTypeForAjax(typeof(AddRuleCollectionsDetail));                   
            hlkEFFECTIVE_FROM.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtEFFECTIVE_FROM'), document.getElementById('txtEFFECTIVE_FROM'))");
            hlkEFFECTIVE_TO.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtEFFECTIVE_TO'), document.getElementById('txtEFFECTIVE_TO'))");
            txtEFFECTIVE_FROM.Attributes.Add("onBlur", "FormatDate()");
            txtEFFECTIVE_TO.Attributes.Add("onBlur", "FormatDate()");
            objresource = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddRuleCollectionsDetail", System.Reflection.Assembly.GetExecutingAssembly());
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");

            if (!Page.IsPostBack)
                {
                SetCaption();
                BindProducts();                
                BindValidationOrder();
                LoadDropDowns();
                SetErrorMessage();

                hidLOBXML.Value = ClsCommon.GetXmlForLobWithoutState();
                if (Request.QueryString["RULE_COLLECTION_ID"] != null && Request.QueryString["RULE_COLLECTION_ID"].ToString() != "")
                {

                    hidRuleCollectionID.Value = Request.QueryString["RULE_COLLECTION_ID"].ToString();
                    this.GetOldDataObject();
                                        

                }
                else if (Request.QueryString["RULE_COLLECTION_ID"] == null)
                {
                    hidRuleCollectionID.Value = "NEW";
                   
                 }
                strRowId = hidRuleCollectionID.Value;


            }

        }

        private void GetOldDataObject()
        {
             try
            {
            systemid = getCarrierSystemID();
            ClsRatingAndUnderwritingRules objRuleCollection = new ClsRatingAndUnderwritingRules(systemid);
            ClsRuleCollectioninfo objRuleCollectioninfo = new ClsRuleCollectioninfo();
            rfvSUB_LOB_ID.Style.Add("display", "none");
            fileATTACH_FILE_NAME.Disabled = true;
            objRuleCollectioninfo = objRuleCollection.FetchData(int.Parse(hidRuleCollectionID.Value));           
            PopulatePageFromEbixModelObject(this.Page, objRuleCollectioninfo);


            if (objRuleCollectioninfo.COUNTRY_ID.CurrentValue.ToString() != "" && objRuleCollectioninfo.COUNTRY_ID.CurrentValue.ToString() != null)
            {
                string country_id = objRuleCollectioninfo.COUNTRY_ID.CurrentValue.ToString();
                cmbCOUNTRY_ID.SelectedValue = country_id;
                FillState(int.Parse(cmbCOUNTRY_ID.SelectedValue), ref cmbSTATE_ID);
            }

            if (objRuleCollectioninfo.STATE_ID.CurrentValue.ToString() != "" && objRuleCollectioninfo.STATE_ID.CurrentValue.ToString() != null)
            {
                string state_id = objRuleCollectioninfo.STATE_ID.CurrentValue.ToString();
                cmbSTATE_ID.SelectedValue = state_id;                
            }

          
            if (objRuleCollectioninfo.LOB_ID.CurrentValue.ToString() != "" && objRuleCollectioninfo.LOB_ID.CurrentValue.ToString() != null)
            {
                string Lob_id = objRuleCollectioninfo.LOB_ID.CurrentValue.ToString();           
                PopulateSubLoBS(Lob_id);
               
            }
            if (objRuleCollectioninfo.SUB_LOB_ID.CurrentValue.ToString() != "" && objRuleCollectioninfo.SUB_LOB_ID.CurrentValue.ToString() != null)
            {
                string Sub_lob_id = objRuleCollectioninfo.SUB_LOB_ID.CurrentValue.ToString();
                cmbSUB_LOB_ID.SelectedValue = Sub_lob_id;

            }

            if (objRuleCollectioninfo.VALIDATION_ORDER.CurrentValue.ToString() != "" && objRuleCollectioninfo.VALIDATION_ORDER.CurrentValue.ToString() != null)
            {
                double Validation_Order = Convert.ToDouble(objRuleCollectioninfo.VALIDATION_ORDER.CurrentValue);
                cmbVALIDATION_ORDER.SelectedValue = Validation_Order.ToString();
            }

            if (objRuleCollectioninfo.VALIDATE_NEXT_IF_FAILED.CurrentValue == "False")
            {
                chkVALIDATE_NEXT_IF_FAILED.Checked = true;
            }
            else
                chkVALIDATE_NEXT_IF_FAILED.Checked = false;

            base.SetPageModelObject(objRuleCollectioninfo);
                         
            }
             catch (Exception ex)
             {
                 lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                 lblMessage.Visible = true;
                 Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
             }

           }


        private void SetErrorMessage()
        {
            rfvEFFECTIVE_FROM.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
            rfvEFFECTIVE_TO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            revEFFECTIVE_FROM.ValidationExpression = aRegExpDate;
            revEFFECTIVE_FROM.ErrorMessage = ClsMessages.FetchGeneralMessage("2077");
            revEFFECTIVE_TO.ValidationExpression = aRegExpDate;
            revEFFECTIVE_TO.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            cpvEFFECTIVE_TO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("447");
            rfvLOB_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            rfvSUB_LOB_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");
            rfvATTACH_FILE_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "50");
            
        }

        #region Set Caption

        private void SetCaption()
        {

            capEFFECTIVE_FROM.Text = objresource.GetString("capEFFECTIVE_FROM");
            capEFFECTIVE_TO.Text = objresource.GetString("capEFFECTIVE_TO");
            capCOUNTRY_ID.Text = objresource.GetString("capCOUNTRY_ID");
            capSTATE_ID.Text = objresource.GetString("capSTATE_ID");
            capLOB_ID.Text = objresource.GetString("capLOB_ID");
            capSUB_LOB_ID.Text = objresource.GetString("capSUB_LOB_ID");
            capVALIDATION_ORDER.Text = objresource.GetString("capVALIDATION_ORDER");
            capVALIDATE_NEXT_IF_FAILED.Text = objresource.GetString("capVALIDATE_NEXT_IF_FAILED");
            capATTACH_FILE_NAME.Text = objresource.GetString("capATTACH_FILE_NAME");

        }
        #endregion



        private void BindProducts()
        {
            DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
            cmbLOB_ID.DataSource = dtLOBs;
            cmbLOB_ID.DataTextField = "LOB_DESC";
            cmbLOB_ID.DataValueField = "LOB_ID";
            cmbLOB_ID.DataBind();
            cmbLOB_ID.Items.Insert(0, new ListItem("", ""));
        }

        private void BindValidationOrder()
        {
            cmbVALIDATION_ORDER.Items.Add("");
            cmbVALIDATION_ORDER.Items.Add("1");
            cmbVALIDATION_ORDER.Items.Add("2");
            cmbVALIDATION_ORDER.Items.Add("3");
            cmbVALIDATION_ORDER.Items.Add("4");
            cmbVALIDATION_ORDER.Items.Add("5");
            cmbVALIDATION_ORDER.Items.Add("6");
            cmbVALIDATION_ORDER.Items.Add("7");
            cmbVALIDATION_ORDER.Items.Add("8");
            cmbVALIDATION_ORDER.Items.Add("9");
            cmbVALIDATION_ORDER.Items.Add("10");
        }

        protected void UploadFile()
        {
            //=================== File Upload ====================
            //Beginigng the impersonation 
            Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
            if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
            {
                if (!SaveUploadedFile(fileATTACH_FILE_NAME))
                {
                    //Some error occured while uploading 
                    lblMessage.Text += ClsMessages.GetMessage(base.ScreenId, "9"); ;
                }

                //ending the impersonation 
                objAttachment.endImpersonation();
            }
            else
            {
                //Impersation failed
                lblMessage.Text += ClsMessages.GetMessage(base.ScreenId, "10"); ;
            }
            //=================== File Upload ====================
        }
        private bool SaveUploadedFile(HtmlInputFile objFile1)
        {
            try
            {
                //Stores the name of the directory where file will get stored
                string strDirName;
                strDirName = CreateDirStructure();


                //Retreiving the extension -- FILE 1
                string strFileName1;
                int Index1 = objFile1.PostedFile.FileName.LastIndexOf("\\");


               // if (oldXML != "" && lblATTACH_FILE_NAME.Text != "")// 
                    strFileName1 = lblATTACH_FILE_NAME.Text;
                //else
                    strFileName1 = objFile1.PostedFile.FileName.Substring(Index1 + 1);

                //copying the files
                objFile1.PostedFile.SaveAs(strDirName + "\\" + hidRuleCollectionID.Value + "_" + strFileName1);
                string Attach = (hidRuleCollectionID.Value + "_" + strFileName1);
                string pol = hidRuleCollectionID.Value;
                return true;
            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                return false;
            }
        }


        private string CreateDirStructure()
        {

            string strRoot, strDirName = "";
            try
            {
                strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL");
                strDirName = Server.MapPath(strRoot);
                //Creating the Attachment folder if not exists
                strDirName = strDirName + "\\RuleCollection";
                if (!System.IO.Directory.Exists(strDirName))
                {
                    //Creating the directory
                    System.IO.Directory.CreateDirectory(strDirName);
                }
            }
            catch (Exception objEx)
            {
                throw (objEx);
            }
            return strDirName;
        }





        private void LoadDropDowns()
        {
            DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
            cmbCOUNTRY_ID.DataSource = dt;
            cmbCOUNTRY_ID.DataTextField = "Country_Name";
            cmbCOUNTRY_ID.DataValueField = "Country_Id";
            cmbCOUNTRY_ID.DataBind();
            cmbCOUNTRY_ID.Items.Insert(0, new ListItem("todos", "0"));
            if (cmbCOUNTRY_ID.SelectedValue != "")
            {
                Cms.BusinessLayer.BlCommon.ClsStates objStates = new Cms.BusinessLayer.BlCommon.ClsStates();
                DataSet ds = objStates.GetStatesCountry(int.Parse(cmbCOUNTRY_ID.SelectedValue));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cmbSTATE_ID.DataSource = ds;
                    cmbSTATE_ID.DataTextField = STATE_NAME;
                    cmbSTATE_ID.DataValueField = STATE_ID;
                    cmbSTATE_ID.DataBind();
                    cmbSTATE_ID.Items.Insert(0, new ListItem("todos", "0"));                   
                }
            }
            
        }

        
        private void getFormValues(ClsRuleCollectioninfo objRuleCollectioninfo)
        {


            if (txtEFFECTIVE_FROM.Text.Trim() != "")
            { objRuleCollectioninfo.EFFECTIVE_FROM.CurrentValue = ConvertToDate(txtEFFECTIVE_FROM.Text); }

            if (txtEFFECTIVE_TO.Text.Trim() != "")
            { objRuleCollectioninfo.EFFECTIVE_TO.CurrentValue = ConvertToDate(txtEFFECTIVE_TO.Text); }

            if (cmbCOUNTRY_ID.SelectedValue != "")
            {                
                objRuleCollectioninfo.COUNTRY_ID.CurrentValue = Convert.ToInt32(cmbCOUNTRY_ID.SelectedValue);
            }

            if (hidSTATE_ID.Value != null && hidSTATE_ID.Value != "")
                FillState(int.Parse(cmbCOUNTRY_ID.SelectedValue), ref cmbSTATE_ID);
                objRuleCollectioninfo.STATE_ID.CurrentValue = hidSTATE_ID.Value;

         
            if (cmbLOB_ID.SelectedItem != null)
            { objRuleCollectioninfo.LOB_ID.CurrentValue = Convert.ToInt32(cmbLOB_ID.SelectedValue);
            string Lob_id = objRuleCollectioninfo.LOB_ID.CurrentValue.ToString();      
                PopulateSubLoBS(Lob_id);
             }


            if (cmbSUB_LOB_ID.SelectedItem != null)
            {
                objRuleCollectioninfo.SUB_LOB_ID.CurrentValue = Convert.ToInt32(cmbSUB_LOB_ID.SelectedValue);
            }

              else
            {
                if (!string.IsNullOrEmpty(hidSUB_LOB_ID.Value) && hidSUB_LOB_ID.Value != "0")
                    objRuleCollectioninfo.SUB_LOB_ID.CurrentValue = Convert.ToInt32(hidSUB_LOB_ID.Value);
            }


            if (!string.IsNullOrEmpty(cmbVALIDATION_ORDER.SelectedValue))
            { objRuleCollectioninfo.VALIDATION_ORDER.CurrentValue = Convert.ToDouble(cmbVALIDATION_ORDER.SelectedValue); }           



            if (chkVALIDATE_NEXT_IF_FAILED.Checked == true)
            {
                objRuleCollectioninfo.VALIDATE_NEXT_IF_FAILED.CurrentValue = "0";
                chkVALIDATE_NEXT_IF_FAILED.Checked = true;
            }
            else
            {
                objRuleCollectioninfo.VALIDATE_NEXT_IF_FAILED.CurrentValue = "1";
                chkVALIDATE_NEXT_IF_FAILED.Checked = false;
            }

            if (objRuleCollectioninfo.VALIDATE_NEXT_IF_FAILED.CurrentValue == "0")
            {
                chkVALIDATE_NEXT_IF_FAILED.Checked = true;
            }
            else
                chkVALIDATE_NEXT_IF_FAILED.Checked = false;


            strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
            strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
            strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

            if (Request.Form["hidATTACH_FILE_NAME"].ToString().Equals("Y"))
                strFileName = fileATTACH_FILE_NAME.PostedFile.FileName;
            else
                strFileName = lblATTACH_FILE_NAME.Text;
            int intIndex = strFileName.LastIndexOf("\\");
            strFileName = strFileName.Substring(intIndex + 1);	//Taking only file name not whole path            
            if (strRowId.ToUpper().Equals("NEW"))
                objRuleCollectioninfo.RULE_XML_PATH.CurrentValue = hidATTACH_FILE_NAME.Value + "_" + strFileName;
            else
                objRuleCollectioninfo.RULE_XML_PATH.CurrentValue = strFileName;

            //=========  File Upload ===========END


        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            //  this.btnResetSeries.Click += new System.EventHandler(this.btnResetSeries_Click);
          //  this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
          //  this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //  this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int intRetval;


            ClsRuleCollectioninfo objRuleCollectioninfo;
            ClsRatingAndUnderwritingRules objRuleCollection = new ClsRatingAndUnderwritingRules(systemid);
            try
            {
                //For new item to add
                strRowId = hidRuleCollectionID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objRuleCollectioninfo = new ClsRuleCollectioninfo();
                    this.getFormValues(objRuleCollectioninfo);

                    objRuleCollectioninfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objRuleCollectioninfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objRuleCollectioninfo.IS_ACTIVE.CurrentValue = "1"; //hidIS_ACTIVE.Value;
                    intRetval = objRuleCollection.AddRuleCollectionInformation(objRuleCollectioninfo);
                    hidRuleCollectionID.Value = objRuleCollectioninfo.RULE_COLLECTION_ID.CurrentValue.ToString();


                    if (intRetval > 0)
                    {
                        UploadFile();
                        hidRuleCollectionID.Value = objRuleCollectioninfo.RULE_COLLECTION_ID.CurrentValue.ToString();

                        this.GetOldDataObject();

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";


                    }
                    else if (intRetval == -1)
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
                //For The Update case
                else
                {

                    objRuleCollectioninfo = (ClsRuleCollectioninfo)base.GetPageModelObject();
                    this.getFormValues(objRuleCollectioninfo);

                    objRuleCollectioninfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objRuleCollectioninfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objRuleCollection.UpdateRuleCollectionInformation(objRuleCollectioninfo);



                    if (intRetval > 0)
                    {
                        UploadFile();
                        hidRuleCollectionID.Value = objRuleCollectioninfo.RULE_COLLECTION_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        rfvSUB_LOB_ID.Style.Add("display", "none");
                        hidFormSaved.Value = "1";


                    }
                    else if (intRetval == -1)
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
                
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsRuleCollectioninfo objRuleCollectioninfo;
            ClsRatingAndUnderwritingRules objRuleCollection = new ClsRatingAndUnderwritingRules(systemid);

            int intRetval;

            try
            {
                objRuleCollectioninfo = (ClsRuleCollectioninfo)base.GetPageModelObject();

                objRuleCollectioninfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                intRetval = objRuleCollection.DeleteRuleCollection(objRuleCollectioninfo);

                if (intRetval > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    trBody.Attributes.Add("style", "display:none");
                    
                }
                else if (intRetval == -1)
                {
                    lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                    hidFormSaved.Value = "2";
                }
                  lblDelete.Visible = true;

            }


            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
     

      

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFillState(string CountryID)
        {
            try
            {
                Cms.CmsWeb.webservices.ClsWebServiceCommon obj = new Cms.CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.FillState(CountryID);
                DataSet ds = new DataSet();
                //cmbSTATE_ID.Items.Insert(0, new ListItem("todos", "0"));      
                ds.ReadXml(new System.IO.StringReader(result));

                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }

     


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public DataSet AjaxFillSubLob(string LobId)
        {
            try
            {

                DataSet ds = new DataSet();
                ds = (Cms.BusinessLayer.BlCommon.ClsEndorsmentDetails.GetSUBLOBs(LobId, ClsCommon.BL_LANG_ID.ToString()));               
               
                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }   //fill FillSubLob from database onchange Lob



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
                cmbState.Items.Insert(0, new ListItem("todos", "0"));       
            }

        }

        public void PopulateSubLoBS(string LOB)
        {

            DataSet ds = new DataSet();
            ds = ClsEndorsmentDetails.GetSUBLOBs(LOB);       
            cmbSUB_LOB_ID.DataSource = ds;
            cmbSUB_LOB_ID.DataTextField = "SUB_LOB_DESC";
            cmbSUB_LOB_ID.DataValueField = "SUB_LOB_ID";
            cmbSUB_LOB_ID.DataBind();
            //cmbSUB_LOB_ID.Items.Insert(0, "");      

        }

    }
}