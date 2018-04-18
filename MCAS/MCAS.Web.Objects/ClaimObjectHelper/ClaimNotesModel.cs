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
using System.Text.RegularExpressions;
using System.Data.Objects.SqlClient;
using MCAS.Web.Objects.Resources.ClaimProcessing;
using System.Globalization;
using System.IO;
using System.Data.Objects;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimNotesModel : BaseModel
    {
        #region Properties
        private DateTime? _noteDate = null;

        public int? NoteId { get; set; }
        public int PolicyId { get; set; }
        public int ClaimId { get; set; }
        public string NoteCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimNotesEditor), ErrorMessageResourceName = "RFVNoteDate")]
        [Display(Name = "Note Date")]
        public DateTime? NoteDate
        {
            get { return _noteDate; }
            set { _noteDate = value; }
        }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimProcessing.ClaimNotesEditor), ErrorMessageResourceName = "RFVNoteTime")]
        public string NoteTime { get; set; }
        public string ImageCode { get; set; }
        public string ImageId { get; set; }
        public string NotesDescription { get; set; }
        public string hiddenprop { get; set; }
        public string imageidval { get; set; }
        public string URL_PATH { get; set; }
        public string ResultMessage { get; set; }
        public int AccidentId { get; set; }
        public override string screenId
        {
            get
            {
                return "133";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "133";
            }

        }
        //        if(Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx" || Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"){
        //        [Required(ErrorMessageResourceType = typeof(ClaimNotesEditor), ErrorMessageResourceName = "RFVTPVehicleNo")]
        //    }else{
        //        [Required(ErrorMessageResourceType = typeof(ClaimNotesEditor), ErrorMessageResourceName = "RFVClaimantName")]
        //}
        [Required(ErrorMessageResourceType = typeof(ClaimNotesEditor), ErrorMessageResourceName = "RFVClaimantName")]
        public string ClaimantNames { get; set; }


        public List<ClaimantName> ClaimantNameList { get; set; }
        public class ClaimantName
        {
            public String Id { get; set; }
            public String Text { get; set; }
        }
        #endregion
        #region properies forvechileuploadlistall
        public int UPLOAD_FILE_ID { get; set; }
        public int UPLOAD_SERIAL_NO { get; set; }
        public string VEHICLE_REGISTRATION_NO { get; set; }
        public string VEHICLE_MAKE { get; set; }
        public string VEHICLE_MODEL { get; set; }
        public string VEHICLE_CLASS { get; set; }
        public string MODEL_DESCRIPTION { get; set; }
        public string BUS_CAPTAIN { get; set; }
        public string IMPORT_ACTION { get; set; }
        public string FileName { get; set; }

        #endregion


        #region "Public Shared Methods"
        public static List<ClaimNotesModel> Fetch(int? AccidentClaimId)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ClaimNotesModel>();
            var model = new ClaimNotesModel();
            //string CreatedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            //string ModifiedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).ModifiedBy;
            var NotesList = (from l in obj.CLM_Notes where l.AccidentId == AccidentClaimId select l);
            if (NotesList.Any())
            {
                foreach (var data in NotesList)
                {
                    var claimantName = "";
                    if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                    {
                        claimantName = (from m in obj.CLM_Claims where SqlFunctions.StringConvert((double)m.ClaimID).Trim() == data.ClaimantNames select m.TPVehicleNo).FirstOrDefault();
                    }
                    else
                    {
                        claimantName = (from m in obj.CLM_Claims where SqlFunctions.StringConvert((double)m.ClaimID).Trim() == data.ClaimantNames select m.ClaimantName).FirstOrDefault();
                    }
                    var claimRecordNo = (from m in obj.CLM_Claims where SqlFunctions.StringConvert((double)m.ClaimID).Trim() == data.ClaimantNames select m.ClaimRecordNo).FirstOrDefault();
                    var claimant = Convert.ToString(claimantName == null ? "" : claimantName);
                    var claimRN = Convert.ToString(claimRecordNo == null ? "" : claimRecordNo);

                    item.Add(new ClaimNotesModel() { NoteId = data.NoteId, ClaimId = data.ClaimId, AccidentId = (int)data.AccidentId, PolicyId = data.PolicyId, NoteCode = data.NoteCode, NoteDate = data.NoteDate, NoteTime = data.NoteTime, ClaimantNames = ((Regex.Replace(claimRN, "-", "") + (data.ClaimantNames == null ? "" : "/") + claimant)), NotesDescription = data.NotesDescription, ImageCode = data.ImageCode, ImageId = data.ImageId, CreatedBy = data.CreatedBy, CreatedOn = DateTime.Now });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public ClaimNotesModel Update()
        {
            MCASEntities obj = new MCASEntities();
            CLM_Notes ClaimNotesinfo;
            try
            {
                if (NoteId.HasValue)
                {
                    ClaimNotesinfo = obj.CLM_Notes.Where(x => x.NoteId == this.NoteId.Value).FirstOrDefault();
                    this.URL_PATH = "~/Uploads/Attachments/";
                    string[] Ignorelist = { "CreatedBy", "CreatedDate" };
                    this.CreatedBy = ClaimNotesinfo.CreatedBy ?? Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    DataMapper.Map(this, ClaimNotesinfo, true, Ignorelist);
                    ClaimNotesinfo.ModifiedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    ClaimNotesinfo.ModifiedDate = DateTime.Now;
                    obj.SaveChanges();
                    this.NoteId = ClaimNotesinfo.NoteId;
                    this.CreatedBy = ClaimNotesinfo.CreatedBy;
                    this.CreatedOn = Convert.ToDateTime(ClaimNotesinfo.CreatedDate);
                    this.ModifiedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    this.ModifiedOn = ClaimNotesinfo.ModifiedDate;
                    this.ClaimId = ClaimNotesinfo.ClaimId;
                    this.ResultMessage = "Records Updated Successfully";
                }
                else
                {
                    ClaimNotesinfo = new CLM_Notes();
                    this.URL_PATH = "~/Uploads/Attachments/";
                    DataMapper.Map(this, ClaimNotesinfo, true);
                    ClaimNotesinfo.CreatedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    ClaimNotesinfo.CreatedDate = DateTime.Now;
                    obj.CLM_Notes.AddObject(ClaimNotesinfo);
                    obj.SaveChanges();
                    this.ClaimId = ClaimNotesinfo.ClaimId;
                    this.NoteId = ClaimNotesinfo.NoteId;
                    this.CreatedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    this.CreatedOn = (DateTime)ClaimNotesinfo.CreatedDate;
                    this.ResultMessage = "Records Saved Successfully";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            obj.Dispose();
            return this;
        }
        #endregion

        public static List<ClaimNotesModel> Fetchall(int id)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<ClaimNotesModel>();
            var notelist = obj.Proc_Get_MIG_IL_VEHICLE_DETAIL(id).ToList();
            if (notelist.Any())
            {
                foreach (var data in notelist)
                {
                    item.Add(new ClaimNotesModel()
                    {
                        VEHICLE_REGISTRATION_NO = data.VEHICLE_REGISTRATION_NO,
                        VEHICLE_MAKE = data.VEHICLE_MAKE,
                        VEHICLE_MODEL = data.VEHICLE_MODEL,
                        VEHICLE_CLASS = data.VEHICLE_CLASS,
                        MODEL_DESCRIPTION = data.MODEL_DESCRIPTION,
                        BUS_CAPTAIN = data.BUS_CAPTAIN,
                        IMPORT_ACTION = data.IMPORT_ACTION,
                        FileName = data.FileName
                    });
                }
            }
            return item;
        }
        public List<ClaimantName> getClaimantName(int AccidentClaimId)
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

        public List<ClaimantName> getTPVehicleNo(int AccidentClaimId)
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


        public ClaimNotesModel FetchNote(ClaimNotesModel objmodel, int AccidentClaimId, int? ClaimId, int? NoteId)
        {
            MCASEntities obj = new MCASEntities();
            try
            {
                var notelist = (from tp in obj.CLM_Notes where tp.NoteId == NoteId select tp).FirstOrDefault();
                objmodel.NoteId = notelist.NoteId;
                objmodel.ClaimId = notelist.ClaimId;
                objmodel.PolicyId = notelist.PolicyId;
                objmodel.NoteCode = notelist.NoteCode;
                objmodel.NoteDate = notelist.NoteDate;
                objmodel.NoteTime = notelist.NoteTime;
                objmodel.ImageCode = notelist.ImageCode;
                objmodel.ImageId = notelist.ImageId;
                objmodel.NotesDescription = notelist.NotesDescription;
                if ((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx") || (Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc"))
                {
                    objmodel.ClaimantNameList = objmodel.getTPVehicleNo(Convert.ToInt32(AccidentClaimId));
                }
                else
                {
                    objmodel.ClaimantNameList = objmodel.getClaimantName(AccidentClaimId);
                }
                objmodel.ClaimantNames = notelist.ClaimantNames;
                objmodel.CreatedBy = notelist.CreatedBy == null ? " " : notelist.CreatedBy;
                if (notelist.CreatedDate != null)
                    objmodel.CreatedOn = (DateTime)notelist.CreatedDate;
                else
                    objmodel.CreatedOn = DateTime.MinValue;
                objmodel.ModifiedBy = notelist.ModifiedBy == null ? " " : notelist.ModifiedBy;
                objmodel.ModifiedOn = notelist.ModifiedDate;
                objmodel.ResultMessage = "Display";
                if (ClaimId.HasValue)
                    objmodel.ClaimId = (int)ClaimId;
                if (NoteId.HasValue)
                    objmodel.NoteId = NoteId;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            obj.Dispose();
            return objmodel;
        }
    }
}
