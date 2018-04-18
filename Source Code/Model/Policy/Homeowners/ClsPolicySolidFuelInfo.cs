/******************************************************************************************
<Author				: - Anurag Verma
<Start Date			: -	18/11/2005 5:40:31 PM
<End Date			: -	
<Description		: - Model class for POL_HOME_OWNER_SOLID_FUEL table
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
	/// Summary description for ClsPolicySolidFuelInfo.
	/// </summary>
	public class ClsPolicySolidFuelInfo: Cms.Model.ClsCommonModel 
	{
		private const string POL_HOME_OWNER_SOLID_FUEL = "POL_HOME_OWNER_SOLID_FUEL";
		public ClsPolicySolidFuelInfo()
		{
			base.dtModel.TableName = "POL_HOME_OWNER_SOLID_FUEL";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_HOME_OWNER_SOLID_FUEL
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("FUEL_ID",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_ID",typeof(int));
			base.dtModel.Columns.Add("MANUFACTURER",typeof(string));
			base.dtModel.Columns.Add("BRAND_NAME",typeof(string));
			base.dtModel.Columns.Add("MODEL_NUMBER",typeof(string));
			base.dtModel.Columns.Add("FUEL",typeof(string));
			base.dtModel.Columns.Add("STOVE_TYPE",typeof(string));
			base.dtModel.Columns.Add("HAVE_LABORATORY_LABEL",typeof(string));
			base.dtModel.Columns.Add("IS_UNIT",typeof(string));
			base.dtModel.Columns.Add("UNIT_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("CONSTRUCTION",typeof(string));
			base.dtModel.Columns.Add("LOCATION",typeof(string));
			base.dtModel.Columns.Add("LOC_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("YEAR_DEVICE_INSTALLED",typeof(int));
			base.dtModel.Columns.Add("WAS_PROF_INSTALL_DONE",typeof(string));
			base.dtModel.Columns.Add("INSTALL_INSPECTED_BY",typeof(string));
			base.dtModel.Columns.Add("INSTALL_OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("HEATING_USE",typeof(string));
			base.dtModel.Columns.Add("HEATING_SOURCE",typeof(string));
			base.dtModel.Columns.Add("OTHER_DESC",typeof(string));
			base.dtModel.Columns.Add("STOVE_INSTALLATION_CONFORM_SPECIFICATIONS",typeof(int));
		
		}
		#region Database schema details

		// model for database field STOVE_INSTALLATION_CONFORM_SPECIFICATIONS(int)

		public int STOVE_INSTALLATION_CONFORM_SPECIFICATIONS
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_INSTALLATION_CONFORM_SPECIFICATIONS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STOVE_INSTALLATION_CONFORM_SPECIFICATIONS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_INSTALLATION_CONFORM_SPECIFICATIONS"] = value;
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
		// model for database field FUEL_ID(int)
		public int FUEL_ID
		{
			get
			{
				return base.dtModel.Rows[0]["FUEL_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["FUEL_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FUEL_ID"] = value;
			}
		}
		// model for database field LOCATION_ID(int)
		public int LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ID"] = value;
			}
		}
		// model for database field SUB_LOC_ID(int)
		public int SUB_LOC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["SUB_LOC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_ID"] = value;
			}
		}
		// model for database field MANUFACTURER(string)
		public string MANUFACTURER
		{
			get
			{
				return base.dtModel.Rows[0]["MANUFACTURER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MANUFACTURER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MANUFACTURER"] = value;
			}
		}
		// model for database field BRAND_NAME(string)
		public string BRAND_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["BRAND_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["BRAND_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["BRAND_NAME"] = value;
			}
		}
		// model for database field MODEL_NUMBER(string)
		public string MODEL_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["MODEL_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MODEL_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MODEL_NUMBER"] = value;
			}
		}
		// model for database field FUEL(string)
		public string FUEL
		{
			get
			{
				return base.dtModel.Rows[0]["FUEL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["FUEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["FUEL"] = value;
			}
		}
		// model for database field STOVE_TYPE(string)
		public string STOVE_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["STOVE_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["STOVE_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["STOVE_TYPE"] = value;
			}
		}
		// model for database field HAVE_LABORATORY_LABEL(string)
		public string HAVE_LABORATORY_LABEL
		{
			get
			{
				return base.dtModel.Rows[0]["HAVE_LABORATORY_LABEL"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HAVE_LABORATORY_LABEL"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HAVE_LABORATORY_LABEL"] = value;
			}
		}
		// model for database field IS_UNIT(string)
		public string IS_UNIT
		{
			get
			{
				return base.dtModel.Rows[0]["IS_UNIT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_UNIT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IS_UNIT"] = value;
			}
		}
		// model for database field UNIT_OTHER_DESC(string)
		public string UNIT_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["UNIT_OTHER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["UNIT_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["UNIT_OTHER_DESC"] = value;
			}
		}
		// model for database field CONSTRUCTION(string)
		public string CONSTRUCTION
		{
			get
			{
				return base.dtModel.Rows[0]["CONSTRUCTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CONSTRUCTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CONSTRUCTION"] = value;
			}
		}
		// model for database field LOCATION(string)
		public string LOCATION
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOCATION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION"] = value;
			}
		}
		// model for database field LOC_OTHER_DESC(string)
		public string LOC_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_OTHER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_OTHER_DESC"] = value;
			}
		}
		// model for database field YEAR_DEVICE_INSTALLED(int)
		public int YEAR_DEVICE_INSTALLED
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR_DEVICE_INSTALLED"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["YEAR_DEVICE_INSTALLED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR_DEVICE_INSTALLED"] = value;
			}
		}
		// model for database field WAS_PROF_INSTALL_DONE(string)
		public string WAS_PROF_INSTALL_DONE
		{
			get
			{
				return base.dtModel.Rows[0]["WAS_PROF_INSTALL_DONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WAS_PROF_INSTALL_DONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WAS_PROF_INSTALL_DONE"] = value;
			}
		}
		// model for database field INSTALL_INSPECTED_BY(string)
		public string INSTALL_INSPECTED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_INSPECTED_BY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSTALL_INSPECTED_BY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_INSPECTED_BY"] = value;
			}
		}
		// model for database field INSTALL_OTHER_DESC(string)
		public string INSTALL_OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALL_OTHER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSTALL_OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSTALL_OTHER_DESC"] = value;
			}
		}
		// model for database field HEATING_USE(string)
		public string HEATING_USE
		{
			get
			{
				return base.dtModel.Rows[0]["HEATING_USE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HEATING_USE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HEATING_USE"] = value;
			}
		}
		// model for database field HEATING_SOURCE(string)
		public string HEATING_SOURCE
		{
			get
			{
				return base.dtModel.Rows[0]["HEATING_SOURCE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HEATING_SOURCE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HEATING_SOURCE"] = value;
			}
		}
		// model for database field OTHER_DESC(string)
		public string OTHER_DESC
		{
			get
			{
				return base.dtModel.Rows[0]["OTHER_DESC"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OTHER_DESC"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OTHER_DESC"] = value;
			}
		}
	
		
		#endregion
	}
}
