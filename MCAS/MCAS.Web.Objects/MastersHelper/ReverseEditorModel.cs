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
using System.ComponentModel;

namespace MCAS.Web.Objects.MastersHelper
{
    public class ReverseEditorModel : BaseModel
    {
        #region properties

        private DateTime? _approveDate = null;

        public int? ReserveId { get; set; }

        public int? ClaimID { get; set; }

        [Required(ErrorMessage = "Claimant is required.")]
        public int? ClaimantID { get; set; }

        public int ReserveType { get; set; }

        public int MovementType { get; set; }

        [Required(ErrorMessage = "Previous Original Reserve is required.")]
        public string PreResOrgCurrCode { get; set; }

        [Required(ErrorMessage = "Previous Original Reserve is required.")]
        public Decimal? PreReserveOrgAmt { get; set; }

        [Required(ErrorMessage = "ROF is required.")]
        public Decimal? PreExRateOrgCurr { get; set; }

        [Required(ErrorMessage = "Previous Local Reserve is required.")]
        public string PreResLocalCurrCode { get; set; }

        [Required(ErrorMessage = "Previous Local Reserve is required.")]
        public Decimal? PreReserveLocalAmt { get; set; }

        [Required(ErrorMessage = "Ex Rate To Local Currency is required.")]
        public Decimal? PreExRateLocalCurr { get; set; }

        [Required(ErrorMessage = "Current Original Reserve is required.")]
        public string FinalResOrgCurrCode { get; set; }

        [Required(ErrorMessage = "Current Original Reserve is required.")]
        public Decimal FinalReserveOrgAmt { get; set; }

        public Decimal? FinalExRateOrgCurr { get; set; }

        [Required(ErrorMessage = "Current Local Reserve is required.")]
        public string FinalResLocalCurrCode { get; set; }

        [Required(ErrorMessage = "Current Local Reserve is required.")]
        public Decimal? FinalReserveLocalAmt { get; set; }

        public Decimal? FinalExRateLocalCurr { get; set; }

        [Required(ErrorMessage = "Reserve Movement Original Reserve is required.")]
        public string MoveResOrgCurrCode { get; set; }

        [Required(ErrorMessage = "Reserve Movement Original Reserve is required.")]
        [NotEqual("Prop1", ErrorMessage = "Reserve Movement Original Reserve is required.")]
        [DisplayName(" Movement Original Reserve")]
        public Decimal MoveReserveOrgAmt { get; set; }

        public Decimal? MoveExRateOrgCurr { get; set; }

        [Required(ErrorMessage = "Reserve Movement Local Reserve is required.")]
        public string MoveResLocalCurrCode { get; set; }

        [Required(ErrorMessage = "Reserve Movement Local Reserve is required.")]
        public Decimal? MoveReserveLocalAmt { get; set; }

        public Decimal? MoveExRateLocalCurr { get; set; }

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

        
        public string ClaimantType { get; set; }

        public Decimal Prop1 { get; set; }

        public string Hclaimant { get; set; }

        public List<ThirdPartyInfoModel> Claimant_List { get; set; }
        public List<ReserveCurr> Payment_Type_List { get; set; }

        public override string screenId
        {
            get
            {
                return base.screenId;
            }
            set
            {
                base.screenId = "138";
            }
        }

        public override string listscreenId
        {
            get
            {
                return base.listscreenId;
            }
            set
            {
                base.listscreenId = "CLAIM";
            }
        }
        #endregion

        #region staticmethod
        public static string Between(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }


        public ReverseEditorModel Update(int id, int cno)
        {
            MCASEntities obj = new MCASEntities();
            var model = new ReverseEditorModel();
            string CreatedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            string ModifiedBy = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
            var res = obj.Proc_ClaimReserveSave(id, cno, 2, this.PreReserveLocalAmt, this.PreExRateLocalCurr, Convert.ToDecimal(this.PreReserveOrgAmt), this.MoveResOrgCurrCode, this.PreExRateOrgCurr, this.FinalReserveLocalAmt, this.FinalResLocalCurrCode, this.FinalReserveOrgAmt, this.FinalResOrgCurrCode, this.MoveReserveLocalAmt, this.MoveResLocalCurrCode, this.MoveReserveOrgAmt, this.MoveResOrgCurrCode, DateTime.Now, CreatedBy, 1);
            obj.SaveChanges();
            return this;
        }

        public ReverseEditorModel Update(ReverseEditorModel model)
        {
            MCASEntities obj = new MCASEntities();
            CLM_ClaimReserve reserveinfo = new CLM_ClaimReserve();
            DataMapper.Map(this, reserveinfo, true);
            obj.CLM_ClaimReserve.AddObject(reserveinfo);
            obj.SaveChanges();
            return this;
        }

        public static List<ThirdPartyInfoModel> Fetch(int id, string clname = "False")
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ThirdPartyInfoModel>();
            var userList = obj.Proc_GetReserveList(id).ToList();
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
        #endregion
    }
}
