using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
  public class SubClassListItem
  {
      #region properties
      public int ID { get; set; }
      public string ClassCode { get; set; }
      public string ClassDesc { get; set; }
      #endregion

      #region static methods
      public static List<SubClassListItem> Fetch(bool addAll = true)
      {
          MCASEntities _db = new MCASEntities();
          List<SubClassListItem> list = new List<SubClassListItem>();
          list = (from l in _db.MNT_ProductClass select new SubClassListItem { ID = l.ID, ClassCode = l.ClassCode, ClassDesc = l.ClassDesc }).ToList();
          if (addAll)
          {
              list.Insert(0, new SubClassListItem() { ClassCode = "", ClassDesc = "[Select..]" });
          }
          return list;
      }   

      #endregion
  }
}
