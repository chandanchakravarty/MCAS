using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using MCAS.Entity;
using System.Data;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using System.Globalization;
using MCAS.Globalisation;
namespace MCAS.Controllers
{
    public class ClaimRecoveryProcessingController : BaseController
    {
        MCASEntities obj = new MCASEntities();
        //
        // GET: /ClaimRecoveryProcessing/

        public ActionResult Index()
        {
            return View();
        }

        #region "Claim Recovery Processing"
        [HttpPost]
        public JsonResult GetRecoveryClaims(SearchCriteria criteria)
        {
            string caller = criteria.claimMode;
            List<InsuranceModel> list = new List<InsuranceModel>();

            if (criteria.ClaimNo != null || criteria.IPNo != null || criteria.vehicleNo != null || criteria.LossDate != null || criteria.ClaimantName != null || criteria.VehicleRegnNo != null)
            {
                list = GetSearchResult(criteria.ClaimNo, criteria.IPNo, Convert.ToDateTime(criteria.LossDate), criteria.vehicleNo, criteria.ClaimantName, criteria.VehicleRegnNo, criteria.claimStatus);
            }
            else
            {
                list = InsuranceModel.FetchRecoveryClaims();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClaimRecoveryProcessing()
        {
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            return View(new SearchCriteria() { claimMode = caller });
        }
        public ActionResult ClaimLogRequest()
        {
            var model = new LogRequestModel();
            model.HopitalNameIdList = model.FetchLogRequest();
            model.HdivHidden = "0";
            //Session["totalAmount"] = model.HdivHidden;
            return View(model);
        }
        public ActionResult ClaimLogList()
        {
            List<LogRequestModel> list = new List<LogRequestModel>();
            if (TempData["search"] != null)
            {
                var model = (LogRequestModel)TempData["search"];
                list = LogRequestModel.GetLogSearchResults(model);
                ModelState.Clear();
            }
            else
            {
                list = LogRequestModel.FetchLogList();
            }

            return View(list);
        }
        [HttpPost]
        public ActionResult ClaimLogRequest(LogRequestModel model)
        {
            TempData["search"] = model;
            model.HopitalNameIdList = model.FetchLogRequest();
            model.HdivHidden = "1";
            return View(model);
        }
        public ActionResult ClaimLogRequestNew()
        {
            var model = new LogRequestModel();

            TempData["LogId"] = MCASQueryString["LogId"];
            TempData["AccidentClaimId"] = MCASQueryString["AccidentClaimId"];
            TempData.Keep("AccidentClaimId");
            string Acc = MCASQueryString["AccidentClaimId"].ToString();
            var mandateID = MCASQueryString["MandateId"].ToString();
            TempData.Keep("LogId");
            TempData["ClaimID"] = MCASQueryString["ClaimID"];
            TempData.Keep("ClaimID");
            int claimID = Int32.Parse(MCASQueryString["ClaimID"].ToString());
            var results = obj.Proc_getLogAmount((Int32.Parse(Acc)), claimID, (Int32.Parse(mandateID))).SingleOrDefault();
            var results_ = results == null ? 0 : results.LogMedicalExpenses_S;
            decimal lOGAmount = Convert.ToDecimal(results_);
            if ((Int32.Parse(TempData["LogId"].ToString()) != 0))
            {
                var Log_ID = Int32.Parse(TempData["LogId"].ToString());
                var logResults = (from x in obj.CLM_LogRequest where x.LogId == Log_ID select x).FirstOrDefault();
                var logDetails = obj.Proc_GetLogsDetails(Log_ID).FirstOrDefault();
                model.ClaimantNameId = logResults.ClaimantNameId;
                model.LogRefNo = logResults.LogRefNo.ToUpper();
                model.Hospital_Id = logResults.Hospital_Id;
                model.LOGDate = logResults.LOGDate;
                model.CORemarks = logResults.CORemarks;
                model.AssignTo = logResults.AssignTo;
                model.IsVoid = logResults.IsVoid;
                model.ClaimNo = logDetails.ClaimNo;
                model.HospitalName = logDetails.HospitalName;
                model.SubClaimNo = logDetails.SubClaimNo;
                model.SubClaimNo = logDetails.SubClaimNo;
                model.CreatedBy = logResults.Createdby;
                model.CreatedOn = Convert.ToDateTime(logResults.CreatedDate);
                if (logResults.ModifiedDate != null)
                {
                    model.ModifiedOn = logResults.ModifiedDate;
                    model.ModifiedBy = logResults.ModifiedBy;
                }
            }
            else
            {
                var accId = Int32.Parse(Acc);
                var ClaimNo = (from x in obj.ClaimAccidentDetails where x.AccidentClaimId == accId select x.ClaimNo).FirstOrDefault();
                var SubClaimNo = (from x in obj.CLM_Claims where x.AccidentClaimId == accId && x.ClaimID == claimID select x.ClaimRecordNo).FirstOrDefault();
                model.ClaimNo = ClaimNo;
                model.SubClaimNo = SubClaimNo;
            }
            model.LOGAmount = lOGAmount;
            model.HopitalNameIdList = model.FetchLogRequest();
            model.SelectListClamiantName = model.GetClaimantNameReserveList();
            model.ClaimantNameId = Int32.Parse(MCASQueryString["ClaimID"].ToString());
            model.AssignToIdList = model.FetchAssignToIdList();
            return View(model);
        }
        [HttpPost]
        public ActionResult ClaimLogRequestNew(string LogId, string ClaimID, string AccidentClaimId, string MandateId, LogRequestModel model)
        {
            var mode = MCASQueryString["Mode"].ToString();
            var mandateID = MCASQueryString["MandateId"].ToString();
            try
            {
                string Acc;
                int claimID;
                if (TempData["AccidentClaimId"] != null)
                {
                    Acc = TempData["AccidentClaimId"].ToString();
                }
                else
                {
                    Acc = AccidentClaimId;
                }

                if (TempData["ClaimID"] != null)
                {
                    model.ClaimID = Int32.Parse(TempData["ClaimID"].ToString());
                    claimID = Int32.Parse(TempData["ClaimID"].ToString());
                }
                else
                {
                    model.ClaimID = Int32.Parse(ClaimID);
                    claimID = Int32.Parse(ClaimID);
                }
                if (model.VoidStatus != 1)
                {

                    var results = obj.Proc_getLogAmount((Int32.Parse(Acc)), claimID, (Int32.Parse(mandateID))).SingleOrDefault();
                    decimal lOGAmount;
                    if (results == null)
                    {
                        lOGAmount = Convert.ToDecimal(0.00);
                    }
                    else
                    {
                        lOGAmount = Convert.ToDecimal(results.LogMedicalExpenses_S);
                    }
                    model.LOGAmount = lOGAmount;
                    if (TempData["LogId"] != null || LogId != "")
                    {
                        if (TempData["LogId"] != null)
                        {
                            model.LogId = Int32.Parse(TempData["LogId"].ToString());
                        }
                        else
                        {
                            model.LogId = Int32.Parse(LogId);
                        }
                        if (model.LogId != 0)
                        {
                            var logDetails = obj.Proc_GetLogsDetails(model.LogId).FirstOrDefault();
                            model.ClaimNo = logDetails.ClaimNo;
                            model.HospitalName = logDetails.HospitalName;
                            model.SubClaimNo = logDetails.SubClaimNo;
                            model.IsVoid = logDetails.IsVoid;

                        }
                    }
                    if (TempData["AccidentClaimId"] != null)
                    {
                        model.AccidentClaimId = Int32.Parse(TempData["AccidentClaimId"].ToString());
                    }
                    else
                    {
                        model.AccidentClaimId = Int32.Parse(AccidentClaimId);
                    }
                    ModelState.Clear();
                    model.MandateId = Convert.ToInt32(mandateID);
                    model.Save();
                    TempData["mode"] = "S";
                    if (mode == "New")
                    {
                        TempData["SuccessMsg"] = "LOG Request Saved Successfully .";
                        mode = "New";
                    }
                    else
                    {
                        TempData["SuccessMsg"] = "LOG Request Updated Successfully .";
                        mode = "Edit";
                    }
                }
                else
                {
                    if (TempData["LogId"] != null)
                    {
                        model.SetVoidType(Int32.Parse(TempData["LogId"].ToString()));
                        model.LogId = Int32.Parse(TempData["LogId"].ToString());
                    }
                    else
                    {
                        model.SetVoidType(Int32.Parse(LogId));
                        model.LogId = Int32.Parse(LogId);
                    }
                    var results = obj.Proc_getLogAmount((Int32.Parse(Acc)), claimID, (Int32.Parse(mandateID))).SingleOrDefault();
                    decimal lOGAmount = Convert.ToDecimal(results.LogMedicalExpenses_S);
                    model.LOGAmount = lOGAmount;
                    model.IsVoid = true;
                    TempData["mode"] = "V";
                    mode = "Void";
                    TempData["SuccessMsg"] = "LOG Request Void Successfully .";
                }
                model.HopitalNameIdList = model.FetchLogRequest();
                model.SelectListClamiantName = model.GetClaimantNameReserveList();
                model.ClaimantNameId = claimID;
                model.AssignToIdList = model.FetchAssignToIdList();
            }
            catch (Exception ex)
            {
                TempData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"].ToUpper();
            object routes = new { LogId = model.LogId, AccidentClaimId = @AccidentClaimId, claimID = model.ClaimID, @MandateId = MandateId, @Mode = mode };
            if (isEncryptedParams.ToUpper() == "YES")
            {
                string res = RouteEncryptDecrypt.getRouteString(routes);
                res = RouteEncryptDecrypt.Encrypt(res);
                routes = new { Q = res };
            }
            return RedirectToAction("ClaimLogRequestNew", routes);
        }
        public ActionResult LogRequestDocument()
        {
            List<LogRequestDocumentModel> list = new List<LogRequestDocumentModel>();
            list = LogRequestDocumentModel.Fetch();
            return View(list);
        }
        [HttpGet]
        public FileResult PreviewDocument(PreViewDocumentModel model)
        {

            string pdfname = "";
            string path = "";
            string templatecode = "";

            var results = (from m in obj.MNT_TEMPLATE_MASTER where m.Template_Id == model.DocumentId select new { Tempaltepath = m.Template_Path, Filename = m.Filename, Tempaltecode = m.Template_Code }).FirstOrDefault();

            pdfname = results.Filename;
            path = results.Tempaltepath;
            templatecode = results.Tempaltecode;
            path = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + path);
            string str = path + "/" + pdfname; //Server.MapPath(path + "/" + pdfname);
            return File(str, "application/pdf", pdfname);
        }
        [HttpGet]
        public FileResult LogGenerateDocument(LogRequestDocumentModel model)
        {
            List<LogRequestDocumentModel> list = new List<LogRequestDocumentModel>();
            //-- pdf generation code
            string MapXmlName = "";
            string XmlPath = "";
            string templatecode = "";
            string str = "";
            string pdfname = "";
            string path = "";
            string pdftemplatepath = "";
            string pdfoutputpath = "";
            string pdfoutputfilename = "";
            string claimid = "";
            DataSet ds;
            String strReturnXML = "";
            string UrlPath = "";
            string myFilePath = "";
            string sdf = "";
            string sty = "";
            string accidentclaimid = "";
            string Domain = "";
            string username = "";
            string passwd = "";
            string reportString = "";

            try
            {
                accidentclaimid = model.AccidentClaimId.ToString();
                int accd = Convert.ToInt16(accidentclaimid);
                obj.AddParameter("@ACCIDENTCLAIM_ID", accidentclaimid);
                ds = obj.ExecuteDataSet("Proc_GetClaimDataXML", CommandType.StoredProcedure);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strReturnXML = dr[0].ToString();

                    }
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strReturnXML);
                XmlNodeList xnList = doc.SelectNodes("/INPUTXML/Claims");
                foreach (XmlNode xn in xnList)
                {
                    claimid = xn["ClaimID"].InnerText;
                }
                Domain = ConfigurationManager.AppSettings["IDomain"];
                username = ConfigurationManager.AppSettings["IUserName"];
                passwd = ConfigurationManager.AppSettings["IPassWd"];

                EAWXmlToPDFParser.XmlToPDFParser ObjXmlParser = new EAWXmlToPDFParser.XmlToPDFParser();
                ObjXmlParser.IDomain = Domain;
                ObjXmlParser.IUserName = username;
                ObjXmlParser.IPassWd = passwd;

                var results = (from m in obj.MNT_TEMPLATE_MASTER where m.Template_Id == model.DocumentId select new { Tempaltepath = m.Template_Path, Filename = m.Filename, MapxmlPath = m.MappingXML_Path, MapXmlFileName = m.MappingXML_FileName, Tempaltecode = m.Template_Code }).FirstOrDefault();
                ModelState.Clear();
                pdfname = results.Filename;
                path = results.Tempaltepath;

                MapXmlName = results.MapXmlFileName;
                XmlPath = results.MapxmlPath;
                templatecode = results.Tempaltecode;

                XmlPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + XmlPath);
                str = XmlPath + "/" + MapXmlName; //Server.MapPath(path + "/" + pdfname);

                doc.Load(str);
                ObjXmlParser.PdfMapXml = doc.InnerXml;
                ObjXmlParser.InputXml = strReturnXML;
                myFilePath = ConfigurationManager.AppSettings["OutPutFilePath"];
                pdftemplatepath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + path);
                ObjXmlParser.PdfTemplatePath = pdftemplatepath;

