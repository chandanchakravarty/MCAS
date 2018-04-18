/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/16/2005 4:32:12 PM
<End Date				: -	
<Description				: - 	Models APP_WATERCRAFT_GEN_INFO
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
namespace Cms.Model.Application.Watercrafts
{
    /// <summary>
    /// Database Model for APP_WATERCRAFT_GEN_INFO.
    /// </summary>
    public class ClsWatercraftGenInfo : Cms.Model.ClsCommonModel
    {
        private const string APP_WATERCRAFT_GEN_INFO = "APP_WATERCRAFT_GEN_INFO";
        public ClsWatercraftGenInfo()
        {
            base.dtModel.TableName = "APP_WATERCRAFT_GEN_INFO";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table APP_WATERCRAFT_GEN_INFO
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
            base.dtModel.Columns.Add("APP_ID",typeof(int));
            base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
            base.dtModel.Columns.Add("HAS_CURR_ADD_THREE_YEARS",typeof(string));
            base.dtModel.Columns.Add("PHY_MENTL_CHALLENGED",typeof(string));
            base.dtModel.Columns.Add("DRIVER_SUS_REVOKED",typeof(string));
            base.dtModel.Columns.Add("IS_CONVICTED_ACCIDENT",typeof(string));
            base.dtModel.Columns.Add("ANY_OTH_INSU_COMP",typeof(string));
            base.dtModel.Columns.Add("OTHER_POLICY_NUMBER_LIST",typeof(string));
            base.dtModel.Columns.Add("ANY_LOSS_THREE_YEARS",typeof(string));
            base.dtModel.Columns.Add("COVERAGE_DECLINED",typeof(string));
			//Nov14,2005:Sumit Chhabra:Removed the field 
            //base.dtModel.Columns.Add("DEGREE_CONVICTION",typeof(string));
            base.dtModel.Columns.Add("IS_CREDIT",typeof(string));
            base.dtModel.Columns.Add("CREDIT_DETAILS",typeof(string));
            base.dtModel.Columns.Add("IS_RENTED_OTHERS",typeof(string));
            base.dtModel.Columns.Add("IS_REGISTERED_OTHERS",typeof(string));

			base.dtModel.Columns.Add("HAS_CURR_ADD_THREE_YEARS_DESC",typeof(string));
		    base.dtModel.Columns.Add("PHY_MENTL_CHALLENGED_DESC",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SUS_REVOKED_DESC",typeof(string));			
			base.dtModel.Columns.Add("IS_CONVICTED_ACCIDENT_DESC",typeof(string));
		//	base.dtModel.Columns.Add("ANY_OTH_INSU_COMP_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_LOSS_THREE_YEARS_DESC",typeof(string));
		    base.dtModel.Columns.Add("COVERAGE_DECLINED_DESC",typeof(string));
	    	//base.dtModel.Columns.Add("DEGREE_CONVICTION_DESC",typeof(string));
		   
            base.dtModel.Columns.Add("IS_RENTED_OTHERS_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_REGISTERED_OTHERS_DESC",typeof(string));
			base.dtModel.Columns.Add("DRINK_DRUG_VOILATION",typeof(string));
			base.dtModel.Columns.Add("MINOR_VIOLATION",typeof(string));

			base.dtModel.Columns.Add("PARTICIPATE_RACE",typeof(string));
			base.dtModel.Columns.Add("CARRY_PASSENGER_FOR_CHARGE",typeof(string));
			base.dtModel.Columns.Add("PARTICIPATE_RACE_DESC",typeof(string));
			base.dtModel.Columns.Add("CARRY_PASSENGER_FOR_CHARGE_DESC",typeof(string));

			// added By Shafi

			base.dtModel.Columns.Add("IS_PRIOR_INSURANCE_CARRIER",typeof(string));
			base.dtModel.Columns.Add("PRIOR_INSURANCE_CARRIER_DESC",typeof(string));

			base.dtModel.Columns.Add("IS_BOAT_COOWNED",typeof(string));
			base.dtModel.Columns.Add("IS_BOAT_COOWNED_DESC",typeof(string));
			//Added by praveen K : 15 feb 2006
			base.dtModel.Columns.Add("BOAT_HOME_DISCOUNT",typeof(string));

			//Added By Shafi 20-03-2006

			base.dtModel.Columns.Add("MULTI_POLICY_DISC_APPLIED",typeof(string));
			base.dtModel.Columns.Add("MULTI_POLICY_DISC_APPLIED_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_BOAT_AMPHIBIOUS",typeof(string));
			base.dtModel.Columns.Add("ANY_BOAT_AMPHIBIOUS_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_BOAT_RESIDENCE",typeof(string));
			base.dtModel.Columns.Add("ANY_BOAT_RESIDENCE_DESC",typeof(string));
			//Added By Praveen 19 sep 2007
			base.dtModel.Columns.Add("IS_BOAT_USED_IN_ANY_WATER",typeof(string));
			base.dtModel.Columns.Add("IS_BOAT_USED_IN_ANY_WATER_DESC",typeof(string));
			
			


			

			
			
			
        }
        #region Database schema details
		
        
		// model for database field CARRY_PASSENGER_FOR_CHARGE_DESC(string)
		public string CARRY_PASSENGER_FOR_CHARGE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["CARRY_PASSENGER_FOR_CHARGE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARRY_PASSENGER_FOR_CHARGE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARRY_PASSENGER_FOR_CHARGE_DESC"] = value;
			}
		}

