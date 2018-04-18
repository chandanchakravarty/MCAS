using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;

namespace MCAS.Web.Objects.CommonHelper
{
    public class UserCountryListItems :BaseModel
    {
        #region Properties
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "Country Name is required.")]
        public string CountryShortCode { get; set; }
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
        public string CountryId { get; set; }

    //    public List<CountryModel> countrylist { get; set; }

        public List<UserCountryListItems> countrylist { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }

        public override string screenId
        {
            get
            {
                return "268";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "267";
            }

        }
        #endregion

        #region Static Methods
        public static List<UserCountryListItems> Fetch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<UserCountryListItems> list = new List<UserCountryListItems>();
            var data = obj.Proc_GetCountryList().ToList();
            foreach (var l in data)
            {
                list.Add(new UserCountryListItems() {
                    CountryCode = l.CountryCode,
                    CountryName = l.CountryName,
                    CountryShortCode = l.CountryShortCode
                });
            }
            if (addAll)
            {
                list.Insert(0, new UserCountryListItems() { CountryShortCode = "", CountryName = "[Select...]" });
            }
            return list;
        }

        public static List<UserCountryListItems> FetchList()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<UserCountryListItems>();
            var usercountryList = (from l in obj.MNT_UserCountry orderby l.CountryCode select l);
            if (usercountryList.Any())
            {
                foreach (var data in usercountryList)
                {
                    item.Add(new UserCountryListItems() { CountryCode = data.CountryCode,CountryShortCode = data.CountryShortCode, CountryName = data.CountryName, Status = data.Status, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public UserCountryListItems Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_UserCountry usrcountryinfo;
            var changeStatus = "";
            if (this.Status == "Active")
            {
                changeStatus = "Y";
            }
            else
            {
                changeStatus = "N";
            }


            if (!string.IsNullOrEmpty(CountryCode))
            {
                usrcountryinfo = obj.MNT_UserCountry.Where(x => x.CountryCode == this.CountryCode).FirstOrDefault();

                usrcountryinfo.CountryCode = this.CountryCode;
                usrcountryinfo.CountryShortCode = (from m in obj.MNT_Country where m.CountryShortCode == this.CountryShortCode select m.CountryShortCode).FirstOrDefault();
                usrcountryinfo.CountryName = (from m in obj.MNT_Country where m.CountryShortCode == this.CountryShortCode select m.CountryName).FirstOrDefault();
                usrcountryinfo.Status = changeStatus;
                usrcountryinfo.ModifiedBy = Convert.ToString(this.ModifiedBy);
                usrcountryinfo.ModifiedDate = DateTime.Now;

                obj.SaveChanges();
                return this;
            }
            else
            {
                usrcountryinfo = new MNT_UserCountry();
                
                var maxlength = 5;
                var prefix = "UC";
                var countrows = (from row in obj.MNT_UserCountry select row).Count();
              //  var countrows = (from row in obj.MNT_UserCountry select (int?)row.CountryCode).Max() ?? 0; 
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var countrycode = (prefix + result + currentno);

                usrcountryinfo.CountryCode = countrycode;
                usrcountryinfo.CountryShortCode = (from m in obj.MNT_Country where m.CountryShortCode == this.CountryShortCode select m.CountryShortCode).FirstOrDefault();
                usrcountryinfo.CountryName = (from m in obj.MNT_Country where m.CountryShortCode == this.CountryShortCode select m.CountryName).FirstOrDefault();
                usrcountryinfo.Status = changeStatus;
                usrcountryinfo.CreatedBy = Convert.ToString(this.CreatedBy);
                usrcountryinfo.CreatedDate = DateTime.Now;


                obj.MNT_UserCountry.AddObject(usrcountryinfo);
                obj.SaveChanges();
                this.CountryCode = usrcountryinfo.CountryCode;
                return this;
            }
        }
        #endregion
    }


    public class ProductsCountryListItems : BaseModel
    {
        #region Properties
        public int? ProductCountryId { get; set; }
        public string Product_Code { get; set; }
        public string CountryCode { get; set; }
        #endregion
    }

    public class UserCountryProductsListItems : BaseModel
    {
        #region Properties
        public int? UserCountryProductId { get; set; }
        public string UserId { get; set; }
        public string CountryCode { get; set; }
        public string ProductCode { get; set; }
        #endregion

        #region Methods

        #endregion
    }

    public class OrgCountryListItems : BaseModel
    {
        #region Properties
        public int? ProductCountryId { get; set; }
        public string Userid { get; set; }
        public string LookupValue { get; set; }
        public string LookupDesc { get; set; }
        public string category { get; set; }
        #endregion
    }
}
