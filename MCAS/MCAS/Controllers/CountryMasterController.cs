using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using MCAS.Globalisation;
namespace MCAS.Controllers
{
    public class CountryMasterController : BaseController
    {

        #region Country
        //
        // GET: /CountryMaster/
        MCASEntities _db = new MCASEntities();

        public ActionResult CountryIndex()
        {
            List<CountryModel> list = new List<CountryModel>();
            try
            {
                list = CountryModel.Fetch();
            }
            catch (Exception ex) {
                ErrorLog(ex.Message,ex.InnerException);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult CountryIndex(string CountryCode, string CountryName)
        {
            List<CountryModel> list = new List<CountryModel>();
            try 
            {

                CountryCode = Request.Form["inputCountryCode"].Trim().ToUpper();
                CountryName = Request.Form["inputCountryName"].Trim();
                ViewBag.Countrycode = CountryCode;
                ViewBag.CountryName = CountryName;
                list = GetCountrySearchResult(CountryCode, CountryName).ToList();
            }

            catch (Exception ex) {
                ErrorLog(ex.Message, ex.InnerException);
            }
            
            return View(list);

        }

        public IQueryable<CountryModel> GetCountrySearchResult(string CountryCode, string CountryName)
        {
            var searchResult = (from country in _db.MNT_Country
                                orderby country.CountryName ascending
                                where
                                country.CountryShortCode.Contains(CountryCode) &&
                                country.CountryName.Contains(CountryName)
                                select country).ToList().Select(item => new CountryModel
                                {
                                    Countryid=item.CountryId,
                                    CountryCode=item.CountryCode,
                                    CountryName=item.CountryName,
                                    CountryShortCode=item.CountryShortCode
                                 }).AsQueryable();

            return searchResult;
        }

        [HttpGet]
        public ActionResult CountryEditor(int? Countryid)
        {
            var country = new CountryModel();
            CountryModel objmodel = new CountryModel();
            try
            {
                if (Countryid.HasValue)
                {
                    var CountryList = (from x in _db.MNT_Country where x.CountryId == Countryid select x).FirstOrDefault();
                    objmodel.Countryid = CountryList.CountryId;
                    objmodel.CountryCode = CountryList.CountryCode;
                    objmodel.CountryName = CountryList.CountryName;
                    objmodel.CountryShortCode = CountryList.CountryShortCode;
                    objmodel.CreatedBy = CountryList.CreatedBy == null ? " " : CountryList.CreatedBy;
                    if (CountryList.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)CountryList.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = CountryList.ModifiedBy == null ? " " : CountryList.ModifiedBy;
                    objmodel.ModifiedOn = CountryList.ModifiedDate;
                    return View(objmodel);
                }
            }
            catch(Exception ex) {
                ErrorLog(ex.Message,ex.InnerException);
                return View(objmodel);
            }
            return View(country);
        }


        public JsonResult CheckCountryName(string CountryName, string CountryN)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Country where t.CountryName.ToLower() == CountryName.ToLower() orderby t.CountryCode descending select t.CountryCode).Take(1)).FirstOrDefault();
            if (num != null && num.ToLower() != CountryN.ToLower())
            {
                result = true;
            }
            return Json(result);
        }

