/******************************************************************************************
<Author					: -   Pradeep Iyer
<Start Date				: -	  5/31/2005 10:22:31 AM
<End Date				: -	
<Description			: -  Model class for Umbrella Gen Info
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

namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_UMBRELLA_GEN_INFO.
	/// </summary>
	public class ClsUmbrellaGenInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_UMBRELLA_GEN_INFO = "APP_UMBRELLA_GEN_INFO";
		
		public ClsUmbrellaGenInfo()
		{
			base.dtModel.TableName = "APP_UMBRELLA_GEN_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_UMBRELLA_GEN_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ANY_AIRCRAFT_OWNED_LEASED",typeof(string));
			base.dtModel.Columns.Add("ANY_OPERATOR_CON_TRAFFIC",typeof(string));
			base.dtModel.Columns.Add("ANY_OPERATOR_IMPIRED",typeof(string));
			base.dtModel.Columns.Add("ANY_SWIMMING_POOL",typeof(string));
			base.dtModel.Columns.Add("REAL_STATE_VEHICLE_USED",typeof(string));
			base.dtModel.Columns.Add("REAL_STATE_VEH_OWNED_HIRED",typeof(string));
			base.dtModel.Columns.Add("ENGAGED_IN_FARMING",typeof(string));
			base.dtModel.Columns.Add("HOLD_NON_COMP_POSITION",typeof(string));
			base.dtModel.Columns.Add("ANY_FULL_TIME_EMPLOYEE",typeof(string));
			base.dtModel.Columns.Add("NON_OWNED_PROPERTY_CARE",typeof(string));
			base.dtModel.Columns.Add("BUSINESS_PROF_ACTIVITY",typeof(string));
			base.dtModel.Columns.Add("REDUCED_LIMIT_OF_LIBLITY",typeof(string));
			base.dtModel.Columns.Add("ANIMALS_EXO_PETS_HISTORY",typeof(string));
			base.dtModel.Columns.Add("ANY_COVERAGE_DECLINED",typeof(string));
			base.dtModel.Columns.Add("ANIMALS_EXOTIC_PETS",typeof(string));
			base.dtModel.Columns.Add("INSU_TRANSFERED_IN_AGENCY",typeof(string));
			base.dtModel.Columns.Add("PENDING_LITIGATIONS",typeof(string));
			base.dtModel.Columns.Add("IS_TEMPOLINE",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("ANY_AIRCRAFT_OWNED_LEASED_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_OPERATOR_CON_TRAFFIC_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_OPERATOR_IMPIRED_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_SWIMMING_POOL_DESC",typeof(string));
			base.dtModel.Columns.Add("REAL_STATE_VEHICLE_USED_DESC",typeof(string));
			base.dtModel.Columns.Add("REAL_STATE_VEH_OWNED_HIRED_DESC",typeof(string));
			base.dtModel.Columns.Add("ENGAGED_IN_FARMING_DESC",typeof(string));
			base.dtModel.Columns.Add("HOLD_NON_COMP_POSITION_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_FULL_TIME_EMPLOYEE_DESC",typeof(string));
			base.dtModel.Columns.Add("NON_OWNED_PROPERTY_CARE_DESC",typeof(string));
			base.dtModel.Columns.Add("BUSINESS_PROF_ACTIVITY_DESC",typeof(string));
			base.dtModel.Columns.Add("REDUCED_LIMIT_OF_LIBLITY_DESC",typeof(string));
			base.dtModel.Columns.Add("ANIMALS_EXOTIC_PETS_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_COVERAGE_DECLINED_DESC",typeof(string));
			base.dtModel.Columns.Add("INSU_TRANSFERED_IN_AGENCY_DESC",typeof(string));
			base.dtModel.Columns.Add("PENDING_LITIGATIONS_DESC",typeof(string));
			base.dtModel.Columns.Add("IS_TEMPOLINE_DESC",typeof(string));
			base.dtModel.Columns.Add("INS_DOMICILED_OUTSIDE_DESC",typeof(string));
			base.dtModel.Columns.Add("HOME_DAY_CARE_DESC",typeof(string));
			base.dtModel.Columns.Add("HAVE_NON_OWNED_AUTO_POL",typeof(string));
			base.dtModel.Columns.Add("INS_DOMICILED_OUTSIDE",typeof(string));
			base.dtModel.Columns.Add("HOME_DAY_CARE",typeof(string));
			base.dtModel.Columns.Add("HAVE_NON_OWNED_AUTO_POL_DESC",typeof(string));
			base.dtModel.Columns.Add("CALCULATIONS",typeof(string));
			base.dtModel.Columns.Add("HOME_RENT_DWELL",typeof(string));
			base.dtModel.Columns.Add("HOME_RENT_DWELL_DESC",typeof(string));
			base.dtModel.Columns.Add("WAT_DWELL",typeof(string));
			base.dtModel.Columns.Add("WAT_DWELL_DESC",typeof(string));
			base.dtModel.Columns.Add("RECR_VEH",typeof(string));
			base.dtModel.Columns.Add("RECR_VEH_DESC",typeof(string));
			base.dtModel.Columns.Add("AUTO_CYCL_TRUCKS",typeof(string));
			base.dtModel.Columns.Add("AUTO_CYCL_TRUCKS_DESC",typeof(string));
			base.dtModel.Columns.Add("APPLI_UNDERSTAND_LIABILITY_EXCLUDED",typeof(string));
			base.dtModel.Columns.Add("APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC",typeof(string));
			base.dtModel.Columns.Add("UND_REMARKS",typeof(string));
			/******Added by Manoj on 5 th dec 2007*******/
			base.dtModel.Columns.Add("OFFICE_PREMISES",typeof(int));
			base.dtModel.Columns.Add("RENTAL_DWELLINGS_UNIT",typeof(int));
			base.dtModel.Columns.Add("FAMILIES",typeof(int));

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
		// model for database field ANY_AIRCRAFT_OWNED_LEASED(string)
		public string ANY_AIRCRAFT_OWNED_LEASED
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED"] = value;
			}
		}
		// model for database field ANY_OPERATOR_CON_TRAFFIC(string)
		public string ANY_OPERATOR_CON_TRAFFIC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OPERATOR_CON_TRAFFIC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OPERATOR_CON_TRAFFIC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OPERATOR_CON_TRAFFIC"] = value;
			}
		}
		// model for database field ANY_OPERATOR_IMPIRED(string)
		public string ANY_OPERATOR_IMPIRED
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OPERATOR_IMPIRED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OPERATOR_IMPIRED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OPERATOR_IMPIRED"] = value;
			}
		}
		// model for database field ANY_SWIMMING_POOL(string)
		public string ANY_SWIMMING_POOL
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_SWIMMING_POOL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_SWIMMING_POOL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_SWIMMING_POOL"] = value;
			}
		}
		// model for database field REAL_STATE_VEHICLE_USED(string)
		public string REAL_STATE_VEHICLE_USED
		{
			get
			{
				return base.dtModel.Rows[0]["REAL_STATE_VEHICLE_USED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REAL_STATE_VEHICLE_USED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REAL_STATE_VEHICLE_USED"] = value;
			}
		}
		// model for database field REAL_STATE_VEH_OWNED_HIRED(string)
		public string REAL_STATE_VEH_OWNED_HIRED
		{
			get
			{
				return base.dtModel.Rows[0]["REAL_STATE_VEH_OWNED_HIRED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REAL_STATE_VEH_OWNED_HIRED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REAL_STATE_VEH_OWNED_HIRED"] = value;
			}
		}
		// model for database field ENGAGED_IN_FARMING(string)
		public string ENGAGED_IN_FARMING
		{
			get
			{
				return base.dtModel.Rows[0]["ENGAGED_IN_FARMING"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENGAGED_IN_FARMING"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENGAGED_IN_FARMING"] = value;
			}
		}
		// model for database field HOLD_NON_COMP_POSITION(string)
		public string HOLD_NON_COMP_POSITION
		{
			get
			{
				return base.dtModel.Rows[0]["HOLD_NON_COMP_POSITION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLD_NON_COMP_POSITION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLD_NON_COMP_POSITION"] = value;
			}
		}
		// model for database field ANY_FULL_TIME_EMPLOYEE(string)
		public string ANY_FULL_TIME_EMPLOYEE
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_FULL_TIME_EMPLOYEE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_FULL_TIME_EMPLOYEE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_FULL_TIME_EMPLOYEE"] = value;
			}
		}
		// model for database field NON_OWNED_PROPERTY_CARE(string)
		public string NON_OWNED_PROPERTY_CARE
		{
			get
			{
				return base.dtModel.Rows[0]["NON_OWNED_PROPERTY_CARE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_OWNED_PROPERTY_CARE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_OWNED_PROPERTY_CARE"] = value;
			}
		}
		// model for database field BUSINESS_PROF_ACTIVITY(string)
		public string BUSINESS_PROF_ACTIVITY
		{
			get
			{
				return base.dtModel.Rows[0]["BUSINESS_PROF_ACTIVITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BUSINESS_PROF_ACTIVITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BUSINESS_PROF_ACTIVITY"] = value;
			}
		}
		// model for database field REDUCED_LIMIT_OF_LIBLITY(string)
		public string REDUCED_LIMIT_OF_LIBLITY
		{
			get
			{
				return base.dtModel.Rows[0]["REDUCED_LIMIT_OF_LIBLITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REDUCED_LIMIT_OF_LIBLITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REDUCED_LIMIT_OF_LIBLITY"] = value;
			}
		}
		// model for database field ANIMALS_EXO_PETS_HISTORY(string)
		public string ANIMALS_EXO_PETS_HISTORY
		{
			get
			{
				return base.dtModel.Rows[0]["ANIMALS_EXO_PETS_HISTORY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANIMALS_EXO_PETS_HISTORY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANIMALS_EXO_PETS_HISTORY"] = value;
			}
		}
		// model for database field ANY_COVERAGE_DECLINED(string)
		public string ANY_COVERAGE_DECLINED
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_COVERAGE_DECLINED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_COVERAGE_DECLINED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_COVERAGE_DECLINED"] = value;
			}
		}
		// model for database field ANIMALS_EXOTIC_PETS(string)
		public string ANIMALS_EXOTIC_PETS
		{
			get
			{
				return base.dtModel.Rows[0]["ANIMALS_EXOTIC_PETS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANIMALS_EXOTIC_PETS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANIMALS_EXOTIC_PETS"] = value;
			}
		}
		// model for database field INSU_TRANSFERED_IN_AGENCY(string)
		public string INSU_TRANSFERED_IN_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["INSU_TRANSFERED_IN_AGENCY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSU_TRANSFERED_IN_AGENCY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSU_TRANSFERED_IN_AGENCY"] = value;
			}
		}
		// model for database field PENDING_LITIGATIONS(string)
		public string PENDING_LITIGATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["PENDING_LITIGATIONS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PENDING_LITIGATIONS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PENDING_LITIGATIONS"] = value;
			}
		}
		// model for database field IS_TEMPOLINE(string)
		public string IS_TEMPOLINE
		{
			get
			{
				return base.dtModel.Rows[0]["IS_TEMPOLINE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_TEMPOLINE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_TEMPOLINE"] = value;
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

		// model for database field ANY_AIRCRAFT_OWNED_LEASED_DESC(string)
		public string ANY_AIRCRAFT_OWNED_LEASED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED_DESC"] = value;
			}
		}

		// model for database field ANY_OPERATOR_CON_TRAFFIC_DESC(string)
		public string ANY_OPERATOR_CON_TRAFFIC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OPERATOR_CON_TRAFFIC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OPERATOR_CON_TRAFFIC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OPERATOR_CON_TRAFFIC_DESC"] = value;
			}
		}

		// model for database field ANY_OPERATOR_IMPIRED_DESC(string)
		public string ANY_OPERATOR_IMPIRED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OPERATOR_IMPIRED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OPERATOR_IMPIRED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OPERATOR_IMPIRED_DESC"] = value;
			}
		}

		// model for database field ANY_SWIMMING_POOL_DESC(string)
		public string ANY_SWIMMING_POOL_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_SWIMMING_POOL_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_SWIMMING_POOL_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_SWIMMING_POOL_DESC"] = value;
			}
		}

		// model for database field REAL_STATE_VEHICLE_USED_DESC(string)
		public string REAL_STATE_VEHICLE_USED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["REAL_STATE_VEHICLE_USED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REAL_STATE_VEHICLE_USED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REAL_STATE_VEHICLE_USED_DESC"] = value;
			}
		}

		// model for database field REAL_STATE_VEH_OWNED_HIRED_DESC(string)
		public string REAL_STATE_VEH_OWNED_HIRED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["REAL_STATE_VEH_OWNED_HIRED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REAL_STATE_VEH_OWNED_HIRED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REAL_STATE_VEH_OWNED_HIRED_DESC"] = value;
			}
		}

		// model for database field ENGAGED_IN_FARMING_DESC(string)
		public string ENGAGED_IN_FARMING_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ENGAGED_IN_FARMING_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENGAGED_IN_FARMING_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENGAGED_IN_FARMING_DESC"] = value;
			}
		}

		// model for database field HOLD_NON_COMP_POSITION_DESC(string)
		public string HOLD_NON_COMP_POSITION_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["HOLD_NON_COMP_POSITION_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOLD_NON_COMP_POSITION_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOLD_NON_COMP_POSITION_DESC"] = value;
			}
		}
		// model for database field ANY_FULL_TIME_EMPLOYEE_DESC(string)
		public string ANY_FULL_TIME_EMPLOYEE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_FULL_TIME_EMPLOYEE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_FULL_TIME_EMPLOYEE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_FULL_TIME_EMPLOYEE_DESC"] = value;
			}
		}
		// model for database field NON_OWNED_PROPERTY_CARE_DESC(string)
		public string NON_OWNED_PROPERTY_CARE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["NON_OWNED_PROPERTY_CARE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_OWNED_PROPERTY_CARE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_OWNED_PROPERTY_CARE_DESC"] = value;
			}
		}
		// model for database field BUSINESS_PROF_ACTIVITY_DESC(string)
		public string BUSINESS_PROF_ACTIVITY_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["BUSINESS_PROF_ACTIVITY_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BUSINESS_PROF_ACTIVITY_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BUSINESS_PROF_ACTIVITY_DESC"] = value;
			}
		}
		// model for database field REDUCED_LIMIT_OF_LIBLITY_DESC(string)
		public string REDUCED_LIMIT_OF_LIBLITY_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["REDUCED_LIMIT_OF_LIBLITY_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REDUCED_LIMIT_OF_LIBLITY_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REDUCED_LIMIT_OF_LIBLITY_DESC"] = value;
			}
		}
		// model for database field ANIMALS_EXOTIC_PETS_DESC(string)
		public string ANIMALS_EXOTIC_PETS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANIMALS_EXOTIC_PETS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANIMALS_EXOTIC_PETS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANIMALS_EXOTIC_PETS_DESC"] = value;
			}
		}
		// model for database field ANY_COVERAGE_DECLINED_DESC(string)
		public string ANY_COVERAGE_DECLINED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_COVERAGE_DECLINED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_COVERAGE_DECLINED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_COVERAGE_DECLINED_DESC"] = value;
			}
		}
		// model for database field INSU_TRANSFERED_IN_AGENCY_DESC(string)
		public string INSU_TRANSFERED_IN_AGENCY_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["INSU_TRANSFERED_IN_AGENCY_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSU_TRANSFERED_IN_AGENCY_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSU_TRANSFERED_IN_AGENCY_DESC"] = value;
			}
		}

		// model for database field PENDING_LITIGATIONS_DESC(string)
		public string PENDING_LITIGATIONS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PENDING_LITIGATIONS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PENDING_LITIGATIONS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PENDING_LITIGATIONS_DESC"] = value;
			}
		}

		// model for database field IS_TEMPOLINE_DESC(string)
		public string IS_TEMPOLINE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_TEMPOLINE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_TEMPOLINE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_TEMPOLINE_DESC"] = value;
			}
		}

		// model for database field HAVE_NON_OWNED_AUTO_POL_DESC(string)
		public string HAVE_NON_OWNED_AUTO_POL_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["HAVE_NON_OWNED_AUTO_POL_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HAVE_NON_OWNED_AUTO_POL_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HAVE_NON_OWNED_AUTO_POL_DESC"] = value;
			}
		}

		// model for database field INS_DOMICILED_OUTSIDE_DESC(string)
		public string INS_DOMICILED_OUTSIDE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["INS_DOMICILED_OUTSIDE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INS_DOMICILED_OUTSIDE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INS_DOMICILED_OUTSIDE_DESC"] = value;
			}
		}

		// model for database field HOME_DAY_CARE_DESC(string)
		public string HOME_DAY_CARE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_DAY_CARE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOME_DAY_CARE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOME_DAY_CARE_DESC"] = value;
			}
		}

		// model for database field HAVE_NON_OWNED_AUTO_POL(string)
		public string HAVE_NON_OWNED_AUTO_POL
		{
			get
			{
				return base.dtModel.Rows[0]["HAVE_NON_OWNED_AUTO_POL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HAVE_NON_OWNED_AUTO_POL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HAVE_NON_OWNED_AUTO_POL"] = value;
			}
		}

		// model for database field INS_DOMICILED_OUTSIDE(string)
		public string INS_DOMICILED_OUTSIDE
		{
			get
			{
				return base.dtModel.Rows[0]["INS_DOMICILED_OUTSIDE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INS_DOMICILED_OUTSIDE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INS_DOMICILED_OUTSIDE"] = value;
			}
		}

		// model for database field HOME_DAY_CARE(string)
		public string HOME_DAY_CARE
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_DAY_CARE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOME_DAY_CARE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOME_DAY_CARE"] = value;
			}
		}

		// model for database field UND_REMARKS(string)
		public string UND_REMARKS
		{
			get
			{
				return base.dtModel.Rows[0]["UND_REMARKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["UND_REMARKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UND_REMARKS"] = value;
			}
		}

		// model for database field CALCULATIONS(string)
		public string CALCULATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["CALCULATIONS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CALCULATIONS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CALCULATIONS"] = value;
			}
		}

		// model for database field HOME_RENT_DWELL(string)
		public string HOME_RENT_DWELL
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_RENT_DWELL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOME_RENT_DWELL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOME_RENT_DWELL"] = value;
			}
		}

		// model for database field HOME_RENT_DWELL_DESC(string)
		public string HOME_RENT_DWELL_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_RENT_DWELL_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOME_RENT_DWELL_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOME_RENT_DWELL_DESC"] = value;
			}
		}
		// model for database field WAT_DWELL(string)
		public string WAT_DWELL
		{
			get
			{
				return base.dtModel.Rows[0]["WAT_DWELL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WAT_DWELL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WAT_DWELL"] = value;
			}
		}
		// model for database field WAT_DWELL_DESC(string)
		public string WAT_DWELL_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["WAT_DWELL_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WAT_DWELL_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WAT_DWELL_DESC"] = value;
			}
		}
		// model for database field RECR_VEH(string)
		public string RECR_VEH
		{
			get
			{
				return base.dtModel.Rows[0]["RECR_VEH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RECR_VEH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECR_VEH"] = value;
			}
		}
		// model for database field RECR_VEH_DESC(string)
		public string RECR_VEH_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["RECR_VEH_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RECR_VEH_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECR_VEH_DESC"] = value;
			}
		}
		// model for database field AUTO_CYCL_TRUCKS(string)
		public string AUTO_CYCL_TRUCKS
		{
			get
			{
				return base.dtModel.Rows[0]["AUTO_CYCL_TRUCKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AUTO_CYCL_TRUCKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AUTO_CYCL_TRUCKS"] = value;
			}
		}
		// model for database field AUTO_CYCL_TRUCKS_DESC(string)
		public string AUTO_CYCL_TRUCKS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["AUTO_CYCL_TRUCKS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AUTO_CYCL_TRUCKS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AUTO_CYCL_TRUCKS_DESC"] = value;
			}
		}
		// model for database field APPLI_UNDERSTAND_LIABILITY_EXCLUDED(string)
		public string APPLI_UNDERSTAND_LIABILITY_EXCLUDED
		{
			get
			{
				return base.dtModel.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED"] = value;
			}
		}
		// model for database field APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC(string)
		public string APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC"] = value;
			}
		}
		//model for database field OFFICE_PREMISES
		
			public int OFFICE_PREMISES
			{
				get
				{
					return base.dtModel.Rows[0]["OFFICE_PREMISES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["OFFICE_PREMISES"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["OFFICE_PREMISES"] = value;
				}
			}
		//model for database field RENTAL_DWELLINGS_UNIT
		public int RENTAL_DWELLINGS_UNIT
		{
			get
			{
				return base.dtModel.Rows[0]["RENTAL_DWELLINGS_UNIT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RENTAL_DWELLINGS_UNIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RENTAL_DWELLINGS_UNIT"] = value;
			}
		}
	
		//model for database field FAMILIES
		public int FAMILIES
		{
			get
			{
				return base.dtModel.Rows[0]["FAMILIES"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FAMILIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FAMILIES"] = value;
			}
		}
		#endregion
	}
}
