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
using MCAS.Web.Objects.Resources.ClaimProcessing;
using System.Globalization;
using System.Data;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class IsFutureDateAttribute : ValidationAttribute, IClientValidatable
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
                ValidationType = "isfuturedate"
            };
        }
    }





    public class IsFutureTimeAttribute : ValidationAttribute, IClientValidatable
    {
        public string OtherProperty { get; private set; }
        public IsFutureTimeAttribute(string otherProperty)
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
            if (!(value == null || (Convert.ToDateTime(Convert.ToDateTime(otherValue).ToString("dd/MM/yyyy") + " " + Convert.ToString(value) + ":00") <= Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":00"))))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "isfuturetime"
            };
        }

    }

    public class ClaimAccidentDetailsModel : BaseModel
    {


        #region Properties
        private claimAddressDetailsModel _addressOwner = new claimAddressDetailsModel();
        public claimAddressDetailsModel OwnerAddressDlt
        {
            get { return _addressOwner; }
            set { _addressOwner = value; }
        }

        private claimAddressDetailsModel _addressDriver = new claimAddressDetailsModel();
        public claimAddressDetailsModel DriverAddressDlt
        {
            get { return _addressDriver; }
            set { _addressDriver = value; }
        }

        private DateTime? _accidentDate = null;
        private DateTime? _reportedDate = null;
        private DateTime? _inputDate = null;
        private DateTime? _dateofFinding = null;
        private DateTime? _dateJoined = null;
        private DateTime? _dateResigned = null;
        private string _accidentTime = null;
        public string UserId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        [DisplayName("IP No.")]
        public string IPNo { get; set; }
        [DisplayName("Claim Number")]
        public string ClaimNo { get; set; }
        public string CDGEClaimRefNo { get; set; }
        public int? IsComplete { get; set; }
        [Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVBusServiceNumber")]
        public string BusServiceNo { get; set; }
        //[Required(ErrorMessage = "Vehicle Number is required.")]
        public string VehicleNo { get; set; }
        [DisplayName("Accident Date")]
        [Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVAccidentDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [IsFutureDate(ErrorMessage = "Accident Date can not be future date.")]
        public DateTime? AccidentDate
        {
            get { return _accidentDate; }
            set { _accidentDate = value; }
        }


        [DisplayName("Accident Time")]
        //[Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVAccidentTime")]
        [IsFutureTime("AccidentDate", ErrorMessage = "Accident Time can not be greater then current time.")]
        public string AccidentTime
        {
            get { return _accidentTime; }
            set { _accidentTime = value; }
        }
        public int TimePeriod { get; set; }
        [Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVOrganization")]
        public int? Organization { get; set; }
        public string AccidentImage { get; set; }
        [DisplayName("Reported Date")]
        //[Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVReportedDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? ReportedDate
        {
            get { return _reportedDate; }
            set { _reportedDate = value; }
        }
        [DisplayName("Input Date")]
        //[Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVInputDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? InputDate
        {
            get { return _inputDate; }
            set { _inputDate = value; }
        }
        public string Facts { get; set; }
        public string Damages { get; set; }
        [DisplayName("Date of Final Finding")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateofFinding
        {
            get { return _dateofFinding; }
            set { _dateofFinding = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Joined")]
        public DateTime? DateJoined
        {
            get { return _dateJoined; }
            set { _dateJoined = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Resigned")]
        public DateTime? DateResigned
        {
            get { return _dateResigned; }
            set { _dateResigned = value; }
        }

        public string InvestStatus { get; set; }
        public string Results { get; set; }
        public int? OperatingHours { get; set; }
        public int? ChckClaimComplete { get; set; }
        public int ChkODStatus { get; set; }
        public int ChkTPStatus { get; set; }
        public string BOIResults { get; set; }
        public int? FinalLiability { get; set; }
        public List<LookUpListItems> FinalLiabilityList { get; set; }
        public List<LookUpListItems> CollisionTypeList { get; set; }
        [DisplayName("Duty IO")]
        public string DutyIO { get; set; }
        public string Make { get; set; }
        public string ModelNo { get; set; }
        public string DriverEmployeeNo { get; set; }
        public string DriverName { get; set; }
        public string DriverNRICNo { get; set; }
        public string InitialEstimate { get; set; }
        public string InsurerClaim { get; set; }
        public string MandateReqd { get; set; }

        public string LossTypeCode { get; set; }
        public string LossNatureCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVPDBI")]
        public string TPClaimentStatus { get; set; }
        [Required(ErrorMessageResourceType = typeof(ClaimAccident), ErrorMessageResourceName = "RFVOD")]
        public string ODStatus { get; set; }
        public string BusCaptainFault { get; set; }
        public string ODAssignmentTranId { get; set; }
        public string TPAssignmentTranId { get; set; }

        public int? ReportedRefId { get; set; }
        public bool? IsReported { get; set; }
        public int Prop1 { get; set; }
        public string IsRecoveryOD { get; set; }
        public string IsRecoveryBI { get; set; }
        public string AccidentLoc { get; set; }
        public string AccidentResult { get; set; }
        public string hidprop { get; set; }
        public int? Interchange { get; set; }
        public string OrganizationType { get; set; }
        public int? HChkApprovedPayment { get; set; }
        public int? HClaimId { get; set; }
        public string InitialLiability { get; set; }
        public string CollisionType { get; set; }
        public string hiddenprop { get; set; }
        public string OrgCategory { get; set; }
        public string OwnerName { get; set; }
        public string hidOrgprop { get; set; }

        //New Fields for upload process 
        public string District { get; set; }
        public string RoadName { get; set; }
        public string MinorInjury { get; set; }
        public string SeriousInjury { get; set; }
        public string Fatal { get; set; }


        public List<InterchangeM> InterchangeList { get; set; }
        public List<ClaimEntryInfoModel> ClaimEntryInfo { get; set; }
        public List<LookUpListItems> genderlookupvalue { get; set; }
        public List<LookUpListItems> OperatingHoursList { get; set; }
        public IEnumerable<SelectListItem> generallookupvalue { get; set; }
        public List<LossTypeModel> LossTypeList { get; set; }
        public List<LossNatureListItems> LossNatureList { get; set; }
        public IEnumerable<SelectListItem> TimeformatRadioList { get; set; }
        public List<VehicleBusCaptainModel> BusCaptainlist { get; set; }
        public List<cedantList> InsurerList { get; set; }
        public List<orgCategory> OrgCatList { get; set; }
        public List<LookUpListItems> OrgCatTypeList { get; set; }
        public List<LookUpListItems> OwnerNameList { get; set; }
        public List<LookUpListItems> DistrictCodeList { get; set; }
        public int? LinkedAccidentClaimId { get; set; }
        public override string screenId
        {
            get
            {
                return "130";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "130";
            }

        }
        #endregion

        #region "Public Shared Methods"
        public ClaimAccidentDetailsModel Fetch()
        {
            MCASEntities obj = new MCASEntities();
            var item = new ClaimAccidentDetailsModel() { ViewMode = this.ViewMode };
            if (AccidentClaimId.HasValue)
            {
                var Accident = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == this.AccidentClaimId select l).FirstOrDefault();
                if (Accident != null)
                {
                    item.AccidentClaimId = this.AccidentClaimId;
                    item.PolicyId = Accident.PolicyId;
                    item.IPNo = Accident.IPNo;
                    item.ClaimNo = Accident.ClaimNo;
                    item.BusServiceNo = Accident.BusServiceNo;
                    item.VehicleNo = Accident.VehicleNo;
                    item.AccidentDate = Accident.AccidentDate;
                    item.AccidentTime = Accident.AccidentTime;
                    item.TimePeriod = Accident.TimePeriod != null ? (int)Accident.TimePeriod : 0;
                    /* uncommented by sanjay */
                    item.Organization = Accident.Organization;
                    //item.AccidentImage = Accident.AccidentImage;
                    item.BusCaptainFault = Accident.BusCaptainFault;
                    item.TPAssignmentTranId = Accident.TPAssignmentTranId;
                    item.ODAssignmentTranId = Accident.ODAssignmentTranId;
                    item.AccidentLoc = Accident.AccidentLoc;
                    item.ReportedDate = Accident.ReportedDate;
                    item.Facts = Accident.Facts;
                    item.Damages = Accident.Damages;
                    item.LossTypeCode = Accident.LossTypeCode;
                    item.LossNatureCode = Accident.LossNatureCode;
                    item.TPClaimentStatus = Accident.TPClaimentStatus;
                    item.ODStatus = Accident.ODStatus;
                    item.Interchange = Accident.Interchange;
                    item.IsReported = Accident.IsReported;
                    item.ReportedRefId = Accident.ReportedRefId;
                    item.OperatingHours = Accident.OperatingHours;

                    item.DateofFinding = Accident.DateofFinding;
                    item.InvestStatus = Accident.InvestStatus;
                    item.Results = Accident.Results;
                    item.BOIResults = Accident.BOIResults;
                    item.FinalLiability = Accident.FinalLiability;
                    item.DutyIO = Accident.DutyIO;

                    item.Make = Accident.Make;
                    item.ModelNo = Accident.ModelNo;


                    item.DriverEmployeeNo = Accident.DriverEmployeeNo;
                    item.DriverName = Accident.DriverName;
                    item.DriverNRICNo = Accident.DriverNRICNo;
                    item.DateResigned = Accident.DateResigned;
                    item.DateJoined = Accident.DateJoined;
                    item.InitialEstimate = Accident.InitialEstimate;
                    item.InsurerClaim = Accident.InsurerClaim;
                    item.MandateReqd = Accident.MandateReqd;
                    item.IsComplete = Accident.IsComplete;
                    item.ReadOnly = Accident.IsReadOnly == null ? false : (bool)Accident.IsReadOnly;
                    item.LinkedAccidentClaimId = Accident.LinkedAccidentClaimId;
                    item.IsRecoveryBI = Accident.IsRecoveryBI;
                    item.IsRecoveryOD = Accident.IsRecoveryOD;
                    item.District = Accident.District;
                    item.RoadName = Accident.RoadName;
                    item.SeriousInjury = Accident.SeriousInjury;
                    item.MinorInjury = Accident.MinorInjury;
                    item.Fatal = Accident.Fatal;
                    var appPayment = (from m in obj.CLM_PaymentSummary where m.AccidentClaimId == Accident.AccidentClaimId && m.ApprovePayment == "Y" select m).FirstOrDefault();
                    if (appPayment != null)
                    {
                        item.HChkApprovedPayment = 1;
                    }
                    else
                    {
                        item.HChkApprovedPayment = 0;
                    }

                    var hasClaim = (from m in obj.CLM_Claims where m.AccidentClaimId == Accident.AccidentClaimId && m.ClaimantStatus != "3" select m).FirstOrDefault();
                    if (hasClaim != null)
                    {
                        item.HClaimId = 1;
                    }
                    else
                    {
                        item.HClaimId = 0;
                    }
                }
            }
            return item;
        }

        //Start By Jitendra
        public List<ClaimAccidentDetailsModel> ReportedClaimList(string AccidentId)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ClaimAccidentDetailsModel>();
            if (!string.IsNullOrEmpty(AccidentId))
            {
                int AccidentClaimId = Convert.ToInt32(AccidentId);
                var details = obj.ClaimAccidentDetails.Where(t => t.AccidentClaimId.Equals(AccidentClaimId)).FirstOrDefault();

                item = (from l in obj.ClaimAccidentDetails
                        where l.VehicleNo == details.VehicleNo && l.BusServiceNo == details.BusServiceNo
                            && l.AccidentDate.Year.Equals(details.AccidentDate.Year)
                            && l.AccidentDate.Month.Equals(details.AccidentDate.Month)
                            && l.AccidentDate.Day.Equals(details.AccidentDate.Day)
                            && l.IsReported == true
                        //&& (l.DriverName == details.DriverName
                        //&& l.DriverEmployeeNo == details.DriverEmployeeNo

                        select new ClaimAccidentDetailsModel()
                        {
                            AccidentClaimId = l.AccidentClaimId,
                            PolicyId = l.PolicyId,

                            IPNo = l.IPNo,
                            ClaimNo = l.ClaimNo,
                            BusServiceNo = l.BusServiceNo,
                            VehicleNo = l.VehicleNo,
                            AccidentDate = l.AccidentDate,
                            AccidentTime = l.AccidentTime,
                            TimePeriod = l.TimePeriod != null ? (int)l.TimePeriod : 0,
                            Organization = l.Organization,
                            BusCaptainFault = l.BusCaptainFault,
                            TPAssignmentTranId = l.TPAssignmentTranId,
                            ODAssignmentTranId = l.ODAssignmentTranId,
                            AccidentLoc = l.AccidentLoc,
                            ReportedDate = l.ReportedDate,
                            Facts = l.Facts,
                            Damages = l.Damages,
                            LossTypeCode = l.LossTypeCode,
                            LossNatureCode = l.LossNatureCode,
                            TPClaimentStatus = l.TPClaimentStatus,
                            ODStatus = l.ODStatus,
                            Interchange = l.Interchange,
                            IsReported = l.IsReported,
                            ReportedRefId = l.ReportedRefId,
                            OperatingHours = l.OperatingHours,
                            DateofFinding = l.DateofFinding,
                            InvestStatus = l.InvestStatus,
                            Results = l.Results,
                            BOIResults = l.BOIResults,
                            FinalLiability = l.FinalLiability,
                            DutyIO = l.DutyIO,
                            Make = l.Make,
                            ModelNo = l.ModelNo,
                            DriverEmployeeNo = l.DriverEmployeeNo,
                            DriverName = l.DriverName,
                            DriverNRICNo = l.DriverNRICNo,
                            DateResigned = l.DateResigned,
                            DateJoined = l.DateJoined,
                            InitialEstimate = l.InitialEstimate,
                            InsurerClaim = l.InsurerClaim,
                            MandateReqd = l.MandateReqd,
                            IsComplete = l.IsComplete,
                            District = l.District,
                            RoadName = l.RoadName,
                            SeriousInjury= l.SeriousInjury,
                            MinorInjury = l.MinorInjury,
                            Fatal = l.Fatal
                        }).ToList();

            }
            return item;
        }
        //End By Jitendra
        public static string converttooriginalname(string p)
        {
            var category = "";
            if (p == "BU")
            {
                category = "Bus";
            }
            else if (p == "TR")
            {
                category = "Train";
            }
            else if (p == "TX")
            {
                category = "Taxi";
            }
            else if (p == "PC")
            {
                category = "Private Cars";
            }
            else if (p == "PB")
            {
                category = "Private Bus";
            }
            else if (p == "RV")
            {
                category = "Rental Vehicles";
            }

            return category;
        }





        #endregion

        #region Methods
        public ClaimAccidentDetailsModel Save()
        {
            MCASEntities obj = new MCASEntities();
            var model = new ClaimAccidentDetailsModel();
            if (obj.Connection.State == System.Data.ConnectionState.Closed)
                obj.Connection.Open();
            using (System.Data.Common.DbTransaction transaction = obj.Connection.BeginTransaction())
            {
                try
                {
                    ClaimAccidentDetail accdetail;
                    if (AccidentClaimId.HasValue && AccidentClaimId != 0)
                    {
                        string[] ignoreList = new string[] { "Createddate", "CreatedBy", "IsComplete", "IsReported", "ReportedRefId", "UploadReportNo", "IsReadOnly", "LinkedAccidentClaimId" };
                        accdetail = obj.ClaimAccidentDetails.Where(x => x.AccidentClaimId == this.AccidentClaimId.Value).FirstOrDefault();
                        DataMapper.Map(this, accdetail, true, ignoreList);
                        accdetail.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        accdetail.ModifiedDate = DateTime.Now;
                        if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                        {
                            UpdateNSaveAddress(obj);
                        }
                        this.ModifiedOn = DateTime.Now;
                        this.CreatedOn = Convert.ToDateTime(accdetail.CreatedDate);
                        this.CreatedBy = accdetail.CreatedBy;
                        this.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.AccidentResult = "Records updated successfully.";

                    }
                    else
                    {
                        accdetail = new ClaimAccidentDetail();
                        DataMapper.Map(this, accdetail, true);
                        accdetail.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        accdetail.CreatedDate = DateTime.Now;
                        if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "bu") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tr"))
                        {
                            accdetail.IsComplete = 1;
                        }
                        else if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                        {
                            accdetail.IsComplete = 2;
                        }
                        obj.ClaimAccidentDetails.AddObject(accdetail);
                        obj.SaveChanges();
                        this.AccidentClaimId = accdetail.AccidentClaimId;
                        if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                        {
                            CreateNSaveAddress(obj);
                        }
                        obj.Proc_UpdateTransactiontableForAccident(accdetail.AccidentClaimId, obj.tranAuditTrailList[0].TranAuditId);
                        if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "bu") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tr"))
                        {
                            this.ClaimNo = ClaimEntryInfoModel.GetPermanentClaimNo(accdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "UCLM", obj);
                        }
                        else if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                        {
                            MNT_OrgCountry mnt_org;

                            if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx"))
                            {
                               // var OrgId = obj.ClaimAccidentDetails.Where(x=>x.AccidentClaimId).Select(x => x.Organization == mnt_org.Id);
                               //// var InitialSelect = (from x in obj.MNT_OrgCountry where x.InsurerType == "TX" && x.Id == OrgId select x.Initial).SingleOrDefault();
                               // var InitialSelect = obj.MNT_OrgCountry.Where(x => x.InsurerType == "TX" && x.Id == OrgId).Select(y=>y.Initial).FirstOrDefault();

                                var InitialSelect = (from m in obj.ClaimAccidentDetails join n in obj.MNT_OrgCountry on m.Organization equals n.Id where m.AccidentClaimId == accdetail.AccidentClaimId && n.InsurerType == "TX" select n.Initial).FirstOrDefault();
                                this.ClaimNo = ClaimEntryInfoModel.GetClaimsNoForCarAndTaxi(accdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "UCLM", InitialSelect, obj);
                            }
                            if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                            {
                                this.ClaimNo = ClaimEntryInfoModel.GetClaimsNoForCarAndTaxi(accdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "UCLM", "PTE", obj);
                            }

                        }
                        else
                        {
                            this.ClaimNo = "";
                        }

                     
                        var save = obj.Proc_UpdateClaimAccDetClaimNo(this.ClaimNo, this.AccidentClaimId);
                        this.PolicyId = accdetail.PolicyId;
                        this.AccidentImage = accdetail.AccidentImage;
                        this.CreatedOn = DateTime.Now;
                        this.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.AccidentResult = "Records saved successfully.";
                        if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "bu") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tr"))
                        {
                            HttpContext.Current.Session["ScreenNameDash"] = "207";
                        }
                        else if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                        {
                            HttpContext.Current.Session["ScreenNameDash"] = "208";
                        }
                    }
                    obj.SaveChanges();
                    transaction.Commit();
                    obj.Dispose();
                    return this;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    obj.Dispose();
                    throw (ex);
                }
            }
        }
        #endregion
        public void CreateNSaveAddress(MCASEntities obj)
        {
            Clm_DriverAddressInfo AddressInfo1 = new Clm_DriverAddressInfo();
            DataMapper.Map(this.OwnerAddressDlt, AddressInfo1, true);
            AddressInfo1.AddressOf = "HR";
            AddressInfo1.AccidentClaimId = this.AccidentClaimId;
            obj.Clm_DriverAddressInfo.AddObject(AddressInfo1);
            Clm_DriverAddressInfo AddressInfo2 = new Clm_DriverAddressInfo();
            DataMapper.Map(this.DriverAddressDlt, AddressInfo2, true);
            AddressInfo2.AddressOf = "DR";
            AddressInfo2.AccidentClaimId = this.AccidentClaimId;
            obj.Clm_DriverAddressInfo.AddObject(AddressInfo2);
        }

        public void UpdateNSaveAddress(MCASEntities obj)
        {
            var HirerAddress = obj.Clm_DriverAddressInfo.Where(x => x.AccidentClaimId == this.AccidentClaimId && x.AddressOf == "HR").FirstOrDefault();
            var DriverAddress = obj.Clm_DriverAddressInfo.Where(x => x.AccidentClaimId == this.AccidentClaimId && x.AddressOf == "DR").FirstOrDefault();
            this.OwnerAddressDlt.AccidentClaimId = HirerAddress.AccidentClaimId ?? default(int);
            this.OwnerAddressDlt.Id = HirerAddress.Id;
            this.DriverAddressDlt.AccidentClaimId = DriverAddress.AccidentClaimId ?? default(int);
            this.DriverAddressDlt.Id = DriverAddress.Id;
            DataMapper.Map(this.OwnerAddressDlt, HirerAddress, true);
            DataMapper.Map(this.DriverAddressDlt, DriverAddress, true);
        }

        public ClaimAccidentDetailsModel FetchAll(int AccidentClaimId)
        {
            MCASEntities obj = new MCASEntities();
            ClaimAccidentDetailsModel model = new ClaimAccidentDetailsModel();
            var accident = obj.ClaimAccidentDetails.Where(x => x.AccidentClaimId == AccidentClaimId).FirstOrDefault();
            DataMapper.Map(accident, model, true);
            var HirerAddress = obj.Clm_DriverAddressInfo.Where(x => x.AccidentClaimId == AccidentClaimId && x.AddressOf == "HR").FirstOrDefault();
            var DriverAddress = obj.Clm_DriverAddressInfo.Where(x => x.AccidentClaimId == AccidentClaimId && x.AddressOf == "DR").FirstOrDefault();
            DataMapper.Map(HirerAddress, model.OwnerAddressDlt, true);
            DataMapper.Map(DriverAddress, model.DriverAddressDlt, true);
            if (accident != null)
            {
                model.CreatedBy = accident.CreatedBy;
                model.CreatedOn = Convert.ToDateTime(accident.CreatedDate);
                if (accident.ModifiedDate != null || accident.ModifiedDate.ToString() != "")
                {
                    model.ModifiedOn = Convert.ToDateTime(accident.ModifiedDate);
                    model.ModifiedBy = accident.ModifiedBy;
                }
                model.ReadOnly = accident.IsReadOnly == null ? false : (bool)accident.IsReadOnly;
                model.InitialLiability = accident.InitialLiability;
                model.CollisionType = accident.CollisionType;
                model.CDGEClaimRefNo = accident.CDGIClaimRef;
            }
            return model;
        }

        public static List<InterchangeM> fetchInterchangeList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<InterchangeM>();
            var Interchange = _db.Proc_GetInterChangeList().ToList();
            if (Interchange.Any())
            {
                item.Add(new InterchangeM() { Interchange_Id = 0, Interchange_Name = "[Select...]" });
                foreach (var data in Interchange)
                {
                    item.Add(new InterchangeM() { Interchange_Id = data.Id, Interchange_Name = data.InterchangeName });
                }
            }
            return item;
        }

        //Add New Method for TFS 21522
        public static List<LookUpListItems> FetchCommonMasterData(string category, int accclmid, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
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
                            var lookupinfo = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.LookupID == ids orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).FirstOrDefault();
                            list.Add(lookupinfo);
                        }
                    }
                }
            }
            list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            obj.Dispose();
            return list;
        }

        public static List<LookUpListItems> FetchCommonMasterDataForNew(string category, int clmorgId, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            //var clmorgId = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == accclmid select l.Organization).FirstOrDefault();
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
                            var lookupinfo = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.LookupID == ids orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).FirstOrDefault();
                            list.Add(lookupinfo);
                        }
                    }
                }
            }
            list.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            obj.Dispose();
            return list;
        }


        public int CheckClaimComplete()
        {
            MCASEntities obj = new MCASEntities();
            int? Chk1 = this.AccidentClaimId == null ? 1 : (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == this.AccidentClaimId select l.IsComplete).FirstOrDefault();
            int Chk = Chk1 ?? default(int);
            return Chk;
        }
        public int CheckODStatus()
        {
            MCASEntities obj = new MCASEntities();
            int? Chk1 = this.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == this.AccidentClaimId && l.ClaimType == 1 select l.ClaimID).FirstOrDefault();
            int Chk = Chk1 ?? default(int);
            return Chk;
        }
        public int CheckTPStatus()
        {
            MCASEntities obj = new MCASEntities();
            int? Chk1 = this.AccidentClaimId == null ? 0 : (from l in obj.CLM_Claims where l.AccidentClaimId == this.AccidentClaimId && (l.ClaimType == 2 || l.ClaimType == 3) select l.ClaimID).FirstOrDefault();
            int Chk = Chk1 ?? default(int);
            return Chk;
        }


        public static List<orgCategory> fetchOrganizationList(string UserId, string Caller, string OrgType)
        {
            MCASEntities obj = new MCASEntities();
            List<orgCategory> list = new List<orgCategory>();
            list = (from l in obj.Proc_GetOrganizationListAccident(UserId, Caller, OrgType)
                    select new orgCategory()
                    {
                        OrgType = l.OrgType,
                        Description = l.Description.Trim()
                    }
                        ).ToList();
            list.Insert(0, new orgCategory() { OrgType = 0, Description = "[Select...]" });
            obj.Dispose();
            return list;
        }

        public static List<cedantList> fetchInsurerList()
        {
            MCASEntities obj = new MCASEntities();
            var Clist = obj.MNT_Cedant.Where(x => x.InsurerType == "1" || x.InsurerType == "3").Select(y => new { cedantId = y.CedantId, cedantName = y.CedantName }).ToList();
            List<cedantList> cedants = new List<cedantList>();
            cedants = (from item in Clist
                       select new cedantList()
                       {
                           cedantId = Convert.ToString(item.cedantId),
                           cedantName = item.cedantName
                       }).ToList();
            cedants.Insert(0, new cedantList() { cedantId = "0", cedantName = "[Select...]" });
            return cedants;
        }

        public static string fetchOrganisationType(string user, int? OrgTypeId)
        {
            MCASEntities obj = new MCASEntities();
            string CountryOrgCode = obj.MNT_OrgCountry.Where(x => x.Id == OrgTypeId).Select(y => y.CountryOrgazinationCode).FirstOrDefault();
            string OrgCode = obj.MNT_UserOrgAccess.Where(x => x.OrgName == CountryOrgCode && x.UserId == user).Select(y => y.OrgCode).FirstOrDefault();
            return OrgCode;
        }

        public static List<orgCategory> fetchInactiveOrgList(string UserId)
        {
            MCASEntities obj = new MCASEntities();
            List<orgCategory> list = new List<orgCategory>();
            list = (from l in obj.Proc_GetInactiveOrgList(UserId)
                    select new orgCategory()
                    {
                        OrgType = l.OrgType,
                        Description = l.Description.Trim()
                    }
                        ).ToList();
            obj.Dispose();
            return list;
        }


        public static List<LookUpListItems> fetchOrganizationCategory()
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            list = (from m in obj.MNT_Lookups
                    where m.Category == "ORGCategory"
                    select new LookUpListItems() { Lookup_value = m.Lookupvalue, Lookup_desc = m.Lookupdesc }).ToList();
            list.Insert(0, new LookUpListItems() { Lookup_value = "0", Lookup_desc = "[Select...]" });
            obj.Dispose();
            return list;
        }

        public static int? ProcessCancelClaim(int? accidentClaimId, string LoggedInUserName)
        {
            MCASEntities obj = new MCASEntities();
            var result = 0;
            try
            {

                result = Convert.ToInt32(obj.Proc_ProcessCancelClaim(accidentClaimId, LoggedInUserName).FirstOrDefault());

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

        public static JsonResult FetchFillMakeAndModel(string VehNo)
        {
            MCASEntities obj = new MCASEntities();
            JsonResult result = new JsonResult();
            var results = (from vl in obj.MNT_VehicleListingMaster join model in obj.MNT_MOTOR_MODEL on vl.VehicleModelCode equals model.ModelCode join make in obj.MNT_Motor_Make on vl.VehicleMakeCode equals make.MakeCode where vl.VehicleRegNo == VehNo select new { vl.VehicleRegNo, model.ModelName, make.MakeName }).FirstOrDefault();
            if (results != null)
            {
                result.Data = new { vehicleRegNo = results.VehicleRegNo, VehicleMakeCode = results.MakeName, VehicleModelCode = results.ModelName };
            }
            else
            {
                result.Data = new { vehicleRegNo = "", VehicleMakeCode = "", VehicleModelCode = "" };
            }
            obj.Dispose();
            return result;
        }
    }

    public class TimeStatus
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
    }

    public class Status
    {
        public String ID { get; set; }
        public String Name { get; set; }
    }

    public class orgCategory
    {
        public string orgName { get; set; }
        public int OrgType { get; set; }
        public string Description { get; set; }
    }

    public class cedantList
    {
        public string cedantId { get; set; }
        public string cedantName { get; set; }
    }

    public class claimAddressDetailsModel : BaseModel
    {
        public int Id { get; set; }
        public int AccidentClaimId { get; set; }
        public string Name { get; set; }
        public string DriverType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Gender { get; set; }
        public string NRIC_PPNo { get; set; }
        public string ContactNo { get; set; }
        public string Country { get; set; }
        public string FaxNo { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public List<LookUpListItems> GenderList { get; set; }

        public List<UserCountryListItems> Countrylist { get; set; }
    }





}
