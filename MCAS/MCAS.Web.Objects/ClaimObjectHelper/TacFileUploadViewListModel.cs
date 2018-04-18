using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.CommonHelper;
using System.ComponentModel.DataAnnotations;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class TacFileUploadViewListModel : BaseModel
    {
        #region Private Fields
        private DateTime? _UploadedDate = null;
        private DateTime? _Processed_Date = null;
        #endregion

        #region Public properties
        public int? FileId { get; set; }
        public string FileRefNo { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string UploadType { get; set; }
        public string UploadPath { get; set; }
        public DateTime? UploadedDate { get { return _UploadedDate; } set { _UploadedDate = value; } }
        public int? TotalRecords { get; set; }
        public int? SuccessRecords { get; set; }
        public int? FailedRecords { get; set; }
        public string Status { get; set; }
        public string Is_Processed { get; set; }
        public DateTime? Processed_Date { get { return _Processed_Date; } set { _Processed_Date = value; } }
        public string Is_Active { get; set; }
        public string HasError { get; set; }
        public string CreatedBy { get; set; }
        #endregion
        public override string screenId
        {
            get
            {
                return "273";
            }

        }
        public override string listscreenId
        {
            get
            {
                return "273";
            }

        }
    }
}
