using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using System.Net;
using Cms.DataLayer;
using System.Web;
using System.Collections;
using System.Management;
using System.Security.Principal;
using System.Management.Instrumentation;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Xml.XPath;
using System.Xml;
using System.Xml.Xsl;




namespace PagnetService
{
    public partial class PagnetService : ServiceBase
    {
        #region GLOBAL VARIABLES

        private System.Timers.Timer timer;
        private System.Timers.Timer timer_for_day_flag;
        private System.Timers.Timer timer_imp;
        //int flagDelFile = 0;
        public Boolean Once_In_A_Day_Flag_Exp = false;
        public Boolean Once_In_A_Day_Flag_Imp = false;
        WindowsImpersonationContext impersonationContext;
        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int LogonUser(String lpszUserName, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public extern static int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);
        // Declare the logon types as constants
        const int LOGON32_LOGON_INTERACTIVE = 2;
        const int LOGON32_LOGON_NETWORK = 3;
        // Declare the logon providers as constants
        const int LOGON32_PROVIDER_DEFAULT = 0;


        public string IDomain = ConfigurationSettings.AppSettings["Domain"].ToString();
        public string IUserName = ConfigurationSettings.AppSettings["UserId"].ToString();
        public string IPassWd = ConfigurationSettings.AppSettings["Password"].ToString();
        public string Impersonate_Inactive = ConfigurationSettings.AppSettings["ImpersonateInactive"].ToString();


        private String LogfileName = System.Configuration.ConfigurationSettings.AppSettings["EventLogFileName"].ToString().Trim();
        private String Logfilepath = System.AppDomain.CurrentDomain.BaseDirectory;
           
        //Boolean ProcessCompleteFlag = false;
        clsUtility obj = new clsUtility();
        ClsMelEvent objClsMelEvent = new ClsMelEvent();

        Boolean flagStart = false;

        #endregion

        #region CONSTRUCTOR
        public PagnetService()
        {
            InitializeComponent();
          
            if (CheckErrorLogFileExist(Logfilepath, LogfileName) == "")
            {
                CreateLogFile();
            }


        }
        #endregion

        #region SERVICE FUNCTION
        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            WriteEventsLog("Service started successfully " );

            InitializeTimer();
            timer.Enabled = true;
            timer_for_day_flag.Enabled = true;
            timer_imp.Enabled = true;

            //GenerateTxt();
            //ImportReturnedFile();          

        }

        protected override void OnStop()
        {

            timer.Enabled = false;
            timer_for_day_flag.Enabled = false;
            timer_imp.Enabled = false;

            WriteEventsLog("Service stopped successfully");

        }

        private void InitializeTimer()
        {
            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Interval = 60000  * Convert.ToDouble(System.Configuration.ConfigurationSettings.AppSettings["IntervalMinutes"]);
            timer.Elapsed += new ElapsedEventHandler(timer1_Elapsed);


            timer_for_day_flag = new System.Timers.Timer();
            timer_for_day_flag.AutoReset = true;
            timer_for_day_flag.Interval = 60000 * Convert.ToDouble(System.Configuration.ConfigurationSettings.AppSettings["IntervalMinutesDayFlag"]);
            timer_for_day_flag.Elapsed += new ElapsedEventHandler(timer_day_flag_Elapsed);

            timer_imp = new System.Timers.Timer();
            timer_imp.AutoReset = true;
            timer_imp.Interval = 60000 * Convert.ToDouble(System.Configuration.ConfigurationSettings.AppSettings["IntervalMinutesMelEvents"]);
            timer_imp.Elapsed += new ElapsedEventHandler(timer_imp_Elapsed);

            

        }

