/******************************************************************************************
<Author				: - Anurag Verma
<Start Date			: -	16/11/2005 5:40:31 PM
<End Date			: -	
<Description		: - Model class for POL_DWELLING_INFO table
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 

using System;
using System.Collections;
using System.Data;
using Cms.Model;

namespace Cms.Model.Policy.Homeowners
{
	/// <summary>
	/// Summary description for ClsPolicyHomeownerGeneralInfo.
	/// </summary>
	public class ClsPolicyHomeownerGeneralInfo: Cms.Model.ClsCommonModel
	{
        string strCalledFrom="";
		private const string POL_HOME_OWNER_GEN_INFO = "POL_HOME_OWNER_GEN_INFO";
		public ClsPolicyHomeownerGeneralInfo()
		{
			base.dtModel.TableName = POL_HOME_OWNER_GEN_INFO;		// setting table name for data table that holds property values.
			this.AddColumns();										// add columns of the database table APP_HOME_OWNER_GEN_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());			// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ANY_FARMING_BUSINESS_COND",typeof(string));
			base.dtModel.Columns.Add("ANY_RESIDENCE_EMPLOYEE",typeof(string));
			base.dtModel.Columns.Add("DESC_RESIDENCE_EMPLOYEE",typeof(string));
			base.dtModel.Columns.Add("ANY_OTHER_RESI_OWNED",typeof(string));
			base.dtModel.Columns.Add("DESC_OTHER_RESIDENCE",typeof(string));
			base.dtModel.Columns.Add("ANY_OTH_INSU_COMP",typeof(string));
			base.dtModel.Columns.Add("DESC_OTHER_INSURANCE",typeof(string));
			base.dtModel.Columns.Add("HAS_INSU_TRANSFERED_AGENCY",typeof(string));
			base.dtModel.Columns.Add("DESC_INSU_TRANSFERED_AGENCY",typeof(string));
			base.dtModel.Columns.Add("ANY_COV_DECLINED_CANCELED",typeof(string));
			base.dtModel.Columns.Add("DESC_COV_DECLINED_CANCELED",typeof(string));
			base.dtModel.Columns.Add("ANIMALS_EXO_PETS_HISTORY",typeof(int));
			base.dtModel.Columns.Add("BREED",typeof(string));
			base.dtModel.Columns.Add("OTHER_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("CONVICTION_DEGREE_IN_PAST",typeof(string));
			base.dtModel.Columns.Add("DESC_CONVICTION_DEGREE_IN_PAST",typeof(string));
			base.dtModel.Columns.Add("ANY_RENOVATION",typeof(string));
			base.dtModel.Columns.Add("DESC_RENOVATION",typeof(string));
			base.dtModel.Columns.Add("TRAMPOLINE",typeof(string));
			base.dtModel.Columns.Add("DESC_TRAMPOLINE",typeof(string));
			base.dtModel.Columns.Add("LEAD_PAINT_HAZARD",typeof(string));
			base.dtModel.Columns.Add("DESC_LEAD_PAINT_HAZARD",typeof(string));
			base.dtModel.Columns.Add("RENTERS",typeof(string));
			base.dtModel.Columns.Add("DESC_RENTERS",typeof(string));
			base.dtModel.Columns.Add("BUILD_UNDER_CON_GEN_CONT",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("MULTI_POLICY_DISC_APPLIED",typeof(string));
			base.dtModel.Columns.Add("NO_OF_PETS",typeof(int));
			base.dtModel.Columns.Add("IS_SWIMPOLL_HOTTUB",typeof(string));
			base.dtModel.Columns.Add("LAST_INSPECTED_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("IS_RENTED_IN_PART",typeof(string));
			base.dtModel.Columns.Add("IS_VACENT_OCCUPY",typeof(string));
			base.dtModel.Columns.Add("IS_DWELLING_OWNED_BY_OTHER",typeof(string));
			base.dtModel.Columns.Add("IS_PROP_NEXT_COMMERICAL",typeof(string));
			base.dtModel.Columns.Add("DESC_PROPERTY",typeof(string));
			base.dtModel.Columns.Add("ARE_STAIRWAYS_PRESENT",typeof(string));
			base.dtModel.Columns.Add("DESC_STAIRWAYS",typeof(string));
			base.dtModel.Columns.Add("IS_OWNERS_DWELLING_CHANGED",typeof(string));
			base.dtModel.Columns.Add("DESC_OWNER",typeof(string));
			base.dtModel.Columns.Add("DESC_VACENT_OCCUPY",typeof(string));
			base.dtModel.Columns.Add("DESC_RENTED_IN_PART",typeof(string));
			base.dtModel.Columns.Add("DESC_DWELLING_OWNED_BY_OTHER",typeof(string));
			base.dtModel.Columns.Add("DESC_ANY_HEATING_SOURCE",typeof(string));
			base.dtModel.Columns.Add("ANY_HEATING_SOURCE",typeof(string));
			base.dtModel.Columns.Add("NON_SMOKER_CREDIT",typeof(string));
			base.dtModel.Columns.Add("SWIMMING_POOL",typeof(string));
			base.dtModel.Columns.Add("SWIMMING_POOL_TYPE",typeof(string));
			base.dtModel.Columns.Add("YEARS_INSU",typeof(int));
			base.dtModel.Columns.Add("YEARS_INSU_WOL",typeof(int));

			//LOSSES
			base.dtModel.Columns.Add("NON_WEATHER_CLAIMS",typeof(int));
			base.dtModel.Columns.Add("WEATHER_CLAIMS",typeof(int));

			base.dtModel.Columns.Add("Any_Forming",typeof(string));
			base.dtModel.Columns.Add("Premises",typeof(int));
			base.dtModel.Columns.Add("Of_Acres",typeof(double));
			base.dtModel.Columns.Add("IsAny_Horse",typeof(string));
			base.dtModel.Columns.Add("Of_Acres_P",typeof(double));
			base.dtModel.Columns.Add("No_Horses",typeof(int));			
			base.dtModel.Columns.Add("Location",typeof(string));
			base.dtModel.Columns.Add("DESC_Location",typeof(string));
			//Sumit Chhabra:30-03-2006:Adding description fields for 
			//IS_SWIMPOLL_HOTTUB,MULTI_POLICY_DISC_APPLIED,BUILD_UNDER_CON_GEN_CONT
			base.dtModel.Columns.Add("DESC_IS_SWIMPOLL_HOTTUB",typeof(string));
			base.dtModel.Columns.Add("DESC_MULTI_POLICY_DISC_APPLIED",typeof(string));
			base.dtModel.Columns.Add("DESC_BUILD_UNDER_CON_GEN_CONT",typeof(string));
			base.dtModel.Columns.Add("APPROVED_FENCE",typeof(int));
			base.dtModel.Columns.Add("DIVING_BOARD",typeof(int));
			base.dtModel.Columns.Add("SLIDE",typeof(int));
			base.dtModel.Columns.Add("DESC_FARMING_BUSINESS_COND",typeof(string));
			base.dtModel.Columns.Add("PROVIDE_HOME_DAY_CARE",typeof(string));

			base.dtModel.Columns.Add("MODULAR_MANUFACTURED_HOME",typeof(string));
			base.dtModel.Columns.Add("BUILT_ON_CONTINUOUS_FOUNDATION",typeof(string));
			base.dtModel.Columns.Add("VALUED_CUSTOMER_DISCOUNT_OVERRIDE",typeof(string));
			base.dtModel.Columns.Add("VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC",typeof(string));

			base.dtModel.Columns.Add("PROPERTY_ON_MORE_THAN",typeof(string));
			base.dtModel.Columns.Add("PROPERTY_ON_MORE_THAN_DESC",typeof(string));
			base.dtModel.Columns.Add("DWELLING_MOBILE_HOME",typeof(string));
			base.dtModel.Columns.Add("DWELLING_MOBILE_HOME_DESC",typeof(string));
			base.dtModel.Columns.Add("PROPERTY_USED_WHOLE_PART",typeof(string));
			base.dtModel.Columns.Add("PROPERTY_USED_WHOLE_PART_DESC",typeof(string));
			
			base.dtModel.Columns.Add("ANY_PRIOR_LOSSES",typeof(string));
			base.dtModel.Columns.Add("ANY_PRIOR_LOSSES_DESC",typeof(string));
			
			base.dtModel.Columns.Add("BOAT_WITH_HOMEOWNER",typeof(string));

		}
		#region Database schema details
		// model for database field SWIMMING_POOL(string)
		public string SWIMMING_POOL
		{
			get
			{
				return base.dtModel.Rows[0]["SWIMMING_POOL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SWIMMING_POOL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SWIMMING_POOL"] = value;
			}
		}

		// model for database field SWIMMING_POOL_TYPE(string)
		public string SWIMMING_POOL_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SWIMMING_POOL_TYPE"] = value;
			}
		}

		// model for database field YEARS_INSU(string)
		public int YEARS_INSU
		{
			get
			{
				return base.dtModel.Rows[0]["YEARS_INSU"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["YEARS_INSU"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_INSU"] = value;
			}
		}

		// model for database field YEARS_INSU_WOL(int)
		public int YEARS_INSU_WOL
		{
			get
			{
				return base.dtModel.Rows[0]["YEARS_INSU_WOL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["YEARS_INSU_WOL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEARS_INSU_WOL"] = value;
			}
		}

		//LOSSES --
		public int NON_WEATHER_CLAIMS
		{
			get
			{
				return base.dtModel.Rows[0]["NON_WEATHER_CLAIMS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NON_WEATHER_CLAIMS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NON_WEATHER_CLAIMS"] = value;
			}
		}

		public int WEATHER_CLAIMS
		{
			get
			{
				return base.dtModel.Rows[0]["WEATHER_CLAIMS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WEATHER_CLAIMS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WEATHER_CLAIMS"] = value;
			}
		}



		// model for database field NON_SMOKER_CREDIT(string)
		public string NON_SMOKER_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["NON_SMOKER_CREDIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NON_SMOKER_CREDIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NON_SMOKER_CREDIT"] = value;
			}
		}

		// model for database field LAST_INSPECTED_DATE(DateTime)
		public DateTime LAST_INSPECTED_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["LAST_INSPECTED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_INSPECTED_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LAST_INSPECTED_DATE"] = value;
			}
		}
		// model for database field IS_SWIMPOLL_HOTTUB(string)
		public string IS_SWIMPOLL_HOTTUB
		{
			get
			{
				return base.dtModel.Rows[0]["IS_SWIMPOLL_HOTTUB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_SWIMPOLL_HOTTUB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_SWIMPOLL_HOTTUB"] = value;
			}
		}
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
		// model for database field ANY_FARMING_BUSINESS_COND(string)
		public string ANY_FARMING_BUSINESS_COND
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_FARMING_BUSINESS_COND"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_FARMING_BUSINESS_COND"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_FARMING_BUSINESS_COND"] = value;
			}
		}
		// model for database field DESC_BUSINESS(string)
		//		public string DESC_BUSINESS
		//		{
		//			get
		//			{
		//				return base.dtModel.Rows[0]["DESC_BUSINESS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_BUSINESS"].ToString();
		//			}
		//			set
		//			{
		//				base.dtModel.Rows[0]["DESC_BUSINESS"] = value;
		//			}
		//		}
		// model for database field ANY_RESIDENCE_EMPLOYEE(string)
		public string ANY_RESIDENCE_EMPLOYEE
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_RESIDENCE_EMPLOYEE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_RESIDENCE_EMPLOYEE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_RESIDENCE_EMPLOYEE"] = value;
			}
		}
		// model for database field DESC_RESIDENCE_EMPLOYEE(string)
		public string DESC_RESIDENCE_EMPLOYEE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_RESIDENCE_EMPLOYEE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_RESIDENCE_EMPLOYEE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_RESIDENCE_EMPLOYEE"] = value;
			}
		}
		// model for database field ANY_OTHER_RESI_OWNED(string)
		public string ANY_OTHER_RESI_OWNED
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OTHER_RESI_OWNED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OTHER_RESI_OWNED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OTHER_RESI_OWNED"] = value;
			}
		}
		// model for database field DESC_OTHER_RESIDENCE(string)
		public string DESC_OTHER_RESIDENCE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_OTHER_RESIDENCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_OTHER_RESIDENCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_OTHER_RESIDENCE"] = value;
			}
		}
		// model for database field ANY_OTH_INSU_COMP(string)
		public string ANY_OTH_INSU_COMP
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OTH_INSU_COMP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OTH_INSU_COMP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OTH_INSU_COMP"] = value;
			}
		}
		// model for database field DESC_OTHER_INSURANCE(string)
		public string DESC_OTHER_INSURANCE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_OTHER_INSURANCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_OTHER_INSURANCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_OTHER_INSURANCE"] = value;
			}
		}
		// model for database field HAS_INSU_TRANSFERED_AGENCY(string)
		public string HAS_INSU_TRANSFERED_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["HAS_INSU_TRANSFERED_AGENCY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HAS_INSU_TRANSFERED_AGENCY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HAS_INSU_TRANSFERED_AGENCY"] = value;
			}
		}
		// model for database field DESC_INSU_TRANSFERED_AGENCY(string)
		public string DESC_INSU_TRANSFERED_AGENCY
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_INSU_TRANSFERED_AGENCY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_INSU_TRANSFERED_AGENCY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_INSU_TRANSFERED_AGENCY"] = value;
			}
		}
		// model for database field ANY_COV_DECLINED_CANCELED(string)
		public string ANY_COV_DECLINED_CANCELED
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_COV_DECLINED_CANCELED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_COV_DECLINED_CANCELED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_COV_DECLINED_CANCELED"] = value;
			}
		}
		// model for database field DESC_COV_DECLINED_CANCELED(string)
		public string DESC_COV_DECLINED_CANCELED
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_COV_DECLINED_CANCELED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_COV_DECLINED_CANCELED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_COV_DECLINED_CANCELED"] = value;
			}
		}
		// model for database field ANIMALS_EXO_PETS_HISTORY(string)
		public int ANIMALS_EXO_PETS_HISTORY
		{
			get
			{
				return base.dtModel.Rows[0]["ANIMALS_EXO_PETS_HISTORY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ANIMALS_EXO_PETS_HISTORY"].ToString());
				
			}
			set
			{
				base.dtModel.Rows[0]["ANIMALS_EXO_PETS_HISTORY"] = value;
			}
		}
		// model for database field BREED(string)
		public string BREED
		{
			get
			{
				return base.dtModel.Rows[0]["BREED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BREED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BREED"] = value;
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

		// model for database field CONVICTION_DEGREE_IN_PAST(string)
		public string CONVICTION_DEGREE_IN_PAST
		{
			get
			{
				return base.dtModel.Rows[0]["CONVICTION_DEGREE_IN_PAST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONVICTION_DEGREE_IN_PAST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONVICTION_DEGREE_IN_PAST"] = value;
			}
		}
		// model for database field DESC_CONVICTION_DEGREE_IN_PAST(string)
		public string DESC_CONVICTION_DEGREE_IN_PAST
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"] = value;
			}
		}

		// model for database field ANY_RENOVATION(string)
		public string ANY_RENOVATION
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_RENOVATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_RENOVATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_RENOVATION"] = value;
			}
		}
		// model for database field DESC_RENOVATION(string)
		public string DESC_RENOVATION
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_RENOVATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_RENOVATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_RENOVATION"] = value;
			}
		}

		// model for database field TRAMPOLINE(string)
		public string TRAMPOLINE
		{
			get
			{
				return base.dtModel.Rows[0]["TRAMPOLINE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TRAMPOLINE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRAMPOLINE"] = value;
			}
		}
		// model for database field DESC_TRAMPOLINE(string)
		public string DESC_TRAMPOLINE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_TRAMPOLINE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_TRAMPOLINE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_TRAMPOLINE"] = value;
			}
		}

		// model for database field LEAD_PAINT_HAZARD(string)
		public string LEAD_PAINT_HAZARD
		{
			get
			{
				return base.dtModel.Rows[0]["LEAD_PAINT_HAZARD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LEAD_PAINT_HAZARD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LEAD_PAINT_HAZARD"] = value;
			}
		}
		// model for database field DESC_LEAD_PAINT_HAZARD(string)
		public string DESC_LEAD_PAINT_HAZARD
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_LEAD_PAINT_HAZARD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_LEAD_PAINT_HAZARD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_LEAD_PAINT_HAZARD"] = value;
			}
		}

		// model for database field RENTERS(string)
		public string RENTERS
		{
			get
			{
				return base.dtModel.Rows[0]["RENTERS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RENTERS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RENTERS"] = value;
			}
		}
		// model for database field DESC_RENTERS(string)
		public string DESC_RENTERS
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_RENTERS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_RENTERS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_RENTERS"] = value;
			}
		}
		// model for database field BUILD_UNDER_CON_GEN_CONT(string)
		public string BUILD_UNDER_CON_GEN_CONT
		{
			get
			{
				return base.dtModel.Rows[0]["BUILD_UNDER_CON_GEN_CONT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BUILD_UNDER_CON_GEN_CONT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BUILD_UNDER_CON_GEN_CONT"] = value;
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

		public string MULTI_POLICY_DISC_APPLIED
		{
			get
			{
				return base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED"] = value;
			}
		}

		public int NO_OF_PETS
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_PETS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_OF_PETS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_PETS"] = value;
			}
		}

		//---------------------------- Added by mohit------------------
		
		
			
		

		// model for database field IS_RENTED_IN_PART(string)
		public string IS_RENTED_IN_PART
		{
			get
			{
				return base.dtModel.Rows[0]["IS_RENTED_IN_PART"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_RENTED_IN_PART"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_RENTED_IN_PART"] = value;
			}
		}
		// model for database field IS_VACENT_OCCUPY(string)
		public string IS_VACENT_OCCUPY
		{
			get
			{
				return base.dtModel.Rows[0]["IS_VACENT_OCCUPY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_VACENT_OCCUPY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_VACENT_OCCUPY"] = value;
			}
		}

		

		// model for database field IS_DWELLING_OWNED_BY_OTHER(string)
		public string IS_DWELLING_OWNED_BY_OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"] = value;
			}
		}
		// model for database field IS_PROP_NEXT_COMMERICAL(string)
		public string IS_PROP_NEXT_COMMERICAL
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PROP_NEXT_COMMERICAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_PROP_NEXT_COMMERICAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_PROP_NEXT_COMMERICAL"] = value;
			}
		}
		
        
		// model for database field DESC_PROPERTY(string)
		public string DESC_PROPERTY
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_PROPERTY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_PROPERTY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_PROPERTY"] = value;
			}
		}
		// model for database field ARE_STAIRWAYS_PRESENT(string)
		public string ARE_STAIRWAYS_PRESENT
		{
			get
			{
				return base.dtModel.Rows[0]["ARE_STAIRWAYS_PRESENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ARE_STAIRWAYS_PRESENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ARE_STAIRWAYS_PRESENT"] = value;
			}
		}

						
						
	

		// model for database field DESC_STAIRWAYS(string)
		public string DESC_STAIRWAYS
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_STAIRWAYS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_STAIRWAYS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_STAIRWAYS"] = value;
			}
		}
		// model for database field IS_OWNERS_DWELLING_CHANGED(string)
		public string IS_OWNERS_DWELLING_CHANGED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_OWNERS_DWELLING_CHANGED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_OWNERS_DWELLING_CHANGED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_OWNERS_DWELLING_CHANGED"] = value;
			}
		}


		// model for database field DESC_OWNER(string)
		public string DESC_OWNER
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_OWNER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_OWNER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_OWNER"] = value;
			}
		}

		
	
	

		// model for database field DESC_VACENT_OCCUPY(string)
		public string DESC_VACENT_OCCUPY
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_VACENT_OCCUPY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_VACENT_OCCUPY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_VACENT_OCCUPY"] = value;
			}
		}

		// model for database field DESC_RENTED_IN_PART(string)
		public string DESC_RENTED_IN_PART
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_RENTED_IN_PART"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_RENTED_IN_PART"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_RENTED_IN_PART"] = value;
			}
		}

		// model for database field DESC_DWELLING_OWNED_BY_OTHER(string)
		public string DESC_DWELLING_OWNED_BY_OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_DWELLING_OWNED_BY_OTHER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_DWELLING_OWNED_BY_OTHER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_DWELLING_OWNED_BY_OTHER"] = value;
			}
		}
		
		// model for database field DESC_ANY_HEATING_SOURCE(string)
		public string DESC_ANY_HEATING_SOURCE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_ANY_HEATING_SOURCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_ANY_HEATING_SOURCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_ANY_HEATING_SOURCE"] = value;
			}
		}

		public string DESC_FARMING_BUSINESS_COND
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_FARMING_BUSINESS_COND"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_FARMING_BUSINESS_COND"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_FARMING_BUSINESS_COND"] = value;
			}
		}

		// model for database field PROVIDE_HOME_DAY_CARE(string)
		public string PROVIDE_HOME_DAY_CARE
		{
			get
			{
				return base.dtModel.Rows[0]["PROVIDE_HOME_DAY_CARE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROVIDE_HOME_DAY_CARE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PROVIDE_HOME_DAY_CARE"] = value;
			}
		}
		
		// model for database field ANY_HEATING_SOURCE(string)
		public string ANY_HEATING_SOURCE
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_HEATING_SOURCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_HEATING_SOURCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_HEATING_SOURCE"] = value;
			}
		}

		public string Any_Forming
		{
			get
			{
				return base.dtModel.Rows[0]["Any_Forming"]==DBNull.Value ? Convert.ToString(null):base.dtModel.Rows[0]["Any_Forming"].ToString() ;
			}
			set
			{
				base.dtModel.Rows[0]["Any_Forming"] = value;
			}
		}
		public int Premises
		{
			get
			{
				return base.dtModel.Rows[0]["Premises"]==DBNull.Value ? Convert.ToInt32(null):int.Parse(base.dtModel.Rows[0]["Premises"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Premises"] = value;
			}
		}
		public double Of_Acres
		{
			get
			{
				return base.dtModel.Rows[0]["Of_Acres"]==DBNull.Value ? Convert.ToDouble(null):double.Parse(base.dtModel.Rows[0]["Of_Acres"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Of_Acres"] = value;
			}
		}
		public int No_Horses
		{
			get
			{
				return base.dtModel.Rows[0]["No_Horses"]==DBNull.Value ? Convert.ToInt32(null):int.Parse(base.dtModel.Rows[0]["No_Horses"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["No_Horses"] = value;
			}
		}
		public string IsAny_Horse
		{
			get
			{
				return base.dtModel.Rows[0]["IsAny_Horse"]==DBNull.Value ? Convert.ToString(null):base.dtModel.Rows[0]["IsAny_Horse"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IsAny_Horse"] = value;
			}
		}
		public double Of_Acres_P
		{
			get
			{
				return base.dtModel.Rows[0]["Of_Acres_P"]==DBNull.Value ? Convert.ToDouble (null):double.Parse(base.dtModel.Rows[0]["Of_Acres_P"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Of_Acres_P"] = value;
			}
		}
		public string Location
		{
			get
			{
				return base.dtModel.Rows[0]["Location"]==DBNull.Value ? Convert.ToString(null):base.dtModel.Rows[0]["Location"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["Location"] = value;
			}
		}
		public string DESC_Location
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_Location"]==DBNull.Value ? Convert.ToString(null):base.dtModel.Rows[0]["DESC_Location"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_Location"] = value;
			}
		}
		// model for database field DESC_IS_SWIMPOLL_HOTTUB(string)
		public string DESC_IS_SWIMPOLL_HOTTUB
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_IS_SWIMPOLL_HOTTUB"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_IS_SWIMPOLL_HOTTUB"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_IS_SWIMPOLL_HOTTUB"] = value;
			}
		}
		// model for database field DESC_MULTI_POLICY_DISC_APPLIED(string)
		public string DESC_MULTI_POLICY_DISC_APPLIED
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_MULTI_POLICY_DISC_APPLIED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_MULTI_POLICY_DISC_APPLIED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_MULTI_POLICY_DISC_APPLIED"] = value;
			}
		}
		// model for database field DESC_BUILD_UNDER_CON_GEN_CONT(string)
		public string DESC_BUILD_UNDER_CON_GEN_CONT
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_BUILD_UNDER_CON_GEN_CONT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_BUILD_UNDER_CON_GEN_CONT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_BUILD_UNDER_CON_GEN_CONT"] = value;
			}
		}
		public int APPROVED_FENCE
		{
			get
			{
				return base.dtModel.Rows[0]["APPROVED_FENCE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APPROVED_FENCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APPROVED_FENCE"] = value;
			}
		}
		public int DIVING_BOARD
		{
			get
			{
				return base.dtModel.Rows[0]["DIVING_BOARD"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DIVING_BOARD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIVING_BOARD"] = value;
			}
		}
		public int SLIDE
		{
			get
			{
				return base.dtModel.Rows[0]["SLIDE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SLIDE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SLIDE"] = value;
			}
		}

		// model for database field MODULAR_MANUFACTURED_HOME(string)
		public string MODULAR_MANUFACTURED_HOME
		{
			get {return base.dtModel.Rows[0]["MODULAR_MANUFACTURED_HOME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MODULAR_MANUFACTURED_HOME"].ToString();}
			set {base.dtModel.Rows[0]["MODULAR_MANUFACTURED_HOME"] = value;}
		}
		
		// model for database field BUILT_ON_CONTINUOUS_FOUNDATION(string)
		public string BUILT_ON_CONTINUOUS_FOUNDATION
		{
			get {return base.dtModel.Rows[0]["BUILT_ON_CONTINUOUS_FOUNDATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BUILT_ON_CONTINUOUS_FOUNDATION"].ToString();}
			set {base.dtModel.Rows[0]["BUILT_ON_CONTINUOUS_FOUNDATION"] = value;}
		}
		
		// model for database field VALUED_CUSTOMER_DISCOUNT_OVERRIDE(string)
		public string VALUED_CUSTOMER_DISCOUNT_OVERRIDE
		{
			get {return base.dtModel.Rows[0]["VALUED_CUSTOMER_DISCOUNT_OVERRIDE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VALUED_CUSTOMER_DISCOUNT_OVERRIDE"].ToString();}
			set {base.dtModel.Rows[0]["VALUED_CUSTOMER_DISCOUNT_OVERRIDE"] = value;}
		}
		
		// model for database field VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC(string)
		public string VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC
		{
			get {return base.dtModel.Rows[0]["VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC"].ToString();}
			set {base.dtModel.Rows[0]["VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC"] = value;}
		}

		// model for database field PROPERTY_ON_MORE_THAN(string)
		public string PROPERTY_ON_MORE_THAN
		{
			get {return base.dtModel.Rows[0]["PROPERTY_ON_MORE_THAN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROPERTY_ON_MORE_THAN"].ToString();}
			set {base.dtModel.Rows[0]["PROPERTY_ON_MORE_THAN"] = value;}
		}
		
		// model for database field PROPERTY_ON_MORE_THAN_DESC(string)
		public string PROPERTY_ON_MORE_THAN_DESC
		{
			get {return base.dtModel.Rows[0]["PROPERTY_ON_MORE_THAN_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROPERTY_ON_MORE_THAN_DESC"].ToString();}
			set {base.dtModel.Rows[0]["PROPERTY_ON_MORE_THAN_DESC"] = value;}
		}
		
		// model for database field DWELLING_MOBILE_HOME(string)
		public string DWELLING_MOBILE_HOME
		{
			get {return base.dtModel.Rows[0]["DWELLING_MOBILE_HOME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DWELLING_MOBILE_HOME"].ToString();}
			set {base.dtModel.Rows[0]["DWELLING_MOBILE_HOME"] = value;}
		}
		
		// model for database field DWELLING_MOBILE_HOME_DESC(string)
		public string DWELLING_MOBILE_HOME_DESC
		{
			get {return base.dtModel.Rows[0]["DWELLING_MOBILE_HOME_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DWELLING_MOBILE_HOME_DESC"].ToString();}
			set {base.dtModel.Rows[0]["DWELLING_MOBILE_HOME_DESC"] = value;}
		}
		
		// model for database field PROPERTY_USED_WHOLE_PART(string)
		public string PROPERTY_USED_WHOLE_PART
		{
			get {return base.dtModel.Rows[0]["PROPERTY_USED_WHOLE_PART"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROPERTY_USED_WHOLE_PART"].ToString();}
			set {base.dtModel.Rows[0]["PROPERTY_USED_WHOLE_PART"] = value;}
		}
		
		
		// model for database field PROPERTY_USED_WHOLE_PART_DESC(string)
		public string PROPERTY_USED_WHOLE_PART_DESC
		{
			get {return base.dtModel.Rows[0]["PROPERTY_USED_WHOLE_PART_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PROPERTY_USED_WHOLE_PART_DESC"].ToString();}
			set {base.dtModel.Rows[0]["PROPERTY_USED_WHOLE_PART_DESC"] = value;}
		}
		
		public string CalledFrom
		{
			get 
			{
				return strCalledFrom.ToString().Trim().ToUpper();
			}
			set
			{
				strCalledFrom=value;
			}
		}

		// model for database field ANY_PRIOR_LOSSES(string)
		public string ANY_PRIOR_LOSSES
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_PRIOR_LOSSES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_PRIOR_LOSSES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_PRIOR_LOSSES"] = value;
			}
		}
		// model for database field ANY_PRIOR_LOSSES_DESC(string)
		public string ANY_PRIOR_LOSSES_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_PRIOR_LOSSES_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_PRIOR_LOSSES_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_PRIOR_LOSSES_DESC"] = value;
			}
		}
		//Model BOAT_WITH_HOMEOWNER
		public string BOAT_WITH_HOMEOWNER
		{
			get
			{
				return base.dtModel.Rows[0]["BOAT_WITH_HOMEOWNER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BOAT_WITH_HOMEOWNER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BOAT_WITH_HOMEOWNER"] = value;
			}
		}


		#endregion
	}
}
