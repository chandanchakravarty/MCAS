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
using System.Resources;
using Cms.Blcommon;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddClausesDetails : Cms.CmsWeb.cmsbase
    {
        #region Adding Web Controls

        protected Label lblDelete;
        protected Label lblMessage;
        protected Label capTYPE;
        protected Label capLOB_ID;
        protected Label capSUBLOB_ID;       
        protected Label capMAN_MSG;        
        protected DropDownList cmbTYPE;
        protected DropDownList cmbLOB_ID;
        protected DropDownList cmbSUBLOB_ID;

        protected TextBox txtFINAL_DATE;
        protected CmsButton btnReset;
        protected CmsButton btnActivateDeactivate;
        protected CmsButton btnDelete;
        protected CmsButton btnSave;
        protected HtmlInputHidden hidLOBXML;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLAUSE_TYPE;      
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPostedFile;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGeneral;
        protected string strUserName = "";
        protected string strPassWd = "";
        protected string strDomain = "";
        protected string strFileName = "";
        protected string strUploadedFileName = "";
        string oldXML;
        protected HtmlInputHidden hidATTACH_FILE_NAME;
        protected HtmlInputHidden hidRootPath;
        protected HtmlInputHidden hidfileLink;
        protected HtmlInputHidden hidOldData;
  
        ResourceManager objResourceMgr;

        #endregion 

        #region Variables
     
        ClsEndorsmentDetails objClsEndorsmentDetails = new ClsEndorsmentDetails();
        private string strRowId = "";
                
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id

            base.ScreenId = "493_0";
            #endregion

            #region setting security Xml
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            #endregion

            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddClausesDetails", System.Reflection.Assembly.GetExecutingAssembly());
            hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/ClausesAttachment/";
          
            SetErrorMessage();
           
            
            if (!Page.IsPostBack)
            {
                SetCaptions();
                PopulateProduct();
                PopulateClauseType();
                PopulateProcessType();

                hidLOBXML.Value = ClsCommon.GetXmlForLobWithoutState();
                btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");

                btnSave.Attributes.Add("onclick", "javascript:return setHidSubLob();RemoveSpecialChar(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE'));RemoveExecutableFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_EXT'));AllowEXTFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_PDF'));");

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "AddClausesDetails.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/AddClausesDetails.xml"); 

                
                if (Request.QueryString["CLAUSE_ID"] != null && Request.QueryString["CLAUSE_ID"].ToString() != "")
                {

                    hidCLAUSEID.Value = Request.QueryString["CLAUSE_ID"].ToString();

                    this.GetOldDataObject(Convert.ToInt32(Request.QueryString["CLAUSE_ID"].ToString()));
                    rfvATTACH_FILE_NAME.Enabled = false;

                }
                else if (Request.QueryString["CLAUSE_ID"] == null)
                {
                    btnActivateDeactivate.Visible = false;
                    btnDelete.Visible = false;
                    hidCLAUSEID.Value = "NEW";

                }
                strRowId = hidCLAUSEID.Value;
               
            }
        }


        # region Methods

        private void SetCaptions()
        {
            #region setcaption in resource file

            capSUBLOB_ID.Text = objResourceMgr.GetString("cmbSUBLOB_ID");
            capLOB_ID.Text = objResourceMgr.GetString("cmbLOB_ID");
            capCLAUSE_DESCRIPTION.Text = objResourceMgr.GetString("capCLAUSE_DESCRIPTION");
            capCLAUSE_TITLE.Text = objResourceMgr.GetString("capCLAUSE_TITLE");
            capATTACH_FILE_NAME.Text = objResourceMgr.GetString("capATTACH_FILE_NAME");
            capCLAUSE_TYPE.Text = objResourceMgr.GetString("capCLAUSE_TYPE");
            capPROCESS_TYPE.Text = objResourceMgr.GetString("capPROCESS_TYPE");
            capMAN_MSG.Text = objResourceMgr.GetString("capMAN_MSG");
            capCLAUSE_CODE.Text = objResourceMgr.GetString("capCLAUSE_CODE");
            #endregion

        }
        #endregion
        public void PopulateClauseType()
        {
            cmbCLAUSE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLATYP");
            cmbCLAUSE_TYPE.DataTextField = "LookupDesc";
            cmbCLAUSE_TYPE.DataValueField = "LookupID";
            cmbCLAUSE_TYPE.DataBind();
            cmbCLAUSE_TYPE.Items.Insert(0, "");
        }
        public void PopulateProcessType()
        {
            cmbPROCESS_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PROTYP");
            cmbPROCESS_TYPE.DataTextField = "LookupDesc";
            cmbPROCESS_TYPE.DataValueField = "LookupID";
            cmbPROCESS_TYPE.DataBind();
            cmbPROCESS_TYPE.Items.Insert(0, "");
            
        }
        public void PopulateProduct()
        {
            cmbLOB_ID.DataSource = Cms.CmsWeb.ClsFetcher.LOBs;
            cmbLOB_ID.DataTextField = "LOB_DESC";
            cmbLOB_ID.DataValueField = "LOB_ID";
            cmbLOB_ID.DataBind();
            cmbLOB_ID.Items.Insert(0, "");
        }
       
        /// <summary>
        /// Use to Get the Old Data Based on the ID
        /// </summary>
        /// <param name="MariTime_ID"></param>
        private void GetOldDataObject(Int32 CLAUSE_ID)
        {
            try
            {
                 
                ClsClausesInfo objClsClausesInfo =new ClsClausesInfo();
                objClsClausesInfo = objClsEndorsmentDetails.FetchData(CLAUSE_ID);
                DataSet dsAttachment = objClsClausesInfo.FetchData(CLAUSE_ID);
                hidOldData.Value = dsAttachment.GetXml();
                PopulateSubLoBS(objClsClausesInfo.LOB_ID.CurrentValue.ToString(), objClsClausesInfo.SUBLOB_ID.CurrentValue.ToString());
                PopulatePageFromEbixModelObject(this.Page, objClsClausesInfo);
                SetPageModelObject(objClsClausesInfo);

                txtareaCLAUSE_DESCRIPTION.Value = objClsClausesInfo.CLAUSE_DESCRIPTION.CurrentValue.ToString();
                hidOldSUB_LOB.Value = objClsClausesInfo.SUBLOB_ID.CurrentValue.ToString();
                cmbPROCESS_TYPE.SelectedValue = objClsClausesInfo.PROCESS_TYPE.CurrentValue.ToString();
                cmbCLAUSE_TYPE.SelectedValue = objClsClausesInfo.CLAUSE_TYPE.CurrentValue.ToString();
                txtCLAUSE_CODE.Text = objClsClausesInfo.CLAUSE_CODE.CurrentValue.ToString();
                if (ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME", hidOldData.Value) != null)
                {
                    string filename = hidRootPath.Value + ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME", hidOldData.Value);
                    int startOfFile = filename.IndexOf("Upload");
                    string filePath = filename.Substring(startOfFile + 6);
                    string[] fileURL = filePath.Split('.');
                    string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
                    hidfileLink.Value = EncryptedPath;
                    revATTACH_FILE_EXT.Enabled = false;
                    revATTACH_FILE_PDF.Enabled = false;
                }

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsClausesInfo.IS_ACTIVE.CurrentValue.ToString().Trim());

                //This will assign values to text boxes and combo boxes at the page
                // In case of drop down only write function of populate
               
                 
            }

            
                catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

                return;
            }
          
        }
        private void SetErrorMessage()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            rfvLOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("413");
            csvCLAUSE_DESCRIPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
           // rfvCLAUSE_DESCRIPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvCLAUSE_TITLE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvSUBLOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvCLAUSE_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            rfvPROCESS_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            rfvATTACH_FILE_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "50");
            hidGeneral.Value = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1190");
            revATTACH_FILE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1273");
            revATTACH_FILE_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1048");
            revATTACH_FILE_PDF.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1327");
            rfvCLAUSE_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1357");
            
        }
       
        /// <summary>
        /// User to Set the Form control (s)'s value Data in the Model info object
        /// </summary>
        /// <param name="objMariTimeInfo"></param>
        private void GetFormValue(ClsClausesInfo objClsClausesInfo)
        {

            if (txtareaCLAUSE_DESCRIPTION.Value.Trim() != "")
            { objClsClausesInfo.CLAUSE_DESCRIPTION.CurrentValue = txtareaCLAUSE_DESCRIPTION.Value; }


            if (txtCLAUSE_TITLE.Text.Trim() != "")
            { objClsClausesInfo.CLAUSE_TITLE.CurrentValue = txtCLAUSE_TITLE.Text; }

            if (cmbLOB_ID.SelectedItem != null)
            { objClsClausesInfo.LOB_ID.CurrentValue = Convert.ToInt32(cmbLOB_ID.SelectedValue); }
           
            if (hidSUB_LOB.Value == "")
            { objClsClausesInfo.SUBLOB_ID.CurrentValue = 0; }
            else{
             objClsClausesInfo.SUBLOB_ID.CurrentValue = Convert.ToInt32(hidSUB_LOB.Value);}
            if (cmbCLAUSE_TYPE.SelectedItem != null)
            {
                objClsClausesInfo.CLAUSE_TYPE.CurrentValue = Convert.ToInt32(cmbCLAUSE_TYPE.SelectedValue); 
            }
            if (cmbPROCESS_TYPE.SelectedItem != null)
            {
                objClsClausesInfo.PROCESS_TYPE.CurrentValue = Convert.ToInt32(cmbPROCESS_TYPE.SelectedValue);
            }
            if (txtCLAUSE_CODE.Text != "") {

                objClsClausesInfo.CLAUSE_CODE.CurrentValue = Convert.ToString(txtCLAUSE_CODE.Text);
            }

            if (cmbCLAUSE_TYPE.SelectedValue =="14696")
            {
                              
                oldXML = hidOldData.Value;

                //=========  File Upload ===========START

                strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

                //---- Begin
                //Below code gets the File name from Label or Text i.e; 
                //in either update or saved case.
                if (oldXML != "")
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(oldXML);

                    System.Xml.XmlNode xmlNode = xmlDoc.SelectSingleNode("/NewDataSet/Table/ATTACH_FILE_NAME");
                    if (xmlNode != null)
                        lblATTACH_FILE_NAME.Text = xmlNode.InnerText;
                }
                if (Request.Form["hidATTACH_FILE_NAME"].ToString().Equals("Y"))
                    strFileName = fileATTACH_FILE_NAME.PostedFile.FileName;
                else
                    strFileName = lblATTACH_FILE_NAME.Text;
                int intIndex = strFileName.LastIndexOf("\\");
                strFileName = strFileName.Substring(intIndex + 1);	//Taking only file name not whole path            
                if (strRowId.ToUpper().Equals("NEW"))
                objClsClausesInfo.ATTACH_FILE_NAME.CurrentValue = hidCLAUSEID.Value + "_" + strFileName;
                else
                    objClsClausesInfo.ATTACH_FILE_NAME.CurrentValue =  strFileName;
               
                //=========  File Upload ===========END

                


                
            }
           
        }

        #region UPLOAD FILE :: Add / Create Directory Structure / Save Uploaded File

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


                if (oldXML != "" && lblATTACH_FILE_NAME.Text!="")// itrack no 1482 by praveer panghal
                    strFileName1 = lblATTACH_FILE_NAME.Text;
                else
                    strFileName1 = objFile1.PostedFile.FileName.Substring(Index1 + 1);

                //if (Index1 >= 0)
                //{
                //    strFileName1 = objFile1.PostedFile.FileName.Substring(Index1 + 1);
                //}
                //else 
                //{
                //    strFileName1 = lblATTACH_FILE_NAME.Text;
                //}
                //else 
                //{
                //    strFileName1 = objFile1.PostedFile.FileName;
                //}
            

                //copying the files
                objFile1.PostedFile.SaveAs(strDirName + "\\" + hidCLAUSEID.Value + "_" + strFileName1);
                ClsClausesInfo ClauseInfo = new ClsClausesInfo();
                string Attach = (hidCLAUSEID.Value + "_" + strFileName1);
                string pol = hidCLAUSEID.Value;
                int retVal = ClauseInfo.UpdateAttachment(pol, Attach);
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
                strDirName = strDirName + "\\ClausesAttachment";
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
        #endregion


     #region "Control Events"

        protected void btnSave_Click(object sender, EventArgs e)
        {

            strRowId = hidCLAUSEID.Value;

            ClsClausesInfo objClsClausesInfo ;

            //For The Save New Record Case 
            try
            {
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objClsClausesInfo  = new ClsClausesInfo();
                    this.GetFormValue(objClsClausesInfo);
                    objClsClausesInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objClsClausesInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    objClsClausesInfo.IS_ACTIVE.CurrentValue = "Y"; 

                    int intRetval = objClsEndorsmentDetails.AddClauses(objClsClausesInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objClsClausesInfo.CLAUSE_ID.CurrentValue);
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                        hidFormSaved.Value = "1";
                        hidCLAUSEID.Value = objClsClausesInfo.CLAUSE_ID.CurrentValue.ToString();
                        rfvATTACH_FILE_NAME.Enabled = false;
                        if (cmbCLAUSE_TYPE.SelectedValue == "14696")
                        {

                            UploadFile();
                            if (ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME", hidOldData.Value) != null)
                            {
                                string filename = hidRootPath.Value  + ClsCommon.FetchValueFromXML("ATTACH_FILE_NAME", hidOldData.Value);
                                int startOfFile = filename.IndexOf("Upload");
                                string filePath = filename.Substring(startOfFile + 6);
                                string[] fileURL = filePath.Split('.');
                                string EncryptedPath = ClsCommon.CreateContentViewerURL(filePath, fileURL[1].ToUpper());
                                hidfileLink.Value = EncryptedPath;
                            }
                        }
                        this.GetOldDataObject(objClsClausesInfo.CLAUSE_ID.CurrentValue);
                    }
                        //Return -4 by insert procedure when record already exists
                    else if (intRetval == -4)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                        hidFormSaved.Value = "2";
                    }

                    else if (intRetval == -2)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1358");
                        hidFormSaved.Value = "2";
                        revATTACH_FILE_EXT.Enabled = false;
                        revATTACH_FILE_PDF.Enabled = false;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }

                else //For The Update case
                {


                    objClsClausesInfo = (ClsClausesInfo)base.GetPageModelObject();
                    this.GetFormValue(objClsClausesInfo);
                    objClsClausesInfo.IS_ACTIVE.CurrentValue = hidIS_ACTIVE.Value;  //Comment

                    objClsClausesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objClsClausesInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    int intRetval = objClsEndorsmentDetails.UpdateClauses(objClsClausesInfo);

                    if (intRetval > 0)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                       
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1");
                        hidFormSaved.Value = "1";
                        rfvATTACH_FILE_NAME.Enabled = false;
                        if (cmbCLAUSE_TYPE.SelectedValue == "14696" && lblATTACH_FILE_NAME.Text == "") // change by praveer for itrack no 1482
                        {

                            UploadFile();

                            
                        }// changes by praveer for itrack no 1482
                        this.GetOldDataObject(objClsClausesInfo.CLAUSE_ID.CurrentValue);

                    }
                    //Return -5 by update procedure when record already exists
                    //Customised message will display 
                    else if (intRetval == -5)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "6");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -2)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1358");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }
                btnActivateDeactivate.Visible = true;
                btnDelete.Visible = true;

                if (objClsClausesInfo.IS_ACTIVE.CurrentValue == "N")
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsClausesInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                }
                else
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsClausesInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
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
                //if (objNamedPerilsinfo != null)
                //    objNamedPerilsinfo.Dispose();
            }

        }

  

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsClausesInfo objClsClausesInfo;
            try
            {
                objClsClausesInfo = (ClsClausesInfo)base.GetPageModelObject();

                objClsClausesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
     
                int intRetval = objClsEndorsmentDetails.DeleteClauses(objClsClausesInfo);
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
                lblMessage.Visible = false;


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }


        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsClausesInfo objClsClausesInfo;

            try
            {
                objClsClausesInfo = (ClsClausesInfo)base.GetPageModelObject();

                if (objClsClausesInfo.IS_ACTIVE.CurrentValue == "Y")
                { objClsClausesInfo.IS_ACTIVE.CurrentValue = "N"; }
                else
                { objClsClausesInfo.IS_ACTIVE.CurrentValue = "Y"; }


                objClsClausesInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objClsClausesInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());


                int intRetval = objClsEndorsmentDetails.ActivateDeactivateClauses(objClsClausesInfo);
                if (intRetval > 0)
                {
                    if (objClsClausesInfo.IS_ACTIVE.CurrentValue == "N")
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsClausesInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objClsClausesInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    }
                    hidFormSaved.Value = "1";

                    SetPageModelObject(objClsClausesInfo);
                }
                lblDelete.Visible = false;
                lblMessage.Visible = true;


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }
        #endregion
       
        //This method populates all SUB LOBs based on  LOBs and returns 
        //Generic Type values of tables
        [System.Web.Services.WebMethod]
        public static System.Collections.Generic.Dictionary<string, object> GetSubLOBs(string Param)
        {
            try
            {
                System.Collections.Generic.Dictionary<string, object> dd = new System.Collections.Generic.Dictionary<string, object>();
              
                DataSet ds = new DataSet();
                ds = ClsEndorsmentDetails.GetSUBLOBs(Param,ClsCommon.BL_LANG_ID.ToString());
               return  Cms.CmsWeb.support.ClsjQueryCommon.ToJson(ds.Tables[0]);
     
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void PopulateSubLoBS(string LOB, String sublobid)
        {
            
            DataSet ds = new DataSet();
            ds = ClsEndorsmentDetails.GetSUBLOBs(LOB);
            cmbSUBLOB_ID.Items.Clear();
           
            cmbSUBLOB_ID.DataSource = ds;
            cmbSUBLOB_ID.DataTextField = "SUB_LOB_DESC";
            cmbSUBLOB_ID.DataValueField = "SUB_LOB_ID";
            cmbSUBLOB_ID.DataBind();
            cmbSUBLOB_ID.Items.Insert(0, new ListItem("","-1"));            
            cmbSUBLOB_ID.Items.Insert(1, new ListItem(hidGeneral.Value,"0"));

            cmbSUBLOB_ID.SelectedIndex = cmbSUBLOB_ID.Items.IndexOf(cmbSUBLOB_ID.Items.FindByValue(sublobid));

        }
        
    }
}
