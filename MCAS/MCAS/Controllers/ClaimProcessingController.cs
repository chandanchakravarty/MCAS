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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;
using System.Transactions;
using System.Data.Objects;
using System.Collections.Specialized;
using System.Data.Objects.SqlClient;
using System.Configuration;
using System.Net;
using System.ComponentModel;
using MCAS.Web.Objects.Resources.Common;
using System.Linq.Dynamic;
using System.Web.Routing;
using MCAS.Globalisation;


namespace MCAS.Controllers
{
    public class ClaimProcessingController : BaseController
    {
        MCASEntities obj = new MCASEntities();
        public ActionResult ClaimTabs()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        #region "Claim Accident Detail"
        public ActionResult ClaimEntryDetails()
        {
            return View(new ClaimAccidentDetailsModel());
        }
        public ActionResult _ClaimDetails(int? PolicyId, int? AccidentClaimId, int? ClaimId)
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
                    UpdateClaimObjectHelper(ClaimObjectHelper.AccidentDetail, "Accident");
                    claimDetails = getClaimObjectHelper();
                    claimDetails.ViewMode = getPageViewMode(caller);
                }
            }

            claimDetails.CallerMenu = CallerMenu;
            if (PolicyId.HasValue)
                claimDetails.PolicyId = (int)PolicyId;

            return PartialView("_ClaimDetails", claimDetails);
        }

        public ActionResult NewClaimProcessing()
        {
            var model = new ClaimAccidentDetailsModel();
            model.OrgCatTypeList = ClaimAccidentDetailsModel.fetchOrganizationCategory(); 
            return View(model);
        }

        public ActionResult ClaimAccidentDetails(int AccidentClaimId)
        {
            List<TimeStatus> list = new List<TimeStatus>();
            list.Add(new TimeStatus() { ID = 1, Name = "AM" });
            list.Add(new TimeStatus() { ID = 2, Name = "PM" });
            SelectList sl = new SelectList(list, "ID", "Name");

            List<Status> list1 = new List<Status>();
            list1.Add(new Status() { ID = "Y", Name = Common.Yes });
            list1.Add(new Status() { ID = "N", Name = Common.No });
            SelectList sl1 = new SelectList(list1, "ID", "Name");

            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            ClearClaimObjectHelper();
            var model = new ClaimAccidentDetailsModel();
            string CreatedBy = LoggedInUserName;
            var ClaimObjectHelper = new ClaimObjectHelper().Fetch(AccidentClaimId);
            SetClaimObjectHelper(ClaimObjectHelper);
            var claimaccident = getClaimAccidentObject();
            if (!AccidentClaimId.Equals(0) && caller != "New" && AccidentClaimId != 0)
            {

                if (!claimaccident.AccidentClaimId.HasValue || claimaccident.AccidentClaimId != AccidentClaimId)
                {
                    claimaccident.AccidentClaimId = AccidentClaimId;
                    claimaccident = claimaccident.Fetch();
                }
                else
                {
                    claimaccident.AccidentClaimId = AccidentClaimId;
                    claimaccident = claimaccident.Fetch();
                }

            }
            claimaccident.ViewMode = getPageViewMode(caller);
            //claimaccident.ReadOnly = true;
            claimaccident.TimeformatRadioList = sl;
            claimaccident.InterchangeList = ClaimAccidentDetailsModel.fetchInterchangeList();
            claimaccident.LossTypeList = LoadLossType();
            claimaccident.LossNatureList = LoadLossNature();
            claimaccident.OperatingHoursList = LoadLookUpValue("OperatingHours");
            claimaccident.generallookupvalue = sl1;
            claimaccident.genderlookupvalue = LoadLookUpValue("GENDER");
            claimaccident.FinalLiabilityList = LoadLookUpValue("InvestigationResult");
            //Impacted Code Commented due to TFS #21522
            //claimaccident.CollisionTypeList = LoadLookUpValue("CollisionType");
            //claimaccident.DistrictCodeList = LoadLookUpValue("DistrictCode");
            claimaccident.CollisionTypeList = ClaimAccidentDetailsModel.FetchCommonMasterData("CollisionType", AccidentClaimId);
            claimaccident.DistrictCodeList = ClaimAccidentDetailsModel.FetchCommonMasterData("DistrictCode", AccidentClaimId);
            claimaccident.BusCaptainlist = LoadBusCaptainDetails();
            string OrgTypeStr = ClaimAccidentDetailsModel.fetchOrganisationType(LoggedInUserId, claimaccident.Organization);
            Session["OrganisationType"] = OrgTypeStr;
            claimaccident.OrgCatList = ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, caller, Convert.ToString(Session["OrganisationType"]));
            claimaccident.ChckClaimComplete = claimaccident.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == claimaccident.AccidentClaimId select l.IsComplete).FirstOrDefault();
            claimaccident.ChkODStatus = claimaccident.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == claimaccident.AccidentClaimId && l.ClaimType == 1 select l.ClaimID).FirstOrDefault();
            claimaccident.ChkODStatus = claimaccident.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == claimaccident.AccidentClaimId && l.ClaimType == 1 select l.ClaimID).FirstOrDefault();
            claimaccident.ChkTPStatus = claimaccident.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == claimaccident.AccidentClaimId && (l.ClaimType == 2 || l.ClaimType == 3) select l.ClaimID).FirstOrDefault();

            var accDtls = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == AccidentClaimId select l).FirstOrDefault();
            if (accDtls != null)
            {
                claimaccident.CreatedBy = accDtls.CreatedBy;
                claimaccident.CreatedOn = Convert.ToDateTime(accDtls.CreatedDate);
                if (accDtls.ModifiedDate != null || accDtls.ModifiedDate.ToString() != "")
                {
                    claimaccident.ModifiedOn = Convert.ToDateTime(accDtls.ModifiedDate);
                    claimaccident.ModifiedBy = accDtls.ModifiedBy;
                }
                claimaccident.InitialLiability = accDtls.InitialLiability;
                claimaccident.CollisionType = accDtls.CollisionType;
            }

            ViewData["EnableTab"] = claimaccident.TPClaimentStatus == "Y" ? true : false;
            ViewData["EnableODTab"] = claimaccident.ODStatus == "Y" ? true : false;
            if (!AccidentClaimId.Equals(0) && caller != "New" && AccidentClaimId != 0)
            {
                UpdateClaimObjectHelper(claimaccident, "Accident");
            }
            else
            {
                ClearClaimObjectHelper();
            }
            if (claimaccident.TPAssignmentTranId != null)
            {
                TempData["DisplayTP"] = "DisplayTP";
            }
            if (claimaccident.ODAssignmentTranId != null)
            {
                TempData["DisplayOD"] = "DisplayOD";
            }
            return View("ClaimAccidentEditor", claimaccident);
        }
        public ActionResult ClaimAccidentEditorNew(int policyId,string OrgCat)
        {
            var model = new ClaimAccidentDetailsModel();
            string CreatedBy = LoggedInUserName;
            List<TimeStatus> list = new List<TimeStatus>();
            list.Add(new TimeStatus() { ID = 1, Name = "AM" });
            list.Add(new TimeStatus() { ID = 2, Name = "PM" });
            SelectList sl = new SelectList(list, "ID", "Name");

            List<Status> list1 = new List<Status>();
            list1.Add(new Status() { ID = "Y", Name = Common.Yes });
            list1.Add(new Status() { ID = "N", Name = Common.No });
            SelectList sl1 = new SelectList(list1, "ID", "Name");
            Session["OrganisationType"] = OrgCat;
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            ClearClaimObjectHelper();
            var claimaccident = new ClaimAccidentDetailsModel();
            claimaccident.PolicyId = policyId;
            claimaccident.hidOrgprop = Convert.ToString(Session["OrganisationType"]);
            claimaccident.ViewMode = getPageViewMode(caller);
            claimaccident.TimeformatRadioList = sl;
            claimaccident.OrgCatList = ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, caller, claimaccident.hidOrgprop);
            claimaccident.InterchangeList = ClaimAccidentDetailsModel.fetchInterchangeList();
            claimaccident.FinalLiabilityList = LoadLookUpValue("InvestigationResult");
            //Impacted Code Commented due to TFS #21522
            //claimaccident.CollisionTypeList = LoadLookUpValue("CollisionType");
            //claimaccident.DistrictCodeList = LoadLookUpValue("DistrictCode");
            claimaccident.CollisionTypeList = ClaimAccidentDetailsModel.FetchCommonMasterDataForNew("CollisionType", 0);
            claimaccident.DistrictCodeList = ClaimAccidentDetailsModel.FetchCommonMasterDataForNew("DistrictCode", 0);
            claimaccident.LossTypeList = LoadLossType();
            claimaccident.LossNatureList = LoadLossNature();
            claimaccident.generallookupvalue = sl1;
            claimaccident.genderlookupvalue = LoadLookUpValue("GENDER");
            claimaccident.OperatingHoursList = LoadLookUpValue("OperatingHours");
            claimaccident.BusCaptainlist = LoadBusCaptainDetails();
            claimaccident.ChckClaimComplete = claimaccident.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == claimaccident.AccidentClaimId select l.IsComplete).FirstOrDefault();
            claimaccident.ChkODStatus = claimaccident.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == claimaccident.AccidentClaimId && l.ClaimType == 1 select l.ClaimID).FirstOrDefault();
            claimaccident.ChkTPStatus = claimaccident.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == claimaccident.AccidentClaimId && (l.ClaimType == 2 || l.ClaimType == 3) select l.ClaimID).FirstOrDefault();
            claimaccident.OrgCategory = Convert.ToString(Session["OrganisationType"]);
            return View("ClaimAccidentEditor", claimaccident);
        }
        public ActionResult ClaimAccidentEditor(int policyId, int AccidentClaimId)
        {
            var model = new ClaimAccidentDetailsModel();
            string CreatedBy = LoggedInUserName;
            List<TimeStatus> list = new List<TimeStatus>();
            list.Add(new TimeStatus() { ID = 1, Name = "AM" });
            list.Add(new TimeStatus() { ID = 2, Name = "PM" });
            SelectList sl = new SelectList(list, "ID", "Name");
            List<Status> list1 = new List<Status>();
            list1.Add(new Status() { ID = "Y", Name = Common.Yes });
            list1.Add(new Status() { ID = "N", Name = Common.No });
            SelectList sl1 = new SelectList(list1, "ID", "Name");
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            var claimaccident = getClaimAccidentObject();
            if (claimaccident.AccidentClaimId != AccidentClaimId)
            {
                ClearClaimObjectHelper();
                var ClaimObjectHelper = new ClaimObjectHelper().Fetch(AccidentClaimId);
                SetClaimObjectHelper(ClaimObjectHelper);
                claimaccident = getClaimAccidentObject();
            }

            claimaccident.ViewMode = getPageViewMode(caller);
            claimaccident.IsComplete = getcompletestatus(AccidentClaimId);
            string OrgTypeStr = ClaimAccidentDetailsModel.fetchOrganisationType(LoggedInUserId, claimaccident.Organization);
            Session["OrganisationType"] = OrgTypeStr;
            claimaccident.OrgCatList = ClaimAccidentDetailsModel.fetchOrganizationList(LoggedInUserId, caller, Convert.ToString(Session["OrganisationType"]));
            claimaccident.TimeformatRadioList = sl;
            claimaccident.InterchangeList = ClaimAccidentDetailsModel.fetchInterchangeList();
            claimaccident.LossTypeList = LoadLossType();
            claimaccident.LossNatureList = LoadLossNature();
            claimaccident.generallookupvalue = sl1;
            claimaccident.OperatingHoursList = LoadLookUpValue("OperatingHours");
            claimaccident.FinalLiabilityList = LoadLookUpValue("InvestigationResult");
            //Impacted Code Commented due to TFS #21522
            //claimaccident.CollisionTypeList = LoadLookUpValue("CollisionType");
            //claimaccident.DistrictCodeList = LoadLookUpValue("DistrictCode");
            claimaccident.CollisionTypeList = ClaimAccidentDetailsModel.FetchCommonMasterData("CollisionType", AccidentClaimId);
            claimaccident.DistrictCodeList = ClaimAccidentDetailsModel.FetchCommonMasterData("DistrictCode", AccidentClaimId);
            claimaccident.genderlookupvalue = LoadLookUpValue("GENDER");
            claimaccident.BusCaptainlist = LoadBusCaptainDetails();
            var accDtls = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == AccidentClaimId select l).FirstOrDefault();
            if (accDtls != null)
            {
                claimaccident.CreatedBy = accDtls.CreatedBy;
                claimaccident.CreatedOn = Convert.ToDateTime(accDtls.CreatedDate);
                if (accDtls.ModifiedDate != null || accDtls.ModifiedDate.ToString() != "")
                {
                    claimaccident.ModifiedOn = Convert.ToDateTime(accDtls.ModifiedDate);
                    claimaccident.ModifiedBy = accDtls.ModifiedBy;
                }
                claimaccident.ReadOnly = accDtls.IsReadOnly == null ? false : (bool)accDtls.IsReadOnly;
                claimaccident.InitialLiability = accDtls.InitialLiability;
                claimaccident.CollisionType = accDtls.CollisionType;
            }

            var hasClaim = (from m in obj.CLM_Claims where m.AccidentClaimId == claimaccident.AccidentClaimId && m.ClaimantStatus != "3" select m).FirstOrDefault();
            if (hasClaim != null)
            {
                claimaccident.HClaimId = 1;
            }
            else
            {
                claimaccident.HClaimId = 0;
            }
            var appPayment = (from m in obj.CLM_PaymentSummary where m.AccidentClaimId == claimaccident.AccidentClaimId && m.ApprovePayment == "Y" select m).FirstOrDefault();
            if (appPayment != null)
            {
                claimaccident.HChkApprovedPayment = 1;
            }
            else
            {
                claimaccident.HChkApprovedPayment = 0;
            }
            ViewData["EnableTab"] = claimaccident.TPClaimentStatus == "Y" ? true : false;
            ViewData["EnableODTab"] = claimaccident.ODStatus == "Y" ? true : false;
            if (!AccidentClaimId.Equals(0) && caller != "New" && AccidentClaimId != 0)
            {
                UpdateClaimObjectHelper(claimaccident, "Accident");
            }
            else
            {
                ClearClaimObjectHelper();
            }
            if (claimaccident.TPAssignmentTranId != null)
            {
                TempData["DisplayTP"] = "DisplayTP";
            }
            if (claimaccident.ODAssignmentTranId != null)
            {
                TempData["DisplayOD"] = "DisplayOD";
            }
            claimaccident.ChckClaimComplete = claimaccident.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == claimaccident.AccidentClaimId select l.IsComplete).FirstOrDefault();
            claimaccident.ChkODStatus = claimaccident.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == claimaccident.AccidentClaimId && l.ClaimType == 1 select l.ClaimID).FirstOrDefault();
            claimaccident.ChkTPStatus = claimaccident.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == claimaccident.AccidentClaimId && (l.ClaimType == 2 || l.ClaimType == 3) select l.ClaimID).FirstOrDefault();
            return View("ClaimAccidentEditor", claimaccident);
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
                // DataSet dsTemp;
                if (isValid == true)
                {
                    //obj.AddParameter("@AccidentClaimId", accidentClaimId.ToString());
                    // obj.AddParameter("@OrganizationType", OrganizationType.ToString());
                    //obj.AddParameter("@ClaimPrefix", ClaimPrefix);
                    //dsTemp = obj.ExecuteDataSet("Proc_SetClaimsNo", CommandType.StoredProcedure);
                    // string perclaimno = dsTemp.Tables[0].Rows[0]["ClaimNo"].ToString();
                    int IsCompleteStatus = 2;
                    obj.ClearParameteres();
                    ViewData["SuccessMsg"] = "Claim has been completed.";
                    var acclist = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == accidentClaimId select l).FirstOrDefault();
                    acclist.IsComplete = 2;
                    acclist.ModifiedDate = DateTime.Now;
                    acclist.ModifiedBy = LoggedInUserName;
                    obj.SaveChanges();
                    var accidentdetails = getClaimAccidentObject();
                    // accidentdetails.ClaimNo = perclaimno;
                    accidentdetails.ViewMode = "Read";
                    accidentdetails.IsComplete = IsCompleteStatus;
                    accidentdetails.ReadOnly = true;
                    getClaimAccidentObject().ReadOnly = true;
                    UpdateClaimObjectHelper(accidentdetails, "Accident");
                    var claimdetails = getClaimEntryInfoObject();
                    string claimStatus = "2";
                    claimdetails.ClaimStatus = claimStatus;
                    claimdetails.ViewMode = "Read";
                    claimdetails.ReadOnly = true;
                    UpdateClaimObjectHelper(claimdetails, "Claim");
                    var claimObjectHelper = getClaimObjectHelper();

                    claimObjectHelper.ReadOnly = true;
                    claimObjectHelper.ViewMode = "Read";
                    // claimObjectHelper.ClaimNo = perclaimno;
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

        public JsonResult CancelClaim(int accidentClaimId, string LoggedInUserName)
        {
            var result = ClaimAccidentDetailsModel.ProcessCancelClaim(accidentClaimId, LoggedInUserName);
            return Json(result);
        }

        [ActionName("ClaimAccidentEditor")]
        [AcceptVerbs(HttpVerbs.Post)]
        [AcceptParameter(Name = "Save")]
        [HttpPost]
        public ActionResult ClaimAccidentEditor(ClaimAccidentDetailsModel model)
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
                //Impacted Code Commented due to TFS #21522
                //model.CollisionTypeList = LoadLookUpValue("CollisionType");
                //model.DistrictCodeList = LoadLookUpValue("DistrictCode");
                model.InterchangeList = ClaimAccidentDetailsModel.fetchInterchangeList();
                model.BusCaptainlist = LoadBusCaptainDetails();
                model.ChckClaimComplete = model.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == model.AccidentClaimId select l.IsComplete).FirstOrDefault();
                model.ChkODStatus = model.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == model.AccidentClaimId && l.ClaimType == 1 select l.ClaimID).FirstOrDefault();
                model.ChkTPStatus = model.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == model.AccidentClaimId && (l.ClaimType == 2 || l.ClaimType == 3) select l.ClaimID).FirstOrDefault();
                model.OrgCategory = model.hidOrgprop;
                model.CollisionTypeList = model.AccidentClaimId == null ? ClaimAccidentDetailsModel.FetchCommonMasterDataForNew("CollisionType", 0) : ClaimAccidentDetailsModel.FetchCommonMasterData("CollisionType",Convert.ToInt32(model.AccidentClaimId));
                model.DistrictCodeList = model.AccidentClaimId == null ? ClaimAccidentDetailsModel.FetchCommonMasterDataForNew("DistrictCode", 0) : ClaimAccidentDetailsModel.FetchCommonMasterData("DistrictCode", Convert.ToInt32(model.AccidentClaimId));
                if (!ModelState.IsValid)
                {
                    if (ModelState["InputDate"] != null)
                    {
                        ModelState["InputDate"].Errors.Clear();
                    }
                    int[] array = (from l in model.OrgCatList where l.Description.EndsWith("-Train") select l.OrgType).ToArray();
                    bool res = Array.Exists(array, element => element == model.Organization);
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    if (res && allErrors.Count() == 1 && allErrors.FirstOrDefault().ErrorMessage == "Bus Service Number is required.")
                    {
                        if (ModelState["BusServiceNo"] != null)
                        {
                            ModelState["BusServiceNo"].Errors.Clear();
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    TempData["DisplayTP"] = "DisplayTP";
                    TempData["DisplayOD"] = "DisplayOD";
                    model = model.Save();
                    UpdateClaimObjectHelper(model, "Accident");
                    ViewData["SuccessMsg"] = model.AccidentResult;
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
                return RedirectToAction("ClaimAccidentEditor", routes);

            }

        }
        public JsonResult GetOrganizationType(string OrganizationID)
        {
            var organizationID = Int32.Parse(OrganizationID);
            var result = (from vl in obj.MNT_OrgCountry where vl.Id == organizationID select vl.InsurerType).FirstOrDefault();
            return Json(result);
        }

        public JsonResult GetUserAccessOrganization(string AccidentID)
        {
            AccidentID = string.IsNullOrEmpty(AccidentID) ? "0" : AccidentID;
            var ID = Int32.Parse(AccidentID);
            var usrId = LoggedInUserId;
            var result = false;
            var claimOrg = (from ca in obj.ClaimAccidentDetails
                            join ua in obj.MNT_UserOrgAccess on ca.CreatedBy equals ua.UserId into ug
                            from ua in ug.DefaultIfEmpty()
                            join oc in obj.MNT_OrgCountry on ca.Organization equals oc.Id into g
                            from oc in g.DefaultIfEmpty()
                            join ml in obj.MNT_Lookups on oc.InsurerType equals ml.Lookupvalue into l
                            from ml in l.DefaultIfEmpty()
                            where ca.AccidentClaimId == ID
                            select new { ClmOrganization = (oc.OrganizationName + "-" + ml.Lookupdesc) }).Distinct().FirstOrDefault();
            var usrOrg = (from ca in obj.MNT_UserOrgAccess
                          join oc in obj.MNT_OrgCountry on new { code = ca.OrgCode, name = ca.OrgName } equals new { code = oc.InsurerType, name = oc.CountryOrgazinationCode } into g
                          from oc in g.DefaultIfEmpty()
                          join ml in obj.MNT_Lookups on oc.InsurerType equals ml.Lookupvalue into l
                          from ml in l.DefaultIfEmpty()
                          where ca.CreatedBy.Contains(usrId)
                          select new { usrOrganization = (oc.OrganizationName + "-" + ml.Lookupdesc) }).Distinct().ToList();
            if (usrOrg.Any())
            {
                foreach (var l in usrOrg)
                {
                    if (l.usrOrganization == claimOrg.ClmOrganization)
                    {
                        result = true;
                    }
                }
            }
            return Json(result);
        }

        public JsonResult Autocomplete(string term)
        {
            var items = (from vl in obj.MNT_VehicleListingMaster where vl.VehicleRegNo != null select vl.VehicleRegNo).ToList();
            var filteredItems = items.Where(item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutocompleteBusCaptaionCode(string term)
        {
            var items = (from vl in obj.MNT_BusCaptain where vl.BusCaptainCode != null select vl.BusCaptainCode).ToList();
            var filteredItems = items.Where(item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FillMakeAndModel(string VehNo, string Uid)
        {
            var result = ClaimAccidentDetailsModel.FetchFillMakeAndModel(VehNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillCollisionType(int OrgId)
        {
            var result = ClaimAccidentDetailsModel.FetchCommonMasterDataForNew("CollisionType", OrgId);
            return Json(result);
        }

        public JsonResult FillDistrict(int OrgId)
        {
            var result = ClaimAccidentDetailsModel.FetchCommonMasterDataForNew("DistrictCode", OrgId);
            return Json(result);
        }

        [HttpGet]
        public ActionResult SetMenuArrow(string screenid, string uid)
        {
            Session["screenID"] = screenid;
            Session.Remove("ScreenNameDash");
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SetScreenName(string ScreenNameDash, string screenid, string uid = "")
        {
            Session["ScreenNameDash"] = ScreenNameDash;
            Session["screenID"] = screenid;
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SetSystemAdminScreenName(string ScreenNameDash, string screenid, string uid)
        {
            Session["GroupName"] = ScreenNameDash;
            Session["SetSystemAdminScreenName"] = screenid;
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FillDriverInfo(int CaptainCode)
        {
            JsonResult result = new JsonResult();
            var results = (from cc in obj.MNT_BusCaptain where cc.TranId == CaptainCode select cc).FirstOrDefault();
            if (results != null)
            {
                var mobno = results.ContactNo == null ? "" : results.ContactNo.Length > 15 ? results.ContactNo.Substring(0, 15) : results.ContactNo;
                result.Data = new { BusCaptainName = results.BusCaptainName, NRICPassportNo = results.NRICPassportNo, ContactNo = mobno, DateJoined1 = results.DateJoined == null ? "" : results.DateJoined.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), DateResigned1 = results.DateResigned == null ? "" : results.DateResigned.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) };
            }
            else
            {
                result.Data = new { BusCaptainName = "", NRICPassportNo = "", ContactNo = "", DateResigned1 = "", DateJoined1 = "" };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FillDriverInfoCode(string CaptainCode)
        {
            JsonResult result = new JsonResult();
            var results = (from cc in obj.MNT_BusCaptain where cc.BusCaptainCode == CaptainCode select cc).FirstOrDefault();
            if (results != null)
            {
                var mobno = results.ContactNo == null ? "" : results.ContactNo.Length > 15 ? results.ContactNo.Substring(0, 15) : results.ContactNo;
                result.Data = new { BusCaptainName = results.BusCaptainName, NRICPassportNo = results.NRICPassportNo, ContactNo = mobno, DateJoined1 = results.DateJoined == null ? "" : results.DateJoined.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), DateResigned1 = results.DateResigned == null ? "" : results.DateResigned.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) };
            }
            else
            {
                result.Data = new { BusCaptainName = "", NRICPassportNo = "", ContactNo = "", DateResigned1 = "", DateJoined1 = "" };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ClaimOfficerDutyRoster(string id)
        {
            List<ClaimOfficerModel> list = new List<ClaimOfficerModel>();
            if (id == "TP")
            {
                list = ClaimOfficerModel.FetchClaimOfficer().Where(x => x.Type == "TP").ToList();
            }
            else
            {
                list = ClaimOfficerModel.FetchClaimOfficer().Where(x => x.Type == "OD").ToList();
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult ClaimOfficerDutyRoster(string Depart, string DeptName)
        {
            Depart = ((Request.Form["ddlDeptCode"] == null) ? "" : Request.Form["ddlDeptCode"]);
            DeptName = ((Request.Form["inputName"] == null) ? "" : Request.Form["inputName"]);
            List<ClaimOfficerModel> list = new List<ClaimOfficerModel>();
            list = GetUserResult(Depart, DeptName).ToList();
            return View(list);
        }

        public JsonResult FillDeptCodeList()
        {
            var returnData = LoadDepts();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillUserGroup()
        {
            var returnData = LoadGroups();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public List<ClaimOfficerModel> GetUserResult(string Depart, string DeptName)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimOfficerModel>();
            var ClaimOfficerDetails = (from x in _db.MNT_ClaimOfficerDetail where x.Department.Contains(Depart) && x.ClaimentOfficerName.Contains(DeptName) select x);
            if (ClaimOfficerDetails.Any())
            {
                foreach (var data in ClaimOfficerDetails)
                {
                    item.Add(new ClaimOfficerModel() { TranId = data.TranId, UserGroup = data.UserGroup, ClaimOfficerName = data.ClaimentOfficerName, ddlDeptCode = data.Department, LastAssignmentdate = data.LastAssignmentDate, Type = data.Type, ClaimNumber = data.ClaimNo });
                }
            }
            _db.Dispose();
            return item;
        }

        [HttpGet]
        public ActionResult AssignClaimOfficerDutyRoster(int TranId)
        {
            JsonResult returnData = new JsonResult();
            var claimOfficerdetails = (from x in obj.MNT_ClaimOfficerDetail where x.TranId == TranId select x).FirstOrDefault();
            if (claimOfficerdetails != null)
            {

                returnData.Data = new { Department = claimOfficerdetails.Department, ClaimentOfficerName = claimOfficerdetails.ClaimentOfficerName, Type = claimOfficerdetails.Type };
            }
            else
            {
                returnData.Data = new { Department = "", ClaimentOfficerName = "", Type = "" };
            }
            return Json(returnData, JsonRequestBehavior.AllowGet);
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
        public JsonResult CheckBusCaptainCode(string CaptainCode)
        {
            MCASEntities db = new MCASEntities();
            var correctvech = (from l in db.MNT_BusCaptain where l.BusCaptainCode == CaptainCode select l).ToList();
            var result = "F";
            if (correctvech.Any())
            {
                result = "T";
            }
            db.Dispose();
            return Json(result);
        }
        #endregion

        #region "ClaimEditor"
        public ActionResult ClaimEditor(int? ClaimID, dynamic AccidentClaimId)
        {
            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;
            var model = new ClaimInfoModel(Convert.ToInt32(AccidentId));
            try
            {
                var ClaimId = Convert.ToString(ClaimID) == "" ? "0" : Convert.ToString(ClaimID);
                model = model.FetchClaim(ClaimId, Convert.ToInt32(AccidentId), model);
                model.EZLinkCardNoList = new List<LookUpListItems>();
                model.EZLinkCardNoList = LoadLookUpValue("MISCCONDITION", false, false).OrderByDescending(x => x.Lookup_desc).ToList();
                model.EZLinkCardNoList.Insert(0, new LookUpListItems()
                {
                    Lookup_value = "",
                    Lookup_desc = "[Select...]"
                });
                model.EZLinkCardNoList.Insert(1, new LookUpListItems()
                {
                    Lookup_value = "A",
                    Lookup_desc = "NA"
                });
                var claimaccident = getClaimAccidentObject();
                claimaccident.IsComplete = getcompletestatus(Convert.ToInt32(AccidentId));
                if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
                string caller = MCASQueryString["claimMode"].ToString();
                TempData["IsCompleteprop"] = claimaccident.IsComplete;
                if (ClaimID != null && AccidentId != "0" && obj.Proc_GetCLM_ClaimListClaimId(ClaimId).FirstOrDefault() != null)
                {
                    TempData["hidedrop"] = "Hide";
                    TempData["headerval"] = model.ClaimType == 1 ? "Own Damage" : model.ClaimType == 2 ? "TPPD" : model.ClaimType == 3 ? "TPBI" : "";
                }
                model.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
                model.ViewMode = getPageViewMode(caller);
                ViewData["SuccessMsg"] = TempData["SuccessMsg"];
 //               TempData["DisplayDiv"] = "Display";
 
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
        [ValidateAntiForgeryToken]
        public ActionResult ClaimEditor(ClaimInfoModel model, dynamic AccidentClaimId, dynamic PolicyId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string policyId = Convert.ToString(PolicyId);
                string policyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : policyId.Contains("System") ? PolicyId[0] : policyId;

                model = model.FetchAllLists(model);
                model.EZLinkCardNoList = new List<LookUpListItems>();
                model.EZLinkCardNoList = LoadLookUpValue("MISCCONDITION", false, false).OrderByDescending(x => x.Lookup_desc).ToList();
                model.EZLinkCardNoList.Insert(0, new LookUpListItems()
                {
                    Lookup_value = "",
                    Lookup_desc = "[Select...]"
                });
                model.EZLinkCardNoList.Insert(1, new LookUpListItems()
                {
                    Lookup_value = "A",
                    Lookup_desc = "NA"
                });
                if (AccidentId == null || AccidentId == "" || AccidentId == "0")
                {
                    ViewData["SuccessMsg"] = "Can not save as Accident Tab is not save yet.";
                    TempData["DisplayDiv"] = "Display";
                }
                else
                {
                    model.PolicyId = Convert.ToInt32(policyID);
                    model.AccidentClaimId = Convert.ToInt32(AccidentId);
                    if (!ModelState.IsValid)
                    {
                        var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.Remove("ConfirmedAmount");
                        ModelState.Add("ConfirmedAmount", new ModelState());
                        ModelState.SetModelValue("ConfirmedAmount", new ValueProviderResult(model.ConfirmedAmount, DateTime.Now.ToString(Convert.ToString(Session["CurrentUICulture"])), null));

                    }
                    if (ModelState.IsValid)
                    {
                        ModelState.Clear();
                        model.Update();
                        ViewData["SuccessMsg"] = model.ResultMessage;
                        TempData["DisplayDiv"] = "Display";
                        TempData["SuccessMsg"] = model.ResultMessage;
                    }
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    object routes = new { AccidentClaimId = model.AccidentClaimId, policyId = model.PolicyId, claimMode = MCASQueryString["claimMode"], mode = MCASQueryString["mode"], ClaimID = model.ClaimID, ReadOnly = false, ClaimType = MCASQueryString["ClaimType"] };
                    string res = RouteEncryptDecrypt.getRouteString(routes);
                    res = RouteEncryptDecrypt.Encrypt(res);
                    routes = new { Q = res };
                    return RedirectToAction("ClaimEditor", routes);

                }
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }

           // return View(model);
        }
        public JsonResult ValidEmail(string EmailAdd1)
        {
            var Emailresults = false;
            if (EmailAdd1 != null && EmailAdd1 != "")
            {
                CommonUtilities.IsEMail isEmail = new CommonUtilities.IsEMail();
                bool result = isEmail.IsEmailValid(EmailAdd1);
                if (result) { }
                else
                {
                    Emailresults = true;

                }
            }
            return Json(Emailresults);
        }
        public JsonResult GetClaimNo(string ClaimNo, string AccidentId)
        {
            MCASEntities db = new MCASEntities();
            var correctvech = db.Proc_GetCLM_ClaimNo(ClaimNo, AccidentId).FirstOrDefault() == null ? 0 : Convert.ToInt32(db.Proc_GetCLM_ClaimNo(ClaimNo, AccidentId).ToList()[0].ClaimID);
            var res = ClaimNo == "1" ? "OD-" : ClaimNo == "2" ? "PD-" : ClaimNo == "3" ? "BI-" : "";
            var result = res + (correctvech + 1);
            db.Dispose();
            return Json(result);
        }
        public JsonResult GetClaimType(string ClaimTypeNo)
        {
            var result = (from l in obj.MNT_Lookups where l.Category == "ClaimType" && l.Lookupvalue == ClaimTypeNo select l.Description).FirstOrDefault();
            obj.Dispose();
            return Json(result);
        }

        public JsonResult GetApprovedPaymentonClaimant(int AccidentClaimId, int ClaimId)
        {
            MCASEntities db = new MCASEntities();
            var result = 0;
            var HChkApprovedPayment = (from m in db.CLM_PaymentSummary where m.AccidentClaimId == AccidentClaimId && m.ClaimID == ClaimId && m.ApprovePayment == "Y" select m).FirstOrDefault();
            if (HChkApprovedPayment != null)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
            return Json(result);
        }
        #endregion

        #region "ServiceProvider"
        public ActionResult ServiceProvider(int? ServiceProviderId, dynamic AccidentClaimId)
        {
            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;
            MCASEntities db = new MCASEntities();
            var model = new ServiceProviderModel(Convert.ToInt32(AccidentId));
            try
            {
                model.PartyTypeList = ServiceProviderModel.FetchLookUpList("PartyTypeList");
                model.StatusList = LoadLookUpValue("StatusList");
                model.usercountrylist = LoadCountry();
                model = model.ServiceProvider(Convert.ToString(ServiceProviderId), model);
                var claimaccident = getClaimAccidentObject();
                claimaccident.IsComplete = getcompletestatus(Convert.ToInt32(AccidentId));
                TempData["IsCompleteprop"] = claimaccident.IsComplete;
                if ((ServiceProviderId != null && db.Proc_GetCLM_ServicePID(Convert.ToString(ServiceProviderId)).Count() != 0) || model.ServiceProviderId != 0)
                {
                    TempData["DisplayDiv"] = "Display";
                }
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
        public ActionResult ServiceProvider(ServiceProviderModel model, dynamic AccidentClaimId, dynamic PolicyId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string policyId = Convert.ToString(PolicyId);
                string PolicyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : policyId.Contains("System") ? PolicyId[0] : policyId;

                model.ClaimTypeList = ServiceProviderModel.FetchLookUpList("ClaimType");
                model.ServiceProviderOptionList = ServiceProviderModel.ServiceProviderList();
                model.PartyTypeList = ServiceProviderModel.FetchLookUpList("PartyTypeList");
                model.ClaimantNameList = ServiceProviderModel.SelectOnlyList();
                model.CompanyNameList = ServiceProviderModel.SelectOnlyList();
                model.StatusList = LoadLookUpValue("StatusList");
                model.usercountrylist = LoadCountry();
                model.CreatedBy = LoggedInUserName;
                var acc = AccidentId == null ? 0 : AccidentId == "" ? 0 : Convert.ToInt32(AccidentId);
                model.PolicyId = Convert.ToInt32(PolicyID);
                model.AccidentId = Convert.ToInt32(AccidentId);
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model.Update();
                    ViewData["SuccessMsg"] = model.ResultMessage;
                    TempData["DisplayDiv"] = "Display";
                }
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }

            return View(model);
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

        public JsonResult OpenServiceProviderEditor(string ClaimTypeCat, string claimType)
        {
            var ClaimTypeList = ClaimInfoModel.FetchSelectedClaimType(ClaimTypeCat, claimType);
            return Json(ClaimTypeList);
        }
        public JsonResult claimcompletestatus(int accidentClaimId)
        {
            MCASEntities _db = new MCASEntities();
            int? result = 0;
            if (accidentClaimId != 0)
            {
                result = (from m in _db.ClaimAccidentDetails where m.AccidentClaimId == accidentClaimId select m.IsComplete).FirstOrDefault();
            }
            return Json(result);
        }

        public JsonResult chkClaimantStatus(int AccId, int ClaimantId)
        {
            MCASEntities _db = new MCASEntities();
            var result = "";
            if (AccId != 0)
            {
                result = (from m in _db.CLM_Claims where m.AccidentClaimId == AccId && m.ClaimID == ClaimantId select m.ClaimantStatus).FirstOrDefault();
            }
            return Json(result);
        }
        #endregion

        #region "Claim Notes"

        public ActionResult ClaimNotesList(int AccidentClaimId)
        {
            List<ClaimNotesModel> list = new List<ClaimNotesModel>();
            try
            {
                list = ClaimNotesModel.Fetch((AccidentClaimId));
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(list);
        }

        public ActionResult ClaimNotesEditor(int? ClaimId, int? NoteId, dynamic AccidentClaimId)
        {
            ClaimNotesModel objmodel = new ClaimNotesModel();
            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

            try
            {
                if (NoteId.HasValue)
                {
                    objmodel = objmodel.FetchNote(objmodel, Convert.ToInt32(AccidentId), ClaimId, NoteId);
                    TempData["Display"] = objmodel.ResultMessage;
                    return View(objmodel);
                }
                if ((Convert.ToString(Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(Session["OrganisationType"]).ToLower() == "pc"))
                {
                    objmodel.ClaimantNameList = objmodel.getTPVehicleNo(Convert.ToInt32(AccidentId));
                }
                else
                {
                    objmodel.ClaimantNameList = objmodel.getClaimantName(Convert.ToInt32(AccidentId));
                }
                if (ClaimId.HasValue)
                    objmodel.ClaimId = (int)ClaimId;
                if (NoteId.HasValue)
                    objmodel.NoteId = NoteId;
                objmodel.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(objmodel);
        }

        [HttpPost]
        public ActionResult ClaimNotesEditor(ClaimNotesModel model, HttpPostedFileBase ImageId, dynamic AccidentClaimId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;
                model.AccidentId = Convert.ToInt32(AccidentId);
                model.ClaimantNameList = model.getClaimantName(Int32.Parse(AccidentId));
                TempData["Display"] = "Display";
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model.Update();
                    if ((Convert.ToString(Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(Session["OrganisationType"]).ToLower() == "pc"))
                    {
                        model.ClaimantNameList = model.getTPVehicleNo(Convert.ToInt32(AccidentId));
                    }
                    ViewData["SuccessMsg"] = model.ResultMessage;
                }
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);

            }
            return View(model);
        }

        #endregion

        #region "TaskEditor"

        public ActionResult TaskEditor(int? Id)
        {
            TaskModel TaskEditor = new TaskModel();
            try
            {
                int id = MCASQueryString["AccidentClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["AccidentClaimId"]);
                if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
                CallerMenu = Convert.ToString(MCASQueryString["claimMode"]);
                SetCaller(id);
                TaskEditor.PromtDetails_List = TaskModel.FetchPromptDetails("TASKTYPE", id);
                TaskEditor = TaskModel.FetchTaskModel(TaskEditor, id, Id);
                TempData["Display"] = TaskEditor.Message;
                TaskEditor.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
            }
            catch (Exception ex)
            {
                TempData["result"] = "Unable to save changes.";
                ModelState.AddModelError("", "Unable to save changes.");
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(TaskEditor);
        }


        [HttpPost]
        public ActionResult TaskEditor(TaskModel model, dynamic AccidentClaimId, dynamic ClaimID)
        {
            try
            {

                int id = MCASQueryString["AccidentClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["AccidentClaimId"]);
                int cid = MCASQueryString["ClaimID"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimID"]);
                model.AccidentClaimId = Convert.ToInt32(model.AccidentClaimId ?? AccidentClaimId ?? id);
                model.ClaimID = Convert.ToInt32(model.ClaimID ?? ClaimID ?? cid);
                if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
                CallerMenu = Convert.ToString(MCASQueryString["claimMode"]);
                SetCaller(id);
                model.ReadOnly = id == 0 ? true : false;
                model.PromtDetails_List = TaskModel.FetchPromptDetails("TASKTYPE", id);
                model.ClaimOfficerList = model.FetchClaimOfficer("CO");
                if ((Convert.ToString(Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(Session["OrganisationType"]).ToLower() == "pc"))
                {
                    model.ClaimantNameList = model.getTPVehicleNo(id);
                }
                else
                {
                    model.ClaimantNameList = model.getClaimantName(id);
                }
                TempData["Display"] = "Display";
                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    model.Update();
                    TempData["result"] = model.ResultMessage;
                    model.TaskIndexList = TaskModel.Fetch(id);
                }
            }
            catch (Exception ex)
            {
                TempData["result"] = "Unable to save changes.";
                ModelState.AddModelError("", "Unable to save changes.");
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(model);
        }

        private void SetCaller(int id)
        {
            var claimaccident = getClaimAccidentObject();
            UpdateClaimObjectHelper(claimaccident, "Task", Convert.ToInt32(id));
            LoadMenuList();
        }

        public JsonResult GetTaskval(string id1)
        {
            MCASEntities db = new MCASEntities();
            var result = 1;
            var id = Convert.ToInt32(id1);
            var num = ((from t in db.CLM_ClaimTask where t.AccidentClaimId == id orderby t.Id descending select t.TaskNo).Take(1)).FirstOrDefault();
            if (num != null)
            {
                result = Convert.ToInt32(num) + 1;
            }
            db.Dispose();
            return Json(result);
        }
        #endregion

        #region "Claim Attachments"
        public ActionResult ClaimAttachmentsList(int? AccidentClaimId)
        {
            ClaimAttachmentsModel attachments = new ClaimAttachmentsModel();
            List<ClaimAttachmentsModel> list = new List<ClaimAttachmentsModel>();
            try
            {
                attachments.AttachTypeList = ClaimAttachmentsModel.fetchlookup("ATTACHMENT");
                TempData["folderoption"] = str2(attachments);
                TempData["delper"] = ClaimAttachmentsModel.delper();
                TempData["writeper"] = ClaimAttachmentsModel.writeper();
                TempData["readper"] = ClaimAttachmentsModel.Readper();
                list = ClaimAttachmentsModel.Fetch(AccidentClaimId);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ViewData["SuccessMsg"] = "Some error occurs";
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(list);
        }

        public ActionResult ClaimAttachmentsEditor(dynamic ClaimId, int? AttachId, dynamic AccidentClaimId)
        {
            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

            string claimId = Convert.ToString(ClaimId);
            string ClaimID = Convert.ToString(ClaimId) == "System.Object" ? "0" : claimId.Contains("System") ? ClaimId[0] : claimId;

            int accId = Convert.ToInt32(AccidentId);
            int clmId = Convert.ToInt32(ClaimID);
            var attachments = new ClaimAttachmentsModel();
            try
            {
                if (AttachId.HasValue)
                {
                    attachments.AttachId = AttachId;
                    ClaimAttachmentsModel objmodel = new ClaimAttachmentsModel();
                    objmodel = ClaimAttachmentsModel.fetchModel(objmodel, AttachId, accId, clmId);
                    TempData["Display"] = "Display";
                    TempData["folderoption"] = str2(objmodel);
                    EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
                    if (!objImPersonate.ImpersonateUserLogin(Convert.ToString(ConfigurationManager.AppSettings["IUserName"]), Convert.ToString(ConfigurationManager.AppSettings["IPassWd"]), Convert.ToString(ConfigurationManager.AppSettings["IDomain"])))
                    {
                        ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                    }
                    return View(objmodel);
                }
                if ((Convert.ToString(Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(Session["OrganisationType"]).ToLower() == "pc"))
                {
                    attachments.ClaimantNameList = ClaimAttachmentsModel.getTPVehicleNo(accId);
                }
                else
                {
                    attachments.ClaimantNameList = ClaimantName(accId);
                }
                
                attachments.AttachTypeList = ClaimAttachmentsModel.fetchlookup("ATTACHMENT");
                if (clmId != 0)
                {
                    attachments.AttachEntId = (int)clmId;
                    attachments.ClaimId = (int)clmId;
                    attachments.ClaimId = attachments.AttachEntId;
                }
                attachments.HchkhasClaim = (from m in obj.CLM_Claims where m.AccidentClaimId == accId select m).FirstOrDefault() != null ? 1 : 0;
                attachments.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
                TempData["folderoption"] = str2(attachments);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ViewData["SuccessMsg"] = "Some error occurs";
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return PartialView("ClaimAttachmentsEditor", attachments);
        }



        [HttpPost]
        public ActionResult ClaimAttachmentsEditor(ClaimAttachmentsModel model, HttpPostedFileBase AttachFileName, int AccidentClaimId)
        {
            try
            {
                model.AttachTypeList = ClaimAttachmentsModel.fetchlookup("ATTACHMENT");
                model.ClaimId = Convert.ToInt32(model.ClaimantName);
                model.AccidentId = AccidentClaimId;
                if ((Convert.ToString(Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(Session["OrganisationType"]).ToLower() == "pc"))
                {
                    model.ClaimantNameList = ClaimAttachmentsModel.getTPVehicleNo(AccidentClaimId);
                }
                else
                {
                    model.ClaimantNameList = ClaimantName(AccidentClaimId);
                }
                

                if (AttachFileName != null)
                {
                    if (ClaimAttachmentsModel.GetFolderPath(model.AttachId) == "1")
                    {
                        ViewData["SuccessMsg"] = "Error: File Can not be shown as File server path not configured. Please contact administrator.";
                        return View(model);
                    }
                    if (ClaimAttachmentsModel.GetFolderPath(model.AttachId) == "2")
                    {
                        ViewData["SuccessMsg"] = "Error: File Can not be shown as Upload Folder path not configured. Please contact administrator.";
                        return View(model);
                    }
                    EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
                    if (!objImPersonate.ImpersonateUserLogin(Convert.ToString(ConfigurationManager.AppSettings["IUserName"]), Convert.ToString(ConfigurationManager.AppSettings["IPassWd"]), Convert.ToString(ConfigurationManager.AppSettings["IDomain"])))
                    {
                        ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                        return View(model);
                    }
                    var FileName = AttachFileName.FileName.Split('\\').Last().ToString();
                    model.AttachFileName = FileName;
                    model.AttachFileType = FileName.Split('.').Last();
                }
                else
                {
                    model.AttachFileName = model.AttachId.HasValue ? Path.GetFileNameWithoutExtension(model.AttachFileName).EndsWith("_" + model.AttachId.Value) ? Path.GetFileNameWithoutExtension(model.AttachFileName).Substring(0, (Path.GetFileNameWithoutExtension(model.AttachFileName).Length - (model.AttachId.Value.ToString().Length + 1))) + Path.GetExtension(model.AttachFileName) : model.AttachFileName : Request.Form["AttachFileName"].Split('\\').Last().ToString();
                    model.IsUrlValid = model.AttachId.HasValue ? System.IO.File.Exists(ClaimAttachmentsModel.ChkIsUrlValid(model.AttachId.Value)) ? "Y" : "N" : "N";
                    model.AttachFileType = model.AttachFileName.Split('.').Last();
                }
                model.AttachFilePath = ClaimAttachmentsModel.GetFolderPath(null);
                model.FilePath = "Attachments";
                ModelState.Remove("AttachFileName");
                ModelState.Add("AttachFileName", new ModelState());
                ModelState.SetModelValue("AttachFileName", new ValueProviderResult(model.AttachFileName, DateTime.Now.ToString(), null));
                ModelState.Remove("AttachFileType");
                ModelState.Add("AttachFileType", new ModelState());
                ModelState.SetModelValue("AttachFileType", new ValueProviderResult(model.AttachFileType, DateTime.Now.ToString(), null));
                model.AttachDateTime = DateTime.Now;
                ModelState.Remove("AttachDateTime");
                ModelState.Add("AttachDateTime", new ModelState());
                ModelState.SetModelValue("AttachDateTime", new ValueProviderResult(model.AttachDateTime, DateTime.Now.ToString(), null));
                TempData["folderoption"] = str2(model);
                var claimsccidentdetails = getClaimAccidentObject();
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model.Update(AttachFileName);
                    ViewData["SuccessMsg"] = model.Message;
                }
                TempData["isPost"] = "Y";
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ViewData["SuccessMsg"] = "Some error occurs";
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteAttach(string AttachmentToDelete)
        {
            try
            {
                MCASEntities _db = new MCASEntities();
                string[] listIds = AttachmentToDelete.Split(',');
                List<MNT_AttachmentList> diaryList = new List<MNT_AttachmentList>();
                try
                {
                    foreach (var listid in listIds)
                    {
                        Int32 id = Convert.ToInt32(listid);
                        MNT_AttachmentList selectedDiary = (MNT_AttachmentList)_db.MNT_AttachmentList.Where(l => l.AttachId.Equals(id)).FirstOrDefault();
                        diaryList.Add(selectedDiary);
                    }

                    foreach (var diary in diaryList)
                    {
                        var res = _db.Proc_MNTAttachment_Delete(diary.AttachId);

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                _db.Dispose();
                return Json("Record deleted successfully!");
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return Json("Some error occurs");
            }

        }
        public FileResult FileDownload(int AttachId)
        {
            string FileName = "";
            string path = "";
            string str = "";
            string contentType = "text/csv";
            try
            {
                var results = (from at in obj.MNT_AttachmentList
                               where at.AttachId == AttachId
                               select new
                                   {
                                       FileName = at.AttachFileName,
                                       Uploadpath = at.FilePath,
                                       FileType = at.AttachFileType
                                   }).FirstOrDefault();
                FileName = results.FileName;
                path = results.Uploadpath;
                contentType = results.FileType;

                string fileServerpath = (from l in obj.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                if (String.IsNullOrEmpty(fileServerpath))
                {
                    ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                    return null;
                }
                string UploadFolder = ConfigurationManager.AppSettings["UploadFolder"];
                if (String.IsNullOrEmpty(UploadFolder))
                {
                    ViewData["SuccessMsg"] = "Error: File Can not be uploaded as Upload Folder not configured. Please contact administrator.";
                    return null;
                }

                path = fileServerpath.TrimEnd('\\') + "\\" + UploadFolder + "\\" + path.TrimStart('\\') + "\\" + (from l in obj.MNT_AttachmentList where l.AttachId == AttachId select l.AttachEntId).FirstOrDefault();

                EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
                if (!objImPersonate.ImpersonateUserLogin(Convert.ToString(ConfigurationManager.AppSettings["IUserName"]), Convert.ToString(ConfigurationManager.AppSettings["IPassWd"]), Convert.ToString(ConfigurationManager.AppSettings["IDomain"])))
                {
                    ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                    return null;
                }
                str = Path.Combine(path, FileName);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }
            var NewFileName = Path.GetFileNameWithoutExtension(FileName).EndsWith("_" + AttachId) ? Path.GetFileNameWithoutExtension(FileName).Substring(0, (Path.GetFileNameWithoutExtension(FileName).Length - (AttachId.ToString().Length + 1))) + Path.GetExtension(FileName) : FileName;
            return File(str, contentType, NewFileName);
        }
        private string str2(ClaimAttachmentsModel attachments)
        {
            var item = new List<permision>();
            string str = string.Empty;
            foreach (var data in attachments.AttachTypeList)
            {
                if (attachments.AttachTypeList.Any())
                {
                    if (data.Lookup_desc != "[Select...]")
                    {
                        str = str + "~" + Regex.Replace(data.Lookup_desc, @"\s+", "");
                    }
                }
            }
            return str;
        }
        public static List<ClaimantName> ClaimantName(int AccidentClaimId)
        {
            return ClaimAttachmentsModel.getClaimantName(AccidentClaimId);
        }
        #endregion

        #region "To Do List"
        public ActionResult DiaryTaskEditor(dynamic PolicyId, dynamic ClaimId, int? ListId)
        {

            var notes = new DiaryModel();
            MCASEntities _db = new MCASEntities();
            try
            {
                string Policyid = Convert.ToString(PolicyId);
                string PolicyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : Policyid.Contains("System") ? PolicyId[0] : Policyid;

                string Claimid = Convert.ToString(ClaimId);
                var ClaimID = Convert.ToString(ClaimId) == "System.Object" ? "0" : Claimid.Contains("System") ? ClaimId[0] : ClaimId;
              
                //string Listid = Convert.ToString(ListId);
                //string ListID = Convert.ToString(ListId) == "System.Object" ? "0" : Listid.Contains("System") ? ListId[0] : Listid;
                //int LstId=Convert.ToInt32(ListID);

                var Accidentid = MCASQueryString["AccidentClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["AccidentClaimId"]);
                if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
                CallerMenu = Convert.ToString(MCASQueryString["claimMode"]);
                SetCaller(Accidentid);
                notes.Values = ReAssignmentModel.Fetch();
                if (ListId.HasValue)
                {
                    var Diarylist = (from x in _db.TODODIARYLISTs where x.LISTID == ListId select x).FirstOrDefault();
                    DiaryModel objmodel = new DiaryModel();
                    objmodel = DiaryModel.FetchDiaryTaskEditor(objmodel, Diarylist, ClaimId, ListId, Accidentid);
                    objmodel.TypeList = DiaryModel.FetchAlertDescription("AlertDesc", Accidentid);//LoadDiaryListType();
                    objmodel.UserList = LoadUserList();
                    objmodel.CreatedBy = Diarylist.CreatedBy;
                    objmodel.CreatedOn = Convert.ToDateTime(Diarylist.CreatedDate);
                    if (Diarylist.ModifiedDate != null & Diarylist.ModifiedBy != null)
                    {
                        objmodel.ModifiedBy = Diarylist.ModifiedBy;
                        objmodel.ModifiedOn = Diarylist.ModifiedDate;
                    }
                    TempData["Display"] = "Display";
                    _db.Dispose();
                    return View(objmodel);
                }

                if (ClaimID != "0")
                    notes.ClaimId = (int)ClaimId;
                if (ListId.HasValue)
                    notes.ListId = ListId;
                notes.Escalationlist = ReAssignmentModel.FetchEscalationList();
                notes.AccidentId = Accidentid;
                notes.DairyList = DiaryModel.Fetch(Accidentid);
                //notes.TypeList = LoadLookUpValue("AlertDesc");//LoadDiaryListType();
                notes.TypeList = DiaryModel.FetchAlertDescription("AlertDesc", Accidentid);
                notes.UserList = LoadUserList();
                notes.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ViewData["SuccessMsg"] = "Some error occurs";
            }
            _db.Dispose();
            return PartialView("DiaryTaskEditor", notes);
        }

        [HttpPost]
        public ActionResult DiaryTaskEditor(DiaryModel model)
        {
            try
            {
                MCASEntities db = new MCASEntities();
                var Accidentid = MCASQueryString["AccidentClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["AccidentClaimId"]);
                if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
                CallerMenu = Convert.ToString(MCASQueryString["claimMode"]);
                SetCaller(Accidentid);
                //model.TypeList = LoadDiaryListType();
                model.TypeList = DiaryModel.FetchAlertDescription("AlertDesc", Accidentid);
                model.UserList = LoadUserList();
                model.Escalationlist = ReAssignmentModel.FetchEscalationList();
                model.Values = ReAssignmentModel.Fetch();
                if (model.hiddenprop == "1")
                {
                    ModelState.Remove("ReAssignDateFrom");
                    ModelState.Remove("ReAssignTo");
                    ModelState.Remove("ReminderBeforeCompletion");
                }
                else if (model.hiddenprop == "2")
                {
                    string[] arr = new string[] { "ToUserId", "Follow_Up_dateTime", "ListTypeID", "SubjectLine", "ReminderBeforeCompletion", "StartTime", "Escalation", "EscalationTo" };
                    for (var i = 0; i < arr.Length; i++)
                    {
                        ModelState.Remove(arr[i]);
                    }
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    if (model.hiddenprop == "1")
                    {
                        TempData["result"] = model.ListId.HasValue ? "Records Updated Successfully." : "Records Saved Successfully.";
                        var noteslist = model.Update();

                    }
                    else if (model.hiddenprop == "2")
                    {
                        var Dairyfrmuser = ReAssignmentModel.fetchuser(model.DairyId);
                        var result = ReAssignmentModel.Update(model.DairyId, "T", model.ReAssignTo, Dairyfrmuser, Convert.ToString(model.ReAssignDateFrom), Convert.ToString(DateTime.Now), model.Remark, Convert.ToString(model.ClaimId), Accidentid);
                        if (result == "T")
                        {
                            for (var i = 0; i < model.DairyId.Split(',').Length; i++)
                            {
                                var id = Convert.ToInt32(Convert.ToString(model.DairyId.Split(',')[i]));
                                var todo = new TODODIARYLIST();
                                var model1 = new DiaryModel();
                                string ModifiedBy = LoggedInUserId;
                                todo = db.TODODIARYLISTs.Where(t => t.LISTID == id).SingleOrDefault();
                                todo.ReassignedDiary = "Yes";
                                todo.ReassignedDiaryDate = DateTime.Now;
                                todo.ModifiedBy = ModifiedBy;
                                todo.ModifiedDate = DateTime.Now;
                                todo.AccidentId = Convert.ToInt32(Accidentid);
                                db.SaveChanges();
                            }
                        }
                        TempData["result"] = "Assigned Successfully.";

                    }
                    model.DairyList = DiaryModel.Fetch(Accidentid); ;
                }
                db.Dispose();
                return PartialView("DiaryTaskEditor", model);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ModelState.AddModelError("", "Unable to save changes.");
                TempData.Remove("result");
            }
            return View(model);
        }
        public JsonResult Dairiestobereassigned(string DairyId)
        {
            MCASEntities _db = new MCASEntities();
            string[] listIds = DairyId.Split(',');
            List<string> diaryList = new List<string>();
            List<string> diaryuserId = new List<string>();
            foreach (var listid in listIds)
            {
                Int64 id = Convert.ToInt64(listid);
                var selectedDiaries = (from m in _db.TODODIARYLISTs join l in _db.MNT_Lookups on m.LISTTYPEID equals l.Lookupvalue where m.LISTID == id select l.Lookupdesc).FirstOrDefault();

                diaryList.Add(selectedDiaries);
            }

            foreach (var listid in listIds)
            {
                Int64 id = Convert.ToInt64(listid);
                var selectedDiariesUserId = (from m in _db.TODODIARYLISTs where m.LISTID == id select m.TOUSERID).FirstOrDefault();
                diaryuserId.Add(Convert.ToString(selectedDiariesUserId));
            }
            string[] ResultList = diaryList.ToArray();
            _db.Dispose();
            return Json(ResultList);
        }

        #endregion

        #region "Reserve"
        public ActionResult ClaimReserveEditor(dynamic ReserveId, dynamic AccidentClaimId)
        {

            //string crid = Convert.ToString(ReserveId);
            //string rID = crid.Contains("System") ? "" : Convert.ToString((System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"].ToUpper().Equals("YES") ? ReserveId : ReserveId[0]));
            string crid = Convert.ToString(ReserveId);
            string rID = Convert.ToString(ReserveId) == "System.Object" ? "0" : crid.Contains("System") ? ReserveId[0] : crid;
            //string rID = Convert.ToString(ReserveId) == "System.Object" ? "0" : Convert.ToString((System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"].ToUpper().Equals("YES") ? ReserveId[0] : crid));

            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

            var Viewmode = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            var ClaimId = MCASQueryString["ClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimId"]);
            var model = new ClaimReserveModel(Convert.ToInt32(AccidentId));
            try
            {
                if ((rID != null && rID != "0") || (Viewmode == "Edit" || Viewmode == "Adjust"))
                {
                    TempData["DisplayDiv"] = "Display";
                }
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                model.ClaimantName = (from l in obj.CLM_Claims where l.ClaimID == ClaimId select l.ClaimantName).FirstOrDefault();
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
        public ActionResult ReverseChangeall(string resid, string AccidentClaimId)
        {
            var list = ClaimReserveModel.Fetchall(resid, AccidentClaimId);
            return View(list);
        }
        [HttpPost]
        public ActionResult ClaimReserveEditor(ClaimReserveModel model, dynamic AccidentClaimId, dynamic PolicyId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string policyId = Convert.ToString(PolicyId);
                string PolicyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : policyId.Contains("System") ? PolicyId[0] : policyId;


                model.RateperdayList = ClaimReserveModel.FetchRateperdayList();
                model.SelectListClamiantName = ClaimReserveModel.SelectOnlyList();
                model.HopitalNameIdList = ClaimReserveModel.FetchHopitalNameIdList();
                model.AssignToIdList = ClaimReserveModel.FetchAssignToIdListList();
                model.CreatedBy = LoggedInUserId;
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                if (AccidentId == null || AccidentId == "" || AccidentId == "0")
                {
                    ViewData["SuccessMsg"] = "Can not save as Accident Tab is not save yet.";
                    TempData["DisplayDiv"] = "Display";
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
                        TempData["DisplayDiv"] = "Display";
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
        public JsonResult GetClaimantNameReserveList(string Acc)
        {
            var item = ClaimReserveModel.GetClaimantNameReserveList(Acc);
            return Json(item);
        }
        public JsonResult GridButtonTextEditOrAssign(string ReserveId)
        {
            var result = ClaimReserveModel.GridButtonTextEditOrAssign(ReserveId);
            return Json(result);
        }
        public JsonResult GridButtonTextEditOrAssignAccordingToPaymentTab(string ReserveId)
        {
            var result = ClaimReserveModel.GridButtonTextEditOrAssignAccordingToPaymentTab(ReserveId);
            return Json(result);
        }
        public JsonResult GridButtonTextEditOrAssignAccordingToReserveFinilize(string ReserveId)
        {
            var result = ClaimReserveModel.GridButtonTextEditOrAssignAccordingToReserveFinilize(ReserveId);
            return Json(result);
        }
        public JsonResult CheckInitialAmountFinilize(string ReserveId)
        {
            var result = ClaimReserveModel.CheckInitialAmountFinilize(ReserveId);
            return Json(result);
        }
        public JsonResult UpdateFinilize(string ReserveId)
        {
            var result = ClaimReserveModel.UpdateFinilize(ReserveId);
            return Json(result);
        }
        public JsonResult GetClaimRecordNoForFinilize(string ReserveId)
        {
            var result = ClaimReserveModel.GetClaimRecordNoForFinilize(ReserveId);
            return Json(result);
        }
        public JsonResult GetClaimTypeNo(string ClaimType)
        {
            var result = ClaimReserveModel.GetClaimTypeNo(ClaimType);
            return Json(result);
        }
        public JsonResult GetRateperdayList(string UniqueId)
        {
            var item = new List<CommonUtilities.CommonType>();
            item = ClaimReserveModel.FetchRateperdayList();
            return Json(item);
        }
        public JsonResult Getdetailsfromreservedetails(string ClaimType, string ReserveId)
        {
            var result = ClaimReserveModel.Fetchdetailsfromreservedetails(ClaimType, ReserveId);
            return Json(result);
        }
        #endregion

        #region "Mandate"

        public ActionResult ClaimMandateReqEditor(dynamic MandateId, dynamic AccidentClaimId, string ViewOnly, string ClaimTypeCode)
        {
            MCASEntities db = new MCASEntities();
            var model = new ClaimMandateModel();
            string mandtId = Convert.ToString(MandateId);
            string mandateId = Convert.ToString(MandateId) == "System.Object" ? null : mandtId.Contains("System") ? MandateId[0] : mandtId;
            string accidentClaimId = Convert.ToString(AccidentClaimId);
            string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;
            List<LookUpListItems> list1 = LoadLookUpValue("MISCCONDITION", false, false).OrderByDescending(x => x.Lookup_value).ToList();
            SelectList sl1 = new SelectList(list1, "Lookup_value", "Lookup_desc");
            string Viewmode = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            var claimId = MCASQueryString["ClaimId"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimId"]);
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            CallerMenu = Convert.ToString(MCASQueryString["claimMode"]);
            SetCaller(Convert.ToInt32(AccidentId));
            int accidentClaimIdS = Convert.ToInt32(AccidentId);
            ViewBag.ClaimantStatus = 0;
            if(accidentClaimIdS!=0)
            {
                try
                {
                    var claim = db.CLM_Claims.Where(x => x.AccidentClaimId == accidentClaimIdS).SingleOrDefault();
                    ViewBag.ClaimantStatus = claim.ClaimantStatus;
                }
                catch { }
            }
            ViewData["ClaimTypeCode"] = Convert.ToString(ClaimTypeCode);
            try
            {
                model.userid = LoggedInUserId;
                model.groupcode = GetGroupcode(model.userid);
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                model.ClaimID = Convert.ToInt32(claimId);
                model.ClaimTypeList = LoadLookUpValue("ClaimType");
                model.investigationTypeList = LoadInvestigationResultValue("InvestigationResult");
                model.MandateList = ClaimMandateModel.FetchMandatelist(AccidentId);
                model.ApproveRecoList = LoadLookUpValue("GENERAL", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                model.EvidenceList = LoadLookUpValue("Evidence");
                model.InformSafetytoreviewfindingsList = LoadIInformSafetytoreviewfindingsValue("InformSafetytoreviewfindings");
                model.ClaimType = MCASQueryString["ClaimType"] == null ? 0 : Convert.ToInt32(MCASQueryString["ClaimType"]);
                model = model.GetRecoverableFromInsurerBIEZLinkCardNolist(model, "MISCCONDITION");
                model.MandateId = mandateId ==null ? 0 : Convert.ToInt32(mandateId);
                model = ClaimMandateModel.FetchMandateModel(model, Viewmode, mandateId);
                model = ClaimMandateModel.GetTotalAmountPaid(model);
                model.generallookupvalue = sl1;
                TempData["Groupuser"] = ClaimMandateModel.Groupuser(LoggedInUserId);
                TempData["RoleCode"] = RoleCode(LoggedInUserId);
                var IsFalOD = ClaimMandateModel.ChkUserFALOD(LoggedInUserId);
                TempData["UserFALOD"] = IsFalOD != "" || IsFalOD != null ? true : false;
                var IsFalPDBI = ClaimMandateModel.ChkUserFALPDBI(LoggedInUserId);
                TempData["UserFALPDBI"] = IsFalPDBI != "" || IsFalPDBI != null ? true : false;
                TempData["Claimtype"] = Convert.ToString(MCASQueryString["ClaimType"]) == null ? Convert.ToString(model.ClaimType) == null ? "" : Convert.ToString(model.ClaimType) : Convert.ToString(MCASQueryString["ClaimType"]);
                TempData["AssignedToSup"] = Convert.ToString(model.SupervisorAssignto);
                TempData["AssignedTo"] = Convert.ToString(model.AssignedTo);
                TempData["InvestigationRes"] = Convert.ToString(model.InvestigationResult);
                TempData["InformSafetytoreviewfindings"] = Convert.ToString(model.InformSafetytoreviewfindings);
                if (mandateId != null || Viewmode == "AddMandate")
                {
                    model = model.FetchMandate(mandateId, model);
                    TempData["DisplayDiv"] = "Display";
                    TempData["ViewOnly"] = "N";
                    ViewData["SelectedCategory"] = model.Evidence != null ? string.Join(",", model.Evidence.ToString().Split(',')) : "";
                }
                model.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
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
        public ActionResult ClaimMandateReqEditor(ClaimMandateModel model, dynamic AccidentClaimId, dynamic claimId, FormCollection form)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string clmId = Convert.ToString(claimId);
                string ClaimId = Convert.ToString(clmId) == "System.Object" ? "0" : clmId.Contains("System") ? claimId[0] : clmId;

                List<LookUpListItems> list1 = LoadLookUpValue("MISCCONDITION", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                SelectList sl1 = new SelectList(list1, "Lookup_value", "Lookup_desc");
                model.generallookupvalue = sl1;
                model.userid = LoggedInUserId;
                model.groupcode = GetGroupcode(model.userid);
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                model = ClaimMandateModel.FetchMandateModel(model, "", "");
                model.ClaimTypeList = LoadLookUpValue("ClaimType");
                model.investigationTypeList = LoadInvestigationResultValue("InvestigationResult");
                model = model.GetRecoverableFromInsurerBIEZLinkCardNolist(model, "MISCCONDITION");
                model.ApproveRecoList = LoadLookUpValue("GENERAL", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                model.EvidenceList = LoadLookUpValue("Evidence");
                model.InformSafetytoreviewfindingsList = LoadIInformSafetytoreviewfindingsValue("InformSafetytoreviewfindings");
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
                TempData["DisplayDiv"] = "Display";
                ModelState.Clear();
                model.Save();
                model = model.FetchMandate(Convert.ToString(model.MandateId), model);
                ViewData["SuccessMsg"] = model.ResultMessage;
                model.MandateList = ClaimMandateModel.FetchMandatelist(AccidentId);
                model = ClaimMandateModel.GetTotalAmountPaid(model);
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

        protected List<LookUpListItems> LoadIInformSafetytoreviewfindingsValue(string category, bool addAll = true, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            list = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "[Select...]", Lookup_desc = "[Select...]" });
            }
            if (addNone)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "(None)" });
            }
            obj.Dispose();
            return list;
        }

        public ActionResult ClaimMandatePDReq()
        {
            var model = new ClaimMandateModel();
            string CreatedBy = LoggedInUserName;
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            TempData["Groupuser"] = gid;
            return View(model);
        }
        public ActionResult ClaimMandatePDAppEditor()
        {
            var model = new ClaimMandateModel();
            string CreatedBy = LoggedInUserName;
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            TempData["Groupuser"] = gid;
            return View(model);
        }
        public ActionResult ClaimMandatePDCounterOffer()
        {
            var model = new ClaimMandateModel();
            string CreatedBy = LoggedInUserName;
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            TempData["Groupuser"] = gid;
            return View(model);
        }
        public ActionResult _MandateListAll(string MandateRecordNo, string AccidentClaimId, string ClaimID, string ClaimType, string MandateId, string uid)
        {
            var list = ClaimMandateModel.MandateListAll(MandateRecordNo, AccidentClaimId, ClaimID, ClaimType, MandateId);
            return View(list);
        }
        public JsonResult GridButtonTextEditOrAssignOnMandate(string MandateId)
        {
            var result = ClaimMandateModel.GridButtonTextEditOrAssignOnMandate(MandateId);
            return Json(result);
        }
        public JsonResult GridButtonTextEditOrAssignAccToPaymentTab(string MandateId)
        {
            var result = ClaimMandateModel.GridButtonTextEditOrAssignAccToPaymentTab(MandateId);
            return Json(result);
        }
        public JsonResult GridButtonTextEditOrAssignAccToApproveMandate(string MandateId)
        {
            var result = ClaimMandateModel.GridButtonTextEditOrAssignAccToApproveMandate(MandateId);
            return Json(result);
        }
        public JsonResult CheckClaimantFinalizeReserve(string AccidentClaimId, string ClaimID)
        {
            var result = ClaimMandateModel.CheckClaimantFinalizeReserve(AccidentClaimId, ClaimID);
            return Json(result);
        }
        public JsonResult Getdetailsfrommandatedetails(string ClaimType, string MandateId, string Typeofmovement)
        {
            var result = ClaimMandateModel.Fetchdetailsfrommmandatedetails(ClaimType, MandateId, Typeofmovement);
            return Json(result);
        }
        #endregion

        #region "PaymentEditorNew"
        public ActionResult PaymentEditorNew(dynamic PolicyId)
        {
            string Policyid = Convert.ToString(PolicyId);
            string PolicyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : Policyid.Contains("System") ? PolicyId[0] : Policyid;

            string Viewmode = MCASQueryString["Viewmode"] == null ? "" : Convert.ToString(MCASQueryString["Viewmode"]);
            string AccidentClaimId = MCASQueryString["AccidentClaimId"] != null ? Convert.ToString(MCASQueryString["AccidentClaimId"]) : "0";
            string ClaimID = MCASQueryString["ClaimID"] != null && MCASQueryString["ClaimID"] != "" ? Convert.ToString(MCASQueryString["ClaimID"]) : "0";
            string ClaimType = MCASQueryString["ClaimType"] != null && MCASQueryString["ClaimType"] != "" ? Convert.ToString(MCASQueryString["ClaimType"]) : "0";
            string PaymentId = MCASQueryString["PaymentId"] != null && MCASQueryString["PaymentId"] != "" ? Convert.ToString(MCASQueryString["PaymentId"]) : "0";
            string MandateRecordNo = MCASQueryString["MandateRecordNo"] != null && MCASQueryString["MandateRecordNo"] != "" ? Convert.ToString(MCASQueryString["MandateRecordNo"]) : "";
            string ReserveId = (Viewmode == "ApprovePayment" || Viewmode == "AddPayment") ? ClaimInfoPayment.FetchReserveId(AccidentClaimId, ClaimID, ClaimType, MandateRecordNo) : MCASQueryString["ReserveId"] != null && MCASQueryString["ReserveId"] != "" ? Convert.ToString(MCASQueryString["ReserveId"]) : "0";
            string MandateId = (Viewmode == "ApprovePayment" || Viewmode == "AddPayment") ? ClaimInfoPayment.FetchMandateId(AccidentClaimId, ClaimID, ClaimType, MandateRecordNo) : MCASQueryString["MandateId"] != null && MCASQueryString["MandateId"] != "" ? Convert.ToString(MCASQueryString["MandateId"]) : "0";
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            CallerMenu = Convert.ToString(MCASQueryString["claimMode"]);
            SetCaller(Convert.ToInt32(AccidentClaimId));

            var model = new ClaimInfoPayment(Convert.ToInt32(AccidentClaimId));
            List<Status> list = new List<Status>();
            list.Add(new Status() { ID = "Y", Name = Common.Yes });
            list.Add(new Status() { ID = "N", Name = Common.No });
            SelectList slist = new SelectList(list, "ID", "Name");
            model.generallookupvalue = slist;
            model.AccidentClaimId = Convert.ToInt32(AccidentClaimId);
            model.ClaimID = Convert.ToInt32(ClaimID);
            model.MandateRecordNo = MandateRecordNo;
            model.MandateId = Convert.ToInt32(MandateId);
            model.InformSafetytoreviewfindingsList = LoadIInformSafetytoreviewfindingsValue("InformSafetytoreviewfindings");
            model = model.GetRecoverableFromInsurerBIEZLinkCardNolist(model, "MISCCONDITION");
            try
            {
                model.userid = LoggedInUserId;
                model.HGroupCode = GetGroupcode(model.userid);
                model.HRoleCode = RoleCode(model.userid);
                model.ApprovePaymentList = LoadLookUpValue("GENERAL", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                model.ReserveId = Convert.ToInt32(ReserveId);
                model.PaymentId = Convert.ToInt32(PaymentId);
                model = model.FetchPayment(model, Viewmode);
                model = model.GetTotalAmountPaid(model);
                Session["ScreenNameDash"] = (null == Session["ScreenNameDash"]) ? "211" : Convert.ToString(Session["ScreenNameDash"]);
                Session["screenID"] = (null == Session["screenID"]) ? "CLM_PAY" : Convert.ToString(Session["screenID"]);
                if (PaymentId != "0" || Viewmode == "AddPayment" || Viewmode == "Adjust")
                {
                    TempData["DisplayDiv"] = "Display";
                }
                model.ReadOnly = getClaimObjectHelper().ReadOnly == true ? true : false;
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Loading Payment Editor.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(AccidentClaimId) ? "0" : AccidentClaimId.ToString());
                addInfo.Add("policy_id", String.IsNullOrEmpty(PolicyId.ToString()) ? "0" : PolicyId.ToString());
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", PaymentId);
                addInfo.Add("entity_type", "Payment");
                PublishException(ex, addInfo, String.IsNullOrEmpty(PaymentId) ? 0 : int.Parse(PaymentId), "Payment");
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult PaymentEditorNew(ClaimInfoPayment model, dynamic AccidentClaimId, dynamic PolicyId)
        {
            try
            {
                string accidentClaimId = Convert.ToString(AccidentClaimId);
                string AccidentId = Convert.ToString(AccidentClaimId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? AccidentClaimId[0] : accidentClaimId;

                string policyId = Convert.ToString(PolicyId);
                string PolicyID = Convert.ToString(PolicyId) == "System.Object" ? "0" : accidentClaimId.Contains("System") ? PolicyId[0] : policyId;

                string MandateRecordNo = MCASQueryString["MandateRecordNo"] != null && MCASQueryString["MandateRecordNo"] != "" ? Convert.ToString(MCASQueryString["MandateRecordNo"]) : "";
                model.userid = LoggedInUserId;
                model.HGroupCode = GetGroupcode(model.userid);
                model.HRoleCode = RoleCode(model.userid);
                model.ApprovePaymentList = LoadLookUpValue("GENERAL", false, false).OrderByDescending(x => x.Lookup_value).ToList();
                model.ClaimType = model.FetchClaimType(model);
                List<Status> list = new List<Status>();
                list.Add(new Status() { ID = "Y", Name = Common.Yes });
                list.Add(new Status() { ID = "N", Name = Common.No });
                SelectList slist = new SelectList(list, "ID", "Name");
                model.generallookupvalue = slist;
                model.AccidentClaimId = Convert.ToInt32(AccidentId);
                model.MandateRecordNo = MandateRecordNo;
                model.InformSafetytoreviewfindingsList = LoadIInformSafetytoreviewfindingsValue("InformSafetytoreviewfindings");
                model = model.GetRecoverableFromInsurerBIEZLinkCardNolist(model, "MISCCONDITION");
                model.CreatedBy = LoggedInUserId;
                model.PolicyId = Convert.ToInt32(PolicyID);
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model.Save();
                    ViewData["SuccessMsg"] = model.ResultMessage;
                    TempData["DisplayDiv"] = "Display";
                }
                model = model.FetchPayment(model, "");
                model = model.GetTotalAmountPaid(model);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Loading Payment Editor.");
                addInfo.Add("accidentClaim_id", String.IsNullOrEmpty(AccidentClaimId) ? "0" : AccidentClaimId.ToString());
                addInfo.Add("policy_id", String.IsNullOrEmpty(PolicyId.ToString()) ? "0" : PolicyId.ToString());
                addInfo.Add("policy_version_id", "0");
                addInfo.Add("claim_id", "0");
                addInfo.Add("entity_id", Convert.ToString(model.PaymentId));
                addInfo.Add("entity_type", "Payment");
                PublishException(ex, addInfo, String.IsNullOrEmpty(Convert.ToString(model.PaymentId)) ? 0 : model.PaymentId, "Payment");
                return View(model);
            }

        }
        public JsonResult GetClaimantName(string ClaimId)
        {
            int ClaimId1 = Convert.ToInt32(ClaimId);
            var result = (from l in obj.CLM_Claims where l.ClaimID == ClaimId1 select l.ClaimantName).FirstOrDefault();
            obj.Dispose();
            return Json(result);
        }

        public JsonResult GetFALforSP(int AssignedtoSP, int ClaimType)
        {
            decimal? result = 0.00m;
            if (ClaimType == 1)
            {
                result = (from usr in obj.MNT_Users
                          join fal in obj.MNT_FAL on usr.FAL_OD equals fal.FALId
                          where usr.SNo == AssignedtoSP
                          select fal.Amount).FirstOrDefault() == null ? 0.00m : (from usr in obj.MNT_Users
                                                                                 join fal in obj.MNT_FAL on usr.FAL_OD equals fal.FALId
                                                                                 where usr.SNo == AssignedtoSP
                                                                                 select fal.Amount).FirstOrDefault();
            }
            else
            {
                result = (from fal in obj.MNT_FAL
                          join usr in obj.MNT_Users on fal.FALId equals usr.FAL_PDBI
                          where usr.SNo == AssignedtoSP
                          select fal.Amount).FirstOrDefault() == null ? 0.00m : (from fal in obj.MNT_FAL
                                                                                 join usr in obj.MNT_Users on fal.FALId equals usr.FAL_PDBI
                                                                                 where usr.SNo == AssignedtoSP
                                                                                 select fal.Amount).FirstOrDefault();
            }
            obj.Dispose();
            return Json(result);
        }
        public ActionResult PaymentChangeall(string resid, string AccidentClaimId)
        {
            var list = ClaimInfoPayment.Fetchall(resid, AccidentClaimId);
            return View(list);
        }
        public JsonResult GetPayeeDetailFrmServiceprovider(string Payee, string UniqueId)
        {
            var result = ClaimInfoPayment.GetPayeeDetailFrmServiceprovider(Payee);
            return Json(result);
        }

        #endregion

        #region "TransactionEditor"
        public ActionResult TransactionEditor(int AccidentClaimId)
        {
            MCASEntities obj = new MCASEntities();
            List<TransactionModel> list = new List<TransactionModel>();
            list = TransactionModel.Fetchall(AccidentClaimId.ToString());
            return View(list);
        }
        public JsonResult GetXMLDiff(string TranAuditId)
        {
            int id = Convert.ToInt32(TranAuditId);
            MCASEntities obj = new MCASEntities();
            var result = obj.Proc_GetXMLDiff(id);
            return Json(result);
        }
        public JsonResult GetTransactionHistoryDetails(string TranAuditId)
        {
            var result = base.GetTransactionAuditHistoryDetails(Convert.ToInt32(TranAuditId));
            return Json(result);
        }


        #endregion

        #region "create/Update ClaimObjectHelper"
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
                if (((ClaimEntryInfoModel)model).ClaimID != null)
                {
                    claimObjectHelper.ClaimId = (int)((ClaimEntryInfoModel)model).ClaimID;
                }
                if (((ClaimEntryInfoModel)model).AccidentClaimId != null)
                {
                    claimObjectHelper.AccidentClaimId = (int)((ClaimEntryInfoModel)model).AccidentClaimId;
                }
                claimObjectHelper.ClaimDetail = ((ClaimEntryInfoModel)model);
                claimObjectHelper.ViewMode = ((ClaimEntryInfoModel)model).ViewMode;
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
        #endregion

        #region "Claim Registration"
        [HttpPost]
        public JsonResult GetClaims(SearchCriteria criteria, string searchvalue = "", string orderBycolumn = "AccidentDate", bool direction = true)
        {
            string caller = criteria.claimMode;
            List<InsuranceModel> list = new List<InsuranceModel>();
            List<InsuranceModel> paginglist = new List<InsuranceModel>();
            List<InsuranceModel> SearchList = new List<InsuranceModel>();
            if (criteria.claimStatus == null)
                criteria.claimStatus = CallerMenu == "Incomplete" ? "1" : CallerMenu == "Adjustment" ? "2" : CallerMenu == "RegEnquiry" ? "0" : null;
            if (criteria.ClaimNo != null || criteria.IPNo != null || criteria.vehicleNo != null || criteria.LossDate != null || criteria.ClaimantName != null || criteria.VehicleRegnNo != null)
            {

                list = GetSearchResult(criteria.ClaimNo, criteria.IPNo, criteria.LossDate, criteria.vehicleNo, criteria.ClaimantName, criteria.VehicleRegnNo, criteria.claimStatus).ToList();
            }
            else
            {
                switch (CallerMenu)
                {
                    case ("New"):
                        { list = InsuranceModel.Fetch(); break; }
                    case ("Incomplete"):
                        { list = InsuranceModel.FetchClaims("1"); break; }
                    case ("Adjustment"):
                        { list = InsuranceModel.FetchAdjClaims("2"); break; }
                    case ("RegEnquiry"):
                        { list = InsuranceModel.FetchClaimsEnq(); break; }
                    default:
                        { list = InsuranceModel.FetchClaims("0"); break; }
                }
            }

            if (!string.IsNullOrEmpty(searchvalue))
            {
                var serval = searchvalue.ToUpper().Trim();
                list = list.Select(x => new InsurenaceModelSearch()
                {
                    AccidentId = x.AccidentId ?? 0,
                    PolicyId = x.PolicyId ?? 0,
                    ClaimNo = x.ClaimNo == null ? "" : x.ClaimNo,
                    IPNo = x.IPNo == null ? "" : x.IPNo,
                    VehicleNo = x.VehicleNo == null ? "" : x.VehicleNo,
                    AccidentDatestr = x.AccidentDatestr == null ? "" : x.AccidentDatestr,
                    TPSurname = x.TPSurname == null ? "" : x.TPSurname,
                    VehicleRegnNo = x.VehicleRegnNo == null ? "" : x.VehicleRegnNo,
                    ClaimType = x.ClaimType == null ? "" : x.ClaimType,
                    ClaimDatestr = x.ClaimDatestr == null ? "" : x.ClaimDatestr,
                    claimantStatus = x.claimantStatus == null ? "" : x.claimantStatus,
                    ClaimOfficer = x.ClaimOfficer == null ? "" : x.ClaimOfficer,
                    IsReported = Convert.ToString(x.IsReported) == null ? "" : Convert.ToString(x.IsReported),
                    IsReadOnly = Convert.ToString(x.IsReadOnly) == null ? "" : Convert.ToString(x.IsReadOnly),
                    LinkedClaimNo = x.LinkedClaimNo == null ? "" : x.LinkedClaimNo,
                    InsurerType = x.InsurerType == null ? "" : x.InsurerType
                }).Where(m => m.GetType().GetProperties().Any(x => x.GetValue(m, null) != null && x.GetValue(m, null).ToString().Contains(searchvalue))).Select(x => new InsuranceModel()
                {
                    AccidentId = x.AccidentId,
                    PolicyId = x.PolicyId,
                    ClaimNo = x.ClaimNo,
                    IPNo = x.IPNo,
                    VehicleNo = x.VehicleNo,
                    AccidentDatestr = x.AccidentDatestr,
                    TPSurname = x.TPSurname,
                    VehicleRegnNo = x.VehicleRegnNo,
                    ClaimType = x.ClaimType,
                    ClaimDatestr = x.ClaimDatestr,
                    claimantStatus = x.claimantStatus,
                    ClaimOfficer = x.ClaimOfficer,
                    IsReadOnly = Convert.ToInt32(x.IsReadOnly),
                    IsReported = Convert.ToInt32(x.IsReported),
                    LinkedClaimNo = x.LinkedClaimNo,
                    InsurerType = x.InsurerType
                }).ToList();
            }

            list = direction == false ? list.OrderBy(orderBycolumn).ToList() : list.OrderBy(orderBycolumn + " DESC").ToList();
            int val = criteria.pageno == null ? 1 : Convert.ToInt32(criteria.pageno) + 1;
            int pno = criteria.pageno == null ? 0 : Convert.ToInt32(criteria.pageno);
            int startval = list.Count != 0 ? list.Count < 10 ? 1 : ((criteria.pageno == null ? 0 : Convert.ToInt32(criteria.pageno)) * 10) + 1 : 0;
            paginglist = list.Count > 0 ? list.Count <= 10 ? list.GetRange(0, list.Count).ToList() : list.GetRange((startval == 0 ? 0 : startval - 1), (Math.Min(((pno * 10) + 10), list.Count) - (pno * 10))).ToList() : list;
            if (paginglist.Count > 0)
            {
                paginglist[0].datalength = list.Count;
                paginglist[0].startlength = startval;
                paginglist[0].endlength = Math.Min(list.Count, (10 * val));
            }
            else
            {
                paginglist.Add(new InsuranceModel()
                {
                    datalength = 0,
                    startlength = 0,
                    endlength = 0
                });

            }
            return Json(paginglist, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ClaimRegistration()
        {
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            return View(new SearchCriteria() { claimMode = caller });
            /*
            switch (caller)
            {
                case ("New"):
                    { list = InsuranceModel.Fetch(); break; }
                case ("Incomplete"):
                    { list = InsuranceModel.FetchClaims("1"); break; }
                case ("Adjustment"):
                    { list = InsuranceModel.FetchClaims("2"); break; }
                case ("RegEnquiry"):
                    { list = InsuranceModel.FetchClaims("0"); break; }
                default:
                    { list = InsuranceModel.FetchClaims("0"); break; }
            }
            return View(list);*/
        }

        /*[HttpPost]
        public ActionResult ClaimRegistration(string PolicyNo, string InsuredName, DateTime? DateofLoss, string MainClassCode, string SubClassCode,string OrgCountries, string callermode)
        {
            PolicyNo = Request.Form["inputPolicyNo"]; 
            InsuredName = Request.Form["inputInsuredName"];
            DateofLoss = Convert.ToDateTime(DateTime.ParseExact(Request.Form["inputDateofLoss"],"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            String DateofLoss_new = String.Format("{0:dd/MM/yyyy}", DateofLoss);
            MainClassCode = ((Request.Form["ddlMainClassCode"] == null) ? "" : Request.Form["ddlMainClassCode"]);
            SubClassCode = ((Request.Form["ddlSubClassCode"] == null) ? "" : Request.Form["ddlSubClassCode"]);
            OrgCountries = ((Request.Form["ddlCountriesCode"] == null) ? "" : Request.Form["ddlCountriesCode"]);
            if (Request.QueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = Request.QueryString["claimMode"].ToString();
            ViewData["PolicyNo"] = PolicyNo;
            ViewData["InsuredName"] = InsuredName;
            ViewData["DateofLoss"] = DateofLoss_new;
            ViewData["MainClassCode"] = MainClassCode;
            ViewData["SubClassCode"] = SubClassCode;
            ViewData["OrgCountries"] = OrgCountries;
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = GetSearchResult(PolicyNo, InsuredName, DateofLoss, MainClassCode, SubClassCode, OrgCountries).ToList();
            return View(list);
        }
        */
        public List<InsuranceModel> GetSearchResult(string ClaimNo, string IPNo, string LossDate, string VehicleNo, string ClaimantName, string VehicleRegnNo, string status)
        {
            var item = new List<InsuranceModel>();
            //ClaimNo = ClaimNo == null ? "" : ClaimNo;
            //IPNo = IPNo == null ? "" : IPNo;
            //VehicleNo = VehicleNo == null ? "" : VehicleNo;
            //vehicleNo = vehicleNo == null ? "" : vehicleNo;
            //ClaimantName = ClaimantName == null ? "" : ClaimantName;
            //VehicleRegnNo = VehicleRegnNo == null ? "" : VehicleRegnNo;

            int clmstatus = status == null ? 0 : int.Parse(status);
            List<string> adjtype = new List<string>() { "ADJ", "SVY" };

            var searchResult = obj.Proc_GetSearchResult(ClaimNo, IPNo, LossDate, VehicleNo, ClaimantName, VehicleRegnNo, status).Distinct().ToList();
            var accDate = string.Empty;
            if (searchResult.Any())
            {
                foreach (var list in searchResult)
                {
                    accDate = list.AccidentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    item.Add(new InsuranceModel()
                    {
                        PolicyId = list.PolicyId,
                        AccidentId = list.AccidentClaimId,
                        PolicyNo = list.PolicyNo,
                        ProductId = list.ProductId,
                        CedantId = list.CedantId,
                        SubClassId = list.SubClassId,
                        PremiumAmount = 0,
                        Deductible = 0,
                        PolicyEffectiveTo = list.PolicyEffectiveTo,
                        PolicyEffectiveFrom = list.PolicyEffectiveFrom,
                        ProductCode = list.ProductCode,
                        ProductName = list.ProductDisplayName,
                        ClaimOfficer = list.ClaimOfficer,
                        DutyIO = list.DutyIO,
                        ClassDescription = list.ClassDescription,
                        ClaimNo = list.ClaimNo,
                        VehicleNo = list.VehicleNo,
                        CedantCode = list.CedantCode,
                        CedantName = list.CedantName,
                        DriverName = list.DriverName,
                        TPSurname = list.TPSurname,
                        ClaimStatus = list.ClaimStatus,
                        IPNo = list.IPNo,
                        ClaimDate = list.ClaimDate,
                        VehicleRegnNo = list.VehicleRegnNo,
                        AccidentDatestr = accDate,
                        //ClaimType = (list.ClaimType == null ? "" : list.ClaimType == 1 ? "OD" : list.ClaimType == 2 ? "PD" : "BI"),
                        ClaimType = (list.ClaimType == null ? "" : list.ClaimType == 1 ? "OD" : list.ClaimType == 2 ? "PD" :list.ClaimType == 3 ? "BI" : "RC"),
                        claimantStatus = (list.ClaimantStatus == null ? "" : list.ClaimantStatus == "1" ? "Pending " : list.ClaimantStatus == "2" ? "Finalized " : list.ClaimantStatus == "3" ? "Cancelled " : "Reopened"),
                        IsReported = list.IsReported ? 1 : 0,
                        IsReadOnly = list.IsReadOnly ? 1 : 0,
                        LinkedClaimNo = list.LinkedClaimNo,
                        ClaimDatestr = list.ClaimDate == null ? "" : list.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        AccidentDate = list.AccidentDate,
                        InsurerType = list.InsurerType
                    });
                }
            }
            return item;
        }


        public JsonResult FillProducts()
        {
            var returnData = LoadProducts();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillOrganization()
        {
            var returnData = LoadOrganization();
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

        public JsonResult ExpensesType(string CurrencyCode)
        {

            var result = LoadExpenses(CurrencyCode);
            return Json(result);
        }

        public JsonResult GetExRate_SGD(string CurrencyCode)
        {

            var result = LoadExpenses(CurrencyCode);
            return Json(result);
        }

        [HttpGet]
        public ActionResult ClaimRegistrationEditor(int? policyId, int? ClaimId, int? AccidentClaimId)
        {
            List<ClaimStatus> list = new List<ClaimStatus>();
            list.Add(new ClaimStatus() { ID = 1, Name = "I" });
            list.Add(new ClaimStatus() { ID = 2, Name = "C" });
            list.Add(new ClaimStatus() { ID = 3, Name = "F" });
            SelectList sl = new SelectList(list, "ID", "Name");
            if (Request.QueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = Request.QueryString["claimMode"].ToString();

            //var claimaccidentdetails = getClaimAccidentObject();
            var claimdetails = getClaimEntryInfoObject();

            if (AccidentClaimId.HasValue && AccidentClaimId != 0)
            {
                claimdetails.AccidentClaimId = AccidentClaimId;
                if (!claimdetails.ClaimID.HasValue || claimdetails.ClaimID != ClaimId)
                {
                    //claimdetails.ClaimID = ClaimId;
                    claimdetails = claimdetails.Fetch();
                    UpdateClaimObjectHelper(claimdetails, "Claim");
                }
                //UpdateClaimObjectHelper(claimaccidentdetails, "Accident");
            }
            claimdetails.ViewMode = getPageViewMode(caller);
            //  claimdetails.ClaimStatus = "1";
            claimdetails.ClaimStatusRadioList = sl;
            claimdetails.ClaimStatus = "1";
            claimdetails.PolicyId = policyId;

            //else
            //    ClearClaimObjectHelper();
            claimdetails.clmtypelist = LoadLookUpValue("CLMTYPE").Where(x => x.Lookup_value == "OD").ToList();

            claimdetails.reservelist = LoadReserve(true);
            claimdetails.expenseslist = LoadExpenses(true);


            claimdetails.adjusterAppointedlist = LoadSeverityDdl("adj");
            claimdetails.lawyerAppointedlist = LoadLawyerAppointed("sol");
            claimdetails.surveyorAppointedlist = LoadSurveyorAppointed("svy");
            claimdetails.depotWorkshoplist = LoadDepotWorkshop();
            claimdetails.adjusterAppointedlist = LoadPayableTo(true);
            DateTime dt = DateTime.Now;
            claimdetails.CreatedDate = dt.ToShortDateString();
            var claimsccidentdetails = getClaimAccidentObject();
            if (claimsccidentdetails.TPClaimentStatus == "Y")
            {
                ViewData["EnableTab"] = true;
            }
            else
            {
                ViewData["EnableTab"] = false;
            }
            // Enable/Disable Own Damage Tab
            if (claimsccidentdetails.ODStatus == "Y")
            {
                ViewData["EnableODTab"] = true;
            }
            else
            {
                ViewData["EnableODTab"] = false;
            }
            ViewData["SuccessMessage"] = "";
            return PartialView("ClaimRegistrationEditor", claimdetails);
        }

        [HttpPost]
        public ActionResult ClaimRegistrationEditor(ClaimEntryInfoModel model, int? AccidentClaimId)
        {
            try
            {
                List<ClaimStatus> list = new List<ClaimStatus>();
                list.Add(new ClaimStatus() { ID = 1, Name = "I" });
                list.Add(new ClaimStatus() { ID = 2, Name = "C" });
                list.Add(new ClaimStatus() { ID = 3, Name = "F" });
                SelectList sl = new SelectList(list, "ID", "Name");
                model.clmtypelist = LoadLookUpValue("CLMTYPE");
                model.reservelist = LoadReserve();
                model.expenseslist = LoadExpenses();
                model.adjusterAppointedlist = LoadSeverityDdl("adj");
                model.lawyerAppointedlist = LoadLawyerAppointed("sol");
                model.surveyorAppointedlist = LoadSurveyorAppointed("svy");
                model.depotWorkshoplist = LoadDepotWorkshop();
                model.ClaimStatusRadioList = sl;
                var msg = "";
                if (AccidentClaimId.HasValue && AccidentClaimId != 0)
                {
                    if (ModelState.IsValid)
                    {
                        ModelState.Clear();
                        if (model.RecordDeletionDate != null)
                        {
                            if (model.RecordDeletionReason == null || model.RecordDeletionReason == "")
                            {
                                msg = "Record Cancellation Reason is required. ";
                                ViewData["SuccessMsg"] = msg;
                                return View(model);
                            }

                        }

                        model = model.Update();
                        UpdateClaimObjectHelper(model, "Claim");
                        int accidentClaimId = (int)model.AccidentClaimId;
                        int ClaimId = (int)model.ClaimID;
                        var message = "";

                        var claimsccidentdetails = getClaimAccidentObject();
                        if (!claimsccidentdetails.AccidentClaimId.HasValue || claimsccidentdetails.AccidentClaimId != accidentClaimId)
                        {
                            claimsccidentdetails = claimsccidentdetails.Fetch();
                        }
                        else
                        {
                            claimsccidentdetails = claimsccidentdetails.Fetch();
                        }
                        if (claimsccidentdetails.ClaimNo == null)
                        {
                            var ClaimNo = ClaimEntryInfoModel.GetClaimNo(accidentClaimId);
                            message = "Claim Number generated for this claim is :" + ClaimNo;
                            claimsccidentdetails.ClaimNo = ClaimNo;
                            UpdateClaimObjectHelper(claimsccidentdetails, "Accident");
                        }

                        if (claimsccidentdetails.TPClaimentStatus == "Y")
                        {
                            ViewData["EnableTab"] = true;
                        }
                        else
                        {
                            ViewData["EnableTab"] = false;
                        }
                        if (claimsccidentdetails.ODStatus == "Y")
                        {
                            ViewData["EnableODTab"] = true;
                        }
                        else
                        {
                            ViewData["EnableODTab"] = false;
                        }
                        ViewData["SuccessMsg"] = "Record Updated Successfully." + message;
                        ViewData["SuccessMessage"] = "";
                        return PartialView(model);
                    }
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        ModelState.Clear();
                        model = model.Update();
                        UpdateClaimObjectHelper(model, "Claim");
                        int accidentClaimId = (int)model.AccidentClaimId;
                        int ClaimId = (int)model.ClaimID;
                        var message = "";

                        var claimsccidentdetails = getClaimAccidentObject();
                        if (!claimsccidentdetails.AccidentClaimId.HasValue || claimsccidentdetails.AccidentClaimId != accidentClaimId)
                        {
                            claimsccidentdetails = claimsccidentdetails.Fetch();
                        }
                        else
                        {
                            claimsccidentdetails = claimsccidentdetails.Fetch();
                        }
                        if (claimsccidentdetails.ClaimNo == null)
                        {
                            var ClaimNo = ClaimEntryInfoModel.GetClaimNo(accidentClaimId);
                            message = "Claim Number generated for this claim is :" + ClaimNo;
                            claimsccidentdetails.ClaimNo = ClaimNo;
                            UpdateClaimObjectHelper(claimsccidentdetails, "Accident");
                        }

                        if (claimsccidentdetails.TPClaimentStatus == "Y")
                        {
                            ViewData["EnableTab"] = true;
                        }
                        else
                        {
                            ViewData["EnableTab"] = false;
                        }
                        // Enable/Disable Own Damage Tab
                        if (claimsccidentdetails.ODStatus == "Y")
                        {
                            ViewData["EnableODTab"] = true;
                        }
                        else
                        {
                            ViewData["EnableODTab"] = false;
                        }
                        ViewData["SuccessMsg"] = "Record Saved Successfully." + message;
                        ViewData["SuccessMessage"] = "";
                        return PartialView(model);
                    }
                }

                //     model.adjusterAppointedlist = LoadPayableTo(true);


                //    return PartialView(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }

        #endregion

        #region "Claim Payment Processing"
        [HttpPost]
        public JsonResult GetClaimPayment(SearchCriteria criteria)
        {
            string caller = criteria.claimMode;
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = InsuranceModel.Fetchall(criteria.ClaimNo, criteria.IPNo, Convert.ToDateTime(criteria.LossDate), criteria.vehicleNo, criteria.ClaimantName, criteria.VehicleRegnNo, criteria.claimStatus,criteria.currPage);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClaimPaymentProcessing()
        {
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;

            return View(new SearchCriteria() { claimMode = caller });
        }

        public static List<ClaimantType> fetchClaimantlist()
        {
            var item = new List<ClaimantType>();
            item.Add(new ClaimantType() { Id = "OD", Text = "Own Damage" });
            item.Add(new ClaimantType() { Id = "PD", Text = "Property Damage" });
            item.Add(new ClaimantType() { Id = "BI", Text = "Body Injury" });
            return item;
        }

        public JsonResult FillClaimType()
        {
            var returnData = fetchClaimantlist();
            returnData.Insert(0, new ClaimantType() { Id = "", Text = "[Select...]" });
            return Json(returnData, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region "Claim Enquiry"

        [HttpPost]
        public JsonResult GetEnquiryClaims(SearchCriteria criteria)
        {
            string caller = criteria.claimMode;
            List<InsuranceModel> list = new List<InsuranceModel>();

            if (criteria.ClaimNo != null || criteria.IPNo != null || criteria.LossDate != null || criteria.vehicleNo != null || criteria.ClaimantName != null || criteria.VehicleRegnNo != null || criteria.claimStatus != null)
            {

                list = GetEnquirySearchResult(criteria.ClaimNo, criteria.IPNo, criteria.LossDate, criteria.vehicleNo, criteria.ClaimantName, criteria.VehicleRegnNo, criteria.claimStatus).ToList();
            }
            else
            {
                list = InsuranceModel.FetchClaims("0");
            }
            TempData["claimList"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClaimEnquiry()
        {
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            return View(new SearchCriteria() { claimMode = caller });
        }

        public JsonResult FillClaimLegalCase()
        {
            var returnData = LoadPayableTo();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        private void addGridColumns(GridView gv, string strColumnNames, string strColumnHeader)
        {
            String[] colNames = strColumnNames.Split(',');
            String[] colHeads = strColumnHeader.Split(',');
            BoundField field;
            for (int ctr = 0; ctr < colNames.Length; ctr++)
            {
                field = new BoundField();
                field.DataField = colNames[ctr];
                field.HeaderText = colHeads[ctr] != null ? colHeads[ctr] : "";
                gv.Columns.Add(field);
            }

        }
        public ActionResult FileDownLoader()
        {
            StringWriter sw = new StringWriter();
            List<InsuranceModel> list = (List<InsuranceModel>)TempData["claimList"];
            string format = "Excel";
            format = Request.QueryString["Fileformat"] != null ? Request.QueryString["Fileformat"].ToString() : format;
            string fileName = "ClaimEnquiryList.xls";
            if (format.ToUpper().Equals("EXCEL"))
            {
                GridView gv = new GridView();
                gv.DataSource = list;
                gv.AutoGenerateColumns = false;
                string columnNames = "ClaimNo,IPNo,VehicleNo,AccidentDatestr,TPSurname,VehicleRegnNo,ClaimType,ClaimDatestr,claimantStatus,ClaimOfficer";
                string columnHeaders = "Claim No,IPNo,Bus No,Date of Accident,Claimant Name,Claimant's Vehicle No,Claim Type, Claim Date,Claimant Status, CO";
                addGridColumns(gv, columnNames, columnHeaders);
                gv.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                //StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            else if (format.ToUpper().Equals("CSV"))
            {
                fileName = "ClaimEnquiryList.csv";
                //First line for column names
                sw.WriteLine("\"PolicyNo\",\"Sub Class\",\"Claim No\"");

                foreach (InsuranceModel item in list)
                {
                    sw.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\"",
                                               item.PolicyNo,
                                               item.ClassDescription,
                                               item.ClaimNo));
                }
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.ContentType = "text/csv";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.Write(sw);
                Response.End();
            }
            TempData["claimList"] = list;
            return View(list);
        }

        public IQueryable<InsuranceModel> GetEnquirySearchResult(string ClaimNo, string IPNo, string LossDate, string vehicleNo, string ClaimantName, string VehicleRegnNo, string claimStatus)
        {
            var item = new List<InsuranceModel>();
            IQueryable<InsuranceModel> list;
            if (ClaimNo == null && IPNo == null && LossDate == null && vehicleNo == null && ClaimantName == "" && VehicleRegnNo == "" && claimStatus == "")
            {
                list = InsuranceModel.FetchEnquiryClaims(0);
                return list;
            }
            else
            {

                int clmstatus = claimStatus == null ? 0 : int.Parse(claimStatus);
                List<string> adjtype = new List<string>() { "ADJ", "SVY" };

                var searchResult = obj.Proc_GetEnqSearchResult(ClaimNo, IPNo, LossDate, vehicleNo, ClaimantName, VehicleRegnNo).Distinct().ToList();
                var accDate = string.Empty;
                if (searchResult.Any())
                {
                    foreach (var lists in searchResult)
                    {
                        accDate = lists.AccidentDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        item.Add(new InsuranceModel()
                        {
                            PolicyId = lists.PolicyId,
                            AccidentId = lists.AccidentClaimId,
                            PolicyNo = lists.PolicyNo,
                            ProductId = lists.ProductId,
                            CedantId = lists.CedantId,
                            SubClassId = lists.SubClassId,
                            PremiumAmount = 0,
                            Deductible = 0,
                            PolicyEffectiveTo = lists.PolicyEffectiveTo,
                            PolicyEffectiveFrom = lists.PolicyEffectiveFrom,
                            ProductCode = lists.ProductCode,
                            ProductName = lists.ProductDisplayName,
                            ClaimOfficer = lists.ClaimOfficer,
                            DutyIO = lists.DutyIO,
                            ClassDescription = lists.ClassDescription,
                            ClaimNo = lists.ClaimNo,
                            VehicleNo = lists.VehicleNo,
                            CedantCode = lists.CedantCode,
                            CedantName = lists.CedantName,
                            DriverName = lists.DriverName,
                            TPSurname = lists.TPSurname,
                            ClaimStatus = lists.ClaimStatus,
                            IPNo = lists.IPNo,
                            ClaimDatestr = lists.ClaimDate == null ? "" : lists.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                            ClaimType = (lists.ClaimType == null ? "" : lists.ClaimType == 1 ? "OD" : lists.ClaimType == 2 ? "PD" : "BI"),

                            VehicleRegnNo = lists.VehicleRegnNo,
                            AccidentDatestr = accDate,
                            claimantStatus = (lists.ClaimantStatus == null ? "" : lists.ClaimantStatus == "1" ? "Pending " : lists.ClaimantStatus == "2" ? "Finalized " : lists.ClaimantStatus == "3" ? "Cancelled " : "Reopened"),
                            IsReported = lists.IsReported ? 1 : 0,
                            IsReadOnly = lists.IsReadOnly ? 1 : 0,
                            LinkedClaimNo = lists.LinkedClaimNo,
                            ClaimId = lists.ClaimID,
                        });
                    }

                }
                return item.ToList().AsQueryable();
            }

        }

        #endregion

        #region "Job Schedule Enquiry"

        [HttpPost]
        public JsonResult GetJobenquirysearch(SearchCriteria criteria)
        {
            string caller = criteria.claimMode;
            List<SearchCriteria> list = new List<SearchCriteria>();
            list = GetJobenquirysearchResult(criteria.JobNo, criteria.ProcessRefNo, Convert.ToDateTime(criteria.ScheduleStartDate), Convert.ToDateTime(criteria.ScheduleToDate), criteria.JobTypedefault, criteria.JobStatusDefault).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private static List<SearchCriteria> GetJobenquirysearchResult(string JobNo, string ProcessRefNo, DateTime ScheduleStartDate, DateTime ScheduleToDate, string JobTypedefault, string JobStatusDefault)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO #TempTable Select JobId ,ScheduleId ,FileRefNo ,ScheduleStartDateTime ,JobStartDateTime,JobEndDateTime,Status from dbo.MNT_UploadFileSchedule where Is_Active ='Y'");

            if ((JobTypedefault != null && JobTypedefault != "Select") || (JobStatusDefault != null && JobStatusDefault != "Select") || (JobNo != null && JobNo != "") || (ProcessRefNo != null && ProcessRefNo != "") || (ScheduleStartDate != null) || (ScheduleToDate != null))
            {
                sb.Append(" and ");
            }
            if (JobNo != null && JobNo != "")
            {
                sb.Append("UPPER(ScheduleId) Like UPPER('" + JobNo + "%')");
                sb.Append(" and ");
            }

            if (ProcessRefNo != null && ProcessRefNo != "")
            {
                sb.Append("UPPER(FileRefNo) Like UPPER('" + ProcessRefNo + "%')");
                sb.Append(" and ");
            }


            if (ScheduleStartDate != DateTime.MinValue && ScheduleToDate != DateTime.MinValue)
            {
                string dd = Convert.ToDateTime(ScheduleStartDate).ToString("yyyy-MM-dd");
                string dd2 = Convert.ToDateTime(ScheduleToDate).ToString("yyyy-MM-dd");
                sb.Append("CAST(ScheduleStartDateTime AS DATE) >= '" + dd + "'  and CAST(ScheduleStartDateTime AS DATE) <= '" + dd2 + "'");
                sb.Append(" and ");
            }
            else if (ScheduleStartDate != DateTime.MinValue)
            {
                string dd = Convert.ToDateTime(ScheduleStartDate).ToString("yyyy-MM-dd");
                sb.Append("CAST(ScheduleStartDateTime AS DATE) >= '" + dd + "'");
                sb.Append(" and ");
            }
            else if (ScheduleToDate != DateTime.MinValue)
            {
                string dd = Convert.ToDateTime(ScheduleToDate).ToString("yyyy-MM-dd");
                sb.Append("CAST(ScheduleStartDateTime AS DATE) <= '" + dd + "'");
            }

            if (JobTypedefault != null && JobTypedefault != "Select")
            {
                if (JobTypedefault == "TAC")
                {
                    sb.Append("UPPER(FileRefNo) Like UPPER('TAC%')");
                }
                else
                {
                    sb.Append("UPPER(FileRefNo) Like UPPER('CLM%')");
                }
                sb.Append(" and ");
            }
            if (JobStatusDefault != null && JobStatusDefault != "Select")
            {
                sb.Append("UPPER(Status) Like UPPER('" + JobStatusDefault + "%')");
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
            var searchResult = obj.Proc_GetJobSearchListList(queryString).ToList();
            var item = new List<SearchCriteria>();
            if (searchResult.Any())
            {

                foreach (var data in searchResult)
                {
                    var jb = data.FileRefNo.Substring(0, 3) == "TAC" ? "TAC Upload" : "Claim Upload";
                    var dd0 = Convert.ToDateTime(data.ScheduleStartDateTime).ToString();
                    var dd = data.JobStartDateTime == null ? "" : Convert.ToDateTime(data.JobStartDateTime).ToString();
                    var dd2 = data.JobEndDateTime == null ? "" : Convert.ToDateTime(data.JobEndDateTime).ToString();
                    var snonew = Convert.ToInt32(data.JobId);
                    item.Add(new SearchCriteria() { SNo = snonew, JobNo = data.ScheduleId, JobTypedefault = jb, ProcessRefNo = data.FileRefNo, ScheduleStartDate = dd0, JobStartDate = dd, JobEndDate = dd2, JobStatusDefault = data.Status });
                }
            }
            obj.Dispose();
            return item;
        }


        public ActionResult JobScheduleEnquiry()
        {
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            return View(new SearchCriteria() { claimMode = caller });
        }
        public JsonResult FillJobType()
        {
            var returnData = LoadFillJobType();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillJobStatus()
        {
            var returnData = LoadFillJobStatus();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Reassignment"
        public JsonResult SaveReAssign(string DairyId, string TypeOfAssignment, string ReAssignTo, string DairyFromUser, string ReAssignDateFrom, string ReAssignDateTo, string Remark, string ClaimId, string AccidentId)
        {
            MCASEntities db = new MCASEntities();
            var result = ReAssignmentModel.Update(DairyId, TypeOfAssignment, ReAssignTo, DairyFromUser, ReAssignDateFrom, ReAssignDateTo, Remark, ClaimId, Convert.ToInt32(AccidentId));
            if (result == "T")
            {
                for (var i = 0; i < DairyId.Split(',').Length; i++)
                {
                    var id = Convert.ToInt32(Convert.ToString(DairyId.Split(',')[i]));
                    var todo = new TODODIARYLIST();
                    var model = new DiaryModel();
                    string ModifiedBy = LoggedInUserId;
                    todo = db.TODODIARYLISTs.Where(t => t.LISTID == id).SingleOrDefault();
                    todo.ReassignedDiary = "Yes";
                    todo.ReassignedDiaryDate = DateTime.Now;
                    todo.ModifiedBy = ModifiedBy;
                    todo.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            return Json(result);
        }

        public ActionResult ReAssignmentEditorP(string DairyId, string Accid, string ClaimId, string disableuserid, string random)
        {
            var ReAssignmentEditorP = new ReAssignmentModel();
            ReAssignmentEditorP.ReAssignTo = new int[] { 0 };
            ReAssignmentEditorP.Values = ReAssignmentModel.Fetch();
            TempData["disableuserid"] = disableuserid;
            MCASEntities _db = new MCASEntities();
            try
            {
                string[] listIds = DairyId.Split(',');
                List<string> diaryList = new List<string>();
                List<string> diaryuserId = new List<string>();
                foreach (var listid in listIds)
                {
                    Int64 id = Convert.ToInt64(listid);
                    var selectedDiaries = (from m in _db.TODODIARYLISTs join l in _db.MNT_Lookups on m.LISTTYPEID equals l.Lookupvalue where m.LISTID == id select l.Lookupdesc).FirstOrDefault();

                    diaryList.Add(selectedDiaries);
                }

                foreach (var listid in listIds)
                {
                    Int64 id = Convert.ToInt64(listid);
                    var selectedDiariesUserId = (from m in _db.TODODIARYLISTs where m.LISTID == id select m.TOUSERID).FirstOrDefault();
                    diaryuserId.Add(Convert.ToString(selectedDiariesUserId));
                }
                string[] ResultList = diaryList.ToArray();
                string ResultString = string.Join("\n", ResultList.ToArray());

                string[] ResultusrList = diaryuserId.ToArray();
                string ResultusrString = string.Join(",", ResultusrList.ToArray());

                ReAssignmentEditorP.Dairiestobereassigned = ResultString;
                ReAssignmentEditorP.DairiestobereassignedId = DairyId;
                ReAssignmentEditorP.DairyFromUser = ResultusrString;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return View(ReAssignmentEditorP);
        }

        [HttpPost]
        public ActionResult ReAssignmentEditorP(ReAssignmentModel model)
        {
            model.Values = ReAssignmentModel.Fetch();
            try
            {
                if (ModelState.IsValid)
                {
                    var str = string.Join(",", model.ReAssignTo.Select(x => x.ToString()).ToArray());
                    ModelState.Clear();
                    //var list = model.Update(str);
                    TempData["result"] = "Records Saved Successfully.";
                    return View("DiaryTaskEditor");
                }
                else
                {
                    ModelState.Clear();
                    return View("DiaryTaskEditor");
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View("DiaryTaskEditor");
        }


        #endregion

        #region "PB/BI Tab"

        public ActionResult ThirdPartyClaimantEditor()
        {
            return View();
        }

        #endregion

        //#region "CLMClaimRecovery"
        //public ActionResult CLMClaimRecoveryALL(string ClaimId)
        //{
        //    MCASEntities obj = new MCASEntities();
        //    var clm = new CLMClaimRecoveryModel();
        //    List<CLMClaimRecoveryModel> list = new List<CLMClaimRecoveryModel>();
        //    int id = Convert.ToInt32(ClaimId);
        //    list = CLMClaimRecoveryModel.Fetchall(id);
        //    obj.Dispose();
        //    return View(list);
        //}

        //public ActionResult CLMClaimRecovery(string ClaimId)
        //{
        //    var CLMClaimRecovery = new CLMClaimRecoveryModel();
        //    CLMClaimRecovery.CurrCode_List = LoadReserve();
        //    CLMClaimRecovery.Status_List = CLMClaimRecoveryModel.Status_List_Fetch();
        //    return View(CLMClaimRecovery);
        //}

        //public JsonResult CLMClaimRecoverySave(string UserName, string Address, string RecoveryReason, string CurrCode, string ClaimID, string ExchangeRate, string LocalCurrAmt, string Status, string PaymentDetails)
        //{
        //    MCASEntities obj = new MCASEntities();
        //    var model = new CLMClaimRecoveryModel();
        //    string CreatedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
        //    string ModifiedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
        //    var ExchangeRate1 = ExchangeRate == "" ? "0" : ExchangeRate;
        //    var result = obj.Proc_Clm_Claimrecovery(UserName, Address, RecoveryReason, CurrCode, Convert.ToInt32(ClaimID), Convert.ToDecimal(ExchangeRate1), Convert.ToDecimal(LocalCurrAmt), Convert.ToInt32(Status), PaymentDetails, CreatedBy, ModifiedBy);
        //    obj.Dispose();
        //    return Json(result);
        //}
        //#endregion

        #region "ReserveEditor"
        public ActionResult ReverseEditor()
        {
            var id = Request.QueryString["ClaimId"] == null ? 0 : Convert.ToInt32(Request.QueryString["ClaimId"]);
            var ReverseEditor = new ReverseEditorModel();
            ReverseEditor.Payment_Type_List = LoadReserve();
            var claimsccidentdetails = getClaimAccidentObject();
            if (claimsccidentdetails.TPClaimentStatus == "Y")
            {
                ViewData["EnableTab"] = true;
            }
            else
            {
                ViewData["EnableTab"] = false;
            }
            ReverseEditor.Claimant_List = ReverseEditorModel.Fetch(id, "True");
            ReverseEditor.ReadOnly = id == 0 ? true : getClaimObjectHelper().AccidentDetail.ReadOnly == true ? true : false;

            if (getClaimObjectHelper() != null && getClaimObjectHelper().ClaimDetail.ClaimID == id)
            {
                string claimStatus = getClaimObjectHelper().ClaimDetail.ClaimStatus;
                if (CallerMenu == "Adjustment" && claimStatus == "2")
                    ReverseEditor.ReadOnly = false;
                else
                    ReverseEditor.ReadOnly = true;
            }
            return View(ReverseEditor);
        }

        public JsonResult GetReserve(int ClaimantNo)
        {
            var result = GetReservea(ClaimantNo);
            return Json(result);
        }


        protected List<ReverseEditorModel> GetReservea(int ClaimantNo)
        {
            Decimal dec = 0.00m;
            MCASEntities db = new MCASEntities();
            List<ReverseEditorModel> list = new List<ReverseEditorModel>();
            list = (from x in db.CLM_ClaimReserve
                    where x.ClaimantID == ClaimantNo
                    select new ReverseEditorModel
                    {
                        PreResOrgCurrCode = x.FinalResOrgCurrCode == null ? "SGD" : x.FinalResOrgCurrCode,
                        PreReserveOrgAmt = x.FinalReserveOrgAmt == null ? dec : x.FinalReserveOrgAmt,
                        PreExRateOrgCurr = x.PreExRateOrgCurr == null ? dec : x.PreExRateOrgCurr,
                        FinalReserveLocalAmt = x.FinalReserveLocalAmt == null ? dec : x.FinalReserveLocalAmt
                    }).ToList();
            var id = (from l in db.CLM_ClaimReserve where l.ClaimantID == ClaimantNo select l).Count() != 0 ? (from l in db.CLM_ClaimReserve where l.ClaimantID == ClaimantNo select l).Count() - 1 : 0;
            if (id != 0)
            {
                for (int i = 0; i <= id - 1; i++)
                {
                    list.RemoveAt(0);
                }
            }
            db.Dispose();
            return list;
        }

        [HttpPost]
        public ActionResult ReverseEditor(ReverseEditorModel model)
        {
            var id = Request.QueryString["ClaimId"] == null ? 0 : Convert.ToInt32(Request.QueryString["ClaimId"]);
            int cno = (!model.ClaimantID.HasValue) ? 0 : Convert.ToInt32(model.ClaimantID);
            Decimal val1, val2;
            var query = from state in ModelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;
            var errors = query.ToArray();
            try
            {
                val1 = query.Count().ToString() == "0" ? 0.00m : Convert.ToDecimal(ReverseEditorModel.Between(errors[0x00000000].ToString(), "The value '", "' is not valid"));
            }
            catch (Exception)
            {
                val1 = 0.00m;
            }
            try
            {

                val2 = query.Count().ToString() == "0" ? 0.00m : Convert.ToDecimal(ReverseEditorModel.Between(errors[0x00000001].ToString(), "The value '", "' is not valid"));
            }
            catch (Exception)
            {
                val2 = 0.00m;
            }
            model.PreReserveOrgAmt = val1;
            ModelState.Remove("PreReserveOrgAmt");
            ModelState.Add("PreReserveOrgAmt", new ModelState());
            ModelState.SetModelValue("PreReserveOrgAmt", new ValueProviderResult(model.PreReserveOrgAmt, DateTime.Now.ToString(), null));
            model.PreReserveLocalAmt = val2;
            ModelState.Remove("PreReserveLocalAmt");
            ModelState.Add("PreReserveLocalAmt", new ModelState());
            ModelState.SetModelValue("PreReserveLocalAmt", new ValueProviderResult(model.PreReserveLocalAmt, DateTime.Now.ToString(), null));
            TempData["Display"] = "Display";
            model.Payment_Type_List = LoadReserve();
            model.Claimant_List = ReverseEditorModel.Fetch(id, "True");
            var claimsccidentdetails = getClaimAccidentObject();
            if (claimsccidentdetails.TPClaimentStatus == "Y")
            {
                ViewData["EnableTab"] = true;
            }
            else
            {
                ViewData["EnableTab"] = false;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var list = model.Update(id, cno);
                    TempData["result"] = "Records Saved Successfully.";
                    return View(model);
                }
                else
                {
                    ModelState.Clear();
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);

        }

        #region "Reverse_Change"
        public ActionResult ReverseChange()
        {
            var id = Request.QueryString["ClaimId"] == null ? 0 : Convert.ToInt32(Request.QueryString["ClaimId"]);
            List<ReverseInfo> list = new List<ReverseInfo>();
            list = ReverseInfo.Fetch(id);
            return View(list);
        }
        #endregion
        #endregion

        #region "Third Party Detail"
        public ActionResult ThirdPartyList(int? ClaimId)
        {
            List<ThirdPartyInfoModel> list = new List<ThirdPartyInfoModel>();
            list = ThirdPartyInfoModel.Fetchlist(ClaimId);
            return View(list);
        }

        public ActionResult ThirdPartyEditor(int? ClaimId, int? TPartyId)
        {
            var thirdParty = new ThirdPartyInfoModel();
            if (TPartyId.HasValue)
            {
                var tplist = (from tp in obj.CLM_ThirdParty where tp.TPartyId == TPartyId select tp).FirstOrDefault();
                ThirdPartyInfoModel objmodel = new ThirdPartyInfoModel();
                objmodel.TPartyId = tplist.TPartyId;
                objmodel.ClaimId = tplist.ClaimId;
                objmodel.OtherPartyType = tplist.OtherPartyType;
                objmodel.CompanyName = tplist.CompanyName;
                objmodel.TPVehicleNo = tplist.TPVehicleNo;
                objmodel.TPAdd1 = tplist.TPAdd1;
                objmodel.TPMobNo = tplist.TPMobNo;
                objmodel.TPEmailAdd = tplist.TPEmailAdd;
                objmodel.TPNRICNo = tplist.TPNRICNo;
                objmodel.VehicleRegnNo = tplist.VehicleRegnNo;
                objmodel.VehicleMake = tplist.VehicleMake;
                objmodel.VehicleModel = tplist.VehicleModel;
                objmodel.LossDamageDesc = tplist.LossDamageDesc;
                objmodel.TPAdjuster = tplist.TPAdjuster;
                objmodel.TPLawyer = tplist.TPLawyer;
                objmodel.TPWorkShop = tplist.TPWorkShop;
                objmodel.Remarks = tplist.Remarks;
                objmodel.AttachedFile = tplist.AttachedFile;
                objmodel.ReserveCurr = tplist.ReserveCurr;
                objmodel.ReserveAmt = tplist.ReserveAmt;
                objmodel.ReserveExRate = tplist.ReserveExRate;
                objmodel.ReserveAmount = tplist.ReserveAmount;
                objmodel.ExpensesCurr = tplist.ExpensesCurr;
                objmodel.ExpensesAmt = tplist.ExpensesAmt;
                objmodel.ExpensesExRate = tplist.ExpensesAmount;
                objmodel.ExpensesAmount = tplist.ExpensesAmount;
                objmodel.TotalReserve = tplist.TotalReserve;
                objmodel.ClaimAmtCurr = tplist.ClaimAmtCurr;
                objmodel.ClaimAmt = tplist.ClaimAmt;
                objmodel.ClaimAmtExRate = tplist.ClaimAmtExRate;
                objmodel.ClaimAmount = tplist.ClaimAmount;
                objmodel.PayableTo = tplist.PayableTo;
                objmodel.PaidToDate = tplist.PaidToDate;
                objmodel.BalanceLOG = tplist.BalanceLOG;
                objmodel.LOGAmount = tplist.LOGAmount;
                objmodel.LOURate = tplist.LOURate;
                objmodel.LOUDays = tplist.LOUDays;

                objmodel.ThirdPartyClmTypeList = LoadLookUpValue("CLMTYPE").Where(x => x.Lookup_value != "OD").ToList();
                objmodel.reservelist = LoadReserve();
                objmodel.expenseslist = LoadExpenses();
                objmodel.claimamtlist = LoadClaimAmt();
                objmodel.payabletolist = LoadLookUpValue("PTO");

                if (ClaimId.HasValue)
                    objmodel.ClaimId = ClaimId;
                if (TPartyId.HasValue)
                    objmodel.TPartyId = TPartyId;
                var claimsccidentdetail = getClaimAccidentObject();
                if (claimsccidentdetail.TPClaimentStatus == "Y")
                {
                    ViewData["EnableTab"] = true;
                }
                else
                {
                    ViewData["EnableTab"] = false;
                }
                // Enable/Disable Own Damage Tab
                if (claimsccidentdetail.ODStatus == "Y")
                {
                    ViewData["EnableODTab"] = true;
                }
                else
                {
                    ViewData["EnableODTab"] = false;
                }
                var reservecount = (from m in obj.CLM_ClaimReserve where m.ClaimantID == objmodel.TPartyId && m.ClaimID == objmodel.ClaimId select m).Count();
                if (reservecount > 1)
                {
                    ViewData["DisplayReserve"] = "readonly";
                }
                return View(objmodel);
            }
            thirdParty.ThirdPartyClmTypeList = LoadLookUpValue("CLMTYPE").Where(x => x.Lookup_value != "OD").ToList();
            thirdParty.reservelist = LoadReserve();
            thirdParty.expenseslist = LoadExpenses();
            thirdParty.claimamtlist = LoadClaimAmt();
            thirdParty.payabletolist = LoadLookUpValue("PTO");
            if (ClaimId.HasValue)
                thirdParty.ClaimId = ClaimId;
            if (TPartyId.HasValue)
                thirdParty.TPartyId = TPartyId;
            var claimsccidentdetails = getClaimAccidentObject();
            if (claimsccidentdetails.TPClaimentStatus == "Y")
            {
                ViewData["EnableTab"] = true;
            }
            else
            {
                ViewData["EnableTab"] = false;
            }
            // Enable/Disable Own Damage Tab
            if (claimsccidentdetails.ODStatus == "Y")
            {
                ViewData["EnableODTab"] = true;
            }
            else
            {
                ViewData["EnableODTab"] = false;
            }
            return PartialView("ThirdPartyEditor", thirdParty);
        }

        [HttpPost]
        public ActionResult ThirdPartyEditor(ThirdPartyInfoModel model)
        {
            try
            {
                model.ThirdPartyClmTypeList = LoadLookUpValue("CLMTYPE").Where(x => x.Lookup_value != "OD").ToList();
                model.reservelist = LoadReserve();
                model.expenseslist = LoadExpenses();
                model.claimamtlist = LoadClaimAmt();
                model.payabletolist = LoadLookUpValue("PTO");
                var claimsccidentdetails = getClaimAccidentObject();
                if (claimsccidentdetails.TPClaimentStatus == "Y")
                {
                    ViewData["EnableTab"] = true;
                }
                else
                {
                    ViewData["EnableTab"] = false;
                }
                // Enable/Disable Own Damage Tab
                if (claimsccidentdetails.ODStatus == "Y")
                {
                    ViewData["EnableODTab"] = true;
                }
                else
                {
                    ViewData["EnableODTab"] = false;
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var tplist = model.Update();
                    ViewData["SuccessMsg"] = "Records Saved Successfully";
                }
                return PartialView("ThirdPartyEditor", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }

        public ActionResult ReserveHistory()
        {
            return View();
        }
        #endregion

        #region "Claim Transactions"

        public ActionResult ClaimTransactionsList(int? ClaimId)
        {
            List<ClaimTransactionsModel> list = new List<ClaimTransactionsModel>();
            list = ClaimTransactionsModel.Fetch(ClaimId);
            return View(list);
        }

        public ActionResult ClaimTransactionsEditor(int? ClaimId, int? TransactionId)
        {
            var trans = new ClaimTransactionsModel();
            if (TransactionId.HasValue)
            {
                var translist = (from ct in obj.CLM_Transactions where ct.TransactionId == TransactionId select ct).FirstOrDefault();
                ClaimTransactionsModel objmodel = new ClaimTransactionsModel();
                objmodel.TransactionId = translist.TransactionId;
                objmodel.ClaimId = translist.ClaimId;
                objmodel.PolicyId = translist.PolicyId;
                objmodel.TransactionDate = translist.TransactionDate;
                objmodel.TransactionType = translist.TransactionType;
                objmodel.CreditorName = translist.CreditorName;
                objmodel.ExpenseCode = translist.ExpenseCode;
                objmodel.AmountPaid = translist.AmountPaid;
                objmodel.Authorizedby = translist.Authorizedby;
                objmodel.AuthorizedDate = translist.AuthorizedDate;
                objmodel.AuthorizedTime = translist.AuthorizedTime;
                objmodel.ProcessedDate = translist.ProcessedDate;

                if (ClaimId.HasValue)
                    objmodel.ClaimId = (int)ClaimId;
                if (TransactionId.HasValue)
                    objmodel.TransactionId = TransactionId;

                return View(objmodel);
            }

            if (ClaimId.HasValue)
                trans.ClaimId = (int)ClaimId;
            if (TransactionId.HasValue)
                trans.TransactionId = TransactionId;
            var claimsccidentdetails = getClaimAccidentObject();
            if (claimsccidentdetails.TPClaimentStatus == "Y")
            {
                ViewData["EnableTab"] = true;
            }
            else
            {
                ViewData["EnableTab"] = false;
            }
            // Enable/Disable Own Damage Tab
            if (claimsccidentdetails.ODStatus == "Y")
            {
                ViewData["EnableODTab"] = true;
            }
            else
            {
                ViewData["EnableODTab"] = false;
            }
            return PartialView("ClaimTransactionsEditor", trans);
        }


        [HttpPost]
        public ActionResult ClaimTransactionsEditor(ClaimTransactionsModel model)
        {
            try
            {
                var claimsccidentdetails = getClaimAccidentObject();
                if (claimsccidentdetails.TPClaimentStatus == "Y")
                {
                    ViewData["EnableTab"] = true;
                }
                else
                {
                    ViewData["EnableTab"] = false;
                }
                // Enable/Disable Own Damage Tab
                if (claimsccidentdetails.ODStatus == "Y")
                {
                    ViewData["EnableODTab"] = true;
                }
                else
                {
                    ViewData["EnableODTab"] = false;
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var translist = model.Update();

                    ViewData["SuccessMsg"] = "Records Saved Successfully";
                }
                return PartialView("ClaimTransactionsEditor", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }
        #endregion

        #region "Claim Finance"
        public ActionResult ClaimFinanceEditor()
        {
            var finance = new ClaimFinanceModel();
            return View("ClaimFinanceEditor", finance);
        }
        #endregion

        #region "PaymentEditort"
        public ActionResult PaymentEditor()
        {
            var id = Request.QueryString["ClaimId"] == null || Request.QueryString["ClaimId"] == "" ? 0 : Convert.ToInt32(Request.QueryString["ClaimId"]);
            var PaymentEditor = new PaymentEditortModel();
            PaymentEditor.Payment_Type_List = LoadReserve();
            var claimsccidentdetails = getClaimAccidentObject();
            if (claimsccidentdetails.TPClaimentStatus == "Y")
            {
                ViewData["EnableTab"] = true;
            }
            else
            {
                ViewData["EnableTab"] = false;
            }
            // Enable/Disable Own Damage Tab
            if (claimsccidentdetails.ODStatus == "Y")
            {
                ViewData["EnableODTab"] = true;
            }
            else
            {
                ViewData["EnableODTab"] = false;
            }
            PaymentEditor.Claimant_List = ReverseEditorModel.Fetch(id, "True");
            PaymentEditor.PayeeType_List = PaymentEditortModel.Fetch_PayeeType();
            PaymentEditor.PaymentType_List = PaymentEditortModel.Fetch_PaymentType();
            PaymentEditor.ReadOnly = id == 0 ? true : false;  // need to implement based on claim status
            if (getClaimObjectHelper() != null && getClaimObjectHelper().ClaimDetail.ClaimID == id)
            {
                string claimStatus = getClaimObjectHelper().ClaimDetail.ClaimStatus;
                if (CallerMenu == "Payment" && claimStatus == "2")
                    PaymentEditor.ReadOnly = false;
                else
                    PaymentEditor.ReadOnly = true;
            }
            return View(PaymentEditor);
        }

        [HttpPost]
        public ActionResult PaymentEditor(PaymentEditortModel model)
        {
            int id = Request.QueryString["ClaimId"] == null ? 0 : Convert.ToInt32(Request.QueryString["ClaimId"]);
            var query = from state in ModelState.Values
                        select state.Value;
            var val1 = Convert.ToDecimal(query.ToArray()[12].AttemptedValue);
            var val2 = DateTime.Parse(query.ToArray()[4].AttemptedValue, new System.Globalization.CultureInfo("en-GB"), System.Globalization.DateTimeStyles.AssumeLocal);
            model.PayableLocalAmt = val1;
            ModelState.Remove("PayableLocalAmt");
            ModelState.Add("PayableLocalAmt", new ModelState());
            ModelState.SetModelValue("PayableLocalAmt", new ValueProviderResult(model.PayableLocalAmt, DateTime.Now.ToString(), null));
            model.PaymentDueDate = val2;
            ModelState.Remove("PaymentDueDate");
            ModelState.Add("PaymentDueDate", new ModelState());
            ModelState.SetModelValue("PaymentDueDate", new ValueProviderResult(model.PaymentDueDate, DateTime.Now.ToString(), null));
            int cno = Convert.ToInt32(model.Claimant);
            TempData["Display"] = "Display";
            model.Payment_Type_List = LoadReserve();
            model.Claimant_List = ReverseEditorModel.Fetch(id, "True");
            model.PayeeType_List = PaymentEditortModel.Fetch_PayeeType();
            model.PaymentType_List = PaymentEditortModel.Fetch_PaymentType();
            try
            {
                if (ModelState.IsValid)
                {
                    var cid = Convert.ToInt32(model.Claimant);
                    MCASEntities obj = new MCASEntities();
                    var check = (from l in obj.CLM_ClaimReserve where l.ClaimID == id && l.ClaimantID == cno select l.ReserveId).FirstOrDefault();
                    var reservecheck = (from l in obj.CLM_ClaimReserve where l.ClaimID == id && l.ClaimantID == cno orderby l.CreatedDate descending select l.FinalReserveOrgAmt).FirstOrDefault();
                    if (check.ToString() == "0")
                    {
                        ModelState.Clear();
                        TempData["result"] = "Can Not Make Payment Because Reserve Has Not Been Set For This Claimant";
                    }
                    else if (Convert.ToInt64(reservecheck) < Convert.ToInt64(model.PayableOrgAmt))
                    {
                        ModelState.Clear();
                        TempData["result"] = "Payment Amount For This Claimant Cannot Be Greater Then Reserve";
                    }
                    else
                    {
                        ModelState.Clear();
                        var list = model.Update(id, cno);
                        TempData["result"] = "Records Saved Successfully.";
                    }
                    obj.Dispose();
                    return View(model);
                }
                else
                {
                    ModelState.Clear();
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }


        #region "claim_Payment"
        public ActionResult ClaimPayment()
        {
            var id = Request.QueryString["ClaimId"] == null || Request.QueryString["ClaimId"] == "" ? 0 : Convert.ToInt32(Request.QueryString["ClaimId"]);
            List<ClaimPaymentInfo> list = new List<ClaimPaymentInfo>();
            list = ClaimPaymentInfo.Fetch(id);
            return View(list);
        }

        public ActionResult ClaimPaymentall()
        {
            int id = Request.QueryString["ClaimId"] == null ? 0 : Convert.ToInt32(Request.QueryString["ClaimId"]);
            List<ClaimPaymentInfo> list = new List<ClaimPaymentInfo>();
            list = ClaimPaymentInfo.Fetchall(id);
            TempData["res"] = ClaimPaymentInfo.Fetchname(id).Replace("**", "");
            return View(list);
        }


        #endregion

        #endregion

        #region "view Payment/Reserve history"
        [HttpGet]
        public ActionResult ReservePaymentHistory(int? ClaimID)
        {
            MCASEntities _db = new MCASEntities();
            //List<ClaimReserveHistory> str = _db.Database.SqlQuery<ClaimReserveHistory>("exec Proc_GetReserveAndPayment {0}", ClaimID).ToList();
            var objModel = _db.ExecuteStoreQuery<ClaimReserveHistory>("exec Proc_GetReserveAndPayment " + ClaimID, null).ToList();
            _db.Dispose();
            return View(objModel);
        }

        #endregion

        #region "Linking of Reported and Unreported Claims"

        public ActionResult ReportedClaimList(string AccidentId)
        {
            TempData["ReportedClaimAccidentId"] = AccidentId;
            var model = new ClaimAccidentDetailsModel();
            var modelList = model.ReportedClaimList(AccidentId);
            return View(modelList);
        }

        [HttpPost]
        public JsonResult LinkReportedList(FormCollection frm)
        {
            string UnReportedClaimAccidentId = TempData.Peek("ReportedClaimAccidentId").ToString();
            string ReportedClaimAccidentId = frm["AccidentClaimCheck"];

            string responseText = "Cannot link some error occured.";
            bool isSuccess = true;
            if (string.IsNullOrEmpty(UnReportedClaimAccidentId) || string.IsNullOrEmpty(ReportedClaimAccidentId))
            {
                isSuccess = false;
                responseText = "Please select atleast one item from the list";
            }
            else
            {
                int reportedClaimId = Convert.ToInt32(ReportedClaimAccidentId);

                //Check If Approve Mandate by Supervisor...
                bool isApproveMandate = obj.CLM_MandateSummary.Where(t => t.AccidentClaimId.Equals(reportedClaimId) && t.ApproveRecommedations == "Y").Any();
                if (isApproveMandate)
                {
                    isSuccess = false;
                    responseText = "Linking is not possible because there is Approved Mandate";
                }
                else
                {
                    int? iReturnValue = obj.Proc_LinkReportedUnReportedClaim(reportedClaimId, Convert.ToInt32(UnReportedClaimAccidentId)).SingleOrDefault();
                    if (iReturnValue.HasValue)
                        isSuccess = Convert.ToBoolean(iReturnValue.Value);
                }
            }
            return Json(new { result = isSuccess, responseText = responseText });
        }

        public ActionResult ClaimDetail(int accidentClaimId)
        {
            var claimaccident = getClaimAccidentObject();
            claimaccident.AccidentClaimId = accidentClaimId;
            claimaccident = claimaccident.Fetch();
            return View(claimaccident);

        }
        [HttpPost]
        public JsonResult UnLinkReportedList(string ReportedClaimAccidentId)
        {
            string responseText = "Cannot UnLink some error occured.";
            bool isSuccess = true;
            if (string.IsNullOrEmpty(ReportedClaimAccidentId))
            {
                isSuccess = false;
                //responseText = "Please select atleast one item from the list";
            }
            else
            {
                int reportedClaimId = Convert.ToInt32(ReportedClaimAccidentId);

                //Check If Approve Mandate by Supervisor...
                bool isApproveMandate = obj.CLM_MandateSummary.Where(t => t.AccidentClaimId.Equals(reportedClaimId) && t.ApproveRecommedations == "Y").Any();
                if (isApproveMandate)
                {
                    isSuccess = false;
                    responseText = "Linking is not possible because there is Approved Mandate";
                }
                else
                {
                    int? iReturnValue = obj.Proc_UnLinkReportedUnReportedClaim(reportedClaimId).SingleOrDefault();
                    if (iReturnValue.HasValue)
                        isSuccess = Convert.ToBoolean(iReturnValue.Value);
                }

            }
            return Json(new { result = isSuccess, responseText = responseText });
        }

        #endregion
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
        public JsonResult CheckOrgTypeForAccident(int AccidentClaimId, int PolicyId, string ClaimMode)
        {
            var claimaccident = getClaimAccidentObject();
            if (claimaccident.AccidentClaimId != AccidentClaimId)
            {
                ClearClaimObjectHelper();
                var ClaimObjectHelper = new ClaimObjectHelper().Fetch(AccidentClaimId);
                SetClaimObjectHelper(ClaimObjectHelper);
                claimaccident = getClaimAccidentObject();
            }
            string OrgTypeStr = ClaimAccidentDetailsModel.fetchOrganisationType(LoggedInUserId, claimaccident.Organization);
            if ((OrgTypeStr.ToLower() == "tx") || (OrgTypeStr.ToLower() == "pc"))
            {
                //return RedirectToAction("ClmAccDltPCNTXEditor", "ClaimRegistrationProcessing", new { policyId = 0, OrgCat = OrgTypeStr, AccidentClaimId = AccidentClaimId });
                return Json(new { url = "/ClaimRegistrationProcessing/ClmAccDltPCNTXEditor?policyId=" + PolicyId + "&OrgCat=" + OrgTypeStr + "&AccidentClaimId=" + AccidentClaimId });
            }
            else
            {
                //return RedirectToAction("ClaimAccidentEditor", "ClaimProcessing", new { policyId = 0, AccidentClaimId = AccidentClaimId });
                return Json(new { url = "/ClaimProcessing/ClaimAccidentEditor?AccidentClaimId=" + AccidentClaimId + "&policyId=" + PolicyId + "&claimMode=" + ClaimMode });
            }
        }
    }
}

