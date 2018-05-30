using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.ComponentModel;
using MCAS.Web.Objects.ClaimObjectHelper;
using System.Globalization;
using MCAS.Web.Objects.Resources.InsuranceMaster;

namespace MCAS.Web.Objects.MastersHelper
{
    public class InsuranceModel : BaseModel
    {

        MCASEntities _db = new MCASEntities();

        #region Properties

        private DateTime? _PolicyEffectiveFrom = null;
        private DateTime? _PolicyEffectiveTo = null;
        private DateTime? _accidentDate = null;
        public int datalength { get; set; }
        public int startlength { get; set; }
        public int endlength { get; set; }
        public int _prop1 { get; set; }
        public int _prop2 { get; set; }
        public int _prop3 { get; set; }
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
        public int Prop2
        {
            get
            {
                return 0;
            }
            set
            {
                this._prop2 = 0;
            }
        }
        public int Prop3
        {
            get
            {
                return 0;
            }
            set
            {
                this._prop3 = 0;
            }
        }
        public int serialno { get; set; }
        public int? PolicyId { get; set; }
        //[NotEqual("Prop1", ErrorMessage = "Insurer is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVInsurer")]
        public int? CedantId { get; set; }
        public string CedantCode { get; set; }
        public string CedantName { get; set; }
        //[Required(ErrorMessage = "Policy Number is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVPolicyNumber")]
        public string PolicyNo { get; set; }
        //[NotEqual("Prop2", ErrorMessage = "Class is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVClass")]
        public int? ProductId { get; set; }
        //[Required(ErrorMessage = "Class is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVClass")]
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        //[NotEqual("Prop3", ErrorMessage = "Sub Class is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVSubClass")]
        public int? SubClassId { get; set; }
        public string AccidentDatestr { get; set; }
        public string ClaimDatestr { get; set; }
        //[Required(ErrorMessage = "Currency is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVCurrency")]
        public string CurrencyCode { get; set; }
        public string ClassCode { get; set; }
        public string ClassDescription { get; set; }
        public string AdjSurName { get; set; }
        public string ProductclassId { get; set; }
        public List<CurrencyMasterModel> currencylist { get; set; }
        public List<ExpensesCurr> expenseslist { get; set; }

        [DisplayName("Policy Effective From")]
        //[Required(ErrorMessage = "Policy Effective From is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVPolicyEffectiveFrom")]
        public DateTime? PolicyEffectiveFrom
        {
            get { return _PolicyEffectiveFrom; }
            set { _PolicyEffectiveFrom = value; }
        }

        [DisplayName("Policy Effective To")]
        //[Required(ErrorMessage = "Policy Effective To is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVPolicyEffectiveTo")]
        public DateTime? PolicyEffectiveTo
        {
            get { return _PolicyEffectiveTo; }
            set { _PolicyEffectiveTo = value; }
        }

        public DateTime? AccidentDate
        {
            get { return _accidentDate; }
            set { _accidentDate = value; }
        }
        public decimal? Deductible { get; set; }
        //[Required(ErrorMessage = "Premium is required.")]
        [Required(ErrorMessageResourceType = typeof(InsurancePolicyMasterEditor), ErrorMessageResourceName = "RFVPremium")]
        [DisplayName("Premium")]
        public decimal? PremiumAmount { get; set; }

        public decimal? Deduct { get; set; }
        public decimal? Premium { get; set; }
        public decimal? Exchangerate { get; set; }
        [DisplayName("Premium(Local Currency) ")]
        public decimal? PremiumLocalCurrency { get; set; }

