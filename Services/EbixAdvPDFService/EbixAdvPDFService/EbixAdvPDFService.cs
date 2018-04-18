using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Web;
using Cms.DataLayer;
using BlGeneratePdf;
using System.Configuration;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
/*
using Cms.BusinessLayer.BlBoleto;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlProcess;
using System.Web;*/
// testing by Rajan for Prod Copy Src Safe Control

namespace EbixAdvPDFService
{
    public partial class EbixAdvPDFService : ServiceBase
    {

        BlGeneratePdf.ClsGeneratePdf ObjClsGeneratePdf = new ClsGeneratePdf();
        //BlGeneratePdf.ClsGeneratePdf ObjClsGeneratePdf = new ClsGeneratePdf();
        private String LogfileName = System.Configuration.ConfigurationManager.AppSettings["EventLogFileName"].ToString().Trim();
        private String Logfilepath = System.AppDomain.CurrentDomain.BaseDirectory;
        private System.Timers.Timer timer;

        String ConnectString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();

        DataWrapper objWrapper; 
        
        Boolean ProcessCompleteFlag = false;
        //Boolean ILProcessFlag = false;
        //task 83, Load balancing
        string Machine_Name = System.Net.Dns.GetHostName();    
       public EbixAdvPDFService()
        {
             
             InitializeComponent();
             
             //objWrapper = new DataWrapper(ConnectString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
             
            
             

            if(CheckErrorLogFileExist(Logfilepath, LogfileName) == "")
            {
                CreateLogFile();
            }
        }

        protected override void OnStart(string[] args)
        {
            InitializeTimer();
            timer.Enabled = true;
           

        }

        protected override void OnStop()
        {

            timer.Enabled = false;
            WriteEventsLog("Service stopped successfully");
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
            SW.WriteLine("This is error log file for generating pdf through PDFService created on     " + DateTime.Now);
            SW.Close();
        }


       

        private void InitializeTimer()
        {
            
            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Interval = 30000 * Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["IntervalMinutes"]);
            timer.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            
        }

      

