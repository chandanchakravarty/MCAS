using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web.Mvc;
using MCAS.Web.Objects.ClaimObjectHelper;

namespace MCAS.Web.Objects.MastersHelper
{
    public class CLMClaimRecoveryModel : BaseModel
    {
        #region Properties
        private DateTime? _createdDate = null;
        private DateTime? _modifiedDate = null;
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string RecoveryReason { get; set; }
        public string CurrCode { get; set; }
        public Decimal? ExchangeRate { get; set; }
        public Decimal? LocalCurrAmt { get; set; }
        public int Status { get; set; }
        public int ClaimID { get; set; }
        public string PaymentDetails { get; set; }
        public string IsActive { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public List<ReserveCurr> CurrCode_List { get; set; }
        public List<StatusList> Status_List { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        #endregion
        #region "Public Shared Methods"
        public static List<StatusList> Status_List_Fetch()
        {
            var item = new List<StatusList>();
            item.Add(new StatusList() { StatusList_Id = 0, StatusList_Name = "Pending" });
            item.Add(new StatusList() { StatusList_Id = 1, StatusList_Name = "Received" });
            item.Add(new StatusList() { StatusList_Id = 2, StatusList_Name = "Discontinue" });
            return item;
        }

        //public static List<CLMClaimRecoveryModel> Fetchall(int id)
        //{
        //    MCASEntities obj = new MCASEntities();
        //    var item = new List<CLMClaimRecoveryModel>();
        //    var PaymentList = obj.Proc_GetCLMRecoveryAll(id).ToList();
        //    if (PaymentList.Any())
        //    {
        //        foreach (var data in PaymentList)
        //        {
        //            item.Add(new CLMClaimRecoveryModel() { CreatedDate = data.CreatedDate, UserName = data.UserName, Address = data.Address, RecoveryReason = data.RecoveryReason, CurrCode = data.CurrCode, ExchangeRate = data.ExchangeRate, LocalCurrAmt = data.LocalCurrAmt, PaymentDetails = data.PaymentDetails, });
        //        }
        //    }
        //    return item;
        //}

        #endregion
    }

    public class StatusList
    {
        public int StatusList_Id { get; set; }
        public String StatusList_Name { get; set; }
    }


}
