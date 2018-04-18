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
    public class ReverseInfo : BaseModel
    {
        #region Properties

        public string Date { get; set; }

        public string Type { get; set; }

        public string ClaimantNo { get; set; }

        public Decimal PreReserveOrgAmt { get; set; }
        public Decimal PreReserveLocalAmt { get; set; }

        public Decimal FinalReserveOrgAmt { get; set; }
        public Decimal FinalReserveLocalAmt { get; set; }

        public Decimal MoveReserveOrgAmt { get; set; }
        public Decimal MoveReserveLocalAmt { get; set; }

        #endregion

        #region "Public Shared Methods"
        public static List<ReverseInfo> Fetch(int id)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ReverseInfo>();
            var ReserveList = obj.Proc_GetReserveList(id).ToList();

            if (ReserveList.Any())
            {
                foreach (var data in ReserveList)
                {
                    var rtype = data.ReserveType == 1 ? "I" : "M";
                    var ClaimantN = data.CompanyName == null ? "OD" : data.CompanyName;
                    item.Add(new ReverseInfo() { Date = Convert.ToDateTime(data.CreatedDate).ToString("dd/MM/yyyy"), Type = Convert.ToString(rtype), ClaimantNo = ClaimantN, PreReserveOrgAmt = data.PreReserveOrgAmt, FinalReserveOrgAmt = data.FinalReserveOrgAmt, MoveReserveOrgAmt = data.MoveReserveOrgAmt });

                }

            }
            return item;
        }


        public static List<ReverseInfo> Fetchall(int id, string Tab = "Res", string ClaimantId = "a")
        {
            string res = string.Empty;
            MCASEntities obj = new MCASEntities();
            var item = new List<ReverseInfo>();
            var ReserveList = obj.Proc_GetReserveListAll(id).ToList();
            var model = new ReverseInfo();
            string CreatedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
            if (ReserveList.Any())
            {
                foreach (var data in ReserveList)
                {
                    var NCreatedBy = data.CreatedBy == null ? CreatedBy1 : data.CreatedBy;
                    if (Tab == "CR")
                    {
                        if (data.CompanyName == null)
                        {
                            var rtype = data.ReserveType == 1 ? "I" : "M";

                            item.Add(new ReverseInfo() { Date = Convert.ToDateTime(data.CreatedDate).ToString(), Type = Convert.ToString(rtype), ClaimantNo = data.CompanyName == null ? "OD" : data.CompanyName, PreReserveOrgAmt = data.PreReserveOrgAmt, PreReserveLocalAmt = Convert.ToDecimal(data.PreReserveLocalAmt), FinalReserveOrgAmt = data.FinalReserveOrgAmt, FinalReserveLocalAmt = Convert.ToDecimal(data.FinalReserveLocalAmt), MoveReserveOrgAmt = data.MoveReserveOrgAmt, MoveReserveLocalAmt = Convert.ToDecimal(data.MoveReserveLocalAmt), CreatedBy = NCreatedBy });
                        }
                    }
                    else if (Tab == "TP")
                    {
                        var ClID = Convert.ToInt32(ClaimantId);
                        var name = obj.CLM_ThirdParty.Where(x => x.TPartyId == ClID).Select(x => x.CompanyName).FirstOrDefault();
                        if (data.CompanyName == name)
                        {
                            var rtype = data.ReserveType == 1 ? "I" : "M";
                            item.Add(new ReverseInfo() { Date = Convert.ToDateTime(data.CreatedDate).ToString(), Type = Convert.ToString(rtype), ClaimantNo = data.CompanyName == null ? "OD" : data.CompanyName, PreReserveOrgAmt = data.PreReserveOrgAmt, PreReserveLocalAmt = Convert.ToDecimal(data.PreReserveLocalAmt), FinalReserveOrgAmt = data.FinalReserveOrgAmt, FinalReserveLocalAmt = Convert.ToDecimal(data.FinalReserveLocalAmt), MoveReserveOrgAmt = data.MoveReserveOrgAmt, MoveReserveLocalAmt = Convert.ToDecimal(data.MoveReserveLocalAmt), CreatedBy = NCreatedBy });
                        }
                    }
                    else
                    {
                        var rtype = data.ReserveType == 1 ? "I" : "M";
                        item.Add(new ReverseInfo() { Date = Convert.ToDateTime(data.CreatedDate).ToString(), Type = Convert.ToString(rtype), ClaimantNo = data.CompanyName == null ? "OD" : data.CompanyName, PreReserveOrgAmt = data.PreReserveOrgAmt, PreReserveLocalAmt = Convert.ToDecimal(data.PreReserveLocalAmt), FinalReserveOrgAmt = data.FinalReserveOrgAmt, FinalReserveLocalAmt = Convert.ToDecimal(data.FinalReserveLocalAmt), MoveReserveOrgAmt = data.MoveReserveOrgAmt, MoveReserveLocalAmt = Convert.ToDecimal(data.MoveReserveLocalAmt), CreatedBy = NCreatedBy });
                    }
                }
            }

            return item;
        }

        public static string Fetchname(int id, string Tab = "Res", string ClaimantId = "a")
        {
            string res = string.Empty;
            MCASEntities obj = new MCASEntities();
            var item = new List<ReverseInfo>();
            var ReserveList = obj.Proc_GetReserveListAll(id).ToList();
            if (ReserveList.Any())
            {
                foreach (var data in ReserveList)
                {
                    if (Tab == "CR")
                    {

                        if (data.CompanyName == null)
                        {
                            var ClaimantN = data.CompanyName == null ? "OD" : data.CompanyName;
                            res = res + ClaimantN + "*";
                        }
                    }
                    else if (Tab == "TP")
                    {
                        var ClID = Convert.ToInt32(ClaimantId);
                        var name = obj.CLM_ThirdParty.Where(x => x.TPartyId == ClID).Select(x => x.CompanyName).FirstOrDefault();
                        if (data.CompanyName == name)
                        {
                            var ClaimantN = data.CompanyName == null ? "OD" : data.CompanyName;
                            res = res + ClaimantN + "*";
                        }
                    }
                    else
                    {
                        var ClaimantN = data.CompanyName == null ? "OD" : data.CompanyName;
                        res = res + ClaimantN + "*";
                    }
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
    public class ClaimReserveHistory
    {
        
        public int ReserveId { get; set; }
        public int ClaimID { get; set; }
        [Display(Name = "Reserve Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Reserve Type")]
        public string ReserveType { get; set; }
        [Display(Name = "Reserve Claimaint Name")]
        public string ClaimantID { get; set; }
        [Display(Name = "Reserve Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? FinalReserveOrgAmt { get; set; }
        [Display(Name = "Expense Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? ExpenseDate { get; set; }
        [Display(Name = "Expense Type")]
        public string ExpType { get; set; }
        [Display(Name = "Expense Claimaint Name")]
        public string E_Claimaint { get; set; }
        [Display(Name = "Expense Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? ExpenseAmount { get; set; }
        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? PayDate { get; set; }
        [Display(Name = "Payment Type")]
        public string PayType { get; set; }
        [Display(Name = "Payment Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? PayAmount { get; set; }
        [Display(Name = "Payment Claimaint Name")]
        public string PayCLID { get; set; }
    }
}