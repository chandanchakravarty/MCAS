using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
   public class DiaryListType:BaseModel
    {
       MCASEntities _db = new MCASEntities();

        #region type properties

       public string TypeId { get; set; }
       public string TypeDescription { get; set; }
       public string Type_flag { get; set; }


        #endregion

       

       #region Static Methods
       public static List<DiaryListType> Fetch(bool AddAll = true)
       {
           MCASEntities _db = new MCASEntities();
           List<DiaryListType> list = new List<DiaryListType>();
           list = (from x in _db.TODODIARYLISTTYPES orderby x.TYPEDESC select new DiaryListType { TypeId = System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.TYPEID).Trim(), TypeDescription = x.TYPEDESC }).ToList();
           if (AddAll)
           {
               list.Insert(0, new DiaryListType() { TypeId = "", TypeDescription = "[Select...]" });
           }
           return list;
       }
        #endregion
    }
}
