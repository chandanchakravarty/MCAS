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
    public class BranchListItems : BaseModel
    {
        #region Properties
        public int? BranchId { get; set; }
        public string BranchType { get; set; }
        
        [Required(ErrorMessage = "Entity Code is required.")]
        public string BranchCode { get; set; }
        [Required(ErrorMessage = "Entity Name is required.")]
        public string BranchName { get; set; }  
        public string RegionCode { get; set; }
        public string MainBranchCode { get; set; }

        public override string screenId
        {
            get
            {
                return "258";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "257";
            }

        }
        #endregion

        #region Static Methods
        public static List<BranchListItems> Fetch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<BranchListItems> list = new List<BranchListItems>();
            list = (from l in obj.MNT_Branch orderby l.BranchName select new BranchListItems { BranchCode = l.BranchCode, BranchName = l.BranchName }).ToList();
            if (addAll)
            {
                list.Insert(0, new BranchListItems() { BranchCode = "", BranchName = "[Select...]" });
            }
            return list;
        }

        public static List<BranchListItems> FetchList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<BranchListItems>();
            var branchList = (from l in obj.MNT_Branch orderby l.BranchCode select l);
            if (branchList.Any())
            {
                foreach (var data in branchList)
                {
                    item.Add(new BranchListItems() { BranchId = data.BranchId, BranchCode = data.BranchCode, BranchName = data.BranchName, BranchType = data.BranchType, RegionCode = data.RegionCode, MainBranchCode = data.MainBranchCode, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public BranchListItems Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_Branch branchinfo;
            branchinfo = obj.MNT_Branch.Where(x => x.BranchId == this.BranchId.Value).FirstOrDefault();

            if (BranchId.HasValue)
            {

                branchinfo.BranchCode = this.BranchCode;
                branchinfo.BranchName = this.BranchName;
                branchinfo.ModifiedBy = this.ModifiedBy;
                branchinfo.ModifiedDate = DateTime.Now;
                obj.SaveChanges();
                this.BranchId = branchinfo.BranchId;
                return this;
            }
            else
            {
                branchinfo = new MNT_Branch();
                //var branch = (from row in obj.MNT_Branch select (int?)row.BranchId).Max() ?? 0;

                branchinfo.BranchCode = this.BranchCode;
                branchinfo.BranchName = this.BranchName;
                branchinfo.CreatedBy = this.CreatedBy;
                branchinfo.CreatedDate = DateTime.Now;
                obj.MNT_Branch.AddObject(branchinfo);
                obj.SaveChanges();
                this.BranchId = branchinfo.BranchId;
                return this;
                
            }
        }
        #endregion
    }


    public class BranchLoginListItems
    {
        #region Properties
        public int TranId { get; set; }
        public string BranchType { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string RegionCode { get; set; }
        public string MainBranchCode { get; set; }
        #endregion
    }
}
