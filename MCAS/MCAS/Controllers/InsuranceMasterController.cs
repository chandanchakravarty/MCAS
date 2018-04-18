using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using MCAS.Entity;
using System.Collections.Specialized;
using MCAS.Web.Objects.Resources.Common;


namespace MCAS.Controllers
{
    public class InsuranceMasterController : BaseController
    {
        //
        // GET: /InsuranceMaster/
        MCASEntities _db = new MCASEntities();

        #region insurance

        #endregion

        [HttpGet]
        public ActionResult InsurancePolicyMasterIndex()
        {
            List<InsuranceModel> list = new List<InsuranceModel>();
            try
            {
                list = InsuranceModel.FetchPolicy();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while loading Insurance Policy " + list + " for insurance Policy." + list + ".");
                addInfo.Add("entity_type", "Insurance Policy");
                PublishException(ex, addInfo, 0, "Insurance Policy" + list);
                return View(list);

            }
            return View(list);
        }

        [HttpPost]
        public ActionResult InsurancePolicyMasterIndex(string Insurer, string policyno, DateTime? EffectiveFrom, DateTime? EffectiveTo)
        {
            Insurer = ((Request.Form["inputInsurer"] == null) ? "" : Request.Form["inputInsurer"]);
            ViewBag.insurer = Insurer;
            //int ins = Convert.ToInt32(Insurer);
            policyno = Request.Form["inputPolicyNo"];
            ViewBag.policy = policyno;

            if (Request.Form["inputPolicyEffdate"] == null || Request.Form["inputPolicyEffdate"] == "")
                EffectiveFrom = null;
            else
                EffectiveFrom = Convert.ToDateTime(Request.Form["inputPolicyEffdate"]);

            String EffectiveFrom_new = String.Format("{0:dd/MM/yyyy}", EffectiveFrom);
            ViewBag.effectiveF = EffectiveFrom_new;
            if (Request.Form["inputPolicyTodate"] == null || Request.Form["inputPolicyTodate"] == "")
                EffectiveTo = null;
            else
                EffectiveTo = Convert.ToDateTime(Request.Form["inputPolicyTodate"]);
            String EffectiveTo_new = String.Format("{0:dd/MM/yyyy}", EffectiveTo);
            ViewBag.effectiveTo = EffectiveTo_new;


            List<InsuranceModel> list = new List<InsuranceModel>();
            try
            {
                list = GetPolicyResult(Insurer, policyno, EffectiveFrom, EffectiveTo).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while searching Insurance Policy " + policyno + " for model No." + Insurer + ".");
                addInfo.Add("entity_type", "Insurance Policy");
                PublishException(ex, addInfo, 0, "Insurance Policy" + policyno);
                return View(list);
            }
            return View(list);

        }

        public IQueryable<InsuranceModel> GetPolicyResult(string Insurer, string policyno, DateTime? EffectiveFrom, DateTime? EffectiveTo)
        {

            //var insurername = Insurer != null ? Insurer : d.CedantName;
            var searchResult = (from u in _db.MNT_InsruanceM
                                join d in _db.MNT_Cedant on u.CedantId equals d.CedantId into gj
                                from cd in gj.DefaultIfEmpty()
                                where
                                  u.PolicyNo.Contains(policyno) &&
                                  cd.CedantName.Contains(Insurer) &&
                                  System.Data.Objects.SqlClient.SqlFunctions.DateDiff("d", u.PolicyEffectiveFrom, EffectiveFrom == null ? u.PolicyEffectiveFrom : EffectiveFrom) <= 0 && System.Data.Objects.SqlClient.SqlFunctions.DateDiff("d", u.PolicyEffectiveTo, EffectiveTo == null ? u.PolicyEffectiveTo : EffectiveTo) >= 0
                                select new InsuranceModel
                                 {
                                     PolicyNo = u.PolicyNo,
                                     PolicyEffectiveFrom = u.PolicyEffectiveFrom,
                                     PolicyEffectiveTo = u.PolicyEffectiveTo,
                                     Deduct = u.Deductible,
                                     Premium = u.PremiumAmount,
                                     CedantName = cd != null ? cd.CedantName : "",
                                     PolicyId = u.PolicyId
                                 }).AsQueryable();

            return searchResult;
        }

