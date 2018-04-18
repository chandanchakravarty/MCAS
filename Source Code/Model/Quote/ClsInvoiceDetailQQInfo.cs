using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Quote
{
    public class ClsInvoiceDetailQQInfo : Cms.Model.ClsCommonModel 
    {
        private const string QQ_INVOICE_PARTICULAR_MARINE = "QQ_INVOICE_PARTICULAR_MARINE";
        public ClsInvoiceDetailQQInfo()
        {
            base.dtModel.TableName = "QQ_INVOICE_PARTICULAR_MARINE";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table QOT_CUSTOMER_QUOTE_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
      
        private void AddColumns()
        {
            base.dtModel.Columns.Add("ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("QUOTE_ID", typeof(int));
            base.dtModel.Columns.Add("CUSTOMER_TYPE", typeof(int));
            base.dtModel.Columns.Add("COMPANY_NAME", typeof(string));
            base.dtModel.Columns.Add("BUSINESS_TYPE", typeof(int));
            base.dtModel.Columns.Add("OPEN_COVER_NO", typeof(string));
            base.dtModel.Columns.Add("INVOICE_AMOUNT", typeof(double));
            base.dtModel.Columns.Add("INVOICE_TYPE", typeof(string));
            base.dtModel.Columns.Add("CURRENCY_TYPE", typeof(int));
            base.dtModel.Columns.Add("BILLING_CURRENCY", typeof(int));
            base.dtModel.Columns.Add("MARK_UP_RATE_PERC", typeof(double));
            //base.dtModel.Columns.Add("IS_ACTIVE", typeof(string));
            //base.dtModel.Columns.Add("CREATED_BY", typeof(int));
            //base.dtModel.Columns.Add("CREATED_DATETIME", typeof(DateTime));
            //base.dtModel.Columns.Add("MODIFIED_BY", typeof(int));
            //base.dtModel.Columns.Add("LAST_UPDATED_DATETIME", typeof(DateTime));
            base.dtModel.Columns.Add("DATE_OF_QUOTATION", typeof(string));
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
 
        }

        public int ID
        {
            get
            {
                return base.dtModel.Rows[0]["ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ID"] = value;
            }
        }

        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
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

        public int CUSTOMER_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_TYPE"] = value;
            }
        }

        public string COMPANY_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["COMPANY_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COMPANY_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["COMPANY_NAME"] = value;
            }
        }

        public int BUSINESS_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["BUSINESS_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BUSINESS_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BUSINESS_TYPE"] = value;
            }
        }

        public string OPEN_COVER_NO
        {
            get
            {
                return base.dtModel.Rows[0]["OPEN_COVER_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["OPEN_COVER_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["OPEN_COVER_NO"] = value;
            }
        }

        public double INVOICE_AMOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["INVOICE_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INVOICE_AMOUNT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INVOICE_AMOUNT"] = value;
            }
        }

        public string INVOICE_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["INVOICE_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INVOICE_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["INVOICE_TYPE"] = value;
            }
        }

        public int CURRENCY_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["CURRENCY_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CURRENCY_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CURRENCY_TYPE"] = value;
            }
        }

        public int BILLING_CURRENCY
        {
            get
            {
                return base.dtModel.Rows[0]["BILLING_CURRENCY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["BILLING_CURRENCY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BILLING_CURRENCY"] = value;
            }
        }

        public double MARK_UP_RATE_PERC
        {
            get
            {
                return base.dtModel.Rows[0]["MARK_UP_RATE_PERC"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MARK_UP_RATE_PERC"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MARK_UP_RATE_PERC"] = value;
            }
        }
        
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

        public DateTime CREATED_DATETIME
        {
            get
            {
                return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
            }
        }

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

     
        public string DATE_OF_QUOTATION
        {
            get
            {
                return base.dtModel.Rows[0]["DATE_OF_QUOTATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DATE_OF_QUOTATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DATE_OF_QUOTATION"] = value;
            }
        }

        public int POLICY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_ID"] = value;
            }
        }

        public int POLICY_VERSION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
            }
        }
    }
}
