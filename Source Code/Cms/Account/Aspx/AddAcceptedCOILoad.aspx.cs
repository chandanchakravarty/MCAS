using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Account;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Net;





using System.IO;



namespace Cms.Account.Aspx 
{
    public partial class AcceptedCOILoad : Cms.Account.AccountBase
    {
       

        ClsAcceptedCOILoadDetails objImportRequest = new ClsAcceptedCOILoadDetails();
        System.Resources.ResourceManager objResourceMgr;
        private String strRowId = String.Empty;
        //protected System.Web.UI.WebControls.ImageButton BtnDeleteFile;
        string strUserName = "";
        string strPassWd = "";
        string strDomain = "";
       // string strFileName;
        protected System.Web.UI.HtmlControls.HtmlInputFile txtATTACH_FILE_NAME;
    

        protected void Page_Load(object sender, EventArgs e)
        {

            base.ScreenId = "538_0";
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            btnAddNew.CmsButtonClass = CmsButtonType.Write;
            btnAddNew.PermissionString = gstrSecurityXML;
            btnStartProcess.CmsButtonClass = CmsButtonType.Write;
            btnStartProcess.PermissionString = gstrSecurityXML;
            btnViewDetails.CmsButtonClass = CmsButtonType.Write;
            btnViewDetails.PermissionString = gstrSecurityXML;
            btnViewDetails.Attributes.Add("onclick", "javascript:return ViewDetails();");
            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddAcceptedCOILoad", System.Reflection.Assembly.GetExecutingAssembly());

            //if (Request.QueryString["IMPORT_REQUEST_FILE_ID"] == null)
            //{
            //    BtnDeleteFile.Visible = false;
            //}
            
            if (!Page.IsPostBack)
            {
                //SetCaptions();
                BindFileType();
                setLabel();
                SetErrorMessages();
                GetQueryStringValues();
                SetPageLabels();
                LoadGridData();
                
                              
            }
        }




        protected void btnSave_Click(object sender, EventArgs e)
        {

            UploadFile();
            btnViewDetails.Visible = false;
           
           
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            BindFileType();
            PnlDetails.Visible = true;
            
            if (int.Parse(hidIMPORT_REQUEST_ID.Value) > 0)
            {               
                trPACKAGE_NAME.Visible = false;

            }
           
        }
        

        protected void btnStartProcess_Click(object sender, EventArgs e)
        {
            StartProcess();

           
 //           Microsoft.SqlServer.Dts.Runtime.Application app = new  Microsoft.SqlServer.Dts.Runtime.Application();
 //           Package package = null;
 //           try
 //           {
         
 //           //Load DTSX
 //           package = 
 //               app.LoadPackage(@"D:\SSIS_ASP_NET\SSIS_ASP_NET_DEMO\SSIS_ASP_NET_DEMO\Package1.dtsx", null);
            
 //           //Global Package Variable
 //           //Variables vars = package.Variables;
 //           //vars[&quot;Business_ID&quot;].Value = txtBusinessID.Text;
 //           //vars[&quot;Business_Name&quot;].Value = txtBusinessName.Text;
            
 //           //Specify Excel Connection From DTSX Connection Manager
 ////           package.Connections["SourceConnectionExcel"].ConnectionString =
 ////"provider=Microsoft.Jet.OLEDB.4.0;data source=&quot; + fileName +";Extended Properties=Excel 8.0; &quot";
            
 //           //Execute DTSX.
 //           Microsoft.SqlServer.Dts.Runtime.DTSExecResult results = package.Execute();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //finally
        //{
        //    package.Dispose();
        //    package = null;
        //}



           
        }


