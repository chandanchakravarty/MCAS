using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using System.ComponentModel;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq.Dynamic;
using MCAS.Web.Objects.Resources.Common;
using MCAS.Globalisation;
namespace MCAS.Web.Objects.MastersHelper
{
    public class IsFutureDateAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else if (((DateTime)value).Date == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()))
            {
                return true;
            }
            else if (((DateTime)value).Date < Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "isfuturedate"
            };
        }
    }
    public class DiaryModel : BaseModel
    {
        MCASEntities _db = new MCASEntities();
        #region Diary
        #region properties
        public string ClaimantName { get; set; }
        public string AssignedTo { get; set; }
        public string ReassignedTo { get; set; }
        public string Requestor { get; set; }
        public string Approver { get; set; }


        public string Approved { get; set; }
        public List<LookUpListItems> ApprovedList { get; set; }
        private DateTime? _startTime = null;
        private DateTime? _endTime = null;
        private DateTime? _follow_Up_dateTime = null;
        private DateTime? _reassignedDiaryDate = null;
        private DateTime? _reAssignDateFrom = null;
        private string _screenId = "136";
        public int? ListId { get; set; }
        public string Uname { get; set; }
        public string SaveCheck { get; set; }
        public string IPNo { get; set; }
        public string Ugroup { get; set; }
        public int UserIds { get; set; }
        public int Sno { get; set; }
        public int AssignTo { get; set; }
        public bool Reassignpermission { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVAssignedTo")]
        public string ToUserId { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVCreatedBy")]
        public string FromUserId { get; set; }
        public DateTime RecDate { get; set; }
        public string DivHeight { get; set; }
        public int? AccidentId { get; set; }

        public string Cmode { get; set; }
        public string RoleCode { get; set; }
        public string ReAssignToId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Rec_date { get; set; }

        //[DisplayName("FollowUp Date")]
        //[DataType(DataType.Date, ErrorMessage = "{0} must be in 'dd/mm/yyyy' format")]
        //[IsFutureDate(ErrorMessage = "Follow Up Date can not be less then current date.")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVFollowUpDate")]
        public DateTime? Follow_Up_dateTime
        {
            get { return _follow_Up_dateTime; }
            set { _follow_Up_dateTime = value; }
        }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVAlertsDescription")]
        public string ListTypeID { get; set; }

        public string ListType { get; set; }
        public string Chk { get; set; }
        public string Action { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVEmailSubject")]
        public string SubjectLine { get; set; }
        public string ListOpen { get; set; }
        public string priority { get; set; }

        public string Note { get; set; }
        public string Notification { get; set; }
        public int App_Id { get; set; }

        public int InsurerId { get; set; }
        public string InsurerName { get; set; }
        public string TaskSelection { get; set; }
        public int ClaimId { get; set; }
        public string ClaimName { get; set; }
        public string ClaimNO { get; set; }

        public int NotificationLists { get; set; }
        public int[] Diarylistdeleted { get; set; }

        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public int UserSNo { get; set; }
        public int UserSNo_ { get; set; }
        //[DisplayName("Expected Payment Date")]
        //[DataType(DataType.Date, ErrorMessage = "{0} must be in 'dd/mm/yyyy' format")]
        //[IsFutureDate(ErrorMessage = "Expected Payment Date can not be less then current date.")]
        public DateTime? ExpectedPaymentDate { get; set; }


        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVReminderBeforeCompDate")]
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVReminderBefNot")]
        public int? ReminderBeforeCompletion { get; set; }

        public string Escalation { get; set; }


        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVEscalationTo")]
        public string EscalationTo { get; set; }

        public string EmailBody { get; set; }
        public string ReassignedDiary { get; set; }

        public int Prop1 { get; set; }
        public string starttimestr { get; set; }
        public DateTime? ReassignedDiaryDate
        {
            get { return _reassignedDiaryDate; }
            set { _reassignedDiaryDate = value; }
        }

        [DisplayName("Estimated Completion Date")]
        //[DataType(DataType.Date, ErrorMessage = "{0} must be in 'dd/mm/yyyy' format")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor), ErrorMessageResourceName = "RFVEstimatedCompletionDate")]
        //[IsFutureDate(ErrorMessage = "Estimated Completion Date can not be less then current date.")]
        public DateTime? StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        [DisplayName("Completed Date")]
        //[DataType(DataType.Date, ErrorMessage = "{0} must be in 'dd/mm/yyyy' format")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[IsFutureDate(ErrorMessage = "Completed Date can not be less then current date.")]
        public DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        public string hiddenprop { get; set; }

        public List<LookUpListItems> TypeList { get; set; }
        public List<UserList> UserList { get; set; }
        public List<LookUpListItems> Prioritylist { get; set; }
        public List<LookUpListItems> Notificationlist { get; set; }
        public List<LookUpListItems> TaskSelectionList { get; set; }
        public IEnumerable<SelectListItem> Escalationlist { get; set; }
        public List<DiaryModel> Diary { get; set; }
        public List<TaskModel> TaskCollection { get; set; }
        public List<DiaryModel> DairyList { get; set; }
        public List<DiaryModel> ReassignDairyList { get; set; }
        public List<DiaryModel> EscalationDairyList { get; set; }
        public List<MandateModelCOllection> MandateList { get; set; }
        public List<ClaimPaymentModelCollectionDashBoard> PaymentList { get; set; }
        public override string screenId
        {
            get
            {
                return _screenId;
            }
            set
            {
                _screenId = value;
            }
        }
        public override string listscreenId
        {

            get
            {
                return "136";
            }

        }

        #region reassign
        public string Dairiestobereassigned { get; set; }
        [DisplayName("Reassignment To")]
        public string ReAssignTo { get; set; }
        public IEnumerable<SelectListItem> Values { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Reassignment Date Form")]
        public DateTime? ReAssignDateFrom
        {
            get { return _reAssignDateFrom; }
            set { _reAssignDateFrom = value; }
        }
        public string Remark { get; set; }
        public string DairyId { get; set; }
        #endregion

        #endregion
        #region Methods
        #region staticMethod
        public static List<DiaryModel> FetchDiary(int UserId)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DiaryModel>();
            var Diarylist = (from x in _db.TODODIARYLISTs where x.TOUSERID == UserId select x);
            try
            {
                if (Diarylist.Any())
                {
                    foreach (var data in Diarylist)
                    {
                        var Types = (from p in _db.MNT_Lookups where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)p.LookupID) == data.LISTTYPEID select p).FirstOrDefault();
                        var model = new DiaryModel();
                        var createby = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
                        var Uname1 = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u.UserId).FirstOrDefault();
                        var ugroup1 = (from u in _db.MNT_Users where u.UserId == createby select u.GroupId).FirstOrDefault();
                        item.Add(new DiaryModel() { ListId = Convert.ToInt32(data.LISTID), ListType = Types == null ? "" : Types.Lookupdesc, Rec_date = Convert.ToDateTime(data.RECDATE), Follow_Up_dateTime = Convert.ToDateTime(data.FOLLOWUPDATE), SubjectLine = data.SUBJECTLINE, Note = data.NOTE, PolicyId = Convert.ToInt32(data.POLICY_ID), App_Id = Convert.ToInt32(data.APP_ID), Uname = Uname1, CreatedBy = createby, CreatedOn = DateTime.Now, Ugroup = Convert.ToString(ugroup1) });
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }
        public static List<DiaryModel> FetchAllDiary()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DiaryModel>();
            try
            {
                var Diarylist = (from x in _db.TODODIARYLISTs where x.LISTOPEN == "Y" || x.LISTOPEN == null select x).ToList();//.Take(50);
                if (Diarylist.Any())
                {
                    foreach (var data in Diarylist)
                    {
                        var Types = (from p in _db.MNT_Lookups where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)p.LookupID) == data.LISTTYPEID select p).FirstOrDefault();
                        var model = new DiaryModel();
                        var createby = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
                        var Uname1 = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u.UserId).FirstOrDefault();
                        var ugroup1 = (from u in _db.MNT_Users where u.UserId == createby select u.GroupId).FirstOrDefault();
                        item.Add(new DiaryModel() { ListId = Convert.ToInt32(data.LISTID), ListType = Types.Lookupdesc, Rec_date = Convert.ToDateTime(data.RECDATE), Follow_Up_dateTime = Convert.ToDateTime(data.FOLLOWUPDATE), SubjectLine = data.SUBJECTLINE, Note = data.NOTE, PolicyId = Convert.ToInt32(data.POLICY_ID), App_Id = Convert.ToInt32(data.APP_ID), Uname = Uname1, CreatedBy = createby, CreatedOn = DateTime.Now, Ugroup = Convert.ToString(ugroup1) });
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }
        public static List<DiaryModel> Fetch(int? AccidentId)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DiaryModel>();
            try
            {
                item = (from data in _db.TODODIARYLISTs
                        where data.AccidentId == AccidentId && data.ReassignedDiary.ToLower() != "yes" && data.IsActive.ToUpper()=="Y"
                        orderby data.LISTID descending
                        select new DiaryModel()
                        {
                            ListId = (int)data.LISTID,
                            ClaimId = (int)data.CLAIMID,
                            ListType = (from p in _db.MNT_Lookups where p.Lookupvalue == data.LISTTYPEID && p.Category == "AlertDesc" select p.Lookupdesc).FirstOrDefault() ?? "",
                            FromUserId = (from u in _db.MNT_Users where u.SNo == (int)(data.FROMUSERID) select u.UserDispName).FirstOrDefault() ?? "",
                            AssignTo = (int)(data.TOUSERID),
                            ToUserId = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u.UserDispName).FirstOrDefault() ?? "",
                            StartTime = data.STARTTIME,
                            EndTime = data.ENDTIME,
                            ReminderBeforeCompletion = data.ReminderBeforeCompletion,
                            EscalationTo = (from u in _db.MNT_Users where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)u.SNo).Trim() == data.EscalationTo select u.UserDispName).FirstOrDefault() ?? "",
                            Rec_date = data.RECDATE,
                            Follow_Up_dateTime = data.FOLLOWUPDATE,
                            SubjectLine = data.SUBJECTLINE,
                            EmailBody = data.EmailBody,
                            Note = data.NOTE,
                            PolicyId = data.POLICY_ID == null ? 0 : (int)data.POLICY_ID,
                            App_Id = data.APP_ID == null ? 0 : (int)data.APP_ID,
                            ReassignedDiary = (from u in _db.TODODIARYLISTs where u.LISTID == data.LISTID select u.ParentId).FirstOrDefault() != null ? "Yes" : data.ReassignedDiary,
                            ReassignedDiaryDate = data.ReassignedDiaryDate,
                            Uname = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u.UserId).FirstOrDefault(),
                            CreatedBy = data.CreatedBy,
                            CreatedOn = DateTime.Now,
                            Ugroup = (from u in _db.MNT_Users join g in _db.MNT_GroupsMaster on u.GroupId equals g.GroupId where u.UserId == data.CreatedBy select g.GroupCode).FirstOrDefault(),
                            AccidentId = data.AccidentId

                        }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }
        private static string[] FetchpermissionListFordashBoard()
        {
            MCASEntities _db = new MCASEntities();
            try
            {
                var model = new BaseModel();
                var createdby = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
                var list = (from l in _db.MNT_GroupPermission join p in _db.MNT_Users on l.GroupId equals SqlFunctions.StringConvert((double)p.GroupId).Trim() where (p.UserId == createdby) && (l.MenuId >= 281 && l.MenuId <= 292) && (l.Read == true) join k in _db.MNT_Menus on l.MenuId equals k.MenuId where k.IsActive == "Y" select l.MenuId);
                return list.ToArray().Select(x => x.ToString()).ToArray();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
        }
        public static List<TaskModel> FetchTaskList(string User, DiaryModel model = null)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<TaskModel>();
            TaskModel mod = new TaskModel();
            int i = 1;
            try
            {
                item = (from x in _db.CLM_ClaimTask join y in _db.ClaimAccidentDetails on x.AccidentClaimId equals y.AccidentClaimId orderby x.Id descending select new { x, y }).ToList().Select(b =>
                         new TaskModel()
                        {
                            SerialNo = i++,
                            Id = b.x.Id,
                            Claimno = b.y.ClaimNo + "/" + (from l in _db.CLM_Claims where l.AccidentClaimId == b.x.AccidentClaimId && l.ClaimID == b.x.ClaimID select l.ClaimRecordNo).FirstOrDefault(),
                            IPno = b.y.IPNo == null ? "" : b.y.IPNo,
                            ClaimantName = b.x.ClaimantNames == null ? "" : b.x.ClaimantNames == "0" ? "" : (from n in mod.getClaimantName(b.y.AccidentClaimId).ToList() where n.Id == b.x.ClaimantNames select n.Text).FirstOrDefault() == null ? "" : (from n in mod.getClaimantName(b.y.AccidentClaimId).ToList() where n.Id == b.x.ClaimantNames select n.Text).FirstOrDefault(),
                            ClaimantNames = b.x.ClaimantNames == null ? "" : b.x.ClaimantNames,
                            CreatedBy = b.x.CreatedBy,
                            TaskNo = b.x.TaskNo,
                            ActionDue = b.x.ActionDue,
                            HActionDue = b.x.ActionDue == null ? "" : b.x.ActionDue.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]),
                            CloseDate = b.x.CloseDate,
                            HcloseDate = b.x.CloseDate == null ? "" : b.x.CloseDate.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]),
                            ModifiedDate = b.x.ModifiedDate,
                            HModifiedDate = b.x.ModifiedDate == null ? "" : b.x.ModifiedDate.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]),
                            PromtDetails = b.x.PromtDetails,
                            AccidentClaimId = b.x.AccidentClaimId,
                            ClaimsOfficer = (from l in _db.MNT_Users where l.SNo == b.x.ClaimsOfficer select l.UserDispName).FirstOrDefault() == null ? "" : (from l in _db.MNT_Users where l.SNo == b.x.ClaimsOfficer select l.UserDispName).FirstOrDefault() == null ? "" : (from l in _db.MNT_Users where l.SNo == b.x.ClaimsOfficer select l.UserDispName).FirstOrDefault() == null ? "" : (from l in _db.MNT_Users where l.SNo == b.x.ClaimsOfficer select l.UserDispName).FirstOrDefault(),
                            ClaimID = b.x.ClaimID,
                            PolicyId = 0,
                            Cmode = (from l in _db.ClaimAccidentDetails where l.AccidentClaimId == b.x.AccidentClaimId select l.IsComplete).FirstOrDefault() == 2 ? "Adj" : ""
                        }).ToList();
                if (model.SaveCheck == "1")
                {
                    item = (from l in item select l).Where(
                        x => x.IPno.ToUpper().Trim().Contains(model.IPNo.ToUpper().Trim()) &&
                            x.ClaimantName.ToUpper().Trim().Contains(model.ClaimantName.ToUpper().Trim()) &&
                            x.ClaimsOfficer.ToUpper().Trim().Contains(model.AssignedTo.ToUpper().Trim())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }
        public static CommonUtilities.DatatableGrid FetchDairyAccToLoginuser(int draw, int start, int length, int sortColumn, string sortDirection, string IPNo, string ClaimantName, string AssignedTo, string ReassignedTo, string SaveCheck, string Searchval = "")
        {
            MCASEntities _db = new MCASEntities();
            List<DiaryModel> item = new List<DiaryModel>();
            List<CommonUtilities.DatatableGrid> result = new List<CommonUtilities.DatatableGrid>();
            List<string[]> resutarray = new List<string[]>();
            List<string[]> res = new List<string[]>();
            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            String LoginUserId = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
            String UDisplayName = (from l in _db.MNT_Users where l.UserId == LoginUserId select l.UserDispName).FirstOrDefault();
            int totalcount;
            try
            {

                string createdby = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]) ?? string.Empty;
                string roleCode = DiaryModel.GetRoleCode(createdby) ?? string.Empty;
                string ugroupcode = Convert.ToString((from u in _db.MNT_Users join g in _db.MNT_GroupsMaster on u.GroupId equals g.GroupId where u.UserId == createdby select g.GroupCode).FirstOrDefault()) ?? string.Empty;
                string ugroupid = Convert.ToString((from u in _db.MNT_Users join g in _db.MNT_GroupsMaster on u.GroupId equals g.GroupId where u.UserId == createdby select g.GroupId).FirstOrDefault()) ?? string.Empty;
                item = (from x in _db.TODODIARYLISTs
                        orderby x.LISTID descending
                        where x.IsActive.ToUpper() == "Y" && x.ReassignedDiary.ToLower() != "yes" 
                        select new DiaryModel()
                            {
                                ListId = (int)x.LISTID,
                                ClaimNO = (from acc in _db.ClaimAccidentDetails where acc.AccidentClaimId == x.AccidentId select acc.ClaimNo).FirstOrDefault(),
                                ClaimName = (from claim in _db.CLM_Claims where claim.AccidentClaimId == x.AccidentId && claim.ClaimID == x.CLAIMID select claim.ClaimantName).FirstOrDefault() ?? String.Empty,
                                ClaimId = (int)x.CLAIMID,
                                IPNo = (from acc in _db.ClaimAccidentDetails where acc.AccidentClaimId == x.AccidentId select acc.IPNo).FirstOrDefault() ?? String.Empty,

                                ListType = (from p in _db.MNT_Lookups where p.Lookupvalue == x.LISTTYPEID && p.Category == "AlertDesc" select p.Lookupdesc).FirstOrDefault() ?? String.Empty,

                                ToUserId = (x.ReassignedDiary == "Yes" ? (from reas in _db.Claim_ReAssignmentDairy join u in _db.MNT_Users on reas.DairyFromUser.Trim() equals SqlFunctions.StringConvert((double)u.SNo).Trim() where reas.DairyId.Trim() == System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.LISTID).Trim() && reas.TypeOfAssignment == "T" select u.UserDispName).FirstOrDefault() : (from u in _db.MNT_Users where u.SNo == (int)(x.TOUSERID) select u.UserDispName).FirstOrDefault() != null ? x.ReassignedDiary == "Yes" ? (from reas in _db.Claim_ReAssignmentDairy join u in _db.MNT_Users on reas.DairyFromUser.Trim() equals SqlFunctions.StringConvert((double)u.SNo).Trim() where reas.DairyId.Trim() == System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.LISTID).Trim() && reas.TypeOfAssignment == "T" select u.UserDispName).FirstOrDefault() : (from u in _db.MNT_Users where u.SNo == (int)(x.TOUSERID) select u.UserDispName).FirstOrDefault() : "") ?? String.Empty,
                                Uname= (from u in _db.MNT_Users where u.SNo == (int)(x.TOUSERID) select u.UserDispName).FirstOrDefault()??"",
                                StartTime = x.STARTTIME,
                                EndTime = x.ENDTIME,
                                EscalationTo = (from u in _db.MNT_Users where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)u.SNo).Trim() == x.EscalationTo select u).FirstOrDefault() != null ? (from u in _db.MNT_Users where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)u.SNo).Trim() == x.EscalationTo select u.UserDispName).FirstOrDefault() : "",
                                PolicyId = (int)(x.POLICY_ID),
                                CreatedBy = (from l in _db.MNT_Users where l.UserId == x.CreatedBy select l.UserDispName).FirstOrDefault() ?? "",
                                AccidentId = x.AccidentId,
                                Cmode = (from l in _db.ClaimAccidentDetails where l.AccidentClaimId == x.AccidentId select l.IsComplete).FirstOrDefault() == 2 ? "Adj" : "",
                                ReAssignToId = (x.ReassignedDiary == "Yes" ? (from reas in _db.Claim_ReAssignmentDairy join u in _db.MNT_Users on reas.ReAssignTo.Trim() equals SqlFunctions.StringConvert((double)u.SNo).Trim() where reas.DairyId.Trim() == System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.LISTID).Trim() && reas.TypeOfAssignment == "T" select u.UserDispName).FirstOrDefault() : "") ?? String.Empty,
                                RoleCode = roleCode,

                                UserSNo = x.ReassignedDiary == "Yes" ? (from reas in _db.Claim_ReAssignmentDairy
                                                                        join u in _db.MNT_Users on
                                                                            reas.ReAssignTo.Trim() equals SqlFunctions.StringConvert((double)u.SNo).Trim()
                                                                        where reas.DairyId.Trim() == System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.LISTID).Trim() && reas.TypeOfAssignment == "T"
                                                                        select u.SNo).FirstOrDefault() : 0,

                                UserSNo_ = x.ReassignedDiary == "Yes" ?
                                                                        (from reas in _db.Claim_ReAssignmentDairy join u in _db.MNT_Users on reas.DairyFromUser.Trim() equals SqlFunctions.StringConvert((double)u.SNo).Trim() where reas.DairyId.Trim() == System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.LISTID).Trim() && reas.TypeOfAssignment == "T" select u.SNo).FirstOrDefault()
                                                                        :
                                                                        (from u in _db.MNT_Users where u.SNo == (int)(x.TOUSERID) select u.UserDispName).FirstOrDefault() != null ? x.ReassignedDiary == "Yes" ? (from reas in _db.Claim_ReAssignmentDairy join u in _db.MNT_Users on reas.DairyFromUser.Trim() equals SqlFunctions.StringConvert((double)u.SNo).Trim() where reas.DairyId.Trim() == System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.LISTID).Trim() && reas.TypeOfAssignment == "T" select u.SNo).FirstOrDefault() : (from u in _db.MNT_Users where u.SNo == (int)(x.TOUSERID) select u.SNo).FirstOrDefault() : 0
                            }).ToList();

                if ((IPNo != "" || ClaimantName != "" || AssignedTo != "" || ReassignedTo != "") && SaveCheck != "Y")
                {
                    item = item.Where(
                            x => x.IPNo.ToUpper().Trim().Contains(IPNo.ToUpper().Trim()) &&
                            x.ClaimName.ToUpper().Trim().Contains(ClaimantName.ToUpper().Trim()) &&
                            x.ToUserId.ToUpper().Trim().Contains(AssignedTo.ToUpper().Trim()) &&
                            x.ReAssignToId.ToUpper().Trim().Contains(ReassignedTo.ToUpper().Trim())).ToList();
                }
                if (Searchval != "")
                {
                    item = item.Where(m => m.GetType().GetProperties().Any(x => x.GetValue(m, null) != null && x.GetValue(m, null).ToString().Contains(Searchval))).ToList();
                }

                totalcount = item.Count();
                if (sortColumn == 0 && sortDirection == "asc")
                {
                    item = item.GetRange(start, Math.Min(length, totalcount - start)).ToList();
                }
                else
                {
                    item = sortDirection == "asc" ? item.OrderBy(MenuListItem.ClaimTabs.GetColumnNameForAlertdashBoard(sortColumn)).ToList().GetRange(start, Math.Min(length, totalcount - start)).ToList() : item.OrderBy(MenuListItem.ClaimTabs.GetColumnNameForAlertdashBoard(sortColumn) + " DESC").ToList().GetRange(start, Math.Min(length, totalcount - start)).ToList();
                }

                res = (from bc in item select bc).ToList().Select(result1 =>
                                     new string[]
                    {
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (UDisplayName == result1.Uname || (result1.RoleCode == "COSP" || result1.RoleCode == "SP") ? "<input type='checkbox' class='chk' value='" + (result1.ListId) + "' name='DiaryToReassignment' accId='" + (result1.AccidentId) + "' claimId='" + (result1.ClaimId) + "' SNO='" + (result1.UserSNo == 0 ? result1.UserSNo_ : result1.UserSNo) + "' />" : "") + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.ClaimNO) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.IPNo) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.ListType) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.CreatedBy) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.StartTime) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.ToUserId) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.ReAssignToId) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + ">" + (result1.EscalationTo) + "</u>",
                        "<u class=" + (result1.EndTime.ToString() != "" ? "class1" : (Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) >= 0 && Math.Round((Convert.ToDateTime(result1.StartTime).Date - DateTime.Now.Date).TotalDays) <= 3) ? "class2" : "class3") + "><a class='Dashboard btn btn-xs btn-info' ScreenNameDash='208' screenid='CLM_REG' url=" + Url.ActionEncoded("DiaryTaskEditor", "ClaimProcessing", new { PolicyId = result1.PolicyId, AccidentClaimId = result1.AccidentId,claimMode = result1.Cmode, ClaimId=result1.ClaimId,mode=result1.Cmode,ListId= result1.ListId}) + " href='#'>" + (Common.View) + "</a></u>"
                    }).ToList();
                
                result.Add(new CommonUtilities.DatatableGrid() { data = res });
                result.FirstOrDefault().draw = draw;
                result.FirstOrDefault().recordsFiltered = totalcount;
                result.FirstOrDefault().recordsTotal = totalcount;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return result.FirstOrDefault();
        }
        private static List<DiaryModel> FetchReassignDairyAccToLoginuser(string userid)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DiaryModel>();
            try
            {
                var Diarylist = (from x in _db.TODODIARYLISTs join y in _db.MNT_Users on x.TOUSERID equals y.SNo join c in _db.ClaimAccidentDetails on x.AccidentId equals c.AccidentClaimId where y.UserId == userid && x.MovementType == "S" && x.IsActive == "Y" select x);
                if (Diarylist.Any())
                {
                    foreach (var data in Diarylist)
                    {
                        var Types = (from p in _db.MNT_Lookups where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)p.LookupID) == data.LISTTYPEID select p).FirstOrDefault();
                        var Tousers = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u).FirstOrDefault();
                        var model = new DiaryModel();
                        var createby = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
                        var Uname1 = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u.UserId).FirstOrDefault();
                        var ugroup1 = (from u in _db.MNT_Users join g in _db.MNT_GroupsMaster on u.GroupId equals g.GroupId where u.UserId == createby select g.GroupCode).FirstOrDefault();
                        var Fromusers = (from u in _db.MNT_Users where u.SNo == (int)(data.FROMUSERID) select u).FirstOrDefault();
                        var Esclationusers = (from u in _db.MNT_Users where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)u.SNo).Trim() == data.EscalationTo select u).FirstOrDefault();
                        item.Add(new DiaryModel()
                        {
                            ListId = Convert.ToInt32(data.LISTID),
                            ClaimId = (int)data.CLAIMID,
                            ListType = Types.Lookupdesc,
                            FromUserId = Fromusers != null ? Fromusers.UserDispName : "",
                            ToUserId = Tousers != null ? Tousers.UserDispName : "",
                            StartTime = data.STARTTIME,
                            EndTime = data.ENDTIME,
                            ReminderBeforeCompletion = data.ReminderBeforeCompletion,
                            EscalationTo = Esclationusers != null ? Esclationusers.UserDispName : "",
                            Rec_date = Convert.ToDateTime(data.RECDATE),
                            Follow_Up_dateTime = Convert.ToDateTime(data.FOLLOWUPDATE),
                            SubjectLine = data.SUBJECTLINE,
                            EmailBody = data.EmailBody,
                            Note = data.NOTE,
                            PolicyId = Convert.ToInt32(data.POLICY_ID),
                            App_Id = Convert.ToInt32(data.APP_ID),
                            ReassignedDiary = data.ReassignedDiary,
                            ReassignedDiaryDate = data.ReassignedDiaryDate,
                            Uname = Uname1,
                            CreatedBy = createby,
                            CreatedOn = DateTime.Now,
                            Ugroup = Convert.ToString(ugroup1),
                            AccidentId = data.AccidentId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }

        public static List<LookUpListItems> FetchAlertDescription(string category, int accclmid, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            var clmorgId = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == accclmid select l.Organization).FirstOrDefault();
            var lookupcodelist = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" orderby l.Lookupdesc select l.LookupID).ToList();
            foreach (decimal ids in lookupcodelist)
            {
                string org = (from l in obj.MNT_Lookups where l.LookupID == ids select l.lookupCode).FirstOrDefault();
                if (org != null)
                {
                    List<string> orglist = org.Split(',').ToList();
                    foreach (string s in orglist)
                    {
                        if (s == Convert.ToString(clmorgId))
                        {
                            var lookupinfo = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.LookupID == ids orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).FirstOrDefault();
                            list.Add(lookupinfo);
                        }
                    }
                }
            }

            //if (addAll)
            //{
                list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            //}
            //if (addNone)
            //{
            //    list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "(None)" });
            //}
            obj.Dispose();
            return list;
        }


        private static List<DiaryModel> FetchEscalationDairyAccToLoginuser(string userid)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DiaryModel>();
            try
            {
                var Diarylist = (from l in _db.Proc_GetEscallationListAccordingTouserId(userid) select l).ToList();
                if (Diarylist.Any())
                {
                    foreach (var data in Diarylist)
                    {
                        var Types = (from p in _db.MNT_Lookups where p.LookupID == data.LISTTYPEID select p).FirstOrDefault();
                        var Tousers = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u).FirstOrDefault();
                        var model = new DiaryModel();
                        var createby = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
                        var Uname1 = (from u in _db.MNT_Users where u.SNo == (int)(data.TOUSERID) select u.UserId).FirstOrDefault();
                        var ugroup1 = (from u in _db.MNT_Users join g in _db.MNT_GroupsMaster on u.GroupId equals g.GroupId where u.UserId == createby select g.GroupCode).FirstOrDefault();
                        var Fromusers = (from u in _db.MNT_Users where u.SNo == (int)(data.FROMUSERID) select u).FirstOrDefault();
                        var Esclationusers = (from u in _db.MNT_Users where System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)u.SNo).Trim() == data.EscalationTo select u).FirstOrDefault();
                        item.Add(new DiaryModel()
                        {
                            ListId = Convert.ToInt32(data.LISTID),
                            ClaimId = (int)data.CLAIMID,
                            ListType = Types.Lookupdesc,
                            FromUserId = Fromusers != null ? Fromusers.UserDispName : "",
                            ToUserId = Tousers != null ? Tousers.UserDispName : "",
                            StartTime = data.STARTTIME,
                            EndTime = data.ENDTIME,
                            ReminderBeforeCompletion = data.ReminderBeforeCompletion,
                            EscalationTo = Esclationusers != null ? Esclationusers.UserDispName : "",
                            Rec_date = Convert.ToDateTime(data.RECDATE),
                            Follow_Up_dateTime = Convert.ToDateTime(data.FOLLOWUPDATE),
                            SubjectLine = data.SUBJECTLINE,
                            EmailBody = data.EmailBody,
                            Note = data.NOTE,
                            PolicyId = Convert.ToInt32(data.POLICY_ID),
                            App_Id = Convert.ToInt32(data.APP_ID),
                            ReassignedDiary = data.ReassignedDiary,
                            ReassignedDiaryDate = data.ReassignedDiaryDate,
                            Uname = Uname1,
                            CreatedBy = createby,
                            CreatedOn = DateTime.Now,
                            Ugroup = Convert.ToString(ugroup1),
                            AccidentId = data.AccidentId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }
        public static List<MandateModelCOllection> FetchMandateListAccToLoginuser(DiaryModel model = null)
        {
            MCASEntities db = new MCASEntities();
            int i = 1;
            try
            {
                var result = (from l in db.Proc_GetClmMandateListAccordingToUserid()
                              orderby l.MandateId descending
                              select new MandateModelCOllection()
                              {
                                  SNo = i++,
                                  IpNo = (from clm in db.ClaimAccidentDetails where clm.AccidentClaimId == l.AccidentClaimId select clm.IPNo).FirstOrDefault() ?? "",
                                  AccidentClaimId = l.AccidentClaimId,
                                  PolicyId = l.PolicyId,
                                  ClaimRecordNo = l.ClaimRecordNo ?? "",
                                  ClaimantName = l.ClaimantName,
                                  ClaimType = l.ClaimType,
                                  Createddate = l.Createddate,
                                  Crdate = l.Createddate.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]),
                                  ModifiedDate = l.Modifieddate,
                                  Total_C = l.Total_C,
                                  Createdby = l.Createdby,
                                  ModifiedBy = l.Modifiedby,
                                  MandateId = l.MandateId,
                                  ClaimTypeDesc = l.ClaimTypeDesc,
                                  ClaimTypeCode = l.ClaimTypeCode,
                                  mode = l.mode,
                                  ClaimNo = l.ClaimNo,
                                  claimId = l.ClaimID,
                                  Requester = l.Createdby ?? "",
                                  Approver = (String.IsNullOrEmpty((from sum in db.CLM_MandateSummary where sum.MandateId == l.MandateId select sum.ApproveRecommedations).FirstOrDefault()) ? (from u in db.MNT_Users join m in db.CLM_MandateSummary on u.SNo equals m.AssignedTo where m.MandateId == l.MandateId select u.UserDispName).FirstOrDefault() : string.IsNullOrEmpty(l.Modifiedby) ? l.Createdby : l.Modifiedby) ?? "",
                                  Approved = ((from sum in db.CLM_MandateSummary where sum.MandateId == l.MandateId select sum.ApproveRecommedations).FirstOrDefault() == null ? "Pending" : (from sum in db.CLM_MandateSummary where sum.MandateId == l.MandateId select sum.ApproveRecommedations).FirstOrDefault() == "Y" ? "Yes" : "No") ?? "",

                                  ApprovalDate = ((from sum in db.CLM_MandateSummary where sum.MandateId == l.MandateId select sum.ApproveRecommedations).FirstOrDefault() == "Y" ? string.IsNullOrEmpty(l.Modifiedby) ? l.Createddate == null ? "" : l.Createddate.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]) : l.Modifieddate == null ? "" : l.Modifieddate.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]) : "") ?? ""
                              }).ToList();


                if (model.SaveCheck == "1")
                {
                    result = result.Where(
                        x => x.IpNo.ToUpper().Trim().Contains(model.IPNo.ToUpper().Trim()) &&
                            !string.IsNullOrEmpty(x.ClaimantName) &&
                            x.ClaimantName.ToUpper().Trim().Contains(model.ClaimantName.ToUpper().Trim()) &&
                            x.Requester.ToUpper().Trim().Contains(model.Requestor.ToUpper().Trim()) &&
                            x.Approver.ToUpper().Trim().Contains(model.Approver.ToUpper().Trim()) &&
                            x.Approved.ToUpper().Trim().Contains(model.Approved.ToUpper().Trim())
                            ).ToList();
                }


                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
        }
        private static List<ClaimPaymentModelCollectionDashBoard> FetchPaymentListAccToLoginuser(DiaryModel model = null)
        {
            MCASEntities db = new MCASEntities();
            var items = new List<ClaimPaymentModelCollectionDashBoard>();
            try
            {
                int i = 1;
                items = (from data in db.Proc_GetCLM_PaymentListForDashBoard()
                         orderby data.PaymentId descending
                         select new ClaimPaymentModelCollectionDashBoard()
                         {
                             SNo = i++,
                             IpNo = (from clm in db.ClaimAccidentDetails where clm.AccidentClaimId == data.AccidentClaimId select clm.IPNo).FirstOrDefault() ?? "",
                             RecordNumber = data.ClaimRecordNo,
                             ClaimID = data.ClaimID,
                             ClaimantName = data.ClaimantName ?? "",
                             ClaimType = data.ClaimType,
                             Createddate = data.Createddate,
                             ModifiedDate = data.Modifieddate,
                             AccidentClaimId = data.AccidentClaimId,
                             Createdby = data.Createdby,
                             ModifiedBy = data.Modifiedby,
                             PolicyId = data.PolicyId,
                             PaymentId = data.PaymentId,
                             Requester = data.Createdby ?? "",
                             Total_D = data.Total_D,
                             ClaimTypeCode = data.ClaimTypeCode,
                             ClaimTypeDesc = data.ClaimTypeDesc,
                             ApprovalDate =
                             (from sum in db.CLM_PaymentSummary where sum.PaymentId == data.PaymentId select sum.ApprovePayment).FirstOrDefault() == null
                             ? "" : data.ApprovedDate == null ? string.IsNullOrEmpty(data.Modifiedby) ? data.Createddate.Value.ToString("dd/MM/yyyy") : data.Modifieddate.Value.ToString("dd/MM/yyyy") : data.ApprovedDate,
                             mode = (from x in db.ClaimAccidentDetails where x.AccidentClaimId == data.AccidentClaimId select x.IsComplete).FirstOrDefault() == 2 ? "Adj" : "",
                             Approved = (from sum in db.CLM_PaymentSummary where sum.PaymentId == data.PaymentId select sum.ApprovePayment).FirstOrDefault() == null ? "Pending" : (from sum in db.CLM_PaymentSummary where sum.PaymentId == data.PaymentId select sum.ApprovePayment).FirstOrDefault() == "Y" ? "Yes" : "No",
                             Approver =
                             (from users in db.MNT_Users where users.SNo == data.AssignedTo select users.UserDispName).FirstOrDefault() == null ? "" : (from users in db.MNT_Users where users.SNo == data.AssignedTo select users.UserDispName).FirstOrDefault()
                         }
                           ).ToList();


                if (model.SaveCheck == "1")
                {
                    items = items.Where(
                        x => x.IpNo.ToUpper().Trim().Contains(model.IPNo.ToUpper().Trim()) &&
                            !string.IsNullOrEmpty(x.ClaimantName) && x.ClaimantName.ToUpper().Trim().Contains(model.ClaimantName.ToUpper().Trim()) &&
                            x.Requester.ToUpper().Trim().Contains(model.Requestor.ToUpper().Trim()) &&
                            x.Approver.ToUpper().Trim().Contains(model.Approver.ToUpper().Trim()) &&
                            x.Approved.ToUpper().Trim().Contains(model.Approved.ToUpper().Trim())
                            ).ToList();
                }


                return items;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
        }
        public static DiaryModel Fetchgrid(DiaryModel model)
        {
            try
            {
                model.TaskSelectionList = FetchTaskSelectionList();
                model.Values = ReAssignmentModel.Fetch();
                model.IPNo = model.IPNo == null ? "" : model.IPNo;
                model.ClaimantName = model.ClaimantName == null ? "" : model.ClaimantName;
                model.AssignedTo = model.AssignedTo == null ? "" : model.AssignedTo;
                model.ReassignedTo = model.ReassignedTo == null ? "" : model.ReassignedTo;
                model.Requestor = model.Requestor == null ? "" : model.Requestor;
                model.Approver = model.Approver == null ? "" : model.Approver;
                model.Approved = model.Approved == null ? "" : model.Approved;
                if (model.TaskSelection == "1")
                {
                    model.TaskCollection = FetchTaskList(Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]), model);
                }

                if (model.TaskSelection == "5")
                {
                    model.MandateList = FetchMandateListAccToLoginuser(model);
                }
                if (model.TaskSelection == "6")
                {
                    model.PaymentList = FetchPaymentListAccToLoginuser(model);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return model;
        }

        public static List<LookUpListItems> FetchTaskSelectionList()
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            try
            {
                string[] PermissionList = FetchpermissionListFordashBoard();
                list = (from l in obj.MNT_Lookups where l.Category == "TaskSelection" && l.IsActive == "Y" && PermissionList.Contains(l.lookupCode) select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).OrderBy(x => x.Lookup_value).ToList();
                list.Insert(0, new LookUpListItems() { Lookup_value = "0", Lookup_desc = "[Select...]" });
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }
            return list;
        }

        public static DiaryModel FetchDiaryTaskEditor(DiaryModel objmodel, TODODIARYLIST Diarylist, int? ClaimId, int? ListId, int Accidentid)
        {
            try
            {
                objmodel.ListId = (int)Diarylist.LISTID;
                objmodel.ClaimId = (int)Diarylist.CLAIMID;
                objmodel.PolicyId = (int)Diarylist.POLICY_ID;
                objmodel.ListTypeID = Convert.ToString(Diarylist.LISTTYPEID);
                objmodel.ToUserId = Convert.ToString(Diarylist.TOUSERID);
                objmodel.FromUserId = Convert.ToString(Diarylist.FROMUSERID);
                objmodel.StartTime = Diarylist.STARTTIME;
                objmodel.EndTime = Diarylist.ENDTIME;
                objmodel.ReminderBeforeCompletion = Diarylist.ReminderBeforeCompletion;
                objmodel.Escalation = Diarylist.Escalation;
                objmodel.EscalationTo = Diarylist.EscalationTo;
                objmodel.SubjectLine = Diarylist.SUBJECTLINE;
                objmodel.EmailBody = Diarylist.EmailBody;
                objmodel.Note = Diarylist.NOTE;
                objmodel.AccidentId = Diarylist.AccidentId;
                objmodel.hiddenprop = "1";
                if (ClaimId.HasValue)
                    objmodel.ClaimId = (int)ClaimId;
                if (ListId.HasValue)
                    objmodel.ListId = ListId;
                objmodel.Escalationlist = ReAssignmentModel.FetchEscalationList();
                objmodel.Values = ReAssignmentModel.Fetch();
                objmodel.DairyList = DiaryModel.Fetch(Accidentid);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return objmodel;
        }

        #endregion

        public DiaryModel Update()
        {
            MCASEntities obj = new MCASEntities();
            try
            {
                TODODIARYLIST dairyInfo;
                var userId = HttpContext.Current.Session["LoggedInUserId"].ToString();
                var uid = (from l in obj.MNT_Users where l.UserId == userId select l.SNo).FirstOrDefault();
                if (ListId.HasValue)
                {
                    dairyInfo = obj.TODODIARYLISTs.Where(x => x.LISTID == this.ListId.Value).FirstOrDefault();
                    dairyInfo.UserId = this.UserIds;
                    dairyInfo.TOUSERID = Convert.ToInt16(this.ToUserId);
                    dairyInfo.FROMUSERID = Convert.ToInt64(uid);
                    dairyInfo.CLAIMID = this.ClaimId;
                    dairyInfo.POLICY_ID = this.PolicyId;
                    dairyInfo.CLAIMNO = this.ClaimName;
                    dairyInfo.LISTTYPEID = this.ListTypeID;
                    dairyInfo.FOLLOWUPDATE = this.Follow_Up_dateTime;
                    dairyInfo.ExpectedPaymentDate = this.ExpectedPaymentDate;
                    dairyInfo.STARTTIME = this.StartTime;
                    dairyInfo.ENDTIME = this.EndTime;
                    dairyInfo.ReminderBeforeCompletion = this.ReminderBeforeCompletion;
                    dairyInfo.Escalation = this.Escalation;
                    dairyInfo.EscalationTo = this.EscalationTo;
                    dairyInfo.SUBJECTLINE = this.SubjectLine;
                    dairyInfo.EmailBody = this.EmailBody;
                    dairyInfo.NOTE = this.Note;
                    dairyInfo.AccidentId = this.AccidentId;
                    dairyInfo.RECDATE = DateTime.Now;
                    dairyInfo.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    dairyInfo.ModifiedDate = DateTime.Now;
                    dairyInfo.LISTOPEN = dairyInfo.LISTOPEN != null ? null : this.ListOpen;
                    obj.SaveChanges();
                    this.ListId = Convert.ToInt32(dairyInfo.LISTID);
                    this.CreatedBy = dairyInfo.CreatedBy;
                    this.CreatedOn = Convert.ToDateTime(dairyInfo.CreatedDate);
                    if (dairyInfo.ModifiedDate != null)
                    {
                        this.ModifiedOn = dairyInfo.ModifiedDate;
                        this.ModifiedBy = dairyInfo.ModifiedBy;
                    }

                    return this;
                }
                else
                {

                    dairyInfo = new TODODIARYLIST();
                    dairyInfo.UserId = this.UserIds;
                    dairyInfo.TOUSERID = Convert.ToInt16(this.ToUserId);
                    dairyInfo.FROMUSERID = Convert.ToInt64(uid);
                    dairyInfo.CLAIMID = this.ClaimId;
                    dairyInfo.POLICY_ID = this.PolicyId;
                    dairyInfo.CLAIMNO = this.ClaimName;
                    dairyInfo.LISTTYPEID = this.ListTypeID;
                    dairyInfo.FOLLOWUPDATE = this.Follow_Up_dateTime;
                    dairyInfo.ExpectedPaymentDate = this.ExpectedPaymentDate;
                    dairyInfo.STARTTIME = this.StartTime;
                    dairyInfo.ENDTIME = this.EndTime;
                    dairyInfo.ReminderBeforeCompletion = this.ReminderBeforeCompletion;
                    dairyInfo.Escalation = this.Escalation;
                    dairyInfo.EscalationTo = this.EscalationTo;
                    dairyInfo.SUBJECTLINE = this.SubjectLine;
                    dairyInfo.EmailBody = this.EmailBody;
                    dairyInfo.NOTE = this.Note;
                    dairyInfo.RECDATE = DateTime.Now;
                    //dairyInfo.CreatedBy = this.CreatedBy;
                    dairyInfo.CreatedDate = DateTime.Now;
                    dairyInfo.AccidentId = this.AccidentId;
                    dairyInfo.ReassignedDiary = "No";
                    dairyInfo.MovementType = "I";
                    dairyInfo.ParentId = null;
                    dairyInfo.IsActive = "Y";
                    dairyInfo.CreatedDate = DateTime.Now;
                    dairyInfo.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    dairyInfo.LISTOPEN = dairyInfo.LISTOPEN != null ? null : this.ListOpen;
                    obj.TODODIARYLISTs.AddObject(dairyInfo);
                    obj.SaveChanges();
                    this.ListId = (int)dairyInfo.LISTID;
                    this.ClaimId = (int)dairyInfo.CLAIMID;
                    this.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    this.CreatedOn = DateTime.Now;
                    return this;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }
        }
        public DiaryModel DiaryUpdate()
        {

            TODODIARYLIST todo = new TODODIARYLIST();
            if (ListId.HasValue)
            {
                todo = _db.TODODIARYLISTs.Where(x => x.LISTID == this.ListId).FirstOrDefault();
                todo.TOUSERID = Convert.ToDecimal(this.ToUserId);
                todo.RECDATE = DateTime.Now;
                todo.LISTTYPEID = this.ListTypeID;

                todo.CLAIMID = this.ClaimId;
                todo.CLAIMNO = this.ClaimName;

                todo.CUSTOMER_ID = this.InsurerId;
                todo.INSURERNAME = this.InsurerName;

                todo.POLICY_ID = this.PolicyId;
                todo.POLICYNO = this.PolicyName;

                todo.FOLLOWUPDATE = DateTime.Now;
                if (this.priority == null)
                {
                    todo.PRIORITY = null;
                }
                else
                {
                    if (this.priority == "H")
                    {
                        todo.PRIORITY = "H";
                    }
                    else if (this.priority == "M")
                    {
                        todo.PRIORITY = "M";
                    }
                    else
                    {
                        todo.PRIORITY = "L";
                    }
                }
                todo.SUBJECTLINE = this.SubjectLine;
                todo.SYSTEMFOLLOWUPID = this.NotificationLists;
                todo.NOTE = this.Note;
                _db.SaveChanges();
                this.ListId = Convert.ToInt32(todo.LISTID);
                return this;
            }
            else
            {
                todo.TOUSERID = Convert.ToDecimal(this.ToUserId);
                todo.RECDATE = DateTime.Now;
                todo.FROMUSERID = 398;

                todo.CLAIMID = this.ClaimId;
                todo.CLAIMNO = this.ClaimName;

                todo.CUSTOMER_ID = this.InsurerId;
                todo.INSURERNAME = this.InsurerName;

                todo.POLICY_ID = this.PolicyId;
                todo.POLICYNO = this.PolicyName;

                todo.LISTTYPEID = this.ListTypeID;



                todo.FOLLOWUPDATE = DateTime.Now;
                todo.SYSTEMFOLLOWUPID = this.NotificationLists;
                todo.SUBJECTLINE = this.SubjectLine;
                todo.NOTE = this.Note;
                _db.TODODIARYLISTs.AddObject(todo);
                _db.SaveChanges();
                this.ListId = Convert.ToInt32(todo.LISTID);
                return this;
            }
        }
        #endregion
        #endregion

        public static string GetRoleCode(string LoggedInUserId)
        {
            MCASEntities _db = new MCASEntities();
            var GroupId = (from t in _db.MNT_Users where t.UserId == LoggedInUserId select t.GroupId).FirstOrDefault();
            var groupId = Int32.Parse(GroupId.ToString());
            var RoleCode = (from t in _db.MNT_GroupsMaster where t.GroupId == groupId select t.RoleCode).FirstOrDefault();
            return RoleCode;
        }



        public List<LookUpListItems> GetApprovedList()
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            list.Insert(1, new LookUpListItems() { Lookup_value = "Pending", Lookup_desc = "Pending" });
            list.Insert(2, new LookUpListItems() { Lookup_value = "Yes", Lookup_desc = "Yes" });
            list.Insert(3, new LookUpListItems() { Lookup_value = "No", Lookup_desc = "No" });
            obj.Dispose();
            return list;
        }

        public static object GetUserDispName(string p)
        {
            MCASEntities _db = new MCASEntities();
            try
            {
                return (from u in _db.MNT_Users where u.UserId == p select u.UserDispName).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }

        }

        public static string GetEncryptedURl(string action, string controller, string RouteString)
        {
            UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return Url.Action(action, controller, new {Q=RouteString });
        }
    }


    public class DiarySetupModel : BaseModel
    {
        MCASEntities _db = new MCASEntities();

        #region Properties

        public int? ID { get; set; }

        [Required(ErrorMessage = "Subject line is Required. ")]
        public string SubjectLine { get; set; }

        public string priority { get; set; }
        public List<LookUpListItems> Prioritylist { get; set; }




        // public int ModuleId { get; set; }

        public int ModuleId = 5;
        public string ModuleName { get; set; }

        public int DiaryTypeId { get; set; }

        //  public int DiaryTypeId = 34;

        public int TypeId { get; set; }
        public string TypeDescription { get; set; }

        public int ProductCode = 57;
        public string IsActive = "Y";
        public string ProductDisplayName { get; set; }
        public List<ProductsListItems> ProductList { get; set; }

        public decimal? FollowUp { get; set; }

        ////The Values of selected users
        //public string[] SelectedUsers { get; set; }
        ////The list of all users
        //public IEnumerable<SelectListItem> AllUsers { get; set; }

        public IEnumerable<string> SelectedUsers { set; get; }

        public List<SelectListItem> users { set; get; }




        #endregion

        #region static Methods
        public static List<DiarySetupModel> fetchDiarySetUp()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DiarySetupModel>();
            var DiarySetUplist = (from x in _db.MNT_DIARY_DETAILS select x);
            if (DiarySetUplist.Any())
            {
                foreach (var data in DiarySetUplist)
                {
                    var prodCode = (from p in _db.MNT_Products where p.ProductId == data.MDD_LOB_ID select p.ProductDisplayName).FirstOrDefault();
                    //var users = (from p in _db.MNT_Users where p.SNo == Convert.ToInt32(data.MDD_USERLIST_ID) select p.UserDispName).FirstOrDefault();
                    //var groups = (from p in _db.MNT_GroupsMaster where p.GroupId == Convert.ToInt32(data.MDD_USERGROUP_ID) select p.GroupName).FirstOrDefault();
                    //   item.Add(new DiarySetupModel() { ID = data.ID, SubjectLine = data.MDD_SUBJECTLINE, SelectedUserList = data.MDD_USERLIST_ID,  SelectedUserGroupList = data.MDD_USERGROUP_ID });

                }
            }
            return item;

        }


        #endregion

        #region Methods

        // public MultiSelectList(IEnumerable items,string dataValueField,string dataTextField,IEnumerable selectedValues) {}




        public DiarySetupModel DiarySetUpUpdate()
        {
            MCASEntities _db = new MCASEntities();
            MNT_DIARY_DETAILS diarysetup = new MNT_DIARY_DETAILS();
            if (ID.HasValue)
            {
                //diarysetup.MDD_MODULE_ID = 5;
                //diarysetup.MDD_DIARYTYPE_ID = 18;
                //diarysetup.MDD_LOB_ID = 57;
                //diarysetup.MDD_USERGROUP_ID = this.SelectedUserGroupList;
                //diarysetup.MDD_USERLIST_ID = this.SelectedUserList;
                //diarysetup.MDD_SUBJECTLINE = this.SubjectLine;
                //diarysetup.MDD_PRIORITY = this.priority;
                //diarysetup.MDD_FOLLOWUP = this.FollowUp;
                //diarysetup.MDD_IS_ACTIVE = "Y";
                //diarysetup.MDD_LAST_UPDATED_DATETIME = DateTime.Now;
                _db.SaveChanges();
                return this;
            }
            else
            {
                //diarysetup.MDD_MODULE_ID = 5;
                //diarysetup.MDD_DIARYTYPE_ID = 18;
                //diarysetup.MDD_LOB_ID = 57;
                //diarysetup.MDD_USERGROUP_ID =Convert.ToString(this.SelectedGroupId);
                //diarysetup.MDD_USERLIST_ID =Convert.ToString(this.SelecteduserId);
                //diarysetup.MDD_SUBJECTLINE = this.SubjectLine;
                //diarysetup.MDD_PRIORITY = this.priority;
                //diarysetup.MDD_FOLLOWUP =this.FollowUp;
                //diarysetup.MDD_IS_ACTIVE = "Y";
                //diarysetup.MDD_CREATED_DATETIME = DateTime.Now;
                _db.MNT_DIARY_DETAILS.AddObject(diarysetup);
                _db.SaveChanges();
                this.ID = diarysetup.ID;
                return this;

            }

        }

        #endregion
    }

    public class DiaryViewModel
    {
        #region Properties
        public bool Selected { get; set; }
        public int ListId { get; set; }
        public string Note { get; set; }
        public string SubjectLine { get; set; }
        #endregion

        #region Methods
        public static List<DiaryViewModel> FetchDiary()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DiaryViewModel>();
            var Diarylist = (from x in _db.TODODIARYLISTs select x).Take(10);
            if (Diarylist.Any())
            {
                foreach (var data in Diarylist)
                {
                    //var prodCode = (from p in _db.MNT_Products where p.ProductId == data.ProductId select p).FirstOrDefault();
                    // var cedantCode = (from c in _db.MNT_Cedant where c.CedantId == data.CedantId select c).FirstOrDefault();
                    // var subclassCode = (from s in _db.MNT_ProductClass where s.ID == data.SubClassId select s).FirstOrDefault();
                    // var claimdetail = (from cd in _db.ClaimAccidentDetails where cd.PolicyId == data.PolicyId select cd).FirstOrDefault();
                    // item.Add(new InsuranceModel() { PolicyId = data.PolicyId, ProductId=data.ProductId, ProductCode = prodCode.ProductCode, ProductName = prodCode.ProductDisplayName, CedantCode= cedantCode.CedantCode,CedantName = cedantCode.CedantName, PolicyNo = data.PolicyNo, ClassCode= subclassCode.ClassCode,ClassDescription= subclassCode.ClassDesc,ClaimNo = claimdetail.ClaimNo,VehicleNo = claimdetail.VehicleNo,DriverName = claimdetail.DriverName, PremiumAmount = Convert.ToDouble(data.PremiumAmount), Deductible=Convert.ToDouble(data.Deductible),PolicyEffectiveTo=Convert.ToDateTime(data.PolicyEffectiveTo), PolicyEffectiveFrom = Convert.ToDateTime(data.PolicyEffectiveFrom) });
                    // }


                    item.Add(new DiaryViewModel() { ListId = Convert.ToInt32(data.LISTID), SubjectLine = data.SUBJECTLINE, Note = data.NOTE });
                }
            }
            return item;
        }

        #endregion
    }
    public class SelectedDiaryViewModel
    {
        #region Properties
        public bool Selected { get; set; }
        public int ListId { get; set; }
        public string Note { get; set; }
        public string SubjectLine { get; set; }
        #endregion

        #region Methods

        //public static List<DiaryViewModel> FetchDiary()
        //{
        //    MCASEntities _db = new MCASEntities();
        //    var item = new List<DiaryViewModel>();
        //    var Diarylist = (from x in _db.TODODIARYLISTs select x).Take(20);
        //    if (Diarylist.Any())
        //    {
        //        foreach (var data in Diarylist)
        //        {
        //            //var prodCode = (from p in _db.MNT_Products where p.ProductId == data.ProductId select p).FirstOrDefault();
        //            // var cedantCode = (from c in _db.MNT_Cedant where c.CedantId == data.CedantId select c).FirstOrDefault();
        //            // var subclassCode = (from s in _db.MNT_ProductClass where s.ID == data.SubClassId select s).FirstOrDefault();
        //            // var claimdetail = (from cd in _db.ClaimAccidentDetails where cd.PolicyId == data.PolicyId select cd).FirstOrDefault();
        //            // item.Add(new InsuranceModel() { PolicyId = data.PolicyId, ProductId=data.ProductId, ProductCode = prodCode.ProductCode, ProductName = prodCode.ProductDisplayName, CedantCode= cedantCode.CedantCode,CedantName = cedantCode.CedantName, PolicyNo = data.PolicyNo, ClassCode= subclassCode.ClassCode,ClassDescription= subclassCode.ClassDesc,ClaimNo = claimdetail.ClaimNo,VehicleNo = claimdetail.VehicleNo,DriverName = claimdetail.DriverName, PremiumAmount = Convert.ToDouble(data.PremiumAmount), Deductible=Convert.ToDouble(data.Deductible),PolicyEffectiveTo=Convert.ToDateTime(data.PolicyEffectiveTo), PolicyEffectiveFrom = Convert.ToDateTime(data.PolicyEffectiveFrom) });
        //            // }


        //            item.Add(new DiaryViewModel() { ListId =Convert.ToInt32(data.LISTID), SubjectLine = data.SUBJECTLINE, Note = data.NOTE });
        //        }
        //    }
        //    return item;
        //}
        #endregion
    }

    public class DiarySelectionViewModel
    {
        public List<SelectedDiaryViewModel> Diary { get; set; }
        public DiarySelectionViewModel()
        {
            this.Diary = new List<SelectedDiaryViewModel>();
        }
        public IEnumerable<int> getSelectedIds()
        {
            // Return an Enumerable containing the Id's of the selected people:
            return (from p in this.Diary where p.Selected select p.ListId).ToList();
        }
    }

    public class EsclationStatus
    {
        public String ID { get; set; }
        public String Name { get; set; }
    }

    public class MandateModelCOllection
    {
        public Int64? SNo { get; set; }
        public int AccidentClaimId { get; set; }
        public int PolicyId { get; set; }
        public string IpNo { get; set; }
        public string ClaimRecordNo { get; set; }
        public string ClaimantName { get; set; }
        public int? ClaimType { get; set; }
        public DateTime? Createddate { get; set; }
        public string Crdate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? Total_C { get; set; }
        public string Createdby { get; set; }
        public string ModifiedBy { get; set; }
        public int MandateId { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public string mode { get; set; }
        public string ClaimNo { get; set; }
        public string Requester { get; set; }
        public string Approver { get; set; }
        public string Approved { get; set; }
        public string ApprovalDate { get; set; }

        public int? claimId { get; set; }
    }
    public class ClaimPaymentModelCollectionDashBoard
    {
        #region "Object Properties"
        public Int64? SNo { get; set; }
        public int? ClaimID { get; set; }
        public int? ClaimType { get; set; }
        public int? PaymentId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public string ApprovePayment { get; set; }
        public string ClaimantName { get; set; }
        public string RecordNumber { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public string Payee { get; set; }
        public Decimal? Total_D { get; set; }
        public string ApprovedDate { get; set; }
        public string mode { get; set; }
        public DateTime? Createddate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Createdby { get; set; }
        public string ModifiedBy { get; set; }
        public string IpNo { get; set; }
        public string Requester { get; set; }
        public string Approver { get; set; }
        public string Approved { get; set; }
        public string ApprovalDate { get; set; }
        #endregion
    }
    public class TaskCollection : BaseModel
    {
        #region "Object Properties"
        public int? Id { get; set; }
        public int? TaskNo { get; set; }
        public DateTime? ActionDue { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Accidentclaimid { get; set; }
        public int? ClaimId { get; set; }
        public string PromtDetails { get; set; }
        public List<string> HeaderListCollection { get; set; }
        #endregion
    }
}
