using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web;
using System.Web.Mvc;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimFinanceModel : BaseModel
    {
        #region Properties
        public int? FinanceId { get; set; }
        public int PolicyId { get; set; }
        public int ClaimId { get; set; }
        public decimal? OwnVehicleRepair { get; set; }
        public decimal? InvestigatorSurvey { get; set; }
        public decimal? TPProperty { get; set; }
        public decimal? TPPersonalInjury { get; set; }
        public decimal? OurSolicitorCost { get; set; }
        public decimal? OurProfessionalExperts { get; set; }
        public decimal? TPLegalCosts { get; set; }
        public decimal? Disbursements { get; set; }
        public decimal? OtherExpenses { get; set; }
        public decimal? TotalGroundUpExpenses { get; set; }
        public decimal? SurveyFees { get; set; }
        public decimal? ReinspectionFees { get; set; }
        public decimal? PteInvestigationFees { get; set; }
        public decimal? TPLossofUsePaid { get; set; }
        public decimal? TPCostofRepairPaid { get; set; }
        public decimal? TPLossofRentalPaid { get; set; }
        public decimal? TPDamagesPaid { get; set; }
        public decimal? TPMedicalExpensesPaid { get; set; }
        public decimal? TPFutureMedicalExpensesPaid { get; set; }
        public decimal? TPLossofEarningCapacityPaid { get; set; }
        public decimal? TPLossofEarningPaid { get; set; }
        public decimal? TPLossofFutureEarningsPaid { get; set; }
        public decimal? TPLOGMedicalExpensesPaid { get; set; }
        public decimal? TPMedicalReportFeesPaid { get; set; }
        public decimal? InterestPaid { get; set; }
        public decimal? MiscellaneousFeePaid { get; set; }
        public decimal? PublicTrusteeFeePaid { get; set; }
        #endregion
    }
}
