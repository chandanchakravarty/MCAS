/******************************************************************************************
<Author					: - Sneha
<Start Date				: -	5/12/2011
<End Date				: -	
<Description			: - 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;
using Cms.EbixDataLayer;

namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsLiabilityCoverage : Cms.Model.ClsCommonModel
    {
        public ClsLiabilityCoverage()
        {
            base.dtModel.TableName = "POL_BOP_GENERAL_COVERAGE";
            this.AddColumns();
            base.dtModel.Rows.Add(base.dtModel.NewRow());
        }

        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int));
            base.dtModel.Columns.Add("COVERAGE_ID", typeof(int));
            base.dtModel.Columns.Add("COVERAGE_CODE_ID", typeof(int));
            base.dtModel.Columns.Add("RI_APPLIES", typeof(string));
            base.dtModel.Columns.Add("LIMIT_OVERRIDE", typeof(string));
            base.dtModel.Columns.Add("LIMIT_1", typeof(decimal));
            base.dtModel.Columns.Add("LIMIT_1_TYPE", typeof(string));
            base.dtModel.Columns.Add("LIMIT_2", typeof(decimal));
            base.dtModel.Columns.Add("LIMIT_2_TYPE", typeof(string));
            base.dtModel.Columns.Add("LIMIT1_AMOUNT_TEXT", typeof(string));
            base.dtModel.Columns.Add("LIMIT2_AMOUNT_TEXT", typeof(string));
            base.dtModel.Columns.Add("DEDUCT_OVERRIDE", typeof(string));
            base.dtModel.Columns.Add("DEDUCTIBLE_1", typeof(decimal));
            base.dtModel.Columns.Add("DEDUCTIBLE_1_TYPE", typeof(string));
            base.dtModel.Columns.Add("DEDUCTIBLE_2", typeof(decimal));
            base.dtModel.Columns.Add("DEDUCTIBLE_2_TYPE", typeof(string));
            base.dtModel.Columns.Add("MINIMUM_DEDUCTIBLE", typeof(decimal));
            base.dtModel.Columns.Add("DEDUCTIBLE1_AMOUNT_TEXT", typeof(string));
            base.dtModel.Columns.Add("DEDUCTIBLE2_AMOUNT_TEXT", typeof(string));
            base.dtModel.Columns.Add("DEDUCTIBLE_REDUCES", typeof(string));
            base.dtModel.Columns.Add("INITIAL_RATE", typeof(decimal));
            base.dtModel.Columns.Add("FINAL_RATE", typeof(decimal));
            base.dtModel.Columns.Add("AVERAGE_RATE", typeof(string));
            base.dtModel.Columns.Add("WRITTEN_PREMIUM", typeof(decimal));
            base.dtModel.Columns.Add("FULL_TERM_PREMIUM", typeof(decimal));
            base.dtModel.Columns.Add("IS_SYSTEM_COVERAGE", typeof(string));
            base.dtModel.Columns.Add("LIMIT_ID", typeof(int));
            base.dtModel.Columns.Add("DEDUC_ID", typeof(int));
            base.dtModel.Columns.Add("ADD_INFORMATION", typeof(string));
           // base.dtModel.Columns.Add("CREATED_BY", typeof(DateTime));
           // base.dtModel.Columns.Add("CREATED_DATETIME", typeof(DateTime));
           //base.dtModel.Columns.Add("MODIFIED_BY", typeof(int));
           // base.dtModel.Columns.Add("LAST_UPDATED_DATETIME", typeof(DateTime));
            base.dtModel.Columns.Add("INDEMNITY_PERIOD", typeof(int));
            base.dtModel.Columns.Add("CHANGE_INFORCE_PREM", typeof(decimal));
            //base.dtModel.Columns.Add("IS_ACTIVE", typeof(string));
            base.dtModel.Columns.Add("ACC_CO_DISCOUNT", typeof(decimal));
            base.dtModel.Columns.Add("ACTION", typeof(string));
            base.dtModel.Columns.Add("COVERAGE_TYPE", typeof(string));
            

        }

        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
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
                return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
                return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
            }
        }


        public int COVERAGE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["COVERAGE_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["COVERAGE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COVERAGE_ID"] = value;
            }
        }


        public int COVERAGE_CODE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["COVERAGE_CODE_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["COVERAGE_CODE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COVERAGE_CODE_ID"] = value;
            }
        }


        public string RI_APPLIES
        {
            get
            {
                return base.dtModel.Rows[0]["RI_APPLIES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RI_APPLIES"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["RI_APPLIES"] = value;
            }
        }


        public string LIMIT_OVERRIDE
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT_OVERRIDE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LIMIT_OVERRIDE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT_OVERRIDE"] = value;
            }
        }


        public double LIMIT_1
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT_1"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["LIMIT_1"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT_1"] = value;
            }
        }


        public string LIMIT_1_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT_1_TYPE"] == DBNull.Value ? Convert.ToString(null) :  (base.dtModel.Rows[0]["LIMIT_1_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT_1_TYPE"] = value;
            }
        }


        public decimal LIMIT_2
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT_2"] == DBNull.Value ? -1 : decimal.Parse(base.dtModel.Rows[0]["LIMIT_2"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT_2"] = value;
            }
        }


        public string LIMIT_2_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT_2_TYPE"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["LIMIT_2_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT_2_TYPE"] = value;
            }
        }


        public string LIMIT1_AMOUNT_TEXT
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT1_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["LIMIT1_AMOUNT_TEXT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT1_AMOUNT_TEXT"] = value;
            }
        }

        public string LIMIT2_AMOUNT_TEXT
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT2_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["LIMIT2_AMOUNT_TEXT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT2_AMOUNT_TEXT"] = value;
            }
        }

        public string DEDUCT_OVERRIDE
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCT_OVERRIDE"] == DBNull.Value ? Convert.ToString(null) :(base.dtModel.Rows[0]["DEDUCT_OVERRIDE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCT_OVERRIDE"] = value;
            }
        }


        public double DEDUCTIBLE_1
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCTIBLE_1"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE_1"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCTIBLE_1"] = value;
            }
        }


        public string DEDUCTIBLE_1_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCTIBLE_1_TYPE"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["DEDUCTIBLE_1_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCTIBLE_1_TYPE"] = value;
            }
        }


        public decimal DEDUCTIBLE_2
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCTIBLE_2"] == DBNull.Value ? -1 : decimal.Parse(base.dtModel.Rows[0]["DEDUCTIBLE_2"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCTIBLE_2"] = value;
            }
        }


        public string DEDUCTIBLE_2_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCTIBLE_2_TYPE"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["DEDUCTIBLE_2_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCTIBLE_2_TYPE"] = value;
            }
        }


        public double MINIMUM_DEDUCTIBLE
        {
            get
            {
                return base.dtModel.Rows[0]["MINIMUM_DEDUCTIBLE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["MINIMUM_DEDUCTIBLE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["MINIMUM_DEDUCTIBLE"] = value;
            }
        }


        public string DEDUCTIBLE1_AMOUNT_TEXT
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCTIBLE1_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["DEDUCTIBLE1_AMOUNT_TEXT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCTIBLE1_AMOUNT_TEXT"] = value;
            }
        }

        public string DEDUCTIBLE2_AMOUNT_TEXT
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCTIBLE2_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["DEDUCTIBLE2_AMOUNT_TEXT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCTIBLE2_AMOUNT_TEXT"] = value;
            }
        }


        public string DEDUCTIBLE_REDUCES
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUCTIBLE_REDUCES"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["DEDUCTIBLE_REDUCES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUCTIBLE_REDUCES"] = value;
            }
        }

        public double INITIAL_RATE
        {
            get
            {
                return base.dtModel.Rows[0]["INITIAL_RATE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["INITIAL_RATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INITIAL_RATE"] = value;
            }
        }


        public double FINAL_RATE
        {
            get
            {
                return base.dtModel.Rows[0]["FINAL_RATE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["FINAL_RATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FINAL_RATE"] = value;
            }
        }


        public string AVERAGE_RATE
        {
            get
            {
                return base.dtModel.Rows[0]["AVERAGE_RATE"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["AVERAGE_RATE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["AVERAGE_RATE"] = value;
            }
        }


        public double WRITTEN_PREMIUM
        {
            get
            {
                return base.dtModel.Rows[0]["WRITTEN_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["WRITTEN_PREMIUM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["WRITTEN_PREMIUM"] = value;
            }
        }


        public double FULL_TERM_PREMIUM
        {
            get
            {
                return base.dtModel.Rows[0]["FULL_TERM_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["FULL_TERM_PREMIUM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["FULL_TERM_PREMIUM"] = value;
            }
        }


        public string IS_SYSTEM_COVERAGE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_SYSTEM_COVERAGE"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["IS_SYSTEM_COVERAGE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["IS_SYSTEM_COVERAGE"] = value;
            }
        }

        public int LIMIT_ID
        {
            get
            {
                return base.dtModel.Rows[0]["LIMIT_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["LIMIT_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LIMIT_ID"] = value;
            }

        }

        public int DEDUC_ID
        {
            get
            {
                return base.dtModel.Rows[0]["DEDUC_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DEDUC_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DEDUC_ID"] = value;
            }

        }

        public string ADD_INFORMATION
        {
            get
            {
                return base.dtModel.Rows[0]["ADD_INFORMATION"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["ADD_INFORMATION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ADD_INFORMATION"] = value;
            }
        }

        //public int CREATED_BY
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["CREATED_BY"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CREATED_BY"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["CREATED_BY"] = value;
        //    }
        //}


        //public DateTime CREATED_DATETIME
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["CREATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CREATED_DATETIME"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["CREATED_DATETIME"] = value;
        //    }
        //}


        //public int MODIFIED_BY
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["MODIFIED_BY"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["MODIFIED_BY"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["MODIFIED_BY"] = value;
        //    }
        //}


        //public DateTime LAST_UPDATED_DATETIME
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["LAST_UPDATED_DATETIME"] = value;
        //    }
        //}


        public int INDEMNITY_PERIOD
        {
            get
            {
                return base.dtModel.Rows[0]["INDEMNITY_PERIOD"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["INDEMNITY_PERIOD"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INDEMNITY_PERIOD"] = value;
            }
        }


        public double CHANGE_INFORCE_PREM
        {
            get
            {
                return base.dtModel.Rows[0]["CHANGE_INFORCE_PREM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["CHANGE_INFORCE_PREM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CHANGE_INFORCE_PREM"] = value;
            }
        }


        //public string IS_ACTIVE
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["IS_ACTIVE"].ToString());
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["IS_ACTIVE"] = value;
        //    }
        //}


        public string ACTION
        {
            get
            {
                return base.dtModel.Rows[0]["ACTION"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["ACTION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACTION"] = value;
            }
        }

        
        public string COVERAGE_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["COVERAGE_TYPE"] == DBNull.Value ? Convert.ToString(null) : (base.dtModel.Rows[0]["COVERAGE_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COVERAGE_TYPE"] = value;
            }
        }

        

        public double ACC_CO_DISCOUNT
        {
            get
            {
                return base.dtModel.Rows[0]["ACC_CO_DISCOUNT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["ACC_CO_DISCOUNT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["ACC_CO_DISCOUNT"] = value;
            }
        }
              
    }
}
