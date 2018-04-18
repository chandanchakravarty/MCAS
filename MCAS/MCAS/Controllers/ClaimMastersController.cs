using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;
using System.Data.Objects;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using MCAS.Web.Objects.Resources.Common;

namespace MCAS.Controllers
{
    public class ClaimMastersController : BaseController
    {
        #region "Loss Type Master"
        MCASEntities obj = new MCASEntities();
        //
        // GET: /ClaimMasters/

        public ActionResult Index()
        {
            List<LossTypeModel> list = new List<LossTypeModel>();
            try
            {
                list = LossTypeModel.GetLossTypeSearchResult("", "", "");
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Type of Loss " + list + " for Loss." + list + ".");
                addInfo.Add("entity_type", "Type of Loss");
                PublishException(ex, addInfo, 0, "Type of Loss" + list);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(string MainClassCode, string LossTypeCode, string SubClassCode)
        {
            MainClassCode = ((Request.Form["ddlMainClassCode"] == null) ? "" : Request.Form["ddlMainClassCode"]);
            SubClassCode = ((Request.Form["ddlSubClass"] == null) ? "" : Request.Form["ddlSubClass"]);
            LossTypeCode = ((Request.Form["ddlLossType"] == null) ? "" : Request.Form["ddlLossType"]);
            ViewBag.mainclass = MainClassCode;
            ViewBag.lossnature = LossTypeCode;
            ViewBag.subclass = SubClassCode;
            List<LossTypeModel> list = new List<LossTypeModel>();
            try
            {
                list = LossTypeModel.GetLossTypeSearchResult(MainClassCode, SubClassCode, LossTypeCode);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Type of loss " + LossTypeCode + " for Search result Type of loss." + MainClassCode + ".");
                addInfo.Add("entity_type", "Vehicle Class");
                PublishException(ex, addInfo, 0, "Vehicle Class" + LossTypeCode);
            }
            return View(list);
        }

        public ActionResult Create(int? TranId)
        {
            LossTypeModel objmodel = new LossTypeModel();
            try
            {
                if (TranId.HasValue)
                {
                    var losstypelist = (from lt in obj.MNT_LossType where lt.TranId == TranId select lt).FirstOrDefault();
                    objmodel.TranId = losstypelist.TranId;
                    objmodel.LossTypeName = losstypelist.LossTypeName;
                    objmodel.LossTypeCode = losstypelist.LossTypeCode;
                    objmodel.ProductCode = losstypelist.ProductCode;
                    objmodel.SubClassCode = losstypelist.SubClassCode;
                    objmodel.ProductList = LoadProductsDescription();
                    objmodel.SubClassList = LoadSubCodeDescription();

                    objmodel.CreatedBy = losstypelist.CreatedBy == null ? " " : losstypelist.CreatedBy;
                    if (losstypelist.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)losstypelist.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = losstypelist.ModifiedBy == null ? " " : losstypelist.ModifiedBy;
                    objmodel.ModifiedOn = losstypelist.ModifiedDate;
                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Type of Class " + TranId + " for Search result Type of Class." + TranId + ".");
                addInfo.Add("entity_type", "Vehicle Class");
                PublishException(ex, addInfo, 0, "Type of Class" + TranId);
            }

            objmodel.ProductList = LoadProductsDescription();
            objmodel.SubClassList = LoadSubCodeDescription();
            return View(objmodel);
        }

        [HttpPost]
        public ActionResult Create(LossTypeModel model)
        {
            TempData["result"] = "";
            try
            {
                model.ProductList = LoadProductsDescription();
                model.SubClassList = LoadSubCodeDescription();
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_LossType where x.TranId == model.TranId select x).FirstOrDefault();
                    if (hits == null)
                    {

                        model.CreatedBy = LoggedInUserName;
                        var losstypelist = model.Update();
                        //TempData["result"] = "Records Saved Successfully.";
                        TempData["result"] = Common.RecordsSavedSuccessfully;

                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var losstypelist = model.Update();
                        //TempData["result"] = "Records Updated Successfully.";
                        TempData["result"] = Common.RecordsUpdatedSuccessfully;
                    }


                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving Type of loss " + model.TranId + " for Vehicle Class." + model.TranId + ".");
                addInfo.Add("entity_type", "Type of loss");
                PublishException(ex, addInfo, 0, "Type of loss" + model.TranId);
                return View(model);
            }
            return View(model);
        }



        #endregion

        #region "Loss Nature Master"
        public ActionResult LossNatureMasterList()
        {
            List<LossNatureListItems> list = new List<LossNatureListItems>();
            list = LossNatureListItems.FetchList();
            return View(list);
        }

        [HttpPost]
        public ActionResult LossNatureMasterList(string MainClassCode, string LossNature, string SubClassCode)
        {
            MainClassCode = ((Request.Form["ddlMainClassCode"] == null) ? "" : Request.Form["ddlMainClassCode"]);
            LossNature = ((Request.Form["ddlLossType"] == null) ? "" : Request.Form["ddlLossType"]);
            SubClassCode = ((Request.Form["ddlSubClass"] == null) ? "" : Request.Form["ddlSubClass"]);
            ViewBag.mainclass = MainClassCode;
            ViewBag.lossnature = LossNature;
            ViewBag.subclass = SubClassCode;
            List<LossNatureListItems> list = new List<LossNatureListItems>();
            list = GetSearchResult(MainClassCode, LossNature, SubClassCode).ToList();
            return View(list);
        }

        public JsonResult FillProducts()
        {
            var returnData = LoadProductsDescription();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillSubClassProducts()
        {
            var returnData = LoadSubCodeDescription();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillLossType()
        {
            var returnData = LoadLossType();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }



        public IQueryable<LossNatureListItems> GetSearchResult(string ProductCode, string LossType, string subclasscode)
        {

            //var query = from m in db.Members
            //   join c in db.Companies on m.CompanyID equals c.CompanyID
            //   select new {m.MemberID,m.MemberName,m.CompanyID,c.CompanyName};

            //  var query1 = from lossnature in obj.MNT_LossNature join p in obj.MNT_Products on lossnature.ProductCode equals p.ProductCode
            //join s in obj.MNT_ProductClass on lossnature.SubClassCode equals s.ClassCode
            //select new {lossnature.LossNatureDescription,p.ProductDisplayName,s.ClassDesc};





            var searchResult = (from l in obj.MNT_LossNature
                                join p in obj.MNT_Products on l.ProductCode equals p.ProductCode
                                join s in obj.MNT_ProductClass on l.SubClassCode equals s.ClassCode

                                where
                                  l.ProductCode.Contains(ProductCode) &&
                                  l.LossType.Contains(LossType) &&
                                  l.SubClassCode.Contains(subclasscode)

                                select new LossNatureListItems
                               {
                                   ProductCode = p.ProductCode,
                                   ProductDisplayName = p.ProductDisplayName,
                                   LossType = l.LossType,
                                   LossNatureDescription = l.LossNatureDescription,
                                   LossNatureCode = l.LossNatureCode,
                                   SubClassCode = s.ClassDesc,
                                   TranId = l.TranId
                               }).AsQueryable();

            //var search = from l in obj.MNT_LossNature 
            //             from p in obj.MNT_Products
            //             from s in obj.MNT_ProductClass
            //             where l.ProductCode == p.ProductCode && l.SubClassCode == s.ClassCode
            //             select new LossNatureListItems()
            //             {
            //                 LossNatureCode=l.LossNatureCode,
            //                 ProductCode = p.ProductDisplayName,
            //                 LossType = l.LossType,
            //                 LossNatureDescription = l.LossNatureDescription,
            //                 SubClassCode = s.ClassDesc,
            //                 TranId = l.TranId
            //             };
            //return search.AsQueryable();
            //var list = new List<MNT_LossNature>();

            //foreach (var item in search) {
            //    list.Add(new MNT_LossNature()
            //    {
            //        ProductCode = item.prodcode.ProductDisplayName,
            //        LossType = item.lossnature.LossType,
            //        LossNatureDescription = item.lossnature.LossNatureDescription,
            //        TranId = item.lossnature.TranId

            //    });
            //    return 
            //}

            return searchResult;
        }

        public ActionResult LossNatureMasterEditor(int? TranId)
        {
            TempData["LossNatureCode"] = "";
            var lossnature = new LossNatureListItems();
            if (TranId.HasValue)
            {
                var lossnaturelist = (from lt in obj.MNT_LossNature where lt.TranId == TranId select lt).FirstOrDefault();
                LossNatureListItems objmodel = new LossNatureListItems();
                objmodel.LossType = lossnaturelist.LossType;
                objmodel.LossTypeList = LoadLossType();
                //  TempData["LossNatureCode"] = "Loss Nature Code - " + objmodel.LossNatureCode + ".";
                objmodel.LossNatureCode = lossnaturelist.LossNatureCode;
                objmodel.LossNatureDescription = lossnaturelist.LossNatureDescription;
                objmodel.ProductCode = lossnaturelist.ProductCode;
                objmodel.ProductList = LoadProductsDescription();
                objmodel.SubClassCode = lossnaturelist.SubClassCode;
                objmodel.SubClassList = LoadSubCodeDescription();

                return View(objmodel);
            }

            lossnature.LossTypeList = LoadLossType();
            lossnature.ProductList = LoadProductsDescription();
            lossnature.SubClassList = LoadSubCodeDescription();
            return View(lossnature);
        }

        [HttpPost]
        public ActionResult LossNatureMasterEditor(LossNatureListItems model)
        {
            TempData["result"] = "";
            TempData["LossNatureCode"] = "";
            model.ProductList = LoadProductsDescription();
            model.LossTypeList = LoadLossType();
            model.SubClassList = LoadSubCodeDescription();

            try
            {

                if (ModelState.IsValid)
                {
                    var hits = (from x in _db.MNT_LossNature where x.TranId == model.TranId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        ModelState.Clear();
                        var lossnaturelist = model.Update();
                        TempData["result"] = "Records Saved Successfully.";
                        //  TempData["LossNatureCode"] = "Loss Nature Code - " + lossnaturelist.LossNatureCode + ".";
                    }
                    else
                    {
                        ModelState.Clear();
                        var lossnaturelist = model.Update();
                        TempData["result"] = "Records Updated Successfully.";
                        //  TempData["LossNatureCode"] = "Loss Nature Code - " + lossnaturelist.LossNatureCode + ".";
                    }


                }
                else
                {
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }
        #endregion

        #region Claim Close
        MCASEntities _db = new MCASEntities();

        [HttpGet]
        public ActionResult ClaimCloseIndex()
        {

            List<ClaimCloseModel> list = new List<ClaimCloseModel>();
            try
            {
                list = ClaimCloseModel.Fetch();

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Claim Close master " + list + " for Claim Close." + list + ".");
                addInfo.Add("entity_type", "Claim Close");
                PublishException(ex, addInfo, 0, "Claim Close" + list);
                return View(list);

            }

            return View(list);
        }

        [HttpPost]
        public JsonResult FindClaimClose(string prefixText)
        {
            var suggestedCCode = from x in _db.MNT_ClaimClosed
                                 where x.CloseDesc.StartsWith(prefixText)
                                 select new
                                 {
                                     id = x.Id,
                                     closedesc = x.CloseCode,
                                     value = x.CloseDesc
                                 };
            var result = Json(suggestedCCode.Take(10).ToList());
            return result;


        }


        [HttpPost]
        public ActionResult ClaimCloseIndex(string claimdesc)
        {
            claimdesc = Request.Form["inputClaimReasonDesc"].Trim();
            ViewBag.claimdesc = claimdesc;
            List<ClaimCloseModel> list = new List<ClaimCloseModel>();
            try
            {
                list = GetClaimCloseResult(claimdesc).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for claim close master " + claimdesc + " for Search result claim close." + claimdesc + ".");
                addInfo.Add("entity_type", "claim close");
                PublishException(ex, addInfo, 0, "Surveyor Master" + claimdesc);
            }
            return View(list);

        }

        public IQueryable<ClaimCloseModel> GetClaimCloseResult(string claimdesc)
        {
            var searchResult = (from claims in _db.MNT_ClaimClosed
                                where
                                claims.CloseDesc.Contains(claimdesc)
                                select claims).ToList().Select(item => new ClaimCloseModel
                                {
                                    CloseCode = item.CloseCode,
                                    CloseDescrpition = item.CloseDesc,
                                    Id = item.Id
                                }).AsQueryable();

            return searchResult;
        }


        [HttpGet]
        public ActionResult ClaimCloseEditor(int? Id)
        {

            var claimclose = new ClaimCloseModel();
            ClaimCloseModel objmodel = new ClaimCloseModel();
            try
            {
                if (Id.HasValue)
                {
                    var Closelist = (from x in _db.MNT_ClaimClosed where x.Id == Id select x).FirstOrDefault();
                    objmodel.Id = Closelist.Id;
                    objmodel.CloseCode = Closelist.CloseCode;
                    objmodel.CloseDescrpition = Closelist.CloseDesc;

                    objmodel.CreatedBy = Closelist.CreatedBy == null ? " " : Closelist.CreatedBy;
                    if (Closelist.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)Closelist.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = Closelist.ModifiedBy == null ? " " : Closelist.ModifiedBy;
                    objmodel.ModifiedOn = Closelist.ModifiedDate;

                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving claim close name " + objmodel.CloseCode + " for claim close." + objmodel.Id + ".");
                addInfo.Add("entity_type", "surveyor Master");
                PublishException(ex, addInfo, 0, "claim close" + objmodel.Id);
            }
            return View(claimclose);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ClaimCloseEditor(ClaimCloseModel model)
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
                    var claimcloses = (from lt in _db.MNT_ClaimClosed where lt.Id == model.Id select lt).FirstOrDefault();
                    if (claimcloses == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var claimclose = model.Update();
                        //TempData["notice"] = "Records Saved Successfully.";
                        TempData["notice"] = Common.RecordsSavedSuccessfully;

                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var claimclose = model.Update();
                        //TempData["notice"] = "Records Updated Successfully.";
                        TempData["notice"] = Common.RecordsUpdatedSuccessfully;

                    }

                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving claim close name " + model.Id + " for surveyor." + model.CloseCode + ".");
                addInfo.Add("entity_type", "claim close");
                PublishException(ex, addInfo, 0, "claim close" + model.CloseCode);
                return View(model);
            }
            return View(model);
        }

        #endregion

        #region Claim Reopen



        [HttpGet]
        public ActionResult ClaimReOpenIndex()
        {
            List<ClaimReOpenModel> list = new List<ClaimReOpenModel>();
            list = ClaimReOpenModel.Fetch();
            return View(list);
        }


        [HttpPost]
        public ActionResult ClaimReOpenIndex(string ReopenCode, string ReOpenDesc)
        {
            ReopenCode = Request.Form["inputClaimReOpenCode"].Trim();
            ReOpenDesc = Request.Form["inputClaimReOpenDesc"].Trim();
            ViewBag.reopen = ReopenCode;
            ViewBag.reopendesc = ReOpenDesc;

            List<ClaimReOpenModel> list = new List<ClaimReOpenModel>();
            list = GetReOpenResult(ReopenCode, ReOpenDesc).ToList();
            return View(list);

        }

        public IQueryable<ClaimReOpenModel> GetReOpenResult(string ReopenCode, string ReOpenDesc)
        {
            var searchResult = (from reopens in _db.MNT_ClaimReOpened
                                where
                                reopens.ReopenCode.Contains(ReopenCode) &&
                                reopens.ReopenDesc.Contains(ReOpenDesc)
                                select reopens).ToList().Select(item => new ClaimReOpenModel
                                {
                                    ReOpenCode = item.ReopenCode,
                                    ReOpenDescription = item.ReopenDesc,
                                    Id = item.id
                                }).AsQueryable();

            return searchResult;
        }

        [HttpGet]
        public ActionResult ClaimReOpenEditor(int? Id)
        {

            var Reopen = new ClaimReOpenModel();
            if (Id.HasValue)
            {
                var ReOpened = (from x in _db.MNT_ClaimReOpened where x.id == Id select x).FirstOrDefault();
                ClaimReOpenModel objmodel = new ClaimReOpenModel();
                objmodel.ReOpenCode = ReOpened.ReopenCode;
                objmodel.ReOpenDescription = ReOpened.ReopenDesc;
                return View(objmodel);
            }
            return View(Reopen);

        }


        [HttpPost]
        public ActionResult ClaimReOpenEditor(ClaimReOpenModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var claimclose = model.Update();
                    TempData["notice"] = "Records saved successfully.";
                    //  return RedirectToAction("ClaimCloseIndex");
                }
                else
                {
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);

        }

        #endregion

        #region ClaimExpense
        [HttpGet]
        public ActionResult ClaimExpenseIndex()
        {

            List<ClaimExpenseModel> list = new List<ClaimExpenseModel>();
            try
            {
                list = ClaimExpenseModel.Fetch();

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Claim expense description " + list + " for Lawyer." + list + ".");
                addInfo.Add("entity_type", "Claim expense");
                PublishException(ex, addInfo, 0, "Claim expense" + list);
                return View(list);

            }


            return View(list);
        }

        public JsonResult FillClaimExpense()
        {
            var returnData = LoadClaimExpense();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ClaimExpenseIndex(string ExpenseCode, string ExpenseDesc)
        {
            ExpenseCode = ((Request.Form["ddlClaimExpense"] == null) ? "" : Request.Form["ddlClaimExpense"]);
            ViewBag.expensecode = ExpenseCode;
            List<ClaimExpenseModel> list = new List<ClaimExpenseModel>();
            try
            {
                list = GetExpenseResult(ExpenseCode).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for claim expense master " + ExpenseCode + " for Search claim expense master." + ExpenseCode + ".");
                addInfo.Add("entity_type", "claim expense master");
                PublishException(ex, addInfo, 0, "Surveyor Master" + ExpenseCode);
            }
            return View(list);

        }

        public IQueryable<ClaimExpenseModel> GetExpenseResult(string ExpenseCode)
        {
            var searchResult = (from claimExpense in _db.MNT_ClaimExpense
                                orderby claimExpense.ClaimExpenseDesc
                                where
                                claimExpense.ClaimExpenseCode.Contains(ExpenseCode)
                                select claimExpense).ToList().Select(item => new ClaimExpenseModel
                                {
                                    ClaimExpenseCode = item.ClaimExpenseCode,
                                    ClaimExpenseDesc = item.ClaimExpenseDesc,
                                    Id = item.Id
                                }).AsQueryable();

            return searchResult;
        }

        [HttpGet]
        public ActionResult ClaimExpenseEditor(int? Id)
        {

            var ClaimExpensed = new ClaimExpenseModel();
            ClaimExpenseModel objmodel = new ClaimExpenseModel();
            try
            {
                if (Id.HasValue)
                {
                    var ClaimExpense = (from x in _db.MNT_ClaimExpense where x.Id == Id select x).FirstOrDefault();
                    objmodel.Id = ClaimExpense.Id;
                    objmodel.ClaimExpenseCode = ClaimExpense.ClaimExpenseCode;
                    objmodel.ClaimExpenseDesc = ClaimExpense.ClaimExpenseDesc;
                   

                    objmodel.CreatedBy = ClaimExpense.CreatedBy == null ? " " : ClaimExpense.CreatedBy;
                    if (ClaimExpense.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)ClaimExpense.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = ClaimExpense.ModifiedBy == null ? " " : ClaimExpense.ModifiedBy;
                    objmodel.ModifiedOn = ClaimExpense.ModifiedDate;
                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving claim expense desc " + objmodel.ClaimExpenseCode + " for claim expense." + objmodel.ClaimExpenseCode + ".");
                addInfo.Add("entity_type", "claim expense Master");
                PublishException(ex, addInfo, 0, "surveyor Master" + objmodel.ClaimExpenseCode);
            }

            return View(ClaimExpensed);

        }

        [HttpPost]
        public ActionResult ClaimExpenseEditor(ClaimExpenseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    TempData["notice"] = "";
                    var claimExpenses = (from lt in _db.MNT_ClaimExpense where lt.Id == model.Id select lt).FirstOrDefault();
                    if (claimExpenses == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var claimExpense = model.Update();
                        //TempData["notice"] = "Record Saved Successfully.";
                        TempData["notice"] = Common.RecordsSavedSuccessfully;
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var claimExpense = model.Update();
                        //TempData["notice"] = "Record Updated Successfully.";
                        TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                    }


                }
                else
                {
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);

        }
        #endregion

        #region CurrencyMaster

        [HttpGet]
        public ActionResult CurrencyMasterIndex()
        {
            List<CurrencyMasterModel> list = new List<CurrencyMasterModel>();
            try
            {
                list = CurrencyMasterModel.Fetch();
            }

            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Currency master " + list + " for Currency master." + list + ".");
                addInfo.Add("entity_type", "Currency master");
                PublishException(ex, addInfo, 0, "Currency master" + list);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult CurrencyMasterIndex(string CurrencyCode, string CurrenDispCode, string CurrencyDesc)
        {
            CurrencyCode = Request.Form["inputCurrencyCode"].Trim();
            CurrenDispCode = Request.Form["inputCurrencyDisplayCode"].Trim();
            CurrencyDesc = Request.Form["inputCurrencyDescription"].Trim();
            ViewBag.currcode = CurrencyCode;
            ViewBag.currdispcode = CurrenDispCode;
            ViewBag.currdesc = CurrencyDesc;
            List<CurrencyMasterModel> list = new List<CurrencyMasterModel>();
            try
            {
                list = GetCurrencyResult(CurrencyCode, CurrenDispCode, CurrencyDesc).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Currency master " + CurrencyCode + " for Search result Currency master." + CurrencyCode + ".");
                addInfo.Add("entity_type", "Currency master");
                PublishException(ex, addInfo, 0, "Currency master" + CurrencyCode);
            }
            return View(list);
        }

        public IQueryable<CurrencyMasterModel> GetCurrencyResult(string CurrencyCode, string CurrenDispCode, string CurrencyDesc)
        {
            var searchResult = (from curency in _db.MNT_CurrencyM
                                where
                                  curency.CurrencyCode.Contains(CurrencyCode) &&
                                  curency.CurrencyDispCode.Contains(CurrenDispCode) &&
                                  curency.Description.Contains(CurrencyDesc)
                                select curency).ToList().Select(item => new CurrencyMasterModel
                                {
                                    CurrencyCode = item.CurrencyCode,
                                    CurrencyDispCode = item.CurrencyDispCode,
                                    Description = item.Description,
                                    TranId = item.TranId
                                }).AsQueryable();

            return searchResult;
        }



        [HttpGet]
        public ActionResult CurrencyMasterEditor(int? TranId)
        {
            var Currency = new CurrencyMasterModel();
            CurrencyMasterModel objmodel = new CurrencyMasterModel();
            try
            {
                if (TranId.HasValue)
                {
                    var current = (from x in _db.MNT_CurrencyM where x.TranId == TranId select x).FirstOrDefault();
                    objmodel.TranId = current.TranId;
                    objmodel.CurrencyCode = current.CurrencyCode;
                    objmodel.CurrencyDispCode = current.CurrencyDispCode;
                    objmodel.Description = current.Description;
                    objmodel.CreatedBy = current.CreatedBy == null ? " " : current.CreatedBy;
                    if (current.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)current.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = current.ModifiedBy == null ? " " : current.ModifiedBy;
                    objmodel.ModifiedOn = current.ModifiedDate;
                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Currency master  " + objmodel.CurrencyCode + " for claim close." + objmodel.CurrencyDispCode + ".");
                addInfo.Add("entity_type", "Currency master");
                PublishException(ex, addInfo, 0, "Currency master" + objmodel.CurrencyCode);
            }
            return View(Currency);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CurrencyMasterEditor(CurrencyMasterModel model)
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
                    var hits = (from x in _db.MNT_CurrencyM where x.TranId == model.TranId select x).FirstOrDefault();

                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var currency = model.Update();
                        //TempData["notice"] = "Records Saved Successfully.";
                        TempData["notice"] = Common.RecordsSavedSuccessfully;
                        return View(model);
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var currency = model.Update();
                        //TempData["notice"] = "Records Updated Successfully.";
                        TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                        return View(model);
                    }

                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Currency master " + model.CurrencyCode + " for Currency." + model.CurrencyDispCode + ".");
                addInfo.Add("entity_type", "Currency master");
                PublishException(ex, addInfo, 0, "Currency master" + model.CurrencyCode);
                return View(model);
            }
            // return View(model);
        }

        [HttpGet]
        public JsonResult CheckCurrencyCode11(string currencycode)
        {
            var currcode = _db.MNT_CurrencyM.ToList();

            var isDuplicate = false;

            foreach (var prod in currcode)
            {
                if (prod.CurrencyCode == currencycode)
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCurrCode(string CurrencyCode, string curr)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_CurrencyM where t.CurrencyCode == CurrencyCode orderby t.CurrencyCode descending select t.CurrencyCode).Take(1)).FirstOrDefault();
            if (num != null && num.ToLower() != curr.ToLower())
            {
                result = true;
            }
            return Json(result);
        }




        [HttpPost]
        public JsonResult FindCurrencyCode(string prefixText)
        {
            var suggestedCCode = from x in _db.MNT_CurrencyM
                                 where x.CurrencyCode.StartsWith(prefixText)
                                 select new
                                 {
                                     id = x.TranId,
                                     CurrencyCode = x.CurrencyCode,
                                     value = x.CurrencyCode
                                 };
            var result = Json(suggestedCCode.Take(10).ToList());
            return result;


        }

        [HttpPost]
        public JsonResult FindCurrencyDisplayCode(string prefixText)
        {
            var suggestedCDisplay = from x in _db.MNT_CurrencyM
                                    where x.CurrencyDispCode.StartsWith(prefixText)
                                    select new
                                    {
                                        id = x.TranId,
                                        value = x.CurrencyDispCode
                                    };
            var result = Json(suggestedCDisplay.Take(10).ToList());
            return result;


        }

        [HttpPost]
        public JsonResult FindCurrencyDesc(string prefixText)
        {
            var suggestedCDesc = from x in _db.MNT_CurrencyM
                                 where x.Description.StartsWith(prefixText)
                                 select new
                                 {
                                     id = x.TranId,
                                     value = x.Description
                                 };
            var result = Json(suggestedCDesc.Take(10).ToList());
            return result;


        }





        #endregion

        #region Currency Exchange master
        [HttpGet]
        public ActionResult ExchangeIndex(string sortOrder)
        {

            List<CurrencyExchangeModel> list = new List<CurrencyExchangeModel>();
            try
            {
                list = CurrencyExchangeModel.Fetch();

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Currency master " + list + " for Exchange master." + list + ".");
                addInfo.Add("entity_type", "Exchange master");
                PublishException(ex, addInfo, 0, "Exchange master" + list);
                return View(list);

            }


            return View(list);
        }


        [HttpPost]
        public ActionResult ExchangeIndex()
        {
            string CurrencyCode = Request.Form["inputCurrencyCode"].Trim();
            ViewBag.currency = CurrencyCode;
            List<CurrencyExchangeModel> list = new List<CurrencyExchangeModel>();
            try
            {
                list = GetCurrencyCodeResult(CurrencyCode).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Exchange master " + CurrencyCode + " for Search result Exchange." + CurrencyCode + ".");
                addInfo.Add("entity_type", "Exchange Master");
                PublishException(ex, addInfo, 0, "Exchange Master" + CurrencyCode);
            }
            return View(list);

        }


        public IQueryable<CurrencyExchangeModel> GetCurrencyCodeResult(string CurrencyCode)
        {
            var searchResult = (from curency in _db.MNT_CurrencyTxn
                                join CName in _db.MNT_CurrencyM on curency.CurrencyCode equals CName.CurrencyCode
                                where
                                  curency.CurrencyCode.Contains(CurrencyCode)

                                select new CurrencyExchangeModel
                                {
                                    CurrencyCode = curency.CurrencyCode,
                                    CurrencyName = CName.Description,
                                    Exchangerate = curency.ExchangeRate,
                                    EffectiveDate = curency.EffDate,
                                    ExpiryDate = curency.ExpDate,
                                    Id = curency.Id_CurrencyTrans
                                }).AsQueryable();

            return searchResult;
        }


        public ActionResult ExchangeEditor(int? Id)
        {
            var Exec = new CurrencyExchangeModel();
            CurrencyExchangeModel objmodel = new CurrencyExchangeModel();
            try
            {
                if (Id.HasValue)
                {
                    var current = (from x in _db.MNT_CurrencyTxn where x.Id_CurrencyTrans == Id select x).FirstOrDefault();
                    objmodel.Id = current.Id_CurrencyTrans;
                    objmodel.CurrencyCode = current.CurrencyCode;
                    objmodel.currencylist = LoadCurrency();
                    objmodel.EffectiveDate = current.EffDate;
                    objmodel.Exchangerate = Convert.ToDecimal(current.ExchangeRate);
                    objmodel.CreatedBy = current.CreatedBy == null ? " " : current.CreatedBy;
                    if (current.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)current.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = current.ModifiedBy == null ? " " : current.ModifiedBy;
                    objmodel.ModifiedOn = current.ModifiedDate;
                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Exchange Master " + objmodel.CurrencyCode + " for Exchange." + objmodel.CurrencyCode + ".");
                addInfo.Add("entity_type", "insurer Master");
                PublishException(ex, addInfo, 0, "Exchange Master" + objmodel.CurrencyCode);
                return View(objmodel);

            }
            Exec.currencylist = LoadCurrency();
            return View(Exec);
        }

        [HttpPost]
        public ActionResult ExchangeEditor(CurrencyExchangeModel model)
        {
            TempData["notice"] = "";
            try
            {
                model.currencylist = LoadCurrency();
                DateTime today = DateTime.Now.Date;

                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }

                if (model.EffectiveDate != today.Date)
                {
                    //TempData["notice"] = "Select today date only.";
                    var hits = (from x in _db.MNT_CurrencyTxn where x.Id_CurrencyTrans == model.Id select x).FirstOrDefault();
                    TempData["notice"] = MCAS.Web.Objects.Resources.ClaimMasters.ExchangeEditor.MsgSelecttodaydateonly;
                    model.CreatedBy = hits.CreatedBy;
                    model.CreatedOn = Convert.ToDateTime(hits.CreatedDate);
                    model.ModifiedOn = hits.ModifiedDate;
                    model.ModifiedBy = hits.ModifiedBy;
                    return View(model);
                }

                if (ModelState.IsValid)
                {

                    // var q2 = _db.MNT_CurrencyTxn.Where((t => t.EffDate.Date == DateTime.Now.Date && t.CurrencyCode == model.CurrencyCode));
                    var hits = (from x in _db.MNT_CurrencyTxn where x.Id_CurrencyTrans == model.Id select x).FirstOrDefault();
                    if (hits == null)
                    {

                        //  var q2 = _db.MNT_CurrencyTxn.Where((t => DbFunctions.TruncateTime.TruncateTime(t.EffDate.Date) ==  && t.CurrencyCode == model.CurrencyCode));
                        var q2 = (from x in _db.MNT_CurrencyTxn where EntityFunctions.TruncateTime(x.EffDate) == EntityFunctions.TruncateTime(today.Date) && x.CurrencyCode == model.CurrencyCode select x).FirstOrDefault();
                        if (q2 == null)
                        {
                            model.CreatedBy = LoggedInUserName;
                            var currency = model.Update();
                            TempData["notice"] = Common.RecordsSavedSuccessfully;
                        }
                        else
                        {
                            //TempData["notice"] = "Cannot have two exchange rate modifications in the same day.";
                            TempData["notice"] = MCAS.Web.Objects.Resources.ClaimMasters.ExchangeEditor.MsgCannotAllowInSameDay;
                        }


                    }
                    else
                    {
                        //var q1 = _db.MNT_CurrencyTxn.Where((t => t.EffDate.Date == DateTime.Now.Date && t.Id_CurrencyTrans == model.Id));
                        var q1 = (from x in _db.MNT_CurrencyTxn where EntityFunctions.TruncateTime(x.EffDate) == EntityFunctions.TruncateTime(today.Date) && x.Id_CurrencyTrans == model.Id select x).FirstOrDefault();
                        if (q1 == null)
                        {
                            model.ModifiedBy = LoggedInUserName;
                            var currency = model.Update();
                            TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                        }
                        else
                        {
                            //TempData["notice"] = "Cannot have two exchange rate modifications in the same day.";
                            TempData["notice"] = MCAS.Web.Objects.Resources.ClaimMasters.ExchangeEditor.MsgCannotAllowInSameDay;
                            model.CreatedBy = hits.CreatedBy;
                            model.CreatedOn = Convert.ToDateTime(hits.CreatedDate);
                            model.ModifiedOn = hits.ModifiedDate;
                            model.ModifiedBy = hits.ModifiedBy;
                        }
                    }

                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving Exchange  " + model.CurrencyCode + " for Exchange." + model.CurrencyCode + ".");
                addInfo.Add("entity_type", "Exchange Master");
                PublishException(ex, addInfo, 0, "Exchange Master" + model.CurrencyCode);
                return View(model);
            }
            return View(model);
        }


        #endregion

        #region GST Setting
        public ActionResult GSTIndex()
        {
            List<GSTModel> list = new List<GSTModel>();
            try
            {
                list = GSTModel.Fetch();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while loading GST " + list + " for GST." + list + ".");
                addInfo.Add("entity_type", "GST");
                PublishException(ex, addInfo, 0, "GST" + list);
                return View(list);
            }

            return View(list);
        }

        [HttpPost]
        public ActionResult GSTIndex(string GstType, string GstDesc)
        {
            GstType = ((Request.Form["ddlGst"] == null) ? "" : Request.Form["ddlGst"]);
            ViewBag.gsttype = GstType;
            List<GSTModel> list = new List<GSTModel>();
            try
            {
                list = GetGstSearchResult(GstType).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while searching GST Rate" + GstType + " for GST." + GstType + ".");
                addInfo.Add("entity_type", "GST Rate");
                PublishException(ex, addInfo, 0, "GST Rate" + list);
                return View(list);
            }
            return View(list);

        }


        public IQueryable<GSTModel> GetGstSearchResult(string GstType)
        {
            var searchResult = (from gst in _db.MNT_GST
                                where
                                gst.GSTType.Contains(GstType)

                                select gst).ToList().Select(item => new GSTModel
                                {
                                    GSTType = item.GSTType,
                                    GSTDescrpition = item.GSTDesc,
                                    EffectiveDateFrom = item.EffDateFrom,
                                    EffectiveDateTo = item.EffDateTo,
                                    Rate = item.Rate,
                                    Id = item.Id
                                }).AsQueryable();

            return searchResult;
        }

        [HttpGet]
        public ActionResult GSTEditor(int? Id)
        {
            var gst = new GSTModel();
            GSTModel objmodel = new GSTModel();
            try
            {
                if (Id.HasValue)
                {
                    var current = (from x in _db.MNT_GST where x.Id == Id select x).FirstOrDefault();

                    objmodel.GSTType = current.GSTType;
                    objmodel.GSTDescrpition = current.GSTDesc;
                    objmodel.EffectiveDateFrom = Convert.ToDateTime(current.EffDateFrom.ToShortDateString());
                    objmodel.EffectiveDateTo = Convert.ToDateTime(current.EffDateTo.ToShortDateString());
                    objmodel.Rate = current.Rate;

                    objmodel.CreatedBy = current.CreatedBy == null ? " " : current.CreatedBy;
                    if (current.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)current.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = current.ModifiedBy == null ? " " : current.ModifiedBy;
                    objmodel.ModifiedOn = current.ModifiedDate;

                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving GST Type " + objmodel.GSTCode + " for GST No." + objmodel.GSTDescrpition + ".");
                addInfo.Add("entity_type", "GSTModel");
                PublishException(ex, addInfo, 0, "Vehicle Model" + objmodel.Id);
                return View(objmodel);
            }
            return View(gst);
        }

        [HttpPost]
        public ActionResult GSTEditor(GSTModel model)
        {
            TempData["notice"] = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_GST where x.Id == model.Id select x).FirstOrDefault();
                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var csd = model.Update();
                        //TempData["notice"] = "Record saved successfully.";
                        TempData["notice"] = Common.RecordsSavedSuccessfully;
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var csd = model.Update();
                        //TempData["notice"] = "Record updated successfully.";
                        TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                    }
                }
                else
                {
                    return View(model);
                }




            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving GST " + model.Id + " for GST." + model.GSTType + ".");
                addInfo.Add("entity_type", "GSTModel");
                PublishException(ex, addInfo, 0, "GST Model" + model.GSTCode);
                return View(model);
            }
            return View(model);
        }



        public JsonResult FillGstList()
        {
            var returnData = LoadGst();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Preview Documents
        public ActionResult PreviewDocumentIndex(string ClaimID, string AccidentClaimId, string ScreenId, string SId, string Mode, string uid)
        {
            List<PreViewDocumentModel> resultlist =
                PreViewDocumentModel.FetchResultList(
                PreViewDocumentModel.Fetch(ClaimID, AccidentClaimId, ScreenId), 
                GenerateDoumet.FetchServiceProviderList(ClaimID, AccidentClaimId),
                ClaimID, 
                AccidentClaimId,
                ScreenId, SId);
            return View(resultlist);
        }

        
        [HttpGet]
        public ActionResult GetFileName(int Id)
        {

            var models = (from m in _db.MNT_TEMPLATE_MASTER where m.Template_Id == Id select m.Filename).FirstOrDefault();
            string str = models.ToString();
            return Content(str);

        }





        [HttpGet]
        public ActionResult ShowPdf(string documentid)
        {
            int docid = Convert.ToInt32(documentid);
            string results = (from f in _db.MNT_TEMPLATE_MASTER
                              where f.Template_Id == docid
                              select f.Filename).FirstOrDefault();
            //  path = results.Filename;
            string contentType = "application/pdf";
            return File(results, contentType, "sanjay.pdf");
            ////declare byte array to get file content from database and string to store file name
            ////byte[] fileData;
            //string fileData;
            //string fileNames;


            //var record = from p in _db.MNT_TEMPLATE_MASTER
            //             where p.Template_Id == documentid
            //             select p;
            ////only one record will be returned from database as expression uses condtion on primary field
            ////so get first record from returned values and retrive file content (binary) and filename 
            //fileData = record.First().Filename;
            //fileNames = record.First().Display_Name;
            ////return file and provide byte file content and file name
            //return File(fileData, "application/pdf", Server.HtmlEncode(fileData));
        }


        [HttpGet]
        public FileResult PreviewDocument(PreViewDocumentModel model)
        {
            //string path = @"D:\Projects\Singapore-Dev\MCAS\MCAS\Uploads\Templates\C Stage\Discharge voucher\Authority_to_Recover_PI_Payout.pdf";
            //return File(path, "application/pdf",Server.HtmlEncode(path));      
            string pdfname = "";
            string path = "";
            string templatecode = "";

            var results = (from m in _db.MNT_TEMPLATE_MASTER where m.Template_Id == model.DocumentId select new { Tempaltepath = m.Template_Path, Filename = m.Filename, Tempaltecode = m.Template_Code }).FirstOrDefault();

            pdfname = results.Filename;
            path = results.Tempaltepath;
            templatecode = results.Tempaltecode;
            path = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + path);
            string str = path + "/" + pdfname; //Server.MapPath(path + "/" + pdfname);
            return File(str, "application/pdf", pdfname);

            //  return File(fileName, "application/pdf");


            //string path = @"D:\Projects\Singapore-Dev\MCAS\MCAS\Uploads\Templates\C Stage\Discharge voucher\Authority_to_Recover_PI_Payout.pdf";
            //return File(path, "application/pdf", Server.HtmlEncode(path));


        }

        public class FileStringResult : FileResult
        {
            public string Data { get; set; }

            public FileStringResult(string data, string contentType)
                : base(contentType)
            {
                Data = data;
            }

            protected override void WriteFile(HttpResponseBase response)
            {
                if (Data == null) { return; }
                response.Write(Data);
            }
        }






        [HttpGet]
        public FileResult GenerateDocument(PreViewDocumentModel model)
        {
            // MCASEntities obj = new MCASEntities();

            string MapXmlName = "";
            string XmlPath = "";
            string templatecode = "";
            string str = "";
            string pdfname = "";
            string path = "";
            string pdftemplatepath = "";
            string pdfoutputpath = "";
            string pdfoutputfilename = "";
            string claimid = "";
            DataSet ds;
            String strReturnXML = "";
            string UrlPath = "";
            string myFilePath = "";
            string sdf = "";
            string sty = "";
            string accidentclaimid = "";
            string Domain = "";
            string username = "";
            string passwd = "";
            string reportString = "";

            try
            {

                //  model.DocumentId = 3;
                accidentclaimid = Request.QueryString["AccidentClaimId"].ToString();
                int accd = Convert.ToInt16(accidentclaimid);

                var DocId = _db.PRINT_JOBS.Where(x => x.ENTITY_ID == model.DocumentId && x.CLAIM_ID == accd).FirstOrDefault();
                if (DocId == null)
                {
                    obj.AddParameter("@ACCIDENTCLAIM_ID", accidentclaimid);
                    ds = obj.ExecuteDataSet("Proc_GetClaimDataXML", CommandType.StoredProcedure);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            strReturnXML = dr[0].ToString();

                        }
                    }

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(strReturnXML);
                    XmlNodeList xnList = doc.SelectNodes("/INPUTXML/Claims");
                    foreach (XmlNode xn in xnList)
                    {
                        claimid = xn["ClaimID"].InnerText;
                        //  string lastName = xn["AccidentClaimId"].InnerText;
                    }
                    Domain = ConfigurationManager.AppSettings["IDomain"];
                    username = ConfigurationManager.AppSettings["IUserName"];
                    passwd = ConfigurationManager.AppSettings["IPassWd"];

                    EAWXmlToPDFParser.XmlToPDFParser ObjXmlParser = new EAWXmlToPDFParser.XmlToPDFParser();
                    ObjXmlParser.IDomain = Domain;
                    ObjXmlParser.IUserName = username;
                    ObjXmlParser.IPassWd = passwd;

                    var results = (from m in _db.MNT_TEMPLATE_MASTER where m.Template_Id == model.DocumentId select new { Tempaltepath = m.Template_Path, Filename = m.Filename, MapxmlPath = m.MappingXML_Path, MapXmlFileName = m.MappingXML_FileName, Tempaltecode = m.Template_Code }).FirstOrDefault();
                    ModelState.Clear();
                    pdfname = results.Filename;
                    path = results.Tempaltepath;

                    MapXmlName = results.MapXmlFileName;
                    XmlPath = results.MapxmlPath;
                    templatecode = results.Tempaltecode;

                    XmlPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + XmlPath);
                    str = XmlPath + "/" + MapXmlName; //Server.MapPath(path + "/" + pdfname);

                    doc.Load(str);
                    ObjXmlParser.PdfMapXml = doc.InnerXml;
                    ObjXmlParser.InputXml = strReturnXML;
                    //ObjXmlParser.PdfTemplatePath = XmlPath;
                    myFilePath = ConfigurationManager.AppSettings["OutPutFilePath"];
                    //   pdftemplatepath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + myFilePath);
                    pdftemplatepath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + path);
                    ObjXmlParser.PdfTemplatePath = pdftemplatepath;

                    //ObjXmlParser.PdfTemplatePath = @"D:\Sanjay\"; 
                    //ObjXmlParser.PdfOutPutPath = @"D:\Sanjay\pdf";
                    //  ObjXmlParser.PdfOutPutFileName = "Final-DV_Interim_(via_email).pdf";

                    pdfoutputpath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + myFilePath + "/" + claimid + "/" + templatecode + "/");
                    ObjXmlParser.PdfOutPutPath = pdfoutputpath;

                    pdfoutputfilename = (Path.GetFileNameWithoutExtension(pdfname) + "_" + claimid + "_" + templatecode + "_" + DateTime.Now.Ticks) + ".pdf";
                    ObjXmlParser.PdfOutPutFileName = pdfoutputfilename;

                    //   sdf = pdfoutputpath + pdfoutputfilename;

                    string fileName = "";
                    try
                    {
                        fileName = ObjXmlParser.GeneratePdf();
                        TempData["result"] = "File : " + fileName + " has been generated.";
                        model.Claimid = Convert.ToInt16(claimid);
                        model.Templatecode = templatecode;
                        model.DocumentName = fileName;

                        var pdf = model.Update();
                        sdf = pdfoutputpath + fileName;

                        //XmlPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + XmlPath);
                        //str = XmlPath + "/" + MapXmlName; //Server.MapPath(path + "/" + pdfname);
                        return new FilePathResult(sdf, "application/pdf");

                        //  return File("Sanjay", "application/pdf", fileName);



                    }
                    catch (System.Data.DataException)
                    {
                        ModelState.AddModelError("", "Unable to open Files.");
                        reportString = " Unable to open Files.";
                        return new FilePathResult(reportString, "application/text");
                    }




                }
                else
                {
                    //  string sty = @"D:\Projects\Singapore-Dev\MCAS\MCAS\Uploads\OutputPDFs\116\C\DV_(Individuals)_116_C_635425018103254708.pdf";
                    var pdf = (from m in _db.PRINT_JOBS where m.ENTITY_ID == model.DocumentId && m.CLAIM_ID == accd select new { Filename = m.FILE_NAME, UrlPath = m.URL_PATH }).FirstOrDefault();
                    pdfname = pdf.Filename;
                    UrlPath = pdf.UrlPath;
                    UrlPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "\\" + UrlPath);
                    sty = UrlPath + "/" + pdfname;
                    return new FilePathResult(sty, "application/pdf");
                    //  return File("sanjay", "application/pdf", pdfname);


                }

