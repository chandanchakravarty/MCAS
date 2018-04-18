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
using System.Data;
using Cms.Model;


namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Summary description for ClsPolicyDwellingCoverageLimitInfo.
	/// </summary>
	public class ClsPolicyDwellingCoverageLimitInfo : Cms.Model.ClsCommonModel
	{
		
		public ClsPolicyDwellingCoverageLimitInfo()
		{
			base.dtModel.TableName = "POL_DWELLING_COVERAGE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POLICY_DWELLING_COVERAGE
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			//base.dtModel.Columns.Add("FORM",typeof(int));
			//base.dtModel.Columns.Add("FORM_OTHER_DESC",typeof(string));
			//base.dtModel.Columns.Add("COVERAGE",typeof(int));
			//base.dtModel.Columns.Add("COVERAGE_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("DWELLING_LIMIT",typeof(double));
			//base.dtModel.Columns.Add("DWELLING_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("DWELLING_REPLACE_COST",typeof(string));
			base.dtModel.Columns.Add("OTHER_STRU_LIMIT",typeof(double));
			base.dtModel.Columns.Add("OTHER_STRU_DESC",typeof(string));
			base.dtModel.Columns.Add("PERSONAL_PROP_LIMIT",typeof(double));
			base.dtModel.Columns.Add("REPLACEMENT_COST_CONTS",typeof(string));
			base.dtModel.Columns.Add("LOSS_OF_USE",typeof(double));
			//base.dtModel.Columns.Add("LOSS_OF_USE_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("PERSONAL_LIAB_LIMIT",typeof(double));
			//base.dtModel.Columns.Add("PERSONAL_LIAB_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("MED_PAY_EACH_PERSON",typeof(double));
			//base.dtModel.Columns.Add("MED_PAY_EACH_PERSON_PREMIUM",typeof(double));
			//base.dtModel.Columns.Add("INFLATION_GUARD",typeof(string));
			base.dtModel.Columns.Add("ALL_PERILL_DEDUCTIBLE_AMT",typeof(double));
			//base.dtModel.Columns.Add("WIND_HAIL_DEDUCTIBLE_AMT",typeof(double));
			base.dtModel.Columns.Add("THEFT_DEDUCTIBLE_AMT",typeof(double));
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
		// model for database field DWELLING_ID(int)
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
		// model for database field FORM(int)
		//		public int FORM
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["FORM"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["FORM"].ToString());
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["FORM"] = value;
		//			}
		//		}
		//		// model for database field FORM_OTHER_DESC(string)
		//		public string FORM_OTHER_DESC
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["FORM_OTHER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FORM_OTHER_DESC"].ToString();
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["FORM_OTHER_DESC"] = value;
		//			}
		//		}
		//		// model for database field COVERAGE(int)
		//		public int COVERAGE
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["COVERAGE"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["COVERAGE"].ToString());
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["COVERAGE"] = value;
		//			}
		//		}
		//		// model for database field COVERAGE_OTHER_DESC(string)
		//		public string COVERAGE_OTHER_DESC
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["COVERAGE_OTHER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_OTHER_DESC"].ToString();
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["COVERAGE_OTHER_DESC"] = value;
		//			}
		//		}
		// model for database field DWELLING_LIMIT(double)
		public double DWELLING_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DWELLING_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_LIMIT"] = value;
			}
		}
		// model for database field DWELLING_PREMIUM(double)
		//		public double DWELLING_PREMIUM
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["DWELLING_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DWELLING_PREMIUM"].ToString());
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["DWELLING_PREMIUM"] = value;
		//			}
		//		}
		// model for database field DWELLING_REPLACE_COST(string)
		public string DWELLING_REPLACE_COST
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_REPLACE_COST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DWELLING_REPLACE_COST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_REPLACE_COST"] = value;
			}
		}
		// model for database field OTHER_STRU_LIMIT(double)
		public double OTHER_STRU_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_STRU_LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["OTHER_STRU_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_STRU_LIMIT"] = value;
			}
		}
		// model for database field OTHER_STRU_DESC(string)
		public string OTHER_STRU_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_STRU_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_STRU_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_STRU_DESC"] = value;
			}
		}
		// model for database field PERSONAL_PROP_LIMIT(double)
		public double PERSONAL_PROP_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["PERSONAL_PROP_LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["PERSONAL_PROP_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERSONAL_PROP_LIMIT"] = value;
			}
		}
		// model for database field REPLACEMENT_COST_CONTS(string)
		public string REPLACEMENT_COST_CONTS
		{
			get
			{
				return base.dtModel.Rows[0]["REPLACEMENT_COST_CONTS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REPLACEMENT_COST_CONTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REPLACEMENT_COST_CONTS"] = value;
			}
		}
		// model for database field LOSS_OF_USE(string)
		public double LOSS_OF_USE
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_OF_USE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["LOSS_OF_USE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_OF_USE"] = value;
			}
		}
		// model for database field LOSS_OF_USE_PREMIUM(double)
		//		public double LOSS_OF_USE_PREMIUM
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["LOSS_OF_USE_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["LOSS_OF_USE_PREMIUM"].ToString());
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["LOSS_OF_USE_PREMIUM"] = value;
		//			}
		//		}
		// model for database field PERSONAL_LIAB_LIMIT(double)
		public double PERSONAL_LIAB_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["PERSONAL_LIAB_LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["PERSONAL_LIAB_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERSONAL_LIAB_LIMIT"] = value;
			}
		}
		// model for database field PERSONAL_LIAB_PREMIUM(double)
		//		public double PERSONAL_LIAB_PREMIUM
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["PERSONAL_LIAB_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["PERSONAL_LIAB_PREMIUM"].ToString());
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["PERSONAL_LIAB_PREMIUM"] = value;
		//			}
		//		}
		// model for database field MED_PAY_EACH_PERSON(double)
		public double MED_PAY_EACH_PERSON
		{
			get
			{
				return base.dtModel.Rows[0]["MED_PAY_EACH_PERSON"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["MED_PAY_EACH_PERSON"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MED_PAY_EACH_PERSON"] = value;
			}
		}
		// model for database field MED_PAY_EACH_PERSON_PREMIUM(double)
		//		public double MED_PAY_EACH_PERSON_PREMIUM
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["MED_PAY_EACH_PERSON_PREMIUM"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["MED_PAY_EACH_PERSON_PREMIUM"].ToString());
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["MED_PAY_EACH_PERSON_PREMIUM"] = value;
		//			}
		//		}
		// model for database field INFLATION_GUARD(string)
		//		public string INFLATION_GUARD
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["INFLATION_GUARD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INFLATION_GUARD"].ToString();
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["INFLATION_GUARD"] = value;
		//			}
		//		}
		// model for database field ALL_PERILL_DEDUCTIBLE_AMT(double)
		public double ALL_PERILL_DEDUCTIBLE_AMT
		{
			get
			{
				return base.dtModel.Rows[0]["ALL_PERILL_DEDUCTIBLE_AMT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["ALL_PERILL_DEDUCTIBLE_AMT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ALL_PERILL_DEDUCTIBLE_AMT"] = value;
			}
		}
		// model for database field WIND_HAIL_DEDUCTIBLE_AMT(double)
		//		public double WIND_HAIL_DEDUCTIBLE_AMT
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["WIND_HAIL_DEDUCTIBLE_AMT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["WIND_HAIL_DEDUCTIBLE_AMT"].ToString());
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["WIND_HAIL_DEDUCTIBLE_AMT"] = value;
		//			}
		//		}
		// model for database field THEFT_DEDUCTIBLE_AMT(double)
		public double THEFT_DEDUCTIBLE_AMT
		{
			get
			{
				return base.dtModel.Rows[0]["THEFT_DEDUCTIBLE_AMT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["THEFT_DEDUCTIBLE_AMT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["THEFT_DEDUCTIBLE_AMT"] = value;
			}
		}
		#endregion

	}
}
