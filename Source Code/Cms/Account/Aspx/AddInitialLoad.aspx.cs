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
using Excel = Microsoft.Office.Interop.Excel;//changes by praveer for TFS# 460 
using Microsoft.Office.Interop.Excel;//changes by praveer for TFS# 460 
using System.Runtime.InteropServices;
using System.Collections;//changes by praveer for TFS# 460 

namespace Cms.Account.Aspx
{
    public partial class AddInitialLoad : Cms.Account.AccountBase
    {
        ClsAcceptedCOILoadDetails objImportRequest = new ClsAcceptedCOILoadDetails();
        System.Resources.ResourceManager objResourceMgr;
        private String strRowId = String.Empty;
        string strUserName = "";
        string strPassWd = "";
        string strDomain = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            base.ScreenId = "551_1";
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

            btnChecksumSummary.CmsButtonClass = CmsButtonType.Write;
            btnChecksumSummary.PermissionString = gstrSecurityXML;

            btnCommitPolicyList.CmsButtonClass = CmsButtonType.Write;
            btnCommitPolicyList.PermissionString = gstrSecurityXML;
            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddInitialLoad", System.Reflection.Assembly.GetExecutingAssembly());
            btnChecksumSummary.Attributes.Add("onclick", "javascript:return ViewChecksum();");
 
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
                intRetval = objImportRequest.UpdateInialLoadImportRequest(objAcceptedCOILoadInfo);


                if (intRetval > 0)
                {
                    hidIMPORT_REQUEST_ID.Value = objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue.ToString();
                    LoadGridData();
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    hidFormSaved.Value = "1";
                    btnStartProcess.Visible = false;
                    btnAddNew.Visible = false;
                    
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

                    int Index = FileName.LastIndexOf(".");


                    if (FileName.Substring(Index, (FileName.Length - Index)).ToLower() != ".xlsx")
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
            rfvGROUP_FILE_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            //setting error message by calling singleton functions

        }

        private void setLabel()
        {
            grdImportRequestFiles.Columns[2].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            grdImportRequestFiles.Columns[3].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");

      
            grdImportRequestFiles.Columns[4].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            grdImportRequestFiles.Columns[5].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            grdImportRequestFiles.Columns[6].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            grdImportRequestFiles.Columns[7].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
            grdImportRequestFiles.Columns[8].HeaderText = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");

            btnAddNew.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");
            lblREQUEST_DESC.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
            capIMPORT_FILE_TYPE.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");
            capATTACH_FILE_NAME.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
            btnStartProcess.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
            capGROUP_FILE_TYPE.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");

            btnChecksumSummary.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");
            btnCommitPolicyList.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
        }
        /// <summary>
        /// This function is used to save the uploaded file in harddisk
        /// </summary>
        /// 
        protected void UploadFile()
        {

            // MODIFIED BY SANTOSH KR GAUTAM ON 14 SEPT 2011(FILE NAME VALIDATION)

            string[] LOB_CODE = { "0196", "0116", "0118", "0351", "0433", "0171", "0981", "0115", "0553", "0523", "0114",
                                   "0621", "9820", "9821", "0622", "0272", "0111", "0167", "0173", "0435" , "0531", "0589"
                                   , "0654" , "0750" , "0977", "0993", "1163", "0588", "0746","0982"
                                };


            string strActualFileName = "";
           
            int Index = txtATTACH_FILE_NAME.PostedFile.FileName.LastIndexOf("\\");
            if (Index >= 0)
                strActualFileName = txtATTACH_FILE_NAME.PostedFile.FileName.Substring(Index + 1);
            else
                strActualFileName = txtATTACH_FILE_NAME.PostedFile.FileName;

            // Invalid File Name
            if (strActualFileName.Length < 13)
            {
                lblMessage.Visible = true;

                if (cmbGROUP_FILE_TYPE.SelectedValue == "1441") // for customer group type
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
                else
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");

                return;
             
            }
            else
            {
                // ==========================================================================
                // VALIDATE LOB CODE
                // ==========================================================================

                string strLOBCode = strActualFileName.Substring(0, 4);

                // IF CUSTOMER TYPE FILE THEN FIRST FOUR DIGIT MUST BE ZERO
                if (cmbGROUP_FILE_TYPE.SelectedValue == "1441" && strLOBCode!="0000")
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
                    lblMessage.Visible = true;
                    return;
                }
                else if(cmbGROUP_FILE_TYPE.SelectedValue != "1441")
                {
                    // OTHER THEN CUSTOMER GROUP TYPE FIRST FOUR DIGIT OF FILE NAME MUST BE SUSEP LOB CODE
                    if (LOB_CODE.Contains(strLOBCode) == false)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
                        lblMessage.Visible = true;
                        return;
                    }

                }

