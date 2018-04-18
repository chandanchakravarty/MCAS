/******************************************************************************************
<Author					: -		Pradeep Iyer
<Start Date				: -	11/10/2005 5:31:45 PM
<End Date				: -	
<Description			: - 	Model class for schedule items and coverage.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.HomeOwners
{
	/// <summary>
	/// Database Model for APP_HOME_OWNER_SCH_ITEMS_CVGS.
	/// </summary>
	public class ClsSchItemsCovgInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_HOME_OWNER_SCH_ITEMS_CVGS = "APP_HOME_OWNER_SCH_ITEMS_CVGS";
		private string strAction="";
		public ClsSchItemsCovgInfo()
		{
			base.dtModel.TableName = "APP_HOME_OWNER_SCH_ITEMS_CVGS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_SCH_ITEMS_CVGS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ITEM_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_CODE_ID",typeof(int));
			base.dtModel.Columns.Add("CATEGORY",typeof(string));
			base.dtModel.Columns.Add("CATEGORY_CODE",typeof(string));
			base.dtModel.Columns.Add("DETAILED_DESC",typeof(string));
			base.dtModel.Columns.Add("SN_DETAILS",typeof(string));
			base.dtModel.Columns.Add("AMOUNT_OF_INSURANCE",typeof(double));
			base.dtModel.Columns.Add("PREMIUM",typeof(double));
			base.dtModel.Columns.Add("RATE",typeof(double));
			base.dtModel.Columns.Add("APPRAISAL",typeof(string));
			base.dtModel.Columns.Add("PURCHASE_APPRAISAL_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("BREAKAGE_COVERAGE",typeof(string));
			base.dtModel.Columns.Add("APPRAISAL_DESC",typeof(string));	
			base.dtModel.Columns.Add("BREAKAGE_DESC",typeof(string));
			base.dtModel.Columns.Add("COV_DESC",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE",typeof(double));
		}
		#region Database schema details

		public string Action
		{
			get
			{
				return strAction.ToString().ToUpper().Trim();
			}
			set
			{
				strAction=value;
			}
		}
		// model for database field DEDUCTIBLE(double)
		public double DEDUCTIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE"] = value;
			}
		}
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field ITEM_ID(int)
		public int ITEM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ITEM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ITEM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ITEM_ID"] = value;
			}
		}

		// model for database field ITEM_ID(int)
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
		
		// model for database field ITEM_ID(int)
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

		// model for database field CATEGORY(string)
		public string CATEGORY
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGORY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CATEGORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CATEGORY"] = value;
			}
		}

		// model for database field CATEGORY(string)
		public string CATEGORY_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CATEGORY_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CATEGORY_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CATEGORY_CODE"] = value;
			}
		}

		// model for database field DETAILED_DESC(string)
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
		// model for database field SN_DETAILS(string)
		public string SN_DETAILS
		{
			get
			{
				return base.dtModel.Rows[0]["SN_DETAILS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SN_DETAILS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SN_DETAILS"] = value;
			}
		}
		// model for database field AMOUNT_OF_INSURANCE(double)
		public double AMOUNT_OF_INSURANCE
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT_OF_INSURANCE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["AMOUNT_OF_INSURANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT_OF_INSURANCE"] = value;
			}
		}
		// model for database field PREMIUM(double)
		public double PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["PREMIUM"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PREMIUM"] = value;
			}
		}
		// model for database field RATE(double)
		public double RATE
		{
			get
			{
				return base.dtModel.Rows[0]["RATE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["RATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RATE"] = value;
			}
		}
		// model for database field APPRAISAL(string)
		public string APPRAISAL
		{
			get
			{
				return base.dtModel.Rows[0]["APPRAISAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APPRAISAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPRAISAL"] = value;
			}
		}
		// model for database field PURCHASE_APPRAISAL_DATE(DateTime)
		public DateTime PURCHASE_APPRAISAL_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["PURCHASE_APPRAISAL_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["PURCHASE_APPRAISAL_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PURCHASE_APPRAISAL_DATE"] = value;
			}
		}
		// model for database field BREAKAGE_COVERAGE(string)
		public string BREAKAGE_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["BREAKAGE_COVERAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BREAKAGE_COVERAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BREAKAGE_COVERAGE"] = value;
			}
		}
		public string APPRAISAL_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["APPRAISAL_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APPRAISAL_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPRAISAL_DESC"] = value;
			}
		}
		public string BREAKAGE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["BREAKAGE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BREAKAGE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BREAKAGE_DESC"] = value;
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

		#endregion
	}
}
