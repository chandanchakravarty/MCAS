using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.Resources.Common;
using MCAS.Entity;
using MCAS.Web.Objects.CommonHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimRecoveryCrTxModel : BaseModel
    {
        public int RecoveryId { get; set; }
        public int AccidentClaimId { get; set; }
        public int ClaimId { get; set; }
        public int PolicyId { get; set; }
        public Int64? ReserveId { get; set; }
       //For Taxi
        public Decimal? InvoiceAmt { get; set; }
        public Decimal? CreditDebitNoteAmount { get; set; }
        public string CreditDebitNoteNo { get; set; }
        public Decimal? Insurer { get; set; }
        public Decimal? OwnerHirer { get; set; }
        public Decimal? ThirdParty { get; set; }
        public Decimal? Contractor { get; set; }
        public Decimal? LossofRental { get; set; }
        public Decimal? LossofIncome { get; set; }
        public Decimal? Others1 { get; set; }
        public Decimal? LegalFee { get; set; }
        public Decimal? LTAFee { get; set; }
        public Decimal? SurveyFee { get; set; }
        public Decimal? Others2 { get; set; }
        public Decimal? TPGIAFee { get; set; }
        public Decimal? TotalClaimReceipts { get; set; }
        public Decimal? NetCostofRepairs { get; set; }
        public Decimal? TClmExpensesClaimant { get; set; }
        public Decimal? NetClaimRecovery { get; set; }
        public Decimal? TClmExpensesIncidental { get; set; }
        public Decimal? ProfitSharingForTaxi { get; set; }
        public Decimal? TotalClaimExpenses { get; set; }
        public Decimal? OverUnderClaimRecovery { get; set; }
        public Decimal? ShareNetClmRecovery { get; set; }
        public Decimal? LessCDGEAdminFee { get; set; }
        public Decimal? CDGE1Or3 { get; set; }
        public Decimal? PrftShrTxTlCases { get; set; }
        public Decimal? Taxi2Or3 { get; set; }
        public string GeneralRemarks { get; set; }
        public string SORASerialNo { get; set; }

        public string ResultMessage { get; set; }


       //For Car in recovery
        public string SORASOCRA { get; set; }
        public Decimal Adjustment { get; set; }
        public Decimal? CELossofUse { get; set; }
        public Decimal? MedicalExpenses { get; set; }
        public Decimal Compensation { get; set; }
        public Decimal? CarRental { get; set; }
        public Decimal? CarCourtesy { get; set; }
        public Decimal CDGECost { get; set; }
        public Decimal TPReceipt { get; set; }
        public Decimal NetClientReceipt { get; set; }
        public Decimal NetCDGEClaimExpenses { get; set; }
        public Decimal OverUnderClientRecovery { get; set; }
        public Decimal ClaimableReceipt { get; set; }
        public Decimal NetClientRecovery { get; set; }
        public string Sora_SocraSrlNo { get; set; }
        //[NotEqual("Prop1", ErrorMessage = "Claim Type is required.")]
        //[DisplayName("Claim Type")]
        public int ClaimType { get; set; }

        public string Hshowgrid { get; set; }
        public string Horgtype { get; set; }

        private List<ClaimCRTXRecoveryModelCollection> _claimRecovery;

        public ClaimRecoveryCrTxModel()
        {
            this._claimRecovery = FetchRecoveryList(0);
        }
        public ClaimRecoveryCrTxModel(int AccidentId, int ClmType)
        {
            this._claimRecovery = FetchRecoveryList(AccidentId);
        }

        private ConfirmAmtModel _cofrmamtbd = new ConfirmAmtModel();
        public ConfirmAmtModel Confirmamtbd
        {
            get { return _cofrmamtbd; }
            set { _cofrmamtbd = value; }
        }

        public List<ClaimCRTXRecoveryModelCollection> ClaimRecoveryModelCollection
        {
            get { return _claimRecovery; }
        }

        public ClmClaimQutmPCNTXModel Reserve { get; set; }

        #region static methods

        public static List<ClaimCRTXRecoveryModelCollection> FetchRecoveryList(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            try
            {

                var items = new List<ClaimCRTXRecoveryModelCollection>();
                if (AccidentClaimId != 0)
                {
                    var Ownlist = db.Proc_GetCLM_RecoveryList( Convert.ToString(AccidentClaimId)).ToList();
                    items = (from data in Ownlist
                             select new ClaimCRTXRecoveryModelCollection()
                             {
                                 RecoveryId = (int)data.RecoveryId,
                                 ClaimID = data.ClaimID,
                                 RecordNumber = data.ClaimRecordNo,
                                 AccidentClaimId = data.AccidentClaimId,
                                 PolicyId = data.PolicyId,
                                 ClaimType = (int)data.ClaimTypeId,
                                 ClaimTypeCode = data.ClaimTypeCode,
                                 ClaimTypeDesc = Common._4,
                                 TPVehicleNo = data.TPVehicleNo,
                                 status = data.RecoveryId == null || data.RecoveryId == 0 ? "" : "Processed"
                             }
                            ).ToList();
                }
                var result = new List<ClaimCRTXRecoveryModelCollection>();
                foreach (var data in items)
                {
                    var PaymentRecordNo = data.PaymentRecordNo == null ? "" : data.PaymentRecordNo + " - $" + data.TotalPaymentDue;
                    result.Add(new ClaimCRTXRecoveryModelCollection()
                    {
                        RecoveryId = data.RecoveryId,
                        ClaimID = data.ClaimID,
                        RecordNumber = data.RecordNumber,
                        AccidentClaimId = data.AccidentClaimId,
                        PolicyId = data.PolicyId,
                        ClaimType = data.ClaimType,
                        ClaimTypeCode = data.ClaimTypeCode,
                        ClaimTypeDesc = data.ClaimTypeDesc,
                        TPVehicleNo = data.TPVehicleNo,
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


        public static ClmClaimQutmPCNTXModel FetchReserve(int AccidentClaimId, int ClaimId, int ClaimType, string ViewMode, String RecoveryID)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                var rid = RecoveryID == "" ? 0 : Convert.ToInt32(RecoveryID);
                var reserveid = (from rs in db.CLM_ReserveSummary
                                 where rs.AccidentClaimId == AccidentClaimId && rs.ClaimID == ClaimId && rs.ClaimType == ClaimType
                                 orderby rs.ReserveId descending
                                 select rs.ReserveId).FirstOrDefault();
                //var reserveid = ViewMode != "Select" ? (from rs in db.CLM_ReserveSummary
                //                                        where rs.AccidentClaimId == AccidentClaimId && rs.ClaimID == ClaimId && rs.ClaimType == ClaimType
                //                                        orderby rs.ReserveId descending
                //                                        select rs.ReserveId).FirstOrDefault() : (from rs in db.CLM_ClaimRecovery
                //                                                                                 where rs.MandateId == mid
                //                                                                                 select rs.ReserveId).FirstOrDefault();
                var finReserve = (from crd in db.CLM_ReserveDetails where crd.ReserveId == reserveid select crd).ToList();
                ClmClaimQutmPCNTXModel reserveModel = new ClmClaimQutmPCNTXModel();
                var ReserveDetailList = (from details in db.CLM_ReserveDetails join lk in db.MNT_Lookups on details.CmpCode equals lk.Lookupvalue orderby lk.DisplayOrder ascending where details.ReserveId == reserveid && lk.Category == "TranComponent" select details);
                if (ReserveDetailList.Any())
                {
                    reserveModel.ReserveDetails = (from detail in ReserveDetailList
                                                   select new ClaimReserveDetails()
                                                   {
                                                       ReserveDetailId = detail.ReserveDetailID,
                                                       ReserveId = detail.ReserveId,
                                                       AccidentClaimId = detail.AccidentClaimId,
                                                       CompCode = detail.CmpCode.Trim(),
                                                       CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == detail.CmpCode select l.Description).FirstOrDefault(),
                                                       InitialReserve = detail.InitialReserve,
                                                       PreviousReserve = detail.PreReserve,
                                                       MovementReserve = detail.MovementReserve,
                                                       CurrentReserve = detail.CurrentReserve,
                                                       //PaymentId = detail.PaymentId
                                                   }
                            ).ToList();

                    var remainlist = (from lk in db.MNT_Lookups where lk.IsActive == "Y" && lk.Category == "TranComponent" select lk).Where(p => !(from details in db.CLM_ReserveDetails where details.ReserveId == reserveid select details).Any(p2 => p2.CmpCode == p.Lookupvalue)).OrderBy(x => x.DisplayOrder).ToList();

                    if (remainlist.Any())
                    {
                        var list = reserveModel.ReserveDetails;
                        foreach (var remain in remainlist)
                        {
                            var Position = (from lk in db.MNT_Lookups where lk.IsActive == "Y" && lk.Category == "TranComponent" orderby lk.DisplayOrder select lk).ToList().FindIndex(c => c.DisplayOrder == remain.DisplayOrder);
                            list.Insert(Convert.ToInt32(Position), new ClaimReserveDetails()
                            {
                                ReserveDetailId = (from ln in reserveModel.ReserveDetails select ln.ReserveDetailId).FirstOrDefault(),
                                ReserveId = (from ln in reserveModel.ReserveDetails select ln.ReserveId).FirstOrDefault(),
                                AccidentClaimId = (from ln in reserveModel.ReserveDetails select ln.AccidentClaimId).FirstOrDefault(),
                                CompCode = remain.Lookupvalue,
                                CompDesc = remain.Description,
                                InitialReserve = 0.00m,
                                PreviousReserve = 0.00m,
                                MovementReserve = 0.00m,
                                CurrentReserve = 0.00m,
                                //PaymentId = (from ln in reserveModel.ReserveDetails select ln.PaymentId).FirstOrDefault()
                            });
                        }
                        reserveModel._reserveDetails.Clear();
                        reserveModel.GetType().GetProperty("ReserveDetails").SetValue(reserveModel, list, null);
                    }
                }
                else
                {
                    var CompCodeList = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" orderby l.DisplayOrder ascending select l.Lookupvalue).ToList();
                    var TranList = new List<ClaimReserveDetails>();
                    for (var i = 0; i < CompCodeList.Count; i++)
                    {
                        var desc = Convert.ToString(CompCodeList[i]);
                        TranList.Add(new ClaimReserveDetails()
                        {
                            ReserveDetailId = 0,
                            ReserveId = 0,
                            AccidentClaimId = AccidentClaimId,
                            CompCode = CompCodeList[i].Trim(),
                            CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == desc select l.Description).FirstOrDefault(),
                            InitialReserve = 0.00m,
                            PreviousReserve = 0.00m,
                            MovementReserve = 0.00m,
                            CurrentReserve = 0.00m,
                            //PaymentId = 0
                        });
                    }
                    reserveModel.ReserveDetails = TranList.ToList();
                }
                return reserveModel;
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
        public static void UpdateSoraNoCodeMaster(String sorano, int ClaimId, int RecoveryId, MCASEntities db)
        {
            int mm = 0, yy = 0,sr=0;
            dynamic ClaRec=null;
            try{
                   //CLM_ClaimRecovery 
                //try
                //{
                //    ClaRec = db.CLM_ClaimRecovery.Where(x => x.ClaimId == ClaimId && x.RecoveryId == RecoveryId).FirstOrDefault();
                //}catch(Exception e){}

                //if (ClaRec == null)
                //{
                    if (!string.IsNullOrEmpty(sorano))
                    {
                        string[] soraar = sorano.Split('/');
                        if (soraar.Length > 2)
                        {
                            mm = Convert.ToInt32(soraar[1]);
                            yy = Convert.ToInt32(soraar[2]);
                            sr = Convert.ToInt32(soraar[4]);
                            try
                            {
                                var sora = (from o in db.TM_CodeMaster
                                            where o.Code == "SORA" && o.CurrentMonth == mm && o.CurrentYear == yy
                                            select o).FirstOrDefault();
                                if (sora!=null && sr > sora.CurrentNo)
                                {
                                    sora.CurrentNo = sora.CurrentNo + 1;
                                    db.SaveChanges();
                                }
                                if(sora==null)
                                {
                                    sora = new TM_CodeMaster();
                                    sora.CurrentNo =  sr;
                                    sora.CurrentMonth = mm;
                                    sora.CurrentYear = yy;
                                    sora.Code = "SORA";
                                    db.TM_CodeMaster.AddObject(sora);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception e) { }

                        }
                    }
               // }
            }catch (Exception e) { }
        }
        public static string FetchSoraNo(String RecoveryID, int ClaimId, int AccidentClaimId, MCASEntities db, ref string  sora)
        {
            string   orgini = "", ociini = "", FinalSettleDate = ""
                , ClaimantStatus = "";
            int? ClaimsOfficer = 0, sr = 0, orgid=0;
            //select Organization,CDGIClaimRef,* from ClaimAccidentDetails where AccidentClaimId=1409
          try
            {
                

                //var claim = (from o in db.CLM_Claims
                //             where o.ClaimID == Convert.ToInt32(ClaimId)
                //           select o).FirstOrDefault();
          
                var claim = db.CLM_Claims.Where(x => x.ClaimID == ClaimId).FirstOrDefault();
                         
                if(claim !=null)
                {
                    if (!string.IsNullOrEmpty(claim.FinalSettleDate.ToString()))
                    {
                        if (claim.FinalSettleDate.ToString().Contains(":"))
                        {
                            FinalSettleDate = claim.FinalSettleDate.ToString().Split(' ')[0];
                        }
                        else
                        {
                            FinalSettleDate = claim.FinalSettleDate.ToString();
                        }
                    }
                    ClaimantStatus = claim.ClaimantStatus;
                    ClaimsOfficer = claim.ClaimsOfficer;
                    if (ClaimantStatus != null && ClaimantStatus.Trim() == "2" && !string.IsNullOrEmpty(FinalSettleDate))
                    {
                        orgid = (from o in db.ClaimAccidentDetails
                                 where o.AccidentClaimId == AccidentClaimId
                                 select o).FirstOrDefault().Organization;
                        orgini = (from o in db.MNT_OrgCountry
                                  where o.Id == orgid
                                  select o).FirstOrDefault().Initial;
                        ociini = (from o in db.MNT_Users
                                  where o.SNo == ClaimsOfficer
                                  select o).FirstOrDefault().Initial;
                    //    DateTime dt = DateTime.ParseExact(FinalSettleDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(FinalSettleDate))
                        {
                            int mm = Convert.ToInt32( FinalSettleDate.Split('/')[1]);
                            int yy = Convert.ToInt32(FinalSettleDate.Split('/')[2].Substring(2, 2));
                            var codem = (from o in db.TM_CodeMaster
                                  where o.Code == "SORA" && o.CurrentMonth == mm
                                                  && o.CurrentYear == yy
                                  select o).FirstOrDefault();
                            if(codem!=null)
                            {
                              sr=  codem.CurrentNo;
                              if (mm > 9)
                              {
                                  sora = orgini + "/" + mm + "/" + yy + "/" + ociini + "/" + (sr + 1);
                              }
                              else
                              {
                                  sora = orgini + "/0" + mm + "/" + yy + "/" + ociini + "/" + (sr + 1);
                              }
                            }
                            else
                            {
                                sr =0;
                                if (mm > 9)
                                {
                                    sora = orgini + "/" + mm + "/" + yy + "/" + ociini + "/" + (sr + 1);
                                }
                                else
                                {
                                    sora = orgini + "/0" + mm + "/" + yy + "/" + ociini + "/" + (sr + 1);
                                }
                            }
                        }
                     
                    }
                }

                

            }
            catch(Exception e) { }

            return sora;
        }
        public static ClaimRecoveryCrTxModel FetchRecoveryModel(ClaimRecoveryCrTxModel model, string ViewMode, String RecoveryID)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                var recoveryId = Convert.ToInt32(RecoveryID);
                if (ViewMode == "Select" || ViewMode == "")
                {
                    //model.Reserve = ClaimRecoveryCrTxModel.FetchReserve(Convert.ToInt32(model.AccidentClaimId), Convert.ToInt32(model.ClaimId), Convert.ToInt32(model.ClaimType), ViewMode, RecoveryID);
                    //for (int i = 0; i < model.Reserve.ReserveDetails.Count; i++)
                    //{
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "INVA")
                    //    {
                    //        model.InvoiceAmt = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "CELOR")
                    //    {
                    //        model.LossofRental = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "CELOU")
                    //    {
                    //        model.CELossofUse = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "CEME")
                    //    {
                    //        model.MedicalExpenses = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "OTH1S")
                    //    {
                    //        model.Others1 = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "CELOI")
                    //    {
                    //        model.LossofIncome = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "LF")
                    //    {
                    //        model.LegalFee = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "CR")
                    //    {
                    //        model.CarRental = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "CESF")
                    //    {
                    //        model.SurveyFee = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "CC")
                    //    {
                    //        model.CarCourtesy = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "TPGIA")
                    //    {
                    //        model.TPGIAFee = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "OTH2S")
                    //    {
                    //        model.Others2 = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //    if (model.Reserve.ReserveDetails[i].CompCode == "LTA")
                    //    {
                    //        model.LTAFee = model.Reserve.ReserveDetails[i].CurrentReserve;
                    //    }
                    //}
                    var recoverydtl = (from l in db.CLM_ClaimRecovery where l.AccidentClaimId == model.AccidentClaimId && l.ClaimId == model.ClaimId && l.RecoveryId == recoveryId select l).FirstOrDefault();
                    if (recoverydtl != null)
                    {
                        model.RecoveryId = recoveryId;
                        model.Adjustment = (Decimal)recoverydtl.Adjustment;
                        model.CELossofUse = recoverydtl.CELossofUse;
                        model.MedicalExpenses = recoverydtl.MedicalExpenses;
                        model.Compensation = (Decimal)recoverydtl.Compensation;
                        model.CarRental = recoverydtl.CarRental;
                        model.CarCourtesy = recoverydtl.CarCourtesy;
                        model.CDGECost = (Decimal)recoverydtl.CDGECost;
                        model.InvoiceAmt = recoverydtl.InvoiceAmount;
                        model.CreditDebitNoteAmount = recoverydtl.CreditDebitNoteAmount;
                        model.CreditDebitNoteNo = recoverydtl.CreditDebitNoteNo;
                        model.Insurer = recoverydtl.Insurer;
                        model.OwnerHirer = recoverydtl.OwnerHirer;
                        model.ThirdParty = recoverydtl.ThirdParty;
                        model.Contractor = recoverydtl.Contractor;
                        model.Others1 = recoverydtl.Others1;
                        model.LossofRental = recoverydtl.LossofRental;
                        model.LossofIncome = recoverydtl.LossofIncome;
                        model.LegalFee = recoverydtl.LegalFee;
                        model.LTAFee = recoverydtl.LTAFee;
                        model.SurveyFee = recoverydtl.SurveyFee;
                        model.Others2 = recoverydtl.Others2;
                        model.TPGIAFee = recoverydtl.TPGIAFee;
                        model.TotalClaimReceipts = recoverydtl.TotalClaimReceipts;
                        model.NetCostofRepairs = recoverydtl.NetCostofRepairs;
                        model.TClmExpensesClaimant = recoverydtl.TClmExpensesClaimant;
                        model.NetClaimRecovery = recoverydtl.NetClaimRecovery;
                        model.TClmExpensesIncidental = recoverydtl.TClmExpensesIncidental;
                        model.ProfitSharingForTaxi = recoverydtl.ProfitSharingForTaxi;
                        model.TotalClaimExpenses = recoverydtl.TotalClaimExpenses;
                        model.OverUnderClaimRecovery = recoverydtl.OverUnderClaimRecovery;
                        model.ShareNetClmRecovery = recoverydtl.ShareNetClmRecovery;
                        model.LessCDGEAdminFee = recoverydtl.LessCDGEAdminFee;
                        model.CDGE1Or3 = recoverydtl.CDGE1Or3;
                        model.PrftShrTxTlCases = recoverydtl.PrftShrTxTlCases;
                        model.Taxi2Or3 = recoverydtl.Taxi2Or3;
                        model.GeneralRemarks = recoverydtl.GeneralRemarks;
                        model.Sora_SocraSrlNo = recoverydtl.SORASOCRA;
                        model.SORASerialNo = recoverydtl.SORASerialNo;
                    }
                }
                else
                {
                    var recoverydtl = (from l in db.CLM_ClaimRecovery where l.AccidentClaimId == model.AccidentClaimId && l.ClaimId == model.ClaimId && l.RecoveryId == recoveryId select l).FirstOrDefault();
                    model.RecoveryId = recoveryId;
                    model.Adjustment = (Decimal)recoverydtl.Adjustment;
                    model.CELossofUse = recoverydtl.CELossofUse;
                    model.MedicalExpenses = recoverydtl.MedicalExpenses;
                    model.Compensation = (Decimal)recoverydtl.Compensation;
                    model.InvoiceAmt = recoverydtl.InvoiceAmount;
                    model.CarRental = recoverydtl.CarRental;
                    model.CarCourtesy = recoverydtl.CarCourtesy;
                    model.CDGECost = (Decimal)recoverydtl.CDGECost;
                    model.CreditDebitNoteAmount = recoverydtl.CreditDebitNoteAmount;
                    model.CreditDebitNoteNo = recoverydtl.CreditDebitNoteNo;
                    model.Insurer = recoverydtl.Insurer;
                    model.OwnerHirer = recoverydtl.OwnerHirer;
                    model.ThirdParty = recoverydtl.ThirdParty;
                    model.Contractor = recoverydtl.Contractor;
                    model.Others1 = recoverydtl.Others1;
                    model.LossofRental = recoverydtl.LossofRental;
                    model.LossofIncome = recoverydtl.LossofIncome;
                    model.LegalFee = recoverydtl.LegalFee;
                    model.LTAFee = recoverydtl.LTAFee;
                    model.SurveyFee = recoverydtl.SurveyFee;
                    model.Others2 = recoverydtl.Others2;
                    model.TPGIAFee = recoverydtl.TPGIAFee;
                    model.TotalClaimReceipts = recoverydtl.TotalClaimReceipts;
                    model.NetCostofRepairs = recoverydtl.NetCostofRepairs;
                    model.TClmExpensesClaimant = recoverydtl.TClmExpensesClaimant;
                    model.NetClaimRecovery = recoverydtl.NetClaimRecovery;
                    model.TClmExpensesIncidental = recoverydtl.TClmExpensesIncidental;
                    model.ProfitSharingForTaxi = recoverydtl.ProfitSharingForTaxi;
                    model.TotalClaimExpenses = recoverydtl.TotalClaimExpenses;
                    model.OverUnderClaimRecovery = recoverydtl.OverUnderClaimRecovery;
                    model.ShareNetClmRecovery = recoverydtl.ShareNetClmRecovery;
                    model.LessCDGEAdminFee = recoverydtl.LessCDGEAdminFee;
                    model.CDGE1Or3 = recoverydtl.CDGE1Or3;
                    model.PrftShrTxTlCases = recoverydtl.PrftShrTxTlCases;
                    model.Taxi2Or3 = recoverydtl.Taxi2Or3;
                    model.GeneralRemarks = recoverydtl.GeneralRemarks;
                    model.Sora_SocraSrlNo = recoverydtl.SORASOCRA;
                    model.SORASerialNo = recoverydtl.SORASerialNo;
                }

                var cnfmamtlist = (from l in db.Clm_ConfirmAmtBreakdown where l.AccidentClaimId == model.AccidentClaimId && l.ClaimId == model.ClaimId select l).FirstOrDefault();
                if (cnfmamtlist != null)
                {
                    model.Confirmamtbd = FetchConfirmAmtBreakdown(Convert.ToInt32(model.AccidentClaimId), Convert.ToInt32(model.ClaimId));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return model;
        }

        public static ConfirmAmtModel FetchConfirmAmtBreakdown(int AccidentClaimId, int ClaimID)
        {
            MCASEntities obj = new MCASEntities();
            ConfirmAmtModel model = new ConfirmAmtModel();
            var confimamtdata = obj.Clm_ConfirmAmtBreakdown.Where(x => x.AccidentClaimId == AccidentClaimId && x.ClaimId == ClaimID).FirstOrDefault();
            DataMapper.Map(confimamtdata, model, true);
            return model;
        }

        #endregion

        #region public methods
        public ClaimRecoveryCrTxModel Save()
        {
            MCASEntities obj = new MCASEntities();
            CLM_ClaimRecovery clmrecov;
            var model = new ClaimRecoveryCrTxModel();
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
                        clmrecov.CarRental = this.CarRental;
                        clmrecov.CarCourtesy = this.CarCourtesy;
                        clmrecov.CDGECost = this.CDGECost;
                        clmrecov.LegalFee = this.LegalFee;
                        clmrecov.SurveyFee = this.SurveyFee;
                        clmrecov.ThirdParty = this.ThirdParty;
                        clmrecov.InvoiceAmount = this.InvoiceAmt;
                        clmrecov.CreatedDate = DateTime.Now;
                        clmrecov.CreatedBy = this.CreatedBy;
                        clmrecov.RecoverFrom = "";
                        obj.CLM_ClaimRecovery.AddObject(clmrecov);
                        this.ResultMessage = "Record saved successfully.";
                        #endregion
                    }
                    else
                    {
                        #region UpdateRecovery
                        string[] ignoreList = new string[] { "Createddate", "CreatedBy" };
                        var crDate = clmrecov.CreatedDate;
                        DataMapper.Map(this, clmrecov, true, ignoreList);
                        clmrecov.CarRental = this.CarRental;
                        clmrecov.CarCourtesy = this.CarCourtesy;
                        clmrecov.CDGECost = this.CDGECost;
                        clmrecov.LegalFee = this.LegalFee;
                        clmrecov.SurveyFee = this.SurveyFee;
                        clmrecov.ThirdParty = this.ThirdParty;
                        clmrecov.InvoiceAmount = this.InvoiceAmt;
                        clmrecov.Modifiedby = this.CreatedBy;
                        clmrecov.ModifiedDate = DateTime.Now;
                        clmrecov.CreatedDate = crDate;
                        clmrecov.RecoverFrom = "";
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
                    if (clmrecov.ModifiedDate != null && clmrecov.Modifiedby != null)
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
        #endregion

    }

   public class ClaimCRTXRecoveryModelCollection
   {
       #region "Object Properties"
       public Int64? OrgRecordNumber { get; set; }
       public int? ClaimID { get; set; }
       public int? ClaimType { get; set; }
       public int? RecoveryId { get; set; }
       public int? AccidentClaimId { get; set; }
       public int? PolicyId { get; set; }
       public string TPVehicleNo { get; set; }
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
