using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web.Mvc;
using System.ComponentModel;
using System.Web;

namespace MCAS.Web.Objects.CommonHelper
{
    public class UserAdminModel : BaseModel
    {

        #region Properties
        public int? SNo { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        //[Remote("UserNameExists", "UserAdmin", "Username is already taken.", AdditionalFields = "InitialUserId")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string LoginPassword { get; set; }
        [Required(ErrorMessage = "Group is required.")]
        public int GroupId { get; set; }
        public string Hgetval { get; set; }
        public string ResultMessage { get; set; }
        public string GroupName { get; set; }
        [Required(ErrorMessage = "User Full Name is required.")]
        public string UserFullName { get; set; }
        [Required(ErrorMessage = "User Display Name is required.")]
        public string UserDispName { get; set; }
        //[Required(ErrorMessage = "Entity is required.")]
        //public string BranchCode { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string hiddentab { get; set; }

        [Display(Name = "Payment Limit")]
        public decimal? PaymentLimit { get; set; }


        [Display(Name = "Credit Note Limit")]
        public decimal? CreditNoteLimit { get; set; }

        public string IsActive { get; set; }
        public string IsEnabled { get; set; }
        public string UserTypeCode { get; set; }
        public string FirstTime { get; set; }
        public string MainClass { get; set; }
        public string usercountry { get; set; }
        public string[] orgCategory { get; set; }
        public int? FAL_OD { get; set; }
        public int? FAL_PDBI { get; set; }
        public string DID_No { get; set; }
        public string FAX_No { get; set; }
        public string CategoryType { get; set; }
        public Boolean LOGApproverCheckbox { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string EmailId { get; set; }
        //[Required]
        //[Display(Name = "AccessLevel")]
        //public byte? AccessLevel { get; set; }
        public Boolean CurrentUserLogChkName { get; set; }
        public string Organization { get; set; }
        public string LogChkName { get; set; }
        public string LoginRoleCode { get; set; }
        public string OrganizationCode { get; set; }
        public string organizationName { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        //By Apurva
        [Required(ErrorMessage = "Initial field is required.")]
        public string Initial { get; set; }
        public List<UserCountryListItems> countrylist { get; set; }

        public List<LookUpListItems> Statuslist { get; set; }
        public IEnumerable<SelectListItem> generallookupvalue { get; set; }


        public List<ProductsListItems> productslist { get; set; }
        public List<UserCountryListItems> usercountrylist { get; set; }
        public List<BranchListItems> branchlist { get; set; }
        public List<DepartmentsListItems> deptlist { get; set; }
        public List<GroupMastersListItems> groupslist { get; set; }
        public List<ProductsCountryListItems> userproductcountrylist { get; set; }
        public List<GroupPermissionListItems> permissionlistlist { get; set; }
        public List<FALModel> FALODlist { get; set; }
        public List<FALModel> FALPDBIlist { get; set; }
        public string hidinput { get; set; }

        public List<OrgCountryListItems> usercategorylist { get; set; }

        public List<UserAdminModel.OrgAccessCategory> OrgAccessCategoryList { get; set; }

        public override string screenId
        {
            get
            {
                return "262";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "261";
            }

        }
        #endregion

        #region "Public Shared Methods"
        public static string converttooriginalname(string p)
        {
            return p == "BU" ? "Bus" : p == "TR" ? "Train" : p == "TX" ? "Taxi" : p == "PC" ? "Private Cars" : p == "PB" ? "Private Bus" : p == "RV" ? "Rental Vehicles" : "";
        }

        public static List<UserAdminModel> FetchOrgCategory(string userid)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<UserAdminModel>();
            try
            {
                var model = new UserAdminModel();
                var orgName = (from x in obj.MNT_UserOrgAccess
                               join y in obj.MNT_OrgCountry on x.OrgName equals y.CountryOrgazinationCode
                               where x.UserId == userid
                               select new { orgName = y.OrganizationName, orgcat = x.OrgCode }).ToList().Select(data => new UserAdminModel()
                               {
                                   OrganizationCode = data.orgcat == "BU" ? "Bus" : data.orgcat == "TR" ? "Train" : data.orgcat == "TX" ? "Train" : data.orgcat == "PC" ? "Private Cars" : data.orgcat == "PB" ? "Private Bus" : data.orgcat == "RV" ? "Rental Vehicles" : "",
                                   organizationName = data.orgName
                               }).ToList();
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            obj.Dispose();
            return item;
        }


        public class OrgCountryCategoryListItems : BaseModel
        {
            #region Properties
            public int? UserCountryProductId { get; set; }
            public bool CheckedStatus { get; set; }
            public string LookupValue { get; set; }
            public string LookupDesc { get; set; }
            #endregion
        }

        public class OrgAccessCategory : BaseModel
        {
            public string CountryOrganizationCode { get; set; }
            public string OrganizationName { get; set; }
        }


        #endregion

        #region Methods
        public UserAdminModel Update(MCASEntities obj)
        {
            MNT_Users userinfo;
            MNT_Users userinfoForLogChk;
            userinfo = obj.MNT_Users.Where(x => x.SNo == this.SNo.Value).FirstOrDefault();
            MNT_UserOrgAccess userOrgAccess;
            userinfoForLogChk = (from l in obj.MNT_Users where l.LOGApproverCheckbox == true select l).FirstOrDefault();
            if (userinfoForLogChk != null)
            {
                userinfoForLogChk.LOGApproverCheckbox = false;
                obj.SaveChanges();
            }
            try
            {
                if (SNo.HasValue && SNo != 0)
                {
                    userOrgAccess = obj.MNT_UserOrgAccess.Where(x => x.UserId == userinfo.UserId).FirstOrDefault();
                    if (userOrgAccess != null && (userinfo.UserId != this.UserId))
                    {
                        obj.Proc_UpdateMnt_OrgAcessUserId(userinfo.UserId, this.UserId);
                    }
                    userinfo.UserId = this.UserId;
                    userinfo.UserFullName = this.UserFullName;
                    userinfo.UserDispName = this.UserDispName;
                    userinfo.EmailId = this.EmailId;
                    userinfo.GroupId = this.GroupId;
                    userinfo.DeptCode = this.DeptCode;
                    //userinfo.BranchCode = this.BranchCode;
                    userinfo.FAL_OD = this.FAL_OD;
                    userinfo.FAL_PDBI = this.FAL_PDBI;
                    userinfo.IsEnabled = this.IsEnabled;
                    userinfo.DID_No = this.DID_No;
                    userinfo.FAX_No = this.FAX_No;
                    userinfo.UserTypeCode = this.UserTypeCode;
                    userinfo.Initial = this.Initial;
                    //userinfo.AccessLevel = this.AccessLevel;
                    userinfo.FirstTime = this.FirstTime;
                    userinfo.MainClass = this.MainClass;
                    userinfo.ModifiedBy = ModifiedBy;
                    userinfo.OrgCategory = this.Country;
                    userinfo.LOGApproverCheckbox = this.LOGApproverCheckbox;
                    userinfo.ModifiedDate = DateTime.Now;
                    userinfo.LoginPassword = EnDecryption.EncryptMessage(this.LoginPassword);
                    userinfo.IsActive = this.IsActive == "Active" ? "Y" : "N";
                    obj.SaveChanges();
                    this.ResultMessage = "Record updated successfully.";
                    this.SNo = userinfo.SNo;
                    this.CreatedBy = userinfo.CreatedBy;
                    this.CreatedOn = userinfo.CreatedDate == null ? DateTime.MinValue : (DateTime)userinfo.CreatedDate;
                    this.ModifiedOn = userinfo.ModifiedDate;
                    this.ModifiedBy = ModifiedBy;
                }
                else
                {
                    userinfo = new MNT_Users();
                    userinfo.UserId = this.UserId;
                    userinfo.LoginPassword = EnDecryption.EncryptMessage(this.LoginPassword);
                    userinfo.UserFullName = this.UserFullName;
                    userinfo.UserDispName = this.UserDispName;
                    userinfo.EmailId = this.EmailId;
                    userinfo.GroupId = this.GroupId;
                    userinfo.DeptCode = this.DeptCode;
                    userinfo.Initial = this.Initial;
                    //userinfo.BranchCode = this.BranchCode;
                    userinfo.FAL_OD = this.FAL_OD;
                    userinfo.FAL_PDBI = this.FAL_PDBI;
                    userinfo.IsActive = this.IsActive == "Active" ? "Y" : "N";
                    userinfo.IsEnabled = this.IsEnabled;
                    userinfo.DID_No = this.DID_No;
                    userinfo.FAX_No = this.FAX_No;
                    userinfo.UserTypeCode = this.UserTypeCode;
                    //userinfo.AccessLevel = this.AccessLevel;
                    userinfo.FirstTime = this.FirstTime;
                    userinfo.MainClass = this.MainClass;
                    userinfo.OrgCategory = this.Country;
                    userinfo.CreatedBy = Convert.ToString(this.CreatedBy);
                    userinfo.CreatedDate = DateTime.Now;
                    obj.MNT_Users.AddObject(userinfo);
                    obj.SaveChanges();
                    this.SNo = userinfo.SNo;
                    this.CreatedOn = (DateTime)userinfo.CreatedDate;
                    this.ResultMessage = "Record saved successfully.";
                }
                this.CurrentUserLogChkName = UserAdminModel.GetCurrentUserLogChkNameUserid(this.UserId);
                this.LogChkName = UserAdminModel.GetLogChkName();
                return this;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public static void DeleteAllValuesFromOrgAcessAccordinGToUserid(string UserId, string orgcat)
        {
            MCASEntities _db = new MCASEntities();
            if (orgcat != (from l in _db.MNT_Users where l.UserId == UserId select l.OrgCategory).FirstOrDefault()) _db.Proc_DeleteMnt_OrgAcessUserId(UserId);
            _db.Dispose();
        }
        #endregion

        public static IQueryable<UserAdminModel> GetResult(string UserId, string UserDispName, string GroupName, string DeptCode, string MainClass, string UserStatus)
        {
            MCASEntities db = new MCASEntities();
            var searchResult = Enumerable.Empty<UserAdminModel>().AsQueryable();
            try
            {
                UserId = UserId == null ? "" : UserId;
                UserDispName = UserDispName == null ? "" : UserDispName;
                GroupName = GroupName == null ? "" : GroupName;
                DeptCode = DeptCode == null ? "" : DeptCode;
                UserStatus = UserStatus == null ? "" : UserStatus;
                searchResult = (from u in db.MNT_Users
                                join g in db.MNT_GroupsMaster on u.GroupId equals g.GroupId into ug
                                from g in ug.DefaultIfEmpty()
                                join d in db.MNT_Department on u.DeptCode equals d.DeptCode into ud
                                from d in ud.DefaultIfEmpty()
                                where
                               u.UserId.Contains(UserId) &&
                               u.UserDispName.Contains(UserDispName) &&
                               g.GroupName.Contains(GroupName) &&
                               d.DeptCode.Contains(DeptCode) &&
                               u.IsActive.Contains(UserStatus)
                               select new UserAdminModel
                                {
                                    SNo = u.SNo,
                                    UserId = u.UserId,
                                    UserDispName = u.UserDispName,
                                    GroupName = g.GroupName,
                                    DeptCode = d.DeptName,
                                    IsActive = u.IsActive
                                }).Distinct().AsQueryable();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return searchResult;
        }

        public static string GetLogChkName()
        {
            MCASEntities _db = new MCASEntities();
            string result = string.Empty;
            try
            {
                result = (from l in _db.MNT_Users where l.LOGApproverCheckbox == true select l.UserDispName).FirstOrDefault() ?? (from l in _db.MNT_Users where l.LOGApproverCheckbox == true select l.UserId).FirstOrDefault() ?? "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return result;
        }

        public static bool GetCurrentUserLogChkName(int uid)
        {
            MCASEntities _db = new MCASEntities();
            bool result;
            try
            {
                result = (from l in _db.MNT_Users where l.SNo == uid select l.LOGApproverCheckbox).FirstOrDefault() ?? false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return result;
        }

        public static bool GetCurrentUserLogChkNameUserid(string p)
        {
            MCASEntities _db = new MCASEntities();
            bool result;
            try
            {
                result = (from l in _db.MNT_Users where l.UserId == p select l.LOGApproverCheckbox).FirstOrDefault() ?? false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return result;
        }
    }

    public class PasswordSetupModel : BaseModel
    {
        #region properties


        //public int SetupID = 1;
        //public int SetupID = 9;
        public int SetupID { get; set; }
        [Required(ErrorMessage = "Enforce password History is required.")]
        public int EnforcePasswordHistory { get; set; }

        [Required(ErrorMessage = "Maximum password age is required.")]
        public int MaxPasswordAge { get; set; }

        [Required(ErrorMessage = "Minimum password age is required.")]
        public int MinPasswordAge { get; set; }
        [Required(ErrorMessage = "Minimum password length is required.")]
        public int MinPasswordLength { get; set; }
        [Required(ErrorMessage = "Password must meet complexity is required.")]
        public string PasswordComplexity { get; set; }
        [Required(ErrorMessage = "Account lockout duration is required.")]
        public int AccLockoutDuration { get; set; }
        [Required(ErrorMessage = "Account threshold is required.")]
        public int AccLockoutThreshold { get; set; }
        [Required(ErrorMessage = "Reset account lockout is required.")]
        public int ResetAccCounterAfter { get; set; }
        [Required(ErrorMessage = "Maximum lifeservice ticket is required.")]
        public int MaxLifeTimeServiceTicket { get; set; }
        [Required(ErrorMessage = "Maximum lifetime user ticket is required.")]
        public int MaxLifeTimeUserTicket { get; set; }
        [Required(ErrorMessage = "Maximum lifetime ticket renewal is required.")]
        public int MaxLifeTimeUserTicketRenewal { get; set; }
        [Required(ErrorMessage = "Password complex list is required.")]
        public string PwdComplexList { get; set; }
        [Required(ErrorMessage = "Send Forgotten Password Through Email is required.")]
        public string SendPwdThroughMail { get; set; }
        [Required(ErrorMessage = "Account lockout duration is required.")]
        public string EnforceUserLogon { get; set; }

        public List<LookUpListItems> PwdComplexitylist { get; set; }
        public List<LookUpListItems> EnforceLoginList { get; set; }
        public List<LookUpListItems> SendPwdThroughMailList { get; set; }

        public override string screenId
        {
            get
            {
                return "261";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "261";
            }

        }

        #endregion

        #region Methods
        public PasswordSetupModel PasswordsetupUpdate()
        {
            MCASEntities _db = new MCASEntities();
            MNT_PasswordSetup pwd = new MNT_PasswordSetup();

            pwd = _db.MNT_PasswordSetup.Where(x => x.SetupID == this.SetupID).FirstOrDefault();
            //pwd.SetupID = this.SetupID;
            if (pwd != null)
            {
                pwd.PasswordComplexity = this.PasswordComplexity;
                pwd.SendForgetPwdThroughMail = this.SendPwdThroughMail;
                // pwd.EnforcePasswordHistory =Convert.ToString(this.EnforcePasswordHistory);
                pwd.MaxPasswordAge = this.MaxPasswordAge;
                pwd.MinPasswordAge = this.MinPasswordAge;
                pwd.MinPasswordLength = this.MinPasswordLength;
                pwd.AccLockoutDuration = this.AccLockoutDuration;
                pwd.AccLockoutThreshold = this.AccLockoutThreshold;
                pwd.ResetAccCounterAfter = this.ResetAccCounterAfter;
                pwd.EnforceLogonRestrict = this.EnforceUserLogon;
                pwd.MaxLifeTimeServiceTicket = this.MaxLifeTimeServiceTicket;
                pwd.MaxLifeTimeUserTicket = this.MaxLifeTimeUserTicket;
                pwd.MaxLifeTimeUserTicketRenewal = this.MaxLifeTimeUserTicketRenewal;
                pwd.ModifiedBy = HttpContext.Current.Session["LoggedInUserName"].ToString();
                pwd.ModifiedDate = DateTime.Now;

                _db.SaveChanges();

                this.SetupID = pwd.SetupID;
                this.CreatedBy = pwd.CreatedBy;
                this.ModifiedBy = pwd.ModifiedBy;
                this.CreatedOn = pwd.CreatedDate == null ? DateTime.MinValue : (DateTime)pwd.CreatedDate;
                this.ModifiedOn = pwd.ModifiedDate;
            }
            else
            {
                pwd = new MNT_PasswordSetup();
                // pwd.SetupID = 11;
                pwd.PasswordComplexity = this.PasswordComplexity;
                pwd.SendForgetPwdThroughMail = this.SendPwdThroughMail;
                //    pwd.EnforcePasswordHistory =Convert.ToString(this.EnforcePasswordHistory);
                pwd.MaxPasswordAge = Convert.ToInt16(this.MaxPasswordAge);
                pwd.MinPasswordAge = Convert.ToInt16(this.MinPasswordAge);
                pwd.MinPasswordLength = Convert.ToInt16(this.MinPasswordLength);
                pwd.AccLockoutDuration = Convert.ToInt16(this.AccLockoutDuration);
                pwd.AccLockoutThreshold = Convert.ToInt16(this.AccLockoutThreshold);
                pwd.ResetAccCounterAfter = Convert.ToInt16(this.ResetAccCounterAfter);
                pwd.EnforceLogonRestrict = this.EnforceUserLogon;
                pwd.MaxLifeTimeServiceTicket = Convert.ToInt16(this.MaxLifeTimeServiceTicket);
                pwd.MaxLifeTimeUserTicket = Convert.ToInt16(this.MaxLifeTimeUserTicket);
                pwd.MaxLifeTimeUserTicketRenewal = Convert.ToInt16(this.MaxLifeTimeUserTicketRenewal);
                pwd.CreatedBy = HttpContext.Current.Session["LoggedInUserName"].ToString(); ;
                pwd.CreatedDate = DateTime.Now;
                _db.MNT_PasswordSetup.AddObject(pwd);
                _db.SaveChanges();
                this.SetupID = pwd.SetupID;
                this.CreatedOn = (DateTime)pwd.CreatedDate;
                this.CreatedBy = pwd.CreatedBy;
            }

            _db.Dispose();

            return this;
        }
        #endregion
    }


    public class orgCategory1
    {
        public string orgName { get; set; }
        public string OrgType { get; set; }
        public string Description { get; set; }
        public string orgCode { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

    }

    public class OrganizationAccessModel : BaseModel
    {
        #region Properties
        public override string screenId
        {
            get
            {
                return "304";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "304";
            }

        }
        public string CategoryType { get; set; }
        public List<LookUpListItems> Categorylist { get; set; }
        public string Organization { get; set; }
        public List<orgCategory1> OrgCatList { get; set; }
        public string OrganizationCode { get; set; }
        public string organizationName { get; set; }

        public string hiddentab { get; set; }
        public List<LookUpListItems> OrgCategoryList { get; set; }

        public List<OrgCountryListItems> usercategorylist { get; set; }
        #endregion

        public static List<string> ExtractFromString(string text, string start, string end)
        {
            List<string> Matched = new List<string>();
            int index_start = 0, index_end = 0;
            bool exit = false;
            while (!exit)
            {
                index_start = text.IndexOf(start);
                index_end = text.IndexOf(end);
                if (index_start != -1 && index_end != -1)
                {
                    Matched.Add(text.Substring(index_start + start.Length, index_end - index_start - start.Length));
                    text = text.Substring(index_end + end.Length);
                }
                else
                    exit = true;
            }
            return Matched;
        }

        public static string Joinstring(string[] ResultList1)
        {
            string res = string.Empty;
            for (var i = 0; i < ResultList1.Count(); i++)
            {
                if (ResultList1[i].Split('~')[0].Length == 2)
                {
                    res = res + "-," + ResultList1[i];
                }
                else
                {
                    res = res + "," + ResultList1[i];
                }

            }
            return res;
        }


        public static string FetchOrgCategoryCode(string userid)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<OrganizationAccessModel>();
            List<orgCategory1> sgf = new List<orgCategory1>();
            var model = new UserAdminModel();
            var query = from product in _db.MNT_UserOrgAccess
                        where product.UserId.Equals(userid)
                        select new
                       {
                           oCode = product.OrgCode,
                           OrganizationName = product.OrgName
                       };
            StringBuilder sb = new StringBuilder();
            foreach (var countryInfo in query)
            {
                sb.Append(countryInfo.oCode);
                sb.Append("~");
            }
            _db.Dispose();
            return sb.ToString();
        }

        public static string FetchOrgCategoryName(string userid)
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<OrganizationAccessModel>();
            List<orgCategory1> sgf = new List<orgCategory1>();
            var model = new UserAdminModel();
            var query =
        from product in obj.MNT_UserOrgAccess
        where product.UserId.Contains(userid)
        select new
        {
            oCode = product.OrgCode,
            OrganizationName = product.OrgName
        };
            StringBuilder sb = new StringBuilder();
            foreach (var countryInfo in query)
            {
                sb.Append(countryInfo.OrganizationName);
                sb.Append("~");


            }
            return sb.ToString();
        }


        public static List<orgCategory1> fetchorgcatlistall(string CountryCode)
        {
            MCASEntities _db = new MCASEntities();
            var list = new List<orgCategory1>();
            var c = (from x in _db.MNT_OrgCountry join y in _db.MNT_Country on x.Country equals y.CountryShortCode where x.Country == CountryCode select new { x.OrganizationName, x.InsurerType, x.CountryOrgazinationCode, y.CountryName ,y.CountryCode}).Distinct().ToList();
            if (c.Any())
            {
                foreach (var data in c)
                {
                    list.Add(new orgCategory1() { orgName = data.OrganizationName, orgCode = data.InsurerType, Description = data.CountryOrgazinationCode, CountryName = data.CountryName, CountryCode = data.CountryCode });
                }
            }
            return list;
        }
    }
}
