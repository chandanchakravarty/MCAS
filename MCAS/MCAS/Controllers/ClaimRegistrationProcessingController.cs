using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.ClaimObjectHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;
using System.Web.Routing;
using System.IO;
using System.Collections.Specialized;
using MCAS.Globalisation;
using MCAS.Web.Objects.Resources.Common;
using System.Data.Objects.SqlClient;

namespace MCAS.Controllers
{
    public class ClaimRegistrationProcessingController : BaseController
    {
        //
        // GET: /ClaimRegProcPCNTX/
        MCASEntities obj = new MCASEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClaimEntryDetails()
        {
            return View(new ClaimAccidentDetailsModel());
        }

        public ActionResult _ClaimDeatailsPCNTX(int? PolicyId, int? AccidentClaimId, int? ClaimId)
        {
            ClaimObjectHelper claimDetails = getClaimObjectHelper();
            string caller = "";
            if (Request.QueryString["claimMode"] != null)
            {
                caller = Request.QueryString["claimMode"].ToString();
                claimDetails.ViewMode = getPageViewMode(caller);
            }
            if (AccidentClaimId.HasValue && !AccidentClaimId.Equals(0) && caller != "New")
            {
                if (claimDetails.AccidentClaimId != AccidentClaimId)
                {
                    ClearClaimObjectHelper();
                    var ClaimObjectHelper = new ClaimObjectHelper().Fetch((int)AccidentClaimId);
                    SetClaimObjectHelper(ClaimObjectHelper);
                    //UpdateClaimObjectHelper(ClaimObjectHelper.AccidentDetail, "Accident");
                    claimDetails = getClaimObjectHelper();
                    claimDetails.ViewMode = getPageViewMode(caller);
                }
            }

            claimDetails.CallerMenu = "New";
            if (PolicyId.HasValue)
                claimDetails.PolicyId = (int)PolicyId;
            if (Session["AccidentDate"] != null)
                claimDetails.ClaimDetail.TimeBarDate = (DateTime)Session["AccidentDate"];
 
            return PartialView("_ClaimDeatailsPCNTX", claimDetails);
        }
        #region "Accident"
        public ActionResult ClmAccDltPCNTXEditor(int policyId, string OrgCat, int AccidentClaimId = 0)
        {
            ClaimAccidentDetailsModel claimaccident = new ClaimAccidentDetailsModel();
            if ((AccidentClaimId != 0) && (AccidentClaimId != null))
            {
                claimaccident = claimaccident.FetchAll(AccidentClaimId);
                UpdateClaimObjectHelper(claimaccident, "Accident");
                var model = new ClaimForCRTXInfoModel(Convert.ToInt32(AccidentClaimId));
                model = model.Fetch(AccidentClaimId);
                if (model.ClaimID != null)
                {
                    UpdateClaimObjectHelper(model, "Claim");
                }
            }
            List<Status> list1 = new List<Status>();
            list1.Add(new Status() { ID = "Y", Name = "Yes" });
            list1.Add(new Status() { ID = "N", Name = "No" });
            SelectList sl1 = new SelectList(list1, "ID", "Name");
            claimaccident.generallookupvalue = sl1;
            if (AccidentClaimId == 0)
            {
                ClearClaimObjectHelper();
            }
            if (OrgCat != null)
            {
                Session["OrganisationType"] = OrgCat;
            }
            if (Session["OrganisationType"] == null)
            {
                string OrgTypeStr = ClaimAccidentDetailsModel.fetchOrganisationType(LoggedInUserId, claimaccident.Organization);
                Session["OrganisationType"] = OrgTypeStr;
            }

            claimaccident.hidOrgprop = Convert.ToString(Session["OrganisationType"]);
            claimaccident.OrgCatList = ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, "New", Convert.ToString(Session["OrganisationType"]));
            claimaccident.InsurerList = ClaimAccidentDetailsModel.fetchInsurerList();
            claimaccident.OwnerAddressDlt.Countrylist = LoadUserCountry(true);
            claimaccident.DriverAddressDlt.Countrylist = LoadUserCountry(true);
            claimaccident.PolicyId = 0;
            claimaccident.ViewMode = getPageViewMode("New");
            claimaccident.OwnerNameList = LookUpListItems.Fetch("OwnerName", true);
            claimaccident.OwnerAddressDlt.GenderList = LookUpListItems.Fetch("GENDER",true);
            claimaccident.DriverAddressDlt.GenderList = LookUpListItems.Fetch("GENDER", true);
            ViewData["SuccessMsgPcTx"] = TempData["SuccessMsgPcTx"];
            claimaccident.OrgCategory = Convert.ToString(Session["OrganisationType"]);
            claimaccident.ChckClaimComplete = claimaccident.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == claimaccident.AccidentClaimId select l.IsComplete).FirstOrDefault();
            var hasClaim = (from m in obj.CLM_Claims where m.AccidentClaimId == claimaccident.AccidentClaimId && m.ClaimantStatus != "3" select m).FirstOrDefault();
            if (hasClaim != null)
            {
                claimaccident.HClaimId = 1;
            }
            else
            {
                claimaccident.HClaimId = 0;
            }
            if (claimaccident.AccidentDate != null)
            {
                Session["AccidentDate"] = claimaccident.AccidentDate.Value.AddYears(6);
            }
             return View("ClmAccDltPCNTXEditor", claimaccident);
        }

        [HttpPost]
        public JsonResult GetDriverType(string OrganizationID)
        {
            if (!string.IsNullOrEmpty(OrganizationID))
            {
                Session["OrganizationID"] = OrganizationID;
            }
            var DriverList = ClaimAccidentDetailsModel.FetchCommonMasterDataForNew("DriverType", Convert.ToInt32(OrganizationID), true);
            return Json(DriverList);
        }

        [HttpPost]
        public ActionResult ClmAccDltPCNTXEditor(ClaimAccidentDetailsModel model)
        {
            string callermode = "";
            if (Request.QueryString["mode"] != null)
            {
                callermode = Request.QueryString["mode"].ToString();
            }
            try
            {
                List<TimeStatus> list = new List<TimeStatus>();
                list.Add(new TimeStatus() { ID = 1, Name = "AM" });
                list.Add(new TimeStatus() { ID = 2, Name = "PM" });
                SelectList sl = new SelectList(list, "ID", "Name");
                List<Status> list1 = new List<Status>();
                list1.Add(new Status() { ID = "Y", Name = Common.Yes });
                list1.Add(new Status() { ID = "N", Name = Common.No });
                SelectList sl1 = new SelectList(list1, "ID", "Name");
                string CreatedBy = LoggedInUserName;
                model.OrgCatList = ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, "", Convert.ToString(Session["OrganisationType"]));
                model.TimeformatRadioList = sl;
                model.LossTypeList = LoadLossType();
                model.LossNatureList = LoadLossNature();
                model.generallookupvalue = sl1;
                model.genderlookupvalue = LoadLookUpValue("GENDER");
                model.OperatingHoursList = LoadLookUpValue("OperatingHours");
                model.FinalLiabilityList = LoadLookUpValue("InvestigationResult");
                model.CollisionTypeList = LoadLookUpValue("CollisionType");
                model.InterchangeList = ClaimAccidentDetailsModel.fetchInterchangeList();
                model.BusCaptainlist = LoadBusCaptainDetails();
                model.ChckClaimComplete = model.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == model.AccidentClaimId select l.IsComplete).FirstOrDefault();
                //model.ChkODStatus = model.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == model.AccidentClaimId && l.ClaimType == 1 select l.ClaimID).FirstOrDefault();
                //model.ChkTPStatus = model.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == model.AccidentClaimId && (l.ClaimType == 2 || l.ClaimType == 3) select l.ClaimID).FirstOrDefault();
                model.OrgCategory = model.hidOrgprop;
                model.ChckClaimComplete = model.CheckClaimComplete();
                model.ChkODStatus = model.CheckODStatus();
                model.ChkTPStatus = model.CheckTPStatus();
                if (!ModelState.IsValid)
                {

                    int[] array = (from l in model.OrgCatList where l.Description.EndsWith("-Train") select l.OrgType).ToArray();
                    bool res = Array.Exists(array, element => element == model.Organization);
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    if ((Session["OrganisationType"].ToString().ToLower() == "tx") || (Session["OrganisationType"].ToString().ToLower() == "pc")) {
                        ModelState["BusServiceNo"].Errors.Clear();
                        ModelState["TPClaimentStatus"].Errors.Clear();
                        ModelState["ODStatus"].Errors.Clear();
                    }
                    if (res && allErrors.Count() == 1 && allErrors.FirstOrDefault().ErrorMessage == "Bus Service Number is required.")
                        ModelState["BusServiceNo"].Errors.Clear();
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    TempData["DisplayTP"] = "DisplayTP";
                    TempData["DisplayOD"] = "DisplayOD";
                    if (model.AccidentTime == null)
                    {
                        model.AccidentTime = "00:00";
                    }
                    model = model.Save();
                    UpdateClaimObjectHelper(model, "Accident");
                    ViewData["SuccessMsg"] = model.AccidentResult;
                    TempData["SuccessMsgPcTx"] = model.AccidentResult;
                    TempData["notice"] = model.AccidentResult;
                    ViewData["EnableTab"] = model.TPClaimentStatus == "Y" ? true : false;
                    ViewData["EnableODTab"] = model.ODStatus == "Y" ? true : false;
                }
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Loading Accident Tab.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(Convert.ToString(model.AccidentClaimId)) ? "0" : Convert.ToString(model.AccidentClaimId));
                addInfo.Add("policy_id", String.IsNullOrEmpty(Convert.ToString(model.PolicyId)) ? "0" : Convert.ToString(model.PolicyId));
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", Convert.ToString(model.AccidentClaimId));
                addInfo.Add("entity_type", "Payment");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.AccidentClaimId)) ? 0 : Convert.ToInt32(model.AccidentClaimId), "Accident");

            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                object routes = new { AccidentClaimId = model.AccidentClaimId, policyId = model.PolicyId, claimMode = "Write", mode = callermode };

                string res = RouteEncryptDecrypt.getRouteString(routes);
                res = RouteEncryptDecrypt.Encrypt(res);
                routes = new { Q = res };
                return RedirectToAction("ClmAccDltPCNTXEditor", routes);

            }
        }

        [HttpPost]
        public ActionResult CompleteClaim(int accidentClaimId, int OrganizationType, string ClaimPrefix)
        {
            try
            {
                JsonResult result = new JsonResult();
                bool isValid = false;
                ClaimRules objClaim = new ClaimRules();
                string strRuleString = objClaim.verifyClaim(accidentClaimId, ref isValid);
                if (isValid == true)
                {
                    int IsCompleteStatus = 2;
                    obj.ClearParameteres();
                    ViewData["SuccessMsg"] = "Claim has been completed.";
                    var acclist = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == accidentClaimId select l).FirstOrDefault();
                    acclist.IsComplete = 2;
                    acclist.ModifiedDate = DateTime.Now;
                    acclist.ModifiedBy = LoggedInUserName;
                    obj.SaveChanges();
                    var accidentdetails = getClaimAccidentObject();
                    accidentdetails.ViewMode = "Read";
                    accidentdetails.IsComplete = IsCompleteStatus;
                    accidentdetails.ReadOnly = true;
                    getClaimAccidentObject().ReadOnly = true;
                    UpdateClaimObjectHelper(accidentdetails, "Accident");
                    var claimModel = new ClaimForCRTXInfoModel(Convert.ToInt32(accidentdetails.AccidentClaimId));
                    claimModel = claimModel.Fetch(Convert.ToInt32(accidentdetails.AccidentClaimId));
                    var claimdetails = getClaimEntryInfoObject();
                    string claimStatus = "2";
                    //claimdetails.ClaimStatus = claimStatus;
                    //claimdetails.ViewMode = "Read";
                    //claimdetails.ReadOnly = true;
                    //UpdateClaimObjectHelper(claimdetails, "Claim");
                    claimModel.ClaimStatus = claimStatus;
                    claimModel.ViewMode = "Read";
                    claimModel.ReadOnly = true;
                    UpdateClaimObjectHelper(claimModel, "Claim");
                    var claimObjectHelper = getClaimObjectHelper();
                    claimObjectHelper.ReadOnly = true;
                    claimObjectHelper.ViewMode = "Read";
                    claimObjectHelper.IsComplete = IsCompleteStatus;
                    SetClaimObjectHelper(claimObjectHelper);
                    result.Data = new { IsValid = isValid, SuccessMsg = ViewData["SuccessMsg"], ModifiedBy = LoggedInUserName };
                    return Json(result);
                }
                else
                {
                    ViewBag.HtmlOutput = strRuleString;
                    result.Data = new { ResultStr = strRuleString, IsValid = isValid };
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error:" + ex.Message);
            }
            return View("ClaimAccidentEditor");
        }

        public ActionResult ClmAccidentEditorNew(int policyId, string OrgCat)
        {
            var model = new ClaimAccidentDetailsModel();
            string CreatedBy = LoggedInUserName;
            List<Status> list1 = new List<Status>();
            list1.Add(new Status() { ID = "Y", Name = "Yes" });
            list1.Add(new Status() { ID = "N", Name = "No" });
            SelectList sl1 = new SelectList(list1, "ID", "Name");
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            ClearClaimObjectHelper();
            var claimaccident = new ClaimAccidentDetailsModel();
            if (OrgCat != null)
            {
                Session["OrganisationType"] = OrgCat;
            }
            if (Session["OrganisationType"] == null)
            {
                string OrgTypeStr = ClaimAccidentDetailsModel.fetchOrganisationType(LoggedInUserId, claimaccident.Organization);
                Session["OrganisationType"] = OrgTypeStr;
            }

            claimaccident.hidOrgprop = Convert.ToString(Session["OrganisationType"]);
            claimaccident.OrgCatList = ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, "New", Convert.ToString(Session["OrganisationType"]));
            claimaccident.InsurerList = ClaimAccidentDetailsModel.fetchInsurerList();
            claimaccident.OwnerAddressDlt.Countrylist = LoadUserCountry(true);
            claimaccident.DriverAddressDlt.Countrylist = LoadUserCountry(true);
            claimaccident.PolicyId = 0;
            claimaccident.generallookupvalue = sl1;
            claimaccident.ViewMode = getPageViewMode("New");
            claimaccident.OwnerNameList = LookUpListItems.Fetch("OwnerName", true);
            claimaccident.OwnerAddressDlt.GenderList = LookUpListItems.Fetch("GENDER");
            claimaccident.DriverAddressDlt.GenderList = LookUpListItems.Fetch("GENDER");
            ViewData["SuccessMsgPcTx"] = TempData["SuccessMsgPcTx"];
            claimaccident.OrgCategory = Convert.ToString(Session["OrganisationType"]);
            claimaccident.ChckClaimComplete = claimaccident.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == claimaccident.AccidentClaimId select l.IsComplete).FirstOrDefault();
            return View("ClmAccDltPCNTXEditor", claimaccident);
        }

        public JsonResult Autocomplete(string term)
        {
            var items = (from vl in obj.MNT_VehicleListingMaster where vl.VehicleRegNo != null select vl.VehicleRegNo).ToList();
            var filteredItems = items.Where(item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FillMakeAndModel(string VehNo, string Uid)
        {
            var result = ClaimAccidentDetailsModel.FetchFillMakeAndModel(VehNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckVechile(string Vechileval)
        {
            MCASEntities db = new MCASEntities();
            var correctvech = (from l in db.MNT_VehicleListingMaster where l.VehicleRegNo == Vechileval select l).ToList();
            var result = "F";
            if (correctvech.Any())
            {
                result = "T";
            }
            db.Dispose();
            return Json(result);
        }
        #endregion

        #region "Service Provider"

        public ActionResult ClmSPDltPCNTX(string Viewmode = "", int PolicyId = 0, int AccidentClaimId = 0, int ServiceProviderId = -1, int ActDone = 0)
        {
            ServiceProviderModel spmodel = new ServiceProviderModel(AccidentClaimId);
            ViewBag.ServiceProviderId = ServiceProviderId;
            ViewBag.AccidentClaimId = AccidentClaimId;
            ViewBag.PolicyId = PolicyId;
            ViewBag.ActDone = ActDone;
            ViewBag.Viewmode = Viewmode;
            return View("ClmSPDltPCNTX", spmodel);
        }

        public JsonResult GetClaimantNameList(string Acc, string CType)
        {
            var list = obj.Proc_GetCLM_Claim_ClamiantName(Convert.ToInt32(Acc), Convert.ToInt32(CType)).ToList();
            var item = new List<ClaimantStatus>();
            item = (from data in list
                    select new ClaimantStatus()
                    {
                        Id = data.ClaimID,
                        Text = data.ClaimantName
                    }
                        ).ToList();
            return Json(item);
        }


        public JsonResult GetCompanyNameList(string InsurerType, string PartyTypeId,string Status)
        {
            var list = obj.Proc_GetMNT_Cedant_CompanyName(InsurerType, PartyTypeId, Status).ToList();
            var item = new List<ClaimantStatus>();
            item = (from data in list
                    select new ClaimantStatus()
                    {
                        Id = data.CedantId,
                        Text = data.CedantName
                    }
                        ).ToList();
            return Json(item);
        }
        public JsonResult GetCompanyNameDetailList(string InsurerType, string PartyTypeId, string CompanyNameId)
        {
            var list = obj.Proc_GetCompanyNameDetailList(InsurerType, PartyTypeId, CompanyNameId).ToList();
            var item = new List<ServiceProviderModel>();
            item = (from data in list
                    select new ServiceProviderModel()
                    {
                        CountryId = data.country,
                        Address1 = data.Address,
                        PostalCode1 = data.PostalCode,
                        Status = data.Status,
                        Address2 = data.Address2,
                        Address3 = data.Address3,
                        City = data.City,
                        State = data.State,
                        ContactPersonName = data.FirstContactPersonName,
                        EmailAddress = data.EmailAddress1,
                        OfficeNo = data.OfficeNo1,
                        Mobile = data.MobileNo1,
                        Fax = data.FaxNo1,
                        ContactPersonName2nd = data.SecondContactPersonName,
                        EmailAddress2nd = data.EmailAddress2,
                        OfficeNo2nd = data.OfficeNo2,
                        Mobile2nd = data.MobileNo2,
                        Fax2nd = data.FaxNo2,
                        Remarks = data.Remarks
                    }
                        ).ToList();
            return Json(item);
        }
        #endregion

        #region "Claim"

        [HttpGet]
        public ActionResult ClmClaimDtlPCNTX(int? ClaimID, dynamic AccidentClaimId)
        {
            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;
            var model = new ClaimForCRTXInfoModel(Convert.ToInt32(AccidentId));
            ConfirmAmtModel confirmamtmodel;
            try
            {
                var ClaimId = Convert.ToString(ClaimID) == "" ? "0" : Convert.ToString(ClaimID);
                model = model.FetchClaim(model, ClaimId, Convert.ToInt32(AccidentId));
                confirmamtmodel = model.Confirmamtbd;
                if (confirmamtmodel != null)
                {
                    TempData["ConfirmAmtBreakdown"] = confirmamtmodel;
                }

                if (ClaimId == "" || ClaimId == "0")
                {
                TempData.Remove("ConfirmAmtBreakdown");
                }

                var claimaccident = getClaimAccidentObject();
                claimaccident.IsComplete = getcompletestatus(Convert.ToInt32(AccidentId));
                if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
                string caller = MCASQueryString["claimMode"].ToString();
                TempData["IsCompleteprop"] = claimaccident.IsComplete;
                if (ClaimID != null && AccidentId != "0" && obj.Proc_GetCLM_ClaimListClaimId(ClaimId).FirstOrDefault() != null)
                {
                    TempData["hidedrop"] = "Hide";
                    TempData["headerval"] = model.ClaimType == 1 ? "Own Damage" : model.ClaimType == 2 ? "TPPD" : model.ClaimType == 3 ? "TPBI" : model.ClaimType == 4 ? "Recovery" : "";
                }
                model.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
                model.ViewMode = getPageViewMode(caller);
                Session["Module"] = "";
                Session["Module"] = "CrTx";
                ViewData["SuccessMsg"] = TempData["SuccessMsg"];

                TempData["clmDatamodel"] = model;
 
                return View("ClmClaimDtlPCNTX", model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult ClmClaimDtlPCNTX(ClaimForCRTXInfoModel model, dynamic AccidentClaimId, dynamic PolicyId)
        {
             
            try
            {
                MCASEntities objEntity = new MCASEntities();
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string policyId = Convert.ToString(PolicyId);
                string policyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : policyId.Contains("System") ? PolicyId[0] : policyId;
                model.FetchAllLists(model);
                model.ClaimType = 4;
                Session["Module"] = "CrTx";
                ConfirmAmtModel confirmamtmodel = new ConfirmAmtModel();
                if (TempData["ConfirmAmtBreakdown"] != null)
                {
                    confirmamtmodel = (ConfirmAmtModel)TempData["ConfirmAmtBreakdown"];
                }
                if (AccidentId == null || AccidentId == "" || AccidentId == "0")
                {
                    ViewData["SuccessMsg"] = "Can not save as Accident Tab is not save yet.";
                    TempData["DisplayDiv"] = "Display";
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        if (Session["OrganisationType"].ToString().ToLower() == "tx")
                        {
                            ModelState["CaseStatus"].Errors.Clear();
                            //Commented and change due to tfs 22091 & 21991
                            //ModelState["CaseTypeL2"].Errors.Clear();
                            //ModelState["Sharellocation"].Errors.Clear();
                            if (!ModelState.IsValid)
                            {
                                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                                return View(model);
                            }
                        }
                        else
                        {
                            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                            return View(model);
                        }

                    }
                    if (ModelState.IsValid)
                    {
                        ModelState.Clear();
                        model.Save(confirmamtmodel);
                       
                        var val1 = objEntity.CLM_Claims.Where(x =>x.AccidentClaimId== model.AccidentClaimId ).Select(x => x.ClaimID== model.ClaimID).FirstOrDefault();
                        if (val1 == true)
                        {
                            UpdateClaimObjectHelper(model, "Claim");
                        }
                       
                        ViewData["SuccessMsg"] = model.ResultMessage;
                        TempData["SuccessMsg"] = model.ResultMessage;
                        TempData["DisplayDiv"] = "Display";
                    }
                    object routes = new { AccidentClaimId = model.AccidentClaimId, policyId = model.PolicyId, ClaimID = model.ClaimID, claimMode = "Write" };

                    string res = RouteEncryptDecrypt.getRouteString(routes);
                    res = RouteEncryptDecrypt.Encrypt(res);
                    routes = new { Q = res };
                    return RedirectToAction("ClmClaimDtlPCNTX", routes);
                }
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }

            return View(model);
        }

        #endregion

        #region "Notes"
        #endregion

        #region "Tasks"
        #endregion

        #region "Attachments"
        #endregion

        #region "Alerts"
        #endregion

        #region "Claim Quantum"
        //Claim Quantum
        public ActionResult ClmClaimQutmPCNTX(dynamic ReserveId, dynamic AccidentClaimId)
        {
            string crid = Convert.ToString(ReserveId);
            string rID = crid.Contains("System") ? "" : Convert.ToString((System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"].ToUpper().Equals("YES") ? ReserveId : ReserveId[0]));
            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;
            var Viewmode = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            var ClaimId = MCASQueryString["ClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimId"]);
            var model = new ClmClaimQutmPCNTXModel(Convert.ToInt32(AccidentId));
            try
            {
                if ((rID != null && rID != "") || (Viewmode == "Edit" || Viewmode == "Adjust"))
                {
                    TempData["DisplayDiv1"] = "Display";
                }
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                int accidentId = Convert.ToInt32(AccidentId);
                model.ClaimRef = (from m in obj.ClaimAccidentDetails where m.AccidentClaimId == accidentId select m.CDGIClaimRef).FirstOrDefault();
                model.TPVehicleNo = (from m in obj.CLM_Claims where m.AccidentClaimId == accidentId && m.ClaimID == ClaimId select m.TPVehicleNo).FirstOrDefault();
                var TPInsurer = (from c in obj.CLM_Claims where c.AccidentClaimId == accidentId && c.ClaimID == ClaimId select c.TPInsurer).FirstOrDefault();
                int TPInsuerval = Convert.ToInt32(TPInsurer);
                model.TPInsurer = (from c in obj.MNT_Cedant where c.CedantId == TPInsuerval select c.CedantName).FirstOrDefault();
                //model.TPInsurer = (from c in obj.CLM_Claims 
                //                   join p in obj.MNT_InsruanceM on c.TPInsurer equals SqlFunctions.StringConvert((double)p.CedantId) into ps
                //                   from p in ps.DefaultIfEmpty()
                //                   join q in obj.MNT_Cedant on p.CedantId equals q.CedantId into qs
                //                   from q in qs.DefaultIfEmpty()
                //                   where c.AccidentClaimId == accidentId && c.ClaimID == ClaimId
                //                   select q.CedantName).Distinct().FirstOrDefault();
                //model.TPInsurer = TPInsurer;
                model.Horgtype = (from c in obj.ClaimAccidentDetails
                                  join p in obj.MNT_OrgCountry on c.Organization equals p.Id into ps
                                  from p in ps.DefaultIfEmpty()
                                  where c.AccidentClaimId == accidentId
                                  select p.InsurerType).FirstOrDefault();
                //model.TPVehNoList = ClmClaimQutmPCNTXModel.GetTPVehNoList(Convert.ToInt32(AccidentId));
                //model.TPInsurerList = ClmClaimQutmPCNTXModel.GetTPInsurerList(Convert.ToInt32(AccidentId));
                model = model.FetchReserve(rID, model, Viewmode);
                model.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ClmClaimQutmPCNTX(ClmClaimQutmPCNTXModel model, dynamic AccidentClaimId, dynamic PolicyId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string policyId = Convert.ToString(PolicyId);
                string PolicyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : policyId.Contains("System") ? PolicyId[0] : policyId;


                model.RateperdayList = ClaimReserveModel.FetchRateperdayList();
                //model.SelectListClamiantName = ClaimReserveModel.SelectOnlyList();
                //model.HopitalNameIdList = ClaimReserveModel.FetchHopitalNameIdList();
                //model.AssignToIdList = ClaimReserveModel.FetchAssignToIdListList();
                //var ClaimId = MCASQueryString["ClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimId"]);
                var ClaimId = model.ClaimID == null ? 0 : Convert.ToInt32(model.ClaimID);
                int accidentId = Convert.ToInt32(AccidentId);
                model.ClaimRef = (from m in obj.ClaimAccidentDetails where m.AccidentClaimId == accidentId select m.CDGIClaimRef).FirstOrDefault();
                model.TPVehicleNo = (from m in obj.CLM_Claims where m.AccidentClaimId == accidentId && m.ClaimID == ClaimId select m.TPVehicleNo).FirstOrDefault();
                var TPInsurer = (from c in obj.CLM_Claims where c.AccidentClaimId == accidentId && c.ClaimID == ClaimId select c.TPInsurer).FirstOrDefault();
                int TPInsuerval = Convert.ToInt32(TPInsurer);
                model.TPInsurer = (from c in obj.MNT_Cedant where c.CedantId == TPInsuerval select c.CedantName).FirstOrDefault();
                //model.TPInsurer = (from c in obj.CLM_Claims
                //                   join p in obj.MNT_InsruanceM on c.TPInsurer equals System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)p.CedantId) into ps
                //                   from p in ps.DefaultIfEmpty()
                //                   join q in obj.MNT_Cedant on p.CedantId equals q.CedantId into qs
                //                   from q in qs.DefaultIfEmpty()
                //                   where c.AccidentClaimId == accidentId && c.ClaimID == ClaimId
                //                   select q.CedantName).FirstOrDefault();
                model.Horgtype = (from c in obj.ClaimAccidentDetails
                                  join p in obj.MNT_OrgCountry on c.Organization equals p.Id into ps
                                  from p in ps.DefaultIfEmpty()
                                  where c.AccidentClaimId == accidentId
                                  select p.InsurerType).FirstOrDefault();
                //model.TPVehNoList = ClmClaimQutmPCNTXModel.GetTPVehNoList(Convert.ToInt32(AccidentId));
                //model.TPInsurerList = ClmClaimQutmPCNTXModel.GetTPInsurerList(Convert.ToInt32(AccidentId));
                model.CreatedBy = LoggedInUserId;
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                if (AccidentId == null || AccidentId == "" || AccidentId == "0")
                {
                    ViewData["SuccessMsg"] = "Can not save as Accident Tab is not save yet.";
                    TempData["DisplayDiv1"] = "Display";
                }
                else
                {
                    model.PolicyId = Convert.ToInt32(PolicyID);
                    model.AccidentId = Convert.ToInt32(AccidentId);
                    if (!ModelState.IsValid)
                    {
                        var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    }
                    if (ModelState.IsValid)
                    {
                        ModelState.Clear();
                        model.Save();
                        //if (model.ClaimantNameId != null && model.ClaimantNameId != 0)
                        //{
                        //    ViewData["SuccessMsg"] = "Record saved successfully.";
                        //}
                        //else
                        //{
                        //    ViewData["SuccessMsg"] = "Record updated successfully.";
                        //}
                        ViewData["SuccessMsg"] = model.ResultMessage;
                        TempData["DisplayDiv1"] = "Display";
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving Reserve.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(AccidentClaimId) ? "0" : AccidentClaimId.ToString());
                addInfo.Add("policy_id", String.IsNullOrEmpty(PolicyId) ? "0" : PolicyId.ToString());
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", Convert.ToString(model.ReserveId));
                addInfo.Add("entity_type", "Reserve");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.ReserveId)) ? 0 : Convert.ToInt32(model.ReserveId), "Reserve");
                return View(model);
            }

        }

        public JsonResult GetRateperdayList(string UniqueId)
        {
            var item = new List<CommonUtilities.CommonType>();
            item = ClaimReserveModel.FetchRateperdayList();
            return Json(item);
        }

        public ActionResult ClmClaimQutmPCNTXList()
        {
            return View();
        }
        #endregion

        #region "Mandate"
        public ActionResult MandateCrTx(dynamic AccidentClaimId, dynamic MandateId, string ViewOnly)
        {
            var model = new ClaimMandatePCNTXModel();
            string mandtId = Convert.ToString(MandateId);
            string mandateId = Convert.ToString(MandateId) == "System.Object" ? null : mandtId.Contains("System") ? MandateId[0] : mandtId;

            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

            string Viewmode = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            var claimId = MCASQueryString["ClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimId"]);

            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            CallerMenu = Convert.ToString(MCASQueryString["claimMode"]);
            SetCaller(Convert.ToInt32(AccidentId));
            try
            {
            model.userid = LoggedInUserId;
            model.groupcode = GetGroupcode(model.userid);
            model.AccidentClaimId = Convert.ToInt32(AccidentId);
            model.ClaimID = Convert.ToInt32(claimId);
            model.ClaimTypeList = LoadLookUpValue("ClaimType");
            model.investigationTypeList = LoadInvestigationResultValue("InvestigationResult");
            model.MandateList = ClaimMandatePCNTXModel.FetchMandatelist(AccidentId);
            model.ApproveRecoList = LoadLookUpValue("GENERAL", false, false).OrderByDescending(x => x.Lookup_value).ToList();
            model.EvidenceList = LoadLookUpValue("Evidence");
            model.ClaimType = MCASQueryString["ClaimType"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimType"]);
            model.MandateId = mandateId == null ? 0 : Convert.ToInt32(mandateId);
            //model = ClaimMandatePCNTXModel.FetchMandateModel(model, Viewmode, mandateId);
            model.AssignTypeListSP = ClaimMandatePCNTXModel.FetchAssignToListFromUserAdmin("SP");
            model.AssignTypeListCO = ClaimMandatePCNTXModel.FetchAssignToListFromUserAdmin("CO");
            model = ClaimMandatePCNTXModel.FetchMandateModel(model, Viewmode, mandateId);
            //ViewBag.MandateId = MandateId;
            TempData["Groupuser"] = ClaimMandatePCNTXModel.Groupuser(LoggedInUserId);
            TempData["RoleCode"] = RoleCode(LoggedInUserId);
            var IsFalOD = ClaimMandatePCNTXModel.ChkUserFALOD(LoggedInUserId);
            TempData["UserFALOD"] = IsFalOD != "" || IsFalOD != null ? true : false;
            var IsFalPDBI = ClaimMandatePCNTXModel.ChkUserFALPDBI(LoggedInUserId);
            TempData["UserFALPDBI"] = IsFalPDBI != "" || IsFalPDBI != null ? true : false;
            TempData["Claimtype"] = Convert.ToString(MCASQueryString["ClaimType"]) == null ? Convert.ToString(model.ClaimType) == null ? "" : Convert.ToString(model.ClaimType) : Convert.ToString(MCASQueryString["ClaimType"]);
            TempData["AssignedToSup"] = Convert.ToString(model.SupervisorAssignto);
            TempData["AssignedTo"] = Convert.ToString(model.AssignedTo);
            TempData["InvestigationRes"] = Convert.ToString(model.InvestigationResult);
            if (mandateId != null || Viewmode == "AddMandate")
            {
                //model = model.FetchMandate(Convert.ToInt32(mandateId), model);
                model = model.FetchMandate(mandateId, model);
                TempData["DisplayDiv1"] = "Display";
                TempData["ViewOnly"] = "N";
                ViewData["SelectedCategory"] = model.Evidence != null ? string.Join(",", model.Evidence.ToString().Split(',')) : "";
            }
            return View(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Loading Mandate Editor.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(Convert.ToString(AccidentClaimId)) ? "0" : AccidentClaimId.ToString());
                addInfo.Add("policy_id", String.IsNullOrEmpty(Convert.ToString(model.PolicyId)) ? "0" : Convert.ToString(model.PolicyId));
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", MandateId);
                addInfo.Add("entity_type", "Mandate");
                PublishException(ex, addInfo, String.IsNullOrEmpty(MandateId) ? 0 : int.Parse(MandateId), "Payment");
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult MandateCrTx(ClaimMandatePCNTXModel model, dynamic AccidentClaimId, dynamic claimId, FormCollection form)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string clmId = Convert.ToString(claimId);
                string ClaimId = Convert.ToString(clmId) == "System.Object" ? "0" : clmId.Contains("System") ? claimId[0] : clmId;


                model.userid = LoggedInUserId;
                model.groupcode = GetGroupcode(model.userid);
                model = ClaimMandatePCNTXModel.FetchMandateModel(model, "", "");
                model.ClaimTypeList = LoadLookUpValue("ClaimType");
                model.investigationTypeList = LoadInvestigationResultValue("InvestigationResult");
                model.ApproveRecoList = LoadLookUpValue("GENERAL", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                model.EvidenceList = LoadLookUpValue("Evidence");
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                model.ClaimID = Convert.ToString(ClaimId) != "" ? Convert.ToInt32(ClaimId) : 0;
                var CategoryList = form.GetValues("query_categoryBox");
                var SelCategoryList = new string[] { };
                string[] ResultList1 = new string[] { };
                string ResultString1 = "";
                ResultString1 = CategoryList != null ? string.Join(",", CategoryList.ToArray().ToArray()) : "";
                ViewData["SelectedCategory"] = ResultString1;
                model.Evidence = ResultString1;
                TempData["Groupuser"] = ClaimMandateModel.Groupuser(LoggedInUserId);
                TempData["RoleCode"] = ClaimMandateModel.RoleCode(LoggedInUserId);
                TempData["Claimtype"] = model.ClaimType.ToString();
                TempData["AssignedToSup"] = model.SupervisorAssignto.ToString();
                TempData["AssignedTo"] = model.AssignedTo.ToString();
                TempData["InvestigationRes"] = model.InvestigationResult.ToString();
                TempData["InformSafetytoreviewfindings"] = Convert.ToString(model.InformSafetytoreviewfindings);
                TempData["ClaimType"] = model.ClaimType.ToString();
                TempData["ViewOnly"] = "N";
                TempData["DisplayDiv1"] = "Display";
                ModelState.Clear();
                model.Save();
                //model = model.FetchMandate(Convert.ToInt32(model.MandateId), model);
                model = model.FetchMandate(Convert.ToString(model.MandateId), model);
                ViewData["SuccessMsg"] = model.ResultMessage;
                model.MandateList = ClaimMandatePCNTXModel.FetchMandatelist(AccidentId);
                model = ClaimMandatePCNTXModel.GetTotalAmountPaid(model);
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                }              


                //List<LookUpListItems> list1 = LoadLookUpValue("MISCCONDITION", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                //SelectList sl1 = new SelectList(list1, "Lookup_value", "Lookup_desc");
                //model.generallookupvalue = sl1;
                //model.userid = LoggedInUserId;
                //model.groupcode = GetGroupcode(model.userid);
                //model = ClaimMandateModel.FetchMandateModel(model, "", "");
                //model.ClaimTypeList = LoadLookUpValue("ClaimType");
                //model.investigationTypeList = LoadInvestigationResultValue("InvestigationResult");
                //model = model.GetRecoverableFromInsurerBIEZLinkCardNolist(model, "MISCCONDITION");
                //model.ApproveRecoList = LoadLookUpValue("GENERAL", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                //model.EvidenceList = LoadLookUpValue("Evidence");
                //model.InformSafetytoreviewfindingsList = LoadIInformSafetytoreviewfindingsValue("InformSafetytoreviewfindings");
                //model.AccidentClaimId = Convert.ToInt32(AccidentId);
                //model.ClaimID = Convert.ToString(ClaimId) != "" ? Convert.ToInt32(ClaimId) : 0;
                //var CategoryList = form.GetValues("query_categoryBox");
                //var SelCategoryList = new string[] { };
                //string[] ResultList1 = new string[] { };
                //string ResultString1 = "";
                //ResultString1 = CategoryList != null ? string.Join(",", CategoryList.ToArray().ToArray()) : "";
                //ViewData["SelectedCategory"] = ResultString1;
                //model.Evidence = ResultString1;
                //TempData["Groupuser"] = ClaimMandateModel.Groupuser(LoggedInUserId);
                //TempData["RoleCode"] = ClaimMandateModel.RoleCode(LoggedInUserId);
                //TempData["Claimtype"] = model.ClaimType.ToString();
                //TempData["AssignedToSup"] = model.SupervisorAssignto.ToString();
                //TempData["AssignedTo"] = model.AssignedTo.ToString();
                //TempData["InvestigationRes"] = model.InvestigationResult.ToString();
                //TempData["InformSafetytoreviewfindings"] = Convert.ToString(model.InformSafetytoreviewfindings);
                //TempData["ClaimType"] = model.ClaimType.ToString();
                //TempData["ViewOnly"] = "N";
                //TempData["DisplayDiv"] = "Display";
                //ModelState.Clear();
                //model.Save();
                //model = model.FetchMandate(Convert.ToString(model.MandateId), model);
                //ViewData["SuccessMsg"] = "Record Saved Successfully.";
                //model.MandateList = ClaimMandateModel.FetchMandatelist(AccidentId);
                //model = ClaimMandateModel.GetTotalAmountPaid(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving Mandate.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(AccidentClaimId) ? "0" : AccidentClaimId.ToString());
                addInfo.Add("policy_id", String.IsNullOrEmpty(Convert.ToString(model.PolicyId)) ? "0" : model.PolicyId.ToString());
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", Convert.ToString(model.MandateId));
                addInfo.Add("entity_type", "Reserve");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.MandateId)) ? 0 : Convert.ToInt32(model.MandateId), "Mandate");
                return View(model);
            }
            return View(model);
        }

        private void SetCaller(int id)
        {
            var claimaccident = getClaimAccidentObject();
            UpdateClaimObjectHelper(claimaccident, "Task", Convert.ToInt32(id));
            LoadMenuList();
        }

        public ActionResult _MandateCRTXListAll(string MandateRecordNo, string AccidentClaimId, string ClaimID, string ClaimType, string MandateId, string uid)
        {
            var list = ClaimMandatePCNTXModel.MandateCRTXListAll(MandateRecordNo, AccidentClaimId, ClaimID, ClaimType, MandateId);
            return View(list);
        }
        #endregion

        #region "Recovery"
        public ActionResult ClaimRecoveryCrTx(dynamic RecoveryId, dynamic AccidentClaimId, dynamic PolicyId)
        {
            string recoveryId = Convert.ToString(RecoveryId);
            string RecoveryID = Convert.ToString(RecoveryId) == "System.Object" ? "0" : recoveryId.Contains("System") ? RecoveryId[0] : recoveryId;

            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

            string Policyid = Convert.ToString(PolicyId);
            string PolicyID = Convert.ToString(Policyid) == "System.Object" ? "0" : Policyid.Contains("System") ? PolicyId[0] : Policyid;

            var Viewmode = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            string AccClaimId = MCASQueryString["AccidentClaimId"] != null ? Convert.ToString(MCASQueryString["AccidentClaimId"]) : "0";
            string ClaimID = MCASQueryString["ClaimID"] != null && MCASQueryString["ClaimID"] != "" ? Convert.ToString(MCASQueryString["ClaimID"]) : "0";
            string claimtype = MCASQueryString["ClaimType"] != null && MCASQueryString["ClaimType"] != "" ? Convert.ToString(MCASQueryString["ClaimType"]) : "0";
            var model = new ClaimRecoveryCrTxModel(Convert.ToInt32(AccidentId),Convert.ToInt32(claimtype));
            try
            {
                model.AccidentClaimId = Convert.ToInt32(AccClaimId);
                model.ClaimId = Convert.ToInt32(ClaimID);
                model.ClaimType = Convert.ToInt32(claimtype);
                model = ClaimRecoveryCrTxModel.FetchRecoveryModel(model, Viewmode, RecoveryID);
                int accidentId = Convert.ToInt32(AccidentId);
                model.Horgtype = (from c in obj.ClaimAccidentDetails
                                  join p in obj.MNT_OrgCountry on c.Organization equals p.Id into ps
                                  from p in ps.DefaultIfEmpty()
                                  where c.AccidentClaimId == accidentId
                                  select p.InsurerType).FirstOrDefault();

                if (RecoveryID != null && RecoveryID != "")
                {
                    TempData["DisplayDiv"] = "Display";
                    Session["ScreenNameDash"] = "208";
                    Session["screenID"] = "CLM_REG";
                    model.AccidentClaimId = Convert.ToInt32(AccidentId);
                    //model = model.FetchRecovery(RecoveryID, model);
                }
                if (string.IsNullOrEmpty(model.SORASerialNo))
                   {
                     string sora = model.SORASerialNo;
                     ClaimRecoveryCrTxModel.FetchSoraNo(Convert.ToString(RecoveryId), model.ClaimId, model.AccidentClaimId, obj, ref  sora);
                     model.SORASerialNo = sora;
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
                addInfo.Add("entity_type", "Recovery");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.RecoveryId)) ? 0 : model.RecoveryId, "Recovery");
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ClaimRecoveryCrTx(ClaimRecoveryCrTxModel model, dynamic AccidentClaimId, dynamic PolicyId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string Policyid = Convert.ToString(PolicyId);
                string PolicyID = Convert.ToString(Policyid) == "System.Object" ? "0" : Policyid.Contains("System") ? PolicyId[0] : Policyid;

                model.PolicyId = Convert.ToInt32(PolicyID);
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                int accidentId = Convert.ToInt32(AccidentId);
                model.Horgtype = (from c in obj.ClaimAccidentDetails
                                  join p in obj.MNT_OrgCountry on c.Organization equals p.Id into ps
                                  from p in ps.DefaultIfEmpty()
                                  where c.AccidentClaimId == accidentId
                                  select p.InsurerType).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model.CreatedBy = LoggedInUserName;
                    model.Save();
                    if(! string.IsNullOrEmpty(model.SORASerialNo))
                    {
                        ClaimRecoveryCrTxModel.UpdateSoraNoCodeMaster(model.SORASerialNo,model.ClaimId,model.RecoveryId, obj);
                    }
                    ViewData["SuccessMsg"] = model.ResultMessage;
                }
                TempData["DisplayDiv"] = "Display";
                Session["ScreenNameDash"] = "208";
                Session["screenID"] = "CLM_REG";
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
                addInfo.Add("entity_type", "Recovery");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.RecoveryId)) ? 0 : model.RecoveryId, "Recovery");
                return View(model);
            }
        }
        #endregion

        private void UpdateClaimObjectHelper(BaseModel model, string Caller, int accidentid = 0)
        {
            ClaimObjectHelper claimObjectHelper = getClaimObjectHelper();
            if (Caller.Equals("Accident"))
            {
                claimObjectHelper.AccidentClaimId = (int)((ClaimAccidentDetailsModel)model).AccidentClaimId;
                claimObjectHelper.PolicyId = (int)((ClaimAccidentDetailsModel)model).PolicyId;
                claimObjectHelper.IPNo = ((ClaimAccidentDetailsModel)model).IPNo;
                claimObjectHelper.AccidentDate = ((ClaimAccidentDetailsModel)model).AccidentDate.ToString();
                claimObjectHelper.AccidentTime = ((ClaimAccidentDetailsModel)model).AccidentTime;
                claimObjectHelper.BusServiceNo = ((ClaimAccidentDetailsModel)model).BusServiceNo;
                claimObjectHelper.ClaimNo = ((ClaimAccidentDetailsModel)model).ClaimNo;
                claimObjectHelper.VehicleNo = ((ClaimAccidentDetailsModel)model).VehicleNo;
                claimObjectHelper.Organization = ((ClaimAccidentDetailsModel)model).Organization;
                var orgtypeid = ((ClaimAccidentDetailsModel)model).Organization;
                claimObjectHelper.OrganizationName = (from l in ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, "", Convert.ToString(Session["OrganisationType"])).Where(x => x.OrgType == orgtypeid) select l.Description).FirstOrDefault();
                claimObjectHelper.AccidentImage = ((ClaimAccidentDetailsModel)model).AccidentImage;
                claimObjectHelper.IsComplete = ((ClaimAccidentDetailsModel)model).IsComplete;
                claimObjectHelper.AccidentDetail = ((ClaimAccidentDetailsModel)model);
                claimObjectHelper.ViewMode = ((ClaimAccidentDetailsModel)model).ViewMode;
                claimObjectHelper.ReadOnly = ((ClaimAccidentDetailsModel)model).ReadOnly;
            }

            else if (Caller.Equals("Claim"))
            {
                ClaimAccidentDetail accdetail = new ClaimAccidentDetail();
                if (((ClaimForCRTXInfoModel)model).ClaimID != null)
                {
                    claimObjectHelper.ClaimId = (int)((ClaimForCRTXInfoModel)model).ClaimID;
                }
                if (((ClaimForCRTXInfoModel)model).AccidentClaimId != null)
                {
                    claimObjectHelper.AccidentClaimId = (int)((ClaimForCRTXInfoModel)model).AccidentClaimId;
                }
                claimObjectHelper.InvoiceNo = ((ClaimForCRTXInfoModel)model).InvoiceNo;
                claimObjectHelper.JobNo = ((ClaimForCRTXInfoModel)model).JobNo;
                //claimObjectHelper.ClaimType = Convert.ToString(((ClaimForCRTXInfoModel)model).ClaimType);
                var claimTypeId = Convert.ToString(((ClaimForCRTXInfoModel)model).ClaimType);
                claimObjectHelper.ClaimType = obj.MNT_Lookups.Where(x => x.Lookupvalue == claimTypeId && x.Category == "ClaimType").Select(y => y.Lookupdesc).Single();
                var claimStatusId = Convert.ToString(((ClaimForCRTXInfoModel)model).ClaimantStatus);
                claimObjectHelper.ClaimStatus = obj.MNT_Lookups.Where(x => x.Lookupvalue == claimStatusId && x.Category == "ClaimantStatus").Select(y => y.Lookupdesc).Single();
                claimObjectHelper.ClaimCrTxDetails = ((ClaimForCRTXInfoModel)model);
                claimObjectHelper.ViewMode = ((ClaimForCRTXInfoModel)model).ViewMode;
                // claimObjectHelper.CDGIClaimRefNo = ((ClaimAccidentDetailsModel)model).ClaimNo;
                //claimObjectHelper.CDGIClaimRefNo = ((ClaimForCRTXInfoModel)model).claimAccidentDetailsModel.ClaimNo;
                var ClaimRefNo = ClaimForCRTXInfoModel.fetchClaimRefNo(((ClaimForCRTXInfoModel)model).AccidentClaimId);
                claimObjectHelper.CDGIClaimRefNo = ClaimRefNo;
            }
            else if (Caller.Equals("Task"))
            {
                ClaimAccidentDetailsModel clmmodel = new ClaimAccidentDetailsModel();
                var accresult = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == accidentid select l).FirstOrDefault();
                DataMapper.Map(accresult, clmmodel, true);
                claimObjectHelper.AccidentClaimId = accresult.AccidentClaimId;
                claimObjectHelper.PolicyId = accresult.PolicyId;
                claimObjectHelper.IPNo = accresult.IPNo;
                claimObjectHelper.AccidentDate = accresult.AccidentDate.ToString();
                claimObjectHelper.AccidentTime = accresult.AccidentTime;
                claimObjectHelper.BusServiceNo = accresult.BusServiceNo;
                claimObjectHelper.ClaimNo = accresult.ClaimNo;
                claimObjectHelper.VehicleNo = accresult.VehicleNo;
                claimObjectHelper.Organization = accresult.Organization;
                var orgtypeid = accresult.Organization;
                claimObjectHelper.OrganizationName = (from l in ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, "", Convert.ToString(Session["OrganisationType"])).Where(x => x.OrgType == orgtypeid) select l.Description).FirstOrDefault();
                claimObjectHelper.AccidentImage = accresult.AccidentImage;
                claimObjectHelper.IsComplete = accresult.IsComplete;
                claimObjectHelper.AccidentDetail = clmmodel;
                claimObjectHelper.ViewMode = accresult.IsComplete == 2 ? "Read" : "Write";
            }
            SetClaimObjectHelper(claimObjectHelper);
        }

        protected List<LookUpListItems> LoadInvestigationResultValue(string category, bool addAll = true, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            list = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "0", Lookup_desc = "[Select...]" });
            }
            if (addNone)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "(None)" });
            }
            obj.Dispose();
            return list;
        }

        public JsonResult GetEncryptedUrl(string Url)
        {
            string RouteString = String.Empty;
            object objRoute;
            string result = String.Empty;
            try
            {
                var values = ((RouteTable.Routes.GetRouteData(new HttpContextWrapper(((new HttpContext(((new HttpRequest(null, Request.Url.Scheme + "://" + Request.Url.Host + Url.Split('?')[0], Url.Split('?')[1]))), ((new HttpResponse(new StringWriter())))))))))).Values;
                var controller = Convert.ToString(values["controller"]);
                var action = Convert.ToString(values["action"]);
                string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"];
                if (isEncryptedParams.ToUpper().Equals("YES"))
                {
                    RouteString = RouteEncryptDecrypt.Encrypt(Url.Split('?')[1].EndsWith("&") ? Url.Split('?')[1].Substring(0, Url.Split('?')[1].Length - 1).Replace("&", "?") : Url.Split('?')[1].Replace("&", "?"));
                    objRoute = new { Q = RouteString };
                    UrlHelper url = new UrlHelper(Request.RequestContext);
                    result = url.Action(action, controller, objRoute);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ConfirmAmountDtl(string ClaimID, string AccidentClaimId)
        {
            ConfirmAmtModel model = new ConfirmAmtModel();
            if (TempData["ConfirmAmtBreakdown"] != null)
            {
                model = (ConfirmAmtModel)TempData["ConfirmAmtBreakdown"];
            }

            return View("ConfirmAmountDtl", model);
        }

        public JsonResult ConfirmAmtBreakdowndetails(ConfirmAmtModel model)
        {
            string result = String.Empty;
            TempData["ConfirmAmtBreakdown"] = model;
            TempData.Keep("ConfirmAmtBreakdown");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