		// model for database field PARTICIPATE_RACE_DESC(string)
		public string PARTICIPATE_RACE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PARTICIPATE_RACE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PARTICIPATE_RACE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PARTICIPATE_RACE_DESC"] = value;
			}
		}


		
		// model for database field CARRY_PASSENGER_FOR_CHARGE(string)
		public string CARRY_PASSENGER_FOR_CHARGE
		{
			get
			{
				return base.dtModel.Rows[0]["CARRY_PASSENGER_FOR_CHARGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CARRY_PASSENGER_FOR_CHARGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CARRY_PASSENGER_FOR_CHARGE"] = value;
			}
		}

		// model for database field PARTICIPATE_RACE(string)
		public string PARTICIPATE_RACE
		{
			get
			{
				return base.dtModel.Rows[0]["PARTICIPATE_RACE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PARTICIPATE_RACE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PARTICIPATE_RACE"] = value;
			}
		}
		



		// model for database field MINOR_VIOLATION(string)
		public string MINOR_VIOLATION
		{
			get
			{
				return base.dtModel.Rows[0]["MINOR_VIOLATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MINOR_VIOLATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MINOR_VIOLATION"] = value;
			}
		}
		// model for database field DRINK_DRUG_VOILATION(string)
		public string DRINK_DRUG_VOILATION
		{
			get
			{
				return base.dtModel.Rows[0]["DRINK_DRUG_VOILATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRINK_DRUG_VOILATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRINK_DRUG_VOILATION"] = value;
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
        // model for database field HAS_CURR_ADD_THREE_YEARS(string)
        public string HAS_CURR_ADD_THREE_YEARS
        {
            get
            {
                return base.dtModel.Rows[0]["HAS_CURR_ADD_THREE_YEARS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HAS_CURR_ADD_THREE_YEARS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["HAS_CURR_ADD_THREE_YEARS"] = value;
            }
        }
        // model for database field PHY_MENTL_CHALLENGED(string)
        public string PHY_MENTL_CHALLENGED
        {
            get
            {
                return base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED"] = value;
            }
        }
        // model for database field DRIVER_SUS_REVOKED(string)
        public string DRIVER_SUS_REVOKED
        {
            get
            {
                return base.dtModel.Rows[0]["DRIVER_SUS_REVOKED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SUS_REVOKED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DRIVER_SUS_REVOKED"] = value;
            }
        }
        // model for database field IS_CONVICTED_ACCIDENT(string)
        public string IS_CONVICTED_ACCIDENT
        {
            get
            {
                return base.dtModel.Rows[0]["IS_CONVICTED_ACCIDENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_CONVICTED_ACCIDENT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_CONVICTED_ACCIDENT"] = value;
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
        // model for database field OTHER_POLICY_NUMBER_LIST(string)
        public string OTHER_POLICY_NUMBER_LIST
        {
            get
            {
                return base.dtModel.Rows[0]["OTHER_POLICY_NUMBER_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_POLICY_NUMBER_LIST"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["OTHER_POLICY_NUMBER_LIST"] = value;
            }
        }
        // model for database field ANY_LOSS_THREE_YEARS(string)
        public string ANY_LOSS_THREE_YEARS
        {
            get
            {
                return base.dtModel.Rows[0]["ANY_LOSS_THREE_YEARS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_LOSS_THREE_YEARS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ANY_LOSS_THREE_YEARS"] = value;
            }
        }
        // model for database field COVERAGE_DECLINED(string)
        public string COVERAGE_DECLINED
        {
            get
            {
                return base.dtModel.Rows[0]["COVERAGE_DECLINED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_DECLINED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["COVERAGE_DECLINED"] = value;
            }
        }
        // model for database field DEGREE_CONVICTION(string)
        /*public string DEGREE_CONVICTION
        {
            get
            {
                return base.dtModel.Rows[0]["DEGREE_CONVICTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEGREE_CONVICTION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DEGREE_CONVICTION"] = value;
            }
        }*/
        // model for database field IS_CREDIT(string)
        public string IS_CREDIT
        {
            get
            {
                return base.dtModel.Rows[0]["IS_CREDIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_CREDIT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_CREDIT"] = value;
            }
        }
        // model for database field CREDIT_DETAILS(string)
        public string CREDIT_DETAILS
        {
            get
            {
                return base.dtModel.Rows[0]["CREDIT_DETAILS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CREDIT_DETAILS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CREDIT_DETAILS"] = value;
            }
        }
        // model for database field IS_RENTED_OTHERS(string)
        public string IS_RENTED_OTHERS
        {
            get
            {
                return base.dtModel.Rows[0]["IS_RENTED_OTHERS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_RENTED_OTHERS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_RENTED_OTHERS"] = value;
            }
        }
        // model for database field IS_REGISTERED_OTHERS(string)
        public string IS_REGISTERED_OTHERS
        {
            get
            {
                return base.dtModel.Rows[0]["IS_REGISTERED_OTHERS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_REGISTERED_OTHERS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_REGISTERED_OTHERS"] = value;
            }
        }

		public string HAS_CURR_ADD_THREE_YEARS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["HAS_CURR_ADD_THREE_YEARS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HAS_CURR_ADD_THREE_YEARS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HAS_CURR_ADD_THREE_YEARS_DESC"] = value;
			}
		}
		public string PHY_MENTL_CHALLENGED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_DESC"] = value;
			}
		}
		public string DRIVER_SUS_REVOKED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_DESC"] = value;
			}
		}
		public string IS_CONVICTED_ACCIDENT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_CONVICTED_ACCIDENT_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_CONVICTED_ACCIDENT_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_CONVICTED_ACCIDENT_DESC"] = value;
			}
		}
		
		public string ANY_LOSS_THREE_YEARS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_LOSS_THREE_YEARS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_LOSS_THREE_YEARS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_LOSS_THREE_YEARS_DESC"] = value;
			}
		}
		public string COVERAGE_DECLINED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_DECLINED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_DECLINED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_DECLINED_DESC"] = value;
			}
		}
		/*public string DEGREE_CONVICTION_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["DEGREE_CONVICTION_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DEGREE_CONVICTION_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEGREE_CONVICTION_DESC"] = value;
			}
		}*/
		
		public string IS_RENTED_OTHERS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_RENTED_OTHERS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_RENTED_OTHERS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_RENTED_OTHERS_DESC"] = value;
			}
		}
		public string IS_REGISTERED_OTHERS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_REGISTERED_OTHERS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_REGISTERED_OTHERS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_REGISTERED_OTHERS_DESC"] = value;
			}
		}


		// Addes by Shafi

		public string IS_PRIOR_INSURANCE_CARRIER
		{
			get
			{
				return base.dtModel.Rows[0]["IS_PRIOR_INSURANCE_CARRIER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_PRIOR_INSURANCE_CARRIER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_PRIOR_INSURANCE_CARRIER"] = value;
			}
		}         

		public string PRIOR_INSURANCE_CARRIER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PRIOR_INSURANCE_CARRIER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PRIOR_INSURANCE_CARRIER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PRIOR_INSURANCE_CARRIER_DESC"] = value;
			}
		}        

		
		public string IS_BOAT_COOWNED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BOAT_COOWNED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_BOAT_COOWNED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_BOAT_COOWNED"] = value;
			}
		}        

		public string IS_BOAT_COOWNED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BOAT_COOWNED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_BOAT_COOWNED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_BOAT_COOWNED_DESC"] = value;
			}
		} 
		//added 15 feb 2006
		public string BOAT_HOME_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["BOAT_HOME_DISCOUNT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BOAT_HOME_DISCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BOAT_HOME_DISCOUNT"] = value;
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
		public string MULTI_POLICY_DISC_APPLIED_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"] = value;
			}


		}


		public string ANY_BOAT_AMPHIBIOUS
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_BOAT_AMPHIBIOUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_BOAT_AMPHIBIOUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_BOAT_AMPHIBIOUS"] = value;
			}
		}         

		public string ANY_BOAT_AMPHIBIOUS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_BOAT_AMPHIBIOUS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_BOAT_AMPHIBIOUS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_BOAT_AMPHIBIOUS_DESC"] = value;
			}
		}

		// ANY_BOAT_RESIDENCE

		public string ANY_BOAT_RESIDENCE
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_BOAT_RESIDENCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_BOAT_RESIDENCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_BOAT_RESIDENCE"] = value;
			}
		}         

		public string ANY_BOAT_RESIDENCE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_BOAT_RESIDENCE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_BOAT_RESIDENCE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_BOAT_RESIDENCE_DESC"] = value;
			}
		}

		//ANY WATER USED
		public string IS_BOAT_USED_IN_ANY_WATER
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BOAT_USED_IN_ANY_WATER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_BOAT_USED_IN_ANY_WATER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_BOAT_USED_IN_ANY_WATER"] = value;
			}
		}         

		public string IS_BOAT_USED_IN_ANY_WATER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_BOAT_USED_IN_ANY_WATER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_BOAT_USED_IN_ANY_WATER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_BOAT_USED_IN_ANY_WATER_DESC"] = value;
			}
		}
        #endregion
    }
}
