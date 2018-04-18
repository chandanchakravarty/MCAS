using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Data.Objects.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimAttachmentsModel : BaseModel
    {
        #region Properties
        private DateTime? _attachDateTime = null;

        public int? AttachId { get; set; }
        public int? ClaimId { get; set; }
        public string AttachLoc { get; set; }
        public string hid1 { get; set; }
        public int? AttachEntId { get; set; }
        public int? AccidentId { get; set; }

        //  [Required(ErrorMessage = "Upload File is required.")]
        public string AttachFileName { get; set; }

        public string IsUrlValid { get; set; }


        public string Message { get; set; }

        public string AttachFilePath { get; set; }


        //[Required(ErrorMessage = "Claimant Name is required.")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAttachmentsEditor), ErrorMessageResourceName = "RFVClaimantName")]
        public string ClaimantName { get; set; }
        public string AttachEntityType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AttachDateTime
        {
            get { return _attachDateTime; }
            set { _attachDateTime = value; }
        }
        public string AttachFileDesc { get; set; }
        public int? AttachPolicyId { get; set; }
        public string FilePath { get; set; }

        //[Required(ErrorMessage = "Attachment Type is required.")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAttachmentsEditor), ErrorMessageResourceName = "RFVAttachmentType")]
        [DisplayName("Attachment Type is required.")]
        public string AttachType { get; set; }

        public string AttachFileType { get; set; }
        public string hiddenprop { get; set; }
        public string HiddenclaimMode { get; set; }
        public string HiddenAccidentClaimId { get; set; }
        public int? HchkhasClaim { get; set; }
        public List<LookUpListItems> AttachTypeList { get; set; }
        public List<ClaimantName> ClaimantNameList { get; set; }

        public override string screenId
        {
            get
            {
                return "135";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "135";
            }

        }


        #endregion

        #region "Public Shared Methods"
        public static List<ClaimAttachmentsModel> Fetch(int? AccidentClaimId)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ClaimAttachmentsModel>();
            var model = new ClaimAttachmentsModel();
            string CreatedBy1 = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
            var attachmentList = (from l in obj.MNT_AttachmentList where l.AccidentId == AccidentClaimId select l);
            if (attachmentList.Any())
            {
                string folder = string.Empty;
                foreach (var data in attachmentList)
                {
                    if (data.AttachType == "F1")
                    {
                        folder = "3rd Party's Documents";
                    }
                    else if (data.AttachType == "F2")
                    {
                        folder = "Insured's Documents";
                    }
                    else if (data.AttachType == "F3")
                    {
                        folder = "Correspondences";
                    }
                    else if (data.AttachType == "F4")
                    {
                        folder = "Internal Documents";
                    }
                    var claimantName = "";
                    if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                    {
                        claimantName = (from m in obj.CLM_Claims where m.ClaimID == data.ClaimID select m.TPVehicleNo).FirstOrDefault();
                    }
                    else
                    {
                        claimantName = (from m in obj.CLM_Claims where m.ClaimID == data.ClaimID select m.ClaimantName).FirstOrDefault();
                    }
                    var claimRecordNo = (from m in obj.CLM_Claims where m.ClaimID == data.ClaimID select m.ClaimRecordNo).FirstOrDefault();
                    var claimant = Convert.ToString(claimantName == null ? "" : claimantName);
                    var claimRN = Convert.ToString(claimRecordNo == null ? "" : claimRecordNo);
                    item.Add(new ClaimAttachmentsModel()
                    {
                        AttachId = data.AttachId,
                        AttachEntId = data.AttachEntId,
                        ClaimId = data.ClaimID,
                        AttachPolicyId = data.AttachPolicyId,
                        AttachFileName = Path.GetFileNameWithoutExtension(data.AttachFileName).EndsWith("_" + data.AttachId) ? Path.GetFileNameWithoutExtension(data.AttachFileName).Substring(0, (Path.GetFileNameWithoutExtension(data.AttachFileName).Length - (data.AttachId.ToString().Length + 1))) + Path.GetExtension(data.AttachFileName) : data.AttachFileName,
                        AttachFileType = data.AttachFileType,
                        AttachDateTime = data.AttachDateTime,
                        AttachType = folder,
                        AttachFileDesc = data.AttachFileDesc,
                        CreatedBy = CreatedBy1,
                        ClaimantName = ((Regex.Replace(claimRN, "-", "") + (data.ClaimantName == null ? "" : "/") + claimant))
                    });
                }
            }
            return item;
        }
        #endregion

        #region "Methods"
        public ClaimAttachmentsModel Update(HttpPostedFileBase AttachFileName)
        {
            MCASEntities obj = new MCASEntities();
            MNT_AttachmentList attachments;
            var path = "";
            if (obj.Connection.State == System.Data.ConnectionState.Closed)
                obj.Connection.Open();
            using (System.Data.Common.DbTransaction transaction = obj.Connection.BeginTransaction())
            {
                try
                {
                    if (Path.GetFileNameWithoutExtension(this.AttachFileName).Length > 180)
                    {
                        this.Message = "Maximum file name can be of 180 character.";
                        obj.Dispose();
                        return this;
                    }
                    else if (AttachId.HasValue)
                    {
                        attachments = obj.MNT_AttachmentList.Where(x => x.AttachId == this.AttachId.Value).FirstOrDefault();
                        string[] IgnoreList = { "IsActive", "CreatedBy", "CreatedDate", "ClaimID", "ClaimantName", "AttachEntityType", "AttachEntId" };
                        DataMapper.Map(this, attachments, true, IgnoreList);
                        if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                        {
                            attachments.ClaimantName = (from m in obj.CLM_Claims where m.ClaimID == this.ClaimId select m.TPVehicleNo).FirstOrDefault();
                            attachments.ClaimID = this.ClaimId;
                        }
                        attachments.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        attachments.ModifiedDate = DateTime.Now;
                        obj.SaveChanges();
                        this.AttachId = attachments.AttachId;
                        this.ClaimId = attachments.ClaimID;
                        this.ClaimantName = Convert.ToString(attachments.ClaimID);
                        this.CreatedBy = attachments.CreatedBy;
                        this.CreatedOn = Convert.ToDateTime(attachments.CreatedDate);
                        this.ModifiedBy = attachments.ModifiedBy;
                        this.ModifiedOn = attachments.ModifiedDate;
                        this.Message = "Records Updated Successfully.";
                    }
                    else
                    {
                        attachments = new MNT_AttachmentList();
                        DataMapper.Map(this, attachments, true);
                        attachments.AttachEntityType = "Claimant";
                        attachments.ClaimID = Convert.ToInt32(this.ClaimantName);
                        attachments.ClaimantName = (from l in obj.CLM_Claims where l.ClaimID == attachments.ClaimID select l.ClaimantName).FirstOrDefault();
                        attachments.AttachEntId = Convert.ToInt32(this.ClaimantName);
                        attachments.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        attachments.CreatedDate = DateTime.Now;
                        attachments.IsActive = "Y";
                        if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                        {
                            attachments.ClaimantName = (from m in obj.CLM_Claims where m.ClaimID == attachments.ClaimID select m.TPVehicleNo).FirstOrDefault();
                        }
                        obj.MNT_AttachmentList.AddObject(attachments);
                        obj.SaveChanges();
                        this.ClaimId = attachments.ClaimID;
                        this.AttachId = attachments.AttachId;
                        this.AttachEntId = attachments.AttachEntId;
                        this.ClaimantName = Convert.ToString(attachments.ClaimID);
                        this.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                        this.CreatedOn = DateTime.Now;
                        this.Message = "Records Saved Successfully.";
                    }
                    try
                    {
                        if (AttachFileName != null)
                        {
                            var Folderpath = this.AttachFilePath + "\\" + this.FilePath + "\\" + this.AttachEntId;
                            if (!Directory.Exists(Folderpath))
                            {
                                Directory.CreateDirectory(Folderpath);
                            }
                            path = Path.Combine(Folderpath, Path.GetFileNameWithoutExtension(this.AttachFileName) + "_" + attachments.AttachId + Path.GetExtension(attachments.AttachFileName));
                            AttachFileName.SaveAs(path);
                        }
                        this.IsUrlValid = "Y";
                    }
                    catch (Exception ex)
                    {
                        this.Message = "Some error occurs";
                        transaction.Rollback();
                        obj.Dispose();
                        return this;
                        throw (ex);
                    }
                    transaction.Commit();
                    obj.Proc_FileNameForMNT_AttachentList(Path.GetFileNameWithoutExtension(this.AttachFileName) + "_" + this.AttachId + Path.GetExtension(this.AttachFileName), this.AttachId);
                    obj.Dispose();
                    return this;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    obj.Dispose();
                    throw (ex);
                }

            }
        }

        #endregion

        public static List<LookUpListItems> fetchlookup(string category, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list, list11 = new List<LookUpListItems>();
            var model = new ClaimAttachmentsModel();
            var names = new int[] { 221, 222, 223, 224 };
            var fnames = new string[] { "3rd Party's Documents", "Insured's Documents", "Correspondences", "Internal Documents" };
            string CreatedBy = System.Web.HttpContext.Current.Session["LoggedInUserId"].ToString();
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            var gid1 = (from x in obj.MNT_GroupsMaster where x.GroupId == gid select x.GroupId).FirstOrDefault();
            var gid2 = Convert.ToString(gid1);
            var list1 = (from x in obj.MNT_GroupPermission where x.GroupId == gid2 && x.Write == true && names.Contains(x.MenuId) orderby x.MenuId ascending select x.MenuId).ToArray();
            var nlist1 = (from x in obj.MNT_Menus where list1.Contains(x.MenuId) select x.AdminDisplayText).ToArray();
            list = (from l in obj.MNT_Lookups where l.Category == category select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
            var names2 = fnames.Except(nlist1).ToArray();
            List<string> names3 = new List<string>();
            for (var i = 0; i < names2.Length; i++)
            {
                names3.Add(Regex.Replace(names2[i], @"\s+", ""));
            }
            var names1 = names3.ToArray();
            foreach (var data in list)
            {
                var index = Array.IndexOf(names1, Regex.Replace(data.Lookup_desc, @"\s+", ""));
                if (index < 0)
                {
                    list11.Add(new LookUpListItems() { Lookup_desc = data.Lookup_desc, Lookup_value = data.Lookup_value });
                }
            }
            list11.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            return list11;
        }

        public static List<LookUpListItems> fetchlookupRead(string category, bool addAll = false, bool addNone = false)
        {
            MCASEntities obj = new MCASEntities();
            List<LookUpListItems> list, list11 = new List<LookUpListItems>();
            var model = new ClaimAttachmentsModel();
            var names = new int[] { 221, 222, 223, 224 };
            var fnames = new string[] { "3rd Party's Documents", "Insured's Documents", "Correspondences", "Internal Documents" };
            string CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            var gid1 = (from x in obj.MNT_GroupsMaster where x.GroupId == gid select x.GroupId).FirstOrDefault();
            var gid2 = Convert.ToString(gid1);
            var list1 = (from x in obj.MNT_GroupPermission where x.GroupId == gid2 && x.Read == true && names.Contains(x.MenuId) orderby x.MenuId ascending select x.MenuId).ToArray();
            var nlist1 = (from x in obj.MNT_Menus where list1.Contains(x.MenuId) select x.AdminDisplayText).ToArray();
            list = (from l in obj.MNT_Lookups where l.Category == category select new LookUpListItems { Lookup_value = l.Lookupvalue, Lookup_desc = l.Lookupdesc }).ToList();
            var names2 = fnames.Except(nlist1).ToArray();
            List<string> names3 = new List<string>();
            for (var i = 0; i < names2.Length; i++)
            {
                names3.Add(Regex.Replace(names2[i], @"\s+", ""));
            }
            var names1 = names3.ToArray();
            foreach (var data in list)
            {
                var index = Array.IndexOf(names1, Regex.Replace(data.Lookup_desc, @"\s+", ""));
                if (index < 0)
                {
                    list11.Add(new LookUpListItems() { Lookup_desc = data.Lookup_desc, Lookup_value = data.Lookup_value });
                }
            }
            list11.Insert(0, new LookUpListItems() { Lookup_value = "", Lookup_desc = "[Select...]" });
            return list11;
        }



        public static string delper()
        {
            MCASEntities obj = new MCASEntities();
            var names = new int[] { 221, 222, 223, 224 };
            var fnames = new string[] { "3rd Party's Documents", "Insured's Documents", "Correspondences", "Internal Documents" };
            var model = new BaseModel();
            string CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            var gid1 = (from x in obj.MNT_GroupsMaster where x.GroupId == gid select x.GroupId).FirstOrDefault();
            var gid2 = Convert.ToString(gid1);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < names.Length; i++)
            {

                var mid = Convert.ToInt32(names[i]);
                var per = (from l in obj.MNT_GroupPermission where l.MenuId == mid && l.GroupId == gid2 select l.Delete).FirstOrDefault();
                if (per == false)
                {
                    sb.Append(Regex.Replace(fnames[i], @"\s+", ""));
                    sb.Append("~");
                }
            }
            return sb.ToString();
        }

        public static string writeper()
        {
            MCASEntities obj = new MCASEntities();
            var names = new int[] { 221, 222, 223, 224 };
            var fnames = new string[] { "3rd Party's Documents", "Insured's Documents", "Correspondences", "Internal Documents" };
            var model = new BaseModel();
            string CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            var gid1 = (from x in obj.MNT_GroupsMaster where x.GroupId == gid select x.GroupId).FirstOrDefault();
            var gid2 = Convert.ToString(gid1);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < names.Length; i++)
            {

                var mid = Convert.ToInt32(names[i]);
                var per = (from l in obj.MNT_GroupPermission where l.MenuId == mid && l.GroupId == gid2 select l.Write).FirstOrDefault();
                if (per == false)
                {
                    sb.Append(Regex.Replace(fnames[i], @"\s+", ""));
                    sb.Append("~");
                }
            }
            return sb.ToString();
        }

        public static string Readper()
        {
            MCASEntities obj = new MCASEntities();
            var names = new int[] { 221, 222, 223, 224 };
            var fnames = new string[] { "3rd Party's Documents", "Insured's Documents", "Correspondences", "Internal Documents" };
            var model = new BaseModel();
            string CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
            var gid = (from x in obj.MNT_Users where x.UserId == CreatedBy select x.GroupId).FirstOrDefault();
            var gid1 = (from x in obj.MNT_GroupsMaster where x.GroupId == gid select x.GroupId).FirstOrDefault();
            var gid2 = Convert.ToString(gid1);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < names.Length; i++)
            {

                var mid = Convert.ToInt32(names[i]);
                var per = (from l in obj.MNT_GroupPermission where l.MenuId == mid && l.GroupId == gid2 select l.Read).FirstOrDefault();
                if (per == true)
                {
                    sb.Append(Regex.Replace(fnames[i], @"\s+", ""));
                    sb.Append("~");
                }
            }
            return sb.ToString();
        }
        public static List<ClaimantName> getClaimantName(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var item = new List<ClaimantName>();
            var Attachmentlist = db.Proc_GetClaimantName(AccidentClaimId).ToList();
            if (Attachmentlist.Any())
            {
                foreach (var data in Attachmentlist)
                {
                    item.Add(new ClaimantName()
                    {
                        Id = Convert.ToString(data.ClaimID),
                        Text = string.IsNullOrEmpty(data.ClaimantName) ? "" : data.ClaimantName
                    });
                }
            }
            item.Insert(0, new ClaimantName() { Id = "", Text = "[Select...]" });
            return item;
        }
        public static List<ClaimantName> getTPVehicleNo(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var item = new List<ClaimantName>();
            var Attachmentlist = db.Proc_GetClaimTPVehicleNo(AccidentClaimId).ToList();
            if (Attachmentlist.Any())
            {
                foreach (var data in Attachmentlist)
                {
                    item.Add(new ClaimantName()
                    {
                        Id = Convert.ToString(data.ClaimId),
                        Text = string.IsNullOrEmpty(data.TPVehicleNo) ? "" : data.TPVehicleNo
                    });
                }
            }
            item.Insert(0, new ClaimantName() { Id = "", Text = "[Select...]" });
            return item;
        }


        public static string GetFolderPath(int? AttachId)
        {
            try
            {
                MCASEntities _db = new MCASEntities();
                string fileServerpath = (from l in _db.Proc_GetFileServerPath() select l.FileServerPath).FirstOrDefault();
                if (String.IsNullOrEmpty(fileServerpath))
                {
                    return "1";
                }
                if (String.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["UploadFolder"])))
                {
                    return "2";
                }
                string result = fileServerpath.TrimEnd('\\') + "\\" + Convert.ToString(ConfigurationManager.AppSettings["UploadFolder"]).TrimStart('\\') + "\\" + (from l in _db.MNT_AttachmentList where l.AttachId == AttachId select l.FilePath).FirstOrDefault() + "\\" + (from l in _db.MNT_AttachmentList where l.AttachId == AttachId select l.AttachEntId).FirstOrDefault();
                _db.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string GetFileName(int? AttachId)
        {
            try
            {
                MCASEntities _db = new MCASEntities();
                var result = (from l in _db.MNT_AttachmentList where l.AttachId == AttachId select l.AttachFileName).FirstOrDefault();
                _db.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        public static string ChkIsUrlValid(int? AttachId, string file = "", string path = "", string FName = "", string AttachEntId = "")
        {
            var FolderPath = path == "" ? ClaimAttachmentsModel.GetFolderPath(AttachId) : path + file + "\\" + AttachEntId;
            var FileName = FName == "" ? ClaimAttachmentsModel.GetFileName(AttachId) : FName;
            var NewFileName = Path.GetFileNameWithoutExtension(FileName).EndsWith("_" + AttachId.Value) ? Path.GetFileNameWithoutExtension(FileName).Substring(0, (Path.GetFileNameWithoutExtension(FileName).Length - (AttachId.Value.ToString().Length + 1))) : FileName;

            var filePath = (FolderPath + "\\" + NewFileName + "_" + AttachId + Path.GetExtension(FileName)).Replace("\\", "/");
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();
            return filePath;

        }



        public static ClaimAttachmentsModel fetchModel(ClaimAttachmentsModel objmodel, int? AttachId, int AccidentClaimId, int? ClaimId)
        {
            try
            {
                MCASEntities obj = new MCASEntities();
                var Attachlist = (from ca in obj.MNT_AttachmentList where ca.AttachId == AttachId select ca).FirstOrDefault();

                DataMapper.Map(Attachlist, objmodel, true);
                objmodel.AttachFilePath = ClaimAttachmentsModel.GetFolderPath(objmodel.AttachId);
                objmodel.ClaimantName = Convert.ToString(Attachlist.ClaimID);
                if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                {
                    objmodel.ClaimantNameList = ClaimAttachmentsModel.getTPVehicleNo(AccidentClaimId);
                }
                else
                {
                    objmodel.ClaimantNameList = getClaimantName(AccidentClaimId);
                }
                objmodel.CreatedOn = Convert.ToDateTime(Attachlist.CreatedDate);
                objmodel.CreatedBy = Attachlist.CreatedBy;
                if (Attachlist.ModifiedDate != null)
                {
                    objmodel.ModifiedOn = Attachlist.ModifiedDate;
                    objmodel.ModifiedBy = Attachlist.ModifiedBy;
                }

                objmodel.AttachTypeList = ClaimAttachmentsModel.fetchlookup("ATTACHMENT");
                objmodel.IsUrlValid = System.IO.File.Exists(ClaimAttachmentsModel.ChkIsUrlValid(AttachId.Value)) ? "Y" : "N";
                obj.Dispose();
                if (ClaimId.HasValue)
                    objmodel.AttachEntId = (int)ClaimId;
                if (AttachId.HasValue)
                    objmodel.AttachId = AttachId;

                objmodel.AttachFileName = Path.GetFileNameWithoutExtension(objmodel.AttachFileName).EndsWith("_" + objmodel.AttachId.Value) ? Path.GetFileNameWithoutExtension(objmodel.AttachFileName).Substring(0, (Path.GetFileNameWithoutExtension(objmodel.AttachFileName).Length - (objmodel.AttachId.Value.ToString().Length + 1))) + Path.GetExtension(objmodel.AttachFileName) : objmodel.AttachFileName;
                obj.Dispose();


                return objmodel;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public class permision
    {
        public string folderpermission { get; set; }
    }

    public class ClaimantName
    {
        public String Id { get; set; }
        public String Text { get; set; }
    }


}