        private void GeneratePDF()
        {


            try
            {
                ProcessCompleteFlag = true;
                
                //This Property will set the name  of Machine, Applied for load balancing
                ObjClsGeneratePdf.Machine_Name = Machine_Name;
                 //apppath we pick the templates and logo  from cmsweb folder
                 //string appPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                string appPath = System.Configuration.ConfigurationManager.AppSettings["TemplatePath"].ToString();
                //Connection string, pdf path, clause path to be changed w.r.t to server used   
                ObjClsGeneratePdf.ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
                ObjClsGeneratePdf.PdfPath = System.Configuration.ConfigurationManager.AppSettings["PDFPath"].ToString();
                ObjClsGeneratePdf.ClauseFilePath = System.Configuration.ConfigurationManager.AppSettings["ClausePath"].ToString();

                ObjClsGeneratePdf.RTF_To_Html =appPath + System.Configuration.ConfigurationManager.AppSettings["RTF_HTML"].ToString();
                ObjClsGeneratePdf.Impersonate_Inactive = System.Configuration.ConfigurationManager.AppSettings["Impersonate"].ToString();
                ObjClsGeneratePdf.IDomain = System.Configuration.ConfigurationManager.AppSettings["IDomain"].ToString();
                ObjClsGeneratePdf.IUserName = System.Configuration.ConfigurationManager.AppSettings["IUserName"].ToString();
                ObjClsGeneratePdf.IPassWd = System.Configuration.ConfigurationManager.AppSettings["IPassWd"].ToString();
            
          
                ObjClsGeneratePdf.FinalPDFPath = System.Configuration.ConfigurationManager.AppSettings["FinalPathpdf"].ToString();
                ObjClsGeneratePdf.CompanyLogo = appPath + System.Configuration.ConfigurationManager.AppSettings["CompanyLogo"].ToString();
                ObjClsGeneratePdf.BoletoCssPath = appPath + System.Configuration.ConfigurationManager.AppSettings["BoletoPDFCss"].ToString();
                ObjClsGeneratePdf.BarcodeImagePath = appPath + System.Configuration.ConfigurationManager.AppSettings["BarcodeImagePath"].ToString();
                ObjClsGeneratePdf.ImagePath = appPath + System.Configuration.ConfigurationManager.AppSettings["LogoPath"].ToString();
                
                //114
                ObjClsGeneratePdf.Xsl_Coverage_114_Comprehensive_Dwelling = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_114_Comprehensive_Dwelling"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_114_More_Than_One_Broker_Dwelling = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_114_More_Than_One_Broker_Dwelling"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_114_More_Than_One_Broker__With_Ceded_COI_Dwelling = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_114_More_Than_One_Broker__With_Ceded_COI_Dwelling"].ToString();
                //116
                ObjClsGeneratePdf.Xsl_Coverage_116_Comprehensive_Condominium = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_116_Comprehensive_Condominium"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_116_More_Than_One_Broker_Condominium = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_116_More_Than_One_Broker_Condominium"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_116_More_Than_One_Broker__With_Ceded_COI_Condominium = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_116_More_Than_One_Broker__With_Ceded_COI_Condominium"].ToString();

                //118
                ObjClsGeneratePdf.Xsl_Coverage_118_Comprehensive_Company = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_118_Comprehensive_Company"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_118_More_Than_One_Broker_Company = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_118_More_Than_One_Broker_Company"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_118_More_Than_One_Broker__With_Ceded_COI_Company = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_118_More_Than_One_Broker__With_Ceded_COI_Company"].ToString();

                //171
                ObjClsGeneratePdf.xsl_Coverage_171_Diversified_Risk = appPath + System.Configuration.ConfigurationManager.AppSettings["xsl_Coverage_171_Diversified_Risk"].ToString();
                ObjClsGeneratePdf.xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker = appPath + System.Configuration.ConfigurationManager.AppSettings["xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker"].ToString();
                ObjClsGeneratePdf.xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker_With_Ceded_COI = appPath + System.Configuration.ConfigurationManager.AppSettings["xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker_With_Ceded_COI"].ToString();

                //NBS Cover and Header
                ObjClsGeneratePdf.CoverXslNewBusiness_Path = appPath + System.Configuration.ConfigurationManager.AppSettings["xslFilepathCover"].ToString();
                ObjClsGeneratePdf.HeaderXslNewBusiness_Path = appPath + System.Configuration.ConfigurationManager.AppSettings["xslFilepathHeader"].ToString();
               
                //Endorsment Cover and header
                ObjClsGeneratePdf.CoverXSLEndorsement_Path = appPath + System.Configuration.ConfigurationManager.AppSettings["xslFilepathCover_Endorsement"].ToString();
                ObjClsGeneratePdf.HeaderXSLEdorsement_Path = appPath + System.Configuration.ConfigurationManager.AppSettings["xslFilepathHeader_Endorsement"].ToString();
                
                //Renewal Cover and Header
                ObjClsGeneratePdf.Xsl_Cover_Renewal = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Cover_Renewal"].ToString();
                ObjClsGeneratePdf.Xsl_Header_Renewal = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Header_Renewal"].ToString();

                //Cancel Cover and Header
                ObjClsGeneratePdf.Xsl_Cover_Cancel_Policy = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Cover_Cancel_Policy"].ToString();
                ObjClsGeneratePdf.Xsl__header_Cancel_Policy = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl__header_Cancel_Policy"].ToString();
                
                //553,523
                ObjClsGeneratePdf.Xsl_Coverage_523_Liability_Transportation = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_523_Liability_Transportation"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_523_Liability_Transportation_withPremium = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_523_Liability_Transportation_withPremium"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_553_Facultive_Liability = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_553_Facultive_Liability"].ToString();
                
                
                ObjClsGeneratePdf.Xsl_Coverage_981_Individual_personal_accident = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_981_Individual_personal_accident"].ToString();
                ObjClsGeneratePdf.Endo_type_21_23_553_Facultative_Liability = appPath + System.Configuration.ConfigurationManager.AppSettings["Endo_type_21_23_553_Facultative_Liability"].ToString();
                ObjClsGeneratePdf.Endo_Type_21_0114_0116_0118_0196_0115_0167 = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167"].ToString();

                ObjClsGeneratePdf.XslFilepathCover_Endorsement_Type_21_0114 = appPath + System.Configuration.ConfigurationManager.AppSettings["xslFilepathCover_Endorsement_Type_21_0114"].ToString();
                ObjClsGeneratePdf.Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2 = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2"].ToString();
                ObjClsGeneratePdf.Endorsement_type_31_553_Facultative_Liability = appPath + System.Configuration.ConfigurationManager.AppSettings["Xsl_Coverage_Endorsement_type_31_553_Facultative_Liability_v2"].ToString();
                ObjClsGeneratePdf.Xsl_COI_More_Thn_One_Broker_Policy = appPath + System.Configuration.ConfigurationManager.AppSettings["COI_More_Thn_One_Broker_Policy"].ToString();
                ObjClsGeneratePdf.Xsl_COI_More_Thn_One_Broker_Policy_Endorsement = appPath + System.Configuration.ConfigurationManager.AppSettings["COI_More_Thn_One_Broker_Policy_Endorsement"].ToString();
                //itrack 1298,modified by naveen
                ObjClsGeneratePdf.Endorsement_type_31_523_Civil_Liability_Transportation = appPath + System.Configuration.ConfigurationManager.AppSettings["Endorsement_type_31_523_Civil_Liability_Transportation"].ToString();
                ObjClsGeneratePdf.Endorsement_type_31_553_D_015_Facultive_Liability = appPath + System.Configuration.ConfigurationManager.AppSettings["Endorsement_type_31_553_D_015_Facultive_Liability"].ToString();
               //itrack 1298, product 22
                ObjClsGeneratePdf.Xsl_Coverage_D_037_Endorsement_type_31_Product_0520_v1 = appPath + System.Configuration.ConfigurationManager.AppSettings["D_037_Endorsement_type_31_Product_0520_v1"].ToString();

                string str= ObjClsGeneratePdf.GetPDdfDataForDocumentType().ToString();
              
                WriteEventsLog(str);
            }
            catch (Exception exe)
            {
                WriteEventsLog(exe.Message.ToString());
            }
            ProcessCompleteFlag = false;

        }


        public void WriteEventsLog(String ErrorMessage)
        {

            StreamWriter SW;
            SW = File.AppendText(Logfilepath + LogfileName);
            SW.WriteLine(ErrorMessage + "Service Last run at:" + DateTime.Now);
            SW.Close();
        }


        private void timer1_Elapsed(object sender, EventArgs e)
        {
            //ClsProductPdfXml objProductPdfXml = new ClsProductPdfXml(objWrapper);

            try
            {   /*
                if (ILProcessFlag == false)
                {
                    objProductPdfXml.GeneratePolicyDocAndBoletoOfInitialLoadData(ref ILProcessFlag); //Added by Pradeep Kushwaha on 15-Nov-2011 for initial load policy boleto
                }*/
                if (ProcessCompleteFlag == false)
                {
                    GeneratePDF();
                   
                }
            }
            catch (Exception ex)
            {
                WriteEventsLog(ex.Message.ToString());

            }
            finally
            {
                //objProductPdfXml.Dispose();
            }

        }
        

       
    }
}
