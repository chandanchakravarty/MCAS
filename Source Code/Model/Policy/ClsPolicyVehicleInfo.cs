/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		03-11-2005
<End Date			: -	
<Description		: - 	Model Class for Policy Vehicle
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy
{
	/// <summary>
	/// Database Model for POL_VEHICLES.
	/// </summary>
	public class ClsPolicyVehicleInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_VEHICLES = "POL_VEHICLES";
		public ClsPolicyVehicleInfo()
		{
			base.dtModel.TableName = "POL_VEHICLES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_VEHICLES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("INSURED_VEH_NUMBER",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_YEAR",typeof(string));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("VIN",typeof(string));
			base.dtModel.Columns.Add("BODY_TYPE",typeof(string));
			base.dtModel.Columns.Add("GRG_ADD1",typeof(string));
			base.dtModel.Columns.Add("GRG_ADD2",typeof(string));
			base.dtModel.Columns.Add("GRG_CITY",typeof(string));
			base.dtModel.Columns.Add("GRG_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("GRG_STATE",typeof(string));
			base.dtModel.Columns.Add("GRG_ZIP",typeof(string));
			base.dtModel.Columns.Add("REGISTERED_STATE",typeof(string));
			base.dtModel.Columns.Add("TERRITORY",typeof(string));
			base.dtModel.Columns.Add("CLASS",typeof(string));
			base.dtModel.Columns.Add("REGN_PLATE_NUMBER",typeof(string));
			base.dtModel.Columns.Add("ST_AMT_TYPE",typeof(string));
			base.dtModel.Columns.Add("AMOUNT",typeof(double));
			base.dtModel.Columns.Add("SYMBOL",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_AGE",typeof(int));
			base.dtModel.Columns.Add("IS_OWN_LEASE",typeof(string));
			base.dtModel.Columns.Add("PURCHASE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("IS_NEW_USED",typeof(string));
			base.dtModel.Columns.Add("MILES_TO_WORK",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_USE",typeof(string));
			base.dtModel.Columns.Add("VEH_PERFORMANCE",typeof(string));
			base.dtModel.Columns.Add("MULTI_CAR",typeof(string));
			base.dtModel.Columns.Add("ANNUAL_MILEAGE",typeof(double));
			base.dtModel.Columns.Add("PASSIVE_SEAT_BELT",typeof(string));
			base.dtModel.Columns.Add("AIR_BAG",typeof(string));
			base.dtModel.Columns.Add("ANTI_LOCK_BRAKES",typeof(string));
			base.dtModel.Columns.Add("DEACTIVATE_REACTIVATE_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("VEHICLE_CC",typeof(int));
			base.dtModel.Columns.Add("MOTORCYCLE_TYPE",typeof(int));
			base.dtModel.Columns.Add("UNINS_MOTOR_INJURY_COVE",typeof(string));
			base.dtModel.Columns.Add("UNINS_PROPERTY_DAMAGE_COVE",typeof(string));
			base.dtModel.Columns.Add("UNDERINS_MOTOR_INJURY_COVE",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_TYPE",typeof(int));
			base.dtModel.Columns.Add("NATURE_OF_INTEREST",typeof(int));
			base.dtModel.Columns.Add("APP_USE_VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VEHICLE_PERCLASS_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VEHICLE_COMCLASS_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VEHICLE_PERTYPE_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VEHICLE_COMTYPE_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VEHICLE_CLASS",typeof(int));
			//base.dtModel.Columns.Add("SAFETY_BELT",typeof(string));
			base.dtModel.Columns.Add("BUSS_PERM_RESI",typeof(int));
			base.dtModel.Columns.Add("SNOWPLOW_CONDS",typeof(int));
			base.dtModel.Columns.Add("CAR_POOL",typeof(int));
			base.dtModel.Columns.Add("AUTO_POL_NO",typeof(string));

			base.dtModel.Columns.Add("RADIUS_OF_USE",typeof(int));
			base.dtModel.Columns.Add("TRANSPORT_CHEMICAL",typeof(string));
			base.dtModel.Columns.Add("COVERED_BY_WC_INSU",typeof(string));
			base.dtModel.Columns.Add("CLASS_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("CYCL_REGD_ROAD_USE",typeof(int));
			base.dtModel.Columns.Add("COMPRH_ONLY",typeof(int));
			base.dtModel.Columns.Add("IS_SUSPENDED",typeof(int));

            //Added by Agniswar for Singapore Implementation
            base.dtModel.Columns.Add("CHASIS_NUMBER", typeof(string));
            base.dtModel.Columns.Add("TRANSMISSION_TYPE", typeof(string));
            base.dtModel.Columns.Add("FUEL_TYPE", typeof(string));
            base.dtModel.Columns.Add("TOTAL_DRIVERS", typeof(int));
            base.dtModel.Columns.Add("RISK_CURRENCY", typeof(int));
            base.dtModel.Columns.Add("VEHICLE_COVERAGE", typeof(string));


		}
		#region Database schema details
		// model for database field POLICY_ID(int)
		public int POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field VEHICLE_ID(int)
		public int VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}
		// model for database field INSURED_VEH_NUMBER(int)
		public int INSURED_VEH_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_VEH_NUMBER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["INSURED_VEH_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_VEH_NUMBER"] = value;
			}
		}
		// model for database field VEHICLE_YEAR(string)
		public string VEHICLE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_YEAR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VEHICLE_YEAR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_YEAR"] = value;
			}
		}
		// model for database field MAKE(string)
		public string MAKE
		{
			get
			{
				return base.dtModel.Rows[0]["MAKE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MAKE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAKE"] = value;
			}
		}
		// model for database field MODEL(string)
		public string MODEL
		{
			get
			{
				return base.dtModel.Rows[0]["MODEL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MODEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL"] = value;
			}
		}
		// model for database field VIN(string)
		public string VIN
		{
			get
			{
				return base.dtModel.Rows[0]["VIN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VIN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VIN"] = value;
			}
		}
		// model for database field BODY_TYPE(string)
		public string BODY_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BODY_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BODY_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BODY_TYPE"] = value;
			}
		}
		// model for database field GRG_ADD1(string)
		public string GRG_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["GRG_ADD1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRG_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GRG_ADD1"] = value;
			}
		}
		// model for database field GRG_ADD2(string)
		public string GRG_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["GRG_ADD2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRG_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GRG_ADD2"] = value;
			}
		}
		// model for database field GRG_CITY(string)
		public string GRG_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["GRG_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRG_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GRG_CITY"] = value;
			}
		}
		// model for database field GRG_COUNTRY(string)
		public string GRG_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["GRG_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRG_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GRG_COUNTRY"] = value;
			}
		}
		// model for database field GRG_STATE(string)
		public string GRG_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["GRG_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRG_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GRG_STATE"] = value;
			}
		}
		// model for database field GRG_ZIP(string)
		public string GRG_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["GRG_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRG_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["GRG_ZIP"] = value;
			}
		}

		public int BUSS_PERM_RESI
		{
			get
			{
				return base.dtModel.Rows[0]["BUSS_PERM_RESI"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BUSS_PERM_RESI"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BUSS_PERM_RESI"] = value;
			}
		}
		// Snowplow Conditions
		public int SNOWPLOW_CONDS
		{
			get
			{
				return base.dtModel.Rows[0]["SNOWPLOW_CONDS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SNOWPLOW_CONDS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SNOWPLOW_CONDS"] = value;
			}
		}
		
		// model for database field AUTO_POL_NO(string)
		public string AUTO_POL_NO
		{
			get
			{
				return base.dtModel.Rows[0]["AUTO_POL_NO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AUTO_POL_NO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AUTO_POL_NO"] = value;
			}
		}

		//used in Car Pool
		public int CAR_POOL
		{
			get
			{
				return base.dtModel.Rows[0]["CAR_POOL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CAR_POOL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CAR_POOL"] = value;
			}
		}
		// model for database field REGISTERED_STATE(string)
		public string REGISTERED_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["REGISTERED_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REGISTERED_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REGISTERED_STATE"] = value;
			}
		}
		// model for database field TERRITORY(string)
		public string TERRITORY
		{
			get
			{
				return base.dtModel.Rows[0]["TERRITORY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TERRITORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TERRITORY"] = value;
			}
		}
		// model for database field CLASS(string)
		public string CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["CLASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLASS"] = value;
			}
		}
		// model for database field REGN_PLATE_NUMBER(string)
		public string REGN_PLATE_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["REGN_PLATE_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["REGN_PLATE_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REGN_PLATE_NUMBER"] = value;
			}
		}
		// model for database field ST_AMT_TYPE(string)
		public string ST_AMT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ST_AMT_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ST_AMT_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ST_AMT_TYPE"] = value;
			}
		}
		// model for database field AMOUNT(double)
		public double AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMOUNT"] = value;
			}
		}
		// model for database field SYMBOL(int)
		public int SYMBOL
		{
			get
			{
				return base.dtModel.Rows[0]["SYMBOL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SYMBOL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SYMBOL"] = value;
			}
		}
		// model for database field VEHICLE_AGE(int)
		public int VEHICLE_AGE
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_AGE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VEHICLE_AGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_AGE"] = value;
			}
		}
		// model for database field IS_OWN_LEASE(string)
		public string IS_OWN_LEASE
		{
			get
			{
				return base.dtModel.Rows[0]["IS_OWN_LEASE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_OWN_LEASE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_OWN_LEASE"] = value;
			}
		}
		// model for database field PURCHASE_DATE(DateTime)
		public DateTime PURCHASE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["PURCHASE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["PURCHASE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PURCHASE_DATE"] = value;
			}
		}
		// model for database field IS_NEW_USED(string)
		public string IS_NEW_USED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_NEW_USED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_NEW_USED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_NEW_USED"] = value;
			}
		}
		// model for database field MILES_TO_WORK(string)
		public string MILES_TO_WORK
		{
			get
			{
				return base.dtModel.Rows[0]["MILES_TO_WORK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MILES_TO_WORK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MILES_TO_WORK"] = value;
			}
		}
		// model for database field VEHICLE_USE(string)
		public string VEHICLE_USE
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_USE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VEHICLE_USE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_USE"] = value;
			}
		}
		// model for database field VEH_PERFORMANCE(string)
		public string VEH_PERFORMANCE
		{
			get
			{
				return base.dtModel.Rows[0]["VEH_PERFORMANCE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VEH_PERFORMANCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEH_PERFORMANCE"] = value;
			}
		}
		// model for database field MULTI_CAR(string)
		public string MULTI_CAR
		{
			get
			{
				return base.dtModel.Rows[0]["MULTI_CAR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MULTI_CAR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MULTI_CAR"] = value;
			}
		}
		// model for database field SAFETY_BELT(string)
		/*public string SAFETY_BELT
		{
			get
			{
				return base.dtModel.Rows[0]["SAFETY_BELT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SAFETY_BELT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SAFETY_BELT"] = value;
			}
		}*/
		// model for database field ANNUAL_MILEAGE(double)
		public double ANNUAL_MILEAGE
		{
			get
			{
				return base.dtModel.Rows[0]["ANNUAL_MILEAGE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["ANNUAL_MILEAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ANNUAL_MILEAGE"] = value;
			}
		}
		// model for database field PASSIVE_SEAT_BELT(string)
		public string PASSIVE_SEAT_BELT
		{
			get
			{
				return base.dtModel.Rows[0]["PASSIVE_SEAT_BELT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PASSIVE_SEAT_BELT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PASSIVE_SEAT_BELT"] = value;
			}
		}
		// model for database field AIR_BAG(string)
		public string AIR_BAG
		{
			get
			{
				return base.dtModel.Rows[0]["AIR_BAG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["AIR_BAG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AIR_BAG"] = value;
			}
		}
		// model for database field ANTI_LOCK_BRAKES(string)
		public string ANTI_LOCK_BRAKES
		{
			get
			{
				return base.dtModel.Rows[0]["ANTI_LOCK_BRAKES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ANTI_LOCK_BRAKES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANTI_LOCK_BRAKES"] = value;
			}
		}
		// model for database field DEACTIVATE_REACTIVATE_DATE(DateTime)
		public DateTime DEACTIVATE_REACTIVATE_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DEACTIVATE_REACTIVATE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DEACTIVATE_REACTIVATE_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DEACTIVATE_REACTIVATE_DATE"] = value;
			}
		}
		// model for database field VEHICLE_CC(int)
		public int VEHICLE_CC
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_CC"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VEHICLE_CC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_CC"] = value;
			}
		}
		// model for database field MOTORCYCLE_TYPE(int)
		public int MOTORCYCLE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["MOTORCYCLE_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MOTORCYCLE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MOTORCYCLE_TYPE"] = value;
			}
		}
		// model for database field UNINS_MOTOR_INJURY_COVE(string)
		public string UNINS_MOTOR_INJURY_COVE
		{
			get
			{
				return base.dtModel.Rows[0]["UNINS_MOTOR_INJURY_COVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["UNINS_MOTOR_INJURY_COVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNINS_MOTOR_INJURY_COVE"] = value;
			}
		}
		// model for database field UNINS_PROPERTY_DAMAGE_COVE(string)
		public string UNINS_PROPERTY_DAMAGE_COVE
		{
			get
			{
				return base.dtModel.Rows[0]["UNINS_PROPERTY_DAMAGE_COVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["UNINS_PROPERTY_DAMAGE_COVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNINS_PROPERTY_DAMAGE_COVE"] = value;
			}
		}
		// model for database field UNDERINS_MOTOR_INJURY_COVE(string)
		public string UNDERINS_MOTOR_INJURY_COVE
		{
			get
			{
				return base.dtModel.Rows[0]["UNDERINS_MOTOR_INJURY_COVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["UNDERINS_MOTOR_INJURY_COVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNDERINS_MOTOR_INJURY_COVE"] = value;
			}
		}
		// model for database field VEHICLE_TYPE(int)
		public int VEHICLE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VEHICLE_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_TYPE"] = value;
			}
		}
		// model for database field NATURE_OF_INTEREST(int)
		public int NATURE_OF_INTEREST
		{
			get
			{
				return base.dtModel.Rows[0]["NATURE_OF_INTEREST"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NATURE_OF_INTEREST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NATURE_OF_INTEREST"] = value;
			}
		}
		// model for database field APP_USE_VEHICLE_ID(int)
		public int APP_USE_VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_USE_VEHICLE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_USE_VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_USE_VEHICLE_ID"] = value;
			}
		}
		// model for database field APP_VEHICLE_PERCLASS_ID(int)
		public int APP_VEHICLE_PERCLASS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_PERCLASS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_PERCLASS_ID"] = value;
			}
		}
		// model for database field APP_VEHICLE_COMCLASS_ID(int)
		public int APP_VEHICLE_COMCLASS_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_COMCLASS_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_COMCLASS_ID"] = value;
			}
		}
		// model for database field APP_VEHICLE_PERTYPE_ID(int)
		public int APP_VEHICLE_PERTYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_PERTYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_PERTYPE_ID"] = value;
			}
		}
		// model for database field APP_VEHICLE_COMTYPE_ID(int)
		public int APP_VEHICLE_COMTYPE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_COMTYPE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_COMTYPE_ID"] = value;
			}
		}
		// model for database field APP_VEHICLE_CLASS(int)
		public int APP_VEHICLE_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_CLASS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VEHICLE_CLASS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_CLASS"] = value;
			}
		}
		// model for database field RADIUS_OF_USE(int)
		public int RADIUS_OF_USE
		{
			get
			{
				return base.dtModel.Rows[0]["RADIUS_OF_USE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RADIUS_OF_USE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RADIUS_OF_USE"] = value;
			}
		}

		// model for database field TRANSPORT_CHEMICAL(string)
		public string TRANSPORT_CHEMICAL
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSPORT_CHEMICAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRANSPORT_CHEMICAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRANSPORT_CHEMICAL"] = value;
			}
		}


		// model for database field COVERED_BY_WC_INSU(string)
		public string COVERED_BY_WC_INSU
		{
			get
			{
				return base.dtModel.Rows[0]["COVERED_BY_WC_INSU"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERED_BY_WC_INSU"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERED_BY_WC_INSU"] = value;
			}
		}

		// model for database field CLASS_DESCRIPTION(string)
		public string CLASS_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["CLASS_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLASS_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLASS_DESCRIPTION"] = value;
			}
		}
		// model for database field CYCL_REGD_ROAD_USE(int)
		public int CYCL_REGD_ROAD_USE
		{
			get
			{
				return base.dtModel.Rows[0]["CYCL_REGD_ROAD_USE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CYCL_REGD_ROAD_USE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CYCL_REGD_ROAD_USE"] = value;
			}
		}

		// COMPRH_ONLY
		public int COMPRH_ONLY
		{
			get
			{
				return base.dtModel.Rows[0]["COMPRH_ONLY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COMPRH_ONLY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COMPRH_ONLY"] = value;
			}
		}
		// IS_SUSPENDED
		public int IS_SUSPENDED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SUSPENDED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["IS_SUSPENDED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IS_SUSPENDED"] = value;
			}
		}
        // model for database field CHASIS_NUMBER(string)
        public string CHASIS_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["CHASIS_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CHASIS_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CHASIS_NUMBER"] = value;
            }
        }
        // model for database field TRANSMISSION_TYPE(string)
        public string TRANSMISSION_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["TRANSMISSION_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRANSMISSION_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["TRANSMISSION_TYPE"] = value;
            }
        }
        // model for database field FUEL_TYPE(string)
        public string FUEL_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["FUEL_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FUEL_TYPE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FUEL_TYPE"] = value;
            }
        }
        // TOTAL_DRIVERS
        public int TOTAL_DRIVERS
        {
            get
            {
                return base.dtModel.Rows[0]["TOTAL_DRIVERS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["TOTAL_DRIVERS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TOTAL_DRIVERS"] = value;
            }
        }

        // RISK_CURRENCY 
        public int RISK_CURRENCY
        {
            get
            {
                return base.dtModel.Rows[0]["RISK_CURRENCY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RISK_CURRENCY"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RISK_CURRENCY"] = value;
            }
        }

        // model for database field VEHICLE_COVERAGE(string)
        public string VEHICLE_COVERAGE
        {
            get
            {
                return base.dtModel.Rows[0]["VEHICLE_COVERAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VEHICLE_COVERAGE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["VEHICLE_COVERAGE"] = value;
            }
        }
		#endregion
	}
}
