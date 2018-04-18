using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.CommonHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MCAS.Entity;
namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimFileUploadEditorModel : BaseModel
    {
        # region properties
        private DateTime? _sdatetime = null;
        public int AccidentClaimId { get; set; }
        public int ClaimId { get; set; }

        [DisplayName("Claim File")]
        [Required(ErrorMessage = "SAP Claim file is required.")]
        public string SAPClaimfile { get; set; }

         [Required(ErrorMessage = "TAC Standard Code Claim file is required.")]
        public string TACClaimFile { get; set; }

        [DisplayName("Scheduled Start Date & Time")]
        [Required(ErrorMessage = "Scheduled Start Date & Time is required.")]
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
                return "274";
            }
        }
        public override string listscreenId
        {

            get
            {
                return "274";
            }

        }
        [Required(ErrorMessage = "Organization Category is required.")]
        public string OrgCategory { get; set; }
        public string OrgCategoryName { get; set; }
        public int? FileId { get; set; }
        public string FileRefNo { get; set; }
        public List<LookUpListItems> Categorylist { get; set; }
        public string Hselect { get; set; }
        public string Hgetval { get; set; }
        public string HgetCatName { get; set; }

        #region hidden
        public string Hf1 { get; set; }
        public string Hfn1 { get; set; }
        public string Hf2 { get; set; }
        public string Hfn2 { get; set; }

        #endregion

        # endregion
   
        public static List<ClaimFileUploadViewListModel> Fetchall(string FileRefNo, string status, string uploadDate, string UploadToDate, string uploadtype)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO #TempTable select    FileId ,FileRefNo ,FileName ,FileType ,UploadType ,UploadPath ,UploadedDate ,TotalRecords ,SuccessRecords ,FailedRecords ,Status ,Is_Processed ,Processed_Date ,Is_Active ,HasError ,CreatedBy from dbo.MNT_FileUpload");

            sb.Append(" where UPPER(UploadType) Like UPPER('" + uploadtype + "%') ");
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
            var item = new List<ClaimFileUploadViewListModel>();
            if (List.Any())
            {
                foreach (var data in List)
                {
                    item.Add(new ClaimFileUploadViewListModel()
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
    }
}