        public JsonResult FillInsurer()
        {
            var returnData = loadCedant();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetCurrencyExchange(string CurrencyCode)
        {

            var models = (from m in _db.MNT_CurrencyTxn where m.CurrencyCode == CurrencyCode select m.ExchangeRate).FirstOrDefault();
            string str = models.ToString();
            return Content(str);

        }

        public JsonResult ExpensesType(string CurrencyCode)
        {
            var result = LoadExpenses(CurrencyCode);
            return Json(result == "" ? "0.00" : Convert.ToString(Math.Round(decimal.Parse(result.ToString()), 2)));
        }

        [HttpGet]
        public ActionResult InsurancePolicyMasterEditor(int? PolicyId)
        {
            var model = new InsuranceModel();
            InsuranceModel objmodel = new InsuranceModel();
            try
            {
                if (PolicyId.HasValue)
                {
                    var policy = (from lt in _db.MNT_InsruanceM where lt.PolicyId == PolicyId select lt).FirstOrDefault();

                    objmodel.PolicyId = policy.PolicyId;
                    objmodel.PolicyNo = policy.PolicyNo;
                    objmodel.PolicyEffectiveFrom = Convert.ToDateTime(policy.PolicyEffectiveFrom);
                    objmodel.PolicyEffectiveTo = Convert.ToDateTime(policy.PolicyEffectiveTo);
                    objmodel.ProductList = loadClass();
                    objmodel.ProductId = policy.ProductId;
                    objmodel.Cedantlist = loadCedant();
                    objmodel.CedantId = policy.CedantId;
                    objmodel.SubClassId = Convert.ToInt16(policy.SubClassId);
                    objmodel.SubClassList = loadSubClass();
                    objmodel.CurrencyCode = policy.CurrencyCode;
                    objmodel.expenseslist = LoadExpenses(true);
                    objmodel.Deductible = policy.Deductible;
                    objmodel.PremiumAmount = policy.PremiumAmount;
                    objmodel.Exchangerate = policy.ExchangeRate;
                    objmodel.PremiumLocalCurrency = policy.PremiumLocalCurrency;

                    objmodel.CreatedBy = policy.CreatedBy == null ? " " : policy.CreatedBy;
                    if (policy.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)policy.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = policy.ModifiedBy == null ? " " : policy.ModifiedBy;
                    objmodel.ModifiedOn = policy.ModifiedDate;

                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving insurance policy " + model.PolicyNo + " for insurance policy." + model.PolicyId + ".");
                addInfo.Add("entity_type", "insurance policy");
                PublishException(ex, addInfo, 0, "insurance policy" + model.PolicyId);
                return View(model);
            }

            model.currencylist = LoadCurrency();
            model.expenseslist = LoadExpenses(true);
            model.ProductList = loadClass();
            model.SubClassList = loadSubClass();
            model.Cedantlist = loadCedant();
            model.Exchangerate = Convert.ToDecimal(string.IsNullOrEmpty(LoadExpenses("SGD")) ? "0.00" : LoadExpenses("SGD"));

            return View(model);


        }

        [HttpPost]
        public ActionResult InsurancePolicyMasterEditor(InsuranceModel model)
        {
            TempData["result"] = "";
            try
            {
                model.Cedantlist = loadCedant();
                model.ProductList = loadClass();
                model.SubClassList = loadSubClass();
                model.currencylist = LoadCurrency();
                model.expenseslist = LoadExpenses(true);
                model.PremiumLocalCurrency = model.PremiumAmount * model.Exchangerate;
                ModelState.Clear();
                var PolicyNo = (from lt in _db.MNT_InsruanceM where lt.PolicyId == model.PolicyId select lt).FirstOrDefault();
                if (PolicyNo == null)
                {
                    model.CreatedBy = LoggedInUserName;
                    var Policy = model.Update();
                    //TempData["result"] = "Records Saved Successfully.";
                    TempData["result"] = Common.RecordsSavedSuccessfully;

                }
                else
                {
                    model.ModifiedBy = LoggedInUserName;
                    var Policy = model.Update();
                    //TempData["result"] = "Records Updated Successfully.";
                    TempData["result"] = Common.RecordsUpdatedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving insurance Master " + model.PolicyId + " for policy." + model.PolicyNo + ".");
                addInfo.Add("entity_type", "insurance Master");
                PublishException(ex, addInfo, 0, "insurance Master" + model.PolicyNo);
                return View(model);
            }
            return View(model);
        }



        public JsonResult FillSubClass(string id)
        {
            MCASEntities _db = new MCASEntities();
            string[] prodCode = id.Split(',');
            JsonResult returnData = new JsonResult();
            foreach (var prod in prodCode)
            {
                var lists = GetSearchResult(prod).ToList();
                returnData.Data = lists;
            }


            return Json(returnData, JsonRequestBehavior.AllowGet);




        }





        public IQueryable<InsuranceModel> GetSearchResult(string prodcode)
        {
            var searchResult = (from survey in _db.MNT_ProductClass
                                where
                                  survey.ProdCode.Contains(prodcode)

                                select survey).ToList().Select(item => new InsuranceModel
                                {
                                    // ID=item.ID,
                                    ProductCode = item.ProdCode,
                                    ClassCode = item.ClassCode,
                                    ClassDescription = item.ClassDesc
                                }).AsQueryable();

            return searchResult;
        }

        public JsonResult FillMainClass()
        {
            JsonResult returnData = new JsonResult();
            returnData.Data = GetMainResult();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public IQueryable<InsuranceModel> GetMainResult()
        {
            var searchResult = (from survey in _db.MNT_Products

                                select survey).ToList().Select(item => new InsuranceModel
                                {
                                    //  ProductId = item.ProductId,
                                    //ProductCode = item.ProdCode,
                                    //ClassCode = item.ClassCode,
                                    ProductCode = item.ProductCode
                                }).AsQueryable();

            return searchResult;
        }


    }
}
