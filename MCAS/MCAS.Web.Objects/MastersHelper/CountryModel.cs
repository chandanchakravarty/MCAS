using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.Resources.CountryMaster;
using System.ComponentModel;
using System.Web;

namespace MCAS.Web.Objects.MastersHelper
{
   public class CountryModel:BaseModel
   {

       MCASEntities _db = new MCASEntities();
       
       #region Properties
       public int? Countryid { get; set; }

             
       public string CountryCode { get; set; }

       
       [Required(ErrorMessage = "Country Name is required.")]
       public string CountryName { get; set; }

       [Required(ErrorMessage = "Country Short code is required.")]
       public string CountryShortCode { get; set; }

       public override string screenId
       {
           get
           {
               return "246";
           }
       }
       public override string listscreenId
       {

           get
           {
               return "115";
           }

       }

#endregion

       #region shared method
       

       public static List<CountryModel> Fetch()
       {
           MCASEntities obj = new MCASEntities();
           var item = new List<CountryModel>();
           var Countrylist = (from l in obj.MNT_Country orderby l.CountryName select l);
           if (Countrylist.Any())
           {
               foreach (var data in Countrylist)
               {
                   item.Add(new CountryModel() { Countryid = data.CountryId, CountryShortCode=data.CountryShortCode, CountryCode = data.CountryCode, CountryName = data.CountryName });
               }
           }
           return item;
       }

       public static List<CountryModel> Fetch(bool addAll = true)
       {
           MCASEntities obj = new MCASEntities();
           List<CountryModel> list = new List<CountryModel>();
           list = (from l in obj.MNT_Country orderby l.CountryName select new CountryModel { CountryCode = l.CountryCode, CountryName = l.CountryName, CountryShortCode = l.CountryShortCode }).ToList();
           if (addAll)
           {
               list.Insert(0, new CountryModel() { CountryShortCode = "", CountryName = "[Select...]" });
           }
           return list;
       }
       #endregion

       #region Method

       public CountryModel Update()
       {
           MCASEntities _db = new MCASEntities();
           MNT_Country csd;
           if (Countryid.HasValue)
           {
               csd = _db.MNT_Country.Where(x => x.CountryId == this.Countryid.Value).FirstOrDefault();
             //  csd.CountryCode = this.CountryCode;
               csd.CountryShortCode = this.CountryShortCode.ToUpper();
               csd.CountryName = this.CountryName;
               csd.ModifiedBy = HttpContext.Current.Session["LoggedInUserName"].ToString(); 
               csd.ModifiedDate = DateTime.Now;
               _db.SaveChanges();
               this.CreatedBy = csd.CreatedBy;
               this.CreatedOn = csd.CreatedDate==null?DateTime.MinValue: (DateTime)csd.CreatedDate ;
               this.ModifiedOn = csd.ModifiedDate;
               return this;
           }
           else
           {
               csd = new MNT_Country();
               var maxlength = 6;
               var prefix = "C00";
               var countrows = (from row in _db.MNT_Country select (int?)row.CountryId).Max() ?? 0;
                       
               string currentno = (countrows + 1).ToString();

               string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

               var countrycode = (prefix + result + currentno);

               csd.CountryCode = countrycode;
               csd.CountryName = this.CountryName;
               csd.CountryShortCode = this.CountryShortCode.ToUpper();
               csd.CreatedBy = HttpContext.Current.Session["LoggedInUserName"].ToString();
               csd.CreatedDate = DateTime.Now;
               _db.MNT_Country.AddObject(csd);
               _db.SaveChanges();
               this.Countryid = csd.CountryId;
               this.CountryCode = csd.CountryCode;
               this.CreatedOn = (DateTime)csd.CreatedDate;
               return this;
           }
           
       }
       #endregion
   }

   public class OrgCountryModel : BaseModel {
       MCASEntities _db = new MCASEntities();
       public OrgCountryModel() {
           CountryAddress = new AddressModel();
       }

