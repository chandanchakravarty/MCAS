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
    public class TransactionModel : BaseModel
    {

        #region Properties
        private DateTime? _timeStamp = null;
        public int TranAuditId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }
        public string TranAuditIdString { get; set; }
        public string Description { get; set; }
        public string TimeStampString { get; set; }
        public int AccidentId { get; set; }
        public string TableName { get; set; }
        public string UserName { get; set; }
        public string Actions { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string NodeName { get; set; }
        public string ChangedColumns { get; set; }
        public string TansDescription { get; set; }
        public string CmpCode { get; set; }
        public string CmpDetails { get; set; }
        public string Selectcriteria { get; set; }
        public string Valuecriteria { get; set; }
        public List<SelectListItem> SelectcriteriaList { get; set; }
        public List<SelectListItem> ValueList { get; set; }
        public override string listscreenId
        {

            get
            {
                return "140";
            }

        }

        public override string screenId
        {
            get
            {
                return "140";
            }
        }

        #endregion
        # region static method
        public static List<TransactionModel> Fetchall(string AccidentClaimId)
        {
            string res = string.Empty;
            MCASEntities obj = new MCASEntities();
            var item = new List<TransactionModel>();
            var id = Convert.ToInt32(AccidentClaimId);
            var List = obj.Proc_GetTransactionAuditLog(id).ToList();
            if (List.Any())
            {
                foreach (var data in List)
                {
                    var desc = data.TableName;
                    var oper = data.Actions == "I" ? "Inserted" : data.Actions == "U" ? "Updated" : "Deleted";
                    var finaldesc = "Record has been " + oper + " in " + desc;
                    var filename = data.TableName != null ? Regex.Replace(data.TableName.ToUpper(), @"\s+", "") : "";
                    if (filename != "PRINTJOBS")
                    {
                        item.Add(new TransactionModel()
                        {
                            TranAuditId = data.TranAuditId,
                            TimeStamp = data.TimeStamp,
                            TableName = data.TableName,
                            UserName = data.UserName,
                            Actions = data.Actions,
                            OldData = data.OldData,
                            NewData = data.NewData,
                            ChangedColumns = data.ChangedColumns,
                            TansDescription = finaldesc,
                        });
                    }
                }
            }
            obj.Dispose();
            return item;
        }
        # endregion

        public static List<TransactionModel> Fetchallfrserviceprovider(string Cedantid, string Tablename, string Displayname)
        {
            string res = string.Empty;
            MCASEntities obj = new MCASEntities();
            var item = new List<TransactionModel>();
            var List = obj.Proc_GetTranAudLogSerProvider(Cedantid, Tablename).ToList();
            if (List.Any())
            {
                foreach (var data in List)
                {
                    Displayname = Displayname.Replace("_", " ");
                    var oper = data.Actions == "I" ? "inserted" : data.Actions == "U" ? "updated" : "deleted";
                    var finaldesc = "Record has been " + oper + " in " + Displayname;
                    item.Add(new TransactionModel()
                    {
                        TranAuditId = data.TranAuditId,
                        TimeStamp = data.TimeStamp,
                        TableName = Displayname,
                        UserName = data.UserName,
                        Actions = data.Actions,
                        OldData = data.OldData,
                        NewData = data.NewData,
                        ChangedColumns = data.ChangedColumns,
                        TansDescription = finaldesc,
                    });
                }
            }
            obj.Dispose();
            return item;
        }
        public System.Resources.ResourceManager GetResourceManager(string resFilewithNameSpace)
        {

            return new System.Resources.ResourceManager(ResourceFileExists(resFilewithNameSpace) ? resFilewithNameSpace : "MCAS.Web.Objects.Resources.Common.Common", System.Reflection.Assembly.GetExecutingAssembly());
        }

        public Boolean ResourceFileExists(string resourcePath)
        {
            List<string> resourceNames = new List<string>(Assembly.GetCallingAssembly().GetManifestResourceNames());
            resourcePath = resourceNames.FirstOrDefault(r => r.Contains(resourcePath));
            return resourcePath == null ? false : true;
        }


        public System.Resources.ResourceManager GetResourceManager(int ScreenId)
        {
            MCASEntities obj = new MCASEntities();
            var Results = (from mntMenu in obj.MNT_Menus where mntMenu.MenuId == ScreenId select mntMenu.VirtualSource).FirstOrDefault();
            obj.Dispose();
            System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("", System.Reflection.Assembly.GetExecutingAssembly());
            if (Results != null)
            {
                resManager = new System.Resources.ResourceManager(Results, System.Reflection.Assembly.GetExecutingAssembly());
            }

            return resManager;
        }
        public object GetTransactionHistory(int TranAuditId)
        {
            MCASEntities obj = new MCASEntities();

            var tableList = new List<CommonUtilities.CommonType>() 
                                { new CommonUtilities.CommonType() { Id = "CLM_MandateSummary", Text = "CLM_MandateDetails" },
                                  new CommonUtilities.CommonType() { Id = "CLM_PaymentSummary", Text = "CLM_PaymentDetails" }, 
                                  new CommonUtilities.CommonType() { Id = "CLM_ReserveSummary", Text = "CLM_ReserveDetails" } };

            var tableName = obj.MNT_TransactionAuditLog.Where(x => x.TranAuditId == TranAuditId).FirstOrDefault().TableName.ToString();
            MNT_Menus Results;
            if (tableList.Where(o => o.Id == tableName).Count() > 0)
            {
                var productName = tableList.Where(o => o.Id == tableName).FirstOrDefault().Text;
                Results = (from mntMenu in obj.MNT_Menus
                           join tranAudit in obj.MNT_TransactionAuditLog
                           on mntMenu.ProductName equals productName
                           where tranAudit.TranAuditId == TranAuditId
                           select mntMenu).FirstOrDefault();
            }
            else
            {
                Results = (from tranAudit in obj.MNT_TransactionAuditLog
                           join mntMenu in obj.MNT_Menus
                           on tranAudit.TableName equals mntMenu.ProductName
                           where tranAudit.TranAuditId == TranAuditId
                           select mntMenu).FirstOrDefault();

            }

            object tranHistory = null;
            if (Results != null)
            {
                var resManager = GetResourceManager(Results.VirtualSource);
                tranHistory = TransactionModel.GetTransactionHistory(TranAuditId.ToString(), resManager);
            }
            obj.Dispose();
            return tranHistory;
        }

        public static object GetTransactionHistory(string TranAuditId, System.Resources.ResourceManager resManager)
        {
            MCASEntities obj = new MCASEntities();
            int tranID = Int32.Parse(TranAuditId);
            var results = MapLabelValue(obj.Proc_GetXMLDiff(tranID).ToList(), resManager);
            obj.Dispose();
            return results;
        }
        private static List<TransactionModel> MapLabelValue(List<Proc_GetXMLDiff_Result1> result, System.Resources.ResourceManager resManager)
        {
            var item = new List<TransactionModel>();
            if (result.Any())
            {
                MCASEntities obj = new MCASEntities();
                foreach (var data in result)
                {
                    var columnName = data.NodeName;
                    var labelName = resManager.GetString(columnName);
                    var cmpDetails = "";
                    var Cmp_Code = "";
                    if (columnName == "CmpCode")
                    {
                        Cmp_Code = "CmpCode";
                        cmpDetails = resManager.GetString(data.OldVal == "" ? data.NewVal : "");
                    }
                    if (labelName != null || cmpDetails != "")
                    {
                        if (((columnName.ToLower() == "ModifiedBy".ToLower() || columnName.ToLower() == "ModifiedDate".ToLower()) && data.OldVal == "" && data.NewVal == "") || ((data.OldVal == data.NewVal)) && cmpDetails == "")
                        {
                        }
                        else
                        {
                            item.Add(new TransactionModel()
                            {
                                NodeName = labelName,
                                OldData = data.OldVal,
                                NewData = data.NewVal,
                                CmpCode = Cmp_Code,
                                CmpDetails = cmpDetails
                            });
                        }

                    }
                }
            }
            return item;
        }

        public static TransactionModel Fetchvalue(TransactionModel Tran)
        {
            Tran.SelectcriteriaList = GetvalueForSelectcriteria();
            Tran.ValueList = GetvalueForValue();
            return Tran;
        }

        private static List<SelectListItem> GetvalueForValue()
        {
            MCASEntities _db = new MCASEntities();
            List<SelectListItem> list = (from td in _db.MNT_TableDesc
                                         join t1 in _db.MNT_TransactionAuditLog on td.TableName equals t1.TableName
                                         select
                                             new SelectListItem()
                                             {
                                                 Text = td.TableDesc,
                                                 Value = td.TableDesc
                                             }).Distinct().ToList();

            list.Insert(0, new SelectListItem() { Text = "[Select...] ", Value = "" });
            _db.Dispose();
            return list;
        }

        private static List<SelectListItem> GetvalueForSelectcriteria()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "[Select...]", Value = "[Select...]" });
            list.Add(new SelectListItem() { Text = "Screen Name", Value = "ScreenName" });
            list.Add(new SelectListItem() { Text = "Transaction Description", Value = "Description" });
            list.Add(new SelectListItem() { Text = "User Name", Value = "UserName" });
            list.Add(new SelectListItem() { Text = "TimeStamp", Value = "TimeStamp" });
            return list;
        }

        public static List<SelectListItem> GetvalLsit(string cat)
        {
            MCASEntities _db = new MCASEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                if (cat == "ScreenName")
                {
                    list = (from td in _db.MNT_TableDesc
                            join t1 in _db.MNT_TransactionAuditLog on td.TableName equals t1.TableName
                            select
                                new SelectListItem()
                                {
                                    Text = td.TableDesc,
                                    Value = td.TableDesc
                                }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            list.Insert(0, new SelectListItem() { Text = "[Select...]", Value = "" });
            return list;
        }


        public static CommonUtilities.DatatableGrid TransactionEditorScreenList(string Selectcriteria, string Valuecriteria, int draw, int start, int length, int sortColumn, string sortDirection, string p, bool bol, string search)
        {
            MCASEntities _db = new MCASEntities();
            List<CommonUtilities.DatatableGrid> item = new List<CommonUtilities.DatatableGrid>();
            List<string[]> res = new List<string[]>();
            List<string[]> filterlist = new List<string[]>();
            List<TransactionModel> list = new List<TransactionModel>();
            List<TransactionModel> sortlist = new List<TransactionModel>();
            string[] columnname = { "TranAuditId", "TableName", "UserName", "TansDescription", "TimeStamp" };
            try
            {
                if (Valuecriteria == "" || !bol)
                { }
                else if (Selectcriteria == "ScreenName")
                {
                    list = (from l in _db.Proc_GetAllTransactionAuditLog().ToList()
                            select new TransactionModel()
                            {   TranAuditId =l.TranAuditId,
                                TranAuditIdString = l.TranAuditIdString,
                                TimeStamp =l.TimeStamp,
                                TimeStampString = l.TimeStampString,
                                TableName = l.TableName,
                                UserName = l.UserName,
                                TansDescription = l.TansDescription
                            }).Where(x => x.TableName == Valuecriteria).ToList();
                }
                else if (Selectcriteria == "Description")
                {
                    list = (from l in _db.Proc_GetAllTransactionAuditLog().ToList()
                            select new TransactionModel()
                            {
                                TranAuditId = l.TranAuditId,
                                TranAuditIdString = l.TranAuditIdString,
                                TimeStamp = l.TimeStamp,
                                TimeStampString = l.TimeStampString,
                                TableName = l.TableName,
                                UserName = l.UserName,
                                TansDescription = l.TansDescription
                            }).Where(x => x.TansDescription.ToUpper().Contains(Valuecriteria.ToUpper())).ToList();
                }
                else if (Selectcriteria == "UserName")
                {
                    list = (from l in _db.Proc_GetAllTransactionAuditLog().ToList()
                            select new TransactionModel()
                            {
                                TranAuditId = l.TranAuditId,
                                TranAuditIdString = l.TranAuditIdString,
                                TimeStamp = l.TimeStamp,
                                TimeStampString = l.TimeStampString,
                                TableName = l.TableName,
                                UserName = l.UserName,
                                TansDescription = l.TansDescription
                            }).Where(x => x.UserName.ToUpper().Contains(Valuecriteria.ToUpper())).ToList();
                }
                else if (Selectcriteria == "TimeStamp")
                {
                    list = (from l in _db.Proc_GetAllTransactionAuditLog().ToList()
                            select new TransactionModel()
                            {
                                TranAuditId = l.TranAuditId,
                                TranAuditIdString = l.TranAuditIdString,
                                TimeStamp = l.TimeStamp,
                                TimeStampString = l.TimeStampString,
                                TableName = l.TableName,
                                UserName = l.UserName,
                                TansDescription = l.TansDescription
                            }).Where(x => x.TimeStampString.ToUpper().Contains(Valuecriteria.ToUpper())).ToList();
                }


                var param = columnname[sortColumn];
                var propertyInfo = typeof(TransactionModel).GetProperty(param);
                sortlist = sortDirection == "asc" ? list.OrderBy(x => propertyInfo.GetValue(x, null)).ToList() : list.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();


                if (search != "")
                    sortlist = sortlist.Where(
                        bl => bl.TranAuditIdString.Contains(search) || bl.TableName.ToUpper().Contains(search.ToUpper()) || bl.UserName.ToUpper().Contains(search.ToUpper()) || bl.TansDescription.ToUpper().Contains(search.ToUpper()) || bl.TimeStampString.ToUpper().Contains(search.ToUpper())).ToList();

                res = sortlist.Select(cap => new string[]{
                                             cap.TranAuditIdString,
                                             cap.TableName,
                                             cap.UserName,
                                             cap.TansDescription,
                                             cap.TimeStampString,
                                             }
                                         ).ToList();



                if (res.Any())
                {
                    filterlist = length != -1 ? res.GetRange(start, Math.Min(length, res.Count - start)) : res;
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
    }
}

