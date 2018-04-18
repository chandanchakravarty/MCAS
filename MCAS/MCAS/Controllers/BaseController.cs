using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Data;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using MCAS.Entity;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.ClaimObjectHelper;
using MCAS.Globalisation;
using System.Collections.Specialized;
using System.Data.Objects.SqlClient;
using System.Configuration;

namespace MCAS.Controllers
{
    [HandleError]
    [EncryptedActionParameter]
    //[SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class BaseController : Controller
    {
        /// <summary>
        /// Manage the internationalization before to invokes the action in the current controller context.
        /// </summary>
        protected override void ExecuteCore()
        {
            string culture = string.Empty;
            //if (this.Session == null || this.Session["CurrentUICulture"] == null)
            //{
            // Get Browser languages.
            var userLanguages = Request.UserLanguages;
            CultureInfo ci;
            if (userLanguages.Count() > 0)
            {
                try
                {
                    ci = new CultureInfo(userLanguages[0]);
                }
                catch (CultureNotFoundException)
                {
                    ci = CultureInfo.InvariantCulture;
                }
            }
            else
            {
                ci = CultureInfo.InvariantCulture;
            }

            culture = ci.ToString();
            this.Session["CurrentUICulture"] = culture;
            //}
            //else
            //{
            //    culture = (string)this.Session["CurrentUICulture"];
            //}

            CultureManager.SetCulture(culture);
            //
            // Invokes the action in the current controller context.
            //

            base.ExecuteCore();
        }
        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        public struct enumCulture
        {
            public const String US = "en-US";
            public const String BR = "pt-BR";
        }

        protected List<LookUpListItems> LoadLookUpValue(string category, bool addAll = true, bool addNone = false)
        {
            return LookUpListItems.Fetch(category, addAll, addNone);
        }
        protected List<LookUpListItems> LoadOrganizationCategory(string category, bool addAll = true, bool addNone = false)
        {
            return LookUpListItems.FetchOrganizationCategory(category, addAll, addNone);
        }
        protected List<ProductsListItems> LoadProducts(bool addAll = true)
        {
            return ProductsListItems.Fetch(addAll);
        }

        protected List<ProductsListItems> LoadProductsDescription(bool addAll = true)
        {
            return ProductsListItems.FetchCodeDescription(addAll);
        }

        protected List<ProductsListItems> LoadSubCodeDescription(bool addAll = true)
        {
            return ProductsListItems.FetchSubCodeDescription(addAll);
        }

        protected List<DiaryListType> LoadDiaryListType(bool addAll = true)
        {
            return DiaryListType.Fetch(addAll);
        }

        protected List<UserList> LoadUserList(bool addAll = true)
        {
            return UserList.Fetch(addAll);
        }

        protected List<ProviceList> LoadProvince(bool addAll = true)
        {
            return ProviceList.Fetch(addAll);
        }

        protected List<RiskTypeListItem> LoadRiskType(bool addAll = true)
        {
            return RiskTypeListItem.Fetch(addAll);
        }

        protected List<GSTListItem> LoadGST(bool addAll = true)
        {
            return GSTListItem.Fetch(addAll);
        }

        protected List<SubClassListItem> LoadSubClass(bool addAll = true)
        {
            return SubClassListItem.Fetch(addAll);
        }

        protected List<VehicleListItem> LoadVehicleClass(bool addAll = true)
        {
            return VehicleListItem.FetchVehicleClass(addAll);
        }

        protected List<VehicleListItem> LoadVehicleMake(bool addAll = true)
        {
            return VehicleListItem.Fetch(addAll);
        }

        protected List<VehicleListItem> LoadVehicleModel(bool addAll = true)
        {
            return VehicleListItem.FetchVehicleModel(addAll);
        }
        protected List<MCAS.Web.Objects.MastersHelper.DepartmentsListItems> LoadDepts(bool addAll = true)
        {
            return MCAS.Web.Objects.MastersHelper.DepartmentsListItems.Fetch(addAll);
        }

        protected List<UserGroupListItems> FillUserGroupList(bool addAll = true)
        {
            return UserGroupListItems.Fetch(addAll);
        }

        protected List<MCAS.Web.Objects.MastersHelper.BranchListItems> LoadBranches(bool addAll = true)
        {
            return MCAS.Web.Objects.MastersHelper.BranchListItems.Fetch(addAll);
        }

        protected List<MCAS.Web.Objects.MastersHelper.GroupMastersListItems> LoadGroups(bool addAll = true)
        {
            return MCAS.Web.Objects.MastersHelper.GroupMastersListItems.Fetch(addAll);
        }

        protected List<UserCountryListItems> LoadUserCountry(bool addAll = true)
        {
            return UserCountryListItems.Fetch(addAll);
        }
        public System.Resources.ResourceManager GetResourceManager(string resFilewithNameSpace)
        {
            return (new TransactionModel()).GetResourceManager(resFilewithNameSpace);
        }

        protected System.Resources.ResourceManager GetResourceManager(int ScreenId)
        {
            return (new TransactionModel()).GetResourceManager(ScreenId);
        }
        protected string GetResourceName(int ScreenId)
        {
            MCASEntities obj = new MCASEntities();
            var Results = (from mntMenu in obj.MNT_Menus
                           where mntMenu.MenuId == ScreenId
                           select mntMenu.VirtualSource).FirstOrDefault();
            obj.Dispose();
            return Results == null ? "" : Results;
        }
        protected object GetTransactionAuditHistoryDetails(int TranAuditId)
        {
            //MCASEntities obj = new MCASEntities();
            //var Results = (from tranAudit in obj.MNT_TransactionAuditLog
            //               join mntMenu in obj.MNT_Menus
            //               on tranAudit.TableName equals mntMenu.ProductName
            //               where tranAudit.TranAuditId == TranAuditId
            //               select mntMenu).FirstOrDefault();

            //object tranHistory=null;
            //if (Results != null)
            //{
            //   var resManager = GetResourceManager(Results.VirtualSource);
            //    tranHistory =  TransactionModel.GetTransactionHistory(TranAuditId.ToString(), resManager);
            //}
            //obj.Dispose();
            //return tranHistory;
            return (new TransactionModel()).GetTransactionHistory(TranAuditId);
        }

        protected List<FALModel> LoadFALODlist(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<FALModel> list = new List<FALModel>();
            list = (from f in obj.MNT_FAL where f.FALAccessCategory == "OD" select new FALModel { FALId = f.FALId, FALLevelName = f.FALLevelName, Amount = f.Amount, FALDisplayName = f.UnlimitedAmt != "Y" ? f.FALLevelName + " - " + (SqlFunctions.StringConvert((double)f.Amount) == null ? "0" : SqlFunctions.StringConvert((double)f.Amount)) : f.FALLevelName + " - " + "Unlimited" }).ToList();
            if (addAll)
            {
                list.Insert(0, new FALModel() { FALId = 0, FALDisplayName = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<FALModel> LoadFALPDBIlist(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<FALModel> list = new List<FALModel>();
            list = (from f in obj.MNT_FAL where f.FALAccessCategory == "PD/BI" select new FALModel { FALId = f.FALId, FALLevelName = f.FALLevelName, Amount = f.Amount, FALDisplayName = f.UnlimitedAmt != "Y" ? f.FALLevelName + " - " + (SqlFunctions.StringConvert((double)f.Amount) == null ? "0" : SqlFunctions.StringConvert((double)f.Amount)) : f.FALLevelName + " - " + "Unlimited" }).ToList();
            if (addAll)
            {
                list.Insert(0, new FALModel() { FALId = 0, FALDisplayName = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        public static List<FALModel> LoadFALLevelName(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<FALModel> list = new List<FALModel>();
            list = (from l in obj.MNT_FAL where l.FALLevelName != null orderby l.FALLevelName select new FALModel { FALId = l.FALId, FALLevelName = l.FALLevelName }).ToList();
            if (addAll)
            {
                list.Insert(0, new FALModel() { FALId = 0, FALLevelName = "[Select...]" });
            }
            return list;
        }

        public static List<FALModel> LoadFALAccessCat(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<FALModel> list = new List<FALModel>();
            list = (from l in obj.MNT_FAL orderby l.FALLevelName select new FALModel { FALId = l.FALId, FALAccessCategory = l.FALAccessCategory }).ToList();
            if (addAll)
            {
                list.Insert(0, new FALModel() { FALId = 0, FALAccessCategory = "[Select...]" });
            }
            return list;
        }

        protected List<UserCountryListItems> LoadUserCountrybyUserId(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<UserCountryListItems> list = new List<UserCountryListItems>();
            list = (from uc in obj.MNT_UserCountry
                    join cp in obj.MNT_ProductsCountry on uc.CountryCode equals cp.CountryCode
                    select new UserCountryListItems { CountryCode = uc.CountryCode, CountryName = uc.CountryName }).Distinct().ToList();
            obj.Dispose();
            return list;
        }

        protected List<ProductsCountryListItems> LoadUserProductCountry(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<ProductsCountryListItems> list = new List<ProductsCountryListItems>();
            list = (from uc in obj.MNT_UserCountry
                    join cp in obj.MNT_ProductsCountry on uc.CountryCode equals cp.CountryCode
                    where uc.Status == "Y"
                    select new ProductsCountryListItems { CountryCode = uc.CountryCode, Product_Code = cp.Product_Code }).Distinct().ToList();
            obj.Dispose();
            return list;
        }

        protected List<OrgCountryListItems> LoadOrgCategory(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<OrgCountryListItems> list = new List<OrgCountryListItems>();
            list = (from uc in obj.MNT_Lookups where uc.Category == "ORGCategory" select new OrgCountryListItems { LookupValue = uc.Lookupvalue, LookupDesc = uc.Lookupdesc, category = "ORGCategory" }).ToList();
            if (addAll)
            {
                list.Insert(0, new OrgCountryListItems() { LookupValue = "", LookupDesc = "[Select...]" });
            }
            obj.Dispose();
            return list;

        }



        protected List<UserCityListItem> LoadCity(bool addAll = true)
        {
            return UserCityListItem.Fetch(addAll);
        }

        protected List<CedantListItem> LoadCedant()
        {
            return CedantListItem.Fetch();
        }

        protected List<UserCountryListItems> LoadCountry(bool addAll = true)
        {
            return UserCountryListItems.Fetch(addAll);
            //MCASEntities obj = new MCASEntities();
            //List<CountryModel> list = new List<CountryModel>();
            //list = (from l in obj.MNT_Country orderby l.CountryName select new CountryModel { CountryShortCode = l.CountryShortCode, CountryName = l.CountryName,CountryCode=l.CountryCode }).ToList();
            //if (addAll)
            //{
            //    list.Insert(0, new CountryModel() { CountryShortCode = "", CountryName = "[Select...]" });
            //}
            //return list;
        }
        protected List<Uploadstatuslist> LoadFillJobType(bool addAll = true)
        {
            List<Uploadstatuslist> list = new List<Uploadstatuslist>();
            list.Insert(0, new Uploadstatuslist() { Id = "Select", Text = "[Select...]" });
            list.Insert(1, new Uploadstatuslist() { Id = "CLM", Text = "Claim Upload" });
            list.Insert(2, new Uploadstatuslist() { Id = "TAC", Text = "TAC Upload" });
            return list;
        }

        protected List<Uploadstatuslist> LoadFillJobStatus(bool addAll = true)
        {
            List<Uploadstatuslist> item = new List<Uploadstatuslist>();
            item.Add(new Uploadstatuslist() { Id = "Select", Text = "[Select...]" });
            item.Add(new Uploadstatuslist() { Id = "Incomplete", Text = "Incomplete" });
            item.Add(new Uploadstatuslist() { Id = "Complete", Text = "Complete" });
            item.Add(new Uploadstatuslist() { Id = "Inprocess", Text = "Inprocess " });
            return item;
        }

        protected string LoadExpenses(string CurrencyCode)
        {
            MCASEntities obj = new MCASEntities();
            var data = obj.MNT_CurrencyTxn.OrderByDescending(p => p.EffDate).Where(p => p.CurrencyCode == CurrencyCode)
                       .Select(g => new
                       {
                           ExchangeRate = g.ExchangeRate,
                       }).Take(1).FirstOrDefault();
            obj.Dispose();
            if (data != null)
            {
                return data.ExchangeRate.ToString();
            }
            else
            {
                return "";
            }


        }

        protected List<LossTypeModel> LoadLossType(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<LossTypeModel> list = new List<LossTypeModel>();
            list = (from l in obj.MNT_LossType orderby l.LossTypeName select new LossTypeModel { LossTypeCode = l.LossTypeCode, LossTypeName = l.LossTypeName }).ToList();
            if (addAll)
            {
                list.Insert(0, new LossTypeModel() { LossTypeCode = "", LossTypeName = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }


        protected List<DiarySetupModel> LoadModule(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<DiarySetupModel> list = new List<DiarySetupModel>();
            list = (from l in obj.MNT_MODULE_MASTER select new DiarySetupModel { ModuleId = l.MM_MODULE_ID, ModuleName = l.MM_MODULE_NAME }).ToList();
            if (addAll)
            {
                list.Insert(0, new DiarySetupModel() { ModuleId = 0, ModuleName = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }


        protected List<MenuListItem> LoadMenuList()
        {
            List<MenuListItem> menuList = MenuListItem.Fetch(1, UserGroupId.ToString()).ToList();
            //ViewData["menuList"] = menuList;
            SetMenuList(menuList);
            return menuList;
        }

        protected List<MenuListItem> LoadMenuListByTabId()
        {
            MCASEntities obj = new MCASEntities();
            var list1 = obj.Proc_GetMenuListByTabId().ToList();
            List<MenuListItem> menuList = new List<MenuListItem>();
            if (list1.Any())
            {

                foreach (var l in list1)
                {
                    var id1 = Convert.ToInt32(l.MainHeaderId);
                    var str = Regex.Replace(l.AdminDisplayText, @"\s+", " ").TrimEnd().TrimStart();
                    menuList.Add(new MenuListItem { MenuId = l.MenuId, AdminDisplayText = str, TabId = l.TabId, IsHeader = l.IsHeader, SubMenu = l.SubMenu, MainHeaderId = id1, SubTabId = l.SubTabId, ProductName = l.ProductName, DisplayText = l.DisplayTitle });
                }
            }
            obj.Dispose();
            return menuList;
        }

        protected List<GroupPermissionListItems> loadPermission()
        {
            MCASEntities obj = new MCASEntities();
            List<GroupPermissionListItems> PermissionList = new List<GroupPermissionListItems>();
            PermissionList = (from l in obj.MNT_GroupPermission select new GroupPermissionListItems { MenuId = l.MenuId, Read = l.Read, Write = l.Write, Delete = l.Delete, SplPermission = l.SplPermission }).ToList();
            obj.Dispose();
            return PermissionList;
        }

        protected List<CatastropheListIems> LoadCatastrophe(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<CatastropheListIems> list = new List<CatastropheListIems>();
            list = (from l in obj.MNT_CastropheMaster select new CatastropheListIems { CastropheCode = l.CastropheCode, CastropheDesc = l.CastropheDesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new CatastropheListIems() { CastropheCode = "[Select...]", CastropheDesc = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<CurrencyMasterModel> LoadCurrency(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<CurrencyMasterModel> list = new List<CurrencyMasterModel>();
            list = (from x in obj.MNT_CurrencyM select new CurrencyMasterModel { CurrencyCode = x.CurrencyCode, CurrencyDispCode = x.CurrencyDispCode, Description = x.Description }).ToList();
            if (addAll)
            {
                list.Insert(0, new CurrencyMasterModel() { CurrencyCode = "[Select...]", Description = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<OrgCountryModel> LoadOrganization(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<OrgCountryModel> list = new List<OrgCountryModel>();
            list = (from x in obj.MNT_OrgCountry select new OrgCountryModel { CountryOrgazinationCode = x.CountryOrgazinationCode, OrganizationName = x.OrganizationName }).ToList();
            if (addAll)
            {
                list.Insert(0, new OrgCountryModel() { CountryOrgazinationCode = "[Select...]", OrganizationName = "[Select...]" });
            }
            obj.Dispose();
            return list;

        }

        protected List<ProductBusinessModel> LoadSubClassCode(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<ProductBusinessModel> list = new List<ProductBusinessModel>();
            list = (from l in obj.MNT_ProductClass orderby l.ClassDesc select new ProductBusinessModel { CobCode = l.ClassCode, CobDesc = l.ClassDesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new ProductBusinessModel() { CobCode = "", CobDesc = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<InsuranceModel> loadSubClass(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = (from l in obj.MNT_ProductClass where l.Status == "1" select new InsuranceModel { SubClassId = l.ID, ClassCode = l.ClassCode, ClassDescription = l.ClassCode + " - " + l.ClassDesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new InsuranceModel() { SubClassId = null, ClassDescription = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<InsuranceModel> loadClass(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = (from l in obj.MNT_Products where l.ProductStatus == 1 select new InsuranceModel { ProductId = l.ProductId, ProductCode = l.ProductCode, ProductDisplayName = l.ProductCode + " - " + l.ProductDisplayName }).ToList();
            if (addAll)
            {
                list.Insert(0, new InsuranceModel() { ProductId = null, ProductDisplayName = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        public void ErrorLog(string message, Exception exception)
        {
            string iDomain = ConfigurationManager.AppSettings["IDomain"].ToString();
            string iUserName = ConfigurationManager.AppSettings["IUserName"].ToString();
            string iPassword = ConfigurationManager.AppSettings["IPassWd"].ToString();

            EAWXmlToPDFParser.ImpersonateUser objImPersonate = new EAWXmlToPDFParser.ImpersonateUser();
            if (objImPersonate.ImpersonateUserLogin(iUserName, iPassword, iDomain))
            {
                string newpath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "\\" + "\\Uploads\\file.txt");
                if (!System.IO.File.Exists(newpath))
                {
                    System.IO.File.Create(newpath).Dispose();
                }
                StreamWriter file2 = new StreamWriter(newpath, true);
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine);
                sb.Append(DateTime.Now + "-" + message + "-" + exception);
                file2.WriteLine(sb.ToString());
                file2.Close();
            }
        }

        public void ModelError(IEnumerable<System.Web.Mvc.ModelError> errors, string ViewName = "")
        {

            try
            {
                string newpath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "\\" + "\\Uploads\\file.txt");
                if (!System.IO.File.Exists(newpath))
                {
                    System.IO.File.Create(newpath).Dispose();
                }
                StreamWriter file2 = new StreamWriter(newpath, true);
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now + "-Model Error starts -" + ViewName);
                int LineNo = 1;
                foreach (var error in errors)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append(LineNo + "-" + "Exception.InnerException" + "-" + error.Exception.InnerException);
                    sb.Append(LineNo + "-" + "Exception.Message" + "-" + error.Exception.Message);
                    sb.Append(LineNo + "-" + "error.ErrorMessage" + "-" + error.ErrorMessage);
                    LineNo++;
                }
                sb.Append(DateTime.Now + "-Model Error Ends -" + ViewName);
                file2.WriteLine(sb.ToString());
                file2.Close();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }

        }
        protected List<InsuranceModel> loadCedant(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<InsuranceModel> list = new List<InsuranceModel>();
            list = (from l in obj.MNT_Cedant orderby l.CedantName select new InsuranceModel { CedantId = l.CedantId, CedantName = l.CedantName }).ToList();
            if (addAll)
            {
                list.Insert(0, new InsuranceModel() { CedantId = null, CedantName = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<GSTModel> LoadGst(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<GSTModel> list = new List<GSTModel>();
            list = (from l in obj.MNT_GST orderby l.GSTDesc select new GSTModel { GSTType = l.GSTType, GSTDescrpition = l.GSTDesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new GSTModel() { GSTType = "", GSTDescrpition = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<ClaimExpenseModel> LoadClaimExpense(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<ClaimExpenseModel> list = new List<ClaimExpenseModel>();
            list = (from l in obj.MNT_ClaimExpense orderby l.ClaimExpenseDesc select new ClaimExpenseModel { ClaimExpenseCode = l.ClaimExpenseCode, ClaimExpenseDesc = l.ClaimExpenseDesc }).ToList();
            if (addAll)
            {
                list.Insert(0, new ClaimExpenseModel() { ClaimExpenseCode = "", ClaimExpenseDesc = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<UserCountryListItems> LoadLoginUserCountry(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<UserCountryListItems> list = new List<UserCountryListItems>();
            list = (from uc in obj.MNT_UserCountry
                    join cp in obj.MNT_ProductsCountry on uc.CountryCode equals cp.CountryCode
                    select new UserCountryListItems { CountryCode = uc.CountryCode, CountryName = uc.CountryName }).Distinct().ToList();
            if (addAll)
            {
                list.Insert(0, new UserCountryListItems() { CountryCode = "", CountryName = "Select Country" });
            }
            obj.Dispose();
            return list;
        }

        protected List<BranchLoginListItems> LoadLoginUserBranch(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<BranchLoginListItems> list = new List<BranchLoginListItems>();
            list = (from l in obj.MNT_BranchLogin select new BranchLoginListItems { BranchCode = l.BranchCode, BranchName = l.BranchName }).ToList();
            if (addAll)
            {
                list.Insert(0, new BranchLoginListItems() { BranchCode = "", BranchName = "Select Branch" });
            }
            obj.Dispose();
            return list;
        }

        protected List<LossNatureListItems> LoadLossNature(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<LossNatureListItems> list = new List<LossNatureListItems>();
            list = (from l in obj.MNT_LossNature select new LossNatureListItems { LossNatureCode = l.LossNatureCode, LossNatureDescription = l.LossNatureDescription }).ToList();
            if (addAll)
            {
                list.Insert(0, new LossNatureListItems() { LossNatureCode = "", LossNatureDescription = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<VehicleBusCaptainModel> LoadBusCaptainDetails(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<VehicleBusCaptainModel> list = new List<VehicleBusCaptainModel>();
            list = (from l in obj.MNT_BusCaptain select new VehicleBusCaptainModel { TranId = l.TranId, BusCaptainCode = l.BusCaptainCode }).ToList();
            if (addAll)
            {
                list.Insert(0, new VehicleBusCaptainModel() { TranId = 0, BusCaptainCode = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<ReserveCurr> LoadReserve(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            var list = obj.MNT_CurrencyM.Select(x => new ReserveCurr
            {
                CurrencyCode = x.CurrencyCode,
                Description = x.Description
            }).ToList();

            list = (from l in obj.MNT_CurrencyM
                    select new
                        ReserveCurr { CurrencyCode = l.CurrencyCode, Description = l.CurrencyDispCode }).ToList();
            if (addAll)
            {
                list.Insert(0, new ReserveCurr() { CurrencyCode = "", Description = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }


        protected List<ExpensesCurr> LoadExpenses(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            var list = obj.MNT_CurrencyM.Select(x => new ExpensesCurr
            {
                CurrencyCode_Expenses = x.CurrencyCode,
                Description_Expenses = x.Description
            }).ToList();

            list = (from l in obj.MNT_CurrencyM
                    select new
                        ExpensesCurr { CurrencyCode_Expenses = l.CurrencyCode, Description_Expenses = l.CurrencyDispCode }).ToList();
            if (addAll)
            {
                list.Insert(0, new ExpensesCurr() { CurrencyCode_Expenses = "", Description_Expenses = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<ClaimAmtCurr> LoadClaimAmt(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            var list = obj.MNT_CurrencyM.Select(x => new ClaimAmtCurr
            {
                CurrencyCode_ClaimAmt = x.CurrencyCode,
                Description_ClaimAmt = x.Description
            }).ToList();

            list = (from l in obj.MNT_CurrencyM
                    select new
                        ClaimAmtCurr { CurrencyCode_ClaimAmt = l.CurrencyCode, Description_ClaimAmt = l.CurrencyDispCode }).ToList();
            if (addAll)
            {
                list.Insert(0, new ClaimAmtCurr() { CurrencyCode_ClaimAmt = "", Description_ClaimAmt = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<AdjusterAppointed> LoadPayableTo(bool addAll = true)
        {
            MCASEntities obj = new MCASEntities();
            List<string> lst = new List<string>();
            lst.Add("ADJ");
            lst.Add("SVY");
            lst.Add("SOL");
            lst.Add("WRK");
            string[] ResultList = lst.ToArray();
            string ResultString = string.Join(",", ResultList.ToArray());
            var list = obj.MNT_Adjusters.Select(x => new AdjusterAppointed
            {

                AdjusterAppointed_Id = x.AdjusterId,
                Description_AdjusterAppointed = x.AdjusterName
            }).ToList();

            list = (from l in obj.MNT_Adjusters
                    orderby l.AdjusterName
                    where ResultString.Contains(l.AdjusterTypeCode)
                    select new
                        AdjusterAppointed { AdjusterAppointed_Id = l.AdjusterId, Description_AdjusterAppointed = l.AdjusterName }).ToList();
            if (addAll)
            {
                list.Insert(0, new AdjusterAppointed() { AdjusterAppointed_Id = 0, Description_AdjusterAppointed = "[Select...]" });
            }
            obj.Dispose();
            return list;
        }

        protected List<AdjusterAppointed> LoadSeverityDdl(string AdjTypeCode)
        {
            MCASEntities obj = new MCASEntities();
            var list = obj.MNT_Adjusters.Where(l => l.AdjusterTypeCode == AdjTypeCode).Select(x => new AdjusterAppointed
            {
                AdjusterAppointed_Id = x.AdjusterId,
                Description_AdjusterAppointed = x.AdjusterName
            }).OrderBy(l => l.Description_AdjusterAppointed).ToList();
            if (list.Count >= 1)
            {
                list.Insert(0, new AdjusterAppointed() { AdjusterAppointed_Id = 0, Description_AdjusterAppointed = "[Select...]" });
            }
            obj.Dispose();
            return list.ToList();
        }
        protected List<LawyerAppointed> LoadLawyerAppointed(string AdjTypeCode)
        {
            MCASEntities obj = new MCASEntities();
            var list = obj.MNT_Adjusters.Where(l => l.AdjusterTypeCode == AdjTypeCode).Select(x => new LawyerAppointed
            {
                LawyerAppointed_Id = x.AdjusterId,
                Description_LawyerAppointed = x.AdjusterName
            }).OrderBy(l => l.Description_LawyerAppointed).ToList();
            if (list.Count >= 1)
            {
                list.Insert(0, new LawyerAppointed() { LawyerAppointed_Id = 0, Description_LawyerAppointed = "[Select...]" });
            }
            obj.Dispose();
            return list.ToList();
        }
        protected List<SurveyorAppointed> LoadSurveyorAppointed(string AdjTypeCode)
        {
            MCASEntities obj = new MCASEntities();
            var list = obj.MNT_Adjusters.Where(l => l.AdjusterTypeCode == AdjTypeCode).Select(x => new SurveyorAppointed
            {
                SurveyorAppointed_Id = x.AdjusterId,
                Description_SurveyorAppointed = x.AdjusterName
            }).OrderBy(l => l.Description_SurveyorAppointed).ToList();
            if (list.Count >= 1)
            {
                list.Insert(0, new SurveyorAppointed() { SurveyorAppointed_Id = 0, Description_SurveyorAppointed = "[Select...]" });
            }
            obj.Dispose();
            return list.ToList();
        }

        protected List<ReverseEditorModel> GetReserveall(int ClaimantNo)
        {
            Decimal dec = 0.0m;
            MCASEntities db = new MCASEntities();
            List<ReverseEditorModel> list = new List<ReverseEditorModel>();
            list = db.CLM_ThirdParty.Where(
                l => l.TPartyId == ClaimantNo).Select(x => new ReverseEditorModel
                {
                    PreResOrgCurrCode = x.ReserveCurr == null ? "USD" : x.ReserveCurr,
                    PreReserveOrgAmt = x.ReserveAmt == null ? dec : x.ReserveAmt,
                    PreExRateLocalCurr = x.ReserveExRate == null ? dec : x.ReserveExRate,
                    PreReserveLocalAmt = x.ReserveAmount == null ? dec : x.ReserveAmount
                }).ToList();
            db.Dispose();
            return list;
        }

        protected List<DepotWorkshop> LoadDepotWorkshop()
        {
            MCASEntities obj = new MCASEntities();
            var list = obj.MNT_DepotMaster.OrderBy(x => x.DepotReference).Select(x => new DepotWorkshop
            {
                //DepotWorkshop_Id = x.DepotCode
                DepotWorkshop_Id = x.DepotId,
                Description_DepotWorkshop = x.DepotReference
            }).OrderBy(l => l.Description_DepotWorkshop).ToList();
            if (list.Count >= 1)
            {
                //    list.Insert(0, new DepotWorkshop() { DepotWorkshop_Id = "", Description_DepotWorkshop = "[Select...]" });
                list.Insert(0, new DepotWorkshop() { DepotWorkshop_Id = 0, Description_DepotWorkshop = "[Select...]" });
            }
            obj.Dispose();
            return list.ToList();
        }
        protected String GetGroupcode(string Userid)
        {
            MCASEntities _db = new MCASEntities();
            var groupcode = (from m in _db.MNT_Users join g in _db.MNT_GroupsMaster on m.GroupId equals g.GroupId where m.UserId == Userid select g.GroupCode).FirstOrDefault();
            _db.Dispose();
            return groupcode;
        }

        public String RoleCode(string Userid)
        {
            MCASEntities db = new MCASEntities();
            var rid = string.Empty;
            try
            {

                rid = (from x in db.MNT_Users
                       join y in db.MNT_GroupsMaster on x.GroupId equals y.GroupId
                       where x.UserId == Userid
                       select y.RoleCode).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return rid;
        }
        protected List<UserList> LoadDiaryUsrList(bool addAll = true)
        {
            MCASEntities _db = new MCASEntities();
            List<UserList> list = new List<UserList>();
            list = (from x in _db.MNT_Users orderby x.UserDispName select new UserList { SNO = System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.SNo).Trim(), UserDispName = x.UserDispName }).ToList();
            _db.Dispose();
            return list;
        }

        protected int? getcompletestatus(int AccidentClaimId)
        {
            MCASEntities _db = new MCASEntities();
            var result = (from m in _db.ClaimAccidentDetails where m.AccidentClaimId == AccidentClaimId select m.IsComplete).FirstOrDefault();
            return result;
        }

        protected string getPageViewMode(string caller)
        {
            string viewMode = "Write";
            switch (caller)
            {
                case ("New"):
                    { viewMode = "Write"; break; }
                case ("Incomplete"):
                    { viewMode = "Write"; break; }
                case ("Adjustment"):
                    { viewMode = "Read"; break; }
                case ("RegEnquiry"):
                    { viewMode = "Read"; break; }
                default:
                    { viewMode = caller == "" ? "Read" : caller; break; }
            }

            return viewMode;
        }
        public System.Collections.Specialized.NameValueCollection MCASQueryString
        {
            get
            {
                return MCAS.Globalisation.MvcExtensions.MCASQueryString(null, Request.QueryString);
            }
        }
        protected SecurityPermissions ValidateUserPermission(int screenid)
        {
            SecurityPermissions UserPermision = new SecurityPermissions();
            string userGroupid = UserGroupId.ToString();
            MCASEntities dbEntity = new MCASEntities();
            MNT_GroupPermission groupPermi = (from P in dbEntity.MNT_GroupPermission
                                              where P.GroupId == userGroupid && P.MenuId == screenid
                                              select P).FirstOrDefault();
            if (groupPermi != null)
            {
                UserPermision.Read = groupPermi.Read != null ? (bool)groupPermi.Read : UserPermision.Read;
                UserPermision.Write = groupPermi.Write != null ? (bool)groupPermi.Write : UserPermision.Write;
                UserPermision.Delete = groupPermi.Delete != null ? (bool)groupPermi.Delete : UserPermision.Delete;
                UserPermision.SplPermission = groupPermi.SplPermission != null ? (bool)groupPermi.SplPermission : UserPermision.SplPermission;
            }
            else
            {
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
            }
            dbEntity.Dispose();
            return UserPermision;
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #region publishing application Exceptions
        protected void PublishException(Exception ex)
        {
            PublishException(ex, null, 0, "");
        }
        protected void PublishException(Exception ex, int entityId, string entityType)
        {
            PublishException(ex, null, entityId, entityType);
        }
        protected void PublishException(Exception ex, NameValueCollection additionalInfo, int entityId, string entityType)
        {
            CommonUtilities.ExceptionManager.PublishException(ex, additionalInfo, entityId, entityType, UserId);
        }
        #endregion

        #region maintaining sessions
        protected void SetMenuList(List<MenuListItem> menuList)
        {
            Session["menuList"] = menuList;
        }
        protected List<MenuListItem> getMenuList()
        {
            if (Session != null && Session["menuList"] != null)
                return (List<MenuListItem>)Session["menuList"];
            else
            {
                LoggedOut();
                return null;
            }

        }
        protected void ClearClaimObjectHelper()
        {
            Session["ClaimObject"] = null;
        }

        protected void SetClaimObjectHelper(ClaimObjectHelper model)
        {
            Session["ClaimObject"] = model;
        }
        protected ClaimObjectHelper getClaimObjectHelper()
        {
            if (Session["ClaimObject"] != null)
                return (ClaimObjectHelper)Session["ClaimObject"];
            else
                return new ClaimObjectHelper();
        }
        protected ClaimEntryInfoModel getClaimEntryInfoObject()
        {
            getClaimObjectHelper().ClaimDetail.ViewMode = getClaimObjectHelper().ViewMode;
            return getClaimObjectHelper().ClaimDetail;
        }
        protected ClaimAccidentDetailsModel getClaimAccidentObject()
        {
            getClaimObjectHelper().AccidentDetail.ViewMode = getClaimObjectHelper().ViewMode;
            getClaimObjectHelper().AccidentDetail.IsComplete = getClaimObjectHelper().IsComplete;
            return getClaimObjectHelper().AccidentDetail;
        }
        protected string CallerMenu
        {
            set
            {
                Session["CallerMenu"] = value;
            }
            get
            {
                if (Session["CallerMenu"] == null)
                {
                    LoggedOut();
                    return null;
                }
                else
                    return Session["CallerMenu"].ToString();
            }
        }
        protected int UserId
        {
            set
            {
                Session["UserId"] = value;
            }
            get
            {
                if (Session["UserId"] == null)
                {
                    LoggedOut();
                    return -1;
                }
                else
                    return Convert.ToInt32(Session["UserId"]);
            }
        }
        protected string SessionId
        {
            set
            {
                Session["SessionId"] = value;
            }
            get
            {
                if (Session["SessionId"] == null)
                {
                    LoggedOut();
                    return null;
                }
                else
                    return Session["SessionId"].ToString();
            }
        }
        protected int UserGroupId
        {
            set
            {
                Session["UserGroupId"] = value;
            }
            get
            {
                if (Session["UserGroupId"] == null)
                {
                    LoggedOut();
                    return -1;
                }
                else
                    return Convert.ToInt32(Session["UserGroupId"]);
            }
        }
        protected string LoggedInUserId
        {
            set
            {
                Session["LoggedInUserId"] = value;
            }
            get
            {
                if (Session["LoggedInUserId"] == null)
                {
                    LoggedOut();
                    return null;
                }
                else
                    return Session["LoggedInUserId"].ToString();
            }
        }
        public void InsertLogOutTime()
        {
            if (Session["LoginAuditLogId"] != null)
            {
                MCASEntities obj = new MCASEntities();
                obj.Proc_InsertLogOutTime(Int32.Parse(Session["LoginAuditLogId"].ToString()));
                obj.Dispose();
            }
        }
        protected string LoggedInUserName
        {
            set
            {
                Session["LoggedInUserName"] = value;
            }
            get
            {
                if (Session["LoggedInUserName"] == null)
                {
                    LoggedOut();
                    return null;
                }
                else
                    return Session["LoggedInUserName"].ToString();
            }
        }
        protected string LoggedInBranch
        {
            set
            {
                Session["LoggedInBranch"] = value;
            }
            get
            {
                if (Session["LoggedInBranch"] == null)
                {
                    LoggedOut();
                    return null;
                }
                else
                    return Session["LoggedInBranch"].ToString();
            }
        }
        protected int LoggedInCountryId
        {
            set
            {
                Session["LoggedInCountryId"] = value;
            }
            get
            {
                if (Session["LoggedInCountryId"] == null)
                {
                    LoggedOut();
                    return -1;
                }
                else
                    return (int)Session["LoggedInCountryId"];
            }
        }
        protected string LoggedInCountryName
        {
            set
            {
                Session["LoggedInCountryName"] = value;
            }
            get
            {
                if (Session["LoggedInCountryName"] == null)
                {
                    LoggedOut();
                    return null;
                }
                else
                    return Session["LoggedInCountryName"].ToString();
            }
        }
        protected ActionResult LoggedOut()
        {
            try
            {
                UserLogin.UpdateLoginDetails();
                Session.Clear();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, ex.InnerException);
            }
            return RedirectToAction("SessionTimeOut", "Home");
        }
        #endregion

        #region overide on actionExecuting
        [NonAction]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MCASEntities obj = new MCASEntities();
            var activeusrsession = (from m in obj.MNT_Users where m.UserId == LoggedInUserId select m.SessionId).FirstOrDefault();
            //filterContext.HttpContext.Trace.Write("(Controller)Action Executing: " +   filterContext.ActionDescriptor.ActionName);
            //Request.IsAjaxRequest() && (!Request.IsAuthenticated
            if (filterContext.ActionDescriptor.ActionName != "Login" && filterContext.ActionDescriptor.ActionName != "SessionTimeOut" && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (activeusrsession != SessionId)
                {
                    TempData["msg"] = "You have been logged out due to multiple login occur from your id.";
                    filterContext.Result = LoggedOut();
                    return;
                }

                if (Session["UserId"] == null)
                {
                    filterContext.Result = LoggedOut();//new RedirectResult(url);
                    return;
                }
                /*if (filterContext.ActionParameters.Count > 0)
                {
                    var model = filterContext.ActionParameters["model"];
                }*/
            }
            base.OnActionExecuting(filterContext);
        }

        [NonAction]
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                //    filterContext.HttpContext.Trace.Write("(Controller)Exception thrown");
                Exception ex = filterContext.Exception;
                if (!(ex is System.Threading.ThreadAbortException))
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;

                    filterContext.ExceptionHandled = true;

                    RedirectToAction("Index", "Error", ex.Message);
                }
            }
            if (filterContext.ActionDescriptor.ActionName != "Login" && filterContext.ActionDescriptor.ActionName != "SessionTimeOut")
            {
                ViewDataDictionary vwModel = new ViewDataDictionary(filterContext.Controller.ViewData.Model);
                BaseModel model = null;
                string screenID = "0";
                if (vwModel != null && vwModel.Model != null && vwModel.Model.GetType().BaseType.Name == "BaseModel")
                {
                    model = vwModel != null ? vwModel.Model != null ? (BaseModel)vwModel.Model : null : null;
                    screenID = model != null ? model.screenId != null ? model.screenId : "0" : "0";
                }
                else if (vwModel != null && vwModel.Model != null)
                {
                    IEnumerable<BaseModel> modelList = (IEnumerable<BaseModel>)vwModel.Model;
                    if (modelList != null && modelList.Count() != 0)
                    {
                        model = modelList.FirstOrDefault();
                        //screenID = model != null ? model.listscreenId != null ? model.listscreenId : "0" : "0";
                    }
                    else
                    {
                        Type collectionType = modelList.GetType();
                        Type parameterType = collectionType.GetGenericArguments()[0];
                        object ModelObject = Activator.CreateInstance(parameterType);
                        model = (BaseModel)ModelObject;
                    }
                    screenID = model != null ? model.listscreenId != null ? model.listscreenId : "0" : "0";
                }
                //as BaseModel;
                if (model != null)
                {
                    //RedirectToAction("Index", "Error");
                    SecurityPermissions mPermission = ValidateUserPermission(int.Parse(screenID));
                    if (!mPermission.Read)
                    {
                        //System.Web.Routing.RouteValueDictionary accessDeniedRouteValues = new System.Web.Routing.RouteValueDictionary(new { statusCode = "500", exception = "AccessDenied" });
                        filterContext.Result = RedirectToAction("SecurityIndex", "Error");//, accessDeniedRouteValues); //new ActionRedirectResult(accessDeniedRouteValues);
                        filterContext.Canceled = true;
                    }
                    else
                    {
                        model.UserPermissions = mPermission;
                        ViewData["UserPermissions"] = mPermission;
                        base.OnActionExecuted(filterContext);
                    }
                    model.Resource = GetResourceName(Convert.ToInt32(screenID));
                    ViewData["PageResource"] = GetResourceName(Convert.ToInt32(screenID));
                }
                else
                {
                    base.OnActionExecuted(filterContext);
                }
                TempData["MenuList"] = getMenuList();
            }
            else
            {
                base.OnActionExecuted(filterContext);
            }

        }

        #endregion

    }

    public class AcceptParameterAttribute : ActionMethodSelectorAttribute
    {
        public string Name { get; set; }
        //public string Value { get; set; }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var req = controllerContext.RequestContext.HttpContext.Request;
            //return req.Form[this.Name] == this.Value;
            return req.Form[this.Name] != null;
        }
    }

    public class HttpParamActionAttribute : ActionNameSelectorAttribute
    {
        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                return true;

            if (!actionName.Equals("Action", StringComparison.InvariantCultureIgnoreCase))
                return false;

            var request = controllerContext.RequestContext.HttpContext.Request;
            return request[methodInfo.Name] != null;
        }
    }
    // by PK Chandel to enforce security on querystring parameters
    #region decryption of query string params
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"];
            if (isEncryptedParams.ToUpper().Equals("YES"))
            {
                Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
                if (HttpContext.Current.Request.QueryString.Get("Q") != null)
                {
                    string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("Q");
                    string decrptedString = RouteEncryptDecrypt.Decrypt(encryptedQueryString.ToString());
                    string[] paramsArrs = decrptedString.Split('?');

                    for (int i = 0; i < paramsArrs.Length; i++)
                    {
                        string[] paramArr = paramsArrs[i].Split('=');
                        int intOut;
                        if (paramArr.Length != 2)
                        {
                            decryptedParameters.Add(paramArr[0], "0");
                        }

                        else if (int.TryParse(paramArr[1], out intOut))
                            decryptedParameters.Add(paramArr[0], intOut);
                        else
                            decryptedParameters.Add(paramArr[0], (paramArr[1]));
                    }
                }
                for (int i = 0; i < decryptedParameters.Count; i++)
                {
                    if (!filterContext.ActionParameters.Contains(new KeyValuePair<string, object>(decryptedParameters.Keys.ElementAt(i), decryptedParameters.Values.ElementAt(i))))
                        filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
                }
            }
            base.OnActionExecuting(filterContext);

        }

    }



    #endregion

    #region Rule varification Layer
    public class ClaimRules
    {
        #region Rule enum
        public enum MainRuleType
        {
            AppDependent = 1,
            PolicyDependent = 2,
            ProcessDependent = 3
        }
        public enum SubSubRuleType
        {
            Mandatory = 1,
            Reject = 2,
            Refered = 3
        }
        public enum SubRuleType
        {
            All = 1,
            NewBusiness = 2,
            Endorsement = 3,
            Renewal = 4,
            CorrectiveUser = 5,
            Cancellation = 6
        }
        #endregion
        public NumberFormatInfo numberFormatInfo;
        protected Hashtable RuleKeys;
        protected Hashtable ProductMasterRuleKeys;

        public ClaimRules()
        {
            RuleKeys = new Hashtable();
            ProductMasterRuleKeys = new Hashtable();
        }
        #region rule validation
        public string verifyClaim(int AccidentId, ref bool isValid)
        {
            string strInputXML = FetchClaimRuleInputXML(AccidentId);
            bool validXML = false;
            validXML = ValidateProductInputRuleXML(ref strInputXML, AccidentId, MainRuleType.AppDependent, SubRuleType.All);
            if (!validXML)
            {
                strInputXML = InputMessagesXML(strInputXML);
            }
            isValid = validXML;
            return strInputXML;

        }
        private bool ValidateProductInputRuleXML(ref string inputXML, int AccidentId, MainRuleType mRuleType, SubRuleType mSubRuleType)
        {
            try
            {
                string Language_code = BaseController.enumCulture.US;
                numberFormatInfo = new CultureInfo(Language_code, true).NumberFormat;
                bool retVal = true;
                XmlDocument tempDoc = new XmlDocument();
                tempDoc.LoadXml(inputXML);
                XmlElement tempElement = tempDoc.DocumentElement;
                XmlNodeList tempNodes = tempElement.ChildNodes;
                //string CustomiseRuleFilePatch = //ClsCommon.GetKeyValueWithIP("FilePathCustomiseClaimRuleXML");
                string CustomiseRuleFilePatch = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + "Support/ClaimRule.xml");
                XmlDocument RuleDoc = new XmlDocument();
                RuleDoc.Load(CustomiseRuleFilePatch);
                retVal = ValidateRuleXMLByRuleNode(tempDoc, RuleDoc, AccidentId, "0", mRuleType, mSubRuleType);
                inputXML = tempDoc.InnerXml;
                return retVal;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {

            }
        }
        private string FetchClaimRuleInputXML(int AccidentId)
        {
            StringBuilder returnString = new StringBuilder();

            try
            {
                String strReturnXML = "";
                DataSet dsTemp;
                returnString.Remove(0, returnString.Length);
                MCASEntities dbEntity = new MCASEntities();

                dbEntity.AddParameter("@ACCIDENTCLAIM_ID", AccidentId.ToString());
                dbEntity.AddParameter("@CLAIM_ID", "0");
                dsTemp = dbEntity.ExecuteDataSet("Proc_GetClaimRuleXML", CommandType.StoredProcedure);
                dbEntity.ClearParameteres();
                dbEntity.Dispose();

                returnString.Append("<INPUTXML>");

                // get the Accident info
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("ACCIDENTS", "ACCIDENTERROR", ref returnString);
                // get the Claim info 
                if (dsTemp.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemp.Tables[1].Rows)
                    {
                        strReturnXML = dr[0].ToString();
                        returnString.Append(strReturnXML);
                    }
                }
                else
                    AddErrorNodeToRuleXML("CLAIMS", "CLAIMERROR", ref returnString);

                // get the TP info 
                //if (dsTemp.Tables[2].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsTemp.Tables[2].Rows)
                //    {
                //        strReturnXML = dr[0].ToString();
                //        returnString.Append(strReturnXML);
                //    }
                //}
                //else
                //    AddErrorNodeToRuleXML("TPARTIES", "TPARTYERROR", ref returnString);
                // get the Notes info 
                //if (dsTemp.Tables[2].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsTemp.Tables[3].Rows)
                //    {
                //        strReturnXML = dr[0].ToString();
                //        returnString.Append(strReturnXML);
                //    }
                //}
                //else
                //    AddErrorNodeToRuleXML("NOTES", "NOTEERROR", ref returnString);

                // get the Transaction  info 
                //if (dsTemp.Tables[4].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsTemp.Tables[4].Rows)
                //    {
                //        strReturnXML = dr[0].ToString();
                //        returnString.Append(strReturnXML);
                //    }
                //}
                //else
                //    AddErrorNodeToRuleXML("TRANSACTIONS", "TRANSACTIONERROR", ref returnString);

                // get the Diary  info 
                //if (dsTemp.Tables[3].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsTemp.Tables[5].Rows)
                //    {
                //        strReturnXML = dr[0].ToString();
                //        returnString.Append(strReturnXML);
                //    }
                //}
                //else
                //    AddErrorNodeToRuleXML("DIARY", "DIARYERROR", ref returnString);


                returnString.Append("</INPUTXML>");
                dbEntity.Dispose();
                return returnString.ToString();


            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        #region private support methods
        // Return the messages 
        private string InputMessagesXML(string strInputXML)
        {
            string strRulePath = "", returnString = "";
            // switch case on the basis of the Product to get the inputmessage xsl
            string msgFilepath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/Support/RuleMessages.xml");
            strInputXML = strInputXML.Replace("<INPUTXML>", "<INPUTXML>" + "<MESSAGE_FILE_PATH>" + msgFilepath + "</MESSAGE_FILE_PATH>");
            strRulePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/Support/UWRulesInputMsg.xsl"); //getKeyValueWithIP("FilePathInputMessageXSLProducts");
            //Transform and show the message for invalid inputs
            //XslTransform xslt = new XslTransform();
            // Create the XsltSettings object with script enabled.
            XsltSettings settings = new XsltSettings(true, false);
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(strRulePath, settings, new XmlUrlResolver());
            StringWriter writer = new StringWriter();
            XmlDocument xmlDocTemp = new XmlDocument();
            xmlDocTemp.LoadXml(strInputXML);
            XPathNavigator nav = ((IXPathNavigable)xmlDocTemp).CreateNavigator();
            xslt.Transform(nav, null, writer);
            returnString = writer.ToString();
            return returnString;

        }
        private void AddErrorNodeToRuleXML(string ScreenNode, string errorNode, ref StringBuilder returnString)
        {
            returnString.Append("<" + ScreenNode + ">");
            returnString.Append("<ERRORS>");
            returnString.Append("<" + errorNode + " ERRFOUND = 'T'/>");
            returnString.Append("</ERRORS>");
            returnString.Append("</" + ScreenNode + ">");
        }
        private bool ValidateRuleXMLByRuleNode(XmlDocument InputRuleDoc, XmlDocument RuleDoc, int AccidentId, string ProductID, MainRuleType mRuleType, SubRuleType mSubRuleType)
        {
            bool retVal = true;
            string rootNode = "INPUTXML";
            XmlNode groupNode = RuleDoc.SelectSingleNode("Root/Group[@Code='" + mRuleType.ToString() + "']");
            XmlNode subGroupNode = groupNode.SelectSingleNode("SubGroup[@Code='" + mSubRuleType + "']");
            if (subGroupNode == null)
                subGroupNode = groupNode.SelectSingleNode("SubGroup[@Code='" + SubRuleType.All.ToString() + "']");
            //Parsing Mandatory Rule
            XmlNode subSubGroupNode = subGroupNode.SelectSingleNode("SubSubGroup[@Code='" + SubSubRuleType.Mandatory.ToString() + "']");
            string EffDate = InputRuleDoc.SelectSingleNode(rootNode + "/ACCIDENTS/ACCIDENTDETAIL/AccidentDate").InnerText;
            string StateId = "0";// InputRuleDoc.SelectSingleNode(rootNode + "/ACCIDENTS/ACCIDENT/STATE_ID").InnerText;
            InitialiseMasterRules(AccidentId, RuleDoc);
            ArrayList arEffectiveRules = GetProductEffectiveRules(subSubGroupNode, Convert.ToDateTime(EffDate), StateId, ProductID);
            for (int i = 0; i < arEffectiveRules.Count; i++)
            {
                XmlNode ruleNode = (XmlNode)arEffectiveRules[i];
                XmlNodeList ScreensNodeArray = ruleNode.SelectNodes("Screen");
                foreach (XmlNode ScreenNode in ScreensNodeArray)
                {
                    string screenId = ScreenNode.Attributes["ID"].InnerText;
                    string IsRequired = ScreenNode.Attributes["IsRequired"].InnerText;
                    //if (IsRequired != "Y") continue;
                    XmlNode RuleXmlNode = InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId);
                    if (RuleXmlNode == null) return false;
                    XmlAttribute AttCount = InputRuleDoc.CreateAttribute("COUNT");
                    foreach (XmlNode RiskRuleNode in RuleXmlNode.ChildNodes)
                    {
                        //InitialiseScreenRules(ScreenNode, RuleXmlNode);
                        string SubScreenID = RiskRuleNode.Name.ToString();
                        if (SubScreenID == "ERRORS")
                        {
                            AttCount.Value = "0";
                            string strValidKey = ValidateWithMasterKeys(screenId, ScreenNode);
                            if (strValidKey == "True")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }
                            if (strValidKey == "NoKey")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }

                            else if (strValidKey == "False")
                                AttCount.Value = "-1";
                            RuleXmlNode.Attributes.Append(AttCount);
                            continue;
                        }
                        AttCount.Value = RuleXmlNode.ChildNodes.Count.ToString();
                        RuleXmlNode.Attributes.Append(AttCount);
                        InitialiseScreenRules(ScreenNode, RiskRuleNode);
                        foreach (object key in RuleKeys.Keys)
                        {
                            XmlNode InnerNode = RiskRuleNode.SelectSingleNode(key.ToString());
                            if (InnerNode != null)
                            {
                                string RuleValue = InnerNode.InnerText;
                                string KeyValue = RuleKeys[key].ToString();
                                if (KeyValue.StartsWith("$"))
                                    KeyValue = RiskRuleNode.SelectSingleNode(KeyValue.Substring(1)).InnerText;
                                if (!((Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "tx" || Convert.ToString(HttpContext.Current.Session["OrganisationType"]).ToLower() == "pc") && InnerNode.Name.ToLower() == "claimdate" && SubScreenID.ToLower() == "claimdetail"))
                                {
                                    if (RuleValue == KeyValue)
                                    {
                                        RiskRuleNode.SelectSingleNode(key.ToString()).InnerText = "";
                                        retVal = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!retVal) return retVal;
            //Parsing Reject Rule
            subSubGroupNode = subGroupNode.SelectSingleNode("SubSubGroup[@Code='" + SubSubRuleType.Reject.ToString() + "']");
            arEffectiveRules = GetProductEffectiveRules(subSubGroupNode, Convert.ToDateTime(EffDate), StateId, ProductID);

            for (int i = 0; i < arEffectiveRules.Count; i++)
            {
                XmlNode ruleNode = (XmlNode)arEffectiveRules[i];
                XmlNodeList ScreensNodeArray = ruleNode.SelectNodes("Screen");
                foreach (XmlNode ScreenNode in ScreensNodeArray)
                {
                    string screenId = ScreenNode.Attributes["ID"].InnerText;
                    string IsRequired = ScreenNode.Attributes["IsRequired"].InnerText;
                    XmlNode RuleXmlNode = InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId);
                    XmlAttribute AttCount = InputRuleDoc.CreateAttribute("COUNT");
                    foreach (XmlNode RiskRuleNode in RuleXmlNode.ChildNodes)
                    {
                        string SubScreenID = RiskRuleNode.Name.ToString();
                        if (SubScreenID == "ERRORS")
                        {
                            AttCount.Value = "0";
                            string strValidKey = ValidateWithMasterKeys(screenId, ScreenNode);
                            if (strValidKey == "True")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }
                            else if (strValidKey == "False")
                                AttCount.Value = "-1";
                            RuleXmlNode.Attributes.Append(AttCount);
                            continue;
                        }

                        //if (SubScreenID == "ERRORS")
                        //{
                        //    AttCount.Value = "0";
                        //    RuleXmlNode.Attributes.Append(AttCount);
                        //    retVal = false; continue;
                        //}
                        AttCount.Value = RuleXmlNode.ChildNodes.Count.ToString();
                        RuleXmlNode.Attributes.Append(AttCount);
                        InitialiseScreenRules(ScreenNode, RiskRuleNode);
                        foreach (object key in RuleKeys.Keys)
                        {
                            XmlNode InnerNode = RiskRuleNode.SelectSingleNode(key.ToString());
                            if (InnerNode != null)
                            {
                                string RuleValue = InnerNode.InnerText;
                                string KeyValue = RuleKeys[key].ToString();
                                if (KeyValue.StartsWith("$"))
                                    KeyValue = RiskRuleNode.SelectSingleNode(KeyValue.Substring(1)).InnerText;
                                if (RuleValue == KeyValue)
                                {
                                    RiskRuleNode.SelectSingleNode(key.ToString()).InnerText = "Y";
                                }
                            }
                        }
                    }
                }
            }
            //Parsing refered Rule
            subSubGroupNode = subGroupNode.SelectSingleNode("SubSubGroup[@Code='" + SubSubRuleType.Refered.ToString() + "']");
            arEffectiveRules = GetProductEffectiveRules(subSubGroupNode, Convert.ToDateTime(EffDate), StateId, ProductID);

            for (int i = 0; i < arEffectiveRules.Count; i++)
            {
                XmlNode ruleNode = (XmlNode)arEffectiveRules[i];
                XmlNodeList ScreensNodeArray = ruleNode.SelectNodes("Screen");
                foreach (XmlNode ScreenNode in ScreensNodeArray)
                {
                    string screenId = ScreenNode.Attributes["ID"].InnerText;
                    string IsRequired = ScreenNode.Attributes["IsRequired"].InnerText;
                    XmlNode RuleXmlNode = InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId);
                    XmlAttribute AttCount = InputRuleDoc.CreateAttribute("COUNT");
                    foreach (XmlNode RiskRuleNode in RuleXmlNode.ChildNodes)
                    {
                        string SubScreenID = RiskRuleNode.Name.ToString();
                        if (SubScreenID == "ERRORS")
                        {
                            AttCount.Value = "0";
                            string strValidKey = ValidateWithMasterKeys(screenId, ScreenNode);
                            if (strValidKey == "True")
                            {
                                if (IsRequired == "Y") retVal = false;
                            }
                            else if (strValidKey == "False")
                                AttCount.Value = "-1";
                            RuleXmlNode.Attributes.Append(AttCount);
                            continue;
                        }
                        AttCount.Value = RuleXmlNode.ChildNodes.Count.ToString();
                        RuleXmlNode.Attributes.Append(AttCount);
                        InitialiseScreenRules(ScreenNode, RiskRuleNode);
                        foreach (object key in RuleKeys.Keys)
                        {
                            XmlNode InnerNode = RiskRuleNode.SelectSingleNode(key.ToString());
                            if (InnerNode != null)
                            {
                                string RuleValue = InnerNode.InnerText;
                                string KeyValue = RuleKeys[key].ToString();
                                if (KeyValue.StartsWith("$"))
                                    KeyValue = RiskRuleNode.SelectSingleNode(KeyValue.Substring(1)).InnerText;
                                if (RuleValue == KeyValue)
                                {
                                    //InputRuleDoc.SelectSingleNode(rootNode + "/" + screenId + "/" + SubScreenID + "/" + key.ToString()).InnerText = "Y";
                                    RiskRuleNode.SelectSingleNode(key.ToString()).InnerText = "Y";
                                }
                            }
                        }
                    }
                }
            }
            return retVal;
        }
        // InitialiseRules initialise Hashtable from data fetched from SP
        private void InitialiseMasterRules(int AccidentId, XmlDocument RuleDoc)
        {
            XmlNode masterNode = RuleDoc.SelectSingleNode("Root/Master");
            string strQuery = "";
            XmlNode queryNode = masterNode.SelectSingleNode("Query[@Level='CLAIM']");
            strQuery = queryNode == null ? "" : queryNode.InnerText;
            if (strQuery == "") return;
            MCASEntities dbEntity = new MCASEntities();

            dbEntity.ClearParameteres();
            dbEntity.AddParameter("@ACCIDENTID", AccidentId.ToString());
            DataSet dtKeyValue = dbEntity.ExecuteDataSet(strQuery, CommandType.StoredProcedure);
            dbEntity.ClearParameteres();
            dbEntity.Dispose();

            if (ProductMasterRuleKeys.Count > 0)
            {
                ProductMasterRuleKeys.Clear();
            }
            XmlNodeList columnNodes = masterNode.SelectNodes("Column");

            if (dtKeyValue.Tables.Count > 0)
            {
                if (dtKeyValue.Tables[0].Rows.Count > 0)
                {

                    foreach (XmlNode node in columnNodes)
                    {
                        string strKey = node.Attributes["Code"].Value;
                        string strValue = "";
                        if (node.Attributes["MapColumnn"].Value.Trim() != "")
                        {
                            if (dtKeyValue.Tables[0].Rows[0][strKey] != DBNull.Value)
                            {
                                strValue = dtKeyValue.Tables[0].Rows[0][strKey].ToString().Trim();
                                //If the returned value is Negative make it Zero
                                if (strValue.StartsWith("-"))
                                {
                                    strValue = "0";
                                }
                            }
                            ProductMasterRuleKeys.Add(strKey, strValue);
                        }
                    }
                }
            }
        }

        private ArrayList GetProductEffectiveRules(XmlNode parentNode, DateTime AppEffectiveDate, string StateId, string ProductId)
        {
            ArrayList ruleNodes = new ArrayList();
            DateTime startDate, endDate;
            XmlNodeList dataNodes = parentNode.SelectNodes("Rule[@Action='Database']");
            foreach (XmlNode effectiveRule in dataNodes)
            {
                startDate = Convert.ToDateTime(effectiveRule.Attributes["StartDate"].Value);
                endDate = Convert.ToDateTime(effectiveRule.Attributes["EndDate"].Value);
                if (AppEffectiveDate >= startDate && AppEffectiveDate <= endDate)
                {
                    if ((effectiveRule.Attributes["STATE_ID"] == null
                        || effectiveRule.Attributes["STATE_ID"].Value == StateId
                        || effectiveRule.Attributes["STATE_ID"].Value == "0"
                        )
                        &&
                        (effectiveRule.Attributes["PRODUCT_ID"] == null
                         || effectiveRule.Attributes["PRODUCT_ID"].Value == ProductId
                         || effectiveRule.Attributes["PRODUCT_ID"].Value == "0"
                         )
                        )
                    {
                        ruleNodes.Add(effectiveRule);
                    }
                }
            }
            return ruleNodes;

        }

        private string ValidateWithMasterKeys(string ScreenId, XmlNode ScreenNode)
        {
            //if (!ProductMasterRuleKeys.Contains(ScreenId)) return false;
            string retval = "NoKey";
            XmlNodeList ConditionsNodeArray = ScreenNode.SelectNodes("Conditions");
            foreach (XmlNode ConditionsNode in ConditionsNodeArray)
            {
                XmlNodeList conditionNodeArray = ConditionsNode.SelectNodes("Condition");
                foreach (XmlNode conditionNode in conditionNodeArray) //Condition Loop
                {
                    string strResult = this.EvalNode(conditionNode, ProductMasterRuleKeys);
                    if ((strResult == "True"))
                    {
                        retval = "True"; //return false;
                        return retval;
                    }
                    else if ((strResult == "NoKey"))
                        retval = "NoKey";
                    else
                    {
                        retval = "False";
                        XmlNodeList subConditionsNodeArray = conditionNode.SelectNodes("SubCondition");
                        if (subConditionsNodeArray != null)
                        {
                            foreach (XmlNode subConditionNode in subConditionsNodeArray)
                            {
                                string strRes = this.EvalNode(subConditionNode, ProductMasterRuleKeys);
                                if ((strRes == "True"))
                                {
                                    retval = "True";// return false;
                                    return retval;
                                }
                                else
                                    retval = strRes;
                            }
                        }
                        //break;
                    }
                }
            }
            return retval;
        }

        private void AddRemoveScreenRuleNode(XmlNode xmlNode)
        {
            //Parse each ToCompare node
            XmlNodeList ToSetNodes = xmlNode.SelectNodes("ToCompare");
            string strToSetKey = ""; string strValue = "";
            foreach (XmlNode node in ToSetNodes)
            {
                strToSetKey = node.Attributes["Key"].Value.Replace("$", "");
                strValue = "";
                //if (node.Attributes["Value"].Value.Trim() != "")
                {
                    strValue = node.Attributes["Value"].Value.Trim();
                    if (RuleKeys.Contains(strToSetKey)) RuleKeys.Remove(strToSetKey);
                    RuleKeys.Add(strToSetKey, strValue);
                }
            }
            //Parse each toRevoke node
            XmlNodeList ToRevokeNodes = xmlNode.SelectNodes("ToRevoke");
            string strRevokeKey = "";
            foreach (XmlNode node in ToRevokeNodes)
            {
                strRevokeKey = node.Attributes["Key"].Value.Replace("$", "");
                if (RuleKeys.Contains(strRevokeKey))
                    RuleKeys.Remove(strRevokeKey);
            }
        }
        private void InitialiseScreenRules(XmlNode ScreenNode, XmlNode RuleXmlNode)
        {
            if (RuleKeys.Count > 0) RuleKeys.Clear();
            XmlNodeList ConditionsNodeArray = ScreenNode.SelectNodes("Conditions");
            foreach (XmlNode ConditionsNode in ConditionsNodeArray)
            {
                this.AddRemoveScreenRuleNode(ConditionsNode);
                //Parse each condiation node
                XmlNodeList conditionNodeArray = ConditionsNode.SelectNodes("Condition");
                foreach (XmlNode conditionNode in conditionNodeArray) //Condition Loop
                {
                    string strResult = this.EvalNode(conditionNode, RuleXmlNode);
                    if (strResult == "True")
                    {
                        //add Condition verified rule
                        this.AddRemoveScreenRuleNode(conditionNode);
                        XmlNodeList subConditionsNodeArray = conditionNode.SelectNodes("SubCondition");
                        if (subConditionsNodeArray != null)
                        {
                            foreach (XmlNode subConditionNode in subConditionsNodeArray)
                            {
                                string strRes = this.EvalNode(subConditionNode, RuleXmlNode);
                                if (strRes == "True")
                                {
                                    this.AddRemoveScreenRuleNode(subConditionNode);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            //IsInitialised = true;
        }

        private string EvalNode(XmlNode node, XmlNode RuleNode)
        {
            string Operand1 = node.Attributes["Operand1"].Value;
            string Operand2 = node.Attributes["Operand2"].Value;
            string Operator = node.Attributes["Operator"].Value;
            string strTemp = "";
            //Check if the operand starts with "$" sign if yes fetch value from 
            //respective key from key value pair
            if (Operand1.StartsWith("$"))
            {
                strTemp = Operand1.Substring(Operand1.IndexOf('$') + 1);
                Operand1 = RuleNode.SelectSingleNode(strTemp).InnerText;
            }
            strTemp = "";
            if (Operand2.StartsWith("$"))
            {
                strTemp = Operand2.Substring(Operand2.IndexOf('$') + 1);
                Operand2 = RuleNode.SelectSingleNode(strTemp).InnerText;
            }
            bool result = false;
            //For comparing Strings 
            if (node.Attributes["OperandType"] != null)
            {
                string OperandType = node.Attributes["OperandType"].Value;
                if (OperandType == "String")
                {

                    if (Operator == "==")
                    {
                        if (Operand1 == Operand2)
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "!=")
                    {
                        if (Operand1 == Operand2)
                            result = false;
                        else
                            result = true;
                    }
                    return result.ToString();
                }
                if (OperandType == "Double")
                {
                    if (Operator == "==")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) == Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "!=")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) == Double.Parse(Operand2, this.numberFormatInfo))
                            result = false;
                        else
                            result = true;
                    }
                    else if (Operator == "&gt;" || Operator == ">")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) > Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "&lt;" || Operator == "<")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) < Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "&gt;=" || Operator == ">=")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) >= Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    else if (Operator == "&lt;=" || Operator == "<=")
                    {
                        if (Double.Parse(Operand1, this.numberFormatInfo) <= Double.Parse(Operand2, this.numberFormatInfo))
                            result = true;
                        else
                            result = false;
                    }
                    //return result.ToString();
                }
            }
            return result.ToString();
        }
        /// <summary>
        /// Evaluate An XML Condition Node
        /// </summary>
        /// <param name="node">XML Condition Node </param>
        /// <returns>Result Of Expression In Condition Node</returns>
        public string EvalNode(XmlNode node, Hashtable masterKeys)
        {
            string Operand1 = node.Attributes["Operand1"].Value;
            string Operand2 = node.Attributes["Operand2"].Value;
            string Operator = node.Attributes["Operator"].Value;
            string strTemp = "";

            if (Operand1.StartsWith("$"))
            {
                strTemp = Operand1.Substring(Operand1.IndexOf('$') + 1);
                Operand1 = masterKeys[strTemp].ToString();
                //Split the Operand if it contains Split Amount 
                string[] strSplits = Operand1.Split('/');
                if (strSplits.Length > 0)
                {
                    string[] strAmt1 = strSplits[0].Split(' ');
                    if (strAmt1.Length > 0)
                    {
                        if (strAmt1[0].Trim() != "")
                        {
                            Operand1 = strAmt1[0];
                        }
                    }
                }
            }
            strTemp = "";
            if (Operand2.StartsWith("$"))
            {
                strTemp = Operand2.Substring(Operand2.IndexOf('$') + 1);
                Operand2 = masterKeys[strTemp].ToString();

                //Split the Operand if it contains Split Amount 
                string[] strSplits = Operand2.Split('/');
                if (strSplits.Length > 0)
                {
                    string[] strAmt1 = strSplits[0].Split(' ');
                    if (strAmt1.Length > 0)
                    {
                        if (strAmt1[0].Trim() != "")
                        {
                            Operand2 = strAmt1[0];
                        }
                    }
                }
            }
            bool result = false;
            //For comparing Strings 
            if (node.Attributes["OperandType"] != null)
            {
                string OperandType = node.Attributes["OperandType"].Value;
                if (OperandType == "String")
                {

                    if (Operator == "==")
                    {
                        if (Operand1 == Operand2)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else if (Operator == "!=")
                    {
                        if (Operand1 == Operand2)
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    //return result.ToString();
                }
            }
            return result.ToString();
        }
        #endregion

        #endregion
    }
    #endregion
}
