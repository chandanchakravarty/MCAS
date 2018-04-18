using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Xml;
using System.Data;
using System.IO;
using System.Xml.Linq;

using EAWXmlToPDFParser;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;

namespace MCAS.Controllers
{
    public class ClaimDocumentController : BaseController
    {



        public ActionResult Index()
        {
            return View();
        }
        /*[HttpGet]
        public FileResult GenerateDocument(PreViewDocumentModel model)
        {
            string accidentclaimid = "";
            try
            {
                accidentclaimid = Request.QueryString["AccidentClaimId"].ToString();
                int accd = Convert.ToInt16(accidentclaimid);


                string Domain = ConfigurationManager.AppSettings["IDomain"];
                string username = ConfigurationManager.AppSettings["IUserName"];
                string passwd = ConfigurationManager.AppSettings["IPassWd"];
                string outFilePath = ConfigurationManager.AppSettings["OutPutFilePath"];
                string UploadFolder = ConfigurationManager.AppSettings["UploadFolder"];
                
                DocumentModel dModel = new DocumentModel() { iPassword = passwd, iDomain = Domain, iUserName = username, outFilePath = outFilePath, UploadFolderPath = UploadFolder, CreatedBy = LoggedInUserId };
                string fileName = dModel.GenerateDocument(accd, model.DocumentId.Value, DocumentGenerate.outFormat.PDF);
                TempData["result"] = "File : " + fileName + " has been generated.";
                return new FilePathResult(fileName, "application/pdf");
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error:" + ex.Message);
                ViewData["SuccessMsg"] = ex.Message;
                throw (ex);
                //return new FileStringResult(sdf, "application/text");

            }
            
        }*/
        [HttpGet]
        public FileResult GenerateDocument(string ClaimID, string AccidentClaimId, string ScreenId, string TemplateId, string PartyTypeId, string Mode, string SId, string uid)
        {
            MCASEntities obj = new MCASEntities();
            SId = SId == "undefined" ? "0" : SId;
            try
            {
                string RecoveryId = ScreenId == "294" ? SId : "";
                string PaymentId = ScreenId == "139" ? SId : "";
                string LogId = ScreenId == "303" ? SId : "";
                string ReserveId = ScreenId == "137" ? SId : ""; 
                int tmpid = Convert.ToInt32(TemplateId);
                int logId = Mode != "View" ? Convert.ToInt32(LogId == "" ? "0" : LogId) : 0;
                int accid = Mode != "View" ? Convert.ToInt32(AccidentClaimId) : 0;
                int clmid = Mode != "View" ? Convert.ToInt32(ClaimID) : 0;
                int prtid = Mode != "View" ? Convert.ToInt32(PartyTypeId) : 0;
                int paymentid = Mode != "View" ? Convert.ToInt32(PaymentId == "" ? "0" : PaymentId) : 0;
                int reserveId = Mode != "View" ? Convert.ToInt32(ReserveId == "" ? "0" : ReserveId) : 0;
                string Domain = ConfigurationManager.AppSettings["IDomain"];
                string username = ConfigurationManager.AppSettings["IUserName"];
                string passwd = ConfigurationManager.AppSettings["IPassWd"];
                string outFilePath = ConfigurationManager.AppSettings["OutPutFilePath"];
                string UploadFolder = ConfigurationManager.AppSettings["UploadFolder"];

                DocumentModel dModel = new DocumentModel() { iPassword = passwd, iDomain = Domain, iUserName = username, outFilePath = outFilePath, UploadFolderPath = UploadFolder, CreatedBy = LoggedInUserId };

                int templateId = Convert.ToInt32(TemplateId);
                string format = obj.MNT_TEMPLATE_MASTER.Where(x => x.Template_Id == templateId).Select(x => x.OutPutFormat).SingleOrDefault() ?? "PDF";
                string fileName = dModel.GenerateDocument(accid, clmid, tmpid, prtid, logId, paymentid, reserveId, (DocumentGenerate.outFormat)Enum.Parse(typeof(DocumentGenerate.outFormat), format));
                TempData["result"] = "File : " + fileName + " has been generated.";
                String ext = System.IO.Path.GetExtension(fileName);
                //if (ext.ToLower() == ".pdf")
                //{
                //return new FilePathResult(fileName, "application/pdf");
                //}
                //else if (ext.ToLower() == ".doc")
                //{
                //    return new FilePathResult(fileName, "application/msword");
                //}
                //else
                //{
                //    return new FilePathResult(fileName, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                //}
                Byte[] returnbytes = dModel.filebytes;
                return File(returnbytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ModelState.AddModelError("", "Error:" + ex.Message ?? ex.InnerException.Message ?? "");
                ViewData["SuccessMsg"] = ex.Message ?? ex.InnerException.Message ?? "";
                throw (ex);
                //return new FileStringResult(sdf, "application/text");

            }
            finally
            {
                obj.Dispose();
            }

        }


    }
}
