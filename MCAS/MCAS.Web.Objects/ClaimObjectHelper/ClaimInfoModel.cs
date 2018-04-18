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
using System.IO;
using System.Data.Objects;
using MCAS.Web.Objects.Resources.Common;
using System.Globalization;
namespace MCAS.Web.Objects.ClaimObjectHelper
{

    public class IsInformedInsurerAttribute : ValidationAttribute, IClientValidatable
    {
        public string OtherProperty { get; private set; }
        public IsInformedInsurerAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} is unknown property",
                        OtherProperty
                    )
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            if (!(Convert.ToString(otherValue) != "3" || ("" != Convert.ToString(value) && "DD/MM/YYYY" != Convert.ToString(value))))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage ?? ErrorMessageString,
                ValidationType = "isinformedinsurer",
            };
            rule.ValidationParameters["other"] = OtherProperty;
            yield return rule;
        }

    }

    public class IsCurrentDateAttribute : ValidationAttribute, IClientValidatable
    {
        public string OtherProperty { get; private set; }
        public IsCurrentDateAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} is unknown property",
                        OtherProperty
                    )
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);
            if (Convert.ToString(otherValue) != "1")
            {
                return null;
            }
            else if ((value == null || ((System.DateTime)(value)).Date.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy")))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
            //return value == DateTime.Now.ToString("dd/MM/yyyy");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage ?? ErrorMessageString,
                ValidationType = "iscurrentdate"
            };
            rule.ValidationParameters["other"] = OtherProperty;
            yield return rule;
        }
    }

    public class OnlycurrentdateAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            return value == null || ((System.DateTime)(value)).Date.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ? true : false;
            //return value == DateTime.Now.ToString("dd/MM/yyyy");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage ?? ErrorMessageString,
                ValidationType = "onlycurrentdate"
            };
        }
    }

    public class FutureDateAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else if ((DateTime)value > DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "futuredate"
            };
        }
    }

    public class NodNosAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty("WritNo");
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);
            if (!(!string.IsNullOrEmpty(Convert.ToString(otherValue)) && string.IsNullOrEmpty(Convert.ToString(value))))
            {
                return null;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage ?? ErrorMessageString,
                ValidationType = "nodnos",
            };
            yield return rule;
        }
    }

    public class ClaimInfoModel : BaseModel
    {
        #region Constructors
        public ClaimInfoModel()
        {
            this._claimsCollection = FetchClaimList(0);
        }
        public ClaimInfoModel(int AccidentClaimId)
        {
            this._claimsCollection = FetchClaimList(AccidentClaimId);
        }
        #endregion
        #region properties
        #region Collection Property
        private List<ClaimCollection> _claimsCollection;
        public List<ClaimCollection> ClaimsCollection
        {
            get { return _claimsCollection; }
        }
        #endregion
        #region 3rdParty
        private DateTime? _claimantDOB = null;
        private DateTime? _infantDOB = null;
        private DateTime? _dateOfMpLetter = null;
        private DateTime? _dateReferredToInsurersB = null;
        private DateTime? _reportSentToInsurer = null;
        private DateTime? _informedInsurerOfSettlement = null;
        public int _prop1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVClaimantName")]
        [DisplayName("Claimant Name")]
        //[RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid Claimant Name")]
        public string ClaimantName { get; set; }

        //[Required(ErrorMessage = "Claimant's NRIC/PP No is required.")]
        //[DisplayName("Claimant's NRIC/PP No")]
        public string ClaimantNRICPPNO { get; set; }
        // [Required(ErrorMessage = "Claimant's Gender is required.")]
        public string ClaimantGender { get; set; }

        [Display(Name = "Claimant's DOB")]
        [FutureDate(ErrorMessage = "Claimant's DOB should not be future date.")]
        public DateTime? ClaimantDOB
        {
            get { return _claimantDOB; }
            set { _claimantDOB = value; }
        }
        public int? ClaimID { get; set; }

        public int Prop1
        {
            get
            {
                return 0;
            }
            set
            {
                this._prop1 = 0;
            }
        }
        public string RequiredVal { get; set; }
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVClaimantType")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVClaimantType")]
        [DisplayName("Claimant Type")]
        public string ClaimantType { get; set; }



        //[Required(ErrorMessage = "Claimant Address1 is required.")]
        [DisplayName("Claimant Address1")]
        public string ClaimantAddress1 { get; set; }

        //[Required(ErrorMessage = "Claimant Address2 is required.")]
        [DisplayName("ClaimantAddress2")]
        public string ClaimantAddress2 { get; set; }

        [DisplayName("Claimant Address3")]
        public string ClaimantAddress3 { get; set; }

        //[Required(ErrorMessage = "Postal Code is required.")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }


        public string Country { get; set; }
        public List<UserCountryListItems> usercountrylist { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        //[Required(ErrorMessage = "Claimant Contact No is required.")]
        [DisplayName("Claimant Contact No")]
        public string ClaimantContactNo { get; set; }
        public string ClaimantEmail { get; set; }


        public string ResultMessage { get; set; }


        //[Required(ErrorMessage = "Vehicle Regn No is required.")]
        [DisplayName("Vehicle Regn No")]
        public string VehicleRegnNo { get; set; }

        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }

        public string Isclaimantaninfant { get; set; }
        public string InfantName { get; set; }
        public string InfantNRIC { get; set; }
        public string RecordNumber { get; set; }
        public Int64? OrgRecordNumber { get; set; }
        public IEnumerable<SelectListItem> ClaimantGenderList { get; set; }
        public IEnumerable<SelectListItem> IsclaimantaninfantList { get; set; }
        public List<ClaimantType> ClaimantTypeList { get; set; }
        public List<ClaimantType> MPList { get; set; }
        public List<ClaimantType> ConstituencyList { get; set; }
        public List<ClaimantType> CaseCategoryList { get; set; }
        public List<ClaimantType> CaseStatusList { get; set; }

        [FutureDate(ErrorMessage = "Infant's DOB should not be future date.")]
        public DateTime? InfantDOB
        {
            get { return _infantDOB; }
            set { _infantDOB = value; }
        }
        [Display(Name = "Date Of MP Letter ")]
        public DateTime? DateOfMpLetter
        {
            get { return _dateOfMpLetter; }
            set { _dateOfMpLetter = value; }
        }
        public string InfantGender { get; set; }
        #endregion
        #region NewEntry
        private DateTime? _timeBarDate = null;
        private DateTime? _claimDate = null;
        private DateTime? _finalSettleDate = null;
        private DateTime? _lappointedDate = null;
        private DateTime? _oWAppDate = null;
        private DateTime? _excessRecoveredDate = null;

        private DateTime? _lODSentdate = null;
        private DateTime? _informedInsurer = null;

        public int ClaimType { get; set; }
        public string Hmlegend { get; set; }
        public string Hdrop { get; set; }
        public string HChildGrid { get; set; }
        public string HOpenText { get; set; }
        public string Hshowgrid { get; set; }
        public string HGridImageSign { get; set; }
        public string Hheader { get; set; }
        public string Hdivdis { get; set; }
        public string Hdivfieldset { get; set; }
        public string Himgsrc { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVEZLinkCardNo")]
        public string EZLinkCardNo { get; set; }

        [NodNos(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "NODNOSValidationMessage")]
        public string NODNOS { get; set; }

        public DateTime Hdate { get; set; }
        public DateTime Htbdate { get; set; }
        public string ClaimType1 { get; set; }


        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVClaimRecordNo")]
        [DisplayName("Claim Record No")]
        public string ClaimRecordNo { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVTimebarDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Timebar Date")]
        public DateTime? TimeBarDate
        {
            get { return _timeBarDate; }
            set { _timeBarDate = value; }
        }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVClaimDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Claim Date")]
        public DateTime? ClaimDate
        {
            get { return _claimDate; }
            set { _claimDate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LOD Sent Date")]
        public DateTime? LODSentdate
        {
            get { return _lODSentdate; }
            set { _lODSentdate = value; }
        }


        [IsInformedInsurer("ClaimType", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVInformedInsurer")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Informed Insurer")]
        public DateTime? InformedInsurer
        {
            get { return _informedInsurer; }
            set { _informedInsurer = value; }
        }


        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVClaimsOfficer")]
        [DisplayName("Claims Officer")]
        public int ClaimsOfficer { get; set; }

        public string ClaimOfficer { get; set; }

        public string DriverLiablity { get; set; }

        //[Required(ErrorMessage = "Accident Cause is required.")]
        //[DisplayName("Accident Cause")]
        //public string AccidentCause { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVCaseCategory")]
        [DisplayName("Case Category")]
        public string CaseCategory { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVCaseStatus")]
        [DisplayName("Case Status")]
        public string CaseStatus { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVClaimantStatus")]
        [DisplayName("Claimant Status")]
        public string ClaimantStatus { get; set; }

        public string SeverityReferenceNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Final Settle Date")]
        [IsCurrentDate("RequiredVal", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor), ErrorMessageResourceName = "RFVFinalSettlementDate")]
        public DateTime? FinalSettleDate
        {
            get { return _finalSettleDate; }
            set { _finalSettleDate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Report Sent To Insurer")]
        public DateTime? ReportSentToInsurer
        {
            get { return _reportSentToInsurer; }
            set { _reportSentToInsurer = value; }
        }




        public string ReferredToInsurers { get; set; }
        public Boolean ReferredToInsurersB { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Informed Insurer Of Settlement")]
        public DateTime? InformedInsurerOfSettlement
        {
            get { return _informedInsurerOfSettlement; }
            set { _informedInsurerOfSettlement = value; }
        }

        public string RecoverableOD { get; set; }
        public string OurSurveyorApp { get; set; }
        public string SappointedDate { get; set; }
        public string SrefNo { get; set; }
        public string OurLawyerApp { get; set; }
        public DateTime? LappointedDate
        {
            get { return _lappointedDate; }
            set { _lappointedDate = value; }
        }
        public string Lrefno { get; set; }
        public string OurAdjustorApp { get; set; }
        public string AappDate { get; set; }
        public string Arefno { get; set; }
        public string OurWorkShop { get; set; }
        public string MP { get; set; }
        public string Constituency { get; set; }
        public int? AccidentId { get; set; }
        public int AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public decimal? ConfirmedAmount { get; set; }
        public decimal? RecoveredtoDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? OWAppDate
        {
            get { return _oWAppDate; }
            set { _oWAppDate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Referred To Insurers")]
        public DateTime? DateReferredToInsurersB
        {
            get { return _dateReferredToInsurersB; }
            set { _dateReferredToInsurersB = value; }
        }

        public int? HChkApprovedPayment { get; set; }
        public int? HChkHasClaim { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Excess Recovered Date")]
        public DateTime? ExcessRecoveredDate
        {
            get { return _excessRecoveredDate; }
            set { _excessRecoveredDate = value; }
        }
        public string Owrefno { get; set; }
        public int? HClaimentStatus { get; set; }
        public List<ClaimantType> ClaimTypeList { get; set; }
        public List<ClaimantType> ClaimantStatusList { get; set; }
        //public List<ClaimantType> AccidentCauseList { get; set; }
        public List<AdjusterModel> list { get; set; }
        public List<AdjusterModel> Sollist { get; set; }
        public List<AdjusterModel> Adjlist { get; set; }
        public List<AdjusterModel> DOPlist { get; set; }
        public List<ClaimOfficerModel> ClaimOfficerList { get; set; }


        public List<LookUpListItems> EZLinkCardNoList { get; set; }

        #endregion
        #region cancellation
        private DateTime? _writIssuedDate = null;
        private DateTime? _createddate = null;
        private DateTime? _reopenedDate = null;
        private DateTime? _lateReopened = null;
        private DateTime? _recordCancellationDate = null;
        public DateTime? _sensitiveCase = null;
        [Display(Name = "Writ Issued")]
        public DateTime? WritIssuedDate
        {
            get { return _writIssuedDate; }
            set { _writIssuedDate = value; }
        }
        public string WritNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sensitive Case")]
        public DateTime? SensitiveCase
        {
            get { return _sensitiveCase; }
            set { _sensitiveCase = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime? Createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Reopened Date")]
        [Onlycurrentdate(ErrorMessage = "Reopened Date must be current date.")]
        public DateTime? ReopenedDate
        {
            get { return _reopenedDate; }
            set { _reopenedDate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Late Reopened")]
        public DateTime? LateReopened
        {
            get { return _lateReopened; }
            set { _lateReopened = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Record Cancellation Date")]
        [Onlycurrentdate(ErrorMessage = "Record Cancellation Date must be current date.")]
        public DateTime? RecordCancellationDate
        {
            get { return _recordCancellationDate; }
            set { _recordCancellationDate = value; }
        }
        public string RecordCancellationReason { get; set; }
        public string RecordReopenedReason { get; set; }
        #endregion
        #region OverrideProp
        public override string screenId
        {
            get
            {
                return "131";
            }

        }
        public override string listscreenId
        {
            get
            {
                return "131";
            }

        }
        #endregion
        #endregion
        #region Methods
        #region static Methods

        public static List<ClaimOfficerModel> FetchClaimOfficer(string roleCode)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimOfficerModel>();
            var ClaimOfficerDetails = _db.Proc_GetClaimOfficerByRole(roleCode).ToList();
            if (ClaimOfficerDetails.Any())
            {
                item = (from n in ClaimOfficerDetails
                        select new ClaimOfficerModel()
                        {
                            TranId = n.SNo,
                            ClaimOfficerName = n.UserDispName
                        }
                        ).ToList();
            }
            item.Insert(0, new ClaimOfficerModel() { TranId = 0, ClaimOfficerName = "[Select...]" });
            _db.Dispose();
            return item;
        }
        public static List<AdjusterModel> DeportFetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<AdjusterModel>();
            var Adjusterlist = (from x in _db.MNT_DepotMaster select x).ToList();
            if (Adjusterlist.Any())
            {
                item = (from n in Adjusterlist
                        select new AdjusterModel()
                        {
                            AdjusterCode = n.DepotCode,
                            AdjusterName = n.DepotReference
                        }
                        ).ToList();
            }
            item.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
            _db.Dispose();
            return item;
        }

        public static List<ClaimantType> FetchSelectedClaimType(string Category, string type)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> list = new List<ClaimantType>();
            list = (from n in obj.MNT_Lookups
                    where n.Category == Category && n.IsActive == "Y" && n.Lookupvalue == type
                    select new ClaimantType()
                   {
                       Id = n.Lookupvalue,
                       Text = n.Description
                   }
                    ).ToList();
            return list;
        }

       

        public static List<ClaimantType> FetchLookUpListForClaimType(int AccidentClaimId)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> list = new List<ClaimantType>();
            list = (from l in obj.Proc_GetClaimType(AccidentClaimId)
                    select new ClaimantType()
                    {
                        Id = l.Lookupvalue.Trim(),
                        Text = l.Description.Trim()
                    }
                        ).ToList();
            list.Insert(0, new ClaimantType() { Id = "0", Text = "[Select...]" });
            return list;
        }
        public static List<ClaimantType> SelectOnlyList()
        {
            var item = new List<ClaimantType>();
            item.Add(new ClaimantType() { Id = "Select", Text = "[Select...]" });
            return item;
        }
        private List<ClaimCollection> FetchClaimList(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                var items = new List<ClaimCollection>();
                if (AccidentClaimId != 0)
                {
                    var Ownlist = db.Proc_GetCLM_ClaimList(Convert.ToString(AccidentClaimId)).ToList();
                    items = (from n in Ownlist
                             select new ClaimCollection()
                             {
                                 OrgRecordNumber = n.RecordNumber,
                                 RecordNumber = n.ClaimRecordNo,
                                 ClaimID = n.ClaimID,
                                 ClaimantName = n.ClaimantName,
                                 ClaimType = n.ClaimType,
                                 AccidentClaimId = n.AccidentClaimId,
                                 PolicyId = n.PolicyId,
                                 ClaimTypeDesc = n.ClaimType.ToString() == "1" ? Common._1 : n.ClaimType.ToString() == "2" ? Common._2 : n.ClaimType.ToString() == "3" ? Common._3 : "",
                                 ClaimTypeCode = n.ClaimTypeCode
                             }
                            ).ToList();
                }

                return items;
            }
            catch (Exception ex)
            {
                throw (new Exception("Claim List Could not be loaded. " + ex));
            }
            finally
            {
                db.Dispose();
            }
        }
        public static IEnumerable<SelectListItem> FetchClaimantGenderList()
        {
            List<ClaimStatus> list = new List<ClaimStatus>();
            list.Add(new ClaimStatus() { ID = 1, Name = Common.Male });
            list.Add(new ClaimStatus() { ID = 2, Name = Common.Female });
            SelectList sl = new SelectList(list, "ID", "Name");
            return sl;
        }
        public static IEnumerable<SelectListItem> FetchIsClaimantanInfantList()
        {
            List<ClaimStatus> list = new List<ClaimStatus>();
            list.Add(new ClaimStatus() { ID = 1, Name = Common.Yes });
            list.Add(new ClaimStatus() { ID = 2, Name = Common.No });
            SelectList sl = new SelectList(list, "ID", "Name");
            return sl;
        }
        #endregion

        #region Public Methods
        public ClaimInfoModel Update()
        {
            MCASEntities objEntity = new MCASEntities();
            CLM_Claims claimdetail;
            if (objEntity.Connection.State == System.Data.ConnectionState.Closed)
                objEntity.Connection.Open();
            using (System.Data.Common.DbTransaction transaction = objEntity.Connection.BeginTransaction())
            {
                try
                {
                    var claimantStatus = objEntity.CLM_Claims.Where(x => x.ClaimID == this.ClaimID).Select(x => x.ClaimantStatus).FirstOrDefault();
                    var val = objEntity.CLM_Claims.Where(x => x.ClaimID == this.ClaimID).Select(x => x.ClaimantStatus).ToList();
                    if (val.Any())
                    {
                        claimdetail = objEntity.CLM_Claims.Where(x => x.ClaimID == this.ClaimID.Value).FirstOrDefault();
                        var InformedInsurerOfSettlement = this.InformedInsurerOfSettlement;
                        this.CreatedBy = claimdetail.CreatedBy ?? Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        DataMapper.Map(this, claimdetail, true);
                        claimdetail.InformedInsurerOfSettlement = InformedInsurerOfSettlement.ToString();
                        claimdetail.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        claimdetail.ModifiedDate = DateTime.Now;
                        claimdetail.ClaimantStatus = this.ClaimantStatus == "4" ? "1" : this.ClaimantStatus;
                        objEntity.SaveChanges();
                        this.CreatedBy = claimdetail.CreatedBy;
                        this.CreatedOn = Convert.ToDateTime(claimdetail.CreatedDate);
                        this.ModifiedOn = DateTime.Now;
                        this.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.ResultMessage = "Record updated successfully.";

                    }
                    else
                    {
                        claimdetail = new CLM_Claims();
                        var InformedInsurerOfSettlement = this.InformedInsurerOfSettlement;
                        DataMapper.Map(this, claimdetail, true);
                        claimdetail.InformedInsurerOfSettlement = InformedInsurerOfSettlement.ToString();
                        claimdetail.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        claimdetail.CreatedDate = DateTime.Now;
                        claimdetail.ClaimStatus = "1";
                        claimdetail.IsActive = "Y";
                        claimdetail.ClaimantStatus = this.ClaimantStatus == "4" ? "1" : this.ClaimantStatus;
                        objEntity.CLM_Claims.AddObject(claimdetail);
                        objEntity.SaveChanges();
                        this.ClaimID = claimdetail.ClaimID;
                        this.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.CreatedOn = DateTime.Now;
                        this.ResultMessage = "Record saved successfully.";
                    }
                    #region "Insert Into AccidentHistoryDeials"
                    if (this.ClaimantStatus != claimantStatus)
                    {
                        InsertAccidentHistoryDeials(this.AccidentClaimId, this.ClaimID, this.ClaimantStatus, Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]), this.ClaimantStatus == "4" ? this.RecordReopenedReason : this.ClaimantStatus == "3" ? this.RecordCancellationReason : null);
                        //For Insert Transaction History for Claimant Status "Reopened".
                        if (this.ClaimantStatus == "4")
                        {
                            InsertAccidentHistoryDeials(this.AccidentClaimId, this.ClaimID, "1", "System", this.ClaimantStatus == "4" ? this.RecordReopenedReason : this.ClaimantStatus == "3" ? this.RecordCancellationReason : null);
                        }
                    }
                    #endregion
                    objEntity.SaveChanges();
                    transaction.Commit();
                    this.Hdate = (from l in objEntity.ClaimAccidentDetails where l.AccidentClaimId == this.AccidentClaimId select l.AccidentDate).FirstOrDefault();
                    objEntity.Dispose();
                    this._claimsCollection = this.FetchClaimList(this.AccidentClaimId);
                    return this;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    objEntity.Dispose();
                    throw (ex);
                }
            }
        }

        public void InsertAccidentHistoryDeials(int AccidentClaimId, int? ClaimID, string ClaimantStatus, string CreatedBy, string Remarks)
        {

            MCASEntities obj = new MCASEntities();
            obj.Proc_InsertAccidentHistoryDetails(AccidentClaimId, ClaimID, ClaimantStatus, CreatedBy, Remarks);
            obj.Dispose();

        }
        public ClaimInfoModel FetchAllLists(ClaimInfoModel model)
        {
            try
            {
                model.usercountrylist = UserCountryListItems.Fetch(true);
                //model.ClaimantTypeList = FetchLookUpList("ClaimantType");
                model.ClaimantTypeList = FetchCommonMasterData("ClaimantType", AccidentClaimId);
                model.ClaimTypeList = FetchLookUpListForClaimType(model.AccidentClaimId);
                model.ClaimantStatusList = FetchLookUpList("ClaimantStatus");
                //model.MPList = FetchLookUpList("MP");
                model.MPList = FetchCommonMasterData("MP", AccidentClaimId);
                //model.ConstituencyList = FetchLookUpList("Constituency");
                model.ConstituencyList = FetchCommonMasterData("Constituency", AccidentClaimId);
                model.list = AdjusterModel.FetchSurveyor();
                model.list.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
                model.Sollist = AdjusterModel.FetchSolicitor();
                model.Sollist.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
                model.Adjlist = AdjusterModel.FetchAdjuster();
                model.Adjlist.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
                model.DOPlist = ClaimInfoModel.DeportFetch();
                model.ClaimOfficerList = FetchClaimOfficer("CO");
                //model.AccidentCauseList = FetchLookUpList("AccidentCause");
                //model.CaseCategoryList = FetchLookUpList("CaseCategory");
                //model.CaseStatusList = FetchLookUpList("CaseStatus");
                model.CaseCategoryList = FetchCommonMasterData("CaseCategory", AccidentClaimId);
                model.CaseStatusList = FetchCommonMasterData("CaseStatus", AccidentClaimId);
                model.ClaimantGenderList = FetchClaimantGenderList();
                model.IsclaimantaninfantList = FetchIsClaimantanInfantList();
                model.CreatedBy = Convert.ToString(model.CreatedBy ?? System.Web.HttpContext.Current.Session["LoggedInUserId"]);
                return model;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public ClaimInfoModel FetchClaim(string ClaimID, int AccidentClaimId, ClaimInfoModel model)
        {
            MCASEntities db = new MCASEntities();
            int Cid = Convert.ToInt32(ClaimID);
            var createdBy = System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString();
            try
            {
                if (ClaimID != null)
                {
                    var list = (from l in db.CLM_Claims where l.ClaimID == Cid select l).FirstOrDefault(); 
                    if (list != null)
                    {
                        model = new ClaimInfoModel(list.AccidentClaimId);
                        DataMapper.Map(list, model, true);
                    }
                }
                model.Hdate = (from l in db.ClaimAccidentDetails where l.AccidentClaimId == AccidentClaimId select l.AccidentDate).FirstOrDefault();
                var clmID = 0;
                if (ClaimID != null)
                {
                    clmID = Int32.Parse(ClaimID);
                    var claimDtls = (from l in db.CLM_Claims where l.ClaimID == clmID select l).FirstOrDefault();
                    if (claimDtls != null)
                    {
                        model.CreatedBy = claimDtls.CreatedBy;
                        model.CreatedOn = Convert.ToDateTime(claimDtls.CreatedDate);
                        model.Createddate = Convert.ToDateTime(claimDtls.CreatedDate);
                        if (claimDtls.ModifiedDate != null || claimDtls.ModifiedDate.ToString() != "")
                        {
                            model.ModifiedOn = Convert.ToDateTime(claimDtls.ModifiedDate);
                            model.ModifiedBy = claimDtls.ModifiedBy;
                        }
                    }

                }
                model.usercountrylist = UserCountryListItems.Fetch(true);
                //Impacted due to tfs # 21522
                //model.ClaimantTypeList = FetchLookUpList("ClaimantType");
                model.ClaimantTypeList = FetchCommonMasterData("ClaimantType", AccidentClaimId);
                model.ClaimTypeList = FetchLookUpListForClaimType(AccidentClaimId);
                //model.MPList = FetchLookUpList("MP");
                model.MPList = FetchCommonMasterData("MP", AccidentClaimId);
                //model.ConstituencyList = FetchLookUpList("Constituency");
                model.ConstituencyList = FetchCommonMasterData("Constituency", AccidentClaimId);
                model.ClaimantStatusList = FetchLookUpList("ClaimantStatus");
                model.list = AdjusterModel.FetchSurveyor();
                model.list.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
                model.Sollist = AdjusterModel.FetchSolicitor();
                model.Sollist.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
                model.Adjlist = AdjusterModel.FetchAdjuster();
                model.Adjlist.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
                model.DOPlist = DeportFetch();
                model.ClaimOfficerList = FetchClaimOfficer("CO");
                //model.AccidentCauseList = FetchLookUpList("AccidentCause");

                //model.CaseCategoryList = FetchLookUpList("CaseCategory");
                //model.CaseStatusList = FetchLookUpList("CaseStatus");
                model.CaseCategoryList = FetchCommonMasterData("CaseCategory", AccidentClaimId);
                model.CaseStatusList = FetchCommonMasterData("CaseStatus", AccidentClaimId);
                model.ClaimantGenderList = FetchClaimantGenderList();
                model.IsclaimantaninfantList = FetchIsClaimantanInfantList();
                model.ClaimType1 = model.ClaimType == 1 ? "Own Damage (OD)" : model.ClaimType == 2 ? "Property Damage (TPPD)" : model.ClaimType == 3 ? "Body Injury (TPBI)" : "[Select...]";
                var appPayment = (from m in db.CLM_PaymentSummary where m.AccidentClaimId == AccidentClaimId && m.ClaimID == clmID && m.ApprovePayment == "Y" select m).FirstOrDefault();
                if (appPayment != null)
                {
                    model.HChkApprovedPayment = 1;
                }
                else
                {
                    model.HChkApprovedPayment = 0;
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

        public static List<ClaimantType> FetchLookUpList(string Category)
        {
            List<ClaimantType> list = new List<ClaimantType>();
            var lookup = LookUpListItems.Fetch(Category, false);
            lookup.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            list = (from n in lookup
                    where n.Lookup_value != null
                    select new ClaimantType()
                    {
                        Id = n.Lookup_value.Trim(),
                        Text = n.Lookup_desc.Trim()
                    }
                        ).ToList();
            return list;
        }

        //Add New Method for TFS 21522
        public static List<ClaimantType> FetchCommonMasterData(string category, int accclmid, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> list = new List<ClaimantType>();
            var clmorgId = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == accclmid select l.Organization).FirstOrDefault();
            var lookupcodelist = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" orderby l.Lookupdesc select l.LookupID).ToList();
            foreach (decimal ids in lookupcodelist)
            {
                string org = (from l in obj.MNT_Lookups where l.LookupID == ids select l.lookupCode).FirstOrDefault();
                if (org != null)
                {
                    List<string> orglist = org.Split(',').ToList();
                    foreach (string s in orglist)
                    {
                        if (s == Convert.ToString(clmorgId))
                        {
                            var lookupinfo = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.LookupID == ids orderby l.Lookupdesc select new ClaimantType { Id = l.Lookupvalue, Text = l.Lookupdesc }).FirstOrDefault();
                            list.Add(lookupinfo);
                        }
                    }
                }
            }
            list.Insert(0, new ClaimantType() { Id = "", Text = "[Select...]" });
            obj.Dispose();
            return list;
        }
        
        #endregion
        #endregion


        public int GetApprovedPaymentonClaimant(int AccidentClaimId, int ClaimId)
        {
            MCASEntities db = new MCASEntities();
            var result = 0;
            try
            {
                result = (from m in db.CLM_PaymentSummary where m.AccidentClaimId == AccidentClaimId && m.ClaimID == ClaimId && m.ApprovePayment == "Y" select m).FirstOrDefault() != null ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public string GetClaimType(int p)
        {
            string ClaimTypeNo = Convert.ToString(p);
            string result = string.Empty;
            MCASEntities obj = new MCASEntities();
            try
            {
                result = (from l in obj.MNT_Lookups where l.Category == "ClaimType" && l.Lookupvalue == ClaimTypeNo select l.Description).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }
            return result;
        }

        public string GetClaimNo(int CType, string AccidentId)
        {
            string result = string.Empty;
            MCASEntities db = new MCASEntities();
            try
            {
                var correctvech = db.Proc_GetCLM_ClaimNo(Convert.ToString(CType), AccidentId).FirstOrDefault() == null ? 0 : Convert.ToInt32(db.Proc_GetCLM_ClaimNo(Convert.ToString(CType), AccidentId).ToList()[0].ClaimID);
                var res = Convert.ToString(CType) == "1" ? "OD-" : Convert.ToString(CType) == "2" ? "PD-" : Convert.ToString(CType) == "3" ? "BI-" : "";
                result = res + (correctvech + 1);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
    }

    public class ClaimAccidentDetailsHistorymodel : BaseModel
    {
        #region "Properties"
        public int ClaimAccStatusId { get; set; }
        public int AccidentClaimId { get; set; }
        public int ClaimId { get; set; }
        public string Status { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Remarks { get; set; }
        #endregion
    }
    public class ClaimantType
    {
        public String Id { get; set; }
        public String Text { get; set; }
    }

    public class SelectType
    {
        public Int32 Id { get; set; }
        public String Text { get; set; }
    }

    public class ClaimCollection
    {

        #region "Object Properties"
        public string RecordNumber { get; set; }
        public Int64? OrgRecordNumber { get; set; }
        public int? ClaimID { get; set; }
        public int? ClaimType { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public int ClaimantNameId { get; set; }
        public string ClaimantName { get; set; }
        public string ClaimantType { get; set; }
        public int PartyTypeId { get; set; }
        public int CompanyNameId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public List<string> HeaderListCollection { get; set; }
        #endregion

        #region "Public Add New"
        //public ClaimInfoModel AddNewClaimModel(int ClaimId )
        //{
        //    ClaimInfoModel claimObject = new ClaimInfoModel().FetchClaim(ClaimId.ToString()) ;
        //    this.ClaimDetailsCollection.Add(claimObject);
        //    return claimObject;
        //}
        #endregion

        #region "Methods"
        //private ClaimInfoModel Fetch(int ClaimId)
        //{
        // return (from n in this.ClaimDetailsCollection where n.ClaimID == ClaimId select n).FirstOrDefault();
        //}
        #endregion

    }

}
