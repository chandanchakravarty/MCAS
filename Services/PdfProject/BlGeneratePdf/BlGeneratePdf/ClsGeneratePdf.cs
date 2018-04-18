using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.IO;
using WebSupergoo.ABCpdf7;
using BlGeneratePdf;
using System.Web;
using System.Net;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using WSBarCode.Barcodes;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Xml;
using System.Xml.Xsl;
//using Microsoft.Office.Tools;
using Microsoft.Office;

namespace BlGeneratePdf
{
    //BlPrintjob CLASS IS USED TO TO CREATE ALL THE FUNCTION NEEDED TO CREATE DIFFERENT TYPES OF PDF DOCUMENT.                    

    #region "Generate Pdf "

    public class ClsGeneratePdf
    {


        string ProcessCode = string.Empty;
        //string ProductCode = "";
        WebSupergoo.ABCpdf7.Doc SuperTheDoc;
        WebSupergoo.ABCpdf7.Doc TheDoc;
        string appPath = "";
        private string _Imagepath;
        string HtmlString = "";
        string Policy_LOB = "";
        StringBuilder strException = new StringBuilder();
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

        #region "Properties"
        public string Machine_Name { get; set; }
        public string BoletoCssPath { get; set; }
        public string BarcodeImagePath { get; set; }
        public string strPdfFilePath;
        public string strConnectionString;
        public string ImagePath
        {
            get
            {
                return _Imagepath;
            }
            set
            {
                _Imagepath = value;
            }
        }
        public string ConnStr
        {

            get
            {
                return strConnectionString;
            }
            set
            {
                strConnectionString = value;
            }
        }
        public string PdfPath
        {
            get
            {

                return strPdfFilePath;
            }

            set
            {
                strPdfFilePath = value;

            }
        }

        public string HeaderXSLEdorsement_Path { get; set; }
        public string FinalPDFPath { get; set; }
        public string CompanyLogo { get; set; }
        public string DocumentCode { get; set; }
        public string ClauseFilePath { get; set; }
        public string RTF_To_Html { get; set; }

        //start
        public string CoverXSLEndorsement_Path { get; set; }
        public string CoverageXSLEndorsement_Path { get; set; }
        //Coverage Comprehensive Dwelling
        public string Xsl_Coverage_114_Comprehensive_Dwelling { get; set; }
        public string Xsl_Coverage_114_More_Than_One_Broker_Dwelling { get; set; }
        public string Xsl_Coverage_114_More_Than_One_Broker__With_Ceded_COI_Dwelling { get; set; }

        //Coverage Comprehensive Condominium
        public string Xsl_Coverage_116_Comprehensive_Condominium { get; set; }
        public string Xsl_Coverage_116_More_Than_One_Broker_Condominium { get; set; }
        public string Xsl_Coverage_116_More_Than_One_Broker__With_Ceded_COI_Condominium { get; set; }

        //Coverage Comprehensive Company
        public string Xsl_Coverage_118_Comprehensive_Company { get; set; }
        public string Xsl_Coverage_118_More_Than_One_Broker_Company { get; set; }
        public string Xsl_Coverage_118_More_Than_One_Broker__With_Ceded_COI_Company { get; set; }

        //Coverage Civil Liability Transprtation
        public string Xsl_Coverage_523_Liability_Transportation { get; set; }
        public string Xsl_Coverage_523_Liability_Transportation_withPremium { get; set; }
        public string Xsl_Coverage_553_Facultive_Liability { get; set; }

        //Coverage  Diversified Risk
        public string xsl_Coverage_171_Diversified_Risk { get; set; }
        public string xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker { get; set; }
        public string xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker_With_Ceded_COI { get; set; }

        //Coverage  Individual Personal Accident

        //Ceded Coinsurance
        public string Xsl_Coverage_981_Individual_personal_accident { get; set; }

        //More then One Broker  
        public string Xsl_Coverage_981_More_Than_One_Broker_Policy { get; set; }

        //Endorsment Type 21,31, for 553,523
        public string Endo_type_21_23_553_Facultative_Liability { get; set; }
        public string Endorsement_type_31_553_Facultative_Liability { get; set; }
        public string Endorsement_type_31_553_D_015_Facultive_Liability { get; set; }
        public string Endorsement_type_31_523_Civil_Liability_Transportation { get; set; }

        //Endorsment Type 21,31, for 118,116,114
        public string Endo_Type_21_0114_0116_0118_0196_0115_0167 { get; set; }
        public string XslFilepathCover_Endorsement_Type_21_0114 { get; set; }

        //Endorsment Type 21,31, for 993,982
        public string Xsl_Coverage_D_012_Endorsement_type_0993_v2 { get; set; }
        public string Xsl_Coverage_D_012_Endorsement_type_0982_v2 { get; set; }

        //COI and More then one broker 
        public string Xsl_COI_More_Thn_One_Broker_Policy { get; set; }
        public string Xsl_COI_More_Thn_One_Broker_Policy_Endorsement { get; set; }


        public string Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2 { get; set; }

        public string Xsl_Coverage_D_037_Endorsement_type_31_Product_0520_v1 { get; set; }//new template added for 0520
        public string Xsl_Coverage_981_More_Than_One_Broker_Policy_with_Ceded_coinsurance { get; set; }

        //Renewal Policy
        public string Xsl_Cover_Renewal { get; set; }
        public string Xsl_Header_Renewal { get; set; }

        //Cancel Policy
        public string Xsl_Cover_Cancel_Policy { get; set; }
        public string Xsl__header_Cancel_Policy { get; set; }
        //NBS
        public string CoverXslNewBusiness_Path { get; set; }
        public string HeaderXslNewBusiness_Path { get; set; }
        //till here
        public int CUSTOMER_Id { get; set; }
        public int Policy_Id { get; set; }
        public int Policy_Version { get; set; }
        public string Document_Name { get; set; }
        public string Document_Path { get; set; }
        public string DOCUMENT_Code { get; set; }
        public string URL_Path { get; set; }
        public string ENTITY_TYPE { get; set; }
        public int Print_Job_Id { get; set; }
        public string DocumentType { get; set; }
        public string IDomain { get; set; }
        public string IUserName { get; set; }
        public string IPassWd { get; set; }
        public string Impersonate_Inactive { get; set; }
        public int Claim_Id { get; set; }
        public int Activity_Id { get; set; }
        public int Entity_Id { get; set; }
        public int _ChkONTheyFly { get; set; }


        #endregion "Properties"


