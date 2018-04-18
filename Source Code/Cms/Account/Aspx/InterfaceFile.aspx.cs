using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cms.CmsWeb;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using System.IO;
using System.Collections;
using System.Data;
using Cms.DataLayer;
using System.Text;
using System.Configuration;




namespace Cms.Account.Aspx
{
    public partial class InterfaceFile : Cms.Account.AccountBase
    {
        //string colorScheme = "";
       
        //public string IDomain = ConfigurationSettings.AppSettings["Domain"].ToString();
        //public string IUserName = ConfigurationSettings.AppSettings["UserId"].ToString();
        //public string IPassWd = ConfigurationSettings.AppSettings["Password"].ToString();

        string strUserName = ConfigurationManager.AppSettings["PagnetIUserName"];
        string strPassWd = ConfigurationManager.AppSettings["PagnetIPassWd"];
        string strDomain = ConfigurationManager.AppSettings["PagnetIDomain"];
        protected string strTemp;
        //public string Impersonate_Inactive = ConfigurationSettings.AppSettings["ImpersonateInactive"].ToString();

        #region CONTROLS METHODS
        protected void Page_Load(object sender, EventArgs e)
        {

            //btnPagnet.CmsButtonClass = CmsButtonType.Write;
            //btnPagnet.PermissionString = gstrSecurityXML;

            //btnMelEvent.CmsButtonClass = CmsButtonType.Write;
            //btnMelEvent.PermissionString = gstrSecurityXML;
            //txtDATE.Attributes.Add("ReadOnly", "ReadOnly");
            base.ScreenId = "534";
            hlkDATE.Attributes.Add("OnClick", "fPopCalendar(document.formInterfaceFile.txtDATE,document.formInterfaceFile.txtDATE)");
            lblErr.Visible = false;
            strTemp = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2009");
            
           
            //revRISK_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
           
            setErrorMessages();
            if (!Page.IsPostBack)
            {
                ddlFilelist.Items.Insert(0, strTemp);
                Bind_Notification();
                setCaption();
                ddlFilelist.Enabled = false;
               
            }
        }

