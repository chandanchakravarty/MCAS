using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCAS.Web.Objects.CommonHelper
{
   public class SearchCriteria :BaseModel
    {
        #region properties

        private int _count = 0;
        public int? pageno { get; set; }
        public int datalength { get; set; }
        public string ClaimNo { get; set; }
        public string PolicyNo { get; set; }
        public string LossDate { get; set; }
        public string InsuredName { get; set; }
        public string mainClassCode { get; set; }
        public string subClassCode { get; set; }
        public string orgCountry { get; set; }
        public string organization { get; set; }
        public string claimMode { get; set; }
        public string claimStatus { get; set; }
        public string vehicleNo { get; set; }
        public string AccidentDateFrom { get; set; }
        public string AccidentDateTo { get; set; }
        public string ClaimRegisDateFrom { get; set; }
        public string ClaimRegisDateTo { get; set; }
        public string ClaimLegelCase { get; set; }
        public string ClaimOfficer { get; set; }
        public string IPNo { get; set; }
        public string ClaimantName { get; set; }
        public string VehicleRegnNo { get; set; }
        public int Count { get { return _count; } set { _count = value; } }
        public int currPage { get; set; }
       
        public string ClaimantType { get; set; }
        #region propertiesJobScheduleEnquiry

        #endregion
        public int SNo { get; set; }
        public string JobNo { get; set; }
        public string ProcessRefNo { get; set; }
        public string ScheduleStartDate { get; set; }
        public string ScheduleToDate { get; set; }
        public string JobTypedefault { get; set; }
        public string JobStatusDefault { get; set; }
        public string JobStartDate { get; set; }
        public string JobEndDate { get; set; }
        #endregion

        #region Methods



        #endregion
    }

  

    

}
