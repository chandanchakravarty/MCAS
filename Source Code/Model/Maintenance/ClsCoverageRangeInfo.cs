/******************************************************************************************
<Author				: -   
<Start Date				: -	7/5/2005 11:23:48 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Database Model for MNT_COVERAGE_RANGES.
	/// </summary>
	public class ClsCoverageRangeInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_COVERAGE_RANGES = "MNT_COVERAGE_RANGES";
		public ClsCoverageRangeInfo()
		{
			base.dtModel.TableName = "MNT_COVERAGE_RANGES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_COVERAGE_RANGES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("LIMIT_DEDUC_ID",typeof(int));
			base.dtModel.Columns.Add("COV_ID",typeof(int));
			base.dtModel.Columns.Add("RANK",typeof(int));
			base.dtModel.Columns.Add("LIMIT_DEDUC_TYPE",typeof(string));
			base.dtModel.Columns.Add("LIMIT_DEDUC_AMOUNT",typeof(decimal));
			base.dtModel.Columns.Add("LIMIT_DEDUC_TEXT",typeof(string));
			base.dtModel.Columns.Add("LIMIT_DEDUC_AMOUNT1",typeof(decimal));
			base.dtModel.Columns.Add("LIMIT_DEDUC_TEXT1",typeof(string));
			base.dtModel.Columns.Add("EFFECTIVE_FROM_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EFFECTIVE_TO_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("DISABLED_DATE",typeof(DateTime));
		
		}
		#region Database schema details
		// model for database field LIMIT_DEDUC_ID(int)
		public int LIMIT_DEDUC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_DEDUC_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LIMIT_DEDUC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_DEDUC_ID"] = value;
			}
		}
		// model for database field COV_ID(int)
		public int COV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COV_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COV_ID"] = value;
			}
		}

		public int RANK
		{
			get
			{
				return base.dtModel.Rows[0]["RANK"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RANK"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RANK"] = value;
			}
		}

		// model for database field LIMIT_DEDUC_TYPE(string)
		public string LIMIT_DEDUC_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_DEDUC_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LIMIT_DEDUC_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_DEDUC_TYPE"] = value;
			}
		}
		// model for database field LIMIT_DEDUC_AMOUNT(decimal)
		public decimal LIMIT_DEDUC_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_DEDUC_AMOUNT"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["LIMIT_DEDUC_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_DEDUC_AMOUNT"] = value;
			}
		}

		public string LIMIT_DEDUC_TEXT
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_DEDUC_TEXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LIMIT_DEDUC_TEXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_DEDUC_TEXT"] = value;
			}
		}

		public decimal LIMIT_DEDUC_AMOUNT1
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_DEDUC_AMOUNT1"] == DBNull.Value ? 0 : decimal.Parse(base.dtModel.Rows[0]["LIMIT_DEDUC_AMOUNT1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_DEDUC_AMOUNT1"] = value;
			}
		}

		public string LIMIT_DEDUC_TEXT1
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_DEDUC_TEXT1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LIMIT_DEDUC_TEXT1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_DEDUC_TEXT1"] = value;
			}
		}

		public DateTime EFFECTIVE_FROM_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_FROM_DATE"] = value;
			}
		}

		public DateTime EFFECTIVE_TO_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_TO_DATE"] = value;
			}
		}

		public DateTime DISABLED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DISABLED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DISABLED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DISABLED_DATE"] = value;
			}
		}
		
		#endregion
	}
}