        public List<InsuranceModel> Cedantlist { get; set; }
        public List<InsuranceModel> ProductList { get; set; }
        public List<InsuranceModel> SubClassList { get; set; }
        public int? AccidentId { get; set; }
        public string ClaimNo { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
        public string ClmStatus { get; set; }
        public int? ClaimStatus { get; set; }
        public string TPSurname { get; set; }
        public string TPGivenName { get; set; }
        public string Insured { get; set; }
        public int? ClaimId { get; set; }
        public string ClaimOfficer { get; set; }
        public string DutyIO { get; set; }
        public string ClaimType { get; set; }
        public string IPNo { get; set; }
        public DateTime? ClaimDate { get; set; }
        public ClaimAccidentDetail claimdetails { get; set; }
        public string claimantStatus { get; set; }
        public int IsReported { get; set; }
        public int IsReadOnly { get; set; }
        public string VehicleRegnNo { get; set; }
        public string LinkedClaimNo { get; set; }
        public bool IsLinkedWithUnReported { get; set; }
        public string InsurerType { get; set; }
        public override string screenId
        {
            get
            {
                return "250";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "123";
            }

        }

        public string TotalnofRec { get; set; }
        #endregion



        public InsuranceModel Update()
        {
            MNT_InsruanceM insr;

            if (PolicyId.HasValue)
            {
                insr = _db.MNT_InsruanceM.Where(t => t.PolicyId == this.PolicyId.Value).SingleOrDefault();
                insr.CedantId = Convert.ToInt16(this.CedantId);
                insr.ProductId = Convert.ToInt16(this.ProductId);
                insr.SubClassId = Convert.ToInt16(this.SubClassId);
                insr.PolicyNo = this.PolicyNo;
                insr.PolicyEffectiveFrom = Convert.ToDateTime(this.PolicyEffectiveFrom);
                insr.PolicyEffectiveTo = Convert.ToDateTime(this.PolicyEffectiveTo);
                insr.PremiumAmount = Convert.ToDecimal(this.PremiumAmount);
                insr.Deductible = Convert.ToDecimal(this.Deductible);
                insr.CurrencyCode = this.CurrencyCode;
                insr.ExchangeRate = this.Exchangerate;
                insr.PremiumLocalCurrency = this.PremiumLocalCurrency;
                insr.ModifiedBy = this.ModifiedBy;
                insr.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.CreatedBy = insr.CreatedBy;
                this.CreatedOn = insr.CreatedDate == null ? DateTime.MinValue : (DateTime)insr.CreatedDate;
                this.ModifiedOn = insr.ModifiedDate;
                return this;
            }
            else
            {
                insr = new MNT_InsruanceM();
                insr.CedantId = Convert.ToInt16(this.CedantId);
                insr.ProductId = Convert.ToInt16(this.ProductId);
                insr.SubClassId = Convert.ToInt16(this.SubClassId);
                insr.PolicyNo = this.PolicyNo;
                insr.PolicyEffectiveFrom = Convert.ToDateTime(this.PolicyEffectiveFrom);
                insr.PolicyEffectiveTo = Convert.ToDateTime(this.PolicyEffectiveTo);
                insr.PremiumAmount = Convert.ToDecimal(this.PremiumAmount);
                insr.Deductible = Convert.ToDecimal(this.Deductible);
                insr.CurrencyCode = this.CurrencyCode;
                insr.ExchangeRate = this.Exchangerate;
                insr.PremiumLocalCurrency = this.PremiumLocalCurrency;
                insr.CreatedBy = this.CreatedBy;
                insr.CreatedDate = DateTime.Now;
                _db.MNT_InsruanceM.AddObject(insr);
                _db.SaveChanges();
                this.PolicyId = insr.PolicyId;
                this.CreatedOn = (DateTime)insr.CreatedDate;
                return this;
            }
        }

        public static List<InsuranceModel> FetchClaims(string ClaimStatus)
        {
            MCASEntities _db = new MCASEntities();
            int claimstatus = ClaimStatus == "" ? 0 : Convert.ToInt32(ClaimStatus);
            List<string> adjtype = new List<string>() { "ADJ", "SVY" };
            var userId = System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString();
            var itemList = new List<InsuranceModel>();
            var p = (
               from accident in _db.ClaimAccidentDetails
               from claims in _db.CLM_Claims.Where(claim => claim.PolicyId == accident.PolicyId && claim.AccidentClaimId == accident.AccidentClaimId && accident.IsComplete == (claimstatus != 0 ? claimstatus : accident.IsComplete)).DefaultIfEmpty()
               from Policy in _db.MNT_InsruanceM.Where(pol => pol.PolicyId == accident.PolicyId).DefaultIfEmpty()
               from product in _db.MNT_Products.Where(prd => prd.ProductId == Policy.ProductId).DefaultIfEmpty()
               from subclass in _db.MNT_ProductClass.Where(sc => sc.ID == Policy.SubClassId).DefaultIfEmpty()
               from Insurer in _db.MNT_Cedant.Where(Ins => Ins.CedantId == Policy.CedantId).DefaultIfEmpty()
               from co in _db.MNT_Users.Where(co => co.SNo == claims.ClaimsOfficer).DefaultIfEmpty()
               from sp in _db.CLM_ServiceProvider.Where(sp => sp.AccidentId == accident.AccidentClaimId && sp.ClaimantNameId == claims.ClaimID).DefaultIfEmpty()
               //from ad in _db.MNT_Adjusters.Where(ad => ad.AdjusterId == sp.CompanyNameId && adjtype.Contains(ad.AdjusterTypeCode)).DefaultIfEmpty()
               from org in _db.MNT_OrgCountry.Where(org => org.Id == accident.Organization).DefaultIfEmpty()
               from orgAccess in _db.MNT_UserOrgAccess
               where 
               accident.IsComplete == (ClaimStatus != "0" ? claimstatus : accident.IsComplete) && 
               (orgAccess.OrgCode == org.InsurerType && orgAccess.OrgName == org.CountryOrgazinationCode && orgAccess.UserId == userId)
               //&& claims.ClaimantStatus != "2"

               select new InsuranceModel()
               {
                   PolicyId = (Policy == null ? 0 : Policy.PolicyId),
                   PolicyNo = (Policy == null ? "" : Policy.PolicyNo),
                   ProductId = Policy.ProductId,
                   CedantId = (Policy == null ? 0 : Policy.CedantId),
                   SubClassId = Policy.SubClassId,
                   AccidentId = accident.AccidentClaimId,
                   PremiumAmount = 0,
                   Deductible = 0, //Convert.ToDouble(Policy.Deductible),
                   PolicyEffectiveTo = Policy.PolicyEffectiveTo,
                   PolicyEffectiveFrom = Policy.PolicyEffectiveFrom,
                   ClaimNo = accident.ClaimNo,
                   VehicleNo = accident.VehicleNo,
                   DriverName = accident.DriverName,
                   Insured = org.OrganizationName,
                   InsurerType = org.InsurerType,
                   ClaimOfficer = (co == null ? "" : co.UserDispName),
                   DutyIO = accident.DutyIO,
                   ProductCode = product.ProductCode,
                   ProductName = product.ProductDisplayName,
                   CedantCode = Insurer.CedantCode,
                   CedantName = Insurer.CedantName,
                   ClassCode = (subclass == null ? "" : subclass.ClassCode),
                   ClassDescription = (subclass == null ? "" : subclass.ClassDesc),
                   ClmStatus = (claims == null ? "" : claims.ClaimStatus),
                   ClaimStatus = (accident == null ? 1 : accident.IsComplete),
                   TPSurname = (claims == null ? "" : claims.ClaimantName),
                   ClaimId = claims.ClaimID,
                   ReadOnly = ClaimStatus == "2" ? true : false,
                   AccidentDate = accident.AccidentDate,
                   IPNo = accident.IPNo == null ? "" : accident.IPNo,
                   ClaimDate = claims.ClaimDate,
                   VehicleRegnNo = claims.VehicleRegnNo,
                   ClaimType = (claims.ClaimType == null ? "" : claims.ClaimType == 1 ? "OD" : claims.ClaimType == 2 ? "PD" : claims.ClaimType == 3 ? "BI": "RC"),
                   claimantStatus = (claims.ClaimantStatus == null ? "" : claims.ClaimantStatus == "1" ? "Pending " : claims.ClaimantStatus == "2" ? "Finalized " : claims.ClaimantStatus == "3" ? "Cancelled " : "Reopened"),
                   //AdjSurName = (ad != null ? "" : ad.AdjusterName)
                   IsReported = accident.IsReported == null ? 0 : accident.IsReported == true ? 1 : 0,
                   IsReadOnly = accident.IsReadOnly == null ? 0 : accident.IsReadOnly == true ? 1 : 0,
                   LinkedClaimNo = accident.LinkedAccidentClaimId == null ? "" : (from details in _db.ClaimAccidentDetails
                                                                                  where details.AccidentClaimId.Equals((int)accident.LinkedAccidentClaimId)
                                                                                  select details.ClaimNo
                                                                                ).FirstOrDefault(),
                   IsLinkedWithUnReported = (from details in _db.ClaimAccidentDetails
                                             where details.LinkedAccidentClaimId == accident.AccidentClaimId
                                             select details.AccidentClaimId).Any()
               }
               );
            var c = p.Distinct().ToList<InsuranceModel>();
            itemList =
               c.Select((player, index) => new InsuranceModel()
               {
                   PolicyId = player.PolicyId,
                   PolicyNo = player.PolicyNo,
                   ProductId = player.ProductId,
                   CedantId = player.CedantId,
                   SubClassId = player.SubClassId,
                   AccidentId = player.AccidentId,
                   PremiumAmount = player.PremiumAmount,
                   Deductible = player.Deductible,
                   PolicyEffectiveTo = player.PolicyEffectiveTo,
                   PolicyEffectiveFrom = player.PolicyEffectiveFrom,
                   ClaimNo = player.ClaimNo,
                   VehicleNo = player.VehicleNo,
                   DriverName = player.DriverName,
                   Insured = player.Insured,
                   InsurerType = player.InsurerType,
                   ClaimOfficer = player.ClaimOfficer,
                   // AdjSurName = player.AdjSurName,
                   DutyIO = player.DutyIO,
                   ProductCode = player.ProductCode,
                   ProductName = player.ProductName,
                   CedantCode = player.CedantCode,
                   CedantName = player.CedantName,
                   ClassCode = player.ClassCode,
                   ClassDescription = player.ClassDescription,
                   ClmStatus = player.ClmStatus,
                   ClaimStatus = player.ClaimStatus,
                   TPSurname = player.TPSurname,
                   ClaimId = player.ClaimId,
                   ReadOnly = player.ReadOnly,
                   AccidentDate = player.AccidentDate,
                   AccidentDatestr = player.AccidentDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   IPNo = player.IPNo,
                   ClaimDate = player.ClaimDate,
                   ClaimDatestr = player.ClaimDate == null ? "" : player.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   VehicleRegnNo = player.VehicleRegnNo,
                   ClaimType = player.ClaimType,
                   claimantStatus = player.claimantStatus,
                   serialno = index + 1,
                   IsReported = player.IsReported,
                   IsReadOnly = player.IsReadOnly,
                   LinkedClaimNo = player.LinkedClaimNo,
                   IsLinkedWithUnReported = player.IsLinkedWithUnReported
               })
                    .ToList();
            _db.Dispose();
            return itemList;
        }
        public static List<InsuranceModel> FetchAdjClaims(string ClaimStatus)
        {
            MCASEntities _db = new MCASEntities();
            int claimstatus = ClaimStatus == "" ? 0 : Convert.ToInt32(ClaimStatus);
            List<string> adjtype = new List<string>() { "ADJ", "SVY" };
            var userId = System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString();
            var itemList = new List<InsuranceModel>();
            var p = (
               from accident in _db.ClaimAccidentDetails
               from claims in _db.CLM_Claims.Where(claim => claim.PolicyId == accident.PolicyId && claim.AccidentClaimId == accident.AccidentClaimId && accident.IsComplete == (claimstatus != 0 ? claimstatus : accident.IsComplete)).DefaultIfEmpty()
               from Policy in _db.MNT_InsruanceM.Where(pol => pol.PolicyId == accident.PolicyId).DefaultIfEmpty()
               from product in _db.MNT_Products.Where(prd => prd.ProductId == Policy.ProductId).DefaultIfEmpty()
               from subclass in _db.MNT_ProductClass.Where(sc => sc.ID == Policy.SubClassId).DefaultIfEmpty()
               from Insurer in _db.MNT_Cedant.Where(Ins => Ins.CedantId == Policy.CedantId).DefaultIfEmpty()
               from co in _db.MNT_Users.Where(co => co.SNo == claims.ClaimsOfficer).DefaultIfEmpty()
               from sp in _db.CLM_ServiceProvider.Where(sp => sp.AccidentId == accident.AccidentClaimId && sp.ClaimantNameId == claims.ClaimID).DefaultIfEmpty()
               from org in _db.MNT_OrgCountry.Where(org => org.Id == accident.Organization).DefaultIfEmpty()
               from orgAccess in _db.MNT_UserOrgAccess
               //.Where(orgAccess => orgAccess.OrgCode == org.InsurerType && orgAccess.OrgName == org.CountryOrgazinationCode).DefaultIfEmpty()
               where accident.IsComplete == (ClaimStatus != "0" ? claimstatus : accident.IsComplete) && orgAccess.UserId == userId
                && (orgAccess.OrgCode == org.InsurerType && orgAccess.OrgName == org.CountryOrgazinationCode && orgAccess.UserId == userId) 
                && claims.ClaimantStatus != "2" && claims.ClaimantStatus != "3"

               select new InsuranceModel()
               {
                   PolicyId = (Policy == null ? 0 : Policy.PolicyId),
                   PolicyNo = (Policy == null ? "" : Policy.PolicyNo),
                   ProductId = Policy.ProductId,
                   CedantId = (Policy == null ? 0 : Policy.CedantId),
                   SubClassId = Policy.SubClassId,
                   AccidentId = accident.AccidentClaimId,
                   PremiumAmount = 0,
                   Deductible = 0, //Convert.ToDouble(Policy.Deductible),
                   PolicyEffectiveTo = Policy.PolicyEffectiveTo,
                   PolicyEffectiveFrom = Policy.PolicyEffectiveFrom,
                   ClaimNo = accident.ClaimNo,
                   VehicleNo = accident.VehicleNo,
                   DriverName = accident.DriverName,
                   Insured = org.OrganizationName,
                   InsurerType = org.InsurerType,
                   ClaimOfficer = (co == null ? "" : co.UserDispName),
                   DutyIO = accident.DutyIO,
                   ProductCode = product.ProductCode,
                   ProductName = product.ProductDisplayName,
                   CedantCode = Insurer.CedantCode,
                   CedantName = Insurer.CedantName,
                   ClassCode = (subclass == null ? "" : subclass.ClassCode),
                   ClassDescription = (subclass == null ? "" : subclass.ClassDesc),
                   ClmStatus = (claims == null ? "" : claims.ClaimStatus),
                   ClaimStatus = (accident == null ? 1 : accident.IsComplete),
                   TPSurname = (claims == null ? "" : claims.ClaimantName),
                   ClaimId = claims.ClaimID,
                   ReadOnly = ClaimStatus == "2" ? true : false,
                   AccidentDate = accident.AccidentDate,
                   IPNo = accident.IPNo == null ? "" : accident.IPNo,
                   ClaimDate = claims.ClaimDate,
                   VehicleRegnNo = claims.VehicleRegnNo,
                   ClaimType = (claims.ClaimType == null ? "" : claims.ClaimType == 1 ? "OD" : claims.ClaimType == 2 ? "PD" : claims.ClaimType == 3 ? "BI" : "RC"),
                   claimantStatus = (claims.ClaimantStatus == null ? "" : claims.ClaimantStatus == "1" ? "Pending " : claims.ClaimantStatus == "2" ? "Finalized " : claims.ClaimantStatus == "3" ? "Cancelled " : "Reopened"),
                   //AdjSurName = (ad != null ? "" : ad.AdjusterName)
                   IsReported = accident.IsReported == null ? 0 : accident.IsReported == true ? 1 : 0,
                   IsReadOnly = accident.IsReadOnly == null ? 0 : accident.IsReadOnly == true ? 1 : 0,
                   LinkedClaimNo = accident.LinkedAccidentClaimId == null ? "" : (from details in _db.ClaimAccidentDetails
                                                                                  where details.AccidentClaimId.Equals((int)accident.LinkedAccidentClaimId)
                                                                                  select details.ClaimNo
                                                                                ).FirstOrDefault(),
                   IsLinkedWithUnReported = (from details in _db.ClaimAccidentDetails
                                             where details.LinkedAccidentClaimId == accident.AccidentClaimId
                                             select details.AccidentClaimId).Any()
               }
               );
            var c = p.Distinct().ToList<InsuranceModel>();
            itemList =
               c.Select((player, index) => new InsuranceModel()
               {
                   PolicyId = player.PolicyId,
                   PolicyNo = player.PolicyNo,
                   ProductId = player.ProductId,
                   CedantId = player.CedantId,
                   SubClassId = player.SubClassId,
                   AccidentId = player.AccidentId,
                   PremiumAmount = player.PremiumAmount,
                   Deductible = player.Deductible,
                   PolicyEffectiveTo = player.PolicyEffectiveTo,
                   PolicyEffectiveFrom = player.PolicyEffectiveFrom,
                   ClaimNo = player.ClaimNo,
                   VehicleNo = player.VehicleNo,
                   DriverName = player.DriverName,
                   Insured = player.Insured,
                   InsurerType = player.InsurerType,
                   ClaimOfficer = player.ClaimOfficer,
                   DutyIO = player.DutyIO,
                   ProductCode = player.ProductCode,
                   ProductName = player.ProductName,
                   CedantCode = player.CedantCode,
                   CedantName = player.CedantName,
                   ClassCode = player.ClassCode,
                   ClassDescription = player.ClassDescription,
                   ClmStatus = player.ClmStatus,
                   ClaimStatus = player.ClaimStatus,
                   TPSurname = player.TPSurname,
                   ClaimId = player.ClaimId,
                   ReadOnly = player.ReadOnly,
                   AccidentDate = player.AccidentDate,
                   AccidentDatestr = player.AccidentDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   IPNo = player.IPNo,
                   ClaimDate = player.ClaimDate,
                   ClaimDatestr = player.ClaimDate == null ? "" : player.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   VehicleRegnNo = player.VehicleRegnNo,
                   ClaimType = player.ClaimType,
                   claimantStatus = player.claimantStatus,
                   serialno = index + 1,
                   IsReported = player.IsReported,
                   IsReadOnly = player.IsReadOnly,
                   LinkedClaimNo = player.LinkedClaimNo,
                   IsLinkedWithUnReported = player.IsLinkedWithUnReported
               })
                    .ToList();
            _db.Dispose();
            return itemList;
        }
        public static List<InsuranceModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var result =
               (
               from Policy in _db.MNT_InsruanceM
               from accident in _db.ClaimAccidentDetails.Where(acc => acc.PolicyId == Policy.PolicyId).DefaultIfEmpty()
               from claims in _db.CLM_Claims.Where(claim => claim.PolicyId == accident.PolicyId && claim.AccidentClaimId == accident.AccidentClaimId).DefaultIfEmpty()
               from product in _db.MNT_Products.Where(prd => prd.ProductId == Policy.ProductId).DefaultIfEmpty()
               from subclass in _db.MNT_ProductClass.Where(sc => sc.ID == Policy.SubClassId).DefaultIfEmpty()
               from Insurer in _db.MNT_Cedant.Where(Ins => Ins.CedantId == Policy.CedantId).DefaultIfEmpty()
               from pc in _db.MNT_ProductClass.Where(pc => pc.ID == Policy.SubClassId).DefaultIfEmpty()
               from co in _db.MNT_ClaimOfficerDetail.Where(co => co.TranId == claims.ClaimsOfficer).DefaultIfEmpty()
               from sp in _db.CLM_ServiceProvider.Where(sp => sp.AccidentId == accident.AccidentClaimId && sp.ClaimantNameId == claims.ClaimID).DefaultIfEmpty()
               from org in _db.MNT_OrgCountry.Where(org => org.Id == accident.Organization).DefaultIfEmpty()
               select new InsuranceModel()
               {
                   PolicyId = Policy.PolicyId,
                   PolicyNo = Policy.PolicyNo,
                   ProductId = Policy.ProductId,
                   CedantId = Policy.CedantId,
                   SubClassId = Policy.SubClassId,
                   AccidentId = accident.AccidentClaimId,
                   PremiumAmount = 0,
                   Deductible = 0,
                   PolicyEffectiveTo = Policy.PolicyEffectiveTo,
                   PolicyEffectiveFrom = Policy.PolicyEffectiveFrom,
                   ClaimNo = accident.ClaimNo,
                   VehicleNo = accident.VehicleNo,
                   DriverName = accident.DriverName,
                   Insured = org.OrganizationName,
                   ClaimOfficer = (co == null ? "" : co.ClaimentOfficerName),
                   DutyIO = accident.DutyIO,
                   ProductCode = product.ProductCode,
                   ProductName = product.ProductDisplayName,
                   CedantCode = Insurer.CedantCode,
                   CedantName = Insurer.CedantName,
                   ClassCode = (subclass == null ? "" : subclass.ClassCode),
                   ClassDescription = (subclass == null ? "" : subclass.ClassDesc),
                   ClmStatus = (claims == null ? "" : claims.ClaimStatus),
                   TPSurname = (claims == null ? "" : claims.ClaimantName),
                   ClaimStatus = (accident == null ? 0 : accident.IsComplete),
                   ClaimId = claims.ClaimID,
                   IsReported = accident.IsReported == null ? 0 : accident.IsReported == true ? 1 : 0,
                   IsReadOnly = accident.IsReadOnly == null ? 0 : accident.IsReadOnly == true ? 1 : 0

               }
               ).Distinct().ToList<InsuranceModel>();

            _db.Dispose();
            return result;
        }