                // ==========================================================================
                // VALIDATE SEQUENCIAL NUMBER
                // ==========================================================================
                string strSeqNum = strActualFileName.Substring(4, 5);
                int temp=0;
                if (int.TryParse(strSeqNum, out  temp) == false)
                {
                    lblMessage.Visible = true;

                    if (cmbGROUP_FILE_TYPE.SelectedValue == "1441") // for customer group type
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
                    else
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");

                    return;
             
                }

                 

            }


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
            {   //start changes by praveer for TFS# 460 
                // comment due the reason, this try to kill whole excel process
                //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                //{
                //    process.Kill();
                //    process.WaitForExit();
                //} 
                object oMissing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Application xl = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook xlBook;
                Excel.Worksheet xlSheet;
                bool flag = false;
               
                //END changes by praveer for TFS# 460 
                if (DoValidationCheck() == false)
                    return;


                if (hidIMPORT_REQUEST_ID.Value == "")
                    if (AddImportRequest() < 1)
                        return;




                //Stores the name of the directory where file will get stored
                string strDirName = "";
                string strActualFileName = "";
                string strFileName = "";
                string strRoot;
                string strFilePath = "";
                string strFilePathToSave = "";

                int Index = 0;

                Index = txtATTACH_FILE_NAME.PostedFile.FileName.LastIndexOf("\\");
                if (Index >= 0)
                    strActualFileName = txtATTACH_FILE_NAME.PostedFile.FileName.Substring(Index + 1);
                else
                    strActualFileName = txtATTACH_FILE_NAME.PostedFile.FileName;


                string Import_Request_ID = hidIMPORT_REQUEST_ID.Value;

                string FileFirstPart = strActualFileName.Substring(0, 9)+"_";
                
                string SUSEP_LOB_CODE = String.Empty;
                int COLUMN_TYPE = int.Parse(cmbIMPORT_FILE_TYPE.SelectedValue);

                switch (int.Parse(cmbIMPORT_FILE_TYPE.SelectedValue))
                {
                    case 14936: strFileName = Import_Request_ID + "_CUSTOMER.xlsx"; break;
                    case 14937: strFileName = Import_Request_ID + "_COAPP.xlsx"; break;
                    case 14938: strFileName = Import_Request_ID + "_CONTACT.xlsx"; break;
                    case 14939: strFileName = Import_Request_ID + "_POLICY.xlsx"; break;
                    case 14940: strFileName = Import_Request_ID + "_REMUNARATION.xlsx"; break;
                    case 14941: strFileName = Import_Request_ID + "_COINSURANCE.xlsx"; break;
                    case 14942: strFileName = Import_Request_ID + "_REINSURANCE.xlsx"; break;
                    case 14960: strFileName = Import_Request_ID + "_LOCATION.xlsx"; break;
                    case 14961: strFileName = Import_Request_ID + "_POLICY_COAPP.xlsx"; break;
                    case 14962: strFileName = Import_Request_ID + "_CLAUSES.xlsx"; break;
                    case 14963: strFileName = Import_Request_ID + "_POLICY_DISCOUNT.xlsx"; break;
                    // FOR POLICY RISK DETAILS
                    case 15008: strFileName = Import_Request_ID + "_POLICY_RISK_INFO.xlsx";
                        SUSEP_LOB_CODE = strActualFileName.Substring(0, 4);
                        break;

                    case 14966: strFileName = Import_Request_ID + "_POLICY_RISK_DISCOUNT.xlsx"; break;
                    case 14967: strFileName = Import_Request_ID + "_POLICY_BENEFICIARY.xlsx"; break;
                    case 14968: strFileName = Import_Request_ID + "_POLICY_COVERAGES.xlsx"; break;
                    case 14969: strFileName = Import_Request_ID + "_BILLING_INFO.xlsx"; break;


                    //FOR CLAIM ADDED BY SHIKHA
                    case 14943: strFileName = Import_Request_ID + "_CLAIM_NOTIFICATION.xlsx"; break;
                    case 14944: strFileName = Import_Request_ID + "PARTIES.xlsx"; break;
                    case 14997: strFileName = Import_Request_ID + "ACTIVITY.xlsx"; break;
                    case 14998: strFileName = Import_Request_ID + "CLAIM_PAYMENT.xlsx"; break;
                    case 14999: strFileName = Import_Request_ID + "VICTIM.xlsx"; break;
                    case 15000: strFileName = Import_Request_ID + "THIRD_PARTY_DAMAGE.xlsx"; break;
                    case 15001: strFileName = Import_Request_ID + "RISK_INFO.xlsx"; break;
                    case 15002: strFileName = Import_Request_ID + "PAYEE.xlsx"; break;
                    case 15003: strFileName = Import_Request_ID + "OCCURRENCE_DETAIL.xlsx"; break;
                    case 15004: strFileName = Import_Request_ID + "LITIGATION.xlsx"; break;
                    case 15005: strFileName = Import_Request_ID + "CLAIM_COVERAGES.xlsx"; break;
                    case 15006: strFileName = Import_Request_ID + "CLAIM_COINSURANCE.xlsx"; break;
                    case 15007: strFileName = Import_Request_ID + "CLAIM_RESERVE.xlsx"; break;
                }


                strFileName = FileFirstPart + strFileName;

                strRoot = System.Configuration.ConfigurationManager.AppSettings.Get("InitialLoadURL");

               
                Index = strRoot.LastIndexOf("/");

                string strFilePathFolder = strRoot.Substring(Index + 1);

                string strFolderName = DateTime.Now.Year.ToString();

                if (DateTime.Now.Month < 10)
                    strFolderName += "0" + DateTime.Now.Month.ToString();
                else
                    strFolderName += DateTime.Now.Month.ToString();

                if (DateTime.Now.Day < 10)
                    strFolderName += "0" + DateTime.Now.Day.ToString();
                else
                    strFolderName += DateTime.Now.Day.ToString();



                strDirName = strRoot + "/" + strFolderName;
                strDirName = Server.MapPath(strDirName);

                CreateDirStructure(strDirName);
  

              

                strFilePathToSave = strFolderName + "\\" + strFileName;

                strFilePath = strDirName + "\\" + strFileName;
                //copying the file
                txtATTACH_FILE_NAME.PostedFile.SaveAs(strFilePath);

                // strFilePath =strFilePathFolder+"\\"+ strFolderName + "\\" + strFileName;

                
                //start changes by praveer for TFS# 460 
                string laPath = strFilePath;
                xlBook = (Workbook)xl.Workbooks.Open(laPath, oMissing,
                  oMissing, oMissing, oMissing, oMissing, oMissing, oMissing
                 , oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

                 for (int i = 1; i <= xlBook.Worksheets.Count; i++)
                 { 
                     xlSheet = (Worksheet)xlBook.Worksheets.get_Item(i);
                     if (xlSheet.Name.ToUpper().Trim() == "PLAN1")
                     {
                          flag = true;
                          break;
                     }
                   
                 }
                 if (flag == false)
                 {
                     xlSheet = (Worksheet)xlBook.Worksheets.get_Item(1);
                     xlSheet.Name = "Plan1";
                 }
                 xlBook.Save();
                // To check columns mismatch --Added By Pradeep 27-10-2011
                //Loop count worksheets 
                 bool ColumnMismatch = false;
                 int retValue = 0;
                 String ColumnsName = String.Empty;   
                 for (int i = 1; i <= xlBook.Worksheets.Count; i++)
                 {
                     xlSheet = (Worksheet)xlBook.Worksheets.get_Item(i);
                     ArrayList LayOutColumnNames = new ArrayList();
                     LayOutColumnNames= GetLayOutCoumns(COLUMN_TYPE,SUSEP_LOB_CODE);

                     if (xlSheet.Name.ToUpper().Trim() == "PLAN1")
                     {
                         Range range = xlSheet.UsedRange;
                         //Both Layout columns should be equal
                         if (range.Columns.Count > LayOutColumnNames.Count)
                         {
                             ColumnMismatch = true;
                             retValue = -1;
                         }//if (range.Columns.Count > LayOutColumnNames.Count)
                         else if(range.Columns.Count < LayOutColumnNames.Count)
                         {
                             ColumnMismatch = true;
                             retValue = -2;
                         }//else if(range.Columns.Count < LayOutColumnNames.Count)
                         else if (range.Columns.Count == LayOutColumnNames.Count)
                         {
                             for (int counter = 1; counter <= range.Columns.Count; counter++)
                             {
                                 string Temp = ((Range)range.Cells[1, counter]).Value;
                                 if (!LayOutColumnNames.Contains(Temp))
                                 {
                                     ColumnMismatch = true;
                                     ColumnsName = Temp;
                                     retValue = -3;
                                     break;

                                 }//if (!LayOutColumnNames.Contains(Temp) )
                             }//for (int counter = 1; counter <=  range.Columns.Count; counter++)
                         }//else if (range.Columns.Count == LayOutColumnNames.Count)

                         if (retValue < 0)
                             break;
                         
                     }//if (xlSheet.Name.ToUpper().Trim() ==  "PLAN1")
                 }//for (int i = 1; i < xlBook.Worksheets.Count;  i++)
                 
                if(ColumnMismatch==false)
                    AddImportRequestFile(strFileName, strFilePathToSave, strActualFileName);
               
                //Added till here
                xl.Application.Workbooks.Close();        
                xl.Quit();
                Marshal.ReleaseComObject(xl);
                Marshal.ReleaseComObject(xlBook);
                xl = null;
                xlSheet = null;
                xlBook = null;
                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);
                // comment due the reason, this try to kill whole excel process
                //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                //{
                //    process.Kill();
                //    process.WaitForExit();
                //}
                //Added by pradeep 
                if (ColumnMismatch == true)
                {
                    if (File.Exists(strFilePath))
                    {
                        File.Delete(strFilePath);
                    }//if (File.Exists(strFilePath))
                }//if (ColumnMismatch == true)
                if (retValue == -1)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("2092");//Uploaded file has more columns than defined standard format.
                    hidFormSaved.Value = "2";
                }// if (retValue == -1)
                else if (retValue == -2)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("2093");//Uploaded file has less columns than defined standard format.
                    hidFormSaved.Value = "2";
                }//else if (retValue == -2)
                else if (retValue == -3)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text =  "'"+ ColumnsName +"'" + " - " + ClsMessages.FetchGeneralMessage("2091");//Incompatibilidade de coluna encontrado.//Column Mismatch found. 
                    hidFormSaved.Value = "2";
                }//else if (retValue == -3)

                //Added till here 
                //END changes by praveer for TFS# 460 
                BindFileType();


            }
            catch (Exception objExp)
            {
                lblMessage.Text = objExp.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                hidFormSaved.Value = "2";
            }
        }

        //Added by Pradeep Kushwaha on 27-10-2011
        private ArrayList GetLayOutCoumns(Int32 COLUMN_TYPE, String SUSEP_LOB_CODE)
        {
            DataSet dsLayOutColumns = null;
            ArrayList arrLayOutColumns = new ArrayList();
            dsLayOutColumns = objImportRequest.GetInialLoadLayOutCoumnsDetails(COLUMN_TYPE, SUSEP_LOB_CODE);
 
            foreach (DataRow row in dsLayOutColumns.Tables[0].Rows)
            {
                arrLayOutColumns.Add(row["COLUMN_NAME"].ToString());
            }//foreach (DataRow row in dsLayOutColumns.Tables[0].Rows)
            return arrLayOutColumns;
        }//private ArrayList GetLayOutCoumns(Int32 COLUMN_TYPE, String SUSEP_LOB_CODE)

        private string CreateDirStructure(string DirName)
        {

            //   string strRoot, strDirName = "";
            try
            {
  
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
                intRetval = objImportRequest.AddInialLoadImportRequest(objAcceptedCOILoadInfo);


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

                int ImportRequestID = 0;
                //int ImportRequestFileID = 0;
                if (hidIMPORT_REQUEST_ID.Value != "")
                    ImportRequestID = int.Parse(hidIMPORT_REQUEST_ID.Value);


                actualFileName = actualFileName.ToLower().Replace(".txt", "");

                objAcceptedCOILoadInfo.IMPORT_FILE_NAME.CurrentValue = fileName;
                objAcceptedCOILoadInfo.DISPLAY_FILE_NAME.CurrentValue = actualFileName;
                objAcceptedCOILoadInfo.IMPORT_FILE_PATH.CurrentValue = filePath;
                objAcceptedCOILoadInfo.REQUEST_DESC.CurrentValue = txtREQUEST_DESC.Text;
                objAcceptedCOILoadInfo.IMPORT_FILE_GROUP_TYPE.CurrentValue = int.Parse(cmbGROUP_FILE_TYPE.SelectedValue);
                objAcceptedCOILoadInfo.IMPORT_FILE_TYPE.CurrentValue = int.Parse(cmbIMPORT_FILE_TYPE.SelectedValue);
                objAcceptedCOILoadInfo.IMPORT_REQUEST_ID.CurrentValue = ImportRequestID;
                //objAcceptedCOILoadInfo.IMPORT_REQUEST_FILE_ID.CurrentValue = ImportRequestFileID;
                objAcceptedCOILoadInfo.FILE_IMPORTED_DATE.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                objAcceptedCOILoadInfo.FILE_IMPORTED_BY.CurrentValue = int.Parse(GetUserId());

                intRetval = objImportRequest.AddInialLoadImportRequestFile(objAcceptedCOILoadInfo);


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
            


        }

        private void LoadGridData()
        {
            ManageScreen();

            if (hidIMPORT_REQUEST_ID.Value == "")
            {
                return;
            }

            //changes by praveer for TFS# 460 
           System.Data.DataTable dt = objImportRequest.GetInialLoadImportRequestDetails(int.Parse(hidIMPORT_REQUEST_ID.Value), ClsCommon.BL_LANG_ID);




            if (dt != null && dt.Rows.Count > 0)
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




            if ((hidREQUEST_STATUS.Value) == "NSTART" || (hidREQUEST_STATUS.Value) == "0")
            {
                btnStartProcess.Visible = true;
                btnAddNew.Visible = true;
                btnChecksumSummary.Visible = false;
                btnCommitPolicyList.Visible = false;
            }
            else if ((hidREQUEST_STATUS.Value) == "COMP" || (hidREQUEST_STATUS.Value) == "FAIL")
            {
                btnStartProcess.Visible = false;
                btnAddNew.Visible = false;
                btnChecksumSummary.Visible = false;
                btnCommitPolicyList.Visible = true;
            }

            else if ((hidREQUEST_STATUS.Value) == "INQUEUE")
            {
                btnStartProcess.Visible = false;
                btnAddNew.Visible = false;
                btnChecksumSummary.Visible = false;
                btnCommitPolicyList.Visible = false;
            }
            else if ((hidREQUEST_STATUS.Value) == "INPRG")
            {
                btnStartProcess.Visible = false;
                btnAddNew.Visible = false;
                btnCommitPolicyList.Visible = false;
                btnChecksumSummary.Visible = false;
            }
            else if ((hidREQUEST_STATUS.Value) == "WTCHS")
            {
                btnStartProcess.Visible = false;
                btnAddNew.Visible = false;
                btnChecksumSummary.Visible = true;
                btnCommitPolicyList.Visible = false;
            }
            else if ((hidREQUEST_STATUS.Value) == "IMQUEUE" || (hidREQUEST_STATUS.Value) == "IMPRG") // QUEUED FOR IMPORTING
            {
                btnStartProcess.Visible = false;
                btnAddNew.Visible = false;
                btnChecksumSummary.Visible = false;
                btnCommitPolicyList.Visible = false;
            }
            //btnViewDetails.Visible = false;



        }

        private void ManageScreen()
        {

            if (hidIMPORT_REQUEST_ID.Value != "" && int.Parse(hidIMPORT_REQUEST_ID.Value) > 0)
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
            string ViewDetails_Disable = "../../cmsweb/images/DisableViewDetails.JPG";
            string ViewDetails_Enable = "../../cmsweb/images/ViewReserveDetails.jpg";

            if (e.Row.RowType != DataControlRowType.EmptyDataRow)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[9].Visible = false; // request status
                e.Row.Cells[10].Visible = false; //FILE TYPE

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[9].Text != "NSTART")
                {
                    ImageButton BtnDeleteRecord = e.Row.FindControl("BtnDeleteFile") as ImageButton;
                    if (BtnDeleteRecord != null)
                    {
                        BtnDeleteRecord.ImageUrl = Delete_Disable;
                        BtnDeleteRecord.Enabled = false;
                    }
                }

                ImageButton BtnViewDetails = e.Row.FindControl("BtnViewDetails") as ImageButton;
                if (e.Row.Cells[9].Text == "COMP" || e.Row.Cells[9].Text == "WTCHS")
                {
                    if (BtnViewDetails != null)
                    {
                        BtnViewDetails.ImageUrl = ViewDetails_Enable;
                        BtnViewDetails.Enabled = true;
                    }

                }
                else
                {
                    BtnViewDetails.ImageUrl = ViewDetails_Disable;
                    BtnViewDetails.Enabled = false;
                }
            }

        }

        private void BindFileType()
        {
            int IMPORT_REQT_FILE_ID = 0;
            int FileGrouptype=0;

            if(cmbGROUP_FILE_TYPE.SelectedIndex>-1 && cmbGROUP_FILE_TYPE.SelectedValue!="")
            {
               FileGrouptype = int.Parse(cmbGROUP_FILE_TYPE.SelectedValue);

            }



            cmbIMPORT_FILE_TYPE.Items.Clear();

            if (hidIMPORT_REQUEST_ID.Value != "")
                IMPORT_REQT_FILE_ID = int.Parse(hidIMPORT_REQUEST_ID.Value);

            ClsAcceptedCOILoadInfo objImportRequest = new ClsAcceptedCOILoadInfo();
            int lang_id = int.Parse(GetLanguageID());

            DataSet ds = objImportRequest.GetInitialLoadFileTypes(lang_id, IMPORT_REQT_FILE_ID, FileGrouptype);

            if (cmbGROUP_FILE_TYPE.Items.Count <= 0)
            {
                cmbGROUP_FILE_TYPE.DataSource = ds.Tables[0];
                cmbGROUP_FILE_TYPE.DataTextField = "LOOKUP_DESC";
                cmbGROUP_FILE_TYPE.DataValueField = "LOOKUP_ID";
                cmbGROUP_FILE_TYPE.DataBind();
               
            }
            
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                cmbIMPORT_FILE_TYPE.DataSource = ds.Tables[1];
                cmbIMPORT_FILE_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
                cmbIMPORT_FILE_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
                cmbIMPORT_FILE_TYPE.DataBind();
                cmbIMPORT_FILE_TYPE.Items.Insert(0, "");
            }
        }
        protected void grdImportRequestFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFile")
            {
                ClsAcceptedCOILoadInfo objAcceptedCOI = new ClsAcceptedCOILoadInfo();
                int Index = int.Parse(e.CommandArgument.ToString());
                objAcceptedCOI.IMPORT_REQUEST_ID.CurrentValue = int.Parse(grdImportRequestFiles.Rows[Index].Cells[0].Text);
                objAcceptedCOI.IMPORT_REQUEST_FILE_ID.CurrentValue = int.Parse(grdImportRequestFiles.Rows[Index].Cells[1].Text);

                if (objImportRequest.DeleteInialLoadFile(objAcceptedCOI) > 0)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "4");
                    hidFormSaved.Value = "2";

                    LoadGridData();
                    BindFileType();
                }
            }

            if (e.CommandName == "ViewDetails")
            {
             //   ClsAcceptedCOILoadInfo objAcceptedCOI = new ClsAcceptedCOILoadInfo();
                int Index = int.Parse(e.CommandArgument.ToString());
               string IMPORT_REQUEST_ID      = grdImportRequestFiles.Rows[Index].Cells[0].Text;
               string FILE_TYPE              = grdImportRequestFiles.Rows[Index].Cells[10].Text;
               string FILE_NAME              = grdImportRequestFiles.Rows[Index].Cells[4].Text;
               string Lob_Id                 = grdImportRequestFiles.Rows[Index].Cells[2].Text; 
               hidIMPORT_FILE_TYPE.Value = FILE_TYPE;
               hidIMPORT_FILE_TYPE_NAME.Value = FILE_NAME;
               hidLobId.Value = Lob_Id.Substring(0,4);
               ClientScript.RegisterStartupScript(this.GetType(), "viewdetail", "<script>ViewDetails();</script>");
            }
        }

        protected void cmbGROUP_FILE_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGROUP_FILE_TYPE.SelectedValue != "")
            {
                BindFileType();
            }
        }
    }


}
