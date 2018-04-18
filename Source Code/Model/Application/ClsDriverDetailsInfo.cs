/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	4/28/2005 5:03:29 PM
<End Date				: -	
<Description				: - 	Model class for Drive Details table
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 14/09/2005
<Modified By				: - Anurag Verma
<Purpose				: - columns added due to merger of driver detail, driver discount and assign vehicle screens

<Modified Date			: - 07/10/2005
<Modified By			: - Sumit Chhabra
<Purpose				: - New column SAFE_DRIVER_RENEWAL_DISCOUNT being added for app_umbrella_driver_details table

<Modified Date			: - 17/10/2005
<Modified By			: - Vijay Arora
<Purpose				: - New column APP_VEHICLE_PRIN_OCC_ID added 

*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Application
{
	/// <summary>
	/// Database Model for APP_DRIVER_DETAILS.
	/// </summary>
	public class ClsDriverDetailsInfo : Cms.Model.ClsCommonModel
	{
		private const string APP_DRIVER_DETAILS = "APP_DRIVER_DETAILS";
		public ClsDriverDetailsInfo()
		{
			base.dtModel.TableName = "APP_DRIVER_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_DRIVER_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DRIVER_ID",typeof(int));
			base.dtModel.Columns.Add("DRIVER_FNAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_MNAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LNAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_CODE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SUFFIX",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ADD1",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ADD2",typeof(string));
			base.dtModel.Columns.Add("DRIVER_CITY",typeof(string));
			base.dtModel.Columns.Add("DRIVER_STATE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_ZIP",typeof(string));
			base.dtModel.Columns.Add("DRIVER_COUNTRY",typeof(string));
			base.dtModel.Columns.Add("DRIVER_HOME_PHONE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_BUSINESS_PHONE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_EXT",typeof(string));
			base.dtModel.Columns.Add("DRIVER_FAX",typeof(string));
			base.dtModel.Columns.Add("DRIVER_MOBILE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DOB",typeof(DateTime));
			//base.dtModel.Columns["DRIVER_DOB"].ExtendedProperties.Add("FORMAT_DATE","N");  
 

			base.dtModel.Columns.Add("DRIVER_SSN",typeof(string));
			base.dtModel.Columns.Add("DRIVER_MART_STAT",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SEX",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIV_LIC",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LIC_STATE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LIC_CLASS",typeof(string));
			base.dtModel.Columns.Add("DATE_EXP_START",typeof(DateTime));
			base.dtModel.Columns.Add("DATE_LICENSED",typeof(DateTime));
			//base.dtModel.Columns["DATE_LICENSED"].ExtendedProperties.Add("FORMAT_DATE","N");  
			base.dtModel.Columns.Add("DRIVER_REL",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIV_TYPE",typeof(string));
		    base.dtModel.Columns.Add("DRIVER_OCC_CODE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_OCC_CLASS",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIVERLOYER_NAME",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIVERLOYER_ADD",typeof(string));
			base.dtModel.Columns.Add("DRIVER_INCOME",typeof(double));
			base.dtModel.Columns.Add("DRIVER_BROADEND_NOFAULT",typeof(int));
			base.dtModel.Columns.Add("DRIVER_PHYS_MED_IMPAIRE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRINK_VIOLATION",typeof(string));
			base.dtModel.Columns.Add("DRIVER_PREF_RISK",typeof(string));
			base.dtModel.Columns.Add("DRIVER_GOOD_STUDENT",typeof(string));
			base.dtModel.Columns.Add("DRIVER_STUD_DIST_OVER_HUNDRED",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LIC_SUSPENDED",typeof(string));
			base.dtModel.Columns.Add("DRIVER_VOLUNTEER_POLICE_FIRE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_US_CITIZEN",typeof(string));
			base.dtModel.Columns.Add("RELATIONSHIP",typeof(int));
			base.dtModel.Columns.Add("RELATIONSHIP_CODE",typeof(string));
			base.dtModel.Columns.Add("SAFE_DRIVER",typeof(bool));
			base.dtModel.Columns.Add("ID",typeof(string));
			base.dtModel.Columns.Add("Good_Driver_Student_Discount",typeof(double));
			base.dtModel.Columns.Add("Premier_Driver_Discount",typeof(double));
			base.dtModel.Columns.Add("Safe_Driver_Renewal_Discount",typeof(double));
			base.dtModel.Columns.Add("VEHICLEID",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(string));
			base.dtModel.Columns.Add("VIOLATIONS",typeof(int));
			base.dtModel.Columns.Add("MVR_ORDERED",typeof(int));
			base.dtModel.Columns.Add("DATE_ORDERED",typeof(DateTime));
			base.dtModel.Columns.Add("PERCENT_DRIVEN",typeof(double));
			base.dtModel.Columns.Add("Mature_Driver",typeof(string));
			base.dtModel.Columns.Add("Mature_Driver_Discount",typeof(double));
			base.dtModel.Columns.Add("Preferred_Risk_Discount",typeof(double));
			base.dtModel.Columns.Add("Preferred_Risk",typeof(string));
			base.dtModel.Columns.Add("TransferExp_Renewal_Discount",typeof(double));
			base.dtModel.Columns.Add("TransferExperience_RenewalCredit",typeof(string));
			base.dtModel.Columns.Add("SAFE_DRIVER_RENEWAL_DISCOUNT",typeof(string));
			base.dtModel.Columns.Add("APP_VEHICLE_PRIN_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("NO_DEPENDENTS",typeof(int));
			base.dtModel.Columns.Add("WAIVER_WORK_LOSS_BENEFITS",typeof(string));
			base.dtModel.Columns.Add("NO_CYCLE_ENDMT",typeof(string));
			base.dtModel.Columns.Add("FORM_F95",typeof(int));
			base.dtModel.Columns.Add("EXT_NON_OWN_COVG_INDIVI",typeof(int));
			base.dtModel.Columns.Add("OP_VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("OP_APP_VEHICLE_PRIN_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("OP_DRIVER_COST_GAURAD_AUX",typeof(int));
			base.dtModel.Columns.Add("HAVE_CAR",typeof(int));
			base.dtModel.Columns.Add("STATIONED_IN_US_TERR",typeof(int));
			base.dtModel.Columns.Add("IN_MILITARY",typeof(int));
			base.dtModel.Columns.Add("FULL_TIME_STUDENT",typeof(int));
			base.dtModel.Columns.Add("SUPPORT_DOCUMENT",typeof(int));
			base.dtModel.Columns.Add("SIGNED_WAIVER_BENEFITS_FORM",typeof(int));
			base.dtModel.Columns.Add("PARENTS_INSURANCE",typeof(int));
			base.dtModel.Columns.Add("ASSIGNED_VEHICLE",typeof(string));
			base.dtModel.Columns.Add("CYCL_WITH_YOU",typeof(int));
			base.dtModel.Columns.Add("COLL_STUD_AWAY_HOME",typeof(int));
			base.dtModel.Columns.Add("MOT_VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("MOT_APP_VEHICLE_PRIN_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("STATE_CODE",typeof(string));
			base.dtModel.Columns.Add("MVR_CLASS",typeof(string));
			base.dtModel.Columns.Add("MVR_LIC_CLASS",typeof(string));
			base.dtModel.Columns.Add("MVR_LIC_RESTR",typeof(string));
			base.dtModel.Columns.Add("MVR_DRIV_LIC_APPL",typeof(string));
			base.dtModel.Columns.Add("MVR_REMARKS",typeof(string));
			base.dtModel.Columns.Add("MVR_STATUS",typeof(string));
			base.dtModel.Columns.Add("LOSSREPORT_ORDER",typeof(int));
			base.dtModel.Columns.Add("LOSSREPORT_DATETIME",typeof(DateTime));
		}
		#region Database schema details
		/// <summary>
		/// //////////////////////////////////////
		/// </summary>
		

		//Added By Ravindra(02-27-2006)
		public int OP_DRIVER_COST_GAURAD_AUX
		{
			get
			{
				return base.dtModel.Rows[0]["OP_DRIVER_COST_GAURAD_AUX"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["OP_DRIVER_COST_GAURAD_AUX"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OP_DRIVER_COST_GAURAD_AUX"] = value;
			}
		}

		public int OP_APP_VEHICLE_PRIN_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OP_APP_VEHICLE_PRIN_OCC_ID"] == DBNull.Value ? -1 : Convert.ToInt32(base.dtModel.Rows[0]["OP_APP_VEHICLE_PRIN_OCC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OP_APP_VEHICLE_PRIN_OCC_ID"] = value;
			}
		}
		
		public int OP_VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["OP_VEHICLE_ID"] == DBNull.Value ? -1 : Convert.ToInt32(base.dtModel.Rows[0]["OP_VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OP_VEHICLE_ID"] = value;
			}
		}
	 ////////////////////////////////////////////////////////////
		
		public int NO_DEPENDENTS
		{
			get
			{
				return base.dtModel.Rows[0]["NO_DEPENDENTS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_DEPENDENTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_DEPENDENTS"] = value;
			}
		}
		// model for database field CUSTOMER_ID(int)
		
		public string RELATIONSHIP_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["RELATIONSHIP_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RELATIONSHIP_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RELATIONSHIP_CODE"] = value;
			}
		}

		

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

		// model for database field DRIVER_ID(int)
		public int DRIVER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ID"] = value;
			}
		}
		// model for database field DRIVER_FNAME(string)
		public string DRIVER_FNAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_FNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_FNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_FNAME"] = value;
			}
		}
		// model for database field DRIVER_MNAME(string)
		public string DRIVER_MNAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_MNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_MNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_MNAME"] = value;
			}
		}
		// model for database field DRIVER_LNAME(string)
		public string DRIVER_LNAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_LNAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LNAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_LNAME"] = value;
			}
		}
		// model for database field DRIVER_CODE(string)
		public string DRIVER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_CODE"] = value;
			}
		}
		// model for database field DRIVER_SUFFIX(string)
		public string DRIVER_SUFFIX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SUFFIX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SUFFIX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SUFFIX"] = value;
			}
		}
		// model for database field DRIVER_ADD1(string)
		public string DRIVER_ADD1
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ADD1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_ADD1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ADD1"] = value;
			}
		}
		// model for database field DRIVER_ADD2(string)
		public string DRIVER_ADD2
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ADD2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_ADD2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ADD2"] = value;
			}
		}
		// model for database field DRIVER_CITY(string)
		public string DRIVER_CITY
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_CITY"] = value;
			}
		}
		// model for database field DRIVER_STATE(string)
		public string DRIVER_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_STATE"] = value;
			}
		}
		// model for database field DRIVER_ZIP(string)
		public string DRIVER_ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_ZIP"] = value;
			}
		}
		// model for database field DRIVER_COUNTRY(string)
		public string DRIVER_COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_COUNTRY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_COUNTRY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_COUNTRY"] = value;
			}
		}
		// model for database field DRIVER_HOME_PHONE(string)
		public string DRIVER_HOME_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_HOME_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_HOME_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_HOME_PHONE"] = value;
			}
		}
		// model for database field DRIVER_BUSINESS_PHONE(string)
		public string DRIVER_BUSINESS_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_BUSINESS_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_BUSINESS_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_BUSINESS_PHONE"] = value;
			}
		}
		// model for database field DRIVER_EXT(string)
		public string DRIVER_EXT
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_EXT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_EXT"] = value;
			}
		}
		// model for database field DRIVER_FAX(string)
		public string DRIVER_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_FAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_FAX"] = value;
			}
		}
		// model for database field DRIVER_MOBILE(string)
		public string DRIVER_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_MOBILE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_MOBILE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_MOBILE"] = value;
			}
		}
		// model for database field DRIVER_DOB(DateTime)
		public DateTime DRIVER_DOB
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DRIVER_DOB"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DOB"] = value;
			}
		}
		// model for database field DRIVER_SSN(string)
		public string DRIVER_SSN
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SSN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SSN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SSN"] = value;
			}
		}
		// model for database field DRIVER_MART_STAT(string)
		public string DRIVER_MART_STAT
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_MART_STAT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_MART_STAT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_MART_STAT"] = value;
			}
		}
		// model for database field DRIVER_SEX(string)
		public string DRIVER_SEX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_SEX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_SEX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_SEX"] = value;
			}
		}
		// model for database field DRIVER_DRIV_LIC(string)
		public string DRIVER_DRIV_LIC
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DRIV_LIC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_DRIV_LIC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRIV_LIC"] = value;
			}
		}
		// model for database field DRIVER_LIC_STATE(string)
		public string DRIVER_LIC_STATE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_LIC_STATE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LIC_STATE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_LIC_STATE"] = value;
			}
		}
		// model for database field DRIVER_LIC_CLASS(string)
		public string DRIVER_LIC_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_LIC_CLASS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LIC_CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_LIC_CLASS"] = value;
			}
		}
		// model for database field DATE_EXP_START(DateTime)
		public DateTime DATE_EXP_START
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_EXP_START"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_EXP_START"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_EXP_START"] = value;
			}
		}
		// model for database field DATE_LICENSED(DateTime)
		public DateTime DATE_LICENSED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_LICENSED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_LICENSED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_LICENSED"] = value;
			}
		}
		// model for database field DRIVER_REL(string)
		public string DRIVER_REL
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_REL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_REL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_REL"] = value;
			}
		}
		// model for database field DRIVER_DRIV_TYPE(string)
		public string DRIVER_DRIV_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"] = value;
			}
		}
		// model for database field DRIVER_OCC_CODE(string)
		public string DRIVER_OCC_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_OCC_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_OCC_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_OCC_CODE"] = value;
			}
		}
		// model for database field DRIVER_OCC_CLASS(string)
		public string DRIVER_OCC_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_OCC_CLASS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_OCC_CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_OCC_CLASS"] = value;
			}
		}
		// model for database field DRIVER_DRIVERLOYER_NAME(string)
		public string DRIVER_DRIVERLOYER_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_NAME"] = value;
			}
		}
		// model for database field DRIVER_DRIVERLOYER_ADD(string)
		public string DRIVER_DRIVERLOYER_ADD
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_ADD"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_ADD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_ADD"] = value;
			}
		}
		// model for database field DRIVER_INCOME(float)
		public double DRIVER_INCOME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_INCOME"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["DRIVER_INCOME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_INCOME"] = value;
			}
		}
		// model for database field DRIVER_BROADEND_NOFAULT(int)
		public int DRIVER_BROADEND_NOFAULT
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_BROADEND_NOFAULT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_BROADEND_NOFAULT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_BROADEND_NOFAULT"] = value;
			}
		}
		// model for database field DRIVER_PHYS_MED_IMPAIRE(string)
		public string DRIVER_PHYS_MED_IMPAIRE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_PHYS_MED_IMPAIRE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_PHYS_MED_IMPAIRE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_PHYS_MED_IMPAIRE"] = value;
			}
		}
		// model for database field DRIVER_DRINK_VIOLATION(string)
		public string DRIVER_DRINK_VIOLATION
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DRINK_VIOLATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_DRINK_VIOLATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRINK_VIOLATION"] = value;
			}
		}
		// model for database field DRIVER_PREF_RISK(string)
		public string DRIVER_PREF_RISK
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_PREF_RISK"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_PREF_RISK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_PREF_RISK"] = value;
			}
		}
		// model for database field DRIVER_GOOD_STUDENT(string)
		public string DRIVER_GOOD_STUDENT
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_GOOD_STUDENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_GOOD_STUDENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_GOOD_STUDENT"] = value;
			}
		}
		// model for database field SAFE_DRIVER_RENEWAL_DISCOUNT(string)
		public string SAFE_DRIVER_RENEWAL_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["SAFE_DRIVER_RENEWAL_DISCOUNT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SAFE_DRIVER_RENEWAL_DISCOUNT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SAFE_DRIVER_RENEWAL_DISCOUNT"] = value;
			}
		}
		// model for database field DRIVER_STUD_DIST_OVER_HUNDRED(string)
		public string DRIVER_STUD_DIST_OVER_HUNDRED
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_STUD_DIST_OVER_HUNDRED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_STUD_DIST_OVER_HUNDRED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_STUD_DIST_OVER_HUNDRED"] = value;
			}
		}
		// model for database field DRIVER_LIC_SUSPENDED(string)
		public string DRIVER_LIC_SUSPENDED
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_LIC_SUSPENDED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_LIC_SUSPENDED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_LIC_SUSPENDED"] = value;
			}
		}
		// model for database field DRIVER_VOLUNTEER_POLICE_FIRE(string)
		public string DRIVER_VOLUNTEER_POLICE_FIRE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_VOLUNTEER_POLICE_FIRE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_VOLUNTEER_POLICE_FIRE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_VOLUNTEER_POLICE_FIRE"] = value;
			}
		}
		// model for database field DRIVER_US_CITIZEN(string)
		public string DRIVER_US_CITIZEN
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_US_CITIZEN"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DRIVER_US_CITIZEN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_US_CITIZEN"] = value;
			}
		}

		public string ID
		{
			get
			{
				return base.dtModel.Rows[0]["ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ID"] = value;
			}
		}

		// model for database field RELATIONSHIP(int)
		public int RELATIONSHIP
		{
			get
			{
				return base.dtModel.Rows[0]["RELATIONSHIP"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RELATIONSHIP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RELATIONSHIP"] = value;
			}
		}

		public bool SAFE_DRIVER
		{
			get
			{
				return base.dtModel.Rows[0]["SAFE_DRIVER"] == DBNull.Value ? Convert.ToBoolean(null) :bool.Parse( base.dtModel.Rows[0]["SAFE_DRIVER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SAFE_DRIVER"] = value;
			}
		}

		public double Good_Driver_Student_Discount
		{
			get
			{
				return base.dtModel.Rows[0]["Good_Driver_Student_Discount"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["Good_Driver_Student_Discount"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Good_Driver_Student_Discount"] = value;
			}
		}

		public double Premier_Driver_Discount
		{
			get
			{
				return base.dtModel.Rows[0]["Premier_Driver_Discount"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["Premier_Driver_Discount"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Premier_Driver_Discount"] = value;
			}
		}

		public double Safe_Driver_Renewal_Discount
		{
			get
			{
				return base.dtModel.Rows[0]["Safe_Driver_Renewal_Discount"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["Safe_Driver_Renewal_Discount"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Safe_Driver_Renewal_Discount"] = value;
			}
		}

		public string VEHICLEID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLEID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VEHICLEID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLEID"] = value;
			}
		}
		public int VIOLATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["VIOLATIONS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["VIOLATIONS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["VIOLATIONS"] = value;
			}
		}
		public int MVR_ORDERED
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_ORDERED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MVR_ORDERED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MVR_ORDERED"] = value;
			}
		}
		public DateTime DATE_ORDERED
		{
			get
			{
				return base.dtModel.Rows[0]["DATE_ORDERED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DATE_ORDERED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DATE_ORDERED"] = value;
			}
		}
		public string VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VEHICLE_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_ID"] = value;
			}
		}

		public double PERCENT_DRIVEN
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_DRIVEN"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_DRIVEN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_DRIVEN"] = value;
			}
		}

		public double Mature_Driver_Discount
		{
			get
			{
				return base.dtModel.Rows[0]["Mature_Driver_Discount"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["Mature_Driver_Discount"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Mature_Driver_Discount"] = value;
			}
		}

		public string Mature_Driver
		{
			get
			{
				return base.dtModel.Rows[0]["Mature_Driver"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["Mature_Driver"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["Mature_Driver"] = value;
			}
		}

		public double Preferred_Risk_Discount
		{
			get
			{
				return base.dtModel.Rows[0]["Preferred_Risk_Discount"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["Preferred_Risk_Discount"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["Preferred_Risk_Discount"] = value;
			}
		}

		public string Preferred_Risk
		{
			get
			{
				return base.dtModel.Rows[0]["Preferred_Risk"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["Preferred_Risk"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["Preferred_Risk"] = value;
			}
		}

		public double TransferExp_Renewal_Discount
		{
			get
			{
				return base.dtModel.Rows[0]["TransferExp_Renewal_Discount"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["TransferExp_Renewal_Discount"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TransferExp_Renewal_Discount"] = value;
			}
		}

		public string TransferExperience_RenewalCredit
		{
			get
			{
				return base.dtModel.Rows[0]["TransferExperience_RenewalCredit"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TransferExperience_RenewalCredit"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TransferExperience_RenewalCredit"] = value;
			}
		}


		public int APP_VEHICLE_PRIN_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"] = value;
			}
		}

		
		//waiver of work loss credit
		public string WAIVER_WORK_LOSS_BENEFITS
		{
			get
			{
				return base.dtModel.Rows[0]["WAIVER_WORK_LOSS_BENEFITS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WAIVER_WORK_LOSS_BENEFITS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WAIVER_WORK_LOSS_BENEFITS"] = value;
			}
		}
		//No Cycle Endorsement on License
		public string NO_CYCLE_ENDMT
		{
			get
			{
				return base.dtModel.Rows[0]["NO_CYCLE_ENDMT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NO_CYCLE_ENDMT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NO_CYCLE_ENDMT"] = value;
			}
		}
		// FORM F-95
		public int FORM_F95
		{
			get
			{
				return base.dtModel.Rows[0]["FORM_F95"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FORM_F95"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FORM_F95"] = value;
			}
		}

		// EXT_NON_OWN_COVG_INDIVI
		public int EXT_NON_OWN_COVG_INDIVI
		{
			get
			{
				return base.dtModel.Rows[0]["EXT_NON_OWN_COVG_INDIVI"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["EXT_NON_OWN_COVG_INDIVI"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXT_NON_OWN_COVG_INDIVI"] = value;
			}
		}

		// IN_MILITARY
		public int IN_MILITARY
		{
			get
			{
				return base.dtModel.Rows[0]["IN_MILITARY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["IN_MILITARY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IN_MILITARY"] = value;
			}
		}
		// STATIONED_IN_US_TERR
		public int STATIONED_IN_US_TERR
		{
			get
			{
				return base.dtModel.Rows[0]["STATIONED_IN_US_TERR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATIONED_IN_US_TERR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATIONED_IN_US_TERR"] = value;
			}
		}
		// HAVE_CAR
		public int HAVE_CAR
		{
			get
			{
				return base.dtModel.Rows[0]["HAVE_CAR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["HAVE_CAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["HAVE_CAR"] = value;
			}
		}
		// FULL_TIME_STUDENT
		public int FULL_TIME_STUDENT
		{
			get
			{
				return base.dtModel.Rows[0]["FULL_TIME_STUDENT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FULL_TIME_STUDENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FULL_TIME_STUDENT"] = value;
			}
		}
		// SUPPORT_DOCUMENT
		public int SUPPORT_DOCUMENT
		{
			get
			{
				return base.dtModel.Rows[0]["SUPPORT_DOCUMENT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUPPORT_DOCUMENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUPPORT_DOCUMENT"] = value;
			}
		}
		// SIGNED_WAIVER_BENEFITS_FORM
		public int SIGNED_WAIVER_BENEFITS_FORM
		{
			get
			{
				return base.dtModel.Rows[0]["SIGNED_WAIVER_BENEFITS_FORM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SIGNED_WAIVER_BENEFITS_FORM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SIGNED_WAIVER_BENEFITS_FORM"] = value;
			}
		}
		// PARENTS_INSURANCE 
		public int PARENTS_INSURANCE
		{
			get
			{
				return base.dtModel.Rows[0]["PARENTS_INSURANCE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PARENTS_INSURANCE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PARENTS_INSURANCE"] = value;
			}
		}
		// Assigned Vehicle 
		public string ASSIGNED_VEHICLE
		{
			get
			{
				return base.dtModel.Rows[0]["ASSIGNED_VEHICLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ASSIGNED_VEHICLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ASSIGNED_VEHICLE"] = value;
			}
		}
		// CYCL_WITH_YOU 
		public int CYCL_WITH_YOU
		{
			get
			{
				return base.dtModel.Rows[0]["CYCL_WITH_YOU"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CYCL_WITH_YOU"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CYCL_WITH_YOU"] = value;
			}
		}
		// COLL_STUD_AWAY_HOME 
		public int COLL_STUD_AWAY_HOME
		{
			get
			{
				return base.dtModel.Rows[0]["COLL_STUD_AWAY_HOME"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COLL_STUD_AWAY_HOME"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COLL_STUD_AWAY_HOME"] = value;
			}
		}
		// MOT_VEHICLE_ID 
		public int MOT_VEHICLE_ID
		{
			get
			{
				return base.dtModel.Rows[0]["MOT_VEHICLE_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MOT_VEHICLE_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MOT_VEHICLE_ID"] = value;
			}
		}
		// MOT_APP_VEHICLE_PRIN_OCC_ID 
		public int MOT_APP_VEHICLE_PRIN_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["MOT_APP_VEHICLE_PRIN_OCC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MOT_APP_VEHICLE_PRIN_OCC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MOT_APP_VEHICLE_PRIN_OCC_ID"] = value;
			}
		}
		//STATE_CODE
		public string STATE_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STATE_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STATE_CODE"] = value;
			}
		}
		public string MVR_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_CLASS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MVR_CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_CLASS"] = value;
			}
		}
		public string MVR_LIC_CLASS
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_LIC_CLASS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MVR_LIC_CLASS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_LIC_CLASS"] = value;
			}
		}
		public string MVR_LIC_RESTR
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_LIC_RESTR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MVR_LIC_RESTR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_LIC_RESTR"] = value;
			}
		}
		public string MVR_DRIV_LIC_APPL
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_DRIV_LIC_APPL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MVR_DRIV_LIC_APPL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_DRIV_LIC_APPL"] = value;
			}
		}
		public string MVR_REMARKS
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_REMARKS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MVR_REMARKS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_REMARKS"] = value;
			}
		}
		public string MVR_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["MVR_STATUS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MVR_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MVR_STATUS"] = value;
			}
		}
		// model for database field LOSSREPORT_ORDER(int)
		public int LOSSREPORT_ORDER
		{
			get
			{
				return base.dtModel.Rows[0]["LOSSREPORT_ORDER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSSREPORT_ORDER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSSREPORT_ORDER"] = value;
			}
		}

		/// <summary>
		/// model for database field LOSSREPORT_DATETIME(datetime)
		/// </summary>
		public DateTime LOSSREPORT_DATETIME
		{
			get
			{
				return dtModel.Rows[0]["LOSSREPORT_DATETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtModel.Rows[0]["LOSSREPORT_DATETIME"]);
			}
			set
			{
				dtModel.Rows[0]["LOSSREPORT_DATETIME"] = value;
			}
		}
		
		#endregion
	}
}
