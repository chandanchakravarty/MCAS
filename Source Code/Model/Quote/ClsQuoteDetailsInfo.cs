using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Quote
{
    public class ClsQuoteDetailsInfo : Cms.Model.ClsCommonModel 
    {
        private const string QQ_CUSTOMER_PARTICULAR = "QQ_MOTOR_QUOTE_DETAILS";
        public ClsQuoteDetailsInfo()
        {
            base.dtModel.TableName = "QQ_MOTOR_QUOTE_DETAILS";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table QOT_CUSTOMER_QUOTE_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }

        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("QUOTE_ID", typeof(int));
            base.dtModel.Columns.Add("YEAR_OF_REG", typeof(string));
            base.dtModel.Columns.Add("MAKE", typeof(string));
            base.dtModel.Columns.Add("MODEL", typeof(string));
            base.dtModel.Columns.Add("MODEL_TYPE", typeof(string));
            base.dtModel.Columns.Add("ENG_CAPACITY", typeof(int));
            base.dtModel.Columns.Add("NO_OF_DRIVERS", typeof(int));
            base.dtModel.Columns.Add("ANY_CLAIM", typeof(string));
            base.dtModel.Columns.Add("NO_OF_CLAIM", typeof(int));
            base.dtModel.Columns.Add("TOTAL_CLAIM_AMT", typeof(string));
            base.dtModel.Columns.Add("COVERAGE_TYPE", typeof(int));
            base.dtModel.Columns.Add("NO_CLAIM_DISCOUNT", typeof(string));

            base.dtModel.Columns.Add("IS_NEW", typeof(string));
            base.dtModel.Columns.Add("REGISTRATION_NO", typeof(string));
            base.dtModel.Columns.Add("COVER_NOTE_NO", typeof(string));
            base.dtModel.Columns.Add("DATE_LTA_REGISTRATION", typeof(string));
            base.dtModel.Columns.Add("ENGINE_NO", typeof(string));
            base.dtModel.Columns.Add("CHASSIS_NO", typeof(string));
            base.dtModel.Columns.Add("IS_UNDER_HIRE", typeof(string));
            base.dtModel.Columns.Add("FINANCE_COMP_NAME", typeof(string));
            base.dtModel.Columns.Add("IS_DEMERIT_POINT", typeof(string));
            base.dtModel.Columns.Add("DEMERIT_DESC", typeof(string));
            base.dtModel.Columns.Add("IS_REJECTED", typeof(string));
            base.dtModel.Columns.Add("REJECTED_DESC", typeof(string));
            base.dtModel.Columns.Add("IS_DISEASE", typeof(string));
            base.dtModel.Columns.Add("DISEASE_DESC", typeof(string));
            base.dtModel.Columns.Add("NAMED_DRIVER_AMT", typeof(string));
            base.dtModel.Columns.Add("UNNAMED_DRIVER_AMT", typeof(string));
            base.dtModel.Columns.Add("BASE_PREMIUM", typeof(double));
            base.dtModel.Columns.Add("DEMERIT_DISC_AMT", typeof(double));
            base.dtModel.Columns.Add("GST_AMOUNT", typeof(double));
            base.dtModel.Columns.Add("FINAL_PREMIUM", typeof(double));

          
            //base.dtModel.Columns.Add("IS_ACTIVE", typeof(string));
            //base.dtModel.Columns.Add("CREATED_BY", typeof(int));
            //base.dtModel.Columns.Add("CREATED_DATETIME", typeof(DateTime));
            //base.dtModel.Columns.Add("MODIFIED_BY", typeof(int));
            //base.dtModel.Columns.Add("LAST_UPDATED_DATETIME", typeof(DateTime));

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

        public string YEAR_OF_REG
        {
            get
            {
                return base.dtModel.Rows[0]["YEAR_OF_REG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["YEAR_OF_REG"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["YEAR_OF_REG"] = value;
            }
        }

        public string MAKE
        {
            get
            {
                return base.dtModel.Rows[0]["MAKE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MAKE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MAKE"] = value;
            }
        }

        public string MODEL
        {
            get
            {
                return base.dtModel.Rows[0]["MODEL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MODEL"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MODEL"] = value;
            }
        }

        public string MODEL_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["MODEL_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MODEL_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["MODEL_TYPE"] = value;
            }
        }

        public int ENG_CAPACITY
        {
            get
            {
                return base.dtModel.Rows[0]["ENG_CAPACITY"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ENG_CAPACITY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ENG_CAPACITY"] = value;
            }
        }

        public int NO_OF_DRIVERS
        {
            get
            {
                return base.dtModel.Rows[0]["NO_OF_DRIVERS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NO_OF_DRIVERS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NO_OF_DRIVERS"] = value;
            }
        }

        public string ANY_CLAIM
        {
            get
            {
                return base.dtModel.Rows[0]["ANY_CLAIM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ANY_CLAIM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ANY_CLAIM"] = value;
            }
        }

        public int NO_OF_CLAIM
        {
            get
            {
                return base.dtModel.Rows[0]["NO_OF_CLAIM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NO_OF_CLAIM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["NO_OF_CLAIM"] = value;
            }
        }

        public string TOTAL_CLAIM_AMT
        {
            get
            {
                return base.dtModel.Rows[0]["TOTAL_CLAIM_AMT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TOTAL_CLAIM_AMT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["TOTAL_CLAIM_AMT"] = value;
            }
        }

        public int COVERAGE_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["COVERAGE_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COVERAGE_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COVERAGE_TYPE"] = value;
            }
        }

        public string NO_CLAIM_DISCOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["NO_CLAIM_DISCOUNT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NO_CLAIM_DISCOUNT"] = value;
            }
        }

        //************************************************************


        public string IS_NEW
        {
            get
            {
                return base.dtModel.Rows[0]["IS_NEW"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_NEW"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_NEW"] = value;
            }
        }

        public string REGISTRATION_NO
        {
            get
            {
                return base.dtModel.Rows[0]["REGISTRATION_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REGISTRATION_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REGISTRATION_NO"] = value;
            }
        }

        public string COVER_NOTE_NO
        {
            get
            {
                return base.dtModel.Rows[0]["COVER_NOTE_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COVER_NOTE_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["COVER_NOTE_NO"] = value;
            }
        }

        public string DATE_LTA_REGISTRATION
        {
            get
            {
                return base.dtModel.Rows[0]["DATE_LTA_REGISTRATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DATE_LTA_REGISTRATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DATE_LTA_REGISTRATION"] = value;
            }
        }

        public string ENGINE_NO
        {
            get
            {
                return base.dtModel.Rows[0]["ENGINE_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENGINE_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ENGINE_NO"] = value;
            }
        }

        public string CHASSIS_NO
        {
            get
            {
                return base.dtModel.Rows[0]["CHASSIS_NO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CHASSIS_NO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CHASSIS_NO"] = value;
            }
        }

        public string IS_UNDER_HIRE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_UNDER_HIRE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_UNDER_HIRE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_UNDER_HIRE"] = value;
            }
        }

        public string FINANCE_COMP_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["FINANCE_COMP_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FINANCE_COMP_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FINANCE_COMP_NAME"] = value;
            }
        }

        public string IS_DEMERIT_POINT
        {
            get
            {
                return base.dtModel.Rows[0]["IS_DEMERIT_POINT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_DEMERIT_POINT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_DEMERIT_POINT"] = value;
            }
        }

        public string DEMERIT_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["DEMERIT_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DEMERIT_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DEMERIT_DESC"] = value;
            }
        }

        public string IS_REJECTED
        {
            get
            {
                return base.dtModel.Rows[0]["IS_REJECTED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_REJECTED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_REJECTED"] = value;
            }
        }

        public string REJECTED_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["REJECTED_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REJECTED_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REJECTED_DESC"] = value;
            }
        }

        public string IS_DISEASE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_DISEASE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_DISEASE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_DISEASE"] = value;
            }
        }

        public string DISEASE_DESC
        {
            get
            {
                return base.dtModel.Rows[0]["DISEASE_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DISEASE_DESC"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DISEASE_DESC"] = value;
            }
        }

        public string NAMED_DRIVER_AMT
        {
            get
            {
                return base.dtModel.Rows[0]["NAMED_DRIVER_AMT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NAMED_DRIVER_AMT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NAMED_DRIVER_AMT"] = value;
            }
        }

        public string UNNAMED_DRIVER_AMT
        {
            get
            {
                return base.dtModel.Rows[0]["UNNAMED_DRIVER_AMT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["UNNAMED_DRIVER_AMT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["UNNAMED_DRIVER_AMT"] = value;
            }
        }

        public double BASE_PREMIUM
        {
            get
            {
                return base.dtModel.Rows[0]["BASE_PREMIUM"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["BASE_PREMIUM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BASE_PREMIUM"] = value;
            }
        }

        public double DEMERIT_DISC_AMT
        {
            get
            {
                return base.dtModel.Rows[0]["DEMERIT_DISC_AMT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["DEMERIT_DISC_AMT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEMERIT_DISC_AMT"] = value;
            }
        }

        public double GST_AMOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["GST_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["GST_AMOUNT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["GST_AMOUNT"] = value;
            }
        }

        public double FINAL_PREMIUM
        {
            get
            {
                return base.dtModel.Rows[0]["FINAL_PREMIUM"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FINAL_PREMIUM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FINAL_PREMIUM"] = value;
            }
        }
       
    }
}
