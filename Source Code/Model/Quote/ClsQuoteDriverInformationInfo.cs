using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Quote
{
    /// <summary>
    /// Summary description for ClsQuoteDriverInformationInfo.
    /// </summary>

    public class ClsQuoteDriverInformationInfo : Cms.Model.ClsCommonModel
    {
        private const string QQ_DRIVER_INFORMATION = "QQ_DRIVER_INFORMATION";

        public ClsQuoteDriverInformationInfo()
        {
            base.dtModel.TableName = "QQ_DRIVER_INFORMATION";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table QQ_DRIVER_INFORMATION
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }

        private void AddColumns()
        {
            base.dtModel.Columns.Add("QUOTE_ID", typeof(Int32));
            base.dtModel.Columns.Add("DRIVER_NAME", typeof(String));
            base.dtModel.Columns.Add("DRIVER_CODE", typeof(String));
            base.dtModel.Columns.Add("DRIVER_GENDER", typeof(String));
            base.dtModel.Columns.Add("DRIVER_DOB", typeof(DateTime));
            base.dtModel.Columns.Add("DRIVER_TYPE", typeof(String));
            base.dtModel.Columns.Add("DRIVER_LICENSE_NO", typeof(String));
            base.dtModel.Columns.Add("DRIVER_LICENSED_DATE", typeof(DateTime));
            base.dtModel.Columns.Add("DRIVER_DRUG_VIOLATION", typeof(String));
        }

        public int QUOTE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["QUOTE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["QUOTE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["QUOTE_ID"] = value;
            }
        }

        public string DRIVER_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_NAME"] = value;
            }
        }

        public string DRIVER_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_CODE"] = value;
            }
        }

        public string DRIVER_GENDER
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_GENDER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_GENDER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_GENDER"] = value;
            }
        }

        public DateTime DRIVER_DOB
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DRIVER_DOB"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_DOB"] = value;
            }
        }

        public String DRIVER_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_TYPE"] = value;
            }
        }

        public string DRIVER_LICENSE_NO
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_LICENSE_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LICENSE_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_LICENSE_NO"] = value;
            }
        }

        public DateTime DRIVER_LICENSED_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_LICENSED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DRIVER_LICENSED_DATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_LICENSED_DATE"] = value;
            }
        }

        public string DRIVER_DRUG_VIOLATION
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_DRUG_VIOLATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_DRUG_VIOLATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_DRUG_VIOLATION"] = value;
            }
        }
    }
}
