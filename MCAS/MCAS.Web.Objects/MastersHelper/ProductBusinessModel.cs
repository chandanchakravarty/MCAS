using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.Resources.ProductBusiness;

namespace MCAS.Web.Objects.MastersHelper
{
    #region MainClass
    public class ProductBusinessModel : BaseModel
    {
        MCASEntities _db = new MCASEntities();

        #region properties

        public int? ProductId { get; set; }
        //public int? ID { get; set; }
        //[Required(ErrorMessage = "Main class code is required.")]
        [Required(ErrorMessageResourceType = typeof(ProductBusinessEditor), ErrorMessageResourceName = "RFVMainClassCode")]
        public string ProductCode { get; set; }


        public string DisplayProductCode { get; set; }
        //[Required(ErrorMessage = "Main class description is required.")]
        [Required(ErrorMessageResourceType = typeof(ProductBusinessEditor), ErrorMessageResourceName = "RFVMainClassDescription")]
        public string ProductDisplayName { get; set; }

        //[Required(ErrorMessage = "Status is required.")]
        [Required(ErrorMessageResourceType = typeof(ProductBusinessEditor), ErrorMessageResourceName = "RFVStatus")]
        public string Status { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }

        public string CobCode = "s";
        public string CobDesc = "s";


        public override string screenId
        {
            get
            {
                return "234";
            }
            set
            {
                base.screenId = "234";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "104";
            }
            set
            {
                base.listscreenId = "104";
            }
        }
        #endregion
        #region methods
        public static List<ProductBusinessModel> FetchList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ProductBusinessModel>();
            var ProductList = (from l in _db.MNT_Products select l);

            if (ProductList.Any())
            {
                foreach (var data in ProductList)
                {

                    item.Add(new ProductBusinessModel() { ProductId = data.ProductId, ProductCode = data.ProductCode, ProductDisplayName = data.ProductDisplayName, Status = Convert.ToString(data.ProductStatus) });
                }
            }
            return item;
        }

        public ProductBusinessModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_Products prods = new MNT_Products();

