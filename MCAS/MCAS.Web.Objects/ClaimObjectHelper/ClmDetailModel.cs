using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using MCAS.Entity;
using MCAS.Web.Objects.MastersHelper;
using System.ComponentModel;
using MCAS.Web.Objects.CommonHelper;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClmDetailModel
    {
        private DateTime? _lawerDate = null;

        public int ClaimId { get; set; }
        [StringLength(100)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]

        public string InvoiceNo { get; set; }
        public string JobNo { get; set; }
        public string CustomerCode { get; set; }
        public string BusinessArea { get; set; }
        public string TPVehicleNo { get; set; }
        public string LawerRef { get; set; }
        public string OwnLawyer { get; set; }
        public string TPInsurer { get; set; }
        public string TPRef { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LawyerDate
        {
            get { return _lawerDate; }
            set { _lawerDate = value; }
        }

        public string LawyerGIADRM { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateToGIADRM { get; set; }
        public string WriteIssued { get; set; }
        public string WriteNo { get; set; }
        public string Sharellocation { get; set; }
        public string ContractorInvoiceNo { get; set; }
        public string WSONo { get; set; }
        public Decimal WsoInvoiceAmt { get; set; }
        public string WsoCnNo { get; set; }
        public Decimal WsoCNAmt { get; set; }
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReopenedDate { get; set; }
        public string RecordReopenedReason { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime RecordCancellationDate { get; set; }

        public string RecordCancellationReason { get; set; }
        public int ClaimType { get; set; }
        public string ClaimType1 { get; set; }
        //public List<ClaimantType> ClaimTypeList { get; set; }
        public List<LookUpListItems> ClaimTypeList { get; set; }
        public Decimal ClaimAmt { get; set; }
        public string ClaimStatus { get; set; }
        public string CaseTypeL1 { get; set; }
        public string CaseTypeL2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FileReceivedDate { get; set; }
        public string Collisiontype { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClaimDate { get; set; }
        public string Surveyor { get; set; }
        public string CDGEStatus { get; set; }


        public int NoOfDaysForRepairs { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FollowUpDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfSurvey { get; set; }
        public string AdminSupport { get; set; }
        public string DriversLiability { get; set; }

        [DisplayName("Office-In-Charge(OIC)")]
        public int OfficeInCharge { get; set; }
        public string Office_InCharge { get; set; }
        public List<ClaimOfficerModel> ClaimOfficerList { get; set; }
        public string CaseStatus { get; set; }
        public string SettledBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }
        public Decimal InvoiceAmt { get; set; }
        public string GST { get; set; }
        public Decimal ActualAmt { get; set; }
        public Decimal ExcessAmt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ConfirmDate { get; set; }
        public Decimal ConfirmAmt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinalSettleDate { get; set; }
        public Decimal SettledAmt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TimeBarDate { get; set; }
        public string PaymentDetails { get; set; }
        public string FileArchievedRef { get; set; }
        public string ChequeDetails { get; set; }
        public string NatureOfAcc { get; set; }
        public string WsActRcvr { get; set; }
        public string ClaimantStatus { get; set; }
        public List<ClaimantType> ClaimantStatusList { get; set; }
        public List<ClaimantType> CaseStatusList { get; set; }


        //By Apurva and Ashish
        public static List<LookUpListItems> ClaimTypeValue(string category, bool addAll = true, bool addNone = false)
        {
            return LookUpListItems.Fetch(category, addAll, addNone);
        }




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

        public ClmDetailModel FetchAllLists(ClmDetailModel model)
        {
            try
            {
                model.ClaimOfficerList = FetchClaimOfficer("CO");
                model.ClaimantStatusList = FetchLookUpList("ClaimantType");
                model.CaseStatusList = FetchLookUpList("CaseStatus");
                return model;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
