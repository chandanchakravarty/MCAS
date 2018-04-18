using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Application
{
	//Class added by Charles on 24-Dec-09 for Itrack 6830
	/// <summary>
	/// Database Model for APP_UNDERWRITING_TIER.
	/// </summary>
	public class ClsUnderwritingTierInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_AUTO_GEN_INFO = "APP_UNDERWRITING_TIER";
		public ClsUnderwritingTierInfo()
		{
			base.dtModel.TableName = APP_AUTO_GEN_INFO;		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_UNDERWRITING_TIER
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));		
			base.dtModel.Columns.Add("UNDERWRITING_TIER",typeof(string));
			base.dtModel.Columns.Add("UNTIER_ASSIGNED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CAP_INC",typeof(string));			
			base.dtModel.Columns.Add("CAP_DEC",typeof(string));	
			base.dtModel.Columns.Add("CAP_RATE_CHANGE_REL",typeof(double));	
			base.dtModel.Columns.Add("CAP_MIN_MAX_ADJUST",typeof(double));	
			base.dtModel.Columns.Add("ACL_PREMIUM",typeof(double));		
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
		
		public string UNDERWRITING_TIER
		{
			get
			{
				return base.dtModel.Rows[0]["UNDERWRITING_TIER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["UNDERWRITING_TIER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNDERWRITING_TIER"] = value;
			}
		}

		public DateTime UNTIER_ASSIGNED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["UNTIER_ASSIGNED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["UNTIER_ASSIGNED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNTIER_ASSIGNED_DATE"] = value;
			}
		}
		
		public string CAP_INC
		{
			get
			{
				return base.dtModel.Rows[0]["CAP_INC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAP_INC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CAP_INC"] = value;
			}
		}

		public string CAP_DEC
		{
			get
			{
				return base.dtModel.Rows[0]["CAP_DEC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAP_DEC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CAP_DEC"] = value;
			}
		}

		public double CAP_RATE_CHANGE_REL
		{
			get
			{
				return base.dtModel.Rows[0]["CAP_RATE_CHANGE_REL"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["CAP_RATE_CHANGE_REL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CAP_RATE_CHANGE_REL"] = value;
			}
		}

		public double CAP_MIN_MAX_ADJUST
		{
			get
			{
				return base.dtModel.Rows[0]["CAP_MIN_MAX_ADJUST"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["CAP_MIN_MAX_ADJUST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CAP_MIN_MAX_ADJUST"] = value;
			}
		}

		public double ACL_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["ACL_PREMIUM"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["ACL_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ACL_PREMIUM"] = value;
			}
		}
		#endregion
	}
	//Added till here
}
