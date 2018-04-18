using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;


namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimPaymentInfo : BaseModel
    {
        #region Properties
        private DateTime? _createdDate = null;

        public int? TranId { get; set; }

        public string PaymentType { get; set; }

        public string ClaimantNo { get; set; }

        public string Payee { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string PaymentDueDate { get; set; }

        public Decimal PaymentAmount { get; set; }

        public Decimal PayableOrgAmt { get; set; }

        public Decimal PayableLocalAmt { get; set; }

        public Decimal PreReserveOrgAmt { get; set; }

        public Decimal? PreReserveLocalAmt { get; set; }

        public Decimal FinalReserveOrgAmt { get; set; }

        public Decimal? FinalReserveLocalAmt { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        #endregion

        #region "Public Shared Methods"
        public static List<ClaimPaymentInfo> Fetch(int id)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ClaimPaymentInfo>();
            var PaymentList = obj.Proc_GetPaymentList(id).ToList();
            if (PaymentList.Any())
            {
                foreach (var data in PaymentList)
                {
                    var ClaimantN = data.CompanyName == null ? "OD" : data.CompanyName;
                    item.Add(new ClaimPaymentInfo() { CreatedDate = data.CreatedDate, PaymentType = data.PaymentType, ClaimantNo = ClaimantN, Payee = data.Payee, PaymentDueDate = Convert.ToDateTime(data.PaymentDueDate).ToString("dd/MM/yyyy"), PayableOrgAmt = data.PayableOrgAmt, PreReserveOrgAmt = data.PreReserveOrgAmt, FinalReserveOrgAmt = data.FinalReserveOrgAmt });
                }
            }
            return item;
        }

        public static List<ClaimPaymentInfo> Fetchall(int id)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ClaimPaymentInfo>();
            var PaymentList = obj.Proc_GetPaymentListAll(id).ToList();
            var model = new ClaimPaymentInfo();
            string CreatedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
            if (PaymentList.Any())
            {
                foreach (var data in PaymentList)
                {
                    var ClaimantN = data.CompanyName == null ? "OD" : data.CompanyName;
                    var NCreatedBy = data.CreatedBy == null ? CreatedBy1 : data.CreatedBy;
                    item.Add(new ClaimPaymentInfo() { CreatedDate = data.CreatedDate, PaymentType = data.PaymentType, ClaimantNo = ClaimantN, Payee = data.Payee, PaymentDueDate = Convert.ToDateTime(data.PaymentDueDate).ToString("dd/MM/yyyy"), PayableOrgAmt = data.PayableOrgAmt == null ? 0.00m : data.PayableOrgAmt, PayableLocalAmt = data.PayableLocalAmt == null ? 0.00m : data.PayableLocalAmt, PreReserveOrgAmt = data.PreReserveOrgAmt == null ? 0.00m : data.PreReserveOrgAmt, PreReserveLocalAmt = data.PreReserveLocalAmt == null ? 0.00m : data.PreReserveLocalAmt, FinalReserveOrgAmt = data.FinalReserveOrgAmt == null ? 0.00m : data.FinalReserveOrgAmt, FinalReserveLocalAmt = data.FinalReserveLocalAmt == null ? 0.00m : data.FinalReserveLocalAmt, CreatedBy = NCreatedBy });
                }
            }
            return item;
        }



        public static string Fetchname(int id)
        {
            string res = string.Empty;
            MCASEntities obj = new MCASEntities();
            var item = new List<ReverseInfo>();
            var PaymentList = obj.Proc_GetPaymentListAll(id).ToList();
            if (PaymentList.Any())
            {
                foreach (var data in PaymentList)
                {
                    var ClaimantN = data.CompanyName == null ? "OD" : data.CompanyName;
                    res = res + ClaimantN + "*";
                }
            }

            string str = Count(res);
            return res + "," + str;
        }

        private static string Count(string res)
        {
            if (res == "")
            {
                return "";
            }
            else
            {
                var str = res;
                var j = 1;
                String temp = String.Empty;
                for (var i = 1; i < str.Split('*').Length; i++)
                {
                    if (str.Split('*')[i - 1].ToString() != str.Split('*')[i].ToString())
                    {

                        temp = temp + "*" + j;
                        j = 1;
                    }
                    else
                    {
                        j++;
                    }

                }
                StringBuilder sb = new StringBuilder(temp);
                sb[0] = Convert.ToChar(" ");
                return sb.ToString();
            }

        }

        #endregion
    }
}
