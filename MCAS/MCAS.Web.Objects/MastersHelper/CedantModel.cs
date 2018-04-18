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

namespace MCAS.Web.Objects.MastersHelper
{
    public class CedantModel : BaseModel
    {
        MCASEntities _db = new MCASEntities();
        public CedantModel()
        {
            CedantAddress = new AddressModel();
        }

        #region Properties
        public int? CedantId { get; set; }
        public string CedantCode { get; set; }
        public string Country { get; set; }


        public string CreditRating { get; set; }
        [DisplayName("Rating Issued")]
        [RegularExpression(@"^[0-9]{0,4}$", ErrorMessage = "Please enter rating issued in proper format.")]
        public string RatingIssued { get; set; }
        private DateTime? _effectiveFromDate = null;
        private DateTime? _effectiveTo = null;

        public DateTime? EffectiveFromDate
        {
            get { return _effectiveFromDate; }
            set { _effectiveFromDate = value; }
        }

        public DateTime? Effectiveto
        {
            get { return _effectiveTo; }
            set { _effectiveTo = value; }
        }

        public string CountryIncorporate { get; set; }

        public List<UserCountryListItems> usercountrylist { get; set; }
        private AddressModel _address = new AddressModel();
        public AddressModel CedantAddress
        {
            get { return _address; }
            set { _address = value; }
        }


        public override string screenId
        {
            get
            {
                return "231";
            }
            set
            {
                base.screenId = "231";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "100";
            }
            set
            {
                base.screenId = "100";
            }
        }
        #endregion

        #region Methods
        public static List<CedantModel> Fetch()
        {
            return Fetch(null);
        }
        public static List<CedantModel> Fetch(string cedantName)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<CedantModel>();
            var userList = String.IsNullOrEmpty(cedantName) ? (from l in _db.MNT_Cedant orderby l.CedantName select l) : (from l in _db.MNT_Cedant where l.CedantName.Contains(cedantName) orderby l.CedantName select l);
            
            if (userList.Any())
            {
                foreach (var data in userList)
                {
                    item.Add(new CedantModel()
                    {
                        CedantId = data.CedantId,
                        CedantCode = data.CedantCode,
                        CedantAddress = new AddressModel()
                        {
                            InsurerName = data.CedantName,
                            Address1 = data.Address,
                            City = data.City,
                            Status = data.Status,
                            Country = data.Country,
                            OffNo1 = data.OfficeNo1,
                            PostalCode = data.PostalCode,
                        }

                    });
                }
            }
            _db.Dispose();
            return item;
        }

