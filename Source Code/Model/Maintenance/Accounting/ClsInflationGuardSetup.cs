using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Summary description for InflationGuardSetup.
	/// </summary>
	public class ClsInflationGuardSetup : Cms.Model.ClsCommonModel
	{
		private const string INFLATION_COST_FACTORS = "INFLATION_COST_FACTORS";
		public ClsInflationGuardSetup()
		{
			base.dtModel.TableName = "INFLATION_COST_FACTORS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_REG_COMM_SETUP
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("INFLATION_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_ID",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(int));
			base.dtModel.Columns.Add("ZIP_CODE",typeof(int));
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("EXPIRY_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("FACTOR",typeof(double));
		}

		#region Database schema details
		
		// model for database field INFLATION_ID(int)
		public int INFLATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["INFLATION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INFLATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INFLATION_ID"] = value;
			}
		}
		// model for database field STATE_ID(int)
		public int STATE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_ID"] = value;
			}
		}
		// model for database field LOB_ID(int)
		public int LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOB_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}
		// model for database field ZIP_CODE(int)
		public int ZIP_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["ZIP_CODE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ZIP_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ZIP_CODE"] = value;
			}
		}
		// model for database field EFFECTIVE_DATE(DateTime)
		public DateTime EFFECTIVE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EFFECTIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EFFECTIVE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EFFECTIVE_DATE"] = value;
			}
		}
		// model for database field EXPIRY_DATE(DateTime)
		public DateTime EXPIRY_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["EXPIRY_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["EXPIRY_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPIRY_DATE"] = value;
			}
		}
		// model for database field FACTOR(double)
		public double FACTOR
		{
			get
			{
				return base.dtModel.Rows[0]["FACTOR"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["FACTOR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FACTOR"] = value;
			}
		}
		#endregion
	}
}
