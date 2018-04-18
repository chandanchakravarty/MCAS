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
    public class LOUModel : BaseModel
    {
        #region properties

        private DateTime? _createdDate = null;
        private DateTime? _modifiedDate = null;
        private DateTime? _effectiveDate = null;


        public int? Id { get; set; }

        [Required(ErrorMessage = "New LOU Rate is required.")]
        [DisplayName("LOU Rate")]
        //[RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        //[Range(typeof(Decimal), "0", "9999", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
        public decimal? LouRate { get; set; }

        [Required(ErrorMessage = "Effective Date is required.")]
        [DisplayName("Effective Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EffectiveDate
        {
            get { return _effectiveDate; }
            set { _effectiveDate = value; }
        }

        public string IsActive { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        public string ClaimId { get; set; }

        public override string screenId
        {
            get
            {
                return "255";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "127";
            }

        }

        #endregion



        #region Static Method
        public static List<LOUModel> FetchLOUDetails()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<LOUModel>();
            var LOUList = (from x in _db.MNT_LOU_MASTER orderby x.EffectiveDate select x);
            if (LOUList.Any())
            {
                foreach (var data in LOUList)
                {
                    item.Add(new LOUModel()
                    {
                        Id = data.Id,
                        LouRate =data.LouRate,
                        EffectiveDate = data.EffectiveDate,
                        IsActive = data.IsActive

                    });
                }
            }
            return item;
        }


        //public static List<LOUModel> GetUplodedResultLOU(string LouRate, DateTime? EffectiveDate)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("INSERT INTO #TempTable (ID,LouRate,IsActive, EffectiveDate)  select ID,LouRate,IsActive, EffectiveDate from dbo.MNT_LOU_MASTER");

        //    if (LouRate != "" || EffectiveDate != null)
        //    {
        //        sb.Append(" where ");
        //    }

        //    if (LouRate != "")
        //    {
        //        sb.Append("LouRate = '" + LouRate + "'");
        //        sb.Append(" and ");
        //    }
        //    if (EffectiveDate != null)
        //    {
        //        string dd = Convert.ToDateTime(EffectiveDate).ToString("yyyy-MM-dd");
        //        sb.Append("CAST(EffectiveDate AS DATE) = '" + dd + "'");
        //        sb.Append(" and ");
        //    }
        //    string fin = sb.ToString().TrimEnd();
        //    string endval = fin.Split(' ').Last();
        //    string queryString = string.Empty;
        //    if (endval == "and")
        //    {
        //        queryString = fin.Substring(0, fin.LastIndexOf(" ") < 0 ? 0 : fin.LastIndexOf(" "));
        //    }
        //    else
        //    {
        //        queryString = fin;
        //    }
        //    MCASEntities obj = new MCASEntities();
        //    var searchResult = obj.Proc_GetLOUMASTER(queryString).ToList();
        //    var item = new List<LOUModel>();
        //    if (searchResult.Any())
        //    {

        //        foreach (var data in searchResult)
        //        {
        //            var status = data.IsActive == "Y" ? "Active" : "Not Active";
        //            item.Add(new LOUModel() { Id = data.Id, LouRate = data.LouRate, EffectiveDate = data.EffectiveDate, IsActive = status });
        //        }
        //    }
        //    return item;
        //}
        #endregion

        #region Methods

        public LOUModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_LOU_MASTER MNTLOUMASTER;
            MNTLOUMASTER = obj.MNT_LOU_MASTER.Where(x => x.Id == this.Id.Value).FirstOrDefault();
            var model = new LOUModel();
            string CreatedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;

            if (Id.HasValue)
            {
                MNTLOUMASTER.LouRate =Convert.ToDecimal(this.LouRate);
                MNTLOUMASTER.EffectiveDate = this.EffectiveDate;
                MNTLOUMASTER.ModifiedDate = DateTime.Now;
                MNTLOUMASTER.ModifiedBy = this.ModifiedBy;
                obj.SaveChanges();
                this.Id = MNTLOUMASTER.Id;
                this.CreatedBy = MNTLOUMASTER.CreatedBy;
                this.CreatedOn = MNTLOUMASTER.CreatedDate == null ? DateTime.MinValue : (DateTime)MNTLOUMASTER.CreatedDate;
                this.ModifiedOn = MNTLOUMASTER.ModifiedDate;
                return this;
            }
            else
            {
                MNTLOUMASTER = new MNT_LOU_MASTER();
                MNTLOUMASTER.LouRate =Convert.ToDecimal(this.LouRate);
                MNTLOUMASTER.EffectiveDate = this.EffectiveDate;
                MNTLOUMASTER.IsActive = "Y";
                MNTLOUMASTER.CreatedBy = this.CreatedBy;
                MNTLOUMASTER.CreatedDate = DateTime.Now;
                obj.MNT_LOU_MASTER.AddObject(MNTLOUMASTER);
                obj.SaveChanges();
                this.Id = MNTLOUMASTER.Id;
                this.CreatedOn = (DateTime)MNTLOUMASTER.CreatedDate;
                return this;

            }
        }
        #endregion
    }
}
