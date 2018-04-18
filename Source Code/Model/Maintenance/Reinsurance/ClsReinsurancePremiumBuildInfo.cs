/******************************************************************************************
<Author					: -		Swarup Kumar Pal
<Start Date				: -		20-Aug-2007 
<End Date				: -	
<Description			: - 	Model for ClsReinsurancePremiumBuildInfo.
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

namespace Cms.Model.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ClsReinsurancePremiumBuildInfo.
	/// </summary>
	public class ClsReinsurancePremiumBuildInfo :Cms.Model.ClsCommonModel
	{
		# region D E C L A R A T I O N 
		public ClsReinsurancePremiumBuildInfo()
		{
			// setting table name for data table that holds property values.
			base.dtModel.TableName = "MNT_REIN_PREMIUM_BUILDER";	

			// add columns of the database table MNT_REIN_PREMIUM_BUILDER
			this.AddColumns();								
			
			// add a blank row in the datatable
			base.dtModel.Rows.Add(base.dtModel.NewRow());
		}
		#endregion

		# region  A D D I N G   C O L U M N S   T O   T A B L E 
		
		private void AddColumns()
		{
			base.dtModel.Columns.Add("PREMIUM_BUILDER_ID",typeof(int)); //1
			base.dtModel.Columns.Add("CONTRACT_ID",typeof(int));//2
			base.dtModel.Columns.Add("CONTRACT",typeof(string));//3
			base.dtModel.Columns.Add("EFFECTIVE_DATE",typeof(DateTime));//4
			base.dtModel.Columns.Add("EXPIRY_DATE",typeof(DateTime));//5
			base.dtModel.Columns.Add("LAYER",typeof(int));//6
			base.dtModel.Columns.Add("COVERAGE_CATEGORY",typeof(string));//7
			base.dtModel.Columns.Add("CALCULATION_BASE",typeof(int));//8
			base.dtModel.Columns.Add("INSURANCE_VALUE",typeof(string));//9
			base.dtModel.Columns.Add("TOTAL_INSURANCE_FROM",typeof(string));//10
			base.dtModel.Columns.Add("TOTAL_INSURANCE_TO",typeof(string));//11
			base.dtModel.Columns.Add("OTHER_INST",typeof(int));//12
			base.dtModel.Columns.Add("RATE_APPLIED",typeof(double));//13
			base.dtModel.Columns.Add("CONSTRUCTION",typeof(int));//14
			base.dtModel.Columns.Add("PROTECTION",typeof(string));//15
			base.dtModel.Columns.Add("ALARM_CREDIT",typeof(int));//16
			base.dtModel.Columns.Add("ALARM_PERCENTAGE",typeof(double));//17
			base.dtModel.Columns.Add("HOME_CREDIT",typeof(int));//18
			base.dtModel.Columns.Add("HOME_AGE",typeof(int));//19
			base.dtModel.Columns.Add("HOME_PERCENTAGE",typeof(double));//20
			base.dtModel.Columns.Add("COMMENTS",typeof(string));//21
			base.dtModel.Columns.Add("COVERAGE_CATEGORY_TLOG",typeof(string));//22
			base.dtModel.Columns.Add("PROTECTION_TLOG",typeof(string));//23

		}

		# endregion 

		#region D A T B A S E   S C H E M A   D E T A I L S 
		// model for database field PREMIUM_BUILDER_ID(int)
		public int PREMIUM_BUILDER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["PREMIUM_BUILDER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PREMIUM_BUILDER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PREMIUM_BUILDER_ID"] = value;
			}
		}

		// model for database field CONTRACT_ID(int)
		public int CONTRACT_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CONTRACT_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT_ID"] = value;
			}
		}

		// model for database field CONTRACT(string)
		public string CONTRACT
		{
			get
			{
				return base.dtModel.Rows[0]["CONTRACT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CONTRACT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONTRACT"] = value;
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

		// model for database field LAYER(int)
		public int LAYER
		{
			get
			{
				return base.dtModel.Rows[0]["LAYER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LAYER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAYER"] = value;
			}
		}

		// model for database field COVERAGE_CATEGORY(string)
		public string COVERAGE_CATEGORY
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_CATEGORY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COVERAGE_CATEGORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_CATEGORY"] = value;
			}
		}


		// model for database field CALCULATION_BASE(int)
		public int CALCULATION_BASE
		{
			get
			{
				return base.dtModel.Rows[0]["CALCULATION_BASE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CALCULATION_BASE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CALCULATION_BASE"] = value;
			}
		}

		// model for database field INSURANCE_VALUE(string)
		public string INSURANCE_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["INSURANCE_VALUE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INSURANCE_VALUE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSURANCE_VALUE"] = value;
			}
		}

		// model for database field TOTAL_INSURANCE_FROM(string)
		public string TOTAL_INSURANCE_FROM
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_INSURANCE_FROM"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TOTAL_INSURANCE_FROM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_INSURANCE_FROM"] = value;
			}
		}

		// model for database field TOTAL_INSURANCE_TO(string)
		public string TOTAL_INSURANCE_TO
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_INSURANCE_TO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TOTAL_INSURANCE_TO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_INSURANCE_TO"] = value;
			}
		}
		// model for database field OTHER_INST(int)
		public int OTHER_INST
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_INST"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["OTHER_INST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_INST"] = value;
			}
		}

		// model for database field RATE_APPLIED(double)
		public double RATE_APPLIED
		{
			get
			{
				return base.dtModel.Rows[0]["RATE_APPLIED"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["RATE_APPLIED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RATE_APPLIED"] = value;
			}
		}

		// model for database field CONSTRUCTION(int)
		public int CONSTRUCTION
		{
			get
			{
				return base.dtModel.Rows[0]["CONSTRUCTION"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CONSTRUCTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONSTRUCTION"] = value;
			}
		}

		// model for database field PROTECTION(string)
		public string PROTECTION
		{
			get
			{
				return base.dtModel.Rows[0]["PROTECTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROTECTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROTECTION"] = value;
			}
		}

		// model for database field ALARM_CREDIT(int)
		public int ALARM_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["ALARM_CREDIT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ALARM_CREDIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ALARM_CREDIT"] = value;
			}
		}

		// model for database field ALARM_PERCENTAGE(double)
		public double ALARM_PERCENTAGE
		{
			get
			{
				return base.dtModel.Rows[0]["ALARM_PERCENTAGE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["ALARM_PERCENTAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ALARM_PERCENTAGE"] = value;
			}
		}

		// model for database field HOME_CREDIT(int)
		public int HOME_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_CREDIT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HOME_CREDIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HOME_CREDIT"] = value;
			}
		}

		// model for database field HOME_AGE(int)
		public int HOME_AGE
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_AGE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HOME_AGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HOME_AGE"] = value;
			}
		}

		// model for database field HOME_PERCENTAGE(double)
		public double HOME_PERCENTAGE
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_PERCENTAGE"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["HOME_PERCENTAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HOME_PERCENTAGE"] = value;
			}
		}


		// model for database field COMMENTS(string)
		public string COMMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["COMMENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COMMENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COMMENTS"] = value;
			}
		}
		// model for database field COVERAGE_CATEGORY_TLOG(string)
		public string COVERAGE_CATEGORY_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_CATEGORY_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["COVERAGE_CATEGORY_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_CATEGORY_TLOG"] = value;
			}
		}
		// model for database field PROTECTION_TLOG(string)
		public string PROTECTION_TLOG
		{
			get
			{
				return base.dtModel.Rows[0]["PROTECTION_TLOG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROTECTION_TLOG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROTECTION_TLOG"] = value;
			}
		}
		# endregion 
	}
}
