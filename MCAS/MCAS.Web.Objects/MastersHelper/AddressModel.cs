using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Globalization;
using System.ComponentModel;

namespace MCAS.Web.Objects.MastersHelper
{
    public class AddressModel : BaseModel
    {
        #region properties
        string MatchEmailPattern = "";
        string email = "";

        //[Required(ErrorMessage = "Company Name is required.")]
        //public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        public string InsurerName { get; set; }

        [Required(ErrorMessage = "Address1 is required.")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }


     //   public string Contact_Person { get; set; }

    //    [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
     //   [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal Code  is required.")]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please enter Postal code in proper format.")]
        public string PostalCode { get; set; }

        public string FirstContactPersonName { get; set; }

         //[RegularExpression(@"^([a-zA-Z0-9_\-\.']+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter Email Address in proper format.")]
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

        [Required(ErrorMessage = "Insurer Type is required.")]
        public string InsurerType { get; set; }
        public List<LookUpListItems> Insurerlist { get; set; }

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

        [DisplayName("Effective To")]
        public DateTime? Effectiveto {
            get { return _effectiveTo; }
            set { _effectiveTo = value; }
        }

        public string Remarks { get; set; }

          



        

        
        

        
        public string validMsg { get; set; }

       
        

        //[Required(ErrorMessage = "City is required.")]
        //public string City { get; set; }
        //[Required(ErrorMessage = "Country is required.")]
        //public string Country { get; set; }
        // [Required(ErrorMessage = "Status is required.")]
        

        public List<UserCityListItem> citylist { get; set; }
     //   public List<UserCountryListItems> usercountrylist { get; set; }
        public List<UserCountryListItems> usercountrylist { get; set; }
         

      //  public List<ProviceList> ProvinceList { get; set; }
        #endregion

        

    }

    

    
}
