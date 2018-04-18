using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using MCAS.Entity;
using MCAS.Web.Objects.MastersHelper;
using System.Data.Entity;
using System.Data.EntityModel;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data.Objects;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Specialized;

namespace MCAS.Controllers
{
    public class FALController : BaseController
    {
        MCASEntities _db = new MCASEntities();
        //
        // GET: /FAL/

        public ActionResult Index()
        {
            return View();
        }

        #region "Finance Authority Limit"
        [HttpGet]
        public ActionResult FALIndex()
        {
            List<FALModel> list = new List<FALModel>();
            try
            {
                list = FALModel.FetchList();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult FALIndex(string FALCatId, string FALNameId, string Amount)
        {
            FALNameId = ((Request.Form["ddlFALLevelName"] == null) ? "" : Request.Form["ddlFALLevelName"]);
            FALCatId = ((Request.Form["ddlFALAccessCat"] == null) ? "" : Request.Form["ddlFALAccessCat"]);
            Amount = ((Request.Form["ddlFALAccessCat"] == null) ? "" : Request.Form["FALAmount"].Trim());
            ViewBag.FALName = FALNameId;
            ViewBag.FALCat = FALCatId;
            ViewBag.Amount = Amount;
            List<FALModel> list = new List<FALModel>();
            try
            {
                list = GetFALResult(FALCatId, FALNameId, Amount).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for FAL " + list + " for Search result FAL." + list + ".");
                addInfo.Add("entity_type", "FAL");
                PublishException(ex, addInfo, 0, "FAL" + list);
                return View(list);
            }
            return View(list);
        }

        public List<FALModel> GetFALResult(string FALCat, string FALName, string Amount)
        {
            FALCat = FALCat == null || FALCat == "[Select...]" ? "" : FALCat;
            FALName = FALName == null || FALName == "[Select...]" ? "" : FALName;
            Amount = Amount == null ? "" : Amount;
            var items = new List<FALModel>();
            var FALlist = _db.Proc_GetFALMasterList(FALCat, FALName, Amount).ToList();
            var searchResult = (from item in FALlist
                     select new FALModel()
                     {
                         FALId = item.FALId,
                         FALAccessCategory = item.FALAccessCategory,
                         FALLevelName = item.FALLevelName,
                         Amount = item.Amount,
                         UnlimitedAmt = item.UnlimitedAmt
                     }).ToList();
            return searchResult;
        }

        [HttpGet]
        public ActionResult FALEditor(int? Id)
        {
            List<UnlimitedAmtStatus> list = new List<UnlimitedAmtStatus>();
            list.Add(new UnlimitedAmtStatus() { ID = "Y", Name = "Yes" });
            list.Add(new UnlimitedAmtStatus() { ID = "N", Name = "No" });
            SelectList sl = new SelectList(list, "ID", "Name");
            var model = new FALModel();
            model.unlimitedAmtlist = sl;
            model.UnlimitedAmt = "N";
            model.FALCatlist = LoadLookUpValue("FALCat");
            if (Id.HasValue)
            {
                var faldetails = (from x in _db.MNT_FAL where x.FALId == Id select x).FirstOrDefault();
                FALModel objmodel = new FALModel();
                objmodel.FALId = faldetails.FALId;
                objmodel.FALLevelName = faldetails.FALLevelName;
                objmodel.FALAccessCategory = faldetails.FALAccessCategory;
                objmodel.UnlimitedAmt = faldetails.UnlimitedAmt;
                objmodel.Amount = faldetails.Amount;

                objmodel.unlimitedAmtlist = sl;
                objmodel.FALCatlist = LoadLookUpValue("FALCat");
                objmodel.CreatedBy = faldetails.CreatedBy == null ? " " : faldetails.CreatedBy;
                if (faldetails.CreatedDate != null)
                    objmodel.CreatedOn = (DateTime)faldetails.CreatedDate;
                else
                    objmodel.CreatedOn = DateTime.MinValue;
                objmodel.ModifiedBy = faldetails.ModifiedBy == null ? " " : faldetails.ModifiedBy;
                objmodel.ModifiedOn = faldetails.ModifiedDate;

                return View(objmodel);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult FALEditor(FALModel model)
        {
            try
            {
                List<UnlimitedAmtStatus> lst = new List<UnlimitedAmtStatus>();
                lst.Add(new UnlimitedAmtStatus() { ID = "Y", Name = "Yes" });
                lst.Add(new UnlimitedAmtStatus() { ID = "N", Name = "No" });
                SelectList sl = new SelectList(lst, "ID", "Name");
                model.unlimitedAmtlist = sl;
                model.FALCatlist = LoadLookUpValue("FALCat");
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    var hits = (from x in _db.MNT_FAL where x.FALId == model.FALId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        ModelState.Clear();
                        model.CreatedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        ModelState.Clear();
                        model.ModifiedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Updated Successfully.";
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
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }

        public JsonResult FillFALLevelNameList()
        {
            var returnData = LoadFALLevelName();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillFALAccessCatList()
        {
            //var returnData = LoadFALAccessCat();
            var returnData = LoadLookUpValue("FALCat");
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FALNameExists(string falnameId,string falcat)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_FAL where t.FALLevelName == falnameId && t.FALAccessCategory == falcat orderby t.FALLevelName descending select t.FALLevelName).Take(1)).FirstOrDefault();
            if (num != null)
            {
                result = true;
            }
            db.Dispose();
            return Json(result);
        }
        #endregion
    }
}