        protected void btnPagnet_Click(object sender, EventArgs e)
        {
            try
            {
                string strPath = "";
                //string[] arr = null;
                // ArrayList arrlst = new ArrayList();


                //object value = (ArrayList)ViewState["arrFileFullPath"];
                //arrlst = (ArrayList)value;

                if (txtDATE.Text.Trim().Length > 0)
                {
                    if (ddl.SelectedIndex > 0 || ddlFilelist.Enabled == true)
                    {
                        if (ddlFilelist.SelectedIndex > 0)
                        {
                            //strPath =  arr[ddlFilelist.SelectedIndex];
                            strPath = ddlFilelist.SelectedValue;
                            ReadFile(strPath);
                        }
                        else
                        {

                            lblErr.Visible = true;
                            lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2015"); 
                            lblErr.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {

                        lblErr.Visible = true;
                        lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2016"); 
                        lblErr.ForeColor = System.Drawing.Color.Red;
                    }

                   

                }
                else
                {
                    lblErr.Visible = true;
                    lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1802");// "Please enter a date !";
                    lblErr.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch
            {

            }
            
            
           
  
        }
       
        protected void btnMelEvent_Click(object sender, EventArgs e)
        {
            try
            {
                //string strPath = "";               
                //string dd = "";
                //string mm = "";
                //string yyyy = "";              
                //string AppPath = "";

               

               // ConvertToDate(txtExpirationStartDate.Text)
                if (txtDATE.Text.Trim().Length > 0)
                {   
                    /////COMMENT FOR GENERATE TXT AS SERVICE AND RESTRICT TO READ TEXT FILE///////
                    //////////////////////////////////////////////////////////////////////////////
                    
                    //Check if file not exists 
                    /*AppPath=  HttpContext.Current.Request.PhysicalApplicationPath.ToString().Trim();
                    if (AppPath.EndsWith("\\Cms\\"))
                    {
                        AppPath = AppPath.Replace("\\Cms\\", "");

                    }
                    strPath = AppPath + System.Configuration.ConfigurationManager.AppSettings.Get("MelEventInterfacePath").ToString();
                   // strPath = Server.MapPath(strRoot); 
                  
                    dd = Convert.ToDateTime(txtDATE.Text).Day.ToString().PadLeft(2, '0');
                    mm = Convert.ToDateTime(txtDATE.Text).Month.ToString().PadLeft(2, '0');
                    yyyy = Convert.ToDateTime(txtDATE.Text).Year.ToString();
                    strPath = strPath + "MelEventos_" + dd + mm + yyyy + ".txt";

                    ReadFile(strPath);*/

                    string path="";
                    path = System.Configuration.ConfigurationManager.AppSettings["MelEventInterfacePath"];
                    //objClsMelEvent.ConnStr = ConfigurationSettings.AppSettings["conString"].ToString();
                    if (CreateFolder(path))
                    {
                        //WriteEventsLog("MEL EVENTS FOLDER CREATED AND GO FOR Proc_MelEventos_Issuance TEXT GENERATION.  ");
                        try //1
                        {
                            //objClsMelEvent.GenerateTxt("Proc_MelEventos_Issuance", "MelEventos", flagDelFile);
                            //flagDelFile = 1;
                            if (GenerateTxt("Proc_MelEventos_Issuance", "Ebix", txtDATE.Text) == true)
                            {
                                lblErr.Visible = true;
                                lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2012");//"Eventos Mel arquivo criado!!";
                                lblErr.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                lblErr.Visible = true;
                                lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2013"); //"O arquivo não pode criar a partir da data. Não há dados disponíveis!!";
                                lblErr.ForeColor = System.Drawing.Color.Red;

                            }

                        }
                        catch (Exception ex)
                        {
                            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                        }
                    }
                    else
                    {
                        lblErr.Visible = true;
                        lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1804");// "User imporsonation failed!!";
                        lblErr.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else
                {
                    lblErr.Visible = true;
                    lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1802");//"Please enter a date !";
                    lblErr.ForeColor = System.Drawing.Color.Red;
                }
                ddlFilelist.Enabled = false;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblErr.Visible = false;
            }

        }
        
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPath = "";
            string[] filePaths = null;
            string FileDate="";
            string SearchDate="";
            //string[] arrFileList = null;
            //string[] arrFileFullPath = null;
            ArrayList arrFileList = new ArrayList();
            ArrayList arrFileFullPath = new ArrayList();

             Hashtable hsFileList = new Hashtable();

             if (!String.IsNullOrEmpty(txtDATE.Text.Trim()))
             {

                 if (ddl.SelectedValue == "14898")
                 {
                     strPath = System.Configuration.ConfigurationManager.AppSettings["FilePathCommission"];

                 }
                 else if (ddl.SelectedValue == "14899")
                 {
                     strPath = System.Configuration.ConfigurationManager.AppSettings["FilePathCustRefund"];

                 }
                 else if (ddl.SelectedValue == "14900")
                 {
                     strPath = System.Configuration.ConfigurationManager.AppSettings["FilePathClaimExp"];

                 }

                 else if (ddl.SelectedValue == "14901")
                 {
                     strPath = System.Configuration.ConfigurationManager.AppSettings["FilePathClaimInd"];

                 }

                 else if (ddl.SelectedValue == "14902")
                 {
                     strPath = System.Configuration.ConfigurationManager.AppSettings["FilePathCORI"];
                 }
                 else
                 {

                 }

                 //string strUserName = ConfigurationSettings.AppSettings["IUserName"];
                 //string strPassWd = ConfigurationSettings.AppSettings["IPassWd"];
                 //string strDomain = ConfigurationSettings.AppSettings["IDomain"];

                 //Beginigng the impersonation 
                 Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
                 if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                 {
                     if (Directory.Exists(strPath))
                     {
                         filePaths = Directory.GetFiles(strPath, "*.txt");

                         //int j = 0;
                         for (int i = 0; i < filePaths.Length; i++)
                         {
                             FileDate = File.GetCreationTime(filePaths[i]).ToShortDateString();
                             SearchDate = Convert.ToDateTime(txtDATE.Text).ToShortDateString();

                             if (FileDate == SearchDate)
                             {
                                 hsFileList.Add(Path.GetFileName(filePaths[i].ToString()), filePaths[i]);
                             }
                         }

                     }
                 }



                 objAttachment.endImpersonation();

                 ViewState["arrFileFullPath"] = arrFileFullPath;
                 if (ddl.SelectedIndex > 0)
                 {
                     ddlFilelist.Enabled = true;
                 }
                 else
                 {
                     ddlFilelist.Enabled = false;
                 }

                 ddlFilelist.DataSource = hsFileList;
                 ddlFilelist.DataValueField = "Value";
                 ddlFilelist.DataTextField = "Key";
                 ddlFilelist.DataBind();
                 ddlFilelist.Items.Insert(0, strTemp);
                 ddlFilelist.SelectedIndex = 0;
                 objAttachment = null; ;
             }
             else
             {
                 return;
             }

        }
        #endregion

        #region Methods
        public void InsertLog(string PROCESS_TYPE, string ACTIVITY_DESCRIPTION, DateTime START_DATETIME, DateTime END_DATETIME, string STATUS, string ADDITIONAL_INFO)
        {
            int ResultSet = 0;           
            string strStoredProcP = "PROC_PAGNET_PROCESS_LOG";
            Cms.DataLayer.DataWrapper objDataWrapperP = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            //objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapperP.AddParameter("@PROCESS_TYPE", PROCESS_TYPE);
                objDataWrapperP.AddParameter("@ACTIVITY_DESCRIPTION", ACTIVITY_DESCRIPTION);
                objDataWrapperP.AddParameter("@START_DATETIME", START_DATETIME);
                objDataWrapperP.AddParameter("@END_DATETIME", END_DATETIME);
                objDataWrapperP.AddParameter("@STATUS", STATUS);
                objDataWrapperP.AddParameter("@ADDITIONAL_INFO", ADDITIONAL_INFO);
                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                //objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                //ResultSet = 1;
            }
            catch (Exception ex)
            {
                //objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw ex;
            }
            finally
            {
                objDataWrapperP.Dispose();
            }


        }


        private Boolean CreateFolder(string path)
        {
             Cms.BusinessLayer.BlCommon.ClsAttachment  objAttachment = new  Cms.BusinessLayer.BlCommon.ClsAttachment();

            try
            {

                if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                {

                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }
                    objAttachment.endImpersonation();
                    objAttachment = null;
                    return true;
                }
                else
                {
                    InsertLog("MelEventos", "Impersonation Failed (On the fly)", DateTime.Now, DateTime.Now, "Fail", path); 
                    objAttachment = null;
                    return false;
                }


            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
        }
        private void ReadFile(string filePath)
        {
            try
            {

                //System.IO.FileStream fs = null;
                string sGenName = System.IO.Path.GetFileName(filePath);

                //fs = System.IO.File.Open(Server.MapPath("TextFiles/" + sFileName + ".txt"), System.IO.FileMode.Open);
                string strUserName = System.Configuration.ConfigurationManager.AppSettings.Get("PagnetIUserName");
                string strPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("PagnetIPassWd");
                string strDomain = System.Configuration.ConfigurationManager.AppSettings.Get("PagnetIDomain");

                //Beginigng the impersonation 
                Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
                if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        //fs = System.IO.File.Open(filePath, System.IO.FileMode.Open);
                        //byte[] btFile = new byte[fs.Length];
                        //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                        //fs.Close();
                        Response.AddHeader("Content-disposition", "attachment; filename=" + sGenName);
                        Response.ContentType = "application/octet-stream";
                        Response.Clear();
                        lblErr.Visible = false;
                        //Response.BinaryWrite(btFile);
                        Response.WriteFile(filePath);
                        Response.End();

                        objAttachment.endImpersonation();
                        objAttachment = null;

                    }
                    else
                    {
                        lblErr.Visible = true;
                        lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1803");// "File not exists!!";
                        lblErr.ForeColor = System.Drawing.Color.Red;
                    }

                }

                else
                {
                    //Impersation failed				
                    lblErr.Visible = true;
                    lblErr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1804");// "User imporsonation failed!!";
                    lblErr.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

            }
        }
        private void setCaption()
        {
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1796");
            capDATE.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1797");
            btnPagnet.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1798");
            btnMelEvent.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1799");
        }
        private void setErrorMessages()
        {
            revDATE.ValidationExpression = aRegExpDate;
            revDATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1800"); //"Please enter a valid date!";
            rfvDATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1801");//"Please enter date!";
        }
        private void Bind_Notification()
        {
            string strSelect = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2014"); 
            ddl.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%BRCOM");
            ddl.DataTextField = "LookupDesc";
            ddl.DataValueField = "LookupID";
            ddl.DataBind();
            ddl.Items.Insert(0, strSelect);
            ddl.SelectedIndex = 0;
        }
        public Boolean GenerateTxt(string procName, string txtFileName,string txtDate)
        {
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            DataSet ds = new DataSet();
            
            string strFullLengthString = "";
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@INCEPTION_DATE", String.Format("{0:MM/dd/yyyy}", ConvertToDate(txtDate)));
            ds = objDataWrapper.ExecuteDataSet(procName); //getData(procName);
            StringBuilder strBuild = new StringBuilder();
            string strTemp = "";
            //string strFileLen = "";
            int i = 0;
            int j = 0;

            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int tableCount = 0; tableCount < ds.Tables.Count; tableCount++)
                    {

                        if (ds.Tables[tableCount].Rows.Count > 0)
                        {
                            InsertLog("MelEventos", "Records are available to create Meleventos File (On the fly). ", DateTime.Now, DateTime.Now, "Success", ""); 
                            foreach (DataRow dr in ds.Tables[tableCount].Rows)
                            {
                                
                                strTemp = "";
                                strFullLengthString = "";
                                if (i == 0)
                                {
                                    strFullLengthString = "|" + dr["LAYOUT_ID"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["COMPANY_CODE"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["YEAR_MONTH"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["BATCH_CODE"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["SEQUENTIAL"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["EVENT_CODE"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["POSTING_DATE"].ToString().Trim() + "|";
                                    strFullLengthString = strFullLengthString + strTemp;
                                }
                                else
                                {
                                    strFullLengthString = "|" + dr["LAYOUT_ID"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["COMPANY_CODE"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["YEAR_MONTH"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["BATCH_CODE"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["SEQUENTIAL"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["EVENT_CODE"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr["POSTING_DATE"].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[7].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[8].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[9].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[10].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[11].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[12].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[13].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[14].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[15].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[16].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[17].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[18].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[19].ToString().Trim();
                                    strFullLengthString = strFullLengthString + strTemp;

                                    strTemp = "|" + dr[20].ToString().Trim() + "|";
                                    strFullLengthString = strFullLengthString + strTemp;

                                }

                                i = i + 1;
                                if (i == ds.Tables[tableCount].Rows.Count)
                                {

                                    strBuild.Append(strFullLengthString);

                                }
                                else
                                {
                                    strBuild.Append(strFullLengthString);
                                    strBuild.Append(Environment.NewLine);
                                }
                            }//foreach close
                        }//if close rows count

                        i = 0;
                        j = j + 1;
                        if (j < ds.Tables.Count)
                        {
                            strBuild.Append(Environment.NewLine);
                            strBuild.Append("\r\n");

                        }

                    } //for loop on tables

                   

                    string dd = ConvertToDate(txtDATE.Text).Day.ToString();
                    string mon = ConvertToDate(txtDATE.Text).Month.ToString();
                    string yy = ConvertToDate(txtDATE.Text).Year.ToString();

                    string FileDate = "";
                    string SearchDate = "";
                    dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                    mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();

                    string strTemp1 = "";
                    string date = "";
                    string strPath = ConfigurationManager.AppSettings["MelEventInterfacePath"];
                    string[] filePaths = null;
                    int flagFileExists = 0;
                    //string[] currentDateFiles = null;                    
                        
                    InsertLog("MelEventos", "Path is going to impersonate (On the fly): " + strPath, DateTime.Now, DateTime.Now, "Success", strPath); 
                    Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
                 if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                    {
                        if (Directory.Exists(strPath))
                        {
                                filePaths = Directory.GetFiles(strPath, "*.txt");

                                //int j = 0;
                                double oldFileName = 0;
                                double newFileName = 0;
                                string oldString = "";
                                string newString = "";

                                for (int k = 0; k < filePaths.Length; k++)
                                {
                                    //DirectoryInfo obj = new DirectoryInfo("");

                                    //FileDate = ConvertToDate(File.GetCreationTime(filePaths[k]).ToString()).ToShortDateString();
                                    FileDate = Path.GetFileNameWithoutExtension(filePaths[k]);
                                    FileDate = FileDate.Substring(FileDate.IndexOf('_') + 1, 8);



                                    SearchDate = ConvertToDate(txtDATE.Text).Day.ToString().PadLeft(2, '0');
                                    SearchDate = SearchDate + ConvertToDate(txtDATE.Text).Month.ToString().PadLeft(2, '0');
                                    SearchDate = SearchDate + ConvertToDate(txtDATE.Text).Year.ToString();
                                    //SearchDate = DateTime.Now.ToShortDateString();

                                    if (FileDate == SearchDate)
                                    {
                                        flagFileExists = 1;
                                        if (flagFileExists == 1)
                                        {
                                            strTemp1 = Path.GetFileNameWithoutExtension(filePaths[k]);

                                            strTemp1 = strTemp1.Substring(strTemp1.IndexOf('_') + 1, (strTemp1.Length - 1) - (strTemp1.IndexOf('_')));



                                            newFileName = Convert.ToDouble(strTemp1);
                                            newString = strTemp1;
                                            if (newFileName > oldFileName)
                                            {
                                                oldFileName = newFileName;
                                                oldString = newString;
                                            }

                                        }

                                    }
                                }//close for loop



                                if (flagFileExists == 0)
                                {
                                    date = dd + mon + yy + "0";

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt", true, Encoding.ASCII))
                                    {
                                        
                                        if (strBuild.Length > 0)
                                        {
                                            file.Write(strBuild);
                                            InsertLog("MelEventos", "Meleventos File (On the fly) is created.", DateTime.Now, DateTime.Now, "Success", ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt"); 
                                        }
                                        else
                                        {
                                            InsertLog("MelEventos", "Meleventos File (On the fly) is not created.", DateTime.Now, DateTime.Now, "Success", ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt"); 
                                            //strReturn = "There is no record to insert";
                                            //return strReturn;
                                        }
                                    }

                                    //return true;

                                  }
                                else
                                {

                                    //date = dd + mon + yy + "0";
                                    int counter = 0;
                                    counter = Convert.ToInt32(oldString.Substring(8, oldString.ToString().Length - 8));
                                    //Convert.ToInt32(oldFileName.ToString().Substring(8,oldFileName.ToString().Length-1));
                                    counter = counter + 1;

                                    date = dd + mon + yy + counter.ToString();

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt", true, Encoding.ASCII))
                                    {
                                        if (strBuild.Length > 0)
                                        {
                                            file.Write(strBuild);
                                            InsertLog("MelEventos", "Meleventos File (On the fly) is created.", DateTime.Now, DateTime.Now, "Success", ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt"); 
                                        }
                                        else
                                        {
                                            InsertLog("MelEventos", "Meleventos File (On the fly) is not created.", DateTime.Now, DateTime.Now, "Success", ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt"); 
                                            //strReturn = "There is no record to insert";
                                            //return strReturn;
                                        }
                                    }

                                }
                        }
                        else
                        {
                            InsertLog("MelEventos", "File Path/Access to write (On the fly) problem", DateTime.Now, DateTime.Now, "Fail", ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt"); 
                            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(new Exception("Interface File Log:Impersonation Failed or Directory access denied."));
                            return false; //directory not exist
                        }
                    
                        objAttachment.endImpersonation();
                        objAttachment = null;
                        return true;

                    }//Impersenation if close
                    
                    else
                    {
                        InsertLog("MelEventos", "Impersonation Failed (On the fly)", DateTime.Now, DateTime.Now, "Fail", ConfigurationManager.AppSettings["MelEventInterfacePath"] + txtFileName + "_" + date + ".txt"); 
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(new Exception("Interface File Log:Impersonation Failed"));
                        objAttachment = null;
                        return false;
                    }
                  


                   // return true;
                }// 
                else //else of top if
                {
                    InsertLog("MelEventos", "Data not exists (On the fly)", DateTime.Now, DateTime.Now, "Fail", ""); 
                    return false;
                }
            }

            catch (Exception ex)
            {
                InsertLog("MelEventos", "Error occured (On the fly)", DateTime.Now, DateTime.Now, "Fail", ""); 
                throw ex;
            }
        }
        #endregion
    }
}
