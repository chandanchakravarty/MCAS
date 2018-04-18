using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Configuration;
//using iTextSharp;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.IO;
using System.Xml;
using System.Collections;
using System.Data;

namespace Cms.BusinessLayer.BlProcess
{
    public class ClsGenerateCancNotice
    {
        int _CustomerID = 0;
        int _PolicyID = 0;
        int _PolicyVersionID = 0;
        String _AgencyCode = "Agency Code Not Found";
        String _pdfPath = "";
        String _strImpersonationUserId = "";
        String _strImpersonationPassword = "";
        String _strImpersonationDomain = "";
        String _CarrierSystemID = "";
        String _FinalPathpdf = "";
        String _strLoBName = "";
        String _InPutFileName = "";
        String _OutPutFileName = "";
        //String _InPutpdfPath = "";
        //String _OutPutpdfPath = "";
        String _StateName = "";
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

        public int CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                _CustomerID = value;
            }
        }
        public int PolicyID
        {
            get
            {
                return _PolicyID;
            }
            set
            {
                _PolicyID = value;
            }
        }
        public int PolicyVersionID
        {
            get
            {
                return _PolicyVersionID;
            }
            set
            {
                _PolicyVersionID = value;
            }
        }
        public String AgencyCode
        {
            get
            {
                return _AgencyCode;
            }
            set
            {
                _AgencyCode = value;
            }
        }
        public String FinalPathpdf
        {
            get
            {
                return _FinalPathpdf;
            }
            set
            {
                _FinalPathpdf = value;
            }
        }
        public String pdfPath
        {
            get
            {
                return _pdfPath;
            }
            set
            {
                _pdfPath = value;

            }
        }
        public String CarrierSystemID
        {
            get
            {
                return _CarrierSystemID;
            }
            set
            {
                _CarrierSystemID = value;

            }
        }
        public String ImpersonationUserId
        {
            get
            {
                return _strImpersonationUserId;
            }
            set
            {
                _strImpersonationUserId = value;

            }
        }
        public String ImpersonationPassword
        {
            get
            {
                return _strImpersonationPassword;
            }
            set
            {
                _strImpersonationPassword = value;

            }
        }
        public String ImpersonationDomain
        {
            get
            {
                return _strImpersonationDomain;
            }
            set
            {
                _strImpersonationDomain = value;

            }
        }
        public String LoBName
        {
            get
            {
                return _strLoBName;
            }
            set
            {
                _strLoBName = value;

            }
        }
        public String StateName
        {
            get
            {
                return _StateName;
            }
            set
            {
                _StateName = value;

            }
        }
        public String InPutFileName
        {
            get
            {
                return _InPutFileName;
            }
            set
            {
                _InPutFileName = value;

            }
        }
        public String OutPutFileName
        {
            get
            {
                return _OutPutFileName;
            }
            set
            {
                _OutPutFileName = value;

            }
        }
        //public String InPutpdfPath
        //{
        //    get
        //    {
        //        return _InPutpdfPath;
        //    }
        //    set
        //    {
        //        _InPutpdfPath = value;

        //    }
        //}
        //public String OutPutpdfPath
        //{
        //    get
        //    {
        //        return _OutPutpdfPath;
        //    }
        //    set
        //    {
        //        _OutPutpdfPath = value;

        //    }
        //}
        private void CreateDirectoryForCancellationNotice()
        {
            try
            {
                string strImpersonationUserId = "", strImpersonationPassword = "", strImpersonationDomain = "";
                //strImpersonationUserId = System.Configuration.ConfigurationSettings.AppSettings.Get("IUserName").ToString().Trim();
                //strImpersonationPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("IPassWd").ToString().Trim();
                //strImpersonationDomain = System.Configuration.ConfigurationSettings.AppSettings.Get("IDomain").ToString().Trim();
                ImpersonateUser(strImpersonationUserId, strImpersonationPassword, strImpersonationDomain);
                //commented
                // String appPath = System.Web.HttpContext.Current.Server.MapPath(pdfPath);
                String appPath = pdfPath;
                appPath = appPath + "\\" + "OutPutPdfs" + "\\";
                //string appPath = System.Web.HttpContext.Current.Server.MapPath("~/");

                System.IO.Directory.CreateDirectory(appPath + CarrierSystemID.Trim());
                //1st Directory AgencyCode
                // String AgencyCode = "27";
                System.IO.Directory.CreateDirectory(appPath + CarrierSystemID.Trim() + "/" + AgencyCode.Trim());
                //2nd Directory Customerid
                System.IO.Directory.CreateDirectory(appPath + CarrierSystemID.Trim() + "/" + AgencyCode.Trim() + "/" + CustomerID.ToString());
                //3rd Directory Customerid
                System.IO.Directory.CreateDirectory(appPath + CarrierSystemID.Trim() + "/" + AgencyCode.Trim() + "/" + CustomerID.ToString() + "/" + "POLICY");
                //Cancellation Notice
                System.IO.Directory.CreateDirectory(appPath + CarrierSystemID.Trim() + "/" + AgencyCode.Trim() + "/" + CustomerID.ToString() + "/" + "POLICY" + "/" + "CANC_NOTICE");
                //final
                System.IO.Directory.CreateDirectory(appPath + CarrierSystemID.Trim() + "/" + AgencyCode.Trim() + "/" + CustomerID.ToString() + "/" + "POLICY" + "/" + "CANC_NOTICE"+"/" + "final");
                //TEMP
                System.IO.Directory.CreateDirectory(appPath + CarrierSystemID.Trim() + "/" + AgencyCode.Trim() + "/" + CustomerID.ToString() + "/" + "POLICY" + "/" + "CANC_NOTICE" + "/" + "final" + "/" + "temp");
                endImpersonation();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #region end impersonate
        /// <summary>
        /// End the imporsonation
        /// </summary>
        public void endImpersonation()
        {
            try
            {
                if (impersonationContext != null) impersonationContext.Undo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("EbixASP WebMerge 3.0", "Impersionation Error; Message:-" + ex.Message);
            }
        }
        #endregion
        #region impersonate
        public bool ImpersonateUser(String userName, String password, String domainName)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;
            bool authentication = false;

            try
            {
                //Temprary code for Block Impersonation (Use for Development)
                if (ConfigurationManager.AppSettings.Get("Impersonate") == "0")
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
            catch//(Exception ex)
            {
            }
            return authentication;
        }
        #endregion

        public string GenerateCancellationNoticePDF(String PDFXml)
        {
           // stateCode = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom, int.Parse(strAppId), int.Parse(strAppVersionId), int.Parse(strCustomerId)); 
            CreateDirectoryForCancellationNotice();
            String OutputPath = "";
            String InputTemplate = pdfPath + "\\" + "InputPdfs" + "\\" + CarrierSystemID + "\\"+ LoBName + "\\" + StateName.ToUpper().Trim() + "\\" + InPutFileName;

            OutputPath = pdfPath + "\\" + "OutPutPdfs" + "\\" + CarrierSystemID + "\\" + AgencyCode + "\\" + CustomerID.ToString() + "\\" + "/POLICY/CANC_NOTICE/final/";

           // FillPDFPage(InputTemplate, OutputPath + OutPutFileName, strPDFXml);
         return   GenerateCancNoticepdf(InputTemplate, OutputPath, PDFXml);

           // return "";
        
        }
        /*public void FillPDFPage(String InputpdfTemplate, String newFile, String RootElement)
        {   StringBuilder sb = new StringBuilder();
            int pdffieldsCount;
            iTextSharp.text.pdf.PdfReader PdfReader = new PdfReader(InputpdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(PdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            pdffieldsCount = pdfFormFields.Fields.Count;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>  " + RootElement + "");
            foreach (DictionaryEntry de in PdfReader.AcroFields.Fields)
            {
                sb.Append(de.Key.ToString() + Environment.NewLine);
                XmlNode child = doc.SelectSingleNode("/POLICY_CANCEL_NOTICE");
                if (child != null)
                {
                    XmlNodeReader nr = new XmlNodeReader(child);
                    while (nr.Read())
                    {
                       if ((de.Key.ToString() == nr.Name))
                       {
                         string dd = (FetchValueFromXML(nr.Name, RootElement));
                         string kk = de.Key.ToString();
                         pdfFormFields.SetField(kk, dd);
                        }
                    }
                }
            }
            pdfStamper.FormFlattening = true;
            // close the pdf
            pdfStamper.Close();
        }
        */
        #region"Fill Policy Premium Notice pdf"
        
        public String GenerateCancNoticepdf(String InputTemplateForPolicyCanceNoticepdf, String newFile, String XmlString)
        {
           
            String FormatedXml = FormatXmlString(XmlString);
            
            //String OutputCancNoticepdf;
            String generatedCancNoticeFileName="";
            /*
            string date = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year;
            //OutPutFileName + "_" +
            generatedCancNoticeFileName = OutPutFileName+ CustomerID.ToString() + "_" + PolicyID.ToString() + "_"
                                  + PolicyVersionID.ToString() + "_" + date + "_" + DateTime.Now.Ticks.ToString() + ".pdf";

            OutputCancNoticepdf = newFile + generatedCancNoticeFileName;
          

            PdfReader pdfReader = new PdfReader(InputTemplateForPolicyCanceNoticepdf);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputCancNoticepdf, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            try
            {
                int pdffieldsCount;
                pdffieldsCount = pdfFormFields.Fields.Count;
                XmlDocument doc = new XmlDocument();
                // doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>  " + XmlString + "");
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>  " + FormatedXml + "");

                String StrAccHistoryTable = FormatXmlStringForAccHistory(FormatedXml);
                XmlDocument docAccHistoryTable = new XmlDocument();
                // doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>  " + XmlString + "");
                docAccHistoryTable.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>  " + StrAccHistoryTable + "");

                StringBuilder sb = new StringBuilder();
                foreach (DictionaryEntry de in pdfReader.AcroFields.Fields)
                {
                    sb.Append(de.Key.ToString() + Environment.NewLine);

                     XmlNode childNode = docAccHistoryTable.SelectSingleNode("/POLICY_CANCEL_NOTICE");
                    if (childNode != null)
                    {
                        XmlNodeReader nre = new XmlNodeReader(childNode);
                        while (nre.Read())
                        {
                            if ((de.Key.ToString() == nre.Name))
                            {
                                string NodeValue = (FetchValueFromXML(nre.Name, StrAccHistoryTable));
                                string FieldName = de.Key.ToString();
                                pdfFormFields.SetField(FieldName, NodeValue);

                            }
                        }

                    }
                  
                }

                pdfStamper.FormFlattening = true;
                // close the pdf
                pdfStamper.Close();
            }
            catch (Exception ex)
            {
                generatedCancNoticeFileName = "";
                throw (ex);
              
            }*/
            return generatedCancNoticeFileName;
        }
        public String FormatXmlString(String PREMXmlString)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(PREMXmlString);

            string oldXmlNode = xmldoc.SelectSingleNode("POLICY_CANCEL_NOTICE/INSTALLMENT_DETAILS").InnerXml;
            oldXmlNode = "<INSTALLMENT_DETAILS>" + oldXmlNode + "</INSTALLMENT_DETAILS>";

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(oldXmlNode);
            //XmlNodeList nl = xd.SelectNodes("INSTALLMENT_DETAILS/ACT_INSTALL_DETAILS");
            XmlNodeList nl = xd.SelectNodes("INSTALLMENT_DETAILS/POL_CUST");
            int a = 1;

            foreach (XmlNode xn in nl)
            {
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode Xno = xn.ChildNodes[i];
                    XmlNode xE = xd.CreateElement(Xno.Name + "_" + a.ToString());
                    xE.InnerText = Xno.InnerText;
                    xn.ReplaceChild(xE, Xno);
                }
                a++;
            }
            string NewXmlNode;

            NewXmlNode = xmldoc.InnerXml.ToString().Replace(xmldoc.SelectSingleNode("POLICY_CANCEL_NOTICE/INSTALLMENT_DETAILS").InnerXml, xd.SelectSingleNode("INSTALLMENT_DETAILS").InnerXml);
            return NewXmlNode;
        }

        public String FormatXmlStringForAccHistory(String PREMXmlString)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(PREMXmlString);

            string oldXmlNode = xmldoc.SelectSingleNode("POLICY_CANCEL_NOTICE/ACT_CST_BAL_INFO").InnerXml;
            oldXmlNode = "<ACT_CST_BAL_INFO>" + oldXmlNode + "</ACT_CST_BAL_INFO>";

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(oldXmlNode);
            XmlNodeList nl = xd.SelectNodes("ACT_CST_BAL_INFO/BAL");
            int a = 1;

            foreach (XmlNode xn in nl)
            {
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode Xno = xn.ChildNodes[i];
                    XmlNode xE = xd.CreateElement(Xno.Name + "_" + a.ToString());
                    xE.InnerText = Xno.InnerText;
                    xn.ReplaceChild(xE, Xno);
                }
                a++;
            }
            string NewXmlNode;

            NewXmlNode = xmldoc.InnerXml.ToString().Replace(xmldoc.SelectSingleNode("POLICY_CANCEL_NOTICE/ACT_CST_BAL_INFO").InnerXml, xd.SelectSingleNode("ACT_CST_BAL_INFO").InnerXml);
            return NewXmlNode;
        }
        #region"Commented Code"
       

        #endregion

        #endregion
        public string FetchValueFromXML(string nodeName, string XMLString)
        {
            try
            {
                if (XMLString == "")
                    return "0";
                string strRetval = "";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLString);
                XmlNodeList nodList = doc.GetElementsByTagName(nodeName);
                if (nodList.Count > 0)
                {
                    strRetval = nodList.Item(0).InnerText;
                }
                return strRetval;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {

            }
        }

       
    }
}
