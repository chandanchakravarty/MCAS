using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
  public class RiskTypeListItem
    {
      MCASEntities _db = new MCASEntities();
        #region Properties
      public int RiskTypeCode { get; set; }
      public string RiskTypeDesc { get; set; }
      public string MainClass { get; set; }
        #endregion

        #region Static Members
      public static List<RiskTypeListItem> Fetch(bool AddAll = true)
      {
          //x.MainClass == "ENG";

          MCASEntities _db = new MCASEntities();
          List<RiskTypeListItem> list = new List<RiskTypeListItem>();
          list = (from x in _db.MNT_RiskType select new RiskTypeListItem { RiskTypeCode = x.RiskTypeCode, RiskTypeDesc = x.RiskTypeDesc }).ToList();
          if (AddAll)
          {
              list.Insert(0, new RiskTypeListItem() { RiskTypeCode = 0, RiskTypeDesc = "[Select.....]" });
          }
          return list;
      }


        #endregion
    }
}
