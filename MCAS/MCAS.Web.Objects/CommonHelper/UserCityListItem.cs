using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
  public class UserCityListItem:BaseModel
  {

      MCASEntities _db = new MCASEntities();
      #region Properties
      public int Cityid { get; set; }
      public string CityCode { get; set; }
      public string CityName { get; set; }
#endregion

      #region Static methods
      public static List<UserCityListItem> Fetch(bool AddAll = true)
      {
          MCASEntities _db = new MCASEntities();
          List<UserCityListItem> list = new List<UserCityListItem>();
          list = (from x in _db.MNT_City orderby x.CityName select new UserCityListItem { Cityid = x.CityId, CityCode = x.CityCode, CityName = x.CityName }).ToList();
          if (AddAll) {
              list.Insert(0, new UserCityListItem() { CityCode = "", CityName = "[Select...]" });
          }
          return list;
      }
      #endregion
  }
}
