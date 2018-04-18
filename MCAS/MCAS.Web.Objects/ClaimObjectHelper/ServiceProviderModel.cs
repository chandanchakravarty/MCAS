using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MCAS.Entity;
using System.Web;
using MCAS.Web.Objects.Resources.Common;
namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ServiceProviderModel : BaseModel
    {
        #region Constructors
        public ServiceProviderModel()
        {
            this._serviceProviderModel = FetchServiceProviderList(0);
        }
        public ServiceProviderModel(int AccidentId)
        {
            this._serviceProviderModel = FetchServiceProviderList(AccidentId);
        }
        private List<ServiceProviderCollection> _serviceProviderModel;
        public List<ServiceProviderCollection> ServiceProviderCollection
        {
            get { return _serviceProviderModel; }
        }
        #endregion

        #region properties
        private DateTime? _appointedDate = null;
        private DateTime? _createddate = null;
        private DateTime? _modifieddate = null;
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

        public int ServiceProviderId { get; set; }

        public string ResultMessage { get; set; }

        [DisplayName("Claim Type")]
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVClaimType")]
        public int ClaimTypeId { get; set; }


        [DisplayName("Claimant Name")]
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVClaimantName")]
        public int ClaimantNameId { get; set; }

        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVPartyType")]
        [DisplayName("Party Type")]
        public int PartyTypeId { get; set; }

        public int ServiceProviderTypeId { get; set; }

        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVCompanyName")]
        [DisplayName("Company Name")]
        public int CompanyNameId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVAppointedDate")]
        [Display(Name = "Appointed Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppointedDate
        {
            get { return _appointedDate; }
            set { _appointedDate = value; }
        }
        public Int64? OrgRecordNumber { get; set; }
        public string PartyName { get; set; }
        public string RecordNumber { get; set; }
        public string ClaimantName { get; set; }
        public string CompanyName { get; set; }


        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVServiceProviderType")]
        [DisplayName("Service Provider Type")]
        public string ServiceProviderOption { get; set; }


        public string Hselect { get; set; }
        public string Hdivdis { get; set; }
        public string Himgsrc { get; set; }
        public string Hshowgrid { get; set; }
        public string HGridImageSign { get; set; }
        public string AddNewPartyText { get; set; }
        public string h3header { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVClaimantAddress1")]
        [DisplayName("Claimant's Address1")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ClaimRecordNo { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVCountry")]
        [DisplayName("Country")]
        public string CountryId { get; set; }

        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVPostalCode")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string PostalCode1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVReference")]
        [DisplayName("Reference Address1")]
        public string Reference { get; set; }

        public string ContactPersonName { get; set; }
        public string EmailAddress { get; set; }
        public string OfficeNo { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string ContactPersonName2nd { get; set; }
        public string EmailAddress2nd { get; set; }
        public string OfficeNo2nd { get; set; }
        public string Mobile2nd { get; set; }
        public string Fax2nd { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int AccidentClaimId { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVStatusId")]
        [DisplayName("Status")]
        public int StatusId { get; set; }
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }
        [Display(Name = "Appointed Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Modifieddate
        {
            get { return _modifieddate; }
            set { _modifieddate = value; }
        }
        public int AccidentId { get; set; }
        public int PolicyId { get; set; }
        public string IsActive { get; set; }
        public List<ClaimantType> ClaimTypeList { get; set; }
        public List<ClaimantType> PartyTypeList { get; set; }
        public List<ClaimantStatus> ClaimantNameList { get; set; }
        public List<ClaimantStatus> CompanyNameList { get; set; }
        public List<LookUpListItems> StatusList { get; set; }
        public List<UserCountryListItems> usercountrylist { get; set; }
        public IEnumerable<SelectListItem> ServiceProviderOptionList { get; set; }
        public override string screenId
        {
            get
            {
                return "132";
            }

        }
        public override string listscreenId
        {

            get
            {
                return "132";
            }

        }

        //For Pvt Car and Taxi Model
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider), ErrorMessageResourceName = "RFVTPVehicleNo")]
        public string TPVehNo { get; set; }
        public string VehNo { get; set; }
        # endregion

        #region Methods
        public static IEnumerable<SelectListItem> ServiceProviderList()
        {
            List<ClaimStatus> list = new List<ClaimStatus>();
            list.Add(new ClaimStatus() { ID = 1, Name = MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider.Own });
            list.Add(new ClaimStatus() { ID = 2, Name = MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider.ThirdParty });
            SelectList sl = new SelectList(list, "ID", "Name");
            return sl;
        }

        public static List<ClaimantStatus> SelectOnlyList()
        {
            var item = new List<ClaimantStatus>();
            item.Add(new ClaimantStatus() { Id = 0, Text = "[Select...]" });
            return item;
        }

        public static List<ClaimantStatus> SelectOnlyListNoSelect()
        {
            var item = new List<ClaimantStatus>();
            return item;
        }

        private List<ServiceProviderCollection> FetchServiceProviderList(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            try
            {
                this.AccidentId = AccidentClaimId;
                var items = new List<ServiceProviderCollection>();
                if (AccidentClaimId != 0)
                {
                    var Ownlist = db.Proc_GetServiceProverList(AccidentClaimId.ToString()).ToList();

                    if ((HttpContext.Current.Session["OrganisationType"].ToString().ToLower() == "tx") || (HttpContext.Current.Session["OrganisationType"].ToString().ToLower() == "pc"))
                    {
                        items = (from data in Ownlist
                                 select new ServiceProviderCollection()
                                 {
                                     ServiceProviderId = (int)data.ServiceProviderId,
                                     OrgRecordNumber = data.RecordNumber,
                                     RecordNumber = data.ClaimRecordNo,
                                     ClaimantName = data.TPVehicleNo,
                                     PartyName = data.PartyName,
                                     ServiceProviderOption = data.ServiceProviderOption,
                                     ServiceProviderOption1 = data.ServiceProviderOption1,
                                     CompanyName = data.CompanyName,
                                     AccidentClaimId = data.AccidentClaimId,
                                     PolicyId = data.PolicyId,
                                     ServiceProviderTypeId = (int)data.ServiceProviderTypeId,
                                     ClaimTypeId = (int)data.ClaimTypeId,
                                     ClaimTypeCode = data.ClaimTypeCode,
                                     ClaimTypeDesc = data.ClaimTypeId.ToString() == "1" ? Common._1 : data.ClaimTypeId.ToString() == "2" ? Common._2 : data.ClaimTypeId.ToString() == "3" ? Common._3 : data.ClaimTypeId.ToString() == "4" ? Common._4 : "",
                                 }
                                ).ToList();
                    }
                    else
                    {
                        items = (from data in Ownlist
                                 select new ServiceProviderCollection()
                                 {
                                     ServiceProviderId = (int)data.ServiceProviderId,
                                     OrgRecordNumber = data.RecordNumber,
                                     RecordNumber = data.ClaimRecordNo,
                                     ClaimantName = data.ClaimantName,
                                     PartyName = data.PartyName,
                                     ServiceProviderOption = data.ServiceProviderOption,
                                     ServiceProviderOption1 = data.ServiceProviderOption1,
                                     CompanyName = data.CompanyName,
                                     AccidentClaimId = data.AccidentClaimId,
                                     PolicyId = data.PolicyId,
                                     ServiceProviderTypeId = (int)data.ServiceProviderTypeId,
                                     ClaimTypeId = (int)data.ClaimTypeId,
                                     ClaimTypeCode = data.ClaimTypeCode,
                                     ClaimTypeDesc = data.ClaimTypeId.ToString() == "1" ? Common._1 : data.ClaimTypeId.ToString() == "2" ? Common._2 : data.ClaimTypeId.ToString() == "3" ? Common._3 : data.ClaimTypeId.ToString() == "4" ? Common._4 : "",
                                 }
                                ).ToList();
                    }
                }

                return items;
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

        public ServiceProviderModel Update()
        {
            MCASEntities obj = new MCASEntities();
            CLM_ServiceProvider service;
            if (obj.Connection.State == System.Data.ConnectionState.Closed)
                obj.Connection.Open();
            using (System.Data.Common.DbTransaction transaction = obj.Connection.BeginTransaction())
            {
                try
                {
                    var val = obj.CLM_ServiceProvider.Where(x => x.ServiceProviderId == this.ServiceProviderId).ToList();
                    if (val.Any())
                    {
                        service = obj.CLM_ServiceProvider.Where(x => x.ServiceProviderId == this.ServiceProviderId).FirstOrDefault();
                        var cDt = service.Createddate;
                        this.IsActive = "Y";
                        this.ServiceProviderTypeId = Convert.ToInt32(this.ServiceProviderOption);
                        service.Modifiedby = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.Modifieddate = DateTime.Now;
                        DataMapper.Map(this, service, true);
                        service.Createddate = cDt;
                        obj.SaveChanges();
                        this.CreatedBy = service.Createdby;
                        this.CreatedOn = Convert.ToDateTime(service.Createddate);
                        this.ModifiedOn = DateTime.Now;
                        this.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.ResultMessage = "Record updated successfully.";
                    }
                    else
                    {

                        service = new CLM_ServiceProvider();
                        var ClaimRecordNo1 = (from l in obj.CLM_Claim where l.ClaimID == this.ClaimantNameId select l.ClaimRecordNo).FirstOrDefault();
                        service.ClaimRecordNo = ClaimRecordNo1;
                        this.IsActive = "Y";
                        this.Createddate = DateTime.Now;
                        this.ServiceProviderTypeId = Convert.ToInt32(this.ServiceProviderOption);
                        DataMapper.Map(this, service, true);
                        service.Createdby = this.CreatedBy;
                        obj.CLM_ServiceProvider.AddObject(service);
                        obj.SaveChanges();
                        this.ServiceProviderId = service.ServiceProviderId;
                        this.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.CreatedOn = DateTime.Now;
                        this.ResultMessage = "Record saved successfully.";
                    }
                    transaction.Commit();
                    obj.Dispose();
                    this._serviceProviderModel = this.FetchServiceProviderList(this.AccidentId);
                    return this;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    obj.Dispose();
                    this.ResultMessage = "Some error Occurs";
                    throw (ex);
                }
            }
        }


        public static List<ClaimantStatus> GetClaimantNameList(int Acc, int CType)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimantStatus>();
            var list = _db.Proc_GetCLM_Claim_ClamiantName(Acc, CType).ToList();
            item = (from n in list
                    select new ClaimantStatus()
                    {
                        Id = n.ClaimID,
                        Text = n.ClaimantName
                    }
                        ).ToList();
            item.Insert(0, new ClaimantStatus() { Id = 0, Text = "[Select...]" });
            _db.Dispose();
            return item;
        }

        public static List<ClaimantStatus> GetCompanyNameList(string InsurerType, string PartyTypeId,string CurrStatus)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimantStatus>();
            var list = _db.Proc_GetMNT_Cedant_CompanyName(InsurerType, PartyTypeId,CurrStatus).ToList();
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
        public ServiceProviderModel ServiceProvider(string ServiceProviderId, ServiceProviderModel model)
        {

            MCASEntities obj = new MCASEntities();
            try
            {
                model.ClaimTypeList = ServiceProviderModel.FetchLookUpList("ClaimType");
                model.ServiceProviderOptionList = ServiceProviderModel.ServiceProviderList();
                if (ServiceProviderId != null && obj.Proc_GetCLM_ServicePID(ServiceProviderId).Count() != 0)
                {
                    var list = obj.Proc_GetCLM_ServicePID(ServiceProviderId).ToList().FirstOrDefault();
                    model.ClaimantNameList = ServiceProviderModel.GetClaimantNameList(list.AccidentId, list.ClaimTypeId);
                    model.CompanyNameList = ServiceProviderModel.GetCompanyNameList(Convert.ToString(list.ServiceProviderTypeId), Convert.ToString(list.PartyTypeId), "Select");
                    if (list != null)
                        DataMapper.Map(list, model, true);
                    model.CreatedBy = list.Createdby;
                    model.CreatedOn = Convert.ToDateTime(list.Createddate);
                    if (list.Modifieddate != null || list.Modifieddate.ToString() != "")
                    {
                        model.ModifiedOn = Convert.ToDateTime(list.Modifieddate);
                        model.ModifiedBy = list.Modifiedby;
                    }
                    model.Hshowgrid = model.ClaimTypeId == 1 ? "OD" : model.ClaimTypeId == 2 ? "PD" : model.ClaimTypeId == 3 ? "BI" : "1";
                }
                else
                {
                    model.ClaimantNameList = ServiceProviderModel.SelectOnlyList();
                    model.CompanyNameList = ServiceProviderModel.SelectOnlyList();
                }
                return model;
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
        #endregion


        public static List<ClaimantType> FetchLookUpList(string Category)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> list;
            try
            {
                list = (from l in obj.MNT_Lookups
                        where l.Category == Category && l.IsActive == "Y"
                        orderby l.Lookupdesc
                        select new ClaimantType
                            {
                                Id = l.Lookupvalue,
                                Text = l.Description
                            }).ToList();
                list.Insert(0, new ClaimantType() { Id = "0", Text = "[Select...]" });
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }
            return list;
        }
        public static List<SelectType> FetchSelectTypeLookUpList(string Category)
        {
            MCASEntities obj = new MCASEntities();
            List<SelectType> SelectTypeList = new List<SelectType>();
            try
            {
                var lookUpListVals = obj.MNT_Lookups.Where(l => l.Category == Category && l.IsActive == "Y").OrderBy(x => x.Lookupdesc);
                foreach(var val in lookUpListVals){
                    SelectType item = new SelectType();
                    item.Id = Convert.ToInt32(val.Lookupvalue);
                    item.Text = val.Description;
                    SelectTypeList.Add(item);
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
            return SelectTypeList;
        }
        public static List<ClaimantType> FetchTPVehicleList(int accidentId)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> TPVehicleNoList = new List<ClaimantType>();
            try
            {
                var TPVehicleNos = obj.CLM_Claims.Where(x => x.AccidentClaimId == accidentId).Select(y => new {y.ClaimID , y.TPVehicleNo});
                foreach (var val in TPVehicleNos)
                {
                    ClaimantType item = new ClaimantType();
                    item.Id = Convert.ToString(val.ClaimID);
                    item.Text = val.TPVehicleNo;
                    TPVehicleNoList.Add(item);
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
            return TPVehicleNoList;
        }
        public static List<ClaimantType> FetchLookUpListWithOptions(string Category, bool TopOption = false)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimantType> list;
            try
            {
                list = (from l in obj.MNT_Lookups
                        where l.Category == Category && l.IsActive == "Y"
                        orderby l.Lookupdesc
                        select new ClaimantType
                        {
                            Id = l.Lookupvalue,
                            Text = l.Description
                        }).ToList();
                if (TopOption)
                {
                    list.Insert(0, new ClaimantType() { Id = "0", Text = "[Select...]" });
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
            return list;
        }
    }
    public class ServiceProviderCollection
    {
        #region "Object Properties"
        public int ServiceProviderId { get; set; }
        public Int64? OrgRecordNumber { get; set; }
        public string RecordNumber { get; set; }
        public string ClaimantName { get; set; }
        public string CompanyName { get; set; }
        public string PartyName { get; set; }
        public string ServiceProviderOption { get; set; }
        public string ServiceProviderOption1 { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public int CompanyNameId { get; set; }
        public int? AccidentId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public int ClaimTypeId { get; set; }
        public int ServiceProviderTypeId { get; set; }
        public List<string> HeaderListCollection { get; set; }
        #endregion
    }
}
