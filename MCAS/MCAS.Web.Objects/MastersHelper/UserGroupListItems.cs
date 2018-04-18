using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
namespace MCAS.Web.Objects.MastersHelper
{
    public class UserGroupListItems : BaseModel
    { 
        #region Properties
        public int? GroupId { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string DeptCode { get; set; }
        public short? AccessLevel { get; set; }
        public string IsActive { get; set; }
        

        public List<DepartmentsListItems> deptlist { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }
        public List<MenuListItem> menulist { get; set; }
        
        #endregion

        #region Static Methods
        public static List<UserGroupListItems> Fetch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<UserGroupListItems> list = new List<UserGroupListItems>();
            list = (from l in obj.MNT_GroupsMaster orderby l.GroupName where l.IsActive == "Y" && l.GroupName!=null  select new UserGroupListItems { GroupCode = l.GroupCode, GroupName = l.GroupName }).ToList();
            if (addAll)
            {
                list.Insert(0, new UserGroupListItems() { GroupCode = "", GroupName = "[Select...]" });
            }
            return list;
        }

        public static List<UserGroupListItems> FetchList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<UserGroupListItems>();
            var userGroupList = (from l in obj.MNT_GroupsMaster select l);
            if (userGroupList.Any())
            {
                foreach (var data in userGroupList)
                {
                    item.Add(new UserGroupListItems() { GroupId = data.GroupId, GroupCode = data.GroupCode, GroupName = data.GroupName, DeptCode = data.DeptCode, IsActive = data.IsActive, AccessLevel = data.AccessLevel, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }
        #endregion
    }
}
