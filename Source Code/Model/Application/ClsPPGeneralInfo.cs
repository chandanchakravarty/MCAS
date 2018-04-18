/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	4/27/2005 3:47:44 PM
<End Date				: -	
<Description				: - 	This file is used as Model for Private Passenger Automobile
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 18/10/2005
<Modified By				: - Mohit Gupta
<Purpose				: -  Adding field COST_EQUIPMENT_DESC
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_AUTO_GEN_INFO.
	/// </summary>
	public class ClsPPGeneralInformationInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_AUTO_GEN_INFO = "APP_AUTO_GEN_INFO";
		public ClsPPGeneralInformationInfo()
		{
			base.dtModel.TableName = "APP_AUTO_GEN_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_AUTO_GEN_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("ANY_NON_OWNED_VEH",typeof(string));
			base.dtModel.Columns.Add("ANY_NON_OWNED_VEH_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_NON_OWNED_VEH_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("CAR_MODIFIED",typeof(string));
			base.dtModel.Columns.Add("CAR_MODIFIED_DESC",typeof(string));
			base.dtModel.Columns.Add("EXISTING_DMG",typeof(string));
			base.dtModel.Columns.Add("EXISTING_DMG_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("EXISTING_DMG_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_CAR_AT_SCH",typeof(string));
			base.dtModel.Columns.Add("ANY_CAR_AT_SCH_DESC",typeof(string));
			base.dtModel.Columns.Add("SCHOOL_CARS_LIST",typeof(string));
			base.dtModel.Columns.Add("ANY_OTH_AUTO_INSU",typeof(string));
			base.dtModel.Columns.Add("ANY_OTH_AUTO_INSU_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_OTH_INSU_COMP",typeof(string));
			base.dtModel.Columns.Add("ANY_OTH_INSU_COMP_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_OTH_INSU_COMP_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("OTHER_POLICY_NUMBER_LIST",typeof(string));
			base.dtModel.Columns.Add("H_MEM_IN_MILITARY",typeof(string));
			base.dtModel.Columns.Add("H_MEM_IN_MILITARY_DESC",typeof(string));
			base.dtModel.Columns.Add("H_MEM_IN_MILITARY_LIST",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SUS_REVOKED",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SUS_REVOKED_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SUS_REVOKED_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("PHY_MENTL_CHALLENGED",typeof(string));
			base.dtModel.Columns.Add("PHY_MENTL_CHALLENGED_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("PHY_MENTL_CHALLENGED_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_FINANCIAL_RESPONSIBILITY",typeof(string));
			base.dtModel.Columns.Add("ANY_FINANCIAL_RESPONSIBILITY_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_FINANCIAL_RESPONSIBILITY_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("INS_AGENCY_TRANSFER",typeof(string));
			base.dtModel.Columns.Add("INS_AGENCY_TRANSFER_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("INS_AGENCY_TRANSFER_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_DECLINED",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_DECLINED_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("COVERAGE_DECLINED_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("AGENCY_VEH_INSPECTED",typeof(string));
			base.dtModel.Columns.Add("AGENCY_VEH_INSPECTED_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("AGENCY_VEH_INSPECTED_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("USE_AS_TRANSPORT_FEE",typeof(string));
			base.dtModel.Columns.Add("USE_AS_TRANSPORT_FEE_DESC",typeof(string));
			base.dtModel.Columns.Add("SALVAGE_TITLE",typeof(string));
			base.dtModel.Columns.Add("SALVAGE_TITLE_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("SALVAGE_TITLE_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("ANY_ANTIQUE_AUTO",typeof(string));
			base.dtModel.Columns.Add("ANY_ANTIQUE_AUTO_DESC",typeof(string));
			base.dtModel.Columns.Add("ANTIQUE_AUTO_LIST",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
            base.dtModel.Columns.Add("IS_COMMERCIAL_USE",typeof(string));
			base.dtModel.Columns.Add("IS_COMMERCIAL_USE_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_USEDFOR_RACING",typeof(string));
		    base.dtModel.Columns.Add("IS_USEDFOR_RACING_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_COST_OVER_DEFINED_LIMIT",typeof(string));
			base.dtModel.Columns.Add("IS_COST_OVER_DEFINED_LIMIT_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_MORE_WHEELS",typeof(string));
		    base.dtModel.Columns.Add("IS_MORE_WHEELS_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_EXTENDED_FORKS",typeof(string));
			base.dtModel.Columns.Add("IS_EXTENDED_FORKS_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_LICENSED_FOR_ROAD",typeof(string));
			base.dtModel.Columns.Add("IS_LICENSED_FOR_ROAD_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_MODIFIED_INCREASE_SPEED",typeof(string));
			base.dtModel.Columns.Add("IS_MODIFIED_INCREASE_SPEED_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_MODIFIED_KIT",typeof(string));
		    base.dtModel.Columns.Add("IS_MODIFIED_KIT_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_TAKEN_OUT",typeof(string));
			base.dtModel.Columns.Add("IS_TAKEN_OUT_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_CONVICTED_CARELESS_DRIVE",typeof(string));
			base.dtModel.Columns.Add("IS_CONVICTED_CARELESS_DRIVE_DESC",typeof(string));
            base.dtModel.Columns.Add("IS_CONVICTED_ACCIDENT",typeof(string));
			base.dtModel.Columns.Add("IS_CONVICTED_ACCIDENT_DESC",typeof(string));
			base.dtModel.Columns.Add("MULTI_POLICY_DISC_APPLIED",typeof(string));
			base.dtModel.Columns.Add("MULTI_POLICY_DISC_APPLIED_PP_DESC",typeof(string));
			base.dtModel.Columns.Add("FullName",typeof(string));
			base.dtModel.Columns.Add("DATE_OF_BIRTH",typeof(DateTime));
			base.dtModel.Columns.Add("DrivingLisence",typeof(string));
			base.dtModel.Columns.Add("InsuredElseWhere",typeof(string));
			base.dtModel.Columns.Add("CompanyName",typeof(string));
			base.dtModel.Columns.Add("PolicyNumber",typeof(string));
			base.dtModel.Columns.Add("IS_OTHER_THAN_INSURED",typeof(string));
			base.dtModel.Columns.Add("CURR_RES_TYPE",typeof(string));
			base.dtModel.Columns.Add("WhichCycle",typeof(string));
			base.dtModel.Columns.Add("MULTI_POLICY_DISC_APPLIED_MC_DESC",typeof(string));
			base.dtModel.Columns.Add("COST_EQUIPMENT_DESC",typeof(double));
			base.dtModel.Columns.Add("YEARS_INSU",typeof(int));
			base.dtModel.Columns.Add("YEARS_INSU_WOL",typeof(int));
			base.dtModel.Columns.Add("SEAT_BELT_CREDIT",typeof(string));
			base.dtModel.Columns.Add("ANY_PRIOR_LOSSES",typeof(string));
			base.dtModel.Columns.Add("ANY_PRIOR_LOSSES_DESC",typeof(string));
			base.dtModel.Columns.Add("APPLY_PERS_UMB_POL",typeof(int));
			base.dtModel.Columns.Add("APPLY_PERS_UMB_POL_DESC",typeof(string));
            base.dtModel.Columns.Add("POLICY_DESCRIPTION", typeof(string));
		}
		#region Database schema details
		public DateTime DATE_OF_BIRTH
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_OF_BIRTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_OF_BIRTH"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_OF_BIRTH"] = value;
			}
		}
		public string FullName
		{
			get
			{
				return base.dtModel.Rows[0]["FullName"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FullName"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FullName"] = value;
			}
		}
		public string DrivingLisence
		{
			get
			{
				return base.dtModel.Rows[0]["DrivingLisence"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DrivingLisence"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DrivingLisence"] = value;
			}
		}
		public string InsuredElseWhere
		{
			get
			{
				return base.dtModel.Rows[0]["InsuredElseWhere"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["InsuredElseWhere"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["InsuredElseWhere"] = value;
			}
		}
		public string CompanyName
		{
			get
			{
				return base.dtModel.Rows[0]["CompanyName"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CompanyName"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CompanyName"] = value;
			}
		}
		public string PolicyNumber
		{
			get
			{
				return base.dtModel.Rows[0]["PolicyNumber"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PolicyNumber"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PolicyNumber"] = value;
			}
		}
		public string IS_OTHER_THAN_INSURED
		{
			get
			{
				return base.dtModel.Rows[0]["IS_OTHER_THAN_INSURED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_OTHER_THAN_INSURED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_OTHER_THAN_INSURED"] = value;
			}
		}
		public string CURR_RES_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["CURR_RES_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CURR_RES_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CURR_RES_TYPE"] = value;
			}
		}
		public string WhichCycle
		{
			get
			{
				return base.dtModel.Rows[0]["WhichCycle"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WhichCycle"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WhichCycle"] = value;
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
		// model for database field ANY_NON_OWNED_VEH(string)
		public string ANY_NON_OWNED_VEH
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_NON_OWNED_VEH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_NON_OWNED_VEH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_NON_OWNED_VEH"] = value;
			}
		}
		// model for database field CAR_MODIFIED(string)
		public string CAR_MODIFIED
		{
			get
			{
				return base.dtModel.Rows[0]["CAR_MODIFIED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAR_MODIFIED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CAR_MODIFIED"] = value;
			}
		}
		// model for database field EXISTING_DMG(string)
		public string EXISTING_DMG
		{
			get
			{
				return base.dtModel.Rows[0]["EXISTING_DMG"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXISTING_DMG"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXISTING_DMG"] = value;
			}
		}
		// model for database field ANY_CAR_AT_SCH(string)
		public string ANY_CAR_AT_SCH
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_CAR_AT_SCH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_CAR_AT_SCH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_CAR_AT_SCH"] = value;
			}
		}
		// model for database field SCHOOL_CARS_LIST(string)
		public string SCHOOL_CARS_LIST
		{
			get
			{
				return base.dtModel.Rows[0]["SCHOOL_CARS_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SCHOOL_CARS_LIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SCHOOL_CARS_LIST"] = value;
			}
		}
		// model for database field ANY_OTH_AUTO_INSU(string)
		public string ANY_OTH_AUTO_INSU
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OTH_AUTO_INSU"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OTH_AUTO_INSU"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OTH_AUTO_INSU"] = value;
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
		// model for database field H_MEM_IN_MILITARY(string)
		public string H_MEM_IN_MILITARY
		{
			get
			{
				return base.dtModel.Rows[0]["H_MEM_IN_MILITARY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["H_MEM_IN_MILITARY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["H_MEM_IN_MILITARY"] = value;
			}
		}
		// model for database field H_MEM_IN_MILITARY_LIST(string)
		public string H_MEM_IN_MILITARY_LIST
		{
			get
			{
				return base.dtModel.Rows[0]["H_MEM_IN_MILITARY_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["H_MEM_IN_MILITARY_LIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["H_MEM_IN_MILITARY_LIST"] = value;
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
		// model for database field ANY_FINANCIAL_RESPONSIBILITY(string)
		public string ANY_FINANCIAL_RESPONSIBILITY
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY"] = value;
			}
		}
		// model for database field INS_AGENCY_TRANSFER(string)
		public string INS_AGENCY_TRANSFER
		{
			get
			{
				return base.dtModel.Rows[0]["INS_AGENCY_TRANSFER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INS_AGENCY_TRANSFER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INS_AGENCY_TRANSFER"] = value;
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
		// model for database field AGENCY_VEH_INSPECTED(string)
		public string AGENCY_VEH_INSPECTED
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED"] = value;
			}
		}
		// model for database field USE_AS_TRANSPORT_FEE(string)
		public string USE_AS_TRANSPORT_FEE
		{
			get
			{
				return base.dtModel.Rows[0]["USE_AS_TRANSPORT_FEE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USE_AS_TRANSPORT_FEE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USE_AS_TRANSPORT_FEE"] = value;
			}
		}
		// model for database field SALVAGE_TITLE(string)
		public string SALVAGE_TITLE
		{
			get
			{
				return base.dtModel.Rows[0]["SALVAGE_TITLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SALVAGE_TITLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SALVAGE_TITLE"] = value;
			}
		}
		// model for database field ANY_ANTIQUE_AUTO(string)
		public string ANY_ANTIQUE_AUTO
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_ANTIQUE_AUTO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_ANTIQUE_AUTO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_ANTIQUE_AUTO"] = value;
			}
		}
		// model for database field ANTIQUE_AUTO_LIST(string)
		public string ANTIQUE_AUTO_LIST
		{
			get
			{
				return base.dtModel.Rows[0]["ANTIQUE_AUTO_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANTIQUE_AUTO_LIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANTIQUE_AUTO_LIST"] = value;
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



        // model for database field IS_COMMERCIAL_USE(string)
        public string IS_COMMERCIAL_USE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_COMMERCIAL_USE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_COMMERCIAL_USE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_COMMERCIAL_USE"] = value;
            }
        }
        // model for database field IS_USEDFOR_RACING(string)
        public string IS_USEDFOR_RACING
        {
            get
            {
                return base.dtModel.Rows[0]["IS_USEDFOR_RACING"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_USEDFOR_RACING"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_USEDFOR_RACING"] = value;
            }
        }
        // model for database field IS_COST_OVER_DEFINED_LIMIT(string)
        public string IS_COST_OVER_DEFINED_LIMIT
        {
            get
            {
                return base.dtModel.Rows[0]["IS_COST_OVER_DEFINED_LIMIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_COST_OVER_DEFINED_LIMIT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_COST_OVER_DEFINED_LIMIT"] = value;
            }
        }
        // model for database field IS_MORE_WHEELS(string)
        public string IS_MORE_WHEELS
        {
            get
            {
                return base.dtModel.Rows[0]["IS_MORE_WHEELS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_MORE_WHEELS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_MORE_WHEELS"] = value;
            }
        }
        // model for database field IS_EXTENDED_FORKS(string)
        public string IS_EXTENDED_FORKS
        {
            get
            {
                return base.dtModel.Rows[0]["IS_EXTENDED_FORKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_EXTENDED_FORKS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_EXTENDED_FORKS"] = value;
            }
        }
        // model for database field IS_LICENSED_FOR_ROAD(string)
        public string IS_LICENSED_FOR_ROAD
        {
            get
            {
                return base.dtModel.Rows[0]["IS_LICENSED_FOR_ROAD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_LICENSED_FOR_ROAD"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_LICENSED_FOR_ROAD"] = value;
            }
        }
        // model for database field IS_MODIFIED_INCREASE_SPEED(string)
        public string IS_MODIFIED_INCREASE_SPEED
        {
            get
            {
                return base.dtModel.Rows[0]["IS_MODIFIED_INCREASE_SPEED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_MODIFIED_INCREASE_SPEED"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_MODIFIED_INCREASE_SPEED"] = value;
            }
        }
        // model for database field IS_MODIFIED_KIT(string)
        public string IS_MODIFIED_KIT
        {
            get
            {
                return base.dtModel.Rows[0]["IS_MODIFIED_KIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_MODIFIED_KIT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_MODIFIED_KIT"] = value;
            }
        }
        // model for database field IS_TAKEN_OUT(string)
        public string IS_TAKEN_OUT
        {
            get
            {
                return base.dtModel.Rows[0]["IS_TAKEN_OUT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_TAKEN_OUT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_TAKEN_OUT"] = value;
            }
        }
        // model for database field IS_CONVICTED_CARELESS_DRIVE(string)
        public string IS_CONVICTED_CARELESS_DRIVE
        {
            get
            {
                return base.dtModel.Rows[0]["IS_CONVICTED_CARELESS_DRIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_CONVICTED_CARELESS_DRIVE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IS_CONVICTED_CARELESS_DRIVE"] = value;
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
		public string ANY_NON_OWNED_VEH_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_NON_OWNED_VEH_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_NON_OWNED_VEH_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_NON_OWNED_VEH_PP_DESC"] = value;
			}
		}
		public string CAR_MODIFIED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["CAR_MODIFIED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CAR_MODIFIED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CAR_MODIFIED_DESC"] = value;
			}

		}
		public string EXISTING_DMG_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["EXISTING_DMG_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXISTING_DMG_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXISTING_DMG_PP_DESC"] = value;
			}

		}
		public string ANY_CAR_AT_SCH_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_CAR_AT_SCH_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_CAR_AT_SCH_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_CAR_AT_SCH_DESC"] = value;
			}

		}
		public string ANY_OTH_AUTO_INSU_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OTH_AUTO_INSU_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OTH_AUTO_INSU_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OTH_AUTO_INSU_DESC"] = value;
			}

		}
		public string ANY_OTH_INSU_COMP_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OTH_INSU_COMP_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OTH_INSU_COMP_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OTH_INSU_COMP_PP_DESC"] = value;
			}

		}
		public string H_MEM_IN_MILITARY_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["H_MEM_IN_MILITARY_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["H_MEM_IN_MILITARY_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["H_MEM_IN_MILITARY_DESC"] = value;
			}

		}
		public string DRIVER_SUS_REVOKED_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"] = value;
			}

		}
		public string PHY_MENTL_CHALLENGED_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"] = value;
			}

		}
		public string ANY_FINANCIAL_RESPONSIBILITY_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_PP_DESC"] = value;
			}

		}
		public string INS_AGENCY_TRANSFER_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["INS_AGENCY_TRANSFER_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INS_AGENCY_TRANSFER_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INS_AGENCY_TRANSFER_PP_DESC"] = value;
			}

		}
		public string COVERAGE_DECLINED_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_DECLINED_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_DECLINED_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_DECLINED_PP_DESC"] = value;
			}

		}
		public string AGENCY_VEH_INSPECTED_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"] = value;
			}

		}
		public string USE_AS_TRANSPORT_FEE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["USE_AS_TRANSPORT_FEE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USE_AS_TRANSPORT_FEE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USE_AS_TRANSPORT_FEE_DESC"] = value;
			}


		}
		public string SALVAGE_TITLE_PP_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["SALVAGE_TITLE_PP_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SALVAGE_TITLE_PP_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SALVAGE_TITLE_PP_DESC"] = value;
			}


		}
		public string ANY_ANTIQUE_AUTO_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_ANTIQUE_AUTO_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_ANTIQUE_AUTO_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_ANTIQUE_AUTO_DESC"] = value;
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
		public string ANY_NON_OWNED_VEH_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_NON_OWNED_VEH_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_NON_OWNED_VEH_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_NON_OWNED_VEH_MC_DESC"] = value;
			}


		}
		public string EXISTING_DMG_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["EXISTING_DMG_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXISTING_DMG_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXISTING_DMG_MC_DESC"] = value;
			}


		}
		public string ANY_OTH_INSU_COMP_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OTH_INSU_COMP_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_OTH_INSU_COMP_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OTH_INSU_COMP_MC_DESC"] = value;
			}
		}
		public string DRIVER_SUS_REVOKED_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SUS_REVOKED_MC_DESC"] = value;
			}
		}
		public string PHY_MENTL_CHALLENGED_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PHY_MENTL_CHALLENGED_MC_DESC"] = value;
			}
		}
		public string ANY_FINANCIAL_RESPONSIBILITY_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_MC_DESC"] = value;
			}
		}
		public string INS_AGENCY_TRANSFER_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["INS_AGENCY_TRANSFER_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INS_AGENCY_TRANSFER_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INS_AGENCY_TRANSFER_MC_DESC"] = value;
			}
		}
		public string COVERAGE_DECLINED_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["COVERAGE_DECLINED_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COVERAGE_DECLINED_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["COVERAGE_DECLINED_MC_DESC"] = value;
			}
		}
		public string AGENCY_VEH_INSPECTED_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["AGENCY_VEH_INSPECTED_MC_DESC"] = value;
			}
		}
		public string SALVAGE_TITLE_MC_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["SALVAGE_TITLE_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SALVAGE_TITLE_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SALVAGE_TITLE_MC_DESC"] = value;
			}
		}
		public string IS_COMMERCIAL_USE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COMMERCIAL_USE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_COMMERCIAL_USE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_COMMERCIAL_USE_DESC"] = value;
			}
		}
		public string IS_USEDFOR_RACING_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_USEDFOR_RACING_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_USEDFOR_RACING_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_USEDFOR_RACING_DESC"] = value;
			}
		}
		public string IS_COST_OVER_DEFINED_LIMIT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_COST_OVER_DEFINED_LIMIT_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_COST_OVER_DEFINED_LIMIT_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_COST_OVER_DEFINED_LIMIT_DESC"] = value;
			}

		}
		public string IS_MORE_WHEELS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_MORE_WHEELS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_MORE_WHEELS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_MORE_WHEELS_DESC"] = value;
			}

		}
		public string IS_EXTENDED_FORKS_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_EXTENDED_FORKS_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_EXTENDED_FORKS_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_EXTENDED_FORKS_DESC"] = value;
			}

		}
		public string IS_LICENSED_FOR_ROAD_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_LICENSED_FOR_ROAD_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_LICENSED_FOR_ROAD_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_LICENSED_FOR_ROAD_DESC"] = value;
			}

		}
		public string IS_MODIFIED_INCREASE_SPEED_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_MODIFIED_INCREASE_SPEED_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_MODIFIED_INCREASE_SPEED_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_MODIFIED_INCREASE_SPEED_DESC"] = value;
			}

		}
		public string IS_MODIFIED_KIT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_MODIFIED_KIT_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_MODIFIED_KIT_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_MODIFIED_KIT_DESC"] = value;
			}

		}
		public string IS_TAKEN_OUT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_TAKEN_OUT_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_TAKEN_OUT_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_TAKEN_OUT_DESC"] = value;
			}

		}
		public string IS_CONVICTED_CARELESS_DRIVE_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["IS_CONVICTED_CARELESS_DRIVE_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_CONVICTED_CARELESS_DRIVE_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_CONVICTED_CARELESS_DRIVE_DESC"] = value;
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
		public string MULTI_POLICY_DISC_APPLIED_MC_DESC		
		{
			get
			{
				return base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED_MC_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED_MC_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MULTI_POLICY_DISC_APPLIED_MC_DESC"] = value;
			}
		}
		// model for database field COST_EQUIPMENT_DESC(double)
		public double COST_EQUIPMENT_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["COST_EQUIPMENT_DESC"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["COST_EQUIPMENT_DESC"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COST_EQUIPMENT_DESC"] = value;
			}
		}

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

		
		public string SEAT_BELT_CREDIT		
		{
			get
			{
				return base.dtModel.Rows[0]["SEAT_BELT_CREDIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SEAT_BELT_CREDIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SEAT_BELT_CREDIT"] = value;
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
		public int APPLY_PERS_UMB_POL
		{
			get
			{
				return base.dtModel.Rows[0]["APPLY_PERS_UMB_POL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APPLY_PERS_UMB_POL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APPLY_PERS_UMB_POL"] = value;
			}
		}
		// model for database field APPLY_PERS_UMB_POL_DESC(string)
		public string APPLY_PERS_UMB_POL_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["APPLY_PERS_UMB_POL_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["APPLY_PERS_UMB_POL_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["APPLY_PERS_UMB_POL_DESC"] = value;
			}
		}

        // model for database field POLICY_DESCRIPTION(string)
        public string POLICY_DESCRIPTION
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_DESCRIPTION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["POLICY_DESCRIPTION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_DESCRIPTION"] = value;
            }
        }
		#endregion
	}
}