                pdfoutputpath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + myFilePath + "/" + claimid + "/" + templatecode + "/");
                ObjXmlParser.PdfOutPutPath = pdfoutputpath;

                pdfoutputfilename = (Path.GetFileNameWithoutExtension(pdfname) + "_" + claimid + "_" + templatecode + "_" + DateTime.Now.Ticks) + ".pdf";
                ObjXmlParser.PdfOutPutFileName = pdfoutputfilename;

                string fileName = "";
                try
                {
                    fileName = ObjXmlParser.GeneratePdf();
                    TempData["result"] = "File : " + fileName + " has been generated.";
                    model.Claimid = Convert.ToInt16(claimid);
                    model.Templatecode = templatecode;
                    model.DocumentName = fileName;
                    sdf = pdfoutputpath + fileName;
                    return new FilePathResult(sdf, "application/pdf");
                }
                catch (System.Data.DataException)
                {
                    ModelState.AddModelError("", "Unable to open Files.");
                    reportString = " Unable to open Files.";
                    return new FilePathResult(reportString, "application/text");
                }

            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error:" + ex.Message);
                sdf = "Error: " + ex.Message + ": " + ex.StackTrace;
                return new MCAS.Controllers.ClaimMastersController.FileStringResult(sdf, "application/text");

            }
        }
        public List<InsuranceModel> GetSearchResult(string ClaimNo, string IPNo, DateTime? LossDate, string vehicleNo, string ClaimantName, string VehicleRegnNo, string status)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO #TempTable SELECT distinct g.IsComplete,g.AccidentClaimId,clm.ClaimID,clm.ClaimType,isnull(co.UserDispName,'') as ClaimOfficer,isnull(g.ClaimNo,'') as ClaimNo,isnull(g.VehicleNo,'') as VehicleNo,isnull(clm.ClaimantName,'') as TPSurname,isnull(g.IsComplete,0) as ClaimStatus,g.AccidentDate,g.IPNo,clm.ClaimDate,clm.VehicleRegnNo,clm.ClaimantStatus,org.InsurerType,org.CountryOrgazinationCode FROM ClaimAccidentDetails g  left join CLM_Claims clm on g.PolicyId = clm.PolicyId and g.AccidentClaimId = clm.AccidentClaimId  left join MNT_InsruanceM u on u.PolicyId = g.PolicyId  left join MNT_Products c on u.ProductId = c.ProductId  left join MNT_Cedant d on u.CedantId = d.CedantId  left join MNT_ProductClass p on u.SubClassId = p.ID left join MNT_Users co on co.SNo  = clm.ClaimsOfficer  left join CLM_ServiceProvider sp on sp.AccidentId = g.AccidentClaimId and sp.ClaimantNameId = clm.ClaimID  left join MNT_Adjusters ad on ad.AdjusterId = sp.CompanyNameId and ad.AdjusterTypeCode in ('ADJ','SVY')  left join MNT_OrgCountry org on org.Id = g.Organization where (g.IsRecoveryOD = 'Y' or g.IsRecoveryBI = 'Y') and g.IsComplete =2 and clm.ClaimType != 2");

            if ((ClaimNo != null && ClaimNo != "") || (IPNo != null && IPNo != "") | (LossDate.Value.ToString() != "01/01/0001 00:00:00") | (vehicleNo != null && vehicleNo != "") | (ClaimantName != null && ClaimantName != "") | (VehicleRegnNo != null && VehicleRegnNo != ""))
            {
                sb.Append(" and");
            }
            if (ClaimNo != null && ClaimNo != "")
            {
                sb.Append(" UPPER(g.ClaimNo) Like UPPER('%" + ClaimNo + "%')");
                sb.Append(" and ");
            }

            if (IPNo != null && IPNo != "")
            {
                sb.Append(" UPPER(g.IPNo) Like UPPER('%" + IPNo + "%')");
                sb.Append(" and ");
            }

            //if (LossDate.Value.ToString() != "01/01/0001 00:00:00")
            if (LossDate.Value != DateTime.MinValue)
            {
                string dd = LossDate.Value.ToString("yyyy-MM-dd");
                sb.Append(" CAST(g.AccidentDate AS DATE) = '" + dd + "'");
                sb.Append(" and ");
            }
            if (vehicleNo != null && vehicleNo != "")
            {
                sb.Append(" UPPER(g.VehicleNo) Like UPPER('%" + vehicleNo + "%')");
                sb.Append(" and ");
            }
            if (ClaimantName != null && ClaimantName != "")
            {
                sb.Append(" UPPER(clm.ClaimantName) Like UPPER('%" + ClaimantName + "%')");
                sb.Append(" and ");
            }
            if (VehicleRegnNo != null && VehicleRegnNo != "")
            {
                sb.Append(" UPPER(clm.VehicleRegnNo) Like UPPER('%" + VehicleRegnNo + "%')");
                sb.Append(" and ");
            }
            string fin = sb.ToString().TrimEnd();
            string endval = fin.Split(' ').Last();
            string queryString = string.Empty;
            if (endval == "and")
            {
                queryString = fin.Substring(0, fin.LastIndexOf(" ") < 0 ? 0 : fin.LastIndexOf(" "));
            }
            else
            {
                queryString = fin;
            }
            MCASEntities obj = new MCASEntities();
            var List = obj.Proc_GetPaymentProcessSearch(queryString).ToList();
            var item = new List<InsuranceModel>();
            if (List.Any())
            {
                int i = 0;
                foreach (var data in List)
                {
                    i++;
                    item.Add(new InsuranceModel()
                    {
                        serialno = i,
                        AccidentId = data.AccidentClaimId,
                        PremiumAmount = 0,
                        ClaimNo = data.ClaimNo,
                        VehicleNo = data.VehicleNo,
                        ClaimOfficer = data.ClaimOfficer,
                        TPSurname = (data.TPSurname == null ? "" : data.TPSurname),
                        ClaimId = data.ClaimID,
                        AccidentDate = data.AccidentDate,
                        AccidentDatestr = data.AccidentDate == null ? "" : data.AccidentDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        IPNo = data.IPNo == null ? "" : data.IPNo,
                        ClaimDate = data.ClaimDate,
                        ClaimDatestr = data.ClaimDate == null ? "" : data.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        VehicleRegnNo = data.VehicleRegnNo,
                        ClaimType = (data.ClaimType == null ? "" : data.ClaimType == 1 ? "OD" : data.ClaimType == 2 ? "PD" : "BI"),
                        claimantStatus = (data.ClaimantStatus == null ? "" : data.ClaimantStatus == 1 ? "Pending " : data.ClaimantStatus == 2 ? "Finalized " : data.ClaimantStatus == 3 ? "Cancelled " : "Reopened")



                    });
                }
            }
            obj.Dispose();
            return item;

        }

        public JsonResult FillProducts()
        {
            var returnData = LoadProducts();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillCountries()
        {
            var returnData = LoadCountry();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillSubClass()
        {
            var returnData = LoadSubClassCode();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Claim Recovery Tab Processing
        public ActionResult ClaimRecoveryEditor(dynamic RecoveryId, dynamic AccidentClaimId, dynamic PolicyId)
        {
            string recoveryId = Convert.ToString(RecoveryId);
            var RecoveryID = Convert.ToString(RecoveryId) == "System.Object" ? "0" : recoveryId.Contains("System") ? RecoveryId[0] : recoveryId;

            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

            string Policyid = Convert.ToString(PolicyId);
            string PolicyID = Convert.ToString(Policyid) == "System.Object" ? "0" : Policyid.Contains("System") ? PolicyId[0] : Policyid;

            string Viewmode = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            string AccClaimId = MCASQueryString["AccidentClaimId"] != null ? Convert.ToString(MCASQueryString["AccidentClaimId"]) : "0";
            string ClaimID = MCASQueryString["ClaimID"] != null && MCASQueryString["ClaimID"] != "" ? Convert.ToString(MCASQueryString["ClaimID"]) : "0";
            string claimtype = MCASQueryString["ClaimType"] != null && MCASQueryString["ClaimType"] != "" ? Convert.ToString(MCASQueryString["ClaimType"]) : "0";
            string MandateId = MCASQueryString["MandateId"] != null && MCASQueryString["MandateId"] != "" ? Convert.ToString(MCASQueryString["MandateId"]) : "0";
            string PaymentId = claimtype == "3" ? MCASQueryString["PaymentId"] != null && MCASQueryString["PaymentId"] != "" ? Convert.ToString(MCASQueryString["PaymentId"]) : "0" : Convert.ToString(MCASQueryString["PaymentId"]);

            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            var InAdjustShowOutStandingAsInitial = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            var model = new ClaimRecoveryModel(Convert.ToInt32(AccidentId), Convert.ToInt32(claimtype));
            if (Viewmode == "Select")
            {
                int mid = Convert.ToInt32(MandateId);
                model.HApproveMandateAmt = (from ma in obj.CLM_MandateSummary where ma.MandateId == mid select ma.MovementMandateSP).FirstOrDefault();
            }
            try
            {
                model.AccidentClaimId = Convert.ToInt32(AccClaimId);
                model.ClaimId = Convert.ToInt32(ClaimID);
                model.ClaimType = Convert.ToInt32(claimtype);
                model.MandateId = Convert.ToInt32(MandateId);
                model.PaymentId = PaymentId != null ? Convert.ToInt32(PaymentId) : model.PaymentId;
                model.RecoverFromListOD = ClaimRecoveryModel.GetRecoverFromODList(Convert.ToInt32(AccClaimId), Convert.ToInt32(claimtype), Convert.ToInt32(ClaimID));
                model.RecoverFromListBI = model.RecoverFromListOD;
                if (RecoveryID != null && RecoveryID != "")
                {
                    TempData["DisplayDiv"] = "Display";
                    Session["ScreenNameDash"] = "215";
                    Session["screenID"] = "CLM_REC";
                    model.AccidentClaimId = Convert.ToInt32(AccidentId);
                    model = model.FetchRecovery(RecoveryID, model);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Loading Recovery Editor.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(AccidentClaimId.ToString()) ? "0" : AccidentClaimId.ToString());
                addInfo.Add("policy_id", String.IsNullOrEmpty(PolicyId.ToString()) ? "0" : PolicyId.ToString());
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", Convert.ToString(model.PaymentId));
                addInfo.Add("entity_type", "Recovery");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.RecoveryId)) ? 0 : model.RecoveryId, "Recovery");
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ClaimRecoveryEditor(ClaimRecoveryModel model, dynamic AccidentClaimId, dynamic PolicyId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string Policyid = Convert.ToString(PolicyId);
                string PolicyID = Convert.ToString(Policyid) == "System.Object" ? "0" : Policyid.Contains("System") ? PolicyId[0] : Policyid;

                model.ClaimType = model.FetchClaimType(model);
                //model = model.FetchRecovery(Convert.ToString(model.RecoveryId), model);
                model.PolicyId = Convert.ToInt32(PolicyID);
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                if (model.ClaimType == 1)
                {
                    ModelState["NetAmtRecovered"].Errors.Clear();
                }
                if (model.ClaimType == 3)
                {
                    ModelState["LeagalLawyerCost_R"].Errors.Clear();
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model.CreatedBy = LoggedInUserName;
                    model.PaymentId = model.PaymentId == 0 ? null : model.PaymentId;
                    model.Save();
                    ViewData["SuccessMsg"] = model.ResultMessage;
                }
                TempData["DisplayDiv"] = "Display";
                Session["ScreenNameDash"] = "215";
                Session["screenID"] = "CLM_REC";
                model.RecoverFromListOD = ClaimRecoveryModel.GetRecoverFromODList(Convert.ToInt32(model.AccidentClaimId), Convert.ToInt32(model.ClaimType), Convert.ToInt32(model.ClaimId));
                model.RecoverFromListBI = model.RecoverFromListOD;
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Loading Recovery Editor.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(AccidentClaimId.ToString()) ? "0" : AccidentClaimId.ToString());
                addInfo.Add("policy_id", String.IsNullOrEmpty(PolicyId.ToString()) ? "0" : PolicyId.ToString());
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", Convert.ToString(model.PaymentId));
                addInfo.Add("entity_type", "Recovery");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.RecoveryId)) ? 0 : model.RecoveryId, "Recovery");
                return View(model);
            }

        }
        public JsonResult GetRecoveryTopId(int AccId, int ClaimType)
        {
            var result = ClaimRecoveryModel.GetRecoveryTopId(AccId, ClaimType);
            return Json(result);
        }

        #endregion
    }
}
