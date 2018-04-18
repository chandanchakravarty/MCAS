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
    public class DepartmentsListItems : BaseModel
    {
        #region Properties
        public int? DeptId { get; set; }
        [Required(ErrorMessage = "Department Code is required.")]
        public string DeptCode { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        public string DeptName { get; set; }

        public override string screenId
        {
            get
            {
                return "264";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "263";
            }

        }
        #endregion

        #region Static Methods
        public static List<DepartmentsListItems> Fetch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<DepartmentsListItems> list = new List<DepartmentsListItems>();
            list = (from l in obj.MNT_Department orderby l.DeptName select new DepartmentsListItems { DeptId = l.DeptId, DeptName = l.DeptName, DeptCode = l.DeptCode }).ToList();
            if (addAll)
            {
                list.Insert(0, new DepartmentsListItems() { DeptCode = "", DeptName = "[Select...]" });
            }
            return list;
        }

        public static List<DepartmentsListItems> FetchList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<DepartmentsListItems>();
            var deptList = (from l in obj.MNT_Department orderby l.DeptCode select l);
            if (deptList.Any())
            {
                foreach (var data in deptList)
                {
                    item.Add(new DepartmentsListItems() { DeptId = data.DeptId, DeptCode = data.DeptCode, DeptName = data.DeptName, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public DepartmentsListItems Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_Department deptinfo;
            deptinfo = obj.MNT_Department.Where(x => x.DeptId == this.DeptId.Value).FirstOrDefault();

            if (DeptId.HasValue)
            {
                deptinfo.ModifiedDate = DateTime.Now;
                DataMapper.Map(this, deptinfo, true);
                deptinfo.ModifiedDate = DateTime.Now;
                deptinfo.ModifiedBy = this.CreatedBy;
                obj.SaveChanges();
                this.CreatedOn = deptinfo.CreatedDate == null ? DateTime.MinValue : (DateTime)deptinfo.CreatedDate;
                this.ModifiedOn = deptinfo.ModifiedDate;
                return this;
            }
            else
            {
                deptinfo = new MNT_Department();
                deptinfo.CreatedDate = DateTime.Now;
                DataMapper.Map(this, deptinfo, true);
                obj.MNT_Department.AddObject(deptinfo);
                obj.SaveChanges();
                this.CreatedOn = (DateTime)deptinfo.CreatedDate;
                this.DeptId =  deptinfo.DeptId;
                return this;
            }
        }
        #endregion
    }
}
