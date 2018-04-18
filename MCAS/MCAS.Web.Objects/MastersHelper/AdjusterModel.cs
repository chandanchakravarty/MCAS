using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;

namespace MCAS.Web.Objects.MastersHelper
{
    public class AdjusterModel:BaseModel
    {
        MCASEntities _db = new MCASEntities();
        public AdjusterModel() {
            AdjusterAddress = new AddressModel();
        }
        #region Properties
        public int? AdjusterId { get; set; }

        private AddressModel _address = new AddressModel();
        public AddressModel AdjusterAddress
        {
            get { return _address; }
            set { _address = value; }
        }
        public string AdjusterCode { get; set; }
        public string SurveyorCode { get; set; }
        public string SolicitorCode { get; set; }
      //  public string SurveyorName { get; set; }

     //   [Required(ErrorMessage = "Company name is required.")]
        public string AdjusterName { get; set; }
        public string AdjusterTypeCode { get; set; }
       

        

        public string GST { get; set; }

        public decimal? VATPer { get; set; }
        public string VATNo { get; set; }
       
        public string WHTPer { get; set; }
        public string VAT { get; set; }
        public string WHT { get; set; }
        public string AdjType { get; set; }
        public string AdjSrc { get; set; }

        public string ProductCode { get; set; }

        // Surveyor
        public override string screenId
        {
            get
            {
                return "241";
            }
            set
            {
                base.screenId = "241";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "109";
            }
            set
            {
                base.listscreenId = "109";
            }
        }


        // adjuster
        //public override string screenId
        //{
        //    get
        //    {
        //        return "240";
        //    }
        //    set
        //    {
        //        base.screenId = "240";
        //    }
        //}
        //public override string listscreenId
        //{
        //    get
        //    {
        //        return "108";
        //    }
        //    set
        //    {
        //        base.listscreenId = "108";
        //    }
        //}

        // solicitor lawyer
        //public override string screenId
        //{
        //    get
        //    {
        //        return "241";
        //    }
        //    set
        //    {
        //        base.screenId = "241";
        //    }
        //}
        //public override string listscreenId
        //{
        //    get
        //    {
        //        return "109";
        //    }
        //    set
        //    {
        //        base.listscreenId = "109";
        //    }
        //}

        #endregion
        #region
        public List<ProductsListItems> ProductList { get; set; }
        public List<UserCityListItem> citylist { get; set; }
        public List<UserCountryListItems> countrylist { get; set; }
        public List<LookUpListItems> SolicitorTypelist { get; set; }
        public List<LookUpListItems> AdjusterSourcelist { get; set; }
        public List<ProviceList> ProvinceList { get; set; }
        public List<GSTListItem> GSTList { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }
        public List<CountryModel> usercountrylist { get; set; }

       #endregion

        #region Methods

        public static List<AdjusterModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<AdjusterModel>();
            var Adjusterlist = (from x in _db.MNT_Adjusters select x);
            if (Adjusterlist.Any())
            {
                foreach (var data in Adjusterlist)
                {
                    item.Add(new AdjusterModel()
                    {
                        AdjusterId = data.AdjusterId,
                        AdjusterCode = data.AdjusterCode,
                        AdjusterName = data.AdjusterName,
                        AdjusterTypeCode = data.AdjusterTypeCode,
                        AdjusterAddress = new AddressModel() { 
                            Status=data.Status,
                            Address1=data.Address1
                        }
                    });
                }
            }
            return item;
        }
        
        public static List<AdjusterModel> FetchAdjuster() {
            MCASEntities _db = new MCASEntities();
            var item = new List<AdjusterModel>();
            var Adjusterlist = (from x in _db.MNT_Adjusters where x.AdjusterCode.Contains("ADJ") orderby x.AdjusterName select x);
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
          //  item.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
            return item;
        }

