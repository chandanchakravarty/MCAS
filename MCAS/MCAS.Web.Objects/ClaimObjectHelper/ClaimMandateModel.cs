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
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using MCAS.Web.Objects.Resources.Common;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimMandateModel : BaseModel
    {
        #region properties


        #region Common property
        public int? MandateId { get; set; }
        public int ReserveId { get; set; }
        public int AccidentClaimId { get; set; }
        public int ClaimID { get; set; }
        public int? SNO { get; set; }
        public string RecordNumber { get; set; }
        public Int64? OrgRecordNumber { get; set; }
        public string ClaimantName { get; set; }
        public int ClaimantType { get; set; }
        public string ReserveAmount { get; set; }
        public int? ClaimType { get; set; }
        public int? PolicyId { get; set; }
        public string IsActive { get; set; }
        public string MandateType { get; set; }
        public int? AssignedTo { get; set; }
        public int? InvestigationResult { get; set; }
        public string Scenario { get; set; }
        [Display(Name = "Inform Safety to review findings")]
        public string InformSafetytoreviewfindings { get; set; }
        public string Evidence { get; set; }
        public string RelatedFacts { get; set; }
        public string COAssessment { get; set; }
        public int SupervisorAssignto { get; set; }
        public string ApproveRecommedations { get; set; }
        public string mode { get; set; }
        public string SupervisorRemarks { get; set; }
        public string ApproveReco { get; set; }
        public string ClaimTypeCode { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string HAjustOrEdit { get; set; }
        public string Hshowgrid { get; set; }
        public string HGridImageSign { get; set; }
        public string HDeductibleAmt { get; set; }
        public string HTotalPreviousApproveMandate { get; set; }
        public string HFALAmtOD { get; set; }
        public string HFALAmtPDBI { get; set; }
        public string MandateRecordNo { get; set; }
        public string userid { get; set; }
        public string groupcode { get; set; }
        public decimal? SPFAL { get; set; }
        public string HClaimantStatus { get; set; }
        public int HUserId { get; set; }
        public string HAssignedTo { get; set; }
        public DateTime? HEffdatefrom { get; set; }
        public DateTime? HEffdateto { get; set; }
        public string HOrgname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date of Notice to Safety")]
        public DateTime? DateofNoticetoSafety { get; set; }

        public string ODStatus { get; set; }
        public string RecoverableFromInsurerBI { get; set; }
        public string EZLinkCardNo { get; set; }
        [IsInformedInsurer("ClaimType", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVInformedInsurer")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Informed Insurer")]
        public DateTime? InformedInsurer
        {
            get { return _informedInsurer; }
            set { _informedInsurer = value; }
        }
        public IEnumerable<SelectListItem> generallookupvalue { get; set; }
        public List<LookUpListItems> RecoverableFromInsurerBIList { get; set; }
        public List<LookUpListItems> EZLinkCardNolist { get; set; }
        private DateTime? _informedInsurer = null;
        //Ashish Rai
        public string ProposedLiability { get; set; }
        #endregion

        //#region Property Damage
        //[Required(ErrorMessage = "Cost of Repairs is required.")]
        //[DisplayName("Cost of Repairs")]
        //public decimal CostofRepairs_P { get; set; }

        //public decimal CostofRepairs_C { get; set; }
        //public decimal CostofRepairs_S { get; set; }

        //[Required(ErrorMessage = "Loss of Use is required.")]
        //[DisplayName("Loss of Use")]
        //public decimal LossofUseUn_P { get; set; }

        //public decimal LossofUseUn_C { get; set; }
        //public decimal LossofUseUn_S { get; set; }



        //[Required(ErrorMessage = "Loss of Earning is required.")]
        //[DisplayName("Loss of Earning")]
        //public decimal LossofEarning_P { get; set; }

        //public decimal LossofEarning_C { get; set; }
        //public decimal LossofEarning_S { get; set; }

        //[Required(ErrorMessage = "Loss of Rental is required.")]
        //[DisplayName("Loss of Rental")]
        //public decimal LossofRental_P { get; set; }

        //public decimal LossofRental_C { get; set; }
        //public decimal LossofRental_S { get; set; }

        //[Required(ErrorMessage = "Excess is required.")]
        //[DisplayName("Excess")]
        //public decimal Excess_P { get; set; }

        //public decimal Excess_C { get; set; }
        //public decimal Excess_S { get; set; }
        //[Required(ErrorMessage = "SubTotal is required.")]
        //[DisplayName("SubTotal")]
        //public decimal SubTotal_P { get; set; }
        //[Required(ErrorMessage = "SubTotal is required.")]
        //[DisplayName("SubTotal")]
        //public decimal SubTotal_C { get; set; }

        //[Required(ErrorMessage = "SubTotal is required.")]
        //[DisplayName("SubTotal")]
        //public decimal SubTotal_S { get; set; }


        //[Required(ErrorMessage = "Other Expense is required.")]
        //[DisplayName("Other Expense")]
        //public decimal OtherExpenses_Q { get; set; }
        //[Required(ErrorMessage = "Other Expense is required.")]
        //[DisplayName("Other Expense")]
        //public decimal OtherExpenses_P { get; set; }
        //public decimal OtherExpenses_C { get; set; }
        //[Required(ErrorMessage = "Other Expense is required.")]
        //[DisplayName("Other Expense")]
        //public decimal OtherExpenses_S { get; set; }


        //[Required(ErrorMessage = "Survey Fee is required.")]
        //[DisplayName("Survey Fee")]
        //public decimal SurveyFee_Q { get; set; }
        //public decimal SurveyFee_P { get; set; }
        //public decimal SurveyFee_C { get; set; }
        //public decimal SurveyFee_S { get; set; }



        //[Required(ErrorMessage = "Re-Survey Fee is required.")]
        //[DisplayName("Re-Survey Fee")]
        //public decimal ReSurveyFee_Q { get; set; }
        //public decimal ReSurveyFee_P { get; set; }
        //public decimal ReSurveyFee_C { get; set; }
        //public decimal ReSurveyFee_S { get; set; }

        //[Required(ErrorMessage = "LTA/GIA/Police Report Fees is required.")]
        //[DisplayName("LTA/GIA/Police Report Fees")]
        //public decimal LGPolRepFee_Q { get; set; }
        //public decimal LGPolRepFee_P { get; set; }
        //public decimal LGPolRepFee_C { get; set; }
        //public decimal LGPolRepFee_S { get; set; }



        //[Required(ErrorMessage = "3rd Party Lawyer's Cost is required.")]
        //[DisplayName("3rd Party Lawyer's Cost")]
        //public decimal ParLawCost3rd_Q { get; set; }
        //public decimal ParLawCost3rd_P { get; set; }
        //public decimal ParLawCost3rd_C { get; set; }
        //public decimal ParLawCost3rd_S { get; set; }


        //[Required(ErrorMessage = "3rd Party Lawyer's Disbursements is required.")]
        //[DisplayName("3rd Party Lawyer's Disbursements")]
        //public decimal ParLawDisbursements3rd_Q { get; set; }
        //public decimal ParLawDisbursements3rd_P { get; set; }
        //public decimal ParLawDisbursements3rd_C { get; set; }
        //public decimal ParLawDisbursements3rd_S { get; set; }

        //[Required(ErrorMessage = "Our Lawyer's Cost is required.")]
        //[DisplayName("Our Lawyer's Cost")]
        //public decimal OurLawyerCost_Q { get; set; }
        //public decimal OurLawyerCost_P { get; set; }
        //public decimal OurLawyerCost_C { get; set; }
        //public decimal OurLawyerCost_S { get; set; }


        //[Required(ErrorMessage = "Our Lawyer's Disbursements is required.")]
        //[DisplayName("Our Lawyer's Disbursements")]
        //public decimal OurLawDisbursements_Q { get; set; }
        //[DisplayName("Our Lawyer's Disbursements")]
        //public decimal OurLawDisbursements_P { get; set; }
        //[DisplayName("Our Lawyer's Disbursements")]
        //public decimal OurLawDisbursements_C { get; set; }
        //[DisplayName("Our Lawyer's Disbursements")]
        //public decimal OurLawDisbursements_S { get; set; }

        //[Required(ErrorMessage = "Total is required.")]
        //[DisplayName("Total")]
        //public decimal Total_Q { get; set; }
        //[Required(ErrorMessage = "Total is required.")]
        //[DisplayName("Total")]
        //public decimal Total_P { get; set; }
        //[Required(ErrorMessage = "Total is required.")]
        //[DisplayName("Total")]
        //public decimal Total_C { get; set; }
        //public decimal Total_S { get; set; }
        //public decimal Htotal { get; set; }
        //public decimal HtotalSp { get; set; }
        //public decimal HtotalPO { get; set; }
        //public decimal HtotalPC { get; set; }

        //#endregion

        //#region Bodily Injury
        //[Required(ErrorMessage = "General Damages is required.")]
        //[DisplayName("General Damages")]
        //public decimal GeneralDamages_P { get; set; }
        //[Required(ErrorMessage = "General Damages is required.")]
        //[DisplayName("General Damages")]
        //public decimal GeneralDamages_C { get; set; }
        //[Required(ErrorMessage = "General Damages is required.")]
        //[DisplayName("General Damages")]
        //public decimal GeneralDamages_S { get; set; }

        //[Required(ErrorMessage = "Medical Expenses is required.")]
        //[DisplayName("Medical Expenses")]
        //public decimal MedicalExpenses_P { get; set; }
        //[Required(ErrorMessage = "Medical Expenses is required.")]
        //[DisplayName("Medical Expenses")]
        //public decimal MedicalExpenses_C { get; set; }
        //[Required(ErrorMessage = "Medical Expenses is required.")]
        //[DisplayName("Medical Expenses")]
        //public decimal MedicalExpenses_S { get; set; }

        //[Required(ErrorMessage = "Future Medical Expenses is required.")]
        //[DisplayName("Future Medical Expenses")]
        //public decimal FutureMedicalExpenses_P { get; set; }
        //[Required(ErrorMessage = "Future Medical Expenses is required.")]
        //[DisplayName("Future Medical Expenses")]
        //public decimal FutureMedicalExpenses_C { get; set; }
        //[Required(ErrorMessage = "Future Medical Expenses is required.")]
        //[DisplayName("Future Medical Expenses")]
        //public decimal FutureMedicalExpenses_S { get; set; }

        //[Required(ErrorMessage = "Log Medical Expenses is required.")]
        //[DisplayName("Log Medical Expenses")]
        //public decimal LogMedicalExpenses_P { get; set; }
        //[Required(ErrorMessage = "Log Medical Expenses is required.")]
        //[DisplayName("Log Medical Expenses")]
        //public decimal LogMedicalExpenses_C { get; set; }
        //[Required(ErrorMessage = "Log Medical Expenses is required.")]
        //[DisplayName("Log Medical Expenses")]
        //public decimal LogMedicalExpenses_S { get; set; }

        //[Required(ErrorMessage = "Loss Of Earning Capacity is required.")]
        //[DisplayName("Loss Of Earning Capacity")]
        //public decimal LossofEarningCapacity_P { get; set; }
        //[Required(ErrorMessage = "Loss Of Earning Capacity is required.")]
        //[DisplayName("Loss Of Earning Capacity")]
        //public decimal LossofEarningCapacity_C { get; set; }
        //[Required(ErrorMessage = "Loss Of Earning Capacity is required.")]
        //[DisplayName("Loss Of Earning Capacity")]
        //public decimal LossofEarningCapacity_S { get; set; }

        //[Required(ErrorMessage = "Loss Of Earning is required.")]
        //[DisplayName("Loss Of Earning")]
        //public decimal LossofEarnings_P { get; set; }
        //[Required(ErrorMessage = "Loss Of Earning is required.")]
        //[DisplayName("Loss Of Earning")]
        //public decimal LossofEarnings_C { get; set; }
        //[Required(ErrorMessage = "Loss Of Earning is required.")]
        //[DisplayName("Loss Of Earning")]
        //public decimal LossofEarnings_S { get; set; }

        //[Required(ErrorMessage = "Loss Of Future Earnings is required.")]
        //[DisplayName("Loss Of Future Earnings")]
        //public decimal LossofFutureEarnings_P { get; set; }
        //[Required(ErrorMessage = "Loss Of Future Earnings is required.")]
        //[DisplayName("Loss Of Future Earnings")]
        //public decimal LossofFutureEarnings_C { get; set; }
        //[Required(ErrorMessage = "Loss Of Future Earnings is required.")]
        //[DisplayName("Loss Of Future Earnings")]
        //public decimal LossofFutureEarnings_S { get; set; }

        //[Required(ErrorMessage = "Transport is required.")]
        //[DisplayName("Transport")]
        //public decimal Transport_P { get; set; }
        //[Required(ErrorMessage = "Transport is required.")]
        //[DisplayName("Transport")]
        //public decimal Transport_C { get; set; }
        //[Required(ErrorMessage = "Transport is required.")]
        //[DisplayName("Transport")]
        //public decimal Transport_S { get; set; }


        //[Required(ErrorMessage = "Medical Report is required.")]
        //[DisplayName("Medical Report")]
        //public decimal MedicalRecord_P { get; set; }
        //[Required(ErrorMessage = "Medical Report is required.")]
        //[DisplayName("Medical Report")]
        //public decimal MedicalRecord_C { get; set; }
        //[Required(ErrorMessage = "Medical Report is required.")]
        //[DisplayName("Medical Report")]
        //public decimal MedicalRecord_S { get; set; }

        //[Required(ErrorMessage = "Public Trustee Fee is required.")]
        //[DisplayName("Public Trustee Fee")]
        //public decimal PublicTrusteeFee_P { get; set; }
        //[Required(ErrorMessage = "Public Trustee Fee is required.")]
        //[DisplayName("Public Trustee Fee")]
        //public decimal PublicTrusteeFee_C { get; set; }
        //[Required(ErrorMessage = "Public Trustee Fee is required.")]
        //[DisplayName("Public Trustee Fee")]
        //public decimal PublicTrusteeFee_S { get; set; }

        //[Required(ErrorMessage = "Our Professional Expert Fee is required.")]
        //[DisplayName("Our Professional Expert Fee")]
        //public decimal OurProfessionalExpertFee_P { get; set; }
        //[Required(ErrorMessage = "Our Professional Expert Fee is required.")]
        //[DisplayName("Our Professional Expert Fee")]
        //public decimal OurProfessionalExpertFee_C { get; set; }
        //[Required(ErrorMessage = "Our Professional Expert Fee is required.")]
        //[DisplayName("Our Professional Expert Fee")]
        //public decimal OurProfessionalExpertFee_S { get; set; }

        //#endregion

        //#region Own damage


        //public decimal CostofRepairs_Q { get; set; }
        //public decimal LossofUseUn_Q { get; set; }

        //#endregion

        //#region Liability

        ////public decimal? Liability_A { get; set; }
        ////public decimal? Liability_R { get; set; }
        ////public decimal? Liability_C { get; set; }
        ////public decimal? Liability_S { get; set; }

        //#endregion

        #region Remarks
        public string Remarks { get; set; }
        #endregion

        //#region 3rd Party Counter-offer
        //public decimal CostofRepairs_A { get; set; }
        //public int? Liability_A { get; set; }
        //public decimal LossofUseUn_A { get; set; }
        //public decimal LossofEarning_A { get; set; }
        //public decimal LossofRental_A { get; set; }
        //public decimal Excess_A { get; set; }
        //public decimal SubTotal_A { get; set; }
        //public decimal OtherExpenses_A { get; set; }
        //public decimal SurveyFee_A { get; set; }
        //public decimal ReSurveyFee_A { get; set; }
        //public decimal LGPolRepFee_A { get; set; }
        //public decimal ParLawCost3rd_A { get; set; }
        //public decimal ParLawDisbursements3rd_A { get; set; }
        //public decimal OurLawyerCost_A { get; set; }
        //public decimal OurLawDisbursements_A { get; set; }
        //public decimal Total_A { get; set; }
        //public decimal GeneralDamages_A { get; set; }
        //public decimal MedicalExpenses_A { get; set; }
        //public decimal FutureMedicalExpenses_A { get; set; }
        //public decimal LogMedicalExpenses_A { get; set; }
        //public decimal LossofEarningCapacity_A { get; set; }
        //public decimal LossofEarnings_A { get; set; }
        //public decimal LossofFutureEarnings_A { get; set; }
        //public decimal Transport_A { get; set; }
        //public decimal MedicalRecord_A { get; set; }
        //public decimal PublicTrusteeFee_A { get; set; }
        //public decimal OurProfessionalExpertFee_A { get; set; }
        //#endregion

        //#region Our Previous Offers
        //public decimal CostofRepairs_R { get; set; }
        //public int? Liability_R { get; set; }
        //public decimal LossofUseUn_R { get; set; }
        //public decimal LossofEarning_R { get; set; }
        //public decimal LossofRental_R { get; set; }
        //public decimal Excess_R { get; set; }
        //public decimal SubTotal_R { get; set; }
        //public decimal OtherExpenses_R { get; set; }
        //public decimal SurveyFee_R { get; set; }
        //public decimal ReSurveyFee_R { get; set; }
        //public decimal LGPolRepFee_R { get; set; }
        //public decimal ParLawCost3rd_R { get; set; }
        //public decimal ParLawDisbursements3rd_R { get; set; }
        //public decimal OurLawyerCost_R { get; set; }
        //public decimal OurLawDisbursements_R { get; set; }
        //public decimal Total_R { get; set; }
        //public decimal GeneralDamages_R { get; set; }
        //public decimal MedicalExpenses_R { get; set; }
        //public decimal FutureMedicalExpenses_R { get; set; }
        //public decimal LogMedicalExpenses_R { get; set; }
        //public decimal LossofEarningCapacity_R { get; set; }
        //public decimal LossofEarnings_R { get; set; }
        //public decimal LossofFutureEarnings_R { get; set; }
        //public decimal Transport_R { get; set; }
        //public decimal MedicalRecord_R { get; set; }
        //public decimal PublicTrusteeFee_R { get; set; }
        //public decimal OurProfessionalExpertFee_R { get; set; }
        //#endregion

        #region properties for new mandate
        public decimal? PreMandate { get; set; }
        public decimal? MovementMandate { get; set; }
        public decimal? CurrentMandate { get; set; }
        public int? PaymentId { get; set; }
        public decimal? PreMandateSP { get; set; }
        public decimal? MovementMandateSP { get; set; }
        public decimal? CurrentMandateSP { get; set; }
        public decimal? PreviousOffers { get; set; }
        public decimal? TPCounterOffer { get; set; }

        private List<ClaimMandateDetails> _mandateDetails = new List<ClaimMandateDetails>();

        public List<ClaimMandateDetails> MandateDetails
        {
            get { return _mandateDetails.ToList<ClaimMandateDetails>(); }
            set { _mandateDetails.AddRange(value); }
        }

        public string ResultMessage { get; set; }
        #endregion

        #region Override Properties
        public override string screenId
        {
            get
            {
                return "138";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "138";
            }

        }
        #endregion

        public List<CommonUtilities.CommonType> MandateTypeList { get; set; }
        public List<CommonUtilities.CommonType> AssignTypeListSP { get; set; }
        public List<CommonUtilities.CommonType> AssignTypeListCO { get; set; }
        public List<LookUpListItems> ClaimTypeList { get; set; }
        public List<LookUpListItems> investigationTypeList { get; set; }
        public List<LookUpListItems> EvidenceList { get; set; }
        public List<LookUpListItems> ApproveRecoList { get; set; }
        public List<LookUpListItems> InformSafetytoreviewfindingsList { get; set; }
        public List<ClaimMandateModel> MandateList { get; set; }
        public List<ClaimMandateModel> MandateClaimantlist { get; set; }



        private static IList<string> GetPropertyNames(Type sourceType)
        {
            List<string> result = new List<string>();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(sourceType);
            foreach (PropertyDescriptor item in props)
                if (item.IsBrowsable)
                    result.Add(item.Name);
            return result;
        }
        public IList<SelectListItem> EvidenceNames { get; set; }

        public ClaimReserveModel Reserve { get; set; }

        #endregion

        #region static methods

        public static List<CommonUtilities.CommonType> FetchMandateList()
        {
            MCASEntities obj = new MCASEntities();
            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            list.Insert(0, new CommonUtilities.CommonType() { Text = "[Select...]" });
            var list1 = (from l in obj.MNT_Lookups
                         where l.Category == "MandateType"
                         select new CommonUtilities.CommonType { Id = l.Lookupvalue, Text = l.Lookupdesc.Trim() }).ToList();
            list.AddRange(list1);
            obj.Dispose();
            return list;
        }

        public static List<CommonUtilities.CommonType> FetchAssignToList(string groupCode)
        {
            MCASEntities obj = new MCASEntities();
            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            list.Insert(0, new CommonUtilities.CommonType() { Text = "[Select...]" });
            var list1 = (from l in obj.MNT_ClaimOfficerDetail
                         where l.UserGroup == groupCode
                         select new CommonUtilities.CommonType { intID = l.TranId, Text = l.ClaimentOfficerName.Trim() }).ToList();
            list.AddRange(list1);
            obj.Dispose();
            return list;
        }

        public static List<CommonUtilities.CommonType> FetchAssignToListFromUserAdmin(string RoleCode)
        {
            MCASEntities obj = new MCASEntities();

            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            list.Insert(0, new CommonUtilities.CommonType() { Text = "[Select...]" });

            var ClaimOfficerDetails = obj.Proc_GetClaimOfficerByRole(RoleCode).ToList();

            var list1 = (from n in ClaimOfficerDetails
                         select new CommonUtilities.CommonType()
                         {
                             intID = n.SNo,
                             Text = n.UserDispName.Trim()
                         }
                        ).ToList();

            list.AddRange(list1);
            obj.Dispose();
            return list;

        }

        public static List<ClaimInfoModel> FetchEZlinkCardNo(int accidentclaimid, int claimid)
        {
            MCASEntities obj = new MCASEntities();

            List<ClaimInfoModel> list = new List<ClaimInfoModel>();

            var list1 = (from n in obj.CLM_Claims
                         where n.ClaimID == claimid && n.AccidentClaimId == accidentclaimid
                         select new ClaimInfoModel()
                         {
                             ClaimID = n.ClaimID,
                             EZLinkCardNo = n.EZLinkCardNo
                         }).ToList();
            list.AddRange(list1);
            obj.Dispose();
            return list;
        }

        public static List<CommonUtilities.CommonType> FetchClaimType()
        {

            MCASEntities obj = new MCASEntities();
            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            list.Insert(0, new CommonUtilities.CommonType() { Text = "[Select...]" });
            var list1 = (from l in obj.MNT_Lookups
                         where l.Category == "ClaimantType"
                         select new CommonUtilities.CommonType { Id = l.Lookupvalue, Text = l.Lookupdesc }).ToList();
            list.AddRange(list1);
            obj.Dispose();
            return list;
        }

        public static List<CommonUtilities.CommonType> SelectOnlyList()
        {
            var item = new List<CommonUtilities.CommonType>();
            item.Add(new CommonUtilities.CommonType { Id = "Select", Text = "Select" });
            return item;
        }

        public static IEnumerable<SelectListItem> fetchApproveList()
        {
            List<ClaimStatus> list = new List<ClaimStatus>();
            list.Add(new ClaimStatus() { ID = 1, Name = "Yes" });
            list.Add(new ClaimStatus() { ID = 2, Name = "No" });
            SelectList sl = new SelectList(list, "ID", "Name");
            return sl;
        }

        public static ClaimReserveModel FetchReserve(int AccidentClaimId, int ClaimId, int ClaimType, string ViewMode, String MandateId)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                var mid = MandateId == "" ? 0 : Convert.ToInt32(MandateId);
                var reserveid = ViewMode != "Select" ? (from rs in db.CLM_ReserveSummary
                                                        where rs.AccidentClaimId == AccidentClaimId && rs.ClaimID == ClaimId && rs.ClaimType == ClaimType
                                                        orderby rs.ReserveId descending
                                                        select rs.ReserveId).FirstOrDefault() : (from rs in db.CLM_MandateSummary
                                                                                                 where rs.MandateId == mid
                                                                                                 select rs.ReserveId).FirstOrDefault();
                var finReserve = (from crd in db.CLM_ReserveDetails where crd.ReserveId == reserveid select crd).ToList();
                ClaimReserveModel reserveModel = new ClaimReserveModel();
                var ReserveDetailList = (from details in db.CLM_ReserveDetails join lk in db.MNT_Lookups on details.CmpCode equals lk.Lookupvalue orderby lk.DisplayOrder ascending where details.ReserveId == reserveid && lk.Category == "TranComponent" select details);
                if (ReserveDetailList.Any())
                {
                    reserveModel.ReserveDetails = (from detail in ReserveDetailList
                                                   select new ClaimReserveDetails()
                                                   {
                                                       ReserveDetailId = detail.ReserveDetailID,
                                                       ReserveId = detail.ReserveId,
                                                       AccidentClaimId = detail.AccidentClaimId,
                                                       CompCode = detail.CmpCode.Trim(),
                                                       CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == detail.CmpCode select l.Description).FirstOrDefault(),
                                                       InitialReserve = detail.InitialReserve,
                                                       PreviousReserve = detail.PreReserve,
                                                       MovementReserve = detail.MovementReserve,
                                                       CurrentReserve = detail.CurrentReserve,
                                                       PaymentId = detail.PaymentId
                                                   }
                            ).ToList();

                    var remainlist = (from lk in db.MNT_Lookups where lk.IsActive == "Y" && lk.Category == "TranComponent" select lk).Where(p => !(from details in db.CLM_ReserveDetails where details.ReserveId == reserveid select details).Any(p2 => p2.CmpCode == p.Lookupvalue)).OrderBy(x => x.DisplayOrder).ToList();

                    if (remainlist.Any())
                    {
                        var list = reserveModel.ReserveDetails;
                        foreach (var remain in remainlist)
                        {
                            var Position = (from lk in db.MNT_Lookups where lk.IsActive == "Y" && lk.Category == "TranComponent" orderby lk.DisplayOrder select lk).ToList().FindIndex(c => c.DisplayOrder == remain.DisplayOrder);
                            list.Insert(Convert.ToInt32(Position), new ClaimReserveDetails()
                            {
                                ReserveDetailId = (from ln in reserveModel.ReserveDetails select ln.ReserveDetailId).FirstOrDefault(),
                                ReserveId = (from ln in reserveModel.ReserveDetails select ln.ReserveId).FirstOrDefault(),
                                AccidentClaimId = (from ln in reserveModel.ReserveDetails select ln.AccidentClaimId).FirstOrDefault(),
                                CompCode = remain.Lookupvalue,
                                CompDesc = remain.Description,
                                InitialReserve = 0.00m,
                                PreviousReserve = 0.00m,
                                MovementReserve = 0.00m,
                                CurrentReserve = 0.00m,
                                PaymentId = (from ln in reserveModel.ReserveDetails select ln.PaymentId).FirstOrDefault()
                            });
                        }
                        reserveModel._reserveDetails.Clear();
                        reserveModel.GetType().GetProperty("ReserveDetails").SetValue(reserveModel, list, null);
                    }
                }
                else
                {
                    var CompCodeList = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" orderby l.DisplayOrder ascending select l.Lookupvalue).ToList();
                    var TranList = new List<ClaimReserveDetails>();
                    for (var i = 0; i < CompCodeList.Count; i++)
                    {
                        var desc = Convert.ToString(CompCodeList[i]);
                        TranList.Add(new ClaimReserveDetails()
                        {
                            ReserveDetailId = 0,
                            ReserveId = 0,
                            AccidentClaimId = AccidentClaimId,
                            CompCode = CompCodeList[i].Trim(),
                            CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == desc select l.Description).FirstOrDefault(),
                            InitialReserve = 0.00m,
                            PreviousReserve = 0.00m,
                            MovementReserve = 0.00m,
                            CurrentReserve = 0.00m,
                            PaymentId = 0
                        });
                    }
                    reserveModel.ReserveDetails = TranList.ToList();
                }
                return reserveModel;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
        }

        public static List<ClaimMandateModel> FetchMandatelist(string AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var item = new List<ClaimMandateModel>();
            try
            {
                item = (from data in db.Proc_GetClmClaimentMandateList(AccidentClaimId).Where(m => m.ClaimantName != null).ToList()
                        select new ClaimMandateModel()
                        {
                            SNO = string.IsNullOrEmpty(data.MandateRecordNo) ? Convert.ToInt32(data.MandateRecordNo) : Convert.ToInt32(data.MandateRecordNo.Split('-')[1]),
                            OrgRecordNumber = data.RecordNumber,
                            RecordNumber = data.ClaimRecordNo,
                            ClaimID = data.ClaimID,
                            ClaimantName = data.ClaimantName,
                            ClaimType = Convert.ToInt32(data.ClaimType),
                            AccidentClaimId = data.AccidentClaimId,
                            PolicyId = data.PolicyId,
                            ReserveId = Convert.ToInt32(data.ReserveId),
                            MandateId = data.MandateId == null ? 0 : data.MandateId,
                            ClaimTypeCode = data.ClaimTypeCode,
                            ClaimTypeDesc = data.ClaimType.ToString() == "1" ? Common._1 : data.ClaimType.ToString() == "2" ? Common._2 : data.ClaimType.ToString() == "3" ? Common._3 : "",
                            MandateRecordNo = data.MandateRecordNo,
                            ApproveRecommedations = data.ApproveRecommedations == null ? "" : (data.ApproveRecommedations == "No" || data.ApproveRecommedations == "N") ? "Rejected" : "Approved",
                            mode = (from x in db.ClaimAccidentDetails where x.AccidentClaimId == data.AccidentClaimId select x.IsComplete).FirstOrDefault() == 2 ? "Adj" : "",
                            HClaimantStatus = data.ClaimantStatus,
                            HAssignedTo = Convert.ToString((from m in db.CLM_MandateSummary where m.MandateId == data.MandateId select m.AssignedTo).FirstOrDefault()),
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return item;
        }

        public static ClaimMandateModel FetchMandateModel(ClaimMandateModel model, string ViewMode, String MandateId)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                model.Reserve = ClaimMandateModel.FetchReserve(Convert.ToInt32(model.AccidentClaimId), Convert.ToInt32(model.ClaimID), Convert.ToInt32(model.ClaimType), ViewMode, MandateId);
                model.MandateTypeList = ClaimMandateModel.FetchMandateList();

                model.AssignTypeListSP = ClaimMandateModel.FetchAssignToListFromUserAdmin("SP");
                var sno = Convert.ToInt32((from l in db.CLM_Claims where l.AccidentClaimId == model.AccidentClaimId && l.ClaimID == model.ClaimID select l.ClaimsOfficer).FirstOrDefault());
                model.AssignTypeListCO = (from l in ClaimMandateModel.FetchAssignToListFromUserAdmin("CO") where l.intID == sno select l).FirstOrDefault() == null ? (from l in ClaimMandateModel.FetchAssignToListFromUserAdmin("CO") where l.intID == 0 select l).ToList() : (from l in ClaimMandateModel.FetchAssignToListFromUserAdmin("CO") where l.intID == sno select l).ToList();
                model.HDeductibleAmt = String.IsNullOrEmpty(Convert.ToString((from deductamt in db.Proc_GetDeductibleAmt(Convert.ToInt32(model.AccidentClaimId)) select deductamt.DeductibleAmt).FirstOrDefault())) ? "0" : Convert.ToString((from deductamt in db.Proc_GetDeductibleAmt(Convert.ToInt32(model.AccidentClaimId)) select deductamt.DeductibleAmt).FirstOrDefault());

                model.HEffdatefrom =
                    ((from fromdate in db.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select fromdate.EffectiveFrom).FirstOrDefault()).HasValue ?
                    (from fromdate in db.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select fromdate.EffectiveFrom).FirstOrDefault() : null;

                model.HEffdateto =
                    ((from todate in db.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select todate.EffectiveTo).FirstOrDefault()).HasValue ?
                    (from todate in db.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select todate.EffectiveTo).FirstOrDefault() : null;

                model.HOrgname = String.IsNullOrEmpty(Convert.ToString((from orgname in db.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select orgname.OrganizationName).FirstOrDefault())) ? "" : Convert.ToString((from orgname in db.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select orgname.OrganizationName).FirstOrDefault());


                model.HFALAmtOD = Convert.ToString((from usr in db.MNT_Users
                                                    join fal in db.MNT_FAL on usr.FAL_OD equals fal.FALId
                                                    where usr.UserId == model.userid
                                                    select fal.Amount).FirstOrDefault());
                model.HFALAmtPDBI = Convert.ToString((from fal in db.MNT_FAL
                                                      join usr in db.MNT_Users on fal.FALId equals usr.FAL_PDBI
                                                      where usr.UserId == model.userid
                                                      select fal.Amount).FirstOrDefault());
                model.HUserId = (from x in db.MNT_Users where x.UserId == model.userid select x.SNo).FirstOrDefault();

                model.ODStatus = (from x in db.ClaimAccidentDetails where x.AccidentClaimId == model.AccidentClaimId select x.ODStatus).FirstOrDefault();
                model.InformedInsurer = (from x in db.CLM_Claims where x.ClaimID == model.ClaimID select x.InformedInsurer).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return model;
        }

        public static ClaimMandateModel GetTotalAmountPaid(ClaimMandateModel model)
        {
            MCASEntities db = new MCASEntities();
            var SumTotalCurrentMandateSP = (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP != null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandateSP).Sum()
                == null
                ?
                0.00m
                :
                (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP != null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandateSP).Sum();

            var SumTotalCurrentMandate = (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP == null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandate).Sum()
                == null
                ?
                0.00m
                :
                (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP == null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandate).Sum();


            model.HTotalPreviousApproveMandate = Convert.ToString(SumTotalCurrentMandateSP + SumTotalCurrentMandate);
            db.Dispose();
            return model;
        }

        public static string GetMandateRecordNo(int ClaimNo, int AccidentId)
        {
            MCASEntities db = new MCASEntities();
            var correctvech = (from p in db.CLM_MandateSummary
                               where p.ClaimType == ClaimNo && p.AccidentClaimId == AccidentId
                               select p).OrderByDescending(x => x.MandateId).Count();
            var res = ClaimNo == 1 ? "OD-" : ClaimNo == 2 ? "PD-" : ClaimNo == 3 ? "BI-" : "";
            var result = res + (correctvech + 1);
            db.Dispose();
            return result;
        }

        public static string GridButtonTextEditOrAssignOnMandate(string MandateId)
        {
            MCASEntities db = new MCASEntities();
            int Mid = Convert.ToInt32(MandateId);
            var GridButtonText = (from Mandate1 in db.CLM_MandateSummary join Mandate2 in db.CLM_MandateSummary on Mandate1.ClaimID equals Mandate2.ClaimID where Mandate1.MandateId == Mid orderby Mandate1.MandateId ascending select Mandate2.Modifieddate).FirstOrDefault();
            db.Dispose();
            return GridButtonText == null ? "F" : "T";
        }

        public static string GridButtonTextEditOrAssignAccToPaymentTab(string MandateId)
        {
            MCASEntities db = new MCASEntities();
            int Mid = Convert.ToInt32(MandateId);
            var GridButtonText = (from Mandate1 in db.CLM_MandateSummary where Mandate1.MandateId == Mid && Mandate1.MovementType == "P" && (Mandate1.ApproveRecommedations == "N" || Mandate1.ApproveRecommedations == null) select Mandate1).FirstOrDefault();
            db.Dispose();
            return GridButtonText == null ? "F" : "T";
        }

        public static string GridButtonTextEditOrAssignAccToApproveMandate(string MandateId)
        {
            MCASEntities db = new MCASEntities();
            int Mid = Convert.ToInt32(MandateId);
            var GridButtonText = (from Mandate1 in db.CLM_MandateSummary where Mandate1.MandateId == Mid && (Mandate1.ApproveRecommedations == "N" || Mandate1.ApproveRecommedations == null) select Mandate1).FirstOrDefault();
            db.Dispose();
            return GridButtonText == null ? "F" : "T";
        }

        public static string CheckClaimantFinalizeReserve(string AccidentClaimId, string ClaimID)
        {
            MCASEntities db = new MCASEntities();
            int Acc = Convert.ToInt32(AccidentClaimId);
            int Cid = Convert.ToInt32(ClaimID);
            var GridButtonText = (from Reserve in db.CLM_Reserve where Reserve.ClaimID == Cid && Reserve.AccidentId == Acc orderby Reserve.ReserveId descending select Reserve.Finalize).FirstOrDefault();
            db.Dispose();
            return (GridButtonText == null || GridButtonText == "N") ? "T" : "F";
        }

        public static string GetLast(string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static string ChkUserFALOD(string CreatedBy)
        {
            MCASEntities db = new MCASEntities();
            var gid = string.Empty;
            try
            {

                gid = (from x in db.MNT_Users
                       join y in db.MNT_FAL on x.FAL_OD equals y.FALId
                       where x.UserId == CreatedBy
                       select x.UserId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return gid;
        }

        public static string ChkUserFALPDBI(string CreatedBy)
        {
            MCASEntities db = new MCASEntities();
            var gid = string.Empty;
            try
            {

                gid = (from x in db.MNT_Users
                       join y in db.MNT_FAL on x.FAL_PDBI equals y.FALId
                       where x.UserId == CreatedBy
                       select x.UserId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return gid;
        }

        public static string Groupuser(string CreatedBy)
        {
            MCASEntities db = new MCASEntities();
            var gid = string.Empty;
            try
            {

                gid = (from x in db.MNT_Users
                       join y in db.MNT_GroupsMaster on x.GroupId equals y.GroupId
                       where x.UserId == CreatedBy
                       select y.GroupCode).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return gid;
        }

        public static string RoleCode(string CreatedBy)
        {
            MCASEntities db = new MCASEntities();
            var rid = string.Empty;
            try
            {

                rid = (from x in db.MNT_Users
                       join y in db.MNT_GroupsMaster on x.GroupId equals y.GroupId
                       where x.UserId == CreatedBy
                       select y.RoleCode).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return rid;
        }

        #endregion

        #region Public Methods

        //public ClaimMandateModel Save()
        //{
        //    MCASEntities obj = new MCASEntities();
        //    CLM_MandateSummary claimmandate = new CLM_MandateSummary();
        //    CLM_MandateDetails claimmandatedetails;
        //    string LoggedInUserId = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserId"]);
        //    this.CreatedBy = LoggedInUserId;
        //    if (obj.Connection.State == System.Data.ConnectionState.Closed)
        //        obj.Connection.Open();
        //    using (System.Data.Common.DbTransaction transaction = obj.Connection.BeginTransaction())
        //    {
        //        try
        //        {

        //            var manId = obj.CLM_MandateSummary.Where(x => x.MandateId == this.MandateId).FirstOrDefault();
        //            if (manId == null)
        //            {
        //                claimmandate.MandateId = 0;
        //                claimmandate.AccidentClaimId = this.AccidentClaimId;
        //                claimmandate.ReserveId = (from rs in obj.CLM_ReserveSummary
        //                                          where rs.AccidentClaimId == this.AccidentClaimId && rs.ClaimID == this.ClaimID
        //                                          orderby rs.ReserveId descending
        //                                          select rs.ReserveId).FirstOrDefault();
        //                claimmandate.ClaimID = this.ClaimID;
        //                claimmandate.ClaimType = Convert.ToInt32(this.ClaimType);
        //                claimmandate.MovementType = "I";
        //                claimmandate.AssignedTo = this.AssignedTo;
        //                claimmandate.InvestigationResult = this.InvestigationResult;
        //                claimmandate.Scenario = this.Scenario;
        //                claimmandate.Evidence = this.Evidence;
        //                claimmandate.RelatedFacts = this.RelatedFacts;
        //                claimmandate.COAssessment = this.COAssessment;
        //                claimmandate.SupervisorAssignto = this.SupervisorAssignto;
        //                claimmandate.ApproveRecommedations = this.ApproveRecommedations;
        //                claimmandate.SupervisorRemarks = this.SupervisorRemarks;
        //                claimmandate.InformSafetytoreviewfindings = this.InformSafetytoreviewfindings;


        //                claimmandate.Createdby = LoggedInUserId;
        //                var correctvech = (from mr in obj.CLM_MandateSummary where mr.AccidentClaimId == claimmandate.AccidentClaimId && mr.ClaimID == claimmandate.ClaimID && mr.ClaimType == claimmandate.ClaimType && mr.PaymentId == null orderby mr.MandateId descending select mr).Count();
        //                var result = "MD-" + (correctvech + 1);
        //                claimmandate.MandateRecordNo = result;
        //                if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
        //                {
        //                    claimmandate.PreMandate = 0.00m;
        //                    claimmandate.MovementMandate = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandate).FirstOrDefault();
        //                    claimmandate.CurrentMandate = (claimmandate.PreMandate + claimmandate.MovementMandate);
        //                }

        //                if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "SP" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
        //                {
        //                    claimmandate.PreMandateSP = 0.00m;
        //                    claimmandate.MovementMandateSP = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandateSP).FirstOrDefault();
        //                    claimmandate.CurrentMandateSP = (claimmandate.PreMandateSP + claimmandate.MovementMandateSP);
        //                }

        //                claimmandate.PreviousOffers = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.PreviousOffers).FirstOrDefault();
        //                claimmandate.TPCounterOffer = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.TPCounterOffer).FirstOrDefault();
        //                claimmandate.Createddate = DateTime.Now;
        //                claimmandate.Createdby = this.CreatedBy;
        //                claimmandate.IsActive = "Y";
        //                obj.CLM_MandateSummary.AddObject(claimmandate);
        //                obj.SaveChanges();
        //                this.MandateId = claimmandate.MandateId;
        //                #region InsertIntoMandateDetails
        //                for (int i = 0; i < this.MandateDetails.Count; i++)
        //                {
        //                    claimmandatedetails = new CLM_MandateDetails();
        //                    claimmandatedetails.MandateDetailId = 0;
        //                    claimmandatedetails.MandateId = Convert.ToInt32(this.MandateId);
        //                    claimmandatedetails.AccidentClaimId = Convert.ToInt32(this.AccidentClaimId);
        //                    claimmandatedetails.ClaimID = Convert.ToInt32(this.ClaimID);
        //                    claimmandatedetails.PreMandate = this.MandateDetails[i].PreMandate == null ? 0.00m : this.MandateDetails[i].PreMandate;
        //                    claimmandatedetails.PreMandateSP = this.MandateDetails[i].PreMandateSP == null ? 0.00m : this.MandateDetails[i].PreMandateSP;
        //                    claimmandatedetails.MovementMandate = this.MandateDetails[i].MovementMandate == null ? 0.00m : this.MandateDetails[i].MovementMandate;
        //                    claimmandatedetails.MovementMandateSP = this.MandateDetails[i].MovementMandateSP == null ? 0.00m : this.MandateDetails[i].MovementMandateSP;
        //                    claimmandatedetails.CurrentMandate = this.MandateDetails[i].CurrentMandate == null ? 0.00m : this.MandateDetails[i].CurrentMandate;
        //                    claimmandatedetails.CurrentMandateSP = this.MandateDetails[i].CurrentMandateSP == null ? 0.00m : this.MandateDetails[i].CurrentMandateSP;
        //                    claimmandatedetails.CmpCode = this.MandateDetails[i].CmpCode;
        //                    if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO")
        //                    {
        //                        bool Ifallequal = new[] { claimmandatedetails.PreMandate, claimmandatedetails.MovementMandate, claimmandatedetails.CurrentMandate }.Distinct().Count() == 1;
        //                        claimmandatedetails.MovementType = Ifallequal && (claimmandatedetails.PreMandate != 0.00m) ? "I" : (!Ifallequal && claimmandatedetails.PreMandate != claimmandatedetails.CurrentMandate) ? "M" : "";
        //                    }
        //                    else if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
        //                    {
        //                        bool Ifallequal = new[] { claimmandatedetails.PreMandateSP, claimmandatedetails.MovementMandateSP, claimmandatedetails.CurrentMandateSP }.Distinct().Count() == 1;
        //                        claimmandatedetails.MovementType = Ifallequal && (claimmandatedetails.PreMandateSP != 0.00m) ? "I" : (!Ifallequal && claimmandatedetails.PreMandateSP != claimmandatedetails.CurrentMandateSP) ? "M" : "";
        //                    }
        //                    claimmandatedetails.PreviousOffers = this.MandateDetails[i].PreviousOffers;
        //                    claimmandatedetails.TPCounterOffer = this.MandateDetails[i].TPCounterOffer;
        //                    claimmandatedetails.Createddate = DateTime.Now;
        //                    claimmandatedetails.Createdby = this.CreatedBy;
        //                    claimmandatedetails.IsActive = "Y";
        //                    claimmandatedetails.ReserveId = claimmandate.ReserveId;
        //                    obj.CLM_MandateDetails.AddObject(claimmandatedetails);
        //                    obj.SaveChanges();
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                claimmandate = obj.CLM_MandateSummary.Where(x => x.MandateId == this.MandateId).FirstOrDefault();
        //                claimmandate.ReserveId = (from rs in obj.CLM_ReserveSummary
        //                                          where rs.AccidentClaimId == this.AccidentClaimId && rs.ClaimID == this.ClaimID
        //                                          orderby rs.ReserveId descending
        //                                          select rs.ReserveId).FirstOrDefault();
        //                claimmandate.MovementType = "I";
        //                claimmandate.AssignedTo = this.AssignedTo;
        //                claimmandate.InvestigationResult = this.InvestigationResult;
        //                claimmandate.Scenario = this.Scenario;
        //                claimmandate.Evidence = this.Evidence;
        //                claimmandate.RelatedFacts = this.RelatedFacts;
        //                claimmandate.COAssessment = this.COAssessment;
        //                claimmandate.SupervisorAssignto = this.SupervisorAssignto;
        //                claimmandate.ApproveRecommedations = this.ApproveRecommedations;
        //                claimmandate.SupervisorRemarks = this.SupervisorRemarks;
        //                claimmandate.InformSafetytoreviewfindings = this.InformSafetytoreviewfindings;
        //                claimmandate.Modifiedby = LoggedInUserId;
        //                claimmandate.Modifieddate = DateTime.Now;
        //                if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
        //                {
        //                    claimmandate.PreMandate = 0.00m;
        //                    claimmandate.MovementMandate = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandate).FirstOrDefault();
        //                    claimmandate.CurrentMandate = (claimmandate.PreMandate + claimmandate.MovementMandate);
        //                }

        //                if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "SP" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
        //                {
        //                    claimmandate.PreMandateSP = 0.00m;
        //                    claimmandate.MovementMandateSP = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandateSP).FirstOrDefault();
        //                    claimmandate.CurrentMandateSP = (claimmandate.PreMandateSP + claimmandate.MovementMandateSP);
        //                }

        //                claimmandate.PreviousOffers = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.PreviousOffers).FirstOrDefault();
        //                claimmandate.TPCounterOffer = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.TPCounterOffer).FirstOrDefault();
        //                this.ModifiedOn = DateTime.Now;
        //                this.ModifiedBy = this.CreatedBy;
        //                claimmandate.IsActive = "Y";
        //                obj.SaveChanges();
        //                this.MandateId = claimmandate.MandateId;
        //                for (int i = 0; i < this.MandateDetails.Count; i++)
        //                {
        //                    var code = this.MandateDetails[i].CmpCode;
        //                    claimmandatedetails = (from m in obj.CLM_MandateDetails where m.MandateId == this.MandateId && m.CmpCode == code select m).FirstOrDefault();
        //                    claimmandatedetails.PreMandate = this.MandateDetails[i].PreMandate == null ? 0.00m : this.MandateDetails[i].PreMandate;
        //                    claimmandatedetails.PreMandateSP = this.MandateDetails[i].PreMandateSP == null ? 0.00m : this.MandateDetails[i].PreMandateSP;
        //                    claimmandatedetails.MovementMandate = this.MandateDetails[i].MovementMandate == null ? 0.00m : this.MandateDetails[i].MovementMandate;
        //                    claimmandatedetails.MovementMandateSP = this.MandateDetails[i].MovementMandateSP == null ? 0.00m : this.MandateDetails[i].MovementMandateSP;
        //                    claimmandatedetails.CurrentMandate = this.MandateDetails[i].CurrentMandate == null ? 0.00m : this.MandateDetails[i].CurrentMandate;
        //                    claimmandatedetails.CurrentMandateSP = this.MandateDetails[i].CurrentMandateSP == null ? 0.00m : this.MandateDetails[i].CurrentMandateSP;
        //                    claimmandatedetails.PreviousOffers = this.MandateDetails[i].PreviousOffers;
        //                    claimmandatedetails.TPCounterOffer = this.MandateDetails[i].TPCounterOffer;
        //                    claimmandatedetails.Modifieddate = DateTime.Now;
        //                    claimmandatedetails.Modifiedby = this.CreatedBy;
        //                    obj.SaveChanges();
        //                }
        //            }
        //            transaction.Commit();
        //            obj.Dispose();
        //            return this;

        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw (ex);
        //        }
        //        finally
        //        {
        //            obj.Dispose();
        //        }
        //    }
        //}

        public ClaimMandateModel Save()
        {
            CommonUtilities objCommonUtilities = new CommonUtilities();
            MCASEntities obj = new MCASEntities();
            if (obj.Connection.State == System.Data.ConnectionState.Closed)
                obj.Connection.Open();
            CLM_MandateSummary claimmandate = new CLM_MandateSummary();
            CLM_MandateSummary objMandateSummary = new CLM_MandateSummary();
            CLM_MandateDetails claimmandatedetails;
            List<CLM_MandateDetails> lstMandateDetails = new List<CLM_MandateDetails>();
            DataTable tableMandateSummary = new DataTable();
            DataTable tableMandateDetails = new DataTable();
            int returnValue = 0;
            string XMLDetails = "";
            string ActionSummary = MCASEntities.AuditActions.I.ToString();
            string ActionDetails = MCASEntities.AuditActions.I.ToString();
            string[] checkListDtails = { "PreMandate", "MovementMandate", "CurrentMandate", "PreMandateSP", "MovementMandateSP", "CurrentMandateSP", "PreviousOffers", "TPCounterOffer" };
            //string LoggedInUserId = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserId"]);
            string LoggedInUserId = this.userid;

            try
            {
                var exists = obj.CLM_MandateSummary.Where(x => x.MandateId == this.MandateId).FirstOrDefault();
                if (exists == null)
                {
                    claimmandate.AccidentClaimId = this.AccidentClaimId;
                    claimmandate.ReserveId = (from rs in obj.CLM_ReserveSummary
                                              where rs.AccidentClaimId == this.AccidentClaimId && rs.ClaimID == this.ClaimID
                                              orderby rs.ReserveId descending
                                              select rs.ReserveId).FirstOrDefault();
                    claimmandate.ClaimID = this.ClaimID;
                    claimmandate.ClaimType = Convert.ToInt32(this.ClaimType);
                    claimmandate.MovementType = "I";
                    claimmandate.AssignedTo = this.AssignedTo;
                    claimmandate.InvestigationResult = this.InvestigationResult;
                    claimmandate.Scenario = this.Scenario;
                    claimmandate.Evidence = this.Evidence;
                    claimmandate.RelatedFacts = this.RelatedFacts;
                    claimmandate.COAssessment = this.COAssessment;
                    claimmandate.SupervisorAssignto = this.SupervisorAssignto;
                    claimmandate.ApproveRecommedations = this.ApproveRecommedations;
                    claimmandate.SupervisorRemarks = this.SupervisorRemarks;
                    claimmandate.InformSafetytoreviewfindings = this.InformSafetytoreviewfindings;


                    claimmandate.DateofNoticetoSafety = this.DateofNoticetoSafety;
                    claimmandate.InformedInsurer = this.InformedInsurer;
                    claimmandate.EZLinkCardNo = this.EZLinkCardNo;
                    claimmandate.ODStatus = this.ODStatus;
                    claimmandate.RecoverableFromInsurerBI = this.RecoverableFromInsurerBI;

                    

                    var correctvech = (from mr in obj.CLM_MandateSummary where mr.AccidentClaimId == claimmandate.AccidentClaimId && mr.ClaimID == claimmandate.ClaimID && mr.ClaimType == claimmandate.ClaimType && mr.PaymentId == null orderby mr.MandateId descending select mr).Count();
                    var result = "MD-" + (correctvech + 1);
                    claimmandate.MandateRecordNo = result;
                    if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
                    {
                        claimmandate.PreMandate = 0.00m;
                        claimmandate.MovementMandate = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandate).FirstOrDefault();
                        claimmandate.CurrentMandate = (claimmandate.PreMandate + claimmandate.MovementMandate);
                    }

                    if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "SP" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
                    {
                        claimmandate.PreMandateSP = 0.00m;
                        claimmandate.MovementMandateSP = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandateSP).FirstOrDefault();
                        claimmandate.CurrentMandateSP = (claimmandate.PreMandateSP + claimmandate.MovementMandateSP);
                    }

                    var objMandateDetails = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail).FirstOrDefault();
                    if (objMandateDetails != null)
                    {
                        claimmandate.PreviousOffers = objMandateDetails.PreviousOffers;
                        claimmandate.TPCounterOffer = objMandateDetails.TPCounterOffer;
                    }
                    claimmandate.Createddate = DateTime.Now;
                    claimmandate.Createdby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    claimmandate.IsActive = "Y";
                    this.CreatedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    this.CreatedOn = DateTime.Now;
                    this.ResultMessage = "Record saved successfully.";
                    #region InsertIntoMandateDetails
                    for (int i = 0; i < this.MandateDetails.Count; i++)
                    {
                        claimmandatedetails = new CLM_MandateDetails();
                        claimmandatedetails.MandateId = Convert.ToInt32(this.MandateId);
                        claimmandatedetails.AccidentClaimId = Convert.ToInt32(this.AccidentClaimId);
                        claimmandatedetails.ClaimID = Convert.ToInt32(this.ClaimID);
                        claimmandatedetails.PreMandate = this.MandateDetails[i].PreMandate == null ? 0.00m : this.MandateDetails[i].PreMandate;
                        claimmandatedetails.PreMandateSP = this.MandateDetails[i].PreMandateSP == null ? 0.00m : this.MandateDetails[i].PreMandateSP;
                        claimmandatedetails.MovementMandate = this.MandateDetails[i].MovementMandate == null ? 0.00m : this.MandateDetails[i].MovementMandate;
                        claimmandatedetails.MovementMandateSP = this.MandateDetails[i].MovementMandateSP == null ? 0.00m : this.MandateDetails[i].MovementMandateSP;
                        claimmandatedetails.CmpCode = this.MandateDetails[i].CmpCode;

                        if (claimmandatedetails.CmpCode.Trim() == "Labl")
                        {
                            claimmandatedetails.CurrentMandate = this.MandateDetails[i].MovementMandate == null ? 0.00m : this.MandateDetails[i].MovementMandate;
                            claimmandatedetails.CurrentMandateSP = this.MandateDetails[i].MovementMandateSP == null ? 0.00m : this.MandateDetails[i].MovementMandateSP;
                        }
                        else
                        {
                            claimmandatedetails.CurrentMandate = this.MandateDetails[i].CurrentMandate == null ? 0.00m : this.MandateDetails[i].CurrentMandate;
                            claimmandatedetails.CurrentMandateSP = this.MandateDetails[i].CurrentMandateSP == null ? 0.00m : this.MandateDetails[i].CurrentMandateSP;
                        }

                        if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO")
                        {
                            bool Ifallequal = new[] { claimmandatedetails.PreMandate, claimmandatedetails.MovementMandate, claimmandatedetails.CurrentMandate }.Distinct().Count() == 1;
                            claimmandatedetails.MovementType = Ifallequal && (claimmandatedetails.PreMandate != 0.00m) ? "I" : (!Ifallequal && claimmandatedetails.PreMandate != claimmandatedetails.CurrentMandate) ? "M" : "";
                        }
                        else if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
                        {
                            bool Ifallequal = new[] { claimmandatedetails.PreMandateSP, claimmandatedetails.MovementMandateSP, claimmandatedetails.CurrentMandateSP }.Distinct().Count() == 1;
                            claimmandatedetails.MovementType = Ifallequal && (claimmandatedetails.PreMandateSP != 0.00m) ? "I" : (!Ifallequal && claimmandatedetails.PreMandateSP != claimmandatedetails.CurrentMandateSP) ? "M" : "";
                        }
                        claimmandatedetails.PreviousOffers = this.MandateDetails[i].PreviousOffers;
                        claimmandatedetails.TPCounterOffer = this.MandateDetails[i].TPCounterOffer;
                        claimmandatedetails.Createddate = DateTime.Now;
                        claimmandatedetails.Createdby = LoggedInUserId;
                        claimmandatedetails.IsActive = "Y";
                        this.ResultMessage = "Record saved successfully.";
                        claimmandatedetails.ReserveId = claimmandate.ReserveId;

                        lstMandateDetails.Add(claimmandatedetails);

                        string tempXML = objCommonUtilities.GenerateXMLForChangedColumns(null, claimmandatedetails, checkListDtails);
                        if (!string.IsNullOrEmpty(tempXML))
                        {
                            XMLDetails += "<Row Id=\"" + claimmandatedetails.CmpCode.Trim() + "\">" + tempXML;
                            XMLDetails += "</Row>";
                        }
                    }

                    #endregion
                }
                else
                {
                    objMandateSummary = exists.ShallowCopy();
                    ActionSummary = MCASEntities.AuditActions.U.ToString();
                    ActionDetails = MCASEntities.AuditActions.U.ToString();

                    claimmandate = obj.CLM_MandateSummary.Where(x => x.MandateId == this.MandateId).FirstOrDefault();
                    claimmandate.ReserveId = (from rs in obj.CLM_ReserveSummary
                                              where rs.AccidentClaimId == this.AccidentClaimId && rs.ClaimID == this.ClaimID
                                              orderby rs.ReserveId descending
                                              select rs.ReserveId).FirstOrDefault();
                    claimmandate.MovementType = "I";
                    claimmandate.AssignedTo = this.AssignedTo;
                    claimmandate.InvestigationResult = this.InvestigationResult;
                    claimmandate.Scenario = this.Scenario;
                    claimmandate.Evidence = this.Evidence;
                    claimmandate.RelatedFacts = this.RelatedFacts;
                    claimmandate.COAssessment = this.COAssessment;
                    claimmandate.SupervisorAssignto = this.SupervisorAssignto;
                    claimmandate.ApproveRecommedations = this.ApproveRecommedations;
                    claimmandate.SupervisorRemarks = this.SupervisorRemarks;
                    claimmandate.InformSafetytoreviewfindings = this.InformSafetytoreviewfindings;
                    claimmandate.Modifiedby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    claimmandate.Modifieddate = DateTime.Now;
                    claimmandate.DateofNoticetoSafety = this.DateofNoticetoSafety;
                    claimmandate.InformedInsurer = this.InformedInsurer;
                    claimmandate.EZLinkCardNo = this.EZLinkCardNo;
                    claimmandate.ODStatus = this.ODStatus;
                    claimmandate.RecoverableFromInsurerBI = this.RecoverableFromInsurerBI;

                    if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "CO" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
                    {
                        claimmandate.PreMandate = 0.00m;
                        claimmandate.MovementMandate = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandate).FirstOrDefault();
                        claimmandate.CurrentMandate = (claimmandate.PreMandate + claimmandate.MovementMandate);
                    }

                    if (ClaimMandateModel.Groupuser(Convert.ToString(LoggedInUserId)) == "SP" || ClaimMandateModel.RoleCode(Convert.ToString(LoggedInUserId)) == "COSP")
                    {
                        claimmandate.PreMandateSP = 0.00m;
                        claimmandate.MovementMandateSP = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail.MovementMandateSP).FirstOrDefault();
                        claimmandate.CurrentMandateSP = (claimmandate.PreMandateSP + claimmandate.MovementMandateSP);
                    }

                    var objMandateDetails = (from detail in this.MandateDetails where detail.CmpCode == "TOTAL" select detail).FirstOrDefault();
                    if (objMandateDetails != null)
                    {
                        claimmandate.PreviousOffers = objMandateDetails.PreviousOffers;
                        claimmandate.TPCounterOffer = objMandateDetails.TPCounterOffer;
                    }
                    this.ModifiedOn = DateTime.Now;
                    this.ModifiedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    claimmandate.IsActive = "Y";
                    this.ResultMessage = "Record updated successfully.";

                    for (int i = 0; i < this.MandateDetails.Count; i++)
                    {
                        var code = this.MandateDetails[i].CmpCode;
                        claimmandatedetails = (from m in obj.CLM_MandateDetails where m.MandateId == this.MandateId && m.CmpCode == code select m).FirstOrDefault();
                        if (claimmandatedetails == null)
                        {
                            claimmandatedetails = new CLM_MandateDetails();
                            claimmandatedetails.CmpCode = this.MandateDetails[i].CmpCode;
                            claimmandatedetails.MandateId = Convert.ToInt32(this.MandateId);
                        }
                        var oldDetails = claimmandatedetails.ShallowCopy();
                        claimmandatedetails.PreMandate = this.MandateDetails[i].PreMandate == null ? 0.00m : this.MandateDetails[i].PreMandate;
                        claimmandatedetails.PreMandateSP = this.MandateDetails[i].PreMandateSP == null ? 0.00m : this.MandateDetails[i].PreMandateSP;
                        claimmandatedetails.MovementMandate = this.MandateDetails[i].MovementMandate == null ? 0.00m : this.MandateDetails[i].MovementMandate;
                        claimmandatedetails.MovementMandateSP = this.MandateDetails[i].MovementMandateSP == null ? 0.00m : this.MandateDetails[i].MovementMandateSP;
                        claimmandatedetails.PreviousOffers = this.MandateDetails[i].PreviousOffers;
                        claimmandatedetails.TPCounterOffer = this.MandateDetails[i].TPCounterOffer;
                        claimmandatedetails.Modifieddate = DateTime.Now;
                        claimmandatedetails.Modifiedby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                        if (code == "Labl")
                        {
                            claimmandatedetails.CurrentMandate = this.MandateDetails[i].MovementMandate == null ? 0.00m : this.MandateDetails[i].MovementMandate;
                            claimmandatedetails.CurrentMandateSP = this.MandateDetails[i].MovementMandateSP == null ? 0.00m : this.MandateDetails[i].MovementMandateSP;
                        }
                        else
                        {
                            claimmandatedetails.CurrentMandate = this.MandateDetails[i].CurrentMandate == null ? 0.00m : this.MandateDetails[i].CurrentMandate;
                            claimmandatedetails.CurrentMandateSP = this.MandateDetails[i].CurrentMandateSP == null ? 0.00m : this.MandateDetails[i].CurrentMandateSP;
                        }


                        //obj.SaveChanges();
                        lstMandateDetails.Add(claimmandatedetails);

                        string tempXML = objCommonUtilities.GenerateXMLForChangedColumns(oldDetails, claimmandatedetails, checkListDtails);
                        if (!string.IsNullOrEmpty(tempXML))
                        {
                            XMLDetails += "<Row Id=\"" + claimmandatedetails.CmpCode.Trim() + "\">" + tempXML;
                            XMLDetails += "</Row>";
                        }
                    }
                }

                string[] ignoreListSummary = new string[] { "PaymentId" };
                tableMandateSummary = objCommonUtilities.CreateDataTable(claimmandate, ignoreListSummary); ;

                string[] ignoreListDetails = new string[] { "PaymentId" };
                tableMandateDetails = objCommonUtilities.CreateDataTable(lstMandateDetails, ignoreListDetails);

                XMLDetails = "<ChangeXml>" + XMLDetails + "</ChangeXml>";
                string XMLSummary = "<ChangeXml>" + objCommonUtilities.GenerateXMLForChangedColumns(objMandateSummary, claimmandate, null) + "</ChangeXml>";

                //Save/Update Mandate
                var parameterSummary = new SqlParameter("@MandateSummary", SqlDbType.Structured);
                parameterSummary.Value = tableMandateSummary;
                parameterSummary.TypeName = "dbo.TVP_MandateSummary";

                var parameterDetails = new SqlParameter("@MandateDetailsList", SqlDbType.Structured);
                parameterDetails.Value = tableMandateDetails;
                parameterDetails.TypeName = "dbo.TVP_MandateDetails";


                object ret;
                obj.AddParameter(parameterSummary);
                obj.AddParameter(parameterDetails);
                obj.AddParameter("@XMLSummary", XMLSummary);
                obj.AddParameter("@XMLDetails", XMLDetails);
                obj.AddParameter("@ActionSummary", ActionSummary);
                obj.AddParameter("@ActionDetails", ActionDetails);
                obj.ExecuteNonQuery("Proc_SaveMandate", CommandType.StoredProcedure, out ret);
                returnValue = Convert.ToInt32(ret);

                if (returnValue > 0)
                    this.MandateId = returnValue;
                else
                {
                    throw new Exception("Exception generated in procedure Prov_SaveMandate.");
                }

                //obj.InsertTransactionAuditLog("CLM_MandateSummary", "MandateId", this.CreatedBy, this.ClaimID, this.AccidentClaimId, ActionSummary, XMLSummary);
                //obj.InsertTransactionAuditLog("CLM_MandateDetails", "MandateDetailId", this.CreatedBy, this.ClaimID, this.AccidentClaimId, ActionDetails, XMLDetails);


                return this;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }

        }

        public ClaimMandateModel FetchMandate(string mandateId, ClaimMandateModel model)
        {
            MCASEntities db = new MCASEntities();
            model.ClaimantName = (from c in db.CLM_Claims where c.ClaimID == model.ClaimID select c.ClaimantName).FirstOrDefault();
            var TransactonHistoryList = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" select l.Description).ToList();
            if (model._mandateDetails.Count != 0)
            {
                model._mandateDetails.RemoveRange(0, model._mandateDetails.Count);
            }
            try
            {
                int mId = Convert.ToInt32(mandateId);
                var MandateSummary = (from l in db.CLM_MandateSummary where l.MandateId == mId select l).FirstOrDefault();
                if (MandateSummary != null)
                {
                    model.AssignedTo = MandateSummary.AssignedTo;
                    model.InvestigationResult = MandateSummary.InvestigationResult;
                    model.Scenario = MandateSummary.Scenario;
                    model.Evidence = MandateSummary.Evidence;
                    model.RelatedFacts = MandateSummary.RelatedFacts;
                    model.COAssessment = MandateSummary.COAssessment;
                    model.InformSafetytoreviewfindings = MandateSummary.InformSafetytoreviewfindings;
                    model.SupervisorAssignto = Convert.ToInt32(MandateSummary.SupervisorAssignto);
                    model.ApproveRecommedations = MandateSummary.ApproveRecommedations;
                    model.SupervisorRemarks = MandateSummary.SupervisorRemarks;
                    model.PreMandate = MandateSummary.PreMandate;
                    model.MovementMandate = MandateSummary.MovementMandate;
                    model.CurrentMandate = MandateSummary.CurrentMandate;
                    model.ReserveId = MandateSummary.ReserveId;
                    model.PreviousOffers = MandateSummary.PreviousOffers;
                    model.TPCounterOffer = MandateSummary.TPCounterOffer;
                    model.SPFAL = GetFALforSP(Convert.ToInt32(model.AssignedTo), Convert.ToInt32(model.ClaimType));
                    model.CreatedBy = MandateSummary.Createdby;
                    model.CreatedOn = Convert.ToDateTime(MandateSummary.Createddate);
                    model.DateofNoticetoSafety = MandateSummary.DateofNoticetoSafety;
                    model.InformedInsurer = MandateSummary.InformedInsurer;
                    model.EZLinkCardNo = MandateSummary.EZLinkCardNo;
                    model.ODStatus = MandateSummary.ODStatus;
                    model.RecoverableFromInsurerBI = MandateSummary.RecoverableFromInsurerBI;
                    if (MandateSummary.Modifiedby != null)
                    {
                        model.ModifiedOn = MandateSummary.Modifieddate;
                        model.ModifiedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    }
                }
                var MandateDetailList = (from details in db.CLM_MandateDetails join mn in db.MNT_Lookups on details.CmpCode equals mn.Lookupvalue where details.MandateId == mId && mn.Category == "TranComponent" orderby mn.DisplayOrder ascending select details);
                if (MandateDetailList.Any() && mId != 0)
                {
                    model.MandateDetails = (from detail in MandateDetailList
                                            select new ClaimMandateDetails()
                                            {
                                                MandateDetailId = detail.MandateDetailId,
                                                MandateId = detail.MandateId,
                                                AccidentClaimId = detail.AccidentClaimId,
                                                CmpCode = detail.CmpCode.Trim(),
                                                CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == detail.CmpCode select l.Description).FirstOrDefault(),
                                                PreMandate = detail.PreMandate == null ? 0.00m : detail.PreMandate,
                                                MovementMandate = detail.MovementMandate == null ? 0.00m : detail.MovementMandate,
                                                CurrentMandate = detail.CurrentMandate == null ? 0.00m : detail.CurrentMandate,
                                                PreMandateSP = detail.PreMandateSP == null ? 0.00m : detail.PreMandateSP,
                                                MovementMandateSP = detail.MovementMandateSP == null ? 0.00m : detail.MovementMandateSP,
                                                CurrentMandateSP = detail.CurrentMandateSP == null ? 0.00m : detail.CurrentMandateSP,
                                                PreviousOffers = detail.PreviousOffers == null ? 0.00m : detail.PreviousOffers,
                                                TPCounterOffer = detail.TPCounterOffer == null ? 0.00m : detail.TPCounterOffer,
                                                PaymentId = detail.PaymentId,
                                                ClaimID = detail.ClaimID
                                            }
                            ).ToList();


                    var remainlist = (from lk in db.MNT_Lookups where lk.IsActive == "Y" && lk.Category == "TranComponent" select lk).Where(p => !(from details in db.CLM_MandateDetails where details.MandateId == mId select details).Any(p2 => p2.CmpCode == p.Lookupvalue)).OrderBy(x => x.DisplayOrder).ToList();

                    if (remainlist.Any())
                    {
                        var list = model.MandateDetails;
                        foreach (var remain in remainlist)
                        {
                            var Position = (from lk in db.MNT_Lookups where lk.IsActive == "Y" && lk.Category == "TranComponent" orderby lk.DisplayOrder select lk).ToList().FindIndex(c => c.DisplayOrder == remain.DisplayOrder);
                            list.Insert(Convert.ToInt32(Position), new ClaimMandateDetails()
                            {
                                MandateDetailId = 0,
                                MandateId = model.MandateDetails.FirstOrDefault().MandateId,
                                AccidentClaimId = MandateDetails.FirstOrDefault().AccidentClaimId,
                                CmpCode = remain.Lookupvalue.Trim(),
                                CompDesc = remain.Description,
                                PreMandate = 0.00m,
                                MovementMandate = 0.00m,
                                CurrentMandate = 0.00m,
                                PreMandateSP = 0.00m,
                                MovementMandateSP = 0.00m,
                                CurrentMandateSP = 0.00m,
                                PreviousOffers = 0.00m,
                                TPCounterOffer = 0.00m,
                                PaymentId = model.MandateDetails.FirstOrDefault().PaymentId,
                                ClaimID = model.MandateDetails.FirstOrDefault().ClaimID
                            });
                        }
                        model._mandateDetails.Clear();
                        model.GetType().GetProperty("MandateDetails").SetValue(model, list, null);
                    }
                }
                else
                {
                    var CompCodeList = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" orderby l.DisplayOrder ascending select l.Lookupvalue).ToList();
                    var TranList = new List<ClaimMandateDetails>();
                    for (var i = 0; i < CompCodeList.Count; i++)
                    {
                        var desc = Convert.ToString(CompCodeList[i]);
                        TranList.Add(new ClaimMandateDetails()
                        {
                            MandateDetailId = 0,
                            MandateId = 0,
                            AccidentClaimId = model.AccidentClaimId,
                            CmpCode = CompCodeList[i].Trim(),
                            CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == desc select l.Description).FirstOrDefault(),
                            PreMandate = 0.00m,
                            MovementMandate = 0.00m,
                            CurrentMandate = 0.00m,
                            PreMandateSP = 0.00m,
                            MovementMandateSP = 0.00m,
                            CurrentMandateSP = 0.00m,
                            PreviousOffers = 0.00m,
                            TPCounterOffer = 0.00m,
                            PaymentId = 0,
                            ClaimID = model.ClaimID
                        });
                    }
                    model.MandateDetails = TranList.ToList();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
        }

        #endregion


        public decimal? GetFALforSP(int AssignedtoSP, int ClaimType)
        {
            MCASEntities obj = new MCASEntities();
            decimal? result = 0.00m;
            if (ClaimType == 1)
            {
                result = (from usr in obj.MNT_Users
                          join fal in obj.MNT_FAL on usr.FAL_OD equals fal.FALId
                          where usr.SNo == AssignedtoSP
                          select fal.Amount).FirstOrDefault() == null ? 0.00m : (from usr in obj.MNT_Users
                                                                                 join fal in obj.MNT_FAL on usr.FAL_OD equals fal.FALId
                                                                                 where usr.SNo == AssignedtoSP
                                                                                 select fal.Amount).FirstOrDefault();
            }
            else
            {
                result = (from fal in obj.MNT_FAL
                          join usr in obj.MNT_Users on fal.FALId equals usr.FAL_PDBI
                          where usr.SNo == AssignedtoSP
                          select fal.Amount).FirstOrDefault() == null ? 0.00m : (from fal in obj.MNT_FAL
                                                                                 join usr in obj.MNT_Users on fal.FALId equals usr.FAL_PDBI
                                                                                 where usr.SNo == AssignedtoSP
                                                                                 select fal.Amount).FirstOrDefault();
            }
            obj.Dispose();
            return result;
        }


        public static List<MandateListAll> MandateListAll(string MandateRecordNo, string AccidentClaimId, string ClaimID, string ClaimType, string MandateId)
        {
            MCASEntities _db = new MCASEntities();
            int acc = Convert.ToInt32(AccidentClaimId);
            int cid = Convert.ToInt32(ClaimID);
            int ctype = Convert.ToInt32(ClaimType);
            int mid = Convert.ToInt32(MandateId);
            int i = 0;
            var list = (from l in _db.CLM_MandateSummary where l.MandateRecordNo == MandateRecordNo && l.AccidentClaimId == acc && l.ClaimID == cid && l.ClaimType == ctype select l).ToList();
            var result = new List<MandateListAll>();
            foreach (var l in list)
            {

                if (((l.PreMandate != null && l.PreMandate != 0.00m) || (l.MovementMandate != null && l.MovementMandate != 0.00m) || (l.CurrentMandate != null && l.CurrentMandate != 0.00m)) && l.MovementType != "P")
                {
                    i++;
                    result.Add(new MandateListAll()
                    {
                        SerialNo = i,
                        TypeOfMovement = "I",
                        MovementMandate = l.MovementMandate == null ? 0.00m : l.MovementMandate,
                        CurrentMandate = l.CurrentMandate == null ? 0.00m : l.CurrentMandate,
                        CreatDate = l.Createddate,
                        Username = l.Createdby,
                        ClaimantName = (from c in _db.CLM_Claims where c.ClaimID == l.ClaimID select c.ClaimantName).FirstOrDefault(),
                        ClaimantrecordNumber = (from c in _db.CLM_Claims where c.ClaimID == l.ClaimID select c.ClaimRecordNo).FirstOrDefault(),
                        MandaterecorNumber = l.MandateRecordNo,
                        MandateId = l.MandateId,
                        ClaimTypeId = l.ClaimType
                    });
                }
                if ((l.PreMandateSP != null && l.PreMandateSP != 0.00m) || (l.MovementMandateSP != null && l.MovementMandateSP != 0.00m) || (l.CurrentMandateSP != null && l.CurrentMandateSP != 0.00m))
                {
                    i++;
                    var date = l.Modifieddate == null ? l.Createddate : l.Modifieddate;
                    var username = l.Modifiedby == null ? l.Createdby : l.Modifiedby;
                    var typeofmovemen = l.MovementType == "P" ? "P" : "M";
                    result.Add(new MandateListAll()
                    {
                        SerialNo = i,
                        TypeOfMovement = typeofmovemen,
                        MovementMandate = l.MovementMandateSP == null ? 0.00m : l.MovementMandateSP,
                        CurrentMandate = l.CurrentMandateSP == null ? 0.00m : l.CurrentMandateSP,
                        CreatDate = date,
                        Username = username,
                        ClaimantName = (from c in _db.CLM_Claims where c.ClaimID == l.ClaimID select c.ClaimantName).FirstOrDefault(),
                        ClaimantrecordNumber = (from c in _db.CLM_Claims where c.ClaimID == l.ClaimID select c.ClaimRecordNo).FirstOrDefault(),
                        MandaterecorNumber = l.MandateRecordNo,
                        MandateId = l.MandateId,
                        ClaimTypeId = l.ClaimType
                    });
                }

            }
            _db.Dispose();
            return result;
        }

        public static List<ClaimMandateDetails> Fetchdetailsfrommmandatedetails(string ClaimType, string MandateId, string Typeofmovement)
        {
            MCASEntities _db = new MCASEntities();
            string LoggedInUserId = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserId"]);
            int mid = Convert.ToInt32(MandateId);
            var userid = (from l in _db.CLM_MandateSummary where l.MandateId == mid select l.Createdby).FirstOrDefault();

            var Resid = Convert.ToInt32(MandateId);
            var result = (from l in _db.CLM_MandateDetails
                          where l.MandateId == Resid
                          select new ClaimMandateDetails()
                          {
                              CmpCode = l.CmpCode,
                              PreMandateSP = l.PreMandateSP,
                              MovementMandateSP = l.MovementMandateSP,
                              CurrentMandateSP = l.CurrentMandateSP,
                              PreMandate = l.PreMandate,
                              MovementMandate = l.MovementMandate,
                              CurrentMandate = l.CurrentMandate,
                              Createby1 = l.Createdby,
                              Createddate = l.Createddate,
                              MovementType = l.MovementType
                          }).ToList();

            var res = new List<ClaimMandateDetails>();
            foreach (var data in result)
            {
                if (Typeofmovement == "I")
                {
                    if (data.MovementMandate != 0.00m || data.CurrentMandate != 0.00m || data.PreMandate != 0.00m)
                    {
                        res.Add(new ClaimMandateDetails()
                        {
                            CompCode = (from l in _db.MNT_Lookups where l.Category == "Trancomponent" && l.Lookupvalue == data.CmpCode select l.Lookupdesc).FirstOrDefault(),
                            MovementMandate = data.MovementMandate,
                            CurrentMandate = data.CurrentMandate,
                            Createby1 = data.Createby1,
                            Crdate = data.Createddate.Value.ToString(),
                            MovementType = Typeofmovement
                        });
                    }
                }
                else
                {
                    if (data.MovementMandateSP != 0.00m || data.CurrentMandateSP != 0.00m || data.PreMandateSP != 0.00m)
                    {
                        res.Add(new ClaimMandateDetails()
                        {
                            CompCode = (from l in _db.MNT_Lookups where l.Category == "Trancomponent" && l.Lookupvalue == data.CmpCode select l.Lookupdesc).FirstOrDefault(),
                            MovementMandate = data.MovementMandateSP,
                            CurrentMandate = data.CurrentMandateSP,
                            Createby1 = data.Createby1,
                            Crdate = data.Createddate.Value.ToString(),
                            MovementType = Typeofmovement
                        });
                    }
                }

            }
            _db.Dispose();
            return res;
        }

        

        public ClaimMandateModel GetRecoverableFromInsurerBIEZLinkCardNolist(ClaimMandateModel model, string category)
        {
            MCASEntities db = new MCASEntities();
            int accid = Convert.ToInt32(model.AccidentClaimId);
            int cid = Convert.ToInt32(model.ClaimID);
            string Recoverableresult = string.Empty;
            string EZLinkresult = string.Empty;
            List<LookUpListItems> Recoverable = new List<LookUpListItems>();
            List<LookUpListItems> EZLink = new List<LookUpListItems>();
            try
            {
                Recoverableresult = (from l in db.ClaimAccidentDetails where l.AccidentClaimId == accid select l.IsRecoveryBI).FirstOrDefault() == null ? "" : (from l in db.ClaimAccidentDetails where l.AccidentClaimId == accid select l.IsRecoveryBI).FirstOrDefault();

                Recoverable = (from l in db.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.Lookupvalue == Recoverableresult orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
                if (Recoverable.Count == 0)
                {
                    Recoverable.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "NA" });
                }

                EZLinkresult = (from l in db.CLM_Claims where l.ClaimID == cid select l.EZLinkCardNo).FirstOrDefault() == null ? "" : (from l in db.CLM_Claims where l.ClaimID == cid select l.EZLinkCardNo).FirstOrDefault();

                EZLink = (from l in db.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.Lookupvalue == EZLinkresult orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();

                if (EZLink.Count == 0)
                {
                    EZLink.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "NA" });
                }

                model.RecoverableFromInsurerBIList = Recoverable;
                model.EZLinkCardNolist = EZLink;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return model;
        }

        //By Apurva and Ashish
        public List<LookUpListItems> ClaimTypeValue(string category, bool addAll = true, bool addNone = false)
        {
            return LookUpListItems.Fetch(category, addAll, addNone);
        }   
    }

    public class MandateListAll : BaseModel
    {
        #region Properties
        public int SerialNo { get; set; }
        public string TypeOfMovement { get; set; }
        public decimal? MovementMandate { get; set; }
        public decimal? CurrentMandate { get; set; }
        public DateTime? CreatDate { get; set; }
        public string Username { get; set; }
        public string ClaimantName { get; set; }
        public string ClaimantrecordNumber { get; set; }
        public string MandaterecorNumber { get; set; }
        public int MandateId { get; set; }
        public int ClaimTypeId { get; set; }
        #endregion
    }


    public class ClaimMandateDetails : BaseModel
    {
        #region Properties
        public int MandateDetailId { get; set; }
        public int MandateId { get; set; }
        public string CompCode { get; set; }
        public string CmpCode { get; set; }
        public string CompDesc { get; set; }
        public string Createby1 { get; set; }
        public decimal? PreMandate { get; set; }
        public decimal? MovementMandate { get; set; }
        public decimal? CurrentMandate { get; set; }
        public int? PaymentId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? ClaimID { get; set; }
        public decimal? PreMandateSP { get; set; }
        public decimal? MovementMandateSP { get; set; }
        public decimal? CurrentMandateSP { get; set; }
        public decimal? PreviousOffers { get; set; }
        public decimal? TPCounterOffer { get; set; }
        public DateTime? Createddate { get; set; }
        public string MovementType { get; set; }
        public string Crdate { get; set; }
        #endregion
    }
}

