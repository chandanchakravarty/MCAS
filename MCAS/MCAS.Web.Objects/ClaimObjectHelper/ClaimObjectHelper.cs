using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;
namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimObjectHelper : BaseModel
    {
        public int AccidentClaimId { get; set; }
        public int PolicyId { get; set; }
        public int ClaimId { get; set; }
        public string IPNo { get; set; }
        public string ClaimNo { get; set; }
        public string ClaimStatus { get; set; }
        public int? IsComplete { get; set; }
        public string CallerMenu { get; set; }
        public string BusServiceNo { get; set; }
        public string VehicleNo { get; set; }
        public string AccidentDate { get; set; }
        public string AccidentTime { get; set; }
        public string Tabredirect { get; set; }
        public List<LookUpListItems> TabredirectList { get; set; }
        //public int TimePeriod { get; set; }
        public int? Organization { get; set; }
        public string OrganizationName { get; set; }
        public string AccidentImage { get; set; }
        public string OrganizationType { get; set; }

        //Added for Pvt car and Taxi
        public string CDGIClaimRefNo { get; set; }
        public string InvoiceNo { get; set; }
        public string ClaimType { get; set; }
        public string ClaimTypeName { get; set; }
        public string JobNo { get; set; }
        public string ClaimStatusName { get; set; }

        private ClaimAccidentDetailsModel _accidentDetail = new ClaimAccidentDetailsModel();
        private ClaimEntryInfoModel _claimDetail = new ClaimEntryInfoModel();
        private List<ThirdPartyInfoModel> _thirdParties = new List<ThirdPartyInfoModel>();
        private List<ClaimNotesModel> _claimNotes = new List<ClaimNotesModel>();
        private ClaimFinanceModel _claimFinance = new ClaimFinanceModel();
        private List<ClaimTransactionsModel> _claimTransactions = new List<ClaimTransactionsModel>();
        private List<ClaimAttachmentsModel> _claimAttachments = new List<ClaimAttachmentsModel>();
        private ClaimForCRTXInfoModel _claimCrTxDetail = new ClaimForCRTXInfoModel();

        public ClaimForCRTXInfoModel ClaimCrTxDetails
        {
            get { return _claimCrTxDetail; }
            set { _claimCrTxDetail = value; }
        }


        public ClaimAccidentDetailsModel AccidentDetail { 
            get { return _accidentDetail; } 
            set { _accidentDetail = value; } 
        }
        public ClaimEntryInfoModel ClaimDetail { 
            get { return _claimDetail; } 
            set { _claimDetail = value; } 
        }
        public List<ThirdPartyInfoModel> ThirdParties { 
            get { return _thirdParties; } 
            set { _thirdParties = value; } 
        }
        public List<ClaimNotesModel> ClaimNotes { 
            get { return _claimNotes; } 
            set { _claimNotes = value; } 
        }
        public ClaimFinanceModel ClaimFinance { 
            get { return _claimFinance; } 
            set { _claimFinance = value; } 
        }
        public List<ClaimTransactionsModel> ClaimTransactions { 
            get { return _claimTransactions; } 
            set { _claimTransactions = value; } 
        }
        public List<ClaimAttachmentsModel> ClaimAttachments { 
            get { return _claimAttachments; } 
            set { _claimAttachments = value; } 
        }
        #region fetch Method
        public ClaimObjectHelper Fetch(int AccidentClaimID)
        {
            this.AccidentClaimId = AccidentClaimID;
            var accident = new ClaimAccidentDetailsModel();
            accident.AccidentClaimId = this.AccidentClaimId;
            this.AccidentDetail = accident.Fetch();
            this.ReadOnly = AccidentDetail.ReadOnly;
            var claim = new ClaimEntryInfoModel();
            claim.AccidentClaimId = this.AccidentClaimId;
            this.ClaimDetail = claim.Fetch();
            this.ClaimId = this.ClaimDetail.ClaimID.HasValue ? (int)this.ClaimDetail.ClaimID : 0;
            this.ThirdParties = ThirdPartyInfoModel.Fetchlist(this.ClaimId);
            var claimCrTx = new ClaimForCRTXInfoModel();
            claimCrTx.ClaimID = this.ClaimId;
            this.ClaimCrTxDetails = claimCrTx.Fetch(AccidentClaimID);
            

            var finance = new ClaimFinanceModel();
            finance.ClaimId = this.ClaimId;
            this.ClaimFinance = finance;

            return this;
        }
        #endregion

        public List<LookUpListItems> GetTabredirectList()
        {
            MCASEntities _db = new MCASEntities();
            List<LookUpListItems> list = new List<LookUpListItems>();
            try
            {
                list = (from l in _db.MNT_Lookups
                        where l.Category == "TabRedirect" && l.IsActive == "Y"
                        orderby l.DisplayOrder
                        select new LookUpListItems
                            {
                                Lookup_value = l.Lookupvalue + "-" + l.Description,
                                Lookup_desc = l.Lookupdesc,
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return list;
        }
    }
}