            if (ProductId.HasValue)
            {
                prods = _db.MNT_Products.Where(x => x.ProductId == this.ProductId).FirstOrDefault();
                prods.ProductCode = this.ProductCode;
                prods.DispProductCode = this.DisplayProductCode;
                prods.ProductDisplayName = this.ProductDisplayName;


                if (this.Status == "Active")
                {
                    prods.ProductStatus = 1;
                }
                else
                {
                    prods.ProductStatus = 0;
                }

                prods.ModifiedBy = this.ModifiedBy;
                prods.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.ProductId = prods.ProductId;
                this.ProductCode = prods.ProductCode;
                this.CreatedBy = prods.CreatedBy;
                this.CreatedOn = prods.CreatedDate == null ? DateTime.MinValue : (DateTime)prods.CreatedDate;
                this.ModifiedOn = prods.ModifiedDate;
                return this;

            }
            else
            {

                prods.ProductCode = this.ProductCode;
                prods.DispProductCode = this.DisplayProductCode;
                prods.ProductDisplayName = this.ProductDisplayName;
                //string input = prods.ProductDisplayName;
                //String output = input.Substring(0,1).ToUpper();
                //prods.ProductCode = this.DisplayProductCode; 

                if (this.Status == "Active")
                {
                    prods.ProductStatus = 1;
                }
                else
                {
                    prods.ProductStatus = 0;
                }
                prods.CreatedBy = this.CreatedBy;
                prods.CreatedDate = DateTime.Now;
                _db.MNT_Products.AddObject(prods);
                _db.SaveChanges();
                this.ProductCode = prods.ProductCode;
                this.CreatedOn = (DateTime)prods.CreatedDate;
                this.ProductId = prods.ProductId;
                return this;
            }



        }


        public String capitalizeFirstLetter(String original)
        {

            return original.Substring(0, 1).ToUpper() + original.Substring(1);
        }

        #endregion



    }
    #endregion





    #region SubClass
    public class SubClassModel : BaseModel
    {

        #region Properties
        public int? ID { get; set; }
        //[Required(ErrorMessage = "Main Class is required.")]
        [Required(ErrorMessageResourceType = typeof(SubClassEditor), ErrorMessageResourceName = "RFVMainClass")]
        public string ProductCode { get; set; }

        public string ProductDescription { get; set; }

        //[Required(ErrorMessage = "Sub Class Code is required.")]
        [Required(ErrorMessageResourceType = typeof(SubClassEditor), ErrorMessageResourceName = "RFVSubClassCode")]
        public string CobCode { get; set; }

        //[Required(ErrorMessage = "Sub Class Description is required.")]
        [Required(ErrorMessageResourceType = typeof(SubClassEditor), ErrorMessageResourceName = "RFVSubClassDescription")]
        public string CobDesc { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string BusinessType { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }
        public List<LookUpListItems> BusinessTypelist { get; set; }
        public List<ProductsListItems> ProductList { get; set; }
        public List<ProductsListItems> MainClassList { get; set; }

        public override string screenId
        {
            get
            {
                return "235";
            }
            set
            {
                base.screenId = "235";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "105";
            }
            set
            {
                base.listscreenId = "105";
            }
        }
        #endregion

        #region Static Methods
        public static List<SubClassModel> GetUserResult(string ClassCode, string SubClassCode)
        {
            MCASEntities _db = new MCASEntities();
            List<SubClassModel> searchResult = new List<SubClassModel>();
            try
            {
                searchResult = (from classes in _db.MNT_ProductClass
                                join prod in _db.MNT_Products on classes.ProdCode equals prod.ProductCode
                                where
                                  classes.ProdCode.Contains(ClassCode) &&
                                  classes.ClassCode.Contains(SubClassCode)

                                select new SubClassModel()
                                {
                                    CobCode = classes.ClassCode,
                                    CobDesc = classes.ClassDesc,
                                    ProductCode = prod.ProductDisplayName,
                                    ID = classes.ID

                                }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return searchResult;
        }
        #endregion

        #region Methods
        public SubClassModel SubClassUpdate()
        {
            MCASEntities _db = new MCASEntities();
            MNT_ProductClass prod = new MNT_ProductClass();
            if (ID.HasValue)
            {
                prod = _db.MNT_ProductClass.Where(x => x.ID == this.ID.Value).FirstOrDefault();

                prod.ProdCode = this.ProductCode.ToUpper();
                prod.ClassCode = this.CobCode.ToUpper();
                prod.ClassDesc = this.CobDesc;


                if (this.Status == "Active")
                {
                    prod.Status = "1";
                }
                else
                {
                    prod.Status = "0";
                }

                if (this.BusinessType == "M")
                {
                    prod.BusinessType = "M";
                }
                else
                {
                    prod.BusinessType = "B";
                }

                prod.Remarks = this.Remarks;
                prod.ModifiedBy = this.ModifiedBy;
                prod.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.CreatedBy = prod.CreatedBy;
                this.CreatedOn = prod.CreatedDate == null ? DateTime.MinValue : (DateTime)prod.CreatedDate;
                this.ModifiedOn = prod.ModifiedDate;
                this.ModifiedBy = prod.ModifiedBy;
                return this;

            }
            else
            {

                prod.ProdCode = this.ProductCode.ToUpper();
                //prod.ClassCode = prod.ProdCode + this.CobCode.ToUpper();
                prod.ClassCode = this.CobCode.ToUpper();
                prod.ClassDesc = this.CobDesc;
                if (this.Status == "Active")
                {
                    prod.Status = "1";
                }
                else
                {
                    prod.Status = "0";
                }
                //  prod.Status = this.Status;
                if (this.BusinessType == "M")
                {
                    prod.BusinessType = "M";
                }
                else
                {
                    prod.BusinessType = "B";
                }
                prod.Remarks = this.Remarks;
                prod.CreatedBy = this.CreatedBy;
                prod.CreatedDate = DateTime.Now;
                _db.MNT_ProductClass.AddObject(prod);
                _db.SaveChanges();
                this.CreatedOn = (DateTime)prod.CreatedDate;
                this.CreatedBy = CreatedBy;
                this.ID = prod.ID;
                return this;
            }
        }
        #endregion

        
    }
    #endregion




}