        private int StartProcess()
        {
            int intRetval = 0;

            try
            {
                ClsAcceptedCOILoadInfo objAcceptedCOILoadInfo = new ClsAcceptedCOILoadInfo();

                int ImportRequestID = 0;
                if (hidIMPORT_REQUEST_ID.Value != "")
                    ImportRequestID = int.Parse(hidIMPORT_REQUEST_ID.Value);

                //objAcceptedCOILoadInfo.REQUEST_STATUS.CurrentValue = (hidREQUEST_STATUS.Value);
                objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue = ImportRequestID;
                intRetval = objImportRequest.UpdateImportRequest(objAcceptedCOILoadInfo);


                if (intRetval > 0)
                {
                    hidIMPORT_REQUEST_ID.Value = objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue.ToString();

                    lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    hidFormSaved.Value = "1";
                    btnStartProcess.Visible = false;
                    btnAddNew.Visible = false;
                    btnViewDetails.Visible = false;


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
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }

            return intRetval;
            

        }

    

        /// <summary>
        /// Validate posted data from form. Calls in SaveFormData function
        /// </summary>
        /// <returns>True if posted data is valid else false</returns>
        private bool DoValidationCheck()
        {
            try
            {
                if (hidIMPORT_REQUEST_FILE_ID.Value == "NEW")
                {
                    if (txtATTACH_FILE_NAME.PostedFile.FileName.Trim().Equals(""))
                    {
                        return false;
                    }

                    string FileName = txtATTACH_FILE_NAME.PostedFile.FileName;

                    int Index=FileName.LastIndexOf(".");


                    if (FileName.Substring(Index,(FileName.Length-Index)).ToLower()!=".txt")
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "123");
                        hidFormSaved.Value = "2";
                       
                        return false;
                    }
                    //if( txtATTACH_FILE_TYPE.Text.Trim().Equals(""))
                    //{
                    //    return false;
                    //}
                    if (txtATTACH_FILE_NAME.PostedFile.InputStream.Length == 0)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "123");
                        hidFormSaved.Value = "1";
                        return false;

                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// This function will set the Error Message,validation expresion all validators controls
        /// </summary>
        private void SetPageLabels()
        {
            //setting validation expression for different validation control

            rfvIMPORT_FILE_TYPE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            rfvREQUEST_DESC.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            rfvATTACH_FILE_NAME.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            //setting error message by calling singleton functions

        }

        private void setLabel()
        {
            grdImportRequestFiles.Columns[2].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            grdImportRequestFiles.Columns[3].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            grdImportRequestFiles.Columns[4].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            grdImportRequestFiles.Columns[5].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            grdImportRequestFiles.Columns[6].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
            btnAddNew.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");
            lblREQUEST_DESC.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
            capIMPORT_FILE_TYPE.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");
            capATTACH_FILE_NAME.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
            btnViewDetails.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");
            btnStartProcess.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
        }
        /// <summary>
        /// This function is used to save the uploaded file in harddisk
        /// </summary>
        /// 
        protected void UploadFile()
        {
            //=========  File Upload ===========START

            strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
            strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
            strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

            Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
            if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
            {
                SaveUploadedFile();               

                //ending the impersonation 
                objAttachment.endImpersonation();
            }
            else
            {
                //Impersation failed
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "1276"); ;
                lblMessage.Visible = true;
            }
            //=================== File Upload ====================
        }

        private void SaveUploadedFile()
        {
            try
            {

                if (DoValidationCheck() == false)
                    return;


                if (hidIMPORT_REQUEST_ID.Value == "")                
                   if(AddImportRequest()<1)
                       return;
                   



                //Stores the name of the directory where file will get stored
                string strDirName = "";
                string strActualFileName = "";
                string strFileName = "";
                string strRoot;
                string strFilePath="";
                string strFilePathToSave = "";
                

                string Import_Request_ID=hidIMPORT_REQUEST_ID.Value;
                switch(int.Parse(cmbIMPORT_FILE_TYPE.SelectedValue))
                {
                        case 14910: strFileName=Import_Request_ID+"_ISSUANCE.txt"; break;
                        case 14911: strFileName=Import_Request_ID+"_COVERAGES.txt"; break;
                        case 14912: strFileName=Import_Request_ID+"_INST_PAYMENT.txt"; break;
                        case 14913: strFileName=Import_Request_ID+"_INST_PAYMENT_CANCEL.txt"; break;
                        case 14914: strFileName=Import_Request_ID+"_FNOL.txt"; break;
                        case 14915: strFileName=Import_Request_ID+"_PAID_CLAIM.txt"; break;
                }

           

                strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("AcceptedCOIUploadURL");

                int Index =0;
                Index= strRoot.LastIndexOf("/");

                string strFilePathFolder = strRoot.Substring(Index + 1);

                string strFolderName  = DateTime.Now.Year.ToString();

                if (DateTime.Now.Month < 10)
                    strFolderName += "0" + DateTime.Now.Month.ToString();
                else
                    strFolderName +=  DateTime.Now.Month.ToString();

                if (DateTime.Now.Day < 10)
                    strFolderName += "0" + DateTime.Now.Day.ToString();
                else
                    strFolderName += DateTime.Now.Day.ToString();



                strDirName = strRoot + "/" + strFolderName;
                strDirName = Server.MapPath(strDirName);

                CreateDirStructure(strDirName);
                //if (!System.IO.Directory.Exists(strDirName))
                //    {
                //        //Creating the directory
                //        System.IO.Directory.CreateDirectory(strDirName);
                //    }

              

                //Retreiving the extension

                Index= txtATTACH_FILE_NAME.PostedFile.FileName.LastIndexOf("\\");
                if (Index >= 0)                
                    strActualFileName = txtATTACH_FILE_NAME.PostedFile.FileName.Substring(Index + 1);                
                else                
                    strActualFileName = txtATTACH_FILE_NAME.PostedFile.FileName;

                strFilePathToSave = strFolderName + "\\" + strFileName;

                strFilePath = strDirName + "\\" + strFileName;
                //copying the file
                txtATTACH_FILE_NAME.PostedFile.SaveAs(strFilePath);

               // strFilePath =strFilePathFolder+"\\"+ strFolderName + "\\" + strFileName;

                AddImportRequestFile(strFileName, strFilePathToSave, strActualFileName);

                BindFileType();


            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);

            }
        }

