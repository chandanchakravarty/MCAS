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
namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ThirdPartyInfoModel : BaseModel
    {
        #region Properties
        private DateTime? _dateAppointed = null;
        private DateTime? _paidDate = null;

        public int? TPartyId { get; set; }
        public int? ClaimId { get; set; }
        [Required(ErrorMessage = "Other Party Type is required.")]
        public string OtherPartyType { get; set; }
        [Required(ErrorMessage = "Claimant Name is required.")]
        public string CompanyName { get; set; }
        public string Reference { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateAppointed
        {
            get { return _dateAppointed; }
            set { _dateAppointed = value; }
        }
        public string TPVehicleNo { get; set; }
        public string TPSurname { get; set; }
        public string TPGivenName { get; set; }
        public string TPNRICNo { get; set; }
        public string TPAdd1 { get; set; }
        public string TPAdd2 { get; set; }
        public string TPCountry { get; set; }
        public decimal? TPPostalCode { get; set; }
        public decimal? TPOfficeNo { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter Contact No in proper format.")]
        public decimal? TPMobNo { get; set; }
        public decimal? TPFaxNo { get; set; }
       [RegularExpression(@"^([a-zA-Z0-9_\-\.']+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter Contact Email in proper format.")]
        public string TPEmailAdd { get; set; }
        public decimal? PaidThisYear { get; set; }
        public decimal? PaidToDate { get; set; }
        public decimal? RecovThisYear { get; set; }
        public decimal? RecovToDate { get; set; }
        public string IsActive { get; set; }

        public string VehicleRegnNo { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string LossDamageDesc { get; set; }
        public string TPAdjuster { get; set; }
        public string TPLawyer { get; set; }
        public string TPWorkShop { get; set; }
        public string Remarks { get; set; }
        public string AttachedFile { get; set; }
        public string ReserveCurr { get; set; }
        public decimal? ReserveAmount { get; set; }
        public decimal? ReserveExRate { get; set; }
        public decimal? ReserveAmt { get; set; }
        public string ExpensesCurr { get; set; }
        public decimal? ExpensesAmount { get; set; }
        public decimal? ExpensesExRate { get; set; }
        public decimal? ExpensesAmt { get; set; }
        public decimal? TotalReserve { get; set; }
        [Required(ErrorMessage = "Paid Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PaidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; }
        }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Balance LOG must be a Number.")]
        [Range(typeof(Decimal), "0", "9999", ErrorMessage = "Balance LOG must be a decimal/number.")]
        public decimal? BalanceLOG { get; set; }

        
        
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "LOG Amount must be a Number.")]
        [Range(typeof(Decimal), "0", "9999", ErrorMessage = "LOG Amount must be a decimal/number.")]
        public decimal? LOGAmount { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "LOU Rate must be a Number.")]
        [Range(typeof(Decimal), "0", "9999", ErrorMessage = "LOU Rate must be a decimal/number.")]
        public decimal? LOURate { get; set; }

        [RegularExpression(@"[0-9]*\?[0-9]+", ErrorMessage = "LOU Days must be a Number.")]
        public decimal? LOUDays { get; set; }
        public string ClaimAmtCurr { get; set; }
        public decimal? ClaimAmount { get; set; }
        public decimal? ClaimAmtExRate { get; set; }
        public decimal? ClaimAmt { get; set; }
        public int? PayableTo { get; set; }
        public string hiddenprop { get; set; }

        public virtual ICollection<ClaimEntryInfoModel> ClaimEntryInfo { get; set; }
        public List<LookUpListItems> ThirdPartyClmTypeList { get; set; }
        public List<ReserveCurr> reservelist { get; set; }
        public List<ExpensesCurr> expenseslist { get; set; }
        public List<ClaimAmtCurr> claimamtlist { get; set; }
        public List<LookUpListItems> payabletolist { get; set; }


        // added by sanjay
        private DateTime? _claimDate = null;
        private DateTime? _finalSettleDate = null;
        private DateTime? _timeBarDate = null;
        private DateTime? _reportSentInsurer = null;
        private DateTime? _referredInsurers = null;
        private DateTime? _informInsurer = null;
        private DateTime? _excessRecoveredDate = null;
        private DateTime? _writIssuedDate = null;
        private DateTime? _senstiveCase = null;
        private DateTime? _mPLetter = null;
        private DateTime? _reopened_date = null;
        private DateTime? _late_reopened_date = null;
        private DateTime? _record_deletion_date = null;

        public int? ClaimID { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }


        [Required(ErrorMessage = "Claim Type is required.")]
        public string ClaimType { get; set; }
        public List<LookUpListItems> clmtypelist { get; set; }
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

        public IEnumerable<SelectListItem> ClaimStatusRadioList { get; set; }
        public IEnumerable<SelectListItem> ClaimantGenderList { get; set; }
        public IEnumerable<SelectListItem> IsclaimantaninfantList { get; set; }

        public DateTime? TimeBarDate
        {
            get { return _timeBarDate; }
            set { _timeBarDate = value; }
        }

        [Required(ErrorMessage = "Case Category is required.")]
        public string CaseCategory { get; set; }
        [Required(ErrorMessage = "Case Status is required.")]
        public string CaseStatus { get; set; }
        public string WritNo { get; set; }
        [DisplayName("Driver's Liability")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public decimal? DriversLiability { get; set; }

        public string AccidentCause { get; set; }

        public int? AdjusterAppointed { get; set; }
        public int? LawyerAppointed { get; set; }
        public int? SurveyorAppointed { get; set; }
        public int? DepotWorkshop { get; set; }

        public List<AdjusterAppointed> adjusterAppointedlist { get; set; }
        public List<LawyerAppointed> lawyerAppointedlist { get; set; }
        public List<SurveyorAppointed> surveyorAppointedlist { get; set; }
        public List<DepotWorkshop> depotWorkshoplist { get; set; }
        public string CreatedDate { get; set; }
        public string RecordDeletionReason { get; set; }
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

        public DateTime? ReopenedDate
        {
            get { return _reopened_date; }
            set { _reopened_date = value; }
        }

        public DateTime? LateReopenedDate
        {
            get { return _late_reopened_date; }
            set { _late_reopened_date = value; }
        }

        public DateTime? RecordDeletionDate
        {
            get { return _record_deletion_date; }
            set { _record_deletion_date = value; }
        }

        public override string screenId
        {
            get
            {
                return "132";
            }

        }
       
        public override string listscreenId
        {

            get
            {
                return "132";
            }

        }

        #endregion

        #region "Public Shared Methods"
        public static List<ThirdPartyInfoModel> Fetch()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ThirdPartyInfoModel>();
            var userList = (from l in obj.CLM_ThirdParty select l);
            if (userList.Any())
            {
                foreach (var data in userList)
                {
                    item.Add(new ThirdPartyInfoModel() { OtherPartyType = data.OtherPartyType,CompanyName= data.CompanyName,DateAppointed= data.DateAppointed,TPVehicleNo= data.TPVehicleNo,TPSurname= data.TPSurname,TPGivenName = data.TPGivenName,TPNRICNo= data.TPNRICNo });
                }
            }
            return item;
        }

        public static List<ThirdPartyInfoModel> Fetchlist(int? ClaimId)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ThirdPartyInfoModel>();
            var userList = (from l in obj.CLM_ThirdParty where l.ClaimId == ClaimId select l);
            if (userList.Any())
            {
                foreach (var data in userList)
                {
                    item.Add(new ThirdPartyInfoModel() { TPartyId= data.TPartyId,ClaimId= data.ClaimId, OtherPartyType = data.OtherPartyType, CompanyName = data.CompanyName, DateAppointed = data.DateAppointed, TPVehicleNo = data.TPVehicleNo,TPLawyer= data.TPLawyer,TPAdjuster= data.TPAdjuster,TPAdd1 = data.TPAdd1,
                       VehicleRegnNo=data.VehicleRegnNo,VehicleMake=data.VehicleMake,VehicleModel=data.VehicleModel, TPSurname = data.TPSurname, TPGivenName = data.TPGivenName, TPNRICNo = data.TPNRICNo });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public ThirdPartyInfoModel Update()
        {
            MCASEntities obj = new MCASEntities();
            CLM_ThirdParty thirdpartyinfo;
            var model = new ThirdPartyInfoModel();
            string CreatedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
            if (TPartyId.HasValue)
            {
                thirdpartyinfo = obj.CLM_ThirdParty.Where(x => x.TPartyId == this.TPartyId.Value).FirstOrDefault();

                thirdpartyinfo.ClaimId = (int)this.ClaimId;
                thirdpartyinfo.OtherPartyType = this.OtherPartyType;
                thirdpartyinfo.CompanyName = this.CompanyName;
                thirdpartyinfo.Reference = this.Reference;
                thirdpartyinfo.DateAppointed = this.DateAppointed;
                thirdpartyinfo.TPVehicleNo = this.TPVehicleNo;
                thirdpartyinfo.TPSurname = this.TPSurname;
                thirdpartyinfo.TPGivenName = this.TPGivenName;
                thirdpartyinfo.TPNRICNo = this.TPNRICNo;
                thirdpartyinfo.TPAdd1 = this.TPAdd1;
                thirdpartyinfo.TPAdd2 = this.TPAdd2;
                thirdpartyinfo.TPCountry = this.TPCountry;
                thirdpartyinfo.TPPostalCode = this.TPPostalCode;
                thirdpartyinfo.TPOfficeNo = this.TPOfficeNo;
                thirdpartyinfo.TPMobNo = this.TPMobNo;
                thirdpartyinfo.TPFaxNo = this.TPFaxNo;
                thirdpartyinfo.TPEmailAdd = this.TPEmailAdd;
                thirdpartyinfo.PaidThisYear = this.PaidThisYear;
                thirdpartyinfo.PaidToDate = this.PaidToDate;
                thirdpartyinfo.RecovThisYear = this.RecovThisYear;
                thirdpartyinfo.RecovToDate = this.RecovToDate;
                thirdpartyinfo.IsActive = this.IsActive;
                thirdpartyinfo.CreatedBy = this.CreatedBy;
                thirdpartyinfo.CreatedDate = this.CreatedOn;
                thirdpartyinfo.ModifiedBy = this.ModifiedBy;
                thirdpartyinfo.ModifiedDate = this.ModifiedOn;
                thirdpartyinfo.VehicleRegnNo = this.VehicleRegnNo;
                thirdpartyinfo.VehicleMake = this.VehicleMake;
                thirdpartyinfo.VehicleModel = this.VehicleModel;
                thirdpartyinfo.LossDamageDesc = this.LossDamageDesc;
                thirdpartyinfo.TPAdjuster = this.TPAdjuster;
                thirdpartyinfo.TPLawyer = this.TPLawyer;
                thirdpartyinfo.TPWorkShop = this.TPWorkShop;
                thirdpartyinfo.Remarks = this.Remarks;
                thirdpartyinfo.AttachedFile = this.AttachedFile;
                thirdpartyinfo.ReserveCurr = this.ReserveCurr;
                thirdpartyinfo.ReserveExRate = this.ReserveExRate;
                thirdpartyinfo.ReserveAmt = this.ReserveAmt == null ? 0 : this.ReserveAmt;
                thirdpartyinfo.ExpensesCurr = this.ExpensesCurr;
                thirdpartyinfo.ExpensesExRate = this.ExpensesExRate;
                thirdpartyinfo.ExpensesAmt = this.ExpensesAmt == null ? 0 : this.ExpensesAmt;
                thirdpartyinfo.TotalReserve = this.TotalReserve == null ? 0 : this.TotalReserve;
                thirdpartyinfo.ClaimAmount = this.ClaimAmount == null ? 0 : this.ClaimAmount;
                thirdpartyinfo.PaidDate = this.PaidDate;
                thirdpartyinfo.BalanceLOG = this.BalanceLOG;
                thirdpartyinfo.LOGAmount = this.LOGAmount;
                thirdpartyinfo.LOURate = this.LOURate;
                thirdpartyinfo.LOUDays = this.LOUDays;
                thirdpartyinfo.ClaimAmtCurr = this.ClaimAmtCurr;
                thirdpartyinfo.ClaimAmtExRate = this.ClaimAmtExRate;
                thirdpartyinfo.ClaimAmt = this.ClaimAmt == null ? 0 : this.ClaimAmt;
                thirdpartyinfo.PayableTo = this.PayableTo;
                thirdpartyinfo.ExpensesAmount = this.ExpensesAmount == null ? 0 : this.ExpensesAmount;
                thirdpartyinfo.ReserveAmount = this.ReserveAmount == null ? 0 : this.ReserveAmount;

                //DataMapper.Map(this, thirdpartyinfo, true);
                obj.SaveChanges();
                this.ClaimId = thirdpartyinfo.ClaimId;
                this.ClaimId = thirdpartyinfo.ClaimId;
                this.TPartyId = thirdpartyinfo.TPartyId;
                this.ReserveAmt = thirdpartyinfo.ReserveAmt;
                this.ExpensesAmt = thirdpartyinfo.ExpensesAmt;
                this.TotalReserve = thirdpartyinfo.TotalReserve;
                this.ClaimAmount = thirdpartyinfo.ClaimAmount;
                this.ClaimAmt = thirdpartyinfo.ClaimAmt;
                this.ExpensesAmount = thirdpartyinfo.ExpensesAmount;
                this.ReserveAmount = thirdpartyinfo.ReserveAmount;

                    ReverseEditorModel clmreserve = new ReverseEditorModel();
                    clmreserve.ClaimID = this.ClaimId;
                    clmreserve.ClaimantID = this.TPartyId;
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

                    clmreserve.CreatedDate = DateTime.Now;
                    clmreserve.ApproveDate = DateTime.Now;
                    clmreserve.CreatedBy = CreatedBy1;
                    clmreserve.Update(clmreserve);

                    return this;
                
            }
            else
            {
                thirdpartyinfo = new CLM_ThirdParty();

                thirdpartyinfo.ClaimId = (int)this.ClaimId;
                thirdpartyinfo.OtherPartyType = this.OtherPartyType;
                thirdpartyinfo.CompanyName = this.CompanyName;
                thirdpartyinfo.Reference = this.Reference;
                thirdpartyinfo.DateAppointed = this.DateAppointed;
                thirdpartyinfo.TPVehicleNo = this.TPVehicleNo;
                thirdpartyinfo.TPSurname = this.TPSurname;
                thirdpartyinfo.TPGivenName = this.TPGivenName;
                thirdpartyinfo.TPNRICNo = this.TPNRICNo;
                thirdpartyinfo.TPAdd1 = this.TPAdd1;
                thirdpartyinfo.TPAdd2 = this.TPAdd2;
                thirdpartyinfo.TPCountry = this.TPCountry;
                thirdpartyinfo.TPPostalCode = this.TPPostalCode;
                thirdpartyinfo.TPOfficeNo = this.TPOfficeNo;
                thirdpartyinfo.TPMobNo = this.TPMobNo;
                thirdpartyinfo.TPFaxNo = this.TPFaxNo;
                thirdpartyinfo.TPEmailAdd = this.TPEmailAdd;
                thirdpartyinfo.PaidThisYear = this.PaidThisYear;
                thirdpartyinfo.PaidToDate = this.PaidToDate;
                thirdpartyinfo.RecovThisYear = this.RecovThisYear;
                thirdpartyinfo.RecovToDate = this.RecovToDate;
                thirdpartyinfo.IsActive = this.IsActive;
                thirdpartyinfo.CreatedBy = this.CreatedBy;
                thirdpartyinfo.CreatedDate = this.CreatedOn;
                thirdpartyinfo.ModifiedBy = this.ModifiedBy;
                thirdpartyinfo.ModifiedDate = this.ModifiedOn;
                thirdpartyinfo.VehicleRegnNo = this.VehicleRegnNo;
                thirdpartyinfo.VehicleMake = this.VehicleMake;
                thirdpartyinfo.VehicleModel = this.VehicleModel;
                thirdpartyinfo.LossDamageDesc = this.LossDamageDesc;
                thirdpartyinfo.TPAdjuster = this.TPAdjuster;
                thirdpartyinfo.TPLawyer = this.TPLawyer;
                thirdpartyinfo.TPWorkShop = this.TPWorkShop;
                thirdpartyinfo.Remarks = this.Remarks;
                thirdpartyinfo.AttachedFile = this.AttachedFile;
                thirdpartyinfo.ReserveCurr = this.ReserveCurr;
                thirdpartyinfo.ReserveExRate = this.ReserveExRate;
                thirdpartyinfo.ReserveAmt = this.ReserveAmt == null ? 0 : this.ReserveAmt;
                thirdpartyinfo.ExpensesCurr = this.ExpensesCurr;
                thirdpartyinfo.ExpensesExRate = this.ExpensesExRate;
                thirdpartyinfo.ExpensesAmt = this.ExpensesAmt == null ? 0 : this.ExpensesAmt;
                thirdpartyinfo.TotalReserve = this.TotalReserve == null ? 0 : this.TotalReserve;
                thirdpartyinfo.ClaimAmount = this.ClaimAmount == null ? 0 : this.ClaimAmount;
                thirdpartyinfo.PaidDate = this.PaidDate;
                thirdpartyinfo.BalanceLOG = this.BalanceLOG;
                thirdpartyinfo.LOGAmount = this.LOGAmount;
                thirdpartyinfo.LOURate = this.LOURate;
                thirdpartyinfo.LOUDays = this.LOUDays;
                thirdpartyinfo.ClaimAmtCurr = this.ClaimAmtCurr;
                thirdpartyinfo.ClaimAmtExRate = this.ClaimAmtExRate;
                thirdpartyinfo.ClaimAmt = this.ClaimAmt == null ? 0 : this.ClaimAmt;
                thirdpartyinfo.PayableTo = this.PayableTo;
                thirdpartyinfo.ExpensesAmount = this.ExpensesAmount == null ? 0 : this.ExpensesAmount;
                thirdpartyinfo.ReserveAmount = this.ReserveAmount == null ? 0 : this.ReserveAmount;


                //DataMapper.Map(this, thirdpartyinfo, true);
                obj.CLM_ThirdParty.AddObject(thirdpartyinfo);
                obj.SaveChanges();
                this.ClaimId = thirdpartyinfo.ClaimId;
                this.TPartyId = thirdpartyinfo.TPartyId;
                this.ReserveAmt = thirdpartyinfo.ReserveAmt;
                this.ExpensesAmt = thirdpartyinfo.ExpensesAmt;
                this.TotalReserve = thirdpartyinfo.TotalReserve;
                this.ClaimAmount = thirdpartyinfo.ClaimAmount;
                this.ClaimAmt = thirdpartyinfo.ClaimAmt;
                this.ExpensesAmount = thirdpartyinfo.ExpensesAmount;
                this.ReserveAmount = thirdpartyinfo.ReserveAmount;

                    ReverseEditorModel clmreserve = new ReverseEditorModel();
                    clmreserve.ClaimID = this.ClaimId;
                    clmreserve.ClaimantID = this.TPartyId;
                    clmreserve.ReserveType = 1;
                    clmreserve.MovementType = 0;

                    clmreserve.PreReserveLocalAmt = 0.00M;
                    clmreserve.PreResLocalCurrCode = "SGD";
                    clmreserve.PreExRateLocalCurr = 1.000000000M;
                    clmreserve.PreReserveOrgAmt = 0.00M;
                    clmreserve.PreResOrgCurrCode = this.ReserveCurr;
                    clmreserve.PreExRateOrgCurr = this.ReserveExRate;

                    clmreserve.MoveReserveLocalAmt = this.ReserveAmount;
                    clmreserve.MoveResLocalCurrCode = "SGD";
                    clmreserve.MoveExRateLocalCurr = 1.000000000M;
                    clmreserve.MoveReserveOrgAmt = (decimal)this.ReserveAmt;
                    clmreserve.MoveResOrgCurrCode = this.ReserveCurr;
                    clmreserve.MoveExRateOrgCurr = this.ReserveExRate;

                    clmreserve.FinalReserveLocalAmt = this.ReserveAmount;
                    clmreserve.FinalResLocalCurrCode = "SGD";
                    clmreserve.FinalExRateLocalCurr = 1.000000000M;
                    clmreserve.FinalReserveOrgAmt = (decimal)this.ReserveAmt;
                    clmreserve.FinalResOrgCurrCode = this.ReserveCurr;
                    clmreserve.FinalExRateOrgCurr = this.ReserveExRate;
                    clmreserve.CreatedBy = CreatedBy1;
                    clmreserve.CreatedDate = DateTime.Now;
                    clmreserve.ApproveDate = DateTime.Now;
                    clmreserve.Update(clmreserve);

                    return this;              
            }
        }
        #endregion
    }
}