        public static List<InsuranceModel> FetchRecoveryClaims()
        {
            MCASEntities _db = new MCASEntities();
            var userId = System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString();
            var itemList = new List<InsuranceModel>();
            var p = (
               from accident in _db.ClaimAccidentDetails
               from claims in _db.CLM_Claims.Where(claim => claim.PolicyId == accident.PolicyId && claim.AccidentClaimId == accident.AccidentClaimId).DefaultIfEmpty()
               from Policy in _db.MNT_InsruanceM.Where(pol => pol.PolicyId == accident.PolicyId).DefaultIfEmpty()
               from product in _db.MNT_Products.Where(prd => prd.ProductId == Policy.ProductId).DefaultIfEmpty()
               from subclass in _db.MNT_ProductClass.Where(sc => sc.ID == Policy.SubClassId).DefaultIfEmpty()
               from Insurer in _db.MNT_Cedant.Where(Ins => Ins.CedantId == Policy.CedantId).DefaultIfEmpty()
               from co in _db.MNT_Users.Where(co => co.SNo == claims.ClaimsOfficer).DefaultIfEmpty()
               from sp in _db.CLM_ServiceProvider.Where(sp => sp.AccidentId == accident.AccidentClaimId && sp.ClaimantNameId == claims.ClaimID).DefaultIfEmpty()
               from org in _db.MNT_OrgCountry.Where(org => org.Id == accident.Organization).DefaultIfEmpty()
               from cms in _db.CLM_MandateSummary.Where(cms => cms.AccidentClaimId == accident.AccidentClaimId && cms.ClaimID == claims.ClaimID && (cms.ClaimType == 1 || cms.ClaimType == 3))
               from orgAccess in _db.MNT_UserOrgAccess
               where accident.IsRecoveryOD == "Y" && accident.IsComplete == 2 && claims.ClaimType != 2
                     && (orgAccess.OrgCode == org.InsurerType && orgAccess.OrgName == org.CountryOrgazinationCode && orgAccess.UserId == userId)
               select new InsuranceModel()
               {
                   PolicyId = (Policy == null ? 0 : Policy.PolicyId),
                   PolicyNo = (Policy == null ? "" : Policy.PolicyNo),
                   ProductId = Policy.ProductId,
                   CedantId = (Policy == null ? 0 : Policy.CedantId),
                   SubClassId = Policy.SubClassId,
                   AccidentId = accident.AccidentClaimId,
                   PremiumAmount = 0,
                   Deductible = 0,
                   PolicyEffectiveTo = Policy.PolicyEffectiveTo,
                   PolicyEffectiveFrom = Policy.PolicyEffectiveFrom,
                   ClaimNo = accident.ClaimNo,
                   VehicleNo = accident.VehicleNo,
                   DriverName = accident.DriverName,
                   Insured = org.OrganizationName,
                   ClaimOfficer = (co == null ? "" : co.UserDispName),
                   DutyIO = accident.DutyIO,
                   ProductCode = product.ProductCode,
                   ProductName = product.ProductDisplayName,
                   CedantCode = Insurer.CedantCode,
                   CedantName = Insurer.CedantName,
                   ClassCode = (subclass == null ? "" : subclass.ClassCode),
                   ClassDescription = (subclass == null ? "" : subclass.ClassDesc),
                   ClmStatus = (claims == null ? "" : claims.ClaimStatus),
                   ClaimStatus = (accident == null ? 1 : accident.IsComplete),
                   TPSurname = (claims == null ? "" : claims.ClaimantName),
                   ClaimId = claims.ClaimID,
                   AccidentDate = accident.AccidentDate,
                   IPNo = accident.IPNo == null ? "" : accident.IPNo,
                   ClaimDate = claims.ClaimDate,
                   VehicleRegnNo = claims.VehicleRegnNo,
                   ClaimType = (claims.ClaimType == null ? "" : claims.ClaimType == 1 ? "OD" : claims.ClaimType == 2 ? "PD" : claims.ClaimType == 3 ? "BI" : "RC"),
                   claimantStatus = (claims.ClaimantStatus == null ? "" : claims.ClaimantStatus == "1" ? "Pending " : claims.ClaimantStatus == "2" ? "Finalized " : claims.ClaimantStatus == "3" ? "Cancelled " : "Reopened")
               }
               );
            var c = p.Distinct().ToList<InsuranceModel>();
            itemList =
               c.Select((player, index) => new InsuranceModel()
               {
                   PolicyId = player.PolicyId,
                   PolicyNo = player.PolicyNo,
                   ProductId = player.ProductId,
                   CedantId = player.CedantId,
                   SubClassId = player.SubClassId,
                   AccidentId = player.AccidentId,
                   PremiumAmount = player.PremiumAmount,
                   Deductible = player.Deductible,
                   PolicyEffectiveTo = player.PolicyEffectiveTo,
                   PolicyEffectiveFrom = player.PolicyEffectiveFrom,
                   ClaimNo = player.ClaimNo,
                   VehicleNo = player.VehicleNo,
                   DriverName = player.DriverName,
                   Insured = player.Insured,
                   ClaimOfficer = player.ClaimOfficer,
                   DutyIO = player.DutyIO,
                   ProductCode = player.ProductCode,
                   ProductName = player.ProductName,
                   CedantCode = player.CedantCode,
                   CedantName = player.CedantName,
                   ClassCode = player.ClassCode,
                   ClassDescription = player.ClassDescription,
                   ClmStatus = player.ClmStatus,
                   ClaimStatus = player.ClaimStatus,
                   TPSurname = player.TPSurname,
                   ClaimId = player.ClaimId,
                   ReadOnly = player.ReadOnly,
                   AccidentDate = player.AccidentDate,
                   AccidentDatestr = player.AccidentDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   IPNo = player.IPNo,
                   ClaimDate = player.ClaimDate,
                   ClaimDatestr = player.ClaimDate == null ? "" : player.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   VehicleRegnNo = player.VehicleRegnNo,
                   ClaimType = player.ClaimType,
                   claimantStatus = player.claimantStatus,
                   serialno = index + 1
               })
                    .ToList();
            _db.Dispose();
            return itemList;
        }

