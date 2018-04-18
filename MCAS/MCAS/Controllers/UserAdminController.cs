using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;
using System.Transactions;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Specialized;
using MCAS.Globalisation;
namespace MCAS.Controllers
{
    public class UserAdminController : BaseController
    {
        MCASEntities obj = new MCASEntities();
        //
        // GET: /UserAdmin/

        public ActionResult Index()
        {
            return View();
        }

        #region "Entity Details Master"
        public ActionResult UserEntityIndex()
        {
            List<BranchListItems> list = new List<BranchListItems>();
            list = BranchListItems.FetchList();
            return View(list);
        }

        [HttpPost]
        public ActionResult UserEntityIndex(string BranchCode, string BranchName)
        {
            BranchCode = Request.Form["inputCode"].Trim();
            BranchName = Request.Form["inputName"].Trim();
            ViewBag.branchcode = BranchCode;
            ViewBag.branchname = BranchName;
            List<BranchListItems> list = new List<BranchListItems>();
            list = GetUserEntityResult(BranchCode, BranchName).ToList();
            return View(list);
        }

        public IQueryable<BranchListItems> GetUserEntityResult(string EntityCode, string EntityName)
        {
            var searchResult = (from entity in obj.MNT_Branch
                                where
                                entity.BranchCode.Contains(EntityCode) &&
                                entity.BranchName.Contains(EntityName)
                                select entity).ToList().Select(item => new BranchListItems
                                {
                                    BranchId = item.BranchId,
                                    BranchCode = item.BranchCode,
                                    BranchName = item.BranchName
                                }).AsQueryable();

            return searchResult;
        }

        public ActionResult UserEntityEditor(int? BranchId)
        {
            var userentity = new BranchListItems();
            if (BranchId.HasValue)
            {
                var branchlist = (from b in obj.MNT_Branch where b.BranchId == BranchId select b).FirstOrDefault();
                BranchListItems objmodel = new BranchListItems();
                objmodel.BranchCode = branchlist.BranchCode;
                objmodel.BranchName = branchlist.BranchName;
                return View(objmodel);
            }
            return View(userentity);
        }


