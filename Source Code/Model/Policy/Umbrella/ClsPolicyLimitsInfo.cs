/******************************************************************************************
<Author					: -  Ravindra Kumar Gupta
<Start Date				: -	 03-20- 2006
<End Date				: -	
<Description			: -  Model Class for POL_UMBRELLA_LIMITS
<Review Date			: - 
<Reviewed By			: - 	
Modification History
************************************************************************************************/
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Umbrella
{
	/// <summary>
	/// Database Model for POL_UMBRELLA_LIMITS.
	/// </summary>
	public class ClsPolicyLimitsInfo : Cms.Model.ClsCommonModel
	{
		#region Constructor
		public ClsPolicyLimitsInfo()
		{
			base.dtModel.TableName = "POL_UMBRELLA_LIMITS";		
			this.AddColumns();								
			base.dtModel.Rows.Add(base.dtModel.NewRow());
		}
		#endregion

		#region AddColumns Function
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_LIMITS",typeof(double));
			base.dtModel.Columns.Add("RETENTION_LIMITS",typeof(double));
			base.dtModel.Columns.Add("UNINSURED_MOTORIST_LIMIT",typeof(double));
			base.dtModel.Columns.Add("UNDERINSURED_MOTORIST_LIMIT",typeof(double));
			base.dtModel.Columns.Add("OTHER_LIMIT",typeof(double));
			base.dtModel.Columns.Add("OTHER_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("BASIC",typeof(double));
			base.dtModel.Columns.Add("RESIDENCES_OWNER_OCCUPIED",typeof(double));
			base.dtModel.Columns.Add("NUM_OF_RENTAL_UNITS",typeof(int));
			base.dtModel.Columns.Add("RENTAL_UNITS",typeof(double));
			base.dtModel.Columns.Add("NUM_OF_AUTO",typeof(int));
			base.dtModel.Columns.Add("AUTOMOBILES",typeof(double));
			base.dtModel.Columns.Add("NUM_OF_OPERATORS",typeof(int));
			base.dtModel.Columns.Add("OPER_UNDER_AGE",typeof(double));
			base.dtModel.Columns.Add("NUM_OF_UNLIC_RV",typeof(int));
			base.dtModel.Columns.Add("UNLIC_RV",typeof(double));
			base.dtModel.Columns.Add("NUM_OF_UNINSU_MOTORIST",typeof(int));
			base.dtModel.Columns.Add("UNISU_MOTORIST",typeof(double));
			base.dtModel.Columns.Add("UNDER_INSURED_MOTORIST",typeof(double));
			base.dtModel.Columns.Add("WATERCRAFT",typeof(double));
			base.dtModel.Columns.Add("NUM_OF_OTHER",typeof(int));
			base.dtModel.Columns.Add("OTHER",typeof(double));
			base.dtModel.Columns.Add("DEPOSIT",typeof(double));
			base.dtModel.Columns.Add("ESTIMATED_TOTAL_PRE",typeof(double));
			base.dtModel.Columns.Add("CALCULATIONS",typeof(string));
			base.dtModel.Columns.Add("TERRITORY",typeof(int));
			base.dtModel.Columns.Add("CLIENT_UPDATE_DATE",typeof(DateTime));

		}
		#endregion

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
		// model for database field POLICY_ID(int)
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
		// model for database field POLICY_VERSION_ID(int)
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
		// model for database field POLICY_LIMITS(double)
		public double POLICY_LIMITS
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_LIMITS"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["POLICY_LIMITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_LIMITS"] = value;
			}
		}
		// model for database field RETENTION_LIMITS(double)
		public double RETENTION_LIMITS
		{
			get
			{
				return base.dtModel.Rows[0]["RETENTION_LIMITS"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["RETENTION_LIMITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RETENTION_LIMITS"] = value;
			}
		}
		// model for database field UNINSURED_MOTORIST_LIMIT(double)
		public double UNINSURED_MOTORIST_LIMIT
		{
			get
			{
				//return base.dtModel.Rows[0]["UNINSURED_MOTORIST_LIMIT"];
				return base.dtModel.Rows[0]["UNINSURED_MOTORIST_LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["UNINSURED_MOTORIST_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNINSURED_MOTORIST_LIMIT"] = value;
			}
		}
		// model for database field UNDERINSURED_MOTORIST_LIMIT(double)
		public double UNDERINSURED_MOTORIST_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["UNDERINSURED_MOTORIST_LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["UNDERINSURED_MOTORIST_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNDERINSURED_MOTORIST_LIMIT"] = value;
			}
		}
		// model for database field OTHER_LIMIT(double)
		public double OTHER_LIMIT
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_LIMIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["OTHER_LIMIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_LIMIT"] = value;
			}
		}
		// model for database field OTHER_DESCRIPTION(string)
		public string OTHER_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DESCRIPTION"] = value;
			}
		}
		// model for database field BASIC(double)
		public double BASIC
		{
			get
			{
				return base.dtModel.Rows[0]["BASIC"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["BASIC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BASIC"] = value;
			}
		}
		// model for database field RESIDENCES_OWNER_OCCUPIED(double)
		public double RESIDENCES_OWNER_OCCUPIED
		{
			get
			{
				return base.dtModel.Rows[0]["RESIDENCES_OWNER_OCCUPIED"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["RESIDENCES_OWNER_OCCUPIED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESIDENCES_OWNER_OCCUPIED"] = value;
			}
		}
		// model for database field NUM_OF_RENTAL_UNITS(int)
		public int NUM_OF_RENTAL_UNITS
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_OF_RENTAL_UNITS"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NUM_OF_RENTAL_UNITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_OF_RENTAL_UNITS"] = value;
			}
		}
		// model for database field RENTAL_UNITS(double)
		public double RENTAL_UNITS
		{
			get
			{
				return base.dtModel.Rows[0]["RENTAL_UNITS"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["RENTAL_UNITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RENTAL_UNITS"] = value;
			}
		}
		// model for database field NUM_OF_AUTO(int)
		public int NUM_OF_AUTO
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_OF_AUTO"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NUM_OF_AUTO"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_OF_AUTO"] = value;
			}
		}
		// model for database field AUTOMOBILES(double)
		public double AUTOMOBILES
		{
			get
			{
				return base.dtModel.Rows[0]["AUTOMOBILES"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["AUTOMOBILES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AUTOMOBILES"] = value;
			}
		}
		// model for database field NUM_OF_OPERATORS(int)
		public int NUM_OF_OPERATORS
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_OF_OPERATORS"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NUM_OF_OPERATORS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_OF_OPERATORS"] = value;
			}
		}
		// model for database field OPER_UNDER_AGE(double)
		public double OPER_UNDER_AGE
		{
			get
			{
				return base.dtModel.Rows[0]["OPER_UNDER_AGE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["OPER_UNDER_AGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OPER_UNDER_AGE"] = value;
			}
		}
		// model for database field NUM_OF_UNLIC_RV(int)
		public int NUM_OF_UNLIC_RV
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_OF_UNLIC_RV"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NUM_OF_UNLIC_RV"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_OF_UNLIC_RV"] = value;
			}
		}
		// model for database field UNLIC_RV(double)
		public double UNLIC_RV
		{
			get
			{
				return base.dtModel.Rows[0]["UNLIC_RV"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["UNLIC_RV"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNLIC_RV"] = value;
			}
		}
		// model for database field NUM_OF_UNINSU_MOTORIST(int)
		public int NUM_OF_UNINSU_MOTORIST
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_OF_UNINSU_MOTORIST"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NUM_OF_UNINSU_MOTORIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_OF_UNINSU_MOTORIST"] = value;
			}
		}
		// model for database field UNISU_MOTORIST(double)
		public double UNISU_MOTORIST
		{
			get
			{
				return base.dtModel.Rows[0]["UNISU_MOTORIST"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["UNISU_MOTORIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNISU_MOTORIST"] = value;
			}
		}
		// model for database field UNDER_INSURED_MOTORIST(int)
		public double UNDER_INSURED_MOTORIST
		{
			get
			{
				return base.dtModel.Rows[0]["UNDER_INSURED_MOTORIST"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["UNDER_INSURED_MOTORIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["UNDER_INSURED_MOTORIST"] = value;
			}
		}
		// model for database field WATERCRAFT(double)
		public double WATERCRAFT
		{
			get
			{
				return base.dtModel.Rows[0]["WATERCRAFT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["WATERCRAFT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WATERCRAFT"] = value;
			}
		}
		// model for database field NUM_OF_OTHER(int)
		public int NUM_OF_OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_OF_OTHER"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NUM_OF_OTHER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_OF_OTHER"] = value;
			}
		}
		// model for database field OTHER(double)
		public double OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["OTHER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OTHER"] = value;
			}
		}
		// model for database field DEPOSIT(double)
		public double DEPOSIT
		{
			get
			{
				return base.dtModel.Rows[0]["DEPOSIT"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DEPOSIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEPOSIT"] = value;
			}
		}
		// model for database field ESTIMATED_TOTAL_PRE(double)
		public double ESTIMATED_TOTAL_PRE
		{
			get
			{
				return base.dtModel.Rows[0]["ESTIMATED_TOTAL_PRE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["ESTIMATED_TOTAL_PRE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ESTIMATED_TOTAL_PRE"] = value;
			}
		}
		// model for database field CALCULATIONS(double)
		public string CALCULATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["CALCULATIONS"] == DBNull.Value ? Convert.ToString(null) :base.dtModel.Rows[0]["CALCULATIONS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CALCULATIONS"] = value;
			}
		}
		// model for database field TERRITORY(int)
		public int TERRITORY
		{
			get
			{
				return base.dtModel.Rows[0]["TERRITORY"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["TERRITORY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TERRITORY"] = value;
			}
		}
		// model for database field CLIENT_UPDATE_DATE(DateTime)
		public DateTime CLIENT_UPDATE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["CLIENT_UPDATE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["CLIENT_UPDATE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLIENT_UPDATE_DATE"] = value;
			}
		}
		#endregion
	}
}
