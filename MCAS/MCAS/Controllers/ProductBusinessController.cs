using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Entity;
using MCAS.Web.Objects.MastersHelper;
using System.Data.Entity;
using System.Data.EntityModel;
using MCAS.Web.Objects.CommonHelper;
using System.Collections.Specialized;
using MCAS.Web.Objects.Resources.Common;
namespace MCAS.Controllers
{
    public class ProductBusinessController : BaseController
    {
        //
        // GET: /ProductBusiness/
        MCASEntities _db = new MCASEntities();
        #region product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductBusinessIndex()
        {

            List<ProductBusinessModel> list = new List<ProductBusinessModel>();
            try
            {
                list = ProductBusinessModel.FetchList();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Product Class " + list + " for Product Class." + list + ".");
                addInfo.Add("entity_type", "Product Close");
                PublishException(ex, addInfo, 0, "Product Close" + list);
                return View(list);
            }
            return View(list);


        }

        public JsonResult FillProductDescription()
        {
            var returnData = LoadProductsDescription();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductBusinessIndex(string classcode)
        {
            classcode = ((Request.Form["ddlProduct"] == null) ? "" : Request.Form["ddlProduct"]);
            ViewBag.classcode = classcode;
            List<ProductBusinessModel> list = new List<ProductBusinessModel>();
            try
            {
                list = GetSearchResult(classcode).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for product Class " + classcode + " for Search result product Class." + classcode + ".");
                addInfo.Add("entity_type", "product Class");
                PublishException(ex, addInfo, 0, "product Class" + classcode);
                return View(list);
            }
            return View(list);
        }

        public IQueryable<ProductBusinessModel> GetSearchResult(string classcode)
        {
            var searchResult = (from groups in _db.MNT_Products
                                where
                                  groups.ProductCode.Contains(classcode)
                                select groups).ToList().Select(item => new ProductBusinessModel
                                {
                                    ProductCode = item.ProductCode,
                                    ProductDisplayName = item.ProductDisplayName,
                                    ProductId = item.ProductId,
                                    Status = Convert.ToString(item.ProductStatus)
                                }).AsQueryable();


            return searchResult;
        }

        [HttpGet]
        public ActionResult ProductBusinessEditor(int? ProductId)
        {
            TempData["product"] = "";
            var products = new ProductBusinessModel();
            ProductBusinessModel model = new ProductBusinessModel();
            try
            {

                if (ProductId.HasValue)
                {
                    var grouplist = (from lt in _db.MNT_Products where lt.ProductId == ProductId select lt).FirstOrDefault();

                    model.ProductId = grouplist.ProductId;
                    model.ProductCode = grouplist.ProductCode;
                    model.DisplayProductCode = grouplist.DispProductCode;
                    model.ProductDisplayName = grouplist.ProductDisplayName;
                    if (grouplist.ProductStatus == 1)
                    {
                        model.Status = "Active";
                    }
                    else
                    {
                        model.Status = "Inactive";
                    }

                    model.Statuslist = LoadLookUpValue("STATUS");

                    model.CreatedBy = grouplist.CreatedBy == null ? " " : grouplist.CreatedBy;
                    if (grouplist.CreatedDate != null)
                        model.CreatedOn = (DateTime)grouplist.CreatedDate;
                    else
                        model.CreatedOn = DateTime.MinValue;
                    model.ModifiedBy = grouplist.ModifiedBy == null ? " " : grouplist.ModifiedBy;
                    model.ModifiedOn = grouplist.ModifiedDate;

                    return View(model);
                }
                products.Statuslist = LoadLookUpValue("STATUS");
                //return View(products);


            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving product Class  " + model.ProductId + " for product Class." + model.ProductDisplayName + ".");
                addInfo.Add("entity_type", "product Class");
                PublishException(ex, addInfo, 0, "product Class" + model.ProductId);
                return View(model);
            }

            return View(products);

        }



        [HttpPost]
        public ActionResult ProductBusinessEditor(ProductBusinessModel model)
        {
            TempData["result"] = "";
            model.Statuslist = LoadLookUpValue("STATUS");
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }

                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_Products where x.ProductId == model.ProductId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var list = model.Update();
                        //TempData["result"] = "Records Saved Successfully.";
                        TempData["result"] = Common.RecordsSavedSuccessfully;
                        TempData["display"] = list.ProductCode;
                        return View(model);

                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var list = model.Update();
                        //TempData["result"] = "Records Updated Successfully.";
                        TempData["result"] = Common.RecordsUpdatedSuccessfully;
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
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving product Class " + model.ProductId + " for product Class." + model.ProductId + ".");
                addInfo.Add("entity_type", "product close");
                PublishException(ex, addInfo, 0, "product Class" + model.ProductId);
                return View(model);
            }


        }

