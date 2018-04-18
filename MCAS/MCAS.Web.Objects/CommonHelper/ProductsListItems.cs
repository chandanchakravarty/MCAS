using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
    public class ProductsListItems
    {
        #region Properties
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDisplayName { get; set; }
        

        
        public int Id { get; set; }

        //String name of a checkbox
        public string Name { get; set; }

        //Boolean value to select a checkbox
        //on the list
        public bool IsSelected { get; set; }

        //Object of html tags to be applied
        //to checkbox, e.g.:'new{tagName = "tagValue"}'
        public object Tags { get; set; }

        public string ClassCode { get; set; }
        public string ClassDescrption { get; set; }
       

        public IList<ProductsListItems> AvailableProducts { get; set; }
        public IList<ProductsListItems> SelectedProducts { get; set; }
        
        
        #endregion

        #region Static Methods
        public static List<ProductsListItems> Fetch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<ProductsListItems> list = new List<ProductsListItems>();
            list = (from l in obj.MNT_Products orderby l.ProductDisplayName where l.ProductStatus == 1 select new ProductsListItems { ProductId = l.ProductId, ProductCode = l.ProductCode, ProductDisplayName = l.ProductDisplayName }).ToList();
            if (addAll)
            {
                list.Insert(0, new ProductsListItems() { ProductCode = "", ProductDisplayName = "[Select...]" });
            }
            return list;
        }

        public static List<ProductsListItems> FetchCodeDescription(bool addAll = true) {
            MCASEntities obj = new MCASEntities();
            List<ProductsListItems> list = new List<ProductsListItems>();
            list = (from l in obj.MNT_Products where l.ProductStatus == 1 orderby l.ProductCode select new ProductsListItems { ProductId = l.ProductId, ProductCode = l.ProductCode, ProductDisplayName = l.ProductCode + " - " + l.ProductDisplayName }).ToList();
            if (addAll)
            {
                list.Insert(0, new ProductsListItems() { ProductCode = "", ProductDisplayName = "[Select...]" });
            }
            return list;
        }

        public static List<ProductsListItems> FetchSubCodeDescription(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<ProductsListItems> list = new List<ProductsListItems>();
            list = (from l in obj.MNT_ProductClass join m in obj.MNT_Products on l.ProdCode equals m.ProductCode where l.Status == "1" orderby l.ClassCode select new ProductsListItems { Id = l.ID, ClassCode = l.ClassCode, ClassDescrption = l.ClassCode + " - " + l.ClassDesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new ProductsListItems() { ClassCode = "", ClassDescrption = "[Select...]" });
            }
            return list;
        }

        #endregion
    }


    

    

    
    
}