                //D:\Projects\Singapore-Dev\MCAS\MCAS\Uploads\OutputPDFs\AccidentClaimID\



                // D:\Projects\Singapore-Dev\MCAS\MCAS\Uploads\OutputPDFs\



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error:" + ex.Message);
                sdf = "Error: " + ex.Message + ": " + ex.StackTrace;
                return new FileStringResult(sdf, "application/text");

            }
            //return new FilePathResult(sdf, "application/pdf");

        }




        #endregion

        #region Print Document
        public ActionResult ClaimDocumentsPrintedIndex()
        {
            //List<ClaimDocumentPrintedModel> clms = new List<ClaimDocumentPrintedModel>();
            //clms = ClaimDocumentPrintedModel.Fetch();
            //TempData["claimList"] = clms;
            //return View(clms);
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            return View(new SearchCriteria() { claimMode = caller });
        }

        [HttpGet]
        public ActionResult GenerateDocumentIndex(int? EntityId)
        {
            // EntityId = 8;
            var pdf = (from m in _db.PRINT_JOBS where m.ENTITY_ID == EntityId select new { Filename = m.FILE_NAME, UrlPath = m.URL_PATH }).FirstOrDefault();
            string pdfname = pdf.Filename;
            string UrlPath = pdf.UrlPath;
            UrlPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "\\" + UrlPath);
            string sty = UrlPath + "\\" + pdfname;
            if (!System.IO.File.Exists(sty))
            {
                TempData["FileExists"] = true;
                TempData["Filename"] = pdf.Filename;
                return View("ClaimDocumentsPrintedIndex");
            }
            else
            {
                return new FilePathResult(sty, "application/pdf");
            }

        }

        [HttpPost]
        public JsonResult GetClaimPrintedDocument(SearchCriteria criteria)
        {
            string caller = criteria.claimMode;
            List<ClaimDocumentPrintedModel> list = new List<ClaimDocumentPrintedModel>();

            if (criteria.ClaimNo != null || criteria.vehicleNo != null)
            {
                list = GetPrintedDocumentSearchResult(criteria.ClaimNo, criteria.vehicleNo).ToList();
            }
            else
            {
                list = ClaimDocumentPrintedModel.Fetch();
            }
            TempData["claimList"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public ActionResult ClaimDocumentsPrintedIndex(string ClaimNo, string VehicleNo) {
        //    ClaimNo =Request.Form["inputClaimNo"].Trim();
        //    ViewBag.claimno = ClaimNo;
        //    VehicleNo = Request.Form["inputVehicleNo"].Trim();
        //    ViewBag.vehicleno = VehicleNo;

        //    if (ClaimNo!="" || VehicleNo!="")
        //    {
        //        List<ClaimDocumentPrintedModel> list = new List<ClaimDocumentPrintedModel>();
        //        list = GetPrintedDocumentSearchResult(ClaimNo, VehicleNo).ToList();
        //        TempData["claimList"] = list;
        //        return View(list);

        //    }
        //    else
        //    {
        //        List<ClaimDocumentPrintedModel> clms = new List<ClaimDocumentPrintedModel>();
        //        clms = ClaimDocumentPrintedModel.Fetch();
        //        TempData["claimList"] = clms;
        //        return View(clms);
        //    }


        //}

        //public IQueryable<ClaimDocumentPrintedModel> GetPrintedDocumentSearchResult(int ClaimNo, string VehicleNo)
        //{
        //    var searchResult = (from x in _db.PRINT_JOBS
        //                        where
        //                          x.CLAIM_ID.Value.Equals(ClaimNo)
        //                        select x).ToList().Select(item => new ClaimDocumentPrintedModel
        //                        {
        //                            DocumentName =Path.GetFileNameWithoutExtension(item.FILE_NAME),
        //                         //   DocumentName = System.IO.Path.GetFileName(item.FILE_NAME),
        //                            DateofGeneration =Convert.ToDateTime(item.CREATED_DATETIME),
        //                            TimeofGeneration =Convert.ToDateTime(item.CREATED_DATETIME),
        //                            UserId = Convert.ToString(item.GENERATED_FROM),
        //                            DocumentID = item.ENTITY_ID,
        //                            ClaimNo=Convert.ToString(item.CLAIM_ID),
        //                            ViewDocumentLink=item.FILE_NAME
        //                        }).AsQueryable();

        //    return searchResult;
        //}


        public IQueryable<ClaimDocumentPrintedModel> GetPrintedDocumentSearchResult(string claimNo, string vehicleNo)
        {

            //var searchResult = (from p in _db.PRINT_JOBS
            //                    join clm in _db.CLM_Claims on p.CLAIM_ID equals  clm.ClaimID 
            //                    join ca in _db.ClaimAccidentDetails on clm.AccidentClaimId equals ca.AccidentClaimId 
            //                    where
            //                      ca.ClaimNo.Contains(claimNo) &&
            //                      ca.VehicleNo.Contains(vehicleNo)
            //                    select new ClaimDocumentPrintedModel
            //                    {
            //                        DocumentName = p.FILE_NAME,
            //                        DateofGeneration = p.CREATED_DATETIME,
            //                        TimeofGeneration = p.CREATED_DATETIME,
            //                        UserId = p.GENERATED_FROM,
            //                        DocumentID = p.ENTITY_ID,
            //                        Claimid = p.CLAIM_ID,
            //                        ViewDocumentLink = p.FILE_NAME,
            //                        ClaimNo = ca.ClaimNo
            //                    }).AsQueryable();

            var searchResult = (from p in _db.PRINT_JOBS
                                join clm in _db.CLM_Claims on p.CLAIM_ID equals clm.ClaimID
                                join ca in _db.ClaimAccidentDetails on clm.AccidentClaimId equals ca.AccidentClaimId
                                where
                                  (ca.ClaimNo != null && (ca.ClaimNo).Contains(claimNo)) || (ca.VehicleNo != null && (ca.VehicleNo).Contains(vehicleNo))
                                select new ClaimDocumentPrintedModel
                                {
                                    DocumentName = p.FILE_NAME,
                                    DateofGeneration = p.CREATED_DATETIME,
                                    TimeofGeneration = p.CREATED_DATETIME,
                                    UserId = p.GENERATED_FROM,
                                    DocumentID = p.ENTITY_ID,
                                    Claimid = p.CLAIM_ID,
                                    ViewDocumentLink = p.FILE_NAME,
                                    ClaimNo = ca.ClaimNo
                                }).AsQueryable();


            return searchResult;
        }

        private void addGridColumns(GridView gv, string strColumnNames, string strColumnHeader)
        {
            String[] colNames = strColumnNames.Split(',');
            String[] colHeads = strColumnHeader.Split(',');
            BoundField field;
            for (int ctr = 0; ctr < colNames.Length; ctr++)
            {
                field = new BoundField();
                field.DataField = colNames[ctr];
                field.HeaderText = colHeads[ctr] != null ? colHeads[ctr] : "";
                gv.Columns.Add(field);
            }

        }

        public ActionResult ClaimDocPrintedDownLoader()
        {
            StringWriter sw = new StringWriter();
            List<ClaimDocumentPrintedModel> list = (List<ClaimDocumentPrintedModel>)TempData["claimList"];
            string format = "Excel";
            format = Request.QueryString["Fileformat"] != null ? Request.QueryString["Fileformat"].ToString() : format;
            string fileName = "ClaimDocPrintedList.xls";
            if (format.ToUpper().Equals("EXCEL"))
            {
                GridView gv = new GridView();
                gv.DataSource = list;
                gv.AutoGenerateColumns = false;
                string columnNames = "DocumentName,DateofGeneration,TimeofGeneration,UserId,ClaimNo";
                string columnHeaders = "Document Name,Date of Generation,Time,User Id,Claim Id";
                addGridColumns(gv, columnNames, columnHeaders);
                gv.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                //StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            else if (format.ToUpper().Equals("CSV"))
            {
                fileName = "ClaimDocPrintedList.csv";
                //First line for column names
                sw.WriteLine("\"Document Name\",\"Date of Generation\",\"Time\",\"User Id\",\"Claim Id\"");

                foreach (ClaimDocumentPrintedModel item in list)
                {
                    sw.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                                               item.DocumentName,
                                               item.DateofGeneration,
                                               item.TimeofGeneration,
                                               item.UserId,
                                               item.ClaimNo));
                }
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.ContentType = "text/csv";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.Write(sw);
                Response.End();
            }
            TempData["claimList"] = list;
            return View(list);
        }
        #endregion

        #region Hospital Information

        [HttpGet]
        public ActionResult HospitalIndex()
        {
            List<HospitalModel> list = new List<HospitalModel>();
            try
            {
                list = HospitalModel.Fetch();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Hospital master " + list + " for Hospital." + list + ".");
                addInfo.Add("entity_type", "Hospital Master");
                PublishException(ex, addInfo, 0, "Hospital Master" + list);
                View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult HospitalIndex(string HospitalName)
        {
            List<HospitalModel> listofHospitals = new List<HospitalModel>();
            HospitalName = Request.Form["inputHospitalName"].Trim();
            ViewBag.hospitalname = HospitalName;
            try
            {
                if (HospitalName != "")
                {
                    List<HospitalModel> list = new List<HospitalModel>();
                    list = GetHospitalSearchResult(HospitalName).ToList();
                    return View(list);
                }
                else
                {

                    listofHospitals = HospitalModel.Fetch();
                    return View(listofHospitals);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Hospital master " + HospitalName + " for Search result hospital." + HospitalName + ".");
                addInfo.Add("entity_type", "Hospital Master");
                PublishException(ex, addInfo, 0, "Hospital Master" + HospitalName);
            }
            return View(listofHospitals);


        }

        public IQueryable<HospitalModel> GetHospitalSearchResult(string HospitalName)
        {
            var searchResult = (from hospital in _db.MNT_Hospital
                                orderby hospital.HospitalName
                                where
                                hospital.HospitalName.Contains(HospitalName)
                                select hospital).ToList().Select(item => new HospitalModel
                                {
                                    HospitalName = item.HospitalName,
                                    HospitalAddress = item.HospitalAddress,
                                    HospitalContactNo = item.HospitalContactNo,
                                    Email = item.Email,
                                    ContactPersonName = item.ContactPersonName,
                                    Id = item.Id
                                }).AsQueryable();

            return searchResult;
        }

        [HttpGet]
        public ActionResult HospitalEditor(int? Id)
        {

            var hospitalmodel = new HospitalModel();
            HospitalModel objmodel = new HospitalModel();
            if (Id.HasValue)
            {
                var Hospiatllist = (from x in _db.MNT_Hospital where x.Id == Id select x).FirstOrDefault();

                objmodel.HospitalAddres = new AddressModel()
                {
                    InsurerName = Hospiatllist.HospitalName,
                    Address1 = Hospiatllist.HospitalAddress,
                    Address2 = Hospiatllist.HospitalAddress2,
                    Address3 = Hospiatllist.HospitalAddress3,
                    OffNo1 = Hospiatllist.officeNo,
                    MobileNo1 = Hospiatllist.MobileNo1,
                    EmailAddress1 = Hospiatllist.Email,
                    Fax1 = Hospiatllist.HospitalFaxNo,
                    FirstContactPersonName = Hospiatllist.FirstContactPersonName,
                    PostalCode = Hospiatllist.PostalCode,
                    City = Hospiatllist.City,
                    Country = Hospiatllist.Country,
                    SecondContactPersonName = Hospiatllist.SecondContactPersonName,
                    EmailAddress2 = Hospiatllist.EmailAddress2,
                    OffNo2 = Hospiatllist.OffNo2,
                    MobileNo2 = Hospiatllist.MobileNo2,
                    Fax2 = Hospiatllist.Fax2,
                    InsurerType = Hospiatllist.HospitalType,
                    EffectiveFromDate = Hospiatllist.EffectiveFrom,
                    Effectiveto = Hospiatllist.EffectiveTo,
                    Status = Hospiatllist.Status,
                    Remarks = Hospiatllist.Remarks,
                    State = Hospiatllist.State
                };

                objmodel.HospitalAddres.Status = Hospiatllist.Status == "1" ? "Active" : "InActive";
                objmodel.HospitalAddres.usercountrylist = LoadCountry();
                objmodel.HospitalName = Hospiatllist.HospitalName;
                objmodel.HospitalAddres.Insurerlist = LoadLookUpValue("InsurerType");
                objmodel.HospitalAddres.Statuslist = LoadLookUpValue("STATUS");
                objmodel.HospitalAddress = Hospiatllist.HospitalAddress;
                objmodel.HospitalContactNo = Hospiatllist.HospitalContactNo;
                objmodel.ContactPersonName = Hospiatllist.ContactPersonName;
                objmodel.HospitalFaxNo = Hospiatllist.HospitalFaxNo;
                objmodel.Email = Hospiatllist.Email;
                objmodel.OfficeNo = Hospiatllist.officeNo;
                objmodel.faxNo = Hospiatllist.FaxNo;
                objmodel.Id = Id;

                objmodel.CreatedBy = Hospiatllist.CreatedBy == null ? " " : Hospiatllist.CreatedBy;
                if (Hospiatllist.CreatedDate != null)
                    objmodel.CreatedOn = (DateTime)Hospiatllist.CreatedDate;
                else
                    objmodel.CreatedOn = DateTime.MinValue;
                objmodel.ModifiedBy = Hospiatllist.ModifiedBy == null ? " " : Hospiatllist.ModifiedBy;
                objmodel.ModifiedOn = Hospiatllist.ModifiedDate;

                return View(objmodel);
            }
            hospitalmodel.Id = Id;
            hospitalmodel.HospitalAddres.Statuslist = LoadLookUpValue("STATUS");
            hospitalmodel.HospitalAddres.Insurerlist = LoadLookUpValue("InsurerType");
            hospitalmodel.HospitalAddres.usercountrylist = LoadCountry();
            return View(hospitalmodel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult HospitalEditor(HospitalModel model, AddressModel addModel)
        {
            addModel.Insurerlist = LoadLookUpValue("InsurerType");
            addModel.Statuslist = LoadLookUpValue("STATUS");
            //   addModel.citylist = LoadCity();
            addModel.usercountrylist = LoadCountry();
            model.HospitalAddres = addModel;
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }

                if (ModelState.IsValid)
                {
                    TempData["notice"] = "";
                    TempData["display"] = "";
                    ModelState.Clear();
                    var hospital = (from lt in _db.MNT_Hospital where lt.Id == model.Id select lt).FirstOrDefault();

                    if (hospital == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var hosp = model.Update();
                        TempData["notice"] = "Record Saved Successfully.";
                        TempData["display"] = hosp.Id;

                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var hosp = model.Update();
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
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving hospital name " + model.HospitalName + " for hospital." + model.HospitalName + ".");
                addInfo.Add("entity_type", "hospital Master");
                PublishException(ex, addInfo, 0, "hospital Master" + model.Id);
                return View(model);
            }
            return View(model);
        }

        public JsonResult CheckHospitalName(string HospitalName, string HosName)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Hospital where t.HospitalName == HospitalName orderby t.HospitalName descending select t.HospitalName).Take(1)).FirstOrDefault();
            if ((num != null) && (HosName == null || HosName == ""))
            {
                result = true;
            }
            else if ((num != null && num.ToLower() != HosName.ToLower()))
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



        #endregion
    }
}
