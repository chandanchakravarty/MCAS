using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.ClaimObjectHelper;

namespace MCAS.Web.Objects.MastersHelper
{
    public class PaymentEditortModel : BaseModel
    {
        private DateTime? _paymentDueDate = null;
        private DateTime? _approveDate = null;
        #region properties
        public int? TranId { get; set; }

        public int ClaimId { get; set; }

        [Required(ErrorMessage = "Claimant is required.")]
        public int Claimant { get; set; }

        [Required(ErrorMessage = "Payee Type is required.")]
        public string PayeeType { get; set; }

        [Required(ErrorMessage = "Payment Type is required.")]
        public string PaymentType { get; set; }

        [Required(ErrorMessage = "Payee is required.")]
        public string Payee { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDueDate
        {
            get { return _paymentDueDate; }
            set { _paymentDueDate = value; }
        }

        public Decimal Prop1 { get; set; }

        public string Hclaimant { get; set; }

        [Required(ErrorMessage = "Payee Address is required.")]
        public string PayeeAdd { get; set; }

        [Required(ErrorMessage = "Payment Currency is required.")]
        public string PaymentCurr { get; set; }

        [Required(ErrorMessage = "Original Currency is required.")]
        public string PayableOrgID { get; set; }

        [Required(ErrorMessage = "Payment Original Amount is required.")]
        [NotEqual("Prop1", ErrorMessage = "Payment Original Amount is required.")]
        public Decimal PayableOrgAmt { get; set; }

        [Required(ErrorMessage = "Local Currency is required.")]
        public string PayableLocalID { get; set; }

        public Decimal PreReserveOrgAmt { get; set; }
        public Decimal PreReserveLocalAmt { get; set; }

        public Decimal FinalReserveOrgAmt { get; set; }
        public Decimal FinalReserveLocalAmt { get; set; }

        [Required(ErrorMessage = "Payment Local Amount is required.")]
        [NotEqual("Prop1", ErrorMessage = "Payment Local Amount is required.")]
        public Decimal PayableLocalAmt { get; set; }

        public List<ReserveCurr> Payment_Type_List { get; set; }

        public List<ThirdPartyInfoModel> Claimant_List { get; set; }

        public List<PayeeType> PayeeType_List { get; set; }

        public List<PaymentType> PaymentType_List { get; set; }

        public string isApprove { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApproveDate
        {
            get { return _approveDate; }
            set { _approveDate = value; }
        }

        public string ApproveBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public override string screenId
        {
            get
            {
                return "139";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "139";
            }

        }

        #endregion

        #region staticmethod
        public PaymentEditortModel Update(int id, int cno)
        {
            MCASEntities obj = new MCASEntities();
            var PReserveOrgAmt = (from l in obj.CLM_ClaimReserve where l.ClaimID == id && l.ClaimantID == cno orderby l.CreatedDate descending select l.PreReserveOrgAmt).FirstOrDefault();
            var FReserveOrgAmt = (from l in obj.CLM_ClaimReserve where l.ClaimID == id && l.ClaimantID == cno orderby l.CreatedDate descending select l.FinalReserveOrgAmt).FirstOrDefault();
            Decimal val = Convert.ToDecimal(PReserveOrgAmt);
            Decimal val1 = Convert.ToDecimal(FReserveOrgAmt);
            var model = new PaymentEditortModel();
            string CreatedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
            var res = obj.Proc_ClaimPaymentSave(id, cno, this.PayeeType, this.PaymentType, this.PaymentDueDate, this.Payee, this.PayeeAdd, this.PaymentCurr, this.PayableOrgID, this.PayableLocalID, Convert.ToDecimal(this.PayableOrgAmt), Convert.ToDecimal(this.PayableLocalAmt), val, val1, 1, CreatedBy, DateTime.Now);
            obj.SaveChanges();
            return this;
        }
        public static List<ThirdPartyInfoModel> Fetch(int id, string clname = "False")
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ThirdPartyInfoModel>();
            var userList = obj.Proc_GetPaymentList(id).ToList();
            item.Add(new ThirdPartyInfoModel() { TPartyId = null, CompanyName = "[Select..]" });

            if (userList.Any())
            {

                foreach (var data in userList)
                {
                    if (data.ClaimantID == 0)
                    {
                        item.Add(new ThirdPartyInfoModel() { TPartyId = 0, CompanyName = "OD" });
                    }
                    else
                    {
                        item.Add(new ThirdPartyInfoModel() { TPartyId = data.ClaimantID, CompanyName = data.CompanyName });
                    }
                }
            }
            else if (clname == "True")
            {
                var od = (from x in obj.CLM_Claims.Where(x => x.ClaimID == id) select x).ToList();
                if (od.Any())
                {
                    item.Add(new ThirdPartyInfoModel() { TPartyId = 0, CompanyName = "OD" });
                }
            }
            return item;
        }
        public static List<PayeeType> Fetch_PayeeType()
        {
            var item = new List<PayeeType>();
            item.Add(new PayeeType() { PayeeType_Id = "", PayeeType_Name = "[Select..]" });
            item.Add(new PayeeType() { PayeeType_Id = "Workshop", PayeeType_Name = "Workshop" });
            item.Add(new PayeeType() { PayeeType_Id = "Adjuster", PayeeType_Name = "Adjuster" });
            item.Add(new PayeeType() { PayeeType_Id = "Lawyer", PayeeType_Name = "Lawyer" });
            item.Add(new PayeeType() { PayeeType_Id = "Claimant", PayeeType_Name = "Claimant" });
            return item;
        }
        public static List<PaymentType> Fetch_PaymentType()
        {
            var item = new List<PaymentType>();
            item.Add(new PaymentType() { PaymentType_Id = "", PaymentType_Name = "[Select..]" });
            item.Add(new PaymentType() { PaymentType_Id = "Expense", PaymentType_Name = "Expense" });
            item.Add(new PaymentType() { PaymentType_Id = "Reserve", PaymentType_Name = "Reserve" });
            return item;
        }

        #endregion
    }
}
