/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	11/7/2005 12:58:48 PM
<End Date				: -	
<Description				: - 	Model Class for Policy Driver
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
namespace Cms.Model.Policy
{
	/// <summary>
	/// Database Model for POL_DRIVER_DETAILS.
	/// </summary>
	public class ClsPolicyDriverInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_DRIVER_DETAILS = "POL_DRIVER_DETAILS";
		public ClsPolicyDriverInfo()
		{
			base.dtModel.TableName = "POL_DRIVER_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_DRIVER_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
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
			base.dtModel.Columns.Add("DRIVER_MOBILE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DOB",typeof(DateTime));
			base.dtModel.Columns.Add("DRIVER_SSN",typeof(string));
			base.dtModel.Columns.Add("DRIVER_MART_STAT",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SEX",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIV_LIC",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LIC_STATE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LIC_CLASS",typeof(string));
			base.dtModel.Columns.Add("DATE_EXP_START",typeof(DateTime));
			base.dtModel.Columns.Add("DATE_LICENSED",typeof(DateTime));
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
			base.dtModel.Columns.Add("DRIVER_FAX",typeof(string));
			base.dtModel.Columns.Add("RELATIONSHIP",typeof(int));
			base.dtModel.Columns.Add("DRIVER_TITLE",typeof(int));
			base.dtModel.Columns.Add("SAFE_DRIVER",typeof(short));
			base.dtModel.Columns.Add("GOOD_DRIVER_STUDENT_DISCOUNT",typeof(double));
			base.dtModel.Columns.Add("PREMIER_DRIVER_DISCOUNT",typeof(double));
			base.dtModel.Columns.Add("SAFE_DRIVER_RENEWAL_DISCOUNT",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("PERCENT_DRIVEN",typeof(double));
			base.dtModel.Columns.Add("MATURE_DRIVER",typeof(string));
			base.dtModel.Columns.Add("MATURE_DRIVER_DISCOUNT",typeof(double));
			base.dtModel.Columns.Add("PREFERRED_RISK_DISCOUNT",typeof(double));
			base.dtModel.Columns.Add("PREFERRED_RISK",typeof(string));
			base.dtModel.Columns.Add("TRANSFEREXP_RENEWAL_DISCOUNT",typeof(double));
			base.dtModel.Columns.Add("TRANSFEREXPERIENCE_RENEWALCREDIT",typeof(string));
			base.dtModel.Columns.Add("APP_VEHICLE_PRIN_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("NO_DEPENDENTS",typeof(int));
			base.dtModel.Columns.Add("WAIVER_WORK_LOSS_BENEFITS",typeof(string));
			//added by Sumit on 15-02-20006
			base.dtModel.Columns.Add("NO_CYCLE_ENDMT",typeof(string));

			//Added By Sumit(03/21/2006) For Operator
			base.dtModel.Columns.Add("OP_VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("OP_APP_VEHICLE_PRIN_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("OP_DRIVER_COST_GAURAD_AUX",typeof(int));
			base.dtModel.Columns.Add("FORM_F95",typeof(int));
			base.dtModel.Columns.Add("EXT_NON_OWN_COVG_INDIVI",typeof(int));
			base.dtModel.Columns.Add("FULL_TIME_STUDENT",typeof(int));
			base.dtModel.Columns.Add("SUPPORT_DOCUMENT",typeof(int));
			base.dtModel.Columns.Add("SIGNED_WAIVER_BENEFITS_FORM",typeof(int));

			base.dtModel.Columns.Add("HAVE_CAR",typeof(int));
			base.dtModel.Columns.Add("STATIONED_IN_US_TERR",typeof(int));
			base.dtModel.Columns.Add("IN_MILITARY",typeof(int));
			base.dtModel.Columns.Add("PARENTS_INSURANCE",typeof(int));
			base.dtModel.Columns.Add("ASSIGNED_VEHICLE",typeof(string));
			base.dtModel.Columns.Add("CYCL_WITH_YOU",typeof(int));
			base.dtModel.Columns.Add("COLL_STUD_AWAY_HOME",typeof(int));
			
			//added by Swarup on 15/12/2006
			base.dtModel.Columns.Add("VIOLATIONS",typeof(int));
			base.dtModel.Columns.Add("MVR_ORDERED",typeof(int));
			base.dtModel.Columns.Add("DATE_ORDERED",typeof(DateTime));
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
		//No of Dependents
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
		// model for database field DRIVER_ID(int)
		public int DRIVER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DRIVER_ID"].ToString());
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
				return base.dtModel.Rows[0]["DRIVER_FNAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_FNAME"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_MNAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_MNAME"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_LNAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_LNAME"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_CODE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_SUFFIX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_SUFFIX"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_ADD1"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_ADD1"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_ADD2"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_ADD2"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_CITY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_CITY"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_STATE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_ZIP"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_ZIP"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_COUNTRY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_COUNTRY"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_HOME_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_HOME_PHONE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_BUSINESS_PHONE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_BUSINESS_PHONE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_EXT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_EXT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_EXT"] = value;
			}
		}
		// model for database field DRIVER_MOBILE(string)
		public string DRIVER_MOBILE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_MOBILE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_MOBILE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_SSN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_SSN"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_MART_STAT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_MART_STAT"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_SEX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_SEX"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_DRIV_LIC"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_DRIV_LIC"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_LIC_STATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_LIC_STATE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_LIC_CLASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_LIC_CLASS"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_REL"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_REL"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_OCC_CODE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_OCC_CODE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_OCC_CLASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_OCC_CLASS"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_NAME"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_ADD"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_ADD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRIVERLOYER_ADD"] = value;
			}
		}
		// model for database field DRIVER_INCOME(double)
		public double DRIVER_INCOME
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_INCOME"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["DRIVER_INCOME"].ToString());
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
				return base.dtModel.Rows[0]["DRIVER_BROADEND_NOFAULT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DRIVER_BROADEND_NOFAULT"].ToString());
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
				return base.dtModel.Rows[0]["DRIVER_PHYS_MED_IMPAIRE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_PHYS_MED_IMPAIRE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_DRINK_VIOLATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_DRINK_VIOLATION"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_PREF_RISK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_PREF_RISK"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_GOOD_STUDENT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_GOOD_STUDENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_GOOD_STUDENT"] = value;
			}
		}
		// model for database field DRIVER_STUD_DIST_OVER_HUNDRED(string)
		public string DRIVER_STUD_DIST_OVER_HUNDRED
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_STUD_DIST_OVER_HUNDRED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_STUD_DIST_OVER_HUNDRED"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_LIC_SUSPENDED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_LIC_SUSPENDED"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_VOLUNTEER_POLICE_FIRE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_VOLUNTEER_POLICE_FIRE"].ToString();
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
				return base.dtModel.Rows[0]["DRIVER_US_CITIZEN"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_US_CITIZEN"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_US_CITIZEN"] = value;
			}
		}
		// model for database field DRIVER_FAX(string)
		public string DRIVER_FAX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_FAX"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DRIVER_FAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_FAX"] = value;
			}
		}
		// model for database field RELATIONSHIP(int)
		public int RELATIONSHIP
		{
			get
			{
				return base.dtModel.Rows[0]["RELATIONSHIP"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["RELATIONSHIP"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RELATIONSHIP"] = value;
			}
		}
		// model for database field DRIVER_TITLE(int)
		public int DRIVER_TITLE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_TITLE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DRIVER_TITLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_TITLE"] = value;
			}
		}
		// model for database field SAFE_DRIVER(short)
		public short SAFE_DRIVER
		{
			get
			{
				return base.dtModel.Rows[0]["SAFE_DRIVER"] == DBNull.Value ? short.Parse("0") : short.Parse(base.dtModel.Rows[0]["SAFE_DRIVER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SAFE_DRIVER"] = value;
			}
		}
		// model for database field GOOD_DRIVER_STUDENT_DISCOUNT(double)
		public double GOOD_DRIVER_STUDENT_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["GOOD_DRIVER_STUDENT_DISCOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["GOOD_DRIVER_STUDENT_DISCOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GOOD_DRIVER_STUDENT_DISCOUNT"] = value;
			}
		}
		// model for database field PREMIER_DRIVER_DISCOUNT(double)
		public double PREMIER_DRIVER_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["PREMIER_DRIVER_DISCOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PREMIER_DRIVER_DISCOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PREMIER_DRIVER_DISCOUNT"] = value;
			}
		}
		// model for database field SAFE_DRIVER_RENEWAL_DISCOUNT(double)
		public int SAFE_DRIVER_RENEWAL_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["SAFE_DRIVER_RENEWAL_DISCOUNT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["SAFE_DRIVER_RENEWAL_DISCOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SAFE_DRIVER_RENEWAL_DISCOUNT"] = value;
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
		// model for database field PERCENT_DRIVEN(double)
		public double PERCENT_DRIVEN
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_DRIVEN"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PERCENT_DRIVEN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_DRIVEN"] = value;
			}
		}
		// model for database field MATURE_DRIVER(string)
		public string MATURE_DRIVER
		{
			get
			{
				return base.dtModel.Rows[0]["MATURE_DRIVER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MATURE_DRIVER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MATURE_DRIVER"] = value;
			}
		}
		// model for database field MATURE_DRIVER_DISCOUNT(double)
		public double MATURE_DRIVER_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["MATURE_DRIVER_DISCOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MATURE_DRIVER_DISCOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MATURE_DRIVER_DISCOUNT"] = value;
			}
		}
		// model for database field PREFERRED_RISK_DISCOUNT(double)
		public double PREFERRED_RISK_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["PREFERRED_RISK_DISCOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PREFERRED_RISK_DISCOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PREFERRED_RISK_DISCOUNT"] = value;
			}
		}
		// model for database field PREFERRED_RISK(string)
		public string PREFERRED_RISK
		{
			get
			{
				return base.dtModel.Rows[0]["PREFERRED_RISK"] == DBNull.Value ? "" : base.dtModel.Rows[0]["PREFERRED_RISK"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PREFERRED_RISK"] = value;
			}
		}
		// model for database field TRANSFEREXP_RENEWAL_DISCOUNT(double)
		public double TRANSFEREXP_RENEWAL_DISCOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSFEREXP_RENEWAL_DISCOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["TRANSFEREXP_RENEWAL_DISCOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["TRANSFEREXP_RENEWAL_DISCOUNT"] = value;
			}
		}
		// model for database field TRANSFEREXPERIENCE_RENEWALCREDIT(string)
		public string TRANSFEREXPERIENCE_RENEWALCREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["TRANSFEREXPERIENCE_RENEWALCREDIT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TRANSFEREXPERIENCE_RENEWALCREDIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TRANSFEREXPERIENCE_RENEWALCREDIT"] = value;
			}
		}
		// model for database field APP_VEHICLE_PRIN_OCC_ID(int)
		public int APP_VEHICLE_PRIN_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["APP_VEHICLE_PRIN_OCC_ID"].ToString());
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
		//Added By Sumit(03-21-2006)
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
		// assigned vehicle
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

		//Added by Swarup on 15/12/2006
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
