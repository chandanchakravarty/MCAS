using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.ComponentModel;
using MCAS.Web.Objects.ClaimObjectHelper;
using System.Globalization;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class LogRequestModel : BaseModel
    {
        MCASEntities _db = new MCASEntities();

        #region Properties
        public int _prop1 { get; set; }
        private DateTime? _modifieddate = null;
        private DateTime? _lOGDate = null;
        public int? LogId { get; set; }
        public bool? IsVoid { get; set; }
        public int VoidStatus { get; set; }
        public string LogRefNo { get; set; }
        public int AccidentClaimId { get; set; }
        public int? ClaimID { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimLogRequest), ErrorMessageResourceName = "RFVClaimantName")]
        public int? ClaimantNameId { get; set; }
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimLogRequest), ErrorMessageResourceName = "RFVHospitalName")]
        public int? Hospital_Id { get; set; }
        public string HospitalName { get; set; }
        public int? AssignTo { get; set; }
        public decimal? LOGAmount { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimLogRequest), ErrorMessageResourceName = "RFVLOGDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "LOG Date")]
        public DateTime? LOGDate
        {
            get { return _lOGDate; }
            set { _lOGDate = value; }
        }
        public string CORemarks { get; set; }
        public string IsActive { get; set; }
        private DateTime? CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified date")]
        public DateTime? Modifieddate
        {
            get { return _modifieddate; }
            set { _modifieddate = value; }
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
        public string ClaimNo { get; set; }
        public string HChildGrid { get; set; }
        public string SubClaimNo { get; set; }
        public int MandateId { get; set; }
        public string MandateNo { get; set; }
        public string HdivHidden { get; set; }
        public List<LogRequestModel> Loglist = new List<LogRequestModel>();
        public List<CommonUtilities.CommonType> HopitalNameIdList { get; set; }
        public List<CommonUtilities.CommonType> SelectListClamiantName { get; set; }
        public List<CommonUtilities.CommonType> AssignToIdList { get; set; }

        #endregion

        #region Method

        public static List<CommonUtilities.CommonType> FetchHopitalNameIdList()
        {
            MCASEntities _db = new MCASEntities();
            var items = new List<CommonUtilities.CommonType>();
            var list = _db.Proc_GetMnt_HopitalList().ToList();
            items = (from n in list
                     select new CommonUtilities.CommonType()
                     {
                         intID = n.Id,
                         Text = n.HospitalName
                     }).ToList();
            items.Insert(0, new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
            _db.Dispose();
            return items;
        }

        public List<CommonUtilities.CommonType> FetchLogRequest()
        {
            return FetchHopitalNameIdList();
        }
        #endregion

        public static List<LogRequestModel> FetchLogList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<LogRequestModel>();
            // var model = new LogRequestModel();

            var logRequestList = obj.Proc_GetLogslist().ToList();
            if (logRequestList.Any())
            {
                foreach (var data in logRequestList)
                {
                    item.Add(new LogRequestModel()
                               {
                                   ClaimNo = data.ClaimNo,
                                   SubClaimNo = data.SubClaimNo,
                                   LogRefNo = data.LogRefNo == null ? "" : data.LogRefNo,
                                   HospitalName = data.HospitalName == null ? "" : data.HospitalName,
                                   LOGAmount = data.LOGAmount == null ? 0 : data.LOGAmount,
                                   LogId = data.LogId == null ? 0 : data.LogId,
                                   ClaimID = data.ClaimID,
                                   AccidentClaimId = data.AccidentClaimId,
                                   MandateId = (int)data.MandateId,
                                   MandateNo = data.MandateRecordNo,
                                   IsVoid = data.IsVoid
                               });
                }
            }
            return item;
        }


        public List<CommonUtilities.CommonType> FetchAssignToIdList()
        {
            MCASEntities _db = new MCASEntities();
            var items = new List<CommonUtilities.CommonType>();
            var list = _db.Proc_GetSupervisorList().ToList();
            items = (from n in list
                     select new CommonUtilities.CommonType()
                     {
                         intID = n.SNo,
                         Text = n.UserId
                     }).ToList();
            items.Insert(0, new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
            _db.Dispose();
            return items;
        }
        public List<CommonUtilities.CommonType> GetClaimantNameReserveList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<CommonUtilities.CommonType>();
            // var list = _db.Proc_GetClaminNameReserve(Acc).ToList();
            var qry = from a in _db.CLM_Claims where a.ClaimType == 3 select a;
            var results = qry.ToList();
            item = (from data in results
                    select new CommonUtilities.CommonType()
                    {
                        intID = (int)data.ClaimID,
                        Text = data.ClaimantName
                    }
                  ).ToList();
            item.Insert(0, new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
            return item;
        }

        public List<CommonUtilities.CommonType> FetchClaimantName()
        {
            var item = new List<CommonUtilities.CommonType>();
            item.Add(new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
            return item;
        }
        public LogRequestModel Save()
        {
            MCASEntities obj = new MCASEntities();
            try
            {
                CLM_LogRequest LogRequest;
                var model = new CLM_LogRequest();
                LogRequest = obj.CLM_LogRequest.Where(x => x.LogId == this.LogId).FirstOrDefault();
                if (LogRequest != null)
                {
                    DataMapper.Map(this, LogRequest, true);
                    LogRequest.ModifiedBy = System.Web.HttpContext.Current.Session["LoggedInUserName"].ToString();
                    LogRequest.ModifiedDate = DateTime.Now;
                    obj.SaveChanges();
                    this.CreatedBy = LogRequest.Createdby;
                    this.CreatedDate = LogRequest.CreatedDate;
                    this.ModifiedBy = DateTime.Now.ToString();
                    this.ModifiedBy = System.Web.HttpContext.Current.Session["LoggedInUserName"].ToString();
                    return this;
                }
                else
                {
                    LogRequest = new CLM_LogRequest();
                    DataMapper.Map(this, LogRequest, true);
                    LogRequest.IsActive = "Y";
                    LogRequest.Createdby = System.Web.HttpContext.Current.Session["LoggedInUserName"].ToString();
                    LogRequest.CreatedDate = DateTime.Now;
                    obj.CLM_LogRequest.AddObject(LogRequest);
                    obj.SaveChanges();
                    this.LogId = LogRequest.LogId;
                    this.CreatedBy = System.Web.HttpContext.Current.Session["LoggedInUserName"].ToString();
                    this.CreatedDate = DateTime.Now;
                    obj.Proc_InsertLogRefNo(this.LogId);
                    return this;
                }
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

        public static List<LogRequestModel> GetLogSearchResults(LogRequestModel model)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<LogRequestModel>();

            var logRequestList = obj.Proc_GetLogSearchResults(model.ClaimNo, model.SubClaimNo, model.LogRefNo, model.Hospital_Id).ToList();
            if (logRequestList.Any())
            {
                foreach (var data in logRequestList)
                {
                    item.Add(new LogRequestModel()
                    {
                        ClaimNo = data.ClaimNo,
                        SubClaimNo = data.SubClaimNo,
                        LogRefNo = data.LogRefNo == null ? "" : data.LogRefNo,
                        HospitalName = data.HospitalName == null ? "" : data.HospitalName,
                        LOGAmount = data.LOGAmount == null ? 0 : data.LOGAmount,
                        LogId = data.LogId == null ? 0 : data.LogId,
                        ClaimID = data.ClaimID,
                        AccidentClaimId = data.AccidentClaimId,
                        MandateId = (int)data.MandateId,
                        MandateNo = data.MandateRecordNo,
                        IsVoid = data.IsVoid
                    });
                }

            }
            return item;
        }

        public void SetVoidType(int LogId)
        {
            MCASEntities obj = new MCASEntities();
            try
            {
                obj.Proc_SetVoidLogRequest(LogId);

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

        public static List<LogRequestModel> GetLogDetails(int Log_ID)
        {
            MCASEntities obj = new MCASEntities();
            var logDetails = obj.Proc_GetLogsDetails(Log_ID).FirstOrDefault();
            var logDetails_ = new List<LogRequestModel>();
            logDetails_.Add(new LogRequestModel()
            {
                ClaimNo = logDetails.ClaimNo,
                SubClaimNo = logDetails.SubClaimNo,
                LogRefNo = logDetails.LogRefNo,
                HospitalName = logDetails.HospitalName
            });
            return logDetails_;
        }
        #region  OverrideProperties
        public override string screenId
        {
            get
            {
                return "303";
            }
        }
        #endregion
    }
}
