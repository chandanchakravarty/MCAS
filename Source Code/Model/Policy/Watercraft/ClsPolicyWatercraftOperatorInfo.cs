/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		11/23/2005 3:57:36 PM
<End Date				: -	
<Description			: - 	Model Class for Policy Watercraft Operator
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
namespace Cms.Model.Policy.Watercraft
{
	/// <summary>
	/// Database Model for POL_WATERCRAFT_DRIVER_DETAILS.
	/// </summary>
	public class ClsPolicyWatercraftOperatorInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_WATERCRAFT_DRIVER_DETAILS = "POL_WATERCRAFT_DRIVER_DETAILS";
		public ClsPolicyWatercraftOperatorInfo()
		{
			base.dtModel.TableName = "POL_WATERCRAFT_DRIVER_DETAILS";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_WATERCRAFT_DRIVER_DETAILS
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
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
			base.dtModel.Columns.Add("DRIVER_DOB",typeof(DateTime));
			base.dtModel.Columns.Add("DRIVER_SSN",typeof(string));
			base.dtModel.Columns.Add("DRIVER_SEX",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIV_LIC",typeof(string));
			base.dtModel.Columns.Add("DRIVER_LIC_STATE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_COST_GAURAD_AUX",typeof(int));
			base.dtModel.Columns.Add("EXPERIENCE_CREDIT",typeof(double));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("PERCENT_DRIVEN",typeof(double));
            base.dtModel.Columns.Add("APP_VEHICLE_PRIN_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("WAT_SAFETY_COURSE",typeof(int));
			base.dtModel.Columns.Add("CERT_COAST_GUARD",typeof(int));
			//Rec Veh Assign
			base.dtModel.Columns.Add("REC_VEH_ID",typeof(int));
			base.dtModel.Columns.Add("APP_REC_VEHICLE_PRIN_OCC_ID",typeof(int));
			base.dtModel.Columns.Add("VIOLATIONS",typeof(int));
			base.dtModel.Columns.Add("MVR_ORDERED",typeof(int));
			base.dtModel.Columns.Add("DATE_ORDERED",typeof(DateTime));
			base.dtModel.Columns.Add("MARITAL_STATUS",typeof(string));
			base.dtModel.Columns.Add("MVR_CLASS",typeof(string));
			base.dtModel.Columns.Add("MVR_LIC_CLASS",typeof(string));
			base.dtModel.Columns.Add("MVR_LIC_RESTR",typeof(string));
			base.dtModel.Columns.Add("MVR_DRIV_LIC_APPL",typeof(string));
			
			base.dtModel.Columns.Add("MVR_REMARKS",typeof(string));
			base.dtModel.Columns.Add("MVR_STATUS",typeof(string));
			base.dtModel.Columns.Add("ASSIGNED_VEHICLE",typeof(string));
			//Done for Itrack Issue 6737 on 17 Nov 09
			base.dtModel.Columns.Add("ASSIGNED_REC_VEHICLE",typeof(string));
			base.dtModel.Columns.Add("DRIVER_DRIV_TYPE",typeof(int));
			
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
		// model for database field DRIVER_SEX(int)		
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
		// model for database field DRIVER_COST_GAURAD_AUX(int)
		public int DRIVER_COST_GAURAD_AUX
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_COST_GAURAD_AUX"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["DRIVER_COST_GAURAD_AUX"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_COST_GAURAD_AUX"] = value;
			}
		}
		// model for database field EXPERIENCE_CREDIT(double)
		public double EXPERIENCE_CREDIT
		{
			get
			{
				return base.dtModel.Rows[0]["EXPERIENCE_CREDIT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["EXPERIENCE_CREDIT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["EXPERIENCE_CREDIT"] = value;
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
		// model for database field WAT_SAFETY_COURSE(int)
		public int WAT_SAFETY_COURSE
		{
			get
			{
				return base.dtModel.Rows[0]["WAT_SAFETY_COURSE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["WAT_SAFETY_COURSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["WAT_SAFETY_COURSE"] = value;
			}
		}
		// model for database field CERT_COAST_GUARD(int)
		public int CERT_COAST_GUARD
		{
			get
			{
				return base.dtModel.Rows[0]["CERT_COAST_GUARD"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CERT_COAST_GUARD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CERT_COAST_GUARD"] = value;
			}
		}
		// model for database field REC_VEH_ID(int)
		public int REC_VEH_ID
		{
			get
			{
				return base.dtModel.Rows[0]["REC_VEH_ID"] == DBNull.Value ? -1 : Convert.ToInt32(base.dtModel.Rows[0]["REC_VEH_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REC_VEH_ID"] = value;
			}
		}
		// model for database field APP_REC_VEHICLE_PRIN_OCC_ID(int)
		public int APP_REC_VEHICLE_PRIN_OCC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_REC_VEHICLE_PRIN_OCC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_REC_VEHICLE_PRIN_OCC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_REC_VEHICLE_PRIN_OCC_ID"] = value;
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

		// model for database field MARITAL_STATUS(string)
		public string MARITAL_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["MARITAL_STATUS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["MARITAL_STATUS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MARITAL_STATUS"] = value;
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
		// Assigned Boat 
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
		//Done for Itrack Issue 6737 on 17 Nov 09
		public string ASSIGNED_REC_VEHICLE
		{
			get
			{
				return base.dtModel.Rows[0]["ASSIGNED_REC_VEHICLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ASSIGNED_REC_VEHICLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ASSIGNED_REC_VEHICLE"] = value;
			}
		}
		public int DRIVER_DRIV_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DRIVER_DRIV_TYPE"] = value;
			}
		}
		
		#endregion
	}
}
