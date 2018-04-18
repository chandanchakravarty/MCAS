using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;

namespace MCAS.Web.Objects.MastersHelper
{
    public class FALModel : BaseModel
    {
        #region "Properties"
        public int? FALId { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage = "FAL Level Name is required.")]
        public string FALLevelName { get; set; }
        public string UnlimitedAmt { get; set; }
        [Required(ErrorMessage = "FAL Access Category is required.")]
        public string FALAccessCategory { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public decimal? Amount { get; set; }
        public string FALDisplayName { get; set; }

        public string HdnId { get; set; }

        public List<LookUpListItems> FALCatlist { get; set; }
        public IEnumerable<SelectListItem> unlimitedAmtlist { get; set; }

        public override string screenId
        {
            get
            {
                return "296";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "295";
            }

        }
        #endregion

        #region methods
        public static List<FALModel> FetchList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<FALModel>();
            var FALList = (from l in _db.MNT_FAL select l);

            if (FALList.Any())
            {
                foreach (var data in FALList)
                {

                    item.Add(new FALModel() { FALId = data.FALId, UserId = data.UserId, FALLevelName = data.FALLevelName, FALAccessCategory = data.FALAccessCategory, Amount =  data.Amount, UnlimitedAmt=data.UnlimitedAmt});
                }
            }
            return item;
        }

        public FALModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_FAL falval;
            falval = obj.MNT_FAL.Where(x => x.FALId == this.FALId).FirstOrDefault();
            var model = new FALModel();
            if (FALId.HasValue)
            {
                falval.FALLevelName = this.FALLevelName;
                falval.FALAccessCategory = this.FALAccessCategory;
                falval.Amount = this.Amount;
                falval.UnlimitedAmt = this.UnlimitedAmt;
                falval.ModifiedBy = this.ModifiedBy; 
                falval.ModifiedDate = DateTime.Now;
                obj.SaveChanges();
                this.FALId = falval.FALId;
                this.CreatedBy = falval.CreatedBy;
                this.CreatedOn = falval.CreatedDate == null ? DateTime.MinValue : (DateTime)falval.CreatedDate;
                this.ModifiedOn = falval.ModifiedDate;
                return this;
            }
            else
            {
                falval = new MNT_FAL();
                falval.FALLevelName = this.FALLevelName;
                falval.FALAccessCategory = this.FALAccessCategory;
                falval.Amount = this.Amount;
                falval.UnlimitedAmt = this.UnlimitedAmt;
                falval.CreatedBy = this.CreatedBy; 
                falval.CreatedDate = DateTime.Now;
                obj.MNT_FAL.AddObject(falval);
                obj.SaveChanges();
                this.FALId = falval.FALId;
                this.CreatedOn = (DateTime)falval.CreatedDate;
                return this;
            }
        }

        

        #endregion

    }

    public class UnlimitedAmtStatus
    {
        public String ID { get; set; }
        public String Name { get; set; }
    }
}