        public static List<InsuranceModel> FetchClaimsEnq()
        {
            MCASEntities _db = new MCASEntities();
            var userId = System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString();
            var itemList = new List<InsuranceModel>();
            var p = (
               from accident in _db.ClaimAccidentDetails
               from claims in _db.CLM_Claims.Where(claim => claim.PolicyId == accident.PolicyId && claim.AccidentClaimId == accident.AccidentClaimId).DefaultIfEmpty()
               from Policy in _db.MNT_InsruanceM.Where(acc => acc.PolicyId == accident.PolicyId).DefaultIfEmpty()
               from product in _db.MNT_Products.Where(prd => prd.ProductId == Policy.ProductId).DefaultIfEmpty()
               from subclass in _db.MNT_ProductClass.Where(sc => sc.ID == Policy.SubClassId).DefaultIfEmpty()
               from Insurer in _db.MNT_Cedant.Where(Ins => Ins.CedantId == Policy.CedantId).DefaultIfEmpty()
               from pc in _db.MNT_ProductClass.Where(pc => pc.ID == Policy.SubClassId).DefaultIfEmpty()
               from co in _db.MNT_Users.Where(co => co.SNo == claims.ClaimsOfficer).DefaultIfEmpty()
               from sp in _db.CLM_ServiceProvider.Where(sp => sp.AccidentId == accident.AccidentClaimId && sp.ClaimantNameId == claims.ClaimID).DefaultIfEmpty()
               from org in _db.MNT_OrgCountry.Where(org => org.Id == accident.Organization).DefaultIfEmpty()
               from orgAccess in _db.MNT_UserOrgAccess.Where(orgAccess => orgAccess.OrgCode == org.InsurerType && orgAccess.OrgName == org.CountryOrgazinationCode && orgAccess.UserId == userId).DefaultIfEmpty() 
               //where (orgAccess.OrgCode == org.InsurerType && orgAccess.OrgName == org.CountryOrgazinationCode && orgAccess.UserId == userId)
               //&& (claims.ClaimantStatus == "2" || claims.ClaimantStatus == "3")
               select new InsuranceModel()
               {
                   PolicyId = (Policy == null ? 0 : Policy.PolicyId),
                   PolicyNo = (Policy == null ? "" : Policy.PolicyNo),
                   ProductId = Policy.ProductId,
                   CedantId = (Policy == null ? 0 : Policy.CedantId),
                   SubClassId = Policy.SubClassId,
                   AccidentId = accident.AccidentClaimId,
                   PremiumAmount = 0,
                   Deductible = 0, //Convert.ToDouble(Policy.Deductible),
                   PolicyEffectiveTo = Policy.PolicyEffectiveTo,
                   PolicyEffectiveFrom = Policy.PolicyEffectiveFrom,
                   ClaimNo = accident.ClaimNo,
                   VehicleNo = accident.VehicleNo,
                   DriverName = accident.DriverName,
                   Insured = org.OrganizationName,
                   ClaimOfficer = (co == null ? "" : co.UserDispName),
                   DutyIO = accident.DutyIO,
                   ProductCode = product.ProductCode,
                   ProductName = product.ProductDisplayName,
                   CedantCode = Insurer.CedantCode,
                   CedantName = Insurer.CedantName,
                   ClassCode = (subclass == null ? "" : subclass.ClassCode),
                   ClassDescription = (subclass == null ? "" : subclass.ClassDesc),
                   ClmStatus = (claims == null ? "" : claims.ClaimStatus),
                   ClaimStatus = (accident == null ? 1 : accident.IsComplete),
                   TPSurname = (claims == null ? "" : claims.ClaimantName),
                   ClaimId = claims.ClaimID,
                   AccidentDate = accident.AccidentDate,
                   IPNo = accident.IPNo == null ? "" : accident.IPNo,
                   ClaimDate = claims.ClaimDate,
                   VehicleRegnNo = claims.VehicleRegnNo,
                   ClaimType = (claims.ClaimType == null ? "" : claims.ClaimType == 1 ? "OD" : claims.ClaimType == 2 ? "PD" : claims.ClaimType == 3 ? "BI" : "RC"),
                   claimantStatus = (claims.ClaimantStatus == null ? "" : claims.ClaimantStatus == "1" ? "Pending " : claims.ClaimantStatus == "2" ? "Finalized " : claims.ClaimantStatus == "3" ? "Cancelled " : "Reopened"),
                   //AdjSurName = (ad != null ? "" : ad.AdjusterName)
                   IsReported = accident.IsReported == null ? 0 : accident.IsReported == true ? 1 : 0,
                   IsReadOnly = accident.IsReadOnly == null ? 0 : accident.IsReadOnly == true ? 1 : 0
               }
               );
            var c = p.Distinct().ToList<InsuranceModel>();
            itemList =
               c.Select((player, index) => new InsuranceModel()
               {
                   PolicyId = player.PolicyId,
                   PolicyNo = player.PolicyNo,
                   ProductId = player.ProductId,
                   CedantId = player.CedantId,
                   SubClassId = player.SubClassId,
                   AccidentId = player.AccidentId,
                   PremiumAmount = player.PremiumAmount,
                   Deductible = player.Deductible,
                   PolicyEffectiveTo = player.PolicyEffectiveTo,
                   PolicyEffectiveFrom = player.PolicyEffectiveFrom,
                   ClaimNo = player.ClaimNo,
                   VehicleNo = player.VehicleNo,
                   DriverName = player.DriverName,
                   Insured = player.Insured,
                   ClaimOfficer = player.ClaimOfficer,
                   // AdjSurName = player.AdjSurName,
                   DutyIO = player.DutyIO,
                   ProductCode = player.ProductCode,
                   ProductName = player.ProductName,
                   CedantCode = player.CedantCode,
                   CedantName = player.CedantName,
                   ClassCode = player.ClassCode,
                   ClassDescription = player.ClassDescription,
                   ClmStatus = player.ClmStatus,
                   ClaimStatus = player.ClaimStatus,
                   TPSurname = player.TPSurname,
                   ClaimId = player.ClaimId,
                   ReadOnly = player.ReadOnly,
                   AccidentDate = player.AccidentDate,
                   AccidentDatestr = player.AccidentDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   ClaimDate = player.ClaimDate,
                   ClaimDatestr = player.ClaimDate == null ? "" : player.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                   IPNo = player.IPNo,
                   VehicleRegnNo = player.VehicleRegnNo,
                   ClaimType = player.ClaimType,
                   claimantStatus = player.claimantStatus,
                   serialno = index + 1,
                   IsReported = player.IsReported,
                   IsReadOnly = player.IsReadOnly
               })
                    .ToList();
            _db.Dispose();
            return itemList;

        }

