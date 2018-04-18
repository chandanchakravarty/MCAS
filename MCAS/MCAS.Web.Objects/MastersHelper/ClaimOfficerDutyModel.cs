using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.ComponentModel;


namespace MCAS.Web.Objects.MastersHelper
{
    #region Claim Officer Duty
    public class ClaimOfficerDutyModel:BaseModel
    {
        MCASEntities _db = new MCASEntities();
        #region properties
        private DateTime? _lastassignmentdate = null;

        public int? TranId { get; set; }

        public string ClaimOfficerCode { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string ClaimOfficerName { get; set; }

        [Required(ErrorMessage = "User Group is required.")]
        public string UserGroup { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string ddlDeptCode { get; set; }


        [DisplayName("Last Assignment Date")]
        [Required(ErrorMessage = "Last Assignment Date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastAssignmentdate {
            get { return _lastassignmentdate; }
            set { _lastassignmentdate = value; }
        }



        public string Type { get; set; }

        [Required(ErrorMessage = "Claim number is required.")]
        public string ClaimNumber { get; set; }

        public List<UserGroupListItems> UserGroupList { get; set; }

        public List<DepartmentsListItems> DeptGroupList { get; set; }

        public override string screenId
        {
            get
            {
                return "254";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "126";
            }

        }

        #endregion

        #region Methods
        public ClaimOfficerDutyModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_ClaimOfficerDetail MNT_ClaimOfficerDetails;
            MNT_ClaimOfficerDetails = obj.MNT_ClaimOfficerDetail.Where(x => x.TranId == this.TranId.Value).FirstOrDefault();

            if (TranId.HasValue)
            {
                MNT_ClaimOfficerDetails.UserGroup = this.UserGroup;
                MNT_ClaimOfficerDetails.ClaimentOfficerName = this.ClaimOfficerName;
                MNT_ClaimOfficerDetails.Department = this.ddlDeptCode;
                MNT_ClaimOfficerDetails.LastAssignmentDate =Convert.ToDateTime(this.LastAssignmentdate);
                MNT_ClaimOfficerDetails.Type = this.Type;
                MNT_ClaimOfficerDetails.ClaimNo = this.ClaimNumber;
                obj.SaveChanges();
                this.TranId = MNT_ClaimOfficerDetails.TranId;
                return this;
            }
            else
            {
                MNT_ClaimOfficerDetails = new MNT_ClaimOfficerDetail();
                MNT_ClaimOfficerDetails.UserGroup = this.UserGroup;
                MNT_ClaimOfficerDetails.ClaimentOfficerName = this.ClaimOfficerName;
                MNT_ClaimOfficerDetails.Department = this.ddlDeptCode;
                MNT_ClaimOfficerDetails.LastAssignmentDate =Convert.ToDateTime(this.LastAssignmentdate);
                MNT_ClaimOfficerDetails.Type = this.Type;
                MNT_ClaimOfficerDetails.ClaimNo = this.ClaimNumber;
                obj.MNT_ClaimOfficerDetail.AddObject(MNT_ClaimOfficerDetails);
                obj.SaveChanges();
                this.TranId = MNT_ClaimOfficerDetails.TranId;
                return this;

            }
        }
        #endregion
    }
    #endregion

    #region Claim Officer Model

    public class ClaimOfficerModel : BaseModel
    {
        #region properties
        public int? TranId { get; set; }

        public string ClaimOfficerCode { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string ClaimOfficerName { get; set; }

        [Required(ErrorMessage = "User Group is required.")]
        public string UserGroup { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string ddlDeptCode { get; set; }

        [Required(ErrorMessage = "Last Assignment date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime LastAssignmentdate { get; set; }

        public string Type { get; set; }

        [Required(ErrorMessage = "Claim number is required.")]
        public string ClaimNumber { get; set; }

        public override string screenId
        {
            get
            {
                return "254";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "126";
            }

        }

        #endregion

        #region Static Methods
        public static List<ClaimOfficerModel> FetchClaimOfficer()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimOfficerModel>();
            var ClaimOfficerDetails = (from x in _db.MNT_ClaimOfficerDetail select x);
            if (ClaimOfficerDetails.Any())
            {
                foreach (var data in ClaimOfficerDetails)
                {
                    var Dept = (from p in _db.MNT_Department where p.DeptCode == data.Department select p.DeptName).FirstOrDefault();
                    item.Add(new ClaimOfficerModel() { TranId = data.TranId, UserGroup = data.UserGroup, ClaimOfficerName = data.ClaimentOfficerName, ddlDeptCode = Dept, LastAssignmentdate = data.LastAssignmentDate, Type = data.Type, ClaimNumber = data.ClaimNo });
                }
            }
            return item;
        }
        #endregion
    }

    #endregion

}