        //PAGNET EXPORT
        private void timer1_Elapsed(object sender, EventArgs e)
        {
            
            
             //string path="";
            if (flagStart == false)
            {
                flagStart = true;
                //int timeToExp = Convert.ToInt32(ConfigurationSettings.AppSettings["TimeToExport"].ToString());
                try
                {

                    #region PAGNET FILE GENERATION
                    //PagNet EXP(Generate txt)
                    // path = System.Configuration.ConfigurationSettings.AppSettings["FilePath"];
                    //if (CreateFolder(path))
                    //{
                    WriteEventsLog("CALLED PAGENET GenerateTxt(). ");
                    GenerateTxt();

                   
                    // }
                    #endregion

                    #region UPDATE STATUS IN PAGNET_EXPORT_FILES TABLE
                    WriteEventsLog("\r\n" + "Call UpdateFiles() for UPDATE STATUS IN PAGNET_EXPORT_FILES TABLE ");
                    try
                    {
                        UpdateFiles(ConfigurationSettings.AppSettings["FilePathCommission"].ToString().ToUpper());
                    }
                    catch (Exception ex)
                    {
                        WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                    }
                    try
                    {
                        UpdateFiles(ConfigurationSettings.AppSettings["FilePathClaimExp"].ToString().ToUpper());
                    }
                    catch (Exception ex)
                    {
                        WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                    }
                    try
                    {
                        UpdateFiles(ConfigurationSettings.AppSettings["FilePathClaimInd"].ToString().ToUpper());
                    }
                    catch (Exception ex)
                    {
                        WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                    }
                    try
                    {
                        UpdateFiles(ConfigurationSettings.AppSettings["FilePathCustRefund"].ToString().ToUpper());
                    }
                    catch (Exception ex)
                    {
                        WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                    }
                    try
                    {
                        UpdateFiles(ConfigurationSettings.AppSettings["FilePathCORI"].ToString().ToUpper());
                    }
                    catch (Exception ex)
                    {
                        WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());

                    }
                    #endregion

                    #region IMPORT

                    int timeToImp = Convert.ToInt32(ConfigurationSettings.AppSettings["TimeToImport"].ToString());

                    try
                    {

                        //if (DateTime.Now.Hour == timeToImp || Once_In_A_Day_Flag_Imp == false)
                        //{
                        // GenerateTxt();
                        ImportReturnedFile(ConfigurationSettings.AppSettings["FilePathCommission"].ToString().ToUpper());
                        ImportReturnedFile(ConfigurationSettings.AppSettings["FilePathClaimExp"].ToString().ToUpper());
                        ImportReturnedFile(ConfigurationSettings.AppSettings["FilePathClaimInd"].ToString().ToUpper());
                        ImportReturnedFile(ConfigurationSettings.AppSettings["FilePathCustRefund"].ToString().ToUpper());
                        ImportReturnedFile(ConfigurationSettings.AppSettings["FilePathCORI"].ToString().ToUpper());
                        //timer_imp.Enabled = false;
                        //Once_In_A_Day_Flag_Imp = true;
                        //}

                    }
                    catch (Exception ex)
                    {
                        WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());

                    }
                    finally
                    {
                        //GC.Collect();
                        
                    }
                    #endregion

                   

                    #region Update End File Status
                    UpdateEndFileStatus();
                    #endregion
                        


                    //}
                }

                catch (Exception ex)
                {
                    WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                    flagStart = false;
                }
                finally
                {

                }

