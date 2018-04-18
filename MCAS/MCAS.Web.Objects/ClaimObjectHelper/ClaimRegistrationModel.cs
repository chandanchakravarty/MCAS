using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimRegistrationModel : BaseModel
    {
        #region Properties
        public string ClaimRefNo { get; set; }
        public DateTime DateNotification { get; set; }
        public DateTime DateAccident { get; set; }
        public DateTime DateDiary { get; set; }
        public string DiaryDesc { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int trx_status { get; set; }
        public int isApprove { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ApproveBy { get; set; }
        public string CashCall_Remarks { get; set; }
        public string isCashCall { get; set; }
        public double RES_ReserveLoc { get; set; }
        public double RES_ReserveFor { get; set; }
        public string EXP_CurrencyCode { get; set; }
        public decimal EXP_ExchangeRate { get; set; }
        public decimal EXP_ReserveLoc { get; set; }
        public decimal EXP_ReserveFor { get; set; }
        public decimal PAY_TotalLoc { get; set; }
        public string POL_TranRefNo { get; set; }
        public string POL_PolRefNo { get; set; }
        public int POL_EntNo { get; set; }
        public int POL_RenNo { get; set; }
        public DateTime POL_DateCommence { get; set; }
        public DateTime POL_DateExpiry { get; set; }
        public string POL_BranchCode { get; set; }
        public string POL_CurrencyCode { get; set; }
        public string POL_InsuredCode { get; set; }
        public string POL_InsuredName { get; set; }
        public string POL_ProducerCode { get; set; }
        public string POL_ProducerName { get; set; }
        public string POL_PolicyStatus { get; set; }
        public string POL_PremiumStatus { get; set; }
        public string POL_ProductCode { get; set; }
        public string POL_ProductName { get; set; }
        public string POL_RiskID { get; set; }
        public string POL_RiskNo { get; set; }
        public string POL_ClassCode { get; set; }
        public string POL_SubClassCode { get; set; }
        public string POL_SubClassName { get; set; }
        public decimal POL_SharePer { get; set; }
        public decimal RES_FacRIPer { get; set; }
        public decimal RES_FacRIXol { get; set; }
        public decimal RES_GrossPer { get; set; }
        public decimal RES_XOLPer { get; set; }
        public string ReportedBy { get; set; }
        public string TempClaimRefNo { get; set; }
        public string Accepted { get; set; }
        public string Comments { get; set; }
        public string TimeAccident { get; set; }
        public string Reason { get; set; }
        public string POL_CI_Leader { get; set; }
        public decimal POL_CI_LeaderShare { get; set; }
        public string POL_ProductType { get; set; }
        public string Recovery { get; set; }
        public string CatastropheCode { get; set; }
        public string CatastropheDesc { get; set; }
        public string ContactNo { get; set; }
        public DateTime RecDateDiary { get; set; }
        public string RecDiaryDesc { get; set; }
        public string RecCurrency { get; set; }
        public decimal RecAmount { get; set; }
        public string CountryCode { get; set; }
        public int IsRecoveryApproved { get; set; }

        public List<CatastropheListIems> Catastrophelist { get; set; }
        public List<LookUpListItems> Recoverylist { get; set; }
        public List<CurrencyMasterModel> Currencylist { get; set; }
        #endregion

        #region Static Method
        public static List<ClaimRegistrationModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimRegistrationModel>();
            var list = (from l in _db.Claim_Registration select l);
            if (list.Any())
            {
                foreach (var data in list)
                {
                    
                    item.Add(new ClaimRegistrationModel() { POL_PolRefNo = data.POL_PolRefNo, POL_ProductCode = data.POL_ProductCode,POL_InsuredName= data.POL_InsuredName,CountryCode= data.CountryCode, POL_DateCommence = Convert.ToDateTime(data.POL_DateCommence), POL_DateExpiry = Convert.ToDateTime(data.POL_DateExpiry) });
                }
            }
            return item;
        }
        #endregion

    }
}
