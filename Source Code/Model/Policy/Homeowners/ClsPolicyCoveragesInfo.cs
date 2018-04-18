/******************************************************************************************
<Author				: -   Anurag verma
<Start Date				: -	14/11/2005 3:14:03 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
*******************************************************************************************/
using System;

namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Summary description for ClsPolicyCoveragesInfo.
	/// </summary>
	public class ClsPolicyCoveragesInfo: Cms.Model.ClsCommonModel
	{
		public ClsPolicyCoveragesInfo()
		{
				base.dtModel.TableName = "POL_DWELLING_SECTION_COVERAGES";		// setting table name for data table that holds property values.
				this.AddColumns();								// add columns of the database table APP_DWELLING_COVERAGE
				base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_CODE_ID",typeof(string));
			base.dtModel.Columns.Add("LIMIT_OVERRIDE",typeof(string));
			base.dtModel.Columns.Add("LIMIT_1",typeof(double));
			base.dtModel.Columns.Add("LIMIT_1_TYPE",typeof(string));
			base.dtModel.Columns.Add("LIMIT_2",typeof(double));
			base.dtModel.Columns.Add("LIMIT_2_TYPE",typeof(string));
			base.dtModel.Columns.Add("DEDUCT_OVERRIDE",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_1",typeof(double));
			base.dtModel.Columns.Add("DEDUCTIBLE_1_TYPE",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_2",typeof(double));
			base.dtModel.Columns.Add("DEDUCTIBLE_2_TYPE",typeof(string));
			base.dtModel.Columns.Add("WRITTEN_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("FULL_TERM_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("IS_SYSTEM_COVERAGE",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_TYPE",typeof(string));
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
		// model for database field APP_ID(int)
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
		// model for database field APP_VERSION_ID(int)
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
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
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
				return base.dtModel.Rows[0]["WRITTEN_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["WRITTEN_PREMIUM"].ToString());
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
				return base.dtModel.Rows[0]["FULL_TERM_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["FULL_TERM_PREMIUM"].ToString());
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

		#endregion
	}
}