                flagStart = false;
            }
            
        }

        private void timer_day_flag_Elapsed(object sender, EventArgs e)
        {
            //Once_In_A_Day_Flag_Exp = false;
            //timer.Enabled = true;
            //Once_In_A_Day_Flag_Imp = false;
            timer_imp.Enabled = true;

        }

         #region MELEVENTS FILE GENERATION
        private void timer_imp_Elapsed(object sender, EventArgs e)       
        {
            string path = "";
            string strReturn = "";
            int timeToExp = Convert.ToInt32(ConfigurationSettings.AppSettings["TimeToExpMelEvents"].ToString());
            if (DateTime.Now.Hour == timeToExp || Once_In_A_Day_Flag_Imp == false)
                {
                    WriteEventsLog("CALLED MELEVENT GenerateTxt(). ");
                    path = System.Configuration.ConfigurationSettings.AppSettings["FilePathForMelEvents"];
                    objClsMelEvent.ConnStr = ConfigurationSettings.AppSettings["conString"].ToString();
                    if (CreateFolder(path))
                    {
                        WriteEventsLog("MEL EVENTS FOLDER CREATED AND GO FOR Proc_MelEventos_Issuance TEXT GENERATION.  ");
                        try //1
                        {
                            //objClsMelEvent.GenerateTxt("Proc_MelEventos_Issuance", "MelEventos", flagDelFile);
                            //flagDelFile = 1;
                           strReturn =  objClsMelEvent.GenerateTxt("Proc_MelEventos_Issuance", "Ebix");
                                                     
                           WriteEventsLog(strReturn);                           
                            
                                
                            
                        }
                        catch (Exception ex)
                        {
                            //flagDelFile = 1;
                            WriteEventsLog("Proc_MelEventos_Issuance : " + ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString().ToString());
                        }
                        

                        Once_In_A_Day_Flag_Imp = true;
                        timer_imp.Enabled = false;
                    }
               }
              

        }
        #endregion
       

       
       

        #endregion

        #region METHODS
        private void UpdateEndFileStatus()
        {
            DataSet ds = new DataSet();
            ds = obj.GetExportFiles("PROC_GET_PAGNET_EXPORT_FILES",null);
            try
            {
                UpdateFiles(ConfigurationSettings.AppSettings["FilePathCommission"].ToString().ToUpper(),ds);
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
            }
            try
            {
                UpdateFiles(ConfigurationSettings.AppSettings["FilePathClaimExp"].ToString().ToUpper(), ds);
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
            }
            try
            {
                UpdateFiles(ConfigurationSettings.AppSettings["FilePathClaimInd"].ToString().ToUpper(), ds);
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
            }
            try
            {
                UpdateFiles(ConfigurationSettings.AppSettings["FilePathCustRefund"].ToString().ToUpper(), ds);
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
            }
            try
            {
                UpdateFiles(ConfigurationSettings.AppSettings["FilePathCORI"].ToString().ToUpper(), ds);
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());

            }

            ds.Dispose();
        }

        private Boolean CreateFolder(string path)
        {

            try
            {

                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {

                    if (!Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }
                    endImpersonation();
                    return true;
                }
                else
                {
                    WriteEventsLog("User does not have permission to create or access directory from  " + path);
                    return false;
                }


            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString().ToString());
                return false;
            }
        }


        public void UpdateFiles(string filePath)
        {
            try
            {
                DataSet ds = new DataSet();
                string strFileName = "";
                string strFileName_fullPath = "";
                string[] DirFileName = null;
                string strDirFileName = "";
                strFileName_fullPath = "";
                int file_id;
                //string filePath= System.Configuration.ConfigurationSettings.AppSettings["FilePathCustRefund"];

                if (filePath.EndsWith("\\APR\\"))
                {
                    filePath = filePath.Replace("\\APR\\", "");

                }

                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    ds = obj.GetExportFiles("PROC_GET_PAGNET_EXPORT_FILES", "Generated");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                strFileName = dr["FILE_NAMES"].ToString();
                                file_id = Convert.ToInt32(dr["ID"].ToString());
                                //strFileName_fullPath = filePath + "\\" + "RET" + "\\" + strFileName;
                                strFileName_fullPath = filePath + "\\" + "RET" ;
                                //CHECKING IN RETURN FOLDER
                                //if (System.IO.File.Exists(strFileName_fullPath))
                                //{
                                if (Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*").Count() > 0)
                                {
                                    DirFileName = Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*");
                                    strDirFileName = Path.GetFileName(DirFileName[0]);
                                    if (strFileName == strDirFileName.Substring(strDirFileName.IndexOf('_') + 1, strDirFileName.Length - strDirFileName.IndexOf('_') - 1))
                                    {
                                        strFileName_fullPath = "";
                                        //strFileName_fullPath = filePath + "\\" + "PRC" + "\\" + strFileName;
                                        strFileName_fullPath = filePath + "\\" + "PRC";

                                        //if (System.IO.File.Exists(strFileName_fullPath))
                                        //{
                                        if (Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*").Count() > 0)
                                        {
                                            DirFileName = Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*");
                                            strDirFileName = Path.GetFileName(DirFileName[0]);
                                            if (strFileName == strDirFileName.Substring(strDirFileName.IndexOf('_') + 1, strDirFileName.Length - strDirFileName.IndexOf('_') - 1))
                                            {
                                                try
                                                {
                                                    //strFileName = Path.GetFileName(strFileName_fullPath);
                                                    obj.UpdateExportFiles("PROC_UPDATE_PAGNET_EXPORT_FILES", "Imported by PagNet with success", Dns.GetHostName(), strFileName);
                                                }
                                                catch (Exception ex)
                                                {
                                                    WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                                }
                                                try
                                                {
                                                    //strFileName = Path.GetFileName(strFileName_fullPath);
                                                    obj.UpdateFlagsInParent("PROC_UPDATE_PARENT_ON_SUCCESS", file_id);
                                                }
                                                catch (Exception ex)
                                                {
                                                    WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                                }
                                            }



                                        }
                                    
                                    else
                                    {
                                        //CHECKING IN ERROR
                                        strFileName_fullPath = "";
                                        //strFileName_fullPath = filePath + "\\" + "ERR" + "\\" + strFileName;
                                        strFileName_fullPath = filePath + "\\" + "ERR";
                                        //if (System.IO.File.Exists(strFileName_fullPath))
                                        //{
                                        if (Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*").Count() > 0)
                                        {
                                            DirFileName = Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*");
                                            strDirFileName = Path.GetFileName(DirFileName[0]);
                                            if (strFileName == strDirFileName.Substring(strDirFileName.IndexOf('_') + 1, strDirFileName.Length - strDirFileName.IndexOf('_') - 1))
                                            {
                                                try
                                                {
                                                    //strFileName = Path.GetFileName(strFileName_fullPath);
                                                    obj.UpdateExportFiles("PROC_UPDATE_PAGNET_EXPORT_FILES", "Imported by PagNet with error", Dns.GetHostName(), strFileName);
                                                }
                                                catch (Exception ex)
                                                {
                                                    WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                                }
                                            }

                                            try
                                            {
                                                string result = "";
                                                WriteEventsLog("\r\n" + "Call UpdateFlagsInParent()");
                                                result = obj.UpdateFlagsInParent("PROC_UPDATE_STATUS_AFTER_RETURN", file_id);
                                                WriteEventsLog("\r\n" + "UPDATE FLAG IN PARENTS TABLE : " + result);
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                            }

                                        }
                                   
                                }
                               }
                             }

                            }
                        }
                    }
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            


        }

        //for processing of End folder files
        public void UpdateFiles(string filePath,DataSet ds)
        {
            try
            {
                // DataSet ds = new DataSet();
                string strFileName = "";
                string[] DirFileName = null;
                string strDirFileName = "";
                string strFileName_fullPath = "";
                strFileName_fullPath = "";
                //string filePath= System.Configuration.ConfigurationSettings.AppSettings["FilePathCustRefund"];

                if (filePath.EndsWith("\\APR\\"))
                {
                    filePath = filePath.Replace("\\APR\\", "");

                }

                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    //ds = obj.GetExportFiles("PROC_GET_PAGNET_EXPORT_FILES");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                strFileName = dr["FILE_NAMES"].ToString();
                                //strFileName_fullPath = filePath + "\\" + "END" + "\\" + strFileName;
                                strFileName_fullPath = filePath + "\\" + "END";
                                //CHECKING IN RETURN FOLDER
                                //if (System.IO.File.Exists(strFileName_fullPath))
                                //{
                                if (Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*").Count() > 0)
                                {
                                    DirFileName = Directory.GetFiles(strFileName_fullPath, "*_" + strFileName + "*");
                                    strDirFileName = Path.GetFileName(DirFileName[0]);
                                    if (strFileName == strDirFileName.Substring(strDirFileName.IndexOf('_') + 1, strDirFileName.Length - strDirFileName.IndexOf('_') - 1))
                                    {

                                        strFileName = Path.GetFileName(strFileName_fullPath);
                                        obj.UpdateExportFiles("PROC_UPDATE_PAGNET_EXPORT_FILES", "Ok", Dns.GetHostName(), strFileName);
                                    }
                                }



                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ds.Dispose();
            }


        }

        private void GenerateTxt()
        {
            clsUtility obj = new clsUtility();
            string path = "";
            obj.ConnStr = ConfigurationSettings.AppSettings["conString"].ToString();
            string strResult = "";
            try
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["FilePathCustRefund"];

                if (CreateFolder(path))
                {
                    // ProcessCompleteFlag = false;
                    //GrantPermission(path);
                    strResult = obj.Customer_Refund();
                   // DenyPermission(path);
                    WriteEventsLog(strResult + " Customer_Refund()");

                    //WriteEventsLog("START FILE STATUS UPDATION");



                }
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString().ToString());
            }

            try
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["FilePathCommission"];

                if (CreateFolder(path))
                {
                    strResult = obj.Broker_Commission();
                    WriteEventsLog(strResult + " Broker_Commission()");
                }
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString().ToString());
            }
            try
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["FilePathClaimExp"];

                if (CreateFolder(path))
                {
                    strResult = obj.CLAIM_EXPENSE();
                    WriteEventsLog(strResult + " CLAIM_EXPENSE()");
                }
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString().ToString());
            }
            try
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["FilePathClaimInd"];

                if (CreateFolder(path))
                {
                    strResult = obj.Claim_Indeminity();
                    WriteEventsLog(strResult + " Claim_Indeminity()");
                }
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString().ToString());
            }
            try
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["FilePathCORI"];

                if (CreateFolder(path))
                {
                    strResult = obj.RI_CLAIM();
                    WriteEventsLog(strResult + " RI_CLAIM()");
                }
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString().ToString());
            }



            // WriteEventsLog("GenerateTxt() END. ");

        }
        /////////////////IMPORTING TEXT FILE///////////////////////
        ///////////////////////////////////////////////////////////
        private void ImportReturnedFile(string filePath)
        {
            //WriteEventsLog("ImportReturnedFile() START. ");
            //ProcessCompleteFlag = false; 
            try
            {

                obj.ConnStr = ConfigurationSettings.AppSettings["conString"].ToString();
                //string filePathToImport = ConfigurationSettings.AppSettings["PathToImport"].ToString();

                if (filePath.EndsWith("\\APR\\"))
                {
                    filePath = filePath.Replace("\\APR\\", "");

                }

                string filePathToImport = filePath +"\\RET";
                string strFull = "";
                int returnValue = 0;
                string[] filePaths = null;
                string fileName = "";
                int counter = 1;
                System.IO.StreamReader file = null;

                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    if (Directory.Exists(filePathToImport))
                    {
                        filePaths = Directory.GetFiles(filePathToImport, "*.txt");

                    }
                    
                   
                    if (!String.IsNullOrEmpty(filePaths.ToString()))
                    {
                        if (filePaths.Length > 0)
                        {

                            for (int i = 0; i < filePaths.Length; i++)
                            {
                                fileName = Path.GetFileName(filePaths[i]);
                                //using (System.IO.StreamReader file = new System.IO.StreamReader(filePaths[i]))
                                //{
                                    file = new System.IO.StreamReader(filePaths[i]);
                                    counter = 1;
                                    if (!file.EndOfStream)
                                    {
                                       
                                        while (!file.EndOfStream)
                                        {
                                           
                                            strFull = file.ReadLine();
                                            if (counter == 1)
                                            {
                                                obj.Import(strFull, 0, "", "", counter);
                                            }
                                            else
                                            {
                                                obj.Import(strFull, 0, "", "", counter);
                                            }
                                            counter = counter + 1;
                                        }
                                    }
                                    if (file.EndOfStream)
                                    {
                                        if (fileName.Contains('_'))
                                        {
                                            fileName = fileName.Substring(fileName.IndexOf('_') + 1, fileName.Length - fileName.IndexOf('_') - 1);
                                        }
                                        returnValue = obj.Import("", 1, Dns.GetHostName(), fileName,0);
                                    }
                                    if (returnValue > 0)
                                    {
                                        //In log file mark for success to import corrosponging file name
                                        // MessageBox.Show("File imported successfully !!", "Pagnet Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        //In log file mark for success to import corrosponging file name
                                        //MessageBox.Show("No record is available to import !!", "Pagnet Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    file.Dispose();
                               // }

                            }
                        }
                        else
                        {
                            WriteEventsLog("No file exists on  " + filePathToImport);
                        }
                    }
                    else
                    {
                        WriteEventsLog("No file exists on  " + filePathToImport);
                    }

                    endImpersonation();
                }
                else
                {
                    WriteEventsLog("User does not have permission to create or access directory from  " + filePathToImport);
                }

            }
            catch (Exception ex)
            {
                WriteEventsLog("Error in File Import: " + ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                //throw ex;
            }
            WriteEventsLog("ImportReturnedFile() END. ");
        }



        


        public static String CheckErrorLogFileExist(string strpath, string strnameBegin)
        {
            try
            {
                string FilePath = strpath;
                FileInfo finfo = new FileInfo(FilePath);
                DirectoryInfo dinfo = finfo.Directory;
                FileSystemInfo[] fsinfo = dinfo.GetFiles();
                foreach (FileSystemInfo info in fsinfo)
                {
                    if (info.Name.StartsWith(strnameBegin))
                    {
                        return info.Name;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CreateLogFile()
        {
            String CreateLogfilepath = Logfilepath + LogfileName;
            StreamWriter SW;
            SW = File.CreateText(CreateLogfilepath);
            SW.WriteLine("This is error log file for generating text files and Import returned text files through PagnetService created on     " + DateTime.Now);
            SW.Close();
        }
        public void WriteEventsLog(String ErrorMessage)
        {

            StreamWriter SW;
            SW = File.AppendText(Logfilepath + LogfileName);
            SW.WriteLine(ErrorMessage + "Service Last run at:" + DateTime.Now);
            SW.Close();

        }

        #endregion           

        #region  IMPERSONATE

        public void endImpersonation()
        {
            try
            {
                if (impersonationContext != null) impersonationContext.Undo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Ebix Advantage Web", "Impersionation Error; Message:-" + ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
            }
        }
        public bool ImpersonateUser(String userName, String password, String domainName)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;
            bool authentication = false;

            try
            {
                //Temprary code for Block Impersonation (Use for Development)
                if (Impersonate_Inactive == "0")
                {
                    authentication = true;
                }
                else
                {

                    if (LogonUser(userName, domainName, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            impersonationContext = tempWindowsIdentity.Impersonate();
                            if (impersonationContext != null)
                                authentication = true;
                            else
                                authentication = false;
                        }
                        else
                            authentication = false;
                    }
                    else
                        authentication = false;
                }
            }
            catch (Exception ex)
            {

            }
            return authentication;
        }

        #endregion End impersonate        

       

        #region Comment
        /*private void ReadErrFiles(string filePath)
        {
            //WriteEventsLog("ImportReturnedFile() START. ");
            //ProcessCompleteFlag = false; 
            try
            {

                obj.ConnStr = ConfigurationSettings.AppSettings["conString"].ToString();
                //string filePathToImport = ConfigurationSettings.AppSettings["PathToImport"].ToString();

                if (filePath.EndsWith("\\APR\\"))
                {
                    filePath = filePath.Replace("\\APR\\", "");

                }

                string filePathToImport = filePath + "\\ERR";
                string strFull = "";
                int returnValue = 0;
                string[] filePaths = null;
                string fileName = "";

                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    if (Directory.Exists(filePathToImport))
                    {
                        filePaths = Directory.GetFiles(filePathToImport, "*.txt");

                    }

                    endImpersonation();

                    if (!String.IsNullOrEmpty(filePaths.ToString()))
                    {
                        if (filePaths.Length > 0)
                        {

                            for (int i = 0; i < filePaths.Length; i++)
                            {
                                fileName = Path.GetFileName(filePaths[i]);
                                using (System.IO.StreamReader file = new System.IO.StreamReader(filePaths[i]))
                                {

                                    if (!file.EndOfStream)
                                    {
                                        while (!file.EndOfStream)
                                        {
                                            strFull = file.ReadLine();
                                            obj.UPDATE_PAGNET_EXPORT_RECORD(strFull, 0);
                                        }
                                    }
                                    if (file.EndOfStream)
                                    {
                                        returnValue = obj.UPDATE_PAGNET_EXPORT_RECORD("", 1);
                                    }
                                    if (returnValue > 0)
                                    {
                                        //In log file mark for success to import corrosponging file name
                                        // MessageBox.Show("File imported successfully !!", "Pagnet Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        //In log file mark for success to import corrosponging file name
                                        //MessageBox.Show("No record is available to import !!", "Pagnet Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                            }
                        }
                        else
                        {
                            WriteEventsLog("No file exists on  " + filePathToImport);
                        }
                    }
                    else
                    {
                        WriteEventsLog("No file exists on  " + filePathToImport);
                    }
                }
                else
                {
                    WriteEventsLog("User does not have permission to create or access directory from  " + filePathToImport);
                }

            }
            catch (Exception ex)
            {
                WriteEventsLog("Error in File Import: " + ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                //throw ex;
            }
            WriteEventsLog("ImportReturnedFile() END. ");
        }*/


        /*
         #region GrantPermission
         public void GrantPermission(string strFilePath)
        {
            //string strFilePath = @"C:\Documents and Settings\" + CommonInformation.GetWindowUserName.ToString() + @"\Application Data\" + CommonInformation.GetWindowUserName.ToString();
            try
            {
                if (ConfigurationSettings.AppSettings["FolderPermission"].ToString().Equals("Lock"))
                {
                    DirectoryInfo myDirectoryInfo = new DirectoryInfo(strFilePath);
                    DirectorySecurity myDirectorySecurity = myDirectoryInfo.GetAccessControl();
                   // string User = System.Environment.UserDomainName + "\\" + WindowsIdentity.GetCurrent().Name.ToString();
                    string User = IDomain + "\\" + IUserName;
                    myDirectorySecurity.RemoveAccessRule(new FileSystemAccessRule(User, FileSystemRights.Read, AccessControlType.Deny));
                    myDirectoryInfo.SetAccessControl(myDirectorySecurity);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
            }

        }
         #endregion

         public void DenyPermission(string strFilePath)
         {
             try
             {
                 if (ConfigurationSettings.AppSettings["FolderPermission"].ToString().Equals("Lock"))
                 {
                    // string strFilePath = str + CommonInformation.GetWindowUserName.ToString() + @"\Application Data\" + CommonInformation.GetWindowUserName.ToString(); //'@"C:\Documents and Settings\" + CommonInformation.GetWindowUserName.ToString() + @"\Application Data\" + CommonInformation.GetWindowUserName.ToString();
                     DirectoryInfo myDirectoryInfo = new DirectoryInfo(strFilePath);
                     DirectorySecurity myDirectorySecurity = myDirectoryInfo.GetAccessControl();
                     //string User = System.Environment.UserDomainName + "\\" + CommonInformation.GetWindowUserName;
                     string User = IDomain + "\\" + IUserName;
                     myDirectorySecurity.AddAccessRule(new FileSystemAccessRule(User, FileSystemRights.Read, AccessControlType.Deny));
                     myDirectoryInfo.SetAccessControl(myDirectorySecurity);
                 }
             }
             catch (Exception ex)
             {
                 
                 throw ex;
             }
             finally
             {
             }
         }
        */
        #endregion
    }


    
}