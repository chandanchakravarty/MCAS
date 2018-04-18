/******************************************************************************************
<Author					: -   Pradeep Iyer
<Start Date				: -	  5/23/2005 3:02:03 PM
<End Date				: -	
<Description			: -  Model class for POL_HOME_OWNER_RECREATIONAL_VEHICLES	
<Review Date			: - 
<Reviewed By			: - 	

*******************************************************************************************/ 

using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.HomeOwners
{
	/// <summary>
	/// Database Model for APP_HOME_OWNER_RECREATIONAL_VEHICLES.
	/// </summary>
	public class ClsPolRecrVehiclesInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_HOME_OWNER_RECREATIONAL_VEHICLES = "APP_HOME_OWNER_RECREATIONAL_VEHICLES";
		public ClsPolRecrVehiclesInfo()
		{
			base.dtModel.TableName = "APP_HOME_OWNER_RECREATIONAL_VEHICLES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_RECREATIONAL_VEHICLES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_NUMBER",typeof(string));
			base.dtModel.Columns.Add("REC_VEH_ID",typeof(int));
			base.dtModel.Columns.Add("COMPANY_ID_NUMBER",typeof(int));
			base.dtModel.Columns.Add("YEAR",typeof(int));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			//added Fir itrack Issue #6710.
			base.dtModel.Columns.Add("LIABILITY",typeof(string));						
			base.dtModel.Columns.Add("MEDICAL_PAYMENTS",typeof(string));
			base.dtModel.Columns.Add("PHYSICAL_DAMAGE",typeof(string));
            // add till here..
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("SERIAL",typeof(string));
			base.dtModel.Columns.Add("STATE_REGISTERED",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_TYPE",typeof(int));
			base.dtModel.Columns.Add("MANUFACTURER_DESC",typeof(string));
			base.dtModel.Columns.Add("HORSE_POWER",typeof(string));
			base.dtModel.Columns.Add("DISPLACEMENT",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("USED_IN_RACE_SPEED",typeof(string));
			base.dtModel.Columns.Add("PRIOR_LOSSES",typeof(string));
			base.dtModel.Columns.Add("IS_UNIT_REG_IN_OTHER_STATE",typeof(string));
			base.dtModel.Columns.Add("RISK_DECL_BY_OTHER_COMP",typeof(string));
			base.dtModel.Columns.Add("DESC_RISK_DECL_BY_OTHER_COMP",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_MODIFIED",typeof(string));
			base.dtModel.Columns.Add("INSURING_VALUE",typeof(double));
			base.dtModel.Columns.Add("DEDUCTIBLE",typeof(double));
			base.dtModel.Columns.Add("UNIT_RENTED",typeof(string));
			base.dtModel.Columns.Add("UNIT_OWNED_DEALERS",typeof(string));
			base.dtModel.Columns.Add("YOUTHFUL_OPERATOR_UNDER_25",typeof(string));
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
		// model for database field COMPANY_ID_NUMBER(int)
		public int COMPANY_ID_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["COMPANY_ID_NUMBER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COMPANY_ID_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMPANY_ID_NUMBER"] = value;
			}
		}
		// model for database field YEAR(int)
		public int YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR"] = value;
			}
		}

		// model for database field MAKE(string)
		public string POLICY_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["POLICY_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_NUMBER"] = value;
			}
		}

		// model for database field MAKE(string)
		public string MAKE
		{
			get
			{
				return base.dtModel.Rows[0]["MAKE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAKE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAKE"] = value;
			}
		}
       // added For itrack Issue #6710.
		// model for database field LIABILITY(string)
		public string LIABILITY
		{
			get
			{
				return base.dtModel.Rows[0]["LIABILITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LIABILITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LIABILITY"] = value;
			}
		}
		// added For itrack Issue #6710.
		// model for database field MEDICAL_PAYMENTS(string)
		public string MEDICAL_PAYMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["MEDICAL_PAYMENTS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MEDICAL_PAYMENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MEDICAL_PAYMENTS"] = value;
			}
		}
        // added For itrack Issue #6710.
		// model for database field PHYSICAL_DAMAGE(string)
		public string PHYSICAL_DAMAGE
		{
			get
			{
				return base.dtModel.Rows[0]["PHYSICAL_DAMAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PHYSICAL_DAMAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PHYSICAL_DAMAGE"] = value;
			}
		}
		

		// model for database field MODEL(string)
		public string MODEL
		{
			get
			{
				return base.dtModel.Rows[0]["MODEL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MODEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL"] = value;
			}
		}
		// model for database field SERIAL(string)
		public string SERIAL
		{
			get
			{
				return base.dtModel.Rows[0]["SERIAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SERIAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERIAL"] = value;
			}
		}
		// model for database field STATE_REGISTERED(int)
		public int STATE_REGISTERED
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_REGISTERED"] == DBNull.Value ? 0 : Convert.ToInt32(base.dtModel.Rows[0]["STATE_REGISTERED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE_REGISTERED"] = value;
			}
		}
		// model for database field VEHICLE_TYPE(string)
		public int VEHICLE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(base.dtModel.Rows[0]["VEHICLE_TYPE"]);
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_TYPE"] = value;
			}
		}
		// model for database field MANUFACTURER_DESC(string)
		public string MANUFACTURER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["MANUFACTURER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MANUFACTURER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MANUFACTURER_DESC"] = value;
			}
		}
		// model for database field HORSE_POWER(string)
		public string HORSE_POWER
		{
			get
			{
				return base.dtModel.Rows[0]["HORSE_POWER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HORSE_POWER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HORSE_POWER"] = value;
			}
		}
		// model for database field DISPLACEMENT(string)
		public string DISPLACEMENT
		{
			get
			{
				return base.dtModel.Rows[0]["DISPLACEMENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DISPLACEMENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DISPLACEMENT"] = value;
			}
		}
		// model for database field REMARKS(string)
		public string REMARKS
		{
			get
			{
				return base.dtModel.Rows[0]["REMARKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REMARKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REMARKS"] = value;
			}
		}
		// model for database field USED_IN_RACE_SPEED(string)
		public string USED_IN_RACE_SPEED
		{
			get
			{
				return base.dtModel.Rows[0]["USED_IN_RACE_SPEED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USED_IN_RACE_SPEED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USED_IN_RACE_SPEED"] = value;
			}
		}
		// model for database field PRIOR_LOSSES(string)
		public string PRIOR_LOSSES
		{
			get
			{
				return base.dtModel.Rows[0]["PRIOR_LOSSES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PRIOR_LOSSES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRIOR_LOSSES"] = value;
			}
		}

		// model for database field UNIT_RENTED(string)
		public string UNIT_RENTED
		{
			get
			{
				return base.dtModel.Rows[0]["UNIT_RENTED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["UNIT_RENTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNIT_RENTED"] = value;
			}
		}
		// model for database field UNIT_RENTED(string)
		public string UNIT_OWNED_DEALERS
		{
			get
			{
				return base.dtModel.Rows[0]["UNIT_OWNED_DEALERS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["UNIT_OWNED_DEALERS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNIT_OWNED_DEALERS"] = value;
			}
		}
		// model for database field UNIT_RENTED(string)
		public string YOUTHFUL_OPERATOR_UNDER_25
		{
			get
			{
				return base.dtModel.Rows[0]["YOUTHFUL_OPERATOR_UNDER_25"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["YOUTHFUL_OPERATOR_UNDER_25"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["YOUTHFUL_OPERATOR_UNDER_25"] = value;
			}
		}
		// model for database field IS_UNIT_REG_IN_OTHER_STATE(string)
		public string IS_UNIT_REG_IN_OTHER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["IS_UNIT_REG_IN_OTHER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_UNIT_REG_IN_OTHER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_UNIT_REG_IN_OTHER_STATE"] = value;
			}
		}
		// model for database field RISK_DECL_BY_OTHER_COMP(string)
		public string RISK_DECL_BY_OTHER_COMP
		{
			get
			{
				return base.dtModel.Rows[0]["RISK_DECL_BY_OTHER_COMP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RISK_DECL_BY_OTHER_COMP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RISK_DECL_BY_OTHER_COMP"] = value;
			}
		}
		// model for database field DESC_RISK_DECL_BY_OTHER_COMP(string)
		public string DESC_RISK_DECL_BY_OTHER_COMP
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_RISK_DECL_BY_OTHER_COMP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_RISK_DECL_BY_OTHER_COMP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_RISK_DECL_BY_OTHER_COMP"] = value;
			}
		}
		// model for database field VEHICLE_MODIFIED(string)
		public string VEHICLE_MODIFIED
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_MODIFIED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VEHICLE_MODIFIED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_MODIFIED"] = value;
			}
		}
		// model for database field INSURING_VALUE(double)
		public double INSURING_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["INSURING_VALUE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["INSURING_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURING_VALUE"] = value;
			}
		}

		// model for database field DEDUCTIBLE(double)
		public double DEDUCTIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["DEDUCTIBLE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["DEDUCTIBLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEDUCTIBLE"] = value;
			}
		}
		#endregion
	}
}
