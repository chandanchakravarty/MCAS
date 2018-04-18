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
using MCAS.Web.Objects.ClaimObjectHelper;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Globalization;

namespace MCAS.Web.Objects.MastersHelper
{
    public class LoginAuditLogModel : BaseModel
    {
        #region Properties
        public int SNo { get; set; }
        public DateTime? LogInTime { get; set; }
        public DateTime? LogOutTime { get; set; }
        public string LogInTimestring { get; set; }
        public string LogOutTimestring { get; set; }
        public string UserId { get; set; }
        #endregion
        public static CommonUtilities.DatatableGrid GetLoginAuditLogDetailsList(string Selectcriteria, int draw, int start, int length, int sortColumn, string sortDirection, string p, string search)
        {
            MCASEntities _db = new MCASEntities();
            List<CommonUtilities.DatatableGrid> item = new List<CommonUtilities.DatatableGrid>();
            List<string[]> res = new List<string[]>();
            List<string[]> filterlist = new List<string[]>();
            List<LoginAuditLogModel> list = new List<LoginAuditLogModel>();
            List<LoginAuditLogModel> sortlist = new List<LoginAuditLogModel>();
            try
            {
                if (Selectcriteria != "")
                {
                    list = (from l in _db.LoginAuditLog
                            where l.LoggedInUserId == Selectcriteria
                            select l).ToList().Select(b => new LoginAuditLogModel()
                            {
                                UserId = b.LoggedInUserId,
                                LogInTimestring = b.LogInTime == null ? "" : b.LogInTime.Value.ToString(),
                                LogOutTimestring = b.LogOutTime == null ? "" : b.LogOutTime.Value.ToString(),
                                LogInTime = b.LogInTime
                            }
                     ).ToList();
                }
                else
                {
                    list = (from l in _db.LoginAuditLog
                            select l).ToList().Select(b => new LoginAuditLogModel()
                            {
                                UserId = b.LoggedInUserId,
                                LogInTimestring = b.LogInTime == null ? "" : b.LogInTime.Value.ToString(),
                                LogOutTimestring = b.LogOutTime == null ? "" : b.LogOutTime.Value.ToString(),
                                LogInTime = b.LogInTime
                            }
                     ).ToList();
                }

                sortlist = sortDirection == "desc" ? sortColumn == 0 ? list.OrderBy(x => x.UserId).ToList() : sortColumn == 1 ? list.OrderBy(x => x.LogInTime).ToList() : list.OrderBy(x => x.LogOutTime).ToList() : sortColumn == 0 ? list.OrderByDescending(x => x.UserId).ToList() : sortColumn == 1 ? list.OrderByDescending(x => x.LogInTime).ToList() : list.OrderByDescending(x => x.LogOutTime).ToList();

                if (search != "")
                    sortlist = sortlist.Where(bl => bl.LogInTimestring.ToUpper().Contains(search.ToUpper()) || bl.LogOutTimestring.ToUpper().ToString().Contains(search.ToUpper()) || bl.UserId.ToUpper().ToString().Contains(search.ToUpper())).ToList();

                res = sortlist.Select(cap => new string[]{
                                             cap.UserId,
                                             cap.LogInTimestring,
                                             cap.LogOutTimestring
                                             }
                                         ).ToList();

                if (res.Any())
                {
                    filterlist = length !=-1? res.GetRange(start, Math.Min(length, res.Count - start)):res;
                }
                item.Add(new CommonUtilities.DatatableGrid() { data = filterlist });
                item.FirstOrDefault().draw = draw;
                item.FirstOrDefault().recordsFiltered = res.Count();
                item.FirstOrDefault().recordsTotal = list.Count();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            _db.Dispose();
            return item.FirstOrDefault();
        }
        public static List<User> GetAllUserList()
        {
            MCASEntities _db = new MCASEntities();
            List<User> userList;
            try
            {

                userList = (from l in _db.MNT_Users
                            select new User()
                            {
                                SNo = l.SNo,
                                UserId = l.UserId
                            }
                         ).OrderByDescending(d => d.UserId).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            _db.Dispose();
            return userList;
        }
        public static IQueryable<LoginAuditLogModel> GetLoginAuditLogDetailsListAll(string Selectcriteria, int draw, int start, int length, int sortColumn, string sortDirection, string p, string search)
        {
            MCASEntities _db = new MCASEntities();
            List<LoginAuditLogModel> list = new List<LoginAuditLogModel>();
            try
            {
                if (Selectcriteria != "")
                {
                    list = (from l in _db.LoginAuditLog
                            where l.LoggedInUserId == Selectcriteria
                            select l).ToList().Select(b => new LoginAuditLogModel()
                            {
                                UserId = b.LoggedInUserId,
                                LogInTimestring = b.LogInTime == null ? "" : b.LogInTime.Value.ToString(),
                                LogOutTimestring = b.LogOutTime == null ? "" : b.LogOutTime.Value.ToString(),
                                LogInTime = b.LogInTime
                            }
                     ).OrderByDescending(d => d.LogInTime).ToList();
                }
                else
                {
                    list = (from l in _db.LoginAuditLog
                            select l).ToList().Select(b => new LoginAuditLogModel()
                            {
                                UserId = b.LoggedInUserId,
                                LogInTimestring = b.LogInTime == null ? "" : b.LogInTime.Value.ToString(),
                                LogOutTimestring = b.LogOutTime == null ? "" : b.LogOutTime.Value.ToString(),
                                LogInTime = b.LogInTime
                            }
                     ).OrderByDescending(d => d.LogInTime).ToList();
                }
                if (search != "")
                {
                    list = list.Where(bl => bl.LogInTimestring.ToUpper().Contains(search.ToUpper()) || bl.LogOutTimestring.ToUpper().ToString().Contains(search.ToUpper()) || bl.UserId.ToUpper().ToString().Contains(search.ToUpper())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            _db.Dispose();
            return list.ToList().AsQueryable();
        }
        public class User
        {
            public int SNo { get; set; }
            public String UserId { get; set; }
        }
    }
}
