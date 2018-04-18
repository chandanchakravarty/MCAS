using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;

namespace MCAS.Web.Objects.MastersHelper
{
    public class LossNatureListItems : BaseModel
    {
        #region Properties
        public int? TranId { get; set; }
        public string LossType { get; set; }
        public string LossNatureCode { get; set; }
        public string LossNatureName { get; set; }
        [Required(ErrorMessage = "Loss Nature Description is required.")]
        public string LossNatureDescription { get; set; }
        [Required(ErrorMessage = "Main Class is required.")]
        public string ProductCode { get; set; }
        public string ProductDisplayName { get; set; }
        public string tempLossNatureCode { get; set; }
        [Required(ErrorMessage = "Sub Class is required.")]
        public string SubClassCode { get; set; }
        public List<ProductsListItems> ProductList { get; set; }
        public List<LossTypeModel> LossTypeList { get; set; }
        public List<ProductsListItems> SubClassList { get; set; }

        public override string screenId
        {
            get
            {
                return "232";
            }
            set
            {
                base.screenId = "232";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "102";
            }
            set
            {
                base.listscreenId = "102";
            }
        }

        #endregion

        #region "Public Shared Methods"
        public static List<LossNatureListItems> FetchList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<LossNatureListItems>();
            var lossnaturelist = (from l in obj.MNT_LossNature select l);
            if (lossnaturelist.Any())
            {
                foreach (var data in lossnaturelist)
                {
                        var prodCode = (from p in obj.MNT_Products where  p.ProductCode == data.ProductCode select p.ProductDisplayName).FirstOrDefault();
                        var subclasscode = (from p in obj.MNT_ProductClass where p.ClassCode == data.SubClassCode select p.ClassDesc).FirstOrDefault();
                        item.Add(new LossNatureListItems() { TranId = data.TranId, LossNatureCode = data.LossNatureCode, LossType = data.LossType, LossNatureDescription = data.LossNatureDescription, LossNatureName = data.LossNatureName, ProductDisplayName = prodCode, SubClassCode = subclasscode, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                    
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public LossNatureListItems Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_LossNature lossnatureinfo;


            if (TranId.HasValue)
            {
                lossnatureinfo = obj.MNT_LossNature.Where(x => x.TranId == this.TranId).FirstOrDefault();

                lossnatureinfo.LossNatureCode = this.LossNatureCode;
                lossnatureinfo.LossNatureName = this.LossNatureName;
                lossnatureinfo.LossType = this.LossType;
                lossnatureinfo.LossNatureDescription = this.LossNatureDescription;
                lossnatureinfo.ProductCode = this.ProductCode;
                lossnatureinfo.SubClassCode = this.SubClassCode;
                lossnatureinfo.ModifiedBy = Convert.ToString(this.ModifiedBy);
                lossnatureinfo.ModifiedDate = DateTime.Now;
                obj.SaveChanges();
                this.LossNatureCode = lossnatureinfo.LossNatureCode;
                return this;
            }
            else
            {
                lossnatureinfo = new MNT_LossNature();

                var maxlength = 5;
                var prefix = "LN";
              //  var countrows = (from row in obj.MNT_LossNature select row.TranId).Max();
                var countrows = (from row in obj.MNT_LossNature select (int?)row.TranId).Max() ?? 0; 
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var lNaturecode = (prefix + result + currentno);

                lossnatureinfo.LossNatureCode = lNaturecode;
                lossnatureinfo.LossNatureName = this.LossNatureName;
                lossnatureinfo.LossNatureDescription = this.LossNatureDescription;
                lossnatureinfo.LossType = this.LossType;
                lossnatureinfo.ProductCode = this.ProductCode;
                lossnatureinfo.SubClassCode = this.SubClassCode;
                lossnatureinfo.CreatedBy = Convert.ToString(this.CreatedBy);
                lossnatureinfo.CreatedDate = DateTime.Now;
                obj.MNT_LossNature.AddObject(lossnatureinfo);
                obj.SaveChanges();
                this.LossNatureCode = lNaturecode;
                this.TranId = lossnatureinfo.TranId;
                return this;
            }

            
        }
        #endregion
    }
}
