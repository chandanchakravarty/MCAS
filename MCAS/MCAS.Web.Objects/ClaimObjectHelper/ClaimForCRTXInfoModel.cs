using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using MCAS.Entity;
using System.Data.Entity;
using MCAS.Web.Objects.MastersHelper;
using System.ComponentModel;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.Resources.Common;
using MCAS.Web.Objects.Resources.ClaimRegProcPCNTX;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Objects;
using System.Globalization;



namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimForCRTXInfoModel:BaseModel
    {
        public ClaimForCRTXInfoModel()
        {
            this._claimsCollection = FetchClaimList(0);
        }
        public ClaimForCRTXInfoModel(int AccidentClaimId)
        {
            this._claimsCollection = FetchClaimList(AccidentClaimId);
        }

        private ConfirmAmtModel _cofrmamtbd = new ConfirmAmtModel();
        public ConfirmAmtModel Confirmamtbd
        {
            get { return _cofrmamtbd; }
            set { _cofrmamtbd = value; }
        }

        public int _prop1 { get; set; }

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

        private DateTime? _lawerDate = null;

        public int AccidentClaimId { get; set; }

        public int? ClaimID { get; set; }

        public int? PolicyId { get; set; }

        public string hidOrgprop { get; set; }

        public string Hshowgrid { get; set; }

        public string OrganizationType { get; set; }

        public string ClaimNo { get; set; }

        public DateTime Hdate { get; set; }

        [StringLength(100)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]

        public string InvoiceNo { get; set; }

        public string JobNo { get; set; }

        public string CustomerCode { get; set; }

        public string BusinessArea { get; set; }

        [Required(ErrorMessageResourceType = typeof(NewClaimClientErrorCrTx), ErrorMessageResourceName = "RFVTPVehicleNo")]
        public string TPVehicleNo { get; set; }

        public string LawerRef { get; set; }

        public string OwnLawyer { get; set; }

        public string TPInsurer { get; set; }

        public string TPRef { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LawyerDate
        {
            get { return _lawerDate; }
            set { _lawerDate = value; }
        }

        public string LawyerGIADRM { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateToGIADRM { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? WriteIssued { get; set; }

        public string WriteNo { get; set; }

        public string Sharellocation { get; set; }

        public string ContractorInvoiceNo { get; set; }

        public string WSONo { get; set; }

        public Decimal WsoInvoiceAmt { get; set; }

        public string WsoCnNo { get; set; }

        public Decimal WsoCNAmt { get; set; }

        //public string CreatedBy { get; set; }

        private DateTime? _createddate = null;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate {
            get { return _createddate; }
            set { _createddate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReopenedDate { get; set; }

        public string RecordReopenedReason { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? RecordCancellationDate { get; set; }

        public string RecordCancellationReason { get; set; }

        public int ClaimType { get; set; }
        public List<LookUpListItems> ClaimTypeList { get; set; }

        public Decimal ClaimAmt { get; set; }

        public string ClaimStatus { get; set; }

        public string CaseTypeL1 { get; set; }

        public string CaseTypeL2 { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "File Received Date Required")]
        public DateTime? FileReceivedDate { get; set; }

        [Required(ErrorMessage = "Collision Type Required")]
        public string Collisiontype { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Claim Date Required")]
        public DateTime? ClaimDate { get; set; }
    
        public int? OurSurveyorApp { get; set; }
        public List<AdjusterModel> list { get; set; }

        public string CDGEStatus { get; set; }

        public int NoOfDaysForRepairs { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FollowUpDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfSurvey { get; set; }

        //[Required(ErrorMessage="Admin Support is required")]
        
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimRegProcPCNTX.NewClaimClientErrorCrTx), ErrorMessageResourceName = "RFVAdminSupport")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimRegProcPCNTX.NewClaimClientErrorCrTx), ErrorMessageResourceName = "RFVAdminSupport")]
        [DisplayName("Admin Support")]
        public int AdminSupport { get; set; }
        public List<ClaimOfficerModel> AdminSupportList { get; set; }

        public string DriversLiability { get; set; }

       

        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimRegProcPCNTX.NewClaimClientErrorCrTx), ErrorMessageResourceName = "RFVOfficeInCharge")]
        public int? OfficeInCharge { get; set; }

        public string Office_InCharge { get; set; }
        public List<ClaimOfficerModel> ClaimOfficerList { get; set; }

     // [RequiredIf("hFirstName", "true", ErrorMessageResourceType = typeof(NewClaimClientErrorCrTx), ErrorMessageResourceName = "RFVCaseStatus")]
        public string CaseStatus { get; set; }

        public string SettledBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InvoiceDate { get; set; }

        public Decimal InvoiceAmt { get; set; }

        public string GST { get; set; }

        public Decimal ActualAmt { get; set; }

        public Decimal ExcessAmt { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ConfirmDate { get; set; }

        public Decimal ConfirmAmt { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FinalSettleDate { get; set; }

        public Decimal SettledAmt { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TimeBarDate { get; set; }

        public string PaymentDetails { get; set; }

        public string FileArchievedRef { get; set; }

        public string ChequeDetails { get; set; }

        public string NatureOfAcc { get; set; }

        public Decimal WsActRcvr { get; set; }

        public string ClaimantStatus { get; set; }

        public string Remarks { get; set; }

        public string SettlementType { get; set; }

        public string ResultMessage { get; set; }

        public List<ClaimantType> NatureAccList { get; set; }

        public List<ClaimantType> SettlementTypeList { get; set; }

        public List<ClaimantType> CDGEStatusList { get; set; }

        public List<ClaimantType> CollisionTypeList { get; set; }

        public List<ClaimantType> ClaimantStatusList { get; set; }

        public List<CedantModel> TPInsurerList { get; set; }

        public List<ClaimantType> CaseStatusList { get; set; }

        private List<ClaimCrTxCollection> _claimsCollection;

        public List<ClaimCrTxCollection> ClaimsCollection
        {
            get { return _claimsCollection; }
        }

        public static string fetchClaimRefNo(int AccidentClaimId) { 
            MCASEntities db = new MCASEntities();
            string claimRefNo = db.ClaimAccidentDetails.Where(x => x.AccidentClaimId == AccidentClaimId).Select(y => y.CDGIClaimRef).FirstOrDefault();
            if (!string.IsNullOrEmpty(claimRefNo))
            {
                claimRefNo = claimRefNo.Replace(" ", "");
            }
            return claimRefNo;
        }



        public static string fetchOwner(int AccidentClaimId)
        { 
            MCASEntities db = new MCASEntities();
            string ownername = db.ClaimAccidentDetails.Where(x => x.AccidentClaimId == AccidentClaimId).Select(y => y.OwnerName).FirstOrDefault();
            return ownername;
        }

        public string GetClaimNo(int CType, string AccidentId)
        {
            string result = string.Empty;
            MCASEntities db = new MCASEntities();
            try
            {
                var correctvech = db.Proc_GetCLM_ClaimNo(Convert.ToString(CType), AccidentId).FirstOrDefault() == null ? 0 : Convert.ToInt32(db.Proc_GetCLM_ClaimNo(Convert.ToString(CType), AccidentId).ToList()[0].ClaimID);
                var res = Convert.ToString(CType) == "1" ? "OD-" : Convert.ToString(CType) == "2" ? "PD-" : Convert.ToString(CType) == "3" ? "BI-" : Convert.ToString(CType) == "4" ? "RC-" : "";
                result = res + (correctvech + 1);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        //public string GetClaimNo(int CType, string AccidentClaimId)
        //{
        //    string result = string.Empty;
        //    MCASEntities db = new MCASEntities();
        //    try
        //    {
        //        var correctvech = db.Proc_GetCLM_ClaimNo(Convert.ToString(CType), AccidentClaimId).FirstOrDefault() == null ? 0 : Convert.ToInt32(db.Proc_GetCLM_ClaimNo(Convert.ToString(CType), AccidentClaimId).ToList()[0].ClaimID);
        //        var res = Convert.ToString(CType) == "1" ? "OD-" : Convert.ToString(CType) == "2" ? "PD-" : Convert.ToString(CType) == "3" ? "BI-" : Convert.ToString(CType) == "4" ? "RC-" : "";
        //        result = res + (correctvech + 1);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        db.Dispose();
        //    }
        //    return result;
        //}

        private List<ClaimCrTxCollection> FetchClaimList(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                this.AccidentClaimId = AccidentClaimId;
                var items = new List<ClaimCrTxCollection>();
                if (AccidentClaimId != 0)
                {
                    //List<MNT_Cedant> list1 = new List<MNT_Cedant>();
                    //list1 = db.MNT_Cedant.ToList();
                    var Ownlist = db.Proc_GetCLM_ClaimListCrTx(Convert.ToString(AccidentClaimId)).ToList();
                    items = (from n in Ownlist
                             select new ClaimCrTxCollection()
                             {
                                 OrgRecordNumber = n.RecordNumber,
                                 RecordNumber = n.ClaimRecordNo,
                                 ClaimID = n.ClaimID,
                                 TPVehicleNo = n.TPVehicleNo,
                                 ClaimType = n.ClaimType,
                                 AccidentClaimId = n.AccidentClaimId,
                                 PolicyId = n.PolicyId,
                                 ClaimTypeDesc = n.ClaimType.ToString() == "1" ? Common._1 : n.ClaimType.ToString() == "2" ? Common._2 : n.ClaimType.ToString() == "3" ? Common._3 : n.ClaimType.ToString() == "4" ? Common._4 : "",
                                 ClaimTypeCode = n.ClaimTypeCode
                             }
                            ).ToList();
                }

                return items;
            }
            catch (Exception ex)
            {
                throw (new Exception("Claim List Could not be loaded. " + ex));
            }
            finally
            {
                db.Dispose();
            }
        }

        public static List<ClaimOfficerModel> FetchClaimOfficer(string roleCode)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimOfficerModel>();
            var ClaimOfficerDetails = _db.Proc_GetClaimOfficerByRole(roleCode).ToList();
            if (ClaimOfficerDetails.Any())
            {
                item = (from n in ClaimOfficerDetails
                        select new ClaimOfficerModel()
                        {
                            TranId = n.SNo,
                            ClaimOfficerName = n.UserDispName
                        }
                        ).ToList();
            }
            item.Insert(0, new ClaimOfficerModel() { TranId = 0, ClaimOfficerName = "[Select...]" });
            _db.Dispose();
            return item;
        }

        public static List<ClaimantType> FetchLookUpList(string Category)
        {
            List<ClaimantType> list = new List<ClaimantType>();
            var lookup = LookUpListItems.Fetch(Category, false);
            lookup.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            list = (from n in lookup
                    where n.Lookup_value != null
                    select new ClaimantType()
                    {
                        Id = n.Lookup_value.Trim(),
                        Text = n.Lookup_desc.Trim()
                    }
                        ).ToList();
            return list;
        }

        public static List<ClaimantType> FetchLookUpListForClaimType(int AccidentClaimId)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> list = new List<ClaimantType>();
            list = (from l in obj.Proc_GetClaimType(AccidentClaimId)
                    select new ClaimantType()
                    {
                        Id = l.Lookupvalue.Trim(),
                        Text = l.Description.Trim()
                    }
                        ).ToList();
            list.Insert(0, new ClaimantType() { Id = "0", Text = "[Select...]" });
            return list;
        }

        public static List<CedantModel> FetchTPInsurer()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<CedantModel>();
            var CedantList = _db.Proc_GetCedantNameList().ToList();
            if (CedantList.Any())
            {
                item = (from n in CedantList
                        select new CedantModel()
                        {
                            CedantId = n.CedantId,
                            CedantCode = n.CedantName
                        }
                        ).ToList();
            }
            item.Insert(0, new CedantModel() { CedantId = 0, CedantCode = "[Select...]" });
            _db.Dispose();
            return item;


        }

        public static List<AdjusterModel> FetchSurveyor()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<AdjusterModel>();
            var Adjusterlist = (from x in _db.MNT_Adjusters where x.AdjusterCode.Contains("SVY") orderby x.AdjusterName select x);
            if (Adjusterlist.Any())
            {
                foreach (var data in Adjusterlist)
                {
                    item.Add(new AdjusterModel()
                    {
                        AdjusterId = data.AdjusterId,
                        AdjusterName = data.AdjusterName,
                        AdjusterTypeCode = data.AdjusterTypeCode,
                        AdjusterCode = data.AdjusterCode,
                        AdjusterAddress = new AddressModel()
                        {
                            Status = data.Status
                        }
                    });
                }
            }
            item.Insert(0, new AdjusterModel() { AdjusterId = 1, AdjusterName = "[Select...]" });
            return item;
        }

        public ClaimForCRTXInfoModel FetchAllLists(ClaimForCRTXInfoModel model)
        {
            try
            {
                ClaimMandateModel m2 = new ClaimMandateModel();
                model.ClaimOfficerList = FetchClaimOfficer("CO");
                //model.ClaimantStatusList = FetchLookUpList("ClaimantType");
                model.ClaimantStatusList = FetchCommonMasterData("ClaimantType", AccidentClaimId);
                //model.CaseStatusList = FetchLookUpList("CaseStatus");
                model.list = FetchSurveyor();
                model.ClaimOfficerList = ClaimForCRTXInfoModel.FetchClaimOfficer("CO");
                model.AdminSupportList = ClaimForCRTXInfoModel.FetchClaimOfficer("All");
                model.ClaimTypeList = m2.ClaimTypeValue("ClaimType");
                model.ClaimantStatusList = ClaimForCRTXInfoModel.FetchLookUpList("ClaimantStatus");
                //model.CaseStatusList = ClaimForCRTXInfoModel.FetchLookUpList("CaseStatus");
                //model.NatureAccList = ClaimForCRTXInfoModel.FetchLookUpList("NatureOfAcc");
                //model.CDGEStatusList = ClaimForCRTXInfoModel.FetchLookUpList("CDGEStatus");
                //model.CollisionTypeList = ClaimForCRTXInfoModel.FetchLookUpList("CollisionType");
                model.CaseStatusList = FetchCommonMasterData("CaseStatus", AccidentClaimId);
                model.NatureAccList = FetchCommonMasterData("NatureOfAcc", AccidentClaimId);
                model.CDGEStatusList = FetchCommonMasterData("CDGEStatus", AccidentClaimId);
                model.CollisionTypeList = FetchCommonMasterData("CollisionType", AccidentClaimId);
                model.SettlementTypeList = FetchCommonMasterData("SettlementType", AccidentClaimId,true);
                model.TPInsurerList = ClaimForCRTXInfoModel.FetchTPInsurer();
                return model;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public ClaimForCRTXInfoModel Fetch(int AccidentClaimID)
        {
            MCASEntities obj = new MCASEntities();
            var item = new ClaimForCRTXInfoModel() { ViewMode = this.ViewMode };
            if (AccidentClaimId != null)
            {
                var Claim = (from clm in obj.CLM_Claims where clm.AccidentClaimId == this.AccidentClaimId select clm).FirstOrDefault();
                if (Claim != null)
                {
                    item.AccidentClaimId = this.AccidentClaimId;
                    item.PolicyId = this.PolicyId;
                    item.ClaimID = Claim.ClaimID;
                    item.ClaimType = Claim.ClaimType ?? default(int);
                    item.ClaimDate = Claim.ClaimDate;
                    item.InvoiceNo = Claim.InvoiceNo;
                    item.JobNo = Claim.JobNo;
                    item.CultureCode = Claim.CustomerCode;
                    item.BusinessArea = Claim.BusinessArea;
                    item.TPVehicleNo = Claim.TPVehicleNo;
                    item.LawerRef = Claim.LawerRef;
                    item.ClaimantStatus = Claim.ClaimantStatus;
                    item.FinalSettleDate = Claim.FinalSettleDate;
                    item.TimeBarDate = Claim.TimeBarDate;
                    item.CaseStatus = Claim.CaseStatus;
                    item.Remarks = Claim.Remarks;

                }
            }
            obj.Dispose();
            return item;
        }

        public static string GetPermanentCDGEClaimRefNo(int accidentClaimId, string OrganizationType, string ClaimPrefix, MCASEntities _db)
        {
            
            var values = _db.Proc_SetCDGIClaimsRefNo(Convert.ToInt32(accidentClaimId), OrganizationType.ToString(), ClaimPrefix).ToList();
            string claimRefNo = values[0].CDGIClaimRef.ToString().Replace(" ","");
            return claimRefNo;
        }

        public ClaimForCRTXInfoModel Save(ConfirmAmtModel confirmamtmodel)
        {
            MCASEntities objEntity = new MCASEntities();
            CLM_Claims claimdetail;
            ClaimAccidentDetail accdetail= new ClaimAccidentDetail();
            Clm_ConfirmAmtBreakdown cnfmamt = new Clm_ConfirmAmtBreakdown();
            
            if (objEntity.Connection.State == System.Data.ConnectionState.Closed)objEntity.Connection.Open();
            using (System.Data.Common.DbTransaction transaction = objEntity.Connection.BeginTransaction())
            {
              
                 try
                    {

                     var claimantStatus = objEntity.CLM_Claims.Where(x => x.ClaimID == this.ClaimID).Select(x => x.ClaimantStatus).FirstOrDefault();
                     var val = objEntity.CLM_Claims.Where(x => x.ClaimID == this.ClaimID).Select(x => x.ClaimantStatus).ToList();
                     if (val.Any())   
                      {
                        claimdetail = objEntity.CLM_Claims.Where(x => x.ClaimID == this.ClaimID.Value).FirstOrDefault();
                        this.CreatedBy = claimdetail.CreatedBy ?? Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        DataMapper.Map(this, claimdetail, true);
                   //     claimdetail.AccidentClaimId = 452;
                        claimdetail.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        claimdetail.ModifiedDate = DateTime.Now;
                        claimdetail.ClaimantStatus = this.ClaimantStatus == "4" ? "1" : this.ClaimantStatus;
                        claimdetail.Gst = this.GST;
                        claimdetail.Invoicedate = this.InvoiceDate;
                        claimdetail.DriverLiablity = this.DriversLiability;
                        claimdetail.AdminSupport = Convert.ToString(this.AdminSupport);
                        claimdetail.SurveyorAppointed = this.OurSurveyorApp; 
                        claimdetail.LawyerGIADRM = this.LawyerGIADRM;
                        claimdetail.WritIssuedDate = this.WriteIssued;
                        claimdetail.WritNo = this.WriteNo;
                        claimdetail.WsoCnNo = this.WsoCnNo;
                        claimdetail.NoOfDaysForRepairs = this.NoOfDaysForRepairs;
                        claimdetail.ClaimsOfficer = this.OfficeInCharge;
                        claimdetail.ChequeDetails = this.ChequeDetails;
                        objEntity.SaveChanges();
                        var val1 = objEntity.CLM_Claims.Where(x =>x.AccidentClaimId== claimdetail.AccidentClaimId ).Select(x => x.ClaimID == this.ClaimID).FirstOrDefault();
                        if (val1 != false)
                        {
                            if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx"))
                            {
                                var OwnerName = ClaimForCRTXInfoModel.fetchOwner(AccidentClaimId);
                                accdetail.OwnerName = OwnerName;
                                accdetail = objEntity.ClaimAccidentDetails.Where(x => x.AccidentClaimId == claimdetail.AccidentClaimId).FirstOrDefault();
                                var orgint = objEntity.MNT_OrgCountry.Where(x => x.Id == accdetail.Organization).FirstOrDefault();
                                accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), (orgint.Initial == null) ? "" : orgint.Initial, objEntity);
                                //if (accdetail.OwnerName == "CTPL")
                                //{
                                //    accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "CTPL", objEntity);
                                //}
                                //else
                                //{
                                //    accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "CCPL", objEntity);
                                //}
                            }
                            else
                            {
                                accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "PTE", objEntity);
                            }
                        }
                        this.Confirmamtbd = confirmamtmodel;
                        UpdateNSaveConfirmAmtBreakdown(objEntity, confirmamtmodel);
                        this.CreatedBy = claimdetail.CreatedBy;
                        this.CreatedOn = Convert.ToDateTime(claimdetail.CreatedDate);
                        this.ModifiedOn = DateTime.Now;
                        this.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.ResultMessage = "Record updated successfully.";
                    }
                    else
                    {
                       
                        claimdetail = new CLM_Claims();
                        DataMapper.Map(this, claimdetail, true);
                   //     claimdetail.AccidentClaimId = 452;
                        claimdetail.Gst = this.GST;
                        claimdetail.Invoicedate = this.InvoiceDate;
                        claimdetail.DriverLiablity = this.DriversLiability;
                        claimdetail.SurveyorAppointed = this.OurSurveyorApp;
                        claimdetail.ClaimsOfficer = this.OfficeInCharge;
                        claimdetail.WsoCnNo = this.WsoCnNo;
                        claimdetail.AdminSupport = Convert.ToString(this.AdminSupport);
                        claimdetail.LawyerGIADRM = this.LawyerGIADRM;
                        claimdetail.ChequeDetails = this.ChequeDetails;
                        claimdetail.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        claimdetail.CreatedDate = DateTime.Now;
                        claimdetail.ClaimStatus = "1";
                        claimdetail.IsActive = "Y";
                        claimdetail.ClaimantStatus = this.ClaimantStatus == "4" ? "1" : this.ClaimantStatus;
                        claimdetail.ClaimType = this.ClaimType;
                        claimdetail.WritIssuedDate = this.WriteIssued;
                        claimdetail.WritNo = this.WriteNo;
                        claimdetail.NoOfDaysForRepairs = this.NoOfDaysForRepairs;
                        //claimdetail.TPVehicleNo = this.TPVehicleNo;
                        // claimdetail.TPInsurer= this.
                        //claimdetail.ClaimType = 4;
                        var ClaimNo = GetClaimNo(ClaimType, Convert.ToString(claimdetail.AccidentClaimId));
                        claimdetail.ClaimRecordNo = ClaimNo;
                        objEntity.CLM_Claims.AddObject(claimdetail);
                        objEntity.SaveChanges();
                        this.ClaimID = claimdetail.ClaimID;
                        var val1 = objEntity.CLM_Claims.Where(x => x.AccidentClaimId == claimdetail.AccidentClaimId).Select(x => x.ClaimID).ToList();
                        if (val1.Count() == 1)
                        {
                            if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx"))
                            {
                                var OwnerName = ClaimForCRTXInfoModel.fetchOwner(AccidentClaimId);
                                accdetail.OwnerName = OwnerName;
                                accdetail = objEntity.ClaimAccidentDetails.Where(x => x.AccidentClaimId == claimdetail.AccidentClaimId).FirstOrDefault();
                                var orgint = objEntity.MNT_OrgCountry.Where(x => x.Id == accdetail.Organization).FirstOrDefault();
                                accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), (orgint.Initial == null) ? "" : orgint.Initial, objEntity);
                            //    if (accdetail.OwnerName == "CTPL")
                            //    {
                            //        accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "CTPL", objEntity);
                            //    }
                            //    else
                            //    {
                            //        accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "CCPL", objEntity);
                            //    }
                            }
                            else
                            {
                                accdetail.CDGIClaimRef = ClaimForCRTXInfoModel.GetPermanentCDGEClaimRefNo(claimdetail.AccidentClaimId, Convert.ToString(accdetail.Organization), "PTE", objEntity);
                            }
                        }
                        this.Confirmamtbd = confirmamtmodel;
                        CreateNSaveConfirmAmtBreakdown(objEntity, confirmamtmodel);
                        this.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.CreatedOn = DateTime.Now;
                        this.PolicyId = 0;
                        this.ResultMessage = "Record saved successfully.";
                    }

                    this.PolicyId = 0;
                    objEntity.SaveChanges();
                    transaction.Commit();
                    objEntity.Dispose();
                    this._claimsCollection = this.FetchClaimList(this.AccidentClaimId);
                    return this;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    objEntity.Dispose();
                    throw (ex);
                }
            }
        }

        public void InsertAccidentHistoryDeials(int AccidentClaimId, int? ClaimID, string ClaimantStatus, string CreatedBy, string Remarks)
        {

            MCASEntities obj = new MCASEntities();
            obj.Proc_InsertAccidentHistoryDetails(AccidentClaimId, ClaimID, ClaimantStatus, CreatedBy, Remarks);
            obj.Dispose();

        }


        public ClaimForCRTXInfoModel FetchClaim( ClaimForCRTXInfoModel model,string ClaimID, int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            int Cid = Convert.ToInt32(ClaimID);
            var createdBy = System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString();
           
            try
            {
               
                if (ClaimID != null)
                {
                    var list = (from l in db.CLM_Claims where l.ClaimID == Cid select l).FirstOrDefault();
                    if (list != null)
                    {
                        model = new ClaimForCRTXInfoModel(list.AccidentClaimId);
                        DataMapper.Map(list, model, true);
                    }
                }
                ClaimMandateModel m2 = new ClaimMandateModel();
                //model.Hdate = (from l in db.ClaimAccidentDetails where l.AccidentClaimId == AccidentClaimId select l.AccidentDate).FirstOrDefault();
                var clmID = 0;
                if (ClaimID != null)
                {
                    clmID = Int32.Parse(ClaimID);
                    var claimDtls = (from l in db.CLM_Claims where l.ClaimID == clmID select l).FirstOrDefault();
                    if (claimDtls != null)
                    {
                        model.CreatedBy = claimDtls.CreatedBy;
                        model.CreatedOn = Convert.ToDateTime(claimDtls.CreatedDate);
                        model.CreatedDate = Convert.ToDateTime(claimDtls.CreatedDate);
                        if (claimDtls.ModifiedDate != null || claimDtls.ModifiedDate.ToString() != "")
                        {
                            model.ModifiedOn = Convert.ToDateTime(claimDtls.ModifiedDate);
                            model.ModifiedBy = claimDtls.ModifiedBy;
                        }

                        model.GST = claimDtls.Gst;
                        model.DriversLiability = claimDtls.DriverLiablity;
                        model.AdminSupport = int.Parse(claimDtls.AdminSupport);
                        model.InvoiceDate = claimDtls.Invoicedate;
                        model.OfficeInCharge = claimDtls.ClaimsOfficer;
                        model.WriteIssued = claimDtls.WritIssuedDate;
                        model.WriteNo = claimDtls.WritNo;
                        model.OurSurveyorApp = claimDtls.SurveyorAppointed;
                        model.NoOfDaysForRepairs = (int)claimDtls.NoOfDaysForRepairs;

                        var cnfmamtlist = (from l in db.Clm_ConfirmAmtBreakdown where l.AccidentClaimId == AccidentClaimId && l.ClaimId == Cid select l).FirstOrDefault();
                        if (cnfmamtlist != null)
                        {
                            model.Confirmamtbd = FetchConfirmAmtBreakdown(AccidentClaimId, clmID);
                        }
                    }

                }

                model.ClaimOfficerList = FetchClaimOfficer("CO");
                //Impacted due to tfs # 21522
                //model.ClaimantStatusList = FetchLookUpList("ClaimantType");
                model.ClaimantStatusList = FetchCommonMasterData("ClaimantType", AccidentClaimId);
                //model.CaseStatusList = FetchLookUpList("CaseStatus");
                model.list = FetchSurveyor();
                model.ClaimOfficerList = FetchClaimOfficer("CO");
                model.AdminSupportList = FetchClaimOfficer("All");
                model.ClaimTypeList = m2.ClaimTypeValue("ClaimType");
                model.ClaimantStatusList = FetchLookUpList("ClaimantStatus");
                //Impacted due to tfs # 21522
                //model.CaseStatusList = FetchLookUpList("CaseStatus");
                //model.NatureAccList = FetchLookUpList("NatureOfAcc");
                //model.CDGEStatusList = FetchLookUpList("CDGEStatus");
                //model.CollisionTypeList = FetchLookUpList("CollisionType");
                model.CaseStatusList = FetchCommonMasterData("CaseStatus", AccidentClaimId);
                model.NatureAccList = FetchCommonMasterData("NatureOfAcc", AccidentClaimId);
                model.CDGEStatusList = FetchCommonMasterData("CDGEStatus", AccidentClaimId);
                model.CollisionTypeList = FetchCommonMasterData("CollisionType", AccidentClaimId);
                model.SettlementTypeList = FetchCommonMasterData("SettlementType", AccidentClaimId, true);
                model.TPInsurerList = FetchTPInsurer();
                
                
                return model;
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

        public void CreateNSaveConfirmAmtBreakdown(MCASEntities obj, ConfirmAmtModel confirmamtmodel)
        {
            Clm_ConfirmAmtBreakdown clmconfirmamt = new Clm_ConfirmAmtBreakdown();
            DataMapper.Map(this.Confirmamtbd, clmconfirmamt, true);
            clmconfirmamt.AccidentClaimId = this.AccidentClaimId;
            clmconfirmamt.ClaimId = (int)this.ClaimID;
            clmconfirmamt.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
            clmconfirmamt.CreatedDate = DateTime.Now;
            obj.Clm_ConfirmAmtBreakdown.AddObject(clmconfirmamt);
        }

        public void UpdateNSaveConfirmAmtBreakdown(MCASEntities obj, ConfirmAmtModel confirmamtmodel)
        {
            var clmconfrimamountdlt = obj.Clm_ConfirmAmtBreakdown.Where(x => x.AccidentClaimId == this.AccidentClaimId && x.ClaimId == this.ClaimID).FirstOrDefault();
            this.Confirmamtbd.AccidentClaimId = clmconfrimamountdlt.AccidentClaimId;
            this.Confirmamtbd.ClaimId = clmconfrimamountdlt.ClaimId;
            this.Confirmamtbd.ConfirmAmtId = clmconfrimamountdlt.ConfirmAmtId;
            DataMapper.Map(this.Confirmamtbd, clmconfrimamountdlt, true);
        }

        public ConfirmAmtModel FetchConfirmAmtBreakdown(int AccidentClaimId, int ClaimID)
        {
            MCASEntities obj = new MCASEntities();
            ConfirmAmtModel model = new ConfirmAmtModel();
            var confimamtdata = obj.Clm_ConfirmAmtBreakdown.Where(x => x.AccidentClaimId == AccidentClaimId && x.ClaimId == ClaimID).FirstOrDefault();
            DataMapper.Map(confimamtdata, model, true);
            return model;
        }

        //Add New Method for TFS 21522
        public static List<ClaimantType> FetchCommonMasterData(string category, int accclmid, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> list = new List<ClaimantType>();
            var clmorgId = (from l in obj.ClaimAccidentDetails where l.AccidentClaimId == accclmid select l.Organization).FirstOrDefault();
            var lookupcodelist = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" orderby l.Lookupdesc select l.LookupID).ToList();
            foreach (decimal ids in lookupcodelist)
            {
                string org = (from l in obj.MNT_Lookups where l.LookupID == ids select l.lookupCode).FirstOrDefault();
                if (org != null)
                {
                    List<string> orglist = org.Split(',').ToList();
                    foreach (string s in orglist)
                    {
                        if (s == Convert.ToString(clmorgId))
                        {
                            var lookupinfo = (from l in obj.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.LookupID == ids orderby l.Lookupdesc select new ClaimantType { Id = l.Lookupvalue, Text = l.Lookupdesc }).FirstOrDefault();
                            list.Add(lookupinfo);
                        }
                    }
                }
            }
            list.Insert(0, new ClaimantType() { Id = "", Text = "[Select...]" });
            obj.Dispose();
            return list;
        }



    }

    public class ClaimCrTxCollection
    {

        #region "Object Properties"
        public string RecordNumber { get; set; }
        public string ClaimRecordNo { get; set; }
        public Int64? OrgRecordNumber { get; set; }
        public int? ClaimID { get; set; }
        public int? ClaimType { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public int ClaimantNameId { get; set; }
        public string ClaimantName { get; set; }
        public string ClaimantType { get; set; }
        public string TPVehicleNo { get; set; }
        public int PartyTypeId { get; set; }
        public int CompanyNameId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public List<string> HeaderListCollection { get; set; }
        #endregion

        #region "Public Add New"
        
        #endregion

        #region "Methods"
        
        #endregion

    }

    //public class ClaimEntryInfoCrTxModel: BaseModel
    //{
    //    public int? AccidentClaimId { get; set; }
    //    public int? ClaimID { get; set; }
    //    public int? PolicyId { get; set; }
    //    public string ClaimType { get; set; }
    //    public string InvoiceNo { get; set; }
    //    public string JobNo { get; set; }
    //    public string ClaimStatus { get; set; }
    //    public int ClaimType { get; set; }
    //    public ClaimEntryInfoCrTxModel Fetch()
    //    {
    //        MCASEntities obj = new MCASEntities();
    //        var item = new ClaimEntryInfoCrTxModel() { ViewMode = this.ViewMode };
    //        if (AccidentClaimId.HasValue)// && ClaimID.HasValue)
    //        {
    //            var Claim = (from clm in obj.CLM_Claims where clm.AccidentClaimId == this.AccidentClaimId select clm).FirstOrDefault();
    //            if (Claim != null)
    //            {
    //                // var ClaimType1 = Claim.ClaimType == 1 ? "OD" : Claim.ClaimType == 2 ? "PD" : "BI";
    //                item.AccidentClaimId = this.AccidentClaimId;
    //                item.PolicyId = this.PolicyId;
    //                item.ClaimID = Claim.ClaimID;
    //                item.ClaimType = Claim.ClaimType;
    //                item.InvoiceNo = Claim.InvoiceNo;
    //                item.JobNo = Claim.JobNo;
    //                item.CultureCode = Claim.CustomerCode;
    //                item. = Claim.ClaimStatus;
                    
                   

    //            }
    //        }
    //        obj.Dispose();
    //        return item;
    //    }
    //}


    public class ConfirmAmtModel : BaseModel
    {
        #region "Properties"
        public int ConfirmAmtId { get; set; }
        public int AccidentClaimId { get; set; }
        public int ClaimId { get; set; }
        public decimal ConfirmInvoiceAmt { get; set; }
        public decimal LossOfIncome { get; set; }
        public decimal LossofRental { get; set; }
        public decimal Others1 { get; set; }
        public decimal LegalFee { get; set; }
        public decimal TPGIAFee { get; set; }
        public decimal SurveyFee { get; set; }
        public decimal Others2 { get; set; }
        public decimal LTAFee { get; set; }
        public decimal LossOfUse { get; set; }
        public decimal MedicalExp { get; set; }
        public decimal CarRental { get; set; }
        public decimal CourtesyCar { get; set; }
        public decimal Total { get; set; }
        #endregion
    }

}
