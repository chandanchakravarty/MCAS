/******************************************************************************************
<Author				: -   Pradeep
<Start Date				: -	6/15/2005 3:27:24 PM
<End Date				: -	
<Description				: - 	Model class for Recreational Vehicles Coverages
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

namespace Cms.Model.Application.HomeOwners
{
	/// <summary>
	/// Database Model for APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES.
	/// </summary>
	public class ClsRecrVehCoveragesInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES = "APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES";
		public ClsRecrVehCoveragesInfo()
		{
			base.dtModel.TableName = "APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("REC_VEH_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_ID",typeof(int));
			base.dtModel.Columns.Add("COVERAGE_CODE",typeof(string));
			base.dtModel.Columns.Add("LIMIT",typeof(double));
			base.dtModel.Columns.Add("DEDUCTIBLE",typeof(double));
			base.dtModel.Columns.Add("WRITTEN_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("FULL_TERM_PREMIUM",typeof(double));
			base.dtModel.Columns.Add("COVERAGE_UNIQUE_ID",typeof(int));
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
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_ID"] = value;
			}
		}
		// model for database field APP_VERSION_ID(int)
		public int APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		// model for database field REC_VEH_ID(int)
		public int REC_VEH_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REC_VEH_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REC_VEH_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REC_VEH_ID"] = value;
			}
		}
		// model for database field COVERAGE_ID(int)
		public int COVERAGE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVERAGE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_ID"] = value;
			}
		}
		// model for database field COVERAGE_CODE(string)
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
		// model for database field LIMIT(double)
		public double LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LIMIT"] = value;
			}
		}
		// model for database field DEDUCTIBLE(double)
		public double DEDUCTIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE"] == System.DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE"] = value;
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
		// model for database field COVERAGE_UNIQUE_ID(int)
		public int COVERAGE_UNIQUE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_UNIQUE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COVERAGE_UNIQUE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_UNIQUE_ID"] = value;
			}
		}
		#endregion
	}
}
