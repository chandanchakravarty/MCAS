using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Globalization;
using MCAS.Web.Objects.ClaimObjectHelper;
using MCAS.Web.Objects.Resources.Masters;


namespace MCAS.Web.Objects.MastersHelper
{
    #region VehicleModel
    public class VehicleModel : BaseModel
    {

        MCASEntities _db = new MCASEntities();
        #region properties
        public int? TranId { get; set; }
        //[Required(ErrorMessage = "Make Code is required.")]
        [Required(ErrorMessageResourceType = typeof(VModelList), ErrorMessageResourceName = "RFVMakeName")]
        public string MakeCode { get; set; }
        //[Required(ErrorMessage = "Make Name is required.")]
        [Required(ErrorMessageResourceType = typeof(VModelList), ErrorMessageResourceName = "RFVMakeName")]
        public string VehicleMakeDescription { get; set; }
        public string ModelCode { get; set; }
        //[Required(ErrorMessage = "Model Description is required.")]
        [Required(ErrorMessageResourceType = typeof(VModelList), ErrorMessageResourceName = "RFVModelDescription")]
        public string ModelName { get; set; }
        public string VehicleClassDescription { get; set; }
        public string MakeBody { get; set; }
        public string VMakeCode { get; set; }
        public string VModelCode { get; set; }
        //[Required(ErrorMessage = "Cylinder Capacity is required.")]
        [Required(ErrorMessageResourceType = typeof(VModelList), ErrorMessageResourceName = "RFVCylinderCapacity")]
        public string CC { get; set; }
        //[Required(ErrorMessage = "Tonnage is required.")]
        [Required(ErrorMessageResourceType = typeof(VModelList), ErrorMessageResourceName = "RFVTonnage")]
        public string LC { get; set; }
        [Required(ErrorMessageResourceType = typeof(VModelList), ErrorMessageResourceName = "RFVStatus")]
        public string Status { get; set; }

        public string SubClassCode { get; set; }
        public string VBody { get; set; }

        public string BodyCode { get; set; }
        public string ModelDescription { get; set; }

        public string NoOfPassenger { get; set; }
        public string VehicleClassCode { get; set; }
        public string VehicleClassDesc { get; set; }

        public List<VehicleListItem> vehicleMakelist { get; set; }
        public List<VehicleListItem> VehicleClasslist { get; set; }
        public List<SubClassListItem> SubClassList { get; set; }
        public List<VehicleModel> VehicleBody { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }

        public override string screenId
        {
            get
            {
                return "249";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "122";
            }

        }

        #endregion

        #region Methods
        public static List<VehicleMakeModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleMakeModel>();
            var Vehiclelist = (from x in _db.MNT_Motor_Make select x);
            if (Vehiclelist.Any())
            {
                foreach (var data in Vehiclelist)
                {
                    item.Add(new VehicleMakeModel() { TranId = data.TranId, SubClassCode = data.SubClassCode, MakeCode = data.MakeCode, MakeName = data.MakeName, Status = data.status });
                }
            }
            return item;
        }

        public static List<VehicleModel> GetVehicleModelSearchResult(string makecode)
        {
            MCASEntities _db = new MCASEntities();
            List<VehicleModel> SearchResult = new List<VehicleModel>();
            try
            {
                SearchResult = (from x in _db.MNT_MOTOR_MODEL
                                join m in _db.MNT_Motor_Make on x.MakeCode equals m.MakeCode
                                join n in _db.MNT_Motor_Class on x.VehicleClassCode equals n.VehicleClassCode
                                where x.MakeCode.Contains(makecode)
                                select new VehicleModel
                                {
                                    MakeCode = x.MakeCode,
                                    ModelCode = x.ModelCode,
                                    ModelName = m.MakeName,
                                    ModelDescription = x.ModelName,
                                    VehicleClassDescription = n.VehicleClassDesc,
                                    TranId = x.TranId

                                }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return SearchResult;
        }

        public static List<VehicleModel> FetchVehicleClass()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleModel>();
            var Vehiclelist = (from x in _db.MNT_Motor_Body select x);
            if (Vehiclelist.Any())
            {
                foreach (var data in Vehiclelist)
                {
                    item.Add(new VehicleModel() { TranId = data.TranId, BodyCode = data.BodyCode, ModelDescription = data.BodyDesc });
                }
            }
            return item;
        }

        public static List<VehicleModel> FetchMotorBody(bool AddAll = true)
        {
            MCASEntities _db = new MCASEntities();
            List<VehicleModel> list = new List<VehicleModel>();
            list = (from x in _db.MNT_Motor_Body select new VehicleModel { TranId = x.TranId, BodyCode = x.BodyCode, ModelDescription = x.BodyDesc }).ToList();
            if (AddAll)
            {
                list.Insert(0, new VehicleModel() { BodyCode = "", ModelDescription = "[Select...]" });
            }
            return list;
        }

        public static List<VehicleModel> FetchModel(string modelcode)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleModel>();
            var VehicleModellist = (from x in _db.MNT_MOTOR_MODEL where x.MakeCode.ToUpper() == modelcode.ToUpper() select x);
            if (VehicleModellist.Any())
            {
                foreach (var data in VehicleModellist)
                {
                    item.Add(new VehicleModel() { TranId = data.TranId, ModelCode = data.ModelCode, ModelName = data.ModelName });
                }
            }
            return item;
        }

        public VehicleModel Update()
        {
            MNT_Motor_Make motormake;
            if (TranId.HasValue)
            {
                motormake = _db.MNT_Motor_Make.Where(x => x.TranId == this.TranId).FirstOrDefault();

                motormake.SubClassCode = this.SubClassCode;
                if (motormake.status == "1")
                {
                    this.Status = "Active";
                }
                else
                {
                    this.Status = "InActive";
                }
                // motormake.MakeName = this.MakeName;
                //  _db.MNT_Motor_Make.AddObject(motormake);
                _db.SaveChanges();
                return this;

            }
            else
            {
                motormake = new MNT_Motor_Make();

            }

            //  motormake.TranId =Convert.ToInt16(this.TranId);
            var maxlength = 5;
            var prefix = "M0";
            var countrows = (from row in _db.MNT_Motor_Make select row).Count();
            //   var countrows = (from row in obj.MNT_BusCaptain select (int?)row.TranId).Max() ?? 0; 
            string currentno = (countrows + 1).ToString();

            string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

            var Makecode = (prefix + result + currentno);
            motormake.SubClassCode = this.SubClassCode;
            motormake.MakeCode = Makecode;
            //    motormake.MakeName = this.MakeName;
            if (this.Status == "Active")
            {
                motormake.status = "1";
            }
            else
            {
                motormake.status = "0";
            }
            _db.MNT_Motor_Make.AddObject(motormake);
            _db.SaveChanges();
            this.MakeCode = Makecode;
            return this;


        }

