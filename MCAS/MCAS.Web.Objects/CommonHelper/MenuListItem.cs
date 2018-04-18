using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web;
using MCAS.Entity;
using MCAS.Web.Objects.Resources.ClaimProcessing;
using MCAS.Globalisation;
namespace MCAS.Web.Objects.CommonHelper
{
    public class MenuListItem
    {
        #region Claim Tabs Menus/ Ids
        public sealed class ClaimTabs
        {
            #region readonly Properties/Ids
            readonly public static int ClaimAccidentEditor = 130;
            readonly public static int ClaimEditor = 131;
            readonly static public int ServiceProvider = 132;
            readonly static public int ClaimNotesEditor = 133;
            readonly static public int TaskEditor = 134;
            readonly static public int ClaimAttachmentsEditor = 135;
            readonly static public int DiaryTaskEditor = 136;
            readonly static public int ClaimReserveEditor = 137;
            readonly static public int ClaimMandateEditor = 138;
            readonly static public int ClaimPaymentEditor = 139;
            readonly static public int ClaimTransactionEditor = 140;
            readonly static public int TacFileUploadEditor = 272;
            readonly static public int TacFileUploadViewEditor = 273;
            readonly static public int ClaimFileUploadEditor = 274;
            readonly static public int ClaimFileUploadViewEditor = 275;
            readonly static public int ClaimRecoveryEditor = 294;
            #endregion

