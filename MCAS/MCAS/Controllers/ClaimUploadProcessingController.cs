using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using MCAS.Entity;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;
using System.Transactions;
using System.Data.Objects;
using System.Collections.Specialized;
using System.Configuration;

namespace MCAS.Controllers
{
    public class ClaimUploadProcessingController : BaseController
    {
        //
        // GET: /ClaimUploadProcessing/
        MCASEntities obj = new MCASEntities();

        public ActionResult Index()
        {
            return View();
        }

        #region "TacFileUpload"
        public ActionResult TacFileUploadEditor(int? FileId, string FileRef)
        {
            MCASEntities _db = new MCASEntities();
            string strOrgName1 = string.Empty;
            var model = new TacFileUploadEditorModel();
            model.Categorylist = LoadLookUpValue("ORGCategory");
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            MNT_FileUpload MNTFileUpload;
            MNTFileUpload = _db.MNT_FileUpload.Where(x => x.FileId == FileId).FirstOrDefault();
            TacFileUploadEditorModel objmodel = new TacFileUploadEditorModel();
            try
            {

                if (FileId.HasValue)
                {
                    if (FileRef.ToUpper().Trim() != null)
                    {
                        var Filelist = (from d in _db.MNT_FileUpload where d.FileRefNo == FileRef.ToUpper().Trim() select d).ToList();
                        objmodel.FileRefNo = Filelist[0].FileRefNo;
                        TempData["FileRef"] = objmodel.FileRefNo;
                        ViewBag.fileRefNo = Filelist[0].FileRefNo;
                        objmodel.Hgetval = Filelist[0].OrganizationName;
                        objmodel.OrgCategory = Filelist[0].OrganizationType;
                        objmodel.OrgCategoryName = Filelist[0].OrganizationName;
                        strOrgName1 = objmodel.OrgCategory;
                        TempData["orgCategory"] = objmodel.OrgCategory;
                        TempData["orgCategoryname"] = objmodel.OrgCategoryName;
                        objmodel.Categorylist = LoadLookUpValue("ORGCategory");

                        objmodel.FileId = Filelist[0].FileId;
                        TempData["FileId1"] = objmodel.FileId;
                        objmodel.Hf1 = Filelist[0].FileName;
                        objmodel.Hfn1 = Filelist[0].FileName;
                        objmodel.FileId = Filelist[1].FileId;
                        TempData["FileId2"] = objmodel.FileId;
                        objmodel.Hf2 = Filelist[1].FileName;
                        objmodel.Hfn2 = Filelist[1].FileName;
                        objmodel.FileId = Filelist[2].FileId;
                        TempData["FileId3"] = objmodel.FileId;
                        objmodel.Hf3 = Filelist[2].FileName;
                        objmodel.Hfn3 = Filelist[2].FileName;
                        objmodel.CreatedBy = Filelist[0].CreatedBy;
                        objmodel.CreatedOn = Filelist[0].UploadedDate;
                        objmodel.ModifiedBy = Filelist[2].ModifiedBy;
                        objmodel.ModifiedOn = Filelist[2].LastModifiedDateTime;
                    }
                    return View(objmodel);

                }

            }

            catch (Exception ex)
            {
                //  ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
                return View(model);
            }

            return View(model);
        }
        public ActionResult TacFileUploadViewList()
        {
            List<TacFileUploadViewListModel> list = new List<TacFileUploadViewListModel>();
            try
            {
                var Uploadrefno = Request.Form["Uploadrefno"] != null ? Convert.ToString(Request.Form["Uploadrefno"]) : "";
                var Uploadstatus = Request.Form["Uploadstatus"] != null ? Convert.ToString(Request.Form["Uploadstatus"]) : "";
                var UploadFromDate = Request.Form["UploadFromDate"] != null ? Convert.ToString(Request.Form["UploadFromDate"]) : "";
                var UploadToDate = Request.Form["UploadToDate"] != null ? Convert.ToString(Request.Form["UploadToDate"]) : "";
                list = TacFileUploadEditorModel.Fetchall(Uploadrefno, Uploadstatus, UploadFromDate, UploadToDate, "TAC");
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(list);
            }

            return View(list);
        }
        [HttpPost]
        public ActionResult TacFileUploadEditor(TacFileUploadEditorModel model, HttpPostedFileBase TACIP, HttpPostedFileBase TACACCREP, HttpPostedFileBase TACIPBUS, string FileRef)
        {

            MCASEntities db = new MCASEntities();
            string TACIPFileName = string.Empty;
            string TACIPFileType = string.Empty;
            string TACACCREPFileName = string.Empty;
            string TACACCREPFileType = string.Empty;
            string TACIPBUSFileName = string.Empty;
            string TACIPBUSFileType = string.Empty;
            string foldername = string.Empty;
            string UploadFolderPath = "";
            string FileUploadPath = "";
            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();
            var dir = "";
            var dirPath = "";
            var fileServerpath = "";
            var schedulestarttime = model.Sdatetime.ToString();
            model.CreatedBy = LoggedInUserName;
            model.OrgCategoryName = model.Hgetval;
            //model.ModifiedBy = LoggedInUserName;
            TempData["orgCategoryname"] = model.OrgCategoryName;
            model.Categorylist = LoadLookUpValue("ORGCategory");
            try
            {
                if (ConfigurationManager.AppSettings["UploadFolder"] == null)
                {
                    ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                    return View(model);
                }
                else
                {
                    UploadFolderPath = ConfigurationManager.AppSettings["UploadFolder"].ToString();
                    if (String.IsNullOrEmpty(UploadFolderPath))
                    {
                        ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                        return View(model);
                    }
                }
                fileServerpath = (from l in db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                if (String.IsNullOrEmpty(fileServerpath))
                {
                    ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                    return View(model);
                }
                FileUploadPath = fileServerpath.TrimEnd('\\') + "\\" + UploadFolderPath.TrimStart('\\');

                if (Path.GetFileNameWithoutExtension(TACIP.FileName).ToUpper().Trim() != "TAC_IP")
                {
                    ViewData["SuccessMsg"] = "Invalid file name \"" + Path.GetFileName(TACIP.FileName) + "\" for TAC_IP";
                    return View(model);
                }
                if (Path.GetFileNameWithoutExtension(TACACCREP.FileName).ToUpper().Trim() != "TAC_ACC_REP")
                {
                    ViewData["SuccessMsg"] = "Invalid file name \"" + Path.GetFileName(TACACCREP.FileName) + "\" for TAC_ACC_REP";
                    return View(model);
                }
                if (Path.GetFileNameWithoutExtension(TACIPBUS.FileName).ToUpper().Trim() != "TAC_IP_BUS")
                {
                    ViewData["SuccessMsg"] = "Invalid file name \"" + Path.GetFileName(TACIPBUS.FileName) + "\" for TAC_IP_BUS";
                    return View(model);
                }
                TACIPFileType = Path.GetExtension(TACIP.FileName).TrimStart('.');
                TACACCREPFileType = Path.GetExtension(TACACCREP.FileName).TrimStart('.');
                TACIPBUSFileType = Path.GetExtension(TACIPBUS.FileName).TrimStart('.');
                var oname = model.OrgCategory == "Select" ? "" : model.OrgCategory;

                EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
                if (!objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
                {
                    ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                    return View(model);
                }
                if (FileRef == null)
                {

                    var LastFile = db.MNT_FileUpload.OrderByDescending(x => x.FileId).FirstOrDefault();
                    int FileId = LastFile == null ? 1 : Convert.ToInt32(LastFile.FileId) + 1;
                    var FileNameid = "T_IP_" + FileId.ToString();
                    TACIPFileName = Path.GetFileNameWithoutExtension(TACIP.FileName) + "_" + FileNameid + Path.GetExtension(TACIP.FileName);

                    FileId = Convert.ToInt32(FileId) + 1;
                    FileNameid = "T_REP_" + FileId.ToString();
                    TACACCREPFileName = Path.GetFileNameWithoutExtension(TACACCREP.FileName) + "_" + FileNameid + Path.GetExtension(TACACCREP.FileName);

                    FileId = Convert.ToInt32(FileId) + 1;
                    FileNameid = "T_BUS_" + FileId.ToString();
                    TACIPBUSFileName = Path.GetFileNameWithoutExtension(TACIPBUS.FileName) + "_" + FileNameid + Path.GetExtension(TACIPBUS.FileName);

                    model.OrgCategoryName = model.Hgetval;
                    var orgName = model.OrgCategoryName == "Select" ? "" : model.OrgCategoryName;
                    db.Proc_TacFileSave(TACIPFileName, TACIPFileType, TACACCREPFileName, TACACCREPFileType, TACIPBUSFileName, TACIPBUSFileType, Convert.ToDateTime(schedulestarttime), "TAC", UploadFolderPath + @"\TacUpload", model.CreatedBy, oname, orgName);
                    var FileRefNo = (from x in db.MNT_FileUpload where x.FileId == FileId select x.FileRefNo).FirstOrDefault();
                    foldername = FileRefNo;
                    TempData["FileRef"] = FileRefNo;
                    ModelState.Remove("FileRefNo");
                    model.FileRefNo = FileRefNo;

                    dir = FileUploadPath + @"\TacUpload\" + foldername;
                    dirPath = UploadFolderPath + @"\TacUpload\" + foldername;
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    TACIP.SaveAs(Path.Combine(dir, TACIPFileName));
                    TACACCREP.SaveAs(Path.Combine(dir, TACACCREPFileName));
                    TACIPBUS.SaveAs(Path.Combine(dir, TACIPBUSFileName));
                    if (dir != null)
                    {
                        db.Proc_UpdateFolderPath(FileRefNo, dirPath);
                    }
                    db.Dispose();
                }
                else if (!String.IsNullOrEmpty(FileRef))
                {

                    var Filelist = (from d in db.MNT_FileUpload where d.FileRefNo == FileRef.ToUpper().Trim() select d).ToList();
                    model.FileRefNo = Filelist[0].FileRefNo;
                    TempData["FileRef"] = model.FileRefNo;
                    ViewBag.fileRefNo = Filelist[0].FileRefNo;

                    int TIPfileid = (Filelist.Where(f => f.UploadType == "T_IP").FirstOrDefault()).FileId;
                    int TREPfileid = (Filelist.Where(f => f.UploadType == "T_REP").FirstOrDefault()).FileId;
                    int TBUSfileid = (Filelist.Where(f => f.UploadType == "T_BUS").FirstOrDefault()).FileId;

                    dir = FileUploadPath + @"\TacUpload\" + Filelist[0].FileRefNo;
                    dirPath = UploadFolderPath + @"\TacUpload\" + Filelist[0].FileRefNo;

                    var FileNameid = "T_IP_" + TIPfileid.ToString();
                    TACIPFileName = Path.GetFileNameWithoutExtension(TACIP.FileName) + "_" + FileNameid + "_" + DateTime.Now.ToString("yyyyMMddmmss") + Path.GetExtension(TACIP.FileName);

                    FileNameid = "T_REP_" + TREPfileid.ToString();
                    TACACCREPFileName = Path.GetFileNameWithoutExtension(TACACCREP.FileName) + "_" + FileNameid + "_" + DateTime.Now.ToString("yyyyMMddmmss") + Path.GetExtension(TACACCREP.FileName);

                    FileNameid = "T_BUS_" + TBUSfileid.ToString();
                    TACIPBUSFileName = Path.GetFileNameWithoutExtension(TACIPBUS.FileName) + "_" + FileNameid + "_" + DateTime.Now.ToString("yyyyMMddmmss") + Path.GetExtension(TACIPBUS.FileName);

                    db.Proc_TacFileSaveUpdate(TACIPFileName, TACIPFileType, TACACCREPFileName, TACACCREPFileType, TACIPBUSFileName, TACIPBUSFileType, Convert.ToDateTime(schedulestarttime), "TAC", "", dir, LoggedInUserName, oname, model.OrgCategoryName, 0, model.FileRefNo);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    TACIP.SaveAs(Path.Combine(dir, TACIPFileName));
                    TACACCREP.SaveAs(Path.Combine(dir, TACACCREPFileName));
                    TACIPBUS.SaveAs(Path.Combine(dir, TACIPBUSFileName));
                    if (dirPath != null)
                    {

                        db.Proc_UpdateFolderPath(model.FileRefNo, dirPath);
                    }
                    db.Dispose();

                }
                model.Hf1 = Path.Combine(dir, TACIPFileName);// TACIP.FileName.Split('\\').Last().ToString();
                model.Hfn1 = TACIPFileName;

                model.Hf2 = Path.Combine(dir, TACACCREPFileName);// TACACCREP.FileName.Split('\\').Last().ToString();
                model.Hfn2 = TACACCREPFileName;

                model.Hf3 = Path.Combine(dir, TACIPBUSFileName); //TACIPBUS.FileName.Split('\\').Last().ToString();
                model.Hfn3 = TACIPBUSFileName;
                model.OrganizationNameList = TacFileUploadEditorModel.FetchOrg();
                model.CreatedBy = LoggedInUserName;
                model.CreatedOn = DateTime.Now;
                
                ViewData["SuccessMsg"] = "Files Uploaded Successfully.";
                objImPersonate.endImpersonation();
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ViewData["SuccessMsg"] = "Error occured while uploading Files. " + ex.Message;
                return View(model);
            }
            //return View(model);
        }
        /*
                [HttpPost]
                public ActionResult TacFileUploadEditor(TacFileUploadEditorModel model, HttpPostedFileBase TACIP, HttpPostedFileBase TACACCREP, HttpPostedFileBase TACIPBUS, string FileRef)
                {
            
                    MCASEntities db = new MCASEntities();
                    string FileName1 = string.Empty;
                    string FileType1 = string.Empty;
                    string FileName2 = string.Empty;
                    string FileType2 = string.Empty;
                    string FileName3 = string.Empty;
                    string FileType3 = string.Empty;
                    string foldername = string.Empty;
                    string newpath = "";
                    string Mynewpath = "";
                    string iDomain = "";
                    string iUserName = "";
                    string iPassword = "";
                    var dir = "";
                    var dirPath = "";
                    var fileServerpath = "";
                    var schedulestarttime = model.Sdatetime.ToString();
                    model.CreatedBy = LoggedInUserName;
                    model.OrgCategoryName = model.Hgetval;
                    model.ModifiedBy = LoggedInUserName;
                    TempData["orgCategoryname"] = model.OrgCategoryName;
                    model.Categorylist = LoadLookUpValue("ORGCategory");

                    newpath = ConfigurationManager.AppSettings["UploadFolder"];
                    fileServerpath = (from l in db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                    Mynewpath = fileServerpath + newpath;
                    try
                    {
                        if (FileRef == null) 
                        {
                    
                            var id = db.MNT_FileUpload.OrderByDescending(x => x.FileId).FirstOrDefault();
                            var id41 = id == null ? 1 : Convert.ToInt32(id.FileId) + 1;
                            int FileId = Convert.ToInt32(id41);
                            var id1 = "T_IP_" + id41;
                            FileName1 = Regex.Replace(TACIP.FileName.Split('\\').Last().ToString().Split('.')[0] + "_" + id1 + "." + TACIP.FileName.Split('\\').Last().ToString().Split('.')[1], @"\s+", "");
                            FileType1 = TACIP.FileName.Split('\\').Last().ToString().Split('.')[1];
                            var path1 = Path.Combine(Mynewpath, FileName1);
                            var se = TACIP.FileName.Split('\\').Last();
                            var s1 = Path.GetFileNameWithoutExtension(se);

                            if (s1.ToUpper().Trim() != "TAC_IP" || s1.ToLower().Trim()!="tac_ip") {
                                ViewData["SuccessMsg"] = "Invalid file name " + se;
                                return View(model);
                            }
                    
                            var id51 = Convert.ToInt32(id41) + 1;
                            var id12 = "T_REP_" + id51;
                            FileName2 = Regex.Replace(TACACCREP.FileName.Split('\\').Last().ToString().Split('.')[0] + "_" + id12 + "." + TACACCREP.FileName.Split('\\').Last().ToString().Split('.')[1], @"\s+", "");
                            FileType2 = TACACCREP.FileName.Split('\\').Last().ToString().Split('.')[1];
                            var path2 = Path.Combine(Mynewpath, FileName2);

                            var se1 = TACACCREP.FileName.Split('\\').Last();
                            var s2 = Path.GetFileNameWithoutExtension(se1);
                            if (s2.ToUpper().Trim() != "TAC_ACC_REP" || s2.ToLower().Trim()!="tac_acc_rep")
                            {
                                ViewData["SuccessMsg"] = "Invalid file name " + se1;
                                return View(model);
                            }
                    
                            var id61 = Convert.ToInt32(id41) + 2;
                            var id13 = "T_BUS_" + id61;
                            FileName3 = Regex.Replace(TACIPBUS.FileName.Split('\\').Last().ToString().Split('.')[0] + "_" + id13 + "." + TACIPBUS.FileName.Split('\\').Last().ToString().Split('.')[1], @"\s+", "");
                            FileType3 = TACIPBUS.FileName.Split('\\').Last().ToString().Split('.')[1];
                            var path3 = Path.Combine(Mynewpath, FileName3);
                            var se2 = TACIPBUS.FileName.Split('\\').Last();
                            var s3 = Path.GetFileNameWithoutExtension(se2);
                            if (s3.ToUpper().Trim() != "TAC_IP_BUS" || s3.ToLower().Trim()!="tac_ip_bus")
                            {
                                ViewData["SuccessMsg"] = "Invalid file name " + se2;
                                return View(model);
                            }
                    
                            var oname = model.OrgCategory == "Select" ? "" : model.OrgCategory;
                            model.OrgCategoryName = model.Hgetval;
                            var orgName = model.OrgCategoryName == "Select" ? "" : model.OrgCategoryName;
                            db.Proc_TacFileSave(FileName1, FileType1, FileName2, FileType2, FileName3, FileType3, Convert.ToDateTime(schedulestarttime), "TAC", @"\Uploads\TacUpload", model.CreatedBy, oname, orgName);
                            var FileRefNo = (from x in db.MNT_FileUpload where x.FileId == id41 select x.FileRefNo).FirstOrDefault();
                            foldername = FileRefNo;
                            TempData["FileRef"] = FileRefNo;
                            model.FileRefNo = FileRefNo;
                            dir = Mynewpath + "\\" + foldername;
                            dirPath = @"\Uploads\TacUpload\" + foldername;
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);

                            }

                            TACIP.SaveAs(Path.Combine(dir, FileName1));
                            TACACCREP.SaveAs(Path.Combine(dir, FileName2));
                            TACIPBUS.SaveAs(Path.Combine(dir, FileName3));
                            if (dir != null)
                            {

                                db.Proc_UpdateFolderPath(FileRefNo, dirPath);
                            }
                                      

                            //iDomain = ConfigurationManager.AppSettings["IDomain"];
                            //iUserName = ConfigurationManager.AppSettings["IUserName"];
                            //iPassword = ConfigurationManager.AppSettings["IPassWd"];
                            //EAWXmlToPDFParser.ImpersonateUser userImpersonate = new EAWXmlToPDFParser.ImpersonateUser();
                            //bool userImp = userImpersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain);
                            //if (userImp)
                            //{
                            //    if (!Directory.Exists(dir))
                            //    {
                            //        Directory.CreateDirectory(dir);

                            //    }
                        
                            //    TACIP.SaveAs(Path.Combine(dir, FileName1));
                            //    TACACCREP.SaveAs(Path.Combine(dir, FileName2));
                            //    TACIPBUS.SaveAs(Path.Combine(dir, FileName3));
                            //    if (dir != null)
                            //    {

                            //        db.Proc_UpdateFolderPath(FileRefNo, dirPath);
                            //    }
                            //}
                            //else
                            //{
                            //    ViewData["SuccessMsg"] = "Don't have permission on this folder !!! ";
                            //}
                            //userImpersonate.endImpersonation();


                            //db.Proc_FileUpload(FileId, FileName1, FileType1, Convert.ToDateTime(schedulestarttime), "TAC", @"\Uploads\TacUpload", CreatedBy, model.OrganizationType, oname, FileRefNo);
                            model.Hf1 = TACIP.FileName.Split('\\').Last().ToString();
                            model.Hfn1 = FileName1;

                            model.Hf2 = TACACCREP.FileName.Split('\\').Last().ToString();
                            model.Hfn2 = FileName2;

                            model.Hf3 = TACIPBUS.FileName.Split('\\').Last().ToString();
                            model.Hfn3 = FileName3;
                            model.OrganizationNameList = TacFileUploadEditorModel.FetchOrg();
                            ViewData["SuccessMsg"] = "Upload Successfully";
                            db.Dispose();
                            return View(model);

                        }

                        if (FileRef.ToUpper().Trim() != null)
                          {
                            var oname = model.OrgCategory == "Select" ? "" : model.OrgCategory;
                               
                            var Filelist = (from d in db.MNT_FileUpload where d.FileRefNo == FileRef.ToUpper().Trim() select d).ToList();
                            model.FileRefNo = Filelist[0].FileRefNo;
                            TempData["FileRef"] = model.FileRefNo;
                            ViewBag.fileRefNo = Filelist[0].FileRefNo;
                            int fileid1 = Filelist[0].FileId;
                            int fileid2 = Filelist[1].FileId;
                            int fileid3 = Filelist[2].FileId;
                            dir = Mynewpath + "\\" + Filelist[0].FileRefNo;
                            dirPath = @"\Uploads\TacUpload\" + Filelist[0].FileRefNo;
                            FileName1 = Regex.Replace(TACIP.FileName.Split('\\').Last().ToString().Split('.')[0] + "_" + fileid1 + "." + TACIP.FileName.Split('\\').Last().ToString().Split('.')[1], @"\s+", "");
                            FileType1 = TACIP.FileName.Split('\\').Last().ToString().Split('.')[1];
                            var se = TACIP.FileName.Split('\\').Last();
                            var s1 = Path.GetFileNameWithoutExtension(se);
                            if (s1.ToUpper().Trim() != "TAC_IP" || s1.ToLower().Trim()!="tac_ip")
                            {
                                ViewData["SuccessMsg"] = "Invalid file name " + se;
                                return View(model);
                            }
                            var FileName11 = s1 + DateTime.Now.ToString("yyyymmddMMss") + "." + FileType1;
                            db.Proc_TacFileSaveUpdate(FileName11, FileType1, "", "", "", "", Convert.ToDateTime(schedulestarttime), "TAC", "T_IP", dir, model.ModifiedBy, oname, model.OrgCategoryName, fileid1, model.FileRefNo);
                    

                            FileName2 = Regex.Replace(TACACCREP.FileName.Split('\\').Last().ToString().Split('.')[0] + "_" + fileid2 + "." + TACACCREP.FileName.Split('\\').Last().ToString().Split('.')[1], @"\s+", "");
                            FileType2 = TACACCREP.FileName.Split('\\').Last().ToString().Split('.')[1];
                            var path2 = Path.Combine(Mynewpath, FileName2);
                            var se1 = TACACCREP.FileName.Split('\\').Last();
                            var s2 = Path.GetFileNameWithoutExtension(se1);
                            if (s2.ToUpper().Trim() != "TAC_ACC_REP" || s2.ToLower().Trim()!="tac_acc_rep")
                            {
                                ViewData["SuccessMsg"] = "Invalid file name " + se1;
                                return View(model);
                            }
                            var FileName22 = s2 + DateTime.Now.ToString("yyyymmddMMss") + "." + FileType2;
                            db.Proc_TacFileSaveUpdate("", "", FileName22, FileType2, "", "", Convert.ToDateTime(schedulestarttime), "TAC", "T_REP", dir, model.ModifiedBy, oname, model.OrgCategoryName, fileid2, model.FileRefNo);

                            FileName3 = Regex.Replace(TACIPBUS.FileName.Split('\\').Last().ToString().Split('.')[0] + "_" + fileid3 + "." + TACIPBUS.FileName.Split('\\').Last().ToString().Split('.')[1], @"\s+", "");
                            FileType3 = TACIPBUS.FileName.Split('\\').Last().ToString().Split('.')[1];
                            var path3 = Path.Combine(Mynewpath, FileName3);
                            var se2 = TACIPBUS.FileName.Split('\\').Last();
                            var s3 = Path.GetFileNameWithoutExtension(se2);
                            if (s3.ToUpper().Trim() != "TAC_IP_BUS" || s3.ToLower().Trim()!="tac_ip_bus")
                            {
                                ViewData["SuccessMsg"] = "Invalid file name " + se2;
                                return View(model);
                            }
                            var FileName33 = s3 + DateTime.Now.ToString("yyyymmddMMss") + "." + FileType3;
                            db.Proc_TacFileSaveUpdate("", "", "", "", FileName33, FileType3, Convert.ToDateTime(schedulestarttime), "TAC", "T_BUS", dir, model.ModifiedBy, oname, model.OrgCategoryName, fileid3, model.FileRefNo);            
                    
                    
                            if (Directory.Exists(dir))
                            {


                            }
                            TACIP.SaveAs(Path.Combine(dir, FileName11));
                            TACACCREP.SaveAs(Path.Combine(dir, FileName22));
                            TACIPBUS.SaveAs(Path.Combine(dir, FileName33));
                            if (dirPath != null)
                            {

                                db.Proc_UpdateFolderPath(model.FileRefNo, dirPath);
                            }

                            //iDomain = ConfigurationManager.AppSettings["IDomain"];
                            //iUserName = ConfigurationManager.AppSettings["IUserName"];
                            //iPassword = ConfigurationManager.AppSettings["IPassWd"];
                            //EAWXmlToPDFParser.ImpersonateUser userImpersonate = new EAWXmlToPDFParser.ImpersonateUser();
                            //bool userImp = userImpersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain);
                            //if (userImp)
                            //{
                            //    if (Directory.Exists(dir))
                            //    {

                            
                            //    }
                            //    TACIP.SaveAs(Path.Combine(dir, newPath1));
                            //    TACACCREP.SaveAs(Path.Combine(dir, newPath2));
                            //    TACIPBUS.SaveAs(Path.Combine(dir, newPath3));
                            //    if (dirPath != null)
                            //    {

                            //        db.Proc_UpdateFolderPath(model.FileRefNo, dirPath);
                            //    }
                                             
                       
                            //}
                            //else
                            //{
                            //    ViewData["SuccessMsg"] = "Don't have permission on this folder !!! ";
                            //}
                            //userImpersonate.endImpersonation();


                            //db.Proc_FileUpload(FileId, FileName1, FileType1, Convert.ToDateTime(schedulestarttime), "TAC", @"\Uploads\TacUpload", CreatedBy, model.OrganizationType, oname, FileRefNo);
                            model.Hf1 = TACIP.FileName.Split('\\').Last().ToString();
                            model.Hfn1 = FileName11;

                            model.Hf2 = TACACCREP.FileName.Split('\\').Last().ToString();
                            model.Hfn2 = FileName22;

                            model.Hf3 = TACIPBUS.FileName.Split('\\').Last().ToString();
                            model.Hfn3 = FileName33;
                            model.OrganizationNameList = TacFileUploadEditorModel.FetchOrg();
                            ViewData["SuccessMsg"] = "Upload Successfully";
                            db.Dispose();
                            return View(model);
                        }
                                
                    } 
                    catch (Exception ex)
                    {
                        ErrorLog(ex.Message,ex.InnerException);
                        return View(model);
                
                    }
                    return View(model);
                }

        */

        # endregion

        #region "TacFileUploadViewEditor"
        public ActionResult TacFileUploadViewEditor()
        {
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            var model = new TacFileUploadViewEditorModel();
            model.Uploadstatuslist1 = TacFileUploadViewEditorModel.fetch();
            return View(model);
        }

        [HttpPost]
        public ActionResult TacFileUploadViewEditor(TacFileUploadViewEditorModel model)
        {
            model.Uploadstatuslist1 = TacFileUploadViewEditorModel.fetch();
            return View(model);
        }
        # endregion

        #region "ClaimFileUpload"
        public ActionResult ClaimFileUploadEditor(int? FileId, string FileRef)
        {
            MCASEntities _db = new MCASEntities();
            var model = new ClaimFileUploadEditorModel();
            string strOrgName1 = string.Empty;
            model.Categorylist = LoadLookUpValue("ORGCategory");
            if (MCASQueryString["claimMode"] == null) { return LoggedOut(); }
            string caller = MCASQueryString["claimMode"].ToString();
            CallerMenu = caller;
            ClaimFileUploadEditorModel objmodel = new ClaimFileUploadEditorModel();
            try
            {
                if (FileId.HasValue)
                {
                    if (FileRef.ToUpper().Trim() != null)
                    {
                        var Filelist = (from d in _db.MNT_FileUpload where d.FileRefNo == FileRef.ToUpper().Trim() select d).ToList();

                        objmodel.FileId = Filelist[0].FileId;
                        objmodel.FileRefNo = Filelist[0].FileRefNo;
                        TempData["FileRefClaim"] = objmodel.FileRefNo;
                        ViewBag.fileRefNo = objmodel.FileRefNo;
                        objmodel.OrgCategory = Filelist[0].OrganizationType;
                        objmodel.OrgCategoryName = Filelist[0].OrganizationName;
                        strOrgName1 = objmodel.OrgCategory;
                        TempData["orgCategory"] = objmodel.OrgCategory;
                        TempData["orgCategoryname"] = objmodel.OrgCategoryName;
                        objmodel.Categorylist = LoadLookUpValue("ORGCategory");
                        objmodel.Hf1 = Filelist[0].FileName;
                        objmodel.Hfn1 = Filelist[0].FileName;
                        objmodel.Hf2 = Filelist[1].FileName;
                        objmodel.Hfn2 = Filelist[1].FileName;
                        objmodel.CreatedBy = Filelist[0].CreatedBy;
                        objmodel.CreatedOn = Filelist[0].UploadedDate;
                        objmodel.ModifiedBy = Filelist[1].ModifiedBy;
                        objmodel.ModifiedOn = Filelist[1].LastModifiedDateTime;
                        return View(objmodel);
                    }
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
        public ActionResult ClaimFileUploadViewList()
        {
            List<ClaimFileUploadViewListModel> list = new List<ClaimFileUploadViewListModel>();
            try
            {
                var Uploadrefno = Request.Form["Uploadrefno"] != null ? Convert.ToString(Request.Form["Uploadrefno"]) : "";
                var Uploadstatus = Request.Form["Uploadstatus"] != null ? Convert.ToString(Request.Form["Uploadstatus"]) : "";
                var UploadFromDate = Request.Form["UploadFromDate"] != null ? Convert.ToString(Request.Form["UploadFromDate"]) : "";
                var UploadToDate = Request.Form["UploadToDate"] != null ? Convert.ToString(Request.Form["UploadToDate"]) : "";
                list = ClaimFileUploadEditorModel.Fetchall(Uploadrefno, Uploadstatus, UploadFromDate, UploadToDate, "CLM");
            }

            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult ClaimFileUploadEditor(ClaimFileUploadEditorModel model, HttpPostedFileBase SAPClaimfile, HttpPostedFileBase TACClaimFile, string FileRef)
        {


            MCASEntities db = new MCASEntities();
            model.Categorylist = LoadLookUpValue("ORGCategory");
            //model.CreatedBy = LoggedInUserName;
            //model.ModifiedBy = LoggedInUserName;
            model.OrgCategoryName = model.Hgetval;

            var schedulestarttime = model.Sdatetime.ToString();
            var dir = "";
            var dirPath = "";
            var fileServerpath = "";
            string SAPClaimfileName = string.Empty;
            string SAPClaimfileType = string.Empty;
            string TACClaimFileName = string.Empty;
            string TACClaimFileType = string.Empty;

            string foldername = string.Empty;
            string UploadFolderPath = "";
            string FileUploadPath = "";
            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();
            TempData["orgCategoryname"] = model.OrgCategoryName;

            try
            {
                if (ConfigurationManager.AppSettings["UploadFolder"] == null)
                {
                    ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                    return View(model);
                }
                else
                {
                    UploadFolderPath = ConfigurationManager.AppSettings["UploadFolder"].ToString();
                    if (String.IsNullOrEmpty(UploadFolderPath))
                    {
                        ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File Upload path not configured. Please contact administrator.";
                        return View(model);
                    }
                }
                fileServerpath = (from l in db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                if (String.IsNullOrEmpty(fileServerpath))
                {
                    ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                    return View(model);
                }
                FileUploadPath = fileServerpath.TrimEnd('\\') + "\\" + UploadFolderPath.TrimStart('\\');

                EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
                if (!objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
                {
                    ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                    return View(model);
                }

                SAPClaimfileType = Path.GetExtension(SAPClaimfile.FileName).TrimStart('.');
                TACClaimFileType = Path.GetExtension(TACClaimFile.FileName).TrimStart('.');
                var oname = model.OrgCategory == "Select" ? "" : model.OrgCategory;
                var orgName = model.OrgCategoryName == "Select" ? "" : model.OrgCategoryName;


                if (model.FileRefNo == null)
                {
                    if ((SAPClaimfile != null && SAPClaimfile.ContentLength > 0) && (TACClaimFile != null && TACClaimFile.ContentLength > 0))
                    {

                        var LastFile = db.MNT_FileUpload.OrderByDescending(x => x.FileId).FirstOrDefault();
                        int FileId = LastFile == null ? 1 : Convert.ToInt32(LastFile.FileId) + 1;
                        var FileNameid = "CLM_" + FileId.ToString();
                        SAPClaimfileName = Path.GetFileNameWithoutExtension(SAPClaimfile.FileName) + "_" + FileNameid + Path.GetExtension(SAPClaimfile.FileName);

                        FileId = Convert.ToInt32(FileId) + 1;
                        FileNameid = "CLM_" + FileId.ToString();
                        TACClaimFileName = Path.GetFileNameWithoutExtension(TACClaimFile.FileName) + "_" + FileNameid + Path.GetExtension(TACClaimFile.FileName);

                        db.Proc_TacFileSave(SAPClaimfileName, SAPClaimfileType, TACClaimFileName, TACClaimFileType, "", "", Convert.ToDateTime(schedulestarttime), "CLM", UploadFolderPath + @"\ClaimUpload", model.CreatedBy, model.OrgCategory, model.OrgCategoryName);
                        var FileRefNo = (from x in db.MNT_FileUpload where x.FileId == FileId select x.FileRefNo).FirstOrDefault();
                        foldername = FileRefNo;
                        TempData["FileRefClaim"] = FileRefNo;
                        ModelState.Remove("FileRefNo");
                        model.FileRefNo = FileRefNo;
                        dir = FileUploadPath + @"\ClaimUpload\" + foldername;
                        dirPath = UploadFolderPath + @"\ClaimUpload\" + foldername;
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        SAPClaimfile.SaveAs(Path.Combine(dir, SAPClaimfileName));
                        TACClaimFile.SaveAs(Path.Combine(dir, TACClaimFileName));
                        if (dir != null)
                            db.Proc_UpdateFolderPath(FileRefNo, dirPath);

                        model.Hf1 = SAPClaimfile.FileName.Split('\\').Last().ToString();
                        model.Hfn1 = SAPClaimfileName;
                        model.Hf2 = TACClaimFile.FileName.Split('\\').Last().ToString();
                        model.Hfn2 = TACClaimFileName;
                        ViewData["SuccessMsg"] = "Files Uploaded Successfully.";
                        model.CreatedBy = LoggedInUserName;
                        model.CreatedOn = DateTime.Now;
                        db.Dispose();
                        return View(model);
                    }
                    else
                    {
                        ViewData["SuccessMsg"] = "CSV file is not in  Proper format !!!";
                        return View(model);
                    }

                }
                else if (!String.IsNullOrEmpty(model.FileRefNo))
                {

                    if ((SAPClaimfile != null && SAPClaimfile.ContentLength > 0) && (TACClaimFile != null && TACClaimFile.ContentLength > 0))
                    {

                        var Filelist = (from d in db.MNT_FileUpload where d.FileRefNo == model.FileRefNo.ToUpper().Trim() select d).ToList();
                        model.FileRefNo = Filelist.FirstOrDefault().FileRefNo;

                        TempData["FileRefClaim"] = model.FileRefNo;

                        int SAPClaimfileid = (Filelist.Where(f => f.UploadType == "CLM").FirstOrDefault()).FileId;
                        int TACClaimfileid = (Filelist.Where(f => f.UploadType == "CLM_STD_CD").FirstOrDefault()).FileId;

                        model.FileId = Filelist.FirstOrDefault().FileId;
                        model.OrgCategory = Filelist.FirstOrDefault().OrganizationType;
                        model.OrgCategoryName = Filelist.FirstOrDefault().OrganizationName;
                        model.Categorylist = LoadLookUpValue("ORGCategory");
                        dir = FileUploadPath + @"\ClaimUpload\" + Filelist[0].FileRefNo;
                        dirPath = UploadFolderPath + @"\ClaimUpload\" + Filelist[0].FileRefNo;
                       
                        var FileNameid = "CLM_" + SAPClaimfileid.ToString();
                        SAPClaimfileName = Path.GetFileNameWithoutExtension(SAPClaimfile.FileName) + "_" + FileNameid + "_" + DateTime.Now.ToString("yyyyMMddmmss") + Path.GetExtension(SAPClaimfile.FileName);

                        FileNameid = "CLM_STD_CD" + TACClaimfileid.ToString();
                        TACClaimFileName = Path.GetFileNameWithoutExtension(TACClaimFile.FileName) + "_" + FileNameid + "_" + DateTime.Now.ToString("yyyyMMddmmss") + Path.GetExtension(TACClaimFile.FileName);

                        db.Proc_TacFileSaveUpdate(SAPClaimfileName, SAPClaimfileType, TACClaimFileName, TACClaimFileType, "", "", Convert.ToDateTime(schedulestarttime), "CLM", "", dirPath, model.ModifiedBy, oname, model.OrgCategoryName, 0, model.FileRefNo);

                        dir = FileUploadPath + @"\ClaimUpload\" + Filelist[0].FileRefNo;
                        dirPath = UploadFolderPath + @"\ClaimUpload\" + Filelist[0].FileRefNo;
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        SAPClaimfile.SaveAs(Path.Combine(dir, SAPClaimfileName));
                        TACClaimFile.SaveAs(Path.Combine(dir, TACClaimFileName));
                        if (dir != null)
                            db.Proc_UpdateFolderPath(model.FileRefNo, dirPath);

                        model.Hf1 = SAPClaimfile.FileName.Split('\\').Last().ToString();
                        model.Hfn1 = SAPClaimfileName;
                        model.Hf2 = TACClaimFile.FileName.Split('\\').Last().ToString();
                        model.Hfn2 = TACClaimFileName;
                        ViewData["SuccessMsg"] = "Files Uploaded Successfully.";
                        model.CreatedBy = LoggedInUserName;
                        model.CreatedOn = DateTime.Now;
                        model.ModifiedBy = LoggedInUserName;
                        model.ModifiedOn = DateTime.Now;
                        db.Dispose();
                        return View(model);
                    }
                    else
                    {
                        ViewData["SuccessMsg"] = "CSV file is not in  Proper format !!!";
                        return View(model);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                ViewData["SuccessMsg"] = "Error occured while uploading Files. " + ex.Message;
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public FileResult Download(string FileRefNo)
        {
            string CSVname = "";
            string path = "";
            string str = "";
            string contentType = "text/csv";
            //try
            //{
            var results = (from m in obj.MNT_FileUpload where m.FileRefNo == FileRefNo.ToUpper().Trim() select new { Uploadpath = m.UploadPath, Filename = m.FileName }).FirstOrDefault();

            CSVname = results.Filename;
            path = results.Uploadpath;
            //path = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "\\" + path);
            string fileServerpath = (from l in obj.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
            if (String.IsNullOrEmpty(fileServerpath))
            {
                ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                return null;
            }
            path = fileServerpath.TrimEnd('\\') + "\\" + path.TrimStart('\\');

            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();

            EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
            if (!objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
            {
                ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                return null;
            }
            str = path + "\\" + CSVname; //Server.MapPath(path + "/" + pdfname);
            return File(str, contentType, CSVname);
        }
        //    }

        //catch (System.Data.DataException)
        //     {
        //         ModelState.AddModelError("", "Unable to open Files.");
        //         reportString =" Unable to open Files.";
        //         return new FilePathResult(reportString, "application/text");
        //     }
        [HttpGet]
        public FileResult FileDownload(string FileRefNo, string FileName)
        {
            string CSVname = "";
            string path = "";
            string str = "";
            string contentType = "text/csv";
            //try
            //{
            var results = (from m in obj.MNT_FileUpload where m.FileRefNo == FileRefNo.ToUpper().Trim() && m.FileName == FileName.ToUpper().Trim() select new { Uploadpath = m.UploadPath, Filename = m.FileName }).FirstOrDefault();

            CSVname = results.Filename;
            path = results.Uploadpath;

            string fileServerpath = (from l in obj.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
            if (String.IsNullOrEmpty(fileServerpath))
            {
                ViewData["SuccessMsg"] = "Error: File Can not be uploaded as File server path not configured. Please contact administrator.";
                return null;
            }
            path = fileServerpath.TrimEnd('\\') + "\\" + path.TrimStart('\\');

            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();

            EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
            if (!objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
            {
                ViewData["SuccessMsg"] = "Error: Could not be impersonate the file server. Please contact administrator.";
                return null;
            }
            str = Path.Combine(path, CSVname); //Server.MapPath(path + "/" + pdfname);

            return File(str, contentType, CSVname);
        }
        # endregion

        #region "ClaimFileUploadViewEditor"
        public ActionResult ClaimFileUploadViewEditor(ClaimFileUploadViewEditorModel model)
        {
            model.Uploadstatuslist1 = ClaimFileUploadViewEditorModel.fetch();
            return View(model);
        }
        # endregion
        #region Download claim/log files
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
        public ActionResult FileDownLoader()
        {
            StringWriter sw = new StringWriter();
            List<object> list = null;
            string format = "CSV";
            format = MCASQueryString["Fileformat"] != null ? MCASQueryString["Fileformat"].ToString() : format;
            string fileRefNo = MCASQueryString["FileRefNo"] != null ? MCASQueryString["FileRefNo"].ToString() : "";
            string FileType = MCASQueryString["FileType"] != null ? MCASQueryString["FileType"].ToString() : "";
            string fileName = "DownloadFile.CSV";

            if (format.ToUpper().Equals("CSV") || format.ToUpper().Equals("TXT"))
            {
                string strProcName = "";
                DataTable dtResult = null;
                MCASEntities objEntity = new MCASEntities();
                try
                {
                    if (FileType == "ClaimDownload")
                    {
                        strProcName = "Proc_ExportProcessedDataClaimFiles";
                        fileName = "ClaimDownload_" + fileRefNo + "." + format;
                        objEntity.AddParameter("@FileRefNo", fileRefNo);
                    }
                    else if (FileType == "LOGFile")
                    {
                        strProcName = "Proc_ExportProcessedFailedDataFiles";
                        fileName = "ErrorLogFileDownload_" + fileRefNo + "." + format;
                        objEntity.AddParameter("@FileRefNo", fileRefNo);
                    }
                    dtResult = objEntity.ExecuteDataSet(strProcName, CommandType.StoredProcedure).Tables[0];
                    objEntity.ClearParameteres();

                }
                catch (Exception ex)
                {
                    NameValueCollection addInfo = new NameValueCollection();
                    addInfo.Add("Err Descriptor ", "Error while downloading file " + FileType + " for Ref No." + fileRefNo + ".");
                    addInfo.Add("entity_type", "FileDownLoad");
                    PublishException(ex, addInfo, 0, "FileDownLoad" + fileRefNo);
                    return View();
                }
                finally { objEntity.Dispose(); }

                int colCount = dtResult.Columns.Count;
                string[] columnHeader = new string[colCount];
                int i = 0;
                foreach (DataColumn col in dtResult.Columns)
                {
                    columnHeader[i] = col.ColumnName;
                    i++;
                }
                string fileheader = "\"" + String.Join("\",\"", columnHeader) + "\"";
                sw.WriteLine(fileheader);
                foreach (var item in dtResult.AsEnumerable())
                {
                    sw.WriteLine("\"" + String.Join("\",\"", item.ItemArray) + "\"");
                }

                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.ContentType = "text/csv";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.Write(sw);
                Response.End();
            }
            else if (format.ToUpper().Equals("EXCEL"))
            {
                GridView gv = new GridView();
                gv.DataSource = list;
                gv.AutoGenerateColumns = false;
                string columnNames = "PolicyNo,ClassDescription";
                string columnHeaders = "Policy No,Sub Class";
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
            return View();
        }
        #endregion
    }
}
