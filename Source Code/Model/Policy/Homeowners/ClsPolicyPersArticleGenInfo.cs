/******************************************************************************************
<Author					: -   Pradeep
<Start Date				: -		11/14/2005 3:33:58 PM
<End Date				: -	
<Description			: - 	Model class for personal Policy item general information.
<Review Date			: - 
<Reviewed By			: - 	

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Database Model for APP_HOME_OWNER_PER_ART_GEN_INFO.
	/// </summary>
	public class ClsPolicyPersArticleGenInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_HOME_OWNER_PER_ART_GEN_INFO = "APP_HOME_OWNER_PER_ART_GEN_INFO";
		public ClsPolicyPersArticleGenInfo()
		{
			base.dtModel.TableName = "APP_HOME_OWNER_PER_ART_GEN_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_PER_ART_GEN_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("PROPERTY_EXHIBITED",typeof(string));
			base.dtModel.Columns.Add("DESC_PROPERTY_EXHIBITED",typeof(string));
			base.dtModel.Columns.Add("DEDUCTIBLE_APPLY",typeof(string));
			base.dtModel.Columns.Add("DESC_DEDUCTIBLE_APPLY",typeof(string));
			base.dtModel.Columns.Add("PROPERTY_USE_PROF_COMM",typeof(string));
			base.dtModel.Columns.Add("OTHER_INSU_WITH_COMPANY",typeof(string));
			base.dtModel.Columns.Add("DESC_INSU_WITH_COMPANY",typeof(string));
			base.dtModel.Columns.Add("LOSS_OCCURED_LAST_YEARS",typeof(string));
			base.dtModel.Columns.Add("DESC_LOSS_OCCURED_LAST_YEARS",typeof(string));
			base.dtModel.Columns.Add("DECLINED_CANCELED_COVERAGE",typeof(string));
			base.dtModel.Columns.Add("ADD_RATING_COV_INFO",typeof(string));
			// Added by mohit on 20/10/2005.
			base.dtModel.Columns.Add("DESC_PROPERTY_USE_PROF_COMM",typeof(string));

		}
		#region Database schema details
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
		// model for database field PROPERTY_EXHIBITED(string)
		public string PROPERTY_EXHIBITED
		{
			get
			{
				return base.dtModel.Rows[0]["PROPERTY_EXHIBITED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROPERTY_EXHIBITED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROPERTY_EXHIBITED"] = value;
			}
		}
		// model for database field DESC_PROPERTY_EXHIBITED(string)
		public string DESC_PROPERTY_EXHIBITED
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_PROPERTY_EXHIBITED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_PROPERTY_EXHIBITED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_PROPERTY_EXHIBITED"] = value;
			}
		}
		// model for database field DEDUCTIBLE_APPLY(string)
		public string DEDUCTIBLE_APPLY
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_APPLY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEDUCTIBLE_APPLY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_APPLY"] = value;
			}
		}
		// model for database field DESC_DEDUCTIBLE_APPLY(string)
		public string DESC_DEDUCTIBLE_APPLY
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_DEDUCTIBLE_APPLY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_DEDUCTIBLE_APPLY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_DEDUCTIBLE_APPLY"] = value;
			}
		}
		// model for database field PROPERTY_USE_PROF_COMM(string)
		public string PROPERTY_USE_PROF_COMM
		{
			get
			{
				return base.dtModel.Rows[0]["PROPERTY_USE_PROF_COMM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROPERTY_USE_PROF_COMM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROPERTY_USE_PROF_COMM"] = value;
			}
		}
		// model for database field OTHER_INSU_WITH_COMPANY(string)
		public string OTHER_INSU_WITH_COMPANY
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_INSU_WITH_COMPANY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_INSU_WITH_COMPANY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_INSU_WITH_COMPANY"] = value;
			}
		}
		// model for database field DESC_INSU_WITH_COMPANY(string)
		public string DESC_INSU_WITH_COMPANY
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_INSU_WITH_COMPANY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_INSU_WITH_COMPANY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_INSU_WITH_COMPANY"] = value;
			}
		}
		// model for database field LOSS_OCCURED_LAST_YEARS(string)
		public string LOSS_OCCURED_LAST_YEARS
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_OCCURED_LAST_YEARS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOSS_OCCURED_LAST_YEARS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_OCCURED_LAST_YEARS"] = value;
			}
		}
		// model for database field DESC_LOSS_OCCURED_LAST_YEARS(string)
		public string DESC_LOSS_OCCURED_LAST_YEARS
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_LOSS_OCCURED_LAST_YEARS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_LOSS_OCCURED_LAST_YEARS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_LOSS_OCCURED_LAST_YEARS"] = value;
			}
		}
		// model for database field DECLINED_CANCELED_COVERAGE(string)
		public string DECLINED_CANCELED_COVERAGE
		{
			get
			{
				return base.dtModel.Rows[0]["DECLINED_CANCELED_COVERAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DECLINED_CANCELED_COVERAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DECLINED_CANCELED_COVERAGE"] = value;
			}
		}
		// model for database field ADD_RATING_COV_INFO(string)
		public string ADD_RATING_COV_INFO
		{
			get
			{
				return base.dtModel.Rows[0]["ADD_RATING_COV_INFO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADD_RATING_COV_INFO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADD_RATING_COV_INFO"] = value;
			}
		}
		// model for database field DESC_PROPERTY_USE_PROF_COMM(string)
		public string DESC_PROPERTY_USE_PROF_COMM
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_PROPERTY_USE_PROF_COMM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_PROPERTY_USE_PROF_COMM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_PROPERTY_USE_PROF_COMM"] = value;
			}
		}
		#endregion
	}
}
