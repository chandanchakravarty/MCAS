using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using System.IO;
using System.Web.Hosting;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using MCAS.Web.Objects.Resources.ClaimMasters;
using System.Data.Objects.SqlClient;
using MCAS.Web.Objects.ClaimObjectHelper;


namespace MCAS.Web.Objects.MastersHelper
{
    public class LossTypeModel : BaseModel
    {
        #region Properties
        public int? TranId { get; set; }

        public string LossTypeCode { get; set; }
        //[Required(ErrorMessage = "Loss Type Name is required.")]
        [Required(ErrorMessageResourceType = typeof(Create), ErrorMessageResourceName = "RFVLossTypeName")]
        public string LossTypeName { get; set; }
        public string LossTypeDescription { get; set; }
        //[Required(ErrorMessage = "Main Class is required.")]
        [Required(ErrorMessageResourceType = typeof(Create), ErrorMessageResourceName = "RFVMainClass")]
        public string ProductCode { get; set; }
        public string ProductDisplayName { get; set; }
        //[Required(ErrorMessage = "Sub Class is required.")]
        [Required(ErrorMessageResourceType = typeof(Create), ErrorMessageResourceName = "RFVSubClass")]
        public string SubClassCode { get; set; }

        public override string screenId
        {
            get
            {
                return "233";
            }
            set
            {
                base.screenId = "233";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "103";
            }
            set
            {
                base.listscreenId = "103";
            }
        }

        public List<ProductsListItems> ProductList { get; set; }
        public List<ProductsListItems> SubClassList { get; set; }
        #endregion



        #region Methods
        public LossTypeModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_LossType losstypeinfo;


            if (TranId.HasValue)
            {
                losstypeinfo = obj.MNT_LossType.Where(x => x.TranId == this.TranId).FirstOrDefault();

                losstypeinfo.LossTypeName = this.LossTypeName;
                losstypeinfo.ProductCode = this.ProductCode;
                losstypeinfo.SubClassCode = this.SubClassCode;
                losstypeinfo.ModifiedBy = this.ModifiedBy;
                losstypeinfo.ModifiedDate = DateTime.Now;
                obj.SaveChanges();
                this.LossTypeCode = losstypeinfo.LossTypeCode;
                this.CreatedBy = losstypeinfo.CreatedBy;
                this.CreatedOn = losstypeinfo.CreatedDate == null ? DateTime.MinValue : (DateTime)losstypeinfo.CreatedDate;
                this.ModifiedOn = losstypeinfo.ModifiedDate;
                return this;
            }
            else
            {
                losstypeinfo = new MNT_LossType();

                var maxlength = 5;
                var prefix = "LT";
                var countrows = (from row in obj.MNT_LossType select (int?)row.TranId).Max() ?? 0;
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var losstypecode = (prefix + result + currentno);

                losstypeinfo.LossTypeCode = losstypecode;
                losstypeinfo.LossTypeName = this.LossTypeName;
                losstypeinfo.LossTypeDescription = this.LossTypeDescription;
                losstypeinfo.ProductCode = this.ProductCode;
                losstypeinfo.SubClassCode = this.SubClassCode;
                losstypeinfo.CreatedBy = this.CreatedBy;
                losstypeinfo.CreatedDate = DateTime.Now;
                obj.MNT_LossType.AddObject(losstypeinfo);
                obj.SaveChanges();

                this.TranId = losstypeinfo.TranId;
                this.LossTypeCode = losstypeinfo.LossTypeCode;
                this.CreatedOn = (DateTime)losstypeinfo.CreatedDate;
                return this;
            }


        }
        #endregion

        public static List<LossTypeModel> GetLossTypeSearchResult(string MainClassCode, string SubClassCode, string LossTypeCode)
        {
            MCASEntities obj = new MCASEntities();
            List<LossTypeModel> searchResult = new List<LossTypeModel>();
            try
            {
                searchResult = (from l in obj.MNT_LossType
                                join p in obj.MNT_Products on l.ProductCode equals p.ProductCode
                                join s in obj.MNT_ProductClass on l.SubClassCode equals s.ClassCode

                                where
                                  l.ProductCode.Contains(MainClassCode) &&
                                  l.LossTypeCode.Contains(LossTypeCode) &&
                                  l.SubClassCode.Contains(SubClassCode)

                                select new LossTypeModel
                                {
                                    ProductCode = p.ProductCode,
                                    ProductDisplayName = p.ProductDisplayName,
                                    LossTypeCode = l.LossTypeCode,
                                    LossTypeName = l.LossTypeName,
                                    SubClassCode = s.ClassDesc,
                                    TranId = l.TranId
                                }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }

            return searchResult;
        }
    }
    public class ClaimCloseModel : BaseModel
    {
        #region Properties


        public int? Id { get; set; }

        public string CloseCode { get; set; }

        //[Required(ErrorMessage = "Claims Close Descrpition is required.")]
        [Required(ErrorMessageResourceType = typeof(ClaimCloseEditor), ErrorMessageResourceName = "RFVClaimsCloseDescription")]
        public string CloseDescrpition { get; set; }

        public override string screenId
        {
            get
            {
                return "243";
            }
            set
            {
                base.screenId = "243";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "111";
            }
            set
            {
                base.listscreenId = "111";
            }
        }

        #endregion

        #region "Public Shared Methods"

