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
using MCAS.Web.Objects.Resources.Common;
using MCAS.Web.Objects.Resources.Masters;
using System.Linq.Dynamic;
using System.Net;




namespace MCAS.Controllers
{
    public class MastersController : BaseController
    {
        MCASEntities _db = new MCASEntities();

        // GET: /Masters/

        //[HttpGet]
        //public ActionResult VehicleIndex(VehicleModel model) {
        //    return View(model);
        //}

        #region Vehicle Make
        [HttpGet]
        public ActionResult VehicleIndex()
        {
            List<VehicleMakeModel> list = new List<VehicleMakeModel>();
            try
            {
                list = VehicleMakeModel.FetchVehicleClass();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Vehicle make  " + list + " for Vehicle make." + list + ".");
                addInfo.Add("entity_type", "Vehicle make");
                PublishException(ex, addInfo, 0, "Vehicle make" + list);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult VehicleIndex(string MakeCode, string MakeName)
        {
            MakeCode = Request.Form["MakeCode"].Trim();
            MakeName = Request.Form["MakeName"].Trim();
            ViewBag.Makecode = MakeCode;
            ViewBag.makename = MakeName;
            List<VehicleMakeModel> _vh = new List<VehicleMakeModel>();
            try
            {
                _vh = GetSearchResult(MakeCode, MakeName).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Vehicle make " + MakeCode + " for Search result Vehicle make." + MakeName + ".");
                addInfo.Add("entity_type", "Vehicle make");
                PublishException(ex, addInfo, 0, "Vehicle make" + MakeCode);
                return View(_vh);
            }
            return View(_vh);



        }


        public IQueryable<VehicleMakeModel> GetSearchResult(string makecode, string makename)
        {

            var SearchResult = (from x in _db.MNT_Motor_Make
                                where x.MakeCode.Contains(makecode) &&
                                      x.MakeName.Contains(makename) &&
                                      x.MakeCode.Contains("vem") &&
                                      x.status == "Active"

                                select x).ToList().Select(x => new VehicleMakeModel
                                {
                                    TranId = x.TranId,
                                    MakeCode = x.MakeCode,
                                    MakeName = x.MakeName,
                                    Status = x.status


                                }).AsQueryable();
            return SearchResult;

        }

        [HttpGet]
        public ActionResult VehicleMaster(int? Tranid)
        {

            var make = new VehicleMakeModel();
            VehicleMakeModel model = new VehicleMakeModel();
            if (Tranid.HasValue)
            {
                var mcode = (from x in _db.MNT_Motor_Make where x.TranId == Tranid select x).FirstOrDefault();
                if (mcode.status == "Active")
                {
                    model.Status = "Active";
                }
                else
                {
                    model.Status = "Inactive";
                }
                model.TranId = mcode.TranId;
                model.Statuslist = LoadLookUpValue("STATUS");
                model.MakeCode = mcode.MakeCode;
                model.MakeName = mcode.MakeName;

                model.CreatedBy = mcode.CreatedBy == null ? " " : mcode.CreatedBy;
                if (mcode.CreatedDate != null)
                    model.CreatedOn = (DateTime)mcode.CreatedDate;
                else
                    model.CreatedOn = DateTime.MinValue;
                model.ModifiedBy = mcode.ModifiedBy == null ? " " : mcode.ModifiedBy;
                model.ModifiedOn = mcode.ModifiedDate;

                return View(model);
            }
            make.Statuslist = LoadLookUpValue("STATUS");
            make.SubClassList = LoadSubClass();
            return View(make);
        }

        [HttpPost]
        public ActionResult VehicleMaster(VehicleMakeModel model)
        {
            model.Statuslist = LoadLookUpValue("STATUS");


            if (ModelState.IsValid)
            {
                ModelState.Clear();
                var moter_make = _db.MNT_Motor_Make.Where(x => x.MakeName == model.MakeName && x.MakeCode.Contains("vem") && x.status == "Active" && x.TranId != model.TranId).Select(x => x.MakeName).Take(1).FirstOrDefault();
                if (moter_make != null)
                {
                    ModelState.AddModelError("", MCAS.Web.Objects.Resources.Masters.VehicleMaster.MsgDuplicatevehiclemakename);
                }
                else
                {
                    var hits = (from x in _db.MNT_Motor_Make where x.TranId == model.TranId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var csd = model.VehicleMakeUpdate();
                        TempData["notice"] = Common.RecordsSavedSuccessfully;
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var csd = model.VehicleMakeUpdate();
                        TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                    }
                }


            }
            else
            {
                var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                ModelError(errors, "VehicleClassEditor");
            }
            return View(model);
        }

        public JsonResult CheckVehicleMakeName(string VMake, string Vcode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            try
            {
                var num = ((from t in db.MNT_Motor_Make where t.MakeName.ToLower() == VMake.ToLower() && t.MakeCode.Contains("vem") && t.status == "Active" select t.MakeName).Take(1)).FirstOrDefault();
                if ((num != null) && (num.ToLower() != Vcode.ToLower()))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }
            finally
            {
                db.Dispose();
            }
            return Json(result);
        }



        #endregion


        #region Vehicle Model
        [HttpGet]
        public ActionResult VModelIndex()
        {
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                list = VehicleModel.GetVehicleModelSearchResult("");
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while loading Vehicle Model " + list + " for model No." + list + ".");
                addInfo.Add("entity_type", "VehicleModel");
                PublishException(ex, addInfo, 0, "VehicleModel" + list);
            }
            return View(list);
        }

        public JsonResult FillMakeCodeList()
        {
            List<VehicleListItem> returnData = VehicleModel.GetVehicleModelSearchResult("").Select(x => new VehicleListItem()
            {
                MakeCode = x.MakeCode,
                MakeName = x.ModelName
            }).Distinct().ToList();
            returnData.Insert(0, new VehicleListItem() { MakeCode = "", MakeName = "[Select...]" });
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillVehicleClassList()
        {
            var returnData = VehicleListItem.FetchVehicleClass();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult VModelIndex(string makecode)
        {
            makecode = ((Request.Form["ddlMake"] == null) ? "" : Request.Form["ddlMake"]);
            ViewBag.make = makecode;
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                list = VehicleModel.GetVehicleModelSearchResult(makecode);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while searching Vehicle Model " + makecode + " for model No." + makecode + ".");
                addInfo.Add("entity_type", "VehicleModel");
                PublishException(ex, addInfo, 0, "VehicleModel" + makecode);
            }
            return View(list);

        }


        [HttpGet]
        public ActionResult VModelList(int? TranId)
        {

            var VModel = new VehicleModel();
            VehicleModel model = new VehicleModel();
            try
            {
                if (TranId.HasValue)
                {
                    var vehlist = (from lt in _db.MNT_MOTOR_MODEL where lt.TranId == TranId select lt).FirstOrDefault();

                    model.TranId = vehlist.TranId;
                    model.ModelCode = vehlist.ModelCode;

                    model.MakeCode = vehlist.MakeCode;
                    model.vehicleMakelist = LoadVehicleMake();

                    model.ModelName = vehlist.ModelName;

                    model.VehicleClassCode = vehlist.VehicleClassCode;
                    model.VehicleClasslist = LoadVehicleClass();

                    model.CC = vehlist.CC;
                    model.LC = vehlist.LC;

                    model.NoOfPassenger = vehlist.NoOfPassenger;

                    model.Status = vehlist.status;
                    model.Status = model.Status == "Active" ? "Active" : "Inactive";
                    model.Statuslist = LoadLookUpValue("STATUS");

                    model.CreatedBy = vehlist.CreatedBy == null ? " " : vehlist.CreatedBy;
                    if (vehlist.CreatedDate != null)
                        model.CreatedOn = (DateTime)vehlist.CreatedDate;
                    else
                        model.CreatedOn = DateTime.MinValue;
                    model.ModifiedBy = vehlist.ModifiedBy == null ? " " : vehlist.ModifiedBy;
                    model.ModifiedOn = vehlist.ModifiedDate;



                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Vehicle Model " + model.ModelName + " for Vehicle Model." + model.ModelDescription + ".");
                addInfo.Add("entity_type", "Vehicle Model");
                PublishException(ex, addInfo, 0, "Vehicle Model" + model.TranId);
                return View(model);
            }

            VModel.VehicleClasslist = LoadVehicleClass();
            VModel.vehicleMakelist = LoadVehicleMake();
            VModel.Statuslist = LoadLookUpValue("STATUS");
            return View(VModel);
        }

        [HttpPost]
        public ActionResult VModelList(VehicleModel model)
        {
            TempData["notice"] = "";
            try
            {
                model.VehicleClasslist = LoadVehicleClass();
                model.vehicleMakelist = LoadVehicleMake();
                model.Statuslist = LoadLookUpValue("STATUS");
                ModelState.Clear();
                var hits = (from x in _db.MNT_MOTOR_MODEL where x.TranId == model.TranId select x).FirstOrDefault();
                if (hits == null)
                {
                    model.CreatedBy = LoggedInUserName;
                    var csd = model.VehicleModelUpdate();
                    TempData["notice"] = Common.RecordsSavedSuccessfully;
                }
                else
                {
                    model.ModifiedBy = LoggedInUserName;
                    var csd = model.VehicleModelUpdate();
                    TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving Vehicle Model " + model.TranId + " for surveyor." + model.ModelDescription + ".");
                addInfo.Add("entity_type", "Vehicle Model");
                PublishException(ex, addInfo, 0, "Vehicle Model" + model.ModelName);
                return View(model);
            }
            return View(model);
        }



        public JsonResult FillMakeModel()
        {
            var returnData = VehicleListItem.LoadVehicleMake();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult CheckVehicleModelName(string VModel, string Vcode)
        //{
        //    MCASEntities db = new MCASEntities();
        //    var result = false;
        //    var num = ((from t in db.MNT_MOTOR_MODEL where t.ModelCode == VModel select t.ModelCode).Take(1)).FirstOrDefault();
        //    if ((num != null) && (num.ToLower() != Vcode.ToLower()))
        //    {
        //        result = true;
        //    }
        //    db.Dispose();
        //    return Json(result);
        //}

        #endregion


        #region Vehicle Class
        [HttpGet]
        public ActionResult VehicleClassIndex()
        {
            List<VehicleTypeModel> list = new List<VehicleTypeModel>();
            try
            {
                list = VehicleTypeModel.GetVehicleClassResult("", "");
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Vehicle Class " + list + " for Vehicle Class." + list + ".");
                addInfo.Add("entity_type", "Vehicle Close");
                PublishException(ex, addInfo, 0, "Vehicle Close" + list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult VehicleClassIndex(string BodyCode, string BodyDesc)
        {
            BodyCode = Request.Form["inputBodyCode"].Trim();
            BodyDesc = Request.Form["inputBodyDescription"].Trim();
            ViewBag.bodycode = BodyCode;
            ViewBag.bodydesc = BodyDesc;
            List<VehicleTypeModel> list = new List<VehicleTypeModel>();
            try
            {
                list = VehicleTypeModel.GetVehicleClassResult(BodyCode, BodyDesc);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Vehicle Class " + BodyCode + " for Search result Vehicle Class." + BodyDesc + ".");
                addInfo.Add("entity_type", "Vehicle Class");
                PublishException(ex, addInfo, 0, "Vehicle Class" + BodyDesc);
            }

            return View(list);

        }

        [HttpGet]
        public ActionResult VehicleClassEditor(int? TranId)
        {
            TempData["VehicleClass"] = "";
            var vehicleclass = new VehicleTypeModel();
            VehicleTypeModel objmodel = new VehicleTypeModel();
            try
            {
                if (TranId.HasValue)
                {
                    var vehicletype = (from x in _db.MNT_Motor_Class where x.TranId == TranId select x).FirstOrDefault();
                    objmodel.TranId = vehicletype.TranId;
                    objmodel.VehicleClassCode = vehicletype.VehicleClassCode;
                    objmodel.Status = vehicletype.Status;
                    objmodel.Status = objmodel.Status == "Active" ? "Active" : "Inactive";
                    objmodel.Statuslist = LoadLookUpValue("STATUS");
                    objmodel.VehicleClassDesc = vehicletype.VehicleClassDesc;
                    objmodel.CreatedBy = vehicletype.CreatedBy == null ? " " : vehicletype.CreatedBy;
                    if (vehicletype.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)vehicletype.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = vehicletype.ModifiedBy == null ? " " : vehicletype.ModifiedBy;
                    objmodel.ModifiedOn = vehicletype.ModifiedDate;

                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Vehicle Class Code " + objmodel.TranId + " for Vehicle Class." + objmodel.VehicleClassCode + ".");
                addInfo.Add("entity_type", "Vehicle Class");
                PublishException(ex, addInfo, 0, "Vehicle Class" + objmodel.TranId);
                return View(objmodel);
            }
            vehicleclass.Statuslist = LoadLookUpValue("STATUS");
            return View(vehicleclass);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VehicleClassEditor(VehicleTypeModel model)
        {
            try
            {
                model.Statuslist = LoadLookUpValue("STATUS");
                TempData["notice"] = "";
                TempData["VehicleClass"] = "";
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_Motor_Class where x.TranId == model.TranId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var csd = model.VehicleClassUpdate();
                        TempData["notice"] = Common.RecordsSavedSuccessfully;
                        TempData["display"] = csd.VehicleClassCode;

                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var csd = model.VehicleClassUpdate();
                        TempData["notice"] = Common.RecordsUpdatedSuccessfully;
                    }
                }
                else
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving Vehicle Class " + model.TranId + " for Vehicle Class." + model.VehicleClassCode + ".");
                addInfo.Add("entity_type", "claim close");
                PublishException(ex, addInfo, 0, "Vehicle Class" + model.VehicleClassDesc);
            }
            return View(model);
        }



        public JsonResult CheckVehicleClassName(string VMake, string Vcode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Motor_Class where t.VehicleClassDesc.ToLower() == VMake.ToLower() select t.VehicleClassDesc).Take(1)).FirstOrDefault();
            if ((num != null) && (num.ToLower() != Vcode.ToLower()))
            {
                result = true;
            }
            return Json(result);
        }
        #endregion


        #region BusCaptain

        [HttpGet]
        public ActionResult VehicleBusCaptainIndex(VehicleUploadViewModel model)
        {
            VehicleUploadViewModel viewModel = new VehicleUploadViewModel();
            viewModel.ListUploadstatus = VehicleUploadModel.fetch();
            viewModel.UploadType = "BC";

            try
            {
                var Uploadrefno = model.UploadFileRefNo != null ? model.UploadFileRefNo : "";
                var Uploadstatus = model.Status != null ? model.Status : "";
                DateTime? UploadFromDate = model.UploadFromDate;
                DateTime? UploadToDate = model.UploadToDate;
                viewModel.ListVehicleUplaod = VehicleUploadModel.SearchVehicleUploadHistory(Uploadrefno, Uploadstatus, UploadFromDate, UploadToDate, viewModel.UploadType);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult VehicleBusCaptainIndex(HttpPostedFileBase uploadFile)
        {
            MCASEntities db = new MCASEntities();
            VehicleBusCaptainModel objModel = new Web.Objects.MastersHelper.VehicleBusCaptainModel();
            string strConnection = ConfigurationManager.AppSettings["ExceptionConString"].ToString();
            //string ssqltable = "MNT_BusCaptain";
            string excelConnectionString = string.Empty;
            var model = new VehicleUploadModel();
            model.CreatedBy = LoggedInUserName;
            model.ModifiedBy = LoggedInUserName;
            string CreatedBy = model.CreatedBy;
            string ModifiedBy = model.CreatedBy;
            var sqlFormattedCreatedDate = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");

            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();
            var fileServerpath = "";
            string UploadFolderPath = "";
            string FileUploadPath = "";


            MNT_VehicleListingUpload fileupload = new MNT_VehicleListingUpload();
            DataSet ds = new DataSet();
            try
            {
                if (ConfigurationManager.AppSettings["UploadFolder"] == null)
                {
                    TempData["upload"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                    return RedirectToAction("VehicleBusCaptainIndex");
                }
                else
                {
                    UploadFolderPath = ConfigurationManager.AppSettings["UploadFolder"].ToString();
                    if (String.IsNullOrEmpty(UploadFolderPath))
                    {
                        TempData["upload"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                        return RedirectToAction("VehicleBusCaptainIndex");
                    }
                }

                fileServerpath = (from l in db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                if (String.IsNullOrEmpty(fileServerpath))
                {
                    TempData["upload"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                    return RedirectToAction("VehicleBusCaptainIndex");
                }
                FileUploadPath = fileServerpath.TrimEnd('\\') + "\\" + UploadFolderPath.TrimStart('\\');

                EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
                if (!objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
                {
                    TempData["upload"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                    return RedirectToAction("VehicleBusCaptainIndex");
                }

                if (uploadFile != null)
                {
                    if (uploadFile.ContentLength > 0)
                    {
                        //string pathForSave = "\\" + "Uploads\\VechileReg";
                        string pathForSave = UploadFolderPath + "\\VechileReg";

                        var fileName = Path.GetFileName(uploadFile.FileName);
                        //string pat = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + "Uploads/VechileReg");

                        string pat = FileUploadPath + @"\VechileReg";
                        if (!Directory.Exists(pat))
                        {
                            Directory.CreateDirectory(pat);
                        }
                        string filePath = Path.Combine(pat, Path.GetFileName(uploadFile.FileName));


                        if (fileName != "")
                        {
                            var filename1 = Path.GetFileName(uploadFile.FileName);
                            //var Fileexistscheck = (from l in _db.MNT_VehicleListingUpload where l.UploadFileName == filename1 select l).FirstOrDefault();
                            bool isFileExists = _db.MNT_VehicleListingUpload.Where(t => t.UploadFileName == fileName).Any();
                            if (isFileExists)
                            {
                                TempData["upload"] = "File with same name already exists.";
                                return RedirectToAction("VehicleBusCaptainIndex");
                            }
                            if (System.IO.File.Exists(filePath))
                                System.IO.File.Delete(filePath);
                            uploadFile.SaveAs(filePath);
                            string fileLocation = filePath;
                            string fileExtension = filePath.Split('\\').Last().ToString().Split('.').Last();
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //if (fileExtension == "xls" || fileExtension == "XLS")
                            //{
                            //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            //    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            //}
                            //else if (fileExtension == "xlsx" || fileExtension == "XLSX")
                            //{
                            //    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            //    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //}
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            DataTable dt = new DataTable();
                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }

                            String[] excelSheets = new String[dt.Rows.Count];
                            int counter = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[counter] = row["TABLE_NAME"].ToString();
                                counter++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                try
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        excelConnection.Dispose();
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                                catch (Exception) { }
                                TempData["upload"] = "Cannot save as their is no data to insert.";
                                return RedirectToAction("VehicleBusCaptainIndex");
                            }
                            if (ds.Tables[0].Columns.Count < 6)
                            {
                                try
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        excelConnection.Dispose();
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                                catch (Exception) { }
                                TempData["upload"] = "Cannot save as number of columns are less than Six.";
                                return RedirectToAction("VehicleBusCaptainIndex");
                            }

                            //Remove if blank rows
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                String valuesarr = String.Empty;
                                List<object> lst = ds.Tables[0].Rows[i].ItemArray.ToList();
                                foreach (Object s in lst)
                                {
                                    valuesarr += s.ToString();
                                }

                                if (String.IsNullOrEmpty(valuesarr))
                                {
                                    ds.Tables[0].Rows.RemoveAt(i);
                                    ds.AcceptChanges();
                                    i--;
                                }
                            }

                            string[] columnNames = { "Driver_No".Trim(), "Name".Trim(), "Driver_NRIC".Trim(), "Nat_cd".Trim(), "Date_Join".Trim(), "Date_Of_Resign".Trim() };
                            List<string> strColumn = columnNames.ToList();

                            bool columnexist = IsAllColumnExist(ds.Tables[0], strColumn);

                            if (!columnexist)
                            {
                                TempData["upload"] = "Cannot save as columns names are not matching.";
                                return RedirectToAction("VehicleBusCaptainIndex");
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                var maxlength = 8;
                                var prefix = "BC";
                                //var countrows = (from row in _db.MNT_VehicleListingUpload select (int?)row.UploadFileId).Max() ?? 0;
                                //string currentno = (countrows + 1).ToString();

                                string UploadFileRefNo = (from row in _db.MNT_VehicleListingUpload
                                                       .Where(t => t.UploadFileRefNo.Contains(prefix))
                                                       .OrderByDescending(t => t.UploadFileId)
                                                          select row.UploadFileRefNo).FirstOrDefault();

                                int lastValue = string.IsNullOrEmpty(UploadFileRefNo) ? 0 : Convert.ToInt32(UploadFileRefNo.Substring(prefix.Length));
                                string currentno = (lastValue + 1).ToString();


                                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                                var fileRefNo = (prefix + result + currentno);
                                fileupload.UploadFileRefNo = fileRefNo;
                                fileupload.UploadFileName = fileName;
                                fileupload.UploadedDate = DateTime.Now;
                                fileupload.IS_PROCESSED = "N";
                                fileupload.IS_ACTIVE = "Y";
                                fileupload.Status = "Incomplete";
                                //   fileupload.PROCESSED_DATE = DateTime.Now;
                                fileupload.UploadPath = pathForSave;
                                _db.MNT_VehicleListingUpload.AddObject(fileupload);
                                _db.SaveChanges();

                                int totalRecords = ds.Tables[0].Rows.Count;
                                int uploadSuccess = 0;

                                DataTable dtLog = new DataTable();
                                foreach (var column in columnNames)
                                {
                                    dtLog.Columns.Add(column);
                                }
                                dtLog.Columns.Add("REASON OF FAILURE");

                                SqlConnection con = new SqlConnection(strConnection);

                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    var VDriverNo = ds.Tables[0].Rows[i]["Driver_No"].ToString();
                                    var VDriverName = ds.Tables[0].Rows[i]["Name"].ToString();
                                    var VDriverNRIC = ds.Tables[0].Rows[i]["Driver_NRIC"].ToString();
                                    var VNatCd = ds.Tables[0].Rows[i]["Nat_cd"].ToString();
                                    //var VDateOfJoin = ds.Tables[0].Rows[i]["Date_Join"].ToString();
                                    //var VDateOfResign = ds.Tables[0].Rows[i]["Date_Of_Resign"].ToString();                                   


                                    string VDateOfJoin = null;
                                    string VDateOfResign = null;
                                    if (Convert.ToString(ds.Tables[0].Rows[i]["Date_Join"]).Trim() != "")
                                        VDateOfJoin = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date_Join"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                    if (Convert.ToString(ds.Tables[0].Rows[i]["Date_Of_Resign"]).Trim() != "")
                                        VDateOfResign = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date_Of_Resign"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");


                                    if (string.IsNullOrEmpty(VDriverNo) || string.IsNullOrEmpty(VDriverName) || string.IsNullOrEmpty(VDriverNRIC) || string.IsNullOrEmpty(VNatCd))
                                    {
                                        dtLog.Rows.Add(VDriverNo, VDriverName, VDriverNRIC, VNatCd, VDateOfJoin, VDateOfResign, "There is no data for mandatory fields");
                                    }
                                    else
                                    {
                                        DateTime? IDateOfJoin = null;
                                        DateTime? IDateOfResign = null;
                                        if (!string.IsNullOrEmpty(VDateOfJoin))
                                        {
                                            IDateOfJoin = Convert.ToDateTime(VDateOfJoin);
                                        }
                                        if (!string.IsNullOrEmpty(VDateOfResign))
                                        {
                                            IDateOfResign = Convert.ToDateTime(VDateOfResign);
                                        }


                                        try
                                        {
                                            #region Comment

                                            //string query1 = "Insert into MNT_BusCaptain(BusCaptainCode,BusCaptainName,NRICPassportNo,Nationality,DateJoined,DateResigned,CreatedBy,CreatedDate,UploadFileRefNo) Values('" + VDriverNo + "','" + VDriverName + "','" + VDriverNRIC +
                                            //           "','" + VNatCd + "',";
                                            //if (string.IsNullOrEmpty(VDateOfJoin))
                                            //    query1 = query1 + "NULL,";
                                            //else
                                            //    query1 = query1 + "'" + VDateOfJoin + "',";

                                            //if (string.IsNullOrEmpty(VDateOfResign))
                                            //    query1 = query1 + "NULL,";
                                            //else
                                            //    query1 = query1 + "'" + VDateOfResign + "',";


                                            //query1 = query1 + "'" + CreatedBy + "','" + sqlFormattedCreatedDate + "','" + fileRefNo + "')";

                                            //con.Open();
                                            //SqlCommand cmd = new SqlCommand(query1, con);
                                            //int rowUpdated = cmd.ExecuteNonQuery();
                                            //con.Close();

                                            //if (rowUpdated > 0)
                                            //    uploadSuccess++;

                                            #endregion

                                            int? rowUpdated = _db.Proc_Insert_MNT_BusCaptain(VDriverNo, VDriverName, VDriverNRIC, VNatCd, IDateOfJoin, IDateOfResign, CreatedBy, fileRefNo).SingleOrDefault();

                                            if (rowUpdated.HasValue)
                                            {
                                                if (rowUpdated.Value == 1)
                                                    uploadSuccess++;
                                                else if (rowUpdated.Value == 2)
                                                {
                                                    dtLog.Rows.Add(VDriverNo, VDriverName, VDriverNRIC, VNatCd, VDateOfJoin, VDateOfResign, "Record aleady exists");
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            con.Close();
                                            dtLog.Rows.Add(VDriverNo, VDriverName, VDriverNRIC, VNatCd, VDateOfJoin, VDateOfResign, "Special characters or data incorrect");
                                        }


                                    }
                                }

                                fileupload.IS_PROCESSED = "Y";
                                fileupload.TotalRecords = totalRecords;
                                fileupload.UplodedSuccess = uploadSuccess;
                                fileupload.UploadedFailed = totalRecords - uploadSuccess;
                                fileupload.Status = "Success";
                                fileupload.PROCESSED_DATE = DateTime.Now;
                                _db.SaveChanges();

                                if (totalRecords != uploadSuccess)
                                {
                                    string logPath = Path.Combine(pat, "Log_" + fileRefNo + ".xls");
                                    SaveDataTableToExcel(dtLog, logPath);
                                    TempData["upload"] = "Uploaded Successfully with some records failed. Uploaded File Ref. No: " + fileRefNo;
                                }
                                else
                                {
                                    TempData["upload"] = "Uploaded Successfully. Uploaded File Ref. No: " + fileRefNo;
                                }
                            }
                            else
                            {
                                TempData["upload"] = "File doesn't have any record.";
                            }

                            return RedirectToAction("VehicleBusCaptainIndex");


                            #region Commented


                            // SQL Server Connection String

                            //var res = VehicleUploadModel.saveexelfile(fileLocation, fileExtension, Path.GetFileName(uploadFile.FileName));
                            //var maxlength = 8;
                            //var prefix = "Ref";
                            //var countrows = (from row in _db.MNT_VehicleListingUpload select (int?)row.UploadFileId).Max() ?? 0;
                            //string currentno = (countrows + 1).ToString();
                            //string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                            //var fileRefNo = (prefix + result + currentno);
                            //fileupload.UploadFileRefNo = fileRefNo;
                            //fileupload.UploadFileName = fileName;
                            //fileupload.UploadedDate = DateTime.Now;
                            //fileupload.IS_PROCESSED = "Y";
                            //fileupload.IS_ACTIVE = "Y";
                            //fileupload.Status = "COMPLETE";
                            //fileupload.PROCESSED_DATE = DateTime.Now;

                            //if (res != "F" && res.Split('-')[0] == "T")
                            //{
                            //    fileupload.TotalRecords = Convert.ToInt32(res.Split('-')[1].Split(',')[0]);
                            //    fileupload.UplodedSuccess = Convert.ToInt32(res.Split('-')[1].Split(',')[1]);
                            //    fileupload.UploadedFailed = Convert.ToInt32(res.Split('-')[1].Split(',')[2]);
                            //    _db.MNT_VehicleListingUpload.AddObject(fileupload);
                            //    _db.SaveChanges();
                            //   // var res2 = _db.Proc_MIG_IL_VEHICLE_DETAIL_Update(Path.GetFileName(uploadFile.FileName));
                            //    TempData["upload"] = "Uploaded Successfully.";
                            //}
                            //else if (res == "F")
                            //{
                            //    TempData["upload"] = "Record Not Inserted Some Error Occurred.";
                            //}
                            //else
                            //{
                            //    TempData["upload"] = res;
                            //}

                            //      }

                            #endregion
                        }
                    }
                }
                else
                {
                    TempData["upload"] = "Please select file to upload.";
                }
            }

            catch (Exception ex)
            {
                TempData["upload"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                //   return View(model);
            }


            return RedirectToAction("VehicleBusCaptainIndex");

        }


        public IQueryable<VehicleBusCaptainModel> GetBusCaptainResult(string BusCaptainCode, string Name, string PassportNo, string ContactNo, string Nationality, DateTime? DateJoined, DateTime? DateResigned)
        {
            return VehicleBusCaptainModel.GetFetchResult(BusCaptainCode, Name, PassportNo, ContactNo, Nationality, DateJoined, DateResigned);
        }


        public ActionResult VehicleBusCaptainEditor(int? TranId)
        {
            VehicleBusCaptainModel objmodel = new VehicleBusCaptainModel();
            if (TranId.HasValue) objmodel = VehicleBusCaptainModel.FetchVehicleBusCaptainModel(objmodel, TranId);
            return View(objmodel);
        }

        [HttpPost]
        public ActionResult VehicleBusCaptainEditor(VehicleBusCaptainModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    TempData["display"] = "";
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_BusCaptain where x.TranId == model.TranId select x).FirstOrDefault();

                    if (hits == null)
                    {
                        var CheckBusCaptainCode = (from x in _db.MNT_BusCaptain where x.BusCaptainCode == model.BusCaptainCode.ToUpper().Trim() select x.TranId).FirstOrDefault();
                        if (CheckBusCaptainCode > 0)
                        {
                            TempData["result"] = "Duplicate Bus Captain Code.";
                            return View(model);
                        }
                        model.CreatedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Saved Successfully.";
                        TempData["display"] = list.BusCaptainCode;
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Updated Successfully.";

                    }
                }
                else
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }

        }

        public JsonResult CheckBusCaptainCode(string BuscaptainCode, string Bcode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_BusCaptain where t.BusCaptainCode == BuscaptainCode select t.BusCaptainCode).Take(1)).FirstOrDefault();
            if ((num != null) && (num.ToLower() != Bcode.ToLower()))
            {
                result = true;
            }
            return Json(result);
        }
        #endregion

        #region Bus Captain Listing Master

        public ActionResult BusCaptainListingMaster()
        {
            List<VehicleBusCaptainModel> list = new List<VehicleBusCaptainModel>();
            try
            {
                list = VehicleBusCaptainModel.FetchVehicleBusCaptain();
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(list);
            }
            return View(list);
        }
        [HttpPost]
        public JsonResult BusCaptainListingMasterJson(int draw, int start, int length)
        {
            CommonUtilities.DatatableGrid list = new CommonUtilities.DatatableGrid();
            JsonResult jsonResult = new JsonResult();
            List<VehicleBusCaptainModel> VehicleBusCaptainlist = new List<VehicleBusCaptainModel>();
            DateTime? dt = null;
            try
            {
                var BusCaptainCode = Request.Form["inputBusCaptainCode"] == null ? "" : Convert.ToString(Request.Form["inputBusCaptainCode"]).Trim();
                var Name = Request.Form["inputName"] == null ? "" : Convert.ToString(Request.Form["inputName"]).Trim();
                var Passportno = Request.Form["inputNric"] == null ? "" : Convert.ToString(Request.Form["inputNric"]).Trim();
                var Nationality = Request.Form["inputNationality"] == null ? "" : Convert.ToString(Request.Form["inputNationality"]).Trim();
                var DateJoined = string.IsNullOrEmpty(Request.Form["DateJoined"]) ? dt : Convert.ToDateTime(Request.Form["DateJoined"]);
                var DateResigned = string.IsNullOrEmpty(Request.Form["DateResigned"]) ? dt : Convert.ToDateTime(Request.Form["DateResigned"]);
                var Searchval = Request.Form["Searchval"] == null ? "" : Convert.ToString(Request.Form["Searchval"]).Trim();

                int sortColumn = Request.Form["order[0][column]"] != null ? Convert.ToInt32(Request.Form["order[0][column]"]) : -1;
                string sortDirection = Request.Form["order[0][dir]"] != null ? Convert.ToString(Request.Form["order[0][dir]"]) : "asc";

                list = string.IsNullOrEmpty(Convert.ToString(Request.Form["search[value]"])) ? VehicleBusCaptainModel.FetchVehicleBusCaptainList(BusCaptainCode, Name, Passportno, "", Nationality, DateJoined, DateResigned, draw, start, length, sortColumn, sortDirection, Request.RawUrl, Request.Form["permisson"] == null ? false : Convert.ToBoolean(Request.Form["permisson"]), Searchval) : VehicleBusCaptainModel.FetchVehicleBusCaptainList(BusCaptainCode, Name, Passportno, "", Nationality, DateJoined, DateResigned, draw, start, length, sortColumn, sortDirection, Request.RawUrl, Request.Form["permisson"] == null ? false : Convert.ToBoolean(Request.Form["permisson"]), Searchval, Convert.ToString(Request.Form["search[value]"]));

                jsonResult = Json(list, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return jsonResult;
        }


        #endregion

        #region Vehicle Listing Master

        public JsonResult GetVsList(VehicleUploadModel criteria)
        {
            List<VehicleUploadModel> list = new List<VehicleUploadModel>();
            List<VehicleUploadModel> paginglist = new List<VehicleUploadModel>();
            try
            {
                list = VehicleUploadModel.FetchVehicleUploadListing();

                if (criteria.BusNo != null || criteria.ChasisNo != null || criteria.Model != null || criteria.Make != null || criteria.VehicleClassCode != null || criteria.Type != null)
                {

                    list = list.Where(
                           x => x.BusNo.ToUpper().Contains(criteria.BusNo == null ? "" : criteria.BusNo.ToUpper().Trim()) &&
                               x.ChasisNo.ToUpper().Contains(criteria.ChasisNo == null ? "" : criteria.ChasisNo.ToUpper().Trim()) &&
                               x.Model.ToUpper().Contains(criteria.Model == null ? "" : criteria.Model.ToUpper().Trim()) &&
                               x.Make.ToUpper().Contains(criteria.Make == null ? "" : criteria.Make.ToUpper().Trim()) &&
                               x.VehicleClassCode.ToUpper().Contains(criteria.VehicleClassCode == null ? "" : criteria.VehicleClassCode.ToUpper().Trim()) &&
                               x.Type.ToUpper().Contains(criteria.Type == null ? "" : criteria.Type.ToUpper().Trim())).ToList();
                }

                if (!string.IsNullOrEmpty(criteria.query))
                {
                    list = list.Where(m => m.GetType().GetProperties().Any(x => x.GetValue(m, null) != null && x.GetValue(m, null).ToString().ToUpper().Contains(criteria.query.ToUpper()))).ToList();
                }

                list = criteria.reverseSort == false ? list.OrderBy(criteria.orderByColumn).ToList() : list.OrderBy(criteria.orderByColumn + " DESC").ToList();
                int val = criteria.currentPage == null ? 1 : Convert.ToInt32(criteria.currentPage) + 1;
                int pno = criteria.currentPage == null ? 0 : Convert.ToInt32(criteria.currentPage);
                int startval = list.Count != 0 ? list.Count < criteria.itemsPerPage ? 1 : ((criteria.currentPage == null ? 0 : Convert.ToInt32(criteria.currentPage)) * criteria.itemsPerPage) + 1 : 0;
                paginglist = list.Count > 0 ? list.Count <= criteria.itemsPerPage ? list.GetRange(0, list.Count).ToList() : list.GetRange((startval == 0 ? 0 : startval - 1), (Math.Min(((pno * criteria.itemsPerPage) + criteria.itemsPerPage), list.Count) - (pno * criteria.itemsPerPage))).ToList() : list;
                if (paginglist.Count > 0)
                {
                    paginglist[0].datalength = list.Count;
                    paginglist[0].startlength = startval;
                    paginglist[0].endlength = Math.Min(list.Count, (criteria.itemsPerPage * val));
                }
                else
                {
                    paginglist.Add(new VehicleUploadModel()
                    {
                        datalength = 0,
                        startlength = 0,
                        endlength = 0
                    });

                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }
            return Json(paginglist, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult VehicleListingMaster()
        {
            List<VehicleUploadModel> list = new List<VehicleUploadModel>();
            try
            {
                list = VehicleUploadModel.FetchVehicleUploadListing();

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot display some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while loading Vehicle listing page " + list + " for model No." + list + ".");
                addInfo.Add("entity_type", "VehicleListing");
                PublishException(ex, addInfo, 0, "VehicleListing" + list);
                return View(list);

            }
            return View(list);
        }

        //public ActionResult VehicleUploadHistory()
        //{
        //    //var model = new VehicleUploadModel();
        //    //model.ListUploadstatus = VehicleUploadModel.fetch();
        //    //return View(model);

        //    VehicleUploadViewModel viewModel = new VehicleUploadViewModel();
        //    viewModel.ListUploadstatus = VehicleUploadModel.fetch();
        //    return View(viewModel);
        //}

        [HttpPost]
        public ActionResult VehicleUploadHistory(VehicleUploadViewModel inputModel)
        {
            if (inputModel.UploadType == "VEH")
                return RedirectToAction("VehicleUploadIndex", inputModel);
            else //if (inputModel.UploadType == "BC")
                return RedirectToAction("VehicleBusCaptainIndex", inputModel);
        }

        #endregion


        #region Vehicle Listing Upload
        [HttpGet]
        public ActionResult VehicleUploadIndex(VehicleUploadViewModel model)
        {
            VehicleUploadViewModel viewModel = new VehicleUploadViewModel();
            viewModel.ListUploadstatus = VehicleUploadModel.fetch();
            viewModel.UploadType = "VEH";
            viewModel.UploadFromDate = model.UploadFromDate;
            viewModel.UploadToDate = model.UploadToDate;

            try
            {
                var Uploadrefno = model.UploadFileRefNo != null ? model.UploadFileRefNo : "";
                var Uploadstatus = model.Status != null ? model.Status : "";
                DateTime? UploadFromDate = model.UploadFromDate;
                DateTime? UploadToDate = model.UploadToDate;

                viewModel.ListVehicleUplaod = VehicleUploadModel.SearchVehicleUploadHistory(Uploadrefno, Uploadstatus, UploadFromDate, UploadToDate, viewModel.UploadType);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(viewModel);
            }
            return View(viewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VehicleUploadIndex(HttpPostedFileBase uploadFile)
        {
            MCASEntities db = new MCASEntities();
            TempData["upload"] = "";
            string VehicleClass = ((Request.Form["ddlVehicleClass"] == null) ? "" : Request.Form["ddlVehicleClass"]);
            if (string.IsNullOrEmpty(VehicleClass))
            {
                TempData["upload"] = "Please Select Vehicle Class To Validate.";
                return RedirectToAction("VehicleUploadIndex");
            }
            ViewBag.VClass = VehicleClass;
            VehicleUploadModel objfile = new VehicleUploadModel();
            var model = new VehicleUploadModel();
            model.CreatedBy = LoggedInUserName;
            model.ModifiedBy = LoggedInUserName;
            string CreatedBy = model.CreatedBy;
            string ModifiedBy = model.CreatedBy;
            var sqlFormattedCreatedDate = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");
            var sqlFormattedModifiedDate = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");

            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();
            var fileServerpath = "";
            string UploadFolderPath = "";
            string FileUploadPath = "";

            MNT_VehicleListingUpload fileupload = new MNT_VehicleListingUpload();
            string strConnection = ConfigurationManager.AppSettings["ExceptionConString"].ToString(); ;
            string excelConnectionString = string.Empty;

            DataSet ds = new DataSet();
            try
            {
                if (ConfigurationManager.AppSettings["UploadFolder"] == null)
                {
                    TempData["upload"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                    return RedirectToAction("VehicleUploadIndex");
                }
                else
                {
                    UploadFolderPath = ConfigurationManager.AppSettings["UploadFolder"].ToString();
                    if (String.IsNullOrEmpty(UploadFolderPath))
                    {
                        TempData["upload"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                        return RedirectToAction("VehicleUploadIndex");
                    }
                }

                fileServerpath = (from l in db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                if (String.IsNullOrEmpty(fileServerpath))
                {
                    TempData["upload"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                    return RedirectToAction("VehicleUploadIndex");
                }
                FileUploadPath = fileServerpath.TrimEnd('\\') + "\\" + UploadFolderPath.TrimStart('\\');

                EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
                if (!objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
                {
                    TempData["upload"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                    return RedirectToAction("VehicleUploadIndex");
                }

                if (uploadFile != null)
                {
                    if (uploadFile.ContentLength > 0)
                    {
                        //string pathForSave = "\\" + "Uploads\\VechileReg";

                        //var fileName = Path.GetFileName(uploadFile.FileName);
                        //string pat = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + "Uploads/VechileReg");
                        //string filePath = Path.Combine(pat, Path.GetFileName(uploadFile.FileName));

                        //string pathForSave = "\\" + "Uploads\\VechileReg";
                        string pathForSave = UploadFolderPath + "\\VechileReg";

                        var fileName = Path.GetFileName(uploadFile.FileName);
                        //string pat = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + "Uploads/VechileReg");

                        string pat = FileUploadPath + @"\VechileReg";
                        if (!Directory.Exists(pat))
                        {
                            Directory.CreateDirectory(pat);
                        }
                        string filePath = Path.Combine(pat, Path.GetFileName(uploadFile.FileName));



                        if (fileName != "")
                        {
                            var filename1 = Path.GetFileName(uploadFile.FileName);
                            //var Fileexistscheck = (from l in _db.MNT_VehicleListingUpload where l.UploadFileName == filename1 select l).FirstOrDefault();
                            bool isFileExists = _db.MNT_VehicleListingUpload.Where(t => t.UploadFileName == fileName).Any();
                            if (isFileExists)
                            {
                                TempData["upload"] = "File with same name already exists.";
                                return RedirectToAction("VehicleUploadIndex");
                            }

                            if (System.IO.File.Exists(filePath))
                                System.IO.File.Delete(filePath);

                            uploadFile.SaveAs(filePath);
                            string fileLocation = filePath;
                            string fileExtension = filePath.Split('\\').Last().ToString().Split('.').Last();
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //if (fileExtension == "xls" || fileExtension == "XLS")
                            //{
                            //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            //    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            //}
                            //else if (fileExtension == "xlsx" || fileExtension == "XLSX")
                            //{
                            //    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            //    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //}
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            DataTable dt = new DataTable();
                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }

                            String[] excelSheets = new String[dt.Rows.Count];
                            int counter = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[counter] = row["TABLE_NAME"].ToString();
                                counter++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                try
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        excelConnection.Dispose();
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                                catch (Exception) { }
                                TempData["upload"] = "Cannot save as their is no data to insert.";
                                return RedirectToAction("VehicleUploadIndex");
                            }
                            if (ds.Tables[0].Columns.Count < 7)
                            {
                                try
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        excelConnection.Dispose();
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                                catch (Exception) { }
                                TempData["upload"] = "Cannot save as number of columns are less than seven.";
                                return RedirectToAction("VehicleUploadIndex");
                            }

                            //Remove if blank rows
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                String valuesarr = String.Empty;
                                List<object> lst = ds.Tables[0].Rows[i].ItemArray.ToList();
                                foreach (Object s in lst)
                                {
                                    valuesarr += s.ToString();
                                }

                                if (String.IsNullOrEmpty(valuesarr))
                                {
                                    ds.Tables[0].Rows.RemoveAt(i);
                                    ds.AcceptChanges();
                                    i--;
                                }
                            }



                            //string[] columnNames = (from dc in ds.Tables[0].Columns.Cast<DataColumn>()
                            //                        select dc.ColumnName.Trim()).ToArray();
                            string[] columnNames = { "BUS NO".Trim(), "CHASSIS NO".Trim(), "MAKE".Trim(), "MODEL".Trim(), "TYPE".Trim(), "AIRCON".Trim(), "AXLE".Trim() };
                            List<string> strColumn = columnNames.ToList();

                            bool columnexist = IsAllColumnExist(ds.Tables[0], strColumn);

                            if (!columnexist)
                            {
                                TempData["upload"] = "Cannot save as columns names are not matching.";
                                return RedirectToAction("VehicleUploadIndex");
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                var maxlength = 8;
                                var prefix = "VEH";
                                //var countrows = (from row in _db.MNT_VehicleListingUpload select (int?)row.UploadFileId).Max() ?? 0;
                                //string currentno = (countrows + 1).ToString();

                                string UploadFileRefNo = (from row in _db.MNT_VehicleListingUpload
                                                      .Where(t => t.UploadFileRefNo.Contains(prefix))
                                                      .OrderByDescending(t => t.UploadFileId)
                                                          select row.UploadFileRefNo).FirstOrDefault();

                                int lastValue = string.IsNullOrEmpty(UploadFileRefNo) ? 0 : Convert.ToInt32(UploadFileRefNo.Substring(prefix.Length));
                                string currentno = (lastValue + 1).ToString();

                                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));
                                var fileRefNo = (prefix + result + currentno);
                                fileupload.UploadFileRefNo = fileRefNo;
                                fileupload.UploadFileName = fileName;
                                fileupload.UploadedDate = DateTime.Now;
                                fileupload.IS_PROCESSED = "N";
                                fileupload.IS_ACTIVE = "Y";
                                fileupload.Status = "Incomplete";
                                //   fileupload.PROCESSED_DATE = DateTime.Now;
                                fileupload.UploadPath = pathForSave;
                                _db.MNT_VehicleListingUpload.AddObject(fileupload);
                                _db.SaveChanges();

                                int totalRecords = ds.Tables[0].Rows.Count;
                                int uploadSuccess = 0;

                                DataTable dtLog = new DataTable();
                                foreach (var column in columnNames)
                                {
                                    dtLog.Columns.Add(column);
                                }
                                dtLog.Columns.Add("REASON OF FAILURE");

                                SqlConnection con = new SqlConnection(strConnection);

                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    var VRegNo = ds.Tables[0].Rows[i]["BUS NO"].ToString().Trim();
                                    var VMake = ds.Tables[0].Rows[i]["MAKE"].ToString().Trim();
                                    var VModel = ds.Tables[0].Rows[i]["MODEL"].ToString().Trim();
                                    var VType = ds.Tables[0].Rows[i]["TYPE"].ToString().Trim();
                                    var VAircon = ds.Tables[0].Rows[i]["AIRCON"].ToString().Trim();
                                    var VAxle = ds.Tables[0].Rows[i]["AXLE"].ToString().Trim();
                                    var VChassisNo = ds.Tables[0].Rows[i]["CHASSIS NO"].ToString().Trim();

                                    if (string.IsNullOrEmpty(VRegNo) || string.IsNullOrEmpty(VChassisNo) || string.IsNullOrEmpty(VMake) || string.IsNullOrEmpty(VModel))
                                    {
                                        dtLog.Rows.Add(VRegNo, VChassisNo, VMake, VModel, VType, VAircon, VAxle, "There is no data for mandatory fields");
                                    }
                                    else
                                    {

                                        //Checking if already exists, than no use of inserting again.
                                        bool exists = _db.MNT_VehicleListingMaster.Any(row => row.VehicleRegNo.Equals(VRegNo) && row.VehicleMakeCode.Equals(VMake) && row.VehicleModelCode.Equals(VModel));

                                        if (!exists)
                                        {
                                            //Check if Class, Make, Model Mapping Exists With Excel Records...
                                            bool classExists = (from mClass in _db.MNT_Motor_Class.Where(v => v.VehicleClassCode == VehicleClass)
                                                                join mModel in _db.MNT_MOTOR_MODEL.Where(m => m.ModelName == VModel)
                                                                  on mClass.VehicleClassCode equals mModel.VehicleClassCode
                                                                join mMake in _db.MNT_Motor_Make.Where(m => m.MakeName == VMake)
                                                                      on mModel.MakeCode equals mMake.MakeCode
                                                                select mClass.VehicleClassCode
                                                                  ).Any();

                                            if (classExists)
                                            {
                                                try
                                                {

                                                    string query1 = "Insert into MNT_VehicleListingMaster(VehicleRegNo,VehicleClassCode,ModelDescription,UploadFileRefNo,VehicleMakeCode,VehicleModelCode,Type,Aircon,Axle,IS_ACTIVE,CreatedBy,CreatedDate) Values('" + VRegNo + "','" + VehicleClass + "','" + VChassisNo +
                                                               "','" + fileRefNo + "','" + VMake + "','" + VModel + "','" + VType + "','" + VAircon + "','" + VAxle + "','" + 'Y' + "','" + CreatedBy + "','" + sqlFormattedCreatedDate + "')";
                                                    con.Open();
                                                    SqlCommand cmd = new SqlCommand(query1, con);
                                                    int rowUpdated = cmd.ExecuteNonQuery();
                                                    con.Close();

                                                    if (rowUpdated > 0)
                                                        uploadSuccess++;
                                                }
                                                catch (Exception)
                                                {
                                                    con.Close();
                                                    dtLog.Rows.Add(VRegNo, VChassisNo, VMake, VModel, VType, VAircon, VAxle, "Special characters or data incorrect");
                                                }

                                            }
                                            else
                                            {
                                                dtLog.Rows.Add(VRegNo, VChassisNo, VMake, VModel, VType, VAircon, VAxle, "Class Make Model not mapped/exists in Master");
                                            }
                                        }
                                        else
                                        {
                                            dtLog.Rows.Add(VRegNo, VChassisNo, VMake, VModel, VType, VAircon, VAxle, "Record aleady exists or data incorrect");
                                        }
                                    }


                                    #region Comment

                                    //bool VMakeExist = _db.MNT_Motor_Make.Any(x => x.MakeName.Equals(VMake));
                                    //bool VModelExist = _db.MNT_MOTOR_MODEL.Any(x => x.ModelName.Equals(VModel));

                                    //if (VMakeExist && VModelExist)
                                    //{
                                    //    string query1 = "Insert into MNT_VehicleListingMaster(VehicleRegNo,VehicleClassCode,ModelDescription,UploadFileRefNo,VehicleMakeCode,VehicleModelCode,Type,Aircon,Axle,IS_ACTIVE,CreatedBy,CreatedDate) Values('" + VRegNo + "','" + VehicleClass + "','" + VChassisNo +
                                    //                    "','" + fileRefNo + "','" + VMake + "','" + VModel + "','" + VType + "','" + VAircon + "','" + VAxle + "','" + 'Y' + "','" + CreatedBy + "','" + sqlFormattedCreatedDate + "')";
                                    //    con.Open();
                                    //    SqlCommand cmd = new SqlCommand(query1, con);
                                    //    cmd.ExecuteNonQuery();
                                    //    con.Close();

                                    //}
                                    //else
                                    //{
                                    //    TempData["upload"] = "Entry failed.";
                                    //    return RedirectToAction("VehicleUploadIndex");
                                    //}

                                    //else
                                    //{
                                    //    string query1 = "update MNT_VehicleListingMaster set VehicleClassCode ='" + VehicleClass + "',VehicleMakeCode='" + ds.Tables[0].Rows[i]["MAKE"].ToString() + "',VehicleModelCode='" + ds.Tables[0].Rows[i]["MODEL"].ToString() + "',Type='" + ds.Tables[0].Rows[i]["TYPE"].ToString() + "',Aircon='" + ds.Tables[0].Rows[i]["AIRCON"].ToString() + "',Axle='" + ds.Tables[0].Rows[i]["AXLE"].ToString() + "' ,IS_ACTIVE='Y',ModifiedBy='" + ModifiedBy + "',ModifiedDate='" + sqlFormattedModifiedDate + "'  where VehicleRegNo = '" + VRegNo + "'";

                                    //    con.Open();
                                    //    SqlCommand cmd = new SqlCommand(query1, con);
                                    //    cmd.ExecuteNonQuery();
                                    //    con.Close();
                                    //}

                                    //if (VMakeExist && !VModelExist) {
                                    //    TempData["upload"] = "Model not matching.";
                                    //}
                                    //else if (!VMakeExist && VModelExist) {
                                    //    TempData["upload"] = "Make not matching.";
                                    //}
                                    //else if (!VMakeExist && VModelExist) {
                                    //    TempData["upload"] = "Make and model not matching.";
                                    //}

                                    #endregion
                                }

                                fileupload.IS_PROCESSED = "Y";
                                fileupload.TotalRecords = totalRecords;
                                fileupload.UplodedSuccess = uploadSuccess;
                                fileupload.UploadedFailed = totalRecords - uploadSuccess;
                                fileupload.Status = "Success";
                                fileupload.PROCESSED_DATE = DateTime.Now;
                                _db.SaveChanges();

                                if (totalRecords != uploadSuccess)
                                {
                                    string logPath = Path.Combine(pat, "Log_" + fileRefNo + ".xls");
                                    SaveDataTableToExcel(dtLog, logPath);

                                    TempData["upload"] = "Uploaded Successfully with some records failed. Uploaded File Ref. No: " + fileRefNo;
                                }
                                else
                                {
                                    TempData["upload"] = "Uploaded Successfully. Uploaded File Ref. No: " + fileRefNo;
                                }
                            }
                            else
                            {
                                TempData["upload"] = "File doesn't have any record.";
                            }

                            return RedirectToAction("VehicleUploadIndex");
                        }
                    }
                }
                else
                {
                    TempData["upload"] = "Please select file to upload.";
                }
            }

            catch (Exception ex)
            {
                TempData["upload"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);

                //   return View(model);
            }


            return RedirectToAction("VehicleUploadIndex");
        }

        public bool SaveDataTableToExcel(DataTable table, string savePath)
        {
            //open file
            StreamWriter wr = new StreamWriter(savePath, false, Encoding.Unicode);

            try
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    wr.Write(table.Columns[i].ToString().ToUpper() + "\t");
                }

                wr.WriteLine();

                //write rows to excel file
                for (int i = 0; i < (table.Rows.Count); i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (table.Rows[i][j] != null)
                        {
                            wr.Write("=\"" + Convert.ToString(table.Rows[i][j]) + "\"" + "\t");
                        }
                        else
                        {
                            wr.Write("\t");
                        }
                    }
                    //go to next line
                    wr.WriteLine();
                }
                //close file
                wr.Close();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);

                return false;
            }

            return true;
        }

        private bool IsAllColumnExist(DataTable tableNameToCheck, List<string> columnsNames)
        {
            bool iscolumnExist = true;
            try
            {
                if (null != tableNameToCheck && tableNameToCheck.Columns != null)
                {
                    foreach (string columnName in columnsNames)
                    {
                        if (!tableNameToCheck.Columns.Contains(columnName))
                        {
                            iscolumnExist = false;
                            break;
                        }
                    }
                }
                else
                {
                    iscolumnExist = false;
                }
            }
            catch (Exception ex)
            {

            }
            return iscolumnExist;
        }


        [HttpPost]
        public ActionResult VehicleEnquiryList(string Busno, string Chasisno, string VClass, string Make, string Model, string Type)
        {

            Busno = Request.Form["inputBusNo"].Trim();
            Chasisno = Request.Form["inputChasisNo"].Trim();
            Make = Request.Form["inputMake"].Trim();
            Model = Request.Form["inputModel"].Trim();
            Type = Request.Form["inputType"].Trim();
            VClass = Request.Form["inputVClass"].Trim();
            ViewBag.Bus = Busno;
            ViewBag.Chasis = Chasisno;
            ViewBag.make = Make;
            ViewBag.model = Model;
            ViewBag.Type = Type;
            string res = string.Empty;
            List<VehicleUploadModel> list = new List<VehicleUploadModel>();
            var results = _db.Proc_VehicleListingUploadSearch(Busno, Chasisno, VClass, Make, Model, Type).ToList();

            foreach (var data in results)
            {
                list.Add(new VehicleUploadModel()
                {
                    VehicleMasterId = data.VehicleMasterId,
                    BusNo = data.VehicleRegNo,
                    VehicleClassCode = data.VehicleClassDesc,
                    ChasisNo = data.ModelDescription,
                    Make = data.VehicleMakeCode,
                    Model = data.VehicleModelCode,
                    Type = data.Type,
                    Aircon = data.Aircon,
                    Axle = data.Axle
                });
            }
            foreach (var res1 in list)
            {
                res = res1.BusNo + "*" + res1.ChasisNo + "*" + res1.VehicleClassCode + "*" + res1.Make + "*" + res1.Model + "*" + res1.Type + "*" + res1.Aircon + "*" + res1.Axle + "*" + "*" + res1.VehicleMasterId + "*" + res;
            }
            TempData["searchres"] = res;
            TempData["Display"] = list;
            TempData["Bus"] = Busno;
            TempData["Chasis"] = Chasisno;
            TempData["make"] = Make;
            TempData["model"] = Model;
            TempData["Type"] = Type;
            TempData["VClass"] = VClass;
            return RedirectToAction("VehicleListingMaster", list);
        }


        public IQueryable<VehicleUploadModel> GetUplodedResult(string Bus, string Chasis, string VClass, string Make, string Model, string Type)
        {
            //   var searchResult = Enumerable.Empty<VehicleUploadModel>().AsQueryable(); ;
            Bus = Bus == null ? "" : Bus;
            Chasis = Chasis == null ? "" : Chasis;
            Make = Make == null ? "" : Make;
            Model = Model == null ? "" : Model;
            Type = Type == null ? "" : Type;
            VClass = VClass == null ? "" : VClass;



            //var SearchResult = (from x in _db.MNT_VehicleListingMaster
            //                    join c in _db.MNT_Motor_Class on x.VehicleClassCode equals c.VehicleClassCode into cx
            //                    from c in cx.DefaultIfEmpty()
            //                    join m in _db.MNT_Motor_Make on x.VehicleMakeCode equals m.MakeCode into mx
            //                    from m in mx.DefaultIfEmpty()
            //                    join d in _db.MNT_MOTOR_MODEL on x.VehicleModelCode equals d.ModelCode into dx
            //                    from d in dx.DefaultIfEmpty()
            //                    where x.VehicleRegNo.Contains(Bus) &&
            //                          c.VehicleClassDesc.Contains(VClass) &&
            //                          m.MakeName.Contains(Make) &&
            //                          d.ModelName.Contains(Model) &&
            //                          x.ModelDescription.Contains(Chasis) &&
            //                          x.Type.Contains(Type)

            //                    select new VehicleUploadModel
            //                    {
            //                        VehicleMasterId = x.VehicleMasterId,
            //                        BusNo = x.VehicleRegNo,
            //                        VehicleClassCode = c.VehicleClassDesc,
            //                        ChasisNo = x.ModelDescription,
            //                        Make =  m.MakeName,
            //                        Model = d.ModelName,
            //                        Type = x.Type,
            //                        Aircon = x.Aircon,
            //                        Axle = x.Axle
            //                    }).AsQueryable();


            var SearchResult = (from x in _db.MNT_VehicleListingMaster
                                join c in _db.MNT_Motor_Class on x.VehicleClassCode equals c.VehicleClassCode into cx
                                from c in cx.DefaultIfEmpty()
                                where x.VehicleRegNo.Contains(Bus) &&
                                      c.VehicleClassDesc.Contains(VClass) &&
                                      x.VehicleMakeCode.Contains(Make) &&
                                      x.VehicleModelCode.Contains(Model) &&
                                      x.ModelDescription.Contains(Chasis) &&
                                      x.Type.Contains(Type)

                                select new VehicleUploadModel
                                {
                                    VehicleMasterId = x.VehicleMasterId,
                                    BusNo = x.VehicleRegNo,
                                    VehicleClassCode = c.VehicleClassDesc,
                                    ChasisNo = x.ModelDescription,
                                    Make = x.VehicleMakeCode,
                                    Model = x.VehicleModelCode,
                                    Type = x.Type,
                                    Aircon = x.Aircon,
                                    Axle = x.Axle
                                }).AsQueryable();
            return SearchResult;


        }

        public ActionResult VehicleUploadListAll(int? Id)
        {
            MCASEntities db = new MCASEntities();
            var model = new VehicleUploadModel();
            string strMake = string.Empty;
            string strModel = string.Empty;
            string strClass = string.Empty;
            try
            {
                if (Id.HasValue)
                {
                    var VehicleId = (from x in _db.MNT_VehicleListingMaster where x.VehicleMasterId == Id select x).FirstOrDefault();
                    model.VehicleMasterId = VehicleId.VehicleMasterId;
                    TempData["ID"] = model.VehicleMasterId;
                    model.BusNo = VehicleId.VehicleRegNo;
                    model.ChasisNo = VehicleId.ModelDescription;
                    model.ModelDescription = VehicleId.ModelDescription; // Chassis No

                    model.VehicleClassCode = VehicleId.VehicleClassCode;
                    TempData["VehicleClass"] = model.VehicleClassCode;
                    model.VehicleClasslist = LoadVehicleClass();


                    model.Make = VehicleId.VehicleMakeCode;
                    TempData["Vehiclemake"] = model.Make;
                    model.vehicleMakelist = LoadVehicleMake();
                    model.Hgetval = model.Make;

                    model.Model = VehicleId.VehicleModelCode;
                    TempData["VehicleModel"] = model.Model;
                    model.VehicleModellist = LoadVehicleModel();
                    model.Hgetval1 = model.Model;

                    model.Type = VehicleId.Type;
                    model.Aircon = VehicleId.Aircon;
                    model.Axle = VehicleId.Axle;
                    model.CreatedBy = VehicleId.CreatedBy;
                    model.ModifiedBy = VehicleId.ModifiedBy;
                    if (VehicleId.CreatedDate != null)
                    {
                        model.CreatedOn = (DateTime)VehicleId.CreatedDate;
                    }
                    model.ModifiedOn = VehicleId.ModifiedDate;
                    return View(model);
                }
                model.VehicleClasslist = LoadVehicleClass();
                model.vehicleMakelist = LoadVehicleMake();
                model.VehicleModellist = LoadVehicleModel();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult VehicleUploadListAll(VehicleUploadModel model)
        {
            try
            {
                model.VehicleClasslist = LoadVehicleClass();
                model.vehicleMakelist = LoadVehicleMake();
                model.VehicleModellist = LoadVehicleModel();
                ModelState.Clear();

                if (ModelState.IsValid)
                {
                    var hits = (from x in _db.MNT_VehicleListingMaster where x.VehicleMasterId == model.VehicleMasterId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        var busNo = (from x in _db.MNT_VehicleListingMaster where x.VehicleRegNo == model.BusNo select x.VehicleRegNo).FirstOrDefault();
                        if (busNo != null)
                        {
                            TempData["result"] = "Duplicate Bus No.";
                        }
                        else
                        {
                            model.CreatedBy = LoggedInUserName;
                            var list = model.Update();
                            TempData["result"] = "Record Saved Successfully.";
                        }
                    }
                    else
                    {
                        var busNo = (from x in _db.MNT_VehicleListingMaster where x.VehicleRegNo == model.BusNo && x.VehicleMasterId!=hits.VehicleMasterId select x.VehicleRegNo).FirstOrDefault();
                       if (busNo!=null)
                        {
                            TempData["result"] = "Duplicate Bus No.";
                        }
                        else
                        {
                            model.ModifiedBy = LoggedInUserName;
                            var list = model.Update();
                            TempData["result"] = "Record Updated Successfully.";
                        }
                    }
                    TempData["Vehiclemake"] = model.Hgetval;
                    TempData["VehicleModel"] = model.Hgetval1;
                    return View(model);
                }
                else
                {

                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }
            return View(model);

        }

        [HttpPost]
        public JsonResult UpdateVehicle(string Vehicleid, string BusNo, string ChasisNo, string Make, string Model, string Type, string Aircon, string Axle)
        {
            var VRegNo = BusNo;
            var VMake = Make;
            var VModel = Model;
            if (Vehicleid != "")
            {
                int VehId = Convert.ToInt32(Vehicleid);

                bool exists = _db.MNT_VehicleListingMaster.Any(row => row.VehicleRegNo.Equals(VRegNo) && row.VehicleMakeCode.Equals(VMake) && row.VehicleModelCode.Equals(VModel));
                if (exists)
                {
                    return Json("D");
                }
                var sp = _db.Proc_UpdateVehicleListing(VehId, BusNo, ChasisNo, Make, Model, Type, Aircon, Axle);
                return Json("U");
            }
            else
            {

                bool exists = _db.MNT_VehicleListingMaster.Any(row => row.VehicleRegNo.Equals(VRegNo) && row.VehicleMakeCode.Equals(VMake) && row.VehicleModelCode.Equals(VModel));
                if (exists)
                {
                    return Json("D");
                }
                var sp = _db.Proc_UpdateVehicleListing(0, BusNo, ChasisNo, Make, Model, Type, Aircon, Axle);
                return Json("I");
            }

            // return Json("T");
        }


        public JsonResult GetMakeAndModelList(string cat)
        {
            MCASEntities db = new MCASEntities();
            List<MakeAndModel> sgf = new List<MakeAndModel>();
            if (cat == "" || cat == null)
            {
                sgf = null;
            }
            else
            {
                sgf.Add(new MakeAndModel()
                {
                    MakeName = "[Select...]",
                    MakeCode = "-1"
                });
                var query = (from Vmodel in db.MNT_MOTOR_MODEL
                             join Vmake in db.MNT_Motor_Make on Vmodel.MakeCode equals Vmake.MakeCode
                             where Vmodel.VehicleClassCode == cat
                             select new
                             {
                                 VMakeCode = Vmake.MakeCode,
                                 VMakeName = Vmake.MakeName
                             }).Distinct();
                foreach (var Info in query)
                {
                    sgf.Add(new MakeAndModel()
                    {
                        MakeCode = Info.VMakeCode,
                        MakeName = Info.VMakeName
                    });
                }

            }

            return Json(sgf);
        }
        public JsonResult GetModelList(string cat)
        {
            MCASEntities db = new MCASEntities();
            List<Model> sgf = new List<Model>();
            if (cat == "" || cat == null)
            {
                sgf = null;
            }
            else
            {
                sgf.Add(new Model()
                {
                    ModelName = "[Select...]",
                    ModelCode = "-1"
                });
                var query = (from Vmodel in db.MNT_MOTOR_MODEL
                             join Vmake in db.MNT_Motor_Make on Vmodel.MakeCode equals Vmake.MakeCode
                             where Vmodel.VehicleClassCode == cat
                             select new
                             {
                                 VModelName = Vmodel.ModelName,
                                 VModelCode = Vmodel.ModelCode
                             }).Distinct();
                foreach (var Info in query)
                {
                    sgf.Add(new Model()
                    {
                        ModelCode = Info.VModelCode,
                        ModelName = Info.VModelName
                    });
                }

            }

            return Json(sgf);
        }

        public JsonResult GetModelListBasedOnMakeAndClass(string cat, string classCode)
        {
            MCASEntities db = new MCASEntities();
            List<Model> sgf = new List<Model>();
            if (cat == "" || cat == null)
            {
                sgf = null;
            }
            else
            {
                sgf.Add(new Model()
                {
                    ModelName = "[Select...]",
                    ModelCode = "-1"
                });
                var query = (from Vmodel in db.MNT_MOTOR_MODEL.Where(t => t.MakeCode.Equals(cat) && t.VehicleClassCode.Equals(classCode))
                             select new
                             {
                                 VModelName = Vmodel.ModelName,
                                 VModelCode = Vmodel.ModelCode
                             }).Distinct();
                foreach (var Info in query)
                {
                    sgf.Add(new Model()
                    {
                        ModelCode = Info.VModelCode,
                        ModelName = Info.VModelName
                    });
                }

            }

            return Json(sgf);
        }

        public JsonResult MakeAndModelListBasedonClass(string cat)
        {
            MCASEntities db = new MCASEntities();
            List<MakeAndModel> sgf = new List<MakeAndModel>();
            if (cat == "" || cat == null)
            {
                sgf = null;
            }
            else
            {
                sgf.Add(new MakeAndModel()
                {
                    MakeName = "[Select...]",
                    MakeCode = "-1"
                });
                var query = (from Vmodel in db.MNT_MOTOR_MODEL
                             join Vmake in db.MNT_Motor_Make on Vmodel.MakeCode equals Vmake.MakeCode
                             where Vmodel.VehicleClassCode == cat
                             select new
                             {
                                 VMakeCode = Vmake.MakeCode,
                                 VMakeName = Vmake.MakeName
                             }).Distinct();
                foreach (var Info in query)
                {
                    sgf.Add(new MakeAndModel()
                    {
                        MakeCode = Info.VMakeCode,
                        MakeName = Info.VMakeName
                    });
                }
            }


            return Json(sgf);
        }


        public JsonResult ModelListBasedonClass(string cat)
        {
            MCASEntities db = new MCASEntities();
            List<Model> sgf = new List<Model>();
            if (cat == "" || cat == null)
            {
                sgf = null;
            }
            else
            {
                sgf.Add(new Model()
                {
                    ModelName = "[Select...]",
                    ModelCode = "-1"
                });
                var query = (from Vmodel in db.MNT_MOTOR_MODEL
                             join Vmake in db.MNT_Motor_Make on Vmodel.MakeCode equals Vmake.MakeCode
                             where Vmodel.VehicleClassCode == cat
                             select new
                             {
                                 VModelName = Vmodel.ModelName,
                                 VModelCode = Vmodel.ModelCode
                             }).Distinct();
                foreach (var Info in query)
                {
                    sgf.Add(new Model()
                    {
                        ModelName = Info.VModelName,
                        ModelCode = Info.VModelCode
                    });
                }
            }
            return Json(sgf);
        }


        #endregion

        #region Download File

        public ActionResult FileDownLoader()
        {
            MCASEntities db = new MCASEntities();
            string path = "";
            string UploadFolderPath = "";
            //string format = "CSV";
            //format = Request.QueryString["Fileformat"] != null ? Request.QueryString["Fileformat"].ToString() : format;
            string fileRefNo = Request.QueryString["FileRefNo"] != null ? Request.QueryString["FileRefNo"].ToString() : "";
            fileRefNo = "Log_" + fileRefNo;

            string fileServerpath = (from l in db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
            if (String.IsNullOrEmpty(fileServerpath))
            {
                ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                return null;
            }

            UploadFolderPath = ConfigurationManager.AppSettings["UploadFolder"].ToString();
            if (String.IsNullOrEmpty(UploadFolderPath))
            {
                ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                return null;
            }


            path = fileServerpath.TrimEnd('\\') + "\\" + UploadFolderPath.TrimStart('\\') + "\\VechileReg"; ;

            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();

            EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
            if (!objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
            {
                ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                return null;
            }


            //string pat = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + "Uploads/VechileReg");
            string filePath = Path.Combine(path, Path.GetFileName(fileRefNo + ".xls"));

            if (System.IO.File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(fileInfo.FullName);
                Response.End();
            }

            return View();
        }

        #endregion

        #region Depot Master

        [HttpGet]
        public ActionResult DepotMasterIndex()
        {
            List<DepotMasterModel> list = new List<DepotMasterModel>();
            try
            {
                list = DepotMasterModel.FetchDepotMaster();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Depot master " + list + " for Depot master." + list + ".");
                addInfo.Add("entity_type", "Depot master");
                PublishException(ex, addInfo, 0, "Depot master" + list);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult DepotMasterIndex(string DepotCode, string DepotName)
        {
            DepotCode = Request.Form["inputDepotCode"].Trim();
            DepotName = Request.Form["inputDepotReference"].Trim();
            ViewBag.depocode = DepotCode;
            ViewBag.deporeference = DepotName;
            List<DepotMasterModel> Depotlist = new List<DepotMasterModel>();
            try
            {
                if (DepotCode != "" || DepotName != "")
                {
                    List<DepotMasterModel> list = new List<DepotMasterModel>();
                    list = GetDepotMasterSearchResult(DepotCode, DepotName).ToList();
                    return View(list);
                }
                else if (DepotCode != "" && DepotName != "")
                {
                    List<DepotMasterModel> list1 = new List<DepotMasterModel>();
                    list1 = GetDepotMasterSearchResult(DepotCode, DepotName).ToList();
                    return View(list1);
                }
                else
                {

                    Depotlist = DepotMasterModel.FetchDepotMaster();
                    return View(Depotlist);
                }
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for Depot master " + DepotName + " for Search result Depot master." + DepotName + ".");
                addInfo.Add("entity_type", "Depot master");
                PublishException(ex, addInfo, 0, "Depot master" + DepotName);
                return View(Depotlist);
            }
        }

        public IQueryable<DepotMasterModel> GetDepotMasterSearchResult(string DepotCode, string DepotName)
        {
            var searchResult = (from depot in _db.MNT_DepotMaster
                                where
                                depot.CompanyName.Contains(DepotName) &&
                                depot.DepotCode.Contains(DepotCode)
                                select depot).ToList().Select(item => new DepotMasterModel
                                {
                                    DepotCode = item.DepotCode,
                                    DepotAddress = new AddressModel
                                    {
                                        Address1 = item.Address1,
                                        OffNo1 = item.TelephoneOff,
                                        EmailAddress1 = item.Email,
                                        InsurerName = item.CompanyName
                                    },
                                    DepotId = item.DepotId
                                }).AsQueryable();

            return searchResult;
        }

        [HttpGet]
        public ActionResult DepotMasterEditor(int? DepotId)
        {
            var depot = new DepotMasterModel();
            DepotMasterModel model = new DepotMasterModel();
            try
            {
                if (DepotId.HasValue)
                {
                    var depo = (from lt in _db.MNT_DepotMaster where lt.DepotId == DepotId select lt).FirstOrDefault();

                    model.DepotCode = depo.DepotCode;
                    model.DepotReference = depo.DepotReference;
                    model.DepotAddress = new AddressModel()
                    {
                        InsurerName = depo.CompanyName,
                        Address1 = depo.Address1,
                        Address2 = depo.Address2,
                        Address3 = depo.Address3,
                        OffNo1 = depo.TelephoneOff,
                        MobileNo1 = depo.MobileNo,
                        EmailAddress1 = depo.Email,
                        Fax1 = depo.Fax,
                        FirstContactPersonName = depo.PersonInCharge,
                        PostalCode = depo.PostalCode,
                        City = depo.City,
                        Country = depo.Country,
                        State = depo.State,
                        SecondContactPersonName = depo.PersonInCharge,
                        EmailAddress2 = depo.EmailAddress2,
                        OffNo2 = depo.OffNo2,
                        MobileNo2 = depo.MobileNo2,
                        Fax2 = depo.Fax2,
                        InsurerType = depo.WorkShopType,
                        EffectiveFromDate = depo.EffectiveFrom,
                        Effectiveto = depo.EffectiveTo,
                        Status = depo.Status,
                        Remarks = depo.Remarks


                    };
                    if (depo.Status == "1")
                    {
                        model.DepotAddress.Status = "Active";
                    }
                    else
                    {
                        model.DepotAddress.Status = "InActive";
                    }
                    model.DepotAddress.Insurerlist = LoadLookUpValue("InsurerType");
                    model.DepotAddress.Statuslist = LoadLookUpValue("STATUS");
                    model.DepotAddress.citylist = LoadCity();
                    model.DepotAddress.usercountrylist = LoadCountry();
                    model.DepotId = DepotId;

                    model.CreatedBy = depo.CreatedBy == null ? " " : depo.CreatedBy;
                    if (depo.CreatedDate != null)
                        model.CreatedOn = (DateTime)depo.CreatedDate;
                    else
                        model.CreatedOn = DateTime.MinValue;
                    model.ModifiedBy = depo.ModifiedBy == null ? " " : depo.ModifiedBy;
                    model.ModifiedOn = depo.ModifiedDate;

                    return View(model);

                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Depot master  " + model.DepotId + " for Depot master." + model.DepotCode + ".");
                addInfo.Add("entity_type", "Depot master");
                PublishException(ex, addInfo, 0, "Depot master" + model.DepotId);
            }
            depot.DepotId = DepotId;
            depot.DepotAddress.Insurerlist = LoadLookUpValue("InsurerType");
            depot.DepotAddress.Statuslist = LoadLookUpValue("STATUS");
            depot.DepotAddress.citylist = LoadCity();
            depot.DepotAddress.usercountrylist = LoadCountry();
            return View(depot);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DepotMasterEditor(DepotMasterModel model, AddressModel addModel)
        {
            TempData["result"] = "";
            TempData["ErrorMsg"] = "";
            TempData["display"] = "";
            string email = "";
            addModel.Insurerlist = LoadLookUpValue("InsurerType");
            addModel.Statuslist = LoadLookUpValue("STATUS");
            addModel.citylist = LoadCity();
            addModel.usercountrylist = LoadCountry();



            try
            {
                model.DepotAddress = addModel;
                email = addModel.EmailAddress1;


                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_DepotMaster where x.DepotId == model.DepotId select x).FirstOrDefault();

                    if (hits == null)
                    {

                        model.CreatedBy = LoggedInUserName;
                        var depo = model.DepotMasterUpdate();
                        TempData["result"] = "Records Saved Successfully.";
                        TempData["display"] = depo.DepotCode;
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var depo = model.DepotMasterUpdate();
                        TempData["result"] = "Records Updated Successfully.";

                    }

                }
                else
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving depot master " + model.DepotId + " for depot." + model.DepotCode + ".");
                addInfo.Add("entity_type", "depot master");
                PublishException(ex, addInfo, 0, "depot master" + model.DepotCode);
            }
            return View(model);
        }



        [HttpGet]
        public JsonResult CheckUsername(string username)
        {
            var allDepotrefrence = _db.MNT_DepotMaster.ToList();

            var isDuplicate = false;

            foreach (var depot in allDepotrefrence)
            {
                if (depot.DepotReference == username)
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckDepotName(string DepotName, string DepoName)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_DepotMaster where t.CompanyName == DepotName orderby t.CompanyName descending select t.CompanyName).Take(1)).FirstOrDefault();
            if ((num != null) && (DepoName == null || DepoName == ""))
            {
                result = true;
            }
            else if ((num != null && num.ToLower() != DepoName.ToLower()))
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

        #region ClaimOfficerDuty

        [HttpGet]
        public ActionResult ClaimOfficerDutyIndex()
        {
            List<ClaimOfficerModel> list = new List<ClaimOfficerModel>();
            list = ClaimOfficerModel.FetchClaimOfficer();
            return View(list);
        }

        [HttpPost]
        public ActionResult ClaimOfficerDutyIndex(string Depart, string DeptName)
        {
            Depart = ((Request.Form["ddlDeptCode"] == null) ? "" : Request.Form["ddlDeptCode"]);
            DeptName = (Request.Form["inputName"]).Trim();
            ViewBag.department = Depart;
            ViewBag.deptname = DeptName;
            List<ClaimOfficerModel> list = new List<ClaimOfficerModel>();
            list = GetUserResult(Depart, DeptName).ToList();
            return View(list);
        }

        public JsonResult FillDeptCodeList()
        {
            var returnData = LoadDepts();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillUserGroup()
        {
            var returnData = FillUserGroupList();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClaimOfficerDutyEditor(int? TranId)
        {
            var buscaptain = new ClaimOfficerDutyModel();
            buscaptain.DeptGroupList = LoadDepts();
            buscaptain.UserGroupList = FillUserGroupList();
            if (TranId.HasValue)
            {
                var buscaptainlist = (from d in _db.MNT_ClaimOfficerDetail where d.TranId == TranId select d).FirstOrDefault();
                ClaimOfficerDutyModel objmodel = new ClaimOfficerDutyModel();
                objmodel.DeptGroupList = LoadDepts();
                objmodel.UserGroupList = FillUserGroupList();
                objmodel.UserGroup = buscaptainlist.UserGroup;
                objmodel.ClaimOfficerName = buscaptainlist.ClaimentOfficerName;
                objmodel.ddlDeptCode = buscaptainlist.Department;
                objmodel.LastAssignmentdate = buscaptainlist.LastAssignmentDate;
                objmodel.Type = buscaptainlist.Type;
                objmodel.ClaimNumber = buscaptainlist.ClaimNo;
                return View(objmodel);
            }
            return View(buscaptain);
        }

        [HttpPost]
        public ActionResult ClaimOfficerDutyEditor(ClaimOfficerDutyModel model)
        {
            model.DeptGroupList = LoadDepts();
            model.UserGroupList = FillUserGroupList();
            try
            {
                if (ModelState.IsValid)
                {
                    var hits = (from x in _db.MNT_ClaimOfficerDetail where x.TranId == model.TranId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        ModelState.Clear();
                        var list = model.Update();
                        TempData["result"] = "Records Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        ModelState.Clear();
                        var list = model.Update();
                        TempData["result"] = "Records Updated Successfully.";
                    }
                }
                else
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
            }
            return View(model);
        }

        public List<ClaimOfficerModel> GetUserResult(string Depart, string DeptName)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimOfficerModel>();
            var ClaimOfficerDetails = (from x in _db.MNT_ClaimOfficerDetail where x.Department.Contains(Depart) && x.ClaimentOfficerName.Contains(DeptName) select x);
            if (ClaimOfficerDetails.Any())
            {
                foreach (var data in ClaimOfficerDetails)
                {
                    var Dept = (from p in _db.MNT_Department where p.DeptCode == data.Department select p.DeptName).FirstOrDefault();
                    item.Add(new ClaimOfficerModel() { TranId = data.TranId, UserGroup = data.UserGroup, ClaimOfficerName = data.ClaimentOfficerName, ddlDeptCode = Dept, LastAssignmentdate = data.LastAssignmentDate, Type = data.Type, ClaimNumber = data.ClaimNo });
                }
            }
            return item;
        }

        #endregion


        #region LOU Master
        public ActionResult LOUIndex()
        {
            List<LOUModel> list = new List<LOUModel>();

            try
            {
                list = list = LOUModel.FetchLOUDetails();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving LOU master " + list + " for LOU." + list + ".");
                addInfo.Add("entity_type", "LOU master");
                PublishException(ex, addInfo, 0, "LOU master" + list);
            }
            return View(list);

        }
        [HttpPost]
        public ActionResult LOUIndex(string LouRate, DateTime? EffectiveDate)
        {

            LouRate = Request.Form["LouRate"].Trim();

            if (Request.Form["EffectiveDate"] == null || Request.Form["EffectiveDate"] == "")
            {
                EffectiveDate = null;
            }

            else
            {
                EffectiveDate = Convert.ToDateTime(Request.Form["EffectiveDate"].Trim());

            }
            List<LOUModel> list = new List<LOUModel>();
            try
            {
                if (LouRate != "" || EffectiveDate != null)
                {
                    List<LOUModel> _vh = new List<LOUModel>();
                    _vh = GetLOUSearchResult(LouRate, EffectiveDate).ToList();
                    TempData["LouRate"] = LouRate;
                    TempData["EffectiveDate"] = EffectiveDate;
                    return View(_vh);
                }
                else
                {

                    list = LOUModel.FetchLOUDetails();
                    return View(list);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for LOU master " + LouRate + " for Search result Type of LOU master." + LouRate + ".");
                addInfo.Add("entity_type", "LOU master");
                PublishException(ex, addInfo, 0, "LOU master" + LouRate);

            }

            return View(list);

        }


        public List<LOUModel> GetLOUSearchResult(string lourate, DateTime? Effectivedate)
        {
            List<LOUModel> items = new List<LOUModel>();
            try
            {
                var Lourate = lourate == null ? "" : lourate;
                var Effectivedate1 = Effectivedate == null ? "" : Effectivedate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);


                var Loulist = _db.Proc_GetLouMasterList(Lourate, Effectivedate1).ToList();
                items = (from item in Loulist
                         select new LOUModel()
                         {
                             Id = item.Id,
                             LouRate = item.LouRate,
                             EffectiveDate = item.EffectiveDate,
                             IsActive = item.IsActive
                         }
                        ).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }
            return items;
        }
        public ActionResult LOUEditor(int? Id)
        {
            MCASEntities obj = new MCASEntities();
            LOUModel objmodel = new LOUModel();
            try
            {
                if (Id.HasValue)
                {
                    var LOURatelist = (from d in _db.MNT_LOU_MASTER where d.Id == Id select d).FirstOrDefault();
                    objmodel.Id = LOURatelist.Id;
                    objmodel.LouRate = Convert.ToDecimal(LOURatelist.LouRate);
                    objmodel.EffectiveDate = LOURatelist.EffectiveDate;
                    objmodel.CreatedBy = LOURatelist.CreatedBy == null ? " " : LOURatelist.CreatedBy;
                    objmodel.CreatedOn = LOURatelist.CreatedDate != null ? (DateTime)LOURatelist.CreatedDate : DateTime.MinValue;
                    objmodel.ModifiedBy = LOURatelist.ModifiedBy == null ? " " : LOURatelist.ModifiedBy;
                    objmodel.ModifiedOn = LOURatelist.ModifiedDate;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving result for LOU Master " + Id + " for  result Type of LOU Master." + Id + ".");
                addInfo.Add("entity_type", "LOU Master");
                PublishException(ex, addInfo, 0, "LOU Master" + Id);
            }
            finally
            {
                obj.Dispose();
            }
            return View(objmodel);
        }
        [HttpPost]
        public ActionResult LOUEditor(LOUModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_LOU_MASTER where x.Id == model.Id select x).FirstOrDefault();
                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Updated Successfully.";
                    }
                }
                else
                {

                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving Type of loss " + model.Id + " for LOU Master." + model.Id + ".");
                addInfo.Add("entity_type", "LOU Master");
                PublishException(ex, addInfo, 0, "Type of loss" + model.Id);
            }
            return View(model);
        }
        #endregion

        #region Interchange
        public ActionResult InterChangeIndex()
        {
            List<InterChangeModel> list = new List<InterChangeModel>();
            try
            {
                list = InterChangeModel.FetchInterChangeDetails();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving interchange list " + list + " for Interchange." + list + ".");
                addInfo.Add("entity_type", "interchange list");
                PublishException(ex, addInfo, 0, "interchange list" + list);
                return View(list);
            }
            return View(list);

        }

        [HttpPost]
        public ActionResult InterChangeIndex(string interchangeName)
        {
            interchangeName = Request.Form["inputInterchange"].Trim();
            ViewBag.interchange = interchangeName;
            List<InterChangeModel> list = new List<InterChangeModel>();
            try
            {
                list = GetInterChangeResult(interchangeName).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for interchange master " + interchangeName + " for Search interchange master." + interchangeName + ".");
                addInfo.Add("entity_type", "interchange master");
                PublishException(ex, addInfo, 0, "interchange Master" + interchangeName);
                return View(list);
            }
            return View(list);
        }

        public List<InterChangeModel> GetInterChangeResult(string InterChange)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<InterChangeModel>();
            var InterchangeList = (from e in _db.MNT_InterChange
                                   join d in _db.MNT_Country on e.Country equals d.CountryShortCode
                                   where e.InterchangeName.Contains(InterChange)
                                   select new { e.Id, e.InterchangeName, e.EffectiveFrom, e.EffectiveTo, e.Status, d.CountryName }).ToList();
            if (InterchangeList.Any())
            {
                foreach (var data in InterchangeList)
                {
                    item.Add(new InterChangeModel()
                    {
                        Id = data.Id,
                        InterchangeName = data.InterchangeName,
                        EffectiveFrom = data.EffectiveFrom,
                        EffectiveTo = data.EffectiveTo,
                        Status = data.Status,
                        Country = data.CountryName
                    });
                }
            }
            _db.Dispose();
            return item;
        }

        public ActionResult InterChangeEditor(int? Id)
        {
            MCASEntities obj = new MCASEntities();
            var model = new InterChangeModel();
            InterChangeModel objmodel = new InterChangeModel();
            try
            {
                MNT_InterChange MNTInterchange;

                MNTInterchange = obj.MNT_InterChange.Where(x => x.Id == Id).FirstOrDefault();
                if (Id.HasValue)
                {
                    var Interchangelist = (from d in _db.MNT_InterChange where d.Id == Id select d).FirstOrDefault();

                    objmodel.InterchangeName = Interchangelist.InterchangeName;
                    objmodel.Address1 = Interchangelist.Address1;
                    objmodel.Address2 = Interchangelist.Address2;
                    objmodel.Address3 = Interchangelist.Address3;
                    objmodel.City = Interchangelist.City;
                    objmodel.State = Interchangelist.State;
                    objmodel.Country = Interchangelist.Country;
                    objmodel.PostalCode = Interchangelist.PostalCode;
                    objmodel.Status = Interchangelist.Status;
                    if (Interchangelist.Status == "1")
                    {
                        objmodel.Status = "Active";

                    }
                    else
                    {
                        objmodel.Status = "InActive";
                    }
                    objmodel.Statuslist = LoadLookUpValue("STATUS");
                    objmodel.Remarks = Interchangelist.Remarks;
                    objmodel.EffectiveFrom = Interchangelist.EffectiveFrom;
                    objmodel.EffectiveTo = Interchangelist.EffectiveTo;
                    objmodel.usercountrylist = LoadCountry();
                    objmodel.Id = Id;
                    objmodel.CreatedBy = Interchangelist.CreatedBy == null ? " " : Interchangelist.CreatedBy;
                    if (Interchangelist.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)Interchangelist.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = Interchangelist.ModifiedBy == null ? " " : Interchangelist.ModifiedBy;
                    objmodel.ModifiedOn = Interchangelist.ModifiedDate;
                    return View(objmodel);
                }
                model.Id = Id;
                model.Statuslist = LoadLookUpValue("STATUS");
                model.usercountrylist = LoadCountry();
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving interchange " + objmodel.Id + " for interchange master." + objmodel.InterchangeName + ".");
                addInfo.Add("entity_type", "interchange Master");
                PublishException(ex, addInfo, 0, "interchange Master" + objmodel.Id);
                return View(model);
            }
            // return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InterChangeEditor(InterChangeModel model)
        {
            string strStatus = string.Empty;
            model.Statuslist = LoadLookUpValue("STATUS");
            model.usercountrylist = LoadCountry();
            strStatus = model.Status == "Active" ? "1" : "0";
            try
            {

                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var hits = (from x in _db.MNT_InterChange where x.Id == model.Id select x).FirstOrDefault();
                    if (hits == null)
                    {

                        model.Status = strStatus;
                        model.CreatedBy = LoggedInUserName;
                        var list = model.Update();
                        model.Status = strStatus == "1" ? "Active" : "Inactive";
                        TempData["result"] = "Record Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        model.Status = strStatus;
                        var list = model.Update();
                        model.Status = strStatus == "1" ? "Active" : "Inactive";
                        TempData["result"] = "Record Updated Successfully.";
                    }
                }
                else
                {

                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving interchange master " + model.Id + " for interchange." + model.InterchangeName + ".");
                addInfo.Add("entity_type", "interchange master");
                PublishException(ex, addInfo, 0, "interchange master" + model.InterchangeName);
            }
            return View(model);
        }
        #endregion


        #region Deductible

        public ActionResult DeductibleNewEditor(int? DeductibleId)
        {
            MCASEntities _db = new MCASEntities();
            var model = new DeductibleModel();

            try
            {
                MNT_Deductible MNTDeductible;
                MNTDeductible = _db.MNT_Deductible.Where(x => x.DeductibleId == DeductibleId).FirstOrDefault();
                if (DeductibleId.HasValue)
                {

                    var Deductiblelist = (from d in _db.MNT_Deductible where d.DeductibleId == DeductibleId select d).FirstOrDefault();
                    DeductibleModel objmodel = new DeductibleModel();
                    objmodel.EffectiveTo = Deductiblelist.EffectiveTo;
                    DateTime Effto = Convert.ToDateTime(Deductiblelist.EffectiveTo);
                    Effto = Effto.AddDays(1);
                    objmodel.NewEffectiveFrom = Effto;
                    objmodel.DeductibleId = DeductibleId;
                    return View(objmodel);

                }

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }


            return View(model);

        }
        [HttpGet]
        public ActionResult DeductibleIndex()
        {
            List<DeductibleModel> list = new List<DeductibleModel>();
            TempData["OrganizationName"] = DeductibleModel.FetchOrgCategoryName();
            TempData["TopId"] = DeductibleModel.GetDeductableAmountTopId();
            list = DeductibleModel.GetOrgResult("", "");
            return View(list);
        }

        [HttpPost]
        public ActionResult DeductibleIndex(string orgCategory11, string CategoryName)
        {
            List<DeductibleModel> list = new List<DeductibleModel>();
            try
            {
                orgCategory11 = Request.Form["ddlCategory"] == null ? "" : Convert.ToString(Request.Form["ddlCategory"]);
                CategoryName = Request.Form["listOrgName"] == null ? "" : Convert.ToString(Request.Form["listOrgName"]);
                ViewBag.orgCategory = orgCategory11;
                ViewBag.OrgCategoryName = CategoryName;
                list = DeductibleModel.GetOrgResult(orgCategory11, CategoryName).ToList();
                TempData["TopId"] = DeductibleModel.GetDeductableAmountTopId();
                return View(list);
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(list);
            }

        }

        [HttpGet]
        public ActionResult DeductibleEditor(int? DeductibleId)
        {
            MCASEntities _db = new MCASEntities();
            var model = new DeductibleModel();
            string strOrgName1 = string.Empty;


            //model.Categorylist = LoadLookUpValue("ORGCategory");
            try
            {
                MNT_Deductible MNTDeductible;
                MNTDeductible = _db.MNT_Deductible.Where(x => x.DeductibleId == DeductibleId).FirstOrDefault();
                if (DeductibleId.HasValue)
                {
                    model.Categorylist = LoadLookUpValue("ORGCategory");
                    var Deductiblelist = (from d in _db.MNT_Deductible where d.DeductibleId == DeductibleId select d).FirstOrDefault();
                    DeductibleModel objmodel = new DeductibleModel();
                    objmodel.OrgCategory = Deductiblelist.OrgCategory;
                    TempData["orgCategory"] = objmodel.OrgCategory;
                    objmodel.Categorylist = LoadLookUpValue("ORGCategory");
                    objmodel.OrgCategoryName = Deductiblelist.OrgCategoryName;
                    strOrgName1 = objmodel.OrgCategoryName;
                    string str = DeductibleModel.FetchOrgCategoryName(strOrgName1);
                    TempData["orgCategoryname"] = objmodel.OrgCategoryName;
                    ViewBag.OrgCategoryName = objmodel.OrgCategoryName;
                    objmodel.DeductibleAmt = Deductiblelist.DeductibleAmt;
                    objmodel.EffectiveFrom = Deductiblelist.EffectiveFrom;
                    TempData["EffectiveFrom"] = Convert.ToDateTime(objmodel.EffectiveFrom);
                    objmodel.EffectiveTo = Deductiblelist.EffectiveTo;
                    TempData["EffectiveTo"] = Convert.ToDateTime(objmodel.EffectiveTo);
                    objmodel.DeductibleId = DeductibleId;

                    objmodel.CreatedBy = Deductiblelist.CreatedBy == null ? " " : Deductiblelist.CreatedBy;
                    if (Deductiblelist.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)Deductiblelist.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = Deductiblelist.ModifiedBy == null ? " " : Deductiblelist.ModifiedBy;
                    objmodel.ModifiedOn = Deductiblelist.ModifiedDate;


                    return View(objmodel);

                }
                else
                {
                    model.Categorylist = LoadOrganizationCategory("ORGCategory");
                }

            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult DeductibleEditor(DeductibleModel model)
        {
            TempData["result"] = "";
            string strcategoryName = string.Empty;
            try
            {
                model.Categorylist = LoadLookUpValue("ORGCategory");
                string CategoryName = model.Hgetval;
                ViewBag.OrgCategoryName = CategoryName;

                if (ModelState.IsValid)
                {
                    var hits = (from x in _db.MNT_Deductible where x.DeductibleId == model.DeductibleId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        ModelState.Clear();
                        model.OrgCategoryName = model.Hgetval;
                        //model.CreatedBy = LoggedInUserName;
                        var q23 = (from x in _db.MNT_Deductible where x.OrgCategory == model.OrgCategory && x.OrgCategoryName == model.OrgCategoryName select x).FirstOrDefault();

                        if (q23 == null && model.EffectiveTo == null)
                        {
                            var list = model.Update();
                            TempData["result"] = "Record Saved Successfully.";
                            return View(model);
                        }


                        if (q23 == null && model.EffectiveTo != null)
                        {
                            var list = model.Update();
                            TempData["result"] = "Record Saved Successfully.";
                        }
                        else
                        {
                            var q24 = (from x in _db.MNT_Deductible where x.OrgCategory == model.OrgCategory && x.OrgCategoryName == model.OrgCategoryName orderby x.DeductibleId descending select x);
                            if (q24.Any())
                            {

                                foreach (var data in q24)
                                {
                                    DateTime dtEffFrom = Convert.ToDateTime(data.EffectiveFrom);
                                    DateTime dtEffTo = Convert.ToDateTime(data.EffectiveTo);
                                    DateTime dtModeleffFrom = Convert.ToDateTime(model.EffectiveFrom);
                                    DateTime dtModeleffTo = Convert.ToDateTime(model.EffectiveTo);
                                    TempData["result"] = DeductibleModel.DateCheck(dtEffFrom, dtEffTo, dtModeleffFrom, dtModeleffTo);
                                    if (Convert.ToString(TempData["result"]) != "T")
                                    {
                                        return View(model);
                                    }
                                }

                            }
                            var list1 = model.Update();
                            TempData["result"] = "Record Saved Successfully.";
                        }
                        //var list1 = model.Update();
                        //TempData["result"] = "Record Saved Successfully.";
                    }
                    else
                    {
                        ModelState.Clear();
                        model.OrgCategoryName = model.Hgetval;
                        // model.ModifiedBy = LoggedInUserName;

                        var q24 = (from x in _db.MNT_Deductible where x.OrgCategory == model.OrgCategory && x.OrgCategoryName == model.OrgCategoryName orderby x.DeductibleId descending select x);
                        if (q24.Any())
                        {

                            foreach (var data in q24)
                            {
                                DateTime dtEffFrom = Convert.ToDateTime(data.EffectiveFrom);
                                DateTime dtEffTo = Convert.ToDateTime(data.EffectiveTo);
                                DateTime dtModeleffFrom = Convert.ToDateTime(model.EffectiveFrom);
                                DateTime dtModeleffTo = Convert.ToDateTime(model.EffectiveTo);

                                TempData["result"] = DeductibleModel.DateCheck(dtEffFrom, dtEffTo, dtModeleffFrom, dtModeleffTo, model.DeductibleId);

                                if (Convert.ToString(TempData["result"]) != "T")
                                {
                                    return View(model);
                                }
                            }

                        }
                        var q23 = (from x in _db.MNT_Deductible where x.DeductibleId == model.DeductibleId && x.OrgCategory == model.OrgCategory && x.OrgCategoryName == model.OrgCategoryName select x).FirstOrDefault();
                        if (q23 != null && q23.EffectiveTo == null && model.EffectiveTo == null)
                        {
                            DateTime dtEffFrom = Convert.ToDateTime(q23.EffectiveFrom);
                            DateTime dtEffTo = Convert.ToDateTime(model.EffectiveTo);
                            DateTime dtModeleffFrom = Convert.ToDateTime(model.EffectiveFrom);
                            DateTime dtModeleffTo = Convert.ToDateTime(model.EffectiveTo);
                            bool exist = DeductibleModel.intersects(dtEffFrom, dtEffTo, dtModeleffFrom, dtModeleffTo);
                            var list = model.Update();
                            TempData["result"] = "Record Updated Successfully.";
                        }
                        else if (q23 != null && q23.EffectiveTo == null && model.EffectiveTo != null)
                        {

                            DateTime dtEffFrom = Convert.ToDateTime(q23.EffectiveFrom);
                            DateTime dtEffTo = Convert.ToDateTime(model.EffectiveTo);
                            DateTime dtModeleffFrom = Convert.ToDateTime(model.EffectiveFrom);
                            DateTime dtModeleffTo = Convert.ToDateTime(model.EffectiveTo);
                            var list2 = model.Update();
                            TempData["result"] = "Record Updated Successfully.";


                        }
                        else if (q23 != null && model.EffectiveTo != null)
                        {

                            DateTime dtEffFrom = Convert.ToDateTime(q23.EffectiveFrom);
                            DateTime dtEffTo = Convert.ToDateTime(model.EffectiveTo);
                            DateTime dtModeleffFrom = Convert.ToDateTime(model.EffectiveFrom);
                            DateTime dtModeleffTo = Convert.ToDateTime(model.EffectiveTo);
                            var list2 = model.Update();
                            TempData["result"] = "Record Updated Successfully.";


                        }
                        else
                        {
                            DateTime dtEffFrom = Convert.ToDateTime(q23.EffectiveFrom);
                            DateTime dtEffTo = Convert.ToDateTime(q23.EffectiveTo);
                            DateTime dtModeleffFrom = Convert.ToDateTime(model.EffectiveFrom);
                            DateTime dtModeleffTo = Convert.ToDateTime(model.EffectiveTo);
                            var list2 = model.Update();
                            TempData["result"] = "Record Updated Successfully.";
                        }



                    }
                }
                else
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
            }
            catch (System.Data.DataException ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(model);
        }


        public JsonResult GetCategoryList(string cat, string pageMode = "", string UniqueId = "")
        {
            var sgf = DeductibleModel.GetlistOrgName(cat, pageMode);
            return Json(sgf);
        }

        public JsonResult GetDeductableAmountTopId()
        {
            return Json(DeductibleModel.GetDeductableAmountTopId());
        }

        public JsonResult FillOrganizationCategory()
        {
            var returnData = LoadOrgCategory();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ViewDeductibleHistory(string Cedantid, string Tablename)
        {
            MCASEntities obj = new MCASEntities();
            List<DeductibleModel> list = new List<DeductibleModel>();
            list = DeductibleModel.Fetchalldeductiblelist(Cedantid, Tablename);
            obj.Dispose();
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveNewDeductible(string deductid, string cat, string catName, DateTime EffFrom, string Effto, string dedAmt)
        {
            MCASEntities _db = new MCASEntities();
            int deduct = Convert.ToInt16(deductid);
            decimal DedAmt = Convert.ToDecimal(dedAmt);
            cat = (from l in _db.MNT_Deductible where l.DeductibleId == deduct select l.OrgCategory).FirstOrDefault();
            catName = (from l in _db.MNT_Deductible where l.DeductibleId == deduct select l.OrgCategoryName).FirstOrDefault();
            var P_EffectiveTo = (from l in _db.MNT_Deductible where l.DeductibleId == deduct select l.EffectiveTo).FirstOrDefault();
            var P_EffectiveFrom = (from l in _db.MNT_Deductible where l.DeductibleId == deduct select l.EffectiveFrom).FirstOrDefault();
            var model = new DeductibleModel();
            model.CreatedBy = LoggedInUserName;
            model.OrgCategoryName = model.Hgetval;
            model.EffectiveFrom = EffFrom;
            if (!string.IsNullOrEmpty(Effto))
            {
                model.EffectiveTo = Convert.ToDateTime(Effto);
            }
            else
            {
                model.EffectiveTo = null;
            }
            var q23 = (from x in _db.MNT_Deductible where x.DeductibleId == deduct select x).FirstOrDefault();
            if (q23 == null)
            {
                DeductibleModel.SaveRecord(cat, catName, DedAmt, EffFrom, Effto);
                _db.Dispose();
                return Json("Record saved successfully.");
            }
            else
            {
                DateTime dtEffFrom = Convert.ToDateTime(P_EffectiveFrom);
                DateTime dtEffTo = Convert.ToDateTime(P_EffectiveTo);
                DateTime dtModeleffFrom = Convert.ToDateTime(EffFrom);
                DateTime? dtModeleffTo = null;
                if (!string.IsNullOrEmpty(Effto))
                {
                    dtModeleffTo = Convert.ToDateTime(Effto);
                }
                else
                {
                    dtModeleffTo = null;
                }

                bool exist = DeductibleModel.DateCheck(dtEffFrom, dtEffTo, dtModeleffFrom, dtModeleffTo) == "T" ? false : true;
                if ((P_EffectiveFrom != dtModeleffFrom && P_EffectiveTo != dtModeleffTo) && !exist)
                {
                    var list = model.UpdateRecord(cat, catName, DedAmt, EffFrom, Effto, deduct);
                    DeductibleModel modellist = new DeductibleModel();
                    var deductible = _db.MNT_Deductible.Where(x => x.DeductibleId == list.DeductibleId).FirstOrDefault();
                    modellist = (from x in _db.MNT_Deductible
                                 where x.DeductibleId == list.DeductibleId
                                 select new DeductibleModel
                        {
                            Title = "Record updated successfully.",
                            CreatedBy = x.CreatedBy,
                            CreatedDate = x.CreatedDate,
                            ModifiedDate = x.ModifiedDate,
                            ModifiedBy = x.ModifiedBy
                        }).FirstOrDefault();

                    _db.Dispose();
                    return Json(modellist, JsonRequestBehavior.AllowGet);
                }
                _db.Dispose();
                return (P_EffectiveFrom == dtModeleffFrom && P_EffectiveTo == dtModeleffTo) ? Json("Effective from and Effective To date overlapping with already save record") : Json(DeductibleModel.DateCheck(dtEffFrom, dtEffTo, dtModeleffFrom, dtModeleffTo));
            }

        }


        public JsonResult FindOrganizationName(string orgCategory, string orgCategoryName)
        {
            List<DeductibleModel> list = new List<DeductibleModel>();
            list = DeductibleModel.FetchDeductibleDetails(orgCategory, orgCategoryName);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Common Master

        public ActionResult CommonMasterIndex()
        {
            List<CommonMasterModel> list = new List<CommonMasterModel>();
            try
            {
                list = CommonMasterModel.FetchCommonMasterDetails();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Common master deatils " + list + " for Common master." + list + ".");
                addInfo.Add("entity_type", "Common master");
                PublishException(ex, addInfo, 0, "Common master" + list);
                return View(list);
            }

            return View(list);
        }

        public JsonResult FillStatus()
        {
            var returnData = LoadLookUpValue("STATUS");
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillMasterDescriptionList()
        {
            var returnData = CommonMasterModel.FetchCommonMasterCategoryList();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CommonMasterIndex(string MasterDesc, string ValueDesc, string ValueCode, string Status)
        {

            MasterDesc = ((Request.Form["ddlMaster"] == null) ? "" : Request.Form["ddlMaster"]);
            ValueDesc = Request.Form["inputValueDescription"].Trim();
            ValueCode = Request.Form["inputValueCode"].Trim();
            Status = ((Request.Form["ddlStatusCode"] == null) ? "" : Request.Form["ddlStatusCode"]);

            ViewBag.MasterDesc = MasterDesc;
            ViewBag.ValueDesc = ValueDesc;
            ViewBag.ValueCode = ValueCode;
            ViewBag.Status = Status;

            var strStat = Status == "" ? "" : Status == "Active" ? "Y" : "N";

            List<CommonMasterModel> list = new List<CommonMasterModel>();
            try
            {
                if (MasterDesc != "" || ValueDesc != "" || ValueCode != "" || strStat != "")
                {
                    list = GetMasterDescriptionResult(MasterDesc, ValueDesc, ValueCode, strStat).ToList();
                }
                else
                {
                    list = CommonMasterModel.FetchCommonMasterDetails();

                }

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for master desc " + MasterDesc + " for Search result Type of master desc." + MasterDesc + ".");
                addInfo.Add("entity_type", "Master Desc");
                PublishException(ex, addInfo, 0, "LOU master" + MasterDesc);

            }

            return View(list);

        }

        public IQueryable<CommonMasterModel> GetMasterDescriptionResult(string MasterDesc, string ValueDesc, string ValueCode, string Status)
        {

            MCASEntities obj = new MCASEntities();
            var item = new List<CommonMasterModel>();
            var AllmenulistInt = (from x in obj.MNT_Lookups where x.IsActive == "Y" select x.Category).ToList();
            var strCategory = (from x in obj.MNT_LookupsMaster where x.IsActive == "Y" select x.LookupCategoryCode).ToList();
            var intersect = AllmenulistInt.Intersect(strCategory);

            var CommonMasterList = (from x in obj.MNT_Lookups
                                    orderby x.Description
                                    where intersect.Contains(x.Category)
                                    select x);
            IQueryable<CommonMasterModel> searchResult = null;
            var MasterDescription = MasterDesc == "0" ? "" : MasterDesc;
            if (intersect.Any())
            {
                searchResult = (from x in CommonMasterList
                                where
                                  x.Category.StartsWith(MasterDescription) &&
                                  x.Lookupvalue.Contains(ValueCode) &&
                                  x.Lookupdesc.Contains(ValueDesc) &&
                                  x.IsActive.Contains(Status)
                                select new CommonMasterModel
                               {
                                   LookupID = x.LookupID,
                                   Lookupvalue = x.Lookupvalue,
                                   Lookupdesc = x.Lookupdesc,
                                   Description = x.Description,
                                   Category = x.Category,
                                   IsActive = x.IsActive,
                                   LookUpCategoryDesc = (from y in obj.MNT_LookupsMaster where y.IsActive == "Y" && y.LookupCategoryCode == x.Category select y.LookupCategoryDesc).FirstOrDefault()
                               }).AsQueryable();
            }
            return searchResult;
        }

        public ActionResult CommonMasterEditor(int? uid)
        {
            MCASEntities obj = new MCASEntities();
            int? Id = uid;
            var model = new CommonMasterModel();
            model.CommonMasterCategoryList = CommonMasterModel.FetchCommonMasterCategoryList();
            model.OrgCatTypelist = CommonMasterModel.fetchOrganizationList(LoggedInUserId, "", ""); 
            model.Statuslist = LoadLookUpValue("STATUS");
            MNT_LookupsMaster MNTLOOKUPMASTER;
            CommonMasterModel objmodel = new CommonMasterModel();
            MNTLOOKUPMASTER = obj.MNT_LookupsMaster.Where(x => x.LookUpMasterID == Id).FirstOrDefault();
            try
            {
                if (Id.HasValue)
                {
                    var CommonMasterlist = (from d in _db.MNT_Lookups where d.LookupID == Id select d).FirstOrDefault();
                    objmodel.LookupID = CommonMasterlist.LookupID;
                    objmodel.LookUpCategory = CommonMasterlist.Category;
                    objmodel.CommonMasterCategoryList = CommonMasterModel.FetchCommonMasterCategoryList();
                    objmodel.Description = CommonMasterlist.Description;
                    objmodel.Lookupdesc = CommonMasterlist.Lookupdesc;
                    //string[] i1;
                    objmodel.idlist = CommonMasterlist.lookupCode;
                    //if (items != null)
                    //{
                    //    i1 = items.Split(new[] {  
                    //    ","  
                    //}, StringSplitOptions.RemoveEmptyEntries);
                    //    for (int i = 0; i < i1.Length; i++)
                    //    {
                    //        string s = i1[i]; /*Inside string type s variable should contain items values */
                    //    }
                    //}

                    objmodel.OrgCatTypelist = CommonMasterModel.fetchOrganizationList(LoggedInUserId, "", ""); 
                    objmodel.Lookupvalue = CommonMasterlist.Lookupvalue;
                    objmodel.Status = CommonMasterlist.IsActive == "Y" ? "Active" : "Inactive";
                    objmodel.Statuslist = LoadLookUpValue("STATUS");
                    objmodel.CreatedBy = CommonMasterlist.CreateBy == null ? " " : CommonMasterlist.CreateBy;
                    objmodel.CreatedOn = CommonMasterlist.CreateDate != null ? (DateTime)CommonMasterlist.CreateDate : DateTime.MinValue;
                    objmodel.ModifiedBy = CommonMasterlist.ModifiedBy == null ? " " : CommonMasterlist.ModifiedBy;
                    objmodel.ModifiedOn = CommonMasterlist.ModifiedDate;
                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving result for Master description " + Id + " for  result Type of Master description." + Id + ".");
                addInfo.Add("entity_type", "Master description");
                PublishException(ex, addInfo, 0, "Master description" + Id);
                return View(model);
            }
            finally
            {
                _db.Dispose();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CommonMasterEditor(CommonMasterModel model, FormCollection frm)
        {
            try
            {
                
                string[] i1;
                string items = frm["idlist"];
                string item = model.idlist;
                if (items != null)
                {
                    i1 = items.Split(new[] {  
                        ","  
                    }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < i1.Length; i++)
                    {
                        string s = i1[i]; /*Inside string type s variable should contain items values */
                    }
                }

                model.OrgCategory = items.TrimEnd(','); 
                model.CommonMasterCategoryList = CommonMasterModel.FetchCommonMasterCategoryList();
                model.Statuslist = LoadLookUpValue("STATUS");
                model.OrgCatTypelist = CommonMasterModel.fetchOrganizationList(LoggedInUserId, "", ""); 
                if (ModelState.IsValid)
                {
                    ModelState.Clear();

                    var hits = (from x in _db.MNT_Lookups where x.LookupID == model.LookupID select x).FirstOrDefault();
                    var CheckLookupValue = (from x in _db.MNT_Lookups where x.Lookupvalue == model.Lookupvalue.Trim() && x.Category == model.LookUpCategory select x.LookupID).FirstOrDefault();

                    if (hits == null)
                    {
                       
                        if (CheckLookupValue > 0)
                        {
                            ModelState.AddModelError("Lookupvalue", "Duplicate Value Code.");
                            ViewData["ModelError"] = "DuplicateError";
                            return View(model);
                        }

                        model.CreatedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        var valuecode = (from x in _db.MNT_Lookups where x.LookupID == model.LookupID && x.Category == model.LookUpCategory select x.Lookupvalue).FirstOrDefault();
                        if (model.Lookupvalue != valuecode)
                        {
                            if (CheckLookupValue > 1)
                            {
                                ModelState.AddModelError("Lookupvalue", "Duplicate Value Code.");
                                MNT_Lookups lookups = _db.MNT_Lookups.Where(x => x.LookupID == model.LookupID).FirstOrDefault();
                                //model.CreatedBy = lookups.CreateBy;
                                //model.CreatedOn = lookups.CreateDate == null ? DateTime.MinValue : (DateTime)lookups.CreateDate;
                                //model.ModifiedBy = lookups.ModifiedBy;
                                //model.ModifiedOn = lookups.ModifiedDate;
                                return View(model);
                            }
                        }
                        model.ModifiedBy = LoggedInUserName;
                        var list = model.Update();
                        TempData["result"] = "Record Updated Successfully.";
                    }

                }
                else
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    ModelError(errors, "VehicleClassEditor");
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving Common Master " + model.LookupID + " for Common Master." + model.Lookupdesc + ".");
                addInfo.Add("entity_type", "Common Master");
                PublishException(ex, addInfo, 0, "Common Master" + model.LookupID);
            }
            return View(model);
        }
        #endregion

        public ActionResult ServiceProviderOnMap()
        {
            return View();
        }

        public JsonResult GetServiceProviderOnMap()
        {
            ServiceProvider objsp = new ServiceProvider();
            List<ServiceProvider> listProvider = objsp.GetServiceProvider();

            foreach (var obj in listProvider)
            {
                string address = string.IsNullOrEmpty(obj.Address1) ? "" : obj.Address1;
                address = address.Trim() + " " + (string.IsNullOrEmpty(obj.Address2) ? "" : obj.Address2);
                address = address.Trim() + " " + (string.IsNullOrEmpty(obj.Address3) ? "" : obj.Address3);
                address = address.Trim() + " " + (string.IsNullOrEmpty(obj.City) ? "" : obj.City);
                address = address.Trim() + " " + (string.IsNullOrEmpty(obj.State) ? "" : obj.State);
                address = address.Trim() + " " + (string.IsNullOrEmpty(obj.Country) ? "" : obj.Country);
                address = address.Trim() + " " + (string.IsNullOrEmpty(obj.PostalCode) ? "" : obj.PostalCode);

                string url = "http://maps.google.com/maps/api/geocode/xml?address=" + address + "&sensor=false";
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        DataSet dsResult = new DataSet();
                        dsResult.ReadXml(reader);
                        if (dsResult.Tables["location"] != null && dsResult.Tables["location"].Rows.Count > 0)
                        {
                            DataRow location = dsResult.Tables["location"].Select()[0];
                            if (location != null)
                            {
                                obj.Latitude = location["lat"].ToString();
                                obj.Longitude = location["lng"].ToString();
                            }
                        }
                    }
                }
            }

            return Json(listProvider, JsonRequestBehavior.AllowGet);
        }

    }
}