        private string CreateDirStructure(string DirName)
        {

         //   string strRoot, strDirName = "";
            try
            {
                //strRoot = System.Configuration.ConfigurationSettings.AppSettings.Get("AcceptedCOIUploadURL");
                //strDirName = Server.MapPath(strRoot);
                ////Creating the Attachment folder if not exists
              //  strDirName = strDirName + "\\" + DirName;
                if (!System.IO.Directory.Exists(DirName))
                {
                    //Creating the directory
                    System.IO.Directory.CreateDirectory(DirName);
                }
            }
            catch (Exception objEx)
            {
                throw (objEx);
            }
            return DirName;
        }
        private int AddImportRequest()
        {
            int intRetval = 0;

            try
            {
                ClsAcceptedCOILoadInfo objAcceptedCOILoadInfo = new ClsAcceptedCOILoadInfo();

                int ImportRequestID = 0;
                if (hidIMPORT_REQUEST_ID.Value != "")
                    ImportRequestID = int.Parse(hidIMPORT_REQUEST_ID.Value);
                

                objAcceptedCOILoadInfo.REQUEST_DESC.CurrentValue = txtREQUEST_DESC.Text;
               
                objAcceptedCOILoadInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                objAcceptedCOILoadInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                objAcceptedCOILoadInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                objAcceptedCOILoadInfo.SUBMITTED_BY.CurrentValue = int.Parse(GetUserId());
                intRetval = objImportRequest.AddImportRequest(objAcceptedCOILoadInfo);


                if (intRetval > 0)
                {
                    hidIMPORT_REQUEST_ID.Value = objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue.ToString();

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
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            
                return intRetval;
            
        }


        private void AddImportRequestFile(string fileName, string filePath, string actualFileName)
        {
            int intRetval = 0;

            try
            {
                ClsAcceptedCOILoadInfo objAcceptedCOILoadInfo = new ClsAcceptedCOILoadInfo();

                int ImportRequestID=0;
                //int ImportRequestFileID = 0;
                if(hidIMPORT_REQUEST_ID.Value!="")
                    ImportRequestID=int.Parse(hidIMPORT_REQUEST_ID.Value);

                //if (hidIMPORT_REQUEST_FILE_ID.Value != "")
                //    ImportRequestFileID = int.Parse(hidIMPORT_REQUEST_ID.Value);

                actualFileName = actualFileName.ToLower().Replace(".txt", "");
           
                objAcceptedCOILoadInfo.IMPORT_FILE_NAME.CurrentValue = fileName;
                objAcceptedCOILoadInfo.DISPLAY_FILE_NAME.CurrentValue = actualFileName;
                objAcceptedCOILoadInfo.IMPORT_FILE_PATH.CurrentValue = filePath;
                objAcceptedCOILoadInfo.REQUEST_DESC.CurrentValue     = txtREQUEST_DESC.Text;
                objAcceptedCOILoadInfo.IMPORT_FILE_TYPE.CurrentValue = int.Parse(cmbIMPORT_FILE_TYPE.SelectedValue);
                objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue = ImportRequestID;
                //objAcceptedCOILoadInfo.IMPORT_REQUEST_FILE_ID.CurrentValue = ImportRequestFileID;
                objAcceptedCOILoadInfo.FILE_IMPORTED_DATE.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                objAcceptedCOILoadInfo.FILE_IMPORTED_BY.CurrentValue = int.Parse(GetUserId());
                //objAcceptedCOILoadInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                //objAcceptedCOILoadInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                        
                intRetval = objImportRequest.AddImportRequestFile(objAcceptedCOILoadInfo);


                if (intRetval > 0)
                {

                    hidIMPORT_REQUEST_ID.Value = objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue.ToString();

                    lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                    hidFormSaved.Value = "1";
                    LoadGridData();

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
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }

        }


       

        private void GetQueryStringValues()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["IMPORT_REQUEST_FILE_ID"]) && Request.QueryString["IMPORT_REQUEST_FILE_ID"] != "NEW")
                hidIMPORT_REQUEST_FILE_ID.Value = Request.QueryString["IMPORT_REQUEST_FILE_ID"].ToString();
            else
                hidIMPORT_REQUEST_FILE_ID.Value = "NEW";

            if (Request.QueryString["IMPORT_REQUEST_ID"] != null && Request.QueryString["IMPORT_REQUEST_ID"].ToString() != "")
                hidIMPORT_REQUEST_ID.Value = Request.QueryString["IMPORT_REQUEST_ID"].ToString();

            if (Request.QueryString["REQUEST_STATUS_CODE"] != null && Request.QueryString["REQUEST_STATUS_CODE"].ToString() != "")
                hidREQUEST_STATUS.Value = Request.QueryString["REQUEST_STATUS_CODE"].ToString();

            //if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
            //    hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            //if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
            //    hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            //if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
            //    hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            //if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
            //    hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();

        }