        public static List<ClaimCloseModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimCloseModel>();
            var ClaimClose = new List<ClaimCloseModel>();
            var claimlist = (from x in _db.MNT_ClaimClosed orderby x.CloseCode, x.CloseDesc select x);
            if (claimlist.Any())
            {
                foreach (var data in claimlist)
                {
                    item.Add(new ClaimCloseModel() { Id = data.Id, CloseCode = data.CloseCode, CloseDescrpition = data.CloseDesc, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }
        #endregion

        #region Methods


        public ClaimCloseModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_ClaimClosed close;
            if (Id.HasValue)
            {
                close = _db.MNT_ClaimClosed.Where(x => x.Id == this.Id).FirstOrDefault();
                close.CloseCode = this.CloseCode;
                close.CloseDesc = this.CloseDescrpition;
                close.ModifiedBy = this.ModifiedBy;
                close.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.CloseCode = close.CloseCode;
                this.CreatedBy = close.CreatedBy;
                this.CreatedOn = close.CreatedDate == null ? DateTime.MinValue : (DateTime)close.CreatedDate;
                this.ModifiedOn = close.ModifiedDate;
                this.ModifiedBy = ModifiedBy;
                return this;
            }
            else
            {
                close = new MNT_ClaimClosed();

                var maxlength = 5;
                var prefix = "L";
                //  var countrows = (from row in _db.MNT_ClaimClosed select row.Id).Max();
                var countrows = (from row in _db.MNT_ClaimClosed select (int?)row.Id).Max() ?? 0;
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var Closecode = (prefix + result + currentno);

                close.CloseCode = Closecode;
                close.CloseDesc = this.CloseDescrpition;
                close.CreatedBy = this.CreatedBy;
                close.CreatedDate = DateTime.Now;
                _db.MNT_ClaimClosed.AddObject(close);
                _db.SaveChanges();
                this.CloseCode = Closecode;
                this.Id = close.Id;
                this.CreatedOn = (DateTime)close.CreatedDate;
                return this;



            }


        }
        #endregion
    }


    public class ClaimExpenseModel : BaseModel
    {
        #region properties
        public int? Id { get; set; }
        public string ClaimExpenseCode { get; set; }
        //[Required(ErrorMessage = "Claims Expense Description is required.")]
        [Required(ErrorMessageResourceType = typeof(ClaimExpenseEditor), ErrorMessageResourceName = "RFVClaimExpenseDescription")]
        public string ClaimExpenseDesc { get; set; }

        public override string screenId
        {
            get
            {
                return "242";
            }
            set
            {
                base.screenId = "242";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "110";
            }
            set
            {
                base.listscreenId = "110";
            }
        }
        #endregion

        #region public shared methods
        public static List<ClaimExpenseModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimExpenseModel>();
            var claimExpense = (from x in _db.MNT_ClaimExpense orderby x.ClaimExpenseDesc select x);
            if (claimExpense.Any())
            {
                foreach (var data in claimExpense)
                {
                    item.Add(new ClaimExpenseModel() { Id = data.Id, ClaimExpenseCode = data.ClaimExpenseCode, ClaimExpenseDesc = data.ClaimExpenseDesc, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;

        }
        #endregion

        #region Methods
        public ClaimExpenseModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_ClaimExpense claimexpense;


            if (Id.HasValue)
            {
                claimexpense = obj.MNT_ClaimExpense.Where(x => x.Id == this.Id).FirstOrDefault();
                claimexpense.ClaimExpenseCode = this.ClaimExpenseCode;
                claimexpense.ClaimExpenseDesc = this.ClaimExpenseDesc;
                claimexpense.ModifiedBy = this.ModifiedBy;
                claimexpense.ModifiedDate = DateTime.Now;
                obj.SaveChanges();
                this.ClaimExpenseCode = claimexpense.ClaimExpenseCode;
                this.CreatedBy = claimexpense.CreatedBy;
                this.CreatedOn = claimexpense.CreatedDate == null ? DateTime.MinValue : (DateTime)claimexpense.CreatedDate;
                this.ModifiedOn = claimexpense.ModifiedDate;

                return this;
            }
            else
            {
                claimexpense = new MNT_ClaimExpense();

                var maxlength = 5;
                var prefix = "CE";
                //   var countrows = (from row in obj.MNT_ClaimExpense select row.Id).Max();
                var countrows = (from row in obj.MNT_ClaimExpense select (int?)row.Id).Max() ?? 0;
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var ClaimExpensecode = (prefix + result + currentno);

                claimexpense.ClaimExpenseCode = ClaimExpensecode;
                claimexpense.ClaimExpenseDesc = this.ClaimExpenseDesc;
                claimexpense.CreatedBy = this.CreatedBy;
                claimexpense.CreatedDate = DateTime.Now;


                obj.MNT_ClaimExpense.AddObject(claimexpense);
                obj.SaveChanges();

                this.Id = claimexpense.Id;
                this.ClaimExpenseCode = claimexpense.ClaimExpenseCode;
                this.CreatedOn = (DateTime)claimexpense.CreatedDate;
                return this;
            }

        }
        #endregion
    }

    public class ClaimReOpenModel : BaseModel
    {

        #region Properties
        public int? Id { get; set; }

        public string ReOpenCode { get; set; }

        [Required(ErrorMessage = "Claim Close Descrpition is required.")]
        public string ReOpenDescription { get; set; }

        #endregion

        #region public Shared Method
        public static List<ClaimReOpenModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimReOpenModel>();
            var claimlist = (from x in _db.MNT_ClaimReOpened select x);
            if (claimlist.Any())
            {
                foreach (var data in claimlist)
                {
                    item.Add(new ClaimReOpenModel() { Id = data.id, ReOpenCode = data.ReopenCode, ReOpenDescription = data.ReopenDesc, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }

        #endregion

        #region public Methods

        public ClaimReOpenModel Update()
        {

            MCASEntities _db = new MCASEntities();
            MNT_ClaimReOpened Reopen;
            if (Id.HasValue)
            {
                Reopen = _db.MNT_ClaimReOpened.Where(x => x.id == this.Id).FirstOrDefault();
                //  Reopen.ReopenCode = this.ReOpenCode;
                Reopen.ReopenDesc = this.ReOpenDescription;
                Reopen.ModifiedBy = Convert.ToString(this.ModifiedBy);
                Reopen.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                return this;
            }
            else
            {
                Reopen = new MNT_ClaimReOpened();

                var maxlength = 5;
                var prefix = "R00";
                var countrows = (from row in _db.MNT_ClaimReOpened select row.id).Max();
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var Closecode = (prefix + result + currentno);

                Reopen.ReopenCode = Closecode;
                Reopen.ReopenDesc = this.ReOpenDescription;
                Reopen.CreatedBy = Convert.ToString(this.CreatedBy);
                Reopen.CreatedDate = DateTime.Now;
                _db.MNT_ClaimReOpened.AddObject(Reopen);
                _db.SaveChanges();
                this.ReOpenCode = Reopen.ReopenCode;
                this.Id = Reopen.id;
                return this;


            }

        }


        #endregion
    }

    public class CurrencyExchangeModel : BaseModel
    {
        #region properties
        public int? Id { get; set; }

        private DateTime? _effectiveDate = null;
        private DateTime? _expiryDate = null;
        [Required(ErrorMessageResourceType = typeof(ExchangeEditor), ErrorMessageResourceName = "RFVCurrencyCode")]
        public string CurrencyCode { get; set; }

        public string CurrencyName { get; set; }
        [Display(Name = "Exchange Rate")]
        [Required(ErrorMessageResourceType = typeof(ExchangeEditor), ErrorMessageResourceName = "RFVExchangeRate")]
        public decimal? Exchangerate { get; set; }

        [Display(Name = "Currency Effective Date")]
        [Required(ErrorMessageResourceType = typeof(ExchangeEditor), ErrorMessageResourceName = "RFVCurrencyEffectiveDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EffectiveDate
        {
            get { return _effectiveDate; }
            set { _effectiveDate = value; }
        }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }

        public List<CurrencyMasterModel> currencylist { get; set; }

        public override string screenId
        {
            get
            {
                return "245";
            }
            set
            {
                base.screenId = "245";
            }
        }

        public override string listscreenId
        {
            get
            {
                return "113";
            }
            set
            {
                base.listscreenId = "113";
            }
        }

        #endregion

        #region Public Shared Method

        public static List<CurrencyExchangeModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<CurrencyExchangeModel>();
            //  var Exchange = new List<CurrencyExchangeModel>();
            var Exclist = (from x in _db.MNT_CurrencyTxn orderby x.CurrencyCode select x);
            if (Exclist.Any())
            {
                foreach (var data in Exclist)
                {
                    var currname = (from p in _db.MNT_CurrencyM where p.CurrencyCode == data.CurrencyCode select p.Description).FirstOrDefault();
                    item.Add(new CurrencyExchangeModel() { Id = data.Id_CurrencyTrans, CurrencyCode = data.CurrencyCode, CurrencyName = currname, Exchangerate = data.ExchangeRate, EffectiveDate = Convert.ToDateTime(data.EffDate), ExpiryDate = Convert.ToDateTime(data.ExpDate), CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now });
                }
            }
            return item;
        }
        #endregion
        #region methods

        public CurrencyExchangeModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_CurrencyTxn curr = new MNT_CurrencyTxn();
            DateTime today = DateTime.Today;
            DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);

            DateTime lastday = DateTime.Today;
            DateTime endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            if (Id.HasValue)
            {
                curr = _db.MNT_CurrencyTxn.Where(x => x.Id_CurrencyTrans == this.Id).FirstOrDefault();
                //   curr.CurrencyCode = this.CurrencyCode;
                //curr.EffDate = this.EffectiveDate;
                curr.EffDate = Convert.ToDateTime(this.EffectiveDate);
                curr.ExchangeRate = Convert.ToDecimal(this.Exchangerate);
                curr.ExpDate = endOfMonth;
                curr.ModifiedBy = this.ModifiedBy;
                curr.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.CreatedBy = curr.CreatedBy;
                this.CreatedOn = curr.CreatedDate == null ? DateTime.MinValue : (DateTime)curr.CreatedDate;
                this.ModifiedOn = curr.ModifiedDate;
                this.ModifiedBy = ModifiedBy;
                return this;
            }
            else
            {

                curr.CurrencyCode = this.CurrencyCode;
                curr.EffDate = Convert.ToDateTime(this.EffectiveDate);
                curr.ExchangeRate = Convert.ToDecimal(this.Exchangerate);
                curr.ExpDate = endOfMonth;
                curr.CreatedBy = this.CreatedBy;
                curr.CreatedDate = DateTime.Now;
                _db.MNT_CurrencyTxn.AddObject(curr);
                _db.SaveChanges();
                this.CreatedOn = (DateTime)curr.CreatedDate;
                this.CreatedBy = curr.CreatedBy;
                return this;

            }

        }

        #endregion
    }

    public class GSTModel : BaseModel
    {
        #region properties
        private DateTime? _EffectiveDateFrom = null;
        private DateTime? _EffectiveDateTo = null;


        public int? Id { get; set; }
        public string GSTCode { get; set; }
        //[Required(ErrorMessage = "GST type is required.")]
        [Required(ErrorMessageResourceType = typeof(GSTEditor), ErrorMessageResourceName = "RFVGSTType")]
        public string GSTType { get; set; }
        //[Required(ErrorMessage = "GST description is required.")]
        [Required(ErrorMessageResourceType = typeof(GSTEditor), ErrorMessageResourceName = "RFVGSTDescription")]
        public string GSTDescrpition { get; set; }

        [DisplayName("Effective Date From")]
        //[Required(ErrorMessage = "Effective Date From is required.")]
        [Required(ErrorMessageResourceType = typeof(GSTEditor), ErrorMessageResourceName = "RFVEffectiveDateFrom")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveDateFrom
        {
            get { return _EffectiveDateFrom; }
            set { _EffectiveDateFrom = value; }
        }

        [DisplayName("Effective Date To")]
        //[Required(ErrorMessage = "Effective Date To is required.")]
        [Required(ErrorMessageResourceType = typeof(GSTEditor), ErrorMessageResourceName = "RFVEffectiveDateTo")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveDateTo
        {
            get { return _EffectiveDateTo; }
            set { _EffectiveDateTo = value; }
        }

        //[Required(ErrorMessage = "Rate is required.")]
        [Required(ErrorMessageResourceType = typeof(GSTEditor), ErrorMessageResourceName = "RFVRate")]
        public decimal? Rate { get; set; }

        public override string screenId
        {
            get
            {
                return "251";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "124";
            }

        }
        #endregion

        #region shared method
        public static List<GSTModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<GSTModel>();
            var Gst = (from x in _db.MNT_GST orderby x.GSTDesc select x);
            if (Gst.Any())
            {
                foreach (var data in Gst)
                {
                    item.Add(new GSTModel() { Id = data.Id, GSTCode = data.GSTCode, GSTType = data.GSTType, GSTDescrpition = data.GSTDesc, EffectiveDateFrom = Convert.ToDateTime(data.EffDateFrom), EffectiveDateTo = Convert.ToDateTime(data.EffDateTo), Rate = data.Rate });
                }
            }
            return item;
        }



        #endregion

        #region Public Methods
        public GSTModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_GST gst = new MNT_GST();

            if (Id.HasValue)
            {
                gst = _db.MNT_GST.Where(x => x.Id == this.Id).FirstOrDefault();
                gst.GSTType = this.GSTType;
                gst.GSTDesc = this.GSTDescrpition;
                gst.EffDateFrom = Convert.ToDateTime(this.EffectiveDateFrom);
                gst.EffDateTo = Convert.ToDateTime(this.EffectiveDateTo);
                gst.Rate = Convert.ToDecimal(this.Rate);
                gst.ModifiedBy = this.ModifiedBy;
                gst.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.GSTCode = gst.GSTCode;
                this.CreatedBy = gst.CreatedBy;
                this.CreatedOn = gst.CreatedDate == null ? DateTime.MinValue : (DateTime)gst.CreatedDate;
                this.ModifiedOn = gst.ModifiedDate;
                return this;
            }
            else
            {

                var maxlength = 5;
                var prefix = "GST";
                //   var countrows = (from row in _db.MNT_GST select row.Id).Max();
                var countrows = (from row in _db.MNT_GST select (int?)row.Id).Max() ?? 0;
                string currentno = (countrows + 1).ToString();

                string result = new String('0', (maxlength - (currentno.Length + prefix.Length)));

                var Gstcode = (prefix + result + currentno);
                gst.GSTCode = Gstcode;
                gst.GSTType = this.GSTType;
                gst.GSTDesc = this.GSTDescrpition;
                gst.EffDateFrom = Convert.ToDateTime(this.EffectiveDateFrom);
                gst.EffDateTo = Convert.ToDateTime(this.EffectiveDateTo);
                gst.Rate = Convert.ToDecimal(this.Rate);
                gst.CreatedBy = this.CreatedBy;
                gst.CreatedDate = DateTime.Now;
                _db.MNT_GST.AddObject(gst);
                _db.SaveChanges();
                this.Id = gst.Id;
                this.GSTCode = gst.GSTCode;
                this.CreatedOn = (DateTime)gst.CreatedDate;
                return this;


            }

        }
        #endregion
    }

    public class PreViewDocumentModel : BaseModel
    {
        #region properties
        public int Template_Id { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string Display_Name { get; set; }
        public string Is_Header { get; set; }
        public int? Carrier_Id { get; set; }
        public int? Lob_Id { get; set; }
        public int? parentId { get; set; }
        public string Template_Path { get; set; }
        public bool Is_System_Template { get; set; }
        public int Template_Format_Id { get; set; }
        public bool Is_Active { get; set; }
        public string MappingXML_Path { get; set; }
        public string MappingXML_FileName { get; set; }
        public string Template_Code { get; set; }
        public bool Has_Dynamic_Data { get; set; }
        public bool Has_Condition { get; set; }
        public bool Has_Dynamic_Footer { get; set; }
        public string Has_Footer_Desc { get; set; }
        public bool Has_Dynamic_Header_Footer { get; set; }
        public int ScreenId { get; set; }
        public string Hvarchar { get; set; }
        public int DocumentId { get; set; }
        public int Claimid { get; set; }
        public int ChildId { get; set; }
        public int? SubChildId { get; set; }
        public int AccidentClaimId { get; set; }
        public string ChildDescription { get; set; }
        public string ChildIHeader { get; set; }
        public string SubChildHeader { get; set; }
        public int? PartyToShownP { get; set; }
        public int? PartyToShownC { get; set; }
        public int? PartyToShownS { get; set; }
        public string SubChildDescription { get; set; }
        public string DocumentName { get; set; }
        public string Templatepath { get; set; }
        public int? PRINT_JOBS_ID { get; set; }
        public string Templatecode { get; set; }
        public int Id { get; set; }
        public int unquieid { get; set; }
        public string Text { get; set; }
        public int? uniquesubchildid { get; set; }
        public string uniquesubchilddescription { get; set; }
        public string LevelHide1 { get; set; }
        public string SId { get; set; }
        public List<CommonUtilities.CommonType> Hospital { get; set; }
        #endregion

        #region shared methods

        public static List<PreViewDocumentModel> Fetch(string ClaimID, string AccidentClaimId, string ScreenId)
        {
            MCASEntities _db = new MCASEntities();
            int cid = Convert.ToInt32(ClaimID);
            int aid = Convert.ToInt32(AccidentClaimId);
            int sid = Convert.ToInt32(ScreenId);
            List<PreViewDocumentModel> item = new List<PreViewDocumentModel>();
            try
            {
                item = (from x in _db.Proc_GetTemplateList(sid).ToList()
                        select new PreViewDocumentModel()
                        {
                            Id = Convert.ToInt32(x.Id),
                            Template_Id = x.Template_Id,
                            Description = x.Description,
                            Filename = x.Filename,
                            parentId = x.parentId,
                            Is_Header = x.Is_Header,
                            Display_Name = x.Display_Name,
                            ScreenId = sid,
                            Claimid = cid,
                            AccidentClaimId = aid,
                            ChildId = Convert.ToInt32(x.ChildId),
                            SubChildId = x.SubChildId,
                            SubChildDescription = x.SubChildDescription,
                            ChildDescription = x.ChildDescription,
                            ChildIHeader = x.ChildIHeader,
                            SubChildHeader = x.SubChildHeader,
                            PartyToShownP = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.parentId select l.PartyToShown).FirstOrDefault(),
                            PartyToShownC = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.PartyToShown).FirstOrDefault(),
                            PartyToShownS = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.SubChildId select l.PartyToShown).FirstOrDefault()
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return item;
        }

        public static List<PreViewDocumentModel> FetchPdf()
        {
            List<PreViewDocumentModel> lstFiles = new List<PreViewDocumentModel>();
            DirectoryInfo dirInfo = new DirectoryInfo(HostingEnvironment.MapPath("../../Uploads/Templates/C Stage/"));

            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {

                lstFiles.Add(new PreViewDocumentModel() { DocumentId = i + 1, DocumentName = item.Name, Templatepath = dirInfo.FullName + @"\" + item.Name });
                i = i + 1;
            }

            return lstFiles;
        }

        public PreViewDocumentModel Update()
        {
            MCASEntities _db = new MCASEntities();
            PRINT_JOBS print = new PRINT_JOBS();
            var model = new PreViewDocumentModel();
            string CreatedBy1 = ((MCAS.Web.Objects.CommonHelper.BaseModel)(model)).CreatedBy;
            if (PRINT_JOBS_ID.HasValue)
            {
                print = _db.PRINT_JOBS.Where(x => x.PRINT_JOBS_ID == this.DocumentId).FirstOrDefault();
                print.CLAIM_ID = 116;
                print.DOCUMENT_CODE = "C";
                print.FILE_NAME = this.DocumentName;
                _db.SaveChanges();
                return this;

            }
            else
            {
                var countrows = (from row in _db.PRINT_JOBS select (int?)row.PRINT_JOBS_ID).Max() ?? 0;
                string currentno = (countrows + 1).ToString();
                print.CLAIM_ID = this.Claimid;
                print.ENTITY_ID = this.DocumentId;
                print.DOCUMENT_CODE = Templatecode;
                print.IS_ACTIVE = "1";
                print.IS_PROCESSED = Convert.ToBoolean(1);
                //print.FILE_NAME = (this.DocumentName + "_" + print.CLAIM_ID + "_" + "C");
                print.FILE_NAME = this.DocumentName;
                //print.FILE_NAME = this.DocumentName + ".pdf";
                print.URL_PATH = "/Uploads/OutputPDFs/" + print.CLAIM_ID + "/" + print.DOCUMENT_CODE;
                print.CREATED_DATETIME = DateTime.Now;
                print.CreatedBy = CreatedBy1;
                _db.PRINT_JOBS.AddObject(print);
                _db.SaveChanges();
                this.PRINT_JOBS_ID = print.PRINT_JOBS_ID;
                return this;


            }
        }


        #endregion

        public static List<PreViewDocumentModel> FetchResultList(List<PreViewDocumentModel> list, List<GenerateDoumet> list1, string ClaimID, string AccidentClaimId, string ScreenId, string Sid = "")
        {
            MCASEntities _db = new MCASEntities();
            List<GenerateDoumet> list2 = new List<GenerateDoumet>();
            List<PreViewDocumentModel> resultlist = new List<PreViewDocumentModel>();
            int pid = (ScreenId == "139" && !string.IsNullOrEmpty(Sid)) ? Convert.ToInt32(Sid) : 0;
            string payeedid = (ScreenId == "139" && !string.IsNullOrEmpty(Sid)) ? (from l in _db.CLM_PaymentSummary where l.PaymentId == pid select l.Payee).FirstOrDefault().Contains('-') ? (from l in _db.CLM_PaymentSummary where l.PaymentId == pid select l.Payee).FirstOrDefault().Split('-')[1] : Convert.ToString((from l in _db.CLM_PaymentSummary where l.PaymentId == pid select l.ClaimID).FirstOrDefault()) : "0";
            list1 = ScreenId == "139" ? (from l in list1 where l.Id == payeedid select l).ToList() : list1;
            List<GenerateDoumet> list3 = list1;
            try
            {
                foreach (var x in list)
                {
                    list1 = (ScreenId == "131" && (x.ChildId == 31 || x.ChildId == 32)) ? (list3.Count > 0 ? list3.Where((value, index) => index == 0).ToList() : list3) : list3;

                    int? id = x.SubChildId != null ? x.PartyToShownS : x.PartyToShownC;
                    if (id == null)
                    {
                        if (list1.Count == 0)
                        {
                            resultlist.Add(new PreViewDocumentModel()
                            {
                                Id = 0,
                                unquieid = 0,
                                Text = null,
                                Template_Id = x.Template_Id,
                                Description = x.Description,
                                Filename = x.Filename,
                                parentId = x.parentId,
                                Is_Header = x.Is_Header,
                                Display_Name = x.Display_Name,
                                ScreenId = Convert.ToInt32(ScreenId),
                                Claimid = Convert.ToInt32(ClaimID),
                                AccidentClaimId = Convert.ToInt32(AccidentClaimId),
                                ChildId = x.ChildId,
                                SubChildId = null,
                                SubChildDescription = "",
                                ChildDescription = x.ChildDescription,
                                ChildIHeader = "Y",
                                SubChildHeader = "Y",
                                uniquesubchildid = null,
                                uniquesubchilddescription = "",
                                LevelHide1 = "Y",
                                SId = Sid,
                                Hospital = GetActiveHospitalList()
                            });
                        }
                        else
                        {
                            foreach (var key in list1)
                            {
                                resultlist.Add(new PreViewDocumentModel()
                                {
                                    Id = x.SubChildDescription == null ? Convert.ToInt32(key.Id) : 0,
                                    unquieid = Convert.ToInt32(key.Id),
                                    Text = x.SubChildDescription == null ? null : key.Text,
                                    Template_Id = x.Template_Id,
                                    Description = x.Description,
                                    Filename = x.Filename,
                                    parentId = x.parentId,
                                    Is_Header = x.Is_Header,
                                    Display_Name = x.Display_Name,
                                    ScreenId = Convert.ToInt32(ScreenId),
                                    Claimid = Convert.ToInt32(ClaimID),
                                    AccidentClaimId = Convert.ToInt32(AccidentClaimId),
                                    ChildId = x.ChildId,
                                    SubChildId = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.SubChildId select l.HasPartyType).FirstOrDefault() == "Y" ? x.SubChildId : null,
                                    SubChildDescription = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.SubChildId select l.HasPartyType).FirstOrDefault() == "Y" ? x.SubChildDescription : "",
                                    ChildDescription = x.ChildDescription,
                                    ChildIHeader = ((from l in _db.MNT_TEMPLATE_MASTER where l.ParentId == x.ChildId select l).FirstOrDefault() != null || (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y") ? "Y" : "N",
                                    SubChildHeader = ((from l in _db.MNT_TEMPLATE_MASTER where l.ParentId == x.SubChildId select l).FirstOrDefault() != null || (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.SubChildId select l.HasPartyType).FirstOrDefault() == "Y") ? "Y" : "N",
                                    uniquesubchildid = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y" ? (int?)Convert.ToInt32(Convert.ToString(x.parentId) + Convert.ToString(x.ChildId) + Convert.ToString(x.SubChildId == null ? null : x.SubChildId)) : null,
                                    uniquesubchilddescription = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y" ? x.SubChildDescription == null ? key.Text : x.SubChildDescription : "",
                                    LevelHide1 = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y" ? "Y" : "N",
                                    SId = Sid,
                                    Hospital = GetActiveHospitalList()
                                });
                            }
                        }
                    }
                    else
                    {
                        list2 = (from l in list1 where l.PartyId == id select l).ToList();
                        if (list2.Count == 0)
                        {
                            resultlist.Add(new PreViewDocumentModel()
                            {
                                Id = 0,
                                unquieid = 0,
                                Text = null,
                                Template_Id = x.Template_Id,
                                Description = x.Description,
                                Filename = x.Filename,
                                parentId = x.parentId,
                                Is_Header = x.Is_Header,
                                Display_Name = x.Display_Name,
                                ScreenId = Convert.ToInt32(ScreenId),
                                Claimid = Convert.ToInt32(ClaimID),
                                AccidentClaimId = Convert.ToInt32(AccidentClaimId),
                                ChildId = x.ChildId,
                                SubChildId = null,
                                SubChildDescription = "",
                                ChildDescription = x.ChildDescription,
                                ChildIHeader = "Y",
                                SubChildHeader = "Y",
                                uniquesubchildid = null,
                                uniquesubchilddescription = "",
                                LevelHide1 = "Y",
                                SId = Sid,
                                Hospital = GetActiveHospitalList()
                            });
                        }
                        else
                        {
                            foreach (var key in list2)
                            {
                                resultlist.Add(new PreViewDocumentModel()
                                {
                                    Id = x.SubChildDescription == null ? Convert.ToInt32(key.Id) : 0,
                                    unquieid = Convert.ToInt32(key.Id),
                                    Text = x.SubChildDescription == null ? null : key.Text,
                                    Template_Id = x.Template_Id,
                                    Description = x.Description,
                                    Filename = x.Filename,
                                    parentId = x.parentId,
                                    Is_Header = x.Is_Header,
                                    Display_Name = x.Display_Name,
                                    ScreenId = Convert.ToInt32(ScreenId),
                                    Claimid = Convert.ToInt32(ClaimID),
                                    AccidentClaimId = Convert.ToInt32(AccidentClaimId),
                                    ChildId = x.ChildId,
                                    SubChildId = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.SubChildId select l.HasPartyType).FirstOrDefault() == "Y" ? x.SubChildId : null,
                                    SubChildDescription = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.SubChildId select l.HasPartyType).FirstOrDefault() == "Y" ? x.SubChildDescription : "",
                                    ChildDescription = x.ChildDescription,
                                    ChildIHeader = ((from l in _db.MNT_TEMPLATE_MASTER where l.ParentId == x.ChildId select l).FirstOrDefault() != null || (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y") ? "Y" : "N",
                                    SubChildHeader = ((from l in _db.MNT_TEMPLATE_MASTER where l.ParentId == x.SubChildId select l).FirstOrDefault() != null || (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.SubChildId select l.HasPartyType).FirstOrDefault() == "Y") ? "Y" : "N",
                                    uniquesubchildid = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y" ? (int?)Convert.ToInt32(Convert.ToString(x.parentId) + Convert.ToString(x.ChildId) + Convert.ToString(x.SubChildId == null ? null : x.SubChildId)) : null,
                                    uniquesubchilddescription = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y" ? x.SubChildDescription == null ? key.Text : x.SubChildDescription : "",
                                    LevelHide1 = (from l in _db.MNT_TEMPLATE_MASTER where l.Id == x.ChildId select l.HasPartyType).FirstOrDefault() == "Y" ? "Y" : "N",
                                    SId = Sid,
                                    Hospital = GetActiveHospitalList()
                                });
                            }
                        }
                    }

 }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return resultlist;
        }

        private static List<CommonUtilities.CommonType> GetActiveHospitalList()
        {
            MCASEntities _db = new MCASEntities();
            List<CommonUtilities.CommonType> result = new List<CommonUtilities.CommonType>();
            try
            {
                result=(from l in _db.MNT_Hospital
                        where l.Status == "1" orderby l.HospitalName ascending
                        select new CommonUtilities.CommonType()
                        {
                            intID = l.Id,
                            Text = l.HospitalName
                        }).ToList();
                result.Insert(0, new CommonUtilities.CommonType() {
                    intID = 0,
                    Text = "[Select...]"
                });
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

      

        private static List<GenerateDoumet> FetchHospitalList(string AccidentClaimId, string ClaimID, List<GenerateDoumet> list3)
        {
            MCASEntities _db = new MCASEntities();
            int acc = Convert.ToInt32(AccidentClaimId);
            int cid = Convert.ToInt32(ClaimID);
            List<GenerateDoumet> result;

            try
            {
                result = (from l in _db.CLM_LogRequest
                        where l.AccidentClaimId == acc && l.ClaimID == cid
                        select new
                            GenerateDoumet()
                        {
                            PolicyId = "0",
                            Claimid = ClaimID,
                            AccidentClaimId = AccidentClaimId,
                            Id = SqlFunctions.StringConvert((double)l.Hospital_Id).Trim(),
                            Text = (from h in _db.MNT_Hospital where h.Id == l.Hospital_Id select h.HospitalName).FirstOrDefault() ?? "",
                            PartyId = null
                        }).Distinct().ToList();
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

    public class GenerateDoumet : BaseModel
    {
        #region properties
        public string PolicyId { get; set; }
        public string Claimid { get; set; }
        public string AccidentClaimId { get; set; }
        public string ScreenId { get; set; }
        public string Id { get; set; }
        public string TempletdID { get; set; }
        public string Text
        {
            get;
            set;
        }
        public int? PartyId
        {
            get;
            set;
        }
        #endregion


        #region Methods
        public static List<GenerateDoumet> FetchServiceProviderList(string ClaimID, string AccidentClaimId)
        {
            MCASEntities obj = new MCASEntities();
            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            List<GenerateDoumet> Result = new List<GenerateDoumet>();
            try
            {
                var ServiceProviderNamelist = obj.Proc_GetCLM_ServiceProviderNameList(AccidentClaimId, ClaimID).ToList();
                int i = 0;
                foreach (var l in ServiceProviderNamelist)
                {
                    if (!string.IsNullOrEmpty(l.ClaimID) && !string.IsNullOrEmpty(l.ClaimantName) && i == 0)
                    {
                        i = i + 1;
                        list.Add(new CommonUtilities.CommonType()
                        {
                            Id = l.ClaimID.Split('-')[1],
                            Text = l.ClaimantName,
                            PartyId = null
                        });
                    }
                    if (!string.IsNullOrEmpty(l.ServiceProviderId) && !string.IsNullOrEmpty(l.cedent_name))
                    {
                        int sid = Convert.ToInt32(l.ServiceProviderId.Split('-')[1]);
                        list.Add(new CommonUtilities.CommonType()
                        {

                            Id = l.ServiceProviderId.Split('-')[1],
                            Text = l.cedent_name,
                            PartyId = (int?)(from x in obj.CLM_ServiceProvider where x.ServiceProviderId == sid select x.PartyTypeId).FirstOrDefault() ?? null
                        });
                    }
                };
                Result = (from l in list
                          select new
                              GenerateDoumet()
                          {
                              PolicyId = "0",
                              Claimid = ClaimID,
                              AccidentClaimId = AccidentClaimId,
                              Id = l.Id,
                              Text = l.Text,
                              PartyId = l.PartyId
                          }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }
            return Result;
        }
        public static List<GenerateDoumet> Fetch(string ClaimID, string AccidentClaimId, string ScreenId, string Tid, string Task)
        {
            MCASEntities obj = new MCASEntities();
            List<CommonUtilities.CommonType> list = new List<CommonUtilities.CommonType>();
            List<GenerateDoumet> Result = new List<GenerateDoumet>();
            try
            {
                var ServiceProviderNamelist = obj.Proc_GetCLM_ServiceProviderNameList(AccidentClaimId, ClaimID).ToList();
                int i = 0;
                foreach (var l in ServiceProviderNamelist)
                {
                    if (!string.IsNullOrEmpty(l.ClaimID) && !string.IsNullOrEmpty(l.ClaimantName) && i == 0)
                    {
                        i = i + 1;
                        list.Add(new CommonUtilities.CommonType()
                        {
                            Id = l.ClaimID.Split('-')[1],
                            Text = l.ClaimantName
                        });
                    }
                    if (!string.IsNullOrEmpty(l.ServiceProviderId) && !string.IsNullOrEmpty(l.cedent_name))
                    {

                        list.Add(new CommonUtilities.CommonType()
                        {
                            Id = l.ServiceProviderId.Split('-')[1],
                            Text = l.cedent_name //CultureInfo.CurrentCulture.TextInfo.ToTitleCase(l.cedent_name.Trim())
                        });
                    }

                };
                Result = (from l in list
                          select new
                              GenerateDoumet()
                          {
                              PolicyId = "0",
                              Claimid = ClaimID,
                              AccidentClaimId = AccidentClaimId,
                              ScreenId = ScreenId,
                              Id = l.Id,
                              Text = l.Text,
                              TempletdID = Tid

                          }).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Dispose();
            }
            return Result;
        }
        #endregion


    }


    public class ClaimDocumentPrintedModel : BaseModel
    {

        #region Properties
        public int? PrintJobId { get; set; }

        private DateTime? _dateofgeneration = null;
        private DateTime? _timeofgeneration = null;
        public string ClaimNo { get; set; }
        public string VehicleNo { get; set; }
        public string DocumentName { get; set; }
        //public DateTime DateofGeneration { get; set; }
        public DateTime? TimeofGeneration { get; set; }

        public DateTime? DateofGeneration
        {
            get { return _dateofgeneration; }
            set { _dateofgeneration = value; }
        }

        //public DateTime? TimeofGeneration {
        //    get { return _timeofgeneration; }
        //    set { _timeofgeneration = value; }
        //}
        public string UserId { get; set; }
        public string ViewDocumentLink { get; set; }
        public int? DocumentID { get; set; }
        public int? Claimid { get; set; }


        #endregion


        #region methods
        public static List<ClaimDocumentPrintedModel> Fetch()
        {

            MCASEntities _db = new MCASEntities();
            var item = new List<ClaimDocumentPrintedModel>();
            var Print = (from x in _db.PRINT_JOBS orderby x.FILE_NAME select x);
            if (Print.Any())
            {
                foreach (var data in Print)
                {
                    // string str = Path.GetFileName(data.FILE_NAME);
                    var claimNo = (from p in _db.PRINT_JOBS
                                   join clm in _db.CLM_Claims on p.CLAIM_ID equals clm.ClaimID
                                   join ca in _db.ClaimAccidentDetails on clm.AccidentClaimId equals ca.AccidentClaimId
                                   where clm.ClaimID == (data.CLAIM_ID)
                                   select ca.ClaimNo).FirstOrDefault();
                    item.Add(new ClaimDocumentPrintedModel() { PrintJobId = data.PRINT_JOBS_ID, DocumentID = data.ENTITY_ID, ClaimNo = claimNo, DocumentName = Path.GetFileName(data.FILE_NAME), DateofGeneration = data.CREATED_DATETIME, TimeofGeneration = data.CREATED_DATETIME, UserId = Convert.ToString(data.GENERATED_FROM), ViewDocumentLink = data.FILE_NAME });
                }
            }
            return item;
        }

        #endregion
    }

    public class HospitalModel : BaseModel
    {
        MCASEntities _db = new MCASEntities();
        public HospitalModel()
        {
            HospitalAddres = new AddressModel();
        }

        #region properties

        private AddressModel _address = new AddressModel();
        public AddressModel HospitalAddres
        {
            get { return _address; }
            set { _address = value; }
        }
        public int? Id { get; set; }


        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string HospitalContactNo { get; set; }
        public string HospitalFaxNo { get; set; }
        public string ContactPersonName { get; set; }
        public string Email { get; set; }
        public string OfficeNo { get; set; }
        public string faxNo { get; set; }


        public override string listscreenId
        {
            get
            {
                return "128";
            }
            set
            {
                base.listscreenId = "128";
            }
        }

        public override string screenId
        {
            get
            {
                return "220";
            }
            set
            {
                base.screenId = "220";
            }
        }

        #endregion
        #region public shared Methods
        public static List<HospitalModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var model = new HospitalModel();
            var item = new List<HospitalModel>();
            var Hospitallist = (from x in _db.MNT_Hospital orderby x.HospitalName select x);
            if (Hospitallist.Any())
            {
                foreach (var data in Hospitallist)
                {
                    item.Add(new HospitalModel() { Id = data.Id, HospitalName = data.HospitalName, HospitalAddress = data.HospitalAddress, HospitalContactNo = data.officeNo, Email = data.Email, ContactPersonName = data.ContactPersonName });
                }
            }
            return item;
        }
        #endregion

        #region Methods
        public HospitalModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_Hospital hospital;
            try
            {
                if (Id.HasValue)
                {
                    hospital = _db.MNT_Hospital.Where(x => x.Id == this.Id).FirstOrDefault();

                    hospital.HospitalName = this.HospitalAddres.InsurerName;
                    hospital.HospitalAddress = this.HospitalAddres.Address1;
                    hospital.HospitalAddress2 = this.HospitalAddres.Address2;
                    hospital.HospitalAddress3 = this.HospitalAddres.Address3;
                    hospital.HospitalContactNo = "1234";
                    hospital.City = this.HospitalAddres.City;
                    hospital.State = this.HospitalAddres.State;
                    hospital.Country = this.HospitalAddres.Country;
                    hospital.PostalCode = this.HospitalAddres.PostalCode;
                    hospital.FirstContactPersonName = this.HospitalAddres.FirstContactPersonName;
                    hospital.Email = this.HospitalAddres.EmailAddress1;
                    hospital.officeNo = this.HospitalAddres.OffNo1;
                    hospital.MobileNo1 = this.HospitalAddres.MobileNo1;
                    hospital.FaxNo = this.HospitalAddres.Fax1;
                    hospital.SecondContactPersonName = this.HospitalAddres.SecondContactPersonName;
                    hospital.EmailAddress2 = this.HospitalAddres.EmailAddress2;
                    hospital.OffNo2 = this.HospitalAddres.OffNo2;
                    hospital.MobileNo2 = this.HospitalAddres.MobileNo2;
                    hospital.Fax2 = this.HospitalAddres.Fax2;
                    hospital.HospitalType = this.HospitalAddres.InsurerType;
                    hospital.Status = this.HospitalAddres.Status;
                    hospital.EffectiveFrom = this.HospitalAddres.EffectiveFromDate;
                    hospital.EffectiveTo = this.HospitalAddres.Effectiveto;
                    hospital.Remarks = this.HospitalAddres.Remarks;

                    hospital.HospitalType = this.HospitalAddres.InsurerType.ToString();
                    hospital.Status = this.HospitalAddres.Status == "Active" ? "1" : "0";
                    hospital.ModifiedBy = this.ModifiedBy;
                    hospital.ModifiedDate = DateTime.Now;
                    _db.SaveChanges();
                    this.CreatedBy = hospital.CreatedBy;
                    this.CreatedOn = hospital.CreatedDate == null ? DateTime.MinValue : (DateTime)hospital.CreatedDate;
                    this.ModifiedOn = DateTime.Now;
                    //this.CloseCode = close.CloseCode;
                    return this;
                }
                else
                {
                    hospital = new MNT_Hospital();
                    hospital.HospitalName = this.HospitalAddres.InsurerName;
                    hospital.HospitalAddress = this.HospitalAddres.Address1;
                    hospital.HospitalAddress2 = this.HospitalAddres.Address2;
                    hospital.HospitalAddress3 = this.HospitalAddres.Address3;
                    hospital.HospitalContactNo = "1234";
                    hospital.City = this.HospitalAddres.City;
                    hospital.State = this.HospitalAddres.State;
                    hospital.Country = this.HospitalAddres.Country;
                    hospital.PostalCode = this.HospitalAddres.PostalCode;
                    hospital.FirstContactPersonName = this.HospitalAddres.FirstContactPersonName;
                    hospital.Email = this.HospitalAddres.EmailAddress1;
                    hospital.officeNo = this.HospitalAddres.OffNo1;
                    hospital.MobileNo1 = this.HospitalAddres.MobileNo1;
                    hospital.FaxNo = this.HospitalAddres.Fax1;
                    hospital.SecondContactPersonName = this.HospitalAddres.SecondContactPersonName;
                    hospital.EmailAddress2 = this.HospitalAddres.EmailAddress2;
                    hospital.OffNo2 = this.HospitalAddres.OffNo2;
                    hospital.MobileNo2 = this.HospitalAddres.MobileNo2;
                    hospital.Fax2 = this.HospitalAddres.Fax2;
                    hospital.HospitalType = this.HospitalAddres.InsurerType;
                    hospital.Status = this.HospitalAddres.Status;
                    hospital.EffectiveFrom = this.HospitalAddres.EffectiveFromDate;
                    hospital.EffectiveTo = this.HospitalAddres.Effectiveto;
                    hospital.Remarks = this.HospitalAddres.Remarks;
                    hospital.HospitalType = this.HospitalAddres.InsurerType.ToString();
                    if (this.HospitalAddres.Status == "Active")
                    {
                        hospital.Status = "1";
                    }
                    else
                    {
                        hospital.Status = "0";
                    }
                    hospital.CreatedBy = this.CreatedBy;
                    hospital.CreatedDate = DateTime.Now;
                    _db.MNT_Hospital.AddObject(hospital);
                    _db.SaveChanges();
                    this.CreatedOn = (DateTime)hospital.CreatedDate;
                    var ecode = Convert.ToString(hospital.Id);
                    int list = (from l in _db.MNT_TransactionAuditLog.Where(x => x.UserName == hospital.CreatedBy && x.Actions == "I" && x.TableName == "MNT_Hospital") orderby l.TimeStamp select l.TranAuditId).ToList().LastOrDefault();
                    var c = _db.MNT_TransactionAuditLog.Where(x => x.TranAuditId == list).FirstOrDefault();
                    c.EntityCode = ecode;
                    _db.SaveChanges();
                    this.Id = hospital.Id;
                    return this;


                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }

        }
        #endregion
    }

    public class InterChangeModel : BaseModel
    {
        #region properties
        private DateTime? _EffectiveDateFrom = null;
        private DateTime? _EffectiveDateTo = null;
        public int? Id { get; set; }
        [Required(ErrorMessage = "Interchange Name is required.")]
        public string InterchangeName { get; set; }
        [Required(ErrorMessage = "Address 1 is required.")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Postal Code is required.")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }
        public string Remarks { get; set; }
        [DisplayName("Effective From")]
        [Required(ErrorMessage = "Effective From is required.")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveFrom
        {
            get { return _EffectiveDateFrom; }
            set { _EffectiveDateFrom = value; }
        }
        [DisplayName("Effective To")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveTo
        {
            get { return _EffectiveDateTo; }
            set { _EffectiveDateTo = value; }
        }

        public override string screenId
        {
            get
            {
                return "278";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "277";
            }

        }
        public List<UserCountryListItems> usercountrylist { get; set; }
        #endregion

        #region Static Methods
        public static List<InterChangeModel> FetchInterChangeDetails()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<InterChangeModel>();
            var InterchangeList = (from e in _db.MNT_InterChange
                                   join d in _db.MNT_Country on e.Country equals d.CountryShortCode
                                   select new { e.Id, e.InterchangeName, e.EffectiveFrom, e.EffectiveTo, e.Status, d.CountryName }).ToList();
            if (InterchangeList.Any())
            {
                foreach (var data in InterchangeList)
                {
                    item.Add(new InterChangeModel()
                    {
                        Id = data.Id,
                        InterchangeName = data.InterchangeName,
                        EffectiveFrom = data.EffectiveFrom,
                        EffectiveTo = data.EffectiveTo,
                        Status = data.Status,
                        Country = data.CountryName
                    });
                }
            }
            _db.Dispose();
            return item;
        }

        #endregion

        #region Methods
        public InterChangeModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_InterChange interchange;
            if (Id.HasValue)
            {
                interchange = obj.MNT_InterChange.Where(x => x.Id == this.Id.Value).FirstOrDefault();
                interchange.ModifiedDate = DateTime.Now;
                var cBy = interchange.CreatedBy.ToString();
                interchange.ModifiedBy = this.ModifiedBy;
                DataMapper.Map(this, interchange, true);
                interchange.CreatedBy = cBy;
                obj.SaveChanges();
                this.Id = interchange.Id;
                this.CreatedBy = interchange.CreatedBy;
                this.CreatedOn = interchange.CreatedDate == null ? DateTime.MinValue : (DateTime)interchange.CreatedDate;
                this.ModifiedOn = interchange.ModifiedDate;
                return this;
            }
            else
            {
                interchange = new MNT_InterChange();
                interchange.CreatedBy = HttpContext.Current.Session["LoggedInUserName"].ToString();
                interchange.CreatedDate = DateTime.Now;
                DataMapper.Map(this, interchange, true);
                obj.MNT_InterChange.AddObject(interchange);
                obj.SaveChanges();
                this.Id = interchange.Id;
                this.CreatedOn = (DateTime)interchange.CreatedDate;
                return this;
            }
        }

        #endregion
    }

    public class DeductibleModel : BaseModel
    {
        #region Properties
        private DateTime? _EffectiveDateFrom = null;
        private DateTime? _NewEffectiveDateFrom = null;
        private DateTime? _EffectiveDateTo = null;
        private DateTime? _NewEffectiveDateTo = null;
        private DateTime? _ModifiedDate = null;
        private DateTime? _CreatedDate = null;
        public int _prop2 { get; set; }
        public int Prop2
        {
            get
            {
                return 0;
            }
            set
            {
                this._prop2 = 0;
            }
        }
        private string _prop1;

        public string Prop1
        {
            get
            {
                return "0.00";
            }
            set
            {
                this._prop1 = "0.00";
            }
        }

        [DisplayName("Effective From")]
        [Required(ErrorMessage = "Effective From is required.")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveFrom
        {
            get { return _EffectiveDateFrom; }
            set { _EffectiveDateFrom = value; }
        }

        [DisplayName("Effective From")]
        [DataType(DataType.Date)]
        public DateTime? NewEffectiveFrom
        {
            get { return _NewEffectiveDateFrom; }
            set { _NewEffectiveDateFrom = value; }
        }

        [DisplayName("Effective To")]
        [Required(ErrorMessage = "Effective To is required.")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveTo
        {
            get { return _EffectiveDateTo; }
            set { _EffectiveDateTo = value; }
        }

        [DisplayName("Effective To")]
        [DataType(DataType.Date)]
        public DateTime? NewEffectiveTo
        {
            get { return _NewEffectiveDateTo; }
            set { _NewEffectiveDateTo = value; }
        }


        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public DateTime? ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }


        public override string screenId
        {
            get
            {
                return "280";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "279";
            }

        }
        public int? DeductibleId { get; set; }
        [NotEqual("Prop2", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.Masters.Deductible), ErrorMessageResourceName = "OrgCategory")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.Masters.Deductible), ErrorMessageResourceName = "OrgCategory")]
        //[Required(ErrorMessage = "Organization Category is required.")]
        public string OrgCategory { get; set; }
        public string OrgCategoryName { get; set; }
        [NotEqual("Prop1", ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.Masters.Deductible), ErrorMessageResourceName = "RFVDedutibleAmt")]
        [DisplayName("Deductible Amount")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.Masters.Deductible), ErrorMessageResourceName = "RFVDedutibleAmt")]
        public decimal? DeductibleAmt { get; set; }
        public string HResultData { get; set; }
        public string Remarks { get; set; }
        public string Title { get; set; }

        public decimal? NewDeductibleAmt { get; set; }




        public List<LookUpListItems> Categorylist { get; set; }

        public List<LookUpListItems> OrgCategoryList { get; set; }
        public string Hselect { get; set; }
        public string Hgetval { get; set; }
        public string HgetCatName { get; set; }
        public string HLatestDeducatbleid { get; set; }

        public List<OrgCountryListItems> usercategorylist { get; set; }

        #endregion

        #region Methods
        public DeductibleModel Update()
        {
            MCASEntities obj = new MCASEntities();
            MNT_Deductible deduct;
            if (DeductibleId.HasValue)
            {
                deduct = obj.MNT_Deductible.Where(x => x.DeductibleId == this.DeductibleId.Value).FirstOrDefault();
                var cDt = deduct.CreatedDate;
                var cBy = deduct.CreatedBy;
                DataMapper.Map(this, deduct, true);
                deduct.ModifiedBy = HttpContext.Current.Session["LoggedInUserName"].ToString();
                deduct.ModifiedDate = DateTime.Now;
                deduct.CreatedDate = cDt;
                deduct.CreatedBy = cBy;
                obj.SaveChanges();
                this.DeductibleId = deduct.DeductibleId;
                this.CreatedBy = deduct.CreatedBy;
                this.CreatedOn = deduct.CreatedDate == null ? DateTime.MinValue : (DateTime)deduct.CreatedDate;
                this.ModifiedOn = deduct.ModifiedDate;
                this.ModifiedBy = deduct.ModifiedBy;
                return this;
            }
            else
            {
                deduct = new MNT_Deductible();
                DataMapper.Map(this, deduct, true);
                deduct.CreatedDate = DateTime.Now;
                deduct.CreatedBy = HttpContext.Current.Session["LoggedInUserName"].ToString();
                obj.MNT_Deductible.AddObject(deduct);
                obj.SaveChanges();
                this.DeductibleId = deduct.DeductibleId;
                this.CreatedOn = DateTime.Now;
                this.CreatedBy = deduct.CreatedBy;
                var remarks = "Initially created.";
                obj.Proc_updateEntityCodeforDeductible(this.DeductibleId, obj.tranAuditTrailList[0].TranAuditId, remarks);
                obj.Dispose();
                return this;
            }
        }

        #endregion

        #region Static Methods



        public static string FetchOrgCategoryName()
        {
            MCASEntities obj = new MCASEntities();
            List<orgCategory1> sgf = new List<orgCategory1>();

            var innerJoinQuery =
    from deduct in obj.MNT_Deductible
    join org in obj.MNT_OrgCountry on deduct.OrgCategoryName equals org.CountryOrgazinationCode
    select new { OrganizationName = org.OrganizationName, OrganizationCode = deduct.OrgCategoryName };



            //    var query =
            //from product in obj.MNT_Deductible
            //where product.OrgCategoryName.Contains(userid)
            //select new
            //{
            //    oCode = product.OrgCode,
            //    OrganizationName = product.OrgName
            //};
            StringBuilder sb = new StringBuilder();
            foreach (var countryInfo in innerJoinQuery)
            {
                //sb.Append(countryInfo.OrganizationCode);
                //sb.Append("~");
                sb.Append(countryInfo.OrganizationName);
                sb.Append("~");
            }
            return sb.ToString();
        }



        public static string FetchOrgCategoryName(string p)
        {

            MCASEntities obj = new MCASEntities();
            List<orgCategory1> sgf = new List<orgCategory1>();

            var innerJoinQuery =
    from deduct in obj.MNT_Deductible
    join org in obj.MNT_OrgCountry on deduct.OrgCategoryName equals org.CountryOrgazinationCode
    select new { OrganizationName = org.OrganizationName, OrganizationCode = deduct.OrgCategoryName };



            //    var query =
            //from product in obj.MNT_Deductible
            //where product.OrgCategoryName.Contains(userid)
            //select new
            //{
            //    oCode = product.OrgCode,
            //    OrganizationName = product.OrgName
            //};
            string sb = "";
            foreach (var countryInfo in innerJoinQuery)
            {
                sb = countryInfo.OrganizationName;
            }
            return sb.ToString();

        }


        public static List<DeductibleModel> FetchDeductibleDetails(string orgcategory, string orgcategoryname)
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<DeductibleModel>();
            var DeductibleList = _db.Proc_GetMNT_OrgCategoryNameList(orgcategory, orgcategoryname).ToList();

            item = (from data in DeductibleList
                    select new DeductibleModel()
                    {
                        DeductibleId = data.DeductibleId,
                        OrgCategory = data.OrgCategory,
                        OrgCategoryName = data.OrganizationName,
                        DeductibleAmt = data.DeductibleAmt,
                        EffectiveFrom = data.EffectiveFrom,
                        EffectiveTo = data.EffectiveTo
                    }
                            ).ToList();

            _db.Dispose();
            return item;
        }
        public static bool intersects(DateTime r1start, DateTime r1end, DateTime r2start, DateTime r2end)
        {
            return (r1start == r2start) || (r1start > r2start ? r1start <= r2end : r2start <= r1end);

        }

        public static string DateCheck(DateTime P_Effectivefrom, DateTime P_EffectiveTo, DateTime N_Effectivefrom, DateTime? N_EffectiveTo, int? DeductibleId = 0)
        {
            if (DateTime.Equals(P_Effectivefrom, N_Effectivefrom) && DateTime.Equals(P_EffectiveTo, N_EffectiveTo) && (DeductibleId == null && DeductibleId == 0))
            {
                return "Effective To and Effective From  date overlapping with already save record";
            }
            else if (Between(N_EffectiveTo, P_Effectivefrom, P_EffectiveTo))
            {
                return "Effective To date overlapping with already save record";
            }
            else if (Between(N_Effectivefrom, P_Effectivefrom, P_EffectiveTo))
            {
                return "Effective From date overlapping with already save record";
            }
            else
            {
                return "T";
            }

        }
        public static bool Between(DateTime? input, DateTime date1, DateTime date2)
        {
            return (input > date1 && input < date2);
        }
        #endregion



        public static string GetDeductableAmountTopId()
        {
            MCASEntities db = new MCASEntities();
            var GridButtonText = string.Join("~", (from l in db.Proc_GetDeductableAmountTopId() select l.DeductibleId).ToArray());
            db.Dispose();
            return (GridButtonText == null) ? "" : GridButtonText;
        }

        public static void SaveRecord(string cat, string catName, decimal DedAmt, DateTime EffFrom, string Effto)
        {
            string LoggedInUserId = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
            MCASEntities db = new MCASEntities();
            MNT_Deductible dect = new MNT_Deductible();
            dect.OrgCategory = cat;
            dect.OrgCategoryName = catName;
            dect.DeductibleAmt = DedAmt;
            dect.EffectiveFrom = EffFrom;
            if (Effto != "")
            {
                dect.EffectiveTo = Convert.ToDateTime(Effto);
            }
            dect.CreatedBy = LoggedInUserId;
            dect.CreatedDate = DateTime.Now;
            db.MNT_Deductible.AddObject(dect);
            db.SaveChanges();
            var remarks = "Expired. New Effective From And Effective To Date and Deductible Amount.";
            db.Proc_updateEntityCodeforDeductible(dect.DeductibleId, db.tranAuditTrailList[0].TranAuditId, remarks);
            db.Dispose();
        }

        public DeductibleModel UpdateRecord(string cat, string catName, decimal DedAmt, DateTime EffFrom, string Effto, int deduct)
        {
            string LoggedInUserId = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
            MCASEntities db = new MCASEntities();
            MNT_Deductible dect = new MNT_Deductible();
            MNT_Deductible savedect = new MNT_Deductible();
            savedect = (from x in db.MNT_Deductible where x.DeductibleId == deduct select x).FirstOrDefault();
            dect.OrgCategory = savedect.OrgCategory;
            dect.OrgCategoryName = savedect.OrgCategoryName;
            dect.DeductibleAmt = DedAmt;
            dect.EffectiveFrom = EffFrom;
            if (Effto != "")
            {
                dect.EffectiveTo = Convert.ToDateTime(Effto);
            }
            else
            {
                dect.EffectiveTo = null;
            }
            dect.CreatedBy = savedect.CreatedBy;
            dect.CreatedDate = savedect.CreatedDate;
            dect.ModifiedBy = LoggedInUserId;
            dect.ModifiedDate = DateTime.Now;
            db.MNT_Deductible.AddObject(dect);
            db.SaveChanges();
            this.DeductibleId = dect.DeductibleId;
            this.CreatedBy = dect.CreatedBy;
            this.CreatedOn = dect.CreatedDate == null ? DateTime.MinValue : (DateTime)dect.CreatedDate;
            this.ModifiedOn = dect.ModifiedDate;
            this.ModifiedBy = dect.ModifiedBy;
            var remarks = "Expired. New Effective From And Effective To Date and Deductible Amount.";
            db.Proc_updateEntityCodeforDeductible(dect.DeductibleId, db.tranAuditTrailList[0].TranAuditId, remarks);
            db.Dispose();
            return this;
        }

        public static List<orgCategory1> GetlistOrgName(string cat, string pageMode)
        {
            MCASEntities db = new MCASEntities();
            List<orgCategory1> sgf = new List<orgCategory1>();
            if (cat == "" || cat == null)
            {
                sgf = null;
            }
            else
            {
                //var list = (from l in db.MNT_Deductible where l.EffectiveFrom <= DateTime.Now && (l.EffectiveTo == null) select new { l.OrgCategory, l.OrgCategoryName }).Union((from l in db.MNT_Deductible where l.EffectiveFrom <= DateTime.Now && l.EffectiveTo >= DateTime.Now select new { l.OrgCategory, l.OrgCategoryName }));
                //var query = from product in db.MNT_OrgCountry
                //            where product.InsurerType.Contains(cat)
                //            select new
                //            {
                //                CountryOrgCode = product.CountryOrgazinationCode,
                //                OrganizationName = product.OrganizationName
                //            };
                //foreach (var countryInfo in query)
                //{
                //    if (pageMode == "Addnew")
                //    {
                //        var Dectoremove = (from dec in list where dec.OrgCategoryName == countryInfo.CountryOrgCode select dec).FirstOrDefault();

                //        if (Dectoremove == null)
                //        {
                //            sgf.Add(new orgCategory1()
                //            {
                //                orgName = countryInfo.OrganizationName,
                //                orgCode = countryInfo.CountryOrgCode

                //            });
                //        }
                //    }
                //    else
                //    {
                //        sgf.Add(new orgCategory1()
                //        {
                //            orgName = countryInfo.OrganizationName,
                //            orgCode = countryInfo.CountryOrgCode

                //        });
                //    }
                //}

                if (pageMode == "Addnew")
                {
                    sgf = (from product in db.MNT_OrgCountry
                           where product.InsurerType.Contains(cat)
                           && !db.MNT_Deductible.Any(dd => dd.OrgCategoryName == product.CountryOrgazinationCode)
                           select new orgCategory1()
                           {
                               orgCode = product.CountryOrgazinationCode,
                               orgName = product.OrganizationName
                           }).OrderBy(t => t.orgName).ToList();
                }
                else
                {
                    sgf = (from product in db.MNT_OrgCountry
                           where product.InsurerType.Contains(cat)
                           select new orgCategory1()
                           {
                               orgCode = product.CountryOrgazinationCode,
                               orgName = product.OrganizationName
                           }).OrderBy(t => t.orgName).ToList();
                }


                sgf.Insert(0, new orgCategory1() { orgCode = "", orgName = "[Select...]" });
            }
            db.Dispose();
            return sgf;
        }
        public static List<DeductibleModel> GetOrgResult(string orgCategory11, string categoryname)
        {
            var searchResult = Enumerable.Empty<DeductibleModel>().AsQueryable();
            MCASEntities _db = new MCASEntities();
            var item = new List<DeductibleModel>();
            try
            {
                var DeductibleList = _db.Proc_GetMNT_OrgCategoryNameList(orgCategory11, categoryname).ToList();
                searchResult = (from data in DeductibleList
                                select new DeductibleModel()
                                {
                                    DeductibleId = data.DeductibleId,
                                    OrgCategory = data.OrgCategory,
                                    OrgCategoryName = data.OrganizationName,
                                    DeductibleAmt = data.DeductibleAmt,
                                    EffectiveFrom = data.EffectiveFrom,
                                    EffectiveTo = data.EffectiveTo
                                }
                                ).AsQueryable();


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                _db.Dispose();
            }
            return searchResult.ToList();
        }


        public static List<DeductibleModel> Fetchalldeductiblelist(string Cedantid, string Tablename)
        {
            string res = string.Empty;
            MCASEntities obj = new MCASEntities();
            var item = new List<DeductibleModel>();
            var List = obj.Proc_GetDeductibleHistory(Cedantid, Tablename).ToList();
            if (List.Any())
            {
                foreach (var data in List)
                {
                    //var oper = data.Actions == "I" ? "inserted" : data.Actions == "U" ? "updated" : "deleted";
                    //var finaldesc = "Record has been " + oper + " in " + Displayname;
                    item.Add(new DeductibleModel()
                    {
                        EffectiveFrom = data.EffectiveFrom,
                        EffectiveTo = data.EffectiveTo,
                        DeductibleAmt = data.DeductibleAmt,
                        CreatedBy = data.CreatedBy,
                        Remarks = data.Remarks,
                    });
                }
            }
            obj.Dispose();
            return item;
        }

    }

    public class LogRequestDocumentModel : BaseModel
    {
        #region properties
        [Required(ErrorMessage = "Document Type is required.")]
        public int DocumentId { get; set; }
        public string Description { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string Templatepath { get; set; }
        public string Templatecode { get; set; }

        public string XmlPath { get; set; }
        public string XmlFileName { get; set; }

        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public int? PRINT_JOBS_ID { get; set; }
        public int Claimid { get; set; }
        public List<CommonUtilities.CommonType> DocumentTypeList { get; set; }

        #endregion

        public static List<LogRequestDocumentModel> Fetch()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<LogRequestDocumentModel>();
            var Preview = (from t in _db.MNT_TEMPLATE_MASTER where t.Filename.Contains("LOG_with_cancellation_clause_for_previous_LOG.pdf") || t.Filename.Contains("LOG.pdf") select t).ToList();
            if (Preview.Any())
            {
                foreach (var data in Preview)
                {
                    item.Add(new LogRequestDocumentModel() { DocumentId = data.Template_Id, Description = data.Description, FileName = data.Filename, Templatepath = data.Template_Path, Templatecode = data.Template_Code, DocumentName = data.Display_Name });
                }
            }
            return item;
        }

        //public List<CommonUtilities.CommonType> FetchDocumentType()
        //{
        //    return FetchDocumentTypeList();
        //}
        //public static List<CommonUtilities.CommonType> FetchDocumentTypeList()
        //{
        //    MCASEntities _db = new MCASEntities();
        //    var items = new List<CommonUtilities.CommonType>();
        //    var list = (from t in _db.MNT_TEMPLATE_MASTER where t.Filename.Contains("LOG_with_cancellation_clause_for_previous_LOG.pdf") || t.Filename.Contains("LOG.pdf") select t).ToList();
        //    items = (from n in list
        //             select new CommonUtilities.CommonType()
        //             {
        //                 intID = n.Template_Id,
        //                 Text = n.Display_Name
        //             }).ToList();
        //    items.Insert(0, new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
        //    _db.Dispose();
        //    return items;
        //}

    }


    public class CommonMasterModel : BaseModel
    {

        #region  Properties
        public decimal? LookupID { get; set; }
        [Required(ErrorMessage = "Value Code is required.")]
        public string Lookupvalue { get; set; }
        [Required(ErrorMessage = "Value Description is required.")]
        public string Lookupdesc { get; set; }
        public string Description { get; set; }
        public string lookupCode { get; set; }
        public string Category { get; set; }
        public string IsActive { get; set; }
        [Required(ErrorMessage = "Master Description is required.")]
        public string LookUpCategory { get; set; }
        public string Status { get; set; }
        public List<LookUpListItems> Statuslist { get; set; }

        public string LookUpCategoryCode { get; set; }
        public String LookUpCategoryDesc { get; set; }
        public string idlist { get; set; }

        public override string screenId
        {
            get
            {
                return "298";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "297";
            }

        }

        public List<LookUpListItems> LookUpCategorylist { get; set; }
        public List<CommonMasterModel> CommonMasterCategoryList { get; set; }
        [Required(ErrorMessage = "Organisation is required.")]
        public string OrgCategory { get; set; }
        public List<orgCategory> OrgCatTypelist { get; set; }
        #endregion

        #region static methods
        public static List<CommonMasterModel> FetchCommonMasterDetails()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<CommonMasterModel>();
            var AllmenulistInt = (from x in obj.MNT_Lookups where x.IsActive == "Y" select x.Category).ToList();
            var strCategory = (from x in obj.MNT_LookupsMaster where x.IsActive == "Y" select x.LookupCategoryCode).ToList();
            var intersect = AllmenulistInt.Intersect(strCategory);

            var CommonMasterList = (from x in obj.MNT_Lookups orderby x.Description where intersect.Contains(x.Category) select x);
            if (intersect.Any())
            {
                foreach (var data in CommonMasterList)
                {
                    item.Add(new CommonMasterModel()
                    {
                        LookupID = data.LookupID,
                        Lookupvalue = data.Lookupvalue,
                        Lookupdesc = data.Lookupdesc,
                        Description = data.Description,
                        Category = data.Category,
                        IsActive = data.IsActive,
                        LookUpCategoryDesc = (from y in obj.MNT_LookupsMaster where y.IsActive == "Y" && y.LookupCategoryCode == data.Category select y.LookupCategoryDesc).FirstOrDefault()

                    });
                }

            }

            return item;




        }

        public static List<CommonMasterModel> FetchCommonMasterCategoryList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<CommonMasterModel>();
            try
            {
                item = (from x in _db.MNT_LookupsMaster
                        where x.IsActive == "Y"
                        select new
                        {
                            x.LookupCategoryCode,
                            x.LookupCategoryDesc
                        }).Distinct().ToList().Select(b => new CommonMasterModel()
                                        {
                                            LookUpCategoryCode = b.LookupCategoryCode,
                                            LookUpCategoryDesc = b.LookupCategoryDesc
                                        }).ToList();
                item.Insert(0, new CommonMasterModel() { LookUpCategoryCode = "0", LookUpCategoryDesc = "[Select...]" });
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            _db.Dispose();
            return item;
        }


        public static List<orgCategory> fetchOrganizationList(string UserId, string Caller, string OrgType)
        {
            MCASEntities obj = new MCASEntities();
            List<orgCategory> list = new List<orgCategory>();
            list = (from l in obj.Proc_GetOrganizationListAccident(UserId, Caller, OrgType)
                    select new orgCategory()
                    {
                        OrgType = l.OrgType,
                        Description = l.Description.Trim()
                    }
                        ).ToList();
            //list.Insert(0, new orgCategory() { OrgType = 0, Description = "[Select...]" });
            obj.Dispose();
            return list;
        }
        #endregion

        #region Methods

        public CommonMasterModel Update()
        {
            MCASEntities _db = new MCASEntities();
            MNT_Lookups lookups;
            if (LookupID.HasValue)
            {
                lookups = _db.MNT_Lookups.Where(x => x.LookupID == this.LookupID).FirstOrDefault();
                lookups.Category = this.LookUpCategory;
                lookups.Lookupdesc = this.Lookupdesc;
                lookups.Description = this.Lookupdesc;
                lookups.Lookupvalue = this.Lookupvalue;
                lookups.lookupCode = this.OrgCategory;
                var strStatus = this.Status == "Active" ? "Y" : "N";
                lookups.IsActive = strStatus;
                lookups.ModifiedBy = this.ModifiedBy;
                lookups.ModifiedDate = DateTime.Now;
                _db.SaveChanges();
                this.LookupID = lookups.LookupID;
                this.CreatedBy = lookups.CreateBy;
                this.CreatedOn = lookups.CreateDate == null ? DateTime.MinValue : (DateTime)lookups.CreateDate;
                this.ModifiedOn = lookups.ModifiedDate;
                return this;
            }
            else
            {
                lookups = new MNT_Lookups();
                lookups.Category = this.LookUpCategory;
                lookups.Lookupdesc = this.Lookupdesc;
                lookups.Description = this.Lookupdesc;
                lookups.Lookupvalue = this.Lookupvalue;
                lookups.lookupCode = this.OrgCategory;
                var strStatus = this.Status == "Active" ? "Y" : "N";
                lookups.IsActive = strStatus;
                lookups.CreateBy = this.CreatedBy;
                lookups.CreateDate = DateTime.Now;
                _db.MNT_Lookups.AddObject(lookups);
                _db.SaveChanges();
                this.CreatedOn = (DateTime)lookups.CreateDate;
                this.LookupID = lookups.LookupID;
                return this;
            }
        }

        #endregion

    }
}

