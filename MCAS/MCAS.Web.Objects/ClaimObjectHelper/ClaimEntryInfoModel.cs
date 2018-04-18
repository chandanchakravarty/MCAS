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

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimEntryInfoModel : BaseModel
    {
        #region Properties
        private DateTime? _claimDate = null;
        private DateTime? _finalSettleDate = null;
        private DateTime? _timeBarDate = null;
        private DateTime? _paidDate = null;
        private DateTime? _reportSentInsurer = null;
        private DateTime? _referredInsurers = null;
        private DateTime? _informInsurer = null;
        private DateTime? _excessRecoveredDate = null;
        private DateTime? _writIssuedDate = null;
        private DateTime? _senstiveCase = null;
        private DateTime? _mPLetter = null;
        private DateTime? _record_deletion_date = null;
        private DateTime? _late_reopened_date = null;
        private DateTime? _reopened_date = null;
        

        public int? ClaimID { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        [Required(ErrorMessage = "Claim Type is required.")]
        public string ClaimType { get; set; }
        //[Required(ErrorMessage = "Claim Date is required.")]
        //***************  
        public string ExpensesCurr { get; set; }
        public string ReserveCurr { get; set; }
        // added by sanjay
        public string RecordDeletionReason { get; set; }
        public string AlertMessage { get; set; }
        //public string _ExchangeRate { get; set; }
        public int? AdjusterAppointed { get; set; }
        public int? LawyerAppointed { get; set; }
        public int? SurveyorAppointed { get; set; }
        public int? DepotWorkshop { get; set; }
        [DisplayName("Total Reserve (Local Curr)")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public decimal? TotalReserve { get; set; }
        public decimal? ReserveExRate { get; set; }
        [DisplayName("Amount (Local Curr)")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public decimal? ReserveAmt { get; set; }
        public decimal? ExpensesExRate { get; set; }
        [DisplayName("Amount (Local Curr)")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public decimal? ExpensesAmt { get; set; }
        public int PayableTo { get; set; }
        public string ClaimAmountCurr { get; set; }
        public decimal? ClaimAmtPayoutExRate { get; set; }
        public decimal? ClaimAmtPayout { get; set; }
        [DisplayName("Reserve Amount")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public decimal? ReserveAmount { get; set; }
        [DisplayName("Expenses Amount")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public decimal? ExpensesAmount { get; set; }
        //***********
        public DateTime? RecordDeletionDate
        {
            get { return _record_deletion_date; }
            set { _record_deletion_date = value; }
        }
        public DateTime? LateReopenedDate
        {
            get { return _late_reopened_date; }
            set { _late_reopened_date = value; }
        }
        public DateTime? ReopenedDate
        {
            get { return _reopened_date; }
            set { _reopened_date = value; }
        }
        public DateTime? ClaimDate
        {
            get { return _claimDate; }
            set { _claimDate = value; }
        }
        [Required(ErrorMessage = "Claim Officer is required.")]
        public string ClaimOfficer { get; set; }
        public DateTime? FinalSettleDate
        {
            get { return _finalSettleDate; }
            set { _finalSettleDate = value; }
        }
        [Required(ErrorMessage = "Claim Status is required.")]
        public string ClaimStatus { get; set; }
        public DateTime? TimeBarDate
        {
            get { return _timeBarDate; }
            set { _timeBarDate = value; }
        }
        [Required(ErrorMessage = "Case Category is required.")]
        public string CaseCategory { get; set; }
        [Required(ErrorMessage = "Case Status is required.")]
        public string CaseStatus { get; set; }

        [DisplayName("Driver's Liability")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public decimal? DriversLiability { get; set; }

        public decimal? ClaimAmount { get; set; }
        public DateTime? PaidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; }
        }
        // sanjay
        public string CreatedDate { get; set; }
        public decimal? BalanceLOG { get; set; }
        public decimal? LOGAmount { get; set; }
        public decimal? LOURate { get; set; }
        public decimal? LOUDays { get; set; }
        public DateTime? ReportSentInsurer
        {
            get { return _reportSentInsurer; }
            set { _reportSentInsurer = value; }
        }
        public DateTime? ReferredInsurers
        {
            get { return _referredInsurers; }
            set { _referredInsurers = value; }
        }
        public DateTime? InformInsurer
        {
            get { return _informInsurer; }
            set { _informInsurer = value; }
        }
        public DateTime? ExcessRecoveredDate
        {
            get { return _excessRecoveredDate; }
            set { _excessRecoveredDate = value; }
        }
        public DateTime? WritIssuedDate
        {
            get { return _writIssuedDate; }
            set { _writIssuedDate = value; }
        }
        public string WritNo { get; set; }
        public DateTime? SenstiveCase
        {
            get { return _senstiveCase; }
            set { _senstiveCase = value; }
        }
        public DateTime? MPLetter
        {
            get { return _mPLetter; }
            set { _mPLetter = value; }
        }
        public string IsActive { get; set; }
        public List<LookUpListItems> clmtypelist { get; set; }
        public IEnumerable<SelectListItem> ClaimStatusRadioList { get; set; }
        public virtual ICollection<ThirdPartyInfoModel> ThirdPartyInfo { get; set; }
        private ClaimAccidentDetailsModel _accidentDetail = new ClaimAccidentDetailsModel();

        public List<ReserveCurr> reservelist { get; set; }
        public List<ExpensesCurr> expenseslist { get; set; }
        public List<AdjusterAppointed> adjusterAppointedlist { get; set; }
        public List<LawyerAppointed> lawyerAppointedlist { get; set; }
        public List<SurveyorAppointed> surveyorAppointedlist { get; set; }
        public List<DepotWorkshop> depotWorkshoplist { get; set; }

        public ClaimAccidentDetailsModel AccidentDetail
        {
            get { return _accidentDetail; }
            set { _accidentDetail = value; }
        }
        public string validMsg { get; set; }

        #endregion

        #region Methods
        #region "Public Shared Methods"
        public ClaimEntryInfoModel Fetch()
        {
            MCASEntities obj = new MCASEntities();
            var item = new ClaimEntryInfoModel() { ViewMode = this.ViewMode };
            if (AccidentClaimId.HasValue)// && ClaimID.HasValue)
            {
                var Claim = (from clm in obj.CLM_Claims where clm.AccidentClaimId == this.AccidentClaimId select clm).FirstOrDefault();
                if (Claim != null)
                {
                    var ClaimType1 = Claim.ClaimType ==1?"OD":Claim.ClaimType ==2?"PD":"BI";
                    item.AccidentClaimId = this.AccidentClaimId;
                    item.PolicyId = this.PolicyId;
                    item.ClaimID = Claim.ClaimID;
                    item.ClaimType = ClaimType1;
                    item.ClaimDate = Claim.ClaimDate;
                    //item.ClaimOfficer = Claim.ClaimOfficer;
                    item.FinalSettleDate = Claim.FinalSettleDate;
                    item.ClaimStatus = Claim.ClaimStatus;
                    item.TimeBarDate = Claim.TimeBarDate;
                    item.CaseCategory = Claim.CaseCategory;
                    item.CaseStatus = Claim.CaseStatus;
                    //item.DriversLiability = Claim.DriversLiability;
                    item.ClaimAmount = Claim.ClaimAmount;
                    //item.PaidDate = Claim.PaidDate;
                    //item.BalanceLOG = Claim.BalanceLOG;
                    //item.LOGAmount = Claim.LOGAmount;
                    //item.LOURate = Claim.LOURate;
                    //item.LOUDays = Claim.LOUDays;
                    //item.ReportSentInsurer = Claim.ReportSentInsurer;
                    //item.ReferredInsurers = Claim.ReferredInsurers;
                    //item.InformInsurer = Claim.InformInsurer;
                    item.ExcessRecoveredDate = Claim.ExcessRecoveredDate;
                    item.WritIssuedDate = Claim.WritIssuedDate;
                    item.WritNo = Claim.WritNo;
                    item.SenstiveCase = Claim.SenstiveCase;
                    item.MPLetter = Claim.MPLetter;
                    item.IsActive = Claim.IsActive;
                    item.CreatedBy = Claim.CreatedBy;
                    //if (Claim.CreatedDate != null)
                    //{
                    //    item.CreatedDate = (DateTime)Claim.CreatedDate;
                    //}

                    if (Claim.CreatedDate != null)
                    {
                        item.CreatedDate = Claim.CreatedDate.ToString();
                    }
                    item.ModifiedBy = Claim.ModifiedBy;
                    item.ModifiedOn = Claim.ModifiedDate;

                    //item.ReserveCurr = Claim.ReserveCurr;
                    //item.ReserveAmt = Claim.ReserveAmt;
                    //item.ReserveExRate = Claim.ReserveExRate;
                    //item.ReserveAmount = Claim.ReserveAmount;
                    //item.ExpensesCurr = Claim.ExpensesCurr;
                    //item.ExpensesAmt = Claim.ExpensesAmt;
                    //item.ExpensesExRate = Claim.ExpensesExRate;
                    //item.ExpensesAmount = Claim.ExpensesAmount;
                    //item.TotalReserve = Claim.TotalReserve;
                    item.AdjusterAppointed = Claim.AdjusterAppointed;
                    item.LawyerAppointed = Claim.LawyerAppointed;
                    item.SurveyorAppointed = Claim.SurveyorAppointed;
                    item.DepotWorkshop = Claim.DepotWorkshop;
                    //if (Claim.PayableTo != null)
                    //{
                    //    item.PayableTo = (int)Claim.PayableTo;
                    //}
                    //item.ClaimAmountCurr = Claim.ClaimAmountCurr;
                    //item.ClaimAmtPayout = Claim.ClaimAmtPayout;
                    //item.ClaimAmtPayoutExRate = Claim.ClaimAmtPayoutExRate;

                    //item.AdjusterAppointed = (int)Claim.AdjusterAppointed;
                    
                }
            }
            obj.Dispose();
            return item;
        }
        #endregion
        public ClaimEntryInfoModel Update()
        {
            MCASEntities obj = new MCASEntities();
            CLM_Claims claiminfo;
            var model = new ClaimEntryInfoModel();
            string CreatedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;

            if (ClaimID.HasValue && ClaimID != 0)
            {
                claiminfo = obj.CLM_Claims.Where(x => x.ClaimID == this.ClaimID.Value).FirstOrDefault();
                if (this.LawyerAppointed == 0)
                {
                    this.LawyerAppointed = null;
                } if (this.AdjusterAppointed == 0)
                {
                    this.AdjusterAppointed = null;
                } if (this.SurveyorAppointed == 0)
                {
                    this.SurveyorAppointed = null;
                } if (this.DepotWorkshop == 0)
                {
                    this.DepotWorkshop = null;
                }

                


                DataMapper.Map(this, claiminfo, true);
                obj.SaveChanges();

                ReverseEditorModel clmreserve = new ReverseEditorModel();
                clmreserve.ClaimID = this.ClaimID;
                clmreserve.ClaimantID = 0;
                clmreserve.ReserveType = 1;
                clmreserve.MovementType = 0;

                clmreserve.PreReserveLocalAmt = 0.00M;
                clmreserve.PreResLocalCurrCode = "SGD";
                clmreserve.PreExRateLocalCurr = 1.000000000M;
                clmreserve.PreReserveOrgAmt = 0.00M;
                clmreserve.PreResOrgCurrCode = this.ReserveCurr;
                clmreserve.PreExRateOrgCurr = this.ReserveExRate;

                if (this.ReserveAmount != null)
                {
                    clmreserve.MoveReserveLocalAmt = this.ReserveAmount;
                }
                clmreserve.MoveResLocalCurrCode = "SGD";
                clmreserve.MoveExRateLocalCurr = 1.000000000M;
                if (this.ReserveAmt != null)
                {
                    clmreserve.MoveReserveOrgAmt = (decimal)this.ReserveAmt;
                }
                clmreserve.MoveResOrgCurrCode = this.ReserveCurr;
                clmreserve.MoveExRateOrgCurr = this.ReserveExRate;

                clmreserve.FinalReserveLocalAmt = this.ReserveAmount;
                clmreserve.FinalResLocalCurrCode = "SGD";
                clmreserve.FinalExRateLocalCurr = 1.000000000M;
                if (this.ReserveAmt != null)
                {
                    clmreserve.FinalReserveOrgAmt = (decimal)this.ReserveAmt;
                }
                clmreserve.FinalResOrgCurrCode = this.ReserveCurr;
                clmreserve.FinalExRateOrgCurr = this.ReserveExRate;
                clmreserve.CreatedBy = CreatedBy1;
                clmreserve.CreatedDate = DateTime.Now;
                clmreserve.ApproveDate = DateTime.Now;
                clmreserve.Update(clmreserve);

                return this;
            }
            else
            {
                claiminfo = new CLM_Claims();

                if (this.LawyerAppointed == 0)
                {
                    this.LawyerAppointed = null;
                } if (this.AdjusterAppointed == 0)
                {
                    this.AdjusterAppointed = null;
                } if (this.SurveyorAppointed == 0)
                {
                    this.SurveyorAppointed = null;
                } if (this.DepotWorkshop == 0)
                {
                    this.DepotWorkshop = null;
                }
                
                DataMapper.Map(this, claiminfo, true);
                obj.CLM_Claims.AddObject(claiminfo);

                obj.SaveChanges();
                this.AccidentClaimId = claiminfo.AccidentClaimId;
                this.ClaimID = claiminfo.ClaimID;
                this.PolicyId = claiminfo.PolicyId;

                ReverseEditorModel clmreserve = new ReverseEditorModel();
                clmreserve.ClaimID = this.ClaimID;
                clmreserve.ClaimantID = 0;
                clmreserve.ReserveType = 1;
                clmreserve.MovementType = 0;
                clmreserve.CreatedBy = CreatedBy1;
                clmreserve.PreReserveLocalAmt = 0.00M;
                clmreserve.PreResLocalCurrCode = "SGD";
                clmreserve.PreExRateLocalCurr = 1.000000000M;
                clmreserve.PreReserveOrgAmt = 0.00M;
                clmreserve.PreResOrgCurrCode = this.ReserveCurr;
                clmreserve.PreExRateOrgCurr = this.ReserveExRate;

                if (this.ReserveAmount != null)
                {
                    clmreserve.MoveReserveLocalAmt = this.ReserveAmount;
                }
                clmreserve.MoveResLocalCurrCode = "SGD";
                clmreserve.MoveExRateLocalCurr = 1.000000000M;
                if (this.ReserveAmt != null)
                {
                    clmreserve.MoveReserveOrgAmt = (decimal)this.ReserveAmt;
                }
                clmreserve.MoveResOrgCurrCode = this.ReserveCurr;
                clmreserve.MoveExRateOrgCurr = this.ReserveExRate;

                if (this.ReserveAmount != null)
                {
                    clmreserve.FinalReserveLocalAmt = this.ReserveAmount;
                }
                clmreserve.FinalResLocalCurrCode = "SGD";
                clmreserve.FinalExRateLocalCurr = 1.000000000M;
                if (this.ReserveAmt != null)
                {
                    clmreserve.FinalReserveOrgAmt = (decimal)this.ReserveAmt;
                }
                clmreserve.FinalResOrgCurrCode = this.ReserveCurr;
                clmreserve.FinalExRateOrgCurr = this.ReserveExRate;
                clmreserve.CreatedBy = CreatedBy1;
                clmreserve.CreatedDate = DateTime.Now;
                clmreserve.ApproveDate = DateTime.Now;
                clmreserve.Update(clmreserve);

                return this;
            }

        }

        public static string GetPermanentClaimNo(int accidentClaimId, string OrganizationType, string ClaimPrefix, MCASEntities _db)
        {
            //MCASEntities _db = new MCASEntities();
            //DataSet dsTemp;
            //_db.AddParameter("@AccidentClaimId", accidentClaimId.ToString());
            //_db.AddParameter("@OrganizationType", OrganizationType.ToString());
            //_db.AddParameter("@ClaimPrefix", ClaimPrefix);
            //dsTemp = _db.ExecuteDataSet("Proc_SetClaimsNo", CommandType.StoredProcedure);
            var values = _db.Proc_SetClaimsNo(Convert.ToInt32(accidentClaimId), OrganizationType.ToString(), ClaimPrefix).ToList();
            //string claimNo = dsTemp.Tables[0].Rows[0]["ClaimNo"].ToString();
            string claimNo = values[0].ClaimNo.ToString();
            //_db.Dispose();
            //dsTemp.Dispose();
            return claimNo;
        }

        public static string GetClaimNo(int AccidentClaimId)
        {
            MCASEntities _db = new MCASEntities();
            var claimNo = _db.Proc_GetClaimNo(AccidentClaimId).FirstOrDefault();
            return claimNo;
        }

        public static string GetClaimsNo(int AccidentClaimId, string OrganizationType, string ClaimPrefix)
        {
            MCASEntities _db = new MCASEntities();
            var claimNo = _db.Proc_SetClaimsNo(AccidentClaimId, OrganizationType, ClaimPrefix).FirstOrDefault();
            return claimNo.ClaimNo;
        }

        public static string GetClaimsNoForCarAndTaxi(int AccidentClaimId, string OrganizationType, string ClaimPrefix, string InitailPrefix, MCASEntities _db)
        {
            //MCASEntities _db = new MCASEntities();
            var values = _db.Proc_SetClaimsNoForCarAndTaxi(AccidentClaimId, OrganizationType, ClaimPrefix, InitailPrefix).ToList();
            string claimNo = values[0].ClaimNo.ToString();
            return claimNo;
        }

        #endregion
    }

    public class ClaimStatus
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
    }
    public class ClaimantStatus
    {
        public Int32 Id { get; set; }
        public String Text { get; set; }
    }
    public class ReserveCurr
    {
        public String CurrencyCode { get; set; }
        public String Description { get; set; }
    }


    public class ExpensesCurr
    {
        public String CurrencyCode_Expenses { get; set; }
        public String Description_Expenses { get; set; }
    }

    public class ClaimAmtCurr
    {
        public String CurrencyCode_ClaimAmt { get; set; }
        public String Description_ClaimAmt { get; set; }
    }

    public class AdjusterAppointed
    {
        public int? AdjusterAppointed_Id { get; set; }
        public String Description_AdjusterAppointed { get; set; }
    }
    public class LawyerAppointed
    {
        public int? LawyerAppointed_Id { get; set; }
        public String Description_LawyerAppointed { get; set; }
    }
    public class SurveyorAppointed
    {
        public int? SurveyorAppointed_Id { get; set; }
        public String Description_SurveyorAppointed { get; set; }
    }
    public class DepotWorkshop
    {
        public int? DepotWorkshop_Id { get; set; }
        public String Description_DepotWorkshop { get; set; }
    }

    public class PaymentType
    {
        public String PaymentType_Id { get; set; }
        public String PaymentType_Name { get; set; }
    }

    public class PayeeType
    {
        public String PayeeType_Id { get; set; }
        public String PayeeType_Name { get; set; }
    }
    public class InterchangeM
    {
        public int Interchange_Id { get; set; }
        public String Interchange_Name { get; set; }
    }
}