        #region "Type of Document"
        public string GetPDdfDataForDocumentType()
        {
            DataWrapper objDataWrapper = null;
            try
            {
                string LogDetail;
                _ChkONTheyFly = 1;
                SuperTheDoc = new WebSupergoo.ABCpdf7.Doc();
                TheDoc = new WebSupergoo.ABCpdf7.Doc();
                /* Below Code is used to Disable the Microsoft XPS Document Writer printer Settings */
                SuperTheDoc.HtmlOptions.HostWebBrowser = false;
                TheDoc.HtmlOptions.HostWebBrowser = false;
                TheDoc.HtmlOptions.CoerceVector = WebSupergoo.ABCpdf7.HtmlRenderConditions.Never;
                SuperTheDoc.HtmlOptions.CoerceVector = WebSupergoo.ABCpdf7.HtmlRenderConditions.Never;

                string strStoredProc = "Proc_Fetch_DocumentCode";
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                DataTable DSDocmentDetail = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
                GenerateDocumentsBulk(DSDocmentDetail);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                TheDoc.Clear();
                TheDoc.Dispose();
                SuperTheDoc.Clear();
                SuperTheDoc.Dispose();
                DSDocmentDetail.Dispose();
                LogDetail = strException.ToString();
                if (strException.Length > 0)
                {
                    strException = strException.Remove(0, strException.Length);
                }
                return LogDetail;

            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while saving record.", ex.InnerException));
            }
            finally
            {
                objDataWrapper.Dispose();
            }


        }
        /// <summary>
        /// This funtion is used in the case where documents are generated on the Fly, without waiting for service to generate document
        /// </summary>
        /// New Parameter print job id is applied to generate selected document.
        public int GeneratePolicyDoc_OntheFly(int CustomerID, int PolicyID, int PolicyVersionID, string DocumentCode, int ClaimID, int ActivityID, int ChkOnTheFily, out string strErrorDesc, int Print_Job_Id)
        {
            strErrorDesc = "Clause File Path Replaced";
            ClauseFilePath = ClauseFilePath.Replace(ClauseFilePath, PdfPath + ClauseFilePath);
            DataSet DSDocmentDetail = new DataSet();
            int retValue = 0;
            try
            {

                try
                {
                    strErrorDesc += "intialization of abcpdf objects;";
                    _ChkONTheyFly = ChkOnTheFily;
                    SuperTheDoc = new WebSupergoo.ABCpdf7.Doc();
                    TheDoc = new WebSupergoo.ABCpdf7.Doc();
                    /* Below Code is used to Disable the Microsoft XPS Document Writer printer Settings */
                    SuperTheDoc.HtmlOptions.HostWebBrowser = false;
                    TheDoc.HtmlOptions.HostWebBrowser = false;
                    TheDoc.HtmlOptions.CoerceVector = WebSupergoo.ABCpdf7.HtmlRenderConditions.Never;
                    SuperTheDoc.HtmlOptions.CoerceVector = WebSupergoo.ABCpdf7.HtmlRenderConditions.Never;
                    strErrorDesc += "abcpdf objects Initialized";
                }
                catch (Exception ex)
                {
                    throw (new Exception(ex.Message.ToString() + ex.InnerException.ToString() + "error1:Error in ABCPDF Object Initialization.", ex.InnerException));
                }


                strErrorDesc += "Fetching Data;";
                DataWrapper objDataWrapper = null;
                string strStoredProc = "Proc_Fetch_DocCodePolicy";
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CustomerID", CustomerID);
                objDataWrapper.AddParameter("@PolicyID", PolicyID);
                objDataWrapper.AddParameter("@PolicyVersionID", PolicyVersionID);
                objDataWrapper.AddParameter("@DocumentCode", DocumentCode);
                objDataWrapper.AddParameter("@ClaimID", ClaimID);
                objDataWrapper.AddParameter("@ActivityID", ActivityID);
                objDataWrapper.AddParameter("@Print_Job_ID", Print_Job_Id);
                DSDocmentDetail = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
                strErrorDesc += "Data Fetched from DB;";
                if (DSDocmentDetail != null && DSDocmentDetail.Tables.Count > 0 && DSDocmentDetail.Tables[0].Rows.Count > 0)
                {

                    String _DocumentType = DSDocmentDetail.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString();
                    Int32 _CUSTOMER_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
                    Int32 _Policy_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["POLICY_ID"].ToString());
                    Int32 _Policy_Version = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
                    Int32 _Print_Job_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["PRINT_JOBS_ID"].ToString());
                    String _Document_Name = DSDocmentDetail.Tables[0].Rows[0]["FILE_NAME"].ToString();
                    String _Document_Path = DSDocmentDetail.Tables[0].Rows[0]["URL_PATH"].ToString();
                    String _ENTITY_TYPE = DSDocmentDetail.Tables[0].Rows[0]["ENTITY_TYPE"].ToString();
                    Int32 _Claim_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["ClAIM_ID"].ToString());
                    Int32 _Activity_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["ACTIVITY_ID"].ToString());
                    Int32 _Entity_Id = Convert.ToInt32(DSDocmentDetail.Tables[0].Rows[0]["ENTITY_ID"].ToString());
                    strErrorDesc += "Variables Initialised and  GenerateDocument() Method called;";
                    GenerateDocument(_DocumentType, _CUSTOMER_Id, _Policy_Id, _Policy_Version, _Print_Job_Id, _Document_Name,
                        _Document_Path, _ENTITY_TYPE, _Claim_Id, _Activity_Id, _Entity_Id);
                    strErrorDesc += "GenerateDocument() Method executed;";
                    retValue = 1;
                }
                else
                    retValue = -2;//if the ATTEMPTS reached the maximum level then retun -2
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message.ToString() + strErrorDesc));
            }

            finally
            {
                TheDoc.Clear();
                TheDoc.Dispose();
                SuperTheDoc.Clear();
                SuperTheDoc.Dispose();
                DSDocmentDetail.Dispose();

            }
            return retValue;
        }



        private void GenerateDocumentsBulk(DataTable DSDocmentDetail)
        {
            ClauseFilePath = ClauseFilePath.Replace(ClauseFilePath, PdfPath + ClauseFilePath);
            try
            {

                if (DSDocmentDetail != null)
                {

                    if (DSDocmentDetail.Rows.Count > 0)
                    {
                        foreach (DataRow dr in DSDocmentDetail.Rows)
                        {
                            DocumentType = dr["DOCUMENT_CODE"].ToString();
                            CUSTOMER_Id = Convert.ToInt32(dr["CUSTOMER_ID"].ToString());
                            Policy_Id = Convert.ToInt32(dr["POLICY_ID"].ToString());
                            Policy_Version = Convert.ToInt32(dr["POLICY_VERSION_ID"].ToString());
                            Print_Job_Id = Convert.ToInt32(dr["PRINT_JOBS_ID"].ToString());
                            Document_Name = dr["FILE_NAME"].ToString();
                            Document_Path = dr["URL_PATH"].ToString();
                            ENTITY_TYPE = dr["ENTITY_TYPE"].ToString();
                            Claim_Id = Convert.ToInt32(dr["ClAIM_ID"]);
                            Activity_Id = Convert.ToInt32(dr["ACTIVITY_ID"]);
                            Entity_Id = Convert.ToInt32(dr["ENTITY_ID"]);

                            try
                            {

                                string strMessage = "";
                                strMessage = GenerateDocuments(DocumentType, CUSTOMER_Id, Policy_Id, Policy_Version, Print_Job_Id, Document_Name, Document_Path, ENTITY_TYPE, Claim_Id, Activity_Id, Entity_Id);
                                strException.Append(strMessage.Trim());
                                strException.Append(Environment.NewLine);


                            }
                            catch (Exception ex)
                            {
                                strException = strException.Append(ex.Message.ToString());
                                strException = strException.Append(Environment.NewLine);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strException = strException.Append(ex.Message.ToString());
                strException = strException.Append(Environment.NewLine);

            }

        }


        private string GenerateDocuments(string strDocumentType, int iCUSTOMER_Id, int iPolicy_Id, int iPolicy_Version, int iPrint_Job_Id, string strDocument_Name, string strDocument_Path, string strENTITY_TYPE, int iClaim_Id, int iActivity_Id, int iEntity_Id)
        {
            return GenerateDocument(strDocumentType, iCUSTOMER_Id, iPolicy_Id, iPolicy_Version, iPrint_Job_Id, strDocument_Name, strDocument_Path, strENTITY_TYPE, iClaim_Id, iActivity_Id, iEntity_Id);
        }

        /// <summary>
        /// This function work for different type of document 
        /// This funtion  is exected on the basis of Document Code like Boleto
        /// </summary>
        private string GenerateDocument(string strDocumentType, int iCUSTOMER_Id, int iPolicy_Id, int iPolicy_Version, int iPrint_Job_Id, string strDocument_Name, string strDocument_Path, string strENTITY_TYPE, int iClaim_Id, int iActivity_Id, int iEntity_Id)
        {

            StringBuilder strException = new StringBuilder();

            try
            {
                DocumentType = strDocumentType;
                CUSTOMER_Id = iCUSTOMER_Id;
                Policy_Id = iPolicy_Id;
                Policy_Version = iPolicy_Version;
                Print_Job_Id = iPrint_Job_Id;
                Document_Name = strDocument_Name;
                Document_Path = strDocument_Path;
                ENTITY_TYPE = strENTITY_TYPE;
                Claim_Id = iClaim_Id;
                Activity_Id = iActivity_Id;
                Entity_Id = iEntity_Id;
                switch (DocumentType)
                {
                    case "BOLETO":
                        try
                        {
                            int FlagdatasetNull = 0;
                            if (GenerateBoleto(CUSTOMER_Id, Policy_Id, Policy_Version, Document_Name, Document_Path, Print_Job_Id, ref FlagdatasetNull) == true)
                            {

                                if (FlagdatasetNull == 1)//if dataset value is null
                                {
                                    strException = strException.Append("No matching record found for - " + Document_Name + " " + "at" + " " + DateTime.Now.ToString());
                                    strException = strException.Append(Environment.NewLine);
                                }
                                else
                                {
                                    strException = strException.Append("Boleto created at - " + DateTime.Now.ToString() + '-' + Document_Name);
                                    strException = strException.Append(Environment.NewLine);
                                }
                            }
                            else
                            {
                                strException = strException.Append("Can not Create Pdf, impersonation is not active.");
                                strException = strException.Append(Environment.NewLine);
                            }

                        }
                        catch (Exception ex)
                        {
                            strException = strException.Append(ex.Message.ToString());
                            strException = strException.Append(Environment.NewLine);
                            throw (new Exception(strException.ToString(), ex.InnerException));

                        }

                        break;

                    case "POLICY_DOC":
                        try
                        {
                            int FlagdatasetNull = 0;

                            if (GeneratePolicyDocument(CUSTOMER_Id, Policy_Id, Policy_Version, Document_Name, Document_Path, Print_Job_Id, ref  FlagdatasetNull) == true)
                            {
                                if (FlagdatasetNull == 1)
                                {
                                    strException = strException.Append("No matching record found for - " + Document_Name + " " + "at" + " " + DateTime.Now.ToString());
                                    strException = strException.Append(Environment.NewLine);
                                }
                                else
                                {
                                    strException = strException.Append("Policy PDF created  - " + Document_Name);
                                    strException = strException.Append(Environment.NewLine);
                                }
                            }
                            else
                            {
                                strException = strException.Append("can not create pdf. impersonation is not active.");
                                strException = strException.Append(Environment.NewLine);
                            }

                        }
                        catch (Exception ex)
                        {
                            strException = strException.Append(ex.Message.ToString());
                            strException = strException.Append(Environment.NewLine);
                            throw (new Exception(strException.ToString(), ex.InnerException));

                        }

                        break;

                    case "REFUSAL":
                        try
                        {
                            int FlagdatasetNull = 0;
                            if (GenerateRefusal_Lettter(CUSTOMER_Id, Policy_Id, Policy_Version, Document_Name, Document_Path, Print_Job_Id, ref  FlagdatasetNull) == true)
                            {
                                if (FlagdatasetNull == 1)
                                {
                                    strException = strException.Append("No matching record found for - " + Document_Name + " " + "at" + " " + DateTime.Now.ToString());
                                    strException = strException.Append(Environment.NewLine);
                                }
                                else
                                {
                                    strException = strException.Append("Refusal Letter created  - " + Document_Name);
                                    strException = strException.Append(Environment.NewLine);
                                }
                            }
                            else
                            {
                                strException = strException.Append("Can not create pdf.impersonation not active.");
                                strException = strException.Append(Environment.NewLine);
                            }

                        }
                        catch (Exception ex)
                        {
                            strException = strException.Append(ex.Message.ToString());
                            strException = strException.Append(Environment.NewLine);
                            throw (new Exception(strException.ToString(), ex.InnerException));

                        }
                        break;

                    case "CANC_NOTICE":

                        try
                        {
                            int FlagdatasetNull = 0;

                            if (GenerateCancel_Lettter(CUSTOMER_Id, Policy_Id, Policy_Version, Document_Name, Document_Path, Print_Job_Id, ref  FlagdatasetNull) == true)
                            {
                                if (FlagdatasetNull == 1)
                                {
                                    strException = strException.Append("No matching record found for - " + Document_Name + " " + "at" + " " + DateTime.Now.ToString());
                                    strException = strException.Append(Environment.NewLine);
                                }
                                else
                                {
                                    strException = strException.Append("Cancel notice  created  - " + Document_Name);
                                    strException = strException.Append(Environment.NewLine);
                                }
                            }
                            else
                            {
                                strException = strException.Append("Can not create pdf.impersonation not active.");
                                strException = strException.Append(Environment.NewLine);
                            }

                        }
                        catch (Exception ex)
                        {
                            strException = strException.Append(ex.Message.ToString());
                            strException = strException.Append(Environment.NewLine);
                            throw (new Exception(strException.ToString(), ex.InnerException));

                        }
                        break;

                    case "CLM_RECEIPT":

                        try
                        {
                            int FlagdatasetNull = 0;

                            if (Generate_Claim_Receipt(Claim_Id, Activity_Id, Document_Name, Document_Path, Print_Job_Id, ref  FlagdatasetNull) == true)
                            {
                                if (FlagdatasetNull == 1)
                                {
                                    strException = strException.Append("No matching record found for - " + Document_Name + " " + "at" + " " + DateTime.Now.ToString());
                                    strException = strException.Append(Environment.NewLine);
                                }
                                else
                                {
                                    strException = strException.Append("Claim Receipt created  - " + Document_Name);
                                    strException = strException.Append(Environment.NewLine);
                                }
                            }
                            else
                            {
                                strException = strException.Append("Can not create pdf.impersonation not active.");
                                strException = strException.Append(Environment.NewLine);
                            }

                        }
                        catch (Exception ex)
                        {
                            strException = strException.Append(ex.Message.ToString());
                            strException = strException.Append(Environment.NewLine);
                            throw (new Exception(strException.ToString(), ex.InnerException));

                        }

                        break;

                    case "CLM_REMIND":

                        try
                        {
                            int FlagdatasetNull = 0;

                            if (Generate_Remind_Lettter(Claim_Id, Activity_Id, Document_Name, Document_Path, Print_Job_Id, ref  FlagdatasetNull) == true)
                            {
                                if (FlagdatasetNull == 1)
                                {
                                    strException = strException.Append("No matching record found for - " + Document_Name + " " + "at" + " " + DateTime.Now.ToString());
                                    strException = strException.Append(Environment.NewLine);
                                }
                                else
                                {
                                    strException = strException.Append("Remind Letter  created  - " + Document_Name);
                                    strException = strException.Append(Environment.NewLine);
                                }
                            }
                            else
                            {
                                strException = strException.Append("Can not create pdf.impersonation not active.");
                                strException = strException.Append(Environment.NewLine);
                            }

                        }
                        catch (Exception ex)
                        {
                            strException = strException.Append(ex.Message.ToString());
                            strException = strException.Append(Environment.NewLine);
                            throw (new Exception(strException.ToString(), ex.InnerException));

                        }

                        break;

                }

                return strException.ToString();
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message.ToString() + "error3", ex.InnerException));
            }
        }

        #endregion "End Type OF Document"

        #region "Boleto"
        private Boolean GenerateBoleto(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id, string DocumentName, string DocumentPath, int PrintJobID, ref int FlagdatasetNull)
        {
            string DestinationFile_Path = "";
            Boolean retValue = false;
            string Release_Status = "";
            try
            {

                DataSet DS_PDF_Boleto = GetPdfDataForBoleto(CUSTOMER_Id, Policy_Id, Policy_Version_Id);

                if (DS_PDF_Boleto.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS_PDF_Boleto.Tables[0].Rows)
                    {

                        HtmlString = dr["BOLETO_HTML"].ToString();
                        Release_Status = dr["RELEASED_STATUS"].ToString();

                        if (HtmlString.ToString() != "")
                        {
                            GenerateBoletoPdf(HtmlString, Release_Status, ref TheDoc);
                            SuperTheDoc.Append(TheDoc);
                        }
                    }
                    if (CreateDirectory(DocumentPath))
                    {
                        if (FinalPDFPath == "final")
                        {
                            DestinationFile_Path = appPath + "/" + DocumentName;
                        }
                        if (ImpersonateUser(IUserName, IPassWd, IDomain))
                        {
                            SuperTheDoc.Save(DestinationFile_Path);

                            UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version, PrintJobID, true, _ChkONTheyFly);
                            endImpersonation();
                            retValue = true;
                        }
                        else
                        {
                            retValue = false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }

                else
                {
                    retValue = true;
                    FlagdatasetNull = 1;
                    UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version, PrintJobID, false, _ChkONTheyFly);
                }

                DS_PDF_Boleto.Dispose();
            }
            catch (Exception ex)
            {
                retValue = false;
                UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version, PrintJobID, false, _ChkONTheyFly);
                throw (new Exception("Error  in Creating Boleto:  " + ex.Message.ToString(), ex.InnerException));


            }
            finally
            {
                TheDoc.Clear();
                SuperTheDoc.Clear();
            }
            return retValue;
        }
        private DataSet GetPdfDataForBoleto(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id)
        {
            DataWrapper objDataWrapper = null;

            string strStoredProc = "Proc_Fetch_INSTALLMENT_BOLETO";
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            DataSet DS_Boleto = new DataSet();
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_Id);
                objDataWrapper.AddParameter("@POLICY_ID", Policy_Id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_Version_Id);
                DS_Boleto = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return DS_Boleto;

        }
        private void GenerateBoletoPdf(string HtmlFile, string release_status, ref WebSupergoo.ABCpdf7.Doc TheDoc)
        {

            string BoletoHtml = "";

            try
            {

                BoletoHtml = this.GetHTML(BoletoCssPath, "TDR");
                StringBuilder HTML_Boleto = new StringBuilder();
                string newhtml = HtmlFile.Replace("/Cms/CmsWeb/images/Boleto/", CompanyLogo);
                HTML_Boleto = HTML_Boleto.Append(newhtml);
                HTML_Boleto = HTML_Boleto.Append(BoletoHtml);
                string finalstring = HTML_Boleto.ToString();
                int index = finalstring.IndexOf("ImagemCodigoBarra.ashx?code=");
                string substring = finalstring.Substring(index);
                substring = substring.Substring(0, substring.IndexOf("\""));
                int indexcode = substring.IndexOf("code=");
                string code = substring.Substring(indexcode + 5);
                string BarcodeImagepath = BarcodeImagePath;

                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    if (File.Exists(BarcodeImagepath))
                    {
                        File.Delete(BarcodeImagepath);
                    }
                    System.Drawing.Bitmap img = new C2of5i(code, 1, 50, code.Length).ToBitmap();
                    img.Save(BarcodeImagepath, ImageFormat.Jpeg);
                    img.Dispose();
                    endImpersonation();
                }
                string finalhtml = finalstring.Replace(substring, BarcodeImagepath);

                int theID = 0;
                TheDoc.Rect.String = "50 50 580 680";
                theID = TheDoc.AddImageHtml(finalhtml);
                if (release_status == "Y")
                {
                    TheDoc.HPos = 0;
                    TheDoc.VPos = 0;
                    TheDoc.Rect.SetRect(100, 265, 500, 80);
                    TheDoc.FontSize = 72;
                    TheDoc.Color.String = "0 0 0 a70";
                    TheDoc.AddText("P    A     G    O");
                }
            }
            catch (Exception ex)
            {

                throw (new Exception("Error  in Creating Boleto:  " + ex.Message.ToString(), ex.InnerException));


            }



        }
        #endregion "End Boleto"


        #region "Policy"
        private Boolean GeneratePolicyDocument(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id, string DocumentName, string DocumentPath, int PrintJOBID, ref int FlagdatasetNull)
        {
            string DestinationFile_Path = "";
            Boolean retValue = false;
            try
            {
                string Policy_Pdf_XML = "";
                DataSet DS_Policy = GetPdfDataForPolicy(CUSTOMER_Id, Policy_Id, Policy_Version_Id);
                if (DS_Policy.Tables[0].Rows.Count > 0)
                {



                    Policy_Pdf_XML = DS_Policy.Tables[0].Rows[0]["DEC_CUSTOMERXML"].ToString();
                    Policy_Pdf_XML = Policy_Pdf_XML.Replace("<POLICY_DOCUMENTS>", "<POLICY_DOCUMENTS><IMAGE_PATH>" + _Imagepath + "</IMAGE_PATH>");
                    if (CreateDirectory(DocumentPath))
                    {

                        if (FinalPDFPath == "final")
                        {
                            DestinationFile_Path = appPath + "/" + DocumentName;
                        }
                        GeneratePolicyPdf(HtmlString, Policy_Pdf_XML, ref TheDoc);
                        if (ImpersonateUser(IUserName, IPassWd, IDomain))
                        {
                            TheDoc.Save(DestinationFile_Path);
                            UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJOBID, true, _ChkONTheyFly);
                            endImpersonation();
                            retValue = true;
                        }
                        else
                            retValue = false;

                    }
                    else
                    {
                        retValue = false;
                    }
                }
                else
                {
                    retValue = true;
                    FlagdatasetNull = 1;
                    UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJOBID, false, _ChkONTheyFly);
                }
                DS_Policy.Dispose();

            }
            catch (Exception ex)
            {
                retValue = false;
                UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJOBID, false, _ChkONTheyFly);
                throw (new Exception("Error Generating in Policy: " + ex.Message.ToString(), ex.InnerException));

            }
            finally
            {
                TheDoc.Clear();
                SuperTheDoc.Clear();


            }
            return retValue;
        }
        private DataSet GetPdfDataForPolicy(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id)
        {
            DataWrapper objDataWrapper = null;

            string strStoredProc = "Proc_GetXMLToGeneratePdf";
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            DataSet DSpdfData = new DataSet();
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@Customer_Id", CUSTOMER_Id);
                objDataWrapper.AddParameter("@Policy_ID", Policy_Id);
                objDataWrapper.AddParameter("@Policy_Version_ID", Policy_Version_Id);
                DSpdfData = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
            return DSpdfData;

        }
        private void GeneratePolicyPdf(string HtmlFile, string XML, ref  WebSupergoo.ABCpdf7.Doc TheDoc)
        {
            string ClauseFileHtml = "";
            int theFont1 = TheDoc.EmbedFont("Arial", "English", false, true);
            StringBuilder Strhtml_Clause = new StringBuilder();
            WebSupergoo.ABCpdf7.Doc TheDoc_Coverage = new WebSupergoo.ABCpdf7.Doc();
            WebSupergoo.ABCpdf7.Doc TheDoc_Caluse = new WebSupergoo.ABCpdf7.Doc();
            int theID = 0, TheID_Coverage = 0, theCount = 0, i = 0, ctr = 0;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XML);
                string CoverHtml = CoverPage_Html(XML);

                XmlNodeList nList = doc.GetElementsByTagName("PROCESS");
                if (nList.Count > 0)
                {
                    for (ctr = 0; ctr < nList.Count; ctr++)
                    {
                        ProcessCode = nList.Item(ctr).InnerText;

                    }
                }


                switch (ReadXmlFileTag("INSTALLMENT_NO", XML).Count)
                {


                    case 0://OK
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:3.20in;");
                        break;
                    case 1://OK
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:3.15in;");
                        break;
                    case 2:
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:3.00in;");
                        break;
                    case 3://OK
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:2.80in;");
                        break;
                    case 4://OK
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:2.40in;");
                        break;
                    case 5://OK
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:2.35in;");
                        break;
                    case 6:
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:2.20in;");
                        break;
                    case 7:
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:1.88in;");
                        break;
                    case 8://OK
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:1.70in;");
                        break;
                    case 9://ok
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:1.60in;");
                        break;
                    case 10://ok
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:1.45in;");
                        break;
                    case 11://ok
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:1.25in;");
                        break;
                    case 12://ok
                        CoverHtml = CoverHtml.Replace("style=\"height:2.70in;", "style=\"height:0.10in;");
                        break;


                }


                //"50 -180 560 630";
                //X,W-left,top-y and h, 
                // "20 300 580 603";
                //"30 -50 580 590";
                //"20 -50 597 590";
                //"20 -50 597 590";
                //"3 -50 611 590";
                //"3 100 611 645";
                // left top right  bottom
                // "3   -50   611   594";
                // "3 -50 611 594";
                //"3 0 611 623";
                //"3 -20 611 611";
                //ok figures for NBS:"3 0 611 620";
                //ok figures for NBS:"3 -10 611 611";
                //to do marigns on the top and bottom we need to handle top and bottom at same time , if we are increasing top we need to increase bootom 2 AND VISE VERSA
                //right column-- when goes up,the up border goes up
                // 2nd from left--when goes up down border goes up

                if (ProcessCode == "ENDOSSO")
                {
                    TheDoc.Rect.String = "3 0 611 620";
                }
                else
                {
                    TheDoc.Rect.String = "3 0 611 619";
                }
                theID = TheDoc.AddImageHtml(CoverHtml);
                //right column-- when goes up,the up border goes up
                //2nd from left--when goes up down border goes up
                TheDoc_Coverage.Rect.String = "14 30 600 606";

                int count = 0;


                //XmlNodeList nodList = doc.GetElementsByTagName("SUSEP_LOB_CODE");
                //if (nodList.Count > 0)
                //{
                //    for (count = 0; count < nodList.Count; count++)
                //    {
                //        ProductCode = nodList.Item(count).InnerText;

                //    }
                //}

                XmlNodeList nodListPolicyLob = doc.GetElementsByTagName("POLICY_LOB");
                if (nodListPolicyLob.Count > 0)
                {
                    for (count = 0; count < nodListPolicyLob.Count; count++)
                    {
                        Policy_LOB = nodListPolicyLob.Item(count).InnerText;

                    }
                }
                int countEndoso = 0;
                string Endorsment_ID = "";
                XmlNodeList nodListEndosso = doc.GetElementsByTagName("ENDORSEMENT_TYPE_ID");
                if (nodListEndosso.Count > 0)
                {
                    for (countEndoso = 0; countEndoso < nodListEndosso.Count; countEndoso++)
                    {
                        Endorsment_ID = nodListEndosso.Item(countEndoso).InnerText;

                    }
                }
                XmlNodeList RiskTag = doc.GetElementsByTagName("RISKINFO");
                switch (Policy_LOB)
                {
                    case "11"://Comprehensive Company  

                        if (RiskTag.Count > 0)
                        {
                            if (ProcessCode == "ENDOSSO")
                            {
                                if (Endorsment_ID == "14682")
                                {

                                    if (Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML) != "")
                                    {
                                        TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML));
                                        TheDoc_Coverage.Color.Gray = 200;
                                        TheDoc_Coverage.FrameRect();
                                    }
                                }
                            }
                            else
                            {

                                if (Coverage_Comprehensive_Company(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Comprehensive_Company(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }
                        break;
                    case "19"://Comprehensive Dwelling 

                        if (RiskTag.Count > 0)
                        {

                            if (ProcessCode == "ENDOSSO")
                            {
                                if (Endorsment_ID == "14682")
                                {

                                    if (Coverage_Endorsement_Type_21_0114(XML) != "")
                                    {
                                        TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endorsement_Type_21_0114(XML));
                                        TheDoc_Coverage.Color.Gray = 200;
                                        TheDoc_Coverage.FrameRect();
                                    }
                                }
                            }
                            else
                            {

                                if (Coverage_Comprehensive_Dwelling(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Comprehensive_Dwelling(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }
                        break;

                    case "10"://Comprehensive Condominium

                        if (RiskTag.Count > 0)
                        {
                            if (ProcessCode == "ENDOSSO")
                            {
                                if (Endorsment_ID == "14682")
                                {
                                    if (Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML) != "")
                                    {
                                        TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML));
                                        TheDoc_Coverage.Color.Gray = 200;
                                        TheDoc_Coverage.FrameRect();
                                    }

                                }
                            }
                            else
                            {

                                if (Coverage_Comprehensive_Condominium(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Comprehensive_Condominium(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }

                        break;




                    case "18": //Civil Liability Transportation
                        if (Coverage_Civil_Libility_Transportation(XML) != "")
                        {
                            if (ProcessCode == "ENDOSSO")
                            {
                                if (Endorsment_ID == "14682")//Additional ,21 D014 template is used, which is  same as DO16 Template of 553
                                {

                                    if (Coverage_Endo_type_21_23_553_Facultative_Liability(XML) != "")
                                    {
                                        TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endo_type_21_23_553_Facultative_Liability(XML));
                                        TheDoc_Coverage.Color.Gray = 200;
                                        TheDoc_Coverage.FrameRect();
                                    }


                                }
                                if (Endorsment_ID == "14690")//monthly billing,31,
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endo_type_31_D013_523_Civil_Liability_Transportation(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }

                            }
                            else
                            {
                                if (Coverage_Civil_Libility_Transportation(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Civil_Libility_Transportation(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }
                        break;
                    case "17":
                        if (ProcessCode == "ENDOSSO")
                        {
                            if (Endorsment_ID == "14682") //21,additional endorsement D-0116
                            {
                                if (Coverage_Endo_type_21_23_553_Facultative_Liability(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endo_type_21_23_553_Facultative_Liability(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }

                            if (Endorsment_ID == "14690")//monthly billing D-015
                            {
                                if (Coverage_Endo_type_31_D015_553_Facultive_Liability(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endo_type_31_D015_553_Facultive_Liability(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }
                        else
                        {
                            if (Coverage_Facultive_Liability(XML) != "") //                       
                            {
                                TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Facultive_Liability(XML));
                                TheDoc_Coverage.Color.Gray = 200;
                                TheDoc_Coverage.FrameRect();
                            }
                        }
                        break;

                    case "9":
                        int countLob = 0;
                        string POLICY_SUBLOB = "";
                        XmlDocument doc1 = new XmlDocument();
                        doc1.LoadXml(XML);
                        XmlNodeList nodListLob = doc1.GetElementsByTagName("POLICY_SUBLOB");
                        if (nodListLob.Count > 0)
                        {
                            for (countLob = 0; countLob < nodListLob.Count; countLob++)
                            {
                                POLICY_SUBLOB = nodListLob.Item(countLob).InnerText;

                            }
                        }
                        if (ProcessCode == "ENDOSSO")
                        {
                            if (Endorsment_ID == "14682")
                            {
                                if (Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }
                        else
                        {
                            //Modified by Naveen(refer itrack 1270)

                            if (POLICY_SUBLOB == "2")
                            {
                                if (Coverage_Comprehensive_Company(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Comprehensive_Company(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                            else
                            {
                                string strCOI_More_Thn_One_Broker_0196 = COI_More_Thn_One_Broker(XML);
                                if (strCOI_More_Thn_One_Broker_0196 != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(strCOI_More_Thn_One_Broker_0196);
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();

                                }
                            }
                        }

                        break;
                    case "14":

                        if (ProcessCode == "ENDOSSO")
                        {
                            if (COI_More_Thn_One_Broker(XML) != "")
                            {
                                TheID_Coverage = TheDoc_Coverage.AddImageHtml(COI_More_Thn_One_Broker(XML));
                                TheDoc_Coverage.Color.Gray = 200;
                                TheDoc_Coverage.FrameRect();
                            }

                        }
                        else
                        {
                            if (Coverage_Diversified_Risk(XML) != "")
                            {
                                TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Diversified_Risk(XML));
                                TheDoc_Coverage.Color.Gray = 200;
                                TheDoc_Coverage.FrameRect();
                            }
                        }
                        break;


                    case "34":
                    case "21":
                    case "22":
                        if (ProcessCode == "ENDOSSO")
                        {
                            if (Endorsment_ID == "14690")
                            {
                                if (Coverage_Endo_D012_31_0520_0982_0993(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endo_D012_31_0520_0982_0993(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }
                        else
                        {
                            if (COI_More_Thn_One_Broker(XML) != "")
                            {
                                TheID_Coverage = TheDoc_Coverage.AddImageHtml(COI_More_Thn_One_Broker(XML));
                                TheDoc_Coverage.Color.Gray = 200;
                                TheDoc_Coverage.FrameRect();
                            }
                        }

                        break;
                    case "16":
                    case "26":
                        if (ProcessCode == "ENDOSSO")
                        {
                            if (Endorsment_ID == "14682") //21,additional endorsement D-0116
                            {
                                if (Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML) != "")
                                {
                                    TheID_Coverage = TheDoc_Coverage.AddImageHtml(Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(XML));
                                    TheDoc_Coverage.Color.Gray = 200;
                                    TheDoc_Coverage.FrameRect();
                                }
                            }
                        }
                        else
                        {
                            if (COI_More_Thn_One_Broker(XML) != "")
                            {
                                TheID_Coverage = TheDoc_Coverage.AddImageHtml(COI_More_Thn_One_Broker(XML));
                                TheDoc_Coverage.Color.Gray = 200;
                                TheDoc_Coverage.FrameRect();
                            }

                        }
                        break;
                    case "15":

                        //Applied codtion for endorsement  of  product 0981. refer itrack 1432.
                        if (ProcessCode == "ENDOSSO")
                        {

                            if (COI_More_Thn_One_Broker(XML) != "")
                            {
                                TheID_Coverage = TheDoc_Coverage.AddImageHtml(COI_More_Thn_One_Broker(XML));
                                TheDoc_Coverage.Color.Gray = 200;
                                TheDoc_Coverage.FrameRect();
                            }

                        }
                        else
                        {

                            string strCoverage_Individual_Personal_Accident = Coverage_Individual_Personal_Accident(XML);
                            if (strCoverage_Individual_Personal_Accident != "")
                            {
                                TheID_Coverage = TheDoc_Coverage.AddImageHtml(strCoverage_Individual_Personal_Accident);
                                TheDoc_Coverage.Color.Gray = 200;
                                TheDoc_Coverage.FrameRect();
                            }

                        }
                        break;


                    //For all product with coi and multibroker session. 
                    case "25":
                    case "35":
                    case "27":
                    case "12":
                    case "13":
                    case "28":
                    case "29":
                    case "36":
                    case "30":
                    case "20":
                    case "23":
                    case "31":
                    case "37":
                    case "32":
                    case "33":

                        string strCOI_More_Thn_One_Broker = COI_More_Thn_One_Broker(XML);
                        if (strCOI_More_Thn_One_Broker != "")
                        {
                            TheID_Coverage = TheDoc_Coverage.AddImageHtml(strCOI_More_Thn_One_Broker);
                            TheDoc_Coverage.Color.Gray = 200;
                            TheDoc_Coverage.FrameRect();
                        }

                        break;
                }
                while (TheDoc_Coverage.Chainable(TheID_Coverage))
                {
                    TheDoc_Coverage.Color.Gray = 200;
                    TheDoc_Coverage.Page = TheDoc_Coverage.AddPage();
                    TheDoc_Coverage.FrameRect();
                    TheID_Coverage = TheDoc_Coverage.AddImageToChain(TheID_Coverage);
                }



                int Count = 0;
                ArrayList ClauseFiles = ReadXmlFileTag("ATTACH_FILE_NAME", XML);
                ArrayList ClauseDesc = ReadXmlFileTag("CLAUSE_DESCRIPTION", XML);
                //Did changes in impersonation code itrack 761
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    if (ClauseDesc.Count > 0)
                    {
                        XmlNodeList nodList1 = doc.GetElementsByTagName("CLAUSE_DESCRIPTION");
                        if (nodList1.Count > 0)
                        {

                            for (count = 0; count < nodList1.Count; count++)
                            {
                                String ClauseDescription = nodList1.Item(count).InnerText;
                                Strhtml_Clause = Strhtml_Clause.Append(ClauseDescription);
                            }
                            for (Count = 0; Count < ClauseFiles.Count; Count++)
                            {
                                if (ClauseFiles[Count].ToString() != "" || ClauseFiles[Count].ToString() != string.Empty)
                                {

                                    if (File.Exists(ClauseFilePath + ClauseFiles[Count].ToString()))
                                    {
                                        String FileExt = ClauseFilePath + ClauseFiles[Count].ToString();
                                        string ext = Path.GetExtension(FileExt);
                                        if (ext == ".rtf" || ext == ".doc" || ext == ".docx")
                                        {
                                            ConvertRtfToHtml(FileExt);
                                            ClauseFileHtml = GetHTML(RTF_To_Html, "TDR");
                                            Strhtml_Clause = Strhtml_Clause.Append(ClauseFileHtml);
                                        }
                                        if (ext == ".htm" || ext == ".html")
                                        {
                                            string HtmlFileString = ClauseFilePath + ClauseFiles[Count].ToString();
                                            ClauseFileHtml = GetHTML(HtmlFileString, "TDR");
                                            if (!ClauseFileHtml.ToUpper().StartsWith("<HTML"))
                                                Strhtml_Clause = Strhtml_Clause.Append("<HTML><body>" + ClauseFileHtml + "<br/></body></HTML>&nbsp");
                                            else
                                                Strhtml_Clause = Strhtml_Clause.Append(ClauseFileHtml + "<br/>");

                                        }

                                    }
                                    else
                                    {
                                        throw (new Exception("Clause not found at:" + ClauseFilePath + ClauseFiles[Count].ToString()));

                                    }

                                }

                            }

                        }

                    }
                    endImpersonation();
                }

                else
                    throw (new Exception("Impersonation not active while Fething Clause at:" + ClauseFilePath + ClauseFiles[Count].ToString()));

                Strhtml_Clause = Strhtml_Clause.Replace("font-family:", "font-family:\"Arial Narrow" + "\",");
                Strhtml_Clause = Strhtml_Clause.Replace("font-size:8.0pt;", "font-size:12.0pt;");
                Strhtml_Clause = Strhtml_Clause.Replace("8.0pt;font-family", "12.0pt;font-family");

                int TheID_Caluse = 0;
                if (Strhtml_Clause.ToString() != "")
                {
                    //Did changes to resolve blank page issue. 30->20
                    TheDoc_Caluse.Rect.String = "14 20 600 606";
                    //TheDoc_Caluse.Rect.String = "40 33 570 575";
                    TheID_Caluse = 0;
                    TheID_Caluse = TheDoc_Caluse.AddImageHtml(Strhtml_Clause.ToString());
                    TheDoc_Caluse.Color.Gray = 200;
                    TheDoc_Caluse.FrameRect();
                }
                /*
                * Loop till there are more pages in the output
                */
                while (TheDoc_Caluse.Chainable(TheID_Caluse))
                {
                    TheDoc_Caluse.Color.Gray = 200;
                    TheDoc_Caluse.Page = TheDoc_Caluse.AddPage();
                    TheDoc_Caluse.FrameRect();
                    TheID_Caluse = TheDoc_Caluse.AddImageToChain(TheID_Caluse);
                }
                TheDoc.Append(TheDoc_Coverage);
                TheDoc.Append(TheDoc_Caluse);
                theCount = TheDoc.PageCount;
                /*Start header*/
                for (i = 1; i <= theCount; i++)
                {
                    TheDoc.PageNumber = i;

                    //right column-- when goes up,the up border goes up
                    // 2nd from left--when goes up down border goes up
                    if (ProcessCode == "ENDOSSO")
                    {
                        TheDoc.Rect.String = "3 805 611 610";

                    }
                    else
                    {
                        TheDoc.Rect.String = "3 795 611 610";
                    }
                    // TheDoc.Rect.String = "3 795 611 610";
                    theID = TheDoc.AddImageHtml(HTML_Header(XML));
                    //"57 -40 300 62";
                    // "40 -40 580 20";
                    TheDoc.Rect.String = "13 -40 580 20";
                    //TheDoc.Rect.String = "40 -40 580 20";
                    TheDoc.FontSize = 11;

                    if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "CLIENTE")
                    {

                        TheDoc.AddHtml("<font pid=" + theFont1.ToString() + ">" + "Via do Segurado" + "</font>");
                    }
                    if (ENTITY_TYPE == "BROKER" || ENTITY_TYPE == "Corretor")
                    {

                        TheDoc.AddHtml("<font pid=" + theFont1.ToString() + ">" + "Via do Corretor" + "</font>");
                    }
                    if (ENTITY_TYPE == "CARRIER" || ENTITY_TYPE == "TÉCNICA")
                    {

                        TheDoc.AddHtml("<font pid=" + theFont1.ToString() + ">" + "Via Técnica" + "</font>");
                    }
                    if (ENTITY_TYPE == "REINSURER" || ENTITY_TYPE == "Ressegurador")
                    {
                        TheDoc.AddHtml("<font pid=" + theFont1.ToString() + ">" + "Via Resseguro" + "</font>");
                    }

                }
                /*End header*/

                /*Start footer */
                //"555 -40 300 165";
                //"545 -40 300 159";
                // "568 -40 300 70";
                TheDoc.Rect.String = "603 -40 300 70";
                //TheDoc.Rect.String = "568 -40 300 70";
                TheDoc.HPos = 1.0;
                TheDoc.VPos = 0.5;
                TheDoc.FontSize = 11;
                for (i = 1; i <= theCount; i++)
                {
                    TheDoc.PageNumber = i;
                    TheDoc.AddHtml("<font pid=" + theFont1.ToString() + ">" + "Página" + " " + TheDoc.PageNumber + "</font>");

                }

            }
            catch (Exception ex)
            {

                throw (new Exception("Error  in Creating Policy Pdf:  " + ex.Message.ToString(), ex.InnerException));

            }


        }
        #endregion "End Policy"




        #region "REFUSAL"
        private Boolean GenerateRefusal_Lettter(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id, string DocumentName, string DocumentPath, int PrintJobID, ref int FlagdatasetNull)
        {
            string DestinationFile_Path = "";
            Boolean retValue = false;
            try
            {
                string Refusal_Html = "";
                DataSet DS_Policy = GetPdfDataForRefusal_Letter(CUSTOMER_Id, Policy_Id, Policy_Version_Id);
                if (DS_Policy.Tables[0].Rows.Count > 0)
                {

                    Refusal_Html = DS_Policy.Tables[0].Rows[0]["DEC_CUSTOMERXML"].ToString();

                    if (CreateDirectory(DocumentPath))
                    {

                        if (FinalPDFPath == "final")
                        {

                            DestinationFile_Path = appPath + "/" + DocumentName;

                        }

                        GenerateRefusalPdf(Refusal_Html, ref TheDoc);
                        if (ImpersonateUser(IUserName, IPassWd, IDomain))
                        {
                            TheDoc.Save(DestinationFile_Path);

                            UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJobID, true, _ChkONTheyFly);
                            endImpersonation();
                            retValue = true;
                        }
                        else
                        {
                            retValue = false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    retValue = true;
                    FlagdatasetNull = 1;
                    UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJobID, false, _ChkONTheyFly);
                }
                DS_Policy.Dispose();

            }
            catch (Exception ex)
            {
                retValue = false;
                UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJobID, false, _ChkONTheyFly);
                throw (new Exception("Error Generating in  refusal letter : " + ex.InnerException.Message.ToString(), ex.InnerException));

            }
            finally
            {
                TheDoc.Clear();
                SuperTheDoc.Clear();


            }
            return retValue;
        }
        private DataSet GetPdfDataForRefusal_Letter(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id)
        {
            DataWrapper objDataWrapper = null;

            string strStoredProc = "Proc_GetXMLToGeneratePdf";
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            DataSet DS_Boleto = new DataSet();
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_Id);
                objDataWrapper.AddParameter("@POLICY_ID", Policy_Id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_Version_Id);
                DS_Boleto = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in Creating Refusal letter:  " + ex.Message.ToString(), ex.InnerException));

            }

            return DS_Boleto;

        }
        private void GenerateRefusalPdf(string HtmlFile, ref WebSupergoo.ABCpdf7.Doc TheDoc)
        {


            StringBuilder Strhtml_Clause = new StringBuilder();
            WebSupergoo.ABCpdf7.Doc TheDoc_Coverage = new WebSupergoo.ABCpdf7.Doc();
            WebSupergoo.ABCpdf7.Doc TheDoc_Caluse = new WebSupergoo.ABCpdf7.Doc();
            int theID = 0, TheID_Coverage = 0, theCount = 0, i = 0;
            try
            {

                //"50 0 560 700";
                string newhtml = HtmlFile.Replace("'Image/pic1.PNG'", ImagePath);
                //TheDoc.Rect.String = "50 0 560 720";
                // "3 -500 611 750"; 
                TheDoc.Rect.String = "3 0 611 810";
                theID = TheDoc.AddImageHtml(newhtml);

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in Creating Refusal Letter:  " + ex.Message.ToString(), ex.InnerException));

            }



        }
        #endregion  "REFUSAL"


        #region "CANCEL"
        private Boolean GenerateCancel_Lettter(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id, string DocumentName, string DocumentPath, int PrintJobID, ref int FlagdatasetNull)
        {
            string DestinationFile_Path = "";
            Boolean retValue = false;
            try
            {
                string Cancel_Html = "";
                DataSet DS_Policy = GetPdfDataForCancel_Letter(CUSTOMER_Id, Policy_Id, Policy_Version_Id);
                if (DS_Policy.Tables[0].Rows.Count > 0)
                {

                    Cancel_Html = DS_Policy.Tables[0].Rows[0]["DEC_CUSTOMERXML"].ToString();
                    if (CreateDirectory(DocumentPath))
                    {

                        if (FinalPDFPath == "final")
                        {

                            DestinationFile_Path = appPath + "/" + DocumentName;

                        }

                        GenerateCancelPdf(Cancel_Html, ref TheDoc);
                        if (ImpersonateUser(IUserName, IPassWd, IDomain))
                        {
                            TheDoc.Save(DestinationFile_Path);

                            UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJobID, true, _ChkONTheyFly);
                            endImpersonation();
                            retValue = true;
                        }
                        else
                        {
                            retValue = false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    retValue = true;
                    FlagdatasetNull = 1;
                    UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJobID, false, _ChkONTheyFly);
                }
                DS_Policy.Dispose();

            }
            catch (Exception ex)
            {
                retValue = false;
                UpdatePrintJobs(CUSTOMER_Id, Policy_Id, Policy_Version_Id, PrintJobID, false, _ChkONTheyFly);
                throw (new Exception("Error Generating in Canel letter: " + ex.InnerException.Message.ToString(), ex.InnerException));

            }
            finally
            {
                TheDoc.Clear();
                SuperTheDoc.Clear();


            }
            return retValue;
        }
        private DataSet GetPdfDataForCancel_Letter(int CUSTOMER_Id, int Policy_Id, int Policy_Version_Id)
        {
            DataWrapper objDataWrapper = null;

            string strStoredProc = "Proc_GetXMLToGeneratePdf";
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            DataSet DS_Boleto = new DataSet();
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_Id);
                objDataWrapper.AddParameter("@POLICY_ID", Policy_Id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_Version_Id);
                DS_Boleto = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in Creating Cancel letter:  " + ex.Message.ToString(), ex.InnerException));

            }

            return DS_Boleto;

        }
        private void GenerateCancelPdf(string HtmlFile, ref WebSupergoo.ABCpdf7.Doc TheDoc)
        {
            StringBuilder Strhtml_Clause = new StringBuilder();
            WebSupergoo.ABCpdf7.Doc TheDoc_Coverage = new WebSupergoo.ABCpdf7.Doc();
            WebSupergoo.ABCpdf7.Doc TheDoc_Caluse = new WebSupergoo.ABCpdf7.Doc();
            int theID = 0, TheID_Coverage = 0, theCount = 0, i = 0;
            try
            {
                string newhtml = HtmlFile.Replace("'/Cms/CmsWeb/images/AliancaLogo.gif'", ImagePath);
                //"50 0 560 720";
                // "3 -500 611 750";
                //right column-- when goes up,the up border goes up
                //2nd from left--when goes up down border goes up
                TheDoc.Rect.String = "3 0 611 810";
                theID = TheDoc.AddImageHtml(newhtml);

            }
            catch (Exception ex)
            {
                throw (new Exception("Error Generating in Canel letter: " + ex.InnerException.Message.ToString(), ex.InnerException));
            }
        }
        #endregion  "END CANCEL"



        #region "CLAIM"
        private Boolean Generate_Claim_Receipt(int Claim_ID, int Activity_Id, string DocumentName, string DocumentPath, int PrintJobID, ref int FlagdatasetNull)
        {
            string DestinationFile_Path = "";
            Boolean retValue = false;
            try
            {
                StringBuilder StrClaim = new StringBuilder();
                string Claim_Html = "";

                DataSet DS_Policy = GetPdfDataFor_Claim_Payment_Receipt(Claim_ID, Activity_Id);
                if (DS_Policy.Tables[0].Rows.Count > 0)
                {

                    Claim_Html = DS_Policy.Tables[0].Rows[0]["DOC_TEXT"].ToString();
                    switch (ENTITY_TYPE)
                    {
                        case "CLM_HO":

                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "1° VIA MATRIZ-CONTABILLIDADE" + "</font>");
                            break;
                        case "CLM_DEPT":
                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "3° VIA ORGAO EMISSOR - SINISTRO(ARQUIVO)" + "</font>");
                            break;
                        case "CLM_BENF":
                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "4° VIA BENEFICIARIO" + "</font>");
                            Claim_Html = Claim_Html.Replace("<table id=border width=35% HEIGHT=15%", "<table id=border width=35% HEIGHT=15% style=display:none");
                            break;
                        case "CLM_REINS":
                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "2° VIA RESSEGURO" + "</font>");
                            break;


                    }


                    if (CreateDirectory(DocumentPath))
                    {

                        if (FinalPDFPath == "final")
                        {

                            DestinationFile_Path = appPath + "/" + Document_Name;

                        }


                        GenerateClaimPdf(Claim_Html.ToString(), ref TheDoc);
                        if (ImpersonateUser(IUserName, IPassWd, IDomain))
                        {
                            TheDoc.Save(DestinationFile_Path);

                            UpdatePrintJobs_Claim_Remind(Claim_ID, Activity_Id, PrintJobID, true, _ChkONTheyFly);
                            endImpersonation();
                            retValue = true;
                        }
                        else
                        {
                            retValue = false;
                        }
                    }

                    else
                    {
                        return false;
                    }
                }
                else
                {
                    retValue = true;
                    FlagdatasetNull = 1;
                    UpdatePrintJobs_Claim_Remind(Claim_ID, Activity_Id, PrintJobID, false, _ChkONTheyFly);
                }
                DS_Policy.Dispose();

            }
            catch (Exception ex)
            {
                retValue = false;
                UpdatePrintJobs_Claim_Remind(Claim_ID, Activity_Id, PrintJobID, false, _ChkONTheyFly);
                throw (new Exception("Error Generating in  Claim receipt : " + ex.InnerException.Message.ToString(), ex.InnerException));

            }
            finally
            {
                TheDoc.Clear();
                SuperTheDoc.Clear();


            }
            return retValue;
        }
        private DataSet GetPdfDataFor_Claim_Payment_Receipt(int Claim_Id, int Activity_Id)
        {
            DataWrapper objDataWrapper = null;

            string strStoredProc = "Proc_GetXMLToGeneratePdf";
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            DataSet DS_Boleto = new DataSet();
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@Claim_Id", Claim_Id);
                objDataWrapper.AddParameter("@Activity_Id", Activity_Id);
                objDataWrapper.AddParameter("@PAYEE_ID", Entity_Id);
                objDataWrapper.AddParameter("@Document_Code", DocumentType);
                objDataWrapper.AddParameter("@Entity_Type", ENTITY_TYPE);
                DS_Boleto = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in Creating Claim Payment Receipt:  " + ex.Message.ToString(), ex.InnerException));

            }

            return DS_Boleto;

        }
        private void GenerateClaimPdf(string HtmlFile, ref WebSupergoo.ABCpdf7.Doc TheDoc)
        {

            string newhtml = "";

            WebSupergoo.ABCpdf7.Doc TheDoc_Caluse = new WebSupergoo.ABCpdf7.Doc();
            int theID = 0;
            try
            {          //"50 0 560 700";
                int theFont1 = TheDoc.EmbedFont("Arial Narrow", "English", false, true);
                newhtml = HtmlFile.Replace("'../../cmsweb/images/AliancaLogo.GIF'", ImagePath);
                //TheDoc.Rect.String = "3 0 611 810";
                TheDoc.Rect.String = "3 0 611 790";
                theID = TheDoc.AddImageHtml(newhtml.ToString());

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating  claim payment receipt:  " + ex.Message.ToString(), ex.InnerException));

            }



        }
        #endregion  "END CLAIM"



        #region "REMIND LETTER"
        private Boolean Generate_Remind_Lettter(int Claim_Id, int Actvity_Id, string DocumentName, string DocumentPath, int PrintJobID, ref int FlagdatasetNull)
        {

            string DestinationFile_Path = "";
            Boolean retValue = false;
            try
            {
                StringBuilder StrClaim = new StringBuilder();
                string Claim_Html = "";

                DataSet DS_Policy = GetPdfData_Remind_Letter(Claim_Id, Activity_Id);
                if (DS_Policy.Tables[0].Rows.Count > 0)
                {
                    Claim_Html = DS_Policy.Tables[0].Rows[0]["DOC_TEXT"].ToString();
                    switch (ENTITY_TYPE)
                    {
                        case "CLM_HO":

                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "1° VIA MATRIZ-CONTABILLIDADE" + "</font>");
                            break;
                        case "CLM_DEPT":
                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "3° VIA ORGAO EMISSOR - SINISTRO(ARQUIVO)" + "</font>");
                            break;
                        case "CLM_BENF":
                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "4° VIA BENEFICIARIO" + "</font>");
                            Claim_Html = Claim_Html.Replace("<table id=border width=35% HEIGHT=15%", "<table id=border width=35% HEIGHT=15% style=display:none");
                            break;
                        case "CLM_REINS":
                            Claim_Html = Claim_Html.Replace("1º VIA MATRIZ - CONTABILIDADE", "<font face=arial size=2>" + "2° VIA RESSEGURO" + "</font>");
                            break;

                    }
                    if (CreateDirectory(DocumentPath))
                    {

                        if (FinalPDFPath == "final")
                        {

                            DestinationFile_Path = appPath + "/" + Document_Name;

                        }


                        Generate_Remind_Letter_Pdf(Claim_Html.ToString(), ref TheDoc);
                        if (ImpersonateUser(IUserName, IPassWd, IDomain))
                        {
                            TheDoc.Save(DestinationFile_Path);

                            UpdatePrintJobs_Claim_Remind(Claim_Id, Activity_Id, PrintJobID, true, _ChkONTheyFly);
                            endImpersonation();
                            retValue = true;
                        }
                        else
                        {
                            retValue = false;

                        }
                    }

                    else
                    {
                        return false;
                    }

                }
                else
                {
                    retValue = true;
                    FlagdatasetNull = 1;
                    UpdatePrintJobs_Claim_Remind(Claim_Id, Activity_Id, PrintJobID, false, _ChkONTheyFly);
                }
                DS_Policy.Dispose();

            }

            catch (Exception ex)
            {
                retValue = false;
                UpdatePrintJobs_Claim_Remind(Claim_Id, Actvity_Id, PrintJobID, false, _ChkONTheyFly);
                throw (new Exception("Error Generating in  Remind letter : " + ex.InnerException.Message.ToString(), ex.InnerException));

            }
            finally
            {
                TheDoc.Clear();
                SuperTheDoc.Clear();


            }
            return retValue;
        }
        private DataSet GetPdfData_Remind_Letter(int Claim_Id, int Activity_Id)
        {
            DataWrapper objDataWrapper = null;

            string strStoredProc = "Proc_GetXMLToGeneratePdf";
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            DataSet DS_Boleto = new DataSet();
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@Claim_Id", Claim_Id);
                objDataWrapper.AddParameter("@Activity_Id", Activity_Id);
                objDataWrapper.AddParameter("@PAYEE_ID", Entity_Id);
                objDataWrapper.AddParameter("@Document_Code", DocumentType);
                objDataWrapper.AddParameter("@Entity_Type", ENTITY_TYPE);
                DS_Boleto = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in Creating remind letter:  " + ex.Message.ToString(), ex.InnerException));

            }

            return DS_Boleto;

        }
        private void Generate_Remind_Letter_Pdf(string HtmlFile, ref WebSupergoo.ABCpdf7.Doc TheDoc)
        {


            string newhtml = "";

            WebSupergoo.ABCpdf7.Doc TheDoc_Caluse = new WebSupergoo.ABCpdf7.Doc();
            int theID = 0;
            try
            {          //"50 0 560 700";
                int theFont1 = TheDoc.EmbedFont("Arial Narrow", "English", false, true);
                newhtml = HtmlFile.Replace("'../../cmsweb/images/AliancaLogo.GIF'", ImagePath);
                // TheDoc.Rect.String = "30 795 580 10";
                //TheDoc.Rect.String = "3 0 611 810";
                TheDoc.Rect.String = "3 0 611 790";
                theID = TheDoc.AddImageHtml(newhtml.ToString());


            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating remind letter:  " + ex.Message.ToString(), ex.InnerException));

            }



        }
        #endregion  "REMIND LETTER"




        #region "Policy Coverpage,Coverage,Header"
        private string CoverPage_Html(string xmlfile)
        {
            XslTransform xslt = new XslTransform();
            XmlDocument inputXmlDoc = new XmlDocument();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {

                    inputXmlDoc.LoadXml(xmlfile);


                    ArrayList ListDocumentType = ReadXmlFileTag("PROCESS", xmlfile);
                    if (ListDocumentType.Contains("APÓLICE NOVA"))
                    {
                        xslt.Load(CoverXslNewBusiness_Path);
                    }
                    if (ListDocumentType.Contains("ENDOSSO"))
                    {
                        xslt.Load(CoverXSLEndorsement_Path);
                    }
                    if (ListDocumentType.Contains("RENOVAÇÃO"))
                    {
                        xslt.Load(Xsl_Cover_Renewal);
                    }
                    if (ListDocumentType.Contains("CANCELAMENTO"))
                    {
                        xslt.Load(Xsl_Cover_Cancel_Policy);
                    }

                }
                XPathNavigator nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                StringWriter swGetHTML = new StringWriter();
                xslt.Transform(nav, null, swGetHTML);
                string strReturnCoverHtml = swGetHTML.ToString();
                return strReturnCoverHtml;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating policy cover page:  " + ex.Message.ToString(), ex.InnerException));

            }
        }

        //D009_Endorsement_Type_21_0114_0116_0118_0196_0115_0167
        private string Coverage_Endorsement_Type_21_0114_0116_0118_0196_0115_0167(string xmlfile)
        {
            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;

                    PdfXML1 = PdfXML1.Replace("</LOCATION><COVERAGE>", "</LOCATION><COVERAGES><COVERAGE>");
                    PdfXML1 = PdfXML1.Replace("</COVERAGE></RISKS>", "</COVERAGE></COVERAGES></RISKS>");
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);

                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            xslt.Load(Endo_Type_21_0114_0116_0118_0196_0115_0167);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                                // <span   style="font-family:Arial;display:none;
                            }
                        }
                        else
                        {
                            xslt.Load(Endo_Type_21_0114_0116_0118_0196_0115_0167);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {

                        xslt.Load(Endo_Type_21_0114_0116_0118_0196_0115_0167);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        //strReturnCoverageHtml = strReturnCoverageHtml + swGetHTML.ToString();
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");

                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");

                            }

                        }
                    }
                    //No Document Created for Follower

                }
                return strReturnCoverageHtml;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail Comprehensive Product:" + ex.Message.ToString(), ex.InnerException));
            }

        }
        private string Coverage_Endorsement_Type_21_0114(string xmlfile)
        {
            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;

                    PdfXML1 = PdfXML1.Replace("</LOCATION><COVERAGE>", "</LOCATION><COVERAGES><COVERAGE>");
                    PdfXML1 = PdfXML1.Replace("</COVERAGE></RISKS>", "</COVERAGE></COVERAGES></RISKS>");
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);

                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            xslt.Load(XslFilepathCover_Endorsement_Type_21_0114);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                                // <span   style="font-family:Arial;display:none;
                            }
                        }
                        else
                        {
                            xslt.Load(XslFilepathCover_Endorsement_Type_21_0114);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {

                        xslt.Load(XslFilepathCover_Endorsement_Type_21_0114);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        //strReturnCoverageHtml = strReturnCoverageHtml + swGetHTML.ToString();
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {

                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");

                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");

                            }

                        }
                    }
                    //No Document Created for Follower

                }
                return strReturnCoverageHtml;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail Comprehensive Dwelling:" + ex.Message.ToString(), ex.InnerException));
            }

        }


        //118
        private string Coverage_Comprehensive_Company(string xmlfile)
        {
            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    PdfXML1 = PdfXML1.Replace("</LOCATION><COVERAGE>", "</LOCATION><COVERAGES><COVERAGE>");
                    PdfXML1 = PdfXML1.Replace("</COVERAGE></RISKS>", "</COVERAGE></COVERAGES></RISKS>");
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct,follower
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            xslt.Load(Xsl_Coverage_118_More_Than_One_Broker_Company);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            //modified by naveen 
                            //strReturnCoverageHtml = strReturnCoverageHtml.Replace("<span id=\"relaco\" style=\"font-family:Arial;", "<span id=\"relaco\" style=\"font-family:Arial;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                                // <span   style="font-family:Arial;display:none;
                            }
                        }
                        else
                        {
                            xslt.Load(Xsl_Coverage_118_Comprehensive_Company);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Xsl_Coverage_118_More_Than_One_Broker__With_Ceded_COI_Company);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        //modified by naveen
                        //strReturnCoverageHtml = strReturnCoverageHtml.Replace("<span id=\"Morethemonebroker\" style=\"font-family:Arial;", "<span id=\"Morethemonebroker\" style=\"font-family:Arial;display:none;");

                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Comprehensive Dwelling:  " + ex.Message.ToString(), ex.InnerException));

            }
        }
        //116
        private string Coverage_Comprehensive_Condominium(string xmlfile)
        {

            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    PdfXML1 = PdfXML1.Replace("</LOCATION><COVERAGE>", "</LOCATION><COVERAGES><COVERAGE>");
                    PdfXML1 = PdfXML1.Replace("</COVERAGE></RISKS>", "</COVERAGE></COVERAGES></RISKS>");
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            xslt.Load(Xsl_Coverage_116_More_Than_One_Broker_Condominium);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            //strReturnCoverageHtml = strReturnCoverageHtml.Replace("<span id=\"relaco\" style=\"font-family:Arial;", "<span id=\"relaco\" style=\"font-family:Arial;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                //strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"border-collapse:collapse;",   "<table id=\"ceded_table\" style=\"border-collapse:collapse;display:none;");

                            }
                        }
                        else
                        {
                            xslt.Load(Xsl_Coverage_116_Comprehensive_Condominium);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Xsl_Coverage_116_More_Than_One_Broker__With_Ceded_COI_Condominium);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        //strReturnCoverageHtml = strReturnCoverageHtml.Replace("<span id=\"Morethemonebroker\" style=\"font-family:Arial;", "<span id=\"Morethemonebroker\" style=\"font-family:Arial;display:none;");
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Comprehensive Dwelling:  " + ex.Message.ToString(), ex.InnerException));

            }
        }
        //114
        private string Coverage_Comprehensive_Dwelling(string xmlfile)
        {

            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    PdfXML1 = PdfXML1.Replace("</LOCATION><COVERAGE>", "</LOCATION><COVERAGES><COVERAGE>");
                    PdfXML1 = PdfXML1.Replace("</COVERAGE></RISKS>", "</COVERAGE></COVERAGES></RISKS>");
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            xslt.Load(Xsl_Coverage_114_More_Than_One_Broker_Dwelling);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();


                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {
                            xslt.Load(Xsl_Coverage_114_Comprehensive_Dwelling);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Xsl_Coverage_114_More_Than_One_Broker__With_Ceded_COI_Dwelling);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Comprehensive Dwelling:  " + ex.Message.ToString(), ex.InnerException));

            }
        }


        //523
        private string Coverage_Civil_Libility_Transportation(string xmlfile)
        {

            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {

                            xslt.Load(Xsl_Coverage_523_Liability_Transportation_withPremium);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                strReturnCoverageHtml = swGetHTML.ToString();
                            }
                            else
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");
                            }
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                xslt.Load(Xsl_Coverage_523_Liability_Transportation_withPremium);
                                nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                                swGetHTML = new StringWriter();
                                xslt.Transform(nav, null, swGetHTML);
                                strReturnCoverageHtml = swGetHTML.ToString();
                            }
                            else
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");

                            }
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");


                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Xsl_Coverage_523_Liability_Transportation_withPremium);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Civil Liability Transportation  " + ex.Message.ToString(), ex.InnerException));

            }
        }

        //553
        private string Coverage_Facultive_Liability(string xmlfile)
        {

            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {

                            xslt.Load(Xsl_Coverage_553_Facultive_Liability);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                strReturnCoverageHtml = swGetHTML.ToString();

                            }
                            else
                            {

                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");
                            }
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {
                            xslt.Load(Xsl_Coverage_553_Facultive_Liability);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Xsl_Coverage_553_Facultive_Liability);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Civil Facultive Liability  " + ex.Message.ToString(), ex.InnerException));

            }
        }

        //171
        private string Coverage_Diversified_Risk(string xmlfile)
        {

            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    //Modified by Abhishek Goel to solve coverages issues (ref itrack 1236 Product 171) on dated 25/06/2011
                    PdfXML1 = PdfXML1.Replace("</LOCATION><COVERAGE>", "</LOCATION><COVERAGES><COVERAGE>");
                    PdfXML1 = PdfXML1.Replace("</COVERAGE></RISKS>", "</COVERAGE></COVERAGES></RISKS>");
                    //Till here
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    //Modified by Abhishek Goel (Ref iTrack 1236) dated 27/06/2011
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            xslt.Load(xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {
                            xslt.Load(xsl_Coverage_171_Diversified_Risk);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(xsl_Coverage_171_Diversified_Risk_More_Than_One_Broker_With_Ceded_COI);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }



            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Comprehensive Dwelling:  " + ex.Message.ToString(), ex.InnerException));

            }
        }


        //981
        private string Coverage_Individual_Personal_Accident(string xmlfile)
        {
            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;

                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);

                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            xslt.Load(Xsl_Coverage_981_Individual_personal_accident);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                                // <span   style="font-family:Arial;display:none;
                            }
                        }
                        else
                        {
                            xslt.Load(Xsl_Coverage_981_Individual_personal_accident);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");


                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {

                        xslt.Load(Xsl_Coverage_981_Individual_personal_accident);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        //strReturnCoverageHtml = strReturnCoverageHtml + swGetHTML.ToString();
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");

                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");

                            }

                        }
                    }
                    //No Document Created for Follower
                }

                return strReturnCoverageHtml;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail Comprehensive Product:" + ex.Message.ToString(), ex.InnerException));
            }

        }


        //Endorsement type 21 Additional ,553
        private string Coverage_Endo_type_21_23_553_Facultative_Liability(string xmlfile)
        {


            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {

                            xslt.Load(Endo_type_21_23_553_Facultative_Liability);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                strReturnCoverageHtml = swGetHTML.ToString();
                                //below template is not in use
                                // Xsl_Coverage_523_Liability_Transportation
                            }
                            else
                            {

                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");
                            }

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                xslt.Load(Endo_type_21_23_553_Facultative_Liability);
                                nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                                swGetHTML = new StringWriter();
                                xslt.Transform(nav, null, swGetHTML);
                                strReturnCoverageHtml = swGetHTML.ToString();
                            }
                            else
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");

                            }
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Endo_type_21_23_553_Facultative_Liability);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Facultive Liability  " + ex.Message.ToString(), ex.InnerException));

            }

        }


        //Endorsment Types 31, monthly billing, 0553 
        private string Coverage_Endo_type_31_D015_553_Facultive_Liability(string xmlfile)
        {


            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {

                            xslt.Load(Endorsement_type_31_553_D_015_Facultive_Liability);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                strReturnCoverageHtml = swGetHTML.ToString();
                                //below template is not in use
                                // Xsl_Coverage_523_Liability_Transportation
                            }
                            else
                            {

                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");
                            }

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                xslt.Load(Endorsement_type_31_553_D_015_Facultive_Liability);
                                nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                                swGetHTML = new StringWriter();
                                xslt.Transform(nav, null, swGetHTML);
                                strReturnCoverageHtml = swGetHTML.ToString();
                            }
                            else
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");

                            }
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Endorsement_type_31_553_D_015_Facultive_Liability);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                }
                return strReturnCoverageHtml;

            }

            catch (Exception ex)
            {
                throw (new Exception("Error  in creating Endorsement Detail of Facultive Liablity:  " + ex.Message.ToString(), ex.InnerException));

            }
        }


        //Endorsment Types 31, monthly billing, 0523 
        private string Coverage_Endo_type_31_D013_523_Civil_Liability_Transportation(string xmlfile)
        {


            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(PdfXML1);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {

                            xslt.Load(Endorsement_type_31_523_Civil_Liability_Transportation);
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                strReturnCoverageHtml = swGetHTML.ToString();
                                //below template is not in use
                                // Xsl_Coverage_523_Liability_Transportation
                            }
                            else
                            {

                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");
                            }

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {

                            if (ReadXmlFileTag("INSTALLMENT_NO", xmlfile).Count > 0)
                            {
                                xslt.Load(Endorsement_type_31_523_Civil_Liability_Transportation);
                                nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                                swGetHTML = new StringWriter();
                                xslt.Transform(nav, null, swGetHTML);
                                strReturnCoverageHtml = swGetHTML.ToString();
                            }
                            else
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table style=\"display:inline", "<table style=\"display:none");

                            }
                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        xslt.Load(Endorsement_type_31_523_Civil_Liability_Transportation);
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td id=\"feeCeded\" style=\"width:30%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }

            catch (Exception ex)
            {
                throw (new Exception("Error  in creating Endorsement Detail of Civil Liability Transportation:  " + ex.Message.ToString(), ex.InnerException));

            }
        }


        //Endoso type 0520,993,982
        private string Coverage_Endo_D012_31_0520_0982_0993(string xmlfile)
        {

            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    string PdfXML1 = xmlfile;
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(xmlfile);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);

                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (Policy_LOB == "22")
                            {
                                xslt.Load(Xsl_Coverage_D_037_Endorsement_type_31_Product_0520_v1);
                            }
                            else
                            {
                                xslt.Load(Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2);
                            }
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                            }
                        }
                        else
                        {
                            if (Policy_LOB == "22")
                            {
                                xslt.Load(Xsl_Coverage_D_037_Endorsement_type_31_Product_0520_v1);
                            }
                            else
                            {
                                xslt.Load(Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2);
                            }
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");

                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {

                        if (Policy_LOB == "22")
                        {
                            xslt.Load(Xsl_Coverage_D_037_Endorsement_type_31_Product_0520_v1);
                        }
                        else
                        {
                            xslt.Load(Xsl_Coverage_D_012_Endorsement_type_0993_0982_0520_v2);
                        }
                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        //strReturnCoverageHtml = strReturnCoverageHtml + swGetHTML.ToString();
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {

                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");

                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline", "<table id=\"multitable\" style=\"border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:none");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower

                }
                return strReturnCoverageHtml;
            }

            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Comprehensive Dwelling:  " + ex.Message.ToString(), ex.InnerException));

            }
        }



        /// <summary>
        /// This Template will be printed for all prodcut which have no risk attached to it.
        /// only cededed coi and multi broker information will be printed on this template.
        /// </summary>
        /// <param name="xmlfile"></param>
        /// <returns></returns>
        private string COI_More_Thn_One_Broker(string xmlfile)
        {

            string strReturnCoverageHtml = "";
            XPathNavigator nav;
            StringWriter swGetHTML = new StringWriter();
            XslTransform xslt = new XslTransform();
            try
            {
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    XmlDocument inputXmlDoc = new XmlDocument();
                    inputXmlDoc.LoadXml(xmlfile);
                    ArrayList ListDocumentType = ReadXmlFileTag("CO_INSURANCE", xmlfile);
                    if (ListDocumentType.Contains("14547") || ListDocumentType.Contains("14549"))//Direct
                    {
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ProcessCode == "ENDOSSO")
                            {
                                xslt.Load(Xsl_COI_More_Thn_One_Broker_Policy_Endorsement);
                            }
                            else
                            {
                                xslt.Load(Xsl_COI_More_Thn_One_Broker_Policy);
                            }
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");

                            }
                        }
                        else
                        {

                            if (ProcessCode == "ENDOSSO")
                            {
                                xslt.Load(Xsl_COI_More_Thn_One_Broker_Policy_Endorsement);
                            }
                            else
                            {
                                xslt.Load(Xsl_COI_More_Thn_One_Broker_Policy);
                            }
                            nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                            swGetHTML = new StringWriter();
                            xslt.Transform(nav, null, swGetHTML);
                            strReturnCoverageHtml = swGetHTML.ToString();
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"ceded_table\" style=\"display:inline", "<table id=\"ceded_table\" style=\"display:none");
                            strReturnCoverageHtml = "";

                        }

                    }
                    if (ListDocumentType.Contains("14548"))//Leader
                    {
                        if (ProcessCode == "ENDOSSO")
                        {
                            xslt.Load(Xsl_COI_More_Thn_One_Broker_Policy_Endorsement);
                        }
                        else
                        {
                            xslt.Load(Xsl_COI_More_Thn_One_Broker_Policy);
                        }

                        nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                        swGetHTML = new StringWriter();
                        xslt.Transform(nav, null, swGetHTML);
                        strReturnCoverageHtml = swGetHTML.ToString();
                        if (ReadXmlFileTag("BROKER_NAME", xmlfile).Count > 1)
                        {
                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Cliente")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Commisio", "<td id=\"Commisio\" style=\"display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"Value\" style=\"width:25%", "<td id=\"Value\"   style=\"width:25%;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");
                            }
                        }
                        else
                        {
                            strReturnCoverageHtml = strReturnCoverageHtml.Replace("<table id=\"multitable\" style=\"border-collapse:collapse;", "<table id=\"multitable\" style=\"border-collapse:collapse;display:none;");

                            if (ENTITY_TYPE == "CUSTOMER" || ENTITY_TYPE == "Client")
                            {
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td id=\"CedededComissao\" style=\"width:20%;vertical-align:top;", "<td id=\"CedededComissao\" style=\"width:30%;vertical-align:top;display:none");
                                strReturnCoverageHtml = strReturnCoverageHtml.Replace("<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top", "<td align=\"right\" id=\"feeCeded\" style=\"width:20%;vertical-align:top;display:none");
                            }

                        }
                    }
                    //No Document Created for Follower
                }
                return strReturnCoverageHtml;

            }



            catch (Exception ex)
            {
                throw (new Exception("Error  in Ceded COI,Multibroker detail:  " + ex.Message.ToString(), ex.InnerException));

            }

        }
        private string HTML_Header(string xmlfile)
        {
            XmlDocument inputXmlDoc = new XmlDocument();
            XslTransform xslt = new XslTransform();
            try
            {

                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {

                    inputXmlDoc.LoadXml(xmlfile);


                    ArrayList ListDocumentType = ReadXmlFileTag("PROCESS", xmlfile);
                    if (ListDocumentType.Contains("ENDOSSO"))
                    {
                        xslt.Load(HeaderXSLEdorsement_Path);
                    }
                    if (ListDocumentType.Contains("APÓLICE NOVA"))
                    {
                        xslt.Load(HeaderXslNewBusiness_Path);
                    }
                    if (ListDocumentType.Contains("RENOVAÇÃO"))
                    {
                        xslt.Load(Xsl_Header_Renewal);
                    }
                    if (ListDocumentType.Contains("CANCELAMENTO"))
                    {
                        xslt.Load(Xsl__header_Cancel_Policy);
                    }
                }
                XPathNavigator nav = ((IXPathNavigable)inputXmlDoc).CreateNavigator();
                StringWriter swGetHTML = new StringWriter();
                xslt.Transform(nav, null, swGetHTML);
                string strReturnHeaderHtml = swGetHTML.ToString();
                return strReturnHeaderHtml;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in creating risk detail for Comprehensive Dwelling:  " + ex.Message.ToString(), ex.InnerException));

            }

        }


        #endregion "End Policy Coverpage,Coverage,Header"


        #region "Comman Function"
        private void UpdatePrintJobs(int Customer_ID, int Policy_ID, int Policy_Version_ID, int PrintJobID, Boolean Status, int ChkOntheFly)
        {
            int ResultSet = 0;

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = "Proc_Update_PrintJobsFor_BoletoAndPolicyDoc";

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapperP.AddParameter("@CUSTOMER_ID", Customer_ID);
                objDataWrapperP.AddParameter("@POLICY_ID", Policy_ID);
                objDataWrapperP.AddParameter("@POLICY_VERSION_ID", Policy_Version_ID);
                objDataWrapperP.AddParameter("@Claim_Id", Claim_Id);
                objDataWrapperP.AddParameter("@Activity_Id", Activity_Id);
                objDataWrapperP.AddParameter("@PRINT_JOBS_ID", PrintJobID);
                objDataWrapperP.AddParameter("@STATUS", Status);
                objDataWrapperP.AddParameter("@Document_Code", DocumentType);
                objDataWrapperP.AddParameter("@ChkOntheFly", ChkOntheFly);
                //Machine Name is passed here to check which service has updated records.
                objDataWrapperP.AddParameter("@Machine_Name", Machine_Name);
                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                ResultSet = 1;
            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while saving record.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }


        }

        private void UpdatePrintJobs_Claim_Remind(int Claim_Id, int Activity_Id, int PrintJobID, Boolean Status, int ChkOntheFly)
        {
            int ResultSet = 0;

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = "Proc_Update_PrintJobsFor_BoletoAndPolicyDoc";

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapperP.AddParameter("@CUSTOMER_ID", CUSTOMER_Id);
                objDataWrapperP.AddParameter("@POLICY_ID", Policy_Id);
                objDataWrapperP.AddParameter("@POLICY_VERSION_ID", Policy_Version);
                objDataWrapperP.AddParameter("@Claim_Id", Claim_Id);
                objDataWrapperP.AddParameter("@Activity_Id", Activity_Id);
                objDataWrapperP.AddParameter("@PRINT_JOBS_ID", PrintJobID);
                objDataWrapperP.AddParameter("@STATUS", Status);
                objDataWrapperP.AddParameter("@Document_Code", DocumentType);
                objDataWrapperP.AddParameter("@ChkOntheFly", ChkOntheFly);
                //Machine Name is passed here to check which service has updated records.
                objDataWrapperP.AddParameter("@Machine_Name", Machine_Name);
                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                ResultSet = 1;
            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while saving record.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }


        }

        private ArrayList ReadXmlFileTag(string nodeName, string XMLString)
        {

            ArrayList Alcategories = new ArrayList();

            int count = 0;
            try
            {
                //if (XMLString == "")
                //    return  ;
                string strRetval = "";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLString);
                XmlNodeList nodList = doc.GetElementsByTagName(nodeName);
                if (nodList.Count > 0)
                {
                    for (count = 0; count < nodList.Count; count++)
                    {
                        strRetval = nodList.Item(count).InnerText;
                        Alcategories.Add(strRetval);
                    }
                }
                return Alcategories;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in Xml File:  " + ex.Message.ToString(), ex.InnerException));

            }
            finally
            {

            }
        }
        private ArrayList ReadXmlFileTag1(string nodeName, string XMLString)
        {

            ArrayList Alcategories = new ArrayList();

            int count = 0;
            try
            {
                //if (XMLString == "")
                //    return  ;
                string strRetval = "";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLString);
                XmlNodeList nodList = doc.GetElementsByTagName(nodeName);
                if (nodList.Count > 0)
                {
                    for (count = 0; count < nodList.Count; count++)
                    {
                        strRetval = nodList.Item(count).InnerText;
                        Alcategories.Add(strRetval);
                    }
                }
                return Alcategories;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in Xml File :  " + ex.Message.ToString(), ex.InnerException));

            }
            finally
            {

            }
        }
        private string GetHTML(string url, string resourceName)
        {
            try
            {

                StringBuilder content = new StringBuilder();
                using (StringWriter writer = new StringWriter(content))
                {
                    using (StreamReader reader = new StreamReader(url, System.Text.Encoding.Default))
                    {
                        writer.Write(reader.ReadToEnd());
                        reader.Close();
                    }
                    writer.Close();
                }
                return content.ToString();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error  in converting xml file to html:  " + ex.Message.ToString(), ex.InnerException));

            }
        }
        private Boolean CreateDirectory(string DocumentPath)
        {
            try
            {
                //string strImpersonationUserId = "", strImpersonationPassword = "", strImpersonationDomain = "";
                if (ImpersonateUser(IUserName, IPassWd, IDomain))
                {
                    appPath = "";
                    appPath = DocumentPath.Replace("/cms/Upload", PdfPath);
                    System.IO.Directory.CreateDirectory(appPath);
                    endImpersonation();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                throw (new Exception("Error  in creating directory :  " + "File Path" + PdfPath + DocumentPath + ex.Message.ToString(), ex.InnerException));

            }
        }

        private void ConvertRtfToHtml(string SoruceFile)
        {




            Microsoft.Office.Interop.Word.Document doc = null;
            try
            {
                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
                {
                    process.Kill();
                    process.WaitForExit();
                }
                string StrSourceFile = SoruceFile;
                string StrDestinationFile = RTF_To_Html;
                object fileNameObj = (Object)StrSourceFile;
                object fileNameObj1 = (Object)StrDestinationFile;
                object novalue = System.Reflection.Missing.Value;
                object isVisible = true;
                object readOnly = false;
                object noPrompt = false;
                object originalFormat = Type.Missing;
                object saveChanges = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
                object routeDocument = Type.Missing;
                object saveChanges1 = Microsoft.Office.Interop.Word.WdSaveOptions.wdSaveChanges;
                //Microsoft.Office.Interop.Word.ApplicationClass objWord = new Microsoft.Office.Interop.Word.ApplicationClass();
                Microsoft.Office.Interop.Word.Application objWord = new Microsoft.Office.Interop.Word.Application();
                doc = objWord.Documents.Open(ref fileNameObj, ref novalue, ref readOnly, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref isVisible, ref novalue, ref novalue, ref novalue, ref novalue);
                doc.SaveAs(ref fileNameObj1, ref saveChanges, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref novalue, ref isVisible, ref novalue, ref novalue, ref novalue, ref novalue);
                objWord.Documents.Close(ref saveChanges1, ref originalFormat, ref routeDocument);
                objWord.Quit(ref novalue, ref novalue, ref novalue);
                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
                {
                    process.Kill();
                    process.WaitForExit();
                }

            }
            catch (Exception ex)
            {

                throw (new Exception("Error  in Creating  Converting rtf to html:  " + ex.Message.ToString(), ex.InnerException));

            }
            finally
            {


            }
        }


        #endregion "End Comman Function"


        #region  impersonate

        public void endImpersonation()
        {
            try
            {
                if (impersonationContext != null) impersonationContext.Undo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Ebix Advantage Web", "Impersionation Error; Message:-" + ex.Message);
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
    }

    #endregion







}