        private void SetCaptions()
        {
            
            lblREQUEST_DESC.Text = objResourceMgr.GetString("lblREQUEST_DESC");
            capIMPORT_FILE_TYPE.Text = objResourceMgr.GetString("capIMPORT_FILE_TYPE");
           capATTACH_FILE_NAME.Text = objResourceMgr.GetString("capATTACH_FILE_NAME");
            
            lblRequiredFieldsInformation.Text = objResourceMgr.GetString("lblRequiredFieldsInformation");

        }

        private void SetErrorMessages()
        {
            //revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            //revJUDICIAL_PROCESS_DATE.ValidationExpression = aRegExpDate;


            //rfvCPF_CNPJ.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
            //revCPF_CNPJ.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
            //rfvJUDICIAL_PROCESS_NO.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
            //rfvJUDICIAL_COMPLAINT_STATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");


            //revJUDICIAL_PROCESS_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            //revPLAINTIFF_REQUESTED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
            ////revPLAINTIFF_REQUESTED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            //revDEFEDANT_OFFERED_AMOUNT.ValidationExpression = aRegExpCurrencyformat;                                                              
            ////revDEFEDANT_OFFERED_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            //csvDEFEDANT_OFFERED_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");
            //csvPLAINTIFF_REQUESTED_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");

            //revESTIMATE_CLASSIFICATION.ValidationExpression = aRegExpInteger;

            //revESTIMATE_CLASSIFICATION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
            //csvESTIMATE_CLASSIFICATION.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");


        }