        [HttpGet]
        public JsonResult CheckProductCode11(string productcode)
        {
            var prodcode = _db.MNT_Products.ToList();

            var isDuplicate = false;

            foreach (var prod in prodcode)
            {
                if (prod.ProductDisplayName == productcode)
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CheckProductCode112(string productcode1)
        {
            var prodcode = _db.MNT_Products.ToList();

            var isDuplicate = false;

            foreach (var prod in prodcode)
            {
                if (prod.ProductCode == productcode1)
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckProdCode(string ProductDescription, string Prod)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Products where t.ProductCode == ProductDescription orderby t.ProductCode descending select t.ProductCode).Take(1)).FirstOrDefault();
            if ((num != null) && (Prod == null || Prod == ""))
            {
                result = true;
            }
            else if ((num != null && num.ToLower() != Prod.ToLower()))
            {
                result = true;
            }
            return Json(result);
        }
        #endregion
        #region Sub Class

        public ActionResult SubClassIndex()
        {

            List<SubClassModel> list = new List<SubClassModel>();
            try
            {
                list = SubClassModel.GetUserResult("", "");
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Sub class " + list + " for SubClass." + list + ".");
                addInfo.Add("entity_type", "Sub Class");
                PublishException(ex, addInfo, 0, "Sub class" + list);
                return View(list);

            }
            return View(list);
        }

        public JsonResult FillGeneralStatus()
        {
            var returnData = LoadLookUpValue("STATUS");
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillProducts()
        {
            var returnData = LoadProducts();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillSubClassProducts()
        {
            var returnData = LoadSubCodeDescription();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckSubClassCode(string SubClassCode, string SubClas)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_ProductClass where t.ClassCode == SubClassCode orderby t.ClassCode descending select t.ClassCode).Take(1)).FirstOrDefault();
            if (num != null && num.ToLower() != SubClas.ToLower())
            {
                result = true;
            }
            return Json(result);
        }


        [HttpPost]
        public ActionResult SubClassIndex(string ClassCode, string SubClassCode)
        {

            ClassCode = ((Request.Form["ddlProduct"] == null) ? "" : Request.Form["ddlProduct"]);
            SubClassCode = ((Request.Form["ddlSubClass"] == null) ? "" : Request.Form["ddlSubClass"]);
            ViewBag.subclass = SubClassCode;
            List<SubClassModel> list = new List<SubClassModel>();
            try
            {
                list = SubClassModel.GetUserResult(ClassCode, SubClassCode);
            }

            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Sub class " + list + " for Search result Subclass." + list + ".");
                addInfo.Add("entity_type", "Sub class");
                PublishException(ex, addInfo, 0, "Sub class" + list);
            }
            return View(list);
        }

        public ActionResult SubClassEditor(int? ID)
        {

            var usergroup = new SubClassModel();
            SubClassModel objmodel = new SubClassModel();
            try
            {
                if (ID.HasValue)
                {
                    var grouplist = (from lt in _db.MNT_ProductClass where lt.ID == ID select lt).FirstOrDefault();
                    objmodel.ID = ID;
                    objmodel.CobCode = grouplist.ClassCode;
                    TempData["SubClass"] = "Sub Class Code - " + objmodel.CobCode + ".";
                    objmodel.CobDesc = grouplist.ClassDesc;
                    objmodel.ProductCode = grouplist.ProdCode;
                    objmodel.MainClassList = LoadProductsDescription();
                    if (grouplist.Status == "1")
                    {
                        objmodel.Status = "Active";
                    }
                    else
                    {
                        objmodel.Status = "Inactive";
                    }

                    if (grouplist.BusinessType == "M")
                    {
                        objmodel.BusinessType = "Material Damage";
                    }
                    else
                    {
                        objmodel.BusinessType = "Business Interuption";
                    }
                    //   objmodel.Status = grouplist.Status;
                    objmodel.Remarks = grouplist.Remarks;
                    objmodel.BusinessType = grouplist.BusinessType;
                    objmodel.BusinessTypelist = LoadLookUpValue("ENGTYPEOFBUSINESS");
                    objmodel.Statuslist = LoadLookUpValue("STATUS");

                    objmodel.CreatedBy = grouplist.CreatedBy == null ? " " : grouplist.CreatedBy;
                    if (grouplist.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)grouplist.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = grouplist.ModifiedBy == null ? " " : grouplist.ModifiedBy;
                    objmodel.ModifiedOn = grouplist.ModifiedDate;
                    return View(objmodel);
                }

            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving sub class name " + objmodel.CobCode + " for sub class." + objmodel.CobDesc + ".");
                addInfo.Add("entity_type", "sub class");
                PublishException(ex, addInfo, 0, "sub Master" + objmodel.CobCode);
                return View(objmodel);
            }


            usergroup.BusinessTypelist = LoadLookUpValue("ENGTYPEOFBUSINESS");
            usergroup.Statuslist = LoadLookUpValue("STATUS");
            usergroup.MainClassList = LoadProductsDescription();
            return View(usergroup);

        }

        [HttpPost]
        public ActionResult SubClassEditor(SubClassModel model)
        {
            TempData["notice"] = "";

            try
            {
                model.Statuslist = LoadLookUpValue("STATUS");
                model.BusinessTypelist = LoadLookUpValue("ENGTYPEOFBUSINESS");
                model.MainClassList = LoadProductsDescription();
                ModelState.Clear();
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    var hits = (from x in _db.MNT_ProductClass where (x.ID == model.ID) select x).FirstOrDefault();
                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var grouplist = model.SubClassUpdate();
                        //TempData["notice"] = "Records Saved Successfully.";
                        TempData["notice"] = Common.RecordsSavedSuccessfully;
                        return View(model);

                    }

                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var grouplist = model.SubClassUpdate();
                        //TempData["notice"] = "Records Updated Successfully.";
                        TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                        return View(model);


                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving sub class name " + model.CobCode + " for subclass." + model.CobDesc + ".");
                addInfo.Add("entity_type", "Sub class");
                PublishException(ex, addInfo, 0, "Sub class" + model.CobCode);
                return View(model);
            }
            return View(model);
        }


        [HttpGet]
        public JsonResult CheckUsername1(string username)
        {
            var prodcode = _db.MNT_ProductClass.ToList();

            var isDuplicate = false;

            foreach (var prod in prodcode)
            {
                if (prod.ClassCode == username.ToUpper())
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
