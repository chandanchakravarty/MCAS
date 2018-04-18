using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;
using System.Data.Entity;
using System.Data.EntityModel;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
namespace MCAS.Controllers
{
    public class AdjusterMastersController : BaseController
    {
        //
        // GET: /Adjuster/

        // GET: /WorkMaster/
        MCASEntities _db = new MCASEntities();
        #region Service Providers
        [HttpGet]
        public ActionResult ServiceProviders(string tabId)
        {
            return View(new CedantModel());

        }
       //[HttpGet]
        public JsonResult ServiceProvider(string tabId,string guid,string searchText)
        {
            tabId = tabId == null ? "Insurer" : tabId;
           try
            {
                if (tabId.Equals("Insurer") || tabId ==null)
                {
                    var list = CedantModel.Fetch(searchText);
                    string screenID = (new CedantModel()).listscreenId != null ? (new CedantModel()).listscreenId : "0";
                    SecurityPermissions mPermission = ValidateUserPermission(int.Parse(screenID));

                    return Json(new { items = list, Permissions = mPermission }, JsonRequestBehavior.AllowGet);
                }
                else if (tabId.Equals("Surveyor"))
                {
                    var list = AdjusterModel.FetchSurveyor();
                    string screenID = (new AdjusterModel()).listscreenId != null ? (new AdjusterModel()).listscreenId : "0";
                    SecurityPermissions mPermission = ValidateUserPermission(int.Parse(screenID));
                    return Json(new { items = list, Permissions = mPermission }, JsonRequestBehavior.AllowGet);
                }
                else if (tabId.Equals("Lawyer"))
                {
                    var sol = AdjusterModel.FetchSolicitor();
                    string screenID = (new AdjusterModel()).listscreenId != null ? (new AdjusterModel()).listscreenId : "0";
                    SecurityPermissions mPermission = ValidateUserPermission(int.Parse(screenID));
                    return Json(new { items = sol, Permissions = mPermission }, JsonRequestBehavior.AllowGet);
                }
                else if (tabId.Equals("Workshop"))
                {
                    var list = DepotMasterModel.FetchDepotMaster();
                    string screenID = (new DepotMasterModel()).listscreenId != null ? (new DepotMasterModel()).listscreenId : "0";
                    SecurityPermissions mPermission = ValidateUserPermission(int.Parse(screenID));
                    return Json(new { items = list, Permissions = mPermission }, JsonRequestBehavior.AllowGet);
                }
                else if (tabId.Equals("Hospital"))
                {
                    var list = HospitalModel.Fetch();
                    string screenID = (new HospitalModel()).listscreenId != null ? (new HospitalModel()).listscreenId : "0";
                    SecurityPermissions mPermission = ValidateUserPermission(int.Parse(screenID));
                    return Json(new { items = list, Permissions = mPermission }, JsonRequestBehavior.AllowGet);
                }
                else if (tabId.Equals("Adjuster"))
                {
                    var list = AdjusterModel.FetchAdjuster();
                    string screenID = (new AdjusterModel()).listscreenId != null ? (new AdjusterModel()).listscreenId : "0";
                    SecurityPermissions mPermission = ValidateUserPermission(int.Parse(screenID));
                    return Json(new { items = list, Permissions = mPermission }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Error = "Error: not a valid TabId"  }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Service Provider master '" + tabId + "'.");
                addInfo.Add("entity_type", "Service provider Master '" + tabId + "'");
                PublishException(ex, addInfo, 0, "Master " + tabId);
                return Json(new { Error = "Error:" + ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #region Adjuster
        public ActionResult AdjusterIndex()
        {
            List<AdjusterModel> list = new List<AdjusterModel>();
            try 
            {
                list = AdjusterModel.FetchAdjuster();
               
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message , ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Adjuster master " + list + " for Adjuster." + list + ".");
                addInfo.Add("entity_type", "Adjuster Master");
                PublishException(ex, addInfo, 0, "Adjuster Master" + list);
                return View(list);
               
            }
            return View(list);
           
        }

        public JsonResult FillAdjuster()
        {
            var returnData = AdjusterModel.FetchAdjuster();
            returnData.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AdjusterIndex(string AdjusterCode)
        {
            List<AdjusterModel> _adj11 = new List<AdjusterModel>();
            AdjusterCode = Request.Form["inputAdjusterName"].Trim();
            ViewBag.adjuster = AdjusterCode;
            try {
                if (AdjusterCode != "")
                {
                    List<AdjusterModel> _adj = new List<AdjusterModel>();
                    _adj = GetResult(AdjusterCode).ToList();
                    return View(_adj);
                }
                else
                {
                    
                    _adj11 = AdjusterModel.FetchAdjuster();
                    
                }
            }
            catch (Exception ex) {
                ErrorLog(ex.Message,ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Adjuster master " + AdjusterCode + " for Search result Adjuster." + AdjusterCode + ".");
                addInfo.Add("entity_type", "Adjuster Master");
                PublishException(ex, addInfo, 0, "Adjuster Master" + AdjusterCode);

            }

            return View(_adj11);     
             
          
                    
           
        }

        public IQueryable<AdjusterModel> GetResult(string adjcode)
        {

            var SearchResult = (from x in _db.MNT_Adjusters orderby x.AdjusterName
                                where x.AdjusterName.Contains(adjcode) && x.AdjusterTypeCode.Contains("adj")
                                select x).ToList().Select(x => new AdjusterModel
                                {
                                    AdjusterCode = x.AdjusterCode,
                                    AdjusterName = x.AdjusterName,
                                    AdjusterTypeCode = x.AdjusterTypeCode,
                                    AdjusterAddress=new AddressModel(){
                                        Status = x.Status
                                    },
                                    
                                    AdjusterId=x.AdjusterId
                                }).AsQueryable();
            return SearchResult;

        }
        
        [HttpGet]
        public ActionResult AdjusterEditor(int? AdjusterId)
        {
            var Adj = new AdjusterModel();
            if (AdjusterId.HasValue)
            {
                var Surveys = (from lt in _db.MNT_Adjusters where lt.AdjusterId == AdjusterId select lt).FirstOrDefault();
                AdjusterModel adjust = new AdjusterModel();
                adjust.AdjusterCode = Surveys.AdjusterCode;
                adjust.AdjusterAddress = new AddressModel() {
                    InsurerName = Surveys.AdjusterName,
                    Address1 = Surveys.Address1,
                    Address2 = Surveys.Address2,
                    Address3 = Surveys.Address3,
                    OffNo1 = Surveys.TelNoOff,
                    EmailAddress1 = Surveys.EMail,
                    Fax1 = Surveys.FaxNo,
                    MobileNo1 = Surveys.MobileNo,
                    FirstContactPersonName = Surveys.ConPer,
                    PostalCode = Surveys.PostCode,
                    Status = Surveys.Status,
                    City = Surveys.City,
                    Country = Surveys.Country,
                    State = Surveys.Province,
                    SecondContactPersonName = Surveys.Classification,
                    EmailAddress2 = Surveys.EmailAddress2,
                    OffNo2 = Surveys.OffNo2,
                    MobileNo2 = Surveys.MobileNo2,
                    Fax2 = Surveys.Fax2,
                    InsurerType = Surveys.InsurerType,
                    EffectiveFromDate = Surveys.EffectiveFrom,
                    Effectiveto = Surveys.EffectiveTo,
                    Remarks = Surveys.Remarks
                };
                adjust.AdjusterAddress.Insurerlist = LoadLookUpValue("InsurerType");
                adjust.AdjusterAddress.usercountrylist = LoadCountry();
                adjust.AdjusterAddress.citylist = LoadCity();
                adjust.AdjusterAddress.Statuslist = LoadLookUpValue("STATUS");
                adjust.VATNo = Surveys.VATNo;
                adjust.VATPer =Convert.ToDecimal(Surveys.VATPer);
                adjust.AdjSrc = Surveys.AdjSrc;
                adjust.SolicitorTypelist = LoadLookUpValue("ADJSRC");
                adjust.AdjType = Surveys.AdjType;
                adjust.AdjusterSourcelist = LoadLookUpValue("CLMADJUSTER");
                adjust.GST = Surveys.VAT;
                adjust.GSTList = LoadGST();
                adjust.AdjusterId = AdjusterId;

                adjust.CreatedBy = Surveys.CreatedBy == null ? " " : Surveys.CreatedBy;
                if (Surveys.CreatedDate != null)
                    adjust.CreatedOn = (DateTime)Surveys.CreatedDate;
                else
                    adjust.CreatedOn = DateTime.MinValue;
                adjust.ModifiedBy = Surveys.ModifiedBy == null ? " " : Surveys.ModifiedBy;
                adjust.ModifiedOn = Surveys.ModifiedDate;

                return View(adjust);

            }
            Adj.AdjusterId = AdjusterId;
            Adj.AdjusterAddress.Insurerlist = LoadLookUpValue("InsurerType");
            Adj.AdjusterAddress.usercountrylist = LoadCountry();
            Adj.AdjusterAddress.citylist = LoadCity();
            Adj.SolicitorTypelist = LoadLookUpValue("ADJSRC");
            Adj.AdjusterSourcelist = LoadLookUpValue("CLMADJUSTER");
            Adj.GSTList = LoadGST();
            Adj.AdjusterAddress.Statuslist = LoadLookUpValue("STATUS");
            return View(Adj);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AdjusterEditor(AdjusterModel model, AddressModel addModel)
        {
            try
            {
                addModel.Insurerlist = LoadLookUpValue("InsurerType");
                addModel.Statuslist = LoadLookUpValue("STATUS");
                addModel.citylist = LoadCity();
                addModel.usercountrylist = LoadCountry();
                model.SolicitorTypelist = LoadLookUpValue("ADJSRC");
                model.AdjusterSourcelist = LoadLookUpValue("CLMADJUSTER");
                model.GSTList = LoadGST();
                model.AdjusterAddress = addModel;
                TempData["display"] = "";
                TempData["notice"] = "";
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }

                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var AdjusterName = (from lt in _db.MNT_Adjusters where lt.AdjusterId == model.AdjusterId select lt).FirstOrDefault();
                    if (AdjusterName == null) 
                    {
                        model.CreatedBy = LoggedInUserName;
                        var survey = model.AdjusterUpdate();
                        TempData["notice"] = "Records Saved Successfully.";
                        TempData["display"] = survey.AdjusterCode;
                    }
                    else 
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var survey = model.AdjusterUpdate();
                        TempData["notice"] = "Records Updated Successfully.";
                    }
                   
                                       
                }
                else
                {                    
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message,ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving adjuster name " + model.AdjusterCode + " for adjuster." + model.AdjusterCode + ".");
                addInfo.Add("entity_type", "adjuster Master");
                PublishException(ex, addInfo, 0, "adjuster Master" + model.AdjusterCode);
                return View(model);
            }
            return View(model);
        }

        #endregion


        #region Surveyor Master

        public ActionResult SurveyorIndex() {
            List<AdjusterModel> list = new List<AdjusterModel>();
            try {
                list = AdjusterModel.FetchSurveyor();
            }
            
            
            catch(Exception ex) {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Surveyor master " + list + " for Surveyor." + list + ".");
                addInfo.Add("entity_type", "Surveyor Master");
                PublishException(ex, addInfo, 0, "Surveyor Master" + list);
                return View(list);
                    
            }
            return View(list);
        }
        public JsonResult FillProducts()
        {
            var returnData = LoadProducts();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


       

        public JsonResult FillSurveyor() {
            var returnData = AdjusterModel.FetchSurveyor();
            returnData.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillTextBoxes( int id) {
            var result = new { Value =  id};
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SurveyorIndex(string SurveyorCode)
        {
         //   SurveyorCode = ((Request.Form["ddlSurveyor"] == Convert.ToString(0)) ? "" : Request.Form["ddlSurveyor"]);

            List<AdjusterModel> adj = new List<AdjusterModel>();
            try
            {
                SurveyorCode = Request.Form["inputSurveyorName"].Trim();
                ViewBag.surveyor = SurveyorCode;
                TempData["svyCode"] = SurveyorCode;
                if (SurveyorCode !="")
                {
                    List<AdjusterModel> list = new List<AdjusterModel>();
                    list = GetSearchResult(SurveyorCode).ToList();
                    return View(list);
                }
                else
                {
                    
                    adj = AdjusterModel.FetchSurveyor();
                    return View(adj);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Surveyor master " + SurveyorCode + " for Search result Surveyor." + SurveyorCode + ".");
                addInfo.Add("entity_type", "Surveyor Master");
                PublishException(ex, addInfo, 0, "Surveyor Master" + SurveyorCode);
            }
            return View(adj);
            
        }
        public IQueryable<AdjusterModel> GetSearchResult(string SurveyorCode)
        
        {
            var searchResult = (from survey in _db.MNT_Adjusters orderby survey.AdjusterName 
                                where
                                  survey.AdjusterName.Contains(SurveyorCode) && survey.AdjusterTypeCode.Contains("SVY") 
                                  
                                select survey).ToList().Select(item => new AdjusterModel
                                {
                                    AdjusterCode = item.AdjusterCode,
                                    AdjusterName = item.AdjusterName,
                                    AdjusterAddress = new AddressModel()
                                    {
                                        Status=item.Status,
                                    },
                                    
                                    AdjusterId=item.AdjusterId
                                 }).AsQueryable();

            return searchResult;
        }

        [HttpGet]
        public ActionResult SurveyorEditor(int? AdjusterId)
        {
        
            var surve = new AdjusterModel();
            AdjusterModel adjust = new AdjusterModel();
            try
            {
                if (AdjusterId.HasValue)
                {
                    var Surveys = (from lt in _db.MNT_Adjusters where lt.AdjusterId == AdjusterId select lt).FirstOrDefault();

                    adjust.AdjusterCode = Surveys.AdjusterCode;
                    adjust.AdjusterAddress = new AddressModel()
                    {
                        InsurerName = Surveys.AdjusterName,
                        Address1 = Surveys.Address1,
                        Address2 = Surveys.Address2,
                        Address3 = Surveys.Address3,
                        OffNo1 = Surveys.TelNoOff,
                        EmailAddress1 = Surveys.EMail,
                        Fax1 = Surveys.FaxNo,
                        MobileNo1 = Surveys.MobileNo,
                        FirstContactPersonName = Surveys.ConPer,
                        PostalCode = Surveys.PostCode,
                        Status = Surveys.Status,
                        City = Surveys.City,
                        Country = Surveys.Country,
                        State = Surveys.Province,
                        SecondContactPersonName = Surveys.Classification,
                        EmailAddress2 = Surveys.EmailAddress2,
                        OffNo2 = Surveys.OffNo2,
                        MobileNo2 = Surveys.MobileNo2,
                        Fax2 = Surveys.Fax2,
                        InsurerType = Surveys.InsurerType,
                        EffectiveFromDate = Surveys.EffectiveFrom,
                        Effectiveto = Surveys.EffectiveTo,
                        Remarks = Surveys.Remarks

                    };
                    adjust.AdjusterId = AdjusterId;
                    adjust.AdjusterAddress.Insurerlist = LoadLookUpValue("InsurerType");
                    adjust.AdjusterAddress.usercountrylist = LoadCountry();
                    adjust.AdjusterAddress.citylist = LoadCity();
                    adjust.AdjusterAddress.Statuslist = LoadLookUpValue("STATUS");
                    adjust.AdjusterName = Surveys.AdjusterName;
                    adjust.VATNo = Surveys.VATNo;
                    adjust.GST = Surveys.VAT;
                    adjust.GSTList = LoadGST();
                    adjust.VATPer = Convert.ToDecimal(Surveys.VATPer);

                    adjust.CreatedBy = Surveys.CreatedBy == null ? " " : Surveys.CreatedBy;
                    if (Surveys.CreatedDate != null)
                        adjust.CreatedOn = (DateTime)Surveys.CreatedDate;
                    else
                        adjust.CreatedOn = DateTime.MinValue;
                    adjust.ModifiedBy = Surveys.ModifiedBy == null ? " " : Surveys.ModifiedBy;
                    adjust.ModifiedOn = Surveys.ModifiedDate;

                    return View(adjust);

                }
                surve.AdjusterAddress.Insurerlist = LoadLookUpValue("InsurerType");
                surve.AdjusterAddress.usercountrylist = LoadCountry();
                surve.AdjusterAddress.citylist = LoadCity();
                surve.GSTList = LoadGST();
                surve.AdjusterAddress.Statuslist = LoadLookUpValue("STATUS");
                return View(surve);
            }
            catch (Exception ex) {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving surveyor name " + adjust.AdjusterName + " for surveyor." + adjust.AdjusterCode + ".");
                addInfo.Add("entity_type", "surveyor Master");
                PublishException(ex, addInfo, 0, "surveyor Master" + adjust.AdjusterCode);
            }
            return View(surve);

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SurveyorEditor(AdjusterModel model, AddressModel addModel)
        {
                       
            try
            {
                model.GSTList = LoadGST();
                addModel.Insurerlist = LoadLookUpValue("InsurerType");
                addModel.Statuslist = LoadLookUpValue("STATUS");
                addModel.citylist = LoadCity();
                addModel.usercountrylist = LoadCountry();
                model.AdjusterAddress = addModel;

                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }

                if (ModelState.IsValid)
                {
                TempData["display"] = "";
                    TempData["notice"] = "";
                    ModelState.Clear();
                    var SurveyorName = (from lt in _db.MNT_Adjusters where lt.AdjusterId == model.AdjusterId select lt).FirstOrDefault();
                    if (SurveyorName == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var survey = model.SurveyUpdate();
                        TempData["notice"] = "Record Saved Successfully.";
                        TempData["display"] = survey.AdjusterCode;
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var survey = model.SurveyUpdate();
                        TempData["notice"] = "Record Updated Successfully.";
                    }
                                                         
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message,ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving surveyor name " + model.AdjusterCode + " for surveyor." + model.AdjusterName + ".");
                addInfo.Add("entity_type", "insurer Master");
                PublishException(ex, addInfo, 0, "surveyor Master" + model.AdjusterName);
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult GetGstPercent(int Id)
        {
            
            var models = (from m in _db.MNT_GST where m.Id == Id select m.Rate).FirstOrDefault();
            string  str = models.ToString();
            return Content(str);
            
        }

        [HttpGet]
        public JsonResult CheckCompanyname11(string companyname)
        {
            var allCompanyName = _db.MNT_Adjusters.ToList();

            var isDuplicate = false;

            foreach (var adjuster in allCompanyName)
            {
                if (adjuster.AdjusterName == companyname)
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCompanyname(string AdjusterName, string AdjName)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Adjusters where t.AdjusterName == AdjusterName orderby t.AdjusterName descending select t.AdjusterName).Take(1)).FirstOrDefault();
            if ((num != null) && (AdjName == null|| AdjName==""))
            {
                result = true;
            }
            else if ((num != null && num.ToLower() != AdjName.ToLower()))
            {
                result = true;
            }

            return Json(result);
        }

        public JsonResult ValidEmail(string EmailAdd1)
        {
            var Emailresults = false;
            if (EmailAdd1 != null && EmailAdd1!="")
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

        
        #endregion

        #region Solicitor Master

        public ActionResult SolicitorIndex() {
            List<AdjusterModel> list = new List<AdjusterModel>();
            try { 
                list = AdjusterModel.FetchSolicitor();
            }
            
            
            catch(Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Lawyer master " + list + " for Lawyer." + list + ".");
                addInfo.Add("entity_type", "Lawyer Master");
                PublishException(ex, addInfo, 0, "Lawyer Master" + list);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult SolicitorIndex(string SolicitorCode) {
            //SolicitorCode = ((Request.Form["ddlSolicitor"] == null) ? "" : Request.Form["ddlSolicitor"]);
            List<AdjusterModel> sol = new List<AdjusterModel>();
            SolicitorCode = Request.Form["inputLawFirmName"].Trim();
            ViewBag.solicitor = SolicitorCode;
            try
            {
                if (SolicitorCode != "")
                {
                    List<AdjusterModel> list = new List<AdjusterModel>();
                    list = GetSolicitorSearchResult(SolicitorCode).ToList();
                    return View(list);
                }
                else
                {
                    
                    sol = AdjusterModel.FetchSolicitor();
                    return View(sol);

                }
            }
            catch (Exception ex) {
                ErrorLog(ex.Message,ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for lawyer master " + SolicitorCode + " for Search result lawyer." + SolicitorCode + ".");
                addInfo.Add("entity_type", "Surveyor Master");
                PublishException(ex, addInfo, 0, "Surveyor Master" + SolicitorCode);

            }
            return View(sol);
            
        }

        public IQueryable<AdjusterModel> GetSolicitorSearchResult(string SolicitorCode)
        {
            var searchResult = (from survey in _db.MNT_Adjusters orderby survey.AdjusterName
                                where
                                  survey.AdjusterName.Contains(SolicitorCode) && survey.AdjusterTypeCode.Contains("SOL")
                                  
                                select survey).ToList().Select(item => new AdjusterModel
                                {
                                    AdjusterCode = item.AdjusterCode,
                                    AdjusterName = item.AdjusterName,
                                    AdjusterAddress = new AddressModel()
                                    {
                                        Status = item.Status
                                    },
                                    AdjusterId=item.AdjusterId
                                }).AsQueryable();

            return searchResult;
        }

        public JsonResult FillSolicitor()
        {
            var returnData = AdjusterModel.FetchSolicitor();
            returnData.Insert(0, new AdjusterModel() { AdjusterCode = "", AdjusterName = "[Select...]" });
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SolicitorEditor(int? AdjusterId)
        {
            var sol = new AdjusterModel();
            if (AdjusterId.HasValue)
            {
                var Surveys = (from lt in _db.MNT_Adjusters where lt.AdjusterId == AdjusterId select lt).FirstOrDefault();
                AdjusterModel adjust = new AdjusterModel();
                adjust.AdjusterCode = Surveys.AdjusterCode;
                adjust.AdjusterName = Surveys.AdjusterName;
                adjust.AdjusterAddress = new AddressModel()
                {
                    InsurerName = Surveys.AdjusterName,
                    Address1 = Surveys.Address1,
                    Address2 = Surveys.Address2,
                    Address3 = Surveys.Address3,
                    OffNo1 = Surveys.TelNoOff,
                    EmailAddress1 = Surveys.EMail,
                    Fax1 = Surveys.FaxNo,
                    MobileNo1 = Surveys.MobileNo,
                    FirstContactPersonName = Surveys.ConPer,
                    PostalCode = Surveys.PostCode,
                    Status = Surveys.Status,
                    City = Surveys.City,
                    Country = Surveys.Country,
                    State = Surveys.Province,
                    SecondContactPersonName = Surveys.Classification,
                    EmailAddress2 = Surveys.EmailAddress2,
                    OffNo2 = Surveys.OffNo2,
                    MobileNo2 = Surveys.MobileNo2,
                    Fax2 = Surveys.Fax2,
                    InsurerType = Surveys.InsurerType,
                    EffectiveFromDate = Surveys.EffectiveFrom,
                    Effectiveto = Surveys.EffectiveTo,
                    Remarks = Surveys.Remarks
                };
                adjust.AdjusterId = AdjusterId;
                adjust.AdjusterAddress.Insurerlist = LoadLookUpValue("InsurerType");
                adjust.AdjusterAddress.usercountrylist = LoadCountry();
                adjust.AdjusterAddress.citylist = LoadCity();
                adjust.AdjusterAddress.Statuslist = LoadLookUpValue("STATUS");
                adjust.VATNo = Surveys.VATNo;
                adjust.VATPer = Convert.ToDecimal(Surveys.VATPer);
                adjust.AdjSrc = Surveys.AdjSrc;
                adjust.SolicitorTypelist = LoadLookUpValue("ADJSRC");
                adjust.GST = Surveys.VAT;
                adjust.GSTList = LoadGST();

                adjust.CreatedBy = Surveys.CreatedBy == null ? " " : Surveys.CreatedBy;
                if (Surveys.CreatedDate != null)
                    adjust.CreatedOn = (DateTime)Surveys.CreatedDate;
                else
                    adjust.CreatedOn = DateTime.MinValue;
                adjust.ModifiedBy = Surveys.ModifiedBy == null ? " " : Surveys.ModifiedBy;
                adjust.ModifiedOn = Surveys.ModifiedDate;

                return View(adjust);

            }
            sol.AdjusterAddress.Insurerlist = LoadLookUpValue("InsurerType");
            sol.AdjusterAddress.usercountrylist = LoadCountry();
            sol.AdjusterAddress.citylist = LoadCity();
            sol.SolicitorTypelist = LoadLookUpValue("ADJSRC");
            sol.GSTList = LoadGST();
            sol.AdjusterAddress.Statuslist = LoadLookUpValue("STATUS");
            return View(sol);

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SolicitorEditor(AdjusterModel model, AddressModel addModel)
        {
            try
            {
                addModel.Insurerlist = LoadLookUpValue("InsurerType");
                addModel.Statuslist = LoadLookUpValue("STATUS");
                addModel.citylist = LoadCity();
                addModel.usercountrylist = LoadCountry();
                model.SolicitorTypelist = LoadLookUpValue("ADJSRC");
                model.GSTList = LoadGST();
                model.AdjusterAddress = addModel;

                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }


                if (ModelState.IsValid)
                {
                    TempData["display"] = "";
                    TempData["notice"] = "";
                    ModelState.Clear();
                    var SolicitorName = (from lt in _db.MNT_Adjusters where lt.AdjusterId == model.AdjusterId select lt).FirstOrDefault();
                    if (SolicitorName == null) {
                       
                        model.CreatedBy = LoggedInUserName;
                        var survey = model.SolicitorUpdate();
                        TempData["notice"] = "Records Saved Successfully.";
                        TempData["display"] = survey.AdjusterCode;
                    } else {
                        model.ModifiedBy = LoggedInUserName;
                        var survey = model.SolicitorUpdate();
                        TempData["notice"] = "Records Updated Successfully.";
                    }
                                                       
                    
                }
                else
                {
                   
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message,ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving lawyer name " + model.AdjusterCode + " for insurer." + model.AdjusterCode + ".");
                addInfo.Add("entity_type", "lawyer Master");
                PublishException(ex, addInfo, 0, "lawyer Master" + model.AdjusterCode);
                return View(model);
                
            }
            return View(model);
        }

#endregion


    }
}
