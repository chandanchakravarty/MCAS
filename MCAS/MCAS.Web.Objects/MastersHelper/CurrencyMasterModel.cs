using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.Resources.ClaimMasters;

namespace MCAS.Web.Objects.MastersHelper
{
    public class CurrencyMasterModel : BaseModel
    {
        #region Properties
        public int? TranId { get; set; }
        //[Required(ErrorMessage = "Currency Code is required.")]
        [Required(ErrorMessageResourceType = typeof(CurrencyMasterEditor), ErrorMessageResourceName = "RFVCurrencyCode")]
        public string CurrencyCode { get; set; }
        //[Required(ErrorMessage = "Currency display code is required.")]
        [Required(ErrorMessageResourceType = typeof(CurrencyMasterEditor), ErrorMessageResourceName = "RFVCurrencyDisplay")]
        public string CurrencyDispCode { get; set; }
        //[Required(ErrorMessage = "Currency description is required.")]
        [Required(ErrorMessageResourceType = typeof(CurrencyMasterEditor), ErrorMessageResourceName = "RFVCurrencyDescription")]
        public string Description { get; set; }

        public override string screenId
        {
            get
            {
                return "244";
            }
            set
            {
                base.screenId = "244";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "112";
            }
            set
            {
                base.listscreenId = "112";
            }
        }
        #endregion

        #region Static Methods
        public static List<CurrencyMasterModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();

            var item = new List<CurrencyMasterModel>();

            var curr = (from l in _db.MNT_CurrencyM orderby l.Description select l);
            if (curr.Any())
            {
                foreach (var data in curr)
                {
                    item.Add(new CurrencyMasterModel() { TranId = data.TranId, CurrencyCode = data.CurrencyCode, CurrencyDispCode = data.CurrencyDispCode, Description = data.Description, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }

        #endregion

        #region Methods

        public CurrencyMasterModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_CurrencyM curr;

            if (TranId.HasValue)
            {
                curr = _db.MNT_CurrencyM.Where(x => x.TranId == this.TranId).FirstOrDefault();
                //curr.CurrencyCode = this.CurrencyCode.ToUpper();
                curr.CurrencyDispCode = this.CurrencyDispCode.ToUpper();
                curr.Description = this.Description;
                curr.ModifiedBy = Convert.ToString(this.ModifiedBy);
                curr.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.TranId = curr.TranId;
                this.CreatedBy = curr.CreatedBy == null ? " " : curr.CreatedBy;
                if (curr.CreatedDate != null)
                    this.CreatedOn = (DateTime)curr.CreatedDate;
                else
                    this.CreatedOn = DateTime.MinValue;
                this.ModifiedBy = curr.ModifiedBy == null ? " " : curr.ModifiedBy;
                this.ModifiedOn = curr.ModifiedDate;
                return this;

            }
            else
            {
                curr = new MNT_CurrencyM();
                curr.CurrencyCode = this.CurrencyCode.ToUpper();
                curr.CurrencyDispCode = this.CurrencyDispCode.ToUpper();
                curr.Description = this.Description;
                curr.CreatedBy = Convert.ToString(this.CreatedBy);
                curr.CreatedDate = DateTime.Now;
                _db.MNT_CurrencyM.AddObject(curr);
                _db.SaveChanges();
                this.TranId = curr.TranId;
                this.CreatedOn = (DateTime)curr.CreatedDate;
                return this;


            }
        }



        #endregion
    }
}
