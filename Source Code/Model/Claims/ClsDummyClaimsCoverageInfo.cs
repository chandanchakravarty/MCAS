/******************************************************************************************
<Author					: - Mohit Agarwal
<Start Date				: -	5-Dec-2007
<End Date				: -	
<Description			: - Models MNT_CLAIM_COVERAGE
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

namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for MNT_CLAIM_COVERAGE.
	/// </summary>
	public class ClsDummyClaimsCoverageInfo : Cms.Model.ClsCommonModel
	{
		private const string MNT_CLAIM_COVERAGE = "MNT_CLAIM_COVERAGE";
		public ClsDummyClaimsCoverageInfo()
		{
			base.dtModel.TableName = "MNT_CLAIM_COVERAGE";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_DUMMY_POLICY
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{

			base.dtModel.Columns.Add("COV_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));
			base.dtModel.Columns.Add("COV_DES",typeof(string));
			base.dtModel.Columns.Add("STATE_ID",typeof(string));	
			base.dtModel.Columns.Add("LOB_ID",typeof(string));	
			base.dtModel.Columns.Add("LIMIT_1",typeof(double));	
			base.dtModel.Columns.Add("DEDUCTIBLE_1",typeof(double));
//			base.dtModel.Columns.Add("IS_ACTIVE",typeof(string));
//			base.dtModel.Columns.Add("CREATED_BY",typeof(int));
//			base.dtModel.Columns.Add("CREATED_DATETIME",typeof(DateTime));			
//			base.dtModel.Columns.Add("MODIFIED_BY",typeof(int));
//			base.dtModel.Columns.Add("LAST_UPDATED_DATETIME",typeof(DateTime));			
		}
		#region Database schema details
		// model for database field COV_ID(int)
		public int COV_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COV_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COV_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COV_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
		// model for database field COV_DES(string)
		public string COV_DES
		{
			get
			{
				return base.dtModel.Rows[0]["COV_DES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COV_DES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COV_DES"] = value;
			}
		}
		// model for database field STATE_ID(string)
		public string STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}
		// model for database field LOB_ID(string)
		public string LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOB_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}
		// model for database field LIMIT_1(double)
		public double LIMIT_1
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT_1"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["LIMIT_1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT_1"] = value;
			}
		}
		// model for database field DEDUCTIBLE_1(double)
		public double DEDUCTIBLE_1
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE_1"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE_1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE_1"] = value;
			}
		}
		#endregion
	}
}
