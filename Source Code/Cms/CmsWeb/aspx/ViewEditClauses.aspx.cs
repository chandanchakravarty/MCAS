/******************************************************************************************
<Author				    : -   Charles Gomes
<Start Date				: -	  16-Apr-2010
<End Date				: -	
<Description			: -   View Edit Clauses
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using System.Resources;
using System.Reflection;
using Cms.Model.Policy;

namespace CmsWeb.aspx
{
    public partial class ViewEditClauses : Cms.CmsWeb.cmsbase
    {
        private ResourceManager objResourceManager = null;
        private ClsPolicyClauseInfo objPolicyClauseInfo = new ClsPolicyClauseInfo();
        private DataTable dtTemp = null;
        protected string strUserName = "";
        protected string strPassWd = "";
        protected string strDomain = "";
        protected string strFileName = "";
        protected string strUploadedFileName = "";  
        protected string PolicyStatus;
      
        string oldXML;

        ClsEndorsmentDetails objClsEndorsmentDetails = new ClsEndorsmentDetails();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "";

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            objResourceManager = new ResourceManager("CmsWeb.Aspx.ViewEditClauses", Assembly.GetExecutingAssembly());
            hidRootPath.Value = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL") + @"/ClausesAttachment/";
            lblMessage.Visible = false;
            SetErrorMessages();
            btnSave.Attributes.Add("onclick", "javascript:RemoveSpecialChar(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE'));RemoveExecutableFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_EXT'));AllowEXTFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_PDF'));");
           PolicyStatus = GetPolicyStatus();
          // hidpolicystatus.Value = GetPolicyStatus();
            if (!IsPostBack)
            {
                hidTransactLabel.Value = ClsCommon.MapTransactionLabel("CmsWeb/aspx/ViewEditClauses.aspx.resx");

                SetCaptions();
                SetErrorMessages();
                PopulateClauseType();
                FetchQueryParams();
                FetchData();
               
                

            }
            objPolicyClauseInfo.TransactLabel = hidTransactLabel.Value;
            
        }
        public void PopulateClauseType()
        {
            cmbCLAUSE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLATYP");
            cmbCLAUSE_TYPE.DataTextField = "LookupDesc";
            cmbCLAUSE_TYPE.DataValueField = "LookupID";
            cmbCLAUSE_TYPE.DataBind();
            cmbCLAUSE_TYPE.Items.Insert(0, "");
        }
        private void SetErrorMessages()
        {
            rfvCLAUSE_TITLE.ErrorMessage = ClsMessages.GetMessage("224_28", "16");
            //rfvCLAUSE_DESCRIPTION.ErrorMessage = ClsMessages.GetMessage("224_28", "17");
            csvCLAUSE_DESCRIPTION.ErrorMessage = ClsMessages.GetMessage("224_28", "17");
            rfvCLAUSE_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1272");
            rfvATTACH_FILE_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1271");
            //revATTACH_FILE_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1047");
            //revATTACH_FILE_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1048");
            revATTACH_FILE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1273");
            revATTACH_FILE_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1048");
            revATTACH_FILE_PDF.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1327");
            rfvCLAUSE_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1357");
        }

        private void FetchQueryParams()
        {   
            //DEFIND_ID=1 Means user comes from system clauses
            //DEFIND_ID=2 Means user comes from user clauses
            if (Request.QueryString["DEFIND_ID"] != null && Request.QueryString["DEFIND_ID"].ToString().Trim() != "")
            {
                hidDEFIND_ID.Value = Request.QueryString["DEFIND_ID"].ToString().Trim();                
            }

            if (Request.QueryString["POL_CLAUSE_ID"] != null && Request.QueryString["POL_CLAUSE_ID"].ToString().Trim() != "" && Request.QueryString["POL_CLAUSE_ID"].ToString().Trim() != "0")
            {
                hidPOL_CLAUSE_ID.Value = Request.QueryString["POL_CLAUSE_ID"].ToString().Trim();

                // get clause id as POL_CLAUSE_ID  from policyclause page and assign in  hidCLAUSE_ID1 
                hidCLAUSE_ID1.Value = Request.QueryString["POL_CLAUSE_ID"].ToString().Trim();
            }
            //else
            //{
            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString().Trim() != "")
            {
                hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString().Trim();
            }

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString().Trim() != "")
            {
                hidPolicyID.Value = Request.QueryString["POLICY_ID"].ToString().Trim();
            }

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString().Trim() != "")
            {
                hidPolicyVersionID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString().Trim();
            }

            if (Request.QueryString["IS_CHECKED"] != null && Request.QueryString["IS_CHECKED"].ToString().Trim() != "")
            {
                hidIS_CHECKED.Value = Request.QueryString["IS_CHECKED"].ToString().Trim();
            }
            //}
        }

        private void FetchData()
        {

            try
            {
                if (hidDEFIND_ID.Value == "2")//User Defind
                {
                    if (hidPOL_CLAUSE_ID.Value != "" && hidPOL_CLAUSE_ID.Value != "0")
                    {
                        dtTemp = objPolicyClauseInfo.FetchUserDefinedClauses(int.Parse(hidPOL_CLAUSE_ID.Value), int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));

                        if (dtTemp != null)
                        {
                            hidOldData.Value = dtTemp.DataSet.GetXml();
                            hidCustomerID.Value = dtTemp.Rows[0]["CUSTOMER_ID"].ToString();
                            hidPolicyID.Value = dtTemp.Rows[0]["POLICY_ID"].ToString();
                            hidPolicyVersionID.Value = dtTemp.Rows[0]["POLICY_VERSION_ID"].ToString();
                            hidCmbSUSEPLOB1.Value = dtTemp.Rows[0]["SUSEP_LOB_ID"].ToString();


                            txtCLAUSE_TITLE.Text = dtTemp.Rows[0]["CLAUSE_TITLE"].ToString();
                            txtareaCLAUSE_DESCRIPTION.Value = dtTemp.Rows[0]["CLAUSE_DESCRIPTION"].ToString();

                            objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue = int.Parse(hidPOL_CLAUSE_ID.Value);
                            objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                            objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                            objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                            objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = txtCLAUSE_TITLE.Text.Trim();
                            objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = txtareaCLAUSE_DESCRIPTION.Value.Trim();                           
                            cmbCLAUSE_TYPE.SelectedValue = objPolicyClauseInfo.CLAUSE_TYPE.CurrentValue.ToString();
                        
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
                                string previousversion = dtTemp.Rows[0]["PREVIOUS_VERSION_ID"].ToString();
                                if (previousversion != hidPolicyVersionID.Value)
                                {
                                    lblHeader.Visible = false;
                                    lblHeader1.Visible = true;
                                    fileATTACH_FILE_NAME.Visible = false;
                                    btnSave.Visible = false;
                                    cmbCLAUSE_TYPE.Enabled = false;
                                    txtCLAUSE_CODE.ReadOnly = true;
                                    txtCLAUSE_TITLE.ReadOnly = true;
                                }                           
                            base.SetPageModelObject(objPolicyClauseInfo);
                        }
                    }
                    if (PolicyStatus == "" || PolicyStatus == "UENDRS")
                    {
                        lblHeader1.Visible = false;

                    }
                    else
                    {
                        lblHeader.Visible = false;
                        fileATTACH_FILE_NAME.Visible = false;
                        btnSave.Visible = false;
                        cmbCLAUSE_TYPE.Enabled = false;
                        txtCLAUSE_CODE.ReadOnly = true;
                        txtCLAUSE_TITLE.ReadOnly = true;
                    }
                   

                }
                else  // System Defind
                {
                    if (hidCLAUSE_ID1.Value != "" && hidCLAUSE_ID1.Value != "0")
                    {

                        dtTemp = objPolicyClauseInfo.FetchDataForSystemDefined(int.Parse(hidCLAUSE_ID1.Value),
                        int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value), hidIS_CHECKED.Value);
                        txtCLAUSE_TITLE.Text = dtTemp.Rows[0]["CLAUSE_TITLE"].ToString();
                        txtareaCLAUSE_DESCRIPTION.Value = dtTemp.Rows[0]["CLAUSE_DESCRIPTION"].ToString();
                        objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue = int.Parse(hidPOL_CLAUSE_ID.Value);
                        objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = txtCLAUSE_TITLE.Text.Trim();
                        objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = txtareaCLAUSE_DESCRIPTION.Value.Trim();                   
                      
                        cmbCLAUSE_TYPE.SelectedValue = objPolicyClauseInfo.CLAUSE_TYPE.CurrentValue.ToString();
                    
                        hidOldData.Value = dtTemp.DataSet.GetXml();

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
                            lblHeader.Visible = false;
                            txtCLAUSE_CODE.ReadOnly = true;

                        }
                        base.SetPageModelObject(objPolicyClauseInfo);

                        // if (hidIS_CHECKED.Value !="1")
                        // {
                        btnSave.Enabled = false;
                        btnSave.Visible = false;
                        // }   


                    }
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            }

        private void SetCaptions()
        {
            try
            {
                capCLAUSE_TITLE.Text = objResourceManager.GetString("txtCLAUSE_TITLE");
                capCLAUSE_DESCRIPTION.Text = objResourceManager.GetString("txtCLAUSE_DESCRIPTION");
                lblHeader.Text = objResourceManager.GetString("lblHeader");
                hidPageTitle.Value = objResourceManager.GetString("lblTitle");
                capATTACH_FILE_NAME.Text = objResourceManager.GetString("capATTACH_FILE_NAME");
                capCLAUSE_TYPE.Text = objResourceManager.GetString("capCLAUSE_TYPE");
                lblHeader1.Text = objResourceManager.GetString("lblHeader1");
                capCLAUSE_CODE.Text = objResourceManager.GetString("capCLAUSE_CODE");
            }
            catch
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("");
                lblMessage.Visible = true;
            }
        }
        private void GetFormValue(ClsPolicyClauseInfo objPolicyClauseInfo)
        {
            try
            {
                objPolicyClauseInfo.CLAUSE_ID.CurrentValue = 0;
                objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = txtCLAUSE_TITLE.Text.Trim();
                objPolicyClauseInfo.PREVIOUS_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                if (txtareaCLAUSE_DESCRIPTION.Value != "")
                {
                    objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = txtareaCLAUSE_DESCRIPTION.Value.Trim();
                }
             
                if (cmbCLAUSE_TYPE.SelectedItem != null)
                {
                    objPolicyClauseInfo.CLAUSE_TYPE.CurrentValue = Convert.ToInt32(cmbCLAUSE_TYPE.SelectedValue);
                }
                if (txtCLAUSE_CODE.Text != "")
                {

                    objPolicyClauseInfo.CLAUSE_CODE.CurrentValue = Convert.ToString(txtCLAUSE_CODE.Text);
                }
                if (cmbCLAUSE_TYPE.SelectedValue == "14696")
                {

                   oldXML = hidOldData.Value;

                    //=========  File Upload ===========START

                   strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
                   strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
                   strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

                    //---- Begin
                    //Below code gets the File name from Label or Text i.e; 
                    //in either update or saved case.
                    if (oldXML != "" && hidPOL_CLAUSE_ID.Value!="")
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
                    // added by praveer , append U in case of User Defined Clauses
                    objPolicyClauseInfo.ATTACH_FILE_NAME.CurrentValue = "U_"+ hidPOL_CLAUSE_ID.Value + '_' + hidCustomerID.Value + '_' + hidPolicyID.Value + '_' + hidPolicyVersionID.Value + '_' + strFileName;


                    //=========  File Upload ===========END

                }
                if (cmbCLAUSE_TYPE.SelectedValue == "14696")
                {
                    objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = "";
                }
                else {
                    objPolicyClauseInfo.ATTACH_FILE_NAME.CurrentValue = "";
                    rfvATTACH_FILE_NAME.Enabled = false;
                }

            }
            catch
            {
                lblMessage.Text = ClsMessages.GetMessage("224_28", "9");
                lblMessage.Visible = true;
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
                    lblMessage.Text += ClsMessages.GetMessage(base.ScreenId, "1275"); ;
                }

                //ending the impersonation 
                objAttachment.endImpersonation();
            }
            else
            {
                //Impersation failed
                lblMessage.Text += ClsMessages.GetMessage(base.ScreenId, "1276"); ;
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

                if (Index1 >= 0)
                {
                    strFileName1 = objFile1.PostedFile.FileName.Substring(Index1 + 1);
                }
                else
                {
                    strFileName1 = objFile1.PostedFile.FileName;
                }

                //copying the files
                // added by praveer , append U in case of User Defined Clauses
                objFile1.PostedFile.SaveAs(strDirName + "\\" + "U_"+ hidPOL_CLAUSE_ID.Value + "_" + hidCustomerID.Value + "_" + hidPolicyID.Value + "_" + hidPolicyVersionID.Value + "_" + strFileName1);

                ClsPolicyClauseInfo PolicyClauseInfo = new ClsPolicyClauseInfo();
                string Attach = ("U_" + hidPOL_CLAUSE_ID.Value + "_" + hidCustomerID.Value + "_" + hidPolicyID.Value + "_" + hidPolicyVersionID.Value + "_" + strFileName1);
                string pol = hidPOL_CLAUSE_ID.Value;
                //PolicyClauseInfo.CLAUSE_CODE.CurrentValue = Convert.ToString(txtCLAUSE_CODE.Text);
                PolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                PolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                PolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);       
                PolicyClauseInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                int retVal = PolicyClauseInfo.UpdateAttachment(pol,Attach);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int retVal = 0;
           
            if (hidDEFIND_ID.Value == "2") //when User Defind then if part of the code will execute
            {

                if (hidPOL_CLAUSE_ID.Value == "0" || hidPOL_CLAUSE_ID.Value == "")
                {
                    try
                    {
                        
                        GetFormValue(objPolicyClauseInfo);

                        objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                        objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                        objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                        objPolicyClauseInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                        objPolicyClauseInfo.SUSEP_LOB_ID.CurrentValue = int.Parse(GetLOBID());
                        retVal = objPolicyClauseInfo.AddPolClauses();

                        if (retVal > 0)
                        {
                            hidPOL_CLAUSE_ID.Value = retVal.ToString();

                            objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue = int.Parse(hidPOL_CLAUSE_ID.Value);
                            base.SetPageModelObject(objPolicyClauseInfo);
                            if (cmbCLAUSE_TYPE.SelectedValue == "14696")
                            {
                               
                                UploadFile();
                                
                               
                               
                            }
                            FetchData();
                            lblMessage.Text = ClsMessages.GetMessage("224_28", "4");
                            lblMessage.Visible = true;
                        }
                        else if (retVal == -2) {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("1358");
                            lblMessage.Visible = true;
                            revATTACH_FILE_EXT.Enabled = false;
                            revATTACH_FILE_PDF.Enabled = false;
                        }
                        else
                        {
                            lblMessage.Text = ClsMessages.GetMessage("224_28", "21");
                            lblMessage.Visible = true;
                            revATTACH_FILE_EXT.Enabled = false;
                            revATTACH_FILE_PDF.Enabled = false;
                        }
                    }
                    catch
                    {
                        lblMessage.Text = ClsMessages.GetMessage("224_28", "9");
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    try
                    {
                        objPolicyClauseInfo = (ClsPolicyClauseInfo)base.GetPageModelObject();
                        objPolicyClauseInfo.TransactLabel = hidTransactLabel.Value;
                        objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = txtCLAUSE_TITLE.Text.Trim();
                        objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = txtareaCLAUSE_DESCRIPTION.Value.Trim();
                        objPolicyClauseInfo.CLAUSE_ID.CurrentValue = 0;
                        GetFormValue(objPolicyClauseInfo);
                        objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                        objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                        objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);


                        objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue = int.Parse(hidPOL_CLAUSE_ID.Value);

                        objPolicyClauseInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                        //-------------- Added By Praveen Kumar 29/04/2010  starts --------------
                        if (hidCmbSUSEPLOB1.Value == "")
                        {
                            objPolicyClauseInfo.SUSEP_LOB_ID.CurrentValue = 0;
                        }
                        else
                        {
                            objPolicyClauseInfo.SUSEP_LOB_ID.CurrentValue = int.Parse(hidCmbSUSEPLOB1.Value);

                        }
                        //-------------- Added By Praveen Kumar 29/04/2010  Ends --------------

                        retVal = objPolicyClauseInfo.UpdatePolClauses();

                        if (retVal ==2)
                        {
                            if (cmbCLAUSE_TYPE.SelectedValue == "14696")
                            {

                                UploadFile();
                                
                            }
                            FetchData();
                            lblMessage.Text = ClsMessages.GetMessage("224_28", "14");
                            lblMessage.Visible = true;

                        }
                        else if (retVal == -2)
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("1358");
                            lblMessage.Visible = true;
                            revATTACH_FILE_EXT.Enabled = false;
                            revATTACH_FILE_PDF.Enabled = false;
                        }
                        else
                        {
                            lblMessage.Text = ClsMessages.GetMessage("224_28", "15");
                            lblMessage.Visible = true;
                        }
                    }
                    catch
                    {
                        lblMessage.Text = ClsMessages.GetMessage("224_28", "15");
                        lblMessage.Visible = true;
                    }
                }
            }
            else ////when System defind then this else part of the code will execute 
                //In System defined case only update statement will execute
            {
               
                try
                {
                    objPolicyClauseInfo = (ClsPolicyClauseInfo)base.GetPageModelObject();

                    objPolicyClauseInfo.CLAUSE_TITLE.CurrentValue = txtCLAUSE_TITLE.Text.Trim();
                    objPolicyClauseInfo.CLAUSE_DESCRIPTION.CurrentValue = txtareaCLAUSE_DESCRIPTION.Value.Trim();
                    objPolicyClauseInfo.CLAUSE_ID.CurrentValue = int.Parse(hidCLAUSE_ID1.Value);
                   
                    GetFormValue(objPolicyClauseInfo);
                    objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomerID.Value);
                    objPolicyClauseInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicyID.Value);
                    objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicyVersionID.Value);
                    objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue = 0;// int.Parse(hidPOL_CLAUSE_ID.Value);
                    

                    objPolicyClauseInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());                    

                    retVal = objPolicyClauseInfo.UpdateSysClauses();
                    if (retVal > 0)
                    {
                        if (cmbCLAUSE_TYPE.SelectedValue == "14696")
                        {

                            UploadFile();
                            
                        }
                        FetchData();
                        lblMessage.Text = ClsMessages.GetMessage("224_28", "19");
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage("224_28", "20");
                        lblMessage.Visible = true;
                    }
                }
                catch
                {
                    lblMessage.Text = ClsMessages.GetMessage("224_28", "20");
                    lblMessage.Visible = true;
                }
            }
        }
    }
}