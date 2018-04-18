using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web.Mvc;
using System.Web;


namespace MCAS.Web.Objects.MastersHelper
{
    public class ReAssignmentModel : BaseModel
    {
        #region properties
        private DateTime? _reAssignDateFrom = null;
        private DateTime? _reAssignDateTo = null;
        private DateTime? _createdDate = null;
        private DateTime? _createdBy = null;
        private DateTime? _modifiedDate = null;

        public int Id { get; set; }
        public string DairyId { get; set; }

        public string Dairiestobereassigned { get; set; }
        public string DairiestobereassignedId { get; set; }
        public string TypeOfAssignment { get; set; }

        public string Permanent { get; set; }

        public string Temporary { get; set; }

        [Required(ErrorMessage = "ReAssign To is required.")]
        [NotEqual("Prop1", ErrorMessage = "ReAssign To is required.")]
        public int[] ReAssignTo { get; set; }


        public string DairyFromUser { get; set; }


        [Required(ErrorMessage = "ReAssign Date From is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReAssignDateFrom
        {
            get { return _reAssignDateFrom; }
            set { _reAssignDateFrom = value; }
        }

        [Required(ErrorMessage = "ReAssign Date To is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReAssignDateTo
        {
            get { return _reAssignDateTo; }
            set { _reAssignDateTo = value; }
        }
        public string Remark { get; set; }

        public string EmailId { get; set; }


        public string Status { get; set; }
        public string IsActive { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string ModifiedBy { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }


        public IEnumerable<SelectListItem> Values { get; set; }

        public string Prop1 { get; set; }
        #endregion
        # region Staticmethod

        public static List<SelectListItem> Fetch()
        {
            MCASEntities obj = new MCASEntities();
            List<SelectListItem> item = new List<SelectListItem>();
            try
            {
                item = (from l in obj.MNT_Users
                        select new CommonUtilities.CommonType()
                        {
                            intID = l.SNo,
                            Text = l.UserDispName
                        }).ToList().Select(x => new SelectListItem()
                        {
                            Text = x.Text,
                            Value = x.intID.ToString()

                        }).ToList();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }
            item.Insert(0, new SelectListItem() { Value = "", Text = "--Individual Users--" });
            return item;
        }

        #endregion
        public static string Update(string DairyId, string TypeOfAssignment, string ReAssignTo, string DairyFromUser, string ReAssignDateFrom, string ReAssignDateTo, string Remark, string ClaimId, int AccidentClaimId)
        {
            try
            {
                MCASEntities obj = new MCASEntities();
                ReAssignmentModel model = new ReAssignmentModel();
                var cid = Convert.ToInt32(ClaimId);
                string CreatedBy = ((BaseModel)(model)).CreatedBy;
                string ModifiedBy = ((BaseModel)(model)).ModifiedBy;
                string[] arr = DairyFromUser.Split(',');
                string[] arr2 = DairyId.Split(',');
                Claim_ReAssignmentDairy Claim_ReAssignmentDairy = new Claim_ReAssignmentDairy();
                Claim_ReAssignmentDairy.DairyId = DairyId;
                Claim_ReAssignmentDairy.TypeOfAssignment = TypeOfAssignment;
                Claim_ReAssignmentDairy.ReAssignTo = ReAssignTo;
                Claim_ReAssignmentDairy.DairyFromUser = DairyFromUser;
                Claim_ReAssignmentDairy.ReAssignDateFrom = Convert.ToDateTime(ReAssignDateFrom);
                Claim_ReAssignmentDairy.ReAssignDateTo = Convert.ToDateTime(ReAssignDateTo);
                Claim_ReAssignmentDairy.Remark = Remark;
                Claim_ReAssignmentDairy.CreatedDate = DateTime.Now;
                Claim_ReAssignmentDairy.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                Claim_ReAssignmentDairy.ModifiedBy = ModifiedBy;
                Claim_ReAssignmentDairy.ClaimId = cid;
                Claim_ReAssignmentDairy.IsActive = "Y";
                Claim_ReAssignmentDairy.AccidentClaimId = AccidentClaimId;
                obj.Claim_ReAssignmentDairy.AddObject(Claim_ReAssignmentDairy);
                obj.SaveChanges();
                var newlist = resultlist(arr, arr2);
                var id1 = Convert.ToInt32(ReAssignTo);
                var touser = (from m in obj.MNT_Users where m.SNo == id1 select m.UserDispName).FirstOrDefault();
                var createby = HttpContext.Current.Session["LoggedInUserId"].ToString();
                var lusersno = (from m in obj.MNT_Users where m.UserId == createby select m.SNo).FirstOrDefault();
                var luserdisname = (from m in obj.MNT_Users where m.UserId == createby select m.UserDispName).FirstOrDefault();
                foreach (var d in newlist)
                {
                    int ouserid = Convert.ToInt32(d.dai3);
                    var ouser = (from m in obj.MNT_Users where m.SNo == ouserid select m.UserDispName).FirstOrDefault();
                    var emailsubject = ouserid == lusersno ? "“Task Reassignment: " + FirstCharToUpper(ouser) + " has reassigned tasks to " + FirstCharToUpper(touser) + "”" : "“Task Reassignment: " + FirstCharToUpper(luserdisname) + " has reassigned " + FirstCharToUpper(ouser) + "'s tasks to " + FirstCharToUpper(touser) + "”";
                    var Emailbody = ouserid == lusersno ? emailbodytoreassign(ouser, d.dai4.Split(',')) : emailbodytoreassign(ouser, d.dai4.Split(','), FirstCharToUpper(luserdisname));
                    for (int i = 0; i < d.dai4.Split(',').Length; i++)
                    {
                        var save = obj.Proc_ReAssignSave(Convert.ToInt32(d.dai4.Split(',')[i].ToString()), id1, ouserid);
                    }

                    var Emailsave = obj.Proc_ReAssignMail(ouserid, id1, ouserid == lusersno ? 0 : lusersno, CreatedBy, HttpContext.Current.Session["LoggedInUserId"].ToString(), Emailbody, emailsubject);
                }
                return "T";
            }
            catch (Exception ex)
            {
                return "F";
            }

        }
        public static string UpdateMultipleReassign(string DairyId, string TypeOfAssignment, string ReAssignTo, string DairyFromUser, string ReAssignDateFrom, string ReAssignDateTo, string Remark, string ClaimId, int AccidentClaimId)
        {
            try
            {
                MCASEntities obj = new MCASEntities();
                ReAssignmentModel model = new ReAssignmentModel();
                var cid = Convert.ToInt32(ClaimId);
                string CreatedBy = ((BaseModel)(model)).CreatedBy;
                string ModifiedBy = ((BaseModel)(model)).ModifiedBy;
                string[] arr = DairyFromUser.Split(',');
                string[] arr2 = DairyId.Split(',');
                Claim_ReAssignmentDairy Claim_ReAssignmentDairy = new Claim_ReAssignmentDairy();
                Claim_ReAssignmentDairy.DairyId = DairyId;
                Claim_ReAssignmentDairy.TypeOfAssignment = TypeOfAssignment;
                Claim_ReAssignmentDairy.ReAssignTo = ReAssignTo;
                Claim_ReAssignmentDairy.DairyFromUser = DairyFromUser;
                Claim_ReAssignmentDairy.ReAssignDateFrom = Convert.ToDateTime(ReAssignDateFrom);
                Claim_ReAssignmentDairy.ReAssignDateTo = Convert.ToDateTime(ReAssignDateTo);
                Claim_ReAssignmentDairy.Remark = Remark;
                Claim_ReAssignmentDairy.CreatedDate = DateTime.Now;
                Claim_ReAssignmentDairy.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                Claim_ReAssignmentDairy.ModifiedBy = ModifiedBy;
                Claim_ReAssignmentDairy.ClaimId = cid;
                Claim_ReAssignmentDairy.IsActive = "Y";
                Claim_ReAssignmentDairy.AccidentClaimId = AccidentClaimId;
                obj.Claim_ReAssignmentDairy.AddObject(Claim_ReAssignmentDairy);
                obj.SaveChanges();
                var newlist = resultlist(arr, arr2);
                var id1 = Convert.ToInt32(ReAssignTo);
                var touser = (from m in obj.MNT_Users where m.SNo == id1 select m.UserDispName).FirstOrDefault();
                var createby = HttpContext.Current.Session["LoggedInUserId"].ToString();
                var lusersno = (from m in obj.MNT_Users where m.UserId == createby select m.SNo).FirstOrDefault();
                var luserdisname = (from m in obj.MNT_Users where m.UserId == createby select m.UserDispName).FirstOrDefault();
                foreach (var d in newlist)
                {
                    int ouserid = Convert.ToInt32(d.dai3);
                    for (int i = 0; i < d.dai4.Split(',').Length; i++)
                    {
                        var save = obj.Proc_ReAssignSave(Convert.ToInt32(d.dai4.Split(',')[i].ToString()), id1, ouserid);
                    }
                }
                return "T";
            }
            catch (Exception)
            {
                return "F";
            }

        }
        private static List<mailclass3> resultlist(string[] arr, string[] arr2)
        {
            List<mailclass> s_List = new List<mailclass>();
            List<mailclass2> s_List2 = new List<mailclass2>();
            List<mailclass3> s_List3 = new List<mailclass3>();
            string[] arrn = arr.Distinct().ToArray();
            for (int i = 0; i < arrn.Length; i++)
            {
                s_List.Add(new mailclass()
                {
                    dai = arrn[i]
                });
            }
            var flist = s_List.Distinct().ToList();
            for (int i = 0; i < arr2.Length; i++)
            {
                s_List2.Add(new mailclass2()
                {
                    dai2 = arr2[i],
                    dai3 = arr[i]
                });
            }

            foreach (var q in flist)
            {
                var res = (from l in s_List2 where l.dai3 == q.dai select l.dai2).ToList();

                s_List3.Add(new mailclass3()
                {
                    dai3 = q.dai,
                    dai4 = string.Join(",", res.ToArray())

                });
            }
            return s_List3;
        }
        private static string emailbodytoreassign(string name, string[] arr2, string lu = "N")
        {
            StringBuilder sb = new StringBuilder();
            MCASEntities obj = new MCASEntities();
            var str = "";
            if (lu == "N")
            {
                str = "&ldquo;" + name + " has reassigned the following tasks to you. Please complete the tasks before the expiry date.&rdquo;<br/><br/>";
            }
            else
            {
                str = "&ldquo;" + lu + " has reassigned the following " + FirstCharToUpper(name) + "'s tasks to you. Please complete the tasks before the expiry date.&rdquo;<br/><br/>";
            }
            var Emailbody = str + "<table border='1' style='background-color:#FFFFFF;border-collapse:collapse;border:1px solid #000000;color:#000000;width:100%' cellpadding='3' cellspacing='3'><tr><td>Name of Task </td><td>Expiry Date</td><td>Task Owner </td></tr>";
            for (var k = 0; k < arr2.Length; k++)
            {
                var lid = Convert.ToInt32(arr2[k].ToString());
                var expdate = (from m in obj.TODODIARYLISTs where m.LISTID == lid select m.STARTTIME).FirstOrDefault();
                var listtpeid = (from m in obj.TODODIARYLISTs where m.LISTID == lid select m.LISTTYPEID).FirstOrDefault();

                int Num;
                bool isNum = int.TryParse(listtpeid.ToString(), out Num);
                if (isNum)
                {
                    var ilisttpeid_ = Convert.ToInt32(listtpeid);
                    var taskname = (from m in obj.MNT_Lookups where m.LookupID == ilisttpeid_ select m.Lookupdesc).FirstOrDefault();
                    sb.Append("<tr><td>" + FirstCharToUpper(taskname) + "</td><td>" + expdate.ToString().Substring(0, 10) + "</td><td>" + FirstCharToUpper(name) + "</td></tr>");
                }
                else
                {
                    var taskname = (from m in obj.MNT_Lookups where m.Lookupvalue == listtpeid select m.Lookupdesc).FirstOrDefault();
                    sb.Append("<tr><td>" + FirstCharToUpper(taskname) + "</td><td>" + expdate.ToString().Substring(0, 10) + "</td><td>" + FirstCharToUpper(name) + "</td></tr>");
                }

            }
            Emailbody = Emailbody + sb.ToString() + "</table>";
            obj.Dispose();
            return Emailbody;
        }
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static string fetchuser(string DairyId)
        {
            string[] listIds = DairyId.Split(',');
            MCASEntities _db = new MCASEntities();
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
            return ResultusrString;
        }
        public static string fetchuserId(string DairyId)
        {
            MCASEntities _db = new MCASEntities();
            Int64 id = Convert.ToInt64(DairyId);
            var selectedDiariesUserId = (from m in _db.TODODIARYLISTs where m.LISTID == id select m.TOUSERID).FirstOrDefault().ToString();
            return selectedDiariesUserId;
        }
        public static IEnumerable<SelectListItem> FetchEscalationList()
        {
            List<EsclationStatus> list1 = new List<EsclationStatus>();
            list1.Add(new EsclationStatus() { ID = "Y", Name = "Yes" });
            list1.Add(new EsclationStatus() { ID = "N", Name = "No" });
            SelectList sl1 = new SelectList(list1, "ID", "Name");
            return sl1;
        }
    }
    public class mailclass
    {
        public string dai { get; set; }
    }
    public class mailclass2
    {
        public string dai2 { get; set; }
        public string dai3 { get; set; }
    }
    public class mailclass3
    {
        public string dai3 { get; set; }
        public string dai4 { get; set; }
    }
    public class ReassignuserList
    {
        #region Properties
        public string ReAssignId { get; set; }
        public string ReAssignName { get; set; }
        #endregion
    }
}