        public static List<InsuranceModel> FetchPolicy()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<InsuranceModel>();
            var list = (from l in _db.MNT_InsruanceM where l.PolicyNo != null select l);
            if (list.Any())
            {
                foreach (var data in list)
                {
                    var prodCode = (from p in _db.MNT_Products where p.ProductId == data.ProductId select p).FirstOrDefault();
                    var cedantCode = (from c in _db.MNT_Cedant where c.CedantId == data.CedantId select c.CedantName).FirstOrDefault();
                    var subclassCode = (from s in _db.MNT_ProductClass where s.ID == data.SubClassId select s).FirstOrDefault();
                    var claimdetail = (from cd in _db.ClaimAccidentDetails where cd.PolicyId == data.PolicyId select cd).FirstOrDefault();

                    item.Add(new InsuranceModel() { PolicyId = data.PolicyId, CedantName = cedantCode, PolicyNo = data.PolicyNo, Premium = data.PremiumAmount, Deduct = data.Deductible, PolicyEffectiveTo = Convert.ToDateTime(data.PolicyEffectiveTo), PolicyEffectiveFrom = Convert.ToDateTime(data.PolicyEffectiveFrom) });

                }
            }
            return item;
        }

        protected List<InsuranceModel> loadSubClass(bool addAll = true)
        {

            MCASEntities obj = new MCASEntities();
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = (from l in obj.MNT_ProductClass where l.Status == "1" select new InsuranceModel { SubClassId = l.ID, ClassCode = l.ClassCode, ClassDescription = l.ClassCode + " - " + l.ClassDesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new InsuranceModel() { SubClassId = 0, ClassDescription = "[Select...]" });
            }
            return list;

        }

