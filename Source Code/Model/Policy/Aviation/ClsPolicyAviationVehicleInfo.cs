/******************************************************************************************
<Author				: -		Pravesh K Chandel
<Start Date			: -		14 Jan 2010
<End Date			: -	
<Description		: - 	Model Class for Policy Aviation Vehicle
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy.Aviation
{
	/// <summary>
	/// Database Model for POL_VEHICLES.
	/// </summary>
	public class ClsPolicyAviationVehicleInfo : Cms.Model.ClsCommonModel
	{
		private const string POL_VEHICLES = "POL_AVIATION_VEHICLES";
		public ClsPolicyAviationVehicleInfo()
		{
			base.dtModel.TableName = "POL_AVIATION_VEHICLES";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table POL_VEHICLES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("VEHICLE_ID",typeof(int));
			base.dtModel.Columns.Add("INSURED_VEH_NUMBER",typeof(int));
			base.dtModel.Columns.Add("USE_VEHICLE",typeof(string));
			base.dtModel.Columns.Add("COVG_PERIMETER",typeof(int));
			base.dtModel.Columns.Add("REG_NUMBER",typeof(string));
			base.dtModel.Columns.Add("SERIAL_NUMBER",typeof(string));
			base.dtModel.Columns.Add("VEHICLE_YEAR",typeof(int));
			base.dtModel.Columns.Add("MAKE",typeof(string));
			base.dtModel.Columns.Add("MAKE_OTHER",typeof(string));
			base.dtModel.Columns.Add("MODEL",typeof(string));
			base.dtModel.Columns.Add("MODEL_OTHER",typeof(string));
			base.dtModel.Columns.Add("CERTIFICATION",typeof(string));
			base.dtModel.Columns.Add("REGISTER",typeof(string));
			base.dtModel.Columns.Add("ENGINE_TYPE",typeof(string));
			base.dtModel.Columns.Add("WING_TYPE",typeof(string));
			base.dtModel.Columns.Add("CREW",typeof(string));
			base.dtModel.Columns.Add("PAX",typeof(string));
			base.dtModel.Columns.Add("REMARKS",typeof(string));
			base.dtModel.Columns.Add("DEACTIVATE_REACTIVATE_DATE",typeof(DateTime));

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
		// model for database field ENGINE_TYPE(STRING)
		public string ENGINE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["ENGINE_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ENGINE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ENGINE_TYPE"] = value;
			}
		}
		// model for database field WING_TYPE(STRING)
		public string WING_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["WING_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WING_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WING_TYPE"] = value;
			}
		}

		public string REG_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["REG_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REG_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REG_NUMBER"] = value;
			}
		}

		public string SERIAL_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["SERIAL_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SERIAL_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SERIAL_NUMBER"] = value;
			}
		}
		
		public string MAKE
		{
			get
			{
				return base.dtModel.Rows[0]["MAKE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAKE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAKE"] = value;
			}
		}
		
		public string MAKE_OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["MAKE_OTHER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MAKE_OTHER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MAKE_OTHER"] = value;
			}
		}
		
		
		// model for database field MODEL(string)
		public string MODEL
		{
			get
			{
				return base.dtModel.Rows[0]["MODEL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MODEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL"] = value;
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

		// model for database field AIR_BAG(string)
		public string MODEL_OTHER
		{
			get
			{
				return base.dtModel.Rows[0]["MODEL_OTHER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MODEL_OTHER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL_OTHER"] = value;
			}
		}

		// model for database field PASSIVE_SEAT_BELT(string)
		public string CERTIFICATION
		{
			get
			{
				return base.dtModel.Rows[0]["CERTIFICATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CERTIFICATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CERTIFICATION"] = value;
			}
		}

		public string REGISTER 
		{
			get
			{
				return base.dtModel.Rows[0]["REGISTER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REGISTER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REGISTER"] = value;
			}
		}
		// model for database field INSURED_VEH_NUMBER(int)
		public int INSURED_VEH_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_VEH_NUMBER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["INSURED_VEH_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_VEH_NUMBER"] = value;
			}
		}
		// model for database field VEHICLE_YEAR(int)
		public string VEHICLE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["VEHICLE_YEAR"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["VEHICLE_YEAR"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["VEHICLE_YEAR"] = value;
			}
		}
		
		public string CREW
		{
			get
			{
				return base.dtModel.Rows[0]["CREW"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CREW"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CREW"] = value;
			}
		}
		public string PAX
		{
			get
			{
				return base.dtModel.Rows[0]["PAX"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PAX"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PAX"] = value;
			}
		}


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

		public int USE_VEHICLE
		{
			get
			{
				return base.dtModel.Rows[0]["USE_VEHICLE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["USE_VEHICLE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["USE_VEHICLE"] = value;
			}
		}
		
		public int COVG_PERIMETER
		{
			get
			{
				return base.dtModel.Rows[0]["COVG_PERIMETER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COVG_PERIMETER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COVG_PERIMETER"] = value;
			}
		}

		#endregion
	}
}
