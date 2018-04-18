using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Entity;
using System.Globalization;
using System.Web.Security;
using System.Data.Objects;
using System.Net.Mail;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.SessionState;

namespace MCAS.Controllers
{
    public class HomeController : BaseController
    {
        MCASEntities _db = new MCASEntities();


        #region Diary
        public ActionResult Index()
        {
            List<DiaryModel> list = new List<DiaryModel>();
            var model = new DiaryModel();
            try
            {
                LoadMenuList();
                model.GetType().GetProperty("screenId").SetValue(model, "200", null);
                model.ApprovedList = model.GetApprovedList();
                TempData["SP"] = model.UserPermissions.SplPermission;
                model.TaskSelectionList = DiaryModel.FetchTaskSelectionList();
                model.Values = ReAssignmentModel.Fetch();
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(model);
        }
        public ActionResult DiaryListing()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDiaries()
        {
            List<DiaryModel> list = new List<DiaryModel>();
            list = DiaryModel.FetchDiary(UserId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Index(DiaryModel model)
        {
            try
            {
                LoadMenuList();
                model.GetType().GetProperty("screenId").SetValue(model, "200", null);
                model.ApprovedList = model.GetApprovedList();
                TempData["SP"] = model.UserPermissions.SplPermission;
                if (model.SaveCheck == "0" || model.SaveCheck == null)
                {
                    ModelState.Clear();
                    model.IPNo = "";
                    model.ClaimantName = "";
                    model.AssignedTo = "";
                    model.ReassignedTo = "";
                    model.Requestor = "";
                    model.Approver = "";
                    model.Approved = "";
                }
                model = DiaryModel.Fetchgrid(model);
                
                model.SaveCheck = "0";
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult DairyJson(int draw, int start, int length)
        {
            CommonUtilities.DatatableGrid list = new CommonUtilities.DatatableGrid();
            JsonResult jsonResult = new JsonResult();
            List<VehicleBusCaptainModel> VehicleBusCaptainlist = new List<VehicleBusCaptainModel>();
            try
            {
                var IPNo = Request.Form["IPNo"] == null ? "" : Convert.ToString(Request.Form["IPNo"]).Trim();
                var ClaimantName = Request.Form["ClaimantName"] == null ? "" : Convert.ToString(Request.Form["ClaimantName"]).Trim();
                var AssignedTo = Request.Form["AssignedTo"] == null ? "" : Convert.ToString(Request.Form["AssignedTo"]).Trim();
                var ReassignedTo = Request.Form["ReassignedTo"] == null ? "" : Convert.ToString(Request.Form["ReassignedTo"]).Trim();
                var SaveCheck = Request.Form["SaveCheck"] == null ? "" : Convert.ToString(Request.Form["SaveCheck"]).Trim();
                var Searchval = Convert.ToString(Request.Form["search[value]"]) ?? Convert.ToString(Request.Form["Searchval"]) ?? "";

                int sortColumn = Request.Form["order[0][column]"] != null ? Convert.ToInt32(Request.Form["order[0][column]"]) : -1;
                string sortDirection = Request.Form["order[0][dir]"] != null ? Convert.ToString(Request.Form["order[0][dir]"]) : "asc";

                list = DiaryModel.FetchDairyAccToLoginuser( draw, start, length, sortColumn, sortDirection, IPNo, ClaimantName, AssignedTo, ReassignedTo,SaveCheck, Searchval.Trim());

                jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return jsonResult;
        }

        [HttpPost]
        public JsonResult FetchTaskList()
        {
            return Json(DiaryModel.FetchTaskList(LoggedInUserId), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult FetchMandateList()
        {
            return Json(DiaryModel.FetchMandateListAccToLoginuser(), JsonRequestBehavior.AllowGet);
        }

        public IQueryable<DiaryModel> GetSearchResult(string SearchCriteria)
        {
            var searchResult = (from diary in _db.TODODIARYLISTs
                                where
                                  diary.NOTE.Contains(SearchCriteria) &&
                                  diary.SUBJECTLINE.Contains(SearchCriteria) &&
                                  diary.INSURERNAME.Contains(SearchCriteria) &&
                                  diary.CLAIMNO.Contains(SearchCriteria) &&
                                  diary.POLICYNO.Contains(SearchCriteria)
                                select diary).ToList().Select(item => new DiaryModel
                                {
                                    Note = item.NOTE,
                                    SubjectLine = item.SUBJECTLINE,
                                    Rec_date = Convert.ToDateTime(item.RECDATE),
                                    Follow_Up_dateTime = Convert.ToDateTime(item.FOLLOWUPDATE),
                                    ListTypeID = Convert.ToString(item.LISTTYPEID),
                                    ClaimName = item.CLAIMNO,
                                    PolicyName = item.POLICYNO,
                                    InsurerName = item.INSURERNAME,
                                    ListId = Convert.ToInt32(item.LISTID)

                                }).AsQueryable();

            return searchResult;
        }



        [HttpPost]
        public JsonResult DeleteDiary(string DiaryToDelete)
        {
            MCASEntities _db = new MCASEntities();
            string[] listIds = DiaryToDelete.Split(',');
            List<TODODIARYLIST> diaryList = new List<TODODIARYLIST>();
            try
            {
                foreach (var listid in listIds)
                {
                    Int64 id = Convert.ToInt64(listid);
                    TODODIARYLIST selectedDiary = (TODODIARYLIST)_db.TODODIARYLISTs.Where(l => l.LISTID.Equals(id)).FirstOrDefault();
                    diaryList.Add(selectedDiary);
                }
                foreach (var diary in diaryList)
                {
                    var res = _db.Proc_ToDoDairyList(diary.LISTID);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            Index();
            return Json("Record deleted successfully!");
        }

        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult DiaryEditor(int? ListId)
        {
            var diarylist = new DiaryModel();
            var Accidentid = Request.QueryString["AccidentClaimId"] == null ? 0 : Convert.ToInt32(Request.QueryString["AccidentClaimId"]);
            List<EsclationStatus> list1 = new List<EsclationStatus>();
            list1.Add(new EsclationStatus() { ID = "Y", Name = "Yes" });
            list1.Add(new EsclationStatus() { ID = "N", Name = "No" });
            SelectList sl1 = new SelectList(list1, "ID", "Name");
            try
            {

                if (ListId.HasValue)
                {
                    var Diarylist = (from x in _db.TODODIARYLISTs where x.LISTID == ListId select x).FirstOrDefault();
                    DiaryModel objmodel = new DiaryModel();
                    objmodel.ListId = (int)Diarylist.LISTID;
                    objmodel.ClaimId = (int)Diarylist.CLAIMID;
                    objmodel.PolicyId = (int)Diarylist.POLICY_ID;
                    objmodel.ListTypeID = Convert.ToString(Diarylist.LISTTYPEID).Trim();
                    objmodel.ToUserId = Convert.ToString(Diarylist.TOUSERID).Trim();
                    objmodel.FromUserId = Convert.ToString(Diarylist.FROMUSERID).Trim();
                    objmodel.Follow_Up_dateTime = (DateTime)Diarylist.FOLLOWUPDATE;
                    objmodel.ExpectedPaymentDate = Diarylist.ExpectedPaymentDate;
                    objmodel.StartTime = Diarylist.STARTTIME;
                    objmodel.EndTime = Diarylist.ENDTIME;
                    objmodel.ReminderBeforeCompletion = Diarylist.ReminderBeforeCompletion;
                    objmodel.Escalation = Diarylist.Escalation;
                    objmodel.EscalationTo = Diarylist.EscalationTo;
                    objmodel.SubjectLine = Diarylist.SUBJECTLINE;
                    objmodel.EmailBody = Diarylist.EmailBody;
                    objmodel.Note = Diarylist.NOTE;
                    if (ListId.HasValue)
                        objmodel.ListId = ListId;
                    objmodel.TypeList = LoadLookUpValue("AlertDesc");//LoadDiaryListType();
                    objmodel.UserList = LoadUserList();
                    objmodel.Escalationlist = sl1;
                    objmodel.AccidentId = Accidentid;
                    return View(objmodel);
                }
                if (ListId.HasValue)
                    diarylist.ListId = ListId;
                diarylist.UserList = LoadUserList();
                diarylist.TypeList = LoadLookUpValue("AlertDesc"); //LoadDiaryListType();
                diarylist.Escalationlist = sl1;
                return View(diarylist);
            }
            catch (System.Data.DataException) { ModelState.AddModelError("", "Unable to View ."); }
            return View(diarylist);

        }

        [HttpPost]
        public ActionResult DiaryEditor(DiaryModel model)
        {
            TempData["notice"] = "";

            try
            {
                List<EsclationStatus> list1 = new List<EsclationStatus>();
                list1.Add(new EsclationStatus() { ID = "Y", Name = "Yes" });
                list1.Add(new EsclationStatus() { ID = "N", Name = "No" });
                SelectList sl1 = new SelectList(list1, "ID", "Name");
                model.TypeList = LoadLookUpValue("AlertDesc");//LoadDiaryListType();
                model.UserList = LoadUserList();
                model.Escalationlist = sl1;
                int UserId = base.UserId;
                model.UserIds = UserId;
                var param = MCASQueryString;
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    var hits = (from diary in _db.TODODIARYLISTs where diary.LISTID == model.ListId select diary).FirstOrDefault();
                    if (hits == null)
                    {
                        ModelState.Clear();
                        var diaries = model.Update();
                        TempData["notice"] = "Record Saved Successfully.";
                        return View(model);
                    }
                    else
                    {

                        ModelState.Clear();
                        var diaries = model.Update();
                        TempData["notice"] = "Record Updated Successfully.";
                        return View(model);
                    }

                }
                else
                {
                    return View(model);
                }

            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }


        public JsonResult FillDiaryEntry()
        {
            var returnData = LoadDiaryUsrList();
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
                    string ModifiedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
                    todo = db.TODODIARYLISTs.Where(t => t.LISTID == id).SingleOrDefault();
                    todo.ReassignedDiary = "Yes";
                    todo.ReassignedDiaryDate = DateTime.Now;
                    todo.ModifiedBy = ModifiedBy;
                    todo.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }
            }
            return Json(result);
        }

        public ActionResult ReAssignmentEditor(string DairyId)
        {
            var ReAssignmentEditor = new ReAssignmentModel();
            ReAssignmentEditor.ReAssignTo = new int[] { 0 };
            ReAssignmentEditor.Values = ReAssignmentModel.Fetch();
            MCASEntities _db = new MCASEntities();
            string[] listIds = DairyId.Split(',');
            List<string> diaryList = new List<string>();
            List<string> diaryuserId = new List<string>();
            foreach (var listid in listIds)
            {
                Int64 id = Convert.ToInt64(listid);
                var selectedDiaries = (from m in _db.TODODIARYLISTs join l in _db.MNT_Lookups on m.LISTTYPEID equals System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)l.LookupID) where m.LISTID == id select l.Lookupdesc).FirstOrDefault();

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
            ReAssignmentEditor.Dairiestobereassigned = ResultString;
            ReAssignmentEditor.DairiestobereassignedId = DairyId;
            ReAssignmentEditor.DairyFromUser = ResultusrString;
            return View(ReAssignmentEditor);
        }

        [HttpPost]
        public ActionResult ReAssignmentEditor(ReAssignmentModel model)
        {
            model.Values = ReAssignmentModel.Fetch();
            try
            {
                if (ModelState.IsValid)
                {
                    var str = string.Join(",", model.ReAssignTo.Select(x => x.ToString()).ToArray());
                    ModelState.Clear();
                    TempData["result"] = "Records Saved Successfully.";
                    return View("DiaryEditor");
                }
                else
                {
                    ModelState.Clear();
                    return View("DiaryEditor");
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View("DiaryEditor");
        }


        #endregion


        #region Login
        public ActionResult Login()
        {
            UserLogin userlogin = new UserLogin();
            UserLogin.UpdateLoginDetails();
            Session.Clear();
            userlogin.branchlist = LoadLoginUserBranch();
            userlogin.usercountrylist = LoadLoginUserCountry();
            string[] lang = Request.UserLanguages;
            if (lang != null && lang.Length > 0)
            {

            }
            var appVer = userlogin.GetAppVersion().FirstOrDefault();
            if (appVer != null)
            {
                userlogin.appVer = appVer.appVer;
                userlogin.ReleaseDate = appVer.ReleaseDate;
            }
            var pwds = (from pw in _db.MNT_PasswordSetup select pw).FirstOrDefault();
            if (pwds != null)
            {
                userlogin.AllowRetrievePwd = pwds.SendForgetPwdThroughMail;
            }
            ResolveCulture(Request);
            ResolveCountry(Request);
            return View(userlogin);
        }
        [HttpPost]
        public ActionResult Login(UserLogin model)
        {
            try
            {
                model.branchlist = LoadLoginUserBranch();
                model.usercountrylist = LoadLoginUserCountry();
                if (ModelState.IsValid)
                {
                    var result = model.IsValid(model.LoggedInUserId, EnDecryption.EncryptMessage(model.LoggedInUserPwd), model.LoggedInUserBranch, model.LoggedInUserCountry);
                    ViewData["Loginresult"] = result;
                    SessionIDManager manager = new SessionIDManager();
                    string newSessionId = manager.CreateSessionID(System.Web.HttpContext.Current);
                    var userinfo = (from usr in _db.MNT_Users where usr.UserId == result select usr).FirstOrDefault();
                    if (userinfo != null)
                    {
                        base.UserId = userinfo.SNo;
                        base.LoggedInUserId = model.LoggedInUserId = userinfo.UserId;
                        base.LoggedInUserName = model.LoggedInUserDispName = userinfo.UserDispName;
                        base.LoggedInBranch = model.LoggedInUserBranch = userinfo.BranchCode;
                        base.LoggedInCountryId = model.LoggedInUserCountryId;
                        base.LoggedInCountryName = model.LoggedInUserCountry;
                        base.SessionId = model.SessionId = newSessionId;
                        if (userinfo.GroupId != null)
                        {
                            var grp = (from g in _db.MNT_GroupsMaster where g.GroupId == userinfo.GroupId select g).FirstOrDefault();
                            base.UserGroupId = grp != null ? grp.GroupId : 0;
                        }
                        else
                            base.UserGroupId = 0;
                        FormsAuthentication.SetAuthCookie(model.LoggedInUserId, false);
                        UserLogin.InsertLoginDetails(model);
                        return RedirectToAction("Index", "Home");
                    }
                    var appVer = model.GetAppVersion().FirstOrDefault();
                    if (appVer != null)
                    {
                        model.appVer = appVer.appVer;
                        model.ReleaseDate = appVer.ReleaseDate;
                    }
                    var pwds = (from pw in _db.MNT_PasswordSetup select pw).FirstOrDefault();
                    if (pwds != null)
                    {
                        model.AllowRetrievePwd = pwds.SendForgetPwdThroughMail;
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }

        }

        [HttpPost]
        public JsonResult RetrievePassword(string userId, string EmailAddress)
        {
            string responseText = string.Empty;
            bool isSuccess = true;
            if (string.IsNullOrEmpty(EmailAddress))
            {
                isSuccess = false;
                responseText = "Email Id is required";
            }
            else
            {
                var objUser = _db.MNT_Users.Where(t => t.UserId.Equals(userId) && t.EmailId == EmailAddress && t.IsActive.Equals("Y")).FirstOrDefault();
                if (objUser != null)
                {

                    string pwd = EnDecryption.DecryptMessage(objUser.LoginPassword);
                    string subject = "Retrieval Of Password For " + objUser.UserId;
                    string emailBody = UserLogin.GetEmailBodyForRetrievePassword(objUser.UserDispName, objUser.UserId, pwd);
                    string createdby = objUser.UserDispName;

                    try
                    {
                        int? iReturnValue = _db.Proc_RetrievePassword(EmailAddress, subject, emailBody, createdby).SingleOrDefault();
                        if (iReturnValue.HasValue)
                            isSuccess = Convert.ToBoolean(iReturnValue.Value);

                    }
                    catch (Exception)
                    {
                        isSuccess = false;
                        responseText = "Cannot retrieve some error occured.";
                    }
                }
                else
                {
                    isSuccess = false;
                    responseText = "Unsuccessful! Please retry or check with your System Admin.";
                }
            }

            return Json(new { result = isSuccess, responseText = responseText });
        }


        //[HttpPost]
        //public JsonResult LinkReportedList(FormCollection frm)
        //{
        //    string UnReportedClaimAccidentId = TempData.Peek("ReportedClaimAccidentId").ToString();
        //    string ReportedClaimAccidentId = frm["AccidentClaimCheck"];

        //    string responseText = "Cannot link some error occured.";
        //    bool isSuccess = true;
        //    if (string.IsNullOrEmpty(UnReportedClaimAccidentId) || string.IsNullOrEmpty(ReportedClaimAccidentId))
        //    {
        //        isSuccess = false;
        //        responseText = "Please select atleast one item from the list";
        //    }
        //    else
        //    {
        //        int reportedClaimId = Convert.ToInt32(ReportedClaimAccidentId);

        //        //Check If Approve Mandate by Supervisor...
        //        bool isApproveMandate = obj.CLM_MandateSummary.Where(t => t.AccidentClaimId.Equals(reportedClaimId) && t.ApproveRecommedations == "Y").Any();
        //        if (isApproveMandate)
        //        {
        //            isSuccess = false;
        //            responseText = "Linking is not possible because there is Approved Mandate";
        //        }
        //        else
        //        {
        //            int? iReturnValue = obj.Proc_LinkReportedUnReportedClaim(reportedClaimId, Convert.ToInt32(UnReportedClaimAccidentId)).SingleOrDefault();
        //            if (iReturnValue.HasValue)
        //                isSuccess = Convert.ToBoolean(iReturnValue.Value);
        //        }
        //    }
        //    return Json(new { result = isSuccess, responseText = responseText });
        //}
        private static CultureInfo ResolveCulture(HttpRequestBase request)
        {
            string[] languages = request.UserLanguages;
            if (languages == null || languages.Length == 0)
                return null;
            try
            {
                string language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
        private static RegionInfo ResolveCountry(HttpRequestBase request)
        {
            CultureInfo culture = ResolveCulture(request);
            if (culture != null)
                return new RegionInfo(culture.LCID);
            return null;
        }


        public ActionResult MenuList()
        {
            return View(TempData["menuList"]);
        }
        [HttpGet]
        public JsonResult GetMenuList()
        {
            List<MenuListItem> list = new List<MenuListItem>();
            list = LoadMenuList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region DiarySetUp

        [HttpGet]
        public ActionResult DiarySetUp()
        {
            MNT_DIARY_DETAILS diary = new MNT_DIARY_DETAILS();

            MCASEntities _db = new MCASEntities();
            var list = new List<SelectListItem>();

            var users = from c in _db.MNT_Users
                        select new
                        {
                            c.SNo,
                            c.UserDispName
                        };
            DiarySetupModel model = new DiarySetupModel();
            model.Prioritylist = LoadLookUpValue("Priority");
            return View(model);

        }

        [HttpPost]
        public ActionResult DiarySetUp(DiarySetupModel model)
        {
            try
            {
                model.Prioritylist = LoadLookUpValue("Priority");
                var hits = (from x in _db.MNT_DIARY_DETAILS where x.ID == model.ID select x).FirstOrDefault();
                if (hits == null)
                {
                    ModelState.Clear();
                    var diarysetup = model.DiarySetUpUpdate();
                    TempData["notice"] = "Records Saved Successfully.";
                }
                else
                {
                    ModelState.Clear();
                    var diarysetup = model.DiarySetUpUpdate();
                    TempData["notice"] = "Records Updated Successfully.";
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);

        }

        public JsonResult FillModule()
        {
            var returnData = LoadModule();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }



        public class Users
        {
            public int SNo { get; set; }
            public string UserDispName { get; set; }

        }

        public MultiSelectList GetUsers(string[] selectedValues)
        {
            var use = _db.MNT_Users.ToList();
            return new MultiSelectList(use, "SNo", "UserDispName", selectedValues);
        }

        #endregion

        #region session time out
        public ActionResult SessionTimeOut()
        {
            return PartialView("SessionTimeOut");
        }

        #endregion

        #region session time out
        public ActionResult SessionMultipleLogin()
        {
            return PartialView("SessionMultipleLogin");
        }

        #endregion


        #region TransactionEditorScreen
        public JsonResult GetvalueList(string cat, string UniqueId)
        {
            var sgf = TransactionModel.GetvalLsit(cat);
            return Json(sgf);
        }
        public ActionResult TransactionEditorScreen()
        {
            TransactionModel Tran = new TransactionModel();
            Tran = TransactionModel.Fetchvalue(Tran);
            return View(Tran);
        }



        [HttpPost]
        public JsonResult TransactionEditorScreenJson(int draw, int start, int length)
        {
            CommonUtilities.DatatableGrid list = new CommonUtilities.DatatableGrid();
            JsonResult jsonResult = new JsonResult();
            List<TransactionModel> TransactionEditorlist = new List<TransactionModel>();
            try
            {
                var Selectcriteria = Request.Form["Selectcriteria"] == null ? "" : Convert.ToString(Request.Form["Selectcriteria"]).Trim();
                var Valuecriteria = Request.Form["Valuecriteria"] == null ? "" : Convert.ToString(Request.Form["Valuecriteria"]).Trim();
                var Searchval = Request.Form["search[value]"] == null ? "" : Convert.ToString(Request.Form["search[value]"]).Trim();
                int sortColumn = Request.Form["order[0][column]"] != null ? Convert.ToInt32(Request.Form["order[0][column]"]) : -1;
                string sortDirection = Request.Form["order[0][dir]"] != null ? Convert.ToString(Request.Form["order[0][dir]"]) : "desc";

                list = TransactionModel.TransactionEditorScreenList(Selectcriteria, Valuecriteria, draw, start, length, sortColumn, sortDirection, Request.RawUrl, Convert.ToBoolean(Request.Form["bool"]), Searchval);

                jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return jsonResult;
        }
        #endregion

        #region LoginAuditLog
        public ActionResult LoginAuditLog()
        {
            int id = Request.QueryString["AccidentClaimId"] == null ? 239 : Convert.ToInt32(Request.QueryString["AccidentClaimId"]);
            LoginAuditLogModel Tran = new LoginAuditLogModel();
            return View(Tran);
        }
        [HttpPost]
        public JsonResult GetLoginAuditLogDetails(int draw, int start, int length)
        {
            CommonUtilities.DatatableGrid list = new CommonUtilities.DatatableGrid();
            JsonResult jsonResult = new JsonResult();
            List<LoginAuditLogModel> TransactionEditorlist = new List<LoginAuditLogModel>();

            try
            {
                var Selectcriteria = Request.Form["UserId"];
                var Searchval = Request.Form["search[value]"] == null ? "" : Convert.ToString(Request.Form["search[value]"]).Trim();
                int sortColumn = Request.Form["order[0][column]"] != null ? Convert.ToInt32(Request.Form["order[0][column]"]) : -1;
                string sortDirection = Request.Form["order[0][dir]"] != null ? Convert.ToString(Request.Form["order[0][dir]"]) : "asc";

                list = LoginAuditLogModel.GetLoginAuditLogDetailsList(Selectcriteria, draw, start, length, sortColumn, sortDirection, Request.RawUrl, Searchval);
                TransactionEditorlist = LoginAuditLogModel.GetLoginAuditLogDetailsListAll(Selectcriteria, draw, start, length, sortColumn, sortDirection, Request.RawUrl, Searchval).ToList();
                TempData["LoginAuditList"] = TransactionEditorlist;
                jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return jsonResult;
        }
        public JsonResult GetAllUserId()
        {
            JsonResult jsonResult = new JsonResult();
            List<LoginAuditLogModel> TransactionEditorlist = new List<LoginAuditLogModel>();

            try
            {

                var list = LoginAuditLogModel.GetAllUserList();

                jsonResult = Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return jsonResult;
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
        public FileResult FileDownLoader()
        {
            StringWriter sw = new StringWriter();
            List<LoginAuditLogModel> list = (List<LoginAuditLogModel>)TempData["LoginAuditList"];
            string format = "Excel";
            format = Request.QueryString["Fileformat"] != null ? Request.QueryString["Fileformat"].ToString() : format;
            string fileName = "LoginAuditLog.xls";
            if (format.ToUpper().Equals("EXCEL"))
            {
                GridView gv = new GridView();
                gv.DataSource = list;
                gv.AutoGenerateColumns = false;
                string columnNames = "UserId,LogInTimestring,LogOutTimestring";
                string columnHeaders = "UserId,LoggedInTime,LoggedOutTime";
                addGridColumns(gv, columnNames, columnHeaders);
                gv.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            TempData["LoginAuditList"] = list;
            return null;
        }
        #endregion

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

        public JsonResult AlertReassigned(string DairyId, string ReassignAccidentId, string ReassignClaimId, string ReAssignTo, string ReAssignDateFrom, string Remark, string Random)
        {
            MCASEntities _db = new MCASEntities();
            string[] listIds = DairyId.Split(',');
            List<string> diaryList = new List<string>();
            try
            {
                foreach (var id in listIds)
                {
                    int listId = Convert.ToInt32(id);
                    var Dairyfrmuser = (from m in _db.TODODIARYLISTs where m.LISTID == listId select m.TOUSERID).FirstOrDefault().ToString();
                    var results = (from t in _db.TODODIARYLISTs where t.LISTID == listId select t).FirstOrDefault();
                    var result = ReAssignmentModel.UpdateMultipleReassign(id, "T", ReAssignTo, Dairyfrmuser, Convert.ToString(ReAssignDateFrom), Convert.ToString(DateTime.Now), Remark, Convert.ToString(results.CLAIMID), Convert.ToInt32(results.AccidentId));
                    if (result == "T")
                    {
                        var diaryId = listId;
                        var todo = new TODODIARYLIST();
                        var model1 = new DiaryModel();
                        string ModifiedBy = LoggedInUserId;
                        todo = _db.TODODIARYLISTs.Where(t => t.LISTID == diaryId).SingleOrDefault();
                        todo.ReassignedDiary = "Yes";
                        todo.ReassignedDiaryDate = DateTime.Now;
                        todo.ModifiedBy = ModifiedBy;
                        todo.ModifiedDate = DateTime.Now;
                        todo.AccidentId = Convert.ToInt32(results.AccidentId);
                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json("T");
        }

        [HttpPost]
        public JsonResult GetGraphDataFinalizedOutstandingClaims(string year)
        {
            //List<object> iData = new List<object>();
            //List<string> labels = new List<string>() { "Jan-15", "Feb-15", "Mar-15", "Apr-15", "May-15", "Jun-15", "Jul-15", "Aug-15", "Sep-15", "Oct-15", "Nov-15", "Dec-15" };

            //iData.Add(labels);

            //List<int> lst_dataItem_1 = new List<int>() { 20, 10, 19, 50, 30, 48, 45, 36, 38, 49, 12, 55 };
            //iData.Add(lst_dataItem_1);

            //List<int> lst_dataItem_2 = new List<int>() { 30, 50, 50, 60, 20, 10, 19, 50, 30, 48, 45, 36 };
            //iData.Add(lst_dataItem_2);

            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            List<int> lst_dataItem_1 = new List<int>();
            List<int> lst_dataItem_2 = new List<int>();

            DateTime dt = DateTime.Now;
            DateTime dtOneYearAgo = dt.AddYears(-1);
            DateTime dtOneYearAgoStartDate = new DateTime(dtOneYearAgo.Year, dtOneYearAgo.Month, 1);

            MCASEntities _db = new MCASEntities();

            //Start-Pending Claims
            var qryPendingClaims = (from s in _db.ClaimAccidentHistoryDetails.Where(t => t.StatusChangeDate >= dtOneYearAgoStartDate)
                                    group s by new { s.ClaimId, s.Status } into g
                                    select new
                                    {
                                        ClaimId = g.Key.ClaimId,
                                        Status = g.Key.Status,
                                        StatusChangeDate = g.Max(p => p.StatusChangeDate)
                                    }).ToList();

            //End-Pending Claims

            //Start-Finalized Claim
            var qryFinalizedClaims = (from s in _db.ClaimAccidentHistoryDetails.Where(t => t.StatusChangeDate >= dtOneYearAgoStartDate && t.Status == "2")
                                      select new
                                      {
                                          ClaimId = s.ClaimId,
                                          StatusChangeDate = s.StatusChangeDate
                                      }).ToList();
            //End-Finalized Claim

            for (int i = 1; i <= 12; i++)
            {
                DateTime eachMonth = dtOneYearAgo.AddMonths(i);
                labels.Add(eachMonth.ToString("MMM") + "-" + eachMonth.Year.ToString());

                //Start-Finalized Claim
                var totalCountFinalized = (from final in qryFinalizedClaims.Where(t => t.StatusChangeDate.Value.Year == eachMonth.Year && t.StatusChangeDate.Value.Month == eachMonth.Month)
                                           select final).Count();
                lst_dataItem_1.Add(totalCountFinalized);
                //End-Finalized Claim

                //Start-Pending Claims
                var qryMonth = (from s in qryPendingClaims.Where(t => t.StatusChangeDate.Value.Year == eachMonth.Year && t.StatusChangeDate.Value.Month == eachMonth.Month)
                                group s by new { s.ClaimId } into g
                                select new
                                {
                                    ClaimId = g.Key.ClaimId,
                                    StatusChangeDate = g.Max(p => p.StatusChangeDate)
                                }).ToList();

                var totalcount = (from whole in qryPendingClaims.Where(t => t.Status == "1")
                                  join monthly in qryMonth
                                  on new { whole.ClaimId, whole.StatusChangeDate } equals new { monthly.ClaimId, monthly.StatusChangeDate }
                                  select whole).Count();

                lst_dataItem_2.Add(totalcount);
                //End-Pending Claims                
            }


            //lst_dataItem_1 = new List<int>() { 20, 10, 19, 50, 30, 48, 45, 36, 38, 49, 12, 55 };
            //lst_dataItem_2 = new List<int>() { 30, 50, 50, 60, 20, 10, 19, 50, 30, 48, 45, 36 };


            iData.Add(labels);
            iData.Add(lst_dataItem_1);
            iData.Add(lst_dataItem_2);

            return Json(iData);
        }

        [HttpPost]
        public JsonResult GetGraphDataNewClaims(string year)
        {
            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            List<int> lst_dataItem_1 = new List<int>();

            DateTime dt = DateTime.Now;
            DateTime dtOneYearAgo = dt.AddYears(-1);
            DateTime dtOneYearAgoStartDate = new DateTime(dtOneYearAgo.Year, dtOneYearAgo.Month, 1);

            MCASEntities _db = new MCASEntities();
            var result = (from s in _db.CLM_Claims.Where(t => t.CreatedDate >= dtOneYearAgoStartDate)
                          group s by new { Year = s.CreatedDate.Value.Year, Month = s.CreatedDate.Value.Month } into g
                          select new
                          {
                              CreatedDate = g.Key,
                              TotalCount = g.Count()
                          }).ToList();

            for (int i = 1; i <= 12; i++)
            {
                DateTime eachMonth = dtOneYearAgo.AddMonths(i);
                labels.Add(eachMonth.ToString("MMM") + "-" + eachMonth.Year.ToString());

                int totalCount = result.Where(t => t.CreatedDate.Year == eachMonth.Year && t.CreatedDate.Month == eachMonth.Month).Select(t => t.TotalCount).FirstOrDefault();

                lst_dataItem_1.Add(totalCount);

            }

            iData.Add(labels);
            iData.Add(lst_dataItem_1);
            return Json(iData);
        }
    }
}