        public CedantModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_Cedant cds;
            if (CedantId.HasValue)
            {
                cds = _db.MNT_Cedant.Where(x => x.CedantId == this.CedantId.Value).FirstOrDefault();
                cds.CedantName = this.CedantAddress.InsurerName;
                cds.Address = this.CedantAddress.Address1;
                cds.Address2 = this.CedantAddress.Address2;
                cds.Address3 = this.CedantAddress.Address3;
                cds.City = this.CedantAddress.City;
                cds.State = this.CedantAddress.State;
                cds.Country = this.CedantAddress.Country;
                cds.PostalCode = this.CedantAddress.PostalCode;
                cds.FirstContactPersonName = this.CedantAddress.FirstContactPersonName;
                cds.EmailAddress1 = this.CedantAddress.EmailAddress1;
                cds.OfficeNo1 = this.CedantAddress.OffNo1;
                cds.MobileNo1 = this.CedantAddress.MobileNo1;
                cds.FaxNo1 = this.CedantAddress.Fax1;
                cds.SecondContactPersonName = this.CedantAddress.SecondContactPersonName;
                cds.EmailAddress2 = this.CedantAddress.EmailAddress2;
                cds.OfficeNo2 = this.CedantAddress.OffNo2;
                cds.MobileNo2 = this.CedantAddress.MobileNo2;
                cds.FaxNo2 = this.CedantAddress.Fax2;
                cds.InsurerType = this.CedantAddress.InsurerType;
                if (this.CedantAddress.Status == "Active")
                {
                    cds.Status = "1";
                }
                else
                {
                    cds.Status = "0";
                }
                cds.EffectiveFrom = this.CedantAddress.EffectiveFromDate;
                cds.EffectiveTo = this.CedantAddress.Effectiveto;
                cds.Remarks = this.CedantAddress.Remarks;
                cds.CountryIncorporate = "SG";
                cds.CreditRating = this.CreditRating;
                cds.RatingIssued = this.RatingIssued;
                cds.ModifiedBy = Convert.ToString(this.ModifiedBy);
                cds.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.CedantId = cds.CedantId;
                this.CreatedBy = cds.CreatedBy;
                this.CreatedOn = cds.CreatedDate == null ? DateTime.MinValue : (DateTime)cds.CreatedDate;
                this.ModifiedOn = cds.ModifiedDate;
                _db.Dispose();
                return this;
            }
            else
            {
                cds = new MNT_Cedant();
                var prefix = "CC-";
                var countrows = (from row in _db.MNT_Cedant select (int?)row.CedantId).Max() ?? 0;
                string str = countrows.ToString();
                string currentno = (countrows + 1).ToString();
                string countryshortcode = "SG";
                var Cedantcode = (prefix + countryshortcode + "-" + currentno);
                cds.CedantCode = Cedantcode;
                cds.CedantName = this.CedantAddress.InsurerName;
                cds.Address = this.CedantAddress.Address1;
                cds.Address2 = this.CedantAddress.Address2;
                cds.Address3 = this.CedantAddress.Address3;
                cds.City = this.CedantAddress.City;
                cds.State = this.CedantAddress.State;
                cds.Country = this.CedantAddress.Country;
                cds.PostalCode = this.CedantAddress.PostalCode;
                cds.FirstContactPersonName = this.CedantAddress.FirstContactPersonName;
                cds.EmailAddress1 = this.CedantAddress.EmailAddress1;
                cds.OfficeNo1 = this.CedantAddress.OffNo1;
                cds.MobileNo1 = this.CedantAddress.MobileNo1;
                cds.FaxNo1 = this.CedantAddress.Fax1;
                cds.SecondContactPersonName = this.CedantAddress.SecondContactPersonName;
                cds.EmailAddress2 = this.CedantAddress.EmailAddress2;
                cds.OfficeNo2 = this.CedantAddress.OffNo2;
                cds.MobileNo2 = this.CedantAddress.MobileNo2;
                cds.FaxNo2 = this.CedantAddress.Fax2;
                cds.InsurerType = this.CedantAddress.InsurerType.ToString();
                if (this.CedantAddress.Status == "Active")
                {
                    cds.Status = "1";
                }
                else
                {
                    cds.Status = "0";
                }
                cds.EffectiveFrom = this.CedantAddress.EffectiveFromDate;
                cds.EffectiveTo = this.CedantAddress.Effectiveto;
                cds.Remarks = this.CedantAddress.Remarks;
                cds.CountryIncorporate = "SG";
                cds.CreditRating = this.CreditRating;
                cds.RatingIssued = this.RatingIssued;
                cds.CreatedBy = Convert.ToString(this.CreatedBy);
                cds.CreatedDate = DateTime.Now;
                _db.MNT_Cedant.AddObject(cds);
                _db.SaveChanges();
                this.CedantId = cds.CedantId;
                this.CedantCode = cds.CedantCode;
                this.CreatedOn = (DateTime)cds.CreatedDate;
                var ecode = Convert.ToString(cds.CedantId);
                int list = (from l in _db.MNT_TransactionAuditLog.Where(x => x.UserName == cds.CreatedBy && x.Actions == "I" && x.TableName == "MNT_Cedant") orderby l.TimeStamp select l.TranAuditId).ToList().LastOrDefault();
                var c = _db.MNT_TransactionAuditLog.Where(x => x.TranAuditId == list).FirstOrDefault();
                c.EntityCode = ecode;
                _db.SaveChanges();
                _db.Dispose();
                return this;
            }

        }

        #endregion

    }
}
