using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClmClaimQutmModel
    {
        //----For Claim Quantum grid
        private List<ClmClaimQutmModelCollection> _claimQntmModel;

        public int ClaimId { get; set; }
        public string ClaimRef { get; set; }
        public string TPVehicleNo { get; set; }
        public string TPInsurer { get; set; }
        public Decimal InAmt_I { get; set; }
        public Decimal InAmt_M { get; set; }
        public Decimal InAmt_C { get; set; }
        public string LOR_Noofdays { get; set; }
        public string LOI_Noofdays { get; set; }
        public string LOR_RatePerday { get; set; }
        public string LOI_RatePerday { get; set; }
        public Decimal LOR_I { get; set; }
        public Decimal LOR_M { get; set; }
        public Decimal LOR_C { get; set; }
        public Decimal LOI_I { get; set; }
        public Decimal LOI_M { get; set; }
        public Decimal LOI_C { get; set; }
        public Decimal Others1_I { get; set; }
        public Decimal Others1_M { get; set; }
        public Decimal Others1_C { get; set; }
        public Decimal LglFee_I { get; set; }
        public Decimal LglFee_M { get; set; }
        public Decimal LglFee_C { get; set; }
        public Decimal SrvFee_I { get; set; }
        public Decimal SrvFee_M { get; set; }
        public Decimal SrvFee_C { get; set; }
        public Decimal TPGIAFee_I { get; set; }
        public Decimal TPGIAFee_M { get; set; }
        public Decimal TPGIAFee_C { get; set; }
        public Decimal LTAFee_I { get; set; }
        public Decimal LTAFee_M { get; set; }
        public Decimal LTAFee_C { get; set; }
        public Decimal Others2_I { get; set; }
        public Decimal Others2_M { get; set; }
        public Decimal Others2_C { get; set; }
        public Decimal Total_I { get; set; }
        public Decimal Total_M { get; set; }
        public Decimal Total_C { get; set; }

      //For PTE Car Only
        public Decimal LOU_I { get; set; }
        public Decimal LOU_M { get; set; }
        public Decimal LOU_C { get; set; }

        public string LOU_Noofdays { get; set; }
        public string LOU_RatePerDay { get; set; }
        public string CrRntl_Noofdays { get; set; }
        public string CrRntl_RatePerDay { get; set; }
        public Decimal MdclExps_I { get; set; }
        public Decimal MdclExps_M { get; set; }
        public Decimal MdclExps_C { get; set; }
        public Decimal CrtsyCR_C { get; set; }
        public Decimal CrtsyCR_M { get; set; }
        public Decimal CrtsyCR_I { get; set; }
        public Decimal CrRntl_I { get; set; }
        public Decimal CrRntl_C { get; set; }
        public Decimal CrRntl_M { get; set; }
        

    }

    public class ClmClaimQutmModelCollection
    {
        #region "Object Properties"
        public Int64? OrgRecordNumber { get; set; }
        public string RecordNumber { get; set; }
        public int? ClaimID { get; set; }
        public string ClaimantName { get; set; }
        public int? ClaimType { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public Decimal? Total_O { get; set; }
        public int? ReserveId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public string HClaimantStatus { get; set; }
        public List<string> HeaderListCollection { get; set; }
        #endregion
    }
   
}