        public JsonResult CheckBranchCode(string BranchCode, string Bcode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Branch where t.BranchCode == BranchCode orderby t.BranchCode descending select t.BranchCode).Take(1)).FirstOrDefault();
            if ((num != null) && (num.ToLower() != Bcode.ToLower()))
            {
                result = true;
            }
            return Json(result);
        }



        [HttpPost]
        public ActionResult UserEntityEditor(BranchListItems model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hits = (from x in obj.MNT_Branch where x.BranchId == model.BranchId select x).FirstOrDefault();
                    if (hits == null)
                    {
                        ModelState.Clear();
                        var branchlist = model.Update();
                        TempData["notice"] = "Record Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        ModelState.Clear();
                        var branchlist = model.Update();
                        TempData["notice"] = "Record Updated Successfully.";
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }

        #endregion


        #region "User Groups Master"
        public ActionResult UserGroupsIndex()
        {
            List<GroupMastersListItems> list = new List<GroupMastersListItems>();
            list = GroupMastersListItems.FetchList();
            return View(list);
        }

        [HttpPost]
        public ActionResult UserGroupsIndex(string GroupCode, string GroupName, string GroupDeptCode, string GroupStatus)
        {
            string status = "";
            GroupCode = Request.Form["inputGroupCode"].Trim();
            GroupName = Request.Form["inputGroupName"].Trim();
            GroupDeptCode = ((Request.Form["ddlDeptCode"] == null) ? "" : Request.Form["ddlDeptCode"]);
            GroupStatus = ((Request.Form["ddlStatusCode"] == null) ? "" : Request.Form["ddlStatusCode"]);
            if (GroupStatus == "")
            {
                status = "";
            }
            else if (GroupStatus == "Active")
            {
                status = "Y";
            }
            else
            {
                status = "N";
            }
            ViewBag.groupcode = GroupCode;
            ViewBag.groupname = GroupName;
            ViewBag.deptcode = GroupDeptCode;
            ViewBag.status = GroupStatus;
            if (GroupCode != "" || GroupName != "" || GroupDeptCode != "" || GroupStatus != "")
            {
                List<GroupMastersListItems> list = new List<GroupMastersListItems>();
                list = GetUserResult(GroupCode, GroupName, GroupDeptCode, status).ToList();
                //  list = GetUserResult(GroupCode, GroupName,GroupDeptCode).ToList();
                return View(list);
            }
            else
            {
                List<GroupMastersListItems> list = new List<GroupMastersListItems>();
                list = GroupMastersListItems.FetchList();
                return View(list);
            }



        }

        public JsonResult FillDeptCodeList()
        {
            var returnData = LoadDepts();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillGeneralStatus()
        {
            var returnData = LoadLookUpValue("STATUS");
            returnData.Insert(0, new LookUpListItems() { Lookup_value = "Active", Lookup_desc = "Active" });
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public IQueryable<GroupMastersListItems> GetUserResult(string GroupCode, string GroupName, string GroupDeptCode, string GroupStatus)
        {
            var searchResult = (from g in obj.MNT_GroupsMaster
                                //join p in obj.MNT_Products on l.ProductCode equals p.ProductCode
                                join d in obj.MNT_Department on g.DeptCode equals d.DeptCode
                                where
                                  g.GroupCode.Contains(GroupCode) &&
                                  g.GroupName.Contains(GroupName) &&
                                  d.DeptCode.Contains(GroupDeptCode) &&
                                  g.IsActive.Contains(GroupStatus)


                                select new GroupMastersListItems
                                {
                                    GroupId = g.GroupId,
                                    GroupCode = g.GroupCode,
                                    GroupName = g.GroupName,
                                    DeptCode = d.DeptName,
                                    AccessLevel = g.AccessLevel,
                                    IsActive = g.IsActive
                                }).AsQueryable();

            return searchResult;
        }


        public ActionResult UserGroupsEditor(int? GroupId)
        {
            string strGroupid = string.Empty;
            var usergroup = new GroupMastersListItems();
            if (GroupId.HasValue)
            {
                var grouplist = (from lt in obj.MNT_GroupsMaster where lt.GroupId == GroupId select lt).FirstOrDefault();

                GroupMastersListItems objmodel = new GroupMastersListItems();
                objmodel.GroupId = grouplist.GroupId;
                strGroupid = Convert.ToString(objmodel.GroupId);
                objmodel.GroupCode = grouplist.GroupCode;
                objmodel.GroupName = grouplist.GroupName;
                objmodel.DeptCode = grouplist.DeptCode;
                objmodel.RoleCode = grouplist.RoleCode;
                var status = grouplist.IsActive;
                if (status == "Y")
                {
                    objmodel.IsActive = "Active";
                }
                else
                {
                    objmodel.IsActive = "InActive";
                }
                objmodel.AccessLevel = grouplist.AccessLevel;
                string dry = Convert.ToString(GroupId);
                var groupdiary = (from gp in obj.MNT_GroupPermission
                                  where gp.GroupId == dry
                                  select gp.MenuId).ToList();

                var grouppermission = obj.Proc_GetMNT_GroupPermission(dry).ToList();
                var item = new List<GroupPermissionListItems>();
                string val = string.Empty;
                if (grouppermission.Any())
                {

                    foreach (var data in grouppermission)
                    {
                        var Read1 = Convert.ToBoolean(data.Read);
                        var Write1 = Convert.ToBoolean(data.Write);
                        var Delete1 = Convert.ToBoolean(data.Delete);
                        var SplPermission1 = Convert.ToBoolean(data.SplPermission);
                        var MenuId1 = Convert.ToInt32(data.MenuId);
                        val = Read1 + "~" + Write1 + "~" + Delete1 + "~" + SplPermission1 + "~" + MenuId1 + "~" + val;
                    }
                }
                int[] ResultList = groupdiary.ToArray();
                string ResultString = string.Join(",", ResultList.ToArray());
                ViewData["SelectedGroupMenu"] = ResultString;
                ViewData["SelectedPermission"] = val.TrimEnd('~');
                ViewData["GroupId"] = strGroupid;
                objmodel.deptlist = LoadDepts();
                objmodel.Statuslist = LoadLookUpValue("STATUS");
                objmodel.Rolelist = LoadLookUpValue("UserRole");
                objmodel.menulist = LoadMenuListByTabId();
                objmodel.readwritelist = loadPermission();

                objmodel.CreatedBy = grouplist.CreatedBy == null ? " " : grouplist.CreatedBy;
                if (grouplist.CreatedDate != null)
                    objmodel.CreatedOn = (DateTime)grouplist.CreatedDate;
                else
                    objmodel.CreatedOn = DateTime.MinValue;
                objmodel.ModifiedBy = grouplist.ModifiedBy == null ? " " : grouplist.ModifiedBy;
                objmodel.ModifiedOn = grouplist.ModifiedDate;

                return View(objmodel);
            }

            usergroup.deptlist = LoadDepts();
            usergroup.Statuslist = LoadLookUpValue("STATUS");
            usergroup.Rolelist = LoadLookUpValue("UserRole");
            usergroup.menulist = LoadMenuListByTabId();
            usergroup.readwritelist = loadPermission();
            ViewData["SelectedGroupMenu"] = "";
            ViewData["SelectedPermission"] = "";
            return View(usergroup);
        }


        public JsonResult CheckGroupCode(string groupname)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_GroupsMaster where t.GroupCode == groupname orderby t.GroupId descending select t.GroupCode).Take(1)).FirstOrDefault();
            if (num != null)
            {
                result = true;
            }
            return Json(result);
        }

        public JsonResult doesUserNameExistGet(string GroupCode)
        {

            var user = (from m in obj.MNT_GroupsMaster where m.GroupCode == GroupCode select m).First();

            return Json(user == null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGroupCode(int GroupVal)
        {
            var returnData = "";
            if (GroupVal != 0)
            {
                returnData = (from g in obj.MNT_GroupsMaster where g.GroupId == GroupVal select g.RoleCode).First();
            }
            else
            {
                returnData = "";
            }
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UserGroupsEditor(GroupMastersListItems model, FormCollection form)
        {

            MCASEntities objEntity = new MCASEntities();
            try
            {
                MNT_GroupPermission grouppemissioninfo = new MNT_GroupPermission();
                model.deptlist = LoadDepts();
                model.Statuslist = LoadLookUpValue("STATUS");
                model.Rolelist = LoadLookUpValue("UserRole");
                model.menulist = LoadMenuListByTabId();
                if (ModelState.IsValid)
                {
                    string ResultString = "";
                    string ResultProdList = "";
                    string ResultProdList1 = "";
                    if (model.GroupId == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        TempData["notice"] = "Record Saved Successfully.";

                    }
                    else
                    {
                        model.ModifiedBy = LoggedInUserName;
                        TempData["notice"] = "Record Updated Successfully.";
                    }



                    ModelState.Clear();
                    var grouplist = model.Update(objEntity);
                    if (form.GetValues("query_Menulist") != null)
                    {
                        if (objEntity.Connection.State == System.Data.ConnectionState.Closed)
                            objEntity.Connection.Open();
                        using (System.Data.Common.DbTransaction transaction = objEntity.Connection.BeginTransaction())
                        {
                            try
                            {
                                var AllmenulistInt = (from x in objEntity.MNT_Menus where x.IsActive == "Y" select x.MenuId).ToList();
                                var Allmenulist = AllmenulistInt.Select(i => i.ToString()).ToList();
                                var groupid = grouplist.GroupId;
                                var SelMenus = form.Get("query_Menulist");
                                var SelMenuList = new List<string>();
                                if (MCASQueryString["pageMode"] != "Edit" && MCASQueryString["GroupId"] != "0")
                                {
                                    var SelMenuchkedList = form.GetValues("query_Menulist").ToList();
                                    var unchkedMenus = Allmenulist.Except(SelMenuchkedList).ToList();
                                    SelMenuchkedList.AddRange(unchkedMenus);
                                    SelMenuList = SelMenuchkedList;
                                }
                                else
                                {
                                   var SelMenuformList = form.GetValues("query_Menulist").ToList();
                                   var SelMenuTabList = (from x in objEntity.MNT_GroupPermission where x.GroupId == System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)groupid).Trim() select System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.MenuId).Trim()).ToList();
                                   if (SelMenuformList.Count == SelMenuTabList.Count)
                                   {
                                       SelMenuList = SelMenuTabList;
                                   }
                                   else if (SelMenuformList.Count > SelMenuTabList.Count)
                                   {
                                       var MenustoBeAdded = SelMenuformList.Except(SelMenuTabList).ToList();
                                       SelMenuTabList.AddRange(MenustoBeAdded);
                                       SelMenuList = SelMenuTabList;
                                   }
                                   else
                                   {
                                       var MenustoBeUpdated = SelMenuTabList.Except(SelMenuformList).ToList();
                                       SelMenuformList.AddRange(MenustoBeUpdated);
                                       SelMenuList = SelMenuformList;
                                   }
                                }
                                var SelMenuProductList = new string[] { };
                                string[] ResultList = new string[] { };
                                grouppemissioninfo.Status = "Y";
                                if (SelMenuList != null)
                                {
                                    //var MenustoBeDeleted = Allmenulist.Except(SelMenuList).ToList();
                                    //for (int i = 0; i < MenustoBeDeleted.Count; i++)
                                    //{
                                    //    try
                                    //    {
                                    //        DeleteMenuId(Convert.ToInt32(MenustoBeDeleted[i]), Convert.ToString(groupid));
                                    //    }
                                    //    catch (Exception ex)
                                    //    {
                                    //        ErrorLog(ex.Message, ex.InnerException);
                                    //    }
                                    //}
                                    ResultString = string.Join(",", SelMenuList.ToArray());
                                    foreach (var menu in SelMenuList)
                                    {
                                        var SelReadPermission = false;
                                        var SelWritePermission = false;
                                        var SelDeletePermission = false;
                                        var SelSplPermission = false;
                                        var SelPermisionList = form.GetValues(menu.ToString());
                                        var names = new int[] { 221, 222, 223, 224 };
                                        if (SelPermisionList != null)
                                        {
                                            foreach (var perm in SelPermisionList)
                                            {

                                                if (perm.ToString().Equals("Read"))
                                                    SelReadPermission = true;
                                                else if (perm.ToString().Equals("Write"))
                                                    SelWritePermission = true;
                                                else if (perm.ToString().Equals("Delete"))
                                                    SelDeletePermission = true;
                                                else if (perm.ToString().Equals("Spl"))
                                                    SelSplPermission = true;

                                            }
                                        }
                                        else
                                        {
                                            SelReadPermission = false;
                                            SelWritePermission = false;
                                            SelDeletePermission = false;
                                            SelSplPermission = false;
                                        }
                                        var pm = MCASQueryString["pageMode"];
                                        var group = MCASQueryString["GroupId"];
                                            if (pm != "Edit" && group != "0")
                                            {
                                                MNT_GroupPermission Newgrouppemissioninfo = new MNT_GroupPermission();
                                                Newgrouppemissioninfo.GroupId = Convert.ToString(groupid);
                                                Newgrouppemissioninfo.MenuId = Convert.ToInt32(menu);
                                                Newgrouppemissioninfo.Read = SelReadPermission;
                                                Newgrouppemissioninfo.Write = SelWritePermission;
                                                Newgrouppemissioninfo.Delete = SelDeletePermission;
                                                Newgrouppemissioninfo.SplPermission = SelSplPermission;
                                                ResultProdList += Newgrouppemissioninfo.Read + "~" + Newgrouppemissioninfo.Write + "~" + Newgrouppemissioninfo.Delete + "~" + Newgrouppemissioninfo.SplPermission + "~" + Newgrouppemissioninfo.MenuId + "~";
                                                ResultProdList1 += Newgrouppemissioninfo.MenuId + ",";
                                                var save = obj.Proc_MNT_GroupPermission_Save(Newgrouppemissioninfo.GroupId, Convert.ToInt32(menu), "Y", null, SelReadPermission, SelWritePermission, SelDeletePermission, SelSplPermission);
                                            }
                                            else
                                            {

                                                MNT_GroupPermission Newgrouppemissioninfo = new MNT_GroupPermission();
                                                Newgrouppemissioninfo.GroupId = Convert.ToString(model.GroupId);
                                                Newgrouppemissioninfo.MenuId = Convert.ToInt32(menu);
                                                Newgrouppemissioninfo.Read = SelReadPermission;
                                                Newgrouppemissioninfo.Write = SelWritePermission;
                                                Newgrouppemissioninfo.Delete = SelDeletePermission;
                                                Newgrouppemissioninfo.SplPermission = SelSplPermission;
                                                ResultProdList += Newgrouppemissioninfo.Read + "~" + Newgrouppemissioninfo.Write + "~" + Newgrouppemissioninfo.Delete + "~" + Newgrouppemissioninfo.SplPermission + "~" + Newgrouppemissioninfo.MenuId + "~";
                                                ResultProdList1 += Newgrouppemissioninfo.MenuId + ",";


                                                if (Newgrouppemissioninfo.Read != false || Newgrouppemissioninfo.Write != false && Newgrouppemissioninfo.Delete != false && Newgrouppemissioninfo.SplPermission != false)
                                                {
                                                    var save = obj.Proc_MNT_GroupPermission_update(Newgrouppemissioninfo.MenuId, Newgrouppemissioninfo.Read, Newgrouppemissioninfo.Write, Newgrouppemissioninfo.Delete, Newgrouppemissioninfo.SplPermission, Newgrouppemissioninfo.GroupId, Newgrouppemissioninfo.MenuId);
                                                }
                                                else
                                                {

                                                    var save = obj.Proc_MNT_GroupPermission_update(Newgrouppemissioninfo.MenuId, Newgrouppemissioninfo.Read, Newgrouppemissioninfo.Write, Newgrouppemissioninfo.Delete, Newgrouppemissioninfo.SplPermission, Newgrouppemissioninfo.GroupId, Newgrouppemissioninfo.MenuId);
                                                }
                                            }
                                        }
                                    }
                                }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw (ex);
                            }
                        }
                    }
                    string ResultProdString = ResultProdList.TrimEnd(',');
                    ViewData["SelectedGroupMenu"] = ResultString;
                    ViewData["SelectedPermission"] = ResultProdString;
                    ViewData["SelectedPermission1"] = ResultProdList1.TrimEnd(',');
                    // TempData["notice"] = "Records Saved Successfully.";

                    return View(model);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes:" + ex.Message);
            }
            finally { objEntity.Dispose(); }
            return View(model);
        }



        #endregion


        #region "User Admin Master"
        private void DeleteMenuId(int MenuId, string GroupId)
        {
            var Deletemenu = obj.Proc_MNT_GroupPermission_Delete(GroupId, MenuId);
        }
        public ActionResult UserAdminMastersList()
        {

            List<UserAdminModel> list = new List<UserAdminModel>();
            try
            {
                list = UserAdminModel.GetResult("", "", "", "", "", "").ToList();
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult UserAdminMastersList(string UserId, string UserDispName, string GroupName, string DeptCode, string MainClass, string UserStatus)
        {
            List<UserAdminModel> list =new List<UserAdminModel>();
            try
            {
                string strStatus = string.Empty;
                UserId = Request.Form["inputUserId"].Trim();
                UserDispName = Request.Form["inputUserDispName"].Trim();
                GroupName = Request.Form["inputGroupName"].Trim();
                DeptCode = ((Request.Form["ddlDeptCode"] == null) ? "" : Request.Form["ddlDeptCode"]);
                MainClass = ((Request.Form["ddlProductCode"] == null) ? "" : Request.Form["ddlProductCode"]);
                UserStatus = ((Request.Form["ddlStatusCode"] == null) ? "" : Request.Form["ddlStatusCode"]);
                strStatus = UserStatus == "" ? "" : UserStatus == "Active" ? "Y" : "N";
                ViewBag.userid = UserId;
                ViewBag.userdisplayname = UserDispName;
                ViewBag.groupid = GroupName;
                ViewBag.deptcode = DeptCode;
                ViewBag.mainclas = MainClass;
                ViewBag.userstatus = UserStatus;
                list = UserAdminModel.GetResult(UserId, UserDispName, GroupName, DeptCode, MainClass, strStatus).ToList();
            }
            catch (Exception ex)
            {
                ViewData["SuccessMsg"] = "Cannot save some error occured.";
                ErrorLog(ex.Message, ex.InnerException);
            }
            return View(list);
        }

        public JsonResult FillBranchCodeList()
        {
            var returnData = LoadBranches();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }



        public JsonResult FillProductDescription()
        {
            var returnData = LoadProductsDescription();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

       

        public JsonResult UserNameExists(string UserId)
        {
            MCASEntities db = new MCASEntities();
            var result = ((from t in db.MNT_Users where t.UserId == UserId orderby t.UserId descending select t.UserId).Take(1)).FirstOrDefault() == null ? false : true;
            db.Dispose();
            return Json(result);
        }

        public ActionResult UserAdminMastersEditor(int? SNo)
        {
            var useradmin = new UserAdminModel();
            UserAdminModel objmodel = new UserAdminModel();
            SecurityPermissions UserPermision = new SecurityPermissions();
            var results = (from lt in obj.MNT_PasswordSetup select lt).FirstOrDefault();

            if (results != null)
            {
                TempData["PasswordComplexity"] = results.PasswordComplexity;
                TempData["MinPasswordLength"] = results.MinPasswordLength;
            }            
            try
            {
                if (SNo != 0 && SNo.HasValue)
                {

                        var userlist = (from lt in obj.MNT_Users where lt.SNo == SNo select lt).FirstOrDefault();

                        objmodel.UserId = userlist.UserId;
                        if (userlist.LoginPassword != null)
                        {
                            if (userlist.LoginPassword != "")
                            {
                                objmodel.LoginPassword = EnDecryption.DecryptMessage(userlist.LoginPassword);
                            }
                        }
                        objmodel.LoginRoleCode = RoleCode(LoggedInUserId);
                        objmodel.CurrentUserLogChkName = UserAdminModel.GetCurrentUserLogChkName(Convert.ToInt32(SNo));
                        objmodel.LogChkName = UserAdminModel.GetLogChkName();
                        objmodel.UserFullName = userlist.UserFullName;
                        objmodel.UserDispName = userlist.UserDispName;
                        objmodel.EmailId = userlist.EmailId;
                        objmodel.GroupId = Convert.ToInt32(userlist.GroupId);
                        objmodel.DeptCode = userlist.DeptCode;
                        objmodel.LOGApproverCheckbox = Convert.ToBoolean(userlist.LOGApproverCheckbox);
                        objmodel.FAL_OD = userlist.FAL_OD;
                        objmodel.FAL_PDBI = userlist.FAL_PDBI;
                        objmodel.Country = userlist.OrgCategory;
                        objmodel.DID_No = userlist.DID_No;
                        objmodel.FAX_No = userlist.FAX_No;
                        //   objmodel.IsActive = userlist.IsActive;
                        objmodel.IsActive = userlist.IsActive == "Y" ? "Active" : "InActive";
                        objmodel.IsEnabled = userlist.IsEnabled;
                        objmodel.UserTypeCode = userlist.UserTypeCode;
                        //objmodel.AccessLevel = userlist.AccessLevel;
                        objmodel.FirstTime = userlist.FirstTime;
                        objmodel.MainClass = userlist.MainClass;
                        objmodel.Initial = userlist.Initial;
                        //   objmodel.orgCategory = userlist.OrgCategory.Split(',');
                        //   string[] str = objmodel.orgCategory;
                        //    ViewData["SelectedCategory"] = string.Join(",", str);

                        var countryproduct = (from cp in obj.MNT_UserCountryProducts
                                              join usr in obj.MNT_Users on cp.UserId equals usr.UserId
                                              where usr.SNo == SNo
                                              select cp.CountryCode).ToList().Distinct();

                        var countryproductlist = (from cp in obj.MNT_UserCountryProducts
                                                  join usr in obj.MNT_Users on cp.UserId equals usr.UserId
                                                  where usr.SNo == SNo
                                                  select new UserCountryProductsListItems
                                                  {
                                                      ProductCode = cp.ProductCode + "~" + cp.CountryCode
                                                  }).ToList();


                        string[] ResultList = countryproduct.ToArray();
                        string ResultString = string.Join(",", ResultList.ToArray());
                        string ResultProdList = "";
                        //var res = countryproductlist.Count() == 0 ? "" : countryproductlist.Select(i => i.ProductCode).Aggregate((i, j) => i + ',' + j);
                        foreach (var m in countryproductlist)
                        {
                            ResultProdList += m.ProductCode + ",";

                        }

                        string ResultProdString = ResultProdList.TrimEnd(',');

                        ViewData["SelectedCountries"] = ResultString;
                        ViewData["SelectedProdCountries"] = ResultProdString;
                        var categ = objmodel.orgCategory;

                        objmodel.usercategorylist = LoadOrgCategory();

                        objmodel.productslist = LoadProducts();
                        objmodel.deptlist = LoadDepts();
                        objmodel.branchlist = LoadBranches();
                        objmodel.groupslist = LoadGroups();
                        objmodel.Statuslist = LoadLookUpValue("STATUS");
                        objmodel.usercountrylist = LoadUserCountrybyUserId();
                        objmodel.userproductcountrylist = LoadUserProductCountry();
                        objmodel.countrylist = LoadCountry();
                        objmodel.FALODlist = LoadFALODlist();
                        objmodel.FALPDBIlist = LoadFALPDBIlist();
                       

                        
                        string userGroupid = objmodel.GroupId.ToString();
                        MCASEntities dbEntity = new MCASEntities();
                        MNT_GroupPermission groupPermi = (from P in dbEntity.MNT_GroupPermission
                                                          where P.GroupId == userGroupid && P.MenuId == 304
                                                          select P).FirstOrDefault();
                        if (groupPermi != null)
                        {
                            UserPermision.Read = groupPermi.Read != null ? (bool)groupPermi.Read : UserPermision.Read;
                            UserPermision.Write = groupPermi.Write != null ? (bool)groupPermi.Write : UserPermision.Write;
                            UserPermision.Delete = groupPermi.Delete != null ? (bool)groupPermi.Delete : UserPermision.Delete;
                            UserPermision.SplPermission = groupPermi.SplPermission != null ? (bool)groupPermi.SplPermission : UserPermision.SplPermission;
                        }

                        ViewData["OrgAccessPer"] = UserPermision;

                        objmodel.CreatedBy = userlist.CreatedBy == null ? " " : userlist.CreatedBy;
                        if (userlist.CreatedDate != null)
                            objmodel.CreatedOn = (DateTime)userlist.CreatedDate;
                        else
                            objmodel.CreatedOn = DateTime.MinValue;
                        objmodel.ModifiedBy = userlist.ModifiedBy == null ? " " : userlist.ModifiedBy;
                        objmodel.ModifiedOn = userlist.ModifiedDate;
                        objmodel.CurrentUserLogChkName = UserAdminModel.GetCurrentUserLogChkName(Convert.ToInt32(SNo));
                        objmodel.LogChkName = UserAdminModel.GetLogChkName();
                        return View(objmodel);
                    
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving user details " + objmodel.UserId + " for user master." + objmodel.GroupId + ".");
                addInfo.Add("entity_type", "user details");
                PublishException(ex, addInfo, 0, "surveyor Master" + objmodel.UserId);
                return View(objmodel);
            }
            useradmin.usercategorylist = LoadOrgCategory();
            useradmin.productslist = LoadProducts();
            useradmin.deptlist = LoadDepts();
            useradmin.branchlist = LoadBranches();
            useradmin.groupslist = LoadGroups();
            useradmin.Statuslist = LoadLookUpValue("STATUS");
            useradmin.usercountrylist = LoadUserCountrybyUserId();
            useradmin.userproductcountrylist = LoadUserProductCountry();
            useradmin.countrylist = LoadCountry();
            useradmin.FALODlist = LoadFALODlist();
            useradmin.FALPDBIlist = LoadFALPDBIlist();
            ViewData["SelectedCountries"] = "";
            ViewData["SelectedProdCountries"] = "";
            string strUserSecurity = System.Configuration.ConfigurationManager.AppSettings["UserSecurity"];
            foreach (string perm in strUserSecurity.Split('^'))
            {
                if (perm.Contains("Read"))
                    UserPermision.Read = perm.Split('=')[1].ToString() == "1" ? true : false;
                else if (perm.Contains("Write"))
                    UserPermision.Write = perm.Split('=')[1].ToString() == "1" ? true : false;
                else if (perm.Contains("Delete"))
                    UserPermision.Delete = perm.Split('=')[1].ToString() == "1" ? true : false;
                else if (perm.Contains("SplPermission"))
                    UserPermision.SplPermission = perm.Split('=')[1].ToString() == "1" ? true : false;
            }
            ViewData["OrgAccessPer"] = UserPermision;

            obj.Dispose();
            return View(useradmin);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult UserAdminMastersEditor(UserAdminModel model, FormCollection form)
        {
            TempData["notice"] = "";
            MCASEntities objEntity = new MCASEntities();
            try
            {
                List<UserCountryProductsListItems> countryprodmodel = new List<UserCountryProductsListItems>();
                UserAdminModel.DeleteAllValuesFromOrgAcessAccordinGToUserid(model.UserId, model.Country);
                model.branchlist = LoadBranches();
                model.deptlist = LoadDepts();
                model.productslist = LoadProducts();
                model.groupslist = LoadGroups();
                model.Statuslist = LoadLookUpValue("STATUS");
                model.usercountrylist = LoadUserCountrybyUserId();
                model.userproductcountrylist = LoadUserProductCountry();
                model.usercategorylist = LoadOrgCategory();
                model.countrylist = LoadCountry();
                model.FALODlist = LoadFALODlist();
                model.FALPDBIlist = LoadFALPDBIlist();
                model.LoginRoleCode = RoleCode(LoggedInUserId);
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    model.CreatedBy = LoggedInUserName;
                    model.ModifiedBy = LoggedInUserName;
                    var userlist = model.Update(objEntity);
                    TempData["notice"] = model.ResultMessage;
                    if (objEntity.Connection.State == System.Data.ConnectionState.Closed)
                        objEntity.Connection.Open();
                    using (System.Data.Common.DbTransaction transaction = objEntity.Connection.BeginTransaction())
                    {
                        try
                        {
                            List<MNT_UserCountryProducts> list = new List<MNT_UserCountryProducts>();
                            var items = objEntity.MNT_UserCountryProducts.Where(item => item.UserId == userlist.UserId);
                            MNT_UserCountryProducts countryproductinfo = new MNT_UserCountryProducts();
                            var SelCountries = form.Get("query_countryTextEditBox");
                            var SelCountryList = form.GetValues("query_countryTextEditBox");
                            var SelCountryProductList = new string[] { };

                            string[] ResultList = new string[] { };

                            string ResultString = "";
                            string ResultProdList = "";
                            string ResultProdList1 = "";

                            countryproductinfo.UserId = model.UserId;

                            if (SelCountryList != null)
                            {
                                ResultList = SelCountryList.ToArray();
                                ResultString = string.Join(",", ResultList.ToArray());
                                foreach (var prd in SelCountryList)
                                {
                                    var SelCountriesProduct = form.Get(prd.ToString());
                                    SelCountryProductList = form.GetValues(prd.ToString());
                                    countryproductinfo.CountryCode = prd;
                                    if (SelCountryProductList != null)
                                    {
                                        foreach (var cprd in SelCountryProductList)
                                        {
                                            MNT_UserCountryProducts Newcountryproductinfo = new MNT_UserCountryProducts();
                                            Newcountryproductinfo = countryproductinfo;
                                            Newcountryproductinfo.ProductCode = cprd;
                                            ResultProdList += Newcountryproductinfo.ProductCode + "~" + Newcountryproductinfo.CountryCode + ",";
                                            objEntity.MNT_UserCountryProducts.AddObject(Newcountryproductinfo);
                                            objEntity.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
                                        }
                                    }
                                }
                            }
                            string ResultProdString = ResultProdList.TrimEnd(',');

                            ViewData["SelectedCountries"] = ResultString;
                            ViewData["SelectedProdCountries"] = ResultProdString;
                            SecurityPermissions UserPermision = new SecurityPermissions();
                            string userGroupid = model.GroupId.ToString();
                            MCASEntities dbEntity = new MCASEntities();
                            MNT_GroupPermission groupPermi = (from P in dbEntity.MNT_GroupPermission
                                                              where P.GroupId == userGroupid && P.MenuId == 304
                                                              select P).FirstOrDefault();
                            if (groupPermi != null)
                            {
                                UserPermision.Read = groupPermi.Read != null ? (bool)groupPermi.Read : UserPermision.Read;
                                UserPermision.Write = groupPermi.Write != null ? (bool)groupPermi.Write : UserPermision.Write;
                                UserPermision.Delete = groupPermi.Delete != null ? (bool)groupPermi.Delete : UserPermision.Delete;
                                UserPermision.SplPermission = groupPermi.SplPermission != null ? (bool)groupPermi.SplPermission : UserPermision.SplPermission;
                            }

                            ViewData["OrgAccessPer"] = UserPermision;
                            objEntity.AcceptAllChanges();
                            transaction.Commit();
                            obj.Dispose();
                            //return View(model);
                            string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"].ToUpper();
                            object routes = new { @SNo = model.SNo, @pageMode = "Edit" };
                            if (isEncryptedParams.ToUpper() == "YES")
                            {
                                string res = RouteEncryptDecrypt.getRouteString(routes);
                                res = RouteEncryptDecrypt.Encrypt(res);
                                routes = new { Q = res };
                            }
                            return RedirectToAction("UserAdminMastersEditor", routes);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw (ex);
                        }
                    }
                }
                else
                {
                    model.CurrentUserLogChkName = UserAdminModel.GetCurrentUserLogChkNameUserid(model.UserId);
                    model.LogChkName = UserAdminModel.GetLogChkName();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving user master " + model.UserId + " for user master." + model.GroupId + ".");
                addInfo.Add("entity_type", "user master");
                PublishException(ex, addInfo, 0, "user master" + model.UserId);
                return View(model);
            }
            finally { objEntity.Dispose(); }
            //  return View(model);
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
                }
            }
            return Json(Emailresults);
        }

        #endregion


        #region "Department Master"
        public ActionResult UserDeptIndex()
        {
            List<DepartmentsListItems> list = new List<DepartmentsListItems>();
            try
            {
                list = DepartmentsListItems.FetchList();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving department master " + list + " for department." + list + ".");
                addInfo.Add("entity_type", "department master");
                PublishException(ex, addInfo, 0, "department master" + list);
                return View(list);
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult UserDeptIndex(string DeptCode, string DeptName)
        {
            DeptCode = Request.Form["inputDeptCode"].Trim();
            DeptName = Request.Form["inputDeptName"].Trim();
            ViewBag.deptcode = DeptCode;
            ViewBag.deptname = DeptName;
            List<DepartmentsListItems> list = new List<DepartmentsListItems>();
            try
            {
                list = GetUserDeptResult(DeptCode, DeptName).ToList();
            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Search result for dept master " + DeptCode + " for Search result Type of dept master." + DeptName + ".");
                addInfo.Add("entity_type", "dept master");
                PublishException(ex, addInfo, 0, "dept master" + DeptCode);
                return View(list);
            }
            return View(list);
        }

        public IQueryable<DepartmentsListItems> GetUserDeptResult(string DeptsCode, string DeptsName)
        {
            var searchResult = (from depts in obj.MNT_Department
                                where
                               depts.DeptCode.Contains(DeptsCode) &&
                               depts.DeptName.Contains(DeptsName)
                                select depts).ToList().Select(item => new DepartmentsListItems
                                {
                                    DeptId = item.DeptId,
                                    DeptCode = item.DeptCode,
                                    DeptName = item.DeptName
                                }).AsQueryable();
            obj.Dispose();
            return searchResult;
        }

        public ActionResult UserDeptEditor(int? DeptId)
        {
            var userdepts = new DepartmentsListItems();
            DepartmentsListItems objmodel = new DepartmentsListItems();
            try
            {
                if (DeptId.HasValue)
                {
                    var deptslist = (from d in obj.MNT_Department where d.DeptId == DeptId select d).FirstOrDefault();

                    objmodel.DeptCode = deptslist.DeptCode;
                    objmodel.DeptName = deptslist.DeptName;
                    objmodel.DeptId = DeptId;
                    objmodel.CreatedBy = deptslist.CreatedBy == null ? LoggedInUserId : deptslist.CreatedBy;
                    if (deptslist.CreatedDate != null)
                        objmodel.CreatedOn = (DateTime)deptslist.CreatedDate;
                    else
                        objmodel.CreatedOn = DateTime.MinValue;
                    objmodel.ModifiedBy = deptslist.ModifiedBy == null ? " " : deptslist.ModifiedBy;
                    objmodel.ModifiedOn = deptslist.ModifiedDate;
                    return View(objmodel);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while reteriving Department id  " + objmodel.DeptName + " for Department  index." + objmodel.DeptCode + ".");
                addInfo.Add("entity_type", "Deaprtment Master");
                PublishException(ex, addInfo, 0, "Deaprtment Master" + objmodel.DeptName);
            }

            obj.Dispose();
            return View(userdepts);
        }



        public JsonResult CheckDeptCode11(string groupname)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Department where t.DeptCode == groupname orderby t.DeptId descending select t.DeptCode).Take(1)).FirstOrDefault();
            if (num != null)
            {
                result = true;
            }
            db.Dispose();
            obj.Dispose();
            return Json(result);
        }

        public JsonResult CheckDeptCode(string deptcode, string dcode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Department where t.DeptCode == deptcode orderby t.DeptId descending select t.DeptCode).Take(1)).FirstOrDefault();
            if ((num != null) && (num.ToLower() != dcode.ToLower()))
            {
                result = true;
            }
            db.Dispose();
            return Json(result);
        }

        public JsonResult CheckDeptName(string deptname, string dcode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_Department where t.DeptName == deptname orderby t.DeptId descending select t.DeptName).Take(1)).FirstOrDefault();
            if ((num != null) && (num.ToLower() != dcode.ToLower()))
            {
                result = true;
            }
            db.Dispose();
            return Json(result);
        }
        [HttpPost]
        public ActionResult UserDeptEditor(DepartmentsListItems model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                }
                if (ModelState.IsValid)
                {
                    var hits = (from x in obj.MNT_Department where x.DeptId == model.DeptId select x).FirstOrDefault();
                    ModelState.Clear();
                    if (hits == null)
                    {
                        model.CreatedBy = LoggedInUserName;
                        var deptslist = model.Update();
                        TempData["notice"] = "Records Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        model.CreatedBy = hits.CreatedBy;
                        model.ModifiedBy = LoggedInUserName;
                        var deptslist = model.Update();
                        TempData["notice"] = "Records Updated Successfully.";
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
                //   obj.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while saving Deaprtmrnt master " + model.DeptCode + " for departmnet." + model.DeptName + ".");
                addInfo.Add("entity_type", "Department master");
                PublishException(ex, addInfo, 0, "Department master" + model.DeptName);
            }
            return View(model);
        }

        #endregion
            

        #region "User Country Details Master"

        public ActionResult UserCountryIndex()
        {
            List<UserCountryListItems> list = new List<UserCountryListItems>();
            list = UserCountryListItems.FetchList();
            return View(list);
        }

        [HttpPost]
        public ActionResult UserCountryIndex(string UsrShortCode, string UsrCountryName, string UsrStatus)
        {
            UsrShortCode = Request.Form["inputUsrShortCode"].Trim();
            UsrCountryName = Request.Form["inputUsrCountryName"].Trim();
            UsrStatus = ((Request.Form["ddlStatusCode"] == null) ? "" : Request.Form["ddlStatusCode"]);
            ViewBag.shortcode = UsrShortCode;
            ViewBag.countryname = UsrCountryName;
            ViewBag.status = UsrStatus;
            var Status = "";
            List<UserCountryListItems> list = new List<UserCountryListItems>();
            if (UsrStatus == "Active")
            {
                Status = "Y";
            }
            else if (UsrStatus == "InActive")
            {
                Status = "N";
            }
            else
            {
                UsrStatus = "";
            }
            list = GetUserCountryResult(UsrShortCode, UsrCountryName, Status).ToList();
            return View(list);
        }

        public JsonResult FillStatus()
        {
            var returnData = LoadLookUpValue("STATUS");
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public IQueryable<UserCountryListItems> GetUserCountryResult(string UsrShortCode, string UsrCountryName, string UsrStatus)
        {
            var searchResult = (from usrcountry in obj.MNT_UserCountry
                                where
                                usrcountry.CountryShortCode.Contains(UsrShortCode) &&
                                usrcountry.CountryName.Contains(UsrCountryName) &&
                                usrcountry.Status.Contains(UsrStatus)
                                select usrcountry).ToList().Select(item => new UserCountryListItems
                                {
                                    CountryCode = item.CountryCode,
                                    CountryShortCode = item.CountryShortCode,
                                    CountryName = item.CountryName,
                                    Status = item.Status
                                }).AsQueryable();

            obj.Dispose();
            return searchResult;
        }

        public ActionResult UserCountryEditor(string CountryCode)
        {
            var usercountry = new UserCountryListItems();
            if (!string.IsNullOrEmpty(CountryCode))
            {
                var countrylist = (from uc in obj.MNT_UserCountry
                                   where uc.CountryCode == CountryCode
                                   select uc).FirstOrDefault();
                UserCountryListItems objmodel = new UserCountryListItems();
                objmodel.CountryCode = countrylist.CountryCode;
                objmodel.CountryName = countrylist.CountryName;
                objmodel.CountryShortCode = countrylist.CountryShortCode.Trim();

                //if (objmodel.CountryName == null)
                //{
                //    objmodel.CountryName = "N/A";
                //}
                //else {
                //    objmodel.CountryName = countrylist.CountryName;
                //}

                //if (objmodel.CountryShortCode == null)
                //{
                //    objmodel.CountryShortCode = "N/A";
                //}
                //else { 
                //    objmodel.CountryShortCode=countrylist.CountryShortCode.Trim();
                //}

                if (countrylist.Status == "Y")
                {
                    objmodel.Status = "Active";
                }
                else
                {
                    objmodel.Status = "InActive";
                }

                objmodel.countrylist = LoadCountry();
                objmodel.countrylist = LoadUserCountry();
                objmodel.Statuslist = LoadLookUpValue("STATUS");
                return View(objmodel);
            }
            usercountry.countrylist = LoadUserCountry();
            usercountry.Statuslist = LoadLookUpValue("STATUS");
            obj.Dispose();
            return View(usercountry);
        }

        public JsonResult UserCountryExist(string CountryCode)
        {
            MCASEntities db = new MCASEntities();
            var result = false;
            var num = ((from t in db.MNT_UserCountry where t.CountryShortCode == CountryCode && t.Status == "Y" orderby t.CountryCode descending select t.CountryCode).Take(1)).FirstOrDefault();
            if (num != null)
            {
                result = true;
            }
            db.Dispose();
            obj.Dispose();
            return Json(result);
        }

        [HttpPost]
        public ActionResult UserCountryEditor(UserCountryListItems model)
        {

            try
            {
                model.countrylist = LoadUserCountry();
                model.Statuslist = LoadLookUpValue("STATUS");
                if (ModelState.IsValid)
                {
                    var hits = (from x in obj.MNT_UserCountry where x.CountryCode == model.CountryCode.ToUpper() select x).FirstOrDefault();
                    if (hits == null)
                    {
                        ModelState.Clear();
                        var usrcountrylist = model.Update();
                        TempData["notice"] = "Records Saved Successfully.";
                        return View(model);
                    }
                    else
                    {
                        ModelState.Clear();
                        var usrcountrylist = model.Update();
                        TempData["notice"] = "Records Updated Successfully.";
                        return View(model);
                    }

                    //return RedirectToAction("UserCountryIndex");
                }
                else
                {
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            obj.Dispose();
            return View(model);
        }

        #endregion

        #region Password Setup
        [HttpGet]
        public ActionResult PasswordSetUpEditor()
        {
            PasswordSetupModel model = new PasswordSetupModel();
            //int SetupId = 9; 
            //int SetupId = 1;
            //if (SetupId != 0)
            //{
            //var pwds = (from pw in obj.MNT_PasswordSetup where pw.SetupID == SetupId select pw).FirstOrDefault();
            var pwds = (from pw in obj.MNT_PasswordSetup select pw).FirstOrDefault();

            if (pwds != null)
            {
                model.SetupID = pwds.SetupID;
                model.PasswordComplexity = pwds.PasswordComplexity;
                model.SendPwdThroughMail = pwds.SendForgetPwdThroughMail;
                //     model.EnforcePasswordHistory =Convert.ToInt16(pwds.EnforcePasswordHistory);
                model.MaxPasswordAge = Convert.ToInt16(pwds.MaxPasswordAge);
                model.MinPasswordAge = Convert.ToInt16(pwds.MinPasswordAge);
                model.MinPasswordLength = Convert.ToInt16(pwds.MinPasswordLength);
                model.AccLockoutDuration = Convert.ToInt16(pwds.AccLockoutDuration);
                model.AccLockoutThreshold = Convert.ToInt16(pwds.AccLockoutThreshold);
                model.ResetAccCounterAfter = Convert.ToInt16(pwds.ResetAccCounterAfter);
                model.EnforceUserLogon = pwds.EnforceLogonRestrict;
                model.MaxLifeTimeServiceTicket = Convert.ToInt16(pwds.MaxLifeTimeServiceTicket);
                model.MaxLifeTimeUserTicket = Convert.ToInt16(pwds.MaxLifeTimeUserTicket);
                model.MaxLifeTimeUserTicketRenewal = Convert.ToInt16(pwds.MaxLifeTimeUserTicketRenewal);
                model.CreatedBy = pwds.CreatedBy == null ? " " : pwds.CreatedBy;
                if (pwds.CreatedDate != null)
                    model.CreatedOn = (DateTime)pwds.CreatedDate;
                else
                    model.CreatedOn = DateTime.MinValue;
                model.ModifiedBy = pwds.ModifiedBy == null ? " " : pwds.ModifiedBy;
                model.ModifiedOn = pwds.ModifiedDate;
            }
            model.PwdComplexitylist = LoadLookUpValue("GENERAL");
            model.EnforceLoginList = LoadLookUpValue("GENERAL");
            model.SendPwdThroughMailList = LoadLookUpValue("GENERAL");
            //}
            //PwdSetUp.PasswordComplexity = pwd.PasswordComplexity;          
            //PwdSetUp.EnforceUserLogon = Convert.ToString(pwd.EnforceLogonRestrict);           

            obj.Dispose();

            return View(model);
        }

        [HttpPost]
        public ActionResult PasswordSetUpEditor(PasswordSetupModel model)
        {
            model.PwdComplexitylist = LoadLookUpValue("GENERAL");
            model.EnforceLoginList = LoadLookUpValue("GENERAL");
            model.SendPwdThroughMailList = LoadLookUpValue("GENERAL");
            try
            {

                //model.SetupID = 11;
                model.CreatedBy = LoggedInUserName;
                var pwdsetup = model.PasswordsetupUpdate();
                TempData["notice"] = "Records Updated Successfully.";

            }
            catch (Exception ex)
            {
                NameValueCollection addInfo = new NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while Saving locked password " + model.SetupID + " for locked password Master." + model.SetupID + ".");
                addInfo.Add("entity_type", "locked password Master");
                PublishException(ex, addInfo, 0, "locked password" + model.SetupID);
                return View(model);
            }
            return RedirectToAction("PasswordSetUpEditor");

        }

        #endregion
        #region Released Locked User
        public ActionResult ReleasedLockedUser()
        {
            return View();
        }
        #endregion
        #region UnderConstruction Page
        public ActionResult UnderConstruction()
        {
            return View();
        }
        #endregion
        #region Organization Access


        public ActionResult OrganizationAccess(string userId, dynamic SNo)
        {
            MCASEntities _db = new MCASEntities();
            var model = new OrganizationAccessModel();
            string sNo = Convert.ToString(SNo);
            string SNO = Convert.ToString(SNo) == "System.Object" ? "0" : sNo.Contains("System") ? SNo[0] : sNo;
            if (SNO != "0")
            {
                

                int sno1 = SNO == null ? 0 : Convert.ToInt32(SNO);
                var user = (from lt in obj.MNT_Users where lt.SNo == sno1 select lt.UserId).FirstOrDefault();
                var CountryCode = (from lt in obj.MNT_Users where lt.SNo == sno1 select lt.OrgCategory).FirstOrDefault();
                string CreatedBy1 = Convert.ToString(LoggedInUserName);
                string str = OrganizationAccessModel.FetchOrgCategoryCode(user);
                string ParentCheckBoxChecked = str == "" ? "" : string.Join("~", str.Split('~').Distinct().ToArray().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                string strName = OrganizationAccessModel.FetchOrgCategoryName(user);
                model.OrgCatList = OrganizationAccessModel.fetchorgcatlistall(CountryCode);
                ViewData["SelectedCategory"] = str;
                ViewData["SelectedCategoryName"] = strName;
                ViewData["ParentCheckBoxChecked"] = ParentCheckBoxChecked;
                if (model.OrgCatList.Count.Equals(0))
                {
                    TempData["notice"] = "Currently no Organization Name is available. ";
                    TempData["orgAccessVisiable"] = "N";
                }
                else
                {
                    var orgAccess = (from x in obj.MNT_UserOrgAccess where x.UserId == user select x).FirstOrDefault();
                    if (orgAccess != null)
                    {
                        model.CreatedBy = orgAccess.CreatedBy;
                        model.CreatedOn = Convert.ToDateTime(orgAccess.CreatedDate);
                        if (orgAccess.ModifiedBy != null && orgAccess.ModifiedDate != null)
                        {
                            model.ModifiedBy = orgAccess.ModifiedBy;
                            model.ModifiedOn = orgAccess.ModifiedDate;
                        }
                    }
                }
            }

            model.usercategorylist = LoadOrgCategory();
            return View(model);
        }

        [HttpPost]
        public ActionResult OrganizationAccess(OrganizationAccessModel model, FormCollection form, dynamic SNo)
        {
            string CreatedBy1 = Convert.ToString(LoggedInUserName);
            MCASEntities _db = new MCASEntities();
            string sNo = Convert.ToString(SNo);
            string SNO = Convert.ToString(SNo) == "System.Object" ? "0" : sNo.Contains("System") ? SNo[0] : sNo;
            int sno1 = SNO == null ? 0 : Convert.ToInt32(SNO);
            var user = (from lt in obj.MNT_Users where lt.SNo == sno1 select lt.UserId).FirstOrDefault();
            var CountryCode = (from lt in obj.MNT_Users where lt.SNo == sno1 select lt.OrgCategory).FirstOrDefault();
            var exist = (from x in obj.MNT_UserOrgAccess where x.UserId == user select x.UserId).FirstOrDefault();
            if (exist == null)
            {
                try
                {
                    model.usercategorylist = LoadOrgCategory();
                    model.OrgCatList = OrganizationAccessModel.fetchorgcatlistall(CountryCode);
                    var CategoryList = form.GetValues("query_categoryBox");
                    var SelCategoryList = new string[] { };
                    string[] ResultList1 = new string[] { };
                    string[] ResultList2 = new string[] { };
                    string ResultString1 = "";
                    string ResultString12 = "";
                    string orgCodes = "";
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb1 = new StringBuilder();
                    if (CategoryList != null)
                    {
                        ResultList1 = CategoryList.ToArray();
                        for (var i = 0; i < ResultList1.Length; i++)
                        {
                            orgCodes = ResultList1[i].Split('~')[0];
                            if (orgCodes.Length == 2)
                            {
                                sb.Append(orgCodes);
                                sb.Append("~");
                                ViewData["SelectedCategory"] = sb;

                            }
                            else
                            {
                                sb1.Append(orgCodes);
                                sb1.Append("~");
                                ViewData["SelectedCategoryName"] = sb1;
                            }
                        }
                        ResultString1 = string.Join(",", ResultList1.ToArray());
                        ResultString12 = OrganizationAccessModel.Joinstring(ResultList1);
                        //string[] arr = ResultString12.Split('-');
                        string[] arr = ResultString12.Split(new[] { "-," }, StringSplitOptions.None);
                        for (var k = 0; k < arr.Length; k++)
                        {
                            List<string> ar = new List<string>();
                            string arrtext = string.IsNullOrEmpty(arr[k]) ? "" : "," + arr[k];
                            ar = OrganizationAccessModel.ExtractFromString(arrtext, ",", "~");
                            //ar = OrganizationAccessModel.ExtractFromString(arr[k], ",", "~");
                            for (var i = 1; i < ar.Count(); i++)
                            {
                                _db.Proc_MNT_UserOrgAccessInsert(user, ar[0], ar[i], DateTime.Now, user, LoggedInUserName);
                            }
                        }

                    }
                    model.CreatedOn = DateTime.Now;
                    model.CreatedBy = LoggedInUserName;
                    TempData["notice"] = "Records Saved Successfully.";

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes.  Try again and if the problem persists, see your system administrator." + ex.Message);
                }
                finally
                {
                    obj.Dispose();

                }

            }
            else
            {
                try
                {
                    var orgAccess = (from x in obj.MNT_UserOrgAccess where x.UserId == user select x).FirstOrDefault();
                    var crBy = orgAccess.CreatedBy;
                    var crDt = orgAccess.CreatedDate;
                    var st = obj.Proc_MNT_UserOrgAccessDelete(user);
                    model.usercategorylist = LoadOrgCategory();
                    model.OrgCatList = OrganizationAccessModel.fetchorgcatlistall(CountryCode);
                    var CategoryList = form.GetValues("query_categoryBox");
                    var SelCategoryList = new string[] { };
                    string[] ResultList1 = new string[] { };
                    string[] ResultList2 = new string[] { };
                    string ResultString1 = "";
                    string ResultString12 = "";
                    string orgCodes = "";
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb1 = new StringBuilder();
                    if (CategoryList != null)
                    {
                        ResultList1 = CategoryList.ToArray();
                        for (var i = 0; i < ResultList1.Length; i++)
                        {
                            orgCodes = ResultList1[i].Split('~')[0];
                            if (orgCodes.Length == 2)
                            {
                                sb.Append(orgCodes);
                                sb.Append("~");
                                ViewData["SelectedCategory"] = sb;
                            }
                            else
                            {
                                sb1.Append(orgCodes);
                                sb1.Append("~");
                                ViewData["SelectedCategoryName"] = sb1;
                            }
                        }
                        ResultString1 = string.Join(",", ResultList1.ToArray());
                        ResultString12 = OrganizationAccessModel.Joinstring(ResultList1);
                        string[] arr = ResultString12.Split(new[] { "-," }, StringSplitOptions.None);
                        for (var k = 0; k < arr.Length; k++)
                        {
                            List<string> ar = new List<string>();
                            string arrtext = string.IsNullOrEmpty(arr[k]) ? "" : "," + arr[k];
                            ar = OrganizationAccessModel.ExtractFromString(arrtext, ",", "~");
                            for (var i = 1; i < ar.Count(); i++)
                            {
                                _db.Proc_MNT_UserOrgAccessInsert(user, ar[0], ar[i], crDt, crBy, LoggedInUserName);
                            }
                        }


                    }
                    model.CreatedBy = crBy;
                    model.CreatedOn = Convert.ToDateTime(crDt);
                    model.ModifiedOn = DateTime.Now;
                    model.ModifiedBy = LoggedInUserName;
                    TempData["notice"] = "Records Updated Successfully.";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes.  Try again and if the problem persists, see your system administrator." + ex.Message);
                }
                finally
                {
                    obj.Dispose();
                }
            }
            string str = OrganizationAccessModel.FetchOrgCategoryCode(user);
            string ParentCheckBoxChecked = str == "" ? "" : string.Join("~", str.Split('~').Distinct().ToArray().Where(x => !string.IsNullOrEmpty(x)).ToArray());
            string strName = OrganizationAccessModel.FetchOrgCategoryName(user);
            model.OrgCatList = OrganizationAccessModel.fetchorgcatlistall(CountryCode);
            ViewData["SelectedCategory"] = str;
            ViewData["SelectedCategoryName"] = strName;
            ViewData["ParentCheckBoxChecked"] = ParentCheckBoxChecked;
            return View(model);
        }


        [HttpPost]
        public JsonResult SelectedOrgCategory(string SelCategory)
        {
            MCASEntities _db = new MCASEntities();
            string orgc = "";
            var model = new UserAdminModel();
            string CreatedBy1 = LoggedInUserId;
            string[] listIds = SelCategory.Split('~');
            orgc = listIds[0];
            string[] strlist = listIds.Skip(1).ToArray();
            List<MNT_UserOrgAccess> diaryList = new List<MNT_UserOrgAccess>();
            try
            {

                if (listIds != null)
                {
                    foreach (var listid in strlist)
                    {
                        MNT_UserOrgAccess access = new MNT_UserOrgAccess();
                        _db.Proc_MNT_UserOrgAccessInsert(CreatedBy1, orgc, listid, DateTime.Now, CreatedBy1, LoggedInUserId);

                    }

                }

                TempData["Display"] = "Display";
            }
            catch (Exception ex)
            {
                throw ex;

            }
            //    OrganizationAccess();
            return Json("Record insert successfully!");

        }



        #endregion

    }
}
