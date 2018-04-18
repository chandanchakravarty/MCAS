using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
   public class ProviceList:BaseModel
    {
       MCASEntities _db = new MCASEntities();

       #region Properties
       public int TranId { get; set; }
       public string ProvinceCode { get; set; }
       public string ProvinceName { get; set; }
       #endregion

       #region Static methods
       public static List<ProviceList> Fetch(bool AddAll = true)
       {
           MCASEntities _db = new MCASEntities();
           List<ProviceList> list = new List<ProviceList>();
           list = (from x in _db.MNT_Province select new ProviceList { TranId=x.TranId, ProvinceCode = x.ProvinceCode, ProvinceName = x.ProvinceName }).ToList();
           if (AddAll)
           {
               list.Insert(0, new ProviceList() { ProvinceCode = "", ProvinceName = "[Select.....]" });
           }
           return list;
       }
       #endregion
    }
}
