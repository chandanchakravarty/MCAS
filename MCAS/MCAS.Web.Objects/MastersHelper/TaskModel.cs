using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using System.ComponentModel;
using System.Web;

namespace MCAS.Web.Objects.MastersHelper
{
    public class TaskModel : BaseModel
    {
        #region properties

        private DateTime? _closeDate = null;
        private DateTime? _approveDate = null;
        private DateTime? _modifiedDate = null;
        private DateTime? _createdDate = null;
        private DateTime? _actionDue = null;
        private DateTime? _taskmodifiedDate = null;

        public int Id { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.TaskEditor), ErrorMessageResourceName = "RFVTaskNumber")]
        [DisplayName("Task Number")]
        public int? TaskNo { get; set; }


        public int? ClaimID { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.TaskEditor), ErrorMessageResourceName = "RFVActionDue")]
        [DisplayName("Action Due")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ActionDue
        {
            get { return _actionDue; }
            set { _actionDue = value; }
        }
        [DisplayName("Close Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CloseDate
        {
            get { return _closeDate; }
            set { _closeDate = value; }
        }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.TaskEditor), ErrorMessageResourceName = "RFVPromptDetails")]
        [DisplayName("Prompt Details")]
        public string PromtDetails { get; set; }
        public string Cmode { get; set; }
        public string Claimno { get; set; }
        public string IPno { get; set; }
        public string Sno { get; set; }
        public int SerialNo { get; set; }
        public string ResultMessage { get; set; }
        public string Message { get; set; }
        public string isApprove { get; set; }

        [DisplayName("Approve Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApproveDate
        {
            get { return _approveDate; }
            set { _approveDate = value; }
        }

        public string ApproveBy { get; set; }


        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        [DisplayName("Modified Date")]
        public DateTime? TaskModifiedDate
        {
            get { return _taskmodifiedDate; }
            set { _taskmodifiedDate = value; }
        }

        [DisplayName("Modified Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public string Remarks { get; set; }
        public string HcloseDate { get; set; }
        public string HActionDue { get; set; }
        public string HModifiedDate { get; set; }
        public int? HchkhasClaim { get; set; }
        [DisplayName("Claims Officer")]
        public string ClaimsOfficer { get; set; }
        public string ClaimantName { get; set; }
        public string ClaimantNames { get; set; }
        public List<ClaimOfficerModel> ClaimOfficerList { get; set; }
        public List<ClaimantNameListSelect> ClaimantNameList { get; set; }
        public List<LookUpListItems> PromtDetails_List { get; set; }
        public List<TaskModel> TaskIndexList { get; set; }

        public override string screenId
        {
            get
            {
                return "134";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "134";
            }

        }
        #endregion

        public static List<TaskModel> Fetch(int id)
        {
            MCASEntities obj = new MCASEntities();
            TaskModel task = new TaskModel();
            var item = new List<TaskModel>();
            var userList = obj.Proc_GetTaskList(id).ToList();

            if (userList.Any())
            {
                foreach (var data in userList)
                {
                    var taskclaimid = Convert.ToString((from l in obj.CLM_ClaimTask where l.Id == data.Id select l.ClaimantNames).FirstOrDefault());
                    var pd = (from l in obj.MNT_Lookups where l.Lookupvalue == data.PromtDetails && l.Category == "TASKTYPE" select l.Lookupdesc).FirstOrDefault();
                    var cmode = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == data.AccidentClaimId select l.IsComplete).FirstOrDefault() == 2 ? "Adj" : "";
                    if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                    {
                        item.Add(new TaskModel()
                        {
                            Id = data.Id,
                            TaskNo = data.TaskNo,
                            ActionDue = data.ActionDue,
                            CloseDate = data.CloseDate,
                            TaskModifiedDate = data.TaskModifiedDate,
                            PromtDetails = pd,
                            AccidentClaimId = data.AccidentClaimId,
                            PolicyId = 0,
                            ClaimID = data.ClaimID,
                            Cmode = cmode,
                            ClaimantNames = taskclaimid == "0" ? "" : (from l in task.getTPVehicleNo(id) where l.Id == taskclaimid select l.Text).FirstOrDefault(),
                            ClaimsOfficer = data.ClaimOfficer,
                        });
                    }
                    else
                    {
                        item.Add(new TaskModel()
                        {
                            Id = data.Id,
                            TaskNo = data.TaskNo,
                            ActionDue = data.ActionDue,
                            CloseDate = data.CloseDate,
                            TaskModifiedDate = data.TaskModifiedDate,
                            PromtDetails = pd,
                            AccidentClaimId = data.AccidentClaimId,
                            PolicyId = 0,
                            ClaimID = data.ClaimID,
                            Cmode = cmode,
                            ClaimantNames = taskclaimid == "0" ? "" : (from l in task.getClaimantName(id) where l.Id == taskclaimid select l.Text).FirstOrDefault(),
                            ClaimsOfficer = data.ClaimOfficer,
                        });
                    }
                }
            }
            obj.Dispose();
            return item;
        }

        public List<ClaimOfficerModel> FetchClaimOfficer(string roleCode)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimOfficerModel>();
            var ClaimOfficerDetails = _db.Proc_GetClaimOfficerByRole(roleCode).ToList();
            if (ClaimOfficerDetails.Any())
            {
                item = (from n in ClaimOfficerDetails
                        select new ClaimOfficerModel()
                        {
                            TranId = n.SNo,
                            ClaimOfficerName = n.UserDispName
                        }
                        ).ToList();
            }
            item.Insert(0, new ClaimOfficerModel() { TranId = 0, ClaimOfficerName = "[Select...]" });
            _db.Dispose();
            return item;
        }
        public List<ClaimantNameListSelect> getClaimantName(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var item = new List<ClaimantNameListSelect>();
            var Attachmentlist = db.Proc_GetClaimantName(AccidentClaimId).ToList();
            if (Attachmentlist.Any())
            {
                foreach (var data in Attachmentlist)
                {
                    item.Add(new ClaimantNameListSelect()
                    {
                        Id = Convert.ToString(data.ClaimID),
                        Text = string.IsNullOrEmpty(data.ClaimantName) ? "" : data.ClaimantName
                    });
                }
            }
            item.Insert(0, new ClaimantNameListSelect() { Id = "0", Text = "[Select...]" });
            return item;
        }
        public List<ClaimantNameListSelect> getTPVehicleNo(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var item = new List<ClaimantNameListSelect>();
            var Attachmentlist = db.Proc_GetClaimTPVehicleNo(AccidentClaimId).ToList();
            if (Attachmentlist.Any())
            {
                foreach (var data in Attachmentlist)
                {
                    item.Add(new ClaimantNameListSelect()
                    {
                        Id = Convert.ToString(data.ClaimId),
                        Text = string.IsNullOrEmpty(data.TPVehicleNo) ? "" : data.TPVehicleNo
                    });
                }
            }
            item.Insert(0, new ClaimantNameListSelect() { Id = "", Text = "[Select...]" });
            return item;
        }
        public static TaskModel FetchTask(TaskModel TaskEditor, CLM_ClaimTask tasklist, int? Id)
        {
            MCASEntities db = new MCASEntities();
            var CoId = (from l in db.CLM_ClaimTask where l.Id == Id select l.ClaimsOfficer).SingleOrDefault();
            if (CoId == null)
            {
                CoId = 0;
            }
            TaskEditor.TaskNo = tasklist.TaskNo;
            TaskEditor.ActionDue = tasklist.ActionDue;
            TaskEditor.CloseDate = tasklist.CloseDate;
            TaskEditor.ModifiedDate = tasklist.ModifiedDate;
            TaskEditor.TaskModifiedDate = tasklist.TaskModifiedDate;
            TaskEditor.PromtDetails = tasklist.PromtDetails;
            TaskEditor.Remarks = tasklist.Remarks;
            TaskEditor.ClaimID = (int)tasklist.ClaimID;
            TaskEditor.ClaimantNames = tasklist.ClaimantNames;
            TaskEditor.ClaimsOfficer = CoId.ToString();
            TaskEditor.CreatedBy = tasklist.CreatedBy;
            TaskEditor.CreatedOn = Convert.ToDateTime(tasklist.CreatedDate);
            if (tasklist.ModifiedDate != null)
            {
                TaskEditor.ModifiedOn = tasklist.ModifiedDate;
                TaskEditor.ModifiedBy = tasklist.ModifiedBy;
            }
            TaskEditor.Id = (int)Id;
            return TaskEditor;
        }
        public class ClaimantNameListSelect
        {
            public String Id { get; set; }
            public String Text { get; set; }
        }

        public TaskModel Update()
        {
            MCASEntities obj = new MCASEntities();
            CLM_ClaimTask ClaimTaskinfo;
            try
            {
                if (this.Id == 0)
                {
                    ClaimTaskinfo = new CLM_ClaimTask();

                    DataMapper.Map(this, ClaimTaskinfo, true);
                    ClaimTaskinfo.ClaimsOfficer = Convert.ToInt32(this.ClaimsOfficer);
                    ClaimTaskinfo.CreatedBy = System.Web.HttpContext.Current.Session["LoggedInUserName"].ToString();
                    ClaimTaskinfo.CreatedDate = DateTime.Now;
                    obj.CLM_ClaimTask.AddObject(ClaimTaskinfo);
                    obj.SaveChanges();
                    this.TaskNo = ClaimTaskinfo.TaskNo;
                    this.CreatedOn = DateTime.Now;
                    this.CreatedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    this.Id = ClaimTaskinfo.Id;
                    this.ResultMessage = "Records Saved Successfully.";
                }
                else
                {

                    ClaimTaskinfo = obj.CLM_ClaimTask.Where(x => x.Id == this.Id).FirstOrDefault();
                    string[] IgnoreVlaue = { "CreatedBy", "CreatedDate" };
                    DataMapper.Map(this, ClaimTaskinfo, true, IgnoreVlaue);
                    ClaimTaskinfo.ClaimsOfficer = Convert.ToInt32(this.ClaimsOfficer);
                    ClaimTaskinfo.ModifiedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    ClaimTaskinfo.ModifiedDate = DateTime.Now;
                    obj.SaveChanges();
                    this.TaskNo = ClaimTaskinfo.TaskNo;
                    this.CreatedBy = ClaimTaskinfo.CreatedBy;
                    this.CreatedOn = Convert.ToDateTime(ClaimTaskinfo.CreatedDate);
                    this.ModifiedOn = DateTime.Now;
                    this.ModifiedBy = System.Web.HttpContext.Current.Session["LoggedInUserName"].ToString();
                    this.ResultMessage = "Records Updated Successfully.";
                }
                obj.Dispose();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return this;
        }


        public static TaskModel FetchTaskModel(TaskModel TaskEditor, int id,int?Id)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                TaskEditor.ClaimOfficerList = TaskEditor.FetchClaimOfficer("CO");
                if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                {
                    TaskEditor.ClaimantNameList = TaskEditor.getTPVehicleNo(Convert.ToInt32(id));
                }
                else
                {
                    TaskEditor.ClaimantNameList = TaskEditor.getClaimantName(id);
                }
                TaskEditor.ReadOnly = id == 0 ? true : false;
                TaskEditor.TaskIndexList = TaskModel.Fetch(id);
                if (Id.HasValue)
                {
                    var tasklist = (from tp in db.CLM_ClaimTask where tp.Id == Id select tp).FirstOrDefault();
                    TaskEditor = TaskModel.FetchTask(TaskEditor, tasklist, Id);
                    TaskEditor.Message = "Display";
                }
                else
                {
                    TaskEditor.TaskNo = (from t in db.CLM_ClaimTask where t.AccidentClaimId == id orderby t.Id descending select t.TaskNo).FirstOrDefault() == null ? 1 : Convert.ToInt32((from t in db.CLM_ClaimTask where t.AccidentClaimId == id orderby t.Id descending select t.TaskNo).FirstOrDefault());
                    TaskEditor.Message = "";
                }
                TaskEditor.HchkhasClaim = (from m in db.CLM_Claims where m.AccidentClaimId == id select m).FirstOrDefault() == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return TaskEditor;
        }

        public static List<LookUpListItems> FetchPromptDetails(string category,int accclmid ,bool addAll = false, bool addNone = false)
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
            list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            obj.Dispose();
            return list;
        }
    }
}