        public JsonResult CountryCodeExists(string CountryCode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Country where t.CountryShortCode.ToLower() == CountryCode.ToLower() orderby t.CountryCode descending select t.CountryShortCode).Take(1)).FirstOrDefault();
            if (num != null)
            {
                result = true;
            }
            db.Dispose();
            return Json(result);
        }

        [HttpPost]
        public ActionResult CountryEditor(CountryModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    TempData["notice"] = "";
                    ModelState.Clear();
                    var country = (from lt in _db.MNT_Country where lt.CountryId == model.Countryid select lt).FirstOrDefault();
                    if (country == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var countries = model.Update();
                        TempData["notice"] = "Records Saved Successfully.";
                        //return View(model);

                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var claimclose = model.Update();
                        TempData["notice"] = "Records Updated Successfully.";
                        //return View(model);
                    }
                    string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"].ToUpper();
                    object routes = new { Countryid = model.Countryid };
                    if (isEncryptedParams.ToUpper() == "YES")
                    {
                        string res = RouteEncryptDecrypt.getRouteString(routes);
                        res = RouteEncryptDecrypt.Encrypt(res);
                        routes = new { Q = res };
                    }
                    return RedirectToAction("CountryEditor", routes);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }
           
        }
        #endregion 

        #region Organization Country

        
        [HttpGet]
        public ActionResult OrgCountryIndex() {
            List<OrgCountryModel> list = new List<OrgCountryModel>();
            list = OrgCountryModel.FetchOrg();
            return View(list);
        }

        [HttpPost]
        public ActionResult OrgCountryIndex(string orgCode, string orgName) {
            orgCode = Request.Form["inputCountryCode"].Trim();
            orgName = Request.Form["inputOrganizationName"].Trim();
            ViewBag.Orgcode = orgCode;
            ViewBag.OrgName = orgName;
            List<OrgCountryModel> list = new List<OrgCountryModel>();
            list = GetOrgCountrySearchResult(orgCode, orgName).ToList();
            return View(list);
        }

        public IQueryable<OrgCountryModel> GetOrgCountrySearchResult(string orgCountryCode, string orgCountryName)
        {
            var searchResult = (from orgC in _db.MNT_OrgCountry
                                where
                                orgC.CountryOrgazinationCode.Contains(orgCountryCode) &&
                                orgC.OrganizationName.Contains(orgCountryName)
                                select orgC).ToList().Select(item => new OrgCountryModel
                                {
                                    Id = item.Id,
                                    CountryOrgazinationCode = item.CountryOrgazinationCode,
                                    OrganizationName = item.OrganizationName,
                                    OffNo1=item.TelNo
                                }).AsQueryable();

            return searchResult;
        }

        public ActionResult OrgCountryEditor(int? ID) {
            var orgCountry = new OrgCountryModel();
            OrgCountryModel org = new OrgCountryModel();
            try {
                if (ID.HasValue)
                {
                    var orgcount = (from x in _db.MNT_OrgCountry where x.Id == ID select x).FirstOrDefault();
                    org.CountryOrgazinationCode = orgcount.CountryOrgazinationCode;
                    org.OrganizationName = orgcount.OrganizationName;
                    org.CategoryType = orgcount.InsurerType;
                    org.Initial = orgcount.Initial;
                    org.Address1 = orgcount.Address1;
                    org.Address2 = orgcount.Address2;
                    org.Address3 = orgcount.Address3;
                    org.OffNo1 = orgcount.TelNo;
                    org.MobileNo1 = orgcount.MobileNo;
                    org.EmailAddress1 = orgcount.Email;
                    org.Fax1 = orgcount.Fax;
                    org.FirstContactPersonName = orgcount.FirstContactPersonName;
                    org.PostalCode = orgcount.PostalCode;
                    org.City = orgcount.City;
                    org.Country = orgcount.Country;
                    org.State = orgcount.State;
                    org.SecondContactPersonName = orgcount.SecondContactPersonName;
                    org.EmailAddress2 = orgcount.EmailAddress2;
                    org.OffNo2 = orgcount.OffNo2;
                    org.MobileNo2 = orgcount.MobileNo2;
                    org.Fax2 = orgcount.Fax2;
                    org.EffectiveFromDate = orgcount.EffectiveFrom;
                    org.Effectiveto = orgcount.EffectiveTo;
                    org.Status = orgcount.Status;
                    org.Remarks = orgcount.Remarks;
                    org.Id = orgcount.Id;
                    if (orgcount.Status == "1")
                    {
                        org.Status = "Active";
                        
                    }
                    else
                    {
                        org.Status = "InActive";
                    }
                    org.Statuslist = LoadLookUpValue("STATUS");
                    org.Categorylist = LoadLookUpValue("ORGCategory");
                    org.usercountrylist = LoadCountry();

                    org.CreatedBy = orgcount.CreatedBy == null ? " " : orgcount.CreatedBy;
                    if (orgcount.CreatedDate != null)
                        org.CreatedOn = (DateTime)orgcount.CreatedDate;
                    else
                        org.CreatedOn = DateTime.MinValue;
                    org.ModifiedBy = orgcount.ModifiedBy == null ? " " : orgcount.ModifiedBy;
                    org.ModifiedOn = orgcount.ModifiedDate;
                                       
                    return View(org);
                }
                orgCountry.usercountrylist = LoadCountry();
                orgCountry.Statuslist = LoadLookUpValue("STATUS");
                orgCountry.Categorylist = LoadLookUpValue("ORGCategory");
                return View(orgCountry);
            
            
            }
            catch (Exception ex) {
                ErrorLog(ex.Message,ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving organization name " + org.CountryOrgazinationCode + " for organization." + org.CountryOrgazinationCode + ".");
                addInfo.Add("entity_type", "Organization Master");
                PublishException(ex, addInfo, 0, "Organization Master" + org.CountryOrgazinationCode);
            }
            return View(orgCountry);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult OrgCountryEditor(OrgCountryModel model)
        {
            TempData["display"] = "";
            TempData["result"] = "";
            model.Statuslist = LoadLookUpValue("STATUS");
            model.usercountrylist = LoadCountry();
            model.Categorylist = LoadLookUpValue("ORGCategory");
            try 
            {
                if (!ModelState.IsValid)
                {
                    ModelState["Initial"].Errors.Clear();
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var orgname = (from x in _db.MNT_OrgCountry where x.Id == model.Id select x).FirstOrDefault();
                    if (orgname == null)
                    {
                       
                        model.CreatedBy = LoggedInUserName;
                        var orgs = model.orgCountryUpdate();
                        TempData["result"] = "Records Saved Successfully.";
                        TempData["display"] = orgs.CountryOrgazinationCode;
                    }
                    else {
                        model.ModifiedBy = LoggedInUserName;
                        var orgs = model.orgCountryUpdate();
                        TempData["result"] = "Records Updated Successfully.";
                    }
                }
                else
                {
                    return View(model);
                }
            
            
            }
            catch (Exception ex) {
                ErrorLog(ex.Message,ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving organization master " + model.OrganizationName + " for organization." + model.OrganizationName + ".");
                addInfo.Add("entity_type", "organization master");
                PublishException(ex, addInfo, 0, "organization master" + model.OrganizationName);
            }
            return View(model);
        }

        public JsonResult ValidEmail(string EmailAdd1)
        {
            var Emailresults = false;
            if (EmailAdd1 != null && EmailAdd1 != "")
            {
                CommonUtilities.IsEMail isEmail = new CommonUtilities.IsEMail();
                bool result = isEmail.IsEmailValid(EmailAdd1);
                if (result) { }
                else
                {
                    Emailresults = true;
                    //// addModel.validMsg = "Please enter Email in proper format.";
                    //TempData["ErrorMsg"] = "Please enter Email in proper format."; ;
                    //return View(model);
                }
            }
            return Json(Emailresults);
        }

        public JsonResult CheckOrganizationname(string InsurerName, string InsuName) {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_OrgCountry where t.OrganizationName == InsurerName orderby t.OrganizationName descending select t.OrganizationName).Take(1)).FirstOrDefault();
            if ((num != null) && (InsuName == null || InsuName == ""))
            {
                result = true;
            }
            else if ((num != null && num.ToLower() != InsuName.ToLower()))
            {
                result = true;
            }

            return Json(result);
        }

        #endregion



    }
}
