/******************************************************************************************
<Author					: -   Vijay Arora
<Start Date				: -	11/16/2005 11:08:09 AM
<End Date				: -	
<Description			: - 	Model Class for Policy Home Rating Info
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

namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Database Model for POL_HOME_RATING_INFO.
	/// </summary>
	public class ClsHomeRatingInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_HOME_RATING_INFO = "POL_HOME_RATING_INFO";
		public ClsHomeRatingInfo()
		{
			base.dtModel.TableName = "POL_HOME_RATING_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_HOME_RATING_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("HYDRANT_DIST",typeof(double));
			base.dtModel.Columns.Add("FIRE_STATION_DIST",typeof(double));
			base.dtModel.Columns.Add("IS_UNDER_CONSTRUCTION",typeof(string));
			base.dtModel.Columns.Add("EXPERIENCE_CREDIT",typeof(string));
			base.dtModel.Columns.Add("IS_AUTO_POL_WITH_CARRIER",typeof(string));
			base.dtModel.Columns.Add("PERSONAL_LIAB_TER_CODE",typeof(string));
			base.dtModel.Columns.Add("PROT_CLASS",typeof(string));
			base.dtModel.Columns.Add("RATING_METHOD",typeof(int));
			base.dtModel.Columns.Add("NO_OF_FAMILIES",typeof(int));
			base.dtModel.Columns.Add("EXTERIOR_CONSTRUCTION",typeof(int));
			base.dtModel.Columns.Add("EXTERIOR_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("FOUNDATION",typeof(int));
			base.dtModel.Columns.Add("FOUNDATION_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("ROOF_TYPE",typeof(int));
			base.dtModel.Columns.Add("ROOF_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("PRIMARY_HEAT_TYPE",typeof(int));
			base.dtModel.Columns.Add("SECONDARY_HEAT_TYPE",typeof(int));
			base.dtModel.Columns.Add("MONTH_OCC_EACH_YEAR",typeof(int));
			base.dtModel.Columns.Add("ADD_COVERAGE_INFO",typeof(string));
			base.dtModel.Columns.Add("IS_OUTSIDE_STAIR",typeof(string));
			base.dtModel.Columns.Add("TOT_SQR_FOOTAGE",typeof(double));
			base.dtModel.Columns.Add("GARAGE_SQR_FOOTAGE",typeof(double));
			base.dtModel.Columns.Add("BREEZE_SQR_FOOTAGE",typeof(double));
			base.dtModel.Columns.Add("BASMT_SQR_FOOTAGE",typeof(double));
			base.dtModel.Columns.Add("WIRING_RENOVATION",typeof(int));
			base.dtModel.Columns.Add("WIRING_UPDATE_YEAR",typeof(int));
			base.dtModel.Columns.Add("PLUMBING_RENOVATION",typeof(int));
			base.dtModel.Columns.Add("PLUMBING_UPDATE_YEAR",typeof(int));
			base.dtModel.Columns.Add("HEATING_RENOVATION",typeof(int));
			base.dtModel.Columns.Add("HEATING_UPDATE_YEAR",typeof(int));
			base.dtModel.Columns.Add("ROOFING_RENOVATION",typeof(int));
			base.dtModel.Columns.Add("ROOFING_UPDATE_YEAR",typeof(int));
			base.dtModel.Columns.Add("NO_OF_AMPS",typeof(int));
			base.dtModel.Columns.Add("CIRCUIT_BREAKERS",typeof(string));
			base.dtModel.Columns.Add("PROTECTIVE_DEVICES",typeof(string));
			base.dtModel.Columns.Add("TEMPERATURE",typeof(string));
			base.dtModel.Columns.Add("SMOKE",typeof(string));
			base.dtModel.Columns.Add("BURGLAR",typeof(string));
			base.dtModel.Columns.Add("FIRE_PLACES",typeof(string));
			base.dtModel.Columns.Add("NO_OF_WOOD_STOVES",typeof(int));
			base.dtModel.Columns.Add("SWIMMING_POOL",typeof(string));
			base.dtModel.Columns.Add("SWIMMING_POOL_TYPE",typeof(int));
			base.dtModel.Columns.Add("SECONDARY_HEAT_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("PRIMARY_HEAT_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("DWELLING_CONST_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CENT_ST_BURG_FIRE",typeof(string));
			base.dtModel.Columns.Add("CENT_ST_FIRE",typeof(string));
			base.dtModel.Columns.Add("CENT_ST_BURG",typeof(string));
			base.dtModel.Columns.Add("DIR_FIRE_AND_POLICE",typeof(string));
			base.dtModel.Columns.Add("DIR_FIRE",typeof(string));
			base.dtModel.Columns.Add("DIR_POLICE",typeof(string));
			base.dtModel.Columns.Add("LOC_FIRE_GAS",typeof(string));
			base.dtModel.Columns.Add("TWO_MORE_FIRE",typeof(string));
			base.dtModel.Columns.Add("CONSTRUCTION_CODE",typeof(string));
			base.dtModel.Columns.Add("NUM_LOC_ALARMS_APPLIES",typeof(int));				
			//Sumit Chhabra:April 05,2005:Added a new field SPRINKER
			base.dtModel.Columns.Add("SPRINKER",typeof(int));
			base.dtModel.Columns.Add("ALARM_CERT_ATTACHED",typeof(string));
			base.dtModel.Columns.Add("IS_SUPERVISED",typeof(string));
			base.dtModel.Columns.Add("NEED_OF_UNITS",typeof(string));
			
			// Suburban class details
			base.dtModel.Columns.Add("SUBURBAN_CLASS",typeof(string));
			base.dtModel.Columns.Add("LOCATED_IN_SUBDIVISION",typeof(string));
			
			
		}
		#region Database schema details
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
		// model for database field DWELLING_ID(int)
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
			}
		}
		// model for database field HYDRANT_DIST(double)
		public double HYDRANT_DIST
		{
			get
			{
				return base.dtModel.Rows[0]["HYDRANT_DIST"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["HYDRANT_DIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HYDRANT_DIST"] = value;
			}
		}
		// model for database field FIRE_STATION_DIST(double)
		public double FIRE_STATION_DIST
		{
			get
			{
				return base.dtModel.Rows[0]["FIRE_STATION_DIST"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["FIRE_STATION_DIST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FIRE_STATION_DIST"] = value;
			}
		}
		// model for database field IS_UNDER_CONSTRUCTION(string)
		public string IS_UNDER_CONSTRUCTION
		{
			get
			{
				return base.dtModel.Rows[0]["IS_UNDER_CONSTRUCTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_UNDER_CONSTRUCTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_UNDER_CONSTRUCTION"] = value;
			}
		}
		// model for database field EXPERIENCE_CREDIT(string)
		public string EXPERIENCE_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERIENCE_CREDIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXPERIENCE_CREDIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPERIENCE_CREDIT"] = value;
			}
		}
		// model for database field IS_AUTO_POL_WITH_CARRIER(string)
		public string IS_AUTO_POL_WITH_CARRIER
		{
			get
			{
				return base.dtModel.Rows[0]["IS_AUTO_POL_WITH_CARRIER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_AUTO_POL_WITH_CARRIER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_AUTO_POL_WITH_CARRIER"] = value;
			}
		}
		// model for database field PERSONAL_LIAB_TER_CODE(string)
		public string PERSONAL_LIAB_TER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["PERSONAL_LIAB_TER_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PERSONAL_LIAB_TER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PERSONAL_LIAB_TER_CODE"] = value;
			}
		}
		// model for database field PROT_CLASS(string)
		public string PROT_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["PROT_CLASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROT_CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROT_CLASS"] = value;
			}
		}
		// model for database field RATING_METHOD(int)
		public int RATING_METHOD
		{
			get
			{
				return base.dtModel.Rows[0]["RATING_METHOD"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RATING_METHOD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RATING_METHOD"] = value;
			}
		}
		// model for database field NO_OF_FAMILIES(int)
		public int NO_OF_FAMILIES
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_FAMILIES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NO_OF_FAMILIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_FAMILIES"] = value;
			}
		}

		public int CONSTRUCTION_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CONSTRUCTION_CODE"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CONSTRUCTION_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONSTRUCTION_CODE"] = value;
			}
		}
		// model for database field EXTERIOR_CONSTRUCTION(int)
		public int EXTERIOR_CONSTRUCTION
		{
			get
			{
				return base.dtModel.Rows[0]["EXTERIOR_CONSTRUCTION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["EXTERIOR_CONSTRUCTION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXTERIOR_CONSTRUCTION"] = value;
			}
		}
		// model for database field EXTERIOR_OTHER_DESC(string)
		public string EXTERIOR_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["EXTERIOR_OTHER_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXTERIOR_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXTERIOR_OTHER_DESC"] = value;
			}
		}
		// model for database field FOUNDATION(int)
		public int FOUNDATION
		{
			get
			{
				return base.dtModel.Rows[0]["FOUNDATION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["FOUNDATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FOUNDATION"] = value;
			}
		}
		// model for database field FOUNDATION_OTHER_DESC(string)
		public string FOUNDATION_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["FOUNDATION_OTHER_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FOUNDATION_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FOUNDATION_OTHER_DESC"] = value;
			}
		}
		// model for database field ROOF_TYPE(int)
		public int ROOF_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ROOF_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ROOF_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROOF_TYPE"] = value;
			}
		}
		// model for database field ROOF_OTHER_DESC(string)
		public string ROOF_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ROOF_OTHER_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ROOF_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ROOF_OTHER_DESC"] = value;
			}
		}
		// model for database field PRIMARY_HEAT_TYPE(int)
		public int PRIMARY_HEAT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["PRIMARY_HEAT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PRIMARY_HEAT_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRIMARY_HEAT_TYPE"] = value;
			}
		}
		// model for database field SECONDARY_HEAT_TYPE(int)
		public int SECONDARY_HEAT_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["SECONDARY_HEAT_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SECONDARY_HEAT_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SECONDARY_HEAT_TYPE"] = value;
			}
		}
		// model for database field MONTH_OCC_EACH_YEAR(int)
		public int MONTH_OCC_EACH_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["MONTH_OCC_EACH_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MONTH_OCC_EACH_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MONTH_OCC_EACH_YEAR"] = value;
			}
		}
		// model for database field ADD_COVERAGE_INFO(string)
		public string ADD_COVERAGE_INFO
		{
			get
			{
				return base.dtModel.Rows[0]["ADD_COVERAGE_INFO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADD_COVERAGE_INFO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADD_COVERAGE_INFO"] = value;
			}
		}
		// model for database field IS_OUTSIDE_STAIR(string)
		public string IS_OUTSIDE_STAIR
		{
			get
			{
				return base.dtModel.Rows[0]["IS_OUTSIDE_STAIR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_OUTSIDE_STAIR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_OUTSIDE_STAIR"] = value;
			}
		}
		// model for database field TOT_SQR_FOOTAGE(double)
		public double TOT_SQR_FOOTAGE
		{
			get
			{
				return base.dtModel.Rows[0]["TOT_SQR_FOOTAGE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TOT_SQR_FOOTAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TOT_SQR_FOOTAGE"] = value;
			}
		}
		// model for database field GARAGE_SQR_FOOTAGE(double)
		public double GARAGE_SQR_FOOTAGE
		{
			get
			{
				return base.dtModel.Rows[0]["GARAGE_SQR_FOOTAGE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["GARAGE_SQR_FOOTAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GARAGE_SQR_FOOTAGE"] = value;
			}
		}
		// model for database field BREEZE_SQR_FOOTAGE(double)
		public double BREEZE_SQR_FOOTAGE
		{
			get
			{
				return base.dtModel.Rows[0]["BREEZE_SQR_FOOTAGE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["BREEZE_SQR_FOOTAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BREEZE_SQR_FOOTAGE"] = value;
			}
		}
		// model for database field BASMT_SQR_FOOTAGE(double)
		public double BASMT_SQR_FOOTAGE
		{
			get
			{
				return base.dtModel.Rows[0]["BASMT_SQR_FOOTAGE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["BASMT_SQR_FOOTAGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BASMT_SQR_FOOTAGE"] = value;
			}
		}
		// model for database field WIRING_RENOVATION(int)
		public int WIRING_RENOVATION
		{
			get
			{
				return base.dtModel.Rows[0]["WIRING_RENOVATION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["WIRING_RENOVATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WIRING_RENOVATION"] = value;
			}
		}
		// model for database field WIRING_UPDATE_YEAR(int)
		public int WIRING_UPDATE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["WIRING_UPDATE_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["WIRING_UPDATE_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WIRING_UPDATE_YEAR"] = value;
			}
		}
		// model for database field PLUMBING_RENOVATION(int)
		public int PLUMBING_RENOVATION
		{
			get
			{
				return base.dtModel.Rows[0]["PLUMBING_RENOVATION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PLUMBING_RENOVATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PLUMBING_RENOVATION"] = value;
			}
		}
		// model for database field PLUMBING_UPDATE_YEAR(int)
		public int PLUMBING_UPDATE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["PLUMBING_UPDATE_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["PLUMBING_UPDATE_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PLUMBING_UPDATE_YEAR"] = value;
			}
		}
		// model for database field HEATING_RENOVATION(int)
		public int HEATING_RENOVATION
		{
			get
			{
				return base.dtModel.Rows[0]["HEATING_RENOVATION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["HEATING_RENOVATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HEATING_RENOVATION"] = value;
			}
		}
		// model for database field HEATING_UPDATE_YEAR(int)
		public int HEATING_UPDATE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["HEATING_UPDATE_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["HEATING_UPDATE_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HEATING_UPDATE_YEAR"] = value;
			}
		}
		// model for database field ROOFING_RENOVATION(int)
		public int ROOFING_RENOVATION
		{
			get
			{
				return base.dtModel.Rows[0]["ROOFING_RENOVATION"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ROOFING_RENOVATION"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROOFING_RENOVATION"] = value;
			}
		}
		// model for database field ROOFING_UPDATE_YEAR(int)
		public int ROOFING_UPDATE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["ROOFING_UPDATE_YEAR"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["ROOFING_UPDATE_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ROOFING_UPDATE_YEAR"] = value;
			}
		}
		// model for database field NO_OF_AMPS(int)
		public int NO_OF_AMPS
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_AMPS"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NO_OF_AMPS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_AMPS"] = value;
			}
		}
		// model for database field CIRCUIT_BREAKERS(string)
		public string CIRCUIT_BREAKERS
		{
			get
			{
				return base.dtModel.Rows[0]["CIRCUIT_BREAKERS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CIRCUIT_BREAKERS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CIRCUIT_BREAKERS"] = value;
			}
		}
		// model for database field PROTECTIVE_DEVICES(string)
		public string PROTECTIVE_DEVICES
		{
			get
			{
				return base.dtModel.Rows[0]["PROTECTIVE_DEVICES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PROTECTIVE_DEVICES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROTECTIVE_DEVICES"] = value;
			}
		}
		// model for database field ALARM_CERT_ATTACHED(string)
		public string ALARM_CERT_ATTACHED
		{
			get
			{
				return base.dtModel.Rows[0]["ALARM_CERT_ATTACHED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ALARM_CERT_ATTACHED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ALARM_CERT_ATTACHED"] = value;
			}
		}
		// model for database field TEMPERATURE(string)
		public string TEMPERATURE
		{
			get
			{
				return base.dtModel.Rows[0]["TEMPERATURE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TEMPERATURE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TEMPERATURE"] = value;
			}
		}
		// model for database field SMOKE(string)
		public string SMOKE
		{
			get
			{
				return base.dtModel.Rows[0]["SMOKE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SMOKE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SMOKE"] = value;
			}
		}
		// model for database field BURGLAR(string)
		public string BURGLAR
		{
			get
			{
				return base.dtModel.Rows[0]["BURGLAR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["BURGLAR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BURGLAR"] = value;
			}
		}
		// model for database field FIRE_PLACES(string)
		public string FIRE_PLACES
		{
			get
			{
				return base.dtModel.Rows[0]["FIRE_PLACES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FIRE_PLACES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FIRE_PLACES"] = value;
			}
		}
		// model for database field NO_OF_WOOD_STOVES(int)
		public int NO_OF_WOOD_STOVES
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_WOOD_STOVES"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["NO_OF_WOOD_STOVES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_WOOD_STOVES"] = value;
			}
		}
		// model for database field SWIMMING_POOL(string)
		public string SWIMMING_POOL
		{
			get
			{
				return base.dtModel.Rows[0]["SWIMMING_POOL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SWIMMING_POOL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SWIMMING_POOL"] = value;
			}
		}
		// model for database field SWIMMING_POOL_TYPE(int)
		public int SWIMMING_POOL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"] = value;
			}
		}
		// model for database field SECONDARY_HEAT_OTHER_DESC(string)
		public string SECONDARY_HEAT_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["SECONDARY_HEAT_OTHER_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SECONDARY_HEAT_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SECONDARY_HEAT_OTHER_DESC"] = value;
			}
		}
		// model for database field PRIMARY_HEAT_OTHER_DESC(string)
		public string PRIMARY_HEAT_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PRIMARY_HEAT_OTHER_DESC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PRIMARY_HEAT_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRIMARY_HEAT_OTHER_DESC"] = value;
			}
		}
		// model for database field DWELLING_CONST_DATE(DateTime)
		public DateTime DWELLING_CONST_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_CONST_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DWELLING_CONST_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_CONST_DATE"] = value;
			}
		}
		// model for database field CENT_ST_BURG_FIRE(string)
		public string CENT_ST_BURG_FIRE
		{
			get
			{
				return base.dtModel.Rows[0]["CENT_ST_BURG_FIRE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CENT_ST_BURG_FIRE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CENT_ST_BURG_FIRE"] = value;
			}
		}
		// model for database field CENT_ST_FIRE(string)
		public string CENT_ST_FIRE
		{
			get
			{
				return base.dtModel.Rows[0]["CENT_ST_FIRE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CENT_ST_FIRE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CENT_ST_FIRE"] = value;
			}
		}
		// model for database field CENT_ST_BURG(string)
		public string CENT_ST_BURG
		{
			get
			{
				return base.dtModel.Rows[0]["CENT_ST_BURG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CENT_ST_BURG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CENT_ST_BURG"] = value;
			}
		}
		// model for database field DIR_FIRE_AND_POLICE(string)
		public string DIR_FIRE_AND_POLICE
		{
			get
			{
				return base.dtModel.Rows[0]["DIR_FIRE_AND_POLICE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DIR_FIRE_AND_POLICE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIR_FIRE_AND_POLICE"] = value;
			}
		}
		// model for database field DIR_FIRE(string)
		public string DIR_FIRE
		{
			get
			{
				return base.dtModel.Rows[0]["DIR_FIRE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DIR_FIRE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIR_FIRE"] = value;
			}
		}
		// model for database field DIR_POLICE(string)
		public string DIR_POLICE
		{
			get
			{
				return base.dtModel.Rows[0]["DIR_POLICE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DIR_POLICE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DIR_POLICE"] = value;
			}
		}
		// model for database field LOC_FIRE_GAS(string)
		public string LOC_FIRE_GAS
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_FIRE_GAS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LOC_FIRE_GAS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_FIRE_GAS"] = value;
			}
		}
		// model for database field TWO_MORE_FIRE(string)
		public string TWO_MORE_FIRE
		{
			get
			{
				return base.dtModel.Rows[0]["TWO_MORE_FIRE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TWO_MORE_FIRE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TWO_MORE_FIRE"] = value;
			}
		}
		// model for database field NUM_LOC_ALARMS_APPLIES(int)
		public int NUM_LOC_ALARMS_APPLIES
		{
			get
			{
				return base.dtModel.Rows[0]["NUM_LOC_ALARMS_APPLIES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NUM_LOC_ALARMS_APPLIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NUM_LOC_ALARMS_APPLIES"] = value;
			}
		}
		// model for database field SPRINKER(int)
		public int SPRINKER
		{
			get
			{
				return base.dtModel.Rows[0]["SPRINKER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SPRINKER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SPRINKER"] = value;
			}
		}
		public string IS_SUPERVISED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SUPERVISED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_SUPERVISED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SUPERVISED"] = value;
			}
		}

		public string NEED_OF_UNITS
		{
			get
			{
				return base.dtModel.Rows[0]["NEED_OF_UNITS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NEED_OF_UNITS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NEED_OF_UNITS"] = value;
			}
		}
		// model for database field SUBURBAN_CLASS(string)
		public string SUBURBAN_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["SUBURBAN_CLASS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUBURBAN_CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUBURBAN_CLASS"] = value;
			}
		}


		// model for database field LOCATED_IN_SUBDIVISION(string)
		public string LOCATED_IN_SUBDIVISION
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATED_IN_SUBDIVISION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOCATED_IN_SUBDIVISION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATED_IN_SUBDIVISION"] = value;
			}
		}
		#endregion
	}
}