        public VehicleModel VehicleModelUpdate()
        {
            MNT_MOTOR_MODEL motormodel;
            if (TranId.HasValue)
            {
                motormodel = _db.MNT_MOTOR_MODEL.Where(x => x.TranId == this.TranId).FirstOrDefault();
                motormodel.ModelCode = this.ModelCode;
                motormodel.MakeCode = this.MakeCode;
                motormodel.ModelName = this.ModelName;
                motormodel.VehicleClassCode = this.VehicleClassCode;
                motormodel.CC = this.CC;
                motormodel.LC = this.LC;
                motormodel.NoOfPassenger = this.NoOfPassenger;
                motormodel.status = this.Status;
                motormodel.ModifiedBy = this.ModifiedBy;
                motormodel.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.ModelCode = motormodel.ModelCode;
                this.CreatedBy = motormodel.CreatedBy;
                this.CreatedOn = motormodel.CreatedDate == null ? DateTime.MinValue : (DateTime)motormodel.CreatedDate;
                this.ModifiedOn = motormodel.ModifiedDate;
                return this;
            }
            else
            {
                motormodel = new MNT_MOTOR_MODEL();

                var maxlength = 6;
                var prefix = "VED";
                var countrows = (from row in _db.MNT_MOTOR_MODEL select (int?)row.TranId).Max() ?? 0;
                string currentno = (countrows + 1).ToString();
                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                var Modelcode = (prefix + result + currentno);
                motormodel.ModelCode = Modelcode;
                motormodel.MakeCode = this.MakeCode;
                motormodel.ModelName = this.ModelName;
                motormodel.VehicleClassCode = this.VehicleClassCode;
                motormodel.CC = this.CC;
                motormodel.LC = this.LC;
                motormodel.NoOfPassenger = this.NoOfPassenger;
                motormodel.status = this.Status;
                motormodel.CreatedBy = this.CreatedBy;
                motormodel.CreatedDate = DateTime.Now;
                _db.MNT_MOTOR_MODEL.AddObject(motormodel);
                _db.SaveChanges();
                this.TranId = motormodel.TranId;
                this.ModelCode = Modelcode;
                this.CreatedOn = (DateTime)motormodel.CreatedDate;
                return this;
            }
        }




