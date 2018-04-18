using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.CommonHelper;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.MastersHelper;
using System.ComponentModel;
using MCAS.Entity;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class TacFileUploadEditorModel:BaseModel
    {
        # region properties
        private DateTime? _sdatetime = null;
        public int AccidentClaimId {get;set;}
        public int ClaimId {get;set;}
        public string Prop1 { get; set; }

        public string OrganizationType { get; set; }

        public string OrganizationName { get; set; }

        public string OrgCategory { get; set; }
        public string OrgCategoryName { get; set; }

        public List<OrgCountryModelnew> OrganizationNameList { get; set; }
        public List<LookUpListItems> Categorylist { get; set; }
        public int? FileId { get; set; }
        public string FileRefNo { get; set; }
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimUploadProcessing.TacFileUploadEditor), ErrorMessageResourceName = "RFVTAC_IPFile")]
        public string TACIP { get; set; }

        public string HclaimMode { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimUploadProcessing.TacFileUploadEditor), ErrorMessageResourceName = "RFVTAC_ACC_REP")]
        public string TACACCREP { get; set; }

        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimUploadProcessing.TacFileUploadEditor), ErrorMessageResourceName = "RFVTAC_IP_BUS")]
        public string TACIPBUS { get; set; }

        [DisplayName("Scheduled Start Date & Time")]
        [Required(ErrorMessageResourceType = typeof(MCAS.Web.Objects.Resources.ClaimUploadProcessing.TacFileUploadEditor), ErrorMessageResourceName = "RFVDateTime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Sdatetime
        {
            get { return _sdatetime; }
            set { _sdatetime = value; }
        }   
 
        public override string screenId
        {
            get
            {
                return "272";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "272";
            }

        }

        #region hidden
        public string Hf1 { get; set; }
        public string Hfn1 { get; set; }
        public string Hf2 { get; set; }
        public string Hfn2 { get; set; }
        public string Hf3 { get; set; }
        public string Hfn3 { get; set; }
        public string Hselect { get; set; }
        public string Hgetval { get; set; }
        public string HgetCatName { get; set; }
        #endregion
        # endregion

        public static List<TacFileUploadViewListModel> Fetchall()
        {
            MCASEntities obj = new MCASEntities();
            var item = new List<TacFileUploadViewListModel>();
            var List = obj.Proc_GetMNT_FileUpload().ToList();
            if (List.Any())
            {
                foreach (var data in List)
                {
                    item.Add(new TacFileUploadViewListModel()
                    {
                        FileId = data.FileId,
                        FileRefNo = data.FileRefNo,
                        FileName = data.FileName,
                        FileType = data.FileType,
                        UploadType = data.UploadType,
                        UploadPath = data.UploadPath,
                        UploadedDate = data.UploadedDate,
                        TotalRecords = data.TotalRecords,
                        SuccessRecords = data.SuccessRecords,
                        FailedRecords = data.FailedRecords,
                        Status = data.Status,
                        Is_Processed = data.Is_Processed,
                        Processed_Date = data.Processed_Date,
                        Is_Active = data.Is_Active,
                        HasError = data.HasError,
                        CreatedBy = data.CreatedBy,
                    });
                }
            }

            return item;
        }



        public static List<TacFileUploadViewListModel> Fetchall(string FileRefNo, string status, string uploadDate, string UploadToDate,string uploadtype)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO #TempTable select    FileId ,FileRefNo ,FileName ,FileType ,UploadType ,UploadPath ,UploadedDate ,TotalRecords ,SuccessRecords ,FailedRecords ,Status ,Is_Processed ,Processed_Date ,Is_Active ,HasError ,CreatedBy from dbo.MNT_FileUpload");

            sb.Append(" where UPPER(FileRefNo) Like UPPER('" + uploadtype + "%')");
            sb.Append(" and ");
            
            if (FileRefNo != "")
            {
                sb.Append("UPPER(FileRefNo) Like UPPER('" + FileRefNo + "%')");
                sb.Append(" and ");
            }

            if (status != "")
            {
                sb.Append("UPPER(Status) = UPPER('" + status + "')");
                sb.Append(" and ");
            }

            if (uploadDate != "" && UploadToDate != "")
            {
                string dd = Convert.ToDateTime(uploadDate).ToString("yyyy-MM-dd");
                string dd2 = Convert.ToDateTime(UploadToDate).ToString("yyyy-MM-dd");
                sb.Append("CAST(UploadedDate AS DATE) >= '" + dd + "'  and CAST(UploadedDate AS DATE) <= '" + dd2 + "'");
            }
            else if (uploadDate != "")
            {
                string dd = Convert.ToDateTime(uploadDate).ToString("yyyy-MM-dd");
                sb.Append("CAST(UploadedDate AS DATE) >= '" + dd + "'");
            }
            else if (UploadToDate != "")
            {
                string dd = Convert.ToDateTime(UploadToDate).ToString("yyyy-MM-dd");
                sb.Append("CAST(UploadedDate AS DATE) <= '" + dd + "'");
            }
            

            string fin = sb.ToString().TrimEnd();
            string endval = fin.Split(' ').Last();
            string queryString = string.Empty;
            if (endval == "and")
            {
                queryString = fin.Substring(0, fin.LastIndexOf(" ") < 0 ? 0 : fin.LastIndexOf(" "));
            }
            else
            {
                queryString = fin;
            }
            MCASEntities obj = new MCASEntities();
            var List = obj.Proc_GetMNT_FileUploadListSearch(queryString).ToList();
            var item = new List<TacFileUploadViewListModel>();
            if (List.Any())
            {
                foreach (var data in List)
                {
                    item.Add(new TacFileUploadViewListModel()
                    {
                        FileId = data.FileId,
                        FileRefNo = data.FileRefNo,
                        FileName = data.FileName,
                        FileType = data.FileType,
                        UploadType = data.UploadType,
                        UploadPath = data.UploadPath,
                        UploadedDate = data.UploadedDate,
                        TotalRecords = data.TotalRecords,
                        SuccessRecords = data.SuccessRecords,
                        FailedRecords = data.FailedRecords,
                        Status = data.Status,
                        Is_Processed = data.Is_Processed,
                        Processed_Date = data.Processed_Date,
                        Is_Active = data.Is_Active,
                        HasError = data.HasError,
                        CreatedBy = data.CreatedBy,
                    });
                }
            }

            return item;
        }

        public static List<OrgCountryModelnew> FetchOrg()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<OrgCountryModelnew>();
            var orgCountry = (from l in _db.MNT_OrgCountry orderby l.OrganizationName select l);
            if (orgCountry.Any())
            {
                item.Add(new OrgCountryModelnew()
                    {
                        OrganizationName = "[Select...]",
                        CountryOrgazinationCode = "Select"
                    });
                foreach (var data in orgCountry)
                {
                    item.Add(new OrgCountryModelnew()
                    {
                        OrganizationName = data.OrganizationName,
                        CountryOrgazinationCode = data.CountryOrgazinationCode,
                    });
                }
            }
            return item;
        }

        public static string FetchOrgCategoryName(string p)
        {

            MCASEntities obj = new MCASEntities();
            List<orgCategory1> sgf = new List<orgCategory1>();

            var innerJoinQuery =
    from deduct in obj.MNT_FileUpload
    join org in obj.MNT_OrgCountry on deduct.OrganizationName equals org.CountryOrgazinationCode
    select new { OrganizationName = org.OrganizationName, OrganizationCode = deduct };



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
    }

    public class OrgCountryModelnew
    {
     public string OrganizationName {get;set;}
     public string CountryOrgazinationCode { get; set; }
    }
}
