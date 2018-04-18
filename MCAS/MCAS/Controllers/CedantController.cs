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


namespace MCAS.Controllers
{
    public class CedantController : BaseController
    {
        MCASEntities _db = new MCASEntities();
        //
        // GET: /Cedant/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CedantIndex()
        {
            List<CedantModel> list = new List<CedantModel>();
            try
            {
               
                list = CedantModel.Fetch();
               
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(list);

            }
            return View(list);
        }




        [HttpPost]
        public ActionResult CedantIndex(string cedantName)
        {
            try
            {
                //CedantCode = Request.Form["inputCedantCode"].Trim();
                cedantName = Request.Form["inputCedantName"].Trim();
                // status = ((Request.Form["ddlStatus"] == null) ? "" : Request.Form["ddlStatus"]);
                //ViewBag.cedantcode = CedantCode;
                ViewBag.cedantname = cedantName;
                List<CedantModel> list = new List<CedantModel>();
                list = GetCedantResult(cedantName).ToList();
                return View(list);
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to Execute.  Try again and if the problem persists, see your system administrator.");
            }
            return View();
        }

        public IQueryable<CedantModel> GetCedantResult(string cedantName)
        {
            var searchResult = (from MNT_Cedant in _db.MNT_Cedant orderby cedantName 
                                where
                                  //MNT_Cedant.CedantCode.Contains(CedantCode) &&
                                  MNT_Cedant.CedantName.Contains(cedantName)


                                select MNT_Cedant).ToList().Select(item => new CedantModel
                                {
                                    CedantCode = item.CedantCode,
                                    //CedantName = item.CedantName,
                                    //Country=item.Country,
                                    CedantAddress = new AddressModel
                                    {
                                        InsurerName = item.CedantName,
                                        OffNo1 = item.OfficeNo1
                                    },
                                    //Status = item.Status,
                                    //Address=item.Address,
                                    //TelephoneNoOff=item.TelephoneNoOff,
                                    CedantId = item.CedantId
                                }).AsQueryable();

            return searchResult;
        }

        public JsonResult FillCountryCodeList()
        {
            var returnData = LoadUserCountry();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillGeneralStatus()
        {
            var returnData = LoadLookUpValue("STATUS");
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CedantEditor(int? CedantId)
        {

            var cedan = new CedantModel();
            CedantModel cds = new CedantModel();
            try
            {
                if (CedantId.HasValue)
                {
                    var grouplist = (from lt in _db.MNT_Cedant where lt.CedantId == CedantId select lt).FirstOrDefault();
                    
                    cds.CedantAddress = new AddressModel()
                    {
                        InsurerName = grouplist.CedantName,
                        Address1 = grouplist.Address,
                        Address2 = grouplist.Address2,
                        Address3 = grouplist.Address3,
                        City = grouplist.City,
                        State = grouplist.State,
                        Country = grouplist.Country,
                        PostalCode = grouplist.PostalCode,
                        FirstContactPersonName = grouplist.FirstContactPersonName,
                        EmailAddress1 = grouplist.EmailAddress1,
                        OffNo1 = grouplist.OfficeNo1,
                        MobileNo1 = grouplist.MobileNo1,
                        Fax1 = grouplist.FaxNo1,
                        SecondContactPersonName = grouplist.SecondContactPersonName,
                        EmailAddress2 = grouplist.EmailAddress2,
                        OffNo2 = grouplist.OfficeNo2,
                        MobileNo2 = grouplist.MobileNo2,
                        Fax2 = grouplist.FaxNo2,
                        InsurerType = grouplist.InsurerType,
                        Status = grouplist.Status,
                        EffectiveFromDate = grouplist.EffectiveFrom,
                        Effectiveto = grouplist.EffectiveTo,
                        Remarks = grouplist.Remarks
                    };

                    cds.CedantCode = grouplist.CedantCode;
                    cds.CedantId = CedantId;
                    cds.CedantAddress.Country = grouplist.Country;
                    if (grouplist.Status == "1")
                    {
                        cds.CedantAddress.Status = "Active";
                    }
                    else
                    {
                        cds.CedantAddress.Status = "InActive";
                    }
                    cds.CedantAddress.Statuslist = LoadLookUpValue("STATUS");
                    cds.CedantAddress.Insurerlist = LoadLookUpValue("InsurerType");
                    cds.CedantAddress.citylist = LoadCity();
                    cds.CedantAddress.usercountrylist = LoadCountry();
                    cds.RatingIssued = grouplist.RatingIssued;
                    cds.CreditRating = grouplist.CreditRating;

                    cds.CreatedBy = grouplist.CreatedBy == null ? " " : grouplist.CreatedBy;
                    if (grouplist.CreatedDate != null)
                        cds.CreatedOn = (DateTime)grouplist.CreatedDate;
                    else
                        cds.CreatedOn = DateTime.MinValue;
                    cds.ModifiedBy = grouplist.ModifiedBy == null ? " " : grouplist.ModifiedBy;
                    cds.ModifiedOn = grouplist.ModifiedDate;

                    return View(cds);
                }
                cedan.CedantAddress.citylist = LoadCity();
                cedan.CedantAddress.Statuslist = LoadLookUpValue("STATUS");
                cedan.CedantAddress.Insurerlist = LoadLookUpValue("InsurerType");
                cedan.CedantAddress.usercountrylist = LoadCountry();
                return View(cedan);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving insurer name " + cds.CedantCode + " for insurer." + cds.CedantAddress.InsurerName + ".");
                addInfo.Add("entity_type", "insurer Master");
                PublishException(ex, addInfo, 0, "insurer Master" + cds.CedantAddress.InsurerName);
               
            }

            return View(cedan);
        }

        public ActionResult ViewCedanthistory(string Cedantid, string Tablename, string Displayname)
        {
            MCASEntities obj = new MCASEntities();
            List<TransactionModel> list = new List<TransactionModel>();
            list = TransactionModel.Fetchallfrserviceprovider(Cedantid, Tablename, Displayname);
            obj.Dispose();
            return View(list);
        }



        [HttpPost, ValidateInput(false)]
        public ActionResult CedantEditor(CedantModel model, AddressModel addModel)
        {
            addModel.Insurerlist = LoadLookUpValue("InsurerType");
            addModel.Statuslist = LoadLookUpValue("STATUS");
            addModel.citylist = LoadCity();
            addModel.usercountrylist = LoadCountry();
            model.CedantAddress = addModel;
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    TempData["display"] = "";
                    ModelState.Clear();
                    var CedantName = (from lt in _db.MNT_Cedant where lt.CedantId == model.CedantId select lt).FirstOrDefault();
                    if (CedantName == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var cedan = model.Update();
                        TempData["result"] = "Record Saved Successfully.";
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var cedan = model.Update();
                        TempData["result"] = "Record Updated Successfully.";
                    }

                }
                else
                {
                    return View(model);
                }

                
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving insurer name " + model.CedantCode + " for insurer." + model.CedantAddress.InsurerName + ".");
                addInfo.Add("entity_type", "insurer Master");
                PublishException(ex, addInfo, 0, "insurer Master" + model.CedantAddress.InsurerName);
                return View(model);
            }
            return View(model);
        }


        [HttpGet]
        public JsonResult CheckInsurername11(string insurername)
        {
            var allInsurer = _db.MNT_Cedant.ToList();

            var isDuplicate = false;

            foreach (var insu in allInsurer)
            {

                if (insu.CedantName == insurername)
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckInsurername(string InsurerName, string InsuName)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Cedant where t.CedantName == InsurerName orderby t.CedantName descending select t.CedantName).Take(1)).FirstOrDefault();
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


    }
}
