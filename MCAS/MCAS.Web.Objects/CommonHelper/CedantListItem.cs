using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
namespace MCAS.Web.Objects.CommonHelper
{
    
   public class CedantListItem
    {
        #region Properties
        public int CedantId { get; set; }
        public string CedantCode { get; set; }
        public string CedantName { get; set; }
        #endregion

        #region static methods
        public static List<CedantListItem> Fetch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<CedantListItem> list = new List<CedantListItem>();
            list = (from l in obj.MNT_Cedant select new CedantListItem { CedantId = l.CedantId, CedantName = l.CedantName, CedantCode = l.CedantCode }).ToList();
            if (addAll)
            {
                list.Insert(0, new CedantListItem() { CedantCode ="", CedantName = "[Select..]" });
            }
            return list;
        }
        #endregion
    }
}
