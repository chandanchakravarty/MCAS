using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
   public class VehicleListItem
    {
       MCASEntities _db = new MCASEntities();

        #region Properties
       public int TranId { get; set; }
       public string MakeCode { get; set; }
       public string MakeName { get; set; }
       public string VehicleClassCode { get; set; }
       public string VehicleClassDesc { get; set; }
       public string VehicleModelCode { get; set; }
       public string VehicleModelName { get; set; }
       public string Status { get; set; }

        #endregion

        #region Static 
       public static List<VehicleListItem> Fetch(bool addAll = true) {
           MCASEntities _db = new MCASEntities();
           List<VehicleListItem> list = new List<VehicleListItem>();
           list = (from x in _db.MNT_Motor_Make orderby x.MakeName where x.status=="Active" select new VehicleListItem { TranId = x.TranId, MakeCode = x.MakeCode, MakeName = x.MakeName,Status=x.status }).ToList();
           if (addAll)
           {
               list.Insert(0, new VehicleListItem() { MakeCode = "", MakeName = "[Select...]" });
           }
           return list;
       }

       public static List<VehicleListItem> FetchVehicleClass(bool addAll = true) {
           MCASEntities _db = new MCASEntities();
           List<VehicleListItem> list = new List<VehicleListItem>();
           list = (from x in _db.MNT_Motor_Class orderby x.VehicleClassDesc where x.Status.Trim() == "Active" select new VehicleListItem { TranId = x.TranId, VehicleClassCode = x.VehicleClassCode, VehicleClassDesc = x.VehicleClassDesc, Status = x.Status }).ToList();
           if (addAll)
           {
               list.Insert(0, new VehicleListItem() { VehicleClassCode = "-1", VehicleClassDesc = "[Select...]" });
           }
           return list;
       }

       public static List<VehicleListItem> FetchVehicleModel(bool addAll = true)
       {
           MCASEntities _db = new MCASEntities();
           List<VehicleListItem> list = new List<VehicleListItem>();
           list = (from x in _db.MNT_MOTOR_MODEL orderby x.ModelName where x.status.Trim() == "Active" select new VehicleListItem { TranId = x.TranId, VehicleModelCode = x.ModelCode, VehicleModelName = x.ModelName, Status = x.status }).ToList();
           if (addAll)
           {
               list.Insert(0, new VehicleListItem() { VehicleModelCode = "", VehicleModelName = "[Select...]" });
           }
           return list;
       }

       public static List<VehicleListItem> LoadVehicleMake(bool addAll = true)
       {
           return VehicleListItem.Fetch(addAll);
       }

        #endregion
    }
}
