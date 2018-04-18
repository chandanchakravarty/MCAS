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
using System.ComponentModel;


namespace MCAS.Web.Objects.MastersHelper
{
    public class GroupMastersListItems : BaseModel
    {
        #region Properties


        public int? GroupId { get; set; }
        [Display(Name = "Group Code")]
        [Required(ErrorMessage = "Group Code is Required. ")]
        [StringLength(2, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string GroupCode { get; set; }
        [Required(ErrorMessage = "Group Name is Required. ")]
        public string GroupName { get; set; }
        [Required(ErrorMessage = "Department is Required. ")]
        public string DeptCode { get; set; }
        [Required(ErrorMessage = "User Role Assignment is Required. ")]
        public string RoleCode { get; set; }
        public short? AccessLevel { get; set; }
        [Required(ErrorMessage = "Status is Required. ")]
        public string IsActive { get; set; }

        private string _screenId { get; set; }
        private string _listscreenId { get; set; }


        public override string screenId
        {
            get
            {
                return "260";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "259";
            }

        }

        public string hiddentab { get; set; }

        public List<DepartmentsListItems> deptlist { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }
        public List<LookUpListItems> Rolelist { get; set; }
        public List<MenuListItem> menulist { get; set; }
        public List<GroupPermissionListItems> readwritelist { get; set; }

        #endregion

        #region Static Methods
        public static List<GroupMastersListItems> Fetch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<GroupMastersListItems> list = new List<GroupMastersListItems>();
            list = (from l in obj.MNT_GroupsMaster orderby l.GroupName where l.IsActive == "Y" select new GroupMastersListItems { GroupCode = l.GroupCode, GroupName = l.GroupName, GroupId = l.GroupId }).ToList();
            if (addAll)
            {
                list.Insert(0, new GroupMastersListItems() { GroupId = 0, GroupName = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        public static List<GroupMastersListItems> FetchList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<GroupMastersListItems>();
            var userGroupList = (from l in obj.MNT_GroupsMaster select l);

            if (userGroupList.Any())
            {
                foreach (var data in userGroupList)
                {
                    var Deptname = (from p in obj.MNT_Department where p.DeptCode == data.DeptCode select p.DeptName).FirstOrDefault();
                    item.Add(new GroupMastersListItems() { GroupId = data.GroupId, GroupCode = data.GroupCode, GroupName = data.GroupName, DeptCode = Deptname, IsActive = data.IsActive, AccessLevel = data.AccessLevel, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            obj.Dispose();
            return item;
        }
        #endregion

        #region Methods
        public GroupMastersListItems Update(MCASEntities obj)
        {
            MNT_GroupsMaster groupinfo;
            if (GroupId.HasValue)
            {
                groupinfo = obj.MNT_GroupsMaster.Where(x => x.GroupId == this.GroupId.Value).FirstOrDefault();

                groupinfo.GroupCode = this.GroupCode;
                groupinfo.GroupName = this.GroupName;
                groupinfo.DeptCode = this.DeptCode;
                groupinfo.AccessLevel = this.AccessLevel;
                groupinfo.RoleCode = this.RoleCode;
                var status = this.IsActive;
                if (status == "Active")
                {
                    groupinfo.IsActive = "Y";
                }
                else
                {
                    groupinfo.IsActive = "N";

                }
                groupinfo.ModifiedBy = Convert.ToString(this.ModifiedBy);
                groupinfo.ModifiedDate = DateTime.Now;

                obj.SaveChanges();
                this.GroupId = groupinfo.GroupId;
                this.GroupCode = groupinfo.GroupCode;
                this.CreatedBy = groupinfo.CreatedBy;
                this.CreatedOn = groupinfo.CreatedDate == null ? DateTime.MinValue : (DateTime)groupinfo.CreatedDate;
                this.ModifiedOn = groupinfo.ModifiedDate;
                return this;
            }
            else
            {
                groupinfo = new MNT_GroupsMaster();
                groupinfo.GroupCode = this.GroupCode;
                groupinfo.GroupName = this.GroupName;
                groupinfo.DeptCode = this.DeptCode;
                groupinfo.AccessLevel = this.AccessLevel;

                if (this.IsActive == "Active")
                {
                    groupinfo.IsActive = "Y";
                }
                else
                {
                    groupinfo.IsActive = "N";
                }
                groupinfo.CreatedBy = Convert.ToString(this.CreatedBy);
                groupinfo.CreatedDate = DateTime.Now;
                var res = obj.Proc_MNT_GroupsMaster_Save(this.GroupCode, this.GroupName, this.DeptCode, this.AccessLevel, groupinfo.IsActive, Convert.ToString(this.CreatedBy),this.RoleCode);
                var gp = (from l in obj.MNT_GroupsMaster.Where(x => x.GroupCode == this.GroupCode) select l.GroupId).SingleOrDefault();
                this.GroupId = gp;
                this.GroupCode = groupinfo.GroupCode;
                this.CreatedOn = (DateTime)groupinfo.CreatedDate;
                return this;
            }
        }
        #endregion
    }


    public class GroupPermissionListItems : BaseModel
    {
        #region Properties
        public int? GroupPermissionId { get; set; }
        public string GroupId { get; set; }
        public int MenuId { get; set; }
        public string Status { get; set; }
        public string Selected { get; set; }
        public int RowId { get; set; }
        public bool? Read { get; set; }
        public bool? Write { get; set; }
        public bool? Delete { get; set; }
        public bool? SplPermission { get; set; }
        public string permission { get; set; }
        public List<GroupPermissionListItems> permissionlistlist { get; set; }
        #endregion
        #region Methods
        public static List<GroupPermissionListItems> FetchList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<GroupPermissionListItems>();
            var userPermissionList = (from l in obj.MNT_GroupPermission select l);
            if (userPermissionList.Any())
            {
                foreach (var data in userPermissionList)
                {
                    item.Add(new GroupPermissionListItems() { GroupId = data.GroupId, MenuId = data.MenuId, Read = data.Read, Write = data.Write, Delete = data.Delete, SplPermission = data.SplPermission });
                }
            }
            obj.Dispose();
            return item;

        }
        #endregion
    }
}