            #region get breadCum Strings Methods
            public static string SubMenuString(string caller, string accidentClaimIdNew, string mode, string screenName)
            {
                string strSubMenu = "";
                switch (screenName)
                {
                    case "ClaimRegistration":
                        {
                            if (caller == "New")
                            {
                                strSubMenu = ClaimRegistration.NewClaimRegistration;
                            }
                            else if (mode == "Incomplete")
                            {
                                strSubMenu = ClaimRegistration.Incompleteclaimregistration;
                            }
                            else if (mode == "Adjustment")
                            {
                                strSubMenu = ClaimRegistration.claimadjustments;
                            }
                            else if (mode == "RegEnquiry")
                            {
                                strSubMenu = ClaimRegistration.ClaimEnquiry;
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = ClaimRegistration.ClaimPaymentProcessing;
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = ClaimRegistration.ClaimRecoveryProcessing;
                            }
                            break;
                        }
                    case "ClaimAccident":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "IncompleteClaimRegistration";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NewClaimRegistration";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "ClaimAdjustments";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "ClaimEnquiry";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "ClaimPaymentProcessing";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "ClaimRecoveryProcessing";
                            }
                            break;
                        }
                    case "ClaimEditor":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "IncompleteClaimRegistration";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NewClaimRegistration";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "ClaimAdjustments";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "ClaimEnquiry";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "ClaimPaymentProcessing";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "ClaimRecoveryProcessing";
                            }
                            break;
                        }
                    case "ServiceProvider":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "IncompleteClaimRegistration";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NewClaimRegistration";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "ClaimAdjustments";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "ClaimEnquiry";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "ClaimPaymentProcessing";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "ClaimRecoveryProcessing";
                            }
                            break;
                        }
                    case "ClaimReserve":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "IncompleteClaimRegistration";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NewClaimRegistration";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "ClaimAdjustments";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "ClaimEnquiry";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "ClaimPaymentProcessing";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "ClaimRecoveryProcessing";
                            }
                            break;
                        }
                    case "ReadOnlyReserve":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "_R";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "_R";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "_I";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "_I";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "_I";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "_I";
                            }
                            break;
                        }
                    case "ShowHideButtonInGridInReserve":
                        {
                            if (caller == "Write" && !String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "6";
                            }
                            else if (caller == "Write" && mode == "")
                            {
                                strSubMenu = "6";
                            }
                            else if (caller == "Write" && mode == "Adj")
                            {
                                strSubMenu = "0";
                            }
                            break;
                        }
                    case "ClaimPayment":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "IncompleteClaimRegistration";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NewClaimRegistration";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "ClaimAdjustments";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "ClaimEnquiry";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "ClaimPaymentProcessing";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "ClaimRecoveryProcessing";
                            }
                            break;
                        }
                    case "ReadOnlyPayment":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "In";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NeW";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "Adj";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "Enq";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "Pay";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "Rec";
                            }
                            break;
                        }
                    case "ClaimMandate":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "IncompleteClaimRegistration";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NewClaimRegistration";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "ClaimAdjustments";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "ClaimEnquiry";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "ClaimPaymentProcessing";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "ClaimRecoveryProcessing";
                            }
                            break;
                        }
                    case "ClaimRecoveryEditor":
                        {
                            if (!String.IsNullOrEmpty(accidentClaimIdNew) && accidentClaimIdNew != "0" && mode == "")
                            {
                                strSubMenu = "IncompleteClaimRegistration";
                            }
                            else if (mode == "")
                            {
                                strSubMenu = "NewClaimRegistration";
                            }
                            else if (mode == "Adj")
                            {
                                strSubMenu = "ClaimAdjustments";
                            }
                            else if (mode == "Enq")
                            {
                                strSubMenu = "ClaimEnquiry";
                            }
                            else if (mode == "Payment")
                            {
                                strSubMenu = "ClaimPaymentProcessing";
                            }
                            else if (mode == "Recovery")
                            {
                                strSubMenu = "ClaimRecoveryProcessing";
                            }
                            break;
                        }
                }
                return strSubMenu;
            }

            public static string GetClaimLabel(string menuid)
            {
                System.Resources.ResourceManager MenuResource = (new MCAS.Web.Objects.MastersHelper.TransactionModel()).GetResourceManager("MCAS.Web.Objects.Resources.Common.Menu");
                return MenuResource.GetString(menuid);
            }

            public static string BreadCrumbString(string caller, string accidentClaimIdNew, string mode, string screenName)
            {
                string strRet = "NewClaimRegistration";
                switch (screenName)
                {
                    case "ClaimAccident":
                        {
                            if ((caller == "Write" && accidentClaimIdNew != null && accidentClaimIdNew != "0") || (caller == "Write") || (caller == "Write" && mode == "Adj") || (caller == "Read" && mode == "Enq") || (caller == "Read" && mode == "Payment") || (caller == "Recovery" && mode == "Recovery")
                                )
                                strRet = "Accident";
                            break;
                        }
                }
                return strRet;
            }

            public static string PreBreadCrumbString(string mode)
            {
                string strRet = "ClaimsRegistration";
                if (mode == "Payment")
                    strRet = "ClaimPayment";
                else if (mode == "Recovery")
                    strRet = "ClaimsRecovery";
                return strRet;
            }
            public static string GetOrganizationType(string OrganizationID)
            {
                MCASEntities _db = new MCASEntities();
                var result = "";
                try
                {
                    var organizationID = Int32.Parse(OrganizationID);
                    result = (from vl in _db.MNT_OrgCountry where vl.Id == organizationID select vl.InsurerType).FirstOrDefault();
                }
                catch (Exception)
                {
                }
                finally
                {
                    _db.Dispose();
                }
                return result;
            }
            public static int? claimcompletestatus(string acc)
            {
                MCASEntities _db = new MCASEntities();
                int? result = null;
                try
                {
                    int accidentClaimId = Convert.ToInt32(acc);
                    result = accidentClaimId != 0 ? (from m in _db.ClaimAccidentDetails where m.AccidentClaimId == accidentClaimId select m.IsComplete).FirstOrDefault() : 0;
                }
                catch (Exception)
                {
                }
                finally
                {
                    _db.Dispose();
                }
                return result;
            }
            public static string GetMode(string p, string Getmode)
            {
                string result = Getmode;
                if (p == "206")
                {
                    result = "New";
                }
                else if (p == "208")
                {
                    result = "Adj";
                }
                else if (p == "209")
                {
                    result = "Enq";
                }
                else if (p == "211")
                {
                    result = "Payment";
                }
                else if (p == "215")
                {
                    result = "Recovery";
                }
                return result;
            }
            #endregion



            public static string Entry(string caller)
            {
                if (caller == "New")
                {
                    return ClaimRegistration.NewClaimEntry;
                }
                else
                {
                    return ClaimRegistration.NewClaimEnquiry;
                }
            }

            public static string GetColumnNameForAlertdashBoard(int sortColumn)
            {

                return sortColumn == 1 ? "ClaimNO" : sortColumn == 2 ? "IPNo" : sortColumn == 3 ? "ListType" : sortColumn == 4 ? "CreatedBy" : sortColumn == 5 ? "StartTime" : sortColumn == 6 ? "ToUserId" : sortColumn == 7 ? "ReAssignToId" : sortColumn == 8 ? "EscalationTo" : "ClaimNO";
            }

            public static string GetQueryStringVal()
            {
               return System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"].ToUpper() == "YES" ?  (HttpContext.Current.Request.QueryString["Q"] == null?"":RouteEncryptDecrypt.Decrypt(HttpContext.Current.Request.QueryString["Q"]).Replace('?', '&')) : HttpContext.Current.Request.Url.Query.Split('?')[1];

            }
        }
        #endregion

        #region "Properties"
        public int MenuId { get; set; }
        public string DisplayText { get; set; }
        public string Value { get; set; }
        public string LinkAddress { get; set; }
        public string ProductName { get; set; }
        public int DisplayOrder { get; set; }
        public string IsHeader { get; set; }
        public string IsMenu { get; set; }
        public string SubMenu { get; set; }
        public int MainHeaderId { get; set; }
        public string TabId { get; set; }
        public string SubTabId { get; set; }
        public string ImageURL { get; set; }
        public string Disabled { get; set; }
        public int LangId { get; set; }
        public string AdminDisplayText { get; set; }
        public string ErrorMessDesc { get; set; }
        public string ErrorMessTitle { get; set; }
        public string ErrorMessHead { get; set; }
        public string DisplayImage { get; set; }
        public string Group { get; set; }
        public string IsActive { get; set; }
        #endregion

        #region  "Public Methods"
        public static List<MenuListItem> Fetch(int LangId, string groupId) //System.Web.HttpContextBase httpContext )
        {
            // EnableDisableNodes = EnableDisableNodesListItem.Fetch(contact)
            List<MenuListItem> Menulist = new List<MenuListItem>();
            MCASEntities obj = new MCASEntities();
            //var model = new BaseModel();
            //var LoginId = model.CreatedBy;
            var gid = groupId;//(from l in obj.MNT_Users join k in obj.MNT_GroupsMaster on l.GroupId equals k.GroupId where l.UserId == LoginId select k.GroupId).FirstOrDefault();
            var Mnlist = obj.Proc_GetMenuList(LangId, Convert.ToInt32(gid)).ToList();

            foreach (var menu in Mnlist)
            {
                bool readcheck = (from l in obj.MNT_GroupPermission where l.GroupId == groupId && l.MenuId == menu.MenuId select l.Read).FirstOrDefault()== null? false: Convert.ToBoolean((from l in obj.MNT_GroupPermission where l.GroupId == groupId && l.MenuId == menu.MenuId select l.Read).FirstOrDefault());
                if (readcheck)
                {
                    MenuListItem mnItem = new MenuListItem();
                    mnItem.MenuId = menu.MenuId;
                    mnItem.DisplayText = menu.DisplayTitle.ToString();
                    mnItem.Value = menu.TId.ToString();
                    mnItem.DisplayImage = menu.Displayimg;
                    mnItem.LinkAddress = menu.Hyp_Link_Address;
                    mnItem.ProductName = menu.ProductName;
                    mnItem.DisplayOrder = (int)menu.DisplayOrder;
                    mnItem.IsHeader = menu.IsHeader;
                    mnItem.SubMenu = menu.SubMenu;
                    mnItem.MainHeaderId = (int)menu.MainHeaderId;
                    mnItem.TabId = menu.TabId;
                    mnItem.SubTabId = menu.SubTabId;
                    mnItem.IsMenu = menu.IsMenuItem;
                    mnItem.Disabled = "NO";
                    mnItem.LangId = Convert.ToInt16(menu.LangId);
                    mnItem.AdminDisplayText = menu.AdminDisplayText;
                    mnItem.ErrorMessDesc = menu.ErrorMessDesc;
                    mnItem.ErrorMessTitle = menu.ErrorMessTitle;
                    mnItem.ErrorMessHead = menu.ErrorMessHead;
                    mnItem.Group = menu.GroupType;
                    mnItem.IsActive = menu.IsActive;
                    Menulist.Add(mnItem);
                }
            }
            /*
            using (var obj = new MCASEntities())
            {
                var idParam = new SqlParameter
                {
                    ParameterName = "LangId",
                    Value = 1
                };
                //Get name of string type
                var menuList = obj.Database.SqlQuery<Course>("exec Proc_GetMenuList @LangId ", idParam).ToList<MenuListItem>();
                //Or can call SP by following way
                var menuList = obj.MNT_Menus.SqlQuery<MenuListItem>("exec Proc_GetMenuList @LangId ", idParam).ToList<MenuListItem>();
                foreach (menu cs in courseList)
                { }
            }        
            */
            obj.Dispose();
            return Menulist;
        }

        private Boolean DisplayScreen(String screenName)
        {
            Boolean Display = false;
            string subSystemCode = "UCW";
            /*ISysScreen = SystemSetup.Data.General.GetData.ISysScreens.FindBySubSystemCodeScreenName(subSystemCode, screenName);
            if(ISysScreen && ISysScreen.IsOptional)
            {
                Screen = SystemSetup.Data.General.GetData.ConfigOptionalScreens.FindByScreenId(ISysScreen.ScreenId);
                if( Screen && Screen.IsDisplayed )
                    Display = True;
                else
                Display = True;
            }*/
            return Display;
        }
        #endregion
    }
}