        public static List<AdjusterModel> FetchSurveyor() {
            MCASEntities _db = new MCASEntities();
            var item = new List<AdjusterModel>();  
            var Adjusterlist = (from x in _db.MNT_Adjusters where x.AdjusterCode.Contains("SVY") orderby x.AdjusterName select x);
            if (Adjusterlist.Any())
            {
                foreach (var data in Adjusterlist)
                {
                    item.Add(new AdjusterModel() { AdjusterId = data.AdjusterId,
                        AdjusterName=data.AdjusterName, 
                        AdjusterTypeCode=data.AdjusterTypeCode,
                        AdjusterCode = data.AdjusterCode ,
                        AdjusterAddress=new AddressModel() {
                        Status = data.Status
                        }
                      });
                }
            }
            //item.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
            return item;
        }


        public static List<AdjusterModel> FetchSolicitor()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<AdjusterModel>();
            var Adjusterlist = (from x in _db.MNT_Adjusters where x.AdjusterCode.Contains("SOL") orderby x.AdjusterName select x);
            if (Adjusterlist.Any())
            {
                foreach (var data in Adjusterlist)
                {
                    item.Add(new AdjusterModel() { AdjusterId = data.AdjusterId,
                        AdjusterName = data.AdjusterName, 
                        AdjusterTypeCode = data.AdjusterTypeCode,
                        AdjusterCode = data.AdjusterCode,
                        AdjusterAddress=new AddressModel(){
                            Status = data.Status
                        }

                     });
                   
                }
            }
          //  item.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
            return item;
           
        }

        

        public static List<MNT_City> GetCityList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<MNT_City>();
            var citylist = (from x in _db.MNT_City select x);
            if (citylist.Any())
            {
                foreach (var c in citylist)
                {
                    item.Add(new MNT_City() { CityId = c.CityId, CityName = c.CityName });
                }
            }
            return item;
        }



        

        public AdjusterModel AdjusterUpdate() {
            MNT_Adjusters adj;
            if (AdjusterId.HasValue)
            {
                adj = _db.MNT_Adjusters.Where(x => x.AdjusterId == this.AdjusterId).FirstOrDefault();
                adj.AdjusterCode = this.AdjusterCode;
                adj.AdjusterName = this.AdjusterAddress.InsurerName;
                adj.AdjusterTypeCode = "ADJ";
                adj.Address1 = this.AdjusterAddress.Address1;
                adj.Address2 = this.AdjusterAddress.Address2;
                adj.Address3 = this.AdjusterAddress.Address3;
                adj.City = this.AdjusterAddress.City;
                adj.Country = this.AdjusterAddress.Country;
                adj.Province = this.AdjusterAddress.State;
                adj.ProductCode = "ENG";
                adj.EMail = this.AdjusterAddress.EmailAddress1;
                adj.FaxNo = this.AdjusterAddress.Fax1;
                adj.MobileNo = this.AdjusterAddress.MobileNo1;
                adj.PostCode = this.AdjusterAddress.PostalCode;
                adj.TelNoOff = this.AdjusterAddress.OffNo1;
                adj.ConPer = this.AdjusterAddress.FirstContactPersonName;
                adj.Classification = this.AdjusterAddress.SecondContactPersonName;
                adj.EmailAddress2 = this.AdjusterAddress.EmailAddress2;
                adj.Fax2 = this.AdjusterAddress.Fax2;
                adj.MobileNo2 = this.AdjusterAddress.MobileNo2;
                adj.OffNo2 = this.AdjusterAddress.OffNo2;
                adj.VATNo = this.VATNo;
                adj.InsurerType = this.AdjusterAddress.InsurerType.ToString();
                adj.Status = this.AdjusterAddress.Status.ToString();
                adj.EffectiveFrom = this.AdjusterAddress.EffectiveFromDate;
                adj.EffectiveTo = this.AdjusterAddress.Effectiveto;
                adj.Remarks = this.AdjusterAddress.Remarks;
                adj.VATNo = this.VATNo;
                adj.AdjType = this.AdjType;
                adj.AdjSrc = this.AdjSrc;
                adj.VAT = this.GST;
                adj.VATPer = this.VATPer;
                adj.ModifiedBy = this.ModifiedBy;
                adj.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.AdjusterCode = adj.AdjusterCode;
                this.CreatedBy = adj.CreatedBy;
                this.CreatedOn = adj.CreatedDate == null ? DateTime.MinValue : (DateTime)adj.CreatedDate;
                this.ModifiedOn = adj.ModifiedDate;
                _db.Dispose();
                return this;
            }
            else
            {
                adj = new MNT_Adjusters();
                var maxlength = 6;
                var prefix = "ADJ";
                string result1;
                var countrows = (from row in _db.MNT_Adjusters where row.AdjusterTypeCode == "ADJ" select (int?)row.AdjusterId).Max() ?? 0;
                if (countrows == 0)
                {
                    result1 = "0";
                }
                else
                {
                    var nxtrows = (from row in _db.MNT_Adjusters where (int?)row.AdjusterId == countrows select row.AdjusterCode).FirstOrDefault();
                    // Select only those characters that are numbers
                    result1 = new String(nxtrows.Where(x => Char.IsDigit(x)).ToArray());
                }
                string currentno = (Convert.ToInt32(result1) + 1).ToString();
                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                var Adjustercode = (prefix + result + currentno);
                adj.AdjusterCode = Adjustercode;
                adj.AdjusterName = this.AdjusterAddress.InsurerName;
                adj.AdjusterTypeCode = "ADJ";
                adj.Address1 = this.AdjusterAddress.Address1;
                adj.Address2 = this.AdjusterAddress.Address2;
                adj.Address3 = this.AdjusterAddress.Address3;
                adj.City = this.AdjusterAddress.City;
                adj.Country = this.AdjusterAddress.Country;
                adj.Province = this.AdjusterAddress.State;
                adj.ProductCode = "ENG";
                adj.EMail = this.AdjusterAddress.EmailAddress1;
                adj.FaxNo = this.AdjusterAddress.Fax1;
                adj.MobileNo = this.AdjusterAddress.MobileNo1;
                adj.PostCode = this.AdjusterAddress.PostalCode;
                adj.TelNoOff = this.AdjusterAddress.OffNo1;
                adj.ConPer = this.AdjusterAddress.FirstContactPersonName;
                adj.Classification = this.AdjusterAddress.SecondContactPersonName;
                adj.EmailAddress2 = this.AdjusterAddress.EmailAddress2;
                adj.Fax2 = this.AdjusterAddress.Fax2;
                adj.MobileNo2 = this.AdjusterAddress.MobileNo2;
                adj.OffNo2 = this.AdjusterAddress.OffNo2;
                adj.InsurerType = this.AdjusterAddress.InsurerType.ToString();
                adj.Status = this.AdjusterAddress.Status.ToString();
                adj.EffectiveFrom = this.AdjusterAddress.EffectiveFromDate;
                adj.EffectiveTo = this.AdjusterAddress.Effectiveto;
                adj.Remarks = this.AdjusterAddress.Remarks;
                adj.ProductCode = "ENG";
                adj.AdjType = this.AdjType;
                adj.AdjSrc = this.AdjSrc;
                adj.VAT = this.GST;
                adj.VATPer = this.VATPer;
                adj.VATNo = this.VATNo;
                adj.CreatedBy = this.CreatedBy;
                adj.CreatedDate = DateTime.Now;
                _db.MNT_Adjusters.AddObject(adj);
                _db.SaveChanges();
                var ecode = Convert.ToString(adj.AdjusterId);
                int list = (from l in _db.MNT_TransactionAuditLog.Where(x => x.UserName == adj.CreatedBy && x.Actions == "I" && x.TableName == "MNT_Adjusters") orderby l.TimeStamp select l.TranAuditId).ToList().LastOrDefault();
                var c = _db.MNT_TransactionAuditLog.Where(x => x.TranAuditId == list).FirstOrDefault();
                c.EntityCode = ecode;
                _db.SaveChanges();
                this.AdjusterId = adj.AdjusterId;
                this.AdjusterCode = adj.AdjusterCode;
                this.CreatedOn = (DateTime)adj.CreatedDate;
                _db.Dispose();
                return this;
            }
        }

        public AdjusterModel SurveyUpdate() {
            MNT_Adjusters adj;
            if (AdjusterId.HasValue) {
                adj = _db.MNT_Adjusters.Where(x => x.AdjusterId == this.AdjusterId).FirstOrDefault();
                adj.AdjusterCode = this.AdjusterCode;
                adj.AdjusterName = this.AdjusterAddress.InsurerName;
                adj.AdjusterTypeCode = "SVY";
                adj.Address1 = this.AdjusterAddress.Address1;
                adj.Address2 = this.AdjusterAddress.Address2;
                adj.Address3 = this.AdjusterAddress.Address3;
                adj.City = this.AdjusterAddress.City;
                adj.Country = this.AdjusterAddress.Country;
                adj.Province = this.AdjusterAddress.State;
                adj.ProductCode = "ENG";
                adj.EMail = this.AdjusterAddress.EmailAddress1;
                adj.FaxNo = this.AdjusterAddress.Fax1;
                adj.MobileNo = this.AdjusterAddress.MobileNo1;
                adj.PostCode = this.AdjusterAddress.PostalCode;
                adj.TelNoOff = this.AdjusterAddress.OffNo1;
                adj.ConPer = this.AdjusterAddress.FirstContactPersonName;
                adj.Classification = this.AdjusterAddress.SecondContactPersonName;
                adj.EmailAddress2 = this.AdjusterAddress.EmailAddress2;
                adj.Fax2 = this.AdjusterAddress.Fax2;
                adj.MobileNo2 = this.AdjusterAddress.MobileNo2;
                adj.OffNo2 = this.AdjusterAddress.OffNo2;
                adj.VATNo = this.VATNo;
                adj.InsurerType = this.AdjusterAddress.InsurerType.ToString();
                adj.Status = this.AdjusterAddress.Status.ToString();
                adj.EffectiveFrom = this.AdjusterAddress.EffectiveFromDate;
                adj.EffectiveTo = this.AdjusterAddress.Effectiveto;
                adj.Remarks = this.AdjusterAddress.Remarks;
                adj.VAT = this.GST;
                adj.VATPer = this.VATPer;
                adj.ModifiedBy = this.ModifiedBy;
                adj.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.AdjusterCode = adj.AdjusterCode;
                this.CreatedBy = adj.CreatedBy;
                this.CreatedOn = adj.CreatedDate == null ? DateTime.MinValue : (DateTime)adj.CreatedDate;
                this.ModifiedOn = adj.ModifiedDate;
                _db.Dispose();
                return this;
            } else {
                adj = new MNT_Adjusters();

                var maxlength = 6;
                var prefix = "SVY";
                string result1;
                var countrows = (from row in _db.MNT_Adjusters where row.AdjusterTypeCode == "SVY" select (int?)row.AdjusterId).Max() ?? 0;
                if (countrows == 0)
                {
                    result1 = "0";
                }
                else
                {
                    var nxtrows = (from row in _db.MNT_Adjusters where (int?)row.AdjusterId == countrows select row.AdjusterCode).FirstOrDefault();
                    // Select only those characters that are numbers
                    result1 = new String(nxtrows.Where(x => Char.IsDigit(x)).ToArray());
                }
                string currentno = (Convert.ToInt32(result1) + 1).ToString();
                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                var Surveyorcode = (prefix + result + currentno);
                adj.AdjusterCode = Surveyorcode;
                adj.AdjusterName = this.AdjusterAddress.InsurerName;
                adj.AdjusterTypeCode = "SVY";
                adj.Address1 = this.AdjusterAddress.Address1;
                adj.Address2 = this.AdjusterAddress.Address2;
                adj.Address3 = this.AdjusterAddress.Address3;
                adj.City = this.AdjusterAddress.City;
                adj.Country =  this.AdjusterAddress.Country;
                adj.Province = this.AdjusterAddress.State;
                adj.ProductCode = "ENG";
                adj.EMail = this.AdjusterAddress.EmailAddress1;
                adj.FaxNo = this.AdjusterAddress.Fax1;
                adj.MobileNo = this.AdjusterAddress.MobileNo1;
                adj.PostCode = this.AdjusterAddress.PostalCode;
                adj.TelNoOff = this.AdjusterAddress.OffNo1;
                adj.ConPer = this.AdjusterAddress.FirstContactPersonName;
                adj.Classification = this.AdjusterAddress.SecondContactPersonName;
                adj.EmailAddress2 = this.AdjusterAddress.EmailAddress2;
                adj.Fax2 = this.AdjusterAddress.Fax2;
                adj.MobileNo2 = this.AdjusterAddress.MobileNo2;
                adj.OffNo2 = this.AdjusterAddress.OffNo2;
                adj.InsurerType = this.AdjusterAddress.InsurerType.ToString();
                adj.Status = this.AdjusterAddress.Status.ToString();
                adj.EffectiveFrom = this.AdjusterAddress.EffectiveFromDate;
                adj.EffectiveTo = this.AdjusterAddress.Effectiveto;
                adj.Remarks = this.AdjusterAddress.Remarks;
                adj.VAT = this.VAT;
                adj.WHTPer = Convert.ToDecimal(this.WHTPer);
                adj.VATNo = this.VATNo;
                adj.Status = this.AdjusterAddress.Status;
                adj.VAT = this.GST;
                adj.VATPer = this.VATPer;
                adj.CreatedBy = this.CreatedBy;
                adj.CreatedDate = DateTime.Now;
                _db.MNT_Adjusters.AddObject(adj);
                _db.SaveChanges();
                this.CreatedOn = (DateTime)adj.CreatedDate;
                var ecode = Convert.ToString(adj.AdjusterId);
                int list = (from l in _db.MNT_TransactionAuditLog.Where(x => x.UserName == adj.CreatedBy && x.Actions == "I" && x.TableName == "MNT_Adjusters") orderby l.TimeStamp select l.TranAuditId).ToList().LastOrDefault();
                var c = _db.MNT_TransactionAuditLog.Where(x => x.TranAuditId == list).FirstOrDefault();
                c.EntityCode = ecode;
                _db.SaveChanges();
                this.AdjusterId = adj.AdjusterId;
                this.AdjusterCode = adj.AdjusterCode;
                _db.Dispose();
                return this;
            }
        }

        public AdjusterModel SolicitorUpdate() {
            MNT_Adjusters adj;
            if (AdjusterId.HasValue)
            {
                adj = _db.MNT_Adjusters.Where(x => x.AdjusterId == this.AdjusterId).FirstOrDefault();
                adj.AdjusterCode = this.AdjusterCode;
                adj.AdjusterName = this.AdjusterAddress.InsurerName; 
                adj.AdjusterTypeCode = "SOL";
                adj.ProductCode = "ENG";
                adj.Address1 = this.AdjusterAddress.Address1;
                adj.Address2 = this.AdjusterAddress.Address2;
                adj.Address3 = this.AdjusterAddress.Address3;
                adj.City = this.AdjusterAddress.City;
                adj.Country = this.AdjusterAddress.Country;
                adj.Province = this.AdjusterAddress.State;
                adj.ProductCode = "ENG";
                adj.EMail = this.AdjusterAddress.EmailAddress1;
                adj.FaxNo = this.AdjusterAddress.Fax1;
                adj.MobileNo = this.AdjusterAddress.MobileNo1;
                adj.PostCode = this.AdjusterAddress.PostalCode;
                adj.TelNoOff = this.AdjusterAddress.OffNo1;
                adj.ConPer = this.AdjusterAddress.FirstContactPersonName;
                adj.Classification = this.AdjusterAddress.SecondContactPersonName;
                adj.EmailAddress2 = this.AdjusterAddress.EmailAddress2;
                adj.Fax2 = this.AdjusterAddress.Fax2;
                adj.MobileNo2 = this.AdjusterAddress.MobileNo2;
                adj.OffNo2 = this.AdjusterAddress.OffNo2;
                adj.VATNo = this.VATNo;
                adj.InsurerType = this.AdjusterAddress.InsurerType.ToString();
                adj.Status = this.AdjusterAddress.Status.ToString();
                adj.EffectiveFrom = this.AdjusterAddress.EffectiveFromDate;
                adj.EffectiveTo = this.AdjusterAddress.Effectiveto;
                adj.Remarks = this.AdjusterAddress.Remarks;
         //       adj.FirstContactPersonName = this.AdjusterAddress.FirstContactPersonName;
                adj.VATNo = this.VATNo;
                adj.AdjType = this.AdjType;
                adj.VAT = this.GST;
                adj.VATPer = this.VATPer;
              //  adj.Status = this.Status;
                adj.ModifiedBy = this.ModifiedBy;
                adj.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.AdjusterCode = adj.AdjusterCode;
                this.CreatedBy = adj.CreatedBy;
                this.CreatedOn = adj.CreatedDate == null ? DateTime.MinValue : (DateTime)adj.CreatedDate;
                this.ModifiedOn = adj.ModifiedDate;
                _db.Dispose();
                return this;
            }
            else
            {
                adj = new MNT_Adjusters();
                var maxlength = 6;
                var prefix = "SOL";
                string result1;
                var countrows = (from row in _db.MNT_Adjusters where row.AdjusterTypeCode == "SOL" select (int?)row.AdjusterId).Max() ?? 0;
                if (countrows == 0)
                {
                    result1 = "0";
                }
                else
                {
                    var nxtrows = (from row in _db.MNT_Adjusters where (int?)row.AdjusterId == countrows select row.AdjusterCode).FirstOrDefault();
                    // Select only those characters that are numbers
                    result1 = new String(nxtrows.Where(x => Char.IsDigit(x)).ToArray());
                }
                string currentno = (Convert.ToInt32(result1) + 1).ToString();
                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                var Solicitorcode = (prefix + result + currentno);
                adj.AdjusterCode = Solicitorcode;
                adj.AdjusterName = this.AdjusterAddress.InsurerName; 
                adj.AdjusterTypeCode = "SOL";
                adj.ProductCode = "ENG";
                adj.Address1 = this.AdjusterAddress.Address1;
                adj.Address2 = this.AdjusterAddress.Address2;
                adj.Address3 = this.AdjusterAddress.Address3;
                adj.City = this.AdjusterAddress.City;
                adj.Country = this.AdjusterAddress.Country;
                adj.Province = this.AdjusterAddress.State;
                adj.Status = this.AdjusterAddress.Status;
                adj.EMail = this.AdjusterAddress.EmailAddress1;
                adj.FaxNo = this.AdjusterAddress.Fax1;
                adj.MobileNo = this.AdjusterAddress.MobileNo1;
                adj.PostCode = this.AdjusterAddress.PostalCode;
                adj.TelNoOff = this.AdjusterAddress.OffNo1;
                adj.ConPer = this.AdjusterAddress.FirstContactPersonName;
                adj.Classification = this.AdjusterAddress.SecondContactPersonName;
                adj.EmailAddress2 = this.AdjusterAddress.EmailAddress2;
                adj.Fax2 = this.AdjusterAddress.Fax2;
                adj.MobileNo2 = this.AdjusterAddress.MobileNo2;
                adj.OffNo2 = this.AdjusterAddress.OffNo2;
                adj.InsurerType = this.AdjusterAddress.InsurerType.ToString();
                adj.Status = this.AdjusterAddress.Status.ToString();
                adj.EffectiveFrom = this.AdjusterAddress.EffectiveFromDate;
                adj.EffectiveTo = this.AdjusterAddress.Effectiveto;
                adj.Remarks = this.AdjusterAddress.Remarks;
                adj.AdjType = this.AdjType;
                adj.VAT = this.GST;
                adj.VATPer = this.VATPer;
                adj.VATNo = this.VATNo;
                adj.AdjSrc = this.AdjSrc;
                adj.CreatedBy =this.CreatedBy;
                adj.CreatedDate = DateTime.Now;
                _db.MNT_Adjusters.AddObject(adj);
                _db.SaveChanges();
                this.CreatedOn = (DateTime)adj.CreatedDate;
                var ecode = Convert.ToString(adj.AdjusterId);
                int list = (from l in _db.MNT_TransactionAuditLog.Where(x => x.UserName == adj.CreatedBy && x.Actions == "I" && x.TableName == "MNT_Adjusters") orderby l.TimeStamp select l.TranAuditId).ToList().LastOrDefault();
                var c = _db.MNT_TransactionAuditLog.Where(x => x.TranAuditId == list).FirstOrDefault();
                c.EntityCode = ecode;
                _db.SaveChanges();
                this.AdjusterId = adj.AdjusterId;
                this.AdjusterCode = adj.AdjusterCode;
                _db.Dispose();
                return this;
            }
        }

        #endregion
    }
}
