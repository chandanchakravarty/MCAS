using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Web.Objects.CommonHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class TacFileUploadViewEditorModel : BaseModel
    {


        # region properties
        public int AccidentClaimId { get; set; }
        public int ClaimId { get; set; }
        public string Uploadrefno { get; set; }
        public string Uploadstatus { get; set; }
        public string claimode { get; set; }
        public List<Uploadstatuslist> Uploadstatuslist1{ get; set; }
        private DateTime? _uploadToDate = null;
        private DateTime? _uploadFromDate = null;
        [DisplayName("Upload To Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UploadToDate
        {
            get { return _uploadToDate; }
            set { _uploadToDate = value; }
        }
        [DisplayName("Upload From Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UploadFromDate
        {
            get { return _uploadFromDate; }
            set { _uploadFromDate = value; }
        }
        #endregion

        public static List<Uploadstatuslist> fetch()
        {
            List<Uploadstatuslist> item = new List<Uploadstatuslist>();
            item.Add(new Uploadstatuslist() { Text = "[Select...]" });
            item.Add(new Uploadstatuslist() { Id = "Incomplete", Text = "Incomplete" });
            item.Add(new Uploadstatuslist() { Id = "Failed", Text = "Failed" });
            item.Add(new Uploadstatuslist() { Id = "Success", Text = "Success" });
            return item;
            
        }
    }

    public class Uploadstatuslist
    {
        public string Id {get;set;}
        public string Text { get; set; }
    }
}
