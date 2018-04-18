using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
    public class LookUpListItems
    {
        #region Properties
        public string Lookup_value { get; set; }
        public string Lookup_desc { get; set; }
        #endregion

        #region Static Methods
        public static List<LookUpListItems> Fetch(string category, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            list = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            }
            if (addNone)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "(None)" });
            }
            obj.Dispose();
            return list;
        }

        public static List<LookUpListItems> FetchOrganizationCategory(string category, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();

            var list = (from product in obj.MNT_OrgCountry
                        join l in obj.MNT_Lookups on product.InsurerType equals l.Lookupvalue
                        where !obj.MNT_Deductible.Any(dd => dd.OrgCategoryName == product.CountryOrgazinationCode)
                        && l.Category == category && l.IsActive == "Y"
                        select new LookUpListItems
                        {
                            Lookup_value = l.Lookupvalue,
                            Lookup_desc = l.Lookupdesc
                        }).Distinct().ToList();

            //List<LookUpListItems> list = new List<LookUpListItems>();
            //list = (from l in obj.MNT_Lookups
            //        //join m in lstOrgCategory on l.Lookupvalue equals lstOrgCategory
            //        where l.Category == category && l.IsActive == "Y"


            //        orderby l.Lookupdesc
            //        select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "0", Lookup_desc = "[Select...]" });
            }
            if (addNone)
            {
                list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "(None)" });
            }
            obj.Dispose();
            return list;
        }



        #endregion

    }
}
