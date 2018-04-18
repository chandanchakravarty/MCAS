/******************************************************************************************
<Author				: -     Shafi
<Start Date				: -	14/02/06
<End Date				: -	
<Description				: - 	Model Class for Policy Coverage
<Review Date				: - 
<Reviewed By			: - 	
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;


namespace Cms.Model.Policy
{
	/// <summary>
	/// Summary description for ClsPolicyCoveragesInfo.
	/// </summary>
	public class ClsPolicyCoveragesInfo:Cms.Model.ClsCommonModel
	{
		public ClsPolicyCoveragesInfo()
		{
		
			//
			// TODO: Add constructor logic here
			//
			base.dtModel.TableName = "POL_WATERCRAFT_COVERAGE_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_VEHICLE_COVERAGES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("RISK_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_ID",typeof(int));
			base.dtModel.Columns.Add("LIMIT_ID",typeof(int));
			base.dtModel.Columns.Add("DEDUC_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_CODE",typeof(string));
			base.dtModel.Columns.Add("COV_DESC",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_CODE_ID",typeof(int));
			base.dtModel.Columns.Add("LIMIT_OVERRIDE",typeof(string));
			base.dtModel.Columns.Add("LIMIT_1",typeof(double));
			base.dtModel.Columns.Add("LIMIT1_AMOUNT_TEXT",typeof(string));
			base.dtModel.Columns.Add("LIMIT_1_TYPE",typeof(string));
			base.dtModel.Columns.Add("LIMIT_2",typeof(double));
			base.dtModel.Columns.Add("LIMIT2_AMOUNT_TEXT",typeof(string));
			base.dtModel.Columns.Add("LIMIT_2_TYPE",typeof(string));
			base.dtModel.Columns.Add("DEDUCT_OVERRIDE",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_1",typeof(double));
			base.dtModel.Columns.Add("DEDUCTIBLE1_AMOUNT_TEXT",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_1_TYPE",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_2",typeof(double));
			base.dtModel.Columns.Add("DEDUCTIBLE2_AMOUNT_TEXT",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_2_TYPE",typeof(string));
			base.dtModel.Columns.Add("WRITTEN_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("FULL_TERM_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("IS_SYSTEM_COVERAGE",typeof(string));
			base.dtModel.Columns.Add("ACTION",typeof(string));
			base.dtModel.Columns.Add("SECTION_TYPE",typeof(string));
			base.dtModel.Columns.Add("DETAILED_DESC",typeof(string));
			base.dtModel.Columns.Add("SIGNATURE_OBTAINED",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_TYPE",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE",typeof(double));
			base.dtModel.Columns.Add("DEDUCTIBLE_TEXT",typeof(string));
			base.dtModel.Columns.Add("ADDDEDUCTIBLE_ID",typeof(int));
			base.dtModel.Columns.Add("ADD_INFORMATION",typeof(string));
			base.dtModel.Columns.Add("LIMIT_AMOUNT_TEXT",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_AMOUNT_TEXT",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_TYPE_ID",typeof(int));
			base.dtModel.Columns.Add("RATE",typeof(double));

            base.dtModel.Columns.Add("MINIMUM_DEDUCTIBLE", typeof(double));
            base.dtModel.Columns.Add("RI_APPLIES", typeof(string));
            base.dtModel.Columns.Add("DEDUCTIBLE_REDUCES", typeof(string));
            base.dtModel.Columns.Add("INITIAL_RATE", typeof(double));
            base.dtModel.Columns.Add("FINAL_RATE", typeof(double));
            base.dtModel.Columns.Add("AVERAGE_RATE", typeof(string));
            base.dtModel.Columns.Add("INDEMNITY_PERIOD", typeof(int));
			base.dtModel.ExtendedProperties.Add("Coverage", "COV_DESC;");

		}
		#region Database schema details
		// model for database field CUSTOMER_ID(int)
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
		// model for database field POLICY_ID(int)
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
		// model for database field POLICY_VERSION_ID(int)
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
		// model for database field VEHICLE_ID(int)
		public int RISK_ID
		{
			get
			{
				return base.dtModel.Rows[0]["RISK_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["RISK_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RISK_ID"] = value;
			}
		}
		// model for database field COVERAGE_ID(int)
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

		// model for database field COVERAGE_ID(int)
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

		/// <summary>
		/// return ADD_INFORMATION
		/// </summary>
		public string ADD_INFORMATION
		{
			get
			{
				return base.dtModel.Rows[0]["ADD_INFORMATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADD_INFORMATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADD_INFORMATION"] = value;
			}
		}
		// model for database field COVERAGE_ID(int)
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

		// model for database field COVERAGE_CODE_ID(int)
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

		public string COVERAGE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_CODE"] = value;
			}
		}
		
		public string COV_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["COV_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COV_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COV_DESC"] = value;
			}
		}

		// model for database field LIMIT_OVERRIDE(string)
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
		// model for database field LIMIT_1(double)
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

		// model for database field LIMIT_1_TYPE(string)
		public string LIMIT1_AMOUNT_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT1_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LIMIT1_AMOUNT_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT1_AMOUNT_TEXT"] = value;
			}
		}
		
		// model for database field LIMIT_1_TYPE(string)
		public string LIMIT2_AMOUNT_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT2_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LIMIT2_AMOUNT_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT2_AMOUNT_TEXT"] = value;
			}
		}
		
		// model for database field LIMIT_1_TYPE(string)
		public string DEDUCTIBLE1_AMOUNT_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE1_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE1_AMOUNT_TEXT"] = value;
			}
		}
		
		// model for database field LIMIT_1_TYPE(string)
		public string DEDUCTIBLE2_AMOUNT_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE2_AMOUNT_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE2_AMOUNT_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE2_AMOUNT_TEXT"] = value;
			}
		}

	

		// model for database field LIMIT_1_TYPE(string)
		public string LIMIT_1_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_1_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LIMIT_1_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_1_TYPE"] = value;
			}
		}
		// model for database field LIMIT_2(double)
		public double LIMIT_2
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_2"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["LIMIT_2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_2"] = value;
			}
		}
		// model for database field LIMIT_2_TYPE(string)
		public string LIMIT_2_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_2_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LIMIT_2_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_2_TYPE"] = value;
			}
		}
		// model for database field DEDUCT_OVERRIDE(string)
		public string DEDUCT_OVERRIDE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCT_OVERRIDE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCT_OVERRIDE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCT_OVERRIDE"] = value;
			}
		}
		// model for database field DEDUCTIBLE_1(double)
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
        // model for database field DEDUCTIBLE_1(double)
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
        
		// model for database field DEDUCTIBLE_1_TYPE(string)
		public string DEDUCTIBLE_1_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_1_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE_1_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_1_TYPE"] = value;
			}
		}
		// model for database field DEDUCTIBLE_2(double)
		public double DEDUCTIBLE_2
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_2"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE_2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_2"] = value;
			}
		}
		// model for database field DEDUCTIBLE_2_TYPE(string)
		public string DEDUCTIBLE_2_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_2_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE_2_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_2_TYPE"] = value;
			}
		}
		// model for database field WRITTEN_PREMIUM(double)
		public double WRITTEN_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["WRITTEN_PREMIUM"] == DBNull.Value ? double.MinValue : double.Parse(base.dtModel.Rows[0]["WRITTEN_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WRITTEN_PREMIUM"] = value;
			}
		}
		// model for database field FULL_TERM_PREMIUM(double)
		public double FULL_TERM_PREMIUM
		{
			get
			{
                return base.dtModel.Rows[0]["FULL_TERM_PREMIUM"] == DBNull.Value ? double.MinValue : double.Parse(base.dtModel.Rows[0]["FULL_TERM_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FULL_TERM_PREMIUM"] = value;
			}
		}
		// model for database field IS_SYSTEM_COVERAGE(string)
		public string IS_SYSTEM_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SYSTEM_COVERAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_SYSTEM_COVERAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SYSTEM_COVERAGE"] = value;
			}
		}

		// model for database field IS_SYSTEM_COVERAGE(string)
		public string ACTION
		{
			get
			{
				return base.dtModel.Rows[0]["ACTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ACTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ACTION"] = value;
			}
		}
		
		// model for database field IS_SYSTEM_COVERAGE(string)
		public string COVERAGE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_TYPE"] = value;
			}
		}

		public string DETAILED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["DETAILED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DETAILED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DETAILED_DESC"] = value;
			}
		}

		public string SIGNATURE_OBTAINED
		{
			get
			{
				return base.dtModel.Rows[0]["SIGNATURE_OBTAINED"]==DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SIGNATURE_OBTAINED"].ToString();
			}

			set
			{
				base.dtModel.Rows[0]["SIGNATURE_OBTAINED"]=value;	
			}

		}
		
		/*public string LIMIT_AMOUNT_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_AMOUNT_TEXT"]==DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LIMIT_AMOUNT_TEXT"].ToString();
			}

			set
			{
				base.dtModel.Rows[0]["LIMIT_AMOUNT_TEXT"]=value;	
			}

		}

		public string DEDUCTIBLE_AMOUNT_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_AMOUNT_TEXT"]==DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE_AMOUNT_TEXT"].ToString();
			}

			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_AMOUNT_TEXT"]=value;	
			}

		}*/
		// model for database field LIMIT_1(double)
		public double DEDUCTIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE"] = value;
			}
		}
		/// <summary>
		/// return ADDDEDUCTIBLE_ID
		/// </summary>
		public int ADDDEDUCTIBLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ADDDEDUCTIBLE_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["ADDDEDUCTIBLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADDDEDUCTIBLE_ID"] = value;
			}
		}

		// model for database field DEDUCTIBLE_TEXT(string)
		public string DEDUCTIBLE_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_TEXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_TEXT"] = value;
			}
		}
		// model for database field RATE(double)
		public double RATE
		{
			get
			{
				return base.dtModel.Rows[0]["RATE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["RATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RATE"] = value;
			}
		}
		public int COVERAGE_TYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_TYPE_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["COVERAGE_TYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_TYPE_ID"] = value;
			}
		}
        // model for database field RI_APPLIES(string)
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
        // model for database field DEDUCTIBLE_REDUCES(string)
        public string DEDUCTIBLE_REDUCES
		{
			get
			{
                return base.dtModel.Rows[0]["DEDUCTIBLE_REDUCES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE_REDUCES"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["DEDUCTIBLE_REDUCES"] = value;
			}
		}
        // model for database field INITIAL_RATE(double)
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

      // model for database field INITIAL_RATE(double)
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
        // model for database field AVERAGE_RATE(string)
        public string AVERAGE_RATE
		{
			get
			{
                return base.dtModel.Rows[0]["AVERAGE_RATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AVERAGE_RATE"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["AVERAGE_RATE"] = value;
			}
		}
        // model for database field AVERAGE_RATE(string)
        public int IDEMNITY_PERIOD
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
		#endregion
		}
	
}
