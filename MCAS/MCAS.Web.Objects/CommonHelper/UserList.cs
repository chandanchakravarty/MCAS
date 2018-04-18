using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web;
using System.Data.Objects;

namespace MCAS.Web.Objects.CommonHelper
{
    public class UserList : BaseModel
    {
        MCASEntities _db = new MCASEntities();
        #region properties
        public string SNO { get; set; }
        public string UserDispName { get; set; }
        #endregion

        #region Methods
        public static List<UserList> Fetch(bool AddAll = true)
        {
            MCASEntities _db = new MCASEntities();
            List<UserList> list = new List<UserList>();
            list = (from x in _db.MNT_Users orderby x.UserDispName select new UserList { SNO = System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.SNo).Trim(), UserDispName = x.UserDispName }).ToList();
            //  list = (from x in _db.MNT_Users orderby x.UserDispName select new UserList { SNO =Convert.ToString(x.SNo), UserDispName = x.UserDispName }).ToList();
            if (AddAll)
            {
                list.Insert(0, new UserList() { SNO = "", UserDispName = "[Select...]" });
            }
            return list;
        }


        #endregion
    }


    public class UserLogin : BaseModel
    {
        MCASEntities _db = new MCASEntities();
        #region properties
        private DateTime? __releaseDate = null;

        public int UserId { get; set; }
        [Required(ErrorMessage = "User Id is required.")]
        public string LoggedInUserId { get; set; }
        public string LoggedInUserDispName { get; set; }
        [Required(ErrorMessage = "User Password is required.")]
        [DataType(DataType.Password)]
        public string LoggedInUserPwd { get; set; }
        //[Required(ErrorMessage = "Please Select Branch.")]
        public string LoggedInUserBranch { get; set; }
        //[Required(ErrorMessage = "Please Select Country.")]
        public string LoggedInUserCountry { get; set; }
        public int LoggedInUserCountryId { get; set; }
        public string appVer { get; set; }
        public string AllowRetrievePwd { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate
        {
            get { return __releaseDate; }
            set { __releaseDate = value; }
        }
        public string SessionId { get; set; }

        public List<UserCountryListItems> usercountrylist { get; set; }
        public List<BranchLoginListItems> branchlist { get; set; }
        #endregion

        public string IsValid(string _username, string _password, string _userbranch, string _usercountry)
        {

            string lstrMessage = "";
            var lstrUserId = _db.Proc_GetUserId(_userbranch, _username, _password, _usercountry).FirstOrDefault();
            switch (lstrUserId)
            {
                case "0":
                    lstrMessage = "Invalid User";
                    break;
                case "-1":
                    lstrMessage = "Wrong password";
                    break;
                case "-2":
                    lstrMessage = "User disabled, contact administrator";
                    break;
                case "-3":
                    lstrMessage = "User does not have access to this branch.";
                    break;
                case "-4":
                    lstrMessage = "User does not have access to this country.";
                    break;
                default:
                    lstrMessage = lstrUserId;
                    break;
            }
            return lstrMessage;
        }

        public List<UserLogin> GetAppVersion()
        {
            var verlst = _db.Proc_GetAppVersionDetails().ToArray();
            List<UserLogin> List = new List<UserLogin>();
            if (verlst.Any())
            {
                foreach (var l in verlst)
                {
                    var appver = l.AppVersion;
                    var releaseDate = l.ReleaseDate;
                    List.Add(new UserLogin { appVer = l.AppVersion, ReleaseDate = l.ReleaseDate });
                }
            }
            _db.Dispose();
            return List;
        }

        public static void InsertLoginDetails(UserLogin model)
        {
            try
            {
                MCASEntities _db = new MCASEntities();
                var UserId = (from l in _db.MNT_Users where l.UserId == model.LoggedInUserId select l.SNo).FirstOrDefault();
                var SNo = new ObjectParameter("sNo", typeof(string));
                _db.Proc_SetLoginSessionId(UserId, model.SessionId);
                _db.Proc_InsertLoginAuditDetails(UserId.ToString(), model.LoggedInUserId, model.LoggedInUserDispName, model.LoggedInUserBranch, model.LoggedInUserCountryId.ToString(), model.LoggedInUserCountry, SNo);
                HttpContext.Current.Session["LoginAuditLogId"] = Convert.ToString(SNo.Value);
                
                _db.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void UpdateLoginDetails()
        {
            try
            {
                if (HttpContext.Current.Session["LoginAuditLogId"] != null)
                {
                    MCASEntities _db = new MCASEntities();
                    int Sno = Convert.ToInt32(HttpContext.Current.Session["LoginAuditLogId"]);
                    _db.Proc_UpdateLoginAuditDetails(Sno.ToString());
                    _db.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetEmailBodyForRetrievePassword(string userName, string userId, string password)
        {
            StringBuilder mailBody = new StringBuilder();            
            mailBody.AppendFormat("Dear {0},<br/>", userName);
            mailBody.AppendFormat("<br />");
            //mailBody.AppendFormat("<p>Please find your credentials below:</p><br/>");            
            mailBody.AppendFormat("Password is: {0} <br/>", password);
            mailBody.AppendFormat("Generated on: {0} <br/>", DateTime .Now.ToString ("yyyy-MM-dd HH:mm tt"));
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<b>Thank you.</b><br/>");
            return mailBody.ToString();
        }
    }
}

