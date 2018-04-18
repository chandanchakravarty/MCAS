/******************************************************************************************
<Author				: - Avijit GOswami
<Start Date			: -	20/01/2012
<End Date			: -	
<Description		: - Database Model for MNT_CURRENCY_MASTER
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance
{
  public  class ClsCurrencyMasterDetailsInfo:Cms.Model.ClsCommonModel
    {
        /// <summary>
        /// Database Model for MNT_ACCUMULATION_REFERENCE.
        /// </summary>

        private const string MNT_CURRENCY_MASTER = "MNT_CURRENCY_MASTER";
        public ClsCurrencyMasterDetailsInfo()
        {
            base.dtModel.TableName = "MNT_CURRENCY_MASTER";	// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table MNT_CURRENCY_MASTER
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("CURRENCY_ID", typeof(int));
            base.dtModel.Columns.Add("CURR_CODE", typeof(string));
            base.dtModel.Columns.Add("CURR_DESC", typeof(string));
            base.dtModel.Columns.Add("CURR_SYMBOL", typeof(string));
            base.dtModel.Columns.Add("CURR_PRECISION", typeof(int));
            base.dtModel.Columns.Add("CURR_CALCULATEFORMAT", typeof(string));
            base.dtModel.Columns.Add("CURR_PRINTFORMAT", typeof(string));
            base.dtModel.Columns.Add("CURR_CHECKDIGITS", typeof(string));
            base.dtModel.Columns.Add("CURR_CHECKDECIMAL", typeof(string));
            base.dtModel.Columns.Add("CURR_DECIMALSEPR", typeof(string));
            base.dtModel.Columns.Add("CURR_THOUSANDSEPR", typeof(string));            
        }
        #region Database schema details
        // model for database field [CURR_ID] INT
        public int CURRENCY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CURRENCY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CURRENCY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CURRENCY_ID"] = value;
            }
        }

        // model for database field [CURR_CODE] NVARCHAR
        public string CURR_CODE
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_CODE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_CODE"] = value;
            }
        }
        // model for database field [CURR_DESC] string
        public string CURR_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_DESC"] = value;
            }
        }
        // model for database field CURR_SYMBOL(string)
        public string CURR_SYMBOL
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_SYMBOL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_SYMBOL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_SYMBOL"] = value;
            }
        }

        // model for database field [CURR_PRECISION] smallint
        public int CURR_PRECISION
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_PRECISION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CURR_PRECISION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CURR_PRECISION"] = value;
            }
        }
        // model for database field  [CURR_CALCULATEFORMATT] varchar
        public string CURR_CALCULATEFORMAT
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_CALCULATEFORMAT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_CALCULATEFORMAT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_CALCULATEFORMAT"] = value;
            }
        }

        // model for database [CURR_PRINTFORMAT] string
        public string CURR_PRINTFORMAT
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_PRINTFORMAT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_PRINTFORMAT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_PRINTFORMAT"] = value;
            }
        }
        // model for database  [CURR_CHECKDIGITS]varchar
        public string CURR_CHECKDIGITS
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_CHECKDIGITS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_CHECKDIGITS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_CHECKDIGITS"] = value;
            }
        }
        // model for database  [CURR_CHECKDECIMAL]varchar
        public string CURR_CHECKDECIMAL
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_CHECKDECIMAL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_CHECKDECIMAL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_CHECKDECIMAL"] = value;
            }
        }
        // model for database  [CURR_DECIMALSEPR]varchar
        public string CURR_DECIMALSEPR
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_DECIMALSEPR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_DECIMALSEPR"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_DECIMALSEPR"] = value;
            }
        }
        // model for database  [CURR_THOUSANDSEPR]varchar
        public string CURR_THOUSANDSEPR
        {
            get
            {
                return base.dtModel.Rows[0]["CURR_THOUSANDSEPR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CURR_THOUSANDSEPR"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CURR_THOUSANDSEPR"] = value;
            }
        }
        // model for database  [IS_ACTIVE]char
        public string IS_ACTIVE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_ACTIVE"] = value;
            }
        }
        // model for database  [CREATED_BY]int
        public int CREATED_BY
        {
            get
            {
                return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CREATED_BY"] = value;
            }
        }
        // model for database  [CREATED_DATETIME]datetime
        public DateTime CREATED_DATETIME
        {
            get
            {
                return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CREATED_BY"] = value;
            }
        }
        // model for database  [MODIFIED_BY]int
        public int MODIFIED_BY
        {
            get
            {
                return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MODIFIED_BY"] = value;
            }
        }
        // model for database  [LAST_UPDATED_DATETIME]datetime
        public DateTime LAST_UPDATED_DATETIME
        {
            get
            {
                return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
            }
        }
        #endregion
    }
}
