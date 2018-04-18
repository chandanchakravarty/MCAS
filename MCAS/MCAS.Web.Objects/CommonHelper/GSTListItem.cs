using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
  public class GSTListItem:BaseModel
    {
      

      #region Properties
      public int id { get; set; }
      public string GSTCode { get; set; }
      public string GSTDesc { get; set; }
      public decimal Rate { get; set; }
      #endregion

      #region Static methods
      public static List<GSTListItem> Fetch(bool AddAll = true)
      {
          MCASEntities _db = new MCASEntities();
          List<GSTListItem> list = new List<GSTListItem>();
          list = (from x in _db.MNT_GST orderby x.GSTDesc select new GSTListItem {id=x.Id, GSTCode = x.GSTCode, GSTDesc = x.GSTDesc,Rate=x.Rate }).ToList();
          if (AddAll)
          {
              list.Insert(0, new GSTListItem() { id = 0, GSTDesc = "[Select...]" });
          }
          return list;
      }
      #endregion

    }
}
