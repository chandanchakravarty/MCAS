using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MCAS.Entity;
using System.Web;
using MCAS.Web.Objects.Resources.Common;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimRecoveryModel : BaseModel
    {
        MCASEntities _db = new MCASEntities();
        #region constructor
        private List<ClaimRecoveryModelCollection> _claimRecovery;
        public ClaimRecoveryModel()
        {
            this._claimRecovery = FetchRecoveryList(0);
        }
        public ClaimRecoveryModel(int AccidentId,int ClmType)
        {
            this._claimRecovery = FetchRecoveryList(AccidentId);
        }
        #endregion

        #region Properties

        #region PrivateProperties
        private DateTime? _createdDate = null;
        public int _prop1 { get; set; }
        public decimal _prop2 { get; set; }
        #endregion

        #region publicProperties
        public List<ClaimRecoveryModelCollection> ClaimRecoveryModelCollection
        {
            get { return _claimRecovery; }
        }

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

        public decimal Prop2
        {
            get
            {
                return 0.00m;
            }
            set
            {
                this._prop2 = 0.00m;
            }
        }
        public int RecoveryId { get; set; }
        [NotEqual("Prop1", ErrorMessage = "Claim Type is required.")]
        [DisplayName("Claim Type")]
        public int ClaimType { get; set; }
        public int AccidentClaimId { get; set; }
        public int ClaimId { get; set; }
        public int PolicyId { get; set; }
        public int ClaimentId { get; set; }
        public int MandateId { get; set; }
        [NotEqual("Prop1", ErrorMessage = "Recover From is required.")]
        [DisplayName("Recover From")]
        public string RecoverFrom { get; set; }
        [NotEqual("Prop1", ErrorMessage = "Recover From is required.")]
        [DisplayName("Recover From")]
        public string RecoverFromBI { get; set; }
        [DisplayName("Address1")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string RecoveryReason { get; set; }
        public string RecoveryReasonBI { get; set; }
        public string ResultMessage { get; set; }
        //public decimal? CostofRepairs_R { get; set; }
        //public decimal? LossofUse_R { get; set; }
        //public decimal? OtherExpences_R { get; set; }
        //public decimal? ReportServeyFee_R { get; set; }
        //public decimal? ReportReserveyFee_R { get; set; }
        //public decimal? ReportLTA_GIA_PolicyFee_R { get; set; }
        //public decimal? TPLawyerCost_R { get; set; }
        //public decimal? TPLawyerDisbursment_R { get; set; }
        //Commented for ITrack - 223
        //[Required(ErrorMessage = "Our Lawyer’s Cost & Disbursements is required.")]
        //[NotEqual("Prop2", ErrorMessage = "Our Lawyer’s Cost & Disbursements is required.")]
        public decimal? LeagalLawyerCost_R { get; set; }
        //public decimal? legalLawyerDisbursement_R { get; set; }
        public decimal? TotalAmt_R { get; set; }

        public decimal? CostofRepairs_S { get; set; }
        public decimal? LossofUse_S { get; set; }
        public decimal? OtherExpences_S { get; set; }
        public decimal? ReportServeyFee_S { get; set; }
        public decimal? ReportReserveyFee_S { get; set; }
        public decimal? ReportLTA_GIA_PolicyFee_S { get; set; }
        public decimal? TPLawyerCost_S { get; set; }
        public decimal? TPLawyerDisbursment_S { get; set; }
        public decimal? LeagalLawyerCost_S { get; set; }
        public decimal? legalLawyerDisbursement_S { get; set; }
        public decimal? TotalAmt_S { get; set; }
        public string HshowHideSaveButton { get; set; }
        public string HDeductibleAmt { get; set; }
        public string HTPBIPayoutAmt { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        //public string CreatedBy { get; set; }
        //public string Modifiedby { get; set; }
        public int? PaymentId { get; set; }
        public string ClaimantName { get; set; }
        public decimal? TPBIPayout { get; set; }
        public decimal? Deductible { get; set; }
        [Required(ErrorMessage = "Amount Recovered is required.")]
        [NotEqual("Prop2", ErrorMessage = "Amount Recovered is required.")]
        [Display(Name = "Amount Recovered")]
        public decimal? RecoverAmt { get; set; }
        [Required(ErrorMessage = "Net Amount Recovered is required.")]
        [NotEqual("Prop2", ErrorMessage = "Net Amount Recovered is required.")]
        [Display(Name = "Net Amount Recovered")]
        public decimal? NetAmtRecovered { get; set; }

        public string Hselect { get; set; }
        public string Hdivdis { get; set; }
        public string Himgsrc { get; set; }
        public string Hshowgrid { get; set; }
        public string HGridImageSign { get; set; }
        public string AddNewPartyText { get; set; }
        public string h3header { get; set; }
        public decimal? HApproveMandateAmt { get; set; }
        public string  Hchkcum { get; set; }

        public List<ClaimantStatus> RecoverFromListBI { get; set; }
        public List<ClaimantStatus> RecoverFromListOD { get; set; }
        #endregion

        #region  OverrideProperties
        public override string screenId
        {
            get
            {
                return "294";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "294";
            }

        }
        #endregion  

        #endregion

        #region methods
        public static List<ClaimRecoveryModelCollection> FetchRecoveryList(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                
                var items = new List<ClaimRecoveryModelCollection>();
                if (AccidentClaimId != 0)
                {
                    var Ownlist = db.Proc_GetClaimRecoveryList(AccidentClaimId.ToString());

                    items = (from data in Ownlist
                             select new ClaimRecoveryModelCollection()
                             {
                                 RecoveryId = (int)data.RecoveryId,
                                 OrgRecordNumber = data.RecordNumber,
                                 ClaimID = data.ClaimID,
                                 RecordNumber = data.ClaimRecordNo,
                                 ClaimantName = data.ClaimantName,
                                 AccidentClaimId = data.AccidentClaimId,
                                 PolicyId = data.PolicyId,
                                 ClaimType = (int)data.ClaimTypeId,
                                 ClaimTypeCode = data.ClaimTypeCode,
                                 ClaimTypeDesc = data.ClaimTypeId.ToString() == "1" ? Common._1 : data.ClaimTypeId.ToString() == "2" ? Common._2 : data.ClaimTypeId.ToString() == "3" ? Common._3 : "",
                                 PaymentId = data.PaymentId,
                                 MandateRecordNo = data.MandateRecordNo,
                                 PaymentRecordNo = data.PaymentRecordNo,
                                 MandateId = data.MandateId,
                                 TotalPaymentDue = data.TotalPaymentDue,
                                 ApprovedDate = data.ApprovedDate,
                                 TotalAmt_R = data.TotalAmt_R,
                                 RecoverAmt = data.RecoverAmt,
                                 NetAmtRecovered = data.NetAmtRecovered,
                                 status = data.RecoveryId == null || data.RecoveryId == 0 ? "" : "Processed"
                             }
                            ).ToList();
                }
                var result = new List<ClaimRecoveryModelCollection>();
                foreach(var data in items)
                {
                    var PaymentRecordNo = data.PaymentRecordNo == null ? "" : data.PaymentRecordNo + " - $" + data.TotalPaymentDue;
                    result.Add(new ClaimRecoveryModelCollection() {
                        RecoveryId = data.RecoveryId,
                        OrgRecordNumber = data.OrgRecordNumber,
                        ClaimID = data.ClaimID,
                        RecordNumber = data.RecordNumber,
                        ClaimantName = data.ClaimantName,
                        AccidentClaimId = data.AccidentClaimId,
                        PolicyId = data.PolicyId,
                        ClaimType = data.ClaimType,
                        ClaimTypeCode = data.ClaimTypeCode,
                        ClaimTypeDesc = data.ClaimTypeDesc,
                        PaymentId = data.PaymentId,
                        MandateRecordNo = data.MandateRecordNo,
                        PaymentRecordNo = PaymentRecordNo,
                        MandateId = data.MandateId,
                        TotalPaymentDue = data.TotalPaymentDue,
                        ApprovedDate = data.ApprovedDate,
                        TotalAmt_R = data.TotalAmt_R,
                        RecoverAmt = data.RecoverAmt,
                        NetAmtRecovered = data.NetAmtRecovered,
                        status = data.RecoveryId == null || data.RecoveryId == 0 ? "" : "Processed"
                    
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
        }


        public ClaimRecoveryModel FetchRecovery(string RecoveryId, ClaimRecoveryModel model)
        {
            MCASEntities obj = new MCASEntities();
            int rId = Convert.ToInt32(RecoveryId);
            var clmRecovery = (from l in obj.CLM_ClaimRecovery where l.RecoveryId == rId select l).FirstOrDefault();
            if (clmRecovery != null)
            {
                model.CreatedBy = clmRecovery.CreatedBy;
                model.CreatedDate = clmRecovery.CreatedDate;
                if (clmRecovery.Modifiedby == null)
                {
                    model.GetType().GetProperty("ModifiedBy").SetValue(model, null, null);
                }
                model.ModifiedOn = clmRecovery.ModifiedDate != null ? clmRecovery.ModifiedDate : null;
                model.ModifiedBy = clmRecovery.Modifiedby;
                if (model.ClaimType == 3)
                {
                    model.RecoverFromBI = clmRecovery.RecoverFrom;
                    model.TPBIPayout = clmRecovery.TPBIPayout;
                    model.Deductible = clmRecovery.Deductible;
                    model.RecoverAmt = clmRecovery.RecoverAmt;
                    model.NetAmtRecovered = clmRecovery.NetAmtRecovered;
                    model.RecoveryReasonBI = clmRecovery.RecoveryReason;
                }
                else
                {
                    model.RecoverFrom = clmRecovery.RecoverFrom;
                    model.RecoverAmt = clmRecovery.RecoverAmt;
                    model.LeagalLawyerCost_R = clmRecovery.LeagalLawyerCost_R;
                    model.TotalAmt_R = clmRecovery.TotalAmt_R;
                    model.RecoveryReason = clmRecovery.RecoveryReason;
                }
            }
            else
            {
                if (model.ClaimType == 3)
                {
                    var deductibleAmt = (from deductamt in obj.Proc_GetDeductibleAmt(Convert.ToInt32(model.AccidentClaimId)) orderby deductamt.DeductibleAmt descending select deductamt.DeductibleAmt).FirstOrDefault();

                    var TotalTPBIAmt = obj.Proc_RecoveryTPBIAmount(Convert.ToString(model.AccidentClaimId)).FirstOrDefault();
                    var chksum = (from m in obj.CLM_ClaimRecovery where m.AccidentClaimId == model.AccidentClaimId && m.ClaimId == model.ClaimId && m.ClaimType == model.ClaimType select m).FirstOrDefault();

                    if (TotalTPBIAmt < deductibleAmt || chksum == null)
                    {
                        model.Deductible = deductibleAmt == null ? 0 : deductibleAmt;
                        model.TPBIPayout = TotalTPBIAmt;
                    }
                    else
                    {
                        model.Deductible = 0.00m;
                        model.TPBIPayout = (from payoutamt in obj.CLM_PaymentSummary where payoutamt.AccidentClaimId == model.AccidentClaimId && payoutamt.ClaimType == 3 && payoutamt.PaymentId == model.PaymentId select payoutamt.TotalPaymentDue).FirstOrDefault();
                    }
                    model.HTPBIPayoutAmt = Convert.ToString(model.TPBIPayout);
                    model.HDeductibleAmt = Convert.ToString(model.Deductible);
                }
            }
            model.HApproveMandateAmt = (from ma in obj.CLM_MandateSummary where ma.AccidentClaimId == model.AccidentClaimId && ma.ClaimID == model.ClaimId && ma.ClaimType == model.ClaimType && ma.ApproveRecommedations == "Y" && ma.MandateId == model.MandateId && ma.PaymentId == null select ma.MovementMandateSP).FirstOrDefault();
            model.RecoveryId = rId;
            obj.Dispose();
            return model;   
        }
        #endregion

        #region Public Method
        public ClaimRecoveryModel Save()
        {
            MCASEntities obj = new MCASEntities();
            CLM_ClaimRecovery clmrecov;
            var model = new ClaimRecoveryModel();
            clmrecov = obj.CLM_ClaimRecovery.Where(x => x.RecoveryId == this.RecoveryId).FirstOrDefault();
            if (obj.Connection.State == System.Data.ConnectionState.Closed)
                obj.Connection.Open();
            using (System.Data.Common.DbTransaction transaction = obj.Connection.BeginTransaction())
            {
                try
                {
                    if (clmrecov == null)
                    {
                        #region InsertIntorecovery
                        clmrecov = new CLM_ClaimRecovery();
                        DataMapper.Map(this, clmrecov, true);
                        clmrecov.CreatedDate = DateTime.Now;
                        clmrecov.CreatedBy = this.CreatedBy;
                        clmrecov.RecoverFrom = this.ClaimType == 3 ? this.RecoverFromBI : this.RecoverFrom;
                        clmrecov.RecoveryReason = this.ClaimType == 3 ? this.RecoveryReasonBI : this.RecoveryReason;
                        obj.CLM_ClaimRecovery.AddObject(clmrecov);
                        this.ResultMessage = "Record saved successfully.";
                        #endregion
                    }
                    else
                    {
                        #region UpdateRecovery
                        string[] ignoreList = new string[] { "Createddate", "CreatedBy"};
                        var crDate = clmrecov.CreatedDate;
                        DataMapper.Map(this, clmrecov, true, ignoreList);
                        clmrecov.Modifiedby = this.CreatedBy;
                        clmrecov.ModifiedDate = DateTime.Now;
                        clmrecov.CreatedDate = crDate;
                        clmrecov.RecoverFrom = this.ClaimType == 3 ? this.RecoverFromBI : this.RecoverFrom;
                        clmrecov.RecoveryReason = this.ClaimType == 3 ? this.RecoveryReasonBI : this.RecoveryReason;
                        this.ResultMessage = "Record updated successfully.";
                        #endregion
                    }

                    #region Save
                    obj.SaveChanges();
                    transaction.Commit();
                    this.RecoveryId = clmrecov.RecoveryId;
                    this.AccidentClaimId = clmrecov.AccidentClaimId;
                    this.ClaimType = Convert.ToInt32(clmrecov.ClaimType);
                    this._claimRecovery = FetchRecoveryList(clmrecov.AccidentClaimId);
                    this.CreatedBy = clmrecov.CreatedBy;
                    this.CreatedOn = Convert.ToDateTime(clmrecov.CreatedDate);
                    if (clmrecov.ModifiedDate != null && clmrecov.Modifiedby!=null)
                    {
                        this.ModifiedBy = clmrecov.Modifiedby;
                        this.ModifiedOn = clmrecov.ModifiedDate;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    obj.Dispose();
                }
            }
            return this;
        }

        public int FetchClaimType(ClaimRecoveryModel model)
        {
            MCASEntities obj = new MCASEntities();
            int ClaimType = Convert.ToInt32((from Claims in obj.CLM_Claims where Claims.ClaimID == model.ClaimId select Claims.ClaimType).FirstOrDefault());
            obj.Dispose();
            return ClaimType;
        }

        public static string GetRecoveryTopId(int accid, int claimType)
        {
            MCASEntities db = new MCASEntities();
            var GridButtonText = string.Join("~", (from l in db.Proc_GetMaxDeductibleRecoveryList(accid,claimType) select l.PaymentId).ToArray());
            db.Dispose();
            return (GridButtonText == null) ? "" : GridButtonText;
        }
       
        //public ClaimRecoveryModel FetchMandateList(int RecoveryId, int AccidentId, ClaimRecoveryModel model)
        //{
        //    MCASEntities obj = new MCASEntities();
        //    var RecoveryList = (from m in obj.CLM_ClaimRecovery where m.RecoveryId == RecoveryId select m).FirstOrDefault();
        //    if (RecoveryList != null)
        //    {
        //        DataMapper.Map(RecoveryList, model, true);
        //    }
        //    var list = (from l in obj.CLM_MandateSummary where l.AccidentId == model.AccidentClaimId && l.ClaimID == model.ClaimId && l.ApproveRecommedations == "Y" orderby l.Createddate descending select l).FirstOrDefault();

        //    model.CostofRepairs_S = list == null || list.CostofRepairs_S == null ? 0.00m : list.CostofRepairs_S;
        //    model.LossofUse_S = list == null || list.LossofUseUn_S == null ? 0.00m : list.LossofUseUn_S;
        //    model.OtherExpences_S = list == null || list.OtherExpenses_S == null ? 0.00m : list.OtherExpenses_S;
        //    model.ReportServeyFee_S = list == null || list.SurveyFee_S == null ? 0.00m : list.SurveyFee_S;
        //    model.ReportReserveyFee_S = list == null || list.ReSurveyFee_S == null ? 0.00m : list.ReSurveyFee_S;
        //    model.ReportLTA_GIA_PolicyFee_S = list == null || list.LGPolRepFee_S == null ? 0.00m : list.LGPolRepFee_S;
        //    model.TPLawyerCost_S = list == null || list.ParLawCost3rd_S == null ? 0.00m : list.ParLawCost3rd_S;
        //    model.TPLawyerDisbursment_S = list == null || list.ParLawDisbursements3rd_S == null ? 0.00m : list.ParLawDisbursements3rd_S;
        //    model.LeagalLawyerCost_S = list == null || list.OurLawyerCost_S == null ? 0.00m : list.OurLawyerCost_S;
        //    model.legalLawyerDisbursement_S = list == null || list.OurLawDisbursements_S == null ? 0.00m : list.OurLawDisbursements_S;
        //    model.TotalAmt_S = list == null || list.Total_S == null ? 0.00m : list.Total_S;
        //    obj.Dispose();
        //    return model;
        //}

        public static List<ClaimantStatus> GetRecoverFromBIList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimantStatus>();
            var ids = new[] { "0", "2" };
            var list = (from m in _db.MNT_Cedant where ids.Contains(m.InsurerType) select m).ToList();
            item = (from n in list
                    select new ClaimantStatus()
                    {
                        Id = n.CedantId,
                        Text = n.CedantName
                    }
                   ).ToList();
            item.Insert(0, new ClaimantStatus() { Id = 0, Text = "[Select...]" });
            _db.Dispose();
            return item;
        }

        public static List<ClaimantStatus> GetRecoverFromODList(int AccidentClaimId,int ClaimType,int ClaimId)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimantStatus>();
            var list = _db.Proc_GetRecoverFromOD(AccidentClaimId, ClaimType, ClaimId);
            item = (from n in list
                    select new ClaimantStatus()
                    {
                        Id = Convert.ToInt32(n.RecordNumber),
                        Text = n.InsurerName
                    }
                   ).ToList();
            item.Insert(0, new ClaimantStatus() { Id = 0, Text = "[Select...]" });
            _db.Dispose();
            return item;
        }

        #endregion
    }

    public class ClaimRecoveryModelCollection
    {
        #region "Object Properties"
        public Int64? OrgRecordNumber { get; set; }
        public int? ClaimID { get; set; }
        public int? PaymentId { get; set; }
        public int? ClaimType { get; set; }
        public int? RecoveryId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public string ClaimantName { get; set; }
        public string RecordNumber { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public string MandateRecordNo { get; set; }
        public string PaymentRecordNo { get; set; }
        public string status { get; set; }
        public int? MandateId { get; set; }
        public Decimal? Total { get; set; }
        public Decimal? TotalPaymentDue { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public decimal? TotalAmt_R { get; set; }
        public decimal? RecoverAmt { get; set; }
        public decimal? NetAmtRecovered { get; set; }
        public List<string> HeaderListCollection { get; set; }

        #endregion
    }
}
