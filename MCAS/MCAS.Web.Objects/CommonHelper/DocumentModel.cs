/******************************************************************************************
<Author				: -  Pravesh K Chandel
<Start Date			: -	 10 July 2015
<End Date			: -	
<Description		: - document model
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:-  
<Modified By		:-  
<Purpose			:-  
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Xml.Linq;
using System.Configuration;
using System.Web;
using EAWXmlToPDFParser;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
    public class DocumentModel:BaseModel
    {

        protected enum TemplateFormat
        {
            PDF = 1,
            RDL = 2,
            RDLC = 3
        }

        #region properties
        private string mOutputpath = "";
        private string mTemplatecode;
        private string mOutputfilename;
        private string mOutputpathRelative = "";
        private string mReportProcName = "";
        private bool mAutoExecuteDB = true;
        //private int mDocumentId { get; set; }
        public string DocumentName { get; set; }
                
        public string outFilePath { get; set; }
        public string UploadFolderPath { get; set; }
        public Byte[] filebytes { get; set; }
      
        public string iDomain { get; set; }
        public string iUserName { get; set; }
        public string iPassword { get; set; }
        

        #endregion
        #region Public Methods
        public string GenerateDocument(int accidentclaimid, int claimId, int DocumentId, int PartyTypeId, int logId, int paymentid, int reserveId, DocumentGenerate.outFormat OutFormat)
        {
            MCASEntities _db = new MCASEntities();
            try
            {
                //string DocFileName = "";
                //string DocFilePath = "";
                string retfile = "";
                //var Doc = _db.PRINT_JOBS.Where(x => x.ENTITY_ID == DocumentId && x.CLAIM_ID == accidentclaimid).FirstOrDefault();
                //if (Doc != null)
                //{
                //    DocFileName = Doc.FILE_NAME;
                //    DocFilePath = Doc.URL_PATH;
                //    string fileServerpath = (from l in _db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                //    if (String.IsNullOrEmpty(fileServerpath))
                //    {
                //        throw new Exception("Error: File Can not be shown as File server path not configured. Please contact administrator.");
                //    }
                //    if (String.IsNullOrEmpty(UploadFolderPath))
                //    {
                //        throw new Exception("Error: File Can not be shown as Upload Folder path not configured. Please contact administrator.");
                //    }
                //    string FileUploadPath = fileServerpath.TrimEnd('\\') + "\\" + UploadFolderPath.TrimStart('\\');
                //    DocFilePath = FileUploadPath + "\\" + DocFilePath;
                //    retfile = DocFilePath + "/" + DocFileName;

                //}
                //else
                //{
                retfile = generate(accidentclaimid, claimId, DocumentId, PartyTypeId, logId, paymentid, reserveId, OutFormat, _db);
                //addPrintJobsEntry(accidentclaimid, DocumentId, _db);
                //}
                return retfile;
            }
            catch (Exception ex)
            { throw (ex); }
            finally { _db.Dispose(); }
        }
       #endregion

       #region Private Methods

       #region generate requested file
        private string generate(int accidentclaimid, int claimid, int DocumentId, int partyTypeId, int logId, int paymentid, int reserveId, DocumentGenerate.outFormat OutFormat, MCASEntities objEntity)
        {
            //MCASEntities _db = new MCASEntities();
            string strRetFileName = "";
            int accd = accidentclaimid;
            //DataSet ds;
            String strReturnXML = "";
            string DocFileName = "";
            string DocFilePath = "";

            var results = (from m in objEntity.MNT_TEMPLATE_MASTER where m.Id == DocumentId select new { Tempaltepath = m.Template_Path, Filename = m.Filename, MapxmlPath = m.MappingXML_Path, MapXmlFileName = m.MappingXML_FileName, Tempaltecode = m.Template_Code, templateFormat = m.Template_Format_Id }).FirstOrDefault();
            if (results == null) throw (new Exception("Template does not exists for this document"));

            string MapXmlName = results.MapXmlFileName;
            string XmlPath = results.MapxmlPath;
            mTemplatecode = results.Tempaltecode;
            DocFileName = results.Filename;
            DocFilePath = results.Tempaltepath;
           
            string OutExt = OutFormat.ToString().ToLower() == "excel" ? "xls" : OutFormat.ToString().ToLower() == "word" ? "Doc" : OutFormat.ToString();

            string fileServerpath = (from l in objEntity.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
            if (String.IsNullOrEmpty(fileServerpath))
            {
                throw new Exception("Error: File Can not be shown as File server path not configured. Please contact administrator.");
            }
            if (String.IsNullOrEmpty(UploadFolderPath))
            {
                throw new Exception("Error: File Can not be shown as Upload Folder path not configured. Please contact administrator.");
            }
            if (String.IsNullOrEmpty(outFilePath))
            {
                throw new Exception("Error: File Can not be shown as OutFilePath not provided.");
            }
            string FileUploadPath = fileServerpath.TrimEnd('\\') + "\\" + UploadFolderPath.TrimStart('\\');

            string templatepath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + DocFilePath);
            mOutputpathRelative = outFilePath + "/" + accidentclaimid.ToString() + "/" + mTemplatecode + "/";
            mOutputpath = FileUploadPath + mOutputpathRelative;
            //mOutputfilename = (Path.GetFileNameWithoutExtension(DocFileName) + "_" + accidentclaimid.ToString() + "_" + mTemplatecode + "_" + DateTime.Now.Ticks) + "." + OutExt;
            mOutputfilename = (Path.GetFileNameWithoutExtension(DocFileName) + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss")) + "." + OutExt;
            //if (results.templateFormat == 1) // 1== PDF
            //{
            //    strReturnXML = fetchDocumentData(accidentclaimid, mTemplatecode, objEntity).ToString();
            //    XmlDocument doc = new XmlDocument();

            //    XmlPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + XmlPath);
            //    XmlPath = XmlPath + "/" + MapXmlName;
            //    doc.Load(XmlPath);

            //    strRetFileName = GenerateDocumentFromXml(iDomain, iUserName, iPassword, doc.InnerXml, strReturnXML, templatepath, mOutputpath, mOutputfilename);
            //}
            //else 
            //if (results.templateFormat == 1) // RDL or RDLC
            //{
            List<Parameter> pList = getTemplateParameters(accidentclaimid, claimid, partyTypeId, logId, paymentid, reserveId, mTemplatecode, objEntity);
                string ConStr = CommonHelper.CommonUtilities.CommonType.GetConnString(objEntity);
                strRetFileName = GenerateDocumentFromRDL(templatepath, mOutputpath, mOutputfilename, OutFormat, DocFileName, pList, ConStr);
                //}
                //_db.Dispose();
            return strRetFileName;
        }
       #endregion

       #region Genrerate document file using RDL/RDLC template
       private string GenerateDocumentFromRDL(string templatepath, string DocOutputPath, string OutFileName, DocumentGenerate.outFormat OutFormat, string RDLtemplateName,List<Parameter> inputParams, string ConStr)
        {
            
            EAWXmlToPDFParser.DocumentGenerate gdoc = new DocumentGenerate();
            gdoc.outputPath = DocOutputPath;
            gdoc.outputFileName = OutFileName;
            gdoc.IsFileBytesOnly = true;

            //gdoc.outputFormat = outFormat.Excel.ToString();
            gdoc.reportTemplatePath = templatepath;
            gdoc.inputReportName = RDLtemplateName;
           
           gdoc.IDomain = this.iDomain;
           gdoc.IUserName = this.iUserName;
           gdoc.IPassWd = this.iPassword;
           gdoc.AutoExecuteDB = mAutoExecuteDB;
           gdoc.DBconnStr = ConStr;
           gdoc.ReportProcName = mReportProcName;
           foreach (var param in inputParams)
           {
               gdoc.Parameters.Add(new Parameter() { Name = param.Name.TrimStart('@'), Value = param.Value, type = param.type});
           }
            try
            {
                Byte[] fBytes;
                string retFileName = gdoc.Process(OutFormat, out fBytes);
                //retFileName = DocOutputPath + retFileName;
                filebytes = fBytes;
                return retFileName;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       #endregion

       #region Genrerate PDF file using PDF template and Mapping XML from Input XML
       private string GenerateDocumentFromXml(string Domain, string userName, string passwd, string strMapXml, string strInputXML, string pdftemplatepath, string pdfOutputPath, string OutFileName)
       {
           EAWXmlToPDFParser.XmlToPDFParser ObjXmlParser = new EAWXmlToPDFParser.XmlToPDFParser();
           ObjXmlParser.IDomain = Domain;
           ObjXmlParser.IUserName = userName;
           ObjXmlParser.IPassWd = passwd;
           ObjXmlParser.PdfMapXml = strMapXml;
           ObjXmlParser.InputXml = strInputXML;
           ObjXmlParser.PdfTemplatePath = pdftemplatepath;
           ObjXmlParser.PdfOutPutPath = pdfOutputPath;
           ObjXmlParser.PdfOutPutFileName = OutFileName;
           string fileName = "";
           try
           {
               fileName = ObjXmlParser.GeneratePdf();
               fileName = pdfOutputPath + fileName;
               ObjXmlParser = null;
               return fileName;
           }
           catch (Exception ex)
           {
               ObjXmlParser = null;
               throw (ex);
           }
       }
       #endregion

       #region createEntry in Print Job Table
       private void addPrintJobsEntry(int AccidentClaimId, int DocumentId, MCASEntities objEntity)
       {
           //MCASEntities objEntity = new MCASEntities();
           PRINT_JOBS print = new PRINT_JOBS();
           {
               var countrows = (from row in objEntity.PRINT_JOBS select (int?)row.PRINT_JOBS_ID).Max() ?? 0;
               string currentno = (countrows + 1).ToString();
               print.CLAIM_ID = AccidentClaimId;
               print.ENTITY_ID = DocumentId;
               print.DOCUMENT_CODE = mTemplatecode;
               print.IS_ACTIVE = "1";
               print.IS_PROCESSED = Convert.ToBoolean(1);
               print.FILE_NAME = mOutputfilename;
               print.URL_PATH = mOutputpathRelative;
               print.CREATED_DATETIME = DateTime.Now;
               print.CreatedBy = this.CreatedBy;
               objEntity.PRINT_JOBS.AddObject(print);
               objEntity.SaveChanges();
           }
           //objEntity.Dispose();
       }
        #endregion

       #region Fetch data from DB and assign template Parameters
       private List<Parameter> getTemplateParameters(int accidentclaimid, int claimid, int partytypeid,int logId,int paymentid,int ReserveId, string templateCode, MCASEntities objEntity)
       {
           List<Parameter> pList = new List<Parameter>();
           if (templateCode == "ACKPD" || templateCode == "ACKBI" || templateCode == "ACK" || templateCode == "CLDV" || templateCode == "LTBIMP" || templateCode == "LTPD" || templateCode == "LTRC" || templateCode == "OPR" || templateCode == "OITVN" || templateCode == "OIO" || templateCode == "COSPD" || templateCode == "COSTP" || templateCode == "COSBI" || templateCode == "LLABI" || templateCode == "LLAPD" || templateCode == "LLARC" || templateCode == "CCTVTPL" || templateCode == "CCTVRL" || templateCode == "CCTVRI" || templateCode == "CBVCB" || templateCode == "CCRMPD" || templateCode == "CCRMBI" || templateCode == "ARPIP" || templateCode == "CPFBBI" || templateCode == "IVCCTV" || templateCode == "MCCDGI" || templateCode == "SNTT" || templateCode == "BMIII" || templateCode == "LTBI" || templateCode == "OLBIR")
           {
               pList.Add(new Parameter() { Name = "AccidentClaimId", Value = accidentclaimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ClaimId", Value = claimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "PartyTypeId", Value = partytypeid.ToString(), type = ParameterType.Both });

               mReportProcName ="Proc_NewClaim_Acknowledgement_letters";
           }
           else if (templateCode == "LOG" || templateCode == "LOGTP")
           {
               pList.Add(new Parameter() { Name = "LogId", Value = logId.ToString(), type = ParameterType.Both });
               mReportProcName = "Proc_LetterofGuarantee";
           }
           else if (templateCode == "DVNO" || templateCode == "SBSTO" || templateCode == "SBSTTP" || templateCode == "DVIO" || templateCode == "DVITPV" || templateCode == "DVIPO" || templateCode == "DVIPTPV")
           {
               pList.Add(new Parameter() { Name = "AccidentClaimId", Value = accidentclaimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ClaimId", Value = claimid.ToString(), type = ParameterType.Both });
               mReportProcName = "Proc_GenerateTemplateNoLogic";
           }
           else if (templateCode == "PMBI")
           {
               pList.Add(new Parameter() { Name = "AccidentClaimId", Value = accidentclaimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ClaimId", Value = claimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "PaymentId", Value = paymentid.ToString(), type = ParameterType.Both });
               mReportProcName = "Proc_GetPaymentMinutesBI";
           }
           else if (templateCode == "PMPD" || templateCode == "PMRC")
           {
               pList.Add(new Parameter() { Name = "AccidentClaimId", Value = accidentclaimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ClaimId", Value = claimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "PaymentId", Value = paymentid.ToString(), type = ParameterType.Both });
               mReportProcName = "Proc_GetPaymentMinutes";
           }
           else if (templateCode == "PFBBI" || templateCode == "PFBPD" || templateCode == "PFRBI")
           {
               pList.Add(new Parameter() { Name = "AccidentClaimId", Value = accidentclaimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ClaimId", Value = claimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "PartyTypeId", Value = partytypeid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "PaymentId", Value = paymentid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "loggedInId", Value = HttpContext.Current.Session["UserId"].ToString(), type = ParameterType.Both });
               mReportProcName = "Proc_GetPaymentFormBus";
           }
           else if (templateCode == "MRF" || templateCode == "LFMR")
           {
               pList.Add(new Parameter() { Name = "AccidentClaimId", Value = accidentclaimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ClaimId", Value = claimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "HospitalNameId", Value = partytypeid.ToString(), type = ParameterType.Both });
               mReportProcName = "Proc_GetMedicalReport";
           }
           else if (templateCode == "LODTPV" || templateCode == "LODG" || templateCode == "LODM")
           {
               pList.Add(new Parameter() { Name = "AccidentClaimId", Value = accidentclaimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ClaimId", Value = claimid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "PartyTypeId", Value = partytypeid.ToString(), type = ParameterType.Both });
               pList.Add(new Parameter() { Name = "ReserveId", Value = ReserveId.ToString(), type = ParameterType.Both });
               mReportProcName = "Proc_GetReserveReportDetails";
           }
           return pList;
       }

       private object fetchDocumentData(int accidentclaimid, string templateCode, MCASEntities objEntity)
       {
           
           string strReturnXML = "";
           try
           {
               switch (templateCode)
               {
                   case "ALBI":
                       {
                           strReturnXML = "";
                           break;
                       }
                   case "DVIT":
                       {
                           objEntity.ClearParameteres();
                           objEntity.AddParameter("@ACCIDENTCLAIM_ID", accidentclaimid.ToString());
                           DataSet ds = objEntity.ExecuteDataSet("Proc_GetClaimDataXML", CommandType.StoredProcedure);
                           if (ds.Tables[0].Rows.Count > 0)
                           {
                               foreach (DataRow dr in ds.Tables[0].Rows)
                               {
                                   strReturnXML = dr[0].ToString();

                               }
                           }
                           break;
                       }
                   default:
                       {
                           objEntity.ClearParameteres();
                           objEntity.AddParameter("@ACCIDENTCLAIM_ID", accidentclaimid.ToString());
                           DataSet ds = objEntity.ExecuteDataSet("Proc_GetClaimDataXML", CommandType.StoredProcedure);
                           if (ds.Tables[0].Rows.Count > 0)
                           {
                               foreach (DataRow dr in ds.Tables[0].Rows)
                               {
                                   strReturnXML = dr[0].ToString();

                               }
                           }
                           break;
                       }
               }
               return strReturnXML;
           }
           catch (Exception ex)
           { throw (ex); }
       }
       #endregion

       #endregion
    }
}