        protected List<InsuranceModel> loadMainClass(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = (from l in obj.MNT_Products where l.ProductStatus == 1 select new InsuranceModel { ProductId = l.ProductId, ProductCode = l.ProductCode, ProductDisplayName = l.ProductCode + " - " + l.ProductDisplayName }).ToList();
            if (addAll)
            {
                list.Insert(0, new InsuranceModel() { ProductId = 0, ProductDisplayName = "[Select...]" });
            }
            return list;
        }

        protected List<InsuranceModel> loadCedant(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = (from l in obj.MNT_Cedant select new InsuranceModel { CedantId = l.CedantId, CedantName = l.CedantName }).ToList();
            if (addAll)
            {
                list.Insert(0, new InsuranceModel() { CedantId = 0, CedantName = "[Select...]" });
            }
            return list;
        }
        public static List<InsuranceModel> Fetchall(string ClaimNo, string IPNo, DateTime? LossDate, string VehicleNo, string ClaimantName, string VehicleRegnNo, string claimStatus, int currPage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO #TempTable SELECT distinct temp.IsComplete,temp.AccidentClaimId,temp.ClaimID,temp.ClaimType,temp.ClaimOfficer,temp.ClaimNo,temp.VehicleNo,temp.TPSurname,temp.ClaimStatus,temp.AccidentDate,temp.IPNo,temp.ClaimDate,temp.VehicleRegnNo,temp.ClaimantStatus, temp.InsurerType,temp.CountryOrgazinationCode From(SELECT g.IsComplete,g.AccidentClaimId,clm.ClaimID,clm.ClaimType,isnull(co.UserDispName,'') as ClaimOfficer,isnull(g.ClaimNo,'') as ClaimNo,isnull(g.VehicleNo,'') as VehicleNo,isnull(clm.ClaimantName,'') as TPSurname,isnull(g.IsComplete,0) as ClaimStatus,g.AccidentDate,g.IPNo,clm.ClaimDate,clm.VehicleRegnNo,clm.ClaimantStatus,org.InsurerType,org.CountryOrgazinationCode FROM ClaimAccidentDetails g  left join CLM_Claims clm on g.PolicyId = clm.PolicyId and g.AccidentClaimId = clm.AccidentClaimId  left join MNT_InsruanceM u on u.PolicyId = g.PolicyId  left join MNT_Products c on u.ProductId = c.ProductId left join MNT_Cedant d on u.CedantId = d.CedantId left join MNT_ProductClass p on u.SubClassId = p.ID left join MNT_Users co on co.SNo  = clm.ClaimsOfficer left join CLM_ServiceProvider sp on sp.AccidentId = g.AccidentClaimId and sp.ClaimantNameId =clm.ClaimID left join MNT_Adjusters ad on ad.AdjusterId = sp.CompanyNameId and ad.AdjusterTypeCode in('ADJ','SVY')left join MNT_OrgCountry org on org.Id = g.Organization join CLM_MandateSummary ms on clm.AccidentClaimId = ms.AccidentClaimId and clm.ClaimID = ms.ClaimID where g.IsComplete='2' and org.InsurerType not in ('TX','PC') )temp, MNT_UserOrgAccess orgAccess Where orgAccess.OrgCode = temp.InsurerType and orgAccess.OrgName = temp.CountryOrgazinationCode and orgAccess.UserId =");
            sb.Append("'" + System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString() + "'");
            if ((ClaimNo != null && ClaimNo != "") || (IPNo != null && IPNo != "") | (LossDate.Value != DateTime.MinValue) | (VehicleNo != null && VehicleNo != "") | (ClaimantName != null && ClaimantName != "") | (VehicleRegnNo != null && VehicleRegnNo != ""))
            {
                sb.Append(" and");
            }


            if (ClaimNo != null && ClaimNo != "")
            {
                sb.Append(" UPPER(temp.ClaimNo) Like UPPER('%" + ClaimNo + "%')");
                sb.Append(" and ");
            }

            if (IPNo != null && IPNo != "")
            {
                sb.Append(" UPPER(temp.IPNo) Like UPPER('%" + IPNo + "%')");
                sb.Append(" and ");
            }

            if (LossDate.Value != DateTime.MinValue)
            {
                string dd = LossDate.Value.ToString("yyyy-MM-dd");
                sb.Append(" CAST(temp.AccidentDate AS DATE) = '" + dd + "'");
                sb.Append(" and ");
            }
            if (VehicleNo != null && VehicleNo != "")
            {
                sb.Append(" UPPER(temp.VehicleNo) Like UPPER('%" + VehicleNo + "%')");
                sb.Append(" and ");
            }
            if (ClaimantName != null && ClaimantName != "")
            {
                sb.Append(" UPPER(temp.TPSurname) Like UPPER('%" + ClaimantName + "%')");
                sb.Append(" and ");
            }
            if (VehicleRegnNo != null && VehicleRegnNo != "")
            {
                sb.Append(" UPPER(temp.VehicleRegnNo) Like UPPER('%" + VehicleRegnNo + "%')");
                sb.Append(" and ");
            }
            string fin = sb.ToString().TrimEnd();
            string endval = fin.Split(' ').Last();
            string queryString = string.Empty;
            if (endval == "and")
            {
                queryString = fin.Substring(0, fin.LastIndexOf(" ") < 0 ? 0 : fin.LastIndexOf(" "));
            }
            else
            {
                queryString = fin;
            }
            MCASEntities obj = new MCASEntities();
            var List = obj.Proc_GetPaymentProcessSearch(queryString).ToList();

            var total = List.Count();
            var pageSize = 10; // set your page size, which is number of records per page

            var page = 1; // set current page number, must be >= 1

            if (currPage == 0)
            {
                page = 1;
            }
            else
            {
                page = currPage;
            }
            var skip = pageSize * (page - 1);

            var item = new List<InsuranceModel>();
            var item1 = new InsuranceModel();
            if (List.Any())
            {
                //var item = new List<InsuranceModel>();
                item = (from data in List
                        select new InsuranceModel()
                        {
                            AccidentId = data.AccidentClaimId,
                            PremiumAmount = 0,
                            Deductible = 0, //Convert.ToDouble(Policy.Deductible),
                            ClaimNo = data.ClaimNo,
                            VehicleNo = data.VehicleNo,
                            ClaimOfficer = data.ClaimOfficer,
                            TPSurname = (data.TPSurname == null ? "" : data.TPSurname),
                            ClaimId = data.ClaimID,
                            AccidentDate = data.AccidentDate,
                            AccidentDatestr = data.AccidentDate == null ? "" : data.AccidentDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                            IPNo = data.IPNo == null ? "" : data.IPNo,
                            ClaimDate = data.ClaimDate,
                            ClaimDatestr = data.ClaimDate == null ? "" : data.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                            VehicleRegnNo = data.VehicleRegnNo,
                            ClaimType = (data.ClaimType == null ? "" : data.ClaimType == 1 ? "OD" : data.ClaimType == 2 ? "PD" : data.ClaimType == 3 ? "BI" : "RC"),
                            claimantStatus = (data.ClaimantStatus == null ? "" : data.ClaimantStatus == 1 ? "Pending " : data.ClaimantStatus == 2 ? "Finalized " : data.ClaimantStatus == 3 ? "Cancelled " : "Reopened")

                        }).Skip(skip).Take(10).ToList<InsuranceModel>();

                item[item.Count - 1].TotalnofRec = total.ToString();
            }
            else
            {
                item1.TotalnofRec = total.ToString();
            }
            //if (List.Any())
            //{
            //    int i = 0;
            //    foreach (var data in List)
            //    {
            //        i++;
            //        item.Add(new InsuranceModel()
            //        {
            //            serialno = i,
            //            AccidentId = data.AccidentClaimId,
            //            PremiumAmount = 0,
            //            Deductible = 0, //Convert.ToDouble(Policy.Deductible),
            //            ClaimNo = data.ClaimNo,
            //            VehicleNo = data.VehicleNo,
            //            ClaimOfficer = data.ClaimOfficer,
            //            TPSurname = (data.TPSurname == null ? "" : data.TPSurname),
            //            ClaimId = data.ClaimID,
            //            AccidentDate = data.AccidentDate,
            //            AccidentDatestr = data.AccidentDate == null ? "" : data.AccidentDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
            //            IPNo = data.IPNo == null ? "" : data.IPNo,
            //            ClaimDate = data.ClaimDate,
            //            ClaimDatestr = data.ClaimDate == null ? "" : data.ClaimDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
            //            VehicleRegnNo = data.VehicleRegnNo,
            //            ClaimType = (data.ClaimType == null ? "" : data.ClaimType == 1 ? "OD" : data.ClaimType == 2 ? "PD" : data.ClaimType == 3 ? "BI" : "RC"),
            //            claimantStatus = (data.ClaimantStatus == null ? "" : data.ClaimantStatus == 1 ? "Pending " : data.ClaimantStatus == 2 ? "Finalized " : data.ClaimantStatus == 3 ? "Cancelled " : "Reopened")
            //        });
            //    }
            //}
            obj.Dispose();
            return item;

        }
        public static IQueryable<InsuranceModel> FetchEnquiryClaims(int ClaimStatus)
        {
            MCASEntities obj = new MCASEntities();
            var searchResult = (from g in obj.ClaimAccidentDetails
                                join clm in obj.CLM_Claims on new { g.PolicyId, g.AccidentClaimId } equals new { clm.PolicyId, clm.AccidentClaimId } into cc
                                from clm in cc.DefaultIfEmpty()
                                join u in obj.MNT_InsruanceM on g.PolicyId equals u.PolicyId into ug
                                from u in ug.DefaultIfEmpty()
                                join c in obj.MNT_Products on u.ProductId equals c.ProductId into uc
                                from c in uc.DefaultIfEmpty()
                                join d in obj.MNT_Cedant on u.CedantId equals d.CedantId into cg
                                from d in cg.DefaultIfEmpty()
                                join p in obj.MNT_ProductClass on u.SubClassId equals p.ID into pc
                                from p in pc.DefaultIfEmpty()
                                join co in obj.MNT_Users on clm.ClaimsOfficer equals co.SNo into cd
                                from co in cd.DefaultIfEmpty()
                                join tp in obj.CLM_ServiceProvider on new { AccClaimId = g.AccidentClaimId, ClmId = clm.ClaimID } equals new { AccClaimId = tp.AccidentId, ClmId = tp.ClaimantNameId } into tc
                                from tp in tc.DefaultIfEmpty()
                                join org in obj.MNT_OrgCountry on g.Organization equals org.Id into og
                                from org in og.DefaultIfEmpty()
                                where g.IsComplete == (ClaimStatus != 0 ? ClaimStatus : g.IsComplete)
                                select new InsuranceModel
                                {
                                    PolicyId = u.PolicyId,
                                    AccidentId = g.AccidentClaimId,
                                    PolicyNo = u.PolicyNo,
                                    ProductId = u.ProductId,
                                    CedantId = u.CedantId,
                                    SubClassId = u.SubClassId,
                                    PremiumAmount = 0,
                                    Deductible = 0,
                                    DutyIO = g.DutyIO,
                                    Insured = org.OrganizationName,
                                    PolicyEffectiveTo = u.PolicyEffectiveTo,
                                    PolicyEffectiveFrom = u.PolicyEffectiveFrom,
                                    ProductCode = c.ProductCode,
                                    ProductName = c.ProductDisplayName,
                                    ClassDescription = p != null ? p.ClassDesc : "",
                                    ClaimNo = g != null ? g.ClaimNo : "",
                                    VehicleNo = g != null ? g.VehicleNo : "",
                                    CedantCode = d.CedantCode,
                                    CedantName = d != null ? d.CedantName : "",
                                    DriverName = g != null ? g.DriverName : "",
                                    TPSurname = (clm == null ? "" : clm.ClaimantName),
                                    ClmStatus = clm != null ? clm.ClaimStatus : "",
                                    ClaimStatus = (g == null ? 1 : g.IsComplete),
                                    ClaimOfficer = co != null ? co.UserDispName : "",
                                    ClaimId = clm.ClaimID,
                                }).Distinct().AsQueryable();
            return searchResult;
        }

    }
    public class InsurenaceModelSearch
    {
        public int? AccidentId { get; set; }
        public int? PolicyId { get; set; }
        public string ClaimNo { get; set; }
        public string IPNo { get; set; }
        public string VehicleNo { get; set; }
        public string AccidentDatestr { get; set; }
        public string TPSurname { get; set; }
        public string VehicleRegnNo { get; set; }
        public string ClaimType { get; set; }
        public string ClaimDatestr { get; set; }
        public string claimantStatus { get; set; }
        public string ClaimOfficer { get; set; }
        public string InsurerType { get; set; }
        public string IsReported { get; set; }
        public string IsReadOnly { get; set; }
        public string LinkedClaimNo { get; set; }
    }
}