        #endregion


    }

    #endregion

    #region Vehicle Type
    public class VehicleTypeModel : BaseModel
    {
        #region Properties
        public int? TranId { get; set; }
        public string SubClassCode { get; set; }
        public string VehicleClassCode { get; set; }
        //[Required(ErrorMessage = "Vehicle Class Description is required.")]
        [Required(ErrorMessageResourceType = typeof(VehicleClassEditor), ErrorMessageResourceName = "RFVVehicleClassDescription")]
        public string VehicleClassDesc { get; set; }
        [Required(ErrorMessageResourceType = typeof(VehicleClassEditor), ErrorMessageResourceName = "RFVStatus")]
        public string Status { get; set; }
        public List<SubClassListItem> SubClassList { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }

        public override string screenId
        {
            get
            {
                return "244";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "120";
            }

        }

        #endregion

        #region Static Methods

        public static List<VehicleTypeModel> GetVehicleClassResult(string BodyCode, string BodyDesc)
        {
            MCASEntities _db = new MCASEntities();
            List<VehicleTypeModel> searchResult = new List<VehicleTypeModel>();
            try
            {
                searchResult = (from vehicleclass in _db.MNT_Motor_Class
                                where
                                vehicleclass.VehicleClassCode.Contains(BodyCode) &&
                                vehicleclass.VehicleClassDesc.Contains(BodyDesc)
                                select vehicleclass).ToList().Select(item => new VehicleTypeModel
                                    {
                                        VehicleClassCode = item.VehicleClassCode,
                                        VehicleClassDesc = item.VehicleClassDesc,
                                        Status = item.Status,
                                        TranId = item.TranId
                                    }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return searchResult;
        }



        #endregion

        #region Methods
        public VehicleTypeModel VehicleClassUpdate()
        {
            MCASEntities _db = new MCASEntities();
            MNT_Motor_Class motorclass;
            if (TranId.HasValue)
            {
                motorclass = _db.MNT_Motor_Class.Where(x => x.TranId == this.TranId).FirstOrDefault();
                motorclass.VehicleClassCode = this.VehicleClassCode;
                motorclass.Status = this.Status;
                motorclass.VehicleClassDesc = this.VehicleClassDesc;
                motorclass.ModifiedBy = this.ModifiedBy;
                motorclass.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.VehicleClassCode = motorclass.VehicleClassCode;

                this.CreatedBy = motorclass.CreatedBy;
                this.CreatedOn = motorclass.CreatedDate == null ? DateTime.MinValue : (DateTime)motorclass.CreatedDate;
                this.ModifiedOn = motorclass.ModifiedDate;

                return this;

            }
            else
            {
                motorclass = new MNT_Motor_Class();
                var maxlength = 6;
                var prefix = "VEH";
                var countrows = (from row in _db.MNT_Motor_Class select (int?)row.TranId).Max() ?? 0;
                string currentno = (countrows + 1).ToString();
                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                var VehicleClasscode = (prefix + result + currentno);
                motorclass.VehicleClassCode = VehicleClasscode;
                motorclass.VehicleClassDesc = this.VehicleClassDesc;
                motorclass.Status = this.Status;
                motorclass.CreatedBy = this.CreatedBy;
                motorclass.CreatedDate = DateTime.Now;
                _db.MNT_Motor_Class.AddObject(motorclass);
                _db.SaveChanges();
                this.TranId = motorclass.TranId;
                this.VehicleClassCode = VehicleClasscode;
                this.CreatedOn = (DateTime)motorclass.CreatedDate;
                return this;

            }


        }
        #endregion


    }
    #endregion

    #region Vehicle Make
    public class VehicleMakeModel : BaseModel
    {
        #region Properties
        public int? TranId { get; set; }
        public string SubClassCode { get; set; }
        public string MakeCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(VehicleMaster), ErrorMessageResourceName = "RFVVehicleMakeName")]
        public string MakeName { get; set; }
        [Required(ErrorMessageResourceType = typeof(VehicleMaster), ErrorMessageResourceName = "RFVStatus")]
        public string Status { get; set; }
        public List<SubClassListItem> SubClassList { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }

        public override string screenId
        {
            get
            {
                return "248";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "121";
            }

        }
        #endregion

        #region Static Methods
        public static List<VehicleMakeModel> FetchVehicleClass()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleMakeModel>();
            var Vehiclelist = (from x1 in _db.MNT_Motor_Make where x1.MakeCode.Contains("vem") && x1.status == "Active" select x1);
            if (Vehiclelist.Any())
            {
                foreach (var data in Vehiclelist)
                {
                    item.Add(new VehicleMakeModel() { TranId = data.TranId, MakeCode = data.MakeCode, MakeName = data.MakeName, Status = data.status });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public VehicleMakeModel VehicleMakeUpdate()
        {
            MCASEntities _db = new MCASEntities();
            MNT_Motor_Make motorclass;
            if (TranId.HasValue)
            {
                motorclass = _db.MNT_Motor_Make.Where(x => x.TranId == this.TranId).FirstOrDefault();
                motorclass.MakeCode = this.MakeCode;
                motorclass.status = this.Status;
                motorclass.MakeName = this.MakeName;
                motorclass.ModifiedBy = this.ModifiedBy;
                motorclass.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.MakeCode = motorclass.MakeCode;
                this.CreatedBy = motorclass.CreatedBy;
                this.CreatedOn = motorclass.CreatedDate == null ? DateTime.MinValue : (DateTime)motorclass.CreatedDate;
                this.ModifiedOn = motorclass.ModifiedDate;
                return this;

            }
            else
            {
                motorclass = new MNT_Motor_Make();
                var maxlength = 6;
                var prefix = "VEM";
                //var countrows = (from row in _db.MNT_Motor_Make select row.TranId).Max();
                var countrows = (from row in _db.MNT_Motor_Make select (int?)row.TranId).Max() ?? 0;
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var VehicleMakecode = (prefix + result + currentno);
                motorclass.MakeCode = VehicleMakecode;
                // motorclass.SubClassCode = this.SubClassCode;

                motorclass.MakeName = this.MakeName;
                motorclass.status = this.Status;
                motorclass.CreatedBy = this.CreatedBy;
                motorclass.CreatedDate = DateTime.Now;
                _db.MNT_Motor_Make.AddObject(motorclass);
                _db.SaveChanges();
                this.TranId = motorclass.TranId;
                this.MakeCode = VehicleMakecode;
                this.CreatedOn = (DateTime)motorclass.CreatedDate;
                return this;

            }



        }
        #endregion
    }

    #endregion

    #region Vehicle Bus Captain

    public class VehicleBusCaptainModel : BaseModel
    {

        #region properties
        public int? TranId { get; set; }
        private DateTime? _dateJoined = null;
        private DateTime? _dateResigned = null;
        [Required(ErrorMessage = "Bus Captain Code is required.")]
        public string BusCaptainCode { get; set; }
        [Required(ErrorMessage = "Bus Captain Name is required.")]
        public string BusCaptainName { get; set; }
        [Required(ErrorMessage = "NRIC/Passport No is required.")]
        public string NRICPassportNo { get; set; }
        //[Required(ErrorMessage = "Contact No is required.")]
        public string DateJoined1 { get; set; }

        public string DateResigned1 { get; set; }
        [Required(ErrorMessage = "Nationality is required.")]
        public string Nationality { get; set; }
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter Contact No in proper format.")]
        public string ContactNo { get; set; }
        [DisplayName("Date Joined")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateJoined
        {
            get { return _dateJoined; }
            set { _dateJoined = value; }
        }

        [DisplayName("Date Resigned")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateResigned
        {
            get { return _dateResigned; }
            set { _dateResigned = value; }
        }
        //public override string screenId
        //{
        //    get
        //    {
        //        return "252";
        //    }
        //}
        //public override string listscreenId
        //{

        //    get
        //    {
        //        return "119";
        //    }

        //}
        public override string screenId
        {
            get
            {
                return "302";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "301";
            }

        }
        #endregion

        #region Static Methods
        public static List<VehicleBusCaptainModel> FetchVehicleBusCaptain()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleBusCaptainModel>();
            var BusCaptainlist = (from x in _db.MNT_BusCaptain select x);
            if (BusCaptainlist.Any())
            {
                foreach (var data in BusCaptainlist)
                {
                    item.Add(new VehicleBusCaptainModel() { TranId = data.TranId, BusCaptainCode = data.BusCaptainCode, BusCaptainName = data.BusCaptainName, NRICPassportNo = data.NRICPassportNo, DateJoined = data.DateJoined, Nationality = data.Nationality, DateResigned = data.DateResigned });
                }
            }
            return item;
        }


        public static CommonUtilities.DatatableGrid FetchVehicleBusCaptainList(string BusCaptainCode, string Name, string PassportNo, string ContactNo, string Nationality, DateTime? DateJoined, DateTime? DateResigned, int draw, int start, int length, int sortColumn, string sortDirection, string RawUrl, bool permission, string Searchval, string search = "")
        {
            MCASEntities _db = new MCASEntities();
            List<CommonUtilities.DatatableGrid> item = new List<CommonUtilities.DatatableGrid>();
            List<string[]> res = new List<string[]>();
            List<string[]> sortlist = new List<string[]>();
            try
            {
                RawUrl = RawUrl.Replace("BusCaptainListingMasterJson", "VehicleBusCaptainEditor");
                var DateJoined2 = DateJoined == null ? "" : DateJoined.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                var DateResigned2 = DateResigned == null ? "" : DateResigned.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                var serachresult = (from bc in _db.MNT_BusCaptain select bc).ToList().Select(b =>
                                    new VehicleBusCaptainModel()
                                    {
                                        BusCaptainCode = b.BusCaptainCode == null ? "" : b.BusCaptainCode,
                                        BusCaptainName = b.BusCaptainName == null ? "" : b.BusCaptainName,
                                        NRICPassportNo = b.NRICPassportNo == null ? "" : b.NRICPassportNo,
                                        ContactNo = b.ContactNo == null ? "" : b.ContactNo,
                                        Nationality = b.Nationality == null ? "" : b.Nationality,
                                        DateJoined = b.DateJoined,
                                        DateJoined1 = b.DateJoined == null ? "" : b.DateJoined.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture),
                                        DateResigned = b.DateResigned,
                                        DateResigned1 = b.DateResigned == null ? "" : b.DateResigned.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture),
                                        TranId = b.TranId
                                    }
                    );

                var BusCaptainlist = serachresult.Where(x => x.BusCaptainCode.ToUpper().Contains(BusCaptainCode.ToUpper()) && x.BusCaptainName.ToUpper().Contains(Name.ToUpper()) && x.NRICPassportNo.ToUpper().Contains(PassportNo.ToUpper()) && x.ContactNo.ToUpper().Contains(ContactNo.ToUpper()) && x.Nationality.ToUpper().Contains(Nationality.ToUpper()) && x.DateJoined1.ToUpper().Contains(DateJoined2.ToUpper()) && x.DateResigned1.ToUpper().Contains(DateResigned2.ToUpper())).ToList();
                if (search != "")
                    BusCaptainlist = BusCaptainlist.Where(bl => bl.BusCaptainName.ToUpper().Contains(search.ToUpper()) || bl.BusCaptainCode.ToUpper().Contains(search.ToUpper()) || bl.DateJoined1.ToUpper().Contains(search.ToUpper()) || bl.DateResigned1.ToUpper().Contains(search.ToUpper()) || bl.Nationality.ToUpper().Contains(search.ToUpper()) || bl.NRICPassportNo.ToUpper().Contains(search.ToUpper())).ToList();

                res = BusCaptainlist.Select(cap => new string[]{
                                             cap.BusCaptainCode,
                                             cap.BusCaptainName,
                                             cap.NRICPassportNo,
                                             cap.DateJoined == null ? "" : cap.DateJoined.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]),
                                             cap.DateResigned == null ? "" : cap.DateResigned.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]),
                                             cap.Nationality,
                                             permission ? "<td><a class='btn btn-xs caption btn-warning' href='" + RawUrl + "?TranId=" + cap.TranId + "&amp;pageMode=Edit'>"+Resources.Masters.BusCaptain.Edit+"</a>&nbsp;<a class='btn btn-xs caption btn-info ' href='" + RawUrl + "?TranId=" + cap.TranId + "&amp;pageMode=View'>View</a></td>" : "<a class='btn btn-xs btn-info' href='" + RawUrl + "?TranId=" + cap.TranId + "&amp;pageMode=View'>"+Resources.Masters.BusCaptain.View+"</a></td>"
                                            }
                                        ).ToList();

                if (res.Any())
                {
                    sortlist = sortDirection == "asc" ? res.GetRange(start, Math.Min(length, res.Count - start)).OrderBy(x => x[sortColumn]).ToList() : res.GetRange(start, Math.Min(length, res.Count - start)).OrderByDescending(x => x[sortColumn]).ToList();
                }
                item.Add(new CommonUtilities.DatatableGrid() { data = sortlist });
                item.FirstOrDefault().draw = draw;
                item.FirstOrDefault().recordsFiltered = res.Count();
                item.FirstOrDefault().recordsTotal = BusCaptainlist.Count();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            _db.Dispose();
            return item.FirstOrDefault();
        }
        #endregion

        #region Methods


        public VehicleBusCaptainModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_BusCaptain buscaptaininfo;
            buscaptaininfo = obj.MNT_BusCaptain.Where(x => x.TranId == this.TranId.Value).FirstOrDefault();

            if (TranId.HasValue)
            {
                buscaptaininfo.BusCaptainCode = this.BusCaptainCode;
                buscaptaininfo.BusCaptainName = this.BusCaptainName;
                buscaptaininfo.NRICPassportNo = this.NRICPassportNo;
                buscaptaininfo.DateJoined = this.DateJoined;
                buscaptaininfo.DateResigned = this.DateResigned;
                buscaptaininfo.Nationality = this.Nationality;
                buscaptaininfo.ModifiedBy = this.ModifiedBy;
                buscaptaininfo.ModifiedDate = DateTime.Now;
                obj.SaveChanges();
                this.TranId = buscaptaininfo.TranId;
                this.CreatedBy = buscaptaininfo.CreatedBy;
                this.CreatedOn = buscaptaininfo.CreatedDate == null ? DateTime.MinValue : (DateTime)buscaptaininfo.CreatedDate;
                this.ModifiedOn = buscaptaininfo.ModifiedDate;
                return this;
            }
            else
            {
                buscaptaininfo = new MNT_BusCaptain();
                buscaptaininfo.BusCaptainCode = this.BusCaptainCode;
                buscaptaininfo.BusCaptainName = this.BusCaptainName;
                buscaptaininfo.NRICPassportNo = this.NRICPassportNo;
                buscaptaininfo.DateJoined = this.DateJoined;
                buscaptaininfo.DateResigned = this.DateResigned;
                buscaptaininfo.Nationality = this.Nationality;
                obj.MNT_BusCaptain.AddObject(buscaptaininfo);
                buscaptaininfo.CreatedBy = this.CreatedBy;
                buscaptaininfo.CreatedDate = DateTime.Now;
                obj.SaveChanges();
                this.BusCaptainCode = buscaptaininfo.BusCaptainCode;
                this.TranId = buscaptaininfo.TranId;
                this.CreatedOn = (DateTime)buscaptaininfo.CreatedDate;
                return this;

            }
        }
        #endregion




        public static VehicleBusCaptainModel FetchVehicleBusCaptainModel(VehicleBusCaptainModel objmodel, int? TranId)
        {
            MCASEntities _db = new MCASEntities();
            var buscaptainlist = (from d in _db.MNT_BusCaptain where d.TranId == TranId select d).FirstOrDefault();
            objmodel.TranId = buscaptainlist.TranId;
            objmodel.BusCaptainCode = buscaptainlist.BusCaptainCode;
            objmodel.BusCaptainName = buscaptainlist.BusCaptainName;
            objmodel.NRICPassportNo = buscaptainlist.NRICPassportNo;
            objmodel.DateJoined = buscaptainlist.DateJoined;
            objmodel.DateResigned = buscaptainlist.DateResigned;
            objmodel.Nationality = buscaptainlist.Nationality;
            objmodel.CreatedBy = buscaptainlist.CreatedBy == null ? " " : buscaptainlist.CreatedBy;
            if (buscaptainlist.CreatedDate != null)
                objmodel.CreatedOn = (DateTime)buscaptainlist.CreatedDate;
            else
                objmodel.CreatedOn = DateTime.MinValue;
            objmodel.ModifiedBy = buscaptainlist.ModifiedBy == null ? " " : buscaptainlist.ModifiedBy;
            objmodel.ModifiedOn = buscaptainlist.ModifiedDate;
            _db.Dispose();
            return objmodel;
        }

        public static IQueryable<VehicleBusCaptainModel> GetFetchResult(string BusCaptainCode, string Name, string PassportNo, string ContactNo, string Nationality, DateTime? DateJoined, DateTime? DateResigned)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleBusCaptainModel>();
            var serachresult1 = (from buscaptain in _db.MNT_BusCaptain select buscaptain).ToList();
            var DateJoined2 = DateJoined == null ? "" : DateJoined.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
            var DateResigned2 = DateResigned == null ? "" : DateResigned.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
            foreach (var data in serachresult1)
            {
                item.Add(new VehicleBusCaptainModel()
                {
                    BusCaptainCode = data.BusCaptainCode == null ? "" : data.BusCaptainCode,
                    BusCaptainName = data.BusCaptainName == null ? "" : data.BusCaptainName,
                    NRICPassportNo = data.NRICPassportNo == null ? "" : data.NRICPassportNo,
                    ContactNo = data.ContactNo == null ? "" : data.ContactNo,
                    Nationality = data.Nationality == null ? "" : data.Nationality,
                    DateJoined = data.DateJoined,
                    DateJoined1 = data.DateJoined == null ? "" : data.DateJoined.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture),
                    DateResigned = data.DateResigned,
                    DateResigned1 = data.DateResigned == null ? "" : data.DateResigned.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture),
                    TranId = data.TranId

                });
            };

            var searchResult1 = item.Where(x => x.BusCaptainCode.Contains(BusCaptainCode) && x.BusCaptainName.Contains(Name) && x.NRICPassportNo.Contains(PassportNo) && x.ContactNo.Contains(ContactNo) && x.Nationality.Contains(Nationality) && x.DateJoined1.Contains(DateJoined2) && x.DateResigned1.Contains(DateResigned2)).ToList();

            var searchResult = searchResult1.Select(x => new VehicleBusCaptainModel
                                {
                                    BusCaptainCode = x.BusCaptainCode,
                                    BusCaptainName = x.BusCaptainName,
                                    NRICPassportNo = x.NRICPassportNo,
                                    ContactNo = x.ContactNo,
                                    Nationality = x.Nationality,
                                    DateJoined = x.DateJoined,
                                    DateResigned = x.DateResigned,
                                    TranId = x.TranId
                                }).AsQueryable();
            _db.Dispose();

            return searchResult;
        }



    }


    #endregion




    #region Vehicle Listing Upload

    public class VehicleUploadViewModel : BaseModel
    {
        public string UploadType { get; set; }
        public string UploadFileRefNo { get; set; }
        public string Status { get; set; }
        public List<Uploadstatuslist> ListUploadstatus { get; set; }
        private DateTime? _uploadToDate = null;
        private DateTime? _uploadFromDate = null;
        [DisplayName("Upload To Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? UploadToDate
        {
            get { return _uploadToDate; }
            set { _uploadToDate = value; }
        }
        [DisplayName("Upload From Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? UploadFromDate
        {
            get { return _uploadFromDate; }
            set { _uploadFromDate = value; }
        }
        public List<VehicleUploadModel> ListVehicleUplaod { get; set; }
        public override string screenId
        {
            get
            {
                return "299";
            }
            set
            {
                base.screenId = "299";
            }
        }
    }



    public class VehicleUploadModel : BaseModel
    {


        #region properties
        private int _itemsPerPage = 10;
        #region ForangularjSGrid
        public int itemsPerPage { get { return _itemsPerPage; } set { _itemsPerPage = value; } }
        public int? currentPage { get; set; }
        public int pageno { get; set; }
        public string orderByField { get; set; }
        public string orderByColumn { get; set; }
        public bool reverseSort { get; set; }
        public string sortOrder { get; set; }
        public string query { get; set; }
        public bool direction { get; set; }
        public int? datalength { get; set; }
        public int? startlength { get; set; }
        public int? endlength { get; set; }
        #endregion

        public int? UploadFileId { get; set; }
        public string UploadFileRefNo { get; set; }
        public string UploadFileName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UploadedDate { get; set; }
        public int? TotalRecords { get; set; }
        public int? UplodedSuccess { get; set; }
        public int? UploadedFailed { get; set; }
        public string Status { get; set; }
        public string inputFileName { get; set; }
        public string UploadPath { get; set; }
        public string inputFileRefNo { get; set; }
        public string inputUploadDate { get; set; }
        public string VehicleRegNo { get; set; }
        public List<VehicleUploadModel> list1 { get; set; }

        public int? VehicleMasterId { get; set; }
        [Required(ErrorMessage = "Bus No is required.")]
        public string BusNo { get; set; }
        [Required(ErrorMessage = "Chasis No is required.")]
        public string ChasisNo { get; set; }
        public string VehicleClassCode { get; set; }
        public string ModelDescription { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Aircon { get; set; }
        public string Axle { get; set; }
        public string Training { get; set; }
        public List<VehicleListItem> vehicleMakelist { get; set; }
        public List<VehicleListItem> VehicleClasslist { get; set; }
        public List<VehicleListItem> VehicleModellist { get; set; }
        public string Hselect { get; set; }
        public string Hgetval { get; set; }
        public string Hgetval1 { get; set; }
        //public override string screenId
        //{
        //    get
        //    {
        //        return base.screenId;
        //    }
        //    set
        //    {
        //        base.screenId = "101";
        //    }
        //}
        //public override string listscreenId
        //{
        //    get
        //    {
        //        return base.listscreenId;
        //    }
        //    set
        //    {
        //        base.listscreenId = "S_ADMN";
        //    }
        //}
        public override string screenId
        {
            get
            {
                return "300";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "299";
            }

        }
        #endregion

        #region static Method
        public static List<VehicleUploadModel> FetchVehicleUploadListing()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleUploadModel>();
            try
            {
                var list = (from x in _db.MNT_VehicleListingMaster select x);
                var vehicle = (from x in _db.MNT_MOTOR_MODEL join c in _db.MNT_Motor_Make on x.MakeCode equals c.MakeCode join b in _db.MNT_Motor_Class on x.VehicleClassCode equals b.VehicleClassCode select x);
                if (vehicle.Any())
                {
                    foreach (var data in list)
                    {

                        item.Add(new VehicleUploadModel()
                        {
                            VehicleMasterId = data.VehicleMasterId,
                            BusNo = data.VehicleRegNo ?? "",
                            ChasisNo = data.ModelDescription ?? "",
                            VehicleClassCode = (from s in _db.MNT_Motor_Class where s.VehicleClassCode == data.VehicleClassCode select s.VehicleClassDesc).FirstOrDefault() ?? "",
                            Make = (from p in _db.MNT_Motor_Make where p.MakeCode == data.VehicleMakeCode select p.MakeName).FirstOrDefault() ?? "",
                            Model = (from x in _db.MNT_MOTOR_MODEL where x.ModelCode == data.VehicleModelCode select x.ModelName).FirstOrDefault() ?? "",
                            Type = data.Type ?? "",
                            Aircon = data.Aircon ?? "",
                            Axle = data.Axle ?? ""
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }

        public static string saveexelfile(string fileLocation, string fileExtension, string filename)
        {
            DataSet ds = new DataSet();
            string excelConnectionString = string.Empty;
            string exists = string.Empty;
            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            if (fileExtension == "xls")
            {
                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (fileExtension == "xlsx")
            {
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
            excelConnection.Open();
            try
            {
                DataTable dt = new DataTable();

                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }
                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                string query = string.Format("Select * from [{0}]", excelSheets[0]);
                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                {
                    dataAdapter.Fill(ds);
                }
                if (ds.Tables[0].Columns.Count != 6)
                {
                    try
                    {
                        if (File.Exists(fileLocation))
                        {
                            excelConnection.Dispose();
                            File.Delete(fileLocation);
                        }
                    }
                    catch (Exception) { }
                    return "Cannot Save As Number Of Columns Are Not Matching.";
                }
                string[] columnNames = (from dc in ds.Tables[0].Columns.Cast<DataColumn>()
                                        select dc.ColumnName).ToArray();
                if (ConvertStringArrayToString(columnNames) != "VEHICLE_REGISTRATION_NO.VEHICLE_MAKE.VEHICLE_MODEL.VEHICLE_CLASS.MODEL_DESCRIPTION.BUS_CAPTAIN.")
                {
                    try
                    {
                        if (File.Exists(fileLocation))
                        {
                            excelConnection.Dispose();
                            File.Delete(fileLocation);
                        }
                    }
                    catch (Exception) { }
                    return "Cannot Save As Columns Names Are Not Matching.";
                }
                //   dt.DefaultView.ToTable(true, "VEHICLE_REGISTRATION_NO");



                if (ds.Tables[0].Rows.Count == 0)
                {
                    try
                    {
                        if (File.Exists(fileLocation))
                        {
                            excelConnection.Dispose();
                            File.Delete(fileLocation);
                        }
                    }
                    catch (Exception) { }
                    return "Cannot Save As Their Is No Data To Insert.";
                }
                else
                {
                    int j = 0, k = 0, m = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        MCASEntities obj = new MCASEntities();
                        var mst = ds.Tables[0].Rows[i][0].ToString();
                        var REGNO = (from l in obj.MNT_VehicleListingMaster where l.VehicleRegNo == mst select l).FirstOrDefault();
                        if (mst == "")
                        {
                            m--;
                        }
                        else if (mst.Length > 10)
                        {
                            k++;
                            exists = Convert.ToString(ds.Tables[0].Rows[i][0]) + "*" + exists;
                            var res1 = obj.Proc_MIG_IL_VEHICLE_DETAIL_Save(0, ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString(), "Not Inserted, Max Length Of Vehicle Reg is 10", filename);
                        }
                        else if (REGNO == null)
                        {
                            j++;
                            var res = obj.Proc_MNT_VehicleListingMaster_Save(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString());
                            var res1 = obj.Proc_MIG_IL_VEHICLE_DETAIL_Save(0, ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString(), "Insert Sucessfully", filename);

                        }
                        else
                        {
                            k++;
                            exists = Convert.ToString(ds.Tables[0].Rows[i][0]) + "*" + exists;
                            var res1 = obj.Proc_MIG_IL_VEHICLE_DETAIL_Save(0, ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString(), "Already Exists", filename);
                        }
                    }
                    return "T" + "-" + m + "," + j + "," + k;
                }
            }
            catch (Exception)
            {
                try
                {
                    if (File.Exists(fileLocation))
                    {
                        excelConnection.Dispose();
                        File.Delete(fileLocation);
                    }
                }
                catch (Exception) { }
                return "F";
            }
            finally
            {
                excelConnection.Dispose();
            }
        }
        static string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();
        }

        public VehicleUploadModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_VehicleListingMaster VehicleMaster;
            VehicleMaster = _db.MNT_VehicleListingMaster.Where(x => x.VehicleMasterId == this.VehicleMasterId).FirstOrDefault();
            if (VehicleMasterId.HasValue)
            {

                VehicleMaster.VehicleRegNo = this.BusNo;
                VehicleMaster.VehicleClassCode = this.VehicleClassCode != "-1" ? this.VehicleClassCode : null;
                VehicleMaster.VehicleMakeCode = this.Hgetval;
                VehicleMaster.VehicleModelCode = this.Hgetval1;
                VehicleMaster.ModelDescription = this.ChasisNo;
                VehicleMaster.Type = this.Type;
                VehicleMaster.Aircon = this.Aircon;
                VehicleMaster.Axle = this.Axle;
                VehicleMaster.IS_ACTIVE = "Y";
                VehicleMaster.ModifiedBy = ModifiedBy;
                VehicleMaster.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.CreatedBy = VehicleMaster.CreatedBy;
                this.CreatedOn = VehicleMaster.CreatedDate == null ? DateTime.MinValue : (DateTime)VehicleMaster.CreatedDate;
                this.ModifiedOn = VehicleMaster.ModifiedDate;
                return this;

            }
            else
            {
                VehicleMaster = new MNT_VehicleListingMaster();
                VehicleMaster.VehicleRegNo = this.BusNo;
                VehicleMaster.VehicleClassCode = this.VehicleClassCode != "-1" ? this.VehicleClassCode : null;
                VehicleMaster.VehicleMakeCode = this.Hgetval;
                VehicleMaster.VehicleModelCode = this.Hgetval1;
                VehicleMaster.ModelDescription = this.ChasisNo;
                VehicleMaster.Type = this.Type;
                VehicleMaster.Aircon = this.Aircon;
                VehicleMaster.Axle = this.Axle;
                VehicleMaster.IS_ACTIVE = "Y";
                VehicleMaster.CreatedBy = CreatedBy;
                VehicleMaster.CreatedDate = DateTime.Now;
                _db.MNT_VehicleListingMaster.AddObject(VehicleMaster);
                _db.SaveChanges();
                this.VehicleMasterId = VehicleMaster.VehicleMasterId;
                this.CreatedOn = (DateTime)VehicleMaster.CreatedDate;
                return this;
            }
        }
        #endregion



        public static string FetchVehicleMake(string strMake)
        {
            MCASEntities obj = new MCASEntities();
            List<MakeAndModel> sgf = new List<MakeAndModel>();


            var query = (from x in obj.MNT_Motor_Make where x.MakeName.ToLower().Trim() == strMake.ToLower().Trim() select x.MakeCode).FirstOrDefault();

            return query.ToString();

        }

        public static List<VehicleUploadModel> SearchVehicleUploadHistory(string FileRefNo, string status, DateTime? uploadDate, DateTime? UploadToDate, string uploadtype)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<VehicleUploadModel>();
            DateTime dtToDate = DateTime.Now;
            if (UploadToDate != null)
            {
                dtToDate = Convert.ToDateTime(UploadToDate).AddDays(1);
            }

            item = (from x in _db.MNT_VehicleListingUpload
                    where (FileRefNo == "" || x.UploadFileRefNo.ToLower().Contains(FileRefNo.ToLower()))
                    && (status == "" || x.Status.ToLower().Contains(status.ToLower()))
                    && (uploadDate == null || x.UploadedDate >= uploadDate)
                    && (UploadToDate == null || x.UploadedDate < dtToDate)
                    && (uploadtype == null || x.UploadFileRefNo.ToLower().Contains(uploadtype.ToLower()))

                    select new VehicleUploadModel()
                    {
                        UploadFileId = x.UploadFileId,
                        UploadFileRefNo = x.UploadFileRefNo,
                        Status = x.Status,
                        UploadedDate = x.UploadedDate,
                        TotalRecords = x.TotalRecords,
                        UplodedSuccess = x.UplodedSuccess,
                        UploadedFailed = x.UploadedFailed
                    }).OrderByDescending(t => t.UploadFileId).ToList();
            return item;

        }

        public static List<Uploadstatuslist> fetch()
        {
            List<Uploadstatuslist> item = new List<Uploadstatuslist>();
            item.Add(new Uploadstatuslist() { Text = "[Select...]" });
            item.Add(new Uploadstatuslist() { Id = "Incomplete", Text = "Incomplete" });
            item.Add(new Uploadstatuslist() { Id = "Failed", Text = "Failed" });
            item.Add(new Uploadstatuslist() { Id = "Success", Text = "Success" });
            return item;

        }
    }


    #endregion

    public class MakeAndModel
    {
        public string MakeCode { get; set; }
        public string MakeName { get; set; }

    }
    public class Model
    {
        public string ModelName { get; set; }
        public string ModelCode { get; set; }

    }
    #region Depot Master

    public class DepotMasterModel : BaseModel
    {
        public DepotMasterModel()
        {
            DepotAddress = new AddressModel();
        }
        #region properties
        public int? DepotId { get; set; }

        public string DepotCode { get; set; }

        //  [Required(ErrorMessage = "Depot Reference is required.")]
        public string DepotReference { get; set; }

        //   [Required(ErrorMessage = "Person in Charge required.")]
        public string ContactPerson { get; set; }

        public string ErrorMsg { get; set; }

        public override string screenId
        {
            get
            {
                return "236";
            }
            set
            {
                base.screenId = "236";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "106";
            }
            set
            {
                base.listscreenId = "106";
            }
        }

        //[Required(ErrorMessage = "Address is required.")]
        //public string Address { get; set; }

        //public string City = "Singapore";

        //[Required(ErrorMessage = "State is required.")]
        //public string State { get; set; }

        //public string Country = "Singapore";

        //[Required(ErrorMessage = "Postal Code  is required.")]
        //[RegularExpression(@"^[0-9]{0,10}$", ErrorMessage = "Please enter Postal code in proper format.")]
        //public string PostalCode { get; set; }
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter phone no in proper format.")]
        //[Required(ErrorMessage = "Telephone No(off) is required.")]
        //public string TelephoneOff { get; set; }
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter mobile no in proper format.")]
        //[Required(ErrorMessage = "Mobile No is required.")]
        //public string MobileNo { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Please enter EMail in proper format.")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Fax is required.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter Fax no in proper format.")]
        //public string Fax { get; set; }

        //[Required(ErrorMessage = "Person In Charge is required.")]
        //public string PersonInCharge { get; set; }

        //public string Status { get; set; }

        public List<LookUpListItems> Statuslist { get; set; }

        public List<ProviceList> ProvinceList { get; set; }
        private AddressModel _address = new AddressModel();
        public AddressModel DepotAddress
        {
            get { return _address; }
            set { _address = value; }
        }
        #endregion

        #region Static Methods
        public static List<DepotMasterModel> FetchDepotMaster()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DepotMasterModel>();
            var DepotMasterList = (from x in _db.MNT_DepotMaster select x);
            if (DepotMasterList.Any())
            {
                foreach (var data in DepotMasterList)
                {
                    item.Add(new DepotMasterModel()
                    {
                        DepotId = data.DepotId,
                        DepotCode = data.DepotCode,
                        DepotAddress = new AddressModel()
                        {
                            Address1 = data.Address1,
                            OffNo1 = data.TelephoneOff,
                            City = data.City,
                            Country = data.Country,
                            EmailAddress1 = data.Email,
                            InsurerName = data.CompanyName
                        }
                    });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public DepotMasterModel DepotMasterUpdate()
        {
            MCASEntities _db = new MCASEntities();
            MNT_DepotMaster depot = new MNT_DepotMaster();
            if (DepotId.HasValue)
            {
                depot = _db.MNT_DepotMaster.Where(x => x.DepotId == this.DepotId).FirstOrDefault();
                depot.DepotCode = this.DepotCode;
                depot.DepotReference = "DEREF-003";
                depot.CompanyName = this.DepotAddress.InsurerName;
                depot.Address1 = this.DepotAddress.Address1;
                depot.Address2 = this.DepotAddress.Address2;
                depot.Address3 = this.DepotAddress.Address3;
                depot.City = this.DepotAddress.City;
                depot.State = this.DepotAddress.State;
                depot.Country = this.DepotAddress.Country;
                depot.PostalCode = this.DepotAddress.PostalCode;
                depot.PersonInCharge = this.DepotAddress.FirstContactPersonName;
                depot.Email = this.DepotAddress.EmailAddress1;
                depot.TelephoneOff = this.DepotAddress.OffNo1;
                depot.MobileNo = this.DepotAddress.MobileNo1;
                depot.Fax = this.DepotAddress.Fax1;
                depot.ContactPerson = this.DepotAddress.SecondContactPersonName;
                depot.EmailAddress2 = this.DepotAddress.EmailAddress2;
                depot.OffNo2 = this.DepotAddress.OffNo2;
                depot.MobileNo2 = this.DepotAddress.MobileNo2;
                depot.Fax2 = this.DepotAddress.Fax2;
                depot.Remarks = this.DepotAddress.Remarks;
                depot.EffectiveFrom = this.DepotAddress.EffectiveFromDate;
                depot.EffectiveTo = this.DepotAddress.Effectiveto;
                depot.WorkShopType = this.DepotAddress.InsurerType.ToString();
                if (this.DepotAddress.Status == "Active")
                {
                    depot.Status = "1";
                }
                else
                {
                    depot.Status = "0";
                }
                depot.ModifiedBy = this.ModifiedBy;
                depot.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.DepotId = depot.DepotId;
                this.CreatedBy = depot.CreatedBy == null ? " " : depot.CreatedBy;
                if (depot.CreatedDate != null)
                    this.CreatedOn = (DateTime)depot.CreatedDate;
                else
                    this.CreatedOn = DateTime.MinValue;
                this.ModifiedBy = depot.ModifiedBy == null ? " " : depot.ModifiedBy;
                this.ModifiedOn = depot.ModifiedDate;
                return this;

            }
            else
            {
                var maxlength = 5;
                var prefix = "DE";
                var countrows = (from row in _db.MNT_DepotMaster select (int?)row.DepotId).Max() ?? 0;
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                var Depocode = (prefix + result + currentno);
                depot.DepotCode = Depocode;
                depot.DepotReference = "DEREF-003";
                depot.CompanyName = this.DepotAddress.InsurerName;
                depot.Address1 = this.DepotAddress.Address1;
                depot.Address2 = this.DepotAddress.Address2;
                depot.Address3 = this.DepotAddress.Address3;
                depot.City = this.DepotAddress.City;

                depot.State = this.DepotAddress.State;
                depot.Country = this.DepotAddress.Country;
                depot.PostalCode = this.DepotAddress.PostalCode;
                depot.PersonInCharge = this.DepotAddress.FirstContactPersonName;
                depot.Email = this.DepotAddress.EmailAddress1;
                depot.TelephoneOff = this.DepotAddress.OffNo1;
                depot.MobileNo = this.DepotAddress.MobileNo1;
                depot.Fax = this.DepotAddress.Fax1;
                depot.ContactPerson = this.DepotAddress.SecondContactPersonName;
                depot.EmailAddress2 = this.DepotAddress.EmailAddress2;
                depot.OffNo2 = this.DepotAddress.OffNo2;
                depot.MobileNo2 = this.DepotAddress.MobileNo2;
                depot.Fax2 = this.DepotAddress.Fax2;
                depot.Remarks = this.DepotAddress.Remarks;
                depot.EffectiveFrom = this.DepotAddress.EffectiveFromDate;
                depot.EffectiveTo = this.DepotAddress.Effectiveto;
                depot.WorkShopType = this.DepotAddress.InsurerType.ToString();
                if (this.DepotAddress.Status == "Active")
                {
                    depot.Status = "1";
                }
                else
                {
                    depot.Status = "0";
                }
                depot.CreatedBy = this.CreatedBy;
                depot.CreatedDate = DateTime.Now;
                _db.MNT_DepotMaster.AddObject(depot);
                _db.SaveChanges();
                var ecode = Convert.ToString(depot.DepotId);
                int list = (from l in _db.MNT_TransactionAuditLog.Where(x => x.UserName == depot.CreatedBy && x.Actions == "I" && x.TableName == "MNT_DepotMaster") orderby l.TimeStamp select l.TranAuditId).ToList().LastOrDefault();
                var c = _db.MNT_TransactionAuditLog.Where(x => x.TranAuditId == list).FirstOrDefault();
                c.EntityCode = ecode;
                _db.SaveChanges();
                this.DepotCode = depot.DepotCode;
                this.DepotId = depot.DepotId;
                this.CreatedOn = (DateTime)depot.CreatedDate;
                return this;

            }
        }


        #endregion
    }


    #endregion

}