       #region properties
       public int? Id { get; set; }
       public string CountryOrgazinationCode { get; set; }
       [Required(ErrorMessage = "Organization Name is required.")]
       public string OrganizationName { get; set; }

       [Required(ErrorMessageResourceType = typeof(OrgCountryErrorLog), ErrorMessageResourceName = "RFVInitial")]
       public string Initial { get; set; }

       [Required(ErrorMessage = "Address1 is required.")]
       public string Address1 { get; set; }

       public string Address2 { get; set; }

       public string Address3 { get; set; }

       public string City { get; set; }

       public string State { get; set; }

       [Required(ErrorMessage = "Country is required.")]
       public string Country { get; set; }

       [Required(ErrorMessage = "Postal Code  is required.")]
       [RegularExpression(@"^[0-9]{0,10}$", ErrorMessage = "Please enter Postal code in proper format.")]
       public string PostalCode { get; set; }

       public string FirstContactPersonName { get; set; }

       public string EmailAddress1 { get; set; }

       [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter office no in proper format.")]
       public string OffNo1 { get; set; }
       [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter mobile no in proper format.")]
       public string MobileNo1 { get; set; }
       [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter fax no in proper format.")]
       public string Fax1 { get; set; }

       public string SecondContactPersonName { get; set; }

       [RegularExpression(@"^([a-zA-Z0-9_\-\.']+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter Email Address in proper format.")]
       public string EmailAddress2 { get; set; }

       [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter office no in proper format.")]
       public string OffNo2 { get; set; }

       [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter mobile no in proper format.")]
       public string MobileNo2 { get; set; }

       [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter fax no in proper format.")]
       public string Fax2 { get; set; }

       [Required(ErrorMessage = "Organization Category is required.")]
       public string CategoryType { get; set; }
       public List<LookUpListItems> Categorylist { get; set; }

       [Required(ErrorMessage = "Status is required.")]
       public string Status { get; set; }
       public List<LookUpListItems> Statuslist { get; set; }

       private DateTime? _effectiveFromDate = null;
       private DateTime? _effectiveTo = null;

       [DisplayName("Effective From")]
       [Required(ErrorMessage = "Effective From is required.")]
       public DateTime? EffectiveFromDate
       {
           get { return _effectiveFromDate; }
           set { _effectiveFromDate = value; }
       }

       //[Required(ErrorMessage = "Effective To is required.")]
       [DisplayName("Effective To")]
       public DateTime? Effectiveto
       {
           get { return _effectiveTo; }
           set { _effectiveTo = value; }
       }

       public List<UserCountryListItems> usercountrylist { get; set; }

       public string Remarks { get; set; }
       public string CountryIncorporate { get; set; }

    //   [Required(ErrorMessage = "Country is required.")]
    //   public string Country { get; set; }
    //   public List<CountryModel> usercountrylist1 { get; set; }

       private AddressModel _address = new AddressModel();
       public AddressModel CountryAddress
       {
           get { return _address; }
           set { _address = value; }
       }

       public override string screenId
       {
           get
           {
               return "253";
           }
       }
       public override string listscreenId
       {

           get
           {
               return "125";
           }

       }

       #endregion

       #region static methods
       public static List<OrgCountryModel> FetchOrg()
       {
           MCASEntities _db = new MCASEntities();
           var item = new List<OrgCountryModel>();
           var orgCountry = (from l in _db.MNT_OrgCountry orderby l.OrganizationName select l);
           if (orgCountry.Any())
           {
               foreach (var data in orgCountry)
               {
                   item.Add(new OrgCountryModel()
                   {
                       Id = data.Id,
                       OrganizationName = data.OrganizationName,
                       CountryOrgazinationCode = data.CountryOrgazinationCode,
                       OffNo1 = data.TelNo
                   });
               }
           }
           return item;
       }

       #endregion

       #region Methods
       public OrgCountryModel orgCountryUpdate() {
           MCASEntities _db = new MCASEntities();
           MNT_OrgCountry org = new MNT_OrgCountry();
           if (Id.HasValue)
           {
               org = _db.MNT_OrgCountry.Where(x => x.Id == this.Id.Value).FirstOrDefault();
               org.CountryOrgazinationCode = this.CountryOrgazinationCode;
               org.OrganizationName = this.OrganizationName;
               org.Address1 = this.Address1;
               org.Address2 = this.Address2;
               org.Address3 = this.Address3;
               org.City = this.City;
               org.Country = this.Country;
               org.State = this.State;
               org.InsurerType = this.CategoryType;
               org.Initial = this.Initial;
               if (this.Status == "Active")
               {
                   org.Status = "1";
               }
               else
               {
                   org.Status = "0";
               }
               
               org.TelNo = this.OffNo1;
               org.MobileNo = this.MobileNo1;
               org.Email = this.EmailAddress1;
               org.PostalCode = this.PostalCode;
               org.Fax = this.Fax1;
               org.FirstContactPersonName = this.FirstContactPersonName;
               org.Remarks = this.Remarks;
               org.SecondContactPersonName = this.SecondContactPersonName;
               org.EmailAddress2 = this.EmailAddress2;
               org.Fax2 = this.Fax2;
               org.OffNo2 = this.OffNo2;
               org.MobileNo2 = this.MobileNo2;
               org.ModifiedBy = this.ModifiedBy;
               org.ModifiedDate = DateTime.Now;
               org.EffectiveFrom = this.EffectiveFromDate;
               org.EffectiveTo = this.Effectiveto;
               _db.SaveChanges();
               this.Id = org.Id;
               this.CreatedBy = org.CreatedBy;
               this.CreatedOn = org.CreatedDate == null ? DateTime.MinValue : (DateTime)org.CreatedDate;
               this.ModifiedOn = org.ModifiedDate;
               return this;
           }
           else {
               var maxlength = 5;
               var prefix = "CZ";
               var countrows = (from row in _db.MNT_OrgCountry select (int?)row.Id).Max() ?? 0;
               string currentno = (countrows + 1).ToString();
               string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
              //  var Cedantcode = (prefix + countryshortcode + "-" + result + currentno);
               var CountryOrgCode = (prefix + result + currentno);
               org.CountryOrgazinationCode = CountryOrgCode;
               org.OrganizationName = this.OrganizationName;
               org.Address1 = this.Address1;
               org.Address2 = this.Address2;
               org.Address3 = this.Address3;
               org.City = this.City;
               org.Country = this.Country;
               org.State = this.State;
               org.InsurerType = this.CategoryType;
                            

               if (this.Status == "Active")
               {
                   org.Status = "1";
               }
               else
               {
                   org.Status = "0";
               }
               org.Initial = this.Initial;
               org.TelNo = this.OffNo1;
               org.MobileNo = this.MobileNo1;
               org.Email = this.EmailAddress1;
               org.PostalCode = this.PostalCode;
               org.Fax = this.Fax1;
               org.FirstContactPersonName = this.FirstContactPersonName;
               org.Remarks = this.Remarks;
               org.CreatedBy =this.CreatedBy;
               org.CreatedDate = DateTime.Now;
               org.SecondContactPersonName = this.SecondContactPersonName;
               org.EmailAddress2 = this.EmailAddress2;
               org.Fax2 = this.Fax2;
               org.OffNo2 = this.OffNo2;
               org.MobileNo2 = this.MobileNo2;
               org.EffectiveFrom = this.EffectiveFromDate;
               org.EffectiveTo = this.Effectiveto;
               _db.MNT_OrgCountry.AddObject(org);
               _db.SaveChanges();
               this.Id = org.Id;
               this.CountryOrgazinationCode = org.CountryOrgazinationCode;
               this.CreatedOn = (DateTime)org.CreatedDate;
               return this;

           }
       }
      

       #endregion
   }
}
