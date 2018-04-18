/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	03/05/2006
<End Date				: -	
<Description			: 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy.GeneralLiability
{
	/// <summary>
	/// Database Model for POL_GENERAL_UNDERWRITING_INFO.
	/// </summary>
	public class ClsGeneralInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_GENERAL_UNDERWRITING_INFO = "POL_GENERAL_UNDERWRITING_INFO";
		public ClsGeneralInfo()
		{
			base.dtModel.TableName = "POL_GENERAL_UNDERWRITING_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_GENERAL_UNDERWRITING_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("INSURANCE_DECLINED_FIVE_YEARS",typeof(string));
			base.dtModel.Columns.Add("MEDICAL_PROFESSIONAL_EMPLOYEED",typeof(string));
			base.dtModel.Columns.Add("EXPOSURE_RATIOACTIVE_NUCLEAR",typeof(string));
			base.dtModel.Columns.Add("HAVE_PAST_PRESENT_OPERATIONS",typeof(string));
			base.dtModel.Columns.Add("ANY_OPERATIONS_SOLD",typeof(string));
			base.dtModel.Columns.Add("MACHINERY_LOANED",typeof(string));
			base.dtModel.Columns.Add("ANY_WATERCRAFT_LEASED",typeof(string));
			base.dtModel.Columns.Add("ANY_PARKING_OWNED",typeof(string));
			base.dtModel.Columns.Add("FEE_CHARGED_PARKING",typeof(string));
			base.dtModel.Columns.Add("RECREATION_PROVIDED",typeof(string));
			base.dtModel.Columns.Add("SWIMMING_POOL_PREMISES",typeof(string));
			base.dtModel.Columns.Add("SPORTING_EVENT_SPONSORED",typeof(string));
			base.dtModel.Columns.Add("STRUCTURAL_ALTERATION_CONTEMPATED",typeof(string));
			base.dtModel.Columns.Add("DEMOLITION_EXPOSURE_CONTEMPLATED",typeof(string));
			base.dtModel.Columns.Add("CUSTOMER_ACTIVE_JOINT_VENTURES",typeof(string));
			base.dtModel.Columns.Add("LEASE_EMPLOYEE",typeof(string));
			base.dtModel.Columns.Add("LABOR_INTERCHANGE_OTH_BUSINESS",typeof(string));
			base.dtModel.Columns.Add("DAY_CARE_FACILITIES",typeof(string));
			base.dtModel.Columns.Add("ADDITIONAL_COMMENTS",typeof(string));

			//---------------------Added by Mohit on 11/10/2005----.
			base.dtModel.Columns.Add("DESC_INSURANCE_DECLINED",typeof(string));	
			base.dtModel.Columns.Add("DESC_MEDICAL_PROFESSIONAL",typeof(string));	
			base.dtModel.Columns.Add("DESC_EXPOSURE_RATIOACTIVE",typeof(string));	
			base.dtModel.Columns.Add("DESC_HAVE_PAST_PRESENT",typeof(string));	
			base.dtModel.Columns.Add("DESC_ANY_OPERATIONS",typeof(string));	
			base.dtModel.Columns.Add("DESC_MACHINERY_LOANED",typeof(string));	
			base.dtModel.Columns.Add("DESC_ANY_WATERCRAFT",typeof(string));	
			base.dtModel.Columns.Add("DESC_ANY_PARKING",typeof(string));	
			base.dtModel.Columns.Add("DESC_FEE_CHARGED",typeof(string));	
			base.dtModel.Columns.Add("DESC_RECREATION_PROVIDED",typeof(string));	
			base.dtModel.Columns.Add("DESC_SWIMMING_POOL",typeof(string));	
			base.dtModel.Columns.Add("DESC_SPORTING_EVENT",typeof(string));	
			base.dtModel.Columns.Add("DESC_STRUCTURAL_ALTERATION",typeof(string));	
			base.dtModel.Columns.Add("DESC_DEMOLITION_EXPOSURE",typeof(string));	
			base.dtModel.Columns.Add("DESC_CUSTOMER_ACTIVE",typeof(string));	
			base.dtModel.Columns.Add("DESC_LEASE_EMPLOYEE",typeof(string));	
			base.dtModel.Columns.Add("DESC_LABOR_INTERCHANGE",typeof(string));	
			base.dtModel.Columns.Add("DESC_DAY_CARE",typeof(string));	

			//----------------------------End--------------------.
		}
		#region Database schema details

		// model for database field ADDITIONAL_COMMENTS(string)
		public string ADDITIONAL_COMMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["ADDITIONAL_COMMENTS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ADDITIONAL_COMMENTS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDITIONAL_COMMENTS"] = value;
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
		// model for database field INSURANCE_DECLINED_FIVE_YEARS(string)
		public string INSURANCE_DECLINED_FIVE_YEARS
		{
			get
			{
				return base.dtModel.Rows[0]["INSURANCE_DECLINED_FIVE_YEARS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["INSURANCE_DECLINED_FIVE_YEARS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSURANCE_DECLINED_FIVE_YEARS"] = value;
			}
		}
		// model for database field MEDICAL_PROFESSIONAL_EMPLOYEED(string)
		public string MEDICAL_PROFESSIONAL_EMPLOYEED
		{
			get
			{
				return base.dtModel.Rows[0]["MEDICAL_PROFESSIONAL_EMPLOYEED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MEDICAL_PROFESSIONAL_EMPLOYEED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MEDICAL_PROFESSIONAL_EMPLOYEED"] = value;
			}
		}
		// model for database field EXPOSURE_RATIOACTIVE_NUCLEAR(string)
		public string EXPOSURE_RATIOACTIVE_NUCLEAR
		{
			get
			{
				return base.dtModel.Rows[0]["EXPOSURE_RATIOACTIVE_NUCLEAR"] == DBNull.Value ? "" : base.dtModel.Rows[0]["EXPOSURE_RATIOACTIVE_NUCLEAR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXPOSURE_RATIOACTIVE_NUCLEAR"] = value;
			}
		}
		// model for database field HAVE_PAST_PRESENT_OPERATIONS(string)
		public string HAVE_PAST_PRESENT_OPERATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["HAVE_PAST_PRESENT_OPERATIONS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["HAVE_PAST_PRESENT_OPERATIONS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HAVE_PAST_PRESENT_OPERATIONS"] = value;
			}
		}
		// model for database field ANY_OPERATIONS_SOLD(string)
		public string ANY_OPERATIONS_SOLD
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_OPERATIONS_SOLD"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ANY_OPERATIONS_SOLD"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_OPERATIONS_SOLD"] = value;
			}
		}
		// model for database field MACHINERY_LOANED(string)
		public string MACHINERY_LOANED
		{
			get
			{
				return base.dtModel.Rows[0]["MACHINERY_LOANED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MACHINERY_LOANED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MACHINERY_LOANED"] = value;
			}
		}
		// model for database field ANY_WATERCRAFT_LEASED(string)
		public string ANY_WATERCRAFT_LEASED
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_WATERCRAFT_LEASED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ANY_WATERCRAFT_LEASED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_WATERCRAFT_LEASED"] = value;
			}
		}
		// model for database field ANY_PARKING_OWNED(string)
		public string ANY_PARKING_OWNED
		{
			get
			{
				return base.dtModel.Rows[0]["ANY_PARKING_OWNED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ANY_PARKING_OWNED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ANY_PARKING_OWNED"] = value;
			}
		}
		// model for database field FEE_CHARGED_PARKING(string)
		public string FEE_CHARGED_PARKING
		{
			get
			{
				return base.dtModel.Rows[0]["FEE_CHARGED_PARKING"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FEE_CHARGED_PARKING"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FEE_CHARGED_PARKING"] = value;
			}
		}
		// model for database field RECREATION_PROVIDED(string)
		public string RECREATION_PROVIDED
		{
			get
			{
				return base.dtModel.Rows[0]["RECREATION_PROVIDED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["RECREATION_PROVIDED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECREATION_PROVIDED"] = value;
			}
		}
		// model for database field SWIMMING_POOL_PREMISES(string)
		public string SWIMMING_POOL_PREMISES
		{
			get
			{
				return base.dtModel.Rows[0]["SWIMMING_POOL_PREMISES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SWIMMING_POOL_PREMISES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SWIMMING_POOL_PREMISES"] = value;
			}
		}
		// model for database field SPORTING_EVENT_SPONSORED(string)
		public string SPORTING_EVENT_SPONSORED
		{
			get
			{
				return base.dtModel.Rows[0]["SPORTING_EVENT_SPONSORED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["SPORTING_EVENT_SPONSORED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SPORTING_EVENT_SPONSORED"] = value;
			}
		}
		// model for database field STRUCTURAL_ALTERATION_CONTEMPATED(string)
		public string STRUCTURAL_ALTERATION_CONTEMPATED
		{
			get
			{
				return base.dtModel.Rows[0]["STRUCTURAL_ALTERATION_CONTEMPATED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["STRUCTURAL_ALTERATION_CONTEMPATED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STRUCTURAL_ALTERATION_CONTEMPATED"] = value;
			}
		}
		// model for database field DEMOLITION_EXPOSURE_CONTEMPLATED(string)
		public string DEMOLITION_EXPOSURE_CONTEMPLATED
		{
			get
			{
				return base.dtModel.Rows[0]["DEMOLITION_EXPOSURE_CONTEMPLATED"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DEMOLITION_EXPOSURE_CONTEMPLATED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DEMOLITION_EXPOSURE_CONTEMPLATED"] = value;
			}
		}
		// model for database field CUSTOMER_ACTIVE_JOINT_VENTURES(string)
		public string CUSTOMER_ACTIVE_JOINT_VENTURES
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ACTIVE_JOINT_VENTURES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CUSTOMER_ACTIVE_JOINT_VENTURES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ACTIVE_JOINT_VENTURES"] = value;
			}
		}
		// model for database field LEASE_EMPLOYEE(string)
		public string LEASE_EMPLOYEE
		{
			get
			{
				return base.dtModel.Rows[0]["LEASE_EMPLOYEE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LEASE_EMPLOYEE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LEASE_EMPLOYEE"] = value;
			}
		}
		// model for database field LABOR_INTERCHANGE_OTH_BUSINESS(string)
		public string LABOR_INTERCHANGE_OTH_BUSINESS
		{
			get
			{
				return base.dtModel.Rows[0]["LABOR_INTERCHANGE_OTH_BUSINESS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LABOR_INTERCHANGE_OTH_BUSINESS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LABOR_INTERCHANGE_OTH_BUSINESS"] = value;
			}
		}
		// model for database field DAY_CARE_FACILITIES(string)
		public string DAY_CARE_FACILITIES
		{
			get
			{
				return base.dtModel.Rows[0]["DAY_CARE_FACILITIES"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DAY_CARE_FACILITIES"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DAY_CARE_FACILITIES"] = value;
			}
		}
		// ----------------Added by mohit om 11/10/2005----------------------.
		
		// model for database field DESC_INSURANCE_DECLINED(string)
		public string DESC_INSURANCE_DECLINED
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_INSURANCE_DECLINED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_INSURANCE_DECLINED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_INSURANCE_DECLINED"] = value;
			}
		}
		// model for database field DESC_MEDICAL_PROFESSIONAL(string)
		public string DESC_MEDICAL_PROFESSIONAL
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_MEDICAL_PROFESSIONAL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_MEDICAL_PROFESSIONAL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_MEDICAL_PROFESSIONAL"] = value;
			}
		}
		// model for database field DESC_EXPOSURE_RATIOACTIVE(string)
		public string DESC_EXPOSURE_RATIOACTIVE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_EXPOSURE_RATIOACTIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_EXPOSURE_RATIOACTIVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_EXPOSURE_RATIOACTIVE"] = value;
			}
		}
		// model for database field DESC_HAVE_PAST_PRESENT(string)
		public string DESC_HAVE_PAST_PRESENT
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_HAVE_PAST_PRESENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_HAVE_PAST_PRESENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_HAVE_PAST_PRESENT"] = value;
			}
		}
		// model for database field DESC_ANY_OPERATIONS(string)
		public string DESC_ANY_OPERATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_ANY_OPERATIONS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_ANY_OPERATIONS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_ANY_OPERATIONS"] = value;
			}
		}
		// model for database field DESC_MACHINERY_LOANED(string)
		public string DESC_MACHINERY_LOANED
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_MACHINERY_LOANED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_MACHINERY_LOANED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_MACHINERY_LOANED"] = value;
			}
		}

		
		// model for database field DESC_ANY_WATERCRAFT(string)
		public string DESC_ANY_WATERCRAFT
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_ANY_WATERCRAFT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_ANY_WATERCRAFT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_ANY_WATERCRAFT"] = value;
			}
		}
		
		// model for database field DESC_ANY_PARKING(string)
		public string DESC_ANY_PARKING
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_ANY_PARKING"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_ANY_PARKING"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_ANY_PARKING"] = value;
			}
		}
		
		// model for database field DESC_FEE_CHARGED(string)
		public string DESC_FEE_CHARGED
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_FEE_CHARGED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_FEE_CHARGED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_FEE_CHARGED"] = value;
			}
		}
			
		// model for database field DESC_RECREATION_PROVIDED(string)
		public string DESC_RECREATION_PROVIDED
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_RECREATION_PROVIDED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_RECREATION_PROVIDED"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_RECREATION_PROVIDED"] = value;
			}
		}
			
		// model for database field DESC_SWIMMING_POOL(string)
		public string DESC_SWIMMING_POOL
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_SWIMMING_POOL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_SWIMMING_POOL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_SWIMMING_POOL"] = value;
			}
		}
				
		// model for database field DESC_SPORTING_EVENT(string)
		public string DESC_SPORTING_EVENT
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_SPORTING_EVENT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_SPORTING_EVENT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_SPORTING_EVENT"] = value;
			}
		}
				
		// model for database field DESC_STRUCTURAL_ALTERATION(string)
		public string DESC_STRUCTURAL_ALTERATION
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_STRUCTURAL_ALTERATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_STRUCTURAL_ALTERATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_STRUCTURAL_ALTERATION"] = value;
			}
		}
					
		// model for database field DESC_DEMOLITION_EXPOSURE(string)
		public string DESC_DEMOLITION_EXPOSURE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_DEMOLITION_EXPOSURE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_DEMOLITION_EXPOSURE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_DEMOLITION_EXPOSURE"] = value;
			}
		}
					
		// model for database field DESC_CUSTOMER_ACTIVE(string)
		public string DESC_CUSTOMER_ACTIVE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_CUSTOMER_ACTIVE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_CUSTOMER_ACTIVE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_CUSTOMER_ACTIVE"] = value;
			}
		}
						
		// model for database field DESC_LEASE_EMPLOYEE(string)
		public string DESC_LEASE_EMPLOYEE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_LEASE_EMPLOYEE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_LEASE_EMPLOYEE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_LEASE_EMPLOYEE"] = value;
			}
		}
						
		// model for database field DESC_LABOR_INTERCHANGE(string)
		public string DESC_LABOR_INTERCHANGE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_LABOR_INTERCHANGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_LABOR_INTERCHANGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_LABOR_INTERCHANGE"] = value;
			}
		}
							
		// model for database field DESC_DAY_CARE(string)
		public string DESC_DAY_CARE
		{
			get
			{
				return base.dtModel.Rows[0]["DESC_DAY_CARE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DESC_DAY_CARE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["DESC_DAY_CARE"] = value;
			}
		}



		//-------------------------End----------------------------------------.

		#endregion
	}
}
