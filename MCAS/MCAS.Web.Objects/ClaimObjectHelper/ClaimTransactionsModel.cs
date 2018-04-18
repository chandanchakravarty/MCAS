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
    public class ClaimTransactionsModel : BaseModel
    {
        #region Properties
        private DateTime? _transactionDate = null;
        private DateTime? _authorizedDate = null;
        private DateTime? _processedDate = null;

        public int? TransactionId { get; set; }
        public int ClaimId { get; set; }
        public int PolicyId { get; set; }

        [Required(ErrorMessage = "Transaction Date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Transaction Date")]
        public DateTime? TransactionDate
        {
            get { return _transactionDate; }
            set { _transactionDate = value; }
        }
        [Required(ErrorMessage = "Transaction Type is required.")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage = "Creditor Name is required.")]
        public string CreditorName { get; set; }

        [Required(ErrorMessage = "Expense Code is required.")]
        public string ExpenseCode { get; set; }

        [Required(ErrorMessage = "Amount Paid is required.")]
        public decimal? AmountPaid { get; set; }

        [Required(ErrorMessage = "Authorized By is required.")]
        public string Authorizedby { get; set; }

        [Required(ErrorMessage = "Authorized Date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Authorized Date")]
        public DateTime? AuthorizedDate
        {
            get { return _authorizedDate; }
            set { _authorizedDate = value; }
        }
        [Required(ErrorMessage = "Authorized Time is required.")]
        public string AuthorizedTime { get; set; }

        [Required(ErrorMessage = "Processed Date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Processed Date")]
        public DateTime? ProcessedDate
        {
            get { return _processedDate; }
            set { _processedDate = value; }
        }
        public string hiddenprop { get; set; }

        #endregion

        #region "Public Shared Methods"
        public static List<ClaimTransactionsModel> Fetch(int? ClaimId)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ClaimTransactionsModel>();
             var model = new ClaimTransactionsModel();
            string CreatedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
            var NotesList = (from l in obj.CLM_Transactions where l.ClaimId == ClaimId select l);
            if (NotesList.Any())
            {
                foreach (var data in NotesList)
                {
                    item.Add(new ClaimTransactionsModel() { TransactionId = data.TransactionId, ClaimId = data.ClaimId, PolicyId = data.PolicyId, TransactionDate = data.TransactionDate, ExpenseCode = data.ExpenseCode, AmountPaid = data.AmountPaid, CreditorName = data.CreditorName, Authorizedby = data.Authorizedby, AuthorizedDate = data.AuthorizedDate, AuthorizedTime = data.AuthorizedTime, CreatedBy = CreatedBy1,CreatedOn=DateTime.Now});
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public ClaimTransactionsModel Update()
        {
            MCASEntities obj = new MCASEntities();
            CLM_Transactions Claimtransinfo;

            if (TransactionId.HasValue)
            {
                Claimtransinfo = obj.CLM_Transactions.Where(x => x.TransactionId == this.TransactionId.Value).FirstOrDefault();
                DataMapper.Map(this, Claimtransinfo, true);
                obj.SaveChanges();
                this.TransactionId = Claimtransinfo.TransactionId;
                this.ClaimId = Claimtransinfo.ClaimId;
                return this;
            }
            else
            {
                Claimtransinfo = new CLM_Transactions();
                DataMapper.Map(this, Claimtransinfo, true);
                obj.CLM_Transactions.AddObject(Claimtransinfo);
                obj.SaveChanges();
                this.ClaimId = Claimtransinfo.ClaimId;
                this.TransactionId = Claimtransinfo.TransactionId;
                return this;
            }
        }
        #endregion
    }
}