        private void LoadGridData()
        {
            ManageScreen();

            if (hidIMPORT_REQUEST_ID.Value == "")
            {              
                return;
            }
                

            DataTable dt = objImportRequest.GetImportRequestDetails(int.Parse(hidIMPORT_REQUEST_ID.Value), ClsCommon.BL_LANG_ID);




            if (dt!=null && dt.Rows.Count > 0)
            {
                PnlGrid.Visible = true; ;
                grdImportRequestFiles.DataSource = dt;
                grdImportRequestFiles.DataBind();

            }
            else
            {
                PnlDetails.Visible = true;
                PnlGrid.Visible = false;


            }

           

           
            if ((hidREQUEST_STATUS.Value) == "NSTART")
            {
                btnStartProcess.Visible = true;
                btnAddNew.Visible = true;
            
                btnViewDetails.Visible = false;
            }
            else if ((hidREQUEST_STATUS.Value) == "COMP" || (hidREQUEST_STATUS.Value) == "FAIL")
            {
                btnViewDetails.Visible = true;

                btnStartProcess.Visible = false;
                btnAddNew.Visible = false;            
            }

            else if ((hidREQUEST_STATUS.Value) == "INQUEUE")
            {
                btnStartProcess.Visible = false;

                btnViewDetails.Visible = false;

                btnAddNew.Visible = false;

            }
            else if ((hidREQUEST_STATUS.Value) == "INPRG")
            {
                btnStartProcess.Visible = false;
                btnViewDetails.Visible = false;
                btnAddNew.Visible = false;
            }
            //btnViewDetails.Visible = false;


          
        }

        private void ManageScreen()
        {

            if (hidIMPORT_REQUEST_ID.Value !="" && int.Parse(hidIMPORT_REQUEST_ID.Value) > 0)
            {
                PnlGrid.Visible = true;
                PnlDetails.Visible = false;
                rfvREQUEST_DESC.Enabled = false;
                rfvREQUEST_DESC.Visible = false;
              lblREQUEST_DESC.Visible = false;
              txtREQUEST_DESC.Visible = false;
              trPACKAGE_NAME.Visible = false;
                btnAddNew.Visible = true;

            }
            else
            {
                PnlGrid.Visible = false;
                PnlDetails.Visible = true;
                rfvREQUEST_DESC.Enabled = true;
                rfvREQUEST_DESC.Visible = true;
                lblREQUEST_DESC.Visible = true;
                txtREQUEST_DESC.Visible = true;
                trPACKAGE_NAME.Visible = true;

            }

        }
        protected void grdImportRequestFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string Delete_Disable = "../../cmsweb/images/Delete_Disabled.jpg"; 

            if (e.Row.RowType != DataControlRowType.EmptyDataRow)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[7].Visible = false;
                
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[7].Text != "NSTART")
                {
                    ImageButton BtnDeleteRecord = e.Row.FindControl("BtnDeleteFile") as ImageButton;
                    if (BtnDeleteRecord != null)
                    {
                        BtnDeleteRecord.ImageUrl = Delete_Disable;
                        BtnDeleteRecord.Enabled = false;
                    }
                }
            }
            
        }

        private void BindFileType()
        { 
            int IMPORT_REQT_FILE_ID=0;
            if(hidIMPORT_REQUEST_ID.Value != "")
                IMPORT_REQT_FILE_ID = int.Parse(hidIMPORT_REQUEST_ID.Value);
           
            ClsAcceptedCOILoadInfo objImportRequest = new ClsAcceptedCOILoadInfo();
            int lang_id = int.Parse(GetLanguageID());
            cmbIMPORT_FILE_TYPE.DataSource = objImportRequest.Getfiletypes(lang_id,IMPORT_REQT_FILE_ID);           
            cmbIMPORT_FILE_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
            cmbIMPORT_FILE_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbIMPORT_FILE_TYPE.DataBind();
            cmbIMPORT_FILE_TYPE.Items.Insert(0, "");
        }
        protected void grdImportRequestFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFile")
            {
                ClsAcceptedCOILoadInfo objAcceptedCOI = new ClsAcceptedCOILoadInfo();
                int Index = int.Parse(e.CommandArgument.ToString());
                objAcceptedCOI.IMPORT_REQUEST_ID.CurrentValue = int.Parse(grdImportRequestFiles.Rows[Index].Cells[0].Text);
                objAcceptedCOI.IMPORT_REQUEST_FILE_ID.CurrentValue = int.Parse(grdImportRequestFiles.Rows[Index].Cells[1].Text);

                if (objImportRequest.DeleteFile(objAcceptedCOI) > 0)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                    hidFormSaved.Value = "2";

                    LoadGridData();
                }

                //if (objImportRequest.DeleteFile(objAcceptedCOI) == 0)
                //{
                //    PnlGrid.Visible = false;
                //}

                
                
            }
        }
    }
       
 
}
