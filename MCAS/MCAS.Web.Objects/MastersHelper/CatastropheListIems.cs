using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;

namespace MCAS.Web.Objects.MastersHelper
{
    public class CatastropheListIems :BaseModel
    {
        public int TranId { get; set; }
        public string CastropheCode { get; set; }
        public string CastropheDesc { get; set; }
        public string Product_Code { get; set; }
    }
}
