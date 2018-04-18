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
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Web;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Globalization;
using MCAS.Web.Objects.Resources.Common;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimInfoPayment : BaseModel
    {
        #region Properties

        #region PrivateProperties
        private DateTime? _createddate = null;
        private DateTime? _Sodifieddate = null;
        private DateTime? _paymentRequestDate = null;
        private DateTime? _paymentDueDate = null;
        public DateTime? _approvedDate = null;
        public DateTime? _createddate1 = null;
        public int _prop1 { get; set; }
        private List<ClaimPaymentModelCollection> _claimInfoPayment;
        #endregion

        #region  PublicProperties
        public List<ClaimPaymentModelCollection> ClaimPaymentModelCollection
        {
            get { return _claimInfoPayment; }
        }
        public ClaimInfoPayment()
        {
            this._claimInfoPayment = FetchPaymentlist(0);
        }
        public ClaimInfoPayment(int AccidentId)
        {
            this._claimInfoPayment = FetchPaymentlist(AccidentId);
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
        public int PaymentId { get; set; }
        public int AccidentClaimId { get; set; }
        public int PolicyId { get; set; }
        public Int64? SNO { get; set; }
        public Int64? SerialNo { get; set; }
        public decimal? Total_D { get; set; }
        public decimal? Total_S { get; set; }
        public decimal? Total_I { get; set; }
        public decimal? Total_R { get; set; }
        public decimal? Total_O { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime? Createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Approved Date")]
        public DateTime? ApprovedDate
        {
            get { return _approvedDate; }
            set { _approvedDate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified Date")]
        public DateTime? Modifieddate
        {
            get { return _Sodifieddate; }
            set { _Sodifieddate = value; }
        }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Request Date")]

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment), ErrorMessageResourceName = "RFVPaymentRequestDate")]
        public DateTime? PaymentRequestDate
        {
            get { return _paymentRequestDate; }
            set { _paymentRequestDate = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Due Date")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment), ErrorMessageResourceName = "RFVPaymentDueDate")]
        public DateTime? PaymentDueDate
        {
            get { return _paymentDueDate; }
            set { _paymentDueDate = value; }
        }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime? Createddate1
        {
            get { return _createddate1; }
            set { _createddate1 = value; }
        }

        [Display(Name = "Payee")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment), ErrorMessageResourceName = "RFVPayee")]
        public string Payee { get; set; }
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment), ErrorMessageResourceName = "RFVAssignedTo")]
        public string AssignedTo { get; set; }

        public string AssignedToSupervisor { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment), ErrorMessageResourceName = "RFVClaimantName")]
        public string ClaimantName { get; set; }
        public string ClaimRecordNo { get; set; }
        public string PaymentRequestType { get; set; }
        public string Createdby1 { get; set; }
        public string Modifiedby1 { get; set; }
        public string IsActive { get; set; }
        public int ClaimID { get; set; }
        public int ReserveId { get; set; }
        public int MandateId { get; set; }
        public int ClaimType { get; set; }
        public string HshowHideSaveButton { get; set; }
        public string HDeductibleAmt { get; set; }
        public string HFALAmtOD { get; set; }
        public string HFALAmtPDBI { get; set; }
        public string HTotalPreviousApprovePayment { get; set; }
        public string Hshowgrid { get; set; }
        public string HGridImageSign { get; set; }
        public string HGroupCode { get; set; }
        public string HRoleCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment), ErrorMessageResourceName = "RFVPayeeAddress1")]
        [Display(Name = "Payee Address1")]
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment), ErrorMessageResourceName = "RFVPostalCode")]
        [Display(Name = "Postal Code")]
        public string PostalCodes { get; set; }
        public string CoRemarks { get; set; }
        public string ApprovePayment { get; set; }
        [Display(Name = "Inform Safety to review findings")]
        public string InformSafetytoreviewfindings { get; set; }
        public DateTime? DateofNoticetoSafety { get; set; }
        public string SupervisorRemarks { get; set; }
        public string Noofdays_D { get; set; }
        public string Noofdays_S { get; set; }
        public string userid { get; set; }
        public string MandateRecordNo { get; set; }
        public decimal? SPFAL { get; set; }
        public int HUserId { get; set; }
        public DateTime? HEffdatefrom { get; set; }
        public DateTime? HEffdateto { get; set; }
        public string HOrgname { get; set; }
        public string HChildGrid { get; set; }
        public string RecoverableFromInsurerBI { get; set; }
        public string EZLinkCardNo { get; set; }
        public string ODStatus { get; set; }
        public List<LookUpListItems> InformSafetytoreviewfindingsList { get; set; }
        public List<LookUpListItems> EZLinkCardNolist { get; set; }
        public List<LookUpListItems> RecoverableFromInsurerBIList { get; set; }
        public List<LookUpListItems> ApprovePaymentList { get; set; }
        public List<CommonUtilities.CommonType> PayeeList { get; set; }
        public List<CommonUtilities.CommonType> AssignedToSupervisorList { get; set; }
        public List<CommonUtilities.CommonType> AssignedToCoList { get; set; }
        private List<ClaimPaymentDetails> _paymentDetails = new List<ClaimPaymentDetails>();
        public IEnumerable<SelectListItem> generallookupvalue { get; set; }

        public List<ClaimPaymentDetails> PaymentDetails
        {
            get { return _paymentDetails.ToList<ClaimPaymentDetails>(); }
            set
            {
                _paymentDetails.AddRange(value);
            }
        }

        public string ResultMessage { get; set; }
        #endregion

        #region  OverrideProperties
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

        #endregion

        #region Methods

        #region StaticMethod
        private static IList<string> GetPropertyNames(Type sourceType)
        {
            List<string> result = new List<string>();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(sourceType);
            foreach (PropertyDescriptor item in props)
                if (item.IsBrowsable)
                    result.Add(item.Name);
            return result;
        }
        public static List<CommonUtilities.CommonType> FetchPayeeList(string Acc, string Claimid)
        {
            MCASEntities obj = new MCASEntities();
            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            list.Insert(0, new CommonUtilities.CommonType() { Text = "[Select...]" });
            var ServiceProviderNamelist = obj.Proc_GetCLM_ServiceProviderNameList(Acc, Claimid).ToList();
            int i = 0;
            foreach (var l in ServiceProviderNamelist)
            {
                if (!string.IsNullOrEmpty(l.ClaimID) && !string.IsNullOrEmpty(l.ClaimantName) && i == 0)
                {
                    i = i + 1;
                    list.Add(new CommonUtilities.CommonType()
                    {
                        Id = l.ClaimID,
                        Text = l.ClaimantName//CultureInfo.CurrentCulture.TextInfo.ToTitleCase(l.ClaimantName.Trim())
                    });
                }
                if (!string.IsNullOrEmpty(l.ServiceProviderId) && !string.IsNullOrEmpty(l.cedent_name))
                {

                    list.Add(new CommonUtilities.CommonType()
                    {
                        Id = l.ServiceProviderId,
                        Text = l.cedent_name //CultureInfo.CurrentCulture.TextInfo.ToTitleCase(l.cedent_name.Trim())
                    });
                }

            }
            obj.Dispose();
            return list;
        }
        public static List<ClaimInfoPayment> Fetchall(string resid, string AccidentClaimId)
        {
            MCASEntities _db = new MCASEntities();
            var items = new List<ClaimInfoPayment>();
            var list = _db.Proc_GetCLM_Payment_ClaimTypeList(resid, AccidentClaimId).ToList();
            items = (from n in list
                     select new ClaimInfoPayment()
                     {
                         SerialNo = n.SerialNo,
                         SNO = n.SNO,
                         ClaimantName = n.ClaimantName,
                         Createdby1 = n.Createdby,
                         Modifiedby1 = n.Modifiedby,
                         Createddate1 = n.Createddate,
                         Modifieddate = n.Modifieddate,
                         Total_I = n.Total_S,
                         Total_R = n.Total_D,
                         Total_O = n.Total_O,
                         ClaimRecordNo = n.ClaimRecordNo,
                         ApprovePayment = n.ApprovePayment == "N" ? "Rejected" : "Approved"

                     }).ToList();
            _db.Dispose();
            return items;
        }

        public static List<ServiceProviderModel> GetPayeeDetailFrmServiceprovider(string payee)
        {
            int p = Convert.ToInt32(payee.Split('-')[1]);
            MCASEntities _db = new MCASEntities();
            var list = payee.Split('-')[0] == "S" ?
                (from data in _db.CLM_ServiceProvider where data.ServiceProviderId == p select new ServiceProviderModel() { Address1 = data.Address1, Address2 = data.Address2, Address3 = data.Address3, PostalCode = data.PostalCode }).ToList()
                :
                (from data in _db.CLM_Claims where data.ClaimID == p select new ServiceProviderModel() { Address1 = data.ClaimantAddress1, Address2 = data.ClaimantAddress2, Address3 = data.ClaimantAddress3, PostalCode = data.PostalCode }).ToList();
            _db.Dispose();
            return list;
        }
        public static List<CommonUtilities.CommonType> FetchAssignToList(string RoleCode)
        {
            MCASEntities obj = new MCASEntities();
            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            list.Insert(0, new CommonUtilities.CommonType() { Text = "[Select...]" });
            var ClaimOfficerDetails = obj.Proc_GetClaimOfficerByRole(RoleCode).ToList();
            var list1 = (from n in ClaimOfficerDetails
                         select new CommonUtilities.CommonType()
                         {
                             intID = n.SNo,
                             Text = n.UserDispName.Trim()
                         }
                        ).ToList();

            list.AddRange(list1);
            obj.Dispose();
            return list;
        }
        public static List<ClaimPaymentModelCollection> FetchPaymentlist(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var items = new List<ClaimPaymentModelCollection>();
            try
            {
                items = (from data in db.Proc_GetPaymentsListByAccId(AccidentClaimId).ToList()
                         select new ClaimPaymentModelCollection()
                         {
                             SNO = string.IsNullOrEmpty(data.MandateRecordNo) ? Convert.ToInt32(data.MandateRecordNo) : Convert.ToInt32(data.MandateRecordNo.Split('-')[1]),
                             UniqueId = data.uniq,
                             RecordNumber = data.RecordNumber,
                             MandateId = data.MandateId,
                             PaymentId = data.PaymentId,
                             ReserveId = data.ReserveId,
                             PolicyId = data.PolicyId,
                             TotalPaymentDue = data.ApprovePayment == "N" ? "" : data.TotalPaymentDue.ToString(),
                             ClaimID = data.ClaimID,
                             AccidentClaimId = data.AccidentClaimId,
                             MandateRecordNo = data.MandateRecordNo,
                             ApproveRecommedations = data.ApproveRecommedations,
                             PaymentRecordNo = data.PaymentRecordNo,
                             ClaimType = data.ClaimType,
                             ClaimantName = data.ClaimantName,
                             ClaimRecordNo = data.ClaimRecordNo,
                             ApprovePayment = data.ApprovePayment == "Y" ? "Approved" : data.ApprovePayment == "N" ? "Rejected" : "Pending",
                             ApprovedDate = data.ApprovedDate == null ? "" : data.ApprovedDate.Value.ToString().Substring(0, 10),
                             PayeeName = data.PayeeName,
                             ClaimTypeCode = data.ClaimTypeCode,
                             ClaimTypeDesc = data.ClaimType.ToString() == "1" ? Common._1 : data.ClaimType.ToString() == "2" ? Common._2 : data.ClaimType.ToString() == "3" ? Common._3 : "",
                             mode = (from x in db.ClaimAccidentDetails where x.AccidentClaimId == data.AccidentClaimId select x.IsComplete).FirstOrDefault() == 2 ? "Adj" : "",
                             HClaimantStatus = data.ClaimantStatus,
                             HAssignedTo = Convert.ToString((from m in db.CLM_PaymentSummary where m.PaymentId == data.PaymentId select m.AssignedTo).FirstOrDefault())
                         }
                        ).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return items;
        }
        #endregion

        #region PublicMethods
        //public ClaimInfoPayment Save()
        //{
        //    MCASEntities obj = new MCASEntities();
        //    EntityConnection entityConn = (EntityConnection)obj.Connection;
        //    CLM_PaymentSummary paysummary = new CLM_PaymentSummary();
        //    CLM_PaymentDetails paydetails = new CLM_PaymentDetails();
        //    if (obj.Connection.State == System.Data.ConnectionState.Closed)
        //        obj.Connection.Open();
        //    using (System.Data.Common.DbTransaction transaction = obj.Connection.BeginTransaction())
        //    {
        //        try
        //        {
        //            paysummary = (from l in obj.CLM_PaymentSummary where l.PaymentId == this.PaymentId select l).FirstOrDefault();
        //            if (paysummary == null)
        //            {
        //                #region InsertIntoPaymentSummary
        //                paysummary = new CLM_PaymentSummary();
        //                this.IsActive = "Y";
        //                DataMapper.Map(this, paysummary, true);
        //                paysummary.TotalAmountMandate = (from l in this.PaymentDetails where l.CompCode.Contains("TOTAL") select l.Mandate).FirstOrDefault();
        //                paysummary.TotalPaymentDue = (from l in this.PaymentDetails where l.CompCode.Contains("TOTAL") select l.PaymentDue).FirstOrDefault();
        //                paysummary.Createddate = DateTime.Now;
        //                paysummary.MandateRecord = this.MandateRecordNo;
        //                if (this.ApprovePayment == "Y")
        //                {
        //                    paysummary.ApprovedDate = DateTime.Now;
        //                }
        //                paysummary.CreatedBy = this.CreatedBy;
        //                paysummary.MovementType = "P";
        //                paysummary.PaymentRecordNo = "PMT-" + ((from mr in obj.CLM_PaymentSummary where mr.AccidentClaimId == paysummary.AccidentClaimId && mr.ClaimID == paysummary.ClaimID && mr.ClaimType == paysummary.ClaimType && mr.MandateRecord == this.MandateRecordNo select mr).Count() + 1);
        //                obj.CLM_PaymentSummary.AddObject(paysummary);
        //                obj.SaveChanges();
        //                this.PaymentId = paysummary.PaymentId;
        //                #endregion
        //                #region InsertIntoPaymentDetails
        //                for (var i = 0; i < this.PaymentDetails.Count; i++)
        //                {
        //                    paydetails = new CLM_PaymentDetails();
        //                    paydetails.CmpCode = this.PaymentDetails[i].CompCode;
        //                    paydetails.TotalPaymentDue = this.PaymentDetails[i].PaymentDue;
        //                    paydetails.TotalAmountMandate = this.PaymentDetails[i].Mandate;
        //                    paydetails.AccidentClaimId = this.PaymentDetails[i].AccidentClaimId;
        //                    paydetails.ReserveId = this.PaymentDetails[i].ReserveId;
        //                    paydetails.MandateId = this.PaymentDetails[i].MandateId;
        //                    paydetails.PaymentId = this.PaymentId;
        //                    paydetails.ClaimId = this.ClaimID;
        //                    paydetails.Createdby = this.CreatedBy;
        //                    paydetails.Createddate = DateTime.Now;
        //                    paydetails.IsActive = "Y";
        //                    obj.CLM_PaymentDetails.AddObject(paydetails);
        //                    obj.SaveChanges();
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                #region UpdatePaymentSummary
        //                var crBy = paysummary.CreatedBy;
        //                var crDt = paysummary.Createddate;
        //                string[] Ignorelist = { "IsActive", "Createdby", "Createddate", "MandateRecord" };
        //                DataMapper.Map(this, paysummary, true, Ignorelist);
        //                paysummary.CreatedBy = crBy;
        //                paysummary.Createddate = crDt;
        //                paysummary.ApprovedDate = DateTime.Now;
        //                paysummary.TotalAmountMandate = (from l in this.PaymentDetails where l.CompCode.Contains("TOTAL") select l.Mandate).FirstOrDefault();
        //                paysummary.TotalPaymentDue = (from l in this.PaymentDetails where l.CompCode.Contains("TOTAL") select l.PaymentDue).FirstOrDefault();
        //                paysummary.Modifiedby = this.CreatedBy;
        //                paysummary.Modifieddate = DateTime.Now;
        //                obj.SaveChanges();
        //                this.ModifiedBy = this.CreatedBy;
        //                this.ModifiedOn = DateTime.Now;
        //                this.CreatedBy = crBy;
        //                this.CreatedOn = Convert.ToDateTime(crDt);
        //                #endregion
        //                #region UpdatePaymentSummary
        //                for (var i = 0; i < this.PaymentDetails.Count; i++)
        //                {
        //                    var code = this.PaymentDetails[i].CompCode;
        //                    paydetails = (from l in obj.CLM_PaymentDetails where l.PaymentId == this.PaymentId && l.CmpCode == code select l).FirstOrDefault();
        //                    paydetails.CmpCode = this.PaymentDetails[i].CompCode;
        //                    paydetails.TotalPaymentDue = this.PaymentDetails[i].PaymentDue;
        //                    paydetails.TotalAmountMandate = this.PaymentDetails[i].Mandate;
        //                    paydetails.AccidentClaimId = this.PaymentDetails[i].AccidentClaimId;
        //                    paydetails.ReserveId = this.PaymentDetails[i].ReserveId;
        //                    paydetails.MandateId = this.PaymentDetails[i].MandateId;
        //                    paydetails.PaymentId = this.PaymentId;
        //                    paydetails.ClaimId = this.ClaimID;
        //                    paydetails.Modifiedby = this.CreatedBy;
        //                    paydetails.Modifieddate = DateTime.Now;
        //                    obj.SaveChanges();
        //                }
        //                #endregion
        //            }

        //            if (this.ApprovePayment == "Y")
        //            {
        //                #region InsertInToReserveAndMandate
        //                obj.Proc_Insert_Claim_Payment(this.PaymentId, this.AccidentClaimId, this.PolicyId, this.Payee, this.AssignedToSupervisor, (from l in this.PaymentDetails where l.CompCode == "TOTAL" select l.PaymentDue).FirstOrDefault(), (from l in this.PaymentDetails where l.CompCode == "TOTAL" select l.Mandate).FirstOrDefault(), DateTime.Now, this.PaymentRequestDate, this.PaymentDueDate, this.CreatedBy, this.AssignedTo, this.ClaimantName, this.ClaimType, "Y", this.ClaimID, this.Address, this.Address1 == null ? "" : this.Address1, this.Address2 == null ? "" : this.Address2, this.PostalCodes == null ? "" : this.PostalCodes, this.CoRemarks == null ? "" : this.CoRemarks, this.ApprovePayment == null ? "" : this.ApprovePayment, this.SupervisorRemarks == null ? "" : this.SupervisorRemarks, this.ApprovePayment == "Y" ? DateTime.Now : this.ApprovedDate, "P", this.MandateId, this.ReserveId);
        //                #endregion
        //            }

        //            transaction.Commit();
        //        }

        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw (ex);
        //        }
        //        finally
        //        {
        //            obj.Dispose();
        //        }
        //        this._claimInfoPayment = FetchPaymentlist(this.AccidentClaimId);
        //        return this;
        //    }
        //}

        public ClaimInfoPayment Save()
        {
            CommonUtilities objCommonUtilities = new CommonUtilities();
            MCASEntities obj = new MCASEntities();
            if (obj.Connection.State == System.Data.ConnectionState.Closed)
                obj.Connection.Open();

            EntityConnection entityConn = (EntityConnection)obj.Connection;
            CLM_PaymentSummary paysummary = new CLM_PaymentSummary();
            CLM_PaymentSummary oldSummary = new CLM_PaymentSummary();
            CLM_PaymentDetails paydetails = new CLM_PaymentDetails();
            List<CLM_PaymentDetails> lstPaymentDetails = new List<CLM_PaymentDetails>();
            DataTable tablePaymentSummary = new DataTable();
            DataTable tablePaymentDetails = new DataTable();
            int returnValue = 0;
            string XMLDetails = "";
            string ActionSummary = MCASEntities.AuditActions.I.ToString();
            string ActionDetails = MCASEntities.AuditActions.I.ToString();
            string[] checkListDtails = { "TotalPaymentDue", "TotalAmountMandate" };
            try
            {
                paysummary = (from l in obj.CLM_PaymentSummary where l.PaymentId == this.PaymentId select l).FirstOrDefault();
                if (paysummary == null)
                {
                    #region InsertIntoPaymentSummary
                    paysummary = new CLM_PaymentSummary();
                    this.IsActive = "Y";
                    DataMapper.Map(this, paysummary, true);

                    var objPaymentDetails = (from l in this.PaymentDetails where l.CompCode.Contains("TOTAL") select l).FirstOrDefault();
                    if (objPaymentDetails != null)
                    {
                        paysummary.TotalAmountMandate = objPaymentDetails.Mandate;
                        paysummary.TotalPaymentDue = objPaymentDetails.PaymentDue;
                    }
                    paysummary.Createddate = DateTime.Now;
                    paysummary.MandateRecord = this.MandateRecordNo;
                    //Note: This has been handled in Procedure.
                    //if (this.ApprovePayment == "Y")
                    //{
                    paysummary.ApprovedDate = DateTime.Now;
                    //}
                    paysummary.CreatedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    paysummary.MovementType = "P";
                    paysummary.PaymentRecordNo = "PMT-" + ((from mr in obj.CLM_PaymentSummary where mr.AccidentClaimId == paysummary.AccidentClaimId && mr.ClaimID == paysummary.ClaimID && mr.ClaimType == paysummary.ClaimType && mr.MandateRecord == this.MandateRecordNo select mr).Count() + 1);
                    //obj.CLM_PaymentSummary.AddObject(paysummary);
                    //obj.SaveChanges();
                    //this.PaymentId = paysummary.PaymentId;
                    this.ResultMessage = "Record saved successfully.";

                    #endregion
                    #region InsertIntoPaymentDetails
                    for (var i = 0; i < this.PaymentDetails.Count; i++)
                    {
                        paydetails = new CLM_PaymentDetails();
                        paydetails.CmpCode = this.PaymentDetails[i].CompCode;
                        paydetails.TotalPaymentDue = this.PaymentDetails[i].PaymentDue;
                        paydetails.TotalAmountMandate = this.PaymentDetails[i].Mandate;
                        paydetails.AccidentClaimId = this.PaymentDetails[i].AccidentClaimId;
                        paydetails.ReserveId = this.PaymentDetails[i].ReserveId;
                        paydetails.MandateId = this.PaymentDetails[i].MandateId;
                        paydetails.PaymentId = this.PaymentId;
                        paydetails.ClaimId = this.ClaimID;
                        paydetails.Createdby = this.CreatedBy;
                        paydetails.Createddate = DateTime.Now;
                        paydetails.IsActive = "Y";
                        this.ResultMessage = "Record saved successfully.";
                        //obj.CLM_PaymentDetails.AddObject(paydetails);
                        //obj.SaveChanges();

                        lstPaymentDetails.Add(paydetails);

                        string tempXML = objCommonUtilities.GenerateXMLForChangedColumns(null, paydetails, checkListDtails);
                        if (!string.IsNullOrEmpty(tempXML))
                        {
                            XMLDetails += "<Row Id=\"" + paydetails.CmpCode.Trim() + "\">" + tempXML;
                            XMLDetails += "</Row>";
                        }
                    }
                    #endregion
                }
                else
                {
                    ActionSummary = MCASEntities.AuditActions.U.ToString();
                    ActionDetails = MCASEntities.AuditActions.U.ToString();
                    oldSummary = paysummary.ShallowCopy();

                    #region UpdatePaymentSummary

                    string[] Ignorelist = { "IsActive", "Createdby", "Createddate", "MandateRecord" };
                    DataMapper.Map(this, paysummary, true, Ignorelist);
                    paysummary.ApprovedDate = DateTime.Now;
                    var objPaymentDetails = (from l in this.PaymentDetails where l.CompCode.Contains("TOTAL") select l).FirstOrDefault();
                    if (objPaymentDetails != null)
                    {
                        paysummary.TotalAmountMandate = objPaymentDetails.Mandate;
                        paysummary.TotalPaymentDue = objPaymentDetails.PaymentDue;
                    }
                    paysummary.Modifiedby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    paysummary.Modifieddate = DateTime.Now;
                    //obj.SaveChanges();
                    this.ModifiedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    this.ModifiedOn = DateTime.Now;
                    this.CreatedBy = paysummary.CreatedBy;
                    this.CreatedOn = Convert.ToDateTime(paysummary.Createddate);
                    this.ResultMessage = "Record Updated successfully.";

                    #endregion
                    #region UpdatePaymentSummary
                    for (var i = 0; i < this.PaymentDetails.Count; i++)
                    {
                        var code = this.PaymentDetails[i].CompCode;
                        paydetails = (from l in obj.CLM_PaymentDetails where l.PaymentId == this.PaymentId && l.CmpCode == code select l).FirstOrDefault();
                        var oldDetails = paydetails.ShallowCopy();
                        paydetails.CmpCode = this.PaymentDetails[i].CompCode;
                        paydetails.TotalPaymentDue = this.PaymentDetails[i].PaymentDue;
                        paydetails.TotalAmountMandate = this.PaymentDetails[i].Mandate;
                        paydetails.AccidentClaimId = this.PaymentDetails[i].AccidentClaimId;
                        paydetails.ReserveId = this.PaymentDetails[i].ReserveId;
                        paydetails.MandateId = this.PaymentDetails[i].MandateId;
                        paydetails.PaymentId = this.PaymentId;
                        paydetails.ClaimId = this.ClaimID;
                        paydetails.Modifiedby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                        paydetails.Modifieddate = DateTime.Now;
                        //obj.SaveChanges();
                        this.ResultMessage = "Record Updated successfully.";
                        lstPaymentDetails.Add(paydetails);

                        string tempXML = objCommonUtilities.GenerateXMLForChangedColumns(oldDetails, paydetails, checkListDtails);
                        if (!string.IsNullOrEmpty(tempXML))
                        {
                            XMLDetails += "<Row Id=\"" + paydetails.CmpCode.Trim() + "\">" + tempXML;
                            XMLDetails += "</Row>";
                        }
                    }
                    #endregion
                }

                string[] ignoreListSummary = new string[] { };
                tablePaymentSummary = objCommonUtilities.CreateDataTable(paysummary, ignoreListSummary);

                string[] ignoreListDetails = new string[] { };
                tablePaymentDetails = objCommonUtilities.CreateDataTable(lstPaymentDetails, ignoreListDetails);

                XMLDetails = "<ChangeXml>" + XMLDetails + "</ChangeXml>";
                string XMLSummary = "<ChangeXml>" + objCommonUtilities.GenerateXMLForChangedColumns(oldSummary, paysummary, null) + "</ChangeXml>";

                //Save/Update Mandate
                var parameterSummary = new SqlParameter("@PaymentSummary", SqlDbType.Structured);
                parameterSummary.Value = tablePaymentSummary;
                parameterSummary.TypeName = "dbo.TVP_PaymentSummary";

                var parameterDetails = new SqlParameter("@PaymentDetailsList", SqlDbType.Structured);
                parameterDetails.Value = tablePaymentDetails;
                parameterDetails.TypeName = "dbo.TVP_PaymentDetails";

                object ret;
                obj.AddParameter(parameterSummary);
                obj.AddParameter(parameterDetails);
                obj.AddParameter("@XMLSummary", XMLSummary);
                obj.AddParameter("@XMLDetails", XMLDetails);
                obj.AddParameter("@ActionSummary", ActionSummary);
                obj.AddParameter("@ActionDetails", ActionDetails);
                obj.ExecuteNonQuery("Proc_SavePayment", CommandType.StoredProcedure, out ret);
                returnValue = Convert.ToInt32(ret);

                if (returnValue > 0)
                    this.PaymentId = returnValue;
                else
                {
                    throw new Exception("Exception generated in procedure Prov_SavePayment.");
                }

                //obj.InsertTransactionAuditLog("CLM_PaymentSummary", "PaymentId", this.CreatedBy, this.ClaimID, this.AccidentClaimId, ActionSummary, XMLSummary);
                //obj.InsertTransactionAuditLog("CLM_PaymentDetails", "PaymentDetailID", this.CreatedBy, this.ClaimID, this.AccidentClaimId, ActionDetails, XMLDetails);

                //if (this.ApprovePayment == "Y")
                //{
                //    #region InsertInToReserveAndMandate
                //    obj.Proc_Insert_Claim_Payment(this.PaymentId, this.AccidentClaimId, this.PolicyId, this.Payee, this.AssignedToSupervisor, (from l in this.PaymentDetails where l.CompCode == "TOTAL" select l.PaymentDue).FirstOrDefault(), (from l in this.PaymentDetails where l.CompCode == "TOTAL" select l.Mandate).FirstOrDefault(), DateTime.Now, this.PaymentRequestDate, this.PaymentDueDate, this.CreatedBy, this.AssignedTo, this.ClaimantName, this.ClaimType, "Y", this.ClaimID, this.Address, this.Address1 == null ? "" : this.Address1, this.Address2 == null ? "" : this.Address2, this.PostalCodes == null ? "" : this.PostalCodes, this.CoRemarks == null ? "" : this.CoRemarks, this.ApprovePayment == null ? "" : this.ApprovePayment, this.SupervisorRemarks == null ? "" : this.SupervisorRemarks, this.ApprovePayment == "Y" ? DateTime.Now : this.ApprovedDate, "P", this.MandateId, this.ReserveId);
                //    #endregion
                //}

                this._claimInfoPayment = FetchPaymentlist(this.AccidentClaimId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }

            return this;

        }

        //public DataTable CreateDataTableForPaymentSummary(CLM_PaymentSummary paymentSummary)
        //{
        //    DataTable tablePaymentSummary = new DataTable();
        //    tablePaymentSummary.Columns.Add("PaymentId");
        //    tablePaymentSummary.Columns.Add("AccidentClaimId");
        //    tablePaymentSummary.Columns.Add("PolicyId");
        //    tablePaymentSummary.Columns.Add("Payee");
        //    tablePaymentSummary.Columns.Add("AssignedToSupervisor");
        //    tablePaymentSummary.Columns.Add("TotalPaymentDue");
        //    tablePaymentSummary.Columns.Add("TotalAmountMandate");
        //    tablePaymentSummary.Columns.Add("Createddate", typeof(DateTime));
        //    tablePaymentSummary.Columns.Add("Modifieddate", typeof(DateTime));
        //    tablePaymentSummary.Columns.Add("PaymentRequestDate", typeof(DateTime));
        //    tablePaymentSummary.Columns.Add("PaymentDueDate", typeof(DateTime));
        //    tablePaymentSummary.Columns.Add("CreatedBy");
        //    tablePaymentSummary.Columns.Add("Modifiedby");
        //    tablePaymentSummary.Columns.Add("AssignedTo");
        //    tablePaymentSummary.Columns.Add("ClaimantName");
        //    tablePaymentSummary.Columns.Add("PaymentRecordNo");
        //    tablePaymentSummary.Columns.Add("ClaimType");
        //    tablePaymentSummary.Columns.Add("IsActive");
        //    tablePaymentSummary.Columns.Add("ClaimID");
        //    tablePaymentSummary.Columns.Add("Address");
        //    tablePaymentSummary.Columns.Add("Address1");
        //    tablePaymentSummary.Columns.Add("Address2");
        //    tablePaymentSummary.Columns.Add("PostalCodes");
        //    tablePaymentSummary.Columns.Add("CoRemarks");
        //    tablePaymentSummary.Columns.Add("ApprovePayment");
        //    tablePaymentSummary.Columns.Add("SupervisorRemarks");
        //    tablePaymentSummary.Columns.Add("ApprovedDate", typeof(DateTime));
        //    tablePaymentSummary.Columns.Add("MovementType");
        //    tablePaymentSummary.Columns.Add("MandateId");
        //    tablePaymentSummary.Columns.Add("ReserveId");
        //    tablePaymentSummary.Columns.Add("MandateRecord");

        //    DataRow dr = tablePaymentSummary.NewRow();
        //    foreach (DataColumn dc in tablePaymentSummary.Columns)
        //    {
        //        dr[dc.ColumnName] = paymentSummary.GetType().GetProperty(dc.ColumnName).GetValue(paymentSummary, null);
        //    }
        //    tablePaymentSummary.Rows.Add(dr);

        //    return tablePaymentSummary;
        //}

        //public DataTable CreateDataTableForPaymentDetails(List<CLM_PaymentDetails> lstPaymentDetails)
        //{
        //    DataTable tablePaymentDetails = new DataTable();
        //    tablePaymentDetails.Columns.Add("PaymentDetailID");
        //    tablePaymentDetails.Columns.Add("CmpCode");
        //    tablePaymentDetails.Columns.Add("TotalPaymentDue");
        //    tablePaymentDetails.Columns.Add("TotalAmountMandate");
        //    tablePaymentDetails.Columns.Add("Createdby");
        //    tablePaymentDetails.Columns.Add("Createddate", typeof(DateTime));
        //    tablePaymentDetails.Columns.Add("Modifiedby");
        //    tablePaymentDetails.Columns.Add("Modifieddate", typeof(DateTime));
        //    tablePaymentDetails.Columns.Add("IsActive");
        //    tablePaymentDetails.Columns.Add("AccidentClaimId");
        //    tablePaymentDetails.Columns.Add("ReserveId");
        //    tablePaymentDetails.Columns.Add("MandateId");
        //    tablePaymentDetails.Columns.Add("PaymentId");
        //    tablePaymentDetails.Columns.Add("ClaimId");

        //    foreach (var paymentDetails in lstPaymentDetails)
        //    {
        //        DataRow dr = tablePaymentDetails.NewRow();
        //        foreach (DataColumn dc in tablePaymentDetails.Columns)
        //        {
        //            dr[dc.ColumnName] = paymentDetails.GetType().GetProperty(dc.ColumnName).GetValue(paymentDetails, null);
        //        }
        //        tablePaymentDetails.Rows.Add(dr);
        //    }

        //    return tablePaymentDetails;
        //}
        #endregion



        public ClaimInfoPayment FetchPayment(ClaimInfoPayment model, string Viewmode)
        {
            MCASEntities obj = new MCASEntities();
            var TransactonHistoryList = (from l in obj.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" select l.Description).ToList();
            if (Viewmode == "Select")
            {
                model.MandateId = Convert.ToInt32((from l in obj.CLM_PaymentSummary where l.PaymentId == model.PaymentId select l.MandateId).FirstOrDefault());
                model.ReserveId = Convert.ToInt32((from l in obj.CLM_PaymentSummary where l.PaymentId == model.PaymentId select l.ReserveId).FirstOrDefault());
            }
            model.PayeeList = FetchPayeeList(Convert.ToString(model.AccidentClaimId), Convert.ToString(model.ClaimID));

            var sno = Convert.ToInt32((from l in obj.CLM_Claims where l.AccidentClaimId == model.AccidentClaimId && l.ClaimID == model.ClaimID select l.ClaimsOfficer).FirstOrDefault());

            model.AssignedToSupervisorList = (from l in FetchAssignToList("CO") where l.intID == sno select l).FirstOrDefault() == null ? (from l in FetchAssignToList("CO") where l.intID == 0 select l).ToList() : (from l in FetchAssignToList("CO") where l.intID == sno select l).ToList();

            model.ODStatus = (from x in obj.ClaimAccidentDetails where x.AccidentClaimId == model.AccidentClaimId select x.ODStatus).FirstOrDefault();

            model.AssignedToCoList = FetchAssignToList("SP");
            model.HDeductibleAmt = String.IsNullOrEmpty(Convert.ToString((from deductamt in obj.Proc_GetDeductibleAmt(Convert.ToInt32(model.AccidentClaimId)) select deductamt.DeductibleAmt).FirstOrDefault())) ? "0" : Convert.ToString((from deductamt in obj.Proc_GetDeductibleAmt(Convert.ToInt32(model.AccidentClaimId)) select deductamt.DeductibleAmt).FirstOrDefault());

            model.HEffdatefrom =
               ((from fromdate in obj.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select fromdate.EffectiveFrom).FirstOrDefault()).HasValue ?
               (from fromdate in obj.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select fromdate.EffectiveFrom).FirstOrDefault() : null;

            model.HEffdateto =
                ((from todate in obj.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select todate.EffectiveTo).FirstOrDefault()).HasValue ?
                (from todate in obj.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select todate.EffectiveTo).FirstOrDefault() : null;

            model.HOrgname = String.IsNullOrEmpty(Convert.ToString((from orgname in obj.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select orgname.OrganizationName).FirstOrDefault())) ? "" : Convert.ToString((from orgname in obj.Proc_GetDeductibleFromandToDate(Convert.ToInt32(model.AccidentClaimId)) select orgname.OrganizationName).FirstOrDefault());

            model.HFALAmtOD = Convert.ToString((from usr in obj.MNT_Users
                                                join fal in obj.MNT_FAL on usr.FAL_OD equals fal.FALId
                                                where usr.UserId == userid
                                                select fal.Amount).FirstOrDefault());
            model.HFALAmtPDBI = Convert.ToString((from fal in obj.MNT_FAL
                                                  join usr in obj.MNT_Users on fal.FALId equals usr.FAL_PDBI
                                                  where usr.UserId == userid
                                                  select fal.Amount).FirstOrDefault());
            model.HUserId = (from x in obj.MNT_Users where x.UserId == model.userid select x.SNo).FirstOrDefault();
            var PaymentSummaryList = (from details in obj.CLM_PaymentSummary where details.PaymentId == model.PaymentId select details).FirstOrDefault();
            if (PaymentSummaryList != null)
            {
                string[] IgnoreList = { "ReserveId", "MandateId" };
                DataMapper.Map(PaymentSummaryList, model, true, IgnoreList);
                model.CreatedBy = PaymentSummaryList.CreatedBy;
                model.CreatedOn = Convert.ToDateTime(PaymentSummaryList.Createddate);
                if (PaymentSummaryList.Modifiedby != null && PaymentSummaryList.Modifieddate != null)
                {
                    model.ModifiedBy = PaymentSummaryList.Modifiedby;
                    model.ModifiedOn = PaymentSummaryList.Modifieddate;
                }
                model.SPFAL = GetFALforSP(Convert.ToInt32(model.AssignedTo), Convert.ToInt32(model.ClaimType));
            }
            var PaymentDetailList = (from details in obj.CLM_PaymentDetails where details.PaymentId == model.PaymentId select details);
            if (PaymentDetailList.Any() && model.PaymentId != 0)
            {
                if (model._paymentDetails.Count != 0)
                {
                    model._paymentDetails.RemoveRange(0, model._paymentDetails.Count);
                }
                model.PaymentDetails = (from detail in PaymentDetailList
                                        orderby detail.PaymentDetailID
                                        select new ClaimPaymentDetails()
                                        {
                                            PaymentDetailId = detail.PaymentDetailID,
                                            MandateId = detail.MandateId,
                                            AccidentClaimId = detail.AccidentClaimId,
                                            ReserveId = detail.ReserveId,
                                            ClaimID = detail.ClaimId,
                                            CompCode = detail.CmpCode.Trim(),
                                            CompDesc = (from l in obj.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == detail.CmpCode select l.Description).FirstOrDefault(),
                                            PaymentId = detail.PaymentId,
                                            PaymentDue = detail.TotalPaymentDue,
                                            Mandate = (from l in obj.CLM_MandateDetails orderby l.PaymentId descending where l.MandateId == model.MandateId && l.CmpCode == detail.CmpCode select l.CurrentMandateSP).FirstOrDefault(),
                                            CurrentReserve = (from l in obj.CLM_ReserveDetails orderby l.PaymentId descending where l.ReserveId == model.ReserveId && l.CmpCode == detail.CmpCode select l.CurrentReserve).FirstOrDefault()
                                        }
                        ).ToList();

                var remainlist = (from lk in obj.MNT_Lookups where lk.Lookupvalue != "SubTotal" && lk.Lookupvalue != "Labl" && lk.IsActive == "Y" && lk.Category == "TranComponent" select lk).Where(p => !(from details in obj.CLM_PaymentDetails where details.PaymentId == model.PaymentId select details).Any(p2 => p2.CmpCode == p.Lookupvalue)).OrderBy(x => x.DisplayOrder).ToList();

                if (remainlist.Any())
                {
                    var list = model.PaymentDetails;
                    foreach (var remain in remainlist)
                    {
                        var Position = (from lk in obj.MNT_Lookups where lk.Lookupvalue != "SubTotal" && lk.Lookupvalue != "Labl" && lk.IsActive == "Y" && lk.Category == "TranComponent" orderby lk.DisplayOrder select lk).ToList().FindIndex(c => c.DisplayOrder == remain.DisplayOrder);
                        list.Insert(Convert.ToInt32(Position), new ClaimPaymentDetails()
                        {
                            PaymentDetailId = 0,
                            MandateId = model.MandateId,
                            AccidentClaimId = model.AccidentClaimId,
                            ReserveId = model.ReserveId,
                            ClaimID = model.ClaimID,
                            CompCode = remain.Lookupvalue,
                            CompDesc = remain.Description,
                            PaymentId = model.PaymentDetails.FirstOrDefault().PaymentId,
                            PaymentDue = 0.00m,
                            Mandate = 0.00m,
                            CurrentReserve = 0.00m
                        });
                    }
                    model._paymentDetails.Clear();
                    model.GetType().GetProperty("PaymentDetails").SetValue(model, list, null);
                }

            }
            else
            {
                var CompCodeList = (from l in obj.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" && l.Lookupvalue != "SubTotal" && l.Lookupvalue != "Labl" orderby l.DisplayOrder ascending select l.Lookupvalue).ToList();
                var TranList = new List<ClaimPaymentDetails>();
                for (var i = 0; i < CompCodeList.Count; i++)
                {
                    var desc = Convert.ToString(CompCodeList[i]);
                    TranList.Add(new ClaimPaymentDetails()
                    {
                        PaymentDetailId = 0,
                        MandateId = model.MandateId,
                        AccidentClaimId = model.AccidentClaimId,
                        ReserveId = model.ReserveId,
                        ClaimID = model.ClaimID,
                        CompCode = CompCodeList[i].Trim(),
                        CompDesc = (from l in obj.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == desc select l.Description).FirstOrDefault(),
                        PaymentId = 0,
                        PaymentDue = 0.00m,
                        Mandate = (from l in obj.CLM_MandateDetails where l.MandateId == model.MandateId && l.CmpCode == desc select l.CurrentMandateSP).FirstOrDefault(),
                        CurrentReserve = (from l in obj.CLM_ReserveDetails where l.ReserveId == model.ReserveId && l.CmpCode == desc select l.CurrentReserve).FirstOrDefault()
                    });
                }
                model.PaymentDetails = TranList.ToList();
            }

            obj.Dispose();
            return model;
        }
        public decimal? GetFALforSP(int AssignedtoSP, int ClaimType)
        {
            MCASEntities obj = new MCASEntities();
            decimal? result = 0.00m;
            if (ClaimType == 1)
            {
                result = (from usr in obj.MNT_Users
                          join fal in obj.MNT_FAL on usr.FAL_OD equals fal.FALId
                          where usr.SNo == AssignedtoSP
                          select fal.Amount).FirstOrDefault() == null ? 0.00m : (from usr in obj.MNT_Users
                                                                                 join fal in obj.MNT_FAL on usr.FAL_OD equals fal.FALId
                                                                                 where usr.SNo == AssignedtoSP
                                                                                 select fal.Amount).FirstOrDefault();
            }
            else
            {
                result = (from fal in obj.MNT_FAL
                          join usr in obj.MNT_Users on fal.FALId equals usr.FAL_PDBI
                          where usr.SNo == AssignedtoSP
                          select fal.Amount).FirstOrDefault() == null ? 0.00m : (from fal in obj.MNT_FAL
                                                                                 join usr in obj.MNT_Users on fal.FALId equals usr.FAL_PDBI
                                                                                 where usr.SNo == AssignedtoSP
                                                                                 select fal.Amount).FirstOrDefault();
            }
            obj.Dispose();
            return result;
        }
        public int FetchClaimType(ClaimInfoPayment model)
        {
            MCASEntities obj = new MCASEntities();
            int ClaimType = Convert.ToInt32((from Claims in obj.CLM_Claims where Claims.ClaimID == model.ClaimID select Claims.ClaimType).FirstOrDefault());
            obj.Dispose();
            return ClaimType;
        }

        #endregion

        public ClaimInfoPayment GetTotalAmountPaid(ClaimInfoPayment model)
        {
            MCASEntities db = new MCASEntities();

            var SumTotalCurrentMandateSP = (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP != null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandateSP).Sum()
               == null
               ?
               0.00m
               :
               (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP != null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandateSP).Sum();

            var SumTotalCurrentMandate = (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP == null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandate).Sum()
                == null
                ?
                0.00m
                :
                (from s in db.CLM_MandateSummary where s.AccidentClaimId == model.AccidentClaimId && s.MovementType != "P" && s.CurrentMandateSP == null && (s.ApproveRecommedations == null || s.ApproveRecommedations == "Y") select s.CurrentMandate).Sum();

            model.HTotalPreviousApprovePayment = Convert.ToString(SumTotalCurrentMandateSP + SumTotalCurrentMandate);
            db.Dispose();
            return model;
        }

        public static string FetchReserveId(string AccidentClaimId, string ClaimID, string ClaimType, string MandateRecordNo)
        {
            MCASEntities _db = new MCASEntities();
            int acc = Convert.ToInt32(AccidentClaimId);
            int cid = Convert.ToInt32(ClaimID);
            int cty = Convert.ToInt32(ClaimType);
            var result = Convert.ToString((from l in _db.CLM_ReserveSummary where l.AccidentClaimId == acc && l.ClaimID == cid && l.ClaimType == cty orderby l.ReserveId descending select l.ReserveId).FirstOrDefault());
            _db.Dispose();
            return result;
        }

        public static string FetchMandateId(string AccidentClaimId, string ClaimID, string ClaimType, string MandateRecordNo)
        {
            MCASEntities _db = new MCASEntities();
            int acc = Convert.ToInt32(AccidentClaimId);
            int cid = Convert.ToInt32(ClaimID);
            int cty = Convert.ToInt32(ClaimType);
            var result = Convert.ToString((from l in _db.CLM_MandateSummary where l.AccidentClaimId == acc && l.ClaimID == cid && l.ClaimType == cty && l.MandateRecordNo == MandateRecordNo && l.ApproveRecommedations == "Y" orderby l.MandateId descending select l.MandateId).FirstOrDefault());
            _db.Dispose();
            return result;
        }

        public ClaimInfoPayment GetRecoverableFromInsurerBIEZLinkCardNolist(ClaimInfoPayment model, string category)
        {
            MCASEntities db = new MCASEntities();
            int accid = Convert.ToInt32(model.AccidentClaimId);
            int cid = Convert.ToInt32(model.ClaimID);
            string Recoverableresult = string.Empty;
            string EZLinkresult = string.Empty;
            List<LookUpListItems> Recoverable = new List<LookUpListItems>();
            List<LookUpListItems> EZLink = new List<LookUpListItems>();
            try
            {
                Recoverableresult = (from l in db.ClaimAccidentDetails where l.AccidentClaimId == accid select l.IsRecoveryBI).FirstOrDefault() == null ? "" : (from l in db.ClaimAccidentDetails where l.AccidentClaimId == accid select l.IsRecoveryBI).FirstOrDefault();

                Recoverable = (from l in db.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.Lookupvalue == Recoverableresult orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
                if (Recoverable.Count == 0)
                {
                    Recoverable.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "NA" });
                }

                EZLinkresult = (from l in db.CLM_Claims where l.ClaimID == cid select l.EZLinkCardNo).FirstOrDefault() == null ? "" : (from l in db.CLM_Claims where l.ClaimID == cid select l.EZLinkCardNo).FirstOrDefault();

                EZLink = (from l in db.MNT_Lookups where l.Category == category && l.IsActive == "Y" && l.Lookupvalue == EZLinkresult orderby l.Lookupdesc select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();

                if (EZLink.Count == 0)
                {
                    EZLink.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "NA" });
                }
                var valDateofNoticetoSafety = (from l in db.CLM_MandateSummary where l.AccidentClaimId == model.AccidentClaimId && l.ClaimID == model.ClaimID && l.MandateRecordNo == model.MandateRecordNo select l.DateofNoticetoSafety).FirstOrDefault();
                string InformSafetytoreview = (from l in db.CLM_MandateSummary where l.AccidentClaimId == model.AccidentClaimId && l.ClaimID == model.ClaimID && l.MandateRecordNo == model.MandateRecordNo select l.InformSafetytoreviewfindings).FirstOrDefault();
                string Informsafterresult = string.IsNullOrEmpty((from l in model.InformSafetytoreviewfindingsList where l.Lookup_value == InformSafetytoreview select l.Lookup_desc).FirstOrDefault())?"":(from l in model.InformSafetytoreviewfindingsList where l.Lookup_value == InformSafetytoreview select l.Lookup_desc).FirstOrDefault();
                if(valDateofNoticetoSafety != null) 
                {
                    model.DateofNoticetoSafety = valDateofNoticetoSafety;
                }
                model.InformSafetytoreviewfindings = Informsafterresult;
                model.RecoverableFromInsurerBIList = Recoverable;
                model.EZLinkCardNolist = EZLink;

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
    }
    public class ClaimPaymentModelCollection
    {
        #region "Object Properties"
        public long? RecordNumber { get; set; }
        public int? SNO { get; set; }
        public int? MandateId { get; set; }
        public int? PaymentId { get; set; }
        public int? ReserveId { get; set; }
        public int PolicyId { get; set; }
        public string TotalPaymentDue { get; set; }
        public int ClaimID { get; set; }
        public int AccidentClaimId { get; set; }
        public string MandateRecordNo { get; set; }
        public string ApproveRecommedations { get; set; }
        public string PaymentRecordNo { get; set; }
        public int? ClaimType { get; set; }
        public string ClaimantName { get; set; }
        public string ClaimRecordNo { get; set; }
        public string UniqueId { get; set; }
        public string ApprovePayment { get; set; }
        public string ApprovedDate { get; set; }
        public string PayeeName { get; set; }
        public string ClaimTypeCode { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string mode { get; set; }
        public string HClaimantStatus { get; set; }
        public string HAssignedTo { get; set; }
        #endregion
    }

    public class ClaimPaymentDetails : BaseModel
    {
        #region "Object Properties"
        public Int64? PaymentDetailId { get; set; }
        public int ReserveId { get; set; }
        public int MandateId { get; set; }
        public int AccidentClaimId { get; set; }
        public int? ClaimID { get; set; }
        public int? PaymentId { get; set; }
        public string CmpCode { get; set; }
        public string CompCode { get; set; }
        public string CompDesc { get; set; }
        public decimal? PaymentDue { get; set; }
        public decimal? Mandate { get; set; }
        public decimal? CurrentReserve { get; set; }
        #endregion
    }
}


